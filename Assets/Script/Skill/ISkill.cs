using System;
using System.Collections.Generic;
using System.ComponentModel;
using SkillClass;

public interface ISkill
{
    /// <summary>
    /// 检验能否释放技能
    /// </summary>
    /// <returns><c>true</c>, if release was caned, <c>false</c> otherwise.</returns>
    /// <param name="skill">Skill.</param>
    bool CanRelease(Skill skill);

    /// <summary>
    /// 选择技能释放的目标
    /// </summary>
    /// <param name="skill">Skill.</param>
    void OnSelecting(Skill skill);

    /// <summary>
    /// 检验技能释放目标是否选择成功
    /// </summary>
    /// <param name="skill">Skill.</param>
    bool CanSelected(Skill skill);

    /// <summary>
    /// 释放技能
    /// </summary>
    /// <param name="skill">Skill.</param>
    void OnRelease(Skill skill);

    /// <summary>
    /// 结束技能持续效果
    /// </summary>
    /// <param name="skill">Skill.</param>
    void OnDurationEnd(Skill skill);
}
