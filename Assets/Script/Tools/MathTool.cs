using UnityEngine;
using System.Collections;

public class MathTool
{
    public static float Round(float f, int acc)
    {
        float temp = f * Mathf.Pow(10, acc);
        return Mathf.Round(temp) / Mathf.Pow(10, acc);
    }


    public static bool IsFacingRight(Transform t)  
    {  
        if (t.localEulerAngles.y > 0) return false;  
        else return true;  
    }  
}
