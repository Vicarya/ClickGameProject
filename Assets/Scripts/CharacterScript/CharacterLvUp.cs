using UnityEngine;
using UnityEngine.UI;

namespace myObject
{
    public class CharacterLvUp : MonoBehaviour
    {
        /// <summary> 名前UI </summary>
        [SerializeField] Text Name;
        /// <summary> 画像UI </summary>
        [SerializeField] Image Graph;
        /// <summary> 経験値UI </summary>
        [SerializeField] Text ExpNeeds;
        /// <summary> 強化前LvUI </summary>
        [SerializeField] Text BeforeLv;
        /// <summary> 強化前攻撃UI </summary>
        [SerializeField] Text BeforePower;
        /// <summary> 強化前素早さ </summary>
        [SerializeField] Text BeforeSpeed;
        /// <summary> 強化後LvUI </summary>
        [SerializeField] Text AfterLv;
        /// <summary> 強化後攻撃UI </summary>
        [SerializeField] Text AfterPower;
        /// <summary> 強化後素早さUI </summary>
        [SerializeField] Text AfterSpeed;
        /// <summary> キャラクターデータ </summary>
        Character Character;

        /// <summary>
        /// 初期化
        /// ・名前, 画像, レベル, 攻撃(上昇前), 攻撃(上昇後), 速度(上昇前), 速度(上昇後), 必要経験値
        /// </summary>
        /// <param name="character">キャラクターデータ</param>
        public void Initialize(Character character) {
            Character = character;
            Graph.sprite = Character.Graph;
            ValueChange(Character.Name, Character.Level, Character.Power, Character.futurePower, Character.Speed, Character.futureSpeed, Character.GrawExp);
        }

        /// <summary>
        /// UIパラメータ変更
        /// </summary>
        /// <param name="name">キャラ名前</param>
        /// <param name="lv">キャラレベル</param>
        /// <param name="b_pow">強化前攻撃力</param>
        /// <param name="a_pow">強化後攻撃力</param>
        /// <param name="b_sp">強化前素早さ</param>
        /// <param name="a_sp">強化後素早さ</param>
        /// <param name="exp">経験値</param>
        public void ValueChange(string name, int lv, float b_pow, float a_pow, int b_sp, int a_sp, int exp)
        {
            Name.text = name;
            BeforeLv.text = "現在レベル:" + lv.ToString();
            BeforePower.text = "現在攻撃力:" + b_pow.ToString();
            BeforeSpeed.text = "現在攻速:" + b_sp.ToString();
            AfterLv.text = "次レベル:" + (lv + 1).ToString();
            AfterPower.text = "強化後攻撃:" + a_pow.ToString();
            AfterSpeed.text = "強化後攻速:" + a_sp.ToString();
            ExpNeeds.text = "必要経験値:" + exp;
        }

        /// <summary>
        /// クリック時実行
        /// ・レベルアップの処理をする
        /// </summary>
        public void OnClick()
        {
            if (Character.GrawExp <= PlayerStatus.GetInstance().PlayerInfo().Exp)
            {
                PlayerStatus.GetInstance().PlayerInfo().Exp -= Character.GrawExp;
                Character.LvUp();
            }
        }
    }
}