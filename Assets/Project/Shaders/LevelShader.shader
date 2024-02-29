Shader "Custom/Level" {

    Properties {
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
            #pragma multi_compile_fwdadd_fullshadows

            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"

            sampler2D _LightTex;
            sampler2D _ShadowTex;

            struct MeshData {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float3 normal : TEXCOORD1;
                float3 worldPos : TEXCOORD2;
                float4 pos : SV_POSITION;
                SHADOW_COORDS(5)
            };

            v2f vert (MeshData v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.uv = v.uv;
                o.normal = UnityObjectToWorldNormal(v.normal);
                TRANSFER_SHADOW(o);
                return o;
            }

            float4 frag(v2f i) : SV_Target{
                i.normal = normalize(i.normal);
                
                float3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos);
                float3 lightDir = _WorldSpaceLightPos0.xyz;
                float3 lightColor = _LightColor0;

                UNITY_LIGHT_ATTENUATION(attenuation, i, i.worldPos);
                float lambert = saturate(dot(lightDir, i.normal));
                float shadowMask = saturate(attenuation * lambert);
                
                float3 color = tex2D(_LightTex, i.uv) * shadowMask + tex2D(_ShadowTex, i.uv) * (1- shadowMask);
                float3 ambient = (unity_SHAr.w, unity_SHAg.w, unity_SHAb.w);
                color *= ambient;

                return float4(color, 1.0);
            }

            ENDCG
        }

        Pass {

            Tags { "LightMode" = "ForwardAdd" }

            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdadd_fullshadows

            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"

            sampler2D _LightTex;
            sampler2D _ShadowTex;

            struct MeshData {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float3 normal : TEXCOORD1;
                float3 worldPos : TEXCOORD2;
                float4 pos : SV_POSITION;
                SHADOW_COORDS(5)
            };

            v2f vert(MeshData v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.uv = v.uv;
                o.normal = UnityObjectToWorldNormal(v.normal);
                TRANSFER_SHADOW(o);
                return o;
            }

            float4 frag(v2f i) : SV_Target{
                i.normal = normalize(i.normal);

                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz - i.worldPos);
                float3 lightColor = _LightColor0;

                UNITY_LIGHT_ATTENUATION(attenuation, i, i.worldPos);
                float lambert = saturate(dot(lightDir, i.normal));
                float shadowMask = saturate(attenuation * (lambert > 0.01) * lightColor);
                shadowMask = (shadowMask > 0.5);

                float3 color = tex2D(_LightTex, i.uv) * shadowMask;

                return float4(color, shadowMask.x);
            }
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

            struct MeshData {
                float4 position : POSITION;
                float3 normal : NORMAL;
            };

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
