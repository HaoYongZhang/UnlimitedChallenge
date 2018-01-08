using System;
using System.Collections.Generic;
using System.ComponentModel;
namespace SkillClass
{
    /// <summary>
    /// 技能的释放状态
    /// </summary>
    public enum SkillReleaseState
    {
        //可使用
        available,
        //选择中
        selecting,
        //已选择
        selected,
        //冷却中
        cooldown
    }

    /// <summary>
    /// 技能的类别
    /// </summary>
    public enum SkillCategory
    {
        [Description("血统")]
        bloodline = 1,
        [Description("内功")]
        careers = 2,
        [Description("武学")]
        martialArt = 3,
        [Description("科技")]
        science = 4,
        [Description("装备")]
        weapon = 5,
        [Description("物品")]
        item = 6,
        [Description("任务")]
        mission = 7,
        [Description("成就")]
        achievement = 8
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
    /// 技能释放影响范围
    /// </summary>
    public enum SkillEffectRange
    {
        [Description("自身")]
        self,
        [Description("指向性")]
        pointing,
        [Description("直线")]
        line,
        [Description("直线不穿透")]
        lineOne,
        [Description("范围性")]
        aoe
    }

    public enum SpecialtyAction
    {
        [Description("自定义")]
        custom,
        [Description("瞬间移动")]
        telesport
    }

    public enum SkillRank
    {
        S,
        A,
        B,
        C,
        D
    }
}

