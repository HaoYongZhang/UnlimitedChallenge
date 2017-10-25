using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillClass;

public class ShootManager : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool handleSkill(Skill skill)
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;
        float distance;
        if (Physics.Raycast(cameraRay, out rayHit))
        {
            //人物到鼠标点击位置的实际直线距离
            distance = (transform.position - rayHit.point).magnitude;
            //点击位置大于施法距离
            if (distance > float.Parse(skill.addlData["skillDistance"]))
            {
                Debug.Log("点击位置大于施法距离");
                return false;
            }
            //点击位置小于施法距离
            //成功施法
            else
            {
                Debug.Log("点击位置小于施法距离");
                Vector3 rayPoint = new Vector3(rayHit.point.x, transform.position.y, rayHit.point.z);
                transform.LookAt(rayPoint);

                GameObject skillPoint = GameObject.Find("ShootPoint");
                GameObject bullet = (GameObject)Resources.Load("Skill/" + skill.addlData["skillPrefab"]);
                bullet = Instantiate(bullet, skillPoint.transform.position, skillPoint.transform.rotation);
                bullet.GetComponent<Shoot>().fire();
                bullet.GetComponent<CollisionEvent>().collisionEventDelegate =
                        (_collider, _colliderCount) =>
                    {
                        handleCollider(bullet, _collider, _colliderCount);
                    };

                //Debug.Log(transform.position);
                //Debug.Log(rayHit.point);
                //Debug.Log("实际距离=" + distance);
                //Debug.Log("技能距离=" + float.Parse(skill.addlData["skillDistance"]));

                return true;
            }

        }
        else
        {
            Debug.Log("点击位置超出游戏范围");
            return false;
        }
    }

    void handleCollider(GameObject bullet, Collider _collider, int _colliderCount)
    {
        Destroy(bullet);
    }
}
