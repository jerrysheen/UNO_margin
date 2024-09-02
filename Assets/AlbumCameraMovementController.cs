using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class AlbumCameraMovementController : MonoBehaviour
{

    public GameObject cameraObject;
    private Vector3 oldPostion;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.localPosition = new Vector3(10128, -31, 0);
        EventManager.Instance.StartListening(GameEvent.MoveCamera, OnMoveCamera);    
        cameraObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnApplicationQuit()
    {
        EventManager.Instance.StopListening(GameEvent.MoveCamera, OnMoveCamera);
    }
    
    private void OnMoveCamera(object  parameter)
    {
        Vector3 mountPosition = Vector3.zero;
        if (parameter is Vector3 position)
        {
            mountPosition = position;

            // 处理 Vector3 数据，例如更新角色位置
            Debug.Log("Position updated to: " + position);
        }
        else
        {
            Debug.LogError("Error: Received incorrect parameter type");
        }
        this.transform.position = mountPosition;
    }    
}
