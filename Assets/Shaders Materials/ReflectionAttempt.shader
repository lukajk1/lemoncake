Shader "Unlit/ScreenSpaceTextureMask"
{
    Properties
    {
        _MainTex ("Render Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 screenUV : TEXCOORD0;
            };

            sampler2D _MainTex;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                // Calculate screen space UV coordinates
                o.screenUV = o.vertex.xy / o.vertex.w; // Normalize to clip space
                o.screenUV = o.screenUV * 0.5 + 0.5;   // Convert to 0-1 range
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Sample the texture using screen UVs
                fixed4 texColor = tex2D(_MainTex, i.screenUV);

                // Output the sampled color
                return texColor;
            }
            ENDCG
        }
    }
}
