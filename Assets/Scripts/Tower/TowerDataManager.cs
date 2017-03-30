using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerDataManager
{
    private static TowerDataManager _instance;

    private const string path = "ConfigData/TowerAsset";

    public static TowerDataManager Instance
    {
        get
        {
            if(_instance==null)
            {
                _instance = new TowerDataManager();
                _instance.Init();
            }
            return _instance;
        }
    }

    private Dictionary<int, TowerBase> dic;

    void Init()
    {
        dic = new Dictionary<int, TowerBase>();
        TowerBaseList list = LoadPrefab.Load<TowerBaseList>(path);
        foreach (TowerBase item in list.data)
        {
            if (dic.ContainsKey(item.id))
            {
                continue;
            }
            dic.Add(item.id, item);
        }
        UnLoadAsset(list);
    }

    public TowerBase GetByID(int id)
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
