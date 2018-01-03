using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BultController : MonoBehaviour {

    public float impulseForce = 10f;
	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().AddForce(transform.up * impulseForce, ForceMode.Impulse);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.name);

        Destroy(gameObject);
    }
}
