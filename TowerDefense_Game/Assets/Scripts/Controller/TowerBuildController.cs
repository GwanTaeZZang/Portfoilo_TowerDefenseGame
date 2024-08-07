using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuildController
{
    private GameObject towerParent;

    public TowerBuildController()
    {
        towerParent = new GameObject();
        towerParent.name = "TowerParent";
    }

    public void CreateTower()
    {
        int count = TowerManager.getInstance.GetBuildAbleTileCount();
        List<BuildAbleTileObject> buildAbleTileObjectList = TowerManager.getInstance.GetBuildAbleTileList();

        for (int i =0; i < count; i++)
        {
            TowerController towerController = GameObject.Instantiate<TowerController>(Resources.Load<TowerController>("Tower/TowerPrefab"), towerParent.transform);

            towerController.transform.position = buildAbleTileObjectList[i].position;

            buildAbleTileObjectList[i].SetTowerController(towerController);

        }
    }
}
