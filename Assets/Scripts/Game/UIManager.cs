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

    private RectTransform towerInfoButtons;
    private Button upButton;
    private Button sellButton;
    private Button cancelButton;
    private Text upMoneyText;
    private Text sellMoneyText;

	private TowerController nowTC;

    void Awake()
    {
        _instance = this;
        Transform root = transform;
        moneyText = root.Find("MoneyPanel/MoneyText").GetComponent<Text>();

        GameObject newGO = Instantiate(Resources.Load<GameObject>("Tower/UI/TowerInfoButtons"), transform);

        towerInfoButtons = newGO.GetComponent<RectTransform>();
        upButton = towerInfoButtons.Find("UPButton").GetComponent<Button>();
        sellButton = towerInfoButtons.Find("SellButton").GetComponent<Button>();
        cancelButton = towerInfoButtons.Find("CancelButton").GetComponent<Button>();
        upMoneyText = upButton.transform.Find("Money").GetComponent<Text>();
        sellMoneyText = upButton.transform.Find("Money").GetComponent<Text>();
        towerInfoButtons.gameObject.SetActive(false);
    }

	void Update()
	{
		if (nowTC!=null) 
		{
			UpdateTowerInfoButtonPos (nowTC.transform);
		}
	}

    public void RefreshMoneyText(int moeny)
    {
        moneyText.text = moeny.ToString();
    }

	public void ClickTowerInfo(TowerController tc)
	{
		if (tc != null) 
		{
			nowTC = tc;
			UpdateTowerInfoButtonPos (tc.transform);
		}
	}


    public void UpdateTowerInfoButtonPos(Transform target)
    {
        Vector2 player2DPosition = Camera.main.WorldToScreenPoint(target.position);
        towerInfoButtons.position = player2DPosition;

        //血条超出屏幕就不显示  
        if (player2DPosition.x > Screen.width || player2DPosition.x < 0 || player2DPosition.y > Screen.height || player2DPosition.y < 0)
        {
            towerInfoButtons.gameObject.SetActive(false);
        }
        else
        {
            towerInfoButtons.gameObject.SetActive(true);
        }
    }

	public void HideTowerInfoButton()
	{
		towerInfoButtons.gameObject.SetActive(false);
	}

	public void UpdateTowerButton()
	{
		if (!nowTC.IsMaxLevel) 
		{
			
		} 
	}

	public void SellTowerButton()
	{
		
	}

	public void RefershTowerInfo()
	{
		if (!nowTC.IsMaxLevel)
		{
			upButton.interactable=true;
			upMoneyText.text = nowTC.TB.info [nowTC.NowLevel + 1].money;
		}
		else
		{
			upButton.interactable=false;
			upMoneyText.text = "MAX";
		}

		sellMoneyText.text = nowTC.GetSellMoney ();
	}


}
