using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WeatherSystem
{
    public class RainDropScene
    {
        public WeatherConfig Config
        {
            get
            {
                return WeatherManager.Instance.WeatherConfig;
            }
        }

        private RainDropSceneFeature mRainDropSceneFeature;

        public void OnInit()
        {
            mRainDropSceneFeature = WeatherManager.Instance.Camera.GetRendererFeature<RainDropSceneFeature>();
            if (mRainDropSceneFeature != null)
            {
                mRainDropSceneFeature.OnInit(Config.mRainDropSceneMat);
                mRainDropSceneFeature.SetActive(true);
            }
        }

        public void OnClear()
        {
            if (mRainDropSceneFeature != null)
            {
                mRainDropSceneFeature.SetActive(false);
            }
        }

        public void OnUpdate()
        {
            if(mRainDropSceneFeature != null)
            {
                mRainDropSceneFeature.mSettings.mRainDropSceneMat.SetTexture("_RainDropsTex", Config.mRainDropSceneTex);
                mRainDropSceneFeature.mSettings.mRainDropSceneMat.SetColor("_RainColor", Config.mRainDropSceneColor);

                mRainDropSceneFeature.mSettings.mRainDropSceneMat.SetVector("_RainLayerStart", Config.mRainDropSceneLayerStart);
                mRainDropSceneFeature.mSettings.mRainDropSceneMat.SetVector("_RainLayerRange", Config.mRainDropSceneLayerRange);
                mRainDropSceneFeature.mSettings.mRainDropSceneMat.SetVector("_RainLayerSpeed", Config.mRainDropSceneLayerSpeed);
                mRainDropSceneFeature.mSettings.mRainDropSceneMat.SetVector("_RainLayerRotate", Config.mRainDropSceneLayerRotate);
                mRainDropSceneFeature.mSettings.mRainDropSceneMat.SetVector("_RainLayerAlpha", Config.mRainDropSceneLayerAlpha);

                mRainDropSceneFeature.mSettings.mRainDropSceneMat.SetVector("_RainLayerST1", Config.mRainDropSceneLayerST1);
                mRainDropSceneFeature.mSettings.mRainDropSceneMat.SetVector("_RainLayerST2", Config.mRainDropSceneLayerST2);
                mRainDropSceneFeature.mSettings.mRainDropSceneMat.SetVector("_RainLayerST3", Config.mRainDropSceneLayerST3);
                mRainDropSceneFeature.mSettings.mRainDropSceneMat.SetVector("_RainLayerST4", Config.mRainDropSceneLayerST4);

                mRainDropSceneFeature.mSettings.mRainDropSceneMat.SetFloat("_RainSpeed", Config.mRainDropSceneSpeed);
                mRainDropSceneFeature.mSettings.mRainDropSceneMat.SetFloat("_RainAlpha", Config.mRainDropSceneAlpha);
                mRainDropSceneFeature.mSettings.mRainDropSceneMat.SetFloat("_RainDensity", Config.mRainDropSceneIntensity);

                mRainDropSceneFeature.OnUpdate(Config.mRainDropSceneRTScale);
            }
        }
    }
}
