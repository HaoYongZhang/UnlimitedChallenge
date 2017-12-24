using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillClass;
using DG.Tweening;
using UnityEngine.EventSystems;

namespace UIHotKeyBar{
    public class MainView : MonoBehaviour
    {
        static int length = 5;
        //技能按钮集合
        public List<SkillClass.UIButton> hotKeysBtns = new List<SkillClass.UIButton>();

        public List<SkillClass.Skill> hotKeys_1 = new List<SkillClass.Skill>(length) { null, null, null, null, null };
        public List<SkillClass.Skill> hotKeys_2 = new List<SkillClass.Skill>(length) { null, null, null, null, null };

        public int currentTag = 1;

        void Start()
        {
            for (int i = 0; i < length; i++)
            {
                SkillClass.UIButton skillButton = SkillClass.UIButton.NewInstantiate();
                skillButton.transform.SetParent(transform, false);

                UIMouseDelegate mouseDelegate = skillButton.gameObject.GetComponent<UIMouseDelegate>();

                mouseDelegate.onPointerEnterDelegate = UIScene.Instance.pointToSkillButton;
                mouseDelegate.onPointerExitDelegate = UIScene.Instance.pointOutSkillButton;
                mouseDelegate.onPointerClickDelegate = OnHotKey;
                mouseDelegate.onDropDelegate = OnDrop;
                mouseDelegate.onlyDrop = true;

                hotKeysBtns.Add(skillButton);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// 点击热键
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <param name="ed">Ed.</param>
        void OnHotKey(GameObject obj, PointerEventData ed)
        {
            SkillClass.UIButton skillBtn = obj.GetComponent<SkillClass.UIButton>();

            if (skillBtn.skill == null)
            {
                return;
            }

            Global.hero.skillManager.OnRelease(skillBtn.skill);
        }

        /// <summary>
        /// 设置热键
        /// </summary>
        /// <param name="tag">Tag.</param>
        /// <param name="skill">Skill.</param>
        public void setHotKey(int tag, Skill skill)
        {
            //设置快捷栏按钮
            if(skill != null)
            {
                Skill oneSkill = SkillClass.Manager.GetOneSkillByID(skill.id);
                hotKeysBtns[tag].setSkill(oneSkill);

                currentHotKeys()[tag] = oneSkill;
            }
            else
            {
                hotKeysBtns[tag].setSkill(null);

                currentHotKeys()[tag] = null;
            }

        }


        /// <summary>
        /// 获取当前的快捷栏内容
        /// </summary>
        /// <returns>The current hot keys.</returns>
        public List<SkillClass.Skill> currentHotKeys()
        {
            if (currentTag == 1)
            {
                return hotKeys_1;
            }
            else
            {
                return hotKeys_2;
            }
        }

        /// <summary>
        /// 切换快捷释放栏
        /// </summary>
        public void switchBar()
        {
            Vector3 skillsBarPosition = GetComponent<RectTransform>().position;
            Vector3 oldPosition = skillsBarPosition;
            Vector3 newPosition = new Vector3(skillsBarPosition.x, skillsBarPosition.y - 150);

            GetComponent<RectTransform>()
                .DOMove(newPosition, 0.15f)
                .SetDelay(0)
                .SetEase(Ease.Linear)
                .OnComplete(delegate ()
                {
                
                    if (currentTag == 1)
                    {
                        currentTag = 2;
                    }
                    else
                    {
                        currentTag = 1;
                    }

                    List<SkillClass.Skill> list = currentHotKeys();
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i] != null)
                        {
                            if (hotKeysBtns[i].skill == null || list[i].id != hotKeysBtns[i].skill.id)
                            {
                                hotKeysBtns[i].setSkill(SkillClass.Manager.GetOneSkillByID(list[i].id));
                            }
                        }
                        else
                        {
                            hotKeysBtns[i].setSkill(null);
                        }
                    }

                    GetComponent<RectTransform>()
                         .DOMove(oldPosition, 0.15f)
                        .SetDelay(0)
                        .SetEase(Ease.Linear)
                        .OnComplete(delegate ()
                        {

                        });
                });

        }


        /// <summary>
        /// 判断放下的物体是否符合
        /// </summary>
        /// <returns><c>true</c>, if drop was caned, <c>false</c> otherwise.</returns>
        /// <param name="dropObj">Drop object.</param>
        bool CanDrop(GameObject dropObj)
        {
            //当物体不存在时
            if(dropObj == null)
            {
                return false;
            }

            //当物体是技能按钮时 
            SkillClass.UIButton skillBtn = dropObj.GetComponent<SkillClass.UIButton>();
            if(skillBtn != null)
            {
                return true;
            }

            return false;
        }

        void OnDrop(GameObject obj, PointerEventData eventData)
        {
            GameObject dropObj = eventData.pointerDrag;

            if(!CanDrop(dropObj))
            {
                return;
            }

            SkillClass.UIButton dropSkillBtn = dropObj.GetComponent<SkillClass.UIButton>();
            SkillClass.UIButton originSkillBtn = obj.GetComponent<SkillClass.UIButton>();

            int index = hotKeysBtns.IndexOf(originSkillBtn);

            for (int i = 0; i < hotKeysBtns.Count; i++)
            {
                //清除重复的技能id
                if(hotKeysBtns[i].skill != null)
                {
                    if (hotKeysBtns[i].skill.id == dropSkillBtn.skill.id)
                    {
                        hotKeysBtns[i].setSkill(null);
                    }
                }
            }

            currentHotKeys()[index] = dropSkillBtn.skill;
            hotKeysBtns[index].setSkill(dropSkillBtn.skill);
        }
    }
}


