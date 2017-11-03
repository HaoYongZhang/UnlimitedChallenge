using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using SkillClass;

public class RangeManager : MonoBehaviour
{

    int pointCount = 50;
    float radius = 10f;
    float angle;
    List<Vector3> points = new List<Vector3>();
    LineRenderer lineRenderer;
    //用于标识是否显示
    public bool rendering;  
                                    
    void Start()
    {
        angle = 360f / pointCount;
        lineRenderer = GetComponent<LineRenderer>();
        if (!lineRenderer)
        {
            Debug.LogError("LineRender is NULL!");
        }
    }

    void Update()
    {
        if (Global.skillRelease == SkillRelease.none)
        {
            rendering = false;
        }

        if (Global.skillRelease == SkillRelease.selecting)
        {
            rendering = true;

        }

        if (Global.skillRelease == SkillRelease.selected) 
        {
            rendering = false;
        }

        if (rendering)
        {
            //这里是设置圆的点数，加1是因为加了一个终点（起点）
            lineRenderer.positionCount = pointCount + 1;
            CalculationPoints();
            DrowPoints();
        }
        else
        {
            //不显示时设置
            lineRenderer.positionCount = 0;
        }

        ClearPoints();
    }

    public void setSkillRange(Skill skill)
    {
        radius = float.Parse(skill.addlData["skillDistance"]);
    }

    /// <summary>
    /// 搜索攻击范围内的敌人
    /// </summary>
    /// <returns>The attack range.</returns>
    /// <param name="attackRange">Attack range.</param>
    public List<GameObject> SearchAttackRangeEnemys(float attackRange)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange);

        List<GameObject> enemys = new List<GameObject>();

        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                enemys.Add(colliders[i].gameObject);
                //Debug.Log(colliders[i].gameObject.name);
            }
        }

        return enemys;
    }

    void CalculationPoints()
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
        Vector3 v = newPosition + transform.forward * radius;
        points.Add(v);
        Quaternion r = transform.rotation;
        for (int i = 1; i < pointCount; i++)
        {
            Quaternion q = Quaternion.Euler(r.eulerAngles.x, r.eulerAngles.y - (angle * i), r.eulerAngles.z);
            v = newPosition + (q * Vector3.forward) * radius;
            points.Add(v);
        }
    }
    void DrowPoints()
    {
        for (int i = 0; i < points.Count; i++)
        {
            lineRenderer.SetPosition(i, points[i]);  //把所有点添加到positions里
        }
        if (points.Count > 0)   //这里要说明一下，因为圆是闭合的曲线，最后的终点也就是起点，
            lineRenderer.SetPosition(pointCount, points[0]);
    }
    void ClearPoints()
    {
        points.Clear();  ///清除所有点
    }
}