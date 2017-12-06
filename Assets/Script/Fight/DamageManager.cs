using UnityEngine;
using System.Collections;
using Utility;
using System;
using System.ComponentModel;

public delegate void AttackDamageDelegate();

public class DamageManager
{
    /// <summary>
    /// 普通攻击
    /// </summary>
    /// <param name="attacker">Attacker.</param>
    /// <param name="victim">Victim.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    /// <typeparam name="V">The 2nd type parameter.</typeparam>
    public static void CommonAttack<T, V>(GameObject attacker, GameObject victim, DamageType damageType)
    {   
        Property attackerProperty = (Property)PropertyUtil.ReflectGetter(attacker.GetComponent<T>(), "property");
        Property victimProperty = (Property)PropertyUtil.ReflectGetter(victim.GetComponent<V>(), "property");

        float damage = 0;
        switch(damageType)
        {
            case DamageType.physics:
                {
                    //护甲的伤害减免（0.06*护甲/(1+0.06*护甲)）
                    float damageReduction = 0.06f * victimProperty.armor / (1 + 0.06f * victimProperty.armor);
                    //伤害倍率
                    float damageRate = 1 - damageReduction;
                    //实际伤害
                    damage = Utility.Math.Round(attackerProperty.attack * damageRate, 1);

                    //Debug.Log("护甲的伤害减免=" + damageReduction);
                    //Debug.Log("伤害倍率=" + damageRate);
                    //Debug.Log("实际伤害=" + damage);
                }
                break;
        }

        victimProperty.hp -= damage;
    }
}


/// <summary>
/// 伤害类型
/// </summary>
public enum DamageType
{
    [Description("物理")]
    physics,
    [Description("能量")]
    energy,
    [Description("纯粹")]
    pure
}