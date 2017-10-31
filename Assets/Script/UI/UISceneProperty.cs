using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

public class UISceneProperty : MonoBehaviour {

    public Slider hpBar;
    public Slider mpBar;
    public Text hpText;
    public Text mpText;
    public Text hpRegenerationText;
    public Text mpRegenerationText;
    public Text strengthText;
    public Text agilityText;
    public Text intellectText;
    public Text attackText;
    public Text armorText;
    public Text moveSpeedText;
        
    public GameObject mainView;
    public GameObject skillsView;
    public GameObject itemsView;

    List<Button> propertyTabbar;
    List<GameObject> views = new List<GameObject>();

    //bool hasLoad;

    // Use this for initialization
    void Start () {
        views.Add(mainView);
        views.Add(skillsView);
        views.Add(itemsView);

        propertyTabbar = new List<Button>(GameObject.Find("PropertyTabbar").GetComponentsInChildren<Button>());

        propertyTabbar[0].onClick.AddListener(delegate ()
        {
            onTabBar(propertyTabbar[0]);
        });

        propertyTabbar[1].onClick.AddListener(delegate ()
        {
            onTabBar(propertyTabbar[1]);
        });

        propertyTabbar[2].onClick.AddListener(delegate ()
        {
            onTabBar(propertyTabbar[2]);
        });

	}
	
	// Update is called once per frame
	void Update () {
        Property property = Global.hero.property;

        hpBar.value = property.hp / property.hpMax;
        mpBar.value = property.mp / property.mpMax;
        hpText.text = property.hp + "/" + property.hpMax;
        mpText.text = property.mp + "/" + property.mpMax;

        if (property.hpRegeneration > 0)
        {
            hpRegenerationText.text = "+" + Math.Round(property.hpRegeneration, 1);
        }
        else
        {
            hpRegenerationText.text = "-" + Math.Round(property.hpRegeneration, 1);
        }

        if (property.mpRegeneration > 0)
        {
            mpRegenerationText.text = "+" + Math.Round(property.mpRegeneration, 1);
        }
        else
        {
            mpRegenerationText.text = "-" + Math.Round(property.mpRegeneration, 1);
        }

        strengthText.text = "    力量：" + "<color=#98FF67>" + property.strength.ToString() + "</color>";
        agilityText.text = "    敏捷：" + "<color=#98FF67>" + property.agility.ToString() + "</color>";
        intellectText.text = "    能量：" + "<color=#98FF67>" + property.intellect.ToString() + "</color>";

        attackText.text = "    攻击力：" + "<color=#98FF67>" + property.attack.ToString() + "</color>";
        armorText.text = "    护甲：" + "<color=#98FF67>" + property.armor.ToString() + "</color>";
        moveSpeedText.text = "    移动速度：" + "<color=#98FF67>" + property.moveSpeed.ToString() + "</color>";
         
            
	}       

    void onTabBar(Button clickBtn)
    {
        for (int j = 0; j < views.Count; j++)
        {
            if(clickBtn == propertyTabbar[j])
            {
                views[j].SetActive(true);
            }
            else
            {
                views[j].SetActive(false);
            }
        }
    }
}           
            
