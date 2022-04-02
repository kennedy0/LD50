Shader "Unlit/gradient_shader"
{
    Properties
    {
        [Header(Color)]
        _ColorA ("Color A", Color) = (1, 0, 0, 1)
        _ColorB ("Color B", Color) = (0, 1, 1, 1)
        [Header(Position)]
        _Scale ("Scale", Float) = 1
        _Offset ("Offset", Float) = 0
        [Header(Orientation)]
        [KeywordEnum(Object, World)] _Space("Coordinate Space", Float) = 0
        [KeywordEnum(X, Y, Z)] _Axis("Axis", Float) = 0
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
            #pragma multi_compile_instancing
            #pragma multi_compile_local _AXIS_X _AXIS_Y _AXIS_Z
            #pragma multi_compile_local _SPACE_OBJECT _SPACE_WORLD

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 local_pos : TEXCOORD1;
                float4 world_pos : TEXCOORD2;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            UNITY_INSTANCING_BUFFER_START(Props)
            UNITY_INSTANCING_BUFFER_END(Props)

            v2f vert (appdata v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.local_pos = v.vertex;
                o.world_pos = mul(unity_ObjectToWorld, v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 _ColorA;
            fixed4 _ColorB;
            float _Scale;
            float _Offset;

            fixed4 frag (v2f i) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(i);

                float4 coordinate_space;
                float position;

                #if _SPACE_OBJECT
                coordinate_space = i.local_pos;
                #elif _SPACE_WORLD
                coordinate_space = i.world_pos;
                #endif

                #if _AXIS_X
                position = coordinate_space.x;
                #elif _AXIS_Y
                position = coordinate_space.y;
                #elif _AXIS_Z
                position = coordinate_space.z;
                #endif

                position = position + (_Offset / 10.0f);
                position = position * _Scale;

                position = (position + 1.0f) / 2.0f;
                position = clamp(position, 0.0f, 1.0f);
                fixed4 c = lerp(_ColorA, _ColorB, position);

                return c;
            }
            ENDCG
        }
    }
}
