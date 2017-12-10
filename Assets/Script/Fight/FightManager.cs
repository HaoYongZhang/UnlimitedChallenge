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
        Debug.Log("cesfy");
    }

    /// <summary>
    /// 攻击时刻
    /// </summary>
    void inAttack()
    {
        //List<GameObject> enemys = Global.hero.rangeManager.SearchRangeEnemys(20);

        //for (int i = 0; i < enemys.Count; i ++)
        //{
        //    float distance = 15f;

        //    Quaternion r = transform.rotation;
        //    Vector3 f0 = (transform.position + (r * Vector3.forward) * distance);
        //    Debug.DrawLine(transform.position, f0, Color.red);

        //    Quaternion r0 = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 30f, transform.rotation.eulerAngles.z);
        //    Quaternion r1 = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 30f, transform.rotation.eulerAngles.z);

        //    Vector3 f1 = (transform.position + (r0 * Vector3.forward) * distance);
        //    Vector3 f2 = (transform.position + (r1 * Vector3.forward) * distance);

        //    Debug.DrawLine(transform.position, f1, Color.red);
        //    Debug.DrawLine(transform.position, f2, Color.red);
        //    Debug.DrawLine(f1, f2, Color.red);

        //    Vector3 point = enemys[0].transform.position;

        //    if (Global.hero.rangeManager.IsInTriangle(point, transform.position, f1, f2))
        //    {
        //        Debug.Log("cube in this !!!");
        //    }
        //    else
        //    {
        //        Debug.Log("cube not in this !!!");
        //    }
        //}
    }

    /// <summary>
    /// 攻击结束
    /// </summary>
    void endAttack()
    {
        Global.hero.animator.SetBool("attack", false);
        //isFight = false;

        Invoke("endIsFight", 0.5f);
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
