using UnityEngine;

namespace myObject
{
    [CreateAssetMenu(fileName = "ScriptableObject", menuName = "Player", order = 4)]
    public class Player : ScriptableObject
    {
        /// <summary> 画像 </summary>
        public Sprite Graph;
        /// <summary> 所持金 </summary>
        public int Money;
        /// <summary> 経験 </summary>
        public int Exp;
        /// <summary> 攻撃力 </summary>
        public float ClickPower;
        /// <summary> 現在地(階層) </summary>
        public int NowFloor;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="graph">プレイヤー画像</param>
        /// <param name="money">所持金</param>
        /// <param name="exp">経験値</param>
        /// <param name="power">攻撃力</param>
        /// <param name="floor">現在地(階層)</param>
        public Player(Sprite graph, int money, int exp, int power, int floor)
        {
            Graph = graph; Money = money; Exp = exp; ClickPower = power; NowFloor = floor;
        }

        /// <summary>
        /// 初期化
        /// ・ゲーム開始時プレイヤーの攻撃力の再設定
        /// </summary>
        /// <param name="weaponTotal">武器攻撃力の合計値</param>
        public void Initialize(float weaponTotal)
        {
            PowerChange(weaponTotal);
        }

        /// <summary>
        /// プレイヤーの攻撃力の再設定
        /// </summary>
        /// <param name="weaponTotal"></param>
        /// <returns>最終攻撃力</returns>
        public float PowerChange(float weaponTotal)
        {
            ClickPower = 1 + weaponTotal;
            return ClickPower;
        }

        /// <summary>
        /// 残高収支
        /// </summary>
        /// <param name="value">価格</param>
        /// <param name="loseflg">支出フラグ</param>
        /// <returns>最終残高</returns>
        public int Bullance(int value, bool loseflg = true)
        {
            if (loseflg) value = -value;
            Money += value;
            return Money;
        }

        /// <summary>
        /// 経験値総量
        /// </summary>
        /// <param name="value">使用量</param>
        /// <param name="loseflg">支出フラグ</param>
        /// <returns>最終経験値</returns>
        public int ExpCharge(int value, bool loseflg = true)
        {
            if (loseflg) value = -value;
            Exp += value;
            return Exp;
        }
    }
}
