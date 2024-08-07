using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSceneController : MonoBehaviour
{
    private MapData mapData;

    private MapCreator mapCreator;
    private TowerBuildController towerBuildController;
    private WaveController waveController;

    //private GameObject towerParent;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
        mapData = MapManager.getInstance.GetMapData();
        mapCreator = new MapCreator();
        towerBuildController = new TowerBuildController();
        waveController = new WaveController(mapData);

        //towerParent = new GameObject();
        //towerParent.name = "TowerParent";
    }

    private void Start()
    {
        mapCreator.CreateMap(mapData);
        towerBuildController.CreateTower();
    }

    private void Update()
    {
        // temp monster Create
        if (Input.GetKeyDown("space"))
        {
            waveController.SpawnMonster();
        }
        // temp Tower Build
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 touchVector = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            int x = Mathf.RoundToInt(touchVector.x);
            int y = Mathf.RoundToInt(touchVector.y);

            //Debug.Log($"x : {x}   y : {y}");

            if(x >= 0 && x < mapData.wid &&
               y >= 0 && y < mapData.tiles.Length / mapData.wid)
            {
                int tileIdx = y * mapData.wid + x;

                Debug.Log(tileIdx);

                if (mapData.tiles[tileIdx].buildable)
                {
                    Debug.Log("buildAble");
                    //TowerController towerController = Instantiate<TowerController>(Resources.Load<TowerController>("Tower/TowerPrefab"), towerParent.transform);

                    //towerController.transform.position = new Vector2(x, y);
                }
            }

        }
    }
}
