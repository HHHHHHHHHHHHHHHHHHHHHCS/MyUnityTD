using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    private static LevelInfo nowLevel;

    private int startHP = 5;

    private int nowHp;
    private int nowMoeny;
    private TowerBaseList towerData;

    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public static LevelInfo SetNowLevel(LevelInfo _nowLevel)
    {

        nowLevel = _nowLevel;
        return nowLevel;
    }

    void Awake()
    {
        _instance = this;

    }

    void Start()
    {
        ChangeHP(startHP);
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

    public int ChangeHP(int value)
    {
        nowHp += value;
        if (nowHp > 0)
        {
            UIManager.Instance.RefreshHPText(nowHp);
        }
        else
        {
            nowHp = 0;
            UIManager.Instance.RefreshHPText(nowHp);
            FailGame();
        }
        return nowHp;
    }

    public void WinGame()
    {
        int star = JudgeStar(nowHp, startHP);
        UIManager.Instance.WinGame(star);
        
        JsonManager.Instance.UpdateData(nowLevel.levelID, star);
    }


    public void FailGame()
    {
        Time.timeScale = 0;
        UIManager.Instance.FailGame();
    }

    public void OnDestroy()
    {
        _instance = null;
    }

    public int JudgeStar(int _nowHP, int _maxHP)
    {
        float f = (float)_nowHP / _maxHP;
        if (f >= 1)
        {
            return 3;
        }
        else if (f >= 0.5)
        {
            return 2;
        }
        else if (f >= 0)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }



    public static string GetStartByCount(int count)
    {
        string result = "";
        switch (count)
        {
            case 0:
                {
                    result = "☆ ☆ ☆";
                    break;
                }
            case 1:
                {
                    result = "★ ☆ ☆";
                    break;
                }
            case 2:
                {
                    result = "★ ★ ☆";
                    break;
                }
            case 3:
                {
                    result = "★ ★ ★";
                    break;
                }
            default:
                {
                    result = "☆ ☆ ☆";
                    break;
                }
        }


        return result;
    }
}
