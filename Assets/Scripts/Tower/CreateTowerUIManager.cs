﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CreateTowerUIManager : MonoBehaviour
{
    private static CreateTowerUIManager _instance;

    private const string buildEffectPath = "Tower/Effect/BuildEffect";
    private const string createButtonPath = "Tower/UI/CreateTowerButton";
    private const string createTowerPath = "TowerList";

    private LayerMask layer ;

    private Transform towerParent;

    private Vector2 startPos = new Vector2(60, 55);
    private float offsetX = 90;

    private int nowID = 0;

    private CreateTowerButton[] towerUIArr;

    private GameObject buildEffectPrefab;

    public static CreateTowerUIManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public void Awake()
    {
        _instance = this;
        towerParent = GameObject.Find(createTowerPath).transform;
        buildEffectPrefab = Resources.Load<GameObject>(buildEffectPath);
        layer = LayerMask.GetMask("MapCube");
        int[] towers = { 20001, 20002, 20003 };
        CreateTowerUI(towers);
    }


    public void Update()
    {
        if(nowID!=0&& Input.GetMouseButtonDown(0))
        {
            if(!EventSystem.current.IsPointerOverGameObject())
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray, out hit, 100,layer);
                if(isCollider)
                {
                    GameObject mapCube = hit.collider.gameObject;
                    CubeBase cb = mapCube.GetComponent<CubeBase>();
                    if(cb!=null&&!cb.HaveBuild())
                    {
                        TowerBase tb = TowerDataManager.Instance.GetByID(nowID);
                        GameManager.Instance.UseMoney(tb.info[0].money);
                        ((GameObject)Instantiate(tb.info[0].prefab, mapCube.transform.position,Quaternion.identity, towerParent))
                            .GetComponent<TowerController>().Init(tb,0);
                        nowID = 0;
                        cb.NewBuild(tb);
                        if(buildEffectPrefab!=null)
                        {
                            Destroy(Instantiate(buildEffectPrefab, mapCube.transform.position
                                , Quaternion.identity, mapCube.transform),1.0f);
                        }

                    }
                }
            }
        }
    }


    void CreateTowerUI(int[] towerIDs)
    {
        GameObject prefab = LoadPrefab.Load<GameObject>(createButtonPath);
        towerUIArr = new CreateTowerButton[towerIDs.Length];
        for (int i = 0; i < towerIDs.Length; i++)
        {
            GameObject newGo = (GameObject)Instantiate(prefab, transform);
            ((RectTransform)newGo.transform).anchoredPosition = new Vector2(startPos.x + offsetX * i, startPos.y);
            towerUIArr[i] = newGo.GetComponent<CreateTowerButton>();
            towerUIArr[i].Init(towerIDs[i]);
        }
    }


    public void GetMoney(int money)
    {
        foreach(CreateTowerButton item in towerUIArr)
        {
            item.RefreshMask(money);
        }
    }


    public void CreateTower(int id)
    {
        if(id!=0)
        {
            if(nowID==id)
            {
                id = 0;
            }
            else
            {
                nowID = id;
            }
        }
    }
}