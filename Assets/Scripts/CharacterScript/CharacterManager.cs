using System.Collections;
using UnityEngine;

namespace myObject
{
    class CharacterManager : MonoBehaviour
    {
        /// <summary> シングルトン </summary>
        static CharacterManager manager;

        /// <summary> キャラデータ </summary>
        [SerializeField] Character[] characters = null;
        /// <summary> キャラクターリスト下地 </summary>
        [SerializeField] GameObject CharacterList = null;
        /// <summary> キャラクターステータスパネルプレハブ </summary>
        [SerializeField] GameObject CharacterPanel = null;
        /// <summary> 育成ボタンリスト下地 </summary>
        [SerializeField] GameObject GrawList = null;
        /// <summary> キャラクターレベル上げボタンプレハブ </summary>
        [SerializeField] GameObject GrawButton = null;
        /// <summary> 雇用ボタンリストパネル </summary>
        [SerializeField] GameObject HireList = null;
        /// <summary> キャラクター雇用ボタンプレハブ </summary>
        [SerializeField] GameObject HireButton = null;

        /// <summary>
        /// ステータス一括初期化
        /// ロード時にセーブデータを取得する
        /// </summary>
        public void AllInitialize()
        {
            var place1 = CharacterList.transform.Find("Viewport/Content").transform;
            var place2 = GrawList.transform.Find("Viewport/Content").transform;
            var place3 = HireList.transform.Find("Viewport/Content").transform;
            //Csvを読込む
            for (int i = 0; i < characters.Length; ++i)
            {
                var stat = Instantiate(CharacterPanel, place1);
                var graw = Instantiate(GrawButton, place2);
                var hire = Instantiate(HireButton, place3);
                characters[i].Initialize(stat, graw, hire);
            }
        }
        
        /// <summary>
        /// キャラクター情報取得(外部接続用)
        /// </summary>
        /// <returns>シングルトンインスタンスを返す</returns>
        public static CharacterManager GetInstance() => manager;

        /// <summary>
        /// ゲーム開始時起動
        /// ・キャラクターの攻撃計算を起動する
        /// ・シングルトンをセットする
        /// </summary>
        void Awake()
        {
            AllInitialize();
            manager = this;
            StartCoroutine("AttackCorrect");
        }

        /// <summary>
        /// キャラクターの攻撃計算
        /// ・敵の素早さを閾値(しきいち)としてフレーム時間を設定
        /// ・キャラクターの素早さを毎フレーム加算していき
        /// 閾値を超えていたら攻撃処理をする
        /// </summary>
        /// <returns>攻撃合計</returns>
        IEnumerator AttackCorrect()
        {
            while (true)
            {
                while (EnemyStatus.GetInstance() == null) {
                    yield return new WaitForSeconds(0.001f);
                }
                float enemySpeed = EnemyStatus.GetInstance().Enemy.Speed;
                float sum = 0;
                foreach (var c in characters)
                {
                    sum += c.Attack(enemySpeed);
                }
                EnemyStatus.GetInstance().Attacked(sum); 
                yield return new WaitForSeconds(enemySpeed * 0.001f);
            }
        }

        /// <summary>
        /// データ初期化
        /// ・キャラのレベル・カウントを0に戻す
        /// </summary>
        public void ParamReset()
        {
            foreach(var c in characters)
            {
                c.Count = 0;
                c.Level = 0;
            }
            AllInitialize();
        }

        /// <summary>
        /// キャラ獲得
        /// ・雇用(ガチャなど、通常の雇用雇用ボタンルート以外)
        /// </summary>
        /// <param name="index">キャラインデクスNo</param>
        /// <param name="value">価格</param>
        /// <param name="noChoice">条件確認フラグ</param>
        /// <returns>獲得フラグ</returns>
        public bool CharaGet(int index, int value = 0, bool noChoice = false)
        {
            if ((characters[index].Price <= value) || noChoice) return false;
            characters[index].Hire();
            return true;
        }

        public int ListMax() => characters.Length;
        //private void Update()
        //{
        //    foreach(var c in characters)
        //    {
        //        c.Attack();
        //    }
        //}
    }
}
