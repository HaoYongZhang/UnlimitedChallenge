using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Utility;
using SkillClass;

public class Equipment {
    //装备id
    public string id;
    //装备类型
    public EquipmentPart part;
    //装备类型名称
    public string partName;
    //武器属性
    public Property property;
    //装备图片
    public Sprite imageSprite;
    //装备技能
    public Skill skill;

    public Equipment(string _id)
    {
        id = _id;

        part = getEnum<EquipmentPart>(id.Substring(0, 1));

        partName = PropertyUtil.GetEnumDescription(part);

        loadPart();
        loadWeaponType();

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

    }

    void loadWeaponType()
    {
        
    }
}
