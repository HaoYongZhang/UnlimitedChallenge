using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {

    static Hero _instance;

    /// <summary>
    /// 单例
    /// </summary>
    public static Hero Instance
    {
        get
        {
            if (_instance == null)  // 如果没有找到
            {
                GameObject go = (GameObject)Instantiate(Resources.Load("Avatar/Hero/Hero"));
                go.name = "Hero";
                _instance = go.AddComponent<Hero>(); // 将实例挂载到GameObject上

                _instance.init();
            }
            return _instance;
        }
    }

    void init(){
        
    }

    void Awake()
    {
        _instance = this;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
