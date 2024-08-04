using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSceneController : MonoBehaviour
{
    private GameObject mapParent;
    private MapData mapData;

    private WaveController waveController;

    private GameObject towerParent;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
        mapData = MapManager.getInstance.GetMapData();
        waveController = new WaveController(mapData);

        towerParent = new GameObject();
        towerParent.name = "TowerParent";
    }
    private void Start()
    {
        CreateMap();
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            waveController.SpawnMonster();
        }
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
                    TowerController towerController = Instantiate<TowerController>(Resources.Load<TowerController>("Tower/TowerPrefab"), towerParent.transform);

                    towerController.transform.position = new Vector2(x, y);
                }
            }

        }
    }


    private void CreateMap()
    {
        mapParent = new GameObject();
        mapParent.name = "Map";

        int width = mapData.wid;
        int height = mapData.tiles.Length / width;

        for(int i =0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
            {
                SpriteRenderer tileSpriteRenderer = Instantiate<SpriteRenderer>(Resources.Load<SpriteRenderer>("Map/TilePrefab"), mapParent.transform);
                tileSpriteRenderer.transform.position = new Vector2(j, i);

                int tileNum = i * width + j;
                tileSpriteRenderer.name = tileSpriteRenderer.name + "_" + tileNum.ToString();

                int spriteidx = mapData.tiles[tileNum].imageIdx;

                tileSpriteRenderer.sprite = MapManager.getInstance.GetTileSprite((TileType)spriteidx);
            }
        }
    }
}
