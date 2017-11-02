using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using SkillClass;
using UnityEngine.EventSystems;

public class UIScene : MonoBehaviour
{
    static UIScene _instance;

    public GameObject scene_ui_object;
    public GameObject skillInfo;
    public GameObject skillsBar;
    public GameObject skillStatusBar;
    public GameObject sceneProperty;

    public Slider hpBar;
    public Slider mpBar;
    public Text hpText;
    public Text mpText;
    public Text hpRegenerationText;
    public Text mpRegenerationText;

    //技能按钮集合
    public List<Button> skillButtons = new List<Button>();

    Transform player;
    //当前显示详细信息的技能
    Skill currentShowInfoSkill;

    /// <summary>
    /// 单例
    /// </summary>
    public static UIScene Instance
    {
        get
        {
            if (_instance == null)  // 如果没有找到
            {
                GameObject go = new GameObject("UIScene"); // 创建一个新的GameObject
                _instance = go.AddComponent<UIScene>(); // 将实例挂载到GameObject上

                _instance.init();
            }
            return _instance;
        }
    }

    private UIScene()
    {

    }

    /// <summary>
    /// 初始化
    /// </summary>
    void init()
    {
        gameObject.AddComponent<UICursor>();

        scene_ui_object = (GameObject)Resources.Load("UI/UIScene");
        scene_ui_object = Instantiate(scene_ui_object);
        scene_ui_object.name = "UISceneObject";
        scene_ui_object.transform.SetParent(_instance.transform);

        hpBar = Utility.Context.GetComponent<Slider>(scene_ui_object, "HpBar");
        mpBar = Utility.Context.GetComponent<Slider>(scene_ui_object, "MpBar");

        hpText = Utility.Context.GetComponent<Text>(scene_ui_object, "HpText");
        mpText = Utility.Context.GetComponent<Text>(scene_ui_object, "MpText");

        hpRegenerationText = Utility.Context.GetComponent<Text>(scene_ui_object, "HpRegenerationText");
        mpRegenerationText = Utility.Context.GetComponent<Text>(scene_ui_object, "MpRegenerationText");

        skillStatusBar = GameObject.Find("SkillStatusBar");

        sceneProperty = GameObject.Find("UISceneProperty");
        sceneProperty.SetActive(false);

        skillInfo = GameObject.Find("SkillInfo");
        skillInfo.SetActive(false);

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
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.value = Global.hero.property.hp / Global.hero.property.hpMax;
        mpBar.value = Global.hero.property.mp / Global.hero.property.mpMax;
        hpText.text = Global.hero.property.hp + "/" + Global.hero.property.hpMax;
        mpText.text = Global.hero.property.mp + "/" + Global.hero.property.mpMax;

        if (Global.hero.property.hpRegeneration > 0)
        {
            hpRegenerationText.text = "+" + Math.Round(Global.hero.property.hpRegeneration, 1);
        }
        else
        {
            hpRegenerationText.text = "-" + Math.Round(Global.hero.property.hpRegeneration, 1);
        }

        if (Global.hero.property.mpRegeneration > 0)
        {
            mpRegenerationText.text = "+" + Math.Round(Global.hero.property.mpRegeneration, 1);
        }
        else
        {
            mpRegenerationText.text = "-" + Math.Round(Global.hero.property.mpRegeneration, 1);
        }
    }

    void FixedUpdate()
    {

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
        foreach (Skill skill in Global.skills)
        {
            if (skill.id == objectName)
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

        List<Text> labels = new List<Text>(Instance.skillInfo.GetComponentsInChildren<Text>());
        string info = "";

        if (skill.isActive)
        {
            info += skill.description + "\n";

            if(skill.data["duration"] != "0")
            {
                info += "持续时间    " + skill.data["duration"] + "\n";
            }

            if (skill.data["cooldown"] != "0")
            {
                info += "冷却时间    " + skill.data["cooldown"] + "\n";
            }

            if (skill.data["costEnergy"] != "0")
            {
                info += "能量消耗    " + "<color=#37abe1>" + skill.data["costEnergy"] + "</color>" + "\n";
            }

        }
        else
        {
            info += "被动技能" + "\n";
            info += skill.description + "\n";
        }

        info += "<size=30>" + "<color=#00ffffff>" + "来源于" + skill.data["origin"] + "</color>" + "</size>";

        float height = 100f;

        foreach(Text label in labels)
        {
            if(label.name == "SkillName")
            {
                label.text = skill.data["name"];
            }
            else if (label.name == "SkillDescription")
            {
                label.text = info;
                //技能描述的实际高度 + 图标高度 + top&bottom的边距 + 间隔
                height = label.preferredHeight + 120 + 20 * 2 + 20;
            }
        }

        skillInfo.GetComponent<RectTransform>().sizeDelta = new Vector2(skillInfo.GetComponent<RectTransform>().sizeDelta.x, height);

        skillInfo.SetActive(true);
    }

    /// <summary>
    /// 隐藏技能的详细信息
    /// </summary>
    void hideSkillInfo()
    {
        skillInfo.SetActive(false);
    }

    /// <summary>
    /// 添加技能的小状态图标
    /// </summary>
    /// <param name="skill">Skill.</param>
    public void addSkillStatusIcon(Skill skill)
    {
        GameObject skillStatusIcon = (GameObject)Resources.Load("UI/SkillStatusIcon");
        skillStatusIcon = Instantiate(skillStatusIcon);
        skillStatusIcon.name = skill.id;
        skillStatusIcon.transform.SetParent(skillStatusBar.transform, false);

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
        if (currentShowInfoSkill != null)
        {
            if (currentShowInfoSkill.id == skill.id)
            {
                currentShowInfoSkill = null;
                hideSkillInfo();
            }
        }
    }
}
