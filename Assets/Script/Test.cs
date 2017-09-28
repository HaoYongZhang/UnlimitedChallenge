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
		print(collider.gameObject.name + ":" + Time.time);
		Object.DontDestroyOnLoad(collider.gameObject); 

		SceneManager.LoadScene ("scene_1_room_1");
		Scene scene = SceneManager.GetSceneByName("scene_1_room_1");;
//		SceneManager.MoveGameObjectToScene (collider.gameObject, scene);


	}
}
