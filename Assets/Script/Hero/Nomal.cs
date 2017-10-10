using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nomal: Property
{
	public static Nomal _instance;

	void Awake()
	{
		_instance = this;
		setDefault ();

	}

	void setDefault()
	{
		strength = 20;
		agility = 20;
		intellect = 20;

		basMoveSpeed = 60;
		Debug.Log (this.hpRegeneration);
	}
}