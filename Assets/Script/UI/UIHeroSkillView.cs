using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SkillClass;
using UnityEngine.EventSystems;

public class UIHeroSkillView : MonoBehaviour {
    
    public GameObject skillSet;
    public GameObject buttonList_1;
    public GameObject buttonList_2;

    public List<Button> skillTypeButtonList = new List<Button>();
    List<string> skillTypeButtonTexts = new List<string>();

    int currentTag = 0;

	void Start () {
        foreach(Button btn in buttonList_1.GetComponentsInChildren<Button>())
        {
            skillTypeButtonList.Add(btn);
        }

        foreach (Button btn in buttonList_2.GetComponentsInChildren<Button>())
        {
            skillTypeButtonList.Add(btn);
        }

        for (int i = 0; i < skillTypeButtonList.Count; i++)
        {
            int j = i;
            skillTypeButtonList[i].onClick.AddListener(delegate ()
            {
                onClick(j);
            });

            skillTypeButtonTexts.Add(skillTypeButtonList[j].GetComponentInChildren<Text>().text);
        }

        setSkillSet(0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void onClick(int i)
    {
        if(currentTag == i)
        {
            return;
        }

        currentTag = i;

        //清除所有技能列表
        for (int ii = 0; ii < skillSet.transform.childCount; ii++) {  
            Destroy (skillSet.transform.GetChild (ii).gameObject);  
        }

        //改变按钮颜色
        for (int j = 0; j < skillTypeButtonList.Count; j++)
        {
            Image image = skillTypeButtonList[j].GetComponent<Image>();
            Text label = skillTypeButtonList[j].GetComponentInChildren<Text>();
            //当前选中的
            if (i == j)
            {
                image.sprite = UIHeroView.selectedBtnBG;
                label.text = "<color=#333333>" + skillTypeButtonTexts[j] + "</color>";
            }
            else
            {
                image.sprite = UIHeroView.unselectedBtnBG;
                label.text = "<color=#ffffff>" + skillTypeButtonTexts[j] + "</color>";
            }
        }

        setSkillSet(i);
    }

    void setSkillSet(int i){
        //由i + 1获取skillType
        SkillType type = (SkillType)Enum.Parse(typeof(SkillType), (i + 1).ToString());

        foreach(Skill skill in Global.skills)
        {
            if(skill.type == type)
            {
                SkillButton skillButton = SkillButton.NewInstantiate(skill);
                skillButton.transform.SetParent(skillSet.transform, false);

                UIMouseDelegate mouseDelegate = skillButton.gameObject.GetComponent<UIMouseDelegate>();
                mouseDelegate.onPointerClickDelegate = onClickSkillButton;
                mouseDelegate.onPointerEnterDelegate = UIScene.Instance.onPointerEnterSkillButton;
                mouseDelegate.onPointerExitDelegate = UIScene.Instance.onPointerExitSkillButton;
            }
        }
    }

    void onClickSkillButton(GameObject obj, PointerEventData ed)
    {
        UIScene.Instance.sceneProperty.SetActive(false);
        UIScene.Instance.hideSkillInfo();

        SkillButton skillBtn = obj.GetComponent<SkillButton>();

        Global.hero.skillManager.useSkill(skillBtn.skill);
    }
}
