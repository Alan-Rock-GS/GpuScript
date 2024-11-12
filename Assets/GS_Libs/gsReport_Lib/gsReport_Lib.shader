Shader "gs/gsReport_Lib"
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
  #define InsertType_No	0
  #define InsertType_Insert	1
  #define InsertType_Append	2
  #define g gReport_Lib[0]
  #define InsertType_No	0
  #define InsertType_Insert	1
  #define InsertType_Append	2
  struct GReport_Lib
  {
    uint has_importFiles, recordCommand, insertType, commentCommand, includeAnimations, displayReportCommands, displayCodeNotes, show_Chinese, show_French, show_German, show_Italian, show_Japanese, show_Russian, show_Spanish, language_English, language_Chinese, language_French, language_German, language_Italian, language_Japanese, language_Russian, language_Spanish, all_html, build, translate, untranslate, drawMouseRect;
    int insertAtLine;
    float3 mouseP0, mouseP1, mouseP2, mouseP3;
  };
  RWStructuredBuffer<GReport_Lib> gReport_Lib;

  Texture2D _PaletteTex;
  struct v2f { float4 pos : POSITION, color : COLOR1, ti : TEXCOORD0, tj : TEXCOORD1, tk : TEXCOORD2; float3 normal : NORMAL, p0 : TEXCOORD3, p1 : TEXCOORD4, wPos : TEXCOORD5; float2 uv : TEXCOORD6; };
  float4 frag_GS(v2f i, float4 color)
  {
    uint libI = roundu(i.tj.x);
    return color;
  }
  void onRenderObject_LIN(bool show, uint _itemN, inout uint i, inout uint index, inout uint3 LIN) { uint n = 0; if (show) { if (i < (n = _itemN)) LIN = uint3(index, i, 0); LIN.z += n; i -= n; } index++; }
  uint3 onRenderObject_LIN(uint i) { uint3 LIN = u000; uint index = 0; onRenderObject_LIN(g.drawMouseRect, 1, i, index, LIN); return LIN; }
  float wrapJ(uint j, uint n) { return ((j + n) % 6) / 3; }
  v2f vert_Draw_Quad(float3 p0, float3 p1, float3 p2, float3 p3, float4 color, uint i, uint j, v2f o) { float3 p = o.wPos = j % 5 == 0 ? p3 : j == 1 ? p2 : j == 4 ? p0 : p1, n = cross(p1 - p0, p0 - p3); o.color = color; o.pos = UnityObjectToClipPos(p); o.uv = float2(wrapJ(j, 2), wrapJ(j, 4)); o.normal = n; o.ti = float4(i, 0, 106, 0); return o; }
  v2f vert_Draw_Mouse_Rect(uint i, uint j, v2f o)
  {
    return vert_Draw_Quad(g.mouseP0, g.mouseP1, g.mouseP2, g.mouseP3, float4(0, 1, -0.1f, 0.25f), i, j, o);
  }
  v2f vert_GS(uint i, uint j, v2f o)
  {
    uint3 LIN = onRenderObject_LIN(i); int index = -1, level = ((int)LIN.x); i = LIN.y;
    if (level == ++index) { o = vert_Draw_Mouse_Rect(i, j, o); o.tj.x = 0; }
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