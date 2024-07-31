using UnityEngine;

public class Raycast : MonoBehaviour
{
    void Update()
    {
        // 检测鼠标点击
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.Log(Input.mousePosition);
        //Debug.DrawLine(Camera.main.transform.position , Input.mousePosition);
         if (Input.GetMouseButtonDown(0))  // 0代表左键点击
         {
             Debug.Log(Input.mousePosition);
             // 将鼠标位置转换为在屏幕上的点
             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
             Debug.DrawLine(Camera.main.transform.position , Input.mousePosition);
             // 进行射线检测
             RaycastHit hit;
             if (Physics.Raycast(ray, out hit))
             {
                 // 输出被射线击中的对象
                 Debug.Log("Ray hit: " + hit.collider.gameObject.name);
             }
             else
             {
                 Debug.Log("No hit");
             }
         }
    }
}