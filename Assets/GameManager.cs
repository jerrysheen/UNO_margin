using System;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject album;
    public GameObject camera;

    public Camera cam;

    public GameObject level1Mount;
    public GameObject level2Mount;
    public GameObject level3Mount;
    public GameObject level4Mount;
    public GameObject level5Mount;
    private static GameManager instance;
    

    public GameState state = GameState.Normal;
    public LevelState levelState = LevelState.Level0;
    public LevelState nextScene = LevelState.Level1;

    public CinemachineVirtualCamera oldCam = null;

    public Dictionary<LevelState, CinemachineVirtualCamera> levelMountDic;
    public Stack<ClickEffect> currentClickEffect;
    public Stack<string> currentDialogue;
    public Stack<int>  currentBehaviour;
    
    public Dictionary<LevelState, Vector3> levelMountPoints;
    public KeyBoardMouseInput keyBoardMouseInputScript;
    
    public GameObject cutSceneEffectGo;
    public enum GameState
    {
        Normal,
        PlayFullScreenPic,
        PlayDialogue,
        FullScreenCamera
    }

    public enum LevelState
    {
        Level0,
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
    }

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                // 在场景中查找现有的 GameManager 实例
                instance = FindObjectOfType<GameManager>();
                
                // 如果找不到，创建一个新的 GameObject，并添加 GameManager 组件
                if (instance == null)
                {
                    GameObject go = new GameObject("GameManager");
                    instance = go.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject); // 确保不销毁单例对象
        }
        else if (instance != this)
        {
            //Destroy(gameObject); // 确保只有一个单例实例存在
        }
    }


    private void Start()
    {
                
        //todo:
        // testcode
        //EventManager.Instance.TriggerEvent(GameEvent.OnEnterLevel3);
        
        levelMountDic = new Dictionary<LevelState, CinemachineVirtualCamera>();
        levelMountPoints = new Dictionary<LevelState, Vector3>();
        Transform parent = this.transform.Find("VirtualLevelCam");
        Transform[] childTrans = parent.GetComponentsInChildren<Transform>();
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            GameObject child = parent.transform.GetChild(i).gameObject;
            if (child.name == parent.name) continue;
            CinemachineVirtualCamera temp = child.GetComponent<CinemachineVirtualCamera>();
            if (temp)
            {
                if (child.name == "Level0Cam")
                {
                    oldCam = child.GetComponent<CinemachineVirtualCamera>();
                    oldCam.Priority = 11;
                }

                levelMountDic.Add((LevelState)(i), temp);
            }
            else
            {
                Debug.LogError("Camera not exits!!!");
            }
        }
        
        parent = this.transform.Find("MountPoint");
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            GameObject child = parent.transform.GetChild(i).gameObject;
            if (child.name == parent.name) continue;
            levelMountPoints.Add((LevelState)(i), child.transform.position);
        }

        keyBoardMouseInputScript = this.GetComponent<KeyBoardMouseInput>();
        if (!keyBoardMouseInputScript)
        {
            Debug.LogError("No such script");
        }

        currentClickEffect = new Stack<ClickEffect>();
        currentDialogue = new Stack<string>();
        currentBehaviour = new Stack<int>();
    }

    public void TriggerDialogue(string name)
    {
        DialogueManager.Instance.TriggerDialogue(name);
        currentDialogue.Push(name);
        currentBehaviour.Push(0);
    }

    public void PushDialogueIntoStack(string name)
    {
        currentDialogue.Push(name);
        currentBehaviour.Push(2);
    }


    public bool SetGameState(GameState newState)
    {
        state = newState;
        if (newState == GameState.FullScreenCamera)
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }

        return true;
    }

    // public void ShowAlbum()
    // {
    //     Debug.Log("ShowAlbum");
    //     album.transform.position = new Vector3(level3Mount.transform.position.x, level3Mount.transform.position.y, album.transform.position.z);
    // }
    //
    //
    // public void OpenCamera()
    // {
    //     Debug.Log("ShowAlbum");
    //     camera.transform.position = new Vector3(level3Mount.transform.position.x, level3Mount.transform.position.y, camera.transform.position.z);
    //
    // }

    // 这里添加 GameManager 的其他功能和方法
    
    public void ItemClicked(GameObject go)
    {
        if(state == GameState.FullScreenCamera)
        {
            EmptyClick();
            return;
        }
        //if(state == GameState.PlayFullScreenPic) return;
        //  这里之后会添加触发条件，来控制。
        ClickEffect clickEffect = go.GetComponent<ClickEffect>();
        if (clickEffect)
        {
            currentClickEffect.Push(clickEffect);
            currentBehaviour.Push(1);
            clickEffect.ChangeState();
        }
    }
    
    public void EmptyClick()
    {
        EventManager.Instance.TriggerEvent(GameEvent.EmptyClicked);
        if(currentBehaviour.Count > 0)
        {
            int behaviour = currentBehaviour.Pop();
            if (behaviour == 0)
            {
                // 对话隐藏逻辑
                string currDialogue = currentDialogue.Pop();
                DialogueManager.Instance.DisableDialogue(currDialogue);
                // if (currentClickEffect != null)
                // {
                //     state = GameState.PlayFullScreenPic;
                // }
                // else
                // {
                //     state = GameState.Normal;
                // }
            }
            else if(behaviour == 1)
            {
                // 点击效果触发。
                ClickEffect currEffect = currentClickEffect.Pop();
                currEffect.DisableEffect();
                //state = GameState.Normal;
            }
            else if (behaviour == 2)
            {
                // 对话触发
                string currDialogue = currentDialogue.Pop();
                DialogueManager.Instance.TriggerDialogue(currDialogue);
                //state = GameState.PlayDialogue;
                currentDialogue.Push(currDialogue);
                currentBehaviour.Push(0);
            }
        }
    }
    
    public void SwitchToScene()
    {
        cutSceneEffectGo.GetComponent<CutSceneEffect>().StartCutScene();
        
        levelMountDic.TryGetValue(nextScene, out CinemachineVirtualCamera nextCam);
        levelMountPoints.TryGetValue(nextScene, out Vector3 nextPos);
        EventManager.Instance.TriggerEvent(GetLevelEvent(nextScene.ToString(), "_Entering"), 1.0f);
        EventManager.Instance.TriggerEvent(GameEvent.MoveCamera, nextPos);
        if (nextCam)
        {
            nextCam.Priority = 12;
            oldCam.Priority = 10;
            oldCam = nextCam;
            oldCam.Priority = 11;
            keyBoardMouseInputScript.startPosition = nextPos;
        }
        else
        {
            Debug.LogError("false!!!");
        }
    }
    
    GameEvent GetLevelEvent(string Level, string EnterOrExit)
    {
        GameEvent temp = (GameEvent)Enum.Parse(typeof(GameEvent), Level + EnterOrExit);
        Debug.Log(temp.ToString());
        return temp;
    }
}
[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GameManager gameManager = (GameManager) target;
        // if (GUILayout.Button("Show Album"))
        // {
        //     gameManager.ShowAlbum();
        // }
        // if (GUILayout.Button("Open Camera"))
        // {
        //     gameManager.OpenCamera();
        // }

        if (GUILayout.Button("Switch To Scene"))
        {
            gameManager.SwitchToScene();
        }
    }
}