Shader "Custom/WaterSurfaceMobile"
{
    Properties
    {
        _Color ("Water Color", Color) = (0.1,0.45,0.6,1)
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _BumpMap ("Normal Map", 2D) = "bump" {}
        _Tiling ("UV Tiling", Vector) = (2,2,0,0)
        _WaveScale ("Wave Scale", Float) = 1.0
        _WaveHeight ("Wave Height", Float) = 0.05
        _WaveSpeed ("Wave Speed", Float) = 1.0
        _FresnelPower ("Fresnel Power", Float) = 2.0
        _ReflectionStrength ("Reflection Strength", Range(0,1)) = 0.4
        _Smoothness ("Smoothness", Range(0,1)) = 0.6
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Back
        Cull Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "UnityCG.cginc"
            
            sampler2D _MainTex;
            sampler2D _BumpMap;
            float4 _MainTex_ST;
            float4 _BumpMap_ST;
            fixed4 _Color;
            float4 _Tiling;
            float _WaveScale;
            float _WaveHeight;
            float _WaveSpeed;
            float _FresnelPower;
            float _ReflectionStrength;
            float _Smoothness;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float3 worldNormal : TEXCOORD1;
                float3 viewDir : TEXCOORD2;
                float4 pos : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                float t = _Time.y * _WaveSpeed;

                float wave = sin((worldPos.x + t) * _WaveScale) + cos((worldPos.z - t) * _WaveScale);
                wave *= 0.5 * _WaveHeight;
                v.vertex.y += wave;

                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex) * _Tiling.xy;

                float3 worldNormal = UnityObjectToWorldNormal(v.normal);
                o.worldNormal = worldNormal;
                o.viewDir = normalize(_WorldSpaceCameraPos - worldPos);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv) * _Color;
                fixed3 normalTex = UnpackNormal(tex2D(_BumpMap, i.uv));
                fixed3 n = normalize(i.worldNormal + normalTex * 0.2);
                
                float fresnel = pow(1.0 - saturate(dot(i.viewDir, n)), _FresnelPower);
                fixed3 reflection = lerp(col.rgb, fixed3(0.5,0.7,0.9), fresnel * _ReflectionStrength);

                col.rgb = reflection;
                col.a = 0.6 + fresnel * 0.3;
                return col;
            }
            ENDCG
        }
    }
    FallBack Off
}
