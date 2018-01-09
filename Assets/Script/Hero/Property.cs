using System.ComponentModel;
using UnityEngine;

public class Property {
    [Description("最大生命值")]
	public float HpMax{get;set;}

    [Description("最大能量值")]
	public float MpMax{get;set;}

    [Description("生命恢复")]
	public float HpRegeneration{get;set;}

    [Description("能量恢复")]
	public float MpRegeneration{get;set;}

    [Description("力量")]
	public float Strength{get;set;}

    [Description("敏捷")]
	public float Agility{get;set;}

    [Description("智力")]
	public float Intellect{get;set;}

    [Description("护甲")]
	public float Armor{get;set;}

    [Description("移动速度")]
	public float MoveSpeed{get;set;}

    [Description("攻击力")]
	public float Attack{get;set;}

    [Description("攻击速度")]
	public float AttackSpeed{get;set;}

    [Description("物理伤害减免")]
    public float PhysicalReduction{ get; set; }

    [Description("法术伤害减免")]
    public float MagicReduction{ get; set; }

    public Property()
    {
        HpMax = 200f;
        MpMax = 100f;
    }
}