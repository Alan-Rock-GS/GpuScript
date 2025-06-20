using GpuScript;
using UnityEngine;
public class gsBrownian_GS : _GS
{
  [GS_UI, AttGS("UI|User Interface")] TreeGroup group_UI;
  [GS_UI, AttGS("Brownian|Brownian Motion")] TreeGroup group_Brownian;
  [GS_UI, AttGS("Stock N|Number of tickers", UI.ValRange, 500, 1, 1_000, UI.Format, "#,###", UI.Pow2_Slider, UI.NearestDigit)] uint tickerN;
  [GS_UI, AttGS("Trade N|Number of trading days", UI.ValRange, 252, 252, 5040, UI.Format, "#,###", UI.Nearest, 252)] uint dayN;
  [GS_UI, AttGS("Gain SD|Standard deviation of price gain", UI.ValRange, 0.02f, 0.0001f, 0.05f, UI.Format, "0.00%")] float gainSD;
  [GS_UI, AttGS("Global Trend|Trend average", UI.ValRange, 0.02f, 0, 0.05f, UI.Format, "0.00%")] float globalTrend;
  [GS_UI, AttGS("Trend SD|Standard deviation of trend", UI.ValRange, 0.02f, 0, 0.05f, UI.Format, "0.00%")] float trendSD;
  [GS_UI, AttGS("Max Gain|Maximum display gain", UI.ValRange, 7, 1, 10, UI.Format, "0.00")] float maxDisplayGain;
  [GS_UI, AttGS("Thickness|Line thickness", UI.Format, "0.000", UI.ValRange, 0.004f, 0.001f, 0.01f)] float lineThickness;
  [GS_UI, AttGS("Ticker I|Index of ticker to display", UI.ValRange, 0, 0, 500)] uint displayTickerI;
  [GS_UI, AttGS("Ticker N|Number of tickers to display", UI.ValRange, 1, 1, 500)] uint displayTickerN;
  [GS_UI, AttGS("Calc|Calculate 1D Motion", UI.OnClicked, "InitBuffers(); Rand_Init(2 * tickerN * dayN, 7);")] void Calc_1D() { }
  [GS_UI, AttGS("Brownian|Brownian Motion")] TreeGroupEnd groupEnd_Brownian;

  [GS_UI, AttGS("GDM|AI analysis")] TreeGroup group_Quantrade_GDM;
  [GS_UI, AttGS("Dim I|Number of drift dimensions to generate", UI.ValRange, 1, 1, 3)] int dimI;
  [GS_UI, AttGS("Drift|Generate drift examples")] void driftExamples() { }
  [GS_UI, AttGS("GDM|AI analysis")] TreeGroupEnd groupEnd_Quantrade_GDM;

  float price0; float2 gainRange;
  int[] vs { set => Size(tickerN * dayN); }
  int[] vs0 { set => Size(tickerN * dayN); }
  float[] trends { set => Size(tickerN); }
  void InitTrends() { Size(tickerN); }
  void InitPrice() { Size(tickerN, dayN); }
  void SumPrice() { Size(tickerN, dayN * (dayN - 1) / 2); }
  void CalcGain() { Size(tickerN, dayN); }
  [GS_UI, AttGS(GS_Render.Quads)] void vert_Draw_Random_Signal() { Size(tickerN); }

  [GS_UI] gsRand Rand;
  #region <Rand>
  uint Rand_N, Rand_I, Rand_J;
  uint4 Rand_seed4;
  uint4[] Rand_rs { set => Size(Rand_N); }
  void Rand_initSeed() { Size(Rand_N); }
  void Rand_initState() { Size(Rand_I); }
  [GS_UI, AttGS(GS_Buffer.GroupShared)] uint4[] Rand_grp { set => Size(1024); }
  void Rand_grp_init_1M() { Size(Rand_N / 1024 / 1024); Sync(); }
  void Rand_grp_init_1K() { Size(Rand_N / 1024); Sync(); }
  void Rand_grp_fill_1K() { Size(Rand_N); Sync(); }
  #endregion <Rand>

  [GS_UI, AttGS(GS_Lib.Internal, GS_Lib.Email, "you@gmail.com", GS_Lib.Expires, "2024/12/10", GS_Lib.Key, 123456)] gsAxes_Lib Axes_Lib;
  #region <Axes_Lib>
  enum Axes_Lib_BDraw_Draw { Point, Sphere, Line, Arrow, Signal, LineSegment, Texture_2D, Quad, WebCam, Mesh, Number, N }
  uint Axes_Lib_BDraw_ABuff_IndexN, Axes_Lib_BDraw_ABuff_BitN, Axes_Lib_BDraw_ABuff_N;
  uint[] Axes_Lib_BDraw_ABuff_Bits, Axes_Lib_BDraw_ABuff_Sums, Axes_Lib_BDraw_ABuff_Indexes;
  void Axes_Lib_BDraw_ABuff_Get_Bits() { Size(Axes_Lib_BDraw_ABuff_BitN); }
  void Axes_Lib_BDraw_ABuff_Get_Bits_Sums() { Size(Axes_Lib_BDraw_ABuff_BitN); Sync(); }
  [GS_UI, AttGS(GS_Buffer.GroupShared_Size, 1024)] uint[] Axes_Lib_BDraw_ABuff_grp, Axes_Lib_BDraw_ABuff_grp0;
  uint Axes_Lib_BDraw_ABuff_BitN1, Axes_Lib_BDraw_ABuff_BitN2;
  uint[] Axes_Lib_BDraw_ABuff_Fills1, Axes_Lib_BDraw_ABuff_Fills2;
  void Axes_Lib_BDraw_ABuff_GetSums() { Size(Axes_Lib_BDraw_ABuff_BitN); Sync(); }
  void Axes_Lib_BDraw_ABuff_GetFills1() { Size(Axes_Lib_BDraw_ABuff_BitN1); Sync(); }
  void Axes_Lib_BDraw_ABuff_GetFills2() { Size(Axes_Lib_BDraw_ABuff_BitN2); Sync(); }
  void Axes_Lib_BDraw_ABuff_IncFills1() { Size(Axes_Lib_BDraw_ABuff_BitN1); }
  void Axes_Lib_BDraw_ABuff_IncSums() { Size(Axes_Lib_BDraw_ABuff_BitN); }
  void Axes_Lib_BDraw_ABuff_GetIndexes() { Size(Axes_Lib_BDraw_ABuff_BitN); }
  struct Axes_Lib_BDraw_FontInfo { float2 uvBottomLeft, uvBottomRight, uvTopLeft, uvTopRight; int advance, bearing, minX, minY, maxX, maxY; };
  struct Axes_Lib_BDraw_TextInfo { float3 p, right, up, p0, p1; float2 size, uvSize; float4 color, backColor; uint justification, textI, quadType, axis; float height; };
  enum Axes_Lib_BDraw_TextAlignment { BottomLeft, CenterLeft, TopLeft, BottomCenter, CenterCenter, TopCenter, BottomRight, CenterRight, TopRight }
  enum Axes_Lib_BDraw_Text_QuadType { FrontOnly, FrontBack, Switch, Arrow, Billboard }
  const uint Axes_Lib_BDraw_Draw_Text3D = 12;
  const uint Axes_Lib_BDraw_LF = 10, Axes_Lib_BDraw_TB = 9, Axes_Lib_BDraw_ZERO = 0x30, Axes_Lib_BDraw_NINE = 0x39, Axes_Lib_BDraw_PERIOD = 0x2e, Axes_Lib_BDraw_COMMA = 0x2c, Axes_Lib_BDraw_PLUS = 0x2b, Axes_Lib_BDraw_MINUS = 0x2d, Axes_Lib_BDraw_SPACE = 0x20;
  bool Axes_Lib_BDraw_omitText, Axes_Lib_BDraw_includeUnicode;
  uint Axes_Lib_BDraw_fontInfoN, Axes_Lib_BDraw_textN, Axes_Lib_BDraw_textCharN, Axes_Lib_BDraw_boxEdgeN;
  float Axes_Lib_BDraw_fontSize;
  Texture2D Axes_Lib_BDraw_fontTexture;
  uint[] Axes_Lib_BDraw_tab_delimeted_text { set => Size(Axes_Lib_BDraw_textN); }
  Axes_Lib_BDraw_TextInfo[] Axes_Lib_BDraw_textInfos { set => Size(Axes_Lib_BDraw_textN); }
  Axes_Lib_BDraw_FontInfo[] Axes_Lib_BDraw_fontInfos { set => Size(Axes_Lib_BDraw_fontInfoN); }
  void Axes_Lib_BDraw_getTextInfo() { Size(Axes_Lib_BDraw_textN); }
  void Axes_Lib_BDraw_setDefaultTextInfo() { Size(Axes_Lib_BDraw_textN); }
  float Axes_Lib_BDraw_boxThickness;
  float4 Axes_Lib_BDraw_boxColor;
  [GS_UI, AttGS(GS_Render.Quads, UI.ShowIf, "Axes_Lib_drawBox && Axes_Lib_drawAxes")] void vert_Axes_Lib_BDraw_Text() { Size(Axes_Lib_BDraw_textN); }
  [GS_UI, AttGS(GS_Render.Quads, UI.ShowIf, nameof(Axes_Lib_drawBox))] void vert_Axes_Lib_BDraw_Box() { Size(12); }
  enum Axes_Lib_PaletteType { Rainbow, GradientRainbow, GradientRainbow10, GradientRainbow20, Heat, GradientHeat, WhiteRainbow, invRainbow, Green, Gray, DarkGray, CT }
  [GS_UI, AttGS("Axes|Show Axes")] TreeGroup Axes_Lib_group_Axes_Lib;
  [GS_UI, AttGS("Geometry|Ranges")] TreeGroup Axes_Lib_group_Geometry;
  [GS_UI, AttGS("Show Grid|Show 3D graphics", UI.OnValueChanged, "Axes_Lib_buildText = true")] bool Axes_Lib_drawGrid;
  [GS_UI, AttGS("Grid X|X Range", UI.ValRange, "0, 0.1", -500, 500, siUnit.m, UI.Format, "0.000", UI.ShowIf, nameof(Axes_Lib_drawGrid), UI.OnValueChanged, "Axes_Lib_buildText = true;")] float2 Axes_Lib_GridX;
  [GS_UI, AttGS("Grid Y|Y Range", UI.ValRange, "0, 0.1", -500, 500, siUnit.m, UI.Format, "0.000", UI.ShowIf, nameof(Axes_Lib_drawGrid), UI.OnValueChanged, "Axes_Lib_buildText = true;")] float2 Axes_Lib_GridY;
  [GS_UI, AttGS("Grid Z|Z Range", UI.ValRange, "0, 0.1", -500, 500, siUnit.m, UI.Format, "0.000", UI.ShowIf, nameof(Axes_Lib_drawGrid), UI.OnValueChanged, "Axes_Lib_buildText = true;")] float2 Axes_Lib_GridZ;
  [GS_UI, AttGS("Geometry|Ranges")] TreeGroupEnd Axes_Lib_group_Geometry_End;
  [GS_UI, AttGS("Axes|Axes properties")] TreeGroup Axes_Lib_group_Axes;
  [GS_UI, AttGS("Show Box|Show outline box", UI.ShowIf, nameof(Axes_Lib_drawGrid), UI.OnValueChanged, "Axes_Lib_buildText = true;")] bool Axes_Lib_drawBox;
  [GS_UI, AttGS("Line Thickness", UI.ValRange, 10, 0, 300, siUnit.mm, UI.Format, "0.000", UI.ShowIf, "Axes_Lib_drawBox", UI.OnValueChanged, "Axes_Lib_buildText = true;", UI.Pow2_Slider)] float Axes_Lib_boxLineThickness;
  [GS_UI, AttGS("Show Axes|Show axis labels on grid", UI.ShowIf, nameof(Axes_Lib_drawGrid), UI.OnValueChanged, "Axes_Lib_buildText = true;")] bool Axes_Lib_drawAxes;
  [GS_UI, AttGS("Custom Ranges|Use custom axes ranges", UI.ValRange, 0, 0, 3, UI.ShowIf, nameof(Axes_Lib_drawGrid) + " && " + nameof(Axes_Lib_drawAxes), UI.OnValueChanged, "Axes_Lib_buildText = true;")] uint Axes_Lib_customAxesRangeN;
  [GS_UI, AttGS("Range Min|Minimum range to display on normalized axes", siUnit.m, UI.Format, "0.000", UI.ShowIf, "Axes_Lib_showNormalizedAxes && Axes_Lib_customAxesRangeN > 0", UI.OnValueChanged, "Axes_Lib_buildText = true;")] float3 Axes_Lib_axesRangeMin;
  [GS_UI, AttGS("Range Max|Maximum range to display on normalized axes", siUnit.m, UI.Format, "0.000", UI.ShowIf, "Axes_Lib_showNormalizedAxes && Axes_Lib_customAxesRangeN > 0", UI.OnValueChanged, "Axes_Lib_buildText = true;")] float3 Axes_Lib_axesRangeMax;
  [GS_UI, AttGS("Range Min 1|Minimum range to display on normalized axes", siUnit.m, UI.Format, "0.000", UI.ShowIf, "Axes_Lib_showNormalizedAxes && Axes_Lib_customAxesRangeN > 1", UI.OnValueChanged, "Axes_Lib_buildText = true;")] float3 Axes_Lib_axesRangeMin1;
  [GS_UI, AttGS("Range Max 1|Maximum range to display on normalized axes", siUnit.m, UI.Format, "0.000", UI.ShowIf, "Axes_Lib_showNormalizedAxes && Axes_Lib_customAxesRangeN > 1", UI.OnValueChanged, "Axes_Lib_buildText = true;")] float3 Axes_Lib_axesRangeMax1;
  [GS_UI, AttGS("Range Min 2|Minimum range to display on normalized axes", siUnit.m, UI.Format, "0.000", UI.ShowIf, "Axes_Lib_showNormalizedAxes && Axes_Lib_customAxesRangeN > 2", UI.OnValueChanged, "Axes_Lib_buildText = true;")] float3 Axes_Lib_axesRangeMin2;
  [GS_UI, AttGS("Range Max 2|Maximum range to display on normalized axes", siUnit.m, UI.Format, "0.000", UI.ShowIf, "Axes_Lib_showNormalizedAxes && Axes_Lib_customAxesRangeN > 2", UI.OnValueChanged, "Axes_Lib_buildText = true;")] float3 Axes_Lib_axesRangeMax2;
  [GS_UI, AttGS("Titles|Axis Axes_Lib_titles", UI.ShowIf, nameof(Axes_Lib_drawGrid) + " && " + nameof(Axes_Lib_drawAxes), UI.OnValueChanged, "Axes_Lib_buildText = true;")] string Axes_Lib_titles;
  [GS_UI, AttGS("Format|Number format for axes", UI.ShowIf, nameof(Axes_Lib_drawGrid) + " && " + nameof(Axes_Lib_drawAxes), UI.OnValueChanged, "Axes_Lib_buildText = true;")] string Axes_Lib_axesFormats;
  [GS_UI, AttGS("Text Size|Size of text for Axes_Lib_titles (x) and numbers (y)", UI.ValRange, 0.075f, 0.001, 10, siUnit.m, UI.Format, "0.000", UI.ShowIf, "Axes_Lib_drawGrid && Axes_Lib_drawAxes", UI.OnValueChanged, "Axes_Lib_buildText = true;", UI.Pow2_Slider)] float2 Axes_Lib_textSize;
  [GS_UI, AttGS("Text Color|RGBA", UI.ValRange, 0.5, 0, 1, UI.Format, "0.000", UI.ShowIf, nameof(Axes_Lib_drawGrid) + " && " + nameof(Axes_Lib_drawAxes), UI.OnValueChanged, "Axes_Lib_buildText = true;")] float3 Axes_Lib_axesColor;
  [GS_UI, AttGS("Opacity|Axes alpha", UI.ValRange, 1, 0, 1, UI.Format, "0.000", UI.ShowIf, nameof(Axes_Lib_drawGrid) + " && " + nameof(Axes_Lib_drawAxes), UI.OnValueChanged, "Axes_Lib_buildText = true;")] float Axes_Lib_axesOpacity;
  [GS_UI, AttGS("Zero Origin|Translate axes numbers to origin", UI.ShowIf, nameof(Axes_Lib_drawGrid) + " && " + nameof(Axes_Lib_drawAxes), UI.OnValueChanged, "Axes_Lib_buildText = true;")] bool Axes_Lib_zeroOrigin;
  [GS_UI, AttGS("Axes|Axes properties")] TreeGroupEnd Axes_Lib_group_Axes_End;
  [GS_UI, AttGS("Axes|Show Axes")] TreeGroupEnd Axes_Lib_group_Axes_Lib_End;
  bool Axes_Lib_buildText, Axes_Lib_showAxes, Axes_Lib_showOutline, Axes_Lib_showNormalizedAxes;

  #endregion <Axes_Lib>

  [GS_UI, AttGS(GS_Lib.External, GS_Lib.Email, "you@gmail.com", GS_Lib.Expires, "2024/12/10", GS_Lib.Key, 123456)] gsOCam_Lib OCam_Lib;
  #region <OCam_Lib>

  #endregion <OCam_Lib>

  [GS_UI, AttGS(GS_Lib.External, GS_Lib.Email, "you@gmail.com", GS_Lib.Expires, "2024/12/10", GS_Lib.Key, 123456)] gsViews_Lib Views_Lib;
  #region <Views_Lib>

  #endregion <Views_Lib>

  //[GS_UI, AttGS(GS_Lib.External, GS_Lib.Email, "alanrock.gs@gmail.com", GS_Lib.Expires, "2035/5/26", GS_Lib.Key, 147695)] g sReport_Lib Report_Lib;

  [GS_UI, AttGS("UI|Rand test")] TreeGroupEnd group_UI_End;
}