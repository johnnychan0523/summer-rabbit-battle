// 引用 Unity API
using UnityEngine;
using UnityEngine.UI;

namespace Johnny
{
    /// <summary>
    /// 動畫系統：透過按鈕控制動畫
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class AnimationSystem : MonoBehaviour
    {
        // 建議用雜湊，可省下字串比較的開銷
        private readonly int hashRun   = Animator.StringToHash("觸發跑步");
        private readonly int hashDeath = Animator.StringToHash("觸發死掉");

        private Animator ani;

        [SerializeField] private Button btnRun;
        [SerializeField] private Button btnDeath;

        // 遊戲一開始呼叫一次
        private void Awake()
        {
            ani = GetComponent<Animator>();

            btnRun.onClick.AddListener(PlayRun);
            btnDeath.onClick.AddListener(PlayDeath);
        }

        /// <summary>播放跑步動畫</summary>
        private void PlayRun()
        {
            ani.SetTrigger(hashRun);
        }

        /// <summary>播放死亡動畫</summary>
        private void PlayDeath()
        {
            ani.SetTrigger(hashDeath);
        }
    }
}
