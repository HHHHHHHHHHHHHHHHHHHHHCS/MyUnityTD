using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange
{
    private static SceneChange _instance;

    public static SceneChange Instance
    {
        get
        {
            if(_instance==null)
            {
                _instance = new SceneChange();
            }
            return _instance;
        }
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }



}
