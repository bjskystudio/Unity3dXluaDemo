using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;


public class SceneTools
{
    [InitializeOnLoadMethod]
    static void Init()
    {
        SceneView.onSceneGUIDelegate += OnSceneGUI;
    }

    private static void OnSceneGUI(SceneView sceneView)
    {
        string currentScenePath = EditorSceneManager.GetActiveScene().path;
        if(currentScenePath.StartsWith("Assets/Code/Render/GPUInstancing/GPUInstancing_Youkia/Sample") == false)
        {
            return;
        }
        Handles.BeginGUI();
        //规定GUI显示区域
        GUILayout.BeginArea(new Rect(5, 5, 500, 100));

        GUILayout.BeginHorizontal();
        //GUI绘制一个按钮
        if (GUILayout.Button("导出"))
        {
            Export();
        }
        
        SceneNavMeshLookTools.OnSceneToolGUI();
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
        Handles.EndGUI();
    }

    /// <summary>
    /// 导出
    /// </summary>
    private static void Export()
    {
        GameObject gpuobj = GameObject.Find(ExportGPUScene.ExportGPUSceneTools.BLOCK_ROOT_NAME);
        if(gpuobj == null)
        {
            EditorUtility.DisplayDialog("警告", "找不到blockRoot节点", "确定");
            return;
        }
        string sceneName = EditorSceneManager.GetActiveScene().name;
        Transform gpuRoot = gpuobj.transform;
        ExportGPUScene.ExportGPUSceneTools.Export(sceneName, gpuRoot);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

       
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}