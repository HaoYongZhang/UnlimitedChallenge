using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Utility;

namespace EnemyClass
{
    public class Enemy : MonoBehaviour
    {
        public string enumyId;
        public Property property { get; set; }

        void Start()
        {
            GetComponent<AI>().attackDamageDelegate = attackDamage;

            property = new Property();
            Dictionary<string, string> propertyData = DataManager.Instance.enemyDatas.getData(enumyId);

            foreach(KeyValuePair<string, string> pair in propertyData)
            {
                //Debug.Log(pair.Key + "===" + pair.Value);
                if(PropertyUtil.isExist(property, pair.Key))
                {
                    PropertyUtil.ReflectSetter(property, pair.Key, float.Parse(pair.Value));
                }
            }
        }


        void Update()
        {

        }

        void attackDamage()
        {
            DamageManager.CommonAttack<Enemy, Hero>(gameObject, Global.hero.gameObject, DamageType.physics);
        }
    }
}
