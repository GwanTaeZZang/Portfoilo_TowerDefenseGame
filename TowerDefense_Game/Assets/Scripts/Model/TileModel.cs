using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TileModel
{
    public Transform transform;
    public int tileId;
    public bool moveAble;
    public bool towerBuledAble;
    public bool isEndWay;
}

public enum TileType
{
    MoveAble,
    MoveDisable,
    TowerBuildAble,
}
