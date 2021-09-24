using ResourceLoad;
using System;
using System.IO;
using UnityEngine;
using YoukiaCore.Log;
using YoukiaCore.Utils;
using UObject = UnityEngine.Object;


//*******************************************
//
// 名称: 资源加载管理器
// 描述: 提供Lua加载资源的方法调用
//       及事件注入
//
//*******************************************
[XLua.CSharpCallLua]
public sealed class AssetLoadManager : DataSingleton<AssetLoadManager>
{
    /// <summary>
    /// 静态的Lua端需要注入的委托
    /// </summary>
    public static LoadObjectCompleteCallBack ConstLuaCallBack;

    protected override void Destroy() { }

    protected override void Init() { }

    protected override void ResetData() { }

    /// <summary>
    /// 获取shader,外部统一走这里
    /// </summary>
    /// <param name="shaderName">shader名字</param>
    /// <returns></returns>
    public Shader LoadShader(string shaderName)
    {
        return ResourceManager.Instance.GetShader(shaderName);
    }

    #region Lua端调用
    /// <summary>
    /// 根据类型参数加载资源
    /// </summary>
    /// <param name="assetName">资源相对路径</param>
    /// <param name="typeName">资源类型名称</param>
    /// <param name="isSync">是否同步加载</param>
    public void LoadObj(string path, string typeName, bool isSync = false)
    {
        AssetLoadType type = GetAssetLoadType(typeName);
        var callback = GetLuaStaticCallback(path, type);
        LoadObj(path, type, isSync, callback);
    }
    #endregion

    #region C#端调用
    /// <summary>
    /// 根据类型参数加载资源
    /// </summary>
    /// <param name="path">资源相对路径</param>
    /// <param name="assetName">资源名称</param>
    /// <param name="type">资源类型</param>
    /// <param name="isSync">是否同步加载</param>
    /// <param name="callback">加载回调</param>
    public void LoadObj(string path, AssetLoadType type, bool isSync = false, Action<UObject, ResRef> callback = null)
    {
        switch (type)
        {
            case AssetLoadType.ePrefab:
                Action<UObject> tCallBack = (obj) =>
                {
                    callback(obj, null);
                };
                ResourceManager.Instance.LoadPrefabInstance(path, tCallBack, isSync);
                break;
            case AssetLoadType.eAtlasSprite:
                ResourceManager.Instance.LoadSpriteUSA(path, callback, isSync);
                break;
            case AssetLoadType.eSprite:
                ResourceManager.Instance.LoadSpriteSingle(path, callback, isSync);
                break;
            case AssetLoadType.eTexture:
                ResourceManager.Instance.LoadTexture(path, callback, isSync);
                break;
            case AssetLoadType.eAudioClip:
                ResourceManager.Instance.LoadAudioClip(path, callback, isSync);
                break;
            case AssetLoadType.eAnimationClip:
                ResourceManager.Instance.LoadAnimationClip(path, callback, isSync);
                break;
            case AssetLoadType.eMaterial:
                ResourceManager.Instance.LoadMaterial(path, callback, isSync);
                break;
            case AssetLoadType.eText:
                ResourceManager.Instance.LoadText(path, callback, isSync);
                break;
            case AssetLoadType.eTMPFont:
                ResourceManager.Instance.LoadABTMPFont(path, GetFileName(path), callback, isSync);
                break;
            case AssetLoadType.eFont:
                ResourceManager.Instance.LoadABFont(path, GetFileName(path), callback, isSync);
                break;
            case AssetLoadType.eScriptableObject:
                ResourceManager.Instance.LoadScriptableObject(path, callback, isSync);
                break;
            default:
                Log.Warning(type, "没有对应的资源加载方法");
                break;
        }
    }
    #endregion

    /// <summary>
    /// 释放资源
    /// </summary>
    /// <param name="path">路径</param>
    /// <param name="assetName">资源名，可不填</param>
    /// <param name="isAll">是否全部</param>
    //public void Release(string path , string assetName = "", bool isAll = false)
    //{
    //    //ResourceManager.Instance.Release(path, assetName, isAll);
    //}

    /// <summary>
    /// 封装Lua回调函数
    /// </summary>
    /// <param name="path">资源相对路径</param>
    /// <param name="type">资源类型</param>
    /// <returns>加载回调</returns>
    private Action<UObject, ResRef> GetLuaStaticCallback(string path, AssetLoadType type)
    {
        if (ConstLuaCallBack == null)
        {
            return null;
        }
        else
        {
            return (obj, res) =>
            {
                ConstLuaCallBack(path, obj, res);
            };
        }
        //以后需要强转在回复，暂时注释
        //switch (type)
        //{
        //    case AssetLoadType.ePrefab:
        //        return obj =>
        //        {
        //            ConstLuaCallBack(path, obj as GameObject);
        //        };
        //    case AssetLoadType.eSprite:
        //        return obj =>
        //        {
        //            ConstLuaCallBack(path, obj as Sprite);
        //        };
        //    case AssetLoadType.eTexture:
        //        return obj =>
        //        {
        //            ConstLuaCallBack(path, obj as Texture);
        //        };
        //    case AssetLoadType.eAudioClip:
        //        return obj =>
        //        {
        //            ConstLuaCallBack(path, obj as AudioClip);
        //        };
        //    case AssetLoadType.eAnimationClip:
        //        return obj =>
        //        {
        //            ConstLuaCallBack(path, obj as AnimationClip);
        //        };
        //    case AssetLoadType.eMaterial:
        //        return obj =>
        //        {
        //            ConstLuaCallBack(path, obj as Material);
        //        };
        //    case AssetLoadType.eText:
        //        return obj =>
        //        {
        //            ConstLuaCallBack(path, obj as TextAsset);
        //        };
        //    case AssetLoadType.eTMPFont:
        //        return obj =>
        //        {
        //            ConstLuaCallBack(path, obj as TMP_FontAsset);
        //        };
        //    case AssetLoadType.eScriptableObject:
        //        return obj =>
        //        {
        //            ConstLuaCallBack(path, obj as ScriptableObject);
        //        };
        //    default:
        //        Log.Warning(type, "没有对应的资源加载方法");
        //        return null;
        //}
    }

    /// <summary>
    /// 获取资源名称
    /// </summary>
    /// <param name="path">资源相对路径</param>
    /// <returns>资源名称</returns>
    private string GetFileName(string path)
    {
        return Path.GetFileName(path);
    }

    /// <summary>
    /// 获取资源类型
    /// </summary>
    /// <param name="typeName">资源类型名称</param>
    /// <returns></returns>
    private AssetLoadType GetAssetLoadType(string typeName)
    {
        AssetLoadType type = AssetLoadType.eNone;
        Enum.TryParse(typeName, out type);
        return type;
    }
}

/// <summary>
/// 加载资源成功回调（Lua端注入）
/// </summary>
/// <param name="path">相对路径</param>
/// <param name="obj">加载成功的资源</param>
public delegate void LoadObjectCompleteCallBack(string path, UObject obj, ResRef res);

/// <summary>
/// 加载方式枚举
/// </summary>
public enum AssetLoadType
{
    eNone,
    ePrefab,
    eTexture,
    eAudioClip,
    eAnimationClip,
    eText,
    eAtlasSprite,
    eSprite,
    eMaterial,
    eTMPFont,
    eFont,
    eScriptableObject,
}