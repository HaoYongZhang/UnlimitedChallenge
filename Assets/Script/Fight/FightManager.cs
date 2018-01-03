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

    public void skillAttack(Skill skill)
    {
        if (!Global.hero.animationManager.isAttacking)
        {
            isNormalAttacking = false;
            AttackAnimation attackAnimation = AttackAnimation.skill_1;
            Global.hero.animationManager.Attack(attackAnimation);
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
            Transform point;
            GameObject bult = Resources.Load("Material/Effects/Bullets/VulcanBullet") as GameObject;

            for (int i = 0; i < Global.hero.charactersManager.boneTransforms.Count; i++)
            {
                if (Global.hero.charactersManager.boneTransforms[i].name == (CharactersManager.prefixBoneName + CharactersManager.left_point_name))
                {
                    point = Global.hero.charactersManager.boneTransforms[i];
                    bult = Instantiate(bult);
                    bult.transform.position = point.position;
                    bult.transform.rotation = Quaternion.Euler(90, Global.hero.transform.rotation.eulerAngles.y, 0);
                    break;
                }
            }
        }
    }

    /// <summary>
    /// 攻击结束
    /// </summary>
    void endAttack()
    {
        bool isLongPress = Global.hero.gameObject.GetComponent<HeroController>().isLongPress;

        if (!isLongPress)
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
