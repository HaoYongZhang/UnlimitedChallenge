using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EquipmentClass;

public class UIFightBar : MonoBehaviour {
    public Image weaponImage;
    public Image weaponBG;
    public WeaponType weaponType;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setWeaponBG(Equipment _equipment)
    {
        if(_equipment != null)
        {
            weaponImage.sprite = _equipment.imageSprite;
            weaponBG.sprite = Global.ranks[_equipment.data["rank"]].image;
        }
        else
        {
            weaponImage.sprite = Global.hero.equipmentManager.defaultWeapon.imageSprite;
            weaponBG.sprite = Global.ranks[Global.hero.equipmentManager.defaultWeapon.data["rank"]].image;
        }
    }
}
