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
public class gsViews_Doc_ : GS, IViews_Lib
{
  [HideInInspector] public uiData data;
  [HideInInspector] public GViews_Doc g; public GViews_Doc G { get { gViews_Doc.GetData(); return gViews_Doc[0]; } }
  [JsonConverter(typeof(StringEnumConverter))] public enum Views_Lib_ProjectionMode : uint { Automatic, Perspective, Orthographic }
  public const uint Views_Lib_ProjectionMode_Automatic = 0, Views_Lib_ProjectionMode_Perspective = 1, Views_Lib_ProjectionMode_Orthographic = 2;
  [HideInInspector] public bool ValuesChanged, gChanged;
  public static gsViews_Doc This;
  public virtual void Awake() { This = this as gsViews_Doc; Awake_GS(); }
  public virtual void Awake_GS()
  {
    if(!GS_Views_Lib.CheckKey("you@gmail.com", "2024/12/10", 123456)) Status($"Views_Lib not registered, check email, expiration, and key in gsViews_Doc_GS class");
    if(!GS_OCam_Lib.CheckKey("you@gmail.com", "2024/12/10", 123456)) Status($"OCam_Lib not registered, check email, expiration, and key in gsViews_Doc_GS class");
    if(!GS_Report_Lib.CheckKey("you@gmail.com", "2024/12/10", 123456)) Status($"Report_Lib not registered, check email, expiration, and key in gsViews_Doc_GS class");
  }
  public virtual void Start() { Build_UI(); Load_UI(); Start0_GS(); InitBuffers(); Start1_GS(); }
  public virtual void Start0_GS()
  {
    Views_Lib_Start0_GS();
  }
  public virtual void Start1_GS()
  {
    Views_Lib_Start1_GS();
  }
  public override void onLoaded()
  {
    base.onLoaded();
    GS_Views_Lib.onLoaded(this);
    GS_OCam_Lib.onLoaded(OCam_Lib);
    GS_Report_Lib.onLoaded(Report_Lib);
  }
  [HideInInspector] public bool already_quited = false;
  public override void OnApplicationQuit() { Save_UI(); OnApplicationQuit_GS(); base.OnApplicationQuit(); already_quited = true; }
  public virtual void OnApplicationQuit_GS()
  {
    Views_Lib_OnApplicationQuit_GS();
  }
  public class _UI_Views_Lib_CamView
  {
    public class UI_Views_Lib_CamView_Items
    {
      public gsViews_Doc_ gs;
      public int row;
      public UI_Views_Lib_CamView_Items(gsViews_Doc_ gs) { this.gs = gs; }
      public UI_string UI_viewName => gs.UI_grid_Views_Lib_CamViews_Lib.RowItems[row][0] as UI_string; public string viewName { get => UI_viewName.v; set => UI_viewName.v = value; }
      public UI_float UI_viewDist => gs.UI_grid_Views_Lib_CamViews_Lib.RowItems[row][1] as UI_float; public float viewDist { get => UI_viewDist.v; set => UI_viewDist.v = value; }
      public UI_float3 UI_viewCenter => gs.UI_grid_Views_Lib_CamViews_Lib.RowItems[row][2] as UI_float3; public float3 viewCenter { get => UI_viewCenter.v; set => UI_viewCenter.v = value; }
      public UI_float2 UI_viewTiltSpin => gs.UI_grid_Views_Lib_CamViews_Lib.RowItems[row][3] as UI_float2; public float2 viewTiltSpin { get => UI_viewTiltSpin.v; set => UI_viewTiltSpin.v = value; }
      public UI_enum UI_viewProjection => gs.UI_grid_Views_Lib_CamViews_Lib.RowItems[row][4] as UI_enum; public Views_Lib_ProjectionMode viewProjection { get => (Views_Lib_ProjectionMode)UI_viewProjection.v; set => UI_viewProjection.v = value.To_uint(); }
      public UI_bool UI_Show_UI => gs.UI_grid_Views_Lib_CamViews_Lib.RowItems[row][5] as UI_bool; public bool Show_UI { get => UI_Show_UI.v; set => UI_Show_UI.v = value; }
      public UI_bool UI_Show_Legend => gs.UI_grid_Views_Lib_CamViews_Lib.RowItems[row][6] as UI_bool; public bool Show_Legend { get => UI_Show_Legend.v; set => UI_Show_Legend.v = value; }
    }
    public gsViews_Doc_ gs;
    public UI_Views_Lib_CamView_Items ui_Views_Lib_CamView_Items;
    public _UI_Views_Lib_CamView(gsViews_Doc_ gs) { this.gs = gs; ui_Views_Lib_CamView_Items = new UI_Views_Lib_CamView_Items(gs); }
    public UI_Views_Lib_CamView_Items this[int i] { get { ui_Views_Lib_CamView_Items.row = i; return ui_Views_Lib_CamView_Items; } }
  }
  public _UI_Views_Lib_CamView UI_Views_Lib_CamViews_Lib;
  public override void ui_to_data()
  {
    if (data == null) return;
    data.siUnits = siUnits;
    data.group_UI = group_UI;
    data.width = width;
    data.Views_Lib_group_CamViews_Lib = Views_Lib_group_CamViews_Lib;
    data.Views_Lib_CamViews_Lib = Views_Lib_CamViews_Lib;
    data.Views_Lib_CamViews_Lib_DisplayCols = UI_grid_Views_Lib_CamViews_Lib?.displayColumns?.Select(a => a.v).ToArray() ?? default;
    data.Views_Lib_CamViews_Lib_VScroll = UI_grid_Views_Lib_CamViews_Lib?.VScroll.value ?? default;
    data.Views_Lib_CamViews_Lib_DisplayRowN = UI_grid_Views_Lib_CamViews_Lib?.dispRowN.v ?? default;
    data.Views_Lib_CamViews_Lib_isExpanded = UI_grid_Views_Lib_CamViews_Lib?.isExpanded ?? default;
    data.Views_Lib_CamViews_Lib_lastClickedRow = UI_grid_Views_Lib_CamViews_Lib?.lastClickedRow ?? default;
    data.Views_Lib_CamViews_Lib_selectedRows = UI_grid_Views_Lib_CamViews_Lib?.isRowSelected.bools_to_RangeStr() ?? default;
  }
  public override void data_to_ui()
  {
    if (data == null) return;
    group_UI = data.group_UI;
    width = ui_txt_str.Contains("\"width\"") ? data.width : 0f;
    Views_Lib_group_CamViews_Lib = data.Views_Lib_group_CamViews_Lib;
    Views_Lib_CamViews_Lib = data.Views_Lib_CamViews_Lib;
    UI_Views_Lib_CamViews_Lib = new _UI_Views_Lib_CamView(this);
    "Views_Lib_CamViews_Lib_To_UI".InvokeMethod(this);
    for (int i = 0; i < data.Views_Lib_CamViews_Lib_DisplayCols?.Length && i < UI_grid_Views_Lib_CamViews_Lib.displayColumns.Count; i++)
    {
      UI_grid_Views_Lib_CamViews_Lib.displayColumns[i].v = data.Views_Lib_CamViews_Lib_DisplayCols[i];
      var item = UI_grid_Views_Lib_CamViews_Lib.RowItems[0][i];
      if(UI_grid_Views_Lib_CamViews_Lib.headerButtons[i].unitLabel != null)
        UI_grid_Views_Lib_CamViews_Lib.headerButtons[i].unitLabel.style.display = DisplayIf(item.siUnit != siUnit.Null || item.usUnit != usUnit.Null || item.Unit != Unit.Null);
    }
    UI_grid_Views_Lib_CamViews_Lib.VScroll.value = data.Views_Lib_CamViews_Lib_VScroll;
    UI_grid_Views_Lib_CamViews_Lib.dispRowN.v = data.Views_Lib_CamViews_Lib_DisplayRowN == 0 ? 20 : data.Views_Lib_CamViews_Lib_DisplayRowN;
    UI_grid_Views_Lib_CamViews_Lib.isExpanded = data.Views_Lib_CamViews_Lib_isExpanded;
    UI_grid_Views_Lib_CamViews_Lib.selectedRows = data.Views_Lib_CamViews_Lib_selectedRows;
    UI_grid_Views_Lib_CamViews_Lib.lastClickedRow = data.Views_Lib_CamViews_Lib_lastClickedRow;
    Views_Lib_CamViews_Lib ??= new Views_Lib_CamView[] { };
    UI_grid_Views_Lib_CamViews_Lib.isRowSelected = data.Views_Lib_CamViews_Lib_selectedRows.RangeStr_to_bools(Views_Lib_CamViews_Lib.Length);
    UI_grid_Views_Lib_CamViews_Lib.DrawGrid();
  }
  public List<Views_Lib_CamView> Views_Lib_CamViews_Lib_CopyPaste;
  public virtual void Views_Lib_CamViews_Lib_OnButtonClicked(int row, int col)
  {
    var UI_grid = UI_grid_Views_Lib_CamViews_Lib;
    var name = UI_grid.RowItems[row][col].name.After("UI_method_").BeforeLast("_");
    if (name == "SaveView") Views_Lib_CamViews_Lib_SaveView(UI_grid.StartRow + row);
    else if (name == "LoadView") Views_Lib_CamViews_Lib_LoadView(UI_grid.StartRow + row);
  }
  public virtual void Views_Lib_CamViews_Lib_OnRowNumberButtonClicked(int row) { }
  public virtual void Views_Lib_CamViews_Lib_OnHeaderButtonClicked(string label)
  {
    if (Views_Lib_CamViews_Lib.Length < 2) return;
    Views_Lib_CamViews_Lib = label switch
    {
      "Name" => Views_Lib_CamViews_Lib[0].viewName.CompareTo(Views_Lib_CamViews_Lib[^1].viewName) < 0 ? Views_Lib_CamViews_Lib.OrderByDescending(a => a.viewName).ToArray() : Views_Lib_CamViews_Lib.OrderBy(a => a.viewName).ToArray(),
      "Dist" => Views_Lib_CamViews_Lib[0].viewDist.CompareTo(Views_Lib_CamViews_Lib[^1].viewDist) < 0 ? Views_Lib_CamViews_Lib.OrderByDescending(a => a.viewDist).ToArray() : Views_Lib_CamViews_Lib.OrderBy(a => a.viewDist).ToArray(),
      "Center" => Views_Lib_CamViews_Lib[0].viewCenter.CompareTo(Views_Lib_CamViews_Lib[^1].viewCenter) < 0 ? Views_Lib_CamViews_Lib.OrderByDescending(a => a.viewCenter).ToArray() : Views_Lib_CamViews_Lib.OrderBy(a => a.viewCenter).ToArray(),
      "Tilt Spin" => Views_Lib_CamViews_Lib[0].viewTiltSpin.CompareTo(Views_Lib_CamViews_Lib[^1].viewTiltSpin) < 0 ? Views_Lib_CamViews_Lib.OrderByDescending(a => a.viewTiltSpin).ToArray() : Views_Lib_CamViews_Lib.OrderBy(a => a.viewTiltSpin).ToArray(),
      "Projection" => Views_Lib_CamViews_Lib[0].viewProjection.CompareTo(Views_Lib_CamViews_Lib[^1].viewProjection) < 0 ? Views_Lib_CamViews_Lib.OrderByDescending(a => a.viewProjection).ToArray() : Views_Lib_CamViews_Lib.OrderBy(a => a.viewProjection).ToArray(),
      "UI" => Views_Lib_CamViews_Lib[0].Show_UI.CompareTo(Views_Lib_CamViews_Lib[^1].Show_UI) < 0 ? Views_Lib_CamViews_Lib.OrderByDescending(a => a.Show_UI).ToArray() : Views_Lib_CamViews_Lib.OrderBy(a => a.Show_UI).ToArray(),
      "Legend" => Views_Lib_CamViews_Lib[0].Show_Legend.CompareTo(Views_Lib_CamViews_Lib[^1].Show_Legend) < 0 ? Views_Lib_CamViews_Lib.OrderByDescending(a => a.Show_Legend).ToArray() : Views_Lib_CamViews_Lib.OrderBy(a => a.Show_Legend).ToArray(),
      _ => default,
    };
    Views_Lib_CamViews_Lib_To_UI();
  }
  public virtual void UI_To_Views_Lib_CamViews_Lib()
  {
    if (Views_Lib_CamViews_Lib == null) return;
    var UI_grid = UI_grid_Views_Lib_CamViews_Lib;
    int startRow = UI_grid.StartRow, i1 = min(UI_grid.DisplayRowN, Views_Lib_CamViews_Lib.Length - startRow);
    i1 = min(i1, UI_grid.RowItems.Count);
    for (int i = 0; i < i1; i++)
    {
      var ui = UI_Views_Lib_CamViews_Lib[i];
      var row = Views_Lib_CamViews_Lib[i + startRow];
      row.viewName = ui.viewName;
      row.viewDist = ui.viewDist;
      row.viewCenter = ui.viewCenter;
      row.viewTiltSpin = ui.viewTiltSpin;
      row.viewProjection = (uint)ui.viewProjection;
      row.Show_UI = ui.Show_UI.To_uint();
      row.Show_Legend = ui.Show_Legend.To_uint();
      Views_Lib_CamViews_Lib[i + startRow] = row;
    }
  }
  protected bool in_Views_Lib_CamViews_Lib_To_UI = false;
  public virtual bool Views_Lib_CamViews_Lib_To_UI()
  {
    if (Views_Lib_CamViews_Lib == null || in_Views_Lib_CamViews_Lib_To_UI) return false;
    isGridBuilding = true;
    in_Views_Lib_CamViews_Lib_To_UI = true;
    var UI_grid = UI_grid_Views_Lib_CamViews_Lib;
    int startRow = UI_grid.StartRow, i1 = min(UI_grid.DisplayRowN, Views_Lib_CamViews_Lib.Length - startRow);
    i1 = min(i1, UI_grid.RowItems.Count);
    for (int i = 0; i < i1; i++)
    {
      var ui = UI_Views_Lib_CamViews_Lib[i];
      var row = Views_Lib_CamViews_Lib[i + startRow];
      ui.viewName = row.viewName;
      ui.viewDist = row.viewDist;
      ui.viewCenter = row.viewCenter;
      ui.viewTiltSpin = row.viewTiltSpin;
      ui.viewProjection = (Views_Lib_ProjectionMode)row.viewProjection;
      ui.Show_UI = row.Show_UI.To_bool();
      ui.Show_Legend = row.Show_Legend.To_bool();
    }
    UI_grid.DrawGrid();
    in_Views_Lib_CamViews_Lib_To_UI = false;
    isGridBuilding = false;
    return true;
  }
  public const int Views_Lib_CamViews_Lib_viewName_Col = 0, Views_Lib_CamViews_Lib_viewDist_Col = 1, Views_Lib_CamViews_Lib_viewCenter_Col = 2, Views_Lib_CamViews_Lib_viewTiltSpin_Col = 3, Views_Lib_CamViews_Lib_viewProjection_Col = 4, Views_Lib_CamViews_Lib_Show_UI_Col = 5, Views_Lib_CamViews_Lib_Show_Legend_Col = 6;
  public virtual bool Views_Lib_CamViews_Lib_ShowIf(int row, int col)
  {
    return true;
  }
  public virtual void base_Views_Lib_CamViews_Lib_OnAddRow() { var list = Views_Lib_CamViews_Lib.ToList(); list.Add(default); Views_Lib_CamViews_Lib = list.ToArray(); }
  public virtual void Views_Lib_CamViews_Lib_viewName_OnValueChanged(int row, string previousValue) { }
  public virtual void Views_Lib_CamViews_Lib_viewDist_OnValueChanged(int row, float previousValue) { }
  public virtual void Views_Lib_CamViews_Lib_viewCenter_OnValueChanged(int row, float3 previousValue) { }
  public virtual void Views_Lib_CamViews_Lib_viewTiltSpin_OnValueChanged(int row, float2 previousValue) { }
  public virtual void Views_Lib_CamViews_Lib_viewProjection_OnValueChanged(int row, Views_Lib_ProjectionMode previousValue) { }
  public virtual void Views_Lib_CamViews_Lib_Show_UI_OnValueChanged(int row, bool previousValue) { }
  public virtual void Views_Lib_CamViews_Lib_Show_Legend_OnValueChanged(int row, bool previousValue) { }
  public virtual void Views_Lib_CamViews_Lib_OnValueChanged(int row, int col)
  {
    if (!ui_loaded) return;
    var UI_grid = UI_grid_Views_Lib_CamViews_Lib;
    int startRow = UI_grid.StartRow;
    var ui = UI_Views_Lib_CamViews_Lib[row];
    if (row + startRow >= Views_Lib_CamViews_Lib.Length) return;
    var data = Views_Lib_CamViews_Lib[row + startRow];
    if (col == Views_Lib_CamViews_Lib_viewName_Col && data.viewName != ui.viewName) { var v = data.viewName; data.viewName = ui.viewName; Views_Lib_CamViews_Lib[row + startRow] = data; Views_Lib_CamViews_Lib_viewName_OnValueChanged(row + startRow, v); }
    else if (col == Views_Lib_CamViews_Lib_viewDist_Col && any(data.viewDist != ui.viewDist)) { var v = data.viewDist; data.viewDist = ui.viewDist; Views_Lib_CamViews_Lib[row + startRow] = data; Views_Lib_CamViews_Lib_viewDist_OnValueChanged(row + startRow, v); }
    else if (col == Views_Lib_CamViews_Lib_viewCenter_Col && any(data.viewCenter != ui.viewCenter)) { var v = data.viewCenter; data.viewCenter = ui.viewCenter; Views_Lib_CamViews_Lib[row + startRow] = data; Views_Lib_CamViews_Lib_viewCenter_OnValueChanged(row + startRow, v); }
    else if (col == Views_Lib_CamViews_Lib_viewTiltSpin_Col && any(data.viewTiltSpin != ui.viewTiltSpin)) { var v = data.viewTiltSpin; data.viewTiltSpin = ui.viewTiltSpin; Views_Lib_CamViews_Lib[row + startRow] = data; Views_Lib_CamViews_Lib_viewTiltSpin_OnValueChanged(row + startRow, v); }
    else if (col == Views_Lib_CamViews_Lib_viewProjection_Col && (Views_Lib_ProjectionMode)data.viewProjection != ui.viewProjection) { var v = (Views_Lib_ProjectionMode)data.viewProjection; data.viewProjection = (uint)ui.viewProjection; Views_Lib_CamViews_Lib[row + startRow] = data; Views_Lib_CamViews_Lib_viewProjection_OnValueChanged(row + startRow, v); }
    else if (col == Views_Lib_CamViews_Lib_Show_UI_Col && data.Show_UI != ui.Show_UI.To_uint()) { var v = data.Show_UI; data.Show_UI = ui.Show_UI.To_uint(); Views_Lib_CamViews_Lib[row + startRow] = data; Views_Lib_CamViews_Lib_Show_UI_OnValueChanged(row + startRow, v.To_bool()); }
    else if (col == Views_Lib_CamViews_Lib_Show_Legend_Col && data.Show_Legend != ui.Show_Legend.To_uint()) { var v = data.Show_Legend; data.Show_Legend = ui.Show_Legend.To_uint(); Views_Lib_CamViews_Lib[row + startRow] = data; Views_Lib_CamViews_Lib_Show_Legend_OnValueChanged(row + startRow, v.To_bool()); }
  }
  public virtual int Views_Lib_CamViews_Lib_GetGridArrayLength() => Views_Lib_CamViews_Lib?.Length ?? 0;
  public virtual int Views_Lib_CamViews_Lib_SelectedRow
  {
    get { for (int i = 0; i < Views_Lib_CamViews_Lib.Length; i++) if (UI_grid_Views_Lib_CamViews_Lib.isRowSelected[i]) return i; return -1; }
    set
    {
      for (int i = 0; i < Views_Lib_CamViews_Lib.Length; i++) UI_grid_Views_Lib_CamViews_Lib.isRowSelected[i] = value == i;
      UI_grid_Views_Lib_CamViews_Lib.lastClickedRow = value;
      UI_grid_Views_Lib_CamViews_Lib.DrawGrid();
    }
  }
  public virtual int[] Views_Lib_CamViews_Lib_SelectedRows
  {
    get => UI_grid_Views_Lib_CamViews_Lib.isRowSelected.Select((a, i) => new { a, i }).Where(a => a.a).Select(a => a.i).ToArray();
    set
    {
      for (int i = 0; i < Views_Lib_CamViews_Lib.Length; i++) UI_grid_Views_Lib_CamViews_Lib.isRowSelected[i] = false;
      for (int i = 0; i < value.Length; i++) { int row = value[i]; UI_grid_Views_Lib_CamViews_Lib.isRowSelected[row] = true; UI_grid_Views_Lib_CamViews_Lib.lastClickedRow = row; }
      UI_grid_Views_Lib_CamViews_Lib.DrawGrid();
    }
  }
  public virtual void Views_Lib_CamViews_Lib_OnCut()
  {
    Views_Lib_CamViews_Lib_OnCopy();
    Views_Lib_CamViews_Lib = Views_Lib_CamViews_Lib.Except(Views_Lib_CamViews_Lib_CopyPaste).ToArray();
    UI_grid_Views_Lib_CamViews_Lib.StartRow = min(UI_grid_Views_Lib_CamViews_Lib.StartRow, max(0, Views_Lib_CamViews_Lib_CopyPaste.Count - UI_grid_Views_Lib_CamViews_Lib.DisplayRowN));
    UI_grid_Views_Lib_CamViews_Lib.isRowSelected = new bool[Views_Lib_CamViews_Lib_CopyPaste.Count];
    UI_grid_Views_Lib_CamViews_Lib.DrawGrid();
  }
  public virtual void Views_Lib_CamViews_Lib_OnCopy() { Views_Lib_CamViews_Lib_CopyPaste = Views_Lib_CamViews_Lib.Where((a, i) => UI_grid_Views_Lib_CamViews_Lib.isRowSelected[i]).Select(a => a).ToList(); }
  public virtual void Views_Lib_CamViews_Lib_OnPaste()
  {
    var list = Views_Lib_CamViews_Lib.ToList();
    var newSelectedRows = new List<int>();
    for (int i = Views_Lib_CamViews_Lib.Length - 1; i >= 0; i--)
      if (UI_grid_Views_Lib_CamViews_Lib.isRowSelected[i])
      {
        for (int j = 0; j < Views_Lib_CamViews_Lib_CopyPaste.Count; j++) { list.Insert(i + j, Views_Lib_CamViews_Lib_CopyPaste[j]); newSelectedRows.Add(i + j); }
        break;
      }
    Views_Lib_CamViews_Lib = list.ToArray();
    UI_grid_Views_Lib_CamViews_Lib.isRowSelected = newSelectedRows.ToArray().ints_to_selected_bools(list.Count);
    Views_Lib_CamViews_Lib_To_UI();
  }
  public virtual void Views_Lib_CamViews_Lib_OnInsert()
  {
    var list = Views_Lib_CamViews_Lib.ToList();
    var newSelectedRows = new List<int>();
    for (int i = 0; i < Views_Lib_CamViews_Lib.Length; i++) if (UI_grid_Views_Lib_CamViews_Lib.isRowSelected[i]) { list.Insert(i, default); newSelectedRows.Add(i); }
    Views_Lib_CamViews_Lib = list.ToArray();
    UI_grid_Views_Lib_CamViews_Lib.isRowSelected = newSelectedRows.ToArray().ints_to_selected_bools(list.Count);
    Views_Lib_CamViews_Lib_To_UI();
  }
  public virtual void Views_Lib_CamViews_Lib_OnDelete() { Views_Lib_CamViews_Lib_OnCut(); }
  public virtual void Views_Lib_CamViews_Lib_OnUpArrow()
  {
    int row = 1;
    if (Views_Lib_CamViews_Lib.Length > 1 && !UI_grid_Views_Lib_CamViews_Lib.isRowSelected[0])
    {
      var list = Views_Lib_CamViews_Lib.ToList();
      var newSelectedRows = new List<int>();
      for (int i = 0; i < Views_Lib_CamViews_Lib.Length; i++)
        if (UI_grid_Views_Lib_CamViews_Lib.isRowSelected[i]) { var item = list[i]; list.RemoveAt(i); list.Insert(i - 1, item); newSelectedRows.Add(i - 1); }
      Views_Lib_CamViews_Lib = list.ToArray();
      UI_grid_Views_Lib_CamViews_Lib.isRowSelected = newSelectedRows.ToArray().ints_to_selected_bools(list.Count);
      Views_Lib_CamViews_Lib_To_UI();
      row = clamp(newSelectedRows[0], 0, UI_grid_Views_Lib_CamViews_Lib.rowNumberButtons.Count - 1);
    }
    var b = UI_grid_Views_Lib_CamViews_Lib.rowNumberButtons[row]; b.schedule.Execute(() => { b.Focus(); });
  }
  public virtual void Views_Lib_CamViews_Lib_OnDownArrow()
  {
    var row = ^1;
    if (Views_Lib_CamViews_Lib.Length > 1 && !UI_grid_Views_Lib_CamViews_Lib.isRowSelected[^1])
    {
      var list = Views_Lib_CamViews_Lib.ToList();
      var newSelectedRows = new List<int>();
      for (int i = Views_Lib_CamViews_Lib.Length - 1; i >= 0; i--)
        if (UI_grid_Views_Lib_CamViews_Lib.isRowSelected[i]) { var item = list[i]; list.RemoveAt(i); list.Insert(i + 1, item); newSelectedRows.Add(i + 1); }
      Views_Lib_CamViews_Lib = list.ToArray();
      UI_grid_Views_Lib_CamViews_Lib.isRowSelected = newSelectedRows.ToArray().ints_to_selected_bools(list.Count);
      Views_Lib_CamViews_Lib_To_UI();
      row = clamp(newSelectedRows[0], 0, UI_grid_Views_Lib_CamViews_Lib.rowNumberButtons.Count - 1);
    }
    var b = UI_grid_Views_Lib_CamViews_Lib.rowNumberButtons[row]; b.schedule.Execute(() => { b.Focus(); });
  }
  public virtual void Views_Lib_CamViews_Lib_OnKeyDown(KeyDownEvent evt)
  {
    //print($"Views_Lib_CamViews_Lib_OnKeyDown, {(evt.shiftKey ? "Shift-" : "")}{(evt.ctrlKey ? "Ctrl-" : "")}{(evt.altKey ? "Alt-" : "")}{evt.keyCode.ToString()}");
  }
  public virtual void base_Views_Lib_CamViews_Lib_SaveView(int row) { }
  public virtual void base_Views_Lib_CamViews_Lib_LoadView(int row) { }
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
    if (UI_width.Changed || width != UI_width.v) width = UI_width.v;
    if (ValuesChanged) { gChanged = true; OnValueChanged(); ValuesChanged = UI_group_UI.Changed = UI_width.Changed = UI_Views_Lib_group_CamViews_Lib.Changed = false; }
    Views_Lib_LateUpdate1_GS();
    LateUpdate1_GS();
    if(lateUpdateI++ == uint_max) lateUpdateI = 100;
  }
  public virtual void LateUpdate0_GS()
  {
    Views_Lib_LateUpdate0_GS();
  }
  public virtual void LateUpdate1_GS()
  {
    Views_Lib_LateUpdate1_GS();
  }
  public override void Update()
  {
    base.Update();
    if (!ui_loaded) return;
    Update0_GS();
    Views_Lib_Update1_GS();
    Update1_GS();
  }
  public virtual void Update0_GS()
  {
    Views_Lib_Update0_GS();
  }
  public virtual void Update1_GS()
  {
    Views_Lib_Update1_GS();
  }
  public override void OnValueChanged()
  {
    if (!ui_loaded) return;
    OnValueChanged_GS();
  }
  public override void OnValueChanged_GS()
  {
    Views_Lib_OnValueChanged_GS();
    var type = "gsReport_Lib".ToType();
    if (type != null) ((GS)GetComponent(type))?.OnValueChanged_GS();

  }
  public string[] Scenes_in_Build => new string[] { "Assets/GS_Docs/gsADraw_Doc/ADraw_Doc.unity" };
  public virtual float width { get => g.width; set { if (any(g.width != value) || any(UI_width.v != value)) { g.width = UI_width.v = value; ValuesChanged = gChanged = true; } } }
  public bool group_UI { get => UI_group_UI?.v ?? false; set { if (UI_group_UI != null) UI_group_UI.v = value; } }
  public bool Views_Lib_group_CamViews_Lib { get => UI_Views_Lib_group_CamViews_Lib?.v ?? false; set { if (UI_Views_Lib_group_CamViews_Lib != null) UI_Views_Lib_group_CamViews_Lib.v = value; } }
  public UI_TreeGroup UI_group_UI, UI_Views_Lib_group_CamViews_Lib;
  public UI_float UI_width;
  public UI_grid UI_grid_Views_Lib_CamViews_Lib;
  public Views_Lib_CamView[] Views_Lib_CamViews_Lib;
  public UI_TreeGroup ui_group_UI => UI_group_UI;
  public UI_float ui_width => UI_width;
  public UI_TreeGroup ui_Views_Lib_group_CamViews_Lib => UI_Views_Lib_group_CamViews_Lib;
  [Serializable]
  public class uiData
  {
    public bool siUnits, group_UI, Views_Lib_group_CamViews_Lib;
    public float width;
    public Views_Lib_CamView[] Views_Lib_CamViews_Lib;
    public bool[] Views_Lib_CamViews_Lib_DisplayCols;
    public float Views_Lib_CamViews_Lib_VScroll;
    public uint Views_Lib_CamViews_Lib_DisplayRowN;
    public bool Views_Lib_CamViews_Lib_isExpanded;
    public string Views_Lib_CamViews_Lib_selectedRows;
    public int Views_Lib_CamViews_Lib_lastClickedRow;
  }
  public virtual void InitBuffers()
  {
    InitBuffers0_GS();
    AddComputeBuffer(ref gViews_Doc, nameof(gViews_Doc), 1);
    InitKernels();
    InitBuffers1_GS();
    isInitBuffers = true;
  }
  public virtual void InitBuffers0_GS()
  {
    Views_Lib_InitBuffers0_GS();
  }
  public virtual void InitBuffers1_GS()
  {
    Views_Lib_InitBuffers1_GS();
  }
  [Serializable]
  public struct GViews_Doc
  {
    public float width;
  };
  public RWStructuredBuffer<GViews_Doc> gViews_Doc;
  public struct Views_Lib_CamView { public string viewName; public float viewDist; public float3 viewCenter; public float2 viewTiltSpin; public uint viewProjection; public uint Show_UI, Show_Legend; };
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
  public virtual void onRenderObject_GS(ref bool render, ref bool cpu)
  {
    Views_Lib_onRenderObject_GS(ref render, ref cpu);
  }
  public virtual float4 frag_GS(v2f i, float4 color) => color;
  public virtual float4 frag(v2f i)
  {
    float4 color = i.color;
    color = frag_GS(i, color);
    if (color.a == 0) Discard(0);
    return color;
  }
  #region <Views_Lib>
  public UI_TreeGroup Views_Lib_ui_Views_Lib_group_CamViews_Lib => UI_Views_Lib_group_CamViews_Lib;
  public virtual void Views_Lib_onLoaded() => GS_Views_Lib.onLoaded(this);
  public virtual void Views_Lib_LateUpdate1_GS()
  {
    base_Views_Lib_LateUpdate1_GS();
    for (int i = 0; i < min(Views_Lib_CamViews_Lib.Length, 10); i++) if (GetKeyDown(CtrlAlt, (char)('1' + i))) { Views_Lib_CamViews_Lib_LoadView(i); break; }
  }
  public virtual void Views_Lib_CamViews_Lib_OnAddRow()
  {
    base_Views_Lib_CamViews_Lib_OnAddRow();
    var view = Views_Lib_CamViews_Lib[^1];
    view.viewName = $"View {Views_Lib_CamViews_Lib.Length}";
    Views_Lib_CamViews_Lib[^1] = view;
    Views_Lib_CamViews_Lib_SaveView(Views_Lib_CamViews_Lib.Length - 1);
  }
  public virtual void Views_Lib_SaveCamView(ref Views_Lib_CamView view) { }
  public virtual void Views_Lib_CamViews_Lib_SaveView(int row)
  {
    var view = Views_Lib_CamViews_Lib[row];
    view.viewTiltSpin = Views_Lib_OCam.tiltSpin; view.viewDist = Views_Lib_OCam.dist; view.viewCenter = Views_Lib_OCam.center; view.viewProjection = (uint)Views_Lib_OCam.projection;
    Views_Lib_SaveCamView(ref view);
    Views_Lib_CamViews_Lib[row] = view;
    Views_Lib_CamViews_Lib_To_UI();
  }
  public virtual void Views_Lib_LoadCamView(ref Views_Lib_CamView view) { }
  public virtual void Views_Lib_CamViews_Lib_LoadView(int row)
  {
    if (row < Views_Lib_CamViews_Lib.Length)
    {
      var view = Views_Lib_CamViews_Lib[row];
      Views_Lib_OCam.tiltSpin = view.viewTiltSpin; Views_Lib_OCam.dist = view.viewDist; Views_Lib_OCam.center = view.viewCenter;
      Views_Lib_OCam.SetProjection(view.viewProjection);
      Views_Lib_LoadCamView(ref view);
    }
  }
  public void Views_Lib_SaveView(int row) => Views_Lib_CamViews_Lib_SaveView(row - 1);
  public void Views_Lib_LoadView(int row) => Views_Lib_CamViews_Lib_LoadView(row - 1);
  gsOCam_Lib Views_Lib__OCam; public gsOCam_Lib Views_Lib_OCam => Views_Lib__OCam = Views_Lib__OCam ?? Add_Component_to_gameObject<gsOCam_Lib>();

  public virtual void base_Views_Lib_Start0_GS() { }
  public virtual void base_Views_Lib_Start1_GS() { }
  public virtual void base_Views_Lib_OnApplicationQuit() { Save_UI(); OnApplicationQuit_GS(); base.OnApplicationQuit(); already_quited = true; }
  public virtual void base_Views_Lib_LateUpdate0_GS() { }
  public virtual void base_Views_Lib_LateUpdate1_GS() { }
  public virtual void base_Views_Lib_Update0_GS() { }
  public virtual void base_Views_Lib_Update1_GS() { }
  public virtual void base_Views_Lib_OnValueChanged_GS()
  {
    var type = "gsReport_Lib".ToType();
    if (type != null) ((GS)GetComponent(type))?.OnValueChanged_GS();
  }
  public virtual void base_Views_Lib_InitBuffers0_GS() { }
  public virtual void base_Views_Lib_InitBuffers1_GS() { }
  public virtual void base_Views_Lib_onRenderObject_GS(ref bool render, ref bool cpu) { }
  public virtual float4 base_frag_Views_Lib_GS(v2f i, float4 color)
  {
    uint libI = roundu(i.tj.x);
    return color;
  }
  public virtual void Views_Lib_InitBuffers0_GS() { }
  public virtual void Views_Lib_InitBuffers1_GS() { }
  public virtual void Views_Lib_LateUpdate0_GS() { }
  public virtual void Views_Lib_Update0_GS() { }
  public virtual void Views_Lib_Update1_GS() { }
  public virtual void Views_Lib_Start0_GS() { }
  public virtual void Views_Lib_Start1_GS() { }
  public virtual void Views_Lib_OnValueChanged_GS() { }
  public virtual void Views_Lib_OnApplicationQuit_GS() { }
  public virtual void Views_Lib_onRenderObject_GS(ref bool render, ref bool cpu) { }
  public virtual float4 frag_Views_Lib_GS(v2f i, float4 color)
  {
    return color;
  }
  #endregion <Views_Lib>
  #region <OCam_Lib>
  gsOCam_Lib _OCam_Lib; public gsOCam_Lib OCam_Lib => _OCam_Lib = _OCam_Lib ?? Add_Component_to_gameObject<gsOCam_Lib>();
  #endregion <OCam_Lib>
  #region <Report_Lib>
  gsReport_Lib _Report_Lib; public gsReport_Lib Report_Lib => _Report_Lib = _Report_Lib ?? Add_Component_to_gameObject<gsReport_Lib>();
  #endregion <Report_Lib>
}