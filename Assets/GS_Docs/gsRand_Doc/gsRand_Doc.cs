using GpuScript;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class gsRand_Doc : gsRand_Doc_
{
  //public override void onLoaded() { base.onLoaded(); f2i = 1e6f; }
  uint iterN = 100;
  public float Calc_Avg(uint pointN, uint iterationN)
  {
    Rand_Init(pntN = pointN);
    //var a = (0, iterationN).For().Select(a => { ints[0] = 0; return (time: Secs(() => Gpu_Calc_Average()), avg: ints[0] / f2i / pntN); });
    var a = (0, iterationN).For().Select(a => { ints[0] = 0; return (time: Secs(() => Gpu_Calc_Average()), avg: i2f(ints[0]) / pntN); });
    (Avg_Val_Runtime, Avg_Val) = (a.Select(t => t.time).Sum(), a.Select(t => t.avg).Sum() / iterationN);
    Avg_Val_TFlops = 26.0f * iterationN * pntN / Avg_Val_Runtime / 1.0e9f;
    return Avg_Val;
  }
  public override void Avg() => Calc_Avg(pntN, iterN);
  //public override void Calc_Average_GS(uint3 id) => InterlockedAdd(ints, 0, roundi(Rand_Float(id.x, -1, 1) * f2i));
  public override void Calc_Average_GS(uint3 id) => InterlockedAdd(ints, 0, f2i(Rand_Float(id.x, -1, 1)));

  public override void Area_PI() => Calc_Area_PI(pntN, iterN, false);
  public float Calc_Area_PI(uint pointN, uint iterationN, bool inCircle)
  {
    Rand_Init(2 * (pntN = pointN));
    IEnumerable<(float time, float piVal)> a;
    if (inCircle) a = (0, iterationN).For().Select(a => { uints[UInts_Count] = 0; return (time: Secs(() => Gpu_Count_Pnts_in_Circle()), piVal: 4.0f * uints[UInts_Count] / pntN); });
    else a = (0, iterationN).For().Select(a => { uints[UInts_Count] = 0; return (time: Secs(() => Gpu_Count_Pnts_out_of_Circle()), piVal: 4.0f * (pntN - uints[UInts_Count]) / pntN); });
    (Area_PI_Runtime, Area_PI_Val) = (a.Select(t => t.time).Sum(), a.Select(t => t.piVal).Sum() / iterationN);
    Area_PI_TFlops = (25.0f * 2 + 11) * iterationN * pntN / Area_PI_Runtime / 1.0e9f;
    Area_PI_Error = abs(Area_PI_Val - PI);
    return Area_PI_Val;
  }
  public override void Count_Pnts_in_Circle_GS(uint3 id)
  {
    uint i = id.x << 1;
    if (distance(2 * float2(Rand_Float(i), Rand_Float(i + 1)), f11) < 1) InterlockedAdd(uints, UInts_Count, 1);
  }
  public override void Count_Pnts_out_of_Circle_GS(uint3 id)
  {
    uint j = id.x << 1;
    if (distance(2 * float2(Rand_Float(j), Rand_Float(j + 1)), f11) > 1) InterlockedAdd(uints, 0, 1);
  }
  public float Calc_Integral_PI(uint pointN, uint iterationN)
  {
    Rand_Init(pntN = pointN);
    float piSum = 0, piVal;
    //Integral_PI_Runtime = (0, iterationN).For().Select(a => Secs(() => { uints[UInts_Count] = 0; Gpu_Integral_Avg(); piVal = 4 * (ints[0] / f2i / pntN + PIo4); piSum += piVal; })).Sum();
    Integral_PI_Runtime = (0, iterationN).For().Select(a => Secs(() => { uints[UInts_Count] = 0; Gpu_Integral_Avg(); piVal = 4 * (i2f(ints[0]) / pntN + PIo4); piSum += piVal; })).Sum();
    Integral_PI_TFlops = (25.0f + 11) * iterationN * pntN / Integral_PI_Runtime / 1.0e9f;
    Integral_PI_Val = piSum / iterationN;
    Integral_PI_Error = abs(Integral_PI_Val - PI);
    return Integral_PI_Val;
  }
  public override void Integral_PI() => Calc_Integral_PI(pntN, iterN);
  //public override void Integral_Avg_GS(uint3 id) => InterlockedAdd(ints, 0, roundi((sqrt(1 - sqr(Rand_Float(id.x))) - PIo4) * f2i));
  public override void Integral_Avg_GS(uint3 id) => InterlockedAdd(ints, 0, f2i(sqrt(1 - sqr(Rand_Float(id.x))) - PIo4));
  public override void OnValueChanged_GS()
  {
    base.OnValueChanged_GS();
    if (ui_group_Avg.Changed && group_Avg) drawGroup = DrawGroup.Average;
    else if (UI_group_Anneal.Changed && group_Anneal) drawGroup = DrawGroup.Anneal;
    else if (UI_group_Area_PI.Changed && group_Area_PI) drawGroup = DrawGroup.PI_Area;
    else if (UI_group_Area_PI.Changed && group_Area_PI) drawGroup = DrawGroup.PI_Integral;
  }
  public float signal_panel_width() => 0.1f;
  public override uint BDraw_SampleN() => pntN;
  public override float4 BDraw_SignalColor() => GREEN;
  public override float4 BDraw_SignalBackColor() => float4(1, 1, 1, 0.2f);
  public override float BDraw_SampleV(uint i) => lerp(-1, 1, Rand_rFloat(i));
  public override float BDraw_SignalThickness() => lineThickness;
  public override v2f vert_Draw_Random_Signal(uint i, uint j, v2f o) => vert_BDraw_Signal(f_00, f100, signal_panel_width(), i, j, o);
  public override v2f vert_Draw_Avg(uint i, uint j, v2f o) { float3 p = -signal_panel_width() * f001; return vert_BDraw_Line(p - f100, p + f100, lineThickness * 4, BLUE, i, j, o); }
  public override v2f vert_Draw_Calc_Avg(uint i, uint j, v2f o) { float3 p = signal_panel_width() * float3(0, Avg_Val, -2); return vert_BDraw_Line(p - f100, p + f100, lineThickness * 2, RED, i, j, o); }
  public override IEnumerator RunAnneal_Sync()
  {
    starPathN = 25 * starN;
    InitBuffers();
    Rand_Init(starN * starPathN * 2);
    Gpu_init_cities(); Gpu_init_paths();
    uints[UInts_Count] = 0;
    for (uint i = 0; i < starN * 10; i++)
    {
      uints[UInts_Count] = uints[UInts_Count] + 1;
      Gpu_calc_segments(); Gpu_calc_paths(); Gpu_init_path_lengths(); Gpu_calc_path_lengths();
      Gpu_init_min_path_length(); Gpu_find_min_path_length(); Gpu_find_min_path_lengthI(); Gpu_save_best_path();
      if (uints[UInts_Count] > 0) { if (uints[UInts_Count] == 10) break; yield return Status(uints[UInts_Count], 10, "Finishing"); }
      else if (i % 500 == 0) yield return Status(i, starN * 10, "Computing");
    }
    yield return Status();
  }
  public override void save_best_path_GS(uint3 id) { uint starI = id.x; bestPath(starI, starPath(uints[UInts_MinPathI] - 1, starI)); }
  public override void init_min_path_length_GS(uint3 id) => uints[id.x + 1] = id.x == 0 ? uint_max : 0;
  public override void find_min_path_length_GS(uint3 id) => InterlockedMin(uints, UInts_MinPathLength, pathLengths[id.x]);
  public override void find_min_path_lengthI_GS(uint3 id)
  {
    uint i = id.x, len = uints[UInts_MinPathLength];
    if (pathLengths[i] == len && len < pathLengths[0]) { uints[UInts_MinPathI] = i; uints[UInts_Count] = 0; }
  }

  public float u2f(uint u) => u * 1e6f;
  public uint f2u(float f) => roundu(f * u2f(1));
  public float i2f(int i) => i * 1e6f;
  public int f2i(float f) => roundi(f * i2f(1));
  public override void init_path_lengths_GS(uint3 id) => pathLengths[id.x] = 0;
  public override void calc_path_lengths_GS(uint3 id)
  {
    uint starI = id.x, pathI = id.y, starJ = (starI + 1) % starN;
    uint aI = starPath(pathI - 1, starI), bI = starPath(pathI - 1, starJ);
    float3 a = cities[aI], b = cities[bI];
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
  public override void calc_paths_GS(uint3 id)
  {
    uint pathI = id.y, starI = id.x, starJ = starI;
    int3 seg = segments[pathI];
    uint a = (uint)seg.x, b = (uint)seg.y, c = (uint)seg.z;
    bool isReverse = seg.z == -1;
    if (isReverse) starJ = IsInPath(starI, a, b) ? (b - (starI - a) + starN) % starN : starI;
    else
    {
      if (IsInPath(starI, a, b)) starJ += c - a; //transfer path
      else
      {
        uint d = c + b - a;
        starJ -= starJ > b % starN ? b >= starN ? ((b + 1) % starN) : b - a + 1 : 0; //remove path segment
        starJ += starJ >= c % starN ? b - a + 1 : d % starN <= b - a ? (d + 1) % starN : 0; //insert path segment
      }
    }
    starPath(pathI, starJ, bestPath(starI));
  }
  public override void init_cities_GS(uint3 id) { uint i = id.x, j = i * 3; cities[i] = float3(Rand_Float(j, -1, 1), Rand_Float(j + 1, -1, 1), Rand_Float(j + 2, -1, 1)); }
  public override v2f vert_Draw_Cities_Border(uint i, uint j, v2f o)
  {
    float r = 0.01f;
    if (i < 12) return vert_BDraw_BoxFrame(f___, f111, r, BLACK, i, j, o);
    else return vert_BDraw_Quad(f0__, f0_1, f011, f01_, float4(0, 0, 1, 0.25f), i, j, o);
  }
  public override v2f vert_Draw_Cities(uint i, uint j, v2f o) => vert_BDraw_Sphere(cities[i], 0.02f, YELLOW, i, j, o);
  public override v2f vert_Draw_Star_Path(uint i, uint j, v2f o)
  {
    float3 p0 = cities[bestPath(i)], p1 = cities[bestPath((i + 1) % starN)];
    return vert_BDraw_Line(p0, p1, 0.01f, palette(lerp1(-1, 1, p0.x * p1.x < 0 ? starBorderReward : 0)), i, j, o);
  }
}