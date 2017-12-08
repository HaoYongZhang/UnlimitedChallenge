using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyClass;

public class Zombie : MonoBehaviour {

    public Property property{ get; set; }

	// Use this for initialization
	void Start () {
        property = new Property();
        property.basAttack = 100;
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
