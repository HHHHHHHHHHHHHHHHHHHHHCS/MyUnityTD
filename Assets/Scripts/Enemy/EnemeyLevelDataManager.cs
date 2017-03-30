using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemeyLevelDataManager 
{
    private static EnemeyLevelDataManager _instance;

    private const string path = "ConfigData/LevelAsset";
    private Dictionary<int, EnemyLevelInfo> dic;

    public static EnemeyLevelDataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new EnemeyLevelDataManager();
                _instance.Init();
            }
            return _instance;
        }
    }



    void Init()
    {
        dic = new Dictionary<int, EnemyLevelInfo>();
        EnemyLevelList list = LoadPrefab.Load<EnemyLevelList>(path);
        foreach (EnemyLevelInfo item in list.data)
        {
            if (dic.ContainsKey(item.levelID))
            {
                continue;
            }
            dic.Add(item.levelID, item);
        }

        UnLoadAsset(list);
    }

    public EnemyLevelInfo GetByID(int id)
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
