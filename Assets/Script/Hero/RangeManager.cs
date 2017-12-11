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
        radius = float.Parse(skill.data["skillDistance"]);
    }

    /// <summary>
    /// 搜索范围内的敌人
    /// </summary>
    /// <returns>The attack range.</returns>
    /// <param name="attackDistance">攻击范围半径.</param>
    public List<GameObject> SearchRangeEnemys(float attackDistance)
    {
        LayerMask layerMask = 1 << LayerMask.NameToLayer("Enemy");

        Collider[] colliders = Physics.OverlapSphere(transform.position, attackDistance, layerMask);

        List<GameObject> enemys = new List<GameObject>();

        for (int i = 0; i < colliders.Length; i++)
        {
            Quaternion r = transform.rotation;
            Vector3 f0 = (transform.position + (r * Vector3.forward) * attackDistance);
            Debug.DrawLine(transform.position, f0, Color.red);

            Quaternion r0 = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 30f, transform.rotation.eulerAngles.z);
            Quaternion r1 = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 30f, transform.rotation.eulerAngles.z);

            Vector3 f1 = (transform.position + (r0 * Vector3.forward) * attackDistance);
            Vector3 f2 = (transform.position + (r1 * Vector3.forward) * attackDistance);

            Debug.DrawLine(transform.position, f1, Color.red);
            Debug.DrawLine(transform.position, f2, Color.red);
            Debug.DrawLine(f1, f2, Color.red);

            Vector3 point = colliders[i].transform.position;

            if (Global.hero.rangeManager.IsInTriangle(point, transform.position, f1, f2))
            {
                enemys.Add(colliders[i].gameObject);
            }
        }

        return enemys;
    }


    public bool IsInTriangle(Vector3 point, Vector3 v0, Vector3 v1, Vector3 v2)
    {
        float x = point.x;
        float y = point.z;

        float v0x = v0.x;
        float v0y = v0.z;

        float v1x = v1.x;
        float v1y = v1.z;

        float v2x = v2.x;
        float v2y = v2.z;

        float t = triangleArea(v0x, v0y, v1x, v1y, v2x, v2y);
        float a = triangleArea(v0x, v0y, v1x, v1y, x, y) + triangleArea(v0x, v0y, x, y, v2x, v2y) + triangleArea(x, y, v1x, v1y, v2x, v2y);

        if (Mathf.Abs(t - a) <= 0.01f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    float triangleArea(float v0x, float v0y, float v1x, float v1y, float v2x, float v2y)
    {
        return Mathf.Abs((v0x * v1y + v1x * v2y + v2x * v0y
            - v1x * v0y - v2x * v1y - v0x * v2y) / 2f);
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