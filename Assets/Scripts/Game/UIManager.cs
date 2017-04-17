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
    private TowerInfoButtons towerInfoOperate;

    public TowerInfoButtons TowerInfoOperate
    {
        get
        {
            return towerInfoOperate;
        }
    }

    void Awake()
    {
        _instance = this;
        Transform root = transform;
        moneyText = root.Find("MoneyPanel/MoneyText").GetComponent<Text>();
        GameObject newGO = Instantiate(Resources.Load<GameObject>("Tower/UI/TowerInfoButtons"), transform);
        towerInfoOperate = newGO.GetComponent<TowerInfoButtons>().Init(newGO);


    }



    public void RefreshMoneyText(int moeny)
    {
        moneyText.text = moeny.ToString();
    }

    


}
