using GpuScript;
using System;
using System.Collections;
using System.Linq;

public class gsAppendBuff : gsAppendBuff_
{
  #region Exclude

  public IEnumerator Test_Coroutine()
  {
    Run(100);
    print($"OnBitN = {IndexN}, {Indexes.Linq().Select(a => a.ToString()).Join(" ")}");
    Quit();
    yield break;
  }

  public override void Start1_GS() { base.Start1_GS(); StartCoroutine(Test_Coroutine()); }
  #endregion Exclude

  //https://forum.unity.com/threads/parallel-prefix-sum-computeshader.518397/

  public void print_Bits(string title) { StrBldr sb = StrBldr($"{title}, Bits"); for (uint i = 0; i < BitN; i++) sb.Add(" ", Bits[i]); print(sb); }
  public void print_Sums(string title) { StrBldr sb = StrBldr($"{title}, Sums"); for (uint i = 0; i < BitN; i++) sb.Add(" ", Sums[i]); print(sb); }
  public void print_Indexes(string title) { StrBldr sb = StrBldr($"{title}: Indexes"); for (uint i = 0; i < IndexN; i++) sb.Add(" ", Indexes[i]); print(sb); }

  public virtual bool IsBitOn(uint i) => i % 32 == 0;
  public uint CeilU(uint n, uint m) => n / m + Is(n % m > 0);

  public void SetN(uint n)
  {
    if (n > 2147450880) throw new System.Exception("gsAppendBuff: N > 2,147,450,880");
    N = n; BitN = CeilU(N, 32); BitN1 = CeilU(BitN, numthreads1); BitN2 = CeilU(BitN1, numthreads1);
  }

  public virtual uint Run(uint n)
  {
    SetN(n); AddComputeBuffer(ref Bits, nameof(Bits), BitN); AddComputeBuffer(ref Fills1, nameof(Fills1), BitN1); AddComputeBuffer(ref Fills2, nameof(Fills2), BitN2); AddComputeBuffer(ref Sums, nameof(Sums), BitN);
    Gpu_GetSums(); Gpu_GetFills1(); Gpu_GetFills2(); Gpu_IncFills1(); Gpu_IncSums();
    AddComputeBuffer(ref Indexes, nameof(Indexes), IndexN); Gpu_GetIndexes(); return IndexN;
  }
  public uint Run(int n) => Run((uint)n);
  public uint Run(uint2 n) => Run(cproduct(n)); public uint Run(uint3 n) => Run(cproduct(n));
  public uint Run(int2 n) => Run(cproduct(n)); public uint Run(int3 n) => Run(cproduct(n));

  public virtual void Run_On_Existing_Bits(uint n, Action a)
  {
    SetN(n);
    AddComputeBuffer(ref Bits, nameof(Bits), BitN); AddComputeBuffer(ref Fills1, nameof(Fills1), BitN1); AddComputeBuffer(ref Fills2, nameof(Fills2), BitN2); AddComputeBuffer(ref Sums, nameof(Sums), BitN);
    a();
    Gpu_Get_Existing_Sums();
    Gpu_GetFills1(); Gpu_GetFills2(); Gpu_IncFills1(); Gpu_IncSums();
    AddComputeBuffer(ref Indexes, nameof(Indexes), IndexN);
    Gpu_GetIndexes();
  }

  public virtual void Init_Bits(uint n, GS parent, string _N, string _BitN, ref RWStructuredBuffer<uint> _Bits)
  {
    SetN(n);
    AddComputeBuffer(ref Bits, nameof(Bits), BitN); AddComputeBuffer(ref Fills1, nameof(Fills1), BitN1); AddComputeBuffer(ref Fills2, nameof(Fills2), BitN2); AddComputeBuffer(ref Sums, nameof(Sums), BitN);
    parent.SetValue(_N, N);
    parent.SetValue(_BitN, BitN);
  }

  public virtual void Prefix_Sums() { Gpu_Get_Bits_Sums(); Gpu_GetFills1(); Gpu_GetFills2(); Gpu_IncFills1(); Gpu_IncSums(); }
  public virtual void Bit_Indexes(RWStructuredBuffer<uint> bits, GS _this, string _IndexN, ref RWStructuredBuffer<uint> _Indexes)
  {
    Prefix_Sums();
    AddComputeBuffer(ref Indexes, nameof(Indexes), IndexN);
    Gpu_GetIndexes();
    _this.SetValue(_IndexN, IndexN);
  }


  public uint Assign_Bits(uint i, uint j, uint bits) => bits | (Is(i < N && IsBitOn(i)) << (int)j);
  public override void Get_Bits_GS(uint3 id)
  {
    uint i = id.x, j, k, bits = 0;
    if (i < BitN) { for (j = 0, k = i * 32; j < 32; j++) bits = Assign_Bits(k + j, j, bits); Bits[i] = bits; }
  }
  public override void Get_Existing_Bits_GS(uint3 id)
  {
    uint i = id.x, j, k, bits = Bits[i];
    if (i < BitN) { for (j = 0, k = i * 32; j < 32; j++) bits = Assign_Bits(k + j, j, bits); Bits[i] = bits; }
  }

  public override IEnumerator GetSums_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI)
  {
    uint i = id.x, c, s, j, k, bits = 0;
    if (i < BitN) { for (j = 0, k = i * 32; j < 32; j++) bits = Assign_Bits(k + j, j, bits); Bits[i] = bits; c = countbits(bits); } else c = 0;
    grp0[grpI] = c; grp[grpI] = c; yield return GroupMemoryBarrierWithGroupSync();
    for (s = 1; s < numthreads1; s *= 2)
    {
      if (grpI >= s && i < BitN) grp[grpI] = grp0[grpI] + grp0[grpI - s]; yield return GroupMemoryBarrierWithGroupSync();
      grp0[grpI] = grp[grpI]; yield return GroupMemoryBarrierWithGroupSync();
    }
    if (i < BitN) Sums[i] = grp[grpI];
  }
  public override IEnumerator Get_Existing_Sums_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI)
  {
    uint i = id.x;
    if (i < BitN)
    {
      uint s, j, k, bits = Bits[i], c = countbits(bits);
      grp0[grpI] = c; grp[grpI] = c; yield return GroupMemoryBarrierWithGroupSync();
      for (s = 1; s < numthreads1; s *= 2)
      {
        if (grpI >= s && i < BitN) grp[grpI] = grp0[grpI] + grp0[grpI - s]; yield return GroupMemoryBarrierWithGroupSync();
        grp0[grpI] = grp[grpI]; yield return GroupMemoryBarrierWithGroupSync();
      }
      if (i < BitN) Sums[i] = grp[grpI];
    }
  }
  public override IEnumerator Get_Bits_Sums_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI)
  {
    uint i = id.x, c = i < BitN ? countbits(Bits[i]) : 0, s;
    grp0[grpI] = c; grp[grpI] = c; yield return GroupMemoryBarrierWithGroupSync();
    for (s = 1; s < numthreads1; s *= 2)
    {
      if (grpI >= s && i < BitN) grp[grpI] = grp0[grpI] + grp0[grpI - s]; yield return GroupMemoryBarrierWithGroupSync();
      grp0[grpI] = grp[grpI]; yield return GroupMemoryBarrierWithGroupSync();
    }
    if (i < BitN) Sums[i] = grp[grpI];
  }
  public override IEnumerator GetFills1_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI)
  {
    uint i = id.x, k = i + 1, j = k * numthreads1 - 1, c = i < BitN1 - 1 ? Sums[j] : i < BitN1 ? Sums[BitN - 1] : 0, s;
    grp0[grpI] = c; grp[grpI] = c; yield return GroupMemoryBarrierWithGroupSync();
    for (s = 1; s < numthreads1; s *= 2)
    {
      if (grpI >= s && i < BitN1) grp[grpI] = grp0[grpI] + grp0[grpI - s]; yield return GroupMemoryBarrierWithGroupSync();
      grp0[grpI] = grp[grpI]; yield return GroupMemoryBarrierWithGroupSync();
    }
    if (i < BitN1) Fills1[i] = grp[grpI];
  }

  public override IEnumerator GetFills2_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI)
  {
    uint i = id.x, k = i + 1, j = k * numthreads1 - 1, c = i < BitN2 - 1 ? Fills1[j] : i < BitN2 ? Fills1[BitN1 - 1] : 0, s;
    grp0[grpI] = c; grp[grpI] = c; yield return GroupMemoryBarrierWithGroupSync();
    for (s = 1; s < numthreads1; s *= 2)
    {
      if (grpI >= s && i < BitN2) grp[grpI] = grp0[grpI] + grp0[grpI - s]; yield return GroupMemoryBarrierWithGroupSync();
      grp0[grpI] = grp[grpI]; yield return GroupMemoryBarrierWithGroupSync();
    }
    if (i < BitN2) Fills2[i] = grp[grpI];
  }
  public override void IncFills1_GS(uint3 id) { uint i = id.x; if (i >= numthreads1) Fills1[i] += Fills2[i / numthreads1 - 1]; }
  public override void IncSums_GS(uint3 id) { uint i = id.x; if (i >= numthreads1) Sums[i] += Fills1[i / numthreads1 - 1]; if (i == BitN - 1) IndexN = Sums[i]; }
  public override void GetIndexes_GS(uint3 id)
  {
    uint i = id.x, j, sum = i == 0 ? 0 : Sums[i - 1], b, i32 = i << 5, k;
    for (k = 0, b = Bits[i]; b > 0; k++) { j = (uint)findLSB(b); Indexes[sum + k] = i32 + j; b = SetBitu(b, j, 0); }
  }
}