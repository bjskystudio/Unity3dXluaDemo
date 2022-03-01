using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using YoukiaCore.Utils;

public class MapEditorWindow3 : EditorWindow
{
    /// <summary>
    /// 所有节点信息
    /// </summary>
    public List<MapMachine> MapList = new List<MapMachine>();
    /// <summary>
    /// 连线信息
    /// </summary>
    public List<MapTransform> MapSTList = new List<MapTransform>(); //连线信息
    public Dictionary<int, int> MapLayerCount = new Dictionary<int, int>();  //层级信息
    private Dictionary<string, string> m_ConfigNameDescD = new Dictionary<string, string>();
    private MapMachine Main = null;
    private Vector2 _skillDescVect;
    /// <summary>
    /// 所有可扩展的配置名字
    /// </summary>
    private Dictionary<string, string> WorldDefCfg = new Dictionary<string, string>();
    /// <summary>
    /// 配置所有信息
    /// </summary>
    public static Dictionary<string, List<List<string>>> CFGFileInfoDic = new Dictionary<string, List<List<string>>>();
    private MapMachine m_startTransition;
    private Vector2 m_sp = new Vector2(0, 0);
    private const float m_rightPanelW = 318;
    private int m_scrollViewX = 0;
    private int m_scrollViewY = 0;
    public static string MapDataName = "MapData名字";
    public int ChooseWindowID; //选择窗口的id
    private MapMachine m_chooseStateMachine; //选择的窗口
    private long MapStartId;
    private int chooseWindow = -1;
    public static bool IsCreate = false;
    [MenuItem("地图编辑器/打开地图编辑器", false,1)]
    static void Open()
    {
        var Window = GetWindow<MapEditorWindow3>();
        Window.titleContent = new GUIContent(EditorGUIUtility.IconContent("TerrainInspector.TerrainToolRaise"));
        Window.titleContent.text = "地图编辑";
        Window.Init();
    }
    void OnInspectorUpdate()
    {
        Repaint();
    }  
    public void Init()
    {
        Main = null;
        MapSTList.Clear();
        WorldDefCfg.Clear();
        MapList.Clear();
        MapLayerCount.Clear();
        CFGFileInfoDic.Clear();
        m_ConfigNameDescD.Clear();
        WorldDefCfg = MapTools.GetWordType();
    }
    void OnGUI()
    {
        m_sp = GUI.BeginScrollView(new Rect(0, 0, position.width - m_rightPanelW, position.height), m_sp, new Rect(0, 0, m_scrollViewX, m_scrollViewY));
        CheckFoucsWindow();
        ShowStateTransitions();
        DrawStateMachine();
        GUI.EndScrollView();
        ShowTopBarBtnAndLab();
        DrawRightGroup();
    }
    /// <summary>
    /// 上方的信息
    /// </summary>
    private void ShowTopBarBtnAndLab()
    {
        GUILayout.BeginHorizontal("toolbarbutton");

        if (GUILayout.Button("选择地图", "GV Gizmo DropDown", GUILayout.MaxWidth(100)))
        {
            var t = GetWindow<MapEditorWindow4>();
            t.titleContent = new GUIContent(EditorGUIUtility.IconContent("TerrainInspector.TerrainToolRaise"));
            t.titleContent.text = "选择地图";
            List<string> ids = new List<string>();
            var cfg = GetCfgDataByType("MapConfig");
            for (int i = 0; i < cfg.Count; i++)
            {
                ids.Add(cfg[i][0] + ":" + cfg[i][3]);
            }
            t.Init(ids, index =>
            {
                ChooseMap(index);
            });
        }
        GUILayout.Label("MapData：", GUILayout.MaxWidth(60));
        MapDataName = EditorGUILayout.TextField("", MapDataName, GUILayout.MaxWidth(100));
        if (Main!=null)
        {
            Main.m_mineCfgStr[4] = MapDataName;
        }
        if (GUILayout.Button("创建地图", "toolbarbutton", GUILayout.MaxWidth(100)))
        {
            IsCreate = true;
            MapDataName = "MapData名字";
            CreateNewStateMachine(new Vector2(100, 100), "MapConfig");
        }
        if (GUILayout.Button("预览编辑", "toolbarbutton", GUILayout.MaxWidth(100)))
        {
            LoadModel();
        }
        if (GUILayout.Button("保存地图", "toolbarbutton", GUILayout.MaxWidth(100)))
        {
            if (string.IsNullOrEmpty(MapDataName))
            {
                EditorUtility.DisplayDialog("错误", "请输入MapData名字", "确定");
                return;
            }
            for (int i = 0; i < MapList.Count; i++)
            {
                MapList[i].SetPos();
            }
            if (MapTools.GetConfigFilePath(MapDataName) != null)
            {
                if (EditorUtility.DisplayDialog("提示", "已经存在相同MapData本次保存会覆盖", "确定", "取消")) 
                {
                    foreach (var VARIABLE in CFGFileInfoDic)
                    {
                        if (VARIABLE.Key != "MapDataTemp")
                        {
                            SaveCfgFileInfo(VARIABLE.Key, VARIABLE.Value);
                        }
                    }
                    AssetDatabase.Refresh();
                }

            }
            else
            {
                foreach (var VARIABLE in CFGFileInfoDic)
                {
                    if (VARIABLE.Key == "MapDataTemp")
                    {
                        var des = File.ReadAllText(Application.dataPath + "/MapEditor/Editor/MapEditor/MapDataTemp.txt");
                        var p = Application.dataPath + "/TempRes/TxtConfig/World/AllSceneConfig/" + MapDataName + ".txt";
                        File.WriteAllText(p, des, Encoding.UTF8);
                        SaveCfgFileInfo(MapDataName, VARIABLE.Value);
                    }
                    else
                    {
                        SaveCfgFileInfo(VARIABLE.Key, VARIABLE.Value);
                    }
                }
                EditorUtility.DisplayDialog("成功", "创建成功", "确定");
                AssetDatabase.Refresh();
            }
         

        }
        if (GUILayout.Button("显示寻路网格", "toolbarbutton", GUILayout.MaxWidth(100)))
        {
            GameObject obj = GameObject.Find("MapEditor");
            if (obj!=null)
            {
                MeshRenderer nav = null;
                foreach (Transform child in obj.transform)
                {
                    if (nav != null) break;
                    var ch = child.GetComponentsInChildren<Transform>();
                    foreach (var VARIABLE in ch)
                    {
                        if (VARIABLE.name.Contains("mesh"))
                        {
                            nav = VARIABLE.GetComponent<MeshRenderer>();
                            break;
                        }
                        
                    }
                }

                if (nav!=null)
                {
                    nav.enabled = true;
                }
                else
                {
                    EditorUtility.DisplayDialog("错误", "没有找到对应网格可能是命名错误或者没有烘焙网格", "确定");
                }
            }
            else
            {
                EditorUtility.DisplayDialog("错误", "请先加载场景", "确定");
            }
            
        }
        if (GUILayout.Button("重置Y轴", "toolbarbutton", GUILayout.MaxWidth(100)))
        {   

        }
        GUILayout.EndHorizontal();
    }

    private void ChooseMap(int index)
    {
        IsCreate = false;
        Init();
        var scene = GetCfgDataByType("MapConfig")[index];
        MapDataName = scene[4];
        var mainState = AddStateMachine(new Vector2(100, 100), "MapConfig", scene, 0);
        mainState.IsMain = true;
        var mapdata = GetCfgDataByType(scene[GetRow(GetCfgDataByType("MapConfig"), "MapData")]);
        m_startTransition = mainState;
        for (int i = 3; i < mapdata.Count; i++)
        {
            string id = mapdata[i][0];
            string type = mapdata[i][1];
            if (!WorldDefCfg.ContainsKey(type))
            {
                Debug.LogError("有一个不存在的类型：" + type);
            }
            string cfg = WorldDefCfg[type];
            var test = GetConfgBySid(cfg.Split('#')[1], id);
            if (test is null)
            {
                Debug.LogError("配置表：" + cfg.Split('#')[1] + "不存在ID：" + id);
                continue;
            }
            var bind =  ChooseBattleMapRT(cfg, "MapData", m_startTransition.m_myIndexLayer, test);
           SetBindMap(bind, MapDataName, id);
        }
        LoadModel();
    }

    private void SetBindMap(MapMachine it, string cfg , string id)
    {
       var t =  GetCfgDataByType(cfg);
       for (int i = 3; i < t.Count; i++)
       {
           if (t[i][0] == id)
           {
               it.SetMapDataConfig(t[i], MapDataName);
               break;
           }
       }
    }
    private List<string> GetConfgBySid(string type,string sid)
    {
        var c = GetCfgDataByType(type);
        for (int i = 3; i < c.Count; i++)
        {
            if (c[i][0]== sid)
            {
                return c[i];
            }
        }
        return null;
    }
    public int GetRow(List<List<string>>cfg,string name)
    {
        for (int i = 0; i < cfg[2].Count; i++)
        {
            if (cfg[2][i] == name)
            {
                return i;
            }
        }
        return 0;
    }
    public void SaveCfgFileInfo(string name, List<List<string>> cfgFileInfo)
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

    /// <summary>
    /// 最左边信息
    /// </summary>
    private void DrawRightGroup()
    {
        if (m_chooseStateMachine == null) return;
        GUILayout.BeginArea(new Rect(position.width - m_rightPanelW, 30, m_rightPanelW, position.height - 35));
        GUI.Box(new Rect(0, 0, 320, position.height), "详情");
        GUILayout.Space(30);
        GUILayout.BeginVertical();
        _skillDescVect = EditorGUILayout.BeginScrollView(_skillDescVect);
        ShowMapDesc();
        EditorGUILayout.EndScrollView();

        GUILayout.EndVertical();
        if (m_chooseStateMachine != null && !string.IsNullOrEmpty(m_chooseStateMachine.m_classDesc))
        {
            GUILayout.Space(20);
            GUILayout.TextArea(m_chooseStateMachine.m_classDesc);
        }
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

    private void ShowMapDesc()
    {
        if (m_chooseStateMachine == null) return;
        if (m_chooseStateMachine.m_mineCfgStr == null) return;
        List<List<string>> chooseConfigStrs = GetCfgDataByType(m_chooseStateMachine.m_type);
        for (int i = 0; i < m_chooseStateMachine.m_mineCfgStr.Count; i++)
        {
            string tittle = chooseConfigStrs[1][i].Split('：')[0];
            var field = chooseConfigStrs[2][i];
            if (m_chooseStateMachine.IsMain)
            {
                if (field!= "MapData")
                {
                    m_chooseStateMachine.m_mineCfgStr[i] = EditorData(m_chooseStateMachine.m_mineCfgStr[i], tittle);
                }
            }
            else
            {
                m_chooseStateMachine.m_mineCfgStr[i] = EditorData(m_chooseStateMachine.m_mineCfgStr[i], tittle);
            }
        }
        GUILayout.Label("");
        GUILayout.BeginHorizontal();
        GUILayout.Label("");
        GUILayout.Label(MapDataName);
        GUILayout.EndHorizontal();
        if (!m_chooseStateMachine.IsMain && m_chooseStateMachine.m_mineMapData!=null)
        {
            string name = IsCreate ? "MapDataTemp" : MapDataName;
            chooseConfigStrs = GetCfgDataByType(name);
            for (int i = 0; i < m_chooseStateMachine.m_mineMapData.Count; i++)
            {
                string tittle = chooseConfigStrs[1][i].Split('：')[0];
                var field = chooseConfigStrs[2][i];
                if (field.ToLower()!="sid")
                {
                    if (field.ToLower() == "type")
                    {
                        GUILayout.Label("当前类型："+ m_chooseStateMachine.m_mineMapData[i]);
                    }
                    else
                    {
                        bool isbool = chooseConfigStrs[0][i] == "bool";
                        bool isPos = false;
                        if (tittle.Contains("位置") || tittle.Contains("旋转") || tittle.Contains("缩放"))
                        {
                            isPos = true;
                        }
                        m_chooseStateMachine.m_mineMapData[i] = EditorData(m_chooseStateMachine.m_mineMapData[i], tittle, isbool, isPos);
                    }
                   
                }
            }
            if (GUILayout.Button("贴合Y轴"))
            {
                if (m_chooseStateMachine.BindObj!=null)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(m_chooseStateMachine.BindObj.transform.position, -Vector3.up, out hit))
                    {
                        if (hit.collider.name.Contains("nav"))
                        {
                            Vector3 t3 = new Vector3(m_chooseStateMachine.BindObj.transform.position.x, hit.point.y+0.01f, m_chooseStateMachine.BindObj.transform.position.z);
                            m_chooseStateMachine.BindObj.transform.position = t3;
                        }
                        else
                        {
                            Debug.LogError("没有找到地板");
                        }
                    }
                }
            }
        }
    }
    private string EditorData(string data,  string tittle, bool isBool= false,bool IsPos =false)
    {
        if (string.IsNullOrEmpty(data))
            data = "";
        if (isBool)
        {
            bool bdata = data == "1" || data.ToLower() == "true";
            bdata = EditorGUILayout.ToggleLeft(tittle, bdata);
            if (data.ToLower() != "true" && data.ToLower() != "false")
                data = bdata ? "1" : "0";
            else
                data = bdata ? "TRUE" : "FALSE";
        }
        else if (IsPos)
        {
            //Vector3 pos = Vector3.one;
            //if (!string.IsNullOrEmpty(data))
            //{
            //    var t = data.Split('#');
            //    pos = new Vector3(t[0].ToFloat(), t[1].ToFloat(), t[2].ToFloat());
            //}
            //pos = EditorGUILayout.Vector3Field(tittle, pos);
            //data = pos.x + "#" + pos.y + "#" + pos.z;
            //if (m_chooseStateMachine!=null && m_chooseStateMachine.BindObj!=null)
            //{
            //    //if (tittle=="位置")
            //    //{
            //    //    m_chooseStateMachine.BindObj.transform.position = pos;
            //    //}
            //    //if (tittle == "旋转")
            //    //{
            //    //    m_chooseStateMachine.BindObj.transform.eulerAngles = pos;
            //    //}
            //    //if (tittle == "缩放")
            //    //{
            //    //    m_chooseStateMachine.BindObj.transform.localScale = pos;
            //    //}
            //}
        }
        else
        {
            data = EditorGUILayout.TextField(tittle, data);
        }
        return data;
    }
    /// <summary>
    /// 检查绘制
    /// </summary>
    private void CheckFoucsWindow()
    {
        bool m_isClickRelSlider = false;
        if (Event.current.button == 1 && Event.current.isMouse)
        {
            chooseWindow = isCollsion(Event.current.mousePosition, out m_isClickRelSlider);
            if (chooseWindow > -1 && !m_isClickRelSlider)
            {
                if (MapList == null) return;
                GenericMenu genericMenu = new GenericMenu();
                genericMenu.AddItem(new GUIContent("删除"), false, delegate () { ChooseMapAllRelInfo(MapList[chooseWindow], true); });
                genericMenu.ShowAsContext();
            }
        }
        else if (Event.current.button == 0 && Event.current.isMouse)
        {
            if (Event.current.type == EventType.MouseDown)
            {
                chooseWindow = isCollsion(Event.current.mousePosition, out m_isClickRelSlider);
                if (chooseWindow > -1)
                {
                    ChooseWindowID = chooseWindow;
                    if (MapList == null) return;
                    if (!m_isClickRelSlider)
                        SetChooseStateMachine(MapList[chooseWindow]);
                }
            }
        }
    }
    /// <summary>
    /// 删除链接
    /// </summary>
    /// <param name="chooseSSM"></param>
    /// <param name="destoryMain"></param>
    private void ChooseMapAllRelInfo(MapMachine chooseSSM, bool destoryMain = false)
    {
        List<MapMachine> ssmList = GetSSMByMapSTL(chooseSSM);
        for (int i = 0; i < ssmList.Count; i++)
        {
            DeleteMapState(ssmList[i]);
        }
        if (destoryMain == true)
        {
            DeleteMapState(chooseSSM);
            m_chooseStateMachine = null;
        }
        DeleteCfg(chooseSSM);
        //删除其后链接
        foreach (var item in ssmList)
        {
            DeleteCfg(item);
        }
    }
    private void DeleteCfg(MapMachine chooseSSM)
    {
        if (chooseSSM.m_mineCfgStr != null && chooseSSM.m_mineCfgStr.Count > 1)
        {
            if (!chooseSSM.IsMain && chooseSSM.m_mineMapData!=null)
            {
                List<List<string>> map = new List<List<string>>();
                if (MapTools.GetConfigFilePath(MapDataName) != null)
                {
                    map = GetCfgDataByType(MapDataName);
                }
                else
                {
                    string name = IsCreate ? "MapDataTemp" : MapDataName;
                    map = GetCfgDataByType(name);
                }
                for (int i = map.Count - 1; i >= 0; i--)
                {
                    if (map[i][0] == chooseSSM.m_mineMapData[0])
                    {
                        map.RemoveAt(i);
                    }
                }
                SetCfgDataByType(MapDataName, map);
            }
            List<List<string>> chooseConfigStrs = GetCfgDataByType(chooseSSM.m_type);
            for (int i = chooseConfigStrs.Count - 1; i >= 0; i--)
            {
                if (chooseConfigStrs[i][0] == chooseSSM.m_mineCfgStr[0])
                {
                    chooseConfigStrs.RemoveAt(i);
                }
            }
            SetCfgDataByType(chooseSSM.m_type, chooseConfigStrs);
        }
    }

    private void LoadModel()
    {
        GameObject obj = GameObject.Find("MapEditor");
        if (obj != null) GameObject.DestroyImmediate(obj);
        MapMachine main = null;
        List<MapMachine> other = new List<MapMachine>();
        for (int i = 0; i < MapList.Count; i++)
        {
            if (MapList[i].IsMain)
            {
                main = MapList[i];
            }
            else
            {
                other.Add(MapList[i]);
            }
        }
        string path = main.m_mineCfgStr[2];
        var load = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Res/" + path + ".prefab");
        if (load != null)
        {
            var s = GameObject.Instantiate(load);
            s.name = "MapEditor";
            GameObject item = new GameObject("ItemRoot");
            item.transform.SetParent(s.transform, false);

        }
        else
        {
            Debug.LogError("不存在的场景路径：" + path);
            GameObject s = new GameObject("MapEditor");
            GameObject item = new GameObject("ItemRoot");
            item.transform.SetParent(s.transform, false);
        }

        for (int i = 0; i < other.Count; i++)
        {
            other[i].LoadModel();
        }
    }
    private void DeleteMapState(MapMachine ssm)
    {
        for (int i = MapSTList.Count - 1; i >= 0; i--)
        {
            if (MapSTList[i].m_start == ssm)
            {
                MapSTList.RemoveAt(i);
            }
            else if (MapSTList[i].m_end == ssm)
            {
                MapSTList.RemoveAt(i);
            }
        }
        MapList.Remove(ssm);
    }
    private List<MapMachine> GetSSMByMapSTL(MapMachine chooseSSM)
    {
        List<MapMachine> ssmList = new List<MapMachine>();
        for (int i = 0; i < MapSTList.Count; i++)
        {
            if (MapSTList[i].m_start == chooseSSM)
            {
                ssmList.Add(MapSTList[i].m_end);
                List<MapMachine> new_ssmList = GetSSMByMapSTL(MapSTList[i].m_end);
                ssmList.AddRange(new_ssmList);
            }
        }
        return ssmList;
    }
    public int isCollsion(Vector2 mousePos, out bool isClickRelSlider)
    {
        isClickRelSlider = false;
        if (MapList == null) return -1;
        for (int i = 0; i < MapList.Count; i++)
        {
            Rect windowRect = MapList[i].m_myRect;
            if (mousePos.x >= windowRect.x && mousePos.x <= windowRect.x + windowRect.width && mousePos.y >= windowRect.y && mousePos.y <= windowRect.y + windowRect.height)
            {
                return i;
            }
        }
        return -1;
    }
    /// <summary>
    /// 绘制节点
    /// </summary>
    private void DrawStateMachine()
    {
        BeginWindows();
        for (int i = 0; i < MapList.Count; i++)
        {
            MapMachine skillSM = MapList[i];
            GUIStyle style = "flow node 1";
            if (ChooseWindowID == i || skillSM.ENABLE)
                style = "flow node 5";
            MapList[i].m_classDesc = MapList[i].m_type;
            
            MapList[i].m_myRect = GUI.Window(i, MapList[i].m_myRect, ShowMapStateWindow, MapList[i].m_Title, style);
            CalculationMaxXAndY(skillSM);
        }
        EndWindows();
    }
    private void CalculationMaxXAndY(MapMachine SSM)
    {
        if (MapList != null)
            if (SSM.m_myRect.x + SSM.m_myRect.width > m_scrollViewX) m_scrollViewX = (int)SSM.m_myRect.x + (int)SSM.m_myRect.width;
        if (SSM.m_myRect.y + SSM.m_myRect.height > m_scrollViewY) m_scrollViewY = (int)SSM.m_myRect.y + (int)SSM.m_myRect.height;
    }

    private void ShowMapStateWindow(int winID)
    {
        var State = MapList[winID];
        int startX = 0;
        int startY = 30;
        int width = 120;
        int height = 25;
        GUI.Label(new Rect(startX, startY, State.m_myRect.width, height), State.m_type);
        startY += 15;
        GUI.Label(new Rect(startX, startY, State.m_myRect.width, height), State.MapID);
        startY += 15;
        if (State.Father != null)
        {
            GUI.Label(new Rect(startX, startY, State.m_myRect.width, height), "上级引用ID:" + State.Father.MapID);
            startY += 25;
        }
        if (State.m_fieldWithRelTypsDic != null)
        {
            foreach (var item in State.m_fieldWithRelTypsDic.Keys)
            {
                string[] relTypes = WorldDefCfg.Values.ToArray();
                GUI.Label(new Rect(startX, startY, width, height), State.m_fieldWithDescDic[item]);
                State.AddFieldYPos(item, startY);
                if (GUI.Button(new Rect(State.m_myRect.width - 25, startY, 20, 20), "+"))
                {
                    var t = MapTools.GetWordType();
                    
                    m_startTransition = State;
                    GenericMenu genericMenu = new GenericMenu();
                    for (int i = 0; i < relTypes.Length; i++)
                    {
                        string nowType = relTypes[i];
                        string nowTypeDesc = nowType;
                        m_ConfigNameDescD.TryGetValue(nowType, out nowTypeDesc);
                        genericMenu.AddItem(new GUIContent("(" + nowType + ")" + nowTypeDesc), false, delegate()
                        {
                            ChooseBattleMapRT(nowType, item, m_startTransition.m_myIndexLayer);
                        });
                    }
                    genericMenu.ShowAsContext();
                }

                startY += height;
            }
        }
        State.m_myRect.height = startY + 10;
        GUI.DragWindow();
    }
    /// <summary>
    /// 创建类型
    /// </summary>
    /// <param name="type"></param>
    /// <param name="mousePos"></param>
    /// <param name="fieldName"></param>
    /// <param name="layer"></param>
    /// <param name="skillStrs"></param>
    /// <returns></returns>
    private MapMachine ChooseBattleMapRT(string type, string fieldName, int layer, List<string> skillStrs = null)
    {
        if (!type.Contains("#"))
        {
            Debug.LogError("创建类型失败，检查WorldDefine.lua注释是不是正确");
            return null;
        }
        type = type.Split('#')[1];

        Vector2 statePos = Vector2.zero;
        if (MapSTList.Count > 0)
        {
            var t = MapSTList[MapSTList.Count - 1];
            statePos = new Vector2(t.m_end.m_myPos.x, t.m_end.m_myPos.y + 100);
        }
        else
        {
            statePos = new Vector2(m_startTransition.m_myPos.x + 320, 50);
        }
        MapMachine m_endTransition = AddStateMachine(statePos, type, skillStrs, layer + 1);
        m_endTransition.m_Title = m_endTransition.m_type;
        foreach (var VARIABLE in WorldDefCfg)
        {
            if (VARIABLE.Value.Contains(m_endTransition.m_type))
            {
                m_endTransition.m_Title = VARIABLE.Value.Split('#')[0];
                break;
            }
        }
        m_endTransition.MyRect(new Rect(statePos.x, statePos.y, 180, 200));
        AddTransition(m_startTransition, m_endTransition, fieldName);
        return m_endTransition;
    }
    private void AddTransition(MapMachine start, MapMachine end, string fieldName = "", int indexRelNum = 0)
    {
        end.Father = start;
        start.Child.Add(end);
        var sst = new MapTransform(start, end, fieldName, indexRelNum);
        MapSTList.Add(sst);
    }
    /// <summary>
    /// 绘制连线信息
    /// </summary>
    private void ShowStateTransitions()
    {
        for (int i = 0; i < MapSTList.Count; i++)
        {
            List<int> YL = null;
            int fieldYPosIndex = MapSTList[i].m_fieldIndex;
            if (MapSTList[i].m_start.m_fieldYPosInWindowDic.TryGetValue(MapSTList[i].m_fieldName, out YL))
            {
                if (fieldYPosIndex >= YL.Count)
                    fieldYPosIndex = YL.Count - 1;
                DrawNodeCurve(MapSTList[i].m_start.m_myRect, MapSTList[i].m_end.m_myRect, YL[fieldYPosIndex]);
            }
        }
    }

    public static string GetId(string name)
    {
        string id = "1";
        if (!string.IsNullOrEmpty(name))
        {
            var t = MapTools.GetCfgFileInfo(name);
            List<long> ids = new List<long>();
            for (int i = 3; i < t.Count; i++)
            {
                ids.Add(t[i][0].ToLong());
            }
            ids = ids.OrderByDescending(l => l).ToList();
            if (ids.Count > 0)
            {
                id = (ids[0] + 1).ToString();
            }
            else
            {
                id = "1";
            }
        }
        return id;
    }
    private void DrawNodeCurve(Rect start, Rect end, int posY)
    {
        Vector3 startPos = new Vector3(start.x + start.width, start.y + posY + 15, 0);
        Vector3 endPos = new Vector3(end.x, end.y + end.height / 2, 0);
        Vector3 startTan = startPos + Vector3.right * 50;
        Vector3 endTan = endPos + Vector3.left * 50;
        Color shadowCol = Color.green;
        for (int i = 0; i < 3; i++)
            Handles.DrawBezier(startPos, endPos, startTan, endTan, shadowCol, null, 1);
        Handles.DrawBezier(startPos, endPos, startTan, endTan, shadowCol, null, 1);
    }

    public static List<List<string>> GetCfgDataByType(string name)
    {
        if (name == "MapDataTemp")
        {
            if (!CFGFileInfoDic.ContainsKey(name))
            {
                var s = Application.dataPath + "/MapEditor/Editor/MapEditor/MapDataTemp.txt";
                CFGFileInfoDic.Add(name, MapTools.GetCfgFilePathInfo(s));
            }
        }
        if (!CFGFileInfoDic.ContainsKey(name))
        {
            CFGFileInfoDic.Add(name, MapTools.GetCfgFileInfo(name));
        }
        return CFGFileInfoDic[name];
    }
    public void CreateNewStateMachine(Vector2 mousePos, string type)
    {
        Init();
        var m = AddStateMachine(mousePos, type);
        m.m_Title = type;
        m.IsMain = true;
        Main = m;
    }

    private MapMachine AddStateMachine(Vector2 currentMousePos, string type, List<string> skillStrs = null, int indexLayer = 0)
    {
        MapMachine item = new MapMachine();
        item.SetData(currentMousePos, skillStrs, type, indexLayer);
        MapList.Add(item);
        if (MapLayerCount.ContainsKey(item.m_myIndexLayer)) MapLayerCount[item.m_myIndexLayer]++;
        item.MyRect(new Rect(item.m_myPos.x, item.m_myPos.y, 180, 200));
        InitCheckHasMineRelationalTypes(item);
        m_chooseStateMachine = item;
        ChooseWindowID = MapList.Count - 1;
        if (ChooseWindowID == -1) ChooseWindowID = 0;
        SetChooseStateMachine(MapList[ChooseWindowID]);
        if (skillStrs==null)
        {
            if (!MapLayerCount.ContainsKey(item.m_myIndexLayer))
            {
                MapLayerCount.Add(item.m_myIndexLayer, GetMaxMapLayerCount(item.m_myIndexLayer) + 1);
            }
            CreateNewMapData(m_chooseStateMachine, null, m_chooseStateMachine.m_myIndexLayer);
        }
        return item;
    }

    private void CreateNewMapData(MapMachine it, string[] skillStrs = null, int layer = 0)
    {
        List<List<string>> chooseConfigStrs = GetCfgDataByType(it.m_type);
        if (skillStrs == null)
        {
            string[] newStrs = new string[chooseConfigStrs[0].Count];
            newStrs[0] = CreateNewMapID(it.m_type);
            for (int i = 1; i < newStrs.Length; i++)
            {
                newStrs[i] = "";
            }
            if (it.m_type == "MapConfig")
            {
                newStrs[4] = MapDataName;
            }
            chooseConfigStrs.Add(newStrs.ToList());
            if (it.m_type != "MapConfig")
            {
                string name = IsCreate ? "MapDataTemp" : MapDataName;
                List <List<string>> MapData = GetCfgDataByType(name);
                string[] info = new string[MapData[0].Count];
                info[0] = newStrs[0];
                info[1] = GetTypeName(it.m_type);
                info[2] = "1#1#1";
                info[3] = "1#1#1";
                info[4] = "1#1#1";
                info[5] = "false";
                MapData.Add(info.ToList());
                SetCfgDataByType("MapDataTemp", MapData);
                it.SetMapDataConfig(MapData[MapData.Count - 1],"");
            }
        }
        else
        {
            skillStrs[0] = CreateNewMapID(it.m_type);
            if (layer == 0)
                skillStrs[0] = MapList[0].MapID;
            chooseConfigStrs.Add(skillStrs.ToList());
        }
        SetCfgDataByType(it.m_type, chooseConfigStrs);
        it.SetConfig(chooseConfigStrs[chooseConfigStrs.Count - 1]);
    }

    private string GetTypeName(string str)
    {
        foreach (var VARIABLE in WorldDefCfg)
        {
            if (VARIABLE.Value.Contains(str))
            {
                return VARIABLE.Key;
            }
        }
        return "";
    }
    private void SetCfgDataByType(string t, List<List<string>> strs)
    {
        if (!CFGFileInfoDic.ContainsKey(t))
        {
            CFGFileInfoDic.Add(t, MapTools.GetCfgFileInfo(t));
        }
        CFGFileInfoDic[t] = strs;
    }
    private string CreateNewMapID(string name)
    {
       return GetId(name);
    }
    private int GetMaxMapLayerCount(int layer)
    {
        if (layer == 0)
        {
            return 0;
        }
        int maxMapLayerCount = 0;
        int length = "{0}{1:D2}".format(MapStartId, layer).Length;
        for (int i = 0; i < MapList.Count; i++)
        {
            if (MapList[i].m_myIndexLayer == layer)
            {
                if (MapList[i].MapID.Length > length)
                {
                    int count = 0;
                    int.TryParse(MapList[i].MapID.Substring(length), out count);
                    if (maxMapLayerCount < count)
                    {
                        maxMapLayerCount = count;
                    }
                }
            }
        }
        return maxMapLayerCount;
    }
    private void SetChooseStateMachine(MapMachine it)
    {
        if (MapList == null) return;
        m_chooseStateMachine = it;
        m_chooseStateMachine.UpdateMapID();
        GUI.FocusControl(null);
        Repaint();
        if (it.BindObj!=null)
        {
            EditorGUIUtility.PingObject(it.BindObj);
            Selection.activeGameObject = it.BindObj;
            SceneView.lastActiveSceneView.FrameSelected();
            Quaternion sceneViewQua = Quaternion.Euler(new Vector3(15, 90, 0));
            SceneView.lastActiveSceneView.LookAt(it.BindObj.transform.position, sceneViewQua, 3.5f);

        }
    }
    private void InitCheckHasMineRelationalTypes(MapMachine stateMachine)
    {
        List<List<string>> configStrs = MapTools.GetCfgFileInfo(stateMachine.m_type);
        string[] fieldBaseTypes = configStrs[0].ToArray();
        for (int i = 0; i < configStrs[0].Count; i++)
        {
            string cfgStr = configStrs[2][i];
            if (cfgStr == "MapData")
            {
                stateMachine.AddFieldRelTypes(configStrs[2][i], new string[] { fieldBaseTypes[i] }, configStrs[1][i]);
            }
        }
    }

    void OnDestroy()
    {
        GameObject obj = GameObject.Find("MapEditor");
        if (obj != null) GameObject.DestroyImmediate(obj);
    }

}
