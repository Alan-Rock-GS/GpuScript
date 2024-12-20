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
public class gsRand_Doc_ : GS
{
  [HideInInspector] public uiData data;
  [HideInInspector] public GRand_Doc g; public GRand_Doc G { get { gRand_Doc.GetData(); return gRand_Doc[0]; } }
  public void g_SetData() { if (gChanged && gRand_Doc != null) { gRand_Doc[0] = g; gRand_Doc.SetData(); gChanged = false; } }
  public virtual void uints_SetKernels(bool reallocate = false) { if (uints != null && (reallocate || uints.reallocated)) { SetKernelValues(uints, nameof(uints), kernel_Count_Pnts_out_of_Circle, kernel_Count_Pnts_in_Circle); uints.reallocated = false; } }
  public virtual void ints_SetKernels(bool reallocate = false) { if (ints != null && (reallocate || ints.reallocated)) { SetKernelValues(ints, nameof(ints), kernel_Integral_Avg, kernel_Calc_Average); ints.reallocated = false; } }
  public virtual void Rand_rs_SetKernels(bool reallocate = false) { if (Rand_rs != null && (reallocate || Rand_rs.reallocated)) { SetKernelValues(Rand_rs, nameof(Rand_rs), kernel_Integral_Avg, kernel_Count_Pnts_out_of_Circle, kernel_Count_Pnts_in_Circle, kernel_Calc_Average, kernel_Rand_initState, kernel_Rand_initSeed); Rand_rs.reallocated = false; } }
  public virtual void Gpu_Integral_Avg() { g_SetData(); ints?.SetCpu(); ints_SetKernels(); Rand_rs?.SetCpu(); Rand_rs_SetKernels(); Gpu(kernel_Integral_Avg, Integral_Avg, pntN); ints?.ResetWrite(); Rand_rs?.ResetWrite(); }
  public virtual void Cpu_Integral_Avg() { ints?.GetGpu(); Rand_rs?.GetGpu(); Cpu(Integral_Avg, pntN); ints.SetData(); Rand_rs.SetData(); }
  public virtual void Cpu_Integral_Avg(uint3 id) { ints?.GetGpu(); Rand_rs?.GetGpu(); Integral_Avg(id); ints.SetData(); Rand_rs.SetData(); }
  public virtual void Gpu_Count_Pnts_out_of_Circle() { g_SetData(); uints?.SetCpu(); uints_SetKernels(); Rand_rs?.SetCpu(); Rand_rs_SetKernels(); Gpu(kernel_Count_Pnts_out_of_Circle, Count_Pnts_out_of_Circle, pntN); uints?.ResetWrite(); Rand_rs?.ResetWrite(); }
  public virtual void Cpu_Count_Pnts_out_of_Circle() { uints?.GetGpu(); Rand_rs?.GetGpu(); Cpu(Count_Pnts_out_of_Circle, pntN); uints.SetData(); Rand_rs.SetData(); }
  public virtual void Cpu_Count_Pnts_out_of_Circle(uint3 id) { uints?.GetGpu(); Rand_rs?.GetGpu(); Count_Pnts_out_of_Circle(id); uints.SetData(); Rand_rs.SetData(); }
  public virtual void Gpu_Count_Pnts_in_Circle() { g_SetData(); uints?.SetCpu(); uints_SetKernels(); Rand_rs?.SetCpu(); Rand_rs_SetKernels(); Gpu(kernel_Count_Pnts_in_Circle, Count_Pnts_in_Circle, pntN); uints?.ResetWrite(); Rand_rs?.ResetWrite(); }
  public virtual void Cpu_Count_Pnts_in_Circle() { uints?.GetGpu(); Rand_rs?.GetGpu(); Cpu(Count_Pnts_in_Circle, pntN); uints.SetData(); Rand_rs.SetData(); }
  public virtual void Cpu_Count_Pnts_in_Circle(uint3 id) { uints?.GetGpu(); Rand_rs?.GetGpu(); Count_Pnts_in_Circle(id); uints.SetData(); Rand_rs.SetData(); }
  public virtual void Gpu_Calc_Average() { g_SetData(); ints?.SetCpu(); ints_SetKernels(); Rand_rs?.SetCpu(); Rand_rs_SetKernels(); Gpu(kernel_Calc_Average, Calc_Average, pntN); ints?.ResetWrite(); Rand_rs?.ResetWrite(); }
  public virtual void Cpu_Calc_Average() { ints?.GetGpu(); Rand_rs?.GetGpu(); Cpu(Calc_Average, pntN); ints.SetData(); Rand_rs.SetData(); }
  public virtual void Cpu_Calc_Average(uint3 id) { ints?.GetGpu(); Rand_rs?.GetGpu(); Calc_Average(id); ints.SetData(); Rand_rs.SetData(); }
  public virtual void Gpu_Rand_initState() { g_SetData(); Rand_rs?.SetCpu(); Rand_rs_SetKernels(); Gpu(kernel_Rand_initState, Rand_initState, Rand_I); Rand_rs?.ResetWrite(); }
  public virtual void Cpu_Rand_initState() { Rand_rs?.GetGpu(); Cpu(Rand_initState, Rand_I); Rand_rs.SetData(); }
  public virtual void Cpu_Rand_initState(uint3 id) { Rand_rs?.GetGpu(); Rand_initState(id); Rand_rs.SetData(); }
  public virtual void Gpu_Rand_initSeed() { g_SetData(); Rand_rs_SetKernels(); Gpu(kernel_Rand_initSeed, Rand_initSeed, Rand_N); Rand_rs?.ResetWrite(); }
  public virtual void Cpu_Rand_initSeed() { Rand_rs?.GetGpu(); Cpu(Rand_initSeed, Rand_N); Rand_rs.SetData(); }
  public virtual void Cpu_Rand_initSeed(uint3 id) { Rand_rs?.GetGpu(); Rand_initSeed(id); Rand_rs.SetData(); }
  public virtual void Gpu_Rand_grp_fill_1K() { g_SetData(); Gpu(kernel_Rand_grp_fill_1K, Rand_grp_fill_1K, Rand_N); }
  public virtual IEnumerator Cpu_Rand_grp_fill_1K() { yield return StartCoroutine(Cpu_Coroutine(kernel_Rand_grp_fill_1K, Rand_grp_fill_1K, Rand_N)); }
  public virtual void Cpu_Rand_grp_fill_1K(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { Rand_grp_fill_1K(grp_tid, grp_id, id, grpI); }
  public virtual void Gpu_Rand_grp_init_1K() { g_SetData(); Gpu(kernel_Rand_grp_init_1K, Rand_grp_init_1K, Rand_N / 1024); }
  public virtual IEnumerator Cpu_Rand_grp_init_1K() { yield return StartCoroutine(Cpu_Coroutine(kernel_Rand_grp_init_1K, Rand_grp_init_1K, Rand_N / 1024)); }
  public virtual void Cpu_Rand_grp_init_1K(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { Rand_grp_init_1K(grp_tid, grp_id, id, grpI); }
  public virtual void Gpu_Rand_grp_init_1M() { g_SetData(); Gpu(kernel_Rand_grp_init_1M, Rand_grp_init_1M, Rand_N / 1024 / 1024); }
  public virtual IEnumerator Cpu_Rand_grp_init_1M() { yield return StartCoroutine(Cpu_Coroutine(kernel_Rand_grp_init_1M, Rand_grp_init_1M, Rand_N / 1024 / 1024)); }
  public virtual void Cpu_Rand_grp_init_1M(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { Rand_grp_init_1M(grp_tid, grp_id, id, grpI); }
  [HideInInspector] public bool ValuesChanged, gChanged;
  public static gsRand_Doc This;
  public virtual void Awake() { This = this as gsRand_Doc; Awake_GS(); }
  public virtual void Awake_GS()
  {
    if(!GS_OCam_Lib.CheckKey("you@gmail.com", "2024/12/10", 123456)) print($"OCam_Lib not registered, check email, expiration, and key in gsRand_Doc_GS class");
    if(!GS_Report_Lib.CheckKey("you@gmail.com", "2024/12/10", 123456)) print($"Report_Lib not registered, check email, expiration, and key in gsRand_Doc_GS class");
  }
  public virtual void Start() { Build_UI(); Load_UI(); Start0_GS(); InitBuffers(); Start1_GS(); }
  public virtual void Start0_GS()
  {
    Rand_Start0_GS();
  }
  public virtual void Start1_GS()
  {
    Rand_Start1_GS();
  }
  public override void onLoaded()
  {
    base.onLoaded();
    GS_OCam_Lib.onLoaded(OCam_Lib);
    GS_Report_Lib.onLoaded(Report_Lib);
  }
  [HideInInspector] public bool already_quited = false;
  public override void OnApplicationQuit() { Save_UI(); OnApplicationQuit_GS(); base.OnApplicationQuit(); already_quited = true; }
  public virtual void OnApplicationQuit_GS()
  {
    Rand_OnApplicationQuit_GS();
  }
  public override void ui_to_data()
  {
    if (data == null) return;
    data.siUnits = siUnits;
    data.group_UI = group_UI;
    data.group_Rand = group_Rand;
    data.pntN = pntN;
    data.group_Avg = group_Avg;
    data.Avg_Val = Avg_Val;
    data.Avg_Val_Runtime = Avg_Val_Runtime;
    data.Avg_Val_TFlops = Avg_Val_TFlops;
    data.group_Area_PI = group_Area_PI;
    data.Area_PI_Val = Area_PI_Val;
    data.Area_PI_Error = Area_PI_Error;
    data.Area_PI_Runtime = Area_PI_Runtime;
    data.Area_PI_TFlops = Area_PI_TFlops;
    data.group_Integral_PI = group_Integral_PI;
    data.Integral_PI_Val = Integral_PI_Val;
    data.Integral_PI_Error = Integral_PI_Error;
    data.Integral_PI_Runtime = Integral_PI_Runtime;
    data.Integral_PI_TFlops = Integral_PI_TFlops;
  }
  public override void data_to_ui()
  {
    if (data == null) return;
    group_UI = data.group_UI;
    group_Rand = data.group_Rand;
    pntN = ui_txt_str.Contains("\"pntN\"") ? data.pntN : 1000;
    group_Avg = data.group_Avg;
    Avg_Val = data.Avg_Val;
    Avg_Val_Runtime = data.Avg_Val_Runtime;
    Avg_Val_TFlops = data.Avg_Val_TFlops;
    group_Area_PI = data.group_Area_PI;
    Area_PI_Val = data.Area_PI_Val;
    Area_PI_Error = data.Area_PI_Error;
    Area_PI_Runtime = data.Area_PI_Runtime;
    Area_PI_TFlops = data.Area_PI_TFlops;
    group_Integral_PI = data.group_Integral_PI;
    Integral_PI_Val = data.Integral_PI_Val;
    Integral_PI_Error = data.Integral_PI_Error;
    Integral_PI_Runtime = data.Integral_PI_Runtime;
    Integral_PI_TFlops = data.Integral_PI_TFlops;
    if (!data.siUnits) { for (int i = 0; i < 3; i++) siUnits = !siUnits; OnUnitsChanged(); }
  }
  public virtual void Save(string path, string projectName)
  {
    projectPaths = path;
    $"{projectPath}{projectName}.txt".WriteAllText(JsonConvert.SerializeObject(data, Formatting.Indented));
    foreach (var lib in GetComponents<GS>()) if (lib != this) lib.Save_UI();
    $"{projectPath}{name}_Data.txt".WriteAllText(JsonConvert.SerializeObject(data, Formatting.Indented));
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
    LateUpdate0_GS();
    if (UI_pntN.Changed || pntN != UI_pntN.v) pntN = UI_pntN.v;
    if (UI_Avg_Val.Changed || Avg_Val != UI_Avg_Val._si) Avg_Val = UI_Avg_Val._si;
    if (UI_Avg_Val_Runtime.Changed || Avg_Val_Runtime != UI_Avg_Val_Runtime.s) Avg_Val_Runtime = UI_Avg_Val_Runtime.s;
    if (UI_Avg_Val_TFlops.Changed || Avg_Val_TFlops != UI_Avg_Val_TFlops._si) Avg_Val_TFlops = UI_Avg_Val_TFlops._si;
    if (UI_Area_PI_Val.Changed || Area_PI_Val != UI_Area_PI_Val._si) Area_PI_Val = UI_Area_PI_Val._si;
    if (UI_Area_PI_Error.Changed || Area_PI_Error != UI_Area_PI_Error._si) Area_PI_Error = UI_Area_PI_Error._si;
    if (UI_Area_PI_Runtime.Changed || Area_PI_Runtime != UI_Area_PI_Runtime.s) Area_PI_Runtime = UI_Area_PI_Runtime.s;
    if (UI_Area_PI_TFlops.Changed || Area_PI_TFlops != UI_Area_PI_TFlops._si) Area_PI_TFlops = UI_Area_PI_TFlops._si;
    if (UI_Integral_PI_Val.Changed || Integral_PI_Val != UI_Integral_PI_Val._si) Integral_PI_Val = UI_Integral_PI_Val._si;
    if (UI_Integral_PI_Error.Changed || Integral_PI_Error != UI_Integral_PI_Error._si) Integral_PI_Error = UI_Integral_PI_Error._si;
    if (UI_Integral_PI_Runtime.Changed || Integral_PI_Runtime != UI_Integral_PI_Runtime.s) Integral_PI_Runtime = UI_Integral_PI_Runtime.s;
    if (UI_Integral_PI_TFlops.Changed || Integral_PI_TFlops != UI_Integral_PI_TFlops._si) Integral_PI_TFlops = UI_Integral_PI_TFlops._si;
    if (ValuesChanged) { gChanged = true; OnValueChanged(); ValuesChanged = UI_group_UI.Changed = UI_group_Rand.Changed = UI_pntN.Changed = UI_group_Avg.Changed = UI_Avg_Val.Changed = UI_Avg_Val_Runtime.Changed = UI_Avg_Val_TFlops.Changed = UI_group_Area_PI.Changed = UI_Area_PI_Val.Changed = UI_Area_PI_Error.Changed = UI_Area_PI_Runtime.Changed = UI_Area_PI_TFlops.Changed = UI_group_Integral_PI.Changed = UI_Integral_PI_Val.Changed = UI_Integral_PI_Error.Changed = UI_Integral_PI_Runtime.Changed = UI_Integral_PI_TFlops.Changed = false; }
    Rand_LateUpdate1_GS();
    LateUpdate1_GS();
    if(lateUpdateI++ == uint_max) lateUpdateI = 100;
  }
  public virtual void LateUpdate0_GS()
  {
    Rand_LateUpdate0_GS();
  }
  public virtual void LateUpdate1_GS()
  {
    Rand_LateUpdate1_GS();
  }
  public override void Update()
  {
    base.Update();
    if (!ui_loaded) return;
    Update0_GS();
    Rand_Update1_GS();
    Update1_GS();
  }
  public virtual void Update0_GS()
  {
    Rand_Update0_GS();
  }
  public virtual void Update1_GS()
  {
    Rand_Update1_GS();
  }
  public override void OnValueChanged()
  {
    if (!ui_loaded) return;
    OnValueChanged_GS();
  }
  public override void OnValueChanged_GS()
  {
    Rand_OnValueChanged_GS();
    var type = "gsReport_Lib".ToType();
    if (type != null) ((GS)GetComponent(type))?.OnValueChanged_GS();

  }
  public string[] Scenes_in_Build => new string[] {  };
  public virtual float f2i { get => g.f2i; set { if (any(g.f2i != value)) { g.f2i = value; ValuesChanged = gChanged = true; } } }
  public virtual uint pntN { get => g.pntN; set { if (g.pntN != value || UI_pntN.v != value) { g.pntN = UI_pntN.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float Avg_Val { get => g.Avg_Val; set { if (any(g.Avg_Val != value) || any(UI_Avg_Val.si != value)) { g.Avg_Val = UI_Avg_Val.si = value; ValuesChanged = gChanged = true; } } }
  public virtual float Avg_Val_Runtime { get => g.Avg_Val_Runtime; set { if (any(g.Avg_Val_Runtime != value) || any(UI_Avg_Val_Runtime.s != value)) { g.Avg_Val_Runtime = UI_Avg_Val_Runtime.s = value; ValuesChanged = gChanged = true; } } }
  public virtual float Avg_Val_TFlops { get => g.Avg_Val_TFlops; set { if (any(g.Avg_Val_TFlops != value) || any(UI_Avg_Val_TFlops.si != value)) { g.Avg_Val_TFlops = UI_Avg_Val_TFlops.si = value; ValuesChanged = gChanged = true; } } }
  public virtual float Area_PI_Val { get => g.Area_PI_Val; set { if (any(g.Area_PI_Val != value) || any(UI_Area_PI_Val.si != value)) { g.Area_PI_Val = UI_Area_PI_Val.si = value; ValuesChanged = gChanged = true; } } }
  public virtual float Area_PI_Error { get => g.Area_PI_Error; set { if (any(g.Area_PI_Error != value) || any(UI_Area_PI_Error.si != value)) { g.Area_PI_Error = UI_Area_PI_Error.si = value; ValuesChanged = gChanged = true; } } }
  public virtual float Area_PI_Runtime { get => g.Area_PI_Runtime; set { if (any(g.Area_PI_Runtime != value) || any(UI_Area_PI_Runtime.s != value)) { g.Area_PI_Runtime = UI_Area_PI_Runtime.s = value; ValuesChanged = gChanged = true; } } }
  public virtual float Area_PI_TFlops { get => g.Area_PI_TFlops; set { if (any(g.Area_PI_TFlops != value) || any(UI_Area_PI_TFlops.si != value)) { g.Area_PI_TFlops = UI_Area_PI_TFlops.si = value; ValuesChanged = gChanged = true; } } }
  public virtual float Integral_PI_Val { get => g.Integral_PI_Val; set { if (any(g.Integral_PI_Val != value) || any(UI_Integral_PI_Val.si != value)) { g.Integral_PI_Val = UI_Integral_PI_Val.si = value; ValuesChanged = gChanged = true; } } }
  public virtual float Integral_PI_Error { get => g.Integral_PI_Error; set { if (any(g.Integral_PI_Error != value) || any(UI_Integral_PI_Error.si != value)) { g.Integral_PI_Error = UI_Integral_PI_Error.si = value; ValuesChanged = gChanged = true; } } }
  public virtual float Integral_PI_Runtime { get => g.Integral_PI_Runtime; set { if (any(g.Integral_PI_Runtime != value) || any(UI_Integral_PI_Runtime.s != value)) { g.Integral_PI_Runtime = UI_Integral_PI_Runtime.s = value; ValuesChanged = gChanged = true; } } }
  public virtual float Integral_PI_TFlops { get => g.Integral_PI_TFlops; set { if (any(g.Integral_PI_TFlops != value) || any(UI_Integral_PI_TFlops.si != value)) { g.Integral_PI_TFlops = UI_Integral_PI_TFlops.si = value; ValuesChanged = gChanged = true; } } }
  public virtual uint Rand_N { get => g.Rand_N; set { if (g.Rand_N != value) { g.Rand_N = value; ValuesChanged = gChanged = true; } } }
  public virtual uint Rand_I { get => g.Rand_I; set { if (g.Rand_I != value) { g.Rand_I = value; ValuesChanged = gChanged = true; } } }
  public virtual uint Rand_J { get => g.Rand_J; set { if (g.Rand_J != value) { g.Rand_J = value; ValuesChanged = gChanged = true; } } }
  public virtual uint4 Rand_seed4 { get => g.Rand_seed4; set { if (any(g.Rand_seed4 != value)) { g.Rand_seed4 = value; ValuesChanged = gChanged = true; } } }
  public bool group_UI { get => UI_group_UI?.v ?? false; set { if (UI_group_UI != null) UI_group_UI.v = value; } }
  public bool group_Rand { get => UI_group_Rand?.v ?? false; set { if (UI_group_Rand != null) UI_group_Rand.v = value; } }
  public bool group_Avg { get => UI_group_Avg?.v ?? false; set { if (UI_group_Avg != null) UI_group_Avg.v = value; } }
  public bool group_Area_PI { get => UI_group_Area_PI?.v ?? false; set { if (UI_group_Area_PI != null) UI_group_Area_PI.v = value; } }
  public bool group_Integral_PI { get => UI_group_Integral_PI?.v ?? false; set { if (UI_group_Integral_PI != null) UI_group_Integral_PI.v = value; } }
  public UI_TreeGroup UI_group_UI, UI_group_Rand, UI_group_Avg, UI_group_Area_PI, UI_group_Integral_PI;
  public UI_uint UI_pntN;
  public UI_method UI_Avg;
  public virtual void Avg() { }
  public UI_float UI_Avg_Val, UI_Avg_Val_Runtime, UI_Avg_Val_TFlops, UI_Area_PI_Val, UI_Area_PI_Error, UI_Area_PI_Runtime, UI_Area_PI_TFlops, UI_Integral_PI_Val, UI_Integral_PI_Error, UI_Integral_PI_Runtime, UI_Integral_PI_TFlops;
  public UI_method UI_Area_PI;
  public virtual void Area_PI() { }
  public UI_method UI_Integral_PI;
  public virtual void Integral_PI() { }
  public UI_TreeGroup ui_group_UI => UI_group_UI;
  public UI_TreeGroup ui_group_Rand => UI_group_Rand;
  public UI_uint ui_pntN => UI_pntN;
  public UI_TreeGroup ui_group_Avg => UI_group_Avg;
  public UI_float ui_Avg_Val => UI_Avg_Val;
  public UI_float ui_Avg_Val_Runtime => UI_Avg_Val_Runtime;
  public UI_float ui_Avg_Val_TFlops => UI_Avg_Val_TFlops;
  public UI_TreeGroup ui_group_Area_PI => UI_group_Area_PI;
  public UI_float ui_Area_PI_Val => UI_Area_PI_Val;
  public UI_float ui_Area_PI_Error => UI_Area_PI_Error;
  public UI_float ui_Area_PI_Runtime => UI_Area_PI_Runtime;
  public UI_float ui_Area_PI_TFlops => UI_Area_PI_TFlops;
  public UI_TreeGroup ui_group_Integral_PI => UI_group_Integral_PI;
  public UI_float ui_Integral_PI_Val => UI_Integral_PI_Val;
  public UI_float ui_Integral_PI_Error => UI_Integral_PI_Error;
  public UI_float ui_Integral_PI_Runtime => UI_Integral_PI_Runtime;
  public UI_float ui_Integral_PI_TFlops => UI_Integral_PI_TFlops;
  [Serializable]
  public class uiData
  {
    public bool siUnits, group_UI, group_Rand, group_Avg, group_Area_PI, group_Integral_PI;
    public uint pntN;
    public float Avg_Val, Avg_Val_Runtime, Avg_Val_TFlops, Area_PI_Val, Area_PI_Error, Area_PI_Runtime, Area_PI_TFlops, Integral_PI_Val, Integral_PI_Error, Integral_PI_Runtime, Integral_PI_TFlops;
  }
  public virtual void InitBuffers()
  {
    InitBuffers0_GS();
    AddComputeBuffer(ref gRand_Doc, nameof(gRand_Doc), 1);
    InitKernels();
    SetKernelValues(gRand_Doc, nameof(gRand_Doc), kernel_Integral_Avg, kernel_Count_Pnts_out_of_Circle, kernel_Count_Pnts_in_Circle, kernel_Calc_Average, kernel_Rand_initState, kernel_Rand_initSeed, kernel_Rand_grp_fill_1K, kernel_Rand_grp_init_1K, kernel_Rand_grp_init_1M);
    AddComputeBuffer(ref uints, nameof(uints), 1);
    AddComputeBuffer(ref ints, nameof(ints), 1);
    AddComputeBuffer(ref Rand_rs, nameof(Rand_rs), Rand_N);
    InitBuffers1_GS();
    isInitBuffers = true;
  }
  public virtual void InitBuffers0_GS()
  {
    Rand_InitBuffers0_GS();
  }
  public virtual void InitBuffers1_GS()
  {
    Rand_InitBuffers1_GS();
  }
  [HideInInspector] public uint4[] Rand_grp = new uint4[1024];
  [Serializable]
  public struct GRand_Doc
  {
    public float f2i, Avg_Val, Avg_Val_Runtime, Avg_Val_TFlops, Area_PI_Val, Area_PI_Error, Area_PI_Runtime, Area_PI_TFlops, Integral_PI_Val, Integral_PI_Error, Integral_PI_Runtime, Integral_PI_TFlops;
    public uint pntN, Rand_N, Rand_I, Rand_J;
    public uint4 Rand_seed4;
  };
  public RWStructuredBuffer<GRand_Doc> gRand_Doc;
  public RWStructuredBuffer<uint> uints;
  public RWStructuredBuffer<int> ints;
  public RWStructuredBuffer<uint4> Rand_rs;
  public virtual void Allocate_uints(uint n) => AddComputeBuffer(ref uints, nameof(uints), n);
  public virtual void Assign_uints(params uint[] data) => AddComputeBufferData(ref uints, nameof(uints), data);
  public virtual void Allocate_ints(uint n) => AddComputeBuffer(ref ints, nameof(ints), n);
  public virtual void Assign_ints(params int[] data) => AddComputeBufferData(ref ints, nameof(ints), data);
  public virtual void Allocate_Rand_rs(uint n) => AddComputeBuffer(ref Rand_rs, nameof(Rand_rs), n);
  public virtual void Assign_Rand_rs(params uint4[] data) => AddComputeBufferData(ref Rand_rs, nameof(Rand_rs), data);
  public Texture2D _PaletteTex;
  public float4 palette(float v) => paletteColor(_PaletteTex, v);
  public float4 palette(float v, float w) => float4(palette(v).xyz, w);

  public virtual void onRenderObject_LIN(bool show, uint _itemN, ref uint i, ref uint index, ref uint3 LIN) { uint n = 0; if (show) { if (i < (n = _itemN)) LIN = uint3(index, i, 0); LIN.z += n; i -= n; } index++; }
  public virtual void onRenderObject_LIN(uint _itemN, ref uint i, ref uint index, ref uint3 LIN) { onRenderObject_LIN(true, _itemN, ref i, ref index, ref LIN); }
  public virtual uint3 onRenderObject_LIN(uint i) { uint3 LIN = u000; uint index = 0; return LIN; }
  public virtual uint3 onRenderObject_LIN_Quads(uint i) { uint3 LIN = u000; uint index = 0; return LIN; }
  public virtual uint3 onRenderObject_LIN_Points(uint i) { uint3 LIN = u000; uint index = 0; return LIN; }
  [HideInInspector] public int kernel_Integral_Avg; [numthreads(numthreads1, 1, 1)] protected void Integral_Avg(uint3 id) { unchecked { if (id.x < pntN) Integral_Avg_GS(id); } }
  public virtual void Integral_Avg_GS(uint3 id) { }
  [HideInInspector] public int kernel_Count_Pnts_out_of_Circle; [numthreads(numthreads1, 1, 1)] protected void Count_Pnts_out_of_Circle(uint3 id) { unchecked { if (id.x < pntN) Count_Pnts_out_of_Circle_GS(id); } }
  public virtual void Count_Pnts_out_of_Circle_GS(uint3 id) { }
  [HideInInspector] public int kernel_Count_Pnts_in_Circle; [numthreads(numthreads1, 1, 1)] protected void Count_Pnts_in_Circle(uint3 id) { unchecked { if (id.x < pntN) Count_Pnts_in_Circle_GS(id); } }
  public virtual void Count_Pnts_in_Circle_GS(uint3 id) { }
  [HideInInspector] public int kernel_Calc_Average; [numthreads(numthreads1, 1, 1)] protected void Calc_Average(uint3 id) { unchecked { if (id.x < pntN) Calc_Average_GS(id); } }
  public virtual void Calc_Average_GS(uint3 id) { }
  [HideInInspector] public int kernel_Rand_initState; [numthreads(numthreads1, 1, 1)] protected void Rand_initState(uint3 id) { unchecked { if (id.x < Rand_I) Rand_initState_GS(id); } }
  [HideInInspector] public int kernel_Rand_initSeed; [numthreads(numthreads1, 1, 1)] protected void Rand_initSeed(uint3 id) { unchecked { if (id.x < Rand_N) Rand_initSeed_GS(id); } }
  [HideInInspector] public int kernel_Rand_grp_fill_1K; [numthreads(numthreads1, 1, 1)] protected IEnumerator Rand_grp_fill_1K(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < Rand_N) yield return StartCoroutine(Rand_grp_fill_1K_GS(grp_tid, grp_id, id, grpI)); } }
  public virtual IEnumerator Rand_grp_fill_1K_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { yield return null; }
  [HideInInspector] public int kernel_Rand_grp_init_1K; [numthreads(numthreads1, 1, 1)] protected IEnumerator Rand_grp_init_1K(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < Rand_N / 1024) yield return StartCoroutine(Rand_grp_init_1K_GS(grp_tid, grp_id, id, grpI)); } }
  public virtual IEnumerator Rand_grp_init_1K_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { yield return null; }
  [HideInInspector] public int kernel_Rand_grp_init_1M; [numthreads(numthreads1, 1, 1)] protected IEnumerator Rand_grp_init_1M(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < Rand_N / 1024 / 1024) yield return StartCoroutine(Rand_grp_init_1M_GS(grp_tid, grp_id, id, grpI)); } }
  public virtual IEnumerator Rand_grp_init_1M_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { yield return null; }
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
    Rand_onRenderObject_GS(ref render, ref cpu);
  }
  public virtual float4 frag_GS(v2f i, float4 color)
  {
    uint libI = roundu(i.tj.x);
    if (libI == 0) return frag_Rand_GS(i, color);
    return color;
  }
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
  #region <OCam_Lib>
  gsOCam_Lib _OCam_Lib; public gsOCam_Lib OCam_Lib => _OCam_Lib = _OCam_Lib ?? Add_Component_to_gameObject<gsOCam_Lib>();
  #endregion <OCam_Lib>
  #region <Report_Lib>
  gsReport_Lib _Report_Lib; public gsReport_Lib Report_Lib => _Report_Lib = _Report_Lib ?? Add_Component_to_gameObject<gsReport_Lib>();
  #endregion <Report_Lib>
}