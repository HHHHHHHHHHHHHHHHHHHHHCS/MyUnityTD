using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class JsonManager
{
    private static JsonManager _instance;


    public static JsonManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new JsonManager();
#if UNITY_ANDROID
                _instance.filePath = string.Format("{0}/{1}", Application.persistentDataPath, fileName);
#elif UNITY_IPHONE
_instance.filePath = string.Format("{0}/{1}", Application.persistentDataPath, fileName);
#else
                _instance.filePath = string.Format("{0}/{1}", Application.dataPath, fileName);
#endif

            }
            return _instance;
        }

    }

    private const string fileName = "Level.json";
    private string filePath;



    public string Read()
    {
        string result = "";
        if (File.Exists(filePath))
        {
            using (StreamReader sr = File.OpenText(filePath))
            {
                result = sr.ReadToEnd();

            }
        }
        return result;
    }


    public List<LevelInfo> ToList()
    {
        return JsonConvert.DeserializeObject<List<LevelInfo>>(Read());
    }

    public string Save(List<LevelInfo> list)
    {
        string jsonStr = JsonConvert.SerializeObject(list);
        using (StreamWriter sw = File.CreateText(filePath))
        {
            sw.Write(jsonStr);
        }
        return jsonStr;
    }

    public string UpdateData(int level,int star)
    {
        var list = ToList();
        if(star> list[level].star)
        {
            list[level].star = star;
        }
        
        return Save(list);
    }
}
