using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;
using UnityEngine.EventSystems;
using UIHeroInfo;

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
        public EquipmentClass.UIButton treasure_2;

        public Dictionary<string, EquipmentClass.UIButton> equipmentDict = new Dictionary<string, EquipmentClass.UIButton>();

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
            treasure_2 = EquipmentClass.UIButton.NewInstantiate(EquipmentPart.treasure);

            equipmentDict.Add("weapon", leftWeapon);
            equipmentDict.Add("subWeapon", rightWeapon);
            equipmentDict.Add("head", head);
            equipmentDict.Add("body", body);
            equipmentDict.Add("legs", legs);
            equipmentDict.Add("treasure", treasure);
            equipmentDict.Add("treasure_2", treasure_2);

            foreach(KeyValuePair<string, EquipmentClass.UIButton> dict in equipmentDict)
            {
                dict.Value.gameObject.GetComponent<UIMouseDelegate>().onPointerDoubleClickDelegate = onDoubleClick;
                dict.Value.gameObject.GetComponent<UIMouseDelegate>().onBeginDragDelegate = onBeginDrag;
                dict.Value.gameObject.GetComponent<UIMouseDelegate>().onDragDelegate = onDrag;
                dict.Value.gameObject.GetComponent<UIMouseDelegate>().onEndDragDelegate = onEndDrag;
                dict.Value.gameObject.GetComponent<UIMouseDelegate>().onDropDelegate = onDrop;
            }

        }

        // Use this for initialization
        void Start()
        {
            takeOnEquipment(body, GetEquipmentButton("30001"));
            takeOnEquipment(legs, GetEquipmentButton("40001"));

            UIScene.Instance.heroInfoView.itemsView.setItemsSet();
        }

        // Update is called once per frame
        void Update()
        {

        }


        public UIButton GetEquipmentButton(string _id)
        {
            foreach(UIButton equipBtn in Global.equipmentButtons)
            {
                if(equipBtn.equipment.id == _id)
                {
                    return equipBtn;
                }
            }

            return null;
        }

        /// <summary>
        /// 更换装备外观
        /// </summary>
        /// <param name="equipment">Equipment.</param>
        public void replaceEquipmentPart(Equipment equipment)
        {
            switch(equipment.part)
            {
                case EquipmentPart.weapon:{
                        GameObject equipmentObj = (GameObject)Instantiate(Resources.Load("Material/Equipment/equipment_" + equipment.id));
                        Global.hero.charactersManager.replaceAvator(CharactersManager.right_weapon_name, equipmentObj);
                    }
                    break;
                case EquipmentPart.head:
                    {
                        HeadPart headPart = EnumTool.GetEnum<HeadPart>(equipment.data["headPart"]);
                        GameObject equipmentObj = (GameObject)Instantiate(Resources.Load("Material/Equipment/equipment_" + equipment.id + "_" + headPart.ToString()));

                        //清空遗留的装备模型
                        Global.hero.charactersManager.replaceAvator(CharactersManager.head_face_name, null);
                        Global.hero.charactersManager.replaceAvator(CharactersManager.headdress_name, null);

                        switch(headPart)
                        {
                            case HeadPart.headdress:
                                {
                                    Global.hero.charactersManager.replaceAvator(CharactersManager.headdress_name, equipmentObj);
                                }
                                break;
                            case HeadPart.face:
                                {
                                    Global.hero.charactersManager.replaceAvator(CharactersManager.head_face_name, equipmentObj);
                                }
                                break;
                        }
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
                case EquipmentPart.legs:
                    {
                        GameObject equipObj_1 = Resources.Load("Material/Equipment/equipment_" + equipment.id + "_leg_left_thigh") as GameObject;
                        GameObject equipObj_2 = Resources.Load("Material/Equipment/equipment_" + equipment.id + "_leg_left_shin") as GameObject;
                        GameObject equipObj_3 = Resources.Load("Material/Equipment/equipment_" + equipment.id + "_leg_right_thigh") as GameObject;
                        GameObject equipObj_4 = Resources.Load("Material/Equipment/equipment_" + equipment.id + "_leg_right_shin") as GameObject;

                        if (equipObj_1 != null)
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

                        Global.hero.charactersManager.replaceAvator(CharactersManager.leg_left_thigh_name, equipObj_1);
                        Global.hero.charactersManager.replaceAvator(CharactersManager.leg_left_shin_name, equipObj_2);
                        Global.hero.charactersManager.replaceAvator(CharactersManager.leg_right_thigh_name, equipObj_3);
                        Global.hero.charactersManager.replaceAvator(CharactersManager.leg_right_shin_name, equipObj_4);
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
            bool hasTakeOne = false;
            foreach (UIButton equipBtn in Global.equipmentButtons)
            {
                //如果已经装备了装备时，取消穿戴状态
                if(tagerEquiBtn.equipment != null)
                {
                    if(tagerEquiBtn.equipment.id == equipBtn.equipment.id)
                    {
                        equipBtn.equipment.isWear = false;
                    }
                }

                //筛选没有穿戴的装备
                if(!equipBtn.equipment.isWear)
                {
                    //设置穿戴状态
                    if (equipBtn.equipment.id == dropEquiBtn.equipment.id && !hasTakeOne)
                    {
                        equipBtn.equipment.isWear = true;
                        hasTakeOne = true;
                    }
                }
            }

            replaceEquipmentPart(dropEquiBtn.equipment);
            tagerEquiBtn.setEquipment(dropEquiBtn.equipment);

            //如果更换的装备是武器时，改变UI图标
            if (tagerEquiBtn.equipment.part == EquipmentPart.weapon)
            {
                UIScene.Instance.fightInfoView.setWeaponBG(tagerEquiBtn.equipment);
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
                        UIScene.Instance.fightInfoView.setWeaponBG(null);
                    }
                    break;
                case EquipmentPart.head:
                    {
                        Global.hero.charactersManager.replaceAvator(CharactersManager.headdress_name, null);
                        Global.hero.charactersManager.replaceAvator(CharactersManager.head_face_name, null);
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
                case EquipmentPart.legs:
                    {
                        Global.hero.charactersManager.replaceAvator(CharactersManager.leg_left_shin_name, null);
                        Global.hero.charactersManager.replaceAvator(CharactersManager.leg_left_thigh_name, null);
                        Global.hero.charactersManager.replaceAvator(CharactersManager.leg_right_shin_name, null);
                        Global.hero.charactersManager.replaceAvator(CharactersManager.leg_right_thigh_name, null);
                    }
                    break;
            }

            equipmentButton.setEquipment(null);
        }

        //双击装备
        void onDoubleClick(GameObject obj, PointerEventData eventData)
        {
            takeOffEquipment(obj.GetComponent<UIButton>());
            UIScene.Instance.heroInfoView.itemsView.GetComponent<ItemView>().setItemsSet();
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

            UIScene.Instance.heroInfoView.itemsView.GetComponent<ItemView>().setItemsSet();
        }

        //接收被拖拽的物品
        void onDrop(GameObject tagerObj, PointerEventData eventData)
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
    }
}

