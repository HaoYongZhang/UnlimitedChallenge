using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EquipmentClass;
using Utility;

public class EnemyDatas
{
    string path = Application.dataPath + "/Resources/Data/Enemy";

    public Dictionary<EquipmentPart, CSVDataStruct> data = new Dictionary<EquipmentPart, CSVDataStruct>();
    public CSVDataStruct propertysData = new CSVDataStruct();

    public EnemyDatas()
    {
        loadData();
    }

    void loadData()
    {
        List<List<string>> csvData = CSV.Instance.loadFile(path, "enemy_property.csv");
        List<string> head = csvData[0];
        csvData.RemoveAt(0);
        List<List<string>> dataList = csvData;
        propertysData = new CSVDataStruct(head, dataList);
    }

    public Dictionary<string, string> getData(string id)
    {
        Dictionary<string, string> enemyData = new Dictionary<string, string>();

        for (int i = 0; i < propertysData.dataList.Count; i++)
        {
            if (propertysData.dataList[i][0] == id)
            {
                for (int j = 0; j < propertysData.dataList[i].Count; j++)
                {
                    //筛选空的属性，减少使用时的历遍
                    if (propertysData.dataList[i][j] != "" && propertysData.dataList[i][j] != "0")
                    {
                        enemyData.Add(propertysData.head[j], propertysData.dataList[i][j]);
                    }
                }
                break;
            }
        }

        //foreach(KeyValuePair<string, string> pair in equipmentData)
        //{
        //    Debug.Log(pair.Key + "===" + pair.Value);
        //}

        return enemyData;
    }
}
