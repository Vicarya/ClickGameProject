using UnityEngine;

namespace myObject
{
    [CreateAssetMenu(fileName = "ScriptableObject", menuName = "Weapon", order = 2)]
    public class Weapon : Item
    {
        WeaponPerchase weapon;
        public float futurePower;

        //初期化
        public void Initialize(GameObject button)
        {
            Initialize();
            weapon = button.GetComponent<WeaponPerchase>();
            weapon.Initialize(this);
        }
        void Initialize()
        {
            Price = (Count == 0) ? basePrice : Price;
            Power = Count * basePower;
            futurePower = basePower * (Count + 1);
        }

        //値の変更
        public void ValueChange()
        {
            ++Count;
            Price *= 2;
            Power = basePower * Count;
            futurePower = basePower * (Count+1);
            weapon.ValueChange(Count, Price, Power, futurePower);
        }

        //セーブデータ削除
        public void Reset()
        {
            Count = 0;
            Price = 0;
            Power = 0;
            Initialize();
        }

        //攻撃力の加算(プレイヤーの攻撃力＝武器攻撃力合計＋１)
        public float Attack(int enemySpeed) => Power*Count;

    }
}
