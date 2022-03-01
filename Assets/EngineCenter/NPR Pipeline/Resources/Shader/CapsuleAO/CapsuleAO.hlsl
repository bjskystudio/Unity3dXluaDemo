#ifndef CAPSULE_AO_INCLUDED
#define CAPSULE_AO_INCLUDED

#ifdef _CAPSULE_AO_ON
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
    float4 _AOCapsuleInfoArray[32]; 
    int _AOCapsuleInfoNum;
    half _AOFieldAngle;
    float _AOStrength;
    half3 _AOLightDir;

    float AcosFast(float x)
    {
        float y = abs(x);
        float p = -0.1565827 * y + 1.570796;
        p *= sqrt(1.0 - y);
        return x >= 0.0 ? p : PI - p;
    }

    float AcosFastPositive(float x) 
    {
        float p = -0.1565827 * x + 1.570796;
        return p * sqrt(1.0 - x);
    }

    float SphericalCapsIntersection(float cosCap1, float cosCap2, float cap2, float cosDistance) 
    {
        float r1 = AcosFastPositive(cosCap1);
        float r2 = cap2;
        float d  = AcosFast(cosDistance);

        if (min(r1, r2) <= max(r1, r2) - d) {
            return 1.0 - max(cosCap1, cosCap2);
        } else if (r1 + r2 <= d) {
            return 0.0;
        }

        float delta = abs(r1 - r2);
        float x = 1.0 - saturate((d - delta) / max(r1 + r2 - delta, 0.0001));
        float area = x * x * (-2.0 * x + 3.0);
        return area * (1.0 - max(cosCap1, cosCap2));
    }

    float DirectionalOcclusionSphere(float3 pos, float4 sphere, half3 lightDir, half angle)
    {
        float3 occluder = sphere.xyz - pos;
        float occluderLength2 = dot(occluder, occluder);
        half3 occluderDir = occluder * rsqrt(occluderLength2);

        float cosPhi = dot(occluderDir, lightDir.xyz);
        float cosTheta = sqrt(occluderLength2 / ((sphere.w * sphere.w) + occluderLength2));
        float cosCone = cos(angle);

        return 1.0 - SphericalCapsIntersection(cosTheta, cosCone, angle, cosPhi) / (1.0 - cosCone);
    }

    float DirectionalOcclusionCapsule(float3 pos, float3 capsuleA, float3 capsuleB, float capsuleRadius, float3 lightDir, half angle) 
    {
        float3 Ld = capsuleB - capsuleA;
        float3 L0 = capsuleA - pos;
        float a = dot(lightDir.xyz, Ld);
        float t = saturate(dot(L0, a * lightDir.xyz - Ld) / (dot(Ld, Ld) - a * a));
        float3 posToRay = capsuleA + t * Ld;

        return DirectionalOcclusionSphere(pos, float4(posToRay, capsuleRadius), lightDir.xyz, angle);
    }

    float CapsuleAO(float3 posWS)
    {
        float ao = 1;
        for(int k = 0; k < _AOCapsuleInfoNum; k++)
        {
            ao = min(ao, DirectionalOcclusionSphere(posWS, _AOCapsuleInfoArray[k], _AOLightDir, _AOFieldAngle));
            //#if SPHERE_AO
            //    ao = min(ao, DirectionalOcclusionSphere(posWS, _CapsuleInfoArray[k], _MainLightPosition, _FieldAngle));
            //#elif CAPSULE_AO
            //    ao = min(ao, DirectionalOcclusionCapsule(posWS, _CapsuleInfoArray[k].xyz, _CapsuleInfoArray[k].xyz + float3(0, 1, 0), _CapsuleInfoArray[k].w, _MainLightPosition, _FieldAngle));
            //#endif
        }

        float4 shadowCoord = TransformWorldToShadowCoord(posWS.xyz);
        Light light = GetMainLight(shadowCoord);
        ao = lerp(_AOStrength, 1, ao);
        //实现了在阴影处 使用capsuleao，而在光亮处只有不受capsuleao影响
        ao = lerp(ao, 1, step(0.8, light.shadowAttenuation));
        return ao;
    }

    #define CAPSULE_AO(ao, posWS) ao = CapsuleAO(posWS); 
#else
    #define CAPSULE_AO(ao, posWS) ao = 1;
#endif

#endif