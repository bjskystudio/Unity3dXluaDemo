using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Framework;
using UnityEngine;
using XLua;

[Hotfix]
[LuaCallCSharp]
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
    /// Lua Env
    /// </summary>
    private LuaEnv luaEnv = null;


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
    protected override void Init()
    {
        LuaScriptsBytesCaching = new Dictionary<string, byte[]>();
        LuaPBBytesCaching = new Dictionary<string, byte[]>();
        LuaFileRelativePathDic = new Dictionary<string, string>();
        //用于处理编辑器模式下虚拟器卸载问题
#if UNITY_EDITOR
#pragma warning disable CS0618 // 类型或成员已过时
        //EditorApplication.playmodeStateChanged += ChangedPlaymodeState;
#pragma warning restore CS0618 // 类型或成员已过时
#endif
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
        //HasHotfix = false;
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
        //luaUpdater = gameObject.GetComponent<LuaUpdater>();
        //if (luaUpdater == null)
        //{
        //    luaUpdater = gameObject.AddComponent<LuaUpdater>();
        //}
        //luaUpdater.OnInit(luaEnv);
    }


    /// <summary>
    /// 加载Lua脚本资源
    /// </summary>
    /// <param name="loadcompletedcb">加载完成回调</param>
    public void LoadLuaScriptsRes(Action loadcompletedcb = null)
    {
        LuaScriptsBytesCaching.Clear();
        // TODO:
        // 判定是否用AB加载模式
        if (Launcher.Instance.UsedAssetBundle && Launcher.Instance.UsedLuaAssetBundle)
        {
            string finalPath = ResUtils.GetResFullPath("luascriptsbyte/luascriptsbyte_bundle.assetbundle");
            AssetBundle _assetBundle = AssetBundle.LoadFromFile(finalPath);
            var assets = _assetBundle.LoadAllAssets<TextAsset>();
            for (int i = 0; i < assets.Length; i++)
            {
                LuaScriptsBytesCaching.Add(assets[i].name, assets[i].bytes);
            }
            _assetBundle.Unload(true);
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
        }
        loadcompletedcb?.Invoke();
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

    /// <summary>
    /// 重载指定脚本
    /// </summary>
    /// <param name="scriptName"></param>
    private void ReimportScript(string scriptName)
    {
        LuaReimport?.Invoke(scriptName);
    }

    public override void Dispose()
    {
        LuaReimport = null;
        //删除委托
        DeleteDelegate();
        //if (luaUpdater != null)
        //{
        //    luaUpdater.OnDispose();
        //}
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

}
