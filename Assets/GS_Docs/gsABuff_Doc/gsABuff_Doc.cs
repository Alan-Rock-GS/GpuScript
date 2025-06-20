using GpuScript;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class gsABuff_Doc : gsABuff_Doc_
{
  public virtual void ABuff_Run_On_Existing_Bits(uint n, Action a) { ABuff_SetN(n); a(); Gpu_ABuff_Get_Existing_Sums(); ABuff_FillIndexes(); }
  public override void ABuff_Get_Existing_Bits_GS(uint3 id) { uint i = id.x, j, k, bits = ABuff_Bits[i]; if (i < ABuff_BitN) { for (j = 0, k = i * 32; j < 32; j++) bits = ABuff_Assign_Bits(k + j, j, bits); ABuff_Bits[i] = bits; } }
  public override IEnumerator ABuff_Get_Existing_Sums_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI)
  {
    uint i = id.x;
    if (i < ABuff_BitN)
    {
      uint s, bits = ABuff_Bits[i], c = countbits(bits);
      ABuff_grp0[grpI] = c; ABuff_grp[grpI] = c; yield return GroupMemoryBarrierWithGroupSync();
      for (s = 1; s < numthreads1; s *= 2)
      {
        if (grpI >= s && i < ABuff_BitN) ABuff_grp[grpI] = ABuff_grp0[grpI] + ABuff_grp0[grpI - s]; yield return GroupMemoryBarrierWithGroupSync();
        ABuff_grp0[grpI] = ABuff_grp[grpI]; yield return GroupMemoryBarrierWithGroupSync();
      }
      if (i < ABuff_BitN) ABuff_Sums[i] = ABuff_grp[grpI];
    }
  }

  public void ABuff_getIndexes_CPU() { AllocData_ABuff_Indexes(ABuff_IndexN); Cpu_ABuff_GetIndexes(); }
  public void ABuff_FillPrefixes_CPU() { Cpu_ABuff_GetFills1(); Cpu_ABuff_GetFills2(); Cpu_ABuff_IncFills1(); Cpu_ABuff_IncSums(); }
  public void ABuff_FillIndexes_CPU() { ABuff_FillPrefixes_CPU(); ABuff_getIndexes_CPU(); }

  public uint ABuff_Run_CPU(uint n) { ABuff_SetN(n); Cpu_ABuff_GetSums(); ABuff_FillIndexes(); return ABuff_IndexN; }

  public override void Run_Append_Buffer()
  {
    ABuff_Run(ABuffTest_N);
    ABuffTest_IndexN = ABuff_IndexN;
    ABuffTest_Time_us = runOnGpu ? TimeAction(ABuffTest_Runtime_N, () => ABuff_Run(ABuffTest_N), Unit.s) : TimeAction(ABuffTest_Runtime_N, () => ABuff_Run_CPU(ABuffTest_N), Unit.s);
  }
  public override void OnValueChanged_GS()
  {
    base.OnValueChanged_GS();
    if (UI_runOnGpu.Changed) { processorType = runOnGpu ? ProcessorType.GPU : ProcessorType.CPU; UI_processorType.Changed = false; }
    if (UI_processorType.Changed) { runOnGpu = processorType == ProcessorType.GPU; UI_runOnGpu.Changed = false; }
  }
  public override void CalcPrimes()
  {
    piN = maxPrimeN;
    pjN = flooru(sqrt(piN));
    uint n = piN / 2;
    ABuff_SetN((n % 32) == 0 ? n : n + 32);
    Gpu_init_Primes(); Gpu_calc_primes();
    Gpu_ABuff_Get_Existing_Sums(); ABuff_FillIndexes(); Gpu_ABuff_Get_Existing_Bits();
    print($"There are {ABuff_IndexN:#,###} prime numbers < {piN:#,###}");
    print(string.Join("\t", ABuff_Indexes.Linq().Select(a => max(2, a * 2 + 1)).Take(25)));
    float gpu_primes_runtime_us = TimeAction(1000, () => Gpu_calc_primes(), Unit.us);
    float gpu_list_runtime_us = TimeAction(100, () => { Gpu_ABuff_Get_Existing_Sums(); ABuff_FillIndexes(); Gpu_ABuff_Get_Existing_Bits(); }, Unit.us);
    Calc_Cpu_Primes();
    print($"Gpu Prime Runtime = {gpu_primes_runtime_us} μs, Gpu List Runtime = {gpu_list_runtime_us} μs");
    print($"Gpu Prime Speedup = {cpu_calc_runtime / gpu_primes_runtime_us:#,###} X, Gpu List Speedup = {cpu_array_runtime / (gpu_primes_runtime_us + gpu_list_runtime_us):#,###} X");
  }
  public override void init_Primes_GS(uint3 id) => ABuff_Bits[id.x] = uint_max;
  public override void calc_primes_GS(uint3 id)
  {
    uint i = id.x, j = id.y + 2, i2 = i * 2 + 1, j2 = id.y * 2 + 3;
    if (i2 > j2 && (i2 % j2) == 0) InterlockedAnd(ABuff_Bits, i / 32, ~(1u << (int)(i % 32)));
  }

  public float cpu_calc_runtime = 0, cpu_array_runtime = 0;
  public void Calc_Cpu_Primes()
  {
    cpu_calc_runtime = 0;
    BitArray cpu_primes = null;
    var a = new List<int>();
    cpu_array_runtime = TimeAction(1, () => { cpu_calc_runtime = TimeAction(1, () => { cpu_primes = cpu_computePrimes(); }, Unit.us); for (int i = 0; i < cpu_primes.Length; i++) if (cpu_primes[i]) a.Add(i); }, Unit.us);
    print($"CPU: There are {a.Count:#,###} prime numbers < {maxPrimeN:#,###}, calc = {cpu_calc_runtime:#,###} μs, convert to array = {cpu_array_runtime:#,###} μs");
  }
  public BitArray cpu_computePrimes()
  {
    BitArray primes = new((int)maxPrimeN);
    primes.SetAll(true);
    primes[0] = primes[1] = false;
    for (int i = 0, i2 = floori(sqrt(maxPrimeN)); i < i2; i++) if (primes[i]) for (int j = i * i; j < maxPrimeN; j += i) primes[j] = false;
    return primes;
  }

  public override void Run_ABuff()
  {
    Run(ABuffTest_N);
    IndexN = ABuff_IndexN;
    ABuffTest_Time_us = runOnGpu ? TimeAction(ABuffTest_Runtime_N, () => Run(ABuffTest_N), Unit.s) : TimeAction(ABuffTest_Runtime_N, () => ABuff_Run_CPU(ABuffTest_N), Unit.s);
  }

  public virtual bool IsBitOn(uint i) => i % 32 == 0;
  public void SetN(uint n)
  {
    if (n > 134_217_700) throw new Exception("gsAABuff: N > 134_217_700");
    N = n; BitN = ceilu(N, 32); BitRowN = BitColN = ceilu(sqrt(BitN)); AllocData_Bits(BitN); AllocData_Sums(BitN); AllocData_ColN_Sums(BitRowN);
  }
  public virtual uint Run(uint n)
  {
    if (n == 0) return IndexN = 0;
    SetN(n); if (n < 66_000_000) { Gpu_Init_Bits_32(); Gpu_Get_Bits_32(); } else Gpu_Get_Bits();
    Gpu_InitSums(); Gpu_CalcSums(); Gpu_Init_ColN_Sums(); Gpu_Calc_ColN_Sums(); Gpu_Add_ColN_Sums();
    AllocData_Indexes(IndexN = Sums[BitN - 1]); Gpu_CalcIndexes();
    return IndexN;
  }
  public uint Assign_Bit(uint i, uint j) => Is(i < N && IsBitOn(i)) << (int)j;
  public uint Assign_Bits(uint i, uint j, uint bits) => bits | Assign_Bit(i, j);
  public override void Get_Bits_GS(uint3 id) { uint i = id.x, j, k, bits = 0; if (i < BitN) { for (j = 0, k = i * 32; j < 32; j++) bits = Assign_Bits(k + j, j, bits); Bits[i] = bits; } }
  public override void Init_Bits_32_GS(uint3 id) => Bits[id.x] = 0;
  public override void Get_Bits_32_GS(uint3 id) { uint i = id.x, j = id.y, k = i * 32 + j, bits; if (i < BitN && (bits = Assign_Bit(k, j)) != 0) InterlockedOr(Bits, i, bits); }
  public override void InitSums_GS(uint3 id) => Sums[id.x] = countbits(Bits[id.x]);
  public override void CalcSums_GS(uint3 id) { uint i = id.x, k = id.y, j = i * BitColN; uint2 u = upperTriangularIndex(k, BitColN) + u11 * j; if (u.x < BitN && u.y < BitN) InterlockedAdd(Sums, u.y, countbits(Bits[u.x])); }
  public uint SumI(uint rowI, uint colJ) => rowI * BitColN + colJ;
  public uint Sum(uint rowI, uint colJ) => Sums[SumI(rowI, colJ)];
  public void Sum(uint rowI, uint colJ, uint v) => Sums[SumI(rowI, colJ)] = v;
  public override void Init_ColN_Sums_GS(uint3 id) => ColN_Sums[id.x] = Sums[min(SumI(id.x, BitColN - 1), BitN - 1)];
  public override void Calc_ColN_Sums_GS(uint3 id) { uint2 u = upperTriangularIndex(id.x, BitColN); if (u.x < BitRowN) InterlockedAdd(ColN_Sums, u.y, Sums[min(SumI(u.x, BitColN - 1), BitN - 1)]); }
  public override void Add_ColN_Sums_GS(uint3 id) { uint rowI = id.x + 1, i = SumI(rowI, id.y); if (i < BitN) InterlockedAdd(Sums, i, ColN_Sums[rowI - 1]); }
  public override void CalcIndexes_GS(uint3 id) { uint i = id.x, j, sum = i == 0 ? 0 : Sums[i - 1], b, i32 = i << 5, k; for (k = 0, b = Bits[i]; b > 0; k++) { j = (uint)findLSB(b); Indexes[sum + k] = i32 + j; b = SetBitu(b, j, 0); } }
}