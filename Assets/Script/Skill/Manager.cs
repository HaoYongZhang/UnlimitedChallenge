using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using EnemyClass;

namespace SkillClass
{
    /// <summary>
    /// 技能管理中心
    /// </summary>
    public class Manager : MonoBehaviour
    {
        public Skill selectedSkill;
        public Vector3 selectedPosition;

        void Start()
        {
            
        }

        void Update()
        {
            cooldown();

            UnactiveSkill();
        }

        /// <summary>
        /// 释放技能
        /// </summary>
        /// <param name="_skill">Skill.</param>
        public void OnRelease(Skill _skill)
        {
            Skill skill = GetOneSkillByID(_skill.id);
            ///判断能否释放技能
            if (CanRelease(skill))
            {
                //如果技能作用于自身时，直接释放
                if (skill.actionRange == SkillActionRange.self)
                {
                    SkillImplementation.Implement(gameObject, skill);
                }
                //如果技能不是作用于自身时，选择释放地点
                else
                {
                    OnSelecting(skill);
                }
            }
        }

        /// <summary>
        /// 判断能否释放技能
        /// </summary>
        /// <returns><c>true</c>, if release skill was caned, <c>false</c> otherwise.</returns>
        /// <param name="skill">Skill.</param>
        public bool CanRelease(Skill skill)
        {
            // 如果技能不存在，返回
            if (skill == null)
            {
                return false;
            }

            //正在进行攻击动画时，不能释放技能
            if(Global.hero.animationManager.isAttacking)
            {
                return false;
            }

            //如果当前有技能正在选择目标时，不能同时点其他技能
            if (Global.skillReleaseState == SkillReleaseState.selecting)
            {
                return false;
            }

            //如果当前有技能正在选择释放时，不能同时点其他技能
            if (Global.skillReleaseState == SkillReleaseState.selected)
            {
                return false;
            }

            // 如果是被动技能，返回
            if (skill.data["isActive"] == "0")
            {
                return false;
            }

            // 如果技能正在冷却中，返回
            if (skill.releaseState == SkillReleaseState.cooldown)
            {
                return false;
            }

            // 如果蓝量不足，返回
            if (Global.hero.propertyManager.Mp < float.Parse(skill.data["costEnergy"]))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 选择技能目标中
        /// </summary>
        /// <param name="skill">Skill.</param>
        public void OnSelecting(Skill skill)
        {
            selectedSkill = skill;
            skill.releaseState = SkillReleaseState.selecting;

            float distance = float.Parse(skill.data["distance"]);

            if (RangeTool.IsSectorActionRange(skill.actionRange))
            {
                Global.hero.rangeManager.SetSectorRange(distance, RangeTool.GetSectorAngle(skill.actionRange));
            }
            else
            {
                Global.hero.rangeManager.SetCircleRange(distance);
            }
        }

        /// <summary>
        /// 已选择技能目标
        /// </summary>
        public void OnSelected()
        {
            if(CanSelected(selectedSkill))
            {
                selectedSkill.releaseState = SkillReleaseState.selected;
                OnImplemented(selectedSkill);
            }
            else
            {
                selectedSkill.releaseState = SkillReleaseState.available;
            }
        }

        /// <summary>
        /// 检验已选择技能目标是否有效
        /// </summary>
        /// <returns><c>true</c>, if selected was caned, <c>false</c> otherwise.</returns>
        /// <param name="skill">Skill.</param>
        public bool CanSelected(Skill skill)
        {
            Ray cameraRay = Global.mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            float distance;
            float skillDistance = float.Parse(skill.data["distance"]);
            if (Physics.Raycast(cameraRay, out rayHit))
            {
                Vector3 heroPosition = new Vector3(transform.position.x, 2.5f, transform.position.z);
                Vector3 rayPosition = new Vector3(rayHit.point.x, 2.5f, rayHit.point.z);
                //人物到鼠标点击位置的实际直线距离
                distance = (heroPosition - rayPosition).magnitude;

                selectedPosition = new Vector3(rayPosition.x, 2.5f, rayPosition.z);

                if (RangeTool.IsSectorActionRange(skill.actionRange))
                {
                    return true;
                }
                else
                {
                    //点击位置大于施法距离
                    if (distance > skillDistance)
                    {
                        return false;
                    }
                    //点击位置小于施法距离，成功施法
                    else
                    {
                        selectedPosition = new Vector3(rayPosition.x, 2.5f, rayPosition.z);
                        return true;
                    }
                }
            }
            else
            {
                Debug.Log("点击位置超出游戏范围");
                return false;
            }
        }


        /// <summary>
        /// 实现技能释放
        /// </summary>
        /// <param name="skill">Skill.</param>
        public void OnImplemented(Skill skill)
        {
            Global.hero.propertyManager.Mp -= float.Parse(skill.data["costEnergy"]);

            Global.hero.fightManager.SkillAttack(skill);
        }

        /// <summary>
        /// 技能释放完成
        /// </summary>
        /// <param name="skill">Skill.</param>
        public void OnFinished(Skill skill)
        {
            skill.releaseState = SkillReleaseState.cooldown;
        }

        /// <summary>
        /// 技能冷却
        /// </summary>
        void cooldown()
        {
            for (int i = 0; i < Global.skills.Count; i++)
            {
                Skill oneSkill = GetOneSkillByID(Global.skills[i].id);
                if (oneSkill.releaseState == SkillReleaseState.cooldown)
                {
                    if (oneSkill.currentCoolDown < float.Parse(oneSkill.data["cooldown"]))
                    {
                        // 更新冷却
                        oneSkill.currentCoolDown += Time.deltaTime;

                        //每秒显示技能冷却时间
                        if (Time.time - oneSkill.second >= 1.0f)
                        {
                            oneSkill.second = Time.time;
                        }

                        //当技能持续时间结束时
                        if (oneSkill.isInDuration && oneSkill.currentCoolDown >= oneSkill.duration)
                        {
                            SkillImplementation.DurationEnd(gameObject, oneSkill);
                        }
                    }
                    else
                    {
                        oneSkill.currentCoolDown = 0;
                        oneSkill.releaseState = SkillReleaseState.available;
                    }
                }

            }
        }

        /// <summary>
        /// 被动技能
        /// </summary>
        void UnactiveSkill()
        {
            foreach (Skill skill in Global.skills)
            {
                //当技能是被动而且未开启时，开启被动技能
                if (!skill.isActive && skill.isInDuration == false)
                {
                    SkillImplementation.Implement(gameObject, skill);
                }
            }
        }

        /// <summary>
        /// 返回同一个技能，共用这个技能的所有状态，否则会有深拷贝造成技能数据不一致
        /// </summary>
        /// <returns>The one skill by identifier.</returns>
        /// <param name="_id">Identifier.</param>
        public static Skill GetOneSkillByID(string _id){
            foreach(Skill skill in Global.skills)
            {
                if(skill.id == _id)
                {
                    return skill;
                }
            }

            return null;
        }

        public void SkillEffect(GameObject effect)
        {
            effect = Instantiate(effect);
            effect.transform.SetParent(transform, false);
        }
    }
}
