using UnityEngine;

namespace myObject
{
    public class BossCallButton : MonoBehaviour
    {
        /// <summary> 呼び出しボタンゲームオブジェクト </summary>
        [SerializeField] GameObject CallButton;
        /// <summary> 制限時間ゲームオブジェクト </summary>
        [SerializeField] GameObject BossTimer;
        /// <summary> 制限時間スクリプト </summary>
        BossTimer bossTimer;

        /// <summary>
        /// クリック時動作
        /// ・呼び出しボタンの非アクティブ化
        /// ・ボスステータス設定
        /// ・制限時間タイマー起動
        /// </summary>
        public void OnClick()
        {
            CallButton.SetActive(false);
            EnemyStatus.GetInstance().SetBoss(true);
            BossTimer.SetActive(true);
        }

        /// <summary>
        /// UI表示初期化
        /// ・タイマーと降臨ボタンをOFFに
        /// </summary>
        private void Start()
        {
            BossTimer.SetActive(false);
            CallButton.SetActive(false);
            bossTimer = BossTimer.GetComponent<BossTimer>();
        }
    }
}