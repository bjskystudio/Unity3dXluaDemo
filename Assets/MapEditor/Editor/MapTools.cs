using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class MapTools 
{
    public static List<List<string>> GetCfgFileInfo(string name)
    {
        string path = GetConfigFilePath(name);
        if (path == null) return null;
        string strInfo = File.ReadAllText(path);
        string[] str = strInfo.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        if (str.Length < 3)
        {
            str = strInfo.Split(new string[] { "\n" },
                StringSplitOptions.RemoveEmptyEntries);
        }
        List<List<string>> ss = new List<List<string>>();
        for (int i = 0; i < str.Length; i++)
        {
            ss.Add(str[i].Split('\t').ToList());
        }
        return ss;
    }

    public static List<List<string>> GetCfgFilePathInfo(string path)
    {
        string strInfo = File.ReadAllText(path);
        string[] str = strInfo.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        if (str.Length < 3)
        {
            str = strInfo.Split(new string[] { "\n" },
                StringSplitOptions.RemoveEmptyEntries);
        }
        List<List<string>> ss = new List<List<string>>();
        for (int i = 0; i < str.Length; i++)
        {
            ss.Add(str[i].Split('\t').ToList());
        }
        return ss;
    }
    /// <summary>
    /// 根据路径获取fileinfo
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static List<List<string>> GetCfgFileInfoByPath(string path)
    {
        if (path == null) return null;
        string strInfo = File.ReadAllText(path);
        string[] str = strInfo.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        if (str.Length < 3)
        {
            str = strInfo.Split(new string[] { "\n" },
                StringSplitOptions.RemoveEmptyEntries);
        }
        List<List<string>> ss = new List<List<string>>();
        for (int i = 0; i < str.Length; i++)
        {
            ss.Add(str[i].Split('\t').ToList());
        }
        return ss;
    }
    public static string GetConfigFilePath(string fileName)
    {
        string[] fileArr = Directory.GetFiles(Application.dataPath + "/TempRes/TxtConfig/World", "*.txt", SearchOption.AllDirectories);
        for (int i = 0; i < fileArr.Length; i++)
        {
            if (fileArr[i].Substring(fileArr[i].LastIndexOf('\\') + 1) == fileName + ".txt")
                return fileArr[i];
        }
        return null;
    }
    /// <summary>
    /// 获取WorldDefine中的枚举配置
    /// </summary>
    /// <returns></returns>
    public static Dictionary<string, string> GetWordType()
    {
        Dictionary<string, string> TypeConfig = new Dictionary<string, string>();
        string c = File.ReadAllText(Application.dataPath + "/Res/LuaScripts/Framework/Define/WorldDefine.lua");
        int index = c.IndexOf("WorldDefine.eItemType =");
        var newc = c.Substring(index, 1500);


        var array = newc.Substring(newc.IndexOf("{"), newc.IndexOf("}")-1).Split('\n').ToList();

        array.RemoveAt(0);
        array.RemoveAt(array.Count - 1); //懒得查了直接删多余的
        array.RemoveAt(array.Count-1);
        List<string> mlist = new List<string>();
        for (int i = 0; i < array.Count; i++)
        {
            if (!string.IsNullOrEmpty(array[i]))
            {
                mlist.Add(array[i]);
            }
        }

        for (int i = 0; i < mlist.Count; i += 2)
        {
            var cc = mlist[i].Split('#');
            string cfg = "";
            if (cc.Length > 1) cfg = cc[1];
            if (i + 1 < mlist.Count)
            {
                var type = mlist[i + 1];
                var tt = type.Replace(",", "").Replace(" ", "").Split('=');
                if (!string.IsNullOrEmpty(cfg))
                {
                    string des = cc[0].Trim().Replace("---", "");
                    TypeConfig.Add(tt[1].Trim(), des.Trim() + "#" + cfg.Trim());
                }
            }

        }
        return TypeConfig;
    }

}
