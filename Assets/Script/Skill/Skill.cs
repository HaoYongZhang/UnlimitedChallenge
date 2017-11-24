using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
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
        public string description
        {
            get{
                return Description.GetDescription(this);
            }
        }
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
        //每秒实际CD间隔时间
        public float second = Time.time;

        public Skill(string _id)
        {
            id = _id;

            category = PropertyUtil.GetEnum<SkillCategory>(id.Substring(0, 1));
            type = PropertyUtil.GetEnum<SkillType>(id.Substring(1, 1));

            categoryName = PropertyUtil.GetEnumDescription(category);
            typeName = PropertyUtil.GetEnumDescription(type);

            loadCategoryProperty();
            loadTypeProperty();
            //loadData();

            imageSprite = Resources.Load("Image/Skill/skill_" + id, typeof(Sprite)) as Sprite;
            isActive = (data["isActive"] == "1");
        }

        void loadData()
        {
            
        }

        /// <summary>
        /// 加载技能的类别属性
        /// </summary>
        void loadCategoryProperty()
        {
            //查找技能的csv文件
            string fileName = "skill_" + category.ToString() + ".csv";
            //csv文件的第一行数据为属性数据
            List<string> property = new List<string> ();
            //csv文件的列表数据
            List<string> propertyValue = new List<string> ();

            List<List<string>> basicData = CSV.Instance.loadFile (Application.dataPath + "/Resources/Data/Skill/Category", fileName);
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
                data.Add (property[i], propertyValue[i]);
            }
        }


        /// <summary>
        /// 加载技能的类型属性
        /// </summary>
        void loadTypeProperty()
        {
            //查找技能的csv文件
            string fileName = "skill_" + type.ToString() + ".csv";
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