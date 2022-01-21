using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class RedPointNode : IDisposable
{
    //key
    public string key;
    //红点类型
    private RedType redType;
    //绑定的对象
    private List<RedPointPref> RedPointObj;
    //父节点对象列表
    private List<RedPointNode> parentNode_list = new List<RedPointNode>();
    //子节点对象列表
    private List<RedPointNode> childNode_list = new List<RedPointNode>();
    //经过对子节点检测后得到的当前状态(真正状态)
    private bool nodeValue;
    //临时状态
    private bool localState;
    //设置Tip数量
    private int localNum;

    public RedPointNode() { }
    public RedPointNode(string urlKey, bool value)
    {
        key = urlKey;
        nodeValue = value;
        redType = new RedType();
        redType.ChildType = eRedPointType.Single;
        redType.ParentType = eRedPointType.Dependency;
    }

    //添加子节点
    public void AddChild(RedPointNode node)
    {
        if (!childNode_list.Contains(node))
        {
            childNode_list.Add(node);
            node.parentNode_list.Add(this);
        }
    }

    //添加子节点
    public void RemoveChild(RedPointNode node)
    {
        if (childNode_list.Contains(node))
        {
            childNode_list.Remove(node);
            node.parentNode_list.Remove(this);
        }
    }

    //检查状态
    public void CheckState()
    {
        bool lastNodeValue = nodeValue;
        // 根节点不用继续推导
        if (parentNode_list.Count == 0)
            return;
        // 它的状态影响子节点 一层层传递
        if (redType.ChildType == eRedPointType.Dependency)
        {
            nodeValue = localState;
            if (!localState)
            {
                for (int i = 0; i < childNode_list.Count; i++)
                {
                    var node = childNode_list[i];
                    if (nodeValue != node.localState)
                    {
                        node.localState = this.nodeValue;
                        node.CheckState();
                    }
                }
            }
        }
        // 它的状态不影响子节点
        else if (redType.ChildType == eRedPointType.Single)
        {
            nodeValue = localState;
        }

        // 它的状态影响父节点
        if (redType.ParentType == eRedPointType.Dependency)
        {
            nodeValue = localState;
            for (int j = 0; j < parentNode_list.Count; j++)
            {
                var parent = parentNode_list[j];
                for (int i = 0; i < parent.childNode_list.Count; i++)
                {
                    var node = parent.childNode_list[i];
                    parent.localState |= node.nodeValue;
                }
                if (parent.localState != parent.nodeValue)
                {
                    parent.CheckState();
                }
            }
        }
        // 它的状态不影响父节点
        else if (redType.ParentType == eRedPointType.Single)
        {
            nodeValue = localState;
        }

        if (lastNodeValue != nodeValue)
        {
            if (nodeValue)
            {
                //显示所有相关的红点
                RefreshPoints();
            }
            else
            {
                //回收所有相关的红点
                RecycleAllPointsGameObject();
            }
        }
    }

    /// <summary>
    /// 计算Node节点的红点数量
    /// </summary>
    public void CalculationNum()
    {
        if (parentNode_list.Count == 0)
            return;
        if (redType.ParentType == eRedPointType.Dependency)
        {
            for (int j = 0; j < parentNode_list.Count; j++)
            {
                var parent = parentNode_list[j];
                parent.localNum = 0;
                var parentChildNodeList = parent.childNode_list;
                var parentChildNodeListCount = parentChildNodeList.Count;
                for (int i = 0; i < parentChildNodeListCount; i++)
                {
                    var parentChildNode = parentChildNodeList[i];
                    if (parentChildNode.redType.ParentType == eRedPointType.Dependency)
                    {
                        parent.localNum += parentChildNode.localNum;
                    }
                }
                //子物体没有了
                if (parent.localNum == 0 || parentChildNodeListCount == 0)
                {
                    parent.nodeValue = false;
                    parent.RefreshPoints();
                }
                parent.CalculationNum();
            }
        }

        //改变绑定ui
        RefreshPoints();
    }

    //更新当前节点绑定的ui对象状态
    private void RefreshPoints()
    {
        if (null != RedPointObj)
        {
            for (int i = RedPointObj.Count - 1; i >= 0; --i)
            {
                var r = RedPointObj[i];
                if (r == null)
                {
                    RedPointObj.RemoveAt(i);
                    continue;
                }

                r.DisplayView(nodeValue, localNum);
            }
        }
    }

    private void RecycleAllPointsGameObject()
    {
        if (RedPointObj != null)
        {
            for (int i = RedPointObj.Count - 1; i >= 0; --i)
            {
                var r = RedPointObj[i];
                if (r == null)
                {
                    RedPointObj.RemoveAt(i);
                    continue;
                }

                r.DisplayView(false);
            }
        }
    }

    //获取子节点列表
    public List<RedPointNode> GetParentNodeList()
    {
        return parentNode_list;
    }

    //获取子节点列表
    public List<RedPointNode> GetChildNodeList()
    {
        return childNode_list;
    }

    public int GetLocalNum()
    {
        return localNum;
    }

    /// 设置红点内容
    public void SetValue(bool vals, int tipNum = 0)
    {
        localState = vals;
        localNum = tipNum;
        CheckState();
        CalculationNum();
    }

    /// 绑定ui对象
    internal void BindUI(RedType redType, RedPointPref obj)
    {
        if (RedPointObj == null)
        {
            RedPointObj = new List<RedPointPref>();
        }

        if (!RedPointObj.Contains(obj))
        {
            RedPointObj.Add(obj);
        }

        this.redType = redType;
        RefreshPoints();
    }

    internal void Register(RedPointPref obj)
    {
        if (obj == null)
        {
            return;
        }

        if (RedPointObj == null)
        {
            RedPointObj = new List<RedPointPref>();
        }


        if (!RedPointObj.Contains(obj))
        {
            RedPointObj.Add(obj);
        }

        redType = obj.redType;
        obj.DisplayView(nodeValue, localNum);
    }

    internal void Unregister(RedPointPref obj)
    {
        if (obj == null)
        {
            return;
        }

        if (RedPointObj != null)
        {
            RedPointObj.Remove(obj);
        }
    }


    public void Dispose()
    {

    }
}

