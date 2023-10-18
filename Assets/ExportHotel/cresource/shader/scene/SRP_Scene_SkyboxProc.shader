Shader "SRP/Scene/SkyboxProc" {
	Properties {
		[Header(Horizon Settings)] _OffsetHorizon ("地平线位移", Range(-1, 1)) = -0.7
		_HorizonIntensity ("地平线强度", Range(0, 10)) = 1
		_SunSet ("太阳升起颜色", Vector) = (1,0.8,1,1)
		_HorizonColorDay ("白天地平线颜色", Vector) = (0,0.8,1,1)
		_HorizonColorNight ("夜晚地平线颜色", Vector) = (0,0.8,1,1)
		[Header(Day Sky Settings)] _DayTopColor ("白天顶部颜色", Vector) = (0.4,1,1,1)
		_DayBottomColor ("白天底部颜色", Vector) = (0,0.8,1,1)
		[Header(Night Sky Settings)] _NightTopColor ("夜晚顶部天空颜色", Vector) = (0,0,0,1)
		_NightBottomColor ("夜晚底部天空颜色", Vector) = (0,0,0.2,1)
		[Header(Main Cloud Settings)] _BaseNoise ("云朵基础遮罩", 2D) = "black" {}
		_Distort ("云朵第二层遮罩", 2D) = "black" {}
		_SecNoise ("云朵第三层遮罩", 2D) = "black" {}
		_Distortion ("扭曲效果", Range(0, 1)) = 0.1
		_Speed ("云朵移动速度", Range(0, 10)) = 1.4
		_CloudCutoff ("云朵裁剪范围", Range(0, 1)) = 0.3
		_Fuzziness ("云朵削弱亮度", Range(0, 1)) = 0.04
		_FuzzinessUnder ("云朵底部削弱", Range(0, 1)) = 0.01
		[Toggle(FUZZY)] _FUZZY ("基础云朵是否扭曲", Float) = 1
		_Brightness ("云朵亮度", Range(1, 10)) = 2.5
		_CoverSunMoonStars ("云朵对太阳月亮的遮挡", Range(0, 3)) = 1
		[Header(Day Clouds Color Settings)] _CloudColorDayEdge ("白天云朵边缘颜色", Vector) = (1,1,1,1)
		_CloudColorDayMain ("白天云朵内部颜色", Vector) = (0.8,0.9,0.8,1)
		_CloudColorDayUnder ("白天云朵内部颜色穿插", Vector) = (0.6,0.7,0.6,1)
		[Header(Night Clouds Color Settings)] _CloudColorNightEdge ("夜晚云朵边缘颜色", Vector) = (0,1,1,1)
		_CloudColorNightMain ("夜晚云朵内部颜色", Vector) = (0,0.2,0.8,1)
		_CloudColorNightUnder ("夜晚云朵内部颜色穿插", Vector) = (0,0.2,0.6,1)
		[Header(Sun Settings)] [HDR] _SunColor ("太阳颜色", Vector) = (1,1,1,1)
		_SunRadius ("太阳半径", Range(0, 2)) = 0.1
		_SunFilling ("太阳缩放范围调整", Range(0, 5)) = 1
		_SunDirection ("太阳方向", Vector) = (0,-1,0,1)
		[Header(Moon Settings)] [HDR] _MoonColor ("月亮颜色", Vector) = (1,1,1,1)
		_MoonRadius ("月亮半径", Range(0, 2)) = 0.1
		_MoonOffset ("月亮位移", Range(-1, 1)) = -0.1
		_MoonFilling ("月亮缩放范围调整", Range(0, 10)) = 1
		_MoonFilling2 ("遮罩月亮缩放范围调整", Range(0, 10)) = 1
		_MoonDirection ("月亮方向", Vector) = (0,1,0,1)
		[Header(Stars Settings)] _Stars ("星星贴图", 2D) = "black" {}
		_StarsCutoff ("星星裁剪", Range(0.01, 1)) = 0.08
		_StarsSpeed ("星星位移速度", Range(0, 1)) = 0.3
		_StarsSkyColor ("星星天空映射颜色", Vector) = (0,0.2,0.1,1)
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = 1;
		}
		ENDCG
	}
}