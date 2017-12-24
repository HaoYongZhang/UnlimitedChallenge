using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UIHeroInfo
{
    public class MainView : MonoBehaviour 
    {
        public PropertyView propertyView;
        public SkillView skillsView;
        public ItemView itemsView;

        public GameObject tabbar;

        List<GameObject> views = new List<GameObject>();
        List<Button> tabbarButtons = new List<Button>();

        int currentTag = 0;

        public static Sprite selectedBtnBG;
        public static Sprite unselectedBtnBG;

        // Use this for initialization
        void Start()
        {
            selectedBtnBG = Resources.Load("Sprites/Buttons/Basic/Filled/PNG/" + "White-Button", typeof(Sprite)) as Sprite;
            unselectedBtnBG = Resources.Load("Sprites/Buttons/Basic/Filled/PNG/" + "BlueDark-Button", typeof(Sprite)) as Sprite;

            views.Add(propertyView.gameObject);
            views.Add(skillsView.gameObject);
            views.Add(itemsView.gameObject);

            tabbarButtons = new List<Button>(tabbar.GetComponentsInChildren<Button>());

            for (int i = 0; i < tabbarButtons.Count; i++)
            {
                int j = i;
                tabbarButtons[i].onClick.AddListener(delegate ()
                {
                    onTabBar(j);
                });
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        void onTabBar(int i)
        {
            if (currentTag == i)
            {
                return;
            }

            currentTag = i;

            for (int j = 0; j < views.Count; j++)
            {
                Image image = tabbarButtons[j].GetComponent<Image>();
                Text label = tabbarButtons[j].GetComponentInChildren<Text>();
                //当前选中的
                if (i == j)
                {
                    views[j].SetActive(true);
                    image.sprite = selectedBtnBG;
                    label.color = ColorTool.getColor("#333333");
                }
                else
                {
                    views[j].SetActive(false);
                    image.sprite = unselectedBtnBG;
                    label.color = ColorTool.getColor("#ffffff");
                }
            }
        }

        public void show()
        {
            gameObject.SetActive(true);
        }

        public void hide()
        {
            gameObject.SetActive(false);
            UIScene.Instance.hideSkillInfo();
        }
    
    }           
}

