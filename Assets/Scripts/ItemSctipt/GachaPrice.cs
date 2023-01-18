using UnityEngine;

namespace myObject
{
    [CreateAssetMenu(fileName = "ScriptableObject", menuName = "GachaPrice", order = 5)]
    public class GachaPrice : ScriptableObject
    {
        /// <summary> スーパーガチャ価格 </summary>
        public int superGachaPrice = 5000;
        /// <summary> ノーマルガチャ価格 </summary>
        public int charaGachaPrice = 1000;
        /// <summary> アイテムガチャ価格 </summary>
        public int weaponGachaPrice = 1000;
        /// <summary> スーパーガチャ価格 </summary>
        [SerializeField] int baseSuperGachaPrice = 5000;
        /// <summary> ノーマルガチャ価格 </summary>
        [SerializeField] int baseCharaGachaPrice = 1000;
        /// <summary> アイテムガチャ価格 </summary>
        [SerializeField] int baseWeaponGachaPrice = 1000;

        /// <summary>
        /// パラメータ初期化
        /// </summary>
        public void Reset()
        {
            superGachaPrice = baseSuperGachaPrice;
            charaGachaPrice = baseCharaGachaPrice;
            weaponGachaPrice = baseWeaponGachaPrice;
        }
    }
}