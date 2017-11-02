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
    public GameObject tabbar;

    List<GameObject> views = new List<GameObject>();
    List<Button> tabbarButtons = new List<Button>();
    List<string> tabbarTexts = new List<string>();

    Sprite selectedBtnBG;
    Sprite unselectedBtnBG;
    //bool hasLoad;

    // Use this for initialization
    void Start () {
        selectedBtnBG = Resources.Load("Sprites/Buttons/Basic/Filled/PNG/" + "White-Button", typeof(Sprite)) as Sprite;
        unselectedBtnBG = Resources.Load("Sprites/Buttons/Basic/Filled/PNG/" + "BlueDark-Button", typeof(Sprite)) as Sprite;

        views.Add(mainView);
        views.Add(skillsView);  
        views.Add(itemsView);

        tabbarButtons = new List<Button>(tabbar.GetComponentsInChildren<Button>());

        for (int i = 0; i < tabbarButtons.Count; i++)
        {
            int j = i;
            tabbarButtons[i].onClick.AddListener(delegate ()
            {
                onTabBar(j);
            });

            tabbarTexts.Add(tabbarButtons[j].GetComponentInChildren<Text>().text);
        }
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

        strengthText.text = "力量：" + "<color=#98FF67>" + property.strength.ToString() + "</color>";
        agilityText.text = "敏捷：" + "<color=#98FF67>" + property.agility.ToString() + "</color>";
        intellectText.text = "能量：" + "<color=#98FF67>" + property.intellect.ToString() + "</color>";

        attackText.text = "攻击力：" + "<color=#98FF67>" + property.attack.ToString() + "</color>";
        armorText.text = "护甲：" + "<color=#98FF67>" + property.armor.ToString() + "</color>";
        moveSpeedText.text = "移动速度：" + "<color=#98FF67>" + property.moveSpeed.ToString() + "</color>";
         
            
	}       

    void onTabBar(int i)
    {
        
        for (int j = 0; j < views.Count; j++)
        {
            Image image = tabbarButtons[j].GetComponent<Image>();
            Text label = tabbarButtons[j].GetComponentInChildren<Text>();
            //当前选中的
            if(i == j)
            {
                views[j].SetActive(true);
                image.sprite = selectedBtnBG;
                label.text = "<color=#333333>" + tabbarTexts[j] + "</color>";
            }
            else
            {
                views[j].SetActive(false);
                image.sprite = unselectedBtnBG;
                label.text = "<color=#ffffff>" + tabbarTexts[j] + "</color>";
            }
        }
    }
}           
            
