using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UObject = UnityEngine.Object;

/// <summary>
/// Transform扩展方法
/// </summary>
[XLua.LuaCallCSharp]
public static class TransformExtends
{



    /// <summary>
    /// 获取对象在Hierarchy中的节点路径
    /// </summary>
    public static string GetHierarchyPath(this Transform tran)
    {
        if (tran == null)
        {
            return string.Empty;
        }
        return _getHierarchPathLoop(tran);
    }

    private static string _getHierarchPathLoop(Transform tran, string path = null)
    {
        if (string.IsNullOrEmpty(path))
        {
            path = tran.gameObject.name;
        }
        else
        {
            path = tran.gameObject.name + "/" + path;
        }

        if (tran.parent != null)
        {
            return _getHierarchPathLoop(tran.parent, path);
        }
        else
        {
            return path;
        }
    }

    #region Layer

    /// <summary>
    /// 递归设置Layer
    /// </summary>
    public static void SetLayer(this Transform tran, string layerName)
    {
        SetLayer(tran, LayerMask.NameToLayer(layerName));
    }

    /// <summary>
    /// 递归设置Layer
    /// </summary>
    public static void SetLayer(this Transform tran, string layerName, string ignoreLayerName)
    {
        SetLayer(tran, LayerMask.NameToLayer(layerName), LayerMask.NameToLayer(ignoreLayerName));
    }

    /// <summary>
    /// 递归设置Layer
    /// </summary>
    public static void SetLayer(this Transform tran, int layer, int ignoreLayer)
    {
        SetLayer(tran, layer, t => t.gameObject.layer != ignoreLayer);
    }

    /// <summary>
    /// 递归子节点设置Layer
    /// <param name="filter">过滤器：返回True为通过过滤</param>
    /// <param name="doSomething">遍历时附加行为</param>
    /// </summary>
    public static void SetLayer(this Transform tran, int layer, Func<Transform, bool> filter = null, Action<Transform> doSomething = null)
    {
        if (filter != null)
        {
            if (filter(tran))
            {
                tran.gameObject.layer = layer;
                doSomething?.Invoke(tran);
            }
        }
        else
        {
            tran.gameObject.layer = layer;
            doSomething?.Invoke(tran);
        }
        if (tran.childCount > 0)
        {
            int i, len = tran.childCount;
            for (i = 0; i < len; i++)
            {
                Transform sub = tran.GetChild(i);
                sub.SetLayer(layer, filter, doSomething);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tran">tran</param>
    /// <param name="layer">设置的layer</param>
    /// <param name="layerDic">字典,存设置前的老layer</param>
    public static void SetLayerAndSaveDic(this Transform tran, int layer, Dictionary<int, int> layerDic)
    {
        if (layerDic != null && !layerDic.ContainsKey(tran.GetHashCode()))
            layerDic.Add(tran.GetHashCode(), tran.gameObject.layer);

        tran.gameObject.layer = layer;

        if (tran.childCount > 0)
        {
            foreach (Transform each in tran)
            {
                SetLayerAndSaveDic(each, layer, layerDic);
            }
        }
    }

    /// <summary>
    /// 通过字典递归设置Layer
    /// </summary>
    /// <param name="layerDic">缓存Layer数据的字典</param>
    public static void SetLayerByDic(this Transform tran, Dictionary<int, int> layerDic)
    {

        int code = tran.GetHashCode();
        if (layerDic != null && layerDic.ContainsKey(code))
        {
            tran.gameObject.layer = layerDic[code];
        }


        if (tran.childCount > 0)
        {
            foreach (Transform each in tran)
            {
                SetLayerByDic(each, layerDic);
            }
        }

    }

    public static void SetRenderLayer(this Transform tran, string layerName)
    {
        int layer = LayerMask.NameToLayer(layerName);
        Renderer[] renders = tran.GetComponentsInChildren<Renderer>(true);
        foreach (var v in renders)
        {
            v.gameObject.layer = layer;
        }
    }

    #endregion

    #region Interface

    /// <summary>
    /// 获取当前Transform对象上挂载的接口
    /// </summary>
    public static T GetInterface<T>(this Transform tran) where T : class
    {
        if (!typeof(T).IsInterface)
        {
            return null;
        }
        //return inObj.GetComponents<Component>().OfType<T>().FirstOrDefault();
        Component[] cp = tran.gameObject.GetComponents<Component>();
        int i, length = cp.Length;
        for (i = 0; i < length; i++)
        {
            if (cp[i] is T)
            {
                T t = cp[i] as T;
                return t;
            }
        }
        return null;
    }

    /// <summary>
    /// 获取当前Transform对象上挂载的接口集合
    /// </summary>
    public static List<T> GetInterfacesList<T>(this Transform tran) where T : class
    {
        if (!typeof(T).IsInterface)
        {
            return null;
        }
        Component[] cp = tran.gameObject.GetComponents<Component>();
        if (cp != null && cp.Length > 0)
        {
            int i, len = cp.Length;
            List<T> list = new List<T>();
            for (i = 0; i < len; i++)
            {
                if (cp[i] is T)
                {
                    T a = cp[i] as T;
                    list.Add(a);
                }
            }
            if (list != null && list.Count > 0)
            {
                return list;
            }
        }
        return null;
    }
    /// <summary>
    /// 获取当前Transform对象上挂载的接口集合
    /// </summary>
    public static T[] GetInterfaces<T>(this Transform tran) where T : class
    {
        List<T> list = tran.GetInterfacesList<T>();
        if (list != null)
        {
            return list.ToArray();
        }
        return null;
    }

    /// <summary>
    /// 获取当前Transform对象或者子节点上挂载的接口集合
    /// 
    ///默认API扩展，GetComponentsInChildren 
    /// 
    /// </summary>
    public static T GetInterfaceInChlidren<T>(this Transform tran) where T : class
    {
        if (!typeof(T).IsInterface)
        {
            return null;
        }

        Component[] cp = tran.gameObject.GetComponentsInChildren<Component>();
        int i, length = cp.Length;
        for (i = 0; i < length; i++)
        {
            if (cp[i] is T)
            {
                T t = cp[i] as T;
                return t;
            }
        }
        return null;
    }

    /// <summary>
    /// 获取当前Transform父集上挂载的接口
    /// 
    /// 默认API扩展，GetComponentInParent
    /// 
    /// </summary>
    public static T GetInterfaceInParent<T>(this Transform tran) where T : class
    {
        if (!typeof(T).IsInterface)
        {
            return null;
        }

        Component[] cp = tran.gameObject.GetComponentsInParent<Component>();
        if (cp != null && cp.Length > 0)
        {
            int i, len = cp.Length;
            for (i = 0; i < len; i++)
            {
                if (cp[i] is T)
                {
                    T t = cp[i] as T;
                    return t;
                }
            }
        }
        return null;
    }

    /// <summary>
    /// 获取当前Transform子集上挂载的接口
    /// 包含隐藏或者未激活的节点
    /// </summary>
    public static T FindInterfaceInChildren<T>(this Transform tran, bool incudeSelf = false) where T : class
    {
        if (!typeof(T).IsInterface)
        {
            return null;
        }

        List<Component> cp = tran.FindComponentListInChildren<Component>(incudeSelf);
        if (cp != null && cp.Count > 0)
        {
            int i, len = cp.Count;
            for (i = 0; i < len; i++)
            {
                if (cp[i] is T)
                {
                    T t = cp[i] as T;
                    return t;
                }
            }
        }
        return null;
    }
    /// <summary>
    /// 获取当前Transform父集上挂载的接口
    /// 包含隐藏或者未激活的节点
    /// </summary
    public static T FindInterfaceInParent<T>(this Transform tran, bool incudeSelf = false) where T : class
    {
        if (!typeof(T).IsInterface)
        {
            return null;
        }

        List<Component> cp = tran.FindComponentListInParent<Component>();
        if (cp != null && cp.Count > 0)
        {
            int i, len = cp.Count;
            for (i = 0; i < len; i++)
            {
                if (cp[i] is T)
                {
                    T t = cp[i] as T;
                    return t;
                }
            }
        }
        return null;
    }

    /// <summary>
    /// 获取当前Transform对象上挂载的接口集合
    /// 
    ///默认API扩展，GetComponentsInChildren
    /// 
    /// </summary>
    public static List<T> GetInterfaceListInChlidren<T>(this Transform tran) where T : class
    {
        if (!typeof(T).IsInterface)
        {
            return null;
        }

        Component[] cp = tran.gameObject.GetComponentsInChildren<Component>();

        if (cp != null && cp.Length > 0)
        {
            int i, len = cp.Length;
            List<T> list = new List<T>();
            for (i = 0; i < len; i++)
            {
                if (cp[i] is T)
                {
                    T t = cp[i] as T;
                    list.Add(t);
                }
            }
            if (list.Count > 0)
            {
                return list;
            }
        }

        return null;
    }

    /// <summary>
    /// 获取当前Transform对象上挂载的接口集合
    /// 
    ///默认API扩展，GetComponentsInChildren
    /// 
    /// </summary>
    public static T[] GetInterfacesInChlidren<T>(this Transform tran) where T : class
    {
        List<T> list = tran.GetInterfaceListInChlidren<T>();
        if (list != null)
        {
            return list.ToArray();
        }
        return null;
    }

    /// <summary>
    /// 获取当前Transform对象上挂载的接口集合
    /// 
    /// 默认API扩展，GetComponentsInParent
    /// 
    /// </summary>
    public static List<T> GetInterfaceListInParent<T>(this Transform tran) where T : class
    {
        if (!typeof(T).IsInterface)
        {
            return null;
        }

        Component[] cp = tran.gameObject.GetComponentsInParent<Component>();
        if (cp != null && cp.Length > 0)
        {
            int i, len = cp.Length;
            List<T> list = new List<T>();
            for (i = 0; i < len; i++)
            {
                if (cp[i] is T)
                {
                    T t = cp[i] as T;
                    list.Add(t);
                }
            }
            if (list.Count > 0)
            {
                return list;
            }
        }

        return null;
    }

    /// <summary>
    /// 获取当前Transform对象上挂载的接口集合
    /// 
    /// 默认API扩展，GetComponentsInParent
    /// 
    /// </summary>
    public static T[] GetInterfacesInParent<T>(this Transform tran) where T : class
    {
        List<T> list = tran.GetInterfaceListInParent<T>();
        if (list != null)
        {
            return list.ToArray();
        }
        return null;
    }

    /// <summary>
    /// 返回当前节点以下的Interface,包含隐藏或者未激活的节点
    /// <param name="incudeSelf">是否包含自身节点</param>
    /// <param name="filter">过滤器：返回True为通过过滤</param>
    /// <param name="doSomething">遍历时附加行为</param>
    /// </summary>
    public static List<T> FindInterfaceListInChildren<T>(this Transform tran, bool incudeSelf = false, Func<T, bool> filter = null, Action<T> doSomething = null) where T : class
    {
        if (!typeof(T).IsInterface)
        {
            return null;
        }

        List<Component> cp = tran.FindComponentListInChildren<Component>(incudeSelf);
        if (cp != null && cp.Count > 0)
        {
            List<T> list = new List<T>();
            int i, len = cp.Count;
            for (i = 0; i < len; i++)
            {
                if (cp[i] is T)
                {
                    T t = cp[i] as T;
                    if (filter != null)
                    {
                        if (filter(t))
                        {
                            list.Add(t);
                            if (doSomething != null) doSomething(t);
                        }
                    }
                    else
                    {
                        list.Add(t);
                        if (doSomething != null) doSomething(t);
                    }
                }
            }
            if (list.Count > 0)
            {
                return list;
            }
        }

        return null;
    }

    /// <summary>
    /// 返回当前节点以下的Interface,包含隐藏或者未激活的节点
    /// <param name="incudeSelf">是否包含自身节点</param>
    /// <param name="filter">过滤器：返回True为通过过滤</param>
    /// <param name="doSomething">遍历时附加行为</param>
    /// </summary>
    public static T[] FindInterfacesInChildren<T>(this Transform tran, bool incudeSelf = false, Func<T, bool> filter = null, Action<T> doSomething = null) where T : class
    {
        List<T> list = tran.FindInterfaceListInChildren<T>(incudeSelf, filter, doSomething);
        if (list != null)
        {
            return list.ToArray();
        }
        return null;
    }

    /// <summary>
    /// 返回当前节点以下的Interface,包含隐藏或者未激活的节点
    /// <param name="incudeSelf">是否包含自身节点</param>
    /// <param name="filter">过滤器：返回True为通过过滤</param>
    /// <param name="doSomething">遍历时附加行为</param>
    /// </summary>
    public static List<T> FindInterfaceListInParent<T>(this Transform tran, bool incudeSelf = false, Func<T, bool> filter = null, Action<T> doSomething = null) where T : class
    {
        if (!typeof(T).IsInterface)
        {
            return null;
        }

        List<Component> cp = tran.FindComponentListInParent<Component>(incudeSelf);
        if (cp != null && cp.Count > 0)
        {
            List<T> list = new List<T>();
            int i, len = cp.Count;
            for (i = 0; i < len; i++)
            {
                if (cp[i] is T)
                {
                    T t = cp[i] as T;
                    if (filter != null)
                    {
                        if (filter(t))
                        {
                            list.Add(t);
                            if (doSomething != null) doSomething(t);
                        }
                    }
                    else
                    {
                        list.Add(t);
                        if (doSomething != null) doSomething(t);
                    }
                }
            }
            if (list.Count > 0)
            {
                return list;
            }
        }

        return null;
    }

    /// <summary>
    /// 返回当前节点以下的Interface,包含隐藏或者未激活的节点
    /// <param name="incudeSelf">是否包含自身节点</param>
    /// <param name="filter">过滤器：返回True为通过过滤</param>
    /// <param name="doSomething">遍历时附加行为</param>
    /// </summary>
    public static T[] FindInterfacesInParent<T>(this Transform tran, bool incudeSelf = false, Func<T, bool> filter = null, Action<T> doSomething = null) where T : class
    {
        List<T> list = FindInterfaceListInParent(tran, incudeSelf, filter, doSomething);
        if (list != null)
        {
            return list.ToArray();
        }
        return null;
    }

    /// <summary>
    /// 返回Root节点以下的所有Interface,包含隐藏或者未激活的节点
    /// <param name="incudeRoot">是否包含Root节点</param>
    /// <param name="filter">过滤器：返回True为通过过滤</param>
    /// <param name="doSomething">遍历时附加行为</param>
    /// </summary>
    public static List<T> FindAllInterfaceList<T>(this Transform tran, Func<T, bool> filter = null, Action<T> doSomething = null) where T : class
    {

        if (!typeof(T).IsInterface)
        {
            return null;
        }

        List<Component> cp = tran.FindAllComponentList<Component>();
        if (cp != null && cp.Count > 0)
        {
            List<T> list = new List<T>();
            int i, len = cp.Count;
            for (i = 0; i < len; i++)
            {
                if (cp[i] is T)
                {
                    T t = cp[i] as T;
                    if (filter != null)
                    {
                        if (filter(t))
                        {
                            list.Add(t);
                            if (doSomething != null) doSomething(t);
                        }
                    }
                    else
                    {
                        list.Add(t);
                        if (doSomething != null) doSomething(t);
                    }
                }
            }
            if (list.Count > 0)
            {
                return list;
            }
        }

        return null;
    }

    /// <summary>
    /// 返回Root节点以下的所有Interface,包含隐藏或者未激活的节点
    /// <param name="incudeRoot">是否包含Root节点</param>
    /// <param name="filter">过滤器：返回True为通过过滤</param>
    /// <param name="doSomething">遍历时附加行为</param>
    /// </summary>
    public static T[] FindAllInterfaces<T>(this Transform tran, Func<T, bool> filter = null, Action<T> doSomething = null) where T : class
    {
        List<T> list = tran.FindAllInterfaceList<T>(filter, doSomething);
        if (list != null)
        {
            return list.ToArray();
        }
        return null;
    }

    #endregion

    #region Component

    /// <summary>
    /// 返回Root节点以下的所有Component,包含隐藏或者未激活的节点
    /// <typeparam name="T">Component</typeparam>
    /// <param name="incudeRoot">是否包含Root节点</param>
    /// <param name="filter">过滤器：返回True为通过过滤</param>
    /// <param name="doSomething">遍历时附加行为</param>
    /// </summary>
    public static List<T> FindAllComponentList<T>(this Transform tran, Func<T, bool> filter = null, Action<T> doSomething = null) where T : Component
    {
        return tran.root.FindComponentListInChildren(true, filter, doSomething);
    }

    /// <summary>
    /// 返回Root节点以下的所有Component,包含隐藏或者未激活的节点
    /// <typeparam name="T">Component</typeparam>
    /// <param name="incudeRoot">是否包含Root节点</param>
    /// <param name="filter">过滤器：返回True为通过过滤</param>
    /// <param name="doSomething">遍历时附加行为</param>
    /// </summary>
    public static T[] FindAllComponents<T>(this Transform tran, Func<T, bool> filter = null, Action<T> doSomething = null) where T : Component
    {
        List<T> list = FindAllComponentList<T>(tran, filter, doSomething);
        if (list != null)
        {
            return list.ToArray();
        }
        return null;
    }

    /// <summary>
    /// 按照节点顺序返回所有子节点的Component,包含隐藏或者未激活的节点;
    /// </summary>
    /// <typeparam name="T">Component</typeparam>
    /// <param name="incudeSelf">是否包含自身节点</param>
    /// <param name="filter">过滤器：返回True为通过过滤</param>
    /// <param name="doSomething">遍历时附加行为</param>
    /// <returns></returns>
    public static List<T> FindComponentListInChildren<T>(this Transform tran, bool incudeSelf = false, Func<T, bool> filter = null, Action<T> doSomething = null) where T : Component
    {
        List<T> list = new List<T>();
        if (incudeSelf)
        {
            T cpt = tran.GetComponent<T>();
            if (cpt != null)
            {
                list.Add(cpt);
            }
        }
        _findComponentInChildrenLoop<T>(tran, ref list, filter, doSomething);
        if (list.Count > 0)
        {
            return list;
        }
        return null;
    }

    /// <summary>
    /// 按照节点顺序返回所有子节点的Component,包含隐藏或者未激活的节点;
    /// </summary>
    /// <typeparam name="T">Component</typeparam>
    /// <param name="incudeSelf">是否包含自身节点</param>
    /// <param name="filter">过滤器：返回True为通过过滤</param>
    /// <param name="doSomething">遍历时附加行为</param>
    /// <returns></returns>
    public static T[] FindComponentsInChildren<T>(this Transform tran, bool incudeSelf = false, Func<T, bool> filter = null, Action<T> doSomething = null) where T : Component
    {
        List<T> list = tran.FindComponentListInChildren<T>(incudeSelf, filter, doSomething);
        if (list != null)
        {
            return list.ToArray();
        }
        return null;
    }

    /// <summary>
    /// 按照节点顺序返回所有上级节点的Component,包含隐藏或者未激活的节点;
    /// </summary>
    /// <typeparam name="T">Component</typeparam>
    /// <param name="incudeSelf">是否包含自身节点</param>
    /// <param name="filter">过滤器：返回True为通过过滤</param>
    /// <param name="doSomething">遍历时附加行为</param>
    /// <returns></returns>
    public static List<T> FindComponentListInParent<T>(this Transform tran, bool incudeSelf = false, Func<T, bool> filter = null, Action<T> doSomething = null) where T : Component
    {
        List<T> list = new List<T>();
        if (incudeSelf)
        {
            T cpt = tran.GetComponent<T>();
            if (cpt != null)
            {
                list.Add(cpt);
            }
        }
        _findComponentInParentLoop<T>(tran, ref list, filter, doSomething);
        if (list.Count > 0)
        {
            return list;
        }
        return null;
    }

    /// <summary>
    /// 按照节点顺序返回所有上级节点的Component,包含隐藏或者未激活的节点;
    /// </summary>
    /// <typeparam name="T">Component</typeparam>
    /// <param name="incudeSelf">是否包含自身节点</param>
    /// <param name="filter">过滤器：返回True为通过过滤</param>
    /// <param name="doSomething">遍历时附加行为</param>
    /// <returns></returns>
    public static T[] FindComponentsInParent<T>(this Transform tran, bool incudeSelf = false, Func<T, bool> filter = null, Action<T> doSomething = null) where T : Component
    {
        List<T> list = tran.FindComponentListInParent<T>(incudeSelf, filter, doSomething);
        if (list != null)
        {
            return list.ToArray();
        }
        return null;
    }

    private static void _findComponentInChildrenLoop<T>(Transform t, ref List<T> list, Func<T, bool> filter = null, Action<T> doSomething = null) where T : Component
    {
        int i, len = t.childCount;
        for (i = 0; i < len; i++)
        {
            Transform ct = t.GetChild(i);
            T[] cpts = ct.GetComponents<T>();
            if (cpts != null && cpts.Length > 0)
            {
                int c, cLen = cpts.Length;
                for (c = 0; c < cLen; c++)
                {
                    T cpt = cpts[c];
                    if (cpt)
                    {
                        if (filter != null)
                        {
                            if (filter(cpt)) list.Add(cpt);
                            if (doSomething != null) doSomething(cpt);
                        }
                        else
                        {
                            list.Add(cpt);
                            if (doSomething != null) doSomething(cpt);
                        }
                    }
                }
            }
            if (ct.childCount > 0)
            {
                _findComponentInChildrenLoop<T>(ct, ref list, filter, doSomething);
            }
        }
    }

    private static void _findComponentInParentLoop<T>(Transform t, ref List<T> list, Func<T, bool> filter = null, Action<T> doSomething = null) where T : Component
    {
        if (t.parent)
        {
            Transform ct = t.parent;
            T[] cpts = ct.GetComponents<T>();
            if (cpts != null && cpts.Length > 0)
            {
                int c, cLen = cpts.Length;
                for (c = 0; c < cLen; c++)
                {
                    T cpt = cpts[c];
                    if (cpt)
                    {
                        if (filter != null)
                        {
                            if (filter(cpt)) list.Add(cpt);
                            if (doSomething != null) doSomething(cpt);
                        }
                        else
                        {
                            list.Add(cpt);
                            if (doSomething != null) doSomething(cpt);
                        }
                    }
                }
            }
            _findComponentInParentLoop<T>(t.parent, ref list, filter, doSomething);
        }
    }

    /// <summary>
    /// 查找并返回Component(当前对象找不到,则查找子对象)
    /// </summary>
    public static T FindComponent<T>(this Transform obj) where T : Component
    {
        T ins = obj.GetComponent<T>();
        if (ins == null)
        {
            ins = obj.GetComponentInChildren<T>();
        }
        return ins;
    }

    /// <summary>
    /// 查找并返回Component(当前对象找不到,则查找子和父级对象)
    /// </summary>
    public static T FindComponentIncParent<T>(this Transform obj) where T : Component
    {
        T ins = obj.GetComponent<T>();
        if (ins == null)
        {
            ins = obj.GetComponentInChildren<T>();
            if (ins == null)
            {
                ins = obj.GetComponentInParent<T>();
            }
        }
        return ins;
    }

    /// <summary>
    /// 查找或者创建Component(当前Component在当前对象和子集对象都找不到,则在当前对象上创建Component)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static T FindOrCreateComponent<T>(this Transform obj) where T : Component
    {
        T ins = obj.GetComponent<T>();
        if (ins == null)
        {
            ins = obj.GetComponentInChildren<T>();
            if (ins == null)
            {
                ins = obj.gameObject.AddComponent<T>();
            }
        }
        return ins;
    }

    /// <summary>
    /// 查找或者创建Component(当前Component在当前对象和子集/父级对象都找不到,则在当前对象上创建Component)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static T FindOrCreateComponentIncParent<T>(this Transform obj) where T : Component
    {
        T ins = obj.GetComponent<T>();
        if (ins == null)
        {
            ins = obj.GetComponentInChildren<T>();
            if (ins == null)
            {
                ins = obj.GetComponentInParent<T>();
                if (ins == null)
                {
                    ins = obj.gameObject.AddComponent<T>();
                }
            }
        }
        return ins;
    }

    /// <summary>
    /// 按照节点顺序返回所有子节点的Component<T>;
    /// （不包含自身）
    /// </summary>
    public static List<T> FindAllComponentsInChildrenInOrder<T>(this Transform tran) where T : Component
    {
        List<T> list = new List<T>();

        FACICIOLoop<T>(tran, ref list);

        if (list.Count > 0)
        {
            return list;
        }
        else
        {
            return null;
        }

    }

    /// <summary>
    /// 从节点的Root开始按照节点顺序返回所有子节点的Component<T>;
    /// （包含自身）
    /// </summary>
    public static List<T> FindAllComponentsInOrder<T>(this Transform tran) where T : Component
    {
        List<T> list = new List<T>();

        T cpt = tran.root.GetComponent<T>();
        if (cpt != null)
        {
            list.Add(cpt);
        }

        FACICIOLoop<T>(tran.root, ref list);

        if (list.Count > 0)
        {

            return list;
        }
        else
        {
            return null;
        }
    }


    private static void FACICIOLoop<T>(Transform t, ref List<T> list) where T : Component
    {
        int i, len = t.childCount;
        for (i = 0; i < len; i++)
        {
            Transform ct = t.GetChild(i);
            T cpt = ct.GetComponent<T>();
            if (cpt != null)
            {
                list.Add(cpt);
            }
            if (ct.childCount > 0)
            {
                FACICIOLoop<T>(ct, ref list);
            }
        }
    }

    #endregion


}