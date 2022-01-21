using System;
using Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CSRedPointManager : MonoSingleton<CSRedPointManager>
{
    private static readonly string[] EmptyStringArray = new string[] { };
    private static readonly string[] PathSplit = new string[] { "_" };
    /// <summary>
    /// 存储红点Key和节点数据关系的字典
    /// </summary>
    private static Dictionary<string, RedPointNode> basNode_Dic = new Dictionary<string, RedPointNode>();
    // Root数据节点
    private RedPointNode Root = new RedPointNode("<Root>", false);
    //当小红点数量超过此值时，显示m_limitShowStr
    private int m_limitNum = 99;
    private string m_limitShowStr = "99+";
    public int LimitNum { get { return m_limitNum; } }
    public string LimitShowStr { get { return m_limitShowStr; } }

    protected override void Init()
    {
        base.Init();
    }

    //获取节点数据
    public RedPointNode GetNode(string nkey)
    {
        RedPointNode node;
        basNode_Dic.TryGetValue(nkey, out node);
        return node;
    }
    
    /// <summary>
    /// 添加红点数据，建立关联关系
    /// </summary>
    /// <param name="fullPath"></param>
    public void AddRedPointNode(string fullPath)
    {
        string[] paths = GetSplitPath(fullPath);
        for (int i = 0; i < paths.Length; i++)
        {
            string cur_path = paths[i];
            if (0 < i)
            {
                string parent_path = paths[i - 1];
                CreateNode(cur_path, parent_path);
            }
            else
            {
                CreateNode(cur_path, null);
            }
        }
    }

    /// <summary>
    /// 绑定ui
    /// </summary>
    /// <param name="key"></param>
    /// <param name="obj"></param>
    public RedPointNode BindUI(RedType redType, string key, RedPointPref obj)
    {
        RedPointNode node;
        basNode_Dic.TryGetValue(key, out node);

        if (null != node)
        {
            node.BindUI(redType, obj);
        }
        else
        {
            Debug.LogError(string.Format("未找到相关红点标记{0}  无法实施ui绑定", key));
        }

        return node;
    }

    internal RedPointNode Register(RedPointPref obj)
    {
        if (obj == null)
        {
            return null;
        }

        RedPointNode node;
        basNode_Dic.TryGetValue(obj.key, out node);

        if (node != null)
        {
            node.Register(obj);
        }
        else
        {
            Debug.LogErrorFormat("未找到相关红点标记{0}  无法实施ui绑定", obj.key);
        }

        return node;
    }

    internal void Unregister(RedPointPref obj)
    {
        if (obj == null)
        {
            return;
        }

        if(obj.dataNode != null)
        {
            obj.dataNode.Unregister(obj);
            obj.dataNode = null;
        }
    }
    
    /// <summary>
    /// 创建节点信息Node
    /// </summary>
    /// <param name="nkey"></param>
    /// <param name="parentKey"></param>
    private void CreateNode(string nkey, string parentKey)
    {
        if (!basNode_Dic.ContainsKey(nkey))
        {
            RedPointNode parentNode = null;
            if (string.IsNullOrEmpty(parentKey))
            {
                parentNode = Root;
            }
            else
            {
                basNode_Dic.TryGetValue(parentKey, out parentNode);
                if (null == parentNode)
                {
                    Debug.LogError(string.Format("子红点数据:{0} 找不到父红点: {1}  ", nkey, parentKey));
                    return;
                }
            }
            RedPointNode current = new RedPointNode(nkey, false);
            parentNode.AddChild(current);
            if (!basNode_Dic.ContainsKey(nkey))
            {
                basNode_Dic.Add(nkey, current);
            }
        }
    }

    /// <summary>
    /// ToDo 更改算法减少开销
    /// 数据结点路径切分工具函数。
    /// </summary>
    /// <param name="path">要切分的数据结点路径。</param>
    /// <returns>切分后的字符串数组。</returns>
    private static string[] GetSplitPath(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            return EmptyStringArray;
        }

        var temp = path.Split(PathSplit, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < temp.Length; i++)
        {
            if (0 < i)
            {
                temp[i] = temp[i - 1] + "_" + temp[i];
            }
        }
        return temp;
    }


    //移除跟当前key依赖的所有节点数据
    public void RemoveNode(string nkey)
    {
        if (basNode_Dic.ContainsKey(nkey))
        {
            RedPointNode node = this.GetNode(nkey);
            basNode_Dic.Remove(nkey);

            var parentNode_list = node.GetParentNodeList();
            for (int i = 0, length = parentNode_list.Count; i < length; i++)
            {
                var parent = parentNode_list[i];
                List<RedPointNode> parentList = parent.GetChildNodeList();
                parentList.Remove(node);
                //移除成功后重新计算子物体的显隐状态
                parent.CheckState();
                //移除成功后重新计算小红点数量
                parent.CalculationNum();
            }
            
            List<RedPointNode> childList = node.GetChildNodeList();
            for (int i = childList.Count - 1; i >= 0; i--)
            {
                RemoveNode(childList[i].key);
            }
            node.Dispose();
        }
    }

    /// <summary>
    /// 删除子节点相互信息
    /// </summary>
    public void RemoveChildNode(string nkey)
    {
        if (basNode_Dic.ContainsKey(nkey))
        {
            RedPointNode node = this.GetNode(nkey);
            List<RedPointNode> childList = node.GetChildNodeList();
            for (int i = childList.Count - 1; i >= 0; i--)
            {
                RedPointNode childNode = childList[i];
                List<RedPointNode> parentNode_list = childNode.GetParentNodeList();
                if (parentNode_list.Count == 1)
                {
                    // 只有1个节点时, 直接删除节点
                    this.RemoveNode(childNode.key);
                }
                else
                {
                    // 多个节点引用时, 就解除节点
                    node.RemoveChild(childNode);
                }
            }
            node.SetValue(false);
        }
    }

    public void InitRedPointNumberView(int limitNum, string limitShowStr)
    {
        this.m_limitNum = limitNum;
        this.m_limitShowStr = limitShowStr;
    }


    public override void Dispose()
    {
        basNode_Dic.Clear();
    }
}


