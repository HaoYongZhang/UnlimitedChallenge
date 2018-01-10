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
        //SkillImplementation.Implement(gameObject, skill);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// 普通攻击
    /// </summary>
    public void NormalAttack()
    {
        if(!Global.hero.animationManager.isAttacking)
        {
            isNormalAttacking = true;
            AttackAnimation attackAnimation = EnumTool.GetEnum<AttackAnimation>(Global.hero.equipmentManager.currentWeapon.data["attackAnimation"]);
            Global.hero.animationManager.Attack(attackAnimation);
        }
    }

    /// <summary>
    /// 技能攻击
    /// </summary>
    /// <param name="skill">Skill.</param>
    public void SkillAttack(Skill skill)
    {
        if (!Global.hero.animationManager.isAttacking)
        {
            isNormalAttacking = false;
            AttackAnimation attackAnimation = skill.attackAnimation;
            Global.hero.animationManager.Attack(attackAnimation);
        }
    }

    /// <summary>
    /// 在攻击的时刻
    /// </summary>
    public void InAttack()
    {
        if (isNormalAttacking)
        {
            Equipment equipment = Global.hero.equipmentManager.currentWeapon;

            float attackDistance = float.Parse(equipment.data["attackDistance"]);

            List<GameObject> enemys = Global.hero.rangeManager.SearchRangeEnemys(Global.hero.transform, attackDistance, 60f);

            for (int i = 0; i < enemys.Count; i++)
            {
                OnNormalAttack(enemys[i]);
            }
        }
        else
        {
            SkillClass.Manager skillManager = Global.hero.skillManager;

            skillManager.OnFinished(skillManager.selectedSkill);

            SkillImplementation.Implement(gameObject, skillManager.selectedSkill);

            SkillActionRange actionRange = EnumTool.GetEnum<SkillActionRange>(skillManager.selectedSkill.data["actionRange"]);
            if (actionRange == SkillActionRange.sector_small ||
                actionRange == SkillActionRange.sector_medium ||
                actionRange == SkillActionRange.sector_large)
            {
                SectorAngle sectorAngle = EnumTool.GetEnum<SectorAngle>(actionRange.ToString());

                float angle = (int)sectorAngle;

                float distance = float.Parse(skillManager.selectedSkill.data["distance"]);

                List<GameObject> enemys = Global.hero.rangeManager.SearchRangeEnemys(Global.hero.transform, distance, angle);

                for (int i = 0; i < enemys.Count; i++)
                {
                    OnNormalAttack(enemys[i]);
                }
            }
        }
    }

    /// <summary>
    /// 受到普通攻击影响时
    /// </summary>
    /// <param name="enemy">Enemy.</param>
    public void OnNormalAttack(GameObject enemy)
    {
        if (enemy.layer == LayerMask.NameToLayer("Enemy"))
        {
            DamageManager.CommonAttack(gameObject, enemy, EnumTool.GetEnum<DamageType>(Global.hero.equipmentManager.currentWeapon.data["damageType"]));
        }
    }

    /// <summary>
    /// 受到技能攻击影响时
    /// </summary>
    /// <param name="enemy">Enemy.</param>
    /// <param name="skill">Skill.</param>
    public void OnSkillAttack(GameObject enemy, Skill skill)
    {
        if (enemy.layer == LayerMask.NameToLayer("Enemy"))
        {
            DamageManager.SkillAttack(gameObject, enemy, skill);
        }
    }
}

public enum CombatType
{
    combat,
    remote_short
}
