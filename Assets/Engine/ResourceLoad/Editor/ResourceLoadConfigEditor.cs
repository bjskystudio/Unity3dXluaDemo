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
            if(EditorUtility.DisplayDialog("是否生成", "是否要生成资源配置？", "确认", "取消"))
            {
                ResourceLoadConfig asset = ScriptableObject.CreateInstance<ResourceLoadConfig>();
                AssetDatabase.CreateAsset(asset, RESOURCE_LOAD_CONFIG);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
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

            if(mTarget.mResourceLoadMode == ResourceLoadMode.eAssetDatabase)
            {
                mTarget.RES_LOCAL_ASSETDATABASE_RELATIVE_PATH = EditorGUILayout.TextField("ASSETDATABASE模式: 相对路径", mTarget.RES_LOCAL_ASSETDATABASE_RELATIVE_PATH);
                EditorGUILayout.LabelField("(实际路径 ： " + Application.dataPath.Replace("Assets", "") + mTarget.RES_LOCAL_ASSETDATABASE_RELATIVE_PATH + ")");
                EditorGUILayout.Space();
            }
            else if(mTarget.mResourceLoadMode == ResourceLoadMode.eAssetbundle)
            {
                mTarget.REPLACE_AB_SHADER = EditorGUILayout.Toggle("替换AB的Shader(编辑器下生效)", mTarget.REPLACE_AB_SHADER);
                EditorGUILayout.Space();

                mTarget.ASSETBUNDLE_SUFFIX_NAME = EditorGUILayout.TextField("assetbundle的后缀名", mTarget.ASSETBUNDLE_SUFFIX_NAME);
                EditorGUILayout.Space();

                mTarget.MANIFEST_NAME = EditorGUILayout.TextField("manifest名字", mTarget.MANIFEST_NAME);
                EditorGUILayout.LabelField("(实际路径 ： " + "包的根路径/" + mTarget.MANIFEST_NAME + ")");
                EditorGUILayout.Space();

                mTarget.SHADER_AB_RELATIVE_PATH = EditorGUILayout.TextField("shader包的相对路径", mTarget.SHADER_AB_RELATIVE_PATH);
                EditorGUILayout.LabelField("(实际路径 ： " + "包的根路径/" + mTarget.SHADER_AB_RELATIVE_PATH + ")");
                EditorGUILayout.Space();

                mTarget.RES_LOCAL_AB_RELATIVE_PATH = EditorGUILayout.TextField("本地相对路径", mTarget.RES_LOCAL_AB_RELATIVE_PATH);
                EditorGUILayout.LabelField("(本地包根路径 ： " + Application.dataPath + "/" + mTarget.RES_LOCAL_AB_RELATIVE_PATH + ")");
                EditorGUILayout.Space();

                mTarget.RES_STREAM_AB_RELATIVE_PATH = EditorGUILayout.TextField("流式相对路径", mTarget.RES_STREAM_AB_RELATIVE_PATH);
                EditorGUILayout.LabelField("(手机流式包根路径 ： " + "Application.streamingAssetsPath/" + mTarget.RES_STREAM_AB_RELATIVE_PATH + ")");
                EditorGUILayout.Space();

                mTarget.RES_PERSISTENT_AB_RELATIVE_PATH = EditorGUILayout.TextField("沙盒相对路径", mTarget.RES_PERSISTENT_AB_RELATIVE_PATH);
                EditorGUILayout.LabelField("(手机沙盒包根路径 ： " + "Application.persistentDataPath/" + mTarget.RES_PERSISTENT_AB_RELATIVE_PATH + ")");
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
