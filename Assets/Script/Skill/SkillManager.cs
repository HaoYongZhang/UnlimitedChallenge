using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace SkillClass
{
    public class SkillManager : MonoBehaviour
    {
        // SkillManager单例
        public static SkillManager _instance;

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
            cooldown();
            keepUnactiveSkill();

            if (Global.skillRelease == SkillRelease.selected)
            {
                inAttack(hasSelectedSkill);
                Global.skillRelease = SkillRelease.none;
            }
        }

        //技能的默认设置
        void defaultSkillSetting()
        {
            GameObject skillsBar = UIScene.Instance.skillsBar;

            UIScene.Instance.skillButtons = new List<Button>(skillsBar.GetComponentsInChildren<Button>());
            for (int i = 0; i < UIScene.Instance.skillButtons.Count; i++)
            {
                int j = i;

                Button btn = UIScene.Instance.skillButtons[i];

                btn.onClick.AddListener(delegate ()
                {
                    onSkillBarButtion(j);
                });
            }

            for (int i = 0; i < Global.shortcutsSkills.Count; i++)
            {
                Image maskImage = UIScene.Instance.skillButtons[i].transform.Find("MaskImage").GetComponent<Image>();
                Image icon = maskImage.transform.Find("Icon").GetComponent<Image>();
                icon.sprite = Global.shortcutsSkills[i].imageSprite;
            }
        }

        void onSkillBarButtion(int i)
        {
            if (i > (Global.shortcutsSkills.Count - 1))
            {
                return;
            }
            else
            {
                useSkill(Global.shortcutsSkills[i]);
            }
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
            for (int i = 0; i < Global.shortcutsSkills.Count; i++)
            {
                Skill skill = Global.shortcutsSkills[i];
                if (skill.isCooldown)
                {
                    Image maskImage = UIScene.Instance.skillButtons[i].transform.Find("MaskImage").GetComponent<Image>();
                    Image cooldownImage = maskImage.transform.Find("CooldownImage").GetComponent<Image>();

                    Text cooldownText = UIScene.Instance.skillButtons[i].transform.Find("CooldownText").GetComponent<Text>();
                    if (skill.currentCoolDown < float.Parse(skill.data["cooldown"]))
                    {
                        // 更新冷却
                        skill.currentCoolDown += Time.deltaTime;

                        //每秒显示技能冷却时间
                        if (Time.time - skill.second >= 1.0f)
                        {
                            skill.second = Time.time;
                            //cooldownText.text = Utility.Math.Round((float.Parse (skill.data ["cooldown"]) - skill.currentCoolDown), 0).ToString();
                        }

                        // 显示冷却动画
                        cooldownImage.fillAmount = 1 - (skill.currentCoolDown / float.Parse(skill.data["cooldown"]));

                        //当技能持续时间结束时
                        if (skill.isInDuration && skill.currentCoolDown >= float.Parse(skill.data["duration"]))
                        {
                            afterCooldown(skill);
                        }
                    }
                    else
                    {
                        skill.currentCoolDown = 0;
                        skill.isCooldown = false;
                        cooldownImage.fillAmount = 0;
                        //cooldownText.text = "";
                    }
                }

            }
        }

        /// <summary>
        /// 技能冷却结束
        /// </summary>
        /// <param name="skill">Skill.</param>
        void afterCooldown(Skill skill)
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
        public void useSkill(Skill skill)
        {
            if (beforeUseSkill(skill) == false)
            {
                return;
            }

            switch (skill.type)
            {
                case SkillType.attack:
                    {
                        attack(skill);
                    }
                    break;
                case SkillType.defense:
                    {
                    }
                    break;
                case SkillType.treatment:
                    {
                        inUseSkill(skill);
                        treatment(skill);
                    }
                    break;
                case SkillType.intensify:
                    {
                        inUseSkill(skill);
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

        bool beforeUseSkill(Skill skill)
        {
            //如果当前有技能正在释放时，不能同时点其他技能
            if (Global.skillRelease != SkillRelease.none)
            {
                return false;
            }

            // 如果技能不存在，返回
            if (skill == null)
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
                Debug.Log("能量不足");
                return false;
            }

            return true;
        }

        void inUseSkill(Skill skill)
        {
            skill.isCooldown = true;

            Global.hero.property.mp -= float.Parse(skill.data["costEnergy"]);

            Image maskImage = UIScene.Instance.skillButtons[Global.shortcutsSkills.IndexOf(skill)].transform.Find("MaskImage").GetComponent<Image>();
            Image cooldownImage = maskImage.transform.Find("CooldownImage").GetComponent<Image>();
            cooldownImage.fillAmount = 1;
        }

        void attack(Skill skill)
        {
            hasSelectedSkill = skill;

            beforeAttack(skill);
        }

        void beforeAttack(Skill skill)
        {
            Range range = GetComponent<Range>();
            range.setSkillRange(skill);

            Global.skillRelease = SkillRelease.selecting;
        }

        void inAttack(Skill skill)
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
            string increateHp = skill.addlData["increateHp"];
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
            //获取英雄对象
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            //获取HeroManager的属性
            Property property = Global.hero.property;

            foreach (KeyValuePair<string, string> dict in skill.addlData)
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
            //获取英雄对象
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            //获取HeroManager的属性
            Property property = Global.hero.property;

            foreach (KeyValuePair<string, string> dict in skill.addlData)
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
    }
}
