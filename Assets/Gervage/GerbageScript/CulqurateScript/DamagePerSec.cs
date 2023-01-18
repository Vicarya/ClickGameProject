using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePerSec : MonoBehaviour
{
    public UnityEngine.UI.Text dpsDisplay;
    public Click click;
    public ItemManager[] items;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AutoClick());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int SamDamagePerSec()
    {
        int click = 0;
        foreach (ItemManager item in items)
        {
            click += item.count * item.clickPower;
        }
        return click;
    }

    public void AutoAttackPerSec()
    {
        EnemySpaun.enemyHp -= SamDamagePerSec();
    }

    IEnumerator AutoClick()
    {
        while (true)
        {
            AutoAttackPerSec();
            yield return new WaitForSeconds(1);
        }
    }
}
