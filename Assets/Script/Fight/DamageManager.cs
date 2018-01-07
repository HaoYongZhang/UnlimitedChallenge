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
    public static void CommonAttack<T, V>(GameObject attacker, GameObject victim, DamageType damageType)
    {   
        Property attackerProperty = attacker.GetComponent<Property>();
        Property victimProperty = victim.GetComponent<Property>();

        float damage = attackerProperty.attack;
        float damageRate = 1;
        switch(damageType)
        {
            case DamageType.physics:
                {
                    //伤害倍率 = 1 - 物理抗性
                    damageRate = 1 - victimProperty.physicalReduction;
                }
                break;
            case DamageType.magic:
                {
                    //伤害倍率 = 1 - 法术抗性
                    damageRate = 1 - victimProperty.magicReduction;
                }
                break;
        }

        //伤害波动范围为80% ~ 120%
        float damageRange = RandomTool.RandomNumber(damageRangeMin, damageRangeMax);

        victimProperty.hp -= MathTool.Round(damage * damageRate * damageRange, 1);
    }


    public static void SkillAttack<T, V>(GameObject attacker, GameObject victim, Skill skill)
    {
        //Property attackerProperty = (Property)PropertyTool.ReflectGetter(attacker.GetComponent<T>(), "property");
        //Property victimProperty = (Property)PropertyTool.ReflectGetter(victim.GetComponent<V>(), "property");

        Property attackerProperty = attacker.GetComponent<Property>();
        Property victimProperty = victim.GetComponent<Property>();


        DamageType damageType = EnumTool.GetEnum<DamageType>(skill.data["damageType"]);
        Debug.Log(skill.id);

        float basicDamage = float.Parse(skill.data["basicDamage"]);
        float strengthDamage = float.Parse(skill.data["strength"]) * attackerProperty.strength;
        float agilityDamage = float.Parse(skill.data["agility"]) * attackerProperty.agility;
        float intellectDamage = float.Parse(skill.data["intellect"]) * attackerProperty.intellect;

        float damage = basicDamage + strengthDamage + agilityDamage + intellectDamage;
        float damageRate = 1f;
        switch (damageType)
        {
            case DamageType.physics:
                {
                    //伤害倍率 = 1 - 物理抗性
                    damageRate = 1 - victimProperty.physicalReduction;
                }
                break;
            case DamageType.magic:
                {
                    //伤害倍率 = 1 - 法术抗性
                    damageRate = 1 - victimProperty.magicReduction;
                }
                break;
        }

        //伤害波动范围为80% ~ 120%
        float damageRange = RandomTool.RandomNumber(damageRangeMin, damageRangeMax);

        victimProperty.hp -= MathTool.Round(damage * damageRate * damageRange, 1);

        Debug.Log("魔抗=" + victimProperty.magicReduction);
        Debug.Log("伤害=" + damage);
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