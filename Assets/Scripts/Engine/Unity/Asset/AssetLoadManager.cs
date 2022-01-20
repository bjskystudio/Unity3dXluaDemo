using ResourceLoad;
using System;
using System.IO;
using TMPro;
using UnityEngine;
using YoukiaCore.Log;
using YoukiaCore.Utils;
using UObject = UnityEngine.Object;

//*******************************************
//
// 名称: 资源加载管理器
// 描述: 接入外部资源加载管理，提供统一的内部API
//
//*******************************************
public sealed class AssetLoadManager : DataSingleton<AssetLoadManager>
{


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

    /// <summary>
    /// 释放无引用资源,一般在切景的时候调用
    /// </summary>
    public void ReleaseInIdle()
    {
        ResourceManager.Instance.ReleaseInIdle();
    }

    /// <summary>
    /// 回调方式,加载prefab并且实例化
    /// 释放方式：GameObject.Destroy，会自动处理引用计数的减少
    /// </summary>
    /// <param name="path">资源路径</param>
    /// <param name="callback">回调</param>
    /// <param name="isSync">是否是同步</param>
    public void LoadPrefabInstance(string path, Action<GameObject> callback = null, bool isSync = false)
    {
        ResourceManager.Instance.LoadPrefabInstance(path, callback, isSync);
    }

    public void LoadSpriteUSA(string path, Action<Sprite, ResRef> callback, bool isSync = false)
    {
        ResourceManager.Instance.LoadSpriteUSA(path, callback, isSync);
    }

    public void LoadSpriteSingle(string path, Action<Sprite, ResRef> callback, bool isSync = false)
    {
        ResourceManager.Instance.LoadSpriteSingle(path, callback, isSync);
    }

    public void LoadTexture(string path, Action<Texture, ResRef> callback, bool isSync = false)
    {
        ResourceManager.Instance.LoadTexture(path, callback, isSync);
    }
    public void LoadAudioClip(string path, Action<AudioClip, ResRef> callback, bool isSync = false)
    {
        ResourceManager.Instance.LoadAudioClip(path, callback, isSync);
    }

    public void LoadAnimationClip(string path, Action<AnimationClip, ResRef> callback, bool isSync = false)
    {
        ResourceManager.Instance.LoadAnimationClip(path, callback, isSync);
    }
    public void LoadMaterial(string path, Action<Material, ResRef> callback, bool isSync = false)
    {
        ResourceManager.Instance.LoadMaterial(path, callback, isSync);
    }
    public void LoadText(string path, Action<TextAsset, ResRef> callback, bool isSync = false)
    {
        ResourceManager.Instance.LoadText(path, callback, isSync);
    }
    public void LoadABTMPFont(string path, Action<TMP_FontAsset, ResRef> callback, bool isSync = false)
    {
        ResourceManager.Instance.LoadABTMPFont(path, GetFileName(path), callback, isSync);
    }
    public void LoadABFont(string path, Action<Font, ResRef> callback, bool isSync = false)
    {
        ResourceManager.Instance.LoadABFont(path, GetFileName(path), callback, isSync);
    }
    public void LoadScriptableObject(string path, Action<ScriptableObject, ResRef> callback, bool isSync = false)
    {
        ResourceManager.Instance.LoadScriptableObject(path, callback, isSync);
    }

    /// <summary>
    /// 根据类型参数加载资源
    /// </summary>
    /// <param name="path">资源相对路径</param>
    /// <param name="assetName">资源名称</param>
    /// <param name="type">资源类型</param>
    /// <param name="isSync">是否同步加载</param>
    /// <param name="callback">加载回调</param>
    public void LoadObj(string path, AssetLoadType type, Action<UObject, ResRef> callback = null, bool isSync = false)
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

    /// <summary>
    /// 获取资源名称
    /// </summary>
    /// <param name="path">资源相对路径</param>
    /// <returns>资源名称</returns>
    private string GetFileName(string path)
    {
        return Path.GetFileName(path);
    }
}

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