using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHeroItemView : MonoBehaviour {
    public Button leftWeapon;
    public Button rightWeapon;
    public Button head;
    public Button body;
    public Button legs;
    public Button treasure;

    public Text leftWeaponText;
    public Text rightWeaponText;
    public Text headText;
    public Text bodyText;
    public Text legsText;
    public Text treasureText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

public struct EquipmentButtonStruct
{
    public Button btn;
    public Text txt;

    public EquipmentButtonStruct(Button _btn,Text _txt)
    {
        btn = _btn;
        txt = _txt;
    }
}
