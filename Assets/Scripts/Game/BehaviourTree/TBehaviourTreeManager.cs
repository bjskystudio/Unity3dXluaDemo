using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using YoukiaCore.Log;

namespace LuaBehaviourTree
{
    /// <summary>
    /// 行为树管理单例类
    /// </summary>
    public class TBehaviourTreeManager : MonoSingleton<TBehaviourTreeManager>
    {
        /// <summary>
        /// 所有有效的行为树列表
        /// </summary>
        public List<TBehaviourTree> AllBehaviourTreeList
        {
            get;
            set;
        }

        /// <summary>
        /// 是否暂停所有
        /// </summary>
        public bool IsPauseAll;

        public TBehaviourTreeManager()
        {
            AllBehaviourTreeList = new List<TBehaviourTree>();
            IsPauseAll = false;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        protected override void Init()
        {
            base.Init();
            // 初始化AI节点对象池
            ObjectPool.Instance.Initialize<Entry>(10);
            foreach (var type in GetType().Assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(Composition)) && type.IsClass && !type.IsAbstract))
            {
                //Log.Info($"继承至Composition的子类名:{type.Name}");
                ObjectPool.Instance.Initialize<Composition>(type, 20);
            }
            foreach (var type in GetType().Assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(Decoration)) && type.IsClass && !type.IsAbstract))
            {
                //Log.Info($"继承至Decoration的子类名:{type.Name}");
                ObjectPool.Instance.Initialize<Decoration>(type, 5);
            }
            var luaactiontype = typeof(LuaAction);
            var luaconditiontype = typeof(LuaCondition);
            foreach (var type in GetType().Assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(BaseAction)) && type.IsClass && !type.IsAbstract && !type.Equals(luaactiontype)))
            {
                //Log.Info($"继承至BaseAction的子类名:{type.Name}");
                ObjectPool.Instance.Initialize<BaseAction>(type, 5);
            }
            foreach (var type in GetType().Assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(BaseCondition)) && type.IsClass && !type.IsAbstract && !type.Equals(luaconditiontype)))
            {
                //Log.Info($"继承至BaseCondition的子类名:{type.Name}");
                ObjectPool.Instance.Initialize<BaseCondition>(type, 5);
            }
            ObjectPool.Instance.Initialize<LuaCondition>(100);
            ObjectPool.Instance.Initialize<LuaAction>(100);
        }

        private void OnDestroy()
        {
            AllBehaviourTreeList.Clear();
        }

        private void Update()
        {
            if (!IsPauseAll)
            {
                for (int i = AllBehaviourTreeList.Count - 1; i >= 0; i--)
                {
                    AllBehaviourTreeList[i].OnUpdate();
                }
            }
        }

        /// <summary>
        /// 注册指定行为树对象
        /// </summary>
        /// <param name="bt"></param>
        /// <returns></returns>
        public bool RegisterTBehaviourTree(TBehaviourTree bt)
        {
            if (AllBehaviourTreeList.Contains(bt) == false)
            {
                AllBehaviourTreeList.Add(bt);
                return true;
            }
            else
            {
                Log.Error($"重复注册指定行为树，挂载对象名:{bt.name}!");
                return false;
            }
        }

        /// <summary>
        /// 取消注册指定行为树对象
        /// </summary>
        /// <param name="bt"></param>
        /// <returns></returns>
        public bool UnregisterTBhaviourTree(TBehaviourTree bt)
        {
            return AllBehaviourTreeList.Remove(bt);
        }

        /// <summary>
        /// 暂停所有行为树
        /// </summary>
        public void PauseAll()
        {
            IsPauseAll = true;
            foreach (var bt in AllBehaviourTreeList)
            {
                bt.Pause();
            }
        }

        /// <summary>
        /// 继续所有行为树
        /// </summary>
        public void ResumeAll()
        {
            IsPauseAll = false;
            foreach (var bt in AllBehaviourTreeList)
            {
                bt.Resume();
            }
        }

        /// <summary>
        /// 打断所有行为树
        /// </summary>
        public void AbortAll()
        {
            foreach (var bt in AllBehaviourTreeList)
            {
                bt.Abort();
            }
        }
    }
}