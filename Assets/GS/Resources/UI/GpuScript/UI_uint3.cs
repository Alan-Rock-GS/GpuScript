using System;
using System.Reflection;
using UnityEngine.UIElements;
using static GpuScript.GS;

namespace GpuScript
{
  [UxmlElement]
  public partial class UI_uint3 : UI_Slider_base
  {
    public override bool Init(GS gs, params GS[] gss)
    {
			if (!base.Init(gs, gss) && !isGrid) return false;
			Build(label, description, siUnit, usUnit, Unit, siFormat, usFormat, isReadOnly, isGrid, isPow2Slider, isPow10, isPow2, Nearest, NearestDigit, treeGroup_parent?.name);
      SliderV = v = _val.To_uint3(); rangeMin = RangeMin.To_uint3(); rangeMax = RangeMax.To_uint3();
      if (siUnit != siUnit.Null) { rangeMin *= roundu(convert(siUnit)); rangeMax *= roundu(convert(siUnit)); }
      if (headerLabel != null) headerLabel.HideIf(label.IsEmpty() || isGrid);
      return true;
    }
    public static void UXML_UI_grid_member(UI_Element e, MemberInfo m, AttGS att, uint rowI, float width)
    {
      if (att == null) return;
      UXML(e, att, $"{className}_{m.Name}_{rowI + 1}", "", "");
      e.uxml.Add($" is-grid=\"true\" style=\"width: {width}px;\" />");
    }
    public uint3 Slider_Pow_Val(float3 v) => clamp(roundu(isPow2Slider ? (pow10(abs(v)) - 1) / 0.999f : v, GetNearest(v)), rangeMin, rangeMax);
    public float3 Slider_Log_Val(uint3 v) => isPow2Slider ? log10(clamp(roundu(v, GetNearest(v)), rangeMin, rangeMax) * 0.999f + 1) : (float3)v;
    public uint3 SliderV { get => Slider_Pow_Val(new float3(sliders[0].value, sliders[1].value, sliders[2].value)); set { var v = Slider_Log_Val(value); sliders[0].value = v.x; sliders[1].value = v.y; sliders[2].value = v.z; } }
    public override Slider[] GetSliders() => new Slider[] { this.Q<Slider>("slider_x"), this.Q<Slider>("slider_y"), this.Q<Slider>("slider_z") }; 
    public UI_uint3() : base() { }
    public override void OnMouseCaptureEvent(MouseCaptureEvent evt) { base.OnMouseCaptureEvent(evt); if (evt.currentTarget is TextField) { var o = evt.currentTarget as TextField; previousValue = o.value.To_uint3(); } }
    public override void OnMouseCaptureOutEvent(MouseCaptureOutEvent evt) { base.OnMouseCaptureOutEvent(evt); if (evt == null || evt.currentTarget is TextField) { var o = textField; if (changed && GS.any(previousValue != o.value.To_uint3())) { OnTextFieldChanged(o); previousValue = o.value.To_uint3(); changed = false; } } }
    public static Type Get_Base_Type() => typeof(uint3); 
    public static bool IsType(Type type) => type == typeof(uint3); 
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
    uint3 _v = default, _val = default;
    public uint3 v
    {
      get => textField != null ? _val = textField.value.To_uint3() : _val;
      set { _val = isPow10 ? GS.roundu(GS.pow10(GS.round(GS.log10(value)))) : isPow2 ? GS.roundu(GS.pow2(GS.round(GS.log2(value)))) : value; if (textField != null) textField.value = _val.ToString(format); if (hasRange) SliderV = _val; }
    }
    public override string textString => v.ToString(format);
    public override object v_obj { get => v; set => v = value.To_uint3(); }
    public override bool hasRange { get => GS.any(rangeMin < rangeMax); }
    uint3 _rangeMin; public uint3 rangeMin { get => _rangeMin; set { _rangeMin = value; if (sliders[0] != null) { var v = Slider_Log_Val(value); for (int i = 0; i < sliders.Length; i++) sliders[i].lowValue = v[i]; } } }
    uint3 _rangeMax; public uint3 rangeMax { get => _rangeMax; set { _rangeMax = value; if (sliders[0] != null) { var v = Slider_Log_Val(value); for (int i = 0; i < sliders.Length; i++) sliders[i].highValue = v[i]; } } }

    public uint3 previousValue;
    public override void OnValueChanged(ChangeEvent<float> evt)
    {
      if (evt.currentTarget is Slider && textField != null) { var val = SliderV; textField.value = val.ToString(); property?.SetValue(gs, val); gs?.OnValueChanged(); }
    }
    public override void OnTextFieldChanged(TextField o)
    {
			if (hasRange) SliderV = o.value.To_uint3(); SetPropertyValue(textField.value.To_uint3());
		}
		public override bool Changed { get => GS.any(v != _v); set => _v = value ? v - 1 : v; }
    public static implicit operator uint3(UI_uint3 f) => f.v; 
  }
}