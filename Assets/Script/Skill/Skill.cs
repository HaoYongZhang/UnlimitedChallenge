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
        //技能类型
        public SkillType type;
        //技能影响范围
        public SkillEffectRange effectRange;
        //技能释放状态
        public SkillReleaseState releaseState;
        //技能描述
        public string description
        {
            get{
                return Description.GetDescription(this);
            }
        }
        //技能数据
        public Dictionary<string, string> data = new Dictionary<string, string>();

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
            data = DataManager.Instance.skillDatas.getSkillData(_id);

            category = PropertyUtil.GetEnum<SkillCategory>(id.Substring(0, 1));
            type = PropertyUtil.GetEnum<SkillType>(id.Substring(1, 1));

            if(type == SkillType.intensify || type == SkillType.defense || type == SkillType.treatment)
            {
                effectRange = SkillEffectRange.self;
            }
            else
            {
                effectRange = PropertyUtil.GetEnum<SkillEffectRange>(data["skillEffectRange"]);
            }


            imageSprite = Resources.Load("Image/Skill/skill_" + id, typeof(Sprite)) as Sprite;
            isActive = (data["isActive"] == "1");
        }
    }
}