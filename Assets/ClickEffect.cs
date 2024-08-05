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
    public float delayTime = 1.0f;

    void Start()
    {
        originGo = transform.Find("Origin")?.gameObject;
        effectGo = transform.Find("Effect")?.gameObject;
        afterCkickGo = transform.Find("AfterClick")?.gameObject;

        originGo.SetActive(true);
        // effectGo 不一定存在
        if (effectGo) effectGo.SetActive(false);
        if (afterCkickGo) afterCkickGo.SetActive(false);
        isStateChange = false;
    }

    public void ChangeState()
    {
        if (!isStateChange && originGo.activeSelf)
        {
            originGo.SetActive(false);
            Sequence mySequence = DOTween.Sequence(); // 创建一个DOTween序列
            if (effectGo)
            {
                // 获取当前GameObject的所有Renderer组件
                Renderer[] renderers = effectGo.GetComponentsInChildren<Renderer>();

                // 遍历所有的Renderer
                foreach (Renderer rend in renderers)
                {
                    // 确保Renderer使用的材质有透明度属性
                    if (rend.material.HasProperty("_Color"))
                    {
                        Color finalColor = rend.material.color;
                        finalColor.a = 0; // 初始透明度设为0
                        rend.material.color = finalColor;
                        effectGo.SetActive(true);
                        // 将透明度从0渐变到原始值
                        mySequence.Join(rend.material.DOColor(new Color(finalColor.r, finalColor.g, finalColor.b, 1),
                            1.5f));
                    }
                }
            }

            // 设置序列结束时的回调
                mySequence.OnComplete(() =>
                {
                    if (afterCkickGo) afterCkickGo.SetActive(true);
                    GameManager.Instance.SetGameState(GameManager.GameState.PlayFullScreenPic);
                });

                // 添加两秒的延迟
                mySequence.AppendInterval(delayTime);

                // 延迟后执行的操作
                mySequence.AppendCallback(() =>
                {
                    //anotherGameObject.SetActive(true); // 例如激活另一个GameObject
                    Debug.Log("拉起对话");
                    GameManager.Instance.SetGameState(GameManager.GameState.PlayDialogue);
                    if (needTriggerDialogue)
                    {
                        // 让gamemanager去拉起dialogue， game manager去负责所有的imput
                        GameManager.Instance.TriggerDialogue(dialogueName);
                    }
                });
            

            isStateChange = true;
        }

        if (animator)
        {
        }
    }

    public void DisableEffect()
    {
        {
            // 获取当前GameObject的所有Renderer组件
            Renderer[] renderers = effectGo.GetComponentsInChildren<Renderer>();
            Sequence mySequence = DOTween.Sequence(); // 创建一个DOTween序列

            // 遍历所有的Renderer
            foreach (Renderer rend in renderers)
            {
                // 确保Renderer使用的材质有透明度属性
                if (rend.material.HasProperty("_Color"))
                {
                    Color finalColor = rend.material.color;
                    finalColor.a = 0; // 初始透明度设为0
                    rend.material.color = finalColor;
                    effectGo.SetActive(true);
                    // 将透明度从0渐变到原始值
                    mySequence.Join(rend.material.DOColor(new Color(finalColor.r, finalColor.g, finalColor.b, 1), 120.5f));
                }
            }

            // 设置序列结束时的回调
            mySequence.OnComplete(() =>
            {
                if (afterCkickGo) afterCkickGo.SetActive(true);
                GameManager.Instance.SetGameState(GameManager.GameState.PlayFullScreenPic);
            });

        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}