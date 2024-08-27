using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CutSceneEffect : MonoBehaviour
{

    public float part1Speed = 0.5f;
    public float intervalTime = 0.5f;
    public float part2Speed = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCutScene()
    {
        Volume volume = GetComponent<Volume>();
        Color currentColor = new Color(1, 1, 1, 1);  // 初始颜色为白色
        Color firstTargetColor = new Color(0, 0, 0, 1);  // 第一个目标颜色为黑色
        Color secondTargetColor = new Color(1, 1, 1, 1);  // 第二个目标颜色为白色
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(DOTween.To(() => currentColor, x => currentColor = x, firstTargetColor, part1Speed)
                .OnUpdate(() =>
                {
                    volume.profile.TryGet<ColorAdjustments>(out var colorAdjustments);
                    colorAdjustments.colorFilter.value = currentColor;
                }))
            .AppendInterval(intervalTime)
            .Append(DOTween.To(() => currentColor, x => currentColor = x, secondTargetColor, part2Speed)
                .OnUpdate(() =>
                {
                    volume.profile.TryGet<ColorAdjustments>(out var colorAdjustments);
                    colorAdjustments.colorFilter.value = currentColor;
                }));
    }
}

[CustomEditor(typeof(CutSceneEffect))]
public class CutSceneEffectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("CutScene Start"))
        {
            CutSceneEffect cutSceneEffect = (CutSceneEffect)target;
            cutSceneEffect.StartCutScene();
        }
    }
}
