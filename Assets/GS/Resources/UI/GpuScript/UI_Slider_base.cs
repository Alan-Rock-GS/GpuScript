//using System.Security.Policy;
using System;
using Codice.CM.Common;
using UnityEngine;
using UnityEngine.UIElements;
using static GpuScript.GS;

namespace GpuScript
{
#if NEW_UI
  [UxmlElement]
  public partial class UI_Slider_base : UI_VisualElement
  {
    //public bool isPow2Slider, is_Pow10, is_Pow2, NearestDigit;
    [UxmlAttribute] public bool isPow2Slider, isPow10, isPow2, NearestDigit;
    [UxmlAttribute] public float Nearest = 0;
    [UxmlAttribute] public string formatString { get; set; }
    [UxmlAttribute] public string usFormat, siFormat;
    //bool valChanging = false;
    //string _val;
    //[UxmlAttribute]
    //public string val
    //{
    //  get => _val;
    //  set { _val = value; if (!valChanging && isEditor) { valChanging = true; Build(label, description, val, RangeMin, RangeMax, siUnit, usUnit, Unit, siFormat, usFormat, isReadOnly, tree); valChanging = false; } }
    //}
    bool valChanging = false;
    string _val;
    [UxmlAttribute]
    public string val
    {
      get => _val;
      set
      {
        _val = value;
        if (!valChanging && isEditor)
        {
          valChanging = true;
          Build(label, description, val, RangeMin, RangeMax, siUnit, usUnit, Unit, siFormat, usFormat, isReadOnly, tree);
          valChanging = false;
        }
      }
    }
    [UxmlAttribute] public string RangeMin { get; set; }
    [UxmlAttribute] public string RangeMax { get; set; }
    public static new void UXML(UI_Element e, AttGS att, string name, string label, string typeName)
    {
      if (att == null) return;
      UI_VisualElement.UXML(e, att, name, label, typeName);
      if (att.siUnit != siUnit.Null) e.uxml.Add($" si-unit=\"{att.siUnit}\"");
      if (att.usUnit != usUnit.Null) e.uxml.Add($" us-unit=\"{att.usUnit}\"");
      if (att.Unit != Unit.Null) e.uxml.Add($" unit=\"{att.Unit}\"");
      string siFormat = att.siFormat, usFormat = att.usFormat;
      if (att.Format.IsNotEmpty() && siFormat.IsEmpty() && usFormat.IsEmpty()) { siFormat = usFormat = att.Format; e.uxml.Add($" format-string=\"{att.Format}\""); } //?
      if (siFormat.IsNotEmpty()) e.uxml.Add($" si-format=\"{siFormat}\"");
      if (usFormat.IsNotEmpty()) e.uxml.Add($" us-format=\"{usFormat}\"");
      if (att.Min != null) e.uxml.Add($" range-min=\"{att.Min}\" range-max=\"{att.Max}\"");
      if (att.is_Pow2_Slider) e.uxml.Add($" is-pow2-slider=\"true\"");
      if (att.is_Pow_10) e.uxml.Add($" is-pow10=\"true\"");
      if (att.is_Pow_2) e.uxml.Add($" is-powPow2=\"true\"");
      if (att.Nearest != 0) e.uxml.Add($" nearest=\"{att.Nearest}\"");
      if (att.NearestDigit) e.uxml.Add($" nearest-digit=\"true\"");
      if (att.Val != null) e.uxml.Add($" val=\"{att.Val}\"");
    }
    public virtual void Build(string title, string description, string val, string rangeMin, string rangeMax,
      siUnit _siUnit, usUnit _usUnit, Unit _Unit, string siFormat, string usFormat, bool isReadOnly, string treeGroup_parent)
    {
      Build(title, description, val, rangeMin, rangeMax, isReadOnly, treeGroup_parent);
      if (usUnit == usUnit.Null && siUnit != siUnit.Null) usUnit = Match(siUnit); else if (usUnit != usUnit.Null && siUnit == siUnit.Null) siUnit = Match(usUnit);
      if (unitLabel != null) { unitLabel.text = unit; unitLabel.style.display = DisplayIf(unit.IsNotEmpty()); }
      this.siFormat = siFormat; this.usFormat = usFormat; formatString = format; textField.value = val;
    }
    public virtual void Build(string title, string description, siUnit _siUnit, usUnit _usUnit, Unit _Unit, string siFormat, string usFormat, bool isReadOnly, bool isGrid,
      bool isPow2Slider, bool isPow10, bool isPow2, float nearest, bool nearestDigit, string treeGroup_parent)
    {
      Build(title, description, isReadOnly, isGrid, treeGroup_parent);
      if (usUnit == usUnit.Null && siUnit != siUnit.Null) usUnit = Match(siUnit); else if (usUnit != usUnit.Null && siUnit == siUnit.Null) siUnit = Match(usUnit);
      if (unitLabel != null) { unitLabel.text = unit; unitLabel.HideIf(unit.IsEmpty() || isGrid); }
      if (headerLabel != null) headerLabel.HideIf(label.IsEmpty() || isGrid);
      this.siFormat = siFormat; this.usFormat = usFormat; formatString = format;
      this.isPow2Slider = isPow2Slider; Nearest = nearest.isNan() ? 0 : nearest; NearestDigit = nearestDigit;
    }
    public override bool _isGrid
    {
      get => base._isGrid;
      set
      {
        base._isGrid = value;
        if (unitLabel != null) { unitLabel.text = unit; unitLabel.HideIf(unit.IsEmpty() || isGrid); }
        if (headerLabel != null) headerLabel.HideIf(label.IsEmpty() || isGrid);
      }
    }
    //public virtual void Build(string title, string description, string val, string rangeMin, string rangeMax,
    //  siUnit _siUnit, usUnit _usUnit, Unit _Unit, string siFormat, string usFormat, bool isReadOnly, string treeGroup_parent)
    //{
    //  Build(title, description, val, rangeMin, rangeMax, isReadOnly, treeGroup_parent);
    //  if (usUnit == usUnit.Null && siUnit != siUnit.Null) usUnit = Match(siUnit); else if (usUnit != usUnit.Null && siUnit == siUnit.Null) siUnit = Match(usUnit);
    //  if (unitLabel != null) { unitLabel.text = unit; unitLabel.style.display = DisplayIf(unit.IsNotEmpty()); }
    //  this.siFormat = siFormat; this.usFormat = usFormat; formatString = format;
    //  textField.value = val;
    //  //textField.value = "0,1,2";
    //  //print($"val = {val}");
    //}
    public virtual void Build(string title, string description, string val, string format, bool isReadOnly, bool isGrid,
      bool isPow2Slider, bool isPow10, bool isPow2, float nearest, bool nearestDigit, string treeGroup_parent)
    {
      base.Build(title, description, isReadOnly, isGrid, treeGroup_parent);
      if (textField != null) { textField.value = val; textField.isReadOnly = isReadOnly; }
      this.isPow2Slider = isPow2Slider; this.isPow10 = isPow10; this.isPow2 = isPow2;
      Nearest = nearest;
      NearestDigit = nearestDigit;
      formatString = format;
      usFormat = siFormat = format;
    }
    public override string unit
    {
      get => base.unit;
      set
      {
        base.unit = value;
        if (usUnit == usUnit.Null && siUnit != siUnit.Null) usUnit = Match(siUnit); else if (usUnit != usUnit.Null && siUnit == siUnit.Null) siUnit = Match(usUnit);
        if (unitLabel != null) { unitLabel.text = unit; unitLabel.style.display = DisplayIf(unit.IsNotEmpty()); }
        //this.siFormat = siFormat; this.usFormat = usFormat; formatString = format; textField.value = val;
      }
    }
#elif !NEW_UI
  public class UI_Slider_base : UI_VisualElement
  {
    public bool isPow2Slider, isPow10, isPow2, NearestDigit;
    public float Nearest = 0;
    public string formatString { get; set; }
    public static new void UXML(UI_Element e, AttGS att, string name, string label, string typeName)
    {
      if (att == null) return;
      UI_VisualElement.UXML(e, att, name, label, typeName);
      if (att.siUnit != siUnit.Null) e.uxml.Add($" {typeName}_siUnit=\"{att.siUnit}\"");
      if (att.usUnit != usUnit.Null) e.uxml.Add($" {typeName}_usUnit=\"{att.usUnit}\"");
      if (att.Unit != Unit.Null) e.uxml.Add($" {typeName}_Unit=\"{att.Unit}\"");
      string siFormat = att.siFormat, usFormat = att.usFormat;
      if (att.Format.IsNotEmpty() && siFormat.IsEmpty() && usFormat.IsEmpty()) { siFormat = usFormat = att.Format; e.uxml.Add($" UI_Format=\"{att.Format}\""); }
      if (siFormat.IsNotEmpty()) e.uxml.Add($" {typeName}_siFormat=\"{siFormat}\"");
      if (usFormat.IsNotEmpty()) e.uxml.Add($" {typeName}_usFormat=\"{usFormat}\"");
      if (att.Val != null) e.uxml.Add($" {typeName}_value=\"{att.Val}\"");
      if (att.Min != null) e.uxml.Add($" {typeName}_min=\"{att.Min}\" {typeName}_max=\"{att.Max}\"");
      if (att.is_Pow2_Slider) e.uxml.Add($" UI_isPow2Slider=\"true\"");
      if (att.is_Pow_10) e.uxml.Add($" UI_isPow10=\"true\"");
      if (att.is_Pow_2) e.uxml.Add($" UI_isPow2=\"true\"");
      if (att.Nearest != 0) e.uxml.Add($" UI_Nearest=\"{att.Nearest}\"");
      if (att.NearestDigit) e.uxml.Add($" UI_NearestDigit=\"true\"");
    }
    public virtual void Build(string title, string description, string val, string rangeMin, string rangeMax,
      string _siUnit, string _usUnit, string _Unit, string siFormat, string usFormat, bool isReadOnly, string treeGroup_parent)
    {
      Build(title, description, val, rangeMin, rangeMax, isReadOnly, treeGroup_parent);
      siUnit = _siUnit.IsNotEmpty() ? _siUnit.ToEnum<siUnit>() : siUnit.Null;
      usUnit = _usUnit.IsNotEmpty() ? _usUnit.ToEnum<usUnit>() : usUnit.Null;
      Unit = _Unit.IsNotEmpty() ? _Unit.ToEnum<Unit>() : Unit.Null;
      if (usUnit == usUnit.Null && siUnit != siUnit.Null) usUnit = Match(siUnit);
      else if (usUnit != usUnit.Null && siUnit == siUnit.Null) siUnit = Match(usUnit);
      unitLabel.text = unit;
      unitLabel.style.display = DisplayIf(unit.IsNotEmpty());
      this.siFormat = siFormat;
      this.usFormat = usFormat;
      formatString = format;
    }
    public virtual void Build(string title, string description,
      string _siUnit, string _usUnit, string _Unit, string siFormat, string usFormat, bool isReadOnly, bool isGrid,
      bool isPow2Slider, bool isPow10, bool isPow2, float nearest, bool nearestDigit, string treeGroup_parent)
    {
      Build(title, description, isReadOnly, isGrid, treeGroup_parent);
      siUnit = _siUnit.IsNotEmpty() ? _siUnit.ToEnum<siUnit>() : siUnit.Null;
      usUnit = _usUnit.IsNotEmpty() ? _usUnit.ToEnum<usUnit>() : usUnit.Null;
      Unit = _Unit.IsNotEmpty() ? _Unit.ToEnum<Unit>() : Unit.Null;

      if (usUnit == usUnit.Null && siUnit != siUnit.Null) usUnit = Match(siUnit);
      else if (usUnit != usUnit.Null && siUnit == siUnit.Null) siUnit = Match(usUnit);

      if (unitLabel != null) { unitLabel.text = unit; unitLabel.HideIf(unit.IsEmpty() || isGrid); }
      if (headerLabel != null) headerLabel.HideIf(label.IsEmpty() || isGrid);

      this.siFormat = siFormat;
      this.usFormat = usFormat;
      formatString = format;
      this.isPow2Slider = isPow2Slider;
      Nearest = nearest.isNan() ? 0 : nearest;
      NearestDigit = nearestDigit;
    }
    public string usFormat, siFormat;
    public virtual void Build(string title, string description, string val, string format, bool isReadOnly, bool isGrid,
      bool isPow2Slider, bool isPow10, bool isPow2, float nearest, bool nearestDigit, string treeGroup_parent)
    {
      base.Build(title, description, isReadOnly, isGrid, treeGroup_parent);
      if (textField != null) { textField.value = val; textField.isReadOnly = isReadOnly; }
      this.isPow2Slider = isPow2Slider; this.isPow10 = isPow10; this.isPow2 = isPow2; Nearest = nearest; NearestDigit = nearestDigit; formatString = format; usFormat = siFormat = format;
    }
#endif //NEW_UI
    public VisualElement container;
    public Label headerLabel, unitLabel;
    public TextField textField;
    public Slider[] sliders;
    public uint GetNearest(uint v) => NearestDigit ? pow10(flooru(log10u(v))) : roundu(Nearest);
    public float GetNearest(float v) => NearestDigit ? pow10(floor(log10(v))) : Nearest;
    public float2 GetNearest(float2 v) => NearestDigit ? pow10(floor(log10(v))) : float2(Nearest);
    public float3 GetNearest(float3 v) => NearestDigit ? pow10(floor(log10(v))) : float3(Nearest);
    public float4 GetNearest(float4 v) => NearestDigit ? pow10(floor(log10(v))) : float4(Nearest);
    public virtual Slider[] GetSliders() => new Slider[] { this.Q<Slider>("slider_x") };
    public override void RegisterGridCallbacks(GS gs, UI_grid grid, int gridRow, int gridCol)
    {
      base.RegisterGridCallbacks(gs, grid, gridRow, gridCol);
      textField.RegisterValueChangedCallback(OnTextFieldChanged);
    }
    //void OnTextFieldChanged(ChangeEvent<string> evt) => grid_OnValueChanged();
    void OnTextFieldChanged(ChangeEvent<string> evt) { }
    public override float ui_width { get => UI_TextWidth(textString); set => style.width = textField.style.width = value; }

    public override Color BackgroundColor { get => base.BackgroundColor; set => textField.Q<TextElement>().style.backgroundColor = new StyleColor(_BackgroundColor = value); }

    public UI_Slider_base() : base()
    {
      RegisterCallback<MouseEnterEvent>(OnMouseEnter);
      RegisterCallback<MouseLeaveEvent>(OnMouseLeave);
      container = this.Q<VisualElement>("container");
      headerLabel = container.Q<Label>(nameof(headerLabel));
      headerLabel.RegisterCallback<ClickEvent>(OnClickEvent);
      textField = container.Q<TextField>();
#if UNITY_STANDALONE_WIN
      textField.isDelayed = true; //RegisterValueChangedCallback only called when user presses enter or gives away focus, with no Escape notification
#endif //UNITY_STANDALONE_WIN
      textField.RegisterValueChangedCallback(OnValueChanged);
      textField.RegisterCallback<FocusOutEvent>(OnFocusOut);
      textField.RegisterCallback<MouseCaptureEvent>(OnMouseCaptureEvent);
      textField.RegisterCallback<MouseCaptureOutEvent>(OnMouseCaptureOutEvent);

      //textField.RegisterCallback<ChangeEvent<string>>(OnChangeEvent);
      textField.RegisterCallback<KeyDownEvent>(OnKeyDownEvent, TrickleDown.TrickleDown);

      unitLabel = container.Q<Label>(nameof(unitLabel));
      unitLabel?.RegisterCallback<ClickEvent>(On_unitLabel_click);
      sliders = GetSliders();
      foreach (var slider in sliders)
      {
        slider.RegisterValueChangedCallback(OnValueChanged);
        slider.RegisterCallback<MouseCaptureEvent>(OnMouseCaptureEvent);
        slider.RegisterCallback<FocusInEvent>(OnSliderFocusIn);
        slider.RegisterCallback<FocusOutEvent>(OnSliderFocusOut);
        slider.RegisterCallback<KeyDownEvent>(OnSliderKeyDown);
        slider.RegisterCallback<KeyUpEvent>(OnSliderKeyUp);
      }
    }

    string validChars = "0123456789.+-,";
    private void OnKeyDownEvent(KeyDownEvent evt)
    {
      if (validChars.IndexOf(evt.character) == -1)
        evt.StopPropagation();
    }
    //private void OnKeyDownEvent(KeyDownEvent evt)
    //{
    //  if (char.IsLetter(evt.character)) // Check if the character is a letter
    //  {
    //    evt.StopPropagation(); // Stop the event from propagating further
    //    //evt.PreventDefault(); // Prevent the default action (typing the character)
    //  }
    //}

    ////public string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    //public string validChars = "0123456789.+-,";
    //bool ValidateInput(string input)
    //{
    //  if (string.IsNullOrEmpty(input)) 
    //    return true; // Or handle empty input as invalid, depending on your needs.
    //  foreach (char c in input)
    //    if (validChars.IndexOf(c) == -1)
    //      return false;
    //  return true;
    //}
    //private void OnChangeEvent(ChangeEvent<string> evt)
    //{
    //  string newText = evt.newValue;
    //  bool isValid = ValidateInput(newText);

    //  if (!isValid)
    //  {
    //    //validationMessage.text = "Invalid input. Please use only letters and numbers.";
    //    //validationMessage.style.color = Color.red;
    //  }
    //  else
    //  {
    //    //validationMessage.text = "Valid input.";
    //    //validationMessage.style.color = Color.green;
    //  }
    //}

    void OnClickEvent(ClickEvent evt) => ShowSliders = hasRange ? !ShowSliders : ShowSliders;
    public bool ShiftKey;
    public void OnSliderKeyDown(KeyDownEvent evt) { if (evt.shiftKey) ShiftKey = true; }
    public void OnSliderKeyUp(KeyUpEvent evt) { if (evt.shiftKey) ShiftKey = false; }
    protected bool hasFocus, mouseInside, mouseDown;
    protected bool ShowSliders { get => sliders.Length > 0 ? sliders[0].style.display == DisplayStyle.Flex : false; set { foreach (var slider in sliders) slider.DisplayIf(value); grid?.DrawGrid(); } }
    public override void OnMouseEnter(MouseEnterEvent evt) { base.OnMouseEnter(evt); mouseInside = true; }
    public override void OnMouseLeave(MouseLeaveEvent evt) { base.OnMouseLeave(evt); mouseInside = false; }
    public virtual void OnMouseCaptureEvent(MouseCaptureEvent evt) { mouseDown = hasFocus = true; if (hasRange) ShowSliders = true; }
    public virtual void OnMouseCaptureOutEvent(MouseCaptureOutEvent evt) { mouseDown = hasFocus = false; if (hasRange && !mouseInside) ShowSliders = false; }
    public void OnFocusOut(FocusOutEvent evt) { OnMouseCaptureOutEvent(null); }
    public void OnSliderFocusIn(FocusInEvent evt) { sliderHasFocus = true; }
    public void OnSliderFocusOut(FocusOutEvent evt) { if (hasRange && !mouseInside) ShowSliders = false; sliderHasFocus = mouseInUI = false; }

    public float siConvert => siUnit != siUnit.Null ? convert(siUnit) : 1;
    public override void OnUnitsChanged() { if (unitLabel != null) unitLabel.text = unit; ShowSliders = false; }
    void On_unitLabel_click(ClickEvent evt) { siUnits = !siUnits; gs.OnUnitsChanged(); }

    public new static void _cs_Write(GS gs, StrBldr tData, StrBldr lateUpdate, StrBldr lateUpdate_ValuesChanged,
      StrBldr showIfs, StrBldr onValueChanged, AttGS attGS, string typeStr, string name)
    {
      StackFields(tData, typeStr, name);
      string unitStr = attGS != null ? $".{GetUnitConversion(attGS)}" : "";
      lateUpdate.Add($"\n    if (UI_{name}.Changed || any({name} != UI_{name}{unitStr})) {name} = UI_{name}{unitStr};");
      UI_VisualElement._cs_Write(gs, tData, lateUpdate, lateUpdate_ValuesChanged, showIfs, onValueChanged, attGS, typeStr, name);
    }
    public override string label { get => base.label; set { base.label = value; if (headerLabel != null) headerLabel.text = value; } }
    public virtual bool hasRange => false;
    public override bool _isReadOnly { get => base._isReadOnly; set { base._isReadOnly = value; if (textField != null) textField.isReadOnly = value; } }
    protected bool changed;
    public void OnValueChanged(ChangeEvent<string> evt) { if (evt.currentTarget is TextField) changed = true; }
    public virtual void OnValueChanged(ChangeEvent<float> evt)
    {
      if (evt.currentTarget is Slider && textField != null)
      {
        float val = default;
        for (int i = 0; i < sliders.Length; i++) val = sliders[i].value;
        textField.value = val.ToString(format);
        property?.SetValue(gs, val);
        if (isGrid) grid.grid_OnValueChanged();
        gs?.OnValueChanged();
      }
    }
    public virtual void OnTextFieldChanged(TextField o) { }
    public virtual void Build(string title, string description, string val, string rangeMin, string rangeMax, bool isReadOnly, string treeGroup_parent)
    {
      base.Build(title, description, isReadOnly, treeGroup_parent); textField.value = val; textField.isReadOnly = isReadOnly;
    }
    public string format => siUnits ? siFormat : usFormat;
  }
}