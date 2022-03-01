using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeatherSystem
{
    //目标：尽量让每个子效果独立，单独提出来也可以自己运行
    public class Rain : WeatherBase
    {
        Dictionary<Material, int> mSharedMaterialMap = new Dictionary<Material, int>();
        static string RAIN_SHADER_KEY = "_RAIN_ON";

        private RenderSceneDepth mRenderSceneDepth;
        private RainDropScene mRainDropScene;
        private RainDropScreen mRainDropScreen;
        private RainSplash mRainSplash;
        private RainRipple mRainRipple;

        public WeatherConfig Config
        {
            get
            {
                return WeatherManager.Instance.WeatherConfig;
            }
        }

        public override void OnInit(Transform parent)
        {
            GameObject renderSceneHeightObj = GameObject.Instantiate(WeatherManager.Instance.WeatherConfig.mRenderSceneDepthPrefab);
            mRenderSceneDepth = renderSceneHeightObj.GetComponent<RenderSceneDepth>();
            if (mRenderSceneDepth != null)
            {
                mRenderSceneDepth.OnInit(WeatherManager.Instance.Camera, Config.mRenderDistance, Config.mIsBake);
            }

            if(Config.mRainDropSceneSwitch)
            {
                mRainDropScene = new RainDropScene();
                mRainDropScene.OnInit();
            }

            if (Config.mRainDropScreenSwitch)
            {
                mRainDropScreen = new RainDropScreen();
                mRainDropScreen.OnInit();
            }

            if(Config.mRainSplashSwitch)
            {
                mRainSplash = new RainSplash();
                mRainSplash.OnInit(mRenderSceneDepth);
            }

            if(Config.mRainRippleSwitch)
            {
                mRainRipple = new RainRipple();
                mRainRipple.OnInit();
            }
        }

        public override void OnClear()
        {
            foreach (Material mat in mSharedMaterialMap.Keys)
            {
                if (mat.IsKeywordEnabled(RAIN_SHADER_KEY))
                    mat.DisableKeyword(RAIN_SHADER_KEY);
            }
            mSharedMaterialMap.Clear();

            if(mRenderSceneDepth != null)
            {
                mRenderSceneDepth.OnClear();
                mRenderSceneDepth = null;
            }

            if (mRainDropScene != null)
            {
                mRainDropScene.OnClear();
                mRainDropScene = null;
            }

            if (mRainDropScreen != null)
            {
                mRainDropScreen.OnClear();
                mRainDropScreen = null;
            }

            if (mRainSplash != null)
            {
                mRainSplash.OnClear();
                mRainSplash = null;
            }

            if (mRainRipple != null)
            {
                mRainRipple.OnClear();
                mRainRipple = null;
            }
        }

        public override void OnUpdate()
        {
            if (mRainDropScene != null)
            {
                mRainDropScene.OnUpdate();
            }

            if (mRainDropScreen != null)
            {
                mRainDropScreen.OnUpdate();
            }

            if(mRainSplash != null)
            {
                mRainSplash.OnUpdate();
            }

            if (mRainRipple != null)
            {
                mRainRipple.OnUpdate();
            }

        }

        public override void OnRefreshFlag(Dictionary<int, WeatherFlag> flagMap)
        {
            foreach (Material mat in mSharedMaterialMap.Keys)
            {
                if (mat.IsKeywordEnabled(RAIN_SHADER_KEY))
                    mat.DisableKeyword(RAIN_SHADER_KEY);
            }
            mSharedMaterialMap.Clear();

            foreach (WeatherFlag flag in flagMap.Values)
            {
                if (flag.mIsRain)
                {
                    if (flag.mMaterial)
                    {
                        if (mSharedMaterialMap.ContainsKey(flag.mMaterial))
                        {
                            ++mSharedMaterialMap[flag.mMaterial];
                        }
                        else
                        {
                            mSharedMaterialMap.Add(flag.mMaterial, 1);
                            if (!flag.mMaterial.IsKeywordEnabled(RAIN_SHADER_KEY))
                            {
                                flag.mMaterial.EnableKeyword(RAIN_SHADER_KEY);
                            }
                        }
                    }
                }
            }
        }
    }
}
