namespace LuaBehaviourTree
{
    /**
    组合节点和修饰节点的定义写在这里，然后去BTUtilities去添加创建定义
    **/

    /// <summary>
    /// 行为树相关数据
    /// </summary>
    public static class BTData
    {
        /// <summary>
        /// 行为树节点存储目录相对路径
        /// </summary>
        public const string BTNodeSaveFolderRelativePath = "BehaviourTree";

        /// <summary>
        /// CS组合节点名数据
        /// </summary>
        public static string[] BTCompositeNodeNameArray = { typeof(Selector).Name, typeof(Sequence).Name, typeof(ParalAllSuccess).Name, typeof(ParalOneSuccess).Name, typeof(RandomSelector).Name };

        /// <summary>
        /// CS修饰节点名数据
        /// </summary>
        public static string[] BTDecorationNodeNameArray = { typeof(InverterDecoration).Name, typeof(RepeatedDecoration).Name };

        /// <summary>
        /// CS条件节点名数据(注意和BTNodeData.cs里保持一致)
        /// </summary>
        public static string[] BTConditionNodeNameArray = { typeof(CompareShareBool).Name, typeof(CompareShareInt).Name, typeof(CompareShareFloat).Name, typeof(CompareShareString).Name };

        /// <summary>
        /// CS行为节点名数据(注意和BTNodeData.cs里保持一致)
        /// </summary>
        public static string[] BTActionNodeNameArray = { typeof(SetShareBool).Name, typeof(SetShareInt).Name, typeof(SetShareFloat).Name, typeof(SetShareString).Name };
    }
}