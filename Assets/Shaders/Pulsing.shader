Shader "Custom/Pulsing" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0.0, 1.0)) = 0.5
		_Metallic("Metallic", Range(0.0, 1.0)) = 0.0

		//_BumpScale("Scale", Float) = 1.0
		_BumpMap("Normal Map", 2D) = "bump" {}

		//_Glossiness ("Smoothness", Range(0,1)) = 0.5
		//_Metallic ("Metallic", Range(0,1)) = 0.0

		_PlanePos ("Plane Position", Vector) = (0.0, 0.0, 0.0, 0.0)
		_PlaneNormal ("Plane Normal", Vector) = (0.0, 1.0, 0.0, 0.0)

		_EmissionColor("EmissionColor", Color) = (0.4419334,0.5356163,0.9852941,0.234)
		_EmissionPower("EmissionPower", Float) = 1
		_FresnelExp("FresnelExp", Float) = 1
	}

	SubShader {
		Tags { "RenderType"="Opaque" }
		//LOD 200

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
		float4 _PlanePos;
		float4 _PlaneNormal;

		float _FresnelExp;
		float4 _EmissionColor;
		float _EmissionPower;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			float3 ToPos = IN.worldPos.xyz - _PlanePos.xyz;

			clip(dot(ToPos, _PlaneNormal.xyz));
			//clip(-1);

			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			//o.Normal = UnpackNormal(tex2D (_BumpMap, IN.uv_MainTex));
			o.Normal = UnpackScaleNormal (tex2D (_BumpMap, IN.uv_MainTex), 0.05);
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			//fresnel emissive
			float3 emissive = _EmissionPower *_EmissionColor.rgb;
			emissive *= abs(sin(_Time*30));
			o.Emission = emissive;
		}
		ENDCG
	}

	FallBack "Diffuse"


}
