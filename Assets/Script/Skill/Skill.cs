using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SkillClass
{
    public class Skill
    {
        //技能id
        public string id;
        //名字
        public string name;
        //是否主动技能
        public bool isActive;
        //技能图片
        public Sprite imageSprite;
        //技能类别
        public SkillCategory category;
        //技能类型
        public SkillType type;
        //技能影响范围
        public SkillActionRange actionRange;
        //技能释放状态
        public SkillReleaseState releaseState;

        public SkillRank rank;
        //技能描述
        public string description
        {
            get
            {
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
        //持续时间
        public float duration;

        public AttackAnimation attackAnimation;

        public Skill(string _id)
        {
            id = _id;
            data = DataManager.Instance.skillDatas.getSkillData(_id);

            //category = EnumTool.GetEnum<SkillCategory>(id.Substring(0, 1));
            type = EnumTool.GetEnum<SkillType>(id.Substring(0, 1));

            imageSprite = Resources.Load("Image/Skill/skill_" + id, typeof(Sprite)) as Sprite;
            isActive = (data["isActive"] == "1");

            rank = EnumTool.GetEnum<SkillRank>(data["rank"]);

            if (data.ContainsKey("duration"))
            {
                duration = float.Parse(data["duration"]);
            }

            InitTypePropert();
        }

        void InitTypePropert()
        {
            switch (type)
            {
                case SkillType.attack:
                    {
                        actionRange = EnumTool.GetEnum<SkillActionRange>(data["actionRange"]);
                        attackAnimation = EnumTool.GetEnum<AttackAnimation>(data["animation"]);
                    }
                    break;
                case SkillType.defense:
                    {
                        actionRange = SkillActionRange.self;
                    }
                    break;
                case SkillType.treatment:
                    {
                        actionRange = SkillActionRange.self;
                    }
                    break;
                case SkillType.intensify:
                    {
                        actionRange = SkillActionRange.self;
                    }
                    break;
                case SkillType.complex:
                    {

                    }
                    break;
                case SkillType.specialty:
                    {
                        actionRange = SkillActionRange.line;
                        attackAnimation = EnumTool.GetEnum<AttackAnimation>(data["animation"]);
                    }
                    break;
            }
        }
    }
}

