using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    private int nowMoeny;
    private TowerBaseList towerData;

    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }



    void Awake()
    {
        _instance = this;

    }

    void Start()
    {
        AddMoney(100);
    }


    public int AddMoney(int money)
    {
        nowMoeny += money;
        //通知UI
        CreateTowerUIManager.Instance.GetMoney(GetMoney());
        UIManager.Instance.RefreshMoneyText(GetMoney());
        return nowMoeny;
    }

    public int UseMoney(int money)
    {
        nowMoeny -= money;
        //通知UI
        CreateTowerUIManager.Instance.GetMoney(GetMoney());
        UIManager.Instance.RefreshMoneyText(GetMoney());

        return nowMoeny;
    }

    public int GetMoney()
    {
        return nowMoeny;
    }

}
