using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using SkillClass;
using UnityEngine.EventSystems;
using DG.Tweening;

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
    public List<SkillClass.UIButton> skillButtons = new List<SkillClass.UIButton>();

    //当前的技能栏
    int currentSkillBar;

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

        scene_ui_object = Instantiate((GameObject)Resources.Load("UI/UISceneCanvas"));
        scene_ui_object.name = "UISceneCanvas";
        scene_ui_object.transform.SetParent(_instance.transform, false);

        hpBar = Utility.Context.GetComponent<Slider>(scene_ui_object, "HpBar");
        mpBar = Utility.Context.GetComponent<Slider>(scene_ui_object, "MpBar");

        hpText = Utility.Context.GetComponent<Text>(scene_ui_object, "HpText");
        mpText = Utility.Context.GetComponent<Text>(scene_ui_object, "MpText");

        hpRegenerationText = Utility.Context.GetComponent<Text>(scene_ui_object, "HpRegenerationText");
        mpRegenerationText = Utility.Context.GetComponent<Text>(scene_ui_object, "MpRegenerationText");

        skillStatusBar = GameObject.Find("SkillStatusBar");

        sceneProperty = Instantiate((GameObject)Resources.Load("UI/UIHeroView"));
        sceneProperty.name = "UIHeroView";
        sceneProperty.transform.SetParent(scene_ui_object.transform, false);
        sceneProperty.SetActive(false);

        skillInfo = GameObject.Find("SkillInfo");
        skillInfo.SetActive(false);

        skillsBar = GameObject.Find("SkillsBar");

        for (int i = 0; i < 5; i++)
        {
            SkillClass.UIButton skillButton = SkillClass.UIButton.NewInstantiate();
            skillButton.transform.SetParent(skillsBar.transform, false);

            UIMouseDelegate mouseDelegate = skillButton.gameObject.GetComponent<UIMouseDelegate>();

            mouseDelegate.onPointerEnterDelegate = onPointerEnterSkillButton;
            mouseDelegate.onPointerExitDelegate = onPointerExitSkillButton;

            skillButtons.Add(skillButton);
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
        if(currentSkillBar == 0)
        {
            for (int i = 0; i < Global.shortcutSkills_1.Count; i++)
            {
                if (Global.shortcutSkills_1[i] != null)
                {
                    if (skillButtons[i].skill == null || Global.shortcutSkills_1[i].id != skillButtons[i].skill.id)
                    {
                        skillButtons[i].setSkill(SkillClass.Manager.GetOneSkillByID(Global.shortcutSkills_1[i].id));
                    }
                }
                else
                {
                    skillButtons[i].setSkill(null);
                }
            }
        }
        else
        {
            for (int i = 0; i < Global.shortcutSkills_2.Count; i++)
            {
                if (Global.shortcutSkills_2[i] != null)
                {
                    if (skillButtons[i].skill == null || Global.shortcutSkills_2[i].id != skillButtons[i].skill.id)
                    {
                        skillButtons[i].setSkill(SkillClass.Manager.GetOneSkillByID(Global.shortcutSkills_2[i].id));
                    }
                }
                else
                {
                    skillButtons[i].setSkill(null);
                }
            }
        }

    }

    public void switchSkillBar()
    {
        Vector3 skillsBarPosition = skillsBar.GetComponent<RectTransform>().position;
        Vector3 oldPosition = skillsBarPosition;
        Vector3 newPosition = new Vector3(skillsBarPosition.x, skillsBarPosition.y - 150);

        skillsBar.GetComponent<RectTransform>()
                 .DOMove(newPosition, 0.15f)
                 .SetDelay(0)
                 .SetEase(Ease.Linear)
                 .OnComplete(delegate () 
        {
            if (currentSkillBar == 0)
            {
                currentSkillBar = 1;
            }
            else
            {
                currentSkillBar = 0;
            }

            skillsBar.GetComponent<RectTransform>()
                     .DOMove(oldPosition, 0.15f)
                    .SetDelay(0)
                    .SetEase(Ease.Linear)
                    .OnComplete(delegate () 
            {
                
            });
        });

    }

    /// <summary>
    /// 鼠标移动到技能栏的按钮
    /// </summary>
    /// <param name="obj">Object.</param>
    public void onPointerEnterSkillButton(GameObject obj, PointerEventData eventData)
    {
        foreach(Skill skill in Global.skills)
        {
            SkillClass.UIButton skillButton = obj.GetComponent<SkillClass.UIButton>();

            if (skillButton.skill != null)
            {
                if (skillButton.skill.id == skill.id)
                {
                    showSkillInfo(skill);
                }
            }
        }
    }

    /// <summary>
    /// 鼠标移出技能栏的按钮
    /// </summary>
    /// <param name="obj">Object.</param>
    public void onPointerExitSkillButton(GameObject obj, PointerEventData eventData)
    {
        hideSkillInfo();
    }

    /// <summary>
    /// 鼠标移动到技能状态
    /// </summary>
    /// <param name="obj">Object name.</param>
    public void onPointerEnterSkillStatus(GameObject obj, PointerEventData eventData)
    {
        foreach (Skill skill in Global.skills)
        {
            if (skill.id == obj.name)
            {
                showSkillInfo(skill);
            }
        }
    }

    /// <summary>
    /// 鼠标移出技能状态
    /// </summary>
    /// <param name="obj">Object name.</param>
    public void onPointerExitSkillStatus(GameObject obj, PointerEventData eventData)
    {
        hideSkillInfo();
    }


    /// <summary>
    /// 显示技能的详细信息
    /// </summary>
    /// <param name="skill">Skill.</param>
    public void showSkillInfo(Skill skill)
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
            info += skill.description + "\n";
        }

        info += "<size=30>" + "<color=#00ffffff>" + "来源于" + skill.data["origin"] + "</color>" + "</size>";

        string isActiveStr = skill.isActive ? "主动技能" : "被动技能";
        float height = 0;

        foreach(Text label in labels)
        {
            if(label.name == "SkillName")
            {
                label.text = skill.data["name"] + "\n" + "<size=34>" + isActiveStr + "</size>";
            }
            else if (label.name == "SkillDescription")
            {
                label.text = info;
                //技能描述的实际高度 + 图标高度 + top&bottom的边距 + 间隔
                height = label.preferredHeight + 120 + 30 * 2 + 20;
            }
        }

        skillInfo.GetComponent<RectTransform>().sizeDelta = new Vector2(skillInfo.GetComponent<RectTransform>().sizeDelta.x, height);

        skillInfo.SetActive(true);
    }

    /// <summary>
    /// 隐藏技能的详细信息
    /// </summary>
    public void hideSkillInfo()
    {
        skillInfo.SetActive(false);
    }

    /// <summary>
    /// 添加技能的小状态图标
    /// </summary>
    /// <param name="skill">Skill.</param>
    public void addSkillStatusIcon(Skill skill)
    {
        GameObject skillStatusIcon = Instantiate((GameObject)Resources.Load("UI/SkillStatusIcon"));
        skillStatusIcon.name = skill.id;
        skillStatusIcon.transform.SetParent(skillStatusBar.transform, false);

        Image icon = skillStatusIcon.transform.Find("Icon").GetComponent<Image>();
        icon.sprite = skill.imageSprite;

        UIMouseDelegate mouseDelegate = skillStatusIcon.GetComponent<UIMouseDelegate>();
        mouseDelegate.onPointerEnterDelegate = onPointerEnterSkillStatus;
        mouseDelegate.onPointerExitDelegate = onPointerExitSkillStatus;
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
