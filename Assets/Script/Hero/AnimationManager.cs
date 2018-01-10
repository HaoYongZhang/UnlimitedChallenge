using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EquipmentClass;
using EnemyClass;
using SkillClass;

public class AnimationManager : MonoBehaviour {

    public Animator animator;

    public bool isMoving;
    public bool isAttacking;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Move()
    {
        isMoving = true;
        animator.SetBool("move", true);
    }

    public void StopMove()
    {
        isMoving = false;
        animator.SetBool("move", false);
    }

    public void Attack(AttackAnimation attackAnim)
    {
        isAttacking = true;
        float threshold = (int)attackAnim;
        animator.SetFloat("attackStyle", threshold);
        animator.SetBool("attack", true);
    }

    public void StopAttack()
    {
        isAttacking = false;
        animator.SetBool("attack", false);
    }

    public void Death()
    {
        animator.SetBool("death", true);
    }
   


    /// <summary>
    /// 开始攻击
    /// </summary>
    void startAttack()
    {
        if (isAttacking)
        {
            Global.faceToMousePosition(gameObject);
        }
    }

    /// <summary>
    /// 攻击时刻
    /// </summary>
    void inAttack()
    {
        Global.hero.fightManager.InAttack();
    }

    /// <summary>
    /// 攻击结束
    /// </summary>
    void endAttack()
    {
        bool isLongPress = Global.hero.gameObject.GetComponent<HeroController>().isLongPress;
        bool isNormalAttacking = Global.hero.fightManager.isNormalAttacking;

        //当前攻击动画是普通攻击时才能长按持续动画
        if (isNormalAttacking)
        {
            if (!isLongPress)
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

public enum AttackAnimation
{
    punch_1 = 0,
    kick_1 = 1,
    kick_2 = 2,
    split_1 = 3,
    split_2 = 4,
    split_3 = 5,
    pistol_1 = 6,
    skill_1 = 7,
    skill_2 = 8,
    skill_3 = 9
}