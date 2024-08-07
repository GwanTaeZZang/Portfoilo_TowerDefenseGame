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




public class BuildAbleTileObject
{
    public int tileIdx;
    public TowerController towerController;
    public int buildTowerId;
    public Vector2 position;

    public BuildAbleTileObject(int _tileIdx, Vector2 _position)
    {
        tileIdx = _tileIdx;
        position = _position;
        SetTowerId(-1);
    }

    public void SetTowerController(TowerController _towerController)
    {
        towerController = _towerController;
    }
    public TowerController GetTowerController()
    {
        return towerController;
    }

    public void SetTowerId(int _towerId)
    {
        buildTowerId = _towerId;
    }
    public int GetTowerId()
    {
        return buildTowerId;
    }
}
