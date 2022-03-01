Shader "Snk/Scene/Snk_Scene_Object_M"
{
    Properties
    {   
		[Toggle(SHADOWRECEP)] SHADOWRECEP("Shadow阴影接受开启/关闭",Float) = 0 
		_LightToggle("0为开启无光模式，1为开启光照模式",Float) = 1
        
        [Header(Base)]
		[HDR]_Color("混合色", Color) = (1,1,1,1)
        _MainTex("Base Map(RGB基本贴图)",2D) = "white" {}
		_glintingFactor("GlintingFactor闪烁系数", Float) = 0
        [Space(5)]
		[Toggle(SPECULARTOGGLE)] SPECULARTOGGLE("高光关闭/开启",Float) = 0 
        [HDR]_SpecCol("镜面高光颜色",color) = (1,1,1,1)
		[NoScaleOffset]_LimMask("Lim Map( 金属图在R通道,粗糙图在G通道,B通道为镜面反射遮罩)",2D) = "white"{}
		_MetallicStrength("金属度(0=非金属，1=金属)",Range(0,1)) = 0 //金属强度
		_RoughnessStrength("粗糙度(0=很粗糙,1=很光滑)",Range(0,1)) = 0 //光滑强度	

        [Header(AlphaClip)]
        [Toggle(ALPHACLIP)] ALPHACLIP("是否开启Clip", Float) = 0  
		_CutOut("CutOut", Float) = 0

        [Space(5)][Header(Shadow)]
        _ShadowColor ("阴影颜色",Color) = (0,0,0,0)
        _ShadowThreshold ("阴影强度",Range(0,3)) = 0 
        [NoScaleOffset]_FixMask("A通道为场景阴影遮罩,R通道为自发光遮罩,G通道为UV流动遮罩",2D) = "black" {}
	               	        
		[Space(5)][Header(EmissLight)]
        [HDR]_EmissCol("_EmissCol(0.1>第一层自发光颜色<0.4)",Color) = (1,1,1,1)
        _EmissThreshod("_EmissThreshod(第一层自发光强度)",Range(0,1)) = 0
        [HDR]_TwoEmissCol("_TwoEmissCol(0.4>第二层自发光颜色<0.7)",Color) = (1,1,1,1)
        _TwoEmissThreshod("_TwoEmissThreshod(第二层自发光强度)",Range(0,1)) = 0
        [HDR]_ThreeEmissCol("_ThreeEmissCol(0.7>第三层自发光颜色=1)",Color) = (1,1,1,1)
        _ThreeEmissThreshod("_ThreeEmissThreshod(第三层自发光强度)",Range(0,1)) = 0

        [Space(20)][Header(UVliudon)]
		_SpeedX("X方向流动", Float) = 0.0
		_SpeedY("Y方向流动", Float) = 0.0

        [Header(Reflection)]
		[Toggle(REFLECTION)] REFLECTION("是否开启场景镜面反射", Float) = 0
        _ReflectionTex("ReflectionTex", 2D) = "white"{}
        _ReflectionIntensity("Reflection Intensity", Range(0,1)) = 1        


        [Header(FogColor)]
		[Space(10)]
		[HideInInspector]_FogDistColor_Single("_FogDistColor_Single", Color) = (1, 1, 1, 1)
		[HideInInspector]_FogHeightColor_Single("_FogHeightColor_Single", Color) = (1, 1, 1, 1)
		[Header(FogDisturb)]
		[Space(10)]
		[HideInInspector]_FogDisturbTex_Single("_FogDisturbTex_Single", 2D) = "white"{}
		[HideInInspector]_FogDisturbIntensity_Single("_FogDisturbIntensity_Single", Float) = 0
		[HideInInspector]_FogDisturbSpeedX_Single("_FogDisturbSpeedX_Single", Range(-2, 2)) = 0
		[HideInInspector]_FogDisturbSpeedY_Single("_FogDisturbSpeedY_Single", Range(-2, 2)) = 0
		[Header(FogDist)]
		[Space(10)]
		[HideInInspector]_FogDistDensity_Single("_FogDistDensity_Single", Range(0, 10)) = 1
		[HideInInspector]_FogDistIntensity_Single("_FogDistIntensity_Single", Float) = 1
		[Header(FogHeight)]
		[Space(10)]
		[HideInInspector]_FogHeightDensity_Single("_FogHeightDensity_Single", Range(0, 50)) = 1
		[HideInInspector]_FogHeightDistDensity_Single("_FogHeightDistDensity_Single", Range(0, 10)) = 0
		[HideInInspector]_FogHeightIntensity_Single("_FogHeightIntensity_Single", Float) = 1
		[HideInInspector]_FogHeightBase_Single("_FogHeightBase_Single", Float) = 0
		[Space(10)]
		[HideInInspector]_FogDistOffset_Single("_FogDistOffset_Single", Float) = 0	
        [Space(10)]
        [Header(Rain)]
        _RainDensity("_RainDensity", Range(0, 5)) = 0.2
        _RainIntensity("_RainIntensity", Float) = 10

        [Space(20)]
		[Enum(UnityEngine.Rendering.CompareFunction)] _zTest("ZTest", Float) = 4
		[Enum(UnityEngine.Rendering.BlendMode)] _srcBlend("Src Blend Mode", Float) = 5
		[Enum(UnityEngine.Rendering.BlendMode)] _dstBlend("Dst Blend Mode", Float) = 10
		[Enum(UnityEngine.Rendering.BlendMode)] _srcAlphaBlend("Src Alpha Blend Mode", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] _dstAlphaBlend("Dst Alpha Blend Mode", Float) = 10    
        
        [Space(20)]
        _RoisMask("噪声遮罩", 2D) = "white" {}
        _SpeedX("X方向速度值",Float) = 0
        _SpeedY("Y方向速度值",Float) = 0
        _Nois("混合颜色",Range(0,1)) = 0

        [Space(20)]
        _OutLineColor("OutlineColor(描边颜色)",Color) = (0,0,0,0)
        _OutlineWidth("OutlineWidth(描边宽度)",Range(0,1)) = 0

        [HideInInspector]_glintingColor("颜色",Color) = (1,1,1,1)
        _glintingColorFactor("强度",Float)  = 0        
    }


    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue" = "Geometry+100"}
        LOD 100
 
        Pass
        {
            Tags {"LightMode" = "UniversalForward"}
			Blend[_srcBlend][_dstBlend],[_srcAlphaBlend][_dstAlphaBlend]
			ZTest[_zTest]
            
			ZWrite On
			Cull off

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Assets/Res/Shader/Scene/CustomLibrary/Scene_Properties.hlsl"
            #include "Assets/Res/Shader/Character/CustomLibrary/CustomAddLighting.hlsl"//多光源
            #include "Assets/EngineCenter/FogBetter/Shader/FogBetter.hlsl"  //自定义Fog
			#include "Assets/Res/Shader/Character/CustomLibrary/CustomBRDF.hlsl"
            #include "Assets/EngineCenter/WeatherSystem/Asset/Rain/RainRipple/Shader/RainRipple.hlsl"

            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile _ _SHADOWS_SOFT
            #pragma multi_compile _ LIGHTMAP_ON  //Lightmap 
            #pragma multi_compile _ _MIXED_LIGHTING_SUBTRACTIVE
            #pragma multi_compile _ _FOG_BETTER_ON  //自定义Fog
            #pragma multi_compile _ _FOG_BETTER_SINGLE_ON 
            #pragma multi_compile _ _RAIN_ON 

            #pragma shader_feature SHADOWRECEP //阴影接受开关
            #pragma shader_feature ALPHACLIP
            #pragma shader_feature REFLECTION  //镜面反射
            #pragma shader_feature SPECULARTOGGLE //镜面高光

           

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float2 lightmapUV : TEXCOORD1; //输入LightmapUV
                float3 normal : NORMAL;
              //  float2 FixUV : TEXCOORD2;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldNormal : NORMAL;
                float2 uv : TEXCOORD0;
                DECLARE_LIGHTMAP_OR_SH(lightmapUV, vertexSH, 2); //输出lightmapUV 和 vertex
                float3 worldPos : TEXCOORD3;
                //float fogCoord  : TEXCOORD2; //Fog 
                #ifdef REFLECTION 
                    float4 screenPos : TEXCOORD4; //取屏幕空间位置
                #endif
                COORDS_FOG_BETTER(5) 
                float2 FixUV : TEXCOORD6;
            };

            

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = TransformObjectToHClip(v.vertex.xyz);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.FixUV = TRANSFORM_TEX(v.uv, _RoisMask);
                o.worldPos = TransformObjectToWorld(v.vertex.xyz);
                o.worldNormal = TransformObjectToWorldNormal(v.normal);
                //o.fogCoord = ComputeFogFactor(o.pos.z);

                //镜面反射
                #ifdef REFLECTION 
                    o.screenPos = ComputeScreenPos(o.pos);
                #endif

                //输出LightmapUV和vertex
                OUTPUT_LIGHTMAP_UV(v.lightmapUV, unity_LightmapST, o.lightmapUV);
                OUTPUT_SH(o.worldNormal.xyz, o.vertexSH);
                //Fog
                TRANSFER_FOG_BETTER(o,v.uv);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                //纹理采样
                float4 LimMap = SAMPLE_TEXTURE2D(_LimMask,sampler_LimMask,i.uv);
                float4 FixMask = SAMPLE_TEXTURE2D(_FixMask,sampler_FixMask, i.uv);             
               
                //UV流动
                float NoisX = _SpeedX * _Time.x;
                float NoisY = _SpeedY * _Time.x;
                float SpeedX = _Time.y*_SpeedX* FixMask.g;
                float SpeedY = _Time.y*_SpeedY* FixMask.g; 
                float4 albedo = SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex, float2(i.uv.x + SpeedX, i.uv.y + SpeedY));
                float4 FixMaskUV = SAMPLE_TEXTURE2D(_FixMask, sampler_FixMask, float2(i.uv.x + SpeedX, i.uv.y + SpeedY));
                
                float4 RoisMaskUV = SAMPLE_TEXTURE2D(_RoisMask, sampler_RoisMask, float2(i.FixUV.x + NoisX, i.FixUV.y + NoisY));
               
                //Alpha Clip
                float Alpha = albedo.a * _Color.a;
                #ifdef ALPHACLIP
                    clip(Alpha - _CutOut);
                #endif    
                
                //LightTeam
				half3 worldPos = i.worldPos;
                float4 SHADOW_COORDS = TransformWorldToShadowCoord(worldPos);//计算shadow位置
                Light mainlight = GetMainLight(SHADOW_COORDS);
                half3 LightDir = normalize(mainlight.direction);
                half3 Viewdir = normalize(_WorldSpaceCameraPos - worldPos);
                float3 Halfdir = normalize(LightDir + Viewdir);

				//LightModeTerm
                half NdotL = saturate(dot(i.worldNormal,LightDir));
                half NdotH = saturate(dot(i.worldNormal,Halfdir));
                half NdotV = saturate(dot(i.worldNormal,Viewdir));
				half LdotH = saturate(dot(LightDir,Halfdir));

				half metallic = LimMap.r * _MetallicStrength;//金属度
				half roughness =  SmoothnessToPerceptualRoughness(LimMap.g *_RoughnessStrength);//粗糙度
                float4 ColorSpace = half4(0.04, 0.04, 0.04, 1.0 - 0.04);
				half oneMinusmetallic = (1- metallic);//计算1 - 反射率,漫反射总比率

                //Shadow、Ambient、LightCol
                float3 Ambient = SampleSH(worldPos).rgb * albedo.rgb * oneMinusmetallic;
                float3 LightColor = mainlight.color.rgb;
                half DistanceAtten =  mainlight.distanceAttenuation;
                #ifdef SHADOWRECEP //阴影开关
                    half atten = lerp(mainlight.shadowAttenuation,1,FixMask.a);
                #else
                    half atten = 1;    
                #endif

				//attenuatedLightColor & CustomShadow 
                float3 CustomShadow = lerp(_ShadowColor.rgb,1,atten);
                CustomShadow = lerp(1,CustomShadow,_ShadowThreshold);     
                half3 attenuatedLightColor = CustomShadow * DistanceAtten;

                //LightMap 
                #ifdef LIGHTMAP_ON 
                    float3 BakedGI = SAMPLE_GI(i.lightmapUV,i.vertexSH,i.worldNormal);
                    MixRealtimeAndBakedGI(mainlight, i.worldNormal, BakedGI, half4(0, 0, 0, 0));
                    float3 GI = CustomShadow * BakedGI;
                    float3 Diffuse = albedo.rgb * GI * oneMinusmetallic * _Color.rgb;
                    float3 DiffuseTeam = lerp(Diffuse, Diffuse * RoisMaskUV.rgb, _Nois);
                #else
                float3 Diffuse = albedo.rgb * lerp(1, attenuatedLightColor, _LightToggle) * oneMinusmetallic * _Color.rgb;
                float3 DiffuseTeam = lerp(Diffuse, Diffuse * RoisMaskUV.rgb, _Nois);
                #endif

				//specularTerm ON/Off
                #ifdef  SPECULARTOGGLE
                    half3 specColor = lerp(ColorSpace.rgb,albedo.rgb,metallic);
                    half V = ComputeSmithJointGGXVisibilityTerm(NdotL,NdotV,roughness);
                    half D = ComputeGGXTerm(NdotH,roughness);
                    half3 F = ComputeFresnelTerm(specColor,LdotH);	
                    half3 SpecularTerm = V * D * F * 3.14159265359; //UNITY_PI : 3.14159265359
                    SpecularTerm = max(0.001,SpecularTerm  * NdotL * attenuatedLightColor * _SpecCol.rgb);   
                #else
                    half3 SpecularTerm = 0;
                #endif             

                //自发光
                float3 Emiss; 
                float3 A =  _EmissCol.rgb * _EmissThreshod;
                float3 B =  _TwoEmissCol.rgb * _TwoEmissThreshod;
                float3 C =  _ThreeEmissCol.rgb * _ThreeEmissThreshod;
                Emiss = lerp(0,A,step(0.1,FixMaskUV.r));
                Emiss = lerp(Emiss ,B,step(0.4,FixMaskUV.r));
                Emiss = lerp(Emiss ,C,step(0.7,FixMaskUV.r));	

                 //添加多光源
                DiffuseTeam = CustomAddSceneLight(DiffuseTeam,albedo.rgb,worldPos);

                //漫反射最终颜色
                float3 finalColor =  DiffuseTeam + SpecularTerm + Emiss + Ambient;

                //镜面反射
                #ifdef REFLECTION 
                    float2 refuv = i.screenPos.xy / i.screenPos.w;
                    float3 reflectCol = SAMPLE_TEXTURE2D(_ReflectionTex, sampler_ReflectionTex, refuv.xy);
                    reflectCol = lerp(finalColor,reflectCol,LimMap.b);
                    finalColor = lerp(finalColor,reflectCol,_ReflectionIntensity);
                #endif
				
                //系统雾效果
                //finalColor = MixFog(finalColor,i.fogCoord);
                float3 glintingColor=(1-_glintingColorFactor)*(finalColor*(1-_glintingFactor))+
                _glintingColorFactor*(finalColor+_glintingColor.rgb*(1-_glintingFactor));
                float4 finalRGBA = float4(glintingColor,Alpha);
				
                //自定义雾效
                APPLY_FOG_BETTER(finalRGBA.rgb, worldPos, i.fogBetterCoord);
            
                return finalRGBA;
            }
            ENDHLSL
        }
                 Pass
	    {   
	    //Tags {"LightMode"="UniversalForwardBase"}
			 
            Cull Front
            ZWrite On
            LOD 100
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            //#include "UnityCG.cginc"
            //#include "Assets/Res/Shader/Character/CustomLibrary/S_Character_Properties.hlsl" 
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Assets/Res/Shader/Scene/CustomLibrary/Scene_Properties.hlsl"
            //#pragma shader_feature SHADOWRECEP _OutlineWidth
           

            struct a2v 
	        {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
                //float4 color : COLOR;
                //float4 tangent : TANGENT;
            };

            struct v2f
	        {
                float4 pos : SV_POSITION;
            };


            v2f vert (a2v v) 
	        {
                v2f o;
		//UNITY_INITIALIZE_OUTPUT(v2f, o);
                //o.pos = TransformObjectToWorld(float4(v.vertex.xyz + v.normal * _OutlineWidth * 0.1 ,1));//顶点沿着法线方向外扩
                
                // v.vertex.xyz += v.normal * _OutlineWidth;
                float3 worldNormal = mul(mul(UNITY_MATRIX_V, unity_ObjectToWorld),v.normal);;
                float4 vert = mul(mul(UNITY_MATRIX_V, unity_ObjectToWorld),v.vertex);
                vert.xyz += worldNormal * _OutlineWidth;
                o.pos = mul(UNITY_MATRIX_P, vert);

                //o.pos = TransformObjectToHClip(v.vertex);

                // o.pos = TransformObjectToHClip(v.vertex);
                //UNITY_INITIALIZE_OUTPUT(v2f, o);
                //o.pos = TransformObjectToHClip(float4(v.vertex.xyz + v.normal * _OutlineWidth * 0.1 ,1));//顶点沿着法线方向外扩

                return o;
            }

            half4 frag(v2f i) : SV_TARGET 
	       {       
            
                return _OutLineColor;
            
            }
            ENDHLSL
        }

        UsePass "EngineCenter/ShadowCaster/ShadowCaster"
        UsePass "EngineCenter/DepthOnly/DepthOnly" 
    }
 }
