using System.Collections.Generic;

namespace LuaBehaviourTree
{
    /// <summary>
    /// 并发节点抽象基类
    /// </summary>
    public abstract class BaseParal : Composition
    {
        /// <summary>
        /// 并发策略
        /// </summary>
        public enum EBTParalPolicy
        {
            /// <summary>
            /// 所有都成功就算成功
            /// </summary>
            AllSuccess = 1,
            /// <summary>
            /// 一个成功就算成功
            /// </summary>
            OneSuccess,
        }

        /// <summary>
        /// 并发成功策略
        /// </summary>
        public EBTParalPolicy ParalPolicy
        {
            get;
            protected set;
        }

        /// <summary>
        /// 子节点执行结果列表
        /// </summary>
        protected List<EBTNodeRunningState> mChildNodeExecuteStateList;

        public BaseParal()
        {

        }

        public BaseParal(BTNode node, TBehaviourTree btowner, BTNode parentnode, long worlditemuid, int instanceid) : base(node, btowner, parentnode, worlditemuid, instanceid)
        {
            OnCreate();
        }

        public override void OnCreate()
        {
            base.OnCreate();
            mChildNodeExecuteStateList = new List<EBTNodeRunningState>();
        }

        public override void SetDatas(BTNode node, TBehaviourTree btowner, BTNode parentnode, long worlditemuid, int instanceid)
        {
            base.SetDatas(node, btowner, parentnode, worlditemuid, instanceid);
            foreach (var childnode in ChildNodes)
            {
                mChildNodeExecuteStateList.Add(childnode.NodeRunningState);
            }
        }

        /// <summary>
        /// 重置所有子节点执行状态
        /// </summary>
        protected void ResetAllChildNodeRunningState()
        {
            for (int i = 0, length = mChildNodeExecuteStateList.Count; i < length; i++)
            {
                mChildNodeExecuteStateList[i] = EBTNodeRunningState.Invalide;
            }
        }

        protected override void OnExit()
        {
            base.OnExit();
            ResetAllChildNodeRunningState();
        }

        /// <summary>
        /// 释放
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
            mChildNodeExecuteStateList = null;
        }
    }
}