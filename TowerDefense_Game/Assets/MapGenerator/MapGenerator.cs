using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int width;
    public int height;
    public bool isCreateMap = false;
    public bool isSetWayTile = false;
    public bool isSetEndWayTile = false;
    public bool isSetTowerBuileTile = false;

    private GameObject mapParent;

    public List<TileModel> tileList = new List<TileModel>();

    public void CreateMap(int _width, int _height)
    {
        if (mapParent == null)
        {
            mapParent = new GameObject();
            mapParent.name = "Map";

            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    SpriteRenderer tileSpriteRenderer = Instantiate<SpriteRenderer>(Resources.Load<SpriteRenderer>("MapSprite/MoveDisableTile"), mapParent.transform);

                    tileSpriteRenderer.transform.position = new Vector2(j, i);
                    int tileNum = i * _width + j;
                    tileSpriteRenderer.name = tileSpriteRenderer.name + "_" + tileNum.ToString();

                    TileModel tile = new TileModel();
                    tile.transform = tileSpriteRenderer.transform;
                    tile.moveAble = false;
                    tile.towerBuledAble = false;

                    tileList.Add(tile);
                }
            }

            Debug.Log(tileList.Count);
        }
        else
        {
            Debug.Log("has already been created");

        }
    }

    public void SetTile(int _tileNum)
    {
        Debug.Log(_tileNum);

        if (!isSetEndWayTile)
        {
            TileModel tile = tileList[_tileNum];
            tile.isEndWay = true;
            tileList[_tileNum] = tile;
        }
        else if (!isSetWayTile)
        {
            TileModel tile = tileList[_tileNum];
            tile.moveAble = true;
            tileList[_tileNum] = tile;
        }
        else if (!isSetTowerBuileTile)
        {
            TileModel tile = tileList[_tileNum];
            tile.towerBuledAble = true;
            tileList[_tileNum] = tile;
        }
    }

    public void CancelEndWayTile()
    {
        int count = tileList.Count;
        for(int i =0; i < count; i++)
        {
            TileModel tile = tileList[i];
            tile.isEndWay = false;
            tileList[i] = tile;
        }
    }
    public void CancelWayTile()
    {
        int count = tileList.Count;
        for (int i = 0; i < count; i++)
        {
            TileModel tile = tileList[i];
            tile.moveAble = false;
            tileList[i] = tile;
        }
    }
    public void CancelTowerBuildTile()
    {
        int count = tileList.Count;
        for (int i = 0; i < count; i++)
        {
            TileModel tile = tileList[i];
            tile.towerBuledAble = false;
            tileList[i] = tile;
        }
    }


    public void DestroyMap()
    {
        GameObject map = GameObject.Find("Map");
        DestroyImmediate(map);
        mapParent = null;
        isCreateMap = false;

        tileList.Clear();


        isSetWayTile = false;
        isSetTowerBuileTile = false;
        isSetEndWayTile = false;
    }
}
