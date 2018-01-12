using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillClass;

public class RangeTool {

    /// <summary>
    /// 判断是否在扇形范围内
    /// </summary>
    /// <returns><c>true</c>, if in sector was ised, <c>false</c> otherwise.</returns>
    /// <param name="startTran">Start tran.</param>
    /// <param name="endTran">End tran.</param>
    /// <param name="distance">Distance.</param>
    /// <param name="angle">Angle.</param>
    public static bool IsInSector(Transform startTran, Transform endTran, float distance, float angle)
    {
        Vector3 p_1 = new Vector3(startTran.position.x, 5, startTran.position.z);
        Vector3 p_2 = new Vector3(endTran.position.x, 5, endTran.position.z);

        //求出两点之间的距离
        float pointsDistance = Vector3.Distance(p_1, p_2);
        //start的正前方向量
        Vector3 v_forward = startTran.rotation * Vector3.forward;
        //p_1指向p_2的向量
        Vector3 v_points = p_2 - p_1;
        //计算两个向量间的夹角
        float pointsAngle = Mathf.Acos(Vector3.Dot(v_forward.normalized, v_points.normalized)) * Mathf.Rad2Deg;
        //当距离少于两点间距离时
        if (pointsDistance <= distance)
        {
            //当两点的向量间的夹角少于角度时
            if (pointsAngle <= angle * 0.5f)
            {
                Debug.Log("在扇形范围内");
                return true;
            }
        }

        return false;
    }

    public static float GetSectorAngle(SkillActionRange actionRange)
    {
        if(actionRange == SkillActionRange.sector_small)
        {
            return 60f;
        }
        else if (actionRange == SkillActionRange.sector_medium)
        {
            return 90f;
        }
        else if (actionRange == SkillActionRange.sector_large)
        {
            return 120f;
        }
        else
        {
            return 60f;
        }
    }

    public static bool IsSectorActionRange(SkillActionRange actionRange)
    {
        if (actionRange == SkillActionRange.sector_small ||
            actionRange == SkillActionRange.sector_medium ||
            actionRange == SkillActionRange.sector_large)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
