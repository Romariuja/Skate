// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Unlit alpha-blended shader.
// - no lighting
// - no lightmap support
// - no per-material color

Shader "GreyScale" {
        Properties
        {
                _MainTex ("Diffuse Textures", 2D) = "white" {}
        }
        SubShader {
                Tags { "RenderType"="Opaque" "Queue" = "Geometry" }
                LOD 200
               
                CGPROGRAM
                #pragma surface surf Lambert alphatest:0.5
 
                sampler2D _MainTex;
 
                struct Input
                {
                    float2 uv_MainTex;
                        float2 uv_GreyMask;
                };
 
                void surf (Input IN, inout SurfaceOutput o)
                {
                        half4 c = tex2D(_MainTex, IN.uv_MainTex);
                        o.Albedo = half3(Luminance(c.rgb));
                        o.Alpha = c.a;
                }
                ENDCG
        }
       
        SubShader {
                Tags { "RenderType"="Transparent" "Queue" = "Transparent" }
                LOD 200
               
                CGPROGRAM
                #pragma surface surf Lambert alpha
 
                sampler2D _MainTex;
 
                struct Input
                {
                    float2 uv_MainTex;
                        float2 uv_GreyMask;
                };
 
                void surf (Input IN, inout SurfaceOutput o)
                {
                        half4 c = tex2D(_MainTex, IN.uv_MainTex);
                        o.Albedo = half3(Luminance(c.rgb));
                        o.Alpha = c.a;
                }
                ENDCG
        }
        FallBack "Diffuse"
}