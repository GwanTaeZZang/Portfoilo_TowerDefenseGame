using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public string mapName;
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
                    tile.tileType = TileType.MoveDisable;
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
            tile.tileType = TileType.MoveAble;
            tile.tileId = 1;

            tileList[_tileNum] = tile;
        }
        else if (!isSetWayTile)
        {
            List<int> wayList = wayListStack.Peek();
            TileModel tile = tileList[_tileNum];
            tile.moveAble = true;
            tile.tileType = TileType.MoveAble;
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
                        betweenTile.tileType = TileType.MoveAble;
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
                        betweenTile.tileType = TileType.MoveAble;
                        betweenTile.tileId = 1;

                        tileList[tileNum] = betweenTile;
                    }

                }
                else
                {
                    //TileModel tile = tileList[_tileNum];
                    tile.moveAble = false;
                    tile.tileType = TileType.MoveDisable;
                    tile.tileId = 0;

                    tileList[_tileNum] = tile;

                    wayList.RemoveAt(wayList.Count - 1);

                }
            }

            List<int> intList = wayListStack.Peek();
            for (int i =0; i < intList.Count; i++)
            {
                Debug.Log(intList[i]);
            }
        }
        else if (!isSetTowerBuileTile)
        {
            TileModel tile = tileList[_tileNum];
            tile.towerBuledAble = true;
            tile.tileType = TileType.TowerBuildAble;
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
            tile.tileType = TileType.MoveDisable;
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
            tile.tileType = TileType.MoveDisable;
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
            tile.tileType = TileType.MoveDisable;
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

    public void CreateJsonMapData()
    {
        if (mapName == "")
        {
            Debug.Log("Set Map Name");
            return;
        }

        JsonController.WriteJson<MapData>(mapName, ConvertTileDataToJsonData());
    }

    private MapData ConvertTileDataToJsonData()
    {
        MapData mapData = new MapData();
        mapData.id = 0;
        mapData.wid = width;

        int count = tileList.Count;

        mapData.tiles = new TileData[count];
        for (int i =0; i < count; i++)
        {
            TileModel tileModel = tileList[i];
            TileData tileData = new TileData();
            tileData.imageIdx = (int)tileModel.tileType;
            tileData.moveable = tileModel.moveAble;
            tileData.buildable = tileModel.towerBuledAble;

            if (tileModel.isEndWay)
            {
                mapData.goalIdx = i;
            }

            mapData.tiles[i] = tileData;
        }

        count = wayListStack.Count;

        mapData.routes = new RouteData[count];
        for(int i =0; i < count; i++)
        {
            List<int> routesList = wayListStack.Pop();

            int listCount = routesList.Count - 1;

            RouteData routeData = new RouteData();
            routeData.tileIdxs = new int[listCount];

            for (int j = 0; j<listCount; j++)
            {
                routeData.tileIdxs[j] = routesList[j];
            }

            mapData.routes[i] = routeData;
        }

        return mapData;
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

    private void ComputBetweenTile(List<int> _wayTileList, int _chageTileType)
    {

    }
}
