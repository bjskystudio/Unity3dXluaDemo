namespace LuaBehaviourTree
{
    /// <summary>
    /// 入口节点
    /// </summary>
    public class Entry : BTNode
    {
        /// <summary>
        /// 子节点
        /// </summary>
        public BTNode ChildNode;

        public Entry()
        {

        }

        public Entry(BTNode node, TBehaviourTree btowner, BTNode parentnode, long worlditemuid, int instanceid) : base(node, btowner, parentnode, worlditemuid, instanceid)
        {
            if (node.ChildNodesUIDList.Count > 0)
            {
                ChildNode = BTUtilities.CreateRunningNodeByNode(btowner.BTOriginalGraph.FindNodeByUID(node.ChildNodesUIDList[0]), btowner, this, worlditemuid, instanceid);
            }
        }

        /// <summary>
        /// 设置数据(运行时用对象池后的调用初始化数据)
        /// </summary>
        /// <param name="node"></param>
        /// <param name="btowner"></param>
        /// <param name="parentnode"></param>
        /// <param name="worlditemuid"></param>
        /// <param name="instanceid"></param>
        public override void SetDatas(BTNode node, TBehaviourTree btowner, BTNode parentnode, long worlditemuid, int instanceid)
        {
            base.SetDatas(node, btowner, parentnode, worlditemuid, instanceid);
            if (node.ChildNodesUIDList.Count > 0)
            {
                ChildNode = BTUtilities.CreateRunningNodeByNode(btowner.BTOriginalGraph.FindNodeByUID(node.ChildNodesUIDList[0]), btowner, this, worlditemuid, instanceid);
            }
        }

        protected override EBTNodeRunningState OnExecute()
        {
            return ChildNode.OnUpdate();
        }
    }
}
