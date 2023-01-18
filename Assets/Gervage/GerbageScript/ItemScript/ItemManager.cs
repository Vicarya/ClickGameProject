using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Click click;
    public UnityEngine.UI.Text itemInfo;
    public float cost;
    public int count = 0;
    public int clickPower;
    public string itemName;
    private float baseCost;

    private void Start()
    {
        baseCost = cost;
    }

    void Update()
    {
        itemInfo.text = itemName + "\nCost: " + cost + "\nPower: " + clickPower;
    }

    //プレイヤー攻撃(クリック)強化 [武器購入]
    //public void ItemUpgrade()
    //{
    //    if (click.gold >= (int)cost)
    //    {
    //        click.gold -= (int)cost;
    //        count += 1;
    //        click.goldPerClick += clickPower;
    //        cost = Mathf.Round(baseCost * Mathf.Pow(1.15f, count));
    //    }
    //}
}
