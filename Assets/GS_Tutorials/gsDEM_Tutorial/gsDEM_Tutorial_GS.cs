using GpuScript;
using UnityEngine;

public class gsDEM_Tutorial_GS : _GS
{
  [GS_UI, AttGS("UI|User Interface")] TreeGroup group_UI;
  [GS_UI, AttGS("DEM|Distinct Element Method Group")] TreeGroup group_DEM;
  enum UInts { maxForce, maxSmp, maxSimGridVal, nearestPnt, timeChanged, dm_minMass, dm_maxStiffness, dm_minDamping, dm_Energy, N };
  uint[] uints { set => Size(UInts.N); }
  struct Node { float3 position, velocity, position0, velocity0; uint u; float mass, stiff, damp; }
  Node[] nodes;
  uint3 nodeN3, gridN3, dimension3;
  int[] forces;
  void BuildNodes() { Size(nodeN); }
  void zero_forces() { Size(nodeN); }
  void calc_forces() { Size(nodeN); }
  void move_nodes() { Size(nodeN); }
  void check_collisions() { Size(nodeN * (nodeN - 1) / 2); }
  [GS_UI, AttGS("Display Nodes|Show or hide the Nodes")] bool displayNodes;
  [GS_UI, AttGS("Node N|The number of nodes", UI.OnValueChanged, "GenerateNodes();", UI.ValRange, 10, 0, 10000, UI.Format, "#,##0", UI.Pow2_Slider, UI.ShowIf, nameof(displayNodes))] uint nodeN;
  [GS_UI, AttGS("Resolution|Voxel size", UI.OnValueChanged, "GenerateNodes();", UI.ValRange, 0.2f, 0.001f, 1, siUnit.m, UI.Format, "0.000", UI.Pow2_Slider, UI.ShowIf, nameof(displayNodes))] float resolution;
  [GS_UI, AttGS("GenerateNodes", UI.ShowIf, false)] void GenerateNodes() { }
  [GS_UI, AttGS("Run|Run continuously", UI.OnValueChanged, "GenerateNodes();")] bool runContinuously;
  [GS_UI, AttGS("DEM|Distinct Element Method Group")] TreeGroupEnd groupEnd_DEM;
  [GS_UI, AttGS("UI|User Interface")] TreeGroupEnd group_UI_End;
  [GS_UI, AttGS(GS_Render.Quads, UI.ShowIf, nameof(displayNodes))] void vert_Nodes() { Size(nodeN); }

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

}
