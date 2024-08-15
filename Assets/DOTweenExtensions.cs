using DG.Tweening;
using UnityEngine;

public static class DOTweenExtensions
{
    public static Tween EnableAnimator(this Animator animator, bool enabledState)
    {
        return DOVirtual.Float(0, 1, 0.1f, _ => {
            animator.enabled = enabledState;
        });
    }
}