using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Framework;
using UnityEditor;
using UnityEngine;
using YoukiaCore.Utils;

public class MapEditorWindow2 : EditorWindow
{

    public static Dictionary<string, string> TypeConfig = new Dictionary<string, string>(); //装每个类型对应的配置

    public static Dictionary<string, Dictionary<string, int>> TypeSidRow = new Dictionary<string, Dictionary<string, int>>();

    /// <summary>
    /// 保存当前配置的
    /// </summary>
    public static Dictionary<string, List<List<string>>> CFGFileInfoDic = new Dictionary<string, List<List<string>>>();
    private List<string> SelectList = new List<string>();
    private GameObject Scene;
    private GameObject ItemRoot;
    private static MapSelectWindow MapChoose;
    private List<List<string>> chooseConfigStrs = new List<List<string>>();
    public static MapEditorWindow2 Window;
    private StringBuilder DesBulider = new StringBuilder();
    private Vector2 m_sp = new Vector2(0, 0);
  //  [MenuItem("地图编辑器/选择地图")]
    public static void Open()
    {
        Window = GetWindow<MapEditorWindow2>();
        Window.titleContent = new GUIContent(EditorGUIUtility.IconContent("TerrainInspector.TerrainToolRaise"));
        Window.titleContent.text = "选择地图";
        Window.Init();
    }
    void OnInspectorUpdate()
    {
        Repaint();
    }
    public void OnGUI()
    {
        GUILayout.Label(DesBulider.ToString());
     //   m_sp = GUI.BeginScrollView(new Rect(0, 0, 500, 50), m_sp, new Rect(0, 0, Screen.width, 400));
        GUILayout.BeginVertical("GroupBox");
        for (int i = 3; i < SelectList.Count; i++)
        {
            if (GUILayout.Button(SelectList[i]))
            {
                SetClick(i);
            }
        }
        GUILayout.EndVertical();
     //   GUI.EndScrollView();
    }
    public void SetClick(int id)
    {
        if (Scene!=null)
        {
            GameObject.DestroyImmediate(Scene);
            Scene = null;
        }
        chooseConfigStrs = GetCfgDataByType("MapConfig");
        var t = chooseConfigStrs[id];
        string path = "Assets/Res/" + t[2]+ ".prefab";
        string data = t[4];
        if (string.IsNullOrEmpty(data))
        {
            EditorUtility.DisplayDialog("错误", "没有mapData数据", "确定");
            return;
        }
        GameObject temp = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        if (temp==null)
        {
            EditorUtility.DisplayDialog("错误", "不存在的路径：" + t[2], "确定");
            return;
        }
        Scene = Instantiate(temp);
        Scene.name = "MapEditor";
        ItemRoot = new GameObject("ItemRoot");
        ItemRoot.transform.SetParent(Scene.transform, false);
        Selection.activeObject = Scene;
        if (MapChoose==null)
        {
            MapChoose = GetWindow<MapSelectWindow>();
            MapChoose.titleContent = new GUIContent(EditorGUIUtility.IconContent("TerrainInspector.TerrainToolRaise"));
        }
        MapChoose.Init(chooseConfigStrs, id, data);
        MapChoose.titleContent.text = "当前场景id:"+ t[0];
        LoadMapData(data);
    }

    private void LoadMapData(string name)
    {
        InitType();
        if (ItemRoot==null)
        {
            EditorUtility.DisplayDialog("错误", "没有加载场景",  "确定");
            return;
        }
        var t = GetCfgDataByType(name);
        for (int i = 3; i < t.Count; i++)
        {
            var low = t[i];
            string sid = low[0];
            string type = low[1];
            string[] pos = low[2].Split('#');
            string[] euler = low[3].Split('#');
            string[] scale = low[4].Split('#');
            GameObject item = new GameObject(sid + "#" + type);
            item.transform.SetParent(ItemRoot.transform, false);
            item.transform.position = GetPos(pos);
            item.transform.eulerAngles = GetPos(euler);
            item.transform.localScale = GetPos(scale);
            if (type!="0")
            {
                string path = LoadPath(TypeConfig[type], sid);
                if (!string.IsNullOrEmpty(path))
                {
                    var model = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(path));
                    model.transform.SetParent(item.transform, false);
                }
                else
                {
                    Debug.LogError("当前的形象没有配置");
                }
            }
        }
    }

    public static int GetBySidRow(string name, string sid)
    {
        int index = 0;
        if (!TypeSidRow.ContainsKey(name))
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            TypeSidRow.Add(name, dic);
        }

        if (TypeSidRow[name].ContainsKey(sid))
        {
            index = TypeSidRow[name][sid];
        }
        else
        {
            var t = GetCfgDataByType(name);
            for (int i = 3; i < t.Count; i++)
            {
                if (t[i][0] == sid)
                {
                    TypeSidRow[name].Add(sid, i);
                    index = i;
                }
            }
        }

        return index;
    }

    private string LoadPath(string cfg,string id)
    {
        string path = "";
        var t = GetCfgDataByType(cfg);
        int pathIndex = 0;
        for (int i = 0; i < t[2].Count; i++)
        {
            if (t[2][i]== "path")
            {
                pathIndex = i;
                break;
            }
        }
        for (int i = 3; i < t.Count; i++)
        {
            var low = t[i];
            string sid = low[0];
            if (sid == id)
            {
                if (pathIndex!=0 && pathIndex< low.Count)
                {
                    path = low[pathIndex];
                }
            }
        }
        if (!string.IsNullOrEmpty(path))
        {
            return "Assets/Res/" + path + ".prefab";
        }
        return path;
    }

    private Vector3 GetPos(string[] vec)
    {
        if (vec==null || vec.Length<2 )
        {
            return Vector3.one;
        }

        return new Vector3(vec[0].ToFloat(), vec[1].ToFloat(), vec[2].ToFloat());
    }
    private void InitType()
    {
        DesBulider.Clear();
        TypeConfig.Clear();
        string c = File.ReadAllText(Application.dataPath + "/Res/LuaScripts/Framework/Define/WorldDefine.lua");
        int index = c.IndexOf("WorldDefine.eItemType =");
        var newc = c.Substring(index, 900);
        var ttt = newc.Substring(0, newc.IndexOf("}") + 1);
        var info = ttt.Replace("}", "").Split('\r');
        List<string> mlist = new List<string>();
        for (int i = 0; i < info.Length; i++)
        {
            if (i >= 3)
            {
                if (!string.IsNullOrEmpty(info[i])) //过滤一下
                {
                    mlist.Add(info[i]);
                }
            }
        }
        for (int i = 0; i < mlist.Count; i += 2)
        {
            var cc = mlist[i].Split('#');
            string cfg = "";
            if (cc.Length > 1) cfg = cc[1];
            if (i + 1 < mlist.Count)
            {
                var type = mlist[i + 1];
                var tt = type.Replace(",", "").Replace(" ", "").Split('=');
                if (!string.IsNullOrEmpty(cfg))
                {
                    TypeConfig.Add(tt[1], cfg);
                }
                if (cc.Length > 1)
                {
                    string des = cc[0].Replace("---", "") + "类型：" + tt[1]+"    配置："+ cfg;
                    DesBulider.Append(des);
                    DesBulider.Append("\r");
                }
            }
            
        }
    }
    void OnDestroy()
    {
        if (Scene!=null)
        {
            GameObject.DestroyImmediate(Scene);
        }
       // if (MapChoose!=null) MapChoose.Close();
    }
    public void Init()
    {
        InitType();
        CFGFileInfoDic.Clear();
        SelectList.Clear();
        List<List<string>> chooseConfigStrs = GetCfgDataByType("MapConfig");
        for (int i = 0; i < chooseConfigStrs.Count; i++)
        {
            SelectList.Add(chooseConfigStrs[i][0] + ":" + chooseConfigStrs[i][3]);
        }
    }
    public static List<List<string>> GetCfgDataByType(string name)
    {
        if (!CFGFileInfoDic.ContainsKey(name))
        {
            CFGFileInfoDic.Add(name, GetCfgFileInfo(name));
        }
        return CFGFileInfoDic[name];
    }

    public static List<List<string>> GetCfgFileInfo(string name)
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

    public static string GetConfigFilePath(string fileName)
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
