using GpuScript;
using UnityEngine;

public class gsRand_Tutorial_GS : _GS
{
  [GS_UI, AttGS("UI|Rand Tutorial")] TreeGroup group_UI;
  [GS_UI, AttGS("Thickness|Line thickness", UI.Format, "0.000", UI.ValRange, 0.004f, 0.001f, 0.01f)] float lineThickness;

  [GS_UI, AttGS("Rand|Rand test")] TreeGroup group_Rand;
  [GS_UI, AttGS("Random Number N|Number of random numbers", UI.Format, "#,##0", UI.ValRange, 128, 8, 33554432, UI.IsPow2, UI.Pow2_Slider, UI.OnValueChanged, "Init_randomNumbers(); Avg();")] uint randomNumberN;

  [GS_UI, AttGS("Avg|Calculate random number average")] TreeGroup group_Avg;
  [GS_UI, AttGS("Init|Initialize Random Number Buffer")] void Init_randomNumbers() { }
  [GS_UI, AttGS("Calc Average|Calculate the average of an array of random numbers")] void Avg() { }
  [GS_UI, AttGS("Average|Calculated average, should be close to zero", UI.Format, "0.000000", UI.ReadOnly)] float Avg_Val;
  [GS_UI, AttGS("Runtime|Time to generate a single random number", Unit.ns, UI.Format, "#,##0.000000", UI.ReadOnly)] float Avg_Val_Runtime;
  [GS_UI, AttGS("TFlops|Tera-Flops per second", UI.ReadOnly, UI.Format, "#,##0.000")] float Avg_Val_TFlops;
  int[] ints { set => Size(1); }
  void Calc_Random_Numbers() { Size(randomNumberN); }
  void Calc_Average() { Size(randomNumberN); }
  uint4[] randomNumbers { set => Size(randomNumberN); }
  [GS_UI, AttGS(GS_Render.Quads)] void vert_Draw_Random_Signal() { Size(1); }
  [GS_UI, AttGS(GS_Render.Quads)] void vert_Draw_Calc_Avg() { Size(1); }
  [GS_UI, AttGS(GS_Render.Quads)] void vert_Draw_Avg() { Size(1); }
  [GS_UI, AttGS(GS_Render.Quads)] void vert_Draw_Pnts() { Size(randomNumberN); }
  [GS_UI, AttGS(GS_Render.Quads)] void vert_Draw_Border() { Size(12); }
  [GS_UI, AttGS("Avg|Calculate random number average")] TreeGroupEnd groupEnd_Avg;

  [GS_UI, AttGS("Rand|Rand test")] TreeGroupEnd groupEnd_Rand;

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

  [GS_UI, AttGS("UI|Rand Tutorial")] TreeGroupEnd groupEnd_UI;
}
