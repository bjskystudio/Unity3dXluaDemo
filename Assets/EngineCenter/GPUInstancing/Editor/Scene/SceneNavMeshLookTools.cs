using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class SceneNavMeshLookTools : Editor
{
    private static bool _isShow = true;
    public static void OnSceneToolGUI()
    {
        if (GUILayout.Button("一键显示NavMesh"))
        {
            SetNavLook(true);
        }
        if (GUILayout.Button("一键隐藏NavMesh"))
        {
            SetNavLook(false);
        }
        if (GUILayout.Button("导出navmesh"))
        {
            string sceneName = EditorSceneManager.GetActiveScene().name;
            Transform navRoot = Selection.activeTransform;
            if(navRoot == null)
            {
                return;
            }
            string blockName = navRoot.parent.name;
            ExportGPUScene.ExportGPUSceneTools.AnalyzeOneNav(navRoot, sceneName + "_" + blockName);
            AssetDatabase.Refresh();
        }
    }

    private static void SetNavLook(bool show)
    {
        if(_isShow == show)
        {
            return;
        }
        GameObject gpuobj = GameObject.Find(ExportGPUScene.ExportGPUSceneTools.BLOCK_ROOT_NAME);
        if(gpuobj == null)
        {
            return;
        }
        UnityEngine.AI.NavMeshSurface[] navs = gpuobj.GetComponentsInChildren<UnityEngine.AI.NavMeshSurface>(true);
        if(navs == null || navs.Length == 0)
        {
            return;
        }
        int len = navs.Length;
        for(int i = 0;i < len;i ++)
        {
            GameObject go = navs[i].gameObject;
            if(go.activeSelf != show)
            {
                go.SetActive(show);
            }
            if(go.layer != LayerUtil.LayerNavMesh)
            {
                LayerUtil.SetLayer(go, LayerUtil.LayerNavMesh);
            }
            
        }


        _isShow = show;
    }
}
