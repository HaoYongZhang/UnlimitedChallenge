using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SkillClass;
using UnityEngine.EventSystems;
using UIHeroInfo;
using UIHeroStatus;
using UIHotKeyBar;
using UISkillInfo;
using DG.Tweening;

public class UIScene : MonoBehaviour
{
    static UIScene _instance;

    public UIHotKeyBar.MainView hotKeyBar;
    public UISkillInfo.MainView skillInfoView;
    public UIHeroInfo.MainView heroInfoView;
    public UIHeroStatus.MainView statusMainView;
    public UIFightInfo.MainView fightInfoView;

    /// <summary>
    /// 单例
    /// </summary>
    public static UIScene Instance
    {
        get
        {
            if (_instance == null)  // 如果没有找到
            {
                GameObject obj = Instantiate((GameObject)Resources.Load("UI/UISceneCanvas"));
                obj.name = "UISceneCanvas";
                _instance = obj.GetComponent<UIScene>();
            }
            return _instance;
        }
    }

    private UIScene()
    {

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
        
    }

    /// <summary>
    /// 设置快捷释放栏
    /// </summary>
    /// <param name="tag">Tag.</param>
    /// <param name="skill">Skill.</param>
    public void setHotKey(int tag, Skill skill)
    {
        hotKeyBar.setHotKey(tag, skill);
    }

    /// <summary>
    /// 切换快捷释放栏
    /// </summary>
    public void switchHotKeyBar()
    {
        hotKeyBar.switchBar();
    }

    /// <summary>
    /// 鼠标移动到技能栏的按钮
    /// </summary>
    /// <param name="obj">Object.</param>
    public void pointToSkillButton(GameObject obj, PointerEventData eventData)
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
    public void pointOutSkillButton(GameObject obj, PointerEventData eventData)
    {
        hideSkillInfo();
    }

    /// <summary>
    /// 鼠标移到状态图标
    /// </summary>
    /// <param name="obj">Object name.</param>
    public void pointToStatusIcon(GameObject obj, PointerEventData eventData)
    {
        UIHeroStatus.StatusIcon statusIcon = obj.GetComponent<StatusIcon>();

        foreach (Skill skill in Global.skills)
        {
            if (skill.id == statusIcon.id)
            {
                showSkillInfo(skill);
            }
        }
    }

    /// <summary>
    /// 鼠标移出状态图标
    /// </summary>
    /// <param name="obj">Object name.</param>
    public void pointOutStatusIcon(GameObject obj, PointerEventData eventData)
    {
        hideSkillInfo();
    }


    /// <summary>
    /// 显示技能的详细信息
    /// </summary>
    /// <param name="skill">Skill.</param>
    public void showSkillInfo(Skill skill)
    {
        skillInfoView.showInfo(skill);
    }

    /// <summary>
    /// 隐藏技能的详细信息
    /// </summary>
    public void hideSkillInfo()
    {
        skillInfoView.hideInfo();
    }

    /// <summary>
    /// 添加技能的小状态图标
    /// </summary>
    /// <param name="skill">Skill.</param>
    public void addStatusIcon(Skill skill)
    {
        statusMainView.statusIconsView.addStatusIcon(skill);
    }

    /// <summary>
    /// 移除技能的小状态图标
    /// </summary>
    /// <param name="skill">Skill.</param>
    public void removeStatusIcon(Skill skill)
    {
        statusMainView.statusIconsView.removeStatusIcon(skill);

        //如果当前正在显示技能状态的详细信息时，关闭
        if (skillInfoView.currentShowInfoSkill != null)
        {
            if (skillInfoView.currentShowInfoSkill.id == skill.id)
            {
                skillInfoView.currentShowInfoSkill = null;
                hideSkillInfo();
            }
        }
    }
}
