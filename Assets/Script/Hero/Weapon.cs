using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Utility;
using SkillClass;

public class Weapon
{
    //武器id
    public string id;
    //武器类型
    public WeaponType type;
    //武器类型名称
    public string typeName;
    //武器属性
    public Property property;
    //武器图片
    public Sprite imageSprite;
    //武器技能
    public Skill skill;
    //武器数据
    public Dictionary<string, string> data = new Dictionary<string, string>();

    public float attack;

    public Weapon(string _id)
    {
        id = _id;

        type = PropertyUtil.GetEnum<WeaponType>(id.Substring(0, 1));

        typeName = PropertyUtil.GetEnumDescription(type);

        loadWeaponData();

        imageSprite = Resources.Load("Image/Weapon/weapon_" + id, typeof(Sprite)) as Sprite;
    }


    void loadWeaponData()
    {
        //查找技能的csv文件
        string fileName = "weapon_" + type.ToString() + ".csv";

        List<List<string>> csvData = CSV.Instance.loadFile(Application.dataPath + "/Resources/Data/Weapon", fileName);
        //csv文件的第一行数据为属性数据
        List<string> propertyKey = new List<string>();
        //csv文件的列表数据
        List<string> propertyValue = new List<string>();

        for (int i = 0; i < csvData.Count; i++)
        {
            if (i == 0)
            {
                propertyKey = csvData[i];
            }
            else
            {
                if (csvData[i][0] == id)
                {
                    propertyValue = csvData[i];
                    break;
                }
            }
        }

        //把类别数据装载到skill类的data里面
        for (int i = 0; i < propertyKey.Count; i++)
        {
            data.Add(propertyKey[i], propertyValue[i]);
        }
    }
}
