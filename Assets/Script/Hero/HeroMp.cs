using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroMp : MonoBehaviour {
	public Slider mp;
	public Text mpText;

	private float width = 120;
	private float height = 15;

	private float valueMax = 100;
	private float value = 100;

	void Start () {
		GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
	}


	void Update () {
		if(Input.GetKeyDown(KeyCode.Q))
		{
			value -= 10;
		}
	}

	void FixedUpdate()
	{
		mpText.text = value + "/" + valueMax;
		mp.value = value/valueMax;
	}
}
