using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ClickEffect : MonoBehaviour
{
    public GameObject originGo;
    public GameObject effectGo;
    public GameObject afterCkickGo;
    public bool isStateChange = false;
    public Animator animator;
    
    public bool needTriggerDialogue = false;
    public string dialogueName = "";
    // Start is called before the first frame update
    void Start()
    {
        originGo = transform.Find("Origin").gameObject;
        effectGo = transform.Find("Effect").gameObject;
        afterCkickGo = transform.Find("AfterClick").gameObject;
        
        originGo.SetActive(true);
        // effectGo 不一定存在
        if(effectGo)effectGo.SetActive(false);
        if(afterCkickGo)afterCkickGo.SetActive(false);
        isStateChange = false;
    }

    public void ChangeState()
    {
        if(!isStateChange && originGo.activeSelf)
        {
            originGo.SetActive(false);
            if (effectGo)
            {
                // 获取当前GameObject的所有Renderer组件
                Renderer[] renderers = effectGo.GetComponentsInChildren<Renderer>();
                Sequence mySequence = DOTween.Sequence();  // 创建一个DOTween序列

                // 遍历所有的Renderer
                foreach (Renderer rend in renderers)
                {
                    // 确保Renderer使用的材质有透明度属性
                    if (rend.material.HasProperty("_Color"))
                    {
                        Color finalColor = rend.material.color;
                        finalColor.a = 0;  // 初始透明度设为0
                        effectGo.SetActive(true);
                        // 将透明度从0渐变到原始值
                        mySequence.Join(rend.material.DOColor(finalColor, "_Color", 0.5f).From());
                    }
                }

                // 设置序列结束时的回调
                mySequence.OnComplete(() =>
                {
                    if(afterCkickGo)afterCkickGo.SetActive(true);
                });
                
                // 添加两秒的延迟
                mySequence.AppendInterval(1.2f);

                // 延迟后执行的操作
                mySequence.AppendCallback(() =>
                {
                    //anotherGameObject.SetActive(true); // 例如激活另一个GameObject
                });
            }
            
            isStateChange = true;
        }

        if (animator)
        {
        }

        if (needTriggerDialogue)
        {
            //DialogueManager.Instance.TriggerDialogue(dialogueName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
