using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

namespace SkillClass
{
    public class UIButton : MonoBehaviour
    {
        public Skill skill;
        public Image background;
        public Image skillImage;
        public Image cooldownImage;
        public Text cooldownText;
        public UIMouseDelegate mouseDelegate;

        Sprite defaultSprite;

        public static UIButton NewInstantiate()
        {
            UIButton skillButton = Instantiate((GameObject)Resources.Load("UI/SkillButton")).GetComponent<UIButton>();
            skillButton.defaultSprite = skillButton.skillImage.sprite;
            skillButton.background.color = ColorTool.getColor(63, 63, 63);
            skillButton.mouseDelegate = skillButton.gameObject.GetComponent<UIMouseDelegate>();

            return skillButton;
        }

        public static UIButton NewInstantiate(Skill skill)
        {
            UIButton skillButton = NewInstantiate();
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
                background.color = ColorTool.getColor(63, 63, 63);
                skillImage.sprite = defaultSprite;
                cooldownImage.fillAmount = 0;
                cooldownText.text = "";
            }

            if (skill != null)
            {
                Skill oneSkill = SkillClass.Manager.GetOneSkillByID(skill.id);
                //当技能开始冷却时
                if (oneSkill.releaseState == SkillReleaseState.cooldown)
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
                background.color = ColorTool.GetSkillColor(_skill.rank);
            }
            else
            {
                skillImage.sprite = defaultSprite;
                background.color = ColorTool.getColor(63, 63, 63);
            }
        }
    }
}

