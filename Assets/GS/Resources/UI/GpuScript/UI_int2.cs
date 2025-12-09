using System;
using System.Reflection;
using UnityEngine.UIElements;
using static GpuScript.GS_cginc;
using static GpuScript.GS;

namespace GpuScript
{
	[UxmlElement]
	public partial class UI_int2 : UI_Slider_base
	{
		public override bool Init(GS gs, params GS[] gss)
		{
			if (!base.Init(gs, gss) && !isGrid) return false;
			Build(label, description, siUnit, usUnit, Unit, siFormat, usFormat, isReadOnly, isGrid, isPow2Slider, isPow10, isPow2, Nearest, NearestDigit, treeGroup_parent?.name);
			SliderV = v = _val.To_int2(); rangeMin = RangeMin.To_int2(); rangeMax = RangeMax.To_int2();
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
		int2 _v = default, _val = default;
		public int2 v
		{
			get => textField != null ? _val = textField.value.To_int2() : _val;
			set
			{
				//_val = isPow10 ? roundi(pow10(round(log10((float2)value)))) : isPow2 ? roundi(pow2(round(log2((float2)value)))) : value;
				_val = get_pow_val(value);
				if (Nearest > 0) _val = roundi(_val, Nearest);
				if (textField != null) textField.value = _val.ToString(format); if (hasRange) SliderV = _val;
			}
		}
		public override string textString => v.ToString(format);
		public override object v_obj { get => v; set => v = value.To_int2(); }
		public override bool hasRange { get => any(rangeMin < rangeMax); }
		int2 _rangeMin; public int2 rangeMin { get => _rangeMin; set { _rangeMin = value; if (sliders[0] != null) { var v = Slider_Log_Val(value); for (int i = 0; i < sliders.Length; i++) sliders[i].lowValue = v[i]; } } }
		int2 _rangeMax; public int2 rangeMax { get => _rangeMax; set { _rangeMax = value; if (sliders[0] != null) { var v = Slider_Log_Val(value); for (int i = 0; i < sliders.Length; i++) sliders[i].highValue = v[i]; } } }

		public int2 previousValue;
		public void Text(int2 val)
		{
			if (siUnit != siUnit.Null && usUnit != usUnit.Null)
				textField.value = (siUnits ? convert(val / siConvert) : val * convert(GetUnitConversion(siUnit), usUnit)).ToString(format);
			else textField.value = val.ToString(format);
			textField.value = val.ToString(format);
		}

		//public float2 Slider_Log_Val(int2 v) => isPow2Slider ? log10(abs(clamp(roundi(v, GetNearest((float2)v)), rangeMin, rangeMax)) * 0.999f + 1) : (float2)v;
		public float Slider_Log_Val_x(int v) => isPow2Slider ? log10(abs(clamp(round(v, GetNearest(v)), rangeMin.x, rangeMax.x)) * 0.999f + 1) : v;
		public float Slider_Log_Val_y(int v) => isPow2Slider ? log10(abs(clamp(round(v, GetNearest(v)), rangeMin.y, rangeMax.y)) * 0.999f + 1) : v;
		public void sld_x(int v) => sliders[0].value = Slider_Log_Val_x(v);
		public void sld_y(int v) => sliders[1].value = Slider_Log_Val_y(v);


		public override bool Changed { get => any(v != _v); set => _v = value ? v - 1 : v; }
		public static implicit operator int2(UI_int2 f) => f.v;



		public override void OnValueChanged(ChangeEvent<float> evt)
		{
			if (evt.currentTarget is Slider && textField != null)
			{
				if (ShowSliders && Key(UnityEngine.KeyCode.F1))
				{
					int2 s = SliderV, dV = s - previousValue;
					if (s.x != previousValue.x) { if (s.y + dV.x <= rangeMax.y && s.y + dV.x >= rangeMin.y) sld_y(s.y + dV.x); else sld_x(previousValue.x); }
					else if (s.y != previousValue.y) { if (s.x + dV.y <= rangeMax.x && s.x + dV.y >= rangeMin.x) sld_x(s.x + dV.y); else sld_y(previousValue.y); }
				}
				int2 val = SliderV; previousValue = val; Text(val);
				if (!GS.isGridVScroll && !GS.isGridBuilding) SetPropertyValue(val);
			}
		}
		public override void OnTextFieldChanged(TextField o)
		{
			if (hasRange) SliderV = o.value.To_int2(); SetPropertyValue(textField.value.To_int2());
		}
		public int2 get_pow_val(int2 x) => isPow10 ? roundi(pow10(round(log10((float2)x)))) : isPow2 ? roundi(pow2(round(log2((float2)x)))) : x;
		public int2 get_val(int2 x)
		{
			x = get_pow_val(x);
			if (Nearest > 0) x = roundi(x, Nearest); if (NearestDigit) x = GetNearestDigit(x);
			return x;
		}
		//public override void OnValueChanged(ChangeEvent<float> evt)
		//{
		//	if (evt.currentTarget is Slider && textField != null)
		//	{
		//		if (ShowSliders && Key(UnityEngine.KeyCode.F1))
		//		{
		//			int2 s = get_val(SliderV), dV = s - previousValue;
		//			if (s.x != previousValue.x) { if (s.y + dV.x <= rangeMax.y && s.y + dV.x >= rangeMin.y) sld_y(s.y + dV.x); else sld_x(previousValue.x); }
		//			else if (s.y != previousValue.y) { if (s.x + dV.y <= rangeMax.x && s.x + dV.y >= rangeMin.x) sld_x(s.x + dV.y); else sld_y(previousValue.y); }
		//		}
		//		int2 val = get_val(SliderV); previousValue = val;
		//		if (any(val != v))
		//			Text(val);
		//	}
		//}
		//public override void OnTextFieldChanged(TextField o)
		//{
		//	var x = get_val(o.value.To_int()); if (x != v) { if (hasRange) SliderV = x; SetPropertyValue(x); }
		//	if (hasRange) SliderV = get_val(o.value.To_int2()); SetPropertyValue(textField.value.To_int2());
		//}

	}
}