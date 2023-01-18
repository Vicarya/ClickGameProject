using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpCharger : MonoBehaviour
{
    [SerializeField]
    private Text expText;
    public static int chargedExp;
    private ItemButton _itemButton;

    void Start()
    {
        //chargedExp = 0;

        //var game = GameObject.Find(tag = "Item");
        //_itemButton = game.GetComponent<ItemButton>();

    }

    void Update()
    {
        expText.text = "EXP: " + chargedExp + "exp";
    }
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
            chargedExp = PlayerPrefs.GetInt("Exp", chargedExp);
        else
        {
            PlayerPrefs.SetInt("Exp", chargedExp);
            SaveScript.delete();
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Exp", chargedExp);
        SaveScript.delete();
    }
}
