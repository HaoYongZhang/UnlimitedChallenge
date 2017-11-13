using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Utility;
using SkillClass;

namespace EquipmentClass
{
    public class Equipment
    {
        //装备id
        public string id;
        //装备类型
        public EquipmentPart part;
        //装备类型名称
        public string partName;
        //武器属性
        public Property property = new Property();
        //装备图片
        public Sprite imageSprite;
        //装备技能
        public Skill skill;
        //装备数据
        public Dictionary<string, string> data = new Dictionary<string, string>();
        //是否穿着
        public bool isWear;

        public Equipment(string _id)
        {
            id = _id;

            part = getEnum<EquipmentPart>(id.Substring(0, 1));

            partName = PropertyUtil.GetEnumDescription(part);

            loadPart();

            imageSprite = Resources.Load("Image/Equipment/equipment_" + id, typeof(Sprite)) as Sprite;
        }

        T getEnum<T>(string enumValue)
        {
            T enumObj = (T)Enum.Parse(typeof(T), enumValue);

            return enumObj;
        }

        void loadPart()
        {
            //查找技能的csv文件
            string fileName = "equipment_" + part.ToString() + ".csv";

            //csv文件的第一行数据为属性数据
            List<string> keyList = new List<string>();
            //csv文件的列表数据
            List<string> valueList = new List<string>();

            List<List<string>> csvData = CSV.Instance.loadFile(Application.dataPath + "/Resources/Data/Equipment", fileName);
            for (int i = 0; i < csvData.Count; i++)
            {
                if (i == 0)
                {
                    keyList = csvData[i];
                }
                else
                {
                    if (csvData[i][0] == id)
                    {
                        valueList = csvData[i];
                        break;
                    }
                }
            }

            Dictionary<string, string> dataDict = new Dictionary<string, string>();

            //把类别数据装载到skill类的data里面
            for (int i = 0; i < keyList.Count; i++)
            {
                dataDict.Add(keyList[i], valueList[i]);
            }

            foreach (KeyValuePair<string, string> dict in dataDict)
            {
                if (PropertyUtil.isExist(property, dict.Key))
                {
                    if(dict.Value !=  "")
                    {
                        PropertyUtil.ReflectSetter(property, dict.Key, float.Parse(dict.Value));
                    }

                }
                else
                {
                    data.Add(dict.Key, dict.Value);
                }
            }
        }

    }
}

