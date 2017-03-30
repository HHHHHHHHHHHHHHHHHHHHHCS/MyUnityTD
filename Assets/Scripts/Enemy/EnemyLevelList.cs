using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct EnemyLevelDeail
{
    [System.Serializable]
    public struct KV
    {
        public int id;
        public int count;
    }

    public List<KV> list;
}


[System.Serializable]
public class EnemyLevelInfo
{
    public int levelID;
    public float spawnTime = 0.75f;
    public float nextWaveTime = 15f;
    public List<EnemyLevelDeail> levelInfo = new List<EnemyLevelDeail>();
}

[System.Serializable]
[CreateAssetMenu(fileName = "MyLevelAsset", menuName = "My/Level", order = 3)]
public class EnemyLevelList : ScriptableObject
{
    public List<EnemyLevelInfo> data;
}
