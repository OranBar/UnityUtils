// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:1,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|diff-2072-RGB,emission-2275-RGB,alpha-1560-OUT;n:type:ShaderForge.SFN_Color,id:2275,x:32335,y:32738,ptovrint:False,ptlb:Emission,ptin:_Emission,varname:_Emission,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Slider,id:5697,x:31503,y:33067,ptovrint:True,ptlb:Fresnel_Intensity,ptin:_Fresnel_Intensity,varname:_Fresnel_Intensity,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1.750347,max:3;n:type:ShaderForge.SFN_Color,id:2072,x:32354,y:32294,ptovrint:False,ptlb:MainColor,ptin:_MainColor,varname:_MainColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Fresnel,id:7089,x:31882,y:32941,varname:node_7089,prsc:2|EXP-5697-OUT;n:type:ShaderForge.SFN_Slider,id:6248,x:31503,y:33250,ptovrint:True,ptlb:Fresnel_Intensity_Internal,ptin:_Fresnel_Intensity_Internal,varname:_Fresnel_Intensity_Internal,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.7047474,max:5;n:type:ShaderForge.SFN_Fresnel,id:9016,x:32069,y:33082,varname:node_9016,prsc:2|EXP-6248-OUT;n:type:ShaderForge.SFN_Multiply,id:1560,x:32502,y:32990,varname:node_1560,prsc:2|A-7089-OUT,B-9016-OUT,C-1216-OUT;n:type:ShaderForge.SFN_TexCoord,id:5641,x:31319,y:32575,varname:node_5641,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:9832,x:31731,y:32426,varname:node_9832,prsc:2|A-8204-OUT,B-1131-OUT;n:type:ShaderForge.SFN_ComponentMask,id:3715,x:31498,y:32615,varname:node_3715,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-5641-V;n:type:ShaderForge.SFN_Sin,id:9035,x:31892,y:32426,varname:node_9035,prsc:2|IN-9832-OUT;n:type:ShaderForge.SFN_RemapRange,id:1216,x:32070,y:32426,varname:node_1216,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-9035-OUT;n:type:ShaderForge.SFN_Time,id:9728,x:31404,y:32782,varname:node_9728,prsc:2;n:type:ShaderForge.SFN_Add,id:1131,x:31665,y:32615,varname:node_1131,prsc:2|A-3715-OUT,B-5865-OUT;n:type:ShaderForge.SFN_Multiply,id:5865,x:31561,y:32782,varname:node_5865,prsc:2|A-9728-T,B-6286-OUT;n:type:ShaderForge.SFN_Slider,id:6286,x:31084,y:32956,ptovrint:True,ptlb:Time_Multiplier,ptin:_Time_Multiplier,varname:_Time_Multiplier,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-0.5,cur:-0.1153846,max:0.5;n:type:ShaderForge.SFN_ValueProperty,id:8204,x:31340,y:32403,ptovrint:False,ptlb:StripesNum,ptin:_StripesNum,varname:_StripesNum,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1000;proporder:2275-5697-2072-6248-6286-8204;pass:END;sub:END;*/

Shader "Assets/FresnelHolo" {
    Properties {
        _Emission ("Emission", Color) = (1,1,1,1)
        _Fresnel_Intensity ("Fresnel_Intensity", Range(0, 3)) = 1.750347
        _MainColor ("MainColor", Color) = (1,1,1,1)
        _Fresnel_Intensity_Internal ("Fresnel_Intensity_Internal", Range(0, 5)) = 0.7047474
        _Time_Multiplier ("Time_Multiplier", Range(-0.5, 0.5)) = -0.1153846
        _StripesNum ("StripesNum", Float ) = 1000
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform float4 _Emission;
            uniform float _Fresnel_Intensity;
            uniform float4 _MainColor;
            uniform float _Fresnel_Intensity_Internal;
            uniform float _Time_Multiplier;
            uniform float _StripesNum;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float3 diffuseColor = _MainColor.rgb;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float3 emissive = _Emission.rgb;
/// Final Color:
                float3 finalColor = diffuse + emissive;
                float4 node_9728 = _Time + _TimeEditor;
                return fixed4(finalColor,(pow(1.0-max(0,dot(normalDirection, viewDirection)),_Fresnel_Intensity)*pow(1.0-max(0,dot(normalDirection, viewDirection)),_Fresnel_Intensity_Internal)*(sin((_StripesNum*(i.uv0.g.r+(node_9728.g*_Time_Multiplier))))*2.0+-1.0)));
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform float4 _Emission;
            uniform float _Fresnel_Intensity;
            uniform float4 _MainColor;
            uniform float _Fresnel_Intensity_Internal;
            uniform float _Time_Multiplier;
            uniform float _StripesNum;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 diffuseColor = _MainColor.rgb;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                float4 node_9728 = _Time + _TimeEditor;
                return fixed4(finalColor * (pow(1.0-max(0,dot(normalDirection, viewDirection)),_Fresnel_Intensity)*pow(1.0-max(0,dot(normalDirection, viewDirection)),_Fresnel_Intensity_Internal)*(sin((_StripesNum*(i.uv0.g.r+(node_9728.g*_Time_Multiplier))))*2.0+-1.0)),0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
