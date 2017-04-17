using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerInfoButtons : MonoBehaviour
{
    private RectTransform towerInfoButtons;
    private Button upButton;
    private Button sellButton;
    private Button cancelButton;
    private Text upMoneyText;
    private Text sellMoneyText;
    private Text nowLVText;
    private TowerController nowTC;
    private CubeBase nowCB;

    public TowerInfoButtons Init(GameObject newGO)
    {

        towerInfoButtons = newGO.GetComponent<RectTransform>();
        upButton = towerInfoButtons.Find("UPButton").GetComponent<Button>();
        sellButton = towerInfoButtons.Find("SellButton").GetComponent<Button>();
        cancelButton = towerInfoButtons.Find("CancelButton").GetComponent<Button>();
        upMoneyText = upButton.transform.Find("Money").GetComponent<Text>();
        sellMoneyText = sellButton.transform.Find("Money").GetComponent<Text>();
        nowLVText = towerInfoButtons.Find("NowLV/Text").GetComponent<Text>();
        towerInfoButtons.gameObject.SetActive(false);

        upButton.onClick.AddListener(UpgradeTowerButton);
        sellButton.onClick.AddListener(SellTowerButton);
        cancelButton.onClick.AddListener(CancelTowerButton);
        return this;
    }

    void Update()
    {
        if (nowTC != null)
        {
            CheckUpgradeButton();
            UpdateTowerInfoButtonPos(nowTC.transform);
        }
    }

    public void ClickTowerInfo(CubeBase cb)
    {

        if(nowCB==cb)
        {
            CancelTowerButton();
            return;
        }
        if (cb != null)
        {
            nowCB = cb;
            nowTC = cb.NowTC;
            RefershTowerInfo();
            UpdateTowerInfoButtonPos(nowTC.transform);
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

    public bool CheckUpgradeButton()
    {
        if (!nowTC.IsMaxLevel())
        {
            if (GameManager.Instance.GetMoney() >= nowTC.TB.info[nowTC.NowLevel + 1].money)
            {
                upButton.interactable = true;
                return true;
            }
            else
            {
                upButton.interactable = false;
                return false;
            }
        }
        else
        {
            upButton.interactable = false;
            return false;
        }
    }

    public void UpgradeTowerButton()
    {
        if (CheckUpgradeButton())
        {
            nowTC.UpgradeTower();
            RefershTowerInfo();
        }
    }

    public void SellTowerButton()
    {
        nowTC.SellSelf();
        nowCB.DestoryInfo();
        CancelTowerButton();
    }

    public void CancelTowerButton()
    {
        nowTC = null;
        nowCB = null;

        gameObject.SetActive(false);
    }

    public void RefershTowerInfo()
    {
        if (!nowTC.IsMaxLevel())
        {
            upButton.interactable = true;
            upMoneyText.text = nowTC.TB.info[nowTC.NowLevel + 1].money.ToString();
        }
        else
        {
            upButton.interactable = false;
            upMoneyText.text = "MAX";
        }

        sellMoneyText.text = nowTC.GetSellMoney().ToString();

        nowLVText.text =string.Format("LV:{0}",(nowTC.NowLevel+1).ToString());
    }
}
