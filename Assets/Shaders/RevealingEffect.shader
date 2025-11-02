Shader "Custom/RevealEffectURP"
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
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "RenderPipeline"="UniversalRenderPipeline" }
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off

        Pass
        {
            Name "UniversalForward"
            Tags { "LightMode"="UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"

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
            float _DepthFadeDistance;

            Varyings vert (Attributes v)
            {
                Varyings o;
                o.positionHCS = TransformObjectToHClip(v.positionOS);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            half4 frag (Varyings i) : SV_Target
            {
                // base color
                float4 col = tex2D(_MainTex, i.uv) * _MainColor;

                // noise mask
                float n = tex2D(_NoiseTex, i.uv).r;

                // reveal mask — работает "прямо"
                float mask = smoothstep(_Reveal, _Reveal + _EdgeWidth, n);
                float edge = smoothstep(_Reveal - _EdgeWidth, _Reveal, n) - mask;

                // final color
                float3 finalColor = col.rgb + _EdgeColor.rgb * edge * _EmissionStrength;
                float alpha = col.a * mask;

                return float4(finalColor, alpha);
            }
            ENDHLSL
        }
    }
}
