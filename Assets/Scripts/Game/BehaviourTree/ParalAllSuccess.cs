namespace LuaBehaviourTree
{
    /// <summary>
    /// 所有成功策略并发节点
    /// </summary>
    public class ParalAllSuccess : BaseParal
    {
        public ParalAllSuccess()
        {

        }

        public ParalAllSuccess(BTNode node, TBehaviourTree btowner, BTNode parentnode, long worlditemuid, int instanceid) : base(node, btowner, parentnode, worlditemuid, instanceid)
        {
            OnCreate();
        }

        /// <summary>
        /// 从池里
        /// </summary>
        public override void OnCreate()
        {
            base.OnCreate();
            ParalPolicy = EBTParalPolicy.AllSuccess;
        }

        protected override EBTNodeRunningState OnExecute()
        {
            var successcount = 0;
            for (int i = 0, length = ChildNodes.Count; i < length; i++)
            {
                // 已经执行完成的直接采用之前的结果
                if (mChildNodeExecuteStateList[i] == EBTNodeRunningState.Failed)
                {
                    return EBTNodeRunningState.Failed;
                }
                else if(mChildNodeExecuteStateList[i] == EBTNodeRunningState.Success)
                {
                    successcount++;
                }
                else
                {
                    EBTNodeRunningState childnodestate = ChildNodes[i].NodeRunningState;
                    if (!ChildNodes[i].IsTerminated)
                    {
                        childnodestate = ChildNodes[i].OnUpdate();
                    }
                    if (childnodestate == EBTNodeRunningState.Success)
                    {
                        successcount++;
                        mChildNodeExecuteStateList[i] = EBTNodeRunningState.Success;
                    }
                    else if (childnodestate == EBTNodeRunningState.Failed)
                    {
                        mChildNodeExecuteStateList[i] = EBTNodeRunningState.Failed;
                        return EBTNodeRunningState.Failed;
                    }
                }
            }
            if (successcount == ChildNodes.Count)
            {
                return EBTNodeRunningState.Success;
            }
            else
            {
                return EBTNodeRunningState.Running;
            }
        }
    }
}
