using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class EnemyInfo
{
    public int id;
    public GameObject prefab;
    public float nowHP;
    public float hp;
    public float speed;
    public float arrmor;
    public float magicArrmor;
    public int reward;


    public EnemyInfo Clone()
    {
        return MemberwiseClone() as EnemyInfo;
    }
}

[System.Serializable]
[CreateAssetMenu(fileName = "MyEnemyAsset", menuName = "My/Enemy", order = 2)]
public class EnemyInfoList : ScriptableObject
{
    public List<EnemyInfo> data;

}
