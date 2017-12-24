using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SkillClass;

namespace UIHeroStatus
{
    public class StatusIconsView : MonoBehaviour
    {
        public List<StatusIcon> statusIconList = new List<StatusIcon>();

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// 添加技能的小状态图标
        /// </summary>
        /// <param name="skill">Skill.</param>
        public void addStatusIcon(Skill skill)
        {
            GameObject statusObj = Instantiate((GameObject)Resources.Load("UI/UIHeroStatusIcon"));
            statusObj.transform.SetParent(transform, false);

            StatusIcon statusIcon = statusObj.GetComponent<StatusIcon>();
            statusIcon.image.sprite = skill.imageSprite;
            statusIcon.id = skill.id;

            statusIconList.Add(statusIcon);

            UIMouseDelegate mouseDelegate = statusObj.GetComponent<UIMouseDelegate>();
            mouseDelegate.onPointerEnterDelegate = UIScene.Instance.pointToStatusIcon;
            mouseDelegate.onPointerExitDelegate = UIScene.Instance.pointOutStatusIcon;
        }

        /// <summary>
        /// 移除技能的小状态图标
        /// </summary>
        /// <param name="skill">Skill.</param>
        public void removeStatusIcon(Skill skill)
        {
            for (int i = 0; i < statusIconList.Count; i ++)
            {
                if (statusIconList[i].id == skill.id)
                {
                    Destroy(statusIconList[i].gameObject);
                    break;
                }
            }
        }
    }
}

