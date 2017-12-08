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
    public EquipmentDatas equipmentDatas;
    public EnemyDatas enemyDatas;

    public static DataManager Instance
    {
        get
        {
            if (_instance == null)  // 如果没有找到
            {
                _instance = new DataManager();
            }
            return _instance;
        }
    }

    DataManager()
    {
        
    }

    public void loadCSVData(){
        skillDatas = new SkillDatas();
        equipmentDatas = new EquipmentDatas();
        enemyDatas = new EnemyDatas();
    }

}
