using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace SkillClass
{
	public class Skill
	{
		//技能id
		public string id;
        //是否主动技能
        public bool isActive;
        //技能图片
        public Sprite imageSprite;
		//技能类别
		public SkillCategory category;
        //技能类别的中文名称
        public string categoryName;
		//技能类型
		public SkillType type;
        //技能类型的中文名称
        public string typeName;
        //技能描述
        public string description;
		//通用技能数据
		public Dictionary<string, string> data = new Dictionary<string, string>();
		//特定技能数据
		public Dictionary<string, string> addlData = new Dictionary<string, string>();
		//技能是否进入冷却
		public bool isCooldown = false;
		//技能当前的冷却时间
		public float currentCoolDown = 0;
		//技能是否在持续中
		public bool isInDuration = false;
        //当前技能等级
        public int currentLevel = 1;

        public float second = Time.time;

		public Skill(string _id)
		{
            id = _id;

			int _category = int.Parse(id.Substring (0, 1));
			int _type = int.Parse(id.Substring (1, 1));

            loadCategoryProperty(_category);
            loadTypeProperty(_type);

			imageSprite = Resources.Load("Image/Skill/skill_" + id, typeof(Sprite)) as Sprite;
            isActive = (data["isActive"] == "1");

            Property property = new Property();
            int i = 0;
            description = data["description"];
            foreach (KeyValuePair<string, string> dict in addlData)
            {
                if (PropertyUtil.isExist(property, dict.Key))
                {
                    string descriptionName = PropertyUtil.ReflectDescription(property, dict.Key);
                    string s = "@" + i;
                    description = description.Replace(s, descriptionName);
                    i++;
                }
            }

            Debug.Log(data["name"] + "=" + description);
		}

        /// <summary>
        /// 加载技能的类别属性
        /// </summary>
        /// <param name="_category">Category.</param>
		void loadCategoryProperty(int _category)
		{
			//根据类别查找技能
			string categoryFileName = "";
            switch (_category) {
    			case (int)SkillCategory.Bloodline:
    				{
                        category = SkillCategory.Bloodline;
                        categoryName = "血统";
                        categoryFileName = "skill_bloodline.csv";
    				}
    				break;
    			case (int)SkillCategory.Careers:
    				{
                        category = SkillCategory.Careers;
                        categoryName = "职业";
                        categoryFileName = "skill_careers.csv";
    				}
    				break;
    			case (int)SkillCategory.Weapon:
    				{
                        category = SkillCategory.Weapon;
                        categoryName = "武器";
                        categoryFileName = "skill_weapon.csv";
    				}
    				break;
    			case (int)SkillCategory.Item:
    				{
                        category = SkillCategory.Item;
                        categoryName = "物品";
                        categoryFileName = "skill_item.csv";
    				}
    				break;
    			case (int)SkillCategory.MartialArt:
    				{
    					category = SkillCategory.MartialArt;
                        categoryName = "武学";
    					categoryFileName = "skill_martialArt.csv";
    				}
    				break;
    			case (int)SkillCategory.Mission:
    				{
                        category = SkillCategory.Mission;
                        categoryName = "任务";
                        categoryFileName = "skill_mission.csv";
    				}
    				break;

    			case (int)SkillCategory.Achievement:
    				{
                        category = SkillCategory.Achievement;
                        categoryName = "成就";
                        categoryFileName = "skill_achievement.csv";
    				}
    				break;

    			}

			//csv文件的第一行数据为属性数据
			List<string> categoryProperty = new List<string> ();
			//csv文件的列表数据
			List<string> categoryPropertyValue = new List<string> ();

            List<List<string>> basicData = CSV.Instance.loadFile (Application.dataPath + "/Resources/Data/Skill/Category", categoryFileName);
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

			//把类别数据装载到skill类的data里面
			for (int i = 0; i < categoryProperty.Count; i++) {
				data.Add (categoryProperty[i], categoryPropertyValue[i]);
			}
		}


        /// <summary>
        /// 加载技能的类型属性
        /// </summary>
        /// <param name="_type">Type.</param>
		void loadTypeProperty(int _type)
		{
			//根据类别查找技能
			string fileName = "";
            switch (_type) {
    			case (int)SkillType.Attack:
    				{
                        type = SkillType.Attack;
                        typeName = "伤害";
                        fileName = "skill_attack.csv";
    				}
    				break;
    			case (int)SkillType.Defense:
    				{
                        type = SkillType.Defense;
                        typeName = "防御";
                        fileName = "skill_defense.csv";
    				}
    				break;
    			case (int)SkillType.Treatment:
    				{
                        type = SkillType.Treatment;
                        typeName = "治疗";
                        fileName = "skill_treatment.csv";
    				}
    				break;
    			case (int)SkillType.Intensify:
    				{
    					type = SkillType.Intensify;
                        typeName = "强化";
    					fileName = "skill_intensify.csv";
    				}
    				break;
    			case (int)SkillType.Complex:
    				{
                        type = SkillType.Complex;
                        typeName = "复合";
                        fileName = "skill_complex.csv";
    				}
    				break;
    			case (int)SkillType.Specialty:
    				{
                        type = SkillType.Specialty;
                        typeName = "特殊";
                        fileName = "skill_specialty.csv";
    				}
    				break;

    		}

			//csv文件的第一行数据为属性数据
			List<string> property = new List<string> ();
			//csv文件的列表数据
			List<string> propertyValue = new List<string> ();

			List<List<string>> basicData = CSV.Instance.loadFile (Application.dataPath + "/Resources/Data/Skill/Type", fileName);
			for (int i = 0; i < basicData.Count; i++) {
				if (i == 0) {
					property = basicData [i];
				} else {
					if (basicData [i] [0] == id) {
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

public enum SkillCategory
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

public enum SkillType
{
	//伤害
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


