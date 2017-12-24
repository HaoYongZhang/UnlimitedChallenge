using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SkillClass;

namespace EquipmentClass
{
    public class Equipment
    {
        //装备id
        public string id;
        //装备部位
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

            part = EnumTool.GetEnum<EquipmentPart>(id.Substring(0, 1));

            partName = EnumTool.GetEnumDescription(part);

            data = DataManager.Instance.equipmentDatas.getEquipmentData(_id);

            imageSprite = Resources.Load("Image/Equipment/equipment_" + id, typeof(Sprite)) as Sprite;
        }

    }
}

