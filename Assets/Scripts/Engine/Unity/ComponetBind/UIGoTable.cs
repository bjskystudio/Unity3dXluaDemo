//*******************************
// Name: wyx Time:2019.05.25
// Des: 预序列化节点类,用于UI的节点获取
//******************************
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XLua;
using LuaAPI = XLua.LuaDLL.Lua;

/// <summary>
/// 序列化节点类,表示一个UI节点
/// </summary>
[System.Serializable]
public sealed class UINodeInfo
{
    /// <summary>
    /// 节点名称,抛Lua使用的Key
    /// </summary>
    public string KeyName;

    /// <summary>
    /// 绑定类型后的组件
    /// </summary>
    public UnityEngine.Object Obj;

    /// <summary>
    /// gameObject对象单独存储
    /// </summary>
    [HideInInspector]
    public UnityEngine.GameObject gameObject;

    /// <summary>
    /// 节点类
    /// </summary>
    /// <param name="name">Lua使用的key名称</param>
    /// <param name="ComponentName">组件原始名称</param>
    /// <param name="obj">绑定类型后的组件</param>
    public UINodeInfo(string keyname, UnityEngine.Object obj, UnityEngine.GameObject gameObject)
    {
        KeyName = keyname;
        Obj = obj;
        this.gameObject = gameObject;
    }
}
/// <summary>
/// UI节点获取类
/// </summary>
public sealed class UIGoTable : MonoBehaviour
{
    [CSharpCallLua]
    public delegate void LuaOnClickBtnAction(LuaTable t, Button g);
    [CSharpCallLua]
    public delegate void LuaOnClickToggleAction(LuaTable t, Toggle g, int isOn);
    [CSharpCallLua]
    public delegate void LuaOnClickTmpAction(LuaTable t, AorTMP g,string linkId);

    /// <summary>
    /// 导出的节点数组
    /// </summary>
    [SerializeField]
    private UINodeInfo[] uiNodeArray;
    [SerializeField]
    private List<Button> uiBtnList;
    [SerializeField]
    private List<Toggle> uiToggleList;
    [SerializeField]
    private List<AorTMP> uiTmpList;

    [HideInInspector]
    [SerializeField]
    private string nowState;
    public string NowState { get => this.nowState; }
    [HideInInspector]
    [SerializeField]
    private List<string> stateNameList = new List<string>();
    public List<string> StateNameList { get => this.stateNameList; }

    [HideInInspector]
    [SerializeField]
    private List<UIStateInfo> stateList = new List<UIStateInfo>();

    /// <summary>
    /// Lua访问的节点数据
    /// </summary>
    private LuaTable goTable;
    /// <summary>
    /// View Lua代码对应的按钮响应事件
    /// </summary>
    [CSharpCallLua]
    public static LuaOnClickBtnAction luaOnClickBtn;
    /// <summary>
    /// View Lua代码对应的Toggle响应事件
    /// </summary>
    [CSharpCallLua]
    public static LuaOnClickToggleAction luaOnClickToggle;
    /// <summary>
    /// View Lua代码对应的超链接响应事件
    /// </summary>
    [CSharpCallLua]
    public static LuaOnClickTmpAction luaOnClickTmp;

#if UNITY_EDITOR
    /// <summary>
    /// 获取所有被标记的节点
    /// </summary>
    [BlackList]
    public void AddTrans()
    {
        List<Transform> ls = UINodeSingleton.instance.AllFlagTrans(transform);//获取所有子节点
        uiNodeArray = new UINodeInfo[ls.Count];               //根据节点数量初始化序列化数组
        for (int i = 0; i < ls.Count; i++)                           //遍历节点
        {
            string keyname = UINodeSingleton.instance.GetUINodeKey(ls[i].name);  //获取Lua使用的节点key
            if (keyname == string.Empty)
            {
                Debug.LogError("[class]UIGoTable 无法获取类型Key [name]" + ls[i].name);
                continue;
            }
            var temp = UINodeSingleton.instance.GetUINodeType(ls[i], keyname); //节点与keyname进行节点和类型的绑定
            if (temp == null)
            {
                Debug.LogError("无法从节点正确获取指定组件:" + string.Join("/", ls[i].GetComponentsInParent<Transform>(true).Reverse().Select((s) => { return s.name; }).ToArray()), ls[i]);
                continue;
            }

            UINodeInfo uINodeInfo = new UINodeInfo(keyname, temp, ls[i].gameObject);     //初始化节点信息类
            uiNodeArray[i] = uINodeInfo;                               //存储节点
        }

        //序列化 Button
        uiBtnList = new List<Button>();
        uiToggleList = new List<Toggle>();
        uiTmpList = new List<AorTMP>();
        for (int i = 0; i < uiNodeArray.Length; i++)
        {
            var nodeinfo = uiNodeArray[i];
            if (nodeinfo.Obj != null)
            {
                if (nodeinfo.gameObject)
                {
                    var btn = nodeinfo.gameObject.GetComponent<Button>();
                    if (btn != null)
                    {
                        uiBtnList.Add(btn);
                    }
                    var toggle = nodeinfo.gameObject.GetComponent<Toggle>();
                    if (toggle != null)
                    {
                        uiToggleList.Add(toggle);
                    }
                    var tmp = nodeinfo.gameObject.GetComponent<AorTMP>();
                    if (tmp != null)
                    {
                        uiTmpList.Add(tmp);
                    }
                }
            }
        }

        Array.Sort(uiNodeArray, (a, b) => { return string.Compare(a.KeyName, b.KeyName); });
    }

    [BlackList]
    public int GetObjCount()
    {
        RectTransform[] tTFList = transform.GetComponentsInChildren<RectTransform>(true);
        return tTFList.Length;
    }

    /// <summary>
    ///  更新对应状态信息
    /// </summary>
    /// <param name="tStateName">状态名字</param>
    [BlackList]
    public void UpdateState(string name)
    {
        this.UpdateState(name, true, true, true, true, true);
    }
    /// <summary>
    ///  更新对应状态信息
    /// </summary>
    /// <param name="tStateName">状态名字</param>
    [BlackList]
    public void UpdateState(string name, bool syncPos = true, bool syncSelfPos = true, bool syncRotation = true, bool syncScale = true, bool syncSize = true)
    {
        RectTransform[] tTFList = transform.GetComponentsInChildren<RectTransform>(true);
        int tIdx;
        if (name != "none" && this.AddStateName("none"))                                                      // 基础状态
        {
            tIdx = this.stateNameList.IndexOf("none");
            this.stateList[tIdx].SetAllTF(tTFList);
        }

        this.AddStateName(name, syncPos, syncSelfPos, syncRotation, syncScale, syncSize);
        tIdx = this.stateNameList.IndexOf(name);
        this.stateList[tIdx].SetAllTF(tTFList);
    }
    [BlackList]
    public bool AddStateName(string name, bool syncPos = true, bool syncSelfPos = true, bool syncRotation = true, bool syncScale = true, bool syncSize = true)
    {
        if (this.stateNameList.IndexOf(name) != -1) { return false; }
        this.stateNameList.Add(name);
        UIStateInfo tUIStateInfo = this.gameObject.AddComponent<UIStateInfo>();
        tUIStateInfo.stateName = name;
        tUIStateInfo.syncPos = syncPos;
        tUIStateInfo.syncSelfPos = syncSelfPos;
        tUIStateInfo.syncRotation = syncRotation;
        tUIStateInfo.syncScale = syncScale;
        tUIStateInfo.syncSize = syncSize;
        this.stateList.Add(tUIStateInfo);
        return true;
    }

    /// <summary>
    ///  删除对应的状态
    /// </summary>
    /// <param name="name">状态名</param>
    [BlackList]
    public void DeleteState(string name)
    {
        int tIdx = this.stateNameList.IndexOf(name);
        if (tIdx == -1) { return; }
        this.stateNameList.RemoveAt(tIdx);
        DestroyImmediate(this.stateList[tIdx]);
        this.stateList.RemoveAt(tIdx);
    }

    [BlackList]
    public void ClearState()
    {
        this.nowState = "";
        this.stateNameList = new List<string>();
        this.stateList = new List<UIStateInfo>();
        UIStateInfo[] tUIStateInfoList = this.GetComponents<UIStateInfo>();
        for (int i = 0; i < tUIStateInfoList.Length; i++)
        {
            DestroyImmediate(tUIStateInfoList[i]);
        }
    }

    /// <summary>
    ///  检查被删除掉的对象，并移除对应的信息
    /// </summary>
    [BlackList]
    public void CheckAllStateInfo()
    {
        // print("----" + this.stateList.Count + " | " + this.stateNameList.Count);
        List<string> tNameList = new List<string>();

        if (this.stateNameList.Count == 0)
        {
            this.nowState = "";
        }
        else if (this.stateNameList.IndexOf(this.nowState) == -1)
        {
            this.nowState = this.stateNameList[0];
        }

        RectTransform[] tTFList = transform.GetComponentsInChildren<RectTransform>(true);
        for (int i = 0; i < this.stateList.Count; i++)
        {
            this.stateList[i].SyncAllStateInfo(tTFList);
        }
    }
#endif

    /// <summary>
    ///  变更状态
    /// </summary>
    /// <param name="name">状态名</param>
    /// <returns>是否变更成功</returns>
    public bool ChangeState(string name)
    {
        int tIdx = this.stateNameList.IndexOf(name);
        if (tIdx == -1) { return false; }        // 不存在的状态

        this.nowState = name;
        UIStateInfo tUIStateInfoList = this.stateList[tIdx];
        if (tUIStateInfoList == null) { return false; }
        tUIStateInfoList.ChangeTo();

        return true;
    }

    public void SetLuaGoTable(LuaTable table)
    {
        goTable = table;
    }

    public LuaTable GetLuaGoTable()
    {
        return goTable;
    }

    /// <summary>
    /// 获取View下节点上的信息
    /// </summary>
    /// <returns></returns>
    public UINodeInfo[] GetUiNodeArray()
    {
        return uiNodeArray;
    }

    private void Awake()
    {
        CreateLuaGoTable();
    }

    /// <summary>
    /// 返回Lua UI节点table goTable.Set会产生较大的开销已由c# 操作更改到lua
    /// </summary>
    /// <returns></returns>
    public LuaTable CreateLuaGoTable()
    {
        if (goTable == null)
        {
            goTable = XLuaManager.Instance.GetLuaEnv().NewTable();
            PushGoTableObject();
            //绑定Click
            BindClick();

            this.ChangeState(this.nowState);
        }
        return goTable;
    }

    public void PushGoTableObject()
    {
        var luaEnv = XLuaManager.Instance.GetLuaEnv();
        var L = luaEnv.L;
        int oldTop = LuaAPI.lua_gettop(L);
        goTable.push(L);
        bool is_first;
        for (int i = 0, max = uiNodeArray.Length; i < max; i++)
        {
            var obj = uiNodeArray[i];
            if (obj.Obj != null && !string.IsNullOrEmpty(obj.KeyName))
            {
                int type_id = luaEnv.translator.getTypeId(L, obj.Obj.GetType(), out is_first);
                luaEnv.translator.PushObject(L, obj.Obj, type_id);
                if (0 != LuaAPI.xlua_psettable_bypath(L, -2, obj.KeyName))
                {
                    luaEnv.ThrowExceptionFromError(oldTop);
                }
            }
        }

        luaEnv.translator.PushObject(L, gameObject, luaEnv.translator.getTypeId(L, typeof(GameObject), out is_first));
        if (0 != LuaAPI.xlua_psettable_bypath(L, -2, "gameObject"))
        {
            luaEnv.ThrowExceptionFromError(oldTop);
        }
        luaEnv.translator.PushObject(L, gameObject.transform, luaEnv.translator.getTypeId(L, transform.GetType(), out is_first));
        if (0 != LuaAPI.xlua_psettable_bypath(L, -2, "transform"))
        {
            luaEnv.ThrowExceptionFromError(oldTop);
        }
        LuaAPI.lua_settop(L, oldTop);
    }

    private bool isBind = false;
    /// <summary>
    /// 绑定按钮响应事件
    /// </summary>
    public void BindClick()
    {
        if (!isBind)
        {
            for (int i = 0; i < uiBtnList.Count; i++)
            {
                var btn = uiBtnList[i];
                if (btn != null)
                {
                    bool isAnimation = btn.transition == Selectable.Transition.Animation;
                    btn.onClick.AddListener(delegate ()
                    {
                        OnBtnClick(btn, null, true, isAnimation);
                    });
                }
            }

            for (int i = 0; i < uiToggleList.Count; i++)
            {
                var toggle = uiToggleList[i];
                if (toggle != null)
                {
                    toggle.onValueChanged.AddListener((isOn) =>
                    {
                        OnToggleClick(toggle, null, isOn, false);
                    });
                }
            }

            for (int i = 0; i < uiTmpList.Count; i++)
            {
                var tmp = uiTmpList[i];
                if (tmp != null)
                {
                    tmp.onLinkClick.AddListener(delegate (string linkId)
                    {
                        OnTmpClick(tmp, null, linkId);
                    });
                }
            }

            isBind = true;
        }
    }

    private void OnBtnClick(Button go, PointerEventData eventData, bool isOn, bool isAnimation = false)
    {
        if (isAnimation)
        {
            Tweener tweener = go.transform.DOScale(new Vector3(0.95f, 0.95f, 1), 0.1f);
            tweener.OnComplete(() =>
            {
                luaOnClickBtn?.Invoke(goTable, go);
                tweener = go.transform.DOScale(new Vector3(1, 1, 1), 0.1f);
            });
        }
        else
        {
            luaOnClickBtn?.Invoke(goTable, go);
        }
    }

    private void OnToggleClick(Toggle go, PointerEventData eventData, bool isOn, bool isAnimation = false)
    {
        if (isAnimation)
        {
            Tweener tweener = go.transform.DOScale(new Vector3(0.95f, 0.95f, 1), 0.1f);
            tweener.OnComplete(() =>
            {
                luaOnClickToggle?.Invoke(goTable, go, isOn ? 1 : 0);
                tweener = go.transform.DOScale(new Vector3(1, 1, 1), 0.1f);
            });
        }
        else
        {
            luaOnClickToggle?.Invoke(goTable, go, isOn ? 1 : 0);
        }
    }
    
    private void OnTmpClick(AorTMP go,PointerEventData eventData, string linkId)
    {
        luaOnClickTmp?.Invoke(goTable, go,linkId);
    }
}

