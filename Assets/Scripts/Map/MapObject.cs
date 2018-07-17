using UnityEngine;
using System.Collections;

/// <summary>
/// 맵에 존재하는 오브젝트의 부모 클래스
/// </summary>
public class MapObject
{
    Offset _position;
    string _name = "";

    public Offset Position { set { _position = value; } get { return _position; } }
    public string Name { set { _name = value; } get { return _name; } }
}


