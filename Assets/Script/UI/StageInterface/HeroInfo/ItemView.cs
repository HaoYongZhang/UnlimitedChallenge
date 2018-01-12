using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EquipmentClass;
using UnityEngine.EventSystems;

namespace UIHeroInfo
{
    public class ItemView : MonoBehaviour
    {
        public GameObject equipmentPartView;
        public GameObject itemsSet;
        public List<UIButton> equipmentButtons = new List<UIButton>();

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
        //判断能否执行拖拽方法
        bool canDrag;


        void Awake()
        {
            List<EquipmentPart> equipmentParts = new List<EquipmentPart>() {
                EquipmentPart.weapon,
                EquipmentPart.weapon,
                EquipmentPart.head,
                EquipmentPart.body,
                EquipmentPart.legs,
                EquipmentPart.treasure,
                EquipmentPart.treasure
            };

            for (int i = 0; i < equipmentParts.Count; i ++)
            {
                UIButton equipBtn = UIButton.NewInstantiate(equipmentParts[i]);

                equipBtn.transform.SetParent(equipmentPartView.transform, false);

                UIMouseDelegate mouseDelegate = equipBtn.gameObject.GetComponent<UIMouseDelegate>();

                mouseDelegate.onPointerDoubleClickDelegate = OnDoubleClickEquipmentPart;
                mouseDelegate.onBeginDragDelegate = OnBeginDragEquipmentPart;
                mouseDelegate.onDragDelegate = onDrag;
                mouseDelegate.onEndDragDelegate = onEndDrag;
                mouseDelegate.onDropDelegate = onDrop;
            }
        }

        // Use this for initialization
        void Start()
        {
            setItemsSet();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void setItemsSet()
        {
            for (int i = 0; i < Global.equipmentButtons.Count; i++)
            {
                UIButton equipBtn = Global.equipmentButtons[i];
                equipBtn.gameObject.SetActive(!equipBtn.equipment.isWear);

                equipBtn.transform.SetParent(itemsSet.transform, false);
                UIMouseDelegate mouseDelegate = equipBtn.gameObject.GetComponent<UIMouseDelegate>();
                mouseDelegate.onBeginDragDelegate = OnBeginDragItemsSet;
                mouseDelegate.onDragDelegate = OnDragItemsSet;
                mouseDelegate.onEndDragDelegate = OnEndDragItemsSet;
                mouseDelegate.onPointerDoubleClickDelegate = OnDoubleClickItemsSet;
            }
        }

        //EquipmentButtons的鼠标代理事件--------------------------------------
        //------------------------------------------------------------------
        //双击装备
        void OnDoubleClickEquipmentPart(GameObject obj, PointerEventData eventData)
        {
            Global.hero.equipmentManager.TakeOffEquipment(obj.GetComponent<UIButton>());

            setItemsSet();
        }

        //开始拖拽
        void OnBeginDragEquipmentPart(GameObject obj, PointerEventData eventData)
        {
            UIButton originalEquiBtn = obj.GetComponentInChildren<UIButton>();

            canDrag = (originalEquiBtn.equipment != null);

            if (!canDrag)
            {
                return;
            }

            //代替品实例化
            dragTempObject = new GameObject("DragTempObject");
            dragTempObject.transform.SetParent(UIScene.Instance.transform, false);
            dragTempObject.AddComponent<RectTransform>();

            EquipmentClass.UIButton temp = EquipmentClass.UIButton.NewInstantiate();
            temp.transform.SetParent(dragTempObject.transform, false);
            temp.setEquipment(originalEquiBtn.equipment);

            //取消武器装备
            takeOffEquipment(originalEquiBtn);

            //防止拖拽结束时，代替品挡住了准备覆盖的对象而使得 OnDrop（） 无效
            CanvasGroup group = dragTempObject.AddComponent<CanvasGroup>();
            group.blocksRaycasts = false;
        }

        //拖拽中
        void OnDragEquipmentPart(GameObject obj, PointerEventData eventData)
        {
            if (!canDrag)
            {
                return;
            }

            //并将拖拽时的坐标给予被拖拽对象的代替品
            Vector3 movePosition = new Vector3(Input.mousePosition.x + 20, Input.mousePosition.y - 20);

            dragTempObject.transform.position = movePosition;
        }

        //结束拖拽
        void OnEndDragEquipmentPart(GameObject obj, PointerEventData eventData)
        {
            if (!canDrag)
            {
                return;
            }

            //拖拽结束，销毁代替品
            if (dragTempObject)
            {
                Destroy(dragTempObject);
            }

            UIScene.Instance.heroInfoView.itemsView.GetComponent<ItemView>().setItemsSet();
        }

        //接收被拖拽的物品
        void OnDropEquipmentPart(GameObject tagerObj, PointerEventData eventData)
        {
            GameObject dropObj = eventData.pointerDrag;

            UIButton dropEquipmentBtn = dropObj.GetComponent<UIButton>();
            UIButton tagerEquipmentBtn = tagerObj.GetComponent<UIButton>();

            //如果装备部位不同，则返回
            if (dropEquipmentBtn.part != tagerEquipmentBtn.part)
            {
                return;
            }

            takeOnEquipment(tagerEquipmentBtn, dropEquipmentBtn);
        }


        //ItemSet的鼠标代理事件-----------------------------------------------
        //------------------------------------------------------------------
        void OnDoubleClickItemsSet(GameObject obj, PointerEventData eventData)
        {
            UIButton equipBtn = obj.GetComponent<UIButton>();

            Global.hero.equipmentManager.TakeOnEquipment(Global.hero.equipmentManager.equipmentDict[equipBtn.part.ToString()], equipBtn);

            setItemsSet();
        }

        void OnBeginDragItemsSet(GameObject obj, PointerEventData eventData)
        {
            //代替品实例化
            dragTempObject = new GameObject("DragTempObject");
            dragTempObject.transform.SetParent(UIScene.Instance.heroInfoView.transform, false);
            dragTempObject.AddComponent<RectTransform>();

            Equipment tempEquipment = new Equipment(obj.GetComponentInChildren<UIButton>().equipment.id);
            UIButton temp = EquipmentClass.UIButton.NewInstantiate(tempEquipment);
            temp.transform.SetParent(dragTempObject.transform, false);

            //防止拖拽结束时，代替品挡住了准备覆盖的对象而使得 OnDrop（） 无效
            CanvasGroup group = dragTempObject.AddComponent<CanvasGroup>();
            group.blocksRaycasts = false;
        }

        void OnDragItemsSet(GameObject obj, PointerEventData eventData)
        {
            //并将拖拽时的坐标给予被拖拽对象的代替品

            Vector3 movePosition = new Vector3(Input.mousePosition.x + 20, Input.mousePosition.y - 20);

            dragTempObject.transform.position = movePosition;
        }

        void OnEndDragItemsSet(GameObject obj, PointerEventData eventData)
        {
            //拖拽结束，销毁代替品
            if (dragTempObject)
            {
                Destroy(dragTempObject);
            }

            UIScene.Instance.heroInfoView.itemsView.GetComponent<ItemView>().setItemsSet();
        }
    }

}