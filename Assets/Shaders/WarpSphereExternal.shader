// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:2,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|diff-1180-RGB,emission-8530-RGB,alpha-4447-OUT;n:type:ShaderForge.SFN_Tex2d,id:8530,x:31886,y:32673,ptovrint:False,ptlb:Main_Texture,ptin:_Main_Texture,varname:_Main_Texture,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:146f67f07a00fa94b923771b2f3ed483,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Color,id:1180,x:32251,y:32533,ptovrint:False,ptlb:Fresnel_Color,ptin:_Fresnel_Color,varname:_Fresnel_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.7573529,c2:0.8694726,c3:1,c4:1;n:type:ShaderForge.SFN_Fresnel,id:8495,x:31282,y:33131,varname:node_8495,prsc:2;n:type:ShaderForge.SFN_Slider,id:8378,x:31429,y:33316,ptovrint:False,ptlb:Alpha_Fresnel_Intensity,ptin:_Alpha_Fresnel_Intensity,varname:_Alpha_Fresnel_Intensity,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:5.660369,max:30;n:type:ShaderForge.SFN_OneMinus,id:6825,x:31442,y:33131,varname:node_6825,prsc:2|IN-8495-OUT;n:type:ShaderForge.SFN_Multiply,id:4447,x:32435,y:33095,varname:node_4447,prsc:2|A-3874-OUT,B-1076-OUT;n:type:ShaderForge.SFN_RemapRange,id:7772,x:31993,y:33131,varname:node_7772,prsc:2,frmn:-1,frmx:1,tomn:1,tomx:0.5|IN-4114-OUT;n:type:ShaderForge.SFN_Multiply,id:4114,x:31819,y:33131,varname:node_4114,prsc:2|A-6825-OUT,B-8378-OUT;n:type:ShaderForge.SFN_Fresnel,id:4565,x:31277,y:33448,varname:node_4565,prsc:2|EXP-3004-OUT;n:type:ShaderForge.SFN_RemapRange,id:4971,x:31988,y:33448,varname:node_4971,prsc:2,frmn:-20,frmx:1,tomn:10,tomx:0.5|IN-4565-OUT;n:type:ShaderForge.SFN_Slider,id:3004,x:31179,y:33703,ptovrint:False,ptlb:Alpha_Fresnel_Intensity_copy,ptin:_Alpha_Fresnel_Intensity_copy,varname:_Alpha_Fresnel_Intensity_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-3,cur:-0.4725811,max:3;n:type:ShaderForge.SFN_Add,id:3874,x:32135,y:33131,varname:node_3874,prsc:2|A-7772-OUT,B-4971-OUT;n:type:ShaderForge.SFN_Slider,id:1076,x:32195,y:33387,ptovrint:False,ptlb:Alpha,ptin:_Alpha,varname:node_1076,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;proporder:8530-1180-8378-3004-1076;pass:END;sub:END;*/

Shader "Assets/WarpSphereExternal" {
    Properties {
        _Main_Texture ("Main_Texture", 2D) = "black" {}
        _Fresnel_Color ("Fresnel_Color", Color) = (0.7573529,0.8694726,1,1)
        _Alpha_Fresnel_Intensity ("Alpha_Fresnel_Intensity", Range(0, 30)) = 5.660369
        _Alpha_Fresnel_Intensity_copy ("Alpha_Fresnel_Intensity_copy", Range(-3, 3)) = -0.4725811
        _Alpha ("Alpha", Range(0, 1)) = 1
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
            uniform sampler2D _Main_Texture; uniform float4 _Main_Texture_ST;
            uniform float4 _Fresnel_Color;
            uniform float _Alpha_Fresnel_Intensity;
            uniform float _Alpha_Fresnel_Intensity_copy;
            uniform float _Alpha;
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
                float3 diffuseColor = _Fresnel_Color.rgb;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float4 _Main_Texture_var = tex2D(_Main_Texture,TRANSFORM_TEX(i.uv0, _Main_Texture));
                float3 emissive = _Main_Texture_var.rgb;
/// Final Color:
                float3 finalColor = diffuse + emissive;
                return fixed4(finalColor,(((((1.0 - (1.0-max(0,dot(normalDirection, viewDirection))))*_Alpha_Fresnel_Intensity)*-0.25+0.75)+(pow(1.0-max(0,dot(normalDirection, viewDirection)),_Alpha_Fresnel_Intensity_copy)*-0.452381+0.9523809))*_Alpha));
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
            uniform sampler2D _Main_Texture; uniform float4 _Main_Texture_ST;
            uniform float4 _Fresnel_Color;
            uniform float _Alpha_Fresnel_Intensity;
            uniform float _Alpha_Fresnel_Intensity_copy;
            uniform float _Alpha;
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
                float3 diffuseColor = _Fresnel_Color.rgb;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                return fixed4(finalColor * (((((1.0 - (1.0-max(0,dot(normalDirection, viewDirection))))*_Alpha_Fresnel_Intensity)*-0.25+0.75)+(pow(1.0-max(0,dot(normalDirection, viewDirection)),_Alpha_Fresnel_Intensity_copy)*-0.452381+0.9523809))*_Alpha),0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
