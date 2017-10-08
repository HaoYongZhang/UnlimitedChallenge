using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Global{
    // 记录人物进入场景前的位置数组
    public static List<Vector3> enterBeforPositions = new List<Vector3>();

    // 人物面向的方向
    // 0 = 正面
    // 1 = 左面
    // 2 = 后面
    // 3 = 右面
    public static int heroDirection = 0;

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
        Scene scene = SceneManager.GetSceneByName(sceneName);
        if(scene.isLoaded == false)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }
        Vector3 heroPosition = collider.transform.position;
        //因为记录进入场景前位置时，人物是站在触发器上的，所以必须位置5距离避免退出场景时
        //又再站在触发器上，进入无限进退场景的bug
        switch (Global.heroDirection)
        {
            //正面进入房间
            case 0:
                {
                    heroPosition = new Vector3(heroPosition.x, heroPosition.y, heroPosition.z - 5f);
                }
                break;
            //左面进入房间
            case 1:
                {
                    heroPosition = new Vector3(heroPosition.x - 5f, heroPosition.y, heroPosition.z);
                }
                break;
            //后面进入房间
            case 2:
                {
                    heroPosition = new Vector3(heroPosition.x, heroPosition.y, heroPosition.z + 5f);
                }
                break;
            //右面进入房间
            case 3:
                {
                    heroPosition = new Vector3(heroPosition.x + 5f, heroPosition.y, heroPosition.z);
                }
                break;
        }
        //记录进入房间之前的位置
        Global.enterBeforPositions.Add(heroPosition);
        collider.transform.position = position;
	}

    public static void outRoom(Collider collider, string sceneName)
    {
        collider.transform.position = Global.enterBeforPositions[Global.enterBeforPositions.Count - 1];
        //退出场景后清除最后一个记录点
        Global.enterBeforPositions.RemoveAt(Global.enterBeforPositions.Count - 1);
    }
}
