using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

public class SceneEditor : Editor
{
    [MenuItem("地图编辑器/创建跑动")]
    public static void OpenMapEditorWindow()
    {
        if (GameObject.Find("Canvas"))
        {
            GameObject.DestroyImmediate(GameObject.Find("Canvas"));
        }
        if (GameObject.Find("角色"))
        {
            GameObject.DestroyImmediate(GameObject.Find("角色"));
        }
        GameObject role = new GameObject("角色");
        role.AddComponent<NavMeshAgent>();
        var r = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Res/Actor/Role/caotijing_0/prefab/caotijing_0.prefab");
        GameObject.Instantiate(r, role.transform);
       var temp =  AssetDatabase.LoadAssetAtPath<GameObject>("Assets/MapEditor/Editor/Canvas.prefab");
       var obj = GameObject.Instantiate(temp);
       obj.name = "Canvas";
    }
    [MenuItem("地图编辑器/删除跑动")]
    public static void DelRun()
    {
        if (GameObject.Find("Canvas"))
        {
            GameObject.DestroyImmediate(GameObject.Find("Canvas"));
        }
        if (GameObject.Find("角色"))
        {
            GameObject.DestroyImmediate(GameObject.Find("角色"));
        }
    }
}
