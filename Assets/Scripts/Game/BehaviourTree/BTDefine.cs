namespace LuaBehaviourTree
{
    /// <summary>
    /// 行为树节点类型枚举
    /// </summary>
    public enum EBTNodeType
    {
        /// <summary>
        /// 入口节点类型(根节点才为此类型)
        /// </summary>
        EntryNodeType = 1,
        /// <summary>
        /// 行为节点类型
        /// </summary>
        ActionNodeType,
        /// <summary>
        /// 组合节点类型
        /// </summary>
        CompositeNodeType,
        /// <summary>
        /// 条件节点类型
        /// </summary>
        ConditionNodeType,
        /// <summary>
        /// 装饰节点类型
        /// </summary>
        DecorationNodeType,
    }

    /// <summary>
    /// 行为树节点运行状态枚举
    /// </summary>
    public enum EBTNodeRunningState
    {
        /// <summary>
        /// 无效状态
        /// </summary>
        Invalide = 1,
        /// <summary>
        /// 运行中
        /// </summary>
        Running,
        /// <summary>
        /// 成功
        /// </summary>
        Success,
        /// <summary>
        /// 失败
        /// </summary>
        Failed,
    }

    /// <summary>
    /// 节点打断类型(参考BehaviourDesigner里的概念)
    /// Note:
    /// 当前设计仅条件节点允许设置成可被打断
    /// </summary>
    public enum EAbortType
    {
        /// <summary>
        /// 不可被打断
        /// </summary>
        None = 1,
        /// <summary>
        /// 可被打断
        /// </summary>
        Self,
    }
}
