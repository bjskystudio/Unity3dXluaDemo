using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MapDisWindow : EditorWindow
{
    private GameObject obj;
    private GameObject bObj;
    [MenuItem("地图编辑器/获得2个物体距离")]
    public static void Open()
    {
      var  Window = GetWindow<MapDisWindow>();
        Window.titleContent = new GUIContent(EditorGUIUtility.IconContent("TerrainInspector.TerrainToolRaise"));
        Window.titleContent.text = "获得距离";
    }
    void OnGUI()
    {
        GUILayout.Label("A物体");
        obj = (GameObject) EditorGUILayout.ObjectField(obj, typeof(GameObject));
        GUILayout.Label("B物体");
        bObj = (GameObject)EditorGUILayout.ObjectField(bObj, typeof(GameObject));
        if (GUILayout.Button("获得距离"))
        {
            if (obj && bObj)
            {
               var dis = Vector3.Distance(obj.transform.position, bObj.transform.position);
               EditorUtility.DisplayDialog("距离", "当前距离：" + dis, "确定");
            }
        }
    }
}
