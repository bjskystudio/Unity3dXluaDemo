// #ifndef UNIVERSAL_CHARACTER_INPUT_INCLUDED
// #define UNIVERSAL_CHARACTER_INPUT_INCLUDED

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl" 
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/CommonMaterial.hlsl"

CBUFFER_START(UnityPerMaterial)
half4 _Color;
half4 _MainTex_ST;

half _SOFT_PARTICLE;
half _InvFade;
CBUFFER_END

TEXTURE2D(_MainTex);                       SAMPLER(sampler_MainTex);
TEXTURE2D(_CameraDepthTexture);            SAMPLER(sampler_CameraDepthTexture);

// #endif