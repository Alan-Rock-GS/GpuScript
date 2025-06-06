using GpuScript;
using UnityEngine;
public class gsDirectedGraph_GS : _GS
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
  [GS_UI, AttGS(GS_Render.Quads)] void vert_BDraw_Text() { Size(BDraw_textN); }
  [GS_UI, AttGS(GS_Render.Quads)] void vert_BDraw_Box() { Size(BDraw_boxEdgeN); }

  #endregion <BDraw>

  [GS_UI, AttGS("UI|User Interface")] TreeGroup group_UI;
  [GS_UI, AttGS("Directed Graph|DirectedGraph specification")] TreeGroup group_DirectedGraph;

  [GS_UI, AttGS("Data|Edge file data")] TreeGroup group_DirectedGraph_Data;
  [GS_UI, AttGS("Notepad|Open data sets in Notepad")] bool notepad;
  [GS_UI, AttGS("Excel|Open data sets in Excel")] bool excel;
  [GS_UI, AttGS("VS|Open data sets in Visual Studio")] bool visual_studio;
  [GS_UI, AttGS("Open Edge File|Open graph csv file")] void OpenEdgeFile() { }
  [GS_UI, AttGS("Load Edge File|Load graph from csv file")] void LoadEdgeFile() { }
  [GS_UI, AttGS("Data|Edge file data")] TreeGroupEnd groupEnd_DirectedGraph_Data;

  [GS_UI, AttGS("Plot|Plot settings")] TreeGroup group_DirectedGraph_Plot;
  [GS_UI, AttGS("Scale|From node index at this stage", UI.ValRange, 1, 0, 2, UI.Pow2_Slider, UI.Format, "0.00")] float plotScale;
  //enum Palette_Type { Rainbow, GradientRainbow, GradientRainbow10, GradientRainbow20, Heat, GradientHeat, WhiteRainbow, invRainbow, Green, Gray, DarkGray, CT }
  enum Palette_Type { Rainbow, GradientRainbow, GradientRainbow10, GradientRainbow20, Heat, GradientHeat, WhiteRainbow, invRainbow, Green, Gray, DarkGray }
  //[GS_UI, AttGS("Palette|Select the palette type", UI.OnValueChanged, "_PaletteTex = LoadPalette(UI_" + nameof(paletteType) + ".textString, ref paletteBuffer);")] PaletteType paletteType;
  [GS_UI, AttGS("Palette|Select the palette type", UI.OnValueChanged, "OCam_Lib.paletteType = (gsOCam_Lib_.PaletteType)palette_Type")] Palette_Type palette_Type;
  //[GS_UI, AttGS("Palette|Select the palette type")] Palette_Type palette_Type;
  [GS_UI, AttGS("Plot|Plot settings")] TreeGroupEnd groupEnd_DirectedGraph_Plot;

  class uiEdge
  {
    [GS_UI, AttGS("Stage|Edge stage index", UI.ValRange, 0, 0, 10)] uint stage;
    [GS_UI, AttGS("Node 0|From node index at this stage", UI.ValRange, 0, 0, 10)] uint node0;
    [GS_UI, AttGS("Node 1|To node index at the next stage", UI.ValRange, 0, 0, 10)] uint node1;
    [GS_UI, AttGS("Cost|Edge cost from Node 0 at this stage to Node 1 on the next stage", UI.ValRange, 0, 0, 10, UI.Format, "0.0")] float cost;
  }
  [GS_UI, AttGS("Edge|Edge data for graph", UI.DisplayRowN, 20)] uiEdge[] ui_edges;

  //uint edgePathN;
  //uint[] edgePaths { set => Size(stageN - 1, edgePathN); }
  //void init_edgePaths() { Size(stageN - 1, edgePathN); }
  //void calc_edgePaths() { Size(stageN - 1, edgePathN); }
  uint stageI;
  int[] NodeCosts { set => Size(nodeN); }
  void init_NodeCosts() { Size(nodeN); }
  void calc_NodeCosts() { Size(edgeN); }
  uint[] bestPath { set => Size(stageN); }
  void find_BestPath() { Size(edgeN); }
  enum CalcMode { Min, Max }
  CalcMode calcMode;
  [GS_UI, AttGS("Min Path|Calculate minimum path")] void CalcMinPath() { }
  [GS_UI, AttGS("Max Path|Calculate minimum path")] void CalcMaxPath() { }

  struct Edge { uint stage, node0, node1; float cost; }
  Edge[] edges { set => Size(edgeN); }
  uint stageN, nodeN, edgeN;
  uint[] stages, stageNodeNs, stageSums;
  void init_stages() { Size(1); }
  void calc_stages() { Size(edgeN); }
  void init_stageNodeNs() { Size(stageN); }
  void calc_stageNodeNs() { Size(edgeN); }
  void init_stageSums() { Size(stageN); }
  void calc_stageSums() { Size(stageN, stageN); }
  void calc_Nodes() { Size(edgeN); }

  float3[] nodePs;
  [GS_UI, AttGS(GS_Render.Quads)] void vert_drawNodes() { Size(nodeN); }
  [GS_UI, AttGS(GS_Render.Quads)] void vert_drawEdges() { Size(edgeN); }
  [GS_UI, AttGS("Directed Graph|DirectedGraph specification")] TreeGroupEnd groupEnd_DirectedGraph;

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