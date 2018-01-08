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