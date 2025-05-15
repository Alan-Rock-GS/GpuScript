Shader "gs/gsTCloud_Doc"
{
  SubShader
  {
    Blend SrcAlpha OneMinusSrcAlpha
    Cull Off
    Pass
    {
      CGPROGRAM
        #pragma target 5.0
        #pragma vertex vert
        #pragma fragment frag
        #include "UnityCG.cginc"
        #include "Lighting.cginc"
        #include "../../GS/GS_Shader.cginc"
  #define Views_ProjectionMode_Automatic	0
  #define Views_ProjectionMode_Perspective	1
  #define Views_ProjectionMode_Orthographic	2
  #define MapType_Plane	0
  #define MapType_Sphere	1
  #define g gTCloud_Doc[0]
  #define Views_ProjectionMode_Automatic	0
  #define Views_ProjectionMode_Perspective	1
  #define Views_ProjectionMode_Orthographic	2
  #define MapType_Plane	0
  #define MapType_Sphere	1
  struct GTCloud_Doc
  {
    float Latitude, Longitude;
    uint LongLatPntN;
  };
  struct LatLongPnt { string LatLongPnt_Str; float LatLongPnt_latitude, LatLongPnt_longitude; };
  struct Views_CamView { string viewName; float3 viewCenter; float viewDist; float2 viewTiltSpin; uint viewProjection; uint mapType; };
  RWStructuredBuffer<GTCloud_Doc> gTCloud_Doc;
  RWStructuredBuffer<float2> LongLatPnts;

  Texture2D _PaletteTex;
  struct v2f { float4 pos : POSITION, color : COLOR1, ti : TEXCOORD0, tj : TEXCOORD1, tk : TEXCOORD2; float3 normal : NORMAL, p0 : TEXCOORD3, p1 : TEXCOORD4, wPos : TEXCOORD5; float2 uv : TEXCOORD6; };
  float4 frag_GS(v2f i, float4 color)
  {
    uint libI = roundu(i.tj.x);
    return color;
  }
  v2f vert_Draw_LongLat_Points(uint i, uint j, v2f o) { return o; }
  void onRenderObject_LIN(bool show, uint _itemN, inout uint i, inout uint index, inout uint3 LIN) { uint n = 0; if (show) { if (i < (n = _itemN)) LIN = uint3(index, i, 0); LIN.z += n; i -= n; } index++; }
  void onRenderObject_LIN(uint _itemN, inout uint i, inout uint index, inout uint3 LIN) { onRenderObject_LIN(true, _itemN, i, index, LIN); }
  uint3 onRenderObject_LIN(uint i) { uint3 LIN = u000; uint index = 0; onRenderObject_LIN(g.LongLatPntN, i, index, LIN); return LIN; }
  v2f vert_GS(uint i, uint j, v2f o)
  {
    uint3 LIN = onRenderObject_LIN(i); int index = -1, level = ((int)LIN.x); i = LIN.y;
    if (level == ++index) { o = vert_Draw_LongLat_Points(i, j, o); o.tj.x = 0; }
    return o;
  }
  float4 frag(v2f i) : SV_Target
  {
    float4 color = i.color;
    color = frag_GS(i, color);
    if (color.a == 0) Discard(0);
    return color;
  }
  v2f vert(uint i : SV_InstanceID, uint j : SV_VertexID)
  {
    v2f o;
    o.pos = o.color = o.tj = o.tk = f0000; o.normal = o.p0 = o.p1 = o.wPos = f000; o.uv = f00; o.ti = float4(i, j, 0, 0);
    return vert_GS(i, j, o);
  }
      ENDCG
    }
  }
    Fallback Off
}