Shader "Custom/OutlineBlink"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _OutlineColor("Outline Color", Color) = (0, 1, 0, 1) // 초록색으로 설정
        _OutlineWidth("Outline Width", Range(0.005, 0.05)) = 0.02 // 윤곽선 두께
        _BlinkSpeed("Blink Speed", Range(0.1, 5.0)) = 1.0 // 깜빡이는 속도
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            Name "OUTLINE"
            Tags { "LightMode" = "Always" }

            Cull Front
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : POSITION;
                float4 color : COLOR;
            };

            float4 _OutlineColor;
            float _OutlineWidth;
            float _BlinkSpeed;

            v2f vert(appdata v)
            {
                // 법선 방향으로 버텍스를 이동
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex + v.normal * _OutlineWidth);
                float alpha = abs(sin(_Time.y * _BlinkSpeed));
                o.color = float4(_OutlineColor.rgb, alpha);
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                return i.color;
            }
            ENDCG
        }

        // Pass
        // {
            Name "MAIN"
            Tags { "LightMode" = "ForwardBase" }

            Cull Back
            ZWrite On
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma surface surf Standard fullforwardshadows
            #pragma target 3.0

            sampler2D _MainTex;

            struct Input
            {
                float2 uv_MainTex;
            };

            void surf(Input IN, inout SurfaceOutputStandard o)
            {
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
                o.Albedo = c.rgb;
                o.Alpha = c.a;
            }
            ENDCG
        // }
    }
    FallBack "Diffuse"
}

