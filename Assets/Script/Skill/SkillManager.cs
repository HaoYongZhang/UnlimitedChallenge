using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace SkillClass
{
	public class SkillManager : MonoBehaviour {
        // SkillManager单例
        public static SkillManager _instance;
        //每秒记录
		private float second;

        void Awake()
        {
            _instance = this;
        }

		void Start () {
			defaultSkillSetting ();
			second = Time.time;

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
                Image image = SceneUI.Instance.skillButtons[i].transform.Find("SkillImage").GetComponent<Image>();
                image.sprite = Global.activeSkills[i].imageSprite;
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
                    intensify(skill, false);
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
                    Text cooldownText = SceneUI.Instance.skillButtons[i].transform.Find("CooldownText").GetComponent<Text>();
					if (skill.currentCoolDown < float.Parse(skill.data["cooldown"]))
					{
						// 更新冷却
						skill.currentCoolDown += Time.deltaTime;

                        //每秒显示技能冷却时间
						if (Time.time - second >= 1.0f) {
							second = Time.time;
							cooldownText.text = Utility.Math.Round((float.Parse (skill.data ["cooldown"]) - skill.currentCoolDown), 0).ToString();
						}

                        // 显示冷却动画
                        maskImage.fillAmount = 1 - (skill.currentCoolDown / float.Parse(skill.data["cooldown"]));

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
						maskImage.fillAmount = 0;
						cooldownText.text = "";
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
				case SkillType.Attack:
					{

					}
					break;
				case SkillType.Defense:
					{
					}
					break;
				case SkillType.Treatment:
					{
					}
					break;
				case SkillType.Intensify:
					{
						intensify(skill, true);
					}
					break;
				case SkillType.Complex:
					{

					}
					break;
				case SkillType.Specialty:
					{
					}
					break;

			}
		}

		//使用技能
		void useSkill(int i)
		{
			Debug.Log ("触发");

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
			maskImage.fillAmount = 1;
            //			Image skillImage = SceneUI.Instance.skillButtons[i].transform.Find("SkillImage").GetComponent<Image>();

			switch (skill.type) {
			case SkillType.Attack:
				{
					
				}
			break;
			case SkillType.Defense:
				{
				}
				break;
			case SkillType.Treatment:
				{
                    treatment(skill);
				}
				break;
			case SkillType.Intensify:
				{
					intensify(skill, false);
				}
				break;
			case SkillType.Complex:
				{

				}
				break;
			case SkillType.Specialty:
				{
				}
				break;

			}
		}


        void treatment(Skill skill)
        {
            HeroManager heroManager = GetComponent<HeroManager>();
            string increateHp = skill.addlData["increateHp"];
            if (increateHp != null)
            {
                //截取字符串，获得+/-符号
                string symbol = increateHp.Substring(0, 1);
                //截取字符串，获得属性增加的值
                float createValue = float.Parse(increateHp.Substring(1, increateHp.Length - 1));
                heroManager.property.hp += createValue;
            }
        }

		/// <summary>
		/// 使用强化技能
		/// </summary>
		/// <param name="skill">Skill.</param>
		/// <param name="isEnd">强化技能时间是否结束</param>
		void intensify(Skill skill, bool isEnd)
		{
			if(isEnd == false)
			{
				skill.isInDuration = true;
			}
			else
			{
				skill.isInDuration = false;
			}
			//获取英雄对象
			Transform player = GameObject.FindGameObjectWithTag ("Player").transform;
            //获取英雄对象下面的HeroManager
            HeroManager heroManager = player.GetComponent<HeroManager> ();
            //获取HeroManager的属性
            Property property = heroManager.property;

			foreach (KeyValuePair<string, string> dict in skill.addlData)
           	{
				//截取字符串，获得属性增加的值
				float createValue = float.Parse(dict.Value);
				//动态获取当前的属性值
				float propertyValue = float.Parse(PropertyUtil.ReflectGetter(property, dict.Key).ToString());
                Debug.Log(float.Parse(dict.Value));
                //使用强化技能时间开始时，应该加上强化属性
                if (isEnd == false)
                {
                    PropertyUtil.ReflectSetter(property, dict.Key, propertyValue + createValue);
                }
                //使用强化技能时间结束后，应该减去强化属性
                else
                {
                    PropertyUtil.ReflectSetter(property, dict.Key, propertyValue - createValue);
                }
           	}
		}
	}
}
