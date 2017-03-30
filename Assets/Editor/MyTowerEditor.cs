using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class MyTowerEditor : EditorWindow
{

    public TowerBaseList towerBaseList;
    private int viewIndex = 1;
    private int infoIndex = 1;
    private Vector2 nowPos;



    private const string myItemPath = "myTowerPath";
    private const string newAssetPath = "Assets/newTowerAsset.asset";

    [MenuItem("My Editor/My Tower Editor %&e")]
    static void Init()
    {
        GetWindow(typeof(MyTowerEditor));
    }

    void OnEnable()
    {
        if (EditorPrefs.HasKey(myItemPath))
        {

            string objectPath = EditorPrefs.GetString(myItemPath);
            towerBaseList = AssetDatabase.LoadAssetAtPath<TowerBaseList>(objectPath);
        }
    }


    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        if (towerBaseList != null)
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
        if (towerBaseList == null)
        {
            GUILayout.Label("No Select Object!!!");
        }

        GUILayout.Space(5);

        if (towerBaseList != null)
        {
            GUILayout.BeginHorizontal();

            if (towerBaseList.data.Count > 0)
            {
                if (GUILayout.Button("Previous"))
                {
                    if (viewIndex > 1)
                        viewIndex--;
                }
                GUILayout.Space(5);
                if (GUILayout.Button("Next"))
                {
                    if (viewIndex < towerBaseList.data.Count)
                    {
                        viewIndex++;
                    }
                }
                GUILayout.Space(10);
                viewIndex = Mathf.Clamp(viewIndex, 1, towerBaseList.data.Count);

            }


            GUILayout.Space(10);
            if (GUILayout.Button("Add Item"))
            {
                AddItem();
            }
            if (towerBaseList.data.Count > 0)
            {
                if (GUILayout.Button("Delete Item"))
                {
                    DeleteItem(viewIndex - 1);
                }
            }

            GUILayout.EndHorizontal();
            GUILayout.Space(10);



            if (towerBaseList.data.Count > 0)
            {
                TowerBase item = towerBaseList.data[viewIndex - 1];

                GUILayout.BeginHorizontal();
                GUILayout.Space(10);
                viewIndex = Mathf.Clamp(EditorGUILayout.IntField("Current Item", viewIndex, GUILayout.ExpandWidth(false)), 1, towerBaseList.data.Count);
                EditorGUILayout.LabelField("of   " + towerBaseList.data.Count.ToString() + "  items", "", GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal();

                item.id = EditorGUILayout.IntField("Item ID", item.id);
                item.objectName = EditorGUILayout.TextField("Item Name", item.objectName);
                item.uiImage = (Sprite)EditorGUILayout.ObjectField("Item UI Image", item.uiImage, typeof(Sprite), false);
                item.attackType = (AttaclType)EditorGUILayout.EnumPopup("Item Attack Type", item.attackType);

                GUILayout.Space(25);

                if (item.info == null)
                {
                    if (GUILayout.Button("Add New Info"))
                    {

                        item.info = new List<TowerInfo>();
                    }
                }
                else
                {
                    List<TowerInfo> info = item.info;
                    GUILayout.Label("Item Info:             All: " + info.Count);

                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("Add Info"))
                    {

                        info.Add(new TowerInfo());
                    }
                    GUILayout.Space(20);
                    if (info.Count > 0)
                    {
                        infoIndex = Mathf.Clamp(EditorGUILayout.IntField("Delete Info Index:", infoIndex), 1, info.Count);

                        if (GUILayout.Button("Delete Info"))
                        {
                            info.RemoveAt(infoIndex - 1);
                        }
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.Space(10);
                    for (int i = 0; i < info.Count; i++)
                    {
                        TowerInfo infoItem = info[i];
                        GUILayout.Space(10);

                        GUILayout.Label("Item Info:  " + i);
                        infoItem.prefab = (GameObject)EditorGUILayout.ObjectField("Info " + i + " Prefab", infoItem.prefab, typeof(GameObject), false);
                        infoItem.bulletPrefab = (GameObject)EditorGUILayout.ObjectField("Info " + i + " Prefab", infoItem.bulletPrefab, typeof(GameObject), false);
                        infoItem.attack = EditorGUILayout.FloatField("Info " + i + " Attack", infoItem.attack);
                        infoItem.attackSpeed = EditorGUILayout.FloatField("Info " + i + " Attack", infoItem.attackSpeed);
                        infoItem.attackRange = EditorGUILayout.FloatField("Info " + i + " Attack Range", infoItem.attackRange);
                        infoItem.money = EditorGUILayout.IntField("Info " + i + " Money", infoItem.money);
                    }
                }
            }
            else
            {
                GUILayout.Label("This List is Empty.");
            }
        }

        if (GUI.changed && towerBaseList)
        {
            EditorUtility.SetDirty(towerBaseList);
        }

        EditorGUILayout.EndScrollView();

    }

    void CreateNewItemList()
    {
        viewIndex = 1;

        TowerBaseList asset = CreateInstance<TowerBaseList>();

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
        towerBaseList = asset;

        if (towerBaseList)
        {
            towerBaseList.data = new List<TowerBase>();
            relPath = AssetDatabase.GetAssetPath(towerBaseList);
            EditorPrefs.SetString(myItemPath, relPath);
        }
    }

    void OpenItemList()
    {
        string absPath = EditorUtility.OpenFilePanel("Select Item List", "", "");


        if (absPath.StartsWith(Application.dataPath))
        {
            string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);

            towerBaseList = AssetDatabase.LoadAssetAtPath<TowerBaseList>(relPath);
            if (towerBaseList != null)
            {
                if (towerBaseList.data == null)
                {
                    towerBaseList.data = new List<TowerBase>();
                }
                EditorPrefs.SetString(myItemPath, relPath);

            }

        }
    }

    void AddItem()
    {

        TowerBase newItem = new TowerBase();
        if (towerBaseList.data.Count > 0)
        {
            newItem.id = towerBaseList.data[towerBaseList.data.Count - 1].id + 1;
        }
        towerBaseList.data.Add(newItem);
        viewIndex = towerBaseList.data.Count;
    }

    void DeleteItem(int index)
    {
        towerBaseList.data.RemoveAt(index - 1);
    }

    void FocusWindow()
    {

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = towerBaseList;
    }
}