using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;

public class CharacterSelect : MonoBehaviour
{
    
    public Texture2D icon_1; // hero1
    public Texture2D icon_2; // hero2
    public Texture2D icon_3; // hero3
    public Vector2 scrollPos = Vector2.zero;
    private string mouseover; // 鼠标移入选中英雄标识

    float positionY; // hero详情框Y轴位移值
    int speed = 0; // hero详情框初始值
    private string flagName; // 是否重新初始化hero详情框初始值标识
    private void OnGUI()
    {

        if(Input.GetKeyDown(KeyCode.A))
        {
            ///GUI.ScrollTo(new Rect(Global.screenWidth / 3, 0, Global.screenWidth, Global.screenHeight));
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            //GUI.ScrollTo(new Rect(Global.screenWidth / 3, 0, Global.screenWidth, Global.screenHeight));
        }
        //  滚动开始
        scrollPos = GUI.BeginScrollView(
            new Rect(0, 0, Global.screenWidth, Global.screenHeight), 
            scrollPos, 
            new Rect(0, 0, Global.screenWidth, 0),
            false,
            false
            );
        bool onButton_1 = GUI.Button(
            new Rect(0, 0, Global.screenWidth / 3, Global.screenHeight),
            new GUIContent(icon_1, "overBtn_1")
            );
        bool onButton_2 = GUI.Button(
            new Rect(Global.screenWidth / 3, 0, Global.screenWidth / 3, Global.screenHeight), 
            new GUIContent(icon_2, "overBtn_2")
            );
        bool onButton_3 = GUI.Button(
            new Rect(Global.screenWidth / 3 * 2, 0, Global.screenWidth / 3, Global.screenHeight), 
            new GUIContent(icon_3, "overBtn_3")
            );
        if (onButton_1)
        {
           
        }
        if (onButton_2)
        {  
            
        }
        if (onButton_3)
        {
            //GUI.ScrollTo(new Rect(Global.screenWidth / 3, 0, Global.screenWidth, Global.screenHeight));
        }
        //  滚动结束
        GUI.EndScrollView();
        
        buttoncheck();
    }

    private void transitionBox(string mouseoverName)
    {
        string name = mouseoverName;
        if (name != flagName || flagName == null)
        {
            speed = 0;
        }
        if (speed == 150)
        {
            return;
        }
        speed += 5;
        positionY = Global.screenHeight - speed;        
    }

    void buttoncheck()
    {
        mouseover = GUI.tooltip;
        //flagName = mouseover;
        if (mouseover == "overBtn_1")
        {
            this.transitionBox(mouseover);
            GUI.Box(new Rect(0, Global.screenHeight - speed, Global.screenWidth / 3, 150), "1");
            flagName = mouseover;
        }
        else if (mouseover == "overBtn_2")
        {
            this.transitionBox(mouseover);
            GUI.Box(new Rect(Global.screenWidth / 3, Global.screenHeight - speed, Global.screenWidth / 3, 150), "2");
            flagName = mouseover;
        }
        else if (mouseover == "overBtn_3")
        {
            this.transitionBox(mouseover);
            GUI.Box(new Rect(Global.screenWidth / 3 * 2, Global.screenHeight - speed, Global.screenWidth / 3, 150), "3");
            flagName = mouseover;
        }
    }

}