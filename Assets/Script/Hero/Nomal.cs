using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nomal: MonoBehaviour
{
	public Property property = new Property();
	public static Nomal _instance;

	void Awake()
	{
		_instance = this;
		setDefault ();
	}

	void Start()
	{
		this.InvokeRepeating ("regenerationPerSecond", 0, 1f);
	}

	void setDefault()
	{
		property.strength = 20;
		property.agility = 20;
		property.intellect = 20;

		property.hp = property.hpMax;
		property.mp = property.mpMax;
		property.basMoveSpeed = 60;
		Debug.Log (this.property.hpMax);
	}

	void FixedUpdate(){
		

	}


	void regenerationPerSecond(){
		//当生命值不是最大值时
		if (property.hp != property.hpMax) {
			if ((property.hp += property.hpRegeneration) > property.hpMax) 
			{
				property.hp = property.hpMax;
			}
			else
			{
				property.hp += property.hpRegeneration;
			}
		}
	}
}