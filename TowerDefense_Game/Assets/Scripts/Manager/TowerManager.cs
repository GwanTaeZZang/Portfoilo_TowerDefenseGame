using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : Singleton<TowerManager>
{
    private List<BuildAbleTileObject> buildAbleTileList = new List<BuildAbleTileObject>();
    private Dictionary<int, BuildAbleTileObject> buildAbleTileDict = new Dictionary<int, BuildAbleTileObject>();


    public void AddBuildAbleTile(BuildAbleTileObject _tileObject)
    {
        buildAbleTileList.Add(_tileObject);
    }

    public List<BuildAbleTileObject> GetBuildAbleTileList()
    {
        return buildAbleTileList;
    }

    public int GetBuildAbleTileCount()
    {
        return buildAbleTileList.Count;
    }


    //public void AddBuildAbleTileObject(int _tileIdx, BuildAbleTileObject _tileObject)
    //{
    //    buildAbleTileDict.Add(_tileIdx, _tileObject);
    //}

    //public Dictionary<int, BuildAbleTileObject> GetBuildAbleTileDict()
    //{
    //    return buildAbleTileDict;
    //}

    //public int GetBuildAbleTileDictCount()
    //{
    //    return buildAbleTileDict.Count;
    //}
}
