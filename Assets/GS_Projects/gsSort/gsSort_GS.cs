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
  [GS_UI, AttGS(GS_Lib.External, GS_Lib.Email, "you@gmail.com", GS_Lib.Expires, "2024/12/10", GS_Lib.Key, 123456)] gsACam_Lib ACam_Lib;
  #region <ACam_Lib>

  #endregion <ACam_Lib>
  [GS_UI, AttGS(GS_Lib.External, GS_Lib.Email, "you@gmail.com", GS_Lib.Expires, "2024/12/10", GS_Lib.Key, 123456)] gsAViews_Lib AViews_Lib;
  #region <AViews_Lib>

  #endregion <AViews_Lib>
  [GS_UI, AttGS(GS_Lib.External, GS_Lib.Email, "you@gmail.com", GS_Lib.Expires, "2024/12/10", GS_Lib.Key, 123456)] gsAReport_Lib AReport_Lib;
  #region <AReport_Lib>

  #endregion <AReport_Lib>
  [GS_UI, AttGS("UI|User Interface")] TreeGroupEnd group_UI_End;
  gsADraw ADraw;
  #region <ADraw>
  enum ADraw_Draw { Point, Sphere, Line, Arrow, Signal, LineSegment, Texture_2D, Quad, WebCam, Mesh, Number, N }
  uint ADraw_ABuff_IndexN, ADraw_ABuff_BitN, ADraw_ABuff_N;
  uint[] ADraw_ABuff_Bits, ADraw_ABuff_Sums, ADraw_ABuff_Indexes;
  void ADraw_ABuff_Get_Bits() { Size(ADraw_ABuff_BitN); }
  void ADraw_ABuff_Get_Bits_Sums() { Size(ADraw_ABuff_BitN); Sync(); }
  [GS_UI, AttGS(GS_Buffer.GroupShared_Size, 1024)] uint[] ADraw_ABuff_grp, ADraw_ABuff_grp0;
  uint ADraw_ABuff_BitN1, ADraw_ABuff_BitN2;
  uint[] ADraw_ABuff_Fills1, ADraw_ABuff_Fills2;
  void ADraw_ABuff_GetSums() { Size(ADraw_ABuff_BitN); Sync(); }
  void ADraw_ABuff_GetFills1() { Size(ADraw_ABuff_BitN1); Sync(); }
  void ADraw_ABuff_GetFills2() { Size(ADraw_ABuff_BitN2); Sync(); }
  void ADraw_ABuff_IncFills1() { Size(ADraw_ABuff_BitN1); }
  void ADraw_ABuff_IncSums() { Size(ADraw_ABuff_BitN); }
  void ADraw_ABuff_GetIndexes() { Size(ADraw_ABuff_BitN); }
  struct ADraw_FontInfo { float2 uvBottomLeft, uvBottomRight, uvTopLeft, uvTopRight; int advance, bearing, minX, minY, maxX, maxY; };
  struct ADraw_TextInfo { float3 p, right, up, p0, p1; float2 size, uvSize; float4 color, backColor; uint justification, textI, quadType, axis; float height; };
  enum ADraw_TextAlignment { BottomLeft, CenterLeft, TopLeft, BottomCenter, CenterCenter, TopCenter, BottomRight, CenterRight, TopRight }
  enum ADraw_Text_QuadType { FrontOnly, FrontBack, Switch, Arrow, Billboard }
  const uint ADraw_Draw_Text3D = 12;
  const uint ADraw_LF = 10, ADraw_TB = 9, ADraw_ZERO = 0x30, ADraw_NINE = 0x39, ADraw_PERIOD = 0x2e, ADraw_COMMA = 0x2c, ADraw_PLUS = 0x2b, ADraw_MINUS = 0x2d, ADraw_SPACE = 0x20;
  bool ADraw_omitText, ADraw_includeUnicode;
  uint ADraw_fontInfoN, ADraw_textN, ADraw_textCharN, ADraw_boxEdgeN;
  float ADraw_fontSize;
  Texture2D ADraw_fontTexture;
  uint[] ADraw_tab_delimeted_text { set => Size(ADraw_textN); }
  ADraw_TextInfo[] ADraw_textInfos { set => Size(ADraw_textN); }
  ADraw_FontInfo[] ADraw_fontInfos { set => Size(ADraw_fontInfoN); }
  void ADraw_getTextInfo() { Size(ADraw_textN); }
  void ADraw_setDefaultTextInfo() { Size(ADraw_textN); }
  float ADraw_boxThickness;
  float4 ADraw_boxColor;
  [GS_UI, AttGS(GS_Render.Quads)] void vert_ADraw_Text() { Size(ADraw_textN); }
  [GS_UI, AttGS(GS_Render.Quads)] void vert_ADraw_Box() { Size(ADraw_boxEdgeN); }

  #endregion <ADraw>
  gsARand ARand;
  #region <ARand>
  uint ARand_N, ARand_I, ARand_J;
  uint4 ARand_seed4;
  uint4[] ARand_rs { set => Size(ARand_N); }
  void ARand_initSeed() { Size(ARand_N); }
  void ARand_initState() { Size(ARand_I); }
  [GS_UI, AttGS(GS_Buffer.GroupShared)] uint4[] ARand_grp { set => Size(1024); }
  void ARand_grp_init_1M() { Size(ARand_N / 1024 / 1024); Sync(); }
  void ARand_grp_init_1K() { Size(ARand_N / 1024); Sync(); }
  void ARand_grp_fill_1K() { Size(ARand_N); Sync(); }
  #endregion <ARand>
}