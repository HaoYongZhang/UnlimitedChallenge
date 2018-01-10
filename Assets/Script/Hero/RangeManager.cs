using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using SkillClass;

public enum RangeType
{
    circle,
    sector
}

public class RangeManager : MonoBehaviour
{
    public bool rendering;
    public float radius = 10f;
    public float angle = 60f;
    public RangeType rangeType;
                                    
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
            Vector3 center = new Vector3(transform.position.x, 3.2f, transform.position.z);
            if(rangeType == RangeType.circle)
            {
                DrawTool.DrawCircle(transform, center, radius);
            }
            else if (rangeType == RangeType.sector)
            {
                DrawTool.DrawSector(transform, center, angle, radius);
            }
            else
            {
                Debug.Log("尚未设置范围类型");
            }
        }
        else
        {
            DrawTool.clear(transform);
        }
    }

    public void SetCircleRange(float _radius)
    {
        rendering = true;
        radius = _radius;
        rangeType = RangeType.circle;
    }

    public void SetSectorRange(float _radius, float _angle)
    {
        rendering = true;
        radius = _radius;
        angle = _angle;
        rangeType = RangeType.sector;
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
            if(RangeTool.IsInSector(startTran, colliders[i].transform, attackDistance, angle))
            {
                enemys.Add(colliders[i].gameObject);
            }
        }

        return enemys;
    }


}