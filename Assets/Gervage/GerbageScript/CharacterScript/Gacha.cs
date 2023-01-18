using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static CharacterManager;

public class Gacha : MonoBehaviour
{
    private void Start()
    {
        SuperGachaPrice.text = $"SuperGacha\nPrice:{superGachaPrice}";
        CharaGachaPrice.text = $"CharaGacha\nPrice:{charaGachaPrice}";
        ItemGachaPrice.text = $"ItemGacha\nPrice:{itemGachaPrice}";
    }
    //**clickStart***************************************************************
    public void OnClick(string name)
    {
        if (name == "ItemGacha")
        {
            ItemGacha();
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
    //**superGacha***************************************************************
    [SerializeField]
    Text SuperGachaPrice;
    [SerializeField]
    int superGachaPrice = 5000;
    [SerializeField]
    float superGachaPer = 2f;
    private void SuperCharGacha()
    {
        Debug.Log("SuperCharGacha");
        bool flg = false;
        if (MoneyView.haveMoney >= superGachaPrice)
            for (int i = 0; i < (int)CharacterNum.nameless; ++i)
            {
                //お得成分あるか選別
                if (CharacterManager._characterPlusPrice[i] < superGachaPrice) continue;
                flg = true;
                //得な条件になるまで繰り返し
                int rand;
                do {
                    rand = UnityEngine.Random.Range(0, (int)CharacterNum.nameless);
                } while (CharacterManager._characterPlusPrice[rand] < superGachaPrice);
                //if (キャラクターの追加価格 >= このガチャの価格) 当てはまったら実行
                CharRandomSpaun(rand);
                MoneyView.haveMoney -= superGachaPrice;
                superGachaPrice = (int)(superGachaPrice * superGachaPer);
                SuperGachaPrice.text = $"SuperGacha\nPrice:{superGachaPrice}";
                break;//支払処理後for文を抜ける
            }
        if (flg == false) Debug.Log("NoChance");
    }
    //**charGacha*****************************************************************
    [SerializeField]
    Text CharaGachaPrice;
    [SerializeField]
    int charaGachaPrice = 1000;
    [SerializeField]
    float charaGachaPer = 1.2f;
    private void CharaGacha()
    {
        Debug.Log("CharaGacha");
        if (MoneyView.haveMoney >= charaGachaPrice)
        {
            CharRandomSpaun(UnityEngine.Random.Range(0, 8));
            MoneyView.haveMoney -= charaGachaPrice;
            charaGachaPrice = (int)(charaGachaPrice * charaGachaPer);
            CharaGachaPrice.text = $"CharaGacha\nPrice:{charaGachaPrice}";
        }
    } 
    //**itemGacha*****************************************************************
    [SerializeField]
    Text ItemGachaPrice;
    [SerializeField]
    int itemGachaPrice = 1000;
    [SerializeField]
    float itemGachaPer = 1.5f;
    private void ItemGacha()
    {
        Debug.Log("ItemGacha");
        if(MoneyView.haveMoney >= itemGachaPrice)
        ItemRandomSpaun(UnityEngine.Random.Range(0,8));
        MoneyView.haveMoney -= itemGachaPrice;
        itemGachaPrice = (int)(itemGachaPrice * itemGachaPer);
        ItemGachaPrice.text = $"ItemGacha\nPrice:{itemGachaPrice}";
    }
    //**charaRondom***************************************************************
    private void CharRandomSpaun(int charRandom)
    {
        switch (charRandom)
        {
            case 0:
                CharacterManager._characterCount[0] += 1;
                break;
            case 1:
                CharacterManager._characterCount[1] += 1;
                break;
            case 2:
                CharacterManager._characterCount[2] += 1;
                break;
            case 3:
                CharacterManager._characterCount[3] += 1;
                break;
            case 4:
                CharacterManager._characterCount[4] += 1;
                break;
            case 5:
                CharacterManager._characterCount[5] += 1;
                break;
            case 6:
                CharacterManager._characterCount[6] += 1;
                break;
            case 7:
                CharacterManager._characterCount[7] += 1;
                break;
        }
    }
    //**itemRandom***************************************************************
    private void ItemRandomSpaun(int itemRandom)
    {
        var obj = GameObject.FindWithTag("Item");
        if (itemRandom == 0)
        {
            if(obj.name == "Sword")
            {
                obj.GetComponent<Button.ButtonClickedEvent>();
            }
        }
        else if (itemRandom == 2)
        {
            //Item.ItemHas[2/*itemNum*/].count++;
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            itemGachaPrice = PlayerPrefs.GetInt("ItemGachaPrice", itemGachaPrice);
            charaGachaPrice = PlayerPrefs.GetInt("CharaGachaPrice", charaGachaPrice);
            superGachaPrice = PlayerPrefs.GetInt("SuperGachaPrice", superGachaPrice);
        }
        else
        {
            PlayerPrefs.SetInt("ItemGachaPrice", itemGachaPrice);
            PlayerPrefs.SetInt("CharaGachaPrice", charaGachaPrice);
            PlayerPrefs.SetInt("SuperGachaPrice", superGachaPrice);
        }
    }
}
