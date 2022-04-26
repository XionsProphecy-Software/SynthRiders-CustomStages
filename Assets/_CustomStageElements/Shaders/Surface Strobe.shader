Shader "Rrye/Strobe/Surface" {
    Properties {
        _StrobeTex("Strobe Texture (RTs)", 2D) = "white" {}
        [KeywordEnum(Albedo,Emission)]_Option("Albedo or Emission?", int) = 0
        _Expo ("Intensity", range(0,1)) = 0
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        sampler2D _StrobeTex;

        struct Input {
            float2 uv_StrobeTex;
        };

        int _Option;
        half _Expo;

        void surf (Input IN, inout SurfaceOutputStandard o) {
            fixed4 c = tex2D(_StrobeTex, IN.uv_StrobeTex);

            half expo = pow(2.0, _Expo);
            #ifndef UNITY_COLORSPACE_GAMMA
            expo = pow(expo, 2.2);
            #endif
            c.rgb *= expo;

            if (_Option == 0) {o.Albedo = c.rgb;}
            if (_Option == 1) {o.Emission = c.rgb;}
        }
        ENDCG
    }
    FallBack "Diffuse"
}