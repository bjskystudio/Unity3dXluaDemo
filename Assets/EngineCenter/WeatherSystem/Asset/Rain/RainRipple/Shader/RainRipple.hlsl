#if _RAIN_ON && _RAIN_RIPPLE_ON
    float _RainRippleIntensity;
    float _RainRippleDisturb;
    float4 _RainRippleDensity;
    float _RainRippleSpeed;

    TEXTURE2D(_RainRippleTex);    SAMPLER(sampler_RainRippleTex);

    float3 RainRippleNormalSingle(float2 uv, float time, float rainRippleDisturbMat)
    {
        float4 rainRippleTexData = SAMPLE_TEXTURE2D(_RainRippleTex, sampler_RainRippleTex, uv);
        rainRippleTexData.yz = rainRippleTexData * 2 - 1;
        float dropFrac = frac(rainRippleTexData.w + time); //水波出现时间差
        float timeFrac = dropFrac - 1 + rainRippleTexData.x; //限制水波出现的位置
        //rainRippleTexData.x 这里乘以x 为了让水波向外的时候衰减
        float rippleFactor = rainRippleTexData.x * (sin(clamp(timeFrac * 9, 0, 3) * 3.1415926)) * _RainRippleDisturb * rainRippleDisturbMat;
        float3 rippleNormal = float3(rainRippleTexData.y * rippleFactor, 1, rainRippleTexData.z * rippleFactor);
        return rippleNormal;
    }

    void RainRippleNormal(out float3 normalTotal, float2 uv, float rainRippleIntensityMat, float rainRippleDisturbMat, float2 rainRippleDensityMat)
    {
        uv = uv * _RainRippleDensity.xy * rainRippleDensityMat.xy;
        float4 timeMul = float4(0.9, 0.65, 0.81, 1.1);
        float4 timeAdd = float4(1.3, 0.8, 1.1, 0.54);
        float4 time = _Time.y * timeMul + timeAdd;
        time = _RainRippleSpeed * time;

        float3 rainRippleNormal1 = RainRippleNormalSingle(uv + float2(0.31, 0), time.x, rainRippleDisturbMat);
        float3 rainRippleNormal2 = RainRippleNormalSingle(uv + float2(0.25, 0.34),  time.y, rainRippleDisturbMat);
        float3 rainRippleNormal3 = RainRippleNormalSingle(uv + float2(0, 0.81), time.z, rainRippleDisturbMat);
        float3 rainRippleNormal4 = RainRippleNormalSingle(uv + float2(1.54, 1.74), time.w, rainRippleDisturbMat);

        float4 weights = _RainRippleIntensity * rainRippleIntensityMat - float4(0, 0.25, 0.5, 0.75);
        weights = saturate(weights * 4);
        float4 z = lerp(1, float4(rainRippleNormal1.y, rainRippleNormal2.y, rainRippleNormal3.y, rainRippleNormal4.y), weights);
        float3 normal = float3(weights.x * rainRippleNormal1.xz + 
        weights.y * rainRippleNormal2.xz + 
        weights.z * rainRippleNormal3.xz + 
        weights.w * rainRippleNormal4.xz, z.x * z.y * z.z * z.w);
        normal = float3(normal.x, normal.z, normal.y);
        normalTotal = normalize(normal); 
    }

    #define RAIN_RIPPLE_NORMAL(normal, uv, rainRippleIntensityMat, rainRippleDisturbMat, rainRippleDensityMat) RainRippleNormal(normal, uv, rainRippleIntensityMat, rainRippleDisturbMat, rainRippleDensityMat)
#else
    #define RAIN_RIPPLE_NORMAL(normal, uv, rainRippleIntensityMat, rainRippleDisturbMat, rainRippleDensityMat) 
#endif