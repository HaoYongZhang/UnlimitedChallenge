using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 场景控制器
/// 主要用来挂靠在场景进入/退出的触发点对象上
/// </summary>
public class SceneController : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// 进入触发点
	/// </summary>
	/// <param name="collider">启动触发点的对象</param>
    void OnTriggerEnter(Collider collider)
    {
		
		//根据当前挂靠的对象名称判断当前行为是要进入或是退出场景
		//如果挂靠的对象名称是主场景名称就执行退出到主场景的方法
		if(SceneConstant.mainSceneNames.IndexOf(this.name) != -1)
        {
            outRoom(collider);
        }
        else
        {
			enterRoom(collider);
        }
       
    }

	/// <summary>
	/// 进入房间
	/// </summary>
	/// <param name="collider">碰撞体.</param>
	public void enterRoom(Collider collider){
		Scene scene = SceneManager.GetSceneByName(this.name);
		if(scene.isLoaded == false)
		{
			SceneManager.LoadScene(this.name, LoadSceneMode.Additive);
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
		Global.enterSceneBeforPositions.Add(heroPosition);
		//设置触发者进入到场景的初始位置
		collider.transform.position = SceneConstant.startPositions[this.name];
	}


	/// <summary>
	/// 退出房间
	/// </summary>
	/// <param name="collider">碰撞体.</param>
	public void outRoom(Collider collider)
	{
		collider.transform.position = Global.enterSceneBeforPositions[Global.enterSceneBeforPositions.Count - 1];
		//退出场景后清除最后一个记录点
		Global.enterSceneBeforPositions.RemoveAt(Global.enterSceneBeforPositions.Count - 1);
	}
}
