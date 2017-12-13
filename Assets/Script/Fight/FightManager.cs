using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.ComponentModel;
using EquipmentClass;
using Utility;
using EnemyClass;

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
        //Debug.Log("开始攻击");
    }

    /// <summary>
    /// 攻击时刻
    /// </summary>
    void inAttack()
    {
        Equipment equipment = Global.hero.equipmentManager.currentWeapon;

        float attackDistance = float.Parse(equipment.data["attackDistance"]);

        List<GameObject> enemys = Global.hero.rangeManager.SearchRangeEnemys(attackDistance);

        for (int i = 0; i < enemys.Count; i++)
        {
            DamageManager.CommonAttack<Hero, Enemy>(gameObject, enemys[i], PropertyUtil.GetEnum<DamageType>(equipment.data["damageType"]));

            Debug.Log(enemys[i].GetComponent<Enemy>().property.hp);
        }
    }

    /// <summary>
    /// 攻击结束
    /// </summary>
    void endAttack()
    {
        Global.hero.animator.SetBool("attack", false);

        isFight = false;
    }
}

public enum CombatType
{
    combat,
    remote_short
}
