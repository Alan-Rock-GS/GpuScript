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
public class gsBackup_Lib_ : GS
{
  [HideInInspector] public uiData data;
  [HideInInspector] public GBackup_Lib g; public GBackup_Lib G { get { gBackup_Lib.GetData(); return gBackup_Lib[0]; } }
  [HideInInspector] public bool ValuesChanged, gChanged;
  public static gsBackup_Lib This;
  public virtual void Awake() { This = this as gsBackup_Lib; Awake_GS(); }
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
    data.group_Backup = group_Backup;
    data.backup_Code = backup_Code;
    data.backup_Data = backup_Data;
    data.backup_HTML = backup_HTML;
    data.backup_Local = backup_Local;
    data.backup_External = backup_External;
    data.backup_Remote = backup_Remote;
    data.backup_Number = backup_Number;
    data.backup_Description = backup_Description;
  }
  public override void data_to_ui()
  {
    if (data == null) return;
    group_Backup = data.group_Backup;
    backup_Code = data.backup_Code;
    backup_Data = data.backup_Data;
    backup_HTML = data.backup_HTML;
    backup_Local = data.backup_Local;
    backup_External = data.backup_External;
    backup_Remote = data.backup_Remote;
    backup_Number = data.backup_Number;
    backup_Description = data.backup_Description;
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
    UI_group_Backup?.Display_Tree();
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
    if (UI_backup_Code.Changed || backup_Code != UI_backup_Code.v) backup_Code = UI_backup_Code.v;
    if (UI_backup_Data.Changed || backup_Data != UI_backup_Data.v) backup_Data = UI_backup_Data.v;
    if (UI_backup_HTML.Changed || backup_HTML != UI_backup_HTML.v) backup_HTML = UI_backup_HTML.v;
    if (UI_backup_Local.Changed || backup_Local != UI_backup_Local.v) backup_Local = UI_backup_Local.v;
    if (UI_backup_External.Changed || backup_External != UI_backup_External.v) backup_External = UI_backup_External.v;
    if (UI_backup_Remote.Changed || backup_Remote != UI_backup_Remote.v) backup_Remote = UI_backup_Remote.v;
    if (UI_backup_Number.Changed || backup_Number != UI_backup_Number.v) backup_Number = UI_backup_Number.v;
    if (UI_backup_Description.Changed || backup_Description != UI_backup_Description.v) { data.backup_Description = UI_backup_Description.v; ValuesChanged = gChanged = true; }
    if (GetKeyDown(CtrlAlt, 'b')) StartCoroutine(Backup());
    if (ValuesChanged) { gChanged = true; OnValueChanged(); ValuesChanged = UI_group_Backup.Changed = UI_backup_Code.Changed = UI_backup_Data.Changed = UI_backup_HTML.Changed = UI_backup_Local.Changed = UI_backup_External.Changed = UI_backup_Remote.Changed = UI_backup_Number.Changed = UI_backup_Description.Changed = false; }
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
    UI_backup_HTML.DisplayIf(Show_backup_HTML && UI_backup_HTML.treeGroup_parent.isExpanded);
    UI_backup_Remote.DisplayIf(Show_backup_Remote && UI_backup_Remote.treeGroup_parent.isExpanded);
    OnValueChanged_GS();
  }
  public override void OnValueChanged_GS()
  {
    var type = "gsReport_Lib".ToType();
    if (type != null) ((GS)GetComponent(type))?.OnValueChanged_GS();

  }
  public string[] Scenes_in_Build => new string[] {  };
  public virtual bool Show_backup_HTML { get => backup_Data; }
  public virtual bool Show_backup_Remote { get => false; }
  public virtual bool backup_Code { get => Is(g.backup_Code); set { if (g.backup_Code != Is(value) || UI_backup_Code.v != value) { g.backup_Code = Is(UI_backup_Code.v = value); ValuesChanged = gChanged = true; } } }
  public virtual bool backup_Data { get => Is(g.backup_Data); set { if (g.backup_Data != Is(value) || UI_backup_Data.v != value) { g.backup_Data = Is(UI_backup_Data.v = value); ValuesChanged = gChanged = true; } } }
  public virtual bool backup_HTML { get => Is(g.backup_HTML); set { if (g.backup_HTML != Is(value) || UI_backup_HTML.v != value) { g.backup_HTML = Is(UI_backup_HTML.v = value); ValuesChanged = gChanged = true; } } }
  public virtual bool backup_Local { get => Is(g.backup_Local); set { if (g.backup_Local != Is(value) || UI_backup_Local.v != value) { g.backup_Local = Is(UI_backup_Local.v = value); ValuesChanged = gChanged = true; } } }
  public virtual bool backup_External { get => Is(g.backup_External); set { if (g.backup_External != Is(value) || UI_backup_External.v != value) { g.backup_External = Is(UI_backup_External.v = value); ValuesChanged = gChanged = true; } } }
  public virtual bool backup_Remote { get => Is(g.backup_Remote); set { if (g.backup_Remote != Is(value) || UI_backup_Remote.v != value) { g.backup_Remote = Is(UI_backup_Remote.v = value); ValuesChanged = gChanged = true; } } }
  public virtual uint backup_Number { get => g.backup_Number; set { if (g.backup_Number != value || UI_backup_Number.v != value) { g.backup_Number = UI_backup_Number.v = value; ValuesChanged = gChanged = true; } } }
  public bool group_Backup { get => UI_group_Backup?.v ?? false; set { if (UI_group_Backup != null) UI_group_Backup.v = value; } }
  public UI_TreeGroup UI_group_Backup;
  public UI_bool UI_backup_Code, UI_backup_Data, UI_backup_HTML, UI_backup_Local, UI_backup_External, UI_backup_Remote;
  public UI_uint UI_backup_Number;
  public UI_string UI_backup_Description;
  public string backup_Description { get => UI_backup_Description?.v ?? ""; set { if (UI_backup_Description != null && data != null) UI_backup_Description.v = data.backup_Description = value; } }
  public UI_method UI_Backup;
  [HideInInspector] public bool in_Backup = false; public IEnumerator Backup() { if (in_Backup) { in_Backup = false; yield break; } in_Backup = true; yield return StartCoroutine(Backup_Sync()); in_Backup = false; }
  public virtual IEnumerator Backup_Sync() { yield return null; }
  public UI_TreeGroup ui_group_Backup => UI_group_Backup;
  public UI_bool ui_backup_Code => UI_backup_Code;
  public UI_bool ui_backup_Data => UI_backup_Data;
  public UI_bool ui_backup_HTML => UI_backup_HTML;
  public UI_bool ui_backup_Local => UI_backup_Local;
  public UI_bool ui_backup_External => UI_backup_External;
  public UI_bool ui_backup_Remote => UI_backup_Remote;
  public UI_uint ui_backup_Number => UI_backup_Number;
  public UI_string ui_backup_Description => UI_backup_Description;
  [Serializable]
  public class uiData
  {
    public bool siUnits, group_Backup, backup_Code, backup_Data, backup_HTML, backup_Local, backup_External, backup_Remote;
    public uint backup_Number;
    public string backup_Description;
  }
  public virtual void InitBuffers()
  {
    InitBuffers0_GS();
    AddComputeBuffer(ref gBackup_Lib, nameof(gBackup_Lib), 1);
    InitKernels();
    InitBuffers1_GS();
    isInitBuffers = true;
  }
  public virtual void InitBuffers0_GS() { }
  public virtual void InitBuffers1_GS() { }
  [Serializable]
  public struct GBackup_Lib
  {
    public uint backup_Code, backup_Data, backup_HTML, backup_Local, backup_External, backup_Remote, backup_Number;
  };
  public RWStructuredBuffer<GBackup_Lib> gBackup_Lib;
  public Texture2D _PaletteTex;
  public float4 palette(float v) => paletteColor(_PaletteTex, v);
  public float4 palette(float v, float w) => float4(palette(v).xyz, w);

  public virtual void onRenderObject_LIN(bool show, uint _itemN, ref uint i, ref uint index, ref uint3 LIN) { uint n = 0; if (show) { if (i < (n = _itemN)) LIN = uint3(index, i, 0); LIN.z += n; i -= n; } index++; }
  public virtual void onRenderObject_LIN(uint _itemN, ref uint i, ref uint index, ref uint3 LIN) { onRenderObject_LIN(true, _itemN, ref i, ref index, ref LIN); }
  public virtual uint3 onRenderObject_LIN(uint i) { uint3 LIN = u000; uint index = 0; return LIN; }
  public virtual uint3 onRenderObject_LIN_Quads(uint i) { uint3 LIN = u000; uint index = 0; return LIN; }
  public virtual uint3 onRenderObject_LIN_Points(uint i) { uint3 LIN = u000; uint index = 0; return LIN; }
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