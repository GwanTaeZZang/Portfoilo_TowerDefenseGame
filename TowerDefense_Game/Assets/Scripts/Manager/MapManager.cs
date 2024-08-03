using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    private Dictionary<TileType, Sprite> tileSprteDict = new Dictionary<TileType, Sprite>();
    private MapData mapData = new MapData();

    public override bool Initialize()
    {
        LoadMapData();
        LoadTileSprite();
        return base.Initialize();
    }

    public MapData GetMapData()
    {
        return mapData;
    }

    public Sprite GetTileSprite(TileType _tileType)
    {
        return tileSprteDict[_tileType];
    }

    private void LoadMapData()
    {
        mapData = JsonController.ReadJson<MapData>("aaaa");
    }
    private void LoadTileSprite()
    {
        int count = (int)TileType.End;
        for(int i = 0; i < count; i++)
        {
            TileType tileType = (TileType)i;
            Sprite tileSprite = Resources.Load<Sprite>("Map/TileSprite_" + i);
            tileSprteDict.Add(tileType, tileSprite);
        }
    }
}
