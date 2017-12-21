using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SkillClass;
using EquipmentClass;

public class Global{
    public static Hero hero{
        get
        {
            return Hero.Instance;
        }
    }

    public static bool faceToMousePosition(GameObject self)
    {
        Ray mouseRay = Global.mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(mouseRay, out hitInfo))
        {
            Vector3 targerPosition = new Vector3(hitInfo.point.x, self.transform.position.y, hitInfo.point.z);
            self.transform.LookAt(targerPosition);

            return true;
        }

        return false;
    }

    public static Camera mainCamera;

    public static Dictionary<string, Rank> ranks = new Dictionary<string, Rank>();

    /// <summary>
    /// 拥有的技能列表
    /// </summary>
    public static List<Skill> skills = new List<Skill>();

    /// <summary>
    /// 快捷技能栏1
    /// </summary>
    public static List<Skill> shortcutSkills_1 = new List<Skill>(5){null, null, null, null, null};

    /// <summary>
    /// 快捷技能栏2
    /// </summary>
    public static List<Skill> shortcutSkills_2 = new List<Skill>(5){null, null, null, null, null};

    /// <summary>
    /// 拥有的主动技能列表
    /// </summary>
    /// <value>The active skills.</value>
    public static List<Skill> activeSkills
    {
        get{
            List<Skill> activeSkillList = new List<Skill>();
            foreach(Skill skill in Global.skills)
            {
                if(skill.data["isActive"] == "1")
                {
                    activeSkillList.Add(skill);
                }
            }

            return activeSkillList;
        }
    }

    /// <summary>
    /// 拥有的被动技能列表
    /// </summary>
    /// <value>The active skills.</value>
    public static List<Skill> unactiveSkills
    {
        get
        {
            List<Skill> unactiveSkillList = new List<Skill>();
            foreach (Skill skill in Global.skills)
            {
                if (skill.data["isActive"] == "0")
                {
                    unactiveSkillList.Add(skill);
                }
            }

            return unactiveSkillList;
        }
    }

    /// <summary>
    /// 技能释放状态
    /// </summary>
    public static SkillReleaseState skillReleaseState
    {
        get
        {
            SkillReleaseState state = SkillReleaseState.available;
            //循环当前技能列表，当有技能处于选择目标状态时
            for (int i = 0; i < Global.skills.Count; i ++)
            {
                if(skills[i].releaseState == SkillReleaseState.selecting)
                {
                    state = SkillReleaseState.selecting;
                    break;
                }
            }
            return state;
        }
    }

    public static Skill FindSkillInSkills(string _id)
    {
        foreach(Skill skill in Global.skills)
        {
            if(skill.id == _id)
            {
                return skill;
            }
        }

        return null;
    }

    /// <summary>
    /// 所有物品
    /// </summary>
    public static List<System.Object> items = new List<System.Object>();

    public static List<EquipmentClass.UIButton> equipmentButtons
    {
        get{
            
            List <EquipmentClass.UIButton> list = new List<EquipmentClass.UIButton>();

            foreach(System.Object o in items)
            {
                if(o.GetType().ToString() == "EquipmentClass.UIButton")
                {
                    list.Add((EquipmentClass.UIButton)o);
                }
            }

            return list;
        }
    }

    // 记录人物进入场景前的位置数组
	public static List<Vector3> enterSceneBeforPositions = new List<Vector3>();
    // 人物面向的方向
    // 0 = 正面
    // 1 = 左面
    // 2 = 后面
    // 3 = 右面
    public static int heroDirection = 0;

    //获取屏幕宽度和高度
    public static float screenWidth = Screen.width;
    public static float screenHeight = Screen.height;

    /// <summary>
    /// 设置场景的通用组件
    /// </summary>
    /// <returns>The scene common.</returns>
    public static void setSceneCommonComponent(){
        
        GameObject gameObject = new GameObject();
        gameObject.name = "SceneCommonComponent";
        UnityEngine.Object.DontDestroyOnLoad(gameObject);

        //设置主摄像头
        Global.mainCamera = gameObject.AddComponent<Camera> ();
        Global.mainCamera.tag = "MainCamera";
        Global.mainCamera.gameObject.AddComponent<HeroCamera> ();

        //设置敌方UI
        UIEnemy.Instance.transform.SetParent(Global.mainCamera.transform, false);
        //设置场景UI
        UIScene.Instance.transform.SetParent(Global.mainCamera.transform, false);
	}
}