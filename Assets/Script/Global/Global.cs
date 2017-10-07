using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Global{
    //获取屏幕宽度和高度
    public static float screenWidth = Screen.width;
    public static float screenHeight = Screen.height;
    /// <summary>
    /// 设置场景的通用组件
    /// </summary>
    /// <returns>The scene common.</returns>
    /// <param name="gameObject">Game object.</param>
    public static void setSceneCommonComponent(){
        
        GameObject gameObject = new GameObject();
        gameObject.name = "SceneCommonComponent";
        Object.DontDestroyOnLoad(gameObject);

        //设置主摄像头
        Camera _mainCamera = gameObject.AddComponent<Camera> ();
		_mainCamera.tag = "MainCamera";
		_mainCamera.gameObject.AddComponent<HeroCamera> ();

		//设置场景UI
		SceneUI.Instance.transform.parent = _mainCamera.transform;
	}


	/// <summary>
	/// 进入房间
	/// </summary>
	/// <param name="collider">碰撞体.</param>
	/// <param name="sceneName">场景名称.</param>
	/// <param name="position">进入后的位置.</param>
	public static void enterRoom(Collider collider, string sceneName, Vector3 position){
		SceneManager.LoadScene (sceneName, LoadSceneMode.Additive);
		collider.transform.position = position;
	}
}
