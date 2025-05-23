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
public class gsARand_Tutorial_ : GS
{
  [HideInInspector] public uiData data;
  [HideInInspector] public GARand_Tutorial g; public GARand_Tutorial G { get { gARand_Tutorial.GetData(); return gARand_Tutorial[0]; } }
  public void g_SetData() { if (gChanged && gARand_Tutorial != null) { gARand_Tutorial[0] = g; gARand_Tutorial.SetData(); gChanged = false; } }
  public virtual void ints_SetKernels(bool reallocate = false) { if (ints != null && (reallocate || ints.reallocated)) { SetKernelValues(ints, nameof(ints), kernel_Calc_Average); ints.reallocated = false; } }
  public virtual void randomNumbers_SetKernels(bool reallocate = false) { if (randomNumbers != null && (reallocate || randomNumbers.reallocated)) { SetKernelValues(randomNumbers, nameof(randomNumbers)); randomNumbers.reallocated = false; } }
  public virtual void ADraw_tab_delimeted_text_SetKernels(bool reallocate = false) { if (ADraw_tab_delimeted_text != null && (reallocate || ADraw_tab_delimeted_text.reallocated)) { SetKernelValues(ADraw_tab_delimeted_text, nameof(ADraw_tab_delimeted_text), kernel_ADraw_ABuff_GetSums, kernel_ADraw_ABuff_Get_Bits, kernel_ADraw_getTextInfo); ADraw_tab_delimeted_text.reallocated = false; } }
  public virtual void ADraw_textInfos_SetKernels(bool reallocate = false) { if (ADraw_textInfos != null && (reallocate || ADraw_textInfos.reallocated)) { SetKernelValues(ADraw_textInfos, nameof(ADraw_textInfos), kernel_ADraw_setDefaultTextInfo, kernel_ADraw_getTextInfo); ADraw_textInfos.reallocated = false; } }
  public virtual void ADraw_fontInfos_SetKernels(bool reallocate = false) { if (ADraw_fontInfos != null && (reallocate || ADraw_fontInfos.reallocated)) { SetKernelValues(ADraw_fontInfos, nameof(ADraw_fontInfos), kernel_ADraw_getTextInfo); ADraw_fontInfos.reallocated = false; } }
  public virtual void ARand_rs_SetKernels(bool reallocate = false) { if (ARand_rs != null && (reallocate || ARand_rs.reallocated)) { SetKernelValues(ARand_rs, nameof(ARand_rs), kernel_Calc_Average, kernel_Calc_Random_Numbers, kernel_ARand_initState, kernel_ARand_initSeed); ARand_rs.reallocated = false; } }
  public virtual void ADraw_ABuff_Bits_SetKernels(bool reallocate = false) { if (ADraw_ABuff_Bits != null && (reallocate || ADraw_ABuff_Bits.reallocated)) { SetKernelValues(ADraw_ABuff_Bits, nameof(ADraw_ABuff_Bits), kernel_ADraw_ABuff_GetIndexes, kernel_ADraw_ABuff_Get_Bits_Sums, kernel_ADraw_ABuff_GetSums, kernel_ADraw_ABuff_Get_Bits); ADraw_ABuff_Bits.reallocated = false; } }
  public virtual void ADraw_ABuff_Sums_SetKernels(bool reallocate = false) { if (ADraw_ABuff_Sums != null && (reallocate || ADraw_ABuff_Sums.reallocated)) { SetKernelValues(ADraw_ABuff_Sums, nameof(ADraw_ABuff_Sums), kernel_ADraw_ABuff_GetIndexes, kernel_ADraw_ABuff_IncSums, kernel_ADraw_ABuff_GetFills1, kernel_ADraw_ABuff_Get_Bits_Sums, kernel_ADraw_ABuff_GetSums); ADraw_ABuff_Sums.reallocated = false; } }
  public virtual void ADraw_ABuff_Indexes_SetKernels(bool reallocate = false) { if (ADraw_ABuff_Indexes != null && (reallocate || ADraw_ABuff_Indexes.reallocated)) { SetKernelValues(ADraw_ABuff_Indexes, nameof(ADraw_ABuff_Indexes), kernel_ADraw_ABuff_GetIndexes, kernel_ADraw_getTextInfo); ADraw_ABuff_Indexes.reallocated = false; } }
  public virtual void ADraw_ABuff_Fills1_SetKernels(bool reallocate = false) { if (ADraw_ABuff_Fills1 != null && (reallocate || ADraw_ABuff_Fills1.reallocated)) { SetKernelValues(ADraw_ABuff_Fills1, nameof(ADraw_ABuff_Fills1), kernel_ADraw_ABuff_IncSums, kernel_ADraw_ABuff_IncFills1, kernel_ADraw_ABuff_GetFills2, kernel_ADraw_ABuff_GetFills1); ADraw_ABuff_Fills1.reallocated = false; } }
  public virtual void ADraw_ABuff_Fills2_SetKernels(bool reallocate = false) { if (ADraw_ABuff_Fills2 != null && (reallocate || ADraw_ABuff_Fills2.reallocated)) { SetKernelValues(ADraw_ABuff_Fills2, nameof(ADraw_ABuff_Fills2), kernel_ADraw_ABuff_IncFills1, kernel_ADraw_ABuff_GetFills2); ADraw_ABuff_Fills2.reallocated = false; } }
  public virtual void Gpu_Calc_Average() { g_SetData(); ints?.SetCpu(); ints_SetKernels(); ARand_rs?.SetCpu(); ARand_rs_SetKernels(); Gpu(kernel_Calc_Average, Calc_Average, randomNumberN); ints?.ResetWrite(); ARand_rs?.ResetWrite(); }
  public virtual void Cpu_Calc_Average() { ints?.GetGpu(); ARand_rs?.GetGpu(); Cpu(Calc_Average, randomNumberN); ints.SetData(); ARand_rs.SetData(); }
  public virtual void Cpu_Calc_Average(uint3 id) { ints?.GetGpu(); ARand_rs?.GetGpu(); Calc_Average(id); ints.SetData(); ARand_rs.SetData(); }
  public virtual void Gpu_Calc_Random_Numbers() { g_SetData(); ARand_rs?.SetCpu(); ARand_rs_SetKernels(); Gpu(kernel_Calc_Random_Numbers, Calc_Random_Numbers, randomNumberN); ARand_rs?.ResetWrite(); }
  public virtual void Cpu_Calc_Random_Numbers() { ARand_rs?.GetGpu(); Cpu(Calc_Random_Numbers, randomNumberN); ARand_rs.SetData(); }
  public virtual void Cpu_Calc_Random_Numbers(uint3 id) { ARand_rs?.GetGpu(); Calc_Random_Numbers(id); ARand_rs.SetData(); }
  public virtual void Gpu_ARand_initState() { g_SetData(); ARand_rs?.SetCpu(); ARand_rs_SetKernels(); Gpu(kernel_ARand_initState, ARand_initState, ARand_I); ARand_rs?.ResetWrite(); }
  public virtual void Cpu_ARand_initState() { ARand_rs?.GetGpu(); Cpu(ARand_initState, ARand_I); ARand_rs.SetData(); }
  public virtual void Cpu_ARand_initState(uint3 id) { ARand_rs?.GetGpu(); ARand_initState(id); ARand_rs.SetData(); }
  public virtual void Gpu_ARand_initSeed() { g_SetData(); ARand_rs_SetKernels(); Gpu(kernel_ARand_initSeed, ARand_initSeed, ARand_N); ARand_rs?.ResetWrite(); }
  public virtual void Cpu_ARand_initSeed() { ARand_rs?.GetGpu(); Cpu(ARand_initSeed, ARand_N); ARand_rs.SetData(); }
  public virtual void Cpu_ARand_initSeed(uint3 id) { ARand_rs?.GetGpu(); ARand_initSeed(id); ARand_rs.SetData(); }
  public virtual void Gpu_ADraw_ABuff_GetIndexes() { g_SetData(); ADraw_ABuff_Bits?.SetCpu(); ADraw_ABuff_Bits_SetKernels(); ADraw_ABuff_Sums?.SetCpu(); ADraw_ABuff_Sums_SetKernels(); ADraw_ABuff_Indexes_SetKernels(); Gpu(kernel_ADraw_ABuff_GetIndexes, ADraw_ABuff_GetIndexes, ADraw_ABuff_BitN); ADraw_ABuff_Indexes?.ResetWrite(); }
  public virtual void Cpu_ADraw_ABuff_GetIndexes() { ADraw_ABuff_Bits?.GetGpu(); ADraw_ABuff_Sums?.GetGpu(); ADraw_ABuff_Indexes?.GetGpu(); Cpu(ADraw_ABuff_GetIndexes, ADraw_ABuff_BitN); ADraw_ABuff_Indexes.SetData(); }
  public virtual void Cpu_ADraw_ABuff_GetIndexes(uint3 id) { ADraw_ABuff_Bits?.GetGpu(); ADraw_ABuff_Sums?.GetGpu(); ADraw_ABuff_Indexes?.GetGpu(); ADraw_ABuff_GetIndexes(id); ADraw_ABuff_Indexes.SetData(); }
  public virtual void Gpu_ADraw_ABuff_IncSums() { g_SetData(); ADraw_ABuff_Sums?.SetCpu(); ADraw_ABuff_Sums_SetKernels(); ADraw_ABuff_Fills1?.SetCpu(); ADraw_ABuff_Fills1_SetKernels(); gARand_Tutorial?.SetCpu(); Gpu(kernel_ADraw_ABuff_IncSums, ADraw_ABuff_IncSums, ADraw_ABuff_BitN); ADraw_ABuff_Sums?.ResetWrite(); g = G; }
  public virtual void Cpu_ADraw_ABuff_IncSums() { ADraw_ABuff_Sums?.GetGpu(); ADraw_ABuff_Fills1?.GetGpu(); Cpu(ADraw_ABuff_IncSums, ADraw_ABuff_BitN); ADraw_ABuff_Sums.SetData(); }
  public virtual void Cpu_ADraw_ABuff_IncSums(uint3 id) { ADraw_ABuff_Sums?.GetGpu(); ADraw_ABuff_Fills1?.GetGpu(); ADraw_ABuff_IncSums(id); ADraw_ABuff_Sums.SetData(); }
  public virtual void Gpu_ADraw_ABuff_IncFills1() { g_SetData(); ADraw_ABuff_Fills1?.SetCpu(); ADraw_ABuff_Fills1_SetKernels(); ADraw_ABuff_Fills2?.SetCpu(); ADraw_ABuff_Fills2_SetKernels(); Gpu(kernel_ADraw_ABuff_IncFills1, ADraw_ABuff_IncFills1, ADraw_ABuff_BitN1); ADraw_ABuff_Fills1?.ResetWrite(); }
  public virtual void Cpu_ADraw_ABuff_IncFills1() { ADraw_ABuff_Fills1?.GetGpu(); ADraw_ABuff_Fills2?.GetGpu(); Cpu(ADraw_ABuff_IncFills1, ADraw_ABuff_BitN1); ADraw_ABuff_Fills1.SetData(); }
  public virtual void Cpu_ADraw_ABuff_IncFills1(uint3 id) { ADraw_ABuff_Fills1?.GetGpu(); ADraw_ABuff_Fills2?.GetGpu(); ADraw_ABuff_IncFills1(id); ADraw_ABuff_Fills1.SetData(); }
  public virtual void Gpu_ADraw_ABuff_GetFills2() { g_SetData(); ADraw_ABuff_Fills1?.SetCpu(); ADraw_ABuff_Fills1_SetKernels(); ADraw_ABuff_Fills2_SetKernels(); Gpu(kernel_ADraw_ABuff_GetFills2, ADraw_ABuff_GetFills2, ADraw_ABuff_BitN2); ADraw_ABuff_Fills2?.ResetWrite(); }
  public virtual IEnumerator Cpu_ADraw_ABuff_GetFills2() { ADraw_ABuff_Fills1?.GetGpu(); ADraw_ABuff_Fills2?.GetGpu(); yield return StartCoroutine(Cpu_Coroutine(kernel_ADraw_ABuff_GetFills2, ADraw_ABuff_GetFills2, ADraw_ABuff_BitN2)); }
  public virtual void Cpu_ADraw_ABuff_GetFills2(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { ADraw_ABuff_Fills1?.GetGpu(); ADraw_ABuff_Fills2?.GetGpu(); ADraw_ABuff_GetFills2(grp_tid, grp_id, id, grpI); ADraw_ABuff_Fills2.SetData(); }
  public virtual void Gpu_ADraw_ABuff_GetFills1() { g_SetData(); ADraw_ABuff_Sums?.SetCpu(); ADraw_ABuff_Sums_SetKernels(); ADraw_ABuff_Fills1_SetKernels(); Gpu(kernel_ADraw_ABuff_GetFills1, ADraw_ABuff_GetFills1, ADraw_ABuff_BitN1); ADraw_ABuff_Fills1?.ResetWrite(); }
  public virtual IEnumerator Cpu_ADraw_ABuff_GetFills1() { ADraw_ABuff_Sums?.GetGpu(); ADraw_ABuff_Fills1?.GetGpu(); yield return StartCoroutine(Cpu_Coroutine(kernel_ADraw_ABuff_GetFills1, ADraw_ABuff_GetFills1, ADraw_ABuff_BitN1)); }
  public virtual void Cpu_ADraw_ABuff_GetFills1(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { ADraw_ABuff_Sums?.GetGpu(); ADraw_ABuff_Fills1?.GetGpu(); ADraw_ABuff_GetFills1(grp_tid, grp_id, id, grpI); ADraw_ABuff_Fills1.SetData(); }
  public virtual void Gpu_ADraw_ABuff_Get_Bits_Sums() { g_SetData(); ADraw_ABuff_Bits?.SetCpu(); ADraw_ABuff_Bits_SetKernels(); ADraw_ABuff_Sums_SetKernels(); Gpu(kernel_ADraw_ABuff_Get_Bits_Sums, ADraw_ABuff_Get_Bits_Sums, ADraw_ABuff_BitN); ADraw_ABuff_Sums?.ResetWrite(); }
  public virtual IEnumerator Cpu_ADraw_ABuff_Get_Bits_Sums() { ADraw_ABuff_Bits?.GetGpu(); ADraw_ABuff_Sums?.GetGpu(); yield return StartCoroutine(Cpu_Coroutine(kernel_ADraw_ABuff_Get_Bits_Sums, ADraw_ABuff_Get_Bits_Sums, ADraw_ABuff_BitN)); }
  public virtual void Cpu_ADraw_ABuff_Get_Bits_Sums(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { ADraw_ABuff_Bits?.GetGpu(); ADraw_ABuff_Sums?.GetGpu(); ADraw_ABuff_Get_Bits_Sums(grp_tid, grp_id, id, grpI); ADraw_ABuff_Sums.SetData(); }
  public virtual void Gpu_ADraw_ABuff_GetSums() { g_SetData(); ADraw_ABuff_Bits_SetKernels(); ADraw_ABuff_Sums_SetKernels(); ADraw_tab_delimeted_text?.SetCpu(); ADraw_tab_delimeted_text_SetKernels(); Gpu(kernel_ADraw_ABuff_GetSums, ADraw_ABuff_GetSums, ADraw_ABuff_BitN); ADraw_ABuff_Bits?.ResetWrite(); ADraw_ABuff_Sums?.ResetWrite(); }
  public virtual IEnumerator Cpu_ADraw_ABuff_GetSums() { ADraw_ABuff_Bits?.GetGpu(); ADraw_ABuff_Sums?.GetGpu(); ADraw_tab_delimeted_text?.GetGpu(); yield return StartCoroutine(Cpu_Coroutine(kernel_ADraw_ABuff_GetSums, ADraw_ABuff_GetSums, ADraw_ABuff_BitN)); }
  public virtual void Cpu_ADraw_ABuff_GetSums(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { ADraw_ABuff_Bits?.GetGpu(); ADraw_ABuff_Sums?.GetGpu(); ADraw_tab_delimeted_text?.GetGpu(); ADraw_ABuff_GetSums(grp_tid, grp_id, id, grpI); ADraw_ABuff_Bits.SetData(); ADraw_ABuff_Sums.SetData(); }
  public virtual void Gpu_ADraw_ABuff_Get_Bits() { g_SetData(); ADraw_ABuff_Bits_SetKernels(); ADraw_tab_delimeted_text?.SetCpu(); ADraw_tab_delimeted_text_SetKernels(); Gpu(kernel_ADraw_ABuff_Get_Bits, ADraw_ABuff_Get_Bits, ADraw_ABuff_BitN); ADraw_ABuff_Bits?.ResetWrite(); }
  public virtual void Cpu_ADraw_ABuff_Get_Bits() { ADraw_ABuff_Bits?.GetGpu(); ADraw_tab_delimeted_text?.GetGpu(); Cpu(ADraw_ABuff_Get_Bits, ADraw_ABuff_BitN); ADraw_ABuff_Bits.SetData(); }
  public virtual void Cpu_ADraw_ABuff_Get_Bits(uint3 id) { ADraw_ABuff_Bits?.GetGpu(); ADraw_tab_delimeted_text?.GetGpu(); ADraw_ABuff_Get_Bits(id); ADraw_ABuff_Bits.SetData(); }
  public virtual void Gpu_ADraw_setDefaultTextInfo() { g_SetData(); ADraw_textInfos?.SetCpu(); ADraw_textInfos_SetKernels(); Gpu(kernel_ADraw_setDefaultTextInfo, ADraw_setDefaultTextInfo, ADraw_textN); ADraw_textInfos?.ResetWrite(); }
  public virtual void Cpu_ADraw_setDefaultTextInfo() { ADraw_textInfos?.GetGpu(); Cpu(ADraw_setDefaultTextInfo, ADraw_textN); ADraw_textInfos.SetData(); }
  public virtual void Cpu_ADraw_setDefaultTextInfo(uint3 id) { ADraw_textInfos?.GetGpu(); ADraw_setDefaultTextInfo(id); ADraw_textInfos.SetData(); }
  public virtual void Gpu_ADraw_getTextInfo() { g_SetData(); ADraw_fontInfos?.SetCpu(); ADraw_fontInfos_SetKernels(); ADraw_textInfos?.SetCpu(); ADraw_textInfos_SetKernels(); ADraw_ABuff_Indexes?.SetCpu(); ADraw_ABuff_Indexes_SetKernels(); ADraw_tab_delimeted_text?.SetCpu(); ADraw_tab_delimeted_text_SetKernels(); Gpu(kernel_ADraw_getTextInfo, ADraw_getTextInfo, ADraw_textN); ADraw_textInfos?.ResetWrite(); }
  public virtual void Cpu_ADraw_getTextInfo() { ADraw_fontInfos?.GetGpu(); ADraw_textInfos?.GetGpu(); ADraw_ABuff_Indexes?.GetGpu(); ADraw_tab_delimeted_text?.GetGpu(); Cpu(ADraw_getTextInfo, ADraw_textN); ADraw_textInfos.SetData(); }
  public virtual void Cpu_ADraw_getTextInfo(uint3 id) { ADraw_fontInfos?.GetGpu(); ADraw_textInfos?.GetGpu(); ADraw_ABuff_Indexes?.GetGpu(); ADraw_tab_delimeted_text?.GetGpu(); ADraw_getTextInfo(id); ADraw_textInfos.SetData(); }
  public virtual void Gpu_ARand_grp_fill_1K() { g_SetData(); Gpu(kernel_ARand_grp_fill_1K, ARand_grp_fill_1K, ARand_N); }
  public virtual IEnumerator Cpu_ARand_grp_fill_1K() { yield return StartCoroutine(Cpu_Coroutine(kernel_ARand_grp_fill_1K, ARand_grp_fill_1K, ARand_N)); }
  public virtual void Cpu_ARand_grp_fill_1K(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { ARand_grp_fill_1K(grp_tid, grp_id, id, grpI); }
  public virtual void Gpu_ARand_grp_init_1K() { g_SetData(); Gpu(kernel_ARand_grp_init_1K, ARand_grp_init_1K, ARand_N / 1024); }
  public virtual IEnumerator Cpu_ARand_grp_init_1K() { yield return StartCoroutine(Cpu_Coroutine(kernel_ARand_grp_init_1K, ARand_grp_init_1K, ARand_N / 1024)); }
  public virtual void Cpu_ARand_grp_init_1K(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { ARand_grp_init_1K(grp_tid, grp_id, id, grpI); }
  public virtual void Gpu_ARand_grp_init_1M() { g_SetData(); Gpu(kernel_ARand_grp_init_1M, ARand_grp_init_1M, ARand_N / 1024 / 1024); }
  public virtual IEnumerator Cpu_ARand_grp_init_1M() { yield return StartCoroutine(Cpu_Coroutine(kernel_ARand_grp_init_1M, ARand_grp_init_1M, ARand_N / 1024 / 1024)); }
  public virtual void Cpu_ARand_grp_init_1M(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { ARand_grp_init_1M(grp_tid, grp_id, id, grpI); }
  [JsonConverter(typeof(StringEnumConverter))] public enum ADraw_Draw : uint { Point, Sphere, Line, Arrow, Signal, LineSegment, Texture_2D, Quad, WebCam, Mesh, Number, N }
  [JsonConverter(typeof(StringEnumConverter))] public enum ADraw_TextAlignment : uint { BottomLeft, CenterLeft, TopLeft, BottomCenter, CenterCenter, TopCenter, BottomRight, CenterRight, TopRight }
  [JsonConverter(typeof(StringEnumConverter))] public enum ADraw_Text_QuadType : uint { FrontOnly, FrontBack, Switch, Arrow, Billboard }
  public const uint ADraw_Draw_Point = 0, ADraw_Draw_Sphere = 1, ADraw_Draw_Line = 2, ADraw_Draw_Arrow = 3, ADraw_Draw_Signal = 4, ADraw_Draw_LineSegment = 5, ADraw_Draw_Texture_2D = 6, ADraw_Draw_Quad = 7, ADraw_Draw_WebCam = 8, ADraw_Draw_Mesh = 9, ADraw_Draw_Number = 10, ADraw_Draw_N = 11;
  public const uint ADraw_TextAlignment_BottomLeft = 0, ADraw_TextAlignment_CenterLeft = 1, ADraw_TextAlignment_TopLeft = 2, ADraw_TextAlignment_BottomCenter = 3, ADraw_TextAlignment_CenterCenter = 4, ADraw_TextAlignment_TopCenter = 5, ADraw_TextAlignment_BottomRight = 6, ADraw_TextAlignment_CenterRight = 7, ADraw_TextAlignment_TopRight = 8;
  public const uint ADraw_Text_QuadType_FrontOnly = 0, ADraw_Text_QuadType_FrontBack = 1, ADraw_Text_QuadType_Switch = 2, ADraw_Text_QuadType_Arrow = 3, ADraw_Text_QuadType_Billboard = 4;
  public const uint ADraw_Draw_Text3D = 12, ADraw_LF = 10, ADraw_TB = 9, ADraw_ZERO = 48, ADraw_NINE = 57, ADraw_PERIOD = 46, ADraw_COMMA = 44, ADraw_PLUS = 43, ADraw_MINUS = 45, ADraw_SPACE = 32;
  [HideInInspector] public bool ValuesChanged, gChanged;
  public static gsARand_Tutorial This;
  public virtual void Awake() { This = this as gsARand_Tutorial; Awake_GS(); }
  public virtual void Awake_GS()
  {
  }
  public virtual void Start() { Build_UI(); Load_UI(); Start0_GS(); InitBuffers(); Start1_GS(); }
  public virtual void Start0_GS()
  {
    ADraw_Start0_GS();
    ARand_Start0_GS();
  }
  public virtual void Start1_GS()
  {
    ADraw_Start1_GS();
    ARand_Start1_GS();
  }
  public override void onLoaded()
  {
    base.onLoaded();
  }
  [HideInInspector] public bool already_quited = false;
  public override void OnApplicationQuit() { Save_UI(); OnApplicationQuit_GS(); base.OnApplicationQuit(); already_quited = true; }
  public virtual void OnApplicationQuit_GS()
  {
    ADraw_OnApplicationQuit_GS();
    ARand_OnApplicationQuit_GS();
  }
  public override void ui_to_data()
  {
    if (data == null) return;
    data.siUnits = siUnits;
    data.group_UI = group_UI;
    data.lineThickness = lineThickness;
    data.group_ARand = group_ARand;
    data.randomNumberN = randomNumberN;
    data.group_Avg = group_Avg;
    data.Avg_Val = Avg_Val;
    data.Avg_Val_Runtime = Avg_Val_Runtime;
    data.Avg_Val_TFlops = Avg_Val_TFlops;
  }
  public override void data_to_ui()
  {
    if (data == null) return;
    group_UI = data.group_UI;
    lineThickness = ui_txt_str.Contains("\"lineThickness\"") ? data.lineThickness : 0.004f;
    group_ARand = data.group_ARand;
    randomNumberN = ui_txt_str.Contains("\"randomNumberN\"") ? data.randomNumberN : 128;
    group_Avg = data.group_Avg;
    Avg_Val = data.Avg_Val;
    Avg_Val_Runtime = data.Avg_Val_Runtime;
    Avg_Val_TFlops = data.Avg_Val_TFlops;
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
    if (UI_lineThickness.Changed || lineThickness != UI_lineThickness.v) lineThickness = UI_lineThickness.v;
    if (UI_randomNumberN.Changed || randomNumberN != UI_randomNumberN.v) randomNumberN = UI_randomNumberN.v;
    if (UI_Avg_Val.Changed || Avg_Val != UI_Avg_Val.v) Avg_Val = UI_Avg_Val.v;
    if (UI_Avg_Val_Runtime.Changed || Avg_Val_Runtime != UI_Avg_Val_Runtime.v) Avg_Val_Runtime = UI_Avg_Val_Runtime.v;
    if (UI_Avg_Val_TFlops.Changed || Avg_Val_TFlops != UI_Avg_Val_TFlops.v) Avg_Val_TFlops = UI_Avg_Val_TFlops.v;
    if (ValuesChanged) { gChanged = true; OnValueChanged(); ValuesChanged = UI_group_UI.Changed = UI_lineThickness.Changed = UI_group_ARand.Changed = UI_randomNumberN.Changed = UI_group_Avg.Changed = UI_Avg_Val.Changed = UI_Avg_Val_Runtime.Changed = UI_Avg_Val_TFlops.Changed = false; }
    ADraw_LateUpdate1_GS();
    ARand_LateUpdate1_GS();
    LateUpdate1_GS();
    if(lateUpdateI++ == uint_max) lateUpdateI = 100;
  }
  public virtual void LateUpdate0_GS()
  {
    ADraw_LateUpdate0_GS();
    ARand_LateUpdate0_GS();
  }
  public virtual void LateUpdate1_GS()
  {
    ADraw_LateUpdate1_GS();
    ARand_LateUpdate1_GS();
  }
  public override void Update()
  {
    base.Update();
    if (!ui_loaded) return;
    Update0_GS();
    ADraw_Update1_GS();
    ARand_Update1_GS();
    Update1_GS();
  }
  public virtual void Update0_GS()
  {
    ADraw_Update0_GS();
    ARand_Update0_GS();
  }
  public virtual void Update1_GS()
  {
    ADraw_Update1_GS();
    ARand_Update1_GS();
  }
  public override void OnValueChanged()
  {
    if (!ui_loaded) return;
    OnValueChanged_GS();
    if (UI_randomNumberN.Changed) { Init_randomNumbers(); Avg(); }
  }
  public override void OnValueChanged_GS()
  {
    ADraw_OnValueChanged_GS();
    ARand_OnValueChanged_GS();
    var type = "gsAReport_Lib".ToType();
    if (type != null) ((GS)GetComponent(type))?.OnValueChanged_GS();

  }
  public string[] Scenes_in_Build => new string[] {  };
  public virtual float lineThickness { get => g.lineThickness; set { if (any(g.lineThickness != value) || any(UI_lineThickness.v != value)) { g.lineThickness = UI_lineThickness.v = value; ValuesChanged = gChanged = true; } } }
  public virtual uint randomNumberN { get => g.randomNumberN; set { if (g.randomNumberN != value || UI_randomNumberN.v != value) { g.randomNumberN = UI_randomNumberN.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float Avg_Val { get => g.Avg_Val; set { if (any(g.Avg_Val != value) || any(UI_Avg_Val.v != value)) { g.Avg_Val = UI_Avg_Val.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float Avg_Val_Runtime { get => g.Avg_Val_Runtime; set { if (any(g.Avg_Val_Runtime != value) || any(UI_Avg_Val_Runtime.v != value)) { g.Avg_Val_Runtime = UI_Avg_Val_Runtime.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float Avg_Val_TFlops { get => g.Avg_Val_TFlops; set { if (any(g.Avg_Val_TFlops != value) || any(UI_Avg_Val_TFlops.v != value)) { g.Avg_Val_TFlops = UI_Avg_Val_TFlops.v = value; ValuesChanged = gChanged = true; } } }
  public virtual uint ADraw_ABuff_IndexN { get => g.ADraw_ABuff_IndexN; set { if (g.ADraw_ABuff_IndexN != value) { g.ADraw_ABuff_IndexN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint ADraw_ABuff_BitN { get => g.ADraw_ABuff_BitN; set { if (g.ADraw_ABuff_BitN != value) { g.ADraw_ABuff_BitN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint ADraw_ABuff_N { get => g.ADraw_ABuff_N; set { if (g.ADraw_ABuff_N != value) { g.ADraw_ABuff_N = value; ValuesChanged = gChanged = true; } } }
  public virtual uint ADraw_ABuff_BitN1 { get => g.ADraw_ABuff_BitN1; set { if (g.ADraw_ABuff_BitN1 != value) { g.ADraw_ABuff_BitN1 = value; ValuesChanged = gChanged = true; } } }
  public virtual uint ADraw_ABuff_BitN2 { get => g.ADraw_ABuff_BitN2; set { if (g.ADraw_ABuff_BitN2 != value) { g.ADraw_ABuff_BitN2 = value; ValuesChanged = gChanged = true; } } }
  public virtual bool ADraw_omitText { get => Is(g.ADraw_omitText); set { if (g.ADraw_omitText != Is(value)) { g.ADraw_omitText = Is(value); ValuesChanged = gChanged = true; } } }
  public virtual bool ADraw_includeUnicode { get => Is(g.ADraw_includeUnicode); set { if (g.ADraw_includeUnicode != Is(value)) { g.ADraw_includeUnicode = Is(value); ValuesChanged = gChanged = true; } } }
  public virtual uint ADraw_fontInfoN { get => g.ADraw_fontInfoN; set { if (g.ADraw_fontInfoN != value) { g.ADraw_fontInfoN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint ADraw_textN { get => g.ADraw_textN; set { if (g.ADraw_textN != value) { g.ADraw_textN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint ADraw_textCharN { get => g.ADraw_textCharN; set { if (g.ADraw_textCharN != value) { g.ADraw_textCharN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint ADraw_boxEdgeN { get => g.ADraw_boxEdgeN; set { if (g.ADraw_boxEdgeN != value) { g.ADraw_boxEdgeN = value; ValuesChanged = gChanged = true; } } }
  public virtual float ADraw_fontSize { get => g.ADraw_fontSize; set { if (any(g.ADraw_fontSize != value)) { g.ADraw_fontSize = value; ValuesChanged = gChanged = true; } } }
  public virtual float ADraw_boxThickness { get => g.ADraw_boxThickness; set { if (any(g.ADraw_boxThickness != value)) { g.ADraw_boxThickness = value; ValuesChanged = gChanged = true; } } }
  public virtual float4 ADraw_boxColor { get => g.ADraw_boxColor; set { if (any(g.ADraw_boxColor != value)) { g.ADraw_boxColor = value; ValuesChanged = gChanged = true; } } }
  public virtual uint ARand_N { get => g.ARand_N; set { if (g.ARand_N != value) { g.ARand_N = value; ValuesChanged = gChanged = true; } } }
  public virtual uint ARand_I { get => g.ARand_I; set { if (g.ARand_I != value) { g.ARand_I = value; ValuesChanged = gChanged = true; } } }
  public virtual uint ARand_J { get => g.ARand_J; set { if (g.ARand_J != value) { g.ARand_J = value; ValuesChanged = gChanged = true; } } }
  public virtual uint4 ARand_seed4 { get => g.ARand_seed4; set { if (any(g.ARand_seed4 != value)) { g.ARand_seed4 = value; ValuesChanged = gChanged = true; } } }
  public bool group_UI { get => UI_group_UI?.v ?? false; set { if (UI_group_UI != null) UI_group_UI.v = value; } }
  public bool group_ARand { get => UI_group_ARand?.v ?? false; set { if (UI_group_ARand != null) UI_group_ARand.v = value; } }
  public bool group_Avg { get => UI_group_Avg?.v ?? false; set { if (UI_group_Avg != null) UI_group_Avg.v = value; } }
  public UI_TreeGroup UI_group_UI, UI_group_ARand, UI_group_Avg;
  public UI_float UI_lineThickness, UI_Avg_Val, UI_Avg_Val_Runtime, UI_Avg_Val_TFlops;
  public UI_uint UI_randomNumberN;
  public UI_method UI_Init_randomNumbers;
  public virtual void Init_randomNumbers() { }
  public UI_method UI_Avg;
  public virtual void Avg() { }
  public UI_TreeGroup ui_group_UI => UI_group_UI;
  public UI_float ui_lineThickness => UI_lineThickness;
  public UI_TreeGroup ui_group_ARand => UI_group_ARand;
  public UI_uint ui_randomNumberN => UI_randomNumberN;
  public UI_TreeGroup ui_group_Avg => UI_group_Avg;
  public UI_float ui_Avg_Val => UI_Avg_Val;
  public UI_float ui_Avg_Val_Runtime => UI_Avg_Val_Runtime;
  public UI_float ui_Avg_Val_TFlops => UI_Avg_Val_TFlops;
  [Serializable]
  public class uiData
  {
    public bool siUnits, group_UI, group_ARand, group_Avg;
    public float lineThickness, Avg_Val, Avg_Val_Runtime, Avg_Val_TFlops;
    public uint randomNumberN;
  }
  public virtual void InitBuffers()
  {
    InitBuffers0_GS();
    AddComputeBuffer(ref gARand_Tutorial, nameof(gARand_Tutorial), 1);
    InitKernels();
    SetKernelValues(gARand_Tutorial, nameof(gARand_Tutorial), kernel_Calc_Average, kernel_Calc_Random_Numbers, kernel_ARand_initState, kernel_ARand_initSeed, kernel_ADraw_ABuff_GetIndexes, kernel_ADraw_ABuff_IncSums, kernel_ADraw_ABuff_IncFills1, kernel_ADraw_ABuff_GetFills2, kernel_ADraw_ABuff_GetFills1, kernel_ADraw_ABuff_Get_Bits_Sums, kernel_ADraw_ABuff_GetSums, kernel_ADraw_ABuff_Get_Bits, kernel_ADraw_setDefaultTextInfo, kernel_ADraw_getTextInfo, kernel_ARand_grp_fill_1K, kernel_ARand_grp_init_1K);
    SetKernelValues(gARand_Tutorial, nameof(gARand_Tutorial), kernel_ARand_grp_init_1M);
    AddComputeBuffer(ref ints, nameof(ints), 1);
    AddComputeBuffer(ref randomNumbers, nameof(randomNumbers), randomNumberN);
    AddComputeBuffer(ref ADraw_tab_delimeted_text, nameof(ADraw_tab_delimeted_text), ADraw_textN);
    AddComputeBuffer(ref ADraw_textInfos, nameof(ADraw_textInfos), ADraw_textN);
    AddComputeBuffer(ref ADraw_fontInfos, nameof(ADraw_fontInfos), ADraw_fontInfoN);
    AddComputeBuffer(ref ARand_rs, nameof(ARand_rs), ARand_N);
    InitBuffers1_GS();
    isInitBuffers = true;
  }
  public virtual void InitBuffers0_GS()
  {
    ADraw_InitBuffers0_GS();
    ARand_InitBuffers0_GS();
  }
  public virtual void InitBuffers1_GS()
  {
    ADraw_InitBuffers1_GS();
    ARand_InitBuffers1_GS();
  }
  [HideInInspector] public uint4[] ARand_grp = new uint4[1024];
  [HideInInspector] public uint[] ADraw_ABuff_grp = new uint[1024];
  [HideInInspector] public uint[] ADraw_ABuff_grp0 = new uint[1024];
  [Serializable]
  public struct GARand_Tutorial
  {
    public float lineThickness, Avg_Val, Avg_Val_Runtime, Avg_Val_TFlops, ADraw_fontSize, ADraw_boxThickness;
    public uint randomNumberN, ADraw_ABuff_IndexN, ADraw_ABuff_BitN, ADraw_ABuff_N, ADraw_ABuff_BitN1, ADraw_ABuff_BitN2, ADraw_omitText, ADraw_includeUnicode, ADraw_fontInfoN, ADraw_textN, ADraw_textCharN, ADraw_boxEdgeN, ARand_N, ARand_I, ARand_J;
    public float4 ADraw_boxColor;
    public uint4 ARand_seed4;
  };
  public RWStructuredBuffer<GARand_Tutorial> gARand_Tutorial;
  public struct ADraw_FontInfo { public float2 uvBottomLeft, uvBottomRight, uvTopLeft, uvTopRight; public int advance, bearing, minX, minY, maxX, maxY; };
  public struct ADraw_TextInfo { public float3 p, right, up, p0, p1; public float2 size, uvSize; public float4 color, backColor; public uint justification, textI, quadType, axis; public float height; };
  public RWStructuredBuffer<int> ints;
  public RWStructuredBuffer<uint4> randomNumbers, ARand_rs;
  public RWStructuredBuffer<uint> ADraw_tab_delimeted_text, ADraw_ABuff_Bits, ADraw_ABuff_Sums, ADraw_ABuff_Indexes, ADraw_ABuff_Fills1, ADraw_ABuff_Fills2;
  public RWStructuredBuffer<ADraw_TextInfo> ADraw_textInfos;
  public RWStructuredBuffer<ADraw_FontInfo> ADraw_fontInfos;
  public virtual void AllocData_ints(uint n) => AddComputeBuffer(ref ints, nameof(ints), n);
  public virtual void AssignData_ints(params int[] data) => AddComputeBufferData(ref ints, nameof(ints), data);
  public virtual void AllocData_randomNumbers(uint n) => AddComputeBuffer(ref randomNumbers, nameof(randomNumbers), n);
  public virtual void AssignData_randomNumbers(params uint4[] data) => AddComputeBufferData(ref randomNumbers, nameof(randomNumbers), data);
  public virtual void AllocData_ADraw_tab_delimeted_text(uint n) => AddComputeBuffer(ref ADraw_tab_delimeted_text, nameof(ADraw_tab_delimeted_text), n);
  public virtual void AssignData_ADraw_tab_delimeted_text(params uint[] data) => AddComputeBufferData(ref ADraw_tab_delimeted_text, nameof(ADraw_tab_delimeted_text), data);
  public virtual void AllocData_ADraw_textInfos(uint n) => AddComputeBuffer(ref ADraw_textInfos, nameof(ADraw_textInfos), n);
  public virtual void AssignData_ADraw_textInfos(params ADraw_TextInfo[] data) => AddComputeBufferData(ref ADraw_textInfos, nameof(ADraw_textInfos), data);
  public virtual void AllocData_ADraw_fontInfos(uint n) => AddComputeBuffer(ref ADraw_fontInfos, nameof(ADraw_fontInfos), n);
  public virtual void AssignData_ADraw_fontInfos(params ADraw_FontInfo[] data) => AddComputeBufferData(ref ADraw_fontInfos, nameof(ADraw_fontInfos), data);
  public virtual void AllocData_ARand_rs(uint n) => AddComputeBuffer(ref ARand_rs, nameof(ARand_rs), n);
  public virtual void AssignData_ARand_rs(params uint4[] data) => AddComputeBufferData(ref ARand_rs, nameof(ARand_rs), data);
  public virtual void AllocData_ADraw_ABuff_Bits(uint n) => AddComputeBuffer(ref ADraw_ABuff_Bits, nameof(ADraw_ABuff_Bits), n);
  public virtual void AssignData_ADraw_ABuff_Bits(params uint[] data) => AddComputeBufferData(ref ADraw_ABuff_Bits, nameof(ADraw_ABuff_Bits), data);
  public virtual void AllocData_ADraw_ABuff_Sums(uint n) => AddComputeBuffer(ref ADraw_ABuff_Sums, nameof(ADraw_ABuff_Sums), n);
  public virtual void AssignData_ADraw_ABuff_Sums(params uint[] data) => AddComputeBufferData(ref ADraw_ABuff_Sums, nameof(ADraw_ABuff_Sums), data);
  public virtual void AllocData_ADraw_ABuff_Indexes(uint n) => AddComputeBuffer(ref ADraw_ABuff_Indexes, nameof(ADraw_ABuff_Indexes), n);
  public virtual void AssignData_ADraw_ABuff_Indexes(params uint[] data) => AddComputeBufferData(ref ADraw_ABuff_Indexes, nameof(ADraw_ABuff_Indexes), data);
  public virtual void AllocData_ADraw_ABuff_Fills1(uint n) => AddComputeBuffer(ref ADraw_ABuff_Fills1, nameof(ADraw_ABuff_Fills1), n);
  public virtual void AssignData_ADraw_ABuff_Fills1(params uint[] data) => AddComputeBufferData(ref ADraw_ABuff_Fills1, nameof(ADraw_ABuff_Fills1), data);
  public virtual void AllocData_ADraw_ABuff_Fills2(uint n) => AddComputeBuffer(ref ADraw_ABuff_Fills2, nameof(ADraw_ABuff_Fills2), n);
  public virtual void AssignData_ADraw_ABuff_Fills2(params uint[] data) => AddComputeBufferData(ref ADraw_ABuff_Fills2, nameof(ADraw_ABuff_Fills2), data);
  public Texture2D _PaletteTex;
  public float4 palette(float v) => paletteColor(_PaletteTex, v);
  public float4 palette(float v, float w) => float4(palette(v).xyz, w);

  public Texture2D ADraw_fontTexture;
  public virtual void onRenderObject_LIN(bool show, uint _itemN, ref uint i, ref uint index, ref uint3 LIN) { uint n = 0; if (show) { if (i < (n = _itemN)) LIN = uint3(index, i, 0); LIN.z += n; i -= n; } index++; }
  public virtual void onRenderObject_LIN(uint _itemN, ref uint i, ref uint index, ref uint3 LIN) { onRenderObject_LIN(true, _itemN, ref i, ref index, ref LIN); }
  public virtual uint3 onRenderObject_LIN(uint i) { uint3 LIN = u000; uint index = 0; onRenderObject_LIN(1, ref i, ref index, ref LIN); onRenderObject_LIN(1, ref i, ref index, ref LIN); onRenderObject_LIN(1, ref i, ref index, ref LIN); onRenderObject_LIN(randomNumberN, ref i, ref index, ref LIN); onRenderObject_LIN(12, ref i, ref index, ref LIN); onRenderObject_LIN(ADraw_textN, ref i, ref index, ref LIN); onRenderObject_LIN(ADraw_boxEdgeN, ref i, ref index, ref LIN); return LIN; }
  public virtual uint3 onRenderObject_LIN_Quads(uint i) { uint3 LIN = u000; uint index = 0; onRenderObject_LIN(1, ref i, ref index, ref LIN); onRenderObject_LIN(1, ref i, ref index, ref LIN); onRenderObject_LIN(1, ref i, ref index, ref LIN); onRenderObject_LIN(randomNumberN, ref i, ref index, ref LIN); onRenderObject_LIN(12, ref i, ref index, ref LIN); onRenderObject_LIN(ADraw_textN, ref i, ref index, ref LIN); onRenderObject_LIN(ADraw_boxEdgeN, ref i, ref index, ref LIN); return LIN; }
  public virtual uint3 onRenderObject_LIN_Points(uint i) { uint3 LIN = u000; return LIN; }
  [HideInInspector] public int kernel_Calc_Average; [numthreads(numthreads1, 1, 1)] protected void Calc_Average(uint3 id) { unchecked { if (id.x < randomNumberN) Calc_Average_GS(id); } }
  public virtual void Calc_Average_GS(uint3 id) { }
  [HideInInspector] public int kernel_Calc_Random_Numbers; [numthreads(numthreads1, 1, 1)] protected void Calc_Random_Numbers(uint3 id) { unchecked { if (id.x < randomNumberN) Calc_Random_Numbers_GS(id); } }
  public virtual void Calc_Random_Numbers_GS(uint3 id) { }
  [HideInInspector] public int kernel_ARand_initState; [numthreads(numthreads1, 1, 1)] protected void ARand_initState(uint3 id) { unchecked { if (id.x < ARand_I) ARand_initState_GS(id); } }
  [HideInInspector] public int kernel_ARand_initSeed; [numthreads(numthreads1, 1, 1)] protected void ARand_initSeed(uint3 id) { unchecked { if (id.x < ARand_N) ARand_initSeed_GS(id); } }
  [HideInInspector] public int kernel_ADraw_ABuff_GetIndexes; [numthreads(numthreads1, 1, 1)] protected void ADraw_ABuff_GetIndexes(uint3 id) { unchecked { if (id.x < ADraw_ABuff_BitN) ADraw_ABuff_GetIndexes_GS(id); } }
  [HideInInspector] public int kernel_ADraw_ABuff_IncSums; [numthreads(numthreads1, 1, 1)] protected void ADraw_ABuff_IncSums(uint3 id) { unchecked { if (id.x < ADraw_ABuff_BitN) ADraw_ABuff_IncSums_GS(id); } }
  [HideInInspector] public int kernel_ADraw_ABuff_IncFills1; [numthreads(numthreads1, 1, 1)] protected void ADraw_ABuff_IncFills1(uint3 id) { unchecked { if (id.x < ADraw_ABuff_BitN1) ADraw_ABuff_IncFills1_GS(id); } }
  [HideInInspector] public int kernel_ADraw_ABuff_GetFills2; [numthreads(numthreads1, 1, 1)] protected IEnumerator ADraw_ABuff_GetFills2(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < ADraw_ABuff_BitN2) yield return StartCoroutine(ADraw_ABuff_GetFills2_GS(grp_tid, grp_id, id, grpI)); } }
  [HideInInspector] public int kernel_ADraw_ABuff_GetFills1; [numthreads(numthreads1, 1, 1)] protected IEnumerator ADraw_ABuff_GetFills1(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < ADraw_ABuff_BitN1) yield return StartCoroutine(ADraw_ABuff_GetFills1_GS(grp_tid, grp_id, id, grpI)); } }
  [HideInInspector] public int kernel_ADraw_ABuff_Get_Bits_Sums; [numthreads(numthreads1, 1, 1)] protected IEnumerator ADraw_ABuff_Get_Bits_Sums(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < ADraw_ABuff_BitN) yield return StartCoroutine(ADraw_ABuff_Get_Bits_Sums_GS(grp_tid, grp_id, id, grpI)); } }
  [HideInInspector] public int kernel_ADraw_ABuff_GetSums; [numthreads(numthreads1, 1, 1)] protected IEnumerator ADraw_ABuff_GetSums(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < ADraw_ABuff_BitN) yield return StartCoroutine(ADraw_ABuff_GetSums_GS(grp_tid, grp_id, id, grpI)); } }
  [HideInInspector] public int kernel_ADraw_ABuff_Get_Bits; [numthreads(numthreads1, 1, 1)] protected void ADraw_ABuff_Get_Bits(uint3 id) { unchecked { if (id.x < ADraw_ABuff_BitN) ADraw_ABuff_Get_Bits_GS(id); } }
  [HideInInspector] public int kernel_ADraw_setDefaultTextInfo; [numthreads(numthreads1, 1, 1)] protected void ADraw_setDefaultTextInfo(uint3 id) { unchecked { if (id.x < ADraw_textN) ADraw_setDefaultTextInfo_GS(id); } }
  [HideInInspector] public int kernel_ADraw_getTextInfo; [numthreads(numthreads1, 1, 1)] protected void ADraw_getTextInfo(uint3 id) { unchecked { if (id.x < ADraw_textN) ADraw_getTextInfo_GS(id); } }
  [HideInInspector] public int kernel_ARand_grp_fill_1K; [numthreads(numthreads1, 1, 1)] protected IEnumerator ARand_grp_fill_1K(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < ARand_N) yield return StartCoroutine(ARand_grp_fill_1K_GS(grp_tid, grp_id, id, grpI)); } }
  public virtual IEnumerator ARand_grp_fill_1K_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { yield return null; }
  [HideInInspector] public int kernel_ARand_grp_init_1K; [numthreads(numthreads1, 1, 1)] protected IEnumerator ARand_grp_init_1K(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < ARand_N / 1024) yield return StartCoroutine(ARand_grp_init_1K_GS(grp_tid, grp_id, id, grpI)); } }
  public virtual IEnumerator ARand_grp_init_1K_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { yield return null; }
  [HideInInspector] public int kernel_ARand_grp_init_1M; [numthreads(numthreads1, 1, 1)] protected IEnumerator ARand_grp_init_1M(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < ARand_N / 1024 / 1024) yield return StartCoroutine(ARand_grp_init_1M_GS(grp_tid, grp_id, id, grpI)); } }
  public virtual IEnumerator ARand_grp_init_1M_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { yield return null; }
  public virtual v2f vert_GS(uint i, uint j, v2f o)
  {
    uint3 LIN = onRenderObject_LIN(i); int index = -1, level = ((int)LIN.x); i = LIN.y;
    if (level == ++index) { o = vert_Draw_Random_Signal(i, j, o); o.tj.x = 0; }
    else if (level == ++index) { o = vert_Draw_Calc_Avg(i, j, o); o.tj.x = 0; }
    else if (level == ++index) { o = vert_Draw_Avg(i, j, o); o.tj.x = 0; }
    else if (level == ++index) { o = vert_Draw_Pnts(i, j, o); o.tj.x = 0; }
    else if (level == ++index) { o = vert_Draw_Border(i, j, o); o.tj.x = 0; }
    else if (level == ++index) { o = vert_ADraw_Text(i, j, o); o.tj.x = 0; }
    else if (level == ++index) { o = vert_ADraw_Box(i, j, o); o.tj.x = 0; }
    return o;
  }
  public v2f vert(uint i, uint j)
  {
    v2f o = default;
    o.pos = o.color = o.tj = o.tk = f0000; o.normal = o.p0 = o.p1 = o.wPos = f000; o.uv = f00; o.ti = float4(i, j, 0, 0);
    return vert_GS(i, j, o);
  }
  public virtual v2f vert_Draw_Random_Signal(uint i, uint j, v2f o) => o;
  public virtual v2f vert_Draw_Calc_Avg(uint i, uint j, v2f o) => o;
  public virtual v2f vert_Draw_Avg(uint i, uint j, v2f o) => o;
  public virtual v2f vert_Draw_Pnts(uint i, uint j, v2f o) => o;
  public virtual v2f vert_Draw_Border(uint i, uint j, v2f o) => o;
  public virtual bool onRenderObject_GS_N(bool cpu)
  {
    RenderQuads(material, onRenderObject_LIN_Quads(0).z);
    RenderPoints(material, onRenderObject_LIN_Points(0).z);
    return true;
  }
  public override bool onRenderObject()
  {
    if (gARand_Tutorial == null) return false;
    bool render = true, cpu = false;
    onRenderObject_GS(ref render, ref cpu);
    g_SetData();
    if (!render) return false;
    if (cpu) Cpu(material, new { gARand_Tutorial }, new { ints }, new { randomNumbers }, new { ADraw_tab_delimeted_text }, new { ADraw_textInfos }, new { ADraw_fontInfos }, new { ARand_rs }, new { ARand_grp }, new { ADraw_ABuff_Bits }, new { ADraw_ABuff_Sums }, new { ADraw_ABuff_Indexes }, new { ADraw_ABuff_grp }, new { ADraw_ABuff_grp0 }, new { ADraw_ABuff_Fills1 }, new { ADraw_ABuff_Fills2 }, new { ADraw_fontTexture }, new { _PaletteTex });
    else Gpu(material, new { gARand_Tutorial }, new { ints }, new { randomNumbers }, new { ADraw_tab_delimeted_text }, new { ADraw_textInfos }, new { ADraw_fontInfos }, new { ARand_rs }, new { ARand_grp }, new { ADraw_ABuff_Bits }, new { ADraw_ABuff_Sums }, new { ADraw_ABuff_Indexes }, new { ADraw_ABuff_grp }, new { ADraw_ABuff_grp0 }, new { ADraw_ABuff_Fills1 }, new { ADraw_ABuff_Fills2 }, new { ADraw_fontTexture }, new { _PaletteTex });
    return onRenderObject_GS_N(cpu);
  }
  public virtual void onRenderObject_GS(ref bool render, ref bool cpu)
  {
    ADraw_onRenderObject_GS(ref render, ref cpu);
    ARand_onRenderObject_GS(ref render, ref cpu);
  }
  public virtual float4 frag_GS(v2f i, float4 color) => frag_ADraw_GS(i, color);
  public virtual float4 frag(v2f i)
  {
    float4 color = i.color;
    color = frag_GS(i, color);
    if (color.a == 0) Discard(0);
    return color;
  }
  #region <ADraw>
  public float ADraw_wrapJ(uint j, uint n) => ((j + n) % 6) / 3;
  public uint2 ADraw_JQuadu(uint j) => uint2(j + 2, j + 1) / 3 % 2;
  public float2 ADraw_JQuadf(uint j) => (float2)ADraw_JQuadu(j);
  public float4 ADraw_Number_quadPoint(float rx, float ry, uint j) { float2 p = ADraw_JQuadf(j); return float4((2 * p.x - 1) * rx, (1 - 2 * p.y) * ry, 0, 0); }
  public float4 ADraw_Sphere_quadPoint(float r, uint j) => r * float4(2 * ADraw_JQuadf(j) - 1, 0, 0);
  public float2 ADraw_Line_uv(float3 p0, float3 p1, float r, uint j) { float2 p = ADraw_JQuadf(j); return float2(length(p1 - p0) * (1 - p.y), (1 - 2 * p.x) * r); }
  public float2 ADraw_LineArrow_uv(float dpf, float3 p0, float3 p1, float r, uint j) { float2 p = ADraw_JQuadf(j); return float2((length(p1 - p0) + 2 * r) * (1 - p.y) - r, (1 - 2 * p.x) * r * dpf); }
  public float4 ADraw_LineArrow_p4(float dpf, float3 p0, float3 p1, float3 p3, float r, uint j) { float2 p = ADraw_JQuadf(j); float3 dp = normalize(cross(p1 - p0, p3 - p0)) * r * dpf; return float4(p.y * (p0 - p1) + p1 + dp * (1 - 2 * p.x), 1); }
  public uint vert_ADraw_index(v2f o) => roundu(o.ti.x);
  public v2f vert_ADraw_index(uint i, v2f o) { o.ti.x = i; return o; }
  public uint vert_ADraw_drawType(v2f o) => roundu(o.ti.z);
  public v2f vert_ADraw_drawType(uint drawType, v2f o) { o.ti.z = drawType; return o; }
  public v2f vert_ADraw_Point(float3 p, float4 color, uint i, v2f o) { o.pos = UnityObjectToClipPos(float4(p, 1)); o.ti = float4(i, 0, ADraw_Draw_Point, 0); o.color = color; return o; }
  public v2f vert_ADraw_Sphere(float3 p, float r, float4 color, uint i, uint j, v2f o)
  {
    float4 p4 = float4(p, 1), quadPoint = ADraw_Sphere_quadPoint(r, j);
    o.pos = mul(UNITY_MATRIX_P, mul(UNITY_MATRIX_V, p4) + quadPoint); o.wPos = p;
    o.uv = quadPoint.xy / r; o.normal = -f001; o.color = color;
    o.ti = float4(i, 0, ADraw_Draw_Sphere, 0);
    o = vert_ADraw_index(i, vert_ADraw_drawType(ADraw_Draw_Sphere, o));
    return o;
  }
  public v2f vert_ADraw_LineArrow(float dpf, float3 p0, float3 p1, float r, float4 color, uint i, uint j, v2f o) { o.p0 = p0; o.p1 = p1; o.uv = ADraw_LineArrow_uv(dpf, p0, p1, r, j); o.pos = UnityObjectToClipPos(ADraw_LineArrow_p4(dpf, p0, p1, _WorldSpaceCameraPos, r, j)); o.color = color; o.ti = float4(i, 0, dpf == 1 ? ADraw_Draw_Line : ADraw_Draw_Arrow, r); return o; }
  public v2f vert_ADraw_Line(float3 p0, float3 p1, float r, float4 color, uint i, uint j, v2f o) { o.p0 = p0; o.p1 = p1; o.uv = ADraw_Line_uv(p0, p1, r, j); o.pos = UnityObjectToClipPos(ADraw_LineArrow_p4(1, p0, p1, _WorldSpaceCameraPos, r, j)); o.color = color; o.ti = float4(i, 0, ADraw_Draw_Line, r); return o; }
  public v2f vert_ADraw_LineSegment(float3 p0, float3 p1, float r, float4 color, uint i, uint j, v2f o) { o = vert_ADraw_LineArrow(1, p0, p1, r, color, i, j, o); o.ti.z = ADraw_Draw_LineSegment; return o; }
  public v2f vert_ADraw_Arrow(float3 p0, float3 p1, float r, float4 color, uint i, uint j, v2f o) { return vert_ADraw_LineArrow(3, p0, p1, r, color, i, j, o); }
  public v2f vert_ADraw_BoxFrame(float3 c0, float3 c1, float lineRadius, float4 color, uint i, uint j, v2f o) { float3 p0, p1; switch (i) { case 0: p0 = c0; p1 = c0 * f110 + c1 * f001; break; case 1: p0 = c0 * f110 + c1 * f001; p1 = c0 * f100 + c1 * f011; break; case 2: p0 = c0 * f100 + c1 * f011; p1 = c0 * f101 + c1 * f010; break; case 3: p0 = c0 * f101 + c1 * f010; p1 = c0; break; case 4: p0 = c0 * f011 + c1 * f100; p1 = c0 * f010 + c1 * f101; break; case 5: p0 = c0 * f010 + c1 * f101; p1 = c1; break; case 6: p0 = c1; p1 = c0 * f001 + c1 * f110; break; case 7: p0 = c0 * f001 + c1 * f110; p1 = c0 * f011 + c1 * f100; break; case 8: p0 = c0; p1 = c0 * f011 + c1 * f100; break; case 9: p0 = c0 * f101 + c1 * f010; p1 = c0 * f001 + c1 * f110; break; case 10: p0 = c0 * f100 + c1 * f011; p1 = c1; break; default: p0 = c0 * f110 + c1 * f001; p1 = c0 * f010 + c1 * f101; break; } return vert_ADraw_Line(p0, p1, lineRadius, color, i, j, o); }
  public v2f vert_ADraw_Quad(float3 p0, float3 p1, float3 p2, float3 p3, float4 color, uint i, uint j, v2f o) { float3 p = o.wPos = j % 5 == 0 ? p3 : j == 1 ? p2 : j == 4 ? p0 : p1, n = cross(p1 - p0, p0 - p3); o.color = color; o.pos = UnityObjectToClipPos(p); o.uv = float2(ADraw_wrapJ(j, 2), ADraw_wrapJ(j, 4)); o.normal = n; o.ti = float4(i, 0, ADraw_Draw_Texture_2D, 0); return o; }
  public v2f vert_ADraw_Legend(uint i, uint j, v2f o) { float h = 8; float3 c = f110 * 10000, p0 = c + float3(0.4f, -h / 2, 0), p1 = p0 + f100 * 0.4f, p2 = p1 + h * f010, p3 = p0 + h * f010; return vert_ADraw_Quad(p0, p1, p2, p3, WHITE, i, j, o); }
  public v2f vert_ADraw_WebCam(float3 p0, float3 p1, float3 p2, float3 p3, float4 color, uint i, uint j, v2f o) { float3 p = o.wPos = j % 5 == 0 ? p3 : j == 1 ? p2 : j == 4 ? p0 : p1, n = cross(p1 - p0, p0 - p3); o.color = color; o.pos = UnityObjectToClipPos(p); o.uv = float2(ADraw_wrapJ(j, 2), ADraw_wrapJ(j, 4)); o.normal = n; o.ti = float4(i, 0, ADraw_Draw_WebCam, 0); return o; }
  public v2f vert_ADraw_Cube(float3 p, float3 r, float4 color, uint i, uint j, v2f o) { float3 p0, p1, p2, p3; switch (i % 6) { case 0: p0 = f___; p1 = f1__; p2 = f11_; p3 = f_1_; break; case 1: p0 = f1_1; p1 = f__1; p2 = f_11; p3 = f111; break; case 2: p0 = f__1; p1 = f1_1; p2 = f1__; p3 = f___; break; case 3: p0 = f_1_; p1 = f11_; p2 = f111; p3 = f_11; break; case 4: p0 = f__1; p1 = f___; p2 = f_1_; p3 = f_11; break; default: p0 = f1__; p1 = f1_1; p2 = f111; p3 = f11_; break; } return vert_ADraw_Quad(p0 * r + p, p1 * r + p, p2 * r + p, p3 * r + p, color, i, j, o); }
  public v2f vert_ADraw_Cube(float3 p, float r, float4 color, uint i, uint j, v2f o) { return vert_ADraw_Cube(p, f111 * r, color, i, j, o); }
  public v2f vert_ADraw_Signal(float3 p0, float3 p1, float r, uint i, uint j, v2f o) { float2 p = ADraw_JQuadf(j); o.p0 = p0; o.p1 = p1; o.uv = f11 - p.yx; o.pos = UnityObjectToClipPos(ADraw_LineArrow_p4(1, p0, p1, _WorldSpaceCameraPos, r, j)); o.ti = float4(i, 0, ADraw_Draw_Signal, r); return o; }
  public v2f vert_ADraw_Signal(float3 p0, float3 p1, float r, float4 color, int drawType, float thickness, uint i, uint j, v2f o) { float2 p = ADraw_JQuadf(j); o.p0 = p0; o.p1 = p1; o.uv = f11 - p.yx; o.pos = UnityObjectToClipPos(ADraw_LineArrow_p4(1, p0, p1, _WorldSpaceCameraPos, r, j)); o.ti = float4(i, 0, ADraw_Draw_Signal, r); o.tj = float4(distance(p0, p1), r, drawType, thickness); o.color = color; return o; }
  public virtual uint ADraw_SignalSmpN(uint chI) => 1024;
  public virtual float4 ADraw_SignalColor(uint chI, uint smpI) => YELLOW;
  public virtual float4 ADraw_SignalBackColor(uint chI, uint smpI) => float4(1, 1, 1, 0.2f);
  public virtual float ADraw_SignalSmpV(uint chI, uint smpI) => 0;
  public virtual float ADraw_SignalThickness(uint chI, uint smpI) => 0.004f;
  public virtual float ADraw_SignalFillCrest(uint chI, uint smpI) => 1;
  public virtual bool ADraw_SignalMarkerColor(uint stationI, float station_smpI, float4 color, uint chI, float smpI, uint display_x, out float4 return_color)
  {
    float d = abs(smpI - station_smpI + display_x);
    return (return_color = chI == stationI && d < 1 ? float4(color.xyz * (1 - d), 1) : f0000).w > 0;
  }
  public virtual float4 ADraw_SignalMarker(uint chI, float smpI) => f0000;
  public virtual float4 frag_ADraw_Signal(v2f i)
  {
    uint chI = roundu(i.ti.x), SmpN = ADraw_SignalSmpN(chI);
    float2 uv = i.uv, wh = float2(distance(i.p1, i.p0), i.ti.w);
    float smpI = lerp(0, SmpN, uv.x), y = lerp(-1, 1, uv.y), h = wh.y / wh.x * SmpN, thick = ADraw_SignalThickness(chI, (uint)smpI) * SmpN, d = float_PositiveInfinity;
    uint SmpI = (uint)smpI, dSmpI = ceilu(thick) + 1, SmpI0 = (uint)max(0, (int)SmpI - (int)dSmpI), SmpI1 = min(SmpN - 1, SmpI + dSmpI);
    float2 p0 = float2(smpI, y * h), q0 = float2(SmpI0, (h - thick) * ADraw_SignalSmpV(chI, SmpI0)), q1;
    for (uint sI = SmpI0; sI < SmpI1; sI++) { q1 = float2(sI + 1, (h - thick) * ADraw_SignalSmpV(chI, sI + 1)); d = min(d, LineSegDist(q0, q1, p0)); q0 = q1; }
    float4 c = ADraw_SignalColor(chI, SmpI);
    float v = 0.9f * lerp(ADraw_SignalSmpV(chI, SmpI), ADraw_SignalSmpV(chI, SmpI + 1), frac(smpI)), crest = ADraw_SignalFillCrest(chI, SmpI);
    float4 marker = ADraw_SignalMarker(chI, smpI);
    if (marker.w > 0) return marker;
    if (crest >= 0 ? y > crest && y < v : y < crest && y > v) return c;
    if (d < thick) return float4(c.xyz * (1 - d / thick), 1);
    return ADraw_SignalBackColor(chI, SmpI);
  }
  public float4 frag_ADraw_Sphere(v2f i) { float2 uv = i.uv; float r = dot(uv, uv); float4 color = i.color; if (r > 1.0f || color.a == 0) return f0000; float3 n = new float3(uv, r - 1), _LightDir = new float3(0.321f, 0.766f, -0.557f); float lightAmp = max(0.0f, dot(n, _LightDir)); float4 diffuse_Light = (lightAmp + UNITY_LIGHTMODEL_AMBIENT) * color; float spec = max(0, (lightAmp - 0.95f) / 0.05f); color = lerp(diffuse_Light, f1111, spec / 4); color.a = 1; return color; }
  public float4 frag_ADraw_Line(v2f i) { float3 p0 = i.p0, p1 = i.p1; float lineRadius = i.ti.w; float2 uv = i.uv; float r = dot(uv, uv), r2 = lineRadius * lineRadius; float4 color = i.color; float3 p10 = p1 - p0; float lp10 = length(p10); if (uv.x < 0) r /= r2; else if (uv.x > lp10) { uv.x -= lp10; r = dot(uv, uv) / r2; } else { uv.x = 0; r = dot(uv, uv) / r2; } if (r > 1.0f || color.a == 0) return f0000; float3 n = new float3(uv, r - 1), _LightDir = new float3(0.321f, 0.766f, -0.557f); float lightAmp = max(0.0f, dot(n, _LightDir)); float4 diffuse_Light = (lightAmp + UNITY_LIGHTMODEL_AMBIENT) * color; float spec = max(0, (lightAmp - 0.95f) / 0.05f); color = lerp(diffuse_Light, f1111, spec / 4); color.a = 1; return color; }
  public float4 frag_ADraw_Arrow(v2f i) { float3 p0 = i.p0, p1 = i.p1; float lineRadius = i.ti.w; float2 uv = i.uv; float r = dot(uv, uv), r2 = lineRadius * lineRadius; float4 color = i.color; float3 p10 = p1 - p0; float lp10 = length(p10); if (uv.x < 0) r /= r2; else if (uv.x > lp10 - lineRadius * 3 && abs(uv.y) > lineRadius) { uv.x -= lp10; uv = rotate_sc(uv, -sign(uv.y) * 0.5f, 0.866025404f); uv.x = 0; r = dot(uv, uv) / r2; } else if (uv.x > lp10) { uv.x -= lp10; r = dot(uv, uv) / r2; } else { uv.x = 0; r = dot(uv, uv) / r2; } if (r > 1.0f || color.a == 0) return f0000; float3 n = new float3(uv, r - 1), _LightDir = new float3(0.321f, 0.766f, -0.557f); float lightAmp = max(0.0f, dot(n, _LightDir)); float4 diffuse_Light = (lightAmp + UNITY_LIGHTMODEL_AMBIENT) * color; float spec = max(0, (lightAmp - 0.95f) / 0.05f); color = lerp(diffuse_Light, f1111, spec / 4); color.a = 1; return color; }
  public float4 frag_ADraw_LineSegment(v2f i) { float3 p0 = i.p0, p1 = i.p1; float lineRadius = i.ti.w; float2 uv = i.uv; float r = dot(uv, uv), r2 = lineRadius * lineRadius; float4 color = i.color; float3 p10 = p1 - p0; float lp10 = length(p10); uv.x = 0; r = dot(uv, uv) / r2; if (r > 1.0f || color.a == 0) return f0000; float3 n = new float3(uv, r - 1), _LightDir = new float3(0.321f, 0.766f, -0.557f); float lightAmp = max(0.0f, dot(n, _LightDir)); float4 diffuse_Light = (lightAmp + UNITY_LIGHTMODEL_AMBIENT) * color; float spec = max(0, (lightAmp - 0.95f) / 0.05f); color = lerp(diffuse_Light, f1111, spec / 4); color.a = 1; return color; }
  public float4 frag_ADraw_Quad(Texture2D t, v2f i) => i.color * tex2Dlod(t, new float4(i.uv, f00));
  public float4 frag_ADraw_Quad(RWStructuredBuffer<Color> v, v2f i, uint2 _image_size) => (float4)(i.color * v[id_to_i((uint2)(i.uv * (float2)_image_size), _image_size)]);
  public float4 frag_ADraw_Quad(RWStructuredBuffer<Color32> v, v2f i, uint2 _image_size) => (float4)(i.color * c32_f4(v[id_to_i((uint2)(i.uv * (float2)_image_size), _image_size)]));
  public float4 frag_ADraw_Mesh(v2f i) { float3 p = i.wPos; float4 color = i.color; color.xyz += dot(i.normal, _WorldSpaceLightPos0.xyz) / 2; return saturate(color); }
  public virtual bool ADraw_ABuff_IsBitOn(uint i) { uint c = ADraw_Byte(i); return c == ADraw_TB || c == ADraw_LF; }
  public class ADraw_TText3D
  {
    public string text; public float3 p, right, up, p0, p1; public float h; public float4 color, backColor;
    public ADraw_Text_QuadType quadType; public ADraw_TextAlignment textAlignment; public uint axis;
  }
  public Font ADraw_font { get; set; }
  public virtual void ADraw_InitBuffers0_GS()
  {
    if (ADraw_omitText) ADraw_fontInfoN = 0;
    else { ADraw_font ??= Resources.Load<Font>("Arial Font/arial Unicode"); ADraw_fontTexture = (Texture2D)ADraw_font.material.mainTexture; ADraw_fontInfoN = ADraw_includeUnicode ? ADraw_font.characterInfo.uLength() : 128 - 32; }
  }
  public virtual void ADraw_InitBuffers1_GS()
  {
    for (int i = 0; i < ADraw_fontInfoN; i++)
    {
      var c = ADraw_font.characterInfo[i];
      if (i == 0) ADraw_fontSize = c.size;
      if (c.index < 128) ADraw_fontInfos[c.index - 32] = new ADraw_FontInfo() { uvBottomLeft = c.uvBottomLeft, uvBottomRight = c.uvBottomRight, uvTopLeft = c.uvTopLeft, uvTopRight = c.uvTopRight, advance = max(c.advance, roundi(c.glyphWidth * 1.05f)), bearing = c.bearing, minX = c.minX, minY = c.minY, maxX = c.maxX, maxY = c.maxY };
    }
    ADraw_fontInfos.SetData();
  }
  public float ADraw_GetTextHeight() { return 0.1f; }
  public uint ADraw_GetText_ch(float v, uint _I, uint neg, uint uN) { return _I < neg ? 13u : _I < uN + neg ? 16u + flooru(10 * frac(abs(v) / pow10(uN - _I + neg))) : _I == uN + neg ? 14u : 16u + flooru(10 * frac(abs(v) * pow10(_I - neg - uN - 1))); }
  public uint ADraw_Byte(uint i) { return TextByte(ADraw_tab_delimeted_text, i); }
  public uint2 ADraw_Get_text_indexes(uint textI) { return uint2(textI == 0 ? 0 : ADraw_ABuff_Indexes[textI - 1] + 1, textI < ADraw_ABuff_IndexN ? ADraw_ABuff_Indexes[textI] : ADraw_textCharN); }
  public float ADraw_GetTextWidth(float v, uint decimalN)
  {
    float textWidth = 0, p10 = pow10(decimalN);
    v = round(v * p10) / p10;
    uint u = flooru(abs(v)), uN = u == 0 ? 1 : flooru(log10(abs(v)) + 1), numDigits = uN + decimalN + (decimalN == 0 ? 0 : 1u), neg = v < 0 ? 1u : 0;
    for (uint _I = 0; _I < numDigits + neg; _I++)
    {
      uint ch = ADraw_GetText_ch(v, _I, neg, uN);
      ADraw_FontInfo f = ADraw_fontInfos[ch];
      float2 mn = new float2(f.minX, f.minY) / ADraw_fontSize, mx = new float2(f.maxX, f.maxY) / ADraw_fontSize, range = mx - mn;
      float dx = f.advance / ADraw_fontSize;
      textWidth += dx;
    }
    return textWidth;
  }
  public float3 ADraw_GetTextWidth(float3 v, uint3 decimalN) => new float3(ADraw_GetTextWidth(v.x, decimalN.x), ADraw_GetTextWidth(v.y, decimalN.y), ADraw_GetTextWidth(v.z, decimalN.z));
  public List<ADraw_TText3D> ADraw_texts = new List<ADraw_TText3D>();
  public void ADraw_ClearTexts() => ADraw_texts.Clear();
  public virtual void ADraw_AddText(string text, float3 p, float3 right, float3 up, float4 color, float4 backColor, float h, ADraw_Text_QuadType quadType, ADraw_TextAlignment textAlignment, float3 p0, float3 p1, uint axis = 0) => ADraw_texts.Add(new ADraw_TText3D() { text = text, p = p, right = right, up = up, color = color, backColor = backColor, h = h, quadType = quadType, textAlignment = textAlignment, p0 = p0, p1 = p1, axis = axis });
  public void ADraw_AddText(string text, float3 p, float3 right, float3 up, float4 color, float4 backColor, float h, ADraw_Text_QuadType quadType, ADraw_TextAlignment textAlignment) => ADraw_AddText(text, p, right, up, color, backColor, h, quadType, textAlignment, f000, f000, 0);
  public virtual ADraw_TextInfo ADraw_textInfo(uint i) { return ADraw_textInfos[i]; }
  public virtual void ADraw_textInfo(uint i, ADraw_TextInfo t) { ADraw_textInfos[i] = t; }
  public int ADraw_ExtraTextN = 0;
  public virtual void ADraw_RebuildExtraTexts() { ADraw_BuildTexts(); ADraw_BuildTexts(); }
  public virtual void ADraw_BuildExtraTexts() { }
  public virtual void ADraw_BuildTexts()
  {
    SetBytes(ref ADraw_tab_delimeted_text, (ADraw_texts.Select(a => a.text).Join("\n") + "\n").ToBytes(Encoding.UTF8));
    AddComputeBuffer(ref ADraw_textInfos, nameof(ADraw_textInfo), ADraw_textN = max(1, ADraw_ABuff_Run(ADraw_textCharN = ADraw_tab_delimeted_text.uLength * 4)));
    for (uint i = 0; i < ADraw_texts.Count; i++)
    {
      var t = ADraw_texts[(int)i];
      var ti = ADraw_textInfo(i);
      ti.color = t.color; ti.backColor = t.backColor; ti.p = t.p; ti.p0 = t.p0; ti.p1 = t.p1; ti.height = t.h;
      ti.quadType = (uint)t.quadType; ti.axis = t.axis; ti.right = t.right; ti.up = t.up; ti.justification = (uint)t.textAlignment;
      ADraw_textInfo(i, ti);
    }
    if (ADraw_ABuff_Indexes == null || ADraw_ABuff_Indexes.computeBuffer == null) { AddComputeBuffer(ref ADraw_ABuff_Indexes, nameof(ADraw_ABuff_Indexes), 1); ADraw_ABuff_Indexes.SetData(new uint[] { 0 }); }
    if (ADraw_fontInfos != null && ADraw_ABuff_Indexes != null) { computeShader.SetBuffer(kernel_ADraw_getTextInfo, nameof(ADraw_textInfos), ADraw_textInfos); Gpu_ADraw_getTextInfo(); }
    if (ADraw_ExtraTextN > 0 && ADraw_texts.Count >= ADraw_ExtraTextN) ADraw_texts.RemoveRange(ADraw_texts.Count - ADraw_ExtraTextN, ADraw_ExtraTextN);
    int n = ADraw_texts.Count;
    ADraw_BuildExtraTexts();
    ADraw_ExtraTextN = ADraw_texts.Count - n;
  }
  public virtual IEnumerator ADraw_BuildTexts_Coroutine()
  {
    SetBytes(ref ADraw_tab_delimeted_text, (ADraw_texts.Select(a => a.text).Join("\n") + "\n").ToBytes(Encoding.UTF8));
    AddComputeBuffer(ref ADraw_textInfos, nameof(ADraw_textInfo), ADraw_textN = max(1, ADraw_ABuff_Run(ADraw_textCharN = ADraw_tab_delimeted_text.uLength * 4)));
    for (uint i = 0; i < ADraw_texts.Count; i++)
    {
      var t = ADraw_texts[(int)i];
      var ti = ADraw_textInfo(i);
      ti.color = t.color; ti.backColor = t.backColor; ti.p = t.p; ti.p0 = t.p0; ti.p1 = t.p1; ti.height = t.h;
      ti.quadType = (uint)t.quadType; ti.axis = t.axis; ti.right = t.right; ti.up = t.up; ti.justification = (uint)t.textAlignment;
      ADraw_textInfo(i, ti);
      if (i % 1000 == 0) { progress(i, (uint)ADraw_texts.Count); yield return null; }
    }
    progress(0);
    if (ADraw_ABuff_Indexes == null || ADraw_ABuff_Indexes.computeBuffer == null) { AddComputeBuffer(ref ADraw_ABuff_Indexes, nameof(ADraw_ABuff_Indexes), 1); ADraw_ABuff_Indexes.SetData(new uint[] { 0 }); }
    if (ADraw_fontInfos != null && ADraw_ABuff_Indexes != null) { computeShader.SetBuffer(kernel_ADraw_getTextInfo, nameof(ADraw_textInfos), ADraw_textInfos); Gpu_ADraw_getTextInfo(); }
  }
  public virtual void ADraw_BuildTexts_Default()
  {
    SetBytes(ref ADraw_tab_delimeted_text, (ADraw_texts.Select(a => a.text).Join("\n") + "\n").ToBytes(Encoding.UTF8));
    AddComputeBuffer(ref ADraw_textInfos, nameof(ADraw_textInfo), ADraw_textN = max(1, ADraw_ABuff_Run(ADraw_textCharN = ADraw_tab_delimeted_text.uLength * 4)));
    if (ADraw_texts.Count > 0)
    {
      var t = ADraw_texts[0];
      var ti = ADraw_textInfo(0);
      ti.color = t.color; ti.backColor = t.backColor; ti.p = t.p; ti.p0 = t.p0; ti.p1 = t.p1; ti.height = t.h;
      ti.quadType = (uint)t.quadType; ti.axis = t.axis; ti.right = t.right; ti.up = t.up; ti.justification = (uint)t.textAlignment;
      ADraw_textInfo(0, ti);
      Gpu_ADraw_setDefaultTextInfo();
    }
    if (ADraw_ABuff_Indexes == null || ADraw_ABuff_Indexes.computeBuffer == null) { AddComputeBuffer(ref ADraw_ABuff_Indexes, nameof(ADraw_ABuff_Indexes), 1); ADraw_ABuff_Indexes.SetData(new uint[] { 0 }); }
    if (ADraw_fontInfos != null && ADraw_ABuff_Indexes != null) Gpu_ADraw_getTextInfo();
  }
  public void ADraw_AddXAxis(string title, float titleHeight, float3 p0, float3 p1, float3 tRight, float3 tUp, float4 color, float2 vRange, string format, float textHeight, float3 right, float3 up, float3 margin, ADraw_Text_QuadType quadType, float3 mn, float3 mx, uint axis = 0)
  {
    if (extent(vRange) <= 0) return;
    uint decimalN = ADraw_GetDecimalN(vRange) + (uint)format.Length, maxTickN = ADraw_GetXAxisN(textHeight, decimalN, p1.xy - p0.xy);
    float range = NiceNum(abs(extent(vRange)), false), dxi = NiceNum(range / (maxTickN - 1), true), xi0 = ceil(vRange.x / dxi) * dxi, xi1 = floor(vRange.y / dxi) * dxi, x0, x1;
    if (vRange.x < vRange.y) { x0 = xi0; x1 = xi1 + dxi / 2; } else { x0 = xi1; x1 = xi0 - dxi / 2; }
    for (float xi = x0; xi < x1; xi += dxi) ADraw_AddText(xi.ToString(format), float3(lerp(p0.x, p1.x, (xi - vRange.x) / extent(vRange)), p0.y, p0.z) + textHeight / 2 * margin, right, up, color, EMPTY, textHeight, quadType, margin.y > 0 ? ADraw_TextAlignment.BottomCenter : ADraw_TextAlignment.TopCenter, mn, mx, axis);
    ADraw_AddText(title, (p0 + p1) / 2 + titleHeight * 2 * margin, tRight, tUp, color, EMPTY, titleHeight, quadType, margin.y > 0 ? ADraw_TextAlignment.BottomCenter : ADraw_TextAlignment.TopCenter, mn, mx, axis);
  }
  public void ADraw_AddYAxis(string title, float titleHeight, float3 p0, float3 p1, float3 tRight, float3 tUp, float4 color, float2 vRange, string format, float textHeight, float3 right, float3 up, float3 margin, ADraw_Text_QuadType quadType, float3 mn, float3 mx, uint axis = 0)
  {
    if (extent(vRange) <= 0) return;
    uint decimalN = ADraw_GetDecimalN(vRange) + (uint)format.Length, maxTickN = ADraw_GetXAxisN(textHeight, decimalN, p1.xy - p0.xy);
    float range = NiceNum(abs(extent(vRange)), false), dxi = NiceNum(range / (maxTickN - 1), true), xi0 = ceil(vRange.x / dxi) * dxi, xi1 = floor(vRange.y / dxi) * dxi, x0, x1;
    if (vRange.x < vRange.y) { x0 = xi0; x1 = xi1 + dxi / 2; } else { x0 = xi1; x1 = xi0 - dxi / 2; }
    for (float xi = x0; xi < x1; xi += dxi) ADraw_AddText(xi.ToString(format), float3(p0.x, lerp(p0.y, p1.y, (xi - vRange.x) / extent(vRange)), p0.z) + textHeight / 2 * margin, right, up, color, EMPTY, textHeight, quadType, tUp.x < 0 ? ADraw_TextAlignment.CenterRight : ADraw_TextAlignment.CenterLeft, mn, mx, axis);
    ADraw_AddText(title, (p0 + p1) / 2 + textHeight * (2 + decimalN / 5.0f) * margin, tRight, tUp, color, EMPTY, titleHeight, quadType, ADraw_TextAlignment.BottomCenter, mn, mx, axis);
  }
  public void ADraw_AddZAxis(string title, float titleHeight, float3 p0, float3 p1, float3 tRight, float3 tUp, float4 color, float2 vRange, string format, float textHeight, float3 right, float3 up, float3 margin, ADraw_Text_QuadType quadType, float3 mn, float3 mx, uint axis = 0)
  {
    if (extent(vRange) <= 0) return;
    uint decimalN = ADraw_GetDecimalN(vRange) + (uint)format.Length, maxTickN = ADraw_GetXAxisN(textHeight, decimalN, p1.zy - p0.zy);
    float range = NiceNum(abs(extent(vRange)), false), dxi = NiceNum(range / (maxTickN - 1), true), xi0 = ceil(vRange.x / dxi) * dxi, xi1 = floor(vRange.y / dxi) * dxi, x0, x1;
    if (vRange.x < vRange.y) { x0 = xi0; x1 = xi1 + dxi / 2; } else { x0 = xi1; x1 = xi0 - dxi / 2; }
    for (float xi = x0; xi < x1; xi += dxi) ADraw_AddText(xi.ToString(format), float3(p0.x, p0.y, lerp(p0.z, p1.z, (xi - vRange.x) / extent(vRange))) + textHeight / 2 * margin, right, up, color, EMPTY, textHeight, quadType, margin.y > 0 ? ADraw_TextAlignment.BottomCenter : ADraw_TextAlignment.TopCenter, mn, mx, axis);
    ADraw_AddText(title, (p0 + p1) / 2 + titleHeight * 2 * margin, tRight, tUp, color, EMPTY, titleHeight, quadType, margin.y > 0 ? ADraw_TextAlignment.BottomCenter : ADraw_TextAlignment.TopCenter, mn, mx, axis);
  }
  public void ADraw_AddAxes(float numberHeight, float titleHeight, float3 p0, float3 p1, float3 range0, float3 range1, float4 color,
    string xTitle, string yTitle, string zTitle, string xFormat = "0.00", string yFormat = null, string zFormat = null)
  {
    if (yFormat == null) yFormat = xFormat; if (zFormat == null) zFormat = yFormat;
    ADraw_AddXAxis(xTitle, titleHeight, float3(p0.x, p0.y, p0.z), float3(p1.x, p0.y, p0.z), f100, f011, color, float2(range0.x, range1.x), xFormat, numberHeight, f100, f011, f0__, ADraw_Text_QuadType.Switch, p0, p1, 100);
    ADraw_AddXAxis(xTitle, titleHeight, float3(p0.x, p0.y, p1.z), float3(p1.x, p0.y, p1.z), f100, f01_, color, float2(range0.x, range1.x), xFormat, numberHeight, f100, f01_, f0_1, ADraw_Text_QuadType.Switch, p0, p1, 200);
    ADraw_AddXAxis(xTitle, titleHeight, float3(p0.x, p1.y, p1.z), float3(p1.x, p1.y, p1.z), f100, f011, color, float2(range0.x, range1.x), xFormat, numberHeight, f100, f011, f011, ADraw_Text_QuadType.Switch, p0, p1, 300);
    ADraw_AddXAxis(xTitle, titleHeight, float3(p0.x, p1.y, p0.z), float3(p1.x, p1.y, p0.z), f100, f01_, color, float2(range0.x, range1.x), xFormat, numberHeight, f100, f01_, f01_, ADraw_Text_QuadType.Switch, p0, p1, 400);
    ADraw_AddYAxis(yTitle, titleHeight, float3(p0.x, p0.y, p0.z), float3(p0.x, p1.y, p0.z), f010, f_0_, color, float2(range0.y, range1.y), yFormat, numberHeight, f101, f010, f_0_, ADraw_Text_QuadType.Switch, p0, p1, 10);
    ADraw_AddYAxis(yTitle, titleHeight, float3(p1.x, p0.y, p0.z), float3(p1.x, p1.y, p0.z), f0_0, f10_, color, float2(range0.y, range1.y), yFormat, numberHeight, f10_, f010, f10_, ADraw_Text_QuadType.Switch, p0, p1, 20);
    ADraw_AddYAxis(yTitle, titleHeight, float3(p1.x, p0.y, p1.z), float3(p1.x, p1.y, p1.z), f0_0, f101, color, float2(range0.y, range1.y), yFormat, numberHeight, f101, f010, f101, ADraw_Text_QuadType.Switch, p0, p1, 30);
    ADraw_AddYAxis(yTitle, titleHeight, float3(p0.x, p0.y, p1.z), float3(p0.x, p1.y, p1.z), f0_0, f_01, color, float2(range0.y, range1.y), yFormat, numberHeight, f10_, f010, f_01, ADraw_Text_QuadType.Switch, p0, p1, 40);
    ADraw_AddZAxis(zTitle, titleHeight, float3(p0.x, p0.y, p0.z), float3(p0.x, p0.y, p1.z), f001, f110, color, float2(range0.z, range1.z), zFormat, numberHeight, f001, f110, f__0, ADraw_Text_QuadType.Switch, p0, p1, 1);
    ADraw_AddZAxis(zTitle, titleHeight, float3(p0.x, p1.y, p0.z), float3(p0.x, p1.y, p1.z), f001, f_10, color, float2(range0.z, range1.z), zFormat, numberHeight, f001, f_10, f_10, ADraw_Text_QuadType.Switch, p0, p1, 2);
    ADraw_AddZAxis(zTitle, titleHeight, float3(p1.x, p1.y, p0.z), float3(p1.x, p1.y, p1.z), f001, f110, color, float2(range0.z, range1.z), zFormat, numberHeight, f001, f110, f110, ADraw_Text_QuadType.Switch, p0, p1, 3);
    ADraw_AddZAxis(zTitle, titleHeight, float3(p1.x, p0.y, p0.z), float3(p1.x, p0.y, p1.z), f001, f_10, color, float2(range0.z, range1.z), zFormat, numberHeight, f001, f_10, f1_0, ADraw_Text_QuadType.Switch, p0, p1, 4);
  }
  public string ADraw_str(string[] s, int i) => i < s.Length ? s[i] : i > 2 && i - 3 < s.Length ? s[i - 3] : "";
  public void ADraw_AddAxes(float numberHeight, float titleHeight, float4 color, float3 p0, float3 p1,
    float3 rangeA0, float3 rangeA1, float3 rangeB0, float3 rangeB1, string xyz_titles, string xyz_formats = "0.00")
  {
    var (ts, fs, ttl, fmt) = (xyz_titles.Split("|"), xyz_formats.Split("|"), new string[6], new string[6]);
    for (int i = 0; i < 6; i++) { ttl[i] = ADraw_str(ts, i); fmt[i] = ADraw_str(fs, i); }
    ADraw_AddAxes(numberHeight, titleHeight, color, p0, p1, rangeA0, rangeA1, rangeB0, rangeB1, ttl, fmt);
  }
  public void ADraw_AddAxes(float numberHeight, float titleHeight, float4 color, float3 p0, float3 p1,
    float3 rangeA0, float3 rangeA1, float3 rangeB0, float3 rangeB1, string[] ttl, string[] fmt)
  {
    ADraw_AddXAxis(ttl[0], titleHeight, float3(p0.x, p0.y, p0.z), float3(p1.x, p0.y, p0.z), f100, f011, color, float2(rangeA0.x, rangeA1.x), fmt[0], numberHeight, f100, f011, f0__, ADraw_Text_QuadType.Switch, p0, p1, 100);
    ADraw_AddXAxis(ttl[0], titleHeight, float3(p0.x, p0.y, p1.z), float3(p1.x, p0.y, p1.z), f100, f01_, color, float2(rangeA0.x, rangeA1.x), fmt[0], numberHeight, f100, f01_, f0_1, ADraw_Text_QuadType.Switch, p0, p1, 200);
    ADraw_AddXAxis(ttl[3], titleHeight, float3(p0.x, p1.y, p1.z), float3(p1.x, p1.y, p1.z), f100, f011, color, float2(rangeB0.x, rangeB1.x), fmt[3], numberHeight, f100, f011, f011, ADraw_Text_QuadType.Switch, p0, p1, 300);
    ADraw_AddXAxis(ttl[3], titleHeight, float3(p0.x, p1.y, p0.z), float3(p1.x, p1.y, p0.z), f100, f01_, color, float2(rangeB0.x, rangeB1.x), fmt[3], numberHeight, f100, f01_, f01_, ADraw_Text_QuadType.Switch, p0, p1, 400);
    ADraw_AddYAxis(ttl[1], titleHeight, float3(p0.x, p0.y, p0.z), float3(p0.x, p1.y, p0.z), f010, f_0_, color, float2(rangeA0.y, rangeA1.y), fmt[1], numberHeight, f101, f010, f_0_, ADraw_Text_QuadType.Switch, p0, p1, 10);
    ADraw_AddYAxis(ttl[4], titleHeight, float3(p1.x, p0.y, p0.z), float3(p1.x, p1.y, p0.z), f0_0, f10_, color, float2(rangeB0.y, rangeB1.y), fmt[4], numberHeight, f10_, f010, f10_, ADraw_Text_QuadType.Switch, p0, p1, 20);
    ADraw_AddYAxis(ttl[1], titleHeight, float3(p1.x, p0.y, p1.z), float3(p1.x, p1.y, p1.z), f0_0, f101, color, float2(rangeA0.y, rangeA1.y), fmt[1], numberHeight, f101, f010, f101, ADraw_Text_QuadType.Switch, p0, p1, 30);
    ADraw_AddYAxis(ttl[4], titleHeight, float3(p0.x, p0.y, p1.z), float3(p0.x, p1.y, p1.z), f0_0, f_01, color, float2(rangeB0.y, rangeB1.y), fmt[4], numberHeight, f10_, f010, f_01, ADraw_Text_QuadType.Switch, p0, p1, 40);
    ADraw_AddZAxis(ttl[2], titleHeight, float3(p0.x, p0.y, p0.z), float3(p0.x, p0.y, p1.z), f001, f110, color, float2(rangeA0.z, rangeA1.z), fmt[2], numberHeight, f001, f110, f__0, ADraw_Text_QuadType.Switch, p0, p1, 1);
    ADraw_AddZAxis(ttl[5], titleHeight, float3(p0.x, p1.y, p0.z), float3(p0.x, p1.y, p1.z), f001, f_10, color, float2(rangeB0.z, rangeB1.z), fmt[5], numberHeight, f001, f_10, f_10, ADraw_Text_QuadType.Switch, p0, p1, 2);
    ADraw_AddZAxis(ttl[5], titleHeight, float3(p1.x, p1.y, p0.z), float3(p1.x, p1.y, p1.z), f001, f110, color, float2(rangeB0.z, rangeB1.z), fmt[5], numberHeight, f001, f110, f110, ADraw_Text_QuadType.Switch, p0, p1, 3);
    ADraw_AddZAxis(ttl[2], titleHeight, float3(p1.x, p0.y, p0.z), float3(p1.x, p0.y, p1.z), f001, f_10, color, float2(rangeA0.z, rangeA1.z), fmt[2], numberHeight, f001, f_10, f1_0, ADraw_Text_QuadType.Switch, p0, p1, 4);
  }
  public void ADraw_AddAxes(float numberHeight, float titleHeight, float3 p0, float3 p1, float3 range0, float3 range1, float4 color, string xyz_titles, string xyz_formats = "0.00")
  {
    var _titles = splitStr3(xyz_titles, false);
    var _formats = splitStr3(xyz_formats, true);
    ADraw_AddAxes(numberHeight, titleHeight, p0, p1, range0, range1, color, _titles[0], _titles[1], _titles[2], _formats[0], _formats[1], _formats[2]);
  }
  public void ADraw_AddAxes(float numberHeight, float titleHeight, float3 p0, float3 p1, float4 color,
    string xTitle, string yTitle, string zTitle, string xFormat = "0.00", string yFormat = null, string zFormat = null, bool zeroOrigin = false)
  {
    float3 range0 = p0, range1 = p1; if (zeroOrigin) { range1 -= range0; range0 = f000; }
    ADraw_AddAxes(numberHeight, titleHeight, p0, p1, range0, range1, color, xTitle, yTitle, zTitle, xFormat, yFormat, zFormat);
  }
  public void ADraw_AddAxes(float numberHeight, float titleHeight, float3 p0, float3 p1, float4 color,
  string xyz_titles, string xyz_formats = "0.00", bool zeroOrigin = false)
  {
    var _titles = splitStr3(xyz_titles, false);
    var _formats = splitStr3(xyz_formats, true);
    float3 range0 = p0, range1 = p1; if (zeroOrigin) { range1 -= range0; range0 = f000; }
    ADraw_AddAxes(numberHeight, titleHeight, p0, p1, range0, range1, color, _titles[0], _titles[1], _titles[2], _formats[0], _formats[1], _formats[2]);
  }
  public virtual void ADraw_AddAxes(float numberHeight, float titleHeight, float4 color,
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
    ADraw_AddXAxis(xTitleA, titleHeight, x0A, x1A, f100, f011, color, xRangeA, xFormatA, numberHeight, f100, f011, f0__, ADraw_Text_QuadType.Switch, f000, f000);
    ADraw_AddXAxis(xTitleB, titleHeight, x0B, x1B, f100, f011, color, xRangeB, xFormatB, numberHeight, f100, f011, f011, ADraw_Text_QuadType.Switch, f000, f000);
    ADraw_AddXAxis(xTitleC, titleHeight, x0C, x1C, f100, f011, color, xRangeC, xFormatC, numberHeight, f100, f011, f0__, ADraw_Text_QuadType.Switch, f000, f000);
    ADraw_AddXAxis(xTitleD, titleHeight, x0D, x1D, f100, f011, color, xRangeD, xFormatD, numberHeight, f100, f011, f011, ADraw_Text_QuadType.Switch, f000, f000);
    ADraw_AddYAxis(yTitleA, titleHeight, y0A, y1A, f010, f_01, color, yRangeA, yFormatA, numberHeight, f10_, f010, f_01, ADraw_Text_QuadType.Switch, f000, f000);
    ADraw_AddYAxis(yTitleB, titleHeight, y0B, y1B, f0_0, f10_, color, yRangeB, yFormatB, numberHeight, f10_, f010, f10_, ADraw_Text_QuadType.Switch, f000, f000);
    ADraw_AddYAxis(yTitleC, titleHeight, y0C, y1C, f010, f_01, color, yRangeC, yFormatC, numberHeight, f10_, f010, f_01, ADraw_Text_QuadType.Switch, f000, f000);
    ADraw_AddYAxis(yTitleD, titleHeight, y0D, y1D, f0_0, f10_, color, yRangeD, yFormatD, numberHeight, f10_, f010, f10_, ADraw_Text_QuadType.Switch, f000, f000);
    ADraw_AddZAxis(zTitleA, titleHeight, z0A, z1A, f010, f_01, color, zRangeA, zFormatA, numberHeight, f10_, f010, f_01, ADraw_Text_QuadType.Switch, f000, f000);
    ADraw_AddZAxis(zTitleB, titleHeight, z0B, z1B, f0_0, f10_, color, zRangeB, zFormatB, numberHeight, f10_, f010, f10_, ADraw_Text_QuadType.Switch, f000, f000);
    ADraw_AddZAxis(zTitleC, titleHeight, z0C, z1C, f010, f_01, color, zRangeC, zFormatC, numberHeight, f10_, f010, f_01, ADraw_Text_QuadType.Switch, f000, f000);
    ADraw_AddZAxis(zTitleD, titleHeight, z0D, z1D, f0_0, f10_, color, zRangeD, zFormatD, numberHeight, f10_, f010, f10_, ADraw_Text_QuadType.Switch, f000, f000);
  }
  public void ADraw_AddAxes(float numberHeight, float titleHeight, float4 color,
    string xTitleA, float3 x0A, float3 x1A, float2 xRangeA, string xFormatA,
    string xTitleB, float3 x0B, float3 x1B, float2 xRangeB, string xFormatB,
    string yTitleA, float3 y0A, float3 y1A, float2 yRangeA, string yFormatA,
    string yTitleB, float3 y0B, float3 y1B, float2 yRangeB, string yFormatB,
    string zTitleA, float3 z0A, float3 z1A, float2 zRangeA, string zFormatA,
    string zTitleB, float3 z0B, float3 z1B, float2 zRangeB, string zFormatB)
  {
    ADraw_AddXAxis(xTitleA, titleHeight, x0A, x1A, f100, f011, color, xRangeA, xFormatA, numberHeight, f100, f011, f0__, ADraw_Text_QuadType.Switch, f000, f000);
    ADraw_AddXAxis(xTitleB, titleHeight, x0B, x1B, f100, f011, color, xRangeB, xFormatB, numberHeight, f100, f011, f011, ADraw_Text_QuadType.Switch, f000, f000);
    ADraw_AddYAxis(yTitleA, titleHeight, y0A, y1A, f010, f_01, color, yRangeA, yFormatA, numberHeight, f10_, f010, f_01, ADraw_Text_QuadType.Switch, f000, f000);
    ADraw_AddYAxis(yTitleB, titleHeight, y0B, y1B, f0_0, f10_, color, yRangeB, yFormatB, numberHeight, f10_, f010, f10_, ADraw_Text_QuadType.Switch, f000, f000);
    ADraw_AddZAxis(zTitleA, titleHeight, z0A, z1A, f010, f_01, color, zRangeA, zFormatA, numberHeight, f10_, f010, f_01, ADraw_Text_QuadType.Switch, f000, f000);
    ADraw_AddZAxis(zTitleB, titleHeight, z0B, z1B, f0_0, f10_, color, zRangeB, zFormatB, numberHeight, f10_, f010, f10_, ADraw_Text_QuadType.Switch, f000, f000);
  }
  public void ADraw_AddAxes(float numberHeight, float titleHeight, float4 color,
    string xTitleA, float3 x0A, float3 x1A, float2 xRangeA, string xFormatA,
    string xTitleB, float3 x0B, float3 x1B, float2 xRangeB, string xFormatB,
    string yTitleA, float3 y0A, float3 y1A, float2 yRangeA, string yFormatA,
    string yTitleB, float3 y0B, float3 y1B, float2 yRangeB, string yFormatB)
  {
    ADraw_AddXAxis(xTitleA, titleHeight, x0A, x1A, f100, f011, color, xRangeA, xFormatA, numberHeight, f100, f011, f0__, ADraw_Text_QuadType.Switch, f000, f000);
    ADraw_AddXAxis(xTitleB, titleHeight, x0B, x1B, f100, f011, color, xRangeB, xFormatB, numberHeight, f100, f011, f011, ADraw_Text_QuadType.Switch, f000, f000);
    ADraw_AddYAxis(yTitleA, titleHeight, y0A, y1A, f010, f_01, color, yRangeA, yFormatA, numberHeight, f10_, f010, f_01, ADraw_Text_QuadType.Switch, f000, f000);
    ADraw_AddYAxis(yTitleB, titleHeight, y0B, y1B, f0_0, f10_, color, yRangeB, yFormatB, numberHeight, f10_, f010, f10_, ADraw_Text_QuadType.Switch, f000, f000);
  }
  public void ADraw_AddAxes(float numberHeight, float titleHeight, float4 color,
    string xTitleA, float3 x0A, float3 x1A, float2 xRangeA, string xFormatA,
    string yTitleA, float3 y0A, float3 y1A, float2 yRangeA, string yFormatA)
  {
    ADraw_AddAxes(numberHeight, titleHeight, color, xTitleA, x0A, x1A, xRangeA, xFormatA, xTitleA, x0A, x1A, xRangeA, xFormatA,
     yTitleA, y0A, y1A, yRangeA, yFormatA, yTitleA, y0A, y1A, yRangeA, yFormatA);
  }
  public uint ADraw_GetXAxisN(float textHeight, uint decimalN, float2 vRange) { float w = decimalN * textHeight; uint axisN = roundu(abs(extent(vRange)) / w); return clamp(axisN, 2, 25); }
  public uint ADraw_GetYAxisN(float textHeight, float2 vRange) => roundu(abs(extent(vRange)) / textHeight * 0.75f);
  public uint3 ADraw_GetXAxisN(float textHeight, uint3 decimalN, float3 cubeMin, float3 cubeMax)
  {
    float3 w = decimalN * textHeight;
    uint3 axisN = roundu(abs(cubeMax - cubeMin) / w);
    return clamp(axisN, u111 * 2, u111 * 25);
  }
  public uint3 ADraw_GetDecimalN(float3 cubeMin, float3 cubeMax)
  {
    int3 tickN = 25 * i111;
    float3 pRange = cubeMax - cubeMin, range = NiceNum(pRange, false);
    float3 di = NiceNum(range / (tickN - 1), true);
    uint3 decimalN = roundu(di >= 1) * flooru(1 + abs(log10(roundu(di == f000) + di)));
    return max(u111, decimalN);
  }
  public uint ADraw_GetDecimalN(float2 vRange)
  {
    int tickN = 25;
    float pRange = abs(extent(vRange)), range = NiceNum(pRange, false);
    float di = NiceNum(range / (tickN - 1), true);
    uint decimalN = roundu(Is(di >= 1)) * flooru(1 + abs(log10(roundu(Is(di == 0)) + di)));
    return max(1, decimalN);
  }
  public void ADraw_AddLegend(string title, float2 vRange, string format)
  {
    float h = 8;
    float3 c = 10000 * f110;
    ADraw_AddYAxis(title, 0.4f, c + float3(0.4f, -h / 2, 0), c + float3(0.4f, h / 2, 0), f010, f_00, BLACK, vRange, format, 0.2f, f100, f010, f_00, ADraw_Text_QuadType.FrontOnly, f000, f000);
  }
  public virtual v2f vert_ADraw_Text(ADraw_TextInfo t, uint i, uint j, v2f o)
  {
    float3 p = t.p, p0 = t.p0, p1 = t.p1, right = t.right, up = t.up;
    float h = t.height;
    float2 uvSize = t.uvSize;
    float4 color = t.color;
    uint quadType = t.quadType, justification = t.justification, axis = t.axis;
    float w = h * uvSize.x;
    float3 jp = f000;
    if (quadType == ADraw_Text_QuadType_Billboard)
    {
      o.wPos = p;
      switch (justification)
      {
        case ADraw_TextAlignment_BottomLeft: jp = w / 2 * right + h / 2 * up; break;
        case ADraw_TextAlignment_CenterLeft: jp = w / 2 * right; break;
        case ADraw_TextAlignment_TopLeft: jp = w / 2 * right - h / 2 * up; break;
        case ADraw_TextAlignment_BottomCenter: jp = h / 2 * up; break;
        case ADraw_TextAlignment_CenterCenter: break;
        case ADraw_TextAlignment_TopCenter: jp = -h / 2 * up; break;
        case ADraw_TextAlignment_BottomRight: jp = -w / 2 * right + h / 2 * up; break;
        case ADraw_TextAlignment_CenterRight: jp = -w / 2 * right; break;
        case ADraw_TextAlignment_TopRight: jp = -w / 2 * right - h / 2 * up; break;
      }
      float4 p4 = new float4(p, 1), billboardQuad = float4((ADraw_wrapJ(j, 2) - 0.5f) * w, (0.5f - ADraw_wrapJ(j, 1)) * h, 0, 0);
      o.pos = mul(UNITY_MATRIX_P, mul(UNITY_MATRIX_V, p4) + billboardQuad + float4(jp, 0));
      o.normal = f00_;
      o.uv = float2(ADraw_wrapJ(j, 2), ADraw_wrapJ(j, 4)) * uvSize;
      o.ti = float4(i, 0, ADraw_Draw_Text3D, i);
      o.color = color;
    }
    else if (quadType == ADraw_Text_QuadType_Arrow)
    {
      o.wPos = p;
      right = normalize(p1 - p0);
      up = f010;
      jp = w / 2 * right;
      float3 q0 = p - jp, q1 = p + jp;
      o = vert_ADraw_Arrow(q0, q1, h * 0.165f, color, i, j, o);
      float4 ti = o.ti; ti.z = ADraw_Draw_Text3D; o.ti = ti;
      if (dot(p - _WorldSpaceCameraPos, cross(right, up)) < 0)
        o.uv = float2(length(q1 - q0) / h * ADraw_wrapJ(j, 1), ADraw_wrapJ(j, 2) - 0.5f);
      else
        o.uv = float2(length(q1 - q0) / h * (1 - ADraw_wrapJ(j, 1)), 0.5f - ADraw_wrapJ(j, 2));
    }
    else if (quadType == ADraw_Text_QuadType_FrontBack || quadType == ADraw_Text_QuadType_Switch || dot(p - _WorldSpaceCameraPos, cross(right, up)) > 0)
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
          case ADraw_TextAlignment_BottomLeft: break;
          case ADraw_TextAlignment_CenterLeft: jp = -h / 2 * up; break;
          case ADraw_TextAlignment_TopLeft: jp = -h * up; break;
          case ADraw_TextAlignment_BottomCenter: jp = -w / 2 * right; break;
          case ADraw_TextAlignment_CenterCenter: jp = -w / 2 * right - h / 2 * up; break;
          case ADraw_TextAlignment_TopCenter: jp = -w / 2 * right - h * up; break;
          case ADraw_TextAlignment_BottomRight: jp = -w * right; break;
          case ADraw_TextAlignment_CenterRight: jp = -w * right - h / 2 * up; break;
          case ADraw_TextAlignment_TopRight: jp = -w * right - h * up; break;
        }
        float3 q0 = p + jp, q1 = q0 + right * w, p2 = q1 + up * h, p3 = q0 + up * h;
        o.pos = UnityObjectToClipPos(o.wPos = j % 5 == 0 ? p3 : j == 1 ? p2 : j == 4 ? q0 : q1);
        o.normal = cross(right, up);
        if (quadType == ADraw_Text_QuadType_Switch && dot(p - _WorldSpaceCameraPos, cross(right, up)) < 0)
          o.uv = float2(1 - ADraw_wrapJ(j, 2), ADraw_wrapJ(j, 4)) * uvSize;
        else
          o.uv = float2(ADraw_wrapJ(j, 2), ADraw_wrapJ(j, 4)) * uvSize;
        o.ti = float4(i, 0, ADraw_Draw_Text3D, i);
        o.color = color;
      }
    }
    return o;
  }
  public virtual v2f vert_ADraw_Text(uint i, uint j, v2f o) => vert_ADraw_Text(ADraw_textInfo(i), i, j, o);
  public virtual void ADraw_getTextInfo_GS(uint3 id)
  {
    uint i = id.x;
    ADraw_TextInfo ti = ADraw_textInfo(i);
    ti.textI = i;
    ti.uvSize = f01;
    uint2 textIs = ADraw_Get_text_indexes(i);
    float2 t = ti.uvSize;
    for (uint j = textIs.x; j < textIs.y; j++) { uint byteI = ADraw_Byte(j); if (byteI >= 32) { byteI -= 32; t.x += ADraw_fontInfos[byteI].advance; } }
    t.x /= g.ADraw_fontSize;
    ti.uvSize = t;
    ADraw_textInfo(i, ti);
  }
  public virtual void ADraw_setDefaultTextInfo_GS(uint3 id)
  {
    uint i = id.x;
    if (i > 0)
    {
      ADraw_TextInfo t = ADraw_textInfo(0), ti = ADraw_textInfo(i);
      ti.color = t.color; ti.backColor = t.backColor; ti.p = t.p; ti.p0 = t.p0; ti.p1 = t.p1; ti.height = t.height;
      ti.quadType = (uint)t.quadType; ti.axis = t.axis; ti.right = t.right; ti.up = t.up; ti.justification = t.justification;
      ADraw_textInfo(i, ti);
    }
  }
  public virtual float3 ADraw_gridMin() { return f000; }
  public virtual float3 ADraw_gridMax() { return f111; }
  public float3 ADraw_gridSize() { return ADraw_gridMax() - ADraw_gridMin(); }
  public float3 ADraw_gridCenter() { return (ADraw_gridMax() + ADraw_gridMin()) / 2; }
  public virtual v2f vert_ADraw_Box(uint i, uint j, v2f o) { return vert_ADraw_BoxFrame(ADraw_gridMin(), ADraw_gridMax(), ADraw_boxThickness, ADraw_boxColor, i, j, o); }
  public virtual float4 frag_ADraw_Text(Texture2D t, RWStructuredBuffer<uint> _text, RWStructuredBuffer<ADraw_FontInfo> ADraw_fontInfos, float ADraw_fontSize, uint quadType, float4 backColor, uint2 textIs, v2f i)
  {
    float uv_x = 0;
    float4 color = backColor;
    for (uint textI = textIs.x; textI < textIs.y; textI++)
    {
      uint fontInfoI = TextByte(_text, textI) - 32;
      ADraw_FontInfo f = ADraw_fontInfos[fontInfoI];
      float dx = f.advance / ADraw_fontSize;
      float2 mn = float2(f.minX, f.minY) / ADraw_fontSize, mx = float2(f.maxX, f.maxY) / ADraw_fontSize, range = mx - mn;
      if (quadType == ADraw_Text_QuadType_Arrow) { mn.y -= 0.5f; mx.y -= 0.5f; }
      float2 uv = (i.uv - uv_x * f10 + f1_ * mn - float2(f.bearing / ADraw_fontSize, 0.25f)) / range;
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
  public virtual float4 frag_ADraw_GS(v2f i, float4 color)
  {
    switch (roundu(i.ti.z))
    {
      case uint_max: Discard(0); break;
      case ADraw_Draw_Sphere: color = frag_ADraw_Sphere(i); break;
      case ADraw_Draw_Line: color = frag_ADraw_Line(i); break;
      case ADraw_Draw_Arrow: color = frag_ADraw_Arrow(i); break;
      case ADraw_Draw_Signal: color = frag_ADraw_Signal(i); break;
      case ADraw_Draw_LineSegment: color = frag_ADraw_LineSegment(i); break;
      case ADraw_Draw_Mesh: color = frag_ADraw_Mesh(i); break;
      case ADraw_Draw_Text3D:
        ADraw_TextInfo t = ADraw_textInfo(roundu(i.ti.x));
        color = frag_ADraw_Text(ADraw_fontTexture, ADraw_tab_delimeted_text, ADraw_fontInfos, ADraw_fontSize, t.quadType, t.backColor, ADraw_Get_text_indexes(t.textI), i);
        break;
    }
    return color;
  }

  public virtual void base_ADraw_Start0_GS()
  {
    ADraw_ABuff_Start0_GS();
  }
  public virtual void base_ADraw_Start1_GS()
  {
    ADraw_ABuff_Start1_GS();
  }
  public virtual void base_ADraw_OnApplicationQuit() { Save_UI(); OnApplicationQuit_GS(); base.OnApplicationQuit(); already_quited = true; }
  public virtual void base_ADraw_LateUpdate0_GS()
  {
    ADraw_ABuff_LateUpdate0_GS();
  }
  public virtual void base_ADraw_LateUpdate1_GS()
  {
    ADraw_ABuff_LateUpdate1_GS();
  }
  public virtual void base_ADraw_Update0_GS()
  {
    ADraw_ABuff_Update0_GS();
  }
  public virtual void base_ADraw_Update1_GS()
  {
    ADraw_ABuff_Update1_GS();
  }
  public virtual void base_ADraw_OnValueChanged_GS()
  {
    ADraw_ABuff_OnValueChanged_GS();
  }
  public virtual void base_ADraw_InitBuffers0_GS()
  {
    ADraw_ABuff_InitBuffers0_GS();
  }
  public virtual void base_ADraw_InitBuffers1_GS()
  {
    ADraw_ABuff_InitBuffers1_GS();
  }
  [HideInInspector]
  public virtual void base_ADraw_onRenderObject_GS(ref bool render, ref bool cpu)
  {
    ADraw_ABuff_onRenderObject_GS(ref render, ref cpu);
  }
  public virtual float4 base_frag_ADraw_GS(v2f i, float4 color) { return color; }
  public void ADraw_ABuff_print_Bits(string title) { StrBldr sb = StrBldr($"{title}, ADraw_ABuff_Bits"); for (uint i = 0; i < ADraw_ABuff_BitN; i++) sb.Add(" ", ADraw_ABuff_Bits[i]); print(sb); }
  public void ADraw_ABuff_print_Sums(string title) { StrBldr sb = StrBldr($"{title}, ADraw_ABuff_Sums"); for (uint i = 0; i < ADraw_ABuff_BitN; i++) sb.Add(" ", ADraw_ABuff_Sums[i]); print(sb); }
  public void ADraw_ABuff_print_Indexes(string title) { StrBldr sb = StrBldr($"{title}: ADraw_ABuff_Indexes"); for (uint i = 0; i < ADraw_ABuff_IndexN; i++) sb.Add(" ", ADraw_ABuff_Indexes[i]); print(sb); }
  public void ADraw_ABuff_SetN(uint n)
  {
    if (n > 2147450880) throw new Exception("gsABuff: ADraw_ABuff_N > 2,147,450,880");
    ADraw_ABuff_N = n; ADraw_ABuff_BitN = ceilu(ADraw_ABuff_N, 32); ADraw_ABuff_BitN1 = ceilu(ADraw_ABuff_BitN, numthreads1); ADraw_ABuff_BitN2 = ceilu(ADraw_ABuff_BitN1, numthreads1);
    AllocData_ADraw_ABuff_Bits(ADraw_ABuff_BitN); AllocData_ADraw_ABuff_Fills1(ADraw_ABuff_BitN1); AllocData_ADraw_ABuff_Fills2(ADraw_ABuff_BitN2); AllocData_ADraw_ABuff_Sums(ADraw_ABuff_BitN);
  }
  public void ADraw_ABuff_FillPrefixes() { Gpu_ADraw_ABuff_GetFills1(); Gpu_ADraw_ABuff_GetFills2(); Gpu_ADraw_ABuff_IncFills1(); Gpu_ADraw_ABuff_IncSums(); }
  public void ADraw_ABuff_getIndexes() { AllocData_ADraw_ABuff_Indexes(ADraw_ABuff_IndexN); Gpu_ADraw_ABuff_GetIndexes(); }
  public void ADraw_ABuff_FillIndexes() { ADraw_ABuff_FillPrefixes(); ADraw_ABuff_getIndexes(); }
  public virtual uint ADraw_ABuff_Run(uint n) { ADraw_ABuff_SetN(n); Gpu_ADraw_ABuff_GetSums(); ADraw_ABuff_FillIndexes(); return ADraw_ABuff_IndexN; }
  public uint ADraw_ABuff_Run(int n) => ADraw_ABuff_Run((uint)n);
  public uint ADraw_ABuff_Run(uint2 n) => ADraw_ABuff_Run(cproduct(n)); public uint ADraw_ABuff_Run(uint3 n) => ADraw_ABuff_Run(cproduct(n));
  public uint ADraw_ABuff_Run(int2 n) => ADraw_ABuff_Run(cproduct(n)); public uint ADraw_ABuff_Run(int3 n) => ADraw_ABuff_Run(cproduct(n));
  public virtual void ADraw_ABuff_Init_Bits(uint n, GS parent, string _N, string _BitN, ref RWStructuredBuffer<uint> _Bits) { ADraw_ABuff_SetN(n); parent.SetValue(_N, ADraw_ABuff_N); parent.SetValue(_BitN, ADraw_ABuff_BitN); }
  public virtual void ADraw_ABuff_Prefix_Sums() { Gpu_ADraw_ABuff_Get_Bits_Sums(); ADraw_ABuff_FillPrefixes(); }
  public virtual void ADraw_ABuff_Bit_Indexes(RWStructuredBuffer<uint> bits, GS _this, string _IndexN, ref RWStructuredBuffer<uint> _Indexes) { ADraw_ABuff_Prefix_Sums(); ADraw_ABuff_getIndexes(); _this.SetValue(_IndexN, ADraw_ABuff_IndexN); }
  public uint ADraw_ABuff_Assign_Bits(uint i, uint j, uint bits) => bits | (Is(i < ADraw_ABuff_N && ADraw_ABuff_IsBitOn(i)) << (int)j);
  public virtual void ADraw_ABuff_Get_Bits_GS(uint3 id) { uint i = id.x, j, k, bits = 0; if (i < ADraw_ABuff_BitN) { for (j = 0, k = i * 32; j < 32; j++) bits = ADraw_ABuff_Assign_Bits(k + j, j, bits); ADraw_ABuff_Bits[i] = bits; } }
  public virtual IEnumerator ADraw_ABuff_GetSums_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI)
  {
    uint i = id.x, c, s, j, k, bits = 0;
    if (i < ADraw_ABuff_BitN) { for (j = 0, k = i * 32; j < 32; j++) bits = ADraw_ABuff_Assign_Bits(k + j, j, bits); ADraw_ABuff_Bits[i] = bits; c = countbits(bits); } else c = 0;
    ADraw_ABuff_grp0[grpI] = c; ADraw_ABuff_grp[grpI] = c; yield return GroupMemoryBarrierWithGroupSync();
    for (s = 1; s < numthreads1; s *= 2)
    {
      if (grpI >= s && i < ADraw_ABuff_BitN) ADraw_ABuff_grp[grpI] = ADraw_ABuff_grp0[grpI] + ADraw_ABuff_grp0[grpI - s]; yield return GroupMemoryBarrierWithGroupSync();
      ADraw_ABuff_grp0[grpI] = ADraw_ABuff_grp[grpI]; yield return GroupMemoryBarrierWithGroupSync();
    }
    if (i < ADraw_ABuff_BitN) ADraw_ABuff_Sums[i] = ADraw_ABuff_grp[grpI];
  }
  public virtual IEnumerator ADraw_ABuff_Get_Bits_Sums_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI)
  {
    uint i = id.x, c = i < ADraw_ABuff_BitN ? countbits(ADraw_ABuff_Bits[i]) : 0, s;
    ADraw_ABuff_grp0[grpI] = c; ADraw_ABuff_grp[grpI] = c; yield return GroupMemoryBarrierWithGroupSync();
    for (s = 1; s < numthreads1; s *= 2)
    {
      if (grpI >= s && i < ADraw_ABuff_BitN) ADraw_ABuff_grp[grpI] = ADraw_ABuff_grp0[grpI] + ADraw_ABuff_grp0[grpI - s]; yield return GroupMemoryBarrierWithGroupSync();
      ADraw_ABuff_grp0[grpI] = ADraw_ABuff_grp[grpI]; yield return GroupMemoryBarrierWithGroupSync();
    }
    if (i < ADraw_ABuff_BitN) ADraw_ABuff_Sums[i] = ADraw_ABuff_grp[grpI];
  }
  public virtual IEnumerator ADraw_ABuff_GetFills1_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI)
  {
    uint i = id.x, k = i + 1, j = k * numthreads1 - 1, c = i < ADraw_ABuff_BitN1 - 1 ? ADraw_ABuff_Sums[j] : i < ADraw_ABuff_BitN1 ? ADraw_ABuff_Sums[ADraw_ABuff_BitN - 1] : 0, s;
    ADraw_ABuff_grp0[grpI] = c; ADraw_ABuff_grp[grpI] = c; yield return GroupMemoryBarrierWithGroupSync();
    for (s = 1; s < numthreads1; s *= 2)
    {
      if (grpI >= s && i < ADraw_ABuff_BitN1) ADraw_ABuff_grp[grpI] = ADraw_ABuff_grp0[grpI] + ADraw_ABuff_grp0[grpI - s]; yield return GroupMemoryBarrierWithGroupSync();
      ADraw_ABuff_grp0[grpI] = ADraw_ABuff_grp[grpI]; yield return GroupMemoryBarrierWithGroupSync();
    }
    if (i < ADraw_ABuff_BitN1) ADraw_ABuff_Fills1[i] = ADraw_ABuff_grp[grpI];
  }
  public virtual IEnumerator ADraw_ABuff_GetFills2_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI)
  {
    uint i = id.x, k = i + 1, j = k * numthreads1 - 1, c = i < ADraw_ABuff_BitN2 - 1 ? ADraw_ABuff_Fills1[j] : i < ADraw_ABuff_BitN2 ? ADraw_ABuff_Fills1[ADraw_ABuff_BitN1 - 1] : 0, s;
    ADraw_ABuff_grp0[grpI] = c; ADraw_ABuff_grp[grpI] = c; yield return GroupMemoryBarrierWithGroupSync();
    for (s = 1; s < numthreads1; s *= 2)
    {
      if (grpI >= s && i < ADraw_ABuff_BitN2) ADraw_ABuff_grp[grpI] = ADraw_ABuff_grp0[grpI] + ADraw_ABuff_grp0[grpI - s]; yield return GroupMemoryBarrierWithGroupSync();
      ADraw_ABuff_grp0[grpI] = ADraw_ABuff_grp[grpI]; yield return GroupMemoryBarrierWithGroupSync();
    }
    if (i < ADraw_ABuff_BitN2) ADraw_ABuff_Fills2[i] = ADraw_ABuff_grp[grpI];
  }
  public virtual void ADraw_ABuff_IncFills1_GS(uint3 id) { uint i = id.x; if (i >= numthreads1) ADraw_ABuff_Fills1[i] += ADraw_ABuff_Fills2[i / numthreads1 - 1]; }
  public virtual void ADraw_ABuff_IncSums_GS(uint3 id) { uint i = id.x; if (i >= numthreads1) ADraw_ABuff_Sums[i] += ADraw_ABuff_Fills1[i / numthreads1 - 1]; if (i == ADraw_ABuff_BitN - 1) ADraw_ABuff_IndexN = ADraw_ABuff_Sums[i]; }
  public virtual void ADraw_ABuff_GetIndexes_GS(uint3 id) { uint i = id.x, j, sum = i == 0 ? 0 : ADraw_ABuff_Sums[i - 1], b, i32 = i << 5, k; for (k = 0, b = ADraw_ABuff_Bits[i]; b > 0; k++) { j = (uint)findLSB(b); ADraw_ABuff_Indexes[sum + k] = i32 + j; b = SetBitu(b, j, 0); } }

  public virtual void base_ADraw_ABuff_Start0_GS() { }
  public virtual void base_ADraw_ABuff_Start1_GS() { }
  public virtual void base_ADraw_ABuff_OnApplicationQuit() { Save_UI(); OnApplicationQuit_GS(); base.OnApplicationQuit(); already_quited = true; }
  public virtual void base_ADraw_ABuff_LateUpdate0_GS() { }
  public virtual void base_ADraw_ABuff_LateUpdate1_GS() { }
  public virtual void base_ADraw_ABuff_Update0_GS() { }
  public virtual void base_ADraw_ABuff_Update1_GS() { }
  public virtual void base_ADraw_ABuff_OnValueChanged_GS() { }
  public virtual void base_ADraw_ABuff_InitBuffers0_GS() { }
  public virtual void base_ADraw_ABuff_InitBuffers1_GS() { }
  [HideInInspector]
  public virtual void base_ADraw_ABuff_onRenderObject_GS(ref bool render, ref bool cpu) { }
  public virtual float4 base_frag_ADraw_ABuff_GS(v2f i, float4 color) { return color; }
  public virtual void ADraw_ABuff_InitBuffers0_GS() { }
  public virtual void ADraw_ABuff_InitBuffers1_GS() { }
  public virtual void ADraw_ABuff_LateUpdate0_GS() { }
  public virtual void ADraw_ABuff_LateUpdate1_GS() { }
  public virtual void ADraw_ABuff_Update0_GS() { }
  public virtual void ADraw_ABuff_Update1_GS() { }
  public virtual void ADraw_ABuff_Start0_GS() { }
  public virtual void ADraw_ABuff_Start1_GS() { }
  public virtual void ADraw_ABuff_OnValueChanged_GS() { }
  public virtual void ADraw_ABuff_OnApplicationQuit_GS() { }
  public virtual void ADraw_ABuff_onRenderObject_GS(ref bool render, ref bool cpu) { }
  public virtual float4 frag_ADraw_ABuff_GS(v2f i, float4 color)
  {
    return color;
  }
  public virtual void ADraw_LateUpdate0_GS() { }
  public virtual void ADraw_LateUpdate1_GS() { }
  public virtual void ADraw_Update0_GS() { }
  public virtual void ADraw_Update1_GS() { }
  public virtual void ADraw_Start0_GS() { }
  public virtual void ADraw_Start1_GS() { }
  public virtual void ADraw_OnValueChanged_GS() { }
  public virtual void ADraw_OnApplicationQuit_GS() { }
  public virtual void ADraw_onRenderObject_GS(ref bool render, ref bool cpu) { }
  #endregion <ADraw>
  #region <ARand>
  public uint ARand_Random_uint(uint minu = 0, uint maxu = uint_max) => roundu(UnityEngine.Random.Range((float)minu, (float)maxu));
  public uint4 ARand_Random_uint4(uint a, uint b, uint c, uint d) => uint4(ARand_Random_uint(0, a), ARand_Random_uint(0, b), ARand_Random_uint(0, c), ARand_Random_uint(0, d));
  public uint4 ARand_Random_uint4() => ARand_Random_uint4(330382100u, 1073741822u, 252645134u, 1971u);
  public virtual void ARand_Init(uint _n, uint seed = 0)
  {
    ARand_N = _n;
    if (seed > 0) UnityEngine.Random.InitState((int)seed);
    ARand_seed4 = Rnd_u4();
    AddComputeBuffer(ref ARand_rs, nameof(ARand_rs), ARand_N);
    Gpu_ARand_initSeed();
    for (ARand_I = 1; ARand_I < ARand_N; ARand_I *= 2) for (ARand_J = 0; ARand_J < 4; ARand_J++) Gpu_ARand_initState();
  }
  public virtual void ARand_initSeed_GS(uint3 id) { uint i = id.x; ARand_rs[i] = i == 0 ? ARand_seed4 : u0000; }
  public virtual void ARand_initState_GS(uint3 id) { uint i = id.x + ARand_I; if (i < ARand_N) ARand_rs[i] = index(ARand_rs[i], ARand_J, ARand_UInt(id.x, 0, uint_max)); }
  protected uint ARand_u(uint a, int b, int c, int d, uint e) => ((a & e) << d) ^ (((a << b) ^ a) >> c);
  protected uint4 ARand_U4(uint4 r) => uint4(ARand_u(r.x, 13, 19, 12, 4294967294u), ARand_u(r.y, 2, 25, 4, 4294967288u), ARand_u(r.z, 3, 11, 17, 4294967280u), r.w * 1664525 + 1013904223u);
  protected uint ARand_UV(uint4 r) => cxor(r);
  protected float ARand_FV(uint4 r) => 2.3283064365387e-10f * ARand_UV(r);
  public uint4 ARand_rUInt4(uint i) => ARand_U4(ARand_rs[i]);
  public uint4 ARand_UInt4(uint i) => ARand_rs[i] = ARand_rUInt4(i);
  public float ARand_rFloat(uint i) => ARand_FV(ARand_rUInt4(i));
  public float ARand_rFloat(uint i, float a, float b) => lerp(a, b, ARand_rFloat(i));
  public float ARand_Float(uint i) => ARand_FV(ARand_UInt4(i));
  public float ARand_Float(uint i, float A, float B) => lerp(A, B, ARand_Float(i));
  public int ARand_Int(uint i, int A, int B) => floori(ARand_Float(i, A, B));
  public int ARand_Int(uint i) => ARand_Int(i, int_min, int_max);
  public uint ARand_UInt(uint i, uint A, uint B) => flooru(ARand_Float(i, A, B));
  public uint ARand_UInt(uint i) => ARand_UV(ARand_UInt4(i));
  protected float3 ARand_onSphere_(float a, float b) => rotateX(rotateZ(f100, acos(a * 2 - 1)), b * TwoPI);
  protected float3 ARand_onSphere_(uint i) { uint j = i * 2; return ARand_onSphere_(ARand_Float(j), ARand_Float(j + 1)); }
  protected float3 ARand_onCircle_(float a) => rotateZ(f100, a * TwoPI);
  public float3 ARand_onSphere(uint i) { uint j = i * 2; return ARand_onSphere_(ARand_Float(j), ARand_Float(j + 1)); }
  public float3 ARand_inSphere(uint i) { uint j = i * 3; return pow(ARand_Float(j + 2), 0.3333333f) * ARand_onSphere_(ARand_Float(j), ARand_Float(j + 1)); }
  public float3 ARand_onCircle(uint i) => ARand_onCircle_(ARand_Float(i));
  public float3 ARand_inCircle(uint i) { uint j = i * 2; return ARand_onCircle_(ARand_Float(j)) * sqrt(ARand_Float(j + 1)); }
  public float3 ARand_inCube(uint i) { uint j = i * 3; return float3(ARand_Float(j), ARand_Float(j + 1), ARand_Float(j + 2)); }
  public float ARand_gauss(uint i) { uint j = i * 2; return sqrt(-2 * ln(1 - ARand_Float(j))) * cos(TwoPI * (1 - ARand_Float(j + 1))); }
  public float ARand_gauss(uint i, float mean, float standardDeviation) => standardDeviation * ARand_gauss(i) + mean;
  public float ARand_exponential(uint i) => -log(ARand_Float(i));
  public float ARand_exponential(uint i, float mean) => mean * ARand_exponential(i);

  public virtual void base_ARand_Start0_GS() { }
  public virtual void base_ARand_Start1_GS() { }
  public virtual void base_ARand_OnApplicationQuit() { Save_UI(); OnApplicationQuit_GS(); base.OnApplicationQuit(); already_quited = true; }
  public virtual void base_ARand_LateUpdate0_GS() { }
  public virtual void base_ARand_LateUpdate1_GS() { }
  public virtual void base_ARand_Update0_GS() { }
  public virtual void base_ARand_Update1_GS() { }
  public virtual void base_ARand_OnValueChanged_GS() { }
  public virtual void base_ARand_InitBuffers0_GS() { }
  public virtual void base_ARand_InitBuffers1_GS() { }
  [HideInInspector]
  public virtual void base_ARand_onRenderObject_GS(ref bool render, ref bool cpu) { }
  public virtual float4 base_frag_ARand_GS(v2f i, float4 color)
  {
    uint libI = roundu(i.tj.x);
    return color;
  }
  public virtual void ARand_InitBuffers0_GS() { }
  public virtual void ARand_InitBuffers1_GS() { }
  public virtual void ARand_LateUpdate0_GS() { }
  public virtual void ARand_LateUpdate1_GS() { }
  public virtual void ARand_Update0_GS() { }
  public virtual void ARand_Update1_GS() { }
  public virtual void ARand_Start0_GS() { }
  public virtual void ARand_Start1_GS() { }
  public virtual void ARand_OnValueChanged_GS() { }
  public virtual void ARand_OnApplicationQuit_GS() { }
  public virtual void ARand_onRenderObject_GS(ref bool render, ref bool cpu) { }
  public virtual float4 frag_ARand_GS(v2f i, float4 color)
  {
    return color;
  }
  #endregion <ARand>
}