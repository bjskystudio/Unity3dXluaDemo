using Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using YoukiaCore.Log;

namespace LuaBehaviourTree
{
    /// <summary>
    /// 行为树节点抽象
    /// </summary>
    [Serializable]
    public class BTNode : IRecycle
    {
        #region 序列化存储部分
        /// <summary>
        /// 节点UID
        /// </summary>
        public int UID;

        /// <summary>
        /// 节点显示框数据
        /// </summary>
        public Rect NodeDisplayRect;

        /// <summary>
        /// 节点索引
        /// </summary>
        public int NodeIndex;

        /// <summary>
        /// 节点名字
        /// </summary>
        public string NodeName;

        /// <summary>
        /// 节点参数(设计成编辑器只负责序列化参数数据，解析由运行时Lua对象节点自行解析)
        /// </summary>
        public string NodeParams;

        /// <summary>
        /// 节点类型
        /// </summary>
        public int NodeType;

        /// <summary>
        /// 父节点UID
        /// </summary>
        public int ParentNodeUID;

        /// <summary>
        /// 子节点UID列表
        /// </summary>
        public List<int> ChildNodesUIDList;

        /// <summary>
        /// 打断类型
        /// </summary>
        public EAbortType AbortType;

        /// <summary>
        /// 是否是CS节点(反之是Lua节点)
        /// 为了优化节点创建判定，避免循环比较判定
        /// </summary>
        public bool IsCSNode;
        #endregion

        #region 运行时部分
        /// <summary>
        /// 行为树节点所属行为树
        /// </summary>
        public TBehaviourTree OwnerBT
        {
            get;
            protected set;
        }

        /// <summary>
        /// 行为树节点所属BTGraph
        /// </summary>
        public BTGraph OwnerBTGraph
        {
            get;
            set;
        }

        /// <summary>
        /// 父节点
        /// </summary>
        public BTNode ParentNode
        {
            get;
            protected set;
        }

        /// <summary>
        /// 实体对象的ID
        /// </summary>
        public int InstanceID
        {
            get;
            protected set;
        }

        /// <summary>
        /// 绑定的世界对象UID
        /// </summary>
        public long WorldItemUID
        {
            get;
            protected set;
        }

        /// <summary>
        /// Lua测对应的行为树节点
        /// </summary>
        protected ILuaBTNode LuaNode;

        /// <summary>
        /// 节点运行状态
        /// </summary>
        public EBTNodeRunningState NodeRunningState
        {
            get;
            protected set;
        }

        /// <summary>
        /// 节点前一次运行状态
        /// Note：
        /// 解决节点OnExit退出后导致状态重置无法正确查看前一次状态问题(用于Editor查看状态用)
        /// </summary>
        public EBTNodeRunningState LastNodeRunningState
        {
            get;
            protected set;
        }

        /// <summary>
        /// 是否处于运行中
        /// </summary>
        public bool IsRunning
        {
            get
            {
                return NodeRunningState == EBTNodeRunningState.Running;
            }
        }

        /// <summary>
        /// 是否终止
        /// </summary>
        public bool IsTerminated
        {
            get
            {
                return NodeRunningState == EBTNodeRunningState.Success || NodeRunningState == EBTNodeRunningState.Failed;
            }
        }
        #endregion

        public BTNode()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodeRect">节点编辑器显示Rect</param>
        /// <param name="nodeIndex">节点索引</param>
        /// <param name="nodeName">节点名字</param>
        /// <param name="nodeType">节点类型</param>
        /// <param name="parentNode">父节点</param>
        /// <param name="allUsedNodeUidMap"></param>
        public BTNode(BTGraph ownerBTGraph, Rect nodeRect, int nodeIndex, string nodeName, EBTNodeType nodeType, BTNode parentNode, Dictionary<int, int> allUsedNodeUidMap)
        {
            // 确保生成的UID唯一
            UID = BTUtilities.GetNodeUID();
            while (allUsedNodeUidMap.ContainsKey(UID))
            {
                UID = BTUtilities.GetNodeUID();
            }
            allUsedNodeUidMap.Add(UID, UID);
            Log.Info($"新增使用UID:{UID}");
            OwnerBTGraph = ownerBTGraph;
            NodeDisplayRect = nodeRect;
            NodeIndex = nodeIndex;
            NodeName = nodeName;
            NodeParams = string.Empty;
            NodeType = (int)nodeType;
            ParentNodeUID = parentNode != null ? parentNode.UID : 0;
            ChildNodesUIDList = new List<int>();
            AbortType = GetNodeDefaultAbortType(nodeType);
            IsCSNode = CheckIsCSNodeInEditor();
            NodeRunningState = EBTNodeRunningState.Invalide;
            LastNodeRunningState = EBTNodeRunningState.Invalide;
        }

        #region 运行时部分
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="btOwner"></param>
        /// <param name="parentNode"></param>
        /// <param name="worldItemUid"></param>
        /// <param name="instanceId"></param>
        public BTNode(BTNode node, TBehaviourTree btOwner, BTNode parentNode, long worldItemUid, int instanceId)
        {
            UID = node.UID;
            NodeDisplayRect = node.NodeDisplayRect;
            NodeIndex = node.NodeIndex;
            NodeName = node.NodeName;
            NodeParams = node.NodeParams;
            NodeType = (int)node.NodeType;
            ParentNodeUID = node.ParentNodeUID;
            ChildNodesUIDList = node.ChildNodesUIDList;
            NodeRunningState = EBTNodeRunningState.Invalide;
            AbortType = node.AbortType;
            IsCSNode = node.IsCSNode;
            LastNodeRunningState = EBTNodeRunningState.Invalide;
            OwnerBT = btOwner;
            OwnerBTGraph = btOwner.BTRunningGraph;
            ParentNode = parentNode;
            WorldItemUID = worldItemUid;
            InstanceID = instanceId;
        }

        public virtual void OnCreate()
        {
            //Log.Info("BTNode:onCreate()");
        }

        /// <summary>
        /// 设置数据(运行时用对象池后的调用初始化数据)
        /// </summary>
        /// <param name="node"></param>
        /// <param name="btOwner"></param>
        /// <param name="parentNode"></param>
        /// <param name="worldItemUid"></param>
        /// <param name="instanceId"></param>
        public virtual void SetDatas(BTNode node, TBehaviourTree btOwner, BTNode parentNode, long worldItemUid, int instanceId)
        {
            UID = node.UID;
            NodeDisplayRect = node.NodeDisplayRect;
            NodeIndex = node.NodeIndex;
            NodeName = node.NodeName;
            NodeParams = node.NodeParams;
            NodeType = (int)node.NodeType;
            ParentNodeUID = node.ParentNodeUID;
            ChildNodesUIDList = node.ChildNodesUIDList;
            AbortType = node.AbortType;
            NodeRunningState = EBTNodeRunningState.Invalide;
            LastNodeRunningState = EBTNodeRunningState.Invalide;
            OwnerBT = btOwner;
            OwnerBTGraph = btOwner.BTRunningGraph;
            ParentNode = parentNode;
            WorldItemUID = worldItemUid;
            InstanceID = instanceId;
        }

        /// <summary>
        /// 释放
        /// </summary>
        public virtual void Dispose()
        {
            UID = 0;
            NodeIndex = -1;
            NodeName = null;
            NodeType = 0;
            ParentNodeUID = 0;
            ChildNodesUIDList = null;
            NodeRunningState = EBTNodeRunningState.Invalide;
            LastNodeRunningState = EBTNodeRunningState.Invalide;
            OwnerBT = null;
            ParentNode = null;
            WorldItemUID = 0;
            InstanceID = 0;
        }

        public void OnDispose()
        {
            //Log.Info("BTNode:onDispose()");
            UID = 0;
            NodeIndex = -1;
            NodeName = null;
            NodeType = 0;
            ParentNodeUID = 0;
            ChildNodesUIDList = null;
            NodeRunningState = EBTNodeRunningState.Invalide;
            LastNodeRunningState = EBTNodeRunningState.Invalide;
            OwnerBT = null;
            ParentNode = null;
            WorldItemUID = 0;
            InstanceID = 0;
        }

        /// <summary>
        /// 添加为执行接地那
        /// </summary>
        /// <returns></returns>
        public bool AddAsExecutingNode()
        {
            if (OwnerBT.BTRunningGraph != null)
            {
                return OwnerBT.BTRunningGraph.AddExecutingNode(this);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 响应条件变化终止
        /// </summary>
        public virtual void OnConditionalAbort()
        {
            // 终止时直接强制退出重置节点
            //Log.Info($"节点UID:{this.UID}响应条件变化终止!");
            OnExit();
        }

        /// <summary>
        /// 更新已执行的条件节点结果
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected bool UpdateExecutedReevaluatedNodeResult(EBTNodeRunningState result)
        {
            if (OwnerBT.BTRunningGraph != null)
            {
                return OwnerBT.BTRunningGraph.UpdateExecutedConditionNodeResult(this, result);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 节点更新
        /// </summary>
        /// <returns></returns>
        public virtual EBTNodeRunningState OnUpdate()
        {
            if (NodeRunningState == EBTNodeRunningState.Invalide)
            {
                OnEnter();
            }
            NodeRunningState = OnExecute();
            LastNodeRunningState = NodeRunningState;
            if (CanReevaluate())
            {
                UpdateExecutedReevaluatedNodeResult(NodeRunningState);
            }
            var tempstate = NodeRunningState;
            if (IsTerminated)
            {
                OnExit();
            }
            return tempstate;
        }

        /// <summary>
        /// 响应暂停
        /// </summary>
        /// <param name="ispause"></param>
        public virtual void OnPause(bool ispause)
        {

        }

        /// <summary>
        /// 重置节点状态
        /// </summary>
        public virtual void Reset()
        {
            //Log.Info(string.Format("重置节点:{0}", NodeName));
            LastNodeRunningState = NodeRunningState;
            NodeRunningState = EBTNodeRunningState.Invalide;
        }

        /// <summary>
        /// 进入节点
        /// </summary>
        protected virtual void OnEnter()
        {
            AddAsExecutingNode();
        }

        /// <summary>
        /// 执行节点
        /// </summary>
        protected virtual EBTNodeRunningState OnExecute()
        {
            return EBTNodeRunningState.Success;
        }

        /// <summary>
        /// 退出节点
        /// </summary>
        protected virtual void OnExit()
        {
            // 节点判定完成(成功或失败)时做一些事情
            Reset();
        }

        /// <summary>
        /// 是否可被重新评估(需要参与重新评估判定的重写支持)
        /// </summary>
        /// <returns></returns>
        protected virtual bool CanReevaluate()
        {
            return false;
        }

        /// <summary>
        /// 运行时检查是否是CS节点
        /// </summary>
        /// <returns></returns>
        public bool CheckIsCSNodeInRunTime()
        {
            return IsCSNode;
        }
        #endregion

        #region 编辑器部分
        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="childnodeuid"></param>
        /// <param name="graph"></param>
        /// <param name="insertindex">默认不传往尾部插入</param>
        public bool AddChildNode(int childnodeuid, BTGraph graph, int? insertindex = null)
        {
            if (insertindex == null)
            {
                insertindex = ChildNodesUIDList.Count;
            }
            if (ChildNodesUIDList.Contains(childnodeuid) == false)
            {
                ChildNodesUIDList.Insert((int)insertindex, childnodeuid);
                Log.Info($"节点UID:{UID}插入子节点位置:{insertindex}UID:{childnodeuid}");
                SortAndUpdateChildeNode();
                return true;
            }
            else
            {
                Debug.LogError($"节点名:{NodeName}不允许添加重复UID:{childnodeuid}!");
                return false;
            }
        }

        /// <summary>
        /// 删除子节点
        /// </summary>
        /// <param name="childnodeuid"></param>
        /// <param name="graph"></param>
        public bool DeleteChildNode(int childnodeuid, BTGraph graph)
        {
            if (ChildNodesUIDList.Remove(childnodeuid))
            {
                Log.Info($"父节点UID:{UID}删除子节点UID:{childnodeuid}");
                Log.Info($"父节点UID:{UID}剩余子节点数量:{ChildNodesUIDList.Count}");
                SortAndUpdateChildeNode();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除所有子节点
        /// </summary>
        public void DeleteAllChildNodes()
        {
            ChildNodesUIDList.Clear();
        }

        /// <summary>
        /// 排序并更新节点子节点ID顺序以及索引顺序
        /// </summary>
        /// <param name="node"></param>
        public void SortAndUpdateChildeNode()
        {
            if (ChildNodesUIDList.Count > 0)
            {
                var childnodelist = new List<BTNode>();
                for (int i = 0, length = ChildNodesUIDList.Count; i < length; ++i)
                {
                    var childnode = OwnerBTGraph.FindNodeByUID(ChildNodesUIDList[i]);
                    childnodelist.Add(childnode);
                }
                childnodelist.Sort(onBTNodeOrderComparison);
                for (int i = 0, length = childnodelist.Count; i < length; i++)
                {
                    childnodelist[i].NodeIndex = i;
                    // 确保ChildNodesUIDList里的顺序和NodeIndex一致
                    ChildNodesUIDList[i] = childnodelist[i].UID;
                }
            }
        }

        /// <summary>
        /// BTNode节点Order排序
        /// </summary>
        /// <param name="node1"></param>
        /// <param name="node2"></param>
        /// <returns></returns>
        private int onBTNodeOrderComparison(BTNode node1, BTNode node2)
        {
            return node1.NodeDisplayRect.x.CompareTo(node2.NodeDisplayRect.x);
        }

        /// <summary>
        /// 判定位置是否在指定节点区域内
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool UnderRectArea(Vector2 pos)
        {
            return NodeDisplayRect.Contains(pos);
        }

        /// <summary>
        /// 节点移动
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="offset"></param>
        /// <param name="recursive"></param>
        public void Move(BTGraph graph, Vector2 offset, bool recursive = false)
        {
            NodeDisplayRect.x = NodeDisplayRect.x + offset.x;
            NodeDisplayRect.y = NodeDisplayRect.y + offset.y;
            if (recursive)
            {
                for (int i = 0, length = ChildNodesUIDList.Count; i < length; i++)
                {
                    var childnode = graph.FindNodeByUID(ChildNodesUIDList[i]);
                    childnode.Move(graph, offset, recursive);
                }
            }
        }

        /// <summary>
        /// 是否是有效节点
        /// </summary>
        /// <returns></returns>
        public bool IsValideNode()
        {
            if (this.NodeType == (int)EBTNodeType.DecorationNodeType)
            {
                if (this.ChildNodesUIDList.Count != 1)
                {
                    Debug.LogError($"装饰节点UID:{this.UID}有且必须拥有一个子节点!");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (this.NodeType == (int)EBTNodeType.CompositeNodeType)
            {
                if (this.ChildNodesUIDList.Count == 0)
                {
                    Debug.LogError($"组合节点UID:{this.UID}至少要有一个子节点!");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (this.NodeParams.Contains("，"))
            {
                Debug.LogError($"组合节点UID:{this.UID}参数不允许包含中文逗号,请确认是否填写错误!");
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 获取指定节点类型默认打断类型
        /// </summary>
        /// <param name="nodetype"></param>
        /// <returns></returns>
        public EAbortType GetNodeDefaultAbortType(EBTNodeType nodetype)
        {
            if (nodetype == EBTNodeType.ConditionNodeType)
            {
                return EAbortType.Self;
            }
            else
            {
                return EAbortType.None;
            }
        }

        /// <summary>
        /// 编辑器检查是否是CS节点
        /// </summary>
        /// <returns></returns>
        public bool CheckIsCSNodeInEditor()
        {
            var index = Array.FindIndex(BTData.BTActionNodeNameArray, (nodename) =>
            {
                return string.Equals(nodename, NodeName);
            });
            if (index == -1)
            {
                index = Array.FindIndex(BTData.BTConditionNodeNameArray, (nodename) =>
                {
                    return string.Equals(nodename, NodeName);
                });
            }
            if (index == -1)
            {
                index = Array.FindIndex(BTData.BTCompositeNodeNameArray, (nodename) =>
                {
                    return string.Equals(nodename, NodeName);
                });
            }
            if (index == -1)
            {
                index = Array.FindIndex(BTData.BTDecorationNodeNameArray, (nodename) =>
                {
                    return string.Equals(nodename, NodeName);
                });
            }
            return index != -1;
        }
        #endregion

        #region 通用部分
        /// <summary>
        /// 是否是根节点
        /// </summary>
        /// <returns></returns>
        public bool IsRootNode()
        {
            return ParentNodeUID == 0;
        }

        /// <summary>
        /// 是否有父节点
        /// </summary>
        /// <returns></returns>
        public bool HasParentNode()
        {
            return ParentNodeUID != 0;
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <returns></returns>
        public BTNode GetParentNode()
        {
            if (HasParentNode())
            {
                var parentnode = OwnerBTGraph.FindNodeByUID(ParentNodeUID);
                return parentnode;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 是否是叶子节点
        /// </summary>
        /// <returns></returns>
        public bool IsLeafNode()
        {
            return ChildNodesUIDList.Count == 0;
        }

        /// <summary>
        /// 是否是入口节点
        /// </summary>
        /// <returns></returns>
        public bool IsEntryNode()
        {
            return NodeType == (int)EBTNodeType.EntryNodeType;
        }

        /// <summary>
        /// 是否是行为节点
        /// </summary>
        /// <returns></returns>
        public bool IsActionNode()
        {
            return NodeType == (int)EBTNodeType.ActionNodeType;
        }

        /// <summary>
        /// 是否是条件节点
        /// </summary>
        /// <returns></returns>
        public bool IsConditionNode()
        {
            return NodeType == (int)EBTNodeType.ConditionNodeType;
        }

        /// <summary>
        /// 是否是组合节点
        /// </summary>
        /// <returns></returns>
        public bool IsCompositionNode()
        {
            return NodeType == (int)EBTNodeType.CompositeNodeType;
        }

        /// <summary>
        /// 是否是装饰节点
        /// </summary>
        /// <returns></returns>
        public bool IsDecorationNode()
        {
            return NodeType == (int)EBTNodeType.DecorationNodeType;
        }
        #endregion
    }
}