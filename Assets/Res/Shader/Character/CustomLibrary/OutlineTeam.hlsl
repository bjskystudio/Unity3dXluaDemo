    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
    #include "Assets/Res/Shader/Character/CustomLibrary/S_Character_Properties.hlsl"  

    struct appdata
    {
        float4 vertex : POSITION;
        float4 tangent : TANGENT;
        float3 normal : NORMAL;
		float4 smoothNormal:COLOR;
        float2 texcoord : TEXCOORD0; 
        float2 texcoord1 : TEXCOORD1; 
        float2 texcoord2 : TEXCOORD2; 
    };

    struct v2f
    {
        float4 pos : SV_POSITION;
        float2 uv : TEXCOORD0;
		float4 smoothNormal:TEXCOORD1;
    };


    v2f OutlineVert(appdata v)
    {
        v2f o;

        float4 vert;
        float4x4 mv = mul(UNITY_MATRIX_V, unity_ObjectToWorld);
        float4 viewPos = mul(mv,v.vertex);//UnityObjectToViewPos(v.vertex);
		vert=viewPos;
        //vert = viewPos / viewPos.w;
        //float3 viewDir = normalize(vert.xyz);

        //float3 offset = _MaxOutlineZOffset * normalize(vert.xyz) * _Scale * lerp((colorA - 0.75) * 4, 0, step(colorA, 0.75));

        //float s = -(viewPos.z / unity_CameraProjection[1].y / 0.01);
        //float power = pow(s, 0.5);


        float3 smoothNormal=v.smoothNormal.xyz  *2  -1;

//float3 worldNormal=normalize(mul(unity_ObjectToWorld,v.normal));
//float3 worldTangent=normalize(mul(unity_ObjectToWorld,v.tangent));
//float3 worldBNormal=normalize(cross(worldNormal,worldTangent)*v.tangent.w);
float3 bitangent=cross(v.normal.xyz,v.tangent.xyz)*v.tangent.w;
float3x3 tangentToModel=float3x3(
v.tangent.x,bitangent.x,v.normal.x,
v.tangent.y,bitangent.y,v.normal.y,
v.tangent.z,bitangent.z,v.normal.z
);
float3 modelSmoothNormal=mul(tangentToModel,smoothNormal);

o.smoothNormal=float4(modelSmoothNormal,1);
        float3 t = mul((float3x3)mv,lerp(v.normal,modelSmoothNormal,_OutlineIFSmooth));
        t.z = 0.01;
        t = normalize(t);

        float3 offset  = 0;
        offset +=(min(min(_OutlineWidth,abs(viewPos.z)),_OutlineScale)* (v.smoothNormal.a)*_OutlineWidth*t)/1000.0;//abs(vert.z)*   float3(t.xyz, 0)
        vert.xy += offset.xy;
		vert.w=1;
		//float _OutlineZ=0.001;
        vert = mul(UNITY_MATRIX_P, vert);
		vert.z=((vert.z/vert.w)*_OutlineZ )*vert.w;
        o.pos = vert;
        o.uv = v.texcoord;
        return o;
    }

    float4 OutlineFrag(v2f i) : SV_Target
    {
        float4 albedo = SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex, i.uv);
clip(_OutlineOpen-0.1);
                float finalAlpha = min(_AlphaCtrl*_Color.a*albedo.a, 1);
        half4 finalColor = albedo * _OutlineColor;
        finalColor.a =finalAlpha;
        return finalColor;//finalColor;//i.smoothNormal;//_OutlineColor;
    }