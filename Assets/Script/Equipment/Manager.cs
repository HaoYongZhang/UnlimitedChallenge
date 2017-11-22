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

        public EquipmentClass.UIButton leftWeapon;
        public EquipmentClass.UIButton rightWeapon;
        public EquipmentClass.UIButton head;
        public EquipmentClass.UIButton body;
        public EquipmentClass.UIButton legs;
        public EquipmentClass.UIButton treasure;

        Dictionary<string, EquipmentClass.UIButton> equipmentDict = new Dictionary<string, EquipmentClass.UIButton>();

        //拖动物品时的临时创建对象
        GameObject dragTempObject;

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
            GameObject equipmentObj = (GameObject)Instantiate(Resources.Load("Material/Equipment/equipment_" + equipment.id));
            switch(equipment.part)
            {
                case EquipmentPart.weapon:{
                        
                        Global.hero.charactersManager.replaceAvator(CharactersManager.left_weapon_name, equipmentObj);
                    }
                    break;
            }
        }

        /// <summary>
        /// 返回同一个技能，共用这个技能的所有状态，否则会有深拷贝造成技能数据不一致
        /// </summary>
        /// <returns>The one skill by identifier.</returns>
        /// <param name="_id">Identifier.</param>
        public static Equipment GetOneSkillByID(string _id)
        {
            foreach (Equipment equipment in Global.equipments)
            {
                if (equipment.id == _id)
                {
                    return equipment;
                }
            }

            return null;
        }

        void onBeginDrag(GameObject obj, PointerEventData eventData)
        {
            //代替品实例化
            dragTempObject = new GameObject("DragTempObject");
            dragTempObject.transform.SetParent(UIScene.Instance.sceneProperty.transform, false);
            dragTempObject.AddComponent<RectTransform>();

            EquipmentClass.UIButton temp = EquipmentClass.UIButton.NewInstantiate();
            temp.transform.SetParent(dragTempObject.transform, false);
            temp.setEquipment(obj.GetComponentInChildren<EquipmentClass.UIButton>().equipment);

            obj.GetComponentInChildren<EquipmentClass.UIButton>().setEquipment(null);

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
        }

        void onDrop(GameObject obj, PointerEventData eventData)
        {
            GameObject dropObj = eventData.pointerDrag;

            Equipment replaceEquipment = dropObj.GetComponent<EquipmentClass.UIButton>().equipment;
            Equipment oldEquipment = obj.GetComponent<EquipmentClass.UIButton>().equipment;

            if (replaceEquipment.part != obj.GetComponent<EquipmentClass.UIButton>().part)
            {
                return;
            }

            replaceEquipmentPart(replaceEquipment);
            obj.GetComponent<EquipmentClass.UIButton>().setEquipment(replaceEquipment);
        }
    }
}

