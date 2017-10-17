using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skill.Collections
{
	public class Skill
	{
		//技能id
		public string id;
		//技能类别
		public SkillCategory category;
		//技能类型
		public SkillType type;
		//通用技能数据
		public Dictionary<string, string> data = new Dictionary<string, string>();
		//特定技能数据
		public Dictionary<string, string> addlData = new Dictionary<string, string>();
		//技能是否冷却了
		public bool isCooldown = true;
		//技能是否持续中
		public bool 
		//技能当前的冷却时间
		public float currentCoolDown = 0;
		//技能是否在持续中
		public float isInDuration = false;

		public Skill(string id)
		{
			this.id = id;

			int category = int.Parse(id.Substring (0, 1));
			int type = int.Parse(id.Substring (1, 1));

			loadCategoryProperty(category);
			loadTypeProperty(type);

		}

		void loadCategoryProperty(int category)
		{
			//根据类别查找技能
			string categoryFileName = "";
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
					categoryFileName = "skill_martialArt.csv";
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

			//csv文件的第一行数据为属性数据
			List<string> categoryProperty = new List<string> ();
			//csv文件的列表数据
			List<string> categoryPropertyValue = new List<string> ();

			List<List<string>> basicData = CSV.Instance.loadFile (Application.dataPath + "/Resources/Data/Skill", categoryFileName);
			for (int i = 0; i < basicData.Count; i++) {
				if (i == 0) {
					categoryProperty = basicData [i];
				} else {
					if (basicData [i] [0] == this.id) {
						categoryPropertyValue = basicData [i];
						break;
					}
				}
			}

			//把类别数据装载到skill类的data里面
			for (int i = 0; i < categoryProperty.Count; i++) {
				data.Add (categoryProperty[i], categoryPropertyValue[i]);
			}
		}

		void loadTypeProperty(int type)
		{
			//根据类别查找技能
			string fileName = "";
			switch (type) {
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
					this.type = SkillType.Intensify;
					fileName = "skill_intensify.csv";
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

			//csv文件的第一行数据为属性数据
			List<string> property = new List<string> ();
			//csv文件的列表数据
			List<string> propertyValue = new List<string> ();

			List<List<string>> basicData = CSV.Instance.loadFile (Application.dataPath + "/Resources/Data/Skill", fileName);
			for (int i = 0; i < basicData.Count; i++) {
				if (i == 0) {
					property = basicData [i];
				} else {
					if (basicData [i] [0] == this.id) {
						propertyValue = basicData [i];
						break;
					}
				}
			}

			//把类别数据装载到skill类的data里面
			for (int i = 0; i < property.Count; i++) {
				if(propertyValue[i] != "" && property[i] != "id")
				{
					addlData.Add (property[i], propertyValue[i]);
				}
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


