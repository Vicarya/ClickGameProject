using UnityEngine;

namespace myObject
{
    //[CreateAssetMenu(fileName = "ScriptableObject", menuName = "Item", order = 0)]
    public class Item : ScriptableObject
    {
        /// <summary> 名前 </summary>
        public string Name;
        /// <summary> 画像 </summary>
        public Sprite Graph;
        /// <summary> 基本価格 </summary>
        [SerializeField] protected int basePrice;
        /// <summary> 購入価格 </summary>
        public int Price;
        /// <summary> 個数 </summary>
        public int Count;
        /// <summary> 基本攻撃力 </summary>
        [SerializeField] protected float basePower;
        /// <summary> パワー </summary>
        public float Power;
    }
}