using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace myObject
{
    public class EnemyStatus : MonoBehaviour
    {
        /// <summary> 可動域 </summary>
        [SerializeField] int MoveWidth = 10;
        /// <summary> ボス召喚必要キル数 </summary>
        [SerializeField] int NeedCallBoss = 0;
        /// <summary> 現キル数 </summary>
        int enemyKillCount = 0;
        /// <summary> キルカウント表示テキスト </summary>
        [SerializeField] Text EnemyKillCount = null;
        /// <summary> 画像表示 </summary>
        [SerializeField] Image Graph = null;
        /// <summary> レベル表示 </summary>
        [SerializeField] Text Lv = null;
        /// <summary> HP表示 </summary>
        [SerializeField] Text Hp = null;
        /// <summary> 素早さ表示 </summary>
        [SerializeField] Text Speed = null;
        /// <summary> シングルトン </summary>
        static EnemyStatus enemyInstance = null;
        /// <summary> 敵オブジェクト </summary>
        public Enemy Enemy { get; private set; }
        /// <summary> 敵種類インデクス </summary>
        int enemyIndex = 0;
        /// <summary> モブリスト </summary>
        [SerializeField] Enemy[] EnemyList = null;
        /// <summary> ボス種類インデクス </summary>
        int bossIndex = 0;
        /// <summary> ボスリスト </summary>
        [SerializeField] Enemy[] BossList = null;
        /// <summary> ボス生存フラグ </summary>
        public bool Bossflg { get; private set; }
        /// <summary> ボス無敵フラグ </summary>
        public bool invincible { get; private set; }
        /// <summary> ボス召喚ボタン </summary>
        [SerializeField] GameObject BossCallButton = null;
        /// <summary> 攻撃ヒットエフェクト </summary>
        [SerializeField] GameObject AttackedEffect = null;

        /// <summary>
        /// 敵初期化
        /// ・敵が倒される度に再登場させる
        /// ・キルカウントの表示更新
        /// </summary>
        /// <param name="enemy">登場させる敵パラメータ</param>
        public void Initialize(Enemy enemy)
        {
            AttackedEffect.SetActive(false);
            if(enemy.Lv==1) enemy.ValueChange(1);
            Enemy = enemy;
            ValueChange(enemy.Graph, enemy.Lv, enemy.Hp = enemy.MaxHp, enemy.Speed);
            EnemyKillCount.text = $"ボス必要撃破数:{enemyKillCount} / {NeedCallBoss}";
            enemyInstance = this;
        }

        /// <summary>
        /// 敵ステータス取得(外部接続用)
        /// </summary>
        /// <returns>敵インスタンスアクセス口を返す</returns>
        public static EnemyStatus GetInstance() => enemyInstance;

        /// <summary>
        /// ステータス表示更新
        /// </summary>
        /// <param name="graph">画像テクスチャ</param>
        /// <param name="lv">レベル</param>
        /// <param name="hp">HP</param>
        /// <param name="speed">素早さ</param>
        public void ValueChange(Sprite graph, int lv, float hp, int speed)
        {
            Graph.sprite = graph;
            Lv.text = "Lv:" + lv;
            Hp.text = "HP:" + (int)hp;
            Speed.text = "素早さ:" + speed;
        }

        /// <summary>
        /// ゲーム開始時に敵ステータスの初期化(モブ召喚)
        /// </summary>
        private void Awake()
        {
            invincible = false;
            enemyKillCount = 0;
            Initialize(EnemyList[0]);
        }

        /// <summary>
        /// 角度(度数法)
        /// </summary>
        float rad = 0;
        /// <summary>
        /// 敵を常に上下させる
        /// ・radを用いて変化させる
        /// </summary>
        void Update()
        {
            if (Enemy)
            {
                gameObject.transform.localPosition = new Vector3(0, Mathf.Sin(rad), 0)*MoveWidth;
                rad += 0.001f * Enemy.Speed;
            }
        }

        /// <summary>
        /// 敵撃破時挙動
        /// ・キル数増加
        /// ・階層上昇<param UpStair = "未確認"></param>
        /// ・初期化
        /// </summary>
        public void OnKilled()
        {
            ++enemyKillCount;
            PlayerStatus.GetInstance().EnemyKillReword(Enemy);
            if(enemyKillCount >= NeedCallBoss && false == BossCallButton.activeSelf)
            {
                BossCallButton.SetActive(true);
            }
            if (Bossflg)
            {
                UpStair();
                Bossflg = false;
            }
            // EnemyListのインデクスを一つ進める(endを超えたら0に戻す)
            Initialize(EnemyList[(++enemyIndex) % EnemyList.Length]);
        }

        /// <summary>
        /// 被弾
        /// ・クリック時プレイヤーからダメージを受ける
        /// ・適時キャラクターからダメージを受ける
        /// ・HPが0になると志望(OnKilled()が動く)
        /// </summary>
        /// <param name="damage"></param>
        public void Attacked(float damage)
        {
            if (invincible) return;
            Enemy.Hp -= damage;
            ValueChange(Enemy.Graph, Enemy.Lv, Enemy.Hp, Enemy.Speed);
            if(damage!=0) StartCoroutine("DamageEffect");
            if (Enemy.Hp <= 0)
            {
                OnKilled();
            }
        }

        /// <summary>
        /// 被弾時爆発エモートを表示
        /// </summary>
        /// <returns>0.02秒毎に非表示</returns>
        IEnumerator DamageEffect()
        {
            AttackedEffect.SetActive(true);
            yield return new WaitForSeconds(0.02f);
            AttackedEffect.SetActive(false);
            yield return null;
        }

        /// <summary>
        /// ボス設定(ボスが死んだときは消す)
        /// </summary>
        /// <param name="callflg">呼び出しか否か</param>
        public void SetBoss(bool callflg)
        {
            Bossflg = callflg;
            if (Bossflg)
            {
                enemyKillCount = 0;
                EnemyKillCount.text = "";
                Initialize(BossList[(++bossIndex) % BossList.Length]);
            }
            else
            {
                Initialize(EnemyList[(++enemyIndex) % EnemyList.Length]);
            }
        }

        /// <summary>
        /// 無敵化(主にリザルト等待機時間に使用)
        /// ・無敵状態のときダメージを受けない
        /// </summary>
        /// <param name="invincible_flg">無敵フラグ</param>
        public void Invincible(bool invincible_flg)
        {
            invincible = invincible_flg;
        }

        /// <summary>
        /// 階層移動
        /// ・プレイヤーを上層階に移動させる
        /// ・階層に合わせ敵のレベルが上昇する
        /// </summary>
        public void UpStair()
        {
            var enemy_lv = PlayerStatus.GetInstance().GoNextFloor();
            foreach (var enemy in EnemyList)
            {
                enemy.Lv = enemy_lv;
                enemy.ValueChange(enemy.Lv);
            }
            foreach (var enemy in BossList)
            {
                enemy.Lv = enemy_lv;
                enemy.ValueChange(enemy.Lv);
            }
        }
    }
}