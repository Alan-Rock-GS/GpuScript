using System;
using System.Reflection;
using UnityEngine.UIElements;
using static GpuScript.GS;

namespace GpuScript
{
  public class UI_uint3 : UI_Slider_base
  {
    public uint3 Slider_Pow_Val(float3 v) => clamp(roundu(is_Pow2_Slider ? (pow10(abs(v)) - 1) / 0.999f : v, Nearest), range_Min, range_Max);
    public float3 Slider_Log_Val(uint3 v) => is_Pow2_Slider ? log10(clamp(roundu(v, Nearest), range_Min, range_Max) * 0.999f + 1) : (float3)v;
    public uint3 SliderV { get => Slider_Pow_Val(new float3(sliders[0].value, sliders[1].value, sliders[2].value)); set { var v = Slider_Log_Val(value); sliders[0].value = v.x; sliders[1].value = v.y; sliders[2].value = v.z; } }
    public override Slider[] GetSliders() => new Slider[] { this.Q<Slider>("slider_x"), this.Q<Slider>("slider_y"), this.Q<Slider>("slider_z") }; 
    public UI_uint3() : base() { }
    public override void OnMouseCaptureEvent(MouseCaptureEvent evt) { base.OnMouseCaptureEvent(evt); if (evt.currentTarget is TextField) { var o = evt.currentTarget as TextField; previousValue = o.value.To_uint3(); } }
    public override void OnMouseCaptureOutEvent(MouseCaptureOutEvent evt) { base.OnMouseCaptureOutEvent(evt); if (evt == null || evt.currentTarget is TextField) { var o = textField; if (changed && GS.any(previousValue != o.value.To_uint3())) { OnTextFieldChanged(o); previousValue = o.value.To_uint3(); changed = false; } } }
    public static Type Get_Base_Type() => typeof(uint3); 
    public static bool IsType(Type type) => type == typeof(uint3); 
    public override bool Init(GS gs, params GS[] gss) { if (!base.Init(gs, gss)) return false; v = textField.value.To_uint3(); return true; }
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
    uint3 _v = default, val = default;
    public uint3 v
    {
      get => textField != null ? val = textField.value.To_uint3() : val;
      set { val = is_Pow10 ? GS.roundu(GS.pow10(GS.round(GS.log10(value)))) : is_Pow2 ? GS.roundu(GS.pow2(GS.round(GS.log2(value)))) : value; if (textField != null) textField.value = val.ToString(format); if (hasRange) SliderV = val; }
    }
    public override string textString => v.ToString(format);
    public override object v_obj { get => v; set => v = value.To_uint3(); }
    public override bool hasRange { get => GS.any(range_Min < range_Max); }
    uint3 _range_Min; public uint3 range_Min { get => _range_Min; set { _range_Min = value; if (sliders[0] != null) { var v = Slider_Log_Val(value); for (int i = 0; i < sliders.Length; i++) sliders[i].lowValue = v[i]; } } }
    uint3 _range_Max; public uint3 range_Max { get => _range_Max; set { _range_Max = value; if (sliders[0] != null) { var v = Slider_Log_Val(value); for (int i = 0; i < sliders.Length; i++) sliders[i].highValue = v[i]; } } }

    public uint3 previousValue;
    public override void OnValueChanged(ChangeEvent<float> evt)
    {
      if (evt.currentTarget is Slider && textField != null) { var val = SliderV; textField.value = val.ToString(); property?.SetValue(gs, val); gs?.OnValueChanged(); }
    }
    public override void OnTextFieldChanged(TextField o)
    {
      //if (hasRange) SliderV = o.value.To_uint3(); else { property.SetValue(gs, textField.value.To_uint3()); gs.OnValueChanged(); }
			if (hasRange) SliderV = o.value.To_uint3(); else SetPropertyValue(textField.value.To_uint3());
		}
		public override bool Changed { get => GS.any(v != _v); set => _v = value ? v - 1 : v; }
    public static implicit operator uint3(UI_uint3 f) => f.v; 
    public void Build(string title, string description, string val, string rangeMin, string rangeMax, string format, bool isReadOnly, bool isGrid, bool isPow2Slider, 
      bool isPow10, bool isPow2, float nearest, string treeGroup_parent)
    {
      base.Build(title, description, val, format, isReadOnly, isGrid, isPow2Slider, isPow10, isPow2, nearest, treeGroup_parent);
      range_Min = rangeMin.To_uint3(); range_Max = rangeMax.To_uint3(); SliderV = val.To_uint3();
      if (headerLabel != null) headerLabel.HideIf(label.IsEmpty() || isGrid);
    }

    public new class UxmlFactory : UxmlFactory<UI_uint3, UxmlTraits> { }
    public new class UxmlTraits : UI_VisualElement.UxmlTraits
    {
      UxmlStringAttributeDescription m_uint3_value = new UxmlStringAttributeDescription { name = "UI_uint3_value" };
      UxmlStringAttributeDescription m_uint3_min = new UxmlStringAttributeDescription { name = "UI_uint3_min" };
      UxmlStringAttributeDescription m_uint3_max = new UxmlStringAttributeDescription { name = "UI_uint3_max" };

      public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
      {
        base.Init(ve, bag, cc);
        ((UI_uint3)ve).Build(m_Label.GetValueFromBag(bag, cc), m_Description.GetValueFromBag(bag, cc),
            m_uint3_value.GetValueFromBag(bag, cc), m_uint3_min.GetValueFromBag(bag, cc), m_uint3_max.GetValueFromBag(bag, cc),
            m_Format.GetValueFromBag(bag, cc), m_isReadOnly.GetValueFromBag(bag, cc), m_isGrid.GetValueFromBag(bag, cc), m_isPow2Slider.GetValueFromBag(bag, cc),
            m_isPow10.GetValueFromBag(bag, cc), m_isPow2.GetValueFromBag(bag, cc), m_Nearest.GetValueFromBag(bag, cc),
            m_TreeGroup_Parent.GetValueFromBag(bag, cc));
      }
    }
  }
}