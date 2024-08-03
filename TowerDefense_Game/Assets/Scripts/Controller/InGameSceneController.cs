using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSceneController : MonoBehaviour
{
    private GameObject mapParent;
    private MapData mapData;

    private WaveController waveController;

    private void Awake()
    {
        mapData = MapManager.getInstance.GetMapData();
        waveController = new WaveController(mapData);
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
