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
            hpBar.value = Global.hero.property.hp / Global.hero.property.hpMax;
            mpBar.value = Global.hero.property.mp / Global.hero.property.mpMax;
            hpText.text = Global.hero.property.hp + "/" + Global.hero.property.hpMax;
            mpText.text = Global.hero.property.mp + "/" + Global.hero.property.mpMax;

            if (Global.hero.property.hpRegeneration > 0)
            {
                hpRegenerationText.text = "+" + MathTool.Round(Global.hero.property.hpRegeneration, 1);
            }
            else
            {
                hpRegenerationText.text = "-" + MathTool.Round(Global.hero.property.hpRegeneration, 1);
            }

            if (Global.hero.property.mpRegeneration > 0)
            {
                mpRegenerationText.text = "+" + MathTool.Round(Global.hero.property.mpRegeneration, 1);
            }
            else
            {
                mpRegenerationText.text = "-" + MathTool.Round(Global.hero.property.mpRegeneration, 1);
            }
        }
    }
}


