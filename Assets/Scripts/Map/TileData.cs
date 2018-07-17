using UnityEngine;
using System.Collections;

/// <summary>
/// 1개의 타일을 구성하는 타일 
/// </summary>
public class TileData
{
    public enum TERRAIN_TYPE
    {
        DEFAULT,
        SEA,
        SHORE,
        LAND,
        MAX,
    }

    Offset _tileOffset;

    float _elevation = 0.0f;

    TERRAIN_TYPE _terrainType = TERRAIN_TYPE.DEFAULT;

    MapObject _tileMapObject = null;

    bool _isVisited = false;

    public bool IsVisited { get { return _isVisited; } }

    public TERRAIN_TYPE TerrainType { set { _terrainType = value; } get { return _terrainType; } }

    public Offset TileOffset { get { return _tileOffset; } }

    public MapObject TileMapObject { get { return _tileMapObject; } }

    public float Elevation { set { _elevation = value; } get { return _elevation; } }

    public TileData(Offset offset, TERRAIN_TYPE terrainType)
    {
        _tileOffset = offset;
        _terrainType = terrainType;
    }
}
