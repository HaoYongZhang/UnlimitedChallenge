using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.ComponentModel;
using EquipmentClass;
using EnemyClass;
using SkillClass;

public class FightManager : MonoBehaviour {
    public CombatType type;
    public bool isNormalAttacking;
    public Skill currentSkill;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void normalAttack()
    {
        if(!Global.hero.animationManager.isAttacking)
        {
            isNormalAttacking = true;
            AttackAnimation attackAnimation = EnumTool.GetEnum<AttackAnimation>(Global.hero.equipmentManager.currentWeapon.data["attackAnimation"]);
            Global.hero.animationManager.Attack(attackAnimation);
        }
    }

    public void SkillAttack(Skill skill)
    {
        if (!Global.hero.animationManager.isAttacking)
        {
            isNormalAttacking = false;
            AttackAnimation attackAnimation = skill.attackAnimation;
            Global.hero.animationManager.Attack(attackAnimation);
            currentSkill = skill;
        }
    }

    /// <summary>
    /// 开始攻击
    /// </summary>
    void startAttack()
    {
        Debug.Log("开始");
        if (Global.hero.animationManager.isAttacking)
        {
            Global.faceToMousePosition(gameObject);
        }
    }

    /// <summary>
    /// 攻击时刻
    /// </summary>
    void inAttack()
    {
        if(isNormalAttacking)
        {
            Equipment equipment = Global.hero.equipmentManager.currentWeapon;

            float attackDistance = float.Parse(equipment.data["attackDistance"]);

            List<GameObject> enemys = Global.hero.rangeManager.SearchRangeEnemys(Global.hero.transform, attackDistance);

            for (int i = 0; i < enemys.Count; i++)
            {
                DamageManager.CommonAttack<Hero, Enemy>(gameObject, enemys[i], EnumTool.GetEnum<DamageType>(equipment.data["damageType"]));
            }
        }
        else
        {
            currentSkill.releasingDelegate(currentSkill);
            Global.hero.skillManager.OnFinished(currentSkill);
            currentSkill = null;
        }
    }

    /// <summary>
    /// 攻击结束
    /// </summary>
    void endAttack()
    {
        bool isLongPress = Global.hero.gameObject.GetComponent<HeroController>().isLongPress;

        //当前攻击动画是普通攻击时才能长按持续动画
        if (isNormalAttacking)
        {
            if(!isLongPress)
            {
                Global.hero.animationManager.StopAttack();
            }
        }
        else
        {
            Global.hero.animationManager.StopAttack();
        }
    }
}

public enum CombatType
{
    combat,
    remote_short
}
