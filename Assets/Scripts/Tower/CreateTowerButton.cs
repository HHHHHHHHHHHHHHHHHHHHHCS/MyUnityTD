using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreateTowerButton : MonoBehaviour
{

    private Button button;
    private Image towerImage;
    private GameObject mask;
    private Text moneyText;

    private int towerID;
    private int needMoney;

    public int NeedMoney
    {
        get
        {
            return needMoney;
        }
    }

    public void Awake()
    {
        button = GetComponent<Button>();
        towerImage = transform.Find("TowerImage").GetComponent<Image>();
        mask = transform.Find("MaskImage").GetComponent<Image>().gameObject;
        moneyText = transform.Find("MoneyText").GetComponent<Text>();
        button.onClick.AddListener(CreateTower);

    }

    public void Init(int id)
    {
        TowerBase tb = TowerDataManager.Instance.GetByID(id);
        if (tb != null)
        {
            towerID = id;
            towerImage.sprite = tb.uiImage;
            needMoney = tb.info[0].money;
            moneyText.text = needMoney.ToString();
        }
    }

    public void RefreshMask(int moeny)
    {
        if (moeny >= needMoney && mask.activeSelf)
        {
            mask.SetActive(false);
        }
        else if (moeny < needMoney && !mask.activeSelf)
        {
            mask.SetActive(true);
        }
    }

    public void CreateTower()
    {
        CreateTowerUIManager.Instance.CreateTower(towerID);
    }

}
