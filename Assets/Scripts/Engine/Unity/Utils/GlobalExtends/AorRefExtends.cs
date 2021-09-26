using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

/// <summary>
/// 常用反射Extends;
/// </summary>
[XLua.LuaCallCSharp]
public static class AorRefExtends
{

    // -- Field get
    #region Field Get

    //缓存
    static Dictionary<Type, Dictionary<string, PropertyInfo>> AllSerializePropertiesCache = new Dictionary<Type, Dictionary<string, PropertyInfo>>();
    static Dictionary<Type, Dictionary<string, FieldInfo>> AllSerializeFieldsCache = new Dictionary<Type, Dictionary<string, FieldInfo>>();

    static Dictionary<Type, Dictionary<string, FieldInfo>> AllSingleFieldsCache = new Dictionary<Type, Dictionary<string, FieldInfo>>();
    static Dictionary<Type, Dictionary<string, MethodInfo>> AllSingleMethodInfoCache = new Dictionary<Type, Dictionary<string, MethodInfo>>();

    public static Dictionary<string, PropertyInfo> GetAllSerializeProperties(Type t)
    {
        Dictionary<string, PropertyInfo> infos = null;
        if (!AllSerializePropertiesCache.ContainsKey(t))
        {
            infos = new Dictionary<string, PropertyInfo>();
            AllSerializePropertiesCache.Add(t, infos);
        }
        else
        {
            return AllSerializePropertiesCache[t];
        }

        GetAllSerializeProperties(t, ref infos);

        return infos;
    }

    private static void GetAllSerializeProperties(Type t, ref Dictionary<string, PropertyInfo> infos)
    {
        PropertyInfo[] props = t.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);


        for (int i = 0; i < props.Length; i++)
        {
            PropertyInfo each = props[i];
            if (each.CanWrite)
            {
                if (!infos.ContainsKey(each.Name))
                {
                    infos.Add(each.Name, each);
                }
            }
            else
            {
                // CustomAttributeData.GetCustomAttributes(each).Where(d => d.Constructor.DeclaringType == typeof(SerializeField)).Select(d => new AttributeFactory(d)).ToList();


                //序列化标识获得
                // Attribute[] attrs = Attribute.GetCustomAttributes(each);
                object[] attrs = each.GetCustomAttributes(typeof(SerializeField), true);

                if (attrs.Length > 0)
                {
                    if (!infos.ContainsKey(each.Name))
                    {
                        infos.Add(each.Name, each);
                    }
                }
            }
        }


        if (t.BaseType != null)
        {
            GetAllSerializeProperties(t.BaseType, ref infos);
        }


    }
    public static PropertyInfo GetSerializePropertie(Type t, string name)
    {
        if (!AllSerializePropertiesCache.ContainsKey(t))
        {
            AllSerializePropertiesCache.Add(t, new Dictionary<string, PropertyInfo>());
        }

        if (AllSerializePropertiesCache[t].ContainsKey(name))
        {
            return AllSerializePropertiesCache[t][name];
        }
        else
        {
            PropertyInfo prop = t.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            if (prop == null)
            {
                if (t.BaseType != null)
                {
                    return GetSerializePropertie(t.BaseType, name);
                }
            }
            else
            {
                if (prop.CanWrite)
                {

                    AllSerializePropertiesCache[t].Add(name, prop);
                    return prop;
                }
            }


        }

        return null;


    }

    public static FieldInfo GetSerializeField(Type t, string name)
    {
        //无ｄｉｃ建立ｄｉｃ
        if (!AllSerializeFieldsCache.ContainsKey(t))
        {
            AllSerializeFieldsCache.Add(t, new Dictionary<string, FieldInfo>());
        }

        if (AllSerializeFieldsCache[t].ContainsKey(name))
        {
            return AllSerializeFieldsCache[t][name];
        }
        else
        {

            FieldInfo field = t.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            if (field == null)
            {


                if (t.BaseType != null)
                {
                    return GetSerializeField(t.BaseType, name);
                }
            }
            else
            {
                if (field.IsPublic)
                {
                    AllSerializeFieldsCache[t].Add(name, field);
                    return field;

                }
                else
                {

                    //序列化标识获得
                    // Attribute[] attrs = Attribute.GetCustomAttributes(each);
                    object[] attrs = field.GetCustomAttributes(typeof(SerializeField), true);

                    if (attrs.Length > 0)
                    {
                        AllSerializeFieldsCache[t].Add(name, field);
                        return field;
                    }


                }
            }


        }


        return null;


    }

    public static Dictionary<string, FieldInfo> GetAllSerializeFields(Type t)
    {
        Dictionary<string, FieldInfo> infos = null;
        if (!AllSerializeFieldsCache.ContainsKey(t))
        {
            infos = new Dictionary<string, FieldInfo>();
            AllSerializeFieldsCache.Add(t, infos);
        }
        else
        {
            return AllSerializeFieldsCache[t];
        }

        GetAllSerializeFields(t, ref infos);

        return infos;
    }

    private static void GetAllSerializeFields(Type t, ref Dictionary<string, FieldInfo> infos)
    {

        FieldInfo[] fields = t.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);


        for (int i = 0; i < fields.Length; i++)
        {
            FieldInfo each = fields[i];
            if (each.IsPublic)
            {
                if (!infos.ContainsKey(each.Name))
                {
                    infos.Add(each.Name, each);
                }
            }
            else
            {
                //序列化标识获得
                // Attribute[] attrs = Attribute.GetCustomAttributes(each);
                object[] attrs = each.GetCustomAttributes(typeof(SerializeField), true);

                if (attrs.Length > 0)
                {
                    if (!infos.ContainsKey(each.Name))
                    {
                        infos.Add(each.Name, each);
                    }
                }
            }
        }


        if (t.BaseType != null)
        {
            GetAllSerializeFields(t.BaseType, ref infos);
        }


    }
    private static object getField(object obj, string fieldName, BindingFlags inst, BindingFlags area)
    {
        Type t = obj.GetType();
        FieldInfo fieldInfo = null;
        bool hasT = AllSingleFieldsCache.ContainsKey(t);
        object data = null;

        //有T就有Dic
        if (hasT)
        {
            if (AllSingleFieldsCache[t].ContainsKey(fieldName))
            {


                fieldInfo = AllSingleFieldsCache[t][fieldName];

            }
            else
            {
                //不包含字段则dic补字段
                fieldInfo = t.GetField(fieldName, inst | area | BindingFlags.GetField);
                AllSingleFieldsCache[t].Add(fieldName, fieldInfo);
            }

        }
        else
        {

            //无T就补带空Dic的T
            fieldInfo = t.GetField(fieldName, inst | area | BindingFlags.GetField);
            Dictionary<string, FieldInfo> dic = new Dictionary<string, FieldInfo>();
            dic.Add(fieldName, fieldInfo);
            AllSingleFieldsCache.Add(t, dic);
        }

        if (fieldInfo != null)
            data = fieldInfo.GetValue(obj);

        return data;

    }

    //----------------


    public static object GetNonPublicField(this object obj, string fieldName)
    {
        return getField(obj, fieldName, BindingFlags.Instance, BindingFlags.NonPublic);
    }

    public static object GetPublicField(this object obj, string fieldName)
    {
        return getField(obj, fieldName, BindingFlags.Instance, BindingFlags.Public);
    }

    //未验证
    public static object GetNonPublicStaticField(this object obj, string fieldName)
    {
        return getField(obj, fieldName, BindingFlags.Static, BindingFlags.NonPublic);
    }

    //未验证
    public static object GetPublicStaticField(this object obj, string fieldName)
    {
        return getField(obj, fieldName, BindingFlags.Static, BindingFlags.Public);
    }

    #endregion

    // -- Field set
    #region Field Set

    private static bool setField(object obj, string fieldName, object value, BindingFlags inst, BindingFlags area)
    {
        Type t = obj.GetType();
        FieldInfo fieldInfo = null;
        bool hasT = AllSingleFieldsCache.ContainsKey(t);

        //有T就有Dic
        if (hasT)
        {
            if (AllSingleFieldsCache[t].ContainsKey(fieldName))
            {


                fieldInfo = AllSingleFieldsCache[t][fieldName];

            }
            else
            {
                //不包含字段则dic补字段
                fieldInfo = t.GetField(fieldName, inst | area | BindingFlags.GetField);
                AllSingleFieldsCache[t].Add(fieldName, fieldInfo);

            }

        }
        else
        {

            //无T就补带空Dic的T
            fieldInfo = t.GetField(fieldName, inst | area | BindingFlags.GetField);
            Dictionary<string, FieldInfo> dic = new Dictionary<string, FieldInfo>();
            dic.Add(fieldName, fieldInfo);
            AllSingleFieldsCache.Add(t, dic);
        }

        if (fieldInfo != null)
        {
            fieldInfo.SetValue(obj, value);
            return true;
        }
        else
        {
            return false;
        }

    }


    public static bool SetNonPublicField(this object obj, string fieldName, object value)
    {
        return setField(obj, fieldName, value, BindingFlags.Instance, BindingFlags.NonPublic);


    }

    public static bool SetPublicField(this object obj, string fieldName, object value)
    {
        return setField(obj, fieldName, value, BindingFlags.Instance, BindingFlags.Public);
    }

    //未验证
    public static bool SetNonPublicStaticField(this object obj, string fieldName, object value)
    {
        return setField(obj, fieldName, value, BindingFlags.Static, BindingFlags.NonPublic);
    }

    //未验证
    public static bool SetPublicStaticField(this object obj, string fieldName, object value)
    {
        return setField(obj, fieldName, value, BindingFlags.Static, BindingFlags.Public);
    }

    #endregion

    // -- InvokeMethod
    #region InvokeMethod


    private static object invokeMethod(object obj, string MethodName, object[] parameters, BindingFlags inst, BindingFlags area)
    {
        Type t = obj.GetType();
        MethodInfo methodInfo = null;
        bool hasT = AllSingleMethodInfoCache.ContainsKey(t);

        //有T就有Dic
        if (hasT)
        {
            if (AllSingleMethodInfoCache[t].ContainsKey(MethodName))
            {


                methodInfo = AllSingleMethodInfoCache[t][MethodName];

            }
            else
            {
                //不包含字段则dic补字段
                methodInfo = t.GetMethod(MethodName, inst | area | BindingFlags.InvokeMethod);
                AllSingleMethodInfoCache[t].Add(MethodName, methodInfo);

            }

        }
        else
        {

            //无T就补带空Dic的T
            methodInfo = t.GetMethod(MethodName, inst | area | BindingFlags.InvokeMethod);
            Dictionary<string, MethodInfo> dic = new Dictionary<string, MethodInfo>();
            dic.Add(MethodName, methodInfo);
            AllSingleMethodInfoCache.Add(t, dic);
        }

        if (methodInfo != null)
        {

            return methodInfo.Invoke(obj, parameters);
        }
        else
        {
            return null;
        }

    }

    public static object InvokeNonPublicMethod(this object obj, string MethodName, object[] parameters)
    {
        return invokeMethod(obj, MethodName, parameters, BindingFlags.Instance, BindingFlags.NonPublic);
    }

    public static object InvokePublicMethod(this object obj, string MethodName, object[] parameters)
    {
        return invokeMethod(obj, MethodName, parameters, BindingFlags.Instance, BindingFlags.Public);
    }

    public static object InvokeNonPublicStaticMethod(this object obj, string MethodName, object[] parameters)
    {
        return invokeMethod(obj, MethodName, parameters, BindingFlags.Static, BindingFlags.NonPublic);
    }

    public static object InvokePublicStaticMethod(this object obj, string MethodName, object[] parameters)
    {
        return invokeMethod(obj, MethodName, parameters, BindingFlags.Static, BindingFlags.Public);
    }

    #endregion
}

