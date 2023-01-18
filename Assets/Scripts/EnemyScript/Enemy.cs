using UnityEngine;

namespace myObject
{
    [CreateAssetMenu(fileName = "ScriptableObject", menuName = "Enemy", order = 3)]
    public class Enemy : ScriptableObject
    {
        /// <summary> レベル </summary>
        public int Lv;
        /// <summary> 基本(1Lv)HP </summary>
        [SerializeField] float BaseHp;
        /// <summary> 現在HP </summary>
        public float Hp;
        /// <summary> 最大HP </summary>
        public float MaxHp;
        /// <summary> 画像 </summary>
        public Sprite Graph;
        /// <summary> 素早さ </summary>
        public int Speed;
        /// <summary> 基本(1Lv)獲得金額 </summary>
        [SerializeField] int BaseDropGold;
        /// <summary> 獲得金額 </summary>
        public int DropGold;
        /// <summary> 基本(1Lv)獲得経験 </summary>
        [SerializeField] int BaseDropExp;
        /// <summary> 獲得経験 </summary>
        public int DropExp;

        /// <summary>
        /// 値変更
        /// ・変更された敵レベルに応じてパラメータ変更
        /// </summary>
        /// <param name="lv">敵レベル</param>
        public void ValueChange(int lv)
        {
            Lv = lv;
            if (lv == 1) {
                MaxHp = BaseHp;
                DropGold = BaseDropGold;
                DropExp = BaseDropExp;
            }
            MaxHp *= 2;
            DropGold *= 2;
            DropExp *= 2;
        }
    }
}