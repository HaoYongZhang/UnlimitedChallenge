using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SkillClass;
using UnityEngine.EventSystems;

namespace UIHeroInfo
{
    public class SkillView : MonoBehaviour
    {

        public GameObject skillSet;
        public GameObject buttonList_1;
        public GameObject buttonList_2;

        public List<Button> skillTypeButtonList = new List<Button>();

        //拖动物品时的临时创建对象
        GameObject dragTempObject;

        public int currentTag;

        void Start()
        {
            foreach (Button btn in buttonList_1.GetComponentsInChildren<Button>())
            {
                skillTypeButtonList.Add(btn);
            }

            foreach (Button btn in buttonList_2.GetComponentsInChildren<Button>())
            {
                skillTypeButtonList.Add(btn);
            }

            for (int i = 0; i < skillTypeButtonList.Count; i++)
            {
                int j = i;
                skillTypeButtonList[i].onClick.AddListener(delegate ()
                {
                    onClick(j);
                });
            }

            setSkillSet();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void onClick(int i)
        {
            if (currentTag == i)
            {
                return;
            }

            currentTag = i;

            //改变按钮颜色
            for (int j = 0; j < skillTypeButtonList.Count; j++)
            {
                Image image = skillTypeButtonList[j].GetComponent<Image>();
                Text label = skillTypeButtonList[j].GetComponentInChildren<Text>();
                //当前选中的
                if (i == j)
                {
                    image.sprite = UIHeroInfo.MainView.selectedBtnBG;
                    label.color = ColorTool.getColor("#333333");
                }
                else
                {
                    image.sprite = UIHeroInfo.MainView.unselectedBtnBG;
                    label.color = ColorTool.getColor("#ffffff");
                }
            }

            setSkillSet();
        }

        public void setSkillSet()
        {
            //清除所有技能列表
            for (int ii = 0; ii < skillSet.transform.childCount; ii++)
            {
                Destroy(skillSet.transform.GetChild(ii).gameObject);
            }

            //由i + 1获取skillType
            SkillType type = (SkillType)Enum.Parse(typeof(SkillType), (currentTag + 1).ToString());

            foreach (Skill skill in Global.skills)
            {
                if (skill.type == type)
                {
                    SkillClass.UIButton skillButton = SkillClass.UIButton.NewInstantiate(skill);
                    skillButton.transform.SetParent(skillSet.transform, false);

                    UIMouseDelegate mouseDelegate = skillButton.gameObject.GetComponent<UIMouseDelegate>();
                    mouseDelegate.onPointerClickDelegate = onClick;
                    mouseDelegate.onPointerEnterDelegate = UIScene.Instance.pointToSkillButton;
                    mouseDelegate.onPointerExitDelegate = UIScene.Instance.pointOutSkillButton;

                    mouseDelegate.onBeginDragDelegate = onBeginDrag;
                    mouseDelegate.onDragDelegate = onDrag;
                    mouseDelegate.onEndDragDelegate = onEndDrag;
                }
            }
        }

        void onClick(GameObject obj, PointerEventData eventData)
        {
            UIScene.Instance.hideSkillInfo();

            SkillClass.UIButton skillBtn = obj.GetComponent<SkillClass.UIButton>();

            Global.hero.skillManager.OnRelease(skillBtn.skill);
        }

        void onBeginDrag(GameObject obj, PointerEventData eventData)
        {
            //代替品实例化
            dragTempObject = new GameObject("DragTempObject");
            dragTempObject.transform.SetParent(UIScene.Instance.transform, false);
            dragTempObject.AddComponent<RectTransform>();

            SkillClass.UIButton tempskillButton = SkillClass.UIButton.NewInstantiate();
            tempskillButton.transform.SetParent(dragTempObject.transform, false);
            tempskillButton.setSkill(obj.GetComponentInChildren<SkillClass.UIButton>().skill);

            //防止拖拽结束时，代替品挡住了准备覆盖的对象而使得 OnDrop（） 无效
            CanvasGroup group = dragTempObject.AddComponent<CanvasGroup>();
            group.blocksRaycasts = false;
        }

        void onDrag(GameObject obj, PointerEventData eventData)
        {
            //并将拖拽时的坐标给予被拖拽对象的代替品

            Vector3 movePosition = new Vector3(Input.mousePosition.x + 20, Input.mousePosition.y - 20);

            dragTempObject.transform.position = movePosition;
        }

        void onEndDrag(GameObject obj, PointerEventData eventData)
        {
            //拖拽结束，销毁代替品
            if (dragTempObject)
            {
                Destroy(dragTempObject);
            }
        }
    }

}

