using UnityEngine;
using System.Collections;

public class CubeBase : MonoBehaviour
{
    private int buildID;
    private int buildLevel;
    private TowerBase nowTB;

    public int BuildIID
    {
        get
        {
            return buildID;
        }
    }

    public int BuildLevel
    {
        get
        {
            return buildLevel;
        }
    }

    public void NewBuild(TowerBase tb)
    {
        buildID = tb.id;
        buildLevel = 1;
        nowTB = tb;
    }

    public void Upgrade(int upLevel = 1)
    {
        if(buildID!=0)
        {
            buildLevel += upLevel;
        }
    }

    public void Sell()
    {
        buildID = 0;
        buildLevel = 0;
    }

    public bool HaveBuild()
    {
        if(buildID!=0)
        {
            return true;
        }
        return false;

    }
}
