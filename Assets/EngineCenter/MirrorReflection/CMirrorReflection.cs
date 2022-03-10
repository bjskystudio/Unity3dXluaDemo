using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using RenderPipeline = UnityEngine.Rendering.RenderPipelineManager;

[ExecuteInEditMode]
public class CMirrorReflection : MonoBehaviour
{
    public int mTextureSize = 512;
    public float mClipPlaneOffset = 0.07f;
    public LayerMask mReflectLayers = 0;
    private Camera mReflectionCamera;
    private RenderTexture mReflectionTexture = null;
    private int mOldReflectionTextureSize = 0;
    Renderer mRenderer = null;
    public Camera mTargetCamera;
    public Camera TargetCamera
    {
        get
        {
            if (mTargetCamera != null)
            {
                return mTargetCamera;
            }
            else
            {
                CameraManager cameraManager = CameraManager.Get();
                if (cameraManager != null)
                {
                    return cameraManager.maincamera;
                }

                return null;
            }
        }
    }

    //反射摄像机拷贝的是主摄像机，此时会使用主摄像机的渲染流程，导致会渲染很多无关的东西，比如深度图，阴影图。所以我们自定了渲染流程，通过将管线Asset上增加一个MirrorReflectionRendererData后
    //然后在这里指定它的下标。我们让反射摄像机使用新的渲染流程
    public int mRendererDataIndex = 2;

    void OnEnable()
    {
        RenderPipelineManager.beginCameraRendering += BeginCameraRendering;
    }

    void OnDisable()
    {
        RenderPipelineManager.beginCameraRendering -= BeginCameraRendering;

        if (mReflectionTexture)
        {
            mReflectionCamera.targetTexture = null;
            DestroyImmediate(mReflectionTexture);
            mReflectionTexture = null;
        }
        if (mReflectionCamera)
        {
            DestroyImmediate(mReflectionCamera.gameObject);
            mReflectionCamera = null;
        }
    }

    void BeginCameraRendering(ScriptableRenderContext context, Camera camera)
    {
        if(TargetCamera != camera)
        {
            return;
        }

        if(TargetCamera == null)
        {
            return;
        }

        if (mRenderer == null)
            mRenderer = GetComponent<Renderer>();

        if (!enabled || !mRenderer || !mRenderer.sharedMaterial || !mRenderer.enabled)
            return;

        Camera cam = TargetCamera;
        Camera reflectionCamera;
        CreateMirrorObjects(cam, out reflectionCamera);
        UpdateCameraModes(cam, reflectionCamera);

        Vector3 pos = transform.position;
        Vector3 normal = transform.InverseTransformDirection(transform.up);
        float d = -Vector3.Dot(normal, pos) - mClipPlaneOffset;
        Vector4 reflectionPlane = new Vector4(normal.x, normal.y, normal.z, d);

        Matrix4x4 reflection = Matrix4x4.zero;
        CalculateReflectionMatrix(ref reflection, reflectionPlane);
        Vector3 oldpos = cam.transform.position;
        Vector3 newpos = reflection.MultiplyPoint(oldpos);
        reflectionCamera.worldToCameraMatrix = cam.worldToCameraMatrix * reflection;

        Vector4 clipPlane = CameraSpacePlane(reflectionCamera, pos, normal, 1.0f);
        Matrix4x4 projection = cam.CalculateObliqueMatrix(clipPlane);
        reflectionCamera.projectionMatrix = projection;

        reflectionCamera.cullingMask = /*~(1 << 4) & */mReflectLayers.value;
        reflectionCamera.targetTexture = mReflectionTexture;
        GL.invertCulling = true;
        reflectionCamera.transform.position = newpos;
        Vector3 euler = cam.transform.eulerAngles;
        reflectionCamera.transform.eulerAngles = new Vector3(0, euler.y, euler.z);

        if (CNPRPipeline.asset != null)
            CNPRPipeline.RenderSingleCamera(context, reflectionCamera);
        else if (UniversalRenderPipeline.asset != null)
            UniversalRenderPipeline.RenderSingleCamera(context, reflectionCamera);

        reflectionCamera.transform.position = oldpos;
        GL.invertCulling = false;
    }

    private void UpdateCameraModes(Camera src, Camera dest)
    {
        if (dest == null)
            return;

        dest.clearFlags = src.clearFlags;
        dest.backgroundColor = src.backgroundColor;
        if (src.clearFlags == CameraClearFlags.Skybox)
        {
            Skybox sky = src.GetComponent(typeof(Skybox)) as Skybox;
            Skybox mysky = dest.GetComponent(typeof(Skybox)) as Skybox;
            if (!sky || !sky.material)
            {
                mysky.enabled = false;
            }
            else
            {
                mysky.enabled = true;
                mysky.material = sky.material;
            }
        }

        dest.farClipPlane = src.farClipPlane;
        dest.nearClipPlane = src.nearClipPlane;
        dest.orthographic = src.orthographic;
        dest.fieldOfView = src.fieldOfView;
        dest.aspect = src.aspect;
        dest.orthographicSize = src.orthographicSize;

        if(mRendererDataIndex != -1)
        {
            CAdditionalCameraData  cameraData = CameraExtensions.GetAdditionalCameraData(dest);
            cameraData.SetRenderer(mRendererDataIndex);
        }
        else
        {
            //YoukiaCore.Log.Log.Debug("请指定渲染反射的RendererData下标!!!请首先在UniversalRenderPipelineAsset 中添加RendererData到RendererList中，然后指定下标. 否则将会渲染很多无关东西，导致性能下降！！！");
        }
    }

    private void CreateMirrorObjects(Camera currentCamera, out Camera reflectionCamera)
    {
        if (!mReflectionTexture || mOldReflectionTextureSize != mTextureSize)
        {
            if (mReflectionTexture)
                DestroyImmediate(mReflectionTexture);
            mReflectionTexture = new RenderTexture(mTextureSize, mTextureSize, 16, RenderTextureFormat.ARGB32, 6);
            mReflectionTexture.useMipMap = true;
            mReflectionTexture.filterMode = FilterMode.Trilinear;
            mReflectionTexture.name = "__MirrorReflection" + GetInstanceID();
            mReflectionTexture.isPowerOfTwo = true;
            mReflectionTexture.hideFlags = HideFlags.DontSave;
            mOldReflectionTextureSize = mTextureSize;

            Material[] materials = mRenderer.sharedMaterials;
            foreach (Material mat in materials)
            {
                if (mat.HasProperty("_ReflectionTex"))
                    mat.SetTexture("_ReflectionTex", mReflectionTexture);
            }
        }

        if (!mReflectionCamera)
        {
            GameObject go = new GameObject("Mirror Refl Camera id" + GetInstanceID() + " for " + currentCamera.GetInstanceID(), typeof(Camera), typeof(Skybox));
            go.hideFlags = HideFlags.HideAndDontSave;
            mReflectionCamera = go.GetComponent<Camera>();
            mReflectionCamera.enabled = false;
            mReflectionCamera.transform.position = transform.position;
            mReflectionCamera.transform.rotation = transform.rotation;
            mReflectionCamera.allowHDR = false;
            mReflectionCamera.allowMSAA = false;
            mReflectionCamera.allowDynamicResolution = false;
            mReflectionCamera.useOcclusionCulling = false;
        }

        reflectionCamera = mReflectionCamera;
    }

    private Vector4 CameraSpacePlane(Camera cam, Vector3 pos, Vector3 normal, float sideSign)
    {
        // 近平面推到反射面处 额外加自主控制的偏移 m_ClipPlaneOffset
        Vector3 offsetPos = pos + normal * mClipPlaneOffset;
        Matrix4x4 m = cam.worldToCameraMatrix;
        Vector3 cpos = m.MultiplyPoint(offsetPos);
        Vector3 cnormal = m.MultiplyVector(normal).normalized * sideSign;
        return new Vector4(cnormal.x, cnormal.y, cnormal.z, -Vector3.Dot(cpos, cnormal));
    }

    private static void CalculateReflectionMatrix(ref Matrix4x4 reflectionMat, Vector4 plane)
    {
        reflectionMat.m00 = (1F - 2F * plane[0] * plane[0]);
        reflectionMat.m01 = (-2F * plane[0] * plane[1]);
        reflectionMat.m02 = (-2F * plane[0] * plane[2]);
        reflectionMat.m03 = (-2F * plane[3] * plane[0]);

        reflectionMat.m10 = (-2F * plane[1] * plane[0]);
        reflectionMat.m11 = (1F - 2F * plane[1] * plane[1]);
        reflectionMat.m12 = (-2F * plane[1] * plane[2]);
        reflectionMat.m13 = (-2F * plane[3] * plane[1]);

        reflectionMat.m20 = (-2F * plane[2] * plane[0]);
        reflectionMat.m21 = (-2F * plane[2] * plane[1]);
        reflectionMat.m22 = (1F - 2F * plane[2] * plane[2]);
        reflectionMat.m23 = (-2F * plane[3] * plane[2]);

        reflectionMat.m30 = 0F;
        reflectionMat.m31 = 0F;
        reflectionMat.m32 = 0F;
        reflectionMat.m33 = 1F;
    }
}