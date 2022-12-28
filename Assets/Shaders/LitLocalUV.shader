Shader "Custom/LitLocalUV"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _TexSize("Texture size", Float) = 1
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows vertex:vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        float _TexSize;

        struct Input
        {
            float2 uv_MainTex;
            float3 normal;
            float3 vertex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;


        void vert(inout appdata_full v, out Input o) {
            UNITY_INITIALIZE_OUTPUT(Input, o);
            o.normal = v.normal;
            o.vertex = v.vertex;
        }

        float2 recalculateUV(Input IN)
        {
            float3 pos = IN.vertex;
            float3 normal = IN.normal;
            float2 uv = pos.xy * abs(normal.z)
                + pos.xz * abs(normal.y)
                + pos.yz * abs(normal.x);
            return uv;
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float2 uv = recalculateUV(IN);
            fixed4 c = tex2D (_MainTex, uv / _TexSize) * _Color;
            o.Albedo = c;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
