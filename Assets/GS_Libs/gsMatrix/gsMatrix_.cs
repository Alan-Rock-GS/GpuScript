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
public class gsMatrix_ : GS
{
  [HideInInspector] public uiData data;
  [HideInInspector] public GMatrix g; public GMatrix G { get { gMatrix.GetData(); return gMatrix[0]; } }
  public void g_SetData() { if (gChanged && gMatrix != null) { gMatrix[0] = g; gMatrix.SetData(); gChanged = false; } }
  public virtual void Ints_SetKernels(bool reallocate = false) { if (Ints != null && (reallocate || Ints.reallocated)) { SetKernelValues(Ints, nameof(Ints)); Ints.reallocated = false; } }
  public virtual void A_matrix_SetKernels(bool reallocate = false) { if (A_matrix != null && (reallocate || A_matrix.reallocated)) { SetKernelValues(A_matrix, nameof(A_matrix)); A_matrix.reallocated = false; } }
  public virtual void Xs_SetKernels(bool reallocate = false) { if (Xs != null && (reallocate || Xs.reallocated)) { SetKernelValues(Xs, nameof(Xs)); Xs.reallocated = false; } }
  public virtual void Bs_SetKernels(bool reallocate = false) { if (Bs != null && (reallocate || Bs.reallocated)) { SetKernelValues(Bs, nameof(Bs)); Bs.reallocated = false; } }
  public virtual void Gpu_Calc_bs() { g_SetData(); Gpu(kernel_Calc_bs, Calc_bs, uint3(col_m, row_n, XN)); }
  public virtual void Cpu_Calc_bs() { Cpu(Calc_bs, uint3(col_m, row_n, XN)); }
  public virtual void Cpu_Calc_bs(uint3 id) { Calc_bs(id); }
  public virtual void Gpu_Zero_bs() { g_SetData(); Gpu(kernel_Zero_bs, Zero_bs, uint2(col_m, XN)); }
  public virtual void Cpu_Zero_bs() { Cpu(Zero_bs, uint2(col_m, XN)); }
  public virtual void Cpu_Zero_bs(uint3 id) { Zero_bs(id); }
  public virtual void Gpu_Get_Bs() { g_SetData(); Gpu(kernel_Get_Bs, Get_Bs, uint2(col_m, XN)); }
  public virtual void Cpu_Get_Bs() { Cpu(Get_Bs, uint2(col_m, XN)); }
  public virtual void Cpu_Get_Bs(uint3 id) { Get_Bs(id); }
  public virtual void Gpu_Set_Xs() { g_SetData(); Gpu(kernel_Set_Xs, Set_Xs, uint2(col_m, XN)); }
  public virtual void Cpu_Set_Xs() { Cpu(Set_Xs, uint2(col_m, XN)); }
  public virtual void Cpu_Set_Xs(uint3 id) { Set_Xs(id); }
  public virtual void Gpu_Set_A_matrix() { g_SetData(); Gpu(kernel_Set_A_matrix, Set_A_matrix, uint2(col_m, row_n)); }
  public virtual void Cpu_Set_A_matrix() { Cpu(Set_A_matrix, uint2(col_m, row_n)); }
  public virtual void Cpu_Set_A_matrix(uint3 id) { Set_A_matrix(id); }
  public virtual void Gpu_Get_A_matrix() { g_SetData(); Gpu(kernel_Get_A_matrix, Get_A_matrix, uint2(col_m, row_n)); }
  public virtual void Cpu_Get_A_matrix() { Cpu(Get_A_matrix, uint2(col_m, row_n)); }
  public virtual void Cpu_Get_A_matrix(uint3 id) { Get_A_matrix(id); }
  [HideInInspector] public bool ValuesChanged, gChanged;
  public static gsMatrix This;
  public virtual void Awake() { This = this as gsMatrix; Awake_GS(); }
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
  public virtual uint IntsN { get => g.IntsN; set { if (g.IntsN != value) { g.IntsN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint col_m { get => g.col_m; set { if (g.col_m != value) { g.col_m = value; ValuesChanged = gChanged = true; } } }
  public virtual uint row_n { get => g.row_n; set { if (g.row_n != value) { g.row_n = value; ValuesChanged = gChanged = true; } } }
  public virtual uint XN { get => g.XN; set { if (g.XN != value) { g.XN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint AI0 { get => g.AI0; set { if (g.AI0 != value) { g.AI0 = value; ValuesChanged = gChanged = true; } } }
  public virtual uint XsI0 { get => g.XsI0; set { if (g.XsI0 != value) { g.XsI0 = value; ValuesChanged = gChanged = true; } } }
  public virtual uint BsI0 { get => g.BsI0; set { if (g.BsI0 != value) { g.BsI0 = value; ValuesChanged = gChanged = true; } } }
  [Serializable]
  public class uiData
  {
    public bool siUnits;
  }
  public virtual void InitBuffers()
  {
    InitBuffers0_GS();
    AllocData_gMatrix(1);
    InitKernels();
    SetKernelValues(gMatrix, nameof(gMatrix), kernel_Calc_bs, kernel_Zero_bs, kernel_Get_Bs, kernel_Set_Xs, kernel_Set_A_matrix, kernel_Get_A_matrix);
    InitBuffers1_GS();
    isInitBuffers = true;
  }
  public virtual void InitBuffers0_GS() { }
  public virtual void InitBuffers1_GS() { }
  [Serializable]
  public struct GMatrix
  {
    public uint IntsN, col_m, row_n, XN, AI0, XsI0, BsI0;
  };
  public RWStructuredBuffer<GMatrix> gMatrix;
  public RWStructuredBuffer<int> Ints;
  public RWStructuredBuffer<float> A_matrix, Xs, Bs;
  public virtual void AllocData_gMatrix(uint n) => AddComputeBuffer(ref gMatrix, nameof(gMatrix), n);
  public virtual void AllocData_Ints(uint n) => AddComputeBuffer(ref Ints, nameof(Ints), n);
  public virtual void AssignData_Ints(params int[] data) => AddComputeBufferData(ref Ints, nameof(Ints), data);
  public virtual void AllocData_A_matrix(uint n) => AddComputeBuffer(ref A_matrix, nameof(A_matrix), n);
  public virtual void AssignData_A_matrix(params float[] data) => AddComputeBufferData(ref A_matrix, nameof(A_matrix), data);
  public virtual void AllocData_Xs(uint n) => AddComputeBuffer(ref Xs, nameof(Xs), n);
  public virtual void AssignData_Xs(params float[] data) => AddComputeBufferData(ref Xs, nameof(Xs), data);
  public virtual void AllocData_Bs(uint n) => AddComputeBuffer(ref Bs, nameof(Bs), n);
  public virtual void AssignData_Bs(params float[] data) => AddComputeBufferData(ref Bs, nameof(Bs), data);
  public Texture2D _PaletteTex;
  public float4 palette(float v) => paletteColor(_PaletteTex, v);
  public float4 palette(float v, float w) => float4(palette(v).xyz, w);

  public virtual void onRenderObject_LIN(bool show, uint _itemN, ref uint i, ref uint index, ref uint3 LIN) { uint n = 0; if (show) { if (i < (n = _itemN)) LIN = uint3(index, i, 0); LIN.z += n; i -= n; } index++; }
  public virtual void onRenderObject_LIN(uint _itemN, ref uint i, ref uint index, ref uint3 LIN) { onRenderObject_LIN(true, _itemN, ref i, ref index, ref LIN); }
  public virtual uint3 onRenderObject_LIN(uint i) { uint3 LIN = u000; return LIN; }
  public virtual uint3 onRenderObject_LIN_Quads(uint i) { uint3 LIN = u000; return LIN; }
  public virtual uint3 onRenderObject_LIN_Points(uint i) { uint3 LIN = u000; return LIN; }
  [HideInInspector] public int kernel_Calc_bs; [numthreads(numthreads3, numthreads3, numthreads3)] protected void Calc_bs(uint3 id) { unchecked { if (id.z < XN && id.y < row_n && id.x < col_m) Calc_bs_GS(id); } }
  public virtual void Calc_bs_GS(uint3 id) { }
  [HideInInspector] public int kernel_Zero_bs; [numthreads(numthreads2, numthreads2, 1)] protected void Zero_bs(uint3 id) { unchecked { if (id.y < XN && id.x < col_m) Zero_bs_GS(id); } }
  public virtual void Zero_bs_GS(uint3 id) { }
  [HideInInspector] public int kernel_Get_Bs; [numthreads(numthreads2, numthreads2, 1)] protected void Get_Bs(uint3 id) { unchecked { if (id.y < XN && id.x < col_m) Get_Bs_GS(id); } }
  public virtual void Get_Bs_GS(uint3 id) { }
  [HideInInspector] public int kernel_Set_Xs; [numthreads(numthreads2, numthreads2, 1)] protected void Set_Xs(uint3 id) { unchecked { if (id.y < XN && id.x < col_m) Set_Xs_GS(id); } }
  public virtual void Set_Xs_GS(uint3 id) { }
  [HideInInspector] public int kernel_Set_A_matrix; [numthreads(numthreads2, numthreads2, 1)] protected void Set_A_matrix(uint3 id) { unchecked { if (id.y < row_n && id.x < col_m) Set_A_matrix_GS(id); } }
  public virtual void Set_A_matrix_GS(uint3 id) { }
  [HideInInspector] public int kernel_Get_A_matrix; [numthreads(numthreads2, numthreads2, 1)] protected void Get_A_matrix(uint3 id) { unchecked { if (id.y < row_n && id.x < col_m) Get_A_matrix_GS(id); } }
  public virtual void Get_A_matrix_GS(uint3 id) { }
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