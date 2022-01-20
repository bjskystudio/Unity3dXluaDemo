using System;
using UnityEngine;
using XLua;

namespace LuaBehaviourTree
{
    [LuaCallCSharp]
    public static class LuaBehaviourTreeUtils
    {
        #region Lua侧静态委托相关
        /// <summary>
        /// Lua测CreateLuaLuaBTNode方法绑定
        /// </summary>
        public static Func<string, int, string, long, int, int> LuaCreateLuaBTnode = null;
        /// <summary>
        /// Lua测UnbindLuaBTNodeCall方法绑定
        /// </summary>
        public static Action<int, int> UnbindLuaBTNodeCall = null;
        /// <summary>
        /// 行为树节点暂停回调
        /// </summary>
        public static Action<int, int, bool> LuaOnPause;
        /// <summary>
        /// 行为树节点重置回调
        /// </summary>
        public static Action<int, int> LuaReset;
        /// <summary>
        /// 行为树节点进入回调
        /// </summary>
        public static Action<int, int> LuaOnEnter;
        /// <summary>
        /// 行为树节点执行回调
        /// </summary>
        public static Func<int, int, int> LuaOnExecute;
        /// <summary>
        /// 行为树节点退出回调
        /// </summary>
        public static Action<int, int> LuaOnExit;
        /// <summary>
        /// 行为树节点释放回调
        /// </summary>
        public static Action<int, int> LuaDispose;
        #endregion

        #region 添加和移除

        /// <summary>
        /// 为指定对象加载AI
        /// </summary>
        /// <param name="uid">场景对象uid</param>
        /// <param name="aiAssetPath">AI配置路径</param>
        public static void AddAI(long uid, string aiAssetPath)
        {
            RoleControler role = RoleControlerManager.GetRole(uid);
            if (role != null)
            {
                role.AddAI(aiAssetPath);
            }
        }
        /// <summary>
        /// 移除指定对象的AI
        /// </summary>
        /// <param name="uid">场景对象uid</param>
        public static void RemoveAI(long uid)
        {
            RoleControler role = RoleControlerManager.GetRole(uid);
            if (role != null)
            {
                role.RemoveAI();
            }
        }

        #endregion


        #region 控制

        /// <summary>
        /// 暂停所有行为树
        /// </summary>
        public static void PauseAll()
        {
            TBehaviourTreeManager.Instance.PauseAll();
        }

        /// <summary>
        /// 暂停指定对象的行为树
        /// </summary>
        public static void PauseAI(long uid)
        {
            RoleControlerManager.GetRole(uid)?.GetAI()?.Pause();
        }

        /// <summary>
        /// 继续所有行为树
        /// </summary>
        public static void ResumeAll()
        {
            TBehaviourTreeManager.Instance.ResumeAll();
        }

        /// <summary>
        /// 继续指定对象的行为树
        /// </summary>
        public static void ResumeAI(long uid)
        {
            RoleControlerManager.GetRole(uid)?.GetAI()?.Resume();
        }

        /// <summary>
        /// 打断所有行为树
        /// </summary>
        public static void AbortAll()
        {
            TBehaviourTreeManager.Instance.AbortAll();
        }

        /// <summary>
        /// 打断指定对象的行为树
        /// </summary>
        public static void AbortAI(long uid)
        {
            RoleControlerManager.GetRole(uid)?.GetAI()?.Abort();
        }

        #endregion

        #region 扩展

        /// <summary>
        /// 添加行为树
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="uid">功能对象的唯一UID</param>
        /// <param name="aiAssetPath">行为树资源路径</param>
        public static void AddBehaviourTree(this GameObject gameObject, long uid, string aiAssetPath)
        {
            if (gameObject == null)
                return;
            var behaviourTree = gameObject.GetOrAddComponent<TBehaviourTree>();
            behaviourTree.SetWorldItemUID(uid);
            behaviourTree.LoadBTGraphAsset(aiAssetPath);
        }

        public static void AddBehaviourTree(this Component component, long uid, string aiAssetPath)
        {
            AddBehaviourTree(component?.gameObject, uid, aiAssetPath);
        }

        /// <summary>
        /// 移除行为树
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="uid"></param>
        /// <param name="aiAssetPath"></param>
        public static void RemoveBehaviourTree(this GameObject gameObject)
        {
            if (gameObject == null)
                return;
            var behaviourTree = gameObject.GetOrAddComponent<TBehaviourTree>();
            if (behaviourTree != null)
            {
                behaviourTree.Pause();
                GameObject.Destroy(behaviourTree);
            }
        }

        public static void RemoveBehaviourTree(this Component component)
        {
            RemoveBehaviourTree(component?.gameObject);
        }

        #endregion
    }
}
