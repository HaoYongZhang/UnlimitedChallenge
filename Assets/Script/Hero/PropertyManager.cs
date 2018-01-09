using UnityEngine;
using System;
using System.Reflection;
using System.ComponentModel;

public class PropertyManager : MonoBehaviour
{
    public Property basicProperty = new Property();
    public Property rateProperty = new Property();


    void Awake()
    {
        InitRateProperty(rateProperty);
    }

    void Start()
    {
        InvokeRepeating("RegenerationPerSecond", 0, 1f);   
    }

    void InitRateProperty(Property propertyRate)
    {
        //得到类型
        Type type = typeof(Property);
        //取得属性集合
        PropertyInfo[] pi = type.GetProperties();

        foreach (PropertyInfo item in pi)
        {
            item.SetValue(propertyRate, 1f, null);
        }
    }

    /// <summary>
    /// 每秒恢复生命值和能量
    /// </summary>
    void RegenerationPerSecond()
    {
        float hp = Hp;
        float hpMax = HpMax;
        float mp = Mp;
        float mpMax = MpMax;
        //当生命值不是最大值时
        if (hp <= hpMax)
        {
            float afterRegeneration = MathTool.Round(hp + HpRegeneration, 1);
            //当回复生命值后将会溢出最大值时
            if (afterRegeneration > hpMax)
            {
                Hp = hpMax;
            }
            else
            {
                Hp = MathTool.Round(hp + HpRegeneration, 1);
            }
        }

        //当能量值不是最大值时
        if (mp <= mpMax)
        {
            //当回复能量值后将会溢出最大值时
            if (MathTool.Round(mp + MpRegeneration, 1) > mpMax)
            {
                Mp = MpMax;
            }
            else
            {
                Mp = MathTool.Round(mp + MpRegeneration, 1);
            }
        }
    }



    //------综合的属性
    //生命值
    public float Hp;
    //能量值
    public float Mp;

    //力量
    public float Strength
    {
        get
        {
            return basicProperty.Strength * rateProperty.Strength;
        }
    }
    //敏捷
    public float Agility
    {
        get
        {
            return basicProperty.Agility * rateProperty.Agility;
        }
    }
    //智力
    public float Intellect
    {
        get
        {
            return basicProperty.Intellect * rateProperty.Intellect;
        }
    }

    //最大生命值
    public float HpMax
    {
        get
        {
            //最大生命值 = （基础最大生命值 + 力量最大生命值（力量*20）） * 比率
            return (basicProperty.HpMax + Strength * 20f) * rateProperty.HpMax;
        }
    }
    //最大能量值
    public float MpMax
    {
        get
        {
            //最大能量值 = 基础最大魔法值 + 智力最大魔法值（智力*11） + 额外最大魔法值 * 比率
            return (basicProperty.MpMax + Intellect * 11f) * rateProperty.HpMax;
        }
    }

    //生命恢复速度
    public float HpRegeneration
    {
        get
        {
            // 生命恢复速度 = 基础生命恢复 + 力量生命恢复（力量*0.06）
            return (basicProperty.HpRegeneration + Strength * 0.06f) * rateProperty.HpRegeneration;
        }
    }

    //能量恢复速度
    public float MpRegeneration
    {
        get
        {
            //能量恢复速度 = 基础能量恢复 + 智力能量恢复（智力 * 0.04）
            return (basicProperty.MpRegeneration + Intellect * 0.04f) * rateProperty.MpRegeneration;
        }
    }

    //护甲
    public float Armor
    {
        get
        {
            //护甲 = 基础护甲 + 敏捷护甲（敏捷*0.143） + 额外护甲
            return (basicProperty.Armor + Agility * 0.143f) * rateProperty.Armor;
        }
    }

    //移动速度
    public float MoveSpeed
    {
        get
        {
            //移动速度 = 基础移动速度 * （移动速度倍率 + （敏捷 * 0.06%））
            return basicProperty.MoveSpeed * (rateProperty.MoveSpeed + Agility * 0.0006f);
        }
    }

    //攻击力
    public float Attack
    {
        get
        {
            return basicProperty.Attack * rateProperty.Attack;
        }
    }

    //攻击速度
    public float AttackSpeed
    {
        get
        {
            //攻击速度 = 基础攻击速度 + 敏捷速度（敏捷*1）
            return (basicProperty.AttackSpeed + Agility) * rateProperty.AttackSpeed;
        }
    }

    //攻击间隔
    //public float AttackTime
    //{
    //    get
    //    {
    //        //攻击间隔 = 基础攻击间隔 ÷ 攻击速度%
    //        return basAttackTime / (AttackSpeed / 100);
    //    }
    //}


    //物理抗性
    public float PhysicalReduction
    {
        get
        {
            //护甲的伤害减免（0.06 * 护甲/(1 + 0.06 * 护甲)）
            float armorReduction = 0.06f * Armor / (1 + 0.06f * Armor);
            //物理伤害倍率（1 - 护甲伤害减免）*（1 - 基础物理伤害减免）*（1 - 额外物理伤害减免）
            float damageRate = (1 - armorReduction) * (1 - basicProperty.PhysicalReduction);

            return (1 - damageRate);
        }
    }
    //法术抗性
    public float MagicReduction
    {
        get
        {
            //智力的伤害减免（0.06 * 护甲/(1 + 0.06 * 护甲)）
            float intellectReduction = 0.03f * Intellect / (1 + 0.03f * Intellect);
            //法术伤害倍率（1 - 智力伤害减免）*（1 - 基础法术伤害减免）*（1 - 额外法术伤害减免）
            float damageRate = (1 - intellectReduction) * (1 - basicProperty.MagicReduction);

            return (1 - damageRate);
        }
    }
    //物理伤害格挡
    public float physicalDamageBlock;
    //魔法伤害格挡
    public float magicDamageBlock;
}