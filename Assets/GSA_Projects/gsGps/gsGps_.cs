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
public class gsGps_ : GS
{
  [HideInInspector] public uiData data;
  [HideInInspector] public GGps g; public GGps G { get { gGps.GetData(); return gGps[0]; } }
  [HideInInspector] public bool ValuesChanged, gChanged;
  public static gsGps This;
  public virtual void Awake() { This = this as gsGps; Awake_GS(); }
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
    data.accuracy = accuracy;
    data.activate = activate;
    data.gpsPosition = gpsPosition;
    data.gpsAltitude = gpsAltitude;
    data.gpsError = gpsError;
    data.date_time = date_time;
    data.input = input;
    data.output = output;
  }
  public override void data_to_ui()
  {
    if (data == null) return;
    group_UI = data.group_UI;
    accuracy = ui_txt_str.Contains("\"accuracy\"") ? data.accuracy : float2("1, 100");
    activate = data.activate;
    gpsPosition = data.gpsPosition;
    gpsAltitude = data.gpsAltitude;
    gpsError = data.gpsError;
    date_time = data.date_time;
    input = ui_txt_str.Contains("\"input\"") ? data.input : 1;
    output = data.output;
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
    if (UI_accuracy.Changed || any(accuracy != UI_accuracy.v)) accuracy = UI_accuracy.v;
    if (UI_activate.Changed || activate != UI_activate.v) activate = UI_activate.v;
    if (UI_gpsPosition.Changed || any(gpsPosition != UI_gpsPosition.v)) gpsPosition = UI_gpsPosition.v;
    if (UI_gpsAltitude.Changed || gpsAltitude != UI_gpsAltitude.v) gpsAltitude = UI_gpsAltitude.v;
    if (UI_gpsError.Changed || any(gpsError != UI_gpsError.v)) gpsError = UI_gpsError.v;
    if (UI_date_time.Changed || date_time != UI_date_time.v) { data.date_time = UI_date_time.v; ValuesChanged = gChanged = true; }
    if (UI_input.Changed || input != UI_input.v) input = UI_input.v;
    if (UI_output.Changed || output != UI_output.v) output = UI_output.v;
    if (ValuesChanged) { gChanged = true; OnValueChanged(); ValuesChanged = UI_group_UI.Changed = UI_accuracy.Changed = UI_activate.Changed = UI_gpsPosition.Changed = UI_gpsAltitude.Changed = UI_gpsError.Changed = UI_date_time.Changed = UI_input.Changed = UI_output.Changed = false; }
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
  public virtual float2 accuracy { get => g.accuracy; set { if (any(g.accuracy != value) || any(UI_accuracy.v != value)) { g.accuracy = UI_accuracy.v = value; ValuesChanged = gChanged = true; } } }
  public virtual bool activate { get => Is(g.activate); set { if (g.activate != Is(value) || UI_activate.v != value) { g.activate = Is(UI_activate.v = value); ValuesChanged = gChanged = true; } } }
  public virtual float2 gpsPosition { get => g.gpsPosition; set { if (any(g.gpsPosition != value) || any(UI_gpsPosition.v != value)) { g.gpsPosition = UI_gpsPosition.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float gpsAltitude { get => g.gpsAltitude; set { if (any(g.gpsAltitude != value) || any(UI_gpsAltitude.v != value)) { g.gpsAltitude = UI_gpsAltitude.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float2 gpsError { get => g.gpsError; set { if (any(g.gpsError != value) || any(UI_gpsError.v != value)) { g.gpsError = UI_gpsError.v = value; ValuesChanged = gChanged = true; } } }
  public virtual uint input { get => g.input; set { if (g.input != value || UI_input.v != value) { g.input = UI_input.v = value; ValuesChanged = gChanged = true; } } }
  public virtual uint output { get => g.output; set { if (g.output != value || UI_output.v != value) { g.output = UI_output.v = value; ValuesChanged = gChanged = true; } } }
  public bool group_UI { get => UI_group_UI?.v ?? false; set { if (UI_group_UI != null) UI_group_UI.v = value; } }
  public UI_TreeGroup UI_group_UI;
  public UI_float2 UI_accuracy, UI_gpsPosition, UI_gpsError;
  public UI_bool UI_activate;
  public UI_float UI_gpsAltitude;
  public UI_string UI_date_time;
  public string date_time { get => UI_date_time?.v ?? ""; set { if (UI_date_time != null && data != null) UI_date_time.v = data.date_time = value; } }
  public UI_method UI_Get_GPS_Data_Path;
  public virtual void Get_GPS_Data_Path() { }
  public UI_uint UI_input, UI_output;
  public UI_method UI_RunComputeShader;
  public virtual void RunComputeShader() { }
  public UI_TreeGroup ui_group_UI => UI_group_UI;
  public UI_float2 ui_accuracy => UI_accuracy;
  public UI_bool ui_activate => UI_activate;
  public UI_float2 ui_gpsPosition => UI_gpsPosition;
  public UI_float ui_gpsAltitude => UI_gpsAltitude;
  public UI_float2 ui_gpsError => UI_gpsError;
  public UI_string ui_date_time => UI_date_time;
  public UI_uint ui_input => UI_input;
  public UI_uint ui_output => UI_output;
  [Serializable]
  public class uiData
  {
    public bool siUnits, group_UI, activate;
    public float2 accuracy, gpsPosition, gpsError;
    public float gpsAltitude;
    public string date_time;
    public uint input, output;
  }
  public virtual void InitBuffers()
  {
    InitBuffers0_GS();
    AddComputeBuffer(ref gGps, nameof(gGps), 1);
    InitKernels();
    InitBuffers1_GS();
    isInitBuffers = true;
  }
  public virtual void InitBuffers0_GS() { }
  public virtual void InitBuffers1_GS() { }
  [Serializable]
  public struct GGps
  {
    public float2 accuracy, gpsPosition, gpsError;
    public uint activate, input, output;
    public float gpsAltitude;
  };
  public RWStructuredBuffer<GGps> gGps;
  public Texture2D _PaletteTex;
  public float4 palette(float v) => paletteColor(_PaletteTex, v);
  public float4 palette(float v, float w) => float4(palette(v).xyz, w);

  public virtual void onRenderObject_LIN(bool show, uint _itemN, ref uint i, ref uint index, ref uint3 LIN) { uint n = 0; if (show) { if (i < (n = _itemN)) LIN = uint3(index, i, 0); LIN.z += n; i -= n; } index++; }
  public virtual void onRenderObject_LIN(uint _itemN, ref uint i, ref uint index, ref uint3 LIN) { onRenderObject_LIN(true, _itemN, ref i, ref index, ref LIN); }
  public virtual uint3 onRenderObject_LIN(uint i) { uint3 LIN = u000; return LIN; }
  public virtual uint3 onRenderObject_LIN_Quads(uint i) { uint3 LIN = u000; return LIN; }
  public virtual uint3 onRenderObject_LIN_Points(uint i) { uint3 LIN = u000; return LIN; }
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
}