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
using Framework.Environment;
using YoukiaCore.Collections;

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
    /// <summary>
    /// 已打开的场景设置列表
    /// </summary>

    private XCoreList<EnvironmentSetting> ESList = new XCoreList<EnvironmentSetting>();
    /// <summary>
    /// 当前场景设置
    /// </summary>
    private EnvironmentSetting LastEnvironmentSetting
    {
        get
        {
            if (ESList.Size > 0)
            {
                return ESList[ESList.Size - 1];
            }
            return null;
        }
    }
    /// <summary>
    /// 已注册的场景绘制阻挠列表
    /// </summary>
    private Dictionary<string, bool> ObstructionDic = new Dictionary<string, bool>();

    public static CameraManager Get()
    {
        return inst;
    }

    public void Start()
    {
        maincamera = transform.Find("MainCamera").GetComponent<Camera>();
        inst = this;
        HudRoot = transform.Find("Canvas/HudRoot");
        StoryRoot = transform.Find("Canvas/StoryRoot");
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
        ObstructionDic.Clear();
        ESList.Clear();
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

    public void AddEnvironmentSetting(EnvironmentSetting setting)
    {
        LastEnvironmentSetting?.SetBloomPostActive(false);
        ESList.Add(setting);
        //SetMainCameraCullingMask(setting.CameraCullingMask);
        //场景的添加,遵循检查是否有场景阻挠器起效的原则
        CheckMainCameraVisibility();
    }

    public void RemoveEnvironmentSetting(EnvironmentSetting setting)
    {
        ESList.Remove(setting);
        var curSetting = LastEnvironmentSetting;
        if (curSetting != null)
        {
            curSetting.UpdateSetting();
            curSetting.LightMap?.SetSceneInfo();
            //SetMainCameraCullingMask(curSetting.CameraCullingMask);
            //场景的移除,遵循检查是否有场景阻挠器起效的原则
            CheckMainCameraVisibility();
        }
    }

    /// <summary>
    /// 注册场景绘制阻挠器
    /// </summary>
    /// <param name="viewName"></param>
    public void RegisterObst(string viewName)
    {
        if (!ObstructionDic.ContainsKey(viewName))
        {
            ObstructionDic.Add(viewName, true);
        }
        CheckMainCameraVisibility();
    }

    /// <summary>
    /// 取消注册场景绘制阻挠器
    /// </summary>
    /// <param name="viewName"></param>
    public void UnRegisterObst(string viewName)
    {
        if (ObstructionDic.ContainsKey(viewName))
        {
            ObstructionDic.Remove(viewName);
        }
        CheckMainCameraVisibility();
    }

    /// <summary>
    /// 检查主摄像机可见性
    /// </summary>
    /// <param name="value"></param>
    private void CheckMainCameraVisibility()
    {
        if (ObstructionDic.Count == 0)
        {
            var curSetting = LastEnvironmentSetting;
            if (curSetting != null)
            {
                SetMainCameraCullingMask(curSetting.CameraCullingMask);
            }
        }
        else
        {
            SetMainCameraCullingMask(0);
        }
    }

    private void SetMainCameraCullingMask(int cullingMask)
    {
        maincamera.cullingMask = cullingMask;
    }

    public void VisibilityLayer(int layer, bool flag)
    {
        if (flag)
        {
            maincamera.cullingMask = (maincamera.cullingMask | 1 << layer);
        }
        else
        {
            maincamera.cullingMask = maincamera.cullingMask & ~(1 << layer);
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
            if (VARIABLE!=null && VARIABLE is WorldItem t && t.TriggerDis > 0)
            {
                UnityEditor.Handles.color = Color.green;
                UnityEditor.Handles.DrawWireDisc(VARIABLE.Root.position, Vector3.up, t.TriggerDis-0.25f);
            }
        }
    }
#endif
}
