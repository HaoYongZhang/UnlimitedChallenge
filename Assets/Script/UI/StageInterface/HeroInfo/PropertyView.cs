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
            Property property = Global.hero.property;

            hpBar.value = property.hp / property.hpMax;
            mpBar.value = property.mp / property.mpMax;
            hpText.text = property.hp + "/" + property.hpMax;
            mpText.text = property.mp + "/" + property.mpMax;

            if (property.hpRegeneration > 0)
            {
                hpRegenerationText.text = "+" + MathTool.Round(property.hpRegeneration, 1);
            }
            else
            {
                hpRegenerationText.text = "-" + MathTool.Round(property.hpRegeneration, 1);
            }

            if (property.mpRegeneration > 0)
            {
                mpRegenerationText.text = "+" + MathTool.Round(property.mpRegeneration, 1);
            }
            else
            {
                mpRegenerationText.text = "-" + MathTool.Round(property.mpRegeneration, 1);
            }

            strengthText.text = "力量：" + "<color=#98FF67>" + property.strength.ToString() + "</color>";
            agilityText.text = "敏捷：" + "<color=#98FF67>" + property.agility.ToString() + "</color>";
            intellectText.text = "能量：" + "<color=#98FF67>" + property.intellect.ToString() + "</color>";

            attackText.text = "攻击力：" + "<color=#98FF67>" + property.attack.ToString() + "</color>";
            armorText.text = "护甲：" + "<color=#98FF67>" + property.armor.ToString() + "</color>";
            moveSpeedText.text = "移动速度：" + "<color=#98FF67>" + property.moveSpeed.ToString() + "</color>";


        }
    }

}
