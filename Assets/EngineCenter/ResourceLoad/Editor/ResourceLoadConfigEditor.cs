using ResourceLoad;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ResourceLoad
{
    [CustomEditor(typeof(ResourceLoadConfig))]
    public class ResourceLoadConfigEditor : Editor
    {
        private ResourceLoadConfig mTarget;
        private static string RESOURCE_LOAD_CONFIG = "Assets/ResourceLoadConfig.asset";

        [MenuItem("Tools/配置/资源加载配置")]
        public static void CreateAsset()
        {
            if (EditorUtility.DisplayDialog("是否生成", "是否要生成资源配置？", "确认", "取消"))
            {
                CreateResLoadCfg();
            }
        }
        /// <summary>
        /// 生成资源加载配置
        /// </summary>
        public static void CreateResLoadCfg()
        {
            ResourceLoadConfig asset = AssetDatabase.LoadAssetAtPath<ResourceLoadConfig>(RESOURCE_LOAD_CONFIG);
            if (asset == null)
            {
                asset = ScriptableObject.CreateInstance<ResourceLoadConfig>();
                asset.mResourceLoadMode = ResourceLoadMode.eAssetDatabase;
                AssetDatabase.CreateAsset(asset, RESOURCE_LOAD_CONFIG);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            //AD
            asset.DEBUG_MODE = true;
            asset.RES_LOCAL_ASSETDATABASE_RELATIVE_PATH = "Assets/Res";
            //AB
            asset.REPLACE_AB_SHADER = true;
            asset.ASSETBUNDLE_SUFFIX_NAME = ".assetbundle";
            asset.MANIFEST_NAME = "StreamingResources";
            asset.SHADER_AB_RELATIVE_PATH = "shader/allshader";
            asset.RES_LOCAL_AB_RELATIVE_PATH = "StreamingAssets/StreamingResources";
            asset.RES_STREAM_AB_RELATIVE_PATH = "StreamingResources";
            asset.RES_PERSISTENT_RELATIVE_PATH = "GameRes";
            asset.RECYBLEBIN_RES_DESTROY_TIME = 3;
        }

        void Awake()
        {
            mTarget = target as ResourceLoadConfig;
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            mTarget.mResourceLoadMode = (ResourceLoadMode)EditorGUILayout.EnumPopup("资源加载类型", mTarget.mResourceLoadMode);
            EditorGUILayout.Space();

            mTarget.DEBUG_MODE = EditorGUILayout.Toggle("调试模式", mTarget.DEBUG_MODE);

            if (mTarget.mResourceLoadMode == ResourceLoadMode.eAssetDatabase)
            {
                mTarget.RES_LOCAL_ASSETDATABASE_RELATIVE_PATH = EditorGUILayout.TextField("ASSETDATABASE模式: 相对路径", mTarget.RES_LOCAL_ASSETDATABASE_RELATIVE_PATH);
                EditorGUILayout.LabelField("(实际路径 ： " + Application.dataPath.Replace("Assets", "") + mTarget.RES_LOCAL_ASSETDATABASE_RELATIVE_PATH + ")");
                EditorGUILayout.Space();
            }
            else if (mTarget.mResourceLoadMode == ResourceLoadMode.eAssetbundle)
            {
                mTarget.REPLACE_AB_SHADER = EditorGUILayout.Toggle("替换AB的Shader(编辑器下生效)", mTarget.REPLACE_AB_SHADER);
                EditorGUILayout.Space();

                mTarget.ASSETBUNDLE_SUFFIX_NAME = EditorGUILayout.TextField("assetbundle的后缀名", mTarget.ASSETBUNDLE_SUFFIX_NAME);
                EditorGUILayout.Space();

                mTarget.MANIFEST_NAME = EditorGUILayout.TextField("manifest名字", mTarget.MANIFEST_NAME);
                EditorGUILayout.LabelField("(实际路径 ： " + "AB根路径/" + mTarget.MANIFEST_NAME + ")");
                EditorGUILayout.Space();

                mTarget.SHADER_AB_RELATIVE_PATH = EditorGUILayout.TextField("shader包的相对路径", mTarget.SHADER_AB_RELATIVE_PATH);
                EditorGUILayout.LabelField("(实际路径 ： " + "AB根路径/" + mTarget.SHADER_AB_RELATIVE_PATH + ")");
                EditorGUILayout.Space();

                mTarget.RES_LOCAL_AB_RELATIVE_PATH = EditorGUILayout.TextField("本地AB相对路径", mTarget.RES_LOCAL_AB_RELATIVE_PATH);
                EditorGUILayout.LabelField("(本地 AB根路径 ： " + Application.dataPath + "/" + mTarget.RES_LOCAL_AB_RELATIVE_PATH + ")");
                EditorGUILayout.Space();

                mTarget.RES_STREAM_AB_RELATIVE_PATH = EditorGUILayout.TextField("流式AB相对路径", mTarget.RES_STREAM_AB_RELATIVE_PATH);
                EditorGUILayout.LabelField("(手机流式 AB根路径 ： " + "Application.streamingAssetsPath/" + mTarget.RES_STREAM_AB_RELATIVE_PATH + ")");
                EditorGUILayout.Space();

                mTarget.RES_PERSISTENT_RELATIVE_PATH = EditorGUILayout.TextField("沙盒资源相对路径", mTarget.RES_PERSISTENT_RELATIVE_PATH);
                EditorGUILayout.LabelField("(手机沙盒 资源根路径 ： " + "Application.persistentDataPath/" + mTarget.RES_PERSISTENT_RELATIVE_PATH + ")");
                EditorGUILayout.Space();

                mTarget.RES_PERSISTENT_AB_RELATIVE_PATH = EditorGUILayout.TextField("沙盒AB相对路径(可以为空,AB直接在沙盒资源目录下)", mTarget.RES_PERSISTENT_AB_RELATIVE_PATH);
                if(string.IsNullOrEmpty(mTarget.RES_PERSISTENT_RELATIVE_PATH))
                {
                    EditorGUILayout.LabelField("(手机沙盒 AB根路径 ： " + "Application.persistentDataPath/" + mTarget.RES_PERSISTENT_AB_RELATIVE_PATH + ")");
                }
                else
                {
                    if (string.IsNullOrEmpty(mTarget.RES_PERSISTENT_AB_RELATIVE_PATH))
                    {
                        EditorGUILayout.LabelField("(手机沙盒 AB根路径 ： " + "Application.persistentDataPath/" + mTarget.RES_PERSISTENT_RELATIVE_PATH + ")");
                    }
                    else
                    {
                        EditorGUILayout.LabelField("(手机沙盒 AB根路径 ： " + "Application.persistentDataPath/" + mTarget.RES_PERSISTENT_RELATIVE_PATH + "/" + mTarget.RES_PERSISTENT_AB_RELATIVE_PATH + ")");
                    }
                }          
                EditorGUILayout.Space();

                mTarget.RECYBLEBIN_RES_DESTROY_TIME = EditorGUILayout.FloatField("回收站资源多久销毁(单位:秒)", mTarget.RECYBLEBIN_RES_DESTROY_TIME);
            }

            EditorGUILayout.Space();

            serializedObject.ApplyModifiedProperties();
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(mTarget);
            }
        }
    }
}
