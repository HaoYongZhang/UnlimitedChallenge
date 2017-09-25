using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_1 : MonoBehaviour {
	public Camera _mainCamera;

	// Use this for initialization
	void Start () {
		_mainCamera = Global.setMainCamera (this.gameObject);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
