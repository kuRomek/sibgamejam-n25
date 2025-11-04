Shader "Custom/RevealEffectURP"
{
    Properties
    {
        _MainTex ("Base Texture", 2D) = "white" {}
        _NoiseTex ("Noise Texture", 2D) = "gray" {}
        _Reveal ("Reveal Amount", Range(0,1)) = 0
        _EdgeWidth ("Edge Width", Range(0.01,0.5)) = 0.1
        _EdgeColor ("Edge Color", Color) = (0.2,0.6,1,1)
        _MainColor ("Main Color", Color) = (1,1,1,1)
        _EmissionStrength ("Edge Glow", Range(0,10)) = 3
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _NoiseTex;
            float4 _MainTex_ST;
            float4 _NoiseTex_ST;

            float _Reveal;
            float _EdgeWidth;
            float4 _EdgeColor;
            float4 _MainColor;
            float _EmissionStrength;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float n = tex2D(_NoiseTex, uv).r;

                // Reveal logic — плавное проявление
                float mask = smoothstep(_Reveal, _Reveal + _EdgeWidth, n);
                float edge = smoothstep(_Reveal - _EdgeWidth, _Reveal, n) - mask;

                fixed4 baseCol = tex2D(_MainTex, uv) * _MainColor;
                float3 edgeGlow = _EdgeColor.rgb * edge * _EmissionStrength;

                float3 finalColor = baseCol.rgb + edgeGlow;
                float alpha = baseCol.a * mask;

                return fixed4(finalColor, alpha);
            }
            ENDCG
        }
    }

    FallBack "Unlit/Transparent"
}
