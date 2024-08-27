using UnityEngine;
using UnityEngine.EventSystems; // 引入事件系统命名空间
using UnityEngine.UI;          // 引入UI命名空间
public class UISwitchScene : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        // 按下时切换到图片B
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // 释放后恢复到图片A
        Debug.Log("Switch Scene");
    }
}
