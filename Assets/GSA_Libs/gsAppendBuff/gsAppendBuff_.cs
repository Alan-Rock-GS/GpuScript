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
public class gsAppendBuff_ : GS
{
  [HideInInspector] public uiData data;
  [HideInInspector] public GAppendBuff g; public GAppendBuff G { get { gAppendBuff.GetData(); return gAppendBuff[0]; } }
  public void g_SetData() { if (gChanged && gAppendBuff != null) { gAppendBuff[0] = g; gAppendBuff.SetData(); gChanged = false; } }
  public virtual void Bits_SetKernels(bool reallocate = false) { if (Bits != null && (reallocate || Bits.reallocated)) { SetKernelValues(Bits, nameof(Bits), kernel_GetIndexes, kernel_Get_Bits_Sums, kernel_GetSums, kernel_Get_Bits); Bits.reallocated = false; } }
  public virtual void Sums_SetKernels(bool reallocate = false) { if (Sums != null && (reallocate || Sums.reallocated)) { SetKernelValues(Sums, nameof(Sums), kernel_GetIndexes, kernel_IncSums, kernel_GetFills1, kernel_Get_Bits_Sums, kernel_GetSums); Sums.reallocated = false; } }
  public virtual void Indexes_SetKernels(bool reallocate = false) { if (Indexes != null && (reallocate || Indexes.reallocated)) { SetKernelValues(Indexes, nameof(Indexes), kernel_GetIndexes); Indexes.reallocated = false; } }
  public virtual void Fills1_SetKernels(bool reallocate = false) { if (Fills1 != null && (reallocate || Fills1.reallocated)) { SetKernelValues(Fills1, nameof(Fills1), kernel_IncSums, kernel_IncFills1, kernel_GetFills2, kernel_GetFills1); Fills1.reallocated = false; } }
  public virtual void Fills2_SetKernels(bool reallocate = false) { if (Fills2 != null && (reallocate || Fills2.reallocated)) { SetKernelValues(Fills2, nameof(Fills2), kernel_IncFills1, kernel_GetFills2); Fills2.reallocated = false; } }
  public virtual void Gpu_GetIndexes() { g_SetData(); Bits?.SetCpu(); Bits_SetKernels(); Sums?.SetCpu(); Sums_SetKernels(); Indexes_SetKernels(); Gpu(kernel_GetIndexes, GetIndexes, BitN); Indexes?.ResetWrite(); }
  public virtual void Cpu_GetIndexes() { Bits?.GetGpu(); Sums?.GetGpu(); Indexes?.GetGpu(); Cpu(GetIndexes, BitN); Indexes.SetData(); }
  public virtual void Cpu_GetIndexes(uint3 id) { Bits?.GetGpu(); Sums?.GetGpu(); Indexes?.GetGpu(); GetIndexes(id); Indexes.SetData(); }
  public virtual void Gpu_IncSums() { g_SetData(); Sums?.SetCpu(); Sums_SetKernels(); Fills1?.SetCpu(); Fills1_SetKernels(); gAppendBuff?.SetCpu(); Gpu(kernel_IncSums, IncSums, BitN); Sums?.ResetWrite(); g = G; }
  public virtual void Cpu_IncSums() { Sums?.GetGpu(); Fills1?.GetGpu(); Cpu(IncSums, BitN); Sums.SetData(); }
  public virtual void Cpu_IncSums(uint3 id) { Sums?.GetGpu(); Fills1?.GetGpu(); IncSums(id); Sums.SetData(); }
  public virtual void Gpu_IncFills1() { g_SetData(); Fills1?.SetCpu(); Fills1_SetKernels(); Fills2?.SetCpu(); Fills2_SetKernels(); Gpu(kernel_IncFills1, IncFills1, BitN1); Fills1?.ResetWrite(); }
  public virtual void Cpu_IncFills1() { Fills1?.GetGpu(); Fills2?.GetGpu(); Cpu(IncFills1, BitN1); Fills1.SetData(); }
  public virtual void Cpu_IncFills1(uint3 id) { Fills1?.GetGpu(); Fills2?.GetGpu(); IncFills1(id); Fills1.SetData(); }
  public virtual void Gpu_GetFills2() { g_SetData(); Fills1?.SetCpu(); Fills1_SetKernels(); Fills2_SetKernels(); Gpu(kernel_GetFills2, GetFills2, BitN2); Fills2?.ResetWrite(); }
  public virtual IEnumerator Cpu_GetFills2() { Fills1?.GetGpu(); Fills2?.GetGpu(); yield return StartCoroutine(Cpu_Coroutine(kernel_GetFills2, GetFills2, BitN2)); }
  public virtual void Cpu_GetFills2(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { Fills1?.GetGpu(); Fills2?.GetGpu(); GetFills2(grp_tid, grp_id, id, grpI); Fills2.SetData(); }
  public virtual void Gpu_GetFills1() { g_SetData(); Sums?.SetCpu(); Sums_SetKernels(); Fills1_SetKernels(); Gpu(kernel_GetFills1, GetFills1, BitN1); Fills1?.ResetWrite(); }
  public virtual IEnumerator Cpu_GetFills1() { Sums?.GetGpu(); Fills1?.GetGpu(); yield return StartCoroutine(Cpu_Coroutine(kernel_GetFills1, GetFills1, BitN1)); }
  public virtual void Cpu_GetFills1(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { Sums?.GetGpu(); Fills1?.GetGpu(); GetFills1(grp_tid, grp_id, id, grpI); Fills1.SetData(); }
  public virtual void Gpu_Get_Bits_Sums() { g_SetData(); Bits?.SetCpu(); Bits_SetKernels(); Sums_SetKernels(); Gpu(kernel_Get_Bits_Sums, Get_Bits_Sums, BitN); Sums?.ResetWrite(); }
  public virtual IEnumerator Cpu_Get_Bits_Sums() { Bits?.GetGpu(); Sums?.GetGpu(); yield return StartCoroutine(Cpu_Coroutine(kernel_Get_Bits_Sums, Get_Bits_Sums, BitN)); }
  public virtual void Cpu_Get_Bits_Sums(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { Bits?.GetGpu(); Sums?.GetGpu(); Get_Bits_Sums(grp_tid, grp_id, id, grpI); Sums.SetData(); }
  public virtual void Gpu_GetSums() { g_SetData(); Bits_SetKernels(); Sums_SetKernels(); Gpu(kernel_GetSums, GetSums, BitN); Bits?.ResetWrite(); Sums?.ResetWrite(); }
  public virtual IEnumerator Cpu_GetSums() { Bits?.GetGpu(); Sums?.GetGpu(); yield return StartCoroutine(Cpu_Coroutine(kernel_GetSums, GetSums, BitN)); }
  public virtual void Cpu_GetSums(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { Bits?.GetGpu(); Sums?.GetGpu(); GetSums(grp_tid, grp_id, id, grpI); Bits.SetData(); Sums.SetData(); }
  public virtual void Gpu_Get_Bits() { g_SetData(); Bits_SetKernels(); Gpu(kernel_Get_Bits, Get_Bits, BitN); Bits?.ResetWrite(); }
  public virtual void Cpu_Get_Bits() { Bits?.GetGpu(); Cpu(Get_Bits, BitN); Bits.SetData(); }
  public virtual void Cpu_Get_Bits(uint3 id) { Bits?.GetGpu(); Get_Bits(id); Bits.SetData(); }
  [HideInInspector] public bool ValuesChanged, gChanged;
  public static gsAppendBuff This;
  public virtual void Awake() { This = this as gsAppendBuff; Awake_GS(); }
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
  }
  public override void data_to_ui()
  {
    if (data == null) return;
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
  public override void OnValueChanged_GS() { }
  public string[] Scenes_in_Build => new string[] {  };
  public virtual uint IndexN { get => g.IndexN; set { if (g.IndexN != value) { g.IndexN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint BitN { get => g.BitN; set { if (g.BitN != value) { g.BitN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint N { get => g.N; set { if (g.N != value) { g.N = value; ValuesChanged = gChanged = true; } } }
  public virtual uint BitN1 { get => g.BitN1; set { if (g.BitN1 != value) { g.BitN1 = value; ValuesChanged = gChanged = true; } } }
  public virtual uint BitN2 { get => g.BitN2; set { if (g.BitN2 != value) { g.BitN2 = value; ValuesChanged = gChanged = true; } } }
  [Serializable]
  public class uiData
  {
    public bool siUnits;
  }
  public virtual void InitBuffers()
  {
    InitBuffers0_GS();
    AddComputeBuffer(ref gAppendBuff, nameof(gAppendBuff), 1);
    InitKernels();
    SetKernelValues(gAppendBuff, nameof(gAppendBuff), kernel_GetIndexes, kernel_IncSums, kernel_IncFills1, kernel_GetFills2, kernel_GetFills1, kernel_Get_Bits_Sums, kernel_GetSums, kernel_Get_Bits);
    InitBuffers1_GS();
    isInitBuffers = true;
  }
  public virtual void InitBuffers0_GS() { }
  public virtual void InitBuffers1_GS() { }
  [HideInInspector] public uint[] grp = new uint[1024];
  [HideInInspector] public uint[] grp0 = new uint[1024];
  [Serializable]
  public struct GAppendBuff
  {
    public uint IndexN, BitN, N, BitN1, BitN2;
  };
  public RWStructuredBuffer<GAppendBuff> gAppendBuff;
  public RWStructuredBuffer<uint> Bits, Sums, Indexes, Fills1, Fills2;
  public virtual void AllocData_Bits(uint n) => AddComputeBuffer(ref Bits, nameof(Bits), n);
  public virtual void AssignData_Bits(params uint[] data) => AddComputeBufferData(ref Bits, nameof(Bits), data);
  public virtual void AllocData_Sums(uint n) => AddComputeBuffer(ref Sums, nameof(Sums), n);
  public virtual void AssignData_Sums(params uint[] data) => AddComputeBufferData(ref Sums, nameof(Sums), data);
  public virtual void AllocData_Indexes(uint n) => AddComputeBuffer(ref Indexes, nameof(Indexes), n);
  public virtual void AssignData_Indexes(params uint[] data) => AddComputeBufferData(ref Indexes, nameof(Indexes), data);
  public virtual void AllocData_Fills1(uint n) => AddComputeBuffer(ref Fills1, nameof(Fills1), n);
  public virtual void AssignData_Fills1(params uint[] data) => AddComputeBufferData(ref Fills1, nameof(Fills1), data);
  public virtual void AllocData_Fills2(uint n) => AddComputeBuffer(ref Fills2, nameof(Fills2), n);
  public virtual void AssignData_Fills2(params uint[] data) => AddComputeBufferData(ref Fills2, nameof(Fills2), data);
  public Texture2D _PaletteTex;
  public float4 palette(float v) => paletteColor(_PaletteTex, v);
  public float4 palette(float v, float w) => float4(palette(v).xyz, w);

  public virtual void onRenderObject_LIN(bool show, uint _itemN, ref uint i, ref uint index, ref uint3 LIN) { uint n = 0; if (show) { if (i < (n = _itemN)) LIN = uint3(index, i, 0); LIN.z += n; i -= n; } index++; }
  public virtual void onRenderObject_LIN(uint _itemN, ref uint i, ref uint index, ref uint3 LIN) { onRenderObject_LIN(true, _itemN, ref i, ref index, ref LIN); }
  public virtual uint3 onRenderObject_LIN(uint i) { uint3 LIN = u000; return LIN; }
  public virtual uint3 onRenderObject_LIN_Quads(uint i) { uint3 LIN = u000; return LIN; }
  public virtual uint3 onRenderObject_LIN_Points(uint i) { uint3 LIN = u000; return LIN; }
  [HideInInspector] public int kernel_GetIndexes; [numthreads(numthreads1, 1, 1)] protected void GetIndexes(uint3 id) { unchecked { if (id.x < BitN) GetIndexes_GS(id); } }
  public virtual void GetIndexes_GS(uint3 id) { }
  [HideInInspector] public int kernel_IncSums; [numthreads(numthreads1, 1, 1)] protected void IncSums(uint3 id) { unchecked { if (id.x < BitN) IncSums_GS(id); } }
  public virtual void IncSums_GS(uint3 id) { }
  [HideInInspector] public int kernel_IncFills1; [numthreads(numthreads1, 1, 1)] protected void IncFills1(uint3 id) { unchecked { if (id.x < BitN1) IncFills1_GS(id); } }
  public virtual void IncFills1_GS(uint3 id) { }
  [HideInInspector] public int kernel_GetFills2; [numthreads(numthreads1, 1, 1)] protected IEnumerator GetFills2(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < BitN2) yield return StartCoroutine(GetFills2_GS(grp_tid, grp_id, id, grpI)); } }
  public virtual IEnumerator GetFills2_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { yield return null; }
  [HideInInspector] public int kernel_GetFills1; [numthreads(numthreads1, 1, 1)] protected IEnumerator GetFills1(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < BitN1) yield return StartCoroutine(GetFills1_GS(grp_tid, grp_id, id, grpI)); } }
  public virtual IEnumerator GetFills1_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { yield return null; }
  [HideInInspector] public int kernel_Get_Bits_Sums; [numthreads(numthreads1, 1, 1)] protected IEnumerator Get_Bits_Sums(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < BitN) yield return StartCoroutine(Get_Bits_Sums_GS(grp_tid, grp_id, id, grpI)); } }
  public virtual IEnumerator Get_Bits_Sums_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { yield return null; }
  [HideInInspector] public int kernel_GetSums; [numthreads(numthreads1, 1, 1)] protected IEnumerator GetSums(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < BitN) yield return StartCoroutine(GetSums_GS(grp_tid, grp_id, id, grpI)); } }
  public virtual IEnumerator GetSums_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { yield return null; }
  [HideInInspector] public int kernel_Get_Bits; [numthreads(numthreads1, 1, 1)] protected void Get_Bits(uint3 id) { unchecked { if (id.x < BitN) Get_Bits_GS(id); } }
  public virtual void Get_Bits_GS(uint3 id) { }
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