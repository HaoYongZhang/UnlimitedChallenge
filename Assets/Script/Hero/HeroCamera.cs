using UnityEngine;
using System.Collections.Generic;
using System.Collections;

//添加脚本到component菜单
[AddComponentMenu ("CameraControl/Follow")]
public class HeroCamera : MonoBehaviour {

	//摄像机固定在猪脚上方10米高度
	public float camera_height=30.0f;
	//摄像机离猪脚大概10米的水平距离
	public float camera_distance=35.0f;

	//摄像机和猪脚的transform属性
	private Transform player;
	private Transform camera;
	// Use this for initialization
	void Start () {
		//初始化
		player  = GameObject.FindGameObjectWithTag("Player").transform;

		camera = Camera.main.transform;
	}

	// Update is called once per frame
	void Update () {
		//与猪脚的正前方为正前方(只取Y轴的旋转度)
		camera.eulerAngles =new Vector3(
			player.eulerAngles.x + 35,
			-45,
			player.eulerAngles.z
		);
		//获取当前的镜头的Y轴旋转度
		float angle = camera.eulerAngles.y;

		//计算x轴的距离差:
		float deltaX = camera_distance * Mathf.Sin(angle * Mathf.PI /180 );
		float deltaZ = camera_distance * Mathf.Cos (angle * Mathf.PI / 180);

		//每一帧都改变摄像机的高度
		camera.position = new Vector3 (
			player.position.x - deltaX,
			player.position.y + camera_height,
			player.position.z - deltaZ
		);

	}
}