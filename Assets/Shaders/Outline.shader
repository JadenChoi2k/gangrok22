Shader "Custom/Outline"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _OutlineColor("Outline Color", Color) = (0, 1, 0, 1) // 초록색으로 설정
        _OutlineWidth("Outline Width", Range(0.005, 0.05)) = 0.0025 // 두께를 증가
        _BlinkSpeed("Blink Speed", Range(0.1, 5.0)) = 1.0 // 깜빡이는 속도
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent"}
        LOD 200

        Cull Front
        ZWrite Off
        CGPROGRAM
        #pragma surface surf NoLight vertex:vert noshadow noambient
        #pragma target 3.0

        float4 _OutlineColor;
        float _OutlineWidth;
        float _BlinkSpeed;

        void vert(inout appdata_full v) {
v.vertex.xyz += v.normal.xyz * _OutlineWidth;
        }

        struct Input
        {
            float4 color;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            // 시간 기반으로 알파 값 조절
            float alpha = abs(sin(_Time.y * _BlinkSpeed));
            o.Albedo = _OutlineColor.rgb;
            o.Alpha = alpha; // 윤곽선 알파 값 설정
        }

        float4 LightingNoLight(SurfaceOutput s, float3 lightDir, float atten) {
            return float4(_OutlineColor.rgb, s.Alpha);
        }
        ENDCG

        Cull Back
        ZWrite On
        CGPROGRAM
        #pragma surface surf Lambert
        #pragma target 3.0

        sampler2D _MainTex;
        struct Input
        {
            float2 uv_MainTex;
        };

        void surf(Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}