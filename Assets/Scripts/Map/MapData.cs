using UnityEngine;
using System.Collections;

/// <summary>
/// 1개의 맵을 구성하는 맵 데이터 클래스
/// </summary>
public class MapData
{
    int _mapCreateTypeId = 0;

    Offset _mapOffset;
    TileData[,] _tileArrData = null;

    int _maxWidth  = 0;
    int _maxHeight = 0;

    public int MapCreateTypeId { get { return _mapCreateTypeId; } }

    public Offset MapOffset { get { return _mapOffset; } }

    public MapData(Offset mapOffset, int maxWidth, int maxHeight, TileData[,] tileArrData)
    {
        _mapOffset   = mapOffset;
        _maxWidth    = maxWidth;
        _maxHeight   = maxHeight;
        _tileArrData = tileArrData;
    }

    public TileData GetTileData(int x, int y)
    {
        if (_tileArrData == null)
            return null;

        if (x >= _maxWidth || y >= _maxHeight)
            return null;

        return _tileArrData[x, y];
    }

    public void GetMaxSize(out Offset size)
    {
        size = new Offset(_maxWidth, _maxHeight);
    }
}
