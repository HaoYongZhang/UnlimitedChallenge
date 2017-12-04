using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour {
    
    public Rigidbody _rigid;
    public Animator _animator;

	// Use this for initialization
	void Start () {
        //Invoke("test", 3f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void test()
    {
        Debug.Log("执行");
        //_animator.SetBool("running", true);
    }
}
