Shader "SaintSeiya2/Scene/FogPlaneURP"
{
    Properties
    {
        [HDR]_Color("_Color", Color) = (1, 1, 1, 1)
        _MainTex("_MainTex", 2D) = "white"{}
        [Toggle]_SOFT_PARTICLE("_SOFT_PARTICLE", Float) = 0
        _InvFade("_InvFade", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent"}
        LOD 100

        Pass
        {
            Tags {"LightMode" = "UniversalForward"}
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            HLSLPROGRAM
            #pragma multi_compile_instancings
            
            #include "S_Scene_Input.hlsl"
            #include "S_FogPlane.hlsl"

            #pragma vertex SceneFogPlanePassVertex
            #pragma fragment SceneFogPlanePassFragment

            #pragma multi_compile_SOFT_PARTICLE_ON

            ENDHLSL
        }
    }
}
