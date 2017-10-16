using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normal
{
	public Property property = new Property();

	public Normal()
	{
		property.basStrength = 20;
		property.basAgility = 20;
		property.basIntellect = 20;

		property.hp = property.hpMax;
		property.mp = property.mpMax;
		property.basMoveSpeed = 20;
	}
}