using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {
	public Button startButton;
	public Button settingButton;
	public Button quitButton;

	// Use this for initialization
	void Start () {
		startButton.onClick.AddListener (delegate() {  
			this.start();   
		});

		settingButton.onClick.AddListener (delegate() {  
			this.setting();   
		});

		quitButton.onClick.AddListener (delegate() {  
			this.quit();   
		});
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//开始游戏
	public void start(){
		Debug.Log ("启动游戏");
		SceneManager.LoadScene ("scene_1");
	}

	//设置游戏
	public void setting(){
		Debug.Log ("设置游戏");
	}

	//退出游戏
	public void quit(){
		Debug.Log ("退出游戏");
	}

	void OnDestroy(){
		startButton.onClick.RemoveAllListeners ();
		settingButton.onClick.RemoveAllListeners ();
		quitButton.onClick.RemoveAllListeners ();
	}
}
