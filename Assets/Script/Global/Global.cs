using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Global{
	/// <summary>
	/// 设置一个主摄像机
	/// </summary>
	/// <returns>The main camera.</returns>
	/// <param name="gameObject">Game object.</param>
	public static Camera setMainCamera(GameObject gameObject){
		Camera _mainCamera = gameObject.AddComponent<Camera> ();
		_mainCamera.tag = "MainCamera";
		_mainCamera.gameObject.AddComponent<HeroCamera> ();

		SceneUI.Instance.transform.parent = _mainCamera.transform;

		return _mainCamera;
	}

	public static void setHp(float hp){
//		GameObject root = scene_ui.transform.Find ("root/Engine").gameObject;
//		Slider hpObj = hpGameObject.GetComponent<Slider>();
//		hpObj.value = hp;
//		Debug.Log (root.transform.childCount);
	}

	public static void test(){
		Debug.Log ("打印");
	}
}
