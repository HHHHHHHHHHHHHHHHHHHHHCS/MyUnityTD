using UnityEngine;
using System.Collections;

public class TowerController : MonoBehaviour
{
    private static Transform bulletManager;
    private TowerBase tb;
    private int nowLevel;
    private float attackTimer;

    private EnemyBase nowTarget;

    private Transform bulletPos;

    public TowerBase TB
    {
        get
        {
            return tb;
        }
    }

    public int NowLevel
    {
        get
        {
            return nowLevel;
        }
    }

    public TowerController Init(TowerBase _tb, int _nowLevel)
    {
        tb = _tb;
        nowLevel = _nowLevel;
        if (bulletPos == null)
        {
            bulletPos = transform.Find("BulletPos");
        }
        if (bulletManager == null)
        {
            bulletManager = GameObject.Find("BulletManager").transform;
        }
        return this;
    }

    void FixedUpdate()
    {
        Attack();
    }

    bool CheckDistance(Vector3 targetPos)
    {
        float range = Mathf.Pow(tb.info[nowLevel].attackRange, 2);
        float distance_sqrt = Mathf.Pow(transform.position.x - targetPos.x, 2) + Mathf.Pow(transform.position.z - targetPos.z, 2);
        return range >= distance_sqrt;
    }


    void _LookAt()
    {
        Vector3 lookAt = new Vector3(nowTarget.transform.position.x - transform.position.x, 0, nowTarget.transform.position.z - transform.position.z);
        Quaternion qua = Quaternion.LookRotation(lookAt);
        transform.rotation = qua;
    }

    void _Attack()
    {
        attackTimer = tb.info[nowLevel].attackSpeed;
        _LookAt();
        BulletBase bb = Instantiate(tb.info[nowLevel].bulletPrefab, bulletPos.position, tb.info[nowLevel].bulletPrefab.transform.rotation
            , bulletManager).GetComponent<BulletBase>();
        bb.Init(nowTarget, tb.attackType, tb.info[nowLevel].attack, tb.bulletSpeed);
    }


    void Attack()
    {
        if (EnemyManager.Instance.EnemyList.Count == 0)
        {
            if (attackTimer > 0)
            {
                attackTimer -= Time.fixedDeltaTime;
            }
            return;
        }
        if (nowTarget != null)
        {
            if (!CheckDistance(nowTarget.transform.position))
            {
                nowTarget = null;
            }
            else
            {
                _LookAt();
            }

        }
        if (nowTarget == null)
        {
            foreach (EnemyBase item in EnemyManager.Instance.EnemyList)
            {
                if (CheckDistance(item.transform.position))
                {
                    nowTarget = item;
                    _LookAt();
                    break;
                }
            }
        }
        if (attackTimer <= 0)
        {
            if (nowTarget != null)
            {
                _Attack();
            }
        }
        else
        {
            attackTimer -= Time.fixedDeltaTime;
        }
    }

    public int GetMaxLevel()
    {
        return tb.info.Count;
    }

    public bool IsMaxLevel()
    {
        return nowLevel >= (GetMaxLevel() - 1);
    }

    public int GetSellMoney()
    {
        int money = 0;
        for (int i = 0; i <= nowLevel; i++)
        {
            money += tb.info[i].money;
        }
        return money >> 1;
    }

    public void UpgradeTower()
    {
        GameManager.Instance.UseMoney(tb.info[nowLevel + 1].money);
        nowLevel++;
        transform.localScale += Vector3.one * 0.25f;
        CreateTowerUIManager.Instance.CreateTowerEffect(gameObject);
    }

    public void SellSelf()
    {
        Destroy(gameObject);
        GameManager.Instance.AddMoney(GetSellMoney());
    }
}

