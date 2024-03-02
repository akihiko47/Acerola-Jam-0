#if !defined(LEVEL_LIGHTING)
#define LEVEL_LIGHTING

#include "UnityCG.cginc"
#include "AutoLight.cginc"
#include "Lighting.cginc"

sampler2D _LightTex;
sampler2D _ShadowTex;
float4 _Color1, _Color2;
float _Twist;

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
    UNITY_FOG_COORDS(3)
    //SHADOW_COORDS(5)
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

v2f vert(MeshData v) {
    v2f o;
    o.worldPos = mul(unity_ObjectToWorld, v.vertex);
    //o.worldPos = Unity_RotateAboutAxis_Radians_float(o.worldPos, float3(0.0, 1.0, 0.0), 3.14 / 100 * o.worldPos.y * _Twist * sin(_Time.y * 0.5));
    o.pos = mul(UNITY_MATRIX_VP, float4(o.worldPos, 1));
    o.uv = v.uv;
    o.normal = UnityObjectToWorldNormal(v.normal);
    UNITY_TRANSFER_FOG(o, o.pos);
    //TRANSFER_SHADOW(o);
    return o;
}

float4 frag(v2f i) : SV_Target{
    #ifdef IN_BASE_PASS
        float alpha = tex2D(_ShadowTex, i.uv).a;
        clip(alpha - 0.8);

        i.normal = normalize(i.normal);

        float3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos);
        float3 lightDir = _WorldSpaceLightPos0.xyz;
        float3 lightColor = _LightColor0;

        float3 albedo = tex2D(_ShadowTex, i.uv);
        float3 ambient = (unity_SHAr.w, unity_SHAg.w, unity_SHAb.w);

        float lambert = saturate(dot(lightDir, i.normal));
        //return float4(lambert.xxx, 1.0);
        //float shadowMask = saturate(attenuation * lambert);

        float3 diffuse = lambert * lightColor.rgb;
        diffuse += ambient;

        float3 color = albedo * diffuse;
        //float3 color = _Color1 * shadowMask + _Color2 * (1- shadowMask);

        UNITY_APPLY_FOG(i.fogCoord, color);

        return float4(color, 1.0);
    #else
        i.normal = normalize(i.normal);

        float3 lightDir = normalize(_WorldSpaceLightPos0.xyz - i.worldPos);
        float3 lightColor = _LightColor0;

        UNITY_LIGHT_ATTENUATION(attenuation, i, i.worldPos);
        
        float shadowMask = saturate(attenuation * lightColor);
        shadowMask = (shadowMask > 0.5);

        float3 color = tex2D(_LightTex, i.uv) * shadowMask;
        //float3 color = _Color1 * shadowMask;

        UNITY_APPLY_FOG(i.fogCoord, color);

        return float4(color, shadowMask.x);
    #endif
}

#endif