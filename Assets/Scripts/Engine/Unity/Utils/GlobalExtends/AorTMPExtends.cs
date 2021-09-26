using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AorTMP 扩展方法
/// </summary>
[XLua.LuaCallCSharp]
public static class AorTMPExtends
{
    public static Tweener DOText(this AorTMP target, string endValue, float duration, bool richTextEnabled = true, ScrambleMode scrambleMode = ScrambleMode.None, string scrambleChars = null)
    {
        if (target == null)
        {
            return null;
        }
        return DOTween.To(() => target.text, x => target.text = x, endValue, duration)
                    .SetOptions(richTextEnabled, scrambleMode, scrambleChars)
                    .SetTarget(target);
    }
}
