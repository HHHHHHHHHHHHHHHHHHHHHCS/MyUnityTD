using UnityEngine;
using System.Collections;



public class EnemyBase : MonoBehaviour
{

    private const string deadEffectPath = "Enemy/Effect/EnemyDeadEffect";

    private static GameObject deadEffect;

    private Transform[] movePoints;
    private int moveIndex = 0;

    private EnemyInfo enemyInfo;

    private EnemyHPBar enemyHPBar;

    public EnemyInfo EnemyInfo
    {
        get
        {
            return enemyInfo;
        }
    }

    public virtual void Awake()
    {
        if (deadEffect == null)
        {
            deadEffect = Resources.Load<GameObject>(deadEffectPath);
        }

    }

    public virtual void Init(EnemyInfo _enemyInfo, EnemyHPBar _enemyHPBar)
    {
        enemyInfo = _enemyInfo;
        enemyInfo.nowHP = enemyInfo.hp;
        enemyHPBar = _enemyHPBar;
    }

    public virtual void Start()
    {
        movePoints = EnemyWayPointController.Instance.WayPoints;

    }

    public virtual void Update()
    {
        Move();
    }


    protected virtual void Move()
    {
        Vector3 dir = movePoints[moveIndex].position - transform.position;
        float endDistance = Vector3.Distance(Vector3.zero, dir);
        if (dir != Vector3.zero && EnemyInfo.speed > 0)
        {

            Vector3 moveDistanceDir = dir.normalized * EnemyInfo.speed * Time.deltaTime;
            float moveDistance = Vector3.Distance(Vector3.zero, moveDistanceDir);
            while (moveDistance > 0)
            {
                if (moveDistance <= endDistance)
                {
                    moveDistance -= endDistance;
                    transform.Translate(moveDistanceDir);
                }
                else
                {
                    transform.position = movePoints[moveIndex].position;
                    if (moveIndex >= movePoints.Length - 1)
                    {
                        moveDistance = 0;
                        Arrived();
                        break;
                    }
                    else
                    {
                        moveIndex++;
                        dir = movePoints[moveIndex].position - transform.position;
                        moveDistance = moveDistance - endDistance;
                        moveDistanceDir = dir.normalized * moveDistance;
                        endDistance = Vector3.Distance(Vector3.zero, dir);
                    }
                }
            }

        }
    }
    public virtual float GetHPPer()
    {
        return enemyInfo.nowHP <= 0 ? 0 : enemyInfo.nowHP / enemyInfo.hp;
    }

    public virtual float TakeDamage(AttaclType damageType, float damage)
    {
        enemyInfo.nowHP -= damage;
       
        if (enemyInfo.nowHP <= 0)
        {
            enemyInfo.nowHP = 0;
            Dead();
        }
        if (enemyHPBar != null)
        {
            enemyHPBar.UpdateHP(GetHPPer());
        }
        return enemyInfo.hp;
    }


    /// <summary>
    /// 被打死
    /// </summary>
    protected virtual void Dead()
    {
        _Dead();
        if (deadEffect)
        {
            Destroy(Instantiate(deadEffect, transform.position + Vector3.up, Quaternion.identity), 1f);
        }
        GameManager.Instance.AddMoney(enemyInfo.reward);

    }


    /// <summary>
    /// 到达终点自我销毁
    /// </summary>
    protected virtual void Arrived()
    {
        _Dead();
    }

    protected virtual void _Dead()
    {
        EnemyManager.Instance.ReduceEnemyCount(this);
        if (enemyHPBar != null)
        {
            enemyHPBar.DestroySelf();
        }
    }

}
