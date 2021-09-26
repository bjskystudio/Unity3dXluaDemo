using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using YoukiaCore.Log;
using UObject = UnityEngine.Object;

/// <summary>
/// 说明：xlua中的扩展方法
/// </summary>
[LuaCallCSharp]
public static class UnityObjectExtends
{
//    #region BlackList
//    /// <summary>
//    /// blackList的目的是为了防止程序员自己又调用了内置接口，比如来新人或者有些人写着写着忘记了 因为这些函数已经封装了正确的调用函数 所以安全起见全部不容许导出xlua 
//    /// </summary>
//    [BlackList]
//    public static List<List<string>> blackList = new List<List<string>>() {
//#region blackList

//        #region Object
//        //属性
//        new List<string>(){ "UnityEngine.Object", "name"},

//        //方法
//        new List<string>(){ "UnityEngine.Object", "Destroy","UnityEngine.Object"},
//        new List<string>(){ "UnityEngine.Object", "DestroyImmediate","UnityEngine.Object"},
//        new List<string>(){ "UnityEngine.Object", "DestroyImmediate","UnityEngine.Object","System.Boolean"},
//        new List<string>(){ "UnityEngine.Object", "DestroyObject","UnityEngine.Object"},

//        new List<string>(){ "UnityEngine.Object", "Instantiate", "UnityEngine.Transform","System.Boolean"},
//        new List<string>(){ "UnityEngine.Object", "Instantiate", "UnityEngine.Transform"},
//	    #endregion

//        #region Componet
//        // Componet
//        //属性
//        new List<string>(){ "UnityEngine.Componet", "transform"},
//        new List<string>(){ "UnityEngine.Componet", "gameObject"},

//        //方法
//        new List<string>(){ "UnityEngine.Component", "GetComponent","System.Type"},
//        new List<string>(){ "UnityEngine.Component", "GetComponent","System.String"},
//	    #endregion

//        #region Transform
//        // Transform
//        //属性
//        new List<string>(){ "UnityEngine.Transform", "childCount"},
//        new List<string>(){ "UnityEngine.Transform", "eulerAngles"},
//        new List<string>(){ "UnityEngine.Transform", "localEulerAngles"},
//        new List<string>(){ "UnityEngine.Transform", "localPosition"},
//        new List<string>(){ "UnityEngine.Transform", "localRotation"},
//        new List<string>(){ "UnityEngine.Transform", "localScale"},
//        new List<string>(){ "UnityEngine.Transform", "parent"},
//        new List<string>(){ "UnityEngine.Transform", "position"},
//        new List<string>(){ "UnityEngine.Transform", "root"},
//        new List<string>(){ "UnityEngine.Transform", "rotation"},

//        //方法
//        new List<string>(){ "UnityEngine.Transform", "DetachChildren"},
//        new List<string>(){ "UnityEngine.Transform", "Find", "System.String"},
//        new List<string>(){ "UnityEngine.Transform", "FindChild","System.String"},
//        new List<string>(){ "UnityEngine.Transform", "GetChild","System.Int32"},
//        new List<string>(){ "UnityEngine.Transform", "GetChildCount"},
//        new List<string>(){ "UnityEngine.Transform", "GetSiblingIndex"},
//        new List<string>(){ "UnityEngine.Transform", "SetAsFirstSibling"},
//        new List<string>(){ "UnityEngine.Transform", "SetAsLastSibling"},
//        new List<string>(){ "UnityEngine.Transform", "SetParent", "UnityEngine.Transform"},
//        new List<string>(){ "UnityEngine.Transform", "SetParent", "UnityEngine.Transform","System.Boolean" },
//        new List<string>(){ "UnityEngine.Transform", "SetSiblingIndex","System.Int32"},
//	    #endregion

//        #region GameObject
//        // GameObject
//        //属性
//        new List<string>(){ "UnityEngine.GameObject", "transform"},
//        new List<string>(){ "UnityEngine.GameObject", "gameObject"},
//        new List<string>(){ "UnityEngine.GameObject", "activeSelf"},
//        new List<string>(){ "UnityEngine.GameObject", "activeInHierarchy"},

//        //方法
//        new List<string>(){ "UnityEngine.GameObject", "SetActive", "System.Boolean"},
//        new List<string>(){ "UnityEngine.GameObject", "AddComponent"},
//        new List<string>(){ "UnityEngine.GameObject", "AddComponent", "System.Type"},
//        new List<string>(){ "UnityEngine.GameObject", "AddComponent", "System.String"},
//        new List<string>(){ "UnityEngine.GameObject", "GetComponent", "System.Type"},
//        new List<string>(){ "UnityEngine.GameObject", "GetComponent", "System.String"},

//        // RectTransform
//        //属性
//        new List<string>(){ "UnityEngine.RectTransform", "anchoredPosition"},
//        new List<string>(){ "UnityEngine.RectTransform", "anchoredPosition3D"},
//        new List<string>(){ "UnityEngine.RectTransform", "anchorMax"},
//        new List<string>(){ "UnityEngine.RectTransform", "anchorMin"},
//        new List<string>(){ "UnityEngine.RectTransform", "offsetMax"},
//        new List<string>(){ "UnityEngine.RectTransform", "offsetMin"},
//        new List<string>(){ "UnityEngine.RectTransform", "pivot"},
//        new List<string>(){ "UnityEngine.RectTransform", "rect"},
//        new List<string>(){ "UnityEngine.RectTransform", "sizeDelta"},
//	    #endregion

//        #region Misc
//	    //Text
//        new List<string>(){ "UnityEngine.UI.Text", "font"},
//        // Camera
//        new List<string>(){ "UnityEngine.Camera", "WorldToScreenPoint", "UnityEngine.Vector3"},
//	    #endregion
//#endregion
//    };

//    #endregion

    // 说明：lua侧判Object为空全部使用这个函数
    public static bool IsNull(this UObject target)
    {
        return target == null;
    }

    #region GetGo/GetTrans UObject转换GameObject/Transform

    public static GameObject GetGo(this UObject target)
    {
        if (target == null)
            throw new Exception("不能传递null");
        var com = target as Component;
        if (com != null)
            return com.gameObject;
        var go = target as GameObject;
        if (go != null)
            return go;
        throw new Exception("无法转化对象" + target.GetType().FullName);
    }

    public static Transform GetTrans(this UObject target)
    {
        if (target == null)
            throw new Exception("不能传递null");
        var com = target as Component;
        if (com != null)
            return com.transform;
        var go = target as GameObject;
        if (go != null)
            return go.transform;
        throw new Exception("无法转化对象" + target.GetType().FullName);
    }

    #endregion

    #region GetName/SetName UObjectName

    public static string GetName(this UObject target)
    {
        if (null == target)
        {
            return string.Empty;
        }
        return target.name;
    }

    public static void SetName(this UObject target, string name)
    {
        if (null == target)
        {
            return;
        }
        target.name = name;
    }

    #endregion

    #region GetOrAddComponent添加获取、DestroyComponent移除
    public static T GetOrAddComponent<T>(this GameObject obj) where T : Component
    {
        if (obj == null)
        {
            return null;
        }
        T t = obj.GetComponent<T>();
        if (t == null)
        {
            t = obj.AddComponent<T>();
        }
        return t;
    }

    public static T GetOrAddComponent<T>(this Component target) where T : Component
    {
        return GetOrAddComponent<T>(target?.gameObject);
    }

    public static Component GetOrAddComponent(this GameObject target, System.Type t)
    {
        if (target == null || t == null)
        {
            return null;
        }
        var tt = target.GetComponent(t);
        if (tt == null)
        {
            tt = target.AddComponent(t);
        }
        return tt;
    }

    public static Component GetOrAddComponent(this Component target, System.Type t)
    {
        return GetOrAddComponent(target?.gameObject, t);
    }

    /// <summary>
    /// 删除指定的组件
    /// </summary>
    /// <param name="target"></param>
    public static void DestroyComponent(this Component target)
    {
        if (target == null)
        {
            return;
        }
        GameObject.Destroy(target);
    }

    #endregion

    #region Instantiate 实例化

    public static GameObject InstantiateSelf(this GameObject target, Transform parent)
    {
        if (target == null)
        {
            return null;
        }
        if (parent != null)
        {
            return UObject.Instantiate(target, Vector3.zero, Quaternion.identity, parent);
        }
        return UObject.Instantiate(target, Vector3.zero, Quaternion.identity);
    }

    public static GameObject InstantiateSelf(this GameObject target, Component parent)
    {
        return InstantiateSelf(target, parent?.transform);
    }

    public static GameObject InstantiateSelf(this GameObject target, GameObject parent)
    {
        return InstantiateSelf(target, parent?.transform);
    }

    public static GameObject InstantiateSelf(this Component target, Component parent)
    {
        return InstantiateSelf(target?.gameObject, parent?.transform);
    }

    public static GameObject InstantiateSelf(this Component target, GameObject parent)
    {
        return InstantiateSelf(target?.gameObject, parent?.transform);
    }

    #endregion

    #region DestroyObject/DestroyGameObjDelay/ClearChildren 销毁

    public static void DestroyGameObj(this GameObject target)
    {
        if (target == null)
        {
            return;
        }
        GameObject.Destroy(target);
    }

    public static void DestroyGameObj(this Component target)
    {
        DestroyGameObj(target?.gameObject);
    }

    public static void DestroyGameObjDelay(this GameObject target, float time)
    {
        if (target == null)
        {
            return;
        }
        GameObject.Destroy(target, time);
    }

    public static void DestroyGameObjDelay(this Component target, float time)
    {
        DestroyGameObjDelay(target?.gameObject, time);
    }

    public static void ClearChildren(this Transform target, int index = 0)
    {
        if (target == null)
        {
            return;
        }
        int len = target.childCount;
        for (int i = len - 1; i >= index; i--)
        {
            Transform child = target.GetChild(i);
            GameObject.Destroy(child.gameObject);
        }
    }

    public static void ClearChildren(this Component target, int index = 0)
    {
        ClearChildren(target?.transform, index);
    }

    public static void ClearChildren(this GameObject target, int index = 0)
    {
        ClearChildren(target?.transform, index);
    }

    #endregion

    #region SetActive 显隐 参数：value(0 false隐藏，1 true显示)

    public static void SetActive(this GameObject target, int value)
    {
        if (target == null)
        {
            return;
        }
        bool flag = value != 0;
        if (target.activeSelf == !flag)
        {
            target.SetActive(flag);
        }
    }

    public static void SetActive(this Component target, int value)
    {
        SetActive(target?.gameObject, value);
    }

    #endregion

    #region GetActive 返回 数字 0、1

    public static int GetActive(this GameObject target)
    {
        if (target == null)
        {
            return 0;
        }
        return target.activeSelf == true ? 1 : 0;
    }

    public static int GetActive(this Component target)
    {
        return GetActive(target?.gameObject);
    }

    #endregion

    #region GetActiveInHierarchy 返回 数字 0、1

    public static int GetActiveInHierarchy(this GameObject target)
    {
        if (target == null)
        {
            return 0;
        }
        return target.activeInHierarchy == true ? 1 : 0;
    }

    public static int GetActiveInHierarchy(this Component target)
    {
        return GetActiveInHierarchy(target?.gameObject);
    }

    #endregion

    #region Transform、位置、缩放、旋转

    #region ResetPRS 重置本地位置：坐标、旋转、缩放

    public static void ResetPRS(this Transform target)
    {
        if (target == null)
        {
            return;
        }
        target.localPosition = Vector3.zero;
        target.localEulerAngles = Vector3.zero;
        target.localScale = Vector3.one;
    }

    public static void ResetPRS(this Component target)
    {
        ResetPRS(target?.transform);
    }
    public static void ResetPRS(this GameObject target)
    {
        ResetPRS(target?.transform);
    }

    #endregion

    #region GetLocalPosition 获取本地坐标  参数 out x,out y,out z

    public static void GetLocalPosition(this Transform target, out float x, out float y, out float z)
    {
        if (target == null)
        {
            x = 0;
            y = 0;
            z = 0;
            return;
        }
        Vector3 _vec3 = target.localPosition;
        x = _vec3.x;
        y = _vec3.y;
        z = _vec3.z;
    }

    public static void GetLocalPosition(this GameObject target, out float x, out float y, out float z)
    {
        GetLocalPosition(target?.transform, out x, out y, out z);
    }

    public static void GetLocalPosition(this Component target, out float x, out float y, out float z)
    {
        GetLocalPosition(target?.transform, out x, out y, out z);
    }

    #endregion

    #region SetLocalPosition 设置本地坐标  参数 x,y,z

    public static void SetLocalPosition(this Transform target, float? x, float? y, float? z)
    {
        if (target == null)
        {
            return;
        }
        target.localPosition = new Vector3(x ?? target.localPosition.x, y ?? target.localPosition.y, z ?? target.localPosition.z);
    }

    public static void SetLocalPosition(this GameObject target, float? x, float? y, float? z)
    {
        SetLocalPosition(target?.transform, x, y, z);
    }

    public static void SetLocalPosition(this Component target, float? x, float? y, float? z)
    {
        SetLocalPosition(target?.transform, x, y, z);
    }

    #endregion

    #region GetPosition 获取世界坐标  参数 out x,out y,out z

    public static void GetPosition(this Transform target, out float x, out float y, out float z)
    {
        if (target == null)
        {
            x = 0;
            y = 0;
            z = 0;
            return;
        }
        Vector3 _vec3 = target.position;
        x = _vec3.x;
        y = _vec3.y;
        z = _vec3.z;
    }

    public static void GetPosition(this Component target, out float x, out float y, out float z)
    {
        GetPosition(target?.transform, out x, out y, out z);
    }

    public static void GetPosition(this GameObject target, out float x, out float y, out float z)
    {
        GetPosition(target?.transform, out x, out y, out z);
    }

    #endregion

    #region SetPosition 设置世界坐标  参数 x,y,z

    public static void SetPosition(this Transform target, float? x, float? y, float? z)
    {
        if (target == null)
        {
            return;
        }
        target.position = new Vector3(x ?? target.position.x, y ?? target.position.y, z ?? target.position.z);
    }

    public static void SetPosition(this Component target, float? x, float? y, float? z)
    {
        SetPosition(target?.transform, x, y, z);
    }

    public static void SetPosition(this GameObject target, float? x, float? y, float? z)
    {
        SetPosition(target?.transform, x, y, z);
    }

    #endregion

    #region SetLocalPositionToZero 本地坐标归零

    public static void SetLocalPositionToZero(this Transform target)
    {
        if (target == null)
        {
            return;
        }
        target.localPosition = Vector3.zero;
    }

    public static void SetLocalPositionToZero(this GameObject target)
    {
        SetLocalPositionToZero(target?.transform);
    }

    public static void SetLocalPositionToZero(this Component target)
    {
        SetLocalPositionToZero(target?.transform);
    }

    #endregion

    #region AddPosition 坐标增量运算 参数：x,y,z,isWorld(默认0本地，1世界)

    public static void AddPosition(this Transform target, float x, float y, float z, int isWorld = 0)
    {
        if (target == null)
        {
            return;
        }
        if (isWorld == 1)
            target.position = target.position + new Vector3(x, y, z);
        else
            target.localPosition = target.localPosition + new Vector3(x, y, z);
    }

    public static void AddPosition(this GameObject target, float x, float y, float z, int isWorld = 0)
    {
        AddPosition(target?.transform, x, y, z, isWorld);
    }

    public static void AddPosition(this Component target, float x, float y, float z, int isWorld = 0)
    {
        AddPosition(target?.transform, x, y, z, isWorld);
    }

    #endregion

    #region SetPositionByREFTarget 根据参考目标设置世界或者本地坐标，可设置偏移 参数：refTarget参考目标,x,y,z,isWorld(默认0本地，1世界)

    public static void SetPositionByREFTarget(this Transform target, Transform refTarget, float offsetX = 0, float offsetY = 0, float offsetZ = 0, int isWorld = 0)
    {
        if (target == null)
        {
            return;
        }
        if (isWorld == 0)
        {
            var pos = refTarget.localPosition;
            pos.x += offsetX;
            pos.y += offsetY;
            pos.z += offsetZ;
            target.localPosition = pos;
        }
        else
        {
            var pos = refTarget.position;
            pos.x += offsetX;
            pos.y += offsetY;
            pos.z += offsetZ;
            target.position = pos;
        }
    }

    public static void SetPositionByREFTarget(this Transform target, Component refPoint, float offsetX = 0, float offsetY = 0, float offsetZ = 0, int isWorld = 0)
    {
        SetPositionByREFTarget(target, refPoint.transform, offsetX, offsetY, offsetZ, isWorld);
    }

    public static void SetPositionByREFTarget(this Transform target, GameObject refPoint, float offsetX = 0, float offsetY = 0, float offsetZ = 0, int isWorld = 0)
    {
        SetPositionByREFTarget(target, refPoint.transform, offsetX, offsetY, offsetZ, isWorld);
    }

    public static void SetPositionByREFTarget(this Component target, Component refPoint, float offsetX = 0, float offsetY = 0, float offsetZ = 0, int isWorld = 0)
    {
        SetPositionByREFTarget(target.transform, refPoint.transform, offsetX, offsetY, offsetZ, isWorld);
    }

    public static void SetPositionByREFTarget(this Component target, GameObject refPoint, float offsetX = 0, float offsetY = 0, float offsetZ = 0, int isWorld = 0)
    {
        SetPositionByREFTarget(target.transform, refPoint.transform, offsetX, offsetY, offsetZ, isWorld);
    }

    public static void SetPositionByREFTarget(this Component target, Transform refPoint, float offsetX = 0, float offsetY = 0, float offsetZ = 0, int isWorld = 0)
    {
        SetPositionByREFTarget(target.transform, refPoint, offsetX, offsetY, offsetZ, isWorld);
    }

    public static void SetPositionByREFTarget(this GameObject target, Component refPoint, float offsetX = 0, float offsetY = 0, float offsetZ = 0, int isWorld = 0)
    {
        SetPositionByREFTarget(target.transform, refPoint.transform, offsetX, offsetY, offsetZ, isWorld);
    }

    public static void SetPositionByREFTarget(this GameObject target, GameObject refPoint, float offsetX = 0, float offsetY = 0, float offsetZ = 0, int isWorld = 0)
    {
        SetPositionByREFTarget(target.transform, refPoint.transform, offsetX, offsetY, offsetZ, isWorld);
    }

    public static void SetPositionByREFTarget(this GameObject target, Transform refPoint, float offsetX = 0, float offsetY = 0, float offsetZ = 0, int isWorld = 0)
    {
        SetPositionByREFTarget(target.transform, refPoint, offsetX, offsetY, offsetZ, isWorld);
    }

    #endregion

    #region GetEulerAngles 获得欧拉角 参数out x,out y,out z

    public static void GetEulerAngles(this Transform target, out float x, out float y, out float z)
    {
        if (target == null)
        {
            x = 0;
            y = 0;
            z = 0;
            return;
        }
        Vector3 _vec = target.eulerAngles;
        x = _vec.x;
        y = _vec.y;
        z = _vec.z;
    }

    public static void GetEulerAngles(this GameObject target, out float x, out float y, out float z)
    {
        GetEulerAngles(target?.transform, out x, out y, out z);
    }

    public static void GetEulerAngles(this Component target, out float x, out float y, out float z)
    {
        GetEulerAngles(target?.transform, out x, out y, out z);
    }

    #endregion

    #region SetEulerAngles 设置欧拉角 参数x,y,z

    public static void SetEulerAngles(this Transform target, float? x, float? y, float? z)
    {
        if (target == null)
        {
            return;
        }
        target.eulerAngles = new Vector3(x ?? target.eulerAngles.x, y ?? target.eulerAngles.y, z ?? target.eulerAngles.z);
    }

    public static void SetEulerAngles(this GameObject target, float? x, float? y, float? z)
    {
        SetEulerAngles(target?.transform, x, y, z);
    }

    public static void SetEulerAngles(this Component target, float? x, float? y, float? z)
    {
        SetEulerAngles(target?.transform, x, y, z);
    }

    #endregion

    #region GetLocalEulerAngles 获得本地欧拉角 参数out x,out y,out z

    public static void GetLocalEulerAngles(this Transform target, out float x, out float y, out float z)
    {
        if (target == null)
        {
            x = 0;
            y = 0;
            z = 0;
            return;
        }
        Vector3 _vec = target.localEulerAngles;
        x = _vec.x;
        y = _vec.y;
        z = _vec.z;
    }

    public static void GetLocalEulerAngles(this GameObject target, out float x, out float y, out float z)
    {
        GetLocalEulerAngles(target?.transform, out x, out y, out z);
    }

    public static void GetLocalEulerAngles(this Component target, out float x, out float y, out float z)
    {
        GetLocalEulerAngles(target?.transform, out x, out y, out z);
    }

    #endregion

    #region SetLocalEulerAngles 设置本地欧拉角 参数x,y,z

    public static void SetLocalEulerAngles(this Transform target, float? x, float? y, float? z)
    {
        if (target == null)
        {
            return;
        }
        target.localEulerAngles = new Vector3(x ?? target.localEulerAngles.x, y ?? target.localEulerAngles.y, z ?? target.localEulerAngles.z);
    }

    public static void SetLocalEulerAngles(this GameObject target, float? x, float? y, float? z)
    {
        SetLocalEulerAngles(target?.transform, x, y, z);
    }

    public static void SetLocalEulerAngles(this Component target, float? x, float? y, float? z)
    {
        SetLocalEulerAngles(target?.transform, x, y, z);
    }

    #endregion

    #region GetRotation 获得旋转 参数out x,out y,out z,out w

    public static void GetRotation(this Transform target, out float x, out float y, out float z, out float w)
    {
        if (target == null)
        {
            x = 0;
            y = 0;
            z = 0;
            w = 0;
            return;
        }
        Quaternion _quaternion = target.rotation;
        x = _quaternion.x;
        y = _quaternion.y;
        z = _quaternion.z;
        w = _quaternion.w;
    }

    public static void GetRotation(this GameObject target, out float x, out float y, out float z, out float w)
    {
        GetRotation(target?.transform, out x, out y, out z, out w);
    }

    public static void GetRotation(this Component target, out float x, out float y, out float z, out float w)
    {
        GetRotation(target?.transform, out x, out y, out z, out w);
    }

    #endregion

    #region SetRotation 设置旋转 参数x,y,z,w

    public static void SetRotation(this Transform target, float? x, float? y, float? z, float? w)
    {
        if (target == null)
        {
            return;
        }
        target.rotation = new Quaternion(x ?? target.rotation.x, y ?? target.rotation.y, z ?? target.rotation.z, w ?? target.rotation.w);
    }

    public static void SetRotation(this GameObject target, float? x, float? y, float? z, float? w)
    {
        SetRotation(target?.transform, x, y, z, w);
    }

    public static void SetRotation(this Component target, float? x, float? y, float? z, float? w)
    {
        SetRotation(target?.transform, x, y, z, w);
    }

    #endregion

    #region GetLocalRotation 获得本地旋转 参数out x,out y,out z,out w

    public static void GetLocalRotation(this Transform target, out float x, out float y, out float z, out float w)
    {
        if (target == null)
        {
            x = 0;
            y = 0;
            z = 0;
            w = 0;
            return;
        }
        Quaternion _quaternion = target.localRotation;
        x = _quaternion.x;
        y = _quaternion.y;
        z = _quaternion.z;
        w = _quaternion.w;
    }

    public static void GetLocalRotation(this GameObject target, out float x, out float y, out float z, out float w)
    {
        GetLocalRotation(target?.transform, out x, out y, out z, out w);
    }

    public static void GetLocalRotation(this Component target, out float x, out float y, out float z, out float w)
    {
        GetLocalRotation(target?.transform, out x, out y, out z, out w);
    }

    #endregion

    #region SetLocalRotation 设置本地旋转 参数x,y,z,w

    public static void SetLocalRotation(this Transform target, float? x, float? y, float? z, float? w)
    {
        if (target == null)
        {
            return;
        }
        target.localRotation = new Quaternion(x ?? target.localRotation.x, y ?? target.localRotation.y, z ?? target.localRotation.z, w ?? target.localRotation.w);
    }

    public static void SetLocalRotation(this GameObject target, float? x, float? y, float? z, float? w)
    {
        SetLocalRotation(target?.transform, x, y, z, w);
    }

    public static void SetLocalRotation(this Component target, float? x, float? y, float? z, float? w)
    {
        SetLocalRotation(target?.transform, x, y, z, w);
    }

    #endregion

    #region SetRotationToIdentity 设置默认旋转,与世界轴完全对齐

    public static void SetRotationToIdentity(this Transform target)
    {
        if (target == null)
        {
            return;
        }
        target.rotation = Quaternion.identity;
    }

    public static void SetRotationToIdentity(this GameObject target)
    {
        SetRotationToIdentity(target?.transform);
    }

    public static void SetRotationToIdentity(this Component target)
    {
        SetRotationToIdentity(target?.transform);
    }

    #endregion

    #region SetRotationToIdentity 设置默认旋转,与父轴完全对齐

    public static void SetLocalRotationToIdentity(this Transform target)
    {
        if (target == null)
        {
            return;
        }
        target.localRotation = Quaternion.identity;
    }

    public static void SetLocalRotationToIdentity(this GameObject target)
    {
        SetLocalRotationToIdentity(target?.transform);
    }

    public static void SetLocalRotationToIdentity(this Component target)
    {
        SetLocalRotationToIdentity(target?.transform);
    }

    #endregion

    #region GetLocalScale 获得本地缩放 参数out x,out y,out z

    public static void GetLocalScale(this Transform target, out float x, out float y, out float z)
    {
        if (target == null)
        {
            x = 0;
            y = 0;
            z = 0;
            return;
        }
        Vector3 _vec = target.localScale;
        x = _vec.x;
        y = _vec.y;
        z = _vec.z;
    }

    public static void GetLocalScale(this GameObject target, out float x, out float y, out float z)
    {
        GetLocalScale(target?.transform, out x, out y, out z);
    }

    public static void GetLocalScale(this Component target, out float x, out float y, out float z)
    {
        GetLocalScale(target?.transform, out x, out y, out z);
    }

    #endregion

    #region SetLocalScale 设置本地缩放 参数x,y,z

    public static void SetLocalScale(this Transform target, float? x = null, float? y = null, float? z = null)
    {
        if (target == null)
        {
            return;
        }
        target.localScale = new Vector3(x ?? target.localScale.x, y ?? target.localScale.y, z ?? target.localScale.z);
    }

    public static void SetLocalScale(this GameObject target, float? x = null, float? y = null, float? z = null)
    {
        SetLocalScale(target?.transform, x, y, z);
    }

    public static void SetLocalScale(this Component target, float? x = null, float? y = null, float? z = null)
    {
        SetLocalScale(target?.transform, x, y, z);
    }

    #endregion

    #region SetLocalScaleXYZ 设置本地缩放XYZ 参数s

    public static void SetLocalScaleXYZ(this Transform target, float s)
    {
        if (target == null)
        {
            return;
        }
        target.localScale = Vector3.one * s;
    }

    public static void SetLocalScaleXYZ(this Component target, float s)
    {
        SetLocalScaleXYZ(target?.transform, s);
    }

    public static void SetLocalScaleXYZ(this GameObject target, float s)
    {
        SetLocalScaleXYZ(target?.transform, s);
    }

    #endregion

    #region SetLocalScaleToOne 设置本地缩放到1

    public static void SetLocalScaleToOne(this Transform target)
    {
        if (target == null)
        {
            return;
        }
        target.localScale = Vector3.one;
    }

    public static void SetLocalScaleToOne(this GameObject target)
    {
        SetLocalScaleToOne(target?.transform);
    }

    public static void SetLocalScaleToOne(this Component target)
    {
        SetLocalScaleToOne(target?.transform);
    }

    #endregion

    #region SyncTrans 同步transform 世界坐标、世界旋转、本地缩放  参数 另外一个unity对象作为参考

    public static void SyncTrans(this Transform target, Transform by)
    {
        if (target == null || by == null)
        {
            return;
        }
        target.position = by.position;
        target.rotation = by.rotation;
        target.localScale = by.localScale;
    }

    public static void SyncTrans(this Transform target, GameObject by)
    {
        SyncTrans(target, by?.transform);
    }

    public static void SyncTrans(this Transform target, Component by)
    {
        SyncTrans(target, by?.transform);
    }

    public static void SyncTrans(this GameObject target, GameObject by)
    {
        SyncTrans(target.transform, by?.transform);
    }

    public static void SyncTrans(this GameObject target, Transform by)
    {
        SyncTrans(target?.transform, by);
    }

    public static void SyncTrans(this GameObject target, Component by)
    {
        SyncTrans(target?.transform, by?.transform);
    }

    public static void SyncTrans(this Component target, Component by)
    {
        SyncTrans(target?.transform, by?.transform);
    }

    public static void SyncTrans(this Component target, Transform by)
    {
        SyncTrans(target?.transform, by);
    }

    public static void SyncTrans(this Component target, GameObject by)
    {
        SyncTrans(target?.transform, by?.transform);
    }

    #endregion

    #region 设置本地坐标的世界偏移
    /// <summary>
    /// 设置本地坐标的世界偏移 
    /// </summary>
    public static void SetLocalOffsetByWorld(this Transform target, float x, float y, float z)
    {
        Vector3 postion = target.parent.InverseTransformPoint(x, y, z);
        target.localPosition = postion;
    }
    public static void SetLocalOffsetByWorld(this GameObject target, float x = 0, float y = 0, float z = 0)
    {
        SetLocalOffsetByWorld(target?.transform, x, y, z);
    }
    public static void SetLocalOffsetByWorld(this Component target, float x = 0, float y = 0, float z = 0)
    {
        SetLocalOffsetByWorld(target?.transform, x, y, z);
    }
    #endregion
    #endregion

    #region 场景

    #region SetForward 设置物体朝向

    public static void SetForward(this Transform target, float x, float y, float z)
    {
        if (target == null)
        {
            return;
        }
        target.forward = new Vector3(x, y, z);
    }

    public static void SetForward(this GameObject target, float x, float y, float z)
    {
        SetForward(target?.transform, x, y, z);
    }

    public static void SetForward(this Component target, float x, float y, float z)
    {
        SetForward(target?.transform, x, y, z);
    }

    #endregion

    #endregion

    #region 2D、RectTransform

    #region SetAnchorPosition 设置Rect的AnchorPosition 参数：x,y

    public static void SetAnchorPosition(this RectTransform target, float? x, float? y)
    {
        if (target == null)
        {
            return;
        }
        target.anchoredPosition = new Vector2(x ?? target.anchoredPosition.x, y ?? target.anchoredPosition.y);
    }

    public static void SetAnchorPosition(this Transform target, float? x, float? y)
    {
        if (target == null)
        {
            return;
        }
        SetAnchorPosition(target as RectTransform, x, y);
    }

    public static void SetAnchorPosition(this Component target, float? x, float? y)
    {
        SetAnchorPosition(target?.transform, x, y);
    }

    public static void SetAnchorPosition(this GameObject target, float? x, float? y)
    {
        SetAnchorPosition(target?.transform, x, y);
    }

    #endregion

    #region SetRectTransformZero 设置RectTransform归零

    public static void SetRectTransformZero(this RectTransform target)
    {
        if (target == null)
        {
            return;
        }
        target.offsetMin = Vector2.zero;
        target.offsetMax = Vector2.zero;
    }

    public static void SetRectTransformZero(this Transform target)
    {
        if (target == null)
        {
            return;
        }
        SetRectTransformZero(target as RectTransform);
    }

    public static void SetRectTransformZero(this Component target)
    {
        SetRectTransformZero(target?.transform);
    }

    public static void SetRectTransformZero(this GameObject target)
    {
        SetRectTransformZero(target?.transform);
    }

    #endregion

    #region SetRectTransform 设置SetRectTransform 参数：minX,minY,maxX,maxY

    public static void SetRectTransform(this RectTransform target, float minX, float minY, float maxX, float maxY)
    {
        if (target == null)
        {
            return;
        }
        target.offsetMin = new Vector2(minX, minY);
        target.offsetMax = new Vector2(maxX, maxY);
    }

    public static void SetRectTransform(this Transform target, float minX, float minY, float maxX, float maxY)
    {
        if (target == null)
        {
            return;
        }
        SetRectTransform(target as RectTransform, minX, minY, maxX, maxY);
    }

    public static void SetRectTransform(this Component target, float minX, float minY, float maxX, float maxY)
    {
        SetRectTransform(target?.transform, minX, minY, maxX, maxY);
    }

    public static void SetRectTransform(this GameObject target, float minX, float minY, float maxX, float maxY)
    {
        SetRectTransform(target?.transform, minX, minY, maxX, maxY);
    }

    #endregion

    #region SetSizeDelta 设置SizeDelta 参数：width,height

    public static void SetSizeDelta(this RectTransform target, float width, float height)
    {
        if (target == null)
        {
            return;
        }
        target.sizeDelta = new Vector2(width, height);
    }

    public static void SetSizeDelta(this Transform target, float width, float height)
    {
        if (target == null)
        {
            return;
        }
        SetSizeDelta(target as RectTransform, width, height);
    }

    public static void SetSizeDelta(this Component target, float width, float height)
    {
        SetSizeDelta(target?.transform, width, height);
    }

    public static void SetSizeDelta(this GameObject target, float width, float height)
    {
        SetSizeDelta(target?.transform, width, height);
    }

    #endregion

    #region SetSizeDeltaByREFTarget 设置SizeDelta跟随参考目标

    public static void SetSizeDeltaByREFTarget(RectTransform target, RectTransform refTarget)
    {
        if (target == null || refTarget == null)
        {
            return;
        }
        target.sizeDelta = refTarget.sizeDelta;
    }

    public static void SetSizeDeltaByREFTarget(RectTransform target, Transform refTarget)
    {
        if (target == null || refTarget == null)
        {
            return;
        }
        SetSizeDeltaByREFTarget(target, refTarget as RectTransform);
    }

    public static void SetSizeDeltaByREFTarget(RectTransform target, Component refTarget)
    {
        if (target == null || refTarget == null)
        {
            return;
        }
        SetSizeDeltaByREFTarget(target, refTarget.transform);
    }

    public static void SetSizeDeltaByREFTarget(RectTransform target, GameObject refTarget)
    {
        if (target == null || refTarget == null)
        {
            return;
        }
        SetSizeDeltaByREFTarget(target, refTarget.transform);
    }

    public static void SetSizeDeltaByREFTarget(Transform target, RectTransform refTarget)
    {
        if (target == null || refTarget == null)
        {
            return;
        }
        SetSizeDeltaByREFTarget(target as RectTransform, refTarget);
    }

    public static void SetSizeDeltaByREFTarget(Transform target, Transform refTarget)
    {
        if (target == null || refTarget == null)
        {
            return;
        }
        SetSizeDeltaByREFTarget(target as RectTransform, refTarget as RectTransform);
    }

    public static void SetSizeDeltaByREFTarget(Transform target, Component refTarget)
    {
        if (target == null || refTarget == null)
        {
            return;
        }
        SetSizeDeltaByREFTarget(target as RectTransform, refTarget.transform);
    }

    public static void SetSizeDeltaByREFTarget(Transform target, GameObject refTarget)
    {
        if (target == null || refTarget == null)
        {
            return;
        }
        SetSizeDeltaByREFTarget(target as RectTransform, refTarget.transform);
    }

    public static void SetSizeDeltaByREFTarget(Component target, RectTransform refTarget)
    {
        SetSizeDeltaByREFTarget(target?.transform, refTarget);
    }

    public static void SetSizeDeltaByREFTarget(Component target, Transform refTarget)
    {
        SetSizeDeltaByREFTarget(target?.transform, refTarget);
    }

    public static void SetSizeDeltaByREFTarget(Component target, Component refTarget)
    {
        SetSizeDeltaByREFTarget(target?.transform, refTarget?.transform);
    }

    public static void SetSizeDeltaByREFTarget(Component target, GameObject refTarget)
    {
        SetSizeDeltaByREFTarget(target?.transform, refTarget?.transform);
    }

    public static void SetSizeDeltaByREFTarget(GameObject target, RectTransform refTarget)
    {
        SetSizeDeltaByREFTarget(target?.transform, refTarget);
    }

    public static void SetSizeDeltaByREFTarget(GameObject target, Transform refTarget)
    {
        SetSizeDeltaByREFTarget(target?.transform, refTarget);
    }

    public static void SetSizeDeltaByREFTarget(GameObject target, Component refTarget)
    {
        SetSizeDeltaByREFTarget(target?.transform, refTarget?.transform);
    }

    public static void SetSizeDeltaByREFTarget(GameObject target, GameObject refTarget)
    {
        SetSizeDeltaByREFTarget(target?.transform, refTarget?.transform);
    }

    #endregion

    #region UIObjectFollow3DObject UI对象跟随3D场景对象 参数：refTarget,uiOffsetX,uiOffsetY

    public static void UIObjectFollow3DObject(this RectTransform target, Transform refTarget, float uiOffsetX = 0, float uiOffsetY = 0)
    {
        if (target == null || refTarget == null)
        {
            return;
        }
        var parentrect = target.parent.GetComponent<RectTransform>();
        Vector3 screenpos3 = Camera.main.WorldToScreenPoint(refTarget.position);
        Vector2 screenpos = new Vector2(screenpos3.x, screenpos3.y);
        Vector2 localPos = Vector2.zero;
        bool flag = RectTransformUtility.ScreenPointToLocalPointInRectangle(parentrect, screenpos, UIModel.Inst.UICamera, out localPos);
        if (flag)
        {
            target.anchoredPosition = localPos + new Vector2(uiOffsetX, uiOffsetY);
        }
        else
        {
            target.anchoredPosition = new Vector2(10000, 100001);
        }
    }

    public static void UIObjectFollow3DObject(this Transform target, Transform refTarget, float uiOffsetX = 0, float uiOffsetY = 0)
    {
        if (target == null || refTarget == null)
        {
            return;
        }
        UIObjectFollow3DObject(target as RectTransform, refTarget, uiOffsetX, uiOffsetY);
    }

    public static void UIObjectFollow3DObject(this Transform target, Component refTarget, float uiOffsetX = 0, float uiOffsetY = 0)
    {
        if (target == null || refTarget == null)
        {
            return;
        }
        UIObjectFollow3DObject(target as RectTransform, refTarget.transform, uiOffsetX, uiOffsetY);
    }

    public static void UIObjectFollow3DObject(this Transform target, GameObject refTarget, float uiOffsetX = 0, float uiOffsetY = 0)
    {
        if (target == null || refTarget == null)
        {
            return;
        }
        UIObjectFollow3DObject(target as RectTransform, refTarget.transform, uiOffsetX, uiOffsetY);
    }

    public static void UIObjectFollow3DObject(this Component target, Transform refTarget, float uiOffsetX = 0, float uiOffsetY = 0)
    {
        UIObjectFollow3DObject(target?.transform, refTarget, uiOffsetX, uiOffsetY);
    }

    public static void UIObjectFollow3DObject(this Component target, Component refTarget, float uiOffsetX = 0, float uiOffsetY = 0)
    {
        UIObjectFollow3DObject(target?.transform, refTarget?.transform, uiOffsetX, uiOffsetY);
    }

    public static void UIObjectFollow3DObject(this Component target, GameObject refTarget, float uiOffsetX = 0, float uiOffsetY = 0)
    {
        UIObjectFollow3DObject(target?.transform, refTarget?.transform, uiOffsetX, uiOffsetY);
    }

    public static void UIObjectFollow3DObject(this GameObject target, Transform refTarget, float uiOffsetX = 0, float uiOffsetY = 0)
    {
        UIObjectFollow3DObject(target?.transform, refTarget, uiOffsetX, uiOffsetY);
    }

    public static void UIObjectFollow3DObject(this GameObject target, Component refTarget, float uiOffsetX = 0, float uiOffsetY = 0)
    {
        UIObjectFollow3DObject(target?.transform, refTarget?.transform, uiOffsetX, uiOffsetY);
    }

    public static void UIObjectFollow3DObject(this GameObject target, GameObject refTarget, float uiOffsetX = 0, float uiOffsetY = 0)
    {
        UIObjectFollow3DObject(target?.transform, refTarget?.transform, uiOffsetX, uiOffsetY);
    }

    #endregion

    #region SetChildrenActiveNumber 实例化指定数量的第一个子节点 参数：showCount

    public static void SetChildrenActiveNumber(this Transform target, int showCount)
    {
        if (target == null)
        {
            return;
        }
        if (showCount < 0)
        {
            Debug.LogError("显示数量不能小于0");
            return;
        }
        int childCount = target.childCount;
        if (0 == childCount)
        {
            return;
        }

        Transform tranChild = target.GetChild(0);
        GameObject instanceObj = tranChild.gameObject, tempGo;
        string objName = instanceObj.name;
        Vector3 localScale = tranChild.localScale;

        var maxCount = Mathf.Max(showCount, childCount);
        for (int i = 0; i < maxCount; i++)
        {
            if (i < childCount)
            {
                target.GetChild(i).gameObject.SetActive(i < showCount);
            }
            else
            {
                tempGo = GameObject.Instantiate(instanceObj, target, false);
                tempGo.name = objName;
                tempGo.transform.localScale = localScale;
                tempGo.SetActive(true);
            }
        }
    }

    public static void SetChildrenActiveNumber(this Component target, int showCount)
    {
        SetChildrenActiveNumber(target?.transform, showCount);
    }

    public static void SetChildrenActiveNumber(this GameObject target, int showCount)
    {
        SetChildrenActiveNumber(target?.transform, showCount);
    }

    #endregion

    #region GetChildCount 获取子节点数量

    public static int GetChildCount(this Component target)
    {
        if (target == null)
        {
            return 0;
        }
        return target.transform.childCount;
    }
    public static int GetChildCount(this GameObject target)
    {
        if (target == null)
        {
            return 0;
        }
        return target.transform.childCount;
    }

    #endregion

    #region GetChild 获取子节点 参数：index  返回：Transform

    public static Transform GetChild(this Component target, int index)
    {
        if (target == null)
        {
            return null;
        }
        return target.transform.GetChild(index);
    }

    public static Transform GetChild(this GameObject target, int index)
    {
        if (target == null)
        {
            return null;
        }
        return target.transform.GetChild(index);
    }

    #endregion

    #region SetParent 设置父节点 参数：parent,worldPositionStays

    public static void SetParent(this Transform target, Transform parent, int worldPositionStays = 0)
    {
        if (target == null)
        {
            return;
        }
        if (null == parent)
        {
            Log.Error(target.name + " parent is nil");
        }
        target.SetParent(parent, worldPositionStays != 0);
    }

    public static void SetParent(this Transform target, Component parent, int worldPositionStays = 0)
    {
        SetParent(target, parent?.transform, worldPositionStays);
    }

    public static void SetParent(this Transform target, GameObject parent, int worldPositionStays = 0)
    {
        SetParent(target, parent?.transform, worldPositionStays);
    }

    public static void SetParent(this Component target, GameObject parent, int worldPositionStays = 0)
    {
        SetParent(target?.transform, parent?.transform, worldPositionStays);
    }

    public static void SetParent(this Component target, Component parent, int worldPositionStays = 0)
    {
        SetParent(target?.transform, parent?.transform, worldPositionStays);
    }

    public static void SetParent(this GameObject target, GameObject parent, int worldPositionStays = 0)
    {
        SetParent(target?.transform, parent?.transform, worldPositionStays);
    }

    public static void SetParent(this GameObject target, Component parent, int worldPositionStays = 0)
    {
        SetParent(target?.transform, parent?.transform, worldPositionStays);
    }

    #endregion

    #endregion

    #region Animator

    #region SetAnimatorTrigger 设置动画Trigger 参数：key

    public static void SetAnimatorTrigger(this GameObject target, string key)
    {
        if (target == null)
        {
            return;
        }
        Animator animator = target.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger(key);
        }
    }

    public static void SetAnimatorTrigger(Component target, string key)
    {
        SetAnimatorTrigger(target?.gameObject, key);
    }

    #endregion

    #endregion

    #region Spine控制器
    //临时屏蔽错误by xin.liu
    //    #region SetSpineAlpha 设置Spine的Alpha值 参数：alpha

    //    public static void SetSpineAlpha(this GameObject target, float alpha)
    //    {
    //            if (target == null)
    //            {
    //                return;
    //            }
    //            SpineCtrl ctrl = target.GetComponent<SpineCtrl>();
    //            if (ctrl == null)
    //            {
    //                Debug.LogError("没有找到SpineCtrl:" + target.name);
    //                return;
    //            }
    //            ctrl.SetAlpha(alpha);
    //    }

    //    public static void SetSpineAlpha(this Component target, float alpha)
    //    {
    //            SetSpineAlpha(target?.gameObject, alpha);
    //}
    //    #endregion

    //    #region SetSpineAlphaWithTime 设置Spine的Alpha值 参数：alpha,time
    //    public static void SetSpineAlphaWithTime(this GameObject target, float alpha, float time)
    //    {
    //            if (target == null)
    //            {
    //                return;
    //            }
    //            SpineCtrl ctrl = target.GetComponent<SpineCtrl>();
    //            if (ctrl == null)
    //            {
    //                Debug.LogError("没有找到SpineCtrl:" + target.name);
    //                return;
    //            }
    //            ctrl.SetAlphaWithTime(alpha, time);
    //    }
    //    public static void SetSpineAlphaWithTime(this Component target, float alpha, float time)
    //    {
    //            if (target == null)
    //            {
    //                return;
    //            }
    //            SetSpineAlphaWithTime(target.gameObject, alpha, time);
    //    }

    //#endregion

#endregion

    #region UI、Aor实现

    #region SetGray 置灰 参数：value(0:false,1:true)

    public static void SetGray(this GameObject target, int value)
{
    if (value != 0)
    {
        Gray g = target.GetComponent<Gray>();
        if (!g)
            target.AddComponent<Gray>();
    }
    else
    {
        Gray g = target.GetComponent<Gray>();
        if (g)
            GameObject.Destroy(g);
    }
}

public static void SetGray(this Component target, int value)
{
    SetGray(target?.gameObject, value);
}

#endregion

#region SetCanvasGroupAlpha 设置CanvasGroupAlpha 参数：value

public static void SetCanvasGroupAlpha(this GameObject obj, float value)
{
    if (obj == null)
    {
        return;
    }
    CanvasGroup canvasGroup = obj.GetOrAddComponent<CanvasGroup>();
    canvasGroup.alpha = Mathf.Clamp(value, 0, 1);
}

public static void SetCanvasGroupAlpha(this Component obj, float value)
{
    SetCanvasGroupAlpha(obj?.gameObject, value);
}

#endregion

#region SetCanvasGroupRaycast 设置CanvasGroupRaycast 参数：value
public static void SetCanvasGroupRaycast(this GameObject obj, int value)
{
    if (obj == null)
    {
        return;
    }
    CanvasGroup canvasGroup = obj.GetOrAddComponent<CanvasGroup>();
    bool flag = value != 0;
    canvasGroup.blocksRaycasts = flag;
}

public static void SetCanvasGroupRaycast(this Component obj, int value)
{
    SetCanvasGroupRaycast(obj?.gameObject, value);
}

#endregion

#endregion

#region DOTween

#region PlayCurvePath  贝塞尔曲线移动 参数：点位:{endx,endy,endz}，中间点偏移，采样点数量，时间，回调，空气墙, easeIndex缓动类型

/// <summary>
/// dotween path 贝塞尔曲线移动
/// </summary>
/// <param name="target">目标</param>
/// <param name="aabb">空气墙:minx,miny,minz,maxx,maxy,maxz</param>
/// <param name="points">点位:{endx,endy,endz}</param>
/// <param name="offsetY">中间点偏移</param>
/// <param name="segmentNum">采样点数量</param>
/// <param name="duration">时间</param>
/// <param name="endCall">回调</param>
public static Tweener PlayCurvePath(this Transform target, float[] points, float offsetY, int segmentNum, float duration, Action endCall, float[] aabb = null, int easeIndex = 0)
{
    if (target == null || points == null || points.Length < 3)
    {
        return null;
    }
    Vector3 start = target.position;
    Vector3 end = new Vector3(points[0], points[1], points[2]);
    Vector3 center = (start + end) * 0.5f;
    center.y += offsetY;
    Vector3[] vecPoints = GetBeizerList(start, center, end, segmentNum);
    if (aabb != null)
    {
        Vector3 aabbMin = new Vector3(aabb[0], aabb[1], aabb[2]);
        Vector3 aabbMax = new Vector3(aabb[3], aabb[4], aabb[5]);
        for (int i = 0; i < vecPoints.Length; i++)
        {
            VectorClamp(ref vecPoints[i], ref aabbMin, ref aabbMax);
        }
    }
    var tweener = target.DOPath(vecPoints, duration, PathType.CatmullRom);
    if (endCall != null)
    {
        tweener.onComplete = (() =>
        {
            endCall();
        });
    }
    if (easeIndex > 0)
    {
        tweener.SetEase((Ease)easeIndex);
    }
    return tweener;
}

public static Tweener PlayCurvePath(this Component target, float[] points, float offsetY, int segmentNum, float time, Action endCall, float[] aabb = null, int easeIndex = 0)
{
    return PlayCurvePath(target?.transform, points, offsetY, segmentNum, time, endCall, aabb, easeIndex);
}

public static Tweener PlayCurvePath(this GameObject target, float[] points, float offsetY, int segmentNum, float time, Action endCall, float[] aabb = null, int easeIndex = 0)
{
    return PlayCurvePath(target?.transform, points, offsetY, segmentNum, time, endCall, aabb, easeIndex);
}

/// <summary>
/// 获取存储贝塞尔曲线点的数组二次贝塞尔
/// </summary>
/// <param name="startPoint"></param>起始点
/// <param name="controlPoint"></param>控制点
/// <param name="endPoint"></param>目标点
/// <param name="segmentNum"></param>采样点的数量
/// <returns></returns>存储贝塞尔曲线点的数组
private static Vector3[] GetBeizerList(Vector3 startPoint, Vector3 controlPoint, Vector3 endPoint, int segmentNum)
{
    Vector3[] path = new Vector3[segmentNum + 1];
    path[0] = startPoint;
    for (int i = 1; i <= segmentNum; i++)
    {
        float t = i / (float)segmentNum;
        Vector3 pixel = CalculateCubicBezierPoint(t, startPoint,
            controlPoint, endPoint);
        path[i] = pixel;
    }
    return path;
}

/// <summary>
/// 根据T值，计算贝塞尔曲线上面相对应的点
/// </summary>
/// <param name="t"></param>T值
/// <param name="p0"></param>起始点
/// <param name="p1"></param>控制点
/// <param name="p2"></param>目标点
/// <returns></returns>根据T值计算出来的贝赛尔曲线点
private static Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
{
    float u = 1 - t;
    float tt = t * t;
    float uu = u * u;
    Vector3 p = uu * p0;
    p += 2 * u * t * p1;
    p += tt * p2;
    return p;
}

/// <summary>
/// 
/// </summary>
/// <param name="value"></param>
/// <param name="min"></param>
/// <param name="max"></param>
private static void VectorClamp(ref Vector3 value, ref Vector3 min, ref Vector3 max)
{
    value.x = Mathf.Clamp(value.x, min.x, max.x);
    value.y = Mathf.Clamp(value.y, min.y, max.y);
    value.z = Mathf.Clamp(value.z, min.z, max.z);
}
#endregion

#region DoPath 执行DoPath移动 参数：points移动点位(3个成组，3的倍数)，时间，回调, easeIndex缓动类型

public static Tweener DoPath(this Component target, float[] points, float duration, Action endCall = null, int easeIndex = 0)
{
    return DoPath(target?.gameObject, points, duration, endCall, easeIndex);
}

public static Tweener DoPath(this GameObject target, float[] points, float duration, Action endCall = null, int easeIndex = 0)
{
    if (target == null || points == null || points.Length < 3 || (points.Length >= 3 && points.Length % 3 != 0))
    {
        return null;
    }
    int count = points.Length / 3;
    Vector3[] vecPoints = new Vector3[count];
    int index = 0;
    for (int i = 0; i < count; i++)
    {
        index = i * 3;
        vecPoints[i] = new Vector3(points[index], points[index + 1], points[index + 2]);
    }
    var tweener = target.transform.DOPath(vecPoints, duration, PathType.CatmullRom);
    if (endCall != null)
    {
        tweener.onComplete = (() =>
        {
            endCall();
        });
    }
    if (easeIndex > 0)
    {
        tweener.SetEase((Ease)easeIndex);
    }
    return tweener;
}

#endregion

#region DoMove 执行DoTween移动 参数：X,Y,Z,时间，回调，只取整数值（默认为false）, easeIndex缓动类型

public static Tweener DoMove(this Component target, float targetX, float targetY, float targetZ, float duration = 0, Action endCall = null, bool snapping = false, int easeIndex = 0)
{
    return DoMove(target?.gameObject, targetX, targetY, targetZ, duration, endCall, snapping, easeIndex);
}

public static Tweener DoMove(this GameObject target, float targetX, float targetY, float targetZ, float duration = 0, Action endCall = null, bool snapping = false, int easeIndex = 0)
{
    if (target == null)
    {
        return null;
    }
    var endvalue = new Vector3(targetX, targetY, targetZ);
    var tweener = target.transform.DOMove(endvalue, duration, snapping);
    if (endCall != null)
    {
        tweener.onComplete = (() =>
        {
            endCall();
        });
    }
    if (easeIndex > 0)
    {
        tweener.SetEase((Ease)easeIndex);
    }
    return tweener;
}

#endregion

#region DOLocalMove 执行DoTween本地坐标移动 参数：X,Y,Z,时间，回调，只取整数值（默认为false）, easeIndex缓动类型

public static Tweener DOLocalMove(this Component target, float targetX, float targetY, float targetZ, float duration = 0, Action endCall = null, bool snapping = false, int easeIndex = 0)
{
    return DOLocalMove(target?.gameObject, targetX, targetY, targetZ, duration, endCall, snapping, easeIndex);
}

public static Tweener DOLocalMove(this GameObject target, float targetX, float targetY, float targetZ, float duration = 0, Action endCall = null, bool snapping = false, int easeIndex = 0)
{
    if (target == null)
    {
        return null;
    }
    var endvalue = new Vector3(targetX, targetY, targetZ);
    var tweener = target.transform.DOLocalMove(endvalue, duration, snapping);
    if (endCall != null)
    {
        tweener.onComplete = (() =>
        {
            endCall();
        });
    }
    if (easeIndex > 0)
    {
        tweener.SetEase((Ease)easeIndex);
    }
    return tweener;
}

#endregion

#region DOScale 执行DOScale缩放 参数：缩放值，时间，回调, easeIndex缓动类型

public static Tweener DOScale(this Component target, float targetScale, float duration, Action endCall, int easeIndex = 0)
{
    return DOScale(target?.gameObject, targetScale, duration, endCall, easeIndex);
}

public static Tweener DOScale(this GameObject target, float targetScale, float duration, Action endCall = null, int easeIndex = 0)
{
    if (target == null)
    {
        return null;
    }
    var tweener = target.transform.DOScale(targetScale, duration);
    if (endCall != null)
    {
        tweener.onComplete = (() =>
        {
            endCall();
        });
    }
    if (easeIndex > 0)
    {
        tweener.SetEase((Ease)easeIndex);
    }
    return tweener;
}

#endregion

#region DOFade 执行DOFade变淡 参数：阿尔法值，时间，回调, easeIndex缓动类型

public static Tweener DOFade(this Component target, float alpha, float duration, Action endCall, int easeIndex = 0)
{
    return DOFade(target?.gameObject, alpha, duration, endCall, easeIndex);
}

public static Tweener DOFade(this GameObject target, float alpha, float duration, Action endCall = null, int easeIndex = 0)
{
    if (target == null)
    {
        return null;
    }
    CanvasGroup cg = target.GetComponent<CanvasGroup>();
    Tweener tweener = null;
    if (cg)
    {
        tweener = cg.DOFade(alpha, duration);
        if (endCall != null)
        {
            tweener.onComplete = (() =>
            {
                endCall();
            });
        }
        if (easeIndex > 0)
        {
            tweener.SetEase((Ease)easeIndex);
        }
    }
    else
    {
        endCall?.Invoke();
    }
    return tweener;
}

#endregion

#region DORotate 执行DORotate选择 参数：x旋转量，y旋转量，z旋转量，时间，回调, easeIndex缓动类型

public static Tweener DORotate(this Component target, float x, float y, float z, float duration, Action endCall = null, int easeIndex = 0)
{
    return DORotate(target?.gameObject, x, y, z, duration, endCall, easeIndex);
}

public static Tweener DORotate(this GameObject target, float x, float y, float z, float duration, Action endCall = null, int easeIndex = 0)
{
    if (target == null)
    {
        return null;
    }
    CanvasGroup cg = target.GetComponent<CanvasGroup>();
    Tweener tweener = null;
    if (cg)
    {
        tweener = cg.transform.DORotate(new Vector3(x, y, z), duration);
        if (endCall != null)
        {
            tweener.onComplete = (() =>
            {
                endCall();
            });
        }
        if (easeIndex > 0)
        {
            tweener.SetEase((Ease)easeIndex);
        }
    }
    else
    {
        endCall?.Invoke();
    }
    return tweener;
}

#endregion

#region DOScaleX 执行DOScaleX缩放 参数：缩放值，时间，回调, easeIndex缓动类型

public static Tweener DOScaleX(this Component target, float targetScale, float duration, Action endCall, int easeIndex = 0)
{
    return DOScaleX(target?.gameObject, targetScale, duration, endCall, easeIndex);
}

public static Tweener DOScaleX(this GameObject target, float targetScale, float duration, Action endCall = null, int easeIndex = 0)
{
    if (target == null)
    {
        return null;
    }
    var tweener = target.transform.DOScaleX(targetScale, duration);
    if (endCall != null)
    {
        tweener.onComplete = (() =>
        {
            endCall();
        });
    }
    if (easeIndex > 0)
    {
        tweener.SetEase((Ease)easeIndex);
    }
    return tweener;
}

#endregion

#endregion
}