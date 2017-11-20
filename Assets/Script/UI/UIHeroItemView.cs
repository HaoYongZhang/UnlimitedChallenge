using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EquipmentClass;
using UnityEngine.EventSystems;

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

    //拖动物品时的临时创建对象
    GameObject dragTempObject;

	// Use this for initialization
	void Start () {
        //创建装备按钮
        leftWeapon = EquipmentButton.NewInstantiate(EquipmentPart.weapon);
        rightWeapon = EquipmentButton.NewInstantiate(EquipmentPart.weapon);
        head = EquipmentButton.NewInstantiate(EquipmentPart.head);
        body = EquipmentButton.NewInstantiate(EquipmentPart.body);
        legs = EquipmentButton.NewInstantiate(EquipmentPart.legs);
        treasure = EquipmentButton.NewInstantiate(EquipmentPart.treasure);

        //设置装备拖拽的代理
        leftWeapon.gameObject.GetComponent<UIMouseDelegate>().onDropDelegate = onDropSkill;
        rightWeapon.gameObject.GetComponent<UIMouseDelegate>().onDropDelegate = onDropSkill;
        head.gameObject.GetComponent<UIMouseDelegate>().onDropDelegate = onDropSkill;
        body.gameObject.GetComponent<UIMouseDelegate>().onDropDelegate = onDropSkill;
        legs.gameObject.GetComponent<UIMouseDelegate>().onDropDelegate = onDropSkill;
        treasure.gameObject.GetComponent<UIMouseDelegate>().onDropDelegate = onDropSkill;

        //添加装备文本
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

            UIMouseDelegate mouseDelegate = equipmentButton.gameObject.GetComponent<UIMouseDelegate>();
            //mouseDelegate.onPointerClickDelegate = Global.hero.skillManager.onClickSkillButton;
            //mouseDelegate.onPointerEnterDelegate = UIScene.Instance.onPointerEnterSkillButton;
            //mouseDelegate.onPointerExitDelegate = UIScene.Instance.onPointerExitSkillButton;
            mouseDelegate.onBeginDragDelegate = onBeginDragSkillButton;
            mouseDelegate.onDragDelegate = onDragSkillButton;
            mouseDelegate.onEndDragDelegate = onEndDragSkillButton;
        }
    }

    void onBeginDragSkillButton(GameObject obj, PointerEventData eventData)
    {
        //代替品实例化
        dragTempObject = new GameObject("DragTempObject");
        dragTempObject.transform.SetParent(UIScene.Instance.sceneProperty.transform, false);
        dragTempObject.AddComponent<RectTransform>();

        EquipmentButton temp = EquipmentButton.NewInstantiate();
        temp.transform.SetParent(dragTempObject.transform, false);
        temp.setEquipment(obj.GetComponentInChildren<EquipmentButton>().equipment);

        //防止拖拽结束时，代替品挡住了准备覆盖的对象而使得 OnDrop（） 无效
        CanvasGroup group = dragTempObject.AddComponent<CanvasGroup>();
        group.blocksRaycasts = false;
    }

    void onDragSkillButton(GameObject obj, PointerEventData eventData)
    {
        //并将拖拽时的坐标给予被拖拽对象的代替品

        Vector3 movePosition = new Vector3(Input.mousePosition.x + 20, Input.mousePosition.y - 20);

        dragTempObject.transform.position = movePosition;
    }

    void onEndDragSkillButton(GameObject obj, PointerEventData eventData)
    {
        //拖拽结束，销毁代替品
        if (dragTempObject)
        {
            Destroy(dragTempObject);
        }
    }


    void onDropSkill(GameObject obj, PointerEventData eventData)
    {
        GameObject dropObj = eventData.pointerDrag;

        Equipment replaceEquipment = dropObj.GetComponent<EquipmentButton>().equipment;
        Equipment oldEquipment = obj.GetComponent<EquipmentButton>().equipment;

        if(replaceEquipment.part != obj.GetComponent<EquipmentButton>().part)
        {
            return;
        }

        obj.GetComponent<EquipmentButton>().setEquipment(replaceEquipment);
    }
}
