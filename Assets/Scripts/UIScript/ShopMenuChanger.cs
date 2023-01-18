using UnityEngine;
using UnityEngine.UI;

public class ShopMenuChanger : MonoBehaviour
{
    /// <summary> ショップメニュー名 </summary>
    string menuName;
    /// <summary> ショップメニュー名テキスト </summary>
    public Text text_;
    /// <summary> ショップメニューEnterボタン </summary>
    public GameObject[] menu;
    /// <summary> メニューバックボタン </summary>
    public GameObject backButton;
    /// <summary> ショップメニューインデクス </summary>
    private int menuNum = 0;
    /// <summary> シングルトン </summary>
    private static ShopMenuChanger menuChanger;

    public static ShopMenuChanger GetInstance() => menuChanger;

    /// <summary>
    /// メニュー初期化
    /// </summary>
    void Start()
    {
        ChangeMenu("SHOP");
        menuChanger = this;
    }

    /// <summary>
    /// メニュー変更
    /// ・押されたボタンに対応するメニューに変更される
    /// </summary>
    /// <param name="menuname">メニューボタン</param>
    public void ChangeMenu(string menuname)
    {
        menuName = menuname;

        if (menuName == "SHOP")
        {
            menu[menuNum = 0].SetActive(true);
        }
        else if(menuName == "Purchase")
        {
            menu[menuNum = 1].SetActive(true);
            
        }
        else if (menuName == "Gacha")
        {
            menu[menuNum = 2].SetActive(true);

        }
        else if (menuName == "PowerUp")
        {
            menu[menuNum = 3].SetActive(true);

        }
        else if (menuName == "Hire")
        {
            menu[menuNum = 4].SetActive(true);

        }
        for(int i = 0; i < menu.Length; i++)
        {
            if(i != menuNum)
            {
                menu[i].SetActive(false);
            }
        }
        if (menuNum == 0)
        {
            backButton.SetActive(false);
        }
        else
        {
            backButton.SetActive(true);
        }
    }
}
