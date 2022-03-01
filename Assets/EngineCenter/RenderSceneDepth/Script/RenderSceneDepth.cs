using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


[ExecuteInEditMode]
public class RenderSceneDepth : MonoBehaviour
{
    public Camera mMainCamera;
    public float mRenderDistance;
    public bool mIsBake; //开启这个之后不会每帧渲染高度，只会渲染一次
    public int mRendererDataIndex = -1;
    private Camera mRenderSceneDepthCamera;
    private RenderSceneDepthPassFeature mRenderSceneDepthPassFeature;
    private Texture2D mSceneDepthTex;

    public void OnInit(Camera camera, float renderDistance, bool isBake)
    {
        mMainCamera = camera;
        mRenderDistance = renderDistance;
        mIsBake = isBake;
        mRenderSceneDepthPassFeature = mRenderSceneDepthCamera.GetRendererFeature<RenderSceneDepthPassFeature>();
        mRenderSceneDepthPassFeature.OnInit();
        mRenderSceneDepthPassFeature.SetActive(true);
    }

    public void OnClear()
    {
        DestroyImmediate(gameObject);
        if(mSceneDepthTex != null)
        {
            DestroyImmediate(mSceneDepthTex);
        }

        if(mRenderSceneDepthPassFeature != null)
        {
            mRenderSceneDepthPassFeature.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        mRenderSceneDepthCamera = GetComponent<Camera>();
        if(mRendererDataIndex == -1)
        {
            Debug.LogError("请指定渲染场景高度的RenderData下标!!!请首先在UniversalRenderPipelineAsset 中添加RendererData到RendererList中，然后指定下标");
        }
        else
        {
            CAdditionalCameraData cameraData = CameraExtensions.GetAdditionalCameraData(mRenderSceneDepthCamera);
            cameraData.SetRenderer(mRendererDataIndex);
        }
    }

    void GetMaxMinPoint(List<Vector3> pointList, out Vector3 minPoint, out Vector3 maxPoint) 
    {
        minPoint = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        maxPoint = new Vector3(float.MinValue, float.MinValue, float.MinValue);
        for (int i = 0; i < pointList.Count; i++)
        {
            minPoint = Vector3.Min(minPoint, pointList[i]);
            maxPoint = Vector3.Max(maxPoint, pointList[i]);
        }
    }

    void UpdateCamera()
    {
        if (mMainCamera == null)
        {
            Debug.LogError("渲染场景深度，请先指定渲染场景得摄像机");
            return;
        }

        Vector3 topLeftDir;
        Vector3 topRightDir;
        Vector3 bottomLeftDir;
        Vector3 bottomRightDir;
        mMainCamera.GetViewDir(out topLeftDir, out topRightDir, out bottomLeftDir, out bottomRightDir, mRenderDistance);
        List<Vector3> pointList = new List<Vector3>();
        pointList.Add(mMainCamera.transform.position);
        pointList.Add(pointList[0] + topLeftDir);
        pointList.Add(pointList[0] + topRightDir);
        pointList.Add(pointList[0] + bottomLeftDir);
        pointList.Add(pointList[0] + bottomRightDir);

        Vector3 minPoint;
        Vector3 maxPoint;
        GetMaxMinPoint(pointList, out minPoint, out maxPoint);

        mRenderSceneDepthCamera.orthographicSize = Mathf.Max(mRenderDistance / 2, (maxPoint.x - minPoint.x) / 2 * (1 / mRenderSceneDepthCamera.aspect));
        mRenderSceneDepthCamera.farClipPlane = maxPoint.y - minPoint.y;
        mRenderSceneDepthCamera.transform.position = new Vector3(minPoint.x + (maxPoint.x - minPoint.x) / 2, minPoint.y + (maxPoint.y - minPoint.y) / 2 + mRenderSceneDepthCamera.farClipPlane / 2, minPoint.z + mRenderSceneDepthCamera.orthographicSize);
    }

    private static Texture2D RenderTextureToTex2D(RenderTexture rt)
    {
        Texture2D tex = new Texture2D(rt.width, rt.height, TextureFormat.R16, false);
        RenderTexture rtOld = RenderTexture.active;

        RenderTexture.active = rt;
        tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        tex.Apply();

        RenderTexture.active = rtOld;
        return tex;

    }

    public Texture2D GetSceneDepthTex()
    {
        if(mRenderSceneDepthPassFeature == null)
        {
            return null;
        }

        if (mRenderSceneDepthPassFeature.IsRenderSceneDepthOK())
        {
            RenderTexture rt = mRenderSceneDepthPassFeature.GetSceneDepthTexture();
            if (mIsBake)
            {
                if(mSceneDepthTex == null)
                {
                    mRenderSceneDepthPassFeature.SetActive(false);
                    mSceneDepthTex = RenderTextureToTex2D(rt);
                }
            }
            else
            {
                if (mSceneDepthTex != null)
                {
                    DestroyImmediate(mSceneDepthTex);
                }

                mSceneDepthTex = RenderTextureToTex2D(rt);
            }

            return mSceneDepthTex;
        }
        else
        {
            return null;
        }
    }

    //根据sceneDepth的信息，获取新的高度信息
    public float GetDepth(Texture2D sceneDepthTex, Vector3 posWS)
    {
        Vector3 posVS = mRenderSceneDepthCamera.worldToCameraMatrix.MultiplyPoint3x4(posWS);
        Vector3 posCS = mRenderSceneDepthCamera.projectionMatrix.MultiplyPoint(posVS);
        Vector2 posSS = (new Vector2(posCS.x, posCS.y) * 0.5f + new Vector2(0.5f, 0.5f)) * new Vector2(sceneDepthTex.width, sceneDepthTex.height);
        Color sceneHeightColor = sceneDepthTex.GetPixel((int)posSS.x, (int)posSS.y);
        float depth = sceneHeightColor.r;
        if (SystemInfo.usesReversedZBuffer)
        {
            depth = 1 - depth;
        }

        //获取深度后，转换回世界空间下，获取高度.这里是正交相机，所以求解方式和透视摄像机不一样
        Vector4 posNewVS = new Vector4(posVS.x, posVS.y, -Mathf.Lerp(mRenderSceneDepthCamera.nearClipPlane, mRenderSceneDepthCamera.farClipPlane, depth), 1);
        Vector3 posNewWS = mRenderSceneDepthCamera.cameraToWorldMatrix.MultiplyPoint(posNewVS);
        return posNewWS.y;
    }

    public Vector3 GetPos(Texture2D sceneDepthTex, Vector3 posWS)
    {
        float height = GetDepth(sceneDepthTex, posWS);
        return new Vector3(posWS.x, height, posWS.z);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCamera();
    }
}
