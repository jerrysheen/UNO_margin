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

    private int currentClip = 0;

    private int playClipPart;

    // 默认有两个part， 进场和消失。
    public int clipTotalPart = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitConversationList()
    {
        conversationList = new List<SingleConversation>();
        Transform[] childTrans = converRoot.GetComponentsInChildren<Transform>();
        Debug.Log(childTrans.Length);
        foreach (var childTran in childTrans)
        {
            if (childTran.name == converRoot.name) continue;
            SingleConversation temp = childTran.GetComponent<SingleConversation>();
            if(!temp) Debug.Log("Assign SingleConversation script!!");
            conversationList.Add(temp);
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
        conversationList[currentClip++].PlayClip(0);

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