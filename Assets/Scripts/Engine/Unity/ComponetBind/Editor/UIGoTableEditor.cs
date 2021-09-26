using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;
using UnityEngine.UI;
using UnityEditor.Experimental.SceneManagement;

#if UNITY_EDITOR
[CustomEditor(typeof(UIGoTable))]
public class UIGoTableEditor : Editor
{
    protected static GUILayoutOption miniWidth = GUILayout.Width(25f);
    protected static GUILayoutOption txtWidth = GUILayout.Width(40f);
    protected static GUILayoutOption GUIWidth1 = GUILayout.Width(60f);
    private const string APISavePath = "Assets/Res/LuaScripts/Editor/WidgeAPI/API";
    private string _curSeekText = "";
    static public bool IsAddListener = false;

    private void Awake()
    {
        if (UIGoTableEditor.IsAddListener == false)
        {
            PrefabStage.prefabStageClosing += OnPrefabStageClosing;
            // EditorApplication.hierarchyChanged += hierarchyChanged;
            Debug.Log("添加退出编辑时的自动保存");
            UIGoTableEditor.IsAddListener = true;
        }

        this.hierarchyChanged();
    }
    private void OnPrefabStageClosing(PrefabStage ps)
    {
        GameObject instance = ps.prefabContentsRoot;
        Selection.activeGameObject = instance;
        ManualInitialize();

        this.hierarchyChanged();

        string prefabPath = ps.prefabAssetPath;
        if (!string.IsNullOrEmpty(prefabPath))
        {
            bool isSuccss = false;
            PrefabUtility.SaveAsPrefabAsset(instance, prefabPath, out isSuccss);
            if (isSuccss)
            {
                Debug.Log("退出编辑时的自动保存:  " + prefabPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }

        PrefabStage.prefabStageClosing -= OnPrefabStageClosing;
        UIGoTableEditor.IsAddListener = false;
    }
    protected virtual void hierarchyChanged()
    {
        if (this.target == null) { return; }
        (this.target as UIGoTable).CheckAllStateInfo();
    }

    private void OnEnable()
    {
        _curSeekText = EditorPrefs.GetString("UIGoTableEditor._curSeekText", "");
    }

    public override void OnInspectorGUI()
    {
        var goTable = this.target as UIGoTable;

        this.StateCtrlUI(goTable);

        EditorGUILayout.Space();
        //显示搜索框
        this.FindUI(goTable);
        EditorGUILayout.Space();

        base.OnInspectorGUI();
    }

    private bool IsShowFind = false;
    private void FindUI(UIGoTable goTable)
    {
        IsShowFind = EditorGUILayout.Foldout(IsShowFind, "显示 go table 详细列表");
        GUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("保存预设体"))
            {
                ManualInitialize();
            }

            string after = EditorGUILayout.TextField("", _curSeekText, "SearchTextField");
            if (GUILayout.Button("", "SearchCancelButton", GUILayout.Width(40f)))
            {
                after = "";
                GUIUtility.keyboardControl = 0;
            }
            if (_curSeekText != after)
            {
                _curSeekText = after;
                EditorPrefs.SetString("UIGoTableEditor._curSeekText", _curSeekText);
            }
        }
        GUILayout.EndHorizontal();

        if (!IsShowFind) { return; }

        //显示搜索信息
        EditorGUI.BeginDisabledGroup(true);
        var nodeArray = goTable.GetUiNodeArray();
        if (nodeArray != null)
        {
            for (int i = 0; i < nodeArray.Length; i++)
            {
                var node = nodeArray[i];
                if (node.Obj == null)
                {
                    GUI.color = Color.red;
                    EditorGUILayout.LabelField(node.KeyName);
                    GUI.color = Color.white;
                    continue;
                }
                if (string.IsNullOrEmpty(_curSeekText) || node.Obj.name.Contains(_curSeekText))
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(string.Format("({0})", i), GUILayout.Width(32));
                    EditorGUILayout.ObjectField(node.Obj as UnityEngine.Object, typeof(UnityEngine.Object), false);
                    EditorGUILayout.EndHorizontal();
                }
            }
        }

        EditorGUI.EndDisabledGroup();
    }

    private bool showSyncSelect = false;
    private bool showAllState = true;
    private bool syncPos = true;
    private bool syncSelfPos = true;
    private bool syncRotation = true;
    private bool syncScale = true;
    private bool syncSize = true;
    private string newStateName = "";
    private void StateCtrlUI(UIGoTable goTable)
    {
        EditorGUILayout.LabelField("当前总对象数:" + goTable.GetObjCount());// , txtWidth);
        newStateName = EditorGUILayout.TextField("新状态名:", newStateName);
        if (!showSyncSelect)
        {
            GUILayout.BeginHorizontal();
            showSyncSelect = EditorGUILayout.Foldout(showSyncSelect, "同步选项");
            syncPos = EditorGUILayout.Toggle(syncPos);
            syncSelfPos = EditorGUILayout.Toggle(syncSelfPos);
            syncRotation = EditorGUILayout.Toggle(syncRotation);
            syncScale = EditorGUILayout.Toggle(syncScale);
            syncSize = EditorGUILayout.Toggle(syncSize);
            GUILayout.EndHorizontal();
        }
        else
        {
            showSyncSelect = EditorGUILayout.Foldout(showSyncSelect, "同步选项");
            if (showSyncSelect)
            {
                syncPos = EditorGUILayout.Toggle("同步子坐标", syncPos, GUIWidth1);
                syncSelfPos = EditorGUILayout.Toggle("同步坐标", syncSelfPos, GUIWidth1);
                syncRotation = EditorGUILayout.Toggle("同步旋转", syncRotation, GUIWidth1);
                syncScale = EditorGUILayout.Toggle("同步缩放", syncScale, GUIWidth1);
                syncSize = EditorGUILayout.Toggle("同步宽高", syncSize, GUIWidth1);
            }
        }
        if (GUILayout.Button("添加新状态"))
        {
            goTable.UpdateState(newStateName, syncPos, syncSelfPos, syncRotation, syncScale, syncSize);
        }

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("更新此状态:") && (goTable.NowState != null && goTable.NowState != ""))
        {
            goTable.UpdateState(goTable.NowState);
        }
        EditorGUILayout.LabelField("[ " + goTable.NowState + " ]");
        GUILayout.EndHorizontal();

        // int now_select = EditorGUILayout.Popup("选择状态", goTable.NowStateIdx, goTable.StateNameList.ToArray());
        // if ((now_select != goTable.NowStateIdx) && (goTable.StateNameList.Count > now_select))
        // {
        //     goTable.NowStateIdx = now_select;
        // }

        GUILayout.BeginHorizontal();
        showAllState = EditorGUILayout.Foldout(showAllState, "选择状态 [" + goTable.StateNameList.Count + "]");
        if (GUILayout.Button("检测"))
        {
            goTable.CheckAllStateInfo();
        }
        if (GUILayout.Button("清空"))
        {
            goTable.ClearState();
        }
        GUILayout.EndHorizontal();

        if (showAllState)
        {
            for (int i = 0; i < goTable.StateNameList.Count; i++)
            {
                GUILayout.BeginHorizontal();
                EditorGUILayout.Space();
                if (GUILayout.Button(goTable.StateNameList[i]))
                {
                    goTable.ChangeState(goTable.StateNameList[i]);
                }
                // EditorGUILayout.LabelField();
                if (GUILayout.Button("☯", miniWidth))
                {
                    goTable.UpdateState(goTable.StateNameList[i]);
                }
                if (GUILayout.Button("-", miniWidth))
                {
                    goTable.DeleteState(goTable.StateNameList[i]);
                }
                GUILayout.EndHorizontal();
            }
        }
    }

    public static bool canUpdatePrefab = true;

    private static bool _forbidUpdatePrefab = false;

    void ManualInitialize()
    {
        _forbidUpdatePrefab = false;
        if (_forbidUpdatePrefab)
        {
            return;
        }

        GameObject instance = Selection.activeGameObject;
        var gotList = instance.GetComponentsInParent<UIGoTable>();
        if (gotList != null && gotList.Length > 0)
        {
            instance = gotList[gotList.Length - 1].gameObject;
        }
        if (instance == null)
        {
            return;
        }

        var prefabStage = UnityEditor.Experimental.SceneManagement.PrefabStageUtility.GetPrefabStage(instance);
        if (prefabStage != null)
        {
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(prefabStage.scene);
        }

        var goTable = instance.GetComponent<UIGoTable>();
        if (goTable != null)
        {
            UIGoTableEditor.UpdatePrefab(instance);
            _forbidUpdatePrefab = true;

            EditorUtility.SetDirty(instance);
            AssetDatabase.SaveAssets();
        }
    }

    public static void UpdatePrefab(GameObject instance)
    {
        var goTable = instance.GetComponent<UIGoTable>();

        //判断到该预制体有UIGoTable则为所有带有$_标记的加上UIGoTable
        if (goTable)
        {
            var tfArray = instance.GetComponentsInChildren<Transform>(true);
            for (int i = 0; i < tfArray.Length; i++)
            {
                Transform node = tfArray[i];
                if (node.name.Contains("$_"))
                {
                    if (node.GetComponent<UIGoTable>() == null)
                    {
                        node.gameObject.AddComponent<UIGoTable>();
                    }
                    if (node.name.Contains("@_"))
                    {
                        if (node.GetComponent<UIGoTable>())
                        {
                            DestroyImmediate(node.GetComponent<UIGoTable>());
                        }
                    }
                }

                //除掉所有小数
                var rectNode = node as RectTransform;
                if (rectNode != null)
                {
                    var text = rectNode.GetComponent<UnityEngine.UI.Text>();
                    //if (text == null)
                    {
                        var lpos = rectNode.anchoredPosition;
                        var x = Mathf.Round(lpos.x);
                        var y = Mathf.Round(lpos.y);
                        if (x != lpos.x || y != lpos.y)
                        {
                            //到顶节点之间, 所有宽度综合, 看单双
                            rectNode.anchoredPosition = Vector2.zero;
                            rectNode.anchoredPosition = new Vector2(x, y);
                            Debug.LogWarningFormat(node, "ui中anchoredPosition不允许存在小数, 节点:{0}被自动规整", node.name);
                        }
                    }
                }
                else
                {
                    Debug.LogErrorFormat(node, "该节点{0}, 没有发现RectTransform组件???", node.name);
                }
            }
        }

        // 重新获取该预制体下所有UIGoTable 执行一遍刷新操作
        UIGoTable[] gotableChilds = instance.GetComponentsInChildren<UIGoTable>(true);
        for (int i = 0; i < gotableChilds.Length; i++)
        {
            var gotable = gotableChilds[i];
            if (gotable != null)
            {
                gotable.AddTrans();
            }
        }

        _CreateGoTableDeclare(goTable);
        AssetDatabase.SaveAssets();
    }

    private static Vector2 GetPositionAll(Transform head, RectTransform trans, Vector2 offset)
    {
        if (trans == null || trans == head)
        {
            return offset;
        }

        var pos = trans.anchoredPosition;
        var sizeDelta = trans.sizeDelta;
        var pivot = trans.pivot;
        var x = pos.x + sizeDelta.x * (pivot.x - 0.5f);
        var y = pos.y + sizeDelta.y * (pivot.y - 0.5f);
        offset.x += x;
        offset.y += y;
        return GetPositionAll(head, trans.parent as RectTransform, offset);
    }

    //生成gotable成员信息
    [MenuItem("CONTEXT/UIGoTable/复制GoTable定义信息")]
    private static void LogNodeArray(MenuCommand command)
    {
        var goTable = command.context as UIGoTable;
        var text = _GetGoTableDefineInfo(goTable);
        GUIUtility.systemCopyBuffer = text;
    }

    [MenuItem("CONTEXT/UIGoTable/整理所有文字")]
    private static void SelectAllText(MenuCommand command)
    {
        var goTable = command.context as UIGoTable;
        if (goTable)
        {
            var texts = goTable.GetComponentsInChildren<UnityEngine.UI.Text>();
            foreach (var item in texts)
            {
                var transform = item.transform;
                transform.position = new Vector3(Mathf.Ceil(transform.position.x * 100) / 100, Mathf.Ceil(transform.position.y * 100) / 100, Mathf.Ceil(transform.position.z * 100) / 100);
            }
        }
    }

    //获取gotable的全名定义
    private static string _GetGoTableDefineInfo(UIGoTable goTable)
    {
        var nameInfo = _GetGoTableViewName(goTable);
        return string.Format("---@field private go_table {0}_GoTable", nameInfo);
    }


    /// <summary>
    /// 生成gotable指定声明信息到特定文件下
    /// </summary>
    /// <param name="goTable"></param>
    private static void _CreateGoTableDeclare(UIGoTable goTable)
    {
        if (goTable == null)
            return;

        var gotables = goTable.GetComponentsInChildren<UIGoTable>(true);
        var name = goTable.name;

        //避免不同窗口下的同名组件相互覆盖
        if (name.StartsWith("$_"))
        {
            var nameInfo = _GetGoTableViewName(goTable);
            name = "$_" + nameInfo.Replace(".", "");
        }

        //Debug.LogFormat("开始对{0}, 生成声明文件", name);

        //筛选重名
        Dictionary<string, UIGoTable> gotableDic = new Dictionary<string, UIGoTable>();
        for (int i = 0, length = gotables.Length; i < length; i++)
        {
            var gotable = gotables[i];

            var gotList = gotable.GetComponentsInParent<UIGoTable>(true);

            string keyName = "";
            if (gotList != null && gotList.Length > 0)
            {
                for (int j = gotList.Length - 1; j >= 0; j--)
                {
                    keyName += gotList[j].gameObject.name;
                    if (j > 0)
                    {
                        keyName += "/";
                    }
                }
            }

            if (gotableDic.ContainsKey(keyName))
            {
                Debug.LogWarningFormat(gotable, "节点命名冲突:{0}", gotable.name);
                continue;
            }

            //从第2个节点开始, 没有前置的也不进行声明
            if (i > 0)
            {
                if (!_IsGoTableClass(gotable))
                {
                    Debug.LogWarningFormat(gotable, "节点命名不规范:{0}", gotable.name);
                    continue;
                }
            }

            gotableDic[keyName] = gotable;
        }
        //开始生成
        StringBuilder sb = new StringBuilder();
        Dictionary<string, int> nameMap = new Dictionary<string, int>();
        List<string> list = new List<string>();

        foreach (var item in gotableDic)
        {
            var key = item.Value.gameObject.name;
            if (list.Contains(key))
            {
                continue;
            }
            else
            {
                list.Add(key);
            }

            _GetGoTableDeclareInfo(item.Value, sb);
            sb.AppendLine();
        }
        //开始保存
        var text = sb.ToString();

        if (!System.IO.Directory.Exists(APISavePath))
        {
            System.IO.Directory.CreateDirectory(APISavePath);
        }
        var path = $"{APISavePath}/{name}.declare.lua";
        var utf8 = new UTF8Encoding(false);
        System.IO.File.WriteAllText(path, text, utf8);

        Debug.LogFormat("已完成对{0}, 生成声明文件, 路径:{1}", name, path);
    }

    private static bool _IsGoTableClass(UIGoTable goTable)
    {
        var goName = goTable.name;
        if (goName.StartsWith("@_"))
        {
            return false;
        }
        else if (goName.StartsWith("$_"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private static string _GetGoTableViewName(UIGoTable goTable)
    {
        var viewName = "View";
        var goName = goTable.name;
        if (goName.StartsWith("@_"))
        {
            viewName = goName.Substring(1);
        }
        else if (goName.StartsWith("$_"))
        {
            viewName = goName.Substring(1);
            if (viewName.StartsWith("_obj_"))
            {
                viewName = viewName.Substring("_obj_".Length);
            }
            var n = _GetGoTableParentViewName(goTable.transform.parent, "");
            if (string.IsNullOrEmpty(n))
            {
                return viewName;
            }
            return n + "_" + viewName;
        }
        else
        {
            viewName = goName;
        }
        return viewName;
    }

    private static string _GetGoTableParentViewName(Transform node, string cname)
    {
        // 找到第一个不带@或$但带Gotable的节点即为View节点(为了支持嵌套)
        if (node == null)
            return null;
        var pgotabele = node.GetComponent<UIGoTable>();
        if (pgotabele != null && !(pgotabele.name.StartsWith("$") || pgotabele.name.StartsWith("@")))
        {
            return $"{pgotabele.name}{cname}";
        }
        else
        {
            return _GetGoTableParentViewName(node.parent, cname);
        }
    }

    /// <summary>
    /// 获得gotable声明信息
    /// </summary>
    /// <param name="goTable"></param>
    /// <param name="sb"></param>
    private static void _GetGoTableDeclareInfo(UIGoTable goTable, StringBuilder sb)
    {
        //得到视口名字
        var viewName = _GetGoTableViewName(goTable);

        var uiNodeArray = goTable.GetUiNodeArray();
        var nodeArrayLength = uiNodeArray == null ? 0 : uiNodeArray.Length;

        //go_table 声明
        sb.AppendLine(string.Format("---@class {0}_GoTable", viewName));
        sb.AppendLine(string.Format("---@field public {0} {1}", "transform", typeof(Transform).FullName));
        sb.AppendLine(string.Format("---@field public {0} {1}", "gameObject", typeof(GameObject).FullName));
        if (uiNodeArray != null)
        {
            for (int i = 0; i < nodeArrayLength; i++)
            {
                var un = uiNodeArray[i];
                //自己这个结点不打印
                if (un.Obj == goTable.gameObject)
                    continue;

                if (un.Obj == null)
                {
                    sb.AppendLine(string.Format("---@field public {0} {1}", un.KeyName, "null"));
                }
                else
                {
                    sb.AppendLine(string.Format("---@field public {0} {1}", un.KeyName, un.Obj.GetType().FullName));
                }
            }
        }
    }

}
#endif