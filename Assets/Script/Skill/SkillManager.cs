﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace SkillClass
{
	public class SkillManager : MonoBehaviour {
        // SkillManager单例
        public static SkillManager _instance;

        void Awake()
        {
            _instance = this;
        }

		void Start () {
			defaultSkillSetting ();
		}
		
		void Update () {
			cooldown();
            keepUnactiveSkill();
		}

		//技能的默认设置
		void defaultSkillSetting()
		{
			GameObject skillsBar = GameObject.Find("SkillsBar");
            SceneUI.Instance.skillButtons = new List<Button>(skillsBar.GetComponentsInChildren<Button>());
            for (int i = 0; i < SceneUI.Instance.skillButtons.Count; i++)
			{
                int j = i;

                Button btn = SceneUI.Instance.skillButtons[i];

				btn.onClick.AddListener(delegate ()
				{
					this.useSkill(j);
				});
			}

            for (int i = 0; i < Global.activeSkills.Count; i++) {
                Image maskImage = SceneUI.Instance.skillButtons[i].transform.Find("MaskImage").GetComponent<Image>();
                Image icon = maskImage.transform.Find("Icon").GetComponent<Image>();
                icon.sprite = Global.activeSkills[i].imageSprite;
			}
		}

        /// <summary>
        /// 维持被动技能
        /// </summary>
        void keepUnactiveSkill()
        {
            foreach(Skill skill in Global.unactiveSkills)
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
            for (int i = 0; i < Global.activeSkills.Count; i++)
			{
                Skill skill = Global.activeSkills[i];
				if (skill.isCooldown)
				{
                    Image maskImage = SceneUI.Instance.skillButtons[i].transform.Find("MaskImage").GetComponent<Image>();
                    Image cooldownImage = maskImage.transform.Find("CooldownImage").GetComponent<Image>();
                   
                    Text cooldownText = SceneUI.Instance.skillButtons[i].transform.Find("CooldownText").GetComponent<Text>();
					if (skill.currentCoolDown < float.Parse(skill.data["cooldown"]))
					{
						// 更新冷却
						skill.currentCoolDown += Time.deltaTime;

                        //每秒显示技能冷却时间
                        if (Time.time - skill.second >= 1.0f) {
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

		//使用技能
		public void useSkill(int i)
		{
			// 如果技能不存在，返回
            if (Global.activeSkills.Count < (i + 1))
			{
				return;
			}

            Skill skill = Global.activeSkills[i];

			// 如果是被动技能，返回
			if (skill.data["isActive"] == "0")
			{
				return;
			}

			// 如果技能正在冷却中，返回
			if (skill.isCooldown == true) {
				return;
			}

            HeroManager heroManager = GetComponent<HeroManager> ();

			// 如果蓝量不足，返回
            if (heroManager.property.mp < float.Parse (skill.data ["costEnergy"])) {
				Debug.Log ("能量不足");
				return;
			}

            heroManager.property.mp -= float.Parse (skill.data ["costEnergy"]);
			skill.isCooldown = true;
            Image maskImage = SceneUI.Instance.skillButtons[i].transform.Find("MaskImage").GetComponent<Image>();
            Image cooldownImage = maskImage.transform.Find("CooldownImage").GetComponent<Image>();
            cooldownImage.fillAmount = 1;
            //			Image skillImage = SceneUI.Instance.skillButtons[i].transform.Find("SkillImage").GetComponent<Image>();

			switch (skill.type) {
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


        void attack(Skill skill)
        {
            //获取英雄对象
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            //获取HeroManager的属性
            Property property = player.GetComponent<HeroManager>().property;
            Knowledge knowledge = player.GetComponent<HeroManager>().knowledge;

            //动态获取当前的属性值
            int knowledgeValue = int.Parse(PropertyUtil.ReflectGetter(knowledge, skill.addlData["knowledgePromote"]).ToString());

            DamageType damageType = SkillEnum.getEnum<DamageType>(skill.addlData["damageType"]);
            SkillEffectType skillEffectType = SkillEnum.getEnum<SkillEffectType>(skill.addlData["skillEffectType"]);
            float damage =
                (float.Parse(skill.addlData["basicDamage"]) +
                 property.strength * float.Parse(skill.addlData["strength"]) +
                 property.agility * float.Parse(skill.addlData["agility"]) +
                 property.intellect * float.Parse(skill.addlData["agility"])) *
                (1 + Math.Round((float)knowledgeValue / 10, 1));



        }


        /// <summary>
        /// 使用治疗技能
        /// </summary>
        /// <returns>The treatment.</returns>
        /// <param name="skill">Skill.</param>
        void treatment(Skill skill)
        {
            HeroManager heroManager = GetComponent<HeroManager>();
            string increateHp = skill.addlData["increateHp"];
            if (increateHp != null)
            {
                //截取字符串，获得属性增加的值
                float createValue = float.Parse(increateHp);
                //使用治疗技能后，加上的血量
                heroManager.property.hp += createValue;

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
            SceneUI.Instance.addSkillStatusIcon(skill);
            //开始技能的持续时间
            skill.isInDuration = true;
			//获取英雄对象
			Transform player = GameObject.FindGameObjectWithTag ("Player").transform;
            //获取英雄对象下面的HeroManager
            HeroManager heroManager = player.GetComponent<HeroManager> ();
            //获取HeroManager的属性
            Property property = heroManager.property;

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
            SceneUI.Instance.removeSkillStatusIcon(skill);
            //结束技能的持续时间
            skill.isInDuration = false;
            //获取英雄对象
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            //获取英雄对象下面的HeroManager
            HeroManager heroManager = player.GetComponent<HeroManager>();
            //获取HeroManager的属性
            Property property = heroManager.property;

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
