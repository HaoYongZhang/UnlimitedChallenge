using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillClass;
public delegate void OnSkillEnterDelegate(Collider _collider, Skill skill);

public class SkillEffect : MonoBehaviour {
    public OnSkillEnterDelegate onSkillEnterDelegate;
    public Skill skill;
    public float moveSpeed = 10f;

    static string path = "Material/Effects/";

    float distance;
    Vector3 startPosition;

    public static SkillEffect NewInstantiate(Vector3 position, Quaternion rotation, Skill skill)
    {
        GameObject obj = Resources.Load(path + skill.data["skillPrefab"]) as GameObject;
        obj.transform.position = position;
        obj.transform.rotation = Quaternion.Euler(90, rotation.eulerAngles.y, 0);

        SkillEffect skillEffect = Instantiate(obj).GetComponent<SkillEffect>();
        skillEffect.skill = skill;
        skillEffect.distance = float.Parse(skill.data["skillDistance"]);

        return skillEffect;
    }


	// Use this for initialization
	void Start () {
        startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);

        if(skill != null)
        {
            float currentDistance = (startPosition - transform.position).magnitude;

            if(currentDistance >= distance)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider _collider)
    {
        if(onSkillEnterDelegate != null)
        {
            onSkillEnterDelegate(_collider, skill);
        }

    }
}
