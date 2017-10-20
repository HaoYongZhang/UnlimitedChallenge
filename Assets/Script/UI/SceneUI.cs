using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using SkillClass;
using UnityEngine.EventSystems;

public class SceneUI : MonoBehaviour {
	static SceneUI _instance;

    public GameObject scene_ui_object;
	public GameObject skillInfo;
	public GameObject skillsBar;
    public GameObject skillStatusBar;
    public Slider hpBar;
    public Slider mpBar;
    public Text hpText;
    public Text mpText;

    //技能按钮集合
    public List<Button> skillButtons = new List<Button>();

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
        player = GameObject.FindGameObjectWithTag("Player").transform;

        scene_ui_object = (GameObject)Resources.Load("UI/SceneUI");
        scene_ui_object = Instantiate(scene_ui_object);
        scene_ui_object.name = "SceneUIObject";
        scene_ui_object.transform.SetParent(_instance.transform);

		hpBar = Utility.Context.GetComponent<Slider>(scene_ui_object, "HpBar");
		mpBar = Utility.Context.GetComponent<Slider>(scene_ui_object, "MpBar");

		hpText = Utility.Context.GetComponent<Text>(scene_ui_object, "HpText");
		mpText = Utility.Context.GetComponent<Text>(scene_ui_object, "MpText");

        skillStatusBar = GameObject.Find("SkillStatusBar");

		skillInfo = GameObject.Find("SkillInfo");
		skillInfo.SetActive (false);

		skillsBar = GameObject.Find("SkillsBar");
		skillButtons = new List<Button>(skillsBar.GetComponentsInChildren<Button>());
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

        HeroManager heroManager = player.GetComponent<HeroManager>();

        //更新人物当前生命值和能量值
        Set(heroManager.property.hp, heroManager.property.hpMax, heroManager.property.mp, heroManager.property.mpMax);
	}

	void FixedUpdate(){
		
	}

    /// <summary>
    /// 鼠标移动到技能栏的按钮
    /// </summary>
    /// <param name="objectName">Object name.</param>
    void onPointerEnterSkillButton(string objectName, PointerEventData eventData)
    {
        int number = int.Parse(objectName.Split('_')[1]);

        if (number <= (Global.activeSkills.Count - 1))
        {
            Skill skill = Global.activeSkills[number];
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

    void showSkillInfo(Skill skill)
    {
        Image image = Utility.Context.GetComponent<Image>(skillInfo, "Icon");
		image.sprite = skill.imageSprite;

		List<Text> labels = new List<Text>(SceneUI.Instance.skillInfo.GetComponentsInChildren<Text> ());

		foreach(Text label in labels)
		{
			if (label.name == "Name") {
				label.text = skill.data ["name"];
			}
			else if (label.name == "IsActive") {
				label.text = skill.data ["isActive"] == "1" ? "主动技能":"被动技能";
			}
			else if (label.name == "LevelMax") {
				label.text = "当前等级    " + skill.currentLevel + "/" + skill.data ["levelMax"];
			}
			else if (label.name == "Description") {
				label.text = skill.data ["description"];
//				Vector2 sizeDelta = label.GetComponent<RectTransform> ().sizeDelta;
//				label.GetComponent<RectTransform> ().sizeDelta = new Vector2 (sizeDelta.x, sizeDelta.y * 2);
//				Debug.Log(sizeDelta.y);
			}
			else if (label.name == "Duration") {
				label.text = "持续时间    " + skill.data ["duration"];
			}
			else if (label.name == "Cooldown") {
				label.text = "冷却时间    " + skill.data ["cooldown"];
			}
			else if (label.name == "CostEnergy") {
				label.text = "能量消耗     " + skill.data ["costEnergy"];
			}

		}

		skillInfo.SetActive (true);
    }

    void hideSkillInfo()
    {
		skillInfo.SetActive (false);
    }
}
