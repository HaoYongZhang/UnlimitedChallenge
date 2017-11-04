using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

namespace SkillClass
{
    public class SkillButton : MonoBehaviour
    {
        public Skill skill;
        public Image skillImage;
        public Image cooldownImage;
        public Text cooldownText;

        Sprite defaultSprite;

        public static SkillButton NewInstantiate()
        {
            SkillButton skillButton = Instantiate((GameObject)Resources.Load("UI/SkillButton")).GetComponent<SkillButton>();

            return skillButton;
        }

        public static SkillButton NewInstantiate(Skill skill)
        {
            SkillButton skillButton = NewInstantiate();
            skillButton.setSkill(skill);

            return skillButton;
        }

        void Start()
        {
            //defaultSprite = Resources.Load("Sprites/Icons-HighResolution/GameController/PS4/Dark_PNG/X-Button-Dark", typeof(Sprite)) as Sprite;
            defaultSprite = skillImage.sprite;
        }

        void Update()
        {
            
        }


        void FixedUpdate()
        {
            //当技能移除，而且技能图片不是默认图片时
            if (skill == null && skillImage.sprite != defaultSprite)
            {
                skillImage.sprite = defaultSprite;
                cooldownImage.fillAmount = 0;
                cooldownText.text = "";
            }

            if (skill != null)
            {
                //当技能开始冷却时
                if (skill.isCooldown)
                {
                    //执行冷却动画
                    cooldownImage.fillAmount = 1 - (skill.currentCoolDown / float.Parse(skill.data["cooldown"]));
                }
                else
                {
                    //还原冷却动画
                    cooldownImage.fillAmount = 0;
                    cooldownText.text = "";
                }
            }
        }

        public void setSkill(Skill _skill){
            skill = _skill;
            skillImage.sprite = _skill.imageSprite;
        }
    }
}

