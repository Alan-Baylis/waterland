using UnityEngine;
using System.Collections;

public class PlayerManager 
{
	static PlayerManager _instance = null;

	public static PlayerManager Instance
	{
		get
		{
			if (_instance == null)
				_instance = new PlayerManager();

			return _instance;
		}
	}



}