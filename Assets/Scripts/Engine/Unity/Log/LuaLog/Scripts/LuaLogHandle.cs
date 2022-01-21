using System.Text.RegularExpressions;
using UnityEngine;

/*
 * Description:             LuaLogHandle.cs
 * Author:                     zy
 * Create Date:             2021/4/25
 */

public class LuaLogHandle
{
    public int index;
    string root;
    string fileExt;
    public Regex regex;
    public MatchEvaluator matchEvaluator;

    public LuaLogHandle()
    {
        Reset();
        fileExt = "lua";
        root = Application.dataPath.Replace("/Assets", "/Assets/Res/LuaScripts");
        string pattern = @"	([A-Za-z0-9/_.]+):([0-9]+):([ A-Za-z0-9/_.\']+)";

        regex = new Regex(pattern, RegexOptions.IgnoreCase);
        matchEvaluator = new MatchEvaluator(OutPutMatch);
    }

    public void Reset()
    {
        index = 0;
    }

    private string OutPutMatch(Match match)
    {
        string fileName = match.Groups[1].Value;
        if (!XLuaManager.Instance.LuaFileRelativePathDic.ContainsKey(fileName))
        {
            Debug.LogWarning("路径有误:" + fileName);
            return string.Empty;
        }
        string fileRelativePath = XLuaManager.Instance.LuaFileRelativePathDic[fileName];
        return string.Format("	{0} -(at <a href=\"Assets\" line=\"{1}\">{2}/{3}:{4}</a>)", match.Groups[3], index++, root, fileRelativePath, match.Groups[2]);
    }

    public string GetLog(string source)
    {
        if (Launcher.Instance.UsedAssetBundle)
        {
            return source;
        }
#if UNITY_EDITOR
        Reset();
        return regex.Replace(source, matchEvaluator);
#else
        return source;
#endif

    }
}