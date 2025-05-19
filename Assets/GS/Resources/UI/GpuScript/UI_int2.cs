using System;
using System.Reflection;
using UnityEngine.UIElements;
using static GpuScript.GS;

namespace GpuScript
{
  public class UI_int2 : UI_Slider_base
  {
    public int2 Slider_Pow_Val(float2 v) => clamp(roundi(is_Pow2_Slider ? (pow10(abs(v)) - 1) / 0.999f : v, GetNearest(v)), range_Min, range_Max);
    public float2 Slider_Log_Val(int2 v) => is_Pow2_Slider ? log10(abs(clamp(roundi(v, GetNearest((float2)v)), range_Min, range_Max)) * 0.999f + 1) : (float2)v;
    public int2 SliderV { get => Slider_Pow_Val(new float2(sliders[0].value, sliders[1].value)); set { var v = Slider_Log_Val(value); sliders[0].value = v.x; sliders[1].value = v.y; } }
    public override Slider[] GetSliders() => new Slider[] { this.Q<Slider>("slider_x"), this.Q<Slider>("slider_y") }; 
    public UI_int2() : base() { }
    public override void OnMouseCaptureEvent(MouseCaptureEvent evt) { base.OnMouseCaptureEvent(evt); if (evt.currentTarget is TextField) { var o = evt.currentTarget as TextField; previousValue = o.value.To_int2(); } }
    public override void OnMouseCaptureOutEvent(MouseCaptureOutEvent evt) { base.OnMouseCaptureOutEvent(evt); if (evt == null || evt.currentTarget is TextField) { var o = textField; if (changed && any(previousValue != o.value.To_int2())) { OnTextFieldChanged(o); previousValue = o.value.To_int2(); changed = false; } } }
    public static Type Get_Base_Type() => typeof(int2); 
    public static bool IsType(Type type) => type == typeof(int2); 
    public override bool Init(GS gs, params GS[] gss) { if (!base.Init(gs, gss)) return false; v = textField.value.To_int2(); return true; }
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
    public static void UXML_UI_grid_member(UI_Element e, MemberInfo m, AttGS att, uint rowI, float width)
    {
      if (att == null) return;
      UXML(e, att, $"{className}_{m.Name}_{rowI + 1}", "", "");
      e.uxml.Add($" UI_isGrid=\"true\" style=\"width: {width}px;\" />");
    }
    int2 _v = default, val = default;
    public int2 v
    {
      get => textField != null ? val = textField.value.To_int2() : val;
      set
      {
        val = is_Pow10 ? roundi(pow10(round(log10((float2)value)))) : is_Pow2 ? roundi(pow2(round(log2((float2)value)))) : value;
        if (Nearest > 0) val = roundi(val, Nearest);
        if (textField != null) textField.value = val.ToString(format); if (hasRange) SliderV = val;
      }
    }
    public override string textString => v.ToString(format);
    public override object v_obj { get => v; set => v = value.To_int2(); }
    public override bool hasRange { get => any(range_Min < range_Max); }
    int2 _range_Min; public int2 range_Min { get => _range_Min; set { _range_Min = value; if (sliders[0] != null) { var v = Slider_Log_Val(value); for (int i = 0; i < sliders.Length; i++) sliders[i].lowValue = v[i]; } } }
    int2 _range_Max; public int2 range_Max { get => _range_Max; set { _range_Max = value; if (sliders[0] != null) { var v = Slider_Log_Val(value); for (int i = 0; i < sliders.Length; i++) sliders[i].highValue = v[i]; } } }

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
            if (sliders[0].value + dV < range_Max.y) sliders[1].value = sliders[0].value + dV;
            else { sliders[1].value = range_Max.y; sliders[0].value = sliders[1].value - dV; }
          }
          else if (sliderV.y != previousValue.y)
          {
            if (sliders[1].value - dV > range_Min.x) sliders[0].value = sliders[1].value - dV;
            else { sliders[0].value = range_Min.x; sliders[1].value = sliders[0].value + dV; }
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

    public void Build(string title, string description, string val, string rangeMin, string rangeMax, string format, bool isReadOnly, bool isGrid, bool isPow2Slider,
      bool isPow10, bool isPow2, float nearest, bool nearestDigit, string treeGroup_parent)
    {
      base.Build(title, description, val, format, isReadOnly, isGrid, isPow2Slider, isPow10, isPow2, nearest, nearestDigit, treeGroup_parent);
      range_Min = rangeMin.To_int2(); range_Max = rangeMax.To_int2(); SliderV = val.To_int2();
      if (headerLabel != null) headerLabel.HideIf(label.IsEmpty() || isGrid);
    }

    public new class UxmlFactory : UxmlFactory<UI_int2, UxmlTraits> { }
    public new class UxmlTraits : UI_VisualElement.UxmlTraits
    {
      UxmlStringAttributeDescription m_int2_value = new UxmlStringAttributeDescription { name = "UI_int2_value" };
      UxmlStringAttributeDescription m_int2_min = new UxmlStringAttributeDescription { name = "UI_int2_min" };
      UxmlStringAttributeDescription m_int2_max = new UxmlStringAttributeDescription { name = "UI_int2_max" };

      public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
      {
        base.Init(ve, bag, cc);
        ((UI_int2)ve).Build(m_Label.GetValueFromBag(bag, cc), m_Description.GetValueFromBag(bag, cc),
            m_int2_value.GetValueFromBag(bag, cc), m_int2_min.GetValueFromBag(bag, cc), m_int2_max.GetValueFromBag(bag, cc),
            m_Format.GetValueFromBag(bag, cc), m_isReadOnly.GetValueFromBag(bag, cc), m_isGrid.GetValueFromBag(bag, cc), m_isPow2Slider.GetValueFromBag(bag, cc),
            m_isPow10.GetValueFromBag(bag, cc), m_isPow2.GetValueFromBag(bag, cc), m_Nearest.GetValueFromBag(bag, cc), m_NearestDigit.GetValueFromBag(bag, cc),
            m_TreeGroup_Parent.GetValueFromBag(bag, cc));
      }
    }
  }
}