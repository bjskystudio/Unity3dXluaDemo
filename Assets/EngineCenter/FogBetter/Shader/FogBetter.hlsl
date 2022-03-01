#if _FOG_BETTER_ON
    #if _FOG_BETTER_SINGLE_ON //为了支持单独控制特定物体的雾效
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

        float4 _FogDistColor_Single;
        float4 _FogHeightColor_Single;

        float4 _FogDisturbTex_Single_ST;
        float _FogDisturbIntensity_Single;
        float _FogDisturbSpeedX_Single;
        float _FogDisturbSpeedY_Single;

        float _FogDistDensity_Single;
        float _FogDistIntensity_Single;

        float _FogHeightDensity_Single;
        float _FogHeightDistDensity_Single;
        float _FogHeightIntensity_Single;
        float _FogHeightBase_Single;

        float _FogDistOffset_Single;
        TEXTURE2D(_FogDisturbTex_Single);   
        SAMPLER(sampler_FogDisturbTex_Single);


        float3 ApplyFog(float3 color, float3 worldPos, float2 uv)
        {
            float disturb = SAMPLE_TEXTURE2D(_FogDisturbTex_Single,sampler_FogDisturbTex_Single, uv + float2(_Time.x * _FogDisturbSpeedX_Single, _Time.x * _FogDisturbSpeedY_Single)).r;
            float3 viewDir = worldPos.xyz - _WorldSpaceCameraPos;
            float distance = length(viewDir);
            float height = worldPos.y - _FogHeightBase_Single;

            _FogDistDensity_Single = _FogDistDensity_Single / 100;
            distance += _FogDistOffset_Single;
            float fogDistAmount = saturate(_FogDistIntensity_Single * (1 - saturate(exp(-_FogDistDensity_Single * distance))) + disturb * _FogDisturbIntensity_Single);
            float3 fogColor = lerp(color, _FogDistColor_Single, fogDistAmount);

            _FogHeightDensity_Single = _FogHeightDensity_Single / 100;
            _FogHeightDistDensity_Single = _FogHeightDistDensity_Single / 100;
            
            float fogAmount = saturate(_FogHeightIntensity_Single * exp(-_FogHeightDensity_Single * height) * smoothstep(0.4, 1, (1 - exp(-_FogHeightDistDensity_Single * distance))));
            fogColor = lerp(fogColor, _FogHeightColor_Single, fogAmount);

            return fogColor;
        }

        #define COORDS_FOG_BETTER(idx) float fogBetterCoord : TEXCOORD##idx;
        #define TRANSFER_FOG_BETTER(o, uv) o.fogBetterCoord = TRANSFORM_TEX(uv, _FogDisturbTex_Single)
        #define APPLY_FOG_BETTER(color, worldPos, fogCoord) color.rgb = ApplyFog(color, worldPos, fogCoord)
    #else
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"


        float4 _FogDistColor;
        float4 _FogHeightColor;

        float4 _FogDisturbTex_ST;
        float _FogDisturbIntensity;
        float _FogDisturbSpeedX;
        float _FogDisturbSpeedY;

        float _FogDistDensity;
        float _FogDistIntensity;

        float _FogHeightDensity;
        float _FogHeightDistDensity;
        float _FogHeightIntensity;
        float _FogHeightBase;

        float _FogDistOffset;

        TEXTURE2D(_FogDisturbTex);   
        SAMPLER(sampler_FogDisturbTex);
        
        float3 ApplyFog(float3 color, float3 worldPos, float2 uv)
        {
            float disturb = SAMPLE_TEXTURE2D(_FogDisturbTex, sampler_FogDisturbTex , uv + float2(_Time.x * _FogDisturbSpeedX, _Time.x * _FogDisturbSpeedY)).r;
            float3 viewDir = worldPos.xyz - _WorldSpaceCameraPos;
            float distance = length(viewDir);
            float height = worldPos.y - _FogHeightBase;

            _FogDistDensity = _FogDistDensity / 100;
            distance += _FogDistOffset;
            float fogDistAmount = saturate(_FogDistIntensity * (1 - saturate(exp(-_FogDistDensity * distance))) + disturb * _FogDisturbIntensity);
            float3 fogColor = lerp(color, _FogDistColor.rgb, fogDistAmount);

            _FogHeightDensity = _FogHeightDensity / 100;
            _FogHeightDistDensity = _FogHeightDistDensity / 100;
            
            float fogAmount = saturate(_FogHeightIntensity * exp(-_FogHeightDensity * height) * smoothstep(0.4, 1, (1 - exp(-_FogHeightDistDensity * distance))));
            fogColor = lerp(fogColor, _FogHeightColor.rgb, fogAmount);

            return fogColor;
        }

        #define COORDS_FOG_BETTER(idx) float fogBetterCoord : TEXCOORD##idx;
        #define TRANSFER_FOG_BETTER(o, uv) o.fogBetterCoord = TRANSFORM_TEX(uv, _FogDisturbTex)
        #define APPLY_FOG_BETTER(color, worldPos, fogCoord) color.rgb = ApplyFog(color, worldPos, fogCoord)
    #endif
#else
    #define COORDS_FOG_BETTER(idx)
    #define TRANSFER_FOG_BETTER(o, uv)
    #define APPLY_FOG_BETTER(color, worldPos, disturbUV)
#endif