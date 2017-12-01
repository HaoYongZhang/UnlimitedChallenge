using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EquipmentClass;
using UnityEngine.EventSystems;

public class UIHeroItemView : MonoBehaviour {
    public GameObject equipmentPartView;
    public GameObject itemsSet;

    List<EquipmentClass.UIButton> equipmentSet
    {
        get
        {
            List<EquipmentClass.UIButton> list = new List<UIButton>();

            for (int i = 0; i < Global.equipmentButtons.Count; i++)
            {
                EquipmentClass.UIButton equipmentButton = Global.equipmentButtons[i];
                //获取没有装备的
                if (!equipmentButton.equipment.isWear)
                {
                    list.Add(equipmentButton);
                }
            }

            return list;
        }
    }

    //拖动物品时的临时创建对象
    GameObject dragTempObject;

	// Use this for initialization
	void Start () {

        Global.hero.equipmentManager.leftWeapon.transform.SetParent(equipmentPartView.transform, false);

        Global.hero.equipmentManager.head.transform.SetParent(equipmentPartView.transform, false);

        Global.hero.equipmentManager.body.transform.SetParent(equipmentPartView.transform, false);

        Global.hero.equipmentManager.legs.transform.SetParent(equipmentPartView.transform, false);

        Global.hero.equipmentManager.rightWeapon.transform.SetParent(equipmentPartView.transform, false);

        Global.hero.equipmentManager.treasure.transform.SetParent(equipmentPartView.transform, false);

        Global.hero.equipmentManager.treasure_2.transform.SetParent(equipmentPartView.transform, false);

        setItemsSet();
    }   
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setItemsSet()
    {
        for (int i = 0; i < Global.equipmentButtons.Count; i ++)
        {
            UIButton equipBtn = Global.equipmentButtons[i];
            equipBtn.gameObject.SetActive(!equipBtn.equipment.isWear);

            equipBtn.transform.SetParent(itemsSet.transform, false);
            UIMouseDelegate mouseDelegate = equipBtn.gameObject.GetComponent<UIMouseDelegate>();
            mouseDelegate.onBeginDragDelegate = onBeginDrag;
            mouseDelegate.onDragDelegate = onDrag;
            mouseDelegate.onEndDragDelegate = onEndDrag;
        }
    }

    void onBeginDrag(GameObject obj, PointerEventData eventData)
    {
        //代替品实例化
        dragTempObject = new GameObject("DragTempObject");
        dragTempObject.transform.SetParent(UIScene.Instance.heroView.transform, false);
        dragTempObject.AddComponent<RectTransform>();

        Equipment tempEquipment = new Equipment(obj.GetComponentInChildren<UIButton>().equipment.id);
        UIButton temp = EquipmentClass.UIButton.NewInstantiate(tempEquipment);
        temp.transform.SetParent(dragTempObject.transform, false);

        //防止拖拽结束时，代替品挡住了准备覆盖的对象而使得 OnDrop（） 无效
        CanvasGroup group = dragTempObject.AddComponent<CanvasGroup>();
        group.blocksRaycasts = false;
    }

    void onDrag(GameObject obj, PointerEventData eventData)
    {
        //并将拖拽时的坐标给予被拖拽对象的代替品
       
        Vector3 movePosition = new Vector3(Input.mousePosition.x + 20, Input.mousePosition.y - 20);

        dragTempObject.transform.position = movePosition;
    }

    void onEndDrag(GameObject obj, PointerEventData eventData)
    {
        //拖拽结束，销毁代替品
        if (dragTempObject)
        {
            Destroy(dragTempObject);
        }

        UIScene.Instance.heroView.itemsView.GetComponent<UIHeroItemView>().setItemsSet();
    }
}
