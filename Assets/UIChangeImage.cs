using UnityEngine;
using UnityEngine.EventSystems; // 引入事件系统命名空间
using UnityEngine.UI;          // 引入UI命名空间

public class UIChangeImage : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Sprite imageA;    // 图片A
    public Sprite imageB;    // 图片B
    private Image image;     // Image组件

    void Start()
    {
        image = GetComponent<Image>();  // 获取Image组件
        image.sprite = imageA;          // 初始设置为图片A
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // 按下时切换到图片B
        image.sprite = imageB;
        GameManager.Instance.SetGameState(GameManager.GameState.PlayFullScreenPic);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // 释放后恢复到图片A
        image.sprite = imageA;
        GameManager.Instance.SetGameState(GameManager.GameState.Normal);
    }
}