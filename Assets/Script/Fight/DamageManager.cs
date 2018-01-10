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
        PropertyManager attackerProperty = attacker.GetComponent<PropertyManager>();
        PropertyManager victimProperty = victim.GetComponent<PropertyManager>();

        //判断是否闪避了
        if(IsDodge(victimProperty))
        {
            Debug.Log("闪避了");
        }
        else
        {
            //抗性减免
            float damageReduction = GetDamageReduction(victimProperty, damageType);
            //伤害比率
            float damageRate = 1 - damageReduction;

            //伤害波动范围为80% ~ 120%
            float damageRange = RandomTool.RandomNumber(damageRangeMin, damageRangeMax);

            //攻击伤害
            float damage = attackerProperty.Attack * damageRate * damageRange;

            //如果暴击了，伤害增加1.5f
            if (IsCrticalStrike(attackerProperty))
            {
                damage = damage * 1.5f;
            }

            victimProperty.Hp -= MathTool.Round(damage, 1);
        }

    }


    public static void SkillAttack(GameObject attacker, GameObject victim, Skill skill)
    {
        PropertyManager attackerProperty = attacker.GetComponent<PropertyManager>();
        PropertyManager victimProperty = victim.GetComponent<PropertyManager>();

        //判断是否闪避了
        if (IsDodge(victimProperty))
        {
            Debug.Log("闪避了");
        }
        else
        {
            DamageType damageType = EnumTool.GetEnum<DamageType>(skill.data["damageType"]);

            float basicDamage = float.Parse(skill.data["basicDamage"]);
            float strengthDamage = float.Parse(skill.data["strength"]) * attackerProperty.Strength;
            float agilityDamage = float.Parse(skill.data["agility"]) * attackerProperty.Agility;
            float intellectDamage = float.Parse(skill.data["intellect"]) * attackerProperty.Intellect;

            //攻击伤害
            float damage = basicDamage + strengthDamage + agilityDamage + intellectDamage;
            //抗性减免
            float damageReduction = GetDamageReduction(victimProperty, damageType);
            //伤害比率
            float damageRate = 1 - damageReduction;
            //伤害波动范围为80% ~ 120%
            float damageRange = RandomTool.RandomNumber(damageRangeMin, damageRangeMax);

            victimProperty.Hp -= MathTool.Round(damage * damageRate * damageRange, 1);
        }
    }

    static float GetDamageReduction(PropertyManager victimProperty, DamageType damageType)
    {
        float damageReduction = 0f;

        switch (damageType)
        {
            case DamageType.physics:
                {
                    //物理抗性
                    damageReduction = victimProperty.PhysicalReduction;
                }
                break;
            case DamageType.magic:
                {
                    //法术抗性
                    damageReduction = victimProperty.MagicReduction;
                }
                break;
        }

        return damageReduction;
    }

    static bool IsDodge(PropertyManager victimProperty)
    {
        float randomNumber = RandomTool.RandomNumber(0, 1);

        if(victimProperty.Dodge < randomNumber)
        {
            return true;
        }

        return false;
    }

    static bool IsCrticalStrike(PropertyManager attackerProperty)
    {
        float randomNumber = RandomTool.RandomNumber(0, 1);

        if (attackerProperty.CrticalStrike < randomNumber)
        {
            return true;
        }

        return false;
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