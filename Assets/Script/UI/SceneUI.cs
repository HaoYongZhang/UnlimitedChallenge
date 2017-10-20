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

	Transform player;
    //当前显示详细信息的技能
    Skill currentShowInfoSkill;

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
    void init()
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

    /// <summary>
    /// 鼠标移动到技能状态
    /// </summary>
    /// <param name="objectName">Object name.</param>
    public void onPointerEnterSkillStatus(string objectName, PointerEventData eventData)
    {
        foreach(Skill skill in Global.skills)
        {
            if(skill.id == objectName)
            {
                showSkillInfo(skill);
            }
        }
    }

    /// <summary>
    /// 鼠标移出技能状态
    /// </summary>
    /// <param name="objectName">Object name.</param>
    public void onPointerExitSkillStatus(string objectName, PointerEventData eventData)
    {
        hideSkillInfo();
    }


    /// <summary>
    /// 显示技能的详细信息
    /// </summary>
    /// <param name="skill">Skill.</param>
    void showSkillInfo(Skill skill)
    {
        currentShowInfoSkill = skill;

        Image image = Utility.Context.GetComponent<Image>(skillInfo, "Icon");
		image.sprite = skill.imageSprite;

		List<Text> labels = new List<Text>(SceneUI.Instance.skillInfo.GetComponentsInChildren<Text> ());

        List<string> texts = new List<string>();

        texts.Add(skill.data["name"]);
        if(skill.isActive)
        {
            texts.Add("持续时间    " + skill.data["duration"]);
            texts.Add("冷却时间    " + skill.data["cooldown"]);
            texts.Add("能量消耗     " + skill.data["costEnergy"]);
            texts.Add(skill.data["description"]);
        }
        else
        {
            texts.Add("被动技能");
            texts.Add(skill.categoryName);
            texts.Add(skill.typeName);
            texts.Add(skill.data["description"]);
        }

        for (int i = 0; i < labels.Count;i++)
		{
            Text label = labels[i];
            label.text = texts[i];
		}



		skillInfo.SetActive (true);
    }

    /// <summary>
    /// 隐藏技能的详细信息
    /// </summary>
    void hideSkillInfo()
    {
		skillInfo.SetActive (false);
    }

    /// <summary>
    /// 添加技能的小状态图标
    /// </summary>
    /// <param name="skill">Skill.</param>
    public void addSkillStatusIcon(Skill skill){
        GameObject skillStatusIcon = (GameObject)Resources.Load("UI/SkillStatusIcon");
        skillStatusIcon = Instantiate(skillStatusIcon);
        skillStatusIcon.name = skill.id;
        skillStatusIcon.transform.SetParent(skillStatusBar.transform);

        Image icon = skillStatusIcon.transform.Find("Icon").GetComponent<Image>();
        icon.sprite = skill.imageSprite;

        UIInfo uiInfo = skillStatusIcon.GetComponent<UIInfo>();
        uiInfo.onPointerEnterDelegate = onPointerEnterSkillStatus;
        uiInfo.onPointerExitDelegate = onPointerExitSkillStatus;
    }

    /// <summary>
    /// 移除技能的小状态图标
    /// </summary>
    /// <param name="skill">Skill.</param>
    public void removeSkillStatusIcon(Skill skill)
    {
        string skillName = skill.id;

        GameObject skillObject = skillStatusBar.transform.Find(skillName).gameObject;
        Destroy(skillObject);
        //如果当前正在显示技能状态的详细信息时，关闭
        if(currentShowInfoSkill.id == skill.id)
        {
            hideSkillInfo();
        }
    }
}
