using System;
using System.Collections.Generic;
using System.ComponentModel;
namespace SkillClass
{
    public class SkillEnum
    {
        public static T getEnum<T>(string enumValue)
        {
            T enumObj = (T)Enum.Parse(typeof(T), enumValue);

            return enumObj;
        }
    }
    /// <summary>
    /// 技能的类别
    /// </summary>
    public enum SkillCategory
    {
        [Description("血统")]
        bloodline = 1,
        [Description("职业")]
        careers = 2,
        [Description("武器")]
        weapon = 3,
        [Description("物品")]
        item = 4,
        [Description("武术")]
        martialArt = 5,
        [Description("任务")]
        mission = 6,
        [Description("成就")]
        achievement = 7
    }

    /// <summary>
    /// 技能的类型
    /// </summary>
    public enum SkillType
    {
        [Description("伤害")]
        attack = 1,
        [Description("防御")]
        defense = 2,
        [Description("治疗")]
        treatment = 3,
        [Description("强化")]
        intensify = 4,
        [Description("复合")]
        complex = 5,
        [Description("特殊")]
        specialty = 6
    }

    /// <summary>
    /// 释放影响类型
    /// </summary>
    public enum SkillEffectType
    {
        [Description("指向性")]
        pointing,
        [Description("直线")]
        line,
        [Description("直线不穿透")]
        lineOne,
        [Description("范围性")]
        aoe
    }

    /// <summary>
    /// 伤害类型
    /// </summary>
    public enum DamageType
    {
        [Description("物理伤害")]
        physics,
        [Description("能量伤害")]
        energy
    }


}

