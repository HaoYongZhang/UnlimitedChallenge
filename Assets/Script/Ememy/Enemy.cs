﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace EnemyClass
{
    public class Enemy : MonoBehaviour
    {
        public string enemyId;
        public Dictionary<string, string> data;
        public PropertyManager propertyManager { get; set; }
        public UIHp hpSlider;

        void Start()
        {
            //设置数据
            data = DataManager.Instance.enemyDatas.getData(enemyId);

            //设置属性
            propertyManager = GetComponent<PropertyManager>();

            Dictionary<string, string> propertyData = DataManager.Instance.enemyDatas.getPropertyData(enemyId);

            foreach(KeyValuePair<string, string> pair in propertyData)
            {
                if(PropertyTool.isExist(propertyManager.basicProperty, pair.Key))
                {
                    PropertyTool.ReflectSetter(propertyManager.basicProperty, pair.Key, float.Parse(pair.Value));
                }
            }

            propertyManager.Hp = propertyManager.HpMax;
            propertyManager.Mp = propertyManager.MpMax;

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
    }
}
