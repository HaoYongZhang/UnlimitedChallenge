﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillClass;

public class UICursor : MonoBehaviour {
    public static UICursor _instance;
    Texture2D cursorTexture;

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        cursorTexture = Resources.Load("Image/Skill/skill_cursor", typeof(Texture2D)) as Texture2D;
    }

    void Update () {
        if (Global.skillReleaseState == SkillReleaseState.available)
        {
            resetCursor();
        }

        if (Global.skillReleaseState == SkillReleaseState.selecting) 
        {
            setSkillSelecting();

        }

        if (Global.skillReleaseState == SkillReleaseState.selected)
        {
            resetCursor();
        }
	}

    void setSkillSelecting()
    {
        Cursor.SetCursor(cursorTexture, new Vector2(32, 32), CursorMode.Auto);
    }

    void resetCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
