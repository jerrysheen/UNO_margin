using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems; // 引入事件系统命名空间
using UnityEngine.UI;
public class PasswordButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public string buttonName;
    
    private Image image;     // Image组件

    public Sprite imageA;    // 图片A
    public Sprite imageB;
    public Sprite imageWrong;
    // Start is called before the first frame update

    public int currIndex = 0;
    
    public PasswordController passwordController;
    void Start()
    {
        buttonName = this.gameObject.name;
        image = GetComponent<Image>();
        passwordController = GameObject.Find("PasswordController")?.GetComponent<PasswordController>();
        if (!passwordController)
        {
            Debug.LogError("Can't find PasswordController");
        }
    }

    // Update is called once per frame
    
    public void OnPointerDown(PointerEventData eventData)
    {
        // 按下时切换到图片B
        if (currIndex == 0)
        {
            image.sprite = imageB;
            currIndex = 1;  
        }
        else if (currIndex == 1)
        {
            image.sprite = imageA;
            currIndex = 0;
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // 释放后恢复到图片A
        if (currIndex == 1)
        {
            if (!passwordController.AddNewButton(buttonName,this))
            {
                image.sprite = imageA;
            }
        }

        if (currIndex == 0)
        {
            passwordController.DeleteKey(buttonName);
        }


    }

    public void TriggerWrongEffect()
    {
        // 创建一个新的 DOTween 序列
        Sequence sequence = DOTween.Sequence();

        // 总时间分配为 1.5s，每次切换需要的时间是 1.5s / 6 = 0.25s
        float switchTime = 0.25f;

        // 添加图片切换的动画
        sequence.AppendCallback(() => image.sprite = imageA);
        sequence.AppendInterval(switchTime);
        sequence.AppendCallback(() => image.sprite = imageB);
        sequence.AppendInterval(switchTime);

        // 设置为循环三次，每次循环包括两次图片切换
        sequence.SetLoops(3, LoopType.Restart);
    }
}
