// #ifndef UNIVERSAL_CHARACTER_INPUT_INCLUDED
// #define UNIVERSAL_CHARACTER_INPUT_INCLUDED

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

struct Attributes
{
    half4 positionOS        : POSITION;
    half2 texcoord          : TEXCOORD0;
    UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct Varyings
{
    half4 positionCS        : SV_POSITION;
    half2 uv                : TEXCOORD0;
    #if _SOFT_PARTICLE_ON
        half4 screenPos     : TEXCOORD1;
    #endif    
    UNITY_VERTEX_INPUT_INSTANCE_ID
    UNITY_VERTEX_OUTPUT_STEREO
};

Varyings SceneFogPlanePassVertex(Attributes input)
{
    Varyings output = (Varyings)0;

    UNITY_SETUP_INSTANCE_ID(input);
    UNITY_TRANSFER_INSTANCE_ID(input, output);
    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

    VertexPositionInputs vertexInput = GetVertexPositionInputs(input.positionOS.xyz);
    output.positionCS  = vertexInput.positionCS;
    output.uv = TRANSFORM_TEX(input.texcoord, _MainTex);
    #if _SOFT_PARTICLE_ON
    output.screenPos = vertexInput.screenPos;
    #endif
    return output;
}

half4 SceneFogPlanePassFragment(Varyings input) : SV_TARGET
{
    UNITY_SETUP_INSTANCE_ID(input);
    UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

    #if _SOFT_PARTICLE_ON
        float eyeDepth = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(input.screenPos)).r);
        float depthDelta = eyeDepth - input.screenPos.w;
        float fade = saturate(_InvFade * depthDelta);
        _Color.a *= fade;
    #endif

    half4 finalColor = _Color * SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex, input.uv);
    finalColor.rgb *= finalColor.a;
    return finalColor;
}

// #endif