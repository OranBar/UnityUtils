// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:1,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|diff-2072-RGB,emission-2275-RGB,alpha-7708-OUT;n:type:ShaderForge.SFN_Color,id:2275,x:32138,y:32140,ptovrint:False,ptlb:Emission,ptin:_Emission,varname:_Emission,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Color,id:2072,x:32330,y:32072,ptovrint:False,ptlb:MainColor,ptin:_MainColor,varname:_MainColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:9832,x:31743,y:32426,varname:node_9832,prsc:2|A-6806-OUT,B-1131-OUT,C-9972-OUT;n:type:ShaderForge.SFN_Sin,id:9035,x:31909,y:32426,varname:node_9035,prsc:2|IN-9832-OUT;n:type:ShaderForge.SFN_Time,id:9728,x:30943,y:32608,varname:node_9728,prsc:2;n:type:ShaderForge.SFN_Add,id:1131,x:31406,y:32401,varname:node_1131,prsc:2|A-4476-OUT,B-5865-OUT;n:type:ShaderForge.SFN_Multiply,id:5865,x:31136,y:32626,varname:node_5865,prsc:2|A-9728-T,B-6286-OUT;n:type:ShaderForge.SFN_Slider,id:6286,x:30667,y:32817,ptovrint:True,ptlb:Time_Multiplier,ptin:_Time_Multiplier,varname:_Time_Multiplier,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-0.5,cur:-0.09829061,max:0.5;n:type:ShaderForge.SFN_ValueProperty,id:8204,x:31495,y:32277,ptovrint:False,ptlb:StripesNum,ptin:_StripesNum,varname:_StripesNum,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:10;n:type:ShaderForge.SFN_ScreenPos,id:4184,x:31044,y:32361,varname:node_4184,prsc:2,sctp:0;n:type:ShaderForge.SFN_RemapRange,id:4476,x:31224,y:32401,varname:node_4476,prsc:2,frmn:-1,frmx:1,tomn:0,tomx:1|IN-4184-V;n:type:ShaderForge.SFN_Tau,id:9972,x:31545,y:32467,varname:node_9972,prsc:2;n:type:ShaderForge.SFN_ViewPosition,id:649,x:31295,y:32205,varname:node_649,prsc:2;n:type:ShaderForge.SFN_Distance,id:1548,x:31495,y:32085,varname:node_1548,prsc:2|A-4336-XYZ,B-649-XYZ;n:type:ShaderForge.SFN_ObjectPosition,id:4336,x:31281,y:32050,varname:node_4336,prsc:2;n:type:ShaderForge.SFN_Multiply,id:6806,x:31672,y:32115,varname:node_6806,prsc:2|A-1548-OUT,B-8204-OUT;n:type:ShaderForge.SFN_Slider,id:4384,x:31990,y:32972,ptovrint:False,ptlb:node_4384,ptin:_node_4384,varname:_node_4384,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.6011671,max:1;n:type:ShaderForge.SFN_Multiply,id:7708,x:32414,y:32828,varname:node_7708,prsc:2|A-9035-OUT,B-4384-OUT;proporder:2275-2072-6286-8204-4384;pass:END;sub:END;*/

Shader "Assets/HoloRenderTexture" {
    Properties {
        _Emission ("Emission", Color) = (1,1,1,1)
        _MainColor ("MainColor", Color) = (1,1,1,1)
        _Time_Multiplier ("Time_Multiplier", Range(-0.5, 0.5)) = -0.09829061
        _StripesNum ("StripesNum", Float ) = 10
        _node_4384 ("node_4384", Range(0, 1)) = 0.6011671
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
            uniform float4 _TimeEditor;
            uniform float4 _Emission;
            uniform float4 _MainColor;
            uniform float _Time_Multiplier;
            uniform float _StripesNum;
            uniform float _node_4384;
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 screenPos : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                o.screenPos = o.pos;
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
////// Lighting:
////// Emissive:
                float3 emissive = _Emission.rgb;
                float3 finalColor = emissive;
                float4 node_9728 = _Time + _TimeEditor;
                return fixed4(finalColor,(sin(((distance(objPos.rgb,_WorldSpaceCameraPos)*_StripesNum)*((i.screenPos.g*0.5+0.5)+(node_9728.g*_Time_Multiplier))*6.28318530718))*_node_4384));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
