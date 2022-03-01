using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeatherSystem
{
    public class WeatherPreview : MonoBehaviour
    {
        public WeatherManager.WeatherType mWeatherType = WeatherManager.WeatherType.None;
        private WeatherManager.WeatherType mWeatherTypePre = WeatherManager.WeatherType.None;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(mWeatherTypePre != mWeatherType)
            {
                mWeatherTypePre = mWeatherType;
                switch (mWeatherType)
                {
                    case WeatherManager.WeatherType.None:
                    case WeatherManager.WeatherType.Fog:
                    case WeatherManager.WeatherType.Sand:
                    case WeatherManager.WeatherType.Snow:
                        {
                            WeatherManager.Instance.SwitchWeather(WeatherManager.WeatherType.None);
                        }
                        break;
                    case WeatherManager.WeatherType.Rain:
                        {
                            WeatherManager.Instance.SwitchWeather(WeatherManager.WeatherType.Rain);
                        }
                        break;
                }
            }
        }
    }
}
