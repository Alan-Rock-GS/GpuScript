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
public class gsText2Num_Tutorial_ : GS
{
  [HideInInspector] public uiData data;
  [HideInInspector] public GText2Num_Tutorial g; public GText2Num_Tutorial G { get { gText2Num_Tutorial.GetData(); return gText2Num_Tutorial[0]; } }
  public void g_SetData() { if (gChanged && gText2Num_Tutorial != null) { gText2Num_Tutorial[0] = g; gText2Num_Tutorial.SetData(); gChanged = false; } }
  public virtual void Text_SetKernels(bool reallocate = false) { if (Text != null && (reallocate || Text.reallocated)) { SetKernelValues(Text, nameof(Text), kernel_parseText, kernel_ABuff_GetSums, kernel_ABuff_Get_Bits); Text.reallocated = false; } }
  public virtual void ints_SetKernels(bool reallocate = false) { if (ints != null && (reallocate || ints.reallocated)) { SetKernelValues(ints, nameof(ints), kernel_parseText); ints.reallocated = false; } }
  public virtual void ABuff_Bits_SetKernels(bool reallocate = false) { if (ABuff_Bits != null && (reallocate || ABuff_Bits.reallocated)) { SetKernelValues(ABuff_Bits, nameof(ABuff_Bits), kernel_ABuff_GetIndexes, kernel_ABuff_Get_Bits_Sums, kernel_ABuff_GetSums, kernel_ABuff_Get_Bits); ABuff_Bits.reallocated = false; } }
  public virtual void ABuff_Sums_SetKernels(bool reallocate = false) { if (ABuff_Sums != null && (reallocate || ABuff_Sums.reallocated)) { SetKernelValues(ABuff_Sums, nameof(ABuff_Sums), kernel_ABuff_GetIndexes, kernel_ABuff_IncSums, kernel_ABuff_GetFills1, kernel_ABuff_Get_Bits_Sums, kernel_ABuff_GetSums); ABuff_Sums.reallocated = false; } }
  public virtual void ABuff_Indexes_SetKernels(bool reallocate = false) { if (ABuff_Indexes != null && (reallocate || ABuff_Indexes.reallocated)) { SetKernelValues(ABuff_Indexes, nameof(ABuff_Indexes), kernel_parseText, kernel_ABuff_GetIndexes); ABuff_Indexes.reallocated = false; } }
  public virtual void ABuff_Fills1_SetKernels(bool reallocate = false) { if (ABuff_Fills1 != null && (reallocate || ABuff_Fills1.reallocated)) { SetKernelValues(ABuff_Fills1, nameof(ABuff_Fills1), kernel_ABuff_IncSums, kernel_ABuff_IncFills1, kernel_ABuff_GetFills2, kernel_ABuff_GetFills1); ABuff_Fills1.reallocated = false; } }
  public virtual void ABuff_Fills2_SetKernels(bool reallocate = false) { if (ABuff_Fills2 != null && (reallocate || ABuff_Fills2.reallocated)) { SetKernelValues(ABuff_Fills2, nameof(ABuff_Fills2), kernel_ABuff_IncFills1, kernel_ABuff_GetFills2); ABuff_Fills2.reallocated = false; } }
  public virtual void Gpu_parseText() { g_SetData(); Text?.SetCpu(); Text_SetKernels(); ints_SetKernels(); ABuff_Indexes?.SetCpu(); ABuff_Indexes_SetKernels(); Gpu(kernel_parseText, parseText, itemN); ints?.ResetWrite(); }
  public virtual void Cpu_parseText() { Text?.GetGpu(); ints?.GetGpu(); ABuff_Indexes?.GetGpu(); Cpu(parseText, itemN); ints.SetData(); }
  public virtual void Cpu_parseText(uint3 id) { Text?.GetGpu(); ints?.GetGpu(); ABuff_Indexes?.GetGpu(); parseText(id); ints.SetData(); }
  public virtual void Gpu_ABuff_GetIndexes() { g_SetData(); ABuff_Bits?.SetCpu(); ABuff_Bits_SetKernels(); ABuff_Sums?.SetCpu(); ABuff_Sums_SetKernels(); ABuff_Indexes_SetKernels(); Gpu(kernel_ABuff_GetIndexes, ABuff_GetIndexes, ABuff_BitN); ABuff_Indexes?.ResetWrite(); }
  public virtual void Cpu_ABuff_GetIndexes() { ABuff_Bits?.GetGpu(); ABuff_Sums?.GetGpu(); ABuff_Indexes?.GetGpu(); Cpu(ABuff_GetIndexes, ABuff_BitN); ABuff_Indexes.SetData(); }
  public virtual void Cpu_ABuff_GetIndexes(uint3 id) { ABuff_Bits?.GetGpu(); ABuff_Sums?.GetGpu(); ABuff_Indexes?.GetGpu(); ABuff_GetIndexes(id); ABuff_Indexes.SetData(); }
  public virtual void Gpu_ABuff_IncSums() { g_SetData(); ABuff_Sums?.SetCpu(); ABuff_Sums_SetKernels(); ABuff_Fills1?.SetCpu(); ABuff_Fills1_SetKernels(); gText2Num_Tutorial?.SetCpu(); Gpu(kernel_ABuff_IncSums, ABuff_IncSums, ABuff_BitN); ABuff_Sums?.ResetWrite(); g = G; }
  public virtual void Cpu_ABuff_IncSums() { ABuff_Sums?.GetGpu(); ABuff_Fills1?.GetGpu(); Cpu(ABuff_IncSums, ABuff_BitN); ABuff_Sums.SetData(); }
  public virtual void Cpu_ABuff_IncSums(uint3 id) { ABuff_Sums?.GetGpu(); ABuff_Fills1?.GetGpu(); ABuff_IncSums(id); ABuff_Sums.SetData(); }
  public virtual void Gpu_ABuff_IncFills1() { g_SetData(); ABuff_Fills1?.SetCpu(); ABuff_Fills1_SetKernels(); ABuff_Fills2?.SetCpu(); ABuff_Fills2_SetKernels(); Gpu(kernel_ABuff_IncFills1, ABuff_IncFills1, ABuff_BitN1); ABuff_Fills1?.ResetWrite(); }
  public virtual void Cpu_ABuff_IncFills1() { ABuff_Fills1?.GetGpu(); ABuff_Fills2?.GetGpu(); Cpu(ABuff_IncFills1, ABuff_BitN1); ABuff_Fills1.SetData(); }
  public virtual void Cpu_ABuff_IncFills1(uint3 id) { ABuff_Fills1?.GetGpu(); ABuff_Fills2?.GetGpu(); ABuff_IncFills1(id); ABuff_Fills1.SetData(); }
  public virtual void Gpu_ABuff_GetFills2() { g_SetData(); ABuff_Fills1?.SetCpu(); ABuff_Fills1_SetKernels(); ABuff_Fills2_SetKernels(); Gpu(kernel_ABuff_GetFills2, ABuff_GetFills2, ABuff_BitN2); ABuff_Fills2?.ResetWrite(); }
  public virtual IEnumerator Cpu_ABuff_GetFills2() { ABuff_Fills1?.GetGpu(); ABuff_Fills2?.GetGpu(); yield return StartCoroutine(Cpu_Coroutine(kernel_ABuff_GetFills2, ABuff_GetFills2, ABuff_BitN2)); }
  public virtual void Cpu_ABuff_GetFills2(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { ABuff_Fills1?.GetGpu(); ABuff_Fills2?.GetGpu(); ABuff_GetFills2(grp_tid, grp_id, id, grpI); ABuff_Fills2.SetData(); }
  public virtual void Gpu_ABuff_GetFills1() { g_SetData(); ABuff_Sums?.SetCpu(); ABuff_Sums_SetKernels(); ABuff_Fills1_SetKernels(); Gpu(kernel_ABuff_GetFills1, ABuff_GetFills1, ABuff_BitN1); ABuff_Fills1?.ResetWrite(); }
  public virtual IEnumerator Cpu_ABuff_GetFills1() { ABuff_Sums?.GetGpu(); ABuff_Fills1?.GetGpu(); yield return StartCoroutine(Cpu_Coroutine(kernel_ABuff_GetFills1, ABuff_GetFills1, ABuff_BitN1)); }
  public virtual void Cpu_ABuff_GetFills1(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { ABuff_Sums?.GetGpu(); ABuff_Fills1?.GetGpu(); ABuff_GetFills1(grp_tid, grp_id, id, grpI); ABuff_Fills1.SetData(); }
  public virtual void Gpu_ABuff_Get_Bits_Sums() { g_SetData(); ABuff_Bits?.SetCpu(); ABuff_Bits_SetKernels(); ABuff_Sums_SetKernels(); Gpu(kernel_ABuff_Get_Bits_Sums, ABuff_Get_Bits_Sums, ABuff_BitN); ABuff_Sums?.ResetWrite(); }
  public virtual IEnumerator Cpu_ABuff_Get_Bits_Sums() { ABuff_Bits?.GetGpu(); ABuff_Sums?.GetGpu(); yield return StartCoroutine(Cpu_Coroutine(kernel_ABuff_Get_Bits_Sums, ABuff_Get_Bits_Sums, ABuff_BitN)); }
  public virtual void Cpu_ABuff_Get_Bits_Sums(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { ABuff_Bits?.GetGpu(); ABuff_Sums?.GetGpu(); ABuff_Get_Bits_Sums(grp_tid, grp_id, id, grpI); ABuff_Sums.SetData(); }
  public virtual void Gpu_ABuff_GetSums() { g_SetData(); ABuff_Bits_SetKernels(); ABuff_Sums_SetKernels(); Text?.SetCpu(); Text_SetKernels(); Gpu(kernel_ABuff_GetSums, ABuff_GetSums, ABuff_BitN); ABuff_Bits?.ResetWrite(); ABuff_Sums?.ResetWrite(); }
  public virtual IEnumerator Cpu_ABuff_GetSums() { ABuff_Bits?.GetGpu(); ABuff_Sums?.GetGpu(); Text?.GetGpu(); yield return StartCoroutine(Cpu_Coroutine(kernel_ABuff_GetSums, ABuff_GetSums, ABuff_BitN)); }
  public virtual void Cpu_ABuff_GetSums(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { ABuff_Bits?.GetGpu(); ABuff_Sums?.GetGpu(); Text?.GetGpu(); ABuff_GetSums(grp_tid, grp_id, id, grpI); ABuff_Bits.SetData(); ABuff_Sums.SetData(); }
  public virtual void Gpu_ABuff_Get_Bits() { g_SetData(); ABuff_Bits_SetKernels(); Text?.SetCpu(); Text_SetKernels(); Gpu(kernel_ABuff_Get_Bits, ABuff_Get_Bits, ABuff_BitN); ABuff_Bits?.ResetWrite(); }
  public virtual void Cpu_ABuff_Get_Bits() { ABuff_Bits?.GetGpu(); Text?.GetGpu(); Cpu(ABuff_Get_Bits, ABuff_BitN); ABuff_Bits.SetData(); }
  public virtual void Cpu_ABuff_Get_Bits(uint3 id) { ABuff_Bits?.GetGpu(); Text?.GetGpu(); ABuff_Get_Bits(id); ABuff_Bits.SetData(); }
  [HideInInspector] public bool ValuesChanged, gChanged;
  public static gsText2Num_Tutorial This;
  public virtual void Awake() { This = this as gsText2Num_Tutorial; Awake_GS(); }
  public virtual void Awake_GS()
  {
  }
  public virtual void Start() { Build_UI(); Load_UI(); Start0_GS(); InitBuffers(); Start1_GS(); }
  public virtual void Start0_GS()
  {
    ABuff_Start0_GS();
  }
  public virtual void Start1_GS()
  {
    ABuff_Start1_GS();
  }
  public override void onLoaded()
  {
    base.onLoaded();
  }
  [HideInInspector] public bool already_quited = false;
  public override void OnApplicationQuit() { Save_UI(); OnApplicationQuit_GS(); base.OnApplicationQuit(); already_quited = true; }
  public virtual void OnApplicationQuit_GS()
  {
    ABuff_OnApplicationQuit_GS();
  }
  public override void ui_to_data()
  {
    if (data == null) return;
    data.siUnits = siUnits;
    data.group_UI = group_UI;
  }
  public override void data_to_ui()
  {
    if (data == null) return;
    group_UI = data.group_UI;
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
    if (ValuesChanged) { gChanged = true; OnValueChanged(); ValuesChanged = UI_group_UI.Changed = false; }
    ABuff_LateUpdate1_GS();
    LateUpdate1_GS();
    if(lateUpdateI++ == uint_max) lateUpdateI = 100;
  }
  public virtual void LateUpdate0_GS()
  {
    ABuff_LateUpdate0_GS();
  }
  public virtual void LateUpdate1_GS()
  {
    ABuff_LateUpdate1_GS();
  }
  public override void Update()
  {
    base.Update();
    if (!ui_loaded) return;
    Update0_GS();
    ABuff_Update1_GS();
    Update1_GS();
  }
  public virtual void Update0_GS()
  {
    ABuff_Update0_GS();
  }
  public virtual void Update1_GS()
  {
    ABuff_Update1_GS();
  }
  public override void OnValueChanged()
  {
    if (!ui_loaded) return;
    OnValueChanged_GS();
  }
  public override void OnValueChanged_GS()
  {
    ABuff_OnValueChanged_GS();
    var type = "gsReport_Lib".ToType();
    if (type != null) ((GS)GetComponent(type))?.OnValueChanged_GS();

  }
  public string[] Scenes_in_Build => new string[] {  };
  public virtual uint itemN { get => g.itemN; set { if (g.itemN != value) { g.itemN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint ABuff_IndexN { get => g.ABuff_IndexN; set { if (g.ABuff_IndexN != value) { g.ABuff_IndexN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint ABuff_BitN { get => g.ABuff_BitN; set { if (g.ABuff_BitN != value) { g.ABuff_BitN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint ABuff_N { get => g.ABuff_N; set { if (g.ABuff_N != value) { g.ABuff_N = value; ValuesChanged = gChanged = true; } } }
  public virtual uint ABuff_BitN1 { get => g.ABuff_BitN1; set { if (g.ABuff_BitN1 != value) { g.ABuff_BitN1 = value; ValuesChanged = gChanged = true; } } }
  public virtual uint ABuff_BitN2 { get => g.ABuff_BitN2; set { if (g.ABuff_BitN2 != value) { g.ABuff_BitN2 = value; ValuesChanged = gChanged = true; } } }
  public bool group_UI { get => UI_group_UI?.v ?? false; set { if (UI_group_UI != null) UI_group_UI.v = value; } }
  public UI_TreeGroup UI_group_UI;
  public UI_method UI_RunBenchmark;
  public virtual void RunBenchmark() { }
  public UI_TreeGroup ui_group_UI => UI_group_UI;
  [Serializable]
  public class uiData
  {
    public bool siUnits, group_UI;
  }
  public virtual void InitBuffers()
  {
    InitBuffers0_GS();
    AllocData_gText2Num_Tutorial(1);
    InitKernels();
    SetKernelValues(gText2Num_Tutorial, nameof(gText2Num_Tutorial), kernel_parseText, kernel_ABuff_GetIndexes, kernel_ABuff_IncSums, kernel_ABuff_IncFills1, kernel_ABuff_GetFills2, kernel_ABuff_GetFills1, kernel_ABuff_Get_Bits_Sums, kernel_ABuff_GetSums, kernel_ABuff_Get_Bits);
    InitBuffers1_GS();
    isInitBuffers = true;
  }
  public virtual void InitBuffers0_GS()
  {
    ABuff_InitBuffers0_GS();
  }
  public virtual void InitBuffers1_GS()
  {
    ABuff_InitBuffers1_GS();
  }
  [HideInInspector] public uint[] ABuff_grp = new uint[1024];
  [HideInInspector] public uint[] ABuff_grp0 = new uint[1024];
  [Serializable]
  public struct GText2Num_Tutorial
  {
    public uint itemN, ABuff_IndexN, ABuff_BitN, ABuff_N, ABuff_BitN1, ABuff_BitN2;
  };
  public RWStructuredBuffer<GText2Num_Tutorial> gText2Num_Tutorial;
  public RWStructuredBuffer<uint> Text, ABuff_Bits, ABuff_Sums, ABuff_Indexes, ABuff_Fills1, ABuff_Fills2;
  public RWStructuredBuffer<int> ints;
  public virtual void AllocData_gText2Num_Tutorial(uint n) => AddComputeBuffer(ref gText2Num_Tutorial, nameof(gText2Num_Tutorial), n);
  public virtual void AllocData_Text(uint n) => AddComputeBuffer(ref Text, nameof(Text), n);
  public virtual void AssignData_Text(params uint[] data) => AddComputeBufferData(ref Text, nameof(Text), data);
  public virtual void AllocData_ints(uint n) => AddComputeBuffer(ref ints, nameof(ints), n);
  public virtual void AssignData_ints(params int[] data) => AddComputeBufferData(ref ints, nameof(ints), data);
  public virtual void AllocData_ABuff_Bits(uint n) => AddComputeBuffer(ref ABuff_Bits, nameof(ABuff_Bits), n);
  public virtual void AssignData_ABuff_Bits(params uint[] data) => AddComputeBufferData(ref ABuff_Bits, nameof(ABuff_Bits), data);
  public virtual void AllocData_ABuff_Sums(uint n) => AddComputeBuffer(ref ABuff_Sums, nameof(ABuff_Sums), n);
  public virtual void AssignData_ABuff_Sums(params uint[] data) => AddComputeBufferData(ref ABuff_Sums, nameof(ABuff_Sums), data);
  public virtual void AllocData_ABuff_Indexes(uint n) => AddComputeBuffer(ref ABuff_Indexes, nameof(ABuff_Indexes), n);
  public virtual void AssignData_ABuff_Indexes(params uint[] data) => AddComputeBufferData(ref ABuff_Indexes, nameof(ABuff_Indexes), data);
  public virtual void AllocData_ABuff_Fills1(uint n) => AddComputeBuffer(ref ABuff_Fills1, nameof(ABuff_Fills1), n);
  public virtual void AssignData_ABuff_Fills1(params uint[] data) => AddComputeBufferData(ref ABuff_Fills1, nameof(ABuff_Fills1), data);
  public virtual void AllocData_ABuff_Fills2(uint n) => AddComputeBuffer(ref ABuff_Fills2, nameof(ABuff_Fills2), n);
  public virtual void AssignData_ABuff_Fills2(params uint[] data) => AddComputeBufferData(ref ABuff_Fills2, nameof(ABuff_Fills2), data);
  public Texture2D _PaletteTex;
  public float4 palette(float v) => paletteColor(_PaletteTex, v);
  public float4 palette(float v, float w) => float4(palette(v).xyz, w);

  public virtual void onRenderObject_LIN(bool show, uint _itemN, ref uint i, ref uint index, ref uint3 LIN) { uint n = 0; if (show) { if (i < (n = _itemN)) LIN = uint3(index, i, 0); LIN.z += n; i -= n; } index++; }
  public virtual void onRenderObject_LIN(uint _itemN, ref uint i, ref uint index, ref uint3 LIN) { onRenderObject_LIN(true, _itemN, ref i, ref index, ref LIN); }
  public virtual uint3 onRenderObject_LIN(uint i) { uint3 LIN = u000; return LIN; }
  public virtual uint3 onRenderObject_LIN_Quads(uint i) { uint3 LIN = u000; return LIN; }
  public virtual uint3 onRenderObject_LIN_Points(uint i) { uint3 LIN = u000; return LIN; }
  [HideInInspector] public int kernel_parseText; [numthreads(numthreads1, 1, 1)] protected void parseText(uint3 id) { unchecked { if (id.x < itemN) parseText_GS(id); } }
  public virtual void parseText_GS(uint3 id) { }
  [HideInInspector] public int kernel_ABuff_GetIndexes; [numthreads(numthreads1, 1, 1)] protected void ABuff_GetIndexes(uint3 id) { unchecked { if (id.x < ABuff_BitN) ABuff_GetIndexes_GS(id); } }
  [HideInInspector] public int kernel_ABuff_IncSums; [numthreads(numthreads1, 1, 1)] protected void ABuff_IncSums(uint3 id) { unchecked { if (id.x < ABuff_BitN) ABuff_IncSums_GS(id); } }
  [HideInInspector] public int kernel_ABuff_IncFills1; [numthreads(numthreads1, 1, 1)] protected void ABuff_IncFills1(uint3 id) { unchecked { if (id.x < ABuff_BitN1) ABuff_IncFills1_GS(id); } }
  [HideInInspector] public int kernel_ABuff_GetFills2; [numthreads(numthreads1, 1, 1)] protected IEnumerator ABuff_GetFills2(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < ABuff_BitN2) yield return StartCoroutine(ABuff_GetFills2_GS(grp_tid, grp_id, id, grpI)); } }
  [HideInInspector] public int kernel_ABuff_GetFills1; [numthreads(numthreads1, 1, 1)] protected IEnumerator ABuff_GetFills1(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < ABuff_BitN1) yield return StartCoroutine(ABuff_GetFills1_GS(grp_tid, grp_id, id, grpI)); } }
  [HideInInspector] public int kernel_ABuff_Get_Bits_Sums; [numthreads(numthreads1, 1, 1)] protected IEnumerator ABuff_Get_Bits_Sums(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < ABuff_BitN) yield return StartCoroutine(ABuff_Get_Bits_Sums_GS(grp_tid, grp_id, id, grpI)); } }
  [HideInInspector] public int kernel_ABuff_GetSums; [numthreads(numthreads1, 1, 1)] protected IEnumerator ABuff_GetSums(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < ABuff_BitN) yield return StartCoroutine(ABuff_GetSums_GS(grp_tid, grp_id, id, grpI)); } }
  [HideInInspector] public int kernel_ABuff_Get_Bits; [numthreads(numthreads1, 1, 1)] protected void ABuff_Get_Bits(uint3 id) { unchecked { if (id.x < ABuff_BitN) ABuff_Get_Bits_GS(id); } }
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
  public virtual void onRenderObject_GS(ref bool render, ref bool cpu)
  {
    ABuff_onRenderObject_GS(ref render, ref cpu);
  }
  public virtual float4 frag_GS(v2f i, float4 color) => color;
  public virtual float4 frag(v2f i)
  {
    float4 color = i.color;
    color = frag_GS(i, color);
    if (color.a == 0) Discard(0);
    return color;
  }
  #region <ABuff>
  public void ABuff_print_Bits(string title) { StrBldr sb = StrBldr($"{title}, ABuff_Bits"); for (uint i = 0; i < ABuff_BitN; i++) sb.Add(" ", ABuff_Bits[i]); print(sb); }
  public void ABuff_print_Sums(string title) { StrBldr sb = StrBldr($"{title}, ABuff_Sums"); for (uint i = 0; i < ABuff_BitN; i++) sb.Add(" ", ABuff_Sums[i]); print(sb); }
  public void ABuff_print_Indexes(string title) { StrBldr sb = StrBldr($"{title}: ABuff_Indexes"); for (uint i = 0; i < ABuff_IndexN; i++) sb.Add(" ", ABuff_Indexes[i]); print(sb); }
  public virtual bool ABuff_IsBitOn(uint i) => i % 32 == 0;
  public void ABuff_SetN(uint n)
  {
    if (n > 2147450880) throw new Exception("gsABuff: ABuff_N > 2,147,450,880");
    ABuff_N = n; ABuff_BitN = ceilu(ABuff_N, 32); ABuff_BitN1 = ceilu(ABuff_BitN, numthreads1); ABuff_BitN2 = ceilu(ABuff_BitN1, numthreads1);
    AllocData_ABuff_Bits(ABuff_BitN); AllocData_ABuff_Fills1(ABuff_BitN1); AllocData_ABuff_Fills2(ABuff_BitN2); AllocData_ABuff_Sums(ABuff_BitN);
  }
  public void ABuff_FillPrefixes() { Gpu_ABuff_GetFills1(); Gpu_ABuff_GetFills2(); Gpu_ABuff_IncFills1(); Gpu_ABuff_IncSums(); }
  public void ABuff_getIndexes() { AllocData_ABuff_Indexes(ABuff_IndexN); Gpu_ABuff_GetIndexes(); }
  public void ABuff_FillIndexes() { ABuff_FillPrefixes(); ABuff_getIndexes(); }
  public virtual uint ABuff_Run(uint n) { ABuff_SetN(n); Gpu_ABuff_GetSums(); ABuff_FillIndexes(); return ABuff_IndexN; }
  public uint ABuff_Run(int n) => ABuff_Run((uint)n);
  public uint ABuff_Run(uint2 n) => ABuff_Run(cproduct(n)); public uint ABuff_Run(uint3 n) => ABuff_Run(cproduct(n));
  public uint ABuff_Run(int2 n) => ABuff_Run(cproduct(n)); public uint ABuff_Run(int3 n) => ABuff_Run(cproduct(n));
  public virtual void ABuff_Init_Bits(uint n, GS parent, string _N, string _BitN, ref RWStructuredBuffer<uint> _Bits) { ABuff_SetN(n); parent.SetValue(_N, ABuff_N); parent.SetValue(_BitN, ABuff_BitN); }
  public virtual void ABuff_Prefix_Sums() { Gpu_ABuff_Get_Bits_Sums(); ABuff_FillPrefixes(); }
  public virtual void ABuff_Bit_Indexes(RWStructuredBuffer<uint> bits, GS _this, string _IndexN, ref RWStructuredBuffer<uint> _Indexes) { ABuff_Prefix_Sums(); ABuff_getIndexes(); _this.SetValue(_IndexN, ABuff_IndexN); }
  public uint ABuff_Assign_Bits(uint i, uint j, uint bits) => bits | (Is(i < ABuff_N && ABuff_IsBitOn(i)) << (int)j);
  public virtual void ABuff_Get_Bits_GS(uint3 id) { uint i = id.x, j, k, bits = 0; if (i < ABuff_BitN) { for (j = 0, k = i * 32; j < 32; j++) bits = ABuff_Assign_Bits(k + j, j, bits); ABuff_Bits[i] = bits; } }
  public virtual IEnumerator ABuff_GetSums_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI)
  {
    uint i = id.x, c, s, j, k, bits = 0;
    if (i < ABuff_BitN) { for (j = 0, k = i * 32; j < 32; j++) bits = ABuff_Assign_Bits(k + j, j, bits); ABuff_Bits[i] = bits; c = countbits(bits); } else c = 0;
    ABuff_grp0[grpI] = c; ABuff_grp[grpI] = c; yield return GroupMemoryBarrierWithGroupSync();
    for (s = 1; s < numthreads1; s *= 2)
    {
      if (grpI >= s && i < ABuff_BitN) ABuff_grp[grpI] = ABuff_grp0[grpI] + ABuff_grp0[grpI - s]; yield return GroupMemoryBarrierWithGroupSync();
      ABuff_grp0[grpI] = ABuff_grp[grpI]; yield return GroupMemoryBarrierWithGroupSync();
    }
    if (i < ABuff_BitN) ABuff_Sums[i] = ABuff_grp[grpI];
  }
  public virtual IEnumerator ABuff_Get_Bits_Sums_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI)
  {
    uint i = id.x, c = i < ABuff_BitN ? countbits(ABuff_Bits[i]) : 0, s;
    ABuff_grp0[grpI] = c; ABuff_grp[grpI] = c; yield return GroupMemoryBarrierWithGroupSync();
    for (s = 1; s < numthreads1; s *= 2)
    {
      if (grpI >= s && i < ABuff_BitN) ABuff_grp[grpI] = ABuff_grp0[grpI] + ABuff_grp0[grpI - s]; yield return GroupMemoryBarrierWithGroupSync();
      ABuff_grp0[grpI] = ABuff_grp[grpI]; yield return GroupMemoryBarrierWithGroupSync();
    }
    if (i < ABuff_BitN) ABuff_Sums[i] = ABuff_grp[grpI];
  }
  public virtual IEnumerator ABuff_GetFills1_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI)
  {
    uint i = id.x, k = i + 1, j = k * numthreads1 - 1, c = i < ABuff_BitN1 - 1 ? ABuff_Sums[j] : i < ABuff_BitN1 ? ABuff_Sums[ABuff_BitN - 1] : 0, s;
    ABuff_grp0[grpI] = c; ABuff_grp[grpI] = c; yield return GroupMemoryBarrierWithGroupSync();
    for (s = 1; s < numthreads1; s *= 2)
    {
      if (grpI >= s && i < ABuff_BitN1) ABuff_grp[grpI] = ABuff_grp0[grpI] + ABuff_grp0[grpI - s]; yield return GroupMemoryBarrierWithGroupSync();
      ABuff_grp0[grpI] = ABuff_grp[grpI]; yield return GroupMemoryBarrierWithGroupSync();
    }
    if (i < ABuff_BitN1) ABuff_Fills1[i] = ABuff_grp[grpI];
  }
  public virtual IEnumerator ABuff_GetFills2_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI)
  {
    uint i = id.x, k = i + 1, j = k * numthreads1 - 1, c = i < ABuff_BitN2 - 1 ? ABuff_Fills1[j] : i < ABuff_BitN2 ? ABuff_Fills1[ABuff_BitN1 - 1] : 0, s;
    ABuff_grp0[grpI] = c; ABuff_grp[grpI] = c; yield return GroupMemoryBarrierWithGroupSync();
    for (s = 1; s < numthreads1; s *= 2)
    {
      if (grpI >= s && i < ABuff_BitN2) ABuff_grp[grpI] = ABuff_grp0[grpI] + ABuff_grp0[grpI - s]; yield return GroupMemoryBarrierWithGroupSync();
      ABuff_grp0[grpI] = ABuff_grp[grpI]; yield return GroupMemoryBarrierWithGroupSync();
    }
    if (i < ABuff_BitN2) ABuff_Fills2[i] = ABuff_grp[grpI];
  }
  public virtual void ABuff_IncFills1_GS(uint3 id) { uint i = id.x; if (i >= numthreads1) ABuff_Fills1[i] += ABuff_Fills2[i / numthreads1 - 1]; }
  public virtual void ABuff_IncSums_GS(uint3 id) { uint i = id.x; if (i >= numthreads1) ABuff_Sums[i] += ABuff_Fills1[i / numthreads1 - 1]; if (i == ABuff_BitN - 1) ABuff_IndexN = ABuff_Sums[i]; }
  public virtual void ABuff_GetIndexes_GS(uint3 id) { uint i = id.x, j, sum = i == 0 ? 0 : ABuff_Sums[i - 1], b, i32 = i << 5, k; for (k = 0, b = ABuff_Bits[i]; b > 0; k++) { j = (uint)findLSB(b); ABuff_Indexes[sum + k] = i32 + j; b = SetBitu(b, j, 0); } }

  public virtual void base_ABuff_Start0_GS() { }
  public virtual void base_ABuff_Start1_GS() { }
  public virtual void base_ABuff_OnApplicationQuit() { Save_UI(); OnApplicationQuit_GS(); base.OnApplicationQuit(); already_quited = true; }
  public virtual void base_ABuff_LateUpdate0_GS() { }
  public virtual void base_ABuff_LateUpdate1_GS() { }
  public virtual void base_ABuff_Update0_GS() { }
  public virtual void base_ABuff_Update1_GS() { }
  public virtual void base_ABuff_OnValueChanged_GS() { }
  public virtual void base_ABuff_InitBuffers0_GS() { }
  public virtual void base_ABuff_InitBuffers1_GS() { }
  [HideInInspector]
  public virtual void base_ABuff_onRenderObject_GS(ref bool render, ref bool cpu) { }
  public virtual float4 base_frag_ABuff_GS(v2f i, float4 color) { return color; }
  public virtual void ABuff_InitBuffers0_GS() { }
  public virtual void ABuff_InitBuffers1_GS() { }
  public virtual void ABuff_LateUpdate0_GS() { }
  public virtual void ABuff_LateUpdate1_GS() { }
  public virtual void ABuff_Update0_GS() { }
  public virtual void ABuff_Update1_GS() { }
  public virtual void ABuff_Start0_GS() { }
  public virtual void ABuff_Start1_GS() { }
  public virtual void ABuff_OnValueChanged_GS() { }
  public virtual void ABuff_OnApplicationQuit_GS() { }
  public virtual void ABuff_onRenderObject_GS(ref bool render, ref bool cpu) { }
  public virtual float4 frag_ABuff_GS(v2f i, float4 color)
  {
    return color;
  }
  #endregion <ABuff>
}