using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordController : MonoBehaviour
{
    // Start is called before the first frame update
    public string fullPassword = "158";
    public List<string> currPass;
    public List<PasswordButton> currPassScript;
    void Start()
    {
        fullPassword = "158";
        currPass = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        GameManager.Instance.TriggerDialogue("Level4_conversation6");
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
    }
}
