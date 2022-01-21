using System.Runtime.CompilerServices;
using System;
using UnityEngine;

public enum eRedPointType
{
    /// <summary>
    /// 依赖型红点(红点状态会影响父子节点)
    /// </summary>
    Dependency = 1,
    /// <summary>
    /// 独立型红点(红点状态不会影响父子节点)
    /// </summary>
    Single = 2,
}

[Serializable]
public class RedType
{
    /// <summary>
    /// 子红点类型(默认独立，子节点不受影响)
    /// </summary>
    public eRedPointType ChildType = eRedPointType.Single;
    /// <summary>
    /// 父红点类型(默认依赖，影响父节点)
    /// </summary>
    public eRedPointType ParentType = eRedPointType.Dependency;
}

[ExecuteInEditMode]
public class RedPointPref : MonoBehaviour
{
    [HideInInspector]
    public bool isNumber = false;

    [HideInInspector]
    public bool isSelfShow = true;

    /// <summary>
    /// 红点样式
    /// </summary>
    [SerializeField]
    [HideInInspector]
    public GameObject styleAssetObject = null;

    /// <summary>
    /// 受到红点影响的gameobj->跟随红点的显隐
    /// </summary>
    public GameObject[] linkObjects = null;

    [SerializeField]
    internal string key = "";

    [SerializeField]
    internal RedType redType;

    /// <summary>
    /// 红点数据
    /// </summary>
    internal RedPointNode dataNode;

    private RedPointPrefabData prefabData;

    private GameObject prefabGo;

    /// <summary>
    /// 当前需要显示的数量
    /// </summary>
    private int _tipsNum = 0;

    private void Awake()
    {
        if (Application.isPlaying)
        {
            if (!string.IsNullOrEmpty(key))
            {
                var node = CSRedPointManager.Instance.GetNode(key);
                if (node == null)
                    CSRedPointManager.Instance.AddRedPointNode(key);
                dataNode = CSRedPointManager.Instance.Register(this);
            }
        }
    }

    private void OnDestroy()
    {
        if (Application.isPlaying && CSRedPointManager.IsInstance())
        {
            CSRedPointManager.Instance.Unregister(this);
            HideView();
        }
    }

    public RedPointNode GetDataNode()
    {
        return dataNode;
    }

    public int GetTipsNum()
    {
        return _tipsNum;
    }

    /// 设置是否显示数字
    public void SwitchNumberShow(bool isNumberShow)
    {
        isNumber = isNumberShow;
        if (prefabData != null)
        {
            prefabData.SwitchNumberShow(isNumberShow);
        }
    }

    public void Unregister()
    {
        CSRedPointManager.Instance.Unregister(this);
    }

    /// 动态绑定
    public void DynamicBind(string key, RedType type)
    {
        if (redType != type)
        {
            if (!string.IsNullOrEmpty(key))
            {
                CSRedPointManager.Instance.Unregister(this);
            }

            redType = type;
            this.key = key;

            dataNode = null;
            if (!string.IsNullOrEmpty(key))
            {
                dataNode = CSRedPointManager.Instance.Register(this);
            }
        }
    }

    /// 更新显示
    public void DisplayView(bool bShowTip, int tipNum = 0)
    {
        _tipsNum = tipNum;
        SetLinkObject(bShowTip);
        if (!isSelfShow || !bShowTip)
        {
            HideView();
            return;
        }
        ShowView();
    }

    private void RefreshView(bool bShowTip)
    {
        if (prefabGo != null)
        {
            prefabGo.SetActive(bShowTip);
        }
        if (prefabData != null)
        {
            prefabData.SwitchNumberShow(isNumber);
            if (isNumber)
            {
                prefabData.SetNumber(_tipsNum);
            }
        }
    }

    private void ShowView()
    {
        if (prefabGo != null)
        {
            RefreshView(true);
            return;
        }
        if (!isSelfShow)
            return;
        if (Application.isPlaying)
        {
            if (styleAssetObject == null)
            {
                Debug.LogErrorFormat("红点预制未关联对应的红点样式!!!!!-->PrefName:{0}", transform.name);
            }
            else
            {
                var go = Instantiate(styleAssetObject, transform);
                UpdatePrefab(go);
                RefreshView(true);
            }
        }
    }

    private void HideView()
    {
        if (prefabGo != null)
        {
            Destroy(prefabGo);
            prefabGo = null;
            prefabData = null;
        }
    }

    private void SetLinkObject(bool value)
    {
        if (linkObjects != null)
        {
            for (int i = linkObjects.Length - 1; i >= 0; i--)
            {
                var target = linkObjects[i];
                if (target != null)
                {
                    target.SetActive(value);
                }
            }
        }
    }

    public void UpdatePrefab(GameObject go)
    {
        go.transform.localScale = Vector3.one;
        go.transform.localPosition = Vector3.zero;
        go.hideFlags = HideFlags.DontSave | HideFlags.NotEditable;
        prefabData = go.GetComponent<RedPointPrefabData>();
        prefabGo = go;
    }

}
