using GpuScript;
using UnityEngine;
public class gsDirectedGraph_GS : _GS
{
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
  [GS_UI, AttGS("Palette|Select the palette type", UI.OnValueChanged, "ACam_Lib.paletteType = (gsACam_Lib_.PaletteType)palette_Type")] Palette_Type palette_Type;
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