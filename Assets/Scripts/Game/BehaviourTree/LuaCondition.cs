namespace LuaBehaviourTree
{
    /// <summary>
    /// Lua条件节点
    /// </summary>
    public class LuaCondition : BaseCondition
    {
        public LuaCondition()
        {

        }

        public LuaCondition(BTNode node, TBehaviourTree btowner, BTNode parentnode, long worlditemuid, int instanceid) : base(node, btowner, parentnode, worlditemuid, instanceid)
        {
            BTUtilities.CreateLuaNode(this, worlditemuid, instanceid);
        }

        public override void SetDatas(BTNode node, TBehaviourTree btowner, BTNode parentnode, long worlditemuid, int instanceid)
        {
            base.SetDatas(node, btowner, parentnode, worlditemuid, instanceid);
            BTUtilities.CreateLuaNode(this, worlditemuid, instanceid);
        }

        /// <summary>
        /// 释放
        /// </summary>
        public override void Dispose()
        {
            // 因为base.Dispose()为重置数据，导致UnbindLuaBTNodeCall无法正确解除回调绑定
            // 所以必须在父类Dispose()之前调用
            LuaBehaviourTreeUtils.LuaDispose?.Invoke(InstanceID, UID);
            LuaBehaviourTreeUtils.UnbindLuaBTNodeCall(InstanceID, UID);
            base.Dispose();
        }

        /// <summary>
        /// 响应暂停
        /// </summary>
        /// <param name="ispause"></param>
        public override void OnPause(bool ispause)
        {
            base.OnPause(ispause);
            LuaBehaviourTreeUtils.LuaOnPause?.Invoke(InstanceID, UID, ispause);
        }

        /// <summary>
        /// 重置节点状态
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            LuaBehaviourTreeUtils.LuaReset?.Invoke(InstanceID, UID);
        }

        protected override void OnEnter()
        {
            base.OnEnter();
            LuaBehaviourTreeUtils.LuaOnEnter?.Invoke(InstanceID, UID);
        }

        protected override EBTNodeRunningState OnExecute()
        {
            return (EBTNodeRunningState)LuaBehaviourTreeUtils.LuaOnExecute?.Invoke(InstanceID, UID);
        }

        /// <summary>
        /// 退出节点
        /// </summary>
        protected override void OnExit()
        {
            base.OnExit();
            LuaBehaviourTreeUtils.LuaOnExit?.Invoke(InstanceID, UID);
        }
    }
}