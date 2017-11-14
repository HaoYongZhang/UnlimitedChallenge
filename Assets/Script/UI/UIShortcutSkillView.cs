using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillClass;
using UnityEngine.EventSystems;

public class UIShortcutSkillView : MonoBehaviour {
    public GameObject shortcutSkillBar_1;
    public GameObject shortcutSkillBar_2;

    public List<SkillButton> skillBtns_1;
    public List<SkillButton> skillBtns_2;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < 5; i++)
        {
            SkillButton skillButton = SkillButton.NewInstantiate();
            skillButton.transform.SetParent(shortcutSkillBar_1.transform, false);

            UIMouseDelegate mouseDelegate = skillButton.gameObject.GetComponent<UIMouseDelegate>();
            mouseDelegate.onDropDelegate = onDropSkill;

            skillBtns_1.Add(skillButton);
        }

        for (int i = 0; i < 5; i++)
        {
            SkillButton skillButton = SkillButton.NewInstantiate();
            skillButton.transform.SetParent(shortcutSkillBar_2.transform, false);

            UIMouseDelegate mouseDelegate = skillButton.gameObject.GetComponent<UIMouseDelegate>();
            mouseDelegate.onDropDelegate = onDropSkill;

            skillBtns_2.Add(skillButton);
        }
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void FixedUpdate()
    {
        for (int i = 0; i < Global.shortcutSkills_1.Count; i++)
        {
            if(Global.shortcutSkills_1[i] !=  null)
            {
                if(skillBtns_1[i].skill == null || Global.shortcutSkills_1[i].id != skillBtns_1[i].skill.id)
                {
                    skillBtns_1[i].setSkill(SkillManager.GetOneSkillByID(Global.shortcutSkills_1[i].id));
                }
            }
            else
            {
                skillBtns_1[i].setSkill(null);
            }
        }

        for (int i = 0; i < Global.shortcutSkills_2.Count; i++)
        {
            if (Global.shortcutSkills_2[i] != null)
            {
                if (skillBtns_2[i].skill == null || Global.shortcutSkills_2[i].id != skillBtns_2[i].skill.id)
                {
                    skillBtns_2[i].setSkill(SkillManager.GetOneSkillByID(Global.shortcutSkills_2[i].id));
                }
            }
            else
            {
                skillBtns_2[i].setSkill(null);
            }
        }
    }

    void onDropSkill(GameObject obj, PointerEventData eventData)
    {
        GameObject dropObj = eventData.pointerDrag;
        Skill replaceSkill = dropObj.GetComponent<SkillButton>().skill;
        Skill oldSkill = obj.GetComponent<SkillButton>().skill;

        if(replaceSkill == null)
        {
            return;
        }


        if(obj.transform.parent.name == "ShortcutSkillBar_1")
        {
            for (int i = 0; i < Global.shortcutSkills_1.Count; i++)
            {
                if (Global.shortcutSkills_1[i] != null && Global.shortcutSkills_1[i].id == replaceSkill.id)
                {
                    Global.shortcutSkills_1[i] = null;
                }
            }

            int objIndex = skillBtns_1.IndexOf(obj.GetComponent<SkillButton>());
            Global.shortcutSkills_1[objIndex] = SkillManager.GetOneSkillByID(replaceSkill.id);
        }
        else
        {
            for (int i = 0; i < Global.shortcutSkills_2.Count; i++)
            {
                if (Global.shortcutSkills_2[i] != null && Global.shortcutSkills_2[i].id == replaceSkill.id)
                {
                    Global.shortcutSkills_2[i] = null;
                }
            }

            int objIndex = skillBtns_2.IndexOf(obj.GetComponent<SkillButton>());
            Global.shortcutSkills_2[objIndex] = SkillManager.GetOneSkillByID(replaceSkill.id);
        }
    }
}
