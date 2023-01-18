using UnityEngine;
using UnityEngine.UI;

namespace myObject
{
    public class CharacterStatus : MonoBehaviour
    {
        /// <summary> 名前UI </summary>
        [SerializeField] Text Name;
        /// <summary> 画像UI </summary>
        [SerializeField] Image Graph;
        /// <summary> レベルUI </summary>
        [SerializeField] Text Lv;
        /// <summary> 攻撃力UI </summary>
        [SerializeField] Text Power;
        /// <summary> 速度UI </summary>
        [SerializeField] Text speedText;
        /// <summary> キャラクターステータス </summary>
        Character Character;

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="character">キャラクターデータ</param>
        public void Initialize(Character character)
        {
            Character = character;
            Graph.sprite = Character.LargeGraphs[0];
            ValueChange(Character.Name, Character.Count, Character.Level, Character.Power, Character.Speed);
        }

        /// <summary>
        /// UI値更新
        /// </summary>
        /// <param name="name">名前</param>
        /// <param name="count">+値</param>
        /// <param name="lv">レベル</param>
        /// <param name="pow">攻撃力</param>
        /// <param name="speed">素早さ</param>
        public void ValueChange(string name, int count, int lv, float pow, int speed)
        {
            Name.text = "名前：" + ((count ==0) ? name : name + "+" + count);
            Lv.text = "Lv：" + lv;
            Power.text = "攻撃：" + pow;
            speedText.text = "素早さ：" + speed;
        }

        /// <summary> 画像リストインデクス </summary>
        int index = 0;
        /// <summary>
        /// キャラクターの画像をgifする
        /// </summary>
        private void Update()
        {
            if (Character.LargeGraphs != null)
            {
                Graph.sprite = Character.LargeGraphs[index];
                ++index;
                if (index >= Character.LargeGraphs.Length) index = 0;
            }
        }
    }
}