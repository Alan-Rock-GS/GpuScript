using GpuScript;
public class gsRand_GS : _GS
{
  uint N, I, J;
  uint4 seed4;
  uint4[] rs { set => Size(N); }
  void initSeed() { Size(N); }
  void initState() { Size(I); }

  [GS_UI, AttGS(GS_Buffer.GroupShared)] uint4[] grp { set => Size(1024); }
  void grp_init_1M() { Size(N / 1024 / 1024); Sync(); }
  void grp_init_1K() { Size(N / 1024); Sync(); }
  void grp_fill_1K() { Size(N); Sync(); }
}