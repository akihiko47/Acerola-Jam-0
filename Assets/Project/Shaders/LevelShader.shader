Shader "Custom/Level" {

    Properties{
        _Twist ("Twist", float) = 0.2
        _Color1 ("Color 1", Color) = (0.5, 0.5, 0.5, 1.0)
        _Color2 ("Color 2", Color) = (0.9, 0.9, 0.9, 1.0)
        [NoScaleOffset] _LightTex ("Main Texture", 2D) = "white" {}
        [NoScaleOffset] _ShadowTex ("No Light Texture", 2D) = "white" {}
    }

    SubShader {
        Tags { "RenderType"="Opaque" }

        Pass {

            Tags { "LightMode" = "ForwardBase" }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog
            //#pragma multi_compile_fwdadd_fullshadows

            #define IN_BASE_PASS

            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"

            #include "LevelLighting.cginc"

            ENDCG
        }

        Pass {

            Tags { "LightMode" = "ForwardAdd" }

            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog

            #include "LevelLighting.cginc"

            ENDCG
        }

       Pass{
            Tags {
                "LightMode" = "ShadowCaster"
            }

            CGPROGRAM

            #pragma multi_compile_shadowcaster

            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            #include "UnityCG.cginc"

            float _Twist;

            struct MeshData {
                float4 position : POSITION;
                float3 normal : NORMAL;
            };

            float3 Unity_RotateAboutAxis_Radians_float(float3 In, float3 Axis, float Rotation) {
                float s = sin(Rotation);
                float c = cos(Rotation);
                float one_minus_c = 1.0 - c;

                Axis = normalize(Axis);
                float3x3 rot_mat =
                { one_minus_c * Axis.x * Axis.x + c, one_minus_c * Axis.x * Axis.y - Axis.z * s, one_minus_c * Axis.z * Axis.x + Axis.y * s,
                    one_minus_c * Axis.x * Axis.y + Axis.z * s, one_minus_c * Axis.y * Axis.y + c, one_minus_c * Axis.y * Axis.z - Axis.x * s,
                    one_minus_c * Axis.z * Axis.x - Axis.y * s, one_minus_c * Axis.y * Axis.z + Axis.x * s, one_minus_c * Axis.z * Axis.z + c
                };
                return mul(rot_mat, In);
            }

            #ifdef SHADOWS_CUBE
                struct Interpolators {
                    float4 position : SV_POSITION;
                    float3 lightVec : TEXCOORD0;
                };

                Interpolators vert(MeshData v) {
                    Interpolators i;
                    i.position = UnityObjectToClipPos(v.position);
                    i.lightVec = mul(unity_ObjectToWorld, v.position).xyz - _LightPositionRange.xyz;
                    return i;
                }

                float4 frag(Interpolators i) : SV_TARGET{
                    float depth = length(i.lightVec) + unity_LightShadowBias.x;
                    depth *= _LightPositionRange.w;
                    return UnityEncodeCubeShadowDepth(depth);
                }
            #else
                float4 vert(MeshData v) : SV_POSITION{
                    float3 worldPos = mul(unity_ObjectToWorld, v.position);
                    worldPos = Unity_RotateAboutAxis_Radians_float(worldPos, float3(0.0, 1.0, 0.0), 3.14 / 100 * worldPos.y * _Twist);
                    v.position.xyz = mul(worldPos.xyz, unity_ObjectToWorld);
                    float4 position = UnityClipSpaceShadowCasterPos(v.position.xyz, v.normal);
                    return UnityApplyLinearShadowBias(position);
                }

                float4 frag() : SV_TARGET{
                    return 0;
                }
            #endif

            ENDCG
        }
    }
}
