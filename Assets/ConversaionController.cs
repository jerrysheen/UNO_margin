using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;

public class ConversaionController : MonoBehaviour
{

    public GameObject converPopMaxPosition;
    public GameObject converLastPosition;
    public GameObject converRoot;
    
    public  List<SingleConversation> conversationList;
    public  Dictionary<string, SingleConversation> conversationDic;

    public int currentClip = 0;

    public int playClipPart;

    // 默认有两个part， 进场和消失。
    public int clipTotalPart = 2;
    // Start is called before the first frame update


    private void Start()
    {
        InitConversationList();
    }

    public void PlayDialogue(string name)
    {
        conversationDic.TryGetValue(name, out var script);
        if(script == null) Debug.LogError("No such dialogue");
        script.maxPosition = converPopMaxPosition;
        script.lastPosition = converLastPosition;
        script.PlayClip(0);
    }

    public void DisableDialogue(string name)
    {
        conversationDic.TryGetValue(name, out var script);
        if(script == null) Debug.LogError("No such dialogue");
        script.maxPosition = converPopMaxPosition;
        script.lastPosition = converLastPosition;
        script.PlayClip(1);
    }

    void InitConversationList()
    {
        conversationList = new List<SingleConversation>();
        conversationDic = new Dictionary<string, SingleConversation>();
        Transform[] childTrans = converRoot.GetComponentsInChildren<Transform>();
        string level_GO = this.transform.parent.name;
        Debug.Log(childTrans.Length);
        foreach (var childTran in childTrans)
        {
            if (childTran.name == converRoot.name) continue;
            SingleConversation temp = childTran.GetComponent<SingleConversation>();
            if (!temp)
            {
                Debug.Log("Assign SingleConversation script!!");
                return;
            }
            conversationList.Add(temp);
            conversationDic.Add(level_GO +"_"+ temp.gameObject.name, temp);
        }
    }

    public void PlayOneClip()
    {
        if (conversationList == null || conversationList.Count == 0)
        {
            InitConversationList();
        }

        conversationList[currentClip].maxPosition = converPopMaxPosition;
        conversationList[currentClip].lastPosition = converLastPosition;
        conversationList[currentClip].PlayClip(playClipPart);

        playClipPart++;
        playClipPart = playClipPart % 2;
        if (playClipPart == 0) currentClip++;
    }
    
}

[CustomEditor(typeof(ConversaionController))]
public class ConversationControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ConversaionController script = target as ConversaionController;
        if (GUILayout.Button("testPlay"))
        {
            script.PlayOneClip();
        }
    }
}