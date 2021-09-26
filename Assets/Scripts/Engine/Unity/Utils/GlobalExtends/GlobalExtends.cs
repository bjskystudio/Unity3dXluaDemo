﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

/// <summary>
/// Aor扩展方法封装集合
/// </summary>
[XLua.LuaCallCSharp]
public static class GlobalExtends
{

    /// <summary>
    /// 获取(广义)字符串字节数(数字/符号/英文字符占1个字节,中文占2个字节)
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static int GetUWidthLength(this string str)
    {
        int byteLen = 0;
        char[] strs = str.ToCharArray();
        int i, len = strs.Length;
        for (i = 0; i < len; i++)
        {
            if (Convert.ToInt32(strs[i]) > 255)
            {
                byteLen += 2;
            }
            else
            {
                byteLen += 1;
            }
        }
        return byteLen;
    }

    /// <summary>
    /// 截取指定字节长度的字符串
    /// </summary>
    /// <param name="str">原字符串</param>
    /// <param name="startIndex">起始位置</param>
    /// <param name="bytesLen">截取字节长度</param>
    /// <returns></returns>
    public static string CutByteString(this string str, int startIndex, int bytesLen)
    {
        string result = string.Empty;// 最终返回的结果
        if (string.IsNullOrEmpty(str)) { return result; }

        int byteLen = System.Text.Encoding.Default.GetByteCount(str);// 单字节字符长度
        int charLen = str.Length;// 把字符平等对待时的字符串长度
        char[] chars = str.ToCharArray();

        if (startIndex == 0)
        {

            int byteCount = 0;// 记录读取进度
            int pos = 0;// 记录截取位置

            for (int i = 0; i < charLen; i++)
            {
                if (Convert.ToInt32(chars[i]) > 255) // 按中文字符计算加2
                {
                    byteCount += 2;
                }
                else // 按英文字符计算加1
                {
                    byteCount += 1;
                }
                if (byteCount > bytesLen)// 超出时只记下上一个有效位置
                {
                    pos = i;
                    break;
                }
                else if (byteCount == bytesLen)// 记下当前位置
                {
                    pos = i + 1;
                    break;
                }
            }
            if (pos >= 0) { result = str.Substring(0, pos); }


        }
        else if (startIndex >= byteLen)
        {
            return result;
        }
        else //startIndex < byteLen
        {

            int AllLen = startIndex + bytesLen;
            int byteCountStart = 0;// 记录读取进度
            int byteCountEnd = 0;// 记录读取进度
            int startpos = 0;// 记录截取位置                
            int endpos = 0;// 记录截取位置

            for (int i = 0; i < charLen; i++)
            {
                if (Convert.ToInt32(chars[i]) > 255) // 按中文字符计算加2
                {
                    byteCountStart += 2;
                }
                else // 按英文字符计算加1
                {
                    byteCountStart += 1;
                }
                if (byteCountStart > startIndex)// 超出时只记下上一个有效位置
                {
                    startpos = i;
                    AllLen = startIndex + bytesLen - 1;
                    break;
                }
                else if (byteCountStart == startIndex)// 记下当前位置
                {
                    startpos = i + 1;
                    break;
                }
            }

            if (startIndex + bytesLen <= byteLen)//截取字符在总长以内
            {
                for (int i = 0; i < charLen; i++)
                {
                    if (Convert.ToInt32(chars[i]) > 255) // 按中文字符计算加2
                    {
                        byteCountEnd += 2;
                    }
                    else // 按英文字符计算加1
                    {
                        byteCountEnd += 1;
                    }
                    if (byteCountEnd > AllLen)// 超出时只记下上一个有效位置
                    {
                        endpos = i;
                        break;
                    }
                    else if (byteCountEnd == AllLen)// 记下当前位置
                    {
                        endpos = i + 1;
                        break;
                    }
                }
                endpos = endpos - startpos;
            }
            else if (startIndex + bytesLen > byteLen)//截取字符超出总长
            {
                endpos = charLen - startpos;
            }
            if (endpos >= 0)
            {
                result = str.Substring(startpos, endpos);
            }
        }
        return result;
    }

    /// <summary>
    /// 可控的误差比较
    /// </summary>
    /// <param name="f1">浮点源1</param>
    /// <param name="f2">浮点源2</param>
    /// <param name="epsilon">公差范围</param>
    /// <returns>是否在误差内</returns>
    public static bool FloatEquel(this float f1, float f2, float epsilon = float.Epsilon)
    {
        return Mathf.Abs(f1 - f2) < epsilon;
    }


    /// <summary>
    /// 克隆一个List
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static List<T> clone<T>(this List<T> list) where T : class
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
    /// Array转LuaTable(仅支持int，string之类的）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <returns></returns>
    public static LuaTable ToLuaTable<T>(this T[] array)
    {
        if (array == null) return null;
        int i, length = array.Length;
        LuaTable luaTable = XLuaManager.Instance.GetLuaEnv().NewTable();
        for (i = 0; i < length; i++)
        {
            luaTable.Set<int, T>(i + 1, array[i]);
        }
        return luaTable;
    }
}