using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillClass;

public class SpecialtySkills {
    /// <summary>
    /// 瞬移
    /// </summary>
    /// <returns>The telesport.</returns>
    /// <param name="skill">Skill.</param>
    public static void Telesport(Skill skill)
    {
        string path = "Material/Effects/";
        GameObject obj = Resources.Load(path + "Explosions/StarExplosion_white") as GameObject;
        Global.hero.skillManager.SkillEffect(obj);
        Global.hero.rigid.position = Global.hero.skillManager.selectedPosition;
    }
}
