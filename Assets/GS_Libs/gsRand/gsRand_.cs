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
public class gsRand_ : GS
{
  [HideInInspector] public uiData data;
  [HideInInspector] public GRand g; public GRand G { get { gRand.GetData(); return gRand[0]; } }
  public void g_SetData() { if (gChanged && gRand != null) { gRand[0] = g; gRand.SetData(); gChanged = false; } }
  public virtual void rs_SetKernels(bool reallocate = false) { if (rs != null && (reallocate || rs.reallocated)) { SetKernelValues(rs, nameof(rs), kernel_initState, kernel_initSeed); rs.reallocated = false; } }
  public virtual void Gpu_initState() { g_SetData(); rs?.SetCpu(); rs_SetKernels(); Gpu(kernel_initState, initState, I); rs?.ResetWrite(); }
  public virtual void Cpu_initState() { rs?.GetGpu(); Cpu(initState, I); rs.SetData(); }
  public virtual void Cpu_initState(uint3 id) { rs?.GetGpu(); initState(id); rs.SetData(); }
  public virtual void Gpu_initSeed() { g_SetData(); rs_SetKernels(); Gpu(kernel_initSeed, initSeed, N); rs?.ResetWrite(); }
  public virtual void Cpu_initSeed() { rs?.GetGpu(); Cpu(initSeed, N); rs.SetData(); }
  public virtual void Cpu_initSeed(uint3 id) { rs?.GetGpu(); initSeed(id); rs.SetData(); }
  public virtual void Gpu_grp_init_1M() { g_SetData(); Gpu(kernel_grp_init_1M, grp_init_1M, N / 1024 / 1024); }
  public virtual IEnumerator Cpu_grp_init_1M() { yield return StartCoroutine(Cpu_Coroutine(kernel_grp_init_1M, grp_init_1M, N / 1024 / 1024)); }
  public virtual void Cpu_grp_init_1M(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { grp_init_1M(grp_tid, grp_id, id, grpI); }
  public virtual void Gpu_grp_init_1K() { g_SetData(); Gpu(kernel_grp_init_1K, grp_init_1K, N / 1024); }
  public virtual IEnumerator Cpu_grp_init_1K() { yield return StartCoroutine(Cpu_Coroutine(kernel_grp_init_1K, grp_init_1K, N / 1024)); }
  public virtual void Cpu_grp_init_1K(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { grp_init_1K(grp_tid, grp_id, id, grpI); }
  public virtual void Gpu_grp_fill_1K() { g_SetData(); Gpu(kernel_grp_fill_1K, grp_fill_1K, N); }
  public virtual IEnumerator Cpu_grp_fill_1K() { yield return StartCoroutine(Cpu_Coroutine(kernel_grp_fill_1K, grp_fill_1K, N)); }
  public virtual void Cpu_grp_fill_1K(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { grp_fill_1K(grp_tid, grp_id, id, grpI); }
  [HideInInspector] public bool ValuesChanged, gChanged;
  public static gsRand This;
  public virtual void Awake() { This = this as gsRand; Awake_GS(); }
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
  public string[] Scenes_in_Build => new string[] { "Assets/GS_Docs/gsADraw_Doc/ADraw_Doc.unity" };
  public virtual uint N { get => g.N; set { if (g.N != value) { g.N = value; ValuesChanged = gChanged = true; } } }
  public virtual uint I { get => g.I; set { if (g.I != value) { g.I = value; ValuesChanged = gChanged = true; } } }
  public virtual uint J { get => g.J; set { if (g.J != value) { g.J = value; ValuesChanged = gChanged = true; } } }
  public virtual uint4 seed4 { get => g.seed4; set { if (any(g.seed4 != value)) { g.seed4 = value; ValuesChanged = gChanged = true; } } }
  [Serializable]
  public class uiData
  {
    public bool siUnits;
  }
  public virtual void InitBuffers()
  {
    InitBuffers0_GS();
    AllocData_gRand(1);
    InitKernels();
    SetKernelValues(gRand, nameof(gRand), kernel_initState, kernel_initSeed, kernel_grp_init_1M, kernel_grp_init_1K, kernel_grp_fill_1K);
    AllocData_rs(N);
    InitBuffers1_GS();
    isInitBuffers = true;
  }
  public virtual void InitBuffers0_GS() { }
  public virtual void InitBuffers1_GS() { }
  [HideInInspector] public uint4[] grp = new uint4[1024];
  [Serializable]
  public struct GRand
  {
    public uint N, I, J;
    public uint4 seed4;
  };
  public RWStructuredBuffer<GRand> gRand;
  public RWStructuredBuffer<uint4> rs;
  public virtual void AllocData_gRand(uint n) => AddComputeBuffer(ref gRand, nameof(gRand), n);
  public virtual void AllocData_rs(uint n) => AddComputeBuffer(ref rs, nameof(rs), n);
  public virtual void AssignData_rs(params uint4[] data) => AddComputeBufferData(ref rs, nameof(rs), data);
  public Texture2D _PaletteTex;
  public float4 palette(float v) => paletteColor(_PaletteTex, v);
  public float4 palette(float v, float w) => float4(palette(v).xyz, w);

  public virtual void onRenderObject_LIN(bool show, uint _itemN, ref uint i, ref uint index, ref uint3 LIN) { uint n = 0; if (show) { if (i < (n = _itemN)) LIN = uint3(index, i, 0); LIN.z += n; i -= n; } index++; }
  public virtual void onRenderObject_LIN(uint _itemN, ref uint i, ref uint index, ref uint3 LIN) { onRenderObject_LIN(true, _itemN, ref i, ref index, ref LIN); }
  public virtual uint3 onRenderObject_LIN(uint i) { uint3 LIN = u000; return LIN; }
  public virtual uint3 onRenderObject_LIN_Quads(uint i) { uint3 LIN = u000; return LIN; }
  public virtual uint3 onRenderObject_LIN_Points(uint i) { uint3 LIN = u000; return LIN; }
  [HideInInspector] public int kernel_initState; [numthreads(numthreads1, 1, 1)] protected void initState(uint3 id) { unchecked { if (id.x < I) initState_GS(id); } }
  public virtual void initState_GS(uint3 id) { }
  [HideInInspector] public int kernel_initSeed; [numthreads(numthreads1, 1, 1)] protected void initSeed(uint3 id) { unchecked { if (id.x < N) initSeed_GS(id); } }
  public virtual void initSeed_GS(uint3 id) { }
  [HideInInspector] public int kernel_grp_init_1M; [numthreads(numthreads1, 1, 1)] protected IEnumerator grp_init_1M(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < N / 1024 / 1024) yield return StartCoroutine(grp_init_1M_GS(grp_tid, grp_id, id, grpI)); } }
  public virtual IEnumerator grp_init_1M_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { yield return null; }
  [HideInInspector] public int kernel_grp_init_1K; [numthreads(numthreads1, 1, 1)] protected IEnumerator grp_init_1K(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < N / 1024) yield return StartCoroutine(grp_init_1K_GS(grp_tid, grp_id, id, grpI)); } }
  public virtual IEnumerator grp_init_1K_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { yield return null; }
  [HideInInspector] public int kernel_grp_fill_1K; [numthreads(numthreads1, 1, 1)] protected IEnumerator grp_fill_1K(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < N) yield return StartCoroutine(grp_fill_1K_GS(grp_tid, grp_id, id, grpI)); } }
  public virtual IEnumerator grp_fill_1K_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { yield return null; }
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