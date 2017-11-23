using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

public class UIHeroView : MonoBehaviour {
        
    public GameObject mainView;
    public GameObject skillsView;
    public GameObject itemsView;
    public GameObject shortcutSkillView;

    public GameObject tabbar;

    List<GameObject> views = new List<GameObject>();
    List<Button> tabbarButtons = new List<Button>();
    List<string> tabbarTexts = new List<string>();

    int currentTag = 0;

    public static Sprite selectedBtnBG;
    public static Sprite unselectedBtnBG;

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
        
	}       

    void onTabBar(int i)
    {
        if(currentTag == i)
        {
            return;
        }

        currentTag = i;

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

    public void show()
    {
        UIScene.Instance.heroView.SetActive(true);
    }

    public void hide()
    {
        UIScene.Instance.heroView.SetActive(false);
        UIScene.Instance.hideSkillInfo();
    }
}           
            
