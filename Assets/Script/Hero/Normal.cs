using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillClass;

public class Normal
{
	public Property property = new Property();

    public string talentSkillID = "540001";

	public Normal()
	{
		property.basStrength = 10;
        property.basAgility = 10;
        property.basIntellect = 10;

		property.hp = property.hpMax;
		property.mp = property.mpMax;
		property.basMoveSpeed = 20;
	}
}