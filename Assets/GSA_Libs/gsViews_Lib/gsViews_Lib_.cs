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
public class gsViews_Lib_ : GS
{
  [HideInInspector] public uiData data;
  [HideInInspector] public GViews_Lib g; public GViews_Lib G { get { gViews_Lib.GetData(); return gViews_Lib[0]; } }
  [JsonConverter(typeof(StringEnumConverter))] public enum ProjectionMode : uint { Automatic, Perspective, Orthographic }
  public const uint ProjectionMode_Automatic = 0, ProjectionMode_Perspective = 1, ProjectionMode_Orthographic = 2;
  [HideInInspector] public bool ValuesChanged, gChanged;
  public static gsViews_Lib This;
  public virtual void Awake() { This = this as gsViews_Lib; Awake_GS(); }
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
  public class _UI_CamView
  {
    public class UI_CamView_Items
    {
      public gsViews_Lib_ gs;
      public int row;
      public UI_CamView_Items(gsViews_Lib_ gs) { this.gs = gs; }
      public UI_string UI_viewName => gs.UI_grid_CamViews_Lib.RowItems[row][0] as UI_string; public string viewName { get => UI_viewName.v; set => UI_viewName.v = value; }
      public UI_float3 UI_viewCenter => gs.UI_grid_CamViews_Lib.RowItems[row][1] as UI_float3; public float3 viewCenter { get => UI_viewCenter.v; set => UI_viewCenter.v = value; }
      public UI_float UI_viewDist => gs.UI_grid_CamViews_Lib.RowItems[row][2] as UI_float; public float viewDist { get => UI_viewDist.v; set => UI_viewDist.v = value; }
      public UI_float2 UI_viewTiltSpin => gs.UI_grid_CamViews_Lib.RowItems[row][3] as UI_float2; public float2 viewTiltSpin { get => UI_viewTiltSpin.v; set => UI_viewTiltSpin.v = value; }
      public UI_enum UI_viewProjection => gs.UI_grid_CamViews_Lib.RowItems[row][4] as UI_enum; public ProjectionMode viewProjection { get => (ProjectionMode)UI_viewProjection.v; set => UI_viewProjection.v = value.To_uint(); }
    }
    public gsViews_Lib_ gs;
    public UI_CamView_Items ui_CamView_Items;
    public _UI_CamView(gsViews_Lib_ gs) { this.gs = gs; ui_CamView_Items = new UI_CamView_Items(gs); }
    public UI_CamView_Items this[int i] { get { ui_CamView_Items.row = i; return ui_CamView_Items; } }
  }
  public _UI_CamView UI_CamViews_Lib;
  public override void ui_to_data()
  {
    if (data == null) return;
    data.siUnits = siUnits;
    data.group_CamViews_Lib = group_CamViews_Lib;
    data.CamViews_Lib = CamViews_Lib;
    data.CamViews_Lib_DisplayCols = UI_grid_CamViews_Lib?.displayColumns?.Select(a => a.v).ToArray() ?? default;
    data.CamViews_Lib_VScroll = UI_grid_CamViews_Lib?.VScroll.value ?? default;
    data.CamViews_Lib_DisplayRowN = UI_grid_CamViews_Lib?.dispRowN.v ?? default;
    data.CamViews_Lib_isExpanded = UI_grid_CamViews_Lib?.isExpanded ?? default;
    data.CamViews_Lib_lastClickedRow = UI_grid_CamViews_Lib?.lastClickedRow ?? default;
    data.CamViews_Lib_selectedRows = UI_grid_CamViews_Lib?.isRowSelected.bools_to_RangeStr() ?? default;
  }
  public override void data_to_ui()
  {
    if (data == null) return;
    group_CamViews_Lib = data.group_CamViews_Lib;
    CamViews_Lib = data.CamViews_Lib;
    UI_CamViews_Lib = new _UI_CamView(this);
    "CamViews_Lib_To_UI".InvokeMethod(this);
    for (int i = 0; i < data.CamViews_Lib_DisplayCols?.Length && i < UI_grid_CamViews_Lib.displayColumns.Count; i++)
    {
      UI_grid_CamViews_Lib.displayColumns[i].v = data.CamViews_Lib_DisplayCols[i];
      var item = UI_grid_CamViews_Lib.RowItems[0][i];
      if(UI_grid_CamViews_Lib.headerButtons[i].unitLabel != null)
        UI_grid_CamViews_Lib.headerButtons[i].unitLabel.style.display = DisplayIf(item.siUnit != siUnit.Null || item.usUnit != usUnit.Null || item.Unit != Unit.Null);
    }
    UI_grid_CamViews_Lib.VScroll.value = data.CamViews_Lib_VScroll;
    UI_grid_CamViews_Lib.dispRowN.v = data.CamViews_Lib_DisplayRowN == 0 ? 20 : data.CamViews_Lib_DisplayRowN;
    UI_grid_CamViews_Lib.isExpanded = data.CamViews_Lib_isExpanded;
    UI_grid_CamViews_Lib.selectedRows = data.CamViews_Lib_selectedRows;
    UI_grid_CamViews_Lib.lastClickedRow = data.CamViews_Lib_lastClickedRow;
    CamViews_Lib ??= new CamView[] { };
    UI_grid_CamViews_Lib.isRowSelected = data.CamViews_Lib_selectedRows.RangeStr_to_bools(CamViews_Lib.Length);
    UI_grid_CamViews_Lib.DrawGrid();
  }
  public List<CamView> CamViews_Lib_CopyPaste;
  public virtual void CamViews_Lib_OnButtonClicked(int row, int col)
  {
    var UI_grid = UI_grid_CamViews_Lib;
    var name = UI_grid.RowItems[row][col].name.After("UI_method_").BeforeLast("_");
    if (name == "SaveView") CamViews_Lib_SaveView(UI_grid.StartRow + row);
    else if (name == "LoadView") CamViews_Lib_LoadView(UI_grid.StartRow + row);
  }
  public virtual void CamViews_Lib_OnRowNumberButtonClicked(int row) { }
  public virtual void CamViews_Lib_OnHeaderButtonClicked(string label)
  {
    if (CamViews_Lib.Length < 2) return;
    CamViews_Lib = label switch
    {
      "Name" => CamViews_Lib[0].viewName.CompareTo(CamViews_Lib[^1].viewName) < 0 ? CamViews_Lib.OrderByDescending(a => a.viewName).ToArray() : CamViews_Lib.OrderBy(a => a.viewName).ToArray(),
      "Center" => CamViews_Lib[0].viewCenter.CompareTo(CamViews_Lib[^1].viewCenter) < 0 ? CamViews_Lib.OrderByDescending(a => a.viewCenter).ToArray() : CamViews_Lib.OrderBy(a => a.viewCenter).ToArray(),
      "Dist" => CamViews_Lib[0].viewDist.CompareTo(CamViews_Lib[^1].viewDist) < 0 ? CamViews_Lib.OrderByDescending(a => a.viewDist).ToArray() : CamViews_Lib.OrderBy(a => a.viewDist).ToArray(),
      "Tilt Spin" => CamViews_Lib[0].viewTiltSpin.CompareTo(CamViews_Lib[^1].viewTiltSpin) < 0 ? CamViews_Lib.OrderByDescending(a => a.viewTiltSpin).ToArray() : CamViews_Lib.OrderBy(a => a.viewTiltSpin).ToArray(),
      "Projection" => CamViews_Lib[0].viewProjection.CompareTo(CamViews_Lib[^1].viewProjection) < 0 ? CamViews_Lib.OrderByDescending(a => a.viewProjection).ToArray() : CamViews_Lib.OrderBy(a => a.viewProjection).ToArray(),
      _ => default,
    };
    CamViews_Lib_To_UI();
  }
  public virtual void UI_To_CamViews_Lib()
  {
    if (CamViews_Lib == null) return;
    var UI_grid = UI_grid_CamViews_Lib;
    int startRow = UI_grid.StartRow, i1 = min(UI_grid.DisplayRowN, CamViews_Lib.Length - startRow);
    i1 = min(i1, UI_grid.RowItems.Count);
    for (int i = 0; i < i1; i++)
    {
      var ui = UI_CamViews_Lib[i];
      var row = CamViews_Lib[i + startRow];
      row.viewName = ui.viewName;
      row.viewCenter = ui.viewCenter;
      row.viewDist = ui.viewDist;
      row.viewTiltSpin = ui.viewTiltSpin;
      row.viewProjection = (uint)ui.viewProjection;
      CamViews_Lib[i + startRow] = row;
    }
  }
  protected bool in_CamViews_Lib_To_UI = false;
  public virtual bool CamViews_Lib_To_UI()
  {
    if (CamViews_Lib == null || in_CamViews_Lib_To_UI) return false;
    isGridBuilding = true;
    in_CamViews_Lib_To_UI = true;
    var UI_grid = UI_grid_CamViews_Lib;
    int startRow = UI_grid.StartRow, i1 = min(UI_grid.DisplayRowN, CamViews_Lib.Length - startRow);
    i1 = min(i1, UI_grid.RowItems.Count);
    for (int i = 0; i < i1; i++)
    {
      var ui = UI_CamViews_Lib[i];
      var row = CamViews_Lib[i + startRow];
      ui.viewName = row.viewName;
      ui.viewCenter = row.viewCenter;
      ui.viewDist = row.viewDist;
      ui.viewTiltSpin = row.viewTiltSpin;
      ui.viewProjection = (ProjectionMode)row.viewProjection;
    }
    UI_grid.DrawGrid();
    in_CamViews_Lib_To_UI = false;
    isGridBuilding = false;
    return true;
  }
  public const int CamViews_Lib_viewName_Col = 0, CamViews_Lib_viewCenter_Col = 1, CamViews_Lib_viewDist_Col = 2, CamViews_Lib_viewTiltSpin_Col = 3, CamViews_Lib_viewProjection_Col = 4;
  public virtual bool CamViews_Lib_ShowIf(int row, int col)
  {
    return true;
  }
  public virtual void CamViews_Lib_OnAddRow() { var list = CamViews_Lib.ToList(); list.Add(default); CamViews_Lib = list.ToArray(); }
  public virtual void CamViews_Lib_viewName_OnValueChanged(int row, string previousValue) { }
  public virtual void CamViews_Lib_viewCenter_OnValueChanged(int row, float3 previousValue) { }
  public virtual void CamViews_Lib_viewDist_OnValueChanged(int row, float previousValue) { }
  public virtual void CamViews_Lib_viewTiltSpin_OnValueChanged(int row, float2 previousValue) { }
  public virtual void CamViews_Lib_viewProjection_OnValueChanged(int row, ProjectionMode previousValue) { }
  public virtual void CamViews_Lib_OnValueChanged(int row, int col)
  {
    if (!ui_loaded) return;
    var UI_grid = UI_grid_CamViews_Lib;
    int startRow = UI_grid.StartRow;
    var ui = UI_CamViews_Lib[row];
    if (row + startRow >= CamViews_Lib.Length) return;
    var data = CamViews_Lib[row + startRow];
    if (col == CamViews_Lib_viewName_Col && data.viewName != ui.viewName) { var v = data.viewName; data.viewName = ui.viewName; CamViews_Lib[row + startRow] = data; CamViews_Lib_viewName_OnValueChanged(row + startRow, v); }
    else if (col == CamViews_Lib_viewCenter_Col && any(data.viewCenter != ui.viewCenter)) { var v = data.viewCenter; data.viewCenter = ui.viewCenter; CamViews_Lib[row + startRow] = data; CamViews_Lib_viewCenter_OnValueChanged(row + startRow, v); }
    else if (col == CamViews_Lib_viewDist_Col && any(data.viewDist != ui.viewDist)) { var v = data.viewDist; data.viewDist = ui.viewDist; CamViews_Lib[row + startRow] = data; CamViews_Lib_viewDist_OnValueChanged(row + startRow, v); }
    else if (col == CamViews_Lib_viewTiltSpin_Col && any(data.viewTiltSpin != ui.viewTiltSpin)) { var v = data.viewTiltSpin; data.viewTiltSpin = ui.viewTiltSpin; CamViews_Lib[row + startRow] = data; CamViews_Lib_viewTiltSpin_OnValueChanged(row + startRow, v); }
    else if (col == CamViews_Lib_viewProjection_Col && (ProjectionMode)data.viewProjection != ui.viewProjection) { var v = (ProjectionMode)data.viewProjection; data.viewProjection = (uint)ui.viewProjection; CamViews_Lib[row + startRow] = data; CamViews_Lib_viewProjection_OnValueChanged(row + startRow, v); }
  }
  public virtual int CamViews_Lib_GetGridArrayLength() => CamViews_Lib?.Length ?? 0;
  public virtual int CamViews_Lib_SelectedRow
  {
    get { for (int i = 0; i < CamViews_Lib.Length; i++) if (UI_grid_CamViews_Lib.isRowSelected[i]) return i; return -1; }
    set
    {
      for (int i = 0; i < CamViews_Lib.Length; i++) UI_grid_CamViews_Lib.isRowSelected[i] = value == i;
      UI_grid_CamViews_Lib.lastClickedRow = value;
      UI_grid_CamViews_Lib.DrawGrid();
    }
  }
  public virtual int[] CamViews_Lib_SelectedRows
  {
    get => UI_grid_CamViews_Lib.isRowSelected.Select((a, i) => new { a, i }).Where(a => a.a).Select(a => a.i).ToArray();
    set
    {
      for (int i = 0; i < CamViews_Lib.Length; i++) UI_grid_CamViews_Lib.isRowSelected[i] = false;
      for (int i = 0; i < value.Length; i++) { int row = value[i]; UI_grid_CamViews_Lib.isRowSelected[row] = true; UI_grid_CamViews_Lib.lastClickedRow = row; }
      UI_grid_CamViews_Lib.DrawGrid();
    }
  }
  public virtual void CamViews_Lib_OnCut()
  {
    CamViews_Lib_OnCopy();
    CamViews_Lib = CamViews_Lib.Except(CamViews_Lib_CopyPaste).ToArray();
    UI_grid_CamViews_Lib.StartRow = min(UI_grid_CamViews_Lib.StartRow, max(0, CamViews_Lib_CopyPaste.Count - UI_grid_CamViews_Lib.DisplayRowN));
    UI_grid_CamViews_Lib.isRowSelected = new bool[CamViews_Lib_CopyPaste.Count];
    UI_grid_CamViews_Lib.DrawGrid();
  }
  public virtual void CamViews_Lib_OnCopy() { CamViews_Lib_CopyPaste = CamViews_Lib.Where((a, i) => UI_grid_CamViews_Lib.isRowSelected[i]).Select(a => a).ToList(); }
  public virtual void CamViews_Lib_OnPaste()
  {
    var list = CamViews_Lib.ToList();
    var newSelectedRows = new List<int>();
    for (int i = CamViews_Lib.Length - 1; i >= 0; i--)
      if (UI_grid_CamViews_Lib.isRowSelected[i])
      {
        for (int j = 0; j < CamViews_Lib_CopyPaste.Count; j++) { list.Insert(i + j, CamViews_Lib_CopyPaste[j]); newSelectedRows.Add(i + j); }
        break;
      }
    CamViews_Lib = list.ToArray();
    UI_grid_CamViews_Lib.isRowSelected = newSelectedRows.ToArray().ints_to_selected_bools(list.Count);
    CamViews_Lib_To_UI();
  }
  public virtual void CamViews_Lib_OnInsert()
  {
    var list = CamViews_Lib.ToList();
    var newSelectedRows = new List<int>();
    for (int i = 0; i < CamViews_Lib.Length; i++) if (UI_grid_CamViews_Lib.isRowSelected[i]) { list.Insert(i, default); newSelectedRows.Add(i); }
    CamViews_Lib = list.ToArray();
    UI_grid_CamViews_Lib.isRowSelected = newSelectedRows.ToArray().ints_to_selected_bools(list.Count);
    CamViews_Lib_To_UI();
  }
  public virtual void CamViews_Lib_OnDelete() { CamViews_Lib_OnCut(); }
  public virtual void CamViews_Lib_OnUpArrow()
  {
    int row = 1;
    if (CamViews_Lib.Length > 1 && !UI_grid_CamViews_Lib.isRowSelected[0])
    {
      var list = CamViews_Lib.ToList();
      var newSelectedRows = new List<int>();
      for (int i = 0; i < CamViews_Lib.Length; i++)
        if (UI_grid_CamViews_Lib.isRowSelected[i]) { var item = list[i]; list.RemoveAt(i); list.Insert(i - 1, item); newSelectedRows.Add(i - 1); }
      CamViews_Lib = list.ToArray();
      UI_grid_CamViews_Lib.isRowSelected = newSelectedRows.ToArray().ints_to_selected_bools(list.Count);
      CamViews_Lib_To_UI();
      row = clamp(newSelectedRows[0], 0, UI_grid_CamViews_Lib.rowNumberButtons.Count - 1);
    }
    var b = UI_grid_CamViews_Lib.rowNumberButtons[row]; b.schedule.Execute(() => { b.Focus(); });
  }
  public virtual void CamViews_Lib_OnDownArrow()
  {
    var row = ^1;
    if (CamViews_Lib.Length > 1 && !UI_grid_CamViews_Lib.isRowSelected[^1])
    {
      var list = CamViews_Lib.ToList();
      var newSelectedRows = new List<int>();
      for (int i = CamViews_Lib.Length - 1; i >= 0; i--)
        if (UI_grid_CamViews_Lib.isRowSelected[i]) { var item = list[i]; list.RemoveAt(i); list.Insert(i + 1, item); newSelectedRows.Add(i + 1); }
      CamViews_Lib = list.ToArray();
      UI_grid_CamViews_Lib.isRowSelected = newSelectedRows.ToArray().ints_to_selected_bools(list.Count);
      CamViews_Lib_To_UI();
      row = clamp(newSelectedRows[0], 0, UI_grid_CamViews_Lib.rowNumberButtons.Count - 1);
    }
    var b = UI_grid_CamViews_Lib.rowNumberButtons[row]; b.schedule.Execute(() => { b.Focus(); });
  }
  public virtual void CamViews_Lib_OnKeyDown(KeyDownEvent evt)
  {
    //print($"CamViews_Lib_OnKeyDown, {(evt.shiftKey ? "Shift-" : "")}{(evt.ctrlKey ? "Ctrl-" : "")}{(evt.altKey ? "Alt-" : "")}{evt.keyCode.ToString()}");
  }
  public virtual void CamViews_Lib_SaveView(int row) { }
  public virtual void CamViews_Lib_LoadView(int row) { }
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
    UI_group_CamViews_Lib?.Display_Tree();
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
    if (ValuesChanged) { gChanged = true; OnValueChanged(); ValuesChanged = UI_group_CamViews_Lib.Changed = false; }
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
  public bool group_CamViews_Lib { get => UI_group_CamViews_Lib?.v ?? false; set { if (UI_group_CamViews_Lib != null) UI_group_CamViews_Lib.v = value; } }
  public UI_TreeGroup UI_group_CamViews_Lib;
  public UI_grid UI_grid_CamViews_Lib;
  public CamView[] CamViews_Lib;
  public UI_TreeGroup ui_group_CamViews_Lib => UI_group_CamViews_Lib;
  [Serializable]
  public class uiData
  {
    public bool siUnits, group_CamViews_Lib;
    public CamView[] CamViews_Lib;
    public bool[] CamViews_Lib_DisplayCols;
    public float CamViews_Lib_VScroll;
    public uint CamViews_Lib_DisplayRowN;
    public bool CamViews_Lib_isExpanded;
    public string CamViews_Lib_selectedRows;
    public int CamViews_Lib_lastClickedRow;
  }
  public virtual void InitBuffers()
  {
    InitBuffers0_GS();
    AddComputeBuffer(ref gViews_Lib, nameof(gViews_Lib), 1);
    InitKernels();
    InitBuffers1_GS();
    isInitBuffers = true;
  }
  public virtual void InitBuffers0_GS() { }
  public virtual void InitBuffers1_GS() { }
  [Serializable]
  public struct GViews_Lib
  {
  };
  public RWStructuredBuffer<GViews_Lib> gViews_Lib;
  public struct CamView { public string viewName; public float3 viewCenter; public float viewDist; public float2 viewTiltSpin; public uint viewProjection; };
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