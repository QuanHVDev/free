Shader "Hidden/UIElements/EditorUIE" {
	Properties {
		[HideInInspector] _MainTex ("Atlas", 2D) = "white" {}
		[HideInInspector] _FontTex ("Font", 2D) = "black" {}
		[HideInInspector] _CustomTex ("Custom", 2D) = "black" {}
		[HideInInspector] _Color ("Tint", Vector) = (1,1,1,1)
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
}