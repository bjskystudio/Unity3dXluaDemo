using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WeatherSystem
{
    public class RainDropScreen 
    {
        public WeatherConfig Config
        {
            get
            {
                return WeatherManager.Instance.WeatherConfig;
            }
        }

        private RainDropScreenFeature mRainDropScreenFeature;

        public void OnInit()
        {
            mRainDropScreenFeature = WeatherManager.Instance.Camera.GetRendererFeature<RainDropScreenFeature>();
            if (mRainDropScreenFeature != null)
            {
                mRainDropScreenFeature.OnInit(Config.mRainDropScreenMat);
                mRainDropScreenFeature.SetActive(true);
            }
        }

        public void OnClear()
        {
            if (mRainDropScreenFeature != null)
            {
                mRainDropScreenFeature.SetActive(false);
            }
        }

        public void OnUpdate()
        {
            if (mRainDropScreenFeature != null)
            {
                mRainDropScreenFeature.mSettings.mRainDropScreenMaterial.SetFloat("_RainDropFlowScale", Config.mRainDropScreenFlowScale);
                mRainDropScreenFeature.mSettings.mRainDropScreenMaterial.SetFloat("_RainDropFlowSize", Config.mRainDropScreenFlowSize);
                mRainDropScreenFeature.mSettings.mRainDropScreenMaterial.SetFloat("_RainDropStaticScale", Config.mRainDropScreenStaticScale);
                mRainDropScreenFeature.mSettings.mRainDropScreenMaterial.SetFloat("_RainDropStaticSize", Config.mRainDropScreenStaticSize);
                mRainDropScreenFeature.mSettings.mRainDropScreenMaterial.SetFloat("_Distortion", Config.mRainDropScreenDistortion);
                mRainDropScreenFeature.mSettings.mRainDropScreenMaterial.SetFloat("_RainDropScreenIntensity", Config.mRainDropScreenIntensity);

                mRainDropScreenFeature.OnUpdate(Config.mRainDropScreenRTScale);
            }
        }
    }

}