using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Global : MonoBehaviour{
	public GameObject scene_ui;
	public float hp;

	// 定义一个静态变量来保存类的实例
	private static Global uniqueInstance;

	// 定义一个标识确保线程同步
	private static readonly object locker = new object();

	// 定义私有构造函数，使外界不能创建该类实例
	private Global()
	{
		scene_ui = (GameObject)Resources.Load("UI/SceneUI");
		scene_ui = Instantiate(scene_ui);
	}

	/// <summary>
	/// 定义公有方法提供一个全局访问点,同时你也可以定义公有属性来提供全局访问点
	/// </summary>
	/// <returns></returns>
	public static Global GetInstance()
	{
		// 当第一个线程运行到这里时，此时会对locker对象 "加锁"，
		// 当第二个线程运行该方法时，首先检测到locker对象为"加锁"状态，该线程就会挂起等待第一个线程解锁
		// lock语句运行完之后（即线程运行完之后）会对该对象"解锁"
		lock (locker)
		{
			// 如果类的实例不存在则创建，否则直接返回
			if (uniqueInstance == null)
			{
				uniqueInstance = new Global();
			}
		}

		return uniqueInstance;
	}


	/// <summary>
	/// 设置一个主摄像机
	/// </summary>
	/// <returns>The main camera.</returns>
	/// <param name="gameObject">Game object.</param>
	public static Camera setMainCamera(GameObject gameObject){
		Camera _mainCamera = gameObject.AddComponent<Camera> ();
		_mainCamera.tag = "MainCamera";
		_mainCamera.gameObject.AddComponent<HeroCamera> ();

		GameObject new_scene_ui = Global.GetInstance ().scene_ui;
		new_scene_ui.transform.parent = _mainCamera.transform;  
//		Global.GetInstance().test();

		return _mainCamera;
	}

//	public void setHp(float hp){
//		Transform transform = scene_ui.transform.Find ("HeroHp");
//		Slider hpObj = transform.gameObject.GetComponent<Slider>();
//		hpObj.value = hp;
//	}

	public void test(){
		Debug.Log ("打印");
	}
}
