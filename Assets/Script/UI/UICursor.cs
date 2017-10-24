using System.Collections;
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
        if (Global.skillRelease == SkillRelease.none)
        {
            resetCursor();
        }

        if (Global.skillRelease == SkillRelease.selecting) 
        {
            setSkillSelecting();

        }

        if (Global.skillRelease == SkillRelease.selected)
        {
            resetCursor();
        }
	}

    void setSkillSelecting()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

    void resetCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
