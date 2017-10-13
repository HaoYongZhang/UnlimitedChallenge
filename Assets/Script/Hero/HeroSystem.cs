using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class HeroSystem : MonoBehaviour {
	public static HeroSystem _instance;
	public Normal normal = new Normal();
	public Property property;

	void Awake()
	{
		_instance = this;
	}

	void Start()
	{
		this.InvokeRepeating ("RegenerationPerSecond", 0, 1f);
		property = normal.property;
	}
		
	// Update is called once per frame
	void Update () {	
		
	}

	void RegenerationPerSecond(){
		//当生命值不是最大值时
		if (property.hp != property.hpMax) {

			//当回复生命值后将会溢出最大值时
			if (Math.Round (property.hp + property.hpRegeneration, 1) > property.hpMax) 
			{
				property.hp = property.hpMax;
			}
			else
			{
				property.hp = Math.Round (property.hp + property.hpRegeneration, 1);
			}
		}

		//当能量值不是最大值时
		if (property.mp != property.mpMax) {

			//当回复能量值后将会溢出最大值时
			if (Math.Round (property.mp + property.mpRegeneration, 1) > property.mpMax) 
			{
				property.mp = property.mpMax;
			}
			else
			{
				property.mp = Math.Round (property.mp + property.mpRegeneration, 1);
			}
		}
	}
}
