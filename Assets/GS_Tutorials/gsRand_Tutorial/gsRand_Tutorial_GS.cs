using GpuScript;
using UnityEngine;

public class gsRand_Tutorial_GS : _GS
{
  [GS_UI, AttGS("UI|Rand Tutorial")] TreeGroup group_UI;
  [GS_UI, AttGS("Signal|Plot Rand signal")] TreeGroup group_Signal;
  uint I, J;
  uint4 seed4;
  void initSeed() { Size(randomNumberN); }
  void initState() { Size(I); }
  [GS_UI, AttGS("Random Number N|Number of random numbers", UI.Format, "#,##0", UI.ValRange, 128, 8, 33554432, UI.IsPow2, UI.Pow2_Slider, UI.OnValueChanged, "Init_randomNumbers(); Avg();")] uint randomNumberN;
  int[] ints { set => Size(1); }
  void Calc_Random_Numbers() { Size(randomNumberN); }
  void Calc_Average() { Size(randomNumberN); }
  uint4[] randomNumbers { set => Size(randomNumberN); }
  [GS_UI, AttGS("Init|Initialize Random Number Buffer")] void Init_randomNumbers() { }
  [GS_UI, AttGS("Thickness|Line thickness", UI.Format, "0.000", UI.ValRange, 0.004f, 0.001f, 0.01f)] float lineThickness;
  [GS_UI, AttGS("Calc Average|Calculate the average of an array of random numbers")] void Avg() { }
  [GS_UI, AttGS("Average|Calculated average, should be close to zero", UI.Format, "0.000000", UI.ReadOnly)] float Average;
  [GS_UI, AttGS("Runtime|Time to generate a single random number", Unit.ns, UI.Format, "#,##0.000000", UI.ReadOnly)] float Runtime;
  [GS_UI, AttGS(GS_Render.Quads, UI.ShowIf, "isDrawSignal")] void vert_Draw_Random_Signal() { Size(1); }
  [GS_UI, AttGS(GS_Render.Quads, UI.ShowIf, "isDrawSignal")] void vert_Draw_Calc_Avg() { Size(1); }
  [GS_UI, AttGS(GS_Render.Quads, UI.ShowIf, "isDrawSignal")] void vert_Draw_Avg() { Size(1); }
  [GS_UI, AttGS("Signal|Plot Rand signal")] TreeGroupEnd groupEnd_Signal;

  [GS_UI, AttGS("Anneal|Simulated annealing for traveling salesman problem")] TreeGroup group_Anneal;
  [GS_UI, AttGS("City N|Number of cities", UI.Format, "#,##0", UI.ValRange, 10, 10, 1000, UI.Nearest, 10, UI.Pow2_Slider, UI.OnValueChanged, "StartCoroutine(RunAnneal_Sync())")] uint cityN;
  [GS_UI, AttGS("Border Reward|Positive reward or negative penalty for crossing the center border", UI.Format, "0.000", UI.ValRange, 0, -1, 1)] float cityBorderReward;
  [GS_UI, AttGS("Run|Run Annealing", UI.Sync)] void RunAnneal() { }
  uint cityPathN;
  float3[] cities { set => Size(cityN); }
  void init_cities() { Size(cityN); }
  int3[] segments { set => Size(cityPathN); }
  void calc_segments() { Size(cityPathN); }
  void calc_reverse_or_transport() { Size(cityPathN); }
  uint[] cityPaths { set => Size(cityN * cityPathN); }
  void init_paths() { Size(cityN, cityPathN); }
  void calc_paths() { Size(cityN, cityPathN - 1); }
  uint[] pathLengths { set => Size(cityPathN); }
  void init_path_lengths() { Size(cityPathN); }
  void calc_path_lengths() { Size(cityN, cityPathN); }
  uint[] min_pathLength_i { set => Size(2); }
  void init_min_path_length() { Size(1); }
  void find_min_path_length() { Size(cityPathN); }
  void find_min_path_lengthI() { Size(cityPathN); }
  void save_best_path() { Size(cityPathN); }
  bool isDrawAnneal, isDrawSignal;
  [GS_UI, AttGS(GS_Render.Quads, UI.ShowIf, "isDrawAnneal")] void vert_Draw_Cities_Border() { Size(5); }
  [GS_UI, AttGS(GS_Render.Quads, UI.ShowIf, "isDrawAnneal")] void vert_Draw_Cities() { Size(cityN); }
  [GS_UI, AttGS(GS_Render.Quads, UI.ShowIf, "isDrawAnneal")] void vert_Draw_City_Path() { Size(cityN); }
  [GS_UI, AttGS("Anneal|Simulated annealing for traveling salesman problem")] TreeGroupEnd groupEnd_Anneal;

  gsBDraw BDraw;
  #region <BDraw>
  enum BDraw_Draw { Point, Sphere, Line, Arrow, Signal, LineSegment, Texture_2D, Quad, WebCam, Mesh, Number, N }
  uint BDraw_AppendBuff_IndexN, BDraw_AppendBuff_BitN, BDraw_AppendBuff_N;
  uint[] BDraw_AppendBuff_Bits, BDraw_AppendBuff_Sums, BDraw_AppendBuff_Indexes;
  void BDraw_AppendBuff_Get_Bits() { Size(BDraw_AppendBuff_BitN); }
  void BDraw_AppendBuff_Get_Existing_Bits() { Size(BDraw_AppendBuff_BitN); }
  void BDraw_AppendBuff_Get_Bits_Sums() { Size(BDraw_AppendBuff_BitN); Sync(); }
  [GS_UI, AttGS(GS_Buffer.GroupShared_Size, 1024)] uint[] BDraw_AppendBuff_grp, BDraw_AppendBuff_grp0;
  uint BDraw_AppendBuff_BitN1, BDraw_AppendBuff_BitN2;
  uint[] BDraw_AppendBuff_Fills1, BDraw_AppendBuff_Fills2;
  void BDraw_AppendBuff_GetSums() { Size(BDraw_AppendBuff_BitN); Sync(); }
  void BDraw_AppendBuff_Get_Existing_Sums() { Size(BDraw_AppendBuff_BitN); Sync(); }
  void BDraw_AppendBuff_GetFills1() { Size(BDraw_AppendBuff_BitN1); Sync(); }
  void BDraw_AppendBuff_GetFills2() { Size(BDraw_AppendBuff_BitN2); Sync(); }
  void BDraw_AppendBuff_IncFills1() { Size(BDraw_AppendBuff_BitN1); }
  void BDraw_AppendBuff_IncSums() { Size(BDraw_AppendBuff_BitN); }
  void BDraw_AppendBuff_GetIndexes() { Size(BDraw_AppendBuff_BitN); }
  struct BDraw_FontInfo { float2 uvBottomLeft, uvBottomRight, uvTopLeft, uvTopRight; int advance, bearing, minX, minY, maxX, maxY; };
  struct BDraw_TextInfo { float3 p, right, up, p0, p1; float2 size, uvSize; float4 color, backColor; uint justification, textI, quadType, axis; float height; };
  enum BDraw_TextAlignment { BottomLeft, CenterLeft, TopLeft, BottomCenter, CenterCenter, TopCenter, BottomRight, CenterRight, TopRight }
  enum BDraw_Text_QuadType { FrontOnly, FrontBack, Switch, Arrow, Billboard }
  const uint BDraw_Draw_Text3D = 12;
  const uint BDraw_maxByteN = 2097152, BDraw_LF = 10, BDraw_TB = 9, BDraw_ZERO = 0x30, BDraw_NINE = 0x39, BDraw_PERIOD = 0x2e, BDraw_COMMA = 0x2c, BDraw_PLUS = 0x2b, BDraw_MINUS = 0x2d, BDraw_SPACE = 0x20;
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

  [GS_UI, AttGS("UI|Rand Tutorial")] TreeGroupEnd groupEnd_UI;
}
