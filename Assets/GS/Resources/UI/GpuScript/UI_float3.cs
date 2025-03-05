using System;
using System.Reflection;
using UnityEngine.UIElements;
using static GpuScript.GS;

namespace GpuScript
{
	public class UI_float3 : UI_Slider_base
	{
		public float3 Slider_Pow_Val(float3 v) => clamp(round(is_Pow2_Slider ? (pow10(abs(v)) - 1) / 0.999f : v, Nearest), range_Min, range_Max);
		public float3 Slider_Log_Val(float3 v) => is_Pow2_Slider ? log10(abs(clamp(round(v, Nearest), range_Min, range_Max)) * 0.999f + 1) : v;
		public float3 SliderV
		{
			get => Slider_Pow_Val(new float3(sliders[0].value, sliders[1].value, sliders[2].value));
			set
			{
				var v = Slider_Log_Val(value);
				sliders[0].value = v.x;
				sliders[1].value = v.y;
				sliders[2].value = v.z;
			}
		}
		public override Slider[] GetSliders() => new Slider[] { this.Q<Slider>("slider_x"), this.Q<Slider>("slider_y"), this.Q<Slider>("slider_z") };
		public UI_float3() : base() { }
		public override void OnMouseCaptureEvent(MouseCaptureEvent evt) { base.OnMouseCaptureEvent(evt); if (evt.currentTarget is TextField) { var o = evt.currentTarget as TextField; previousValue = o.value.To_float3(); } }
		public override void OnMouseCaptureOutEvent(MouseCaptureOutEvent evt) { base.OnMouseCaptureOutEvent(evt); if (evt == null || evt.currentTarget is TextField) { var o = textField; if (changed && any(previousValue != o.value.To_float3())) { OnTextFieldChanged(o); previousValue = o.value.To_float3(); changed = false; } } }
		public static Type Get_Base_Type() => typeof(float3);
		public static bool IsType(Type type) => type == typeof(float3);
		public override bool Init(GS gs, params GS[] gss) { if (!base.Init(gs, gss)) return false; v = textField.value.To_float3(); return true; }
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
		float3 _v0 = default;
		public float3 _si; public float3 si { get => _si; set => v = value; }
		public void Text(float3 val)
		{
			if (siUnit != siUnit.Null && usUnit != usUnit.Null) textField.value = (siUnits ? val : convert(val)).ToString(format);
			else textField.value = val.ToString(format);
		}
		public float3 v
		{
			get => _si;
			set
			{
				if (any(isnan(value)) || any(isinf(value))) return;
				var val = is_Pow10 ? round(pow10(round(log10(value)))) : is_Pow2 ? round(pow2(round(log2(value)))) : value;
				if (Nearest > 0) val = round(val, Nearest);
				Text(val);
				_si = val;
				if (hasRange) 
					SliderV = val;
			}
		}
		public override string textString => v.ToString(format);
		public override object v_obj { get => v; set => v = value.To_float3(); }
		public override void OnUnitsChanged() { base.OnUnitsChanged(); if (siUnit != siUnit.Null && usUnit != usUnit.Null && textField != null) textField.value = (siUnits ? iconvert(si) : convert(si)).ToString(format); }

		public override bool hasRange { get => any(range_Min < range_Max); }
		float3 _range_Min; public float3 range_Min { get => _range_Min; set { _range_Min = value; if (sliders[0] != null) { var v = Slider_Log_Val(value); for (int i = 0; i < sliders.Length; i++) sliders[i].lowValue = v[i]; } } }
		float3 _range_Max; public float3 range_Max { get => _range_Max; set { _range_Max = value; if (sliders[0] != null) { var v = Slider_Log_Val(value); for (int i = 0; i < sliders.Length; i++) sliders[i].highValue = v[i]; } } }

		public float3 previousValue;
		public override void OnValueChanged(ChangeEvent<float> evt)
		{
			if (evt.currentTarget is Slider && textField != null)
			{
				float3 val = SliderV;
				Text(val);
				SetPropertyValue(val);
			}
		}
		public override void OnTextFieldChanged(TextField o)
		{
			float3 val = o.value.To_float3();
			if (siUnit != siUnit.Null && usUnit != usUnit.Null && !siUnits)
				val = iconvert(val);
			SetPropertyValue(SliderV = val);
		}

		public void Build(string title, string description, string val, string rangeMin, string rangeMax, string _siUnit, string _usUnit, string _Unit,
			string siFormat, string usFormat, bool isReadOnly, bool isGrid, bool isPow2Slider, bool isPow10, bool isPow2, float nearest, string treeGroup_parent)
		{
			base.Build(title, description, _siUnit, _usUnit, _Unit, siFormat, usFormat, isReadOnly, isGrid, isPow2Slider, isPow10, isPow2, nearest, treeGroup_parent);
			range_Min = rangeMin.To_float3(); range_Max = rangeMax.To_float3(); SliderV = val.To_float3();
		}

		public new class UxmlFactory : UxmlFactory<UI_float3, UxmlTraits> { }
		public new class UxmlTraits : UI_VisualElement.UxmlTraits
		{
			UxmlStringAttributeDescription m_float3_value = new UxmlStringAttributeDescription { name = "UI_float3_value" };
			UxmlStringAttributeDescription m_float3_min = new UxmlStringAttributeDescription { name = "UI_float3_min" };
			UxmlStringAttributeDescription m_float3_max = new UxmlStringAttributeDescription { name = "UI_float3_max" };
			UxmlStringAttributeDescription m_float3_siFormat = new UxmlStringAttributeDescription { name = "UI_float3_siFormat", defaultValue = "0.0000" };
			UxmlStringAttributeDescription m_float3_usFormat = new UxmlStringAttributeDescription { name = "UI_float3_usFormat", defaultValue = "0.0000" };
			UxmlStringAttributeDescription m_float3_siUnit = new UxmlStringAttributeDescription { name = "UI_float3_siUnit", defaultValue = "" };
			UxmlStringAttributeDescription m_float3_usUnit = new UxmlStringAttributeDescription { name = "UI_float3_usUnit", defaultValue = "" };
			UxmlStringAttributeDescription m_float3_Unit = new UxmlStringAttributeDescription { name = "UI_float3_Unit", defaultValue = "" };

			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				((UI_float3)ve).Build(m_Label.GetValueFromBag(bag, cc), m_Description.GetValueFromBag(bag, cc),
					m_float3_value.GetValueFromBag(bag, cc), m_float3_min.GetValueFromBag(bag, cc), m_float3_max.GetValueFromBag(bag, cc),
					m_float3_siUnit.GetValueFromBag(bag, cc), m_float3_usUnit.GetValueFromBag(bag, cc), m_float3_Unit.GetValueFromBag(bag, cc),
					m_float3_siFormat.GetValueFromBag(bag, cc), m_float3_usFormat.GetValueFromBag(bag, cc),
					m_isReadOnly.GetValueFromBag(bag, cc), m_isGrid.GetValueFromBag(bag, cc), m_isPow2Slider.GetValueFromBag(bag, cc),
					m_isPow10.GetValueFromBag(bag, cc), m_isPow2.GetValueFromBag(bag, cc), m_Nearest.GetValueFromBag(bag, cc),
					m_TreeGroup_Parent.GetValueFromBag(bag, cc));
			}
		}

		//public float3 siRange, usRange;
		public override bool Changed { get => any(v != _v0); set => _v0 = value ? v - 1 : v; }

		public static implicit operator float3(UI_float3 f) => f.si;

		public float3 ns { get => si * convert(Unit, Unit.ns); set => si = value / convert(Unit, Unit.ns); }
		public float3 us { get => si * convert(Unit, Unit.us); set => si = value / convert(Unit, Unit.us); }
		public float3 ms { get => si * convert(Unit, Unit.ms); set => si = value / convert(Unit, Unit.ms); }
		public float3 s { get => si * convert(Unit, Unit.s); set => si = value / convert(Unit, Unit.s); }
		public float3 min { get => si * convert(Unit, Unit.min); set => si = value / convert(Unit, Unit.min); }
		public float3 hr { get => si * convert(Unit, Unit.hr); set => si = value / convert(Unit, Unit.hr); }
		public float3 day { get => si * convert(Unit, Unit.day); set => si = value / convert(Unit, Unit.day); }
		public float3 week { get => si * convert(Unit, Unit.week); set => si = value / convert(Unit, Unit.week); }
		public float3 month { get => si * convert(Unit, Unit.month); set => si = value / convert(Unit, Unit.month); }
		public float3 year { get => si * convert(Unit, Unit.year); set => si = value / convert(Unit, Unit.year); }

		public float3 deg { get => si * convert(Unit, Unit.deg); set => si = value / convert(Unit, Unit.deg); }
		public float3 rad { get => si * convert(Unit, Unit.rad); set => si = value / convert(Unit, Unit.rad); }

		public float3 bit { get => si * convert(Unit, Unit.bit); set => si = value / convert(Unit, Unit.bit); }
		public float3 Byte { get => si * convert(Unit, Unit.Byte); set => si = value / convert(Unit, Unit.Byte); }
		public float3 KB { get => si * convert(Unit, Unit.KB); set => si = value / convert(Unit, Unit.KB); }
		public float3 MB { get => si * convert(Unit, Unit.MB); set => si = value / convert(Unit, Unit.MB); }
		public float3 GB { get => si * convert(Unit, Unit.GB); set => si = value / convert(Unit, Unit.GB); }
		public float3 TB { get => si * convert(Unit, Unit.TB); set => si = value / convert(Unit, Unit.TB); }
		public float3 PB { get => si * convert(Unit, Unit.PB); set => si = value / convert(Unit, Unit.PB); }

		public float3 bps { get => si * convert(Unit, Unit.bps); set => si = value / convert(Unit, Unit.bps); }
		public float3 Kbps { get => si * convert(Unit, Unit.Kbps); set => si = value / convert(Unit, Unit.Kbps); }
		public float3 Mbps { get => si * convert(Unit, Unit.Mbps); set => si = value / convert(Unit, Unit.Mbps); }
		public float3 Gbps { get => si * convert(Unit, Unit.Gbps); set => si = value / convert(Unit, Unit.Gbps); }
		public float3 Tbps { get => si * convert(Unit, Unit.Tbps); set => si = value / convert(Unit, Unit.Tbps); }
		public float3 Pbps { get => si * convert(Unit, Unit.Pbps); set => si = value / convert(Unit, Unit.Pbps); }
		public float3 Bps { get => si * convert(Unit, Unit.Bps); set => si = value / convert(Unit, Unit.Bps); }
		public float3 KBps { get => si * convert(Unit, Unit.KBps); set => si = value / convert(Unit, Unit.KBps); }
		public float3 MBps { get => si * convert(Unit, Unit.MBps); set => si = value / convert(Unit, Unit.MBps); }
		public float3 GBps { get => si * convert(Unit, Unit.GBps); set => si = value / convert(Unit, Unit.GBps); }
		public float3 TBps { get => si * convert(Unit, Unit.TBps); set => si = value / convert(Unit, Unit.TBps); }
		public float3 PBps { get => si * convert(Unit, Unit.PBps); set => si = value / convert(Unit, Unit.PBps); }

		public float3 FLOPS { get => si * convert(Unit, Unit.FLOPS); set => si = value / convert(Unit, Unit.FLOPS); }
		public float3 kFLOPS { get => si * convert(Unit, Unit.kFLOPS); set => si = value / convert(Unit, Unit.kFLOPS); }
		public float3 MFLOPS { get => si * convert(Unit, Unit.MFLOPS); set => si = value / convert(Unit, Unit.MFLOPS); }
		public float3 GFLOPS { get => si * convert(Unit, Unit.GFLOPS); set => si = value / convert(Unit, Unit.GFLOPS); }
		public float3 TFLOPS { get => si * convert(Unit, Unit.TFLOPS); set => si = value / convert(Unit, Unit.TFLOPS); }
		public float3 PFLOPS { get => si * convert(Unit, Unit.PFLOPS); set => si = value / convert(Unit, Unit.PFLOPS); }
		public float3 EFLOPS { get => si * convert(Unit, Unit.EFLOPS); set => si = value / convert(Unit, Unit.EFLOPS); }
		public float3 ZFLOPS { get => si * convert(Unit, Unit.ZFLOPS); set => si = value / convert(Unit, Unit.ZFLOPS); }
		public float3 YFLOPS { get => si * convert(Unit, Unit.YFLOPS); set => si = value / convert(Unit, Unit.YFLOPS); }

		public float3 Hz { get => si * convert(Unit, Unit.Hz); set => si = value / convert(Unit, Unit.Hz); }
		public float3 kHz { get => si * convert(Unit, Unit.kHz); set => si = value / convert(Unit, Unit.kHz); }
		public float3 MHz { get => si * convert(Unit, Unit.MHz); set => si = value / convert(Unit, Unit.MHz); }
		public float3 GHz { get => si * convert(Unit, Unit.GHz); set => si = value / convert(Unit, Unit.GHz); }
		public float3 THz { get => si * convert(Unit, Unit.THz); set => si = value / convert(Unit, Unit.THz); }

		public float3 ohm { get => si * convert(Unit, Unit.ohm); set => si = value / convert(Unit, Unit.ohm); }
		public float3 mho { get => si * convert(Unit, Unit.mho); set => si = value / convert(Unit, Unit.mho); }

		public float3 mi { get => si * convert(siUnit, usUnit.mi); set => si = value / convert(siUnit, usUnit.mi); }//length nmi=nautical mile, ua=astronomic unit, ly=light-year, pc=parsec(parallax second), point=printer's point
		public float3 yd { get => si * convert(siUnit, usUnit.yd); set => si = value / convert(siUnit, usUnit.yd); }
		public float3 ft { get => si * convert(siUnit, usUnit.ft); set => si = value / convert(siUnit, usUnit.ft); }
		public float3 inch { get => si * convert(siUnit, usUnit.inch); set => si = value / convert(siUnit, usUnit.inch); }
		public float3 mil { get => si * convert(siUnit, usUnit.mil); set => si = value / convert(siUnit, usUnit.mil); }
		public float3 microinch { get => si * convert(siUnit, usUnit.microinch); set => si = value / convert(siUnit, usUnit.microinch); }
		public float3 angstrom { get => si * convert(siUnit, usUnit.angstrom); set => si = value / convert(siUnit, usUnit.angstrom); }
		public float3 point { get => si * convert(siUnit, usUnit.point); set => si = value / convert(siUnit, usUnit.point); }
		public float3 nmi { get => si * convert(siUnit, usUnit.nmi); set => si = value / convert(siUnit, usUnit.nmi); }
		public float3 fathom { get => si * convert(siUnit, usUnit.fathom); set => si = value / convert(siUnit, usUnit.fathom); }
		public float3 ua { get => si * convert(siUnit, usUnit.ua); set => si = value / convert(siUnit, usUnit.ua); }
		public float3 ly { get => si * convert(siUnit, usUnit.ly); set => si = value / convert(siUnit, usUnit.ly); }
		public float3 pc { get => si * convert(siUnit, usUnit.pc); set => si = value / convert(siUnit, usUnit.pc); }

		public float3 mi2 { get => si * convert(siUnit, usUnit.mi2); set => si = value / convert(siUnit, usUnit.mi2); }//area ha=hectare
		public float3 acre { get => si * convert(siUnit, usUnit.acre); set => si = value / convert(siUnit, usUnit.acre); }
		public float3 yd2 { get => si * convert(siUnit, usUnit.yd2); set => si = value / convert(siUnit, usUnit.yd2); }
		public float3 ft2 { get => si * convert(siUnit, usUnit.ft2); set => si = value / convert(siUnit, usUnit.ft2); }
		public float3 in2 { get => si * convert(siUnit, usUnit.in2); set => si = value / convert(siUnit, usUnit.in2); }

		public float3 in3 { get => si * convert(siUnit, usUnit.in3); set => si = value / convert(siUnit, usUnit.in3); } //Volume (bbl=oil-barrel, bu=bushel
		public float3 ft3 { get => si * convert(siUnit, usUnit.ft3); set => si = value / convert(siUnit, usUnit.ft3); }
		public float3 yd3 { get => si * convert(siUnit, usUnit.yd3); set => si = value / convert(siUnit, usUnit.yd3); }
		public float3 acre_ft { get => si * convert(siUnit, usUnit.acre_ft); set => si = value / convert(siUnit, usUnit.acre_ft); }
		public float3 bbl { get => si * convert(siUnit, usUnit.bbl); set => si = value / convert(siUnit, usUnit.bbl); }
		public float3 bu { get => si * convert(siUnit, usUnit.bu); set => si = value / convert(siUnit, usUnit.bu); }
		public float3 gal { get => si * convert(siUnit, usUnit.gal); set => si = value / convert(siUnit, usUnit.gal); }
		public float3 qt { get => si * convert(siUnit, usUnit.qt); set => si = value / convert(siUnit, usUnit.qt); }
		public float3 pt { get => si * convert(siUnit, usUnit.pt); set => si = value / convert(siUnit, usUnit.pt); }
		public float3 cup { get => si * convert(siUnit, usUnit.cup); set => si = value / convert(siUnit, usUnit.cup); }
		public float3 Tbs { get => si * convert(siUnit, usUnit.tbsp); set => si = value / convert(siUnit, usUnit.tbsp); }
		public float3 tsp { get => si * convert(siUnit, usUnit.tsp); set => si = value / convert(siUnit, usUnit.tsp); }
		public float3 fl_oz { get => si * convert(siUnit, usUnit.fl_oz); set => si = value / convert(siUnit, usUnit.fl_oz); }

		public float3 mph { get => si * convert(siUnit, usUnit.mph); set => si = value / convert(siUnit, usUnit.mph); } //speed
		public float3 ftps { get => si * convert(siUnit, usUnit.ftps); set => si = value / convert(siUnit, usUnit.ftps); }
		public float3 kn { get => si * convert(siUnit, usUnit.kn); set => si = value / convert(siUnit, usUnit.kn); }

		public float3 ftps2 { get => si * convert(siUnit, usUnit.ftps2); set => si = value / convert(siUnit, usUnit.ftps2); } //acceleration
		public float3 inps2 { get => si * convert(siUnit, usUnit.inps2); set => si = value / convert(siUnit, usUnit.inps2); }

		public float3 ft3ps { get => si * convert(siUnit, usUnit.ft3ps); set => si = value / convert(siUnit, usUnit.ft3ps); } //flow rate
		public float3 ft3pmin { get => si * convert(siUnit, usUnit.ft3pmin); set => si = value / convert(siUnit, usUnit.ft3pmin); }
		public float3 yd3pmin { get => si * convert(siUnit, usUnit.yd3pmin); set => si = value / convert(siUnit, usUnit.yd3pmin); }
		public float3 galpmin { get => si * convert(siUnit, usUnit.galpmin); set => si = value / convert(siUnit, usUnit.galpmin); }
		public float3 galpday { get => si * convert(siUnit, usUnit.galpday); set => si = value / convert(siUnit, usUnit.galpday); }

		public float3 F { get => si * convert(siUnit, usUnit.F); set => si = value / convert(siUnit, usUnit.F); } //temperature

		public float3 mpg { get => si * convert(siUnit, usUnit.mpg); set => si = value / convert(siUnit, usUnit.mpg); } //fuel efficiency

		public float3 ton_us { get => si * convert(siUnit, usUnit.ton); set => si = value / convert(siUnit, usUnit.ton); } //weight
		public float3 lb { get => si * convert(siUnit, usUnit.lb); set => si = value / convert(siUnit, usUnit.lb); }
		public float3 oz { get => si * convert(siUnit, usUnit.oz); set => si = value / convert(siUnit, usUnit.oz); }
		public float3 grain { get => si * convert(siUnit, usUnit.grain); set => si = value / convert(siUnit, usUnit.grain); }

		public float3 lb_ft { get => si * convert(siUnit, usUnit.lb_ft); set => si = value / convert(siUnit, usUnit.lb_ft); }//moment of mass
		public float3 tonpyd3 { get => si * convert(siUnit, usUnit.tonpyd3); set => si = value / convert(siUnit, usUnit.tonpyd3); } //density
		public float3 lbpft3 { get => si * convert(siUnit, usUnit.lbpft3); set => si = value / convert(siUnit, usUnit.lbpft3); }
		public float3 lbfpft3 { get => si * convert(siUnit, usUnit.lbfpft3); set => si = value / convert(siUnit, usUnit.lbfpft3); }
		public float3 lbpgal { get => si * convert(siUnit, usUnit.lbpgal); set => si = value / convert(siUnit, usUnit.lbpgal); } //concentration
		public float3 ozpgal { get => si * convert(siUnit, usUnit.ozpgal); set => si = value / convert(siUnit, usUnit.ozpgal); }
		public float3 lb_ftps { get => si * convert(siUnit, usUnit.lb_ftps); set => si = value / convert(siUnit, usUnit.lb_ftps); }//momentum
		public float3 lb_ft2 { get => si * convert(siUnit, usUnit.lb_ft2); set => si = value / convert(siUnit, usUnit.lb_ft2); }//moment of inertia
		public float3 lbf { get => si * convert(siUnit, usUnit.lbf); set => si = value / convert(siUnit, usUnit.lbf); } //force
		public float3 poundal { get => si * convert(siUnit, usUnit.poundal); set => si = value / convert(siUnit, usUnit.poundal); }
		public float3 lbf_ft { get => si * convert(siUnit, usUnit.lbf_ft); set => si = value / convert(siUnit, usUnit.lbf_ft); }  //torque
		public float3 lbf_in { get => si * convert(siUnit, usUnit.lbf_in); set => si = value / convert(siUnit, usUnit.lbf_in); }
		public float3 psf { get => si * convert(siUnit, usUnit.psf); set => si = value / convert(siUnit, usUnit.psf); }   //pressure
		public float3 torr { get => si * convert(siUnit, usUnit.torr); set => si = value / convert(siUnit, usUnit.torr); }
		public float3 psi { get => si * convert(siUnit, usUnit.psi); set => si = value / convert(siUnit, usUnit.psi); }
		public float3 atm { get => si * convert(siUnit, usUnit.atm); set => si = value / convert(siUnit, usUnit.atm); }
		public float3 bar { get => si * convert(siUnit, usUnit.bar); set => si = value / convert(siUnit, usUnit.bar); }
		public float3 mbar { get => si * convert(siUnit, usUnit.mbar); set => si = value / convert(siUnit, usUnit.mbar); }
		public float3 ksi { get => si * convert(siUnit, usUnit.ksi); set => si = value / convert(siUnit, usUnit.ksi); }

		public float3 centipoise { get => si * convert(siUnit, usUnit.centipoise); set => si = value / convert(siUnit, usUnit.centipoise); }//viscosity (dynamic)
		public float3 centistokes { get => si * convert(siUnit, usUnit.centistokes); set => si = value / convert(siUnit, usUnit.centistokes); }//Viscosity (kinematic)
		public float3 kWh { get => si * convert(siUnit, usUnit.kWh); set => si = value / convert(siUnit, usUnit.kWh); } //Energy 
		public float3 cal { get => si * convert(siUnit, usUnit.cal); set => si = value / convert(siUnit, usUnit.cal); }
		public float3 Cal { get => si * convert(siUnit, usUnit.Cal); set => si = value / convert(siUnit, usUnit.Cal); }
		public float3 BTU { get => si * convert(siUnit, usUnit.BTU); set => si = value / convert(siUnit, usUnit.BTU); }
		public float3 therm { get => si * convert(siUnit, usUnit.therm); set => si = value / convert(siUnit, usUnit.therm); }
		public float3 hph { get => si * convert(siUnit, usUnit.hph); set => si = value / convert(siUnit, usUnit.hph); }
		public float3 ft_lbf { get => si * convert(siUnit, usUnit.ft_lbf); set => si = value / convert(siUnit, usUnit.ft_lbf); }
		public float3 BTUps { get => si * convert(siUnit, usUnit.BTUps); set => si = value / convert(siUnit, usUnit.BTUps); } //Power
		public float3 BTUphr { get => si * convert(siUnit, usUnit.BTUphr); set => si = value / convert(siUnit, usUnit.BTUphr); }
		public float3 hp { get => si * convert(siUnit, usUnit.hp); set => si = value / convert(siUnit, usUnit.hp); }
		public float3 ft_lbfps { get => si * convert(siUnit, usUnit.ft_lbfps); set => si = value / convert(siUnit, usUnit.ft_lbfps); }
		public float3 BTU_inphr_ft2F { get => si * convert(siUnit, usUnit.BTU_inphr_ft2F); set => si = value / convert(siUnit, usUnit.BTU_inphr_ft2F); }//Thermal conductivity
		public float3 BTUphrft2F { get => si * convert(siUnit, usUnit.BTUphrft2F); set => si = value / convert(siUnit, usUnit.BTUphrft2F); }//Coefficient of heat transfer 
		public float3 BTUpF { get => si * convert(siUnit, usUnit.BTUpF); set => si = value / convert(siUnit, usUnit.BTUpF); }//Heat capacity
		public float3 BTUplbF { get => si * convert(siUnit, usUnit.BTUplbF); set => si = value / convert(siUnit, usUnit.BTUplbF); } //Specific heat capacity 
		public float3 oersted { get => si * convert(siUnit, usUnit.oersted); set => si = value / convert(siUnit, usUnit.oersted); }//Magnetic field strength
		public float3 maxwell { get => si * convert(siUnit, usUnit.maxwell); set => si = value / convert(siUnit, usUnit.maxwell); }//Magnetic flux
		public float3 gauss { get => si * convert(siUnit, usUnit.gauss); set => si = value / convert(siUnit, usUnit.gauss); }//Magnetic flux density
		public float3 Ah { get => si * convert(siUnit, usUnit.Ah); set => si = value / convert(siUnit, usUnit.Ah); }//Electric charge
		public float3 lambert { get => si * convert(siUnit, usUnit.lambert); set => si = value / convert(siUnit, usUnit.lambert); } //Luminance
		public float3 cdpin2 { get => si * convert(siUnit, usUnit.cdpin2); set => si = value / convert(siUnit, usUnit.cdpin2); }
		public float3 lmpft2 { get => si * convert(siUnit, usUnit.lmpft2); set => si = value / convert(siUnit, usUnit.lmpft2); } // Luminous exitance
		public float3 phot { get => si * convert(siUnit, usUnit.phot); set => si = value / convert(siUnit, usUnit.phot); }
		public float3 fc { get => si * convert(siUnit, usUnit.fc); set => si = value / convert(siUnit, usUnit.fc); }//Illuminance
		public float3 Curie { get => si * convert(siUnit, usUnit.Curie); set => si = value / convert(siUnit, usUnit.Curie); }  //Activity (of a radionuclide)

		public float3 km { get => si * convert(siUnit, siUnit.km); set => si = value / convert(siUnit, siUnit.km); }  //length
		public float3 m { get => si * convert(siUnit, siUnit.m); set => si = value / convert(siUnit, siUnit.m); }
		public float3 cm { get => si * convert(siUnit, siUnit.cm); set => si = value / convert(siUnit, siUnit.cm); }
		public float3 mm { get => si * convert(siUnit, siUnit.mm); set => si = value / convert(siUnit, siUnit.mm); }
		public float3 um { get => si * convert(siUnit, siUnit.um); set => si = value / convert(siUnit, siUnit.um); }
		public float3 nm { get => si * convert(siUnit, siUnit.nm); set => si = value / convert(siUnit, siUnit.nm); }
		public float3 pm { get => si * convert(siUnit, siUnit.pm); set => si = value / convert(siUnit, siUnit.pm); }

		public float3 km2 { get => si * convert(siUnit, siUnit.km2); set => si = value / convert(siUnit, siUnit.km2); }  //area ha=hectare
		public float3 ha { get => si * convert(siUnit, siUnit.ha); set => si = value / convert(siUnit, siUnit.ha); }
		public float3 m2 { get => si * convert(siUnit, siUnit.m2); set => si = value / convert(siUnit, siUnit.m2); }
		public float3 cm2 { get => si * convert(siUnit, siUnit.cm2); set => si = value / convert(siUnit, siUnit.cm2); }
		public float3 mm2 { get => si * convert(siUnit, siUnit.mm2); set => si = value / convert(siUnit, siUnit.mm2); }

		public float3 m3 { get => si * convert(siUnit, siUnit.m3); set => si = value / convert(siUnit, siUnit.m3); } //Volume
		public float3 cm3 { get => si * convert(siUnit, siUnit.cm3); set => si = value / convert(siUnit, siUnit.cm3); }
		public float3 mm3 { get => si * convert(siUnit, siUnit.mm3); set => si = value / convert(siUnit, siUnit.mm3); }
		public float3 l { get => si * convert(siUnit, siUnit.l); set => si = value / convert(siUnit, siUnit.l); }
		public float3 ml { get => si * convert(siUnit, siUnit.ml); set => si = value / convert(siUnit, siUnit.ml); }

		public float3 kph { get => si * convert(siUnit, siUnit.kph); set => si = value / convert(siUnit, siUnit.kph); } //speed: kn = knot
		public float3 mps { get => si * convert(siUnit, siUnit.mps); set => si = value / convert(siUnit, siUnit.mps); }
		//public float3 kn { get => si * convert(siUnit, siUnit.kn); }

		public float3 mps2 { get => si * convert(siUnit, siUnit.mps2); set => si = value / convert(siUnit, siUnit.mps2); } //acceleration
		public float3 m3ps { get => si * convert(siUnit, siUnit.m3ps); set => si = value / convert(siUnit, siUnit.m3ps); } //flow rate
		public float3 lps { get => si * convert(siUnit, siUnit.lps); set => si = value / convert(siUnit, siUnit.lps); }
		public float3 lpd { get => si * convert(siUnit, siUnit.lpd); set => si = value / convert(siUnit, siUnit.lpd); }
		public float3 C { get => si * convert(siUnit, siUnit.C); set => si = value / convert(siUnit, siUnit.C); }//temperature
		public float3 K { get => si * convert(siUnit, siUnit.K); set => si = value / convert(siUnit, siUnit.K); }

		public float3 kpl { get => si * convert(siUnit, siUnit.kmpl); set => si = value / convert(siUnit, siUnit.kmpl); }//fuel efficiency
		public float3 tonsi { get => si * convert(siUnit, siUnit.ton); set => si = value / convert(siUnit, siUnit.ton); } //weight
		public float3 kg { get => si * convert(siUnit, siUnit.kg); set => si = value / convert(siUnit, siUnit.kg); }
		public float3 g { get => si * convert(siUnit, siUnit.g); set => si = value / convert(siUnit, siUnit.g); }
		public float3 mg { get => si * convert(siUnit, siUnit.mg); set => si = value / convert(siUnit, siUnit.mg); }
		public float3 ug { get => si * convert(siUnit, siUnit.ug); set => si = value / convert(siUnit, siUnit.ug); }
		public float3 kg_m { get => si * convert(siUnit, siUnit.kg_m); set => si = value / convert(siUnit, siUnit.kg_m); }//moment of mass
		public float3 kgpm3 { get => si * convert(siUnit, siUnit.kgpm3); set => si = value / convert(siUnit, siUnit.kgpm3); } //density
		public float3 Npm3 { get => si * convert(siUnit, siUnit.Npm3); set => si = value / convert(siUnit, siUnit.Npm3); } //density
		public float3 tpm3 { get => si * convert(siUnit, siUnit.Tpm3); set => si = value / convert(siUnit, siUnit.Tpm3); }
		public float3 gpl { get => si * convert(siUnit, siUnit.gpL); set => si = value / convert(siUnit, siUnit.gpL); }//concentration
		public float3 kg_mps { get => si * convert(siUnit, siUnit.kg_mps); set => si = value / convert(siUnit, siUnit.kg_mps); }//momentum
		public float3 kg_m2 { get => si * convert(siUnit, siUnit.kg_m2); set => si = value / convert(siUnit, siUnit.kg_m2); }//moment of inertia
		public float3 N { get => si * convert(siUnit, siUnit.N); set => si = value / convert(siUnit, siUnit.N); }//force
		public float3 Nm { get => si * convert(siUnit, siUnit.Nm); set => si = value / convert(siUnit, siUnit.Nm); } //torque
		public float3 Pa { get => si * convert(siUnit, siUnit.Pa); set => si = value / convert(siUnit, siUnit.Pa); }  //pressure
		public float3 kPa { get => si * convert(siUnit, siUnit.kPa); set => si = value / convert(siUnit, siUnit.kPa); }
		public float3 MPa { get => si * convert(siUnit, siUnit.MPa); set => si = value / convert(siUnit, siUnit.MPa); }
		public float3 GPa { get => si * convert(siUnit, siUnit.GPa); set => si = value / convert(siUnit, siUnit.GPa); }
		public float3 Npm2 { get => si * convert(siUnit, siUnit.Npm2); set => si = value / convert(siUnit, siUnit.Npm2); }

		public float3 mPa_s { get => si * convert(siUnit, siUnit.mPa_s); set => si = value / convert(siUnit, siUnit.mPa_s); }//viscosity (dynamic)
		public float3 mm2ps { get => si * convert(siUnit, siUnit.mm2ps); set => si = value / convert(siUnit, siUnit.mm2ps); }//Viscosity (kinematic)
		public float3 J { get => si * convert(siUnit, siUnit.J); set => si = value / convert(siUnit, siUnit.J); }  //Energy 
		public float3 kJ { get => si * convert(siUnit, siUnit.kJ); set => si = value / convert(siUnit, siUnit.kJ); }
		public float3 MJ { get => si * convert(siUnit, siUnit.MJ); set => si = value / convert(siUnit, siUnit.MJ); }
		public float3 W { get => si * convert(siUnit, siUnit.W); set => si = value / convert(siUnit, siUnit.W); } //Power
		public float3 kW { get => si * convert(siUnit, siUnit.kW); set => si = value / convert(siUnit, siUnit.kW); }
		public float3 Wpm_K { get => si * convert(siUnit, siUnit.Wpm_K); set => si = value / convert(siUnit, siUnit.Wpm_K); }//Thermal conductivity
		public float3 Wpm2_K { get => si * convert(siUnit, siUnit.Wpm2_K); set => si = value / convert(siUnit, siUnit.Wpm2_K); }//Coefficient of heat transfer 
		public float3 kJpK { get => si * convert(siUnit, siUnit.kJpK); set => si = value / convert(siUnit, siUnit.kJpK); }//Heat capacity
		public float3 kJpkg_K { get => si * convert(siUnit, siUnit.kJpkg_K); set => si = value / convert(siUnit, siUnit.kJpkg_K); } //Specific heat capacity 
		public float3 Apm { get => si * convert(siUnit, siUnit.Apm); set => si = value / convert(siUnit, siUnit.Apm); }//Magnetic field strength
		public float3 nWb { get => si * convert(siUnit, siUnit.nWb); set => si = value / convert(siUnit, siUnit.nWb); }//Magnetic flux
		public float3 mT { get => si * convert(siUnit, siUnit.mT); set => si = value / convert(siUnit, siUnit.mT); }//Magnetic flux density
		public float3 Coulomb { get => si * convert(siUnit, siUnit.Coulomb); set => si = value / convert(siUnit, siUnit.Coulomb); }//Electric charge
		public float3 cdpm2 { get => si * convert(siUnit, siUnit.cdpm2); set => si = value / convert(siUnit, siUnit.cdpm2); }//Luminance
		public float3 lx { get => si * convert(siUnit, siUnit.lx); set => si = value / convert(siUnit, siUnit.lx); }// Luminous exitance { get => si * convert(siUnit, siUnit.mho); set => si = value / convert(siUnit, siUnit.xxx); }mph; Illuminance
		public float3 MBq { get => si * convert(siUnit, siUnit.MBq); set => si = value / convert(siUnit, siUnit.MBq); }  //Activity (of a radionuclide)
	}
}