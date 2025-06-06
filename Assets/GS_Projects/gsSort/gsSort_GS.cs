using GpuScript;
using UnityEngine;
/// <summary>
/// To sort 4M, put in 2048x2048 rows and columns
///   Sort each row, then each column. All rows are are also sorted after this
///   Option 1. Merge sort every other row
///   Option 2. Count sort every row
///     if [row_i+1,col_1] > [row_i,col_n] then append rows, they are sorted. Add n to each item in row_i+1.
/// </summary>
public class gsSort_GS : _GS
{
  [GS_UI, AttGS("UI|User Interface")] TreeGroup group_UI;
  [GS_UI, AttGS("Sort|Sort specification")] TreeGroup group_Sort;
  [GS_UI, AttGS("Compress|Reduce threads from N^2 to N(N-1)/2")] bool useUpperTriangular;
  //[GS_UI, AttGS("Array Length|Length of each array to sort", UI.ValRange, 16, 4, 2048, UI.Pow2_Slider)] uint arrayLength;
  //[GS_UI, AttGS("Number of Arrays|number of arrays to sort.", UI.ValRange, 1, 1, 32700, UI.Pow2_Slider)] uint numberOfArrays;
  //[GS_UI, AttGS("Array Length|Length of each array to sort", UI.ValRange, 16, 4, 4194304, UI.Pow2_Slider)] uint arrayLength;
  //[GS_UI, AttGS("Number of Arrays|number of arrays to sort.", UI.ValRange, 1, 1, 2048, UI.Pow2_Slider)] uint numberOfArrays;
  [GS_UI, AttGS("Array Length|Length of each array to sort", UI.ValRange, 16, 4, 2048, UI.Pow2_Slider)] uint arrayLength;
  [GS_UI, AttGS("Number of Arrays|number of arrays to sort.", UI.ValRange, 1, 1, 2048, UI.Pow2_Slider)] uint numberOfArrays;
  [GS_UI, AttGS("Runtime N|number of iterations per sort to increase runtime accuracy", UI.ValRange, 1, 1, 100, UI.Pow2_Slider)] uint runtimeN;

  [GS_UI, AttGS("Sort|Run Sort")] void RunSort() { }
  [GS_UI, AttGS("Runtime|Time in micro-seconds for gsSort to run", UI.ReadOnly, UI.Format, "0.000", Unit.us)] float sort_runtime;
  [GS_UI, AttGS("Node Size|Radius of links and nodes", UI.ValRange, 40, 1, 40, UI.Format, "0.0", UI.Pow2_Slider)] float node_size;
  enum BenchmarkType { Nanosec, TFlops }
  [GS_UI, AttGS("Table|What to display in benchmark table")] BenchmarkType benchmarkType;
  [GS_UI, AttGS("Benchmark|Build benchmark table", UI.Sync)] void RunBenchmark() { }
  [GS_UI, AttGS("Sort|Sort specification")] TreeGroupEnd groupEnd_Sort;
  uint vsN;
  float[] vs { set => Size(vsN); }
  uint[] counts { set => Size(vsN); }
  uint[] sorts { set => Size(vsN); }
  void init_vs() { Size(vsN); }
  void init_counts() { Size(vsN); }
  void add_counts_triangle() { Size(numberOfArrays, arrayLength * (arrayLength - 1) / 2); }
  void set_sorts() { Size(numberOfArrays, arrayLength); }
  void add_counts() { Size(numberOfArrays, arrayLength, arrayLength); }
  [GS_UI, AttGS(GS_Render.Quads, UI.ShowIf, "node_size > 1")] void vert_vs0() { Size(vsN); }
  [GS_UI, AttGS(GS_Render.Quads, UI.ShowIf, "node_size > 1")] void vert_vs1() { Size(vsN); }
  [GS_UI, AttGS(GS_Render.Quads, UI.ShowIf, "node_size > 1")] void vert_vs_arrows() { Size(vsN); }
  [GS_UI, AttGS(GS_Lib.External, GS_Lib.Email, "you@gmail.com", GS_Lib.Expires, "2024/12/10", GS_Lib.Key, 123456)] gsOCam_Lib OCam_Lib;
  #region <OCam_Lib>

  #endregion <OCam_Lib>
  [GS_UI, AttGS(GS_Lib.External, GS_Lib.Email, "you@gmail.com", GS_Lib.Expires, "2024/12/10", GS_Lib.Key, 123456)] gsViews_Lib Views_Lib;
  #region <Views_Lib>

  #endregion <Views_Lib>
  [GS_UI, AttGS(GS_Lib.External, GS_Lib.Email, "you@gmail.com", GS_Lib.Expires, "2024/12/10", GS_Lib.Key, 123456)] gsReport_Lib Report_Lib;
  #region <Report_Lib>

  #endregion <Report_Lib>
  [GS_UI, AttGS("UI|User Interface")] TreeGroupEnd group_UI_End;
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
  gsRand Rand;
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
}