using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CubeBase : MonoBehaviour
{
    private bool isEnter;
    private Renderer render;
    private int buildID;
    private TowerController nowTC;

    public int BuildIID
    {
        get
        {
            return buildID;
        }
    }

    public TowerController NowTC
    {
        get
        {
            return nowTC;
        }
    }

    private void Start()
    {
        render = GetComponent<Renderer>();
    }

    void Update()
    {
        if (isEnter && buildID != 0&& Input.GetMouseButtonDown(0) 
            && !EventSystem.current.IsPointerOverGameObject()
            && nowTC != null && !CreateTowerUIManager.Instance.IsBuildingNow())
        {
            UIManager.Instance.TowerInfoOperate.ClickTowerInfo(this);
        }
    }

    public void NewBuild(TowerController tc)
    {
        nowTC = tc;
        buildID = nowTC.TB.id;
    }

    public void DestoryInfo()
    {
        buildID = 0;
        nowTC = null;
    }

    public bool HaveBuild()
    {
        if (buildID != 0 && NowTC != null)
        {
            return true;
        }
        return false;
    }

    private void OnMouseEnter()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            isEnter = true;
            render.material.color = Color.blue;
        }
    }

    private void OnMouseExit()
    {
        isEnter = false;
        render.material.color = Color.white;
    }
}
