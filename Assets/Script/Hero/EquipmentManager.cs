using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;

public class EquipmentManager : MonoBehaviour
{
    public Weapon leftWeapon;
    public Weapon rightWeapon;
    public Equipment head;
    public Equipment body;
    public Equipment legs;
    public Equipment shoes;
    public Equipment treasure;

    // Use this for initialization
    void Start()
    {
        leftWeapon = new Weapon("20001");

        GameObject leftWeaponObj = (GameObject)Instantiate(Resources.Load("Material/Weapon/weapon_" + leftWeapon.id));
        Global.hero.charactersManager.replaceAvator(Global.hero.charactersManager.left_weapon_name, leftWeaponObj);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

/// <summary>
/// 装备部位
/// </summary>
public enum EquipmentPart
{
    [Description("头")]
    head,
    [Description("上身")]
    body,
    [Description("下身")]
    legs,
    [Description("鞋子")]
    shoes,
    [Description("宝物")]
    treasure
}

/// <summary>
/// 武器类型
/// </summary>
public enum WeaponType
{
    [Description("不存在")]
    nothing,
    [Description("非武器")]
    none,
    [Description("剑")]
    sword,
    [Description("刀")]
    knife,
    [Description("枪")]
    spear,
    [Description("棍")]
    stick,
    [Description("机械手枪")]
    pistol,
    [Description("机械枪")]
    gun
}

/// <summary>
/// 伤害类型
/// </summary>
public enum DamageType
{
    [Description("物理")]
    physics,
    [Description("内力")]
    Force,
    [Description("魔法")]
    Magic,
    [Description("魔法")]
    other
}

