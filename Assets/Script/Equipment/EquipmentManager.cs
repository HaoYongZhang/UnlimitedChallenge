using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;

namespace EquipmentClass
{
    public class EquipmentManager : MonoBehaviour
    {
        public Equipment leftWeapon;
        public Equipment rightWeapon;
        public Equipment head;
        public Equipment body;
        public Equipment legs;
        public Equipment treasure;

        // Use this for initialization
        void Start()
        {


        }

        // Update is called once per frame
        void Update()
        {

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
    }
}

