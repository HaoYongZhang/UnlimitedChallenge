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
        Global.hero.equipmentManager.leftWeapon.transform.SetParent(equipmentLeftView.transform, false);
        leftWeaponText.transform.SetParent(equipmentLeftView.transform, false);

        Global.hero.equipmentManager.head.transform.SetParent(equipmentLeftView.transform, false);
        headText.transform.SetParent(equipmentLeftView.transform, false);

        Global.hero.equipmentManager.treasure.transform.SetParent(equipmentLeftView.transform, false);
        treasureText.transform.SetParent(equipmentLeftView.transform, false);

        //右边
        Global.hero.equipmentManager.rightWeapon.transform.SetParent(equipmentRightView.transform, false);
        rightWeaponText.transform.SetParent(equipmentRightView.transform, false);

        Global.hero.equipmentManager.body.transform.SetParent(equipmentRightView.transform, false);
        bodyText.transform.SetParent(equipmentRightView.transform, false);

        Global.hero.equipmentManager.legs.transform.SetParent(equipmentRightView.transform, false);
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
            EquipmentClass.UIButton equipmentButton = EquipmentClass.UIButton.NewInstantiate(equipment);
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
        dragTempObject.transform.SetParent(UIScene.Instance.heroView.transform, false);
        dragTempObject.AddComponent<RectTransform>();

        EquipmentClass.UIButton temp = EquipmentClass.UIButton.NewInstantiate();
        temp.transform.SetParent(dragTempObject.transform, false);
        temp.setEquipment(obj.GetComponentInChildren<EquipmentClass.UIButton>().equipment);

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
}
