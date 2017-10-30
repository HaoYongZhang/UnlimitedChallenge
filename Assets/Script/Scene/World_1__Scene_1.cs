using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World_1__Scene_1 : MonoBehaviour {
	// Use this for initialization
	void Start () {
		Global.setSceneCommonComponent ();
        Hero.Instance.transform.parent = transform;
        Hero.Instance.gameObject.GetComponent<CharactersManager>().mainBone.transform.position =new Vector3(0,0,0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
