using UnityEngine;

namespace myObject
{
    [CreateAssetMenu(fileName = "ScriptableObject", menuName = "Character", order = 1)]
    public class Character : Item
    {
        /// <summary> 特大画像 </summary>
        public Sprite[] LargeGraphs;
        /// <summary> レベル </summary>
        public int Level;
        /// <summary> レベルアップ用経験値 </summary>
        public int GrawExp;
        /// <summary> 基礎攻撃速度 </summary>
        public int BaseSpeed;
        /// <summary> 攻撃速度 </summary>
        public int Speed;
        /// <summary> 次レベルのスピード </summary>
        public int futureSpeed;
        /// <summary> 次レベルのパワー </summary>
        public float futurePower;
        /// <summary> データcsvのディレクトリ </summary>
        //public string dataDir;
        /// <summary> csvデータ </summary>
        public TextAsset paramData;

        /// <summary> ステータス </summary>
        CharacterStatus status;
        /// <summary> キャラステータスUI </summary>
        GameObject statObj;
        /// <summary> 育成 </summary>
        CharacterLvUp lvUp;
        /// <summary> キャラ育成ボタンUI </summary>
        GameObject lvupObj;
        /// <summary> 雇用 </summary>
        CharacterHire hire;

        /// <summary> 攻撃待機時間 </summary>
        float waitCount;

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="statusPanel">キャラステータスUIリスト</param>
        /// <param name="lvUpButton">キャラ育成UIリスト</param>
        /// <param name="hireButton">キャラ雇用UIリスト</param>
        public void Initialize(GameObject statusPanel, GameObject lvUpButton, GameObject hireButton)
        {
            if(Count == 0)
            {
                Price = basePrice;
                Level = 0;
            }
            statObj = statusPanel;
            status = statusPanel.GetComponent<CharacterStatus>();
            if(this.Count==0) statusPanel.SetActive(false);
            status.Initialize(this);
            lvupObj = lvUpButton;
            lvUp = lvUpButton.GetComponent<CharacterLvUp>();
            if(this.Count==0) lvUpButton.SetActive(false);
            lvUp.Initialize(this);
            hire = hireButton.GetComponent<CharacterHire>();
            hire.Initialize(this);
        }

        /// <summary>
        /// レベルアップ処理
        /// </summary>
        public void LvUp()
        {
            ++Level;
            ValueChange();
        }

        /// <summary>
        /// 雇用処理
        /// </summary>
        public void Hire()
        {
            ++Count;
            Price *= 2;
            ValueChange();
        }

        /// <summary>
        /// 値の変更
        /// </summary>
        public void ValueChange()
        {
            //ステータスロード
            if (Count > 0)
            {
                lvupObj.SetActive(true);
                statObj.SetActive(true);
            }
            //var strs = LoadCSV.LoadCsv(Application.dataPath + "/Resources/" + dataDir + ".csv", Level);
            var strs = LoadCSV.LoadCsv(paramData, Level);
            Power = float.Parse(strs[1]);
            Speed = int.Parse(strs[2]);
            GrawExp = int.Parse(strs[3]);
            //var strsAf = LoadCSV.LoadCsv(Application.dataPath + "/" + dataDir + ".csv", Level + 1);
            var strsAf = LoadCSV.LoadCsv(paramData, Level+1);
            futurePower = float.Parse(strsAf[1]);
            futureSpeed = int.Parse(strsAf[2]);
            lvUp.ValueChange(Name + " +" + (Count-1), Level, Power, futurePower, Speed, futureSpeed, GrawExp);
            hire.ValueChange(Name + " +" + (Count - 1), Level, Power, futurePower, Price);
            status.ValueChange(Name, (Count - 1), Level, Power, Speed);
        }

        /// <summary>
        /// 攻撃待機時間のカウント(自分の素早さ分進める)
        /// </summary>
        /// <param name="enemySpeed">敵素早さ</param>
        /// <returns>攻撃回数</returns>
        public int CountUp(float enemySpeed)
        {
            int attackCount = 0; // 攻撃可能回数
            waitCount += Speed;
            while(waitCount > enemySpeed)
            {
                ++attackCount; // 閾値を超えた分攻撃できる
                waitCount -= enemySpeed;
            }
            return attackCount;
        }

        /// <summary>
        /// 攻撃できる回数の判定(閾値判定法)
        /// </summary>
        /// <param name="enemySpeed">敵素早さ</param>
        /// <returns>攻撃ダメージ</returns>
        public float Attack(float enemySpeed = 100) => Power * CountUp(enemySpeed);

    }
}
