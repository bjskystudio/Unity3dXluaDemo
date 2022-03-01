using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.Rendering.Universal.CNPRPipelineRuntimeData;

[ExecuteInEditMode]
public class CNPRPipelineRuntimeSettings : MonoBehaviour
{
    public bool m_bIsAutoRun = true;
    public CParameterFloat m_ShadowDistance = new CParameterFloat(60.0f);
    public CParameterInt m_ShadowCascades = new CParameterInt((int)ShadowCascades.NoCascades);
    public CParameterFloat m_Cascade2Split = new CParameterFloat(0.25f);

    void OnEnable()
    {
        if (m_bIsAutoRun)
            Refresh();
    }

    void OnDisable()
    {
        if (m_bIsAutoRun)
            InvalidAlll();
    }

    public void SetAutoRun(bool b)
    {
        if (b)
            Refresh();
        else
            InvalidAlll();
    }

    public void Refresh()
    {
        if (!enabled)
            return;

        CNPRPipelineRuntimeData data = CNPRPipeline.GetRuntimeData();

        RefreshField(m_ShadowDistance.is_overrided, () => { data.shadow_distance = m_ShadowDistance.value; }, () => { data.InvalidShadowDistance(); });
        RefreshField(m_ShadowCascades.is_overrided, () => { data.shadow_cascades = (ShadowCascades)m_ShadowCascades.value; }, () => { data.InvalidShadowCascades(); });
        RefreshField(m_Cascade2Split.is_overrided, () => { data.cascade_2_split = m_Cascade2Split.value; }, () => { data.InvalidCascade2Split(); });
    }

    public static void InvalidAlll()
    {
        CNPRPipeline.GetRuntimeData().InvalidShadowDistance();
        CNPRPipeline.GetRuntimeData().InvalidShadowCascades();
        CNPRPipeline.GetRuntimeData().InvalidCascade2Split();
    }

    void RefreshField(bool bOverrided, Action on_refresh_field, Action on_invalid_field)
    {
        if (bOverrided)
            on_refresh_field();
        else
            on_invalid_field();
    }
}
