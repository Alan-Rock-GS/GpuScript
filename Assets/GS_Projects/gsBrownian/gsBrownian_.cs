using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using GpuScript;
using System.Text;
public class gsBrownian_ : GS, IAxes_Lib
{
  [HideInInspector] public uiData data;
  [HideInInspector] public GBrownian g; public GBrownian G { get { gBrownian.GetData(); return gBrownian[0]; } }
  public void g_SetData() { if (gChanged && gBrownian != null) { gBrownian[0] = g; gBrownian.SetData(); gChanged = false; } }
  public virtual void vs_SetKernels(bool reallocate = false) { if (vs != null && (reallocate || vs.reallocated)) { SetKernelValues(vs, nameof(vs), kernel_CalcGain, kernel_SumPrice, kernel_InitPrice); vs.reallocated = false; } }
  public virtual void vs0_SetKernels(bool reallocate = false) { if (vs0 != null && (reallocate || vs0.reallocated)) { SetKernelValues(vs0, nameof(vs0), kernel_CalcGain, kernel_SumPrice, kernel_InitPrice); vs0.reallocated = false; } }
  public virtual void trends_SetKernels(bool reallocate = false) { if (trends != null && (reallocate || trends.reallocated)) { SetKernelValues(trends, nameof(trends), kernel_InitTrends); trends.reallocated = false; } }
  public virtual void Rand_rs_SetKernels(bool reallocate = false) { if (Rand_rs != null && (reallocate || Rand_rs.reallocated)) { SetKernelValues(Rand_rs, nameof(Rand_rs), kernel_InitPrice, kernel_InitTrends, kernel_Rand_initState, kernel_Rand_initSeed); Rand_rs.reallocated = false; } }
  public virtual void Axes_Lib_BDraw_tab_delimeted_text_SetKernels(bool reallocate = false) { if (Axes_Lib_BDraw_tab_delimeted_text != null && (reallocate || Axes_Lib_BDraw_tab_delimeted_text.reallocated)) { SetKernelValues(Axes_Lib_BDraw_tab_delimeted_text, nameof(Axes_Lib_BDraw_tab_delimeted_text), kernel_Axes_Lib_BDraw_ABuff_GetSums, kernel_Axes_Lib_BDraw_ABuff_Get_Bits, kernel_Axes_Lib_BDraw_getTextInfo); Axes_Lib_BDraw_tab_delimeted_text.reallocated = false; } }
  public virtual void Axes_Lib_BDraw_textInfos_SetKernels(bool reallocate = false) { if (Axes_Lib_BDraw_textInfos != null && (reallocate || Axes_Lib_BDraw_textInfos.reallocated)) { SetKernelValues(Axes_Lib_BDraw_textInfos, nameof(Axes_Lib_BDraw_textInfos), kernel_Axes_Lib_BDraw_setDefaultTextInfo, kernel_Axes_Lib_BDraw_getTextInfo); Axes_Lib_BDraw_textInfos.reallocated = false; } }
  public virtual void Axes_Lib_BDraw_fontInfos_SetKernels(bool reallocate = false) { if (Axes_Lib_BDraw_fontInfos != null && (reallocate || Axes_Lib_BDraw_fontInfos.reallocated)) { SetKernelValues(Axes_Lib_BDraw_fontInfos, nameof(Axes_Lib_BDraw_fontInfos), kernel_Axes_Lib_BDraw_getTextInfo); Axes_Lib_BDraw_fontInfos.reallocated = false; } }
  public virtual void Axes_Lib_BDraw_ABuff_Bits_SetKernels(bool reallocate = false) { if (Axes_Lib_BDraw_ABuff_Bits != null && (reallocate || Axes_Lib_BDraw_ABuff_Bits.reallocated)) { SetKernelValues(Axes_Lib_BDraw_ABuff_Bits, nameof(Axes_Lib_BDraw_ABuff_Bits), kernel_Axes_Lib_BDraw_ABuff_GetIndexes, kernel_Axes_Lib_BDraw_ABuff_Get_Bits_Sums, kernel_Axes_Lib_BDraw_ABuff_GetSums, kernel_Axes_Lib_BDraw_ABuff_Get_Bits); Axes_Lib_BDraw_ABuff_Bits.reallocated = false; } }
  public virtual void Axes_Lib_BDraw_ABuff_Sums_SetKernels(bool reallocate = false) { if (Axes_Lib_BDraw_ABuff_Sums != null && (reallocate || Axes_Lib_BDraw_ABuff_Sums.reallocated)) { SetKernelValues(Axes_Lib_BDraw_ABuff_Sums, nameof(Axes_Lib_BDraw_ABuff_Sums), kernel_Axes_Lib_BDraw_ABuff_GetIndexes, kernel_Axes_Lib_BDraw_ABuff_IncSums, kernel_Axes_Lib_BDraw_ABuff_GetFills1, kernel_Axes_Lib_BDraw_ABuff_Get_Bits_Sums, kernel_Axes_Lib_BDraw_ABuff_GetSums); Axes_Lib_BDraw_ABuff_Sums.reallocated = false; } }
  public virtual void Axes_Lib_BDraw_ABuff_Indexes_SetKernels(bool reallocate = false) { if (Axes_Lib_BDraw_ABuff_Indexes != null && (reallocate || Axes_Lib_BDraw_ABuff_Indexes.reallocated)) { SetKernelValues(Axes_Lib_BDraw_ABuff_Indexes, nameof(Axes_Lib_BDraw_ABuff_Indexes), kernel_Axes_Lib_BDraw_ABuff_GetIndexes, kernel_Axes_Lib_BDraw_getTextInfo); Axes_Lib_BDraw_ABuff_Indexes.reallocated = false; } }
  public virtual void Axes_Lib_BDraw_ABuff_Fills1_SetKernels(bool reallocate = false) { if (Axes_Lib_BDraw_ABuff_Fills1 != null && (reallocate || Axes_Lib_BDraw_ABuff_Fills1.reallocated)) { SetKernelValues(Axes_Lib_BDraw_ABuff_Fills1, nameof(Axes_Lib_BDraw_ABuff_Fills1), kernel_Axes_Lib_BDraw_ABuff_IncSums, kernel_Axes_Lib_BDraw_ABuff_IncFills1, kernel_Axes_Lib_BDraw_ABuff_GetFills2, kernel_Axes_Lib_BDraw_ABuff_GetFills1); Axes_Lib_BDraw_ABuff_Fills1.reallocated = false; } }
  public virtual void Axes_Lib_BDraw_ABuff_Fills2_SetKernels(bool reallocate = false) { if (Axes_Lib_BDraw_ABuff_Fills2 != null && (reallocate || Axes_Lib_BDraw_ABuff_Fills2.reallocated)) { SetKernelValues(Axes_Lib_BDraw_ABuff_Fills2, nameof(Axes_Lib_BDraw_ABuff_Fills2), kernel_Axes_Lib_BDraw_ABuff_IncFills1, kernel_Axes_Lib_BDraw_ABuff_GetFills2); Axes_Lib_BDraw_ABuff_Fills2.reallocated = false; } }
  public virtual void Gpu_CalcGain() { g_SetData(); vs_SetKernels(); vs0?.SetCpu(); vs0_SetKernels(); Gpu(kernel_CalcGain, CalcGain, uint2(tickerN, dayN)); vs?.ResetWrite(); }
  public virtual void Cpu_CalcGain() { vs?.GetGpu(); vs0?.GetGpu(); Cpu(CalcGain, uint2(tickerN, dayN)); vs.SetData(); }
  public virtual void Cpu_CalcGain(uint3 id) { vs?.GetGpu(); vs0?.GetGpu(); CalcGain(id); vs.SetData(); }
  public virtual void Gpu_SumPrice() { g_SetData(); vs?.SetCpu(); vs_SetKernels(); vs0?.SetCpu(); vs0_SetKernels(); Gpu(kernel_SumPrice, SumPrice, uint2(tickerN, dayN * (dayN - 1) / 2)); vs0?.ResetWrite(); }
  public virtual void Cpu_SumPrice() { vs?.GetGpu(); vs0?.GetGpu(); Cpu(SumPrice, uint2(tickerN, dayN * (dayN - 1) / 2)); vs0.SetData(); }
  public virtual void Cpu_SumPrice(uint3 id) { vs?.GetGpu(); vs0?.GetGpu(); SumPrice(id); vs0.SetData(); }
  public virtual void Gpu_InitPrice() { g_SetData(); vs_SetKernels(); Rand_rs?.SetCpu(); Rand_rs_SetKernels(); vs0_SetKernels(); Gpu(kernel_InitPrice, InitPrice, uint2(tickerN, dayN)); vs?.ResetWrite(); Rand_rs?.ResetWrite(); vs0?.ResetWrite(); }
  public virtual void Cpu_InitPrice() { vs?.GetGpu(); Rand_rs?.GetGpu(); vs0?.GetGpu(); Cpu(InitPrice, uint2(tickerN, dayN)); vs.SetData(); Rand_rs.SetData(); vs0.SetData(); }
  public virtual void Cpu_InitPrice(uint3 id) { vs?.GetGpu(); Rand_rs?.GetGpu(); vs0?.GetGpu(); InitPrice(id); vs.SetData(); Rand_rs.SetData(); vs0.SetData(); }
  public virtual void Gpu_InitTrends() { g_SetData(); trends_SetKernels(); Rand_rs?.SetCpu(); Rand_rs_SetKernels(); Gpu(kernel_InitTrends, InitTrends, tickerN); trends?.ResetWrite(); Rand_rs?.ResetWrite(); }
  public virtual void Cpu_InitTrends() { trends?.GetGpu(); Rand_rs?.GetGpu(); Cpu(InitTrends, tickerN); trends.SetData(); Rand_rs.SetData(); }
  public virtual void Cpu_InitTrends(uint3 id) { trends?.GetGpu(); Rand_rs?.GetGpu(); InitTrends(id); trends.SetData(); Rand_rs.SetData(); }
  public virtual void Gpu_Axes_Lib_BDraw_ABuff_GetIndexes() { g_SetData(); Axes_Lib_BDraw_ABuff_Bits?.SetCpu(); Axes_Lib_BDraw_ABuff_Bits_SetKernels(); Axes_Lib_BDraw_ABuff_Sums?.SetCpu(); Axes_Lib_BDraw_ABuff_Sums_SetKernels(); Axes_Lib_BDraw_ABuff_Indexes_SetKernels(); Gpu(kernel_Axes_Lib_BDraw_ABuff_GetIndexes, Axes_Lib_BDraw_ABuff_GetIndexes, Axes_Lib_BDraw_ABuff_BitN); Axes_Lib_BDraw_ABuff_Indexes?.ResetWrite(); }
  public virtual void Cpu_Axes_Lib_BDraw_ABuff_GetIndexes() { Axes_Lib_BDraw_ABuff_Bits?.GetGpu(); Axes_Lib_BDraw_ABuff_Sums?.GetGpu(); Axes_Lib_BDraw_ABuff_Indexes?.GetGpu(); Cpu(Axes_Lib_BDraw_ABuff_GetIndexes, Axes_Lib_BDraw_ABuff_BitN); Axes_Lib_BDraw_ABuff_Indexes.SetData(); }
  public virtual void Cpu_Axes_Lib_BDraw_ABuff_GetIndexes(uint3 id) { Axes_Lib_BDraw_ABuff_Bits?.GetGpu(); Axes_Lib_BDraw_ABuff_Sums?.GetGpu(); Axes_Lib_BDraw_ABuff_Indexes?.GetGpu(); Axes_Lib_BDraw_ABuff_GetIndexes(id); Axes_Lib_BDraw_ABuff_Indexes.SetData(); }
  public virtual void Gpu_Axes_Lib_BDraw_ABuff_IncSums() { g_SetData(); Axes_Lib_BDraw_ABuff_Sums?.SetCpu(); Axes_Lib_BDraw_ABuff_Sums_SetKernels(); Axes_Lib_BDraw_ABuff_Fills1?.SetCpu(); Axes_Lib_BDraw_ABuff_Fills1_SetKernels(); gBrownian?.SetCpu(); Gpu(kernel_Axes_Lib_BDraw_ABuff_IncSums, Axes_Lib_BDraw_ABuff_IncSums, Axes_Lib_BDraw_ABuff_BitN); Axes_Lib_BDraw_ABuff_Sums?.ResetWrite(); g = G; }
  public virtual void Cpu_Axes_Lib_BDraw_ABuff_IncSums() { Axes_Lib_BDraw_ABuff_Sums?.GetGpu(); Axes_Lib_BDraw_ABuff_Fills1?.GetGpu(); Cpu(Axes_Lib_BDraw_ABuff_IncSums, Axes_Lib_BDraw_ABuff_BitN); Axes_Lib_BDraw_ABuff_Sums.SetData(); }
  public virtual void Cpu_Axes_Lib_BDraw_ABuff_IncSums(uint3 id) { Axes_Lib_BDraw_ABuff_Sums?.GetGpu(); Axes_Lib_BDraw_ABuff_Fills1?.GetGpu(); Axes_Lib_BDraw_ABuff_IncSums(id); Axes_Lib_BDraw_ABuff_Sums.SetData(); }
  public virtual void Gpu_Axes_Lib_BDraw_ABuff_IncFills1() { g_SetData(); Axes_Lib_BDraw_ABuff_Fills1?.SetCpu(); Axes_Lib_BDraw_ABuff_Fills1_SetKernels(); Axes_Lib_BDraw_ABuff_Fills2?.SetCpu(); Axes_Lib_BDraw_ABuff_Fills2_SetKernels(); Gpu(kernel_Axes_Lib_BDraw_ABuff_IncFills1, Axes_Lib_BDraw_ABuff_IncFills1, Axes_Lib_BDraw_ABuff_BitN1); Axes_Lib_BDraw_ABuff_Fills1?.ResetWrite(); }
  public virtual void Cpu_Axes_Lib_BDraw_ABuff_IncFills1() { Axes_Lib_BDraw_ABuff_Fills1?.GetGpu(); Axes_Lib_BDraw_ABuff_Fills2?.GetGpu(); Cpu(Axes_Lib_BDraw_ABuff_IncFills1, Axes_Lib_BDraw_ABuff_BitN1); Axes_Lib_BDraw_ABuff_Fills1.SetData(); }
  public virtual void Cpu_Axes_Lib_BDraw_ABuff_IncFills1(uint3 id) { Axes_Lib_BDraw_ABuff_Fills1?.GetGpu(); Axes_Lib_BDraw_ABuff_Fills2?.GetGpu(); Axes_Lib_BDraw_ABuff_IncFills1(id); Axes_Lib_BDraw_ABuff_Fills1.SetData(); }
  public virtual void Gpu_Axes_Lib_BDraw_ABuff_GetFills2() { g_SetData(); Axes_Lib_BDraw_ABuff_Fills1?.SetCpu(); Axes_Lib_BDraw_ABuff_Fills1_SetKernels(); Axes_Lib_BDraw_ABuff_Fills2_SetKernels(); Gpu(kernel_Axes_Lib_BDraw_ABuff_GetFills2, Axes_Lib_BDraw_ABuff_GetFills2, Axes_Lib_BDraw_ABuff_BitN2); Axes_Lib_BDraw_ABuff_Fills2?.ResetWrite(); }
  public virtual IEnumerator Cpu_Axes_Lib_BDraw_ABuff_GetFills2() { Axes_Lib_BDraw_ABuff_Fills1?.GetGpu(); Axes_Lib_BDraw_ABuff_Fills2?.GetGpu(); yield return StartCoroutine(Cpu_Coroutine(kernel_Axes_Lib_BDraw_ABuff_GetFills2, Axes_Lib_BDraw_ABuff_GetFills2, Axes_Lib_BDraw_ABuff_BitN2)); }
  public virtual void Cpu_Axes_Lib_BDraw_ABuff_GetFills2(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { Axes_Lib_BDraw_ABuff_Fills1?.GetGpu(); Axes_Lib_BDraw_ABuff_Fills2?.GetGpu(); Axes_Lib_BDraw_ABuff_GetFills2(grp_tid, grp_id, id, grpI); Axes_Lib_BDraw_ABuff_Fills2.SetData(); }
  public virtual void Gpu_Axes_Lib_BDraw_ABuff_GetFills1() { g_SetData(); Axes_Lib_BDraw_ABuff_Sums?.SetCpu(); Axes_Lib_BDraw_ABuff_Sums_SetKernels(); Axes_Lib_BDraw_ABuff_Fills1_SetKernels(); Gpu(kernel_Axes_Lib_BDraw_ABuff_GetFills1, Axes_Lib_BDraw_ABuff_GetFills1, Axes_Lib_BDraw_ABuff_BitN1); Axes_Lib_BDraw_ABuff_Fills1?.ResetWrite(); }
  public virtual IEnumerator Cpu_Axes_Lib_BDraw_ABuff_GetFills1() { Axes_Lib_BDraw_ABuff_Sums?.GetGpu(); Axes_Lib_BDraw_ABuff_Fills1?.GetGpu(); yield return StartCoroutine(Cpu_Coroutine(kernel_Axes_Lib_BDraw_ABuff_GetFills1, Axes_Lib_BDraw_ABuff_GetFills1, Axes_Lib_BDraw_ABuff_BitN1)); }
  public virtual void Cpu_Axes_Lib_BDraw_ABuff_GetFills1(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { Axes_Lib_BDraw_ABuff_Sums?.GetGpu(); Axes_Lib_BDraw_ABuff_Fills1?.GetGpu(); Axes_Lib_BDraw_ABuff_GetFills1(grp_tid, grp_id, id, grpI); Axes_Lib_BDraw_ABuff_Fills1.SetData(); }
  public virtual void Gpu_Axes_Lib_BDraw_ABuff_Get_Bits_Sums() { g_SetData(); Axes_Lib_BDraw_ABuff_Bits?.SetCpu(); Axes_Lib_BDraw_ABuff_Bits_SetKernels(); Axes_Lib_BDraw_ABuff_Sums_SetKernels(); Gpu(kernel_Axes_Lib_BDraw_ABuff_Get_Bits_Sums, Axes_Lib_BDraw_ABuff_Get_Bits_Sums, Axes_Lib_BDraw_ABuff_BitN); Axes_Lib_BDraw_ABuff_Sums?.ResetWrite(); }
  public virtual IEnumerator Cpu_Axes_Lib_BDraw_ABuff_Get_Bits_Sums() { Axes_Lib_BDraw_ABuff_Bits?.GetGpu(); Axes_Lib_BDraw_ABuff_Sums?.GetGpu(); yield return StartCoroutine(Cpu_Coroutine(kernel_Axes_Lib_BDraw_ABuff_Get_Bits_Sums, Axes_Lib_BDraw_ABuff_Get_Bits_Sums, Axes_Lib_BDraw_ABuff_BitN)); }
  public virtual void Cpu_Axes_Lib_BDraw_ABuff_Get_Bits_Sums(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { Axes_Lib_BDraw_ABuff_Bits?.GetGpu(); Axes_Lib_BDraw_ABuff_Sums?.GetGpu(); Axes_Lib_BDraw_ABuff_Get_Bits_Sums(grp_tid, grp_id, id, grpI); Axes_Lib_BDraw_ABuff_Sums.SetData(); }
  public virtual void Gpu_Axes_Lib_BDraw_ABuff_GetSums() { g_SetData(); Axes_Lib_BDraw_ABuff_Bits_SetKernels(); Axes_Lib_BDraw_ABuff_Sums_SetKernels(); Axes_Lib_BDraw_tab_delimeted_text?.SetCpu(); Axes_Lib_BDraw_tab_delimeted_text_SetKernels(); Gpu(kernel_Axes_Lib_BDraw_ABuff_GetSums, Axes_Lib_BDraw_ABuff_GetSums, Axes_Lib_BDraw_ABuff_BitN); Axes_Lib_BDraw_ABuff_Bits?.ResetWrite(); Axes_Lib_BDraw_ABuff_Sums?.ResetWrite(); }
  public virtual IEnumerator Cpu_Axes_Lib_BDraw_ABuff_GetSums() { Axes_Lib_BDraw_ABuff_Bits?.GetGpu(); Axes_Lib_BDraw_ABuff_Sums?.GetGpu(); Axes_Lib_BDraw_tab_delimeted_text?.GetGpu(); yield return StartCoroutine(Cpu_Coroutine(kernel_Axes_Lib_BDraw_ABuff_GetSums, Axes_Lib_BDraw_ABuff_GetSums, Axes_Lib_BDraw_ABuff_BitN)); }
  public virtual void Cpu_Axes_Lib_BDraw_ABuff_GetSums(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { Axes_Lib_BDraw_ABuff_Bits?.GetGpu(); Axes_Lib_BDraw_ABuff_Sums?.GetGpu(); Axes_Lib_BDraw_tab_delimeted_text?.GetGpu(); Axes_Lib_BDraw_ABuff_GetSums(grp_tid, grp_id, id, grpI); Axes_Lib_BDraw_ABuff_Bits.SetData(); Axes_Lib_BDraw_ABuff_Sums.SetData(); }
  public virtual void Gpu_Axes_Lib_BDraw_ABuff_Get_Bits() { g_SetData(); Axes_Lib_BDraw_ABuff_Bits_SetKernels(); Axes_Lib_BDraw_tab_delimeted_text?.SetCpu(); Axes_Lib_BDraw_tab_delimeted_text_SetKernels(); Gpu(kernel_Axes_Lib_BDraw_ABuff_Get_Bits, Axes_Lib_BDraw_ABuff_Get_Bits, Axes_Lib_BDraw_ABuff_BitN); Axes_Lib_BDraw_ABuff_Bits?.ResetWrite(); }
  public virtual void Cpu_Axes_Lib_BDraw_ABuff_Get_Bits() { Axes_Lib_BDraw_ABuff_Bits?.GetGpu(); Axes_Lib_BDraw_tab_delimeted_text?.GetGpu(); Cpu(Axes_Lib_BDraw_ABuff_Get_Bits, Axes_Lib_BDraw_ABuff_BitN); Axes_Lib_BDraw_ABuff_Bits.SetData(); }
  public virtual void Cpu_Axes_Lib_BDraw_ABuff_Get_Bits(uint3 id) { Axes_Lib_BDraw_ABuff_Bits?.GetGpu(); Axes_Lib_BDraw_tab_delimeted_text?.GetGpu(); Axes_Lib_BDraw_ABuff_Get_Bits(id); Axes_Lib_BDraw_ABuff_Bits.SetData(); }
  public virtual void Gpu_Axes_Lib_BDraw_setDefaultTextInfo() { g_SetData(); Axes_Lib_BDraw_textInfos?.SetCpu(); Axes_Lib_BDraw_textInfos_SetKernels(); Gpu(kernel_Axes_Lib_BDraw_setDefaultTextInfo, Axes_Lib_BDraw_setDefaultTextInfo, Axes_Lib_BDraw_textN); Axes_Lib_BDraw_textInfos?.ResetWrite(); }
  public virtual void Cpu_Axes_Lib_BDraw_setDefaultTextInfo() { Axes_Lib_BDraw_textInfos?.GetGpu(); Cpu(Axes_Lib_BDraw_setDefaultTextInfo, Axes_Lib_BDraw_textN); Axes_Lib_BDraw_textInfos.SetData(); }
  public virtual void Cpu_Axes_Lib_BDraw_setDefaultTextInfo(uint3 id) { Axes_Lib_BDraw_textInfos?.GetGpu(); Axes_Lib_BDraw_setDefaultTextInfo(id); Axes_Lib_BDraw_textInfos.SetData(); }
  public virtual void Gpu_Axes_Lib_BDraw_getTextInfo() { g_SetData(); Axes_Lib_BDraw_fontInfos?.SetCpu(); Axes_Lib_BDraw_fontInfos_SetKernels(); Axes_Lib_BDraw_textInfos?.SetCpu(); Axes_Lib_BDraw_textInfos_SetKernels(); Axes_Lib_BDraw_ABuff_Indexes?.SetCpu(); Axes_Lib_BDraw_ABuff_Indexes_SetKernels(); Axes_Lib_BDraw_tab_delimeted_text?.SetCpu(); Axes_Lib_BDraw_tab_delimeted_text_SetKernels(); Gpu(kernel_Axes_Lib_BDraw_getTextInfo, Axes_Lib_BDraw_getTextInfo, Axes_Lib_BDraw_textN); Axes_Lib_BDraw_textInfos?.ResetWrite(); }
  public virtual void Cpu_Axes_Lib_BDraw_getTextInfo() { Axes_Lib_BDraw_fontInfos?.GetGpu(); Axes_Lib_BDraw_textInfos?.GetGpu(); Axes_Lib_BDraw_ABuff_Indexes?.GetGpu(); Axes_Lib_BDraw_tab_delimeted_text?.GetGpu(); Cpu(Axes_Lib_BDraw_getTextInfo, Axes_Lib_BDraw_textN); Axes_Lib_BDraw_textInfos.SetData(); }
  public virtual void Cpu_Axes_Lib_BDraw_getTextInfo(uint3 id) { Axes_Lib_BDraw_fontInfos?.GetGpu(); Axes_Lib_BDraw_textInfos?.GetGpu(); Axes_Lib_BDraw_ABuff_Indexes?.GetGpu(); Axes_Lib_BDraw_tab_delimeted_text?.GetGpu(); Axes_Lib_BDraw_getTextInfo(id); Axes_Lib_BDraw_textInfos.SetData(); }
  public virtual void Gpu_Rand_initState() { g_SetData(); Rand_rs?.SetCpu(); Rand_rs_SetKernels(); Gpu(kernel_Rand_initState, Rand_initState, Rand_I); Rand_rs?.ResetWrite(); }
  public virtual void Cpu_Rand_initState() { Rand_rs?.GetGpu(); Cpu(Rand_initState, Rand_I); Rand_rs.SetData(); }
  public virtual void Cpu_Rand_initState(uint3 id) { Rand_rs?.GetGpu(); Rand_initState(id); Rand_rs.SetData(); }
  public virtual void Gpu_Rand_initSeed() { g_SetData(); Rand_rs_SetKernels(); Gpu(kernel_Rand_initSeed, Rand_initSeed, Rand_N); Rand_rs?.ResetWrite(); }
  public virtual void Cpu_Rand_initSeed() { Rand_rs?.GetGpu(); Cpu(Rand_initSeed, Rand_N); Rand_rs.SetData(); }
  public virtual void Cpu_Rand_initSeed(uint3 id) { Rand_rs?.GetGpu(); Rand_initSeed(id); Rand_rs.SetData(); }
  public virtual void Gpu_Rand_grp_init_1M() { g_SetData(); Gpu(kernel_Rand_grp_init_1M, Rand_grp_init_1M, Rand_N / 1024 / 1024); }
  public virtual IEnumerator Cpu_Rand_grp_init_1M() { yield return StartCoroutine(Cpu_Coroutine(kernel_Rand_grp_init_1M, Rand_grp_init_1M, Rand_N / 1024 / 1024)); }
  public virtual void Cpu_Rand_grp_init_1M(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { Rand_grp_init_1M(grp_tid, grp_id, id, grpI); }
  public virtual void Gpu_Rand_grp_init_1K() { g_SetData(); Gpu(kernel_Rand_grp_init_1K, Rand_grp_init_1K, Rand_N / 1024); }
  public virtual IEnumerator Cpu_Rand_grp_init_1K() { yield return StartCoroutine(Cpu_Coroutine(kernel_Rand_grp_init_1K, Rand_grp_init_1K, Rand_N / 1024)); }
  public virtual void Cpu_Rand_grp_init_1K(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { Rand_grp_init_1K(grp_tid, grp_id, id, grpI); }
  public virtual void Gpu_Rand_grp_fill_1K() { g_SetData(); Gpu(kernel_Rand_grp_fill_1K, Rand_grp_fill_1K, Rand_N); }
  public virtual IEnumerator Cpu_Rand_grp_fill_1K() { yield return StartCoroutine(Cpu_Coroutine(kernel_Rand_grp_fill_1K, Rand_grp_fill_1K, Rand_N)); }
  public virtual void Cpu_Rand_grp_fill_1K(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { Rand_grp_fill_1K(grp_tid, grp_id, id, grpI); }
  [JsonConverter(typeof(StringEnumConverter))] public enum Axes_Lib_BDraw_Draw : uint { Point, Sphere, Line, Arrow, Signal, LineSegment, Texture_2D, Quad, WebCam, Mesh, Number, N }
  [JsonConverter(typeof(StringEnumConverter))] public enum Axes_Lib_BDraw_TextAlignment : uint { BottomLeft, CenterLeft, TopLeft, BottomCenter, CenterCenter, TopCenter, BottomRight, CenterRight, TopRight }
  [JsonConverter(typeof(StringEnumConverter))] public enum Axes_Lib_BDraw_Text_QuadType : uint { FrontOnly, FrontBack, Switch, Arrow, Billboard }
  [JsonConverter(typeof(StringEnumConverter))] public enum Axes_Lib_PaletteType : uint { Rainbow, GradientRainbow, GradientRainbow10, GradientRainbow20, Heat, GradientHeat, WhiteRainbow, invRainbow, Green, Gray, DarkGray, CT }
  public const uint Axes_Lib_BDraw_Draw_Point = 0, Axes_Lib_BDraw_Draw_Sphere = 1, Axes_Lib_BDraw_Draw_Line = 2, Axes_Lib_BDraw_Draw_Arrow = 3, Axes_Lib_BDraw_Draw_Signal = 4, Axes_Lib_BDraw_Draw_LineSegment = 5, Axes_Lib_BDraw_Draw_Texture_2D = 6, Axes_Lib_BDraw_Draw_Quad = 7, Axes_Lib_BDraw_Draw_WebCam = 8, Axes_Lib_BDraw_Draw_Mesh = 9, Axes_Lib_BDraw_Draw_Number = 10, Axes_Lib_BDraw_Draw_N = 11;
  public const uint Axes_Lib_BDraw_TextAlignment_BottomLeft = 0, Axes_Lib_BDraw_TextAlignment_CenterLeft = 1, Axes_Lib_BDraw_TextAlignment_TopLeft = 2, Axes_Lib_BDraw_TextAlignment_BottomCenter = 3, Axes_Lib_BDraw_TextAlignment_CenterCenter = 4, Axes_Lib_BDraw_TextAlignment_TopCenter = 5, Axes_Lib_BDraw_TextAlignment_BottomRight = 6, Axes_Lib_BDraw_TextAlignment_CenterRight = 7, Axes_Lib_BDraw_TextAlignment_TopRight = 8;
  public const uint Axes_Lib_BDraw_Text_QuadType_FrontOnly = 0, Axes_Lib_BDraw_Text_QuadType_FrontBack = 1, Axes_Lib_BDraw_Text_QuadType_Switch = 2, Axes_Lib_BDraw_Text_QuadType_Arrow = 3, Axes_Lib_BDraw_Text_QuadType_Billboard = 4;
  public const uint Axes_Lib_PaletteType_Rainbow = 0, Axes_Lib_PaletteType_GradientRainbow = 1, Axes_Lib_PaletteType_GradientRainbow10 = 2, Axes_Lib_PaletteType_GradientRainbow20 = 3, Axes_Lib_PaletteType_Heat = 4, Axes_Lib_PaletteType_GradientHeat = 5, Axes_Lib_PaletteType_WhiteRainbow = 6, Axes_Lib_PaletteType_invRainbow = 7, Axes_Lib_PaletteType_Green = 8, Axes_Lib_PaletteType_Gray = 9, Axes_Lib_PaletteType_DarkGray = 10, Axes_Lib_PaletteType_CT = 11;
  public const uint Axes_Lib_BDraw_Draw_Text3D = 12, Axes_Lib_BDraw_LF = 10, Axes_Lib_BDraw_TB = 9, Axes_Lib_BDraw_ZERO = 48, Axes_Lib_BDraw_NINE = 57, Axes_Lib_BDraw_PERIOD = 46, Axes_Lib_BDraw_COMMA = 44, Axes_Lib_BDraw_PLUS = 43, Axes_Lib_BDraw_MINUS = 45, Axes_Lib_BDraw_SPACE = 32;
  [HideInInspector] public bool ValuesChanged, gChanged;
  public static gsBrownian This;
  public virtual void Awake() { This = this as gsBrownian; Awake_GS(); }
  public virtual void Awake_GS()
  {
    if(!GS_Axes_Lib.CheckKey("you@gmail.com", "2024/12/10", 123456)) Status($"Axes_Lib not registered, check email, expiration, and key in gsBrownian_GS class");
    if(!GS_OCam_Lib.CheckKey("you@gmail.com", "2024/12/10", 123456)) Status($"OCam_Lib not registered, check email, expiration, and key in gsBrownian_GS class");
    if(!GS_Views_Lib.CheckKey("you@gmail.com", "2024/12/10", 123456)) Status($"Views_Lib not registered, check email, expiration, and key in gsBrownian_GS class");
  }
  public virtual void Start() { Build_UI(); Load_UI(); Start0_GS(); InitBuffers(); Start1_GS(); }
  public virtual void Start0_GS()
  {
    Rand_Start0_GS();
    Axes_Lib_Start0_GS();
  }
  public virtual void Start1_GS()
  {
    Rand_Start1_GS();
    Axes_Lib_Start1_GS();
  }
  public override void onLoaded()
  {
    base.onLoaded();
    GS_Axes_Lib.onLoaded(this);
    GS_OCam_Lib.onLoaded(OCam_Lib);
    GS_Views_Lib.onLoaded(Views_Lib);
  }
  [HideInInspector] public bool already_quited = false;
  public override void OnApplicationQuit() { Save_UI(); OnApplicationQuit_GS(); base.OnApplicationQuit(); already_quited = true; }
  public virtual void OnApplicationQuit_GS()
  {
    Rand_OnApplicationQuit_GS();
    Axes_Lib_OnApplicationQuit_GS();
  }
  public override void ui_to_data()
  {
    if (data == null) return;
    data.siUnits = siUnits;
    data.group_UI = group_UI;
    data.group_Brownian = group_Brownian;
    data.tickerN = tickerN;
    data.dayN = dayN;
    data.gainSD = gainSD;
    data.globalTrend = globalTrend;
    data.trendSD = trendSD;
    data.maxDisplayGain = maxDisplayGain;
    data.lineThickness = lineThickness;
    data.displayTickerI = displayTickerI;
    data.displayTickerN = displayTickerN;
    data.group_Quantrade_GDM = group_Quantrade_GDM;
    data.dimI = dimI;
    data.Axes_Lib_group_Axes_Lib = Axes_Lib_group_Axes_Lib;
    data.Axes_Lib_group_Geometry = Axes_Lib_group_Geometry;
    data.Axes_Lib_drawGrid = Axes_Lib_drawGrid;
    data.Axes_Lib_GridX = Axes_Lib_GridX;
    data.Axes_Lib_GridY = Axes_Lib_GridY;
    data.Axes_Lib_GridZ = Axes_Lib_GridZ;
    data.Axes_Lib_group_Axes = Axes_Lib_group_Axes;
    data.Axes_Lib_drawBox = Axes_Lib_drawBox;
    data.Axes_Lib_boxLineThickness = Axes_Lib_boxLineThickness;
    data.Axes_Lib_drawAxes = Axes_Lib_drawAxes;
    data.Axes_Lib_customAxesRangeN = Axes_Lib_customAxesRangeN;
    data.Axes_Lib_axesRangeMin = Axes_Lib_axesRangeMin;
    data.Axes_Lib_axesRangeMax = Axes_Lib_axesRangeMax;
    data.Axes_Lib_axesRangeMin1 = Axes_Lib_axesRangeMin1;
    data.Axes_Lib_axesRangeMax1 = Axes_Lib_axesRangeMax1;
    data.Axes_Lib_axesRangeMin2 = Axes_Lib_axesRangeMin2;
    data.Axes_Lib_axesRangeMax2 = Axes_Lib_axesRangeMax2;
    data.Axes_Lib_titles = Axes_Lib_titles;
    data.Axes_Lib_axesFormats = Axes_Lib_axesFormats;
    data.Axes_Lib_textSize = Axes_Lib_textSize;
    data.Axes_Lib_axesColor = Axes_Lib_axesColor;
    data.Axes_Lib_axesOpacity = Axes_Lib_axesOpacity;
    data.Axes_Lib_zeroOrigin = Axes_Lib_zeroOrigin;
  }
  public override void data_to_ui()
  {
    if (data == null) return;
    group_UI = data.group_UI;
    group_Brownian = data.group_Brownian;
    tickerN = ui_txt_str.Contains("\"tickerN\"") ? data.tickerN : 500;
    dayN = ui_txt_str.Contains("\"dayN\"") ? data.dayN : 252;
    gainSD = ui_txt_str.Contains("\"gainSD\"") ? data.gainSD : 0.02f;
    globalTrend = ui_txt_str.Contains("\"globalTrend\"") ? data.globalTrend : 0.02f;
    trendSD = ui_txt_str.Contains("\"trendSD\"") ? data.trendSD : 0.02f;
    maxDisplayGain = ui_txt_str.Contains("\"maxDisplayGain\"") ? data.maxDisplayGain : 7f;
    lineThickness = ui_txt_str.Contains("\"lineThickness\"") ? data.lineThickness : 0.004f;
    displayTickerI = ui_txt_str.Contains("\"displayTickerI\"") ? data.displayTickerI : 0;
    displayTickerN = ui_txt_str.Contains("\"displayTickerN\"") ? data.displayTickerN : 1;
    group_Quantrade_GDM = data.group_Quantrade_GDM;
    dimI = ui_txt_str.Contains("\"dimI\"") ? data.dimI : 1;
    Axes_Lib_group_Axes_Lib = data.Axes_Lib_group_Axes_Lib;
    Axes_Lib_group_Geometry = data.Axes_Lib_group_Geometry;
    Axes_Lib_drawGrid = data.Axes_Lib_drawGrid;
    Axes_Lib_GridX = ui_txt_str.Contains("\"Axes_Lib_GridX\"") ? data.Axes_Lib_GridX : float2("0, 0.1");
    Axes_Lib_GridY = ui_txt_str.Contains("\"Axes_Lib_GridY\"") ? data.Axes_Lib_GridY : float2("0, 0.1");
    Axes_Lib_GridZ = ui_txt_str.Contains("\"Axes_Lib_GridZ\"") ? data.Axes_Lib_GridZ : float2("0, 0.1");
    Axes_Lib_group_Axes = data.Axes_Lib_group_Axes;
    Axes_Lib_drawBox = data.Axes_Lib_drawBox;
    Axes_Lib_boxLineThickness = ui_txt_str.Contains("\"Axes_Lib_boxLineThickness\"") ? data.Axes_Lib_boxLineThickness : 10f;
    Axes_Lib_drawAxes = data.Axes_Lib_drawAxes;
    Axes_Lib_customAxesRangeN = ui_txt_str.Contains("\"Axes_Lib_customAxesRangeN\"") ? data.Axes_Lib_customAxesRangeN : 0;
    Axes_Lib_axesRangeMin = data.Axes_Lib_axesRangeMin;
    Axes_Lib_axesRangeMax = data.Axes_Lib_axesRangeMax;
    Axes_Lib_axesRangeMin1 = data.Axes_Lib_axesRangeMin1;
    Axes_Lib_axesRangeMax1 = data.Axes_Lib_axesRangeMax1;
    Axes_Lib_axesRangeMin2 = data.Axes_Lib_axesRangeMin2;
    Axes_Lib_axesRangeMax2 = data.Axes_Lib_axesRangeMax2;
    Axes_Lib_titles = data.Axes_Lib_titles;
    Axes_Lib_axesFormats = data.Axes_Lib_axesFormats;
    Axes_Lib_textSize = ui_txt_str.Contains("\"Axes_Lib_textSize\"") ? data.Axes_Lib_textSize : float2("0.075");
    Axes_Lib_axesColor = ui_txt_str.Contains("\"Axes_Lib_axesColor\"") ? data.Axes_Lib_axesColor : float3("0.5");
    Axes_Lib_axesOpacity = ui_txt_str.Contains("\"Axes_Lib_axesOpacity\"") ? data.Axes_Lib_axesOpacity : 1f;
    Axes_Lib_zeroOrigin = data.Axes_Lib_zeroOrigin;
  }
  public virtual void Save(string path, string projectName)
  {
    projectPaths = path;
    $"{projectPath}{projectName}.txt".WriteAllText(JsonConvert.SerializeObject(data, Formatting.Indented));
    foreach (var lib in GetComponents<GS>()) if (lib != this) lib.Save_UI();
    string usFile = $"{projectPath}usUnits.txt";
    if (siUnits) usFile.DeleteFile();
    else usFile.WriteAllText("usUnits");
  }
  public override bool Save_UI_As(string path, string projectName)
  {
    if (already_quited) return false;
    if (data != null) ui_to_data();
    if (lib_parent_gs == this) Save(path, projectName);
    else $"{path}{projectName}.txt".WriteAllText(JsonConvert.SerializeObject(data, Formatting.Indented));
    return true;
  }
  public override bool Load_UI_As(string path, string projectName)
  {
    if (path == appPath && SelectedProjectFile.Exists())
      path = $"{appPath}{SelectedProjectFile.ReadAllText()}/".Replace("//", "/");
    if(loadedProjectPath == path) return false;
    projectPaths = loadedProjectPath = path;
    string file = $"{projectPath}{projectName}.txt";
    data = file.Exists() ? JsonConvert.DeserializeObject<uiData>(ui_txt_str = file.ReadAllText()) : new uiData();
    if(data == null) return false;
    foreach (var fld in data.GetType().GetFields(bindings)) if (fld != null && fld.FieldType == typeof(TreeGroup) && fld.GetValue(data) == null) fld.SetValue(data, new TreeGroup() { isChecked = true });
    data_to_ui();
    UI_group_UI?.Display_Tree();
    if (lib_parent_gs == this)
    {
      foreach (var lib in GetComponents<GS>())
        if (lib != this && lib.GetType() != "gsProject_Lib".ToType())
        {
          lib.Build_UI();
          lib.Load_UI();
        }
    }
    ui_loaded = true;
    return true;
  }
  public virtual void OnApplicationPause(bool pause) { if (ui_loaded) Save_UI(); }
  [HideInInspector] public uint lateUpdateI = 0;
  public virtual void LateUpdate()
  {
    if (!ui_loaded) return;
    string usFile = $"{projectPath}usUnits.txt";
    if (lateUpdateI == 5 && usFile.Exists()) { usFile.DeleteFile(); siUnits = false; OnUnitsChanged(); }
    LateUpdate0_GS();
    if (UI_tickerN.Changed || tickerN != UI_tickerN.v) tickerN = UI_tickerN.v;
    if (UI_dayN.Changed || dayN != UI_dayN.v) dayN = UI_dayN.v;
    if (UI_gainSD.Changed || gainSD != UI_gainSD.v) gainSD = UI_gainSD.v;
    if (UI_globalTrend.Changed || globalTrend != UI_globalTrend.v) globalTrend = UI_globalTrend.v;
    if (UI_trendSD.Changed || trendSD != UI_trendSD.v) trendSD = UI_trendSD.v;
    if (UI_maxDisplayGain.Changed || maxDisplayGain != UI_maxDisplayGain.v) maxDisplayGain = UI_maxDisplayGain.v;
    if (UI_lineThickness.Changed || lineThickness != UI_lineThickness.v) lineThickness = UI_lineThickness.v;
    if (UI_displayTickerI.Changed || displayTickerI != UI_displayTickerI.v) displayTickerI = UI_displayTickerI.v;
    if (UI_displayTickerN.Changed || displayTickerN != UI_displayTickerN.v) displayTickerN = UI_displayTickerN.v;
    if (UI_dimI.Changed || dimI != UI_dimI.v) dimI = UI_dimI.v;
    if (UI_Axes_Lib_drawGrid.Changed || Axes_Lib_drawGrid != UI_Axes_Lib_drawGrid.v) Axes_Lib_drawGrid = UI_Axes_Lib_drawGrid.v;
    if (UI_Axes_Lib_GridX.Changed || any(Axes_Lib_GridX != UI_Axes_Lib_GridX.v)) Axes_Lib_GridX = UI_Axes_Lib_GridX.v;
    if (UI_Axes_Lib_GridY.Changed || any(Axes_Lib_GridY != UI_Axes_Lib_GridY.v)) Axes_Lib_GridY = UI_Axes_Lib_GridY.v;
    if (UI_Axes_Lib_GridZ.Changed || any(Axes_Lib_GridZ != UI_Axes_Lib_GridZ.v)) Axes_Lib_GridZ = UI_Axes_Lib_GridZ.v;
    if (UI_Axes_Lib_drawBox.Changed || Axes_Lib_drawBox != UI_Axes_Lib_drawBox.v) Axes_Lib_drawBox = UI_Axes_Lib_drawBox.v;
    if (UI_Axes_Lib_boxLineThickness.Changed || Axes_Lib_boxLineThickness != UI_Axes_Lib_boxLineThickness.v) Axes_Lib_boxLineThickness = UI_Axes_Lib_boxLineThickness.v;
    if (UI_Axes_Lib_drawAxes.Changed || Axes_Lib_drawAxes != UI_Axes_Lib_drawAxes.v) Axes_Lib_drawAxes = UI_Axes_Lib_drawAxes.v;
    if (UI_Axes_Lib_customAxesRangeN.Changed || Axes_Lib_customAxesRangeN != UI_Axes_Lib_customAxesRangeN.v) Axes_Lib_customAxesRangeN = UI_Axes_Lib_customAxesRangeN.v;
    if (UI_Axes_Lib_axesRangeMin.Changed || any(Axes_Lib_axesRangeMin != UI_Axes_Lib_axesRangeMin.v)) Axes_Lib_axesRangeMin = UI_Axes_Lib_axesRangeMin.v;
    if (UI_Axes_Lib_axesRangeMax.Changed || any(Axes_Lib_axesRangeMax != UI_Axes_Lib_axesRangeMax.v)) Axes_Lib_axesRangeMax = UI_Axes_Lib_axesRangeMax.v;
    if (UI_Axes_Lib_axesRangeMin1.Changed || any(Axes_Lib_axesRangeMin1 != UI_Axes_Lib_axesRangeMin1.v)) Axes_Lib_axesRangeMin1 = UI_Axes_Lib_axesRangeMin1.v;
    if (UI_Axes_Lib_axesRangeMax1.Changed || any(Axes_Lib_axesRangeMax1 != UI_Axes_Lib_axesRangeMax1.v)) Axes_Lib_axesRangeMax1 = UI_Axes_Lib_axesRangeMax1.v;
    if (UI_Axes_Lib_axesRangeMin2.Changed || any(Axes_Lib_axesRangeMin2 != UI_Axes_Lib_axesRangeMin2.v)) Axes_Lib_axesRangeMin2 = UI_Axes_Lib_axesRangeMin2.v;
    if (UI_Axes_Lib_axesRangeMax2.Changed || any(Axes_Lib_axesRangeMax2 != UI_Axes_Lib_axesRangeMax2.v)) Axes_Lib_axesRangeMax2 = UI_Axes_Lib_axesRangeMax2.v;
    if (UI_Axes_Lib_titles.Changed || Axes_Lib_titles != UI_Axes_Lib_titles.v) { data.Axes_Lib_titles = UI_Axes_Lib_titles.v; ValuesChanged = gChanged = true; }
    if (UI_Axes_Lib_axesFormats.Changed || Axes_Lib_axesFormats != UI_Axes_Lib_axesFormats.v) { data.Axes_Lib_axesFormats = UI_Axes_Lib_axesFormats.v; ValuesChanged = gChanged = true; }
    if (UI_Axes_Lib_textSize.Changed || any(Axes_Lib_textSize != UI_Axes_Lib_textSize.v)) Axes_Lib_textSize = UI_Axes_Lib_textSize.v;
    if (UI_Axes_Lib_axesColor.Changed || any(Axes_Lib_axesColor != UI_Axes_Lib_axesColor.v)) Axes_Lib_axesColor = UI_Axes_Lib_axesColor.v;
    if (UI_Axes_Lib_axesOpacity.Changed || Axes_Lib_axesOpacity != UI_Axes_Lib_axesOpacity.v) Axes_Lib_axesOpacity = UI_Axes_Lib_axesOpacity.v;
    if (UI_Axes_Lib_zeroOrigin.Changed || Axes_Lib_zeroOrigin != UI_Axes_Lib_zeroOrigin.v) Axes_Lib_zeroOrigin = UI_Axes_Lib_zeroOrigin.v;
    if (ValuesChanged) { gChanged = true; OnValueChanged(); ValuesChanged = UI_group_UI.Changed = UI_group_Brownian.Changed = UI_tickerN.Changed = UI_dayN.Changed = UI_gainSD.Changed = UI_globalTrend.Changed = UI_trendSD.Changed = UI_maxDisplayGain.Changed = UI_lineThickness.Changed = UI_displayTickerI.Changed = UI_displayTickerN.Changed = UI_group_Quantrade_GDM.Changed = UI_dimI.Changed = UI_Axes_Lib_group_Axes_Lib.Changed = UI_Axes_Lib_group_Geometry.Changed = UI_Axes_Lib_drawGrid.Changed = UI_Axes_Lib_GridX.Changed = UI_Axes_Lib_GridY.Changed = UI_Axes_Lib_GridZ.Changed = UI_Axes_Lib_group_Axes.Changed = UI_Axes_Lib_drawBox.Changed = UI_Axes_Lib_boxLineThickness.Changed = UI_Axes_Lib_drawAxes.Changed = UI_Axes_Lib_customAxesRangeN.Changed = UI_Axes_Lib_axesRangeMin.Changed = UI_Axes_Lib_axesRangeMax.Changed = UI_Axes_Lib_axesRangeMin1.Changed = UI_Axes_Lib_axesRangeMax1.Changed = UI_Axes_Lib_axesRangeMin2.Changed = UI_Axes_Lib_axesRangeMax2.Changed = UI_Axes_Lib_titles.Changed = UI_Axes_Lib_axesFormats.Changed = UI_Axes_Lib_textSize.Changed = UI_Axes_Lib_axesColor.Changed = UI_Axes_Lib_axesOpacity.Changed = UI_Axes_Lib_zeroOrigin.Changed = false; }
    Rand_LateUpdate1_GS();
    Axes_Lib_LateUpdate1_GS();
    LateUpdate1_GS();
    if(lateUpdateI++ == uint_max) lateUpdateI = 100;
  }
  public virtual void LateUpdate0_GS()
  {
    Rand_LateUpdate0_GS();
    Axes_Lib_LateUpdate0_GS();
  }
  public virtual void LateUpdate1_GS()
  {
    Rand_LateUpdate1_GS();
    Axes_Lib_LateUpdate1_GS();
  }
  public override void Update()
  {
    base.Update();
    if (!ui_loaded) return;
    Update0_GS();
    Rand_Update1_GS();
    Axes_Lib_Update1_GS();
    Update1_GS();
  }
  public virtual void Update0_GS()
  {
    Rand_Update0_GS();
    Axes_Lib_Update0_GS();
  }
  public virtual void Update1_GS()
  {
    Rand_Update1_GS();
    Axes_Lib_Update1_GS();
  }
  public override void OnValueChanged()
  {
    if (!ui_loaded) return;
    OnValueChanged_GS();
    if (UI_Axes_Lib_drawGrid.Changed) { Axes_Lib_buildText = true; }
    if (UI_Axes_Lib_GridX.Changed) { Axes_Lib_buildText = true; }
    UI_Axes_Lib_GridX.DisplayIf(Show_Axes_Lib_GridX && UI_Axes_Lib_GridX.treeGroup_parent.isExpanded);
    if (UI_Axes_Lib_GridY.Changed) { Axes_Lib_buildText = true; }
    UI_Axes_Lib_GridY.DisplayIf(Show_Axes_Lib_GridY && UI_Axes_Lib_GridY.treeGroup_parent.isExpanded);
    if (UI_Axes_Lib_GridZ.Changed) { Axes_Lib_buildText = true; }
    UI_Axes_Lib_GridZ.DisplayIf(Show_Axes_Lib_GridZ && UI_Axes_Lib_GridZ.treeGroup_parent.isExpanded);
    if (UI_Axes_Lib_drawBox.Changed) { Axes_Lib_buildText = true; }
    UI_Axes_Lib_drawBox.DisplayIf(Show_Axes_Lib_drawBox && UI_Axes_Lib_drawBox.treeGroup_parent.isExpanded);
    if (UI_Axes_Lib_boxLineThickness.Changed) { Axes_Lib_buildText = true; }
    UI_Axes_Lib_boxLineThickness.DisplayIf(Show_Axes_Lib_boxLineThickness && UI_Axes_Lib_boxLineThickness.treeGroup_parent.isExpanded);
    if (UI_Axes_Lib_drawAxes.Changed) { Axes_Lib_buildText = true; }
    UI_Axes_Lib_drawAxes.DisplayIf(Show_Axes_Lib_drawAxes && UI_Axes_Lib_drawAxes.treeGroup_parent.isExpanded);
    if (UI_Axes_Lib_customAxesRangeN.Changed) { Axes_Lib_buildText = true; }
    UI_Axes_Lib_customAxesRangeN.DisplayIf(Show_Axes_Lib_customAxesRangeN && UI_Axes_Lib_customAxesRangeN.treeGroup_parent.isExpanded);
    if (UI_Axes_Lib_axesRangeMin.Changed) { Axes_Lib_buildText = true; }
    UI_Axes_Lib_axesRangeMin.DisplayIf(Show_Axes_Lib_axesRangeMin && UI_Axes_Lib_axesRangeMin.treeGroup_parent.isExpanded);
    if (UI_Axes_Lib_axesRangeMax.Changed) { Axes_Lib_buildText = true; }
    UI_Axes_Lib_axesRangeMax.DisplayIf(Show_Axes_Lib_axesRangeMax && UI_Axes_Lib_axesRangeMax.treeGroup_parent.isExpanded);
    if (UI_Axes_Lib_axesRangeMin1.Changed) { Axes_Lib_buildText = true; }
    UI_Axes_Lib_axesRangeMin1.DisplayIf(Show_Axes_Lib_axesRangeMin1 && UI_Axes_Lib_axesRangeMin1.treeGroup_parent.isExpanded);
    if (UI_Axes_Lib_axesRangeMax1.Changed) { Axes_Lib_buildText = true; }
    UI_Axes_Lib_axesRangeMax1.DisplayIf(Show_Axes_Lib_axesRangeMax1 && UI_Axes_Lib_axesRangeMax1.treeGroup_parent.isExpanded);
    if (UI_Axes_Lib_axesRangeMin2.Changed) { Axes_Lib_buildText = true; }
    UI_Axes_Lib_axesRangeMin2.DisplayIf(Show_Axes_Lib_axesRangeMin2 && UI_Axes_Lib_axesRangeMin2.treeGroup_parent.isExpanded);
    if (UI_Axes_Lib_axesRangeMax2.Changed) { Axes_Lib_buildText = true; }
    UI_Axes_Lib_axesRangeMax2.DisplayIf(Show_Axes_Lib_axesRangeMax2 && UI_Axes_Lib_axesRangeMax2.treeGroup_parent.isExpanded);
    if (UI_Axes_Lib_titles.Changed) { Axes_Lib_buildText = true; }
    UI_Axes_Lib_titles.DisplayIf(Show_Axes_Lib_titles && UI_Axes_Lib_titles.treeGroup_parent.isExpanded);
    if (UI_Axes_Lib_axesFormats.Changed) { Axes_Lib_buildText = true; }
    UI_Axes_Lib_axesFormats.DisplayIf(Show_Axes_Lib_axesFormats && UI_Axes_Lib_axesFormats.treeGroup_parent.isExpanded);
    if (UI_Axes_Lib_textSize.Changed) { Axes_Lib_buildText = true; }
    UI_Axes_Lib_textSize.DisplayIf(Show_Axes_Lib_textSize && UI_Axes_Lib_textSize.treeGroup_parent.isExpanded);
    if (UI_Axes_Lib_axesColor.Changed) { Axes_Lib_buildText = true; }
    UI_Axes_Lib_axesColor.DisplayIf(Show_Axes_Lib_axesColor && UI_Axes_Lib_axesColor.treeGroup_parent.isExpanded);
    if (UI_Axes_Lib_axesOpacity.Changed) { Axes_Lib_buildText = true; }
    UI_Axes_Lib_axesOpacity.DisplayIf(Show_Axes_Lib_axesOpacity && UI_Axes_Lib_axesOpacity.treeGroup_parent.isExpanded);
    if (UI_Axes_Lib_zeroOrigin.Changed) { Axes_Lib_buildText = true; }
    UI_Axes_Lib_zeroOrigin.DisplayIf(Show_Axes_Lib_zeroOrigin && UI_Axes_Lib_zeroOrigin.treeGroup_parent.isExpanded);
  }
  public override void OnValueChanged_GS()
  {
    Rand_OnValueChanged_GS();
    Axes_Lib_OnValueChanged_GS();
    var type = "gsReport_Lib".ToType();
    if (type != null) ((GS)GetComponent(type))?.OnValueChanged_GS();

  }
  public string[] Scenes_in_Build => new string[] {  };
  public virtual bool Show_vert_Axes_Lib_BDraw_Text { get => Axes_Lib_drawBox && Axes_Lib_drawAxes; }
  public virtual bool Show_vert_Axes_Lib_BDraw_Box { get => Axes_Lib_drawBox; }
  public virtual bool Show_Axes_Lib_GridX { get => Axes_Lib_drawGrid; }
  public virtual bool Show_Axes_Lib_GridY { get => Axes_Lib_drawGrid; }
  public virtual bool Show_Axes_Lib_GridZ { get => Axes_Lib_drawGrid; }
  public virtual bool Show_Axes_Lib_drawBox { get => Axes_Lib_drawGrid; }
  public virtual bool Show_Axes_Lib_boxLineThickness { get => Axes_Lib_drawBox; }
  public virtual bool Show_Axes_Lib_drawAxes { get => Axes_Lib_drawGrid; }
  public virtual bool Show_Axes_Lib_customAxesRangeN { get => Axes_Lib_drawGrid && Axes_Lib_drawAxes; }
  public virtual bool Show_Axes_Lib_axesRangeMin { get => Axes_Lib_showNormalizedAxes && Axes_Lib_customAxesRangeN > 0; }
  public virtual bool Show_Axes_Lib_axesRangeMax { get => Axes_Lib_showNormalizedAxes && Axes_Lib_customAxesRangeN > 0; }
  public virtual bool Show_Axes_Lib_axesRangeMin1 { get => Axes_Lib_showNormalizedAxes && Axes_Lib_customAxesRangeN > 1; }
  public virtual bool Show_Axes_Lib_axesRangeMax1 { get => Axes_Lib_showNormalizedAxes && Axes_Lib_customAxesRangeN > 1; }
  public virtual bool Show_Axes_Lib_axesRangeMin2 { get => Axes_Lib_showNormalizedAxes && Axes_Lib_customAxesRangeN > 2; }
  public virtual bool Show_Axes_Lib_axesRangeMax2 { get => Axes_Lib_showNormalizedAxes && Axes_Lib_customAxesRangeN > 2; }
  public virtual bool Show_Axes_Lib_titles { get => Axes_Lib_drawGrid && Axes_Lib_drawAxes; }
  public virtual bool Show_Axes_Lib_axesFormats { get => Axes_Lib_drawGrid && Axes_Lib_drawAxes; }
  public virtual bool Show_Axes_Lib_textSize { get => Axes_Lib_drawGrid && Axes_Lib_drawAxes; }
  public virtual bool Show_Axes_Lib_axesColor { get => Axes_Lib_drawGrid && Axes_Lib_drawAxes; }
  public virtual bool Show_Axes_Lib_axesOpacity { get => Axes_Lib_drawGrid && Axes_Lib_drawAxes; }
  public virtual bool Show_Axes_Lib_zeroOrigin { get => Axes_Lib_drawGrid && Axes_Lib_drawAxes; }
  public virtual uint tickerN { get => g.tickerN; set { if (g.tickerN != value || UI_tickerN.v != value) { g.tickerN = UI_tickerN.v = value; ValuesChanged = gChanged = true; } } }
  public virtual uint dayN { get => g.dayN; set { if (g.dayN != value || UI_dayN.v != value) { g.dayN = UI_dayN.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float gainSD { get => g.gainSD; set { if (any(g.gainSD != value) || any(UI_gainSD.v != value)) { g.gainSD = UI_gainSD.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float globalTrend { get => g.globalTrend; set { if (any(g.globalTrend != value) || any(UI_globalTrend.v != value)) { g.globalTrend = UI_globalTrend.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float trendSD { get => g.trendSD; set { if (any(g.trendSD != value) || any(UI_trendSD.v != value)) { g.trendSD = UI_trendSD.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float maxDisplayGain { get => g.maxDisplayGain; set { if (any(g.maxDisplayGain != value) || any(UI_maxDisplayGain.v != value)) { g.maxDisplayGain = UI_maxDisplayGain.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float lineThickness { get => g.lineThickness; set { if (any(g.lineThickness != value) || any(UI_lineThickness.v != value)) { g.lineThickness = UI_lineThickness.v = value; ValuesChanged = gChanged = true; } } }
  public virtual uint displayTickerI { get => g.displayTickerI; set { if (g.displayTickerI != value || UI_displayTickerI.v != value) { g.displayTickerI = UI_displayTickerI.v = value; ValuesChanged = gChanged = true; } } }
  public virtual uint displayTickerN { get => g.displayTickerN; set { if (g.displayTickerN != value || UI_displayTickerN.v != value) { g.displayTickerN = UI_displayTickerN.v = value; ValuesChanged = gChanged = true; } } }
  public virtual int dimI { get => g.dimI; set { if (g.dimI != value || UI_dimI.v != value) { g.dimI = UI_dimI.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float price0 { get => g.price0; set { if (any(g.price0 != value)) { g.price0 = value; ValuesChanged = gChanged = true; } } }
  public virtual float2 gainRange { get => g.gainRange; set { if (any(g.gainRange != value)) { g.gainRange = value; ValuesChanged = gChanged = true; } } }
  public virtual uint Rand_N { get => g.Rand_N; set { if (g.Rand_N != value) { g.Rand_N = value; ValuesChanged = gChanged = true; } } }
  public virtual uint Rand_I { get => g.Rand_I; set { if (g.Rand_I != value) { g.Rand_I = value; ValuesChanged = gChanged = true; } } }
  public virtual uint Rand_J { get => g.Rand_J; set { if (g.Rand_J != value) { g.Rand_J = value; ValuesChanged = gChanged = true; } } }
  public virtual uint4 Rand_seed4 { get => g.Rand_seed4; set { if (any(g.Rand_seed4 != value)) { g.Rand_seed4 = value; ValuesChanged = gChanged = true; } } }
  public virtual uint Axes_Lib_BDraw_ABuff_IndexN { get => g.Axes_Lib_BDraw_ABuff_IndexN; set { if (g.Axes_Lib_BDraw_ABuff_IndexN != value) { g.Axes_Lib_BDraw_ABuff_IndexN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint Axes_Lib_BDraw_ABuff_BitN { get => g.Axes_Lib_BDraw_ABuff_BitN; set { if (g.Axes_Lib_BDraw_ABuff_BitN != value) { g.Axes_Lib_BDraw_ABuff_BitN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint Axes_Lib_BDraw_ABuff_N { get => g.Axes_Lib_BDraw_ABuff_N; set { if (g.Axes_Lib_BDraw_ABuff_N != value) { g.Axes_Lib_BDraw_ABuff_N = value; ValuesChanged = gChanged = true; } } }
  public virtual uint Axes_Lib_BDraw_ABuff_BitN1 { get => g.Axes_Lib_BDraw_ABuff_BitN1; set { if (g.Axes_Lib_BDraw_ABuff_BitN1 != value) { g.Axes_Lib_BDraw_ABuff_BitN1 = value; ValuesChanged = gChanged = true; } } }
  public virtual uint Axes_Lib_BDraw_ABuff_BitN2 { get => g.Axes_Lib_BDraw_ABuff_BitN2; set { if (g.Axes_Lib_BDraw_ABuff_BitN2 != value) { g.Axes_Lib_BDraw_ABuff_BitN2 = value; ValuesChanged = gChanged = true; } } }
  public virtual bool Axes_Lib_BDraw_omitText { get => Is(g.Axes_Lib_BDraw_omitText); set { if (g.Axes_Lib_BDraw_omitText != Is(value)) { g.Axes_Lib_BDraw_omitText = Is(value); ValuesChanged = gChanged = true; } } }
  public virtual bool Axes_Lib_BDraw_includeUnicode { get => Is(g.Axes_Lib_BDraw_includeUnicode); set { if (g.Axes_Lib_BDraw_includeUnicode != Is(value)) { g.Axes_Lib_BDraw_includeUnicode = Is(value); ValuesChanged = gChanged = true; } } }
  public virtual uint Axes_Lib_BDraw_fontInfoN { get => g.Axes_Lib_BDraw_fontInfoN; set { if (g.Axes_Lib_BDraw_fontInfoN != value) { g.Axes_Lib_BDraw_fontInfoN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint Axes_Lib_BDraw_textN { get => g.Axes_Lib_BDraw_textN; set { if (g.Axes_Lib_BDraw_textN != value) { g.Axes_Lib_BDraw_textN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint Axes_Lib_BDraw_textCharN { get => g.Axes_Lib_BDraw_textCharN; set { if (g.Axes_Lib_BDraw_textCharN != value) { g.Axes_Lib_BDraw_textCharN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint Axes_Lib_BDraw_boxEdgeN { get => g.Axes_Lib_BDraw_boxEdgeN; set { if (g.Axes_Lib_BDraw_boxEdgeN != value) { g.Axes_Lib_BDraw_boxEdgeN = value; ValuesChanged = gChanged = true; } } }
  public virtual float Axes_Lib_BDraw_fontSize { get => g.Axes_Lib_BDraw_fontSize; set { if (any(g.Axes_Lib_BDraw_fontSize != value)) { g.Axes_Lib_BDraw_fontSize = value; ValuesChanged = gChanged = true; } } }
  public virtual float Axes_Lib_BDraw_boxThickness { get => g.Axes_Lib_BDraw_boxThickness; set { if (any(g.Axes_Lib_BDraw_boxThickness != value)) { g.Axes_Lib_BDraw_boxThickness = value; ValuesChanged = gChanged = true; } } }
  public virtual float4 Axes_Lib_BDraw_boxColor { get => g.Axes_Lib_BDraw_boxColor; set { if (any(g.Axes_Lib_BDraw_boxColor != value)) { g.Axes_Lib_BDraw_boxColor = value; ValuesChanged = gChanged = true; } } }
  public virtual bool Axes_Lib_drawGrid { get => Is(g.Axes_Lib_drawGrid); set { if (g.Axes_Lib_drawGrid != Is(value) || UI_Axes_Lib_drawGrid.v != value) { g.Axes_Lib_drawGrid = Is(UI_Axes_Lib_drawGrid.v = value); ValuesChanged = gChanged = true; } } }
  public virtual float2 Axes_Lib_GridX { get => g.Axes_Lib_GridX; set { if (any(g.Axes_Lib_GridX != value) || any(UI_Axes_Lib_GridX.v != value)) { g.Axes_Lib_GridX = UI_Axes_Lib_GridX.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float2 Axes_Lib_GridY { get => g.Axes_Lib_GridY; set { if (any(g.Axes_Lib_GridY != value) || any(UI_Axes_Lib_GridY.v != value)) { g.Axes_Lib_GridY = UI_Axes_Lib_GridY.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float2 Axes_Lib_GridZ { get => g.Axes_Lib_GridZ; set { if (any(g.Axes_Lib_GridZ != value) || any(UI_Axes_Lib_GridZ.v != value)) { g.Axes_Lib_GridZ = UI_Axes_Lib_GridZ.v = value; ValuesChanged = gChanged = true; } } }
  public virtual bool Axes_Lib_drawBox { get => Is(g.Axes_Lib_drawBox); set { if (g.Axes_Lib_drawBox != Is(value) || UI_Axes_Lib_drawBox.v != value) { g.Axes_Lib_drawBox = Is(UI_Axes_Lib_drawBox.v = value); ValuesChanged = gChanged = true; } } }
  public virtual float Axes_Lib_boxLineThickness { get => g.Axes_Lib_boxLineThickness; set { if (any(g.Axes_Lib_boxLineThickness != value) || any(UI_Axes_Lib_boxLineThickness.v != value)) { g.Axes_Lib_boxLineThickness = UI_Axes_Lib_boxLineThickness.v = value; ValuesChanged = gChanged = true; } } }
  public virtual bool Axes_Lib_drawAxes { get => Is(g.Axes_Lib_drawAxes); set { if (g.Axes_Lib_drawAxes != Is(value) || UI_Axes_Lib_drawAxes.v != value) { g.Axes_Lib_drawAxes = Is(UI_Axes_Lib_drawAxes.v = value); ValuesChanged = gChanged = true; } } }
  public virtual uint Axes_Lib_customAxesRangeN { get => g.Axes_Lib_customAxesRangeN; set { if (g.Axes_Lib_customAxesRangeN != value || UI_Axes_Lib_customAxesRangeN.v != value) { g.Axes_Lib_customAxesRangeN = UI_Axes_Lib_customAxesRangeN.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float3 Axes_Lib_axesRangeMin { get => g.Axes_Lib_axesRangeMin; set { if (any(g.Axes_Lib_axesRangeMin != value) || any(UI_Axes_Lib_axesRangeMin.v != value)) { g.Axes_Lib_axesRangeMin = UI_Axes_Lib_axesRangeMin.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float3 Axes_Lib_axesRangeMax { get => g.Axes_Lib_axesRangeMax; set { if (any(g.Axes_Lib_axesRangeMax != value) || any(UI_Axes_Lib_axesRangeMax.v != value)) { g.Axes_Lib_axesRangeMax = UI_Axes_Lib_axesRangeMax.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float3 Axes_Lib_axesRangeMin1 { get => g.Axes_Lib_axesRangeMin1; set { if (any(g.Axes_Lib_axesRangeMin1 != value) || any(UI_Axes_Lib_axesRangeMin1.v != value)) { g.Axes_Lib_axesRangeMin1 = UI_Axes_Lib_axesRangeMin1.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float3 Axes_Lib_axesRangeMax1 { get => g.Axes_Lib_axesRangeMax1; set { if (any(g.Axes_Lib_axesRangeMax1 != value) || any(UI_Axes_Lib_axesRangeMax1.v != value)) { g.Axes_Lib_axesRangeMax1 = UI_Axes_Lib_axesRangeMax1.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float3 Axes_Lib_axesRangeMin2 { get => g.Axes_Lib_axesRangeMin2; set { if (any(g.Axes_Lib_axesRangeMin2 != value) || any(UI_Axes_Lib_axesRangeMin2.v != value)) { g.Axes_Lib_axesRangeMin2 = UI_Axes_Lib_axesRangeMin2.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float3 Axes_Lib_axesRangeMax2 { get => g.Axes_Lib_axesRangeMax2; set { if (any(g.Axes_Lib_axesRangeMax2 != value) || any(UI_Axes_Lib_axesRangeMax2.v != value)) { g.Axes_Lib_axesRangeMax2 = UI_Axes_Lib_axesRangeMax2.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float2 Axes_Lib_textSize { get => g.Axes_Lib_textSize; set { if (any(g.Axes_Lib_textSize != value) || any(UI_Axes_Lib_textSize.v != value)) { g.Axes_Lib_textSize = UI_Axes_Lib_textSize.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float3 Axes_Lib_axesColor { get => g.Axes_Lib_axesColor; set { if (any(g.Axes_Lib_axesColor != value) || any(UI_Axes_Lib_axesColor.v != value)) { g.Axes_Lib_axesColor = UI_Axes_Lib_axesColor.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float Axes_Lib_axesOpacity { get => g.Axes_Lib_axesOpacity; set { if (any(g.Axes_Lib_axesOpacity != value) || any(UI_Axes_Lib_axesOpacity.v != value)) { g.Axes_Lib_axesOpacity = UI_Axes_Lib_axesOpacity.v = value; ValuesChanged = gChanged = true; } } }
  public virtual bool Axes_Lib_zeroOrigin { get => Is(g.Axes_Lib_zeroOrigin); set { if (g.Axes_Lib_zeroOrigin != Is(value) || UI_Axes_Lib_zeroOrigin.v != value) { g.Axes_Lib_zeroOrigin = Is(UI_Axes_Lib_zeroOrigin.v = value); ValuesChanged = gChanged = true; } } }
  public virtual bool Axes_Lib_buildText { get => Is(g.Axes_Lib_buildText); set { if (g.Axes_Lib_buildText != Is(value)) { g.Axes_Lib_buildText = Is(value); ValuesChanged = gChanged = true; } } }
  public virtual bool Axes_Lib_showAxes { get => Is(g.Axes_Lib_showAxes); set { if (g.Axes_Lib_showAxes != Is(value)) { g.Axes_Lib_showAxes = Is(value); ValuesChanged = gChanged = true; } } }
  public virtual bool Axes_Lib_showOutline { get => Is(g.Axes_Lib_showOutline); set { if (g.Axes_Lib_showOutline != Is(value)) { g.Axes_Lib_showOutline = Is(value); ValuesChanged = gChanged = true; } } }
  public virtual bool Axes_Lib_showNormalizedAxes { get => Is(g.Axes_Lib_showNormalizedAxes); set { if (g.Axes_Lib_showNormalizedAxes != Is(value)) { g.Axes_Lib_showNormalizedAxes = Is(value); ValuesChanged = gChanged = true; } } }
  public bool group_UI { get => UI_group_UI?.v ?? false; set { if (UI_group_UI != null) UI_group_UI.v = value; } }
  public bool group_Brownian { get => UI_group_Brownian?.v ?? false; set { if (UI_group_Brownian != null) UI_group_Brownian.v = value; } }
  public bool group_Quantrade_GDM { get => UI_group_Quantrade_GDM?.v ?? false; set { if (UI_group_Quantrade_GDM != null) UI_group_Quantrade_GDM.v = value; } }
  public bool Axes_Lib_group_Axes_Lib { get => UI_Axes_Lib_group_Axes_Lib?.v ?? false; set { if (UI_Axes_Lib_group_Axes_Lib != null) UI_Axes_Lib_group_Axes_Lib.v = value; } }
  public bool Axes_Lib_group_Geometry { get => UI_Axes_Lib_group_Geometry?.v ?? false; set { if (UI_Axes_Lib_group_Geometry != null) UI_Axes_Lib_group_Geometry.v = value; } }
  public bool Axes_Lib_group_Axes { get => UI_Axes_Lib_group_Axes?.v ?? false; set { if (UI_Axes_Lib_group_Axes != null) UI_Axes_Lib_group_Axes.v = value; } }
  public UI_TreeGroup UI_group_UI, UI_group_Brownian, UI_group_Quantrade_GDM, UI_Axes_Lib_group_Axes_Lib, UI_Axes_Lib_group_Geometry, UI_Axes_Lib_group_Axes;
  public UI_uint UI_tickerN, UI_dayN, UI_displayTickerI, UI_displayTickerN, UI_Axes_Lib_customAxesRangeN;
  public UI_float UI_gainSD, UI_globalTrend, UI_trendSD, UI_maxDisplayGain, UI_lineThickness, UI_Axes_Lib_boxLineThickness, UI_Axes_Lib_axesOpacity;
  public UI_method UI_Calc_1D;
  public virtual void Calc_1D() { InitBuffers(); Rand_Init(2 * tickerN * dayN, 7); }
  public UI_int UI_dimI;
  public UI_method UI_driftExamples;
  public virtual void driftExamples() { }
  public UI_bool UI_Axes_Lib_drawGrid, UI_Axes_Lib_drawBox, UI_Axes_Lib_drawAxes, UI_Axes_Lib_zeroOrigin;
  public UI_float2 UI_Axes_Lib_GridX, UI_Axes_Lib_GridY, UI_Axes_Lib_GridZ, UI_Axes_Lib_textSize;
  public UI_float3 UI_Axes_Lib_axesRangeMin, UI_Axes_Lib_axesRangeMax, UI_Axes_Lib_axesRangeMin1, UI_Axes_Lib_axesRangeMax1, UI_Axes_Lib_axesRangeMin2, UI_Axes_Lib_axesRangeMax2, UI_Axes_Lib_axesColor;
  public UI_string UI_Axes_Lib_titles, UI_Axes_Lib_axesFormats;
  public string Axes_Lib_titles { get => UI_Axes_Lib_titles?.v ?? ""; set { if (UI_Axes_Lib_titles != null && data != null) UI_Axes_Lib_titles.v = data.Axes_Lib_titles = value; } }
  public string Axes_Lib_axesFormats { get => UI_Axes_Lib_axesFormats?.v ?? ""; set { if (UI_Axes_Lib_axesFormats != null && data != null) UI_Axes_Lib_axesFormats.v = data.Axes_Lib_axesFormats = value; } }
  public UI_TreeGroup ui_group_UI => UI_group_UI;
  public UI_TreeGroup ui_group_Brownian => UI_group_Brownian;
  public UI_uint ui_tickerN => UI_tickerN;
  public UI_uint ui_dayN => UI_dayN;
  public UI_float ui_gainSD => UI_gainSD;
  public UI_float ui_globalTrend => UI_globalTrend;
  public UI_float ui_trendSD => UI_trendSD;
  public UI_float ui_maxDisplayGain => UI_maxDisplayGain;
  public UI_float ui_lineThickness => UI_lineThickness;
  public UI_uint ui_displayTickerI => UI_displayTickerI;
  public UI_uint ui_displayTickerN => UI_displayTickerN;
  public UI_TreeGroup ui_group_Quantrade_GDM => UI_group_Quantrade_GDM;
  public UI_int ui_dimI => UI_dimI;
  public UI_TreeGroup ui_Axes_Lib_group_Axes_Lib => UI_Axes_Lib_group_Axes_Lib;
  public UI_TreeGroup ui_Axes_Lib_group_Geometry => UI_Axes_Lib_group_Geometry;
  public UI_bool ui_Axes_Lib_drawGrid => UI_Axes_Lib_drawGrid;
  public UI_float2 ui_Axes_Lib_GridX => UI_Axes_Lib_GridX;
  public UI_float2 ui_Axes_Lib_GridY => UI_Axes_Lib_GridY;
  public UI_float2 ui_Axes_Lib_GridZ => UI_Axes_Lib_GridZ;
  public UI_TreeGroup ui_Axes_Lib_group_Axes => UI_Axes_Lib_group_Axes;
  public UI_bool ui_Axes_Lib_drawBox => UI_Axes_Lib_drawBox;
  public UI_float ui_Axes_Lib_boxLineThickness => UI_Axes_Lib_boxLineThickness;
  public UI_bool ui_Axes_Lib_drawAxes => UI_Axes_Lib_drawAxes;
  public UI_uint ui_Axes_Lib_customAxesRangeN => UI_Axes_Lib_customAxesRangeN;
  public UI_float3 ui_Axes_Lib_axesRangeMin => UI_Axes_Lib_axesRangeMin;
  public UI_float3 ui_Axes_Lib_axesRangeMax => UI_Axes_Lib_axesRangeMax;
  public UI_float3 ui_Axes_Lib_axesRangeMin1 => UI_Axes_Lib_axesRangeMin1;
  public UI_float3 ui_Axes_Lib_axesRangeMax1 => UI_Axes_Lib_axesRangeMax1;
  public UI_float3 ui_Axes_Lib_axesRangeMin2 => UI_Axes_Lib_axesRangeMin2;
  public UI_float3 ui_Axes_Lib_axesRangeMax2 => UI_Axes_Lib_axesRangeMax2;
  public UI_string ui_Axes_Lib_titles => UI_Axes_Lib_titles;
  public UI_string ui_Axes_Lib_axesFormats => UI_Axes_Lib_axesFormats;
  public UI_float2 ui_Axes_Lib_textSize => UI_Axes_Lib_textSize;
  public UI_float3 ui_Axes_Lib_axesColor => UI_Axes_Lib_axesColor;
  public UI_float ui_Axes_Lib_axesOpacity => UI_Axes_Lib_axesOpacity;
  public UI_bool ui_Axes_Lib_zeroOrigin => UI_Axes_Lib_zeroOrigin;
  [Serializable]
  public class uiData
  {
    public bool siUnits, group_UI, group_Brownian, group_Quantrade_GDM, Axes_Lib_group_Axes_Lib, Axes_Lib_group_Geometry, Axes_Lib_drawGrid, Axes_Lib_group_Axes, Axes_Lib_drawBox, Axes_Lib_drawAxes, Axes_Lib_zeroOrigin;
    public uint tickerN, dayN, displayTickerI, displayTickerN, Axes_Lib_customAxesRangeN;
    public float gainSD, globalTrend, trendSD, maxDisplayGain, lineThickness, Axes_Lib_boxLineThickness, Axes_Lib_axesOpacity;
    public int dimI;
    public float2 Axes_Lib_GridX, Axes_Lib_GridY, Axes_Lib_GridZ, Axes_Lib_textSize;
    public float3 Axes_Lib_axesRangeMin, Axes_Lib_axesRangeMax, Axes_Lib_axesRangeMin1, Axes_Lib_axesRangeMax1, Axes_Lib_axesRangeMin2, Axes_Lib_axesRangeMax2, Axes_Lib_axesColor;
    public string Axes_Lib_titles, Axes_Lib_axesFormats;
  }
  public virtual void InitBuffers()
  {
    InitBuffers0_GS();
    AllocData_gBrownian(1);
    InitKernels();
    SetKernelValues(gBrownian, nameof(gBrownian), kernel_CalcGain, kernel_SumPrice, kernel_InitPrice, kernel_InitTrends, kernel_Axes_Lib_BDraw_ABuff_GetIndexes, kernel_Axes_Lib_BDraw_ABuff_IncSums, kernel_Axes_Lib_BDraw_ABuff_IncFills1, kernel_Axes_Lib_BDraw_ABuff_GetFills2, kernel_Axes_Lib_BDraw_ABuff_GetFills1, kernel_Axes_Lib_BDraw_ABuff_Get_Bits_Sums, kernel_Axes_Lib_BDraw_ABuff_GetSums, kernel_Axes_Lib_BDraw_ABuff_Get_Bits, kernel_Axes_Lib_BDraw_setDefaultTextInfo, kernel_Axes_Lib_BDraw_getTextInfo, kernel_Rand_initState, kernel_Rand_initSeed);
    SetKernelValues(gBrownian, nameof(gBrownian), kernel_Rand_grp_init_1M, kernel_Rand_grp_init_1K, kernel_Rand_grp_fill_1K);
    AllocData_vs(tickerN * dayN);
    AllocData_vs0(tickerN * dayN);
    AllocData_trends(tickerN);
    AllocData_Rand_rs(Rand_N);
    AllocData_Axes_Lib_BDraw_tab_delimeted_text(Axes_Lib_BDraw_textN);
    AllocData_Axes_Lib_BDraw_textInfos(Axes_Lib_BDraw_textN);
    AllocData_Axes_Lib_BDraw_fontInfos(Axes_Lib_BDraw_fontInfoN);
    InitBuffers1_GS();
    isInitBuffers = true;
  }
  public virtual void InitBuffers0_GS()
  {
    Rand_InitBuffers0_GS();
    Axes_Lib_InitBuffers0_GS();
  }
  public virtual void InitBuffers1_GS()
  {
    Rand_InitBuffers1_GS();
    Axes_Lib_InitBuffers1_GS();
  }
  [HideInInspector] public uint4[] Rand_grp = new uint4[1024];
  [HideInInspector] public uint[] Axes_Lib_BDraw_ABuff_grp = new uint[1024];
  [HideInInspector] public uint[] Axes_Lib_BDraw_ABuff_grp0 = new uint[1024];
  [Serializable]
  public struct GBrownian
  {
    public uint tickerN, dayN, displayTickerI, displayTickerN, Rand_N, Rand_I, Rand_J, Axes_Lib_BDraw_ABuff_IndexN, Axes_Lib_BDraw_ABuff_BitN, Axes_Lib_BDraw_ABuff_N, Axes_Lib_BDraw_ABuff_BitN1, Axes_Lib_BDraw_ABuff_BitN2, Axes_Lib_BDraw_omitText, Axes_Lib_BDraw_includeUnicode, Axes_Lib_BDraw_fontInfoN, Axes_Lib_BDraw_textN, Axes_Lib_BDraw_textCharN, Axes_Lib_BDraw_boxEdgeN, Axes_Lib_drawGrid, Axes_Lib_drawBox, Axes_Lib_drawAxes, Axes_Lib_customAxesRangeN, Axes_Lib_zeroOrigin, Axes_Lib_buildText, Axes_Lib_showAxes, Axes_Lib_showOutline, Axes_Lib_showNormalizedAxes;
    public float gainSD, globalTrend, trendSD, maxDisplayGain, lineThickness, price0, Axes_Lib_BDraw_fontSize, Axes_Lib_BDraw_boxThickness, Axes_Lib_boxLineThickness, Axes_Lib_axesOpacity;
    public int dimI;
    public float2 gainRange, Axes_Lib_GridX, Axes_Lib_GridY, Axes_Lib_GridZ, Axes_Lib_textSize;
    public uint4 Rand_seed4;
    public float4 Axes_Lib_BDraw_boxColor;
    public float3 Axes_Lib_axesRangeMin, Axes_Lib_axesRangeMax, Axes_Lib_axesRangeMin1, Axes_Lib_axesRangeMax1, Axes_Lib_axesRangeMin2, Axes_Lib_axesRangeMax2, Axes_Lib_axesColor;
  };
  public RWStructuredBuffer<GBrownian> gBrownian;
  public struct Axes_Lib_BDraw_FontInfo { public float2 uvBottomLeft, uvBottomRight, uvTopLeft, uvTopRight; public int advance, bearing, minX, minY, maxX, maxY; };
  public struct Axes_Lib_BDraw_TextInfo { public float3 p, right, up, p0, p1; public float2 size, uvSize; public float4 color, backColor; public uint justification, textI, quadType, axis; public float height; };
  public RWStructuredBuffer<int> vs, vs0;
  public RWStructuredBuffer<float> trends;
  public RWStructuredBuffer<uint4> Rand_rs;
  public RWStructuredBuffer<uint> Axes_Lib_BDraw_tab_delimeted_text, Axes_Lib_BDraw_ABuff_Bits, Axes_Lib_BDraw_ABuff_Sums, Axes_Lib_BDraw_ABuff_Indexes, Axes_Lib_BDraw_ABuff_Fills1, Axes_Lib_BDraw_ABuff_Fills2;
  public RWStructuredBuffer<Axes_Lib_BDraw_TextInfo> Axes_Lib_BDraw_textInfos;
  public RWStructuredBuffer<Axes_Lib_BDraw_FontInfo> Axes_Lib_BDraw_fontInfos;
  public virtual void AllocData_gBrownian(uint n) => AddComputeBuffer(ref gBrownian, nameof(gBrownian), n);
  public virtual void AllocData_vs(uint n) => AddComputeBuffer(ref vs, nameof(vs), n);
  public virtual void AssignData_vs(params int[] data) => AddComputeBufferData(ref vs, nameof(vs), data);
  public virtual void AllocData_vs0(uint n) => AddComputeBuffer(ref vs0, nameof(vs0), n);
  public virtual void AssignData_vs0(params int[] data) => AddComputeBufferData(ref vs0, nameof(vs0), data);
  public virtual void AllocData_trends(uint n) => AddComputeBuffer(ref trends, nameof(trends), n);
  public virtual void AssignData_trends(params float[] data) => AddComputeBufferData(ref trends, nameof(trends), data);
  public virtual void AllocData_Rand_rs(uint n) => AddComputeBuffer(ref Rand_rs, nameof(Rand_rs), n);
  public virtual void AssignData_Rand_rs(params uint4[] data) => AddComputeBufferData(ref Rand_rs, nameof(Rand_rs), data);
  public virtual void AllocData_Axes_Lib_BDraw_tab_delimeted_text(uint n) => AddComputeBuffer(ref Axes_Lib_BDraw_tab_delimeted_text, nameof(Axes_Lib_BDraw_tab_delimeted_text), n);
  public virtual void AssignData_Axes_Lib_BDraw_tab_delimeted_text(params uint[] data) => AddComputeBufferData(ref Axes_Lib_BDraw_tab_delimeted_text, nameof(Axes_Lib_BDraw_tab_delimeted_text), data);
  public virtual void AllocData_Axes_Lib_BDraw_textInfos(uint n) => AddComputeBuffer(ref Axes_Lib_BDraw_textInfos, nameof(Axes_Lib_BDraw_textInfos), n);
  public virtual void AssignData_Axes_Lib_BDraw_textInfos(params Axes_Lib_BDraw_TextInfo[] data) => AddComputeBufferData(ref Axes_Lib_BDraw_textInfos, nameof(Axes_Lib_BDraw_textInfos), data);
  public virtual void AllocData_Axes_Lib_BDraw_fontInfos(uint n) => AddComputeBuffer(ref Axes_Lib_BDraw_fontInfos, nameof(Axes_Lib_BDraw_fontInfos), n);
  public virtual void AssignData_Axes_Lib_BDraw_fontInfos(params Axes_Lib_BDraw_FontInfo[] data) => AddComputeBufferData(ref Axes_Lib_BDraw_fontInfos, nameof(Axes_Lib_BDraw_fontInfos), data);
  public virtual void AllocData_Axes_Lib_BDraw_ABuff_Bits(uint n) => AddComputeBuffer(ref Axes_Lib_BDraw_ABuff_Bits, nameof(Axes_Lib_BDraw_ABuff_Bits), n);
  public virtual void AssignData_Axes_Lib_BDraw_ABuff_Bits(params uint[] data) => AddComputeBufferData(ref Axes_Lib_BDraw_ABuff_Bits, nameof(Axes_Lib_BDraw_ABuff_Bits), data);
  public virtual void AllocData_Axes_Lib_BDraw_ABuff_Sums(uint n) => AddComputeBuffer(ref Axes_Lib_BDraw_ABuff_Sums, nameof(Axes_Lib_BDraw_ABuff_Sums), n);
  public virtual void AssignData_Axes_Lib_BDraw_ABuff_Sums(params uint[] data) => AddComputeBufferData(ref Axes_Lib_BDraw_ABuff_Sums, nameof(Axes_Lib_BDraw_ABuff_Sums), data);
  public virtual void AllocData_Axes_Lib_BDraw_ABuff_Indexes(uint n) => AddComputeBuffer(ref Axes_Lib_BDraw_ABuff_Indexes, nameof(Axes_Lib_BDraw_ABuff_Indexes), n);
  public virtual void AssignData_Axes_Lib_BDraw_ABuff_Indexes(params uint[] data) => AddComputeBufferData(ref Axes_Lib_BDraw_ABuff_Indexes, nameof(Axes_Lib_BDraw_ABuff_Indexes), data);
  public virtual void AllocData_Axes_Lib_BDraw_ABuff_Fills1(uint n) => AddComputeBuffer(ref Axes_Lib_BDraw_ABuff_Fills1, nameof(Axes_Lib_BDraw_ABuff_Fills1), n);
  public virtual void AssignData_Axes_Lib_BDraw_ABuff_Fills1(params uint[] data) => AddComputeBufferData(ref Axes_Lib_BDraw_ABuff_Fills1, nameof(Axes_Lib_BDraw_ABuff_Fills1), data);
  public virtual void AllocData_Axes_Lib_BDraw_ABuff_Fills2(uint n) => AddComputeBuffer(ref Axes_Lib_BDraw_ABuff_Fills2, nameof(Axes_Lib_BDraw_ABuff_Fills2), n);
  public virtual void AssignData_Axes_Lib_BDraw_ABuff_Fills2(params uint[] data) => AddComputeBufferData(ref Axes_Lib_BDraw_ABuff_Fills2, nameof(Axes_Lib_BDraw_ABuff_Fills2), data);
  public Texture2D _PaletteTex;
  public float4 palette(float v) => paletteColor(_PaletteTex, v);
  public float4 palette(float v, float w) => float4(palette(v).xyz, w);

  public Texture2D Axes_Lib_BDraw_fontTexture;
  public virtual void onRenderObject_LIN(bool show, uint _itemN, ref uint i, ref uint index, ref uint3 LIN) { uint n = 0; if (show) { if (i < (n = _itemN)) LIN = uint3(index, i, 0); LIN.z += n; i -= n; } index++; }
  public virtual void onRenderObject_LIN(uint _itemN, ref uint i, ref uint index, ref uint3 LIN) { onRenderObject_LIN(true, _itemN, ref i, ref index, ref LIN); }
  public virtual uint3 onRenderObject_LIN(uint i) { uint3 LIN = u000; uint index = 0; onRenderObject_LIN(tickerN, ref i, ref index, ref LIN); onRenderObject_LIN(Axes_Lib_drawBox && Axes_Lib_drawAxes, Axes_Lib_BDraw_textN, ref i, ref index, ref LIN); onRenderObject_LIN(Axes_Lib_drawBox, 12, ref i, ref index, ref LIN); return LIN; }
  public virtual uint3 onRenderObject_LIN_Quads(uint i) { uint3 LIN = u000; uint index = 0; onRenderObject_LIN(tickerN, ref i, ref index, ref LIN); onRenderObject_LIN(Axes_Lib_drawBox && Axes_Lib_drawAxes, Axes_Lib_BDraw_textN, ref i, ref index, ref LIN); onRenderObject_LIN(Axes_Lib_drawBox, 12, ref i, ref index, ref LIN); return LIN; }
  public virtual uint3 onRenderObject_LIN_Points(uint i) { uint3 LIN = u000; return LIN; }
  [HideInInspector] public int kernel_CalcGain; [numthreads(numthreads2, numthreads2, 1)] protected void CalcGain(uint3 id) { unchecked { if (id.y < dayN && id.x < tickerN) CalcGain_GS(id); } }
  public virtual void CalcGain_GS(uint3 id) { }
  [HideInInspector] public int kernel_SumPrice; [numthreads(numthreads2, numthreads2, 1)] protected void SumPrice(uint3 id) { unchecked { if (id.y < dayN * (dayN - 1) / 2 && id.x < tickerN) SumPrice_GS(id); } }
  public virtual void SumPrice_GS(uint3 id) { }
  [HideInInspector] public int kernel_InitPrice; [numthreads(numthreads2, numthreads2, 1)] protected void InitPrice(uint3 id) { unchecked { if (id.y < dayN && id.x < tickerN) InitPrice_GS(id); } }
  public virtual void InitPrice_GS(uint3 id) { }
  [HideInInspector] public int kernel_InitTrends; [numthreads(numthreads1, 1, 1)] protected void InitTrends(uint3 id) { unchecked { if (id.x < tickerN) InitTrends_GS(id); } }
  public virtual void InitTrends_GS(uint3 id) { }
  [HideInInspector] public int kernel_Axes_Lib_BDraw_ABuff_GetIndexes; [numthreads(numthreads1, 1, 1)] protected void Axes_Lib_BDraw_ABuff_GetIndexes(uint3 id) { unchecked { if (id.x < Axes_Lib_BDraw_ABuff_BitN) Axes_Lib_BDraw_ABuff_GetIndexes_GS(id); } }
  [HideInInspector] public int kernel_Axes_Lib_BDraw_ABuff_IncSums; [numthreads(numthreads1, 1, 1)] protected void Axes_Lib_BDraw_ABuff_IncSums(uint3 id) { unchecked { if (id.x < Axes_Lib_BDraw_ABuff_BitN) Axes_Lib_BDraw_ABuff_IncSums_GS(id); } }
  [HideInInspector] public int kernel_Axes_Lib_BDraw_ABuff_IncFills1; [numthreads(numthreads1, 1, 1)] protected void Axes_Lib_BDraw_ABuff_IncFills1(uint3 id) { unchecked { if (id.x < Axes_Lib_BDraw_ABuff_BitN1) Axes_Lib_BDraw_ABuff_IncFills1_GS(id); } }
  [HideInInspector] public int kernel_Axes_Lib_BDraw_ABuff_GetFills2; [numthreads(numthreads1, 1, 1)] protected IEnumerator Axes_Lib_BDraw_ABuff_GetFills2(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < Axes_Lib_BDraw_ABuff_BitN2) yield return StartCoroutine(Axes_Lib_BDraw_ABuff_GetFills2_GS(grp_tid, grp_id, id, grpI)); } }
  [HideInInspector] public int kernel_Axes_Lib_BDraw_ABuff_GetFills1; [numthreads(numthreads1, 1, 1)] protected IEnumerator Axes_Lib_BDraw_ABuff_GetFills1(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < Axes_Lib_BDraw_ABuff_BitN1) yield return StartCoroutine(Axes_Lib_BDraw_ABuff_GetFills1_GS(grp_tid, grp_id, id, grpI)); } }
  [HideInInspector] public int kernel_Axes_Lib_BDraw_ABuff_Get_Bits_Sums; [numthreads(numthreads1, 1, 1)] protected IEnumerator Axes_Lib_BDraw_ABuff_Get_Bits_Sums(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < Axes_Lib_BDraw_ABuff_BitN) yield return StartCoroutine(Axes_Lib_BDraw_ABuff_Get_Bits_Sums_GS(grp_tid, grp_id, id, grpI)); } }
  [HideInInspector] public int kernel_Axes_Lib_BDraw_ABuff_GetSums; [numthreads(numthreads1, 1, 1)] protected IEnumerator Axes_Lib_BDraw_ABuff_GetSums(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < Axes_Lib_BDraw_ABuff_BitN) yield return StartCoroutine(Axes_Lib_BDraw_ABuff_GetSums_GS(grp_tid, grp_id, id, grpI)); } }
  [HideInInspector] public int kernel_Axes_Lib_BDraw_ABuff_Get_Bits; [numthreads(numthreads1, 1, 1)] protected void Axes_Lib_BDraw_ABuff_Get_Bits(uint3 id) { unchecked { if (id.x < Axes_Lib_BDraw_ABuff_BitN) Axes_Lib_BDraw_ABuff_Get_Bits_GS(id); } }
  [HideInInspector] public int kernel_Axes_Lib_BDraw_setDefaultTextInfo; [numthreads(numthreads1, 1, 1)] protected void Axes_Lib_BDraw_setDefaultTextInfo(uint3 id) { unchecked { if (id.x < Axes_Lib_BDraw_textN) Axes_Lib_BDraw_setDefaultTextInfo_GS(id); } }
  [HideInInspector] public int kernel_Axes_Lib_BDraw_getTextInfo; [numthreads(numthreads1, 1, 1)] protected void Axes_Lib_BDraw_getTextInfo(uint3 id) { unchecked { if (id.x < Axes_Lib_BDraw_textN) Axes_Lib_BDraw_getTextInfo_GS(id); } }
  [HideInInspector] public int kernel_Rand_initState; [numthreads(numthreads1, 1, 1)] protected void Rand_initState(uint3 id) { unchecked { if (id.x < Rand_I) Rand_initState_GS(id); } }
  [HideInInspector] public int kernel_Rand_initSeed; [numthreads(numthreads1, 1, 1)] protected void Rand_initSeed(uint3 id) { unchecked { if (id.x < Rand_N) Rand_initSeed_GS(id); } }
  [HideInInspector] public int kernel_Rand_grp_init_1M; [numthreads(numthreads1, 1, 1)] protected IEnumerator Rand_grp_init_1M(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < Rand_N / 1024 / 1024) yield return StartCoroutine(Rand_grp_init_1M_GS(grp_tid, grp_id, id, grpI)); } }
  public virtual IEnumerator Rand_grp_init_1M_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { yield return null; }
  [HideInInspector] public int kernel_Rand_grp_init_1K; [numthreads(numthreads1, 1, 1)] protected IEnumerator Rand_grp_init_1K(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < Rand_N / 1024) yield return StartCoroutine(Rand_grp_init_1K_GS(grp_tid, grp_id, id, grpI)); } }
  public virtual IEnumerator Rand_grp_init_1K_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { yield return null; }
  [HideInInspector] public int kernel_Rand_grp_fill_1K; [numthreads(numthreads1, 1, 1)] protected IEnumerator Rand_grp_fill_1K(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < Rand_N) yield return StartCoroutine(Rand_grp_fill_1K_GS(grp_tid, grp_id, id, grpI)); } }
  public virtual IEnumerator Rand_grp_fill_1K_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { yield return null; }
  public virtual v2f vert_GS(uint i, uint j, v2f o)
  {
    uint3 LIN = onRenderObject_LIN(i); int index = -1, level = ((int)LIN.x); i = LIN.y;
    if (level == ++index) o = vert_Draw_Random_Signal(i, j, o);
    else if (level == ++index) o = vert_Axes_Lib_BDraw_Text(i, j, o);
    else if (level == ++index) o = vert_Axes_Lib_BDraw_Box(i, j, o);
    return o;
  }
  public virtual v2f vert(uint i, uint j)
  {
    v2f o = default;
    o.pos = o.color = o.tj = o.tk = f0000; o.normal = o.p0 = o.p1 = o.wPos = f000; o.uv = f00; o.ti = float4(i, j, 0, 0);
    return vert_GS(i, j, o);
  }
  public virtual v2f vert_Draw_Random_Signal(uint i, uint j, v2f o) => o;
  public virtual bool onRenderObject_GS_N(bool cpu)
  {
    RenderQuads(material, onRenderObject_LIN_Quads(0).z);
    RenderPoints(material, onRenderObject_LIN_Points(0).z);
    return true;
  }
  public override bool onRenderObject()
  {
    if (gBrownian == null) return false;
    bool render = true, cpu = false;
    onRenderObject_GS(ref render, ref cpu);
    g_SetData();
    if (!render) return false;
    if (cpu) Cpu(material, new { gBrownian }, new { vs }, new { vs0 }, new { trends }, new { Rand_rs }, new { Rand_grp }, new { Axes_Lib_BDraw_tab_delimeted_text }, new { Axes_Lib_BDraw_textInfos }, new { Axes_Lib_BDraw_fontInfos }, new { Axes_Lib_BDraw_ABuff_Bits }, new { Axes_Lib_BDraw_ABuff_Sums }, new { Axes_Lib_BDraw_ABuff_Indexes }, new { Axes_Lib_BDraw_ABuff_grp }, new { Axes_Lib_BDraw_ABuff_grp0 }, new { Axes_Lib_BDraw_ABuff_Fills1 }, new { Axes_Lib_BDraw_ABuff_Fills2 }, new { Axes_Lib_BDraw_fontTexture }, new { _PaletteTex });
    else Gpu(material, new { gBrownian }, new { vs }, new { vs0 }, new { trends }, new { Rand_rs }, new { Rand_grp }, new { Axes_Lib_BDraw_tab_delimeted_text }, new { Axes_Lib_BDraw_textInfos }, new { Axes_Lib_BDraw_fontInfos }, new { Axes_Lib_BDraw_ABuff_Bits }, new { Axes_Lib_BDraw_ABuff_Sums }, new { Axes_Lib_BDraw_ABuff_Indexes }, new { Axes_Lib_BDraw_ABuff_grp }, new { Axes_Lib_BDraw_ABuff_grp0 }, new { Axes_Lib_BDraw_ABuff_Fills1 }, new { Axes_Lib_BDraw_ABuff_Fills2 }, new { Axes_Lib_BDraw_fontTexture }, new { _PaletteTex });
    return onRenderObject_GS_N(cpu);
  }
  public virtual void onRenderObject_GS(ref bool render, ref bool cpu)
  {
    Rand_onRenderObject_GS(ref render, ref cpu);
    Axes_Lib_onRenderObject_GS(ref render, ref cpu);
  }
  public virtual float4 frag_GS(v2f i, float4 color) => frag_Axes_Lib_GS(i, color);
  public virtual float4 frag(v2f i)
  {
    float4 color = i.color;
    color = frag_GS(i, color);
    if (color.a == 0) Discard(0);
    return color;
  }
  #region <Rand>
  public uint Rand_Random_uint(uint minu = 0, uint maxu = uint_max) => roundu(UnityEngine.Random.Range((float)minu, (float)maxu));
  public uint4 Rand_Random_uint4(uint a, uint b, uint c, uint d) => uint4(Rand_Random_uint(0, a), Rand_Random_uint(0, b), Rand_Random_uint(0, c), Rand_Random_uint(0, d));
  public uint4 Rand_Random_uint4() => Rand_Random_uint4(330382100u, 1073741822u, 252645134u, 1971u);
  public virtual void Rand_Init(uint _n, uint seed = 0)
  {
    Rand_N = _n;
    if (seed > 0) UnityEngine.Random.InitState((int)seed);
    Rand_seed4 = Random_u4();
    AddComputeBuffer(ref Rand_rs, nameof(Rand_rs), Rand_N);
    Gpu_Rand_initSeed();
    for (Rand_I = 1; Rand_I < Rand_N; Rand_I *= 2) for (Rand_J = 0; Rand_J < 4; Rand_J++) Gpu_Rand_initState();
  }
  public virtual void Rand_initSeed_GS(uint3 id) { uint i = id.x; Rand_rs[i] = i == 0 ? Rand_seed4 : u0000; }
  public virtual void Rand_initState_GS(uint3 id) { uint i = id.x + Rand_I; if (i < Rand_N) Rand_rs[i] = index(Rand_rs[i], Rand_J, Rand_UInt(id.x, 0, uint_max)); }
  protected uint Rand_u(uint a, int b, int c, int d, uint e) => ((a & e) << d) ^ (((a << b) ^ a) >> c);
  protected uint4 Rand_U4(uint4 r) => uint4(Rand_u(r.x, 13, 19, 12, 4294967294u), Rand_u(r.y, 2, 25, 4, 4294967288u), Rand_u(r.z, 3, 11, 17, 4294967280u), r.w * 1664525 + 1013904223u);
  protected uint Rand_UV(uint4 r) => cxor(r);
  protected float Rand_FV(uint4 r) => 2.3283064365387e-10f * Rand_UV(r);
  public uint4 Rand_rUInt4(uint i) => Rand_U4(Rand_rs[i]);
  public uint4 Rand_UInt4(uint i) => Rand_rs[i] = Rand_rUInt4(i);
  public float Rand_rFloat(uint i) => Rand_FV(Rand_rUInt4(i));
  public float Rand_rFloat(uint i, float a, float b) => lerp(a, b, Rand_rFloat(i));
  public float Rand_Float(uint i) => Rand_FV(Rand_UInt4(i));
  public float Rand_Float(uint i, float A, float B) => lerp(A, B, Rand_Float(i));
  public int Rand_Int(uint i, int A, int B) => floori(Rand_Float(i, A, B));
  public int Rand_Int(uint i) => Rand_Int(i, int_min, int_max);
  public uint Rand_UInt(uint i, uint A, uint B) => flooru(Rand_Float(i, A, B));
  public uint Rand_UInt(uint i) => Rand_UV(Rand_UInt4(i));
  protected float3 Rand_onSphere_(float a, float b) => rotateX(rotateZ(f100, acos(a * 2 - 1)), b * TwoPI);
  protected float3 Rand_onSphere_(uint i) { uint j = i * 2; return Rand_onSphere_(Rand_Float(j), Rand_Float(j + 1)); }
  protected float3 Rand_onCircle_(float a) => rotateZ(f100, a * TwoPI);
  public float3 Rand_onSphere(uint i) { uint j = i * 2; return Rand_onSphere_(Rand_Float(j), Rand_Float(j + 1)); }
  public float3 Rand_inSphere(uint i) { uint j = i * 3; return pow(Rand_Float(j + 2), 0.3333333f) * Rand_onSphere_(Rand_Float(j), Rand_Float(j + 1)); }
  public float3 Rand_onCircle(uint i) => Rand_onCircle_(Rand_Float(i));
  public float3 Rand_inCircle(uint i) { uint j = i * 2; return Rand_onCircle_(Rand_Float(j)) * sqrt(Rand_Float(j + 1)); }
  public float3 Rand_inCube(uint i) { uint j = i * 3; return float3(Rand_Float(j), Rand_Float(j + 1), Rand_Float(j + 2)); }
  public float Rand_gauss(uint i) { uint j = i * 2; return sqrt(-2 * ln(1 - Rand_Float(j))) * cos(TwoPI * (1 - Rand_Float(j + 1))); }
  public float Rand_gauss(uint i, float mean, float standardDeviation) => standardDeviation * Rand_gauss(i) + mean;
  public float Rand_exponential(uint i) => -log(Rand_Float(i));
  public float Rand_exponential(uint i, float mean) => mean * Rand_exponential(i);

  public virtual void base_Rand_Start0_GS() { }
  public virtual void base_Rand_Start1_GS() { }
  public virtual void base_Rand_OnApplicationQuit() { Save_UI(); OnApplicationQuit_GS(); base.OnApplicationQuit(); already_quited = true; }
  public virtual void base_Rand_LateUpdate0_GS() { }
  public virtual void base_Rand_LateUpdate1_GS() { }
  public virtual void base_Rand_Update0_GS() { }
  public virtual void base_Rand_Update1_GS() { }
  public virtual void base_Rand_OnValueChanged_GS() { }
  public virtual void base_Rand_InitBuffers0_GS() { }
  public virtual void base_Rand_InitBuffers1_GS() { }
  [HideInInspector]
  public virtual void base_Rand_onRenderObject_GS(ref bool render, ref bool cpu) { }
  public virtual float4 base_frag_Rand_GS(v2f i, float4 color) { return color; }
  public virtual void Rand_InitBuffers0_GS() { }
  public virtual void Rand_InitBuffers1_GS() { }
  public virtual void Rand_LateUpdate0_GS() { }
  public virtual void Rand_LateUpdate1_GS() { }
  public virtual void Rand_Update0_GS() { }
  public virtual void Rand_Update1_GS() { }
  public virtual void Rand_Start0_GS() { }
  public virtual void Rand_Start1_GS() { }
  public virtual void Rand_OnValueChanged_GS() { }
  public virtual void Rand_OnApplicationQuit_GS() { }
  public virtual void Rand_onRenderObject_GS(ref bool render, ref bool cpu) { }
  public virtual float4 frag_Rand_GS(v2f i, float4 color)
  {
    return color;
  }
  #endregion <Rand>
  #region <Axes_Lib>
  public UI_TreeGroup Axes_Lib_ui_Axes_Lib_group_Axes_Lib => UI_Axes_Lib_group_Axes_Lib;
  public IAxes_Lib Axes_Lib_iAxes => lib_parent_gs as IAxes_Lib;
  public virtual void Axes_Lib_LateUpdate0_GS()
  {
    Axes_Lib_showAxes = Axes_Lib_drawGrid && Axes_Lib_drawAxes;
    Axes_Lib_showOutline = Axes_Lib_drawGrid && Axes_Lib_drawBox; Axes_Lib_showNormalizedAxes = Axes_Lib_drawGrid && Axes_Lib_customAxesRangeN > 0;
  }
  public virtual void Axes_Lib_LateUpdate1_GS()
  {
    base_Axes_Lib_LateUpdate1_GS();
    if (!isInitBuffers) return;
    if (Axes_Lib_buildText) Axes_Lib_RebuildText();
    Axes_Lib_buildText = ValuesChanged = gChanged = false;
  }
  public virtual float3 Axes_Lib__axesRangeMin() => Axes_Lib_axesRangeMin / (siUnits ? 1 : 0.3048f);
  public virtual float3 Axes_Lib__axesRangeMax() => Axes_Lib_axesRangeMax / (siUnits ? 1 : 0.3048f);
  public virtual float3 Axes_Lib__axesRangeMin1() => Axes_Lib_axesRangeMin1 / (siUnits ? 1 : 0.3048f);
  public virtual float3 Axes_Lib__axesRangeMax1() => Axes_Lib_axesRangeMax1 / (siUnits ? 1 : 0.3048f);
  public virtual bool Axes_Lib_AddAxes1() { Axes_Lib_BDraw_AddAxes(Axes_Lib_textSize.x, Axes_Lib_textSize.y, Axes_Lib_gridMin(), Axes_Lib_gridMax(), Axes_Lib__axesRangeMin(), Axes_Lib__axesRangeMax(), float4(Axes_Lib_axesColor, Axes_Lib_axesOpacity), Axes_Lib_titles, Axes_Lib_axesFormats); return true; }
  public virtual bool Axes_Lib_AddAxes2() { Axes_Lib_BDraw_AddAxes(Axes_Lib_textSize.x, Axes_Lib_textSize.y, float4(Axes_Lib_axesColor, Axes_Lib_axesOpacity), Axes_Lib_gridMin(), Axes_Lib_gridMax(), Axes_Lib__axesRangeMin(), Axes_Lib__axesRangeMax(), Axes_Lib__axesRangeMin1(), Axes_Lib__axesRangeMax1(), Axes_Lib_titles, Axes_Lib_axesFormats); return true; }
  public virtual bool Axes_Lib_AddAxes3() { Axes_Lib_BDraw_AddAxes(Axes_Lib_textSize.x, Axes_Lib_textSize.y, Axes_Lib_gridMin(), Axes_Lib_gridMax(), float4(Axes_Lib_axesColor, Axes_Lib_axesOpacity), Axes_Lib_titles, Axes_Lib_axesFormats, Axes_Lib_zeroOrigin); return true; }
  public virtual void Axes_Lib_RebuildText()
  {
    Axes_Lib_BDraw_ClearTexts();
    bool r = Axes_Lib_showAxes && Axes_Lib_customAxesRangeN switch { 1 => Axes_Lib_AddAxes1(), 2 => Axes_Lib_AddAxes2(), _ => Axes_Lib_AddAxes3() };
    Axes_Lib_RebuildText_Extra();
    Axes_Lib_BDraw_BuildTexts();
  }
  public virtual void Axes_Lib_RebuildText_Extra() { }
  public virtual void Axes_Lib_InitBuffers0_GS() { base_Axes_Lib_InitBuffers0_GS(); Axes_Lib_buildText = true; }
  public virtual void Axes_Lib_InitBuffers1_GS()
  {
    base_Axes_Lib_InitBuffers1_GS();
    Axes_Lib_RebuildText();
  }
  public float3 Axes_Lib_gridMin() => float3(Axes_Lib_GridX.x, Axes_Lib_GridY.x, Axes_Lib_GridZ.x);
  public float3 Axes_Lib_gridMax() => float3(Axes_Lib_GridX.y, Axes_Lib_GridY.y, Axes_Lib_GridZ.y);
  public float3 Axes_Lib_gridExtent() => Axes_Lib_gridMax() - Axes_Lib_gridMin();
  public float3 Axes_Lib_gridSize() => Axes_Lib_gridMax() - Axes_Lib_gridMin();
  public float3 Axes_Lib_gridCenter() => (Axes_Lib_gridMax() + Axes_Lib_gridMin()) / 2;
  public float2 Axes_Lib_axesRangeX() => float2(Axes_Lib_axesRangeMin.x, Axes_Lib_axesRangeMax.x);
  public float2 Axes_Lib_axesRangeY() => float2(Axes_Lib_axesRangeMin.y, Axes_Lib_axesRangeMax.y);
  public float2 Axes_Lib_axesRangeZ() => float2(Axes_Lib_axesRangeMin.z, Axes_Lib_axesRangeMax.z);
  public void Axes_Lib_axesRangeX(float2 r) { Axes_Lib_axesRangeMin = Axes_Lib_axesRangeMin * f011 + f100 * r.x; Axes_Lib_axesRangeMax = Axes_Lib_axesRangeMax * f011 + f100 * r.y; }
  public void Axes_Lib_axesRangeY(float2 r) { Axes_Lib_axesRangeMin = Axes_Lib_axesRangeMin * f101 + f010 * r.x; Axes_Lib_axesRangeMax = Axes_Lib_axesRangeMax * f101 + f010 * r.y; }
  public void Axes_Lib_axesRangeZ(float2 r) { Axes_Lib_axesRangeMin = Axes_Lib_axesRangeMin * f110 + f001 * r.x; Axes_Lib_axesRangeMax = Axes_Lib_axesRangeMax * f110 + f001 * r.y; }
  public void Axes_Lib_gridMin(float3 v) { Axes_Lib_GridX = float2(v.x, Axes_Lib_GridX.y); Axes_Lib_GridY = float2(v.y, Axes_Lib_GridY.y); Axes_Lib_GridZ = float2(v.z, Axes_Lib_GridZ.y); }
  public void Axes_Lib_gridMax(float3 v) { Axes_Lib_GridX = float2(Axes_Lib_GridX.x, v.x); Axes_Lib_GridY = float2(Axes_Lib_GridY.x, v.y); Axes_Lib_GridZ = float2(Axes_Lib_GridZ.x, v.x); }
  public void Axes_Lib_gridSize(float3 v) { float3 c = Axes_Lib_gridCenter(); v /= 2; Axes_Lib_gridMin(c - v); Axes_Lib_gridMax(c + v); }
  public void Axes_Lib_gridCenter(float3 v) { float3 r = Axes_Lib_gridSize() / 2; Axes_Lib_gridMin(v - r); Axes_Lib_gridMax(v + r); }
  public virtual v2f vert_Axes_Lib_BDraw_Box(uint i, uint j, v2f o) => vert_Axes_Lib_BDraw_BoxFrame(Axes_Lib_gridMin(), Axes_Lib_gridMax(), Axes_Lib_boxLineThickness, DARK_BLUE, i, j, o);
  public virtual float4 frag_Axes_Lib_GS(v2f i, float4 color)
  {
    switch (Axes_Lib_BDraw_o_drawType(i))
    {
      case uint_max: Discard(0); break;
      case Axes_Lib_BDraw_Draw_Sphere: color = frag_Axes_Lib_BDraw_Sphere(i); break;
      case Axes_Lib_BDraw_Draw_Line: color = frag_Axes_Lib_BDraw_Line(i); break;
      case Axes_Lib_BDraw_Draw_Arrow: color = frag_Axes_Lib_BDraw_Arrow(i); break;
      case Axes_Lib_BDraw_Draw_Signal: color = frag_Axes_Lib_BDraw_Signal(i); break;
      case Axes_Lib_BDraw_Draw_LineSegment: color = frag_Axes_Lib_BDraw_LineSegment(i); break;
      case Axes_Lib_BDraw_Draw_Mesh: color = frag_Axes_Lib_BDraw_Mesh(i); break;
      case Axes_Lib_BDraw_Draw_Text3D:
        Axes_Lib_BDraw_TextInfo t = Axes_Lib_BDraw_textInfo(Axes_Lib_BDraw_o_i(i));
        color = frag_Axes_Lib_BDraw_Text(Axes_Lib_BDraw_fontTexture, Axes_Lib_BDraw_tab_delimeted_text, Axes_Lib_BDraw_fontInfos, Axes_Lib_BDraw_fontSize, t.quadType, t.backColor, Axes_Lib_BDraw_Get_text_indexes(t.textI), i);
        break;
    }
    return color;
  }

  public virtual void base_Axes_Lib_Start0_GS()
  {
    Axes_Lib_BDraw_Start0_GS();
  }
  public virtual void base_Axes_Lib_Start1_GS()
  {
    Axes_Lib_BDraw_Start1_GS();
  }
  public virtual void base_Axes_Lib_OnApplicationQuit() { Save_UI(); OnApplicationQuit_GS(); base.OnApplicationQuit(); already_quited = true; }
  public virtual void base_Axes_Lib_LateUpdate0_GS()
  {
    Axes_Lib_BDraw_LateUpdate0_GS();
  }
  public virtual void base_Axes_Lib_LateUpdate1_GS()
  {
    Axes_Lib_BDraw_LateUpdate1_GS();
  }
  public virtual void base_Axes_Lib_Update0_GS()
  {
    Axes_Lib_BDraw_Update0_GS();
  }
  public virtual void base_Axes_Lib_Update1_GS()
  {
    Axes_Lib_BDraw_Update1_GS();
  }
  public virtual void base_Axes_Lib_OnValueChanged_GS()
  {
    Axes_Lib_BDraw_OnValueChanged_GS();
    var type = "gsReport_Lib".ToType();
    if (type != null) ((GS)GetComponent(type))?.OnValueChanged_GS();
  }
  public virtual void base_Axes_Lib_InitBuffers0_GS()
  {
    Axes_Lib_BDraw_InitBuffers0_GS();
  }
  public virtual void base_Axes_Lib_InitBuffers1_GS()
  {
    Axes_Lib_BDraw_InitBuffers1_GS();
  }
  [HideInInspector]
  public virtual void base_Axes_Lib_onRenderObject_GS(ref bool render, ref bool cpu)
  {
    Axes_Lib_BDraw_onRenderObject_GS(ref render, ref cpu);
  }
  public virtual float4 base_frag_Axes_Lib_GS(v2f i, float4 color) { return frag_Axes_Lib_BDraw_GS(i, color); }
  public float Axes_Lib_BDraw_wrapJ(uint j, uint n) => ((j + n) % 6) / 3;
  public uint2 Axes_Lib_BDraw_JQuadu(uint j) => uint2(j + 2, j + 1) / 3 % 2;
  public float2 Axes_Lib_BDraw_JQuadf(uint j) => (float2)Axes_Lib_BDraw_JQuadu(j);
  public float4 Axes_Lib_BDraw_Number_quadPoint(float rx, float ry, uint j) { float2 p = Axes_Lib_BDraw_JQuadf(j); return float4((2 * p.x - 1) * rx, (1 - 2 * p.y) * ry, 0, 0); }
  public float4 Axes_Lib_BDraw_Sphere_quadPoint(float r, uint j) => r * float4(2 * Axes_Lib_BDraw_JQuadf(j) - 1, 0, 0);
  public float2 Axes_Lib_BDraw_Line_uv(float3 p0, float3 p1, float r, uint j) { float2 p = Axes_Lib_BDraw_JQuadf(j); return float2(length(p1 - p0) * (1 - p.y), (1 - 2 * p.x) * r); }
  public float2 Axes_Lib_BDraw_LineArrow_uv(float dpf, float3 p0, float3 p1, float r, uint j) { float2 p = Axes_Lib_BDraw_JQuadf(j); return float2((length(p1 - p0) + 2 * r) * (1 - p.y) - r, (1 - 2 * p.x) * r * dpf); }
  public float4 Axes_Lib_BDraw_LineArrow_p4(float dpf, float3 p0, float3 p1, float3 p3, float r, uint j) { float2 p = Axes_Lib_BDraw_JQuadf(j); float3 dp = normalize(cross(p1 - p0, p3 - p0)) * r * dpf; return float4(p.y * (p0 - p1) + p1 + dp * (1 - 2 * p.x), 1); }
  public float4 Axes_Lib_BDraw_LineArrow_p4(float dpf, float3 p0, float3 p1, float r, uint j) => Axes_Lib_BDraw_LineArrow_p4(dpf, p0, p1, _WorldSpaceCameraPos, r, j);
  public uint Axes_Lib_BDraw_o_i(v2f o) => roundu(o.ti.x);
  public v2f Axes_Lib_BDraw_o_i(uint i, v2f o) { o.ti.x = i; return o; }
  public uint Axes_Lib_BDraw_o_j(v2f o) => roundu(o.ti.y);
  public v2f Axes_Lib_BDraw_o_j(uint j, v2f o) { o.ti.y = j; return o; }
  public uint Axes_Lib_BDraw_o_drawType(v2f o) => roundu(o.ti.z);
  public v2f Axes_Lib_BDraw_o_drawType(uint drawType, v2f o) { o.ti.z = drawType; return o; }
  public float4 Axes_Lib_BDraw_o_color(v2f o) => o.color;
  public v2f Axes_Lib_BDraw_o_color(float4 color, v2f o) { o.color = color; return o; }
  public float3 Axes_Lib_BDraw_o_normal(v2f o) => o.normal;
  public v2f Axes_Lib_BDraw_o_normal(float3 normal, v2f o) { o.normal = normal; return o; }
  public float2 Axes_Lib_BDraw_o_uv(v2f o) => o.uv;
  public v2f Axes_Lib_BDraw_o_uv(float2 uv, v2f o) { o.uv = uv; return o; }
  public float4 Axes_Lib_BDraw_o_pos(v2f o) => o.pos;
  public v2f Axes_Lib_BDraw_o_pos(float4 pos, v2f o) { o.pos = pos; return o; }
  public v2f Axes_Lib_BDraw_o_pos_PV(float3 p, float4 q, v2f o) => Axes_Lib_BDraw_o_pos(mul(UNITY_MATRIX_P, mul(UNITY_MATRIX_V, float4(p, 1)) + q), o);
  public v2f Axes_Lib_BDraw_o_pos_c(float4 c, v2f o) => Axes_Lib_BDraw_o_pos(UnityObjectToClipPos(c), o);
  public v2f Axes_Lib_BDraw_o_pos_c(float3 c, v2f o) => Axes_Lib_BDraw_o_pos(UnityObjectToClipPos(c), o);
  public float3 Axes_Lib_BDraw_o_wPos(v2f o) => o.wPos;
  public v2f Axes_Lib_BDraw_o_wPos(float3 wPos, v2f o) { o.wPos = wPos; return o; }
  public float3 Axes_Lib_BDraw_o_p0(v2f o) => o.p0;
  public v2f Axes_Lib_BDraw_o_p0(float3 p0, v2f o) { o.p0 = p0; return o; }
  public float3 Axes_Lib_BDraw_o_p1(v2f o) => o.p1;
  public v2f Axes_Lib_BDraw_o_p1(float3 p1, v2f o) { o.p1 = p1; return o; }
  public float Axes_Lib_BDraw_o_r(v2f o) => o.ti.w;
  public v2f Axes_Lib_BDraw_o_r(float r, v2f o) { o.ti.w = r; return o; }
  public float3 Axes_Lib_BDraw_quad(float3 p0, float3 p1, float3 p2, float3 p3, uint j) => j % 5 == 0 ? p3 : j == 1 ? p2 : j == 4 ? p0 : p1;
  public float4 Axes_Lib_BDraw_o_ti(v2f o) => o.ti;
  public v2f Axes_Lib_BDraw_o_ti(float4 ti, v2f o) { o.ti = ti; return o; }
  public float4 Axes_Lib_BDraw_o_tj(v2f o) => o.tj;
  public v2f Axes_Lib_BDraw_o_tj(float4 tj, v2f o) { o.tj = tj; return o; }
  public float4 Axes_Lib_BDraw_o_tk(v2f o) => o.tk;
  public v2f Axes_Lib_BDraw_o_tk(float4 tk, v2f o) { o.tk = tk; return o; }
  public v2f Axes_Lib_BDraw_o_zero() => Axes_Lib_BDraw_o_pos(f0000, Axes_Lib_BDraw_o_color(f0000, Axes_Lib_BDraw_o_ti(f0000, Axes_Lib_BDraw_o_tj(f0000, Axes_Lib_BDraw_o_tk(f0000, Axes_Lib_BDraw_o_normal(f000, Axes_Lib_BDraw_o_p0(f000, Axes_Lib_BDraw_o_p1(f000, Axes_Lib_BDraw_o_wPos(f000, Axes_Lib_BDraw_o_uv(f00, default))))))))));
  public v2f vert_Axes_Lib_BDraw_Point(float3 p, float4 color, uint i, v2f o) => Axes_Lib_BDraw_o_i(i, Axes_Lib_BDraw_o_drawType(Axes_Lib_BDraw_Draw_Point, Axes_Lib_BDraw_o_color(color, Axes_Lib_BDraw_o_pos(UnityObjectToClipPos(float4(p, 1)), o))));
  public v2f vert_Axes_Lib_BDraw_Sphere(float3 p, float r, float4 color, uint i, uint j, v2f o) { float4 q = Axes_Lib_BDraw_Sphere_quadPoint(r, j); return Axes_Lib_BDraw_o_i(i, Axes_Lib_BDraw_o_drawType(Axes_Lib_BDraw_Draw_Sphere, Axes_Lib_BDraw_o_color(color, Axes_Lib_BDraw_o_normal(-f001, Axes_Lib_BDraw_o_uv(q.xy / r, Axes_Lib_BDraw_o_pos_PV(p, q, Axes_Lib_BDraw_o_wPos(p, o))))))); }
  public v2f vert_Axes_Lib_BDraw_LineArrow(float dpf, float3 p0, float3 p1, float r, float4 color, uint i, uint j, v2f o) => Axes_Lib_BDraw_o_i(i, Axes_Lib_BDraw_o_p0(p0, Axes_Lib_BDraw_o_p1(p1, Axes_Lib_BDraw_o_r(r, Axes_Lib_BDraw_o_drawType(dpf == 1 ? Axes_Lib_BDraw_Draw_Line : Axes_Lib_BDraw_Draw_Arrow, Axes_Lib_BDraw_o_color(color, Axes_Lib_BDraw_o_uv(Axes_Lib_BDraw_LineArrow_uv(dpf, p0, p1, r, j), Axes_Lib_BDraw_o_pos_c(Axes_Lib_BDraw_LineArrow_p4(dpf, p0, p1, r, j), o))))))));
  public v2f vert_Axes_Lib_BDraw_Line(float3 p0, float3 p1, float r, float4 color, uint i, uint j, v2f o) => Axes_Lib_BDraw_o_i(i, Axes_Lib_BDraw_o_p0(p0, Axes_Lib_BDraw_o_p1(p1, Axes_Lib_BDraw_o_r(r, Axes_Lib_BDraw_o_drawType(Axes_Lib_BDraw_Draw_Line, Axes_Lib_BDraw_o_color(color, Axes_Lib_BDraw_o_uv(Axes_Lib_BDraw_Line_uv(p0, p1, r, j), Axes_Lib_BDraw_o_pos_c(Axes_Lib_BDraw_LineArrow_p4(1, p0, p1, r, j), o))))))));
  public v2f vert_Axes_Lib_BDraw_LineSegment(float3 p0, float3 p1, float r, float4 color, uint i, uint j, v2f o) => Axes_Lib_BDraw_o_drawType(Axes_Lib_BDraw_Draw_LineSegment, vert_Axes_Lib_BDraw_LineArrow(1, p0, p1, r, color, i, j, o));
  public v2f vert_Axes_Lib_BDraw_Arrow(float3 p0, float3 p1, float r, float4 color, uint i, uint j, v2f o) => vert_Axes_Lib_BDraw_LineArrow(3, p0, p1, r, color, i, j, o);
  public v2f vert_Axes_Lib_BDraw_BoxFrame(float3 c0, float3 c1, float lineRadius, float4 color, uint i, uint j, v2f o) { float3 p0, p1; switch (i) { case 0: p0 = c0; p1 = c0 * f110 + c1 * f001; break; case 1: p0 = c0 * f110 + c1 * f001; p1 = c0 * f100 + c1 * f011; break; case 2: p0 = c0 * f100 + c1 * f011; p1 = c0 * f101 + c1 * f010; break; case 3: p0 = c0 * f101 + c1 * f010; p1 = c0; break; case 4: p0 = c0 * f011 + c1 * f100; p1 = c0 * f010 + c1 * f101; break; case 5: p0 = c0 * f010 + c1 * f101; p1 = c1; break; case 6: p0 = c1; p1 = c0 * f001 + c1 * f110; break; case 7: p0 = c0 * f001 + c1 * f110; p1 = c0 * f011 + c1 * f100; break; case 8: p0 = c0; p1 = c0 * f011 + c1 * f100; break; case 9: p0 = c0 * f101 + c1 * f010; p1 = c0 * f001 + c1 * f110; break; case 10: p0 = c0 * f100 + c1 * f011; p1 = c1; break; default: p0 = c0 * f110 + c1 * f001; p1 = c0 * f010 + c1 * f101; break; } return vert_Axes_Lib_BDraw_Line(p0, p1, lineRadius, color, i, j, o); }
  public v2f vert_Axes_Lib_BDraw_Quad(float3 p0, float3 p1, float3 p2, float3 p3, float4 color, uint i, uint j, v2f o) { float3 p = Axes_Lib_BDraw_quad(p0, p1, p2, p3, j); return Axes_Lib_BDraw_o_i(i, Axes_Lib_BDraw_o_drawType(Axes_Lib_BDraw_Draw_Texture_2D, Axes_Lib_BDraw_o_normal(cross(p1 - p0, p0 - p3), Axes_Lib_BDraw_o_uv(float2(Axes_Lib_BDraw_wrapJ(j, 2), Axes_Lib_BDraw_wrapJ(j, 4)), Axes_Lib_BDraw_o_wPos(p, Axes_Lib_BDraw_o_pos_c(p, Axes_Lib_BDraw_o_color(color, o))))))); }
  public v2f vert_Axes_Lib_BDraw_Legend(uint i, uint j, v2f o) { float h = 8; float3 c = f110 * 10000, p0 = c + float3(0.4f, -h / 2, 0), p1 = p0 + f100 * 0.4f, p2 = p1 + h * f010, p3 = p0 + h * f010; return vert_Axes_Lib_BDraw_Quad(p0, p1, p2, p3, WHITE, i, j, o); }
  public v2f vert_Axes_Lib_BDraw_WebCam(float3 p0, float3 p1, float3 p2, float3 p3, float4 color, uint i, uint j, v2f o) => Axes_Lib_BDraw_o_drawType(Axes_Lib_BDraw_Draw_WebCam, vert_Axes_Lib_BDraw_Quad(p0, p1, p2, p3, color, i, j, o));
  public v2f vert_Axes_Lib_BDraw_Cube(float3 p, float3 r, float4 color, uint i, uint j, v2f o) { float3 p0, p1, p2, p3; switch (i % 6) { case 0: p0 = f___; p1 = f1__; p2 = f11_; p3 = f_1_; break; case 1: p0 = f1_1; p1 = f__1; p2 = f_11; p3 = f111; break; case 2: p0 = f__1; p1 = f1_1; p2 = f1__; p3 = f___; break; case 3: p0 = f_1_; p1 = f11_; p2 = f111; p3 = f_11; break; case 4: p0 = f__1; p1 = f___; p2 = f_1_; p3 = f_11; break; default: p0 = f1__; p1 = f1_1; p2 = f111; p3 = f11_; break; } return vert_Axes_Lib_BDraw_Quad(p0 * r + p, p1 * r + p, p2 * r + p, p3 * r + p, color, i, j, o); }
  public v2f vert_Axes_Lib_BDraw_Cube(float3 p, float r, float4 color, uint i, uint j, v2f o) => vert_Axes_Lib_BDraw_Cube(p, f111 * r, color, i, j, o);
  public virtual bool Axes_Lib_BDraw_SignalQuad(uint chI) => false;
  public virtual float3 Axes_Lib_BDraw_SignalQuad_Min(uint chI) => f000;
  public virtual float3 Axes_Lib_BDraw_SignalQuad_Size(uint chI) => f111;
  public virtual float3 Axes_Lib_BDraw_SignalQuad_p0(uint chI) => Axes_Lib_BDraw_SignalQuad_Min(chI);
  public virtual float3 Axes_Lib_BDraw_SignalQuad_p1(uint chI) => Axes_Lib_BDraw_SignalQuad_p0(chI) + Axes_Lib_BDraw_SignalQuad_Size(chI) * f100;
  public virtual float3 Axes_Lib_BDraw_SignalQuad_p2(uint chI) => Axes_Lib_BDraw_SignalQuad_p0(chI) + Axes_Lib_BDraw_SignalQuad_Size(chI) * f110;
  public virtual float3 Axes_Lib_BDraw_SignalQuad_p3(uint chI) => Axes_Lib_BDraw_SignalQuad_p0(chI) + Axes_Lib_BDraw_SignalQuad_Size(chI) * f010;
  public virtual v2f vert_Axes_Lib_BDraw_Signal(float3 p0, float3 p1, float r, uint i, uint j, v2f o)
  {
    if (!Axes_Lib_BDraw_SignalQuad(i)) return Axes_Lib_BDraw_o_i(i, Axes_Lib_BDraw_o_p0(p0, Axes_Lib_BDraw_o_p1(p1, Axes_Lib_BDraw_o_uv(f11 - Axes_Lib_BDraw_JQuadf(j).yx, Axes_Lib_BDraw_o_drawType(Axes_Lib_BDraw_Draw_Signal, Axes_Lib_BDraw_o_r(r, Axes_Lib_BDraw_o_pos_c(Axes_Lib_BDraw_LineArrow_p4(1, p0, p1, r, j), o)))))));
    float3 q0 = Axes_Lib_BDraw_SignalQuad_p0(i), q1 = Axes_Lib_BDraw_SignalQuad_p1(i), q2 = Axes_Lib_BDraw_SignalQuad_p2(i), q3 = Axes_Lib_BDraw_SignalQuad_p3(i);
    return Axes_Lib_BDraw_o_p0(p0, Axes_Lib_BDraw_o_p1(p1, Axes_Lib_BDraw_o_r(distance(q0, q3), Axes_Lib_BDraw_o_drawType(Axes_Lib_BDraw_Draw_Signal, vert_Axes_Lib_BDraw_Quad(q0, q1, q2, q3, f1111, i, j, o)))));
  }
  public virtual v2f vert_Axes_Lib_BDraw_Signal(float3 p0, float3 p1, float r, float4 color, int drawType, float thickness, uint i, uint j, v2f o) => Axes_Lib_BDraw_o_tj(float4(distance(p0, p1), r, drawType, thickness), Axes_Lib_BDraw_o_color(color, vert_Axes_Lib_BDraw_Signal(p0, p1, r, i, j, o)));
  public virtual uint Axes_Lib_BDraw_SignalSmpN(uint chI) => 1024;
  public virtual float4 Axes_Lib_BDraw_SignalColor(uint chI, uint smpI) => YELLOW;
  public virtual float4 Axes_Lib_BDraw_SignalBackColor(uint chI, uint smpI) => f0000;
  public virtual float Axes_Lib_BDraw_SignalSmpV(uint chI, uint smpI) => 0;
  public virtual float Axes_Lib_BDraw_SignalThickness(uint chI, uint smpI) => 0.004f;
  public virtual float Axes_Lib_BDraw_SignalFillCrest(uint chI, uint smpI) => 1;
  public virtual bool Axes_Lib_BDraw_SignalMarkerColor(uint stationI, float station_smpI, float4 color, uint chI, float smpI, uint display_x, out float4 return_color)
  {
    float d = abs(smpI - station_smpI + display_x);
    return (return_color = chI == stationI && d < 1 ? float4(color.xyz * (1 - d), 1) : f0000).w > 0;
  }
  public virtual float4 Axes_Lib_BDraw_SignalMarker(uint chI, float smpI) => f0000;
  public virtual float4 frag_Axes_Lib_BDraw_Signal(v2f i)
  {
    uint chI = Axes_Lib_BDraw_o_i(i), SmpN = Axes_Lib_BDraw_SignalSmpN(chI);
    float2 uv = i.uv, wh = float2(distance(i.p1, i.p0), Axes_Lib_BDraw_o_r(i));
    float smpI = lerp(0, SmpN, uv.x), y = lerp(-1, 1, uv.y), h = wh.y / wh.x * SmpN, thick = Axes_Lib_BDraw_SignalThickness(chI, (uint)smpI) * SmpN, d = float_PositiveInfinity;
    uint SmpI = (uint)smpI, dSmpI = ceilu(thick) + 1, SmpI0 = (uint)max(0, (int)SmpI - (int)dSmpI), SmpI1 = min(SmpN - 1, SmpI + dSmpI);
    float2 p0 = float2(smpI, y * h), q0 = float2(SmpI0, (h - thick) * Axes_Lib_BDraw_SignalSmpV(chI, SmpI0)), q1;
    for (uint sI = SmpI0; sI < SmpI1; sI++) { q1 = float2(sI + 1, (h - thick) * Axes_Lib_BDraw_SignalSmpV(chI, sI + 1)); d = min(d, LineSegDist(q0, q1, p0)); q0 = q1; }
    float4 c = Axes_Lib_BDraw_SignalColor(chI, SmpI);
    float v = 0.9f * lerp(Axes_Lib_BDraw_SignalSmpV(chI, SmpI), Axes_Lib_BDraw_SignalSmpV(chI, SmpI + 1), frac(smpI)), crest = Axes_Lib_BDraw_SignalFillCrest(chI, SmpI);
    float4 marker = Axes_Lib_BDraw_SignalMarker(chI, smpI);
    if (marker.w > 0) return marker;
    if (crest >= 0 ? y > crest && y < v : y < crest && y > v) return c;
    if (d < thick) return float4(c.xyz * (1 - d / thick), c.w);
    return Axes_Lib_BDraw_SignalBackColor(chI, SmpI);
  }
  public float4 frag_Axes_Lib_BDraw_Sphere(v2f i) { float2 uv = i.uv; float r = dot(uv, uv); float4 color = i.color; if (r > 1.0f || color.a == 0) return f0000; float3 n = new float3(uv, r - 1), _LightDir = new float3(0.321f, 0.766f, -0.557f); float lightAmp = max(0.0f, dot(n, _LightDir)); float4 diffuse_Light = (lightAmp + UNITY_LIGHTMODEL_AMBIENT) * color; float spec = max(0, (lightAmp - 0.95f) / 0.05f); color = lerp(diffuse_Light, f1111, spec / 4); color.a = 1; return color; }
  public float4 frag_Axes_Lib_BDraw_Line(v2f i) { float3 p0 = i.p0, p1 = i.p1; float lineRadius = Axes_Lib_BDraw_o_r(i); float2 uv = i.uv; float r = dot(uv, uv), r2 = lineRadius * lineRadius; float4 color = i.color; float3 p10 = p1 - p0; float lp10 = length(p10); if (uv.x < 0) r /= r2; else if (uv.x > lp10) { uv.x -= lp10; r = dot(uv, uv) / r2; } else { uv.x = 0; r = dot(uv, uv) / r2; } if (r > 1.0f || color.a == 0) return f0000; float3 n = new float3(uv, r - 1), _LightDir = new float3(0.321f, 0.766f, -0.557f); float lightAmp = max(0.0f, dot(n, _LightDir)); float4 diffuse_Light = (lightAmp + UNITY_LIGHTMODEL_AMBIENT) * color; float spec = max(0, (lightAmp - 0.95f) / 0.05f); color = lerp(diffuse_Light, f1111, spec / 4); color.a = 1; return color; }
  public float4 frag_Axes_Lib_BDraw_Arrow(v2f i) { float3 p0 = i.p0, p1 = i.p1; float lineRadius = Axes_Lib_BDraw_o_r(i); float2 uv = i.uv; float r = dot(uv, uv), r2 = lineRadius * lineRadius; float4 color = i.color; float3 p10 = p1 - p0; float lp10 = length(p10); if (uv.x < 0) r /= r2; else if (uv.x > lp10 - lineRadius * 3 && abs(uv.y) > lineRadius) { uv.x -= lp10; uv = rotate_sc(uv, -sign(uv.y) * 0.5f, 0.866025404f); uv.x = 0; r = dot(uv, uv) / r2; } else if (uv.x > lp10) { uv.x -= lp10; r = dot(uv, uv) / r2; } else { uv.x = 0; r = dot(uv, uv) / r2; } if (r > 1.0f || color.a == 0) return f0000; float3 n = new float3(uv, r - 1), _LightDir = new float3(0.321f, 0.766f, -0.557f); float lightAmp = max(0.0f, dot(n, _LightDir)); float4 diffuse_Light = (lightAmp + UNITY_LIGHTMODEL_AMBIENT) * color; float spec = max(0, (lightAmp - 0.95f) / 0.05f); color = lerp(diffuse_Light, f1111, spec / 4); color.a = 1; return color; }
  public float4 frag_Axes_Lib_BDraw_LineSegment(v2f i) { float3 p0 = i.p0, p1 = i.p1; float lineRadius = Axes_Lib_BDraw_o_r(i); float2 uv = i.uv; float r = dot(uv, uv), r2 = lineRadius * lineRadius; float4 color = i.color; float3 p10 = p1 - p0; float lp10 = length(p10); uv.x = 0; r = dot(uv, uv) / r2; if (r > 1.0f || color.a == 0) return f0000; float3 n = new float3(uv, r - 1), _LightDir = new float3(0.321f, 0.766f, -0.557f); float lightAmp = max(0.0f, dot(n, _LightDir)); float4 diffuse_Light = (lightAmp + UNITY_LIGHTMODEL_AMBIENT) * color; float spec = max(0, (lightAmp - 0.95f) / 0.05f); color = lerp(diffuse_Light, f1111, spec / 4); color.a = 1; return color; }
  public float4 frag_Axes_Lib_BDraw_Quad(Texture2D t, v2f i) => i.color * tex2Dlod(t, new float4(i.uv, f00));
  public float4 frag_Axes_Lib_BDraw_Quad(RWStructuredBuffer<Color> v, v2f i, uint2 _image_size) => (float4)(i.color * v[id_to_i((uint2)(i.uv * (float2)_image_size), _image_size)]);
  public float4 frag_Axes_Lib_BDraw_Quad(RWStructuredBuffer<Color32> v, v2f i, uint2 _image_size) => (float4)(i.color * c32_f4(v[id_to_i((uint2)(i.uv * (float2)_image_size), _image_size)]));
  public float4 frag_Axes_Lib_BDraw_Mesh(v2f i) { float3 p = i.wPos; float4 color = i.color; color.xyz += dot(i.normal, _WorldSpaceLightPos0.xyz) / 2; return saturate(color); }
  public virtual bool Axes_Lib_BDraw_ABuff_IsBitOn(uint i) { uint c = Axes_Lib_BDraw_Byte(i); return c == Axes_Lib_BDraw_TB || c == Axes_Lib_BDraw_LF; }
  public class Axes_Lib_BDraw_TText3D
  {
    public string text; public float3 p, right, up, p0, p1; public float h; public float4 color, backColor;
    public Axes_Lib_BDraw_Text_QuadType quadType; public Axes_Lib_BDraw_TextAlignment textAlignment; public uint axis;
  }
  public Font Axes_Lib_BDraw_font { get; set; }
  public virtual void Axes_Lib_BDraw_InitBuffers0_GS()
  {
    if (Axes_Lib_BDraw_omitText) Axes_Lib_BDraw_fontInfoN = 0;
    else { Axes_Lib_BDraw_font ??= Resources.Load<Font>("Arial Font/arial Unicode"); Axes_Lib_BDraw_fontTexture = (Texture2D)Axes_Lib_BDraw_font.material.mainTexture; Axes_Lib_BDraw_fontInfoN = Axes_Lib_BDraw_includeUnicode ? Axes_Lib_BDraw_font.characterInfo.uLength() : 128 - 32; }
  }
  public virtual void Axes_Lib_BDraw_InitBuffers1_GS()
  {
    for (int i = 0; i < Axes_Lib_BDraw_fontInfoN; i++)
    {
      var c = Axes_Lib_BDraw_font.characterInfo[i];
      if (i == 0) Axes_Lib_BDraw_fontSize = c.size;
      if (c.index < 128) Axes_Lib_BDraw_fontInfos[c.index - 32] = new Axes_Lib_BDraw_FontInfo() { uvBottomLeft = c.uvBottomLeft, uvBottomRight = c.uvBottomRight, uvTopLeft = c.uvTopLeft, uvTopRight = c.uvTopRight, advance = max(c.advance, roundi(c.glyphWidth * 1.05f)), bearing = c.bearing, minX = c.minX, minY = c.minY, maxX = c.maxX, maxY = c.maxY };
    }
    Axes_Lib_BDraw_fontInfos.SetData();
  }
  public float Axes_Lib_BDraw_GetTextHeight() => 0.1f;
  public uint Axes_Lib_BDraw_GetText_ch(float v, uint _I, uint neg, uint uN) => _I < neg ? 13u : _I < uN + neg ? 16u + flooru(10 * frac(abs(v) / pow10(uN - _I + neg))) : _I == uN + neg ? 14u : 16u + flooru(10 * frac(abs(v) * pow10(_I - neg - uN - 1)));
  public uint Axes_Lib_BDraw_Byte(uint i) => TextByte(Axes_Lib_BDraw_tab_delimeted_text, i);
  public uint2 Axes_Lib_BDraw_Get_text_indexes(uint textI) => uint2(textI == 0 ? 0 : Axes_Lib_BDraw_ABuff_Indexes[textI - 1] + 1, textI < Axes_Lib_BDraw_ABuff_IndexN ? Axes_Lib_BDraw_ABuff_Indexes[textI] : Axes_Lib_BDraw_textCharN);
  public float Axes_Lib_BDraw_GetTextWidth(float v, uint decimalN)
  {
    float textWidth = 0, p10 = pow10(decimalN);
    v = round(v * p10) / p10;
    uint u = flooru(abs(v)), uN = u == 0 ? 1 : flooru(log10(abs(v)) + 1), numDigits = uN + decimalN + (decimalN == 0 ? 0 : 1u), neg = v < 0 ? 1u : 0;
    for (uint _I = 0; _I < numDigits + neg; _I++)
    {
      uint ch = Axes_Lib_BDraw_GetText_ch(v, _I, neg, uN);
      Axes_Lib_BDraw_FontInfo f = Axes_Lib_BDraw_fontInfos[ch];
      float2 mn = new float2(f.minX, f.minY) / Axes_Lib_BDraw_fontSize, mx = new float2(f.maxX, f.maxY) / Axes_Lib_BDraw_fontSize, range = mx - mn;
      float dx = f.advance / Axes_Lib_BDraw_fontSize;
      textWidth += dx;
    }
    return textWidth;
  }
  public float3 Axes_Lib_BDraw_GetTextWidth(float3 v, uint3 decimalN) => new float3(Axes_Lib_BDraw_GetTextWidth(v.x, decimalN.x), Axes_Lib_BDraw_GetTextWidth(v.y, decimalN.y), Axes_Lib_BDraw_GetTextWidth(v.z, decimalN.z));
  public List<Axes_Lib_BDraw_TText3D> Axes_Lib_BDraw_texts = new List<Axes_Lib_BDraw_TText3D>();
  public void Axes_Lib_BDraw_ClearTexts() => Axes_Lib_BDraw_texts.Clear();
  public virtual void Axes_Lib_BDraw_AddText(string text, float3 p, float3 right, float3 up, float4 color, float4 backColor, float h, Axes_Lib_BDraw_Text_QuadType quadType, Axes_Lib_BDraw_TextAlignment textAlignment, float3 p0, float3 p1, uint axis = 0) => Axes_Lib_BDraw_texts.Add(new Axes_Lib_BDraw_TText3D() { text = text, p = p, right = right, up = up, color = color, backColor = backColor, h = h, quadType = quadType, textAlignment = textAlignment, p0 = p0, p1 = p1, axis = axis });
  public void Axes_Lib_BDraw_AddText(string text, float3 p, float3 right, float3 up, float4 color, float4 backColor, float h, Axes_Lib_BDraw_Text_QuadType quadType, Axes_Lib_BDraw_TextAlignment textAlignment) => Axes_Lib_BDraw_AddText(text, p, right, up, color, backColor, h, quadType, textAlignment, f000, f000, 0);
  public virtual Axes_Lib_BDraw_TextInfo Axes_Lib_BDraw_textInfo(uint i) => Axes_Lib_BDraw_textInfos[i];
  public virtual void Axes_Lib_BDraw_textInfo(uint i, Axes_Lib_BDraw_TextInfo t) => Axes_Lib_BDraw_textInfos[i] = t;
  public int Axes_Lib_BDraw_ExtraTextN = 0;
  public virtual void Axes_Lib_BDraw_RebuildExtraTexts() { Axes_Lib_BDraw_BuildTexts(); Axes_Lib_BDraw_BuildTexts(); }
  public virtual void Axes_Lib_BDraw_BuildExtraTexts() { }
  public virtual void Axes_Lib_BDraw_BuildTexts()
  {
    SetBytes(ref Axes_Lib_BDraw_tab_delimeted_text, (Axes_Lib_BDraw_texts.Select(a => a.text).Join("\n") + "\n").ToBytes(Encoding.UTF8));
    AddComputeBuffer(ref Axes_Lib_BDraw_textInfos, nameof(Axes_Lib_BDraw_textInfo), Axes_Lib_BDraw_textN = max(1, Axes_Lib_BDraw_ABuff_Run(Axes_Lib_BDraw_textCharN = Axes_Lib_BDraw_tab_delimeted_text.uLength * 4)));
    for (uint i = 0; i < Axes_Lib_BDraw_texts.Count; i++)
    {
      var t = Axes_Lib_BDraw_texts[(int)i];
      var ti = Axes_Lib_BDraw_textInfo(i);
      ti.color = t.color; ti.backColor = t.backColor; ti.p = t.p; ti.p0 = t.p0; ti.p1 = t.p1; ti.height = t.h;
      ti.quadType = (uint)t.quadType; ti.axis = t.axis; ti.right = t.right; ti.up = t.up; ti.justification = (uint)t.textAlignment;
      Axes_Lib_BDraw_textInfo(i, ti);
    }
    if (Axes_Lib_BDraw_ABuff_Indexes == null || Axes_Lib_BDraw_ABuff_Indexes.computeBuffer == null) { AddComputeBuffer(ref Axes_Lib_BDraw_ABuff_Indexes, nameof(Axes_Lib_BDraw_ABuff_Indexes), 1); Axes_Lib_BDraw_ABuff_Indexes.SetData(new uint[] { 0 }); }
    if (Axes_Lib_BDraw_fontInfos != null && Axes_Lib_BDraw_ABuff_Indexes != null) { computeShader.SetBuffer(kernel_Axes_Lib_BDraw_getTextInfo, nameof(Axes_Lib_BDraw_textInfos), Axes_Lib_BDraw_textInfos); Gpu_Axes_Lib_BDraw_getTextInfo(); }
    if (Axes_Lib_BDraw_ExtraTextN > 0 && Axes_Lib_BDraw_texts.Count >= Axes_Lib_BDraw_ExtraTextN) Axes_Lib_BDraw_texts.RemoveRange(Axes_Lib_BDraw_texts.Count - Axes_Lib_BDraw_ExtraTextN, Axes_Lib_BDraw_ExtraTextN);
    int n = Axes_Lib_BDraw_texts.Count;
    Axes_Lib_BDraw_BuildExtraTexts();
    Axes_Lib_BDraw_ExtraTextN = Axes_Lib_BDraw_texts.Count - n;
  }
  public virtual IEnumerator Axes_Lib_BDraw_BuildTexts_Coroutine()
  {
    SetBytes(ref Axes_Lib_BDraw_tab_delimeted_text, (Axes_Lib_BDraw_texts.Select(a => a.text).Join("\n") + "\n").ToBytes(Encoding.UTF8));
    AddComputeBuffer(ref Axes_Lib_BDraw_textInfos, nameof(Axes_Lib_BDraw_textInfo), Axes_Lib_BDraw_textN = max(1, Axes_Lib_BDraw_ABuff_Run(Axes_Lib_BDraw_textCharN = Axes_Lib_BDraw_tab_delimeted_text.uLength * 4)));
    for (uint i = 0; i < Axes_Lib_BDraw_texts.Count; i++)
    {
      var t = Axes_Lib_BDraw_texts[(int)i];
      var ti = Axes_Lib_BDraw_textInfo(i);
      ti.color = t.color; ti.backColor = t.backColor; ti.p = t.p; ti.p0 = t.p0; ti.p1 = t.p1; ti.height = t.h;
      ti.quadType = (uint)t.quadType; ti.axis = t.axis; ti.right = t.right; ti.up = t.up; ti.justification = (uint)t.textAlignment;
      Axes_Lib_BDraw_textInfo(i, ti);
      if (i % 1000 == 0) { progress(i, (uint)Axes_Lib_BDraw_texts.Count); yield return null; }
    }
    progress(0);
    if (Axes_Lib_BDraw_ABuff_Indexes == null || Axes_Lib_BDraw_ABuff_Indexes.computeBuffer == null) { AddComputeBuffer(ref Axes_Lib_BDraw_ABuff_Indexes, nameof(Axes_Lib_BDraw_ABuff_Indexes), 1); Axes_Lib_BDraw_ABuff_Indexes.SetData(new uint[] { 0 }); }
    if (Axes_Lib_BDraw_fontInfos != null && Axes_Lib_BDraw_ABuff_Indexes != null) { computeShader.SetBuffer(kernel_Axes_Lib_BDraw_getTextInfo, nameof(Axes_Lib_BDraw_textInfos), Axes_Lib_BDraw_textInfos); Gpu_Axes_Lib_BDraw_getTextInfo(); }
  }
  public virtual void Axes_Lib_BDraw_BuildTexts_Default()
  {
    SetBytes(ref Axes_Lib_BDraw_tab_delimeted_text, (Axes_Lib_BDraw_texts.Select(a => a.text).Join("\n") + "\n").ToBytes(Encoding.UTF8));
    AddComputeBuffer(ref Axes_Lib_BDraw_textInfos, nameof(Axes_Lib_BDraw_textInfo), Axes_Lib_BDraw_textN = max(1, Axes_Lib_BDraw_ABuff_Run(Axes_Lib_BDraw_textCharN = Axes_Lib_BDraw_tab_delimeted_text.uLength * 4)));
    if (Axes_Lib_BDraw_texts.Count > 0)
    {
      var t = Axes_Lib_BDraw_texts[0];
      var ti = Axes_Lib_BDraw_textInfo(0);
      ti.color = t.color; ti.backColor = t.backColor; ti.p = t.p; ti.p0 = t.p0; ti.p1 = t.p1; ti.height = t.h;
      ti.quadType = (uint)t.quadType; ti.axis = t.axis; ti.right = t.right; ti.up = t.up; ti.justification = (uint)t.textAlignment;
      Axes_Lib_BDraw_textInfo(0, ti);
      Gpu_Axes_Lib_BDraw_setDefaultTextInfo();
    }
    if (Axes_Lib_BDraw_ABuff_Indexes == null || Axes_Lib_BDraw_ABuff_Indexes.computeBuffer == null) { AddComputeBuffer(ref Axes_Lib_BDraw_ABuff_Indexes, nameof(Axes_Lib_BDraw_ABuff_Indexes), 1); Axes_Lib_BDraw_ABuff_Indexes.SetData(new uint[] { 0 }); }
    if (Axes_Lib_BDraw_fontInfos != null && Axes_Lib_BDraw_ABuff_Indexes != null) Gpu_Axes_Lib_BDraw_getTextInfo();
  }
  public void Axes_Lib_BDraw_AddXAxis(string title, float titleHeight, float3 p0, float3 p1, float3 tRight, float3 tUp, float4 color, float2 vRange, string format, float textHeight, float3 right, float3 up, float3 margin, Axes_Lib_BDraw_Text_QuadType quadType, float3 mn, float3 mx, uint axis = 0)
  {
    if (extent(vRange) <= 0) return;
    uint decimalN = Axes_Lib_BDraw_GetDecimalN(vRange) + (uint)format.Length, maxTickN = Axes_Lib_BDraw_GetXAxisN(textHeight, decimalN, p1.xy - p0.xy);
    float range = NiceNum(abs(extent(vRange)), false), dxi = NiceNum(range / (maxTickN - 1), true), xi0 = ceil(vRange.x / dxi) * dxi, xi1 = floor(vRange.y / dxi) * dxi, x0, x1;
    if (vRange.x < vRange.y) { x0 = xi0; x1 = xi1 + dxi / 2; } else { x0 = xi1; x1 = xi0 - dxi / 2; }
    for (float xi = x0; xi < x1; xi += dxi) Axes_Lib_BDraw_AddText(xi.ToString(format), float3(lerp(p0.x, p1.x, (xi - vRange.x) / extent(vRange)), p0.y, p0.z) + textHeight / 2 * margin, right, up, color, EMPTY, textHeight, quadType, margin.y > 0 ? Axes_Lib_BDraw_TextAlignment.BottomCenter : Axes_Lib_BDraw_TextAlignment.TopCenter, mn, mx, axis);
    Axes_Lib_BDraw_AddText(title, (p0 + p1) / 2 + titleHeight * 2 * margin, tRight, tUp, color, EMPTY, titleHeight, quadType, margin.y > 0 ? Axes_Lib_BDraw_TextAlignment.BottomCenter : Axes_Lib_BDraw_TextAlignment.TopCenter, mn, mx, axis);
  }
  public void Axes_Lib_BDraw_AddYAxis(string title, float titleHeight, float3 p0, float3 p1, float3 tRight, float3 tUp, float4 color, float2 vRange, string format, float textHeight, float3 right, float3 up, float3 margin, Axes_Lib_BDraw_Text_QuadType quadType, float3 mn, float3 mx, uint axis = 0)
  {
    if (extent(vRange) <= 0) return;
    uint decimalN = Axes_Lib_BDraw_GetDecimalN(vRange) + (uint)format.Length, maxTickN = Axes_Lib_BDraw_GetXAxisN(textHeight, decimalN, p1.xy - p0.xy);
    float range = NiceNum(abs(extent(vRange)), false), dxi = NiceNum(range / (maxTickN - 1), true), xi0 = ceil(vRange.x / dxi) * dxi, xi1 = floor(vRange.y / dxi) * dxi, x0, x1;
    if (vRange.x < vRange.y) { x0 = xi0; x1 = xi1 + dxi / 2; } else { x0 = xi1; x1 = xi0 - dxi / 2; }
    for (float xi = x0; xi < x1; xi += dxi) Axes_Lib_BDraw_AddText(xi.ToString(format), float3(p0.x, lerp(p0.y, p1.y, (xi - vRange.x) / extent(vRange)), p0.z) + textHeight / 2 * margin, right, up, color, EMPTY, textHeight, quadType, tUp.x < 0 ? Axes_Lib_BDraw_TextAlignment.CenterRight : Axes_Lib_BDraw_TextAlignment.CenterLeft, mn, mx, axis);
    Axes_Lib_BDraw_AddText(title, (p0 + p1) / 2 + textHeight * (2 + decimalN / 5.0f) * margin, tRight, tUp, color, EMPTY, titleHeight, quadType, Axes_Lib_BDraw_TextAlignment.BottomCenter, mn, mx, axis);
  }
  public void Axes_Lib_BDraw_AddZAxis(string title, float titleHeight, float3 p0, float3 p1, float3 tRight, float3 tUp, float4 color, float2 vRange, string format, float textHeight, float3 right, float3 up, float3 margin, Axes_Lib_BDraw_Text_QuadType quadType, float3 mn, float3 mx, uint axis = 0)
  {
    if (extent(vRange) <= 0) return;
    uint decimalN = Axes_Lib_BDraw_GetDecimalN(vRange) + (uint)format.Length, maxTickN = Axes_Lib_BDraw_GetXAxisN(textHeight, decimalN, p1.zy - p0.zy);
    float range = NiceNum(abs(extent(vRange)), false), dxi = NiceNum(range / (maxTickN - 1), true), xi0 = ceil(vRange.x / dxi) * dxi, xi1 = floor(vRange.y / dxi) * dxi, x0, x1;
    if (vRange.x < vRange.y) { x0 = xi0; x1 = xi1 + dxi / 2; } else { x0 = xi1; x1 = xi0 - dxi / 2; }
    for (float xi = x0; xi < x1; xi += dxi) Axes_Lib_BDraw_AddText(xi.ToString(format), float3(p0.x, p0.y, lerp(p0.z, p1.z, (xi - vRange.x) / extent(vRange))) + textHeight / 2 * margin, right, up, color, EMPTY, textHeight, quadType, margin.y > 0 ? Axes_Lib_BDraw_TextAlignment.BottomCenter : Axes_Lib_BDraw_TextAlignment.TopCenter, mn, mx, axis);
    Axes_Lib_BDraw_AddText(title, (p0 + p1) / 2 + titleHeight * 2 * margin, tRight, tUp, color, EMPTY, titleHeight, quadType, margin.y > 0 ? Axes_Lib_BDraw_TextAlignment.BottomCenter : Axes_Lib_BDraw_TextAlignment.TopCenter, mn, mx, axis);
  }
  public void Axes_Lib_BDraw_AddAxes(float numberHeight, float titleHeight, float3 p0, float3 p1, float3 range0, float3 range1, float4 color,
    string xTitle, string yTitle, string zTitle, string xFormat = "0.00", string yFormat = null, string zFormat = null)
  {
    if (yFormat == null) yFormat = xFormat; if (zFormat == null) zFormat = yFormat;
    Axes_Lib_BDraw_AddXAxis(xTitle, titleHeight, float3(p0.x, p0.y, p0.z), float3(p1.x, p0.y, p0.z), f100, f011, color, float2(range0.x, range1.x), xFormat, numberHeight, f100, f011, f0__, Axes_Lib_BDraw_Text_QuadType.Switch, p0, p1, 100);
    Axes_Lib_BDraw_AddXAxis(xTitle, titleHeight, float3(p0.x, p0.y, p1.z), float3(p1.x, p0.y, p1.z), f100, f01_, color, float2(range0.x, range1.x), xFormat, numberHeight, f100, f01_, f0_1, Axes_Lib_BDraw_Text_QuadType.Switch, p0, p1, 200);
    Axes_Lib_BDraw_AddXAxis(xTitle, titleHeight, float3(p0.x, p1.y, p1.z), float3(p1.x, p1.y, p1.z), f100, f011, color, float2(range0.x, range1.x), xFormat, numberHeight, f100, f011, f011, Axes_Lib_BDraw_Text_QuadType.Switch, p0, p1, 300);
    Axes_Lib_BDraw_AddXAxis(xTitle, titleHeight, float3(p0.x, p1.y, p0.z), float3(p1.x, p1.y, p0.z), f100, f01_, color, float2(range0.x, range1.x), xFormat, numberHeight, f100, f01_, f01_, Axes_Lib_BDraw_Text_QuadType.Switch, p0, p1, 400);
    Axes_Lib_BDraw_AddYAxis(yTitle, titleHeight, float3(p0.x, p0.y, p0.z), float3(p0.x, p1.y, p0.z), f010, f_0_, color, float2(range0.y, range1.y), yFormat, numberHeight, f101, f010, f_0_, Axes_Lib_BDraw_Text_QuadType.Switch, p0, p1, 10);
    Axes_Lib_BDraw_AddYAxis(yTitle, titleHeight, float3(p1.x, p0.y, p0.z), float3(p1.x, p1.y, p0.z), f0_0, f10_, color, float2(range0.y, range1.y), yFormat, numberHeight, f10_, f010, f10_, Axes_Lib_BDraw_Text_QuadType.Switch, p0, p1, 20);
    Axes_Lib_BDraw_AddYAxis(yTitle, titleHeight, float3(p1.x, p0.y, p1.z), float3(p1.x, p1.y, p1.z), f0_0, f101, color, float2(range0.y, range1.y), yFormat, numberHeight, f101, f010, f101, Axes_Lib_BDraw_Text_QuadType.Switch, p0, p1, 30);
    Axes_Lib_BDraw_AddYAxis(yTitle, titleHeight, float3(p0.x, p0.y, p1.z), float3(p0.x, p1.y, p1.z), f0_0, f_01, color, float2(range0.y, range1.y), yFormat, numberHeight, f10_, f010, f_01, Axes_Lib_BDraw_Text_QuadType.Switch, p0, p1, 40);
    Axes_Lib_BDraw_AddZAxis(zTitle, titleHeight, float3(p0.x, p0.y, p0.z), float3(p0.x, p0.y, p1.z), f001, f110, color, float2(range0.z, range1.z), zFormat, numberHeight, f001, f110, f__0, Axes_Lib_BDraw_Text_QuadType.Switch, p0, p1, 1);
    Axes_Lib_BDraw_AddZAxis(zTitle, titleHeight, float3(p0.x, p1.y, p0.z), float3(p0.x, p1.y, p1.z), f001, f_10, color, float2(range0.z, range1.z), zFormat, numberHeight, f001, f_10, f_10, Axes_Lib_BDraw_Text_QuadType.Switch, p0, p1, 2);
    Axes_Lib_BDraw_AddZAxis(zTitle, titleHeight, float3(p1.x, p1.y, p0.z), float3(p1.x, p1.y, p1.z), f001, f110, color, float2(range0.z, range1.z), zFormat, numberHeight, f001, f110, f110, Axes_Lib_BDraw_Text_QuadType.Switch, p0, p1, 3);
    Axes_Lib_BDraw_AddZAxis(zTitle, titleHeight, float3(p1.x, p0.y, p0.z), float3(p1.x, p0.y, p1.z), f001, f_10, color, float2(range0.z, range1.z), zFormat, numberHeight, f001, f_10, f1_0, Axes_Lib_BDraw_Text_QuadType.Switch, p0, p1, 4);
  }
  public string Axes_Lib_BDraw_str(string[] s, int i) => i < s.Length ? s[i] : i > 2 && i - 3 < s.Length ? s[i - 3] : "";
  public void Axes_Lib_BDraw_AddAxes(float numberHeight, float titleHeight, float4 color, float3 p0, float3 p1,
    float3 rangeA0, float3 rangeA1, float3 rangeB0, float3 rangeB1, string xyz_titles, string xyz_formats = "0.00")
  {
    var (ts, fs, ttl, fmt) = (xyz_titles.Split('|', ';'), xyz_formats.Split('|', ';'), new string[6], new string[6]);
    for (int i = 0; i < 6; i++) { ttl[i] = Axes_Lib_BDraw_str(ts, i); fmt[i] = Axes_Lib_BDraw_str(fs, i); }
    Axes_Lib_BDraw_AddAxes(numberHeight, titleHeight, color, p0, p1, rangeA0, rangeA1, rangeB0, rangeB1, ttl, fmt);
  }
  public virtual void Axes_Lib_BDraw_AddAxes(float numberHeight, float titleHeight, float4 color, float3 p0, float3 p1,
    float3 rangeA0, float3 rangeA1, float3 rangeB0, float3 rangeB1, string[] ttl, string[] fmt)
  {
    Axes_Lib_BDraw_AddXAxis(ttl[0], titleHeight, float3(p0.x, p0.y, p0.z), float3(p1.x, p0.y, p0.z), f100, f011, color, float2(rangeA0.x, rangeA1.x), fmt[0], numberHeight, f100, f011, f0__, Axes_Lib_BDraw_Text_QuadType.Switch, p0, p1, 100);
    Axes_Lib_BDraw_AddXAxis(ttl[0], titleHeight, float3(p0.x, p0.y, p1.z), float3(p1.x, p0.y, p1.z), f100, f01_, color, float2(rangeA0.x, rangeA1.x), fmt[0], numberHeight, f100, f01_, f0_1, Axes_Lib_BDraw_Text_QuadType.Switch, p0, p1, 200);
    Axes_Lib_BDraw_AddXAxis(ttl[3], titleHeight, float3(p0.x, p1.y, p1.z), float3(p1.x, p1.y, p1.z), f100, f011, color, float2(rangeB0.x, rangeB1.x), fmt[3], numberHeight, f100, f011, f011, Axes_Lib_BDraw_Text_QuadType.Switch, p0, p1, 300);
    Axes_Lib_BDraw_AddXAxis(ttl[3], titleHeight, float3(p0.x, p1.y, p0.z), float3(p1.x, p1.y, p0.z), f100, f01_, color, float2(rangeB0.x, rangeB1.x), fmt[3], numberHeight, f100, f01_, f01_, Axes_Lib_BDraw_Text_QuadType.Switch, p0, p1, 400);
    Axes_Lib_BDraw_AddYAxis(ttl[1], titleHeight, float3(p0.x, p0.y, p0.z), float3(p0.x, p1.y, p0.z), f010, f_0_, color, float2(rangeA0.y, rangeA1.y), fmt[1], numberHeight, f101, f010, f_0_, Axes_Lib_BDraw_Text_QuadType.Switch, p0, p1, 10);
    Axes_Lib_BDraw_AddYAxis(ttl[4], titleHeight, float3(p1.x, p0.y, p0.z), float3(p1.x, p1.y, p0.z), f0_0, f10_, color, float2(rangeB0.y, rangeB1.y), fmt[4], numberHeight, f10_, f010, f10_, Axes_Lib_BDraw_Text_QuadType.Switch, p0, p1, 20);
    Axes_Lib_BDraw_AddYAxis(ttl[4], titleHeight, float3(p1.x, p0.y, p1.z), float3(p1.x, p1.y, p1.z), f0_0, f101, color, float2(rangeB0.y, rangeB1.y), fmt[4], numberHeight, f101, f010, f101, Axes_Lib_BDraw_Text_QuadType.Switch, p0, p1, 30);
    Axes_Lib_BDraw_AddYAxis(ttl[1], titleHeight, float3(p0.x, p0.y, p1.z), float3(p0.x, p1.y, p1.z), f0_0, f_01, color, float2(rangeA0.y, rangeA1.y), fmt[1], numberHeight, f10_, f010, f_01, Axes_Lib_BDraw_Text_QuadType.Switch, p0, p1, 40);
    Axes_Lib_BDraw_AddZAxis(ttl[2], titleHeight, float3(p0.x, p0.y, p0.z), float3(p0.x, p0.y, p1.z), f001, f110, color, float2(rangeA0.z, rangeA1.z), fmt[2], numberHeight, f001, f110, f__0, Axes_Lib_BDraw_Text_QuadType.Switch, p0, p1, 1);
    Axes_Lib_BDraw_AddZAxis(ttl[5], titleHeight, float3(p0.x, p1.y, p0.z), float3(p0.x, p1.y, p1.z), f001, f_10, color, float2(rangeB0.z, rangeB1.z), fmt[5], numberHeight, f001, f_10, f_10, Axes_Lib_BDraw_Text_QuadType.Switch, p0, p1, 2);
    Axes_Lib_BDraw_AddZAxis(ttl[5], titleHeight, float3(p1.x, p1.y, p0.z), float3(p1.x, p1.y, p1.z), f001, f110, color, float2(rangeB0.z, rangeB1.z), fmt[5], numberHeight, f001, f110, f110, Axes_Lib_BDraw_Text_QuadType.Switch, p0, p1, 3);
    Axes_Lib_BDraw_AddZAxis(ttl[2], titleHeight, float3(p1.x, p0.y, p0.z), float3(p1.x, p0.y, p1.z), f001, f_10, color, float2(rangeA0.z, rangeA1.z), fmt[2], numberHeight, f001, f_10, f1_0, Axes_Lib_BDraw_Text_QuadType.Switch, p0, p1, 4);
  }
  public void Axes_Lib_BDraw_AddAxes(float numberHeight, float titleHeight, float3 p0, float3 p1, float3 range0, float3 range1, float4 color, string xyz_titles, string xyz_formats = "0.00")
  {
    var _titles = splitStr3(xyz_titles, false);
    var _formats = splitStr3(xyz_formats, true);
    Axes_Lib_BDraw_AddAxes(numberHeight, titleHeight, p0, p1, range0, range1, color, _titles[0], _titles[1], _titles[2], _formats[0], _formats[1], _formats[2]);
  }
  public void Axes_Lib_BDraw_AddAxes(float numberHeight, float titleHeight, float3 p0, float3 p1, float4 color,
    string xTitle, string yTitle, string zTitle, string xFormat = "0.00", string yFormat = null, string zFormat = null, bool zeroOrigin = false)
  {
    float3 range0 = p0, range1 = p1; if (zeroOrigin) { range1 -= range0; range0 = f000; }
    Axes_Lib_BDraw_AddAxes(numberHeight, titleHeight, p0, p1, range0, range1, color, xTitle, yTitle, zTitle, xFormat, yFormat, zFormat);
  }
  public void Axes_Lib_BDraw_AddAxes(float numberHeight, float titleHeight, float3 p0, float3 p1, float4 color,
  string xyz_titles, string xyz_formats = "0.00", bool zeroOrigin = false)
  {
    var _titles = splitStr3(xyz_titles, false);
    var _formats = splitStr3(xyz_formats, true);
    float3 range0 = p0, range1 = p1; if (zeroOrigin) { range1 -= range0; range0 = f000; }
    Axes_Lib_BDraw_AddAxes(numberHeight, titleHeight, p0, p1, range0, range1, color, _titles[0], _titles[1], _titles[2], _formats[0], _formats[1], _formats[2]);
  }
  public virtual void Axes_Lib_BDraw_AddAxes(float numberHeight, float titleHeight, float4 color,
    string xTitleA, float3 x0A, float3 x1A, float2 xRangeA, string xFormatA,
    string xTitleB, float3 x0B, float3 x1B, float2 xRangeB, string xFormatB,
    string xTitleC, float3 x0C, float3 x1C, float2 xRangeC, string xFormatC,
    string xTitleD, float3 x0D, float3 x1D, float2 xRangeD, string xFormatD,
    string yTitleA, float3 y0A, float3 y1A, float2 yRangeA, string yFormatA,
    string yTitleB, float3 y0B, float3 y1B, float2 yRangeB, string yFormatB,
    string yTitleC, float3 y0C, float3 y1C, float2 yRangeC, string yFormatC,
    string yTitleD, float3 y0D, float3 y1D, float2 yRangeD, string yFormatD,
    string zTitleA, float3 z0A, float3 z1A, float2 zRangeA, string zFormatA,
    string zTitleB, float3 z0B, float3 z1B, float2 zRangeB, string zFormatB,
    string zTitleC, float3 z0C, float3 z1C, float2 zRangeC, string zFormatC,
    string zTitleD, float3 z0D, float3 z1D, float2 zRangeD, string zFormatD)
  {
    Axes_Lib_BDraw_AddXAxis(xTitleA, titleHeight, x0A, x1A, f100, f011, color, xRangeA, xFormatA, numberHeight, f100, f011, f0__, Axes_Lib_BDraw_Text_QuadType.Switch, f000, f000);
    Axes_Lib_BDraw_AddXAxis(xTitleB, titleHeight, x0B, x1B, f100, f011, color, xRangeB, xFormatB, numberHeight, f100, f011, f011, Axes_Lib_BDraw_Text_QuadType.Switch, f000, f000);
    Axes_Lib_BDraw_AddXAxis(xTitleC, titleHeight, x0C, x1C, f100, f011, color, xRangeC, xFormatC, numberHeight, f100, f011, f0__, Axes_Lib_BDraw_Text_QuadType.Switch, f000, f000);
    Axes_Lib_BDraw_AddXAxis(xTitleD, titleHeight, x0D, x1D, f100, f011, color, xRangeD, xFormatD, numberHeight, f100, f011, f011, Axes_Lib_BDraw_Text_QuadType.Switch, f000, f000);
    Axes_Lib_BDraw_AddYAxis(yTitleA, titleHeight, y0A, y1A, f010, f_01, color, yRangeA, yFormatA, numberHeight, f10_, f010, f_01, Axes_Lib_BDraw_Text_QuadType.Switch, f000, f000);
    Axes_Lib_BDraw_AddYAxis(yTitleB, titleHeight, y0B, y1B, f0_0, f10_, color, yRangeB, yFormatB, numberHeight, f10_, f010, f10_, Axes_Lib_BDraw_Text_QuadType.Switch, f000, f000);
    Axes_Lib_BDraw_AddYAxis(yTitleC, titleHeight, y0C, y1C, f010, f_01, color, yRangeC, yFormatC, numberHeight, f10_, f010, f_01, Axes_Lib_BDraw_Text_QuadType.Switch, f000, f000);
    Axes_Lib_BDraw_AddYAxis(yTitleD, titleHeight, y0D, y1D, f0_0, f10_, color, yRangeD, yFormatD, numberHeight, f10_, f010, f10_, Axes_Lib_BDraw_Text_QuadType.Switch, f000, f000);
    Axes_Lib_BDraw_AddZAxis(zTitleA, titleHeight, z0A, z1A, f010, f_01, color, zRangeA, zFormatA, numberHeight, f10_, f010, f_01, Axes_Lib_BDraw_Text_QuadType.Switch, f000, f000);
    Axes_Lib_BDraw_AddZAxis(zTitleB, titleHeight, z0B, z1B, f0_0, f10_, color, zRangeB, zFormatB, numberHeight, f10_, f010, f10_, Axes_Lib_BDraw_Text_QuadType.Switch, f000, f000);
    Axes_Lib_BDraw_AddZAxis(zTitleC, titleHeight, z0C, z1C, f010, f_01, color, zRangeC, zFormatC, numberHeight, f10_, f010, f_01, Axes_Lib_BDraw_Text_QuadType.Switch, f000, f000);
    Axes_Lib_BDraw_AddZAxis(zTitleD, titleHeight, z0D, z1D, f0_0, f10_, color, zRangeD, zFormatD, numberHeight, f10_, f010, f10_, Axes_Lib_BDraw_Text_QuadType.Switch, f000, f000);
  }
  public void Axes_Lib_BDraw_AddAxes(float numberHeight, float titleHeight, float4 color,
    string xTitleA, float3 x0A, float3 x1A, float2 xRangeA, string xFormatA,
    string xTitleB, float3 x0B, float3 x1B, float2 xRangeB, string xFormatB,
    string yTitleA, float3 y0A, float3 y1A, float2 yRangeA, string yFormatA,
    string yTitleB, float3 y0B, float3 y1B, float2 yRangeB, string yFormatB,
    string zTitleA, float3 z0A, float3 z1A, float2 zRangeA, string zFormatA,
    string zTitleB, float3 z0B, float3 z1B, float2 zRangeB, string zFormatB)
  {
    Axes_Lib_BDraw_AddXAxis(xTitleA, titleHeight, x0A, x1A, f100, f011, color, xRangeA, xFormatA, numberHeight, f100, f011, f0__, Axes_Lib_BDraw_Text_QuadType.Switch, f000, f000);
    Axes_Lib_BDraw_AddXAxis(xTitleB, titleHeight, x0B, x1B, f100, f011, color, xRangeB, xFormatB, numberHeight, f100, f011, f011, Axes_Lib_BDraw_Text_QuadType.Switch, f000, f000);
    Axes_Lib_BDraw_AddYAxis(yTitleA, titleHeight, y0A, y1A, f010, f_01, color, yRangeA, yFormatA, numberHeight, f10_, f010, f_01, Axes_Lib_BDraw_Text_QuadType.Switch, f000, f000);
    Axes_Lib_BDraw_AddYAxis(yTitleB, titleHeight, y0B, y1B, f0_0, f10_, color, yRangeB, yFormatB, numberHeight, f10_, f010, f10_, Axes_Lib_BDraw_Text_QuadType.Switch, f000, f000);
    Axes_Lib_BDraw_AddZAxis(zTitleA, titleHeight, z0A, z1A, f010, f_01, color, zRangeA, zFormatA, numberHeight, f10_, f010, f_01, Axes_Lib_BDraw_Text_QuadType.Switch, f000, f000);
    Axes_Lib_BDraw_AddZAxis(zTitleB, titleHeight, z0B, z1B, f0_0, f10_, color, zRangeB, zFormatB, numberHeight, f10_, f010, f10_, Axes_Lib_BDraw_Text_QuadType.Switch, f000, f000);
  }
  public void Axes_Lib_BDraw_AddAxes(float numberHeight, float titleHeight, float4 color,
    string xTitleA, float3 x0A, float3 x1A, float2 xRangeA, string xFormatA,
    string xTitleB, float3 x0B, float3 x1B, float2 xRangeB, string xFormatB,
    string yTitleA, float3 y0A, float3 y1A, float2 yRangeA, string yFormatA,
    string yTitleB, float3 y0B, float3 y1B, float2 yRangeB, string yFormatB)
  {
    Axes_Lib_BDraw_AddXAxis(xTitleA, titleHeight, x0A, x1A, f100, f011, color, xRangeA, xFormatA, numberHeight, f100, f011, f0__, Axes_Lib_BDraw_Text_QuadType.Switch, f000, f000);
    Axes_Lib_BDraw_AddXAxis(xTitleB, titleHeight, x0B, x1B, f100, f011, color, xRangeB, xFormatB, numberHeight, f100, f011, f011, Axes_Lib_BDraw_Text_QuadType.Switch, f000, f000);
    Axes_Lib_BDraw_AddYAxis(yTitleA, titleHeight, y0A, y1A, f010, f_01, color, yRangeA, yFormatA, numberHeight, f10_, f010, f_01, Axes_Lib_BDraw_Text_QuadType.Switch, f000, f000);
    Axes_Lib_BDraw_AddYAxis(yTitleB, titleHeight, y0B, y1B, f0_0, f10_, color, yRangeB, yFormatB, numberHeight, f10_, f010, f10_, Axes_Lib_BDraw_Text_QuadType.Switch, f000, f000);
  }
  public void Axes_Lib_BDraw_AddAxes(float numberHeight, float titleHeight, float4 color,
    string xTitleA, float3 x0A, float3 x1A, float2 xRangeA, string xFormatA,
    string yTitleA, float3 y0A, float3 y1A, float2 yRangeA, string yFormatA)
  {
    Axes_Lib_BDraw_AddAxes(numberHeight, titleHeight, color, xTitleA, x0A, x1A, xRangeA, xFormatA, xTitleA, x0A, x1A, xRangeA, xFormatA,
     yTitleA, y0A, y1A, yRangeA, yFormatA, yTitleA, y0A, y1A, yRangeA, yFormatA);
  }
  public uint Axes_Lib_BDraw_GetXAxisN(float textHeight, uint decimalN, float2 vRange) { float w = decimalN * textHeight; uint axisN = roundu(abs(extent(vRange)) / w); return clamp(axisN, 2, 25); }
  public uint Axes_Lib_BDraw_GetYAxisN(float textHeight, float2 vRange) => roundu(abs(extent(vRange)) / textHeight * 0.75f);
  public uint3 Axes_Lib_BDraw_GetXAxisN(float textHeight, uint3 decimalN, float3 cubeMin, float3 cubeMax)
  {
    float3 w = decimalN * textHeight;
    uint3 axisN = roundu(abs(cubeMax - cubeMin) / w);
    return clamp(axisN, u111 * 2, u111 * 25);
  }
  public uint3 Axes_Lib_BDraw_GetDecimalN(float3 cubeMin, float3 cubeMax)
  {
    int3 tickN = 25 * i111;
    float3 pRange = cubeMax - cubeMin, range = NiceNum(pRange, false);
    float3 di = NiceNum(range / (tickN - 1), true);
    uint3 decimalN = roundu(di >= 1) * flooru(1 + abs(log10(roundu(di == f000) + di)));
    return max(u111, decimalN);
  }
  public uint Axes_Lib_BDraw_GetDecimalN(float2 vRange)
  {
    int tickN = 25;
    float pRange = abs(extent(vRange)), range = NiceNum(pRange, false);
    float di = NiceNum(range / (tickN - 1), true);
    uint decimalN = roundu(Is(di >= 1)) * flooru(1 + abs(log10(roundu(Is(di == 0)) + di)));
    return max(1, decimalN);
  }
  public void Axes_Lib_BDraw_AddLegend(string title, float2 vRange, string format)
  {
    float h = 8;
    float3 c = 10000 * f110;
    Axes_Lib_BDraw_AddYAxis(title, 0.4f, c + float3(0.4f, -h / 2, 0), c + float3(0.4f, h / 2, 0), f010, f_00, BLACK, vRange, format, 0.2f, f100, f010, f_00, Axes_Lib_BDraw_Text_QuadType.FrontOnly, f000, f000);
  }
  public virtual v2f vert_Axes_Lib_BDraw_Text(Axes_Lib_BDraw_TextInfo t, uint i, uint j, v2f o)
  {
    float3 p = t.p, p0 = t.p0, p1 = t.p1, right = t.right, up = t.up;
    float h = t.height;
    float2 uvSize = t.uvSize;
    float4 color = t.color;
    uint quadType = t.quadType, justification = t.justification, axis = t.axis;
    float w = h * uvSize.x;
    float3 jp = f000;
    if (quadType == Axes_Lib_BDraw_Text_QuadType_Billboard)
    {
      o.wPos = p;
      switch (justification)
      {
        case Axes_Lib_BDraw_TextAlignment_BottomLeft: jp = w / 2 * right + h / 2 * up; break;
        case Axes_Lib_BDraw_TextAlignment_CenterLeft: jp = w / 2 * right; break;
        case Axes_Lib_BDraw_TextAlignment_TopLeft: jp = w / 2 * right - h / 2 * up; break;
        case Axes_Lib_BDraw_TextAlignment_BottomCenter: jp = h / 2 * up; break;
        case Axes_Lib_BDraw_TextAlignment_CenterCenter: break;
        case Axes_Lib_BDraw_TextAlignment_TopCenter: jp = -h / 2 * up; break;
        case Axes_Lib_BDraw_TextAlignment_BottomRight: jp = -w / 2 * right + h / 2 * up; break;
        case Axes_Lib_BDraw_TextAlignment_CenterRight: jp = -w / 2 * right; break;
        case Axes_Lib_BDraw_TextAlignment_TopRight: jp = -w / 2 * right - h / 2 * up; break;
      }
      float4 billboardQuad = float4((Axes_Lib_BDraw_wrapJ(j, 2) - 0.5f) * w, (0.5f - Axes_Lib_BDraw_wrapJ(j, 1)) * h, 0, 0);
      o = Axes_Lib_BDraw_o_i(i, Axes_Lib_BDraw_o_drawType(Axes_Lib_BDraw_Draw_Text3D, Axes_Lib_BDraw_o_r(i, Axes_Lib_BDraw_o_color(color, Axes_Lib_BDraw_o_pos_PV(p, billboardQuad + float4(jp, 0), Axes_Lib_BDraw_o_normal(f00_, Axes_Lib_BDraw_o_uv(float2(Axes_Lib_BDraw_wrapJ(j, 2), Axes_Lib_BDraw_wrapJ(j, 4)) * uvSize, o)))))));
    }
    else if (quadType == Axes_Lib_BDraw_Text_QuadType_Arrow)
    {
      o.wPos = p;
      right = normalize(p1 - p0);
      up = f010;
      jp = w / 2 * right;
      float3 q0 = p - jp, q1 = p + jp;
      o = Axes_Lib_BDraw_o_uv(dot(p - _WorldSpaceCameraPos, cross(right, up)) < 0 ? float2(length(q1 - q0) / h * Axes_Lib_BDraw_wrapJ(j, 1), Axes_Lib_BDraw_wrapJ(j, 2) - 0.5f) : float2(length(q1 - q0) / h * (1 - Axes_Lib_BDraw_wrapJ(j, 1)), 0.5f - Axes_Lib_BDraw_wrapJ(j, 2)), Axes_Lib_BDraw_o_drawType(Axes_Lib_BDraw_Draw_Text3D, vert_Axes_Lib_BDraw_Arrow(q0, q1, h * 0.165f, color, i, j, o)));
    }
    else if (quadType == Axes_Lib_BDraw_Text_QuadType_FrontBack || quadType == Axes_Lib_BDraw_Text_QuadType_Switch || dot(p - _WorldSpaceCameraPos, cross(right, up)) > 0)
    {
      bool draw = true;
      if (_WorldSpaceCameraPos.y > p1.y)
      {
        if ((axis == 400 && _WorldSpaceCameraPos.z < p0.z) || (axis == 300 && _WorldSpaceCameraPos.z > p1.z)) draw = false;
        if ((axis == 3 && _WorldSpaceCameraPos.x > p1.x) || (axis == 2 && _WorldSpaceCameraPos.x < p0.x)) draw = false;
        if (axis == 100 && _WorldSpaceCameraPos.z > p0.z) draw = false;
        if (axis == 200 && _WorldSpaceCameraPos.z < p1.z) draw = false;
        if (axis == 4 && _WorldSpaceCameraPos.x < p1.x) draw = false;
        if (axis == 1 && _WorldSpaceCameraPos.x > p0.x) draw = false;
      }
      else if (_WorldSpaceCameraPos.y < p0.y)
      {
        if ((axis == 100 && _WorldSpaceCameraPos.z < p0.z) || (axis == 200 && _WorldSpaceCameraPos.z > p1.z)) draw = false;
        if ((axis == 4 && _WorldSpaceCameraPos.x > p1.x) || (axis == 1 && _WorldSpaceCameraPos.x < p0.x)) draw = false;
        if (axis == 400 && _WorldSpaceCameraPos.z > p0.z) draw = false;
        if (axis == 300 && _WorldSpaceCameraPos.z < p1.z) draw = false;
        if (axis == 3 && _WorldSpaceCameraPos.x < p1.x) draw = false;
        if (axis == 2 && _WorldSpaceCameraPos.x > p0.x) draw = false;
      }
      else
      {
        if ((axis == 400 && _WorldSpaceCameraPos.z > p0.z) || (axis == 300 && _WorldSpaceCameraPos.z < p1.z)) draw = false;
        if ((axis == 3 && _WorldSpaceCameraPos.x < p1.x) || (axis == 2 && _WorldSpaceCameraPos.x > p0.x)) draw = false;
        if ((axis == 100 && _WorldSpaceCameraPos.z > p0.z) || (axis == 200 && _WorldSpaceCameraPos.z < p1.z)) draw = false;
        if ((axis == 4 && _WorldSpaceCameraPos.x < p1.x) || (axis == 1 && _WorldSpaceCameraPos.x > p0.x)) draw = false;
      }
      if (axis == 10 && _WorldSpaceCameraPos.x > p0.x && _WorldSpaceCameraPos.z > p0.z) draw = false;
      else if (axis == 10 && _WorldSpaceCameraPos.x < p0.x && _WorldSpaceCameraPos.z < p0.z) draw = false;
      else if (axis == 20 && _WorldSpaceCameraPos.x > p1.x && _WorldSpaceCameraPos.z < p0.z) draw = false;
      else if (axis == 20 && _WorldSpaceCameraPos.x < p1.x && _WorldSpaceCameraPos.z > p0.z) draw = false;
      else if (axis == 30 && _WorldSpaceCameraPos.x > p1.x && _WorldSpaceCameraPos.z > p1.z) draw = false;
      else if (axis == 30 && _WorldSpaceCameraPos.x < p1.x && _WorldSpaceCameraPos.z < p1.z) draw = false;
      else if (axis == 40 && _WorldSpaceCameraPos.x < p0.x && _WorldSpaceCameraPos.z > p1.z) draw = false;
      else if (axis == 40 && _WorldSpaceCameraPos.x > p0.x && _WorldSpaceCameraPos.z < p1.z) draw = false;
      if (draw)
      {
        o.wPos = p;
        switch (justification)
        {
          case Axes_Lib_BDraw_TextAlignment_BottomLeft: break;
          case Axes_Lib_BDraw_TextAlignment_CenterLeft: jp = -h / 2 * up; break;
          case Axes_Lib_BDraw_TextAlignment_TopLeft: jp = -h * up; break;
          case Axes_Lib_BDraw_TextAlignment_BottomCenter: jp = -w / 2 * right; break;
          case Axes_Lib_BDraw_TextAlignment_CenterCenter: jp = -w / 2 * right - h / 2 * up; break;
          case Axes_Lib_BDraw_TextAlignment_TopCenter: jp = -w / 2 * right - h * up; break;
          case Axes_Lib_BDraw_TextAlignment_BottomRight: jp = -w * right; break;
          case Axes_Lib_BDraw_TextAlignment_CenterRight: jp = -w * right - h / 2 * up; break;
          case Axes_Lib_BDraw_TextAlignment_TopRight: jp = -w * right - h * up; break;
        }
        float3 q0 = p + jp, q1 = q0 + right * w, p2 = q1 + up * h, p3 = q0 + up * h;
        o = Axes_Lib_BDraw_o_i(i, Axes_Lib_BDraw_o_drawType(Axes_Lib_BDraw_Draw_Text3D, Axes_Lib_BDraw_o_r(i, Axes_Lib_BDraw_o_color(color, Axes_Lib_BDraw_o_pos_c(o.wPos = j % 5 == 0 ? p3 : j == 1 ? p2 : j == 4 ? q0 : q1, Axes_Lib_BDraw_o_normal(cross(right, up), Axes_Lib_BDraw_o_uv(quadType == Axes_Lib_BDraw_Text_QuadType_Switch && dot(p - _WorldSpaceCameraPos, cross(right, up)) < 0 ? float2(1 - Axes_Lib_BDraw_wrapJ(j, 2), Axes_Lib_BDraw_wrapJ(j, 4)) * uvSize : float2(Axes_Lib_BDraw_wrapJ(j, 2), Axes_Lib_BDraw_wrapJ(j, 4)) * uvSize, o)))))));
      }
    }
    return o;
  }
  public virtual v2f vert_Axes_Lib_BDraw_Text(uint i, uint j, v2f o) => vert_Axes_Lib_BDraw_Text(Axes_Lib_BDraw_textInfo(i), i, j, o);
  public virtual void Axes_Lib_BDraw_getTextInfo_GS(uint3 id)
  {
    uint i = id.x;
    Axes_Lib_BDraw_TextInfo ti = Axes_Lib_BDraw_textInfo(i);
    ti.textI = i;
    ti.uvSize = f01;
    uint2 textIs = Axes_Lib_BDraw_Get_text_indexes(i);
    float2 t = ti.uvSize;
    for (uint j = textIs.x; j < textIs.y; j++) { uint byteI = Axes_Lib_BDraw_Byte(j); if (byteI >= 32) { byteI -= 32; t.x += Axes_Lib_BDraw_fontInfos[byteI].advance; } }
    t.x /= g.Axes_Lib_BDraw_fontSize;
    ti.uvSize = t;
    Axes_Lib_BDraw_textInfo(i, ti);
  }
  public virtual void Axes_Lib_BDraw_setDefaultTextInfo_GS(uint3 id)
  {
    uint i = id.x;
    if (i > 0)
    {
      Axes_Lib_BDraw_TextInfo t = Axes_Lib_BDraw_textInfo(0), ti = Axes_Lib_BDraw_textInfo(i);
      ti.color = t.color; ti.backColor = t.backColor; ti.p = t.p; ti.p0 = t.p0; ti.p1 = t.p1; ti.height = t.height;
      ti.quadType = (uint)t.quadType; ti.axis = t.axis; ti.right = t.right; ti.up = t.up; ti.justification = t.justification;
      Axes_Lib_BDraw_textInfo(i, ti);
    }
  }
  public virtual float4 frag_Axes_Lib_BDraw_Text(Texture2D t, RWStructuredBuffer<uint> _text, RWStructuredBuffer<Axes_Lib_BDraw_FontInfo> Axes_Lib_BDraw_fontInfos, float Axes_Lib_BDraw_fontSize, uint quadType, float4 backColor, uint2 textIs, v2f i)
  {
    float uv_x = 0;
    float4 color = backColor;
    for (uint textI = textIs.x; textI < textIs.y; textI++)
    {
      uint fontInfoI = TextByte(_text, textI) - 32;
      Axes_Lib_BDraw_FontInfo f = Axes_Lib_BDraw_fontInfos[fontInfoI];
      float dx = f.advance / Axes_Lib_BDraw_fontSize;
      float2 mn = float2(f.minX, f.minY) / Axes_Lib_BDraw_fontSize, mx = float2(f.maxX, f.maxY) / Axes_Lib_BDraw_fontSize, range = mx - mn;
      if (quadType == Axes_Lib_BDraw_Text_QuadType_Arrow) { mn.y -= 0.5f; mx.y -= 0.5f; }
      float2 uv = (i.uv - uv_x * f10 + f1_ * mn - float2(f.bearing / Axes_Lib_BDraw_fontSize, 0.25f)) / range;
      if (IsNotOutside(uv, f00, f11))
      {
        if (any(f.uvTopLeft > f.uvBottomRight)) uv = uv.yx;
        uv = lerp(f.uvBottomLeft, f.uvTopRight, uv);
        color = tex2Dlod(t, float4(uv, f00));
        color.rgb = (1 - color.rgb) * i.color.rgb;
        if (color.w == 0) color = backColor;
        else color.w = i.color.w;
        break;
      }
      uv_x += dx;
    }
    return color;
  }
  public virtual float4 frag_Axes_Lib_BDraw_GS(v2f i, float4 color)
  {
    switch (Axes_Lib_BDraw_o_drawType(i))
    {
      case uint_max: Discard(0); break;
      case Axes_Lib_BDraw_Draw_Sphere: color = frag_Axes_Lib_BDraw_Sphere(i); break;
      case Axes_Lib_BDraw_Draw_Line: color = frag_Axes_Lib_BDraw_Line(i); break;
      case Axes_Lib_BDraw_Draw_Arrow: color = frag_Axes_Lib_BDraw_Arrow(i); break;
      case Axes_Lib_BDraw_Draw_Signal: color = frag_Axes_Lib_BDraw_Signal(i); break;
      case Axes_Lib_BDraw_Draw_LineSegment: color = frag_Axes_Lib_BDraw_LineSegment(i); break;
      case Axes_Lib_BDraw_Draw_Mesh: color = frag_Axes_Lib_BDraw_Mesh(i); break;
      case Axes_Lib_BDraw_Draw_Text3D:
        Axes_Lib_BDraw_TextInfo t = Axes_Lib_BDraw_textInfo(Axes_Lib_BDraw_o_i(i));
        color = frag_Axes_Lib_BDraw_Text(Axes_Lib_BDraw_fontTexture, Axes_Lib_BDraw_tab_delimeted_text, Axes_Lib_BDraw_fontInfos, Axes_Lib_BDraw_fontSize, t.quadType, t.backColor, Axes_Lib_BDraw_Get_text_indexes(t.textI), i);
        break;
    }
    return color;
  }

  public virtual void base_Axes_Lib_BDraw_Start0_GS()
  {
    Axes_Lib_BDraw_ABuff_Start0_GS();
  }
  public virtual void base_Axes_Lib_BDraw_Start1_GS()
  {
    Axes_Lib_BDraw_ABuff_Start1_GS();
  }
  public virtual void base_Axes_Lib_BDraw_OnApplicationQuit() { Save_UI(); OnApplicationQuit_GS(); base.OnApplicationQuit(); already_quited = true; }
  public virtual void base_Axes_Lib_BDraw_LateUpdate0_GS()
  {
    Axes_Lib_BDraw_ABuff_LateUpdate0_GS();
  }
  public virtual void base_Axes_Lib_BDraw_LateUpdate1_GS()
  {
    Axes_Lib_BDraw_ABuff_LateUpdate1_GS();
  }
  public virtual void base_Axes_Lib_BDraw_Update0_GS()
  {
    Axes_Lib_BDraw_ABuff_Update0_GS();
  }
  public virtual void base_Axes_Lib_BDraw_Update1_GS()
  {
    Axes_Lib_BDraw_ABuff_Update1_GS();
  }
  public virtual void base_Axes_Lib_BDraw_OnValueChanged_GS()
  {
    Axes_Lib_BDraw_ABuff_OnValueChanged_GS();
  }
  public virtual void base_Axes_Lib_BDraw_InitBuffers0_GS()
  {
    Axes_Lib_BDraw_ABuff_InitBuffers0_GS();
  }
  public virtual void base_Axes_Lib_BDraw_InitBuffers1_GS()
  {
    Axes_Lib_BDraw_ABuff_InitBuffers1_GS();
  }
  [HideInInspector]
  public virtual void base_Axes_Lib_BDraw_onRenderObject_GS(ref bool render, ref bool cpu)
  {
    Axes_Lib_BDraw_ABuff_onRenderObject_GS(ref render, ref cpu);
  }
  public virtual float4 base_frag_Axes_Lib_BDraw_GS(v2f i, float4 color) { return color; }
  public void Axes_Lib_BDraw_ABuff_print_Bits(string title) { StrBldr sb = StrBldr($"{title}, Axes_Lib_BDraw_ABuff_Bits"); for (uint i = 0; i < Axes_Lib_BDraw_ABuff_BitN; i++) sb.Add(" ", Axes_Lib_BDraw_ABuff_Bits[i]); print(sb); }
  public void Axes_Lib_BDraw_ABuff_print_Sums(string title) { StrBldr sb = StrBldr($"{title}, Axes_Lib_BDraw_ABuff_Sums"); for (uint i = 0; i < Axes_Lib_BDraw_ABuff_BitN; i++) sb.Add(" ", Axes_Lib_BDraw_ABuff_Sums[i]); print(sb); }
  public void Axes_Lib_BDraw_ABuff_print_Indexes(string title) { StrBldr sb = StrBldr($"{title}: Axes_Lib_BDraw_ABuff_Indexes"); for (uint i = 0; i < Axes_Lib_BDraw_ABuff_IndexN; i++) sb.Add(" ", Axes_Lib_BDraw_ABuff_Indexes[i]); print(sb); }
  public void Axes_Lib_BDraw_ABuff_SetN(uint n)
  {
    if (n > 2147450880) throw new Exception("gsABuff: Axes_Lib_BDraw_ABuff_N > 2,147,450,880");
    Axes_Lib_BDraw_ABuff_N = n; Axes_Lib_BDraw_ABuff_BitN = ceilu(Axes_Lib_BDraw_ABuff_N, 32); Axes_Lib_BDraw_ABuff_BitN1 = ceilu(Axes_Lib_BDraw_ABuff_BitN, numthreads1); Axes_Lib_BDraw_ABuff_BitN2 = ceilu(Axes_Lib_BDraw_ABuff_BitN1, numthreads1);
    AllocData_Axes_Lib_BDraw_ABuff_Bits(Axes_Lib_BDraw_ABuff_BitN); AllocData_Axes_Lib_BDraw_ABuff_Fills1(Axes_Lib_BDraw_ABuff_BitN1); AllocData_Axes_Lib_BDraw_ABuff_Fills2(Axes_Lib_BDraw_ABuff_BitN2); AllocData_Axes_Lib_BDraw_ABuff_Sums(Axes_Lib_BDraw_ABuff_BitN);
  }
  public void Axes_Lib_BDraw_ABuff_FillPrefixes() { Gpu_Axes_Lib_BDraw_ABuff_GetFills1(); Gpu_Axes_Lib_BDraw_ABuff_GetFills2(); Gpu_Axes_Lib_BDraw_ABuff_IncFills1(); Gpu_Axes_Lib_BDraw_ABuff_IncSums(); }
  public void Axes_Lib_BDraw_ABuff_getIndexes() { AllocData_Axes_Lib_BDraw_ABuff_Indexes(Axes_Lib_BDraw_ABuff_IndexN); Gpu_Axes_Lib_BDraw_ABuff_GetIndexes(); }
  public void Axes_Lib_BDraw_ABuff_FillIndexes() { Axes_Lib_BDraw_ABuff_FillPrefixes(); Axes_Lib_BDraw_ABuff_getIndexes(); }
  public virtual uint Axes_Lib_BDraw_ABuff_Run(uint n) { Axes_Lib_BDraw_ABuff_SetN(n); Gpu_Axes_Lib_BDraw_ABuff_GetSums(); Axes_Lib_BDraw_ABuff_FillIndexes(); return Axes_Lib_BDraw_ABuff_IndexN; }
  public uint Axes_Lib_BDraw_ABuff_Run(int n) => Axes_Lib_BDraw_ABuff_Run((uint)n);
  public uint Axes_Lib_BDraw_ABuff_Run(uint2 n) => Axes_Lib_BDraw_ABuff_Run(cproduct(n)); public uint Axes_Lib_BDraw_ABuff_Run(uint3 n) => Axes_Lib_BDraw_ABuff_Run(cproduct(n));
  public uint Axes_Lib_BDraw_ABuff_Run(int2 n) => Axes_Lib_BDraw_ABuff_Run(cproduct(n)); public uint Axes_Lib_BDraw_ABuff_Run(int3 n) => Axes_Lib_BDraw_ABuff_Run(cproduct(n));
  public virtual void Axes_Lib_BDraw_ABuff_Init_Bits(uint n, GS parent, string _N, string _BitN, ref RWStructuredBuffer<uint> _Bits) { Axes_Lib_BDraw_ABuff_SetN(n); parent.SetValue(_N, Axes_Lib_BDraw_ABuff_N); parent.SetValue(_BitN, Axes_Lib_BDraw_ABuff_BitN); }
  public virtual void Axes_Lib_BDraw_ABuff_Prefix_Sums() { Gpu_Axes_Lib_BDraw_ABuff_Get_Bits_Sums(); Axes_Lib_BDraw_ABuff_FillPrefixes(); }
  public virtual void Axes_Lib_BDraw_ABuff_Bit_Indexes(RWStructuredBuffer<uint> bits, GS _this, string _IndexN, ref RWStructuredBuffer<uint> _Indexes) { Axes_Lib_BDraw_ABuff_Prefix_Sums(); Axes_Lib_BDraw_ABuff_getIndexes(); _this.SetValue(_IndexN, Axes_Lib_BDraw_ABuff_IndexN); }
  public uint Axes_Lib_BDraw_ABuff_Assign_Bits(uint i, uint j, uint bits) => bits | (Is(i < Axes_Lib_BDraw_ABuff_N && Axes_Lib_BDraw_ABuff_IsBitOn(i)) << (int)j);
  public virtual void Axes_Lib_BDraw_ABuff_Get_Bits_GS(uint3 id) { uint i = id.x, j, k, bits = 0; if (i < Axes_Lib_BDraw_ABuff_BitN) { for (j = 0, k = i * 32; j < 32; j++) bits = Axes_Lib_BDraw_ABuff_Assign_Bits(k + j, j, bits); Axes_Lib_BDraw_ABuff_Bits[i] = bits; } }
  public virtual IEnumerator Axes_Lib_BDraw_ABuff_GetSums_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI)
  {
    uint i = id.x, c, s, j, k, bits = 0;
    if (i < Axes_Lib_BDraw_ABuff_BitN) { for (j = 0, k = i * 32; j < 32; j++) bits = Axes_Lib_BDraw_ABuff_Assign_Bits(k + j, j, bits); Axes_Lib_BDraw_ABuff_Bits[i] = bits; c = countbits(bits); } else c = 0;
    Axes_Lib_BDraw_ABuff_grp0[grpI] = c; Axes_Lib_BDraw_ABuff_grp[grpI] = c; yield return GroupMemoryBarrierWithGroupSync();
    for (s = 1; s < numthreads1; s *= 2)
    {
      if (grpI >= s && i < Axes_Lib_BDraw_ABuff_BitN) Axes_Lib_BDraw_ABuff_grp[grpI] = Axes_Lib_BDraw_ABuff_grp0[grpI] + Axes_Lib_BDraw_ABuff_grp0[grpI - s]; yield return GroupMemoryBarrierWithGroupSync();
      Axes_Lib_BDraw_ABuff_grp0[grpI] = Axes_Lib_BDraw_ABuff_grp[grpI]; yield return GroupMemoryBarrierWithGroupSync();
    }
    if (i < Axes_Lib_BDraw_ABuff_BitN) Axes_Lib_BDraw_ABuff_Sums[i] = Axes_Lib_BDraw_ABuff_grp[grpI];
  }
  public virtual IEnumerator Axes_Lib_BDraw_ABuff_Get_Bits_Sums_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI)
  {
    uint i = id.x, c = i < Axes_Lib_BDraw_ABuff_BitN ? countbits(Axes_Lib_BDraw_ABuff_Bits[i]) : 0, s;
    Axes_Lib_BDraw_ABuff_grp0[grpI] = c; Axes_Lib_BDraw_ABuff_grp[grpI] = c; yield return GroupMemoryBarrierWithGroupSync();
    for (s = 1; s < numthreads1; s *= 2)
    {
      if (grpI >= s && i < Axes_Lib_BDraw_ABuff_BitN) Axes_Lib_BDraw_ABuff_grp[grpI] = Axes_Lib_BDraw_ABuff_grp0[grpI] + Axes_Lib_BDraw_ABuff_grp0[grpI - s]; yield return GroupMemoryBarrierWithGroupSync();
      Axes_Lib_BDraw_ABuff_grp0[grpI] = Axes_Lib_BDraw_ABuff_grp[grpI]; yield return GroupMemoryBarrierWithGroupSync();
    }
    if (i < Axes_Lib_BDraw_ABuff_BitN) Axes_Lib_BDraw_ABuff_Sums[i] = Axes_Lib_BDraw_ABuff_grp[grpI];
  }
  public virtual IEnumerator Axes_Lib_BDraw_ABuff_GetFills1_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI)
  {
    uint i = id.x, k = i + 1, j = k * numthreads1 - 1, c = i < Axes_Lib_BDraw_ABuff_BitN1 - 1 ? Axes_Lib_BDraw_ABuff_Sums[j] : i < Axes_Lib_BDraw_ABuff_BitN1 ? Axes_Lib_BDraw_ABuff_Sums[Axes_Lib_BDraw_ABuff_BitN - 1] : 0, s;
    Axes_Lib_BDraw_ABuff_grp0[grpI] = c; Axes_Lib_BDraw_ABuff_grp[grpI] = c; yield return GroupMemoryBarrierWithGroupSync();
    for (s = 1; s < numthreads1; s *= 2)
    {
      if (grpI >= s && i < Axes_Lib_BDraw_ABuff_BitN1) Axes_Lib_BDraw_ABuff_grp[grpI] = Axes_Lib_BDraw_ABuff_grp0[grpI] + Axes_Lib_BDraw_ABuff_grp0[grpI - s]; yield return GroupMemoryBarrierWithGroupSync();
      Axes_Lib_BDraw_ABuff_grp0[grpI] = Axes_Lib_BDraw_ABuff_grp[grpI]; yield return GroupMemoryBarrierWithGroupSync();
    }
    if (i < Axes_Lib_BDraw_ABuff_BitN1) Axes_Lib_BDraw_ABuff_Fills1[i] = Axes_Lib_BDraw_ABuff_grp[grpI];
  }
  public virtual IEnumerator Axes_Lib_BDraw_ABuff_GetFills2_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI)
  {
    uint i = id.x, k = i + 1, j = k * numthreads1 - 1, c = i < Axes_Lib_BDraw_ABuff_BitN2 - 1 ? Axes_Lib_BDraw_ABuff_Fills1[j] : i < Axes_Lib_BDraw_ABuff_BitN2 ? Axes_Lib_BDraw_ABuff_Fills1[Axes_Lib_BDraw_ABuff_BitN1 - 1] : 0, s;
    Axes_Lib_BDraw_ABuff_grp0[grpI] = c; Axes_Lib_BDraw_ABuff_grp[grpI] = c; yield return GroupMemoryBarrierWithGroupSync();
    for (s = 1; s < numthreads1; s *= 2)
    {
      if (grpI >= s && i < Axes_Lib_BDraw_ABuff_BitN2) Axes_Lib_BDraw_ABuff_grp[grpI] = Axes_Lib_BDraw_ABuff_grp0[grpI] + Axes_Lib_BDraw_ABuff_grp0[grpI - s]; yield return GroupMemoryBarrierWithGroupSync();
      Axes_Lib_BDraw_ABuff_grp0[grpI] = Axes_Lib_BDraw_ABuff_grp[grpI]; yield return GroupMemoryBarrierWithGroupSync();
    }
    if (i < Axes_Lib_BDraw_ABuff_BitN2) Axes_Lib_BDraw_ABuff_Fills2[i] = Axes_Lib_BDraw_ABuff_grp[grpI];
  }
  public virtual void Axes_Lib_BDraw_ABuff_IncFills1_GS(uint3 id) { uint i = id.x; if (i >= numthreads1) Axes_Lib_BDraw_ABuff_Fills1[i] += Axes_Lib_BDraw_ABuff_Fills2[i / numthreads1 - 1]; }
  public virtual void Axes_Lib_BDraw_ABuff_IncSums_GS(uint3 id) { uint i = id.x; if (i >= numthreads1) Axes_Lib_BDraw_ABuff_Sums[i] += Axes_Lib_BDraw_ABuff_Fills1[i / numthreads1 - 1]; if (i == Axes_Lib_BDraw_ABuff_BitN - 1) Axes_Lib_BDraw_ABuff_IndexN = Axes_Lib_BDraw_ABuff_Sums[i]; }
  public virtual void Axes_Lib_BDraw_ABuff_GetIndexes_GS(uint3 id) { uint i = id.x, j, sum = i == 0 ? 0 : Axes_Lib_BDraw_ABuff_Sums[i - 1], b, i32 = i << 5, k; for (k = 0, b = Axes_Lib_BDraw_ABuff_Bits[i]; b > 0; k++) { j = (uint)findLSB(b); Axes_Lib_BDraw_ABuff_Indexes[sum + k] = i32 + j; b = SetBitu(b, j, 0); } }

  public virtual void base_Axes_Lib_BDraw_ABuff_Start0_GS() { }
  public virtual void base_Axes_Lib_BDraw_ABuff_Start1_GS() { }
  public virtual void base_Axes_Lib_BDraw_ABuff_OnApplicationQuit() { Save_UI(); OnApplicationQuit_GS(); base.OnApplicationQuit(); already_quited = true; }
  public virtual void base_Axes_Lib_BDraw_ABuff_LateUpdate0_GS() { }
  public virtual void base_Axes_Lib_BDraw_ABuff_LateUpdate1_GS() { }
  public virtual void base_Axes_Lib_BDraw_ABuff_Update0_GS() { }
  public virtual void base_Axes_Lib_BDraw_ABuff_Update1_GS() { }
  public virtual void base_Axes_Lib_BDraw_ABuff_OnValueChanged_GS() { }
  public virtual void base_Axes_Lib_BDraw_ABuff_InitBuffers0_GS() { }
  public virtual void base_Axes_Lib_BDraw_ABuff_InitBuffers1_GS() { }
  [HideInInspector]
  public virtual void base_Axes_Lib_BDraw_ABuff_onRenderObject_GS(ref bool render, ref bool cpu) { }
  public virtual float4 base_frag_Axes_Lib_BDraw_ABuff_GS(v2f i, float4 color) { return color; }
  public virtual void Axes_Lib_BDraw_ABuff_InitBuffers0_GS() { }
  public virtual void Axes_Lib_BDraw_ABuff_InitBuffers1_GS() { }
  public virtual void Axes_Lib_BDraw_ABuff_LateUpdate0_GS() { }
  public virtual void Axes_Lib_BDraw_ABuff_LateUpdate1_GS() { }
  public virtual void Axes_Lib_BDraw_ABuff_Update0_GS() { }
  public virtual void Axes_Lib_BDraw_ABuff_Update1_GS() { }
  public virtual void Axes_Lib_BDraw_ABuff_Start0_GS() { }
  public virtual void Axes_Lib_BDraw_ABuff_Start1_GS() { }
  public virtual void Axes_Lib_BDraw_ABuff_OnValueChanged_GS() { }
  public virtual void Axes_Lib_BDraw_ABuff_OnApplicationQuit_GS() { }
  public virtual void Axes_Lib_BDraw_ABuff_onRenderObject_GS(ref bool render, ref bool cpu) { }
  public virtual float4 frag_Axes_Lib_BDraw_ABuff_GS(v2f i, float4 color)
  {
    return color;
  }
  public virtual void Axes_Lib_BDraw_LateUpdate0_GS() { }
  public virtual void Axes_Lib_BDraw_LateUpdate1_GS() { }
  public virtual void Axes_Lib_BDraw_Update0_GS() { }
  public virtual void Axes_Lib_BDraw_Update1_GS() { }
  public virtual void Axes_Lib_BDraw_Start0_GS() { }
  public virtual void Axes_Lib_BDraw_Start1_GS() { }
  public virtual void Axes_Lib_BDraw_OnValueChanged_GS() { }
  public virtual void Axes_Lib_BDraw_OnApplicationQuit_GS() { }
  public virtual void Axes_Lib_BDraw_onRenderObject_GS(ref bool render, ref bool cpu) { }
  public virtual void Axes_Lib_Update0_GS() { }
  public virtual void Axes_Lib_Update1_GS() { }
  public virtual void Axes_Lib_Start0_GS() { }
  public virtual void Axes_Lib_Start1_GS() { }
  public virtual void Axes_Lib_OnValueChanged_GS() { }
  public virtual void Axes_Lib_OnApplicationQuit_GS() { }
  public virtual void Axes_Lib_onRenderObject_GS(ref bool render, ref bool cpu) { }
  #endregion <Axes_Lib>
  #region <OCam_Lib>
  gsOCam_Lib _OCam_Lib; public gsOCam_Lib OCam_Lib => _OCam_Lib = _OCam_Lib ?? Add_Component_to_gameObject<gsOCam_Lib>();
  #endregion <OCam_Lib>
  #region <Views_Lib>
  gsViews_Lib _Views_Lib; public gsViews_Lib Views_Lib => _Views_Lib = _Views_Lib ?? Add_Component_to_gameObject<gsViews_Lib>();
  #endregion <Views_Lib>
}