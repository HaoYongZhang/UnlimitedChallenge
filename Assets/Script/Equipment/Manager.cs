using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;
using UnityEngine.EventSystems;

namespace EquipmentClass
{
    public class Manager : MonoBehaviour
    {
        public Equipment defaultWeapon;

        public Equipment currentWeapon
        {
            get{
                if(leftWeapon.equipment != null)
                {
                    return leftWeapon.equipment;
                }
                else
                {
                    return defaultWeapon;
                }
            }
        }

        public EquipmentClass.UIButton leftWeapon;
        public EquipmentClass.UIButton rightWeapon;
        public EquipmentClass.UIButton head;
        public EquipmentClass.UIButton body;
        public EquipmentClass.UIButton legs;
        public EquipmentClass.UIButton treasure;

        Dictionary<string, EquipmentClass.UIButton> equipmentDict = new Dictionary<string, EquipmentClass.UIButton>();

        //拖动物品时的临时创建对象
        GameObject dragTempObject;
        //判断能否执行拖拽方法
        bool canDrag;

        void Awake()
        {
            //默认武器
            defaultWeapon = new Equipment("10001");

            //创建装备按钮
            leftWeapon = EquipmentClass.UIButton.NewInstantiate(EquipmentPart.weapon);
            rightWeapon = EquipmentClass.UIButton.NewInstantiate(EquipmentPart.weapon);
            head = EquipmentClass.UIButton.NewInstantiate(EquipmentPart.head);
            body = EquipmentClass.UIButton.NewInstantiate(EquipmentPart.body);
            legs = EquipmentClass.UIButton.NewInstantiate(EquipmentPart.legs);
            treasure = EquipmentClass.UIButton.NewInstantiate(EquipmentPart.treasure);

            equipmentDict.Add("leftWeapon", leftWeapon);
            equipmentDict.Add("rightWeapon", rightWeapon);
            equipmentDict.Add("head", head);
            equipmentDict.Add("body", body);
            equipmentDict.Add("legs", legs);
            equipmentDict.Add("treasure", treasure);

            foreach(KeyValuePair<string, EquipmentClass.UIButton> dict in equipmentDict)
            {
                dict.Value.gameObject.GetComponent<UIMouseDelegate>().onBeginDragDelegate = onBeginDrag;
                dict.Value.gameObject.GetComponent<UIMouseDelegate>().onDragDelegate = onDrag;
                dict.Value.gameObject.GetComponent<UIMouseDelegate>().onEndDragDelegate = onEndDrag;
                dict.Value.gameObject.GetComponent<UIMouseDelegate>().onDropDelegate = onDrop;
            }

        }

        // Use this for initialization
        void Start()
        {


        }

        // Update is called once per frame
        void Update()
        {

        }

        public void replaceEquipmentPart(Equipment equipment)
        {
            
            switch(equipment.part)
            {
                case EquipmentPart.weapon:{
                        GameObject equipmentObj = (GameObject)Instantiate(Resources.Load("Material/Equipment/equipment_" + equipment.id));
                        Global.hero.charactersManager.replaceAvator(CharactersManager.left_weapon_name, equipmentObj);
                    }
                    break;
                case EquipmentPart.body:
                    {
                        GameObject equipObj_1 = Resources.Load("Material/Equipment/equipment_" + equipment.id + "_body") as GameObject;
                        GameObject equipObj_2 = Resources.Load("Material/Equipment/equipment_" + equipment.id + "_hand_left_arm") as GameObject;
                        GameObject equipObj_3 = Resources.Load("Material/Equipment/equipment_" + equipment.id + "_hand_right_arm") as GameObject;
                        GameObject equipObj_4 = Resources.Load("Material/Equipment/equipment_" + equipment.id + "_hand_left_forearm") as GameObject;
                        GameObject equipObj_5 = Resources.Load("Material/Equipment/equipment_" + equipment.id + "_hand_right_forearm") as GameObject;

                        if(equipObj_1 != null)
                        {
                            equipObj_1 = Instantiate(equipObj_1);
                        }
                        if (equipObj_2 != null)
                        {
                            equipObj_2 = Instantiate(equipObj_2);
                        }
                        if (equipObj_3 != null)
                        {
                            equipObj_3 = Instantiate(equipObj_3);
                        }
                        if (equipObj_4 != null)
                        {
                            equipObj_4 = Instantiate(equipObj_4);
                        }
                        if (equipObj_5 != null)
                        {
                            equipObj_5 = Instantiate(equipObj_5);
                        }

                        Global.hero.charactersManager.replaceAvator(CharactersManager.body_name, equipObj_1);
                        Global.hero.charactersManager.replaceAvator(CharactersManager.hand_left_arm_name, equipObj_2);
                        Global.hero.charactersManager.replaceAvator(CharactersManager.hand_right_arm_name, equipObj_3);
                        Global.hero.charactersManager.replaceAvator(CharactersManager.hand_left_forearm_name, equipObj_4);
                        Global.hero.charactersManager.replaceAvator(CharactersManager.hand_right_forearm_name, equipObj_5);
                    }

                    break;
            }
        }

        /// <summary>
        /// 穿上装备
        /// </summary>
        /// <param name="tagerEquiBtn">Tager equi button.</param>
        /// <param name="dropEquiBtn">Source equi button.</param>
        public void takeOnEquipment(UIButton tagerEquiBtn, UIButton dropEquiBtn)
        {
            replaceEquipmentPart(dropEquiBtn.equipment);
            tagerEquiBtn.setEquipment(dropEquiBtn.equipment);

            if(tagerEquiBtn.equipment.part == EquipmentPart.weapon)
            {
                UIScene.Instance.fightBar.setWeaponBG(tagerEquiBtn.equipment);
            }

            foreach (UIButton equipBtn in Global.equipmentButtons)
            {
                if (equipBtn.equipment.id == tagerEquiBtn.equipment.id)
                {
                    equipBtn.equipment.isWear = true;
                    break;
                }
            }
        }

        /// <summary>
        /// 脱下装备
        /// </summary>
        /// <param name="equipmentButton">Equipment button.</param>
        public void takeOffEquipment(EquipmentClass.UIButton equipmentButton)
        {
            foreach(EquipmentClass.UIButton equipBtn in Global.equipmentButtons)
            {
                if(equipBtn.equipment.id == equipmentButton.equipment.id)
                {
                    equipBtn.equipment.isWear = false;
                    break;
                }
            }

            switch (equipmentButton.equipment.part)
            {
                case EquipmentPart.weapon:
                    {
                        Global.hero.charactersManager.replaceAvator(CharactersManager.left_weapon_name, null);
                        UIScene.Instance.fightBar.setWeaponBG(null);
                    }
                    break;
                case EquipmentPart.body:
                    {
                        Global.hero.charactersManager.replaceAvator(CharactersManager.body_name, null);
                        Global.hero.charactersManager.replaceAvator(CharactersManager.hand_left_arm_name, null);
                        Global.hero.charactersManager.replaceAvator(CharactersManager.hand_right_arm_name, null);
                        Global.hero.charactersManager.replaceAvator(CharactersManager.hand_left_forearm_name, null);
                        Global.hero.charactersManager.replaceAvator(CharactersManager.hand_right_forearm_name, null);
                    }
                    break;
            }

            equipmentButton.setEquipment(null);
        }

        //开始拖拽
        void onBeginDrag(GameObject obj, PointerEventData eventData)
        {
            UIButton originalEquiBtn = obj.GetComponentInChildren<UIButton>();

            canDrag = (originalEquiBtn.equipment != null);

            if(!canDrag)
            {
                return;
            }

            //代替品实例化
            dragTempObject = new GameObject("DragTempObject");
            dragTempObject.transform.SetParent(UIScene.Instance.heroView.transform, false);
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
        void onDrag(GameObject obj, PointerEventData eventData)
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
        void onEndDrag(GameObject obj, PointerEventData eventData)
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

            UIScene.Instance.heroView.itemsView.GetComponent<UIHeroItemView>().setItemsSet();
        }

        //接收被拖拽的物品
        void onDrop(GameObject tagerObj, PointerEventData eventData)
        {
            GameObject dropObj = eventData.pointerDrag;

            UIButton dropEquipmentBtn = dropObj.GetComponent<UIButton>();
            UIButton tagerEquipmentBtn = tagerObj.GetComponent<UIButton>();

            if (dropEquipmentBtn.part != tagerEquipmentBtn.part)
            {
                return;
            }

            takeOnEquipment(tagerEquipmentBtn, dropEquipmentBtn);
        }
    }
}

