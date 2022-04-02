Shader "Unlit/minimal_shader"
{
    Properties
    {
        _TopColor ("Top Color", Color) = (0, 1, 0, 0)
        _FrontColor ("Front Color", Color) = (0, 0, 1, 0)
        _SideColor ("Side Color", Color) = (1, 0, 0, 0)
        _BottomColor ("Bottom Color", Color) = (0, 0, 0, 0)
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
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.uv = v.uv;
                return o;
            }

            fixed4 _TopColor;
            fixed4 _FrontColor;
            fixed4 _SideColor;
            fixed4 _BottomColor;

            fixed4 frag (v2f i) : SV_Target
            {
                float3 facing_up = clamp(dot(i.normal.y, float3(0, 1, 0)), 0, 1);
                float3 facing_front = clamp(dot(abs(i.normal.z), float3(0, 0, 1)), 0, 1);
                float3 facing_side = clamp(dot(abs(i.normal.x), float3(1, 0, 0)), 0, 1);
                float3 facing_down = clamp(dot(-i.normal.y, float3(0, 1, 0)), 0, 1);
                
                fixed4 black = fixed4(0, 0, 0, 0);
                fixed4 c = black;
                
                c.rgb = lerp(c, _SideColor, facing_side);
                c.rgb = lerp(c, _FrontColor, facing_front);
                c.rgb = lerp(c, _TopColor, facing_up);
                c.rgb = lerp(c, _BottomColor, facing_down);
                
                return c;
            }
            ENDCG
        }
    }
}
