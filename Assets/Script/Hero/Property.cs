using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Property {
	//------基础的（basic）
	//基础最大生命值
	public float basHpMax;
	//基础最大能量值
	public float basMpMax;
	//基础生命恢复速度
	public float basHpRegeneration;
	//基础能量恢复速度
	public float basMpRegeneration;
	//基础护甲
	public float basArmor;
	//基础移动速度
	public float basMoveSpeed;
	//基础攻击力
	public float basAttack;
	//基础攻击间隔
	public float basAttackTime;
	//基础攻击速度
	public float basAttackSpeed;

	//------额外的（additional）
	//额外最大生命值
	public float addlHpMax;
	//额外最大能量值
	public float addlMpMax;
	//额外生命恢复速度
	public float addlHpRegeneration;
	//额外能量恢复速度
	public float addlMpRegeneration;
	//额外护甲
	public float addlArmor;
	//额外移动速度
	public float addlMoveSpeed;
	//额外攻击力
	public float addlAttack;
	//额外攻击速度
	public float addlAttackSpeed;

	//------综合的属性
	//生命值
	public float hp;
	//能量值
	public float mp;

	//力量
    public float strength;
    //敏捷
    public float agility;
    //智力
    public float intellect;
    
    //最大生命值
    public float hpMax{
		get
		{
			//最大生命值 = 基础最大生命值 + 力量最大生命值（力量*20） + 额外最大生命值
			return basHpMax + strength * 20f  + addlHpMax;
		}
	}
    //最大能量值
    public float mpMax{
		get
		{
			//最大能量值 = 基础最大魔法值 + 智力最大魔法值（智力*11） + 额外最大魔法值
			return basMpMax + intellect * 11f + addlMpMax;
		}
	}
		
	/// <summary>
	/// 生命恢复速度 = (基础生命恢复 + 力量生命恢复（力量*0.06） + 额外生命恢复)
	/// </summary>
	/// <value>The hp regeneration.</value>
	public float hpRegeneration
	{
		get
      	{
			return basHpRegeneration + strength * 0.06f  + addlHpRegeneration; 
      	}
    }

	/// <summary>
	/// 能量恢复速度 = (基础能量恢复 + 智力能量恢复（智力*0.04） + 额外能量恢复)
	/// </summary>
	/// <value>The mp regeneration.</value>
	public float mpRegeneration
	{
		get
		{
			return basMpRegeneration + intellect * 0.04f + addlMpRegeneration;
		}
	}

    //护甲
    public float armor
	{
		get
		{
			//护甲 = 基础护甲 + 敏捷护甲（敏捷*0.143） + 额外护甲
			return basArmor + agility * 0.143f + addlArmor;
		}
	}

    //移动速度
    public float moveSpeed
	{
		get
		{
			return basMoveSpeed + addlMoveSpeed;
		}
	}

    //攻击力
	public float attack{
		get 
      {
         return basAttack + addlAttack; 
      }
    }

	//攻击速度
	public float attackSpeed
	{
		get
		{
			//攻击速度 = 基础攻击速度 + 敏捷速度（敏捷*1） + 额外攻击速度
			return basAttackSpeed + agility + addlAttackSpeed;
		}
	}

	//攻击间隔
	public float attackTime
	{
		get
		{
			//攻击间隔 = 基础攻击间隔 ÷ 攻击速度%
			return basAttackTime / (attackSpeed/100);
		}
	}


    //物理抗性
    public float physicalResistance;
    //魔法抗性
    public float magicResistance;
    //物理伤害格挡
    public float physicalDamageBlock;
    //魔法伤害格挡
    public float magicDamageBlock;

	public Property()
    {
		basAttackTime = 1.7f;
		basHpMax = 200f;
		basMpMax = 75f;
        magicDamageBlock = 0.25f;
    }

	void Awake()
	{
		
	}
}