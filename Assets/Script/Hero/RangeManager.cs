using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using SkillClass;

public enum RangeType
{
    
}

public class RangeManager : MonoBehaviour
{
    //用于标识是否显示
    public bool rendering;
    public float radius = 10f;
                                    
    void Start()
    {
        
    }

    void Update()
    {
        if (Global.skillReleaseState != SkillReleaseState.selecting)
        {
            rendering = false;
        }

        if (rendering)
        {
            DrawTool.DrawCircle(transform, transform.position, radius);
        }
        else
        {
            DrawTool.clear(transform);
        }


    }

    /// <summary>
    /// 搜索范围内的敌人
    /// </summary>
    /// <returns>The attack range.</returns>
    /// <param name="attackDistance">攻击范围半径.</param>
    public List<GameObject> SearchRangeEnemys(Transform startTran, float attackDistance, float angle)
    {
        LayerMask layerMask = 1 << LayerMask.NameToLayer("Enemy");

        Collider[] colliders = Physics.OverlapSphere(transform.position, attackDistance, layerMask);

        List<GameObject> enemys = new List<GameObject>();

        for (int i = 0; i < colliders.Length; i++)
        {
            if(IsInSector(startTran, colliders[i].transform, attackDistance, angle))
            {
                enemys.Add(colliders[i].gameObject);
            }
        }

        return enemys;
    }

    public bool IsInSector(Transform startTran, Transform endTran, float distance, float angle)
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
}