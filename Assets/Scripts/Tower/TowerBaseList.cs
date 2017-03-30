using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public enum AttaclType
{
    physical,
    magic,
    pure
}

[System.Serializable]
public class TowerInfo
{
    public GameObject prefab;
    public GameObject bulletPrefab;
    public float attack;
    public float attackSpeed;
    public float attackRange;
    public int money;
}


[System.Serializable]
public class TowerBase
{
    public int id;
    public string objectName;
    public Sprite uiImage;
    public AttaclType attackType = AttaclType.physical;
    public List<TowerInfo> info;
}


[System.Serializable]
[CreateAssetMenu(fileName = "MyTowerAsset", menuName = "My/Tower", order = 1)]
public class TowerBaseList:ScriptableObject
{

    public List<TowerBase> data;
}
