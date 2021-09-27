using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

static class GUIUtils
{
    static readonly GUIContent sTmpContent = new GUIContent();
    public static GUIContent TempGUIContent(string _label, string _tooltip = null)
    {
        sTmpContent.text = _label;
        sTmpContent.tooltip = _tooltip;
        return sTmpContent;
    }

    static public void ModuleHeader(string moduleName,ref bool isEnable, ref bool isExpanded)
    {
        Rect rect = GUILayoutUtility.GetRect(TempGUIContent(moduleName), new GUIStyle("ShurikenModuleTitle"));

        //Checkmark
        Rect toggleRect = new Rect(rect);
        toggleRect.width = 16f;
        toggleRect.x += 2f;
        toggleRect.y += 1f;
        
        isEnable = GUI.Toggle(toggleRect, isEnable, "", new GUIStyle("ShurikenCheckMark"));

        bool guiChanged = GUI.changed;
        
        EditorGUI.BeginChangeCheck();

        Color guiColor = GUI.color;
        GUI.Toggle(rect, isExpanded, moduleName, new GUIStyle("ShurikenModuleTitle"));
        GUI.color = guiColor;

        if (EditorGUI.EndChangeCheck() && Event.current.type == EventType.Used)
        {
            //Left click
            if (Event.current.button == 0)
            {
                isExpanded = !isExpanded;
            }
        }

        //Don't affect GUI.changed to prevent overwritting values when opening/closing module
        GUI.changed = guiChanged;

        //Checkmark (visual)
        GUI.Toggle(toggleRect, isEnable, "", new GUIStyle("ShurikenCheckMark"));

        if (isExpanded)
            GUILayout.Space(2f);
        
    }

}

public class FXStandardInspector : ShaderGUI
{
    //Styles
    static private GUIStyle _s_ShurikenEffectBg_style;
    static private GUIStyle ShurikenEffectBgStyle
    {
        get
        {
            if (_s_ShurikenEffectBg_style == null)
            {
                _s_ShurikenEffectBg_style = new GUIStyle("ShurikenEffectBg");
                _s_ShurikenEffectBg_style.margin = new RectOffset(0, 0, 0, 0);
            }
            return _s_ShurikenEffectBg_style;
        }
    }

    static private GUIStyle _s_ShurikenModuleBg_style;
    static private GUIStyle ShurikenModuleBgStyle
    {
        get
        {
            if (_s_ShurikenModuleBg_style == null)
            {
                _s_ShurikenModuleBg_style = new GUIStyle("ShurikenModuleBg");
                _s_ShurikenModuleBg_style.margin = new RectOffset(0, 0, 0, -10);
            }
            return _s_ShurikenModuleBg_style;
        }
    }

    private bool[] isExpandeds = new bool[8];
    private bool[] isEnables = new bool[8];
    static private string[] mainPorps = new string[]
    {
        "_GENERALEFFECTS","_FinalFloat","_Color", "_Alpha", "_Transparent_Write_Depth", "_WriteDepthThreshold", "_Frame_Blend", "_Soft_Particle","_SoftNearFade", "_SoftFarFade", "_MainTex", "_MainTexSpeed_U", "_MainTexSpeed_V", "_U_Mirror", "_V_Mirror", "_MainTexAngle",
        "_MainTexStrength", "_zWrite", "_zTest", "_cull", "_srcBlend", "_dstBlend", "_srcAlphaBlend", "_dstAlphaBlend"
    };
    static private string[] detailPorps = new string[]
    {
        "_DetailTex", "_DetailTexAngle", "_DetailTexSpeed_U", "_DetailTexSpeed_V", "_DetailTexStrength", "_DetailChoiceRGB", "_DetailAddorMultiply"
    };
    static private string[] maskPorps = new string[]
    {
        "_MaskTex", "_MaskRGBA", "_MaskTexAngle", 
    };
    static private string[] distortPorps = new string[]
    {
        "_DistortTex", "_DistRGBA", "_DistForceU", "_DistForceV", "_DistTime"
    };
    static private string[] RimPorps = new string[]
    {
        "_RimLightColor", "_RimLightPower", "_RimLightIntensity", "_RimLightCenterAlpha", "_RimLightAlpha"
    };
    static private string[] dissipatePorps = new string[]
    {
        "_DissolutionTex", "_DissolutionThreshold", "_DissolutionSoftness", "_DissolutionColor",//"_DissipateAmount", "_DissipateSoftCutout", "_UseParticlesAlphaCutout"
    };

    static private string[] backMask = new string[]
    {
        "_BlendTex", "_backColor", "_Expose", "_RangeX", "_RangeY"
    };

    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
        //base.OnGUI(materialEditor, properties);
        materialEditor.SetDefaultGUIWidths();
        MaterialProperty curProp = null;
        for (int i = 0; i < mainPorps.Length; i++)
        {
            curProp = FindProperty(mainPorps[i], properties);
            materialEditor.ShaderProperty(curProp, curProp.displayName);
        }


        Material material = materialEditor.target as Material;

        //bool toggle = false;
        GUILayout.BeginVertical(ShurikenModuleBgStyle, GUILayout.MinHeight(10f));
        isEnables[0] = material.IsKeywordEnabled("_USE_DETAIL_ON");
        GUIUtils.ModuleHeader("Use Detail Texture(细节纹理)", ref isEnables[0], ref isExpandeds[0]);
        if (isExpandeds[0])
        {
            for (int i = 0; i < detailPorps.Length; i++)
            {
                curProp = FindProperty(detailPorps[i], properties);
                materialEditor.ShaderProperty(curProp, curProp.displayName);
            }
        }
        isEnables[1] = material.IsKeywordEnabled("_USE_MASK_ON");
        GUIUtils.ModuleHeader("Use Mask Texture(遮罩)", ref isEnables[1], ref isExpandeds[1]);
        if (isExpandeds[1])
        {
            for (int i = 0; i < maskPorps.Length; i++)
            {
                curProp = FindProperty(maskPorps[i], properties);
                materialEditor.ShaderProperty(curProp, curProp.displayName);
            }
        }

        isEnables[2] = material.IsKeywordEnabled("_USE_DISTORT_ON");
        GUIUtils.ModuleHeader("Use Distort Texture(纹理扭曲)", ref isEnables[2], ref isExpandeds[2]);
        if (isExpandeds[2])
        {
            for (int i = 0; i < distortPorps.Length; i++)
            {
                curProp = FindProperty(distortPorps[i], properties);
                materialEditor.ShaderProperty(curProp, curProp.displayName);
            }
        }
        isEnables[3] = material.IsKeywordEnabled("_USE_RIMLIGHT_ON");
        GUIUtils.ModuleHeader("Use Rim Light(边光)", ref isEnables[3], ref isExpandeds[3]);
        if (isExpandeds[3])
        {
            for (int i = 0; i < RimPorps.Length; i++)
            {
                curProp = FindProperty(RimPorps[i], properties);
                materialEditor.ShaderProperty(curProp, curProp.displayName);
            }
        }
        isEnables[4] = material.IsKeywordEnabled("_USE_CUTOUT");
        GUIUtils.ModuleHeader("Use Dissipate(消散溶解)", ref isEnables[4], ref isExpandeds[4]);
        if (isExpandeds[4])
        {
            for (int i = 0; i < dissipatePorps.Length; i++)
            {
                curProp = FindProperty(dissipatePorps[i], properties);
                materialEditor.ShaderProperty(curProp, curProp.displayName);
            }
        }
        //isEnables[5] = material.IsKeywordEnabled("_USE_CUTOUT_TEX");
        //GUIUtils.ModuleHeader("Use Dissipate(消散溶解Mask)", ref isEnables[5], ref isExpandeds[5]);
        //if (isExpandeds[5])
        //{
        //    curProp = FindProperty("_DissipateCutoutTex", properties);
        //    materialEditor.ShaderProperty(curProp, curProp.displayName);
        //}

        //isEnables[6] = material.IsKeywordEnabled("_USE_CUTOUT_THRESHOLD");
        //GUIUtils.ModuleHeader("Use Dissipate(消散溶解Burn)", ref isEnables[6], ref isExpandeds[6]);
        //if (isExpandeds[6])
        //{
        //    curProp = FindProperty("_DissipateColor", properties);
        //    materialEditor.ShaderProperty(curProp, curProp.displayName);
        //    curProp = FindProperty("_DissipateCutoutThreshold", properties);
        //    materialEditor.ShaderProperty(curProp, curProp.displayName);
        //}
        isEnables[7] = material.IsKeywordEnabled("_BACK_MASK") || !(material.shader.name == "Fairy/Effect/FX_Standard");
        GUIUtils.ModuleHeader("Use Back Mask(背景混合)", ref isEnables[7], ref isExpandeds[7]);
        if (isExpandeds[7])
        {
            for (int i = 0; i < backMask.Length; i++)
            {
                curProp = FindProperty(backMask[i], properties);
                materialEditor.ShaderProperty(curProp, curProp.displayName);
            }
        }

        GUILayout.EndVertical();
        GUILayout.Space(5);
        GuiLine();
        GUILayout.Space(5);
        materialEditor.RenderQueueField();
        materialEditor.EnableInstancingField();
        materialEditor.DoubleSidedGIField();

        if (isEnables[0] != material.IsKeywordEnabled("_USE_DETAIL_ON"))
        {
            SetKeyword(material, "_USE_DETAIL_ON", isEnables[0]);
        }
        if (isEnables[1] != material.IsKeywordEnabled("_USE_MASK_ON"))
        {
            SetKeyword(material, "_USE_MASK_ON", isEnables[1]);
        }
        if (isEnables[2] != material.IsKeywordEnabled("_USE_DISTORT_ON"))
        {
            SetKeyword(material, "_USE_DISTORT_ON", isEnables[2]);
        }
        if (isEnables[3] != material.IsKeywordEnabled("_USE_RIMLIGHT_ON"))
        {
            SetKeyword(material, "_USE_RIMLIGHT_ON", isEnables[3]);
        }
        if (isEnables[4] != material.IsKeywordEnabled("_USE_CUTOUT"))
        {
            SetKeyword(material, "_USE_CUTOUT", isEnables[4]);
        }
        //if (isEnables[5] != material.IsKeywordEnabled("_USE_CUTOUT_TEX"))
        //{
        //    SetKeyword(material, "_USE_CUTOUT_TEX", isEnables[5]);
        //}
        //if (isEnables[6] != material.IsKeywordEnabled("_USE_CUTOUT_THRESHOLD"))
        //{
        //    SetKeyword(material, "_USE_CUTOUT_THRESHOLD", isEnables[6]);
        //}
        if ((material.shader.name == "HZW/Effect/FX_Standard") && (isEnables[7] != material.IsKeywordEnabled("_BACK_MASK")))
        {
            SetKeyword(material, "_BACK_MASK", isEnables[7]);
        }

        bool passEnable = material.GetShaderPassEnabled("ForwardBase");
        if (material.IsKeywordEnabled("_TRANSPARENT_WRITE_DEPTH"))
        {
            if (!passEnable)
            {
                material.SetShaderPassEnabled("ForwardBase", true);
            }
        }
        else
        {
            if (passEnable)
            {
                material.SetShaderPassEnabled("ForwardBase", false);
            }
        }

    }


    static void SetKeyword(Material m, string keyword, bool state)
    {
        if (state)
            m.EnableKeyword(keyword);
        else
            m.DisableKeyword(keyword);
    }

    void GuiLine(int i_height = 1)

    {

        Rect rect = EditorGUILayout.GetControlRect(false, i_height, new GUIStyle("DefaultLineSeparator"));

        rect.height = i_height;

        EditorGUI.DrawRect(rect, new Color(0.12f, 0.12f, 0.12f, 1.333f));

    }
}
