using UnityEngine;

public class KeyBoardMouseInput : MonoBehaviour
{
    void Update()
    {
        // 检测鼠标点击
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.Log(Input.mousePosition);
        //Debug.DrawLine(Camera.main.transform.position , Input.mousePosition);
        if (Input.GetMouseButtonDown(0)) // 0代表左键点击
        {
            //Debug.Log(Input.mousePosition);
            Vector3 calculatedPos =
                new Vector3(Input.mousePosition.x + 7532, Input.mousePosition.y, Input.mousePosition.z);
            // 将鼠标位置转换为在屏幕上的点
            Ray ray = Camera.main.ScreenPointToRay(calculatedPos);
            ray = new Ray(Camera.main.transform.position,
                Vector3.Normalize(calculatedPos - Camera.main.transform.position));
            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green, 1.0f);
            // 进行射线检测
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // 输出被射线击中的对象
                Debug.Log("Ray hit: " + hit.collider.gameObject.name);

                GameManager.Instance.ItemClicked(hit.collider.gameObject);
                // 如果被击中的对象有ClickEffect组件

            }
            else
            {
                Debug.Log("No hit");
                GameManager.Instance.EmptyClick();
            }
        }
        else if(Input.GetKeyDown("space"))
        {
            Debug.Log("Not hit");
            GameManager.Instance.EmptyClick();
        }
    }
}