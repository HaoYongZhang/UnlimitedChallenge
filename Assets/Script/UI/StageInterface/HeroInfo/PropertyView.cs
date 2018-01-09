using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UIHeroInfo
{
    public class PropertyView : MonoBehaviour
    {

        public Slider hpBar;
        public Slider mpBar;
        public Text hpText;
        public Text mpText;
        public Text hpRegenerationText;
        public Text mpRegenerationText;
        public Text strengthText;
        public Text agilityText;
        public Text intellectText;
        public Text attackText;
        public Text armorText;
        public Text moveSpeedText;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void FixedUpdate()
        {
            float hp = Global.hero.propertyManager.Hp;
            float hpMax = Global.hero.propertyManager.HpMax;
            float hpRegeneration = Global.hero.propertyManager.HpRegeneration;
            float mp = Global.hero.propertyManager.Mp;
            float mpMax = Global.hero.propertyManager.MpMax;
            float mpRegeneration = Global.hero.propertyManager.MpRegeneration;


            hpBar.value = hp / hpMax;
            mpBar.value = mp / mpMax;
            hpText.text = hp + "/" + hpMax;
            mpText.text = mp + "/" + mpMax;

            if (hpRegeneration > 0)
            {
                hpRegenerationText.text = "+" + MathTool.Round(hpRegeneration, 1);
            }
            else
            {
                hpRegenerationText.text = "-" + MathTool.Round(hpRegeneration, 1);
            }

            if (mpRegeneration > 0)
            {
                mpRegenerationText.text = "+" + MathTool.Round(mpRegeneration, 1);
            }
            else
            {
                mpRegenerationText.text = "-" + MathTool.Round(mpRegeneration, 1);
            }

            strengthText.text = "力量：" + "<color=#98FF67>" + Global.hero.propertyManager.Strength.ToString() + "</color>";
            agilityText.text = "敏捷：" + "<color=#98FF67>" + Global.hero.propertyManager.Agility.ToString() + "</color>";
            intellectText.text = "能量：" + "<color=#98FF67>" + Global.hero.propertyManager.Intellect.ToString() + "</color>";

            attackText.text = "攻击力：" + "<color=#98FF67>" + Global.hero.propertyManager.Attack.ToString() + "</color>";
            armorText.text = "护甲：" + "<color=#98FF67>" + Global.hero.propertyManager.Armor.ToString() + "</color>";
            moveSpeedText.text = "移动速度：" + "<color=#98FF67>" + Global.hero.propertyManager.MoveSpeed.ToString() + "</color>";


        }
    }

}
