Shader "gs/gsVGrid_Doc"
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
  #define VGrid_Lib_BDraw_Draw_Point	0
  #define VGrid_Lib_BDraw_Draw_Sphere	1
  #define VGrid_Lib_BDraw_Draw_Line	2
  #define VGrid_Lib_BDraw_Draw_Arrow	3
  #define VGrid_Lib_BDraw_Draw_Signal	4
  #define VGrid_Lib_BDraw_Draw_LineSegment	5
  #define VGrid_Lib_BDraw_Draw_Texture_2D	6
  #define VGrid_Lib_BDraw_Draw_Quad	7
  #define VGrid_Lib_BDraw_Draw_WebCam	8
  #define VGrid_Lib_BDraw_Draw_Mesh	9
  #define VGrid_Lib_BDraw_Draw_Number	10
  #define VGrid_Lib_BDraw_Draw_N	11
  #define VGrid_Lib_BDraw_TextAlignment_BottomLeft	0
  #define VGrid_Lib_BDraw_TextAlignment_CenterLeft	1
  #define VGrid_Lib_BDraw_TextAlignment_TopLeft	2
  #define VGrid_Lib_BDraw_TextAlignment_BottomCenter	3
  #define VGrid_Lib_BDraw_TextAlignment_CenterCenter	4
  #define VGrid_Lib_BDraw_TextAlignment_TopCenter	5
  #define VGrid_Lib_BDraw_TextAlignment_BottomRight	6
  #define VGrid_Lib_BDraw_TextAlignment_CenterRight	7
  #define VGrid_Lib_BDraw_TextAlignment_TopRight	8
  #define VGrid_Lib_BDraw_Text_QuadType_FrontOnly	0
  #define VGrid_Lib_BDraw_Text_QuadType_FrontBack	1
  #define VGrid_Lib_BDraw_Text_QuadType_Switch	2
  #define VGrid_Lib_BDraw_Text_QuadType_Arrow	3
  #define VGrid_Lib_BDraw_Text_QuadType_Billboard	4
  #define VGrid_Lib_PaletteType_Rainbow	0
  #define VGrid_Lib_PaletteType_GradientRainbow	1
  #define VGrid_Lib_PaletteType_GradientRainbow10	2
  #define VGrid_Lib_PaletteType_GradientRainbow20	3
  #define VGrid_Lib_PaletteType_Heat	4
  #define VGrid_Lib_PaletteType_GradientHeat	5
  #define VGrid_Lib_PaletteType_WhiteRainbow	6
  #define VGrid_Lib_PaletteType_invRainbow	7
  #define VGrid_Lib_PaletteType_Green	8
  #define VGrid_Lib_PaletteType_Gray	9
  #define VGrid_Lib_PaletteType_DarkGray	10
  #define VGrid_Lib_PaletteType_CT	11
  #define Views_Lib_ProjectionMode_Automatic	0
  #define Views_Lib_ProjectionMode_Perspective	1
  #define Views_Lib_ProjectionMode_Orthographic	2
  #define VGrid_Lib_BDraw_Draw_Text3D 12
  #define VGrid_Lib_BDraw_LF 10
  #define VGrid_Lib_BDraw_TB 9
  #define VGrid_Lib_BDraw_ZERO 48
  #define VGrid_Lib_BDraw_NINE 57
  #define VGrid_Lib_BDraw_PERIOD 46
  #define VGrid_Lib_BDraw_COMMA 44
  #define VGrid_Lib_BDraw_PLUS 43
  #define VGrid_Lib_BDraw_MINUS 45
  #define VGrid_Lib_BDraw_SPACE 32
  #define g gVGrid_Doc[0]
  #define VGrid_Lib_BDraw_Draw_Point	0
  #define VGrid_Lib_BDraw_Draw_Sphere	1
  #define VGrid_Lib_BDraw_Draw_Line	2
  #define VGrid_Lib_BDraw_Draw_Arrow	3
  #define VGrid_Lib_BDraw_Draw_Signal	4
  #define VGrid_Lib_BDraw_Draw_LineSegment	5
  #define VGrid_Lib_BDraw_Draw_Texture_2D	6
  #define VGrid_Lib_BDraw_Draw_Quad	7
  #define VGrid_Lib_BDraw_Draw_WebCam	8
  #define VGrid_Lib_BDraw_Draw_Mesh	9
  #define VGrid_Lib_BDraw_Draw_Number	10
  #define VGrid_Lib_BDraw_Draw_N	11
  #define VGrid_Lib_BDraw_TextAlignment_BottomLeft	0
  #define VGrid_Lib_BDraw_TextAlignment_CenterLeft	1
  #define VGrid_Lib_BDraw_TextAlignment_TopLeft	2
  #define VGrid_Lib_BDraw_TextAlignment_BottomCenter	3
  #define VGrid_Lib_BDraw_TextAlignment_CenterCenter	4
  #define VGrid_Lib_BDraw_TextAlignment_TopCenter	5
  #define VGrid_Lib_BDraw_TextAlignment_BottomRight	6
  #define VGrid_Lib_BDraw_TextAlignment_CenterRight	7
  #define VGrid_Lib_BDraw_TextAlignment_TopRight	8
  #define VGrid_Lib_BDraw_Text_QuadType_FrontOnly	0
  #define VGrid_Lib_BDraw_Text_QuadType_FrontBack	1
  #define VGrid_Lib_BDraw_Text_QuadType_Switch	2
  #define VGrid_Lib_BDraw_Text_QuadType_Arrow	3
  #define VGrid_Lib_BDraw_Text_QuadType_Billboard	4
  #define VGrid_Lib_PaletteType_Rainbow	0
  #define VGrid_Lib_PaletteType_GradientRainbow	1
  #define VGrid_Lib_PaletteType_GradientRainbow10	2
  #define VGrid_Lib_PaletteType_GradientRainbow20	3
  #define VGrid_Lib_PaletteType_Heat	4
  #define VGrid_Lib_PaletteType_GradientHeat	5
  #define VGrid_Lib_PaletteType_WhiteRainbow	6
  #define VGrid_Lib_PaletteType_invRainbow	7
  #define VGrid_Lib_PaletteType_Green	8
  #define VGrid_Lib_PaletteType_Gray	9
  #define VGrid_Lib_PaletteType_DarkGray	10
  #define VGrid_Lib_PaletteType_CT	11
  #define Views_Lib_ProjectionMode_Automatic	0
  #define Views_Lib_ProjectionMode_Perspective	1
  #define Views_Lib_ProjectionMode_Orthographic	2
  #define VGrid_Lib_BDraw_Draw_Text3D 12
  #define VGrid_Lib_BDraw_LF 10
  #define VGrid_Lib_BDraw_TB 9
  #define VGrid_Lib_BDraw_ZERO 48
  #define VGrid_Lib_BDraw_NINE 57
  #define VGrid_Lib_BDraw_PERIOD 46
  #define VGrid_Lib_BDraw_COMMA 44
  #define VGrid_Lib_BDraw_PLUS 43
  #define VGrid_Lib_BDraw_MINUS 45
  #define VGrid_Lib_BDraw_SPACE 32
  struct GVGrid_Doc
  {
    float sphere, cube, torus, box, roundBox, boxFrame, VGrid_Lib_BDraw_fontSize, VGrid_Lib_BDraw_boxThickness, VGrid_Lib_resolution, VGrid_Lib_boxLineThickness, VGrid_Lib_axesOpacity, VGrid_Lib_GridLineThickness, VGrid_Lib_opacity, VGrid_Lib_meshVal, VGrid_Lib_orthoSize, VGrid_Lib_maxDist, VGrid_Lib_minResolution;
    uint VGrid_Lib_BDraw_ABuff_IndexN, VGrid_Lib_BDraw_ABuff_BitN, VGrid_Lib_BDraw_ABuff_N, VGrid_Lib_BDraw_ABuff_BitN1, VGrid_Lib_BDraw_ABuff_BitN2, VGrid_Lib_BDraw_omitText, VGrid_Lib_BDraw_includeUnicode, VGrid_Lib_BDraw_fontInfoN, VGrid_Lib_BDraw_textN, VGrid_Lib_BDraw_textCharN, VGrid_Lib_BDraw_boxEdgeN, VGrid_Lib_drawGrid, VGrid_Lib_drawBox, VGrid_Lib_drawAxes, VGrid_Lib_customAxesRangeN, VGrid_Lib_zeroOrigin, VGrid_Lib_drawSurface, VGrid_Lib_GridDrawFront, VGrid_Lib_GridDrawBack, VGrid_Lib_show_slices, VGrid_Lib_paletteType, VGrid_Lib_twoSided, VGrid_Lib_reCalc, VGrid_Lib_buildText, VGrid_Lib_isOrtho, VGrid_Lib_showMeshVal, VGrid_Lib_showMeshRange, VGrid_Lib_showOutline, VGrid_Lib_showSurface, VGrid_Lib_showAxes, VGrid_Lib_showNormalizedAxes, VGrid_Lib_retrace;
    float4 VGrid_Lib_BDraw_boxColor;
    float2 VGrid_Lib_GridX, VGrid_Lib_GridY, VGrid_Lib_GridZ, VGrid_Lib_textSize, VGrid_Lib_paletteRange, VGrid_Lib_meshRange;
    float3 VGrid_Lib_axesRangeMin, VGrid_Lib_axesRangeMax, VGrid_Lib_axesRangeMin1, VGrid_Lib_axesRangeMax1, VGrid_Lib_axesRangeMin2, VGrid_Lib_axesRangeMax2, VGrid_Lib_axesColor, VGrid_Lib_slices, VGrid_Lib_sliceRotation;
    uint3 VGrid_Lib_nodeN;
    uint2 VGrid_Lib_viewSize;
    uint4 VGrid_Lib_viewRect;
    Matrix4x4 VGrid_Lib_camToWorld, VGrid_Lib_cameraInvProjection;
  };
  struct VGrid_Lib_BDraw_FontInfo { float2 uvBottomLeft, uvBottomRight, uvTopLeft, uvTopRight; int advance, bearing, minX, minY, maxX, maxY; };
  struct VGrid_Lib_BDraw_TextInfo { float3 p, right, up, p0, p1; float2 size, uvSize; float4 color, backColor; uint justification, textI, quadType, axis; float height; };
  struct VGrid_Lib_TRay { float3 origin, direction; float4 color; float dist; };
  struct Views_Lib_CamView { string viewName; float3 viewCenter; float viewDist; float2 viewTiltSpin; uint viewProjection; float view_sphere, view_cube, view_torus, view_box, view_roundBox, view_boxFrame; uint view_twoSided; float view_meshVal; float2 view_meshRange; };
  RWStructuredBuffer<GVGrid_Doc> gVGrid_Doc;
  RWStructuredBuffer<uint> VGrid_Lib_BDraw_tab_delimeted_text, VGrid_Lib_BDraw_ABuff_Bits, VGrid_Lib_BDraw_ABuff_Sums, VGrid_Lib_BDraw_ABuff_Indexes, VGrid_Lib_BDraw_ABuff_Fills1, VGrid_Lib_BDraw_ABuff_Fills2;
  RWStructuredBuffer<VGrid_Lib_BDraw_TextInfo> VGrid_Lib_BDraw_textInfos;
  RWStructuredBuffer<VGrid_Lib_BDraw_FontInfo> VGrid_Lib_BDraw_fontInfos;
  RWStructuredBuffer<uint2> VGrid_Lib_depthColors;
  RWStructuredBuffer<Color32> VGrid_Lib_paletteBuffer;
  RWStructuredBuffer<float> VGrid_Lib_Vals;

  public Texture2D VGrid_Lib_BDraw_fontTexture;
  Texture2D _PaletteTex;
  struct v2f { float4 pos : POSITION, color : COLOR1, ti : TEXCOORD0, tj : TEXCOORD1, tk : TEXCOORD2; float3 normal : NORMAL, p0 : TEXCOORD3, p1 : TEXCOORD4, wPos : TEXCOORD5; float2 uv : TEXCOORD6; };
  void onRenderObject_LIN(bool show, uint _itemN, inout uint i, inout uint index, inout uint3 LIN) { uint n = 0; if (show) { if (i < (n = _itemN)) LIN = uint3(index, i, 0); LIN.z += n; i -= n; } index++; }
  uint3 onRenderObject_LIN(uint i) { uint3 LIN = u000; uint index = 0; onRenderObject_LIN(g.VGrid_Lib_drawBox && g.VGrid_Lib_drawAxes, g.VGrid_Lib_BDraw_textN, i, index, LIN); onRenderObject_LIN(g.VGrid_Lib_drawBox, 12, i, index, LIN); onRenderObject_LIN(g.VGrid_Lib_drawGrid && g.VGrid_Lib_showSurface, product(g.VGrid_Lib_viewSize), i, index, LIN); return LIN; }
  VGrid_Lib_BDraw_TextInfo VGrid_Lib_BDraw_textInfo(uint i) { return VGrid_Lib_BDraw_textInfos[i]; }
  float3 VGrid_Lib_gridMin() { return float3(g.VGrid_Lib_GridX.x, g.VGrid_Lib_GridY.x, g.VGrid_Lib_GridZ.x); }
  float3 VGrid_Lib_gridMax() { return float3(g.VGrid_Lib_GridX.y, g.VGrid_Lib_GridY.y, g.VGrid_Lib_GridZ.y); }
  uint2 VGrid_Lib_pixDepthColor(uint i) { return VGrid_Lib_depthColors[i]; }
  uint2 VGrid_Lib_pixDepthColor(uint2 id) { return VGrid_Lib_pixDepthColor(id_to_i(id, g.VGrid_Lib_viewSize)); }
  VGrid_Lib_TRay VGrid_Lib_CreateShaderCameraRay(float2 _uv)
  {
    VGrid_Lib_TRay ray;
    ray.origin = mul(g.VGrid_Lib_camToWorld, g.VGrid_Lib_isOrtho ? float4(g.VGrid_Lib_orthoSize * _uv / float2(aspect(g.VGrid_Lib_viewSize), 1), 0, 1) : f0001).xyz;
    ray.direction = normalize(mul(g.VGrid_Lib_camToWorld, float4(mul(g.VGrid_Lib_cameraInvProjection, float4(_uv, 0, 1)).xyz, 0)).xyz);
    ray.color = f0000;
    ray.dist = 0;
    return ray;
  }
  float VGrid_Lib_pixDepth(uint2 dc) { return dc.x / (float)uint_max * (g.VGrid_Lib_maxDist - 2); }
  float4 VGrid_Lib_pixColor(uint2 dc) { return c32_f4(u_c32(dc.y)); }
  uint VGrid_Lib_BDraw_o_drawType(v2f o) { return roundu(o.ti.z); }
  float4 frag_VGrid_Lib_BDraw_Sphere(v2f i) { float2 uv = i.uv; float r = dot(uv, uv); float4 color = i.color; if (r > 1.0f || color.a == 0) return f0000; float3 n = new float3(uv, r - 1), _LightDir = new float3(0.321f, 0.766f, -0.557f); float lightAmp = max(0.0f, dot(n, _LightDir)); float4 diffuse_Light = (lightAmp + UNITY_LIGHTMODEL_AMBIENT) * color; float spec = max(0, (lightAmp - 0.95f) / 0.05f); color = lerp(diffuse_Light, f1111, spec / 4); color.a = 1; return color; }
  float4 frag_VGrid_Lib_BDraw_Mesh(v2f i) { float3 p = i.wPos; float4 color = i.color; color.xyz += dot(i.normal, _WorldSpaceLightPos0.xyz) / 2; return saturate(color); }
  uint VGrid_Lib_BDraw_o_i(v2f o) { return roundu(o.ti.x); }
  float4 frag_VGrid_Lib_BDraw_Text(Texture2D t, RWStructuredBuffer<uint> _text, RWStructuredBuffer<VGrid_Lib_BDraw_FontInfo> VGrid_Lib_BDraw_fontInfos, float VGrid_Lib_BDraw_fontSize, uint quadType, float4 backColor, uint2 textIs, v2f i)
  {
    float uv_x = 0;
    float4 color = backColor;
    for (uint textI = textIs.x; textI < textIs.y; textI++)
    {
      uint fontInfoI = TextByte(_text, textI) - 32;
      VGrid_Lib_BDraw_FontInfo f = VGrid_Lib_BDraw_fontInfos[fontInfoI];
      float dx = f.advance / g.VGrid_Lib_BDraw_fontSize;
      float2 mn = float2(f.minX, f.minY) / g.VGrid_Lib_BDraw_fontSize, mx = float2(f.maxX, f.maxY) / g.VGrid_Lib_BDraw_fontSize, range = mx - mn;
      if (quadType == VGrid_Lib_BDraw_Text_QuadType_Arrow) { mn.y -= 0.5f; mx.y -= 0.5f; }
      float2 uv = (i.uv - uv_x * f10 + f1_ * mn - float2(f.bearing / g.VGrid_Lib_BDraw_fontSize, 0.25f)) / range;
      if (IsNotOutside(uv, f00, f11))
      {
        if (any(f.uvTopLeft > f.uvBottomRight)) uv = uv.yx;
        uv = lerp(f.uvBottomLeft, f.uvTopRight, uv);
        color = tex2Dlod(t, float4(uv, f00));
        color.rgb = (1 - color.rgb) * i.color.rgb;
        if (color.w == 0) color = backColor;
        else color.w = i.color.w;
        break;
      }
      uv_x += dx;
    }
    return color;
  }
  uint2 VGrid_Lib_BDraw_Get_text_indexes(uint textI) { return uint2(textI == 0 ? 0 : VGrid_Lib_BDraw_ABuff_Indexes[textI - 1] + 1, textI < g.VGrid_Lib_BDraw_ABuff_IndexN ? VGrid_Lib_BDraw_ABuff_Indexes[textI] : g.VGrid_Lib_BDraw_textCharN); }
  float VGrid_Lib_BDraw_wrapJ(uint j, uint n) { return ((j + n) % 6) / 3; }
  v2f VGrid_Lib_BDraw_o_i(uint i, v2f o) { o.ti.x = i; return o; }
  v2f VGrid_Lib_BDraw_o_drawType(uint drawType, v2f o) { o.ti.z = drawType; return o; }
  v2f VGrid_Lib_BDraw_o_r(float r, v2f o) { o.ti.w = r; return o; }
  v2f VGrid_Lib_BDraw_o_color(float4 color, v2f o) { o.color = color; return o; }
  v2f VGrid_Lib_BDraw_o_normal(float3 normal, v2f o) { o.normal = normal; return o; }
  v2f VGrid_Lib_BDraw_o_uv(float2 uv, v2f o) { o.uv = uv; return o; }
  v2f VGrid_Lib_BDraw_o_pos(float4 pos, v2f o) { o.pos = pos; return o; }
  v2f vert_VGrid_Lib_BDraw_Point(float3 p, float4 color, uint i, v2f o) { return VGrid_Lib_BDraw_o_i(i, VGrid_Lib_BDraw_o_drawType(VGrid_Lib_BDraw_Draw_Point, VGrid_Lib_BDraw_o_color(color, VGrid_Lib_BDraw_o_pos(UnityObjectToClipPos(float4(p, 1)), o)))); }
  v2f vert_VGrid_Lib_DrawScreen(uint i, uint j, v2f o)
  {
    uint2 id = i_to_id(i, g.VGrid_Lib_viewSize), dc = VGrid_Lib_pixDepthColor(i);
    float2 uv = (id + f11 * 0.5f) / g.VGrid_Lib_viewSize * 2 - 1;
    VGrid_Lib_TRay ray = VGrid_Lib_CreateShaderCameraRay(uv);
    return vert_VGrid_Lib_BDraw_Point(ray.origin + VGrid_Lib_pixDepth(dc) * ray.direction, VGrid_Lib_pixColor(dc), i, o);
  }
  v2f VGrid_Lib_BDraw_o_pos_PV(float3 p, float4 q, v2f o) { return VGrid_Lib_BDraw_o_pos(mul(UNITY_MATRIX_P, mul(UNITY_MATRIX_V, float4(p, 1)) + q), o); }
  v2f VGrid_Lib_BDraw_o_pos_c(float4 c, v2f o) { return VGrid_Lib_BDraw_o_pos(UnityObjectToClipPos(c), o); }
  v2f VGrid_Lib_BDraw_o_pos_c(float3 c, v2f o) { return VGrid_Lib_BDraw_o_pos(UnityObjectToClipPos(c), o); }
  float VGrid_Lib_BDraw_o_r(v2f o) { return o.ti.w; }
  float4 frag_VGrid_Lib_BDraw_Line(v2f i) { float3 p0 = i.p0, p1 = i.p1; float lineRadius = VGrid_Lib_BDraw_o_r(i); float2 uv = i.uv; float r = dot(uv, uv), r2 = lineRadius * lineRadius; float4 color = i.color; float3 p10 = p1 - p0; float lp10 = length(p10); if (uv.x < 0) r /= r2; else if (uv.x > lp10) { uv.x -= lp10; r = dot(uv, uv) / r2; } else { uv.x = 0; r = dot(uv, uv) / r2; } if (r > 1.0f || color.a == 0) return f0000; float3 n = new float3(uv, r - 1), _LightDir = new float3(0.321f, 0.766f, -0.557f); float lightAmp = max(0.0f, dot(n, _LightDir)); float4 diffuse_Light = (lightAmp + UNITY_LIGHTMODEL_AMBIENT) * color; float spec = max(0, (lightAmp - 0.95f) / 0.05f); color = lerp(diffuse_Light, f1111, spec / 4); color.a = 1; return color; }
  float4 frag_VGrid_Lib_BDraw_Arrow(v2f i) { float3 p0 = i.p0, p1 = i.p1; float lineRadius = VGrid_Lib_BDraw_o_r(i); float2 uv = i.uv; float r = dot(uv, uv), r2 = lineRadius * lineRadius; float4 color = i.color; float3 p10 = p1 - p0; float lp10 = length(p10); if (uv.x < 0) r /= r2; else if (uv.x > lp10 - lineRadius * 3 && abs(uv.y) > lineRadius) { uv.x -= lp10; uv = rotate_sc(uv, -sign(uv.y) * 0.5f, 0.866025404f); uv.x = 0; r = dot(uv, uv) / r2; } else if (uv.x > lp10) { uv.x -= lp10; r = dot(uv, uv) / r2; } else { uv.x = 0; r = dot(uv, uv) / r2; } if (r > 1.0f || color.a == 0) return f0000; float3 n = new float3(uv, r - 1), _LightDir = new float3(0.321f, 0.766f, -0.557f); float lightAmp = max(0.0f, dot(n, _LightDir)); float4 diffuse_Light = (lightAmp + UNITY_LIGHTMODEL_AMBIENT) * color; float spec = max(0, (lightAmp - 0.95f) / 0.05f); color = lerp(diffuse_Light, f1111, spec / 4); color.a = 1; return color; }
  float4 frag_VGrid_Lib_BDraw_LineSegment(v2f i) { float3 p0 = i.p0, p1 = i.p1; float lineRadius = VGrid_Lib_BDraw_o_r(i); float2 uv = i.uv; float r = dot(uv, uv), r2 = lineRadius * lineRadius; float4 color = i.color; float3 p10 = p1 - p0; float lp10 = length(p10); uv.x = 0; r = dot(uv, uv) / r2; if (r > 1.0f || color.a == 0) return f0000; float3 n = new float3(uv, r - 1), _LightDir = new float3(0.321f, 0.766f, -0.557f); float lightAmp = max(0.0f, dot(n, _LightDir)); float4 diffuse_Light = (lightAmp + UNITY_LIGHTMODEL_AMBIENT) * color; float spec = max(0, (lightAmp - 0.95f) / 0.05f); color = lerp(diffuse_Light, f1111, spec / 4); color.a = 1; return color; }
  uint VGrid_Lib_BDraw_SignalSmpN(uint chI) { return 1024; }
  float VGrid_Lib_BDraw_SignalThickness(uint chI, uint smpI) { return 0.004f; }
  float VGrid_Lib_BDraw_SignalSmpV(uint chI, uint smpI) { return 0; }
  float4 VGrid_Lib_BDraw_SignalColor(uint chI, uint smpI) { return YELLOW; }
  float VGrid_Lib_BDraw_SignalFillCrest(uint chI, uint smpI) { return 1; }
  float4 VGrid_Lib_BDraw_SignalMarker(uint chI, float smpI) { return f0000; }
  float4 VGrid_Lib_BDraw_SignalBackColor(uint chI, uint smpI) { return float4(1, 1, 1, 0.2f); }
  float4 frag_VGrid_Lib_BDraw_Signal(v2f i)
  {
    uint chI = VGrid_Lib_BDraw_o_i(i), SmpN = VGrid_Lib_BDraw_SignalSmpN(chI);
    float2 uv = i.uv, wh = float2(distance(i.p1, i.p0), VGrid_Lib_BDraw_o_r(i));
    float smpI = lerp(0, SmpN, uv.x), y = lerp(-1, 1, uv.y), h = wh.y / wh.x * SmpN, thick = VGrid_Lib_BDraw_SignalThickness(chI, (uint)smpI) * SmpN, d = float_PositiveInfinity;
    uint SmpI = (uint)smpI, dSmpI = ceilu(thick) + 1, SmpI0 = (uint)max(0, (int)SmpI - (int)dSmpI), SmpI1 = min(SmpN - 1, SmpI + dSmpI);
    float2 p0 = float2(smpI, y * h), q0 = float2(SmpI0, (h - thick) * VGrid_Lib_BDraw_SignalSmpV(chI, SmpI0)), q1;
    for (uint sI = SmpI0; sI < SmpI1; sI++) { q1 = float2(sI + 1, (h - thick) * VGrid_Lib_BDraw_SignalSmpV(chI, sI + 1)); d = min(d, LineSegDist(q0, q1, p0)); q0 = q1; }
    float4 c = VGrid_Lib_BDraw_SignalColor(chI, SmpI);
    float v = 0.9f * lerp(VGrid_Lib_BDraw_SignalSmpV(chI, SmpI), VGrid_Lib_BDraw_SignalSmpV(chI, SmpI + 1), frac(smpI)), crest = VGrid_Lib_BDraw_SignalFillCrest(chI, SmpI);
    float4 marker = VGrid_Lib_BDraw_SignalMarker(chI, smpI);
    if (marker.w > 0) return marker;
    if (crest >= 0 ? y > crest && y < v : y < crest && y > v) return c;
    if (d < thick) return float4(c.xyz * (1 - d / thick), 1);
    return VGrid_Lib_BDraw_SignalBackColor(chI, SmpI);
  }
  float4 frag_VGrid_Lib_GS(v2f i, float4 color)
  {
    switch (VGrid_Lib_BDraw_o_drawType(i))
    {
      case uint_max: Discard(0); break;
      case VGrid_Lib_BDraw_Draw_Sphere: color = frag_VGrid_Lib_BDraw_Sphere(i); break;
      case VGrid_Lib_BDraw_Draw_Line: color = frag_VGrid_Lib_BDraw_Line(i); break;
      case VGrid_Lib_BDraw_Draw_Arrow: color = frag_VGrid_Lib_BDraw_Arrow(i); break;
      case VGrid_Lib_BDraw_Draw_Signal: color = frag_VGrid_Lib_BDraw_Signal(i); break;
      case VGrid_Lib_BDraw_Draw_LineSegment: color = frag_VGrid_Lib_BDraw_LineSegment(i); break;
      case VGrid_Lib_BDraw_Draw_Mesh: color = frag_VGrid_Lib_BDraw_Mesh(i); break;
      case VGrid_Lib_BDraw_Draw_Text3D:
        VGrid_Lib_BDraw_TextInfo t = VGrid_Lib_BDraw_textInfo(VGrid_Lib_BDraw_o_i(i));
        color = frag_VGrid_Lib_BDraw_Text(VGrid_Lib_BDraw_fontTexture, VGrid_Lib_BDraw_tab_delimeted_text, VGrid_Lib_BDraw_fontInfos, g.VGrid_Lib_BDraw_fontSize, t.quadType, t.backColor, VGrid_Lib_BDraw_Get_text_indexes(t.textI), i);
        break;
    }
    return color;
  }
  float4 frag_GS(v2f i, float4 color) { return frag_VGrid_Lib_GS(i, color); }
  v2f VGrid_Lib_BDraw_o_p0(float3 p0, v2f o) { o.p0 = p0; return o; }
  v2f VGrid_Lib_BDraw_o_p1(float3 p1, v2f o) { o.p1 = p1; return o; }
  uint2 VGrid_Lib_BDraw_JQuadu(uint j) { return uint2(j + 2, j + 1) / 3 % 2; }
  float2 VGrid_Lib_BDraw_JQuadf(uint j) { return (float2)VGrid_Lib_BDraw_JQuadu(j); }
  float2 VGrid_Lib_BDraw_Line_uv(float3 p0, float3 p1, float r, uint j) { float2 p = VGrid_Lib_BDraw_JQuadf(j); return float2(length(p1 - p0) * (1 - p.y), (1 - 2 * p.x) * r); }
  float2 VGrid_Lib_BDraw_LineArrow_uv(float dpf, float3 p0, float3 p1, float r, uint j) { float2 p = VGrid_Lib_BDraw_JQuadf(j); return float2((length(p1 - p0) + 2 * r) * (1 - p.y) - r, (1 - 2 * p.x) * r * dpf); }
  float4 VGrid_Lib_BDraw_LineArrow_p4(float dpf, float3 p0, float3 p1, float3 p3, float r, uint j) { float2 p = VGrid_Lib_BDraw_JQuadf(j); float3 dp = normalize(cross(p1 - p0, p3 - p0)) * r * dpf; return float4(p.y * (p0 - p1) + p1 + dp * (1 - 2 * p.x), 1); }
  float4 VGrid_Lib_BDraw_LineArrow_p4(float dpf, float3 p0, float3 p1, float r, uint j) { return VGrid_Lib_BDraw_LineArrow_p4(dpf, p0, p1, _WorldSpaceCameraPos, r, j); }
  v2f vert_VGrid_Lib_BDraw_Line(float3 p0, float3 p1, float r, float4 color, uint i, uint j, v2f o) { return VGrid_Lib_BDraw_o_i(i, VGrid_Lib_BDraw_o_p0(p0, VGrid_Lib_BDraw_o_p1(p1, VGrid_Lib_BDraw_o_r(r, VGrid_Lib_BDraw_o_drawType(VGrid_Lib_BDraw_Draw_Line, VGrid_Lib_BDraw_o_color(color, VGrid_Lib_BDraw_o_uv(VGrid_Lib_BDraw_Line_uv(p0, p1, r, j), VGrid_Lib_BDraw_o_pos_c(VGrid_Lib_BDraw_LineArrow_p4(1, p0, p1, r, j), o)))))))); }
  v2f vert_VGrid_Lib_BDraw_BoxFrame(float3 c0, float3 c1, float lineRadius, float4 color, uint i, uint j, v2f o) { float3 p0, p1; switch (i) { case 0: p0 = c0; p1 = c0 * f110 + c1 * f001; break; case 1: p0 = c0 * f110 + c1 * f001; p1 = c0 * f100 + c1 * f011; break; case 2: p0 = c0 * f100 + c1 * f011; p1 = c0 * f101 + c1 * f010; break; case 3: p0 = c0 * f101 + c1 * f010; p1 = c0; break; case 4: p0 = c0 * f011 + c1 * f100; p1 = c0 * f010 + c1 * f101; break; case 5: p0 = c0 * f010 + c1 * f101; p1 = c1; break; case 6: p0 = c1; p1 = c0 * f001 + c1 * f110; break; case 7: p0 = c0 * f001 + c1 * f110; p1 = c0 * f011 + c1 * f100; break; case 8: p0 = c0; p1 = c0 * f011 + c1 * f100; break; case 9: p0 = c0 * f101 + c1 * f010; p1 = c0 * f001 + c1 * f110; break; case 10: p0 = c0 * f100 + c1 * f011; p1 = c1; break; default: p0 = c0 * f110 + c1 * f001; p1 = c0 * f010 + c1 * f101; break; } return vert_VGrid_Lib_BDraw_Line(p0, p1, lineRadius, color, i, j, o); }
  v2f vert_VGrid_Lib_BDraw_Box(uint i, uint j, v2f o) { return vert_VGrid_Lib_BDraw_BoxFrame(VGrid_Lib_gridMin(), VGrid_Lib_gridMax(), g.VGrid_Lib_boxLineThickness, DARK_BLUE, i, j, o); }
  v2f vert_VGrid_Lib_BDraw_LineArrow(float dpf, float3 p0, float3 p1, float r, float4 color, uint i, uint j, v2f o) { return VGrid_Lib_BDraw_o_i(i, VGrid_Lib_BDraw_o_p0(p0, VGrid_Lib_BDraw_o_p1(p1, VGrid_Lib_BDraw_o_r(r, VGrid_Lib_BDraw_o_drawType(dpf == 1 ? VGrid_Lib_BDraw_Draw_Line : VGrid_Lib_BDraw_Draw_Arrow, VGrid_Lib_BDraw_o_color(color, VGrid_Lib_BDraw_o_uv(VGrid_Lib_BDraw_LineArrow_uv(dpf, p0, p1, r, j), VGrid_Lib_BDraw_o_pos_c(VGrid_Lib_BDraw_LineArrow_p4(dpf, p0, p1, r, j), o)))))))); }
  v2f vert_VGrid_Lib_BDraw_Arrow(float3 p0, float3 p1, float r, float4 color, uint i, uint j, v2f o) { return vert_VGrid_Lib_BDraw_LineArrow(3, p0, p1, r, color, i, j, o); }
  v2f vert_VGrid_Lib_BDraw_Text(VGrid_Lib_BDraw_TextInfo t, uint i, uint j, v2f o)
  {
    float3 p = t.p, p0 = t.p0, p1 = t.p1, right = t.right, up = t.up;
    float h = t.height;
    float2 uvSize = t.uvSize;
    float4 color = t.color;
    uint quadType = t.quadType, justification = t.justification, axis = t.axis;
    float w = h * uvSize.x;
    float3 jp = f000;
    if (quadType == VGrid_Lib_BDraw_Text_QuadType_Billboard)
    {
      o.wPos = p;
      switch (justification)
      {
        case VGrid_Lib_BDraw_TextAlignment_BottomLeft: jp = w / 2 * right + h / 2 * up; break;
        case VGrid_Lib_BDraw_TextAlignment_CenterLeft: jp = w / 2 * right; break;
        case VGrid_Lib_BDraw_TextAlignment_TopLeft: jp = w / 2 * right - h / 2 * up; break;
        case VGrid_Lib_BDraw_TextAlignment_BottomCenter: jp = h / 2 * up; break;
        case VGrid_Lib_BDraw_TextAlignment_CenterCenter: break;
        case VGrid_Lib_BDraw_TextAlignment_TopCenter: jp = -h / 2 * up; break;
        case VGrid_Lib_BDraw_TextAlignment_BottomRight: jp = -w / 2 * right + h / 2 * up; break;
        case VGrid_Lib_BDraw_TextAlignment_CenterRight: jp = -w / 2 * right; break;
        case VGrid_Lib_BDraw_TextAlignment_TopRight: jp = -w / 2 * right - h / 2 * up; break;
      }
      float4 billboardQuad = float4((VGrid_Lib_BDraw_wrapJ(j, 2) - 0.5f) * w, (0.5f - VGrid_Lib_BDraw_wrapJ(j, 1)) * h, 0, 0);
      o = VGrid_Lib_BDraw_o_i(i, VGrid_Lib_BDraw_o_drawType(VGrid_Lib_BDraw_Draw_Text3D, VGrid_Lib_BDraw_o_r(i, VGrid_Lib_BDraw_o_color(color, VGrid_Lib_BDraw_o_pos_PV(p, billboardQuad + float4(jp, 0), VGrid_Lib_BDraw_o_normal(f00_, VGrid_Lib_BDraw_o_uv(float2(VGrid_Lib_BDraw_wrapJ(j, 2), VGrid_Lib_BDraw_wrapJ(j, 4)) * uvSize, o)))))));
    }
    else if (quadType == VGrid_Lib_BDraw_Text_QuadType_Arrow)
    {
      o.wPos = p;
      right = normalize(p1 - p0);
      up = f010;
      jp = w / 2 * right;
      float3 q0 = p - jp, q1 = p + jp;
      o = VGrid_Lib_BDraw_o_uv(dot(p - _WorldSpaceCameraPos, cross(right, up)) < 0 ? float2(length(q1 - q0) / h * VGrid_Lib_BDraw_wrapJ(j, 1), VGrid_Lib_BDraw_wrapJ(j, 2) - 0.5f) : float2(length(q1 - q0) / h * (1 - VGrid_Lib_BDraw_wrapJ(j, 1)), 0.5f - VGrid_Lib_BDraw_wrapJ(j, 2)), VGrid_Lib_BDraw_o_drawType(VGrid_Lib_BDraw_Draw_Text3D, vert_VGrid_Lib_BDraw_Arrow(q0, q1, h * 0.165f, color, i, j, o)));
    }
    else if (quadType == VGrid_Lib_BDraw_Text_QuadType_FrontBack || quadType == VGrid_Lib_BDraw_Text_QuadType_Switch || dot(p - _WorldSpaceCameraPos, cross(right, up)) > 0)
    {
      bool draw = true;
      if (_WorldSpaceCameraPos.y > p1.y)
      {
        if ((axis == 400 && _WorldSpaceCameraPos.z < p0.z) || (axis == 300 && _WorldSpaceCameraPos.z > p1.z)) draw = false;
        if ((axis == 3 && _WorldSpaceCameraPos.x > p1.x) || (axis == 2 && _WorldSpaceCameraPos.x < p0.x)) draw = false;
        if (axis == 100 && _WorldSpaceCameraPos.z > p0.z) draw = false;
        if (axis == 200 && _WorldSpaceCameraPos.z < p1.z) draw = false;
        if (axis == 4 && _WorldSpaceCameraPos.x < p1.x) draw = false;
        if (axis == 1 && _WorldSpaceCameraPos.x > p0.x) draw = false;
      }
      else if (_WorldSpaceCameraPos.y < p0.y)
      {
        if ((axis == 100 && _WorldSpaceCameraPos.z < p0.z) || (axis == 200 && _WorldSpaceCameraPos.z > p1.z)) draw = false;
        if ((axis == 4 && _WorldSpaceCameraPos.x > p1.x) || (axis == 1 && _WorldSpaceCameraPos.x < p0.x)) draw = false;
        if (axis == 400 && _WorldSpaceCameraPos.z > p0.z) draw = false;
        if (axis == 300 && _WorldSpaceCameraPos.z < p1.z) draw = false;
        if (axis == 3 && _WorldSpaceCameraPos.x < p1.x) draw = false;
        if (axis == 2 && _WorldSpaceCameraPos.x > p0.x) draw = false;
      }
      else
      {
        if ((axis == 400 && _WorldSpaceCameraPos.z > p0.z) || (axis == 300 && _WorldSpaceCameraPos.z < p1.z)) draw = false;
        if ((axis == 3 && _WorldSpaceCameraPos.x < p1.x) || (axis == 2 && _WorldSpaceCameraPos.x > p0.x)) draw = false;
        if ((axis == 100 && _WorldSpaceCameraPos.z > p0.z) || (axis == 200 && _WorldSpaceCameraPos.z < p1.z)) draw = false;
        if ((axis == 4 && _WorldSpaceCameraPos.x < p1.x) || (axis == 1 && _WorldSpaceCameraPos.x > p0.x)) draw = false;
      }
      if (axis == 10 && _WorldSpaceCameraPos.x > p0.x && _WorldSpaceCameraPos.z > p0.z) draw = false;
      else if (axis == 10 && _WorldSpaceCameraPos.x < p0.x && _WorldSpaceCameraPos.z < p0.z) draw = false;
      else if (axis == 20 && _WorldSpaceCameraPos.x > p1.x && _WorldSpaceCameraPos.z < p0.z) draw = false;
      else if (axis == 20 && _WorldSpaceCameraPos.x < p1.x && _WorldSpaceCameraPos.z > p0.z) draw = false;
      else if (axis == 30 && _WorldSpaceCameraPos.x > p1.x && _WorldSpaceCameraPos.z > p1.z) draw = false;
      else if (axis == 30 && _WorldSpaceCameraPos.x < p1.x && _WorldSpaceCameraPos.z < p1.z) draw = false;
      else if (axis == 40 && _WorldSpaceCameraPos.x < p0.x && _WorldSpaceCameraPos.z > p1.z) draw = false;
      else if (axis == 40 && _WorldSpaceCameraPos.x > p0.x && _WorldSpaceCameraPos.z < p1.z) draw = false;
      if (draw)
      {
        o.wPos = p;
        switch (justification)
        {
          case VGrid_Lib_BDraw_TextAlignment_BottomLeft: break;
          case VGrid_Lib_BDraw_TextAlignment_CenterLeft: jp = -h / 2 * up; break;
          case VGrid_Lib_BDraw_TextAlignment_TopLeft: jp = -h * up; break;
          case VGrid_Lib_BDraw_TextAlignment_BottomCenter: jp = -w / 2 * right; break;
          case VGrid_Lib_BDraw_TextAlignment_CenterCenter: jp = -w / 2 * right - h / 2 * up; break;
          case VGrid_Lib_BDraw_TextAlignment_TopCenter: jp = -w / 2 * right - h * up; break;
          case VGrid_Lib_BDraw_TextAlignment_BottomRight: jp = -w * right; break;
          case VGrid_Lib_BDraw_TextAlignment_CenterRight: jp = -w * right - h / 2 * up; break;
          case VGrid_Lib_BDraw_TextAlignment_TopRight: jp = -w * right - h * up; break;
        }
        float3 q0 = p + jp, q1 = q0 + right * w, p2 = q1 + up * h, p3 = q0 + up * h;
        o = VGrid_Lib_BDraw_o_i(i, VGrid_Lib_BDraw_o_drawType(VGrid_Lib_BDraw_Draw_Text3D, VGrid_Lib_BDraw_o_r(i, VGrid_Lib_BDraw_o_color(color, VGrid_Lib_BDraw_o_pos_c(o.wPos = j % 5 == 0 ? p3 : j == 1 ? p2 : j == 4 ? q0 : q1, VGrid_Lib_BDraw_o_normal(cross(right, up), VGrid_Lib_BDraw_o_uv(quadType == VGrid_Lib_BDraw_Text_QuadType_Switch && dot(p - _WorldSpaceCameraPos, cross(right, up)) < 0 ? float2(1 - VGrid_Lib_BDraw_wrapJ(j, 2), VGrid_Lib_BDraw_wrapJ(j, 4)) * uvSize : float2(VGrid_Lib_BDraw_wrapJ(j, 2), VGrid_Lib_BDraw_wrapJ(j, 4)) * uvSize, o)))))));
      }
    }
    return o;
  }
  v2f vert_VGrid_Lib_BDraw_Text(uint i, uint j, v2f o) { return vert_VGrid_Lib_BDraw_Text(VGrid_Lib_BDraw_textInfo(i), i, j, o); }
  v2f vert_GS(uint i, uint j, v2f o)
  {
    uint3 LIN = onRenderObject_LIN(i); int index = -1, level = ((int)LIN.x); i = LIN.y;
    if (level == ++index) { o = vert_VGrid_Lib_BDraw_Text(i, j, o); o.tj.x = 0; }
    else if (level == ++index) { o = vert_VGrid_Lib_BDraw_Box(i, j, o); o.tj.x = 0; }
    else if (level == ++index) { o = vert_VGrid_Lib_DrawScreen(i, j, o); o.tj.x = 0; }
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