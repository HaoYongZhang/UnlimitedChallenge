using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void CollisionEventDelegate(Collider _collider, int _colliderCount);

public class CollisionEvent : MonoBehaviour {
    public CollisionEventDelegate collisionEventDelegate;
    public int colliderCount;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// 进入触发点
    /// </summary>
    /// <param name="_collider">启动触发点的对象</param>
    void OnTriggerEnter(Collider _collider)
    {
        if (collisionEventDelegate != null)
        {
            colliderCount++;
            collisionEventDelegate(_collider, colliderCount);
        }
    }
}
