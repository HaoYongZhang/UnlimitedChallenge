using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour {

    [RuntimeInitializeOnLoadMethod]
    static void initialize()
    {
        Debug.Log("初始化");
        HeroModel hero = new HeroModel();
        Debug.Log(hero.hp);
    }
}
