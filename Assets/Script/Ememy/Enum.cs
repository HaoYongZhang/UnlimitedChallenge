using UnityEngine;
using System.Collections;
using System.ComponentModel;

namespace EnemyClass
{
    public enum Sense
    {
        none,
        senseRange,
        attackRange
    }

    public enum Action
    {
        idle,
        chase,
        attack,
        death
    }
}
