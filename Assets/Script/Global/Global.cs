using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SkillClass;
public class Global{
    /// <summary>
    /// 拥有的技能列表
    /// </summary>
    public static List<Skill> skills = new List<Skill>();

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


    public static SkillRelease skillRelease = SkillRelease.none;

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
        Object.DontDestroyOnLoad(gameObject);

        //设置主摄像头
        Camera _mainCamera = gameObject.AddComponent<Camera> ();
		_mainCamera.tag = "MainCamera";
		_mainCamera.gameObject.AddComponent<HeroCamera> ();

		//设置场景UI
		SceneUI.Instance.transform.parent = _mainCamera.transform;
	}
}