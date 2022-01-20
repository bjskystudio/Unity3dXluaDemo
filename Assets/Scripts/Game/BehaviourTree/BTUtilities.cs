using Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace LuaBehaviourTree
{
    /// <summary>
    /// BTUtilities.cs
    /// 行为树静态工具类
    /// </summary>
    public static class BTUtilities
    {
        #region 静态成员
        /// <summary>
        /// 行为树节点名类型信息缓存映射Map(优化行为树节点反射创建)
        /// Key为节点类名，Value为对应类型信息
        /// </summary>
        public static Dictionary<string, Type> BTNodeTypeCacheMap = new Dictionary<string, Type>();

        /// <summary>
        /// 当前类所在Assembly
        /// </summary>
        public static Assembly CurrentLocatedAssembly = typeof(BTUtilities).Assembly;

        /// <summary>
        /// BTNode类型
        /// </summary>
        public static Type BTNodeType = typeof(BTNode);

        /// <summary>
        /// CompareShareBool类型名
        /// </summary>
        public static string CompareShareBoolTypeName = typeof(CompareShareBool).Name;

        /// <summary>
        /// CompareShareInt类型名
        /// </summary>
        public static string CompareShareIntTypeName = typeof(CompareShareInt).Name;

        /// <summary>
        /// CompareShareFloat类型名
        /// </summary>
        public static string CompareShareFloatTypeName = typeof(CompareShareFloat).Name;

        /// <summary>
        /// CompareShareString类型名
        /// </summary>
        public static string CompareShareStringTypeName = typeof(CompareShareString).Name;

        /// <summary>
        /// SetShareBool类型名
        /// </summary>
        public static string SetShareBoolTypeName = typeof(SetShareBool).Name;

        /// <summary>
        /// SetShareInt类型名
        /// </summary>
        public static string SetShareIntTypeName = typeof(SetShareInt).Name;

        /// <summary>
        /// SetShareFloat类型名
        /// </summary>
        public static string SetShareFloatTypeName = typeof(SetShareFloat).Name;

        /// <summary>
        /// SetShareString类型名
        /// </summary>
        public static string SetShareStringTypeName = typeof(SetShareString).Name;
        #endregion

        #region 静态方法
        /// <summary>
        /// 根据节点数据创建运行时节点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="btowner"></param>
        /// <param name="parentnode"></param>
        /// <param name="worlditemuid"></param>
        /// <param name="instanceid"></param>
        /// <returns></returns>
        public static BTNode CreateRunningNodeByNode(BTNode node, TBehaviourTree btowner, BTNode parentnode, long worlditemuid, int instanceid)
        {
            switch ((EBTNodeType)node.NodeType)
            {
                case EBTNodeType.ActionNodeType:
                    var actionnode = CreateActionNode(node, btowner, parentnode, worlditemuid, instanceid);
                    if (btowner.BTRunningGraph.AddNode(actionnode))
                    {
                        return actionnode;
                    }
                    else
                    {
                        return null;
                    }
                case EBTNodeType.ConditionNodeType:
                    var conditionnode = CreateConditionNode(node, btowner, parentnode, worlditemuid, instanceid);
                    if (btowner.BTRunningGraph.AddNode(conditionnode))
                    {
                        return conditionnode;
                    }
                    else
                    {
                        return null;
                    }
                case EBTNodeType.CompositeNodeType:
                    var compositenode = CreateCompositionNode(node, btowner, parentnode, worlditemuid, instanceid);
                    if (btowner.BTRunningGraph.AddNode(compositenode))
                    {
                        return compositenode;
                    }
                    else
                    {
                        return null;
                    }
                case EBTNodeType.DecorationNodeType:
                    var decorationnode = CreateDecorationNode(node, btowner, parentnode, worlditemuid, instanceid);
                    if (btowner.BTRunningGraph.AddNode(decorationnode))
                    {
                        return decorationnode;
                    }
                    else
                    {
                        return null;
                    }
                case EBTNodeType.EntryNodeType:
                    var entrynode = CreateEntryNode(node, btowner, parentnode, worlditemuid, instanceid);
                    if (btowner.BTRunningGraph.AddNode(entrynode))
                    {
                        return entrynode;
                    }
                    else
                    {
                        return null;
                    }
                default:
                    Debug.LogError($"不支持的节点类型:{node.NodeType}");
                    return null;
            }
        }

        /// <summary>
        /// 创建组合节点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="btowner"></param>
        /// <param name="parentnode"></param>
        /// <param name="worlditemuid"></param>
        /// <param name="instanceid"></param>
        /// <returns></returns>
        public static Composition CreateCompositionNode(BTNode node, TBehaviourTree btowner, BTNode parentnode, long worlditemuid, int instanceid)
        {
            // 考虑到扩展性，最终还是选择采用反射的形式创建CS节点类
            var compositenode = TryGetBTNodeTypeInstance<Composition>(node.NodeName);
            compositenode?.SetDatas(node, btowner, parentnode, worlditemuid, instanceid);
            return compositenode;

            /**
            //BTCompositionNode btcompositenode = null;
            //// 没统一使用反射增加扩展性的原因是考虑到性能
            //if (node.NodeName == BTData.BTCompositeNodeNameArray[0])
            //{
            //    btcompositenode = ObjectPool.Singleton.Pop<BTSelectorNode>();
            //}
            //else if(node.NodeName == BTData.BTCompositeNodeNameArray[1])
            //{
            //    btcompositenode = ObjectPool.Singleton.Pop<BTSequenceNode>();
            //}
            //else if(node.NodeName == BTData.BTCompositeNodeNameArray[2])
            //{
            //    btcompositenode = ObjectPool.Singleton.Pop<BTParalAllSuccessNode>();
            //}
            //else if (node.NodeName == BTData.BTCompositeNodeNameArray[3])
            //{
            //    btcompositenode = ObjectPool.Singleton.Pop<BTParalOneSuccessNode>();
            //}
            //else if(node.NodeName == BTData.BTCompositeNodeNameArray[4])
            //{
            //    btcompositenode = ObjectPool.Singleton.Pop<BTRandomSelectorNode>();
            //}
            //else
            //{
            //    Debug.LogError($"不支持的组合节点名:{node.NodeName},创建组合节点失败!");
            //    return null;
            //}
            //btcompositenode.SetDatas(node, btowner, parentnode, worlditemuid, instanceid);
            //return btcompositenode;
            **/
        }

        /// <summary>
        /// 创建修饰节点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="btowner"></param>
        /// <param name="parentnode"></param>
        /// <param name="worlditemuid"></param>
        /// <param name="instanceid"></param>
        /// <returns></returns>
        public static Decoration CreateDecorationNode(BTNode node, TBehaviourTree btowner, BTNode parentnode, long worlditemuid, int instanceid)
        {
            // 考虑到扩展性，最终还是选择采用反射的形式创建CS节点类
            var decorationnode = TryGetBTNodeTypeInstance<Decoration>(node.NodeName);
            decorationnode?.SetDatas(node, btowner, parentnode, worlditemuid, instanceid);
            return decorationnode;

            /**
            //BTDecorationNode btdecorationnode = null;
            //// 没统一使用反射增加扩展性的原因是考虑到性能
            //if (node.NodeName == BTData.BTDecorationNodeNameArray[0])
            //{
            //    btdecorationnode = ObjectPool.Singleton.Pop<BTInverterDecorationNode>();
            //}
            //else if(node.NodeName == BTData.BTDecorationNodeNameArray[1])
            //{
            //    btdecorationnode = ObjectPool.Singleton.Pop<BTRepeatedDecorationNode>();
            //}
            //else
            //{
            //    Debug.LogError($"不支持的修饰节点名:{node.NodeName},创建修饰节点失败!");
            //    return null;
            //}
            //btdecorationnode.SetDatas(node, btowner, parentnode, worlditemuid, instanceid);
            //return btdecorationnode;
            **/
        }

        /// <summary>
        /// 创建条件节点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="btowner"></param>
        /// <param name="parentnode"></param>
        /// <param name="worlditemuid"></param>
        /// <param name="instanceid"></param>
        /// <returns></returns>
        public static BaseCondition CreateConditionNode(BTNode node, TBehaviourTree btowner, BTNode parentnode, long worlditemuid, int instanceid)
        {
            var iscsnode = Application.isPlaying ? node.CheckIsCSNodeInRunTime() : node.CheckIsCSNodeInEditor();
            if (!iscsnode)
            {
                LuaCondition luaconditionnode = ObjectPool.Instance.Pop<LuaCondition>();
                luaconditionnode.SetDatas(node, btowner, parentnode, worlditemuid, instanceid);
                return luaconditionnode;
            }
            else
            {
                // 考虑到扩展性，最终还是选择采用反射的形式创建CS节点类
                var conditionnode = TryGetBTNodeTypeInstance<BaseCondition>(node.NodeName);
                conditionnode?.SetDatas(node, btowner, parentnode, worlditemuid, instanceid);
                return conditionnode;

                /**
                //// 为了避免反射带来的性能开销，这里采用穷举法判定创建具体CS节点类
                //if (node.NodeName.Equals(BTData.BTCSConditionNodeNameArray[0]))
                //{
                //    var btconditionnode = ObjectPool.Singleton.Pop<BTCompareShareBool>();
                //    btconditionnode.SetDatas(node, btowner, parentnode, worlditemuid, instanceid);
                //    return btconditionnode;
                //}
                //else if (node.NodeName.Equals(BTData.BTCSConditionNodeNameArray[1]))
                //{
                //    var btconditionnode = ObjectPool.Singleton.Pop<BTCompareShareInt>();
                //    btconditionnode.SetDatas(node, btowner, parentnode, worlditemuid, instanceid);
                //    return btconditionnode;
                //}
                //else if (node.NodeName.Equals(BTData.BTCSConditionNodeNameArray[2]))
                //{
                //    var btconditionnode = ObjectPool.Singleton.Pop<BTCompareShareFloat>();
                //    btconditionnode.SetDatas(node, btowner, parentnode, worlditemuid, instanceid);
                //    return btconditionnode;
                //}
                //else if (node.NodeName.Equals(BTData.BTCSConditionNodeNameArray[3]))
                //{
                //    var btconditionnode = ObjectPool.Singleton.Pop<BTCompareShareString>();
                //    btconditionnode.SetDatas(node, btowner, parentnode, worlditemuid, instanceid);
                //    return btconditionnode;
                //}
                //else
                //{
                //    Debug.LogError($"不支持创建的CS条件节点名:{node.NodeName},请检查代码!");
                //    return null;
                //}
                **/
            }
        }

        /// <summary>
        /// 创建动作节点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="btowner"></param>
        /// <param name="parentnode"></param>
        /// <param name="worlditemuid"></param>
        /// <param name="instanceid"></param>
        /// <returns></returns>
        public static BaseAction CreateActionNode(BTNode node, TBehaviourTree btowner, BTNode parentnode, long worlditemuid, int instanceid)
        {
            var iscsnode = Application.isPlaying ? node.CheckIsCSNodeInRunTime() : node.CheckIsCSNodeInEditor();
            if (!iscsnode)
            {
                LuaAction luaactionnode = ObjectPool.Instance.Pop<LuaAction>();
                luaactionnode.SetDatas(node, btowner, parentnode, worlditemuid, instanceid);
                return luaactionnode;
            }
            else
            {
                // 考虑到扩展性，最终还是选择采用反射的形式创建CS节点类
                var actionnode = TryGetBTNodeTypeInstance<BaseAction>(node.NodeName);
                actionnode?.SetDatas(node, btowner, parentnode, worlditemuid, instanceid);
                return actionnode;

                /**
                // 为了避免反射带来的性能开销，这里采用穷举法判定创建具体CS节点类
                if (node.NodeName.Equals(BTData.BTCSActionNodeNameArray[0]))
                {
                    var btactionnode = ObjectPool.Singleton.Pop<BTSetShareBool>();
                    btactionnode.SetDatas(node, btowner, parentnode, worlditemuid, instanceid);
                    return btactionnode;
                }
                else if (node.NodeName.Equals(BTData.BTCSActionNodeNameArray[1]))
                {
                    var btactionnode = ObjectPool.Singleton.Pop<BTSetShareInt>();
                    btactionnode.SetDatas(node, btowner, parentnode, worlditemuid, instanceid);
                    return btactionnode;
                }
                else if (node.NodeName.Equals(BTData.BTCSActionNodeNameArray[2]))
                {
                    var btactionnode = ObjectPool.Singleton.Pop<BTSetShareFloat>();
                    btactionnode.SetDatas(node, btowner, parentnode, worlditemuid, instanceid);
                    return btactionnode;
                }
                else if (node.NodeName.Equals(BTData.BTCSActionNodeNameArray[3]))
                {
                    var btactionnode = ObjectPool.Singleton.Pop<BTSetShareString>();
                    btactionnode.SetDatas(node, btowner, parentnode, worlditemuid, instanceid);
                    return btactionnode;
                }
                else
                {
                    Debug.LogError($"不支持创建的CS行为节点名:{node.NodeName},请检查代码!");
                    return null;
                }
                **/
            }
        }

        /// <summary>
        /// 创建入口节点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="btowner"></param>
        /// <param name="parentnode"></param>
        /// <param name="worlditemuid"></param>
        /// <param name="instanceid"></param>
        /// <returns></returns>
        public static Entry CreateEntryNode(BTNode node, TBehaviourTree btowner, BTNode parentnode, long worlditemuid, int instanceid)
        {
            Entry entrynode = ObjectPool.Instance.Pop<Entry>();
            entrynode.SetDatas(node, btowner, parentnode, worlditemuid, instanceid);
            return entrynode;
        }

        /// <summary>
        /// 创建绑定指定BTNode的Lua脚本对象
        /// </summary>
        /// <param name="node"></param>
        /// <param name="worlditemuid"></param>
        /// <param name="instanceid"></param>
        /// <returns>Lua节点UID</returns>
        public static int CreateLuaNode(BTNode node, long worlditemuid, int instanceid)
        {
            if (TBehaviourTreeManager.Instance != null && LuaBehaviourTreeUtils.LuaCreateLuaBTnode != null)
            {
                var uid = LuaBehaviourTreeUtils.LuaCreateLuaBTnode(node.NodeName, node.UID, node.NodeParams, worlditemuid, instanceid);
                return uid;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取一个唯一个UID
        /// </summary>
        /// <returns></returns>
        public static int GetNodeUID()
        {
            // 经测试，1000000次大概有100次重复的几率
            // 后续会结合本地是否使用过此UID来做二次验证确保唯一性
            byte[] buffer = Guid.NewGuid().ToByteArray();
            int uid = BitConverter.ToInt32(buffer, 0);
            return uid;
        }

        /// <summary>
        /// 指定节点名是否是否设置公共变量行为节点
        /// </summary>
        /// <param name="nodename"></param>
        /// <returns></returns>
        public static bool IsSetShareVariableAction(string nodename)
        {
            if (string.Equals(nodename, SetShareBoolTypeName) ||
                string.Equals(nodename, SetShareIntTypeName) ||
                string.Equals(nodename, SetShareFloatTypeName) ||
                string.Equals(nodename, SetShareStringTypeName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 指定节点名是否是否比较公共变量条件节点
        /// </summary>
        /// <param name="nodename"></param>
        /// <returns></returns>
        public static bool IsCompareToShareVariableCondition(string nodename)
        {
            if (string.Equals(nodename, CompareShareBoolTypeName) ||
                string.Equals(nodename, CompareShareIntTypeName) ||
                string.Equals(nodename, CompareShareFloatTypeName) ||
                string.Equals(nodename, CompareShareStringTypeName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取节点的变量类型
        /// </summary>
        /// <param name="nodename"></param>
        /// <returns></returns>
        public static EVariableType GetNodeVariableType(string nodename)
        {
            if (string.Equals(nodename, SetShareBoolTypeName) || string.Equals(nodename, CompareShareBoolTypeName))
            {
                return EVariableType.Bool;
            }
            else if (string.Equals(nodename, SetShareIntTypeName) || string.Equals(nodename, CompareShareIntTypeName))
            {
                return EVariableType.Int;
            }
            else if (string.Equals(nodename, SetShareFloatTypeName) || string.Equals(nodename, CompareShareFloatTypeName))
            {
                return EVariableType.Float;
            }
            else if (string.Equals(nodename, SetShareStringTypeName) || string.Equals(nodename, CompareShareStringTypeName))
            {
                return EVariableType.String;
            }
            else
            {
                return EVariableType.Invalide;
            }
        }

        /// <summary>
        /// 获取指定变量类型的节点变量默认值
        /// Note:
        /// 限非运行时用
        /// </summary>
        /// <param name="nodeuid"></param>
        /// <param name="variabletype"></param>
        /// <param name="variablename"></param>
        /// <returns></returns>
        public static CustomVariableNodeData GetVariableNodeDefaultValueInEditor(int nodeuid, string variablename, EVariableType variabletype)
        {
            if (variabletype == EVariableType.Bool)
            {
                return new CustomBoolVariableNodeData(nodeuid, variablename, variabletype, default);
            }
            else if (variabletype == EVariableType.Int)
            {
                return new CustomIntVariableNodeData(nodeuid, variablename, variabletype, default);
            }
            else if (variabletype == EVariableType.String)
            {
                return new CustomStringVariableNodeData(nodeuid, variablename, variabletype, default);
            }
            else if (variabletype == EVariableType.Float)
            {
                return new CustomFloatVariableNodeData(nodeuid, variablename, variabletype, default);
            }
            else
            {
                Debug.LogError($"不支持的变量类型:{variabletype},获取节点变量默认值失败!");
                return null;
            }
        }

        /// <summary>
        /// 尝试获取指定节点类型实例对象
        /// </summary>
        /// <param name="nodename"></param>
        /// <returns></returns>
        private static T TryGetBTNodeTypeInstance<T>(string nodename) where T : BTNode
        {
            // 考虑到扩展性，最终还是选择采用反射的形式创建CS节点类
            if (!BTNodeTypeCacheMap.TryGetValue(nodename, out Type nodetype))
            {
                var nodefullname = $"LuaBehaviourTree.{nodename}";
                nodetype = CurrentLocatedAssembly.GetType(nodefullname);
                if (BTNodeType.IsAssignableFrom(nodetype))
                {
                    BTNodeTypeCacheMap.Add(nodename, nodetype);
                    //Log.Info($"添加行为树节点类型:{nodename}缓存!");
                }
                else
                {
                    nodetype = null;
                }
            }
            if (nodetype != null)
            {
                // Note:
                // 带参的反射构建比不带参的默认构造函数反射构建耗时更多
                var btnode = ObjectPool.Instance.PopWithType(nodetype);
                return (T)btnode;
            }
            else
            {
                Debug.LogError($"节点名:{nodename}未继承至:BTNode类型，请检查代码!");
                return null;
            }
        }
        #endregion
    }
}