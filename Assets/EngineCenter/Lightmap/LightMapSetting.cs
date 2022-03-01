using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class LightMapSetting : MonoBehaviour
{
    //LightMap信息  
    [System.Serializable]
    public class RendererInfo
    {
        public Renderer mRenderer;
        public int mLightMapIndex;
        public Vector4 mLightMapOffsetScale;
    }

    [System.Serializable]
    public class LightMapInfo
    {
        public LightMapInfo()
        {
            mRendererList = new List<RendererInfo>();
            mLightMapColorList = new List<Texture2D>();
            mLightMapDirList = new List<Texture2D>();
        }

        public int mLightDataCount;
        public List<RendererInfo> mRendererList;
        public List<Texture2D> mLightMapColorList;
        public List<Texture2D> mLightMapDirList;
        public List<Texture2D> mLightMapShadowList;
        public LightmapsMode mLightMapMode;
        public Terrain mTerrain;
        public RendererInfo mTerrainRendererInfo;
    }

    //场景中的Fog信息  
    [System.Serializable]
    public class FogInfo
    {
        public bool fog;
        public FogMode fogMode;
        public Color fogColor;
        public float fogStartDistance;
        public float fogEndDistance;
        public float fogDensity;
    }

    public FogInfo mFogInfo;
    public LightMapInfo mLightMapInfo;
    public List<GameObject> mStaticBatchList = new List<GameObject>();

    public void Start()
    {
        SetSceneInfo();
    }

    //设置光照信息  
    [ContextMenu("设置场景信息")]
    public void SetSceneInfo()
    {
        LoadLightMap();
        LoadFog();

        if(Application.isPlaying)
        {
            //避免在editor预览时,合并了场景的mesh
            for(int i = 0; i < mStaticBatchList.Count; i++)
            {
                StaticBatchingUtility.Combine(mStaticBatchList[i]);
            }
        }
    }

    //保存场景信息
    [ContextMenu("储存场景信息")]
    public void SaveSceneInfo()
    {
        SaveLightMap();
        SaveFog();
    }

    public void SaveFog()
    {
        mFogInfo = new FogInfo();
        mFogInfo.fog = RenderSettings.fog;
        mFogInfo.fogMode = RenderSettings.fogMode;
        mFogInfo.fogColor = RenderSettings.fogColor;
        mFogInfo.fogStartDistance = RenderSettings.fogStartDistance;
        mFogInfo.fogEndDistance = RenderSettings.fogEndDistance;
    }

    public void SaveLightMap()
    {
        mLightMapInfo = new LightMapInfo();
        mLightMapInfo.mLightMapMode = LightmapSettings.lightmapsMode;
        mLightMapInfo.mLightDataCount = LightmapSettings.lightmaps.Length;

        //缓存lightmap的信息
        for (int i = 0; i < LightmapSettings.lightmaps.Length; i++)
        {
            LightmapData data = LightmapSettings.lightmaps[i];
            if (data.lightmapColor != null)
            {
                mLightMapInfo.mLightMapColorList.Add(data.lightmapColor);
            }

            if (data.lightmapDir != null)
            {
                mLightMapInfo.mLightMapDirList.Add(data.lightmapDir);
            }

            if(data.shadowMask != null)
            {
                mLightMapInfo.mLightMapShadowList.Add(data.shadowMask);
            }
        }

        //缓存renderer的信息
        var renderers = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer r in renderers)
        {
            if (r.lightmapIndex != -1)
            {
                RendererInfo info = new RendererInfo();
                info.mRenderer = r;
                info.mLightMapOffsetScale = r.lightmapScaleOffset;
                info.mLightMapIndex = r.lightmapIndex;
                mLightMapInfo.mRendererList.Add(info);
            }
        }

        //设置地形
        mLightMapInfo.mTerrain = GetComponentInChildren<Terrain>();
        if (mLightMapInfo.mTerrain != null)
        {
            mLightMapInfo.mTerrainRendererInfo = new RendererInfo();
            mLightMapInfo.mTerrainRendererInfo.mLightMapOffsetScale = mLightMapInfo.mTerrain.lightmapScaleOffset;
            mLightMapInfo.mTerrainRendererInfo.mLightMapIndex = mLightMapInfo.mTerrain.lightmapIndex;
        }
    }

    public void LoadFog()
    {
        RenderSettings.fog = mFogInfo.fog;
        RenderSettings.fogMode = mFogInfo.fogMode;
        RenderSettings.fogColor = mFogInfo.fogColor;
        RenderSettings.fogStartDistance = mFogInfo.fogStartDistance;
        RenderSettings.fogEndDistance = mFogInfo.fogEndDistance;
        RenderSettings.fogDensity = mFogInfo.fogDensity;
    }

    public void LoadLightMap()
    {
        if(mLightMapInfo == null)
        {
            return;
        }
        
        //还原lightmap信息
        if(mLightMapInfo.mLightDataCount > 0)
        {
            LightmapData[] lightmapData = new LightmapData[mLightMapInfo.mLightDataCount];
            for (int i = 0; i < lightmapData.Length; i++)
            {
                lightmapData[i] = new LightmapData();
                lightmapData[i].lightmapColor = mLightMapInfo.mLightMapColorList[i];
                if(mLightMapInfo.mLightMapDirList != null && mLightMapInfo.mLightMapDirList.Count > 0)
                {
                    lightmapData[i].lightmapDir = mLightMapInfo.mLightMapDirList[i];
                }
            }
            LightmapSettings.lightmapsMode = mLightMapInfo.mLightMapMode;
            LightmapSettings.lightmaps = lightmapData;
        }

        //还原terrain
        if (mLightMapInfo.mTerrain != null && mLightMapInfo.mTerrainRendererInfo != null)
        {
            mLightMapInfo.mTerrain.lightmapScaleOffset = mLightMapInfo.mTerrainRendererInfo.mLightMapOffsetScale;
            mLightMapInfo.mTerrain.lightmapIndex = mLightMapInfo.mTerrainRendererInfo.mLightMapIndex;
        }

        //还原场景renderer
        foreach (var item in mLightMapInfo.mRendererList)
        {
            if(item != null && item.mRenderer != null)
            {
                item.mRenderer.lightmapIndex = item.mLightMapIndex;
                item.mRenderer.lightmapScaleOffset = item.mLightMapOffsetScale;
            }
        }
    }
}