using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    private Transform target;
    private float damage;

    public void Update()
    {
        if(target=null)
        {
            Destroy(gameObject);
        }
        else
        {
            if(Vector3.Distance(target.position,transform.position)<=0.15f)
            {
                //打伤害
                //创建效果
            }
        }
    }
}
