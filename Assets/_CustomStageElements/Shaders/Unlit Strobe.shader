Shader "Rrye/Strobe/Unlit" {
    Properties {
        _StrobeTex("Strobe Texture (RTs)", 2D) = "white" {}
        _Expo ("Intensity", range(0,1)) = 0
        [Toggle]_FT("Apply Fog?", int) = 0
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };

            sampler2D _StrobeTex;
            int _FT;
            half _Expo;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;

                if (_FT == 0) {}
                if (_FT == 1) {UNITY_TRANSFER_FOG(o,o.vertex);}

                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                fixed4 c = tex2D(_StrobeTex, i.uv);

                half expo = pow(2.0, _Expo);
                #ifndef UNITY_COLORSPACE_GAMMA
                expo = pow(expo, 2.2);
                #endif
                c.rgb *= expo;

                if (_FT == 0) {}
                if (_FT == 1) {UNITY_APPLY_FOG(i.fogCoord, c);}

                return c;
            }
            ENDCG
        }
    }
}