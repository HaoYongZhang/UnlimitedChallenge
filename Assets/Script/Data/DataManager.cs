using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct CSVDataStruct{
    public List<string> head;
    public List<List<string>> dataList;
    public CSVDataStruct(List<string> _head, List<List<string>> _dataList)
    {
        head = _head;
        dataList = _dataList;
    }
}

public class DataManager
{
    static DataManager _instance;
    public SkillDatas skillDatas;

    public static DataManager Instance
    {
        get
        {
            if (_instance == null)  // 如果没有找到
            {
                _instance = new DataManager();
                _instance.skillDatas = new SkillDatas();
            }
            return _instance;
        }
    }

    DataManager()
    {
        
    }


}
