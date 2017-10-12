using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

public class SceneUI : MonoBehaviour {
	static SceneUI _instance;

    public GameObject scene_ui_object;
    public Slider hpBar;
    public Slider mpBar;
    public Text hpText;
    public Text mpText;

	private Transform player;


    /// <summary>
    /// 单例
    /// </summary>
    public static SceneUI Instance {  
		get {  
            if (_instance == null)  // 如果没有找到
            {
                Debug.Log("执行scene_ui初始化");
                GameObject go = new GameObject("SceneUI"); // 创建一个新的GameObject
                _instance = go.AddComponent<SceneUI>(); // 将实例挂载到GameObject上

                _instance.init();
            }
            return _instance;
        }  
	}

    private SceneUI()
    {

    }

    /// <summary>
    /// 初始化
    /// </summary>
    private void init()
    {
        scene_ui_object = (GameObject)Resources.Load("UI/SceneUI");
        scene_ui_object.name = "SceneUIObject";
        scene_ui_object = Instantiate(scene_ui_object);
        scene_ui_object.transform.SetParent(_instance.transform);

        hpBar = ComponentUtility.GetComponent<Slider>(scene_ui_object, "HpBar");
        mpBar = ComponentUtility.GetComponent<Slider>(scene_ui_object, "MpBar");

        hpText = ComponentUtility.GetComponent<Text>(scene_ui_object, "HpText");
        mpText = ComponentUtility.GetComponent<Text>(scene_ui_object, "MpText");

    }

    /// <summary>
    /// 设置界面主要元素的值
    /// </summary>
    /// <param name="hp">hp</param>
    /// <param name="hpMax">hpMax</param>
    /// <param name="mp">mp</param>
    /// <param name="mpMax">mpMax</param>
    public void Set(float hp, float hpMax, float mp, float mpMax)
    {
        hpBar.value = hp / hpMax;
        mpBar.value = mp / mpMax;
        hpText.text = hp + "/" + hpMax;
        mpText.text = mp + "/" + mpMax;
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

	void FixedUpdate(){
		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player").transform;
		}

		HeroSystem heroSystem = player.GetComponent<HeroSystem> ();

		Set (heroSystem.property.hp, heroSystem.property.hpMax, heroSystem.property.mp, heroSystem.property.mpMax);
	}
}
