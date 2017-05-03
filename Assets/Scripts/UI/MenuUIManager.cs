using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIManager : MonoBehaviour
{
    private static MenuUIManager _instance;

    public static MenuUIManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private static bool isFirst = true;

    [SerializeField]
    private GameObject startPanel;
    [SerializeField]
    private GameObject choosePanel;

    private void Awake()
    {
        _instance = this;

    }

    private void Update()
    {
        if(isFirst&& Input.GetMouseButtonDown(0))
        {
            isFirst = false;
            startPanel.SetActive(false);
            choosePanel.SetActive(true);
        }
        else if(!isFirst)
        {
            startPanel.SetActive(false);
            choosePanel.SetActive(true);
        }
    }
}
