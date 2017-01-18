// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/StandardHighlightedZAlways" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0.0, 1.0)) = 0.5
		_Metallic("Metallic", Range(0.0, 1.0)) = 0.0

		//_BumpScale("Scale", Float) = 1.0
		_BumpMap("Normal Map", 2D) = "bump" {}


		_EmissionColor("EmissionColor", Color) = (0.4419334,0.5356163,0.9852941,0.234)
		_EmissionPower("EmissionPower", Float) = 1
		_FresnelExp("FresnelExp", Float) = 1
	}

	SubShader {
		Tags { "RenderType"="Opaque" }
		//LOD 200
		Cull Off

		Pass{
			Name "HighlightZAlways"
			Tags{}

			ZTest Always
			ZWrite Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma multi_compile_shadowcaster
			#pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
			#pragma target 3.0

			uniform float _FresnelExp;
			uniform float4 _EmissionColor;
			uniform float _EmissionPower;

			//Vertex Shader
			struct VertexInput {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};
			struct VertexOutput {
				float4 pos : SV_POSITION;
				float4 posWorld : TEXCOORD1;
				float3 normalDir : TEXCOORD2;
			};

			VertexOutput vert(VertexInput v) {
				VertexOutput o = (VertexOutput)0;
				o.normalDir = UnityObjectToWorldNormal(v.normal);
				o.posWorld = mul(unity_ObjectToWorld, v.vertex);
				o.pos = mul(UNITY_MATRIX_MVP, float4(v.vertex.xyz + v.normal*0.0001 ,1));
				return o;
			}
		
			//Fragment Shader
			float4 frag(VertexOutput i) : COLOR{
				//i.normalDir = normalize(i.normalDir);
				float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
				//float3 viewDirection = _WorldSpaceCameraPos.xyz - i.posWorld.xyz;
				float3 emissive = (_EmissionPower*(_EmissionColor.rgb*pow(1.0 - max(0,dot((i.normalDir), viewDirection)),_FresnelExp)));
				float modulate = abs(sin(_Time*30));
				emissive.r *= modulate;
				emissive.g *= modulate;
				emissive.b *= modulate;
				return fixed4(emissive,1);
			}
				ENDCG
			}


		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows


		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0
		#include "UnityCG.cginc"

		sampler2D _MainTex;
		sampler2D _MetallicGlossMap;
		sampler2D _BumpMap;





		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
			float3 viewDir;
			float3 worldNormal;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		float _FresnelExp;
		float4 _EmissionColor;
		float _EmissionPower;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color

			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			//o.Normal = UnpackNormal(tex2D (_BumpMap, IN.uv_MainTex));
			o.Normal = UnpackScaleNormal (tex2D (_BumpMap, IN.uv_MainTex), 0.05);
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			float3 emissive = (_EmissionPower*(_EmissionColor.rgb*pow(1.0 - max(0, dot((o.Normal), IN.viewDir)), _FresnelExp)));
			float modulate = abs(sin(_Time*30));
			emissive.r *= modulate;
			emissive.g *= modulate;
			emissive.b *= modulate;
			o.Emission = emissive;
		}
		ENDCG
	}

	FallBack "Diffuse"


}
