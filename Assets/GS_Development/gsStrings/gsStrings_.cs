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
public class gsStrings_ : GS
{
  [HideInInspector] public uiData data;
  [HideInInspector] public GStrings g; public GStrings G { get { gStrings.GetData(); return gStrings[0]; } }
  [HideInInspector] public bool ValuesChanged, gChanged;
  public static gsStrings This;
  public virtual void Awake() { This = this as gsStrings; Awake_GS(); }
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
    data.Names = Names;
  }
  public override void data_to_ui()
  {
    if (data == null) return;
    group_UI = data.group_UI;
    Names = data.Names;
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
    if (ValuesChanged) { gChanged = true; OnValueChanged(); ValuesChanged = UI_group_UI.Changed = UI_Names.Changed = false; }
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
  public bool group_UI { get => UI_group_UI?.v ?? false; set { if (UI_group_UI != null) UI_group_UI.v = value; } }
  public UI_TreeGroup UI_group_UI;
  public UI_strings UI_Names;
  public string Names { get => UI_Names?.v ?? ""; set { if (UI_Names != null && data != null) UI_Names.v = data.Names = value; } }
  public UI_TreeGroup ui_group_UI => UI_group_UI;
  public UI_strings ui_Names => UI_Names;
  [Serializable]
  public class uiData
  {
    public bool siUnits, group_UI;
    public string Names;
  }
  public virtual void InitBuffers()
  {
    InitBuffers0_GS();
    AllocData_gStrings(1);
    InitKernels();
    InitBuffers1_GS();
    isInitBuffers = true;
  }
  public virtual void InitBuffers0_GS() { }
  public virtual void InitBuffers1_GS() { }
  [Serializable]
  public struct GStrings
  {
  };
  public RWStructuredBuffer<GStrings> gStrings;
  public virtual void AllocData_gStrings(uint n) => AddComputeBuffer(ref gStrings, nameof(gStrings), n);
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