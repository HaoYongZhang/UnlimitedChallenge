using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SkillClass;

public class SkillImplementation
{
    public static void Implement(GameObject obj, Skill skill)
    {
        switch (skill.type)
        {
            case SkillType.attack:
                {
                    //Global.hero.fightManager.skillAttack(skill);
                }
                break;
            case SkillType.defense:
                {
                }
                break;
            case SkillType.treatment:
                {
                    Treatment(obj, skill);
                }
                break;
            case SkillType.intensify:
                {
                    Intensify(obj, skill);
                }
                break;
            case SkillType.complex:
                {

                }
                break;
            case SkillType.specialty:
                {
                    Global.hero.fightManager.skillAttack(skill);
                }
                break;
        }
    }

    public static void DurationEnd(GameObject obj, Skill skill)
    {
        switch (skill.type)
        {
            case SkillType.attack:
                {
                    //Global.hero.fightManager.skillAttack(skill);
                }
                break;
            case SkillType.defense:
                {
                }
                break;
            case SkillType.treatment:
                {
                    TreatmentEnd(obj, skill);
                }
                break;
            case SkillType.intensify:
                {
                    IntensifyEnd(obj, skill);
                }
                break;
            case SkillType.complex:
                {

                }
                break;
            case SkillType.specialty:
                {
                    Global.hero.fightManager.skillAttack(skill);
                }
                break;
        }
    }

    /// <summary>
    /// 使用治疗技能
    /// </summary>
    /// <returns>The treatment.</returns>
    /// <param name="skill">Skill.</param>
    public static void Treatment(GameObject obj, Skill skill)
    {
        Property property = obj.GetComponent<Property>();

        string increateHp = skill.data["increateHp"];

        if (increateHp != null && increateHp != "")
        {
            //截取字符串，获得属性增加的值
            float createValue = float.Parse(increateHp);
            //使用治疗技能后，加上的血量
            property.hp += createValue;
        }

        if(skill.duration > 0)
        {
            //添加强化的小图标
            UIScene.Instance.addStatusIcon(skill);

            //开始技能的持续时间
            skill.isInDuration = true;

            PropertIncrease(property, skill);
        }

        SkillClass.Manager skillManager = obj.GetComponent<SkillClass.Manager>();

        skillManager.OnFinished(skill);
    }


    public static void TreatmentEnd(GameObject obj, Skill skill)
    {
        //删除强化的小图标
        UIScene.Instance.removeStatusIcon(skill);
        //结束技能的持续时间
        skill.isInDuration = false;
        //获取Hero的属性
        Property property = obj.GetComponent<Property>();

        PropertReduction(property, skill);
    }

    /// <summary>
    /// 使用强化技能
    /// </summary>
    /// <param name="skill">Skill.</param>
    public static void Intensify(GameObject obj, Skill skill)
    {
        //添加强化的小图标
        UIScene.Instance.addStatusIcon(skill);
        //开始技能的持续时间
        skill.isInDuration = true;
        //获取HeroManager的属性
        Property property = obj.GetComponent<Property>();

        PropertIncrease(property, skill);

        SkillClass.Manager skillManager = obj.GetComponent<SkillClass.Manager>();

        skillManager.OnFinished(skill);
    }

    /// <summary>
    /// 强化技能的持续时间结束
    /// </summary>
    /// <param name="skill">Skill.</param>
    public static void IntensifyEnd(GameObject obj, Skill skill)
    {
        //删除强化的小图标
        UIScene.Instance.removeStatusIcon(skill);
        //结束技能的持续时间
        skill.isInDuration = false;
        //获取Hero的属性
        Property property = obj.GetComponent<Property>();

        PropertReduction(property, skill);
    }

    /// <summary>
    /// 属性的增强
    /// </summary>
    /// <param name="property">Property.</param>
    /// <param name="skill">Skill.</param>
    public static void PropertIncrease(Property property, Skill skill)
    {
        foreach (KeyValuePair<string, string> dict in skill.data)
        {
            if (PropertyTool.isExist(property, dict.Key))
            {
                //截取字符串，获得属性增加的值
                float createValue = float.Parse(dict.Value);
                //动态获取当前的属性值
                float propertyValue = float.Parse(PropertyTool.ReflectGetter(property, dict.Key).ToString());
                //使用强化技能时间结束后，应该减去强化属性
                PropertyTool.ReflectSetter(property, dict.Key, propertyValue + createValue);
            }
        }
    }


    /// <summary>
    /// 属性的减弱
    /// </summary>
    /// <param name="property">Property.</param>
    /// <param name="skill">Skill.</param>
    public static void PropertReduction(Property property, Skill skill)
    {
        foreach (KeyValuePair<string, string> dict in skill.data)
        {
            if (PropertyTool.isExist(property, dict.Key))
            {
                //截取字符串，获得属性增加的值
                float createValue = float.Parse(dict.Value);
                //动态获取当前的属性值
                float propertyValue = float.Parse(PropertyTool.ReflectGetter(property, dict.Key).ToString());
                //使用强化技能时间结束后，应该减去强化属性
                PropertyTool.ReflectSetter(property, dict.Key, propertyValue - createValue);
            }
        }
    }
}
