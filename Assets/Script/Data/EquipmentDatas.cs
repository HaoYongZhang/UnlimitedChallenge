using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EquipmentClass;
using Utility;

public class EquipmentDatas
{
    string path = Application.dataPath + "/Resources/Data/Equipment";

    public Dictionary<EquipmentPart, CSVDataStruct> data = new Dictionary<EquipmentPart, CSVDataStruct>();
    public CSVDataStruct propertysData = new CSVDataStruct();

    public EquipmentDatas()
    {
        loadData();
    }

    void loadData()
    {
        List<List<string>> csvData = CSV.Instance.loadFile(path, "equipment_property.csv");
        List<string> head = csvData[0];
        csvData.RemoveAt(0);
        List<List<string>> dataList = csvData;
        propertysData = new CSVDataStruct(head, dataList);

        //武器数据
        List<List<string>> csvData_1 = CSV.Instance.loadFile(path, "equipment_weapon.csv");
        List<string> head_1 = csvData_1[0];
        csvData_1.RemoveAt(0);
        List<List<string>> dataList_1 = csvData_1;
        data.Add(EquipmentPart.weapon, new CSVDataStruct(head_1, dataList_1));

        //头部数据
        List<List<string>> csvData_2 = CSV.Instance.loadFile(path, "equipment_head.csv");
        List<string> head_2 = csvData_2[0];
        csvData_2.RemoveAt(0);
        List<List<string>> dataList_2 = csvData_2;
        data.Add(EquipmentPart.head, new CSVDataStruct(head_2, dataList_2));
    }

    public Dictionary<string, string> getEquipmentData(string id)
    {
        EquipmentPart part = PropertyUtil.GetEnum<EquipmentPart>(id.Substring(0, 1));
        CSVDataStruct csvStruct = data[part];

        Dictionary<string, string> equipmentData = new Dictionary<string, string>();

        for (int i = 0; i < csvStruct.dataList.Count; i++)
        {
            if (csvStruct.dataList[i][0] == id)
            {
                for (int j = 0; j < csvStruct.dataList[i].Count; j++)
                {
                    equipmentData.Add(csvStruct.head[j], csvStruct.dataList[i][j]);
                }
                break;
            }
        }

        for (int i = 0; i < propertysData.dataList.Count; i++)
        {
            if (propertysData.dataList[i][0] == id)
            {
                for (int j = 0; j < propertysData.dataList[i].Count; j++)
                {
                    Debug.Log(propertysData.head[j]);
                    //因为上表已经存在id字段了，字典key值不能重复
                    if (propertysData.head[j] != "id")
                    {
                        //筛选空的属性，减少使用时的历遍
                        if (propertysData.dataList[i][j] != "")
                        {
                            equipmentData.Add(propertysData.head[j], propertysData.dataList[i][j]);
                        }
                    }
                }
                break;
            }
        }

        foreach(KeyValuePair<string, string> pair in equipmentData)
        {
            Debug.Log(pair.Key + "===" + pair.Value);
        }

        return equipmentData;
    }
}
