Shader "Unlit/Barrier"
{
	Properties
	{
		_MainColor("MainColor", Color) = (0.5, 0.5, 0.5, 1)
		_MainTex ("Texture", 2D) = "white" {}
		// Fresnel Power
		_Fresnel("Fresnel Intensity", Range(0,150)) = 3.0
		// Fresnel Width determines visible
		_FresnelWidth("Fresnel Width", Range(0,2)) = 3.0
		_ScrollSpeedValueU("Scroll U", float) = 0
		_ScrollSpeedValueV("Scroll V", float) = 0
	}
	SubShader
	{ 
		// Transparent Render Setup
		Tags{ "Queue" = "Overlay" "IgnoreProjector" = "True" "RenderType" = "Transparent" }

		GrabPass{ "_GrabPassTex" }
		Pass
		{
			// UnityDoc https://docs.unity3d.com/Manual/SL-Blend.html
			Blend SrcAlpha OneMinusSrcAlpha

			// Off Disables culling - all faces are drawn. Used for special effects.
			Cull Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float4 normal: NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float3 normal :TEXCOORD1;
				float4 screenPos: TEXCOORD2;
			};

			// Sampler Textures Setup
			sampler2D _MainTex, _CameraDepthTexture, _GrabPassTex;
			// Transform _ST && _TexelSize determines -> Vector4(1 / width, 1 / height, width, height)
			float4 _MainTex_ST,_MainColor,_GrabPassTex_ST, _GrabPassTex_TexelSize;
			// Pass float variables by reference of properties
			float _Fresnel, _FresnelWidth, _ScrollSpeedValueU, _ScrollSpeedValueV;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);

				// Scroll uv
				o.uv.x += _ScrollSpeedValueU * _Time;
				o.uv.y -= _ScrollSpeedValueV * _Time;

				// Fresnel Equation
				float3 viewDir = normalize(ObjSpaceViewDir(v.vertex));
				float dotProduct = 1 - saturate(dot(v.normal, viewDir));
				o.normal = smoothstep(1 - _FresnelWidth, 1.0, dotProduct) * 0.5;
				o.screenPos = ComputeScreenPos(o.vertex);

				// Eye space depth of the vertex 
				COMPUTE_EYEDEPTH(o.screenPos.z);
				return o;
			}
			
			float4 frag (v2f i,float face : VFACE) : SV_Target
			{
				// Intersection
				float intersection = saturate((abs(LinearEyeDepth(tex2Dproj(_CameraDepthTexture, i.screenPos).r) - i.screenPos.z)) / 0.1);
				float3 main = tex2D(_MainTex, i.uv);
				
				// Distortion
				i.screenPos.xy += (main.rg * 2 - 1) * 0.5 * _GrabPassTex_TexelSize.xy;
				float3 distortionColor = tex2Dproj(_GrabPassTex, i.screenPos);
				distortionColor *= _MainColor * _MainColor.a + 1;

				// Intersection hightlight
				i.normal *= intersection * clamp(0,1,face);
				main *= _MainColor * pow(_Fresnel,i.normal) ;
				
				// Lerp Distortion color & Fresnel color
				main = lerp(distortionColor, main, i.normal.r);
				main += (1 - intersection) * (face > 0 ? 0.03: 0.3) * _MainColor * _Fresnel;
				return float4(main, 0.7);
			}
			ENDCG
		}
	}
}
