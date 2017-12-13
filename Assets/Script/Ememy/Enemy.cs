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
        public UIHp hpSlider;

        void Start()
        {
            //添加伤害代理
            GetComponent<AI>().attackDamageDelegate = attackDamage;

            //设置属性
            property = new Property();
            Dictionary<string, string> propertyData = DataManager.Instance.enemyDatas.getData(enumyId);

            foreach(KeyValuePair<string, string> pair in propertyData)
            {
                if(PropertyUtil.isExist(property, pair.Key))
                {
                    PropertyUtil.ReflectSetter(property, pair.Key, float.Parse(pair.Value));
                }
            }

            property.hp = property.hpMax;
            property.mp = property.mpMax;

            //设置血条UI
            GameObject hpObj = (GameObject)Instantiate(Resources.Load("UI/EnemyHpSlider"));

            hpSlider = hpObj.GetComponent<UIHp>();
            hpSlider.tager = this;
            hpSlider.offset = new Vector2(0, 60);

            hpObj.transform.SetParent(UIEnemy.Instance.transform, false);
            UIEnemy.Instance.enemyHps.Add(hpObj);
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
