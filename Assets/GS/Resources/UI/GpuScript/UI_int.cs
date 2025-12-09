using System;
using System.Reflection;
using UnityEngine.UIElements;
using static GpuScript.GS_cginc;
using static GpuScript.GS;

namespace GpuScript
{
	[UxmlElement]
	public partial class UI_int : UI_Slider_base
	{
		public override bool Init(GS gs, params GS[] gss)
		{
			if (!base.Init(gs, gss) && !isGrid) return false;
			Build(label, description, siUnit, usUnit, Unit, siFormat, usFormat, isReadOnly, isGrid, isPow2Slider, isPow10, isPow2, Nearest, NearestDigit, treeGroup_parent?.name);
			SliderV = v = _val.To_int(); rangeMin = RangeMin.To_int(); rangeMax = RangeMax.To_int();
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
		public int Slider_Pow_Val(float v) => clamp(roundi(isPow2Slider ? (pow10(abs(v)) - 1) / 0.999f : v, GetNearest(v)), rangeMin, rangeMax);
		public float Slider_Log_Val(int v) => isPow2Slider ? log10(abs(clamp(round(v, GetNearest(v)), rangeMin, rangeMax)) * 0.999f + 1) : v;
		public int SliderV { get => Slider_Pow_Val(sliders[0].value); set => sliders[0].value = Slider_Log_Val(value); }
		public override Slider[] GetSliders() => new Slider[] { this.Q<Slider>("slider_x") };
		public UI_int() : base() { }
		public override void OnMouseCaptureEvent(MouseCaptureEvent evt) { base.OnMouseCaptureEvent(evt); if (evt.currentTarget is TextField) { var o = evt.currentTarget as TextField; previousValue = o.value.To_int(); } }
		public override void OnMouseCaptureOutEvent(MouseCaptureOutEvent evt) { base.OnMouseCaptureOutEvent(evt); if (evt == null || evt.currentTarget is TextField) { var o = textField; if (changed && any(previousValue != o.value.To_int())) { OnTextFieldChanged(o); previousValue = o.value.To_int(); changed = false; } } }
		public static Type Get_Base_Type() => typeof(int);
		public static bool IsType(Type type) => type == typeof(int);
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
		int _v = default, _val = default;
		public int v
		{
			get => textField != null ? _val = textField.value.To_int() : _val;
			set
			{
				//_val = isPow10 ? roundi(pow10(round(log10(value)))) : isPow2 ? roundi(pow2(round(log2(value)))) : value;
				_val = get_pow_val(value);

				if (Nearest > 0) _val = roundi(_val, Nearest);
				Text(_val);
				if (hasRange) SliderV = _val;
			}
		}
		public void Text(float val) => textField.value = val.ToString(format);

		public override string textString => v.ToString(format);
		public override object v_obj { get => v; set => v = value.To_int(); }
		public override bool hasRange { get => rangeMin < rangeMax; }
		int _rangeMin, _rangeMax;
		public int rangeMin { get => _rangeMin; set { _rangeMin = value; if (sliders[0] != null) for (int i = 0; i < sliders.Length; i++) sliders[i].lowValue = Slider_Log_Val(value); } }
		public int rangeMax { get => _rangeMax; set { _rangeMax = value; if (sliders[0] != null) for (int i = 0; i < sliders.Length; i++) sliders[i].highValue = Slider_Log_Val(value); } }

		int previousValue;
		public int get_pow_val(int x) => isPow10 ? roundi(pow10(round(log10(x)))) : isPow2 ? roundi(pow2(round(log2(x)))) : x;
		public int get_val(int x)
		{
			x = get_pow_val(x);
			if (Nearest > 0) x = roundi(x, Nearest); if (NearestDigit) x = GetNearestDigit(x);
			return x;
		}
		public override void OnValueChanged(ChangeEvent<float> evt) { if (evt.currentTarget is Slider && textField != null) { var x = get_val(SliderV); if (isGrid || x != v) Text(x); } }
		public override void OnTextFieldChanged(TextField o)
		{
			var x = get_val(o.value.To_int());
			if (isGrid || x != v)
			{
				if (hasRange)
					SliderV = x;
				if (!GS.isGridVScroll && !GS.isGridBuilding)
					SetPropertyValue(x);
			}
		}


		public override bool Changed { get => v != _v; set => _v = value ? v - 1 : v; }
		public static implicit operator int(UI_int f) => f.v;
	}
}