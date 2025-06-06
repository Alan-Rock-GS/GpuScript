using GpuScript;

public class gsAppendBuff_Doc_GS : _GS
{
  [GS_UI, AttGS("UI|User Interface")] TreeGroup group_UI;
  [GS_UI, AttGS("Primes|AppendBuff library test")] TreeGroup group_AppendBuff;
  [GS_UI, AttGS("Max N|Maximum number to test for prime", UI.ValRange, 1_000, 1, 2_097_152, UI.Pow2_Slider, UI.IsPow2, UI.Format, "#,##0")] uint maxPrimeN;
  [GS_UI, AttGS("Calc Primes|Calculate prime numbers")] void CalcPrimes() { }
  [GS_UI, AttGS("Primes|AppendBuff library test")] TreeGroupEnd groupEnd_AppendBuff;

  [GS_UI, AttGS("AppendBuff|A_AppendBuff Test")] TreeGroup group_AppendBuffTest;
  [GS_UI, AttGS("N|Number of bits", UI.ValRange, 1, 1, 134_217_000, UI.Format, "#,##0", UI.Nearest, 1_000_000)] uint AppendBuffTest_N;
  [GS_UI, AttGS("Runtime N|Number of times to run for benchmark test", UI.ValRange, 1, 1, 1000)] uint AppendBuffTest_Runtime_N;
  enum ProcessorType { CPU, GPU }
  //[GS_UI, AttGS("Processor|Run on CPU or GPU", UI.OnValueChanged, "runOnGpu = processorType == ProcessorType.GPU; UI_runOnGpu.Changed = false;")] ProcessorType processorType;
  //[GS_UI, AttGS("Gpu|Run on CPU or GPU", UI.OnValueChanged, "processorType = runOnGpu ? ProcessorType.GPU : ProcessorType.CPU; UI_processorType.Changed = false;")] bool runOnGpu;
  [GS_UI, AttGS("Processor|Run on CPU or GPU")] ProcessorType processorType;
  [GS_UI, AttGS("Gpu|Run on CPU or GPU")] bool runOnGpu;
  [GS_UI, AttGS("Run|Run A_AppendBuff")] void Run_Append_Buffer() { }
  [GS_UI, AttGS("IndexN|Should be N / 32", UI.ReadOnly, UI.Format, "#,##0")] uint AppendBuffTest_IndexN;
  [GS_UI, AttGS("RunTime|Tuntime in us", UI.ReadOnly, UI.Format, "#,##0", Unit.us)] float AppendBuffTest_Time_us;
  [GS_UI, AttGS("AppendBuff|A_AppendBuff Test")] TreeGroupEnd groupEnd_AppendBuffTest;
  [GS_UI, AttGS("UI|AppendBuff library test")] TreeGroupEnd group_UI_End;

  uint primeFactor;
  void init_Primes() { Size(AppendBuff_BitN); }
  uint pN, piN, pjN;
  uint[] primes;
  void calc_primes() { Size(piN / 2, pjN / 2); }

  void AppendBuff_Get_Existing_Bits() { Size(AppendBuff_BitN); }
  void AppendBuff_Get_Existing_Sums() { Size(AppendBuff_BitN); Sync(); }

  [GS_UI] gsAppendBuff AppendBuff;
  #region <AppendBuff>
  uint AppendBuff_IndexN, AppendBuff_BitN, AppendBuff_N;
  uint[] AppendBuff_Bits, AppendBuff_Sums, AppendBuff_Indexes;
  void AppendBuff_Get_Bits() { Size(AppendBuff_BitN); }
  void AppendBuff_Get_Bits_Sums() { Size(AppendBuff_BitN); Sync(); }
  [GS_UI, AttGS(GS_Buffer.GroupShared_Size, 1024)] uint[] AppendBuff_grp, AppendBuff_grp0;
  uint AppendBuff_BitN1, AppendBuff_BitN2;
  uint[] AppendBuff_Fills1, AppendBuff_Fills2;
  void AppendBuff_GetSums() { Size(AppendBuff_BitN); Sync(); }
  void AppendBuff_GetFills1() { Size(AppendBuff_BitN1); Sync(); }
  void AppendBuff_GetFills2() { Size(AppendBuff_BitN2); Sync(); }
  void AppendBuff_IncFills1() { Size(AppendBuff_BitN1); }
  void AppendBuff_IncSums() { Size(AppendBuff_BitN); }
  void AppendBuff_GetIndexes() { Size(AppendBuff_BitN); }

  #endregion <AppendBuff>

}
