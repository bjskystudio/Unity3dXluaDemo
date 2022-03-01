using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class MapSelectWindow : EditorWindow
{
    private List<List<string>> SceneCfg;
    private int MapRow;
    private string DataName;
    public void OnGUI()
    {
        if (SceneCfg == null) return;
        GUILayout.Label("");
        if (GUILayout.Button("一键保存地图数据"))
        {
            Transform child = GameObject.Find("MapEditor/ItemRoot").transform;
            for (int i = 0; i < child.childCount; i++)
            {
                Transform ch = child.GetChild(i);
                int row = MapEditorWindow2.GetBySidRow(DataName, ch.name.Split('#')[0]);
                var cfg = MapEditorWindow2.GetCfgDataByType(DataName);
                cfg[row][2] = ch.position.x + "#" + ch.position.y + "#" + ch.position.z;
                cfg[row][3] = ch.eulerAngles.x + "#" + ch.eulerAngles.y + "#" + ch.eulerAngles.z;
                cfg[row][4] = ch.localScale.x + "#" + ch.localScale.y + "#" + ch.localScale.z;
            }
            foreach (var VARIABLE in MapEditorWindow2.CFGFileInfoDic)
            {
                SaveCfgFileInfo(VARIABLE.Key, VARIABLE.Value);
            }
            EditorUtility.DisplayDialog("保存成功", "一键保存成功", "确定");
            MapEditorWindow2.Window?.Init();
        //    Close();
        }
        GUILayout.Label("当前MapData:" + DataName);
        GUILayout.Label("");
        if (Selection.activeObject!=null && Selection.activeObject.name== "MapEditor")
        {
            List<string> des = SceneCfg[1];
            List<string> config = SceneCfg[MapRow];
            GUILayout.BeginVertical("GroupBox");
            for (int i = 0; i < config.Count; i++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(des[i],GUILayout.Width(80));
                SceneCfg[MapRow][i] = GUILayout.TextField(config[i],GUILayout.Width(320));
                GUILayout.EndHorizontal();
                GUILayout.Label("");
            }
            GUILayout.EndVertical();
        }else if (Selection.activeObject != null && Selection.activeObject is GameObject obj && obj.transform.parent!=null && obj.transform.parent.name== "ItemRoot")
        {
            var sid = Selection.activeObject.name.Split('#');
            if (sid[1]=="0")
            {
                GUILayout.Label("当前选中item类型为0：" + sid[0]);
            }
            else
            {
                var cfg = MapEditorWindow2.GetCfgDataByType(MapEditorWindow2.TypeConfig[sid[1]]);
                List<string> des = cfg[1];
                int row = MapEditorWindow2.GetBySidRow(MapEditorWindow2.TypeConfig[sid[1]], sid[0]);
                List<string> config = cfg[row];
                GUILayout.BeginVertical("GroupBox");
                for (int i = 0; i < config.Count; i++)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label(des[i], GUILayout.Width(80));
                    config[i] = GUILayout.TextField(config[i], GUILayout.Width(320));
                    GUILayout.EndHorizontal();
                    GUILayout.Label("");
                }
                GUILayout.EndVertical();
            }
        }
        else
        {
            GUILayout.Label("请选中场景,或者ItemRoot节点下的模型");
        }
    }


    public  void SaveCfgFileInfo(string name, List<List<string>> cfgFileInfo)
    {
        string filePath = MapEditorWindow2.GetConfigFilePath(name);
        if (filePath == null)
        {
            EditorUtility.DisplayDialog("保存失败", "未能找到相关文件路径 \nFileName :: " + name, "返回");
            return;
        }
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < cfgFileInfo.Count; i++)
        {
            for (int j = 0; j < cfgFileInfo[i].Count; j++)
            {
                sb.Append(j != cfgFileInfo[i].Count - 1
                    ? cfgFileInfo[i][j] + "\t"
                    : cfgFileInfo[i][j] + "\r\n");
            }
        }
        File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
    }

    public void Init(List<List<string>> cfg, int maprow,string dataname)
    {
        SceneCfg = cfg;
        MapRow = maprow;
        DataName = dataname;
    }
    void OnInspectorUpdate()
    {
        Repaint();
    }

}
