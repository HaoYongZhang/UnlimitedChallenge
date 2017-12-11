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
        //技能数据
        public Dictionary<string, string> data = new Dictionary<string, string>();

        //技能是否进入冷却
        public bool isCooldown;
        //技能当前的冷却时间
        public float currentCoolDown;
        //技能是否在持续中
        public bool isInDuration;
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

            data = DataManager.Instance.skillDatas.getSkillData(_id);

            imageSprite = Resources.Load("Image/Skill/skill_" + id, typeof(Sprite)) as Sprite;
            isActive = (data["isActive"] == "1");
        }

        public void release()
        {
            Global.hero.skillManager.releaseSkill(this);
        }
    }
}