// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:1,spxs:False,tesm:0,olmd:1,culm:1,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|alpha-1569-OUT,refract-1396-OUT;n:type:ShaderForge.SFN_Slider,id:6997,x:31802,y:33823,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:_Opacity,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Multiply,id:1396,x:32114,y:33201,varname:node_1396,prsc:2|A-8098-RGB,B-6302-OUT,C-8137-OUT,D-2847-OUT;n:type:ShaderForge.SFN_Slider,id:6302,x:31562,y:33173,ptovrint:False,ptlb:Refraction_Intensity,ptin:_Refraction_Intensity,varname:_Refraction_Intensity,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-2,cur:0,max:2;n:type:ShaderForge.SFN_Tex2d,id:8098,x:31724,y:32760,ptovrint:False,ptlb:Refraction_Texture,ptin:_Refraction_Texture,varname:node_8098,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:e80c3c84ea861404d8a427db8b7abf04,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Fresnel,id:8137,x:31753,y:33263,varname:node_8137,prsc:2|EXP-3089-OUT;n:type:ShaderForge.SFN_Slider,id:3089,x:31395,y:33320,ptovrint:False,ptlb:Fresnel_Intensity,ptin:_Fresnel_Intensity,varname:_Refraction_Intensity_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-5,cur:2.472799,max:5;n:type:ShaderForge.SFN_TexCoord,id:7186,x:30980,y:32704,varname:node_7186,prsc:2,uv:0;n:type:ShaderForge.SFN_NormalVector,id:328,x:30960,y:32888,prsc:2,pt:False;n:type:ShaderForge.SFN_Multiply,id:2847,x:31345,y:32786,varname:node_2847,prsc:2|A-7186-UVOUT,B-328-OUT;n:type:ShaderForge.SFN_Fresnel,id:5148,x:31526,y:33519,varname:node_5148,prsc:2|EXP-7328-OUT;n:type:ShaderForge.SFN_Slider,id:7328,x:31208,y:33546,ptovrint:False,ptlb:Exp,ptin:_Exp,varname:node_7328,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1.650315,max:5;n:type:ShaderForge.SFN_Multiply,id:1569,x:32145,y:33329,varname:node_1569,prsc:2|A-5148-OUT,B-9874-OUT,C-6997-OUT;n:type:ShaderForge.SFN_Fresnel,id:3800,x:31526,y:33669,varname:node_3800,prsc:2|EXP-991-OUT;n:type:ShaderForge.SFN_Slider,id:991,x:31208,y:33696,ptovrint:False,ptlb:Exp_copy,ptin:_Exp_copy,varname:_Exp_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1.481498,max:5;n:type:ShaderForge.SFN_OneMinus,id:9874,x:31693,y:33669,varname:node_9874,prsc:2|IN-3800-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:8028,x:32456,y:33415,varname:node_8028,prsc:2;n:type:ShaderForge.SFN_Add,id:8016,x:32681,y:33472,varname:node_8016,prsc:2|A-8028-XYZ,B-762-OUT;n:type:ShaderForge.SFN_NormalVector,id:9866,x:32442,y:33555,prsc:2,pt:False;n:type:ShaderForge.SFN_Multiply,id:762,x:32640,y:33593,varname:node_762,prsc:2|A-9866-OUT,B-2233-OUT;n:type:ShaderForge.SFN_Slider,id:2233,x:32285,y:33757,ptovrint:False,ptlb:Scale,ptin:_Scale,varname:node_2233,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:-0.3460631,max:0;proporder:6997-6302-8098-3089-7328-991-2233;pass:END;sub:END;*/

Shader "Assets/WarpSphereDistortion" {
    Properties {
        _Opacity ("Opacity", Range(0, 1)) = 1
        _Refraction_Intensity ("Refraction_Intensity", Range(-2, 2)) = 0
        _Refraction_Texture ("Refraction_Texture", 2D) = "white" {}
        _Fresnel_Intensity ("Fresnel_Intensity", Range(-5, 5)) = 2.472799
        _Exp ("Exp", Range(0, 5)) = 1.650315
        _Exp_copy ("Exp_copy", Range(0, 5)) = 1.481498
        _Scale ("Scale", Range(-1, 0)) = -0.3460631
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        GrabPass{ }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Front
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _GrabTexture;
            uniform float _Opacity;
            uniform float _Refraction_Intensity;
            uniform sampler2D _Refraction_Texture; uniform float4 _Refraction_Texture_ST;
            uniform float _Fresnel_Intensity;
            uniform float _Exp;
            uniform float _Exp_copy;
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
                float4 screenPos : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(-v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                o.screenPos = o.pos;
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.normalDir = normalize(i.normalDir);
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float4 _Refraction_Texture_var = tex2D(_Refraction_Texture,TRANSFORM_TEX(i.uv0, _Refraction_Texture));
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5 + (_Refraction_Texture_var.rgb*_Refraction_Intensity*pow(1.0-max(0,dot(normalDirection, viewDirection)),_Fresnel_Intensity)*(float3(i.uv0,0.0)*i.normalDir)).rg;
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
////// Lighting:
                float3 finalColor = 0;
                return fixed4(lerp(sceneColor.rgb, finalColor,(pow(1.0-max(0,dot(normalDirection, viewDirection)),_Exp)*(1.0 - pow(1.0-max(0,dot(normalDirection, viewDirection)),_Exp_copy))*_Opacity)),1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
