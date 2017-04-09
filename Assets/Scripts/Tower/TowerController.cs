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


    public void Init(TowerBase _tb, int _nowLevel)
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


    void _Attack()
    {
        attackTimer = tb.info[nowLevel].attackSpeed;
        Vector3 lookAt = new Vector3(nowTarget.transform.position.x - transform.position.x, 0, nowTarget.transform.position.z - transform.position.z);
        Quaternion qua = Quaternion.LookRotation(lookAt);
        transform.rotation = qua;
        BulletBase bb= Instantiate(tb.info[nowLevel].bulletPrefab, bulletPos.position, tb.info[nowLevel].bulletPrefab.transform.rotation
            , bulletManager).GetComponent<BulletBase>();
        bb.Init(nowTarget,tb.attackType, tb.info[nowLevel].attack, tb.bulletSpeed);
    }


    void Attack()
    {
        if (attackTimer <= 0)
        {
            if (nowTarget != null)
            {
                if (!CheckDistance(nowTarget.transform.position))
                {
                    nowTarget = null;
                }
            }
            if (nowTarget == null)
            {
                foreach (EnemyBase item in EnemyManager.Instance.EnemyList)
                {
                    if (CheckDistance(item.transform.position))
                    {
                        nowTarget = item;
                        break;
                    }
                }
            }
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
}
