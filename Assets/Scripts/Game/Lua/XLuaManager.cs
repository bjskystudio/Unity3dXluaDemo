using Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using XLua;
using YoukiaCore;

[Hotfix]
[LuaCallCSharp]
[CSharpCallLua]
public class XLuaManager : MonoSingleton<XLuaManager>
{
    /// <summary>
    /// Lua脚本相对目录路径
    /// </summary>
    private const string luaScriptsRelativeFolderPath = "/Res/LuaScripts";

    /// <summary>
    /// Lua PB相对目录路径
    /// </summary>
    private const string luaPBRelativeFolderPath = "/Res/PBData";

    /// <summary>
    /// Lua启动脚本名
    /// </summary>
    private const string gameMainScriptName = "GameMain";

    /// <summary>
    /// 热修复脚本名
    /// </summary>
    private const string hotfixMainScriptName = "HotfixMain";

    /// <summary>
    /// Lua Env
    /// </summary>
    private LuaEnv luaEnv = null;

    /// <summary>
    /// Lua更新脚本
    /// </summary>
    private LuaUpdater luaUpdater = null;

    /// <summary>
    /// Lua脚本二进制缓存
    /// </summary>
    public Dictionary<string, byte[]> LuaScriptsBytesCaching
    {
        get;
        private set;
    }

    /// <summary>
    /// Lua OB二进制缓存
    /// </summary>
    public Dictionary<string, byte[]> LuaPBBytesCaching
    {
        get;
        private set;
    }

    /// <summary>
    /// Lua文件相对路径
    /// </summary>
    public Dictionary<string, string> LuaFileRelativePathDic
    {
        get;
        private set;
    }

    /// <summary>
    /// Lua脚本重新载入回调绑定
    /// </summary>
    public static Action<string> LuaReimport;

    /// <summary>
    /// 游戏是否开始
    /// </summary>
    public bool HasGameStart
    {
        get;
        protected set;
    }

    /// <summary>
    /// 是否启用HotFix
    /// </summary>
    public bool HasHotfix
    {
        get;
        protected set;
    }

    /// <summary>
    /// 加载Lua脚本资源
    /// </summary>
    /// <param name="loadcompletedcb">加载完成回调</param>
    public void LoadLuaScriptsRes(Action loadcompletedcb = null)
    {
        LuaScriptsBytesCaching.Clear();
        if (Launcher.Instance.UsedAssetBundle && Launcher.Instance.UsedLuaAssetBundle)
        {
            ResourceLoad.ResourceManager.Instance.LoadABTextAll("luascriptsbyte/luascriptsbyte_bundle", (list, res) =>
            {
                for (int i = 0; i < list.Count; i++)
                {
                    LuaScriptsBytesCaching.Add(list[i].name, list[i].bytes);
                }
                res.Release();
                loadcompletedcb?.Invoke();
            }, false);
        }
        else
        {
            string luascriptfolderfullpath = Application.dataPath + luaScriptsRelativeFolderPath;
            string[] files = Directory.GetFiles(luascriptfolderfullpath, "*.lua", SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; i++)
            {
                string luaname = Path.GetFileNameWithoutExtension(files[i]);
                var bytes = File.ReadAllBytes(files[i]);
                if (!LuaScriptsBytesCaching.ContainsKey(luaname))
                {
                    LuaScriptsBytesCaching.Add(luaname, bytes);
                }
                else
                {
                    Debug.LogError($"有重名的Lua脚本:{luaname}!");
                }
#if UNITY_EDITOR_WIN
                if (!LuaFileRelativePathDic.ContainsKey(luaname))
                {
                    string luaRelativePath = files[i].Replace(luascriptfolderfullpath, "");
                    luaRelativePath = luaRelativePath.Replace('\\', '/').Remove(0, 1);
                    LuaFileRelativePathDic.Add(luaname, luaRelativePath);
                }
                else
                {
                    Debug.LogError($"有重名的Lua脚本:{luaname}!");
                }
#endif
            }
            loadcompletedcb?.Invoke();
        }
    }

    /// <summary>
    /// 加载Lua PB资源
    /// </summary>
    /// <param name="loadcompletedcb">加载完成回调</param>
    public void LoadLuaPBRes(Action loadcompletedcb = null)
    {
        LuaPBBytesCaching.Clear();
        if (Launcher.Instance.UsedAssetBundle && Launcher.Instance.UsedLuaAssetBundle)
        {
            ResourceLoad.ResourceManager.Instance.LoadABTextAll("pbdata/pbdata_bundle", (list, res) =>
            {
                for (int i = 0; i < list.Count; i++)
                {
                    LuaPBBytesCaching.Add(list[i].name, list[i].bytes);
                }
                res.Release();
                loadcompletedcb?.Invoke();
            }, false);
        }
        else
        {
            string luapbfolderfullpath = Application.dataPath + luaPBRelativeFolderPath;
            string[] files = Directory.GetFiles(luapbfolderfullpath, "*.bytes", SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; i++)
            {
                string pbname = Path.GetFileNameWithoutExtension(files[i]);
                var bytes = File.ReadAllBytes(files[i]);
                if (!LuaPBBytesCaching.ContainsKey(pbname))
                {
                    LuaPBBytesCaching.Add(pbname, bytes);
                }
                else
                {
                    Debug.LogError($"有重名的PB文件:{pbname}!");
                }
            }
            loadcompletedcb?.Invoke();
        }
    }

    protected override void Init()
    {
        LuaScriptsBytesCaching = new Dictionary<string, byte[]>();
        LuaPBBytesCaching = new Dictionary<string, byte[]>();
        LuaFileRelativePathDic = new Dictionary<string, string>();
        //用于处理编辑器模式下虚拟器卸载问题
#if UNITY_EDITOR
#pragma warning disable CS0618 // 类型或成员已过时
        EditorApplication.playmodeStateChanged += ChangedPlaymodeState;
#pragma warning restore CS0618 // 类型或成员已过时
#endif
    }

    /// <summary>
    /// 初始化Lua环境
    /// </summary>
    public void InitLuaEnv()
    {
        if (luaEnv != null)
        {
            Debug.LogError($"重复初始化Lua环境!");
            return;
        }
        Debug.Log($"初始化Lua环境!");
        luaEnv = new LuaEnv();
        luaEnv.AddBuildin("pb", XLua.LuaDLL.Lua.LoadLuaProfobuf);
        HasGameStart = false;
        HasHotfix = false;
        if (luaEnv != null)
        {
            luaEnv.AddLoader(CustomLoader);
        }
        else
        {
            Debug.LogError("InitLuaEnv null!!!");
        }

        //加载LuaMain.lua
        LoadScript(gameMainScriptName);
        StartGame();
        luaUpdater = gameObject.GetComponent<LuaUpdater>();
        if (luaUpdater == null)
        {
            luaUpdater = gameObject.AddComponent<LuaUpdater>();
        }
        luaUpdater.OnInit(luaEnv);
    }

    /// <summary>
    /// 获取当前Lua Env
    /// </summary>
    /// <returns></returns>
    public LuaEnv GetLuaEnv()
    {
        return luaEnv;
    }

    /// <summary>
    /// 强制重写LuaCaching
    /// </summary>
    /// <param name="file"></param>
    public void ReLoadLuaFileCaching(string file)
    {
        string luaName = Path.GetFileNameWithoutExtension(file);
        var bytes = File.ReadAllBytes(file);

        LuaScriptsBytesCaching[luaName] = bytes;

        if (LuaScriptsBytesCaching.ContainsKey(luaName))
        {
            Debug.Log("<color=gold><size=15>" + "强制重写LuaCaching:    " + luaName + "</size></color>");
        }
        else
        {
            Debug.Log("<color=gold><size=15>" + "新增LuaCaching:    " + luaName + "</size></color>");
        }
    }

    /// <summary>
    /// 启用热修复
    /// </summary>
    public void StartHotfix()
    {
        if (luaEnv == null)
        {
            return;
        }
        HasHotfix = true;
        LoadScript(hotfixMainScriptName);
        SafeDoString(hotfixMainScriptName + ":Start()");
    }

    /// <summary>
    /// 停止热修复
    /// </summary>
    public void StopHotfix()
    {
        if (luaEnv != null && HasHotfix)
        {
            SafeDoString(hotfixMainScriptName + ":Stop()");
        }
    }

    /// <summary>
    /// 游戏开始
    /// </summary>
    private void StartGame()
    {
        if (luaEnv != null)
        {
            SafeDoString(gameMainScriptName + ":Start()");
            HasGameStart = true;
        }
    }

    /// <summary>
    /// 游戏Stop
    /// </summary>
    private void StopGameMain()
    {
        if (luaEnv != null && HasGameStart)
        {
            SafeDoString(gameMainScriptName + ":Stop()");
        }
    }

    /// <summary>
    /// 加载指定脚本
    /// </summary>
    /// <param name="scriptName"></param>
    public void LoadScript(string scriptName)
    {
        SafeDoString(string.Format("require('{0}')", scriptName));
    }

    /// <summary>
    /// 重载指定脚本
    /// </summary>
    /// <param name="scriptName"></param>
    private void ReimportScript(string scriptName)
    {
        LuaReimport?.Invoke(scriptName);
    }

    /// <summary>
    /// 自定义Lua脚本加载回调
    /// </summary>
    /// <param name="filepath"></param>
    /// <returns></returns>
    private byte[] CustomLoader(ref string filepath)
    {
        string scriptPath = string.Empty;

        filepath = filepath.Replace(".", "/");
        var luaNames = filepath.Split('/');
        var luaName = luaNames[luaNames.Length - 1];
        scriptPath = filepath;
        byte[] bytes = null;
        LuaScriptsBytesCaching.TryGetValue(luaName, out bytes);
        if (bytes == null)
        {
            if (scriptPath == "emmy_core")
            {
                return null;
            }
            Debug.LogError("找不到脚本：" + scriptPath);
        }
        return bytes;
    }

    private void Update()
    {
        if (luaEnv != null)
        {
            luaEnv.Tick();
        }
    }
#if UNITY_EDITOR
    GUIStyle style;
    int frameOldMemory, frameDiff, frameOldDiff;
    int secondsOldMemory, secondsDiff, secondsOldDiff;
    string frameDesc;
    string secondsDesc;
    int frameDelay = 1;
    int secondsDelay = 1;
    bool isCoFrameEnd = true;
    bool isCoSecondsEnd = true;
    private void OnGUI()
    {
        if (!XLuaEditorExpand.EnableLuaAnalyzer)
        {
            return;
        }
        if (luaEnv == null)
        {
            return;
        }
        if (isCoFrameEnd)
        {
            isCoFrameEnd = false;
            StartCoroutine(CoFrame());
        }
        if (isCoSecondsEnd)
        {
            isCoSecondsEnd = false;
            StartCoroutine(CoSeconds());
        }
        if (luaEnv != null && !Launcher.Instance.ShowLuaMem)
        {
            if (style == null)
            {
                style = new GUIStyle();
                style.normal.textColor = Color.green;
            }
            GUI.Label(new Rect(10, Screen.height - 260, 200, 20), frameDesc, style);
            GUI.Label(new Rect(10, Screen.height - 240, 200, 20), secondsDesc, style);
        }
    }
    IEnumerator CoFrame()
    {
        for (int i = 0; i < frameDelay; i++)
        {
            yield return 1;
        }
        isCoFrameEnd = true;
        frameDiff = luaEnv.Memroy - frameOldMemory;
        if (frameDiff != 0)
        {
            frameOldDiff = frameDiff;
        }
        frameDesc = $"Lua Memory每{frameDelay}帧计算:{luaEnv.Memroy}Kb，差异：{frameOldDiff}Kb";
        frameOldMemory = luaEnv.Memroy;
    }
    IEnumerator CoSeconds()
    {
        yield return new WaitForSeconds(secondsDelay);
        isCoSecondsEnd = true;
        secondsDiff = luaEnv.Memroy - secondsOldMemory;
        if (secondsDiff != 0)
        {
            secondsOldDiff = secondsDiff;
        }
        secondsDesc = $"Lua Memory每{secondsDelay}秒计算:{luaEnv.Memroy}Kb，差异：{secondsOldDiff}Kb";
        secondsOldMemory = luaEnv.Memroy;
    }
#endif

#if UNITY_EDITOR
    private void ChangedPlaymodeState()
    {
        if (!EditorApplication.isPlaying && !EditorApplication.isPlayingOrWillChangePlaymode)
        {
            Dispose();
        }
    }
#endif

    /// <summary>
    /// 停止虚拟机
    /// </summary>
    public void StopLuaEnv()
    {
        DeleteDelegate();
        if (luaUpdater != null)
        {
            DestroyImmediate(luaUpdater.gameObject);
            luaUpdater = null;
        }
    }

    /// <summary>
    /// 重启虚拟机
    /// </summary>
    public void Restart()
    {
        StopHotfix();
        StopGameMain();
        StopLuaEnv();
        LuaScriptsBytesCaching.Clear();
        LuaPBBytesCaching.Clear();
        string script = @"
                local load = {[""math""] = true,[""io""] = true,[""coroutine""] = true,[""debug""] = true,[""utf8""] = true,[""package""] = true,[""string""] = true,[""os""] = true,[""_G""] = true,[""table""] = true,}
                for key,val in pairs(package.loaded) do
                    if load[key] ~= true  then
                        package.loaded[key] = nil;
                    end
                end
                local t = {[""realRequire""] = true,[""rawlen""] = true,[""setfenv""] = true,[""tostring""] = true,[""table""] = true,[""typeof""] = true,[""type""] = true,[""assert""] = true,[""getmetatable""] = true,[""tonumber""] = true,[""xlua""] = true,[""print""] = true,[""require""] = true,[""rawset""] = true,[""next""] = true,[""package""] = true,[""collectgarbage""] = true,[""_VERSION""] = true,[""select""] = true,[""utf8""] = true,[""_G""] = true,[""xpcall""] = true,[""error""] = true,[""debug""] = true,[""dofile""] = true,[""rawequal""] = true,[""string""] = true,[""cast""] = true,[""pairs""] = true,[""io""] = true,[""template""] = true,[""uint64""] = true,[""CS""] = true,[""getfenv""] = true,[""ipairs""] = true,[""rawget""] = true,[""os""] = true,[""setmetatable""] = true,[""loadfile""] = true,[""math""] = true,[""pcall""] = true,[""load""] = true,[""base""] = true,[""coroutine""] = true,}
                for key, val in pairs(_G) do
                    if t[key] ~= true  then
                        _G[key] = nil
                    end
                end
                collectgarbage(""collect"")";
        SafeDoString(script);
    }

    public override void Dispose()
    {
        LuaReimport = null;
        //删除委托
        DeleteDelegate();
        if (luaUpdater != null)
        {
            luaUpdater.OnDispose();
        }
#if !UNITY_EDITOR
            // 关闭虚拟机
            if (luaEnv != null)
            {
                try
                {
                    luaEnv.Dispose();
                }
                catch (System.Exception ex)
                {
                    string msg = string.Format("xLua exception : {0}\n {1}", ex.Message, ex.StackTrace);
                    Debug.LogError(msg, null);
                }
                finally
                {
                    luaEnv = null;
                }
            }
#endif
    }

    /// <summary>
    ///  删除委托
    /// </summary>
    public void DeleteDelegate()
    {

    }

    public void SafeDoString(string scriptContent)
    {
        if (luaEnv != null)
        {
            try
            {
                luaEnv.DoString(scriptContent);
            }
            catch (System.Exception ex)
            {
                string msg = string.Format("xLua exception : {2} >> {0}\n {1}", ex.Message, ex.StackTrace, scriptContent);
                Debug.LogError(msg, null);
            }
        }
    }

    public LuaTable CreateNewTable()
    {
        return luaEnv != null ? luaEnv.NewTable() : null;
    }
}

#if UNITY_EDITOR
/// <summary>
/// XLua编辑器工具扩展
/// </summary>
public class XLuaEditorExpand
{
    private const string LuaAnalyzerTitle = "Tools/GUI增强/开启Lua性能分析器";
    //是否开启Lua性能分析器
    [MenuItem(LuaAnalyzerTitle, false)]
    public static void LuaAnalyzerToggle()
    {
        EnableLuaAnalyzer = !EnableLuaAnalyzer;
    }
    [MenuItem(LuaAnalyzerTitle, true)]
    public static bool SetLuaAnalyzerToggle()
    {
        Menu.SetChecked(LuaAnalyzerTitle, EnableLuaAnalyzer);
        return true;
    }

    //是否开启Lua性能分析器
    private const string EnableLuaAnalyzerKey = "EnableLuaAnalyzer";
    private static int enableLuaAnalyzer = -1;
    internal static bool EnableLuaAnalyzer
    {
        get
        {
            if (enableLuaAnalyzer == -1)
            {
                enableLuaAnalyzer = EditorPrefs.GetInt(EnableLuaAnalyzerKey, 1);
            }
            return enableLuaAnalyzer == 1;
        }
        set
        {
            int newValue = value ? 1 : 0;
            if (newValue != enableLuaAnalyzer)
            {
                enableLuaAnalyzer = newValue;
                EditorPrefs.SetInt(EnableLuaAnalyzerKey, enableLuaAnalyzer);
            }
        }
    }
}
#endif