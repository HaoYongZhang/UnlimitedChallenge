using System.ComponentModel;

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
	//基础力量
    public float basStrength;
    //基础敏捷
    public float basAgility;
    //基础智力
    public float basIntellect;
	//基础物理护甲
	public float basArmor;
	//基础移动速度
	public float basMoveSpeed;
	//基础攻击力
	public float basAttack;
	//基础攻击间隔
	public float basAttackTime;
	//基础攻击速度
	public float basAttackSpeed;
    //基础物理抗性
    public float basPhysicalReduction;
    //基础法术抗性
    public float basMagicReduction;

	//------额外的（additional）
	//额外最大生命值
    [Description("最大生命值")]
	public float addlHpMax{get;set;}
	//额外最大能量值
    [Description("最大能量值")]
	public float addlMpMax{get;set;}
	//额外生命恢复速度
    [Description("生命恢复")]
	public float addlHpRegeneration{get;set;}
	//额外能量恢复速度
    [Description("能量恢复")]
	public float addlMpRegeneration{get;set;}
	//额外力量
    [Description("力量")]
	public float addlStrength{get;set;}
    //额外敏捷
    [Description("敏捷")]
	public float addlAgility{get;set;}
    //额外智力
    [Description("智力")]
	public float addlIntellect{get;set;}
    //额外物理护甲
    [Description("护甲")]
	public float addlArmor{get;set;}
	//额外移动速度
    [Description("移动速度")]
	public float addlMoveSpeed{get;set;}
	//额外攻击力
    [Description("攻击力")]
	public float addlAttack{get;set;}
	//额外攻击速度
    [Description("攻击速度")]
	public float addlAttackSpeed{get;set;}
    //额外物理抗性
    [Description("物理伤害减免")]
    public float addlPhysicalReduction;
    //额外法术抗性
    public float addlMagicReduction;

    //------比例的（rate）
    //最大生命值比率
    [Description("生命值")]
    public float rateHpMax{ get; set; }
    //最大能量值比率
    [Description("能量值")]
    public float rateMpMax{ get; set; }
    //生命恢复速度比率
    [Description("生命恢复")]
    public float rateHpRegeneration{ get; set; }
    //能量恢复速度比率
    [Description("能量恢复")]
    public float rateMpRegeneration{ get; set; }
    //力量比率
    [Description("力量")]
    public float rateStrength{ get; set; }
    //敏捷比率
    [Description("敏捷")]
    public float rateAgility{ get; set; }
    //智力比率
    [Description("智力")]
    public float rateIntellect{ get; set; }
    //物理护甲比率
    [Description("护甲")]
    public float rateArmor{ get; set; }
    //移动速度比率
    [Description("移动速度")]
    public float rateMoveSpeed{ get; set; }
    //攻击力比率
    [Description("攻击力")]
    public float rateAttack{ get; set; }
    //攻击速度比率
    [Description("攻击速度")]
    public float rateAttackSpeed{ get; set; }

	//------综合的属性
	//生命值
	public float hp;
	//能量值
	public float mp;

	//力量
    public float strength{
		get
		{
            return (basStrength + addlStrength) * rateStrength;
		}
	}
    //敏捷
    public float agility{
		get
		{
            return (basAgility + addlAgility) * rateAgility;
		}
	}
    //智力
    public float intellect{
		get
		{
            return (basIntellect + addlIntellect) * rateIntellect;
		}
	}
    
    //最大生命值
    public float hpMax{
		get
		{
			//最大生命值 = 基础最大生命值 + 力量最大生命值（力量*20） + 额外最大生命值 * 比率
            return (basHpMax + strength * 20f  + addlHpMax) * rateHpMax;
		}
	}
    //最大能量值
    public float mpMax{
		get
		{
			//最大能量值 = 基础最大魔法值 + 智力最大魔法值（智力*11） + 额外最大魔法值 * 比率
            return (basMpMax + intellect * 11f + addlMpMax) * rateHpMax;
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
            return (basHpRegeneration + strength * 0.06f  + addlHpRegeneration) * rateHpRegeneration; 
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
            return (basMpRegeneration + intellect * 0.04f + addlMpRegeneration) * rateMpRegeneration;
		}
	}

    //护甲
    public float armor
	{
		get
		{
			//护甲 = 基础护甲 + 敏捷护甲（敏捷*0.143） + 额外护甲
            return (basArmor + agility * 0.143f + addlArmor) * rateArmor;
		}
	}

    //移动速度
    public float moveSpeed
	{
		get
		{
            //移动速度 = （基础移动速度 + 额外移动速度）* （移动速度倍率 + （敏捷 * 0.06%））
            return (basMoveSpeed + addlMoveSpeed) * (rateMoveSpeed + agility * 0.0006f);
		}
	}

    //攻击力
	public float attack{
		get 
      {
            return (basAttack + addlAttack) * rateAttack; 
      }
    }

	//攻击速度
	public float attackSpeed
	{
		get
		{
			//攻击速度 = 基础攻击速度 + 敏捷速度（敏捷*1） + 额外攻击速度
            return (basAttackSpeed + agility + addlAttackSpeed) * rateAttackSpeed; 
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
    public float physicalReduction
    {
        get
        {
            //护甲的伤害减免（0.06 * 护甲/(1 + 0.06 * 护甲)）
            float armorReduction = 0.06f * armor / (1 + 0.06f * armor);
            //物理伤害倍率（1 - 护甲伤害减免）*（1 - 基础物理伤害减免）*（1 - 额外物理伤害减免）
            float damageRate = (1 - armorReduction) * (1 - basPhysicalReduction) * (1 - addlPhysicalReduction);

            return (1 - damageRate);
        }
    }
    //法术抗性
    public float magicReduction
    {
        get
        {
            //智力的伤害减免（0.06 * 护甲/(1 + 0.06 * 护甲)）
            float intellectReduction = 0.03f * intellect / (1 + 0.03f * intellect);
            //法术伤害倍率（1 - 智力伤害减免）*（1 - 基础法术伤害减免）*（1 - 额外法术伤害减免）
            float damageRate = (1 - intellectReduction) * (1 - basMagicReduction) * (1 - addlMagicReduction);

            return (1 - damageRate);
        }
    }
    //物理伤害格挡
    public float physicalDamageBlock;
    //魔法伤害格挡
    public float magicDamageBlock;

	public Property()
    {
		basAttackTime = 1.7f;
		basHpMax = 200f;
		basMpMax = 75f;

        //最大生命值比率
        rateHpMax = 1f;
        //最大能量值比率
        rateMpMax = 1f;
        //生命恢复速度比率
        rateHpRegeneration = 1f;
        //能量恢复速度比率
        rateMpRegeneration = 1f;
        //力量比率
        rateStrength = 1f;
        //敏捷比率
        rateAgility = 1f;
        //智力比率
        rateIntellect = 1f;
        //物理护甲比率
        rateArmor = 1f;
        //移动速度比率
        rateMoveSpeed = 1f;
        //攻击力比率
        rateAttack = 1f;
        //攻击速度比率
        rateAttackSpeed = 1f;
    }

}