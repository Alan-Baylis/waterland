using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WorldController : MonoBehaviour
{
    public  Camera  _mainCamera = null;
    private Vector3 _moveDir    = new Vector3();
    private float   _moveSpeed  = 3.0f;

    public WorldCreator _worldCreator = null;

    public InputField _inputOffsetX = null;
    public InputField _inputOffsetY = null;

	void Start()
	{
        
    }

    void Update()
    {
        if (!_mainCamera)
            return;

        _moveDir.x = Input.GetAxis("Horizontal");
        _moveDir.z = Input.GetAxis("Vertical");
        _moveDir.y = 0;

        _moveDir = _moveDir.normalized * _moveSpeed * Time.deltaTime;

        _mainCamera.transform.position += _moveDir;
    }

    public void CallbackBtnGo()
    {
        if (!_worldCreator)
            return;

        int x = 0;
        int y = 0;
        Debug.Log("x" + x +"y" + y);
        int.TryParse(_inputOffsetX.text, out x);
        int.TryParse(_inputOffsetY.text, out y);

        _worldCreator.LoadMapData(x, y);
    }
}
