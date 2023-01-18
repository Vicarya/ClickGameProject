using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class Character : MonoBehaviour
{
    
    //キャラクター画像管理用
    [SerializeField]
    protected Image _characterImage;
    [SerializeField]
    protected Sprite _characterSprite;
    public Sprite CharacterSprite => _characterSprite;

    ///キャラクターパワー管理用
    //public Text _characterPower;
    //public int CharacterPower;

    //キャラクター名管理用
    [SerializeField]
    protected Text _characterNameText;
    public string CharacterName;
    //キャラクター枚数管理用
    protected int CharacterCount = 0;

    //キャラクターレベル管理用
    [SerializeField]
    protected Text _characterLvText;
    public int CharacterLv;
    
    //キャラクター購入管理用(plus)
    [SerializeField]
    protected Text _characterPlusPriceText;
    [SerializeField]
    protected int CharacterPlusPrice;

    //キャラクター購入管理用(level)
    [SerializeField]
    protected Text _characterGrowExpText;
    [SerializeField]
    protected int CharacterGrowExp;
    virtual protected void Start()
    {
        CharacterManager._characterName[NameChanger(tag)] = CharacterName;
        CharacterManager._characterLv[NameChanger(tag)] = CharacterLv;
        CharacterManager._characterCount[NameChanger(tag)] = CharacterCount;
        CharacterManager._characterPlusPrice[NameChanger(tag)] = CharacterPlusPrice;
        CharacterManager._characterGrowExp[NameChanger(tag)] = CharacterGrowExp;
        _characterNameText.text = CharacterName;
        _characterLvText.text = $"Lv.{CharacterLv}";
        _characterPlusPriceText.text = $"{CharacterPlusPrice} Gold";
        _characterGrowExpText.text = $"{CharacterGrowExp} exp";
        _characterImage.sprite = CharacterSprite;
    }

    virtual protected void Update()
    {
        CharacterName = CharacterManager._characterName[NameChanger(tag)];
        CharacterLv = CharacterManager._characterLv[NameChanger(tag)];
        CharacterCount = CharacterManager._characterCount[NameChanger(tag)];
        CharacterPlusPrice = CharacterManager._characterPlusPrice[NameChanger(tag)];
        CharacterGrowExp = CharacterManager._characterGrowExp[NameChanger(tag)];

        if (CharacterManager._characterCount[NameChanger(tag)] <= 1)
            _characterNameText.text = $"{CharacterManager._characterName[NameChanger(tag)]}";
        else _characterNameText.text = $"{CharacterManager._characterName[NameChanger(tag)]} " +
                $"+{CharacterManager._characterCount[NameChanger(tag)] - 1}";
        _characterLvText.text = $"Lv.{CharacterManager._characterLv[NameChanger(tag)]}";
    }
    virtual protected int NameChanger(string charName)
    {
        switch (charName)
        {
            case "huto":
                return 0;// (int)CharacterNum.huto;
            case "koishi":
                return 1;// (int)CharacterNum.koishi;
            case "marisa":
                return 2;// (int)CharacterNum.marisa;
            case "nue":
                return 3;// (int)CharacterNum.nue;
            case "pachry":
                return 4;// (int)CharacterNum.pachry;
            case "reimu":
                return 5;// (int)CharacterNum.reimu;
            case "remilia":
                return 6;// (int)CharacterNum.remilia;
            case "sanae":
                return 7;// (int)CharacterNum.sanae;
            default:
                return 0;// -1;
        }
    }
    //ファイル操作
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            CharacterCount = PlayerPrefs.GetInt(CharacterName + $"Count", CharacterCount);
            CharacterName = PlayerPrefs.GetString(CharacterName + $"Name", CharacterName);
            CharacterLv = PlayerPrefs.GetInt(CharacterName + $"Lv", CharacterLv);
            CharacterPlusPrice = PlayerPrefs.GetInt(CharacterName + $"Price", CharacterPlusPrice);
            CharacterGrowExp = PlayerPrefs.GetInt(CharacterName + $"Exp", CharacterGrowExp);
            CharacterManager._characterPower[NameChanger(tag)] = PlayerPrefs.GetFloat(CharacterName + $"Pow", CharacterManager._characterPower[NameChanger(tag)]);
        }
        else
        {
            PlayerPrefs.SetInt(CharacterName + $"Count", CharacterManager._characterCount[NameChanger(tag)]);
            PlayerPrefs.SetString(CharacterName + $"Name", CharacterManager._characterName[NameChanger(tag)]);
            PlayerPrefs.SetInt(CharacterName + $"Lv", CharacterManager._characterLv[NameChanger(tag)]);
            PlayerPrefs.SetInt(CharacterName + $"Price", CharacterManager._characterPlusPrice[NameChanger(tag)]);
            PlayerPrefs.SetInt(CharacterName + $"Exp", CharacterManager._characterGrowExp[NameChanger(tag)]);
            PlayerPrefs.SetFloat(CharacterName + $"Pow", CharacterManager._characterPower[NameChanger(tag)]);
            SaveScript.delete();
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt(CharacterName + $"Count", CharacterManager._characterCount[NameChanger(tag)]);
        PlayerPrefs.SetString(CharacterName + $"Name", CharacterManager._characterName[NameChanger(tag)]);
        PlayerPrefs.SetInt(CharacterName + $"Lv", CharacterManager._characterLv[NameChanger(tag)]);
        PlayerPrefs.SetInt(CharacterName + $"Price", CharacterManager._characterPlusPrice[NameChanger(tag)]);
        PlayerPrefs.SetInt(CharacterName + $"Exp", CharacterManager._characterGrowExp[NameChanger(tag)]);
        PlayerPrefs.SetFloat(CharacterName + $"Pow", CharacterManager._characterPower[NameChanger(tag)]);
        SaveScript.delete();
    }
}
