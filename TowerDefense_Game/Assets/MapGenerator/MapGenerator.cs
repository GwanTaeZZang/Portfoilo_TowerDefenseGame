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
    public List<SpriteRenderer> tileSpriteRendererList = new List<SpriteRenderer>();
    public Stack<List<int>> wayListStack = new Stack<List<int>>();
    public List<int> wayTileList;

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
                    SpriteRenderer tileSpriteRenderer = Instantiate<SpriteRenderer>(Resources.Load<SpriteRenderer>("MapSprite/TilePrefab"), mapParent.transform);

                    tileSpriteRenderer.transform.position = new Vector2(j, i);

                    int tileNum = i * _width + j;
                    tileSpriteRenderer.name = tileSpriteRenderer.name + "_" + tileNum.ToString();

                    TileModel tile = new TileModel();
                    tile.tileId = 0;
                    tile.transform = tileSpriteRenderer.transform;
                    tile.moveAble = false;
                    tile.towerBuledAble = false;

                    tileList.Add(tile);

                    tileSpriteRendererList.Add(tileSpriteRenderer);
                }
            }

            Debug.Log(tileList.Count);
        }
        else
        {
            Debug.Log("has already been created");

        }
    }

    public void AddWay()
    {
        wayTileList = new List<int>();
        wayListStack.Push(wayTileList);
    }

    public void DeleteWay()
    {
        List<int> list = wayListStack.Pop();
        // want Destory No Clear
        list.Clear();
    }

    public void SetTile(int _tileNum)
    {
        Debug.Log(_tileNum);

        if (!isSetEndWayTile)
        {
            TileModel tile = tileList[_tileNum];
            tile.isEndWay = true;
            tile.tileId = 1;

            tileList[_tileNum] = tile;
        }
        else if (!isSetWayTile)
        {
            List<int> wayList = wayListStack.Peek();
            TileModel tile = tileList[_tileNum];
            tile.moveAble = true;
            tile.tileId = 1;
            tileList[_tileNum] = tile;

            wayList.Add(_tileNum);

            if(wayList.Count > 1)
            {
                int prevTileNum = wayList[wayList.Count - 2];
                TileModel prevTile = tileList[prevTileNum];

                int curTileNum = wayList[wayList.Count - 1];
                TileModel curTile = tileList[curTileNum];

                Vector2 dir = curTile.transform.position - prevTile.transform.position;
                //dir.x = Mathf.Abs(dir.x);
                //dir.y = Mathf.Abs(dir.y);

                if(dir.x == 0)
                {
                    int count = (int)Mathf.Abs(dir.y);
                    Vector2 prevTilePos = prevTile.transform.position;

                    for (int i = 0; i < count; i++)
                    {
                        //Debug.Log(dir.y);

                        int increase = dir.y > 0 ? i + 1 : (i + 1) * -1;
                        //Debug.Log(increase);

                        Vector2 betweenTilePos = new Vector2(prevTilePos.x, prevTilePos.y + increase);
                        int tileNum = (int)betweenTilePos.y * width + (int)betweenTilePos.x;
                        TileModel betweenTile = tileList[tileNum];
                        betweenTile.moveAble = true;
                        betweenTile.tileId = 1;

                        tileList[tileNum] = betweenTile;


                    }
                }
                else if(dir.y == 0)
                {
                    int count = (int)Mathf.Abs(dir.x);
                    Vector2 prevTilePos = prevTile.transform.position;

                    for (int i = 0; i < count; i++)
                    {
                        //Debug.Log(dir.x);

                        int increase = dir.x > 0 ? i + 1 : (i + 1) * -1;
                        //Debug.Log(increase);

                        Vector2 betweenTilePos = new Vector2(prevTilePos.x + increase, prevTilePos.y);
                        int tileNum = (int)betweenTilePos.y * width + (int)betweenTilePos.x;
                        TileModel betweenTile = tileList[tileNum];
                        betweenTile.moveAble = true;
                        betweenTile.tileId = 1;

                        tileList[tileNum] = betweenTile;
                    }

                }
                else
                {
                    //TileModel tile = tileList[_tileNum];
                    tile.moveAble = false;
                    tile.tileId = 0;

                    tileList[_tileNum] = tile;

                    wayList.RemoveAt(wayList.Count - 1);

                }
            }
        }
        else if (!isSetTowerBuileTile)
        {
            TileModel tile = tileList[_tileNum];
            tile.towerBuledAble = true;
            tile.tileId = 2;
            tileList[_tileNum] = tile;
        }


        UpdateMap();
    }

    public void CancelEndWayTile()
    {
        int count = tileList.Count;
        for(int i =0; i < count; i++)
        {
            TileModel tile = tileList[i];
            tile.isEndWay = false;
            tile.tileId = 0;
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
            tile.tileId = 0;
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
            tile.tileId = 0;
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
        int count = wayListStack.Count;
        for (int i =0; i < count; i++)
        {
            wayListStack.Pop().Clear();
        }
        wayListStack.Clear();

        tileSpriteRendererList.Clear();

        isSetWayTile = false;
        isSetTowerBuileTile = false;
        isSetEndWayTile = false;
    }


    private void UpdateMap()
    {
        for(int i = 0; i < height; i++)
        {
            for(int j =0; j < width; j++)
            {
                int tileNum = i * width + j;
                TileModel tileModel = tileList[tileNum];

                if (tileModel.tileId == 0)
                {
                    tileSpriteRendererList[tileNum].sprite = Resources.Load<Sprite>("MapSprite/MoveDisableTile");
                }
                else if (tileModel.tileId == 1)
                {
                    tileSpriteRendererList[tileNum].sprite = Resources.Load<Sprite>("MapSprite/MoveAbleTile");
                }
                else
                {
                    tileSpriteRendererList[tileNum].sprite = Resources.Load<Sprite>("MapSprite/TowerBuildTile");
                }
            }
        }
    }
}
