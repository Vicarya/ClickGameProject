using UnityEngine;
using UnityEngine.UI;
public class CharacterButton : Character
{
    
    //強化値育成
    public void OnClick()
    {
        if (MoneyView.haveMoney >= CharacterManager._characterPlusPrice[NameChanger(tag)])
        {
            CharacterManager._characterPlusPrice[NameChanger(tag)] = Consume(CharacterManager._characterPlusPrice[NameChanger(tag)]);
            UpdateValue();
        }
    }
    //アイテム購入所持管理
    private int Consume(int value_)
    {
        MoneyView.haveMoney -= value_;
        value_ = (int)(value_ * CharacterManager.char_per_up);
        ++CharacterManager._characterCount[NameChanger(tag)];
        CharacterManager._characterPower[NameChanger(tag)] = (float)(0.1 * CharacterManager._characterCount
            [NameChanger(tag)] * CharacterManager._characterLv[NameChanger(tag)]);
        return value_;
    }
    private void UpdateValue()
    {
        _characterPlusPriceText.text = $"{CharacterManager._characterPlusPrice[NameChanger(tag)]} Gold";
    }
    //string name = gameObject.tag;
}