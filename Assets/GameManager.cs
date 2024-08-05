using System;
using Unity.VisualScripting;
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
    public LevelState levelState = LevelState.Intro_Entering;
    
    
    
    public ClickEffect currentClickEffect;
    public string currentDialogue;
    public enum GameState
    {
        Normal,
        PlayFullScreenPic,
        PlayDialogue
    }

    public enum LevelState
    {
        Intro_Entering,
        Intro,
        Intro_Leaving,
        Level1_Entering,
        Level1,
        Level1_Leaving,
        Level2_Entering,
        Level2,
        Level2_Leaving,
        Level3_Entering,
        Level3,
        Level3_Leaving,
        Level4_Entering,
        Level4,
        Level4_Leaving,
        Level5_Entering,
        Level5,
        Level5_Leaving,
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
        EventManager.Instance.TriggerEvent(GameEvent.OnEnterLevel3);
    }

    public void TriggerDialogue(string name)
    {
        DialogueManager.Instance.TriggerDialogue(name);
        currentDialogue = name;
    }


    public bool SetGameState(GameState newState)
    {
        state = newState;
        return true;
    }

    public void ShowAlbum()
    {
        Debug.Log("ShowAlbum");
        album.transform.position = new Vector3(level3Mount.transform.position.x, level3Mount.transform.position.y, album.transform.position.z);
    }

    
    public void OpenCamera()
    {
        Debug.Log("ShowAlbum");
        camera.transform.position = new Vector3(level3Mount.transform.position.x, level3Mount.transform.position.y, camera.transform.position.z);

    }

    // 这里添加 GameManager 的其他功能和方法
    
    public void ItemClicked(GameObject go)
    {
        //  这里之后会添加触发条件，来控制。
        ClickEffect clickEffect = go.GetComponent<ClickEffect>();
        if (clickEffect)
        {
            currentClickEffect = clickEffect;
            clickEffect.ChangeState();
        }
    }
    
    public void EmptyClick()
    {
        EventManager.Instance.TriggerEvent(GameEvent.EmptyClicked);
        if (currentDialogue != "")
        {
            DialogueManager.Instance.DisableDialogue(currentDialogue);
            currentDialogue = "";
            if (currentClickEffect != null)
            {
                state = GameState.PlayFullScreenPic;
            }
            else
            {
                state = GameState.Normal;
            }
        }
        else if (currentClickEffect != null)
        {
            currentClickEffect.DisableEffect();
            currentClickEffect = null;
            state = GameState.Normal;
        }
    }
}