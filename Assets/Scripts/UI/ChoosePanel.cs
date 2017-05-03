using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosePanel : MonoBehaviour
{

    private void Awake()
    {
        List<LevelInfo> list = JsonManager.Instance.ToList();
        GameObject prefab = Resources.Load<GameObject>("UI/ChooseLevelButton");

        foreach(var i in list)
        {
            GameObject newGO = Instantiate(prefab);
            newGO.transform.SetParent(transform);
            newGO.GetComponent<ChooseLevelButton>().Init(i);
        }
    }
}
