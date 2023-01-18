using UnityEngine;
using UnityEngine.UI;

namespace myObject
{
    public class CharacterHire : MonoBehaviour
    {
        /// <summary> 名前UI </summary>
        [SerializeField] Text Name = null;
        /// <summary> 画像UI </summary>
        [SerializeField] Image Graph = null;
        /// <summary> 価格UI </summary>
        [SerializeField] Text GoldNeeds = null;
        /// <summary> 強化前レベルUI </summary>
        [SerializeField] Text BeforeLv = null;
        /// <summary> 強化前攻撃力UI </summary>
        [SerializeField] Text BeforePower = null;
        /// <summary> 強化後レベルUI </summary>
        [SerializeField] Text AfterLv = null;
        /// <summary> 強化後攻撃力UI </summary>
        [SerializeField] Text AfterPower = null;
        /// <summary> キャラクターデータ </summary>
        Character Character;

        /// <summary>
        /// 初期化
        /// ・UIパラメータ全設定
        /// </summary>
        /// <param name="character">キャラクターデータ</param>
        public void Initialize(Character character)
        {
            Character = character;
            Graph.sprite = character.Graph;
            ValueChange(character.Name, character.Level, character.Power, character.futurePower, character.Price);
        }

        /// <summary>
        /// UIパラメータ更新
        /// </summary>
        /// <param name="name">キャラ名</param>
        /// <param name="lv">キャラレベル</param>
        /// <param name="pow">キャラ攻撃力</param>
        /// <param name="f_pow">キャラ攻撃力(+値上昇後)</param>
        /// <param name="gold">消費金額</param>
        public void ValueChange(string name, int lv, float pow, float f_pow, int gold)
        {
            Name.text = name;
            BeforeLv.text = "現在レベル:" + lv.ToString();
            BeforePower.text = "現在攻撃力:" + pow.ToString();
            AfterLv.text = "次レベル:" + (lv + 1).ToString();
            AfterPower.text = "強化後攻撃:" + f_pow.ToString();
            GoldNeeds.text = "強化費用:" + gold.ToString();
        }

        /// <summary>
        /// ボタンクリック時動作
        /// ・キャラクターを雇用する(費用と引き換えに+値増加)
        /// </summary>
        public void OnClick()
        {
            if (Character.Price <= PlayerStatus.GetInstance().PlayerInfo().Money)
            {
                PlayerStatus.GetInstance().PlayerInfo().Money -= Character.Price;
                Character.Hire();
            }
        }
    }
}