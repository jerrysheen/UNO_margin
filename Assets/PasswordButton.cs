using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems; // 引入事件系统命名空间
using UnityEngine.UI;          // 引入UI命名空间
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
            if (!passwordController.AddNewButton(buttonName))
            {
                image.sprite = imageA;
            }
        }

        if (currIndex == 0)
        {
            passwordController.DeleteKey(buttonName);
        }


    }
}
