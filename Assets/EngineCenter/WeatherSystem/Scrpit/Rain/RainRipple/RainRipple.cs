using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeatherSystem
{
    public class RainRipple
    {
        public WeatherConfig Config
        {
            get
            {
                return WeatherManager.Instance.WeatherConfig;
            }
        }

        private static string RAIN_RIPPLE_ON = "_RAIN_RIPPLE_ON";

        public void OnInit()
        {
            Shader.SetGlobalTexture("_RainRippleTex", Config.mRainRippleTex);
            Shader.EnableKeyword(RAIN_RIPPLE_ON);
        }

        public void OnClear()
        {
            Shader.DisableKeyword(RAIN_RIPPLE_ON);
        }

        public void OnUpdate()
        {
            Shader.SetGlobalVector("_RainRippleDensity", Config.mRainRippleDensity);
            Shader.SetGlobalFloat("_RainRippleSpeed", Config.mRainRippleSpeed);
            Shader.SetGlobalFloat("_RainRippleDisturb", Config.mRainRippleDisturb);
            Shader.SetGlobalFloat("_RainRippleIntensity", Config.mRainRippleIntensity);
        }
    }
}