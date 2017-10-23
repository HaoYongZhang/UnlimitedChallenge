using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knowledge {
    //------近战（CloseCombat）
    //剑法
    public int sword{ get; set; }
    //刀法
    public int knife{ get; set; }
    //枪法
    public int spear{ get; set; }
    //棍法
    public int stick{ get; set; }
    //拳法
    public int fist{ get; set; }
    //掌法
    public int palm{ get; set; }
    //爪法
    public int talon{ get; set; }

    //------远程（RemoteCombat）
    //射击
    public int shot{ get; set; }
    //魔法知识
    public int magic{ get; set; }

    //------特殊（Special）
    //毒
    public int poison{ get; set; }
}
