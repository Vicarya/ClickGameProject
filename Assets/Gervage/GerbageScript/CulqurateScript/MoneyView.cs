using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyView : MonoBehaviour
{
    [SerializeField]
    private Text goldText;
    public static int haveMoney;
    private ItemButton _itemButton;

    void Start()
    {
        //haveMoney = 0;
    }

    void Update()
    {
        goldText.text = "Wallet: " + haveMoney + "Gold";
    }
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
            haveMoney = PlayerPrefs.GetInt("Gold", haveMoney);
        else
        {
            PlayerPrefs.SetInt("Gold", haveMoney);
            SaveScript.delete();
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Gold", haveMoney);
        SaveScript.delete();
    }

}
