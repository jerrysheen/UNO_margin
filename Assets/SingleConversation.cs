using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SingleConversation : MonoBehaviour
{
    public string converName = "DefaultConversation";

    public GameObject maxPosition;

    public GameObject lastPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayClip(int clipIndex)
    {
        if (clipIndex == 0)
        {
            Vector3 position = maxPosition.transform.position;
            position.y = this.transform.position.y;
            this.transform.DOMove(position, 2.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
