using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataManager
{
    static DataManager _instance;
    public static SkillDatas skillDatas;

    public static DataManager Instance
    {
        get
        {
            if (_instance == null)  // 如果没有找到
            {
                skillDatas = new SkillDatas();
            }
            return _instance;
        }
    }




}
