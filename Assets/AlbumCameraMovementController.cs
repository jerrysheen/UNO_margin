using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class AlbumCameraMovementController : MonoBehaviour
{

    public GameObject cameraObject;
    public GameObject cameraComponent;
    private Vector3 oldPostion;

    private Vector3 mountPoint;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.localPosition = new Vector3(10128, -31, 0);
        EventManager.Instance.StartListening(GameEvent.MoveCamera, OnMoveCamera);    
        cameraObject.SetActive(false);
        oldPostion = cameraComponent.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 calculatedPos =
            new Vector3(Input.mousePosition.x + mountPoint.x, Input.mousePosition.y + mountPoint.y, Input.mousePosition.z + mountPoint.z);
        cameraComponent.transform.position = calculatedPos;
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
        
        mountPoint = mountPosition;
        this.transform.position = mountPosition;
        cameraComponent.transform.position = oldPostion;
    }    
}
