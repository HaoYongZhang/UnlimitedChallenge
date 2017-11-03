using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public float moveSpeed = 200f;
    public float distanceMax = 100f;
    public Vector3 startPosition;

	// Use this for initialization
	void Start () {
        startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        //当子弹超出范围时，销毁对象
        if ((transform.position - startPosition).magnitude > distanceMax)
        {
            Debug.Log("超出范围，销毁子弹");
            Destroy(gameObject);
        }
	}

    /// <summary>
    /// 射击子弹
    /// </summary>
    public void fire()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * moveSpeed;
    }
}
