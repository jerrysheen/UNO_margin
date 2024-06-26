using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SceneManager : MonoBehaviour
{

    public GameObject root;
    public List<GameObject> canvasData;

    public Dictionary<string, GameObject> canvsDic;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RecordSceneData()
    {
        canvasData = new List<GameObject>();
        canvsDic = new Dictionary<string, GameObject>();
        for (int i = 0; i < root.transform.childCount; i++)
        {
            canvasData.Add(root.transform.GetChild(i).gameObject);
            canvsDic[canvasData[i].name] = canvasData[i];
        }
    }
}

[CustomEditor(typeof(SceneManager))]
public class SceneManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Record Every Scene Data"))
        {
            SceneManager script = target as SceneManager;
            script.RecordSceneData();
        }
    }
}

