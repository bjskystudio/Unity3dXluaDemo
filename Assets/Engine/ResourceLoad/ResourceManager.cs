using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

//记录一些点by lh：
//1.资源路径一开始需要带后缀得原因，由于AssetDatabase加载得方式需要，而AB和Resource加载得方式不需要，所以就带上了。
//(
//日志(2021.3.5)：AssetDatabase加载现在不用带后缀了(就算带了也能正确加载)，内部会自动找到正确的后缀名(原理：去找到文件夹下同名文件 并且 类型相同的资源，然后得到后缀名，让AssetDatabase加载)
//日志(2021.3.7):由于会出现同一个AB中有同名资源，但是不同类型的情况，此时我们在AB的情况下也需要带上后缀名，否则无法加载正确。如果没有这种情况，可以不带后缀名
//当下总结：两种方式加载资源都不用带后缀名，除非有同一个AB中有两个同名不同类型的资源，此时请带上后缀名以作区分
//)
//2.资源带路径得原因，由于美术会规划自己得资源路径，要支持AssetDatabase和AB使用统一路径加载资源，所以要带上路径，ab名也带上了路径
//(注意：并不是所有资源都能统一路径，因为AB中可以包含多份资源，比如所有lua文件都可以打包到一个ab中去，此时无法统一路径，这种情况不仅仅有path路径，还会有一个assetName名字， AssetDatabase加载会失效对于这种情况）
//3.使用引用计数来维护资源，所以使用者必须很清楚加载了资源，一定要释放资源，否则资源会一直保存再内存中。对于prefab实例化得资源，我们会加上PrefabAutoDestory，自动维护技术，其余资源自己手动维护
//4.支持同步加载和异步加载，那么就会涉及到异步加载得过程中同步加载同样资源会如何？ 此时同步加载会将异步加载强行打断，立即返回，不会有问题
//5.一般所有shader会打包到一个ab中，这里就假定名字为SHADER_AB_NAME， 注意变体问题！(也就是使用了shader_feature，可以通过几种方法解决 1.将一个激活对应变体得dump材质打包到SHADER_AB_NAME中 2.使用multi_compile 3.使用shader变体列表)
//6.加载图集很特殊，使用ab得时候，必须把ab中得所有资源加载出来，然后找到要加载得sprite.
//7.加载一个ab中得所有资源得时候，使用了*这个符号作为assetName，不是所有资源的得时候，使用了assetName自身。
//8.非AB模式下,也就是在编辑器下使用时,存在外部变量引用的情况时使用 Resources.UnloadUnusedAssets() 和 Resources.UnloadAsset 是卸载不干净的，必须清空引用后在调用接口才能卸载干净。AB模式下就不用担心了，Assetbundle.Unload(true)干干净净，就算有引用也会变成missing
//9.AssetBundle.LoadAssetAsync 方法的传入的资源名字，不区分大小写，并且可以带上后缀进行加载 或者 传入类型加载(这样我们就能很好区分Assetbundle中同名但不同类型资源了)，后缀或者类型错误那么加载失败。正常加载出来的资源的名字是不带后缀名的.
//10.AssetBundle.LoadFromFileAsync 再PC上传入的路径名是不区分大小写的。 Android上区分大小写的，所以一定要保证名字大小写匹配
//11.其实所有资源都可以走一个统一的接口，不过这样返回的对象就只能是UnityEngine.Object, 外部拿到对象后还需要转换一次，并且内部的逻辑也会很混乱，每种资源加载完成都会有不同的处理逻辑，比如Prefab,Material，这样就会写if判断来处理，不优雅.
//而且再AB中有同名不同类型资源的时候，还需增加一个类型type参数,这样内部才能处理这种情况。所以综上所述没有选用统一接口，而是明确的根据资源类型区分了接口，加载什么拿到就是什么，不需要转换，每种资源对应一个HXXX类，
//这样也很方便处理每种资源的不同需求，易扩展。麻烦点就是遇见一个新类型的资源，就需要添加一个HXXX类，其实也还好。
//12.对于循环依赖的问题也解决了，比如A上的一个脚本依赖B，B上的一个脚本依赖A，此时加载不会卡死。

//日志
//2021/6/2 
//1.支持了unity sprite atlas方式
//2021/6/3
//1.支持了加载 sprite single的方式，单图的sprite
//2.废弃了ResourceManager.Release()接口，改用每个加载的回调都返回Hxxx的对象，因为为了支持sprite atlas 和 sprite single，此时已经无法只使用一个Release接口
//传入path的方式定位资源了
//2021/6/10
//血坑，当一个物体在prefab就没有激活，那么实例化出来的时候也重来没有激活过，那么这个物体销毁的时候也就不会调用OnDestroy接口，这有个很严重的问题，因为我们扩展了RawImage
//如果我们使用RawImage上的接口在这种对象上加载了贴图，那么我们就无法在OnDestroy中释放，所以作出修改， 新增了ResRef对象包裹返回HXXX对象，然后我们定义了析构函数用GC来解决这种问题
//2021/6/11
//ResourceManager之前继承Monobehaviour类型的单例，但是这种单例存在一个问题，由于static变量和 OnDestroy释放次序无法确定，比如停止Editor下运行的时候，static变量会提前销毁，这会可能某个
//OnDestory中调用ResourceManager的单例，又会new一个新的对象出来，此时unity会报错：
//Some Objects were not cleaned up when closing the scene.(Dis you spawn Gameobjects from OnDestory?)
//已修复，新增一个更新对象即可

//无法处理的情况：
//如果一个ab中有同类型并且同名的资源，那么此时不保证能否加载正确。实际遇见的情况：同名的 prefab和fbx在一个AB中，都是GameObject类型

namespace ResourceLoad
{
    public enum AssetType
    {
        eNone,
        eAB,
        ePrefab,
        eTexture,
        eAudioClip,
        eAnimationClip,
        eText,
        eShader,
        eSprite,
        eManifest,
        eMaterial,
        eFont,
        eScriptableObject,
        eVideoClip,
    }

    public enum ResourceLoadMode
    {
        eAssetDatabase,
        eAssetbundle,
    }

    public class ResourceManager
    {
        private static ResourceManager mInstance;
        public static ResourceManager Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new ResourceManager();
                }

                mInstance.CheckUpdate();
                return mInstance;
            }
        }

        private static ResourceLoadConfig mConfig;
        public static ResourceLoadConfig Config
        {
            get
            {
                if (mConfig == null)
                {
                    ResourceLoadConfigRef configRef = GameObject.FindObjectOfType<ResourceLoadConfigRef>();
                    if(configRef != null)
                    {
                        mConfig = configRef.mConfig;
                    }
                }

                if (mConfig == null)
                {
                    Debug.LogError("!!!!!!!!!!!!!没再场景中找到ResourceLoadConfig脚本!!!!!!!!!!!!!!!!");
                }

                return mConfig;
            }
        }

        public ResourceLoadMode LoadMode
        {
            get
            {
                if(Config != null)
                {
                    return Config.mResourceLoadMode;
                }
                else
                {
                    return ResourceLoadMode.eAssetDatabase;
                }
            }
        }

        private static ResourceManagerUpdate mUpdater;
        private ResRef mShaderResRef;
        private Dictionary<string, Shader> mShaderMap = new Dictionary<string, Shader>(); //所有shader列表
        public Dictionary<string, HRes> mResMap = new Dictionary<string, HRes>(); //所有资源列表
        public Dictionary<HRes, string> mRecycleBinMap = new Dictionary<HRes, string>(); //回收站列表
        private List<HRes> mRecycleBinRemoveList = new List<HRes>(); //回收站待移除元素
#if UNITY_EDITOR
        public Dictionary<string, DebugInfo> mResDebugInfoMap = new Dictionary<string, DebugInfo>(); //调试信息列表
        public GameObject mDebugRootObj;
#endif
        private List<string> mActiveVariantNameList = new List<string>();
        private List<HRes> mReleaseRequestList = new List<HRes>();
        private object mLocker = new object();

        internal void StartCoroutine(IEnumerator routine)
        {
            if(mUpdater != null)
            {
                mUpdater.StartCoroutine(routine);
            }
        }

        //请先调用该函数初始化资源加载
        public void Init(Action completeAction)
        {
            StartCoroutine(CoInit(completeAction));
        }

        IEnumerator CoInit(Action completeAction)
        {
            if (LoadMode == ResourceLoadMode.eAssetbundle)
            {
                HAssetBundle.InitManifest();
                yield return CoInitShader();

            }
            completeAction?.Invoke();
        }

        IEnumerator CoInitShader()
        {
            AsyncRequest request = LoadShaderAllCoRequest(Config.SHADER_AB_RELATIVE_PATH);
            yield return request;
            mShaderResRef = request.ResRef;
            if (request.Assets != null)
            {
                for (int i = 0; i < request.Assets.Count; i++)
                {
                    if(request.Assets[i] != null)
                    {
                        if (!mShaderMap.ContainsKey(request.Assets[i].name))
                        {
                            mShaderMap[request.Assets[i].name] = request.Assets[i] as Shader;
                        }
                        else
                        {
                            Debug.LogError(string.Format("{0}中有同多个同名{1}", Config.SHADER_AB_RELATIVE_PATH, request.Assets[i].name));
                        }
                    }
                }
            }
        }

        private void CheckUpdate()
        {
            if (!mUpdater)
            {
                mUpdater = GameObject.FindObjectOfType<ResourceManagerUpdate>();
                if (!mUpdater)
                {
                    mUpdater = new GameObject("ResourceManagerUpdate").AddComponent<ResourceManagerUpdate>();
                    mUpdater.gameObject.hideFlags = HideFlags.DontSave;
                }
            }
        }

        #region prefab
        /// <summary>
        /// 回调方式,加载prefab并且实例化
        /// 释放方式：GameObject.Destroy，会自动处理引用计数的减少
        /// </summary>
        /// <param name="path">资源路径</param>
        /// <param name="callback">回调</param>
        /// <param name="isSync">是否是同步</param>
        public void LoadPrefabInstance(string path, Action<GameObject> callback, bool isSync = false)
        {
            string assetName = GetAssetName(path);
            Load<GameObject, HPrefab>(path, assetName, AssetType.ePrefab, callback, isSync);
        }

        /// <summary>
        /// 回调方式,加载AB中的prefab并且实例化(AssetDatabase模式无法使用)
        /// 释放方式：GameObject.Destroy，会自动处理引用计数的减少
        /// </summary>
        /// <param name="path">路径名</param>
        /// <param name="assetName">资源名</param>
        /// <param name="callback">回调</param>
        /// <param name="isSync">是否是同步</param>
        public void LoadABPrefabInstance(string path, string assetName, Action<GameObject> callback, bool isSync = false)
        {
            Load<GameObject, HPrefab>(path, assetName, AssetType.ePrefab, callback, isSync);
        }

        /// <summary>
        /// 协程方式，加载prefab并且实例化
        /// 释放方式：GameObject.Destroy，会自动处理引用计数的减少
        /// </summary>
        /// <param name="path">资源路径</param>
        /// <returns></returns>
        public AsyncRequest LoadPrefabInstanceCoRequest(string path)
        {
            string assetName = GetAssetName(path);
            return LoadCoRequest<GameObject, HPrefab>(path, assetName, AssetType.ePrefab);
        }

        /// <summary>
        /// 回调方式，加载Prefab，但不实例化
        /// 释放方式：调用回调第二个返回对象Release()去释放
        /// </summary>
        /// <param name="path">资源路径</param>
        /// <param name="callback">回调</param>
        /// <param name="isSync">是否同步</param>
        public void LoadPrefab(string path, Action<GameObject, ResRef> callback, bool isSync = false)
        {
            string assetName = GetAssetName(path);
            Load<GameObject, HPrefab>(path, assetName, AssetType.ePrefab, callback, isSync, false, true);
        }

        /// <summary>
        /// 协程方式，加载Prefab，但不实例化
        /// 释放方式：调用AsyncRequest中的HRes对象的Release()去释放
        /// </summary>
        /// <param name="path">资源路径</param>
        public AsyncRequest LoadPrefabCoRequest(string path)
        {
            string assetName = GetAssetName(path);
            return LoadCoRequest<GameObject, HPrefab>(path, assetName, AssetType.ePrefab, false, true);
        }

        #endregion

        #region sprite
        /// <summary>
        /// 回调方式，这种方式是支持unity sprite atlas模式(简写：USA)
        /// 释放方式：调用回调第二个返回对象Release()去释放
        /// </summary>
        /// <param name="path">路径名</param>
        /// <param name="callback">回调</param>
        /// <param name="isSync">是否是同步</param>
        public void LoadSpriteUSA(string path, Action<Sprite, ResRef> callback, bool isSync = false)
        {
            string assetName = GetAssetName(path);
            if (LoadMode == ResourceLoadMode.eAssetbundle)
            {
                path = Path.GetDirectoryName(path).Replace("\\", "/");
            }
            Load<Sprite, HSprite>(path, assetName, AssetType.eSprite, callback, isSync, true);
        }

        /// <summary>
        /// 回调方式，加载Single Sprite, 一个图集中只有一个sprite元素，类似于texture
        /// 释放方式：调用回调第二个返回对象Release()去释放
        /// </summary>
        /// <param name="path">路径名</param>
        /// <param name="callback">回调</param>
        /// <param name="isSync">是否是同步</param>
        public void LoadSpriteSingle(string path, Action<Sprite, ResRef> callback, bool isSync = false)
        {
            string assetName = GetAssetName(path);
            Load<Sprite, HSprite>(path, assetName, AssetType.eSprite, callback, isSync, true);
        }

        /// <summary>
        /// 回调方式，加载Sprite，图集有很多元素
        /// 释放方式：调用回调第二个返回对象Release()去释放
        /// 这种方式是使用texture packer 打包sprite为图集的方式
        /// </summary>
        /// <param name="path">路径名</param>
        /// <param name="callback">回调</param>
        /// <param name="isSync">是否是同步</param>
        public void LoadSprite(string path, Action<Sprite, ResRef> callback, bool isSync = false)
        {
            string assetName = GetAssetName(path);
            path = Path.GetDirectoryName(path).Replace("\\", "/");
            Load<Sprite, HSprite>(path, assetName, AssetType.eSprite, callback, isSync, true);
        }

        /// <summary>
        /// 协程方式，加载Sprite
        /// 释放方式：调用AsyncRequest中的HRes对象的Release()去释放
        /// </summary>
        /// <param name="path">图集路径名<</param>
        /// <param name="assetName">sprite名字</param>
        /// <returns></returns>
        public AsyncRequest LoadSpriteCoRequest(string path)
        {
            string assetName = GetAssetName(path);
            path = Path.GetDirectoryName(path).Replace("\\", "/");
            return LoadCoRequest<Sprite, HSprite>(path, assetName, AssetType.eSprite, true);
        }

        /// <summary>
        /// 回调方式，加载图集中的所有sprite
        /// 释放方式：调用回调第二个返回对象Release()去释放
        /// </summary>
        /// <param name="path">图集路径名</param>
        /// <param name="callback">回调</param>
        /// <param name="isSync">是否是同步</param>
        public void LoadSpriteAll(string path, Action<List<Sprite>, ResRef> callback, bool isSync = false)
        {
            LoadAll<Sprite, HSprite>(path, AssetType.eSprite, callback, isSync);
        }

        /// <summary>
        /// 协程方式，加载图集中的所有sprite
        /// 释放方式：调用AsyncRequest中的HRes对象的Release()去释放
        /// </summary>
        /// <param name="path">图集路径名</param>
        /// <returns></returns>
        public AsyncRequest LoadSpriteAllCoRequest(string path)
        {
            return LoadAllCoRequest<Sprite, HSprite>(path, AssetType.eSprite);
        }
        #endregion

        #region texture
        /// <summary>
        /// 回调方式，加载单图
        /// 释放方式：调用回调第二个返回对象Release()去释放
        /// </summary>
        /// <param name="path">单图路径名</param>
        /// <param name="callback">回调</param>
        /// <param name="isSync">是否是同步</param>
        public void LoadTexture(string path, Action<Texture, ResRef> callback, bool isSync = false)
        {
            string assetName = GetAssetName(path);
            Load<Texture, HTexture>(path, assetName, AssetType.eTexture, callback, isSync);
        }

        /// <summary>
        /// 回调方式，加载AB单图(AssetDatabase模式无法使用)
        /// 释放方式：调用回调第二个返回对象Release()去释放
        /// </summary>
        /// <param name="path">单图路径名</param>
        /// <param name="assetName"></param>
        /// <param name="callback"></param>
        /// <param name="isSync"></param>
        public void LoadABTexture(string path, string assetName, Action<Texture, ResRef> callback, bool isSync = false)
        {
            Load<Texture, HTexture>(path, assetName, AssetType.eTexture, callback, isSync);
        }

        /// <summary>
        /// 协程方式，加载单图
        /// 释放方式：调用AsyncRequest中的HRes对象的Release()去释放
        /// </summary>
        /// <param name="path">单图路径名</param>
        /// <returns></returns>
        public AsyncRequest LoadTextureCoRequest(string path)
        {
            string assetName = GetAssetName(path);
            return LoadCoRequest<Texture, HTexture>(path, assetName, AssetType.eTexture);
        }
        #endregion

        #region AudioClip
        /// <summary>
        /// 回调方式，加载AudioClip
        /// 释放方式：调用回调第二个返回对象Release()去释放
        /// </summary>
        /// <param name="path">路径名</param>
        /// <param name="callback">回调</param>
        /// <param name="isSync">是否是同步</param>
        public void LoadAudioClip(string path, Action<AudioClip, ResRef> callback, bool isSync = false)
        {
            string assetName = GetAssetName(path);
            Load<AudioClip, HAudioCilp>(path, assetName, AssetType.eAudioClip, callback, isSync);
        }

        /// <summary>
        /// 协程方式，加载AudioClip
        /// 释放方式：调用AsyncRequest中的HRes对象的Release()去释放
        /// </summary>
        /// <param name="path">路径名</param>
        /// <returns></returns>
        public AsyncRequest LoadAudioClipCoRequest(string path)
        {
            string assetName = GetAssetName(path);
            return  LoadCoRequest<AudioClip, HAudioCilp>(path, assetName, AssetType.eAudioClip);
        }
        #endregion

        #region AnimationClip
        /// <summary>
        /// 回调方式，加载AnimationClip
        /// 释放方式：调用回调第二个返回对象Release()去释放
        /// </summary>
        /// <param name="path">路径名</param>
        /// <param name="callback">回调</param>
        /// <param name="isSync">是否是同步</param>
        public void LoadAnimationClip(string path, Action<AnimationClip, ResRef> callback, bool isSync = false)
        {
            string assetName = GetAssetName(path);
            Load<AnimationClip, HAnimationClip>(path, assetName, AssetType.eAnimationClip, callback, isSync);
        }

        /// <summary>
        /// 回调方式，加载AB中所有AnimationClip(AssetDatabase模式无法使用)
        /// 释放方式：调用回调第二个返回对象Release()去释放
        /// </summary>
        /// <param name="path">路径名</param>
        /// <param name="callback">回调</param>
        /// <param name="isSync">是否是同步</param>
        public void LoadABAnimationClipAll(string path, Action<List<AnimationClip>, ResRef> callback, bool isSync = false)
        {
            LoadAll<AnimationClip, HAnimationClip>(path, AssetType.eAnimationClip, callback, isSync);
        }

        /// <summary>
        /// 协程方式，加载AnimationClip
        /// 释放方式：调用AsyncRequest中的HRes对象的Release()去释放
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public AsyncRequest LoadAnimationClipCoRequest(string path)
        {
            string assetName = GetAssetName(path);
            return LoadCoRequest<AnimationClip, HAnimationClip>(path, assetName, AssetType.eAnimationClip);
        }
        #endregion

        #region Material
        /// <summary>
        /// 回调方式，加载Material
        /// 释放方式：调用回调第二个返回对象Release()去释放
        /// </summary>
        /// <param name="path">路径名</param>
        /// <param name="callback">回调</param>
        /// <param name="isSync">是否是同步</param>
        public void LoadMaterial(string path, Action<Material, ResRef> callback, bool isSync = false)
        {
            string assetName = GetAssetName(path);
            Load<Material, HMaterial>(path, assetName, AssetType.eMaterial, callback, isSync);
        }

        /// <summary>
        /// 回调方式，加载AB中的Material，支持同一个ab中加载不同名字的material(AssetDatabase模式无法使用)
        /// 释放方式：调用回调第二个返回对象Release()去释放
        /// </summary>
        /// <param name="path">路径名</param>
        /// <param name="callback">回调</param>
        /// <param name="isSync">是否是同步</param>
        public void LoadABMaterial(string path, string assetName, Action<Material, ResRef> callback, bool isSync = false)
        {
            Load<Material, HMaterial>(path, assetName, AssetType.eMaterial, callback, isSync);
        }

        /// <summary>
        /// 协程方式，加载Material
        /// 释放方式：调用AsyncRequest中的HRes对象的Release()去释放
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public AsyncRequest LoadMaterialCoRequest(string path)
        {
            string assetName = GetAssetName(path);
            return LoadCoRequest<Material, HMaterial>(path, assetName, AssetType.eMaterial);
        }
        #endregion

        #region text
        /// <summary>
        /// 回调方式，加载Text
        /// 释放方式：调用回调第二个返回对象Release()去释放
        /// </summary>
        /// <param name="path">路径名</param>
        /// <param name="callback">回调</param>
        /// <param name="isSync">是否同步</param>
        public void LoadText(string path, Action<TextAsset, ResRef> callback, bool isSync = false)
        {
            string assetName = GetAssetName(path);
            Load<TextAsset, HText>(path, assetName, AssetType.eText, callback, isSync);
        }

        /// <summary>
        /// 回调方式，加载AB中的Text, 支持同一个AB中加载不同名字的Text文件(AssetDatabase模式无法使用)
        /// 释放方式：调用回调第二个返回对象Release()去释放
        /// </summary>
        /// <param name="path">路径名</param>
        /// <param name="assetName">资源名</param>
        /// <param name="callback">回调</param>
        /// <param name="isSync">是否同步</param>
        public void LoadABText(string path, string assetName, Action<TextAsset, ResRef> callback, bool isSync = false)
        {
            Load<TextAsset, HText>(path, assetName, AssetType.eText, callback, isSync);
        }

        /// <summary>
        /// 回调方式，加载AB中的所有Text(AssetDatabase模式无法使用)
        /// 释放方式：调用回调第二个返回对象Release()去释放
        /// </summary>
        /// <param name="path">路径名</param>
        /// <param name="callback">回调</param>
        /// <param name="isSync">是否同步</param>
        public void LoadABTextAll(string path, Action<List<TextAsset>, ResRef> callback, bool isSync = false)
        {
            LoadAll<TextAsset, HText>(path, AssetType.eText, callback, isSync);
        }

        /// <summary>
        /// 协程方式，加载Text
        /// 释放方式：调用AsyncRequest中的HRes对象的Release()去释放
        /// </summary>
        /// <param name="path">路径名</param>
        /// <returns></returns>
        public AsyncRequest LoadTextCoRequest(string path)
        {
            string assetName = GetAssetName(path);
            return LoadCoRequest<TextAsset, HText>(path, assetName, AssetType.eText);
        }

        /// <summary>
        /// 协程方式，加载AB中指定名字的Text(AssetDatabase模式无法使用)
        /// 释放方式：调用AsyncRequest中的HRes对象的Release()去释放
        /// </summary>
        /// <param name="path">路径名</param>
        /// <param name="assetName">资源名</param>
        /// <returns></returns>
        public AsyncRequest LoadABTextCoRequest(string path, string assetName)
        {
            return LoadCoRequest<TextAsset, HText>(path, assetName, AssetType.eText);
        }
        #endregion

        #region Shader
        /// 释放方式：调用回调第二个返回对象Release()去释放
        private void LoadShaderAll(string path, Action<List<Shader>, ResRef> callback, bool isSync = false)
        {
            LoadAll<Shader, HShader>(path, AssetType.eShader, callback, isSync);
        }

        /// 释放方式：调用AsyncRequest中的HRes对象的Release()去释放
        private AsyncRequest LoadShaderAllCoRequest(string path)
        {
            return LoadAllCoRequest<Shader, HShader>(path, AssetType.eShader);
        }

        /// <summary>
        /// 获取shader,外部统一走这里
        /// </summary>
        /// <param name="shaderName">shader名字</param>
        /// <returns></returns>
        public Shader GetShader(string shaderName)
        {
            if(LoadMode != ResourceLoadMode.eAssetbundle || Application.isEditor)
            {
                Shader shader = Shader.Find(shaderName);
                if(shader == null)
                {
                    Debug.LogError(string.Format("不存在 {0}", shaderName));
                }

                return shader;
            }
            else
            {
                if (mShaderMap.ContainsKey(shaderName))
                {
                    return mShaderMap[shaderName];
                }
                else
                {
                    Debug.LogError(string.Format("{0} 中不存在 {1} ", mConfig.SHADER_AB_RELATIVE_PATH, shaderName));
                    return null;
                }
            }
        }
        #endregion

        #region Font
        /// <summary>
        /// 回调方式，加载AB中的指定名字的Font
        /// 释放方式：调用回调第二个返回对象Release()去释放
        /// </summary>
        /// <param name="path">路径名</param>
        /// <param name="assetName">资源名</param>
        /// <param name="callback">回调</param>
        /// <param name="isSync">是否同步</param>
        public void LoadABFont(string path, string assetName, Action<Font, ResRef> callback, bool isSync = false)
        {
            Load<Font, HFont>(path, assetName, AssetType.eFont, callback, isSync);
        }

        /// <summary>
        /// 回调方式，加载AB中的指定名字的TMP Font
        /// 释放方式：调用回调第二个返回对象Release()去释放
        /// </summary>
        /// <param name="path">路径名</param>
        /// <param name="assetName">资源名</param>
        /// <param name="callback">回调</param>
        /// <param name="isSync">是否同步</param>
        public void LoadABTMPFont(string path, string assetName, Action<TMP_FontAsset, ResRef> callback, bool isSync = false)
        {
            Load<TMP_FontAsset, HFont>(path, assetName, AssetType.eFont, callback, isSync);
        }

        /// <summary>
        /// 协程方式，加载AB中的指定名字Font
        /// 释放方式：调用AsyncRequest中的HRes对象的Release()去释放
        /// </summary>
        /// <param name="path">路径名</param>
        /// <param name="assetName">资源名</param>
        /// <returns></returns>
        public AsyncRequest LoadABFontCoRequest(string path, string assetName)
        {
            return LoadCoRequest<TMP_FontAsset, HFont>(path, assetName, AssetType.eFont);
        }
        #endregion

        #region ScriptableObject
        /// <summary>
        /// 回调方式，加载ScriptableObject
        /// 释放方式：调用回调第二个返回对象Release()去释放
        /// </summary>
        /// <param name="path">路径名</param>
        /// <param name="callback">回调</param>
        /// <param name="isSync">是否同步</param>
        public void LoadScriptableObject(string path, Action<ScriptableObject, ResRef> callback, bool isSync = false)
        {
            string assetName = GetAssetName(path);
            Load<ScriptableObject, HScriptableObject>(path, assetName, AssetType.eScriptableObject, callback, isSync);
        }
        #endregion

        #region video clip
        /// <summary>
        /// 回调方式，加载Video Clip
        /// 释放方式：调用回调第二个返回对象Release()去释放
        /// </summary>
        /// <param name="path">路径名</param>
        /// <param name="callback">回调</param>
        /// <param name="isSync">是否同步</param>
        public void LoadVideoClip(string path, Action<VideoClip, ResRef> callback, bool isSync = false)
        {
            string assetName = GetAssetName(path);
            Load<VideoClip, HVideoClip>(path, assetName, AssetType.eVideoClip, callback, isSync);
        }
        #endregion


        public void ActivateVariantName(string variantName)
        {
            mActiveVariantNameList.Add(variantName);
        }

        public void RemoveVariantName(string variantName)
        {
            mActiveVariantNameList.Remove(variantName);
        }

        public string RemapVariantName(string abName, bool isDep)
        {
            if (mActiveVariantNameList.Count == 0 || isDep)
            {
                return abName;
            }
            else
            {
                if (HAssetBundle.VariantMap.ContainsKey(abName))
                {
                    for (int i = 0; i < mActiveVariantNameList.Count; i++)
                    {
                        if (HAssetBundle.VariantMap[abName].ContainsKey(mActiveVariantNameList[i]))
                        {
                            return HAssetBundle.VariantMap[abName][mActiveVariantNameList[i]];
                        }
                    }
                }

                return abName;
            }
        }

        private string GetAssetName(string assetPath)
        {
            return Path.GetFileName(assetPath);
        }

        private string GetResName(string assetPath, string assetName)
        {
            assetPath = assetPath.ToLower();
            string resName = string.IsNullOrEmpty(assetName) ? assetPath : string.Format("{0}/{1}", assetPath, assetName);
            return resName.ToLower();
        }

        internal T Get<T>(string assetPath, string assetName, AssetType assetType, bool isAll = false, bool isDep = false) where T : HRes, new()
        {
            string resName = GetResName(assetPath, assetName);
            HRes res = null;
            if (!mResMap.TryGetValue(resName, out res))
            {
                res = new T();
                mResMap.Add(resName, res);
                res.Init(assetPath, assetName, resName, assetType, isAll);
            }

            res.RefCount++;
            RemoveRecycleBin(res);

#if UNITY_EDITOR
            AddDebugInfo(res, resName, assetType);
#endif
            return res as T;
        }

        private void Load<T1, T2>(string assetPath, string assetName, AssetType assetType, Action<T1> callback, bool isSync = false, bool isAll = false, bool isPreload = false) where T1 : UnityEngine.Object where T2 : HRes, new()
        {
            if (string.IsNullOrEmpty(assetPath))
            {
                Debug.LogError("assetPath is null!!!");
                if (callback != null)
                {
                    callback(null);
                }
                return;
            }

            Action<System.Object, ResRef> tCallBack = null;
            if (callback != null)
            {
                tCallBack = (asset, resRef) =>
                {
                    callback(asset as T1);
                };
            }

            T2 res = Get<T2>(assetPath, assetName, assetType, isAll);
            res.StartLoad(isSync, isAll, isPreload, tCallBack);
        }


        private void Load<T1, T2>(string assetPath, string assetName, AssetType assetType, Action<T1, ResRef> callback, bool isSync = false, bool isAll = false, bool isPreload = false) where T1 : UnityEngine.Object where T2 : HRes, new()
        {
            if (string.IsNullOrEmpty(assetPath))
            {
                Debug.LogError("assetPath is null!!!");
                if (callback != null)
                {
                    callback(null, null);
                }
                return;
            }

            Action<System.Object, ResRef> tCallBack = null;
            if (callback != null)
            {
                tCallBack = (asset, resRef) =>
                {
                    callback(asset as T1, resRef);
                };
            }

            T2 res = Get<T2>(assetPath, assetName, assetType, isAll);
            res.StartLoad(isSync, isAll, isPreload, tCallBack);
        }

        private void LoadAll<T1, T2>(string assetPath, AssetType assetType, Action<List<T1>, ResRef> callback, bool isSync = false, bool isPreload = false) where T1 : UnityEngine.Object where T2 : HRes, new()
        {
            if (string.IsNullOrEmpty(assetPath))
            {
                Debug.LogError("assetPath is null!!!");
                if (callback != null)
                {
                    callback(null, null);
                }
                return;
            }

            Action<System.Object, ResRef> tCallBack = null;
            if (callback != null)
            {
                tCallBack = (asset, resRef) =>
                {
                    if (asset != null)
                    {
                        List<System.Object> objectList = (asset as IEnumerable<System.Object>).Cast<System.Object>().ToList();
                        List<T1> assetList = objectList.ConvertAll((item) => { return item as T1; });
                        assetList.RemoveAll((item) => { return item == null; });
                        callback(assetList, resRef);
                    }
                    else
                    {
                        callback(null, null);
                    }
                };
            }

            T2 res = Get<T2>(assetPath, "*", assetType, true);
            res.StartLoad(isSync, true, isPreload, tCallBack);
        }

        private AsyncRequest LoadCoRequest<T1, T2>(string path, string assetName, AssetType assetType, bool isAll = false, bool isPreload = false) where T1 : UnityEngine.Object where T2 : HRes, new()
        {
            AsyncRequest request = new AsyncRequest();
            Load<T1, T2>(path, assetName, assetType, (obj, resRef) =>
            {
                request.isDone = true;
                request.progress = 1;
                request.Asset = obj;
                request.ResRef = resRef;
            }, false, isAll, isPreload);

            return request;
        }

        private AsyncRequest LoadAllCoRequest<T1, T2>(string path, AssetType assetType, bool isPreload = false) where T1 : UnityEngine.Object where T2 : HRes, new()
        {
            AsyncRequest request = new AsyncRequest();
            LoadAll<T1, T2>(path, assetType, (obj, resRef) =>
            {
                request.isDone = true;
                request.progress = 1;
                if (obj != null)
                {
                    request.Assets = obj.ConvertAll((item) => { return item as UnityEngine.Object; });
                    request.ResRef = resRef;
                }
            }, false, isPreload);

            return request;
        }
        
        public void ReleaseAll()
        {
            Action ClearResAction = () =>
            {
                List<HRes> resList = new List<HRes>();
                foreach (var item in mResMap)
                {
                    resList.Add(item.Value);
                }

                //释放所有资源
                for (int i = 0; i < resList.Count; i++)
                {
                    resList[i].ReleaseAll();
                }
            };

#if UNITY_EDITOR
            if (LoadMode == ResourceLoadMode.eAssetDatabase)
            {
                ClearResAction();
                Resources.UnloadUnusedAssets();
                System.GC.Collect();
            }
            else if (LoadMode == ResourceLoadMode.eAssetbundle)
#endif
            {
                //清空缓存shader
                mShaderResRef = null;
                mShaderMap.Clear();
                //停止掉正在加载的AB
                ABRequest.StopAllRequest();
                //停止掉正在加载的AB中的资源
                AssetRequest.StopAllRequest();
                //清空回收站
                foreach(var item in mRecycleBinMap)
                {
                    item.Key.ReleaseReal();
                }
                mRecycleBinMap.Clear();
                //清空
                ClearResAction();
                //释放Manifeset
                HAssetBundle.ReleaseAssetBundleManifest();
            }
        }

        //一般在切景的时候调用
        public void ReleaseInIdle()
        {
            System.GC.Collect();
            Resources.UnloadUnusedAssets();
        }

        internal void AddRecycleBin(HRes res)
        {
            if(!mRecycleBinMap.ContainsKey(res))
            {
                Debug.Log("res recycle bin : " + res.ResName + "/" + Time.realtimeSinceStartup);
                res.RecycleBinPutInTime = Time.realtimeSinceStartup;
                mRecycleBinMap.Add(res, res.ResName);
            }
        }

        internal void RemoveRecycleBin(HRes res)
        {
            if(mRecycleBinMap.ContainsKey(res))
            {
                res.RecycleBinPutInTime = -1;
                mRecycleBinMap.Remove(res);
            }
        }

#if UNITY_EDITOR
        public void AddDebugInfo(HRes res, string resName, AssetType assetType)
        {
            if (Config.DEBUG_MODE)
            {
                if (mDebugRootObj == null)
                {
                    mDebugRootObj = new GameObject();
                    mDebugRootObj.name = "RES_LOAD_DEBUG";
                    mDebugRootObj.AddComponent<DebugResSnapshoot>();
                }

                DebugInfo debugInfo = null;
                if (!mResDebugInfoMap.TryGetValue(resName, out debugInfo))
                {
                    GameObject obj = new GameObject();
                    obj.transform.SetParent(mDebugRootObj.transform);
                    obj.name = res.ResName;
                    if (assetType == AssetType.eAB)
                    {
                        DebugInfoAB debugInfoAB = obj.AddComponent<DebugInfoAB>();
                        debugInfoAB.mHAssetbundle = res as HAssetBundle;
                        debugInfoAB.mObj = obj;
                        mResDebugInfoMap.Add(resName, debugInfoAB);
                    }
                    else
                    {
                        DebugInfoRes debugInfoRes = obj.AddComponent<DebugInfoRes>();
                        debugInfoRes.mRes = res as HRes;
                        debugInfoRes.mObj = obj;
                        mResDebugInfoMap.Add(resName, debugInfoRes);
                    }
                }
            }
        }

        public void RemoveDebugInfo(string name)
        {
            if (Config.DEBUG_MODE)
            {
                DebugInfo debugInfo = null;
                if (Instance.mResDebugInfoMap.TryGetValue(name, out debugInfo))
                {
                    GameObject.Destroy(debugInfo.mObj);
                }
                mResDebugInfoMap.Remove(name);

                List<string> tempList = new List<string>();
                foreach (var item in HAssetBundle.mWhoRefMeMapAll)
                {
                    if (item.Value.ContainsKey(name))
                    {
                        item.Value.Remove(name);
                        if (mResMap.ContainsKey(item.Key))
                        {
                            HAssetBundle hAB = mResMap[item.Key] as HAssetBundle;
                            hAB.AddWhoRefMe(item.Value);
                        }
                    }

                    if (item.Value.Count == 0)
                    {
                        tempList.Add(item.Key);
                    }
                }

                for (int i = 0; i < tempList.Count; i++)
                {
                    HAssetBundle.mWhoRefMeMapAll.Remove(tempList[i]);
                }

            }
        }
#endif

        internal void Update()
        {
            foreach(var item in mRecycleBinMap)
            {
                HRes res = item.Key;
                float time = Time.realtimeSinceStartup - res.RecycleBinPutInTime;
                if(time > Config.RECYBLEBIN_RES_DESTROY_TIME)
                {
                    //真正销毁
                    if(res.RefCount == 0)
                    {
                        res.ReleaseReal();
                        mRecycleBinRemoveList.Add(res);
                    }
                }
            }

            for(int i = 0; i < mRecycleBinRemoveList.Count; i++)
            {
                mRecycleBinMap.Remove(mRecycleBinRemoveList[i]);
            }

            mRecycleBinRemoveList.Clear();

            lock(mLocker)
            {
                if (mReleaseRequestList.Count > 0)
                {
                    for (int i = 0; i < mReleaseRequestList.Count; i++)
                    {
                        mReleaseRequestList[i].Release();
                    }
                    mReleaseRequestList.Clear();
                }
            }
        }

        public void AddReleaseRequest(HRes hRes)
        {
            lock (mLocker)
            {
                mReleaseRequestList.Add(hRes);
            }
        }
    }
}
