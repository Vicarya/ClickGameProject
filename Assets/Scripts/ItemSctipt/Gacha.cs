using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace myObject
{
    public class Gacha : MonoBehaviour
    {
        [SerializeField] GachaPrice gachaPrice;
        /// <summary>
        /// クリック時の仕様
        /// ・ボタン名を取得して対応するガチャ実行
        /// </summary>
        /// <param name="name">ボタン名</param>
        public void OnClick(string name)
        {
            if (name == "WeaponGacha")
            {
                WeaponGacha();
            }
            else if (name == "CharaGacha")
            {
                CharaGacha();
            }
            else if (name == "SuperGacha")
            {
                SuperCharGacha();
            }
            else
            {
                Debug.Log("Miss");
            }

        }

        /// <summary> スーパーガチャ価格UI </summary>
        [SerializeField] Text SuperGachaPrice = null;
        /// <summary> スーパーガチャインフレ率 </summary>
        [SerializeField] float superGachaPer = 2f;
        /// <summary>
        /// スーパーガチャを実行する
        /// ・基本雇用価格をガチャ価格が下回っている場合のみ実行
        /// </summary>
        private void SuperCharGacha()
        {
            var characterManager = CharacterManager.GetInstance();
            var playerStatus = PlayerStatus.GetInstance();

            Debug.Log("SuperCharGacha");
            if (playerStatus.PlayerInfo().Money < gachaPrice.superGachaPrice)
            {
                Debug.Log("NoChance");return;
            }
            //キャラのインデクスを一度リストに格納
            List<int> vs = new List<int>();
            while(vs.Count < characterManager.ListMax())
            {
                vs.Add(vs.Count);
            }
            while (vs.Count>0)
            {
                int randIndex = UnityEngine.Random.Range(0, vs.Count);
                //お得成分あるか選別(得でなければリストから削除)
                //同時に得なら入手が動く
                bool getFlg = characterManager.CharaGet(randIndex, gachaPrice.superGachaPrice);
                if (!getFlg)
                {
                    vs.RemoveAt(randIndex);
                    continue;//得な条件になるまで繰り返し
                }
                //購入処理と価格インフレ
                playerStatus.HireCharacter(gachaPrice.superGachaPrice);
                gachaPrice.superGachaPrice = (int)(gachaPrice.superGachaPrice * superGachaPer);
                SuperGachaPrice.text = $"SuperGacha\nPrice:{gachaPrice.superGachaPrice}";
                break;//支払処理後ループを抜ける
            }
        }

        /// <summary> ノーマルガチャ価格UI </summary>
        [SerializeField] Text CharaGachaPrice = null;
        /// <summary> ノーマルガチャインフレ率 </summary>
        [SerializeField] float charaGachaPer = 1.2f;
        /// <summary>
        /// ノーマルガチャを実行
        /// ・特に指定条件なしで実行
        /// </summary>
        private void CharaGacha()
        {
            var characterManager = CharacterManager.GetInstance();
            var playerStatus = PlayerStatus.GetInstance();
            Debug.Log("CharGacha");
            if (playerStatus.PlayerInfo().Money < gachaPrice.charaGachaPrice)
            {
                Debug.Log("NoChance"); return;
            }
            int randIndex = UnityEngine.Random.Range(0, characterManager.ListMax());
            if (!characterManager.CharaGet(randIndex, gachaPrice.charaGachaPrice, true)) return;
            //購入処理と価格インフレ
            playerStatus.HireCharacter(gachaPrice.charaGachaPrice);
            gachaPrice.charaGachaPrice = (int)(gachaPrice.charaGachaPrice * charaGachaPer);
            CharaGachaPrice.text = $"CharaGacha\nPrice:{gachaPrice.charaGachaPrice}";
        }

        /// <summary> 武器ガチャ価格UI </summary>
        [SerializeField] Text WeaponGachaPrice = null;
        /// <summary> 武器ガチャインフレ率 </summary>
        [SerializeField] float itemGachaPer = 1.5f;
        /// <summary>
        /// 武器ガチャを実行
        /// ・特に指定条件なしで実行
        /// </summary>
        private void WeaponGacha()
        {
            var weaponManager = WeaponManager.GetInstance();
            var playerStatus = PlayerStatus.GetInstance();
            Debug.Log("CharGacha");
            if (playerStatus.PlayerInfo().Money < gachaPrice.weaponGachaPrice)
            {
                Debug.Log("NoChance"); return;
            }
            int randIndex = UnityEngine.Random.Range(0, weaponManager.ListMax());
            weaponManager.GetWeapon(randIndex);
            //購入処理と価格インフレ
            playerStatus.BuyWeapon(gachaPrice.weaponGachaPrice);
            gachaPrice.weaponGachaPrice = (int)(gachaPrice.weaponGachaPrice * itemGachaPer);
            WeaponGachaPrice.text = $"ItemGacha\nPrice:{gachaPrice.weaponGachaPrice}";
        }

        /// <summary>
        /// UIに初期値を挿入
        /// </summary>
        private void Start()
        {
            SuperGachaPrice.text = $"SuperGacha\nPrice:{gachaPrice.superGachaPrice}";
            CharaGachaPrice.text = $"CharaGacha\nPrice:{gachaPrice.charaGachaPrice}";
            WeaponGachaPrice.text = $"ItemGacha\nPrice:{gachaPrice.weaponGachaPrice}";
        }
    }
}