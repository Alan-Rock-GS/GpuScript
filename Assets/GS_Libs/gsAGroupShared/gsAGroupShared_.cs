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
public class gsAGroupShared_ : GS
{
  [HideInInspector] public uiData data;
  [HideInInspector] public GAGroupShared g; public GAGroupShared G { get { gAGroupShared.GetData(); return gAGroupShared[0]; } }
  public void g_SetData() { if (gChanged && gAGroupShared != null) { gAGroupShared[0] = g; gAGroupShared.SetData(); gChanged = false; } }
  public virtual void Bits_SetKernels(bool reallocate = false) { if (Bits != null && (reallocate || Bits.reallocated)) { SetKernelValues(Bits, nameof(Bits), kernel_CalcIndexes, kernel_CalcSums, kernel_InitSums, kernel_Get_Bits_32, kernel_Init_Bits_32, kernel_Get_Bits); Bits.reallocated = false; } }
  public virtual void Sums_SetKernels(bool reallocate = false) { if (Sums != null && (reallocate || Sums.reallocated)) { SetKernelValues(Sums, nameof(Sums), kernel_CalcIndexes, kernel_Add_ColN_Sums, kernel_Calc_ColN_Sums, kernel_Init_ColN_Sums, kernel_CalcSums, kernel_InitSums); Sums.reallocated = false; } }
  public virtual void Indexes_SetKernels(bool reallocate = false) { if (Indexes != null && (reallocate || Indexes.reallocated)) { SetKernelValues(Indexes, nameof(Indexes), kernel_CalcIndexes); Indexes.reallocated = false; } }
  public virtual void ColN_Sums_SetKernels(bool reallocate = false) { if (ColN_Sums != null && (reallocate || ColN_Sums.reallocated)) { SetKernelValues(ColN_Sums, nameof(ColN_Sums), kernel_Add_ColN_Sums, kernel_Calc_ColN_Sums, kernel_Init_ColN_Sums); ColN_Sums.reallocated = false; } }
  public virtual void Gpu_CalcIndexes() { g_SetData(); Bits?.SetCpu(); Bits_SetKernels(); Sums?.SetCpu(); Sums_SetKernels(); Indexes_SetKernels(); Gpu(kernel_CalcIndexes, CalcIndexes, BitN); Indexes?.ResetWrite(); }
  public virtual void Cpu_CalcIndexes() { Bits?.GetGpu(); Sums?.GetGpu(); Indexes?.GetGpu(); Cpu(CalcIndexes, BitN); Indexes.SetData(); }
  public virtual void Cpu_CalcIndexes(uint3 id) { Bits?.GetGpu(); Sums?.GetGpu(); Indexes?.GetGpu(); CalcIndexes(id); Indexes.SetData(); }
  public virtual void Gpu_Add_ColN_Sums() { g_SetData(); Sums?.SetCpu(); Sums_SetKernels(); ColN_Sums?.SetCpu(); ColN_Sums_SetKernels(); Gpu(kernel_Add_ColN_Sums, Add_ColN_Sums, uint2(BitRowN - 1, BitColN)); Sums?.ResetWrite(); }
  public virtual void Cpu_Add_ColN_Sums() { Sums?.GetGpu(); ColN_Sums?.GetGpu(); Cpu(Add_ColN_Sums, uint2(BitRowN - 1, BitColN)); Sums.SetData(); }
  public virtual void Cpu_Add_ColN_Sums(uint3 id) { Sums?.GetGpu(); ColN_Sums?.GetGpu(); Add_ColN_Sums(id); Sums.SetData(); }
  public virtual void Gpu_Calc_ColN_Sums() { g_SetData(); Sums?.SetCpu(); Sums_SetKernels(); ColN_Sums?.SetCpu(); ColN_Sums_SetKernels(); Gpu(kernel_Calc_ColN_Sums, Calc_ColN_Sums, BitRowN * (BitRowN - 1) / 2); ColN_Sums?.ResetWrite(); }
  public virtual void Cpu_Calc_ColN_Sums() { Sums?.GetGpu(); ColN_Sums?.GetGpu(); Cpu(Calc_ColN_Sums, BitRowN * (BitRowN - 1) / 2); ColN_Sums.SetData(); }
  public virtual void Cpu_Calc_ColN_Sums(uint3 id) { Sums?.GetGpu(); ColN_Sums?.GetGpu(); Calc_ColN_Sums(id); ColN_Sums.SetData(); }
  public virtual void Gpu_Init_ColN_Sums() { g_SetData(); Sums?.SetCpu(); Sums_SetKernels(); ColN_Sums_SetKernels(); Gpu(kernel_Init_ColN_Sums, Init_ColN_Sums, BitRowN); ColN_Sums?.ResetWrite(); }
  public virtual void Cpu_Init_ColN_Sums() { Sums?.GetGpu(); ColN_Sums?.GetGpu(); Cpu(Init_ColN_Sums, BitRowN); ColN_Sums.SetData(); }
  public virtual void Cpu_Init_ColN_Sums(uint3 id) { Sums?.GetGpu(); ColN_Sums?.GetGpu(); Init_ColN_Sums(id); ColN_Sums.SetData(); }
  public virtual void Gpu_CalcSums() { g_SetData(); Bits?.SetCpu(); Bits_SetKernels(); Sums?.SetCpu(); Sums_SetKernels(); Gpu(kernel_CalcSums, CalcSums, uint2(BitRowN, BitColN * (BitColN - 1) / 2)); Bits?.ResetWrite(); Sums?.ResetWrite(); }
  public virtual void Cpu_CalcSums() { Bits?.GetGpu(); Sums?.GetGpu(); Cpu(CalcSums, uint2(BitRowN, BitColN * (BitColN - 1) / 2)); Bits.SetData(); Sums.SetData(); }
  public virtual void Cpu_CalcSums(uint3 id) { Bits?.GetGpu(); Sums?.GetGpu(); CalcSums(id); Bits.SetData(); Sums.SetData(); }
  public virtual void Gpu_InitSums() { g_SetData(); Bits?.SetCpu(); Bits_SetKernels(); Sums_SetKernels(); Gpu(kernel_InitSums, InitSums, BitN); Sums?.ResetWrite(); }
  public virtual void Cpu_InitSums() { Bits?.GetGpu(); Sums?.GetGpu(); Cpu(InitSums, BitN); Sums.SetData(); }
  public virtual void Cpu_InitSums(uint3 id) { Bits?.GetGpu(); Sums?.GetGpu(); InitSums(id); Sums.SetData(); }
  public virtual void Gpu_Get_Bits_32() { g_SetData(); Bits?.SetCpu(); Bits_SetKernels(); Gpu(kernel_Get_Bits_32, Get_Bits_32, uint2(BitN, 32)); Bits?.ResetWrite(); }
  public virtual void Cpu_Get_Bits_32() { Bits?.GetGpu(); Cpu(Get_Bits_32, uint2(BitN, 32)); Bits.SetData(); }
  public virtual void Cpu_Get_Bits_32(uint3 id) { Bits?.GetGpu(); Get_Bits_32(id); Bits.SetData(); }
  public virtual void Gpu_Init_Bits_32() { g_SetData(); Bits_SetKernels(); Gpu(kernel_Init_Bits_32, Init_Bits_32, BitN); Bits?.ResetWrite(); }
  public virtual void Cpu_Init_Bits_32() { Bits?.GetGpu(); Cpu(Init_Bits_32, BitN); Bits.SetData(); }
  public virtual void Cpu_Init_Bits_32(uint3 id) { Bits?.GetGpu(); Init_Bits_32(id); Bits.SetData(); }
  public virtual void Gpu_Get_Bits() { g_SetData(); Bits_SetKernels(); Gpu(kernel_Get_Bits, Get_Bits, BitN); Bits?.ResetWrite(); }
  public virtual void Cpu_Get_Bits() { Bits?.GetGpu(); Cpu(Get_Bits, BitN); Bits.SetData(); }
  public virtual void Cpu_Get_Bits(uint3 id) { Bits?.GetGpu(); Get_Bits(id); Bits.SetData(); }
  [HideInInspector] public bool ValuesChanged, gChanged;
  public static gsAGroupShared This;
  public virtual void Awake() { This = this as gsAGroupShared; Awake_GS(); }
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
  public virtual uint IndexN { get => g.IndexN; set { if (g.IndexN != value) { g.IndexN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint BitN { get => g.BitN; set { if (g.BitN != value) { g.BitN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint N { get => g.N; set { if (g.N != value) { g.N = value; ValuesChanged = gChanged = true; } } }
  public virtual uint BitRowN { get => g.BitRowN; set { if (g.BitRowN != value) { g.BitRowN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint BitColN { get => g.BitColN; set { if (g.BitColN != value) { g.BitColN = value; ValuesChanged = gChanged = true; } } }
  [Serializable]
  public class uiData
  {
    public bool siUnits;
  }
  public virtual void InitBuffers()
  {
    InitBuffers0_GS();
    AllocData_gAGroupShared(1);
    InitKernels();
    SetKernelValues(gAGroupShared, nameof(gAGroupShared), kernel_CalcIndexes, kernel_Add_ColN_Sums, kernel_Calc_ColN_Sums, kernel_Init_ColN_Sums, kernel_CalcSums, kernel_InitSums, kernel_Get_Bits_32, kernel_Init_Bits_32, kernel_Get_Bits);
    InitBuffers1_GS();
    isInitBuffers = true;
  }
  public virtual void InitBuffers0_GS() { }
  public virtual void InitBuffers1_GS() { }
  [HideInInspector] public uint[] grp = new uint[1024];
  [HideInInspector] public uint[] grp0 = new uint[1024];
  [HideInInspector] public float2[] grpf2 = new float2[1024];
  [Serializable]
  public struct GAGroupShared
  {
    public uint IndexN, BitN, N, BitRowN, BitColN;
  };
  public RWStructuredBuffer<GAGroupShared> gAGroupShared;
  public RWStructuredBuffer<uint> Bits, Sums, Indexes, ColN_Sums;
  public virtual void AllocData_gAGroupShared(uint n) => AddComputeBuffer(ref gAGroupShared, nameof(gAGroupShared), n);
  public virtual void AllocData_Bits(uint n) => AddComputeBuffer(ref Bits, nameof(Bits), n);
  public virtual void AssignData_Bits(params uint[] data) => AddComputeBufferData(ref Bits, nameof(Bits), data);
  public virtual void AllocData_Sums(uint n) => AddComputeBuffer(ref Sums, nameof(Sums), n);
  public virtual void AssignData_Sums(params uint[] data) => AddComputeBufferData(ref Sums, nameof(Sums), data);
  public virtual void AllocData_Indexes(uint n) => AddComputeBuffer(ref Indexes, nameof(Indexes), n);
  public virtual void AssignData_Indexes(params uint[] data) => AddComputeBufferData(ref Indexes, nameof(Indexes), data);
  public virtual void AllocData_ColN_Sums(uint n) => AddComputeBuffer(ref ColN_Sums, nameof(ColN_Sums), n);
  public virtual void AssignData_ColN_Sums(params uint[] data) => AddComputeBufferData(ref ColN_Sums, nameof(ColN_Sums), data);
  public Texture2D _PaletteTex;
  public float4 palette(float v) => paletteColor(_PaletteTex, v);
  public float4 palette(float v, float w) => float4(palette(v).xyz, w);

  public virtual void onRenderObject_LIN(bool show, uint _itemN, ref uint i, ref uint index, ref uint3 LIN) { uint n = 0; if (show) { if (i < (n = _itemN)) LIN = uint3(index, i, 0); LIN.z += n; i -= n; } index++; }
  public virtual void onRenderObject_LIN(uint _itemN, ref uint i, ref uint index, ref uint3 LIN) { onRenderObject_LIN(true, _itemN, ref i, ref index, ref LIN); }
  public virtual uint3 onRenderObject_LIN(uint i) { uint3 LIN = u000; return LIN; }
  public virtual uint3 onRenderObject_LIN_Quads(uint i) { uint3 LIN = u000; return LIN; }
  public virtual uint3 onRenderObject_LIN_Points(uint i) { uint3 LIN = u000; return LIN; }
  [HideInInspector] public int kernel_CalcIndexes; [numthreads(numthreads1, 1, 1)] protected void CalcIndexes(uint3 id) { unchecked { if (id.x < BitN) CalcIndexes_GS(id); } }
  public virtual void CalcIndexes_GS(uint3 id) { }
  [HideInInspector] public int kernel_Add_ColN_Sums; [numthreads(numthreads2, numthreads2, 1)] protected void Add_ColN_Sums(uint3 id) { unchecked { if (id.y < BitColN && id.x < BitRowN - 1) Add_ColN_Sums_GS(id); } }
  public virtual void Add_ColN_Sums_GS(uint3 id) { }
  [HideInInspector] public int kernel_Calc_ColN_Sums; [numthreads(numthreads1, 1, 1)] protected void Calc_ColN_Sums(uint3 id) { unchecked { if (id.x < BitRowN * (BitRowN - 1) / 2) Calc_ColN_Sums_GS(id); } }
  public virtual void Calc_ColN_Sums_GS(uint3 id) { }
  [HideInInspector] public int kernel_Init_ColN_Sums; [numthreads(numthreads1, 1, 1)] protected void Init_ColN_Sums(uint3 id) { unchecked { if (id.x < BitRowN) Init_ColN_Sums_GS(id); } }
  public virtual void Init_ColN_Sums_GS(uint3 id) { }
  [HideInInspector] public int kernel_CalcSums; [numthreads(numthreads2, numthreads2, 1)] protected void CalcSums(uint3 id) { unchecked { if (id.y < BitColN * (BitColN - 1) / 2 && id.x < BitRowN) CalcSums_GS(id); } }
  public virtual void CalcSums_GS(uint3 id) { }
  [HideInInspector] public int kernel_InitSums; [numthreads(numthreads1, 1, 1)] protected void InitSums(uint3 id) { unchecked { if (id.x < BitN) InitSums_GS(id); } }
  public virtual void InitSums_GS(uint3 id) { }
  [HideInInspector] public int kernel_Get_Bits_32; [numthreads(numthreads2, numthreads2, 1)] protected void Get_Bits_32(uint3 id) { unchecked { if (id.y < 32 && id.x < BitN) Get_Bits_32_GS(id); } }
  public virtual void Get_Bits_32_GS(uint3 id) { }
  [HideInInspector] public int kernel_Init_Bits_32; [numthreads(numthreads1, 1, 1)] protected void Init_Bits_32(uint3 id) { unchecked { if (id.x < BitN) Init_Bits_32_GS(id); } }
  public virtual void Init_Bits_32_GS(uint3 id) { }
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