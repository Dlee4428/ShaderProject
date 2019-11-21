Shader "Hidden/HSCI"
{
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			HLSLPROGRAM
			#pragma vertex VertDefault
			#pragma fragment Frag
			#include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

			uniform sampler2D _MainTex;

			//uniform float _Hue;
			//uniform float _Saturation;
			//uniform float _Contrast;
			//uniform float _Invert;

			uniform float _Power;
			uniform float _Speed;
			uniform float _Scale;

			float4 Frag(VaryingsDefault i) : SV_Target
			{
				float2 offset = float2(_Power * sin(_Time.w + i.texcoord.x * 10), _Power * cos(_Time.w + i.texcoord.y * 10));

				offset = (i.texcoord - 0.5) * 2; 

				float4 col = tex2D(_MainTex, i.texcoord);
				float4 colA = tex2D(_MainTex, i.texcoord + offset * _Power * sin((_Time.w * _Speed) + i.texcoord.x * _Scale));
				float4 colB = tex2D(_MainTex, i.texcoord - offset * _Power * cos((_Time.w * _Speed) + i.texcoord.y * _Scale));

				return float4(colA.r, col.g, colB.b, col.a);
			}
			ENDHLSL
		}
	}
}

