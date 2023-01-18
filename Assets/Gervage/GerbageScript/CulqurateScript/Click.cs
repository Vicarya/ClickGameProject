using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    [SerializeField]
    GameObject obj;
    private bool obj_flg = false;

    public UnityEngine.UI.Text enemyHpDisplay;

    public UnityEngine.UI.Text clickPow;
    public static int damagePerClick = 1;


    private void Start()
    {
        clickPow.text = $"Pow:{damagePerClick}";
        obj.SetActive(obj_flg);
    }

    public void Clicked()
    {
        if (EnemySpaun.enemyHp > 0)
        {
            EnemySpaun.enemyHp -= damagePerClick;
            StartCoroutine(AttackMotion());
        }
    }
    IEnumerator AttackMotion()
    {
        while (true)
        {
            if (obj_flg == true)
            {
                obj.SetActive(obj_flg = false);
                yield break;
            }
            else if (obj.activeSelf == false)
            {
                obj.SetActive(obj_flg = true);
                yield return new WaitForSeconds(0.02f);
            }
        }
    }
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
            damagePerClick = PlayerPrefs.GetInt("Click", damagePerClick);
        else
        {
            PlayerPrefs.SetInt("Click", damagePerClick);
            SaveScript.delete();
        }
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Click", damagePerClick);
        SaveScript.delete();
    }
}
