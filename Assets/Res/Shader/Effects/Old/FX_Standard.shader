/*
*  float uvOffset = frac(Time.y * _Speed); 
*  Can Be Optimized By C#: 
*  (float)Math.IEEERemainder(Time.timeSinceLevelLoad * Speed, 1.0);
*/
Shader "SaintSeiya2/Effect/FX_Standard" 
{ 
    Properties
	{
		//Base
        [Toggle(_GENERALEFFECTS)] _GENERALEFFECTS("通用特效变色开关", Float) = 0
		_FinalFloat("颜色比例值: 0=ColorA、0.4=ColorB、0.8=ColorC",Float) = 0

		[Space(10)]
        [HDR]_Color ("Color", Color) = (1, 1, 1, 1) //[HDR]
        _Alpha ("Alpha", float) = 1

		[Toggle(_TRANSPARENT_WRITE_DEPTH)] _Transparent_Write_Depth("Transparent Write Depth", Float) = 0
		_WriteDepthThreshold("Write Depth Alpha Threshold", Range(0, 1)) = 0.95
        
		[Toggle(_FRAME_BLENDING)] _Frame_Blend("Frame Blend", Float) = 0
		[Toggle(_SOFT_PARTICLE)] _Soft_Particle("Soft Particle", Float) = 0
		_SoftNearFade("Soft Particles Near Fade", Float) = 0.0
        _SoftFarFade("Soft Particles Far Fade", Float) = 1.0

		_MainTex ("MainTex", 2D) = "white" {}

		_MainTexSpeed_U ("MainTexSpeed_U", Float ) = 0
		_MainTexSpeed_V ("MainTexSpeed_V", Float ) = 0

		[MaterialToggle] _U_Mirror ("U_Mirror", Float ) = 0
        [MaterialToggle] _V_Mirror ("V_Mirror", Float ) = 0
		_MainTexAngle ("MainTexAngle", Range(0, 1.0)) = 0
		_MainTexStrength ("MainTexStrength", Range(0, 10)) = 1

		_BlendTex ("Back Ground Tex", 2D) = "black" {}
		_backColor("Back Color", Color) = (0,0,0,1)
		_Expose("Expose",  Range(1 , 2)) = 1.1
		_RangeX("RangeCenter",  Range(0 , 0.5)) = 0.16
		_RangeY("RangeCorner",  Range(0.51, 1)) = 0.51
		
		//Detail
		_DetailTex ("DetailTex", 2D) = "white" {}
		_DetailTexAngle ("DetailTexAngle", Range(0, 360)) = 0
		_DetailTexSpeed_U ("DetailTexSpeed_U", Float) = 0
		_DetailTexSpeed_V ("DetailTexSpeed_V", Float) = 0
		_DetailTexStrength ("DetailTexStrength", Range(0, 10)) = 1
		_DetailChoiceRGB ("DetailChoiceRGB", Range(0, 1)) = 0
		_DetailAddorMultiply ("DetailAddorMultiply", Range(0, 1)) = 0

		//Mask
		_MaskTex ("MaskTex", 2D) = "white" {}
		_MaskRGBA ("MaskRGBA", Vector) = (0, 0, 0, 1)
		_MaskTexAngle ("MaskTexAngle", Range(0, 360)) = 0

		//DisTex
		_DistortTex ("DistortTex", 2D) = "white" {}
		_DistRGBA ("DistRGBA", Vector) = (0, 0, 0, 1)
		_DistForceU ("DistStrength_U", range (-1,1)) = 1
		_DistForceV ("DistStrength_V", range (-1,1)) = 1
        _DistTime("DistSpeed", range (-1,1)) = 0.1

		//Rim
		[HDR]_RimLightColor("RimLightColor",Color) = (1.0,1.0,0.0,1.0)
		_RimLightPower("RimLightPower",Range(0.001,10.0)) = 1.0
		_RimLightIntensity("RimLightIntensity", float) = 1.0
		[MaterialToggle]_RimLightCenterAlpha("RimLightCenterAlpha", Range(0.0,1.0)) = 1.0
		_RimLightAlpha("RimLightAlpha", Range(0.0,1.0)) = 1.0

		//Dissipate
		/*_DissipateAmount("DissipateBase Amount",Range(0.0,1.0)) = 0.0
		_DissipateSoftCutout("DissipateBase Soft Cutout", Float) = 1
		_UseParticlesAlphaCutout("DissipateBase Use Particles Alpha", Int) = 0
		_DissipateCutoutTex("DissipateTex", 2D) = "white" {}
		[HDR]_DissipateColor("DissipateCutout Color", Color) = (1,1,1,1)
		_DissipateCutoutThreshold("DissipateCutout Threshold",Range(0.0,1.0)) = 0.015*/

		_DissolutionTex("消融贴图", 2D) = "white" { }
		_DissolutionThreshold("消融阀值", Float) = 0.5
		_DissolutionSoftness("消融过渡", Float) = 0.5
		[HDR]_DissolutionColor("消融顔色", Color) = (1,0,0,1)


		////[MaterialToggle]
		//[HideInInspector] _FxSrcBlend ("SrcBlend", Int) = 5 // SrcAlpha
		//[HideInInspector] _FxDstBlend ("DstBlend", Int) = 10 // OneMinusSrcAlpha
		//[HideInInspector] _FxCullMode ("_CullMode", Int) = 2 // Back
		//_ZTest ("_ZTest", Float) = 4 // LessEqual;
        //此段可以将 ZWrite 选项暴露在Unity的Inspector中
		[Enum(Off, 0, On, 1)] _zWrite("ZWrite", Float) = 0
		//此段可以将 Ztest  选项暴露在Unity的Inspector中
		[Enum(UnityEngine.Rendering.CompareFunction)] _zTest("ZTest", Float) = 8
		//此段可以将 Cull  选项暴露在Unity的Inspector中
		[Enum(UnityEngine.Rendering.CullMode)] _cull("Cull Mode", Float) = 2
		//此段可以将 Blend 选项暴露在Unity的Inspector中
		[Enum(UnityEngine.Rendering.BlendMode)] _srcBlend("Src Blend Mode", Float) = 5
		[Enum(UnityEngine.Rendering.BlendMode)] _dstBlend("Dst Blend Mode", Float) = 10
		[Enum(UnityEngine.Rendering.BlendMode)] _srcAlphaBlend("Src Alpha Blend Mode", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] _dstAlphaBlend("Dst Alpha Blend Mode", Float) = 10
    }

	CGINCLUDE
		#pragma target 3.0

		#pragma shader_feature _USE_DISTORT_ON
		#pragma multi_compile __ _USE_DETAIL_ON
		#pragma shader_feature _USE_MASK_ON
		#pragma shader_feature _USE_RIMLIGHT_ON
		#pragma shader_feature _USE_DISSIPATE_ON
		#pragma shader_feature _USE_CUTOUT
		/*#pragma shader_feature _USE_CUTOUT_TEX
		#pragma shader_feature _USE_CUTOUT_THRESHOLD*/
		#pragma shader_feature _FRAME_BLENDING
		#pragma shader_feature _SOFT_PARTICLE
		#pragma shader_feature _BACK_MASK
		#pragma shader_feature _TRANSPARENT_WRITE_DEPTH

		#pragma shader_feature _GENERALEFFECTS

		#pragma multi_compile __ SOFTPARTICLES_ON
					//#pragma multi_compile __ _DEPTHBUFFER_ON
		#pragma multi_compile_instancing

		#include "FX_Standard.cginc"
	ENDCG

    SubShader
	{
        Tags
		{
			"IgnoreProjector"="True"
			"Queue"="Transparent"
			"RenderType"="Transparent"
			//"PreviewType"="Plane"
		}

		// Pass{
		// 	Tags 
		// 	{
  //               "LightMode" = "LightweightForward"
  //           }
			
		// 	ZTest[_zTest]
		// 	Cull[_cull]
		// 	ZWrite On
		// 	CGPROGRAM
		// 	#pragma vertex vert
		// 	#pragma fragment frag1
				
		// 	ENDCG
		// }

   
        Pass 
		{
            Name "FORWARD"
            Tags 
			{
                "LightMode" = "SRPDefaultUnlit"
            }
   //         Cull [_FxCullMode]
   //         Blend [_FxSrcBlend] [_FxDstBlend]
   //         ZWrite Off
			//ZTest [_ZTest]
			Blend[_srcBlend][_dstBlend],[_srcAlphaBlend][_dstAlphaBlend]
			ZWrite[_zWrite]
			ZTest[_zTest]
			Cull[_cull]
           
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
 
            ENDCG
        }
    }
	CustomEditor "FXStandardInspector"
}
