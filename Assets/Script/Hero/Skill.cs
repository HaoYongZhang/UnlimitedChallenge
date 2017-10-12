using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
	//id
	public int id;
	//名字
	public string name;
	//类型
	public SkillType type = 0;
	//图片
	public string imageName;
	//描述
	public string description;

	public Skill()
	{

	}
}

public enum SkillType
{
	//攻击
    Attack = 0,
    //防御
    Defense = 1,
    //治疗
    Treatment = 2,
    //强化
    Intensify = 3,
    //特殊
    Specialty = 4
}