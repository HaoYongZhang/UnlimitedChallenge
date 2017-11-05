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
            skillButton.defaultSprite = skillButton.skillImage.sprite;

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
            
        }

        void Update()
        {
            
        }


        void FixedUpdate()
        {
            //当技能移除时
            if (skill == null)
            {
                skillImage.sprite = defaultSprite;
                cooldownImage.fillAmount = 0;
                cooldownText.text = "";
            }

            if (skill != null)
            {
                Skill oneSkill = SkillManager.GetOneSkillByID(skill.id);
                //当技能开始冷却时
                if (oneSkill.isCooldown)
                {
                    //执行冷却动画
                    cooldownImage.fillAmount = 1 - (oneSkill.currentCoolDown / float.Parse(oneSkill.data["cooldown"]));
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

            if(_skill != null)
            {
                skillImage.sprite = _skill.imageSprite;
            }
            else
            {
                skillImage.sprite = defaultSprite;
            }
        }
    }
}

