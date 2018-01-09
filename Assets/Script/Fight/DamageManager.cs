using UnityEngine;
using System.Collections;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using SkillClass;

public delegate void AttackDamageDelegate();

public class DamageManager
{
    public static float damageRangeMin = 0.8f;
    public static float damageRangeMax = 1.2f;

    /// <summary>
    /// 普通攻击
    /// </summary>
    /// <param name="attacker">Attacker.</param>
    /// <param name="victim">Victim.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    /// <typeparam name="V">The 2nd type parameter.</typeparam>
    public static void CommonAttack(GameObject attacker, GameObject victim, DamageType damageType)
    {   
        Property attackerProperty = attacker.GetComponent<Property>();
        Property victimProperty = victim.GetComponent<Property>();

        //攻击伤害
        float damage = attackerProperty.attack;
        //抗性减免
        float damageReduction = GetDamageReduction(victimProperty, damageType);
        //伤害比率
        float damageRate = 1 - damageReduction;

        //伤害波动范围为80% ~ 120%
        float damageRange = RandomTool.RandomNumber(damageRangeMin, damageRangeMax);

        victimProperty.hp -= MathTool.Round(damage * damageRate * damageRange, 1);
    }


    public static void SkillAttack(GameObject attacker, GameObject victim, Skill skill)
    {
        Property attackerProperty = attacker.GetComponent<Property>();
        Property victimProperty = victim.GetComponent<Property>();


        DamageType damageType = EnumTool.GetEnum<DamageType>(skill.data["damageType"]);

        float basicDamage = float.Parse(skill.data["basicDamage"]);
        float strengthDamage = float.Parse(skill.data["strength"]) * attackerProperty.strength;
        float agilityDamage = float.Parse(skill.data["agility"]) * attackerProperty.agility;
        float intellectDamage = float.Parse(skill.data["intellect"]) * attackerProperty.intellect;

        //攻击伤害
        float damage = basicDamage + strengthDamage + agilityDamage + intellectDamage;
        //抗性减免
        float damageReduction = GetDamageReduction(victimProperty, damageType);
        //伤害比率
        float damageRate = 1 - damageReduction;
        //伤害波动范围为80% ~ 120%
        float damageRange = RandomTool.RandomNumber(damageRangeMin, damageRangeMax);

        victimProperty.hp -= MathTool.Round(damage * damageRate * damageRange, 1);
    }

    static float GetDamageReduction(Property victimProperty, DamageType damageType)
    {
        float damageReduction = 0f;

        switch (damageType)
        {
            case DamageType.physics:
                {
                    //物理抗性
                    damageReduction = victimProperty.physicalReduction;
                }
                break;
            case DamageType.magic:
                {
                    //法术抗性
                    damageReduction = victimProperty.magicReduction;
                }
                break;
        }

        return damageReduction;
    }
}


/// <summary>
/// 伤害类型
/// </summary>
public enum DamageType
{
    [Description("物理")]
    physics,
    [Description("法术")]
    magic,
    [Description("纯粹")]
    pure
}