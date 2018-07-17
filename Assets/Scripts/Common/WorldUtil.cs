using UnityEngine;
using System.Collections;

/// <summary>
/// 1.Common methods with World
/// 2.Common constant values in World
/// </summary>
public static class WorldUtil
{
	static Vector3 _vectorTemp = new Vector3();
	static Offset  _offestTemp = new Offset();

	public readonly static float TILE_WIDTH = 0.5f;

	enum WORLD_COMMON
	{
		MAX_TILEGROUP_OFFSET_WIDTH = 10
	}
			
	public static Offset GetTileOffsetByCameraLookAtPos(Vector3 cameraLookAtPos)
	{
		_offestTemp.Set((int)(cameraLookAtPos.z / TILE_WIDTH), (int)(cameraLookAtPos.x / TILE_WIDTH));
		return _offestTemp;
	}

	public static Vector3 GetTilePosByTileOffset(Offset tileOffset)
	{
		_vectorTemp.Set(tileOffset.y * TILE_WIDTH, 0.0f, tileOffset.x * TILE_WIDTH);
		return _vectorTemp;
	}

	public static Vector3 GetTileGroupPosByCameraLookAtPos(Vector3 cameraLookAtPos)
	{
		Offset lookAtTileOffset = GetTileOffsetByCameraLookAtPos(cameraLookAtPos);
		int tileGroupOffsetX = lookAtTileOffset.x / (int)WORLD_COMMON.MAX_TILEGROUP_OFFSET_WIDTH;
		int tileGroupOffsetY = lookAtTileOffset.y / (int)WORLD_COMMON.MAX_TILEGROUP_OFFSET_WIDTH;

		_vectorTemp.Set(tileGroupOffsetY * TILE_WIDTH, 0.0f, tileGroupOffsetX * TILE_WIDTH);

		return _vectorTemp;
	}

	public static Offset GetTileGroupOffsetByTileOffset(Offset tileOffset)
	{
		_offestTemp.Set(tileOffset.x / (int)WORLD_COMMON.MAX_TILEGROUP_OFFSET_WIDTH, tileOffset.y / (int)WORLD_COMMON.MAX_TILEGROUP_OFFSET_WIDTH);
		return _offestTemp;
	}

	public static Vector3 GetTileGroupPosByTileGroupOffset(Offset tileGroupOffset)
	{
		//_vectorTemp.Set(tileGroupOffset.y );

		return _vectorTemp;
	}
}
