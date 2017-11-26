using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;

namespace EquipmentClass
{
    /// <summary>
    /// 装备部位
    /// </summary>
    public enum EquipmentPart
    {
        [Description("不存在")]
        nothing,
        [Description("武器")]
        weapon,
        [Description("头")]
        head,
        [Description("上身")]
        body,
        [Description("下身")]
        legs,
        [Description("宝物")]
        treasure
    }

    /// <summary>
    /// 武器类型
    /// </summary>
    public enum WeaponType
    {
        [Description("拳爪")]
        hands,
        [Description("刀剑")]
        sword,
        [Description("枪棍")]
        stick,
        [Description("锤")]
        hammer,
        [Description("轻型手枪")]
        pistol,
        [Description("中型步枪")]
        gun
    }

    /// <summary>
    /// 伤害修正
    /// </summary>
    public enum DamageCorrect
    {
        [Description("不存在")]
        nothing,
        [Description("非武器")]
        none,
        [Description("剑")]
        sword,
        [Description("刀")]
        knife,
        [Description("枪")]
        spear,
        [Description("棍")]
        stick,
        [Description("机械手枪")]
        pistol,
        [Description("机械枪")]
        gun
    }

    /// <summary>
    /// 伤害类型
    /// </summary>
    public enum DamageType
    {
        [Description("物理")]
        physics,
        [Description("内力")]
        Force,
        [Description("魔法")]
        Magic,
        [Description("能量")]
        other
    }
}