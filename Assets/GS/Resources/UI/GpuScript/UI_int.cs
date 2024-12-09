using System;
using System.Reflection;
using UnityEngine.UIElements;
using static GpuScript.GS;

namespace GpuScript
{
  public class UI_int : UI_Slider_base
  {
    public int Slider_Pow_Val(float v) => clamp(roundi(is_Pow2_Slider ? (pow10(abs(v)) - 1) / 0.999f : v, Nearest), range_Min, range_Max);
    public float Slider_Log_Val(int v) => is_Pow2_Slider ? log10(abs(clamp(round(v, Nearest), range_Min, range_Max)) * 0.999f + 1) : v;
    public int SliderV { get => Slider_Pow_Val(sliders[0].value); set => sliders[0].value = Slider_Log_Val(value); }
    public override Slider[] GetSliders() => new Slider[] { this.Q<Slider>("slider_x") };
    public UI_int() : base() { }
    public override void OnMouseCaptureEvent(MouseCaptureEvent evt) { base.OnMouseCaptureEvent(evt); if (evt.currentTarget is TextField) { var o = evt.currentTarget as TextField; previousValue = o.value.To_int(); } }
    public override void OnMouseCaptureOutEvent(MouseCaptureOutEvent evt) { base.OnMouseCaptureOutEvent(evt); if (evt == null || evt.currentTarget is TextField) { var o = textField; if (changed && any(previousValue != o.value.To_int())) { OnTextFieldChanged(o); previousValue = o.value.To_int(); changed = false; } } }
    public static Type Get_Base_Type() => typeof(int);
    public static bool IsType(Type type) => type == typeof(int);
    public override bool Init(GS gs, params GS[] gss) { if (!base.Init(gs, gss)) return false; v = textField.value.To_int(); return true; }
    public new static void _cs_Write(GS gs, StrBldr tData, StrBldr lateUpdate, StrBldr lateUpdate_ValuesChanged,
      StrBldr showIfs, StrBldr onValueChanged, AttGS attGS, string typeStr, string name)
    {
      StackFields(tData, typeStr, name);
      lateUpdate.Add($"\n    if (UI_{name}.Changed || {name} != UI_{name}.v) {name} = UI_{name}.v;");
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
    int _v = default, val = default;
    public int v
    {
      get => textField != null ? val = textField.value.To_int() : val;
      set
      {
        val = is_Pow10 ? roundi(pow10(round(log10(value)))) : is_Pow2 ? roundi(pow2(round(log2(value)))) : value;
        if (Nearest > 0) val = roundi(val, Nearest);
        if (textField != null) textField.value = val.ToString(format); if (hasRange) SliderV = val;
      }
    }
    public override string textString => v.ToString(format);
    public override object v_obj { get => v; set => v = value.To_int(); }
    public override bool hasRange { get => range_Min < range_Max; }
    int _range_Min, _range_Max;
    public int range_Min { get => _range_Min; set { _range_Min = value; if (sliders[0] != null) for (int i = 0; i < sliders.Length; i++) sliders[i].lowValue = Slider_Log_Val(value); } }
    public int range_Max { get => _range_Max; set { _range_Max = value; if (sliders[0] != null) for (int i = 0; i < sliders.Length; i++) sliders[i].highValue = Slider_Log_Val(value); } }

    int previousValue;
    public override void OnValueChanged(ChangeEvent<float> evt)
    {
      if (evt.currentTarget is Slider && textField != null) { var val = SliderV; textField.value = val.ToString(format); property?.SetValue(gs, val); gs?.OnValueChanged(); }
    }
    public override void OnTextFieldChanged(TextField o)
    {
      if (hasRange) SliderV = o.value.To_int();
      else
      {
        if (property == null) print("property == null");
        if (textField == null) print("textField == null");
        if (gs == null) print("gs == null");
        property.SetValue(gs, textField.value.To_int());
        gs.OnValueChanged();
      }
    }
    public override bool Changed { get => v != _v; set => _v = value ? v - 1 : v; }
    public static implicit operator int(UI_int f) => f.v;
    public void Build(string title, string description, string val, string rangeMin, string rangeMax, string format, bool isReadOnly, bool isGrid, bool isPow2Slider,
      bool isPow10, bool isPow2, float nearest, string treeGroup_parent)
    {
      base.Build(title, description, val, format, isReadOnly, isGrid, isPow2Slider, isPow10, isPow2, nearest, treeGroup_parent);
      range_Min = rangeMin.To_int(); range_Max = rangeMax.To_int(); SliderV = val.To_int();
      if (headerLabel != null) headerLabel.HideIf(label.IsEmpty() || isGrid);
    }

    public new class UxmlFactory : UxmlFactory<UI_int, UxmlTraits> { }
    public new class UxmlTraits : UI_VisualElement.UxmlTraits
    {
      UxmlStringAttributeDescription m_int_value = new UxmlStringAttributeDescription { name = "UI_int_value" };
      UxmlStringAttributeDescription m_int_min = new UxmlStringAttributeDescription { name = "UI_int_min" };
      UxmlStringAttributeDescription m_int_max = new UxmlStringAttributeDescription { name = "UI_int_max" };

      public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
      {
        base.Init(ve, bag, cc);
        ((UI_int)ve).Build(m_Label.GetValueFromBag(bag, cc), m_Description.GetValueFromBag(bag, cc),
            m_int_value.GetValueFromBag(bag, cc), m_int_min.GetValueFromBag(bag, cc), m_int_max.GetValueFromBag(bag, cc),
            m_Format.GetValueFromBag(bag, cc), m_isReadOnly.GetValueFromBag(bag, cc), m_isGrid.GetValueFromBag(bag, cc), m_isPow2Slider.GetValueFromBag(bag, cc),
            m_isPow10.GetValueFromBag(bag, cc), m_isPow2.GetValueFromBag(bag, cc), m_Nearest.GetValueFromBag(bag, cc),
            m_TreeGroup_Parent.GetValueFromBag(bag, cc));
      }
    }
  }
}