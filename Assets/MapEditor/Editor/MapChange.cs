using System.Collections;
using System.Collections.Generic;
using System.IO;
using Cinemachine;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using XLua;

public class MapChange : EditorWindow
{
    public string Path;
    public string Next = "Battle_Streetscape";
    public string RoleId = "1127025806749466625";
    [MenuItem("Tools/直接切换场景(徐兵)")]
    public static void OpenMapEditorWindow()
    {
        GetWindow<MapChange>();
    }
    public void OnGUI()
    {
        GUILayout.Label("主角名字(id)");
        RoleId = GUILayout.TextField(RoleId);

        GUILayout.Label("上一个场景名字");
        Next = GUILayout.TextField(Next);
        GUILayout.Label("");
        GUILayout.Label("场景路径");
       Path = GUILayout.TextField(Path);
    
       if (GUILayout.Button("切换"))
       {
           if (GameObject.Find(Next)==null)
           {
               EditorUtility.DisplayDialog("错误", "找不到上一个场景", "确定");
               return;
           }
           GameObject item = GameObject.Find(Next + "/itemParent/"+ RoleId);
           string path = Path + ".prefab";
           GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(path);
          var sc =  GameObject.Instantiate(obj);
          var c = sc.transform.Find("Camera").GetComponentInChildren<CinemachineVirtualCamera>();
          item.transform.SetParent(sc.transform,false);
            c.Follow = item.transform;
          GameObject.Find(Next).SetActive(false);
       }

    }
}
