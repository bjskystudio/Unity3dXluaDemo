using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YoukiaCore;
using UnityEngine.Rendering.Universal;
using Framework;
using Cinemachine;
using ResourceLoad;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CameraManager : MonoBehaviour
{
    private static CameraManager inst;
    public Camera maincamera;
    MonoBehaviour cameradata;
    public Transform HudRoot { get; private set; }
    public Transform StoryRoot { get; private set; }

    private CinemachineBrain cinemachineBrain;
    private CinemachineBrain CinemachineCamera
    {
        get
        {
            if (cinemachineBrain == null)
            {
                cinemachineBrain = maincamera.GetComponent<CinemachineBrain>();
            }
            return cinemachineBrain;
        }
    }

    public static CameraManager Get()
    {
        return inst;
    }

    public void Start()
    {
        inst = this;
        HudRoot = transform.Find("Canvas/HudRoot");
        StoryRoot = transform.Find("Canvas/StoryRoot");
        maincamera = Camera.main;
        if (CNPRPipeline.asset != null)
            cameradata = maincamera.GetAdditionalCameraData();
        else if (UniversalRenderPipeline.asset != null)
            cameradata = maincamera.GetUniversalAdditionalCameraData();
        AddCameraToStack(UIModel.Inst.BlurCamera);
        AddCameraToStack(UIModel.Inst.UICamera);
        DontDestroyOnLoad(gameObject);
       
    }


    private void OnDestroy()
    {
        inst = null;
    }

    public void AddCameraToStack(Camera camera)
    {
        if (CNPRPipeline.asset != null)
            ((CAdditionalCameraData)cameradata).cameraStack.Add(camera);
        else if (UniversalRenderPipeline.asset != null)
            ((UniversalAdditionalCameraData)cameradata).cameraStack.Add(camera);
    }
    public void RemoveCameraFromStrack(Camera camera)
    {
        if (CNPRPipeline.asset != null)
            ((CAdditionalCameraData)cameradata).cameraStack.Remove(camera);
        else if (UniversalRenderPipeline.asset != null)
            ((UniversalAdditionalCameraData)cameradata).cameraStack.Remove(camera);
    }
    public void VisibilityLayer(int layer, bool flag)
    {
        if (flag)
        {
            maincamera.cullingMask = (maincamera.cullingMask | layer);
        }
        else
        {
            if ((maincamera.cullingMask & layer) > 0)
            {
                maincamera.cullingMask -= layer;
            }
        }
    }

    #region Cinemachine摄像机动画控制

    private ResRef CBSettingResRef;

    public void SetDefaultBlend(int style = 0, float time = 2)
    {
        SetDefaultBlend((CinemachineBlendDefinition.Style)style, time);
    }

    public void SetDefaultBlend(CinemachineBlendDefinition.Style style, float time)
    {
        SetBlendData(null);
        CinemachineCamera.m_DefaultBlend = new CinemachineBlendDefinition(style, time);
    }

    public void LoadBlendData(string settingPath)
    {
        if (string.IsNullOrEmpty(settingPath))
        {
            SetBlendData(null);
        }
        else
        {
            ResourceManager.Instance.LoadScriptableObject(settingPath, (so, res) =>
            {
                SetBlendData(so as CinemachineBlenderSettings);
                CBSettingResRef = res;
            }, true);
        }
    }

    public void SetBlendData(CinemachineBlenderSettings settings)
    {
        ClearSBSetting();
        CinemachineCamera.m_CustomBlends = settings;
    }

    private void ClearSBSetting()
    {
        if (CBSettingResRef != null)
        {
            CBSettingResRef.Release();
        }
    }

    #endregion
#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        foreach (var VARIABLE in WorldManager.Instance.AllObj.Values)
        {
            if (VARIABLE is WorldItem t && t.TriggerDis > 0)
            {
                UnityEditor.Handles.color = Color.green;
                UnityEditor.Handles.DrawWireDisc(VARIABLE.Root.position, Vector3.up, t.TriggerDis);
            }
        }
    }
#endif
}
