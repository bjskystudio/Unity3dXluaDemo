// #ifndef UNITY_UI_INCLUDED
// #define UNITY_UI_INCLUDED

inline float UnityGet2DClipping (in float2 position, in float4 clipRect)
{
    float2 inside = step(clipRect.xy, position.xy) * step(position.xy, clipRect.zw);
    return inside.x * inside.y;
}

inline float4 UnityGetUIDiffuseColor(in float2 position, in sampler2D mainTexture, in sampler2D alphaTexture, float4 textureSampleAdd)
{
    return float4(tex2D(mainTexture, position).rgb + textureSampleAdd.rgb, tex2D(alphaTexture, position).r + textureSampleAdd.a);
}
// #endif

inline float4 UnityPixelSnap (float4 pos)
{
        float2 hpc = _ScreenParams.xy * 0.5f;
    #if  SHADER_API_PSSL
        float2 temp = ((pos.xy / pos.w) * hpc) + float2(0.5f,0.5f);
        float2 pixelPos = float2(__v_floor_f32(temp.x), __v_floor_f32(temp.y));
    #else
        float2 pixelPos = round ((pos.xy / pos.w) * hpc);
    #endif
        pos.xy = pixelPos / hpc * pos.w;
        return pos;
}