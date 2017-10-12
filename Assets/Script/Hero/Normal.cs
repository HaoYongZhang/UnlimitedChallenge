using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normal
{
	public Property property = new Property();

	public Normal()
	{
		property.strength = 20;
		property.agility = 20;
		property.intellect = 20;

		property.hp = property.hpMax;
		property.mp = property.mpMax;
		property.basMoveSpeed = 20;
	}
}