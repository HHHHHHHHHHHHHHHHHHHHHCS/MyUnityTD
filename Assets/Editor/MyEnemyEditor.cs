using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class MyEnemyEditor : EditorWindow
{

    public EnemyInfoList enemyBaseList;
    private int viewIndex = 1;
    private Vector2 nowPos;


    private const string myItemPath = "myEnemyPath";
    private const string newAssetPath = "Assets/newEnemyAsset.asset";

    [MenuItem("My Editor/My Enemy Editor %#e")]
    static void Init()
    {
        GetWindow(typeof(MyEnemyEditor));
    }

    void OnEnable()
    {
        if (EditorPrefs.HasKey(myItemPath))
        {
            string objectPath = EditorPrefs.GetString(myItemPath);
            enemyBaseList = AssetDatabase.LoadAssetAtPath<EnemyInfoList>(objectPath);
        }
    }


    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        if (enemyBaseList != null)
        {
            if (GUILayout.Button("Show Item List"))
            {
                FocusWindow();
            }
        }
        if (GUILayout.Button("Open Item List"))
        {
            OpenItemList();
        }
        if (GUILayout.Button("New Item List"))
        {
            CreateNewItemList();
        }
        GUILayout.EndHorizontal();
        nowPos = EditorGUILayout.BeginScrollView(nowPos, false, false);
        if (enemyBaseList == null)
        {
            GUILayout.Label("No Select Object!!!");
        }

        GUILayout.Space(5);

        if (enemyBaseList != null)
        {
            GUILayout.BeginHorizontal();

            if (enemyBaseList.data.Count > 0)
            {
                if (GUILayout.Button("Previous"))
                {
                    Selection.activeObject = enemyBaseList;
                    if (viewIndex > 1)
                        viewIndex--;
                }
                GUILayout.Space(5);
                if (GUILayout.Button("Next"))
                {
                    if (viewIndex < enemyBaseList.data.Count)
                    {
                        viewIndex++;
                    }
                }
                GUILayout.Space(10);
                viewIndex = Mathf.Clamp(viewIndex, 1, enemyBaseList.data.Count);

            }


            GUILayout.Space(10);
            if (GUILayout.Button("Add Item"))
            {
                Selection.activeObject = enemyBaseList;
                AddItem();
            }
            if (enemyBaseList.data.Count > 0)
            {
                if (GUILayout.Button("Delete Item"))
                {
                    DeleteItem(viewIndex - 1);
                }
            }

            GUILayout.EndHorizontal();
            GUILayout.Space(10);



            if (enemyBaseList.data.Count > 0)
            {
                EnemyInfo item = enemyBaseList.data[viewIndex - 1];

                GUILayout.BeginHorizontal();
                GUILayout.Space(10);
                viewIndex = Mathf.Clamp(EditorGUILayout.IntField("Current Item", viewIndex, GUILayout.ExpandWidth(false)), 1, enemyBaseList.data.Count);
                EditorGUILayout.LabelField("of   " + enemyBaseList.data.Count.ToString() + "  items", "", GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal();

                item.id = EditorGUILayout.IntField("Item ID", item.id);
                item.prefab = (GameObject)EditorGUILayout.ObjectField("Item Prefab", item.prefab, typeof(GameObject), false);
                item.hp = EditorGUILayout.FloatField("Item HP", item.hp);
                item.speed = EditorGUILayout.FloatField("Item Speed", item.speed);
                item.arrmor = EditorGUILayout.FloatField("Item Arrmor", item.arrmor); 
                item.magicArrmor = EditorGUILayout.FloatField("Item Magic Arrmor", item.magicArrmor);
                item.reward = EditorGUILayout.IntField("Item Reward", item.reward); ;
            }
            else
            {
                GUILayout.Label("This List is Empty.");
            }
        }

        if (GUI.changed && enemyBaseList)
        {
            EditorUtility.SetDirty(enemyBaseList);
        }

        EditorGUILayout.EndScrollView();

    }

    void CreateNewItemList()
    {
        viewIndex = 1;

        EnemyInfoList asset = CreateInstance<EnemyInfoList>();

        string relPath = newAssetPath.Substring("Assets".Length);

        if (File.Exists(Application.dataPath + relPath))
        {
            int i = 0;
            do
            {
                relPath = relPath.Replace(".asset", i + ".asset");
                i++;
            }
            while (File.Exists(Application.dataPath + relPath));
        }

        AssetDatabase.CreateAsset(asset, "Assets" + relPath);
        AssetDatabase.SaveAssets();

        FocusWindow();
        enemyBaseList = asset;

        if (enemyBaseList)
        {
            enemyBaseList.data = new List<EnemyInfo>();
            relPath = AssetDatabase.GetAssetPath(enemyBaseList);
            EditorPrefs.SetString(myItemPath, relPath);
        }
    }

    void OpenItemList()
    {
        string absPath = EditorUtility.OpenFilePanel("Select Item List", "", "");


        if (absPath.StartsWith(Application.dataPath))
        {
            string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);

            enemyBaseList = AssetDatabase.LoadAssetAtPath<EnemyInfoList>(relPath);
            if (enemyBaseList != null)
            {
                if (enemyBaseList.data == null)
                {
                    enemyBaseList.data = new List<EnemyInfo>();
                }
                EditorPrefs.SetString(myItemPath, relPath);

            }

        }
    }

    void AddItem()
    {
        EnemyInfo newItem = new EnemyInfo();
        if (enemyBaseList.data.Count > 0)
        {
            newItem.id = enemyBaseList.data[enemyBaseList.data.Count - 1].id + 1;
        }
        enemyBaseList.data.Add(newItem);
        viewIndex = enemyBaseList.data.Count;
    }

    void DeleteItem(int index)
    {
        enemyBaseList.data.RemoveAt(index - 1);
    }

    void FocusWindow()
    {
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = enemyBaseList;
    }
}