using System;
using System.Reflection;
using UnityEngine.UIElements;
using static GpuScript.GS;

namespace GpuScript
{
  [UxmlElement]
  public partial class UI_int2 : UI_Slider_base
  {
    public override bool Init(GS gs, params GS[] gss)
    {
      if (!base.Init(gs, gss)) return false;
      Build(label, description, siUnit, usUnit, Unit, siFormat, usFormat, isReadOnly, isGrid, isPow2Slider, isPow10, isPow2, Nearest, NearestDigit, treeGroup_parent?.name);
      SliderV = v = val.To_int2(); rangeMin = RangeMin.To_int2(); rangeMax = RangeMax.To_int2();
      if (siUnit != siUnit.Null) { rangeMin *= roundi(convert(siUnit)); rangeMax *= roundi(convert(siUnit)); }
      if (headerLabel != null) headerLabel.HideIf(label.IsEmpty() || isGrid);
      return true;
    }
    public static void UXML_UI_grid_member(UI_Element e, MemberInfo m, AttGS att, uint rowI, float width)
    {
      if (att == null) return;
      UXML(e, att, $"{className}_{m.Name}_{rowI + 1}", "", "");
      e.uxml.Add($" is-grid=\"true\" style=\"width: {width}px;\" />");
    }
    public int2 Slider_Pow_Val(float2 v) => clamp(roundi(isPow2Slider ? (pow10(abs(v)) - 1) / 0.999f : v, GetNearest(v)), rangeMin, rangeMax);
    public float2 Slider_Log_Val(int2 v) => isPow2Slider ? log10(abs(clamp(roundi(v, GetNearest((float2)v)), rangeMin, rangeMax)) * 0.999f + 1) : (float2)v;
    public int2 SliderV { get => Slider_Pow_Val(new float2(sliders[0].value, sliders[1].value)); set { var v = Slider_Log_Val(value); sliders[0].value = v.x; sliders[1].value = v.y; } }
    public override Slider[] GetSliders() => new Slider[] { this.Q<Slider>("slider_x"), this.Q<Slider>("slider_y") }; 
    public UI_int2() : base() { }
    public override void OnMouseCaptureEvent(MouseCaptureEvent evt) { base.OnMouseCaptureEvent(evt); if (evt.currentTarget is TextField) { var o = evt.currentTarget as TextField; previousValue = o.value.To_int2(); } }
    public override void OnMouseCaptureOutEvent(MouseCaptureOutEvent evt) { base.OnMouseCaptureOutEvent(evt); if (evt == null || evt.currentTarget is TextField) { var o = textField; if (changed && any(previousValue != o.value.To_int2())) { OnTextFieldChanged(o); previousValue = o.value.To_int2(); changed = false; } } }
    public static Type Get_Base_Type() => typeof(int2); 
    public static bool IsType(Type type) => type == typeof(int2); 
    public new static void _cs_Write(GS gs, StrBldr tData, StrBldr lateUpdate, StrBldr lateUpdate_ValuesChanged,
     StrBldr showIfs, StrBldr onValueChanged, AttGS attGS, string typeStr, string name)
    {
      StackFields(tData, typeStr, name);
      lateUpdate.Add($"\n    if (UI_{name}.Changed || any({name} != UI_{name}.v)) {name} = UI_{name}.v;");
      UI_VisualElement._cs_Write(gs, tData, lateUpdate, lateUpdate_ValuesChanged, showIfs, onValueChanged, attGS, typeStr, name);
    }
    public static string className => MethodBase.GetCurrentMethod().DeclaringType.ToString().After("GpuScript.");
    public static void UXML_UI_Element(UI_Element e) { UXML(e, e.attGS, e.memberInfo.Name, e.attGS.Name, className); e.uxml.Add($" />"); }
    public static new void UXML(UI_Element e, AttGS att, string name, string label, string typeName) { UI_Slider_base.UXML(e, att, name, label, className); }
    int2 _v = default, val = default;
    public int2 v
    {
      get => textField != null ? val = textField.value.To_int2() : val;
      set
      {
        val = isPow10 ? roundi(pow10(round(log10((float2)value)))) : isPow2 ? roundi(pow2(round(log2((float2)value)))) : value;
        if (Nearest > 0) val = roundi(val, Nearest);
        if (textField != null) textField.value = val.ToString(format); if (hasRange) SliderV = val;
      }
    }
    public override string textString => v.ToString(format);
    public override object v_obj { get => v; set => v = value.To_int2(); }
    public override bool hasRange { get => any(rangeMin < rangeMax); }
    int2 _rangeMin; public int2 rangeMin { get => _rangeMin; set { _rangeMin = value; if (sliders[0] != null) { var v = Slider_Log_Val(value); for (int i = 0; i < sliders.Length; i++) sliders[i].lowValue = v[i]; } } }
    int2 _rangeMax; public int2 rangeMax { get => _rangeMax; set { _rangeMax = value; if (sliders[0] != null) { var v = Slider_Log_Val(value); for (int i = 0; i < sliders.Length; i++) sliders[i].highValue = v[i]; } } }

    public int2 previousValue;
    int dV = 0;
    bool F1_Pressed = false;
    public override void OnValueChanged(ChangeEvent<float> evt)
    {
      if (evt.currentTarget is Slider && textField != null)
      {
        if (Key(UnityEngine.KeyCode.F1))
        {
          var sliderV = SliderV;
          if (!F1_Pressed) { F1_Pressed = true; dV = sliderV.y - sliderV.x; }

          if (sliderV.x != previousValue.x)
          {
            if (sliders[0].value + dV < rangeMax.y) sliders[1].value = sliders[0].value + dV;
            else { sliders[1].value = rangeMax.y; sliders[0].value = sliders[1].value - dV; }
          }
          else if (sliderV.y != previousValue.y)
          {
            if (sliders[1].value - dV > rangeMin.x) sliders[0].value = sliders[1].value - dV;
            else { sliders[0].value = rangeMin.x; sliders[1].value = sliders[0].value + dV; }
          }
        }
        else F1_Pressed = false;
        var val = SliderV;
        previousValue = val;
        textField.value = val.ToString(format);
        //SetPropertyValue(val);
        property?.SetValue(gs, val);
        gs?.OnValueChanged();
      }
    }

    public override void OnTextFieldChanged(TextField o)
    {
			if (hasRange) SliderV = o.value.To_int2(); SetPropertyValue(textField.value.To_int2());
		}
		public override bool Changed { get => any(v != _v); set => _v = value ? v - 1 : v; }
    public static implicit operator int2(UI_int2 f) => f.v; 

  }
}