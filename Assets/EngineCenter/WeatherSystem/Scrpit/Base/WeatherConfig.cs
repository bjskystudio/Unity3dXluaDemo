using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeatherSystem
{
    public class WeatherConfig : MonoBehaviour
    {
        [HideInInspector] public WeatherManager.WeatherType mPreviewWeatherType;
        public WeatherManager.WeatherType mWeatherType;

        public GameObject mRenderSceneDepthPrefab;
        public float mRenderDistance = 40;
        public bool mIsBake = true;

        public bool mRainDropSceneSwitch;
        public Material mRainDropSceneMat;
        public Texture2D mRainDropSceneTex;
        public Color mRainDropSceneColor;
        public Vector4 mRainDropSceneLayerStart = new Vector4(0, 5, 10, 20);
        public Vector4 mRainDropSceneLayerRange = new Vector4(5, 5, 10, 10);
        public Vector4 mRainDropSceneLayerSpeed = new Vector4(1, 1, 3, 4);
        public Vector4 mRainDropSceneLayerRotate = new Vector4(3.1f, -1.36f, 4.73f, 0);
        public Vector4 mRainDropSceneLayerAlpha = new Vector4(0.4f, 0.4f, 0.4f, 0.4f);
        public Vector2 mRainDropSceneLayerST1 = new Vector2(5, 0.3f);
        public Vector2 mRainDropSceneLayerST2 = new Vector2(5, 0.3f);
        public Vector2 mRainDropSceneLayerST3 = new Vector2(10, 0.3f);
        public Vector2 mRainDropSceneLayerST4 = new Vector2(10, 0.3f);
        [Range(0, 4)]
        public float mRainDropSceneSpeed = 0.5f;
        [Range(0, 1)]
        public float mRainDropSceneAlpha = 1;
        [Range(0, 1)]
        public float mRainDropSceneIntensity = 1;
        [Range(1, 4)]
        public int mRainDropSceneRTScale = 1;

        public bool mRainDropScreenSwitch;
        public Material mRainDropScreenMat;
        [Range(1, 4)]
        public int mRainDropScreenRTScale = 2;
        [Range(0, 20)]
        public float mRainDropScreenFlowScale = 5;
        [Range(0, 0.5f)]
        public float mRainDropScreenFlowSize = 0.35f;
        [Range(0, 20)]
        public float mRainDropScreenStaticScale = 10;
        [Range(0, 0.5f)]
        public float mRainDropScreenStaticSize = 0.12f;
        [Range(-2.0f, 2.0f)]
        public float mRainDropScreenDistortion = 0.5f;
        [Range(0, 1)]
        public float mRainDropScreenIntensity = 1;

        public bool mRainSplashSwitch;
        public GameObject mRainSplashPrefab;
        [Range(0, 1000)]
        public int mRainSplashMaxNum = 100;
        public Vector3 mRainSplashOffset = Vector3.zero;
        [Range(0, 1)]
        public float mRainSplashMinScale = 0.4f;
        [Range(0, 1)]
        public float mRainSplashMaxScale = 1.0f;
        [Range(0, 1)]
        public float mRainSplashMinAlpha = 0.3f;
        [Range(0, 1)]
        public float mRainSplashMaxAlpha = 1.0f;
        [Range(0, 20)]
        public float mRainSplashMinGapTime = 0.0f;
        [Range(0, 20)]
        public float mRainSplashMaxGapTime = 5.0f;
        [Range(0, 100)]
        public float mRainSplashRadius = 20;
        public int mRainSplashFrameAnimRowNum = 5;
        public int mRainSplashFrameAnimColumnNum = 4;
        [Range(0, 1)]
        public float mRainSplashIntensity = 1;

        public bool mRainRippleSwitch;
        public Texture2D mRainRippleTex;
        public Vector2 mRainRippleDensity = new Vector2(2.0f, 2.0f);
        [Range(0, 3)]
        public float mRainRippleSpeed = 1;
        [Range(0, 5)]
        public float mRainRippleDisturb = 1;
        [Range(0, 1)]
        public float mRainRippleIntensity = 1;

        [HideInInspector]
        public float mSnowIntensity = 1;
        [HideInInspector]
        public float mFogIntensity = 1;
        [HideInInspector]
        public float mSandIntensity = 1;
    }
}
