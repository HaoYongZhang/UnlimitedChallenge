using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillClass;
public delegate void OnSkillEnterDelegate(Collider _collider, Skill skill);

public class SkillEffect : MonoBehaviour {
    public OnSkillEnterDelegate onSkillEnterDelegate;
    public GameObject explosion;
    public Skill skill;
    public float moveSpeed = 10f;
    public float height;

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
        float skillEffectHeight = transform.position.y;

        if(height > 0)
        {
            skillEffectHeight = height;
        }
        transform.position = new Vector3(transform.position.x, skillEffectHeight, transform.position.z);
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
        if(_collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Vector3 explosionPosition = new Vector3(_collider.transform.position.x, transform.position.y, _collider.transform.position.z);
            Instantiate(explosion, explosionPosition, transform.rotation);
        }

        if(onSkillEnterDelegate != null)
        {
            onSkillEnterDelegate(_collider, skill);
        }

    }
}
