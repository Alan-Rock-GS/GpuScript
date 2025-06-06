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
public class gsVGrid_Lib_Doc_ : GS, IVGrid_Lib, IViews_Lib
{
  [HideInInspector] public uiData data;
  [HideInInspector] public GVGrid_Lib_Doc g; public GVGrid_Lib_Doc G { get { gVGrid_Lib_Doc.GetData(); return gVGrid_Lib_Doc[0]; } }
  public void g_SetData() { if (gChanged && gVGrid_Lib_Doc != null) { gVGrid_Lib_Doc[0] = g; gVGrid_Lib_Doc.SetData(); gChanged = false; } }
  public virtual void VGrid_Lib_BDraw_tab_delimeted_text_SetKernels(bool reallocate = false) { if (VGrid_Lib_BDraw_tab_delimeted_text != null && (reallocate || VGrid_Lib_BDraw_tab_delimeted_text.reallocated)) { SetKernelValues(VGrid_Lib_BDraw_tab_delimeted_text, nameof(VGrid_Lib_BDraw_tab_delimeted_text), kernel_VGrid_Lib_BDraw_AppendBuff_GetSums, kernel_VGrid_Lib_BDraw_AppendBuff_Get_Bits, kernel_VGrid_Lib_BDraw_getTextInfo); VGrid_Lib_BDraw_tab_delimeted_text.reallocated = false; } }
  public virtual void VGrid_Lib_BDraw_textInfos_SetKernels(bool reallocate = false) { if (VGrid_Lib_BDraw_textInfos != null && (reallocate || VGrid_Lib_BDraw_textInfos.reallocated)) { SetKernelValues(VGrid_Lib_BDraw_textInfos, nameof(VGrid_Lib_BDraw_textInfos), kernel_VGrid_Lib_BDraw_setDefaultTextInfo, kernel_VGrid_Lib_BDraw_getTextInfo); VGrid_Lib_BDraw_textInfos.reallocated = false; } }
  public virtual void VGrid_Lib_BDraw_fontInfos_SetKernels(bool reallocate = false) { if (VGrid_Lib_BDraw_fontInfos != null && (reallocate || VGrid_Lib_BDraw_fontInfos.reallocated)) { SetKernelValues(VGrid_Lib_BDraw_fontInfos, nameof(VGrid_Lib_BDraw_fontInfos), kernel_VGrid_Lib_BDraw_getTextInfo); VGrid_Lib_BDraw_fontInfos.reallocated = false; } }
  public virtual void VGrid_Lib_depthColors_SetKernels(bool reallocate = false) { if (VGrid_Lib_depthColors != null && (reallocate || VGrid_Lib_depthColors.reallocated)) { SetKernelValues(VGrid_Lib_depthColors, nameof(VGrid_Lib_depthColors), kernel_VGrid_Lib_Grid_Simple_TraceRay, kernel_VGrid_Lib_Grid_TraceRay); VGrid_Lib_depthColors.reallocated = false; } }
  public virtual void VGrid_Lib_paletteBuffer_SetKernels(bool reallocate = false) { if (VGrid_Lib_paletteBuffer != null && (reallocate || VGrid_Lib_paletteBuffer.reallocated)) { SetKernelValues(VGrid_Lib_paletteBuffer, nameof(VGrid_Lib_paletteBuffer), kernel_VGrid_Lib_Grid_Simple_TraceRay, kernel_VGrid_Lib_Grid_TraceRay); VGrid_Lib_paletteBuffer.reallocated = false; } }
  public virtual void VGrid_Lib_BDraw_AppendBuff_Bits_SetKernels(bool reallocate = false) { if (VGrid_Lib_BDraw_AppendBuff_Bits != null && (reallocate || VGrid_Lib_BDraw_AppendBuff_Bits.reallocated)) { SetKernelValues(VGrid_Lib_BDraw_AppendBuff_Bits, nameof(VGrid_Lib_BDraw_AppendBuff_Bits), kernel_VGrid_Lib_BDraw_AppendBuff_GetIndexes, kernel_VGrid_Lib_BDraw_AppendBuff_Get_Bits_Sums, kernel_VGrid_Lib_BDraw_AppendBuff_GetSums, kernel_VGrid_Lib_BDraw_AppendBuff_Get_Bits); VGrid_Lib_BDraw_AppendBuff_Bits.reallocated = false; } }
  public virtual void VGrid_Lib_BDraw_AppendBuff_Sums_SetKernels(bool reallocate = false) { if (VGrid_Lib_BDraw_AppendBuff_Sums != null && (reallocate || VGrid_Lib_BDraw_AppendBuff_Sums.reallocated)) { SetKernelValues(VGrid_Lib_BDraw_AppendBuff_Sums, nameof(VGrid_Lib_BDraw_AppendBuff_Sums), kernel_VGrid_Lib_BDraw_AppendBuff_GetIndexes, kernel_VGrid_Lib_BDraw_AppendBuff_IncSums, kernel_VGrid_Lib_BDraw_AppendBuff_GetFills1, kernel_VGrid_Lib_BDraw_AppendBuff_Get_Bits_Sums, kernel_VGrid_Lib_BDraw_AppendBuff_GetSums); VGrid_Lib_BDraw_AppendBuff_Sums.reallocated = false; } }
  public virtual void VGrid_Lib_BDraw_AppendBuff_Indexes_SetKernels(bool reallocate = false) { if (VGrid_Lib_BDraw_AppendBuff_Indexes != null && (reallocate || VGrid_Lib_BDraw_AppendBuff_Indexes.reallocated)) { SetKernelValues(VGrid_Lib_BDraw_AppendBuff_Indexes, nameof(VGrid_Lib_BDraw_AppendBuff_Indexes), kernel_VGrid_Lib_BDraw_AppendBuff_GetIndexes, kernel_VGrid_Lib_BDraw_getTextInfo); VGrid_Lib_BDraw_AppendBuff_Indexes.reallocated = false; } }
  public virtual void VGrid_Lib_BDraw_AppendBuff_Fills1_SetKernels(bool reallocate = false) { if (VGrid_Lib_BDraw_AppendBuff_Fills1 != null && (reallocate || VGrid_Lib_BDraw_AppendBuff_Fills1.reallocated)) { SetKernelValues(VGrid_Lib_BDraw_AppendBuff_Fills1, nameof(VGrid_Lib_BDraw_AppendBuff_Fills1), kernel_VGrid_Lib_BDraw_AppendBuff_IncSums, kernel_VGrid_Lib_BDraw_AppendBuff_IncFills1, kernel_VGrid_Lib_BDraw_AppendBuff_GetFills2, kernel_VGrid_Lib_BDraw_AppendBuff_GetFills1); VGrid_Lib_BDraw_AppendBuff_Fills1.reallocated = false; } }
  public virtual void VGrid_Lib_BDraw_AppendBuff_Fills2_SetKernels(bool reallocate = false) { if (VGrid_Lib_BDraw_AppendBuff_Fills2 != null && (reallocate || VGrid_Lib_BDraw_AppendBuff_Fills2.reallocated)) { SetKernelValues(VGrid_Lib_BDraw_AppendBuff_Fills2, nameof(VGrid_Lib_BDraw_AppendBuff_Fills2), kernel_VGrid_Lib_BDraw_AppendBuff_IncFills1, kernel_VGrid_Lib_BDraw_AppendBuff_GetFills2); VGrid_Lib_BDraw_AppendBuff_Fills2.reallocated = false; } }
  public virtual void VGrid_Lib_Vals_SetKernels(bool reallocate = false) { if (VGrid_Lib_Vals != null && (reallocate || VGrid_Lib_Vals.reallocated)) { SetKernelValues(VGrid_Lib_Vals, nameof(VGrid_Lib_Vals), kernel_VGrid_Lib_Grid_Calc_Vals, kernel_VGrid_Lib_Grid_Simple_TraceRay, kernel_VGrid_Lib_Grid_TraceRay); VGrid_Lib_Vals.reallocated = false; } }
  public virtual void Gpu_VGrid_Lib_Grid_Calc_Vals() { g_SetData(); VGrid_Lib_Vals?.SetCpu(); VGrid_Lib_Vals_SetKernels(); Gpu(kernel_VGrid_Lib_Grid_Calc_Vals, VGrid_Lib_Grid_Calc_Vals, VGrid_Lib_nodeN); VGrid_Lib_Vals?.ResetWrite(); }
  public virtual void Cpu_VGrid_Lib_Grid_Calc_Vals() { VGrid_Lib_Vals?.GetGpu(); Cpu(VGrid_Lib_Grid_Calc_Vals, VGrid_Lib_nodeN); VGrid_Lib_Vals.SetData(); }
  public virtual void Cpu_VGrid_Lib_Grid_Calc_Vals(uint3 id) { VGrid_Lib_Vals?.GetGpu(); VGrid_Lib_Grid_Calc_Vals(id); VGrid_Lib_Vals.SetData(); }
  public virtual void Gpu_VGrid_Lib_BDraw_AppendBuff_GetIndexes() { g_SetData(); VGrid_Lib_BDraw_AppendBuff_Bits?.SetCpu(); VGrid_Lib_BDraw_AppendBuff_Bits_SetKernels(); VGrid_Lib_BDraw_AppendBuff_Sums?.SetCpu(); VGrid_Lib_BDraw_AppendBuff_Sums_SetKernels(); VGrid_Lib_BDraw_AppendBuff_Indexes_SetKernels(); Gpu(kernel_VGrid_Lib_BDraw_AppendBuff_GetIndexes, VGrid_Lib_BDraw_AppendBuff_GetIndexes, VGrid_Lib_BDraw_AppendBuff_BitN); VGrid_Lib_BDraw_AppendBuff_Indexes?.ResetWrite(); }
  public virtual void Cpu_VGrid_Lib_BDraw_AppendBuff_GetIndexes() { VGrid_Lib_BDraw_AppendBuff_Bits?.GetGpu(); VGrid_Lib_BDraw_AppendBuff_Sums?.GetGpu(); VGrid_Lib_BDraw_AppendBuff_Indexes?.GetGpu(); Cpu(VGrid_Lib_BDraw_AppendBuff_GetIndexes, VGrid_Lib_BDraw_AppendBuff_BitN); VGrid_Lib_BDraw_AppendBuff_Indexes.SetData(); }
  public virtual void Cpu_VGrid_Lib_BDraw_AppendBuff_GetIndexes(uint3 id) { VGrid_Lib_BDraw_AppendBuff_Bits?.GetGpu(); VGrid_Lib_BDraw_AppendBuff_Sums?.GetGpu(); VGrid_Lib_BDraw_AppendBuff_Indexes?.GetGpu(); VGrid_Lib_BDraw_AppendBuff_GetIndexes(id); VGrid_Lib_BDraw_AppendBuff_Indexes.SetData(); }
  public virtual void Gpu_VGrid_Lib_BDraw_AppendBuff_IncSums() { g_SetData(); VGrid_Lib_BDraw_AppendBuff_Sums?.SetCpu(); VGrid_Lib_BDraw_AppendBuff_Sums_SetKernels(); VGrid_Lib_BDraw_AppendBuff_Fills1?.SetCpu(); VGrid_Lib_BDraw_AppendBuff_Fills1_SetKernels(); gVGrid_Lib_Doc?.SetCpu(); Gpu(kernel_VGrid_Lib_BDraw_AppendBuff_IncSums, VGrid_Lib_BDraw_AppendBuff_IncSums, VGrid_Lib_BDraw_AppendBuff_BitN); VGrid_Lib_BDraw_AppendBuff_Sums?.ResetWrite(); g = G; }
  public virtual void Cpu_VGrid_Lib_BDraw_AppendBuff_IncSums() { VGrid_Lib_BDraw_AppendBuff_Sums?.GetGpu(); VGrid_Lib_BDraw_AppendBuff_Fills1?.GetGpu(); Cpu(VGrid_Lib_BDraw_AppendBuff_IncSums, VGrid_Lib_BDraw_AppendBuff_BitN); VGrid_Lib_BDraw_AppendBuff_Sums.SetData(); }
  public virtual void Cpu_VGrid_Lib_BDraw_AppendBuff_IncSums(uint3 id) { VGrid_Lib_BDraw_AppendBuff_Sums?.GetGpu(); VGrid_Lib_BDraw_AppendBuff_Fills1?.GetGpu(); VGrid_Lib_BDraw_AppendBuff_IncSums(id); VGrid_Lib_BDraw_AppendBuff_Sums.SetData(); }
  public virtual void Gpu_VGrid_Lib_BDraw_AppendBuff_IncFills1() { g_SetData(); VGrid_Lib_BDraw_AppendBuff_Fills1?.SetCpu(); VGrid_Lib_BDraw_AppendBuff_Fills1_SetKernels(); VGrid_Lib_BDraw_AppendBuff_Fills2?.SetCpu(); VGrid_Lib_BDraw_AppendBuff_Fills2_SetKernels(); Gpu(kernel_VGrid_Lib_BDraw_AppendBuff_IncFills1, VGrid_Lib_BDraw_AppendBuff_IncFills1, VGrid_Lib_BDraw_AppendBuff_BitN1); VGrid_Lib_BDraw_AppendBuff_Fills1?.ResetWrite(); }
  public virtual void Cpu_VGrid_Lib_BDraw_AppendBuff_IncFills1() { VGrid_Lib_BDraw_AppendBuff_Fills1?.GetGpu(); VGrid_Lib_BDraw_AppendBuff_Fills2?.GetGpu(); Cpu(VGrid_Lib_BDraw_AppendBuff_IncFills1, VGrid_Lib_BDraw_AppendBuff_BitN1); VGrid_Lib_BDraw_AppendBuff_Fills1.SetData(); }
  public virtual void Cpu_VGrid_Lib_BDraw_AppendBuff_IncFills1(uint3 id) { VGrid_Lib_BDraw_AppendBuff_Fills1?.GetGpu(); VGrid_Lib_BDraw_AppendBuff_Fills2?.GetGpu(); VGrid_Lib_BDraw_AppendBuff_IncFills1(id); VGrid_Lib_BDraw_AppendBuff_Fills1.SetData(); }
  public virtual void Gpu_VGrid_Lib_BDraw_AppendBuff_GetFills2() { g_SetData(); VGrid_Lib_BDraw_AppendBuff_Fills1?.SetCpu(); VGrid_Lib_BDraw_AppendBuff_Fills1_SetKernels(); VGrid_Lib_BDraw_AppendBuff_Fills2_SetKernels(); Gpu(kernel_VGrid_Lib_BDraw_AppendBuff_GetFills2, VGrid_Lib_BDraw_AppendBuff_GetFills2, VGrid_Lib_BDraw_AppendBuff_BitN2); VGrid_Lib_BDraw_AppendBuff_Fills2?.ResetWrite(); }
  public virtual IEnumerator Cpu_VGrid_Lib_BDraw_AppendBuff_GetFills2() { VGrid_Lib_BDraw_AppendBuff_Fills1?.GetGpu(); VGrid_Lib_BDraw_AppendBuff_Fills2?.GetGpu(); yield return StartCoroutine(Cpu_Coroutine(kernel_VGrid_Lib_BDraw_AppendBuff_GetFills2, VGrid_Lib_BDraw_AppendBuff_GetFills2, VGrid_Lib_BDraw_AppendBuff_BitN2)); }
  public virtual void Cpu_VGrid_Lib_BDraw_AppendBuff_GetFills2(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { VGrid_Lib_BDraw_AppendBuff_Fills1?.GetGpu(); VGrid_Lib_BDraw_AppendBuff_Fills2?.GetGpu(); VGrid_Lib_BDraw_AppendBuff_GetFills2(grp_tid, grp_id, id, grpI); VGrid_Lib_BDraw_AppendBuff_Fills2.SetData(); }
  public virtual void Gpu_VGrid_Lib_BDraw_AppendBuff_GetFills1() { g_SetData(); VGrid_Lib_BDraw_AppendBuff_Sums?.SetCpu(); VGrid_Lib_BDraw_AppendBuff_Sums_SetKernels(); VGrid_Lib_BDraw_AppendBuff_Fills1_SetKernels(); Gpu(kernel_VGrid_Lib_BDraw_AppendBuff_GetFills1, VGrid_Lib_BDraw_AppendBuff_GetFills1, VGrid_Lib_BDraw_AppendBuff_BitN1); VGrid_Lib_BDraw_AppendBuff_Fills1?.ResetWrite(); }
  public virtual IEnumerator Cpu_VGrid_Lib_BDraw_AppendBuff_GetFills1() { VGrid_Lib_BDraw_AppendBuff_Sums?.GetGpu(); VGrid_Lib_BDraw_AppendBuff_Fills1?.GetGpu(); yield return StartCoroutine(Cpu_Coroutine(kernel_VGrid_Lib_BDraw_AppendBuff_GetFills1, VGrid_Lib_BDraw_AppendBuff_GetFills1, VGrid_Lib_BDraw_AppendBuff_BitN1)); }
  public virtual void Cpu_VGrid_Lib_BDraw_AppendBuff_GetFills1(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { VGrid_Lib_BDraw_AppendBuff_Sums?.GetGpu(); VGrid_Lib_BDraw_AppendBuff_Fills1?.GetGpu(); VGrid_Lib_BDraw_AppendBuff_GetFills1(grp_tid, grp_id, id, grpI); VGrid_Lib_BDraw_AppendBuff_Fills1.SetData(); }
  public virtual void Gpu_VGrid_Lib_BDraw_AppendBuff_Get_Bits_Sums() { g_SetData(); VGrid_Lib_BDraw_AppendBuff_Bits?.SetCpu(); VGrid_Lib_BDraw_AppendBuff_Bits_SetKernels(); VGrid_Lib_BDraw_AppendBuff_Sums_SetKernels(); Gpu(kernel_VGrid_Lib_BDraw_AppendBuff_Get_Bits_Sums, VGrid_Lib_BDraw_AppendBuff_Get_Bits_Sums, VGrid_Lib_BDraw_AppendBuff_BitN); VGrid_Lib_BDraw_AppendBuff_Sums?.ResetWrite(); }
  public virtual IEnumerator Cpu_VGrid_Lib_BDraw_AppendBuff_Get_Bits_Sums() { VGrid_Lib_BDraw_AppendBuff_Bits?.GetGpu(); VGrid_Lib_BDraw_AppendBuff_Sums?.GetGpu(); yield return StartCoroutine(Cpu_Coroutine(kernel_VGrid_Lib_BDraw_AppendBuff_Get_Bits_Sums, VGrid_Lib_BDraw_AppendBuff_Get_Bits_Sums, VGrid_Lib_BDraw_AppendBuff_BitN)); }
  public virtual void Cpu_VGrid_Lib_BDraw_AppendBuff_Get_Bits_Sums(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { VGrid_Lib_BDraw_AppendBuff_Bits?.GetGpu(); VGrid_Lib_BDraw_AppendBuff_Sums?.GetGpu(); VGrid_Lib_BDraw_AppendBuff_Get_Bits_Sums(grp_tid, grp_id, id, grpI); VGrid_Lib_BDraw_AppendBuff_Sums.SetData(); }
  public virtual void Gpu_VGrid_Lib_BDraw_AppendBuff_GetSums() { g_SetData(); VGrid_Lib_BDraw_AppendBuff_Bits_SetKernels(); VGrid_Lib_BDraw_AppendBuff_Sums_SetKernels(); VGrid_Lib_BDraw_tab_delimeted_text?.SetCpu(); VGrid_Lib_BDraw_tab_delimeted_text_SetKernels(); Gpu(kernel_VGrid_Lib_BDraw_AppendBuff_GetSums, VGrid_Lib_BDraw_AppendBuff_GetSums, VGrid_Lib_BDraw_AppendBuff_BitN); VGrid_Lib_BDraw_AppendBuff_Bits?.ResetWrite(); VGrid_Lib_BDraw_AppendBuff_Sums?.ResetWrite(); }
  public virtual IEnumerator Cpu_VGrid_Lib_BDraw_AppendBuff_GetSums() { VGrid_Lib_BDraw_AppendBuff_Bits?.GetGpu(); VGrid_Lib_BDraw_AppendBuff_Sums?.GetGpu(); VGrid_Lib_BDraw_tab_delimeted_text?.GetGpu(); yield return StartCoroutine(Cpu_Coroutine(kernel_VGrid_Lib_BDraw_AppendBuff_GetSums, VGrid_Lib_BDraw_AppendBuff_GetSums, VGrid_Lib_BDraw_AppendBuff_BitN)); }
  public virtual void Cpu_VGrid_Lib_BDraw_AppendBuff_GetSums(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { VGrid_Lib_BDraw_AppendBuff_Bits?.GetGpu(); VGrid_Lib_BDraw_AppendBuff_Sums?.GetGpu(); VGrid_Lib_BDraw_tab_delimeted_text?.GetGpu(); VGrid_Lib_BDraw_AppendBuff_GetSums(grp_tid, grp_id, id, grpI); VGrid_Lib_BDraw_AppendBuff_Bits.SetData(); VGrid_Lib_BDraw_AppendBuff_Sums.SetData(); }
  public virtual void Gpu_VGrid_Lib_BDraw_AppendBuff_Get_Bits() { g_SetData(); VGrid_Lib_BDraw_AppendBuff_Bits_SetKernels(); VGrid_Lib_BDraw_tab_delimeted_text?.SetCpu(); VGrid_Lib_BDraw_tab_delimeted_text_SetKernels(); Gpu(kernel_VGrid_Lib_BDraw_AppendBuff_Get_Bits, VGrid_Lib_BDraw_AppendBuff_Get_Bits, VGrid_Lib_BDraw_AppendBuff_BitN); VGrid_Lib_BDraw_AppendBuff_Bits?.ResetWrite(); }
  public virtual void Cpu_VGrid_Lib_BDraw_AppendBuff_Get_Bits() { VGrid_Lib_BDraw_AppendBuff_Bits?.GetGpu(); VGrid_Lib_BDraw_tab_delimeted_text?.GetGpu(); Cpu(VGrid_Lib_BDraw_AppendBuff_Get_Bits, VGrid_Lib_BDraw_AppendBuff_BitN); VGrid_Lib_BDraw_AppendBuff_Bits.SetData(); }
  public virtual void Cpu_VGrid_Lib_BDraw_AppendBuff_Get_Bits(uint3 id) { VGrid_Lib_BDraw_AppendBuff_Bits?.GetGpu(); VGrid_Lib_BDraw_tab_delimeted_text?.GetGpu(); VGrid_Lib_BDraw_AppendBuff_Get_Bits(id); VGrid_Lib_BDraw_AppendBuff_Bits.SetData(); }
  public virtual void Gpu_VGrid_Lib_BDraw_setDefaultTextInfo() { g_SetData(); VGrid_Lib_BDraw_textInfos?.SetCpu(); VGrid_Lib_BDraw_textInfos_SetKernels(); Gpu(kernel_VGrid_Lib_BDraw_setDefaultTextInfo, VGrid_Lib_BDraw_setDefaultTextInfo, VGrid_Lib_BDraw_textN); VGrid_Lib_BDraw_textInfos?.ResetWrite(); }
  public virtual void Cpu_VGrid_Lib_BDraw_setDefaultTextInfo() { VGrid_Lib_BDraw_textInfos?.GetGpu(); Cpu(VGrid_Lib_BDraw_setDefaultTextInfo, VGrid_Lib_BDraw_textN); VGrid_Lib_BDraw_textInfos.SetData(); }
  public virtual void Cpu_VGrid_Lib_BDraw_setDefaultTextInfo(uint3 id) { VGrid_Lib_BDraw_textInfos?.GetGpu(); VGrid_Lib_BDraw_setDefaultTextInfo(id); VGrid_Lib_BDraw_textInfos.SetData(); }
  public virtual void Gpu_VGrid_Lib_BDraw_getTextInfo() { g_SetData(); VGrid_Lib_BDraw_fontInfos?.SetCpu(); VGrid_Lib_BDraw_fontInfos_SetKernels(); VGrid_Lib_BDraw_textInfos?.SetCpu(); VGrid_Lib_BDraw_textInfos_SetKernels(); VGrid_Lib_BDraw_AppendBuff_Indexes?.SetCpu(); VGrid_Lib_BDraw_AppendBuff_Indexes_SetKernels(); VGrid_Lib_BDraw_tab_delimeted_text?.SetCpu(); VGrid_Lib_BDraw_tab_delimeted_text_SetKernels(); Gpu(kernel_VGrid_Lib_BDraw_getTextInfo, VGrid_Lib_BDraw_getTextInfo, VGrid_Lib_BDraw_textN); VGrid_Lib_BDraw_textInfos?.ResetWrite(); }
  public virtual void Cpu_VGrid_Lib_BDraw_getTextInfo() { VGrid_Lib_BDraw_fontInfos?.GetGpu(); VGrid_Lib_BDraw_textInfos?.GetGpu(); VGrid_Lib_BDraw_AppendBuff_Indexes?.GetGpu(); VGrid_Lib_BDraw_tab_delimeted_text?.GetGpu(); Cpu(VGrid_Lib_BDraw_getTextInfo, VGrid_Lib_BDraw_textN); VGrid_Lib_BDraw_textInfos.SetData(); }
  public virtual void Cpu_VGrid_Lib_BDraw_getTextInfo(uint3 id) { VGrid_Lib_BDraw_fontInfos?.GetGpu(); VGrid_Lib_BDraw_textInfos?.GetGpu(); VGrid_Lib_BDraw_AppendBuff_Indexes?.GetGpu(); VGrid_Lib_BDraw_tab_delimeted_text?.GetGpu(); VGrid_Lib_BDraw_getTextInfo(id); VGrid_Lib_BDraw_textInfos.SetData(); }
  public virtual void Gpu_VGrid_Lib_Grid_Simple_TraceRay() { g_SetData(); VGrid_Lib_Vals?.SetCpu(); VGrid_Lib_Vals_SetKernels(); VGrid_Lib_paletteBuffer?.SetCpu(); VGrid_Lib_paletteBuffer_SetKernels(); VGrid_Lib_depthColors_SetKernels(); Gpu(kernel_VGrid_Lib_Grid_Simple_TraceRay, VGrid_Lib_Grid_Simple_TraceRay, VGrid_Lib_viewSize); VGrid_Lib_Vals?.ResetWrite(); VGrid_Lib_depthColors?.ResetWrite(); }
  public virtual void Cpu_VGrid_Lib_Grid_Simple_TraceRay() { VGrid_Lib_Vals?.GetGpu(); VGrid_Lib_paletteBuffer?.GetGpu(); VGrid_Lib_depthColors?.GetGpu(); Cpu(VGrid_Lib_Grid_Simple_TraceRay, VGrid_Lib_viewSize); VGrid_Lib_Vals.SetData(); VGrid_Lib_depthColors.SetData(); }
  public virtual void Cpu_VGrid_Lib_Grid_Simple_TraceRay(uint3 id) { VGrid_Lib_Vals?.GetGpu(); VGrid_Lib_paletteBuffer?.GetGpu(); VGrid_Lib_depthColors?.GetGpu(); VGrid_Lib_Grid_Simple_TraceRay(id); VGrid_Lib_Vals.SetData(); VGrid_Lib_depthColors.SetData(); }
  public virtual void Gpu_VGrid_Lib_Grid_TraceRay() { g_SetData(); VGrid_Lib_Vals?.SetCpu(); VGrid_Lib_Vals_SetKernels(); VGrid_Lib_paletteBuffer?.SetCpu(); VGrid_Lib_paletteBuffer_SetKernels(); VGrid_Lib_depthColors_SetKernels(); Gpu(kernel_VGrid_Lib_Grid_TraceRay, VGrid_Lib_Grid_TraceRay, VGrid_Lib_viewSize); VGrid_Lib_Vals?.ResetWrite(); VGrid_Lib_depthColors?.ResetWrite(); }
  public virtual void Cpu_VGrid_Lib_Grid_TraceRay() { VGrid_Lib_Vals?.GetGpu(); VGrid_Lib_paletteBuffer?.GetGpu(); VGrid_Lib_depthColors?.GetGpu(); Cpu(VGrid_Lib_Grid_TraceRay, VGrid_Lib_viewSize); VGrid_Lib_Vals.SetData(); VGrid_Lib_depthColors.SetData(); }
  public virtual void Cpu_VGrid_Lib_Grid_TraceRay(uint3 id) { VGrid_Lib_Vals?.GetGpu(); VGrid_Lib_paletteBuffer?.GetGpu(); VGrid_Lib_depthColors?.GetGpu(); VGrid_Lib_Grid_TraceRay(id); VGrid_Lib_Vals.SetData(); VGrid_Lib_depthColors.SetData(); }
  [JsonConverter(typeof(StringEnumConverter))] public enum VGrid_Lib_BDraw_Draw : uint { Point, Sphere, Line, Arrow, Signal, LineSegment, Texture_2D, Quad, WebCam, Mesh, Number, N }
  [JsonConverter(typeof(StringEnumConverter))] public enum VGrid_Lib_BDraw_TextAlignment : uint { BottomLeft, CenterLeft, TopLeft, BottomCenter, CenterCenter, TopCenter, BottomRight, CenterRight, TopRight }
  [JsonConverter(typeof(StringEnumConverter))] public enum VGrid_Lib_BDraw_Text_QuadType : uint { FrontOnly, FrontBack, Switch, Arrow, Billboard }
  [JsonConverter(typeof(StringEnumConverter))] public enum VGrid_Lib_PaletteType : uint { Rainbow, GradientRainbow, GradientRainbow10, GradientRainbow20, Heat, GradientHeat, WhiteRainbow, invRainbow, Green, Gray, DarkGray, CT }
  [JsonConverter(typeof(StringEnumConverter))] public enum Views_Lib_ProjectionMode : uint { Automatic, Perspective, Orthographic }
  public const uint VGrid_Lib_BDraw_Draw_Point = 0, VGrid_Lib_BDraw_Draw_Sphere = 1, VGrid_Lib_BDraw_Draw_Line = 2, VGrid_Lib_BDraw_Draw_Arrow = 3, VGrid_Lib_BDraw_Draw_Signal = 4, VGrid_Lib_BDraw_Draw_LineSegment = 5, VGrid_Lib_BDraw_Draw_Texture_2D = 6, VGrid_Lib_BDraw_Draw_Quad = 7, VGrid_Lib_BDraw_Draw_WebCam = 8, VGrid_Lib_BDraw_Draw_Mesh = 9, VGrid_Lib_BDraw_Draw_Number = 10, VGrid_Lib_BDraw_Draw_N = 11;
  public const uint VGrid_Lib_BDraw_TextAlignment_BottomLeft = 0, VGrid_Lib_BDraw_TextAlignment_CenterLeft = 1, VGrid_Lib_BDraw_TextAlignment_TopLeft = 2, VGrid_Lib_BDraw_TextAlignment_BottomCenter = 3, VGrid_Lib_BDraw_TextAlignment_CenterCenter = 4, VGrid_Lib_BDraw_TextAlignment_TopCenter = 5, VGrid_Lib_BDraw_TextAlignment_BottomRight = 6, VGrid_Lib_BDraw_TextAlignment_CenterRight = 7, VGrid_Lib_BDraw_TextAlignment_TopRight = 8;
  public const uint VGrid_Lib_BDraw_Text_QuadType_FrontOnly = 0, VGrid_Lib_BDraw_Text_QuadType_FrontBack = 1, VGrid_Lib_BDraw_Text_QuadType_Switch = 2, VGrid_Lib_BDraw_Text_QuadType_Arrow = 3, VGrid_Lib_BDraw_Text_QuadType_Billboard = 4;
  public const uint VGrid_Lib_PaletteType_Rainbow = 0, VGrid_Lib_PaletteType_GradientRainbow = 1, VGrid_Lib_PaletteType_GradientRainbow10 = 2, VGrid_Lib_PaletteType_GradientRainbow20 = 3, VGrid_Lib_PaletteType_Heat = 4, VGrid_Lib_PaletteType_GradientHeat = 5, VGrid_Lib_PaletteType_WhiteRainbow = 6, VGrid_Lib_PaletteType_invRainbow = 7, VGrid_Lib_PaletteType_Green = 8, VGrid_Lib_PaletteType_Gray = 9, VGrid_Lib_PaletteType_DarkGray = 10, VGrid_Lib_PaletteType_CT = 11;
  public const uint Views_Lib_ProjectionMode_Automatic = 0, Views_Lib_ProjectionMode_Perspective = 1, Views_Lib_ProjectionMode_Orthographic = 2;
  public const uint VGrid_Lib_BDraw_Draw_Text3D = 12, VGrid_Lib_BDraw_LF = 10, VGrid_Lib_BDraw_TB = 9, VGrid_Lib_BDraw_ZERO = 48, VGrid_Lib_BDraw_NINE = 57, VGrid_Lib_BDraw_PERIOD = 46, VGrid_Lib_BDraw_COMMA = 44, VGrid_Lib_BDraw_PLUS = 43, VGrid_Lib_BDraw_MINUS = 45, VGrid_Lib_BDraw_SPACE = 32;
  [HideInInspector] public bool ValuesChanged, gChanged;
  public static gsVGrid_Lib_Doc This;
  public virtual void Awake() { This = this as gsVGrid_Lib_Doc; Awake_GS(); }
  public virtual void Awake_GS()
  {
    if(!GS_VGrid_Lib.CheckKey("you@gmail.com", "2024/12/10", 123456)) Status($"VGrid_Lib not registered, check email, expiration, and key in gsVGrid_Lib_Doc_GS class");
    if(!GS_OCam_Lib.CheckKey("you@gmail.com", "2024/12/10", 123456)) Status($"OCam_Lib not registered, check email, expiration, and key in gsVGrid_Lib_Doc_GS class");
    if(!GS_Views_Lib.CheckKey("you@gmail.com", "2024/12/10", 123456)) Status($"Views_Lib not registered, check email, expiration, and key in gsVGrid_Lib_Doc_GS class");
    if(!GS_Report_Lib.CheckKey("you@gmail.com", "2024/12/10", 123456)) Status($"Report_Lib not registered, check email, expiration, and key in gsVGrid_Lib_Doc_GS class");
  }
  public virtual void Start() { Build_UI(); Load_UI(); Start0_GS(); InitBuffers(); Start1_GS(); }
  public virtual void Start0_GS()
  {
    VGrid_Lib_Start0_GS();
    Views_Lib_Start0_GS();
  }
  public virtual void Start1_GS()
  {
    VGrid_Lib_Start1_GS();
    Views_Lib_Start1_GS();
  }
  public override void onLoaded()
  {
    base.onLoaded();
    GS_VGrid_Lib.onLoaded(this);
    GS_OCam_Lib.onLoaded(OCam_Lib);
    GS_Views_Lib.onLoaded(this);
    GS_Report_Lib.onLoaded(Report_Lib);
  }
  [HideInInspector] public bool already_quited = false;
  public override void OnApplicationQuit() { Save_UI(); OnApplicationQuit_GS(); base.OnApplicationQuit(); already_quited = true; }
  public virtual void OnApplicationQuit_GS()
  {
    VGrid_Lib_OnApplicationQuit_GS();
    Views_Lib_OnApplicationQuit_GS();
  }
  public class _UI_Views_Lib_CamView
  {
    public class UI_Views_Lib_CamView_Items
    {
      public gsVGrid_Lib_Doc_ gs;
      public int row;
      public UI_Views_Lib_CamView_Items(gsVGrid_Lib_Doc_ gs) { this.gs = gs; }
      public UI_string UI_viewName => gs.UI_grid_Views_Lib_CamViews_Lib.RowItems[row][0] as UI_string; public string viewName { get => UI_viewName.v; set => UI_viewName.v = value; }
      public UI_float3 UI_viewCenter => gs.UI_grid_Views_Lib_CamViews_Lib.RowItems[row][1] as UI_float3; public float3 viewCenter { get => UI_viewCenter.v; set => UI_viewCenter.v = value; }
      public UI_float UI_viewDist => gs.UI_grid_Views_Lib_CamViews_Lib.RowItems[row][2] as UI_float; public float viewDist { get => UI_viewDist.v; set => UI_viewDist.v = value; }
      public UI_float2 UI_viewTiltSpin => gs.UI_grid_Views_Lib_CamViews_Lib.RowItems[row][3] as UI_float2; public float2 viewTiltSpin { get => UI_viewTiltSpin.v; set => UI_viewTiltSpin.v = value; }
      public UI_enum UI_viewProjection => gs.UI_grid_Views_Lib_CamViews_Lib.RowItems[row][4] as UI_enum; public Views_Lib_ProjectionMode viewProjection { get => (Views_Lib_ProjectionMode)UI_viewProjection.v; set => UI_viewProjection.v = value.To_uint(); }
      public UI_float UI_view_sphere => gs.UI_grid_Views_Lib_CamViews_Lib.RowItems[row][5] as UI_float; public float view_sphere { get => UI_view_sphere.v; set => UI_view_sphere.v = value; }
      public UI_float UI_view_cube => gs.UI_grid_Views_Lib_CamViews_Lib.RowItems[row][6] as UI_float; public float view_cube { get => UI_view_cube.v; set => UI_view_cube.v = value; }
      public UI_float UI_view_torus => gs.UI_grid_Views_Lib_CamViews_Lib.RowItems[row][7] as UI_float; public float view_torus { get => UI_view_torus.v; set => UI_view_torus.v = value; }
      public UI_float UI_view_box => gs.UI_grid_Views_Lib_CamViews_Lib.RowItems[row][8] as UI_float; public float view_box { get => UI_view_box.v; set => UI_view_box.v = value; }
      public UI_float UI_view_roundBox => gs.UI_grid_Views_Lib_CamViews_Lib.RowItems[row][9] as UI_float; public float view_roundBox { get => UI_view_roundBox.v; set => UI_view_roundBox.v = value; }
      public UI_float UI_view_boxFrame => gs.UI_grid_Views_Lib_CamViews_Lib.RowItems[row][10] as UI_float; public float view_boxFrame { get => UI_view_boxFrame.v; set => UI_view_boxFrame.v = value; }
      public UI_bool UI_view_twoSided => gs.UI_grid_Views_Lib_CamViews_Lib.RowItems[row][11] as UI_bool; public bool view_twoSided { get => UI_view_twoSided.v; set => UI_view_twoSided.v = value; }
      public UI_float UI_view_meshVal => gs.UI_grid_Views_Lib_CamViews_Lib.RowItems[row][12] as UI_float; public float view_meshVal { get => UI_view_meshVal.v; set => UI_view_meshVal.v = value; }
      public UI_float2 UI_view_meshRange => gs.UI_grid_Views_Lib_CamViews_Lib.RowItems[row][13] as UI_float2; public float2 view_meshRange { get => UI_view_meshRange.v; set => UI_view_meshRange.v = value; }
    }
    public gsVGrid_Lib_Doc_ gs;
    public UI_Views_Lib_CamView_Items ui_Views_Lib_CamView_Items;
    public _UI_Views_Lib_CamView(gsVGrid_Lib_Doc_ gs) { this.gs = gs; ui_Views_Lib_CamView_Items = new UI_Views_Lib_CamView_Items(gs); }
    public UI_Views_Lib_CamView_Items this[int i] { get { ui_Views_Lib_CamView_Items.row = i; return ui_Views_Lib_CamView_Items; } }
  }
  public _UI_Views_Lib_CamView UI_Views_Lib_CamViews_Lib;
  public override void ui_to_data()
  {
    if (data == null) return;
    data.siUnits = siUnits;
    data.group_UI = group_UI;
    data.group_Test = group_Test;
    data.sphere = sphere;
    data.cube = cube;
    data.torus = torus;
    data.box = box;
    data.roundBox = roundBox;
    data.boxFrame = boxFrame;
    data.VGrid_Lib_group_VGrid_Lib = VGrid_Lib_group_VGrid_Lib;
    data.VGrid_Lib_group_Geometry = VGrid_Lib_group_Geometry;
    data.VGrid_Lib_drawGrid = VGrid_Lib_drawGrid;
    data.VGrid_Lib_GridX = VGrid_Lib_GridX;
    data.VGrid_Lib_GridY = VGrid_Lib_GridY;
    data.VGrid_Lib_GridZ = VGrid_Lib_GridZ;
    data.VGrid_Lib_resolution = VGrid_Lib_resolution;
    data.VGrid_Lib_group_Axes = VGrid_Lib_group_Axes;
    data.VGrid_Lib_drawBox = VGrid_Lib_drawBox;
    data.VGrid_Lib_boxLineThickness = VGrid_Lib_boxLineThickness;
    data.VGrid_Lib_drawAxes = VGrid_Lib_drawAxes;
    data.VGrid_Lib_customAxesRangeN = VGrid_Lib_customAxesRangeN;
    data.VGrid_Lib_axesRangeMin = VGrid_Lib_axesRangeMin;
    data.VGrid_Lib_axesRangeMax = VGrid_Lib_axesRangeMax;
    data.VGrid_Lib_axesRangeMin1 = VGrid_Lib_axesRangeMin1;
    data.VGrid_Lib_axesRangeMax1 = VGrid_Lib_axesRangeMax1;
    data.VGrid_Lib_axesRangeMin2 = VGrid_Lib_axesRangeMin2;
    data.VGrid_Lib_axesRangeMax2 = VGrid_Lib_axesRangeMax2;
    data.VGrid_Lib_titles = VGrid_Lib_titles;
    data.VGrid_Lib_axesFormats = VGrid_Lib_axesFormats;
    data.VGrid_Lib_textSize = VGrid_Lib_textSize;
    data.VGrid_Lib_axesColor = VGrid_Lib_axesColor;
    data.VGrid_Lib_axesOpacity = VGrid_Lib_axesOpacity;
    data.VGrid_Lib_zeroOrigin = VGrid_Lib_zeroOrigin;
    data.VGrid_Lib_group_Mesh = VGrid_Lib_group_Mesh;
    data.VGrid_Lib_drawSurface = VGrid_Lib_drawSurface;
    data.VGrid_Lib_GridDrawFront = VGrid_Lib_GridDrawFront;
    data.VGrid_Lib_GridDrawBack = VGrid_Lib_GridDrawBack;
    data.VGrid_Lib_show_slices = VGrid_Lib_show_slices;
    data.VGrid_Lib_slices = VGrid_Lib_slices;
    data.VGrid_Lib_sliceRotation = VGrid_Lib_sliceRotation;
    data.VGrid_Lib_GridLineThickness = VGrid_Lib_GridLineThickness;
    data.VGrid_Lib_opacity = VGrid_Lib_opacity;
    data.VGrid_Lib_paletteRange = VGrid_Lib_paletteRange;
    data.VGrid_Lib_paletteType = VGrid_Lib_paletteType;
    data.VGrid_Lib_twoSided = VGrid_Lib_twoSided;
    data.VGrid_Lib_meshVal = VGrid_Lib_meshVal;
    data.VGrid_Lib_meshRange = VGrid_Lib_meshRange;
    data.Views_Lib_group_CamViews_Lib = Views_Lib_group_CamViews_Lib;
    data.Views_Lib_CamViews_Lib = Views_Lib_CamViews_Lib;
    data.Views_Lib_CamViews_Lib_DisplayCols = UI_grid_Views_Lib_CamViews_Lib?.displayColumns?.Select(a => a.v).ToArray() ?? default;
    data.Views_Lib_CamViews_Lib_VScroll = UI_grid_Views_Lib_CamViews_Lib?.VScroll.value ?? default;
    data.Views_Lib_CamViews_Lib_DisplayRowN = UI_grid_Views_Lib_CamViews_Lib?.dispRowN.v ?? default;
    data.Views_Lib_CamViews_Lib_isExpanded = UI_grid_Views_Lib_CamViews_Lib?.isExpanded ?? default;
    data.Views_Lib_CamViews_Lib_lastClickedRow = UI_grid_Views_Lib_CamViews_Lib?.lastClickedRow ?? default;
    data.Views_Lib_CamViews_Lib_selectedRows = UI_grid_Views_Lib_CamViews_Lib?.isRowSelected.bools_to_RangeStr() ?? default;
  }
  public override void data_to_ui()
  {
    if (data == null) return;
    group_UI = data.group_UI;
    group_Test = data.group_Test;
    sphere = ui_txt_str.Contains("\"sphere\"") ? data.sphere : 1f;
    cube = ui_txt_str.Contains("\"cube\"") ? data.cube : 1f;
    torus = ui_txt_str.Contains("\"torus\"") ? data.torus : 0f;
    box = ui_txt_str.Contains("\"box\"") ? data.box : 0f;
    roundBox = ui_txt_str.Contains("\"roundBox\"") ? data.roundBox : 0f;
    boxFrame = ui_txt_str.Contains("\"boxFrame\"") ? data.boxFrame : 0f;
    VGrid_Lib_group_VGrid_Lib = data.VGrid_Lib_group_VGrid_Lib;
    VGrid_Lib_group_Geometry = data.VGrid_Lib_group_Geometry;
    VGrid_Lib_drawGrid = data.VGrid_Lib_drawGrid;
    VGrid_Lib_GridX = ui_txt_str.Contains("\"VGrid_Lib_GridX\"") ? data.VGrid_Lib_GridX : float2("0, 0.1");
    VGrid_Lib_GridY = ui_txt_str.Contains("\"VGrid_Lib_GridY\"") ? data.VGrid_Lib_GridY : float2("0, 0.1");
    VGrid_Lib_GridZ = ui_txt_str.Contains("\"VGrid_Lib_GridZ\"") ? data.VGrid_Lib_GridZ : float2("0, 0.1");
    VGrid_Lib_resolution = ui_txt_str.Contains("\"VGrid_Lib_resolution\"") ? data.VGrid_Lib_resolution : 0.1f;
    VGrid_Lib_group_Axes = data.VGrid_Lib_group_Axes;
    VGrid_Lib_drawBox = data.VGrid_Lib_drawBox;
    VGrid_Lib_boxLineThickness = ui_txt_str.Contains("\"VGrid_Lib_boxLineThickness\"") ? data.VGrid_Lib_boxLineThickness : 10f;
    VGrid_Lib_drawAxes = data.VGrid_Lib_drawAxes;
    VGrid_Lib_customAxesRangeN = ui_txt_str.Contains("\"VGrid_Lib_customAxesRangeN\"") ? data.VGrid_Lib_customAxesRangeN : 0;
    VGrid_Lib_axesRangeMin = data.VGrid_Lib_axesRangeMin;
    VGrid_Lib_axesRangeMax = data.VGrid_Lib_axesRangeMax;
    VGrid_Lib_axesRangeMin1 = data.VGrid_Lib_axesRangeMin1;
    VGrid_Lib_axesRangeMax1 = data.VGrid_Lib_axesRangeMax1;
    VGrid_Lib_axesRangeMin2 = data.VGrid_Lib_axesRangeMin2;
    VGrid_Lib_axesRangeMax2 = data.VGrid_Lib_axesRangeMax2;
    VGrid_Lib_titles = data.VGrid_Lib_titles;
    VGrid_Lib_axesFormats = data.VGrid_Lib_axesFormats;
    VGrid_Lib_textSize = ui_txt_str.Contains("\"VGrid_Lib_textSize\"") ? data.VGrid_Lib_textSize : float2("0.075");
    VGrid_Lib_axesColor = ui_txt_str.Contains("\"VGrid_Lib_axesColor\"") ? data.VGrid_Lib_axesColor : float3("0.5");
    VGrid_Lib_axesOpacity = ui_txt_str.Contains("\"VGrid_Lib_axesOpacity\"") ? data.VGrid_Lib_axesOpacity : 1f;
    VGrid_Lib_zeroOrigin = data.VGrid_Lib_zeroOrigin;
    VGrid_Lib_group_Mesh = data.VGrid_Lib_group_Mesh;
    VGrid_Lib_drawSurface = data.VGrid_Lib_drawSurface;
    VGrid_Lib_GridDrawFront = data.VGrid_Lib_GridDrawFront;
    VGrid_Lib_GridDrawBack = data.VGrid_Lib_GridDrawBack;
    VGrid_Lib_show_slices = data.VGrid_Lib_show_slices;
    VGrid_Lib_slices = ui_txt_str.Contains("\"VGrid_Lib_slices\"") ? data.VGrid_Lib_slices : float3("0");
    VGrid_Lib_sliceRotation = ui_txt_str.Contains("\"VGrid_Lib_sliceRotation\"") ? data.VGrid_Lib_sliceRotation : float3("0");
    VGrid_Lib_GridLineThickness = ui_txt_str.Contains("\"VGrid_Lib_GridLineThickness\"") ? data.VGrid_Lib_GridLineThickness : 10f;
    VGrid_Lib_opacity = ui_txt_str.Contains("\"VGrid_Lib_opacity\"") ? data.VGrid_Lib_opacity : 1f;
    VGrid_Lib_paletteRange = ui_txt_str.Contains("\"VGrid_Lib_paletteRange\"") ? data.VGrid_Lib_paletteRange : float2("0.0001, 1");
    VGrid_Lib_paletteType = data.VGrid_Lib_paletteType;
    VGrid_Lib_twoSided = data.VGrid_Lib_twoSided;
    VGrid_Lib_meshVal = ui_txt_str.Contains("\"VGrid_Lib_meshVal\"") ? data.VGrid_Lib_meshVal : 0.5f;
    VGrid_Lib_meshRange = ui_txt_str.Contains("\"VGrid_Lib_meshRange\"") ? data.VGrid_Lib_meshRange : float2("0.0001, 1");
    Views_Lib_group_CamViews_Lib = data.Views_Lib_group_CamViews_Lib;
    Views_Lib_CamViews_Lib = data.Views_Lib_CamViews_Lib;
    UI_Views_Lib_CamViews_Lib = new _UI_Views_Lib_CamView(this);
    "Views_Lib_CamViews_Lib_To_UI".InvokeMethod(this);
    for (int i = 0; i < data.Views_Lib_CamViews_Lib_DisplayCols?.Length && i < UI_grid_Views_Lib_CamViews_Lib.displayColumns.Count; i++)
    {
      UI_grid_Views_Lib_CamViews_Lib.displayColumns[i].v = data.Views_Lib_CamViews_Lib_DisplayCols[i];
      var item = UI_grid_Views_Lib_CamViews_Lib.RowItems[0][i];
      if(UI_grid_Views_Lib_CamViews_Lib.headerButtons[i].unitLabel != null)
        UI_grid_Views_Lib_CamViews_Lib.headerButtons[i].unitLabel.style.display = DisplayIf(item.siUnit != siUnit.Null || item.usUnit != usUnit.Null || item.Unit != Unit.Null);
    }
    UI_grid_Views_Lib_CamViews_Lib.VScroll.value = data.Views_Lib_CamViews_Lib_VScroll;
    UI_grid_Views_Lib_CamViews_Lib.dispRowN.v = data.Views_Lib_CamViews_Lib_DisplayRowN == 0 ? 20 : data.Views_Lib_CamViews_Lib_DisplayRowN;
    UI_grid_Views_Lib_CamViews_Lib.isExpanded = data.Views_Lib_CamViews_Lib_isExpanded;
    UI_grid_Views_Lib_CamViews_Lib.selectedRows = data.Views_Lib_CamViews_Lib_selectedRows;
    UI_grid_Views_Lib_CamViews_Lib.lastClickedRow = data.Views_Lib_CamViews_Lib_lastClickedRow;
    Views_Lib_CamViews_Lib ??= new Views_Lib_CamView[] { };
    UI_grid_Views_Lib_CamViews_Lib.isRowSelected = data.Views_Lib_CamViews_Lib_selectedRows.RangeStr_to_bools(Views_Lib_CamViews_Lib.Length);
    UI_grid_Views_Lib_CamViews_Lib.DrawGrid();
  }
  public List<Views_Lib_CamView> Views_Lib_CamViews_Lib_CopyPaste;
  public virtual void Views_Lib_CamViews_Lib_OnButtonClicked(int row, int col)
  {
    var UI_grid = UI_grid_Views_Lib_CamViews_Lib;
    var name = UI_grid.RowItems[row][col].name.After("UI_method_").BeforeLast("_");
    if (name == "SaveView") Views_Lib_CamViews_Lib_SaveView(UI_grid.StartRow + row);
    else if (name == "LoadView") Views_Lib_CamViews_Lib_LoadView(UI_grid.StartRow + row);
  }
  public virtual void Views_Lib_CamViews_Lib_OnRowNumberButtonClicked(int row) { }
  public virtual void Views_Lib_CamViews_Lib_OnHeaderButtonClicked(string label)
  {
    if (Views_Lib_CamViews_Lib.Length < 2) return;
    Views_Lib_CamViews_Lib = label switch
    {
      "Name" => Views_Lib_CamViews_Lib[0].viewName.CompareTo(Views_Lib_CamViews_Lib[^1].viewName) < 0 ? Views_Lib_CamViews_Lib.OrderByDescending(a => a.viewName).ToArray() : Views_Lib_CamViews_Lib.OrderBy(a => a.viewName).ToArray(),
      "Center" => Views_Lib_CamViews_Lib[0].viewCenter.CompareTo(Views_Lib_CamViews_Lib[^1].viewCenter) < 0 ? Views_Lib_CamViews_Lib.OrderByDescending(a => a.viewCenter).ToArray() : Views_Lib_CamViews_Lib.OrderBy(a => a.viewCenter).ToArray(),
      "Dist" => Views_Lib_CamViews_Lib[0].viewDist.CompareTo(Views_Lib_CamViews_Lib[^1].viewDist) < 0 ? Views_Lib_CamViews_Lib.OrderByDescending(a => a.viewDist).ToArray() : Views_Lib_CamViews_Lib.OrderBy(a => a.viewDist).ToArray(),
      "Tilt Spin" => Views_Lib_CamViews_Lib[0].viewTiltSpin.CompareTo(Views_Lib_CamViews_Lib[^1].viewTiltSpin) < 0 ? Views_Lib_CamViews_Lib.OrderByDescending(a => a.viewTiltSpin).ToArray() : Views_Lib_CamViews_Lib.OrderBy(a => a.viewTiltSpin).ToArray(),
      "Projection" => Views_Lib_CamViews_Lib[0].viewProjection.CompareTo(Views_Lib_CamViews_Lib[^1].viewProjection) < 0 ? Views_Lib_CamViews_Lib.OrderByDescending(a => a.viewProjection).ToArray() : Views_Lib_CamViews_Lib.OrderBy(a => a.viewProjection).ToArray(),
      "Sphere" => Views_Lib_CamViews_Lib[0].view_sphere.CompareTo(Views_Lib_CamViews_Lib[^1].view_sphere) < 0 ? Views_Lib_CamViews_Lib.OrderByDescending(a => a.view_sphere).ToArray() : Views_Lib_CamViews_Lib.OrderBy(a => a.view_sphere).ToArray(),
      "Cube" => Views_Lib_CamViews_Lib[0].view_cube.CompareTo(Views_Lib_CamViews_Lib[^1].view_cube) < 0 ? Views_Lib_CamViews_Lib.OrderByDescending(a => a.view_cube).ToArray() : Views_Lib_CamViews_Lib.OrderBy(a => a.view_cube).ToArray(),
      "Torus" => Views_Lib_CamViews_Lib[0].view_torus.CompareTo(Views_Lib_CamViews_Lib[^1].view_torus) < 0 ? Views_Lib_CamViews_Lib.OrderByDescending(a => a.view_torus).ToArray() : Views_Lib_CamViews_Lib.OrderBy(a => a.view_torus).ToArray(),
      "Box" => Views_Lib_CamViews_Lib[0].view_box.CompareTo(Views_Lib_CamViews_Lib[^1].view_box) < 0 ? Views_Lib_CamViews_Lib.OrderByDescending(a => a.view_box).ToArray() : Views_Lib_CamViews_Lib.OrderBy(a => a.view_box).ToArray(),
      "Round Box" => Views_Lib_CamViews_Lib[0].view_roundBox.CompareTo(Views_Lib_CamViews_Lib[^1].view_roundBox) < 0 ? Views_Lib_CamViews_Lib.OrderByDescending(a => a.view_roundBox).ToArray() : Views_Lib_CamViews_Lib.OrderBy(a => a.view_roundBox).ToArray(),
      "Box Frame" => Views_Lib_CamViews_Lib[0].view_boxFrame.CompareTo(Views_Lib_CamViews_Lib[^1].view_boxFrame) < 0 ? Views_Lib_CamViews_Lib.OrderByDescending(a => a.view_boxFrame).ToArray() : Views_Lib_CamViews_Lib.OrderBy(a => a.view_boxFrame).ToArray(),
      "2-sided" => Views_Lib_CamViews_Lib[0].view_twoSided.CompareTo(Views_Lib_CamViews_Lib[^1].view_twoSided) < 0 ? Views_Lib_CamViews_Lib.OrderByDescending(a => a.view_twoSided).ToArray() : Views_Lib_CamViews_Lib.OrderBy(a => a.view_twoSided).ToArray(),
      "Value" => Views_Lib_CamViews_Lib[0].view_meshVal.CompareTo(Views_Lib_CamViews_Lib[^1].view_meshVal) < 0 ? Views_Lib_CamViews_Lib.OrderByDescending(a => a.view_meshVal).ToArray() : Views_Lib_CamViews_Lib.OrderBy(a => a.view_meshVal).ToArray(),
      "Mesh Range" => Views_Lib_CamViews_Lib[0].view_meshRange.CompareTo(Views_Lib_CamViews_Lib[^1].view_meshRange) < 0 ? Views_Lib_CamViews_Lib.OrderByDescending(a => a.view_meshRange).ToArray() : Views_Lib_CamViews_Lib.OrderBy(a => a.view_meshRange).ToArray(),
      _ => default,
    };
    Views_Lib_CamViews_Lib_To_UI();
  }
  public virtual void UI_To_Views_Lib_CamViews_Lib()
  {
    if (Views_Lib_CamViews_Lib == null) return;
    var UI_grid = UI_grid_Views_Lib_CamViews_Lib;
    int startRow = UI_grid.StartRow, i1 = min(UI_grid.DisplayRowN, Views_Lib_CamViews_Lib.Length - startRow);
    i1 = min(i1, UI_grid.RowItems.Count);
    for (int i = 0; i < i1; i++)
    {
      var ui = UI_Views_Lib_CamViews_Lib[i];
      var row = Views_Lib_CamViews_Lib[i + startRow];
      row.viewName = ui.viewName;
      row.viewCenter = ui.viewCenter;
      row.viewDist = ui.viewDist;
      row.viewTiltSpin = ui.viewTiltSpin;
      row.viewProjection = (uint)ui.viewProjection;
      row.view_sphere = ui.view_sphere;
      row.view_cube = ui.view_cube;
      row.view_torus = ui.view_torus;
      row.view_box = ui.view_box;
      row.view_roundBox = ui.view_roundBox;
      row.view_boxFrame = ui.view_boxFrame;
      row.view_twoSided = ui.view_twoSided.To_uint();
      row.view_meshVal = ui.view_meshVal;
      row.view_meshRange = ui.view_meshRange;
      Views_Lib_CamViews_Lib[i + startRow] = row;
    }
  }
  protected bool in_Views_Lib_CamViews_Lib_To_UI = false;
  public virtual bool Views_Lib_CamViews_Lib_To_UI()
  {
    if (Views_Lib_CamViews_Lib == null || in_Views_Lib_CamViews_Lib_To_UI) return false;
    isGridBuilding = true;
    in_Views_Lib_CamViews_Lib_To_UI = true;
    var UI_grid = UI_grid_Views_Lib_CamViews_Lib;
    int startRow = UI_grid.StartRow, i1 = min(UI_grid.DisplayRowN, Views_Lib_CamViews_Lib.Length - startRow);
    i1 = min(i1, UI_grid.RowItems.Count);
    for (int i = 0; i < i1; i++)
    {
      var ui = UI_Views_Lib_CamViews_Lib[i];
      var row = Views_Lib_CamViews_Lib[i + startRow];
      ui.viewName = row.viewName;
      ui.viewCenter = row.viewCenter;
      ui.viewDist = row.viewDist;
      ui.viewTiltSpin = row.viewTiltSpin;
      ui.viewProjection = (Views_Lib_ProjectionMode)row.viewProjection;
      ui.view_sphere = row.view_sphere;
      ui.view_cube = row.view_cube;
      ui.view_torus = row.view_torus;
      ui.view_box = row.view_box;
      ui.view_roundBox = row.view_roundBox;
      ui.view_boxFrame = row.view_boxFrame;
      ui.view_twoSided = row.view_twoSided.To_bool();
      ui.view_meshVal = row.view_meshVal;
      ui.view_meshRange = row.view_meshRange;
    }
    UI_grid.DrawGrid();
    in_Views_Lib_CamViews_Lib_To_UI = false;
    isGridBuilding = false;
    return true;
  }
  public const int Views_Lib_CamViews_Lib_viewName_Col = 0, Views_Lib_CamViews_Lib_viewCenter_Col = 1, Views_Lib_CamViews_Lib_viewDist_Col = 2, Views_Lib_CamViews_Lib_viewTiltSpin_Col = 3, Views_Lib_CamViews_Lib_viewProjection_Col = 4, Views_Lib_CamViews_Lib_view_sphere_Col = 5, Views_Lib_CamViews_Lib_view_cube_Col = 6, Views_Lib_CamViews_Lib_view_torus_Col = 7, Views_Lib_CamViews_Lib_view_box_Col = 8, Views_Lib_CamViews_Lib_view_roundBox_Col = 9, Views_Lib_CamViews_Lib_view_boxFrame_Col = 10, Views_Lib_CamViews_Lib_view_twoSided_Col = 11, Views_Lib_CamViews_Lib_view_meshVal_Col = 12, Views_Lib_CamViews_Lib_view_meshRange_Col = 13;
  public virtual bool Views_Lib_CamViews_Lib_ShowIf(int row, int col)
  {
    return true;
  }
  public virtual void base_Views_Lib_CamViews_Lib_OnAddRow() { var list = Views_Lib_CamViews_Lib.ToList(); list.Add(default); Views_Lib_CamViews_Lib = list.ToArray(); }
  public virtual void Views_Lib_CamViews_Lib_viewName_OnValueChanged(int row, string previousValue) { }
  public virtual void Views_Lib_CamViews_Lib_viewCenter_OnValueChanged(int row, float3 previousValue) { }
  public virtual void Views_Lib_CamViews_Lib_viewDist_OnValueChanged(int row, float previousValue) { }
  public virtual void Views_Lib_CamViews_Lib_viewTiltSpin_OnValueChanged(int row, float2 previousValue) { }
  public virtual void Views_Lib_CamViews_Lib_viewProjection_OnValueChanged(int row, Views_Lib_ProjectionMode previousValue) { }
  public virtual void Views_Lib_CamViews_Lib_view_sphere_OnValueChanged(int row, float previousValue) { }
  public virtual void Views_Lib_CamViews_Lib_view_cube_OnValueChanged(int row, float previousValue) { }
  public virtual void Views_Lib_CamViews_Lib_view_torus_OnValueChanged(int row, float previousValue) { }
  public virtual void Views_Lib_CamViews_Lib_view_box_OnValueChanged(int row, float previousValue) { }
  public virtual void Views_Lib_CamViews_Lib_view_roundBox_OnValueChanged(int row, float previousValue) { }
  public virtual void Views_Lib_CamViews_Lib_view_boxFrame_OnValueChanged(int row, float previousValue) { }
  public virtual void Views_Lib_CamViews_Lib_view_twoSided_OnValueChanged(int row, bool previousValue) { }
  public virtual void Views_Lib_CamViews_Lib_view_meshVal_OnValueChanged(int row, float previousValue) { }
  public virtual void Views_Lib_CamViews_Lib_view_meshRange_OnValueChanged(int row, float2 previousValue) { }
  public virtual void Views_Lib_CamViews_Lib_OnValueChanged(int row, int col)
  {
    if (!ui_loaded) return;
    var UI_grid = UI_grid_Views_Lib_CamViews_Lib;
    int startRow = UI_grid.StartRow;
    var ui = UI_Views_Lib_CamViews_Lib[row];
    if (row + startRow >= Views_Lib_CamViews_Lib.Length) return;
    var data = Views_Lib_CamViews_Lib[row + startRow];
    if (col == Views_Lib_CamViews_Lib_viewName_Col && data.viewName != ui.viewName) { var v = data.viewName; data.viewName = ui.viewName; Views_Lib_CamViews_Lib[row + startRow] = data; Views_Lib_CamViews_Lib_viewName_OnValueChanged(row + startRow, v); }
    else if (col == Views_Lib_CamViews_Lib_viewCenter_Col && any(data.viewCenter != ui.viewCenter)) { var v = data.viewCenter; data.viewCenter = ui.viewCenter; Views_Lib_CamViews_Lib[row + startRow] = data; Views_Lib_CamViews_Lib_viewCenter_OnValueChanged(row + startRow, v); }
    else if (col == Views_Lib_CamViews_Lib_viewDist_Col && any(data.viewDist != ui.viewDist)) { var v = data.viewDist; data.viewDist = ui.viewDist; Views_Lib_CamViews_Lib[row + startRow] = data; Views_Lib_CamViews_Lib_viewDist_OnValueChanged(row + startRow, v); }
    else if (col == Views_Lib_CamViews_Lib_viewTiltSpin_Col && any(data.viewTiltSpin != ui.viewTiltSpin)) { var v = data.viewTiltSpin; data.viewTiltSpin = ui.viewTiltSpin; Views_Lib_CamViews_Lib[row + startRow] = data; Views_Lib_CamViews_Lib_viewTiltSpin_OnValueChanged(row + startRow, v); }
    else if (col == Views_Lib_CamViews_Lib_viewProjection_Col && (Views_Lib_ProjectionMode)data.viewProjection != ui.viewProjection) { var v = (Views_Lib_ProjectionMode)data.viewProjection; data.viewProjection = (uint)ui.viewProjection; Views_Lib_CamViews_Lib[row + startRow] = data; Views_Lib_CamViews_Lib_viewProjection_OnValueChanged(row + startRow, v); }
    else if (col == Views_Lib_CamViews_Lib_view_sphere_Col && any(data.view_sphere != ui.view_sphere)) { var v = data.view_sphere; data.view_sphere = ui.view_sphere; Views_Lib_CamViews_Lib[row + startRow] = data; Views_Lib_CamViews_Lib_view_sphere_OnValueChanged(row + startRow, v); }
    else if (col == Views_Lib_CamViews_Lib_view_cube_Col && any(data.view_cube != ui.view_cube)) { var v = data.view_cube; data.view_cube = ui.view_cube; Views_Lib_CamViews_Lib[row + startRow] = data; Views_Lib_CamViews_Lib_view_cube_OnValueChanged(row + startRow, v); }
    else if (col == Views_Lib_CamViews_Lib_view_torus_Col && any(data.view_torus != ui.view_torus)) { var v = data.view_torus; data.view_torus = ui.view_torus; Views_Lib_CamViews_Lib[row + startRow] = data; Views_Lib_CamViews_Lib_view_torus_OnValueChanged(row + startRow, v); }
    else if (col == Views_Lib_CamViews_Lib_view_box_Col && any(data.view_box != ui.view_box)) { var v = data.view_box; data.view_box = ui.view_box; Views_Lib_CamViews_Lib[row + startRow] = data; Views_Lib_CamViews_Lib_view_box_OnValueChanged(row + startRow, v); }
    else if (col == Views_Lib_CamViews_Lib_view_roundBox_Col && any(data.view_roundBox != ui.view_roundBox)) { var v = data.view_roundBox; data.view_roundBox = ui.view_roundBox; Views_Lib_CamViews_Lib[row + startRow] = data; Views_Lib_CamViews_Lib_view_roundBox_OnValueChanged(row + startRow, v); }
    else if (col == Views_Lib_CamViews_Lib_view_boxFrame_Col && any(data.view_boxFrame != ui.view_boxFrame)) { var v = data.view_boxFrame; data.view_boxFrame = ui.view_boxFrame; Views_Lib_CamViews_Lib[row + startRow] = data; Views_Lib_CamViews_Lib_view_boxFrame_OnValueChanged(row + startRow, v); }
    else if (col == Views_Lib_CamViews_Lib_view_twoSided_Col && data.view_twoSided != ui.view_twoSided.To_uint()) { var v = data.view_twoSided; data.view_twoSided = ui.view_twoSided.To_uint(); Views_Lib_CamViews_Lib[row + startRow] = data; Views_Lib_CamViews_Lib_view_twoSided_OnValueChanged(row + startRow, v.To_bool()); }
    else if (col == Views_Lib_CamViews_Lib_view_meshVal_Col && any(data.view_meshVal != ui.view_meshVal)) { var v = data.view_meshVal; data.view_meshVal = ui.view_meshVal; Views_Lib_CamViews_Lib[row + startRow] = data; Views_Lib_CamViews_Lib_view_meshVal_OnValueChanged(row + startRow, v); }
    else if (col == Views_Lib_CamViews_Lib_view_meshRange_Col && any(data.view_meshRange != ui.view_meshRange)) { var v = data.view_meshRange; data.view_meshRange = ui.view_meshRange; Views_Lib_CamViews_Lib[row + startRow] = data; Views_Lib_CamViews_Lib_view_meshRange_OnValueChanged(row + startRow, v); }
  }
  public virtual int Views_Lib_CamViews_Lib_GetGridArrayLength() => Views_Lib_CamViews_Lib?.Length ?? 0;
  public virtual int Views_Lib_CamViews_Lib_SelectedRow
  {
    get { for (int i = 0; i < Views_Lib_CamViews_Lib.Length; i++) if (UI_grid_Views_Lib_CamViews_Lib.isRowSelected[i]) return i; return -1; }
    set
    {
      for (int i = 0; i < Views_Lib_CamViews_Lib.Length; i++) UI_grid_Views_Lib_CamViews_Lib.isRowSelected[i] = value == i;
      UI_grid_Views_Lib_CamViews_Lib.lastClickedRow = value;
      UI_grid_Views_Lib_CamViews_Lib.DrawGrid();
    }
  }
  public virtual int[] Views_Lib_CamViews_Lib_SelectedRows
  {
    get => UI_grid_Views_Lib_CamViews_Lib.isRowSelected.Select((a, i) => new { a, i }).Where(a => a.a).Select(a => a.i).ToArray();
    set
    {
      for (int i = 0; i < Views_Lib_CamViews_Lib.Length; i++) UI_grid_Views_Lib_CamViews_Lib.isRowSelected[i] = false;
      for (int i = 0; i < value.Length; i++) { int row = value[i]; UI_grid_Views_Lib_CamViews_Lib.isRowSelected[row] = true; UI_grid_Views_Lib_CamViews_Lib.lastClickedRow = row; }
      UI_grid_Views_Lib_CamViews_Lib.DrawGrid();
    }
  }
  public virtual void Views_Lib_CamViews_Lib_OnCut()
  {
    Views_Lib_CamViews_Lib_OnCopy();
    Views_Lib_CamViews_Lib = Views_Lib_CamViews_Lib.Except(Views_Lib_CamViews_Lib_CopyPaste).ToArray();
    UI_grid_Views_Lib_CamViews_Lib.StartRow = min(UI_grid_Views_Lib_CamViews_Lib.StartRow, max(0, Views_Lib_CamViews_Lib_CopyPaste.Count - UI_grid_Views_Lib_CamViews_Lib.DisplayRowN));
    UI_grid_Views_Lib_CamViews_Lib.isRowSelected = new bool[Views_Lib_CamViews_Lib_CopyPaste.Count];
    UI_grid_Views_Lib_CamViews_Lib.DrawGrid();
  }
  public virtual void Views_Lib_CamViews_Lib_OnCopy() { Views_Lib_CamViews_Lib_CopyPaste = Views_Lib_CamViews_Lib.Where((a, i) => UI_grid_Views_Lib_CamViews_Lib.isRowSelected[i]).Select(a => a).ToList(); }
  public virtual void Views_Lib_CamViews_Lib_OnPaste()
  {
    var list = Views_Lib_CamViews_Lib.ToList();
    var newSelectedRows = new List<int>();
    for (int i = Views_Lib_CamViews_Lib.Length - 1; i >= 0; i--)
      if (UI_grid_Views_Lib_CamViews_Lib.isRowSelected[i])
      {
        for (int j = 0; j < Views_Lib_CamViews_Lib_CopyPaste.Count; j++) { list.Insert(i + j, Views_Lib_CamViews_Lib_CopyPaste[j]); newSelectedRows.Add(i + j); }
        break;
      }
    Views_Lib_CamViews_Lib = list.ToArray();
    UI_grid_Views_Lib_CamViews_Lib.isRowSelected = newSelectedRows.ToArray().ints_to_selected_bools(list.Count);
    Views_Lib_CamViews_Lib_To_UI();
  }
  public virtual void Views_Lib_CamViews_Lib_OnInsert()
  {
    var list = Views_Lib_CamViews_Lib.ToList();
    var newSelectedRows = new List<int>();
    for (int i = 0; i < Views_Lib_CamViews_Lib.Length; i++) if (UI_grid_Views_Lib_CamViews_Lib.isRowSelected[i]) { list.Insert(i, default); newSelectedRows.Add(i); }
    Views_Lib_CamViews_Lib = list.ToArray();
    UI_grid_Views_Lib_CamViews_Lib.isRowSelected = newSelectedRows.ToArray().ints_to_selected_bools(list.Count);
    Views_Lib_CamViews_Lib_To_UI();
  }
  public virtual void Views_Lib_CamViews_Lib_OnDelete() { Views_Lib_CamViews_Lib_OnCut(); }
  public virtual void Views_Lib_CamViews_Lib_OnUpArrow()
  {
    int row = 1;
    if (Views_Lib_CamViews_Lib.Length > 1 && !UI_grid_Views_Lib_CamViews_Lib.isRowSelected[0])
    {
      var list = Views_Lib_CamViews_Lib.ToList();
      var newSelectedRows = new List<int>();
      for (int i = 0; i < Views_Lib_CamViews_Lib.Length; i++)
        if (UI_grid_Views_Lib_CamViews_Lib.isRowSelected[i]) { var item = list[i]; list.RemoveAt(i); list.Insert(i - 1, item); newSelectedRows.Add(i - 1); }
      Views_Lib_CamViews_Lib = list.ToArray();
      UI_grid_Views_Lib_CamViews_Lib.isRowSelected = newSelectedRows.ToArray().ints_to_selected_bools(list.Count);
      Views_Lib_CamViews_Lib_To_UI();
      row = clamp(newSelectedRows[0], 0, UI_grid_Views_Lib_CamViews_Lib.rowNumberButtons.Count - 1);
    }
    var b = UI_grid_Views_Lib_CamViews_Lib.rowNumberButtons[row]; b.schedule.Execute(() => { b.Focus(); });
  }
  public virtual void Views_Lib_CamViews_Lib_OnDownArrow()
  {
    var row = ^1;
    if (Views_Lib_CamViews_Lib.Length > 1 && !UI_grid_Views_Lib_CamViews_Lib.isRowSelected[^1])
    {
      var list = Views_Lib_CamViews_Lib.ToList();
      var newSelectedRows = new List<int>();
      for (int i = Views_Lib_CamViews_Lib.Length - 1; i >= 0; i--)
        if (UI_grid_Views_Lib_CamViews_Lib.isRowSelected[i]) { var item = list[i]; list.RemoveAt(i); list.Insert(i + 1, item); newSelectedRows.Add(i + 1); }
      Views_Lib_CamViews_Lib = list.ToArray();
      UI_grid_Views_Lib_CamViews_Lib.isRowSelected = newSelectedRows.ToArray().ints_to_selected_bools(list.Count);
      Views_Lib_CamViews_Lib_To_UI();
      row = clamp(newSelectedRows[0], 0, UI_grid_Views_Lib_CamViews_Lib.rowNumberButtons.Count - 1);
    }
    var b = UI_grid_Views_Lib_CamViews_Lib.rowNumberButtons[row]; b.schedule.Execute(() => { b.Focus(); });
  }
  public virtual void Views_Lib_CamViews_Lib_OnKeyDown(KeyDownEvent evt)
  {
    //print($"Views_Lib_CamViews_Lib_OnKeyDown, {(evt.shiftKey ? "Shift-" : "")}{(evt.ctrlKey ? "Ctrl-" : "")}{(evt.altKey ? "Alt-" : "")}{evt.keyCode.ToString()}");
  }
  public virtual void base_Views_Lib_CamViews_Lib_SaveView(int row) { }
  public virtual void base_Views_Lib_CamViews_Lib_LoadView(int row) { }
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
    if (UI_sphere.Changed || sphere != UI_sphere.v) sphere = UI_sphere.v;
    if (UI_cube.Changed || cube != UI_cube.v) cube = UI_cube.v;
    if (UI_torus.Changed || torus != UI_torus.v) torus = UI_torus.v;
    if (UI_box.Changed || box != UI_box.v) box = UI_box.v;
    if (UI_roundBox.Changed || roundBox != UI_roundBox.v) roundBox = UI_roundBox.v;
    if (UI_boxFrame.Changed || boxFrame != UI_boxFrame.v) boxFrame = UI_boxFrame.v;
    if (UI_VGrid_Lib_drawGrid.Changed || VGrid_Lib_drawGrid != UI_VGrid_Lib_drawGrid.v) VGrid_Lib_drawGrid = UI_VGrid_Lib_drawGrid.v;
    if (UI_VGrid_Lib_GridX.Changed || any(VGrid_Lib_GridX != UI_VGrid_Lib_GridX.v)) VGrid_Lib_GridX = UI_VGrid_Lib_GridX.v;
    if (UI_VGrid_Lib_GridY.Changed || any(VGrid_Lib_GridY != UI_VGrid_Lib_GridY.v)) VGrid_Lib_GridY = UI_VGrid_Lib_GridY.v;
    if (UI_VGrid_Lib_GridZ.Changed || any(VGrid_Lib_GridZ != UI_VGrid_Lib_GridZ.v)) VGrid_Lib_GridZ = UI_VGrid_Lib_GridZ.v;
    if (UI_VGrid_Lib_resolution.Changed || VGrid_Lib_resolution != UI_VGrid_Lib_resolution.v) VGrid_Lib_resolution = UI_VGrid_Lib_resolution.v;
    if (UI_VGrid_Lib_drawBox.Changed || VGrid_Lib_drawBox != UI_VGrid_Lib_drawBox.v) VGrid_Lib_drawBox = UI_VGrid_Lib_drawBox.v;
    if (UI_VGrid_Lib_boxLineThickness.Changed || VGrid_Lib_boxLineThickness != UI_VGrid_Lib_boxLineThickness.v) VGrid_Lib_boxLineThickness = UI_VGrid_Lib_boxLineThickness.v;
    if (UI_VGrid_Lib_drawAxes.Changed || VGrid_Lib_drawAxes != UI_VGrid_Lib_drawAxes.v) VGrid_Lib_drawAxes = UI_VGrid_Lib_drawAxes.v;
    if (UI_VGrid_Lib_customAxesRangeN.Changed || VGrid_Lib_customAxesRangeN != UI_VGrid_Lib_customAxesRangeN.v) VGrid_Lib_customAxesRangeN = UI_VGrid_Lib_customAxesRangeN.v;
    if (UI_VGrid_Lib_axesRangeMin.Changed || any(VGrid_Lib_axesRangeMin != UI_VGrid_Lib_axesRangeMin.v)) VGrid_Lib_axesRangeMin = UI_VGrid_Lib_axesRangeMin.v;
    if (UI_VGrid_Lib_axesRangeMax.Changed || any(VGrid_Lib_axesRangeMax != UI_VGrid_Lib_axesRangeMax.v)) VGrid_Lib_axesRangeMax = UI_VGrid_Lib_axesRangeMax.v;
    if (UI_VGrid_Lib_axesRangeMin1.Changed || any(VGrid_Lib_axesRangeMin1 != UI_VGrid_Lib_axesRangeMin1.v)) VGrid_Lib_axesRangeMin1 = UI_VGrid_Lib_axesRangeMin1.v;
    if (UI_VGrid_Lib_axesRangeMax1.Changed || any(VGrid_Lib_axesRangeMax1 != UI_VGrid_Lib_axesRangeMax1.v)) VGrid_Lib_axesRangeMax1 = UI_VGrid_Lib_axesRangeMax1.v;
    if (UI_VGrid_Lib_axesRangeMin2.Changed || any(VGrid_Lib_axesRangeMin2 != UI_VGrid_Lib_axesRangeMin2.v)) VGrid_Lib_axesRangeMin2 = UI_VGrid_Lib_axesRangeMin2.v;
    if (UI_VGrid_Lib_axesRangeMax2.Changed || any(VGrid_Lib_axesRangeMax2 != UI_VGrid_Lib_axesRangeMax2.v)) VGrid_Lib_axesRangeMax2 = UI_VGrid_Lib_axesRangeMax2.v;
    if (UI_VGrid_Lib_titles.Changed || VGrid_Lib_titles != UI_VGrid_Lib_titles.v) { data.VGrid_Lib_titles = UI_VGrid_Lib_titles.v; ValuesChanged = gChanged = true; }
    if (UI_VGrid_Lib_axesFormats.Changed || VGrid_Lib_axesFormats != UI_VGrid_Lib_axesFormats.v) { data.VGrid_Lib_axesFormats = UI_VGrid_Lib_axesFormats.v; ValuesChanged = gChanged = true; }
    if (UI_VGrid_Lib_textSize.Changed || any(VGrid_Lib_textSize != UI_VGrid_Lib_textSize.v)) VGrid_Lib_textSize = UI_VGrid_Lib_textSize.v;
    if (UI_VGrid_Lib_axesColor.Changed || any(VGrid_Lib_axesColor != UI_VGrid_Lib_axesColor.v)) VGrid_Lib_axesColor = UI_VGrid_Lib_axesColor.v;
    if (UI_VGrid_Lib_axesOpacity.Changed || VGrid_Lib_axesOpacity != UI_VGrid_Lib_axesOpacity.v) VGrid_Lib_axesOpacity = UI_VGrid_Lib_axesOpacity.v;
    if (UI_VGrid_Lib_zeroOrigin.Changed || VGrid_Lib_zeroOrigin != UI_VGrid_Lib_zeroOrigin.v) VGrid_Lib_zeroOrigin = UI_VGrid_Lib_zeroOrigin.v;
    if (UI_VGrid_Lib_drawSurface.Changed || VGrid_Lib_drawSurface != UI_VGrid_Lib_drawSurface.v) VGrid_Lib_drawSurface = UI_VGrid_Lib_drawSurface.v;
    if (UI_VGrid_Lib_GridDrawFront.Changed || VGrid_Lib_GridDrawFront != UI_VGrid_Lib_GridDrawFront.v) VGrid_Lib_GridDrawFront = UI_VGrid_Lib_GridDrawFront.v;
    if (UI_VGrid_Lib_GridDrawBack.Changed || VGrid_Lib_GridDrawBack != UI_VGrid_Lib_GridDrawBack.v) VGrid_Lib_GridDrawBack = UI_VGrid_Lib_GridDrawBack.v;
    if (UI_VGrid_Lib_show_slices.Changed || VGrid_Lib_show_slices != UI_VGrid_Lib_show_slices.v) VGrid_Lib_show_slices = UI_VGrid_Lib_show_slices.v;
    if (UI_VGrid_Lib_slices.Changed || any(VGrid_Lib_slices != UI_VGrid_Lib_slices.v)) VGrid_Lib_slices = UI_VGrid_Lib_slices.v;
    if (UI_VGrid_Lib_sliceRotation.Changed || any(VGrid_Lib_sliceRotation != UI_VGrid_Lib_sliceRotation.v)) VGrid_Lib_sliceRotation = UI_VGrid_Lib_sliceRotation.v;
    if (UI_VGrid_Lib_GridLineThickness.Changed || VGrid_Lib_GridLineThickness != UI_VGrid_Lib_GridLineThickness.v) VGrid_Lib_GridLineThickness = UI_VGrid_Lib_GridLineThickness.v;
    if (UI_VGrid_Lib_opacity.Changed || VGrid_Lib_opacity != UI_VGrid_Lib_opacity.v) VGrid_Lib_opacity = UI_VGrid_Lib_opacity.v;
    if (UI_VGrid_Lib_paletteRange.Changed || any(VGrid_Lib_paletteRange != UI_VGrid_Lib_paletteRange.v)) VGrid_Lib_paletteRange = UI_VGrid_Lib_paletteRange.v;
    if (UI_VGrid_Lib_paletteType.Changed || (uint)VGrid_Lib_paletteType != UI_VGrid_Lib_paletteType.v) VGrid_Lib_paletteType = (VGrid_Lib_PaletteType)UI_VGrid_Lib_paletteType.v;
    if (UI_VGrid_Lib_twoSided.Changed || VGrid_Lib_twoSided != UI_VGrid_Lib_twoSided.v) VGrid_Lib_twoSided = UI_VGrid_Lib_twoSided.v;
    if (UI_VGrid_Lib_meshVal.Changed || VGrid_Lib_meshVal != UI_VGrid_Lib_meshVal.v) VGrid_Lib_meshVal = UI_VGrid_Lib_meshVal.v;
    if (UI_VGrid_Lib_meshRange.Changed || any(VGrid_Lib_meshRange != UI_VGrid_Lib_meshRange.v)) VGrid_Lib_meshRange = UI_VGrid_Lib_meshRange.v;
    if (ValuesChanged) { gChanged = true; OnValueChanged(); ValuesChanged = UI_group_UI.Changed = UI_group_Test.Changed = UI_sphere.Changed = UI_cube.Changed = UI_torus.Changed = UI_box.Changed = UI_roundBox.Changed = UI_boxFrame.Changed = UI_VGrid_Lib_group_VGrid_Lib.Changed = UI_VGrid_Lib_group_Geometry.Changed = UI_VGrid_Lib_drawGrid.Changed = UI_VGrid_Lib_GridX.Changed = UI_VGrid_Lib_GridY.Changed = UI_VGrid_Lib_GridZ.Changed = UI_VGrid_Lib_resolution.Changed = UI_VGrid_Lib_group_Axes.Changed = UI_VGrid_Lib_drawBox.Changed = UI_VGrid_Lib_boxLineThickness.Changed = UI_VGrid_Lib_drawAxes.Changed = UI_VGrid_Lib_customAxesRangeN.Changed = UI_VGrid_Lib_axesRangeMin.Changed = UI_VGrid_Lib_axesRangeMax.Changed = UI_VGrid_Lib_axesRangeMin1.Changed = UI_VGrid_Lib_axesRangeMax1.Changed = UI_VGrid_Lib_axesRangeMin2.Changed = UI_VGrid_Lib_axesRangeMax2.Changed = UI_VGrid_Lib_titles.Changed = UI_VGrid_Lib_axesFormats.Changed = UI_VGrid_Lib_textSize.Changed = UI_VGrid_Lib_axesColor.Changed = UI_VGrid_Lib_axesOpacity.Changed = UI_VGrid_Lib_zeroOrigin.Changed = UI_VGrid_Lib_group_Mesh.Changed = UI_VGrid_Lib_drawSurface.Changed = UI_VGrid_Lib_GridDrawFront.Changed = UI_VGrid_Lib_GridDrawBack.Changed = UI_VGrid_Lib_show_slices.Changed = UI_VGrid_Lib_slices.Changed = UI_VGrid_Lib_sliceRotation.Changed = UI_VGrid_Lib_GridLineThickness.Changed = UI_VGrid_Lib_opacity.Changed = UI_VGrid_Lib_paletteRange.Changed = UI_VGrid_Lib_paletteType.Changed = UI_VGrid_Lib_twoSided.Changed = UI_VGrid_Lib_meshVal.Changed = UI_VGrid_Lib_meshRange.Changed = UI_Views_Lib_group_CamViews_Lib.Changed = false; }
    VGrid_Lib_LateUpdate1_GS();
    Views_Lib_LateUpdate1_GS();
    LateUpdate1_GS();
    if(lateUpdateI++ == uint_max) lateUpdateI = 100;
  }
  public virtual void LateUpdate0_GS()
  {
    VGrid_Lib_LateUpdate0_GS();
    Views_Lib_LateUpdate0_GS();
  }
  public virtual void LateUpdate1_GS()
  {
    VGrid_Lib_LateUpdate1_GS();
    Views_Lib_LateUpdate1_GS();
  }
  public override void Update()
  {
    base.Update();
    if (!ui_loaded) return;
    Update0_GS();
    VGrid_Lib_Update1_GS();
    Views_Lib_Update1_GS();
    Update1_GS();
  }
  public virtual void Update0_GS()
  {
    VGrid_Lib_Update0_GS();
    Views_Lib_Update0_GS();
  }
  public virtual void Update1_GS()
  {
    VGrid_Lib_Update1_GS();
    Views_Lib_Update1_GS();
  }
  public override void OnValueChanged()
  {
    if (!ui_loaded) return;
    OnValueChanged_GS();
    if (UI_sphere.Changed) { VGrid_Lib_reCalc = true; }
    if (UI_cube.Changed) { VGrid_Lib_reCalc = true; }
    if (UI_torus.Changed) { VGrid_Lib_reCalc = true; }
    if (UI_box.Changed) { VGrid_Lib_reCalc = true; }
    if (UI_roundBox.Changed) { VGrid_Lib_reCalc = true; }
    if (UI_boxFrame.Changed) { VGrid_Lib_reCalc = true; }
    if (UI_VGrid_Lib_drawGrid.Changed) { VGrid_Lib_retrace = VGrid_Lib_buildText = true; }
    if (UI_VGrid_Lib_GridX.Changed) { VGrid_Lib_reCalc = VGrid_Lib_buildText = true; }
    UI_VGrid_Lib_GridX.DisplayIf(Show_VGrid_Lib_GridX && UI_VGrid_Lib_GridX.treeGroup_parent.isExpanded);
    if (UI_VGrid_Lib_GridY.Changed) { VGrid_Lib_reCalc = VGrid_Lib_buildText = true; }
    UI_VGrid_Lib_GridY.DisplayIf(Show_VGrid_Lib_GridY && UI_VGrid_Lib_GridY.treeGroup_parent.isExpanded);
    if (UI_VGrid_Lib_GridZ.Changed) { VGrid_Lib_reCalc = VGrid_Lib_buildText = true; }
    UI_VGrid_Lib_GridZ.DisplayIf(Show_VGrid_Lib_GridZ && UI_VGrid_Lib_GridZ.treeGroup_parent.isExpanded);
    if (UI_VGrid_Lib_resolution.Changed) { VGrid_Lib_reCalc = true; }
    UI_VGrid_Lib_resolution.DisplayIf(Show_VGrid_Lib_resolution && UI_VGrid_Lib_resolution.treeGroup_parent.isExpanded);
    if (UI_VGrid_Lib_drawBox.Changed) { VGrid_Lib_buildText = true; }
    UI_VGrid_Lib_drawBox.DisplayIf(Show_VGrid_Lib_drawBox && UI_VGrid_Lib_drawBox.treeGroup_parent.isExpanded);
    if (UI_VGrid_Lib_boxLineThickness.Changed) { VGrid_Lib_buildText = true; }
    UI_VGrid_Lib_boxLineThickness.DisplayIf(Show_VGrid_Lib_boxLineThickness && UI_VGrid_Lib_boxLineThickness.treeGroup_parent.isExpanded);
    if (UI_VGrid_Lib_drawAxes.Changed) { VGrid_Lib_buildText = true; }
    UI_VGrid_Lib_drawAxes.DisplayIf(Show_VGrid_Lib_drawAxes && UI_VGrid_Lib_drawAxes.treeGroup_parent.isExpanded);
    if (UI_VGrid_Lib_customAxesRangeN.Changed) { VGrid_Lib_buildText = true; }
    UI_VGrid_Lib_customAxesRangeN.DisplayIf(Show_VGrid_Lib_customAxesRangeN && UI_VGrid_Lib_customAxesRangeN.treeGroup_parent.isExpanded);
    if (UI_VGrid_Lib_axesRangeMin.Changed) { VGrid_Lib_buildText = true; }
    UI_VGrid_Lib_axesRangeMin.DisplayIf(Show_VGrid_Lib_axesRangeMin && UI_VGrid_Lib_axesRangeMin.treeGroup_parent.isExpanded);
    if (UI_VGrid_Lib_axesRangeMax.Changed) { VGrid_Lib_buildText = true; }
    UI_VGrid_Lib_axesRangeMax.DisplayIf(Show_VGrid_Lib_axesRangeMax && UI_VGrid_Lib_axesRangeMax.treeGroup_parent.isExpanded);
    if (UI_VGrid_Lib_axesRangeMin1.Changed) { VGrid_Lib_buildText = true; }
    UI_VGrid_Lib_axesRangeMin1.DisplayIf(Show_VGrid_Lib_axesRangeMin1 && UI_VGrid_Lib_axesRangeMin1.treeGroup_parent.isExpanded);
    if (UI_VGrid_Lib_axesRangeMax1.Changed) { VGrid_Lib_buildText = true; }
    UI_VGrid_Lib_axesRangeMax1.DisplayIf(Show_VGrid_Lib_axesRangeMax1 && UI_VGrid_Lib_axesRangeMax1.treeGroup_parent.isExpanded);
    if (UI_VGrid_Lib_axesRangeMin2.Changed) { VGrid_Lib_buildText = true; }
    UI_VGrid_Lib_axesRangeMin2.DisplayIf(Show_VGrid_Lib_axesRangeMin2 && UI_VGrid_Lib_axesRangeMin2.treeGroup_parent.isExpanded);
    if (UI_VGrid_Lib_axesRangeMax2.Changed) { VGrid_Lib_buildText = true; }
    UI_VGrid_Lib_axesRangeMax2.DisplayIf(Show_VGrid_Lib_axesRangeMax2 && UI_VGrid_Lib_axesRangeMax2.treeGroup_parent.isExpanded);
    if (UI_VGrid_Lib_titles.Changed) { VGrid_Lib_buildText = true; }
    UI_VGrid_Lib_titles.DisplayIf(Show_VGrid_Lib_titles && UI_VGrid_Lib_titles.treeGroup_parent.isExpanded);
    if (UI_VGrid_Lib_axesFormats.Changed) { VGrid_Lib_buildText = true; }
    UI_VGrid_Lib_axesFormats.DisplayIf(Show_VGrid_Lib_axesFormats && UI_VGrid_Lib_axesFormats.treeGroup_parent.isExpanded);
    if (UI_VGrid_Lib_textSize.Changed) { VGrid_Lib_buildText = true; }
    UI_VGrid_Lib_textSize.DisplayIf(Show_VGrid_Lib_textSize && UI_VGrid_Lib_textSize.treeGroup_parent.isExpanded);
    if (UI_VGrid_Lib_axesColor.Changed) { VGrid_Lib_buildText = true; }
    UI_VGrid_Lib_axesColor.DisplayIf(Show_VGrid_Lib_axesColor && UI_VGrid_Lib_axesColor.treeGroup_parent.isExpanded);
    if (UI_VGrid_Lib_axesOpacity.Changed) { VGrid_Lib_buildText = true; }
    UI_VGrid_Lib_axesOpacity.DisplayIf(Show_VGrid_Lib_axesOpacity && UI_VGrid_Lib_axesOpacity.treeGroup_parent.isExpanded);
    if (UI_VGrid_Lib_zeroOrigin.Changed) { VGrid_Lib_buildText = true; }
    UI_VGrid_Lib_zeroOrigin.DisplayIf(Show_VGrid_Lib_zeroOrigin && UI_VGrid_Lib_zeroOrigin.treeGroup_parent.isExpanded);
    if (UI_VGrid_Lib_drawSurface.Changed) { VGrid_Lib_retrace = true; }
    UI_VGrid_Lib_drawSurface.DisplayIf(Show_VGrid_Lib_drawSurface && UI_VGrid_Lib_drawSurface.treeGroup_parent.isExpanded);
    if (UI_VGrid_Lib_GridDrawFront.Changed) { VGrid_Lib_retrace = true; }
    UI_VGrid_Lib_GridDrawFront.DisplayIf(Show_VGrid_Lib_GridDrawFront && UI_VGrid_Lib_GridDrawFront.treeGroup_parent.isExpanded);
    if (UI_VGrid_Lib_GridDrawBack.Changed) { VGrid_Lib_retrace = true; }
    UI_VGrid_Lib_GridDrawBack.DisplayIf(Show_VGrid_Lib_GridDrawBack && UI_VGrid_Lib_GridDrawBack.treeGroup_parent.isExpanded);
    if (UI_VGrid_Lib_show_slices.Changed) { VGrid_Lib_retrace = true; }
    UI_VGrid_Lib_show_slices.DisplayIf(Show_VGrid_Lib_show_slices && UI_VGrid_Lib_show_slices.treeGroup_parent.isExpanded);
    if (UI_VGrid_Lib_slices.Changed) { VGrid_Lib_retrace = true; }
    UI_VGrid_Lib_slices.DisplayIf(Show_VGrid_Lib_slices && UI_VGrid_Lib_slices.treeGroup_parent.isExpanded);
    if (UI_VGrid_Lib_sliceRotation.Changed) { VGrid_Lib_retrace = true; }
    UI_VGrid_Lib_sliceRotation.DisplayIf(Show_VGrid_Lib_sliceRotation && UI_VGrid_Lib_sliceRotation.treeGroup_parent.isExpanded);
    if (UI_VGrid_Lib_GridLineThickness.Changed) { VGrid_Lib_retrace = true; }
    UI_VGrid_Lib_GridLineThickness.DisplayIf(Show_VGrid_Lib_GridLineThickness && UI_VGrid_Lib_GridLineThickness.treeGroup_parent.isExpanded);
    if (UI_VGrid_Lib_opacity.Changed) { VGrid_Lib_retrace = true; }
    UI_VGrid_Lib_opacity.DisplayIf(Show_VGrid_Lib_opacity && UI_VGrid_Lib_opacity.treeGroup_parent.isExpanded);
    if (UI_VGrid_Lib_paletteRange.Changed) { VGrid_Lib_retrace = true; }
    if (UI_VGrid_Lib_paletteType.Changed) { VGrid_Lib_retrace = true; _PaletteTex = LoadPalette(UI_VGrid_Lib_paletteType.textString, ref VGrid_Lib_paletteBuffer); }
    if (UI_VGrid_Lib_twoSided.Changed) { VGrid_Lib_retrace = true; }
    UI_VGrid_Lib_twoSided.DisplayIf(Show_VGrid_Lib_twoSided && UI_VGrid_Lib_twoSided.treeGroup_parent.isExpanded);
    if (UI_VGrid_Lib_meshVal.Changed) { VGrid_Lib_retrace = true; }
    UI_VGrid_Lib_meshVal.DisplayIf(Show_VGrid_Lib_meshVal && UI_VGrid_Lib_meshVal.treeGroup_parent.isExpanded);
    if (UI_VGrid_Lib_meshRange.Changed) { VGrid_Lib_retrace = true; if (VGrid_Lib_meshRange.x > VGrid_Lib_meshRange.y) VGrid_Lib_meshRange = VGrid_Lib_meshRange.xx; }
    UI_VGrid_Lib_meshRange.DisplayIf(Show_VGrid_Lib_meshRange && UI_VGrid_Lib_meshRange.treeGroup_parent.isExpanded);
  }
  public override void OnValueChanged_GS()
  {
    VGrid_Lib_OnValueChanged_GS();
    Views_Lib_OnValueChanged_GS();
    var type = "gsReport_Lib".ToType();
    if (type != null) ((GS)GetComponent(type))?.OnValueChanged_GS();

  }
  public string[] Scenes_in_Build => new string[] {  };
  public virtual bool Show_vert_VGrid_Lib_BDraw_Text { get => VGrid_Lib_drawBox && VGrid_Lib_drawAxes; }
  public virtual bool Show_vert_VGrid_Lib_BDraw_Box { get => VGrid_Lib_drawBox; }
  public virtual bool Show_VGrid_Lib_GridX { get => VGrid_Lib_drawGrid; }
  public virtual bool Show_VGrid_Lib_GridY { get => VGrid_Lib_drawGrid; }
  public virtual bool Show_VGrid_Lib_GridZ { get => VGrid_Lib_drawGrid; }
  public virtual bool Show_VGrid_Lib_resolution { get => VGrid_Lib_drawGrid; }
  public virtual bool Show_VGrid_Lib_drawBox { get => VGrid_Lib_drawGrid; }
  public virtual bool Show_VGrid_Lib_boxLineThickness { get => VGrid_Lib_drawBox; }
  public virtual bool Show_VGrid_Lib_drawAxes { get => VGrid_Lib_drawGrid; }
  public virtual bool Show_VGrid_Lib_customAxesRangeN { get => VGrid_Lib_drawGrid && VGrid_Lib_drawAxes; }
  public virtual bool Show_VGrid_Lib_axesRangeMin { get => VGrid_Lib_showNormalizedAxes && VGrid_Lib_customAxesRangeN > 0; }
  public virtual bool Show_VGrid_Lib_axesRangeMax { get => VGrid_Lib_showNormalizedAxes && VGrid_Lib_customAxesRangeN > 0; }
  public virtual bool Show_VGrid_Lib_axesRangeMin1 { get => VGrid_Lib_showNormalizedAxes && VGrid_Lib_customAxesRangeN > 1; }
  public virtual bool Show_VGrid_Lib_axesRangeMax1 { get => VGrid_Lib_showNormalizedAxes && VGrid_Lib_customAxesRangeN > 1; }
  public virtual bool Show_VGrid_Lib_axesRangeMin2 { get => VGrid_Lib_showNormalizedAxes && VGrid_Lib_customAxesRangeN > 2; }
  public virtual bool Show_VGrid_Lib_axesRangeMax2 { get => VGrid_Lib_showNormalizedAxes && VGrid_Lib_customAxesRangeN > 2; }
  public virtual bool Show_VGrid_Lib_titles { get => VGrid_Lib_drawGrid && VGrid_Lib_drawAxes; }
  public virtual bool Show_VGrid_Lib_axesFormats { get => VGrid_Lib_drawGrid && VGrid_Lib_drawAxes; }
  public virtual bool Show_VGrid_Lib_textSize { get => VGrid_Lib_drawGrid && VGrid_Lib_drawAxes; }
  public virtual bool Show_VGrid_Lib_axesColor { get => VGrid_Lib_drawGrid && VGrid_Lib_drawAxes; }
  public virtual bool Show_VGrid_Lib_axesOpacity { get => VGrid_Lib_drawGrid && VGrid_Lib_drawAxes; }
  public virtual bool Show_VGrid_Lib_zeroOrigin { get => VGrid_Lib_drawGrid && VGrid_Lib_drawAxes; }
  public virtual bool Show_VGrid_Lib_drawSurface { get => VGrid_Lib_drawGrid; }
  public virtual bool Show_VGrid_Lib_GridDrawFront { get => VGrid_Lib_showSurface; }
  public virtual bool Show_VGrid_Lib_GridDrawBack { get => VGrid_Lib_showSurface; }
  public virtual bool Show_VGrid_Lib_show_slices { get => VGrid_Lib_showSurface; }
  public virtual bool Show_VGrid_Lib_slices { get => VGrid_Lib_showSurface && VGrid_Lib_show_slices; }
  public virtual bool Show_VGrid_Lib_sliceRotation { get => VGrid_Lib_showSurface && VGrid_Lib_show_slices; }
  public virtual bool Show_VGrid_Lib_GridLineThickness { get => VGrid_Lib_showSurface; }
  public virtual bool Show_VGrid_Lib_opacity { get => VGrid_Lib_showSurface; }
  public virtual bool Show_VGrid_Lib_twoSided { get => VGrid_Lib_showSurface; }
  public virtual bool Show_VGrid_Lib_meshVal { get => VGrid_Lib_drawGrid && !VGrid_Lib_twoSided; }
  public virtual bool Show_VGrid_Lib_meshRange { get => VGrid_Lib_drawGrid && VGrid_Lib_twoSided; }
  public virtual bool Show_vert_VGrid_Lib_DrawScreen { get => VGrid_Lib_drawGrid && VGrid_Lib_showSurface; }
  public virtual float sphere { get => g.sphere; set { if (any(g.sphere != value) || any(UI_sphere.v != value)) { g.sphere = UI_sphere.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float cube { get => g.cube; set { if (any(g.cube != value) || any(UI_cube.v != value)) { g.cube = UI_cube.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float torus { get => g.torus; set { if (any(g.torus != value) || any(UI_torus.v != value)) { g.torus = UI_torus.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float box { get => g.box; set { if (any(g.box != value) || any(UI_box.v != value)) { g.box = UI_box.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float roundBox { get => g.roundBox; set { if (any(g.roundBox != value) || any(UI_roundBox.v != value)) { g.roundBox = UI_roundBox.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float boxFrame { get => g.boxFrame; set { if (any(g.boxFrame != value) || any(UI_boxFrame.v != value)) { g.boxFrame = UI_boxFrame.v = value; ValuesChanged = gChanged = true; } } }
  public virtual uint VGrid_Lib_BDraw_AppendBuff_IndexN { get => g.VGrid_Lib_BDraw_AppendBuff_IndexN; set { if (g.VGrid_Lib_BDraw_AppendBuff_IndexN != value) { g.VGrid_Lib_BDraw_AppendBuff_IndexN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint VGrid_Lib_BDraw_AppendBuff_BitN { get => g.VGrid_Lib_BDraw_AppendBuff_BitN; set { if (g.VGrid_Lib_BDraw_AppendBuff_BitN != value) { g.VGrid_Lib_BDraw_AppendBuff_BitN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint VGrid_Lib_BDraw_AppendBuff_N { get => g.VGrid_Lib_BDraw_AppendBuff_N; set { if (g.VGrid_Lib_BDraw_AppendBuff_N != value) { g.VGrid_Lib_BDraw_AppendBuff_N = value; ValuesChanged = gChanged = true; } } }
  public virtual uint VGrid_Lib_BDraw_AppendBuff_BitN1 { get => g.VGrid_Lib_BDraw_AppendBuff_BitN1; set { if (g.VGrid_Lib_BDraw_AppendBuff_BitN1 != value) { g.VGrid_Lib_BDraw_AppendBuff_BitN1 = value; ValuesChanged = gChanged = true; } } }
  public virtual uint VGrid_Lib_BDraw_AppendBuff_BitN2 { get => g.VGrid_Lib_BDraw_AppendBuff_BitN2; set { if (g.VGrid_Lib_BDraw_AppendBuff_BitN2 != value) { g.VGrid_Lib_BDraw_AppendBuff_BitN2 = value; ValuesChanged = gChanged = true; } } }
  public virtual bool VGrid_Lib_BDraw_omitText { get => Is(g.VGrid_Lib_BDraw_omitText); set { if (g.VGrid_Lib_BDraw_omitText != Is(value)) { g.VGrid_Lib_BDraw_omitText = Is(value); ValuesChanged = gChanged = true; } } }
  public virtual bool VGrid_Lib_BDraw_includeUnicode { get => Is(g.VGrid_Lib_BDraw_includeUnicode); set { if (g.VGrid_Lib_BDraw_includeUnicode != Is(value)) { g.VGrid_Lib_BDraw_includeUnicode = Is(value); ValuesChanged = gChanged = true; } } }
  public virtual uint VGrid_Lib_BDraw_fontInfoN { get => g.VGrid_Lib_BDraw_fontInfoN; set { if (g.VGrid_Lib_BDraw_fontInfoN != value) { g.VGrid_Lib_BDraw_fontInfoN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint VGrid_Lib_BDraw_textN { get => g.VGrid_Lib_BDraw_textN; set { if (g.VGrid_Lib_BDraw_textN != value) { g.VGrid_Lib_BDraw_textN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint VGrid_Lib_BDraw_textCharN { get => g.VGrid_Lib_BDraw_textCharN; set { if (g.VGrid_Lib_BDraw_textCharN != value) { g.VGrid_Lib_BDraw_textCharN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint VGrid_Lib_BDraw_boxEdgeN { get => g.VGrid_Lib_BDraw_boxEdgeN; set { if (g.VGrid_Lib_BDraw_boxEdgeN != value) { g.VGrid_Lib_BDraw_boxEdgeN = value; ValuesChanged = gChanged = true; } } }
  public virtual float VGrid_Lib_BDraw_fontSize { get => g.VGrid_Lib_BDraw_fontSize; set { if (any(g.VGrid_Lib_BDraw_fontSize != value)) { g.VGrid_Lib_BDraw_fontSize = value; ValuesChanged = gChanged = true; } } }
  public virtual float VGrid_Lib_BDraw_boxThickness { get => g.VGrid_Lib_BDraw_boxThickness; set { if (any(g.VGrid_Lib_BDraw_boxThickness != value)) { g.VGrid_Lib_BDraw_boxThickness = value; ValuesChanged = gChanged = true; } } }
  public virtual float4 VGrid_Lib_BDraw_boxColor { get => g.VGrid_Lib_BDraw_boxColor; set { if (any(g.VGrid_Lib_BDraw_boxColor != value)) { g.VGrid_Lib_BDraw_boxColor = value; ValuesChanged = gChanged = true; } } }
  public virtual bool VGrid_Lib_drawGrid { get => Is(g.VGrid_Lib_drawGrid); set { if (g.VGrid_Lib_drawGrid != Is(value) || UI_VGrid_Lib_drawGrid.v != value) { g.VGrid_Lib_drawGrid = Is(UI_VGrid_Lib_drawGrid.v = value); ValuesChanged = gChanged = true; } } }
  public virtual float2 VGrid_Lib_GridX { get => g.VGrid_Lib_GridX; set { if (any(g.VGrid_Lib_GridX != value) || any(UI_VGrid_Lib_GridX.v != value)) { g.VGrid_Lib_GridX = UI_VGrid_Lib_GridX.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float2 VGrid_Lib_GridY { get => g.VGrid_Lib_GridY; set { if (any(g.VGrid_Lib_GridY != value) || any(UI_VGrid_Lib_GridY.v != value)) { g.VGrid_Lib_GridY = UI_VGrid_Lib_GridY.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float2 VGrid_Lib_GridZ { get => g.VGrid_Lib_GridZ; set { if (any(g.VGrid_Lib_GridZ != value) || any(UI_VGrid_Lib_GridZ.v != value)) { g.VGrid_Lib_GridZ = UI_VGrid_Lib_GridZ.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float VGrid_Lib_resolution { get => g.VGrid_Lib_resolution; set { if (any(g.VGrid_Lib_resolution != value) || any(UI_VGrid_Lib_resolution.v != value)) { g.VGrid_Lib_resolution = UI_VGrid_Lib_resolution.v = value; ValuesChanged = gChanged = true; } } }
  public virtual bool VGrid_Lib_drawBox { get => Is(g.VGrid_Lib_drawBox); set { if (g.VGrid_Lib_drawBox != Is(value) || UI_VGrid_Lib_drawBox.v != value) { g.VGrid_Lib_drawBox = Is(UI_VGrid_Lib_drawBox.v = value); ValuesChanged = gChanged = true; } } }
  public virtual float VGrid_Lib_boxLineThickness { get => g.VGrid_Lib_boxLineThickness; set { if (any(g.VGrid_Lib_boxLineThickness != value) || any(UI_VGrid_Lib_boxLineThickness.v != value)) { g.VGrid_Lib_boxLineThickness = UI_VGrid_Lib_boxLineThickness.v = value; ValuesChanged = gChanged = true; } } }
  public virtual bool VGrid_Lib_drawAxes { get => Is(g.VGrid_Lib_drawAxes); set { if (g.VGrid_Lib_drawAxes != Is(value) || UI_VGrid_Lib_drawAxes.v != value) { g.VGrid_Lib_drawAxes = Is(UI_VGrid_Lib_drawAxes.v = value); ValuesChanged = gChanged = true; } } }
  public virtual uint VGrid_Lib_customAxesRangeN { get => g.VGrid_Lib_customAxesRangeN; set { if (g.VGrid_Lib_customAxesRangeN != value || UI_VGrid_Lib_customAxesRangeN.v != value) { g.VGrid_Lib_customAxesRangeN = UI_VGrid_Lib_customAxesRangeN.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float3 VGrid_Lib_axesRangeMin { get => g.VGrid_Lib_axesRangeMin; set { if (any(g.VGrid_Lib_axesRangeMin != value) || any(UI_VGrid_Lib_axesRangeMin.v != value)) { g.VGrid_Lib_axesRangeMin = UI_VGrid_Lib_axesRangeMin.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float3 VGrid_Lib_axesRangeMax { get => g.VGrid_Lib_axesRangeMax; set { if (any(g.VGrid_Lib_axesRangeMax != value) || any(UI_VGrid_Lib_axesRangeMax.v != value)) { g.VGrid_Lib_axesRangeMax = UI_VGrid_Lib_axesRangeMax.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float3 VGrid_Lib_axesRangeMin1 { get => g.VGrid_Lib_axesRangeMin1; set { if (any(g.VGrid_Lib_axesRangeMin1 != value) || any(UI_VGrid_Lib_axesRangeMin1.v != value)) { g.VGrid_Lib_axesRangeMin1 = UI_VGrid_Lib_axesRangeMin1.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float3 VGrid_Lib_axesRangeMax1 { get => g.VGrid_Lib_axesRangeMax1; set { if (any(g.VGrid_Lib_axesRangeMax1 != value) || any(UI_VGrid_Lib_axesRangeMax1.v != value)) { g.VGrid_Lib_axesRangeMax1 = UI_VGrid_Lib_axesRangeMax1.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float3 VGrid_Lib_axesRangeMin2 { get => g.VGrid_Lib_axesRangeMin2; set { if (any(g.VGrid_Lib_axesRangeMin2 != value) || any(UI_VGrid_Lib_axesRangeMin2.v != value)) { g.VGrid_Lib_axesRangeMin2 = UI_VGrid_Lib_axesRangeMin2.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float3 VGrid_Lib_axesRangeMax2 { get => g.VGrid_Lib_axesRangeMax2; set { if (any(g.VGrid_Lib_axesRangeMax2 != value) || any(UI_VGrid_Lib_axesRangeMax2.v != value)) { g.VGrid_Lib_axesRangeMax2 = UI_VGrid_Lib_axesRangeMax2.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float2 VGrid_Lib_textSize { get => g.VGrid_Lib_textSize; set { if (any(g.VGrid_Lib_textSize != value) || any(UI_VGrid_Lib_textSize.v != value)) { g.VGrid_Lib_textSize = UI_VGrid_Lib_textSize.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float3 VGrid_Lib_axesColor { get => g.VGrid_Lib_axesColor; set { if (any(g.VGrid_Lib_axesColor != value) || any(UI_VGrid_Lib_axesColor.v != value)) { g.VGrid_Lib_axesColor = UI_VGrid_Lib_axesColor.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float VGrid_Lib_axesOpacity { get => g.VGrid_Lib_axesOpacity; set { if (any(g.VGrid_Lib_axesOpacity != value) || any(UI_VGrid_Lib_axesOpacity.v != value)) { g.VGrid_Lib_axesOpacity = UI_VGrid_Lib_axesOpacity.v = value; ValuesChanged = gChanged = true; } } }
  public virtual bool VGrid_Lib_zeroOrigin { get => Is(g.VGrid_Lib_zeroOrigin); set { if (g.VGrid_Lib_zeroOrigin != Is(value) || UI_VGrid_Lib_zeroOrigin.v != value) { g.VGrid_Lib_zeroOrigin = Is(UI_VGrid_Lib_zeroOrigin.v = value); ValuesChanged = gChanged = true; } } }
  public virtual bool VGrid_Lib_drawSurface { get => Is(g.VGrid_Lib_drawSurface); set { if (g.VGrid_Lib_drawSurface != Is(value) || UI_VGrid_Lib_drawSurface.v != value) { g.VGrid_Lib_drawSurface = Is(UI_VGrid_Lib_drawSurface.v = value); ValuesChanged = gChanged = true; } } }
  public virtual bool VGrid_Lib_GridDrawFront { get => Is(g.VGrid_Lib_GridDrawFront); set { if (g.VGrid_Lib_GridDrawFront != Is(value) || UI_VGrid_Lib_GridDrawFront.v != value) { g.VGrid_Lib_GridDrawFront = Is(UI_VGrid_Lib_GridDrawFront.v = value); ValuesChanged = gChanged = true; } } }
  public virtual bool VGrid_Lib_GridDrawBack { get => Is(g.VGrid_Lib_GridDrawBack); set { if (g.VGrid_Lib_GridDrawBack != Is(value) || UI_VGrid_Lib_GridDrawBack.v != value) { g.VGrid_Lib_GridDrawBack = Is(UI_VGrid_Lib_GridDrawBack.v = value); ValuesChanged = gChanged = true; } } }
  public virtual bool VGrid_Lib_show_slices { get => Is(g.VGrid_Lib_show_slices); set { if (g.VGrid_Lib_show_slices != Is(value) || UI_VGrid_Lib_show_slices.v != value) { g.VGrid_Lib_show_slices = Is(UI_VGrid_Lib_show_slices.v = value); ValuesChanged = gChanged = true; } } }
  public virtual float3 VGrid_Lib_slices { get => g.VGrid_Lib_slices; set { if (any(g.VGrid_Lib_slices != value) || any(UI_VGrid_Lib_slices.v != value)) { g.VGrid_Lib_slices = UI_VGrid_Lib_slices.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float3 VGrid_Lib_sliceRotation { get => g.VGrid_Lib_sliceRotation; set { if (any(g.VGrid_Lib_sliceRotation != value) || any(UI_VGrid_Lib_sliceRotation.v != value)) { g.VGrid_Lib_sliceRotation = UI_VGrid_Lib_sliceRotation.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float VGrid_Lib_GridLineThickness { get => g.VGrid_Lib_GridLineThickness; set { if (any(g.VGrid_Lib_GridLineThickness != value) || any(UI_VGrid_Lib_GridLineThickness.v != value)) { g.VGrid_Lib_GridLineThickness = UI_VGrid_Lib_GridLineThickness.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float VGrid_Lib_opacity { get => g.VGrid_Lib_opacity; set { if (any(g.VGrid_Lib_opacity != value) || any(UI_VGrid_Lib_opacity.v != value)) { g.VGrid_Lib_opacity = UI_VGrid_Lib_opacity.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float2 VGrid_Lib_paletteRange { get => g.VGrid_Lib_paletteRange; set { if (any(g.VGrid_Lib_paletteRange != value) || any(UI_VGrid_Lib_paletteRange.v != value)) { g.VGrid_Lib_paletteRange = UI_VGrid_Lib_paletteRange.v = value; ValuesChanged = gChanged = true; } } }
  public virtual VGrid_Lib_PaletteType VGrid_Lib_paletteType { get => (VGrid_Lib_PaletteType)g.VGrid_Lib_paletteType; set { if ((VGrid_Lib_PaletteType)g.VGrid_Lib_paletteType != value || (VGrid_Lib_PaletteType)UI_VGrid_Lib_paletteType.v != value) { g.VGrid_Lib_paletteType = UI_VGrid_Lib_paletteType.v = (uint)value; ValuesChanged = gChanged = true; } } }
  public virtual bool VGrid_Lib_twoSided { get => Is(g.VGrid_Lib_twoSided); set { if (g.VGrid_Lib_twoSided != Is(value) || UI_VGrid_Lib_twoSided.v != value) { g.VGrid_Lib_twoSided = Is(UI_VGrid_Lib_twoSided.v = value); ValuesChanged = gChanged = true; } } }
  public virtual float VGrid_Lib_meshVal { get => g.VGrid_Lib_meshVal; set { if (any(g.VGrid_Lib_meshVal != value) || any(UI_VGrid_Lib_meshVal.v != value)) { g.VGrid_Lib_meshVal = UI_VGrid_Lib_meshVal.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float2 VGrid_Lib_meshRange { get => g.VGrid_Lib_meshRange; set { if (any(g.VGrid_Lib_meshRange != value) || any(UI_VGrid_Lib_meshRange.v != value)) { g.VGrid_Lib_meshRange = UI_VGrid_Lib_meshRange.v = value; ValuesChanged = gChanged = true; } } }
  public virtual bool VGrid_Lib_reCalc { get => Is(g.VGrid_Lib_reCalc); set { if (g.VGrid_Lib_reCalc != Is(value)) { g.VGrid_Lib_reCalc = Is(value); ValuesChanged = gChanged = true; } } }
  public virtual bool VGrid_Lib_buildText { get => Is(g.VGrid_Lib_buildText); set { if (g.VGrid_Lib_buildText != Is(value)) { g.VGrid_Lib_buildText = Is(value); ValuesChanged = gChanged = true; } } }
  public virtual uint3 VGrid_Lib_nodeN { get => g.VGrid_Lib_nodeN; set { if (any(g.VGrid_Lib_nodeN != value)) { g.VGrid_Lib_nodeN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint2 VGrid_Lib_viewSize { get => g.VGrid_Lib_viewSize; set { if (any(g.VGrid_Lib_viewSize != value)) { g.VGrid_Lib_viewSize = value; ValuesChanged = gChanged = true; } } }
  public virtual uint4 VGrid_Lib_viewRect { get => g.VGrid_Lib_viewRect; set { if (any(g.VGrid_Lib_viewRect != value)) { g.VGrid_Lib_viewRect = value; ValuesChanged = gChanged = true; } } }
  public virtual bool VGrid_Lib_isOrtho { get => Is(g.VGrid_Lib_isOrtho); set { if (g.VGrid_Lib_isOrtho != Is(value)) { g.VGrid_Lib_isOrtho = Is(value); ValuesChanged = gChanged = true; } } }
  public virtual float VGrid_Lib_orthoSize { get => g.VGrid_Lib_orthoSize; set { if (any(g.VGrid_Lib_orthoSize != value)) { g.VGrid_Lib_orthoSize = value; ValuesChanged = gChanged = true; } } }
  public virtual float VGrid_Lib_maxDist { get => g.VGrid_Lib_maxDist; set { if (any(g.VGrid_Lib_maxDist != value)) { g.VGrid_Lib_maxDist = value; ValuesChanged = gChanged = true; } } }
  public Matrix4x4 VGrid_Lib_camToWorld { get => g.VGrid_Lib_camToWorld; set { g.VGrid_Lib_camToWorld = value; } }
  public Matrix4x4 VGrid_Lib_cameraInvProjection { get => g.VGrid_Lib_cameraInvProjection; set { g.VGrid_Lib_cameraInvProjection = value; } }
  public virtual bool VGrid_Lib_showMeshVal { get => Is(g.VGrid_Lib_showMeshVal); set { if (g.VGrid_Lib_showMeshVal != Is(value)) { g.VGrid_Lib_showMeshVal = Is(value); ValuesChanged = gChanged = true; } } }
  public virtual bool VGrid_Lib_showMeshRange { get => Is(g.VGrid_Lib_showMeshRange); set { if (g.VGrid_Lib_showMeshRange != Is(value)) { g.VGrid_Lib_showMeshRange = Is(value); ValuesChanged = gChanged = true; } } }
  public virtual bool VGrid_Lib_showOutline { get => Is(g.VGrid_Lib_showOutline); set { if (g.VGrid_Lib_showOutline != Is(value)) { g.VGrid_Lib_showOutline = Is(value); ValuesChanged = gChanged = true; } } }
  public virtual bool VGrid_Lib_showSurface { get => Is(g.VGrid_Lib_showSurface); set { if (g.VGrid_Lib_showSurface != Is(value)) { g.VGrid_Lib_showSurface = Is(value); ValuesChanged = gChanged = true; } } }
  public virtual bool VGrid_Lib_showAxes { get => Is(g.VGrid_Lib_showAxes); set { if (g.VGrid_Lib_showAxes != Is(value)) { g.VGrid_Lib_showAxes = Is(value); ValuesChanged = gChanged = true; } } }
  public virtual bool VGrid_Lib_showNormalizedAxes { get => Is(g.VGrid_Lib_showNormalizedAxes); set { if (g.VGrid_Lib_showNormalizedAxes != Is(value)) { g.VGrid_Lib_showNormalizedAxes = Is(value); ValuesChanged = gChanged = true; } } }
  public virtual bool VGrid_Lib_retrace { get => Is(g.VGrid_Lib_retrace); set { if (g.VGrid_Lib_retrace != Is(value)) { g.VGrid_Lib_retrace = Is(value); ValuesChanged = gChanged = true; } } }
  public virtual float VGrid_Lib_minResolution { get => g.VGrid_Lib_minResolution; set { if (any(g.VGrid_Lib_minResolution != value)) { g.VGrid_Lib_minResolution = value; ValuesChanged = gChanged = true; } } }
  public bool group_UI { get => UI_group_UI?.v ?? false; set { if (UI_group_UI != null) UI_group_UI.v = value; } }
  public bool group_Test { get => UI_group_Test?.v ?? false; set { if (UI_group_Test != null) UI_group_Test.v = value; } }
  public bool VGrid_Lib_group_VGrid_Lib { get => UI_VGrid_Lib_group_VGrid_Lib?.v ?? false; set { if (UI_VGrid_Lib_group_VGrid_Lib != null) UI_VGrid_Lib_group_VGrid_Lib.v = value; } }
  public bool VGrid_Lib_group_Geometry { get => UI_VGrid_Lib_group_Geometry?.v ?? false; set { if (UI_VGrid_Lib_group_Geometry != null) UI_VGrid_Lib_group_Geometry.v = value; } }
  public bool VGrid_Lib_group_Axes { get => UI_VGrid_Lib_group_Axes?.v ?? false; set { if (UI_VGrid_Lib_group_Axes != null) UI_VGrid_Lib_group_Axes.v = value; } }
  public bool VGrid_Lib_group_Mesh { get => UI_VGrid_Lib_group_Mesh?.v ?? false; set { if (UI_VGrid_Lib_group_Mesh != null) UI_VGrid_Lib_group_Mesh.v = value; } }
  public bool Views_Lib_group_CamViews_Lib { get => UI_Views_Lib_group_CamViews_Lib?.v ?? false; set { if (UI_Views_Lib_group_CamViews_Lib != null) UI_Views_Lib_group_CamViews_Lib.v = value; } }
  public UI_TreeGroup UI_group_UI, UI_group_Test, UI_VGrid_Lib_group_VGrid_Lib, UI_VGrid_Lib_group_Geometry, UI_VGrid_Lib_group_Axes, UI_VGrid_Lib_group_Mesh, UI_Views_Lib_group_CamViews_Lib;
  public UI_float UI_sphere, UI_cube, UI_torus, UI_box, UI_roundBox, UI_boxFrame, UI_VGrid_Lib_resolution, UI_VGrid_Lib_boxLineThickness, UI_VGrid_Lib_axesOpacity, UI_VGrid_Lib_GridLineThickness, UI_VGrid_Lib_opacity, UI_VGrid_Lib_meshVal;
  public UI_bool UI_VGrid_Lib_drawGrid, UI_VGrid_Lib_drawBox, UI_VGrid_Lib_drawAxes, UI_VGrid_Lib_zeroOrigin, UI_VGrid_Lib_drawSurface, UI_VGrid_Lib_GridDrawFront, UI_VGrid_Lib_GridDrawBack, UI_VGrid_Lib_show_slices, UI_VGrid_Lib_twoSided;
  public UI_float2 UI_VGrid_Lib_GridX, UI_VGrid_Lib_GridY, UI_VGrid_Lib_GridZ, UI_VGrid_Lib_textSize, UI_VGrid_Lib_paletteRange, UI_VGrid_Lib_meshRange;
  public UI_uint UI_VGrid_Lib_customAxesRangeN;
  public UI_float3 UI_VGrid_Lib_axesRangeMin, UI_VGrid_Lib_axesRangeMax, UI_VGrid_Lib_axesRangeMin1, UI_VGrid_Lib_axesRangeMax1, UI_VGrid_Lib_axesRangeMin2, UI_VGrid_Lib_axesRangeMax2, UI_VGrid_Lib_axesColor, UI_VGrid_Lib_slices, UI_VGrid_Lib_sliceRotation;
  public UI_string UI_VGrid_Lib_titles, UI_VGrid_Lib_axesFormats;
  public string VGrid_Lib_titles { get => UI_VGrid_Lib_titles?.v ?? ""; set { if (UI_VGrid_Lib_titles != null && data != null) UI_VGrid_Lib_titles.v = data.VGrid_Lib_titles = value; } }
  public string VGrid_Lib_axesFormats { get => UI_VGrid_Lib_axesFormats?.v ?? ""; set { if (UI_VGrid_Lib_axesFormats != null && data != null) UI_VGrid_Lib_axesFormats.v = data.VGrid_Lib_axesFormats = value; } }
  public UI_enum UI_VGrid_Lib_paletteType;
  public UI_grid UI_grid_Views_Lib_CamViews_Lib;
  public Views_Lib_CamView[] Views_Lib_CamViews_Lib;
  public UI_TreeGroup ui_group_UI => UI_group_UI;
  public UI_TreeGroup ui_group_Test => UI_group_Test;
  public UI_float ui_sphere => UI_sphere;
  public UI_float ui_cube => UI_cube;
  public UI_float ui_torus => UI_torus;
  public UI_float ui_box => UI_box;
  public UI_float ui_roundBox => UI_roundBox;
  public UI_float ui_boxFrame => UI_boxFrame;
  public UI_TreeGroup ui_VGrid_Lib_group_VGrid_Lib => UI_VGrid_Lib_group_VGrid_Lib;
  public UI_TreeGroup ui_VGrid_Lib_group_Geometry => UI_VGrid_Lib_group_Geometry;
  public UI_bool ui_VGrid_Lib_drawGrid => UI_VGrid_Lib_drawGrid;
  public UI_float2 ui_VGrid_Lib_GridX => UI_VGrid_Lib_GridX;
  public UI_float2 ui_VGrid_Lib_GridY => UI_VGrid_Lib_GridY;
  public UI_float2 ui_VGrid_Lib_GridZ => UI_VGrid_Lib_GridZ;
  public UI_float ui_VGrid_Lib_resolution => UI_VGrid_Lib_resolution;
  public UI_TreeGroup ui_VGrid_Lib_group_Axes => UI_VGrid_Lib_group_Axes;
  public UI_bool ui_VGrid_Lib_drawBox => UI_VGrid_Lib_drawBox;
  public UI_float ui_VGrid_Lib_boxLineThickness => UI_VGrid_Lib_boxLineThickness;
  public UI_bool ui_VGrid_Lib_drawAxes => UI_VGrid_Lib_drawAxes;
  public UI_uint ui_VGrid_Lib_customAxesRangeN => UI_VGrid_Lib_customAxesRangeN;
  public UI_float3 ui_VGrid_Lib_axesRangeMin => UI_VGrid_Lib_axesRangeMin;
  public UI_float3 ui_VGrid_Lib_axesRangeMax => UI_VGrid_Lib_axesRangeMax;
  public UI_float3 ui_VGrid_Lib_axesRangeMin1 => UI_VGrid_Lib_axesRangeMin1;
  public UI_float3 ui_VGrid_Lib_axesRangeMax1 => UI_VGrid_Lib_axesRangeMax1;
  public UI_float3 ui_VGrid_Lib_axesRangeMin2 => UI_VGrid_Lib_axesRangeMin2;
  public UI_float3 ui_VGrid_Lib_axesRangeMax2 => UI_VGrid_Lib_axesRangeMax2;
  public UI_string ui_VGrid_Lib_titles => UI_VGrid_Lib_titles;
  public UI_string ui_VGrid_Lib_axesFormats => UI_VGrid_Lib_axesFormats;
  public UI_float2 ui_VGrid_Lib_textSize => UI_VGrid_Lib_textSize;
  public UI_float3 ui_VGrid_Lib_axesColor => UI_VGrid_Lib_axesColor;
  public UI_float ui_VGrid_Lib_axesOpacity => UI_VGrid_Lib_axesOpacity;
  public UI_bool ui_VGrid_Lib_zeroOrigin => UI_VGrid_Lib_zeroOrigin;
  public UI_TreeGroup ui_VGrid_Lib_group_Mesh => UI_VGrid_Lib_group_Mesh;
  public UI_bool ui_VGrid_Lib_drawSurface => UI_VGrid_Lib_drawSurface;
  public UI_bool ui_VGrid_Lib_GridDrawFront => UI_VGrid_Lib_GridDrawFront;
  public UI_bool ui_VGrid_Lib_GridDrawBack => UI_VGrid_Lib_GridDrawBack;
  public UI_bool ui_VGrid_Lib_show_slices => UI_VGrid_Lib_show_slices;
  public UI_float3 ui_VGrid_Lib_slices => UI_VGrid_Lib_slices;
  public UI_float3 ui_VGrid_Lib_sliceRotation => UI_VGrid_Lib_sliceRotation;
  public UI_float ui_VGrid_Lib_GridLineThickness => UI_VGrid_Lib_GridLineThickness;
  public UI_float ui_VGrid_Lib_opacity => UI_VGrid_Lib_opacity;
  public UI_float2 ui_VGrid_Lib_paletteRange => UI_VGrid_Lib_paletteRange;
  public UI_enum ui_VGrid_Lib_paletteType => UI_VGrid_Lib_paletteType;
  public UI_bool ui_VGrid_Lib_twoSided => UI_VGrid_Lib_twoSided;
  public UI_float ui_VGrid_Lib_meshVal => UI_VGrid_Lib_meshVal;
  public UI_float2 ui_VGrid_Lib_meshRange => UI_VGrid_Lib_meshRange;
  public UI_TreeGroup ui_Views_Lib_group_CamViews_Lib => UI_Views_Lib_group_CamViews_Lib;
  [Serializable]
  public class uiData
  {
    public bool siUnits, group_UI, group_Test, VGrid_Lib_group_VGrid_Lib, VGrid_Lib_group_Geometry, VGrid_Lib_drawGrid, VGrid_Lib_group_Axes, VGrid_Lib_drawBox, VGrid_Lib_drawAxes, VGrid_Lib_zeroOrigin, VGrid_Lib_group_Mesh, VGrid_Lib_drawSurface, VGrid_Lib_GridDrawFront, VGrid_Lib_GridDrawBack, VGrid_Lib_show_slices, VGrid_Lib_twoSided, Views_Lib_group_CamViews_Lib;
    public float sphere, cube, torus, box, roundBox, boxFrame, VGrid_Lib_resolution, VGrid_Lib_boxLineThickness, VGrid_Lib_axesOpacity, VGrid_Lib_GridLineThickness, VGrid_Lib_opacity, VGrid_Lib_meshVal;
    public float2 VGrid_Lib_GridX, VGrid_Lib_GridY, VGrid_Lib_GridZ, VGrid_Lib_textSize, VGrid_Lib_paletteRange, VGrid_Lib_meshRange;
    public uint VGrid_Lib_customAxesRangeN;
    public float3 VGrid_Lib_axesRangeMin, VGrid_Lib_axesRangeMax, VGrid_Lib_axesRangeMin1, VGrid_Lib_axesRangeMax1, VGrid_Lib_axesRangeMin2, VGrid_Lib_axesRangeMax2, VGrid_Lib_axesColor, VGrid_Lib_slices, VGrid_Lib_sliceRotation;
    public string VGrid_Lib_titles, VGrid_Lib_axesFormats;
    [JsonConverter(typeof(StringEnumConverter))] public VGrid_Lib_PaletteType VGrid_Lib_paletteType;
    public Views_Lib_CamView[] Views_Lib_CamViews_Lib;
    public bool[] Views_Lib_CamViews_Lib_DisplayCols;
    public float Views_Lib_CamViews_Lib_VScroll;
    public uint Views_Lib_CamViews_Lib_DisplayRowN;
    public bool Views_Lib_CamViews_Lib_isExpanded;
    public string Views_Lib_CamViews_Lib_selectedRows;
    public int Views_Lib_CamViews_Lib_lastClickedRow;
  }
  public virtual void InitBuffers()
  {
    InitBuffers0_GS();
    AddComputeBuffer(ref gVGrid_Lib_Doc, nameof(gVGrid_Lib_Doc), 1);
    InitKernels();
    SetKernelValues(gVGrid_Lib_Doc, nameof(gVGrid_Lib_Doc), kernel_VGrid_Lib_Grid_Calc_Vals, kernel_VGrid_Lib_BDraw_AppendBuff_GetIndexes, kernel_VGrid_Lib_BDraw_AppendBuff_IncSums, kernel_VGrid_Lib_BDraw_AppendBuff_IncFills1, kernel_VGrid_Lib_BDraw_AppendBuff_GetFills2, kernel_VGrid_Lib_BDraw_AppendBuff_GetFills1, kernel_VGrid_Lib_BDraw_AppendBuff_Get_Bits_Sums, kernel_VGrid_Lib_BDraw_AppendBuff_GetSums, kernel_VGrid_Lib_BDraw_AppendBuff_Get_Bits, kernel_VGrid_Lib_BDraw_setDefaultTextInfo, kernel_VGrid_Lib_BDraw_getTextInfo, kernel_VGrid_Lib_Grid_Simple_TraceRay, kernel_VGrid_Lib_Grid_TraceRay);
    AddComputeBuffer(ref VGrid_Lib_BDraw_tab_delimeted_text, nameof(VGrid_Lib_BDraw_tab_delimeted_text), VGrid_Lib_BDraw_textN);
    AddComputeBuffer(ref VGrid_Lib_BDraw_textInfos, nameof(VGrid_Lib_BDraw_textInfos), VGrid_Lib_BDraw_textN);
    AddComputeBuffer(ref VGrid_Lib_BDraw_fontInfos, nameof(VGrid_Lib_BDraw_fontInfos), VGrid_Lib_BDraw_fontInfoN);
    AddComputeBuffer(ref VGrid_Lib_depthColors, nameof(VGrid_Lib_depthColors), product(VGrid_Lib_viewSize));
    AddComputeBuffer(ref VGrid_Lib_paletteBuffer, nameof(VGrid_Lib_paletteBuffer), 256);
    InitBuffers1_GS();
    isInitBuffers = true;
  }
  public virtual void InitBuffers0_GS()
  {
    VGrid_Lib_InitBuffers0_GS();
    Views_Lib_InitBuffers0_GS();
  }
  public virtual void InitBuffers1_GS()
  {
    VGrid_Lib_InitBuffers1_GS();
    Views_Lib_InitBuffers1_GS();
  }
  [HideInInspector] public uint[] VGrid_Lib_BDraw_AppendBuff_grp = new uint[1024];
  [HideInInspector] public uint[] VGrid_Lib_BDraw_AppendBuff_grp0 = new uint[1024];
  [Serializable]
  public struct GVGrid_Lib_Doc
  {
    public float sphere, cube, torus, box, roundBox, boxFrame, VGrid_Lib_BDraw_fontSize, VGrid_Lib_BDraw_boxThickness, VGrid_Lib_resolution, VGrid_Lib_boxLineThickness, VGrid_Lib_axesOpacity, VGrid_Lib_GridLineThickness, VGrid_Lib_opacity, VGrid_Lib_meshVal, VGrid_Lib_orthoSize, VGrid_Lib_maxDist, VGrid_Lib_minResolution;
    public uint VGrid_Lib_BDraw_AppendBuff_IndexN, VGrid_Lib_BDraw_AppendBuff_BitN, VGrid_Lib_BDraw_AppendBuff_N, VGrid_Lib_BDraw_AppendBuff_BitN1, VGrid_Lib_BDraw_AppendBuff_BitN2, VGrid_Lib_BDraw_omitText, VGrid_Lib_BDraw_includeUnicode, VGrid_Lib_BDraw_fontInfoN, VGrid_Lib_BDraw_textN, VGrid_Lib_BDraw_textCharN, VGrid_Lib_BDraw_boxEdgeN, VGrid_Lib_drawGrid, VGrid_Lib_drawBox, VGrid_Lib_drawAxes, VGrid_Lib_customAxesRangeN, VGrid_Lib_zeroOrigin, VGrid_Lib_drawSurface, VGrid_Lib_GridDrawFront, VGrid_Lib_GridDrawBack, VGrid_Lib_show_slices, VGrid_Lib_paletteType, VGrid_Lib_twoSided, VGrid_Lib_reCalc, VGrid_Lib_buildText, VGrid_Lib_isOrtho, VGrid_Lib_showMeshVal, VGrid_Lib_showMeshRange, VGrid_Lib_showOutline, VGrid_Lib_showSurface, VGrid_Lib_showAxes, VGrid_Lib_showNormalizedAxes, VGrid_Lib_retrace;
    public float4 VGrid_Lib_BDraw_boxColor;
    public float2 VGrid_Lib_GridX, VGrid_Lib_GridY, VGrid_Lib_GridZ, VGrid_Lib_textSize, VGrid_Lib_paletteRange, VGrid_Lib_meshRange;
    public float3 VGrid_Lib_axesRangeMin, VGrid_Lib_axesRangeMax, VGrid_Lib_axesRangeMin1, VGrid_Lib_axesRangeMax1, VGrid_Lib_axesRangeMin2, VGrid_Lib_axesRangeMax2, VGrid_Lib_axesColor, VGrid_Lib_slices, VGrid_Lib_sliceRotation;
    public uint3 VGrid_Lib_nodeN;
    public uint2 VGrid_Lib_viewSize;
    public uint4 VGrid_Lib_viewRect;
    public Matrix4x4 VGrid_Lib_camToWorld, VGrid_Lib_cameraInvProjection;
  };
  public RWStructuredBuffer<GVGrid_Lib_Doc> gVGrid_Lib_Doc;
  public struct VGrid_Lib_BDraw_FontInfo { public float2 uvBottomLeft, uvBottomRight, uvTopLeft, uvTopRight; public int advance, bearing, minX, minY, maxX, maxY; };
  public struct VGrid_Lib_BDraw_TextInfo { public float3 p, right, up, p0, p1; public float2 size, uvSize; public float4 color, backColor; public uint justification, textI, quadType, axis; public float height; };
  public struct VGrid_Lib_TRay { public float3 origin, direction; public float4 color; public float dist; };
  public struct Views_Lib_CamView { public string viewName; public float3 viewCenter; public float viewDist; public float2 viewTiltSpin; public uint viewProjection; public float view_sphere, view_cube, view_torus, view_box, view_roundBox, view_boxFrame; public uint view_twoSided; public float view_meshVal; public float2 view_meshRange; };
  public RWStructuredBuffer<uint> VGrid_Lib_BDraw_tab_delimeted_text, VGrid_Lib_BDraw_AppendBuff_Bits, VGrid_Lib_BDraw_AppendBuff_Sums, VGrid_Lib_BDraw_AppendBuff_Indexes, VGrid_Lib_BDraw_AppendBuff_Fills1, VGrid_Lib_BDraw_AppendBuff_Fills2;
  public RWStructuredBuffer<VGrid_Lib_BDraw_TextInfo> VGrid_Lib_BDraw_textInfos;
  public RWStructuredBuffer<VGrid_Lib_BDraw_FontInfo> VGrid_Lib_BDraw_fontInfos;
  public RWStructuredBuffer<uint2> VGrid_Lib_depthColors;
  public RWStructuredBuffer<Color32> VGrid_Lib_paletteBuffer;
  public RWStructuredBuffer<float> VGrid_Lib_Vals;
  public virtual void AllocData_VGrid_Lib_BDraw_tab_delimeted_text(uint n) => AddComputeBuffer(ref VGrid_Lib_BDraw_tab_delimeted_text, nameof(VGrid_Lib_BDraw_tab_delimeted_text), n);
  public virtual void AssignData_VGrid_Lib_BDraw_tab_delimeted_text(params uint[] data) => AddComputeBufferData(ref VGrid_Lib_BDraw_tab_delimeted_text, nameof(VGrid_Lib_BDraw_tab_delimeted_text), data);
  public virtual void AllocData_VGrid_Lib_BDraw_textInfos(uint n) => AddComputeBuffer(ref VGrid_Lib_BDraw_textInfos, nameof(VGrid_Lib_BDraw_textInfos), n);
  public virtual void AssignData_VGrid_Lib_BDraw_textInfos(params VGrid_Lib_BDraw_TextInfo[] data) => AddComputeBufferData(ref VGrid_Lib_BDraw_textInfos, nameof(VGrid_Lib_BDraw_textInfos), data);
  public virtual void AllocData_VGrid_Lib_BDraw_fontInfos(uint n) => AddComputeBuffer(ref VGrid_Lib_BDraw_fontInfos, nameof(VGrid_Lib_BDraw_fontInfos), n);
  public virtual void AssignData_VGrid_Lib_BDraw_fontInfos(params VGrid_Lib_BDraw_FontInfo[] data) => AddComputeBufferData(ref VGrid_Lib_BDraw_fontInfos, nameof(VGrid_Lib_BDraw_fontInfos), data);
  public virtual void AllocData_VGrid_Lib_depthColors(uint n) => AddComputeBuffer(ref VGrid_Lib_depthColors, nameof(VGrid_Lib_depthColors), n);
  public virtual void AssignData_VGrid_Lib_depthColors(params uint2[] data) => AddComputeBufferData(ref VGrid_Lib_depthColors, nameof(VGrid_Lib_depthColors), data);
  public virtual void AllocData_VGrid_Lib_paletteBuffer(uint n) => AddComputeBuffer(ref VGrid_Lib_paletteBuffer, nameof(VGrid_Lib_paletteBuffer), n);
  public virtual void AssignData_VGrid_Lib_paletteBuffer(params Color32[] data) => AddComputeBufferData(ref VGrid_Lib_paletteBuffer, nameof(VGrid_Lib_paletteBuffer), data);
  public virtual void AllocData_VGrid_Lib_BDraw_AppendBuff_Bits(uint n) => AddComputeBuffer(ref VGrid_Lib_BDraw_AppendBuff_Bits, nameof(VGrid_Lib_BDraw_AppendBuff_Bits), n);
  public virtual void AssignData_VGrid_Lib_BDraw_AppendBuff_Bits(params uint[] data) => AddComputeBufferData(ref VGrid_Lib_BDraw_AppendBuff_Bits, nameof(VGrid_Lib_BDraw_AppendBuff_Bits), data);
  public virtual void AllocData_VGrid_Lib_BDraw_AppendBuff_Sums(uint n) => AddComputeBuffer(ref VGrid_Lib_BDraw_AppendBuff_Sums, nameof(VGrid_Lib_BDraw_AppendBuff_Sums), n);
  public virtual void AssignData_VGrid_Lib_BDraw_AppendBuff_Sums(params uint[] data) => AddComputeBufferData(ref VGrid_Lib_BDraw_AppendBuff_Sums, nameof(VGrid_Lib_BDraw_AppendBuff_Sums), data);
  public virtual void AllocData_VGrid_Lib_BDraw_AppendBuff_Indexes(uint n) => AddComputeBuffer(ref VGrid_Lib_BDraw_AppendBuff_Indexes, nameof(VGrid_Lib_BDraw_AppendBuff_Indexes), n);
  public virtual void AssignData_VGrid_Lib_BDraw_AppendBuff_Indexes(params uint[] data) => AddComputeBufferData(ref VGrid_Lib_BDraw_AppendBuff_Indexes, nameof(VGrid_Lib_BDraw_AppendBuff_Indexes), data);
  public virtual void AllocData_VGrid_Lib_BDraw_AppendBuff_Fills1(uint n) => AddComputeBuffer(ref VGrid_Lib_BDraw_AppendBuff_Fills1, nameof(VGrid_Lib_BDraw_AppendBuff_Fills1), n);
  public virtual void AssignData_VGrid_Lib_BDraw_AppendBuff_Fills1(params uint[] data) => AddComputeBufferData(ref VGrid_Lib_BDraw_AppendBuff_Fills1, nameof(VGrid_Lib_BDraw_AppendBuff_Fills1), data);
  public virtual void AllocData_VGrid_Lib_BDraw_AppendBuff_Fills2(uint n) => AddComputeBuffer(ref VGrid_Lib_BDraw_AppendBuff_Fills2, nameof(VGrid_Lib_BDraw_AppendBuff_Fills2), n);
  public virtual void AssignData_VGrid_Lib_BDraw_AppendBuff_Fills2(params uint[] data) => AddComputeBufferData(ref VGrid_Lib_BDraw_AppendBuff_Fills2, nameof(VGrid_Lib_BDraw_AppendBuff_Fills2), data);
  public virtual void AllocData_VGrid_Lib_Vals(uint n) => AddComputeBuffer(ref VGrid_Lib_Vals, nameof(VGrid_Lib_Vals), n);
  public virtual void AssignData_VGrid_Lib_Vals(params float[] data) => AddComputeBufferData(ref VGrid_Lib_Vals, nameof(VGrid_Lib_Vals), data);
  public Texture2D _PaletteTex;
  public float4 palette(float v) => paletteColor(_PaletteTex, v);
  public float4 palette(float v, float w) => float4(palette(v).xyz, w);

  public Texture2D VGrid_Lib_BDraw_fontTexture;
  public virtual void onRenderObject_LIN(bool show, uint _itemN, ref uint i, ref uint index, ref uint3 LIN) { uint n = 0; if (show) { if (i < (n = _itemN)) LIN = uint3(index, i, 0); LIN.z += n; i -= n; } index++; }
  public virtual void onRenderObject_LIN(uint _itemN, ref uint i, ref uint index, ref uint3 LIN) { onRenderObject_LIN(true, _itemN, ref i, ref index, ref LIN); }
  public virtual uint3 onRenderObject_LIN(uint i) { uint3 LIN = u000; uint index = 0; onRenderObject_LIN(VGrid_Lib_drawBox && VGrid_Lib_drawAxes, VGrid_Lib_BDraw_textN, ref i, ref index, ref LIN); onRenderObject_LIN(VGrid_Lib_drawBox, 12, ref i, ref index, ref LIN); onRenderObject_LIN(VGrid_Lib_drawGrid && VGrid_Lib_showSurface, product(VGrid_Lib_viewSize), ref i, ref index, ref LIN); return LIN; }
  public virtual uint3 onRenderObject_LIN_Quads(uint i) { uint3 LIN = u000; uint index = 0; onRenderObject_LIN(VGrid_Lib_drawBox && VGrid_Lib_drawAxes, VGrid_Lib_BDraw_textN, ref i, ref index, ref LIN); onRenderObject_LIN(VGrid_Lib_drawBox, 12, ref i, ref index, ref LIN); return LIN; }
  public virtual uint3 onRenderObject_LIN_Points(uint i) { uint3 LIN = u000; uint index = 0; onRenderObject_LIN(VGrid_Lib_drawGrid && VGrid_Lib_showSurface, product(VGrid_Lib_viewSize), ref i, ref index, ref LIN); return LIN; }
  [HideInInspector] public int kernel_VGrid_Lib_Grid_Calc_Vals; [numthreads(numthreads3, numthreads3, numthreads3)] protected void VGrid_Lib_Grid_Calc_Vals(uint3 id) { unchecked { if (all(id < VGrid_Lib_nodeN)) VGrid_Lib_Grid_Calc_Vals_GS(id); } }
  [HideInInspector] public int kernel_VGrid_Lib_BDraw_AppendBuff_GetIndexes; [numthreads(numthreads1, 1, 1)] protected void VGrid_Lib_BDraw_AppendBuff_GetIndexes(uint3 id) { unchecked { if (id.x < VGrid_Lib_BDraw_AppendBuff_BitN) VGrid_Lib_BDraw_AppendBuff_GetIndexes_GS(id); } }
  [HideInInspector] public int kernel_VGrid_Lib_BDraw_AppendBuff_IncSums; [numthreads(numthreads1, 1, 1)] protected void VGrid_Lib_BDraw_AppendBuff_IncSums(uint3 id) { unchecked { if (id.x < VGrid_Lib_BDraw_AppendBuff_BitN) VGrid_Lib_BDraw_AppendBuff_IncSums_GS(id); } }
  [HideInInspector] public int kernel_VGrid_Lib_BDraw_AppendBuff_IncFills1; [numthreads(numthreads1, 1, 1)] protected void VGrid_Lib_BDraw_AppendBuff_IncFills1(uint3 id) { unchecked { if (id.x < VGrid_Lib_BDraw_AppendBuff_BitN1) VGrid_Lib_BDraw_AppendBuff_IncFills1_GS(id); } }
  [HideInInspector] public int kernel_VGrid_Lib_BDraw_AppendBuff_GetFills2; [numthreads(numthreads1, 1, 1)] protected IEnumerator VGrid_Lib_BDraw_AppendBuff_GetFills2(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < VGrid_Lib_BDraw_AppendBuff_BitN2) yield return StartCoroutine(VGrid_Lib_BDraw_AppendBuff_GetFills2_GS(grp_tid, grp_id, id, grpI)); } }
  [HideInInspector] public int kernel_VGrid_Lib_BDraw_AppendBuff_GetFills1; [numthreads(numthreads1, 1, 1)] protected IEnumerator VGrid_Lib_BDraw_AppendBuff_GetFills1(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < VGrid_Lib_BDraw_AppendBuff_BitN1) yield return StartCoroutine(VGrid_Lib_BDraw_AppendBuff_GetFills1_GS(grp_tid, grp_id, id, grpI)); } }
  [HideInInspector] public int kernel_VGrid_Lib_BDraw_AppendBuff_Get_Bits_Sums; [numthreads(numthreads1, 1, 1)] protected IEnumerator VGrid_Lib_BDraw_AppendBuff_Get_Bits_Sums(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < VGrid_Lib_BDraw_AppendBuff_BitN) yield return StartCoroutine(VGrid_Lib_BDraw_AppendBuff_Get_Bits_Sums_GS(grp_tid, grp_id, id, grpI)); } }
  [HideInInspector] public int kernel_VGrid_Lib_BDraw_AppendBuff_GetSums; [numthreads(numthreads1, 1, 1)] protected IEnumerator VGrid_Lib_BDraw_AppendBuff_GetSums(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < VGrid_Lib_BDraw_AppendBuff_BitN) yield return StartCoroutine(VGrid_Lib_BDraw_AppendBuff_GetSums_GS(grp_tid, grp_id, id, grpI)); } }
  [HideInInspector] public int kernel_VGrid_Lib_BDraw_AppendBuff_Get_Bits; [numthreads(numthreads1, 1, 1)] protected void VGrid_Lib_BDraw_AppendBuff_Get_Bits(uint3 id) { unchecked { if (id.x < VGrid_Lib_BDraw_AppendBuff_BitN) VGrid_Lib_BDraw_AppendBuff_Get_Bits_GS(id); } }
  [HideInInspector] public int kernel_VGrid_Lib_BDraw_setDefaultTextInfo; [numthreads(numthreads1, 1, 1)] protected void VGrid_Lib_BDraw_setDefaultTextInfo(uint3 id) { unchecked { if (id.x < VGrid_Lib_BDraw_textN) VGrid_Lib_BDraw_setDefaultTextInfo_GS(id); } }
  [HideInInspector] public int kernel_VGrid_Lib_BDraw_getTextInfo; [numthreads(numthreads1, 1, 1)] protected void VGrid_Lib_BDraw_getTextInfo(uint3 id) { unchecked { if (id.x < VGrid_Lib_BDraw_textN) VGrid_Lib_BDraw_getTextInfo_GS(id); } }
  [HideInInspector] public int kernel_VGrid_Lib_Grid_Simple_TraceRay; [numthreads(numthreads2, numthreads2, 1)] protected void VGrid_Lib_Grid_Simple_TraceRay(uint3 id) { unchecked { if (all(id.xy < VGrid_Lib_viewSize)) VGrid_Lib_Grid_Simple_TraceRay_GS(id); } }
  [HideInInspector] public int kernel_VGrid_Lib_Grid_TraceRay; [numthreads(numthreads2, numthreads2, 1)] protected void VGrid_Lib_Grid_TraceRay(uint3 id) { unchecked { if (all(id.xy < VGrid_Lib_viewSize)) VGrid_Lib_Grid_TraceRay_GS(id); } }
  public virtual v2f vert_GS(uint i, uint j, v2f o)
  {
    uint3 LIN = onRenderObject_LIN(i); int index = -1, level = ((int)LIN.x); i = LIN.y;
    if (level == ++index) { o = vert_VGrid_Lib_BDraw_Text(i, j, o); o.tj.x = 0; }
    else if (level == ++index) { o = vert_VGrid_Lib_BDraw_Box(i, j, o); o.tj.x = 0; }
    else if (level == ++index) { o = vert_VGrid_Lib_DrawScreen(i, j, o); o.tj.x = 0; }
    return o;
  }
  public v2f vert(uint i, uint j)
  {
    v2f o = default;
    o.pos = o.color = o.tj = o.tk = f0000; o.normal = o.p0 = o.p1 = o.wPos = f000; o.uv = f00; o.ti = float4(i, j, 0, 0);
    return vert_GS(i, j, o);
  }
  public virtual bool onRenderObject_GS_N(bool cpu)
  {
    RenderQuads(material, onRenderObject_LIN_Quads(0).z);
    RenderPoints(material, onRenderObject_LIN_Points(0).z);
    return true;
  }
  public override bool onRenderObject()
  {
    if (gVGrid_Lib_Doc == null) return false;
    bool render = true, cpu = false;
    onRenderObject_GS(ref render, ref cpu);
    g_SetData();
    if (!render) return false;
    if (cpu) Cpu(material, new { gVGrid_Lib_Doc }, new { VGrid_Lib_BDraw_tab_delimeted_text }, new { VGrid_Lib_BDraw_textInfos }, new { VGrid_Lib_BDraw_fontInfos }, new { VGrid_Lib_depthColors }, new { VGrid_Lib_paletteBuffer }, new { VGrid_Lib_BDraw_AppendBuff_Bits }, new { VGrid_Lib_BDraw_AppendBuff_Sums }, new { VGrid_Lib_BDraw_AppendBuff_Indexes }, new { VGrid_Lib_BDraw_AppendBuff_grp }, new { VGrid_Lib_BDraw_AppendBuff_grp0 }, new { VGrid_Lib_BDraw_AppendBuff_Fills1 }, new { VGrid_Lib_BDraw_AppendBuff_Fills2 }, new { VGrid_Lib_Vals }, new { VGrid_Lib_BDraw_fontTexture }, new { _PaletteTex });
    else Gpu(material, new { gVGrid_Lib_Doc }, new { VGrid_Lib_BDraw_tab_delimeted_text }, new { VGrid_Lib_BDraw_textInfos }, new { VGrid_Lib_BDraw_fontInfos }, new { VGrid_Lib_depthColors }, new { VGrid_Lib_paletteBuffer }, new { VGrid_Lib_BDraw_AppendBuff_Bits }, new { VGrid_Lib_BDraw_AppendBuff_Sums }, new { VGrid_Lib_BDraw_AppendBuff_Indexes }, new { VGrid_Lib_BDraw_AppendBuff_grp }, new { VGrid_Lib_BDraw_AppendBuff_grp0 }, new { VGrid_Lib_BDraw_AppendBuff_Fills1 }, new { VGrid_Lib_BDraw_AppendBuff_Fills2 }, new { VGrid_Lib_Vals }, new { VGrid_Lib_BDraw_fontTexture }, new { _PaletteTex });
    return onRenderObject_GS_N(cpu);
  }
  public virtual void onRenderObject_GS(ref bool render, ref bool cpu)
  {
    VGrid_Lib_onRenderObject_GS(ref render, ref cpu);
    Views_Lib_onRenderObject_GS(ref render, ref cpu);
  }
  public virtual float4 frag_GS(v2f i, float4 color) => frag_VGrid_Lib_GS(i, color);
  public virtual float4 frag(v2f i)
  {
    float4 color = i.color;
    color = frag_GS(i, color);
    if (color.a == 0) Discard(0);
    return color;
  }
  #region <VGrid_Lib>
  public Light VGrid_Lib__directionalLightObj = null;
  public Light VGrid_Lib_directionalLightObj { get { if (VGrid_Lib__directionalLightObj == null) VGrid_Lib__directionalLightObj = FindGameObject("Directional Light").GetComponent<Light>(); return VGrid_Lib__directionalLightObj; } set => VGrid_Lib__directionalLightObj = value; }
  public virtual bool VGrid_Lib_ViewportChanged() => any(VGrid_Lib_viewRect != roundu(mainCam.pixelRect));
  public Camera VGrid_Lib__legendCam; public Camera VGrid_Lib_legendCam => VGrid_Lib__legendCam ??= mainCam.gameObject.transform.parent.Find("Legend Camera")?.GetComponent<Camera>();
  public UI_TreeGroup VGrid_Lib_ui_VGrid_Lib_group_VGrid_Lib => UI_VGrid_Lib_group_VGrid_Lib;
  public virtual void VGrid_Lib_updateScreenSize() => VGrid_Lib_viewSize = (VGrid_Lib_viewRect = roundu(mainCam.pixelRect)).zw;
  public virtual void VGrid_Lib_AllocData_VGrid_Lib_depthColors(uint n) => AddComputeBuffer(ref VGrid_Lib_depthColors, nameof(VGrid_Lib_depthColors), n);
  public virtual void VGrid_Lib_AllocData_VGrid_Lib_Vals(uint n) => AddComputeBuffer(ref VGrid_Lib_Vals, nameof(VGrid_Lib_Vals), n);
  public IVGrid_Lib VGrid_Lib_iVGrid => lib_parent_gs as IVGrid_Lib;
  public virtual void VGrid_Lib_InitVariableNBuffers(float maxNodeEdgeN) => GS_VGrid_Lib.VGrid_Lib_InitVariableNBuffers(VGrid_Lib_iVGrid, maxNodeEdgeN);
  public virtual void VGrid_Lib_InitVariableNBuffers() => VGrid_Lib_InitVariableNBuffers(806);
  public virtual void VGrid_Lib_ResizeGrid() { if (VGrid_Lib_resolution > 0) { UI_VGrid_Lib_slices.range_Min = VGrid_Lib_gridMin() - VGrid_Lib_gridExtent(); UI_VGrid_Lib_slices.range_Max = VGrid_Lib_gridMax() + VGrid_Lib_gridExtent(); VGrid_Lib_InitVariableNBuffers(); Gpu_VGrid_Lib_Grid_Calc_Vals(); } }
  public virtual void VGrid_Lib_Start0_GS() { base_VGrid_Lib_Start0_GS(); if (VGrid_Lib_resolution == 0) VGrid_Lib_resolution = 0.1f; }
  public virtual void VGrid_Lib_LateUpdate0_GS()
  {
    VGrid_Lib_showAxes = VGrid_Lib_drawGrid && VGrid_Lib_drawAxes; VGrid_Lib_showMeshRange = VGrid_Lib_drawGrid && VGrid_Lib_twoSided; VGrid_Lib_showMeshVal = VGrid_Lib_drawGrid && !VGrid_Lib_twoSided;
    VGrid_Lib_showOutline = VGrid_Lib_drawGrid && VGrid_Lib_drawBox; VGrid_Lib_showNormalizedAxes = VGrid_Lib_drawGrid && VGrid_Lib_customAxesRangeN > 0; VGrid_Lib_showSurface = VGrid_Lib_drawGrid && VGrid_Lib_drawSurface;
    if (UI_VGrid_Lib_paletteRange.Changed) UI_VGrid_Lib_meshVal.range = VGrid_Lib_paletteRange;
  }
  public virtual void VGrid_Lib_LateUpdate1_GS()
  {
    base_VGrid_Lib_LateUpdate1_GS();
    if (!isInitBuffers) return;
    if (VGrid_Lib_buildText) VGrid_Lib_RebuildText();
    if (mainCam.transform.hasChanged) { VGrid_Lib_retrace = true; mainCam.transform.hasChanged = false; }
    if (VGrid_Lib_reCalc) { VGrid_Lib_ResizeGrid(); VGrid_Lib_retrace = true; }
    if (VGrid_Lib_retrace) VGrid_Lib_TraceRays();
    VGrid_Lib_reCalc = VGrid_Lib_buildText = VGrid_Lib_retrace = ValuesChanged = gChanged = false;
  }
  public virtual bool VGrid_Lib_Vals_Ok => VGrid_Lib_Vals != null;
  public virtual void VGrid_Lib_TraceRays()
  {
    VGrid_Lib_updateScreenSize();
    VGrid_Lib_maxDist = mainCam.farClipPlane; VGrid_Lib_camToWorld = mainCam.cameraToWorldMatrix; VGrid_Lib_isOrtho = mainCam.orthographic;
    VGrid_Lib_orthoSize = mainCam.orthographicSize; VGrid_Lib_cameraInvProjection = mainCam.projectionMatrix.inverse;
    if (VGrid_Lib_Vals_Ok) if (VGrid_Lib_twoSided) Gpu_VGrid_Lib_Grid_TraceRay(); else Gpu_VGrid_Lib_Grid_Simple_TraceRay();
  }
  public virtual float3 VGrid_Lib__axesRangeMin() => VGrid_Lib_axesRangeMin / (siUnits ? 1 : 0.3048f);
  public virtual float3 VGrid_Lib__axesRangeMax() => VGrid_Lib_axesRangeMax / (siUnits ? 1 : 0.3048f);
  public virtual float3 VGrid_Lib__axesRangeMin1() => VGrid_Lib_axesRangeMin1 / (siUnits ? 1 : 0.3048f);
  public virtual float3 VGrid_Lib__axesRangeMax1() => VGrid_Lib_axesRangeMax1 / (siUnits ? 1 : 0.3048f);
  public virtual bool VGrid_Lib_AddAxes1() { VGrid_Lib_BDraw_AddAxes(VGrid_Lib_textSize.x, VGrid_Lib_textSize.y, VGrid_Lib_gridMin(), VGrid_Lib_gridMax(), VGrid_Lib__axesRangeMin(), VGrid_Lib__axesRangeMax(), float4(VGrid_Lib_axesColor, VGrid_Lib_axesOpacity), VGrid_Lib_titles, VGrid_Lib_axesFormats); return true; }
  public virtual bool VGrid_Lib_AddAxes2() { VGrid_Lib_BDraw_AddAxes(VGrid_Lib_textSize.x, VGrid_Lib_textSize.y, float4(VGrid_Lib_axesColor, VGrid_Lib_axesOpacity), VGrid_Lib_gridMin(), VGrid_Lib_gridMax(), VGrid_Lib__axesRangeMin(), VGrid_Lib__axesRangeMax(), VGrid_Lib__axesRangeMin1(), VGrid_Lib__axesRangeMax1(), VGrid_Lib_titles, VGrid_Lib_axesFormats); return true; }
  public virtual bool VGrid_Lib_AddAxes3() { VGrid_Lib_BDraw_AddAxes(VGrid_Lib_textSize.x, VGrid_Lib_textSize.y, VGrid_Lib_gridMin(), VGrid_Lib_gridMax(), float4(VGrid_Lib_axesColor, VGrid_Lib_axesOpacity), VGrid_Lib_titles, VGrid_Lib_axesFormats, VGrid_Lib_zeroOrigin); return true; }
  public virtual void VGrid_Lib_RebuildText()
  {
    VGrid_Lib_BDraw_ClearTexts();
    bool r = VGrid_Lib_showAxes && VGrid_Lib_customAxesRangeN switch { 1 => VGrid_Lib_AddAxes1(), 2 => VGrid_Lib_AddAxes2(), _ => VGrid_Lib_AddAxes3() };
    VGrid_Lib_RebuildText_Extra();
    VGrid_Lib_BDraw_BuildTexts();
  }
  public virtual void VGrid_Lib_RebuildText_Extra() { }
  public virtual void VGrid_Lib_InitBuffers0_GS() { base_VGrid_Lib_InitBuffers0_GS(); VGrid_Lib_nodeN = roundu((VGrid_Lib_gridMax() - VGrid_Lib_gridMin()) / VGrid_Lib_resolution + f111); VGrid_Lib_retrace = VGrid_Lib_reCalc = VGrid_Lib_buildText = true; }
  public virtual void VGrid_Lib_InitBuffers1_GS()
  {
    base_VGrid_Lib_InitBuffers1_GS();
    VGrid_Lib_updateScreenSize();
    _WorldSpaceLightPos0 = float4(-VGrid_Lib_directionalLightObj.transform.forward, VGrid_Lib_directionalLightObj.intensity);
    VGrid_Lib_RebuildText();
    VGrid_Lib_retrace = VGrid_Lib_reCalc = true;
  }
  public float4 VGrid_Lib_paletteBufferColor(float v) => c32_f4(VGrid_Lib_paletteBuffer[roundu(clamp(v * 255, 0, 255))]);
  public float4 VGrid_Lib_paletteBufferColor(float v, float w) => float4(VGrid_Lib_paletteBufferColor(v).xyz, w);
  public float3 VGrid_Lib_gridMin() => float3(VGrid_Lib_GridX.x, VGrid_Lib_GridY.x, VGrid_Lib_GridZ.x);
  public float3 VGrid_Lib_gridMax() => float3(VGrid_Lib_GridX.y, VGrid_Lib_GridY.y, VGrid_Lib_GridZ.y);
  public float3 VGrid_Lib_gridExtent() => VGrid_Lib_gridMax() - VGrid_Lib_gridMin();
  public float3 VGrid_Lib_gridSize() => VGrid_Lib_gridMax() - VGrid_Lib_gridMin();
  public float3 VGrid_Lib_gridCenter() => (VGrid_Lib_gridMax() + VGrid_Lib_gridMin()) / 2;
  public float2 VGrid_Lib_axesRangeX() => float2(VGrid_Lib_axesRangeMin.x, VGrid_Lib_axesRangeMax.x);
  public float2 VGrid_Lib_axesRangeY() => float2(VGrid_Lib_axesRangeMin.y, VGrid_Lib_axesRangeMax.y);
  public float2 VGrid_Lib_axesRangeZ() => float2(VGrid_Lib_axesRangeMin.z, VGrid_Lib_axesRangeMax.z);
  public uint3 VGrid_Lib_NodeID(uint i) => i_to_id(i, VGrid_Lib_nodeN);
  public uint VGrid_Lib_NodeI(uint3 id) => id_to_i(id, VGrid_Lib_nodeN);
  public float3 VGrid_Lib_NodeLocation(uint i) => VGrid_Lib_NodeID(i) * VGrid_Lib_resolution + VGrid_Lib_gridMin();
  public float3 VGrid_Lib_NodeLocation3(uint3 id) => id * VGrid_Lib_resolution + VGrid_Lib_gridMin();
  public uint3 VGrid_Lib_nodeN1() => max(VGrid_Lib_nodeN, u111) - u111;
  public uint VGrid_Lib_GridToIndex(int3 _I) => id_to_i(clamp(_I, u000, VGrid_Lib_nodeN1()), VGrid_Lib_nodeN);
  public float3 VGrid_Lib_GetCorner_q(float3 p) => clamp((p - VGrid_Lib_gridMin()) / VGrid_Lib_resolution, f000, (int3)VGrid_Lib_nodeN - f111);
  public uint3 VGrid_Lib_GetCorner_I(float3 q) => (uint3)q;
  public uint4 VGrid_Lib_GetFaceI(uint3 _I, int3 x, int3 y, int3 z, int3 w) => new uint4(VGrid_Lib_GridToIndex((int3)_I + x), VGrid_Lib_GridToIndex((int3)_I + y), VGrid_Lib_GridToIndex((int3)_I + z), VGrid_Lib_GridToIndex((int3)_I + w));
  public uint4 VGrid_Lib_GetFaceI(uint3 _I, int3 d) => VGrid_Lib_GetFaceI(_I, i000 + d, i001 + d, i010 + d, i011 + d);
  public virtual float VGrid_Lib_Val(uint i) => VGrid_Lib_Vals[i];
  public virtual float VGrid_Lib_Val3(uint3 id) => VGrid_Lib_Val(VGrid_Lib_NodeI(id));
  public float VGrid_Lib_Interpolate_Val(float3 fq, uint4 c0, uint4 c1) => Interpolate(VGrid_Lib_Val(c0.x), VGrid_Lib_Val(c0.y), VGrid_Lib_Val(c0.z), VGrid_Lib_Val(c0.w), VGrid_Lib_Val(c1.x), VGrid_Lib_Val(c1.y), VGrid_Lib_Val(c1.z), VGrid_Lib_Val(c1.w), fq);
  public virtual float VGrid_Lib_Val(float3 p) { float3 q = VGrid_Lib_GetCorner_q(p); uint3 _I = VGrid_Lib_GetCorner_I(q); return VGrid_Lib_Interpolate_Val(frac(q), VGrid_Lib_GetFaceI(_I, i000), VGrid_Lib_GetFaceI(_I, i100)); }
  public virtual float3 VGrid_Lib_Val(float3 p, float d) => float3(VGrid_Lib_Val(p + f100 * d), VGrid_Lib_Val(p + f010 * d), VGrid_Lib_Val(p + f001 * d));
  public float VGrid_Lib_setDepth(float depth, VGrid_Lib_TRay ray, ref float3 p, ref float val) { p = depth * ray.direction + ray.origin; val = VGrid_Lib_Val(p); return depth; }
  public float3 VGrid_Lib_Normal(float3 p)
  {
    float r = VGrid_Lib_resolution * 0.5f, margin = r * 0.02f;
    float3 normal = p <= VGrid_Lib_gridMin() + margin;
    if (all(normal == f000)) normal = p >= VGrid_Lib_gridMax() - margin;
    if (all(normal == f000)) normal = normalize(VGrid_Lib_Val(p, r) - VGrid_Lib_Val(p, -r));
    return normal;
  }
  public uint2 VGrid_Lib_pixDepthColor(uint i) => VGrid_Lib_depthColors[i];
  public uint2 VGrid_Lib_pixDepthColor(uint2 id) => VGrid_Lib_pixDepthColor(id_to_i(id, VGrid_Lib_viewSize));
  public float VGrid_Lib_pixDepth(uint2 dc) => dc.x / (float)uint_max * (VGrid_Lib_maxDist - 2); 
  public float4 VGrid_Lib_pixColor(uint2 dc) => c32_f4(u_c32(dc.y));
  public virtual VGrid_Lib_TRay VGrid_Lib_CreateShaderCameraRay(float2 _uv)
  {
    VGrid_Lib_TRay ray;
    ray.origin = mul(VGrid_Lib_camToWorld, VGrid_Lib_isOrtho ? float4(VGrid_Lib_orthoSize * _uv / float2(aspect(VGrid_Lib_viewSize), 1), 0, 1) : f0001).xyz;
    ray.direction = normalize(mul(VGrid_Lib_camToWorld, float4(mul(VGrid_Lib_cameraInvProjection, float4(_uv, 0, 1)).xyz, 0)).xyz);
    ray.color = f0000;
    ray.dist = 0;
    return ray;
  }
  public virtual VGrid_Lib_TRay VGrid_Lib_CreateRay(float3 origin, float3 direction) { VGrid_Lib_TRay ray; ray.origin = origin; ray.direction = direction; ray.color = f0000; ray.dist = 0; return ray; }
  public virtual VGrid_Lib_TRay VGrid_Lib_CreateCameraRay(float2 _uv) { VGrid_Lib_TRay ray = VGrid_Lib_CreateShaderCameraRay(_uv); return VGrid_Lib_CreateRay(ray.origin, ray.direction); }
  public void VGrid_Lib_gridMin(float3 v) { VGrid_Lib_GridX = float2(v.x, VGrid_Lib_GridX.y); VGrid_Lib_GridY = float2(v.y, VGrid_Lib_GridY.y); VGrid_Lib_GridZ = float2(v.z, VGrid_Lib_GridZ.y); }
  public void VGrid_Lib_gridMax(float3 v) { VGrid_Lib_GridX = float2(VGrid_Lib_GridX.x, v.x); VGrid_Lib_GridY = float2(VGrid_Lib_GridY.x, v.y); VGrid_Lib_GridZ = float2(VGrid_Lib_GridZ.x, v.x); }
  public void VGrid_Lib_gridSize(float3 v) { float3 c = VGrid_Lib_gridCenter(); v /= 2; VGrid_Lib_gridMin(c - v); VGrid_Lib_gridMax(c + v); }
  public void VGrid_Lib_gridCenter(float3 v) { float3 r = VGrid_Lib_gridSize() / 2; VGrid_Lib_gridMin(v - r); VGrid_Lib_gridMax(v + r); }
  public void VGrid_Lib_pixDepthColor(uint i, float d, float4 c) => VGrid_Lib_depthColors[i] = uint2((uint)(d / VGrid_Lib_maxDist * uint_max), c32_u(f4_c32(c)));
  public void VGrid_Lib_pixDepthColor(uint2 id, float d, float4 c) => VGrid_Lib_pixDepthColor(id_to_i(id, VGrid_Lib_viewSize), d, c);
  public VGrid_Lib_TRay VGrid_Lib_CreateRayHit() { VGrid_Lib_TRay hit; hit.origin = f000; hit.dist = fPosInf; hit.direction = f000; hit.color = f0000; return hit; }
  public void VGrid_Lib_Assign(ref VGrid_Lib_TRay hit, float3 position, float3 normal, float4 color, float dist) { hit.origin = position; hit.direction = normal; hit.color = color; hit.dist = dist; }
  public void VGrid_Lib_Val(uint i, float v) => VGrid_Lib_Vals[i] = v;
  public void VGrid_Lib_Val3(uint3 id, float v) => VGrid_Lib_Val(VGrid_Lib_NodeI(id), v);
  public bool VGrid_Lib_insideSurface(float val) => IsInside(val, VGrid_Lib_meshRange);
  public virtual float4 VGrid_Lib_GetPointColor(float3 p, float val) => VGrid_Lib_paletteBufferColor(lerp1(VGrid_Lib_paletteRange, val));
  public virtual float4 VGrid_Lib_GetNormalColor(VGrid_Lib_TRay ray, float3 normal, float val, float3 p)
  {
    float3 lightDirection = _WorldSpaceLightPos0.xyz;
    float3 h = (lightDirection - ray.direction) / 2;
    float v = 1, gloss = 0.5f, s = sqr(abs(dot(normal, h))) * gloss, NdotL = abs(dot(normal, lightDirection));
    if (VGrid_Lib_GridLineThickness > 0)
    {
      float w = 1 / max(0.00001f, VGrid_Lib_GridLineThickness);
      p /= VGrid_Lib_resolution;
      float3 blend = normalize(max((abs(normal) - 0.2f) * 7, 0.0f));
      v = csum(blend * saturate(0.5f * float3(product((1 - abs(1 - 2 * frac(p.yz))) * w), product((1 - abs(1 - 2 * frac(p.xz))) * w), product((1 - abs(1 - 2 * frac(p.xy))) * w))));
    }
    return float4(saturate((VGrid_Lib_GetPointColor(p, val).xyz * (NdotL + 0.5f) + s) * v), VGrid_Lib_opacity);
  }
  public virtual float4 VGrid_Lib_GetColor(VGrid_Lib_TRay ray, ref float3 normal, float val, float3 p) => VGrid_Lib_GetNormalColor(ray, normal = VGrid_Lib_Normal(p), val, p);
  public virtual float4 VGrid_Lib_DrawSliceColor(float4 color, float3 q) => color;
  public virtual void VGrid_Lib_DrawSlice(float3 axis, VGrid_Lib_TRay ray, ref VGrid_Lib_TRay hit, ref float3 p, ref float val, ref float depth, ref bool found, ref float4 color, float3 normal)
  {
    float depth_slice;
    float3 axis2 = rotateZYXDeg(axis, VGrid_Lib_sliceRotation), q = PlaneLineIntersectionPoint(axis2, VGrid_Lib_slices, ray.origin, ray.direction);
    if (q.x != fNegInf && IsNotOutside(q, VGrid_Lib_gridMin(), VGrid_Lib_gridMax()))
    {
      depth_slice = VGrid_Lib_setDepth(max(distance(q, ray.origin), 0.018f), ray, ref p, ref val);
      if (depth_slice < depth) { found = true; depth = depth_slice; color = VGrid_Lib_DrawSliceColor(VGrid_Lib_GetNormalColor(ray, axis2, val, p), q); VGrid_Lib_Assign(ref hit, p, normal, color, depth); }
    }
  }
  public virtual void VGrid_Lib_TraceRay(uint3 id, bool isSimple)
  {
    VGrid_Lib_TRay ray = VGrid_Lib_CreateCameraRay(2.0f * id.xy / VGrid_Lib_viewSize - 1), hit = VGrid_Lib_CreateRayHit();
    float3 mn = VGrid_Lib_gridMin(), mx = max(mn + 0.001f, VGrid_Lib_gridMax());
    float2 dst = HitGridBox(mn, mx, ray.origin, ray.direction);
    bool hitOutside = HitOutsideGrid(dst), hitInside = HitInsideGrid(dst);
    if (hitOutside || hitInside)
    {
      float3 p = f000, normal = f100;
      float val = 0, depth = VGrid_Lib_setDepth(max(dst.x, 0.018f), ray, ref p, ref val), depth2 = dst.y, step = VGrid_Lib_resolution, d0, d2;
      bool found = false;
      float4 color = f0000;
      if (VGrid_Lib_GridDrawFront && hitOutside) { color = VGrid_Lib_GetColor(ray, ref normal, val, p); VGrid_Lib_Assign(ref hit, p, normal, color, depth); }
      else
      {
        float val0 = val;
        if (isSimple)
        {
          float v = VGrid_Lib_meshVal;
          for (uint i = 0, n = csum(VGrid_Lib_nodeN); i < n && !found; i++)
          {
            if (val0 > v && val <= v)
            {
              val0 = val;
              for (d0 = max(0, depth - step), d2 = depth; d2 - d0 > step / 100;) { depth = VGrid_Lib_setDepth((d0 + d2) / 2, ray, ref p, ref val); if (val > v) d0 = depth; else d2 = depth; }
              color = VGrid_Lib_GetColor(ray, ref normal, val, p);
              VGrid_Lib_Assign(ref hit, p, normal, color, depth); found = true;
            }
            else if (val0 < v && val >= v)
            {
              val0 = val;
              for (d0 = depth - step, d2 = depth; d2 - d0 > step / 100;) { depth = VGrid_Lib_setDepth((d0 + d2) / 2, ray, ref p, ref val); if (val < v) d0 = depth; else d2 = depth; }
              if (depth + step < depth2)
              {
                color = VGrid_Lib_GetColor(ray, ref normal, val, p);
                VGrid_Lib_Assign(ref hit, p, normal, color, depth); found = true;
              }
            }
            else val0 = val;
            if (depth + step > depth2) { depth = VGrid_Lib_setDepth(depth2, ray, ref p, ref val); break; }
            depth = VGrid_Lib_setDepth(depth + step, ray, ref p, ref val);
          }
        }
        else
        {
          for (uint i = 0, n = csum(VGrid_Lib_nodeN); i < n && (VGrid_Lib_opacity < 0.999f || !found); i++)
          {
            if (i == 0 && hitOutside && val <= VGrid_Lib_meshRange.y && val >= VGrid_Lib_meshRange.x)
            {
              val0 = val;
              color = VGrid_Lib_GetColor(ray, ref normal, val, p);
              VGrid_Lib_Assign(ref hit, p, normal, color, depth);
              found = true;
            }
            else if (val0 > VGrid_Lib_meshRange.y && val <= VGrid_Lib_meshRange.y)
            {
              val0 = val;
              for (d0 = max(0, depth - step), d2 = depth; d2 - d0 > step / 100;) { depth = VGrid_Lib_setDepth((d0 + d2) / 2, ray, ref p, ref val); if (val > VGrid_Lib_meshRange.y) d0 = depth; else d2 = depth; }
              color = VGrid_Lib_GetColor(ray, ref normal, val, p);
              if (!found) { VGrid_Lib_Assign(ref hit, p, normal, color, depth); found = true; }
              else hit.color.xyz = VGrid_Lib_opacity * hit.color.xyz + (1 - VGrid_Lib_opacity) * color.xyz;
            }
            else if (val0 < VGrid_Lib_meshRange.x && val >= VGrid_Lib_meshRange.x)
            {
              val0 = val;
              for (d0 = depth - step, d2 = depth; d2 - d0 > step / 100;) { depth = VGrid_Lib_setDepth((d0 + d2) / 2, ray, ref p, ref val); if (val < VGrid_Lib_meshRange.x) d0 = depth; else d2 = depth; }
              if (depth + step < depth2)
              {
                color = VGrid_Lib_GetColor(ray, ref normal, val, p);
                if (!found) { VGrid_Lib_Assign(ref hit, p, normal, color, depth); found = true; }
                else hit.color.xyz = VGrid_Lib_opacity * hit.color.xyz + (1 - VGrid_Lib_opacity) * color.xyz;
              }
            }
            else if (val0 > VGrid_Lib_meshRange.x && val <= VGrid_Lib_meshRange.x)
            {
              val0 = val;
              for (d0 = depth - step, d2 = depth; d2 - d0 > step / 100;) { depth = VGrid_Lib_setDepth((d0 + d2) / 2, ray, ref p, ref val); if (val > VGrid_Lib_meshRange.x) d0 = depth; else d2 = depth; }
              color = VGrid_Lib_GetColor(ray, ref normal, val, p);
              if (!found) { VGrid_Lib_Assign(ref hit, p, normal, color, depth); found = true; }
              else hit.color.xyz = VGrid_Lib_opacity * hit.color.xyz + (1 - VGrid_Lib_opacity) * color.xyz;
            }
            else if (val0 < VGrid_Lib_meshRange.y && val >= VGrid_Lib_meshRange.y)
            {
              val0 = val;
              for (d0 = depth - step, d2 = depth; d2 - d0 > step / 100;) { depth = VGrid_Lib_setDepth((d0 + d2) / 2, ray, ref p, ref val); if (val < VGrid_Lib_meshRange.y) d0 = depth; else d2 = depth; }
              color = VGrid_Lib_GetColor(ray, ref normal, val, p);
              if (!found) { VGrid_Lib_Assign(ref hit, p, normal, color, depth); found = true; }
              else hit.color.xyz = VGrid_Lib_opacity * hit.color.xyz + (1 - VGrid_Lib_opacity) * color.xyz;
            }
            else if (hitInside && depth > step / 2 && val0 > VGrid_Lib_meshRange.x && val <= VGrid_Lib_meshRange.x)
            {
              val0 = val;
              for (d0 = depth - step, d2 = depth; d2 - d0 > step / 100;) { depth = VGrid_Lib_setDepth((d0 + d2) / 2, ray, ref p, ref val); if (val > VGrid_Lib_meshRange.x) d0 = depth; else d2 = depth; }
              color = VGrid_Lib_GetColor(ray, ref normal, val, p);
              if (!found) { VGrid_Lib_Assign(ref hit, p, normal, color, depth); found = true; }
              else hit.color.xyz = VGrid_Lib_opacity * hit.color.xyz + (1 - VGrid_Lib_opacity) * color.xyz;
            }
            else val0 = val;
            if (depth + step > depth2) { depth = VGrid_Lib_setDepth(depth2, ray, ref p, ref val); break; }
            depth = VGrid_Lib_setDepth(depth + step, ray, ref p, ref val);
          }
        }
        if (VGrid_Lib_show_slices) for (uint i = 0; i < 3; i++) VGrid_Lib_DrawSlice(index(f000, i, 1.0f), ray, ref hit, ref p, ref val, ref depth, ref found, ref color, normal);
        if (VGrid_Lib_GridDrawBack)
        {
          color = VGrid_Lib_GetColor(ray, ref normal, val, p);
          if (!found) VGrid_Lib_Assign(ref hit, p, normal, color, depth);
          else hit.color.xyz = VGrid_Lib_opacity * hit.color.xyz + (1 - VGrid_Lib_opacity) * color.xyz;
        }
      }
    }
    VGrid_Lib_pixDepthColor(id.xy, hit.dist, hit.color);
  }
  public virtual void VGrid_Lib_Grid_Calc_Vals_GS(uint3 id)
  {
    float3 p = VGrid_Lib_NodeLocation3(id);
    float R = 0.3333f, v = sqr(length(p.xz) - R) + sqr(p.y);
    VGrid_Lib_Val3(id, v * 10);
  }
  public virtual void VGrid_Lib_Grid_TraceRay_GS(uint3 id) { VGrid_Lib_TraceRay(id, false); }
  public virtual void VGrid_Lib_Grid_Simple_TraceRay_GS(uint3 id) { VGrid_Lib_TraceRay(id, true); }
  public Camera VGrid_Lib_vrCam;
  public Transform VGrid_Lib_camTransform0;
  public virtual v2f vert_VGrid_Lib_BDraw_Box(uint i, uint j, v2f o) => vert_VGrid_Lib_BDraw_BoxFrame(VGrid_Lib_gridMin(), VGrid_Lib_gridMax(), VGrid_Lib_boxLineThickness, DARK_BLUE, i, j, o);
  public virtual v2f vert_VGrid_Lib_DrawScreen(uint i, uint j, v2f o)
  {
    uint2 id = i_to_id(i, VGrid_Lib_viewSize), dc = VGrid_Lib_pixDepthColor(i);
    float2 uv = (id + f11 * 0.5f) / VGrid_Lib_viewSize * 2 - 1;
    VGrid_Lib_TRay ray = VGrid_Lib_CreateShaderCameraRay(uv);
    return vert_VGrid_Lib_BDraw_Point(ray.origin + VGrid_Lib_pixDepth(dc) * ray.direction, VGrid_Lib_pixColor(dc), i, o);
  }
  public virtual float4 frag_VGrid_Lib_GS(v2f i, float4 color)
  {
    switch (VGrid_Lib_BDraw_o_drawType(i))
    {
      case uint_max: Discard(0); break;
      case VGrid_Lib_BDraw_Draw_Sphere: color = frag_VGrid_Lib_BDraw_Sphere(i); break;
      case VGrid_Lib_BDraw_Draw_Line: color = frag_VGrid_Lib_BDraw_Line(i); break;
      case VGrid_Lib_BDraw_Draw_Arrow: color = frag_VGrid_Lib_BDraw_Arrow(i); break;
      case VGrid_Lib_BDraw_Draw_Signal: color = frag_VGrid_Lib_BDraw_Signal(i); break;
      case VGrid_Lib_BDraw_Draw_LineSegment: color = frag_VGrid_Lib_BDraw_LineSegment(i); break;
      case VGrid_Lib_BDraw_Draw_Mesh: color = frag_VGrid_Lib_BDraw_Mesh(i); break;
      case VGrid_Lib_BDraw_Draw_Text3D:
        VGrid_Lib_BDraw_TextInfo t = VGrid_Lib_BDraw_textInfo(VGrid_Lib_BDraw_o_i(i));
        color = frag_VGrid_Lib_BDraw_Text(VGrid_Lib_BDraw_fontTexture, VGrid_Lib_BDraw_tab_delimeted_text, VGrid_Lib_BDraw_fontInfos, VGrid_Lib_BDraw_fontSize, t.quadType, t.backColor, VGrid_Lib_BDraw_Get_text_indexes(t.textI), i);
        break;
    }
    return color;
  }

  public virtual void base_VGrid_Lib_Start0_GS()
  {
    VGrid_Lib_BDraw_Start0_GS();
  }
  public virtual void base_VGrid_Lib_Start1_GS()
  {
    VGrid_Lib_BDraw_Start1_GS();
  }
  public virtual void base_VGrid_Lib_OnApplicationQuit() { Save_UI(); OnApplicationQuit_GS(); base.OnApplicationQuit(); already_quited = true; }
  public virtual void base_VGrid_Lib_LateUpdate0_GS()
  {
    VGrid_Lib_BDraw_LateUpdate0_GS();
  }
  public virtual void base_VGrid_Lib_LateUpdate1_GS()
  {
    VGrid_Lib_BDraw_LateUpdate1_GS();
  }
  public virtual void base_VGrid_Lib_Update0_GS()
  {
    VGrid_Lib_BDraw_Update0_GS();
  }
  public virtual void base_VGrid_Lib_Update1_GS()
  {
    VGrid_Lib_BDraw_Update1_GS();
  }
  public virtual void base_VGrid_Lib_OnValueChanged_GS()
  {
    VGrid_Lib_BDraw_OnValueChanged_GS();
    var type = "gsReport_Lib".ToType();
    if (type != null) ((GS)GetComponent(type))?.OnValueChanged_GS();
  }
  public virtual void base_VGrid_Lib_InitBuffers0_GS()
  {
    VGrid_Lib_BDraw_InitBuffers0_GS();
  }
  public virtual void base_VGrid_Lib_InitBuffers1_GS()
  {
    VGrid_Lib_BDraw_InitBuffers1_GS();
  }
  [HideInInspector]
  public virtual void base_VGrid_Lib_onRenderObject_GS(ref bool render, ref bool cpu)
  {
    VGrid_Lib_BDraw_onRenderObject_GS(ref render, ref cpu);
  }
  public virtual float4 base_frag_VGrid_Lib_GS(v2f i, float4 color) { return frag_VGrid_Lib_BDraw_GS(i, color); }
  public float VGrid_Lib_BDraw_wrapJ(uint j, uint n) => ((j + n) % 6) / 3;
  public uint2 VGrid_Lib_BDraw_JQuadu(uint j) => uint2(j + 2, j + 1) / 3 % 2;
  public float2 VGrid_Lib_BDraw_JQuadf(uint j) => (float2)VGrid_Lib_BDraw_JQuadu(j);
  public float4 VGrid_Lib_BDraw_Number_quadPoint(float rx, float ry, uint j) { float2 p = VGrid_Lib_BDraw_JQuadf(j); return float4((2 * p.x - 1) * rx, (1 - 2 * p.y) * ry, 0, 0); }
  public float4 VGrid_Lib_BDraw_Sphere_quadPoint(float r, uint j) => r * float4(2 * VGrid_Lib_BDraw_JQuadf(j) - 1, 0, 0);
  public float2 VGrid_Lib_BDraw_Line_uv(float3 p0, float3 p1, float r, uint j) { float2 p = VGrid_Lib_BDraw_JQuadf(j); return float2(length(p1 - p0) * (1 - p.y), (1 - 2 * p.x) * r); }
  public float2 VGrid_Lib_BDraw_LineArrow_uv(float dpf, float3 p0, float3 p1, float r, uint j) { float2 p = VGrid_Lib_BDraw_JQuadf(j); return float2((length(p1 - p0) + 2 * r) * (1 - p.y) - r, (1 - 2 * p.x) * r * dpf); }
  public float4 VGrid_Lib_BDraw_LineArrow_p4(float dpf, float3 p0, float3 p1, float3 p3, float r, uint j) { float2 p = VGrid_Lib_BDraw_JQuadf(j); float3 dp = normalize(cross(p1 - p0, p3 - p0)) * r * dpf; return float4(p.y * (p0 - p1) + p1 + dp * (1 - 2 * p.x), 1); }
  public float4 VGrid_Lib_BDraw_LineArrow_p4(float dpf, float3 p0, float3 p1, float r, uint j) => VGrid_Lib_BDraw_LineArrow_p4(dpf, p0, p1, _WorldSpaceCameraPos, r, j);
  public uint VGrid_Lib_BDraw_o_i(v2f o) => roundu(o.ti.x);
  public v2f VGrid_Lib_BDraw_o_i(uint i, v2f o) { o.ti.x = i; return o; }
  public uint VGrid_Lib_BDraw_o_drawType(v2f o) => roundu(o.ti.z);
  public v2f VGrid_Lib_BDraw_o_drawType(uint drawType, v2f o) { o.ti.z = drawType; return o; }
  public float4 VGrid_Lib_BDraw_o_color(v2f o) => o.color;
  public v2f VGrid_Lib_BDraw_o_color(float4 color, v2f o) { o.color = color; return o; }
  public float3 VGrid_Lib_BDraw_o_normal(v2f o) => o.normal;
  public v2f VGrid_Lib_BDraw_o_normal(float3 normal, v2f o) { o.normal = normal; return o; }
  public float2 VGrid_Lib_BDraw_o_uv(v2f o) => o.uv;
  public v2f VGrid_Lib_BDraw_o_uv(float2 uv, v2f o) { o.uv = uv; return o; }
  public float4 VGrid_Lib_BDraw_o_pos(v2f o) => o.pos;
  public v2f VGrid_Lib_BDraw_o_pos(float4 pos, v2f o) { o.pos = pos; return o; }
  public v2f VGrid_Lib_BDraw_o_pos_PV(float3 p, float4 q, v2f o) => VGrid_Lib_BDraw_o_pos(mul(UNITY_MATRIX_P, mul(UNITY_MATRIX_V, float4(p, 1)) + q), o);
  public v2f VGrid_Lib_BDraw_o_pos_c(float4 c, v2f o) => VGrid_Lib_BDraw_o_pos(UnityObjectToClipPos(c), o);
  public v2f VGrid_Lib_BDraw_o_pos_c(float3 c, v2f o) => VGrid_Lib_BDraw_o_pos(UnityObjectToClipPos(c), o);
  public float3 VGrid_Lib_BDraw_o_wPos(v2f o) => o.wPos;
  public v2f VGrid_Lib_BDraw_o_wPos(float3 wPos, v2f o) { o.wPos = wPos; return o; }
  public float3 VGrid_Lib_BDraw_o_p0(v2f o) => o.p0;
  public v2f VGrid_Lib_BDraw_o_p0(float3 p0, v2f o) { o.p0 = p0; return o; }
  public float3 VGrid_Lib_BDraw_o_p1(v2f o) => o.p1;
  public v2f VGrid_Lib_BDraw_o_p1(float3 p1, v2f o) { o.p1 = p1; return o; }
  public float VGrid_Lib_BDraw_o_r(v2f o) => o.ti.w;
  public v2f VGrid_Lib_BDraw_o_r(float r, v2f o) { o.ti.w = r; return o; }
  public float3 VGrid_Lib_BDraw_quad(float3 p0, float3 p1, float3 p2, float3 p3, uint j) => j % 5 == 0 ? p3 : j == 1 ? p2 : j == 4 ? p0 : p1;
  public float4 VGrid_Lib_BDraw_o_tj(v2f o) => o.tj;
  public v2f VGrid_Lib_BDraw_o_tj(float4 tj, v2f o) { o.tj = tj; return o; }
  public v2f vert_VGrid_Lib_BDraw_Point(float3 p, float4 color, uint i, v2f o) => VGrid_Lib_BDraw_o_i(i, VGrid_Lib_BDraw_o_drawType(VGrid_Lib_BDraw_Draw_Point, VGrid_Lib_BDraw_o_color(color, VGrid_Lib_BDraw_o_pos(UnityObjectToClipPos(float4(p, 1)), o))));
  public v2f vert_VGrid_Lib_BDraw_Sphere(float3 p, float r, float4 color, uint i, uint j, v2f o) { float4 q = VGrid_Lib_BDraw_Sphere_quadPoint(r, j); return VGrid_Lib_BDraw_o_i(i, VGrid_Lib_BDraw_o_drawType(VGrid_Lib_BDraw_Draw_Sphere, VGrid_Lib_BDraw_o_color(color, VGrid_Lib_BDraw_o_normal(-f001, VGrid_Lib_BDraw_o_uv(q.xy / r, VGrid_Lib_BDraw_o_pos_PV(p, q, VGrid_Lib_BDraw_o_wPos(p, o))))))); }
  public v2f vert_VGrid_Lib_BDraw_LineArrow(float dpf, float3 p0, float3 p1, float r, float4 color, uint i, uint j, v2f o) => VGrid_Lib_BDraw_o_i(i, VGrid_Lib_BDraw_o_p0(p0, VGrid_Lib_BDraw_o_p1(p1, VGrid_Lib_BDraw_o_r(r, VGrid_Lib_BDraw_o_drawType(dpf == 1 ? VGrid_Lib_BDraw_Draw_Line : VGrid_Lib_BDraw_Draw_Arrow, VGrid_Lib_BDraw_o_color(color, VGrid_Lib_BDraw_o_uv(VGrid_Lib_BDraw_LineArrow_uv(dpf, p0, p1, r, j), VGrid_Lib_BDraw_o_pos_c(VGrid_Lib_BDraw_LineArrow_p4(dpf, p0, p1, r, j), o))))))));
  public v2f vert_VGrid_Lib_BDraw_Line(float3 p0, float3 p1, float r, float4 color, uint i, uint j, v2f o) => VGrid_Lib_BDraw_o_i(i, VGrid_Lib_BDraw_o_p0(p0, VGrid_Lib_BDraw_o_p1(p1, VGrid_Lib_BDraw_o_r(r, VGrid_Lib_BDraw_o_drawType(VGrid_Lib_BDraw_Draw_Line, VGrid_Lib_BDraw_o_color(color, VGrid_Lib_BDraw_o_uv(VGrid_Lib_BDraw_Line_uv(p0, p1, r, j), VGrid_Lib_BDraw_o_pos_c(VGrid_Lib_BDraw_LineArrow_p4(1, p0, p1, r, j), o))))))));
  public v2f vert_VGrid_Lib_BDraw_LineSegment(float3 p0, float3 p1, float r, float4 color, uint i, uint j, v2f o) => VGrid_Lib_BDraw_o_drawType(VGrid_Lib_BDraw_Draw_LineSegment, vert_VGrid_Lib_BDraw_LineArrow(1, p0, p1, r, color, i, j, o));
  public v2f vert_VGrid_Lib_BDraw_Arrow(float3 p0, float3 p1, float r, float4 color, uint i, uint j, v2f o) => vert_VGrid_Lib_BDraw_LineArrow(3, p0, p1, r, color, i, j, o);
  public v2f vert_VGrid_Lib_BDraw_BoxFrame(float3 c0, float3 c1, float lineRadius, float4 color, uint i, uint j, v2f o) { float3 p0, p1; switch (i) { case 0: p0 = c0; p1 = c0 * f110 + c1 * f001; break; case 1: p0 = c0 * f110 + c1 * f001; p1 = c0 * f100 + c1 * f011; break; case 2: p0 = c0 * f100 + c1 * f011; p1 = c0 * f101 + c1 * f010; break; case 3: p0 = c0 * f101 + c1 * f010; p1 = c0; break; case 4: p0 = c0 * f011 + c1 * f100; p1 = c0 * f010 + c1 * f101; break; case 5: p0 = c0 * f010 + c1 * f101; p1 = c1; break; case 6: p0 = c1; p1 = c0 * f001 + c1 * f110; break; case 7: p0 = c0 * f001 + c1 * f110; p1 = c0 * f011 + c1 * f100; break; case 8: p0 = c0; p1 = c0 * f011 + c1 * f100; break; case 9: p0 = c0 * f101 + c1 * f010; p1 = c0 * f001 + c1 * f110; break; case 10: p0 = c0 * f100 + c1 * f011; p1 = c1; break; default: p0 = c0 * f110 + c1 * f001; p1 = c0 * f010 + c1 * f101; break; } return vert_VGrid_Lib_BDraw_Line(p0, p1, lineRadius, color, i, j, o); }
  public v2f vert_VGrid_Lib_BDraw_Quad(float3 p0, float3 p1, float3 p2, float3 p3, float4 color, uint i, uint j, v2f o) { float3 p = VGrid_Lib_BDraw_quad(p0, p1, p2, p3, j); return VGrid_Lib_BDraw_o_i(i, VGrid_Lib_BDraw_o_drawType(VGrid_Lib_BDraw_Draw_Texture_2D, VGrid_Lib_BDraw_o_normal(cross(p1 - p0, p0 - p3), VGrid_Lib_BDraw_o_uv(float2(VGrid_Lib_BDraw_wrapJ(j, 2), VGrid_Lib_BDraw_wrapJ(j, 4)), VGrid_Lib_BDraw_o_wPos(p, VGrid_Lib_BDraw_o_pos_c(p, VGrid_Lib_BDraw_o_color(color, o))))))); }
  public v2f vert_VGrid_Lib_BDraw_Legend(uint i, uint j, v2f o) { float h = 8; float3 c = f110 * 10000, p0 = c + float3(0.4f, -h / 2, 0), p1 = p0 + f100 * 0.4f, p2 = p1 + h * f010, p3 = p0 + h * f010; return vert_VGrid_Lib_BDraw_Quad(p0, p1, p2, p3, WHITE, i, j, o); }
  public v2f vert_VGrid_Lib_BDraw_WebCam(float3 p0, float3 p1, float3 p2, float3 p3, float4 color, uint i, uint j, v2f o) => VGrid_Lib_BDraw_o_drawType(VGrid_Lib_BDraw_Draw_WebCam, vert_VGrid_Lib_BDraw_Quad(p0, p1, p2, p3, color, i, j, o));
  public v2f vert_VGrid_Lib_BDraw_Cube(float3 p, float3 r, float4 color, uint i, uint j, v2f o) { float3 p0, p1, p2, p3; switch (i % 6) { case 0: p0 = f___; p1 = f1__; p2 = f11_; p3 = f_1_; break; case 1: p0 = f1_1; p1 = f__1; p2 = f_11; p3 = f111; break; case 2: p0 = f__1; p1 = f1_1; p2 = f1__; p3 = f___; break; case 3: p0 = f_1_; p1 = f11_; p2 = f111; p3 = f_11; break; case 4: p0 = f__1; p1 = f___; p2 = f_1_; p3 = f_11; break; default: p0 = f1__; p1 = f1_1; p2 = f111; p3 = f11_; break; } return vert_VGrid_Lib_BDraw_Quad(p0 * r + p, p1 * r + p, p2 * r + p, p3 * r + p, color, i, j, o); }
  public v2f vert_VGrid_Lib_BDraw_Cube(float3 p, float r, float4 color, uint i, uint j, v2f o) => vert_VGrid_Lib_BDraw_Cube(p, f111 * r, color, i, j, o);
  public v2f vert_VGrid_Lib_BDraw_Signal(float3 p0, float3 p1, float r, uint i, uint j, v2f o) => VGrid_Lib_BDraw_o_i(i, VGrid_Lib_BDraw_o_p0(p0, VGrid_Lib_BDraw_o_p1(p1, VGrid_Lib_BDraw_o_uv(f11 - VGrid_Lib_BDraw_JQuadf(j).yx, VGrid_Lib_BDraw_o_drawType(VGrid_Lib_BDraw_Draw_Signal, VGrid_Lib_BDraw_o_r(r, VGrid_Lib_BDraw_o_pos_c(VGrid_Lib_BDraw_LineArrow_p4(1, p0, p1, r, j), o)))))));
  public v2f vert_VGrid_Lib_BDraw_Signal(float3 p0, float3 p1, float r, float4 color, int drawType, float thickness, uint i, uint j, v2f o) => VGrid_Lib_BDraw_o_tj(float4(distance(p0, p1), r, drawType, thickness), VGrid_Lib_BDraw_o_color(color, vert_VGrid_Lib_BDraw_Signal(p0, p1, r, i, j, o)));
  public virtual uint VGrid_Lib_BDraw_SignalSmpN(uint chI) => 1024;
  public virtual float4 VGrid_Lib_BDraw_SignalColor(uint chI, uint smpI) => YELLOW;
  public virtual float4 VGrid_Lib_BDraw_SignalBackColor(uint chI, uint smpI) => float4(1, 1, 1, 0.2f);
  public virtual float VGrid_Lib_BDraw_SignalSmpV(uint chI, uint smpI) => 0;
  public virtual float VGrid_Lib_BDraw_SignalThickness(uint chI, uint smpI) => 0.004f;
  public virtual float VGrid_Lib_BDraw_SignalFillCrest(uint chI, uint smpI) => 1;
  public virtual bool VGrid_Lib_BDraw_SignalMarkerColor(uint stationI, float station_smpI, float4 color, uint chI, float smpI, uint display_x, out float4 return_color)
  {
    float d = abs(smpI - station_smpI + display_x);
    return (return_color = chI == stationI && d < 1 ? float4(color.xyz * (1 - d), 1) : f0000).w > 0;
  }
  public virtual float4 VGrid_Lib_BDraw_SignalMarker(uint chI, float smpI) => f0000;
  public virtual float4 frag_VGrid_Lib_BDraw_Signal(v2f i)
  {
    uint chI = VGrid_Lib_BDraw_o_i(i), SmpN = VGrid_Lib_BDraw_SignalSmpN(chI);
    float2 uv = i.uv, wh = float2(distance(i.p1, i.p0), VGrid_Lib_BDraw_o_r(i));
    float smpI = lerp(0, SmpN, uv.x), y = lerp(-1, 1, uv.y), h = wh.y / wh.x * SmpN, thick = VGrid_Lib_BDraw_SignalThickness(chI, (uint)smpI) * SmpN, d = float_PositiveInfinity;
    uint SmpI = (uint)smpI, dSmpI = ceilu(thick) + 1, SmpI0 = (uint)max(0, (int)SmpI - (int)dSmpI), SmpI1 = min(SmpN - 1, SmpI + dSmpI);
    float2 p0 = float2(smpI, y * h), q0 = float2(SmpI0, (h - thick) * VGrid_Lib_BDraw_SignalSmpV(chI, SmpI0)), q1;
    for (uint sI = SmpI0; sI < SmpI1; sI++) { q1 = float2(sI + 1, (h - thick) * VGrid_Lib_BDraw_SignalSmpV(chI, sI + 1)); d = min(d, LineSegDist(q0, q1, p0)); q0 = q1; }
    float4 c = VGrid_Lib_BDraw_SignalColor(chI, SmpI);
    float v = 0.9f * lerp(VGrid_Lib_BDraw_SignalSmpV(chI, SmpI), VGrid_Lib_BDraw_SignalSmpV(chI, SmpI + 1), frac(smpI)), crest = VGrid_Lib_BDraw_SignalFillCrest(chI, SmpI);
    float4 marker = VGrid_Lib_BDraw_SignalMarker(chI, smpI);
    if (marker.w > 0) return marker;
    if (crest >= 0 ? y > crest && y < v : y < crest && y > v) return c;
    if (d < thick) return float4(c.xyz * (1 - d / thick), 1);
    return VGrid_Lib_BDraw_SignalBackColor(chI, SmpI);
  }
  public float4 frag_VGrid_Lib_BDraw_Sphere(v2f i) { float2 uv = i.uv; float r = dot(uv, uv); float4 color = i.color; if (r > 1.0f || color.a == 0) return f0000; float3 n = new float3(uv, r - 1), _LightDir = new float3(0.321f, 0.766f, -0.557f); float lightAmp = max(0.0f, dot(n, _LightDir)); float4 diffuse_Light = (lightAmp + UNITY_LIGHTMODEL_AMBIENT) * color; float spec = max(0, (lightAmp - 0.95f) / 0.05f); color = lerp(diffuse_Light, f1111, spec / 4); color.a = 1; return color; }
  public float4 frag_VGrid_Lib_BDraw_Line(v2f i) { float3 p0 = i.p0, p1 = i.p1; float lineRadius = VGrid_Lib_BDraw_o_r(i); float2 uv = i.uv; float r = dot(uv, uv), r2 = lineRadius * lineRadius; float4 color = i.color; float3 p10 = p1 - p0; float lp10 = length(p10); if (uv.x < 0) r /= r2; else if (uv.x > lp10) { uv.x -= lp10; r = dot(uv, uv) / r2; } else { uv.x = 0; r = dot(uv, uv) / r2; } if (r > 1.0f || color.a == 0) return f0000; float3 n = new float3(uv, r - 1), _LightDir = new float3(0.321f, 0.766f, -0.557f); float lightAmp = max(0.0f, dot(n, _LightDir)); float4 diffuse_Light = (lightAmp + UNITY_LIGHTMODEL_AMBIENT) * color; float spec = max(0, (lightAmp - 0.95f) / 0.05f); color = lerp(diffuse_Light, f1111, spec / 4); color.a = 1; return color; }
  public float4 frag_VGrid_Lib_BDraw_Arrow(v2f i) { float3 p0 = i.p0, p1 = i.p1; float lineRadius = VGrid_Lib_BDraw_o_r(i); float2 uv = i.uv; float r = dot(uv, uv), r2 = lineRadius * lineRadius; float4 color = i.color; float3 p10 = p1 - p0; float lp10 = length(p10); if (uv.x < 0) r /= r2; else if (uv.x > lp10 - lineRadius * 3 && abs(uv.y) > lineRadius) { uv.x -= lp10; uv = rotate_sc(uv, -sign(uv.y) * 0.5f, 0.866025404f); uv.x = 0; r = dot(uv, uv) / r2; } else if (uv.x > lp10) { uv.x -= lp10; r = dot(uv, uv) / r2; } else { uv.x = 0; r = dot(uv, uv) / r2; } if (r > 1.0f || color.a == 0) return f0000; float3 n = new float3(uv, r - 1), _LightDir = new float3(0.321f, 0.766f, -0.557f); float lightAmp = max(0.0f, dot(n, _LightDir)); float4 diffuse_Light = (lightAmp + UNITY_LIGHTMODEL_AMBIENT) * color; float spec = max(0, (lightAmp - 0.95f) / 0.05f); color = lerp(diffuse_Light, f1111, spec / 4); color.a = 1; return color; }
  public float4 frag_VGrid_Lib_BDraw_LineSegment(v2f i) { float3 p0 = i.p0, p1 = i.p1; float lineRadius = VGrid_Lib_BDraw_o_r(i); float2 uv = i.uv; float r = dot(uv, uv), r2 = lineRadius * lineRadius; float4 color = i.color; float3 p10 = p1 - p0; float lp10 = length(p10); uv.x = 0; r = dot(uv, uv) / r2; if (r > 1.0f || color.a == 0) return f0000; float3 n = new float3(uv, r - 1), _LightDir = new float3(0.321f, 0.766f, -0.557f); float lightAmp = max(0.0f, dot(n, _LightDir)); float4 diffuse_Light = (lightAmp + UNITY_LIGHTMODEL_AMBIENT) * color; float spec = max(0, (lightAmp - 0.95f) / 0.05f); color = lerp(diffuse_Light, f1111, spec / 4); color.a = 1; return color; }
  public float4 frag_VGrid_Lib_BDraw_Quad(Texture2D t, v2f i) => i.color * tex2Dlod(t, new float4(i.uv, f00));
  public float4 frag_VGrid_Lib_BDraw_Quad(RWStructuredBuffer<Color> v, v2f i, uint2 _image_size) => (float4)(i.color * v[id_to_i((uint2)(i.uv * (float2)_image_size), _image_size)]);
  public float4 frag_VGrid_Lib_BDraw_Quad(RWStructuredBuffer<Color32> v, v2f i, uint2 _image_size) => (float4)(i.color * c32_f4(v[id_to_i((uint2)(i.uv * (float2)_image_size), _image_size)]));
  public float4 frag_VGrid_Lib_BDraw_Mesh(v2f i) { float3 p = i.wPos; float4 color = i.color; color.xyz += dot(i.normal, _WorldSpaceLightPos0.xyz) / 2; return saturate(color); }
  public virtual bool VGrid_Lib_BDraw_AppendBuff_IsBitOn(uint i) { uint c = VGrid_Lib_BDraw_Byte(i); return c == VGrid_Lib_BDraw_TB || c == VGrid_Lib_BDraw_LF; }
  public class VGrid_Lib_BDraw_TText3D
  {
    public string text; public float3 p, right, up, p0, p1; public float h; public float4 color, backColor;
    public VGrid_Lib_BDraw_Text_QuadType quadType; public VGrid_Lib_BDraw_TextAlignment textAlignment; public uint axis;
  }
  public Font VGrid_Lib_BDraw_font { get; set; }
  public virtual void VGrid_Lib_BDraw_InitBuffers0_GS()
  {
    if (VGrid_Lib_BDraw_omitText) VGrid_Lib_BDraw_fontInfoN = 0;
    else { VGrid_Lib_BDraw_font ??= Resources.Load<Font>("Arial Font/arial Unicode"); VGrid_Lib_BDraw_fontTexture = (Texture2D)VGrid_Lib_BDraw_font.material.mainTexture; VGrid_Lib_BDraw_fontInfoN = VGrid_Lib_BDraw_includeUnicode ? VGrid_Lib_BDraw_font.characterInfo.uLength() : 128 - 32; }
  }
  public virtual void VGrid_Lib_BDraw_InitBuffers1_GS()
  {
    for (int i = 0; i < VGrid_Lib_BDraw_fontInfoN; i++)
    {
      var c = VGrid_Lib_BDraw_font.characterInfo[i];
      if (i == 0) VGrid_Lib_BDraw_fontSize = c.size;
      if (c.index < 128) VGrid_Lib_BDraw_fontInfos[c.index - 32] = new VGrid_Lib_BDraw_FontInfo() { uvBottomLeft = c.uvBottomLeft, uvBottomRight = c.uvBottomRight, uvTopLeft = c.uvTopLeft, uvTopRight = c.uvTopRight, advance = max(c.advance, roundi(c.glyphWidth * 1.05f)), bearing = c.bearing, minX = c.minX, minY = c.minY, maxX = c.maxX, maxY = c.maxY };
    }
    VGrid_Lib_BDraw_fontInfos.SetData();
  }
  public float VGrid_Lib_BDraw_GetTextHeight() => 0.1f;
  public uint VGrid_Lib_BDraw_GetText_ch(float v, uint _I, uint neg, uint uN) => _I < neg ? 13u : _I < uN + neg ? 16u + flooru(10 * frac(abs(v) / pow10(uN - _I + neg))) : _I == uN + neg ? 14u : 16u + flooru(10 * frac(abs(v) * pow10(_I - neg - uN - 1)));
  public uint VGrid_Lib_BDraw_Byte(uint i) => TextByte(VGrid_Lib_BDraw_tab_delimeted_text, i);
  public uint2 VGrid_Lib_BDraw_Get_text_indexes(uint textI) => uint2(textI == 0 ? 0 : VGrid_Lib_BDraw_AppendBuff_Indexes[textI - 1] + 1, textI < VGrid_Lib_BDraw_AppendBuff_IndexN ? VGrid_Lib_BDraw_AppendBuff_Indexes[textI] : VGrid_Lib_BDraw_textCharN);
  public float VGrid_Lib_BDraw_GetTextWidth(float v, uint decimalN)
  {
    float textWidth = 0, p10 = pow10(decimalN);
    v = round(v * p10) / p10;
    uint u = flooru(abs(v)), uN = u == 0 ? 1 : flooru(log10(abs(v)) + 1), numDigits = uN + decimalN + (decimalN == 0 ? 0 : 1u), neg = v < 0 ? 1u : 0;
    for (uint _I = 0; _I < numDigits + neg; _I++)
    {
      uint ch = VGrid_Lib_BDraw_GetText_ch(v, _I, neg, uN);
      VGrid_Lib_BDraw_FontInfo f = VGrid_Lib_BDraw_fontInfos[ch];
      float2 mn = new float2(f.minX, f.minY) / VGrid_Lib_BDraw_fontSize, mx = new float2(f.maxX, f.maxY) / VGrid_Lib_BDraw_fontSize, range = mx - mn;
      float dx = f.advance / VGrid_Lib_BDraw_fontSize;
      textWidth += dx;
    }
    return textWidth;
  }
  public float3 VGrid_Lib_BDraw_GetTextWidth(float3 v, uint3 decimalN) => new float3(VGrid_Lib_BDraw_GetTextWidth(v.x, decimalN.x), VGrid_Lib_BDraw_GetTextWidth(v.y, decimalN.y), VGrid_Lib_BDraw_GetTextWidth(v.z, decimalN.z));
  public List<VGrid_Lib_BDraw_TText3D> VGrid_Lib_BDraw_texts = new List<VGrid_Lib_BDraw_TText3D>();
  public void VGrid_Lib_BDraw_ClearTexts() => VGrid_Lib_BDraw_texts.Clear();
  public virtual void VGrid_Lib_BDraw_AddText(string text, float3 p, float3 right, float3 up, float4 color, float4 backColor, float h, VGrid_Lib_BDraw_Text_QuadType quadType, VGrid_Lib_BDraw_TextAlignment textAlignment, float3 p0, float3 p1, uint axis = 0) => VGrid_Lib_BDraw_texts.Add(new VGrid_Lib_BDraw_TText3D() { text = text, p = p, right = right, up = up, color = color, backColor = backColor, h = h, quadType = quadType, textAlignment = textAlignment, p0 = p0, p1 = p1, axis = axis });
  public void VGrid_Lib_BDraw_AddText(string text, float3 p, float3 right, float3 up, float4 color, float4 backColor, float h, VGrid_Lib_BDraw_Text_QuadType quadType, VGrid_Lib_BDraw_TextAlignment textAlignment) => VGrid_Lib_BDraw_AddText(text, p, right, up, color, backColor, h, quadType, textAlignment, f000, f000, 0);
  public virtual VGrid_Lib_BDraw_TextInfo VGrid_Lib_BDraw_textInfo(uint i) => VGrid_Lib_BDraw_textInfos[i];
  public virtual void VGrid_Lib_BDraw_textInfo(uint i, VGrid_Lib_BDraw_TextInfo t) => VGrid_Lib_BDraw_textInfos[i] = t;
  public int VGrid_Lib_BDraw_ExtraTextN = 0;
  public virtual void VGrid_Lib_BDraw_RebuildExtraTexts() { VGrid_Lib_BDraw_BuildTexts(); VGrid_Lib_BDraw_BuildTexts(); }
  public virtual void VGrid_Lib_BDraw_BuildExtraTexts() { }
  public virtual void VGrid_Lib_BDraw_BuildTexts()
  {
    SetBytes(ref VGrid_Lib_BDraw_tab_delimeted_text, (VGrid_Lib_BDraw_texts.Select(a => a.text).Join("\n") + "\n").ToBytes(Encoding.UTF8));
    AddComputeBuffer(ref VGrid_Lib_BDraw_textInfos, nameof(VGrid_Lib_BDraw_textInfo), VGrid_Lib_BDraw_textN = max(1, VGrid_Lib_BDraw_AppendBuff_Run(VGrid_Lib_BDraw_textCharN = VGrid_Lib_BDraw_tab_delimeted_text.uLength * 4)));
    for (uint i = 0; i < VGrid_Lib_BDraw_texts.Count; i++)
    {
      var t = VGrid_Lib_BDraw_texts[(int)i];
      var ti = VGrid_Lib_BDraw_textInfo(i);
      ti.color = t.color; ti.backColor = t.backColor; ti.p = t.p; ti.p0 = t.p0; ti.p1 = t.p1; ti.height = t.h;
      ti.quadType = (uint)t.quadType; ti.axis = t.axis; ti.right = t.right; ti.up = t.up; ti.justification = (uint)t.textAlignment;
      VGrid_Lib_BDraw_textInfo(i, ti);
    }
    if (VGrid_Lib_BDraw_AppendBuff_Indexes == null || VGrid_Lib_BDraw_AppendBuff_Indexes.computeBuffer == null) { AddComputeBuffer(ref VGrid_Lib_BDraw_AppendBuff_Indexes, nameof(VGrid_Lib_BDraw_AppendBuff_Indexes), 1); VGrid_Lib_BDraw_AppendBuff_Indexes.SetData(new uint[] { 0 }); }
    if (VGrid_Lib_BDraw_fontInfos != null && VGrid_Lib_BDraw_AppendBuff_Indexes != null) { computeShader.SetBuffer(kernel_VGrid_Lib_BDraw_getTextInfo, nameof(VGrid_Lib_BDraw_textInfos), VGrid_Lib_BDraw_textInfos); Gpu_VGrid_Lib_BDraw_getTextInfo(); }
    if (VGrid_Lib_BDraw_ExtraTextN > 0 && VGrid_Lib_BDraw_texts.Count >= VGrid_Lib_BDraw_ExtraTextN) VGrid_Lib_BDraw_texts.RemoveRange(VGrid_Lib_BDraw_texts.Count - VGrid_Lib_BDraw_ExtraTextN, VGrid_Lib_BDraw_ExtraTextN);
    int n = VGrid_Lib_BDraw_texts.Count;
    VGrid_Lib_BDraw_BuildExtraTexts();
    VGrid_Lib_BDraw_ExtraTextN = VGrid_Lib_BDraw_texts.Count - n;
  }
  public virtual IEnumerator VGrid_Lib_BDraw_BuildTexts_Coroutine()
  {
    SetBytes(ref VGrid_Lib_BDraw_tab_delimeted_text, (VGrid_Lib_BDraw_texts.Select(a => a.text).Join("\n") + "\n").ToBytes(Encoding.UTF8));
    AddComputeBuffer(ref VGrid_Lib_BDraw_textInfos, nameof(VGrid_Lib_BDraw_textInfo), VGrid_Lib_BDraw_textN = max(1, VGrid_Lib_BDraw_AppendBuff_Run(VGrid_Lib_BDraw_textCharN = VGrid_Lib_BDraw_tab_delimeted_text.uLength * 4)));
    for (uint i = 0; i < VGrid_Lib_BDraw_texts.Count; i++)
    {
      var t = VGrid_Lib_BDraw_texts[(int)i];
      var ti = VGrid_Lib_BDraw_textInfo(i);
      ti.color = t.color; ti.backColor = t.backColor; ti.p = t.p; ti.p0 = t.p0; ti.p1 = t.p1; ti.height = t.h;
      ti.quadType = (uint)t.quadType; ti.axis = t.axis; ti.right = t.right; ti.up = t.up; ti.justification = (uint)t.textAlignment;
      VGrid_Lib_BDraw_textInfo(i, ti);
      if (i % 1000 == 0) { progress(i, (uint)VGrid_Lib_BDraw_texts.Count); yield return null; }
    }
    progress(0);
    if (VGrid_Lib_BDraw_AppendBuff_Indexes == null || VGrid_Lib_BDraw_AppendBuff_Indexes.computeBuffer == null) { AddComputeBuffer(ref VGrid_Lib_BDraw_AppendBuff_Indexes, nameof(VGrid_Lib_BDraw_AppendBuff_Indexes), 1); VGrid_Lib_BDraw_AppendBuff_Indexes.SetData(new uint[] { 0 }); }
    if (VGrid_Lib_BDraw_fontInfos != null && VGrid_Lib_BDraw_AppendBuff_Indexes != null) { computeShader.SetBuffer(kernel_VGrid_Lib_BDraw_getTextInfo, nameof(VGrid_Lib_BDraw_textInfos), VGrid_Lib_BDraw_textInfos); Gpu_VGrid_Lib_BDraw_getTextInfo(); }
  }
  public virtual void VGrid_Lib_BDraw_BuildTexts_Default()
  {
    SetBytes(ref VGrid_Lib_BDraw_tab_delimeted_text, (VGrid_Lib_BDraw_texts.Select(a => a.text).Join("\n") + "\n").ToBytes(Encoding.UTF8));
    AddComputeBuffer(ref VGrid_Lib_BDraw_textInfos, nameof(VGrid_Lib_BDraw_textInfo), VGrid_Lib_BDraw_textN = max(1, VGrid_Lib_BDraw_AppendBuff_Run(VGrid_Lib_BDraw_textCharN = VGrid_Lib_BDraw_tab_delimeted_text.uLength * 4)));
    if (VGrid_Lib_BDraw_texts.Count > 0)
    {
      var t = VGrid_Lib_BDraw_texts[0];
      var ti = VGrid_Lib_BDraw_textInfo(0);
      ti.color = t.color; ti.backColor = t.backColor; ti.p = t.p; ti.p0 = t.p0; ti.p1 = t.p1; ti.height = t.h;
      ti.quadType = (uint)t.quadType; ti.axis = t.axis; ti.right = t.right; ti.up = t.up; ti.justification = (uint)t.textAlignment;
      VGrid_Lib_BDraw_textInfo(0, ti);
      Gpu_VGrid_Lib_BDraw_setDefaultTextInfo();
    }
    if (VGrid_Lib_BDraw_AppendBuff_Indexes == null || VGrid_Lib_BDraw_AppendBuff_Indexes.computeBuffer == null) { AddComputeBuffer(ref VGrid_Lib_BDraw_AppendBuff_Indexes, nameof(VGrid_Lib_BDraw_AppendBuff_Indexes), 1); VGrid_Lib_BDraw_AppendBuff_Indexes.SetData(new uint[] { 0 }); }
    if (VGrid_Lib_BDraw_fontInfos != null && VGrid_Lib_BDraw_AppendBuff_Indexes != null) Gpu_VGrid_Lib_BDraw_getTextInfo();
  }
  public void VGrid_Lib_BDraw_AddXAxis(string title, float titleHeight, float3 p0, float3 p1, float3 tRight, float3 tUp, float4 color, float2 vRange, string format, float textHeight, float3 right, float3 up, float3 margin, VGrid_Lib_BDraw_Text_QuadType quadType, float3 mn, float3 mx, uint axis = 0)
  {
    if (extent(vRange) <= 0) return;
    uint decimalN = VGrid_Lib_BDraw_GetDecimalN(vRange) + (uint)format.Length, maxTickN = VGrid_Lib_BDraw_GetXAxisN(textHeight, decimalN, p1.xy - p0.xy);
    float range = NiceNum(abs(extent(vRange)), false), dxi = NiceNum(range / (maxTickN - 1), true), xi0 = ceil(vRange.x / dxi) * dxi, xi1 = floor(vRange.y / dxi) * dxi, x0, x1;
    if (vRange.x < vRange.y) { x0 = xi0; x1 = xi1 + dxi / 2; } else { x0 = xi1; x1 = xi0 - dxi / 2; }
    for (float xi = x0; xi < x1; xi += dxi) VGrid_Lib_BDraw_AddText(xi.ToString(format), float3(lerp(p0.x, p1.x, (xi - vRange.x) / extent(vRange)), p0.y, p0.z) + textHeight / 2 * margin, right, up, color, EMPTY, textHeight, quadType, margin.y > 0 ? VGrid_Lib_BDraw_TextAlignment.BottomCenter : VGrid_Lib_BDraw_TextAlignment.TopCenter, mn, mx, axis);
    VGrid_Lib_BDraw_AddText(title, (p0 + p1) / 2 + titleHeight * 2 * margin, tRight, tUp, color, EMPTY, titleHeight, quadType, margin.y > 0 ? VGrid_Lib_BDraw_TextAlignment.BottomCenter : VGrid_Lib_BDraw_TextAlignment.TopCenter, mn, mx, axis);
  }
  public void VGrid_Lib_BDraw_AddYAxis(string title, float titleHeight, float3 p0, float3 p1, float3 tRight, float3 tUp, float4 color, float2 vRange, string format, float textHeight, float3 right, float3 up, float3 margin, VGrid_Lib_BDraw_Text_QuadType quadType, float3 mn, float3 mx, uint axis = 0)
  {
    if (extent(vRange) <= 0) return;
    uint decimalN = VGrid_Lib_BDraw_GetDecimalN(vRange) + (uint)format.Length, maxTickN = VGrid_Lib_BDraw_GetXAxisN(textHeight, decimalN, p1.xy - p0.xy);
    float range = NiceNum(abs(extent(vRange)), false), dxi = NiceNum(range / (maxTickN - 1), true), xi0 = ceil(vRange.x / dxi) * dxi, xi1 = floor(vRange.y / dxi) * dxi, x0, x1;
    if (vRange.x < vRange.y) { x0 = xi0; x1 = xi1 + dxi / 2; } else { x0 = xi1; x1 = xi0 - dxi / 2; }
    for (float xi = x0; xi < x1; xi += dxi) VGrid_Lib_BDraw_AddText(xi.ToString(format), float3(p0.x, lerp(p0.y, p1.y, (xi - vRange.x) / extent(vRange)), p0.z) + textHeight / 2 * margin, right, up, color, EMPTY, textHeight, quadType, tUp.x < 0 ? VGrid_Lib_BDraw_TextAlignment.CenterRight : VGrid_Lib_BDraw_TextAlignment.CenterLeft, mn, mx, axis);
    VGrid_Lib_BDraw_AddText(title, (p0 + p1) / 2 + textHeight * (2 + decimalN / 5.0f) * margin, tRight, tUp, color, EMPTY, titleHeight, quadType, VGrid_Lib_BDraw_TextAlignment.BottomCenter, mn, mx, axis);
  }
  public void VGrid_Lib_BDraw_AddZAxis(string title, float titleHeight, float3 p0, float3 p1, float3 tRight, float3 tUp, float4 color, float2 vRange, string format, float textHeight, float3 right, float3 up, float3 margin, VGrid_Lib_BDraw_Text_QuadType quadType, float3 mn, float3 mx, uint axis = 0)
  {
    if (extent(vRange) <= 0) return;
    uint decimalN = VGrid_Lib_BDraw_GetDecimalN(vRange) + (uint)format.Length, maxTickN = VGrid_Lib_BDraw_GetXAxisN(textHeight, decimalN, p1.zy - p0.zy);
    float range = NiceNum(abs(extent(vRange)), false), dxi = NiceNum(range / (maxTickN - 1), true), xi0 = ceil(vRange.x / dxi) * dxi, xi1 = floor(vRange.y / dxi) * dxi, x0, x1;
    if (vRange.x < vRange.y) { x0 = xi0; x1 = xi1 + dxi / 2; } else { x0 = xi1; x1 = xi0 - dxi / 2; }
    for (float xi = x0; xi < x1; xi += dxi) VGrid_Lib_BDraw_AddText(xi.ToString(format), float3(p0.x, p0.y, lerp(p0.z, p1.z, (xi - vRange.x) / extent(vRange))) + textHeight / 2 * margin, right, up, color, EMPTY, textHeight, quadType, margin.y > 0 ? VGrid_Lib_BDraw_TextAlignment.BottomCenter : VGrid_Lib_BDraw_TextAlignment.TopCenter, mn, mx, axis);
    VGrid_Lib_BDraw_AddText(title, (p0 + p1) / 2 + titleHeight * 2 * margin, tRight, tUp, color, EMPTY, titleHeight, quadType, margin.y > 0 ? VGrid_Lib_BDraw_TextAlignment.BottomCenter : VGrid_Lib_BDraw_TextAlignment.TopCenter, mn, mx, axis);
  }
  public void VGrid_Lib_BDraw_AddAxes(float numberHeight, float titleHeight, float3 p0, float3 p1, float3 range0, float3 range1, float4 color,
    string xTitle, string yTitle, string zTitle, string xFormat = "0.00", string yFormat = null, string zFormat = null)
  {
    if (yFormat == null) yFormat = xFormat; if (zFormat == null) zFormat = yFormat;
    VGrid_Lib_BDraw_AddXAxis(xTitle, titleHeight, float3(p0.x, p0.y, p0.z), float3(p1.x, p0.y, p0.z), f100, f011, color, float2(range0.x, range1.x), xFormat, numberHeight, f100, f011, f0__, VGrid_Lib_BDraw_Text_QuadType.Switch, p0, p1, 100);
    VGrid_Lib_BDraw_AddXAxis(xTitle, titleHeight, float3(p0.x, p0.y, p1.z), float3(p1.x, p0.y, p1.z), f100, f01_, color, float2(range0.x, range1.x), xFormat, numberHeight, f100, f01_, f0_1, VGrid_Lib_BDraw_Text_QuadType.Switch, p0, p1, 200);
    VGrid_Lib_BDraw_AddXAxis(xTitle, titleHeight, float3(p0.x, p1.y, p1.z), float3(p1.x, p1.y, p1.z), f100, f011, color, float2(range0.x, range1.x), xFormat, numberHeight, f100, f011, f011, VGrid_Lib_BDraw_Text_QuadType.Switch, p0, p1, 300);
    VGrid_Lib_BDraw_AddXAxis(xTitle, titleHeight, float3(p0.x, p1.y, p0.z), float3(p1.x, p1.y, p0.z), f100, f01_, color, float2(range0.x, range1.x), xFormat, numberHeight, f100, f01_, f01_, VGrid_Lib_BDraw_Text_QuadType.Switch, p0, p1, 400);
    VGrid_Lib_BDraw_AddYAxis(yTitle, titleHeight, float3(p0.x, p0.y, p0.z), float3(p0.x, p1.y, p0.z), f010, f_0_, color, float2(range0.y, range1.y), yFormat, numberHeight, f101, f010, f_0_, VGrid_Lib_BDraw_Text_QuadType.Switch, p0, p1, 10);
    VGrid_Lib_BDraw_AddYAxis(yTitle, titleHeight, float3(p1.x, p0.y, p0.z), float3(p1.x, p1.y, p0.z), f0_0, f10_, color, float2(range0.y, range1.y), yFormat, numberHeight, f10_, f010, f10_, VGrid_Lib_BDraw_Text_QuadType.Switch, p0, p1, 20);
    VGrid_Lib_BDraw_AddYAxis(yTitle, titleHeight, float3(p1.x, p0.y, p1.z), float3(p1.x, p1.y, p1.z), f0_0, f101, color, float2(range0.y, range1.y), yFormat, numberHeight, f101, f010, f101, VGrid_Lib_BDraw_Text_QuadType.Switch, p0, p1, 30);
    VGrid_Lib_BDraw_AddYAxis(yTitle, titleHeight, float3(p0.x, p0.y, p1.z), float3(p0.x, p1.y, p1.z), f0_0, f_01, color, float2(range0.y, range1.y), yFormat, numberHeight, f10_, f010, f_01, VGrid_Lib_BDraw_Text_QuadType.Switch, p0, p1, 40);
    VGrid_Lib_BDraw_AddZAxis(zTitle, titleHeight, float3(p0.x, p0.y, p0.z), float3(p0.x, p0.y, p1.z), f001, f110, color, float2(range0.z, range1.z), zFormat, numberHeight, f001, f110, f__0, VGrid_Lib_BDraw_Text_QuadType.Switch, p0, p1, 1);
    VGrid_Lib_BDraw_AddZAxis(zTitle, titleHeight, float3(p0.x, p1.y, p0.z), float3(p0.x, p1.y, p1.z), f001, f_10, color, float2(range0.z, range1.z), zFormat, numberHeight, f001, f_10, f_10, VGrid_Lib_BDraw_Text_QuadType.Switch, p0, p1, 2);
    VGrid_Lib_BDraw_AddZAxis(zTitle, titleHeight, float3(p1.x, p1.y, p0.z), float3(p1.x, p1.y, p1.z), f001, f110, color, float2(range0.z, range1.z), zFormat, numberHeight, f001, f110, f110, VGrid_Lib_BDraw_Text_QuadType.Switch, p0, p1, 3);
    VGrid_Lib_BDraw_AddZAxis(zTitle, titleHeight, float3(p1.x, p0.y, p0.z), float3(p1.x, p0.y, p1.z), f001, f_10, color, float2(range0.z, range1.z), zFormat, numberHeight, f001, f_10, f1_0, VGrid_Lib_BDraw_Text_QuadType.Switch, p0, p1, 4);
  }
  public string VGrid_Lib_BDraw_str(string[] s, int i) => i < s.Length ? s[i] : i > 2 && i - 3 < s.Length ? s[i - 3] : "";
  public void VGrid_Lib_BDraw_AddAxes(float numberHeight, float titleHeight, float4 color, float3 p0, float3 p1,
    float3 rangeA0, float3 rangeA1, float3 rangeB0, float3 rangeB1, string xyz_titles, string xyz_formats = "0.00")
  {
    var (ts, fs, ttl, fmt) = (xyz_titles.Split("|"), xyz_formats.Split("|"), new string[6], new string[6]);
    for (int i = 0; i < 6; i++) { ttl[i] = VGrid_Lib_BDraw_str(ts, i); fmt[i] = VGrid_Lib_BDraw_str(fs, i); }
    VGrid_Lib_BDraw_AddAxes(numberHeight, titleHeight, color, p0, p1, rangeA0, rangeA1, rangeB0, rangeB1, ttl, fmt);
  }
  public void VGrid_Lib_BDraw_AddAxes(float numberHeight, float titleHeight, float4 color, float3 p0, float3 p1,
    float3 rangeA0, float3 rangeA1, float3 rangeB0, float3 rangeB1, string[] ttl, string[] fmt)
  {
    VGrid_Lib_BDraw_AddXAxis(ttl[0], titleHeight, float3(p0.x, p0.y, p0.z), float3(p1.x, p0.y, p0.z), f100, f011, color, float2(rangeA0.x, rangeA1.x), fmt[0], numberHeight, f100, f011, f0__, VGrid_Lib_BDraw_Text_QuadType.Switch, p0, p1, 100);
    VGrid_Lib_BDraw_AddXAxis(ttl[0], titleHeight, float3(p0.x, p0.y, p1.z), float3(p1.x, p0.y, p1.z), f100, f01_, color, float2(rangeA0.x, rangeA1.x), fmt[0], numberHeight, f100, f01_, f0_1, VGrid_Lib_BDraw_Text_QuadType.Switch, p0, p1, 200);
    VGrid_Lib_BDraw_AddXAxis(ttl[3], titleHeight, float3(p0.x, p1.y, p1.z), float3(p1.x, p1.y, p1.z), f100, f011, color, float2(rangeB0.x, rangeB1.x), fmt[3], numberHeight, f100, f011, f011, VGrid_Lib_BDraw_Text_QuadType.Switch, p0, p1, 300);
    VGrid_Lib_BDraw_AddXAxis(ttl[3], titleHeight, float3(p0.x, p1.y, p0.z), float3(p1.x, p1.y, p0.z), f100, f01_, color, float2(rangeB0.x, rangeB1.x), fmt[3], numberHeight, f100, f01_, f01_, VGrid_Lib_BDraw_Text_QuadType.Switch, p0, p1, 400);
    VGrid_Lib_BDraw_AddYAxis(ttl[1], titleHeight, float3(p0.x, p0.y, p0.z), float3(p0.x, p1.y, p0.z), f010, f_0_, color, float2(rangeA0.y, rangeA1.y), fmt[1], numberHeight, f101, f010, f_0_, VGrid_Lib_BDraw_Text_QuadType.Switch, p0, p1, 10);
    VGrid_Lib_BDraw_AddYAxis(ttl[4], titleHeight, float3(p1.x, p0.y, p0.z), float3(p1.x, p1.y, p0.z), f0_0, f10_, color, float2(rangeB0.y, rangeB1.y), fmt[4], numberHeight, f10_, f010, f10_, VGrid_Lib_BDraw_Text_QuadType.Switch, p0, p1, 20);
    VGrid_Lib_BDraw_AddYAxis(ttl[1], titleHeight, float3(p1.x, p0.y, p1.z), float3(p1.x, p1.y, p1.z), f0_0, f101, color, float2(rangeA0.y, rangeA1.y), fmt[1], numberHeight, f101, f010, f101, VGrid_Lib_BDraw_Text_QuadType.Switch, p0, p1, 30);
    VGrid_Lib_BDraw_AddYAxis(ttl[4], titleHeight, float3(p0.x, p0.y, p1.z), float3(p0.x, p1.y, p1.z), f0_0, f_01, color, float2(rangeB0.y, rangeB1.y), fmt[4], numberHeight, f10_, f010, f_01, VGrid_Lib_BDraw_Text_QuadType.Switch, p0, p1, 40);
    VGrid_Lib_BDraw_AddZAxis(ttl[2], titleHeight, float3(p0.x, p0.y, p0.z), float3(p0.x, p0.y, p1.z), f001, f110, color, float2(rangeA0.z, rangeA1.z), fmt[2], numberHeight, f001, f110, f__0, VGrid_Lib_BDraw_Text_QuadType.Switch, p0, p1, 1);
    VGrid_Lib_BDraw_AddZAxis(ttl[5], titleHeight, float3(p0.x, p1.y, p0.z), float3(p0.x, p1.y, p1.z), f001, f_10, color, float2(rangeB0.z, rangeB1.z), fmt[5], numberHeight, f001, f_10, f_10, VGrid_Lib_BDraw_Text_QuadType.Switch, p0, p1, 2);
    VGrid_Lib_BDraw_AddZAxis(ttl[5], titleHeight, float3(p1.x, p1.y, p0.z), float3(p1.x, p1.y, p1.z), f001, f110, color, float2(rangeB0.z, rangeB1.z), fmt[5], numberHeight, f001, f110, f110, VGrid_Lib_BDraw_Text_QuadType.Switch, p0, p1, 3);
    VGrid_Lib_BDraw_AddZAxis(ttl[2], titleHeight, float3(p1.x, p0.y, p0.z), float3(p1.x, p0.y, p1.z), f001, f_10, color, float2(rangeA0.z, rangeA1.z), fmt[2], numberHeight, f001, f_10, f1_0, VGrid_Lib_BDraw_Text_QuadType.Switch, p0, p1, 4);
  }
  public void VGrid_Lib_BDraw_AddAxes(float numberHeight, float titleHeight, float3 p0, float3 p1, float3 range0, float3 range1, float4 color, string xyz_titles, string xyz_formats = "0.00")
  {
    var _titles = splitStr3(xyz_titles, false);
    var _formats = splitStr3(xyz_formats, true);
    VGrid_Lib_BDraw_AddAxes(numberHeight, titleHeight, p0, p1, range0, range1, color, _titles[0], _titles[1], _titles[2], _formats[0], _formats[1], _formats[2]);
  }
  public void VGrid_Lib_BDraw_AddAxes(float numberHeight, float titleHeight, float3 p0, float3 p1, float4 color,
    string xTitle, string yTitle, string zTitle, string xFormat = "0.00", string yFormat = null, string zFormat = null, bool zeroOrigin = false)
  {
    float3 range0 = p0, range1 = p1; if (zeroOrigin) { range1 -= range0; range0 = f000; }
    VGrid_Lib_BDraw_AddAxes(numberHeight, titleHeight, p0, p1, range0, range1, color, xTitle, yTitle, zTitle, xFormat, yFormat, zFormat);
  }
  public void VGrid_Lib_BDraw_AddAxes(float numberHeight, float titleHeight, float3 p0, float3 p1, float4 color,
  string xyz_titles, string xyz_formats = "0.00", bool zeroOrigin = false)
  {
    var _titles = splitStr3(xyz_titles, false);
    var _formats = splitStr3(xyz_formats, true);
    float3 range0 = p0, range1 = p1; if (zeroOrigin) { range1 -= range0; range0 = f000; }
    VGrid_Lib_BDraw_AddAxes(numberHeight, titleHeight, p0, p1, range0, range1, color, _titles[0], _titles[1], _titles[2], _formats[0], _formats[1], _formats[2]);
  }
  public virtual void VGrid_Lib_BDraw_AddAxes(float numberHeight, float titleHeight, float4 color,
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
    VGrid_Lib_BDraw_AddXAxis(xTitleA, titleHeight, x0A, x1A, f100, f011, color, xRangeA, xFormatA, numberHeight, f100, f011, f0__, VGrid_Lib_BDraw_Text_QuadType.Switch, f000, f000);
    VGrid_Lib_BDraw_AddXAxis(xTitleB, titleHeight, x0B, x1B, f100, f011, color, xRangeB, xFormatB, numberHeight, f100, f011, f011, VGrid_Lib_BDraw_Text_QuadType.Switch, f000, f000);
    VGrid_Lib_BDraw_AddXAxis(xTitleC, titleHeight, x0C, x1C, f100, f011, color, xRangeC, xFormatC, numberHeight, f100, f011, f0__, VGrid_Lib_BDraw_Text_QuadType.Switch, f000, f000);
    VGrid_Lib_BDraw_AddXAxis(xTitleD, titleHeight, x0D, x1D, f100, f011, color, xRangeD, xFormatD, numberHeight, f100, f011, f011, VGrid_Lib_BDraw_Text_QuadType.Switch, f000, f000);
    VGrid_Lib_BDraw_AddYAxis(yTitleA, titleHeight, y0A, y1A, f010, f_01, color, yRangeA, yFormatA, numberHeight, f10_, f010, f_01, VGrid_Lib_BDraw_Text_QuadType.Switch, f000, f000);
    VGrid_Lib_BDraw_AddYAxis(yTitleB, titleHeight, y0B, y1B, f0_0, f10_, color, yRangeB, yFormatB, numberHeight, f10_, f010, f10_, VGrid_Lib_BDraw_Text_QuadType.Switch, f000, f000);
    VGrid_Lib_BDraw_AddYAxis(yTitleC, titleHeight, y0C, y1C, f010, f_01, color, yRangeC, yFormatC, numberHeight, f10_, f010, f_01, VGrid_Lib_BDraw_Text_QuadType.Switch, f000, f000);
    VGrid_Lib_BDraw_AddYAxis(yTitleD, titleHeight, y0D, y1D, f0_0, f10_, color, yRangeD, yFormatD, numberHeight, f10_, f010, f10_, VGrid_Lib_BDraw_Text_QuadType.Switch, f000, f000);
    VGrid_Lib_BDraw_AddZAxis(zTitleA, titleHeight, z0A, z1A, f010, f_01, color, zRangeA, zFormatA, numberHeight, f10_, f010, f_01, VGrid_Lib_BDraw_Text_QuadType.Switch, f000, f000);
    VGrid_Lib_BDraw_AddZAxis(zTitleB, titleHeight, z0B, z1B, f0_0, f10_, color, zRangeB, zFormatB, numberHeight, f10_, f010, f10_, VGrid_Lib_BDraw_Text_QuadType.Switch, f000, f000);
    VGrid_Lib_BDraw_AddZAxis(zTitleC, titleHeight, z0C, z1C, f010, f_01, color, zRangeC, zFormatC, numberHeight, f10_, f010, f_01, VGrid_Lib_BDraw_Text_QuadType.Switch, f000, f000);
    VGrid_Lib_BDraw_AddZAxis(zTitleD, titleHeight, z0D, z1D, f0_0, f10_, color, zRangeD, zFormatD, numberHeight, f10_, f010, f10_, VGrid_Lib_BDraw_Text_QuadType.Switch, f000, f000);
  }
  public void VGrid_Lib_BDraw_AddAxes(float numberHeight, float titleHeight, float4 color,
    string xTitleA, float3 x0A, float3 x1A, float2 xRangeA, string xFormatA,
    string xTitleB, float3 x0B, float3 x1B, float2 xRangeB, string xFormatB,
    string yTitleA, float3 y0A, float3 y1A, float2 yRangeA, string yFormatA,
    string yTitleB, float3 y0B, float3 y1B, float2 yRangeB, string yFormatB,
    string zTitleA, float3 z0A, float3 z1A, float2 zRangeA, string zFormatA,
    string zTitleB, float3 z0B, float3 z1B, float2 zRangeB, string zFormatB)
  {
    VGrid_Lib_BDraw_AddXAxis(xTitleA, titleHeight, x0A, x1A, f100, f011, color, xRangeA, xFormatA, numberHeight, f100, f011, f0__, VGrid_Lib_BDraw_Text_QuadType.Switch, f000, f000);
    VGrid_Lib_BDraw_AddXAxis(xTitleB, titleHeight, x0B, x1B, f100, f011, color, xRangeB, xFormatB, numberHeight, f100, f011, f011, VGrid_Lib_BDraw_Text_QuadType.Switch, f000, f000);
    VGrid_Lib_BDraw_AddYAxis(yTitleA, titleHeight, y0A, y1A, f010, f_01, color, yRangeA, yFormatA, numberHeight, f10_, f010, f_01, VGrid_Lib_BDraw_Text_QuadType.Switch, f000, f000);
    VGrid_Lib_BDraw_AddYAxis(yTitleB, titleHeight, y0B, y1B, f0_0, f10_, color, yRangeB, yFormatB, numberHeight, f10_, f010, f10_, VGrid_Lib_BDraw_Text_QuadType.Switch, f000, f000);
    VGrid_Lib_BDraw_AddZAxis(zTitleA, titleHeight, z0A, z1A, f010, f_01, color, zRangeA, zFormatA, numberHeight, f10_, f010, f_01, VGrid_Lib_BDraw_Text_QuadType.Switch, f000, f000);
    VGrid_Lib_BDraw_AddZAxis(zTitleB, titleHeight, z0B, z1B, f0_0, f10_, color, zRangeB, zFormatB, numberHeight, f10_, f010, f10_, VGrid_Lib_BDraw_Text_QuadType.Switch, f000, f000);
  }
  public void VGrid_Lib_BDraw_AddAxes(float numberHeight, float titleHeight, float4 color,
    string xTitleA, float3 x0A, float3 x1A, float2 xRangeA, string xFormatA,
    string xTitleB, float3 x0B, float3 x1B, float2 xRangeB, string xFormatB,
    string yTitleA, float3 y0A, float3 y1A, float2 yRangeA, string yFormatA,
    string yTitleB, float3 y0B, float3 y1B, float2 yRangeB, string yFormatB)
  {
    VGrid_Lib_BDraw_AddXAxis(xTitleA, titleHeight, x0A, x1A, f100, f011, color, xRangeA, xFormatA, numberHeight, f100, f011, f0__, VGrid_Lib_BDraw_Text_QuadType.Switch, f000, f000);
    VGrid_Lib_BDraw_AddXAxis(xTitleB, titleHeight, x0B, x1B, f100, f011, color, xRangeB, xFormatB, numberHeight, f100, f011, f011, VGrid_Lib_BDraw_Text_QuadType.Switch, f000, f000);
    VGrid_Lib_BDraw_AddYAxis(yTitleA, titleHeight, y0A, y1A, f010, f_01, color, yRangeA, yFormatA, numberHeight, f10_, f010, f_01, VGrid_Lib_BDraw_Text_QuadType.Switch, f000, f000);
    VGrid_Lib_BDraw_AddYAxis(yTitleB, titleHeight, y0B, y1B, f0_0, f10_, color, yRangeB, yFormatB, numberHeight, f10_, f010, f10_, VGrid_Lib_BDraw_Text_QuadType.Switch, f000, f000);
  }
  public void VGrid_Lib_BDraw_AddAxes(float numberHeight, float titleHeight, float4 color,
    string xTitleA, float3 x0A, float3 x1A, float2 xRangeA, string xFormatA,
    string yTitleA, float3 y0A, float3 y1A, float2 yRangeA, string yFormatA)
  {
    VGrid_Lib_BDraw_AddAxes(numberHeight, titleHeight, color, xTitleA, x0A, x1A, xRangeA, xFormatA, xTitleA, x0A, x1A, xRangeA, xFormatA,
     yTitleA, y0A, y1A, yRangeA, yFormatA, yTitleA, y0A, y1A, yRangeA, yFormatA);
  }
  public uint VGrid_Lib_BDraw_GetXAxisN(float textHeight, uint decimalN, float2 vRange) { float w = decimalN * textHeight; uint axisN = roundu(abs(extent(vRange)) / w); return clamp(axisN, 2, 25); }
  public uint VGrid_Lib_BDraw_GetYAxisN(float textHeight, float2 vRange) => roundu(abs(extent(vRange)) / textHeight * 0.75f);
  public uint3 VGrid_Lib_BDraw_GetXAxisN(float textHeight, uint3 decimalN, float3 cubeMin, float3 cubeMax)
  {
    float3 w = decimalN * textHeight;
    uint3 axisN = roundu(abs(cubeMax - cubeMin) / w);
    return clamp(axisN, u111 * 2, u111 * 25);
  }
  public uint3 VGrid_Lib_BDraw_GetDecimalN(float3 cubeMin, float3 cubeMax)
  {
    int3 tickN = 25 * i111;
    float3 pRange = cubeMax - cubeMin, range = NiceNum(pRange, false);
    float3 di = NiceNum(range / (tickN - 1), true);
    uint3 decimalN = roundu(di >= 1) * flooru(1 + abs(log10(roundu(di == f000) + di)));
    return max(u111, decimalN);
  }
  public uint VGrid_Lib_BDraw_GetDecimalN(float2 vRange)
  {
    int tickN = 25;
    float pRange = abs(extent(vRange)), range = NiceNum(pRange, false);
    float di = NiceNum(range / (tickN - 1), true);
    uint decimalN = roundu(Is(di >= 1)) * flooru(1 + abs(log10(roundu(Is(di == 0)) + di)));
    return max(1, decimalN);
  }
  public void VGrid_Lib_BDraw_AddLegend(string title, float2 vRange, string format)
  {
    float h = 8;
    float3 c = 10000 * f110;
    VGrid_Lib_BDraw_AddYAxis(title, 0.4f, c + float3(0.4f, -h / 2, 0), c + float3(0.4f, h / 2, 0), f010, f_00, BLACK, vRange, format, 0.2f, f100, f010, f_00, VGrid_Lib_BDraw_Text_QuadType.FrontOnly, f000, f000);
  }
  public virtual v2f vert_VGrid_Lib_BDraw_Text(VGrid_Lib_BDraw_TextInfo t, uint i, uint j, v2f o)
  {
    float3 p = t.p, p0 = t.p0, p1 = t.p1, right = t.right, up = t.up;
    float h = t.height;
    float2 uvSize = t.uvSize;
    float4 color = t.color;
    uint quadType = t.quadType, justification = t.justification, axis = t.axis;
    float w = h * uvSize.x;
    float3 jp = f000;
    if (quadType == VGrid_Lib_BDraw_Text_QuadType_Billboard)
    {
      o.wPos = p;
      switch (justification)
      {
        case VGrid_Lib_BDraw_TextAlignment_BottomLeft: jp = w / 2 * right + h / 2 * up; break;
        case VGrid_Lib_BDraw_TextAlignment_CenterLeft: jp = w / 2 * right; break;
        case VGrid_Lib_BDraw_TextAlignment_TopLeft: jp = w / 2 * right - h / 2 * up; break;
        case VGrid_Lib_BDraw_TextAlignment_BottomCenter: jp = h / 2 * up; break;
        case VGrid_Lib_BDraw_TextAlignment_CenterCenter: break;
        case VGrid_Lib_BDraw_TextAlignment_TopCenter: jp = -h / 2 * up; break;
        case VGrid_Lib_BDraw_TextAlignment_BottomRight: jp = -w / 2 * right + h / 2 * up; break;
        case VGrid_Lib_BDraw_TextAlignment_CenterRight: jp = -w / 2 * right; break;
        case VGrid_Lib_BDraw_TextAlignment_TopRight: jp = -w / 2 * right - h / 2 * up; break;
      }
      float4 billboardQuad = float4((VGrid_Lib_BDraw_wrapJ(j, 2) - 0.5f) * w, (0.5f - VGrid_Lib_BDraw_wrapJ(j, 1)) * h, 0, 0);
      o = VGrid_Lib_BDraw_o_i(i, VGrid_Lib_BDraw_o_drawType(VGrid_Lib_BDraw_Draw_Text3D, VGrid_Lib_BDraw_o_r(i, VGrid_Lib_BDraw_o_color(color, VGrid_Lib_BDraw_o_pos_PV(p, billboardQuad + float4(jp, 0), VGrid_Lib_BDraw_o_normal(f00_, VGrid_Lib_BDraw_o_uv(float2(VGrid_Lib_BDraw_wrapJ(j, 2), VGrid_Lib_BDraw_wrapJ(j, 4)) * uvSize, o)))))));
    }
    else if (quadType == VGrid_Lib_BDraw_Text_QuadType_Arrow)
    {
      o.wPos = p;
      right = normalize(p1 - p0);
      up = f010;
      jp = w / 2 * right;
      float3 q0 = p - jp, q1 = p + jp;
      o = VGrid_Lib_BDraw_o_uv(dot(p - _WorldSpaceCameraPos, cross(right, up)) < 0 ? float2(length(q1 - q0) / h * VGrid_Lib_BDraw_wrapJ(j, 1), VGrid_Lib_BDraw_wrapJ(j, 2) - 0.5f) : float2(length(q1 - q0) / h * (1 - VGrid_Lib_BDraw_wrapJ(j, 1)), 0.5f - VGrid_Lib_BDraw_wrapJ(j, 2)), VGrid_Lib_BDraw_o_drawType(VGrid_Lib_BDraw_Draw_Text3D, vert_VGrid_Lib_BDraw_Arrow(q0, q1, h * 0.165f, color, i, j, o)));
    }
    else if (quadType == VGrid_Lib_BDraw_Text_QuadType_FrontBack || quadType == VGrid_Lib_BDraw_Text_QuadType_Switch || dot(p - _WorldSpaceCameraPos, cross(right, up)) > 0)
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
          case VGrid_Lib_BDraw_TextAlignment_BottomLeft: break;
          case VGrid_Lib_BDraw_TextAlignment_CenterLeft: jp = -h / 2 * up; break;
          case VGrid_Lib_BDraw_TextAlignment_TopLeft: jp = -h * up; break;
          case VGrid_Lib_BDraw_TextAlignment_BottomCenter: jp = -w / 2 * right; break;
          case VGrid_Lib_BDraw_TextAlignment_CenterCenter: jp = -w / 2 * right - h / 2 * up; break;
          case VGrid_Lib_BDraw_TextAlignment_TopCenter: jp = -w / 2 * right - h * up; break;
          case VGrid_Lib_BDraw_TextAlignment_BottomRight: jp = -w * right; break;
          case VGrid_Lib_BDraw_TextAlignment_CenterRight: jp = -w * right - h / 2 * up; break;
          case VGrid_Lib_BDraw_TextAlignment_TopRight: jp = -w * right - h * up; break;
        }
        float3 q0 = p + jp, q1 = q0 + right * w, p2 = q1 + up * h, p3 = q0 + up * h;
        o = VGrid_Lib_BDraw_o_i(i, VGrid_Lib_BDraw_o_drawType(VGrid_Lib_BDraw_Draw_Text3D, VGrid_Lib_BDraw_o_r(i, VGrid_Lib_BDraw_o_color(color, VGrid_Lib_BDraw_o_pos_c(o.wPos = j % 5 == 0 ? p3 : j == 1 ? p2 : j == 4 ? q0 : q1, VGrid_Lib_BDraw_o_normal(cross(right, up), VGrid_Lib_BDraw_o_uv(quadType == VGrid_Lib_BDraw_Text_QuadType_Switch && dot(p - _WorldSpaceCameraPos, cross(right, up)) < 0 ? float2(1 - VGrid_Lib_BDraw_wrapJ(j, 2), VGrid_Lib_BDraw_wrapJ(j, 4)) * uvSize : float2(VGrid_Lib_BDraw_wrapJ(j, 2), VGrid_Lib_BDraw_wrapJ(j, 4)) * uvSize, o)))))));
      }
    }
    return o;
  }
  public virtual v2f vert_VGrid_Lib_BDraw_Text(uint i, uint j, v2f o) => vert_VGrid_Lib_BDraw_Text(VGrid_Lib_BDraw_textInfo(i), i, j, o);
  public virtual void VGrid_Lib_BDraw_getTextInfo_GS(uint3 id)
  {
    uint i = id.x;
    VGrid_Lib_BDraw_TextInfo ti = VGrid_Lib_BDraw_textInfo(i);
    ti.textI = i;
    ti.uvSize = f01;
    uint2 textIs = VGrid_Lib_BDraw_Get_text_indexes(i);
    float2 t = ti.uvSize;
    for (uint j = textIs.x; j < textIs.y; j++) { uint byteI = VGrid_Lib_BDraw_Byte(j); if (byteI >= 32) { byteI -= 32; t.x += VGrid_Lib_BDraw_fontInfos[byteI].advance; } }
    t.x /= g.VGrid_Lib_BDraw_fontSize;
    ti.uvSize = t;
    VGrid_Lib_BDraw_textInfo(i, ti);
  }
  public virtual void VGrid_Lib_BDraw_setDefaultTextInfo_GS(uint3 id)
  {
    uint i = id.x;
    if (i > 0)
    {
      VGrid_Lib_BDraw_TextInfo t = VGrid_Lib_BDraw_textInfo(0), ti = VGrid_Lib_BDraw_textInfo(i);
      ti.color = t.color; ti.backColor = t.backColor; ti.p = t.p; ti.p0 = t.p0; ti.p1 = t.p1; ti.height = t.height;
      ti.quadType = (uint)t.quadType; ti.axis = t.axis; ti.right = t.right; ti.up = t.up; ti.justification = t.justification;
      VGrid_Lib_BDraw_textInfo(i, ti);
    }
  }
  public virtual float3 VGrid_Lib_BDraw_gridMin() { return f000; }
  public virtual float3 VGrid_Lib_BDraw_gridMax() { return f111; }
  public float3 VGrid_Lib_BDraw_gridSize() { return VGrid_Lib_BDraw_gridMax() - VGrid_Lib_BDraw_gridMin(); }
  public float3 VGrid_Lib_BDraw_gridCenter() { return (VGrid_Lib_BDraw_gridMax() + VGrid_Lib_BDraw_gridMin()) / 2; }
  public virtual float4 frag_VGrid_Lib_BDraw_Text(Texture2D t, RWStructuredBuffer<uint> _text, RWStructuredBuffer<VGrid_Lib_BDraw_FontInfo> VGrid_Lib_BDraw_fontInfos, float VGrid_Lib_BDraw_fontSize, uint quadType, float4 backColor, uint2 textIs, v2f i)
  {
    float uv_x = 0;
    float4 color = backColor;
    for (uint textI = textIs.x; textI < textIs.y; textI++)
    {
      uint fontInfoI = TextByte(_text, textI) - 32;
      VGrid_Lib_BDraw_FontInfo f = VGrid_Lib_BDraw_fontInfos[fontInfoI];
      float dx = f.advance / VGrid_Lib_BDraw_fontSize;
      float2 mn = float2(f.minX, f.minY) / VGrid_Lib_BDraw_fontSize, mx = float2(f.maxX, f.maxY) / VGrid_Lib_BDraw_fontSize, range = mx - mn;
      if (quadType == VGrid_Lib_BDraw_Text_QuadType_Arrow) { mn.y -= 0.5f; mx.y -= 0.5f; }
      float2 uv = (i.uv - uv_x * f10 + f1_ * mn - float2(f.bearing / VGrid_Lib_BDraw_fontSize, 0.25f)) / range;
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
  public virtual float4 frag_VGrid_Lib_BDraw_GS(v2f i, float4 color)
  {
    switch (VGrid_Lib_BDraw_o_drawType(i))
    {
      case uint_max: Discard(0); break;
      case VGrid_Lib_BDraw_Draw_Sphere: color = frag_VGrid_Lib_BDraw_Sphere(i); break;
      case VGrid_Lib_BDraw_Draw_Line: color = frag_VGrid_Lib_BDraw_Line(i); break;
      case VGrid_Lib_BDraw_Draw_Arrow: color = frag_VGrid_Lib_BDraw_Arrow(i); break;
      case VGrid_Lib_BDraw_Draw_Signal: color = frag_VGrid_Lib_BDraw_Signal(i); break;
      case VGrid_Lib_BDraw_Draw_LineSegment: color = frag_VGrid_Lib_BDraw_LineSegment(i); break;
      case VGrid_Lib_BDraw_Draw_Mesh: color = frag_VGrid_Lib_BDraw_Mesh(i); break;
      case VGrid_Lib_BDraw_Draw_Text3D:
        VGrid_Lib_BDraw_TextInfo t = VGrid_Lib_BDraw_textInfo(VGrid_Lib_BDraw_o_i(i));
        color = frag_VGrid_Lib_BDraw_Text(VGrid_Lib_BDraw_fontTexture, VGrid_Lib_BDraw_tab_delimeted_text, VGrid_Lib_BDraw_fontInfos, VGrid_Lib_BDraw_fontSize, t.quadType, t.backColor, VGrid_Lib_BDraw_Get_text_indexes(t.textI), i);
        break;
    }
    return color;
  }

  public virtual void base_VGrid_Lib_BDraw_Start0_GS()
  {
    VGrid_Lib_BDraw_AppendBuff_Start0_GS();
  }
  public virtual void base_VGrid_Lib_BDraw_Start1_GS()
  {
    VGrid_Lib_BDraw_AppendBuff_Start1_GS();
  }
  public virtual void base_VGrid_Lib_BDraw_OnApplicationQuit() { Save_UI(); OnApplicationQuit_GS(); base.OnApplicationQuit(); already_quited = true; }
  public virtual void base_VGrid_Lib_BDraw_LateUpdate0_GS()
  {
    VGrid_Lib_BDraw_AppendBuff_LateUpdate0_GS();
  }
  public virtual void base_VGrid_Lib_BDraw_LateUpdate1_GS()
  {
    VGrid_Lib_BDraw_AppendBuff_LateUpdate1_GS();
  }
  public virtual void base_VGrid_Lib_BDraw_Update0_GS()
  {
    VGrid_Lib_BDraw_AppendBuff_Update0_GS();
  }
  public virtual void base_VGrid_Lib_BDraw_Update1_GS()
  {
    VGrid_Lib_BDraw_AppendBuff_Update1_GS();
  }
  public virtual void base_VGrid_Lib_BDraw_OnValueChanged_GS()
  {
    VGrid_Lib_BDraw_AppendBuff_OnValueChanged_GS();
  }
  public virtual void base_VGrid_Lib_BDraw_InitBuffers0_GS()
  {
    VGrid_Lib_BDraw_AppendBuff_InitBuffers0_GS();
  }
  public virtual void base_VGrid_Lib_BDraw_InitBuffers1_GS()
  {
    VGrid_Lib_BDraw_AppendBuff_InitBuffers1_GS();
  }
  [HideInInspector]
  public virtual void base_VGrid_Lib_BDraw_onRenderObject_GS(ref bool render, ref bool cpu)
  {
    VGrid_Lib_BDraw_AppendBuff_onRenderObject_GS(ref render, ref cpu);
  }
  public virtual float4 base_frag_VGrid_Lib_BDraw_GS(v2f i, float4 color) { return color; }
  public void VGrid_Lib_BDraw_AppendBuff_print_Bits(string title) { StrBldr sb = StrBldr($"{title}, VGrid_Lib_BDraw_AppendBuff_Bits"); for (uint i = 0; i < VGrid_Lib_BDraw_AppendBuff_BitN; i++) sb.Add(" ", VGrid_Lib_BDraw_AppendBuff_Bits[i]); print(sb); }
  public void VGrid_Lib_BDraw_AppendBuff_print_Sums(string title) { StrBldr sb = StrBldr($"{title}, VGrid_Lib_BDraw_AppendBuff_Sums"); for (uint i = 0; i < VGrid_Lib_BDraw_AppendBuff_BitN; i++) sb.Add(" ", VGrid_Lib_BDraw_AppendBuff_Sums[i]); print(sb); }
  public void VGrid_Lib_BDraw_AppendBuff_print_Indexes(string title) { StrBldr sb = StrBldr($"{title}: VGrid_Lib_BDraw_AppendBuff_Indexes"); for (uint i = 0; i < VGrid_Lib_BDraw_AppendBuff_IndexN; i++) sb.Add(" ", VGrid_Lib_BDraw_AppendBuff_Indexes[i]); print(sb); }
  public void VGrid_Lib_BDraw_AppendBuff_SetN(uint n)
  {
    if (n > 2147450880) throw new Exception("gsAppendBuff: VGrid_Lib_BDraw_AppendBuff_N > 2,147,450,880");
    VGrid_Lib_BDraw_AppendBuff_N = n; VGrid_Lib_BDraw_AppendBuff_BitN = ceilu(VGrid_Lib_BDraw_AppendBuff_N, 32); VGrid_Lib_BDraw_AppendBuff_BitN1 = ceilu(VGrid_Lib_BDraw_AppendBuff_BitN, numthreads1); VGrid_Lib_BDraw_AppendBuff_BitN2 = ceilu(VGrid_Lib_BDraw_AppendBuff_BitN1, numthreads1);
    AllocData_VGrid_Lib_BDraw_AppendBuff_Bits(VGrid_Lib_BDraw_AppendBuff_BitN); AllocData_VGrid_Lib_BDraw_AppendBuff_Fills1(VGrid_Lib_BDraw_AppendBuff_BitN1); AllocData_VGrid_Lib_BDraw_AppendBuff_Fills2(VGrid_Lib_BDraw_AppendBuff_BitN2); AllocData_VGrid_Lib_BDraw_AppendBuff_Sums(VGrid_Lib_BDraw_AppendBuff_BitN);
  }
  public void VGrid_Lib_BDraw_AppendBuff_FillPrefixes() { Gpu_VGrid_Lib_BDraw_AppendBuff_GetFills1(); Gpu_VGrid_Lib_BDraw_AppendBuff_GetFills2(); Gpu_VGrid_Lib_BDraw_AppendBuff_IncFills1(); Gpu_VGrid_Lib_BDraw_AppendBuff_IncSums(); }
  public void VGrid_Lib_BDraw_AppendBuff_getIndexes() { AllocData_VGrid_Lib_BDraw_AppendBuff_Indexes(VGrid_Lib_BDraw_AppendBuff_IndexN); Gpu_VGrid_Lib_BDraw_AppendBuff_GetIndexes(); }
  public void VGrid_Lib_BDraw_AppendBuff_FillIndexes() { VGrid_Lib_BDraw_AppendBuff_FillPrefixes(); VGrid_Lib_BDraw_AppendBuff_getIndexes(); }
  public virtual uint VGrid_Lib_BDraw_AppendBuff_Run(uint n) { VGrid_Lib_BDraw_AppendBuff_SetN(n); Gpu_VGrid_Lib_BDraw_AppendBuff_GetSums(); VGrid_Lib_BDraw_AppendBuff_FillIndexes(); return VGrid_Lib_BDraw_AppendBuff_IndexN; }
  public uint VGrid_Lib_BDraw_AppendBuff_Run(int n) => VGrid_Lib_BDraw_AppendBuff_Run((uint)n);
  public uint VGrid_Lib_BDraw_AppendBuff_Run(uint2 n) => VGrid_Lib_BDraw_AppendBuff_Run(cproduct(n)); public uint VGrid_Lib_BDraw_AppendBuff_Run(uint3 n) => VGrid_Lib_BDraw_AppendBuff_Run(cproduct(n));
  public uint VGrid_Lib_BDraw_AppendBuff_Run(int2 n) => VGrid_Lib_BDraw_AppendBuff_Run(cproduct(n)); public uint VGrid_Lib_BDraw_AppendBuff_Run(int3 n) => VGrid_Lib_BDraw_AppendBuff_Run(cproduct(n));
  public virtual void VGrid_Lib_BDraw_AppendBuff_Init_Bits(uint n, GS parent, string _N, string _BitN, ref RWStructuredBuffer<uint> _Bits) { VGrid_Lib_BDraw_AppendBuff_SetN(n); parent.SetValue(_N, VGrid_Lib_BDraw_AppendBuff_N); parent.SetValue(_BitN, VGrid_Lib_BDraw_AppendBuff_BitN); }
  public virtual void VGrid_Lib_BDraw_AppendBuff_Prefix_Sums() { Gpu_VGrid_Lib_BDraw_AppendBuff_Get_Bits_Sums(); VGrid_Lib_BDraw_AppendBuff_FillPrefixes(); }
  public virtual void VGrid_Lib_BDraw_AppendBuff_Bit_Indexes(RWStructuredBuffer<uint> bits, GS _this, string _IndexN, ref RWStructuredBuffer<uint> _Indexes) { VGrid_Lib_BDraw_AppendBuff_Prefix_Sums(); VGrid_Lib_BDraw_AppendBuff_getIndexes(); _this.SetValue(_IndexN, VGrid_Lib_BDraw_AppendBuff_IndexN); }
  public uint VGrid_Lib_BDraw_AppendBuff_Assign_Bits(uint i, uint j, uint bits) => bits | (Is(i < VGrid_Lib_BDraw_AppendBuff_N && VGrid_Lib_BDraw_AppendBuff_IsBitOn(i)) << (int)j);
  public virtual void VGrid_Lib_BDraw_AppendBuff_Get_Bits_GS(uint3 id) { uint i = id.x, j, k, bits = 0; if (i < VGrid_Lib_BDraw_AppendBuff_BitN) { for (j = 0, k = i * 32; j < 32; j++) bits = VGrid_Lib_BDraw_AppendBuff_Assign_Bits(k + j, j, bits); VGrid_Lib_BDraw_AppendBuff_Bits[i] = bits; } }
  public virtual IEnumerator VGrid_Lib_BDraw_AppendBuff_GetSums_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI)
  {
    uint i = id.x, c, s, j, k, bits = 0;
    if (i < VGrid_Lib_BDraw_AppendBuff_BitN) { for (j = 0, k = i * 32; j < 32; j++) bits = VGrid_Lib_BDraw_AppendBuff_Assign_Bits(k + j, j, bits); VGrid_Lib_BDraw_AppendBuff_Bits[i] = bits; c = countbits(bits); } else c = 0;
    VGrid_Lib_BDraw_AppendBuff_grp0[grpI] = c; VGrid_Lib_BDraw_AppendBuff_grp[grpI] = c; yield return GroupMemoryBarrierWithGroupSync();
    for (s = 1; s < numthreads1; s *= 2)
    {
      if (grpI >= s && i < VGrid_Lib_BDraw_AppendBuff_BitN) VGrid_Lib_BDraw_AppendBuff_grp[grpI] = VGrid_Lib_BDraw_AppendBuff_grp0[grpI] + VGrid_Lib_BDraw_AppendBuff_grp0[grpI - s]; yield return GroupMemoryBarrierWithGroupSync();
      VGrid_Lib_BDraw_AppendBuff_grp0[grpI] = VGrid_Lib_BDraw_AppendBuff_grp[grpI]; yield return GroupMemoryBarrierWithGroupSync();
    }
    if (i < VGrid_Lib_BDraw_AppendBuff_BitN) VGrid_Lib_BDraw_AppendBuff_Sums[i] = VGrid_Lib_BDraw_AppendBuff_grp[grpI];
  }
  public virtual IEnumerator VGrid_Lib_BDraw_AppendBuff_Get_Bits_Sums_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI)
  {
    uint i = id.x, c = i < VGrid_Lib_BDraw_AppendBuff_BitN ? countbits(VGrid_Lib_BDraw_AppendBuff_Bits[i]) : 0, s;
    VGrid_Lib_BDraw_AppendBuff_grp0[grpI] = c; VGrid_Lib_BDraw_AppendBuff_grp[grpI] = c; yield return GroupMemoryBarrierWithGroupSync();
    for (s = 1; s < numthreads1; s *= 2)
    {
      if (grpI >= s && i < VGrid_Lib_BDraw_AppendBuff_BitN) VGrid_Lib_BDraw_AppendBuff_grp[grpI] = VGrid_Lib_BDraw_AppendBuff_grp0[grpI] + VGrid_Lib_BDraw_AppendBuff_grp0[grpI - s]; yield return GroupMemoryBarrierWithGroupSync();
      VGrid_Lib_BDraw_AppendBuff_grp0[grpI] = VGrid_Lib_BDraw_AppendBuff_grp[grpI]; yield return GroupMemoryBarrierWithGroupSync();
    }
    if (i < VGrid_Lib_BDraw_AppendBuff_BitN) VGrid_Lib_BDraw_AppendBuff_Sums[i] = VGrid_Lib_BDraw_AppendBuff_grp[grpI];
  }
  public virtual IEnumerator VGrid_Lib_BDraw_AppendBuff_GetFills1_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI)
  {
    uint i = id.x, k = i + 1, j = k * numthreads1 - 1, c = i < VGrid_Lib_BDraw_AppendBuff_BitN1 - 1 ? VGrid_Lib_BDraw_AppendBuff_Sums[j] : i < VGrid_Lib_BDraw_AppendBuff_BitN1 ? VGrid_Lib_BDraw_AppendBuff_Sums[VGrid_Lib_BDraw_AppendBuff_BitN - 1] : 0, s;
    VGrid_Lib_BDraw_AppendBuff_grp0[grpI] = c; VGrid_Lib_BDraw_AppendBuff_grp[grpI] = c; yield return GroupMemoryBarrierWithGroupSync();
    for (s = 1; s < numthreads1; s *= 2)
    {
      if (grpI >= s && i < VGrid_Lib_BDraw_AppendBuff_BitN1) VGrid_Lib_BDraw_AppendBuff_grp[grpI] = VGrid_Lib_BDraw_AppendBuff_grp0[grpI] + VGrid_Lib_BDraw_AppendBuff_grp0[grpI - s]; yield return GroupMemoryBarrierWithGroupSync();
      VGrid_Lib_BDraw_AppendBuff_grp0[grpI] = VGrid_Lib_BDraw_AppendBuff_grp[grpI]; yield return GroupMemoryBarrierWithGroupSync();
    }
    if (i < VGrid_Lib_BDraw_AppendBuff_BitN1) VGrid_Lib_BDraw_AppendBuff_Fills1[i] = VGrid_Lib_BDraw_AppendBuff_grp[grpI];
  }
  public virtual IEnumerator VGrid_Lib_BDraw_AppendBuff_GetFills2_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI)
  {
    uint i = id.x, k = i + 1, j = k * numthreads1 - 1, c = i < VGrid_Lib_BDraw_AppendBuff_BitN2 - 1 ? VGrid_Lib_BDraw_AppendBuff_Fills1[j] : i < VGrid_Lib_BDraw_AppendBuff_BitN2 ? VGrid_Lib_BDraw_AppendBuff_Fills1[VGrid_Lib_BDraw_AppendBuff_BitN1 - 1] : 0, s;
    VGrid_Lib_BDraw_AppendBuff_grp0[grpI] = c; VGrid_Lib_BDraw_AppendBuff_grp[grpI] = c; yield return GroupMemoryBarrierWithGroupSync();
    for (s = 1; s < numthreads1; s *= 2)
    {
      if (grpI >= s && i < VGrid_Lib_BDraw_AppendBuff_BitN2) VGrid_Lib_BDraw_AppendBuff_grp[grpI] = VGrid_Lib_BDraw_AppendBuff_grp0[grpI] + VGrid_Lib_BDraw_AppendBuff_grp0[grpI - s]; yield return GroupMemoryBarrierWithGroupSync();
      VGrid_Lib_BDraw_AppendBuff_grp0[grpI] = VGrid_Lib_BDraw_AppendBuff_grp[grpI]; yield return GroupMemoryBarrierWithGroupSync();
    }
    if (i < VGrid_Lib_BDraw_AppendBuff_BitN2) VGrid_Lib_BDraw_AppendBuff_Fills2[i] = VGrid_Lib_BDraw_AppendBuff_grp[grpI];
  }
  public virtual void VGrid_Lib_BDraw_AppendBuff_IncFills1_GS(uint3 id) { uint i = id.x; if (i >= numthreads1) VGrid_Lib_BDraw_AppendBuff_Fills1[i] += VGrid_Lib_BDraw_AppendBuff_Fills2[i / numthreads1 - 1]; }
  public virtual void VGrid_Lib_BDraw_AppendBuff_IncSums_GS(uint3 id) { uint i = id.x; if (i >= numthreads1) VGrid_Lib_BDraw_AppendBuff_Sums[i] += VGrid_Lib_BDraw_AppendBuff_Fills1[i / numthreads1 - 1]; if (i == VGrid_Lib_BDraw_AppendBuff_BitN - 1) VGrid_Lib_BDraw_AppendBuff_IndexN = VGrid_Lib_BDraw_AppendBuff_Sums[i]; }
  public virtual void VGrid_Lib_BDraw_AppendBuff_GetIndexes_GS(uint3 id) { uint i = id.x, j, sum = i == 0 ? 0 : VGrid_Lib_BDraw_AppendBuff_Sums[i - 1], b, i32 = i << 5, k; for (k = 0, b = VGrid_Lib_BDraw_AppendBuff_Bits[i]; b > 0; k++) { j = (uint)findLSB(b); VGrid_Lib_BDraw_AppendBuff_Indexes[sum + k] = i32 + j; b = SetBitu(b, j, 0); } }

  public virtual void base_VGrid_Lib_BDraw_AppendBuff_Start0_GS() { }
  public virtual void base_VGrid_Lib_BDraw_AppendBuff_Start1_GS() { }
  public virtual void base_VGrid_Lib_BDraw_AppendBuff_OnApplicationQuit() { Save_UI(); OnApplicationQuit_GS(); base.OnApplicationQuit(); already_quited = true; }
  public virtual void base_VGrid_Lib_BDraw_AppendBuff_LateUpdate0_GS() { }
  public virtual void base_VGrid_Lib_BDraw_AppendBuff_LateUpdate1_GS() { }
  public virtual void base_VGrid_Lib_BDraw_AppendBuff_Update0_GS() { }
  public virtual void base_VGrid_Lib_BDraw_AppendBuff_Update1_GS() { }
  public virtual void base_VGrid_Lib_BDraw_AppendBuff_OnValueChanged_GS() { }
  public virtual void base_VGrid_Lib_BDraw_AppendBuff_InitBuffers0_GS() { }
  public virtual void base_VGrid_Lib_BDraw_AppendBuff_InitBuffers1_GS() { }
  [HideInInspector]
  public virtual void base_VGrid_Lib_BDraw_AppendBuff_onRenderObject_GS(ref bool render, ref bool cpu) { }
  public virtual float4 base_frag_VGrid_Lib_BDraw_AppendBuff_GS(v2f i, float4 color) { return color; }
  public virtual void VGrid_Lib_BDraw_AppendBuff_InitBuffers0_GS() { }
  public virtual void VGrid_Lib_BDraw_AppendBuff_InitBuffers1_GS() { }
  public virtual void VGrid_Lib_BDraw_AppendBuff_LateUpdate0_GS() { }
  public virtual void VGrid_Lib_BDraw_AppendBuff_LateUpdate1_GS() { }
  public virtual void VGrid_Lib_BDraw_AppendBuff_Update0_GS() { }
  public virtual void VGrid_Lib_BDraw_AppendBuff_Update1_GS() { }
  public virtual void VGrid_Lib_BDraw_AppendBuff_Start0_GS() { }
  public virtual void VGrid_Lib_BDraw_AppendBuff_Start1_GS() { }
  public virtual void VGrid_Lib_BDraw_AppendBuff_OnValueChanged_GS() { }
  public virtual void VGrid_Lib_BDraw_AppendBuff_OnApplicationQuit_GS() { }
  public virtual void VGrid_Lib_BDraw_AppendBuff_onRenderObject_GS(ref bool render, ref bool cpu) { }
  public virtual float4 frag_VGrid_Lib_BDraw_AppendBuff_GS(v2f i, float4 color)
  {
    return color;
  }
  public virtual void VGrid_Lib_BDraw_LateUpdate0_GS() { }
  public virtual void VGrid_Lib_BDraw_LateUpdate1_GS() { }
  public virtual void VGrid_Lib_BDraw_Update0_GS() { }
  public virtual void VGrid_Lib_BDraw_Update1_GS() { }
  public virtual void VGrid_Lib_BDraw_Start0_GS() { }
  public virtual void VGrid_Lib_BDraw_Start1_GS() { }
  public virtual void VGrid_Lib_BDraw_OnValueChanged_GS() { }
  public virtual void VGrid_Lib_BDraw_OnApplicationQuit_GS() { }
  public virtual void VGrid_Lib_BDraw_onRenderObject_GS(ref bool render, ref bool cpu) { }
  public virtual void VGrid_Lib_Update0_GS() { }
  public virtual void VGrid_Lib_Update1_GS() { }
  public virtual void VGrid_Lib_Start1_GS() { }
  public virtual void VGrid_Lib_OnValueChanged_GS() { }
  public virtual void VGrid_Lib_OnApplicationQuit_GS() { }
  public virtual void VGrid_Lib_onRenderObject_GS(ref bool render, ref bool cpu) { }
  #endregion <VGrid_Lib>
  #region <OCam_Lib>
  gsOCam_Lib _OCam_Lib; public gsOCam_Lib OCam_Lib => _OCam_Lib = _OCam_Lib ?? Add_Component_to_gameObject<gsOCam_Lib>();
  #endregion <OCam_Lib>
  #region <Views_Lib>
  public UI_TreeGroup Views_Lib_ui_Views_Lib_group_CamViews_Lib => UI_Views_Lib_group_CamViews_Lib;
  public virtual void Views_Lib_onLoaded() => GS_Views_Lib.onLoaded(this);
  public virtual void Views_Lib_LateUpdate1_GS()
  {
    base_Views_Lib_LateUpdate1_GS();
    for (int i = 0; i < min(Views_Lib_CamViews_Lib.Length, 10); i++) if (GetKeyDown(CtrlAlt, (char)('1' + i))) { Views_Lib_CamViews_Lib_LoadView(i); break; }
  }
  public virtual void Views_Lib_CamViews_Lib_OnAddRow()
  {
    base_Views_Lib_CamViews_Lib_OnAddRow();
    var view = Views_Lib_CamViews_Lib[^1];
    view.viewName = $"View {Views_Lib_CamViews_Lib.Length}";
    Views_Lib_CamViews_Lib[^1] = view;
    Views_Lib_CamViews_Lib_SaveView(Views_Lib_CamViews_Lib.Length - 1);
  }
  public virtual void Views_Lib_SaveCamView(ref Views_Lib_CamView view) { }
  public virtual void Views_Lib_CamViews_Lib_SaveView(int row)
  {
    var view = Views_Lib_CamViews_Lib[row];
    view.viewTiltSpin = Views_Lib_OCam.tiltSpin; view.viewDist = Views_Lib_OCam.dist; view.viewCenter = Views_Lib_OCam.center; view.viewProjection = (uint)Views_Lib_OCam.projection;
    Views_Lib_SaveCamView(ref view);
    Views_Lib_CamViews_Lib[row] = view;
    Views_Lib_CamViews_Lib_To_UI();
  }
  public virtual void Views_Lib_LoadCamView(ref Views_Lib_CamView view) { }
  public virtual void Views_Lib_CamViews_Lib_LoadView(int row)
  {
    if (row < Views_Lib_CamViews_Lib.Length)
    {
      var view = Views_Lib_CamViews_Lib[row];
      Views_Lib_OCam.tiltSpin = view.viewTiltSpin; Views_Lib_OCam.dist = view.viewDist; Views_Lib_OCam.center = view.viewCenter;
      Views_Lib_OCam.SetProjection(view.viewProjection);
      Views_Lib_LoadCamView(ref view);
    }
  }
  public void Views_Lib_SaveView(int row) => Views_Lib_CamViews_Lib_SaveView(row - 1);
  public void Views_Lib_LoadView(int row) => Views_Lib_CamViews_Lib_LoadView(row - 1);
  gsOCam_Lib Views_Lib__OCam; public gsOCam_Lib Views_Lib_OCam => Views_Lib__OCam = Views_Lib__OCam ?? Add_Component_to_gameObject<gsOCam_Lib>();

  public virtual void base_Views_Lib_Start0_GS() { }
  public virtual void base_Views_Lib_Start1_GS() { }
  public virtual void base_Views_Lib_OnApplicationQuit() { Save_UI(); OnApplicationQuit_GS(); base.OnApplicationQuit(); already_quited = true; }
  public virtual void base_Views_Lib_LateUpdate0_GS() { }
  public virtual void base_Views_Lib_LateUpdate1_GS() { }
  public virtual void base_Views_Lib_Update0_GS() { }
  public virtual void base_Views_Lib_Update1_GS() { }
  public virtual void base_Views_Lib_OnValueChanged_GS()
  {
    var type = "gsReport_Lib".ToType();
    if (type != null) ((GS)GetComponent(type))?.OnValueChanged_GS();
  }
  public virtual void base_Views_Lib_InitBuffers0_GS() { }
  public virtual void base_Views_Lib_InitBuffers1_GS() { }
  public virtual void base_Views_Lib_onRenderObject_GS(ref bool render, ref bool cpu) { }
  public virtual float4 base_frag_Views_Lib_GS(v2f i, float4 color)
  {
    uint libI = roundu(i.tj.x);
    return color;
  }
  public virtual void Views_Lib_InitBuffers0_GS() { }
  public virtual void Views_Lib_InitBuffers1_GS() { }
  public virtual void Views_Lib_LateUpdate0_GS() { }
  public virtual void Views_Lib_Update0_GS() { }
  public virtual void Views_Lib_Update1_GS() { }
  public virtual void Views_Lib_Start0_GS() { }
  public virtual void Views_Lib_Start1_GS() { }
  public virtual void Views_Lib_OnValueChanged_GS() { }
  public virtual void Views_Lib_OnApplicationQuit_GS() { }
  public virtual void Views_Lib_onRenderObject_GS(ref bool render, ref bool cpu) { }
  public virtual float4 frag_Views_Lib_GS(v2f i, float4 color)
  {
    return color;
  }
  #endregion <Views_Lib>
  #region <Report_Lib>
  gsReport_Lib _Report_Lib; public gsReport_Lib Report_Lib => _Report_Lib = _Report_Lib ?? Add_Component_to_gameObject<gsReport_Lib>();
  #endregion <Report_Lib>
}