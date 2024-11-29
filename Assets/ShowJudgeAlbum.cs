using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowJudgeAlbum : MonoBehaviour
{
    
    public GameObject album;
    // Start is called before the first frame update
    void Start()
    {
        album.SetActive(false);
    }

    public void onClickAlbumButton()
    {
        album.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
