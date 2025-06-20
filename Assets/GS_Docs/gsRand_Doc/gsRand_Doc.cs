using GpuScript;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class gsRand_Doc : gsRand_Doc_
{
  uint iterN = 100;
  public float Calc_Avg(uint pointN, uint iterationN)
  {
    Rand_Init(pntN = pointN);
    var a = For(iterationN).Select(a => { ints[0] = 0; return (time: Secs(() => Gpu_Calc_Average()), avg: i2f(ints[0]) / pntN); });
    (Avg_Val_Runtime, Avg_Val) = (a.Select(t => t.time).Min() * iterationN, a.Select(t => t.avg).Sum() / iterationN);
    Avg_Val_TFlops = 26.0f * iterationN * pntN / Avg_Val_Runtime / 1.0e9f;
    return Avg_Val;
  }
  public override void Avg() => Calc_Avg(pntN, iterN);
  public override void Calc_Average_GS(uint3 id) => InterlockedAdd(ints, 0, f2i(Rand_Float(id.x, -1, 1)));
  public override void LateUpdate1_GS()
  {
    base.LateUpdate1_GS();
    if (drawGroup == DrawGroup.Average) Avg();
    if (drawGroup == DrawGroup.PI_Area) Area_PI();
    if (drawGroup == DrawGroup.PI_Integral) Integral_PI();
    //frag(vert(0, 0));
  }
  public override void Area_PI() => Calc_Area_PI(pntN, iterN, false);
  public float Calc_Area_PI(uint pointN, uint iterationN, bool inCircle)
  {
    Rand_Init(2 * (pntN = pointN));
    IEnumerable<(float time, float piVal)> a;
    if (inCircle) a = For(iterationN).Select(a => { uints[UInts_Count] = 0; return (time: Secs(() => Gpu_Count_Pnts_in_Circle()), piVal: 4.0f * uints[UInts_Count] / pntN); });
    else a = For(iterationN).Select(a => { uints[UInts_Count] = 0; return (time: Secs(() => Gpu_Count_Pnts_out_of_Circle()), piVal: 4.0f * (pntN - uints[UInts_Count]) / pntN); });
    (Area_PI_Runtime, Area_PI_Val) = (a.Select(t => t.time).Min() * iterationN, a.Select(t => t.piVal).Sum() / iterationN);
    Area_PI_TFlops = (25.0f * 2 + 11) * iterationN * pntN / Area_PI_Runtime / 1.0e9f;
    Area_PI_Error = abs(Area_PI_Val - PI);
    return Area_PI_Val;
  }
  public override void Count_Pnts_in_Circle_GS(uint3 id) { uint i = id.x << 1; if (distance(2 * float2(Rand_Float(i), Rand_Float(i + 1)), f11) < 1) InterlockedAdd(uints, UInts_Count, 1); }
  public override void Count_Pnts_out_of_Circle_GS(uint3 id) { uint j = id.x << 1; if (distance(2 * float2(Rand_Float(j), Rand_Float(j + 1)), f11) > 1) InterlockedAdd(uints, 0, 1); }
  public float CalcIntegralPI(uint pointN, uint iterationN)
  {
    Rand_Init(pntN = pointN);
    float piSum = 0, piVal;
    Integral_PI_Runtime = For(iterationN).Select(a => Secs(() => { ints[0] = 0; Gpu_Calc_Integral_PI(); piVal = 4 * (i2f(ints[0]) / pntN + PIo4); piSum += piVal; })).Min() * iterationN;
    Integral_PI_TFlops = (25.0f + 11) * iterationN * pntN / Integral_PI_Runtime / 1.0e9f;
    Integral_PI_Val = piSum / iterationN;
    Integral_PI_Error = abs(Integral_PI_Val - PI);
    return Integral_PI_Val;
  }
  public override void Integral_PI() => CalcIntegralPI(pntN, iterN);
  public override void Calc_Integral_PI_GS(uint3 id) => InterlockedAdd(ints, 0, f2i(sqrt(1 - sqr(Rand_Float(id.x))) - PIo4));
  public override void OnValueChanged_GS()
  {
    base.OnValueChanged_GS();
    if (ui_group_Avg.Changed && group_Avg) drawGroup = DrawGroup.Average;
    else if (UI_group_TSP.Changed && group_TSP) drawGroup = DrawGroup.TSP;
    else if (UI_group_Area_PI.Changed && group_Area_PI) drawGroup = DrawGroup.PI_Area;
    else if (UI_group_Integral_PI.Changed && group_Integral_PI) drawGroup = DrawGroup.PI_Integral;
  }
  public float signal_panel_width() => 0.1f;
  public override uint BDraw_SignalSmpN(uint chI) => pntN;
  public override float4 BDraw_SignalColor(uint chI, uint smpI) => GREEN;
  public override float4 BDraw_SignalBackColor(uint chI, uint smpI) => float4(1, 1, 1, 0.2f);
  public override float BDraw_SignalSmpV(uint chI, uint smpI) => Rand_rFloat(smpI, -1, 1);
  public override float BDraw_SignalThickness(uint chI, uint smpI) => lineThickness;
  public override v2f vert_Draw_Random_Signal(uint i, uint j, v2f o) => vert_BDraw_Signal(float3(-1, 1.1f, 0), float3(1, 1.1f, 0), signal_panel_width(), i, j, o);
  public override v2f vert_Draw_Avg(uint i, uint j, v2f o) { float3 p = -signal_panel_width() * f001; return vert_BDraw_Line(p - f100, p + f100, lineThickness * 4, BLUE, i, j, o); }
  public override v2f vert_Draw_Calc_Avg(uint i, uint j, v2f o) { float3 p = signal_panel_width() * float3(0, Avg_Val, -2); return vert_BDraw_Line(p - f100, p + f100, lineThickness * 2, RED, i, j, o); }
  public override v2f vert_Draw_Pnts(uint i, uint j, v2f o)
  {
    if (drawGroup == DrawGroup.Average) return vert_BDraw_Sphere(float3(i / (pntN - 1.0f) * 2 - 1, Rand_rFloat(i) * 2 - 1, 0), 0.01f, Rand_rFloat(i) > 0.5f ? BLUE : RED, i, j, o);
    if (drawGroup == DrawGroup.PI_Area) return vert_BDraw_Sphere(float3(Rand_rFloat(i * 2) * 2 - 1, Rand_rFloat(i * 2 + 1) * 2 - 1, 0), 0.01f, length(float2(Rand_rFloat(i * 2), Rand_rFloat(i * 2 + 1))) < 1 ? BLUE : RED, i, j, o);
    return vert_BDraw_Sphere(float3(Rand_rFloat(i), sqrt(1 - sqr(Rand_rFloat(i))), 0) * 2 - 1, 0.01f, Rand_rFloat(i) > PIo4 ? BLUE : RED, i, j, o);
  }
  public override IEnumerator RunTSP_Sync()
  {
    starPathN = 20500000 / starN;
    ClockSec();
    InitBuffers();
    Rand_Init(max(starN * 3, starPathN * 4));
    Gpu_init_stars(); Gpu_init_paths();
    float minPathLength = fPosInf, runtime = 0;
    uint stuckN = 0, i;
    var s = StrBldr().AddTabLine("i", "Length", "Stuck");
    for (i = 0; i < starN * 10 && stuckN < 50; i++)
    {
      Gpu_calc_segments(); Gpu_calc_paths(); Gpu_init_path_lengths(); Gpu_calc_path_lengths();
      Gpu_init_min_path_length(); Gpu_find_min_path_length(); Gpu_find_min_path_lengthI(); Gpu_save_best_path();
      if (i % 500 == 0) { runtime += ClockSec(); yield return null; ClockSec(); }
      float pathLength = u2f(uints[UInts_MinPathLength]);
      if (pathLength < minPathLength) { minPathLength = pathLength; if (i == 0 || stuckN > 1) s.AddTabLine(i, minPathLength, stuckN); stuckN = 0; } else stuckN++;
    }
    runtime += ClockSec();
    s.AddTabLine(i, minPathLength, stuckN);
    print($"StarN = {starN}, minPathLength[{i}] = {minPathLength}, Time = {runtime}\n{s} secs");
    yield return Status();
  }
  public override void save_best_path_GS(uint3 id) { uint starI = id.x; bestPath(starI, starPath(uints[UInts_MinPathI] - 1, starI)); }
  public override void init_min_path_length_GS(uint3 id) => uints[id.x + 1] = id.x == 0 ? uint_max : 0;
  public override void find_min_path_length_GS(uint3 id) => InterlockedMin(uints, UInts_MinPathLength, pathLengths[id.x]);
  public override void find_min_path_lengthI_GS(uint3 id) { uint i = id.x, len = uints[UInts_MinPathLength]; if (pathLengths[i] == len && len < pathLengths[0]) { uints[UInts_MinPathI] = i; uints[UInts_Count] = 0; } }
  public float u2f(uint u) => u * 1e-5f;
  public uint f2u(float f) => roundu(f / u2f(1));
  public float i2f(int i) => i * 1e-6f;
  public int f2i(float f) => roundi(f / i2f(1));
  public override void init_path_lengths_GS(uint3 id) => pathLengths[id.x] = 0;
  public override void calc_path_lengths_GS(uint3 id)
  {
    uint starI = id.x, pathI = id.y, starJ = (starI + 1) % starN;
    uint aI = starPath(pathI - 1, starI), bI = starPath(pathI - 1, starJ);
    float3 a = stars[aI], b = stars[bI];
    InterlockedAdd(pathLengths, pathI, f2u(max(0, distance(a, b) - starBorderReward * Is(a.x * b.x < 0))));
  }
  public override void calc_segments_GS(uint3 id)
  {
    uint i = id.x, j = i * 4, a = Rand_UInt(j + 1, 0, starN), b = a + 1 + Rand_UInt(j + 2, 0, starN - 1);
    bool isReverse = Rand_Float(j) < 0.5f;
    int c = isReverse ? -1 : (int)(b + 1 + Rand_UInt(j + 3, 0, starN - (b - a)));
    segments[i] = int3(a, b, c);
  }
  public uint starPath(uint pathI, uint starI) => starPaths[(pathI + 1) * starN + (starI % starN)];
  public void starPath(uint pathI, uint starI, uint v) => starPaths[(pathI + 1) * starN + (starI % starN)] = v;
  public uint bestPath(uint starI) => starPaths[starI % starN];
  public void bestPath(uint starI, uint v) => starPaths[starI % starN] = v;
  public bool IsInPath(uint i, uint a, uint b) { a %= starN; b %= starN; if (i < a) i += starN; if (b < a) b += starN; return i >= a && i <= b; }
  public override void init_paths_GS(uint3 id) { uint pI = id.y, cI = id.x; if (pI == 0) bestPath(cI, cI); else starPath(pI, cI, uint_max); }
  public uint ReversePath(uint starI, uint a, uint b) => IsInPath(starI, a, b) ? (b - (starI - a) + starN) % starN : starI;
  public uint TransferPath(uint starJ, uint a, uint c) => starJ + c - a;
  public uint RemovePath(uint starJ, uint a, uint b) => starJ - (starJ > b % starN ? b >= starN ? ((b + 1) % starN) : b - a + 1 : 0);
  public uint InsertPath(uint starJ, uint a, uint b, uint c) { uint d = c + b - a; return starJ + (starJ >= c % starN ? b - a + 1 : d % starN <= b - a ? (d + 1) % starN : 0); }
  public uint MovePath(uint starI, uint a, uint b, uint c) => IsInPath(starI, a, b) ? TransferPath(starI, a, c) : InsertPath(RemovePath(starI, a, b), a, b, c);
  public override void calc_paths_GS(uint3 id)
  {
    uint pathI = id.y, starI = id.x;
    int3 seg = segments[pathI];
    uint a = (uint)seg.x, b = (uint)seg.y, c = (uint)seg.z;
    uint starJ = seg.z == -1 ? ReversePath(starI, a, b) : MovePath(starI, a, b, c);
    starPath(pathI, starJ, bestPath(starI));
  }
  public override void init_stars_GS(uint3 id) { uint i = id.x, j = i * 3; stars[i] = float3(Rand_Float(j, -1, 1), Rand_Float(j + 1, -1, 1), Rand_Float(j + 2, -1, 1)); }
  public override v2f vert_Draw_Stars_Border(uint i, uint j, v2f o)
  {
    float r = 0.01f;
    if (i < 12) return vert_BDraw_BoxFrame(f___, f111, r, BLACK, i, j, o);
    else return vert_BDraw_Quad(f0__, f0_1, f011, f01_, float4(0, 0, 1, 0.25f), i, j, o);
  }
  public override v2f vert_Draw_Stars(uint i, uint j, v2f o) => vert_BDraw_Sphere(stars[i], lineThickness * 2, YELLOW, i, j, o);
  public override v2f vert_Draw_Star_Path(uint i, uint j, v2f o)
  {
    float3 p0 = stars[bestPath(i)], p1 = stars[bestPath((i + 1) % starN)];
    float t = (i - ((_Time.y * 100) % starN) + starN) % starN, n = 100, r = lineThickness * (t < n ? 4 * t / n + 1 : 1);
    return vert_BDraw_Line(p0, p1, r, t < n ? palette(t / n / 2 + 0.5f) : palette(lerp1(-1, 1, p0.x * p1.x < 0 ? starBorderReward : 0)), i, j, o);
  }
}