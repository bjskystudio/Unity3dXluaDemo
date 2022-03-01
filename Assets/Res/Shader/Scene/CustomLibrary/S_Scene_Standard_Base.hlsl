#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
#include "Assets/EngineCenter/FogBetter/Shader/FogBetter.hlsl"

struct Attributes
{
    float4 positionOS   : POSITION;
    float3 normalOS     : NORMAL;
    float4 tangentOS    : TANGENT;
    float2 texcoord     : TEXCOORD0;
    float2 lightmapUV   : TEXCOORD1;
};

struct Varyings
{
    float4 positionCS               : SV_POSITION;
    float2 uv                       : TEXCOORD0;
    DECLARE_LIGHTMAP_OR_SH(lightmapUV, vertexSH, 1);
    float3 positionWS               : TEXCOORD2;
    float4 normalWS                 : TEXCOORD3;   
    float4 tangentWS                : TEXCOORD4;   
    float4 bitangentWS              : TEXCOORD5; 
    float4 fogFactorAndVertexLight  : TEXCOORD6; 
	float3 worldPos                 : TEXCOORD7;
    COORDS_FOG_BETTER(8)    
    UNITY_VERTEX_OUTPUT_STEREO

    #ifdef REFLECTION 
        float4 screenPos : TEXCOORD9; //取屏幕空间位置
    #endif
    
};

void InitializeInputData(Varyings input, half3 normalTS, out InputData inputData)
{
    inputData = (InputData)0;

    inputData.positionWS = input.positionWS;

    half3 viewDirWS = half3(input.normalWS.w, input.tangentWS.w, input.bitangentWS.w);
    inputData.normalWS = TransformTangentToWorld(normalTS, half3x3(input.tangentWS.xyz, input.bitangentWS.xyz, input.normalWS.xyz));

    inputData.normalWS = NormalizeNormalPerPixel(inputData.normalWS);
    viewDirWS = SafeNormalize(viewDirWS);
    inputData.viewDirectionWS = viewDirWS;
    #ifdef RECEIVESHADOWS
        inputData.shadowCoord = TransformWorldToShadowCoord(inputData.positionWS);
    #else
        inputData.shadowCoord = float4(0, 0, 0, 0);
    #endif
    inputData.fogCoord = input.fogFactorAndVertexLight.x;
    inputData.vertexLighting = input.fogFactorAndVertexLight.yzw;
    inputData.bakedGI = SAMPLE_GI(input.lightmapUV, input.vertexSH, inputData.normalWS);
}


Varyings ScenePassVertex(Attributes input)
{
    Varyings output = (Varyings)0;

    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

    VertexPositionInputs vertexInput = GetVertexPositionInputs(input.positionOS.xyz);
    VertexNormalInputs normalInput = GetVertexNormalInputs(input.normalOS, input.tangentOS);
    half3 viewDirWS = GetCameraPositionWS() - vertexInput.positionWS;
    half3 vertexLight = VertexLighting(vertexInput.positionWS, normalInput.normalWS);
    half fogFactor = ComputeFogFactor(vertexInput.positionCS.z);

    output.uv = TRANSFORM_TEX(input.texcoord, _BaseMap);

    output.normalWS = half4(normalInput.normalWS, viewDirWS.x);
    output.tangentWS = half4(normalInput.tangentWS, viewDirWS.y);
    output.bitangentWS = half4(normalInput.bitangentWS, viewDirWS.z);

    OUTPUT_LIGHTMAP_UV(input.lightmapUV, unity_LightmapST, output.lightmapUV);
    OUTPUT_SH(output.normalWS.xyz, output.vertexSH);

    output.fogFactorAndVertexLight = half4(fogFactor, vertexLight);

    output.positionWS = vertexInput.positionWS;
    output.positionCS = vertexInput.positionCS;
    output.worldPos = TransformObjectToWorld(input.positionOS.xyz);

    //镜面反射
    #ifdef REFLECTION 
        output.screenPos = ComputeScreenPos(output.positionCS);
    #endif    

	TRANSFER_FOG_BETTER(output, input.texcoord);
    return output;
}


half4 ScenePassFragment(Varyings input) : SV_Target
{
    UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

    SurfaceData surfaceData;
    InitializeStandardLitSurfaceData(input.uv, surfaceData);

    InputData inputData;
    InitializeInputData(input, surfaceData.normalTS, inputData);

    half4 color = UniversalFragmentPBR(inputData, surfaceData.albedo, surfaceData.metallic, surfaceData.specular, surfaceData.smoothness, surfaceData.occlusion, surfaceData.emission, surfaceData.alpha);

    //镜面反射
    #ifdef REFLECTION 
        float2 refuv = input.screenPos.xy / input.screenPos.w;
        float4 reflectCol = SAMPLE_TEXTURE2D(_ReflectionTex, sampler_ReflectionTex, refuv.xy);
        reflectCol = lerp(color,reflectCol,surfaceData.Reflfecmask);
        color = lerp(color,reflectCol,_ReflectionIntensity);
    #endif

    APPLY_FOG_BETTER(color.rgb, input.worldPos, input.fogBetterCoord);
    return color;
}
