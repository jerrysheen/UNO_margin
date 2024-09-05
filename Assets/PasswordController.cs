using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PasswordController : MonoBehaviour
{
    // Start is called before the first frame update
    public string fullPassword = "158";
    public List<string> currPass;
    public List<PasswordButton> currPassScript;

    public GameObject imageGameObject;
    public Material passWordBoxMaterial;
    private Material imageMaterial;
    public Color boxMatColor;
    public Color imageColor;
    public GameObject zhaoPian;
    
    public GameObject enableObj;
    public GameObject disableObj;
    void Start()
    {
        fullPassword = "158";
        currPass = new List<string>();
        boxMatColor = passWordBoxMaterial.color;
        imageMaterial = zhaoPian.GetComponent<SpriteRenderer>().material;
        imageColor = imageMaterial.color;
        
        Color targetColor = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        imageMaterial.DOColor(targetColor, 1.5f);
        
        EventManager.Instance.StartListening(GameEvent.OnOpenPasswordBox, OnOpenPasswordBox);
        targetColor = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        passWordBoxMaterial.DOColor(targetColor, 0.01f);
        this.gameObject.SetActive(false);
    }

//    private void OnDisable()
//    {
//        EventManager.Instance.StopListening(GameEvent.OnOpenPasswordBox, OnOpenPasswordBox);
//    }

    public void OnOpenPasswordBox()
    {
        Sequence mySequence = DOTween.Sequence();
        
        mySequence.AppendInterval(0.01f);
        mySequence.AppendCallback(() =>
        {
            this.gameObject.SetActive(true);
        });
        mySequence.AppendInterval(0.01f);
        mySequence.AppendCallback(() =>
        {
            Color targetColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            passWordBoxMaterial.DOColor(targetColor, 1.5f);
        });
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        //GameManager.Instance.TriggerDialogue("Level4_conversation6");
        // 判断， 如果是第二次开启，就需要enable这个。
    }

    public bool AddNewButton(string newString, PasswordButton script)
    {
        currPass.Add(newString);
        currPassScript.Add(script);
        if (currPass.Count < 3)
        {
            return true;
        }
        else if (currPass.Count == 3)
        {
            string result = String.Join("", currPass);
            ComparePassword(result);
            return true;
        }

        return false;
    }

    public bool DeleteKey(string key)
    {
        // 使用 RemoveAll 方法来删除所有匹配的元素
        currPass.RemoveAll(x => x == key);

        // 打印修改后的列表，确认是否删除了所有的 2
        foreach (string number in currPass)
        {
            System.Console.WriteLine(number);
        }

        return true;
    }

    public void ComparePassword(string password)
    {
        if (password == fullPassword)
        {
            Debug.Log("true!!!!");
            TriggerPasswordRight();
        }
        else
        {
            Debug.Log(password);
            TriggerPasswordWrong();
            ResetCondition();
        }
    }

    public void TriggerPasswordWrong()
    {
        foreach (var script in currPassScript)
        {
            script.TriggerWrongEffect();
        }
    }

    public void ResetCondition()
    {
        Debug.LogError("Password wrong!!!");
        currPass.Clear();
        currPassScript.Clear();
        GameManager.Instance.TriggerDialogue("Level4_conversation5");
    }

    public void TriggerPasswordRight()
    {
        GameManager.Instance.TriggerDialogue("Level4_conversation4");
        // show photo here maybe...
        // 创建一个序列
        Sequence mySequence = DOTween.Sequence();

        mySequence.AppendInterval(1.5f);

        // 添加第一个要执行的函数
        mySequence.AppendCallback(() => {GameManager.Instance.EmptyClick();});

        // // 等待1.5秒
        // mySequence.AppendInterval(1.5f);
        //
        // // 添加第二个要执行的函数
        // mySequence.AppendCallback(() => 
        // {        
        //     GameManager.Instance.TriggerDialogue("Level4_conversation6");
        // });

        // 再次等待1.5秒
        mySequence.AppendInterval(1.5f);

        // 执行打开界面的函数
        mySequence.AppendCallback(() => {GameManager.Instance.EmptyClick();});
        Color targetColor = new Color(boxMatColor.r, boxMatColor.g, boxMatColor.b, 0);
        mySequence.Append(passWordBoxMaterial.DOColor(targetColor, 1.5f));
        mySequence.AppendCallback(() => {imageGameObject.GetComponent<ClickEffect>().ChangeState();});
        mySequence.AppendInterval(1.5f);
        mySequence.AppendCallback(() => 
        {        
            GameManager.Instance.TriggerDialogue("Level4_conversation3");
        });
        mySequence.AppendInterval(1.5f);
        mySequence.AppendCallback(() => {GameManager.Instance.EmptyClick();});
        //mySequence.AppendInterval(1.5f);
        
        targetColor = imageColor;
        mySequence.Append(imageMaterial.DOColor(new Color(1.0f, 1.0f, 1.0f, 1.0f), 1.5f));
        mySequence.AppendInterval(0.3f);

        mySequence.AppendCallback(() => 
        {        
            GameManager.Instance.TriggerDialogue("Level4_conversation1");
            if(enableObj)enableObj.SetActive(true);
            if(disableObj)disableObj.SetActive(false);
        });
        mySequence.AppendInterval(1.5f);
        mySequence.AppendCallback(() => {GameManager.Instance.EmptyClick();});
        mySequence.AppendInterval(1.5f);
        mySequence.AppendCallback(() =>
        {
            imageGameObject.GetComponent<ClickEffect>().DisableEffect();
            GameManager.Instance.SetGameState(GameManager.GameState.Normal);
        });
        
    }

    private void OnApplicationQuit()
    {
        Debug.Log("Quit");
        passWordBoxMaterial.color = boxMatColor;
        imageMaterial.color = imageColor;
        // 
    }
}