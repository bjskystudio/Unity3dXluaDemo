namespace Framework
{
    /// <summary>
    /// 对象可回收接口设计
    /// </summary>
    public interface IRecycle
    {
        /// <summary>
        /// 创建时调用接口
        /// </summary>
        void OnCreate();

        /// <summary>
        /// 回收时调用接口
        /// </summary>
        void OnDispose();
    }
}
