using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerUp : Character
{
    //経験値育成
    public void OnClick()
    {
        if (ExpCharger.chargedExp >= CharacterManager._characterGrowExp[NameChanger(tag)])
        {
            CharacterManager._characterGrowExp[NameChanger(tag)] = Grawing(CharacterManager._characterGrowExp[NameChanger(tag)]);
            UpdateValue();
        }
    }
    //アイテム購入所持管理
    private int Grawing(int value_)
    {
        ExpCharger.chargedExp -= value_;
        value_ = (int)(value_ * CharacterManager.char_per_up);
        ++CharacterManager._characterLv[NameChanger(tag)];
        CharacterManager._characterPower[NameChanger(tag)] = (float)(0.1 * CharacterManager._characterCount
            [NameChanger(tag)] * CharacterManager._characterLv[NameChanger(tag)]);
        return value_;
    }
    private void UpdateValue()
    {
        _characterGrowExpText.text = $"{CharacterManager._characterGrowExp[NameChanger(tag)]} exp";
    }
    
}
