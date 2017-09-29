using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collider)
	{
		Global.enterRoom (collider, "scene_1_room_1", new Vector3 (40.41f, 0.5f, -12.75f));
		Debug.Log (this.name);
	}
}
