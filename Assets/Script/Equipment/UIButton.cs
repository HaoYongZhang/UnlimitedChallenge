using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EquipmentClass;

namespace EquipmentClass
{
	public class UIButton : MonoBehaviour
	{

		public Equipment equipment;
		public EquipmentPart part;
		public Image icon;
        public Image bg;
        public int number;

		Sprite defaultIcon;
        Sprite defaultBG;

		public static UIButton NewInstantiate()
		{
			UIButton equipmentButton = Instantiate((GameObject)Resources.Load("UI/EquipmentButton")).GetComponent<UIButton>();

			return equipmentButton;
		}

		public static UIButton NewInstantiate(Equipment _equipment)
		{
			UIButton equipmentButton = NewInstantiate();
			equipmentButton.equipment = _equipment;
            equipmentButton.part = _equipment.part;
			equipmentButton.icon.sprite = _equipment.imageSprite;
            equipmentButton.bg.sprite = Global.ranks[_equipment.data["rank"]].image;

			return equipmentButton;
		}

		public static UIButton NewInstantiate(EquipmentPart _part)
		{
			UIButton equipmentButton = NewInstantiate();
			equipmentButton.part = _part;
			equipmentButton.defaultIcon = equipmentButton.getPartDefaultImage(_part);
            equipmentButton.defaultBG = equipmentButton.bg.sprite;
			equipmentButton.icon.sprite = equipmentButton.defaultIcon;

			return equipmentButton;
		}


		// Use this for initialization
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}

		void FixedUpdate()
		{
			
		}

		Sprite getPartDefaultImage(EquipmentPart _part)
		{
			string equipmentPath = "";

			switch (_part)
			{
				case EquipmentPart.weapon:
					{
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

		public void setEquipment(Equipment _equipment)
		{
			equipment = _equipment;

            if(_equipment != null)
            {
                icon.sprite = _equipment.imageSprite;
                bg.sprite = Global.ranks[_equipment.data["rank"]].image;
            }
            else
            {
                icon.sprite = defaultIcon;
                bg.sprite = defaultBG;
            }
		
		}
	}
}

