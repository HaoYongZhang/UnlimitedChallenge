using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
	//id
	public string id;
	//名字
	public string name;
	//类型
	public string type;
	//图片
	public string imageName;
	//描述
	public string description;

	public Skill(Dictionary<string, string> dict)
	{
		this.id = dict["id"];
		this.name = dict["name"];
		this.type = dict["type"];
		this.imageName = dict["imageName"];
		this.description = dict["description"];
	}
}