using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SkillClass;

namespace UIHeroStatus
{
    public class StatusIcon : MonoBehaviour
    {
        public string id;
        public Image image;
        public Image background;

        public static StatusIcon NewInstantiate(Skill skill)
        {
            StatusIcon statusIcon = Instantiate((GameObject)Resources.Load("UI/UIHeroStatusIcon")).GetComponent<StatusIcon>();

            statusIcon.id = skill.id;
            statusIcon.image.sprite = skill.imageSprite;
            statusIcon.background.color = ColorTool.GetSkillColor(skill.rank);

            return statusIcon;
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

