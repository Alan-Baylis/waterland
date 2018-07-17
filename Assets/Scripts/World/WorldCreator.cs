using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WorldCreator : MonoBehaviour
{
    public GameObject _originTileNode;
    public GameObject _tileRoot;

    public Text _textDesc = null;

    public int MAX_TILE_WIDTH = 150;
    public int MAX_TILE_HEIGHT = 150;

    public int MAX_WORLD_WIDTH = 30;
    public int MAX_WORLD_HEIGHT = 20;

    public int MAX_MAP_WIDTH = 120;
    public int MAX_MAP_HEIGHT = 144;

    public int[] _terrainNumbers =  new int[] { 1, 4, 7, 3, 6, 9 };
    public int _terrainNumbersMax = 9;

    GameObject[,] _tileArrObject = null;


    public Sprite _sprite_grass = null;
    public Sprite _sprite_land = null;
    public Sprite _sprite_sea = null;
    public Sprite _sprite_shore = null;

    float _section = 0f;
    float _noiseness = 0.15f;
    float _floatRange = 500f;
    float _tolerance = 0.75f;
    float _shoreSize = 0.9f;

    public bool _isRandomCreate = false;

    float _tileSize = 0.5f;

    void Start()
    {
        MapCreateType.InitMapCreateType(); // 맵 생성 타입 모델 데이터 로드
        CreateWorldData(1, 1);
        CreateMapTiles();

        LoadMapData(0, 0);

        if (_textDesc)
        {
            _textDesc.text = string.Format("MaxWorldSize : X : {0}, Y :{1}", MAX_WORLD_WIDTH, MAX_WORLD_HEIGHT);
        }

        Debug.Log("World create complete");
    }

    MapData CreateMapData(Offset mapOffset, int mapWidth, int mapHeight, bool isRandomized)
    {
        TileData[,] tileArrData = new TileData[mapWidth, mapHeight];

        if (isRandomized)
        {
            InitMapSettingRandom();
        }

        // TODO (용훈): 맵 타입 얻는 코드, 주석 풀고 디버깅 돌려서 MapCreateType 클래스와 MAP_CREATE.json 내용 비교하며 감 잡을 것.
        //MapCreateType mapCreateType = MapCreateType.GetMapCreateTypeById(Random.Range(1,13));
        //Debug.Log(string.Format("TypeName : {0} TerrainSort : {1} LocateTerrain : {2}", mapCreateType.TypeName, mapCreateType.TerrainSort, mapCreateType.LocateTerrain));






        for (int i = 0; i < mapWidth; ++i)
        {
            for (int j = 0; j < mapHeight; ++j)
            {
                // Not setting terrain type at the instaitiation (might need to change constructor)
                tileArrData[i, j] = new TileData(new Offset(i, j), TileData.TERRAIN_TYPE.DEFAULT);
            }
        }

        // Now we have map data with random noise 

        // For test only
        MapCreateType temp = new MapCreateType();
        temp.Id = 1;
        temp.Divide = _terrainNumbersMax;
        temp.LocateTerrain = _terrainNumbers;
        temp.TypeName = "sand_01";
        temp.TerrainSort = "sand";

        // Modify map for corresponding map type
        SetMapType(tileArrData, mapWidth, mapHeight, temp);
        SetTileType(tileArrData, mapWidth, mapHeight);

        MapData mapData = new MapData(mapOffset, mapWidth, mapHeight, tileArrData);


        return mapData;
    }

    void SetTileType(TileData[,] tileArrData, float mapWidth, float mapHeight)
    {
        // because we added elevation ?
        float elevation;
        for (int i = 0; i < mapWidth; i++)
        {
            for (int j = 0; j < mapHeight; j++)
            {
                elevation = tileArrData[i, j].Elevation;

                if (elevation < _tolerance)
                {
                    tileArrData[i, j].TerrainType = TileData.TERRAIN_TYPE.SEA;
                }
                else if (elevation < _tolerance + (2) * _shoreSize)
                {
                    tileArrData[i, j].TerrainType = TileData.TERRAIN_TYPE.SHORE;
                }
                else
                {
                    tileArrData[i, j].TerrainType = TileData.TERRAIN_TYPE.LAND;
                }
            }
        }


    }

    void SetMapType(TileData[,] tileArrData, float mapWidth, float mapHeight, MapCreateType mapCreateType)
    {
        int sideLength = (int)Mathf.Sqrt(mapCreateType.Divide);
        int sectionWidth = (int)(mapWidth / sideLength);
        int sectionHeight = (int)(mapHeight / sideLength);

        // sets the default noise (could be placed somewhere else..
        for (int index = 0; index < mapWidth; ++index)
        {
            for (int j = 0; j < mapHeight; ++j)
            {
                // Not setting terrain type at the instaitiation (might need to change constructor)
                tileArrData[index, j].Elevation = Mathf.PerlinNoise(_section + index * _noiseness, _section + j * _noiseness) - 0.3f;
            }
        }

        // set elevation for selected division 
        for (int index = 0; index < mapCreateType.LocateTerrain.Length; index++)
        {
            // change to 0 based indexing
            int division = mapCreateType.LocateTerrain[index] - 1;

            int centerX, centerY;
            // 110% of width and height / 2 on each side
            int horizontalRange = (int)(sectionWidth * 1.5f);
            int verticalRange = (int)(sectionHeight * 1.5f);
            // handle vertices first
            // left top
            if (division == 0)
            {
                centerX = 0;
                centerY = 0;
            }
            // right top
            else if (division == sideLength - 1)
            {
                centerX = (int)mapWidth;
                centerY = 0;
            }
            // left bottom
            else if (division == mapCreateType.Divide - sideLength)
            {
                centerX = 0;
                centerY = (int)mapHeight;
            }
            // right bottom
            else if (division == mapCreateType.Divide - 1)
            {
                centerX = (int)mapWidth;
                centerY = (int)mapHeight;
            }

            // edges
            // left edges
            else if (division % sideLength == 0)
            {
                centerX = 0;
                centerY = sectionHeight * (int)(division / sideLength) + sectionHeight / 2;
            }
            // top edges
            else if (division / sideLength == 0)
            {
                centerX = sectionWidth * (division % sideLength) + sectionWidth / 2;
                centerY = 0;

            }
            // right edges
            else if (division % sideLength == 2)
            {
                centerX = (int)mapWidth;
                centerY = sectionHeight * (int)(division / sideLength) + sectionHeight / 2;
 
            }
            // bottom edges
            else if (division / sideLength == sideLength - 1)
            {
                centerX = sectionWidth * (division % sideLength) + sectionWidth / 2;
                centerY = (int)mapHeight;

            }
            // others are centered at the center
            else
            {
                centerX = (int)mapWidth / 2;
                centerY = (int)mapHeight / 2;
            }
            
            // could be useful but can be taken away
            centerX += Random.Range(-sectionWidth / 4, sectionWidth / 4);
            centerY += Random.Range(-sectionHeight / 4, sectionHeight / 4);

            // increament elevation from centerX and centerY
            //float maxDist = Mathf.Sqrt(horizontalRange/2 * horizontalRange/2 + verticalRange/2 * verticalRange/2);
            float maxDist = horizontalRange > verticalRange ? horizontalRange : verticalRange;
            maxDist *= 0.7f;

            for (int i = centerX -horizontalRange; i < centerX + horizontalRange; i++)
            {
                if ( i < 0 || i >= mapWidth) continue;

                for (int j = centerY -verticalRange; j < centerY + verticalRange; j++)
                {
                    if ( j < 0 ||  j >= mapHeight) continue;

                    float dist = Mathf.Sqrt((centerX - i) * (centerX - i) + (centerY - j) * (centerY - j));
                    if (dist >= maxDist) continue;
                    float elevationToAdd = Mathf.Log( (1 - dist / maxDist) + 1) * 7;

                    tileArrData[i,  j].Elevation += elevationToAdd;
                }
            }
        }
    }



    void CreateWorldData(int worldWidth, int worldHeight)
    {
        WorldManager.Instance.InitWorldSize(new MapData[worldWidth, worldHeight], worldWidth, worldHeight);

        for (int i = 0; i < worldWidth; ++i)
        {
            for (int j = 0; j < worldHeight; ++j)
            {
                MapData mapData = CreateMapData(new Offset(i, j), MAX_MAP_WIDTH, MAX_MAP_HEIGHT, _isRandomCreate);
                WorldManager.Instance.InitMap(new Offset(i, j), mapData);
            }
        }
    }

    void CreateMapTiles()
    {
        if (_tileArrObject != null)
            return;

        _tileArrObject = new GameObject[MAX_TILE_WIDTH, MAX_TILE_HEIGHT];

        // 임시 
        _originTileNode = Resources.Load("Prefabs/WorldScene/WorldTile") as GameObject;

        _tileRoot = GameObject.Find("TileRoot");

        if (!_originTileNode)
            return;

        if (!_tileRoot)
            return;

        for (int i = 0; i < MAX_TILE_WIDTH; ++i)
        {
            for (int j = 0; j < MAX_TILE_HEIGHT; ++j)
            {
                GameObject cloneNode = Util.Clone(_originTileNode, _tileRoot.transform);
                if (!cloneNode)
                    return;

                cloneNode.transform.position = new Vector3(i * _tileSize, 0, j * _tileSize);
                _tileArrObject[i, j] = cloneNode;
            }
        }
    }

    // unused?
    TileData.TERRAIN_TYPE CalcTileType(int x, int y)
    {
        float noise = Mathf.PerlinNoise(_section + x * _noiseness, _section + y * _noiseness);
        TileData.TERRAIN_TYPE type = TileData.TERRAIN_TYPE.DEFAULT;

        if (noise < _tolerance)
        {
            type = TileData.TERRAIN_TYPE.SEA;
        }
        else if (noise < _tolerance +  _shoreSize) 
        {
            type = TileData.TERRAIN_TYPE.SHORE;
        }
        else
        {
            type = TileData.TERRAIN_TYPE.LAND;
        }

        return type;
    }

    void InitMapSettingRandom()
    {
        //0f;
        //0.15f
        //500f;
        //0.65f
        //0.4f;

        _section = Random.Range(0, _floatRange);
        //         _noiseness = Random.Range(0.1f, 0.2f);
        //         _floatRange = Random.Range(300.0f, 500f);
        //         _tolerance = Random.Range(0.3f, 0.65f);
        //         _shoreSize = Random.Range(0.2f, 0.45f);

    }

    void CreateMapObject(MapData mapData, Offset offset)
    {

    }

    public void LoadMapData(int worldPosX, int worldPosY)
    {
        if (worldPosX > MAX_WORLD_WIDTH || worldPosY > MAX_WORLD_HEIGHT)
            return;

        MapData mapData = WorldManager.Instance.GetMapdata(new Offset(worldPosX, worldPosY));
        if (mapData == null)
            return;

        Offset mapSize;
        mapData.GetMaxSize(out mapSize);

        for (int i = 0; i < MAX_TILE_WIDTH; ++i)
        {
            for (int j = 0; j < MAX_TILE_HEIGHT; ++j)
            {
                GameObject tileObj = _tileArrObject[i, j];

                if (i >= mapSize.x || j >= mapSize.y)
                {
                    tileObj.SetActive(false);
                    continue;
                }
                else
                {
                    tileObj.SetActive(true);
                }

                TileData tileData = mapData.GetTileData(i, j);
                if (tileData == null)
                    continue;

                SpriteRenderer comSprite = tileObj.GetComponent<SpriteRenderer>();
                if (!comSprite)
                    return;

                if (tileData.TerrainType == TileData.TERRAIN_TYPE.LAND)
                {
                    comSprite.sprite = _sprite_land;
                }
                else if (tileData.TerrainType == TileData.TERRAIN_TYPE.SEA)
                {
                    comSprite.sprite = _sprite_sea;
                }
                else if (tileData.TerrainType == TileData.TERRAIN_TYPE.SHORE)
                {
                    comSprite.sprite = _sprite_shore;
                }
            }
        }
    }
}
