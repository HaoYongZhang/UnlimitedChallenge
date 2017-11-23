using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SkillClass;
public class SkillDatas
{
    string path = Application.dataPath + "/Resources/Data/Skill";

    public List<string> propertysHead = new List<string>();
    public List<List<string>> propertysData = new List<List<string>>();

    public SkillDatas()
    {
        loadProperty();
    }

    void loadProperty()
    {
        List<List<string>> data = CSV.Instance.loadFile(path, "skill_property.csv");
        propertysHead = data[0];
        data.RemoveAt(0);
        propertysData = data;
    }
}