using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Texture2D cursorTexture;  // 拖拽你的光标图像到这个字段
    public Vector2 hotSpot = Vector2.zero;  // 光标的热点位置

    void Start()
    {
        // 设置自定义光标
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
    }
    
}