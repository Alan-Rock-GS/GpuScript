using GpuScript;
using System.Collections;
using System.Linq;

public class gsSort : gsSort_
{
  public override void InitBuffers0_GS() { segN = min(segN, 4194304 / sortN); vsN = sortN * segN; base.InitBuffers0_GS(); }
  public override void InitBuffers1_GS() { base.InitBuffers1_GS(); Rand_Init(vsN, 17); Gpu_init_vs(); }
  public override void RunSort() { InitBuffers(); Gpu_init_counts(); sort_runtime = Secs(() => Gpu_add_counts()); Gpu_set_sorts(); }
  public override IEnumerator RunBenchmark_Sync()
  {
    StrBldr s = new("Results");
    int i = 0, n = 0;
    (4, 2048, 2).ForEachProduct(_sortN => (1, _sortN == 2048 ? 4096 : 16384, 2).ForEachProduct(_segN => { if (_sortN == 4) s.Add($"\tSegN_{_segN}"); n++; }));
    for (uint _sortN = 4; _sortN <= 2048; _sortN *= 2)
    {
      sortN = (uint)_sortN;
      s.Add($"\n{sortN}");
      for (uint _segN = 1; _segN <= (sortN == 2048 ? 4096u : 16384u); _segN *= 2)
      {
        segN = _segN;
        InitBuffers();
        sort_runtime = (0, _sortN < 1024 && segN <= 512 ? 3 : 1).For().Select(timeI => { Gpu_init_counts(); return Secs(() => Gpu_add_counts()); }).Min();
        Gpu_set_sorts();
        float sec_per_array = sort_runtime / segN, nano_sec_per_array = sec_per_array * 1e9f, ops = sortN * (sortN - 1) / 2, tFlops = ops / sec_per_array / 1e12f;
        s.Add("\t", benchmarkType == BenchmarkType.Nanosec ? nano_sec_per_array : tFlops);
        yield return Status(i++, n, $"vs[{segN}, {sortN}]");
      }
    }
    print(Clipboard = s);
    yield return Status();
  }
  public override void init_vs_GS(uint3 id) => vs[id.x] = Rand_Float(id.x);
  public override void init_counts_GS(uint3 id) => counts[id.x] = 0;
  public override void add_counts_GS(uint3 id)
  {
    uint segI = id.x, k = id.y, j = segI * sortN;
    uint2 u = upperTriangularIndex(k, sortN) + u11 * j;
    InterlockedAdd(counts, vs[u.x] > vs[u.y] ? u.x : u.y, 1);
  }
  public override void set_sorts_GS(uint3 id) { uint segI = id.x, sortI = id.y, j = segI * sortN, i = j + sortI; sorts[j + counts[i]] = sortI; }
  public float get_r() => node_size / 1000;
  public float3 get_p0(uint i) => float3(2 * (i % sortN) / (sortN - 1.0f) - 1, 0.2f, i / sortN * get_r() * 4);
  public float3 get_p1(uint i) => float3(2 * counts[i] / (sortN - 1.0f) - 1, -0.2f, i / sortN * get_r() * 4);
  public override v2f vert_vs0(uint i, uint j, v2f o) => vert_BDraw_Sphere(get_p0(i), get_r(), palette(vs[i]), i, j, o);
  public override v2f vert_vs1(uint i, uint j, v2f o) => vert_BDraw_Sphere(get_p1(i), get_r(), palette(vs[i]), i, j, o);
  public override v2f vert_vs_arrows(uint i, uint j, v2f o) { float r = get_r(); return vert_BDraw_Arrow(get_p0(i) - r * f010, get_p1(i) + r * f010, r / 4, palette(vs[i]), i, j, o); }
}