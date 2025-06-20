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
public class gsFunctional_ : GS
{
  [HideInInspector] public uiData data;
  [HideInInspector] public GFunctional g; public GFunctional G { get { gFunctional.GetData(); return gFunctional[0]; } }
  public void g_SetData() { if (gChanged && gFunctional != null) { gFunctional[0] = g; gFunctional.SetData(); gChanged = false; } }
  public virtual void ints_SetKernels(bool reallocate = false) { if (ints != null && (reallocate || ints.reallocated)) { SetKernelValues(ints, nameof(ints), kernel_Calc_Min_Max_Sum, kernel_Init_ints); ints.reallocated = false; } }
  public virtual void Numbers_SetKernels(bool reallocate = false) { if (Numbers != null && (reallocate || Numbers.reallocated)) { SetKernelValues(Numbers, nameof(Numbers), kernel_Calc_Min_Max_Sum, kernel_Init_Lists); Numbers.reallocated = false; } }
  public virtual void B_Ints_SetKernels(bool reallocate = false) { if (B_Ints != null && (reallocate || B_Ints.reallocated)) { SetKernelValues(B_Ints, nameof(B_Ints), kernel_RunBillion); B_Ints.reallocated = false; } }
  public virtual void intNums_SetKernels(bool reallocate = false) { if (intNums != null && (reallocate || intNums.reallocated)) { SetKernelValues(intNums, nameof(intNums), kernel_Calc_Max, kernel_Init_intNums); intNums.reallocated = false; } }
  public virtual void MaxInt_SetKernels(bool reallocate = false) { if (MaxInt != null && (reallocate || MaxInt.reallocated)) { SetKernelValues(MaxInt, nameof(MaxInt), kernel_Calc_Max, kernel_Init_MaxInt); MaxInt.reallocated = false; } }
  public virtual void Mat_A_SetKernels(bool reallocate = false) { if (Mat_A != null && (reallocate || Mat_A.reallocated)) { SetKernelValues(Mat_A, nameof(Mat_A), kernel_Multiply_Mat_A_x, kernel_Init_Mat_A); Mat_A.reallocated = false; } }
  public virtual void Mat_x_SetKernels(bool reallocate = false) { if (Mat_x != null && (reallocate || Mat_x.reallocated)) { SetKernelValues(Mat_x, nameof(Mat_x), kernel_Multiply_Mat_A_x, kernel_Init_Mat_b, kernel_Init_Mat_x); Mat_x.reallocated = false; } }
  public virtual void Mat_b_SetKernels(bool reallocate = false) { if (Mat_b != null && (reallocate || Mat_b.reallocated)) { SetKernelValues(Mat_b, nameof(Mat_b), kernel_Multiply_Mat_A_x); Mat_b.reallocated = false; } }
  public virtual void Gpu_Calc_Max() { g_SetData(); intNums?.SetCpu(); intNums_SetKernels(); MaxInt?.SetCpu(); MaxInt_SetKernels(); Gpu(kernel_Calc_Max, Calc_Max, NumberN); MaxInt?.ResetWrite(); }
  public virtual void Cpu_Calc_Max() { intNums?.GetGpu(); MaxInt?.GetGpu(); Cpu(Calc_Max, NumberN); MaxInt.SetData(); }
  public virtual void Cpu_Calc_Max(uint3 id) { intNums?.GetGpu(); MaxInt?.GetGpu(); Calc_Max(id); MaxInt.SetData(); }
  public virtual void Gpu_Init_intNums() { g_SetData(); intNums_SetKernels(); Gpu(kernel_Init_intNums, Init_intNums, NumberN); intNums?.ResetWrite(); }
  public virtual void Cpu_Init_intNums() { intNums?.GetGpu(); Cpu(Init_intNums, NumberN); intNums.SetData(); }
  public virtual void Cpu_Init_intNums(uint3 id) { intNums?.GetGpu(); Init_intNums(id); intNums.SetData(); }
  public virtual void Gpu_Init_MaxInt() { g_SetData(); MaxInt_SetKernels(); Gpu(kernel_Init_MaxInt, Init_MaxInt, 1); MaxInt?.ResetWrite(); }
  public virtual void Cpu_Init_MaxInt() { MaxInt?.GetGpu(); Cpu(Init_MaxInt, 1); MaxInt.SetData(); }
  public virtual void Cpu_Init_MaxInt(uint3 id) { MaxInt?.GetGpu(); Init_MaxInt(id); MaxInt.SetData(); }
  public virtual void Gpu_Calc_Min_Max_Sum() { g_SetData(); ints?.SetCpu(); ints_SetKernels(); Numbers?.SetCpu(); Numbers_SetKernels(); Gpu(kernel_Calc_Min_Max_Sum, Calc_Min_Max_Sum, uint3(ListN, NumberN, Ints.N)); ints?.ResetWrite(); }
  public virtual void Cpu_Calc_Min_Max_Sum() { ints?.GetGpu(); Numbers?.GetGpu(); Cpu(Calc_Min_Max_Sum, uint3(ListN, NumberN, Ints.N)); ints.SetData(); }
  public virtual void Cpu_Calc_Min_Max_Sum(uint3 id) { ints?.GetGpu(); Numbers?.GetGpu(); Calc_Min_Max_Sum(id); ints.SetData(); }
  public virtual void Gpu_Init_Lists() { g_SetData(); Numbers_SetKernels(); Gpu(kernel_Init_Lists, Init_Lists, uint2(ListN, NumberN)); Numbers?.ResetWrite(); }
  public virtual void Cpu_Init_Lists() { Numbers?.GetGpu(); Cpu(Init_Lists, uint2(ListN, NumberN)); Numbers.SetData(); }
  public virtual void Cpu_Init_Lists(uint3 id) { Numbers?.GetGpu(); Init_Lists(id); Numbers.SetData(); }
  public virtual void Gpu_Init_ints() { g_SetData(); ints_SetKernels(); Gpu(kernel_Init_ints, Init_ints, uint2(ListN, Ints.N)); ints?.ResetWrite(); }
  public virtual void Cpu_Init_ints() { ints?.GetGpu(); Cpu(Init_ints, uint2(ListN, Ints.N)); ints.SetData(); }
  public virtual void Cpu_Init_ints(uint3 id) { ints?.GetGpu(); Init_ints(id); ints.SetData(); }
  public virtual void Gpu_RunBillion() { g_SetData(); B_Ints?.SetCpu(); B_Ints_SetKernels(); Gpu(kernel_RunBillion, RunBillion, 1000); B_Ints?.ResetWrite(); }
  public virtual void Cpu_RunBillion() { B_Ints?.GetGpu(); Cpu(RunBillion, 1000); B_Ints.SetData(); }
  public virtual void Cpu_RunBillion(uint3 id) { B_Ints?.GetGpu(); RunBillion(id); B_Ints.SetData(); }
  public virtual void Gpu_Multiply_Mat_A_x() { g_SetData(); Mat_b?.SetCpu(); Mat_b_SetKernels(); Mat_A?.SetCpu(); Mat_A_SetKernels(); Mat_x?.SetCpu(); Mat_x_SetKernels(); Gpu(kernel_Multiply_Mat_A_x, Multiply_Mat_A_x, uint3(MatN, MatRowN, MatRowN)); Mat_b?.ResetWrite(); }
  public virtual void Cpu_Multiply_Mat_A_x() { Mat_b?.GetGpu(); Mat_A?.GetGpu(); Mat_x?.GetGpu(); Cpu(Multiply_Mat_A_x, uint3(MatN, MatRowN, MatRowN)); Mat_b.SetData(); }
  public virtual void Cpu_Multiply_Mat_A_x(uint3 id) { Mat_b?.GetGpu(); Mat_A?.GetGpu(); Mat_x?.GetGpu(); Multiply_Mat_A_x(id); Mat_b.SetData(); }
  public virtual void Gpu_Init_Mat_b() { g_SetData(); Mat_x_SetKernels(); Gpu(kernel_Init_Mat_b, Init_Mat_b, uint2(MatN, MatRowN)); Mat_x?.ResetWrite(); }
  public virtual void Cpu_Init_Mat_b() { Mat_x?.GetGpu(); Cpu(Init_Mat_b, uint2(MatN, MatRowN)); Mat_x.SetData(); }
  public virtual void Cpu_Init_Mat_b(uint3 id) { Mat_x?.GetGpu(); Init_Mat_b(id); Mat_x.SetData(); }
  public virtual void Gpu_Init_Mat_x() { g_SetData(); Mat_x_SetKernels(); Gpu(kernel_Init_Mat_x, Init_Mat_x, uint2(MatN, MatRowN)); Mat_x?.ResetWrite(); }
  public virtual void Cpu_Init_Mat_x() { Mat_x?.GetGpu(); Cpu(Init_Mat_x, uint2(MatN, MatRowN)); Mat_x.SetData(); }
  public virtual void Cpu_Init_Mat_x(uint3 id) { Mat_x?.GetGpu(); Init_Mat_x(id); Mat_x.SetData(); }
  public virtual void Gpu_Init_Mat_A() { g_SetData(); Mat_A_SetKernels(); Gpu(kernel_Init_Mat_A, Init_Mat_A, uint3(MatN, MatRowN, MatRowN)); Mat_A?.ResetWrite(); }
  public virtual void Cpu_Init_Mat_A() { Mat_A?.GetGpu(); Cpu(Init_Mat_A, uint3(MatN, MatRowN, MatRowN)); Mat_A.SetData(); }
  public virtual void Cpu_Init_Mat_A(uint3 id) { Mat_A?.GetGpu(); Init_Mat_A(id); Mat_A.SetData(); }
  [JsonConverter(typeof(StringEnumConverter))] public enum Ints : uint { Min, Max, Sum, N }
  public const uint Ints_Min = 0, Ints_Max = 1, Ints_Sum = 2, Ints_N = 3;
  [HideInInspector] public bool ValuesChanged, gChanged;
  public static gsFunctional This;
  public virtual void Awake() { This = this as gsFunctional; Awake_GS(); }
  public virtual void Awake_GS()
  {
  }
  public virtual void Start() { Build_UI(); Load_UI(); Start0_GS(); InitBuffers(); Start1_GS(); }
  public virtual void Start0_GS() { }
  public virtual void Start1_GS() { }
  public override void onLoaded()
  {
    base.onLoaded();
  }
  [HideInInspector] public bool already_quited = false;
  public override void OnApplicationQuit() { Save_UI(); OnApplicationQuit_GS(); base.OnApplicationQuit(); already_quited = true; }
  public virtual void OnApplicationQuit_GS() { }
  public override void ui_to_data()
  {
    if (data == null) return;
    data.siUnits = siUnits;
    data.group_UI = group_UI;
    data.group_WindowsUpdate = group_WindowsUpdate;
    data.Names = Names;
    data.group_Units = group_Units;
    data.units_length = units_length;
    data.units_width = units_width;
    data.group_Billion = group_Billion;
    data.group_MinMaxSum = group_MinMaxSum;
    data.ListN = ListN;
    data.NumberN = NumberN;
    data.group_MatMultiply = group_MatMultiply;
    data.MatN = MatN;
    data.MatRowN = MatRowN;
    data.group_Functional = group_Functional;
    data.group_Exceptional = group_Exceptional;
    data.group_Screenshot = group_Screenshot;
  }
  public override void data_to_ui()
  {
    if (data == null) return;
    group_UI = data.group_UI;
    group_WindowsUpdate = data.group_WindowsUpdate;
    Names = data.Names;
    group_Units = data.group_Units;
    units_length = ui_txt_str.Contains("\"units_length\"") ? data.units_length : 5f;
    units_width = ui_txt_str.Contains("\"units_width\"") ? data.units_width : 5f;
    group_Billion = data.group_Billion;
    group_MinMaxSum = data.group_MinMaxSum;
    ListN = ui_txt_str.Contains("\"ListN\"") ? data.ListN : 1000;
    NumberN = ui_txt_str.Contains("\"NumberN\"") ? data.NumberN : 1000;
    group_MatMultiply = data.group_MatMultiply;
    MatN = ui_txt_str.Contains("\"MatN\"") ? data.MatN : 2048;
    MatRowN = ui_txt_str.Contains("\"MatRowN\"") ? data.MatRowN : 2048;
    group_Functional = data.group_Functional;
    group_Exceptional = data.group_Exceptional;
    group_Screenshot = data.group_Screenshot;
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
    if (UI_Names.Changed || Names != UI_Names.v) { data.Names = UI_Names.v; ValuesChanged = gChanged = true; }
    if (UI_units_length.Changed || units_length != UI_units_length.v) units_length = UI_units_length.v;
    if (UI_units_width.Changed || units_width != UI_units_width.v) units_width = UI_units_width.v;
    if (UI_ListN.Changed || ListN != UI_ListN.v) ListN = UI_ListN.v;
    if (UI_NumberN.Changed || NumberN != UI_NumberN.v) NumberN = UI_NumberN.v;
    if (UI_MatN.Changed || MatN != UI_MatN.v) MatN = UI_MatN.v;
    if (UI_MatRowN.Changed || MatRowN != UI_MatRowN.v) MatRowN = UI_MatRowN.v;
    if (GetKeyDown(Ctrl, 'w')) RunScreenshot();
    if (ValuesChanged) { gChanged = true; OnValueChanged(); ValuesChanged = UI_group_UI.Changed = UI_group_WindowsUpdate.Changed = UI_Names.Changed = UI_group_Units.Changed = UI_units_length.Changed = UI_units_width.Changed = UI_group_Billion.Changed = UI_group_MinMaxSum.Changed = UI_ListN.Changed = UI_NumberN.Changed = UI_group_MatMultiply.Changed = UI_MatN.Changed = UI_MatRowN.Changed = UI_group_Functional.Changed = UI_group_Exceptional.Changed = UI_group_Screenshot.Changed = false; }
    LateUpdate1_GS();
    if(lateUpdateI++ == uint_max) lateUpdateI = 100;
  }
  public virtual void LateUpdate0_GS() { }
  public virtual void LateUpdate1_GS() { }
  public override void Update()
  {
    base.Update();
    if (!ui_loaded) return;
    Update0_GS();
    Update1_GS();
  }
  public virtual void Update0_GS() { }
  public virtual void Update1_GS() { }
  public override void OnValueChanged()
  {
    if (!ui_loaded) return;
    OnValueChanged_GS();
  }
  public override void OnValueChanged_GS()
  {
    var type = "gsReport_Lib".ToType();
    if (type != null) ((GS)GetComponent(type))?.OnValueChanged_GS();

  }
  public string[] Scenes_in_Build => new string[] {  };
  public virtual float units_length { get => g.units_length; set { if (any(g.units_length != value) || any(UI_units_length.v != value)) { g.units_length = UI_units_length.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float units_width { get => g.units_width; set { if (any(g.units_width != value) || any(UI_units_width.v != value)) { g.units_width = UI_units_width.v = value; ValuesChanged = gChanged = true; } } }
  public virtual uint BillionN { get => g.BillionN; set { if (g.BillionN != value) { g.BillionN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint ListN { get => g.ListN; set { if (g.ListN != value || UI_ListN.v != value) { g.ListN = UI_ListN.v = value; ValuesChanged = gChanged = true; } } }
  public virtual uint NumberN { get => g.NumberN; set { if (g.NumberN != value || UI_NumberN.v != value) { g.NumberN = UI_NumberN.v = value; ValuesChanged = gChanged = true; } } }
  public virtual uint MatN { get => g.MatN; set { if (g.MatN != value || UI_MatN.v != value) { g.MatN = UI_MatN.v = value; ValuesChanged = gChanged = true; } } }
  public virtual uint MatRowN { get => g.MatRowN; set { if (g.MatRowN != value || UI_MatRowN.v != value) { g.MatRowN = UI_MatRowN.v = value; ValuesChanged = gChanged = true; } } }
  public bool group_UI { get => UI_group_UI?.v ?? false; set { if (UI_group_UI != null) UI_group_UI.v = value; } }
  public bool group_WindowsUpdate { get => UI_group_WindowsUpdate?.v ?? false; set { if (UI_group_WindowsUpdate != null) UI_group_WindowsUpdate.v = value; } }
  public bool group_Units { get => UI_group_Units?.v ?? false; set { if (UI_group_Units != null) UI_group_Units.v = value; } }
  public bool group_Billion { get => UI_group_Billion?.v ?? false; set { if (UI_group_Billion != null) UI_group_Billion.v = value; } }
  public bool group_MinMaxSum { get => UI_group_MinMaxSum?.v ?? false; set { if (UI_group_MinMaxSum != null) UI_group_MinMaxSum.v = value; } }
  public bool group_MatMultiply { get => UI_group_MatMultiply?.v ?? false; set { if (UI_group_MatMultiply != null) UI_group_MatMultiply.v = value; } }
  public bool group_Functional { get => UI_group_Functional?.v ?? false; set { if (UI_group_Functional != null) UI_group_Functional.v = value; } }
  public bool group_Exceptional { get => UI_group_Exceptional?.v ?? false; set { if (UI_group_Exceptional != null) UI_group_Exceptional.v = value; } }
  public bool group_Screenshot { get => UI_group_Screenshot?.v ?? false; set { if (UI_group_Screenshot != null) UI_group_Screenshot.v = value; } }
  public UI_TreeGroup UI_group_UI, UI_group_WindowsUpdate, UI_group_Units, UI_group_Billion, UI_group_MinMaxSum, UI_group_MatMultiply, UI_group_Functional, UI_group_Exceptional, UI_group_Screenshot;
  public UI_strings UI_Names;
  public string Names { get => UI_Names?.v ?? ""; set { if (UI_Names != null && data != null) UI_Names.v = data.Names = value; } }
  public UI_method UI_StopWindowsUpdate;
  [HideInInspector] public bool in_StopWindowsUpdate = false; public IEnumerator StopWindowsUpdate() { if (in_StopWindowsUpdate) { in_StopWindowsUpdate = false; yield break; } in_StopWindowsUpdate = true; yield return StartCoroutine(StopWindowsUpdate_Sync()); in_StopWindowsUpdate = false; }
  public virtual IEnumerator StopWindowsUpdate_Sync() { yield return null; }
  public UI_float UI_units_length, UI_units_width;
  public UI_method UI_BillionLoop;
  public virtual void BillionLoop() { }
  public UI_uint UI_ListN, UI_NumberN, UI_MatN, UI_MatRowN;
  public UI_method UI_MinMaxSum;
  public virtual void MinMaxSum() { }
  public UI_method UI_LinqMax;
  public virtual void LinqMax() { }
  public UI_method UI_MatMultiply;
  public virtual void MatMultiply() { }
  public UI_method UI_RunPerson;
  public virtual void RunPerson() { }
  public UI_method UI_RunTest;
  public virtual void RunTest() { }
  public UI_method UI_TestCoroutine;
  [HideInInspector] public bool in_TestCoroutine = false; public IEnumerator TestCoroutine() { if (in_TestCoroutine) { in_TestCoroutine = false; yield break; } in_TestCoroutine = true; yield return StartCoroutine(TestCoroutine_Sync()); in_TestCoroutine = false; }
  public virtual IEnumerator TestCoroutine_Sync() { yield return null; }
  public UI_method UI_RunExceptional;
  public virtual void RunExceptional() { }
  public UI_method UI_RunScreenshot;
  public virtual void RunScreenshot() { }
  public UI_TreeGroup ui_group_UI => UI_group_UI;
  public UI_TreeGroup ui_group_WindowsUpdate => UI_group_WindowsUpdate;
  public UI_strings ui_Names => UI_Names;
  public UI_TreeGroup ui_group_Units => UI_group_Units;
  public UI_float ui_units_length => UI_units_length;
  public UI_float ui_units_width => UI_units_width;
  public UI_TreeGroup ui_group_Billion => UI_group_Billion;
  public UI_TreeGroup ui_group_MinMaxSum => UI_group_MinMaxSum;
  public UI_uint ui_ListN => UI_ListN;
  public UI_uint ui_NumberN => UI_NumberN;
  public UI_TreeGroup ui_group_MatMultiply => UI_group_MatMultiply;
  public UI_uint ui_MatN => UI_MatN;
  public UI_uint ui_MatRowN => UI_MatRowN;
  public UI_TreeGroup ui_group_Functional => UI_group_Functional;
  public UI_TreeGroup ui_group_Exceptional => UI_group_Exceptional;
  public UI_TreeGroup ui_group_Screenshot => UI_group_Screenshot;
  [Serializable]
  public class uiData
  {
    public bool siUnits, group_UI, group_WindowsUpdate, group_Units, group_Billion, group_MinMaxSum, group_MatMultiply, group_Functional, group_Exceptional, group_Screenshot;
    public string Names;
    public float units_length, units_width;
    public uint ListN, NumberN, MatN, MatRowN;
  }
  public virtual void InitBuffers()
  {
    InitBuffers0_GS();
    AllocData_gFunctional(1);
    InitKernels();
    SetKernelValues(gFunctional, nameof(gFunctional), kernel_Calc_Max, kernel_Init_intNums, kernel_Init_MaxInt, kernel_Calc_Min_Max_Sum, kernel_Init_Lists, kernel_Init_ints, kernel_RunBillion, kernel_Multiply_Mat_A_x, kernel_Init_Mat_b, kernel_Init_Mat_x, kernel_Init_Mat_A);
    AllocData_ints(ListN * Ints_N);
    AllocData_Numbers(ListN * NumberN);
    InitBuffers1_GS();
    isInitBuffers = true;
  }
  public virtual void InitBuffers0_GS() { }
  public virtual void InitBuffers1_GS() { }
  [Serializable]
  public struct GFunctional
  {
    public float units_length, units_width;
    public uint BillionN, ListN, NumberN, MatN, MatRowN;
  };
  public RWStructuredBuffer<GFunctional> gFunctional;
  public RWStructuredBuffer<int> ints, intNums, MaxInt, Mat_b;
  public RWStructuredBuffer<float> Numbers, Mat_A, Mat_x;
  public RWStructuredBuffer<uint> B_Ints;
  public virtual void AllocData_gFunctional(uint n) => AddComputeBuffer(ref gFunctional, nameof(gFunctional), n);
  public virtual void AllocData_ints(uint n) => AddComputeBuffer(ref ints, nameof(ints), n);
  public virtual void AssignData_ints(params int[] data) => AddComputeBufferData(ref ints, nameof(ints), data);
  public virtual void AllocData_Numbers(uint n) => AddComputeBuffer(ref Numbers, nameof(Numbers), n);
  public virtual void AssignData_Numbers(params float[] data) => AddComputeBufferData(ref Numbers, nameof(Numbers), data);
  public virtual void AllocData_B_Ints(uint n) => AddComputeBuffer(ref B_Ints, nameof(B_Ints), n);
  public virtual void AssignData_B_Ints(params uint[] data) => AddComputeBufferData(ref B_Ints, nameof(B_Ints), data);
  public virtual void AllocData_intNums(uint n) => AddComputeBuffer(ref intNums, nameof(intNums), n);
  public virtual void AssignData_intNums(params int[] data) => AddComputeBufferData(ref intNums, nameof(intNums), data);
  public virtual void AllocData_MaxInt(uint n) => AddComputeBuffer(ref MaxInt, nameof(MaxInt), n);
  public virtual void AssignData_MaxInt(params int[] data) => AddComputeBufferData(ref MaxInt, nameof(MaxInt), data);
  public virtual void AllocData_Mat_A(uint n) => AddComputeBuffer(ref Mat_A, nameof(Mat_A), n);
  public virtual void AssignData_Mat_A(params float[] data) => AddComputeBufferData(ref Mat_A, nameof(Mat_A), data);
  public virtual void AllocData_Mat_x(uint n) => AddComputeBuffer(ref Mat_x, nameof(Mat_x), n);
  public virtual void AssignData_Mat_x(params float[] data) => AddComputeBufferData(ref Mat_x, nameof(Mat_x), data);
  public virtual void AllocData_Mat_b(uint n) => AddComputeBuffer(ref Mat_b, nameof(Mat_b), n);
  public virtual void AssignData_Mat_b(params int[] data) => AddComputeBufferData(ref Mat_b, nameof(Mat_b), data);
  public Texture2D _PaletteTex;
  public float4 palette(float v) => paletteColor(_PaletteTex, v);
  public float4 palette(float v, float w) => float4(palette(v).xyz, w);

  public virtual void onRenderObject_LIN(bool show, uint _itemN, ref uint i, ref uint index, ref uint3 LIN) { uint n = 0; if (show) { if (i < (n = _itemN)) LIN = uint3(index, i, 0); LIN.z += n; i -= n; } index++; }
  public virtual void onRenderObject_LIN(uint _itemN, ref uint i, ref uint index, ref uint3 LIN) { onRenderObject_LIN(true, _itemN, ref i, ref index, ref LIN); }
  public virtual uint3 onRenderObject_LIN(uint i) { uint3 LIN = u000; return LIN; }
  public virtual uint3 onRenderObject_LIN_Quads(uint i) { uint3 LIN = u000; return LIN; }
  public virtual uint3 onRenderObject_LIN_Points(uint i) { uint3 LIN = u000; return LIN; }
  [HideInInspector] public int kernel_Calc_Max; [numthreads(numthreads1, 1, 1)] protected void Calc_Max(uint3 id) { unchecked { if (id.x < NumberN) Calc_Max_GS(id); } }
  public virtual void Calc_Max_GS(uint3 id) { }
  [HideInInspector] public int kernel_Init_intNums; [numthreads(numthreads1, 1, 1)] protected void Init_intNums(uint3 id) { unchecked { if (id.x < NumberN) Init_intNums_GS(id); } }
  public virtual void Init_intNums_GS(uint3 id) { }
  [HideInInspector] public int kernel_Init_MaxInt; [numthreads(numthreads1, 1, 1)] protected void Init_MaxInt(uint3 id) { unchecked { if (id.x < 1) Init_MaxInt_GS(id); } }
  public virtual void Init_MaxInt_GS(uint3 id) { }
  [HideInInspector] public int kernel_Calc_Min_Max_Sum; [numthreads(numthreads3, numthreads3, numthreads3)] protected void Calc_Min_Max_Sum(uint3 id) { unchecked { if (id.z < Ints_N && id.y < NumberN && id.x < ListN) Calc_Min_Max_Sum_GS(id); } }
  public virtual void Calc_Min_Max_Sum_GS(uint3 id) { }
  [HideInInspector] public int kernel_Init_Lists; [numthreads(numthreads2, numthreads2, 1)] protected void Init_Lists(uint3 id) { unchecked { if (id.y < NumberN && id.x < ListN) Init_Lists_GS(id); } }
  public virtual void Init_Lists_GS(uint3 id) { }
  [HideInInspector] public int kernel_Init_ints; [numthreads(numthreads2, numthreads2, 1)] protected void Init_ints(uint3 id) { unchecked { if (id.y < Ints_N && id.x < ListN) Init_ints_GS(id); } }
  public virtual void Init_ints_GS(uint3 id) { }
  [HideInInspector] public int kernel_RunBillion; [numthreads(numthreads1, 1, 1)] protected void RunBillion(uint3 id) { unchecked { if (id.x < 1000) RunBillion_GS(id); } }
  public virtual void RunBillion_GS(uint3 id) { }
  [HideInInspector] public int kernel_Multiply_Mat_A_x; [numthreads(numthreads3, numthreads3, numthreads3)] protected void Multiply_Mat_A_x(uint3 id) { unchecked { if (id.z < MatRowN && id.y < MatRowN && id.x < MatN) Multiply_Mat_A_x_GS(id); } }
  public virtual void Multiply_Mat_A_x_GS(uint3 id) { }
  [HideInInspector] public int kernel_Init_Mat_b; [numthreads(numthreads2, numthreads2, 1)] protected void Init_Mat_b(uint3 id) { unchecked { if (id.y < MatRowN && id.x < MatN) Init_Mat_b_GS(id); } }
  public virtual void Init_Mat_b_GS(uint3 id) { }
  [HideInInspector] public int kernel_Init_Mat_x; [numthreads(numthreads2, numthreads2, 1)] protected void Init_Mat_x(uint3 id) { unchecked { if (id.y < MatRowN && id.x < MatN) Init_Mat_x_GS(id); } }
  public virtual void Init_Mat_x_GS(uint3 id) { }
  [HideInInspector] public int kernel_Init_Mat_A; [numthreads(numthreads3, numthreads3, numthreads3)] protected void Init_Mat_A(uint3 id) { unchecked { if (id.z < MatRowN && id.y < MatRowN && id.x < MatN) Init_Mat_A_GS(id); } }
  public virtual void Init_Mat_A_GS(uint3 id) { }
  public virtual v2f vert_GS(uint i, uint j, v2f o)
  {
    return o;
  }
  public virtual v2f vert(uint i, uint j)
  {
    v2f o = default;
    o.pos = o.color = o.tj = o.tk = f0000; o.normal = o.p0 = o.p1 = o.wPos = f000; o.uv = f00; o.ti = float4(i, j, 0, 0);
    return vert_GS(i, j, o);
  }
  public virtual void onRenderObject_GS(ref bool render, ref bool cpu) { }
  public virtual float4 frag_GS(v2f i, float4 color) => color;
  public virtual float4 frag(v2f i)
  {
    float4 color = i.color;
    color = frag_GS(i, color);
    if (color.a == 0) Discard(0);
    return color;
  }
}