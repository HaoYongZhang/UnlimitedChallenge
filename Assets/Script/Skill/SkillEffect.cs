using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillClass;
public delegate void OnSkillEnterDelegate(GameObject obj, Skill skill);

public class SkillEffect : MonoBehaviour {
    public OnSkillEnterDelegate onSkillEnterDelegate;
    public GameObject explosion;
    public Skill skill;

    public float moveSpeed = 10f;
    public float height;

    float distance;
    SkillActionRange actionRange;
    Vector3 startPosition;
    bool canMove = true;

    public static SkillEffect NewInstantiate(Vector3 position, Quaternion rotation, Skill skill)
    {
        string path = "Material/Effects/";

        GameObject obj = Resources.Load(path + skill.data["prefab"]) as GameObject;
        obj.transform.position = position;
        obj.transform.rotation = Quaternion.Euler(90, rotation.eulerAngles.y, 0);

        SkillEffect skillEffect = Instantiate(obj).GetComponent<SkillEffect>();
        skillEffect.skill = skill;
        skillEffect.distance = float.Parse(skill.data["distance"]);
        skillEffect.actionRange = EnumTool.GetEnum<SkillActionRange>(skill.data["actionRange"]);

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

        if (
            actionRange == SkillActionRange.sector_small ||
            actionRange == SkillActionRange.sector_medium ||
            actionRange == SkillActionRange.sector_large
        )
        {
            canMove = false;

            Destroy(gameObject, 2);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        if(canMove)
        {
            transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);

            if (skill != null)
            {
                float currentDistance = (startPosition - transform.position).magnitude;

                if (currentDistance >= distance)
                {
                    Destroy(gameObject);
                }
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
            onSkillEnterDelegate(_collider.gameObject, skill);
        }
    }
}
