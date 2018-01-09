using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SkillClass;

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

        //强化数据
        List<List<string>> csvData_6 = CSV.Instance.loadFile(path, "skill_specialty.csv");
        List<string> head_6 = csvData_6[0];
        csvData_6.RemoveAt(0);
        List<List<string>> dataList_6 = csvData_6;
        data.Add(SkillType.specialty, new CSVDataStruct(head_6, dataList_6));
    }

    public Dictionary<string, string> getSkillData(string id)
    {
        
        SkillType type = EnumTool.GetEnum<SkillType>(id.Substring(0, 1));
        CSVDataStruct csvStruct = data[type];
        Dictionary<string, string> skillData = new Dictionary<string, string>();

        for (int i = 0; i < csvStruct.dataList.Count; i++)
        {
            if (csvStruct.dataList[i][0] == id)
            {
                for(int j = 0; j < csvStruct.dataList[i].Count; j++)
                {
                    skillData.Add(csvStruct.head[j], csvStruct.dataList[i][j]);
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
                    //因为上表已经存在id字段了，字典key值不能重复
                    if(propertysData.head[j] != "id" && propertysData.head[j] != "mName")
                    {
                        //筛选空的属性，减少使用时的历遍
                        if(propertysData.dataList[i][j] != "" && propertysData.dataList[i][j] != "0")
                        {
                            skillData.Add(propertysData.head[j], propertysData.dataList[i][j]);
                        }
                    }
                }
                break;
            }
        }

        //foreach(KeyValuePair<string, string> pair in skillData)
        //{
        //    Debug.Log(pair.Key + "===" + pair.Value);
        //}

        return skillData;
    }
}