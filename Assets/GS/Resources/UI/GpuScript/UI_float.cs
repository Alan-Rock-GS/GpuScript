using System;
using System.Reflection;
using UnityEngine.UIElements;
using static GpuScript.GS;

namespace GpuScript
{
	public class UI_float : UI_Slider_base
	{
		public float Slider_Pow_Val(float v) => clamp(round(is_Pow2_Slider ? (pow10(abs(v)) - 1) / 0.999f : v, Nearest), range_Min, range_Max);
		public float Slider_Log_Val(float v) => is_Pow2_Slider ? log10(abs(clamp(round(v, Nearest), range_Min, range_Max)) * 0.999f + 1) : v;
		//public float Slider_Pow_Val(float v) => clamp(is_Pow2_Slider ? (pow10(abs(v)) - 1) / 0.999f : v, range_Min, range_Max);
		//public float Slider_Log_Val(float v) => is_Pow2_Slider ? log10(abs(v) * 0.999f + 1) : v;
		public float SliderV { get => sliders[0] == null ? default : Slider_Pow_Val(sliders[0].value); set { if (sliders[0] != null) { sliders[0].value = Slider_Log_Val(value); } } }
		public override Slider[] GetSliders() => new Slider[] { this.Q<Slider>("slider_x") };
		public UI_float() : base() { }
		public override void OnMouseCaptureEvent(MouseCaptureEvent evt) { base.OnMouseCaptureEvent(evt); if (evt.currentTarget is TextField) { var o = evt.currentTarget as TextField; previousValue = o.value.To_float(); } }
		public override void OnMouseCaptureOutEvent(MouseCaptureOutEvent evt) { base.OnMouseCaptureOutEvent(evt); if (evt == null || evt.currentTarget is TextField) { var o = textField; if (changed && any(previousValue != o.value.To_float())) { OnTextFieldChanged(o); previousValue = o.value.To_float(); changed = false; } } }
		public static Type Get_Base_Type() => typeof(float);
		public static bool IsType(Type type) => type == typeof(float);
		public override bool Init(GS gs, params GS[] gss) { if (!base.Init(gs, gss)) return false; v = textField.value.To_float(); return true; }
		public new static void _cs_Write(GS gs, StrBldr tData, StrBldr lateUpdate, StrBldr lateUpdate_ValuesChanged, StrBldr showIfs, StrBldr onValueChanged, AttGS attGS, string typeStr, string name)
		{
			StackFields(tData, typeStr, name);
			string si = UI_VisualElement.GetUnitConversion(attGS);
			if (si == "Null") si = "_si";
			lateUpdate.Add($"\n    if (UI_{name}.Changed || {name} != UI_{name}.{si}) {name} = UI_{name}.{si};");
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
		public float _v0 = default;
		public float _si; public float si { get => _si; set => v = convert(_si = value); }
		public float v
		{
			get => textField != null ? _si = siUnits ? textField.value.To_float() : iconvert(textField.value.To_float()) : _si;
			set
			{
				if (any(isnan(value)) || any(isinf(value))) return;
				var val = is_Pow10 ? roundu(pow10(round(log10(value)))) : is_Pow2 ? roundu(pow2(round(log2(value)))) : value;
				if (Nearest > 0) val = round(val, Nearest);
				if (textField != null) textField.value = val.ToString(format);
				_si = hasRange ? iconvert(SliderV = val) : iconvert(val);
			}
		}

		public override string textString => v.ToString(format);
		public override object v_obj { get => v; set => v = value.To_float(); }
		//public override void OnUnitsChanged()
		//{
		//	base.OnUnitsChanged();
		//	if (siUnit != siUnit.Null && usUnit != usUnit.Null && hasRange)
		//		if (siUnits) { siUnits = false; range_Min = iconvert(range_Min); range_Max = iconvert(range_Max); siUnits = true; v = convert(si); }
		//		else { range_Min = convert(range_Min); range_Max = convert(range_Max); v = convert(si); }
		//}

		public override void OnUnitsChanged()
		{
			base.OnUnitsChanged();
			if (siUnit != siUnit.Null && usUnit != usUnit.Null)
			{
				if (hasRange)
				{
					if (siUnits) { siUnits = false; range_Min = iconvert(range_Min); range_Max = iconvert(range_Max); siUnits = true; }
					else { range_Min = convert(range_Min); range_Max = convert(range_Max); }
				}
				v = convert(si);
			}
		}


		public override bool hasRange { get => range_Min < range_Max; }
		float _range_Min; public float range_Min { get => _range_Min; set { _range_Min = value; if (sliders[0] != null) for (int i = 0; i < sliders.Length; i++) sliders[i].lowValue = Slider_Log_Val(value); } }
		float _range_Max; public float range_Max { get => _range_Max; set { _range_Max = value; if (sliders[0] != null) for (int i = 0; i < sliders.Length; i++) sliders[i].highValue = Slider_Log_Val(value); } }
		public float previousValue;
		public override void OnValueChanged(ChangeEvent<float> evt) { if (evt.currentTarget is Slider && textField != null) { float val = SliderV; textField.value = val.ToString(format); SetPropertyValue(val); } }
		public override void OnTextFieldChanged(TextField o) { float val = o.value.To_float(); SliderV = val; SetPropertyValue(val); }

		public void Build(string title, string description, string val, string rangeMin, string rangeMax, string _siUnit, string _usUnit, string _Unit,
			string siFormat, string usFormat, bool isReadOnly, bool isGrid, bool isPow2Slider, bool isPow10, bool isPow2, float nearest, string treeGroup_parent)
		{
			base.Build(title, description, _siUnit, _usUnit, _Unit, siFormat, usFormat, isReadOnly, isGrid, isPow2Slider, isPow10, isPow2, nearest, treeGroup_parent);
			range_Min = rangeMin.To_float(); range_Max = rangeMax.To_float(); SliderV = val.To_float();
		}

		public new class UxmlFactory : UxmlFactory<UI_float, UxmlTraits> { }
		public new class UxmlTraits : UI_VisualElement.UxmlTraits
		{
			UxmlStringAttributeDescription m_float_value = new UxmlStringAttributeDescription { name = "UI_float_value" };
			UxmlStringAttributeDescription m_float_min = new UxmlStringAttributeDescription { name = "UI_float_min" };
			UxmlStringAttributeDescription m_float_max = new UxmlStringAttributeDescription { name = "UI_float_max" };
			UxmlStringAttributeDescription m_float_siFormat = new UxmlStringAttributeDescription { name = "UI_float_siFormat", defaultValue = "0.0000" };
			UxmlStringAttributeDescription m_float_usFormat = new UxmlStringAttributeDescription { name = "UI_float_usFormat", defaultValue = "0.0000" };
			UxmlStringAttributeDescription m_float_siUnit = new UxmlStringAttributeDescription { name = "UI_float_siUnit", defaultValue = "" };
			UxmlStringAttributeDescription m_float_usUnit = new UxmlStringAttributeDescription { name = "UI_float_usUnit", defaultValue = "" };
			UxmlStringAttributeDescription m_float_Unit = new UxmlStringAttributeDescription { name = "UI_float_Unit", defaultValue = "" };

			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				((UI_float)ve).Build(m_Label.GetValueFromBag(bag, cc), m_Description.GetValueFromBag(bag, cc),
					m_float_value.GetValueFromBag(bag, cc), m_float_min.GetValueFromBag(bag, cc), m_float_max.GetValueFromBag(bag, cc),
					m_float_siUnit.GetValueFromBag(bag, cc), m_float_usUnit.GetValueFromBag(bag, cc), m_float_Unit.GetValueFromBag(bag, cc),
					m_float_siFormat.GetValueFromBag(bag, cc), m_float_usFormat.GetValueFromBag(bag, cc),
					m_isReadOnly.GetValueFromBag(bag, cc), m_isGrid.GetValueFromBag(bag, cc), m_isPow2Slider.GetValueFromBag(bag, cc),
					m_isPow10.GetValueFromBag(bag, cc), m_isPow2.GetValueFromBag(bag, cc), m_Nearest.GetValueFromBag(bag, cc),
					m_TreeGroup_Parent.GetValueFromBag(bag, cc));
			}
		}

		//public float siRange, usRange;
		public float2 range => float2(range_Min, range_Max);
		public override bool Changed { get => any(v != _v0); set => _v0 = value ? v - 1 : v; }

		public static implicit operator float(UI_float f) => f.si;

		public float ns { get => si * convert(Unit, Unit.ns); set => si = value / convert(Unit, Unit.ns); }
		public float us { get => si * convert(Unit, Unit.us); set => si = value / convert(Unit, Unit.us); }
		public float ms { get => si * convert(Unit, Unit.ms); set => si = value / convert(Unit, Unit.ms); }
		public float s { get => si * convert(Unit, Unit.s); set => si = value / convert(Unit, Unit.s); }
		public float min { get => si * convert(Unit, Unit.min); set => si = value / convert(Unit, Unit.min); }
		public float hr { get => si * convert(Unit, Unit.hr); set => si = value / convert(Unit, Unit.hr); }
		public float day { get => si * convert(Unit, Unit.day); set => si = value / convert(Unit, Unit.day); }
		public float week { get => si * convert(Unit, Unit.week); set => si = value / convert(Unit, Unit.week); }
		public float month { get => si * convert(Unit, Unit.month); set => si = value / convert(Unit, Unit.month); }
		public float year { get => si * convert(Unit, Unit.year); set => si = value / convert(Unit, Unit.year); }

		public float deg { get => si * convert(Unit, Unit.deg); set => si = value / convert(Unit, Unit.deg); }
		public float rad { get => si * convert(Unit, Unit.rad); set => si = value / convert(Unit, Unit.rad); }

		public float bit { get => si * convert(Unit, Unit.bit); set => si = value / convert(Unit, Unit.bit); }
		public float Byte { get => si * convert(Unit, Unit.Byte); set => si = value / convert(Unit, Unit.Byte); }
		public float KB { get => si * convert(Unit, Unit.KB); set => si = value / convert(Unit, Unit.KB); }
		public float MB { get => si * convert(Unit, Unit.MB); set => si = value / convert(Unit, Unit.MB); }
		public float GB { get => si * convert(Unit, Unit.GB); set => si = value / convert(Unit, Unit.GB); }
		public float TB { get => si * convert(Unit, Unit.TB); set => si = value / convert(Unit, Unit.TB); }
		public float PB { get => si * convert(Unit, Unit.PB); set => si = value / convert(Unit, Unit.PB); }

		public float bps { get => si * convert(Unit, Unit.bps); set => si = value / convert(Unit, Unit.bps); }
		public float Kbps { get => si * convert(Unit, Unit.Kbps); set => si = value / convert(Unit, Unit.Kbps); }
		public float Mbps { get => si * convert(Unit, Unit.Mbps); set => si = value / convert(Unit, Unit.Mbps); }
		public float Gbps { get => si * convert(Unit, Unit.Gbps); set => si = value / convert(Unit, Unit.Gbps); }
		public float Tbps { get => si * convert(Unit, Unit.Tbps); set => si = value / convert(Unit, Unit.Tbps); }
		public float Pbps { get => si * convert(Unit, Unit.Pbps); set => si = value / convert(Unit, Unit.Pbps); }
		public float Bps { get => si * convert(Unit, Unit.Bps); set => si = value / convert(Unit, Unit.Bps); }
		public float KBps { get => si * convert(Unit, Unit.KBps); set => si = value / convert(Unit, Unit.KBps); }
		public float MBps { get => si * convert(Unit, Unit.MBps); set => si = value / convert(Unit, Unit.MBps); }
		public float GBps { get => si * convert(Unit, Unit.GBps); set => si = value / convert(Unit, Unit.GBps); }
		public float TBps { get => si * convert(Unit, Unit.TBps); set => si = value / convert(Unit, Unit.TBps); }
		public float PBps { get => si * convert(Unit, Unit.PBps); set => si = value / convert(Unit, Unit.PBps); }

		public float FLOPS { get => si * convert(Unit, Unit.FLOPS); set => si = value / convert(Unit, Unit.FLOPS); }
		public float kFLOPS { get => si * convert(Unit, Unit.kFLOPS); set => si = value / convert(Unit, Unit.kFLOPS); }
		public float MFLOPS { get => si * convert(Unit, Unit.MFLOPS); set => si = value / convert(Unit, Unit.MFLOPS); }
		public float GFLOPS { get => si * convert(Unit, Unit.GFLOPS); set => si = value / convert(Unit, Unit.GFLOPS); }
		public float TFLOPS { get => si * convert(Unit, Unit.TFLOPS); set => si = value / convert(Unit, Unit.TFLOPS); }
		public float PFLOPS { get => si * convert(Unit, Unit.PFLOPS); set => si = value / convert(Unit, Unit.PFLOPS); }
		public float EFLOPS { get => si * convert(Unit, Unit.EFLOPS); set => si = value / convert(Unit, Unit.EFLOPS); }
		public float ZFLOPS { get => si * convert(Unit, Unit.ZFLOPS); set => si = value / convert(Unit, Unit.ZFLOPS); }
		public float YFLOPS { get => si * convert(Unit, Unit.YFLOPS); set => si = value / convert(Unit, Unit.YFLOPS); }

		public float Hz { get => si * convert(Unit, Unit.Hz); set => si = value / convert(Unit, Unit.Hz); }
		public float kHz { get => si * convert(Unit, Unit.kHz); set => si = value / convert(Unit, Unit.kHz); }
		public float MHz { get => si * convert(Unit, Unit.MHz); set => si = value / convert(Unit, Unit.MHz); }
		public float GHz { get => si * convert(Unit, Unit.GHz); set => si = value / convert(Unit, Unit.GHz); }
		public float THz { get => si * convert(Unit, Unit.THz); set => si = value / convert(Unit, Unit.THz); }

		public float ohm { get => si * convert(Unit, Unit.ohm); set => si = value / convert(Unit, Unit.ohm); }
		public float mho { get => si * convert(Unit, Unit.mho); set => si = value / convert(Unit, Unit.mho); }

		public float mi { get => si * convert(siUnit, usUnit.mi); set => si = value / convert(siUnit, usUnit.mi); }//length nmi=nautical mile, ua=astronomic unit, ly=light-year, pc=parsec(parallax second), point=printer's point
		public float yd { get => si * convert(siUnit, usUnit.yd); set => si = value / convert(siUnit, usUnit.yd); }
		public float ft { get => si * convert(siUnit, usUnit.ft); set => si = value / convert(siUnit, usUnit.ft); }
		public float inch { get => si * convert(siUnit, usUnit.inch); set => si = value / convert(siUnit, usUnit.inch); }
		public float mil { get => si * convert(siUnit, usUnit.mil); set => si = value / convert(siUnit, usUnit.mil); }
		public float microinch { get => si * convert(siUnit, usUnit.microinch); set => si = value / convert(siUnit, usUnit.microinch); }
		public float angstrom { get => si * convert(siUnit, usUnit.angstrom); set => si = value / convert(siUnit, usUnit.angstrom); }
		public float point { get => si * convert(siUnit, usUnit.point); set => si = value / convert(siUnit, usUnit.point); }
		public float nmi { get => si * convert(siUnit, usUnit.nmi); set => si = value / convert(siUnit, usUnit.nmi); }
		public float fathom { get => si * convert(siUnit, usUnit.fathom); set => si = value / convert(siUnit, usUnit.fathom); }
		public float ua { get => si * convert(siUnit, usUnit.ua); set => si = value / convert(siUnit, usUnit.ua); }
		public float ly { get => si * convert(siUnit, usUnit.ly); set => si = value / convert(siUnit, usUnit.ly); }
		public float pc { get => si * convert(siUnit, usUnit.pc); set => si = value / convert(siUnit, usUnit.pc); }

		public float mi2 { get => si * convert(siUnit, usUnit.mi2); set => si = value / convert(siUnit, usUnit.mi2); }//area ha=hectare
		public float acre { get => si * convert(siUnit, usUnit.acre); set => si = value / convert(siUnit, usUnit.acre); }
		public float yd2 { get => si * convert(siUnit, usUnit.yd2); set => si = value / convert(siUnit, usUnit.yd2); }
		public float ft2 { get => si * convert(siUnit, usUnit.ft2); set => si = value / convert(siUnit, usUnit.ft2); }
		public float in2 { get => si * convert(siUnit, usUnit.in2); set => si = value / convert(siUnit, usUnit.in2); }

		public float in3 { get => si * convert(siUnit, usUnit.in3); set => si = value / convert(siUnit, usUnit.in3); } //Volume (bbl=oil-barrel, bu=bushel
		public float ft3 { get => si * convert(siUnit, usUnit.ft3); set => si = value / convert(siUnit, usUnit.ft3); }
		public float yd3 { get => si * convert(siUnit, usUnit.yd3); set => si = value / convert(siUnit, usUnit.yd3); }
		public float acre_ft { get => si * convert(siUnit, usUnit.acre_ft); set => si = value / convert(siUnit, usUnit.acre_ft); }
		public float bbl { get => si * convert(siUnit, usUnit.bbl); set => si = value / convert(siUnit, usUnit.bbl); }
		public float bu { get => si * convert(siUnit, usUnit.bu); set => si = value / convert(siUnit, usUnit.bu); }
		public float gal { get => si * convert(siUnit, usUnit.gal); set => si = value / convert(siUnit, usUnit.gal); }
		public float qt { get => si * convert(siUnit, usUnit.qt); set => si = value / convert(siUnit, usUnit.qt); }
		public float pt { get => si * convert(siUnit, usUnit.pt); set => si = value / convert(siUnit, usUnit.pt); }
		public float cup { get => si * convert(siUnit, usUnit.cup); set => si = value / convert(siUnit, usUnit.cup); }
		public float Tbs { get => si * convert(siUnit, usUnit.tbsp); set => si = value / convert(siUnit, usUnit.tbsp); }
		public float tsp { get => si * convert(siUnit, usUnit.tsp); set => si = value / convert(siUnit, usUnit.tsp); }
		public float fl_oz { get => si * convert(siUnit, usUnit.fl_oz); set => si = value / convert(siUnit, usUnit.fl_oz); }

		public float mph { get => si * convert(siUnit, usUnit.mph); set => si = value / convert(siUnit, usUnit.mph); } //speed
		public float ftps { get => si * convert(siUnit, usUnit.ftps); set => si = value / convert(siUnit, usUnit.ftps); }
		public float kn { get => si * convert(siUnit, usUnit.kn); set => si = value / convert(siUnit, usUnit.kn); }

		public float ftps2 { get => si * convert(siUnit, usUnit.ftps2); set => si = value / convert(siUnit, usUnit.ftps2); } //acceleration
		public float inps2 { get => si * convert(siUnit, usUnit.inps2); set => si = value / convert(siUnit, usUnit.inps2); }

		public float ft3ps { get => si * convert(siUnit, usUnit.ft3ps); set => si = value / convert(siUnit, usUnit.ft3ps); } //flow rate
		public float ft3pmin { get => si * convert(siUnit, usUnit.ft3pmin); set => si = value / convert(siUnit, usUnit.ft3pmin); }
		public float yd3pmin { get => si * convert(siUnit, usUnit.yd3pmin); set => si = value / convert(siUnit, usUnit.yd3pmin); }
		public float galpmin { get => si * convert(siUnit, usUnit.galpmin); set => si = value / convert(siUnit, usUnit.galpmin); }
		public float galpday { get => si * convert(siUnit, usUnit.galpday); set => si = value / convert(siUnit, usUnit.galpday); }

		public float F { get => si * convert(siUnit, usUnit.F); set => si = value / convert(siUnit, usUnit.F); } //temperature

		public float mpg { get => si * convert(siUnit, usUnit.mpg); set => si = value / convert(siUnit, usUnit.mpg); } //fuel efficiency

		public float ton_us { get => si * convert(siUnit, usUnit.ton); set => si = value / convert(siUnit, usUnit.ton); } //weight
		public float lb { get => si * convert(siUnit, usUnit.lb); set => si = value / convert(siUnit, usUnit.lb); }
		public float oz { get => si * convert(siUnit, usUnit.oz); set => si = value / convert(siUnit, usUnit.oz); }
		public float grain { get => si * convert(siUnit, usUnit.grain); set => si = value / convert(siUnit, usUnit.grain); }

		public float lb_ft { get => si * convert(siUnit, usUnit.lb_ft); set => si = value / convert(siUnit, usUnit.lb_ft); }//moment of mass
		public float tonpyd3 { get => si * convert(siUnit, usUnit.tonpyd3); set => si = value / convert(siUnit, usUnit.tonpyd3); } //density
		public float lbpft3 { get => si * convert(siUnit, usUnit.lbpft3); set => si = value / convert(siUnit, usUnit.lbpft3); }
		public float lbfpft3 { get => si * convert(siUnit, usUnit.lbfpft3); set => si = value / convert(siUnit, usUnit.lbfpft3); }
		public float lbpgal { get => si * convert(siUnit, usUnit.lbpgal); set => si = value / convert(siUnit, usUnit.lbpgal); } //concentration
		public float ozpgal { get => si * convert(siUnit, usUnit.ozpgal); set => si = value / convert(siUnit, usUnit.ozpgal); }
		public float lb_ftps { get => si * convert(siUnit, usUnit.lb_ftps); set => si = value / convert(siUnit, usUnit.lb_ftps); }//momentum
		public float lb_ft2 { get => si * convert(siUnit, usUnit.lb_ft2); set => si = value / convert(siUnit, usUnit.lb_ft2); }//moment of inertia
		public float lbf { get => si * convert(siUnit, usUnit.lbf); set => si = value / convert(siUnit, usUnit.lbf); } //force
		public float poundal { get => si * convert(siUnit, usUnit.poundal); set => si = value / convert(siUnit, usUnit.poundal); }
		public float lbf_ft { get => si * convert(siUnit, usUnit.lbf_ft); set => si = value / convert(siUnit, usUnit.lbf_ft); }  //torque
		public float lbf_in { get => si * convert(siUnit, usUnit.lbf_in); set => si = value / convert(siUnit, usUnit.lbf_in); }
		public float psf { get => si * convert(siUnit, usUnit.psf); set => si = value / convert(siUnit, usUnit.psf); }   //pressure
		public float torr { get => si * convert(siUnit, usUnit.torr); set => si = value / convert(siUnit, usUnit.torr); }
		public float psi { get => si * convert(siUnit, usUnit.psi); set => si = value / convert(siUnit, usUnit.psi); }
		public float atm { get => si * convert(siUnit, usUnit.atm); set => si = value / convert(siUnit, usUnit.atm); }
		public float bar { get => si * convert(siUnit, usUnit.bar); set => si = value / convert(siUnit, usUnit.bar); }
		public float mbar { get => si * convert(siUnit, usUnit.mbar); set => si = value / convert(siUnit, usUnit.mbar); }
		public float ksi { get => si * convert(siUnit, usUnit.ksi); set => si = value / convert(siUnit, usUnit.ksi); }

		public float centipoise { get => si * convert(siUnit, usUnit.centipoise); set => si = value / convert(siUnit, usUnit.centipoise); }//viscosity (dynamic)
		public float centistokes { get => si * convert(siUnit, usUnit.centistokes); set => si = value / convert(siUnit, usUnit.centistokes); }//Viscosity (kinematic)
		public float kWh { get => si * convert(siUnit, usUnit.kWh); set => si = value / convert(siUnit, usUnit.kWh); } //Energy 
		public float cal { get => si * convert(siUnit, usUnit.cal); set => si = value / convert(siUnit, usUnit.cal); }
		public float Cal { get => si * convert(siUnit, usUnit.Cal); set => si = value / convert(siUnit, usUnit.Cal); }
		public float BTU { get => si * convert(siUnit, usUnit.BTU); set => si = value / convert(siUnit, usUnit.BTU); }
		public float therm { get => si * convert(siUnit, usUnit.therm); set => si = value / convert(siUnit, usUnit.therm); }
		public float hph { get => si * convert(siUnit, usUnit.hph); set => si = value / convert(siUnit, usUnit.hph); }
		public float ft_lbf { get => si * convert(siUnit, usUnit.ft_lbf); set => si = value / convert(siUnit, usUnit.ft_lbf); }
		public float BTUps { get => si * convert(siUnit, usUnit.BTUps); set => si = value / convert(siUnit, usUnit.BTUps); } //Power
		public float BTUphr { get => si * convert(siUnit, usUnit.BTUphr); set => si = value / convert(siUnit, usUnit.BTUphr); }
		public float hp { get => si * convert(siUnit, usUnit.hp); set => si = value / convert(siUnit, usUnit.hp); }
		public float ft_lbfps { get => si * convert(siUnit, usUnit.ft_lbfps); set => si = value / convert(siUnit, usUnit.ft_lbfps); }
		public float BTU_inphr_ft2F { get => si * convert(siUnit, usUnit.BTU_inphr_ft2F); set => si = value / convert(siUnit, usUnit.BTU_inphr_ft2F); }//Thermal conductivity
		public float BTUphrft2F { get => si * convert(siUnit, usUnit.BTUphrft2F); set => si = value / convert(siUnit, usUnit.BTUphrft2F); }//Coefficient of heat transfer 
		public float BTUpF { get => si * convert(siUnit, usUnit.BTUpF); set => si = value / convert(siUnit, usUnit.BTUpF); }//Heat capacity
		public float BTUplbF { get => si * convert(siUnit, usUnit.BTUplbF); set => si = value / convert(siUnit, usUnit.BTUplbF); } //Specific heat capacity 
		public float oersted { get => si * convert(siUnit, usUnit.oersted); set => si = value / convert(siUnit, usUnit.oersted); }//Magnetic field strength
		public float maxwell { get => si * convert(siUnit, usUnit.maxwell); set => si = value / convert(siUnit, usUnit.maxwell); }//Magnetic flux
		public float gauss { get => si * convert(siUnit, usUnit.gauss); set => si = value / convert(siUnit, usUnit.gauss); }//Magnetic flux density
		public float Ah { get => si * convert(siUnit, usUnit.Ah); set => si = value / convert(siUnit, usUnit.Ah); }//Electric charge
		public float lambert { get => si * convert(siUnit, usUnit.lambert); set => si = value / convert(siUnit, usUnit.lambert); } //Luminance
		public float cdpin2 { get => si * convert(siUnit, usUnit.cdpin2); set => si = value / convert(siUnit, usUnit.cdpin2); }
		public float lmpft2 { get => si * convert(siUnit, usUnit.lmpft2); set => si = value / convert(siUnit, usUnit.lmpft2); } // Luminous exitance
		public float phot { get => si * convert(siUnit, usUnit.phot); set => si = value / convert(siUnit, usUnit.phot); }
		public float fc { get => si * convert(siUnit, usUnit.fc); set => si = value / convert(siUnit, usUnit.fc); }//Illuminance
		public float Curie { get => si * convert(siUnit, usUnit.Curie); set => si = value / convert(siUnit, usUnit.Curie); }  //Activity (of a radionuclide)

		public float km { get => si * convert(siUnit, siUnit.km); set => si = value / convert(siUnit, siUnit.km); }  //length
		public float m { get => si * convert(siUnit, siUnit.m); set => si = value / convert(siUnit, siUnit.m); }
		public float cm { get => si * convert(siUnit, siUnit.cm); set => si = value / convert(siUnit, siUnit.cm); }
		public float mm { get => si * convert(siUnit, siUnit.mm); set => si = value / convert(siUnit, siUnit.mm); }
		public float um { get => si * convert(siUnit, siUnit.um); set => si = value / convert(siUnit, siUnit.um); }
		public float nm { get => si * convert(siUnit, siUnit.nm); set => si = value / convert(siUnit, siUnit.nm); }
		public float pm { get => si * convert(siUnit, siUnit.pm); set => si = value / convert(siUnit, siUnit.pm); }

		public float km2 { get => si * convert(siUnit, siUnit.km2); set => si = value / convert(siUnit, siUnit.km2); }  //area ha=hectare
		public float ha { get => si * convert(siUnit, siUnit.ha); set => si = value / convert(siUnit, siUnit.ha); }
		public float m2 { get => si * convert(siUnit, siUnit.m2); set => si = value / convert(siUnit, siUnit.m2); }
		public float cm2 { get => si * convert(siUnit, siUnit.cm2); set => si = value / convert(siUnit, siUnit.cm2); }
		public float mm2 { get => si * convert(siUnit, siUnit.mm2); set => si = value / convert(siUnit, siUnit.mm2); }

		public float m3 { get => si * convert(siUnit, siUnit.m3); set => si = value / convert(siUnit, siUnit.m3); } //Volume
		public float cm3 { get => si * convert(siUnit, siUnit.cm3); set => si = value / convert(siUnit, siUnit.cm3); }
		public float mm3 { get => si * convert(siUnit, siUnit.mm3); set => si = value / convert(siUnit, siUnit.mm3); }
		public float l { get => si * convert(siUnit, siUnit.l); set => si = value / convert(siUnit, siUnit.l); }
		public float ml { get => si * convert(siUnit, siUnit.ml); set => si = value / convert(siUnit, siUnit.ml); }

		public float kph { get => si * convert(siUnit, siUnit.kph); set => si = value / convert(siUnit, siUnit.kph); } //speed: kn = knot
		public float mps { get => si * convert(siUnit, siUnit.mps); set => si = value / convert(siUnit, siUnit.mps); }
		//public float kn { get => si * convert(siUnit, siUnit.kn); }

		public float mps2 { get => si * convert(siUnit, siUnit.mps2); set => si = value / convert(siUnit, siUnit.mps2); } //acceleration
		public float m3ps { get => si * convert(siUnit, siUnit.m3ps); set => si = value / convert(siUnit, siUnit.m3ps); } //flow rate
		public float lps { get => si * convert(siUnit, siUnit.lps); set => si = value / convert(siUnit, siUnit.lps); }
		public float lpd { get => si * convert(siUnit, siUnit.lpd); set => si = value / convert(siUnit, siUnit.lpd); }
		public float C { get => si * convert(siUnit, siUnit.C); set => si = value / convert(siUnit, siUnit.C); }//temperature
		public float K { get => si * convert(siUnit, siUnit.K); set => si = value / convert(siUnit, siUnit.K); }

		public float kpl { get => si * convert(siUnit, siUnit.kmpl); set => si = value / convert(siUnit, siUnit.kmpl); }//fuel efficiency
		public float tonsi { get => si * convert(siUnit, siUnit.ton); set => si = value / convert(siUnit, siUnit.ton); } //weight
		public float kg { get => si * convert(siUnit, siUnit.kg); set => si = value / convert(siUnit, siUnit.kg); }
		public float g { get => si * convert(siUnit, siUnit.g); set => si = value / convert(siUnit, siUnit.g); }
		public float mg { get => si * convert(siUnit, siUnit.mg); set => si = value / convert(siUnit, siUnit.mg); }
		public float ug { get => si * convert(siUnit, siUnit.ug); set => si = value / convert(siUnit, siUnit.ug); }
		public float kg_m { get => si * convert(siUnit, siUnit.kg_m); set => si = value / convert(siUnit, siUnit.kg_m); }//moment of mass
		public float kgpm3 { get => si * convert(siUnit, siUnit.kgpm3); set => si = value / convert(siUnit, siUnit.kgpm3); } //density
		public float Npm3 { get => si * convert(siUnit, siUnit.Npm3); set => si = value / convert(siUnit, siUnit.Npm3); } //density
		public float tpm3 { get => si * convert(siUnit, siUnit.Tpm3); set => si = value / convert(siUnit, siUnit.Tpm3); }
		public float gpl { get => si * convert(siUnit, siUnit.gpL); set => si = value / convert(siUnit, siUnit.gpL); }//concentration
		public float kg_mps { get => si * convert(siUnit, siUnit.kg_mps); set => si = value / convert(siUnit, siUnit.kg_mps); }//momentum
		public float kg_m2 { get => si * convert(siUnit, siUnit.kg_m2); set => si = value / convert(siUnit, siUnit.kg_m2); }//moment of inertia
		public float N { get => si * convert(siUnit, siUnit.N); set => si = value / convert(siUnit, siUnit.N); }//force
		public float Nm { get => si * convert(siUnit, siUnit.Nm); set => si = value / convert(siUnit, siUnit.Nm); } //torque
		public float Pa { get => si * convert(siUnit, siUnit.Pa); set => si = value / convert(siUnit, siUnit.Pa); }  //pressure
		public float kPa { get => si * convert(siUnit, siUnit.kPa); set => si = value / convert(siUnit, siUnit.kPa); }
		public float MPa { get => si * convert(siUnit, siUnit.MPa); set => si = value / convert(siUnit, siUnit.MPa); }
		public float GPa { get => si * convert(siUnit, siUnit.GPa); set => si = value / convert(siUnit, siUnit.GPa); }
		public float Npm2 { get => si * convert(siUnit, siUnit.Npm2); set => si = value / convert(siUnit, siUnit.Npm2); }

		public float mPa_s { get => si * convert(siUnit, siUnit.mPa_s); set => si = value / convert(siUnit, siUnit.mPa_s); }//viscosity (dynamic)
		public float mm2ps { get => si * convert(siUnit, siUnit.mm2ps); set => si = value / convert(siUnit, siUnit.mm2ps); }//Viscosity (kinematic)
		public float J { get => si * convert(siUnit, siUnit.J); set => si = value / convert(siUnit, siUnit.J); }  //Energy 
		public float kJ { get => si * convert(siUnit, siUnit.kJ); set => si = value / convert(siUnit, siUnit.kJ); }
		public float MJ { get => si * convert(siUnit, siUnit.MJ); set => si = value / convert(siUnit, siUnit.MJ); }
		public float W { get => si * convert(siUnit, siUnit.W); set => si = value / convert(siUnit, siUnit.W); } //Power
		public float kW { get => si * convert(siUnit, siUnit.kW); set => si = value / convert(siUnit, siUnit.kW); }
		public float Wpm_K { get => si * convert(siUnit, siUnit.Wpm_K); set => si = value / convert(siUnit, siUnit.Wpm_K); }//Thermal conductivity
		public float Wpm2_K { get => si * convert(siUnit, siUnit.Wpm2_K); set => si = value / convert(siUnit, siUnit.Wpm2_K); }//Coefficient of heat transfer 
		public float kJpK { get => si * convert(siUnit, siUnit.kJpK); set => si = value / convert(siUnit, siUnit.kJpK); }//Heat capacity
		public float kJpkg_K { get => si * convert(siUnit, siUnit.kJpkg_K); set => si = value / convert(siUnit, siUnit.kJpkg_K); } //Specific heat capacity 
		public float Apm { get => si * convert(siUnit, siUnit.Apm); set => si = value / convert(siUnit, siUnit.Apm); }//Magnetic field strength
		public float nWb { get => si * convert(siUnit, siUnit.nWb); set => si = value / convert(siUnit, siUnit.nWb); }//Magnetic flux
		public float mT { get => si * convert(siUnit, siUnit.mT); set => si = value / convert(siUnit, siUnit.mT); }//Magnetic flux density
		public float Coulomb { get => si * convert(siUnit, siUnit.Coulomb); set => si = value / convert(siUnit, siUnit.Coulomb); }//Electric charge
		public float cdpm2 { get => si * convert(siUnit, siUnit.cdpm2); set => si = value / convert(siUnit, siUnit.cdpm2); }//Luminance
		public float lx { get => si * convert(siUnit, siUnit.lx); set => si = value / convert(siUnit, siUnit.lx); }// Luminous exitance { get => si * convert(siUnit, siUnit.mho); set => si = value / convert(siUnit, siUnit.xxx); }mph; Illuminance
		public float MBq { get => si * convert(siUnit, siUnit.MBq); set => si = value / convert(siUnit, siUnit.MBq); }  //Activity (of a radionuclide)
	}
}