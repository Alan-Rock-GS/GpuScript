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
public class gsAppendBuff_Doc_ : GS
{
  [HideInInspector] public uiData data;
  [HideInInspector] public GAppendBuff_Doc g; public GAppendBuff_Doc G { get { gAppendBuff_Doc.GetData(); return gAppendBuff_Doc[0]; } }
  public void g_SetData() { if (gChanged && gAppendBuff_Doc != null) { gAppendBuff_Doc[0] = g; gAppendBuff_Doc.SetData(); gChanged = false; } }
  public virtual void primes_SetKernels(bool reallocate = false) { if (primes != null && (reallocate || primes.reallocated)) { SetKernelValues(primes, nameof(primes)); primes.reallocated = false; } }
  public virtual void AppendBuff_Bits_SetKernels(bool reallocate = false) { if (AppendBuff_Bits != null && (reallocate || AppendBuff_Bits.reallocated)) { SetKernelValues(AppendBuff_Bits, nameof(AppendBuff_Bits), kernel_calc_primes, kernel_init_Primes, kernel_AppendBuff_Get_Existing_Sums, kernel_AppendBuff_Get_Existing_Bits, kernel_AppendBuff_GetIndexes, kernel_AppendBuff_Get_Bits_Sums, kernel_AppendBuff_GetSums, kernel_AppendBuff_Get_Bits); AppendBuff_Bits.reallocated = false; } }
  public virtual void AppendBuff_Sums_SetKernels(bool reallocate = false) { if (AppendBuff_Sums != null && (reallocate || AppendBuff_Sums.reallocated)) { SetKernelValues(AppendBuff_Sums, nameof(AppendBuff_Sums), kernel_AppendBuff_Get_Existing_Sums, kernel_AppendBuff_GetIndexes, kernel_AppendBuff_IncSums, kernel_AppendBuff_GetFills1, kernel_AppendBuff_Get_Bits_Sums, kernel_AppendBuff_GetSums); AppendBuff_Sums.reallocated = false; } }
  public virtual void AppendBuff_Indexes_SetKernels(bool reallocate = false) { if (AppendBuff_Indexes != null && (reallocate || AppendBuff_Indexes.reallocated)) { SetKernelValues(AppendBuff_Indexes, nameof(AppendBuff_Indexes), kernel_AppendBuff_GetIndexes); AppendBuff_Indexes.reallocated = false; } }
  public virtual void AppendBuff_Fills1_SetKernels(bool reallocate = false) { if (AppendBuff_Fills1 != null && (reallocate || AppendBuff_Fills1.reallocated)) { SetKernelValues(AppendBuff_Fills1, nameof(AppendBuff_Fills1), kernel_AppendBuff_IncSums, kernel_AppendBuff_IncFills1, kernel_AppendBuff_GetFills2, kernel_AppendBuff_GetFills1); AppendBuff_Fills1.reallocated = false; } }
  public virtual void AppendBuff_Fills2_SetKernels(bool reallocate = false) { if (AppendBuff_Fills2 != null && (reallocate || AppendBuff_Fills2.reallocated)) { SetKernelValues(AppendBuff_Fills2, nameof(AppendBuff_Fills2), kernel_AppendBuff_IncFills1, kernel_AppendBuff_GetFills2); AppendBuff_Fills2.reallocated = false; } }
  public virtual void Gpu_calc_primes() { g_SetData(); AppendBuff_Bits?.SetCpu(); AppendBuff_Bits_SetKernels(); Gpu(kernel_calc_primes, calc_primes, uint2(piN / 2, pjN / 2)); AppendBuff_Bits?.ResetWrite(); }
  public virtual void Cpu_calc_primes() { AppendBuff_Bits?.GetGpu(); Cpu(calc_primes, uint2(piN / 2, pjN / 2)); AppendBuff_Bits.SetData(); }
  public virtual void Cpu_calc_primes(uint3 id) { AppendBuff_Bits?.GetGpu(); calc_primes(id); AppendBuff_Bits.SetData(); }
  public virtual void Gpu_init_Primes() { g_SetData(); AppendBuff_Bits_SetKernels(); Gpu(kernel_init_Primes, init_Primes, AppendBuff_BitN); AppendBuff_Bits?.ResetWrite(); }
  public virtual void Cpu_init_Primes() { AppendBuff_Bits?.GetGpu(); Cpu(init_Primes, AppendBuff_BitN); AppendBuff_Bits.SetData(); }
  public virtual void Cpu_init_Primes(uint3 id) { AppendBuff_Bits?.GetGpu(); init_Primes(id); AppendBuff_Bits.SetData(); }
  public virtual void Gpu_AppendBuff_Get_Existing_Sums() { g_SetData(); AppendBuff_Bits?.SetCpu(); AppendBuff_Bits_SetKernels(); AppendBuff_Sums_SetKernels(); Gpu(kernel_AppendBuff_Get_Existing_Sums, AppendBuff_Get_Existing_Sums, AppendBuff_BitN); AppendBuff_Sums?.ResetWrite(); }
  public virtual IEnumerator Cpu_AppendBuff_Get_Existing_Sums() { AppendBuff_Bits?.GetGpu(); AppendBuff_Sums?.GetGpu(); yield return StartCoroutine(Cpu_Coroutine(kernel_AppendBuff_Get_Existing_Sums, AppendBuff_Get_Existing_Sums, AppendBuff_BitN)); }
  public virtual void Cpu_AppendBuff_Get_Existing_Sums(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { AppendBuff_Bits?.GetGpu(); AppendBuff_Sums?.GetGpu(); AppendBuff_Get_Existing_Sums(grp_tid, grp_id, id, grpI); AppendBuff_Sums.SetData(); }
  public virtual void Gpu_AppendBuff_Get_Existing_Bits() { g_SetData(); AppendBuff_Bits?.SetCpu(); AppendBuff_Bits_SetKernels(); Gpu(kernel_AppendBuff_Get_Existing_Bits, AppendBuff_Get_Existing_Bits, AppendBuff_BitN); AppendBuff_Bits?.ResetWrite(); }
  public virtual void Cpu_AppendBuff_Get_Existing_Bits() { AppendBuff_Bits?.GetGpu(); Cpu(AppendBuff_Get_Existing_Bits, AppendBuff_BitN); AppendBuff_Bits.SetData(); }
  public virtual void Cpu_AppendBuff_Get_Existing_Bits(uint3 id) { AppendBuff_Bits?.GetGpu(); AppendBuff_Get_Existing_Bits(id); AppendBuff_Bits.SetData(); }
  public virtual void Gpu_AppendBuff_GetIndexes() { g_SetData(); AppendBuff_Bits?.SetCpu(); AppendBuff_Bits_SetKernels(); AppendBuff_Sums?.SetCpu(); AppendBuff_Sums_SetKernels(); AppendBuff_Indexes_SetKernels(); Gpu(kernel_AppendBuff_GetIndexes, AppendBuff_GetIndexes, AppendBuff_BitN); AppendBuff_Indexes?.ResetWrite(); }
  public virtual void Cpu_AppendBuff_GetIndexes() { AppendBuff_Bits?.GetGpu(); AppendBuff_Sums?.GetGpu(); AppendBuff_Indexes?.GetGpu(); Cpu(AppendBuff_GetIndexes, AppendBuff_BitN); AppendBuff_Indexes.SetData(); }
  public virtual void Cpu_AppendBuff_GetIndexes(uint3 id) { AppendBuff_Bits?.GetGpu(); AppendBuff_Sums?.GetGpu(); AppendBuff_Indexes?.GetGpu(); AppendBuff_GetIndexes(id); AppendBuff_Indexes.SetData(); }
  public virtual void Gpu_AppendBuff_IncSums() { g_SetData(); AppendBuff_Sums?.SetCpu(); AppendBuff_Sums_SetKernels(); AppendBuff_Fills1?.SetCpu(); AppendBuff_Fills1_SetKernels(); gAppendBuff_Doc?.SetCpu(); Gpu(kernel_AppendBuff_IncSums, AppendBuff_IncSums, AppendBuff_BitN); AppendBuff_Sums?.ResetWrite(); g = G; }
  public virtual void Cpu_AppendBuff_IncSums() { AppendBuff_Sums?.GetGpu(); AppendBuff_Fills1?.GetGpu(); Cpu(AppendBuff_IncSums, AppendBuff_BitN); AppendBuff_Sums.SetData(); }
  public virtual void Cpu_AppendBuff_IncSums(uint3 id) { AppendBuff_Sums?.GetGpu(); AppendBuff_Fills1?.GetGpu(); AppendBuff_IncSums(id); AppendBuff_Sums.SetData(); }
  public virtual void Gpu_AppendBuff_IncFills1() { g_SetData(); AppendBuff_Fills1?.SetCpu(); AppendBuff_Fills1_SetKernels(); AppendBuff_Fills2?.SetCpu(); AppendBuff_Fills2_SetKernels(); Gpu(kernel_AppendBuff_IncFills1, AppendBuff_IncFills1, AppendBuff_BitN1); AppendBuff_Fills1?.ResetWrite(); }
  public virtual void Cpu_AppendBuff_IncFills1() { AppendBuff_Fills1?.GetGpu(); AppendBuff_Fills2?.GetGpu(); Cpu(AppendBuff_IncFills1, AppendBuff_BitN1); AppendBuff_Fills1.SetData(); }
  public virtual void Cpu_AppendBuff_IncFills1(uint3 id) { AppendBuff_Fills1?.GetGpu(); AppendBuff_Fills2?.GetGpu(); AppendBuff_IncFills1(id); AppendBuff_Fills1.SetData(); }
  public virtual void Gpu_AppendBuff_GetFills2() { g_SetData(); AppendBuff_Fills1?.SetCpu(); AppendBuff_Fills1_SetKernels(); AppendBuff_Fills2_SetKernels(); Gpu(kernel_AppendBuff_GetFills2, AppendBuff_GetFills2, AppendBuff_BitN2); AppendBuff_Fills2?.ResetWrite(); }
  public virtual IEnumerator Cpu_AppendBuff_GetFills2() { AppendBuff_Fills1?.GetGpu(); AppendBuff_Fills2?.GetGpu(); yield return StartCoroutine(Cpu_Coroutine(kernel_AppendBuff_GetFills2, AppendBuff_GetFills2, AppendBuff_BitN2)); }
  public virtual void Cpu_AppendBuff_GetFills2(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { AppendBuff_Fills1?.GetGpu(); AppendBuff_Fills2?.GetGpu(); AppendBuff_GetFills2(grp_tid, grp_id, id, grpI); AppendBuff_Fills2.SetData(); }
  public virtual void Gpu_AppendBuff_GetFills1() { g_SetData(); AppendBuff_Sums?.SetCpu(); AppendBuff_Sums_SetKernels(); AppendBuff_Fills1_SetKernels(); Gpu(kernel_AppendBuff_GetFills1, AppendBuff_GetFills1, AppendBuff_BitN1); AppendBuff_Fills1?.ResetWrite(); }
  public virtual IEnumerator Cpu_AppendBuff_GetFills1() { AppendBuff_Sums?.GetGpu(); AppendBuff_Fills1?.GetGpu(); yield return StartCoroutine(Cpu_Coroutine(kernel_AppendBuff_GetFills1, AppendBuff_GetFills1, AppendBuff_BitN1)); }
  public virtual void Cpu_AppendBuff_GetFills1(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { AppendBuff_Sums?.GetGpu(); AppendBuff_Fills1?.GetGpu(); AppendBuff_GetFills1(grp_tid, grp_id, id, grpI); AppendBuff_Fills1.SetData(); }
  public virtual void Gpu_AppendBuff_Get_Bits_Sums() { g_SetData(); AppendBuff_Bits?.SetCpu(); AppendBuff_Bits_SetKernels(); AppendBuff_Sums_SetKernels(); Gpu(kernel_AppendBuff_Get_Bits_Sums, AppendBuff_Get_Bits_Sums, AppendBuff_BitN); AppendBuff_Sums?.ResetWrite(); }
  public virtual IEnumerator Cpu_AppendBuff_Get_Bits_Sums() { AppendBuff_Bits?.GetGpu(); AppendBuff_Sums?.GetGpu(); yield return StartCoroutine(Cpu_Coroutine(kernel_AppendBuff_Get_Bits_Sums, AppendBuff_Get_Bits_Sums, AppendBuff_BitN)); }
  public virtual void Cpu_AppendBuff_Get_Bits_Sums(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { AppendBuff_Bits?.GetGpu(); AppendBuff_Sums?.GetGpu(); AppendBuff_Get_Bits_Sums(grp_tid, grp_id, id, grpI); AppendBuff_Sums.SetData(); }
  public virtual void Gpu_AppendBuff_GetSums() { g_SetData(); AppendBuff_Bits_SetKernels(); AppendBuff_Sums_SetKernels(); Gpu(kernel_AppendBuff_GetSums, AppendBuff_GetSums, AppendBuff_BitN); AppendBuff_Bits?.ResetWrite(); AppendBuff_Sums?.ResetWrite(); }
  public virtual IEnumerator Cpu_AppendBuff_GetSums() { AppendBuff_Bits?.GetGpu(); AppendBuff_Sums?.GetGpu(); yield return StartCoroutine(Cpu_Coroutine(kernel_AppendBuff_GetSums, AppendBuff_GetSums, AppendBuff_BitN)); }
  public virtual void Cpu_AppendBuff_GetSums(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { AppendBuff_Bits?.GetGpu(); AppendBuff_Sums?.GetGpu(); AppendBuff_GetSums(grp_tid, grp_id, id, grpI); AppendBuff_Bits.SetData(); AppendBuff_Sums.SetData(); }
  public virtual void Gpu_AppendBuff_Get_Bits() { g_SetData(); AppendBuff_Bits_SetKernels(); Gpu(kernel_AppendBuff_Get_Bits, AppendBuff_Get_Bits, AppendBuff_BitN); AppendBuff_Bits?.ResetWrite(); }
  public virtual void Cpu_AppendBuff_Get_Bits() { AppendBuff_Bits?.GetGpu(); Cpu(AppendBuff_Get_Bits, AppendBuff_BitN); AppendBuff_Bits.SetData(); }
  public virtual void Cpu_AppendBuff_Get_Bits(uint3 id) { AppendBuff_Bits?.GetGpu(); AppendBuff_Get_Bits(id); AppendBuff_Bits.SetData(); }
  [JsonConverter(typeof(StringEnumConverter))] public enum ProcessorType : uint { CPU, GPU }
  public const uint ProcessorType_CPU = 0, ProcessorType_GPU = 1;
  [HideInInspector] public bool ValuesChanged, gChanged;
  public static gsAppendBuff_Doc This;
  public virtual void Awake() { This = this as gsAppendBuff_Doc; Awake_GS(); }
  public virtual void Awake_GS()
  {
  }
  public virtual void Start() { Build_UI(); Load_UI(); Start0_GS(); InitBuffers(); Start1_GS(); }
  public virtual void Start0_GS()
  {
    AppendBuff_Start0_GS();
  }
  public virtual void Start1_GS()
  {
    AppendBuff_Start1_GS();
  }
  public override void onLoaded()
  {
    base.onLoaded();
  }
  [HideInInspector] public bool already_quited = false;
  public override void OnApplicationQuit() { Save_UI(); OnApplicationQuit_GS(); base.OnApplicationQuit(); already_quited = true; }
  public virtual void OnApplicationQuit_GS()
  {
    AppendBuff_OnApplicationQuit_GS();
  }
  public override void ui_to_data()
  {
    if (data == null) return;
    data.siUnits = siUnits;
    data.group_UI = group_UI;
    data.group_AppendBuff = group_AppendBuff;
    data.maxPrimeN = maxPrimeN;
    data.group_AppendBuffTest = group_AppendBuffTest;
    data.AppendBuffTest_N = AppendBuffTest_N;
    data.AppendBuffTest_Runtime_N = AppendBuffTest_Runtime_N;
    data.processorType = processorType;
    data.runOnGpu = runOnGpu;
    data.AppendBuffTest_IndexN = AppendBuffTest_IndexN;
    data.AppendBuffTest_Time_us = AppendBuffTest_Time_us;
  }
  public override void data_to_ui()
  {
    if (data == null) return;
    group_UI = data.group_UI;
    group_AppendBuff = data.group_AppendBuff;
    maxPrimeN = ui_txt_str.Contains("\"maxPrimeN\"") ? data.maxPrimeN : 1000;
    group_AppendBuffTest = data.group_AppendBuffTest;
    AppendBuffTest_N = ui_txt_str.Contains("\"AppendBuffTest_N\"") ? data.AppendBuffTest_N : 1;
    AppendBuffTest_Runtime_N = ui_txt_str.Contains("\"AppendBuffTest_Runtime_N\"") ? data.AppendBuffTest_Runtime_N : 1;
    processorType = data.processorType;
    runOnGpu = data.runOnGpu;
    AppendBuffTest_IndexN = data.AppendBuffTest_IndexN;
    AppendBuffTest_Time_us = data.AppendBuffTest_Time_us;
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
    if (UI_maxPrimeN.Changed || maxPrimeN != UI_maxPrimeN.v) maxPrimeN = UI_maxPrimeN.v;
    if (UI_AppendBuffTest_N.Changed || AppendBuffTest_N != UI_AppendBuffTest_N.v) AppendBuffTest_N = UI_AppendBuffTest_N.v;
    if (UI_AppendBuffTest_Runtime_N.Changed || AppendBuffTest_Runtime_N != UI_AppendBuffTest_Runtime_N.v) AppendBuffTest_Runtime_N = UI_AppendBuffTest_Runtime_N.v;
    if (UI_processorType.Changed || (uint)processorType != UI_processorType.v) processorType = (ProcessorType)UI_processorType.v;
    if (UI_runOnGpu.Changed || runOnGpu != UI_runOnGpu.v) runOnGpu = UI_runOnGpu.v;
    if (UI_AppendBuffTest_IndexN.Changed || AppendBuffTest_IndexN != UI_AppendBuffTest_IndexN.v) AppendBuffTest_IndexN = UI_AppendBuffTest_IndexN.v;
    if (UI_AppendBuffTest_Time_us.Changed || AppendBuffTest_Time_us != UI_AppendBuffTest_Time_us.v) AppendBuffTest_Time_us = UI_AppendBuffTest_Time_us.v;
    if (ValuesChanged) { gChanged = true; OnValueChanged(); ValuesChanged = UI_group_UI.Changed = UI_group_AppendBuff.Changed = UI_maxPrimeN.Changed = UI_group_AppendBuffTest.Changed = UI_AppendBuffTest_N.Changed = UI_AppendBuffTest_Runtime_N.Changed = UI_processorType.Changed = UI_runOnGpu.Changed = UI_AppendBuffTest_IndexN.Changed = UI_AppendBuffTest_Time_us.Changed = false; }
    AppendBuff_LateUpdate1_GS();
    LateUpdate1_GS();
    if(lateUpdateI++ == uint_max) lateUpdateI = 100;
  }
  public virtual void LateUpdate0_GS()
  {
    AppendBuff_LateUpdate0_GS();
  }
  public virtual void LateUpdate1_GS()
  {
    AppendBuff_LateUpdate1_GS();
  }
  public override void Update()
  {
    base.Update();
    if (!ui_loaded) return;
    Update0_GS();
    AppendBuff_Update1_GS();
    Update1_GS();
  }
  public virtual void Update0_GS()
  {
    AppendBuff_Update0_GS();
  }
  public virtual void Update1_GS()
  {
    AppendBuff_Update1_GS();
  }
  public override void OnValueChanged()
  {
    if (!ui_loaded) return;
    OnValueChanged_GS();
  }
  public override void OnValueChanged_GS()
  {
    AppendBuff_OnValueChanged_GS();
    var type = "gsReport_Lib".ToType();
    if (type != null) ((GS)GetComponent(type))?.OnValueChanged_GS();

  }
  public string[] Scenes_in_Build => new string[] {  };
  public virtual uint maxPrimeN { get => g.maxPrimeN; set { if (g.maxPrimeN != value || UI_maxPrimeN.v != value) { g.maxPrimeN = UI_maxPrimeN.v = value; ValuesChanged = gChanged = true; } } }
  public virtual uint AppendBuffTest_N { get => g.AppendBuffTest_N; set { if (g.AppendBuffTest_N != value || UI_AppendBuffTest_N.v != value) { g.AppendBuffTest_N = UI_AppendBuffTest_N.v = value; ValuesChanged = gChanged = true; } } }
  public virtual uint AppendBuffTest_Runtime_N { get => g.AppendBuffTest_Runtime_N; set { if (g.AppendBuffTest_Runtime_N != value || UI_AppendBuffTest_Runtime_N.v != value) { g.AppendBuffTest_Runtime_N = UI_AppendBuffTest_Runtime_N.v = value; ValuesChanged = gChanged = true; } } }
  public virtual ProcessorType processorType { get => (ProcessorType)g.processorType; set { if ((ProcessorType)g.processorType != value || (ProcessorType)UI_processorType.v != value) { g.processorType = UI_processorType.v = (uint)value; ValuesChanged = gChanged = true; } } }
  public virtual bool runOnGpu { get => Is(g.runOnGpu); set { if (g.runOnGpu != Is(value) || UI_runOnGpu.v != value) { g.runOnGpu = Is(UI_runOnGpu.v = value); ValuesChanged = gChanged = true; } } }
  public virtual uint AppendBuffTest_IndexN { get => g.AppendBuffTest_IndexN; set { if (g.AppendBuffTest_IndexN != value || UI_AppendBuffTest_IndexN.v != value) { g.AppendBuffTest_IndexN = UI_AppendBuffTest_IndexN.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float AppendBuffTest_Time_us { get => g.AppendBuffTest_Time_us; set { if (any(g.AppendBuffTest_Time_us != value) || any(UI_AppendBuffTest_Time_us.v != value)) { g.AppendBuffTest_Time_us = UI_AppendBuffTest_Time_us.v = value; ValuesChanged = gChanged = true; } } }
  public virtual uint primeFactor { get => g.primeFactor; set { if (g.primeFactor != value) { g.primeFactor = value; ValuesChanged = gChanged = true; } } }
  public virtual uint pN { get => g.pN; set { if (g.pN != value) { g.pN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint piN { get => g.piN; set { if (g.piN != value) { g.piN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint pjN { get => g.pjN; set { if (g.pjN != value) { g.pjN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint AppendBuff_IndexN { get => g.AppendBuff_IndexN; set { if (g.AppendBuff_IndexN != value) { g.AppendBuff_IndexN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint AppendBuff_BitN { get => g.AppendBuff_BitN; set { if (g.AppendBuff_BitN != value) { g.AppendBuff_BitN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint AppendBuff_N { get => g.AppendBuff_N; set { if (g.AppendBuff_N != value) { g.AppendBuff_N = value; ValuesChanged = gChanged = true; } } }
  public virtual uint AppendBuff_BitN1 { get => g.AppendBuff_BitN1; set { if (g.AppendBuff_BitN1 != value) { g.AppendBuff_BitN1 = value; ValuesChanged = gChanged = true; } } }
  public virtual uint AppendBuff_BitN2 { get => g.AppendBuff_BitN2; set { if (g.AppendBuff_BitN2 != value) { g.AppendBuff_BitN2 = value; ValuesChanged = gChanged = true; } } }
  public bool group_UI { get => UI_group_UI?.v ?? false; set { if (UI_group_UI != null) UI_group_UI.v = value; } }
  public bool group_AppendBuff { get => UI_group_AppendBuff?.v ?? false; set { if (UI_group_AppendBuff != null) UI_group_AppendBuff.v = value; } }
  public bool group_AppendBuffTest { get => UI_group_AppendBuffTest?.v ?? false; set { if (UI_group_AppendBuffTest != null) UI_group_AppendBuffTest.v = value; } }
  public UI_TreeGroup UI_group_UI, UI_group_AppendBuff, UI_group_AppendBuffTest;
  public UI_uint UI_maxPrimeN, UI_AppendBuffTest_N, UI_AppendBuffTest_Runtime_N, UI_AppendBuffTest_IndexN;
  public UI_method UI_CalcPrimes;
  public virtual void CalcPrimes() { }
  public UI_enum UI_processorType;
  public UI_bool UI_runOnGpu;
  public UI_method UI_Run_Append_Buffer;
  public virtual void Run_Append_Buffer() { }
  public UI_float UI_AppendBuffTest_Time_us;
  public UI_TreeGroup ui_group_UI => UI_group_UI;
  public UI_TreeGroup ui_group_AppendBuff => UI_group_AppendBuff;
  public UI_uint ui_maxPrimeN => UI_maxPrimeN;
  public UI_TreeGroup ui_group_AppendBuffTest => UI_group_AppendBuffTest;
  public UI_uint ui_AppendBuffTest_N => UI_AppendBuffTest_N;
  public UI_uint ui_AppendBuffTest_Runtime_N => UI_AppendBuffTest_Runtime_N;
  public UI_enum ui_processorType => UI_processorType;
  public UI_bool ui_runOnGpu => UI_runOnGpu;
  public UI_uint ui_AppendBuffTest_IndexN => UI_AppendBuffTest_IndexN;
  public UI_float ui_AppendBuffTest_Time_us => UI_AppendBuffTest_Time_us;
  [Serializable]
  public class uiData
  {
    public bool siUnits, group_UI, group_AppendBuff, group_AppendBuffTest, runOnGpu;
    public uint maxPrimeN, AppendBuffTest_N, AppendBuffTest_Runtime_N, AppendBuffTest_IndexN;
    [JsonConverter(typeof(StringEnumConverter))] public ProcessorType processorType;
    public float AppendBuffTest_Time_us;
  }
  public virtual void InitBuffers()
  {
    InitBuffers0_GS();
    AddComputeBuffer(ref gAppendBuff_Doc, nameof(gAppendBuff_Doc), 1);
    InitKernels();
    SetKernelValues(gAppendBuff_Doc, nameof(gAppendBuff_Doc), kernel_calc_primes, kernel_init_Primes, kernel_AppendBuff_Get_Existing_Sums, kernel_AppendBuff_Get_Existing_Bits, kernel_AppendBuff_GetIndexes, kernel_AppendBuff_IncSums, kernel_AppendBuff_IncFills1, kernel_AppendBuff_GetFills2, kernel_AppendBuff_GetFills1, kernel_AppendBuff_Get_Bits_Sums, kernel_AppendBuff_GetSums, kernel_AppendBuff_Get_Bits);
    InitBuffers1_GS();
    isInitBuffers = true;
  }
  public virtual void InitBuffers0_GS()
  {
    AppendBuff_InitBuffers0_GS();
  }
  public virtual void InitBuffers1_GS()
  {
    AppendBuff_InitBuffers1_GS();
  }
  [HideInInspector] public uint[] AppendBuff_grp = new uint[1024];
  [HideInInspector] public uint[] AppendBuff_grp0 = new uint[1024];
  [Serializable]
  public struct GAppendBuff_Doc
  {
    public uint maxPrimeN, AppendBuffTest_N, AppendBuffTest_Runtime_N, processorType, runOnGpu, AppendBuffTest_IndexN, primeFactor, pN, piN, pjN, AppendBuff_IndexN, AppendBuff_BitN, AppendBuff_N, AppendBuff_BitN1, AppendBuff_BitN2;
    public float AppendBuffTest_Time_us;
  };
  public RWStructuredBuffer<GAppendBuff_Doc> gAppendBuff_Doc;
  public RWStructuredBuffer<uint> primes, AppendBuff_Bits, AppendBuff_Sums, AppendBuff_Indexes, AppendBuff_Fills1, AppendBuff_Fills2;
  public virtual void AllocData_primes(uint n) => AddComputeBuffer(ref primes, nameof(primes), n);
  public virtual void AssignData_primes(params uint[] data) => AddComputeBufferData(ref primes, nameof(primes), data);
  public virtual void AllocData_AppendBuff_Bits(uint n) => AddComputeBuffer(ref AppendBuff_Bits, nameof(AppendBuff_Bits), n);
  public virtual void AssignData_AppendBuff_Bits(params uint[] data) => AddComputeBufferData(ref AppendBuff_Bits, nameof(AppendBuff_Bits), data);
  public virtual void AllocData_AppendBuff_Sums(uint n) => AddComputeBuffer(ref AppendBuff_Sums, nameof(AppendBuff_Sums), n);
  public virtual void AssignData_AppendBuff_Sums(params uint[] data) => AddComputeBufferData(ref AppendBuff_Sums, nameof(AppendBuff_Sums), data);
  public virtual void AllocData_AppendBuff_Indexes(uint n) => AddComputeBuffer(ref AppendBuff_Indexes, nameof(AppendBuff_Indexes), n);
  public virtual void AssignData_AppendBuff_Indexes(params uint[] data) => AddComputeBufferData(ref AppendBuff_Indexes, nameof(AppendBuff_Indexes), data);
  public virtual void AllocData_AppendBuff_Fills1(uint n) => AddComputeBuffer(ref AppendBuff_Fills1, nameof(AppendBuff_Fills1), n);
  public virtual void AssignData_AppendBuff_Fills1(params uint[] data) => AddComputeBufferData(ref AppendBuff_Fills1, nameof(AppendBuff_Fills1), data);
  public virtual void AllocData_AppendBuff_Fills2(uint n) => AddComputeBuffer(ref AppendBuff_Fills2, nameof(AppendBuff_Fills2), n);
  public virtual void AssignData_AppendBuff_Fills2(params uint[] data) => AddComputeBufferData(ref AppendBuff_Fills2, nameof(AppendBuff_Fills2), data);
  public Texture2D _PaletteTex;
  public float4 palette(float v) => paletteColor(_PaletteTex, v);
  public float4 palette(float v, float w) => float4(palette(v).xyz, w);

  public virtual void onRenderObject_LIN(bool show, uint _itemN, ref uint i, ref uint index, ref uint3 LIN) { uint n = 0; if (show) { if (i < (n = _itemN)) LIN = uint3(index, i, 0); LIN.z += n; i -= n; } index++; }
  public virtual void onRenderObject_LIN(uint _itemN, ref uint i, ref uint index, ref uint3 LIN) { onRenderObject_LIN(true, _itemN, ref i, ref index, ref LIN); }
  public virtual uint3 onRenderObject_LIN(uint i) { uint3 LIN = u000; return LIN; }
  public virtual uint3 onRenderObject_LIN_Quads(uint i) { uint3 LIN = u000; return LIN; }
  public virtual uint3 onRenderObject_LIN_Points(uint i) { uint3 LIN = u000; return LIN; }
  [HideInInspector] public int kernel_calc_primes; [numthreads(numthreads2, numthreads2, 1)] protected void calc_primes(uint3 id) { unchecked { if (id.y < pjN / 2 && id.x < piN / 2) calc_primes_GS(id); } }
  public virtual void calc_primes_GS(uint3 id) { }
  [HideInInspector] public int kernel_init_Primes; [numthreads(numthreads1, 1, 1)] protected void init_Primes(uint3 id) { unchecked { if (id.x < AppendBuff_BitN) init_Primes_GS(id); } }
  public virtual void init_Primes_GS(uint3 id) { }
  [HideInInspector] public int kernel_AppendBuff_Get_Existing_Sums; [numthreads(numthreads1, 1, 1)] protected IEnumerator AppendBuff_Get_Existing_Sums(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < AppendBuff_BitN) yield return StartCoroutine(AppendBuff_Get_Existing_Sums_GS(grp_tid, grp_id, id, grpI)); } }
  public virtual IEnumerator AppendBuff_Get_Existing_Sums_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { yield return null; }
  [HideInInspector] public int kernel_AppendBuff_Get_Existing_Bits; [numthreads(numthreads1, 1, 1)] protected void AppendBuff_Get_Existing_Bits(uint3 id) { unchecked { if (id.x < AppendBuff_BitN) AppendBuff_Get_Existing_Bits_GS(id); } }
  public virtual void AppendBuff_Get_Existing_Bits_GS(uint3 id) { }
  [HideInInspector] public int kernel_AppendBuff_GetIndexes; [numthreads(numthreads1, 1, 1)] protected void AppendBuff_GetIndexes(uint3 id) { unchecked { if (id.x < AppendBuff_BitN) AppendBuff_GetIndexes_GS(id); } }
  [HideInInspector] public int kernel_AppendBuff_IncSums; [numthreads(numthreads1, 1, 1)] protected void AppendBuff_IncSums(uint3 id) { unchecked { if (id.x < AppendBuff_BitN) AppendBuff_IncSums_GS(id); } }
  [HideInInspector] public int kernel_AppendBuff_IncFills1; [numthreads(numthreads1, 1, 1)] protected void AppendBuff_IncFills1(uint3 id) { unchecked { if (id.x < AppendBuff_BitN1) AppendBuff_IncFills1_GS(id); } }
  [HideInInspector] public int kernel_AppendBuff_GetFills2; [numthreads(numthreads1, 1, 1)] protected IEnumerator AppendBuff_GetFills2(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < AppendBuff_BitN2) yield return StartCoroutine(AppendBuff_GetFills2_GS(grp_tid, grp_id, id, grpI)); } }
  [HideInInspector] public int kernel_AppendBuff_GetFills1; [numthreads(numthreads1, 1, 1)] protected IEnumerator AppendBuff_GetFills1(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < AppendBuff_BitN1) yield return StartCoroutine(AppendBuff_GetFills1_GS(grp_tid, grp_id, id, grpI)); } }
  [HideInInspector] public int kernel_AppendBuff_Get_Bits_Sums; [numthreads(numthreads1, 1, 1)] protected IEnumerator AppendBuff_Get_Bits_Sums(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < AppendBuff_BitN) yield return StartCoroutine(AppendBuff_Get_Bits_Sums_GS(grp_tid, grp_id, id, grpI)); } }
  [HideInInspector] public int kernel_AppendBuff_GetSums; [numthreads(numthreads1, 1, 1)] protected IEnumerator AppendBuff_GetSums(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < AppendBuff_BitN) yield return StartCoroutine(AppendBuff_GetSums_GS(grp_tid, grp_id, id, grpI)); } }
  [HideInInspector] public int kernel_AppendBuff_Get_Bits; [numthreads(numthreads1, 1, 1)] protected void AppendBuff_Get_Bits(uint3 id) { unchecked { if (id.x < AppendBuff_BitN) AppendBuff_Get_Bits_GS(id); } }
  public virtual v2f vert_GS(uint i, uint j, v2f o)
  {
    return o;
  }
  public v2f vert(uint i, uint j)
  {
    v2f o = default;
    o.pos = o.color = o.tj = o.tk = f0000; o.normal = o.p0 = o.p1 = o.wPos = f000; o.uv = f00; o.ti = float4(i, j, 0, 0);
    return vert_GS(i, j, o);
  }
  public virtual void onRenderObject_GS(ref bool render, ref bool cpu)
  {
    AppendBuff_onRenderObject_GS(ref render, ref cpu);
  }
  public virtual float4 frag_GS(v2f i, float4 color)
  {
    uint libI = roundu(i.tj.x);
    return color;
  }
  public virtual float4 frag(v2f i)
  {
    float4 color = i.color;
    color = frag_GS(i, color);
    if (color.a == 0) Discard(0);
    return color;
  }
  #region <AppendBuff>
  public void AppendBuff_print_Bits(string title) { StrBldr sb = StrBldr($"{title}, AppendBuff_Bits"); for (uint i = 0; i < AppendBuff_BitN; i++) sb.Add(" ", AppendBuff_Bits[i]); print(sb); }
  public void AppendBuff_print_Sums(string title) { StrBldr sb = StrBldr($"{title}, AppendBuff_Sums"); for (uint i = 0; i < AppendBuff_BitN; i++) sb.Add(" ", AppendBuff_Sums[i]); print(sb); }
  public void AppendBuff_print_Indexes(string title) { StrBldr sb = StrBldr($"{title}: AppendBuff_Indexes"); for (uint i = 0; i < AppendBuff_IndexN; i++) sb.Add(" ", AppendBuff_Indexes[i]); print(sb); }
  public virtual bool AppendBuff_IsBitOn(uint i) => i % 32 == 0;
  public void AppendBuff_SetN(uint n)
  {
    if (n > 2147450880) throw new Exception("gsAppendBuff: AppendBuff_N > 2,147,450,880");
    AppendBuff_N = n; AppendBuff_BitN = ceilu(AppendBuff_N, 32); AppendBuff_BitN1 = ceilu(AppendBuff_BitN, numthreads1); AppendBuff_BitN2 = ceilu(AppendBuff_BitN1, numthreads1);
    AllocData_AppendBuff_Bits(AppendBuff_BitN); AllocData_AppendBuff_Fills1(AppendBuff_BitN1); AllocData_AppendBuff_Fills2(AppendBuff_BitN2); AllocData_AppendBuff_Sums(AppendBuff_BitN);
  }
  public void AppendBuff_FillPrefixes() { Gpu_AppendBuff_GetFills1(); Gpu_AppendBuff_GetFills2(); Gpu_AppendBuff_IncFills1(); Gpu_AppendBuff_IncSums(); }
  public void AppendBuff_getIndexes() { AllocData_AppendBuff_Indexes(AppendBuff_IndexN); Gpu_AppendBuff_GetIndexes(); }
  public void AppendBuff_FillIndexes() { AppendBuff_FillPrefixes(); AppendBuff_getIndexes(); }
  public virtual uint AppendBuff_Run(uint n) { AppendBuff_SetN(n); Gpu_AppendBuff_GetSums(); AppendBuff_FillIndexes(); return AppendBuff_IndexN; }
  public uint AppendBuff_Run(int n) => AppendBuff_Run((uint)n);
  public uint AppendBuff_Run(uint2 n) => AppendBuff_Run(cproduct(n)); public uint AppendBuff_Run(uint3 n) => AppendBuff_Run(cproduct(n));
  public uint AppendBuff_Run(int2 n) => AppendBuff_Run(cproduct(n)); public uint AppendBuff_Run(int3 n) => AppendBuff_Run(cproduct(n));
  public virtual void AppendBuff_Init_Bits(uint n, GS parent, string _N, string _BitN, ref RWStructuredBuffer<uint> _Bits) { AppendBuff_SetN(n); parent.SetValue(_N, AppendBuff_N); parent.SetValue(_BitN, AppendBuff_BitN); }
  public virtual void AppendBuff_Prefix_Sums() { Gpu_AppendBuff_Get_Bits_Sums(); AppendBuff_FillPrefixes(); }
  public virtual void AppendBuff_Bit_Indexes(RWStructuredBuffer<uint> bits, GS _this, string _IndexN, ref RWStructuredBuffer<uint> _Indexes) { AppendBuff_Prefix_Sums(); AppendBuff_getIndexes(); _this.SetValue(_IndexN, AppendBuff_IndexN); }
  public uint AppendBuff_Assign_Bits(uint i, uint j, uint bits) => bits | (Is(i < AppendBuff_N && AppendBuff_IsBitOn(i)) << (int)j);
  public virtual void AppendBuff_Get_Bits_GS(uint3 id) { uint i = id.x, j, k, bits = 0; if (i < AppendBuff_BitN) { for (j = 0, k = i * 32; j < 32; j++) bits = AppendBuff_Assign_Bits(k + j, j, bits); AppendBuff_Bits[i] = bits; } }
  public virtual IEnumerator AppendBuff_GetSums_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI)
  {
    uint i = id.x, c, s, j, k, bits = 0;
    if (i < AppendBuff_BitN) { for (j = 0, k = i * 32; j < 32; j++) bits = AppendBuff_Assign_Bits(k + j, j, bits); AppendBuff_Bits[i] = bits; c = countbits(bits); } else c = 0;
    AppendBuff_grp0[grpI] = c; AppendBuff_grp[grpI] = c; yield return GroupMemoryBarrierWithGroupSync();
    for (s = 1; s < numthreads1; s *= 2)
    {
      if (grpI >= s && i < AppendBuff_BitN) AppendBuff_grp[grpI] = AppendBuff_grp0[grpI] + AppendBuff_grp0[grpI - s]; yield return GroupMemoryBarrierWithGroupSync();
      AppendBuff_grp0[grpI] = AppendBuff_grp[grpI]; yield return GroupMemoryBarrierWithGroupSync();
    }
    if (i < AppendBuff_BitN) AppendBuff_Sums[i] = AppendBuff_grp[grpI];
  }
  public virtual IEnumerator AppendBuff_Get_Bits_Sums_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI)
  {
    uint i = id.x, c = i < AppendBuff_BitN ? countbits(AppendBuff_Bits[i]) : 0, s;
    AppendBuff_grp0[grpI] = c; AppendBuff_grp[grpI] = c; yield return GroupMemoryBarrierWithGroupSync();
    for (s = 1; s < numthreads1; s *= 2)
    {
      if (grpI >= s && i < AppendBuff_BitN) AppendBuff_grp[grpI] = AppendBuff_grp0[grpI] + AppendBuff_grp0[grpI - s]; yield return GroupMemoryBarrierWithGroupSync();
      AppendBuff_grp0[grpI] = AppendBuff_grp[grpI]; yield return GroupMemoryBarrierWithGroupSync();
    }
    if (i < AppendBuff_BitN) AppendBuff_Sums[i] = AppendBuff_grp[grpI];
  }
  public virtual IEnumerator AppendBuff_GetFills1_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI)
  {
    uint i = id.x, k = i + 1, j = k * numthreads1 - 1, c = i < AppendBuff_BitN1 - 1 ? AppendBuff_Sums[j] : i < AppendBuff_BitN1 ? AppendBuff_Sums[AppendBuff_BitN - 1] : 0, s;
    AppendBuff_grp0[grpI] = c; AppendBuff_grp[grpI] = c; yield return GroupMemoryBarrierWithGroupSync();
    for (s = 1; s < numthreads1; s *= 2)
    {
      if (grpI >= s && i < AppendBuff_BitN1) AppendBuff_grp[grpI] = AppendBuff_grp0[grpI] + AppendBuff_grp0[grpI - s]; yield return GroupMemoryBarrierWithGroupSync();
      AppendBuff_grp0[grpI] = AppendBuff_grp[grpI]; yield return GroupMemoryBarrierWithGroupSync();
    }
    if (i < AppendBuff_BitN1) AppendBuff_Fills1[i] = AppendBuff_grp[grpI];
  }
  public virtual IEnumerator AppendBuff_GetFills2_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI)
  {
    uint i = id.x, k = i + 1, j = k * numthreads1 - 1, c = i < AppendBuff_BitN2 - 1 ? AppendBuff_Fills1[j] : i < AppendBuff_BitN2 ? AppendBuff_Fills1[AppendBuff_BitN1 - 1] : 0, s;
    AppendBuff_grp0[grpI] = c; AppendBuff_grp[grpI] = c; yield return GroupMemoryBarrierWithGroupSync();
    for (s = 1; s < numthreads1; s *= 2)
    {
      if (grpI >= s && i < AppendBuff_BitN2) AppendBuff_grp[grpI] = AppendBuff_grp0[grpI] + AppendBuff_grp0[grpI - s]; yield return GroupMemoryBarrierWithGroupSync();
      AppendBuff_grp0[grpI] = AppendBuff_grp[grpI]; yield return GroupMemoryBarrierWithGroupSync();
    }
    if (i < AppendBuff_BitN2) AppendBuff_Fills2[i] = AppendBuff_grp[grpI];
  }
  public virtual void AppendBuff_IncFills1_GS(uint3 id) { uint i = id.x; if (i >= numthreads1) AppendBuff_Fills1[i] += AppendBuff_Fills2[i / numthreads1 - 1]; }
  public virtual void AppendBuff_IncSums_GS(uint3 id) { uint i = id.x; if (i >= numthreads1) AppendBuff_Sums[i] += AppendBuff_Fills1[i / numthreads1 - 1]; if (i == AppendBuff_BitN - 1) AppendBuff_IndexN = AppendBuff_Sums[i]; }
  public virtual void AppendBuff_GetIndexes_GS(uint3 id) { uint i = id.x, j, sum = i == 0 ? 0 : AppendBuff_Sums[i - 1], b, i32 = i << 5, k; for (k = 0, b = AppendBuff_Bits[i]; b > 0; k++) { j = (uint)findLSB(b); AppendBuff_Indexes[sum + k] = i32 + j; b = SetBitu(b, j, 0); } }

  public virtual void base_AppendBuff_Start0_GS() { }
  public virtual void base_AppendBuff_Start1_GS() { }
  public virtual void base_AppendBuff_OnApplicationQuit() { Save_UI(); OnApplicationQuit_GS(); base.OnApplicationQuit(); already_quited = true; }
  public virtual void base_AppendBuff_LateUpdate0_GS() { }
  public virtual void base_AppendBuff_LateUpdate1_GS() { }
  public virtual void base_AppendBuff_Update0_GS() { }
  public virtual void base_AppendBuff_Update1_GS() { }
  public virtual void base_AppendBuff_OnValueChanged_GS() { }
  public virtual void base_AppendBuff_InitBuffers0_GS() { }
  public virtual void base_AppendBuff_InitBuffers1_GS() { }
  [HideInInspector]
  public virtual void base_AppendBuff_onRenderObject_GS(ref bool render, ref bool cpu) { }
  public virtual float4 base_frag_AppendBuff_GS(v2f i, float4 color)
  {
    uint libI = roundu(i.tj.x);
    return color;
  }
  public virtual void AppendBuff_InitBuffers0_GS() { }
  public virtual void AppendBuff_InitBuffers1_GS() { }
  public virtual void AppendBuff_LateUpdate0_GS() { }
  public virtual void AppendBuff_LateUpdate1_GS() { }
  public virtual void AppendBuff_Update0_GS() { }
  public virtual void AppendBuff_Update1_GS() { }
  public virtual void AppendBuff_Start0_GS() { }
  public virtual void AppendBuff_Start1_GS() { }
  public virtual void AppendBuff_OnValueChanged_GS() { }
  public virtual void AppendBuff_OnApplicationQuit_GS() { }
  public virtual void AppendBuff_onRenderObject_GS(ref bool render, ref bool cpu) { }
  public virtual float4 frag_AppendBuff_GS(v2f i, float4 color)
  {
    return color;
  }
  #endregion <AppendBuff>
}