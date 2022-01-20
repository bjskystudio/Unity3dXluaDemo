namespace LuaBehaviourTree
{
    /// <summary>
    /// 设置自定义变量Int行为节点
    /// </summary>
    public class SetShareInt : BaseAction
    {
        /// <summary>
        /// 需要改变的自定义变量名
        /// </summary>
        protected string mVariableName;

        /// <summary>
        /// 目标自定义变量数据
        /// </summary>
        protected int mTargetVariableValue;

        #region 运行时部分
        public SetShareInt()
        {

        }

        public SetShareInt(BTNode node, TBehaviourTree btowner, BTNode parentnode, long worlditemuid, int instanceid) : base(node, btowner, parentnode, worlditemuid, instanceid)
        {
            var variablenodedata = OwnerBTGraph.GetVariableNodeValueInRuntime(this.UID) as CustomIntVariableNodeData;
            mVariableName = variablenodedata.VariableName;
            mTargetVariableValue = variablenodedata.VariableValue;
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
            var variablenodedata = OwnerBTGraph.GetVariableNodeValueInRuntime(this.UID) as CustomIntVariableNodeData;
            mVariableName = variablenodedata.VariableName;
            mTargetVariableValue = variablenodedata.VariableValue;
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
            var result = OwnerBTGraph.UpdateData<int>(mVariableName, mTargetVariableValue);
            return result ? EBTNodeRunningState.Success : EBTNodeRunningState.Failed;
        }

        /// <summary>
        /// 退出节点
        /// </summary>
        protected override void OnExit()
        {
            base.OnExit();
        }
        #endregion
    }
}