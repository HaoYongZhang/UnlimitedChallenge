using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    kick_1 = 10,
    kick_2 = 11,
    split_1 = 20,
    split_2 = 21,
    split_3 = 22,
    pistol_1 = 30,
    skill_1 = 60
}