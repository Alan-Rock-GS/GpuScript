using GpuScript;
using System.Collections;
using System.Linq;
using UnityEngine;

public class gsRand_Tutorial : gsRand_Tutorial_
{
  public void Init_Random_Numbers(uint _n, uint seed = 0)
  {
    if (seed > 0) Random.InitState((int)seed);
    seed4 = Random_u4();
    Allocate_randomNumbers(randomNumberN = _n);
    Gpu_initSeed();
    for (I = 1; I < randomNumberN; I *= 2) for (J = 0; J < 4; J++) Gpu_initState();
  }
  public override void initSeed_GS(uint3 id) { uint i = id.x; randomNumbers[i] = i == 0 ? seed4 : u0000; }
  public override void initState_GS(uint3 id)
  {
    uint i = id.x + I;
    if (i < randomNumberN) randomNumbers[i] = index(randomNumbers[i], J, UInt(id.x));
  }
  public uint TausStep(uint z, int S1, int S2, int S3, uint M) => ((z & M) << S3) ^ (((z << S1) ^ z) >> S2);
  public uint4 UInt4(uint4 r) => uint4(TausStep(r.x, 13, 19, 12, 4294967294u), TausStep(r.y, 2, 25, 4, 4294967288u), TausStep(r.z, 3, 11, 17, 4294967280u), r.w * 1664525 + 1013904223u);
  public uint UInt(uint i) => cxor(randomNumbers[i] = UInt4(randomNumbers[i]));
  public uint UInt(uint i, uint a, uint b) => (uint)min(b - 1, lerp(a, b, Float(i)));
  public float Float(uint i) => 2.3283064365387e-10f * UInt(i);
  public float Float(uint i, float a, float b) => lerp(a, b, Float(i));
  public uint rUInt(uint i) => cxor(UInt4(randomNumbers[i]));
  public float rFloat(uint i) => 2.3283064365387e-10f * rUInt(i);
  public float rFloat(uint i, float a, float b) => lerp(a, b, rFloat(i));
  public uint rand_uint => (uint)(Random.value * uint_max);
  public override void Init_randomNumbers() => Init_Random_Numbers(randomNumberN, 7);
  public float avg() => ints[0] / 1e6f / randomNumberN;
  public override void Avg() { Allocate_ints(1); ints[0] = 0; Gpu_Calc_Average(); float a = avg(); Average = abs(Average) > abs(a) ? a : Average; }
  public override void Calc_Random_Numbers_GS(uint3 id) => Float(id.x);
  public override void Calc_Average_GS(uint3 id) => InterlockedAdd(ints, 0, roundi(Float(id.x, -1, 1) * 1e6f));
  public override void LateUpdate1_GS()
  {
    base.LateUpdate1_GS();
    if (isDrawSignal) { Runtime = min(Runtime, (0, 10).For().Min(t => Secs(() => Gpu_Calc_Random_Numbers())) / (float)randomNumberN); Avg(); }
  }
  public float signal_panel_width() => 0.1f;
  public override uint BDraw_SampleN() => randomNumberN;
  public override float4 BDraw_SignalColor() => GREEN;
  public override float4 BDraw_SignalBackColor() => float4(1, 1, 1, 0.2f);
  public override float BDraw_SampleV(uint i) => rFloat(i, -1, 1);
  public override float BDraw_SignalThickness() => lineThickness;
  public override v2f vert_Draw_Random_Signal(uint i, uint j, v2f o) => vert_BDraw_Signal(f_00, f100, signal_panel_width(), i, j, o);
  public override v2f vert_Draw_Avg(uint i, uint j, v2f o) { float3 p = -signal_panel_width() * f001; return vert_BDraw_Line(p - f100, p + f100, lineThickness * 4, BLUE, i, j, o); }
  public override v2f vert_Draw_Calc_Avg(uint i, uint j, v2f o) { float3 p = signal_panel_width() * float3(0, avg(), -2); return vert_BDraw_Line(p - f100, p + f100, lineThickness * 2, RED, i, j, o); }
  public override void OnValueChanged_GS()
  {
    base.OnValueChanged_GS();
    if (UI_group_Signal.Changed && group_Signal) { group_Anneal = false; isDrawSignal = true; isDrawAnneal = false; }
    else if (UI_group_Anneal.Changed && group_Anneal) { group_Signal = false; isDrawAnneal = true; isDrawSignal = false; }
  }

  public override void onLoaded() { base.onLoaded(); StartCoroutine(RunAnneal_Sync()); }
  bool in_RunAnneal_Sync = false;
  public override IEnumerator RunAnneal_Sync()
  {
    if (in_RunAnneal_Sync) yield break;
    in_RunAnneal_Sync = true;
    cityPathN = 25 * cityN;
    InitBuffers();
    Init_Random_Numbers(cityN * cityPathN * 2);
    Gpu_init_cities(); Gpu_init_paths();
    for (uint i = 0; i < cityN * 10; i++)
    {
      Gpu_calc_segments(); Gpu_calc_paths(); Gpu_init_path_lengths(); Gpu_calc_path_lengths();
      Gpu_init_min_path_length(); Gpu_find_min_path_length(); Gpu_find_min_path_lengthI(); Gpu_save_best_path();
      if (i % 500 == 0) yield return null;
    }
    in_RunAnneal_Sync = false;
  }
  public override void save_best_path_GS(uint3 id) { uint cityI = id.x; bestPath(cityI, cityPath(min_pathLength_i[1] - 1, cityI)); }
  public override void init_min_path_length_GS(uint3 id) => min_pathLength_i[id.x] = uint_max;
  public override void find_min_path_length_GS(uint3 id) => InterlockedMin(min_pathLength_i, 0, pathLengths[id.x]);
  public override void find_min_path_lengthI_GS(uint3 id) { uint i = id.x; if (pathLengths[i] == min_pathLength_i[0]) min_pathLength_i[1] = i; }
  public float u2f(uint u) => u * 1e6f;
  public uint f2u(float f) => roundu(f * u2f(1));
  public override void init_path_lengths_GS(uint3 id) => pathLengths[id.x] = 0;
  public override void calc_path_lengths_GS(uint3 id)
  {
    uint cityI = id.x, pathI = id.y, cityJ = (cityI + 1) % cityN;
    uint aI = cityPath(pathI - 1, cityI), bI = cityPath(pathI - 1, cityJ);
    float3 a = cities[aI], b = cities[bI];
    InterlockedAdd(pathLengths, pathI, f2u(max(0, distance(a, b) - cityBorderReward * Is(a.x * b.x < 0))));
  }
  public override void calc_segments_GS(uint3 id)
  {
    uint i = id.x, j = i * 4, a = UInt(j + 1, 0, cityN), b = a + 1 + UInt(j + 2, 0, cityN - 1);
    bool isReverse = Float(j) < 0.5f;
    int c = isReverse ? -1 : (int)(b + 1 + UInt(j + 3, 0, cityN - (b - a)));
    segments[i] = int3(a, b, c);
  }
  public uint cityPath(uint pathI, uint cityI) => cityPaths[(pathI + 1) * cityN + (cityI % cityN)];
  public void cityPath(uint pathI, uint cityI, uint v) => cityPaths[(pathI + 1) * cityN + (cityI % cityN)] = v;
  public uint bestPath(uint cityI) => cityPaths[cityI % cityN];
  public void bestPath(uint cityI, uint v) => cityPaths[cityI % cityN] = v;
  public bool IsInPath(uint i, uint a, uint b) { a %= cityN; b %= cityN; if (i < a) i += cityN; if (b < a) b += cityN; return i >= a && i <= b; }
  public override void init_paths_GS(uint3 id) { uint pI = id.y, cI = id.x; if (pI == 0) bestPath(cI, cI); else cityPath(pI, cI, uint_max); }
  public override void calc_paths_GS(uint3 id)
  {
    uint pathI = id.y, cityI = id.x, cityJ = cityI;
    int3 seg = segments[pathI];
    uint a = (uint)seg.x, b = (uint)seg.y, c = (uint)seg.z;
    bool isReverse = seg.z == -1;
    if (isReverse) cityJ = IsInPath(cityI, a, b) ? (b - (cityI - a) + cityN) % cityN : cityI;
    else
    {
      if (IsInPath(cityI, a, b)) cityJ += c - a; //transfer path
      else
      {
        uint d = c + b - a;
        cityJ -= cityJ > b % cityN ? b >= cityN ? ((b + 1) % cityN) : b - a + 1 : 0; //remove path segment
        cityJ += cityJ >= c % cityN ? b - a + 1 : d % cityN <= b - a ? (d + 1) % cityN : 0; //insert path segment
      }
    }
    cityPath(pathI, cityJ, bestPath(cityI));
  }
  public override void init_cities_GS(uint3 id) { uint i = id.x, j = i * 2; cities[i] = float3(Float(j, -1, 1), Float(j + 1, -1, 1), 0); }
  public override v2f vert_Draw_Cities_Border(uint i, uint j, v2f o)
  {
    float r = 0.01f;
    if (i == 0) return vert_BDraw_Line(f__0, f1_0, r, BLACK, i, j, o);
    else if (i == 1) return vert_BDraw_Line(f1_0, f110, r, BLACK, i, j, o);
    else if (i == 2) return vert_BDraw_Line(f110, f_10, r, BLACK, i, j, o);
    else if (i == 3) return vert_BDraw_Line(f_10, f__0, r, BLACK, i, j, o);
    return vert_BDraw_Line(f0_0, f010, r, BLUE, i, j, o);
  }
  public override v2f vert_Draw_Cities(uint i, uint j, v2f o) => vert_BDraw_Sphere(cities[i], 0.02f, YELLOW, i, j, o);
  public override v2f vert_Draw_City_Path(uint i, uint j, v2f o)
  {
    float3 p0 = cities[bestPath(i)], p1 = cities[bestPath((i + 1) % cityN)];
    return vert_BDraw_Line(p0, p1, 0.01f, p0.x * p1.x < 0 ? RED : GREEN, i, j, o);
  }
}
