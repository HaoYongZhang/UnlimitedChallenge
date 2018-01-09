using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SkillClass;

namespace UIHeroStatus
{
    public class MainView : MonoBehaviour
    {
        public Slider hpBar;
        public Slider mpBar;
        public Text hpText;
        public Text mpText;
        public Text hpRegenerationText;
        public Text mpRegenerationText;

        public StatusIconsView statusIconsView;

        // Use this for initialization
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
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
        }
    }
}


