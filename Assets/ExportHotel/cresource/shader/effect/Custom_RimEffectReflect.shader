Shader "Custom/RimEffectReflect" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_RimColor ("Rim Color", Vector) = (0.5,0.5,0.5,0.5)
		_InnerColor ("Inner Color", Vector) = (0.5,0.5,0.5,0.5)
		_InnerColorPower ("Inner Color Power", Range(0, 1)) = 0.5
		_RimPower ("Rim Power", Range(0, 5)) = 2.5
		_AlphaPower ("Alpha Rim Power", Range(0, 8)) = 4
		_AllPower ("All Power", Range(0, 5)) = 1
		_MaskColor ("Mask Color", Vector) = (1,1,1,1)
		_ReflectColor ("Reflection Color", Vector) = (1,1,1,1)
		_Offset ("Offset", Range(0, 0.3)) = 0
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
}