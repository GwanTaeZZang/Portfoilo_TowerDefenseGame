using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerModel
{
    public int Uid;
    public TowerType towerType;
    public int[] nextUid;
    public int grade;
    public float coolTime;
    public float atkInterval;
    public float atkRange;
}

public enum TowerType
{
    SingleFire,
    Boom,
    Debuff,
    Pierce,
    Launcher,
    AutoFire,
    Multi,
}