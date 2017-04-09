using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    private EnemyBase eb;
    private Transform target;
    private AttaclType attackType;
    private float damage;
    private float speed;

    public void Init(EnemyBase _eb,AttaclType _attackType, float _damage, float _speed)
    {
        eb = _eb;
        target = _eb.transform;
        attackType = _attackType;
        damage = _damage;
        speed = _speed;
    }


    public void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
        }
        else if (!TakeDamage())
        {
            Vector3 lookAt = new Vector3(target.transform.position.x - transform.position.x, 0, target.transform.position.z - transform.position.z);
            Quaternion qua = Quaternion.LookRotation(lookAt);
            transform.rotation = qua;
            transform.position += (transform.forward * speed * Time.deltaTime);
            TakeDamage();
        }
    }


    bool TakeDamage()
    {
        Vector2 dis = new Vector2(target.position.x - transform.position.x, target.position.z - transform.position.z);
        if (Vector2.Distance(dis, Vector2.zero) <= 0.5f)
        {
            Destroy(gameObject);
            eb.TakeDamage(attackType,damage);
            return true;
        }
        return false;
    }
}
