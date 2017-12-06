using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillClass;

public class Normal
{
	public Property property = new Property();
    public Knowledge knowledge = new Knowledge();

    public string talentSkillID = "340001";

	public Normal()
	{
		property.basStrength = 10;
        property.basAgility = 0;
        property.basIntellect = 10;

		property.hp = property.hpMax;
		property.mp = property.mpMax;
		property.basMoveSpeed = 20;

        knowledge.shot = 3;
	}
}