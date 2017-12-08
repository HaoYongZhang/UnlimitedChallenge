using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.ComponentModel;
using EquipmentClass;
using Utility;

public class FightManager : MonoBehaviour {
    
    public bool isFight;
    public CombatType type;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void fight()
    {
        if(!isFight)
        {
            isFight = true;
            WeaponType weaponType = PropertyUtil.GetEnum<WeaponType>(Global.hero.equipmentManager.currentWeapon.data["weaponType"]);
            Global.hero.animator.SetFloat("weaponType", (float)weaponType);
            Global.hero.animator.SetBool("attack", true);
        }
    }

    /// <summary>
    /// 开始攻击
    /// </summary>
    void startAttack()
    {
        
    }

    /// <summary>
    /// 攻击时刻
    /// </summary>
    void inAttack()
    {
        Global.hero.rangeManager.SearchAttackRangeEnemys(20);
    }

    /// <summary>
    /// 攻击结束
    /// </summary>
    void endAttack()
    {
        Global.hero.animator.SetBool("attack", false);
        isFight = false;

        //Invoke("endIsFight", 0.5f);
    }

    void endIsFight()
    {
        isFight = false;
    }
}

public enum CombatType
{
    combat,
    remote_short
}
