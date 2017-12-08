using UnityEngine;
using System.Collections;

namespace EnemyClass
{
    public class Enemy : MonoBehaviour
    {
        public string enumyId;
        // Use this for initialization
        void Start()
        {
            GetComponent<AI>().attackDamageDelegate = attackDamage;

            Dictionary<string, string> propertyData
        }

        // Update is called once per frame
        void Update()
        {

        }

        void attackDamage()
        {
            DamageManager.CommonAttack<Zombie, Hero>(gameObject, Global.hero.gameObject, DamageType.physics);
        }
    }
}

