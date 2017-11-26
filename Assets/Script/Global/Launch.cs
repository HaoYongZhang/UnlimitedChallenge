using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour {

    [RuntimeInitializeOnLoadMethod]
    static void initialize()
    {
        DataManager.Instance.loadCSVData();

        Global.ranks.Add("S", new Rank("S"));
        Global.ranks.Add("A", new Rank("A"));
        Global.ranks.Add("B", new Rank("B"));
        Global.ranks.Add("C", new Rank("C"));
        Global.ranks.Add("D", new Rank("D"));
    }
}
