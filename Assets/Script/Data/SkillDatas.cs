using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SkillClass;
using Utility;

public class SkillDatas
{
    string path = Application.dataPath + "/Resources/Data/Skill";

    public Dictionary<SkillType, CSVDataStruct> data = new Dictionary<SkillType, CSVDataStruct>();
    public CSVDataStruct propertysData = new CSVDataStruct();

    public SkillDatas()
    {
        loadData();
    }

    void loadData()
    {
        List<List<string>> csvData = CSV.Instance.loadFile(path, "skill_property.csv");
        List<string> head = csvData[0];
        csvData.RemoveAt(0);
        List<List<string>> dataList = csvData;
        propertysData = new CSVDataStruct(head, dataList);

        //攻击数据
        List<List<string>> csvData_1 = CSV.Instance.loadFile(path, "skill_attack.csv");
        List<string> head_1 = csvData_1[0];
        csvData_1.RemoveAt(0);
        List<List<string>> dataList_1 = csvData_1;
        data.Add(SkillType.attack, new CSVDataStruct(head_1, dataList_1));

        //治疗数据
        List<List<string>> csvData_3 = CSV.Instance.loadFile(path, "skill_treatment.csv");
        List<string> head_3 = csvData_3[0];
        csvData_3.RemoveAt(0);
        List<List<string>> dataList_3 = csvData_3;
        data.Add(SkillType.treatment, new CSVDataStruct(head_3, dataList_3));

        //强化数据
        List<List<string>> csvData_4 = CSV.Instance.loadFile(path, "skill_intensify.csv");
        List<string> head_4 = csvData_4[0];
        csvData_4.RemoveAt(0);
        List<List<string>> dataList_4 = csvData_4;
        data.Add(SkillType.intensify, new CSVDataStruct(head_4, dataList_4));

        foreach (KeyValuePair<SkillType, CSVDataStruct> pair in data)
        {
            Debug.Log(pair.Key);
            for (int i = 0; i < pair.Value.dataList.Count; i++)
            {
                Debug.Log(dataList[i][1]);
            }
        }
    }

    public Dictionary<string, string> getSkillData(string id)
    {
        
        SkillType type = PropertyUtil.GetEnum<SkillType>(id.Substring(1, 1));
        CSVDataStruct csvStruct = data[type];
        Debug.Log(type);
        Dictionary<string, string> skillData = new Dictionary<string, string>();

        for (int i = 0; i < csvStruct.dataList.Count; i++)
        {
            Debug.Log(csvStruct.dataList[i][0]);
            if (csvStruct.dataList[i][0] == id)
            {
                Debug.Log("进入");
                for(int j = 0; j < csvStruct.dataList[i].Count; j++)
                {
                    skillData.Add(csvStruct.head[j], csvStruct.dataList[i][j]);
                }
                break;
            }
        }

        foreach(KeyValuePair<string, string> pair in skillData)
        {
            Debug.Log(pair.Key + "===" + pair.Value);
        }

        return skillData;
    }
}