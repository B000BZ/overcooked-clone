Shader "ToonyColorsPro/CustomCircleSpreadWithTexture" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Color1 ("Color 1", Color) = (1, 1, 1, 1)
        _Color2 ("Color 2", Color) = (1, 0, 0, 1)
        _SpreadSlider ("Spread", Range(0, 1)) = 0
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Toon outline

        struct Input {
            float2 uv_MainTex;
        };

        sampler2D _MainTex;
        fixed4 _Color1;
        fixed4 _Color2;
        half _SpreadSlider;

        void surf (Input IN, inout SurfaceOutput o) {
            // Sample the texture
            fixed4 texColor = tex2D(_MainTex, IN.uv_MainTex);

            // Calculate distance from center
            float2 center = float2(0.5, 0.5);
            float distance = length(IN.uv_MainTex - center);

            // Calculate circle spread
            float spread = smoothstep(0, _SpreadSlider, distance);

            // Mix colors based on spread
            fixed4 finalColor = lerp(_Color1, _Color2, spread);

            // Combine texture color and final color
            finalColor.rgb = texColor.rgb * (1 - spread) + finalColor.rgb * spread;

            o.Albedo = finalColor.rgb;
            o.Alpha = texColor.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
