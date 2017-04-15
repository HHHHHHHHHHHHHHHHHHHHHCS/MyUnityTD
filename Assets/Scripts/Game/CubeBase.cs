using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CubeBase : MonoBehaviour
{
    private bool isEnter;
    private Renderer render;
    private int buildID;
    private int buildLevel;
    private TowerBase nowTB;

    public int BuildIID
    {
        get
        {
            return buildID;
        }
    }

    public int BuildLevel
    {
        get
        {
            return buildLevel;
        }
    }

    private void Start()
    {
        render = GetComponent<Renderer>();
    }

    void Update()
    {
        if (isEnter && buildID != 0 &&Input.GetMouseButtonDown(0)
            && !CreateTowerUIManager.Instance.IsBuildingNow())
        {
            UIManager.Instance.UpdateTowerInfoButtonPos(transform);
        }
    }

    public void NewBuild(TowerBase tb)
    {
        buildID = tb.id;
        buildLevel = 1;
        nowTB = tb;
    }

    public void Upgrade(int upLevel = 1)
    {
        if (buildID != 0)
        {
            buildLevel += upLevel;
        }
    }

    public void Sell()
    {
        buildID = 0;
        buildLevel = 0;
    }

    public bool HaveBuild()
    {
        if (buildID != 0)
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
