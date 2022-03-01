using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class MapEditorCfg : EditorWindow
{
    private Dictionary<string, List<List<string>>> CFGFileInfoDic = new Dictionary<string, List<List<string>>>();
    private List<string> SelectList = new List<string>();
  //  [MenuItem("地图编辑器/创建地图编辑器")]
    public static void Open()
    {
       var t = GetWindow<MapEditorCfg>();
       t.Init();
    }

    public void OnGUI()
    {
        GUILayout.BeginVertical("GroupBox");
        for (int i = 3; i < SelectList.Count; i++)
        {
            if (GUILayout.Button(SelectList[i]))
            {
                SetClick(i);
            }
        }
        GUILayout.EndVertical();
    }

    public void SetClick(int id)
    {
        Debug.LogError("当前选择id：" + id);
    }

    public void Init()
    {
        SelectList = GetCfgKeys();
        Debug.LogError(GetConfigFilePath("MapConfig"));
    }

    public List<string> GetCfgKeys()
    {
        List<List<string>> chooseConfigStrs = GetCfgDataByType("MapConfig");
        List<string> chooseConfigKeys = new List<string>();
        for (int i = 0; i < chooseConfigStrs.Count; i++)
        {
            chooseConfigKeys.Add(chooseConfigStrs[i][0] + ":" + chooseConfigStrs[i][3]);
        }
        return chooseConfigKeys;
    }
    private List<List<string>> GetCfgDataByType(string name)
    {
        if (!CFGFileInfoDic.ContainsKey(name))
        {
            CFGFileInfoDic.Add(name, GetCfgFileInfo(name));
        }
        return CFGFileInfoDic[name];
    }

    public List<List<string>> GetCfgFileInfo(string name)
    {
        string path = GetConfigFilePath(name);
        if (path == null) return null;
        string strInfo = File.ReadAllText(path);
        string[] str = strInfo.Split(new string[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
        if (str.Length < 3)
        {
            str = strInfo.Split(new string[] {"\n"},
                StringSplitOptions.RemoveEmptyEntries);
        }
        List<List<string>> ss = new List<List<string>>();
        for (int i = 0; i < str.Length; i++)
        {
            ss.Add(str[i].Split('\t').ToList());
        }
        return ss;
    }

    public string GetConfigFilePath(string fileName)
    {
        string[] fileArr = Directory.GetFiles(Application.dataPath+ "/TempRes/TxtConfig/World", "*.txt", SearchOption.AllDirectories);
        for (int i = 0; i < fileArr.Length; i++)
        {
            if (fileArr[i].Substring(fileArr[i].LastIndexOf('\\') + 1) == fileName + ".txt")
                return fileArr[i];
        }
        return null;
    }
}
