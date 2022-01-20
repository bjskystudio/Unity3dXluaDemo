﻿using System;
using System.Collections.Generic;
using UnityEngine;
using YoukiaCore.Utils;

namespace Framework
{
    public class ObjectPool : DataSingleton<ObjectPool>
    {
        public readonly object Locker = new object();
        /// <summary>
        /// 对象管理池
        /// </summary>
        private Dictionary<int, Stack<IRecycle>> ObjectPoolMap;
        /// <summary>
        /// IRecycle类型
        /// </summary>
        private Type mIRecycleType;

        protected override void Init()
        {
            ObjectPoolMap = new Dictionary<int, Stack<IRecycle>>();
            mIRecycleType = typeof(IRecycle);
        }

        /// <summary>
        /// 初始化指定数量的指定对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="number"></param>
        public void Initialize<T>(int number) where T : IRecycle
        {
            lock (Locker)
            {
                for (int i = 0; i < number; i++)
                {
                    var obj = Activator.CreateInstance<T>();
                    Push<T>(obj);
                }
                var hashcode = typeof(T).GetHashCode();
                //Debug.Log(string.Format("初始化类型:{0}的剩余数量:{1}", typeof(T).Name, ObjectPoolMap[hashcode].Count));
            }
        }

        /// <summary>
        /// 初始化指定数量的指定对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="number"></param>
        public void Initialize<T>(Type type, int number) where T : IRecycle
        {
            lock (Locker)
            {
                if (mIRecycleType.IsAssignableFrom(type))
                {
                    for (int i = 0; i < number; i++)
                    {
                        var obj = Activator.CreateInstance(type);
                        Push<T>((T)obj);
                    }
                    //var hashcode = typeof(T).GetHashCode();
                    //Debug.Log(string.Format("初始化类型:{0}的剩余数量:{1}", typeof(T).Name, ObjectPoolMap[hashcode].Count));
                }
                else
                {
                    Debug.LogError($"传入的类型:{type.Name}不符合类型where T:{mIRecycleType.Name}要求，初始化池对象失败!");
                }
            }
        }

        /// <summary>
        /// 指定对象进池
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void Push<T>(T obj) where T : IRecycle
        {
            lock (Locker)
            {
                obj.OnDispose();
                var hashcode = obj.GetType().GetHashCode();
                if (!ObjectPoolMap.ContainsKey(hashcode))
                {
                    ObjectPoolMap.Add(hashcode, new Stack<IRecycle>());
                }
                ObjectPoolMap[hashcode].Push(obj);
                //Debug.Log(string.Format("类型:{0}进对象池!",typeof(T).Name));
                //Debug.Log(string.Format("池里类型:{0}的剩余数量:{1}", typeof(T).Name, ObjectPoolMap[hashcode].Count));
            }
        }

        /// <summary>
        /// 指定对象作为IRecycle进池
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void PushAsObj(IRecycle obj)
        {
            lock (Locker)
            {
                obj.OnDispose();
                var hashcode = obj.GetType().GetHashCode();
                if (!ObjectPoolMap.ContainsKey(hashcode))
                {
                    ObjectPoolMap.Add(hashcode, new Stack<IRecycle>());
                }
                ObjectPoolMap[hashcode].Push(obj);
                //Debug.Log(string.Format("类型:{0}进对象池!",typeof(T).Name));
                //Debug.Log(string.Format("池里类型:{0}的剩余数量:{1}", typeof(T).Name, ObjectPoolMap[hashcode].Count));
            }
        }

        /// <summary>
        /// 弹出可用指定对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Pop<T>() where T : IRecycle
        {
            lock (Locker)
            {
                var hashcode = typeof(T).GetHashCode();
                if (ObjectPoolMap.ContainsKey(hashcode))
                {
                    var instance = ObjectPoolMap[hashcode].Pop();
                    instance.OnCreate();
                    //Debug.Log(string.Format("类型:{0}出对象池!", typeof(T).Name));
                    //Debug.Log(string.Format("池里类型:{0}的剩余数量:{1}", typeof(T).Name, ObjectPoolMap[hashcode].Count));
                    if (ObjectPoolMap[hashcode].Count == 0)
                    {
                        Clear<T>();
                    }
                    return (T)instance;
                }
                else
                {
                    //Debug.Log(string.Format("类型:{0}构建新的对象!", typeof(T).Name));
                    // 默认池里没有反射创建,尽量避免反射创建，
                    // 可以考虑调用Initialize初始化一定数量进池
                    var instance = Activator.CreateInstance<T>();
                    instance.OnCreate();
                    return instance;
                }
            }
        }

        /// <summary>
        /// 弹出可用指定类型对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IRecycle PopWithType(Type type)
        {
            lock (Locker)
            {
                if (mIRecycleType.IsAssignableFrom(type))
                {
                    var hashcode = type.GetHashCode();
                    if (ObjectPoolMap.ContainsKey(hashcode))
                    {
                        var instance = ObjectPoolMap[hashcode].Pop();
                        instance.OnCreate();
                        //Debug.Log(string.Format("类型:{0}出对象池!", typeof(T).Name));
                        //Debug.Log(string.Format("池里类型:{0}的剩余数量:{1}", typeof(T).Name, ObjectPoolMap[hashcode].Count));
                        if (ObjectPoolMap[hashcode].Count == 0)
                        {
                            ClearWithType(type);
                        }
                        return (IRecycle)instance;
                    }
                    else
                    {
                        //Debug.Log(string.Format("类型:{0}构建新的对象!", typeof(T).Name));
                        // 默认池里没有反射创建,尽量避免反射创建，
                        // 可以考虑调用Initialize初始化一定数量进池
                        var instance = (IRecycle)Activator.CreateInstance(type);
                        instance.OnCreate();
                        return instance;
                    }
                }
                else
                {
                    Debug.LogError($"传入的类型:{type.Name}不符合类型T:{mIRecycleType.Name}要求，出对象池失败!");
                    return null;
                }
            }
        }

        /// <summary>
        /// 清除指定类型的对象缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Clear<T>() where T : IRecycle
        {
            lock (Locker)
            {
                var hashcode = typeof(T).GetHashCode();
                if (ObjectPoolMap.ContainsKey(hashcode))
                {
                    //Debug.Log(string.Format("清除对象池里的类型:{0}", typeof(T).Name));
                    foreach (var obj in ObjectPoolMap[hashcode])
                    {
                        obj.OnDispose();
                    }
                    ObjectPoolMap.Remove(hashcode);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 清除指定类型的对象缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool ClearWithType(Type type)
        {
            lock (Locker)
            {
                if (mIRecycleType.IsAssignableFrom(type))
                {
                    var hashcode = type.GetHashCode();
                    if (ObjectPoolMap.ContainsKey(hashcode))
                    {
                        //Debug.Log(string.Format("清除对象池里的类型:{0}", typeof(T).Name));
                        foreach (var obj in ObjectPoolMap[hashcode])
                        {
                            obj.OnDispose();
                        }
                        ObjectPoolMap.Remove(hashcode);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    Debug.LogError($"传入的类型:{type.Name}不符合类型T:{mIRecycleType.Name}要求，清理对象池失败!");
                    return false;
                }
            }
        }

        /// <summary>
        /// 清除所有对象缓存
        /// </summary>
        public void ClearAll()
        {
            lock (Locker)
            {
                var objhascodearray = new int[ObjectPoolMap.Keys.Count];
                ObjectPoolMap.Keys.CopyTo(objhascodearray, 0);
                foreach (var objhashcode in objhascodearray)
                {
                    Clear(objhashcode);
                }
                ObjectPoolMap.Clear();
            }
        }

        /// <summary>
        /// 清除指定类型的对象缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private bool Clear(int hashcode)
        {
            lock (Locker)
            {
                if (ObjectPoolMap.ContainsKey(hashcode))
                {
                    //Debug.Log(string.Format("清除对象池里的类型:{0}", typeof(T).Name));
                    foreach (var obj in ObjectPoolMap[hashcode])
                    {
                        obj.OnDispose();
                    }
                    ObjectPoolMap.Remove(hashcode);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        protected override void ResetData()
        {
            ClearAll();
        }

        protected override void Destroy()
        {
            ClearAll();
        }
    }
}
