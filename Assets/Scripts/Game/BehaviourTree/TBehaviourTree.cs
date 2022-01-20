using ResourceLoad;
using UnityEngine;
using YoukiaCore.Log;

namespace LuaBehaviourTree
{
    /// <summary>
    /// Lua行为树挂载驱动组件
    /// </summary>
    [DisallowMultipleComponent]
    public class TBehaviourTree : MonoBehaviour
    {
        //         /// <summary>
        //         /// 行为树图数据
        //         /// </summary>
        //         public TextAsset BTGraphAsset;

        /// <summary>
        /// 当行为树完成时重新开始判定
        /// </summary>
        [Header("完成时重新开启判定(每帧判定)")]
        public bool RestartWhenComplete = true;

        /// <summary>
        /// 实体对象ID
        /// </summary>
        [Header("世界对象UID(暴露出来是为了方便查看，请勿修改)")]
        public int InstanceID;

        /// <summary>
        /// 世界对象UID(为了反向查逻辑对象)
        /// </summary>
        [Header("世界对象UID(暴露出来是为了方便查看，请勿修改)")]
        public long WorldItemUID;

        /// <summary>
        /// 运行时的行为树图数据(根据反序列化数据构建而成)
        /// </summary>
        public BTGraph BTRunningGraph
        {
            get;
            private set;
        }

        /// <summary>
        /// 行为树是否开启
        /// </summary>
        public bool IsBTEnable
        {
            get;
            private set;
        }

        /// <summary>
        /// 是否触发了Start
        /// </summary>
        public bool IsStart
        {
            get;
            private set;
        }

        /// <summary>
        /// 是否暂停
        /// </summary>
        public bool IsPaused;

        /// <summary>
        /// 行为树图原始数据对象(反序列化)
        /// </summary>
        public BTGraph BTOriginalGraph
        {
            get;
            private set;
        }

        /// <summary>
        /// 当前加载的行为树资源索引计数对象
        /// </summary>
        public ResRef BTGraphAssetRef
        {
            get;
            private set;
        }

        /// <summary>
        /// 等待加载的行为树Asset(用于解决默认隐藏状态下加载无法正确触发OnDestroy导致无法正确释放资源问题)
        /// </summary>
        private string mWaitLoadedAssetName;

        private void Awake()
        {
            InstanceID = gameObject.GetInstanceID();
        }

        /// <summary>
        /// 设置当前世界物体的UID(挂在脚本后必须调用指定绑定的世界对象)
        /// </summary>
        /// <param name="uid"></param>
        public void SetWorldItemUID(long uid)
        {
            //Log.Info($"设置实例对象ID:{gameObject.GetInstanceID()}的世界物体UID:{uid}");
            WorldItemUID = uid;
        }

        private void Start()
        {
            if (WorldItemUID == 0)
            {
                Log.Error($"挂在后请调用SetWorldItemUID()设置绑定世界对象UID!");
                return;
            }
            IsPaused = false;
            IsStart = true;
            TBehaviourTreeManager.Instance.RegisterTBehaviourTree(this);
        }

        private void OnEnable()
        {
            IsBTEnable = true;
            if (string.IsNullOrEmpty(mWaitLoadedAssetName) == false)
            {
                LoadBTGraphAsset(mWaitLoadedAssetName);
                mWaitLoadedAssetName = null;
            }
        }

        private void OnDisable()
        {
            IsBTEnable = false;
        }

        /// <summary>
        /// 触发行为树更新
        /// </summary>
        public void OnUpdate()
        {
            if (IsBTEnable && !IsPaused)
            {
                BTRunningGraph?.OnUpdate();
            }
        }

        private void OnDestroy()
        {
            if (IsStart)
            {
                if (TBehaviourTreeManager.IsInstance())
                    TBehaviourTreeManager.Instance.UnregisterTBhaviourTree(this);
            }
            ReleaseBTGraphAsset();
            IsBTEnable = false;
        }

        /// <summary>
        /// 加载行为树图数据
        /// </summary>
        /// <param name="assetname"></param>
        public void LoadBTGraphAsset(string assetname)
        {
            if (gameObject.activeInHierarchy)
            {
                //隐藏加载AI时可能没有得到正确的InstanceID
                if (InstanceID == 0)
                {
                    InstanceID = gameObject.GetInstanceID();
                }
                ReleaseBTGraphAsset();
                //Log.Info($"加载行为树资源:{assetname}");
                ResourceManager.Instance.LoadScriptableObject($"{BTData.BTNodeSaveFolderRelativePath}/{assetname}", OnLoadBTComplete);
            }
            else
            {
                // 隐藏状态下需要等待显示后再加载，
                //避免默认隐藏状态下直接销毁OnDestroy不会进，无法正确释放已加载的行为树
                //Log.Info($"实体对象:{gameObject.name}处于不显示状态，等待显示后再加载行为树:{assetname}");
                mWaitLoadedAssetName = assetname;
            }
        }

        /// <summary>
        /// 行为树资源加载成功
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        private void OnLoadBTComplete(ScriptableObject textAsset, ResRef resRef)
        {
            BTGraphAssetRef = resRef;
            BTOriginalGraph = textAsset as BTGraph;
            BTOriginalGraph.InitNoteData();
            BTRunningGraph = ScriptableObject.CreateInstance<BTGraph>();
            BTRunningGraph.SetBTOwner(this);
        }

        /// <summary>
        /// 释放行为树数据
        /// </summary>
        /// <returns></returns>
        public bool ReleaseBTGraphAsset()
        {
            if (BTGraphAssetRef != null)
            {
                //Log.Info($"释放行为树资源:{BTOriginalGraph?.name}");
                //BTOriginalGraph?.Dispose();//原始资源不能执行Dispose，清引用等AB释放
                BTOriginalGraph = null;
                BTRunningGraph?.Dispose();
                BTRunningGraph = null;
                BTGraphAssetRef?.Release();
                BTGraphAssetRef = null;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 暂停
        /// </summary>
        public void Pause()
        {
            IsPaused = true;
            BTRunningGraph?.OnPause(true);
        }

        /// <summary>
        /// 继续
        /// </summary>
        public void Resume()
        {
            IsPaused = false;
            BTRunningGraph?.OnPause(false);
        }

        /// <summary>
        /// 中断行为树
        /// </summary>
        public void Abort()
        {
            BTRunningGraph?.DoAbortBehaviourTree();
        }
    }
}