using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.ComponentModel;
namespace ItemClass
{
    /// <summary>
    /// 物品的类别
    /// </summary>
    public enum ItemCategory
    {
        [Description("常规")]
        normal,
        [Description("任务")]
        mission,
        [Description("特殊")]
        special
    }
}
