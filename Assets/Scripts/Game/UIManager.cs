using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            return _instance;
        }
    }


    private Text moneyText;

    void Awake ()
    {
        _instance = this;
        Transform root = transform;
        moneyText = root.Find("MoneyPanel/MoneyText").GetComponent<Text>();

	}
	
	public void RefreshMoneyText (int moeny)
    {
        moneyText.text = moeny.ToString();
	}
}
