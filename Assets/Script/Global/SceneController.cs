using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {
    private Dictionary<string, Vector3> sceneEnterPosition = new Dictionary<string, Vector3>();
    private List<string> mainScene = new List<string>();
	// Use this for initialization
	void Start () {
        mainScene.Add("World_1__Scene_1");

        sceneEnterPosition.Add("World_1__Scene_1__Room_1", new Vector3(40.41f, -200f, -8.5f));
        sceneEnterPosition.Add("World_1__Scene_1__Room_1_1", new Vector3(40.41f + 300f, -300f, -8.5f));

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        if(mainScene.IndexOf(this.name) != -1)
        {
            Debug.Log("退出");
            Global.outRoom(collider, this.name);
        }
        else
        {
            Debug.Log("进入");
            Global.enterRoom(collider, this.name, sceneEnterPosition[this.name]);
        }
       
    }
}
