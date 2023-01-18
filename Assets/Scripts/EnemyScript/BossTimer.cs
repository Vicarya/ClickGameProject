using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace myObject
{
    public class BossTimer : MonoBehaviour
    {
        /// <summary> 制限時間 </summary>
        [SerializeField] int TimerCountMax = 0;
        /// <summary> 刻む時間 </summary>
        [SerializeField] float TimerCountClock = 0;
        /// <summary> 残り時間 </summary>
        float timerCount;
        /// <summary> リザルト表示テキスト </summary>
        [SerializeField] GameObject BossBattleResult = null;
        /// <summary> リザルト表示時間 </summary>
        [SerializeField] int ResultAppearTime = 0;

        /// <summary>
        /// 制限時間カウント
        /// ・タイマーが0になると「敗北」リザルト
        /// ・敵を倒すと「勝利」リザルト
        /// </summary>
        /// <returns>リザルト表示</returns>
        IEnumerator TimerCountDown()
        {
            var countText = gameObject.GetComponent<Text>();
            countText.text = "" + (timerCount = TimerCountMax);
            var enemy = EnemyStatus.GetInstance();
            while (timerCount > 0)
            {//タイマーカウント0になるまで実行
                if (!enemy)
                {//敵情報がセットされるまで待機
                    yield return new WaitForSeconds(TimerCountClock);
                    continue;
                }
                else if (!enemy.Bossflg) break;//ボスを倒すと強制停止
                timerCount -= TimerCountClock;
                countText.text = "" + (int)timerCount;
                yield return new WaitForSeconds(TimerCountClock);
            }
            yield return Result(enemy.Bossflg, enemy.Enemy.DropGold, enemy.Enemy.DropExp);
        }
        
        /// <summary>
        /// タイマー起動
        /// </summary>
        void OnEnable()
        {
            StartCoroutine("TimerCountDown");
        }

        /// <summary>
        /// タイマー削除と同時にリザルトも削除
        /// </summary>
        private void OnDisable()
        {
            BossBattleResult.SetActive(false);
        }

        /// <summary>
        /// リザルト表示
        /// ・リザルト表示中は敵に干渉できない様にする
        /// ・ボスの生存状態に従ってリザルトを表示する
        /// ・表示が終了したら敵の無敵をはがして非活性化する
        /// </summary>
        /// <param name="bosslive">ボス生存フラグ</param>
        /// <param name="gold">獲得金額</param>
        /// <param name="exp">獲得経験</param>
        IEnumerator Result(bool bosslive, int gold = 0, int exp = 0)
        {
            EnemyStatus.GetInstance().Invincible(true);
            BossBattleResult.SetActive(true);
            var txt = BossBattleResult.GetComponent<Text>();
            if (bosslive)
            {
                txt.text = "LOSE\n武器やキャラを強化して\n再チャレンジ";
                txt.color = Color.cyan;
            }
            else
            {
                txt.text = $"WIN\n獲得金：{gold}\n獲得経験：{exp}";
                txt.color = Color.yellow;
            }
            yield return new WaitForSeconds(ResultAppearTime);
            EnemyStatus.GetInstance().SetBoss(false);
            EnemyStatus.GetInstance().Invincible(false);
            gameObject.SetActive(false);
            yield return null;
        }
    }
}