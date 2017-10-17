using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Skill.Collections;
using Utility;

namespace Skill.Collections
{
	public class SkillManager : MonoBehaviour {
		//技能按钮集合
		private List<Button> skillButtons = new List<Button>();
		//技能集合
		public List<Skill> skills = new List<Skill>();

		void Start () {
			Skill skill = new Skill ("540001");
			skills.Add(skill);
			defaultSkillSetting ();
		}
		

		void Update () {
			cooldown();

		}

		//技能的默认设置
		void defaultSkillSetting()
		{
			GameObject skillObject = skillObject = GameObject.Find("SkillObject");
			skillButtons = new List<Button>(skillObject.GetComponentsInChildren<Button>());
			for (int i = 0; i < skillButtons.Count; i++)
			{
				Button btn = skillButtons[i];
				int j = i;

				btn.onClick.AddListener(delegate ()
				{
					this.useSkill(j);
				});
			}
		}

		//技能冷却
		void cooldown()
		{
			for (int i = 0; i < skills.Count; i++)
			{
				Skill skill = skills[i];
				if (skill.isCooldown)
				{
					if (skill.currentCoolDown < float.Parse(skill.data["cooldown"]))
					{
						// 更新冷却
						skill.currentCoolDown += Time.deltaTime;

						if (skill.isInDuration && skill.currentCoolDown >= float.Parse(skill.data["duration"]))
						{
							afterCooldown(skill);
						}

						Image maskImage = skillButtons[i].transform.Find("MaskImage").GetComponent<Image>();
						// 显示冷却动画
						maskImage.fillAmount = skill.currentCoolDown / coolDown;
					}
					else
					{
						skill.currentCoolDown = 0;
						skill.isCooldown = true;

					}
				}

			}
		}

		//技能冷却结束
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
			if (skills.Count < (i + 1))
			{
				return;
			}

			Skill skill = skills[i];

			// 如果是被动技能，返回
			if (skill.data.["isActives"] == false)
			{
				return;
			}

			// 如果技能冷却还未好，返回
			if (skill.isCooldown == false) {
				return;
			}

			HeroSystem heroSystem = GetComponent<HeroSystem> ();

			// 如果蓝量不足，返回
			if (heroSystem.property.mp < float.Parse (skill.data ["costEnergy"])) {
				Debug.Log ("能量不足");
				return;
			} else {
				heroSystem.property.mp -= float.Parse (skill.data ["costEnergy"]);
				skill.isCooldown = true;
			}

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

		/// <summary>
		/// 使用强化技能
		/// </summary>
		/// <param name="skill">Skill.</param>
		/// <param name="isEnd">强化技能时间是否结束</param>
		void intensify(Skill skill, bool isEnd)
		{
			if(isEnd)
			{
				skill.isInDuration = true;
			}
			else
			{
				skill.isInDuration = false;
			}
			//获取英雄对象
			Transform player = GameObject.FindGameObjectWithTag ("Player").transform;
			//获取英雄对象下面的heroSystem
			HeroSystem heroSystem = player.GetComponent<HeroSystem> ();
			//获取heroSystem的属性
			Property property = heroSystem.property;

			foreach (KeyValuePair<string, string> dict in skill.addlData)
           	{
				//截取字符串，获得+/-符号
				string symbol = dict.Value.Substring (0, 1);
				//截取字符串，获得属性增加的值
				int createValue = int.Parse(dict.Value.Substring (1, dict.Value.Length - 1));
				//动态获取当前的属性值
				float propertyValue = float.Parse(PropertyUtil.ReflectGetter(property, dict.Key).ToString());

				if(symbol == "+")
				{
					//使用强化技能时间开始时，应该加上强化属性
					if (isEnd)
					{
						PropertyUtil.ReflectSetter(property, dict.Key, propertyValue + float.Parse(dict.Value));
					}
					//使用强化技能时间结束后，应该减去强化属性
					else
					{
						PropertyUtil.ReflectSetter(property, dict.Key, propertyValue - float.Parse(dict.Value));
					}
				}
				else if(symbol == "-"){
					//使用强化技能时间开始时，应该减去强化属性
					if (isEnd)
					{
						PropertyUtil.ReflectSetter(property, dict.Key, propertyValue - float.Parse(dict.Value));
					}
					//使用强化技能时间结束后，应该加上强化属性
					else
					{
						PropertyUtil.ReflectSetter(property, dict.Key, propertyValue + float.Parse(dict.Value));
					}

				}
				else
				{
					Debug.Log ("技能属性的值没有写上+或者-");
				}
           	}
		}
	}
}
