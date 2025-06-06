using GpuScript;
using UnityEngine;
class gsAxes_Lib_GS : _GS
{
  gsBDraw BDraw;
  #region <BDraw>
  enum BDraw_Draw { Point, Sphere, Line, Arrow, Signal, LineSegment, Texture_2D, Quad, WebCam, Mesh, Number, N }
  uint BDraw_ABuff_IndexN, BDraw_ABuff_BitN, BDraw_ABuff_N;
  uint[] BDraw_ABuff_Bits, BDraw_ABuff_Sums, BDraw_ABuff_Indexes;
  void BDraw_ABuff_Get_Bits() { Size(BDraw_ABuff_BitN); }
  void BDraw_ABuff_Get_Bits_Sums() { Size(BDraw_ABuff_BitN); Sync(); }
  [GS_UI, AttGS(GS_Buffer.GroupShared_Size, 1024)] uint[] BDraw_ABuff_grp, BDraw_ABuff_grp0;
  uint BDraw_ABuff_BitN1, BDraw_ABuff_BitN2;
  uint[] BDraw_ABuff_Fills1, BDraw_ABuff_Fills2;
  void BDraw_ABuff_GetSums() { Size(BDraw_ABuff_BitN); Sync(); }
  void BDraw_ABuff_GetFills1() { Size(BDraw_ABuff_BitN1); Sync(); }
  void BDraw_ABuff_GetFills2() { Size(BDraw_ABuff_BitN2); Sync(); }
  void BDraw_ABuff_IncFills1() { Size(BDraw_ABuff_BitN1); }
  void BDraw_ABuff_IncSums() { Size(BDraw_ABuff_BitN); }
  void BDraw_ABuff_GetIndexes() { Size(BDraw_ABuff_BitN); }
  struct BDraw_FontInfo { float2 uvBottomLeft, uvBottomRight, uvTopLeft, uvTopRight; int advance, bearing, minX, minY, maxX, maxY; };
  struct BDraw_TextInfo { float3 p, right, up, p0, p1; float2 size, uvSize; float4 color, backColor; uint justification, textI, quadType, axis; float height; };
  enum BDraw_TextAlignment { BottomLeft, CenterLeft, TopLeft, BottomCenter, CenterCenter, TopCenter, BottomRight, CenterRight, TopRight }
  enum BDraw_Text_QuadType { FrontOnly, FrontBack, Switch, Arrow, Billboard }
  const uint BDraw_Draw_Text3D = 12;
  const uint BDraw_LF = 10, BDraw_TB = 9, BDraw_ZERO = 0x30, BDraw_NINE = 0x39, BDraw_PERIOD = 0x2e, BDraw_COMMA = 0x2c, BDraw_PLUS = 0x2b, BDraw_MINUS = 0x2d, BDraw_SPACE = 0x20;
  bool BDraw_omitText, BDraw_includeUnicode;
  uint BDraw_fontInfoN, BDraw_textN, BDraw_textCharN, BDraw_boxEdgeN;
  float BDraw_fontSize;
  Texture2D BDraw_fontTexture;
  uint[] BDraw_tab_delimeted_text { set => Size(BDraw_textN); }
  BDraw_TextInfo[] BDraw_textInfos { set => Size(BDraw_textN); }
  BDraw_FontInfo[] BDraw_fontInfos { set => Size(BDraw_fontInfoN); }
  void BDraw_getTextInfo() { Size(BDraw_textN); }
  void BDraw_setDefaultTextInfo() { Size(BDraw_textN); }
  float BDraw_boxThickness;
  float4 BDraw_boxColor;
  //[GS_UI, AttGS(GS_Render.Quads)] void vert_BDraw_Text() { Size(BDraw_textN); }
  //[GS_UI, AttGS(GS_Render.Quads)] void vert_BDraw_Box() { Size(BDraw_boxEdgeN); }

  #endregion <BDraw>

  [GS_UI, AttGS(GS_Render.Quads, UI.ShowIf, "drawBox && drawAxes")] void vert_BDraw_Text() { Size(BDraw_textN); }
  [GS_UI, AttGS(GS_Render.Quads, UI.ShowIf, nameof(drawBox))] void vert_BDraw_Box() { Size(12); }

  enum PaletteType { Rainbow, GradientRainbow, GradientRainbow10, GradientRainbow20, Heat, GradientHeat, WhiteRainbow, invRainbow, Green, Gray, DarkGray, CT }

  [GS_UI, AttGS("Axes|Show Axes")] TreeGroup group_Axes_Lib;

  [GS_UI, AttGS("Geometry|Ranges")] TreeGroup group_Geometry;
  [GS_UI, AttGS("Show Grid|Show 3D graphics", UI.OnValueChanged, "buildText = true")] bool drawGrid;
  [GS_UI, AttGS("Grid X|X Range", UI.ValRange, "0, 0.1", -500, 500, siUnit.m, UI.Format, "0.000", UI.ShowIf, nameof(drawGrid), UI.OnValueChanged, "buildText = true;")] float2 GridX;
  [GS_UI, AttGS("Grid Y|Y Range", UI.ValRange, "0, 0.1", -500, 500, siUnit.m, UI.Format, "0.000", UI.ShowIf, nameof(drawGrid), UI.OnValueChanged, "buildText = true;")] float2 GridY;
  [GS_UI, AttGS("Grid Z|Z Range", UI.ValRange, "0, 0.1", -500, 500, siUnit.m, UI.Format, "0.000", UI.ShowIf, nameof(drawGrid), UI.OnValueChanged, "buildText = true;")] float2 GridZ;
  [GS_UI, AttGS("Geometry|Ranges")] TreeGroupEnd group_Geometry_End;

  [GS_UI, AttGS("Axes|Axes properties")] TreeGroup group_Axes;
  [GS_UI, AttGS("Show Box|Show outline box", UI.ShowIf, nameof(drawGrid), UI.OnValueChanged, "buildText = true;")] bool drawBox;
  [GS_UI, AttGS("Line Thickness", UI.ValRange, 10, 0, 300, siUnit.mm, UI.Format, "0.000", UI.ShowIf, "drawBox", UI.OnValueChanged, "buildText = true;", UI.Pow2_Slider)] float boxLineThickness;
  [GS_UI, AttGS("Show Axes|Show axis labels on grid", UI.ShowIf, nameof(drawGrid), UI.OnValueChanged, "buildText = true;")] bool drawAxes;
  [GS_UI, AttGS("Custom Ranges|Use custom axes ranges", UI.ValRange, 0, 0, 3, UI.ShowIf, nameof(drawGrid) + " && " + nameof(drawAxes), UI.OnValueChanged, "buildText = true;")] uint customAxesRangeN;
  [GS_UI, AttGS("Range Min|Minimum range to display on normalized axes", siUnit.m, UI.Format, "0.000", UI.ShowIf, "showNormalizedAxes && customAxesRangeN > 0", UI.OnValueChanged, "buildText = true;")] float3 axesRangeMin;
  [GS_UI, AttGS("Range Max|Maximum range to display on normalized axes", siUnit.m, UI.Format, "0.000", UI.ShowIf, "showNormalizedAxes && customAxesRangeN > 0", UI.OnValueChanged, "buildText = true;")] float3 axesRangeMax;
  [GS_UI, AttGS("Range Min 1|Minimum range to display on normalized axes", siUnit.m, UI.Format, "0.000", UI.ShowIf, "showNormalizedAxes && customAxesRangeN > 1", UI.OnValueChanged, "buildText = true;")] float3 axesRangeMin1;
  [GS_UI, AttGS("Range Max 1|Maximum range to display on normalized axes", siUnit.m, UI.Format, "0.000", UI.ShowIf, "showNormalizedAxes && customAxesRangeN > 1", UI.OnValueChanged, "buildText = true;")] float3 axesRangeMax1;
  [GS_UI, AttGS("Range Min 2|Minimum range to display on normalized axes", siUnit.m, UI.Format, "0.000", UI.ShowIf, "showNormalizedAxes && customAxesRangeN > 2", UI.OnValueChanged, "buildText = true;")] float3 axesRangeMin2;
  [GS_UI, AttGS("Range Max 2|Maximum range to display on normalized axes", siUnit.m, UI.Format, "0.000", UI.ShowIf, "showNormalizedAxes && customAxesRangeN > 2", UI.OnValueChanged, "buildText = true;")] float3 axesRangeMax2;
  [GS_UI, AttGS("Titles|Axis titles", UI.ShowIf, nameof(drawGrid) + " && " + nameof(drawAxes), UI.OnValueChanged, "buildText = true;")] string titles;
  [GS_UI, AttGS("Format|Number format for axes", UI.ShowIf, nameof(drawGrid) + " && " + nameof(drawAxes), UI.OnValueChanged, "buildText = true;")] string axesFormats;
  [GS_UI, AttGS("Text Size|Size of text for titles (x) and numbers (y)", UI.ValRange, 0.075f, 0.001, 10, siUnit.m, UI.Format, "0.000", UI.ShowIf, "drawGrid && drawAxes", UI.OnValueChanged, "buildText = true;", UI.Pow2_Slider)] float2 textSize;
  [GS_UI, AttGS("Text Color|RGBA", UI.ValRange, 0.5, 0, 1, UI.Format, "0.000", UI.ShowIf, nameof(drawGrid) + " && " + nameof(drawAxes), UI.OnValueChanged, "buildText = true;")] float3 axesColor;
  [GS_UI, AttGS("Opacity|Axes alpha", UI.ValRange, 1, 0, 1, UI.Format, "0.000", UI.ShowIf, nameof(drawGrid) + " && " + nameof(drawAxes), UI.OnValueChanged, "buildText = true;")] float axesOpacity;
  [GS_UI, AttGS("Zero Origin|Translate axes numbers to origin", UI.ShowIf, nameof(drawGrid) + " && " + nameof(drawAxes), UI.OnValueChanged, "buildText = true;")] bool zeroOrigin;
  [GS_UI, AttGS("Axes|Axes properties")] TreeGroupEnd group_Axes_End;

  [GS_UI, AttGS("Axes|Show Axes")] TreeGroupEnd group_Axes_Lib_End;

  bool buildText, showAxes, showOutline, showNormalizedAxes;
}