// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Snk/Character/PBR/S_PBR_Character_Hair" 
{
    Properties
    {
		[KeywordEnum(OFF,ON)]SHADOWRECEP("Shadow阴影接受开启/关闭",Float) = 0 
        
        [Space(10)][Header(Base)]
		_Color("混合色", Color) = (1,1,1,1)
        [NoScaleOffset]_MainTex("Base Map(RGB基本贴图,A通道为半透明)",2D) = "white" {}
        _AOMulti("AO强度",Range(0,1)) = 0
                        
        [Space(10)][Header(NomalMap)]
		_BumpMap("Normal Map(法线贴图)",2D) = "bump"{}
		_BumpScale("Normal Multi(法线强度)",float) = 1

        [Space(10)][Header(Specular)]
        [HDR]_SpecCol("镜面高光颜色",color) = (1,1,1,1)
		[NoScaleOffset]_LimMask("R为Hiar形状贴图,G为Hiar范围遮罩,,B通道为整体AO,A通道为自发光遮罩",2D) = "white"{}
		_Specularoffuse("镜面高光偏移",Range(-10,10)) = 1 //金属强度
		_SpecularThreshod("镜面高光范围",Range(0,500)) = 1 //光滑强度
        _SpecularSize("镜面高光重复度",Range(0,10)) = 1
        _SpecularMullit("镜面高光强度",Range(0,10)) = 0


		[Space(10)][Header(RimLight)]
		//[HDR]_RimCol("RimCol(边光颜色)", Color) = (0,0,0,0)
        _RimThreshod("RimThreshod(边光阈值)",Range(0,1)) = 0
		_RimSmooth ("RimSmooth(边光平滑)",Range(0,1)) = 0.5
		_RimFadeVector("边光衰减方向xy,z衰减强度,w=1开启此功能",Vector)=(0,0,0,0)

		[Space(10)][Header(EmissLight)]
        [HDR]_EmissCol("_EmissCol(自发光颜色)",Color) = (1,1,1,1)
        _EmissThreshod("_EmissThreshod(自发光阈值)",Range(0,1)) = 0

        //渐隐效果
        [HideInInspector]_AlphaCtrl("_AlphaCtrl", Float) = 1
        [HideInInspector]_ZWriteCtrl("_ZWriteCtrl", Float) = 1

        [Space(20)]
        [Space(0)][Header(BeHit)]
		_BeHitMaskTex("受击图：R：控制区域", 2D) = "white" {}
		_BeHitColor("受击颜色", Color) = (1, 1, 1, 1)
		_BeHitThreshodAdd("受击阈值(叠加类型)", Range(0, 1)) = 0
		_BeHitThreshodReplace("受击阈值(替换类型)", Range(0, 1)) = 0
		_BeHitStrength("受击强度", float) = 1
		
		[Space(10)]
		_BeHitRimColor("受击边缘光颜色", Color) = (0, 0, 0, 1)
		_BeHitRimThreshod("受击边缘阈值", Range(0, 1)) = 0.5
		_BeHitRimPower("受击边缘区域范围", Float) = 8
		_BeHitRimStrength("受击边缘光颜色强度", Float) = 1
		_BeHitRimSmooth("受击边缘光平滑", Range(0.001, 1)) = 0.1

		[Space(8)][Header(OutLine)]
        [Enum(Off,0,On,1)]_OutlineOpen("_OutlineOpen", Float) = 1
        _OutlineColor("OutlineColor(描边颜色)",Color) = (0.5,0.5,0.5,0)
        _OutlineWidth("描边宽度)",Range(0,50)) =50
        _OutlineZ("_OutlineZ(描边深度偏移)",Float) =1
        _OutlineScale("_OutlineScale(描边宽度缩放系数)",Range(0,5)) =0.1
        _OutlineIFSmooth("是否使用平滑的法线",Range(0,1)) =0

        [Space(20)]
		[Enum(UnityEngine.Rendering.CompareFunction)] _zTest("ZTest", Float) = 4
		[Enum(UnityEngine.Rendering.CullMode)] _cull("Cull Mode", Float) = 2
		[Enum(UnityEngine.Rendering.BlendMode)] _srcBlend("Src Blend Mode", Float) = 5
		[Enum(UnityEngine.Rendering.BlendMode)] _dstBlend("Dst Blend Mode", Float) = 10
		[Enum(UnityEngine.Rendering.BlendMode)] _srcAlphaBlend("Src Alpha Blend Mode", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] _dstAlphaBlend("Dst Alpha Blend Mode", Float) = 10        
         
        [Space(20)]
        //残影模板测试
        [HideInInspector]
        _StencilRef("StencilRef", Float) = 40

        [HideInInspector]
        [Enum(UnityEngine.Rendering.CompareFunction)] 
        _StencilComp("StencilComp", Float) = 8 

        [HideInInspector]
        [Enum(UnityEngine.Rendering.StencilOp)] 
        _StencilOp("StencilOp", Float) = 2 
        
        [HideInInspector]
        _StencilReadMask("ReadMask", Float) = 255 

        [HideInInspector]
        _StencilWriteMask("WriteMask", Float) = 255
    }


    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue" = "Geometry+200"}
        LOD 100


        Pass
        {
            Tags {"LightMode" = "UniversalForward"}
			Blend[_srcBlend][_dstBlend],[_srcAlphaBlend][_dstAlphaBlend]
			ZTest[_zTest]
			ZWrite[_ZWriteCtrl]
            Cull[_cull]
            //Stencil
            // {
            //     Ref 2 //参考值
            //     ReadMask  255
            //     WriteMask 255
            //     Comp Always
            //     Pass Replace
            //     ZFail Replace
            // }
            Stencil
            { 
                Ref [_StencilRef] 
                Comp[_StencilComp] 
                Pass[_StencilOp] 
                ReadMask[_StencilReadMask] 
                WriteMask[_StencilWriteMask] 
            }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag 
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Assets/Res/Shader/Character/CustomLibrary/S_Character_Properties.hlsl" 
            #include "Assets/Res/Shader/Character/CustomLibrary/CustomAddLighting.hlsl"//多光源 
			#include "Assets/Res/Shader/Character/CustomLibrary/CustomBRDF.hlsl"

            #pragma multi_compile CUSTOMLIGHT_OFF CUSTOMLIGHT_ON
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile _ _SHADOWS_SOFT
            #pragma shader_feature SHADOWRECEP_OFF SHADOWRECEP_ON //阴影接受开关
            #pragma multi_compile _ BEHITREPLACE
            #pragma multi_compile _ BEHIT_RIM
            #pragma multi_compile _ _ADDITIONAL_LIGHTS

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
				float2 texcoord1 : TEXCOORD1;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                half3 worldNormal : NORMAL;
                half3 worldPos : TEXCOORD1;
                half3 viewPos : TEXCOORD2;
				half4 TtoW0 : TEXCOORD3;
				half4 TtoW1 : TEXCOORD4;
				half4 TtoW2 : TEXCOORD5;      
                half3 worldTangent : TANGENT;
                half3 worldBinormal : TEXCOORD6;
                half3 CustomPos : TEXCOORD7;
                half2 uv2 : TEXCOORD8;
            };
            v2f vert (appdata v)
            {
                v2f o =  (v2f)0;
                o.pos = TransformObjectToHClip(v.vertex.xyz);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldNormal = TransformObjectToWorldNormal(v.normal);
                o.worldPos = TransformObjectToWorld(v.vertex.xyz);
				o.viewPos = TransformWorldToView(TransformObjectToWorld(v.vertex.xyz));

				o.worldTangent = TransformObjectToWorldDir(v.tangent.xyz);
				o.worldBinormal = cross(o.worldNormal,normalize(o.worldTangent)) * v.tangent.w;

				o.TtoW0 = float4(o.worldTangent.x,o.worldBinormal.x,o.worldNormal.x,o.worldPos.x);
				o.TtoW1 = float4(o.worldTangent.y,o.worldBinormal.y,o.worldNormal.y,o.worldPos.y);
				o.TtoW2 = float4(o.worldTangent.z,o.worldBinormal.z,o.worldNormal.z,o.worldPos.z);
                o.uv2 = v.texcoord1;
                return o;
            }


            float4 frag (v2f i) : SV_Target
            {
            #ifdef BEHITREPLACE
                half4 finalColor = 0;
                #ifdef _ADDITIONAL_LIGHTS
                	float3 worldPos = float3(i.TtoW0.w,i.TtoW1.w,i.TtoW2.w);
                    float3 worldViewDir = normalize(_WorldSpaceCameraPos - worldPos);
                    float3 worldNormal = normalize(i.worldNormal);

                    //边缘光
                    half NdotV = saturate(dot(worldNormal, worldViewDir));
                    half rimScale = smoothstep(_BeHitRimThreshod - _BeHitRimSmooth * 0.5 , _BeHitRimThreshod + _BeHitRimSmooth * 0.5, pow(1 - NdotV, 1 /_BeHitRimPower));
                    half3 rimColor = _BeHitRimColor.rgb * rimScale * _BeHitRimStrength;

                    //受击颜色
                    half3 diffuseColor = 0;
                    half4 beHitMaskCol = SAMPLE_TEXTURE2D(_BeHitMaskTex, sampler_BeHitMaskTex, i.uv);
                    uint pixelLightCount = GetAdditionalLightsCount();
                    for (uint lightIndex = 0u; lightIndex < pixelLightCount; ++lightIndex)
                    {
                        Light light = GetAdditionalLight(lightIndex, worldPos);
                        half NdotL = saturate(dot(light.direction, worldNormal));
                        half3 attenuatedLightColor = light.color * light.distanceAttenuation;
                        diffuseColor +=_BeHitStrength * beHitMaskCol.r * _BeHitColor * attenuatedLightColor * step(_BeHitThreshodReplace, NdotL);
                    }

                    finalColor.rgb = diffuseColor + rimColor;
                    finalColor.a = 1;
                #endif

                return finalColor;
            #else
                 //世界坐标 、切线 、副法线
				float3 worldPos = float3(i.TtoW0.w,i.TtoW1.w,i.TtoW2.w);

                //纹理采样
                float4 albedo = SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex, i.uv);
                float4 PbrMap = SAMPLE_TEXTURE2D(_LimMask,sampler_LimMask,i.uv2);
				float3 BumpMap = UnpackNormal(SAMPLE_TEXTURE2D(_BumpMap,sampler_BumpMap,i.uv));
				half3 worldNormal = ComputeBumpNormal(BumpMap, _BumpScale, i.TtoW0, i.TtoW1, i.TtoW2);
                half3 Viewdir = normalize(_WorldSpaceCameraPos - worldPos);
				half3 refDir = reflect(-Viewdir,worldNormal);//世界空间下的反射方向
                float4 SHADOW_COORDS = TransformWorldToShadowCoord(worldPos);//计算shadow位置
                Light mainlight = GetMainLight(SHADOW_COORDS);
                half3 LightDir =normalize(mainlight.direction); 

				#ifdef CUSTOMLIGHT_ON
                    LightDir =normalize(-1*_CustomAvatarLightVec.xyz);
				#endif
				half3 Halfdir = normalize(LightDir + Viewdir); 

				//LightModeTerm
                half NdotL = saturate(dot(worldNormal,LightDir));
                half NdotH = saturate(dot(worldNormal,Halfdir));
                half NdotV = saturate(dot(i.worldNormal,Viewdir));
				half LdotH = saturate(dot(LightDir,Halfdir));

				//Light
                float3 Ambient = SampleSH(worldPos).rgb * albedo.rgb;
                float3 LightColor = mainlight.color.rgb;
				#ifdef CUSTOMLIGHT_ON
                LightColor =_CustomAvatarLightColor;
				#endif     
                #ifdef SHADOWRECEP_ON //阴影开关
                    half atten = mainlight.shadowAttenuation;
                #else
                    half atten = 1;
                #endif
                
                //添加假的光照效果，只有颜色和方向
                float GNdotL = saturate(dot(worldNormal, -1 * _CustomLightGlobalDir));
                float3 CustomGlobalCol = GNdotL * _CustomLigthClobalColor * _CustomLigthClobalIntensity;

				//DiffuseTeam
                float LightTeam = NdotL * atten;
				float3 albedoColor = albedo.rgb  * LightTeam * _Color.rgb * LightColor.rgb;
                float3 DiffuseTeam = lerp(albedoColor, albedoColor + (CustomGlobalCol * albedo.rgb), _CustomLigthClobalEnable); 

				//SpecularTerm
                float3 Ts = HiarTangent(i.worldBinormal, worldNormal, PbrMap.r - _Specularoffuse);
                float specMain = HiarSpecular(Ts, Viewdir, LightDir, _SpecularThreshod) * PbrMap.g;
                float3 SpecularTerm = _SpecCol.rgb * specMain * _SpecularMullit * LightTeam * albedo.rgb;

                //边光
                float3 RimLight;
				half3 viewNormal=mul((float3x3)UNITY_MATRIX_V,worldNormal);
				float factor=step(_RimFadeVector.w,0)+saturate(dot(viewNormal.xz,_RimFadeVector.xy))*(1-clamp(0,1,_RimFadeVector.z));
                #ifdef BEHIT_RIM
                    half NV = saturate(dot(worldNormal, Viewdir));
					half rimScale = pow(1 - NV, 1 /_BeHitRimPower);
					rimScale = smoothstep(_BeHitRimThreshod - _BeHitRimSmooth * 0.5 , _BeHitRimThreshod + _BeHitRimSmooth * 0.5, rimScale);
					RimLight = _BeHitRimColor.rgb * rimScale * _BeHitRimStrength;
                #else
                    RimLight = factor*smoothstep((1-_RimThreshod)-_RimSmooth,(1-_RimThreshod)+_RimSmooth,(1-NdotV)) * _RimCol.rgb *(1-(NdotL));
                #endif

                //自发光
                half3 Emiss; 
                Emiss = PbrMap.a*_EmissCol.rgb * _EmissThreshod;

                //漫反射最终颜色
                float3 finalColor = (SpecularTerm + DiffuseTeam ) + Ambient;
                finalColor = lerp(finalColor,finalColor*PbrMap.b,_AOMulti);  //AO
                finalColor = (finalColor  + RimLight + Emiss); // Rim +Emiss

                finalColor += CustomAddLight(albedo.rgb,worldPos,worldNormal);              

                finalColor = lerp(finalColor,  _BeHitColor.rgb * _BeHitStrength + RimLight, _BeHitThreshodAdd);
                float finalAlpha = min(_AlphaCtrl*_Color.a*albedo.a, 1);
                float4 finalRGBA = float4(finalColor, finalAlpha);
          
                return finalRGBA;
            #endif
            }
            ENDHLSL
        }

         Pass
         {
             Name "Outline"
             Tags{ "Queue" ="Transparent+10" "RenderType" = "Transparent" "IgnoreProjector" = "True" }
		 	//LOD 100
             Stencil
             {
                 Ref 1 //参考值
                 ReadMask  255
                 WriteMask 255
                 Comp Greater
                 Pass Keep
                 ZFail Keep
             }
			ZTest Less//Always//Less
		 	ZWrite On// On
		 	Cull Front//Front
             Blend   [_srcBlend][_dstBlend],[_srcAlphaBlend][_dstAlphaBlend]//one zero//[_srcBlend][_dstBlend],[_srcAlphaBlend][_dstAlphaBlend]
             Tags{"LightMode" = "SRPDefaultUnlit"}//SRPDefaultUnlit
		 	HLSLPROGRAM
             #include "Assets/Res/Shader/Character/CustomLibrary/OutlineTeam.hlsl"
             #pragma vertex OutlineVert
             #pragma fragment OutlineFrag
		 	ENDHLSL
         }

        UsePass "EngineCenter/ShadowCaster/ShadowCaster"
        UsePass "EngineCenter/DepthOnly/DepthOnly"
    }
}
