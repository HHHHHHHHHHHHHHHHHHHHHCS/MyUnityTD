using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyInfoDataManager
{
    private static EnemyInfoDataManager _instance;

    private const string path = "ConfigData/EnemyAsset";
    private Dictionary<int, EnemyInfo> dic;

    public static EnemyInfoDataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new EnemyInfoDataManager();
                _instance.Init();
            }
            return _instance;
        }
    }



    void Init()
    {
        dic = new Dictionary<int, EnemyInfo>();
        EnemyInfoList list = LoadPrefab.Load<EnemyInfoList>(path);
        foreach (EnemyInfo item in list.data)
        {
            if (dic.ContainsKey(item.id))
            {
                continue;
            }
            dic.Add(item.id, item);
        }
        UnLoadAsset(list);
    }

    public EnemyInfo GetByID(int id)
    {
        if (dic == null || dic.Count == 0 || !dic.ContainsKey(id))
        {
            return null;
        }
        return dic[id];
    }


    private void UnLoadAsset(Object asset)
    {
        Resources.UnloadAsset(asset);
        asset = null;
    }
}
