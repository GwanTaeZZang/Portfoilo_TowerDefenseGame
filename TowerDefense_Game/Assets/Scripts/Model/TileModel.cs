using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TileModel
{
    public TileType tileType;
    public Transform transform;
    public int tileId;
    public bool moveAble;
    public bool towerBuledAble;
    public bool isEndWay;
}

public enum TileType
{
    MoveDisable,
    MoveAble,
    TowerBuildAble,
    End
}




[System.Serializable]
public struct MapData
{
    public int id;
    public int wid;
    public int goalIdx;
    public TileData[] tiles;
    public RouteData[] routes;
}

[System.Serializable]
public struct TileData
{
    public int imageIdx;
    public bool moveable;
    public bool buildable;
}

[System.Serializable]
public struct WaveData
{
    public SpawnData[] spawnDatas;
}

[System.Serializable]
public struct SpawnData
{
    //.. TODO :: monster
    public int routeIdx;
}

[System.Serializable]
public struct RouteData
{
    public int[] tileIdxs;
}
