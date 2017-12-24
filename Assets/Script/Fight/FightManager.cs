using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.ComponentModel;
using EquipmentClass;
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
            WeaponType weaponType = EnumTool.GetEnum<WeaponType>(Global.hero.equipmentManager.currentWeapon.data["weaponType"]);
            Global.hero.animator.SetFloat("weaponType", (float)weaponType);
            Global.hero.animator.SetBool("attack", true);
        }
    }

    /// <summary>
    /// 开始攻击
    /// </summary>
    void startAttack()
    {
        Global.faceToMousePosition(gameObject);
    }

    /// <summary>
    /// 攻击时刻
    /// </summary>
    void inAttack()
    {
        Equipment equipment = Global.hero.equipmentManager.currentWeapon;

        float attackDistance = float.Parse(equipment.data["attackDistance"]);

        //List<GameObject> enemys = Global.hero.rangeManager.SearchRangeEnemys(Global.hero.transform, attackDistance);
        List<GameObject> enemys = Global.hero.rangeManager.SearchRangeEnemys(Global.hero.transform, 30);

        for (int i = 0; i < enemys.Count; i++)
        {
            DamageManager.CommonAttack<Hero, Enemy>(gameObject, enemys[i], EnumTool.GetEnum<DamageType>(equipment.data["damageType"]));
        }
    }

    /// <summary>
    /// 攻击结束
    /// </summary>
    void endAttack()
    {
        bool isLongPress = Global.hero.gameObject.GetComponent<HeroController>().isLongPress;

        if(!isLongPress)
        {
            Global.hero.animator.SetBool("attack", false);

            isFight = false;
        }
    }
}

public enum CombatType
{
    combat,
    remote_short
}
