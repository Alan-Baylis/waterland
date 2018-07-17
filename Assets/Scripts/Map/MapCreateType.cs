using System;
using System.Collections.Generic;

/// <summary>
/// MAP_CREATE.json의 모델데이터 클래스
/// </summary>
public class MapCreateType
{
    int    _id            = 0;
    int    _divide        = 0;
    int[]  _locateTerrain = null;
    string _typeName      = "";
    string _terrainSort   = "";

    static MapCreateType[] _arrMapCreateType = null;

    public int    Id            { set { _id = value; }            get { return _id; } }
    public int    Divide        { set { _divide = value; }        get { return _divide; } }
    public int[]  LocateTerrain { set { _locateTerrain = value; } get { return _locateTerrain; } }
    public string TypeName      { set { _typeName = value; }      get { return _typeName; } }
    public string TerrainSort   { set { _terrainSort = value; }   get { return _terrainSort; } }

    public static void InitMapCreateType()
    {
        Dictionary<string, object> json = Util.LoadJSON("WorldScene/MAP_CREATE");
        Dictionary<string, object>[] MAP_CREATE = json["MAP_CREATE"] as Dictionary<string, object>[];
        int nLength = MAP_CREATE.Length;

        _arrMapCreateType = new MapCreateType[nLength];

        int nCount = 0;
        foreach (var typeData in MAP_CREATE)
        {
            _arrMapCreateType[nCount] = new MapCreateType();

            _arrMapCreateType[nCount].Id          = Convert.ToInt32(typeData["id"]);
            _arrMapCreateType[nCount].TypeName    = typeData["TYPE_NAME"].ToString();
            _arrMapCreateType[nCount].TerrainSort = typeData["TERRAIN_SORT"].ToString();
            _arrMapCreateType[nCount].Divide      = Convert.ToInt32(typeData["DIVIDE"]);

            // 이녀석은 입력 값이 "1,2,4,5" 와 같은 string으로 들어오기 때문에 ',' 단위로 잘라서 int 배열에 밀어넣는 고달픈 작업이 필요.
            string strLocateTerrain = typeData["LOCATE_TERRAIN"].ToString();
            string strTemp = strLocateTerrain;

            //먼저 이게 총 몇 개가 필요한지 구해보자
            int i = 0;
            while (strTemp.Length >= 0)
            {
                int index = strTemp.IndexOf(",");
                if(index != -1)
                {
                    strTemp = strTemp.Substring(index + 1, strTemp.Length - index - 1);
                }
                else
                {
                    i++;
                    break;
                }

                if (i > 100) // 무한 루프 방지 예외처리
                    break;

                i++;
            }

            int[] arrData = new int[i];

            // 길이 구했으니까 해당 길이에 맞게 데이터를 밀어넣는다.
            i = 0;
            while (strLocateTerrain.Length >= 0)
            {
                int index = strLocateTerrain.IndexOf(",");
                if (index != -1)
                {
                    int.TryParse(strLocateTerrain.Substring(0, index), out arrData[i]);
                    strLocateTerrain = strLocateTerrain.Substring(index + 1, strLocateTerrain.Length - index - 1);
                }
                else
                {
                    // 마지막, 완전히 비어있는 경우는 뻑날것임.
                    int.TryParse(strLocateTerrain, out arrData[i]);
                    break;
                }

                if (i > 100) // 무한 루프 방지 예외처리
                    break;

                i++;
            }

            _arrMapCreateType[nCount].LocateTerrain = arrData;

            nCount++;
        }
    }

    public static MapCreateType GetMapCreateTypeById(int id)
    {
        if (_arrMapCreateType == null)
            return null;

        int nCount = _arrMapCreateType.Length;
        for (int i = 0; i < nCount; ++i)
        {
            if (id == _arrMapCreateType[i].Id)
                return _arrMapCreateType[i];
        }

        return null;
    }

}


