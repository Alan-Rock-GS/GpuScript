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
public class gsFR_Smart_ : GS
{
  [HideInInspector] public uiData data;
  [HideInInspector] public GFR_Smart g; public GFR_Smart G { get { gFR_Smart.GetData(); return gFR_Smart[0]; } }
  public void g_SetData() { if (gChanged && gFR_Smart != null) { gFR_Smart[0] = g; gFR_Smart.SetData(); gChanged = false; } }
  public virtual void a_SetKernels(bool reallocate = false) { if (a != null && (reallocate || a.reallocated)) { SetKernelValues(a, nameof(a)); a.reallocated = false; } }
  public virtual void X_SetKernels(bool reallocate = false) { if (X != null && (reallocate || X.reallocated)) { SetKernelValues(X, nameof(X)); X.reallocated = false; } }
  public virtual void Y_SetKernels(bool reallocate = false) { if (Y != null && (reallocate || Y.reallocated)) { SetKernelValues(Y, nameof(Y)); Y.reallocated = false; } }
  public virtual void b_SetKernels(bool reallocate = false) { if (b != null && (reallocate || b.reallocated)) { SetKernelValues(b, nameof(b)); b.reallocated = false; } }
  public virtual void row_maxAbs_SetKernels(bool reallocate = false) { if (row_maxAbs != null && (reallocate || row_maxAbs.reallocated)) { SetKernelValues(row_maxAbs, nameof(row_maxAbs)); row_maxAbs.reallocated = false; } }
  public virtual void ia_SetKernels(bool reallocate = false) { if (ia != null && (reallocate || ia.reallocated)) { SetKernelValues(ia, nameof(ia)); ia.reallocated = false; } }
  public virtual void sums_SetKernels(bool reallocate = false) { if (sums != null && (reallocate || sums.reallocated)) { SetKernelValues(sums, nameof(sums)); sums.reallocated = false; } }
  public virtual void P_SetKernels(bool reallocate = false) { if (P != null && (reallocate || P.reallocated)) { SetKernelValues(P, nameof(P)); P.reallocated = false; } }
  public virtual void uints_SetKernels(bool reallocate = false) { if (uints != null && (reallocate || uints.reallocated)) { SetKernelValues(uints, nameof(uints)); uints.reallocated = false; } }
  public virtual void Gpu_A_times_B_to_X() { g_SetData(); Gpu(kernel_A_times_B_to_X, A_times_B_to_X, N); }
  public virtual void Cpu_A_times_B_to_X() { Cpu(A_times_B_to_X, N); }
  public virtual void Cpu_A_times_B_to_X(uint3 id) { A_times_B_to_X(id); }
  public virtual void Gpu_Zero_B() { g_SetData(); Gpu(kernel_Zero_B, Zero_B, N); }
  public virtual void Cpu_Zero_B() { Cpu(Zero_B, N); }
  public virtual void Cpu_Zero_B(uint3 id) { Zero_B(id); }
  public virtual void Gpu_Interlock_SolveXfromUXequalY() { g_SetData(); Gpu(kernel_Interlock_SolveXfromUXequalY, Interlock_SolveXfromUXequalY, 1); }
  public virtual void Cpu_Interlock_SolveXfromUXequalY() { Cpu(Interlock_SolveXfromUXequalY, 1); }
  public virtual void Cpu_Interlock_SolveXfromUXequalY(uint3 id) { Interlock_SolveXfromUXequalY(id); }
  public virtual void Gpu_Interlock_SolveYfromLYequalB() { g_SetData(); Gpu(kernel_Interlock_SolveYfromLYequalB, Interlock_SolveYfromLYequalB, 1); }
  public virtual void Cpu_Interlock_SolveYfromLYequalB() { Cpu(Interlock_SolveYfromLYequalB, 1); }
  public virtual void Cpu_Interlock_SolveYfromLYequalB(uint3 id) { Interlock_SolveYfromLYequalB(id); }
  public virtual void Gpu_Interlock_Sum_SolveXfromUXequalY() { g_SetData(); Gpu(kernel_Interlock_Sum_SolveXfromUXequalY, Interlock_Sum_SolveXfromUXequalY, N); }
  public virtual void Cpu_Interlock_Sum_SolveXfromUXequalY() { Cpu(Interlock_Sum_SolveXfromUXequalY, N); }
  public virtual void Cpu_Interlock_Sum_SolveXfromUXequalY(uint3 id) { Interlock_Sum_SolveXfromUXequalY(id); }
  public virtual void Gpu_Interlock_Sum_SolveYfromLYequalB() { g_SetData(); Gpu(kernel_Interlock_Sum_SolveYfromLYequalB, Interlock_Sum_SolveYfromLYequalB, N); }
  public virtual void Cpu_Interlock_Sum_SolveYfromLYequalB() { Cpu(Interlock_Sum_SolveYfromLYequalB, N); }
  public virtual void Cpu_Interlock_Sum_SolveYfromLYequalB(uint3 id) { Interlock_Sum_SolveYfromLYequalB(id); }
  public virtual void Gpu_SolveXfromUXequalY() { g_SetData(); Gpu(kernel_SolveXfromUXequalY, SolveXfromUXequalY, 1); }
  public virtual void Cpu_SolveXfromUXequalY() { Cpu(SolveXfromUXequalY, 1); }
  public virtual void Cpu_SolveXfromUXequalY(uint3 id) { SolveXfromUXequalY(id); }
  public virtual void Gpu_SolveYfromLYequalB() { g_SetData(); Gpu(kernel_SolveYfromLYequalB, SolveYfromLYequalB, 1); }
  public virtual void Cpu_SolveYfromLYequalB() { Cpu(SolveYfromLYequalB, 1); }
  public virtual void Cpu_SolveYfromLYequalB(uint3 id) { SolveYfromLYequalB(id); }
  public virtual void Gpu_Set_rowMultiplier() { g_SetData(); Gpu(kernel_Set_rowMultiplier, Set_rowMultiplier, 1); }
  public virtual void Cpu_Set_rowMultiplier() { Cpu(Set_rowMultiplier, 1); }
  public virtual void Cpu_Set_rowMultiplier(uint3 id) { Set_rowMultiplier(id); }
  public virtual void Gpu_MakeElementZeroAndFillWithLowerMatrixElement() { g_SetData(); Gpu(kernel_MakeElementZeroAndFillWithLowerMatrixElement, MakeElementZeroAndFillWithLowerMatrixElement, N); }
  public virtual void Cpu_MakeElementZeroAndFillWithLowerMatrixElement() { Cpu(MakeElementZeroAndFillWithLowerMatrixElement, N); }
  public virtual void Cpu_MakeElementZeroAndFillWithLowerMatrixElement(uint3 id) { MakeElementZeroAndFillWithLowerMatrixElement(id); }
  public virtual void Gpu_Find_rowMultiplier() { g_SetData(); Gpu(kernel_Find_rowMultiplier, Find_rowMultiplier, 1); }
  public virtual void Cpu_Find_rowMultiplier() { Cpu(Find_rowMultiplier, 1); }
  public virtual void Cpu_Find_rowMultiplier(uint3 id) { Find_rowMultiplier(id); }
  public virtual void Gpu_Divide_by_maxAbs() { g_SetData(); Gpu(kernel_Divide_by_maxAbs, Divide_by_maxAbs, N * N); }
  public virtual void Cpu_Divide_by_maxAbs() { Cpu(Divide_by_maxAbs, N * N); }
  public virtual void Cpu_Divide_by_maxAbs(uint3 id) { Divide_by_maxAbs(id); }
  public virtual void Gpu_MakeLowerTriangleZeroAndFillWithL() { g_SetData(); Gpu(kernel_MakeLowerTriangleZeroAndFillWithL, MakeLowerTriangleZeroAndFillWithL, N); }
  public virtual void Cpu_MakeLowerTriangleZeroAndFillWithL() { Cpu(MakeLowerTriangleZeroAndFillWithL, N); }
  public virtual void Cpu_MakeLowerTriangleZeroAndFillWithL(uint3 id) { MakeLowerTriangleZeroAndFillWithL(id); }
  public virtual void Gpu_Interlock_Find_maxElementRow() { g_SetData(); Gpu(kernel_Interlock_Find_maxElementRow, Interlock_Find_maxElementRow, N); }
  public virtual void Cpu_Interlock_Find_maxElementRow() { Cpu(Interlock_Find_maxElementRow, N); }
  public virtual void Cpu_Interlock_Find_maxElementRow(uint3 id) { Interlock_Find_maxElementRow(id); }
  public virtual void Gpu_Interlock_Find_maxElementVal() { g_SetData(); Gpu(kernel_Interlock_Find_maxElementVal, Interlock_Find_maxElementVal, N); }
  public virtual void Cpu_Interlock_Find_maxElementVal() { Cpu(Interlock_Find_maxElementVal, N); }
  public virtual void Cpu_Interlock_Find_maxElementVal(uint3 id) { Interlock_Find_maxElementVal(id); }
  public virtual void Gpu_Find_maxElementRow() { g_SetData(); Gpu(kernel_Find_maxElementRow, Find_maxElementRow, 1); }
  public virtual void Cpu_Find_maxElementRow() { Cpu(Find_maxElementRow, 1); }
  public virtual void Cpu_Find_maxElementRow(uint3 id) { Find_maxElementRow(id); }
  public virtual void Gpu_Find_maxAbs() { g_SetData(); Gpu(kernel_Find_maxAbs, Find_maxAbs, 1); }
  public virtual void Cpu_Find_maxAbs() { Cpu(Find_maxAbs, 1); }
  public virtual void Cpu_Find_maxAbs(uint3 id) { Find_maxAbs(id); }
  public virtual void Gpu_Find_row_maxAbs() { g_SetData(); Gpu(kernel_Find_row_maxAbs, Find_row_maxAbs, N); }
  public virtual void Cpu_Find_row_maxAbs() { Cpu(Find_row_maxAbs, N); }
  public virtual void Cpu_Find_row_maxAbs(uint3 id) { Find_row_maxAbs(id); }
  public virtual void Gpu_InitP() { g_SetData(); Gpu(kernel_InitP, InitP, N); }
  public virtual void Cpu_InitP() { Cpu(InitP, N); }
  public virtual void Cpu_InitP(uint3 id) { InitP(id); }
  [JsonConverter(typeof(StringEnumConverter))] public enum RunOn : uint { Gpu, Cpu }
  [JsonConverter(typeof(StringEnumConverter))] public enum UInts : uint { maxAbs, maxElementRow, maxElementVal, N }
  public const uint RunOn_Gpu = 0, RunOn_Cpu = 1;
  public const uint UInts_maxAbs = 0, UInts_maxElementRow = 1, UInts_maxElementVal = 2, UInts_N = 3;
  [HideInInspector] public bool ValuesChanged, gChanged;
  public static gsFR_Smart This;
  public virtual void Awake() { This = this as gsFR_Smart; Awake_GS(); }
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
    data.group_LU = group_LU;
    data.runOn = runOn;
    data.useInterlocked = useInterlocked;
    data.debug = debug;
    data.repeatN = repeatN;
    data.group_Report = group_Report;
    data.record_Report_Info = record_Report_Info;
    data.report_Info = report_Info;
  }
  public override void data_to_ui()
  {
    if (data == null) return;
    group_UI = data.group_UI;
    group_LU = data.group_LU;
    runOn = data.runOn;
    useInterlocked = data.useInterlocked;
    debug = data.debug;
    repeatN = ui_txt_str.Contains("\"repeatN\"") ? data.repeatN : 1;
    group_Report = data.group_Report;
    record_Report_Info = data.record_Report_Info;
    report_Info = data.report_Info;
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
    if (UI_runOn.Changed || (uint)runOn != UI_runOn.v) runOn = (RunOn)UI_runOn.v;
    if (UI_useInterlocked.Changed || useInterlocked != UI_useInterlocked.v) useInterlocked = UI_useInterlocked.v;
    if (UI_debug.Changed || debug != UI_debug.v) debug = UI_debug.v;
    if (UI_repeatN.Changed || repeatN != UI_repeatN.v) repeatN = UI_repeatN.v;
    if (UI_record_Report_Info.Changed || record_Report_Info != UI_record_Report_Info.v) record_Report_Info = UI_record_Report_Info.v;
    if (UI_report_Info.Changed || report_Info != UI_report_Info.v) { data.report_Info = UI_report_Info.v; ValuesChanged = gChanged = true; }
    if (ValuesChanged) { gChanged = true; OnValueChanged(); ValuesChanged = UI_group_UI.Changed = UI_group_LU.Changed = UI_runOn.Changed = UI_useInterlocked.Changed = UI_debug.Changed = UI_repeatN.Changed = UI_group_Report.Changed = UI_record_Report_Info.Changed = UI_report_Info.Changed = false; }
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
    if (UI_repeatN.Changed) { repeatN = roundu(pow10(round(log10(repeatN)))); }
    UI_report_Info.DisplayIf(Show_report_Info && UI_report_Info.treeGroup_parent.isExpanded);
  }
  public override void OnValueChanged_GS()
  {
    var type = "gsReport_Lib".ToType();
    if (type != null) ((GS)GetComponent(type))?.OnValueChanged_GS();

  }
  public string[] Scenes_in_Build => new string[] { "Assets/GS_Docs/gsADraw_Doc/ADraw_Doc.unity" };
  public virtual bool Show_report_Info { get => record_Report_Info; }
  public virtual RunOn runOn { get => (RunOn)g.runOn; set { if ((RunOn)g.runOn != value || (RunOn)UI_runOn.v != value) { g.runOn = UI_runOn.v = (uint)value; ValuesChanged = gChanged = true; } } }
  public virtual bool useInterlocked { get => Is(g.useInterlocked); set { if (g.useInterlocked != Is(value) || UI_useInterlocked.v != value) { g.useInterlocked = Is(UI_useInterlocked.v = value); ValuesChanged = gChanged = true; } } }
  public virtual bool debug { get => Is(g.debug); set { if (g.debug != Is(value) || UI_debug.v != value) { g.debug = Is(UI_debug.v = value); ValuesChanged = gChanged = true; } } }
  public virtual uint repeatN { get => g.repeatN; set { if (g.repeatN != value || UI_repeatN.v != value) { g.repeatN = UI_repeatN.v = value; ValuesChanged = gChanged = true; } } }
  public virtual bool record_Report_Info { get => Is(g.record_Report_Info); set { if (g.record_Report_Info != Is(value) || UI_record_Report_Info.v != value) { g.record_Report_Info = Is(UI_record_Report_Info.v = value); ValuesChanged = gChanged = true; } } }
  public virtual uint N { get => g.N; set { if (g.N != value) { g.N = value; ValuesChanged = gChanged = true; } } }
  public virtual uint focusedColumn { get => g.focusedColumn; set { if (g.focusedColumn != value) { g.focusedColumn = value; ValuesChanged = gChanged = true; } } }
  public virtual uint focusedRow { get => g.focusedRow; set { if (g.focusedRow != value) { g.focusedRow = value; ValuesChanged = gChanged = true; } } }
  public virtual uint4 LowerTriangleBounds { get => g.LowerTriangleBounds; set { if (any(g.LowerTriangleBounds != value)) { g.LowerTriangleBounds = value; ValuesChanged = gChanged = true; } } }
  public virtual float maxAbs { get => g.maxAbs; set { if (any(g.maxAbs != value)) { g.maxAbs = value; ValuesChanged = gChanged = true; } } }
  public virtual float rowMultiplier { get => g.rowMultiplier; set { if (any(g.rowMultiplier != value)) { g.rowMultiplier = value; ValuesChanged = gChanged = true; } } }
  public virtual float scale { get => g.scale; set { if (any(g.scale != value)) { g.scale = value; ValuesChanged = gChanged = true; } } }
  public bool group_UI { get => UI_group_UI?.v ?? false; set { if (UI_group_UI != null) UI_group_UI.v = value; } }
  public bool group_LU { get => UI_group_LU?.v ?? false; set { if (UI_group_LU != null) UI_group_LU.v = value; } }
  public bool group_Report { get => UI_group_Report?.v ?? false; set { if (UI_group_Report != null) UI_group_Report.v = value; } }
  public UI_TreeGroup UI_group_UI, UI_group_LU, UI_group_Report;
  public UI_enum UI_runOn;
  public UI_bool UI_useInterlocked, UI_debug, UI_record_Report_Info;
  public UI_uint UI_repeatN;
  public UI_method UI_LU_Decomposition;
  public virtual void LU_Decomposition() { }
  public UI_string UI_report_Info;
  public string report_Info { get => UI_report_Info?.v ?? ""; set { if (UI_report_Info != null && data != null) UI_report_Info.v = data.report_Info = value; } }
  public UI_method UI_Edit_Report;
  public virtual void Edit_Report() { }
  public UI_method UI_Build_Report;
  [HideInInspector] public bool in_Build_Report = false; public IEnumerator Build_Report() { if (in_Build_Report) { in_Build_Report = false; yield break; } in_Build_Report = true; yield return StartCoroutine(Build_Report_Sync()); in_Build_Report = false; }
  public virtual IEnumerator Build_Report_Sync() { yield return null; }
  public UI_method UI_Open_Report;
  public virtual void Open_Report() { }
  public UI_TreeGroup ui_group_UI => UI_group_UI;
  public UI_TreeGroup ui_group_LU => UI_group_LU;
  public UI_enum ui_runOn => UI_runOn;
  public UI_bool ui_useInterlocked => UI_useInterlocked;
  public UI_bool ui_debug => UI_debug;
  public UI_uint ui_repeatN => UI_repeatN;
  public UI_TreeGroup ui_group_Report => UI_group_Report;
  public UI_bool ui_record_Report_Info => UI_record_Report_Info;
  public UI_string ui_report_Info => UI_report_Info;
  [Serializable]
  public class uiData
  {
    public bool siUnits, group_UI, group_LU, useInterlocked, debug, group_Report, record_Report_Info;
    [JsonConverter(typeof(StringEnumConverter))] public RunOn runOn;
    public uint repeatN;
    public string report_Info;
  }
  public virtual void InitBuffers()
  {
    InitBuffers0_GS();
    AddComputeBuffer(ref gFR_Smart, nameof(gFR_Smart), 1);
    InitKernels();
    SetKernelValues(gFR_Smart, nameof(gFR_Smart), kernel_A_times_B_to_X, kernel_Zero_B, kernel_Interlock_SolveXfromUXequalY, kernel_Interlock_SolveYfromLYequalB, kernel_Interlock_Sum_SolveXfromUXequalY, kernel_Interlock_Sum_SolveYfromLYequalB, kernel_SolveXfromUXequalY, kernel_SolveYfromLYequalB, kernel_Set_rowMultiplier, kernel_MakeElementZeroAndFillWithLowerMatrixElement, kernel_Find_rowMultiplier, kernel_Divide_by_maxAbs, kernel_MakeLowerTriangleZeroAndFillWithL, kernel_Interlock_Find_maxElementRow, kernel_Interlock_Find_maxElementVal, kernel_Find_maxElementRow);
    SetKernelValues(gFR_Smart, nameof(gFR_Smart), kernel_Find_maxAbs, kernel_Find_row_maxAbs, kernel_InitP);
    InitBuffers1_GS();
    isInitBuffers = true;
  }
  public virtual void InitBuffers0_GS() { }
  public virtual void InitBuffers1_GS() { }
  [HideInInspector] public float[] grp_f_4096 = new float[4096];
  [Serializable]
  public struct GFR_Smart
  {
    public uint runOn, useInterlocked, debug, repeatN, record_Report_Info, N, focusedColumn, focusedRow;
    public uint4 LowerTriangleBounds;
    public float maxAbs, rowMultiplier, scale;
  };
  public RWStructuredBuffer<GFR_Smart> gFR_Smart;
  public RWStructuredBuffer<float> a, X, Y, b, row_maxAbs;
  public RWStructuredBuffer<int> ia, sums;
  public RWStructuredBuffer<uint> P, uints;
  public virtual void AllocData_a(uint n) => AddComputeBuffer(ref a, nameof(a), n);
  public virtual void AssignData_a(params float[] data) => AddComputeBufferData(ref a, nameof(a), data);
  public virtual void AllocData_X(uint n) => AddComputeBuffer(ref X, nameof(X), n);
  public virtual void AssignData_X(params float[] data) => AddComputeBufferData(ref X, nameof(X), data);
  public virtual void AllocData_Y(uint n) => AddComputeBuffer(ref Y, nameof(Y), n);
  public virtual void AssignData_Y(params float[] data) => AddComputeBufferData(ref Y, nameof(Y), data);
  public virtual void AllocData_b(uint n) => AddComputeBuffer(ref b, nameof(b), n);
  public virtual void AssignData_b(params float[] data) => AddComputeBufferData(ref b, nameof(b), data);
  public virtual void AllocData_row_maxAbs(uint n) => AddComputeBuffer(ref row_maxAbs, nameof(row_maxAbs), n);
  public virtual void AssignData_row_maxAbs(params float[] data) => AddComputeBufferData(ref row_maxAbs, nameof(row_maxAbs), data);
  public virtual void AllocData_ia(uint n) => AddComputeBuffer(ref ia, nameof(ia), n);
  public virtual void AssignData_ia(params int[] data) => AddComputeBufferData(ref ia, nameof(ia), data);
  public virtual void AllocData_sums(uint n) => AddComputeBuffer(ref sums, nameof(sums), n);
  public virtual void AssignData_sums(params int[] data) => AddComputeBufferData(ref sums, nameof(sums), data);
  public virtual void AllocData_P(uint n) => AddComputeBuffer(ref P, nameof(P), n);
  public virtual void AssignData_P(params uint[] data) => AddComputeBufferData(ref P, nameof(P), data);
  public virtual void AllocData_uints(uint n) => AddComputeBuffer(ref uints, nameof(uints), n);
  public virtual void AssignData_uints(params uint[] data) => AddComputeBufferData(ref uints, nameof(uints), data);
  public Texture2D _PaletteTex;
  public float4 palette(float v) => paletteColor(_PaletteTex, v);
  public float4 palette(float v, float w) => float4(palette(v).xyz, w);

  public virtual void onRenderObject_LIN(bool show, uint _itemN, ref uint i, ref uint index, ref uint3 LIN) { uint n = 0; if (show) { if (i < (n = _itemN)) LIN = uint3(index, i, 0); LIN.z += n; i -= n; } index++; }
  public virtual void onRenderObject_LIN(uint _itemN, ref uint i, ref uint index, ref uint3 LIN) { onRenderObject_LIN(true, _itemN, ref i, ref index, ref LIN); }
  public virtual uint3 onRenderObject_LIN(uint i) { uint3 LIN = u000; return LIN; }
  public virtual uint3 onRenderObject_LIN_Quads(uint i) { uint3 LIN = u000; return LIN; }
  public virtual uint3 onRenderObject_LIN_Points(uint i) { uint3 LIN = u000; return LIN; }
  [HideInInspector] public int kernel_A_times_B_to_X; [numthreads(numthreads1, 1, 1)] protected void A_times_B_to_X(uint3 id) { unchecked { if (id.x < N) A_times_B_to_X_GS(id); } }
  public virtual void A_times_B_to_X_GS(uint3 id) { }
  [HideInInspector] public int kernel_Zero_B; [numthreads(numthreads1, 1, 1)] protected void Zero_B(uint3 id) { unchecked { if (id.x < N) Zero_B_GS(id); } }
  public virtual void Zero_B_GS(uint3 id) { }
  [HideInInspector] public int kernel_Interlock_SolveXfromUXequalY; [numthreads(numthreads1, 1, 1)] protected void Interlock_SolveXfromUXequalY(uint3 id) { unchecked { if (id.x < 1) Interlock_SolveXfromUXequalY_GS(id); } }
  public virtual void Interlock_SolveXfromUXequalY_GS(uint3 id) { }
  [HideInInspector] public int kernel_Interlock_SolveYfromLYequalB; [numthreads(numthreads1, 1, 1)] protected void Interlock_SolveYfromLYequalB(uint3 id) { unchecked { if (id.x < 1) Interlock_SolveYfromLYequalB_GS(id); } }
  public virtual void Interlock_SolveYfromLYequalB_GS(uint3 id) { }
  [HideInInspector] public int kernel_Interlock_Sum_SolveXfromUXequalY; [numthreads(numthreads1, 1, 1)] protected void Interlock_Sum_SolveXfromUXequalY(uint3 id) { unchecked { if (id.x < N) Interlock_Sum_SolveXfromUXequalY_GS(id); } }
  public virtual void Interlock_Sum_SolveXfromUXequalY_GS(uint3 id) { }
  [HideInInspector] public int kernel_Interlock_Sum_SolveYfromLYequalB; [numthreads(numthreads1, 1, 1)] protected void Interlock_Sum_SolveYfromLYequalB(uint3 id) { unchecked { if (id.x < N) Interlock_Sum_SolveYfromLYequalB_GS(id); } }
  public virtual void Interlock_Sum_SolveYfromLYequalB_GS(uint3 id) { }
  [HideInInspector] public int kernel_SolveXfromUXequalY; [numthreads(numthreads1, 1, 1)] protected void SolveXfromUXequalY(uint3 id) { unchecked { if (id.x < 1) SolveXfromUXequalY_GS(id); } }
  public virtual void SolveXfromUXequalY_GS(uint3 id) { }
  [HideInInspector] public int kernel_SolveYfromLYequalB; [numthreads(numthreads1, 1, 1)] protected void SolveYfromLYequalB(uint3 id) { unchecked { if (id.x < 1) SolveYfromLYequalB_GS(id); } }
  public virtual void SolveYfromLYequalB_GS(uint3 id) { }
  [HideInInspector] public int kernel_Set_rowMultiplier; [numthreads(numthreads1, 1, 1)] protected void Set_rowMultiplier(uint3 id) { unchecked { if (id.x < 1) Set_rowMultiplier_GS(id); } }
  public virtual void Set_rowMultiplier_GS(uint3 id) { }
  [HideInInspector] public int kernel_MakeElementZeroAndFillWithLowerMatrixElement; [numthreads(numthreads1, 1, 1)] protected void MakeElementZeroAndFillWithLowerMatrixElement(uint3 id) { unchecked { if (id.x < N) MakeElementZeroAndFillWithLowerMatrixElement_GS(id); } }
  public virtual void MakeElementZeroAndFillWithLowerMatrixElement_GS(uint3 id) { }
  [HideInInspector] public int kernel_Find_rowMultiplier; [numthreads(numthreads1, 1, 1)] protected void Find_rowMultiplier(uint3 id) { unchecked { if (id.x < 1) Find_rowMultiplier_GS(id); } }
  public virtual void Find_rowMultiplier_GS(uint3 id) { }
  [HideInInspector] public int kernel_Divide_by_maxAbs; [numthreads(numthreads1, 1, 1)] protected void Divide_by_maxAbs(uint3 id) { unchecked { if (id.x < N * N) Divide_by_maxAbs_GS(id); } }
  public virtual void Divide_by_maxAbs_GS(uint3 id) { }
  [HideInInspector] public int kernel_MakeLowerTriangleZeroAndFillWithL; [numthreads(numthreads1, 1, 1)] protected void MakeLowerTriangleZeroAndFillWithL(uint3 id) { unchecked { if (id.x < N) MakeLowerTriangleZeroAndFillWithL_GS(id); } }
  public virtual void MakeLowerTriangleZeroAndFillWithL_GS(uint3 id) { }
  [HideInInspector] public int kernel_Interlock_Find_maxElementRow; [numthreads(numthreads1, 1, 1)] protected void Interlock_Find_maxElementRow(uint3 id) { unchecked { if (id.x < N) Interlock_Find_maxElementRow_GS(id); } }
  public virtual void Interlock_Find_maxElementRow_GS(uint3 id) { }
  [HideInInspector] public int kernel_Interlock_Find_maxElementVal; [numthreads(numthreads1, 1, 1)] protected void Interlock_Find_maxElementVal(uint3 id) { unchecked { if (id.x < N) Interlock_Find_maxElementVal_GS(id); } }
  public virtual void Interlock_Find_maxElementVal_GS(uint3 id) { }
  [HideInInspector] public int kernel_Find_maxElementRow; [numthreads(numthreads1, 1, 1)] protected void Find_maxElementRow(uint3 id) { unchecked { if (id.x < 1) Find_maxElementRow_GS(id); } }
  public virtual void Find_maxElementRow_GS(uint3 id) { }
  [HideInInspector] public int kernel_Find_maxAbs; [numthreads(numthreads1, 1, 1)] protected void Find_maxAbs(uint3 id) { unchecked { if (id.x < 1) Find_maxAbs_GS(id); } }
  public virtual void Find_maxAbs_GS(uint3 id) { }
  [HideInInspector] public int kernel_Find_row_maxAbs; [numthreads(numthreads1, 1, 1)] protected void Find_row_maxAbs(uint3 id) { unchecked { if (id.x < N) Find_row_maxAbs_GS(id); } }
  public virtual void Find_row_maxAbs_GS(uint3 id) { }
  [HideInInspector] public int kernel_InitP; [numthreads(numthreads1, 1, 1)] protected void InitP(uint3 id) { unchecked { if (id.x < N) InitP_GS(id); } }
  public virtual void InitP_GS(uint3 id) { }
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