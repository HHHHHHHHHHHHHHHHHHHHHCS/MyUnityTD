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

    [SerializeField]
    private TowerInfoButtons towerInfoOperate;
    [SerializeField]
    private Text moneyText;
    [SerializeField]
    private Text hpText;
    [SerializeField]
    private GameObject winPanel;
    [SerializeField]
    private GameObject failPanel;
    [SerializeField]
    private Text winStart;
    [SerializeField]
    private Button win_Back;
    [SerializeField]
    private Button fail_Back;
    [SerializeField]
    private Button fail_Again;

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

        towerInfoOperate.Init();

        win_Back.onClick.AddListener(() => { SceneChange.Instance.Load(SceneName.menu); });
        fail_Back.onClick.AddListener(() => { SceneChange.Instance.Load(SceneName.menu); });
        fail_Again.onClick.AddListener(() => { SceneChange.Instance.Reload(); });
    }



    public void RefreshMoneyText(int moeny)
    {
        moneyText.text = moeny.ToString();
    }


    public void RefreshHPText(int hp)
    {
        hpText.text = hp.ToString();
    }

    public void WinGame(int star)
    {
        winPanel.SetActive(true);
        winStart.text = GameManager.GetStartByCount(star);
    }

    public void FailGame()
    {
        failPanel.SetActive(true);
    }
}
