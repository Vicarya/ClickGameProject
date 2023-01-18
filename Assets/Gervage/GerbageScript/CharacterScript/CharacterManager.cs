using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    //キャラクター画像管理用
    [SerializeField]
    private Image[] _characterImage;
    int pause_num;
    const int PAUSE_MAX = 3;
    private Sprite[] CharacterSprite;// => _characterSprite1;

    //キャラクターパワー管理用
    [SerializeField]
    public Text[] _characterPowerText;
    public float[] CharacterPower => _characterPower;

    //キャラクター名管理用
    [SerializeField]
    protected Text[] _characterNameText;
    public string[] CharacterName => _characterName;
    //キャラクター枚数管理用
    protected int[] CharacterCount => _characterCount;

    //キャラクターレベル管理用
    [SerializeField]
    protected Text[] _characterLvText;
    public int[] CharacterLv => _characterLv;

    //キャラ一体一体に付加する方法もあるが、設定面倒なので一括
    public static float char_per_up = 2f;   //キャラクターの購入割合上昇(gold, exp共用)

    //キャラクター画像管理用
    [SerializeField]
    private Sprite[] _characterSprite1;  //キャラの動き含め同じキャラの画像複数必要なため、二次元配列で用意したい…。
    [SerializeField]
    private Sprite[] _characterSprite2;  //キャラの動き含め同じキャラの画像複数必要なため、二次元配列で用意したい…。
    [SerializeField]
    private Sprite[] _characterSprite3;  //キャラの動き含め同じキャラの画像複数必要なため、二次元配列で用意したい…。
    //キャラクターパワー管理用
    public static float[] _characterPower = new float[(int)CharacterNum.nameless];
    //キャラクター枚数管理用
    public static int[] _characterCount = new int[(int)CharacterNum.nameless];
    //キャラクター名管理用
    public static string[] _characterName = new string[(int)CharacterNum.nameless];
    //キャラクターレベル管理用
    public static int[] _characterLv = new int[(int)CharacterNum.nameless];
    //キャラクター購入管理用(gold)
    public static int[] _characterPlusPrice = new int[(int)CharacterNum.nameless];
    //キャラクター購入管理用(exp)
    public static int[] _characterGrowExp = new int[(int)CharacterNum.nameless];
    public enum CharacterNum
    {
        huto,
        koishi,
        marisa,
        nue,
        pachry,
        reimu,
        remilia,
        sanae,
        nameless
    };
    private void Start()
    {
        StartCoroutine(DamagePerSec());
        StartCoroutine(CharacterMotion());
    }

    public bool[] _setActFlg = { false };
    public GameObject[] _powupButton;
    public GameObject[] _characterDetail;

    public static float characterPowerSum = 0;

    private void Update()
    {
        float _characterPowerSum = 0;
        for (int i = 0; i < (int)CharacterNum.nameless; ++i)
        {
            _characterPowerSum += _characterPower[i];
            if (_characterCount[i] >= 1)
            {
                _setActFlg[i] = true;
            }
            _powupButton[i].SetActive(_setActFlg[i]);
            _characterDetail[i].SetActive(_setActFlg[i]);
            //テキスト更新
            if (_setActFlg[i])
            {
                if(CharacterCount[i]<=1) _characterNameText[i].text = $"Name : " + CharacterName[i];
                else _characterNameText[i].text = $"Name : " + (CharacterName[i] + (CharacterCount[i] - 1));
                _characterLvText[i].text = $"Lv : " + CharacterLv[i];
                _characterPowerText[i].text = $"ATK : " + CharacterPower[i];
                _characterImage[i].sprite = CharacterSprite[i];
            }
        }
        characterPowerSum = _characterPowerSum;

    }
    //攻撃力計算
    public static float CharacterPowerSum()
    {
        float dps = 0;
        for (int i = 0; i < (int)CharacterNum.nameless; i++)
        {
            dps += _characterPower[i];
        }
        return dps;
    }
    //ダメージ計算
    IEnumerator DamagePerSec()
    {
        while (true)
        {
            if (EnemySpaun.enemyHp > 0) EnemySpaun.enemyHp -= CharacterPowerSum();  //Money自動加算(仮値:1)
            yield return new WaitForSeconds(0.1f);
        }
    }
    //アニメーション
    IEnumerator CharacterMotion()
    {
        while (true)
        {
            pause_num++;
            if (1 == pause_num) CharacterSprite = _characterSprite1;
            else if (2 == pause_num) CharacterSprite = _characterSprite2;
            else if (3 == pause_num) CharacterSprite = _characterSprite3;
            if (pause_num >= PAUSE_MAX) pause_num = 0;
            yield return new WaitForSeconds(0.2f);
        }
    }

    ////ファイル操作
    //private void OnApplicationFocus(bool focus)
    //{
    //    if(focus)
    //        for (int i = 0; i < (int)CharacterNum.nameless; ++i)
    //        {
    //            _characterPower[i] = PlayerPrefs.GetFloat(CharacterName[i] + $"Pow{i}", CharacterPower[i]);
    //        }
    //    else
    //        for (int i = 0; i < (int)CharacterNum.nameless; ++i)
    //        {
    //            PlayerPrefs.SetFloat(CharacterName[i] + $"Pow{i}", CharacterPower[i]);
    //        }
    //}
}
