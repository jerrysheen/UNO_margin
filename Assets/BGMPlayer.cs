using System.Collections;
using System.Collections.Generic;
using JSAM;
using TMPro;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    public bool bgmLevel03_firstPlay = true;
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
        if (bgmLevel03_firstPlay)
        {
            AudioManager.PlayMusic(NewLibraryMusic.Level3_BGM);
            bgmLevel03_firstPlay = false;
        }
    }    
}
