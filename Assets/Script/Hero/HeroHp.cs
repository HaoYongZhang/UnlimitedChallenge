using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroHp : MonoBehaviour {
	public Slider hp;
	public Text hpText;

	private float width = 120;
	private float height = 15;

	private float valueMax = 100;
	private float value = 100;

	void Start () {
		GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
	}
	

	void Update () {
        if (Input.GetKeyDown(KeyCode.E))
        {
			value -= 10;
		}
	}

	void FixedUpdate()
	{
//		hpText.text = value + "/" + valueMax;
//		hp.value = value/valueMax;
	}
}
