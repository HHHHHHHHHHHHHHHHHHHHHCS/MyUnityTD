using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class EnemyManager : MonoBehaviour
{

    private static EnemyManager _instance;


    private EnemyLevelInfo levelList;

    private float startWatiTime = 5f;

    private Vector3 startPos;

    private bool finishSpawn;

    private List<EnemyBase> enemyList = new List<EnemyBase>();

    private static GameObject enemyHPBar;
    private const string enemyHPBarPath = "Enemy/UI/HPBar";
    private static Transform enemyHPBarParent;
    private const string enemyHPBarParentPath = "Canvas/HPBarManager";
    private const string enemyHPBarPosPath = "HPBarPos";


    public static EnemyManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public List<EnemyBase> EnemyList
    {
        get
        {
            return enemyList;
        }
    }

    void Awake()
    {
        _instance = this;
        finishSpawn = false;
        SetList(1);
        startPos = GameObject.Find("StartPos").transform.position;
        if(enemyHPBar==null)
        {
            enemyHPBar = Resources.Load<GameObject>(enemyHPBarPath);
        }
        if (enemyHPBarParent == null)
        {
            enemyHPBarParent = GameObject.Find(enemyHPBarParentPath).transform;
        }
    }

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    public int ReduceEnemyCount(EnemyBase deathEnemy)
    {
        if(enemyList.Contains(deathEnemy))
        {
            enemyList.Remove(deathEnemy);
            Destroy(deathEnemy.gameObject);
        }

        if (enemyList.Count <= 0 && finishSpawn)
        {
            Debug.Log("Win");
        }
        return enemyList.Count;
    }

    public void SetList(int i)
    {

        levelList = EnemeyLevelDataManager.Instance.GetByID(i);
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(startWatiTime);

        int nowWave = 0;
        foreach (EnemyLevelDeail levelInfo in levelList.levelInfo)
        {
            nowWave++;

            foreach (EnemyLevelDeail.KV waveInfo in levelInfo.list)
            {
                if (waveInfo.count == 0)
                {
                    continue;
                }

                EnemyInfo waveEnemyInfo=GetEnemyInfoById(waveInfo.id);

                for (int count = 0; count < waveInfo.count; count++)
                {
                    SpawnEnemy(waveEnemyInfo);
                    yield return new WaitForSeconds(levelList.spawnTime);
                }
            }
            if (nowWave == levelList.levelInfo.Count)
            {
                finishSpawn = true;
            }
            else
            {
                yield return new WaitForSeconds(levelList.nextWaveTime);
            }
        }
    }

    EnemyBase SpawnEnemy(EnemyInfo waveEnemyInfo)
    {
        EnemyBase eb = Instantiate(waveEnemyInfo.prefab, startPos, Quaternion.identity, transform)
    .GetComponent<EnemyBase>();
        EnemyHPBar hpBar=null;
        if (enemyHPBar)
        {
            hpBar = Instantiate(enemyHPBar, enemyHPBarParent).GetComponent<EnemyHPBar>();
        }
        eb.Init(waveEnemyInfo.Clone(),hpBar);
        if(hpBar)
        {
            hpBar.Init(eb.transform.Find(enemyHPBarPosPath), eb.GetHPPer());
        }
        enemyList.Add(eb);
        return eb;
    }

    public EnemyInfo GetEnemyInfoById(int id)
    {
        return EnemyInfoDataManager.Instance.GetByID(id);
    }
}


