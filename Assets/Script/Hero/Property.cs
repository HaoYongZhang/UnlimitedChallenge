using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Property : MonoBehaviour {
	//力量
    public float strength;
    //敏捷
    public float agility;
    //智力
    public float intellect;

    //血量
    public float hp;
    //能量
    public float mp;
    //血量上限
    public float hpMax;
    //能量上限
    public float mpMax;
	//基础血量恢复速度
	public float basicHpRegeneration;
	//额外血量恢复速度
    public float additionalHpRegeneration;
	//血量恢复速度
	public float hpRegeneration
	{
		get
      	{
			//每一点力量值增加0.06点生命恢复速率
			return strength * 0.06f + basicHpRegeneration + additionalHpRegeneration; 
      	}
    }
	//能量回复速度
	public float mpRegeneration;

    //护甲
    public float armor;
    //速度
    public float speed;
    

    //基础攻击力
    public float basicAttack;
    //额外攻击力
    public float additionalAttack;
    //攻击力
	public float attack{
		get 
      {
         return basicAttack + additionalAttack; 
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
		basicHpRegeneration = 1;
        magicDamageBlock = 0.25f;
    }

	void Awake()
	{
		
	}
}