using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class AlbumController : MonoBehaviour
{
    
    public Camera albumCamera;

    public GameObject sprite;
    public RenderTexture outputTexture;

    public List<GameObject> photoPlace;

    public List<RenderTexture> photoContent;
    // Start is called before the first frame update
    private int count = 0;
    void Start()
    {
        sprite.GetComponent<RawImage>().texture = outputTexture;
        //mat.SetTexture("_BaseMap", outputTexture );
        photoPlace = new List<GameObject>();
        photoContent = new List<RenderTexture>();
        count = 0;
        var parent = this.transform.Find("Canvas").gameObject;
        if (parent)
        {
            for (int i = 0; i < photoPlace.Count; i++)
            {
                var go = parent.transform.GetChild(i).gameObject;
                photoPlace.Add(go);
            }
        }

        for (int i = 0; i < photoPlace.Count; i++)
        {
            photoContent.Add(RenderTexture.GetTemporary(1920, 1080, 24));
        }
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
        sprite.GetComponent<RawImage>().texture = photoContent[count];
        albumCamera.targetTexture = outputTexture;
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
