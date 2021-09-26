using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Graphic扩展方法
/// </summary>
[XLua.LuaCallCSharp]
public static class GraphicExtends
{
    /// <summary>
    /// 通过十六进制字串设置颜色RGB值
    /// </summary>
    public static void SetColorByHexadecimal(this Graphic graphic, string hexadecimal)
    {
        Color color = new Color();
        if (ColorUtility.TryParseHtmlString(hexadecimal, out color))
        {
            graphic.color = color;
        }
    }
}