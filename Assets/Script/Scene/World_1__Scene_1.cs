using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World_1__Scene_1 : MonoBehaviour {
	// Use this for initialization
	void Start () {
		Global.setSceneCommonComponent ();
        Global.hero.transform.position = new Vector3(45f, 2f, -45f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
