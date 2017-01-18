// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:33238,y:32672,varname:node_3138,prsc:2|emission-6925-OUT,alpha-3333-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:32479,y:32731,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Fresnel,id:9143,x:32417,y:32967,varname:node_9143,prsc:2;n:type:ShaderForge.SFN_Multiply,id:6299,x:32613,y:32967,varname:node_6299,prsc:2|A-9143-OUT,B-5983-OUT;n:type:ShaderForge.SFN_OneMinus,id:9648,x:32782,y:32967,varname:node_9648,prsc:2|IN-6299-OUT;n:type:ShaderForge.SFN_Slider,id:5983,x:32417,y:33111,ptovrint:False,ptlb:Alpha Fade Intensity,ptin:_AlphaFadeIntensity,varname:node_5983,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1.433936,max:3;n:type:ShaderForge.SFN_Lerp,id:6925,x:32823,y:32745,varname:node_6925,prsc:2|A-5061-OUT,B-7241-RGB,T-7599-OUT;n:type:ShaderForge.SFN_Color,id:790,x:32136,y:32279,ptovrint:False,ptlb:Color_2,ptin:_Color_2,varname:_Color_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.7058823,c2:0.8904665,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:7599,x:32334,y:32715,varname:node_7599,prsc:2|A-2386-OUT,B-9022-OUT;n:type:ShaderForge.SFN_Slider,id:9022,x:32150,y:32875,ptovrint:False,ptlb:White Fade Intensity,ptin:_WhiteFadeIntensity,varname:_Intensity_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2,max:3;n:type:ShaderForge.SFN_Fresnel,id:8013,x:32099,y:32450,varname:node_8013,prsc:2|NRM-5610-OUT,EXP-5282-OUT;n:type:ShaderForge.SFN_Multiply,id:4138,x:32281,y:32431,varname:node_4138,prsc:2|A-8013-OUT,B-216-OUT;n:type:ShaderForge.SFN_Slider,id:216,x:32099,y:32599,ptovrint:False,ptlb:Core Intensity,ptin:_CoreIntensity,varname:_Intensity_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:8.474799,max:20;n:type:ShaderForge.SFN_NormalVector,id:5610,x:31819,y:32411,prsc:2,pt:False;n:type:ShaderForge.SFN_Fresnel,id:2386,x:32180,y:32715,varname:node_2386,prsc:2;n:type:ShaderForge.SFN_Multiply,id:5669,x:32495,y:32424,varname:node_5669,prsc:2|A-3828-OUT,B-4138-OUT;n:type:ShaderForge.SFN_OneMinus,id:3828,x:32306,y:32279,varname:node_3828,prsc:2|IN-790-RGB;n:type:ShaderForge.SFN_OneMinus,id:5061,x:32653,y:32402,varname:node_5061,prsc:2|IN-5669-OUT;n:type:ShaderForge.SFN_Vector1,id:5282,x:31819,y:32572,varname:node_5282,prsc:2,v1:0.9;n:type:ShaderForge.SFN_Multiply,id:3333,x:32987,y:33018,varname:node_3333,prsc:2|A-9648-OUT,B-1314-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1314,x:32796,y:33207,ptovrint:False,ptlb:Global_Alpha,ptin:_Global_Alpha,varname:node_1314,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;proporder:7241-5983-790-9022-216-1314;pass:END;sub:END;*/

Shader "Shader Forge/Energy_Glow" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _AlphaFadeIntensity ("Alpha Fade Intensity", Range(0, 3)) = 1.433936
        _Color_2 ("Color_2", Color) = (0.7058823,0.8904665,1,1)
        _WhiteFadeIntensity ("White Fade Intensity", Range(0, 3)) = 2
        _CoreIntensity ("Core Intensity", Range(0, 20)) = 8.474799
        _Global_Alpha ("Global_Alpha", Float ) = 1
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
            uniform float4 _Color;
            uniform float _AlphaFadeIntensity;
            uniform float4 _Color_2;
            uniform float _WhiteFadeIntensity;
            uniform float _CoreIntensity;
            uniform float _Global_Alpha;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float3 emissive = lerp((1.0 - ((1.0 - _Color_2.rgb)*(pow(1.0-max(0,dot(i.normalDir, viewDirection)),0.9)*_CoreIntensity))),_Color.rgb,((1.0-max(0,dot(normalDirection, viewDirection)))*_WhiteFadeIntensity));
                float3 finalColor = emissive;
                return fixed4(finalColor,((1.0 - ((1.0-max(0,dot(normalDirection, viewDirection)))*_AlphaFadeIntensity))*_Global_Alpha));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
