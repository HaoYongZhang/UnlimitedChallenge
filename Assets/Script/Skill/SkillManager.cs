using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Skill.Collections;
using Utility;

namespace Skill.Collections
{
	public class SkillManager : MonoBehaviour {
		private List<Button> skillButtons = new List<Button>();
		private List<bool> isCooldowns = new List<bool>();
		public List<Skill> skills = new List<Skill>();

		void initSkillButtons()
		{
			GameObject skillObject = GameObject.Find("SkillObject");
			skillButtons = new List<Button>(skillObject.GetComponentsInChildren<Button> ());
			for (int i = 0; i < skillButtons.Count; i++) {
				Button btn = skillButtons[i];
				int j = i;

				btn.onClick.AddListener (delegate() {  
					this.useSkill(j);
				});

				isCooldowns.Add (false);
			}
		}

		// Use this for initialization
		void Start () {
			Skill skill = new Skill ("540001");
			skills.Add(skill);
			initSkillButtons ();
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		void cooldown(int i)
		{
			Skill skill = skills[i];
			InvokeRepeating ("cooldowns", 0, 1);
		}

		void cooldownTime()
		{
			Button btn = skillButtons [i];

//			Sprite.fillAmount -= (1f / TimeSpeed) * Time.deltaTime;//对图片按照时间进行360度的旋转剪切
//			Text label = btn.GetComponent<Text>();
//			label.text = (TimeSpeed * Sprite.fillAmount).ToString("f1");//改变冷却时间
//			if (Sprite.fillAmount <= 0.05f)
//			{
//				isCude = false;
//				Sprite.fillAmount = 0;
//				Lable.text = "";
//			}
		}

		void useSkill(int i)
		{
			Debug.Log ("触发");

			if (skills.Count < (i + 1))
			{
				return;
			}

			if (isCooldowns [i] == true) {
				return;
			}

			Skill skill = skills[i];

			HeroSystem heroSystem = GetComponent<HeroSystem> ();

			if (heroSystem.property.mp < float.Parse (skill.data ["energy"])) {
				Debug.Log ("能量不足");
				return;
			} else {
				heroSystem.property.mp -= float.Parse (skill.data ["energy"]);
				isCooldowns [i] = true;
			}

			cooldown (i);

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
					intensify(skill);
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
				string symbol = dict.Value.Substring (0, 1);
				//截取字符串，获得属性增加的值
				int createValue = int.Parse(dict.Value.Substring (1, dict.Value.Length - 1));
				//动态获取当前的属性值
				float propertyValue = float.Parse(PropertyUtil.ReflectGetter(property, dict.Key).ToString());

				if(symbol == "+")
				{
					PropertyUtil.ReflectSetter(property, dict.Key, propertyValue + float.Parse(dict.Value));
				}
				else if(symbol == "-"){
					PropertyUtil.ReflectSetter(property, dict.Key, propertyValue - float.Parse(dict.Value));
				}
				else
				{
					Debug.Log ("技能属性的值没有写上+或者-");
				}
           	}
		}
	}
}
