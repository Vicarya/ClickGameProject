using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    //名前
    [SerializeField]
    protected Text _itemNameText;
    [SerializeField]
    protected string _itemName;
    public string ItemName => _itemName;

    //価格
    [SerializeField]
    protected Text _itemPriceText;
    [SerializeField]
    protected int _itemPrice;
    public int ItemPrice => _itemPrice;

    //画像
    [SerializeField]
    protected Image _itemImage;
    [SerializeField]
    protected Sprite _itemSprite;
    public Sprite ItemSprite => _itemSprite;

    //個数
    [SerializeField]
    protected int itemCount = 0;

    void Awake()
    {
        _itemNameText.text = ItemName;
        _itemPriceText.text = $"{ItemPrice}Gold";
        _itemImage.sprite = ItemSprite;
    }

}
