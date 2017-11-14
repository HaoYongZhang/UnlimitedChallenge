﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EquipmentClass;

public class EquipmentButton : MonoBehaviour {

    public Equipment equipment;
    public EquipmentPart part;
    public Image icon;

    Sprite defaultIcon;

    public static EquipmentButton NewInstantiate()
    {
        EquipmentButton equipmentButton = Instantiate((GameObject)Resources.Load("UI/EquipmentButton")).GetComponent<EquipmentButton>();

        return equipmentButton;
    }

    public static EquipmentButton NewInstantiate(Equipment _equipment)
    {
        EquipmentButton equipmentButton = NewInstantiate();
        equipmentButton.equipment = _equipment;
        equipmentButton.icon.sprite = _equipment.imageSprite;

        return equipmentButton;
    }

    public static EquipmentButton NewInstantiate(EquipmentPart _part)
    {
        EquipmentButton equipmentButton = NewInstantiate();
        equipmentButton.part = _part;
        equipmentButton.defaultIcon = equipmentButton.getPartDefaultImage(_part);
        equipmentButton.icon.sprite = equipmentButton.defaultIcon;

        return equipmentButton;
    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        //当技能移除时
        if (equipment == null)
        {
            icon.sprite = defaultIcon;
        }

        //if (equipment != null)
        //{
        //    Equipment oneEquipment = EquipmentManager.GetOneSkillByID(equipment.id);
        //}
    }

    Sprite getPartDefaultImage(EquipmentPart _part){
        string equipmentPath = "";

        switch(_part)
        {
            case EquipmentPart.weapon:{
                    equipmentPath = "Sword-Icon";
                }
                break;
            case EquipmentPart.head:
                {
                    equipmentPath = "Head-Icon_2";
                }
                break;
            case EquipmentPart.body:
                {
                    equipmentPath = "Torso-Icon";
                }
                break;
            case EquipmentPart.legs:
                {
                    equipmentPath = "Legs-Icon";
                }
                break;
            case EquipmentPart.treasure:
                {
                    equipmentPath = "Amulet-Icon-Small";
                }
                break;
        }

        Sprite imageSprite = Resources.Load("Image/Equipment/" + equipmentPath, typeof(Sprite)) as Sprite;
        return imageSprite;
    }

    public void setEquipment(Equipment _equipment){
        equipment = _equipment;
        icon.sprite = _equipment.imageSprite;
    }
}