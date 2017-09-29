using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneUI : MonoBehaviour {
	static SceneUI scene_ui;  

	public static SceneUI Instance {  
		get {  
			if (scene_ui == null) {  
				scene_ui = FindObjectOfType (typeof(SceneUI)) as SceneUI;  
				if (scene_ui == null) {   
					Debug.Log ("执行scene_ui初始化");
					GameObject go = (GameObject)Resources.Load("UI/SceneUI");
					go.name = "SceneUI";
					scene_ui = go.AddComponent<SceneUI>();  
					scene_ui = Instantiate(scene_ui);
				}  
			}  
			return scene_ui;  
		}  
	}
		
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
