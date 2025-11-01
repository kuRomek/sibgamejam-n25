Shader "Custom/RevealingEffect"
{
    Properties
    {
        _MainTex ("Base Texture", 2D) = "white" {}
        _NoiseTex ("Noise Texture", 2D) = "gray" {}
        _Reveal ("Reveal Amount", Range(0,1)) = 0
        _EdgeWidth ("Edge Width", Range(0.01, 0.5)) = 0.1
        _EdgeColor ("Edge Color", Color) = (0.2, 0.6, 1, 1)
        _MainColor ("Main Color", Color) = (1,1,1,1)
        _EmissionStrength ("Edge Glow", Range(0,10)) = 3
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            sampler2D _NoiseTex;
            float4 _MainTex_ST;
            float4 _NoiseTex_ST;

            float _Reveal;
            float _EdgeWidth;
            float4 _EdgeColor;
            float4 _MainColor;
            float _EmissionStrength;

            Varyings vert (Attributes v)
            {
                Varyings o;
                o.positionHCS = TransformObjectToHClip(v.positionOS);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            half4 frag (Varyings i) : SV_Target
            {
                float2 uv = i.uv;

                float noise = tex2D(_NoiseTex, uv).r;
                float revealEdge = smoothstep(_Reveal - _EdgeWidth, _Reveal, noise);

                float baseAlpha = revealEdge;
                float edgeMask = smoothstep(_Reveal, _Reveal + _EdgeWidth, noise) - revealEdge;

                float4 baseColor = tex2D(_MainTex, uv) * _MainColor;
                float3 edgeGlow = _EdgeColor.rgb * edgeMask * _EmissionStrength;

                float3 finalColor = baseColor.rgb + edgeGlow;

                return float4(finalColor, baseAlpha * baseColor.a);
            }
            ENDHLSL
        }
    }

    FallBack "Transparent/Diffuse"
}
