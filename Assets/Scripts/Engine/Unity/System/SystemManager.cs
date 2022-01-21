using UnityEngine;
using YoukiaCore;
using YoukiaCore.Event;
using YoukiaCore.Log;
using YoukiaCore.Timer;

namespace Framework
{
    /// <summary>
    /// 系统管理器
    /// <para>只能初始化和框架系统有关的内容</para>
    /// </summary>
    public class SystemManager : MonoSingleton<SystemManager>
    {
        public override void Startup()
        {
            base.Startup();
        }

        protected override void Init()
        {
            base.Init();
            TimerManager.Init(new UnityGetTime());
        }

        /// <summary>
        /// 设置打印级别
        /// </summary>
        /// <param name="level"></param>
        public void InitLogLevel(Log.LogLevel level)
        {
            Log.Init(new UnityLogger());
            Log.Level = level;
        }

        void Update()
        {
            TimerManager.Update();
            BaseEvent.UpdateAll();
        }

        public override void Dispose()
        {
            base.Dispose();
            TimerManager.Clear();
        }
    }
}
