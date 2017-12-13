using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
namespace EnemyClass
{
    public class UIHp : MonoBehaviour
    {
        Slider hpSlider;
        RectTransform rectTransform;

        public Enemy tager;
        public Vector2 offset;

        // Use this for initialization
        void Start()
        {
            hpSlider = GetComponent<Slider>();
            rectTransform = GetComponent<RectTransform>();

            hpSlider.value = tager.property.hp / tager.property.hpMax;
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        void FixedUpdate()
        {
            Vector2 worldPosition = RectTransformUtility.WorldToScreenPoint(Global.mainCamera, tager.transform.position);

            rectTransform.DOMove(offset + worldPosition, 0.3f);

            hpSlider.value = tager.property.hp / tager.property.hpMax;
        }
    }

}

