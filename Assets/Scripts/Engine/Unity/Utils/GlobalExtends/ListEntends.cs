using System;
using System.Collections.Generic;

[XLua.LuaCallCSharp]
public static class ListEntends
{
    /// <summary>
    /// 克隆一个List
    /// </summary>
    public static List<T> Clone<T>(this List<T> list) where T : class
    {
        if (list == null) return null;
        List<T> t = new List<T>();
        int i, length = list.Count;
        for (i = 0; i < length; i++)
        {
            T item = list[i] as T;
            t.Add(item);
        }
        return t;
    }


    /// <summary>
    /// 冒泡排序
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="list">容器</param>
    /// <param name="func">判断方法</param>
    public static void BubbleSort<T>(this List<T> list, System.Func<T, T, bool> func)
    {
        T temp = default(T);
        for (int i = list.Count; i > 0; i--)
        {
            for (int j = 0; j < i - 1; j++)
            {
                if (func(list[j], list[j + 1]))
                {
                    temp = list[j];
                    list[j] = list[j + 1];
                    list[j + 1] = temp;
                }
            }
        }
    }

    static public void BubbleSort<T>(this List<T> array, Comparison<T> comparison)
    {
        T temp;//存储临时变量
        for (int i = 0; i < array.Count; i++)
            for (int j = i - 1; j >= 0; j--)
                //if (intArray[j + 1] < intArray[j])
                if (comparison(array[j + 1], array[j]) < 0)
                {
                    temp = array[j + 1];
                    array[j + 1] = array[j];
                    array[j] = temp;
                }
    }
}