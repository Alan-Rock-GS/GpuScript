using GpuScript;
using System.Linq;

public class gsDirectedGraph : gsDirectedGraph_
{
  public string graphFile => $"{projectPath}uiEdge.txt";
  public override void OpenEdgeFile()
  {
    string f = graphFile;
    if (f.DoesNotExist()) f.WriteAllText(StrBldr().AddTabRow("Stage", "Node0", "Node1", "Cost"));
    if (notepad) f.Run(); if (excel) RunExcel(projectPath, f); if (visual_studio) Open_File_in_Visual_Studio(f);
  }
  public override void LoadEdgeFile()
  {
    string[][] lines = graphFile.ReadAllLineItems_RemoveCommentsAndBlanks();
    ui_edges = new uiEdge[lines.Length - 1];
    For(1, lines.Length, i => { var s = lines[i]; ui_edges[i - 1] = new uiEdge() { stage = s[0].To_uint(), node0 = s[1].To_uint(), node1 = s[2].To_uint(), cost = s[3].To_float() }; });
    ui_edges_To_UI();
    initGraph();
  }
  public override void onLoaded() { base.onLoaded(); initGraph(); }
  public override void init_stages_GS(uint3 id) => stages[id.x] = 0;
  public override void calc_stages_GS(uint3 id) => InterlockedMax(stages, 0, edges[id.x].stage + 2);
  public override void init_stageNodeNs_GS(uint3 id) => stageNodeNs[id.x] = id.x < stageN - 1 ? 0 : 1u;
  public override void calc_stageNodeNs_GS(uint3 id) => InterlockedMax(stageNodeNs, edges[id.x].stage, edges[id.x].node0 + 1);
  public override void init_stageSums_GS(uint3 id) => stageSums[id.x] = 0;
  public override void calc_stageSums_GS(uint3 id) { uint i = id.x, j = id.y; if (i >= j) InterlockedAdd(stageSums, i, stageNodeNs[j]); }
  public void initGraph()
  {
    if (ui_edges.Length == 0 || gDirectedGraph == null) return;
    edgeN = ui_edges.uLength();
    AssignData_edges(ui_edges.Select(e => new Edge() { stage = e.stage, node0 = e.node0, node1 = e.node1, cost = e.cost }).ToArray());
    AllocData_stages(1); Gpu_init_stages();
    Gpu_calc_stages();
    AllocData_stageNodeNs(stageN = stages[0]); AllocData_stageSums(stageN); AllocData_bestPath(stageN);
    Gpu_init_stageNodeNs(); Gpu_calc_stageNodeNs(); Gpu_init_stageSums(); Gpu_calc_stageSums();
    AllocData_nodePs(nodeN = For(stageN).Select(i => stageNodeNs[i]).Sum());
    AllocData_NodeCosts(nodeN); Gpu_calc_Nodes();
    BDraw_textN = nodeN + edgeN; BuildTexts();
  }
  public override void calc_Nodes_GS(uint3 id)
  {
    uint i = id.x;
    Edge e = edges[i];
    uint d_node0 = e.stage == 0 ? 0 : stageSums[e.stage - 1];
    if (i == edgeN - 1) nodePs[nodeN - 1] = f100;
    nodePs[d_node0 + e.node0] = float3(e.stage, -0.5f * (e.node0 - (stageNodeNs[e.stage] - 1) / 2.0f), 0) / (stageN - 1.0f);

    e.node0 += d_node0; e.node1 += stageSums[e.stage];
    edges[i] = e;
  }
  public float radius() => 0.025f * plotScale;
  public override v2f vert_drawNodes(uint i, uint j, v2f o)
  {
    float color = 0;
    for (int I = 0; I < stageN; I++) if (i == bestPath[I]) { color = 0.5f; break; }
    if (i == 0) color = 0.5f;
    return vert_BDraw_Sphere(nodePs[i], radius(), palette(color), i, j, o);
  }
  public override v2f vert_drawEdges(uint i, uint j, v2f o)
  {
    float r = radius();
    Edge e = edges[i];
    float3 p0 = nodePs[e.node0] + f001 * 0.01f, p1 = nodePs[e.node1] + f001 * 0.01f, d = normalize(p1 - p0) * r;
    r /= 10;
    bool found = false;
    for (int I = 1; I < stageN; I++) if (e.node0 == bestPath[I - 1] && e.node1 == bestPath[I]) { r *= 3; found = true; break; }
    if (!found && e.stage == 0 && e.node1 == bestPath[1]) r *= 3;
    return vert_BDraw_Arrow(p0 + d, p1 - d, r, palette(e.cost / 9), i, j, o);
  }
  public override v2f vert_BDraw_Text(BDraw_TextInfo t, uint i, uint j, v2f o)
  {
    uint nodeI = i, edgeI = i - nodeN;
    if (i < nodeN) t.p = nodePs[nodeI]; else { Edge e = edges[edgeI]; t.p = lerp(nodePs[e.node0], nodePs[e.node1], 0.4f); }
    t.p -= 0.001f * f001;
    t.color = BLACK; t.height = radius();
    return base.vert_BDraw_Text(t, i, j, o);
  }
  public override BDraw_TextInfo BDraw_textInfo(uint i) => BDraw_textInfos[i % BDraw_textN];
  public override void BDraw_textInfo(uint i, BDraw_TextInfo t) => BDraw_textInfos[i % BDraw_textN] = t;
  void AddText(string t, float textHeight) => BDraw_AddText(t, f000, f100, f011, BLUE, f0000, textHeight, BDraw_Text_QuadType.Billboard, BDraw_TextAlignment.CenterCenter);
  public void BuildTexts()
  {
    if (gDirectedGraph == null) return;
    BDraw_ClearTexts();
    float textHeight = radius();
    For(nodeN, i => AddText(i == 0 || NodeCosts[i] > 0 ? i2f(NodeCosts[i]).ToString("0") : "", textHeight));
    For(edgeN, i => AddText(edges[i].cost.ToString("0"), textHeight));
    BDraw_BuildTexts();
  }
  public void CalcPath(CalcMode c)
  {
    calcMode = c;
    Gpu_init_NodeCosts();
    For(stageN - 1, i => { stageI = i; Gpu_calc_NodeCosts(); });
    For(stageN - 1, i => { stageI = stageN - 1 - i; Gpu_find_BestPath(); });
    BuildTexts();
  }
  public override void CalcMinPath() => CalcPath(CalcMode.Min);
  public override void CalcMaxPath() => CalcPath(CalcMode.Max);
  public override void init_NodeCosts_GS(uint3 id) => NodeCosts[id.x] = id.x == 0 ? 0 : calcMode == CalcMode_Min ? int_max : int_min;
  public int f2i(float v) => roundi(v * 1e6f);
  public float i2f(int v) => v / 1e6f;
  public override void calc_NodeCosts_GS(uint3 id)
  {
    Edge e = edges[id.x];
    if (e.stage == stageI)
      if (calcMode == CalcMode_Min) InterlockedMin(NodeCosts, e.node1, f2i(e.cost) + NodeCosts[e.node0]);
      else InterlockedMax(NodeCosts, e.node1, f2i(e.cost) + NodeCosts[e.node0]);
  }
  public override void find_BestPath_GS(uint3 id)
  {
    uint eI = id.x;
    Edge e = edges[eI];
    if (stageI == stageN - 1 && eI == 0) bestPath[stageI] = nodeN - 1;
    else if (e.stage == stageI && e.node1 == bestPath[stageI + 1] && NodeCosts[e.node1] - f2i(e.cost) == NodeCosts[e.node0]) bestPath[stageI] = e.node0;
  }
}