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
public class gsSort_ : GS
{
  [HideInInspector] public uiData data;
  [HideInInspector] public GSort g; public GSort G { get { gSort.GetData(); return gSort[0]; } }
  public void g_SetData() { if (gChanged && gSort != null) { gSort[0] = g; gSort.SetData(); gChanged = false; } }
  public virtual void vs_SetKernels(bool reallocate = false) { if (vs != null && (reallocate || vs.reallocated)) { SetKernelValues(vs, nameof(vs), kernel_add_counts, kernel_add_counts_triangle, kernel_init_vs); vs.reallocated = false; } }
  public virtual void counts_SetKernels(bool reallocate = false) { if (counts != null && (reallocate || counts.reallocated)) { SetKernelValues(counts, nameof(counts), kernel_set_sorts, kernel_add_counts, kernel_add_counts_triangle, kernel_init_counts); counts.reallocated = false; } }
  public virtual void sorts_SetKernels(bool reallocate = false) { if (sorts != null && (reallocate || sorts.reallocated)) { SetKernelValues(sorts, nameof(sorts), kernel_set_sorts); sorts.reallocated = false; } }
  public virtual void BDraw_tab_delimeted_text_SetKernels(bool reallocate = false) { if (BDraw_tab_delimeted_text != null && (reallocate || BDraw_tab_delimeted_text.reallocated)) { SetKernelValues(BDraw_tab_delimeted_text, nameof(BDraw_tab_delimeted_text), kernel_BDraw_ABuff_GetSums, kernel_BDraw_ABuff_Get_Bits, kernel_BDraw_getTextInfo); BDraw_tab_delimeted_text.reallocated = false; } }
  public virtual void BDraw_textInfos_SetKernels(bool reallocate = false) { if (BDraw_textInfos != null && (reallocate || BDraw_textInfos.reallocated)) { SetKernelValues(BDraw_textInfos, nameof(BDraw_textInfos), kernel_BDraw_setDefaultTextInfo, kernel_BDraw_getTextInfo); BDraw_textInfos.reallocated = false; } }
  public virtual void BDraw_fontInfos_SetKernels(bool reallocate = false) { if (BDraw_fontInfos != null && (reallocate || BDraw_fontInfos.reallocated)) { SetKernelValues(BDraw_fontInfos, nameof(BDraw_fontInfos), kernel_BDraw_getTextInfo); BDraw_fontInfos.reallocated = false; } }
  public virtual void Rand_rs_SetKernels(bool reallocate = false) { if (Rand_rs != null && (reallocate || Rand_rs.reallocated)) { SetKernelValues(Rand_rs, nameof(Rand_rs), kernel_init_vs, kernel_Rand_initState, kernel_Rand_initSeed); Rand_rs.reallocated = false; } }
  public virtual void BDraw_ABuff_Bits_SetKernels(bool reallocate = false) { if (BDraw_ABuff_Bits != null && (reallocate || BDraw_ABuff_Bits.reallocated)) { SetKernelValues(BDraw_ABuff_Bits, nameof(BDraw_ABuff_Bits), kernel_BDraw_ABuff_GetIndexes, kernel_BDraw_ABuff_Get_Bits_Sums, kernel_BDraw_ABuff_GetSums, kernel_BDraw_ABuff_Get_Bits); BDraw_ABuff_Bits.reallocated = false; } }
  public virtual void BDraw_ABuff_Sums_SetKernels(bool reallocate = false) { if (BDraw_ABuff_Sums != null && (reallocate || BDraw_ABuff_Sums.reallocated)) { SetKernelValues(BDraw_ABuff_Sums, nameof(BDraw_ABuff_Sums), kernel_BDraw_ABuff_GetIndexes, kernel_BDraw_ABuff_IncSums, kernel_BDraw_ABuff_GetFills1, kernel_BDraw_ABuff_Get_Bits_Sums, kernel_BDraw_ABuff_GetSums); BDraw_ABuff_Sums.reallocated = false; } }
  public virtual void BDraw_ABuff_Indexes_SetKernels(bool reallocate = false) { if (BDraw_ABuff_Indexes != null && (reallocate || BDraw_ABuff_Indexes.reallocated)) { SetKernelValues(BDraw_ABuff_Indexes, nameof(BDraw_ABuff_Indexes), kernel_BDraw_ABuff_GetIndexes, kernel_BDraw_getTextInfo); BDraw_ABuff_Indexes.reallocated = false; } }
  public virtual void BDraw_ABuff_Fills1_SetKernels(bool reallocate = false) { if (BDraw_ABuff_Fills1 != null && (reallocate || BDraw_ABuff_Fills1.reallocated)) { SetKernelValues(BDraw_ABuff_Fills1, nameof(BDraw_ABuff_Fills1), kernel_BDraw_ABuff_IncSums, kernel_BDraw_ABuff_IncFills1, kernel_BDraw_ABuff_GetFills2, kernel_BDraw_ABuff_GetFills1); BDraw_ABuff_Fills1.reallocated = false; } }
  public virtual void BDraw_ABuff_Fills2_SetKernels(bool reallocate = false) { if (BDraw_ABuff_Fills2 != null && (reallocate || BDraw_ABuff_Fills2.reallocated)) { SetKernelValues(BDraw_ABuff_Fills2, nameof(BDraw_ABuff_Fills2), kernel_BDraw_ABuff_IncFills1, kernel_BDraw_ABuff_GetFills2); BDraw_ABuff_Fills2.reallocated = false; } }
  public virtual void Gpu_set_sorts() { g_SetData(); counts?.SetCpu(); counts_SetKernels(); sorts?.SetCpu(); sorts_SetKernels(); Gpu(kernel_set_sorts, set_sorts, uint2(numberOfArrays, arrayLength)); }
  public virtual void Cpu_set_sorts() { counts?.GetGpu(); sorts?.GetGpu(); Cpu(set_sorts, uint2(numberOfArrays, arrayLength)); }
  public virtual void Cpu_set_sorts(uint3 id) { counts?.GetGpu(); sorts?.GetGpu(); set_sorts(id); }
  public virtual void Gpu_add_counts() { g_SetData(); vs?.SetCpu(); vs_SetKernels(); counts?.SetCpu(); counts_SetKernels(); Gpu(kernel_add_counts, add_counts, uint3(numberOfArrays, arrayLength, arrayLength)); counts?.ResetWrite(); }
  public virtual void Cpu_add_counts() { vs?.GetGpu(); counts?.GetGpu(); Cpu(add_counts, uint3(numberOfArrays, arrayLength, arrayLength)); counts.SetData(); }
  public virtual void Cpu_add_counts(uint3 id) { vs?.GetGpu(); counts?.GetGpu(); add_counts(id); counts.SetData(); }
  public virtual void Gpu_add_counts_triangle() { g_SetData(); vs?.SetCpu(); vs_SetKernels(); counts?.SetCpu(); counts_SetKernels(); Gpu(kernel_add_counts_triangle, add_counts_triangle, uint2(numberOfArrays, arrayLength * (arrayLength - 1) / 2)); counts?.ResetWrite(); }
  public virtual void Cpu_add_counts_triangle() { vs?.GetGpu(); counts?.GetGpu(); Cpu(add_counts_triangle, uint2(numberOfArrays, arrayLength * (arrayLength - 1) / 2)); counts.SetData(); }
  public virtual void Cpu_add_counts_triangle(uint3 id) { vs?.GetGpu(); counts?.GetGpu(); add_counts_triangle(id); counts.SetData(); }
  public virtual void Gpu_init_counts() { g_SetData(); counts_SetKernels(); Gpu(kernel_init_counts, init_counts, vsN); counts?.ResetWrite(); }
  public virtual void Cpu_init_counts() { counts?.GetGpu(); Cpu(init_counts, vsN); counts.SetData(); }
  public virtual void Cpu_init_counts(uint3 id) { counts?.GetGpu(); init_counts(id); counts.SetData(); }
  public virtual void Gpu_init_vs() { g_SetData(); vs_SetKernels(); Rand_rs?.SetCpu(); Rand_rs_SetKernels(); Gpu(kernel_init_vs, init_vs, vsN); vs?.ResetWrite(); Rand_rs?.ResetWrite(); }
  public virtual void Cpu_init_vs() { vs?.GetGpu(); Rand_rs?.GetGpu(); Cpu(init_vs, vsN); vs.SetData(); Rand_rs.SetData(); }
  public virtual void Cpu_init_vs(uint3 id) { vs?.GetGpu(); Rand_rs?.GetGpu(); init_vs(id); vs.SetData(); Rand_rs.SetData(); }
  public virtual void Gpu_Rand_initState() { g_SetData(); Rand_rs?.SetCpu(); Rand_rs_SetKernels(); Gpu(kernel_Rand_initState, Rand_initState, Rand_I); Rand_rs?.ResetWrite(); }
  public virtual void Cpu_Rand_initState() { Rand_rs?.GetGpu(); Cpu(Rand_initState, Rand_I); Rand_rs.SetData(); }
  public virtual void Cpu_Rand_initState(uint3 id) { Rand_rs?.GetGpu(); Rand_initState(id); Rand_rs.SetData(); }
  public virtual void Gpu_Rand_initSeed() { g_SetData(); Rand_rs_SetKernels(); Gpu(kernel_Rand_initSeed, Rand_initSeed, Rand_N); Rand_rs?.ResetWrite(); }
  public virtual void Cpu_Rand_initSeed() { Rand_rs?.GetGpu(); Cpu(Rand_initSeed, Rand_N); Rand_rs.SetData(); }
  public virtual void Cpu_Rand_initSeed(uint3 id) { Rand_rs?.GetGpu(); Rand_initSeed(id); Rand_rs.SetData(); }
  public virtual void Gpu_BDraw_ABuff_GetIndexes() { g_SetData(); BDraw_ABuff_Bits?.SetCpu(); BDraw_ABuff_Bits_SetKernels(); BDraw_ABuff_Sums?.SetCpu(); BDraw_ABuff_Sums_SetKernels(); BDraw_ABuff_Indexes_SetKernels(); Gpu(kernel_BDraw_ABuff_GetIndexes, BDraw_ABuff_GetIndexes, BDraw_ABuff_BitN); BDraw_ABuff_Indexes?.ResetWrite(); }
  public virtual void Cpu_BDraw_ABuff_GetIndexes() { BDraw_ABuff_Bits?.GetGpu(); BDraw_ABuff_Sums?.GetGpu(); BDraw_ABuff_Indexes?.GetGpu(); Cpu(BDraw_ABuff_GetIndexes, BDraw_ABuff_BitN); BDraw_ABuff_Indexes.SetData(); }
  public virtual void Cpu_BDraw_ABuff_GetIndexes(uint3 id) { BDraw_ABuff_Bits?.GetGpu(); BDraw_ABuff_Sums?.GetGpu(); BDraw_ABuff_Indexes?.GetGpu(); BDraw_ABuff_GetIndexes(id); BDraw_ABuff_Indexes.SetData(); }
  public virtual void Gpu_BDraw_ABuff_IncSums() { g_SetData(); BDraw_ABuff_Sums?.SetCpu(); BDraw_ABuff_Sums_SetKernels(); BDraw_ABuff_Fills1?.SetCpu(); BDraw_ABuff_Fills1_SetKernels(); gSort?.SetCpu(); Gpu(kernel_BDraw_ABuff_IncSums, BDraw_ABuff_IncSums, BDraw_ABuff_BitN); BDraw_ABuff_Sums?.ResetWrite(); g = G; }
  public virtual void Cpu_BDraw_ABuff_IncSums() { BDraw_ABuff_Sums?.GetGpu(); BDraw_ABuff_Fills1?.GetGpu(); Cpu(BDraw_ABuff_IncSums, BDraw_ABuff_BitN); BDraw_ABuff_Sums.SetData(); }
  public virtual void Cpu_BDraw_ABuff_IncSums(uint3 id) { BDraw_ABuff_Sums?.GetGpu(); BDraw_ABuff_Fills1?.GetGpu(); BDraw_ABuff_IncSums(id); BDraw_ABuff_Sums.SetData(); }
  public virtual void Gpu_BDraw_ABuff_IncFills1() { g_SetData(); BDraw_ABuff_Fills1?.SetCpu(); BDraw_ABuff_Fills1_SetKernels(); BDraw_ABuff_Fills2?.SetCpu(); BDraw_ABuff_Fills2_SetKernels(); Gpu(kernel_BDraw_ABuff_IncFills1, BDraw_ABuff_IncFills1, BDraw_ABuff_BitN1); BDraw_ABuff_Fills1?.ResetWrite(); }
  public virtual void Cpu_BDraw_ABuff_IncFills1() { BDraw_ABuff_Fills1?.GetGpu(); BDraw_ABuff_Fills2?.GetGpu(); Cpu(BDraw_ABuff_IncFills1, BDraw_ABuff_BitN1); BDraw_ABuff_Fills1.SetData(); }
  public virtual void Cpu_BDraw_ABuff_IncFills1(uint3 id) { BDraw_ABuff_Fills1?.GetGpu(); BDraw_ABuff_Fills2?.GetGpu(); BDraw_ABuff_IncFills1(id); BDraw_ABuff_Fills1.SetData(); }
  public virtual void Gpu_BDraw_ABuff_GetFills2() { g_SetData(); BDraw_ABuff_Fills1?.SetCpu(); BDraw_ABuff_Fills1_SetKernels(); BDraw_ABuff_Fills2_SetKernels(); Gpu(kernel_BDraw_ABuff_GetFills2, BDraw_ABuff_GetFills2, BDraw_ABuff_BitN2); BDraw_ABuff_Fills2?.ResetWrite(); }
  public virtual IEnumerator Cpu_BDraw_ABuff_GetFills2() { BDraw_ABuff_Fills1?.GetGpu(); BDraw_ABuff_Fills2?.GetGpu(); yield return StartCoroutine(Cpu_Coroutine(kernel_BDraw_ABuff_GetFills2, BDraw_ABuff_GetFills2, BDraw_ABuff_BitN2)); }
  public virtual void Cpu_BDraw_ABuff_GetFills2(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { BDraw_ABuff_Fills1?.GetGpu(); BDraw_ABuff_Fills2?.GetGpu(); BDraw_ABuff_GetFills2(grp_tid, grp_id, id, grpI); BDraw_ABuff_Fills2.SetData(); }
  public virtual void Gpu_BDraw_ABuff_GetFills1() { g_SetData(); BDraw_ABuff_Sums?.SetCpu(); BDraw_ABuff_Sums_SetKernels(); BDraw_ABuff_Fills1_SetKernels(); Gpu(kernel_BDraw_ABuff_GetFills1, BDraw_ABuff_GetFills1, BDraw_ABuff_BitN1); BDraw_ABuff_Fills1?.ResetWrite(); }
  public virtual IEnumerator Cpu_BDraw_ABuff_GetFills1() { BDraw_ABuff_Sums?.GetGpu(); BDraw_ABuff_Fills1?.GetGpu(); yield return StartCoroutine(Cpu_Coroutine(kernel_BDraw_ABuff_GetFills1, BDraw_ABuff_GetFills1, BDraw_ABuff_BitN1)); }
  public virtual void Cpu_BDraw_ABuff_GetFills1(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { BDraw_ABuff_Sums?.GetGpu(); BDraw_ABuff_Fills1?.GetGpu(); BDraw_ABuff_GetFills1(grp_tid, grp_id, id, grpI); BDraw_ABuff_Fills1.SetData(); }
  public virtual void Gpu_BDraw_ABuff_Get_Bits_Sums() { g_SetData(); BDraw_ABuff_Bits?.SetCpu(); BDraw_ABuff_Bits_SetKernels(); BDraw_ABuff_Sums_SetKernels(); Gpu(kernel_BDraw_ABuff_Get_Bits_Sums, BDraw_ABuff_Get_Bits_Sums, BDraw_ABuff_BitN); BDraw_ABuff_Sums?.ResetWrite(); }
  public virtual IEnumerator Cpu_BDraw_ABuff_Get_Bits_Sums() { BDraw_ABuff_Bits?.GetGpu(); BDraw_ABuff_Sums?.GetGpu(); yield return StartCoroutine(Cpu_Coroutine(kernel_BDraw_ABuff_Get_Bits_Sums, BDraw_ABuff_Get_Bits_Sums, BDraw_ABuff_BitN)); }
  public virtual void Cpu_BDraw_ABuff_Get_Bits_Sums(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { BDraw_ABuff_Bits?.GetGpu(); BDraw_ABuff_Sums?.GetGpu(); BDraw_ABuff_Get_Bits_Sums(grp_tid, grp_id, id, grpI); BDraw_ABuff_Sums.SetData(); }
  public virtual void Gpu_BDraw_ABuff_GetSums() { g_SetData(); BDraw_ABuff_Bits_SetKernels(); BDraw_ABuff_Sums_SetKernels(); BDraw_tab_delimeted_text?.SetCpu(); BDraw_tab_delimeted_text_SetKernels(); Gpu(kernel_BDraw_ABuff_GetSums, BDraw_ABuff_GetSums, BDraw_ABuff_BitN); BDraw_ABuff_Bits?.ResetWrite(); BDraw_ABuff_Sums?.ResetWrite(); }
  public virtual IEnumerator Cpu_BDraw_ABuff_GetSums() { BDraw_ABuff_Bits?.GetGpu(); BDraw_ABuff_Sums?.GetGpu(); BDraw_tab_delimeted_text?.GetGpu(); yield return StartCoroutine(Cpu_Coroutine(kernel_BDraw_ABuff_GetSums, BDraw_ABuff_GetSums, BDraw_ABuff_BitN)); }
  public virtual void Cpu_BDraw_ABuff_GetSums(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { BDraw_ABuff_Bits?.GetGpu(); BDraw_ABuff_Sums?.GetGpu(); BDraw_tab_delimeted_text?.GetGpu(); BDraw_ABuff_GetSums(grp_tid, grp_id, id, grpI); BDraw_ABuff_Bits.SetData(); BDraw_ABuff_Sums.SetData(); }
  public virtual void Gpu_BDraw_ABuff_Get_Bits() { g_SetData(); BDraw_ABuff_Bits_SetKernels(); BDraw_tab_delimeted_text?.SetCpu(); BDraw_tab_delimeted_text_SetKernels(); Gpu(kernel_BDraw_ABuff_Get_Bits, BDraw_ABuff_Get_Bits, BDraw_ABuff_BitN); BDraw_ABuff_Bits?.ResetWrite(); }
  public virtual void Cpu_BDraw_ABuff_Get_Bits() { BDraw_ABuff_Bits?.GetGpu(); BDraw_tab_delimeted_text?.GetGpu(); Cpu(BDraw_ABuff_Get_Bits, BDraw_ABuff_BitN); BDraw_ABuff_Bits.SetData(); }
  public virtual void Cpu_BDraw_ABuff_Get_Bits(uint3 id) { BDraw_ABuff_Bits?.GetGpu(); BDraw_tab_delimeted_text?.GetGpu(); BDraw_ABuff_Get_Bits(id); BDraw_ABuff_Bits.SetData(); }
  public virtual void Gpu_BDraw_setDefaultTextInfo() { g_SetData(); BDraw_textInfos?.SetCpu(); BDraw_textInfos_SetKernels(); Gpu(kernel_BDraw_setDefaultTextInfo, BDraw_setDefaultTextInfo, BDraw_textN); BDraw_textInfos?.ResetWrite(); }
  public virtual void Cpu_BDraw_setDefaultTextInfo() { BDraw_textInfos?.GetGpu(); Cpu(BDraw_setDefaultTextInfo, BDraw_textN); BDraw_textInfos.SetData(); }
  public virtual void Cpu_BDraw_setDefaultTextInfo(uint3 id) { BDraw_textInfos?.GetGpu(); BDraw_setDefaultTextInfo(id); BDraw_textInfos.SetData(); }
  public virtual void Gpu_BDraw_getTextInfo() { g_SetData(); BDraw_fontInfos?.SetCpu(); BDraw_fontInfos_SetKernels(); BDraw_textInfos?.SetCpu(); BDraw_textInfos_SetKernels(); BDraw_ABuff_Indexes?.SetCpu(); BDraw_ABuff_Indexes_SetKernels(); BDraw_tab_delimeted_text?.SetCpu(); BDraw_tab_delimeted_text_SetKernels(); Gpu(kernel_BDraw_getTextInfo, BDraw_getTextInfo, BDraw_textN); BDraw_textInfos?.ResetWrite(); }
  public virtual void Cpu_BDraw_getTextInfo() { BDraw_fontInfos?.GetGpu(); BDraw_textInfos?.GetGpu(); BDraw_ABuff_Indexes?.GetGpu(); BDraw_tab_delimeted_text?.GetGpu(); Cpu(BDraw_getTextInfo, BDraw_textN); BDraw_textInfos.SetData(); }
  public virtual void Cpu_BDraw_getTextInfo(uint3 id) { BDraw_fontInfos?.GetGpu(); BDraw_textInfos?.GetGpu(); BDraw_ABuff_Indexes?.GetGpu(); BDraw_tab_delimeted_text?.GetGpu(); BDraw_getTextInfo(id); BDraw_textInfos.SetData(); }
  public virtual void Gpu_Rand_grp_init_1M() { g_SetData(); Gpu(kernel_Rand_grp_init_1M, Rand_grp_init_1M, Rand_N / 1024 / 1024); }
  public virtual IEnumerator Cpu_Rand_grp_init_1M() { yield return StartCoroutine(Cpu_Coroutine(kernel_Rand_grp_init_1M, Rand_grp_init_1M, Rand_N / 1024 / 1024)); }
  public virtual void Cpu_Rand_grp_init_1M(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { Rand_grp_init_1M(grp_tid, grp_id, id, grpI); }
  public virtual void Gpu_Rand_grp_init_1K() { g_SetData(); Gpu(kernel_Rand_grp_init_1K, Rand_grp_init_1K, Rand_N / 1024); }
  public virtual IEnumerator Cpu_Rand_grp_init_1K() { yield return StartCoroutine(Cpu_Coroutine(kernel_Rand_grp_init_1K, Rand_grp_init_1K, Rand_N / 1024)); }
  public virtual void Cpu_Rand_grp_init_1K(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { Rand_grp_init_1K(grp_tid, grp_id, id, grpI); }
  public virtual void Gpu_Rand_grp_fill_1K() { g_SetData(); Gpu(kernel_Rand_grp_fill_1K, Rand_grp_fill_1K, Rand_N); }
  public virtual IEnumerator Cpu_Rand_grp_fill_1K() { yield return StartCoroutine(Cpu_Coroutine(kernel_Rand_grp_fill_1K, Rand_grp_fill_1K, Rand_N)); }
  public virtual void Cpu_Rand_grp_fill_1K(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { Rand_grp_fill_1K(grp_tid, grp_id, id, grpI); }
  [JsonConverter(typeof(StringEnumConverter))] public enum BenchmarkType : uint { Nanosec, TFlops }
  [JsonConverter(typeof(StringEnumConverter))] public enum BDraw_Draw : uint { Point, Sphere, Line, Arrow, Signal, LineSegment, Texture_2D, Quad, WebCam, Mesh, Number, N }
  [JsonConverter(typeof(StringEnumConverter))] public enum BDraw_TextAlignment : uint { BottomLeft, CenterLeft, TopLeft, BottomCenter, CenterCenter, TopCenter, BottomRight, CenterRight, TopRight }
  [JsonConverter(typeof(StringEnumConverter))] public enum BDraw_Text_QuadType : uint { FrontOnly, FrontBack, Switch, Arrow, Billboard }
  public const uint BenchmarkType_Nanosec = 0, BenchmarkType_TFlops = 1;
  public const uint BDraw_Draw_Point = 0, BDraw_Draw_Sphere = 1, BDraw_Draw_Line = 2, BDraw_Draw_Arrow = 3, BDraw_Draw_Signal = 4, BDraw_Draw_LineSegment = 5, BDraw_Draw_Texture_2D = 6, BDraw_Draw_Quad = 7, BDraw_Draw_WebCam = 8, BDraw_Draw_Mesh = 9, BDraw_Draw_Number = 10, BDraw_Draw_N = 11;
  public const uint BDraw_TextAlignment_BottomLeft = 0, BDraw_TextAlignment_CenterLeft = 1, BDraw_TextAlignment_TopLeft = 2, BDraw_TextAlignment_BottomCenter = 3, BDraw_TextAlignment_CenterCenter = 4, BDraw_TextAlignment_TopCenter = 5, BDraw_TextAlignment_BottomRight = 6, BDraw_TextAlignment_CenterRight = 7, BDraw_TextAlignment_TopRight = 8;
  public const uint BDraw_Text_QuadType_FrontOnly = 0, BDraw_Text_QuadType_FrontBack = 1, BDraw_Text_QuadType_Switch = 2, BDraw_Text_QuadType_Arrow = 3, BDraw_Text_QuadType_Billboard = 4;
  public const uint BDraw_Draw_Text3D = 12, BDraw_LF = 10, BDraw_TB = 9, BDraw_ZERO = 48, BDraw_NINE = 57, BDraw_PERIOD = 46, BDraw_COMMA = 44, BDraw_PLUS = 43, BDraw_MINUS = 45, BDraw_SPACE = 32;
  [HideInInspector] public bool ValuesChanged, gChanged;
  public static gsSort This;
  public virtual void Awake() { This = this as gsSort; Awake_GS(); }
  public virtual void Awake_GS()
  {
    if(!GS_OCam_Lib.CheckKey("you@gmail.com", "2024/12/10", 123456)) Status($"OCam_Lib not registered, check email, expiration, and key in gsSort_GS class");
    if(!GS_Views_Lib.CheckKey("you@gmail.com", "2024/12/10", 123456)) Status($"Views_Lib not registered, check email, expiration, and key in gsSort_GS class");
    if(!GS_Report_Lib.CheckKey("you@gmail.com", "2024/12/10", 123456)) Status($"Report_Lib not registered, check email, expiration, and key in gsSort_GS class");
  }
  public virtual void Start() { Build_UI(); Load_UI(); Start0_GS(); InitBuffers(); Start1_GS(); }
  public virtual void Start0_GS()
  {
    BDraw_Start0_GS();
    Rand_Start0_GS();
  }
  public virtual void Start1_GS()
  {
    BDraw_Start1_GS();
    Rand_Start1_GS();
  }
  public override void onLoaded()
  {
    base.onLoaded();
    GS_OCam_Lib.onLoaded(OCam_Lib);
    GS_Views_Lib.onLoaded(Views_Lib);
    GS_Report_Lib.onLoaded(Report_Lib);
  }
  [HideInInspector] public bool already_quited = false;
  public override void OnApplicationQuit() { Save_UI(); OnApplicationQuit_GS(); base.OnApplicationQuit(); already_quited = true; }
  public virtual void OnApplicationQuit_GS()
  {
    BDraw_OnApplicationQuit_GS();
    Rand_OnApplicationQuit_GS();
  }
  public override void ui_to_data()
  {
    if (data == null) return;
    data.siUnits = siUnits;
    data.group_UI = group_UI;
    data.group_Sort = group_Sort;
    data.useUpperTriangular = useUpperTriangular;
    data.arrayLength = arrayLength;
    data.numberOfArrays = numberOfArrays;
    data.runtimeN = runtimeN;
    data.sort_runtime = sort_runtime;
    data.node_size = node_size;
    data.benchmarkType = benchmarkType;
  }
  public override void data_to_ui()
  {
    if (data == null) return;
    group_UI = data.group_UI;
    group_Sort = data.group_Sort;
    useUpperTriangular = data.useUpperTriangular;
    arrayLength = ui_txt_str.Contains("\"arrayLength\"") ? data.arrayLength : 16;
    numberOfArrays = ui_txt_str.Contains("\"numberOfArrays\"") ? data.numberOfArrays : 1;
    runtimeN = ui_txt_str.Contains("\"runtimeN\"") ? data.runtimeN : 1;
    sort_runtime = data.sort_runtime;
    node_size = ui_txt_str.Contains("\"node_size\"") ? data.node_size : 40f;
    benchmarkType = data.benchmarkType;
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
    if (UI_useUpperTriangular.Changed || useUpperTriangular != UI_useUpperTriangular.v) useUpperTriangular = UI_useUpperTriangular.v;
    if (UI_arrayLength.Changed || arrayLength != UI_arrayLength.v) arrayLength = UI_arrayLength.v;
    if (UI_numberOfArrays.Changed || numberOfArrays != UI_numberOfArrays.v) numberOfArrays = UI_numberOfArrays.v;
    if (UI_runtimeN.Changed || runtimeN != UI_runtimeN.v) runtimeN = UI_runtimeN.v;
    if (UI_sort_runtime.Changed || sort_runtime != UI_sort_runtime.v) sort_runtime = UI_sort_runtime.v;
    if (UI_node_size.Changed || node_size != UI_node_size.v) node_size = UI_node_size.v;
    if (UI_benchmarkType.Changed || (uint)benchmarkType != UI_benchmarkType.v) benchmarkType = (BenchmarkType)UI_benchmarkType.v;
    if (ValuesChanged) { gChanged = true; OnValueChanged(); ValuesChanged = UI_group_UI.Changed = UI_group_Sort.Changed = UI_useUpperTriangular.Changed = UI_arrayLength.Changed = UI_numberOfArrays.Changed = UI_runtimeN.Changed = UI_sort_runtime.Changed = UI_node_size.Changed = UI_benchmarkType.Changed = false; }
    BDraw_LateUpdate1_GS();
    Rand_LateUpdate1_GS();
    LateUpdate1_GS();
    if(lateUpdateI++ == uint_max) lateUpdateI = 100;
  }
  public virtual void LateUpdate0_GS()
  {
    BDraw_LateUpdate0_GS();
    Rand_LateUpdate0_GS();
  }
  public virtual void LateUpdate1_GS()
  {
    BDraw_LateUpdate1_GS();
    Rand_LateUpdate1_GS();
  }
  public override void Update()
  {
    base.Update();
    if (!ui_loaded) return;
    Update0_GS();
    BDraw_Update1_GS();
    Rand_Update1_GS();
    Update1_GS();
  }
  public virtual void Update0_GS()
  {
    BDraw_Update0_GS();
    Rand_Update0_GS();
  }
  public virtual void Update1_GS()
  {
    BDraw_Update1_GS();
    Rand_Update1_GS();
  }
  public override void OnValueChanged()
  {
    if (!ui_loaded) return;
    OnValueChanged_GS();
  }
  public override void OnValueChanged_GS()
  {
    BDraw_OnValueChanged_GS();
    Rand_OnValueChanged_GS();
    var type = "gsReport_Lib".ToType();
    if (type != null) ((GS)GetComponent(type))?.OnValueChanged_GS();

  }
  public string[] Scenes_in_Build => new string[] { "Assets/GS_Docs/gsADraw_Doc/ADraw_Doc.unity" };
  public virtual bool Show_vert_vs0 { get => node_size > 1; }
  public virtual bool Show_vert_vs1 { get => node_size > 1; }
  public virtual bool Show_vert_vs_arrows { get => node_size > 1; }
  public virtual bool useUpperTriangular { get => Is(g.useUpperTriangular); set { if (g.useUpperTriangular != Is(value) || UI_useUpperTriangular.v != value) { g.useUpperTriangular = Is(UI_useUpperTriangular.v = value); ValuesChanged = gChanged = true; } } }
  public virtual uint arrayLength { get => g.arrayLength; set { if (g.arrayLength != value || UI_arrayLength.v != value) { g.arrayLength = UI_arrayLength.v = value; ValuesChanged = gChanged = true; } } }
  public virtual uint numberOfArrays { get => g.numberOfArrays; set { if (g.numberOfArrays != value || UI_numberOfArrays.v != value) { g.numberOfArrays = UI_numberOfArrays.v = value; ValuesChanged = gChanged = true; } } }
  public virtual uint runtimeN { get => g.runtimeN; set { if (g.runtimeN != value || UI_runtimeN.v != value) { g.runtimeN = UI_runtimeN.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float sort_runtime { get => g.sort_runtime; set { if (any(g.sort_runtime != value) || any(UI_sort_runtime.v != value)) { g.sort_runtime = UI_sort_runtime.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float node_size { get => g.node_size; set { if (any(g.node_size != value) || any(UI_node_size.v != value)) { g.node_size = UI_node_size.v = value; ValuesChanged = gChanged = true; } } }
  public virtual BenchmarkType benchmarkType { get => (BenchmarkType)g.benchmarkType; set { if ((BenchmarkType)g.benchmarkType != value || (BenchmarkType)UI_benchmarkType.v != value) { g.benchmarkType = UI_benchmarkType.v = (uint)value; ValuesChanged = gChanged = true; } } }
  public virtual uint vsN { get => g.vsN; set { if (g.vsN != value) { g.vsN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint BDraw_ABuff_IndexN { get => g.BDraw_ABuff_IndexN; set { if (g.BDraw_ABuff_IndexN != value) { g.BDraw_ABuff_IndexN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint BDraw_ABuff_BitN { get => g.BDraw_ABuff_BitN; set { if (g.BDraw_ABuff_BitN != value) { g.BDraw_ABuff_BitN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint BDraw_ABuff_N { get => g.BDraw_ABuff_N; set { if (g.BDraw_ABuff_N != value) { g.BDraw_ABuff_N = value; ValuesChanged = gChanged = true; } } }
  public virtual uint BDraw_ABuff_BitN1 { get => g.BDraw_ABuff_BitN1; set { if (g.BDraw_ABuff_BitN1 != value) { g.BDraw_ABuff_BitN1 = value; ValuesChanged = gChanged = true; } } }
  public virtual uint BDraw_ABuff_BitN2 { get => g.BDraw_ABuff_BitN2; set { if (g.BDraw_ABuff_BitN2 != value) { g.BDraw_ABuff_BitN2 = value; ValuesChanged = gChanged = true; } } }
  public virtual bool BDraw_omitText { get => Is(g.BDraw_omitText); set { if (g.BDraw_omitText != Is(value)) { g.BDraw_omitText = Is(value); ValuesChanged = gChanged = true; } } }
  public virtual bool BDraw_includeUnicode { get => Is(g.BDraw_includeUnicode); set { if (g.BDraw_includeUnicode != Is(value)) { g.BDraw_includeUnicode = Is(value); ValuesChanged = gChanged = true; } } }
  public virtual uint BDraw_fontInfoN { get => g.BDraw_fontInfoN; set { if (g.BDraw_fontInfoN != value) { g.BDraw_fontInfoN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint BDraw_textN { get => g.BDraw_textN; set { if (g.BDraw_textN != value) { g.BDraw_textN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint BDraw_textCharN { get => g.BDraw_textCharN; set { if (g.BDraw_textCharN != value) { g.BDraw_textCharN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint BDraw_boxEdgeN { get => g.BDraw_boxEdgeN; set { if (g.BDraw_boxEdgeN != value) { g.BDraw_boxEdgeN = value; ValuesChanged = gChanged = true; } } }
  public virtual float BDraw_fontSize { get => g.BDraw_fontSize; set { if (any(g.BDraw_fontSize != value)) { g.BDraw_fontSize = value; ValuesChanged = gChanged = true; } } }
  public virtual float BDraw_boxThickness { get => g.BDraw_boxThickness; set { if (any(g.BDraw_boxThickness != value)) { g.BDraw_boxThickness = value; ValuesChanged = gChanged = true; } } }
  public virtual float4 BDraw_boxColor { get => g.BDraw_boxColor; set { if (any(g.BDraw_boxColor != value)) { g.BDraw_boxColor = value; ValuesChanged = gChanged = true; } } }
  public virtual uint Rand_N { get => g.Rand_N; set { if (g.Rand_N != value) { g.Rand_N = value; ValuesChanged = gChanged = true; } } }
  public virtual uint Rand_I { get => g.Rand_I; set { if (g.Rand_I != value) { g.Rand_I = value; ValuesChanged = gChanged = true; } } }
  public virtual uint Rand_J { get => g.Rand_J; set { if (g.Rand_J != value) { g.Rand_J = value; ValuesChanged = gChanged = true; } } }
  public virtual uint4 Rand_seed4 { get => g.Rand_seed4; set { if (any(g.Rand_seed4 != value)) { g.Rand_seed4 = value; ValuesChanged = gChanged = true; } } }
  public bool group_UI { get => UI_group_UI?.v ?? false; set { if (UI_group_UI != null) UI_group_UI.v = value; } }
  public bool group_Sort { get => UI_group_Sort?.v ?? false; set { if (UI_group_Sort != null) UI_group_Sort.v = value; } }
  public UI_TreeGroup UI_group_UI, UI_group_Sort;
  public UI_bool UI_useUpperTriangular;
  public UI_uint UI_arrayLength, UI_numberOfArrays, UI_runtimeN;
  public UI_method UI_RunSort;
  public virtual void RunSort() { }
  public UI_float UI_sort_runtime, UI_node_size;
  public UI_enum UI_benchmarkType;
  public UI_method UI_RunBenchmark;
  [HideInInspector] public bool in_RunBenchmark = false; public IEnumerator RunBenchmark() { if (in_RunBenchmark) { in_RunBenchmark = false; yield break; } in_RunBenchmark = true; yield return StartCoroutine(RunBenchmark_Sync()); in_RunBenchmark = false; }
  public virtual IEnumerator RunBenchmark_Sync() { yield return null; }
  public UI_TreeGroup ui_group_UI => UI_group_UI;
  public UI_TreeGroup ui_group_Sort => UI_group_Sort;
  public UI_bool ui_useUpperTriangular => UI_useUpperTriangular;
  public UI_uint ui_arrayLength => UI_arrayLength;
  public UI_uint ui_numberOfArrays => UI_numberOfArrays;
  public UI_uint ui_runtimeN => UI_runtimeN;
  public UI_float ui_sort_runtime => UI_sort_runtime;
  public UI_float ui_node_size => UI_node_size;
  public UI_enum ui_benchmarkType => UI_benchmarkType;
  [Serializable]
  public class uiData
  {
    public bool siUnits, group_UI, group_Sort, useUpperTriangular;
    public uint arrayLength, numberOfArrays, runtimeN;
    public float sort_runtime, node_size;
    [JsonConverter(typeof(StringEnumConverter))] public BenchmarkType benchmarkType;
  }
  public virtual void InitBuffers()
  {
    InitBuffers0_GS();
    AddComputeBuffer(ref gSort, nameof(gSort), 1);
    InitKernels();
    SetKernelValues(gSort, nameof(gSort), kernel_set_sorts, kernel_add_counts, kernel_add_counts_triangle, kernel_init_counts, kernel_init_vs, kernel_Rand_initState, kernel_Rand_initSeed, kernel_BDraw_ABuff_GetIndexes, kernel_BDraw_ABuff_IncSums, kernel_BDraw_ABuff_IncFills1, kernel_BDraw_ABuff_GetFills2, kernel_BDraw_ABuff_GetFills1, kernel_BDraw_ABuff_Get_Bits_Sums, kernel_BDraw_ABuff_GetSums, kernel_BDraw_ABuff_Get_Bits, kernel_BDraw_setDefaultTextInfo);
    SetKernelValues(gSort, nameof(gSort), kernel_BDraw_getTextInfo, kernel_Rand_grp_init_1M, kernel_Rand_grp_init_1K, kernel_Rand_grp_fill_1K);
    AddComputeBuffer(ref vs, nameof(vs), vsN);
    AddComputeBuffer(ref counts, nameof(counts), vsN);
    AddComputeBuffer(ref sorts, nameof(sorts), vsN);
    AddComputeBuffer(ref BDraw_tab_delimeted_text, nameof(BDraw_tab_delimeted_text), BDraw_textN);
    AddComputeBuffer(ref BDraw_textInfos, nameof(BDraw_textInfos), BDraw_textN);
    AddComputeBuffer(ref BDraw_fontInfos, nameof(BDraw_fontInfos), BDraw_fontInfoN);
    AddComputeBuffer(ref Rand_rs, nameof(Rand_rs), Rand_N);
    InitBuffers1_GS();
    isInitBuffers = true;
  }
  public virtual void InitBuffers0_GS()
  {
    BDraw_InitBuffers0_GS();
    Rand_InitBuffers0_GS();
  }
  public virtual void InitBuffers1_GS()
  {
    BDraw_InitBuffers1_GS();
    Rand_InitBuffers1_GS();
  }
  [HideInInspector] public uint4[] Rand_grp = new uint4[1024];
  [HideInInspector] public uint[] BDraw_ABuff_grp = new uint[1024];
  [HideInInspector] public uint[] BDraw_ABuff_grp0 = new uint[1024];
  [Serializable]
  public struct GSort
  {
    public uint useUpperTriangular, arrayLength, numberOfArrays, runtimeN, benchmarkType, vsN, BDraw_ABuff_IndexN, BDraw_ABuff_BitN, BDraw_ABuff_N, BDraw_ABuff_BitN1, BDraw_ABuff_BitN2, BDraw_omitText, BDraw_includeUnicode, BDraw_fontInfoN, BDraw_textN, BDraw_textCharN, BDraw_boxEdgeN, Rand_N, Rand_I, Rand_J;
    public float sort_runtime, node_size, BDraw_fontSize, BDraw_boxThickness;
    public float4 BDraw_boxColor;
    public uint4 Rand_seed4;
  };
  public RWStructuredBuffer<GSort> gSort;
  public struct BDraw_FontInfo { public float2 uvBottomLeft, uvBottomRight, uvTopLeft, uvTopRight; public int advance, bearing, minX, minY, maxX, maxY; };
  public struct BDraw_TextInfo { public float3 p, right, up, p0, p1; public float2 size, uvSize; public float4 color, backColor; public uint justification, textI, quadType, axis; public float height; };
  public RWStructuredBuffer<float> vs;
  public RWStructuredBuffer<uint> counts, sorts, BDraw_tab_delimeted_text, BDraw_ABuff_Bits, BDraw_ABuff_Sums, BDraw_ABuff_Indexes, BDraw_ABuff_Fills1, BDraw_ABuff_Fills2;
  public RWStructuredBuffer<BDraw_TextInfo> BDraw_textInfos;
  public RWStructuredBuffer<BDraw_FontInfo> BDraw_fontInfos;
  public RWStructuredBuffer<uint4> Rand_rs;
  public virtual void AllocData_vs(uint n) => AddComputeBuffer(ref vs, nameof(vs), n);
  public virtual void AssignData_vs(params float[] data) => AddComputeBufferData(ref vs, nameof(vs), data);
  public virtual void AllocData_counts(uint n) => AddComputeBuffer(ref counts, nameof(counts), n);
  public virtual void AssignData_counts(params uint[] data) => AddComputeBufferData(ref counts, nameof(counts), data);
  public virtual void AllocData_sorts(uint n) => AddComputeBuffer(ref sorts, nameof(sorts), n);
  public virtual void AssignData_sorts(params uint[] data) => AddComputeBufferData(ref sorts, nameof(sorts), data);
  public virtual void AllocData_BDraw_tab_delimeted_text(uint n) => AddComputeBuffer(ref BDraw_tab_delimeted_text, nameof(BDraw_tab_delimeted_text), n);
  public virtual void AssignData_BDraw_tab_delimeted_text(params uint[] data) => AddComputeBufferData(ref BDraw_tab_delimeted_text, nameof(BDraw_tab_delimeted_text), data);
  public virtual void AllocData_BDraw_textInfos(uint n) => AddComputeBuffer(ref BDraw_textInfos, nameof(BDraw_textInfos), n);
  public virtual void AssignData_BDraw_textInfos(params BDraw_TextInfo[] data) => AddComputeBufferData(ref BDraw_textInfos, nameof(BDraw_textInfos), data);
  public virtual void AllocData_BDraw_fontInfos(uint n) => AddComputeBuffer(ref BDraw_fontInfos, nameof(BDraw_fontInfos), n);
  public virtual void AssignData_BDraw_fontInfos(params BDraw_FontInfo[] data) => AddComputeBufferData(ref BDraw_fontInfos, nameof(BDraw_fontInfos), data);
  public virtual void AllocData_Rand_rs(uint n) => AddComputeBuffer(ref Rand_rs, nameof(Rand_rs), n);
  public virtual void AssignData_Rand_rs(params uint4[] data) => AddComputeBufferData(ref Rand_rs, nameof(Rand_rs), data);
  public virtual void AllocData_BDraw_ABuff_Bits(uint n) => AddComputeBuffer(ref BDraw_ABuff_Bits, nameof(BDraw_ABuff_Bits), n);
  public virtual void AssignData_BDraw_ABuff_Bits(params uint[] data) => AddComputeBufferData(ref BDraw_ABuff_Bits, nameof(BDraw_ABuff_Bits), data);
  public virtual void AllocData_BDraw_ABuff_Sums(uint n) => AddComputeBuffer(ref BDraw_ABuff_Sums, nameof(BDraw_ABuff_Sums), n);
  public virtual void AssignData_BDraw_ABuff_Sums(params uint[] data) => AddComputeBufferData(ref BDraw_ABuff_Sums, nameof(BDraw_ABuff_Sums), data);
  public virtual void AllocData_BDraw_ABuff_Indexes(uint n) => AddComputeBuffer(ref BDraw_ABuff_Indexes, nameof(BDraw_ABuff_Indexes), n);
  public virtual void AssignData_BDraw_ABuff_Indexes(params uint[] data) => AddComputeBufferData(ref BDraw_ABuff_Indexes, nameof(BDraw_ABuff_Indexes), data);
  public virtual void AllocData_BDraw_ABuff_Fills1(uint n) => AddComputeBuffer(ref BDraw_ABuff_Fills1, nameof(BDraw_ABuff_Fills1), n);
  public virtual void AssignData_BDraw_ABuff_Fills1(params uint[] data) => AddComputeBufferData(ref BDraw_ABuff_Fills1, nameof(BDraw_ABuff_Fills1), data);
  public virtual void AllocData_BDraw_ABuff_Fills2(uint n) => AddComputeBuffer(ref BDraw_ABuff_Fills2, nameof(BDraw_ABuff_Fills2), n);
  public virtual void AssignData_BDraw_ABuff_Fills2(params uint[] data) => AddComputeBufferData(ref BDraw_ABuff_Fills2, nameof(BDraw_ABuff_Fills2), data);
  public Texture2D _PaletteTex;
  public float4 palette(float v) => paletteColor(_PaletteTex, v);
  public float4 palette(float v, float w) => float4(palette(v).xyz, w);

  public Texture2D BDraw_fontTexture;
  public virtual void onRenderObject_LIN(bool show, uint _itemN, ref uint i, ref uint index, ref uint3 LIN) { uint n = 0; if (show) { if (i < (n = _itemN)) LIN = uint3(index, i, 0); LIN.z += n; i -= n; } index++; }
  public virtual void onRenderObject_LIN(uint _itemN, ref uint i, ref uint index, ref uint3 LIN) { onRenderObject_LIN(true, _itemN, ref i, ref index, ref LIN); }
  public virtual uint3 onRenderObject_LIN(uint i) { uint3 LIN = u000; uint index = 0; onRenderObject_LIN(node_size > 1, vsN, ref i, ref index, ref LIN); onRenderObject_LIN(node_size > 1, vsN, ref i, ref index, ref LIN); onRenderObject_LIN(node_size > 1, vsN, ref i, ref index, ref LIN); onRenderObject_LIN(BDraw_textN, ref i, ref index, ref LIN); onRenderObject_LIN(BDraw_boxEdgeN, ref i, ref index, ref LIN); return LIN; }
  public virtual uint3 onRenderObject_LIN_Quads(uint i) { uint3 LIN = u000; uint index = 0; onRenderObject_LIN(node_size > 1, vsN, ref i, ref index, ref LIN); onRenderObject_LIN(node_size > 1, vsN, ref i, ref index, ref LIN); onRenderObject_LIN(node_size > 1, vsN, ref i, ref index, ref LIN); onRenderObject_LIN(BDraw_textN, ref i, ref index, ref LIN); onRenderObject_LIN(BDraw_boxEdgeN, ref i, ref index, ref LIN); return LIN; }
  public virtual uint3 onRenderObject_LIN_Points(uint i) { uint3 LIN = u000; return LIN; }
  [HideInInspector] public int kernel_set_sorts; [numthreads(numthreads2, numthreads2, 1)] protected void set_sorts(uint3 id) { unchecked { if (id.y < arrayLength && id.x < numberOfArrays) set_sorts_GS(id); } }
  public virtual void set_sorts_GS(uint3 id) { }
  [HideInInspector] public int kernel_add_counts; [numthreads(numthreads3, numthreads3, numthreads3)] protected void add_counts(uint3 id) { unchecked { if (id.z < arrayLength && id.y < arrayLength && id.x < numberOfArrays) add_counts_GS(id); } }
  public virtual void add_counts_GS(uint3 id) { }
  [HideInInspector] public int kernel_add_counts_triangle; [numthreads(numthreads2, numthreads2, 1)] protected void add_counts_triangle(uint3 id) { unchecked { if (id.y < arrayLength * (arrayLength - 1) / 2 && id.x < numberOfArrays) add_counts_triangle_GS(id); } }
  public virtual void add_counts_triangle_GS(uint3 id) { }
  [HideInInspector] public int kernel_init_counts; [numthreads(numthreads1, 1, 1)] protected void init_counts(uint3 id) { unchecked { if (id.x < vsN) init_counts_GS(id); } }
  public virtual void init_counts_GS(uint3 id) { }
  [HideInInspector] public int kernel_init_vs; [numthreads(numthreads1, 1, 1)] protected void init_vs(uint3 id) { unchecked { if (id.x < vsN) init_vs_GS(id); } }
  public virtual void init_vs_GS(uint3 id) { }
  [HideInInspector] public int kernel_Rand_initState; [numthreads(numthreads1, 1, 1)] protected void Rand_initState(uint3 id) { unchecked { if (id.x < Rand_I) Rand_initState_GS(id); } }
  [HideInInspector] public int kernel_Rand_initSeed; [numthreads(numthreads1, 1, 1)] protected void Rand_initSeed(uint3 id) { unchecked { if (id.x < Rand_N) Rand_initSeed_GS(id); } }
  [HideInInspector] public int kernel_BDraw_ABuff_GetIndexes; [numthreads(numthreads1, 1, 1)] protected void BDraw_ABuff_GetIndexes(uint3 id) { unchecked { if (id.x < BDraw_ABuff_BitN) BDraw_ABuff_GetIndexes_GS(id); } }
  [HideInInspector] public int kernel_BDraw_ABuff_IncSums; [numthreads(numthreads1, 1, 1)] protected void BDraw_ABuff_IncSums(uint3 id) { unchecked { if (id.x < BDraw_ABuff_BitN) BDraw_ABuff_IncSums_GS(id); } }
  [HideInInspector] public int kernel_BDraw_ABuff_IncFills1; [numthreads(numthreads1, 1, 1)] protected void BDraw_ABuff_IncFills1(uint3 id) { unchecked { if (id.x < BDraw_ABuff_BitN1) BDraw_ABuff_IncFills1_GS(id); } }
  [HideInInspector] public int kernel_BDraw_ABuff_GetFills2; [numthreads(numthreads1, 1, 1)] protected IEnumerator BDraw_ABuff_GetFills2(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < BDraw_ABuff_BitN2) yield return StartCoroutine(BDraw_ABuff_GetFills2_GS(grp_tid, grp_id, id, grpI)); } }
  [HideInInspector] public int kernel_BDraw_ABuff_GetFills1; [numthreads(numthreads1, 1, 1)] protected IEnumerator BDraw_ABuff_GetFills1(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < BDraw_ABuff_BitN1) yield return StartCoroutine(BDraw_ABuff_GetFills1_GS(grp_tid, grp_id, id, grpI)); } }
  [HideInInspector] public int kernel_BDraw_ABuff_Get_Bits_Sums; [numthreads(numthreads1, 1, 1)] protected IEnumerator BDraw_ABuff_Get_Bits_Sums(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < BDraw_ABuff_BitN) yield return StartCoroutine(BDraw_ABuff_Get_Bits_Sums_GS(grp_tid, grp_id, id, grpI)); } }
  [HideInInspector] public int kernel_BDraw_ABuff_GetSums; [numthreads(numthreads1, 1, 1)] protected IEnumerator BDraw_ABuff_GetSums(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < BDraw_ABuff_BitN) yield return StartCoroutine(BDraw_ABuff_GetSums_GS(grp_tid, grp_id, id, grpI)); } }
  [HideInInspector] public int kernel_BDraw_ABuff_Get_Bits; [numthreads(numthreads1, 1, 1)] protected void BDraw_ABuff_Get_Bits(uint3 id) { unchecked { if (id.x < BDraw_ABuff_BitN) BDraw_ABuff_Get_Bits_GS(id); } }
  [HideInInspector] public int kernel_BDraw_setDefaultTextInfo; [numthreads(numthreads1, 1, 1)] protected void BDraw_setDefaultTextInfo(uint3 id) { unchecked { if (id.x < BDraw_textN) BDraw_setDefaultTextInfo_GS(id); } }
  [HideInInspector] public int kernel_BDraw_getTextInfo; [numthreads(numthreads1, 1, 1)] protected void BDraw_getTextInfo(uint3 id) { unchecked { if (id.x < BDraw_textN) BDraw_getTextInfo_GS(id); } }
  [HideInInspector] public int kernel_Rand_grp_init_1M; [numthreads(numthreads1, 1, 1)] protected IEnumerator Rand_grp_init_1M(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < Rand_N / 1024 / 1024) yield return StartCoroutine(Rand_grp_init_1M_GS(grp_tid, grp_id, id, grpI)); } }
  public virtual IEnumerator Rand_grp_init_1M_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { yield return null; }
  [HideInInspector] public int kernel_Rand_grp_init_1K; [numthreads(numthreads1, 1, 1)] protected IEnumerator Rand_grp_init_1K(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < Rand_N / 1024) yield return StartCoroutine(Rand_grp_init_1K_GS(grp_tid, grp_id, id, grpI)); } }
  public virtual IEnumerator Rand_grp_init_1K_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { yield return null; }
  [HideInInspector] public int kernel_Rand_grp_fill_1K; [numthreads(numthreads1, 1, 1)] protected IEnumerator Rand_grp_fill_1K(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < Rand_N) yield return StartCoroutine(Rand_grp_fill_1K_GS(grp_tid, grp_id, id, grpI)); } }
  public virtual IEnumerator Rand_grp_fill_1K_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { yield return null; }
  public virtual v2f vert_GS(uint i, uint j, v2f o)
  {
    uint3 LIN = onRenderObject_LIN(i); int index = -1, level = ((int)LIN.x); i = LIN.y;
    if (level == ++index) { o = vert_vs0(i, j, o); o.tj.x = 0; }
    else if (level == ++index) { o = vert_vs1(i, j, o); o.tj.x = 0; }
    else if (level == ++index) { o = vert_vs_arrows(i, j, o); o.tj.x = 0; }
    else if (level == ++index) { o = vert_BDraw_Text(i, j, o); o.tj.x = 3; }
    else if (level == ++index) { o = vert_BDraw_Box(i, j, o); o.tj.x = 3; }
    return o;
  }
  public v2f vert(uint i, uint j)
  {
    v2f o = default;
    o.pos = o.color = o.tj = o.tk = f0000; o.normal = o.p0 = o.p1 = o.wPos = f000; o.uv = f00; o.ti = float4(i, j, 0, 0);
    return vert_GS(i, j, o);
  }
  public virtual v2f vert_vs0(uint i, uint j, v2f o) => o;
  public virtual v2f vert_vs1(uint i, uint j, v2f o) => o;
  public virtual v2f vert_vs_arrows(uint i, uint j, v2f o) => o;
  public virtual bool onRenderObject_GS_N(bool cpu)
  {
    RenderQuads(material, onRenderObject_LIN_Quads(0).z);
    RenderPoints(material, onRenderObject_LIN_Points(0).z);
    return true;
  }
  public override bool onRenderObject()
  {
    if (gSort == null) return false;
    bool render = true, cpu = false;
    onRenderObject_GS(ref render, ref cpu);
    g_SetData();
    if (!render) return false;
    if (cpu) Cpu(material, new { gSort }, new { vs }, new { counts }, new { sorts }, new { BDraw_tab_delimeted_text }, new { BDraw_textInfos }, new { BDraw_fontInfos }, new { Rand_rs }, new { Rand_grp }, new { BDraw_ABuff_Bits }, new { BDraw_ABuff_Sums }, new { BDraw_ABuff_Indexes }, new { BDraw_ABuff_grp }, new { BDraw_ABuff_grp0 }, new { BDraw_ABuff_Fills1 }, new { BDraw_ABuff_Fills2 }, new { BDraw_fontTexture }, new { _PaletteTex });
    else Gpu(material, new { gSort }, new { vs }, new { counts }, new { sorts }, new { BDraw_tab_delimeted_text }, new { BDraw_textInfos }, new { BDraw_fontInfos }, new { Rand_rs }, new { Rand_grp }, new { BDraw_ABuff_Bits }, new { BDraw_ABuff_Sums }, new { BDraw_ABuff_Indexes }, new { BDraw_ABuff_grp }, new { BDraw_ABuff_grp0 }, new { BDraw_ABuff_Fills1 }, new { BDraw_ABuff_Fills2 }, new { BDraw_fontTexture }, new { _PaletteTex });
    return onRenderObject_GS_N(cpu);
  }
  public virtual void onRenderObject_GS(ref bool render, ref bool cpu)
  {
    BDraw_onRenderObject_GS(ref render, ref cpu);
    Rand_onRenderObject_GS(ref render, ref cpu);
  }
  public virtual float4 frag_GS(v2f i, float4 color) => frag_BDraw_GS(i, color);
  public virtual float4 frag(v2f i)
  {
    float4 color = i.color;
    color = frag_GS(i, color);
    if (color.a == 0) Discard(0);
    return color;
  }
  #region <OCam_Lib>
  gsOCam_Lib _OCam_Lib; public gsOCam_Lib OCam_Lib => _OCam_Lib = _OCam_Lib ?? Add_Component_to_gameObject<gsOCam_Lib>();
  #endregion <OCam_Lib>
  #region <Views_Lib>
  gsViews_Lib _Views_Lib; public gsViews_Lib Views_Lib => _Views_Lib = _Views_Lib ?? Add_Component_to_gameObject<gsViews_Lib>();
  #endregion <Views_Lib>
  #region <Report_Lib>
  gsReport_Lib _Report_Lib; public gsReport_Lib Report_Lib => _Report_Lib = _Report_Lib ?? Add_Component_to_gameObject<gsReport_Lib>();
  #endregion <Report_Lib>
  #region <BDraw>
  public float BDraw_wrapJ(uint j, uint n) => ((j + n) % 6) / 3;
  public uint2 BDraw_JQuadu(uint j) => uint2(j + 2, j + 1) / 3 % 2;
  public float2 BDraw_JQuadf(uint j) => (float2)BDraw_JQuadu(j);
  public float4 BDraw_Number_quadPoint(float rx, float ry, uint j) { float2 p = BDraw_JQuadf(j); return float4((2 * p.x - 1) * rx, (1 - 2 * p.y) * ry, 0, 0); }
  public float4 BDraw_Sphere_quadPoint(float r, uint j) => r * float4(2 * BDraw_JQuadf(j) - 1, 0, 0);
  public float2 BDraw_Line_uv(float3 p0, float3 p1, float r, uint j) { float2 p = BDraw_JQuadf(j); return float2(length(p1 - p0) * (1 - p.y), (1 - 2 * p.x) * r); }
  public float2 BDraw_LineArrow_uv(float dpf, float3 p0, float3 p1, float r, uint j) { float2 p = BDraw_JQuadf(j); return float2((length(p1 - p0) + 2 * r) * (1 - p.y) - r, (1 - 2 * p.x) * r * dpf); }
  public float4 BDraw_LineArrow_p4(float dpf, float3 p0, float3 p1, float3 p3, float r, uint j) { float2 p = BDraw_JQuadf(j); float3 dp = normalize(cross(p1 - p0, p3 - p0)) * r * dpf; return float4(p.y * (p0 - p1) + p1 + dp * (1 - 2 * p.x), 1); }
  public float4 BDraw_LineArrow_p4(float dpf, float3 p0, float3 p1, float r, uint j) => BDraw_LineArrow_p4(dpf, p0, p1, _WorldSpaceCameraPos, r, j);
  public uint BDraw_o_i(v2f o) => roundu(o.ti.x);
  public v2f BDraw_o_i(uint i, v2f o) { o.ti.x = i; return o; }
  public uint BDraw_o_drawType(v2f o) => roundu(o.ti.z);
  public v2f BDraw_o_drawType(uint drawType, v2f o) { o.ti.z = drawType; return o; }
  public float4 BDraw_o_color(v2f o) => o.color;
  public v2f BDraw_o_color(float4 color, v2f o) { o.color = color; return o; }
  public float3 BDraw_o_normal(v2f o) => o.normal;
  public v2f BDraw_o_normal(float3 normal, v2f o) { o.normal = normal; return o; }
  public float2 BDraw_o_uv(v2f o) => o.uv;
  public v2f BDraw_o_uv(float2 uv, v2f o) { o.uv = uv; return o; }
  public float4 BDraw_o_pos(v2f o) => o.pos;
  public v2f BDraw_o_pos(float4 pos, v2f o) { o.pos = pos; return o; }
  public v2f BDraw_o_pos_PV(float3 p, float4 q, v2f o) => BDraw_o_pos(mul(UNITY_MATRIX_P, mul(UNITY_MATRIX_V, float4(p, 1)) + q), o);
  public v2f BDraw_o_pos_c(float4 c, v2f o) => BDraw_o_pos(UnityObjectToClipPos(c), o);
  public v2f BDraw_o_pos_c(float3 c, v2f o) => BDraw_o_pos(UnityObjectToClipPos(c), o);
  public float3 BDraw_o_wPos(v2f o) => o.wPos;
  public v2f BDraw_o_wPos(float3 wPos, v2f o) { o.wPos = wPos; return o; }
  public float3 BDraw_o_p0(v2f o) => o.p0;
  public v2f BDraw_o_p0(float3 p0, v2f o) { o.p0 = p0; return o; }
  public float3 BDraw_o_p1(v2f o) => o.p1;
  public v2f BDraw_o_p1(float3 p1, v2f o) { o.p1 = p1; return o; }
  public float BDraw_o_r(v2f o) => o.ti.w;
  public v2f BDraw_o_r(float r, v2f o) { o.ti.w = r; return o; }
  public float3 BDraw_quad(float3 p0, float3 p1, float3 p2, float3 p3, uint j) => j % 5 == 0 ? p3 : j == 1 ? p2 : j == 4 ? p0 : p1;
  public float4 BDraw_o_tj(v2f o) => o.tj;
  public v2f BDraw_o_tj(float4 tj, v2f o) { o.tj = tj; return o; }
  public v2f vert_BDraw_Point(float3 p, float4 color, uint i, v2f o) => BDraw_o_i(i, BDraw_o_drawType(BDraw_Draw_Point, BDraw_o_color(color, BDraw_o_pos(UnityObjectToClipPos(float4(p, 1)), o))));
  public v2f vert_BDraw_Sphere(float3 p, float r, float4 color, uint i, uint j, v2f o) { float4 q = BDraw_Sphere_quadPoint(r, j); return BDraw_o_i(i, BDraw_o_drawType(BDraw_Draw_Sphere, BDraw_o_color(color, BDraw_o_normal(-f001, BDraw_o_uv(q.xy / r, BDraw_o_pos_PV(p, q, BDraw_o_wPos(p, o))))))); }
  public v2f vert_BDraw_LineArrow(float dpf, float3 p0, float3 p1, float r, float4 color, uint i, uint j, v2f o) => BDraw_o_i(i, BDraw_o_p0(p0, BDraw_o_p1(p1, BDraw_o_r(r, BDraw_o_drawType(dpf == 1 ? BDraw_Draw_Line : BDraw_Draw_Arrow, BDraw_o_color(color, BDraw_o_uv(BDraw_LineArrow_uv(dpf, p0, p1, r, j), BDraw_o_pos_c(BDraw_LineArrow_p4(dpf, p0, p1, r, j), o))))))));
  public v2f vert_BDraw_Line(float3 p0, float3 p1, float r, float4 color, uint i, uint j, v2f o) => BDraw_o_i(i, BDraw_o_p0(p0, BDraw_o_p1(p1, BDraw_o_r(r, BDraw_o_drawType(BDraw_Draw_Line, BDraw_o_color(color, BDraw_o_uv(BDraw_Line_uv(p0, p1, r, j), BDraw_o_pos_c(BDraw_LineArrow_p4(1, p0, p1, r, j), o))))))));
  public v2f vert_BDraw_LineSegment(float3 p0, float3 p1, float r, float4 color, uint i, uint j, v2f o) => BDraw_o_drawType(BDraw_Draw_LineSegment, vert_BDraw_LineArrow(1, p0, p1, r, color, i, j, o));
  public v2f vert_BDraw_Arrow(float3 p0, float3 p1, float r, float4 color, uint i, uint j, v2f o) => vert_BDraw_LineArrow(3, p0, p1, r, color, i, j, o);
  public v2f vert_BDraw_BoxFrame(float3 c0, float3 c1, float lineRadius, float4 color, uint i, uint j, v2f o) { float3 p0, p1; switch (i) { case 0: p0 = c0; p1 = c0 * f110 + c1 * f001; break; case 1: p0 = c0 * f110 + c1 * f001; p1 = c0 * f100 + c1 * f011; break; case 2: p0 = c0 * f100 + c1 * f011; p1 = c0 * f101 + c1 * f010; break; case 3: p0 = c0 * f101 + c1 * f010; p1 = c0; break; case 4: p0 = c0 * f011 + c1 * f100; p1 = c0 * f010 + c1 * f101; break; case 5: p0 = c0 * f010 + c1 * f101; p1 = c1; break; case 6: p0 = c1; p1 = c0 * f001 + c1 * f110; break; case 7: p0 = c0 * f001 + c1 * f110; p1 = c0 * f011 + c1 * f100; break; case 8: p0 = c0; p1 = c0 * f011 + c1 * f100; break; case 9: p0 = c0 * f101 + c1 * f010; p1 = c0 * f001 + c1 * f110; break; case 10: p0 = c0 * f100 + c1 * f011; p1 = c1; break; default: p0 = c0 * f110 + c1 * f001; p1 = c0 * f010 + c1 * f101; break; } return vert_BDraw_Line(p0, p1, lineRadius, color, i, j, o); }
  public v2f vert_BDraw_Quad(float3 p0, float3 p1, float3 p2, float3 p3, float4 color, uint i, uint j, v2f o) { float3 p = BDraw_quad(p0, p1, p2, p3, j); return BDraw_o_i(i, BDraw_o_drawType(BDraw_Draw_Texture_2D, BDraw_o_normal(cross(p1 - p0, p0 - p3), BDraw_o_uv(float2(BDraw_wrapJ(j, 2), BDraw_wrapJ(j, 4)), BDraw_o_wPos(p, BDraw_o_pos_c(p, BDraw_o_color(color, o))))))); }
  public v2f vert_BDraw_Legend(uint i, uint j, v2f o) { float h = 8; float3 c = f110 * 10000, p0 = c + float3(0.4f, -h / 2, 0), p1 = p0 + f100 * 0.4f, p2 = p1 + h * f010, p3 = p0 + h * f010; return vert_BDraw_Quad(p0, p1, p2, p3, WHITE, i, j, o); }
  public v2f vert_BDraw_WebCam(float3 p0, float3 p1, float3 p2, float3 p3, float4 color, uint i, uint j, v2f o) => BDraw_o_drawType(BDraw_Draw_WebCam, vert_BDraw_Quad(p0, p1, p2, p3, color, i, j, o));
  public v2f vert_BDraw_Cube(float3 p, float3 r, float4 color, uint i, uint j, v2f o) { float3 p0, p1, p2, p3; switch (i % 6) { case 0: p0 = f___; p1 = f1__; p2 = f11_; p3 = f_1_; break; case 1: p0 = f1_1; p1 = f__1; p2 = f_11; p3 = f111; break; case 2: p0 = f__1; p1 = f1_1; p2 = f1__; p3 = f___; break; case 3: p0 = f_1_; p1 = f11_; p2 = f111; p3 = f_11; break; case 4: p0 = f__1; p1 = f___; p2 = f_1_; p3 = f_11; break; default: p0 = f1__; p1 = f1_1; p2 = f111; p3 = f11_; break; } return vert_BDraw_Quad(p0 * r + p, p1 * r + p, p2 * r + p, p3 * r + p, color, i, j, o); }
  public v2f vert_BDraw_Cube(float3 p, float r, float4 color, uint i, uint j, v2f o) => vert_BDraw_Cube(p, f111 * r, color, i, j, o);
  public v2f vert_BDraw_Signal(float3 p0, float3 p1, float r, uint i, uint j, v2f o) => BDraw_o_i(i, BDraw_o_p0(p0, BDraw_o_p1(p1, BDraw_o_uv(f11 - BDraw_JQuadf(j).yx, BDraw_o_drawType(BDraw_Draw_Signal, BDraw_o_r(r, BDraw_o_pos_c(BDraw_LineArrow_p4(1, p0, p1, r, j), o)))))));
  public v2f vert_BDraw_Signal(float3 p0, float3 p1, float r, float4 color, int drawType, float thickness, uint i, uint j, v2f o) => BDraw_o_tj(float4(distance(p0, p1), r, drawType, thickness), BDraw_o_color(color, vert_BDraw_Signal(p0, p1, r, i, j, o)));
  public virtual uint BDraw_SignalSmpN(uint chI) => 1024;
  public virtual float4 BDraw_SignalColor(uint chI, uint smpI) => YELLOW;
  public virtual float4 BDraw_SignalBackColor(uint chI, uint smpI) => float4(1, 1, 1, 0.2f);
  public virtual float BDraw_SignalSmpV(uint chI, uint smpI) => 0;
  public virtual float BDraw_SignalThickness(uint chI, uint smpI) => 0.004f;
  public virtual float BDraw_SignalFillCrest(uint chI, uint smpI) => 1;
  public virtual bool BDraw_SignalMarkerColor(uint stationI, float station_smpI, float4 color, uint chI, float smpI, uint display_x, out float4 return_color)
  {
    float d = abs(smpI - station_smpI + display_x);
    return (return_color = chI == stationI && d < 1 ? float4(color.xyz * (1 - d), 1) : f0000).w > 0;
  }
  public virtual float4 BDraw_SignalMarker(uint chI, float smpI) => f0000;
  public virtual float4 frag_BDraw_Signal(v2f i)
  {
    uint chI = BDraw_o_i(i), SmpN = BDraw_SignalSmpN(chI);
    float2 uv = i.uv, wh = float2(distance(i.p1, i.p0), BDraw_o_r(i));
    float smpI = lerp(0, SmpN, uv.x), y = lerp(-1, 1, uv.y), h = wh.y / wh.x * SmpN, thick = BDraw_SignalThickness(chI, (uint)smpI) * SmpN, d = float_PositiveInfinity;
    uint SmpI = (uint)smpI, dSmpI = ceilu(thick) + 1, SmpI0 = (uint)max(0, (int)SmpI - (int)dSmpI), SmpI1 = min(SmpN - 1, SmpI + dSmpI);
    float2 p0 = float2(smpI, y * h), q0 = float2(SmpI0, (h - thick) * BDraw_SignalSmpV(chI, SmpI0)), q1;
    for (uint sI = SmpI0; sI < SmpI1; sI++) { q1 = float2(sI + 1, (h - thick) * BDraw_SignalSmpV(chI, sI + 1)); d = min(d, LineSegDist(q0, q1, p0)); q0 = q1; }
    float4 c = BDraw_SignalColor(chI, SmpI);
    float v = 0.9f * lerp(BDraw_SignalSmpV(chI, SmpI), BDraw_SignalSmpV(chI, SmpI + 1), frac(smpI)), crest = BDraw_SignalFillCrest(chI, SmpI);
    float4 marker = BDraw_SignalMarker(chI, smpI);
    if (marker.w > 0) return marker;
    if (crest >= 0 ? y > crest && y < v : y < crest && y > v) return c;
    if (d < thick) return float4(c.xyz * (1 - d / thick), 1);
    return BDraw_SignalBackColor(chI, SmpI);
  }
  public float4 frag_BDraw_Sphere(v2f i) { float2 uv = i.uv; float r = dot(uv, uv); float4 color = i.color; if (r > 1.0f || color.a == 0) return f0000; float3 n = new float3(uv, r - 1), _LightDir = new float3(0.321f, 0.766f, -0.557f); float lightAmp = max(0.0f, dot(n, _LightDir)); float4 diffuse_Light = (lightAmp + UNITY_LIGHTMODEL_AMBIENT) * color; float spec = max(0, (lightAmp - 0.95f) / 0.05f); color = lerp(diffuse_Light, f1111, spec / 4); color.a = 1; return color; }
  public float4 frag_BDraw_Line(v2f i) { float3 p0 = i.p0, p1 = i.p1; float lineRadius = BDraw_o_r(i); float2 uv = i.uv; float r = dot(uv, uv), r2 = lineRadius * lineRadius; float4 color = i.color; float3 p10 = p1 - p0; float lp10 = length(p10); if (uv.x < 0) r /= r2; else if (uv.x > lp10) { uv.x -= lp10; r = dot(uv, uv) / r2; } else { uv.x = 0; r = dot(uv, uv) / r2; } if (r > 1.0f || color.a == 0) return f0000; float3 n = new float3(uv, r - 1), _LightDir = new float3(0.321f, 0.766f, -0.557f); float lightAmp = max(0.0f, dot(n, _LightDir)); float4 diffuse_Light = (lightAmp + UNITY_LIGHTMODEL_AMBIENT) * color; float spec = max(0, (lightAmp - 0.95f) / 0.05f); color = lerp(diffuse_Light, f1111, spec / 4); color.a = 1; return color; }
  public float4 frag_BDraw_Arrow(v2f i) { float3 p0 = i.p0, p1 = i.p1; float lineRadius = BDraw_o_r(i); float2 uv = i.uv; float r = dot(uv, uv), r2 = lineRadius * lineRadius; float4 color = i.color; float3 p10 = p1 - p0; float lp10 = length(p10); if (uv.x < 0) r /= r2; else if (uv.x > lp10 - lineRadius * 3 && abs(uv.y) > lineRadius) { uv.x -= lp10; uv = rotate_sc(uv, -sign(uv.y) * 0.5f, 0.866025404f); uv.x = 0; r = dot(uv, uv) / r2; } else if (uv.x > lp10) { uv.x -= lp10; r = dot(uv, uv) / r2; } else { uv.x = 0; r = dot(uv, uv) / r2; } if (r > 1.0f || color.a == 0) return f0000; float3 n = new float3(uv, r - 1), _LightDir = new float3(0.321f, 0.766f, -0.557f); float lightAmp = max(0.0f, dot(n, _LightDir)); float4 diffuse_Light = (lightAmp + UNITY_LIGHTMODEL_AMBIENT) * color; float spec = max(0, (lightAmp - 0.95f) / 0.05f); color = lerp(diffuse_Light, f1111, spec / 4); color.a = 1; return color; }
  public float4 frag_BDraw_LineSegment(v2f i) { float3 p0 = i.p0, p1 = i.p1; float lineRadius = BDraw_o_r(i); float2 uv = i.uv; float r = dot(uv, uv), r2 = lineRadius * lineRadius; float4 color = i.color; float3 p10 = p1 - p0; float lp10 = length(p10); uv.x = 0; r = dot(uv, uv) / r2; if (r > 1.0f || color.a == 0) return f0000; float3 n = new float3(uv, r - 1), _LightDir = new float3(0.321f, 0.766f, -0.557f); float lightAmp = max(0.0f, dot(n, _LightDir)); float4 diffuse_Light = (lightAmp + UNITY_LIGHTMODEL_AMBIENT) * color; float spec = max(0, (lightAmp - 0.95f) / 0.05f); color = lerp(diffuse_Light, f1111, spec / 4); color.a = 1; return color; }
  public float4 frag_BDraw_Quad(Texture2D t, v2f i) => i.color * tex2Dlod(t, new float4(i.uv, f00));
  public float4 frag_BDraw_Quad(RWStructuredBuffer<Color> v, v2f i, uint2 _image_size) => (float4)(i.color * v[id_to_i((uint2)(i.uv * (float2)_image_size), _image_size)]);
  public float4 frag_BDraw_Quad(RWStructuredBuffer<Color32> v, v2f i, uint2 _image_size) => (float4)(i.color * c32_f4(v[id_to_i((uint2)(i.uv * (float2)_image_size), _image_size)]));
  public float4 frag_BDraw_Mesh(v2f i) { float3 p = i.wPos; float4 color = i.color; color.xyz += dot(i.normal, _WorldSpaceLightPos0.xyz) / 2; return saturate(color); }
  public virtual bool BDraw_ABuff_IsBitOn(uint i) { uint c = BDraw_Byte(i); return c == BDraw_TB || c == BDraw_LF; }
  public class BDraw_TText3D
  {
    public string text; public float3 p, right, up, p0, p1; public float h; public float4 color, backColor;
    public BDraw_Text_QuadType quadType; public BDraw_TextAlignment textAlignment; public uint axis;
  }
  public Font BDraw_font { get; set; }
  public virtual void BDraw_InitBuffers0_GS()
  {
    if (BDraw_omitText) BDraw_fontInfoN = 0;
    else { BDraw_font ??= Resources.Load<Font>("Arial Font/arial Unicode"); BDraw_fontTexture = (Texture2D)BDraw_font.material.mainTexture; BDraw_fontInfoN = BDraw_includeUnicode ? BDraw_font.characterInfo.uLength() : 128 - 32; }
  }
  public virtual void BDraw_InitBuffers1_GS()
  {
    for (int i = 0; i < BDraw_fontInfoN; i++)
    {
      var c = BDraw_font.characterInfo[i];
      if (i == 0) BDraw_fontSize = c.size;
      if (c.index < 128) BDraw_fontInfos[c.index - 32] = new BDraw_FontInfo() { uvBottomLeft = c.uvBottomLeft, uvBottomRight = c.uvBottomRight, uvTopLeft = c.uvTopLeft, uvTopRight = c.uvTopRight, advance = max(c.advance, roundi(c.glyphWidth * 1.05f)), bearing = c.bearing, minX = c.minX, minY = c.minY, maxX = c.maxX, maxY = c.maxY };
    }
    BDraw_fontInfos.SetData();
  }
  public float BDraw_GetTextHeight() => 0.1f;
  public uint BDraw_GetText_ch(float v, uint _I, uint neg, uint uN) => _I < neg ? 13u : _I < uN + neg ? 16u + flooru(10 * frac(abs(v) / pow10(uN - _I + neg))) : _I == uN + neg ? 14u : 16u + flooru(10 * frac(abs(v) * pow10(_I - neg - uN - 1)));
  public uint BDraw_Byte(uint i) => TextByte(BDraw_tab_delimeted_text, i);
  public uint2 BDraw_Get_text_indexes(uint textI) => uint2(textI == 0 ? 0 : BDraw_ABuff_Indexes[textI - 1] + 1, textI < BDraw_ABuff_IndexN ? BDraw_ABuff_Indexes[textI] : BDraw_textCharN);
  public float BDraw_GetTextWidth(float v, uint decimalN)
  {
    float textWidth = 0, p10 = pow10(decimalN);
    v = round(v * p10) / p10;
    uint u = flooru(abs(v)), uN = u == 0 ? 1 : flooru(log10(abs(v)) + 1), numDigits = uN + decimalN + (decimalN == 0 ? 0 : 1u), neg = v < 0 ? 1u : 0;
    for (uint _I = 0; _I < numDigits + neg; _I++)
    {
      uint ch = BDraw_GetText_ch(v, _I, neg, uN);
      BDraw_FontInfo f = BDraw_fontInfos[ch];
      float2 mn = new float2(f.minX, f.minY) / BDraw_fontSize, mx = new float2(f.maxX, f.maxY) / BDraw_fontSize, range = mx - mn;
      float dx = f.advance / BDraw_fontSize;
      textWidth += dx;
    }
    return textWidth;
  }
  public float3 BDraw_GetTextWidth(float3 v, uint3 decimalN) => new float3(BDraw_GetTextWidth(v.x, decimalN.x), BDraw_GetTextWidth(v.y, decimalN.y), BDraw_GetTextWidth(v.z, decimalN.z));
  public List<BDraw_TText3D> BDraw_texts = new List<BDraw_TText3D>();
  public void BDraw_ClearTexts() => BDraw_texts.Clear();
  public virtual void BDraw_AddText(string text, float3 p, float3 right, float3 up, float4 color, float4 backColor, float h, BDraw_Text_QuadType quadType, BDraw_TextAlignment textAlignment, float3 p0, float3 p1, uint axis = 0) => BDraw_texts.Add(new BDraw_TText3D() { text = text, p = p, right = right, up = up, color = color, backColor = backColor, h = h, quadType = quadType, textAlignment = textAlignment, p0 = p0, p1 = p1, axis = axis });
  public void BDraw_AddText(string text, float3 p, float3 right, float3 up, float4 color, float4 backColor, float h, BDraw_Text_QuadType quadType, BDraw_TextAlignment textAlignment) => BDraw_AddText(text, p, right, up, color, backColor, h, quadType, textAlignment, f000, f000, 0);
  public virtual BDraw_TextInfo BDraw_textInfo(uint i) => BDraw_textInfos[i];
  public virtual void BDraw_textInfo(uint i, BDraw_TextInfo t) => BDraw_textInfos[i] = t;
  public int BDraw_ExtraTextN = 0;
  public virtual void BDraw_RebuildExtraTexts() { BDraw_BuildTexts(); BDraw_BuildTexts(); }
  public virtual void BDraw_BuildExtraTexts() { }
  public virtual void BDraw_BuildTexts()
  {
    SetBytes(ref BDraw_tab_delimeted_text, (BDraw_texts.Select(a => a.text).Join("\n") + "\n").ToBytes(Encoding.UTF8));
    AddComputeBuffer(ref BDraw_textInfos, nameof(BDraw_textInfo), BDraw_textN = max(1, BDraw_ABuff_Run(BDraw_textCharN = BDraw_tab_delimeted_text.uLength * 4)));
    for (uint i = 0; i < BDraw_texts.Count; i++)
    {
      var t = BDraw_texts[(int)i];
      var ti = BDraw_textInfo(i);
      ti.color = t.color; ti.backColor = t.backColor; ti.p = t.p; ti.p0 = t.p0; ti.p1 = t.p1; ti.height = t.h;
      ti.quadType = (uint)t.quadType; ti.axis = t.axis; ti.right = t.right; ti.up = t.up; ti.justification = (uint)t.textAlignment;
      BDraw_textInfo(i, ti);
    }
    if (BDraw_ABuff_Indexes == null || BDraw_ABuff_Indexes.computeBuffer == null) { AddComputeBuffer(ref BDraw_ABuff_Indexes, nameof(BDraw_ABuff_Indexes), 1); BDraw_ABuff_Indexes.SetData(new uint[] { 0 }); }
    if (BDraw_fontInfos != null && BDraw_ABuff_Indexes != null) { computeShader.SetBuffer(kernel_BDraw_getTextInfo, nameof(BDraw_textInfos), BDraw_textInfos); Gpu_BDraw_getTextInfo(); }
    if (BDraw_ExtraTextN > 0 && BDraw_texts.Count >= BDraw_ExtraTextN) BDraw_texts.RemoveRange(BDraw_texts.Count - BDraw_ExtraTextN, BDraw_ExtraTextN);
    int n = BDraw_texts.Count;
    BDraw_BuildExtraTexts();
    BDraw_ExtraTextN = BDraw_texts.Count - n;
  }
  public virtual IEnumerator BDraw_BuildTexts_Coroutine()
  {
    SetBytes(ref BDraw_tab_delimeted_text, (BDraw_texts.Select(a => a.text).Join("\n") + "\n").ToBytes(Encoding.UTF8));
    AddComputeBuffer(ref BDraw_textInfos, nameof(BDraw_textInfo), BDraw_textN = max(1, BDraw_ABuff_Run(BDraw_textCharN = BDraw_tab_delimeted_text.uLength * 4)));
    for (uint i = 0; i < BDraw_texts.Count; i++)
    {
      var t = BDraw_texts[(int)i];
      var ti = BDraw_textInfo(i);
      ti.color = t.color; ti.backColor = t.backColor; ti.p = t.p; ti.p0 = t.p0; ti.p1 = t.p1; ti.height = t.h;
      ti.quadType = (uint)t.quadType; ti.axis = t.axis; ti.right = t.right; ti.up = t.up; ti.justification = (uint)t.textAlignment;
      BDraw_textInfo(i, ti);
      if (i % 1000 == 0) { progress(i, (uint)BDraw_texts.Count); yield return null; }
    }
    progress(0);
    if (BDraw_ABuff_Indexes == null || BDraw_ABuff_Indexes.computeBuffer == null) { AddComputeBuffer(ref BDraw_ABuff_Indexes, nameof(BDraw_ABuff_Indexes), 1); BDraw_ABuff_Indexes.SetData(new uint[] { 0 }); }
    if (BDraw_fontInfos != null && BDraw_ABuff_Indexes != null) { computeShader.SetBuffer(kernel_BDraw_getTextInfo, nameof(BDraw_textInfos), BDraw_textInfos); Gpu_BDraw_getTextInfo(); }
  }
  public virtual void BDraw_BuildTexts_Default()
  {
    SetBytes(ref BDraw_tab_delimeted_text, (BDraw_texts.Select(a => a.text).Join("\n") + "\n").ToBytes(Encoding.UTF8));
    AddComputeBuffer(ref BDraw_textInfos, nameof(BDraw_textInfo), BDraw_textN = max(1, BDraw_ABuff_Run(BDraw_textCharN = BDraw_tab_delimeted_text.uLength * 4)));
    if (BDraw_texts.Count > 0)
    {
      var t = BDraw_texts[0];
      var ti = BDraw_textInfo(0);
      ti.color = t.color; ti.backColor = t.backColor; ti.p = t.p; ti.p0 = t.p0; ti.p1 = t.p1; ti.height = t.h;
      ti.quadType = (uint)t.quadType; ti.axis = t.axis; ti.right = t.right; ti.up = t.up; ti.justification = (uint)t.textAlignment;
      BDraw_textInfo(0, ti);
      Gpu_BDraw_setDefaultTextInfo();
    }
    if (BDraw_ABuff_Indexes == null || BDraw_ABuff_Indexes.computeBuffer == null) { AddComputeBuffer(ref BDraw_ABuff_Indexes, nameof(BDraw_ABuff_Indexes), 1); BDraw_ABuff_Indexes.SetData(new uint[] { 0 }); }
    if (BDraw_fontInfos != null && BDraw_ABuff_Indexes != null) Gpu_BDraw_getTextInfo();
  }
  public void BDraw_AddXAxis(string title, float titleHeight, float3 p0, float3 p1, float3 tRight, float3 tUp, float4 color, float2 vRange, string format, float textHeight, float3 right, float3 up, float3 margin, BDraw_Text_QuadType quadType, float3 mn, float3 mx, uint axis = 0)
  {
    if (extent(vRange) <= 0) return;
    uint decimalN = BDraw_GetDecimalN(vRange) + (uint)format.Length, maxTickN = BDraw_GetXAxisN(textHeight, decimalN, p1.xy - p0.xy);
    float range = NiceNum(abs(extent(vRange)), false), dxi = NiceNum(range / (maxTickN - 1), true), xi0 = ceil(vRange.x / dxi) * dxi, xi1 = floor(vRange.y / dxi) * dxi, x0, x1;
    if (vRange.x < vRange.y) { x0 = xi0; x1 = xi1 + dxi / 2; } else { x0 = xi1; x1 = xi0 - dxi / 2; }
    for (float xi = x0; xi < x1; xi += dxi) BDraw_AddText(xi.ToString(format), float3(lerp(p0.x, p1.x, (xi - vRange.x) / extent(vRange)), p0.y, p0.z) + textHeight / 2 * margin, right, up, color, EMPTY, textHeight, quadType, margin.y > 0 ? BDraw_TextAlignment.BottomCenter : BDraw_TextAlignment.TopCenter, mn, mx, axis);
    BDraw_AddText(title, (p0 + p1) / 2 + titleHeight * 2 * margin, tRight, tUp, color, EMPTY, titleHeight, quadType, margin.y > 0 ? BDraw_TextAlignment.BottomCenter : BDraw_TextAlignment.TopCenter, mn, mx, axis);
  }
  public void BDraw_AddYAxis(string title, float titleHeight, float3 p0, float3 p1, float3 tRight, float3 tUp, float4 color, float2 vRange, string format, float textHeight, float3 right, float3 up, float3 margin, BDraw_Text_QuadType quadType, float3 mn, float3 mx, uint axis = 0)
  {
    if (extent(vRange) <= 0) return;
    uint decimalN = BDraw_GetDecimalN(vRange) + (uint)format.Length, maxTickN = BDraw_GetXAxisN(textHeight, decimalN, p1.xy - p0.xy);
    float range = NiceNum(abs(extent(vRange)), false), dxi = NiceNum(range / (maxTickN - 1), true), xi0 = ceil(vRange.x / dxi) * dxi, xi1 = floor(vRange.y / dxi) * dxi, x0, x1;
    if (vRange.x < vRange.y) { x0 = xi0; x1 = xi1 + dxi / 2; } else { x0 = xi1; x1 = xi0 - dxi / 2; }
    for (float xi = x0; xi < x1; xi += dxi) BDraw_AddText(xi.ToString(format), float3(p0.x, lerp(p0.y, p1.y, (xi - vRange.x) / extent(vRange)), p0.z) + textHeight / 2 * margin, right, up, color, EMPTY, textHeight, quadType, tUp.x < 0 ? BDraw_TextAlignment.CenterRight : BDraw_TextAlignment.CenterLeft, mn, mx, axis);
    BDraw_AddText(title, (p0 + p1) / 2 + textHeight * (2 + decimalN / 5.0f) * margin, tRight, tUp, color, EMPTY, titleHeight, quadType, BDraw_TextAlignment.BottomCenter, mn, mx, axis);
  }
  public void BDraw_AddZAxis(string title, float titleHeight, float3 p0, float3 p1, float3 tRight, float3 tUp, float4 color, float2 vRange, string format, float textHeight, float3 right, float3 up, float3 margin, BDraw_Text_QuadType quadType, float3 mn, float3 mx, uint axis = 0)
  {
    if (extent(vRange) <= 0) return;
    uint decimalN = BDraw_GetDecimalN(vRange) + (uint)format.Length, maxTickN = BDraw_GetXAxisN(textHeight, decimalN, p1.zy - p0.zy);
    float range = NiceNum(abs(extent(vRange)), false), dxi = NiceNum(range / (maxTickN - 1), true), xi0 = ceil(vRange.x / dxi) * dxi, xi1 = floor(vRange.y / dxi) * dxi, x0, x1;
    if (vRange.x < vRange.y) { x0 = xi0; x1 = xi1 + dxi / 2; } else { x0 = xi1; x1 = xi0 - dxi / 2; }
    for (float xi = x0; xi < x1; xi += dxi) BDraw_AddText(xi.ToString(format), float3(p0.x, p0.y, lerp(p0.z, p1.z, (xi - vRange.x) / extent(vRange))) + textHeight / 2 * margin, right, up, color, EMPTY, textHeight, quadType, margin.y > 0 ? BDraw_TextAlignment.BottomCenter : BDraw_TextAlignment.TopCenter, mn, mx, axis);
    BDraw_AddText(title, (p0 + p1) / 2 + titleHeight * 2 * margin, tRight, tUp, color, EMPTY, titleHeight, quadType, margin.y > 0 ? BDraw_TextAlignment.BottomCenter : BDraw_TextAlignment.TopCenter, mn, mx, axis);
  }
  public void BDraw_AddAxes(float numberHeight, float titleHeight, float3 p0, float3 p1, float3 range0, float3 range1, float4 color,
    string xTitle, string yTitle, string zTitle, string xFormat = "0.00", string yFormat = null, string zFormat = null)
  {
    if (yFormat == null) yFormat = xFormat; if (zFormat == null) zFormat = yFormat;
    BDraw_AddXAxis(xTitle, titleHeight, float3(p0.x, p0.y, p0.z), float3(p1.x, p0.y, p0.z), f100, f011, color, float2(range0.x, range1.x), xFormat, numberHeight, f100, f011, f0__, BDraw_Text_QuadType.Switch, p0, p1, 100);
    BDraw_AddXAxis(xTitle, titleHeight, float3(p0.x, p0.y, p1.z), float3(p1.x, p0.y, p1.z), f100, f01_, color, float2(range0.x, range1.x), xFormat, numberHeight, f100, f01_, f0_1, BDraw_Text_QuadType.Switch, p0, p1, 200);
    BDraw_AddXAxis(xTitle, titleHeight, float3(p0.x, p1.y, p1.z), float3(p1.x, p1.y, p1.z), f100, f011, color, float2(range0.x, range1.x), xFormat, numberHeight, f100, f011, f011, BDraw_Text_QuadType.Switch, p0, p1, 300);
    BDraw_AddXAxis(xTitle, titleHeight, float3(p0.x, p1.y, p0.z), float3(p1.x, p1.y, p0.z), f100, f01_, color, float2(range0.x, range1.x), xFormat, numberHeight, f100, f01_, f01_, BDraw_Text_QuadType.Switch, p0, p1, 400);
    BDraw_AddYAxis(yTitle, titleHeight, float3(p0.x, p0.y, p0.z), float3(p0.x, p1.y, p0.z), f010, f_0_, color, float2(range0.y, range1.y), yFormat, numberHeight, f101, f010, f_0_, BDraw_Text_QuadType.Switch, p0, p1, 10);
    BDraw_AddYAxis(yTitle, titleHeight, float3(p1.x, p0.y, p0.z), float3(p1.x, p1.y, p0.z), f0_0, f10_, color, float2(range0.y, range1.y), yFormat, numberHeight, f10_, f010, f10_, BDraw_Text_QuadType.Switch, p0, p1, 20);
    BDraw_AddYAxis(yTitle, titleHeight, float3(p1.x, p0.y, p1.z), float3(p1.x, p1.y, p1.z), f0_0, f101, color, float2(range0.y, range1.y), yFormat, numberHeight, f101, f010, f101, BDraw_Text_QuadType.Switch, p0, p1, 30);
    BDraw_AddYAxis(yTitle, titleHeight, float3(p0.x, p0.y, p1.z), float3(p0.x, p1.y, p1.z), f0_0, f_01, color, float2(range0.y, range1.y), yFormat, numberHeight, f10_, f010, f_01, BDraw_Text_QuadType.Switch, p0, p1, 40);
    BDraw_AddZAxis(zTitle, titleHeight, float3(p0.x, p0.y, p0.z), float3(p0.x, p0.y, p1.z), f001, f110, color, float2(range0.z, range1.z), zFormat, numberHeight, f001, f110, f__0, BDraw_Text_QuadType.Switch, p0, p1, 1);
    BDraw_AddZAxis(zTitle, titleHeight, float3(p0.x, p1.y, p0.z), float3(p0.x, p1.y, p1.z), f001, f_10, color, float2(range0.z, range1.z), zFormat, numberHeight, f001, f_10, f_10, BDraw_Text_QuadType.Switch, p0, p1, 2);
    BDraw_AddZAxis(zTitle, titleHeight, float3(p1.x, p1.y, p0.z), float3(p1.x, p1.y, p1.z), f001, f110, color, float2(range0.z, range1.z), zFormat, numberHeight, f001, f110, f110, BDraw_Text_QuadType.Switch, p0, p1, 3);
    BDraw_AddZAxis(zTitle, titleHeight, float3(p1.x, p0.y, p0.z), float3(p1.x, p0.y, p1.z), f001, f_10, color, float2(range0.z, range1.z), zFormat, numberHeight, f001, f_10, f1_0, BDraw_Text_QuadType.Switch, p0, p1, 4);
  }
  public string BDraw_str(string[] s, int i) => i < s.Length ? s[i] : i > 2 && i - 3 < s.Length ? s[i - 3] : "";
  public void BDraw_AddAxes(float numberHeight, float titleHeight, float4 color, float3 p0, float3 p1,
    float3 rangeA0, float3 rangeA1, float3 rangeB0, float3 rangeB1, string xyz_titles, string xyz_formats = "0.00")
  {
    var (ts, fs, ttl, fmt) = (xyz_titles.Split("|"), xyz_formats.Split("|"), new string[6], new string[6]);
    for (int i = 0; i < 6; i++) { ttl[i] = BDraw_str(ts, i); fmt[i] = BDraw_str(fs, i); }
    BDraw_AddAxes(numberHeight, titleHeight, color, p0, p1, rangeA0, rangeA1, rangeB0, rangeB1, ttl, fmt);
  }
  public void BDraw_AddAxes(float numberHeight, float titleHeight, float4 color, float3 p0, float3 p1,
    float3 rangeA0, float3 rangeA1, float3 rangeB0, float3 rangeB1, string[] ttl, string[] fmt)
  {
    BDraw_AddXAxis(ttl[0], titleHeight, float3(p0.x, p0.y, p0.z), float3(p1.x, p0.y, p0.z), f100, f011, color, float2(rangeA0.x, rangeA1.x), fmt[0], numberHeight, f100, f011, f0__, BDraw_Text_QuadType.Switch, p0, p1, 100);
    BDraw_AddXAxis(ttl[0], titleHeight, float3(p0.x, p0.y, p1.z), float3(p1.x, p0.y, p1.z), f100, f01_, color, float2(rangeA0.x, rangeA1.x), fmt[0], numberHeight, f100, f01_, f0_1, BDraw_Text_QuadType.Switch, p0, p1, 200);
    BDraw_AddXAxis(ttl[3], titleHeight, float3(p0.x, p1.y, p1.z), float3(p1.x, p1.y, p1.z), f100, f011, color, float2(rangeB0.x, rangeB1.x), fmt[3], numberHeight, f100, f011, f011, BDraw_Text_QuadType.Switch, p0, p1, 300);
    BDraw_AddXAxis(ttl[3], titleHeight, float3(p0.x, p1.y, p0.z), float3(p1.x, p1.y, p0.z), f100, f01_, color, float2(rangeB0.x, rangeB1.x), fmt[3], numberHeight, f100, f01_, f01_, BDraw_Text_QuadType.Switch, p0, p1, 400);
    BDraw_AddYAxis(ttl[1], titleHeight, float3(p0.x, p0.y, p0.z), float3(p0.x, p1.y, p0.z), f010, f_0_, color, float2(rangeA0.y, rangeA1.y), fmt[1], numberHeight, f101, f010, f_0_, BDraw_Text_QuadType.Switch, p0, p1, 10);
    BDraw_AddYAxis(ttl[4], titleHeight, float3(p1.x, p0.y, p0.z), float3(p1.x, p1.y, p0.z), f0_0, f10_, color, float2(rangeB0.y, rangeB1.y), fmt[4], numberHeight, f10_, f010, f10_, BDraw_Text_QuadType.Switch, p0, p1, 20);
    BDraw_AddYAxis(ttl[1], titleHeight, float3(p1.x, p0.y, p1.z), float3(p1.x, p1.y, p1.z), f0_0, f101, color, float2(rangeA0.y, rangeA1.y), fmt[1], numberHeight, f101, f010, f101, BDraw_Text_QuadType.Switch, p0, p1, 30);
    BDraw_AddYAxis(ttl[4], titleHeight, float3(p0.x, p0.y, p1.z), float3(p0.x, p1.y, p1.z), f0_0, f_01, color, float2(rangeB0.y, rangeB1.y), fmt[4], numberHeight, f10_, f010, f_01, BDraw_Text_QuadType.Switch, p0, p1, 40);
    BDraw_AddZAxis(ttl[2], titleHeight, float3(p0.x, p0.y, p0.z), float3(p0.x, p0.y, p1.z), f001, f110, color, float2(rangeA0.z, rangeA1.z), fmt[2], numberHeight, f001, f110, f__0, BDraw_Text_QuadType.Switch, p0, p1, 1);
    BDraw_AddZAxis(ttl[5], titleHeight, float3(p0.x, p1.y, p0.z), float3(p0.x, p1.y, p1.z), f001, f_10, color, float2(rangeB0.z, rangeB1.z), fmt[5], numberHeight, f001, f_10, f_10, BDraw_Text_QuadType.Switch, p0, p1, 2);
    BDraw_AddZAxis(ttl[5], titleHeight, float3(p1.x, p1.y, p0.z), float3(p1.x, p1.y, p1.z), f001, f110, color, float2(rangeB0.z, rangeB1.z), fmt[5], numberHeight, f001, f110, f110, BDraw_Text_QuadType.Switch, p0, p1, 3);
    BDraw_AddZAxis(ttl[2], titleHeight, float3(p1.x, p0.y, p0.z), float3(p1.x, p0.y, p1.z), f001, f_10, color, float2(rangeA0.z, rangeA1.z), fmt[2], numberHeight, f001, f_10, f1_0, BDraw_Text_QuadType.Switch, p0, p1, 4);
  }
  public void BDraw_AddAxes(float numberHeight, float titleHeight, float3 p0, float3 p1, float3 range0, float3 range1, float4 color, string xyz_titles, string xyz_formats = "0.00")
  {
    var _titles = splitStr3(xyz_titles, false);
    var _formats = splitStr3(xyz_formats, true);
    BDraw_AddAxes(numberHeight, titleHeight, p0, p1, range0, range1, color, _titles[0], _titles[1], _titles[2], _formats[0], _formats[1], _formats[2]);
  }
  public void BDraw_AddAxes(float numberHeight, float titleHeight, float3 p0, float3 p1, float4 color,
    string xTitle, string yTitle, string zTitle, string xFormat = "0.00", string yFormat = null, string zFormat = null, bool zeroOrigin = false)
  {
    float3 range0 = p0, range1 = p1; if (zeroOrigin) { range1 -= range0; range0 = f000; }
    BDraw_AddAxes(numberHeight, titleHeight, p0, p1, range0, range1, color, xTitle, yTitle, zTitle, xFormat, yFormat, zFormat);
  }
  public void BDraw_AddAxes(float numberHeight, float titleHeight, float3 p0, float3 p1, float4 color,
  string xyz_titles, string xyz_formats = "0.00", bool zeroOrigin = false)
  {
    var _titles = splitStr3(xyz_titles, false);
    var _formats = splitStr3(xyz_formats, true);
    float3 range0 = p0, range1 = p1; if (zeroOrigin) { range1 -= range0; range0 = f000; }
    BDraw_AddAxes(numberHeight, titleHeight, p0, p1, range0, range1, color, _titles[0], _titles[1], _titles[2], _formats[0], _formats[1], _formats[2]);
  }
  public virtual void BDraw_AddAxes(float numberHeight, float titleHeight, float4 color,
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
    BDraw_AddXAxis(xTitleA, titleHeight, x0A, x1A, f100, f011, color, xRangeA, xFormatA, numberHeight, f100, f011, f0__, BDraw_Text_QuadType.Switch, f000, f000);
    BDraw_AddXAxis(xTitleB, titleHeight, x0B, x1B, f100, f011, color, xRangeB, xFormatB, numberHeight, f100, f011, f011, BDraw_Text_QuadType.Switch, f000, f000);
    BDraw_AddXAxis(xTitleC, titleHeight, x0C, x1C, f100, f011, color, xRangeC, xFormatC, numberHeight, f100, f011, f0__, BDraw_Text_QuadType.Switch, f000, f000);
    BDraw_AddXAxis(xTitleD, titleHeight, x0D, x1D, f100, f011, color, xRangeD, xFormatD, numberHeight, f100, f011, f011, BDraw_Text_QuadType.Switch, f000, f000);
    BDraw_AddYAxis(yTitleA, titleHeight, y0A, y1A, f010, f_01, color, yRangeA, yFormatA, numberHeight, f10_, f010, f_01, BDraw_Text_QuadType.Switch, f000, f000);
    BDraw_AddYAxis(yTitleB, titleHeight, y0B, y1B, f0_0, f10_, color, yRangeB, yFormatB, numberHeight, f10_, f010, f10_, BDraw_Text_QuadType.Switch, f000, f000);
    BDraw_AddYAxis(yTitleC, titleHeight, y0C, y1C, f010, f_01, color, yRangeC, yFormatC, numberHeight, f10_, f010, f_01, BDraw_Text_QuadType.Switch, f000, f000);
    BDraw_AddYAxis(yTitleD, titleHeight, y0D, y1D, f0_0, f10_, color, yRangeD, yFormatD, numberHeight, f10_, f010, f10_, BDraw_Text_QuadType.Switch, f000, f000);
    BDraw_AddZAxis(zTitleA, titleHeight, z0A, z1A, f010, f_01, color, zRangeA, zFormatA, numberHeight, f10_, f010, f_01, BDraw_Text_QuadType.Switch, f000, f000);
    BDraw_AddZAxis(zTitleB, titleHeight, z0B, z1B, f0_0, f10_, color, zRangeB, zFormatB, numberHeight, f10_, f010, f10_, BDraw_Text_QuadType.Switch, f000, f000);
    BDraw_AddZAxis(zTitleC, titleHeight, z0C, z1C, f010, f_01, color, zRangeC, zFormatC, numberHeight, f10_, f010, f_01, BDraw_Text_QuadType.Switch, f000, f000);
    BDraw_AddZAxis(zTitleD, titleHeight, z0D, z1D, f0_0, f10_, color, zRangeD, zFormatD, numberHeight, f10_, f010, f10_, BDraw_Text_QuadType.Switch, f000, f000);
  }
  public void BDraw_AddAxes(float numberHeight, float titleHeight, float4 color,
    string xTitleA, float3 x0A, float3 x1A, float2 xRangeA, string xFormatA,
    string xTitleB, float3 x0B, float3 x1B, float2 xRangeB, string xFormatB,
    string yTitleA, float3 y0A, float3 y1A, float2 yRangeA, string yFormatA,
    string yTitleB, float3 y0B, float3 y1B, float2 yRangeB, string yFormatB,
    string zTitleA, float3 z0A, float3 z1A, float2 zRangeA, string zFormatA,
    string zTitleB, float3 z0B, float3 z1B, float2 zRangeB, string zFormatB)
  {
    BDraw_AddXAxis(xTitleA, titleHeight, x0A, x1A, f100, f011, color, xRangeA, xFormatA, numberHeight, f100, f011, f0__, BDraw_Text_QuadType.Switch, f000, f000);
    BDraw_AddXAxis(xTitleB, titleHeight, x0B, x1B, f100, f011, color, xRangeB, xFormatB, numberHeight, f100, f011, f011, BDraw_Text_QuadType.Switch, f000, f000);
    BDraw_AddYAxis(yTitleA, titleHeight, y0A, y1A, f010, f_01, color, yRangeA, yFormatA, numberHeight, f10_, f010, f_01, BDraw_Text_QuadType.Switch, f000, f000);
    BDraw_AddYAxis(yTitleB, titleHeight, y0B, y1B, f0_0, f10_, color, yRangeB, yFormatB, numberHeight, f10_, f010, f10_, BDraw_Text_QuadType.Switch, f000, f000);
    BDraw_AddZAxis(zTitleA, titleHeight, z0A, z1A, f010, f_01, color, zRangeA, zFormatA, numberHeight, f10_, f010, f_01, BDraw_Text_QuadType.Switch, f000, f000);
    BDraw_AddZAxis(zTitleB, titleHeight, z0B, z1B, f0_0, f10_, color, zRangeB, zFormatB, numberHeight, f10_, f010, f10_, BDraw_Text_QuadType.Switch, f000, f000);
  }
  public void BDraw_AddAxes(float numberHeight, float titleHeight, float4 color,
    string xTitleA, float3 x0A, float3 x1A, float2 xRangeA, string xFormatA,
    string xTitleB, float3 x0B, float3 x1B, float2 xRangeB, string xFormatB,
    string yTitleA, float3 y0A, float3 y1A, float2 yRangeA, string yFormatA,
    string yTitleB, float3 y0B, float3 y1B, float2 yRangeB, string yFormatB)
  {
    BDraw_AddXAxis(xTitleA, titleHeight, x0A, x1A, f100, f011, color, xRangeA, xFormatA, numberHeight, f100, f011, f0__, BDraw_Text_QuadType.Switch, f000, f000);
    BDraw_AddXAxis(xTitleB, titleHeight, x0B, x1B, f100, f011, color, xRangeB, xFormatB, numberHeight, f100, f011, f011, BDraw_Text_QuadType.Switch, f000, f000);
    BDraw_AddYAxis(yTitleA, titleHeight, y0A, y1A, f010, f_01, color, yRangeA, yFormatA, numberHeight, f10_, f010, f_01, BDraw_Text_QuadType.Switch, f000, f000);
    BDraw_AddYAxis(yTitleB, titleHeight, y0B, y1B, f0_0, f10_, color, yRangeB, yFormatB, numberHeight, f10_, f010, f10_, BDraw_Text_QuadType.Switch, f000, f000);
  }
  public void BDraw_AddAxes(float numberHeight, float titleHeight, float4 color,
    string xTitleA, float3 x0A, float3 x1A, float2 xRangeA, string xFormatA,
    string yTitleA, float3 y0A, float3 y1A, float2 yRangeA, string yFormatA)
  {
    BDraw_AddAxes(numberHeight, titleHeight, color, xTitleA, x0A, x1A, xRangeA, xFormatA, xTitleA, x0A, x1A, xRangeA, xFormatA,
     yTitleA, y0A, y1A, yRangeA, yFormatA, yTitleA, y0A, y1A, yRangeA, yFormatA);
  }
  public uint BDraw_GetXAxisN(float textHeight, uint decimalN, float2 vRange) { float w = decimalN * textHeight; uint axisN = roundu(abs(extent(vRange)) / w); return clamp(axisN, 2, 25); }
  public uint BDraw_GetYAxisN(float textHeight, float2 vRange) => roundu(abs(extent(vRange)) / textHeight * 0.75f);
  public uint3 BDraw_GetXAxisN(float textHeight, uint3 decimalN, float3 cubeMin, float3 cubeMax)
  {
    float3 w = decimalN * textHeight;
    uint3 axisN = roundu(abs(cubeMax - cubeMin) / w);
    return clamp(axisN, u111 * 2, u111 * 25);
  }
  public uint3 BDraw_GetDecimalN(float3 cubeMin, float3 cubeMax)
  {
    int3 tickN = 25 * i111;
    float3 pRange = cubeMax - cubeMin, range = NiceNum(pRange, false);
    float3 di = NiceNum(range / (tickN - 1), true);
    uint3 decimalN = roundu(di >= 1) * flooru(1 + abs(log10(roundu(di == f000) + di)));
    return max(u111, decimalN);
  }
  public uint BDraw_GetDecimalN(float2 vRange)
  {
    int tickN = 25;
    float pRange = abs(extent(vRange)), range = NiceNum(pRange, false);
    float di = NiceNum(range / (tickN - 1), true);
    uint decimalN = roundu(Is(di >= 1)) * flooru(1 + abs(log10(roundu(Is(di == 0)) + di)));
    return max(1, decimalN);
  }
  public void BDraw_AddLegend(string title, float2 vRange, string format)
  {
    float h = 8;
    float3 c = 10000 * f110;
    BDraw_AddYAxis(title, 0.4f, c + float3(0.4f, -h / 2, 0), c + float3(0.4f, h / 2, 0), f010, f_00, BLACK, vRange, format, 0.2f, f100, f010, f_00, BDraw_Text_QuadType.FrontOnly, f000, f000);
  }
  public virtual v2f vert_BDraw_Text(BDraw_TextInfo t, uint i, uint j, v2f o)
  {
    float3 p = t.p, p0 = t.p0, p1 = t.p1, right = t.right, up = t.up;
    float h = t.height;
    float2 uvSize = t.uvSize;
    float4 color = t.color;
    uint quadType = t.quadType, justification = t.justification, axis = t.axis;
    float w = h * uvSize.x;
    float3 jp = f000;
    if (quadType == BDraw_Text_QuadType_Billboard)
    {
      o.wPos = p;
      switch (justification)
      {
        case BDraw_TextAlignment_BottomLeft: jp = w / 2 * right + h / 2 * up; break;
        case BDraw_TextAlignment_CenterLeft: jp = w / 2 * right; break;
        case BDraw_TextAlignment_TopLeft: jp = w / 2 * right - h / 2 * up; break;
        case BDraw_TextAlignment_BottomCenter: jp = h / 2 * up; break;
        case BDraw_TextAlignment_CenterCenter: break;
        case BDraw_TextAlignment_TopCenter: jp = -h / 2 * up; break;
        case BDraw_TextAlignment_BottomRight: jp = -w / 2 * right + h / 2 * up; break;
        case BDraw_TextAlignment_CenterRight: jp = -w / 2 * right; break;
        case BDraw_TextAlignment_TopRight: jp = -w / 2 * right - h / 2 * up; break;
      }
      float4 billboardQuad = float4((BDraw_wrapJ(j, 2) - 0.5f) * w, (0.5f - BDraw_wrapJ(j, 1)) * h, 0, 0);
      o = BDraw_o_i(i, BDraw_o_drawType(BDraw_Draw_Text3D, BDraw_o_r(i, BDraw_o_color(color, BDraw_o_pos_PV(p, billboardQuad + float4(jp, 0), BDraw_o_normal(f00_, BDraw_o_uv(float2(BDraw_wrapJ(j, 2), BDraw_wrapJ(j, 4)) * uvSize, o)))))));
    }
    else if (quadType == BDraw_Text_QuadType_Arrow)
    {
      o.wPos = p;
      right = normalize(p1 - p0);
      up = f010;
      jp = w / 2 * right;
      float3 q0 = p - jp, q1 = p + jp;
      o = BDraw_o_uv(dot(p - _WorldSpaceCameraPos, cross(right, up)) < 0 ? float2(length(q1 - q0) / h * BDraw_wrapJ(j, 1), BDraw_wrapJ(j, 2) - 0.5f) : float2(length(q1 - q0) / h * (1 - BDraw_wrapJ(j, 1)), 0.5f - BDraw_wrapJ(j, 2)), BDraw_o_drawType(BDraw_Draw_Text3D, vert_BDraw_Arrow(q0, q1, h * 0.165f, color, i, j, o)));
    }
    else if (quadType == BDraw_Text_QuadType_FrontBack || quadType == BDraw_Text_QuadType_Switch || dot(p - _WorldSpaceCameraPos, cross(right, up)) > 0)
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
          case BDraw_TextAlignment_BottomLeft: break;
          case BDraw_TextAlignment_CenterLeft: jp = -h / 2 * up; break;
          case BDraw_TextAlignment_TopLeft: jp = -h * up; break;
          case BDraw_TextAlignment_BottomCenter: jp = -w / 2 * right; break;
          case BDraw_TextAlignment_CenterCenter: jp = -w / 2 * right - h / 2 * up; break;
          case BDraw_TextAlignment_TopCenter: jp = -w / 2 * right - h * up; break;
          case BDraw_TextAlignment_BottomRight: jp = -w * right; break;
          case BDraw_TextAlignment_CenterRight: jp = -w * right - h / 2 * up; break;
          case BDraw_TextAlignment_TopRight: jp = -w * right - h * up; break;
        }
        float3 q0 = p + jp, q1 = q0 + right * w, p2 = q1 + up * h, p3 = q0 + up * h;
        o = BDraw_o_i(i, BDraw_o_drawType(BDraw_Draw_Text3D, BDraw_o_r(i, BDraw_o_color(color, BDraw_o_pos_c(o.wPos = j % 5 == 0 ? p3 : j == 1 ? p2 : j == 4 ? q0 : q1, BDraw_o_normal(cross(right, up), BDraw_o_uv(quadType == BDraw_Text_QuadType_Switch && dot(p - _WorldSpaceCameraPos, cross(right, up)) < 0 ? float2(1 - BDraw_wrapJ(j, 2), BDraw_wrapJ(j, 4)) * uvSize : float2(BDraw_wrapJ(j, 2), BDraw_wrapJ(j, 4)) * uvSize, o)))))));
      }
    }
    return o;
  }
  public virtual v2f vert_BDraw_Text(uint i, uint j, v2f o) => vert_BDraw_Text(BDraw_textInfo(i), i, j, o);
  public virtual void BDraw_getTextInfo_GS(uint3 id)
  {
    uint i = id.x;
    BDraw_TextInfo ti = BDraw_textInfo(i);
    ti.textI = i;
    ti.uvSize = f01;
    uint2 textIs = BDraw_Get_text_indexes(i);
    float2 t = ti.uvSize;
    for (uint j = textIs.x; j < textIs.y; j++) { uint byteI = BDraw_Byte(j); if (byteI >= 32) { byteI -= 32; t.x += BDraw_fontInfos[byteI].advance; } }
    t.x /= g.BDraw_fontSize;
    ti.uvSize = t;
    BDraw_textInfo(i, ti);
  }
  public virtual void BDraw_setDefaultTextInfo_GS(uint3 id)
  {
    uint i = id.x;
    if (i > 0)
    {
      BDraw_TextInfo t = BDraw_textInfo(0), ti = BDraw_textInfo(i);
      ti.color = t.color; ti.backColor = t.backColor; ti.p = t.p; ti.p0 = t.p0; ti.p1 = t.p1; ti.height = t.height;
      ti.quadType = (uint)t.quadType; ti.axis = t.axis; ti.right = t.right; ti.up = t.up; ti.justification = t.justification;
      BDraw_textInfo(i, ti);
    }
  }
  public virtual float3 BDraw_gridMin() { return f000; }
  public virtual float3 BDraw_gridMax() { return f111; }
  public float3 BDraw_gridSize() { return BDraw_gridMax() - BDraw_gridMin(); }
  public float3 BDraw_gridCenter() { return (BDraw_gridMax() + BDraw_gridMin()) / 2; }
  public virtual v2f vert_BDraw_Box(uint i, uint j, v2f o) { return vert_BDraw_BoxFrame(BDraw_gridMin(), BDraw_gridMax(), BDraw_boxThickness, BDraw_boxColor, i, j, o); }
  public virtual float4 frag_BDraw_Text(Texture2D t, RWStructuredBuffer<uint> _text, RWStructuredBuffer<BDraw_FontInfo> BDraw_fontInfos, float BDraw_fontSize, uint quadType, float4 backColor, uint2 textIs, v2f i)
  {
    float uv_x = 0;
    float4 color = backColor;
    for (uint textI = textIs.x; textI < textIs.y; textI++)
    {
      uint fontInfoI = TextByte(_text, textI) - 32;
      BDraw_FontInfo f = BDraw_fontInfos[fontInfoI];
      float dx = f.advance / BDraw_fontSize;
      float2 mn = float2(f.minX, f.minY) / BDraw_fontSize, mx = float2(f.maxX, f.maxY) / BDraw_fontSize, range = mx - mn;
      if (quadType == BDraw_Text_QuadType_Arrow) { mn.y -= 0.5f; mx.y -= 0.5f; }
      float2 uv = (i.uv - uv_x * f10 + f1_ * mn - float2(f.bearing / BDraw_fontSize, 0.25f)) / range;
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
  public virtual float4 frag_BDraw_GS(v2f i, float4 color)
  {
    switch (BDraw_o_drawType(i))
    {
      case uint_max: Discard(0); break;
      case BDraw_Draw_Sphere: color = frag_BDraw_Sphere(i); break;
      case BDraw_Draw_Line: color = frag_BDraw_Line(i); break;
      case BDraw_Draw_Arrow: color = frag_BDraw_Arrow(i); break;
      case BDraw_Draw_Signal: color = frag_BDraw_Signal(i); break;
      case BDraw_Draw_LineSegment: color = frag_BDraw_LineSegment(i); break;
      case BDraw_Draw_Mesh: color = frag_BDraw_Mesh(i); break;
      case BDraw_Draw_Text3D:
        BDraw_TextInfo t = BDraw_textInfo(BDraw_o_i(i));
        color = frag_BDraw_Text(BDraw_fontTexture, BDraw_tab_delimeted_text, BDraw_fontInfos, BDraw_fontSize, t.quadType, t.backColor, BDraw_Get_text_indexes(t.textI), i);
        break;
    }
    return color;
  }

  public virtual void base_BDraw_Start0_GS()
  {
    BDraw_ABuff_Start0_GS();
  }
  public virtual void base_BDraw_Start1_GS()
  {
    BDraw_ABuff_Start1_GS();
  }
  public virtual void base_BDraw_OnApplicationQuit() { Save_UI(); OnApplicationQuit_GS(); base.OnApplicationQuit(); already_quited = true; }
  public virtual void base_BDraw_LateUpdate0_GS()
  {
    BDraw_ABuff_LateUpdate0_GS();
  }
  public virtual void base_BDraw_LateUpdate1_GS()
  {
    BDraw_ABuff_LateUpdate1_GS();
  }
  public virtual void base_BDraw_Update0_GS()
  {
    BDraw_ABuff_Update0_GS();
  }
  public virtual void base_BDraw_Update1_GS()
  {
    BDraw_ABuff_Update1_GS();
  }
  public virtual void base_BDraw_OnValueChanged_GS()
  {
    BDraw_ABuff_OnValueChanged_GS();
  }
  public virtual void base_BDraw_InitBuffers0_GS()
  {
    BDraw_ABuff_InitBuffers0_GS();
  }
  public virtual void base_BDraw_InitBuffers1_GS()
  {
    BDraw_ABuff_InitBuffers1_GS();
  }
  [HideInInspector]
  public virtual void base_BDraw_onRenderObject_GS(ref bool render, ref bool cpu)
  {
    BDraw_ABuff_onRenderObject_GS(ref render, ref cpu);
  }
  public virtual float4 base_frag_BDraw_GS(v2f i, float4 color) { return color; }
  public void BDraw_ABuff_print_Bits(string title) { StrBldr sb = StrBldr($"{title}, BDraw_ABuff_Bits"); for (uint i = 0; i < BDraw_ABuff_BitN; i++) sb.Add(" ", BDraw_ABuff_Bits[i]); print(sb); }
  public void BDraw_ABuff_print_Sums(string title) { StrBldr sb = StrBldr($"{title}, BDraw_ABuff_Sums"); for (uint i = 0; i < BDraw_ABuff_BitN; i++) sb.Add(" ", BDraw_ABuff_Sums[i]); print(sb); }
  public void BDraw_ABuff_print_Indexes(string title) { StrBldr sb = StrBldr($"{title}: BDraw_ABuff_Indexes"); for (uint i = 0; i < BDraw_ABuff_IndexN; i++) sb.Add(" ", BDraw_ABuff_Indexes[i]); print(sb); }
  public void BDraw_ABuff_SetN(uint n)
  {
    if (n > 2147450880) throw new Exception("gsABuff: BDraw_ABuff_N > 2,147,450,880");
    BDraw_ABuff_N = n; BDraw_ABuff_BitN = ceilu(BDraw_ABuff_N, 32); BDraw_ABuff_BitN1 = ceilu(BDraw_ABuff_BitN, numthreads1); BDraw_ABuff_BitN2 = ceilu(BDraw_ABuff_BitN1, numthreads1);
    AllocData_BDraw_ABuff_Bits(BDraw_ABuff_BitN); AllocData_BDraw_ABuff_Fills1(BDraw_ABuff_BitN1); AllocData_BDraw_ABuff_Fills2(BDraw_ABuff_BitN2); AllocData_BDraw_ABuff_Sums(BDraw_ABuff_BitN);
  }
  public void BDraw_ABuff_FillPrefixes() { Gpu_BDraw_ABuff_GetFills1(); Gpu_BDraw_ABuff_GetFills2(); Gpu_BDraw_ABuff_IncFills1(); Gpu_BDraw_ABuff_IncSums(); }
  public void BDraw_ABuff_getIndexes() { AllocData_BDraw_ABuff_Indexes(BDraw_ABuff_IndexN); Gpu_BDraw_ABuff_GetIndexes(); }
  public void BDraw_ABuff_FillIndexes() { BDraw_ABuff_FillPrefixes(); BDraw_ABuff_getIndexes(); }
  public virtual uint BDraw_ABuff_Run(uint n) { BDraw_ABuff_SetN(n); Gpu_BDraw_ABuff_GetSums(); BDraw_ABuff_FillIndexes(); return BDraw_ABuff_IndexN; }
  public uint BDraw_ABuff_Run(int n) => BDraw_ABuff_Run((uint)n);
  public uint BDraw_ABuff_Run(uint2 n) => BDraw_ABuff_Run(cproduct(n)); public uint BDraw_ABuff_Run(uint3 n) => BDraw_ABuff_Run(cproduct(n));
  public uint BDraw_ABuff_Run(int2 n) => BDraw_ABuff_Run(cproduct(n)); public uint BDraw_ABuff_Run(int3 n) => BDraw_ABuff_Run(cproduct(n));
  public virtual void BDraw_ABuff_Init_Bits(uint n, GS parent, string _N, string _BitN, ref RWStructuredBuffer<uint> _Bits) { BDraw_ABuff_SetN(n); parent.SetValue(_N, BDraw_ABuff_N); parent.SetValue(_BitN, BDraw_ABuff_BitN); }
  public virtual void BDraw_ABuff_Prefix_Sums() { Gpu_BDraw_ABuff_Get_Bits_Sums(); BDraw_ABuff_FillPrefixes(); }
  public virtual void BDraw_ABuff_Bit_Indexes(RWStructuredBuffer<uint> bits, GS _this, string _IndexN, ref RWStructuredBuffer<uint> _Indexes) { BDraw_ABuff_Prefix_Sums(); BDraw_ABuff_getIndexes(); _this.SetValue(_IndexN, BDraw_ABuff_IndexN); }
  public uint BDraw_ABuff_Assign_Bits(uint i, uint j, uint bits) => bits | (Is(i < BDraw_ABuff_N && BDraw_ABuff_IsBitOn(i)) << (int)j);
  public virtual void BDraw_ABuff_Get_Bits_GS(uint3 id) { uint i = id.x, j, k, bits = 0; if (i < BDraw_ABuff_BitN) { for (j = 0, k = i * 32; j < 32; j++) bits = BDraw_ABuff_Assign_Bits(k + j, j, bits); BDraw_ABuff_Bits[i] = bits; } }
  public virtual IEnumerator BDraw_ABuff_GetSums_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI)
  {
    uint i = id.x, c, s, j, k, bits = 0;
    if (i < BDraw_ABuff_BitN) { for (j = 0, k = i * 32; j < 32; j++) bits = BDraw_ABuff_Assign_Bits(k + j, j, bits); BDraw_ABuff_Bits[i] = bits; c = countbits(bits); } else c = 0;
    BDraw_ABuff_grp0[grpI] = c; BDraw_ABuff_grp[grpI] = c; yield return GroupMemoryBarrierWithGroupSync();
    for (s = 1; s < numthreads1; s *= 2)
    {
      if (grpI >= s && i < BDraw_ABuff_BitN) BDraw_ABuff_grp[grpI] = BDraw_ABuff_grp0[grpI] + BDraw_ABuff_grp0[grpI - s]; yield return GroupMemoryBarrierWithGroupSync();
      BDraw_ABuff_grp0[grpI] = BDraw_ABuff_grp[grpI]; yield return GroupMemoryBarrierWithGroupSync();
    }
    if (i < BDraw_ABuff_BitN) BDraw_ABuff_Sums[i] = BDraw_ABuff_grp[grpI];
  }
  public virtual IEnumerator BDraw_ABuff_Get_Bits_Sums_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI)
  {
    uint i = id.x, c = i < BDraw_ABuff_BitN ? countbits(BDraw_ABuff_Bits[i]) : 0, s;
    BDraw_ABuff_grp0[grpI] = c; BDraw_ABuff_grp[grpI] = c; yield return GroupMemoryBarrierWithGroupSync();
    for (s = 1; s < numthreads1; s *= 2)
    {
      if (grpI >= s && i < BDraw_ABuff_BitN) BDraw_ABuff_grp[grpI] = BDraw_ABuff_grp0[grpI] + BDraw_ABuff_grp0[grpI - s]; yield return GroupMemoryBarrierWithGroupSync();
      BDraw_ABuff_grp0[grpI] = BDraw_ABuff_grp[grpI]; yield return GroupMemoryBarrierWithGroupSync();
    }
    if (i < BDraw_ABuff_BitN) BDraw_ABuff_Sums[i] = BDraw_ABuff_grp[grpI];
  }
  public virtual IEnumerator BDraw_ABuff_GetFills1_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI)
  {
    uint i = id.x, k = i + 1, j = k * numthreads1 - 1, c = i < BDraw_ABuff_BitN1 - 1 ? BDraw_ABuff_Sums[j] : i < BDraw_ABuff_BitN1 ? BDraw_ABuff_Sums[BDraw_ABuff_BitN - 1] : 0, s;
    BDraw_ABuff_grp0[grpI] = c; BDraw_ABuff_grp[grpI] = c; yield return GroupMemoryBarrierWithGroupSync();
    for (s = 1; s < numthreads1; s *= 2)
    {
      if (grpI >= s && i < BDraw_ABuff_BitN1) BDraw_ABuff_grp[grpI] = BDraw_ABuff_grp0[grpI] + BDraw_ABuff_grp0[grpI - s]; yield return GroupMemoryBarrierWithGroupSync();
      BDraw_ABuff_grp0[grpI] = BDraw_ABuff_grp[grpI]; yield return GroupMemoryBarrierWithGroupSync();
    }
    if (i < BDraw_ABuff_BitN1) BDraw_ABuff_Fills1[i] = BDraw_ABuff_grp[grpI];
  }
  public virtual IEnumerator BDraw_ABuff_GetFills2_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI)
  {
    uint i = id.x, k = i + 1, j = k * numthreads1 - 1, c = i < BDraw_ABuff_BitN2 - 1 ? BDraw_ABuff_Fills1[j] : i < BDraw_ABuff_BitN2 ? BDraw_ABuff_Fills1[BDraw_ABuff_BitN1 - 1] : 0, s;
    BDraw_ABuff_grp0[grpI] = c; BDraw_ABuff_grp[grpI] = c; yield return GroupMemoryBarrierWithGroupSync();
    for (s = 1; s < numthreads1; s *= 2)
    {
      if (grpI >= s && i < BDraw_ABuff_BitN2) BDraw_ABuff_grp[grpI] = BDraw_ABuff_grp0[grpI] + BDraw_ABuff_grp0[grpI - s]; yield return GroupMemoryBarrierWithGroupSync();
      BDraw_ABuff_grp0[grpI] = BDraw_ABuff_grp[grpI]; yield return GroupMemoryBarrierWithGroupSync();
    }
    if (i < BDraw_ABuff_BitN2) BDraw_ABuff_Fills2[i] = BDraw_ABuff_grp[grpI];
  }
  public virtual void BDraw_ABuff_IncFills1_GS(uint3 id) { uint i = id.x; if (i >= numthreads1) BDraw_ABuff_Fills1[i] += BDraw_ABuff_Fills2[i / numthreads1 - 1]; }
  public virtual void BDraw_ABuff_IncSums_GS(uint3 id) { uint i = id.x; if (i >= numthreads1) BDraw_ABuff_Sums[i] += BDraw_ABuff_Fills1[i / numthreads1 - 1]; if (i == BDraw_ABuff_BitN - 1) BDraw_ABuff_IndexN = BDraw_ABuff_Sums[i]; }
  public virtual void BDraw_ABuff_GetIndexes_GS(uint3 id) { uint i = id.x, j, sum = i == 0 ? 0 : BDraw_ABuff_Sums[i - 1], b, i32 = i << 5, k; for (k = 0, b = BDraw_ABuff_Bits[i]; b > 0; k++) { j = (uint)findLSB(b); BDraw_ABuff_Indexes[sum + k] = i32 + j; b = SetBitu(b, j, 0); } }

  public virtual void base_BDraw_ABuff_Start0_GS() { }
  public virtual void base_BDraw_ABuff_Start1_GS() { }
  public virtual void base_BDraw_ABuff_OnApplicationQuit() { Save_UI(); OnApplicationQuit_GS(); base.OnApplicationQuit(); already_quited = true; }
  public virtual void base_BDraw_ABuff_LateUpdate0_GS() { }
  public virtual void base_BDraw_ABuff_LateUpdate1_GS() { }
  public virtual void base_BDraw_ABuff_Update0_GS() { }
  public virtual void base_BDraw_ABuff_Update1_GS() { }
  public virtual void base_BDraw_ABuff_OnValueChanged_GS() { }
  public virtual void base_BDraw_ABuff_InitBuffers0_GS() { }
  public virtual void base_BDraw_ABuff_InitBuffers1_GS() { }
  [HideInInspector]
  public virtual void base_BDraw_ABuff_onRenderObject_GS(ref bool render, ref bool cpu) { }
  public virtual float4 base_frag_BDraw_ABuff_GS(v2f i, float4 color) { return color; }
  public virtual void BDraw_ABuff_InitBuffers0_GS() { }
  public virtual void BDraw_ABuff_InitBuffers1_GS() { }
  public virtual void BDraw_ABuff_LateUpdate0_GS() { }
  public virtual void BDraw_ABuff_LateUpdate1_GS() { }
  public virtual void BDraw_ABuff_Update0_GS() { }
  public virtual void BDraw_ABuff_Update1_GS() { }
  public virtual void BDraw_ABuff_Start0_GS() { }
  public virtual void BDraw_ABuff_Start1_GS() { }
  public virtual void BDraw_ABuff_OnValueChanged_GS() { }
  public virtual void BDraw_ABuff_OnApplicationQuit_GS() { }
  public virtual void BDraw_ABuff_onRenderObject_GS(ref bool render, ref bool cpu) { }
  public virtual float4 frag_BDraw_ABuff_GS(v2f i, float4 color)
  {
    return color;
  }
  public virtual void BDraw_LateUpdate0_GS() { }
  public virtual void BDraw_LateUpdate1_GS() { }
  public virtual void BDraw_Update0_GS() { }
  public virtual void BDraw_Update1_GS() { }
  public virtual void BDraw_Start0_GS() { }
  public virtual void BDraw_Start1_GS() { }
  public virtual void BDraw_OnValueChanged_GS() { }
  public virtual void BDraw_OnApplicationQuit_GS() { }
  public virtual void BDraw_onRenderObject_GS(ref bool render, ref bool cpu) { }
  #endregion <BDraw>
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
  public virtual float4 base_frag_Rand_GS(v2f i, float4 color)
  {
    uint libI = roundu(i.tj.x);
    return color;
  }
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
}