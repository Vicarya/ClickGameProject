using UnityEngine;
using UnityEngine.UI;

namespace myObject
{
    public class WeaponPerchase : MonoBehaviour
    {
        Text nameText; //武器名
        Image itemImage; //武器画像
        Text priceText; //価格
        Text powerText; //パワー
        Text futurePowerText; //強化後パワー
        Weapon weapon;

        public void Initialize(Weapon weapon_)
        {
            weapon = weapon_;
            nameText = gameObject.transform.Find("Name").GetComponent<Text>();
            itemImage = gameObject.transform.Find("Graph").GetComponent<Image>();
            priceText = gameObject.transform.Find("Price").GetComponent<Text>();
            powerText = gameObject.transform.Find("Power").GetComponent<Text>();
            futurePowerText = gameObject.transform.Find("FuturePower").GetComponent<Text>();
            ValueChange(weapon.Count, weapon.Price, weapon.Power, weapon.futurePower);
            itemImage.sprite = weapon.Graph;
        }

        //値の変更
        public void ValueChange(int count, int price, float power, float futurePower)
        {
            nameText.text = weapon.Name + ((count > 0) ? " x" + count : "");
            priceText.text = "購入価格" + price;
            powerText.text = "攻撃力：" + power;
            futurePowerText.text = "強化後：" + futurePower;
        }

        public void OnClick()
        {
            bool buyed = PlayerStatus.GetInstance().BuyWeapon(weapon.Price);
            if(buyed) weapon.ValueChange();
            PlayerStatus.GetInstance().PlayerUpdate();
        }
    }
}