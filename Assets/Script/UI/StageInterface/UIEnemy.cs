using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EnemyClass;

public class UIEnemy : MonoBehaviour {

    static UIEnemy _instance;

    public List<GameObject> enemyHps = new List<GameObject>();

    /// <summary>
    /// 单例
    /// </summary>
    public static UIEnemy Instance
    {
        get
        {
            if (_instance == null)  // 如果没有找到
            {
                GameObject go = Instantiate((GameObject)Resources.Load("UI/UIEnemy")); // 创建一个新的GameObject
                _instance = go.GetComponent<UIEnemy>(); // 将实例挂载到GameObject上
            }
            return _instance;
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        
    }
}
