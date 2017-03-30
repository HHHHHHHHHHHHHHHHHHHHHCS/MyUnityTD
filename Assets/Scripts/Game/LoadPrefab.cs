using UnityEngine;
using System.Collections;

public class LoadPrefab
{
    public static T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

}
