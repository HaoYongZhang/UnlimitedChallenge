using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skill.Collections
{
	public class Skill
	{
		public SkillCategory category;

		public Dictionary<string, string> data = new Dictionary<string, string>();

		public Skill(string id)
		{
			int category = int.Parse(id.Substring (0, 1));
			int type = int.Parse(id.Substring (1, 1));



			string fileName = "";
			switch (category) {
			case (int)SkillCategory.Bloodline:
				{
					
				}
				break;
			case (int)SkillCategory.Careers:
				{
				}
				break;
			case (int)SkillCategory.Weapon:
				{
				}
				break;
			case (int)SkillCategory.Item:
				{
					
				}
				break;
			case (int)SkillCategory.MartialArt:
				{
					this.category = SkillCategory.MartialArt;
					fileName = "skill_martialArt.csv";
				}
				break;
			case (int)SkillCategory.Mission:
				{
				}
				break;

			case (int)SkillCategory.Achievement:
				{
				}
				break;

			}

			List<string> categoryProperty = new List<string> ();
			List<string> categoryPropertyValue = new List<string> ();

			List<List<string>> basicData = CSV.Instance.loadFile (Application.dataPath + "/Resources/Data/Skill", fileName);
			for (int i = 0; i < basicData.Count; i++) {
				if (i == 0) {
					categoryProperty = basicData [i];
				} else {
					if (basicData [i] [0] == id) {
						categoryPropertyValue = basicData [i];
						break;
					}
				}
			}

			for (int i = 0; i < categoryProperty.Count; i++) {
				data.Add (categoryProperty[i], categoryPropertyValue[i]);
			}
		}
	}
}

public enum SkillCategory:int
{
	//血统
	Bloodline = 1,
	//职业
	Careers = 2,
	//武器
	Weapon = 3,
	//物品
	Item = 4,
	//武术
	MartialArt = 5,
	//任务
	Mission = 6,
	//成就
	Achievement = 7
}

public enum SkillType:int
{
	//攻击
	Attack = 1,
	//防御
	Defense = 2,
	//治疗
	Treatment = 3,
	//强化
	Intensify = 4,
	//复合
	Complex = 5,
	//特殊
	Specialty = 6
}


