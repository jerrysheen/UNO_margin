using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class AlbumController : MonoBehaviour
{
    
    public Camera albumCamera;
    public GameObject albumCanvas;
    public GameObject cameraObject;
    public GameObject sprite;
    public RenderTexture outputTexture;

    public List<GameObject> photoPlace;

    public List<RenderTexture> photoContent;
    // Start is called before the first frame update
    public int count = 0;
    void Start()
    {
        //sprite.GetComponent<RawImage>().texture = outputTexture;
        //mat.SetTexture("_BaseMap", outputTexture );
        photoPlace = new List<GameObject>();
        photoContent = new List<RenderTexture>();
        count = 0;
        var parent = this.transform.Find("Canvas").gameObject;
        if (parent)
        {
            for (int i = 0; i < parent.transform.childCount; i++)
            {
                var go = parent.transform.GetChild(i).gameObject;
                photoPlace.Add(go);
            }
        }
        else
        {
            Debug.Log("Not Find Canvas");
        }

        for (int i = 0; i < photoPlace.Count; i++)
        {
            photoContent.Add(RenderTexture.GetTemporary(1920, 1080, 24));
        }

        foreach (var go in photoPlace)
        {
            Color currColor = go.GetComponent<RawImage>().color;
            go.GetComponent<RawImage>().color = new Color(currColor.r, currColor.g, currColor.b, 0.0f);
        }
        
        albumCanvas.SetActive(false);
    }


    private void OnApplicationQuit()
    {
        foreach (var rt in photoContent)
        {
            if (rt)
            {
                RenderTexture.ReleaseTemporary(rt);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CaptureOnePhoto()
    {
        if (count >= photoPlace.Count)
        {
            return;
        }
        albumCamera.targetTexture = photoContent[count];
        albumCamera.Render();
        photoPlace[count].GetComponent<RawImage>().texture = photoContent[count];
        albumCamera.targetTexture = outputTexture;
        photoPlace[count].GetComponent<RawImage>().DOColor(new Color(1,1,1,1), 1);
        count++;
    }
}
[CustomEditor(typeof(AlbumController))]
public class AlbumControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Capture"))
        {
            AlbumController albumController = (AlbumController)target;
            albumController.CaptureOnePhoto();

        }
    }
}
