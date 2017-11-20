using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EquipmentClass;

public class UIFightBar : MonoBehaviour {
    Equipment weapon;
    public Image weaponImage;
    public Image weaponBG;
    public WeaponType weaponType;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate(){
        //Equipment wearsEquipment = UIScene.Instance.sceneProperty.GetComponent<UIHeroView>().itemsView.GetComponent<UIHeroItemView>().leftWeapon.equipment;

        //if(weapon != wearsEquipment)
        //{
        //    weapon = wearsEquipment;
        //    if(weapon != null)
        //    {
        //        weaponImage.sprite = wearsEquipment.imageSprite;
        //    }
        //}
    }
}
