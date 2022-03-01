using ResourceLoad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YoukiaEngine;

public class LoadGPUScene : MonoBehaviour
{
    public Button mButton;
    public string mSceneName;
    public string mSceneObjsName;
    private GPUObject mGPUObject;
    public Texture2D mLightMap;


    // Start is called before the first frame update
    void Start()
    {
        mButton.onClick.AddListener(() =>
        {
            ResourceManager.Instance.LoadPrefabInstance(mSceneName, (sceneObj) =>
            {
                LightMapData lightMapData = sceneObj.GetComponent<LightMapData>();
                LightMapData.LightMapInfo mLightMapInfo = lightMapData.mLightMapInfo;
                if (mLightMapInfo.mLightDataCount > 0)
                {
                    LightmapData[] lightmapData = new LightmapData[mLightMapInfo.mLightDataCount];
                    for (int i = 0; i < lightmapData.Length; i++)
                    {
                        lightmapData[i] = new LightmapData();
                        lightmapData[i].lightmapColor = mLightMapInfo.mLightMapColorList[i];
                        if(i < mLightMapInfo.mLightMapDirList.Count)
                        {
                            lightmapData[i].lightmapDir = mLightMapInfo.mLightMapDirList[i];
                        }
                    }
                    LightmapSettings.lightmapsMode = mLightMapInfo.mLightMapMode;
                    LightmapSettings.lightmaps = lightmapData;
                }


                ResourceManager.Instance.LoadScriptableObject(mSceneObjsName, (sceneObjAsset, hRes1) =>
                {
                    GPUSceneAsset asset = sceneObjAsset as GPUSceneAsset;
                    for (int i = 0; i < asset.mObjs.Length; i++)
                    {
                        GPUSceneObjectData data = asset.mObjs[i];
                        if(data.mPrefab == null)
                        {
                            continue;
                        }

                        Renderer[] renderers = data.mPrefab.GetComponentsInChildren<Renderer>();
                        for (int j = 0; j < renderers.Length; j++)
                        {
                            renderers[j].sharedMaterial.shader = Shader.Find(renderers[j].sharedMaterial.shader.name);
                            renderers[j].sharedMaterial.EnableKeyword("LIGHTMAP_ON");
                        }

                        GPUObject gpuObject = new GPUObject();
                        GPUObject.TexInfo texInfo = new GPUObject.TexInfo();
                        texInfo.mName = "unity_Lightmap";
                        texInfo.mTex = lightMapData.mLightMapInfo.mLightMapColorList[data.mLightMapIndex];
                        gpuObject.Init(data.mPrefab, null, texInfo);
                        gpuObject.SetPosition(ref data.mPos);
                        gpuObject.SetScale(ref data.mScale);
                        gpuObject.RegisterVectorProperty("unity_LightmapST");
                        gpuObject.RegisterVectorProperty("unity_LightmapST1");
                        gpuObject.RegisterVectorProperty("_Color_Inst");
                        gpuObject.RegisterVectorProperty("_Color_Inst1");
                        gpuObject.RegisterVectorProperty("_Color_Inst2");
                        gpuObject.SetPropertyVector("unity_LightmapST1", data.mLightScaleOffset);
                        gpuObject.SetPropertyVector("unity_LightmapST", data.mLightScaleOffset);
                        gpuObject.SetPropertyVector("_Color_Inst", new Color(Random.Range(0, 1.0f), Random.Range(0, 1.0f), Random.Range(0, 1.0f)));
                        gpuObject.SetPropertyVector("_Color_Inst1", new Color(1, 1, Random.Range(0, 1.0f)));
                        gpuObject.SetPropertyVector("_Color_Inst2", new Color(1, 0, Random.Range(0, 1.0f)));
                        mGPUObject = gpuObject;
                    }
                });
            });
        });
    }

    // Update is called once per frame
    void Update()
    {
    }
}
