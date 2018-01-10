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
            StatusIcon statusIcon = StatusIcon.NewInstantiate(skill);

            statusIcon.transform.SetParent(transform, false);

            statusIconList.Add(statusIcon);

            UIMouseDelegate mouseDelegate = statusIcon.gameObject.GetComponent<UIMouseDelegate>();
            mouseDelegate.onPointerEnterDelegate = UIScene.Instance.pointToStatusIcon;
            mouseDelegate.onPointerExitDelegate = UIScene.Instance.pointOutStatusIcon;
        }

        /// <summary>
        /// 移除技能的小状态图标
        /// </summary>
        /// <param name="skill">Skill.</param>
        public void removeStatusIcon(Skill skill)
        {
            int deleteIndex = 0;
            for (int i = 0; i < statusIconList.Count; i ++)
            {
                if (statusIconList[i].id == skill.id)
                {
                    deleteIndex = i;
                    break;
                }
            }

            Destroy(statusIconList[deleteIndex].gameObject);
            statusIconList.RemoveAt(deleteIndex);
        }
    }
}

