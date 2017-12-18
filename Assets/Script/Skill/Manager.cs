using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using UnityEngine.EventSystems;

namespace SkillClass
{
    /// <summary>
    /// 技能管理中心
    /// </summary>
    public class Manager : MonoBehaviour, ISkill
    {
        // SkillManager单例
        public static Manager _instance;

        public Skill hasSelectedSkill;

        void Awake()
        {
            _instance = this;
        }

        void Start()
        {
            defaultSkillSetting();
        }

        void Update()
        {
            //cooldown();
            keepUnactiveSkill();

            if (Global.skillRelease == SkillRelease.selected)
            {
                inAttackSkill(hasSelectedSkill);
                Global.skillRelease = SkillRelease.none;
            }
        }

        public void OnRelease(Skill skill)
        {
            
        }

        public void OnDurationEnd(Skill skill)
        {
            
        }

        public void OnSelecting(Skill skill)
        {
            
        }

        //技能的默认设置
        void defaultSkillSetting()
        {
            for (int i = 0; i < UIScene.Instance.skillButtons.Count; i++)
            {
                SkillClass.UIButton btn = UIScene.Instance.skillButtons[i];

                btn.gameObject.GetComponent<UIMouseDelegate>().onPointerClickDelegate = onClickSkillButton;
            }
        }

        public void onClickSkillButton(GameObject obj, PointerEventData ed)
        {
            SkillClass.UIButton skillBtn = obj.GetComponent<SkillClass.UIButton>();

            if(skillBtn.skill == null)
            {
                return;
            }

            releaseSkill(skillBtn.skill);
        }

        /// <summary>
        /// 维持被动技能
        /// </summary>
        void keepUnactiveSkill()
        {
            foreach (Skill skill in Global.unactiveSkills)
            {
                if (skill.isInDuration == false)
                {
                    intensify(skill);
                }
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
                if (oneSkill.isCooldown)
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
                            endDuration(oneSkill);
                        }
                    }
                    else
                    {
                        oneSkill.currentCoolDown = 0;
                        oneSkill.isCooldown = false;
                    }
                }

            }
        }

        /// <summary>
        /// 技能冷却结束
        /// </summary>
        /// <param name="skill">Skill.</param>
        void endDuration(Skill skill)
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
        /// 使用技能
        /// </summary>
        /// <param name="skill">Skill.</param>
        public void releaseSkill(Skill skill)
        {
            Skill oneSkill = GetOneSkillByID(skill.id);

            if (CanRelease(oneSkill) == false)
            {
                return;
            }

            switch (oneSkill.type)
            {
                case SkillType.attack:
                    {
                        releaseAttackSkill(oneSkill);
                    }
                    break;
                case SkillType.defense:
                    {
                    }
                    break;
                case SkillType.treatment:
                    {
                        inUseSkill(oneSkill);
                        treatment(oneSkill);
                    }
                    break;
                case SkillType.intensify:
                    {
                        inUseSkill(oneSkill);
                        intensify(oneSkill);
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
            if (Global.skillRelease != SkillRelease.none)
            {
                return false;
            }

            // 如果是被动技能，返回
            if (skill.data["isActive"] == "0")
            {
                return false;
            }

            // 如果技能正在冷却中，返回
            if (skill.isCooldown == true)
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

        void inUseSkill(Skill skill)
        {
            skill.isCooldown = true;

            Global.hero.property.mp -= float.Parse(skill.data["costEnergy"]);
        }

        void releaseAttackSkill(Skill skill)
        {
            hasSelectedSkill = skill;

            beforeAttack(skill);
        }

        void beforeAttack(Skill skill)
        {
            RangeManager rangeManager = GetComponent<RangeManager>();
            rangeManager.setSkillRange(skill);

            Global.skillRelease = SkillRelease.selecting;
        }

        void inAttackSkill(Skill skill)
        {
            ////获取英雄对象
            //Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            ////获取HeroManager的属性
            //Property property = player.GetComponent<HeroManager>().property;
            //Knowledge knowledge = player.GetComponent<HeroManager>().knowledge;

            ////动态获取当前的属性值
            //int knowledgeValue = int.Parse(PropertyUtil.ReflectGetter(knowledge, skill.addlData["knowledgePromote"]).ToString());

            //DamageType damageType = SkillEnum.getEnum<DamageType>(skill.addlData["damageType"]);
            //SkillEffectType skillEffectType = SkillEnum.getEnum<SkillEffectType>(skill.addlData["skillEffectType"]);
            //float damage =
                //(float.Parse(skill.addlData["basicDamage"]) +
                // property.strength * float.Parse(skill.addlData["strength"]) +
                // property.agility * float.Parse(skill.addlData["agility"]) +
                // property.intellect * float.Parse(skill.addlData["agility"])) *
                //(1 + Math.Round((float)knowledgeValue / 10, 1));

            bool isSuccessRelease = GetComponent<ShootManager>().handleSkill(skill);
            if(isSuccessRelease)
            {
                inUseSkill(skill);
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
            if (increateHp != null)
            {
                //截取字符串，获得属性增加的值
                float createValue = float.Parse(increateHp);
                //使用治疗技能后，加上的血量
                Global.hero.property.hp += createValue;

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
            UIScene.Instance.addSkillStatusIcon(skill);
            //开始技能的持续时间
            skill.isInDuration = true;
            //获取HeroManager的属性
            Property property = Global.hero.property;

            foreach (KeyValuePair<string, string> dict in skill.data)
            {
                if (PropertyUtil.isExist(property, dict.Key))
                {
                    //截取字符串，获得属性增加的值
                    float createValue = float.Parse(dict.Value);
                    //动态获取当前的属性值
                    float propertyValue = float.Parse(PropertyUtil.ReflectGetter(property, dict.Key).ToString());
                    //使用强化技能时间开始时，应该加上强化属性
                    PropertyUtil.ReflectSetter(property, dict.Key, propertyValue + createValue);
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
            UIScene.Instance.removeSkillStatusIcon(skill);
            //结束技能的持续时间
            skill.isInDuration = false;
            //获取Hero的属性
            Property property = Global.hero.property;

            foreach (KeyValuePair<string, string> dict in skill.data)
            {
                if (PropertyUtil.isExist(property, dict.Key))
                {
                    //截取字符串，获得属性增加的值
                    float createValue = float.Parse(dict.Value);
                    //动态获取当前的属性值
                    float propertyValue = float.Parse(PropertyUtil.ReflectGetter(property, dict.Key).ToString());
                    //使用强化技能时间结束后，应该减去强化属性
                    PropertyUtil.ReflectSetter(property, dict.Key, propertyValue - createValue);
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
