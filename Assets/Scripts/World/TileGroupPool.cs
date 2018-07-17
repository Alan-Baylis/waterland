using UnityEngine;
using System.Collections;

/// <summary>
/// 타일 그룹 메모리 풀
/// </summary>
//[ExecuteInEditMode]
public class TileGroupPool : MonoBehaviour
{
    public int TILEGROUP_MAXWIDTH = 10;
    float _tileSize = 0.5f;

    public GameObject _originTileNode = null;
    public GameObject _tileGroupPool = null;

	GameObject[] _tileGroups = null;

    void Start()
    {
        
    }

	void Refresh(Vector3 carmeraLookPos)
	{
		if (_tileGroups == null)
			return;

		Vector3 dd = WorldUtil.GetTileGroupPosByCameraLookAtPos(carmeraLookPos);

		for (int i = -1; i < 1; ++i) 
		{
			for (int j = -1; j < 1; ++j) 
			{
				
			}
		}
	}

	/// <summary>
	/// Create 9 tilegroups and save reference at array. 
	/// </summary>
	void InitTileGroupPool()
	{
		if (!_originTileNode || !_tileGroupPool)
			return;

		_tileGroups = new GameObject[9];

		for (int i = 0; i < 9; ++i) 
		{
			_tileGroups[i] = Instantiate(_originTileNode, _tileGroupPool.transform) as GameObject;
		}
	}

	/// <summary>
	/// Create tileGroup prefab 
	/// Use this code with [ExecuteInEditMode] attribute and run, You can get result Object in Editor.
	/// Then, Drag and drop to '-Project- Prefabs/WorldScene' this result Object.
	/// </summary>
	void CreateTileGroup()
    {
        if (!_originTileNode)
            return;

        if (!_tileGroupPool)
            return;

        for (int i = 0; i < TILEGROUP_MAXWIDTH; ++i)
        {
            for (int j = 0; j < TILEGROUP_MAXWIDTH; ++j)
            {
                GameObject cloneNode = Util.Clone(_originTileNode, _tileGroupPool.transform);
                if (!cloneNode)
                    return;

                cloneNode.transform.position = new Vector3(i * _tileSize, 0, j * _tileSize);
            }
        }
    }
}
