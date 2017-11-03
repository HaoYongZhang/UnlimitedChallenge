using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.ComponentModel;

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
        isFight = true;
        Global.hero.animator.SetBool("Fight", true);
        Global.hero.animator.SetFloat("CombatType", (float)type);
    }


    /// <summary>
    /// 攻击动画攻击时刻
    /// </summary>
    void attackAnimationEnter()
    {
        Global.hero.rangeManager.SearchAttackRangeEnemys(20);
    }

    /// <summary>
    /// 攻击动画结束
    /// </summary>
    void attackAnimationEnd()
    {
        Global.hero.animator.SetBool("Fight", false);
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
