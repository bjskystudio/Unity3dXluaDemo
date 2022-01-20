using UnityEngine;
using System.Collections;
using YoukiaCore.Utils;
using System.Collections.Generic;
using ResourceLoad;
using YoukiaCore.Log;
using System;

namespace Framework
{
    /// <summary>
    /// 缓存池管理器
    /// </summary>
    public class AssetPoolManager : MonoSingleton<AssetPoolManager>
    {
        /// <summary>
        /// 池列表
        /// </summary>
        private Dictionary<string, GameObject> Pools = new Dictionary<string, GameObject>();
        /// <summary>
        /// 池内对应的节点列表
        /// </summary>
        private Dictionary<string, Dictionary<string, List<GameObject>>> Nodes = new Dictionary<string, Dictionary<string, List<GameObject>>>();
        /// <summary>
        /// 已经取出的对象
        /// </summary>
        private Dictionary<GameObject, string> UsedDic = new Dictionary<GameObject, string>();

        public override void Startup()
        {
            base.Startup();
        }

        protected override void Init()
        {
            base.Init();
            CleanAllPool();
        }

        public override void Dispose()
        {
            base.Dispose();
            CleanAllPool();
        }

        /// <summary>
        /// 建立池
        /// </summary>
        /// <param name="poolName">池名字</param>
        public void CreatePool(string poolName)
        {
            if (string.IsNullOrEmpty(poolName))
            {
                Log.Error("不允许池名字为空!");
                return;
            }
            if (Pools.ContainsKey(poolName))
            {
                Log.Error($"对象池已存在!{poolName}");
                return;
            }
            GameObject pool = new GameObject(poolName);
            pool.transform.parent = transform;
            pool.transform.localPosition = new Vector3(0, 99999, 0);
            pool.transform.localEulerAngles = Vector3.zero;
            pool.transform.localScale = Vector3.one;
            Pools.Add(poolName, pool);
            Nodes.Add(poolName, new Dictionary<string, List<GameObject>>());
        }

        /// <summary>
        /// 是否存在对应缓冲池
        /// </summary>
        /// <param name="poolName">池名字</param>
        /// <returns></returns>
        private bool IsHavePool(string poolName)
        {
            if (string.IsNullOrEmpty(poolName))
            {
                Log.Error($"池名字不能为空");
                return false;
            }
            if (Nodes.ContainsKey(poolName) == false || Nodes[poolName] == null)
            {
                Log.Error($"没有对应的缓冲池:{poolName}");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 销毁池
        /// </summary>
        public void DestroyPool(string poolName)
        {
            if (!Pools.ContainsKey(poolName))
                return;
            GameObject.Destroy(Pools[poolName]);
            Pools.Remove(poolName);
            Nodes.Remove(poolName);
        }

        [ContextMenu("Tools/CleanAllPool")]
        public void CleanAllPool()
        {
            if (Pools.Count == 0)
                return;
            var list = new List<string>(Pools.Keys);
            for (int i = 0; i < list.Count; i++)
            {
                CleanPool(list[i]);
            }
        }

        /// <summary>
        /// 清空池
        /// </summary>
        public void CleanPool(string poolName)
        {
            if (Pools.Count == 0 || !Pools.ContainsKey(poolName) || string.IsNullOrEmpty(poolName))
                return;
            List<GameObject> objArray = new List<GameObject>();
            foreach (Transform each in Pools[poolName].transform)
            {
                objArray.Add(each.gameObject);
            }
            for (int i = 0; i < objArray.Count; i++)
            {
                GameObject.DestroyImmediate(objArray[i]);
            }
            objArray = null;
            Nodes[poolName] = null;
            Nodes[poolName] = new Dictionary<string, List<GameObject>>();
        }

        /// <summary>
        /// 缓存指定数量预制体
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="poolName">池名字</param>
        /// <param name="count">数量</param>
        public void CachePrefab(string poolName, string path, int count = 1, Action callBack = null)
        {
            if (!IsHavePool(poolName))
                return;
            int addNum = 0;
            for (int i = 0; i < count; i++)
            {
                AssetLoadManager.Instance.LoadPrefabInstance(path, (go) =>
                {
                    if (go == null)
                    {
                        Log.Error(path);
                        return;
                    }
                    RecyclePrefab(poolName, path, go);
                    addNum++;
                    if (addNum >= count)
                    {
                        callBack?.Invoke();
                    }
                });
            }
        }

        /// <summary>
        /// 回收预制体到池中
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="go">预制体</param>
        /// <param name="poolName">池名字</param>
        /// <param name="stayPos">是否保留位置（带有虚拟相机的预制体，如果主相机不是cut模式切换，需要保留位置，不然镜头会显示错误）</param>
        public void RecyclePrefab(string poolName, string path, GameObject go, int stayPos = 0)
        {
            if (string.IsNullOrEmpty(path) || go == null)
                return;
            if (!IsHavePool(poolName))
            {
                Destroy(go);
                return;
            }
            if (stayPos == 0)
            {
                go.transform.SetParent(Pools[poolName].transform, false);
                go.ResetPRS();
            }
            else
            {
                go.transform.SetParent(Pools[poolName].transform, true);
            }
          
            go.SetActive(false);

            //无同路径字典，新建一个
            if (!Nodes[poolName].ContainsKey(path))
            {
                Nodes[poolName].Add(path, new List<GameObject>());
            }
            if (!Nodes[poolName][path].Contains(go))
            {
                Nodes[poolName][path].Add(go);
            }
            else
            {
                Debug.LogError("存在重复入池的操作，请检查逻辑!");
            }
        }

        /// <summary>
        /// 从池中获得预制体
        /// </summary>
        ///  <param name="path">路径</param>
        /// <param name="poolName">池名字</param>
        /// <param name="callback">回调</param>
        public void LoadPrefab(string poolName, string path, Action<GameObject> callback)
        {
            if (string.IsNullOrEmpty(poolName))
            {
                Log.Error("不允许池名字为空!");
                return;
            }
            if (!IsHavePool(poolName))
                return;

            GameObject tmp = null;

            if (Nodes[poolName].ContainsKey(path))
            {
                if (Nodes[poolName][path].Count > 0)
                {

                    tmp = Nodes[poolName][path][0];
                    Nodes[poolName][path].Remove(tmp);

                    if (Nodes[poolName][path].Count <= 0)
                        Nodes[poolName].Remove(path);
                }
            }

            if (tmp == null)
            {
                AssetLoadManager.Instance.LoadPrefabInstance(path, (go) =>
                {
                    tmp = go;
                    if (tmp == null)
                    {
                        Log.Error(path);
                        callback?.Invoke(null);
                        return;
                    }
                    UsedDic.Add(tmp, path);
                    callback?.Invoke(tmp);
                });
            }
            else
            {
                if (!UsedDic.ContainsKey(tmp))
                {
                    UsedDic.Add(tmp, path);
                }
                tmp.gameObject.SetActive(true);
                callback?.Invoke(tmp);
            }
        }
        /// <summary>
        /// 清理池中所有相同路径预制体
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="poolName">池名字</param>
        public void DestroyAllPrefab(string poolName, string path)
        {
            if (!IsHavePool(poolName))
                return;
            if (Nodes.TryGetValue(poolName, out var _dic))
            {
                if (_dic.TryGetValue(path, out var _list))
                {
                    for (int i = 0; i < _list.Count; i++)
                    {
                        GameObject.Destroy(_list[i].gameObject);
                    }
                    _list.Clear();
                    _dic.Remove(path);
                }
            }
        }
        /// <summary>
        /// 从池中删除预制体
        /// </summary>
        /// <param name="poolName">池名字</param>
        /// <param name="path">路径</param>
        /// <param name="go">预制体</param>
        public void DestroyPrefab(string poolName, string path, GameObject go = null)
        {
            if (!IsHavePool(poolName))
                return;
            if (Nodes.TryGetValue(poolName, out var _dic))
            {
                if (_dic.TryGetValue(path, out var _list))
                {
                    if (go == null) //缓存又没加载出来的，可以通过路径去移除
                        go = _list[0];
                    _list.Remove(go);
                    GameObject.Destroy(go);
                    if (_list.Count == 0)
                    {
                        _dic.Remove(path);
                    }
                }
            }
        }
    }
}
