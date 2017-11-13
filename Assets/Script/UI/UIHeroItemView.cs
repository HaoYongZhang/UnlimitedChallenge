using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EquipmentClass;

public class UIHeroItemView : MonoBehaviour {
    public GameObject equipmentLeftView;
    public GameObject equipmentRightView;
    public GameObject itemsSet;

    public EquipmentButton leftWeapon;
    public EquipmentButton rightWeapon;
    public EquipmentButton head;
    public EquipmentButton body;
    public EquipmentButton legs;
    public EquipmentButton treasure;

    public Text leftWeaponText;
    public Text rightWeaponText;
    public Text headText;
    public Text bodyText;
    public Text legsText;
    public Text treasureText;

    List<string> equipmentNameList = new List<string>{"主武器", "副武器", "头部", "上半身", "下半身", "宝物"};
    List<Text> equipmentTextList = new List<Text>();

	// Use this for initialization
	void Start () {
        leftWeapon = EquipmentButton.NewInstantiate(EquipmentPart.weapon);
        rightWeapon = EquipmentButton.NewInstantiate(EquipmentPart.weapon);
        head = EquipmentButton.NewInstantiate(EquipmentPart.head);
        body = EquipmentButton.NewInstantiate(EquipmentPart.body);
        legs = EquipmentButton.NewInstantiate(EquipmentPart.legs);
        treasure = EquipmentButton.NewInstantiate(EquipmentPart.treasure);

        leftWeaponText = new GameObject().AddComponent<Text>();
        rightWeaponText = new GameObject().AddComponent<Text>();
        headText = new GameObject().AddComponent<Text>();
        bodyText = new GameObject().AddComponent<Text>();
        legsText = new GameObject().AddComponent<Text>();
        treasureText = new GameObject().AddComponent<Text>();

        equipmentTextList.Add(leftWeaponText);
        equipmentTextList.Add(rightWeaponText);
        equipmentTextList.Add(headText);
        equipmentTextList.Add(bodyText);
        equipmentTextList.Add(legsText);
        equipmentTextList.Add(treasureText);

        for (int i = 0; i < equipmentTextList.Count; i++)
        {
            equipmentTextList[i].rectTransform.sizeDelta = new Vector2(120, 30);
            equipmentTextList[i].text = equipmentNameList[i];
            equipmentTextList[i].font = UIScene.Instance.skillInfo.GetComponentsInChildren<Text>()[1].font;
            equipmentTextList[i].fontSize = 25;
            equipmentTextList[i].alignment = TextAnchor.MiddleCenter;
            equipmentTextList[i].color = new Color(255f, 255f, 255f);
        }

        //左边
        leftWeapon.transform.SetParent(equipmentLeftView.transform, false);
        leftWeaponText.transform.SetParent(equipmentLeftView.transform, false);

        head.transform.SetParent(equipmentLeftView.transform, false);
        headText.transform.SetParent(equipmentLeftView.transform, false);

        treasure.transform.SetParent(equipmentLeftView.transform, false);
        treasureText.transform.SetParent(equipmentLeftView.transform, false);

        //右边
        rightWeapon.transform.SetParent(equipmentRightView.transform, false);
        rightWeaponText.transform.SetParent(equipmentRightView.transform, false);

        body.transform.SetParent(equipmentRightView.transform, false);
        bodyText.transform.SetParent(equipmentRightView.transform, false);

        legs.transform.SetParent(equipmentRightView.transform, false);
        legsText.transform.SetParent(equipmentRightView.transform, false);

        setItemsSet();
    }   
	
	// Update is called once per frame
	void Update () {
		
	}

    void setItemsSet()
    {
        foreach (Equipment equipment in Global.equipments)
        {
            EquipmentButton equipmentButton = EquipmentButton.NewInstantiate(equipment);
            equipmentButton.transform.SetParent(itemsSet.transform, false);

            //UIMouseDelegate mouseDelegate = equipmentButton.gameObject.GetComponent<UIMouseDelegate>();
            //mouseDelegate.onPointerClickDelegate = Global.hero.skillManager.onClickSkillButton;
            //mouseDelegate.onPointerEnterDelegate = UIScene.Instance.onPointerEnterSkillButton;
            //mouseDelegate.onPointerExitDelegate = UIScene.Instance.onPointerExitSkillButton;
            //mouseDelegate.onBeginDragDelegate = onBeginDragSkillButton;
            //mouseDelegate.onDragDelegate = onDragSkillButton;
            //mouseDelegate.onEndDragDelegate = onEndDragSkillButton;
        }
    }
}
