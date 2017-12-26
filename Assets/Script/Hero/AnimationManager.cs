using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

    public Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Move()
    {
        animator.SetBool("move", true);
    }

    public void StopMove()
    {
        animator.SetBool("move", false);
    }

    public void Attack()
    {
        animator.SetBool("attack", true);
    }

    public void StopAttack()
    {
        animator.SetBool("attack", false);
    }

    public void Death()
    {
        animator.SetBool("death", true);
    }
}
