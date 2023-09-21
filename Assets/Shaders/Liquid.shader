
Shader "Olik/LiquidUnlit"
{
    Properties
    {
        _SideColor ("Side Color", Color) = (1,1,1,1)
        _TopColor ("Top Color", Color) = (1,1,1,1)
        _NoiseTex (" Wave Texture", 2D) = "white" {}
        _BubbleTex("Bubble Texture", 2D) = "white" {}
        _BubbleSpeed("Bubble Speed",float) = 1
        _LiquidFill("Fill",Range(0,1)) = 0.5
        _NoiseAmount("Noise Amount",Range(0,1)) = 0
        _FresnelAmount("Fresnel",Range(0.25,16)) = 1
        _FresnelColor("Fresnel Color",Color) = (1,1,1,1)
        _LiquidTexColor("Liquid Texture",Color) = (1,1,1,1)
        _LiquidTexAmount("Liquid Tex Amount",Range(0,1)) = .5

        /*_OffsetZ("Z Offset",Range(-1,1)) = 0
        _OffsetX("X Offset",Range(-1,1)) = 0*/
        _Length("length",float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Cull off

        GrabPass
        {
            "_BackgroundTexture"
        }


        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                UNITY_FOG_COORDS(2)
                float4 vertex : SV_POSITION;
                float4 objPos : TEXCOORD3;
                float3 normal : TEXCOORD4;
                float3 viewDir : TEXCOORD5;
                float4 grabPos : TEXCOORD6;
            };

            sampler2D _NoiseTex;
            sampler2D _BubbleTex;
            float4 _NoiseTex_ST;
            float4 _BubbleTex_ST;
            float _LiquidFill;
            float4 _SideColor;
            float4 _TopColor;
            float _OffsetX;
            float _OffsetZ;
            float _Length;
            float _NoiseAmount;
            float _BubbleSpeed;
            float _FresnelAmount;
            float4 _FresnelColor;
            float4 _LiquidTexColor;
            float _LiquidTexAmount;
            sampler2D _BackgroundTexture;


            float getfill(float y)
            {
                return step(1-_LiquidFill,(1-(y/_Length * 0.5+0.5)));
            }

            float4 RotateAroundZInDegrees (float4 vertex, float degrees)
            {
                float alpha = degrees * UNITY_PI / 180.0;
                float sina, cosa;
                sincos(alpha, sina, cosa);
                float2x2 m = float2x2(cosa, -sina, sina, cosa);
                return float4(mul(m, vertex.xy), vertex.zw).zxyw;
            }
            float4 RotateAroundXInDegrees (float4 vertex, float degrees)
            {
                float alpha = degrees * UNITY_PI / 180.0;
                float sina, cosa;
                sincos(alpha, sina, cosa);
                float2x2 m = float2x2(cosa, -sina, sina, cosa);
                return float4(mul(m, vertex.zy), vertex.zw).zxyw;
            }

            v2f vert (appdata v)
            {
                v2f o;

                o.vertex = UnityObjectToClipPos(v.vertex);

                float4 wPos = mul(unity_ObjectToWorld,v.vertex);
                float4 wObjPos = mul(unity_ObjectToWorld, float4(0,0,0,1));
                float4 finalPos = wPos - wObjPos;

                float4 posRotX = RotateAroundXInDegrees(finalPos,-90 * (1+_OffsetX));
                float4 posRotZ = RotateAroundZInDegrees(finalPos,-90 * (1+_OffsetZ));

                o.normal = UnityObjectToWorldNormal(v.normal);
                o.viewDir = normalize(ObjSpaceViewDir(v.vertex));
                o.grabPos = ComputeGrabScreenPos(o.vertex);

                o.objPos = finalPos + posRotX + posRotZ;

                _NoiseTex_ST.z += _Time.y*.2;
                _BubbleTex_ST.w -= _Time.y * _BubbleSpeed;
                o.uv0 = TRANSFORM_TEX(v.uv0, _NoiseTex);
                o.uv1 = TRANSFORM_TEX(v.uv1, _BubbleTex);
                
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i, fixed facing : VFACE) : SV_Target
            {

                fixed4 bgcolor = tex2Dproj(_BackgroundTexture, i.grabPos);
                float fresnel = dot(i.normal, i.viewDir);
                fresnel = saturate(1 - fresnel);
                fresnel = pow(fresnel, _FresnelAmount);
                fixed4 fresnelColor = fresnel * _FresnelColor;

                //float4 noiseTex = tex2D(_NoiseTex, i.uv) * _NoiseAmount * abs(_OffsetX+_OffsetZ);
                float4 noiseTex = tex2D(_NoiseTex, i.uv0) * _NoiseAmount;

                float4 bubbleTex = tex2D(_BubbleTex, i.uv1) ;
                float4 lqTex = lerp( float4(1,1,1,1),lerp(float4(1,1,1,1),_LiquidTexColor,bubbleTex), _LiquidTexAmount); 
                fixed4 sideCol =  _SideColor * lqTex  + fresnelColor;
                //sideCol = lerp(sideCol,bgcolor,.5);
                fixed4 topCol =  _TopColor  ;

                

                UNITY_APPLY_FOG(i.fogCoord, col);
                clip(getfill(i.objPos.y + noiseTex.x)-0.1f);
                return facing > 0 ? sideCol : topCol;
            }
            ENDCG
        }
    }
}
