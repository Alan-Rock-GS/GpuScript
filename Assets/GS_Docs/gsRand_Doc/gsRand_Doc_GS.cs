using GpuScript;
using UnityEngine;

public class gsRand_Doc_GS : _GS
{
  [GS_UI, AttGS("UI|Rand test")] TreeGroup group_UI;

  [GS_UI, AttGS("Rand|Rand test")] TreeGroup group_Rand;
  [GS_UI, AttGS("N|Number of random numbers", UI.ValRange, 1000, 1, 10000000, UI.Format, "#,###", UI.Pow2_Slider, UI.IsPow10)] uint pntN;
  [GS_UI, AttGS("Thickness|Line thickness", UI.Format, "0.000", UI.ValRange, 0.004f, 0.001f, 0.01f)] float lineThickness;

  [GS_UI, AttGS("Avg|Calculate random number average")] TreeGroup group_Avg;
  [GS_UI, AttGS("Calc Avg|Calculate the average")] void Avg() { }
  [GS_UI, AttGS("Avg|Calculated average", UI.ReadOnly)] float Avg_Val;
  [GS_UI, AttGS("Runtime|Time to calculate average", UI.ReadOnly, Unit.ms)] float Avg_Val_Runtime;
  [GS_UI, AttGS("TFlops|Tera-Flops per second", UI.ReadOnly, UI.Format, "#,##0.000")] float Avg_Val_TFlops;
  [GS_UI, AttGS(GS_Render.Quads, UI.ShowIf, "drawGroup == DrawGroup.Average || drawGroup == DrawGroup.PI_Area || drawGroup == DrawGroup.PI_Integral")] void vert_Draw_Random_Signal() { Size(1); }
  [GS_UI, AttGS(GS_Render.Quads, UI.ShowIf, "drawGroup == DrawGroup.Average")] void vert_Draw_Calc_Avg() { Size(1); }
  [GS_UI, AttGS(GS_Render.Quads, UI.ShowIf, "drawGroup == DrawGroup.Average")] void vert_Draw_Avg() { Size(1); }
  [GS_UI, AttGS(GS_Render.Quads, UI.ShowIf, "drawGroup == DrawGroup.Average || drawGroup == DrawGroup.PI_Area || drawGroup == DrawGroup.PI_Integral")] void vert_Draw_Pnts() { Size(pntN); }
  [GS_UI, AttGS("Avg|Calculate random number average")] TreeGroupEnd groupEnd_Avg;

  [GS_UI, AttGS("Area π|Calculate PI from area using random numbers")] TreeGroup group_Area_PI;
  [GS_UI, AttGS("Calc Area π|Calculate PI using area of square and circle")] void Area_PI() { }
  [GS_UI, AttGS("Area π|Calculated value of PI using area", UI.ReadOnly)] float Area_PI_Val;
  [GS_UI, AttGS("Area π Error|PI error using area", UI.ReadOnly)] float Area_PI_Error;
  [GS_UI, AttGS("Runtime|Time to calculate PI using area", UI.ReadOnly, Unit.ms)] float Area_PI_Runtime;
  [GS_UI, AttGS("TFlops|Tera-Flops per second", UI.ReadOnly, UI.Format, "#,##0.000")] float Area_PI_TFlops;
  [GS_UI, AttGS("Area π|Calculate PI using random numbers")] TreeGroupEnd groupEnd_Area_PI;

  [GS_UI, AttGS("Integral π|Calculate PI from integral using random numbers")] TreeGroup group_Integral_PI;
  [GS_UI, AttGS("Calc Integral π|Calculate PI using integral")] void Integral_PI() { }
  [GS_UI, AttGS("Integral π|Calculated value of PI using integral", UI.ReadOnly)] float Integral_PI_Val;
  [GS_UI, AttGS("Integral π Error|PI error using integral", UI.ReadOnly)] float Integral_PI_Error;
  [GS_UI, AttGS("Runtime|Time to calculate PI using integral", UI.ReadOnly, Unit.ms)] float Integral_PI_Runtime;
  [GS_UI, AttGS("TFlops|Tera-Flops per second", UI.ReadOnly, UI.Format, "#,##0.000")] float Integral_PI_TFlops;
  [GS_UI, AttGS("Integral π|Calculate PI using random numbers")] TreeGroupEnd groupEnd_Integral_PI;

  [GS_UI, AttGS("TSP|Simulated annealing for traveling starship problem")] TreeGroup group_TSP;
  [GS_UI, AttGS("Star N|Number of stars", UI.Format, "#,##0", UI.ValRange, 10, 10, 4500, UI.Nearest, 10, UI.Pow2_Slider, UI.OnValueChanged, "StartCoroutine(RunTSP())")] uint starN;
  [GS_UI, AttGS("Border Reward|Positive reward or negative penalty for crossing the center border", UI.Format, "0.000", UI.ValRange, 0, -1, 1, UI.OnValueChanged, "StartCoroutine(RunTSP())")] float starBorderReward;
  [GS_UI, AttGS("Run TSP|Run the Traveling Salesman Problem", UI.Sync)] void RunTSP() { }
  uint starPathN;
  float3[] stars { set => Size(starN); }
  void init_stars() { Size(starN); }
  int3[] segments { set => Size(starPathN); }
  void calc_segments() { Size(starPathN); }
  void calc_reverse_or_transport() { Size(starPathN); }
  uint[] starPaths { set => Size(starN * starPathN); }
  void init_paths() { Size(starN, starPathN); }
  void calc_paths() { Size(starN, starPathN - 1); }
  uint[] pathLengths { set => Size(starPathN); }
  void init_path_lengths() { Size(starPathN); }
  void calc_path_lengths() { Size(starN, starPathN); }
  void init_min_path_length() { Size(2); }
  void find_min_path_length() { Size(starPathN); }
  void find_min_path_lengthI() { Size(starPathN); }
  void save_best_path() { Size(starPathN); }
  enum DrawGroup { None, Average, PI_Area, PI_Integral, TSP}
  DrawGroup drawGroup;
  [GS_UI, AttGS(GS_Render.Quads, UI.ShowIf, "drawGroup == DrawGroup.TSP")] void vert_Draw_Stars() { Size(starN); }
  [GS_UI, AttGS(GS_Render.Quads, UI.ShowIf, "drawGroup == DrawGroup.TSP")] void vert_Draw_Star_Path() { Size(starN); }
  [GS_UI, AttGS(GS_Render.Quads)] void vert_Draw_Stars_Border() { Size(13); }
  [GS_UI, AttGS("TSP|Simulated annealing for traveling starship problem")] TreeGroupEnd groupEnd_TSP;
  [GS_UI, AttGS("Rand|Rand test")] TreeGroupEnd groupEnd_Rand;

  enum UInts { Count, MinPathLength, MinPathI, N}
  uint[] uints { set => Size(UInts.N); }
  int[] ints { set => Size(1); }
  void Calc_Average() { Size(pntN); }
  void Count_Pnts_in_Circle() { Size(pntN); }
  void Count_Pnts_out_of_Circle() { Size(pntN); }
  void Calc_Integral_PI() { Size(pntN); }

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
  [GS_UI, AttGS(GS_Render.Quads)] void vert_BDraw_Text() { Size(BDraw_textN); }
  [GS_UI, AttGS(GS_Render.Quads)] void vert_BDraw_Box() { Size(BDraw_boxEdgeN); }

  #endregion <BDraw>

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
  [GS_UI, AttGS(GS_Lib.External, GS_Lib.Email, "you@gmail.com", GS_Lib.Expires, "2024/12/10", GS_Lib.Key, 123456)] gsViews_Lib Views_Lib;
  #region <Views_Lib>

  #endregion <Views_Lib>
  
  [GS_UI, AttGS(GS_Lib.External, GS_Lib.Email, "you@gmail.com", GS_Lib.Expires, "2024/12/10", GS_Lib.Key, 123456)] gsOCam_Lib OCam_Lib;
  #region <OCam_Lib>

  #endregion <OCam_Lib>

  [GS_UI, AttGS(GS_Lib.External, GS_Lib.Email, "you@gmail.com", GS_Lib.Expires, "2024/12/10", GS_Lib.Key, 123456)] gsReport_Lib Report_Lib;
  #region <Report_Lib>

  #endregion <Report_Lib>

  [GS_UI, AttGS("UI|Rand test")] TreeGroupEnd group_UI_End;
}