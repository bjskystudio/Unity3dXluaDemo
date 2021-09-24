using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{

    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T mInstance = null;

        public static T Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = GameObject.FindObjectOfType(typeof(T)) as T;
                    if (mInstance == null)
                    {
                        GameObject go = new GameObject(typeof(T).Name);
                        mInstance = go.AddComponent<T>();
                        GameObject parent = GameObject.Find("Boot");
                        if (parent == null)
                        {
                            parent = new GameObject("Boot");
                        }
                        GameObject.DontDestroyOnLoad(parent);
                        if (parent != null)
                        {
                            go.transform.parent = parent.transform;
                        }
                    }
                }

                return mInstance;
            }
        }
        public static bool IsInstance()
        {
            return mInstance != null;
        }

        /// <summary>
        /// 没有任何实现的函数，用于保证MonoSingleton在使用前已创建
        /// </summary>
        public virtual void Startup()
        {

        }

        void Awake()
        {
            if (mInstance == null)
            {
                mInstance = this as T;
                DontDestroyOnLoad(gameObject);
                Init();
            }
            else if (mInstance != this)
            {
                UnityEngine.Object.DestroyImmediate(gameObject);
            }
        }

        protected virtual void Init()
        {

        }

        /// <summary>
        /// 延迟一帧销毁
        /// <para>需要修改时机请重写DelayCo</para>
        /// </summary>
        public void DelayDestroySelf()
        {
            StartCoroutine(CoDestroySelf());
        }

        private IEnumerator CoDestroySelf()
        {
            yield return DelayCo();
            Dispose();
            MonoSingleton<T>.mInstance = null;
            UnityEngine.Object.DestroyImmediate(gameObject);
        }

        /// <summary>
        /// 销毁前延迟流程
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerator DelayCo()
        {
            yield return null;
        }

        public void DestroySelf()
        {
            Dispose();
            MonoSingleton<T>.mInstance = null;
            UnityEngine.Object.DestroyImmediate(gameObject);
        }

        public virtual void Dispose()
        {

        }
    }
}