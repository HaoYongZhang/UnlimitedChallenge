using UnityEngine;
using System.Collections;
using Utility;
using System;
using System.ComponentModel;
using System.Collections.Generic;

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
                    //伤害倍率 = 1 - 物理抗性
                    float damageRate = 1 - victimProperty.physicalReduction;
                    //实际伤害
                    damage = Utility.Math.Round(attackerProperty.attack * damageRate, 1);

                    //Debug.Log("伤害倍率=" + damageRate);
                    //Debug.Log("实际伤害=" + damage);
                }
                break;
            case DamageType.magic:
                {
                    //伤害倍率 = 1 - 法术抗性
                    float damageRate = 1 - victimProperty.magicReduction;
                    //实际伤害
                    damage = Utility.Math.Round(attackerProperty.attack * damageRate, 1);

                    Debug.Log("伤害倍率=" + damageRate);
                    Debug.Log("实际伤害=" + damage);
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
    [Description("法术")]
    magic,
    [Description("纯粹")]
    pure
}