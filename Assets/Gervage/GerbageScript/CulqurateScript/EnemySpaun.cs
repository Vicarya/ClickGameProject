using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpaun : MonoBehaviour
{
    //画像関係
    [SerializeField]
    GameObject EnemyObject;
    [SerializeField]
    private Image _enemy;
    private int _enemyLiveFlg = 0;//1なら通常敵、2ならボス
    [SerializeField]
    private Sprite[] EnemySprite;
    private int _enemySpriteNum;
    [SerializeField]
    private Sprite[] BossSprite;
    private int _bossSpriteNum = 0;
    float th = 0;

    //敵のHP設定(階層毎強化)
    public UnityEngine.UI.Text enemyHpDisplay;
    public int enemyHpBase = 10;
    public static float enemyHp;

    //撃破数関係
    [SerializeField]
    private int NEED_BEATED = 20; //必要撃破数
    int beatCounter; //敵の撃破数カウント
    public GameObject bossButton;
    public bool bossFlg = false; //階層移動挑戦時のボス出現時、自動生成挙動管理

    //階層関係
    public UnityEngine.UI.Text FloorCounter;
    [SerializeField]
    private int nowFloor = 1;
    private static bool upStairFlg = false;

    //報奨関係
    private int gainGold = 0;
    [SerializeField]
    private int GoldPerWin = 1;
    private int gainExp = 0;
    [SerializeField]
    private int ExpPerWin = 1;

    //制限時間関係
    public GameObject LimitCounter;
    public UnityEngine.UI.Text limitCounter;
    public const int TIMELIMIT = 10;
    [SerializeField]
    private int clock;
    public float deltaClock;

    //撃破表示(勝敗)
    public GameObject ResultController;
    public UnityEngine.UI.Text ResultText;

    // Start is called before the first frame update
    void Start()
    {
        ResultController.SetActive(false);
        EnemyObject.SetActive(false);
        bossButton.SetActive(false);
        enemyHp = enemyHpBase;
        gainGold = enemyHpBase * GoldPerWin;
        gainExp = enemyHpBase * ExpPerWin;
        clock = TIMELIMIT;
        beatCounter = 0;
        StartCoroutine(UpdateIE());
    }
    
    IEnumerator UpdateIE()
    {
        float sum = CharacterManager.CharacterPowerSum();
        MoneyView.haveMoney += (int)((login_time - logout_time).TotalSeconds * (sum*10))
            / enemyHpBase * gainGold;
        ExpCharger.chargedExp += (int)((login_time - logout_time).TotalSeconds * sum)
            / enemyHpBase * gainExp;
        while (true)
        {
            if (deltaClock > 0) { LimitCounter.SetActive(true); }
            else { LimitCounter.SetActive(false); }

            if (enemyHp < 0) enemyHp = 0;
            enemyHpDisplay.text = "HP: " + (int)enemyHp;

            if (upStairFlg)
            {
                enemyHpBase = enemyHpBase * nowFloor;
                gainGold = enemyHpBase * GoldPerWin;
                gainExp = enemyHpBase * ExpPerWin;
                upStairFlg = false;
            }
            FloorCounter.text = "Now Explooring\n" + nowFloor + " Floor";

            if (enemyHp != 0 && !bossFlg)
            {
                _enemyLiveFlg = 1;
            }
            if (enemyHp == 0 && !bossFlg)
            {
                ++_enemySpriteNum;
                if (_enemySpriteNum >= EnemySprite.Length) { _enemySpriteNum = 0; }
                _enemyLiveFlg = 0;
                MoneyView.haveMoney += gainGold;
                ExpCharger.chargedExp += gainExp;
                enemyHp = enemyHpBase;// new enemy->enemyHp;
                beatCounter++;
                if (beatCounter >= NEED_BEATED)
                {
                    ///bossSwitchOn
                    bossButton.SetActive(true);
                }
            }
            else if (enemyHp != 0 && bossFlg)
            {
                _enemyLiveFlg = 2;
                if (deltaClock > 0)
                {
                    if (clock <= 3) { limitCounter.color = UnityEngine.Color.red; }
                    else { limitCounter.color = UnityEngine.Color.white; }
                    limitCounter.text = "Last " + clock.ToString() + " Seconds !";
                }
                else
                {
                    /// Lose処理を入れる。
                    yield return Lose();
                    bossFlg = false;
                    beatCounter = 0;
                    enemyHp = enemyHpBase;
                    _enemyLiveFlg = 0;
                }
            }
            else if (enemyHp == 0 && bossFlg)
            {
                ++_bossSpriteNum;
                if (_bossSpriteNum >= BossSprite.Length) { _bossSpriteNum = 0; }
                _enemyLiveFlg = 0;
                MoneyView.haveMoney += (gainGold * 10);
                ExpCharger.chargedExp += (gainExp * 10);
                bossFlg = false;
                nowFloor++;
                upStairFlg = true;
                ///勝利エフェクト
                yield return Win();
                deltaClock = 0;
                beatCounter = 0;
            }

            if (_enemyLiveFlg == 1 && EnemyObject.activeSelf == false)
            {
                EnemyObject.SetActive(true);
                _enemy.sprite = EnemySprite[_enemySpriteNum];
                _enemy.rectTransform.sizeDelta = new Vector2(100, 100);
            }
            else if (_enemyLiveFlg == 0 && EnemyObject.activeSelf == true)
            {
                EnemyObject.SetActive(false);
            }
            else if (_enemyLiveFlg == 2)
            {
                EnemyObject.SetActive(true);
                _enemy.sprite = BossSprite[_bossSpriteNum];
                _enemy.rectTransform.sizeDelta = new Vector2(200, 200);
            }
            th += 0.02f;
            EnemyObject.transform.Translate(0, 0.5f*Mathf.Sin(th), 0);

            deltaClock -= Time.deltaTime;
            clock = (int)deltaClock;
            yield return null;
        }
    }
    public void OnClick()
    {
        bossFlg = true;
        enemyHp = enemyHpBase * 10;
        deltaClock = TIMELIMIT;
        bossButton.SetActive(false); //挑戦ボタンを消す動作。
    }

    IEnumerator Lose()
    {
        ResultController.SetActive(true);
        ResultText.color = Color.cyan;
        ResultText.text = "LOSE...\nYou Must Get More Power";
        yield return new WaitForSeconds(5);
        ResultController.SetActive(false);
    }
    IEnumerator Win()
    {
        ResultController.SetActive(true);
        ResultText.color = Color.yellow;
        ResultText.text = "WIN!!\n To Next Floor !";
        yield return new WaitForSeconds(2);
        ResultController.SetActive(false);
    }

    //オフライン計算
    DateTime logout_time;
    DateTime login_time;
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            _enemySpriteNum = PlayerPrefs.GetInt("EnemyNum", _enemySpriteNum);
            _bossSpriteNum = PlayerPrefs.GetInt("BossNum", _bossSpriteNum);
            enemyHp = PlayerPrefs.GetFloat("EnemyHp", enemyHp);
            beatCounter = PlayerPrefs.GetInt("BeatCounter", beatCounter);
            nowFloor = PlayerPrefs.GetInt("NowFloor", nowFloor);
            enemyHpBase = PlayerPrefs.GetInt("EnemyMaxHp", enemyHpBase);
            gainGold = PlayerPrefs.GetInt("GainGold", gainGold);
            gainExp = PlayerPrefs.GetInt("GainExp", gainExp);

            login_time = DateTime.UtcNow;
            string tmp = PlayerPrefs.GetString("LogoutTime", login_time.ToBinary().ToString());
            logout_time = System.DateTime.FromBinary(System.Convert.ToInt64(tmp));
            Debug.Log(logout_time);
            Debug.Log(login_time);
        }
        else
        {
            PlayerPrefs.SetInt("EnemyNum", _enemySpriteNum);
            PlayerPrefs.SetInt("BossNum", _bossSpriteNum);
            PlayerPrefs.SetFloat("EnemyHp", enemyHp);
            PlayerPrefs.SetInt("BeatCounter", beatCounter);
            PlayerPrefs.SetInt("NowFloor", nowFloor);
            PlayerPrefs.SetInt("EnemyMaxHp", enemyHpBase);
            PlayerPrefs.SetInt("GainGold", gainGold);
            PlayerPrefs.SetInt("GainExp", gainExp);

            logout_time = DateTime.UtcNow;
            PlayerPrefs.SetString("LogoutTime", logout_time.ToBinary().ToString());
            SaveScript.delete();
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("EnemyNum", _enemySpriteNum);
        PlayerPrefs.SetInt("BossNum", _bossSpriteNum);
        PlayerPrefs.SetFloat("EnemyHp", enemyHp);
        PlayerPrefs.SetInt("BeatCounter", beatCounter);
        PlayerPrefs.SetInt("NowFloor", nowFloor);
        PlayerPrefs.SetInt("EnemyMaxHp", enemyHpBase);
        PlayerPrefs.SetInt("GainGold", gainGold);
        PlayerPrefs.SetInt("GainExp", gainExp);

        logout_time = DateTime.UtcNow;
        PlayerPrefs.SetString("LogoutTime", logout_time.ToBinary().ToString());
        SaveScript.delete();
    }
}
