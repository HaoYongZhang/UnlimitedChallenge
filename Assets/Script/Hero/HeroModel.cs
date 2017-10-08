using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroModel {
    //血量
    public float hp;
    //能量
    public float mp;
    //血量上限
    public float hpMax;
    //能量上限
    public float mpMax;
    //护甲
    public float armor;
    //速度
    public float speed;
    //力量
    public float strength;
    //敏捷
    public float agility;
    //智力
    public float intellect;
    //基础攻击力
    public float basicAttack;
    //额外攻击力
    public float additionalAttack;
    //攻击力
    public float attack;
    //物理抗性
    public float physicalResistance;
    //魔法抗性
    public float magicResistance;
    //物理伤害格挡
    public float physicalDamageBlock;
    //魔法伤害格挡
    public float magicDamageBlock;

    public HeroModel()
    {
        magicDamageBlock = 0.25f;
    }
}
