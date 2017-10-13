using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Global{
    // 记录人物进入场景前的位置数组
	public static List<Vector3> enterSceneBeforPositions = new List<Vector3>();

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
}

public enum SkillType
{
	//攻击
    Attack = "Attack",
    //防御
    Defense = "Defense",
    //治疗
    Treatment = "Treatment",
    //强化
    Intensify = "Intensify",
    //特殊
    Specialty = "Specialty"
}