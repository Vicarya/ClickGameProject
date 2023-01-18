using UnityEngine;

namespace myObject
{
    class WeaponManager : MonoBehaviour
    {
        /// <summary> シングルトン </summary>
        static WeaponManager manager;
        /// <summary> 武器データ </summary>
        [SerializeField] Weapon[] weapons;
        /// <summary> 購入ボタンリスト </summary>
        [SerializeField] GameObject PerchaseList = null;
        /// <summary> 購入ボタンプレハブ </summary>
        [SerializeField] GameObject PerchaseButton = null;
        /// <summary> プレイヤーセーブデータ </summary>
        [SerializeField] GameObject Player = null;

        /// <summary>
        /// ステータス一括初期化
        /// ロード時にセーブデータを取得する
        /// </summary>
        public void AllInitialize()
        {
            var place = PerchaseList.transform.Find("Viewport/Content");
            //Csvを読込む
            for (int i = 0; i < weapons.Length; ++i)
            {
                var obj = Instantiate(PerchaseButton, place);
                weapons[i].Initialize(obj);
            }
            Player.GetComponent<PlayerStatus>().Initialize(TotalPower());
        }

        /// <summary>
        /// プレイヤーの攻撃力を計算する
        /// ・攻撃力はプレイヤーの基本攻撃力(=1)購入武器の攻撃力
        /// </summary>
        /// <returns>プレイヤーの攻撃力を返す</returns>
        public float TotalPower()
        {
            float totalPower = 0;
            foreach(var v in weapons)
            {
                totalPower += v.Power;
            }
            return totalPower;
        }

        /// <summary>
        /// 武器種類の総数
        /// </summary>
        /// <returns>総数を返す</returns>
        public int ListMax() => weapons.Length;

        /// <summary>
        /// 武器の入手
        /// ・主に直接購入以外のルートで使用
        /// </summary>
        /// <param name="index">武器インデクス</param>
        public void GetWeapon(int index)
        {
            weapons[index].ValueChange();
        }

        /// <summary>
        /// 起動時初期化及びシングルトン設定
        /// </summary>
        void Awake()
        {
            AllInitialize();
            manager = this;
        }

        /// <summary>
        /// 外部アクセス口
        /// </summary>
        /// <returns></returns>
        public static WeaponManager GetInstance() => manager;

    }
}
