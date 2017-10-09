using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 场景常量
/// 此类保存一切场景用到的常量
/// </summary>
public class SceneConstant {
	// 为了使用单例模式而设置的私有属性
	private static Dictionary<string, Vector3> startPositionsInstance = new Dictionary<string, Vector3>();
	private static List<string> mainSceneNamesInstance = new List<string>();

	/// <summary>
	/// 主场景的列表
	/// </summary>
	/// <value>The main scene names.</value>
	public static List<string> mainSceneNames
	{
		get
		{
			if (SceneConstant.mainSceneNamesInstance.Count == 0) {
				mainSceneNamesInstance.Add("World_1__Scene_1");
			}

			return mainSceneNamesInstance;
		}
	}

	/// <summary>
	/// 进入场景后的开始位置
	/// </summary>
	/// <value>The start position.</value>
	public static Dictionary<string, Vector3> startPositions
	{
		get
		{
			if (SceneConstant.startPositionsInstance.Count == 0) {
				SceneConstant.startPositionsInstance.Add("World_1__Scene_1__Room_1", new Vector3(40.41f, -200f, -8.5f));
				SceneConstant.startPositionsInstance.Add("World_1__Scene_1__Room_1_1", new Vector3(40.41f + 300f, -300f, -8.5f));
			}

			return startPositionsInstance;
		}
	}



}
