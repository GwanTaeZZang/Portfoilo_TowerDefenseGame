using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : Singleton<TowerManager>
{
    //private List<BuildAbleTileObject> buildAbleTileList = new List<BuildAbleTileObject>();
    private Dictionary<int, TowerController> towerControllerDict = new Dictionary<int, TowerController>();


    //public void AddBuildAbleTile(BuildAbleTileObject _tileObject)
    //{
    //    buildAbleTileList.Add(_tileObject);
    //}

    //public List<BuildAbleTileObject> GetBuildAbleTileList()
    //{
    //    return buildAbleTileList;
    //}

    //public int GetBuildAbleTileCount()
    //{
    //    return buildAbleTileList.Count;
    //}

    public void AddTowerControllerDictKey(int _tileIdx, TowerController _towerController)
    {
        towerControllerDict.Add(_tileIdx, _towerController);
    }

    public Dictionary<int, TowerController> GetTowerControllerDict()
    {
        return towerControllerDict;
    }

    public TowerController GetTowerController(int _tileIdx)
    {
        if (towerControllerDict.ContainsKey(_tileIdx))
        {
            return towerControllerDict[_tileIdx];
        }

        Debug.Log("ket not found in dict");
        return null;
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
