using UnityEngine;

namespace myObject
{
    public class AttackButton : MonoBehaviour
    {
        /// <summary>
        /// クリック時実行
        /// ・プレイヤーの攻撃力を取得し、敵にダメージを与える
        /// </summary>
        public void OnClick()
            =>EnemyStatus.GetInstance().Attacked(
                PlayerStatus.GetInstance().PlayerInfo().ClickPower);
    }
}