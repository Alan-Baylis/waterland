
/// <summary>
/// 월드 전체의 데이터
/// </summary>
public class WorldManager
{
    static WorldManager _instance = null;
    MapData[,] _worldData = null;

    int _maxWorldOffsetX = 0;
    int _maxWorldOffsetY = 0;

    public static WorldManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new WorldManager();

            return _instance;
        }
    }

    public void InitWorldSize(MapData[,] worldData, int maxWorldSizeX, int maxWorldSizeY)
    {
        if (worldData == null)
            return;

        _worldData = worldData;
        _maxWorldOffsetX = maxWorldSizeX;
        _maxWorldOffsetY = maxWorldSizeY;
    }

    public void InitMap(Offset mapOffset, MapData mapData)
    {
        if (mapData == null || _worldData == null)
            return;

        if (mapOffset.x > _maxWorldOffsetX || mapOffset.y > _maxWorldOffsetY)
            return;

        _worldData[mapOffset.x, mapOffset.y] = mapData;
    }

    public MapData GetMapdata(Offset mapOffset)
    {
        if (_worldData == null)
            return null;

        if (mapOffset.x > _maxWorldOffsetX || mapOffset.y > _maxWorldOffsetY)
            return null;

        return _worldData[mapOffset.x, mapOffset.y];
    }
}
