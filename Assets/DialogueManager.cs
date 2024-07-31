using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }  // 单例的实例访问器

    public Dictionary<string, GameObject> dialogueDic;

    public List<GameObject> levelDialogueList;
    public List<GameObject> debugList;
    private void Awake()
    {
        // 确保只有一个实例存在
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // 防止被销毁
        }
        else
        {
            Destroy(gameObject);
        }
        
        GameObject level1 = GameObject.Find("Level1");
        GameObject level2 = GameObject.Find("Level2");
        GameObject level3 = GameObject.Find("Level3");
        GameObject level4 = GameObject.Find("Level4");
        GameObject level5 = GameObject.Find("Level5");
        
        levelDialogueList = new List<GameObject>(){level1, level2, level3, level4, level5};
        debugList = new List<GameObject>();
        dialogueDic = new Dictionary<string, GameObject>();
        foreach (var level_GO in levelDialogueList)
        {
            // 不要这么写， 直接是dialogue的地方，找到conversation controller。然后那边去做这件事。
            // 具体 click里面吊起manager， 传过来一个string，
            // manager通过string去找到对应的场景，以及他的conversationcontroller、
            // controller去触发对应的对话。
            
            // GameObject dialogueParent = level_GO.transform.Find("Conversation").gameObject.transform.Find("ConversationRoot").gameObject;
            //
            // for (int i = 0; i < dialogueParent.transform.childCount; i++)
            // {
            //     GameObject dialogue = dialogueParent.transform.GetChild(i).gameObject;
            //     if(dialogue.name == dialogueParent.name)continue;
            //     dialogueDic.Add(level_GO.name + "_" + dialogue.name, dialogue);
            //     Debug.Log(level_GO.name +"_"+ dialogue.name);
            //     debugList.Add(dialogue);
            // }
        }
    }

    // 显示对话框
    public void Show()
    {
        // 逻辑来显示对话框
        Debug.Log("Dialogue shown.");
        // 此处可以添加更多代码，比如设置对话框内容，使其可见等
    }

    // 隐藏对话框
    public void Off()
    {
        // 逻辑来隐藏对话框
        Debug.Log("Dialogue hidden.");
        // 此处可以添加更多代码，比如使对话框不可见
    }
}