using UnityEngine;
using UnityEngine.EventSystems; // 引入事件系统命名空间
using UnityEngine.UI;          // 引入UI命名空间

public class UIChangeImage : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Sprite imageA;    // 图片A
    public Sprite imageB;    // 图片B
    private Image image;     // Image组件
    public GameObject triggerObject;
    public bool enable;
    public string dialogue;
    public bool triggerDialogue = false;
    public bool needSwitchScene = false;
    public GameManager.LevelState nextScene;

    public bool needChangeGameState = false;
    public GameManager.GameState gameState;
    void Start()
    {
        image = GetComponent<Image>();  // 获取Image组件
        if(imageA)image.sprite = imageA;          // 初始设置为图片A
        if (triggerObject)
        {
            triggerObject.SetActive(false);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // 按下时切换到图片B
        Debug.Log("PointDown");
        if(imageA && imageB)image.sprite = imageB;
        GameManager.Instance.SetGameState(GameManager.GameState.PlayFullScreenPic);

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // 释放后恢复到图片A
        Debug.Log("On point up");
        if(imageA && imageB)image.sprite = imageA;
        GameManager.Instance.SetGameState(GameManager.GameState.Normal);
        if (triggerObject)
        {
            triggerObject.SetActive(enable);
        }

        if (needSwitchScene)
        {
            GameManager.Instance.nextScene = nextScene;
            GameManager.Instance.SwitchToScene();
        }

        if (needChangeGameState)
        {
            GameManager.Instance.SetGameState(gameState);
        }

        if (triggerDialogue)
        {
            GameManager.Instance.TriggerDialogue(dialogue);
        }
    }
}