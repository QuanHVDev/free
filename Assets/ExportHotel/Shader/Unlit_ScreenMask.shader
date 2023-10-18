Shader "Unlit/ScreenMask" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		posX ("x", Float) = 0.5
		posY ("y", Float) = 0.5
		width ("width", Float) = 100
		blur ("blur", Float) = 2
		box ("box", Float) = 0
		boxX ("boxX", Float) = 0.5
		boxY ("boxY", Float) = 0.5
		boxW ("boxW", Float) = 100
		boxH ("boxH", Float) = 100
		boxBlur ("boxBlur", Float) = 0.2
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