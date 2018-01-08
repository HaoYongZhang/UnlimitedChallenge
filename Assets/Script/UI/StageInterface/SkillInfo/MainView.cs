using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SkillClass;

namespace UISkillInfo
{
    public class MainView : MonoBehaviour
    {
        public Image skillImage;
        public Image background;
        public Text title;
        public Text content;

        public Skill currentShowInfoSkill;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// 显示技能的详细信息
        /// </summary>
        /// <param name="skill">Skill.</param>
        public void showInfo(Skill skill)
        {
            currentShowInfoSkill = skill;

            skillImage.sprite = skill.imageSprite;
            background.color = ColorTool.GetSkillColor(skill.rank);

            string info = "";

            if (skill.isActive)
            {
                info += skill.description + "\n";

                if (skill.data["duration"] != "0")
                {
                    info += "持续时间    " + skill.data["duration"] + "\n";
                }

                if (skill.data["cooldown"] != "0")
                {
                    info += "冷却时间    " + skill.data["cooldown"] + "\n";
                }

                if (skill.data["costEnergy"] != "0")
                {
                    info += "能量消耗    " + "<color=#37abe1>" + skill.data["costEnergy"] + "</color>" + "\n";
                }

            }
            else
            {
                info += skill.description + "\n";
            }

            string isActiveStr = skill.isActive ? "主动技能" : "被动技能";
            float contentHeight = 0;

            title.text = skill.data["mName"] + "\n" + "<size=34>" + isActiveStr + "</size>";
            content.text = info;
            //技能描述的实际高度 + 图标高度 + top&bottom的边距 + 间隔
            contentHeight = content.preferredHeight + 120 + 30 * 2;

            GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, contentHeight);

            gameObject.SetActive(true);
        }


        /// <summary>
        /// 隐藏技能的详细信息
        /// </summary>
        public void hideInfo()
        {
            gameObject.SetActive(false);
        }
    }
}


