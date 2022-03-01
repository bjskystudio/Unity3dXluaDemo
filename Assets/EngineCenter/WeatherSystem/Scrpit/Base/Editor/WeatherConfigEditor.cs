using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace WeatherSystem
{
    [CustomEditor(typeof(WeatherConfig))]
    public class WeatherConfigEditor : Editor
    {
        private bool mInit;
        private GUIStyle mHeaderGS;
        private GUIStyle mHeaderGS1;

        private WeatherConfig mWeatherConfig;

        private SerializedProperty mWeatherTypeSP;

        private SerializedProperty mRenderSceneDepthPrefabSP;
        private SerializedProperty mRenderDistanceSP;
        private SerializedProperty mIsBakeSP;

        private SerializedProperty mRainDropSceneSwitchSP;
        private SerializedProperty mRainDropSceneMatSP;
        private SerializedProperty mRainDropSceneTexSP;
        private SerializedProperty mRainDropSceneColorSP;
        private SerializedProperty mRainDropSceneLayerStartSP;
        private SerializedProperty mRainDropSceneLayerRangeSP;
        private SerializedProperty mRainDropSceneLayerSpeedSP;
        private SerializedProperty mRainDropSceneLayerRotateSP;
        private SerializedProperty mRainDropSceneLayerAlphaSP;
        private SerializedProperty mRainDropSceneLayerST1SP;
        private SerializedProperty mRainDropSceneLayerST2SP;
        private SerializedProperty mRainDropSceneLayerST3SP;
        private SerializedProperty mRainDropSceneLayerST4SP;
        private SerializedProperty mRainDropSceneSpeedSP;
        private SerializedProperty mRainDropSceneAlphaSP;
        private SerializedProperty mRainDropSceneIntensitySP;
        private SerializedProperty mRainDropSceneRTScaleSP;

        private SerializedProperty mRainDropScreenSwitchSP;
        private SerializedProperty mRainDropScreenMatSP;
        private SerializedProperty mRainDropScreenRTScaleSP;
        private SerializedProperty mRainDropScreenFlowScaleSP;
        private SerializedProperty mRainDropScreenFlowSizeSP;
        private SerializedProperty mRainDropScreenStaticScaleSP;
        private SerializedProperty mRainDropScreenStaticSizeSP;
        private SerializedProperty mRainDropScreenDistortionSP;
        private SerializedProperty mRainDropScreenIntensitySP;

        private SerializedProperty mRainSplashSwitchSP;
        private SerializedProperty mRainSplashPrefabSP;
        private SerializedProperty mRainSplashMaxNumSP;
        private SerializedProperty mRainSplashOffsetSP;
        private SerializedProperty mRainSplashMinScaleSP;
        private SerializedProperty mRainSplashMaxScaleSP;
        private SerializedProperty mRainSplashMinAlphaSP;
        private SerializedProperty mRainSplashMaxAlphaSP;
        private SerializedProperty mRainSplashMinGapTimeSP;
        private SerializedProperty mRainSplashMaxGapTimeSP;
        private SerializedProperty mRainSplashRadiusSP;
        private SerializedProperty mRainSplashFrameAnimRowNumSP;
        private SerializedProperty mRainSplashFrameAnimColumnNumSP;
        private SerializedProperty mRainSplashIntensitySP;

        private SerializedProperty mRainRippleSwitchSP;
        private SerializedProperty mRainRippleTexSP;
        private SerializedProperty mRainRippleDensitySP;
        private SerializedProperty mRainRippleSpeedSP;
        private SerializedProperty mRainRippleDisturbSP;
        private SerializedProperty mRainRippleIntensitySP;

        void Awake()
        {
            mWeatherConfig = target as WeatherConfig;

            mWeatherTypeSP = serializedObject.FindProperty("mWeatherType");

            mRenderSceneDepthPrefabSP = serializedObject.FindProperty("mRenderSceneDepthPrefab");
            mRenderDistanceSP = serializedObject.FindProperty("mRenderDistance");
            mIsBakeSP = serializedObject.FindProperty("mIsBake");

            mRainDropSceneSwitchSP = serializedObject.FindProperty("mRainDropSceneSwitch");
            mRainDropSceneMatSP = serializedObject.FindProperty("mRainDropSceneMat");
            mRainDropSceneTexSP = serializedObject.FindProperty("mRainDropSceneTex");
            mRainDropSceneColorSP = serializedObject.FindProperty("mRainDropSceneColor");
            mRainDropSceneLayerStartSP = serializedObject.FindProperty("mRainDropSceneLayerStart");
            mRainDropSceneLayerRangeSP = serializedObject.FindProperty("mRainDropSceneLayerRange");
            mRainDropSceneLayerSpeedSP = serializedObject.FindProperty("mRainDropSceneLayerSpeed");
            mRainDropSceneLayerRotateSP = serializedObject.FindProperty("mRainDropSceneLayerRotate");
            mRainDropSceneLayerAlphaSP = serializedObject.FindProperty("mRainDropSceneLayerAlpha");
            mRainDropSceneLayerST1SP = serializedObject.FindProperty("mRainDropSceneLayerST1");
            mRainDropSceneLayerST2SP = serializedObject.FindProperty("mRainDropSceneLayerST2");
            mRainDropSceneLayerST3SP = serializedObject.FindProperty("mRainDropSceneLayerST3");
            mRainDropSceneLayerST4SP = serializedObject.FindProperty("mRainDropSceneLayerST4");
            mRainDropSceneSpeedSP = serializedObject.FindProperty("mRainDropSceneSpeed");
            mRainDropSceneAlphaSP = serializedObject.FindProperty("mRainDropSceneAlpha");
            mRainDropSceneIntensitySP = serializedObject.FindProperty("mRainDropSceneIntensity");
            mRainDropSceneRTScaleSP = serializedObject.FindProperty("mRainDropSceneRTScale");

            mRainDropScreenSwitchSP = serializedObject.FindProperty("mRainDropScreenSwitch");
            mRainDropScreenMatSP = serializedObject.FindProperty("mRainDropScreenMat");
            mRainDropScreenRTScaleSP = serializedObject.FindProperty("mRainDropScreenRTScale");
            mRainDropScreenFlowScaleSP = serializedObject.FindProperty("mRainDropScreenFlowScale");
            mRainDropScreenFlowSizeSP = serializedObject.FindProperty("mRainDropScreenFlowSize");
            mRainDropScreenStaticScaleSP = serializedObject.FindProperty("mRainDropScreenStaticScale");
            mRainDropScreenStaticSizeSP = serializedObject.FindProperty("mRainDropScreenStaticSize");
            mRainDropScreenDistortionSP = serializedObject.FindProperty("mRainDropScreenDistortion");
            mRainDropScreenIntensitySP = serializedObject.FindProperty("mRainDropScreenIntensity");

            mRainSplashSwitchSP = serializedObject.FindProperty("mRainSplashSwitch");
            mRainSplashPrefabSP = serializedObject.FindProperty("mRainSplashPrefab");
            mRainSplashMaxNumSP = serializedObject.FindProperty("mRainSplashMaxNum");
            mRainSplashOffsetSP = serializedObject.FindProperty("mRainSplashOffset");
            mRainSplashMinScaleSP = serializedObject.FindProperty("mRainSplashMinScale");
            mRainSplashMinScaleSP = serializedObject.FindProperty("mRainSplashMinScale");
            mRainSplashMaxScaleSP = serializedObject.FindProperty("mRainSplashMaxScale");
            mRainSplashMinAlphaSP = serializedObject.FindProperty("mRainSplashMinAlpha");
            mRainSplashMaxAlphaSP = serializedObject.FindProperty("mRainSplashMaxAlpha");
            mRainSplashMinGapTimeSP = serializedObject.FindProperty("mRainSplashMinGapTime");
            mRainSplashMaxGapTimeSP = serializedObject.FindProperty("mRainSplashMaxGapTime");
            mRainSplashRadiusSP = serializedObject.FindProperty("mRainSplashRadius");
            mRainSplashFrameAnimRowNumSP = serializedObject.FindProperty("mRainSplashFrameAnimRowNum");
            mRainSplashFrameAnimColumnNumSP = serializedObject.FindProperty("mRainSplashFrameAnimColumnNum");
            mRainSplashIntensitySP = serializedObject.FindProperty("mRainSplashIntensity");

            mRainRippleSwitchSP = serializedObject.FindProperty("mRainRippleSwitch");
            mRainRippleTexSP = serializedObject.FindProperty("mRainRippleTex");
            mRainRippleDensitySP = serializedObject.FindProperty("mRainRippleDensity");
            mRainRippleSpeedSP = serializedObject.FindProperty("mRainRippleSpeed");
            mRainRippleDisturbSP = serializedObject.FindProperty("mRainRippleDisturb");
            mRainRippleIntensitySP = serializedObject.FindProperty("mRainRippleIntensity");
        }

        public override void OnInspectorGUI()
        {
            if(!mInit)
            {
                mInit = true;
                mHeaderGS = new GUIStyle(GUI.skin.label)
                {
                    fontSize = 12,
                    fontStyle = FontStyle.Bold,
                };

                mHeaderGS1 = new GUIStyle(GUI.skin.label)
                {
                    fontSize = 15,
                    fontStyle = FontStyle.Bold,
                };
            }

            serializedObject.Update();

            EditorGUILayout.Space(20);
            EditorGUILayout.LabelField("配置", mHeaderGS1);
            EditorGUILayout.PropertyField(mWeatherTypeSP, new GUIContent("天气类型"));

            EditorGUILayout.Space(20);
            EditorGUILayout.PropertyField(mRenderSceneDepthPrefabSP, new GUIContent("场景深度prefab"));
            EditorGUILayout.PropertyField(mRenderDistanceSP, new GUIContent("场景深度渲染距离"));
            EditorGUILayout.PropertyField(mIsBakeSP, new GUIContent("场景深度是否烘培(非实时)"));

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("场景雨滴", mHeaderGS);
            EditorGUILayout.PropertyField(mRainDropSceneSwitchSP, new GUIContent("场景雨滴开关(非实时)"));
            EditorGUILayout.PropertyField(mRainDropSceneMatSP, new GUIContent("场景雨滴材质"));
            EditorGUILayout.PropertyField(mRainDropSceneTexSP, new GUIContent("场景雨滴贴图"));
            EditorGUILayout.PropertyField(mRainDropSceneColorSP, new GUIContent("场景雨滴颜色"));
            EditorGUI.BeginChangeCheck();
            mWeatherConfig.mRainDropSceneLayerStart = EditorGUILayout.Vector4Field(new GUIContent("场景雨滴每层开始位置"), mWeatherConfig.mRainDropSceneLayerStart);
            mWeatherConfig.mRainDropSceneLayerRange = EditorGUILayout.Vector4Field(new GUIContent("场景雨滴每层范围"), mWeatherConfig.mRainDropSceneLayerRange);
            mWeatherConfig.mRainDropSceneLayerSpeed = EditorGUILayout.Vector4Field(new GUIContent("场景雨滴每层速度"), mWeatherConfig.mRainDropSceneLayerSpeed);
            mWeatherConfig.mRainDropSceneLayerRotate = EditorGUILayout.Vector4Field(new GUIContent("场景雨滴每层旋转"), mWeatherConfig.mRainDropSceneLayerRotate);
            mWeatherConfig.mRainDropSceneLayerAlpha = EditorGUILayout.Vector4Field(new GUIContent("场景雨滴每层透明度"), mWeatherConfig.mRainDropSceneLayerAlpha);
            mWeatherConfig.mRainDropSceneLayerST1 = EditorGUILayout.Vector4Field(new GUIContent("场景雨滴第一层缩放"), mWeatherConfig.mRainDropSceneLayerST1);
            mWeatherConfig.mRainDropSceneLayerST2 = EditorGUILayout.Vector4Field(new GUIContent("场景雨滴第二层缩放"), mWeatherConfig.mRainDropSceneLayerST2);
            mWeatherConfig.mRainDropSceneLayerST3 = EditorGUILayout.Vector4Field(new GUIContent("场景雨滴第三层缩放"), mWeatherConfig.mRainDropSceneLayerST3);
            mWeatherConfig.mRainDropSceneLayerST4 = EditorGUILayout.Vector4Field(new GUIContent("场景雨滴第四层缩放"), mWeatherConfig.mRainDropSceneLayerST4);
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(mWeatherConfig);
            }
            EditorGUILayout.PropertyField(mRainDropSceneSpeedSP, new GUIContent("场景雨滴速度"));
            EditorGUILayout.PropertyField(mRainDropSceneAlphaSP, new GUIContent("场景雨滴透明度"));
            EditorGUILayout.PropertyField(mRainDropSceneIntensitySP, new GUIContent("场景雨滴强度"));
            EditorGUILayout.PropertyField(mRainDropSceneRTScaleSP, new GUIContent("场景雨滴RT缩放"));

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("屏幕雨滴", mHeaderGS);
            EditorGUILayout.PropertyField(mRainDropScreenSwitchSP, new GUIContent("屏幕雨滴开关(非实时)"));
            EditorGUILayout.PropertyField(mRainDropScreenMatSP, new GUIContent("屏幕雨滴材质"));
            EditorGUILayout.PropertyField(mRainDropScreenRTScaleSP, new GUIContent("屏幕雨滴RT缩放"));
            EditorGUILayout.PropertyField(mRainDropScreenFlowScaleSP, new GUIContent("屏幕动态雨滴雨滴密度"));
            EditorGUILayout.PropertyField(mRainDropScreenFlowSizeSP, new GUIContent("屏幕动态雨滴大小"));
            EditorGUILayout.PropertyField(mRainDropScreenStaticScaleSP, new GUIContent("屏幕静态雨滴雨滴密度"));
            EditorGUILayout.PropertyField(mRainDropScreenStaticSizeSP, new GUIContent("屏幕静态雨滴大小"));
            EditorGUILayout.PropertyField(mRainDropScreenDistortionSP, new GUIContent("屏幕雨滴扰动"));
            EditorGUILayout.PropertyField(mRainDropScreenIntensitySP, new GUIContent("屏幕雨滴强度"));

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("地面水花", mHeaderGS);
            EditorGUILayout.PropertyField(mRainSplashSwitchSP, new GUIContent("雨水花开关(非实时)"));
            EditorGUILayout.PropertyField(mRainSplashPrefabSP, new GUIContent("雨水花prefab"));
            EditorGUILayout.PropertyField(mRainSplashMaxNumSP, new GUIContent("雨水花最大数量"));
            EditorGUILayout.PropertyField(mRainSplashOffsetSP, new GUIContent("雨水花偏移"));
            EditorGUILayout.PropertyField(mRainSplashMinScaleSP, new GUIContent("雨水花最小缩放"));
            EditorGUILayout.PropertyField(mRainSplashMaxScaleSP, new GUIContent("雨水花最大缩放"));
            EditorGUILayout.PropertyField(mRainSplashMinAlphaSP, new GUIContent("雨水花最小透明"));
            EditorGUILayout.PropertyField(mRainSplashMaxAlphaSP, new GUIContent("雨水花最大透明"));
            EditorGUILayout.PropertyField(mRainSplashMinGapTimeSP, new GUIContent("雨水花最小间隙时间"));
            EditorGUILayout.PropertyField(mRainSplashMaxGapTimeSP, new GUIContent("雨水花最大间隙时间"));
            EditorGUILayout.PropertyField(mRainSplashRadiusSP, new GUIContent("雨水花随机半径"));
            EditorGUILayout.PropertyField(mRainSplashFrameAnimRowNumSP, new GUIContent("雨水花动画帧行数"));
            EditorGUILayout.PropertyField(mRainSplashFrameAnimColumnNumSP, new GUIContent("雨水花动画帧列数"));
            EditorGUILayout.PropertyField(mRainSplashIntensitySP, new GUIContent("雨水花强度"));

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("地面水波", mHeaderGS);
            EditorGUILayout.PropertyField(mRainRippleSwitchSP, new GUIContent("地面水波开关(非实时)"));
            EditorGUILayout.PropertyField(mRainRippleTexSP, new GUIContent("地面水波贴图"));
            EditorGUILayout.PropertyField(mRainRippleDensitySP, new GUIContent("地面水波密度"));
            EditorGUILayout.PropertyField(mRainRippleSpeedSP, new GUIContent("地面水波速度"));
            EditorGUILayout.PropertyField(mRainRippleDisturbSP, new GUIContent("地面水波扰动强度"));
            EditorGUILayout.PropertyField(mRainRippleIntensitySP, new GUIContent("地面水波强度"));



            serializedObject.ApplyModifiedProperties();
        }
    }
}
