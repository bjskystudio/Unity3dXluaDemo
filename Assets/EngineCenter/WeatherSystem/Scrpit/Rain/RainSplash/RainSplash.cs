using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeatherSystem;
using YoukiaEngine;

namespace WeatherSystem
{
    public class RainSplash
    {
        public class RainSpalshData
        {
            public Vector3 mPos;
            public Vector3 mScale;
            public float mAlpha;
            public Vector4 mColor;
            public GPUObject mGPUObject;
            public int mFrameAnimIndex;
            public int mFrameAnimWidth;
            public int mFrameAnimHeight;
            public bool mNeedRandom;
            public float mGapTime;
            public float mCurGapTime;
            public float mCurFrameTime;
        }

        private List<RainSpalshData> mRainSpalshDataList = new List<RainSpalshData>();

        public WeatherConfig Config
        {
            get
            {
                return WeatherManager.Instance.WeatherConfig;
            }
        }

        private RenderSceneDepth mRenderSceneDepth;

        public void OnInit(RenderSceneDepth renderSceneDepth)
        {
            mRenderSceneDepth = renderSceneDepth;
            for (int i = 0; i < Config.mRainSplashMaxNum; i++)
            {
                RainSpalshData data = new RainSpalshData();
                mRainSpalshDataList.Add(data);
                data.mNeedRandom = false;
                data.mGapTime = Random.Range(Config.mRainSplashMinGapTime, Config.mRainSplashMaxGapTime);
                data.mGPUObject = new GPUObject();
                data.mGPUObject.Init(Config.mRainSplashPrefab);
                data.mGPUObject.RegisterFloatProperty("_Alpha");
                data.mGPUObject.RegisterFloatProperty("_Uvx");
                data.mGPUObject.RegisterFloatProperty("_Uvy");
            }
        }

        public void OnClear()
        {
            for (int i = 0; i < mRainSpalshDataList.Count; i++)
            {
                mRainSpalshDataList[i].mGPUObject.Clear();
            }

            mRainSpalshDataList.Clear();
        }

        private Vector2 GetFrameAnimaUVOffset(int frameIndex, int rowNum, int columnNum)
        {
            int row = frameIndex / columnNum;
            int column = frameIndex % columnNum;
            return new Vector2(column, row);
        }

        private Vector3 GetPos()
        {
            Camera camera = WeatherManager.Instance.Camera;
            float halfTheta = Mathf.Deg2Rad * camera.fieldOfView / 2;
            float halfHeight = camera.nearClipPlane * Mathf.Tan(halfTheta);
            float halfWidth = halfHeight * camera.aspect;
            float zRandom = Random.Range(0, Config.mRainSplashRadius);
            float xRange = halfWidth * zRandom / camera.nearClipPlane;
            float xRandom = Random.Range(-xRange, xRange);
            //出现位置随机再视野范围内，y值再GPU中根据高度图进行修正
            Vector3 posWS = camera.transform.position + camera.transform.right * xRandom + camera.transform.forward * zRandom;
            return posWS;
        }

        // Update is called once per frame
        public void OnUpdate()
        {
            //Texture2D sceneDepthTex = mRenderSceneDepth.GetSceneDepthTex();
            //if(sceneDepthTex == null)
            //{
            //    return;
            //}

            Shader.SetGlobalFloat("_RainSplashIntensity", Config.mRainSplashIntensity);
            Shader.SetGlobalVector("_RainSplashOffset", Config.mRainSplashOffset);
            for (int i = 0; i < mRainSpalshDataList.Count; i++)
            {
                RainSpalshData data = mRainSpalshDataList[i];
                if (data.mNeedRandom)
                {
                    data.mNeedRandom = false;
                    data.mPos = GetPos();
                    data.mScale = new Vector3(1, 1, 1) * Random.Range(Config.mRainSplashMinScale, Config.mRainSplashMaxScale);
                    data.mColor = new Vector4(1, 1, 1, 1);
                    data.mAlpha = Random.Range(Config.mRainSplashMinAlpha, Config.mRainSplashMaxAlpha);
                    data.mFrameAnimIndex = -1;
                    data.mGapTime = Random.Range(Config.mRainSplashMinGapTime, Config.mRainSplashMaxGapTime);

                    data.mGPUObject.SetPosition(ref data.mPos);
                    data.mGPUObject.SetScale(ref data.mScale);
                    Quaternion rotation = Quaternion.Euler(0, 0, 90);
                    data.mGPUObject.SetRotation(ref rotation);
                    data.mGPUObject.SetPropertyFloat("_Alpha", data.mAlpha);
                }

                if (data.mFrameAnimIndex + 1 == Config.mRainSplashFrameAnimRowNum * Config.mRainSplashFrameAnimColumnNum)
                {
                    if (data.mCurGapTime > data.mGapTime)
                    {
                        data.mCurGapTime = 0;
                        data.mFrameAnimIndex = -1;
                        data.mNeedRandom = true;
                    }
                    else
                    {
                        data.mCurGapTime = data.mCurGapTime + Time.deltaTime;
                    }
                }
                else
                {
                    data.mFrameAnimIndex++;
                }

                if(data.mFrameAnimIndex >= 0)
                {
                    Vector2 uvOffset = GetFrameAnimaUVOffset(data.mFrameAnimIndex, Config.mRainSplashFrameAnimRowNum, Config.mRainSplashFrameAnimColumnNum);
                    data.mGPUObject.SetPropertyFloat("_Uvx", uvOffset.x);
                    data.mGPUObject.SetPropertyFloat("_Uvy", uvOffset.y);
                }
            }
        }
    }
}
