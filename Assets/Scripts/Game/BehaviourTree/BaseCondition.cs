namespace LuaBehaviourTree
{
    /// <summary>
    /// 条件节点基类
    /// </summary>
    public abstract class BaseCondition : BTNode
    {
        public BaseCondition()
        {

        }

        public BaseCondition(BTNode node, TBehaviourTree btowner, BTNode parentnode, long worlditemuid, int instanceid) : base(node, btowner, parentnode, worlditemuid, instanceid)
        {

        }

        /// <summary>
        /// 释放
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
        }

        /// <summary>
        /// 重置节点状态
        /// </summary>
        public override void Reset()
        {
            base.Reset();
        }

        protected override void OnEnter()
        {
            base.OnEnter();
        }

        protected override EBTNodeRunningState OnExecute()
        {
            return base.OnExecute();
        }

        /// <summary>
        /// 退出节点
        /// </summary>
        protected override void OnExit()
        {
            base.OnExit();
        }

        /// <summary>
        /// 是否可被重新评估
        /// </summary>
        /// <returns></returns>
        protected override bool CanReevaluate()
        {
            return AbortType == EAbortType.Self;
        }
    }
}