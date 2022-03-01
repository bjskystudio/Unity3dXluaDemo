    //在逐像素着色器里算的多光源
    
    inline half3 LightingHalfLambert(half3 lightColor, half3 lightDir, half3 normal)
    {
        half NdotL = saturate(dot(normal, lightDir))*0.5+0.5;
        return lightColor * NdotL;
    }

    inline half3 EmissLighting(half3 lightColor, half3 diffues)
    {
        return lightColor * diffues;
    }


    inline half3 CustomAddLight(half3 diffues , half3 worldPos , half3 worldNormal)
    {
        half3 tempCol = 0;
    #ifdef _ADDITIONAL_LIGHTS
        uint pixelLightCount = GetAdditionalLightsCount();
        for(uint lightIndex = 0; lightIndex < pixelLightCount; ++lightIndex)
        {
            Light light = GetAdditionalLight(lightIndex,worldPos);
            half3 attenuatedLightColor = light.color * (light.distanceAttenuation * light.shadowAttenuation);
            tempCol += diffues * LightingLambert(attenuatedLightColor,light.direction,worldNormal);
        }
    #endif
        return tempCol;
    }

    inline half3 CustomAddSceneLight(half3 diffues ,half3 albedo, half3 worldPos)
    {
    #ifdef _ADDITIONAL_LIGHTS
        uint pixelLightCount = GetAdditionalLightsCount();
        for(uint lightIndex = 0; lightIndex < pixelLightCount; ++lightIndex)
        {
            Light light = GetAdditionalLight(lightIndex,worldPos);
            half3 attenuatedLightColor = light.color * (light.distanceAttenuation * light.shadowAttenuation);
            diffues +=  albedo * EmissLighting(attenuatedLightColor,albedo);
        }
    #endif  
        return diffues;
    } 
