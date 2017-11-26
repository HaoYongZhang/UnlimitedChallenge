using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Rank
{
    public string name;
    public Sprite image;

    public Rank(string _name)
    {
        name = _name;
        image = Resources.Load("Image/Rank/" + _name + "-Rank", typeof(Sprite)) as Sprite;
    }

    //public static string getRankBGPath(string rank)
    //{
    //    return "Image/Rank/" + rank + "-Rank";
    //}
}
