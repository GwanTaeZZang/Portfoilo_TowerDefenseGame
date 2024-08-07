using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator
{
    private GameObject mapParent;

    public MapCreator()
    {
        mapParent = new GameObject();
        mapParent.name = "Map";
    }

    public void CreateMap(MapData _mapData)
    {

        int width = _mapData.wid;
        int height = _mapData.tiles.Length / width;

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                SpriteRenderer tileSpriteRenderer = GameObject.Instantiate<SpriteRenderer>(Resources.Load<SpriteRenderer>("Map/TilePrefab"), mapParent.transform);
                tileSpriteRenderer.transform.position = new Vector2(j, i);

                int tileNum = i * width + j;
                tileSpriteRenderer.name = tileSpriteRenderer.name + "_" + tileNum.ToString();

                int spriteidx = _mapData.tiles[tileNum].imageIdx;

                //if (_mapData.tiles[tileNum].buildable)
                //{
                //    //BuildAbleTileObject buildAbleTile = new BuildAbleTileObject(tileNum, new Vector2(j, i));
                //    //TowerManager.getInstance.AddBuildAbleTile(buildAbleTile);


                //    TowerManager.getInstance.AddTowerControllerDictKey(tileNum);
                //    Debug.Log($"Add the BuildAble Tower Obejct in Tower Manager {tileNum} :  {new Vector2(j, i)}");
                //}

                tileSpriteRenderer.sprite = MapManager.getInstance.GetTileSprite((TileType)spriteidx);
            }
        }
    }
}
