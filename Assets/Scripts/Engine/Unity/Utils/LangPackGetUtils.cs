using UnityEngine;
using XLua;
using System.IO;
using YoukiaCore.Utils;

public class LangPackGetUtils
{
    private static LangPackGetUtils _Inst;
    public static LangPackGetUtils Inst
    {
        get
        {
            if (_Inst == null)
            {
                _Inst = new LangPackGetUtils();
            }
            return _Inst;
        }
        set { }
    }

    LuaEnv luaEnv;

    public LangPackGetUtils()
    {
        Init();
    }

    public void Init()
    {
        if (luaEnv != null)
        {
            luaEnv.Dispose();
        }

        luaEnv = new LuaEnv();
        luaEnv.AddLoader((ref string filepath) =>
        {
            string scriptPath = string.Empty;
            if (filepath == "Logger")
            {
                filepath = "Framework.Debug.Logger";
            }
            filepath = filepath.Replace(".", "/") + ".lua";
            scriptPath = Path.Combine(Application.dataPath, "Res/LuaScript");
            scriptPath = Path.Combine(scriptPath, filepath);
            return FileUtils.ReadAllBytes(scriptPath);
        });
        luaEnv.DoString("require('AppSetting')");
        luaEnv.DoString("require('Framework.Util.LuaUtil')");
        luaEnv.DoString("require('Framework.OOP.ConstClass')");
    }

    public string GetLangPackValue(string key, bool isNeedTips)
    {
        try
        {
            return luaEnv.DoString($"return require('LuaConfig.Auto.Common.LanguagePackage')['{key}'].CN")[0] as string;
        }
        catch
        {
#if UNITY_EDITOR
            if (isNeedTips)
                UnityEditor.EditorUtility.DisplayDialog("Wrong!", "Language Key错误或者为空", "OK");
            return "";
#endif
        }
    }

}
