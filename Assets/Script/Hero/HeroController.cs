﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Utility;

public class HeroController : MonoBehaviour {

	private Rigidbody _rigidbody;
	public float rotationSpeed = 20;

	private Transform player;
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this);
		_rigidbody = this.GetComponent<Rigidbody>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Global.heroDirection = 0;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Global.heroDirection = 1;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Global.heroDirection = 2;
        }
		if (Input.GetKeyDown(KeyCode.D))
        {
            Global.heroDirection = 3;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
		{
            HeroManager hero = player.GetComponent<HeroManager> ();

        }

		if (Input.GetKeyDown(KeyCode.Q))
		{
            HeroManager hero = player.GetComponent<HeroManager> ();
		}
	}

	void FixedUpdate()
	{
		MoveControlByTranslateGetAxis();
	}

	//Translate移动控制函数
	void MoveControlByTranslateGetAxis()
	{
        HeroManager heroManager = player.GetComponent<HeroManager> ();
        float horizontal = Input.GetAxis("Horizontal"); //A D 左右
		float vertical = Input.GetAxis("Vertical"); //W S 上 下

        _rigidbody.MovePosition(this.transform.position + new Vector3(horizontal, 0, vertical) * heroManager.property.moveSpeed * Time.deltaTime);
		if(horizontal != 0f || vertical != 0f)
		{
			Rotating(horizontal, vertical);
		}
	}

	void Rotating(float horizontal, float vertical)
	{
		// 创建角色目标方向的向量
		Vector3 targetDirection = new Vector3(horizontal, 0f, vertical);
		// 创建目标旋转值 并假设Y轴正方向为"上"方向
		Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up); //函数参数解释: LookRotation(目标方向为"前方向", 定义声明"上方向")
		// 创建新旋转值 并根据转向速度平滑转至目标旋转值
		//函数参数解释: Lerp(角色刚体当前旋转值, 目标旋转值, 根据旋转速度平滑转向)
		Quaternion newRotation = Quaternion.Lerp(_rigidbody.rotation, targetRotation, rotationSpeed * Time.deltaTime);
		// 更新刚体旋转值为 新旋转值
		_rigidbody.MoveRotation(newRotation);

	}
}
