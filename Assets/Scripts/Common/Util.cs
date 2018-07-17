using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JsonFx.Json;

static public class Util
{
    public static GameObject Clone(GameObject ogirinObj, Transform parent)
    {
        GameObject cloneObj = GameObject.Instantiate(ogirinObj) as GameObject;
        cloneObj.transform.localPosition = Vector3.zero;
        cloneObj.transform.parent = parent;

        return cloneObj;
    }

    public static bool IsNull(params object[] objs)
    {
        int nCount = objs.Length;
        for (int i = 0; i < nCount; i++)
        {
            if (objs[i] == null)
            {
                return true;
            }
        }

        return false;
    }

	public static Dictionary<string, object> LoadJSON(string jsonPath)
	{
		object jsonData = Resources.Load(jsonPath);
		if (jsonData == null)
			return null;

		return JsonReader.Deserialize <Dictionary<string, object>>(jsonData.ToString());
	}

}
