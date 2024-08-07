using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuildController
{
    private GameObject towerParent;
    private TowerManager towerMgr;

    public TowerBuildController()
    {
        towerMgr = TowerManager.getInstance;
        towerParent = new GameObject();
        towerParent.name = "TowerParent";
    }

    public void OnClickBuildAbleTile(int _tileIdx)
    {
        TowerController towerController = towerMgr.GetTowerController(_tileIdx);

        if (towerController == null)
        {
            return;
        }

        towerController.gameObject.SetActive(true);
    }



    public void CreateTower(MapData _mapData)
    {
        //int count = TowerManager.getInstance.GetBuildAbleTileCount();
        //List<BuildAbleTileObject> buildAbleTileObjectList = TowerManager.getInstance.GetBuildAbleTileList();

        //for (int i =0; i < count; i++)
        //{
        //    TowerController towerController = GameObject.Instantiate<TowerController>(Resources.Load<TowerController>("Tower/TowerPrefab"), towerParent.transform);

        //    towerController.transform.position = buildAbleTileObjectList[i].position;

        //    buildAbleTileObjectList[i].SetTowerController(towerController);

        //}


        //Dictionary<int, TowerController> towerControllerDict = TowerManager.getInstance.GetTowerControllerDict();
        //int mapWidth = MapManager.getInstance.GetMapData().wid;

        //foreach(var key in towerControllerDict.Keys)
        //{
        //    TowerController towerController = GameObject.Instantiate<TowerController>(Resources.Load<TowerController>("Tower/TowerPrefab"), towerParent.transform);

        //    Vector2 towerPos = towerController.transform.position;
        //    towerPos.x = key % mapWidth;
        //    towerPos.y = key / mapWidth;
        //    towerController.transform.position = towerPos;

        //    towerControllerDict[key] = towerController;
        //}


        //Dictionary<int, TowerController> towerControllerDict = TowerManager.getInstance.GetTowerControllerDict();
        //int mapWidth = MapManager.getInstance.GetMapData().wid;

        //foreach (var kvp in towerControllerDict)
        //{
        //    TowerController towerController = GameObject.Instantiate<TowerController>(Resources.Load<TowerController>("Tower/TowerPrefab"), towerParent.transform);

        //    Vector2 towerPos = towerController.transform.position;
        //    towerPos.x = kvp.Key % mapWidth;
        //    towerPos.y = kvp.Key / mapWidth;
        //    towerController.transform.position = towerPos;

        //    towerControllerDict[kvp.Key] = towerController;
        //}


        int count = _mapData.tiles.Length;
        int mapWidth = _mapData.wid;
        for(int i = 0; i < count; i++)
        {
            TileData tile = _mapData.tiles[i];
            if (tile.buildable)
            {
                TowerController towerController = GameObject.Instantiate<TowerController>(Resources.Load<TowerController>("Tower/TowerPrefab"), towerParent.transform);

                Vector2 towerPos = towerController.transform.position;
                towerPos.x = i % mapWidth;
                towerPos.y = i / mapWidth;
                towerController.transform.position = towerPos;

                towerMgr.AddTowerControllerDictKey(i, towerController);
            }
        }
    }
}
