using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace SkillClass
{
    /// <summary>
    /// 技能管理中心
    /// </summary>
    public class Manager : MonoBehaviour, ISkill
    {
        public Skill selectedSkill;

        Ray ray;  
        RaycastHit hit;  
        LineRenderer lineRenderer;  

        void Start()
        {
            
        }

        void Update()
        {
            cooldown();

            unactiveSkill();
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

            //如果当前有技能正在释放时，不能同时点其他技能
            if (Global.skillReleaseState != SkillReleaseState.available)
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
            if (Global.hero.property.mp < float.Parse(skill.data["costEnergy"]))
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
            Global.hero.rangeManager.radius = float.Parse(skill.data["skillDistance"]);
            Global.hero.rangeManager.rendering = true;
        }

        /// <summary>
        /// 已选择技能目标
        /// </summary>
        public void OnSelected()
        {
            selectedSkill.releaseState = SkillReleaseState.selected;
            if(CanSelected(selectedSkill))
            {
                OnImplemented(selectedSkill);
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
            float skillDistance = float.Parse(skill.data["skillDistance"]);
            if (Physics.Raycast(cameraRay, out rayHit))
            {
                Vector3 heroPosition = new Vector3(transform.position.x, 5, transform.position.z);
                Vector3 rayPosition = new Vector3(rayHit.point.x, 5, rayHit.point.z);
                //人物到鼠标点击位置的实际直线距离
                distance = (heroPosition - rayPosition).magnitude;

                Debug.Log("heroPosition =" + heroPosition);
                Debug.Log("rayHit =" + rayPosition);
                Debug.Log("distance =" + distance);
                Debug.Log("skillDistance =" + skillDistance);

                //点击位置大于施法距离
                if (distance > skillDistance)
                {
                    Debug.Log("点击位置大于施法距离");
                    return false;
                }
                //点击位置小于施法距离，成功施法
                else
                {
                    return true;
                }

            }
            else
            {
                Debug.Log("点击位置超出游戏范围");
                return false;
            }
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
                if (skill.effectRange == SkillEffectRange.self)
                {
                    OnImplemented(skill);
                }
                //如果技能不是作用于自身时，选择释放地点
                else
                {
                    skill.releaseState = SkillReleaseState.selecting;
                    OnSelecting(skill);
                }
            }


        }


        /// <summary>
        /// 实现技能释放
        /// </summary>
        /// <param name="skill">Skill.</param>
        public void OnImplemented(Skill skill)
        {
            Global.hero.property.mp -= float.Parse(skill.data["costEnergy"]);

            skill.releaseState = SkillReleaseState.cooldown;

            switch (skill.type)
            {
                case SkillType.attack:
                    {
                        
                    }
                    break;
                case SkillType.defense:
                    {
                    }
                    break;
                case SkillType.treatment:
                    {
                        treatment(skill);
                    }
                    break;
                case SkillType.intensify:
                    {
                        intensify(skill);
                    }
                    break;
                case SkillType.complex:
                    {

                    }
                    break;
                case SkillType.specialty:
                    {

                    }
                    break;
            }
        }

        /// <summary>
        /// 技能持续效果结束
        /// </summary>
        /// <param name="skill">Skill.</param>
        public void OnDurationEnd(Skill skill)
        {
            switch (skill.type)
            {
                case SkillType.attack:
                    {

                    }
                    break;
                case SkillType.defense:
                    {
                    }
                    break;
                case SkillType.treatment:
                    {
                        endIntensify(skill);
                    }
                    break;
                case SkillType.intensify:
                    {
                        endIntensify(skill);
                    }
                    break;
                case SkillType.complex:
                    {

                    }
                    break;
                case SkillType.specialty:
                    {
                    }
                    break;

            }
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
                        if (oneSkill.isInDuration && oneSkill.currentCoolDown >= float.Parse(oneSkill.data["duration"]))
                        {
                            OnDurationEnd(oneSkill);
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
        void unactiveSkill()
        {
            foreach (Skill skill in Global.skills)
            {
                //当技能是被动而且未开启时，开启被动技能
                if (!skill.isActive && skill.isInDuration == false)
                {
                    intensify(skill);
                }
            }
        }


        /// <summary>
        /// 使用治疗技能
        /// </summary>
        /// <returns>The treatment.</returns>
        /// <param name="skill">Skill.</param>
        void treatment(Skill skill)
        {
            string increateHp = skill.data["increateHp"];
            if (increateHp != null && increateHp != "")
            {
                //截取字符串，获得属性增加的值
                float createValue = float.Parse(increateHp);
                //使用治疗技能后，加上的血量
                Global.hero.property.hp += createValue;

                //Color hpColor = new Color(90 / 255f, 160 / 255f, 55 / 255f, 1);

                Color hpColor = ColorTool.getColor(90, 160, 55);
                ParticleManager.vertical(Global.hero.gameObject, hpColor);

                intensify(skill);
            }
        }

        /// <summary>
        /// 使用强化技能
        /// </summary>
        /// <param name="skill">Skill.</param>
        void intensify(Skill skill)
        {
            //添加强化的小图标
            UIScene.Instance.addStatusIcon(skill);
            //开始技能的持续时间
            skill.isInDuration = true;
            //获取HeroManager的属性
            Property property = Global.hero.property;

            foreach (KeyValuePair<string, string> dict in skill.data)
            {
                if (PropertyTool.isExist(property, dict.Key))
                {
                    //截取字符串，获得属性增加的值
                    float createValue = float.Parse(dict.Value);
                    //动态获取当前的属性值
                    float propertyValue = float.Parse(PropertyTool.ReflectGetter(property, dict.Key).ToString());
                    //使用强化技能时间开始时，应该加上强化属性
                    PropertyTool.ReflectSetter(property, dict.Key, propertyValue + createValue);
                }
            }
        }

        /// <summary>
        /// 强化技能的持续时间结束
        /// </summary>
        /// <param name="skill">Skill.</param>
        void endIntensify(Skill skill)
        {
            //删除强化的小图标
            UIScene.Instance.removeStatusIcon(skill);
            //结束技能的持续时间
            skill.isInDuration = false;
            //获取Hero的属性
            Property property = Global.hero.property;

            foreach (KeyValuePair<string, string> dict in skill.data)
            {
                if (PropertyTool.isExist(property, dict.Key))
                {
                    //截取字符串，获得属性增加的值
                    float createValue = float.Parse(dict.Value);
                    //动态获取当前的属性值
                    float propertyValue = float.Parse(PropertyTool.ReflectGetter(property, dict.Key).ToString());
                    //使用强化技能时间结束后，应该减去强化属性
                    PropertyTool.ReflectSetter(property, dict.Key, propertyValue - createValue);
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
    }
}
