using UnityEngine;
using System.Collections;
using System.Globalization;
using System;
using SkillClass;

public class ColorTool
{
    public static Color getColor(string hexColor)
    {
        hexColor = hexColor.ToUpper();

        if(hexColor.StartsWith("#", StringComparison.CurrentCulture))
        {
            hexColor = hexColor.Substring(1, hexColor.Length - 1);
        }

        if(hexColor.Length < 6)
        {
            Debug.LogError("十六进制颜色格式错误");
            return Color.black;
        }

        int location = 0;
        int length = 2;

        int r = Int32.Parse(hexColor.Substring(location, length), NumberStyles.HexNumber);
        location = 2;
        int g = Int32.Parse(hexColor.Substring(location, length), NumberStyles.HexNumber);
        location = 4;
        int b = Int32.Parse(hexColor.Substring(location, length), NumberStyles.HexNumber);

        Color color = new Color(r / 255f, g / 255f, b / 255f, 1);

        return color;
    }

    public static Color getColor(int r, int g, int b)
    {
        Color color = new Color(r / 255f, g / 255f, b / 255f, 1);

        return color;
    }

    public static Color GetSkillColor(SkillRank rank)
    {
        switch (rank)
        {
            case SkillRank.S:
                {
                    //黄金
                    return ColorTool.getColor("#ffd700");
                }
            case SkillRank.A:
                {
                    //紫色
                    return ColorTool.getColor(140, 105, 210);
                }
            case SkillRank.B:
                {
                    //褐色
                    return ColorTool.getColor(99, 63, 63);
                }
            case SkillRank.C:
                {
                    //钢铁蓝
                    return ColorTool.getColor("#4682b4");
                }
            case SkillRank.D:
                {
                    //青色
                    return ColorTool.getColor("#2e8b57");
                }
            default:
                {
                    return ColorTool.getColor("#000000");
                }
        }
    }
}
