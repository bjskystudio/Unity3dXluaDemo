#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/CommonMaterial.hlsl"

CBUFFER_START(UnityPerMaterial)
float4 _Color;
float _glintingFactor;
float _glintingColorFactor;
float4 _glintingColor;
float4 _MainTex_ST;
float4 _DiffuseTex_ST;
float4 _DetailTex_ST;
float4 _FixMask_ST;
float4 _ShadowColor;
float _ShadowThreshold;
float4 _EmissCol;
float _EmissThreshod;
float _SpeedY;
float _SpeedX;
float4 _TwoEmissCol;
float _TwoEmissThreshod;
float4 _ThreeEmissCol;
float _ThreeEmissThreshod;
float4 _RoisMask_ST;
float _Nois;
float _CutOut;
float4 _SpecCol;
float4 _LimMask_ST;
float _MetallicStrength;
float _RoughnessStrength;
float _LightToggle;
float _ReflectionIntensity;
float _BaseTexUVX;
float _BaseTexUVY;
float4 _OutLineColor;
float _OutlineWidth;
// float _Size;
// float4 _AtmoColor;
// float _OutLightPow;
// float _OutLightStrength;
float4  _RimColor;
float _RimPower;

half _MeshType;
half4 _CloudColor;
float _CloudSpeed;
float2 _MoveDir;
float _CloudDistortSpeed;
float _DensityX;
float _DensityY;
float _Planeness;
float _CloudSize;
float _cloudSmooth;
half _CloudRangeB;
half _CloudRangeT;
float _OutsidelightToggsss;
float _TillSize;
float _WetLevel;
float _PuddleLevel;
float _RainRippleIntensityMat;
float _RainRippleDisturbMat;
float4 _RainRippleDensityMat;
CBUFFER_END

TEXTURE2D(_MainTex);    SAMPLER(sampler_MainTex);
TEXTURE2D(_FixMask);    SAMPLER(sampler_FixMask);
TEXTURE2D(_RoisMask);    SAMPLER(sampler_RoisMask);
TEXTURE2D(_LimMask);    SAMPLER(sampler_LimMask);
TEXTURE2D(_ReflectionTex);    SAMPLER(sampler_ReflectionTex);
//add
TEXTURE2D(_DiffuseTex);    SAMPLER(sampler_DiffuseTex);
TEXTURE2D(_DetailTex);    SAMPLER(sampler_DetailTex);
TEXTURECUBE(_CubeMap);  SAMPLER(sampler_CubeMap);

