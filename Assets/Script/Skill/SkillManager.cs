using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Skill.Collections;

namespace Skill.Collections
{
	public class SkillManager : MonoBehaviour {
		public Button skill_btn_1;
		public Button skill_btn_2;
		public Button skill_btn_3;
		public Button skill_btn_4;
		public Button skill_btn_5;
		public List<Skill> skills;

		// Use this for initialization
		void Start () {
			Skill skill = new Skill ("540001");
			skill_btn_1.onClick.AddListener (delegate() {  
				this.useSkill(0);   
			});

			skill_btn_2.onClick.AddListener (delegate() {  
				this.useSkill(1);   
			});

			skill_btn_3.onClick.AddListener (delegate() {  
				this.useSkill(2);   
			});

			skill_btn_4.onClick.AddListener (delegate() {  
				this.useSkill(3);   
			});

			skill_btn_5.onClick.AddListener (delegate() {  
				this.useSkill(4);   
			});

			skills.Add(skill);
			Debug.Log (skill.data ["name"]);
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		void useSkill(int i)
		{
			if (skills[i] == null)
			{
				return;
			}

			Skill skill = skills[i];

			switch (skill.type) {
			case (int)SkillType.Attack:
				{
					
				}
			break;
			case (int)SkillType.Defense:
				{
				}
				break;
			case (int)SkillType.Treatment:
				{
				}
				break;
			case (int)SkillType.Intensify:
				{
					intensify(skill);
				}
				break;
			case (int)SkillType.Complex:
				{

				}
				break;
			case (int)SkillType.Specialty:
				{
				}
				break;

			}
		}

		void intensify(Skill skill)
		{
			//获取英雄对象
			Transform player = GameObject.FindGameObjectWithTag ("Player").transform;
			//获取英雄对象下面的heroSystem
			HeroSystem heroSystem = player.GetComponent<HeroSystem> ();
			//获取heroSystem的属性
			Property property = heroSystem.property;

			Dictionary<string, string> skillData = skill.skillData;

			foreach (KeyValuePair<string, string> dict in skillData)
           	{
				//截取字符串，获得+/-符号
				string symbol = dict.value.Substring (0, 1);
				//截取字符串，获得属性增加的值
				int createValue = int.Parse(dict.value.Substring (1, dict.value.length - 1));
				//动态获取当前的属性值
            	float propertyValue = PropertyUtil.ReflectGetter(property, dict.key);

				if(symbol == "+")
				{
					PropertyUtil.ReflectSetter(property, dict.key, propertyValue + dict.value);
				}
				else if(symbol == "-"){
					PropertyUtil.ReflectSetter(property, dict.key, propertyValue - dict.value);
				}
				else
				{
					Debug.Log("技能属性的值没有写上+或者-")
				}
           	}
		}
	}
}
