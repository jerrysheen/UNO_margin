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
    private Material targetMaterial;
    void Start()
    {
        targetMaterial = this.GetComponent<SpriteRenderer>().material;
    }

    public void PlayClip(int clipIndex)
    {
        if (clipIndex == 0)
        {
            // 创建一个序列
            Sequence mySequence = DOTween.Sequence();
            
            Vector3 position = this.transform.position;
            position.y = maxPosition.transform.position.y;
            // 添加向上移动的动画，假设向上移动 5 单位，耗时 1 秒
            mySequence.Append(this.transform.DOMove(position, 0.3f));
            position.y = lastPosition.transform.position.y;
            mySequence.Append(this.transform.DOMove(position, 0.15f));
            
            // 播放序列
            mySequence.Play();
            Debug.Log("Dialogue Play" + this.name);
        }
        else if (clipIndex == 1)
        {
            // 创建一个序列
            Sequence mySequence = DOTween.Sequence();
            
            mySequence.Append(DOTween.To(() => targetMaterial.GetFloat("_DissolveAmount"), 
                x => targetMaterial.SetFloat("_DissolveAmount", x), 
                1.0f, // 目标值
                1.0f  // 持续时间
            ));
            
            // 播放序列
            mySequence.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
