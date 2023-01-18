using UnityEngine;
using UnityEngine.UI;

public class ItemButton : Item
{
    [SerializeField]
    float item_per_up;  //アイテムインフレ割合
    
    public Text clickPow;

    private void Start()
    {
        UpdateValue();
    }
    public void OnClick()
    {
        if (MoneyView.haveMoney >= _itemPrice)
            _itemPrice = Consume(_itemPrice);
        UpdateValue();
    }
    //アイテム購入所持管理
    private int Consume(int value_)
    {
        MoneyView.haveMoney -= value_;
        value_ = (int)(value_*item_per_up);
        ++itemCount;
        Click.damagePerClick += 1;  //Money手動強化(仮値:1)
        clickPow.text = $"Pow:{Click.damagePerClick}";
        return value_;
    }
    private void UpdateValue()
    {
        _itemPriceText.text = $"{_itemPrice} Gold";
        NameChange();
    }
    private void NameChange()
    {
        if (gameObject.name == ItemName)
        {
            if (itemCount >= 1) _itemNameText.text = ItemName+$"×{itemCount}";
        }
    }
    //ファイル操作
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            _itemName = PlayerPrefs.GetString(ItemName + "Name", ItemName);
            _itemPrice = PlayerPrefs.GetInt(ItemName + "Price", ItemPrice);
            itemCount = PlayerPrefs.GetInt(ItemName + "Count", itemCount);
        }
        else
        {
            PlayerPrefs.SetString(ItemName + "Name", _itemName);
            PlayerPrefs.SetInt(ItemName + "Price", _itemPrice);
            PlayerPrefs.SetInt(ItemName + "Count", itemCount);
            SaveScript.delete();
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString(ItemName + "Name", _itemName);
        PlayerPrefs.SetInt(ItemName + "Price", _itemPrice);
        PlayerPrefs.SetInt(ItemName + "Count", itemCount);
        SaveScript.delete();
    }
}
