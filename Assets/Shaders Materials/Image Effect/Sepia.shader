Shader "PostProcessing/FogEffect"
{
    Properties
    {
        _MainTex ("Render Texture", 2D) = "white" {}
        _CameraDepthTexture ("Camera Depth Texture", 2D) = "white" {}
        _Near ("Near Plane", Float) = 0.1
        _Far ("Far Plane", Float) = 1000
        _FogColor ("Fog Color", Color) = (1, 1, 1, 1) // White fog
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _CameraDepthTexture;
            float _Near;
            float _Far;
            float4 _FogColor;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                // Sample the original scene color
                float4 sceneColor = tex2D(_MainTex, i.uv);

                // Sample the depth value from the depth texture
                float depth = tex2D(_CameraDepthTexture, i.uv).r;

                // Linearize the depth value
                float linearDepth = (2.0 * _Near) / (_Far + _Near - depth * (_Far - _Near));

                // Normalize depth to range [0, 1]
                float normalizedDepth = saturate((linearDepth - _Near) / (_Far - _Near));

                // Interpolate between scene color and fog color based on depth
                float fogFactor = normalizedDepth; // Objects closer to far plane get more fog
                float4 finalColor = lerp(sceneColor, _FogColor, fogFactor);

                return finalColor;
            }
            ENDCG
        }
    }
}
