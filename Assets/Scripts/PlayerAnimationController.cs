using UnityEngine;

/// <summary>
/// 專門負責播放跑步／死亡動畫，不改動原有 Player.cs
/// </summary>
public class PlayerAnimationController : MonoBehaviour
{
    [Tooltip("拖進有兔子 Animator 的物件")]
    [SerializeField] Animator animator;

    /// <summary>UI 按鈕呼叫：播放跑步動畫</summary>
    public void PlayRun()
    {
        if (animator != null)
            animator.SetTrigger("Run");
    }

    /// <summary>UI 按鈕呼叫：播放死亡動畫</summary>
    public void PlayDie()
    {
        if (animator != null)
            animator.SetTrigger("Die");
    }
}
