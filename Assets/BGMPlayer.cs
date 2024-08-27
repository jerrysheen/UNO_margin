using System.Collections;
using System.Collections.Generic;
using JSAM;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        EventManager.Instance.StartListening(GameEvent.Level3_Entering, OnLevelStart);
    }

    private void OnDisable()
    {
        EventManager.Instance.StopListening(GameEvent.Level3_Entering, OnLevelStart);

    }

    private void OnLevelStart()
    {
        AudioManager.PlayMusic(NewLibraryMusic.Level3_BGM);
    }    
}
