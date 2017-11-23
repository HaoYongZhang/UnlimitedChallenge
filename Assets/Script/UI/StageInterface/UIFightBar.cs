using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EquipmentClass;

public class UIFightBar : MonoBehaviour {
    public Equipment currentWeapon;
    public Image weaponImage;
    public Image weaponBG;
    public WeaponType weaponType;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setCurrentWeapon(Equipment _equipment)
    {
        if(_equipment != null)
        {
            currentWeapon = _equipment;
            weaponImage.sprite = _equipment.imageSprite;
        }
        else
        {
            currentWeapon = Global.hero.equipmentManager.defaultWeapon;
            weaponImage.sprite = Global.hero.equipmentManager.defaultWeapon.imageSprite;
        }
    }
}
