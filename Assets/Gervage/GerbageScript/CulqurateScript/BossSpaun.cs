using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpaun : MonoBehaviour
{
    EnemySpaun enemy;
    public const int TIMELIMIT = 10;
    public void OnClick()
    {
        enemy.bossFlg = true;
        EnemySpaun.enemyHp = enemy.enemyHpBase * 5;
        enemy.deltaClock = TIMELIMIT;
        ///挑戦ボタンを消す動作。
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
