using UnityEngine;
using System.Collections;



public class EnemyBase : MonoBehaviour
{
    private Transform[] movePoints;
    private int moveIndex = 0;

    private EnemyInfo enemyInfo;

    public EnemyInfo EnemyInfo
    {
        get
        {
            return enemyInfo;
        }

        set
        {
            enemyInfo = value;
        }
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


    public virtual float TakeDamage(AttaclType damageType,float damage)
    {
        enemyInfo.hp -= damage;
        if (enemyInfo.hp<= 0)
        {
            enemyInfo.hp = 0;
            Dead();
        }
        return enemyInfo.hp;
    }


    /// <summary>
    /// 被打死
    /// </summary>
    protected virtual void Dead()
    {
        GameManager.Instance.AddMoney(enemyInfo.reward);
        EnemyManager.Instance.ReduceEnemyCount(this);
    }


    /// <summary>
    /// 到达终点自我销毁
    /// </summary>
    protected virtual void Arrived()
    {
        EnemyManager.Instance.ReduceEnemyCount(this);
    }



}
