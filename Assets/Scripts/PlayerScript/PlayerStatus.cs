using UnityEngine;
using UnityEngine.UI;

namespace myObject
{
    public class PlayerStatus : MonoBehaviour
    {
        /// <summary> 画像UI </summary>
        [SerializeField] Image Graph = null;
        /// <summary> 所持金UI </summary>
        [SerializeField] Text MoneyView = null;
        /// <summary> 経験UI </summary>
        [SerializeField] Text ExpCharger = null;
        /// <summary> 攻撃力UI </summary>
        [SerializeField] Text ClickPower = null;
        /// <summary> プレイヤーデータ </summary>
        [SerializeField] Player Player = null;
        /// <summary> 現在地(階層)UI </summary>
        [SerializeField] Text NowFloor = null;

        /// <summary> シングルトン </summary>
        static PlayerStatus status;

        /// <summary>
        /// 初期化
        /// ・プレイヤー情報UIに値を挿入
        /// </summary>
        /// <param name="totalPower">合計攻撃力</param>
        public void Initialize(float totalPower)
        {
            Player.Initialize(totalPower);
            MoneyView.text = "所持金：" + Player.Money;
            ExpCharger.text = "所持経験：" + Player.Exp;
            ClickPower.text = "攻撃力：" + Player.ClickPower;
            NowFloor.text = "現在階層：" + Player.NowFloor + "F";
            status = this;
        }

        /// <summary>
        /// プレイヤー情報取得(外部接続用)
        /// </summary>
        /// <returns>シングルトンインスタンスを返す</returns>
        public static PlayerStatus GetInstance() => status;

        /// <summary>
        /// 武器購入
        /// ・武器を購入し、所持金使用および攻撃力の更新処理
        /// ・同時にUIの更新
        /// </summary>
        /// <param name="weaponPrice">武器価格</param>
        /// <returns> 購入フラグ </returns>
        public bool BuyWeapon(int weaponPrice)
        {
            if (Player.Money < weaponPrice) return false;
            MoneyView.text = "所持金：" + Player.Bullance(weaponPrice);
            return true;
        }

        /// <summary>
        /// プレイヤーのステータスアップデート
        /// </summary>
        /// <returns></returns>
        public void PlayerUpdate()
        {
            ClickPower.text = "攻撃力：" +
                    Player.PowerChange(WeaponManager.GetInstance().TotalPower());
        }

        /// <summary>
        /// キャラクター育成
        /// ・キャラクターのレベル上げに経験値を使用する
        /// ・同時にUIの更新
        /// </summary>
        /// <param name="charaExp">必要経験値</param>
        public void GrowCharacter(int charaExp)
            =>ExpCharger.text = "EXP：" + Player.ExpCharge(charaExp);

        public void Enpty() => ExpCharger.text = "enpty";

        /// <summary>
        /// キャラクター雇用
        /// ・キャラクターの新規雇用及び+値の上昇
        /// ・同時にUIの更新
        /// </summary>
        /// <param name="charaPrice">雇用金額</param>
        public void HireCharacter(int charaPrice)
            =>MoneyView.text = "所持金：" + Player.Bullance(charaPrice);
        
        /// <summary>
        /// 敵撃破報酬
        /// </summary>
        /// <param name="enemy">敵データ</param>
        public void EnemyKillReword(Enemy enemy)
        {
            MoneyView.text = "所持金：" + Player.Bullance(enemy.DropGold, false);
            ExpCharger.text = "経験値：" + Player.ExpCharge(enemy.DropExp, false);
        }

        /// <summary>
        /// プレイヤー情報を取得する
        /// </summary>
        /// <returns>プレイヤー情報</returns>
        public Player PlayerInfo()
            =>this.Player;

        /// <summary>
        /// 次のフロアへ進行する
        /// </summary>
        /// <returns></returns>
        public int GoNextFloor()
        {
            NowFloor.text = "現在階層：" + (++Player.NowFloor) + "F";
            return Player.NowFloor;
        }

        /// <summary>
        /// パラメーターリセット
        /// </summary>
        public void ResetParam()
            =>Player = new Player(Player.Graph, 0, 0, 1, 1);
    }
}