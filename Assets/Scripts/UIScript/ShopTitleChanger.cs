using UnityEngine;
using UnityEngine.UI;

public class ShopTitleChanger : MonoBehaviour
{
    /// <summary> ショップメニュータイトルテキスト </summary>
    public Text text_;

    /// <summary>
    /// クリック時挙動
    /// ・メニューボタンを押すとショップタイトルが変わる
    /// </summary>
    /// <param name="obj">メニュー名</param>
    public void onClick(Button obj)
    {
        var menuChander = ShopMenuChanger.GetInstance();
        if(obj.name == "Purchase")
        {
            text_.text = "Purchase";
        }
        else if (obj.name == "Gacha")
        {
            text_.text = "Gacha";
        }
        else if (obj.name == "PowerUp")
        {
            text_.text = "PowerUp";
        }
        else if (obj.name == "Hire")
        {
            text_.text = "Hire";
        }
        else if(obj.name == "BackButton")
        {
            text_.text = "SHOP";
        }
        menuChander.ChangeMenu(text_.text);
    }
}
