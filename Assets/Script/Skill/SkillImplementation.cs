using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SkillClass;

public class SkillImplementation
{
    public static void Implement(GameObject obj, Skill skill, Vector3 selectedPosition)
    {
        switch (skill.type)
        {
            case SkillType.attack:
                {
                    skill.releasingDelegate = Attack;
                    Global.hero.fightManager.SkillAttack(skill);
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
                    skill.releasingDelegate = Specialty;
                    Global.hero.fightManager.SkillAttack(skill);
                }
                break;
        }
    }

    public static void Attack(Skill skill)
    {
        for (int i = 0; i < Global.hero.charactersManager.boneTransforms.Count; i++)
        {
            if (Global.hero.charactersManager.boneTransforms[i].name == (CharactersManager.prefixBoneName + CharactersManager.left_point_name))
            {
                Transform point = Global.hero.charactersManager.boneTransforms[i];

                SkillEffect skillEffect = SkillEffect.NewInstantiate(point.position, Global.hero.transform.rotation, skill);
                skillEffect.onSkillEnterDelegate = Global.hero.skillManager.OnSkill;

                break;
            }
        }
    }

    public static void Specialty(Skill skill)
    {
        SpecialtyAction action = EnumTool.GetEnum<SpecialtyAction>(skill.data["specialtyAction"]);

        switch (action)
        {
            case SpecialtyAction.custom:
                {

                }
                break;
            case SpecialtyAction.telesport:
                {
                    SpecialtySkills.Telesport(skill);
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
        PropertyManager propertyManager = obj.GetComponent<PropertyManager>();

        string increateHp = skill.data["increateHp"];

        if (increateHp != null && increateHp != "")
        {
            //截取字符串，获得属性增加的值
            float createValue = float.Parse(increateHp);
            //使用治疗技能后，加上的血量
            propertyManager.Hp += createValue;
        }

        if(skill.duration > 0)
        {
            //添加强化的小图标
            UIScene.Instance.addStatusIcon(skill);

            //开始技能的持续时间
            skill.isInDuration = true;

            PropertIncrease(propertyManager, skill);
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
        PropertyManager propertyManager = obj.GetComponent<PropertyManager>();

        PropertReduction(propertyManager, skill);
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
        PropertyManager propertyManager = obj.GetComponent<PropertyManager>();

        PropertIncrease(propertyManager, skill);

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
        PropertyManager propertyManager = obj.GetComponent<PropertyManager>();

        PropertReduction(propertyManager, skill);
    }

    /// <summary>
    /// 属性的增强
    /// </summary>
    /// <param name="propertyManager">Property.</param>
    /// <param name="skill">Skill.</param>
    public static void PropertIncrease(PropertyManager propertyManager, Skill skill)
    {
        foreach (KeyValuePair<string, string> dict in skill.data)
        {
            if (PropertyTool.isExist(propertyManager.basicProperty, dict.Key))
            {
                //是否百分比的值
                bool isRate = dict.Value.EndsWith("%");

                if(isRate)
                {
                    //截取百分号
                    float value = float.Parse(dict.Value.Substring(0, dict.Value.Length - 1));
                    //动态获取当前的属性值
                    float propertyValue = float.Parse(PropertyTool.ReflectGetter(propertyManager.rateProperty, dict.Key).ToString());
                    //使用强化技能时间结束后，应该减去强化属性
                    PropertyTool.ReflectSetter(propertyManager.rateProperty, dict.Key, propertyValue + value/100);
                }
                else
                {
                    //截取字符串，获得属性增加的值
                    float createValue = float.Parse(dict.Value);
                    //动态获取当前的属性值
                    float propertyValue = float.Parse(PropertyTool.ReflectGetter(propertyManager.basicProperty, dict.Key).ToString());
                    //使用强化技能时间结束后，应该减去强化属性
                    PropertyTool.ReflectSetter(propertyManager.basicProperty, dict.Key, propertyValue + createValue);
                }

            }
        }
    }


    /// <summary>
    /// 属性的减弱
    /// </summary>
    /// <param name="property">Property.</param>
    /// <param name="skill">Skill.</param>
    public static void PropertReduction(PropertyManager propertyManager, Skill skill)
    {
        foreach (KeyValuePair<string, string> dict in skill.data)
        {
            if (PropertyTool.isExist(propertyManager.basicProperty, dict.Key))
            {
                //是否百分比的值
                bool isRate = dict.Value.EndsWith("%");

                if (isRate)
                {
                    //截取百分号
                    float value = float.Parse(dict.Value.Substring(0, dict.Value.Length - 1));
                    //动态获取当前的属性值
                    float propertyValue = float.Parse(PropertyTool.ReflectGetter(propertyManager.rateProperty, dict.Key).ToString());
                    //使用强化技能时间结束后，应该减去强化属性
                    PropertyTool.ReflectSetter(propertyManager.rateProperty, dict.Key, propertyValue - value / 100);
                }
                else
                {
                    //截取字符串，获得属性增加的值
                    float value = float.Parse(dict.Value);
                    //动态获取当前的属性值
                    float propertyValue = float.Parse(PropertyTool.ReflectGetter(propertyManager.basicProperty, dict.Key).ToString());
                    //使用强化技能时间结束后，应该减去强化属性
                    PropertyTool.ReflectSetter(propertyManager.basicProperty, dict.Key, propertyValue - value);
                }
            }
        }
    }
}
