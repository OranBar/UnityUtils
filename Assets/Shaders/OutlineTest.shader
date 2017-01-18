// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.3382353,fgcg:0.3382353,fgcb:0.3382353,fgca:1,fgde:0.002,fgrn:5,fgrf:15,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:2649,x:32994,y:32637,varname:node_2649,prsc:2|emission-80-OUT,alpha-2388-A;n:type:ShaderForge.SFN_Color,id:2388,x:32566,y:32967,ptovrint:False,ptlb:node_2388,ptin:_node_2388,varname:node_2388,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Fresnel,id:3267,x:32243,y:32749,varname:node_3267,prsc:2|EXP-8412-OUT;n:type:ShaderForge.SFN_Slider,id:8412,x:31895,y:32772,ptovrint:False,ptlb:HighlightIntensity,ptin:_HighlightIntensity,varname:node_8412,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.34,max:3;n:type:ShaderForge.SFN_RemapRange,id:6721,x:32400,y:32766,varname:node_6721,prsc:2,frmn:0,frmx:1,tomn:-3,tomx:2|IN-3267-OUT;n:type:ShaderForge.SFN_Multiply,id:80,x:32745,y:32766,varname:node_80,prsc:2|A-2640-OUT,B-2388-RGB;n:type:ShaderForge.SFN_Clamp,id:2640,x:32555,y:32766,varname:node_2640,prsc:2|IN-6721-OUT,MIN-7429-OUT,MAX-8288-OUT;n:type:ShaderForge.SFN_Vector1,id:7429,x:32248,y:32933,varname:node_7429,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:8288,x:32248,y:32994,varname:node_8288,prsc:2,v1:1;n:type:ShaderForge.SFN_Slider,id:2721,x:31569,y:33329,ptovrint:False,ptlb:Refraction_Intensity_copy,ptin:_Refraction_Intensity_copy,varname:_Refraction_Intensity_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-5,cur:1.47091,max:5;proporder:2388-8412;pass:END;sub:END;*/

Shader "Outlined/OutlineTest" {
    Properties {
        _node_2388 ("node_2388", Color) = (1,1,1,1)
        _HighlightIntensity ("HighlightIntensity", Range(0, 3)) = 0.34
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
            #pragma multi_compile_fog
            #pragma exclude_renderers gles xbox360 ps3 
            #pragma target 3.0
            uniform float4 _node_2388;
            uniform float _HighlightIntensity;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                UNITY_FOG_COORDS(2)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float3 emissive = (clamp((pow(1.0-max(0,dot(normalDirection, viewDirection)),_HighlightIntensity)*5.0+-3.0),0.0,1.0)*_node_2388.rgb);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,_node_2388.a);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
