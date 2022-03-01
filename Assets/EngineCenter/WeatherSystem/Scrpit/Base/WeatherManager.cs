using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeatherSystem
{
    public class WeatherManager
    {
        public enum Quality
        {
            High = 0,
            Medium,
            Low
        }
        public enum WeatherType
        {
            None = 0,
            Rain,
            Snow,
            Fog,
            Sand
        }

        private WeatherConfig mWeatherConfig;
        public WeatherConfig WeatherConfig
        {
            get
            {
                if (mWeatherConfig == null)
                {
                    mWeatherConfig = GameObject.FindObjectOfType<WeatherConfig>();
                }

                if(mWeatherConfig == null)
                {
                    Debug.LogError("没有找到WeatherConfig组件, 请确保场景上有WeatherConfig的组件!");
                }

                return mWeatherConfig;
            }
        }

        public Camera Camera
        {
            get
            {
                Camera camera = null;
                if (CameraManager.Get() != null)
                {
                    camera = CameraManager.Get().maincamera;
                    if (camera != null)
                    {
                        return camera;
                    }
                }

                camera = Camera.main;
                if(camera != null)
                {
                    return camera;
                }

                if(camera == null)
                {
                    Debug.LogError("错误，CameraManager 没有获取到camera, 也没有标记为MainCamera得摄像机，请确认！！！");
                }

                return null;
            }
        }

        private Action<WeatherType> mSwitchWeatherCallback;
        private WeatherType mPreWeatherType = WeatherType.None;
        private WeatherType mCurWeatherType = WeatherType.None;
        private WeatherBase mCurWeather;
        Dictionary<int, WeatherFlag> mWeatherFlagMap = new Dictionary<int, WeatherFlag>();

        private static WeatherManager mInstance = null;
        public static WeatherManager Instance
        {
            get
            {
                if (mInstance == null)
                    mInstance = new WeatherManager();
                return mInstance;
            }
        }

        public void Destroy()
        {
            if (mCurWeather != null)
            {
                mCurWeather.OnClear();
                mCurWeather = null;
            }
            mPreWeatherType = WeatherType.None;
            mCurWeatherType = WeatherType.None;
            mWeatherFlagMap.Clear();
        }

        public void Update()
        {
            if (mCurWeather != null)
            {
                mCurWeather.OnUpdate();
            }
        }

        public void AddFlag(WeatherFlag flag)
        {
            if (!mWeatherFlagMap.ContainsKey(flag.GetInstanceID()))
                mWeatherFlagMap.Add(flag.GetInstanceID(), flag);

            if (mCurWeather != null)
            {
                mCurWeather.OnRefreshFlag(mWeatherFlagMap);
            }
        }

        public void RemoveFlag(WeatherFlag flag)
        {
            if (mWeatherFlagMap.ContainsKey(flag.GetInstanceID()))
                mWeatherFlagMap.Remove(flag.GetInstanceID());

            if (mCurWeather != null)
            {
                mCurWeather.OnRefreshFlag(mWeatherFlagMap);
            }
        }

        public void UpdateFlag()
        {
            if (mCurWeather != null)
            {
                mCurWeather.OnRefreshFlag(mWeatherFlagMap);
            }
        }

        public void SetQuality(Quality quality)
        {
            if(mCurWeather != null)
            {
                switch(quality)
                {
                    case Quality.Low:
                        {
                            mCurWeather.OnLow();
                        }
                        break;
                    case Quality.Medium:
                        {
                            mCurWeather.OnMedium();
                        }
                        break;
                    case Quality.High:
                        {
                            mCurWeather.OnHigh();
                        }
                        break;
                }
            }
        }

        public void SwitchWeather(WeatherType weatherType)
        {
            if(mCurWeatherType == weatherType)
            {
                return;
            }

            if (mCurWeather != null)
            {
                mCurWeather.OnClear();
                mCurWeather = null;
            }

            mPreWeatherType = mCurWeatherType;
            mCurWeatherType = weatherType;
            switch (mCurWeatherType)
            {
                case WeatherType.None:
                    break;
                case WeatherType.Rain:
                    {
                        mCurWeather = new Rain();
                        mCurWeather.OnInit(WeatherConfig.transform);
                        mCurWeather.OnRefreshFlag(mWeatherFlagMap);
                    }
                    break;
            }

            mSwitchWeatherCallback?.Invoke(mPreWeatherType);
        }
    }
}