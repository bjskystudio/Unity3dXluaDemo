using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using YoukiaCore.Utils;

public class MapMachine
{
    /// <summary>
    /// 是否第一个模块
    /// </summary>
    public bool IsMain { get; set; }
    /// <summary>
    /// 描述
    /// </summary>
    public string Des = "";
    /// <summary>
    /// 当前选中的本条单条数据.
    /// </summary>
    public List<string> m_mineCfgStr;
    /// <summary>
    /// 当前选中的mapdata数据
    /// </summary>
    public List<string> m_mineMapData;
    /// <summary>
    /// MapData对应信息
    /// </summary>
    public string[] m_MapDataFieldBaseTypes;
    /// <summary>
    /// 每个字段的基础类型数组
    /// </summary>
    public string[] m_chooseFieldBaseTypes;
    public List<string> m_chooseStrList = new List<string>();


    /// <summary>
    /// 父节点
    /// </summary>
    public MapMachine Father;
    /// <summary>
    /// 子节点
    /// </summary>
    public List<MapMachine> Child;
    /// <summary>
    /// 所属层级
    /// </summary>
    public int m_myIndexLayer;
    /// <summary>
    /// 当前类型
    /// </summary>
    public string m_type;

    public Vector2 m_myPos;
    public Rect m_myRect;
    public string m_classDesc = "描述";

    public string m_Title = "";
    /// <summary>
    /// 描述信息
    /// </summary>
    public Dictionary<string, string> m_fieldWithDescDic = new Dictionary<string, string>();

    /// <summary>
    /// 对应的类型
    /// </summary>
    public Dictionary<string, string[]> m_fieldWithRelTypsDic = new Dictionary<string, string[]>();
    /// <summary>
    /// 对应层级信息
    /// </summary>
    public Dictionary<string, List<int>> m_fieldYPosInWindowDic = new Dictionary<string, List<int>>();
    public string MapID { get; set; }
    public GameObject BindObj;
    /// <summary>
    /// 是否被激活
    /// </summary>
    public bool ENABLE { get; set; }
    public void SetData(Vector2 myPos, List<string> Strs, string type, int indexLayer)
    {
        Child = new List<MapMachine>();
        m_myIndexLayer = indexLayer;
        m_myPos = myPos;
        m_type = type;
        m_myRect = new Rect(m_myPos.x, m_myPos.y, 210, 200);
        SetConfig(Strs);
        ClearData();
    }
    /// <summary>
    /// 添加一个字段可扩展的信息列表
    /// </summary>
    /// <param name="field">字段名</param>
    /// <param name="relTypes">可扩展的type列表</param>
    /// <param name="descField">字段的描述名</param>
    public void AddFieldRelTypes(string field, string[] relTypes, string descField = "")
    {
        m_fieldWithRelTypsDic[field] = relTypes;
        m_fieldWithDescDic[field] = descField;
    }
    public void MyRect(Rect windowRect)
    {
        m_myRect = windowRect;
    }
    public void SetConfig(List<string> mineCfgStr)
    {
       var chooseConfigStrs = MapEditorWindow3.GetCfgDataByType(m_type.Trim());
       m_mineCfgStr = mineCfgStr;
       m_chooseFieldBaseTypes = chooseConfigStrs[0].ToArray();
    }

    public void SetMapDataConfig(List<string> mineCfgStr,string name)
    {
        List<List<string>> chooseConfigStrs = null;
        string n = MapEditorWindow3.IsCreate ? "MapDataTemp" : MapEditorWindow3.MapDataName;
        chooseConfigStrs = MapEditorWindow3.GetCfgDataByType(n);
        m_mineMapData = mineCfgStr;
        m_MapDataFieldBaseTypes = chooseConfigStrs[0].ToArray();
    }
    public void AddFieldYPos(string fieldName, int posY)
    {
        m_fieldYPosInWindowDic[fieldName] = new List<int>();
        m_fieldYPosInWindowDic[fieldName].Add(posY);
    }
    private void ClearData()
    {
        m_mineMapData = null;
        m_fieldWithDescDic.Clear();
        m_fieldWithRelTypsDic.Clear();
        m_fieldYPosInWindowDic.Clear();
        Child.Clear();
    }

    public void LoadModel()
    {
        var all = MapTools.GetCfgFileInfo(m_type);

        string path = m_mineCfgStr[GetRow(all, "path")];
        var load = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Res/" + path + ".prefab");
        if (load==null)
        {
            GameObject obj = new GameObject(m_mineCfgStr[0]);
            obj.transform.SetParent(GameObject.Find("MapEditor/ItemRoot").transform, false);
            BindObj = obj;
        }
        else
        {
            GameObject obj = GameObject.Instantiate(load);
            obj.transform.SetParent(GameObject.Find("MapEditor/ItemRoot").transform, false);
            obj.name = m_mineCfgStr[0];
            BindObj = obj;
        }
        var pos = m_mineMapData[2].Split('#');
        var euler = m_mineMapData[3].Split('#');
        var scale = m_mineMapData[4].Split('#');
        if (pos.Length<3) pos = new[] { "0", "0", "0" };
        if (scale.Length<3) scale= new[] { "1", "1", "1" };
        if (euler.Length<3) euler = new[] { "0", "0", "0" };
        BindObj.transform.position = new Vector3(pos[0].ToFloat(), pos[1].ToFloat(), pos[2].ToFloat());
        BindObj.transform.eulerAngles = new Vector3(euler[0].ToFloat(), euler[1].ToFloat(), euler[2].ToFloat());
        BindObj.transform.localScale = new Vector3(scale[0].ToFloat(), scale[1].ToFloat(), scale[2].ToFloat());
    }

    public void SetPos()
    {
        if (BindObj)
        {
            m_mineMapData[2] = BindObj.transform.position.x + "#" + BindObj.transform.position.y + "#" + BindObj.transform.position.z;
            m_mineMapData[3] = BindObj.transform.eulerAngles.x + "#" + BindObj.transform.eulerAngles.y + "#" + BindObj.transform.eulerAngles.z;
            m_mineMapData[4] = BindObj.transform.localScale.x + "#" + BindObj.transform.localScale.y + "#" + BindObj.transform.localScale.z;
        }
    }
    public int GetRow(List<List<string>> cfg, string name)
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
    public void UpdateMapID()
    {
        if (m_mineCfgStr == null)
        {
            if (string.IsNullOrEmpty(MapID))
            {
                MapID = MapEditorWindow3.GetId(m_type);
            }
        }
        else
        {
            MapID = m_mineCfgStr[0];
        }
    }

}
