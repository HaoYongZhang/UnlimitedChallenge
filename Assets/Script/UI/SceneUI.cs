using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using Skill.Collections;
using UnityEngine.EventSystems;

public class SceneUI : MonoBehaviour {
	static SceneUI _instance;

    public GameObject scene_ui_object;
    public Slider hpBar;
    public Slider mpBar;
    public Text hpText;
    public Text mpText;

    //技能按钮集合
    private List<Button> skillButtons = new List<Button>();

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

		hpBar = Utility.Context.GetComponent<Slider>(scene_ui_object, "HpBar");
		mpBar = Utility.Context.GetComponent<Slider>(scene_ui_object, "MpBar");

		hpText = Utility.Context.GetComponent<Text>(scene_ui_object, "HpText");
		mpText = Utility.Context.GetComponent<Text>(scene_ui_object, "MpText");


        GameObject skillObject = GameObject.Find("SkillObject");
        skillButtons = new List<Button>(skillObject.GetComponentsInChildren<Button>());
        for (int i = 0; i < skillButtons.Count; i++)
        {
            int j = i;

            Button btn = skillButtons[i];

            UIInfo uiInfo = btn.gameObject.GetComponent<UIInfo>();

            uiInfo.onPointerEnterDelegate = onPointerEnterSkillButton;
            uiInfo.onPointerExitDelegate = onPointerExitSkillButton;
        }
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

		//更新人物当前生命值和能量值
		Set (heroSystem.property.hp, heroSystem.property.hpMax, heroSystem.property.mp, heroSystem.property.mpMax);
	}

    /// <summary>
    /// 鼠标移动到技能栏的按钮
    /// </summary>
    /// <param name="objectName">Object name.</param>
    void onPointerEnterSkillButton(string objectName, PointerEventData eventData)
    {
        GameObject go = GameObject.Find(objectName);

        int number = int.Parse(objectName.Split('_')[1]);

        SkillManager skillManager = player.GetComponent<SkillManager>();

        if (number <= (skillManager.skills.Count - 1))
        {
            Skill.Collections.Skill skill = skillManager.skills[number];
            showSkillInfo(skill);
        }

    }

    /// <summary>
    /// 鼠标移出技能栏的按钮
    /// </summary>
    /// <param name="objectName">Object name.</param>
    void onPointerExitSkillButton(string objectName, PointerEventData eventData)
    {
        hideSkillInfo();
    }

    void showSkillInfo(Skill.Collections.Skill skill)
    {
        //skill.data["imageName"];
        //skill.data["name"];
        //skill.data["isActive"];
        //skill.data["levelMax"];
        //skill.data["duration"];
        //skill.data["cooldown"];
        //skill.data["costEnergy"];
        //skill.data["description"];

        Debug.Log(skill.data["imageName"]);
        Debug.Log(skill.data["name"]);
        Debug.Log(skill.data["isActive"]);
        Debug.Log(skill.data["description"]);
        Debug.Log(skill.data["levelMax"]);
        Debug.Log(skill.data["duration"]);
        Debug.Log(skill.data["cooldown"]);
        Debug.Log(skill.data["costEnergy"]);

    }

    void hideSkillInfo()
    {
        
    }
}
