using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMapData : MonoBehaviour
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

    public LightMapInfo mLightMapInfo;

    //保存场景信息
    [ContextMenu("储存场景信息")]
    public void SaveSceneInfo()
    {
        SaveLightMap();
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

            if (data.shadowMask != null)
            {
                mLightMapInfo.mLightMapShadowList.Add(data.shadowMask);
            }
        }
    }
}
