using GpuScript;

public class gsABuff_Doc_GS : _GS
{
  [GS_UI, AttGS("UI|User Interface")] TreeGroup group_UI;
  [GS_UI, AttGS("Primes|ABuff library test")] TreeGroup group_ABuff;
  [GS_UI, AttGS("Max N|Maximum number to test for prime", UI.ValRange, 1_000, 1, 2_097_152, UI.Pow2_Slider, UI.IsPow2, UI.Format, "#,##0")] uint maxPrimeN;
  [GS_UI, AttGS("Calc Primes|Calculate prime numbers")] void CalcPrimes() { }
  [GS_UI, AttGS("Primes|ABuff library test")] TreeGroupEnd groupEnd_ABuff;

  [GS_UI, AttGS("ABuff|A_ABuff Test")] TreeGroup group_ABuffTest;
  //[GS_UI, AttGS("N|Number of bits", UI.ValRange, 1, 1, 134_217_000, UI.Format, "#,##0", UI.Nearest, 1_000_000)] uint ABuffTest_N;
  [GS_UI, AttGS("N|Number of bits", UI.ValRange, 1, 1, 134_217_000, UI.Format, "#,##0", UI.NearestDigit, UI.Pow2_Slider)] uint ABuffTest_N;
  //[GS_UI, AttGS("N|Number of bits", UI.ValRange, 1, 1, 134_217_000, UI.Format, "#,##0", UI.IsPow10, UI.Pow2_Slider)] uint ABuffTest_N;
  [GS_UI, AttGS("Runtime N|Number of times to run for benchmark test", UI.ValRange, 1, 1, 1000)] uint ABuffTest_Runtime_N;
  enum ProcessorType { CPU, GPU }
  //[GS_UI, AttGS("Processor|Run on CPU or GPU", UI.OnValueChanged, "runOnGpu = processorType == ProcessorType.GPU; UI_runOnGpu.Changed = false;")] ProcessorType processorType;
  //[GS_UI, AttGS("Gpu|Run on CPU or GPU", UI.OnValueChanged, "processorType = runOnGpu ? ProcessorType.GPU : ProcessorType.CPU; UI_processorType.Changed = false;")] bool runOnGpu;
  [GS_UI, AttGS("Processor|Run on CPU or GPU")] ProcessorType processorType;
  [GS_UI, AttGS("Gpu|Run on CPU or GPU")] bool runOnGpu;
  [GS_UI, AttGS("Run|Run A_ABuff")] void Run_Append_Buffer() { }
  [GS_UI, AttGS("IndexN|Should be N / 32", UI.ReadOnly, UI.Format, "#,##0")] uint ABuffTest_IndexN;
  [GS_UI, AttGS("RunTime|Tuntime in us", UI.ReadOnly, UI.Format, "#,##0", Unit.us)] float ABuffTest_Time_us;
  [GS_UI, AttGS("ABuff|A_ABuff Test")] TreeGroupEnd groupEnd_ABuffTest;
  [GS_UI, AttGS("A Buff|A_ABuff Test")] TreeGroup group_A_BuffTest;

  uint IndexN, BitN, N, BitRowN, BitColN;
  uint[] Bits, Sums, Indexes, ColN_Sums;
  void Init_Bits_32() { Size(BitN); }
  void Get_Bits_32() { Size(BitN, 32); }
  void Get_Bits() { Size(BitN); }
  void InitSums() { Size(BitN); }
  void CalcSums() { Size(BitRowN, BitColN * (BitColN - 1) / 2); }
  void Init_ColN_Sums() { Size(BitRowN); }
  void Calc_ColN_Sums() { Size(BitRowN * (BitRowN - 1) / 2); }
  void Add_ColN_Sums() { Size(BitRowN - 1, BitColN); }
  void CalcIndexes() { Size(BitN); }
  [GS_UI, AttGS("Run ABuff|Run A_ABuff")] void Run_ABuff() { }

  [GS_UI, AttGS("A Buff|A_ABuff Test")] TreeGroupEnd groupEnd_A_BuffTest;

  [GS_UI, AttGS("UI|ABuff library test")] TreeGroupEnd group_UI_End;

  uint primeFactor;
  void init_Primes() { Size(ABuff_BitN); }
  uint pN, piN, pjN;
  uint[] primes;
  void calc_primes() { Size(piN / 2, pjN / 2); }

  void ABuff_Get_Existing_Bits() { Size(ABuff_BitN); }
  void ABuff_Get_Existing_Sums() { Size(ABuff_BitN); Sync(); }

  [GS_UI] gsABuff ABuff;
  #region <ABuff>
  uint ABuff_IndexN, ABuff_BitN, ABuff_N;
  uint[] ABuff_Bits, ABuff_Sums, ABuff_Indexes;
  void ABuff_Get_Bits() { Size(ABuff_BitN); }
  void ABuff_Get_Bits_Sums() { Size(ABuff_BitN); Sync(); }
  [GS_UI, AttGS(GS_Buffer.GroupShared_Size, 1024)] uint[] ABuff_grp, ABuff_grp0;
  uint ABuff_BitN1, ABuff_BitN2;
  uint[] ABuff_Fills1, ABuff_Fills2;
  void ABuff_GetSums() { Size(ABuff_BitN); Sync(); }
  void ABuff_GetFills1() { Size(ABuff_BitN1); Sync(); }
  void ABuff_GetFills2() { Size(ABuff_BitN2); Sync(); }
  void ABuff_IncFills1() { Size(ABuff_BitN1); }
  void ABuff_IncSums() { Size(ABuff_BitN); }
  void ABuff_GetIndexes() { Size(ABuff_BitN); }

  #endregion <ABuff>
}
