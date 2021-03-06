﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillClass;

namespace SkillClass
{
    /// <summary>
    /// 技能描述
    /// </summary>
    public class Description
    {

        /// <summary>
        /// 获取技能的描述说明
        /// </summary>
        /// <returns>The description.</returns>
        public static string GetDescription(Skill skill)
        {
            if (skill == null)
            {
                return "技能为空";
            }
            switch (skill.type)
            {
                case SkillType.attack:
                    {
                        return getAttackDescription(skill);
                    }
                case SkillType.defense:
                    {
                        return "";
                    }
                case SkillType.treatment:
                    {
                        return getTreatmentDescription(skill);
                    }
                case SkillType.intensify:
                    {
                        return getIntensifyDescription(skill);
                    }
                case SkillType.complex:
                    {
                        return "";
                    }
                case SkillType.specialty:
                    {
                        return getSpecialtyDescription(skill);
                    }
                default:
                    {
                        return "";
                    }

            }
        }

        /// <summary>
        /// 获取伤害类型的技能描述说明
        /// </summary>
        /// <returns>The intensify description.</returns>
        /// <param name="skill">Skill.</param>
        static string getAttackDescription(Skill skill)
        {
            string originalDescription = skill.data["description"];

            string strength = skill.data["strength"] != "0" ? "+" + "力量*" + skill.data["strength"] : "";
            string agility = skill.data["agility"] != "0" ? "+" +"敏捷*" + skill.data["agility"] : "";
            string intellect = skill.data["intellect"] != "0" ? "+" +"内力*" + skill.data["intellect"] : "";
            string damageFormula = "伤害：" + skill.data["basicDamage"] + strength + agility + intellect;

            return originalDescription + "\n" + "<color=orange>" + damageFormula + "</color>";
        }

        /// <summary>
        /// 获取治疗类型的技能描述说明
        /// </summary>
        /// <returns>The intensify description.</returns>
        /// <param name="skill">Skill.</param>
        static string getTreatmentDescription(Skill skill)
        {
            string originalDescription = skill.data["description"];
            int i = 0;
            Property property = new Property();

            foreach (KeyValuePair<string, string> dict in skill.data)
            {
                if (PropertyTool.isExist(property, dict.Key))
                {
                    //截取字符串，获得+/-符号
                    string symbol = dict.Value.Substring(0, 1);
                    string increateStr = (symbol == "+" ? "+" : "-");
                    string increateColor = (symbol == "+" ? "<color=lime>" : "<color=red>");
                    string increateValue;
                    string descriptionName;

                    //判断如果属性值是百分比时，最高恢复是100%，也就是1.0f
                    //所以大于1.0f就是直接增加生命值的显示
                    if (float.Parse(dict.Value) > 1.0f)
                    {
                        increateValue = dict.Value.Substring(1, dict.Value.Length - 1) + "点";
                    }
                    else
                    {
                        float rate = float.Parse(dict.Value.Substring(1, dict.Value.Length - 1));

                        increateValue = rate * 100 + "%";
                    }

                    if (dict.Key == "increateHp")
                    {
                        descriptionName = "恢复生命";
                    }
                    else if (dict.Key == "increateMp")
                    {
                        descriptionName = "恢复能量";
                    }
                    else if (dict.Key == "addlHpRegeneration")
                    {
                        descriptionName = "每秒恢复生命";
                    }
                    else if (dict.Key == "addlMpRegeneration")
                    {
                        descriptionName = "每秒恢复能量";
                    }
                    else
                    {
                        descriptionName = "技能属性错误";
                    }

                    string s = "@" + i;
                    originalDescription = originalDescription.Replace(s, "\n" + increateColor + descriptionName + increateStr + increateValue + "</color>");

                    i++;
                }
                            

            }

            return originalDescription;
        }


        /// <summary>
        /// 获取强化类型的技能描述说明
        /// </summary>
        /// <returns>The intensify description.</returns>
        /// <param name="skill">Skill.</param>
        static string getIntensifyDescription(Skill skill)
        {
            Property property = new Property();
            int i = 0;
            string originalDescription = skill.data["description"];

            foreach (KeyValuePair<string, string> dict in skill.data)
            {
                if (PropertyTool.isExist(property, dict.Key))
                {
                    //截取字符串，获得+/-符号
                    string symbol = dict.Value.Substring(0, 1);
                    string increateColor = (symbol == "+" ? "<color=lime>" : "<color=red>");

                    string increateValue = dict.Value;

                    string descriptionName = PropertyTool.ReflectDescription(property, dict.Key);

                    originalDescription += "\n" + increateColor + descriptionName + "  " + increateValue + "</color>";

                    i++;
                }
            }

            return originalDescription;
        }

        static string getSpecialtyDescription(Skill skill)
        {
            string originalDescription = skill.data["description"];
            string[] strList = originalDescription.Split('/');
            string description = strList[0] + "\n" + "<color=#7BA5C0>" + strList[1] + "</color>";


            return description;
        }
    }

}