using UnityEngine;
using System.Collections;

public class EnemyWayPointController : MonoBehaviour
{
    private static EnemyWayPointController _instance;

    private Transform[] wayPoints;

    public static EnemyWayPointController Instance
    {
        get
        {
            return _instance;
        }
    }

    public Transform[] WayPoints
    {
        get
        {
            return wayPoints;
        }
    }

    void Awake()
    {
        _instance = this;
        wayPoints = new Transform[transform.childCount];
        for(int i = 0;i<transform.childCount;i++)
        {
            WayPoints[i] = transform.GetChild(i);
        }
    }
}
