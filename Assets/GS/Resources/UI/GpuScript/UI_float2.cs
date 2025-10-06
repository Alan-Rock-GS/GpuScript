using System;
using System.Reflection;
using UnityEngine.UIElements;
using static GpuScript.GS;

namespace GpuScript
{
  [UxmlElement]
  public partial class UI_float2 : UI_Slider_base
  {
    public override bool Init(GS gs, params GS[] gss)
    {
			if (!base.Init(gs, gss) && !isGrid) return false;
			Build(label, description, siUnit, usUnit, Unit, siFormat, usFormat, isReadOnly, isGrid, isPow2Slider, isPow10, isPow2, Nearest, NearestDigit, treeGroup_parent?.name);
      SliderV = v = val.To_float2(); rangeMin = RangeMin.To_float2(); rangeMax = RangeMax.To_float2();
      if (siUnit != siUnit.Null) { rangeMin *= convert(siUnit); rangeMax *= convert(siUnit); }
      if (headerLabel != null) headerLabel.HideIf(label.IsEmpty() || isGrid);
      return true;
    }
    public static void UXML_UI_grid_member(UI_Element e, MemberInfo m, AttGS att, uint rowI, float width)
    {
      if (att == null) return;
      UXML(e, att, $"{className}_{m.Name}_{rowI + 1}", "", "");
      e.uxml.Add($" is-grid=\"true\" style=\"width: {width}px;\" />");
    }
    public float2 Slider_Pow_Val(float2 v) => clamp(round(isPow2Slider ? (pow10(abs(v)) - 1) / 0.999f : v, GetNearest(v)), rangeMin, rangeMax);
		public float2 Slider_Log_Val(float2 v) => isPow2Slider ? log10(abs(clamp(round(v, GetNearest(v)), rangeMin, rangeMax)) * 0.999f + 1) : v;
		public float2 SliderV { get => Slider_Pow_Val(new float2(sliders[0].value, sliders[1].value)); set { var v = Slider_Log_Val(value); sliders[0].value = v.x; sliders[1].value = v.y; } }
		public override Slider[] GetSliders() => new Slider[] { this.Q<Slider>("slider_x"), this.Q<Slider>("slider_y") };
		public UI_float2() : base() { }
		//public UI_float2(AttGS att) : base(att)
		//{

		//}
		public override void OnMouseCaptureEvent(MouseCaptureEvent evt) { base.OnMouseCaptureEvent(evt); if (evt.currentTarget is TextField) { var o = evt.currentTarget as TextField; previousValue = o.value.To_float2(); } }
		public override void OnMouseCaptureOutEvent(MouseCaptureOutEvent evt) { base.OnMouseCaptureOutEvent(evt); if (evt == null || evt.currentTarget is TextField) { var o = textField; if (changed && any(previousValue != o.value.To_float2())) { OnTextFieldChanged(o); previousValue = o.value.To_float2(); changed = false; } } }
		public static Type Get_Base_Type() => typeof(float2);
		public static bool IsType(Type type) => type == typeof(float2);
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
		float2 _v0 = default;
		public float2 _si; public float2 si { get => _si; set => v = value; }
		public void Text(float2 val)
		{
			if (siUnit != siUnit.Null && usUnit != usUnit.Null)
				textField.value = (siUnits ? convert(val / siConvert) : val * convert(GetUnitConversion(siUnit), usUnit)).ToString(format);
			//else if (Unit != Unit.Null) textField.value = (val * convert(GetUnitConversion(Unit), Unit)).ToString(format);
			else textField.value = val.ToString(format);
		}

		public float2 v
		{
			get => _si;
			set
			{
				if (any(isnan(value)) || any(isinf(value))) return;
				var val = isPow10 ? round(pow10(round(log10(value)))) : isPow2 ? round(pow2(round(log2(value)))) : value;
				if (Nearest > 0) val = round(val, Nearest);
				Text(val);
				_si = val;
				if (hasRange) SliderV = val;
			}
		}
		public override string textString => v.ToString(format);
		public override object v_obj { get => v; set => v = value.To_float2(); }
		public override void OnUnitsChanged() { base.OnUnitsChanged(); if (siUnit != siUnit.Null && usUnit != usUnit.Null && textField != null) textField.value = (siUnits ? iconvert(si / siConvert) : convert(si / siConvert)).ToString(format); }

		public override bool hasRange { get => any(rangeMin < rangeMax); }
		float2 _rangeMin; public float2 rangeMin { get => _rangeMin; set { _rangeMin = value; if (sliders[0] != null) { var v = Slider_Log_Val(value); for (int i = 0; i < sliders.Length; i++) sliders[i].lowValue = v[i]; } } }
		float2 _rangeMax; public float2 rangeMax { get => _rangeMax; set { _rangeMax = value; if (sliders[0] != null) { var v = Slider_Log_Val(value); for (int i = 0; i < sliders.Length; i++) sliders[i].highValue = v[i]; } } }

		public float2 previousValue;
		float dV = 0;
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
				Text(val);
				_si = val;
				if (!GS.isGridVScroll && !GS.isGridBuilding)
					SetPropertyValue(val);
			}
		}
		public override void OnTextFieldChanged(TextField o)
		{
			float2 val = o.value.To_float2(); 
			if (siUnit != siUnit.Null && usUnit != usUnit.Null) val = siUnits ? val * convert(siUnit) : val / convert(GetUnitConversion(siUnit), usUnit);
			else if (Unit != Unit.Null) val = val / convert(GetUnitConversion(Unit), Unit);
			_si = val;
			if (!hasRange)
			{
				if (!GS.isGridVScroll && !GS.isGridBuilding) SetPropertyValue(val);
			}
			else SliderV = val;
		}

		public override bool Changed { get => any(v != _v0); set => _v0 = value ? v - 1 : v; }

		public static implicit operator float2(UI_float2 f) => f.si;

		public float2 ns { get => si * convert(Unit, Unit.ns); set => si = value / convert(Unit, Unit.ns); }
		public float2 us { get => si * convert(Unit, Unit.us); set => si = value / convert(Unit, Unit.us); }
		public float2 ms { get => si * convert(Unit, Unit.ms); set => si = value / convert(Unit, Unit.ms); }
		public float2 s { get => si * convert(Unit, Unit.s); set => si = value / convert(Unit, Unit.s); }
		public float2 min { get => si * convert(Unit, Unit.min); set => si = value / convert(Unit, Unit.min); }
		public float2 hr { get => si * convert(Unit, Unit.hr); set => si = value / convert(Unit, Unit.hr); }
		public float2 day { get => si * convert(Unit, Unit.day); set => si = value / convert(Unit, Unit.day); }
		public float2 week { get => si * convert(Unit, Unit.week); set => si = value / convert(Unit, Unit.week); }
		public float2 month { get => si * convert(Unit, Unit.month); set => si = value / convert(Unit, Unit.month); }
		public float2 year { get => si * convert(Unit, Unit.year); set => si = value / convert(Unit, Unit.year); }

		public float2 deg { get => si * convert(Unit, Unit.deg); set => si = value / convert(Unit, Unit.deg); }
		public float2 rad { get => si * convert(Unit, Unit.rad); set => si = value / convert(Unit, Unit.rad); }
		public float2 deg_per_sec { get => si * convert(Unit, Unit.deg_per_sec); set => si = value / convert(Unit, Unit.deg_per_sec); }
		public float2 rad_per_sec { get => si * convert(Unit, Unit.rad_per_sec); set => si = value / convert(Unit, Unit.rad_per_sec); }
		public float2 rpm { get => si * convert(Unit, Unit.rpm); set => si = value / convert(Unit, Unit.rpm); }
		public float2 rps { get => si * convert(Unit, Unit.rps); set => si = value / convert(Unit, Unit.rps); }

		public float2 bit { get => si * convert(Unit, Unit.bit); set => si = value / convert(Unit, Unit.bit); }
		public float2 Byte { get => si * convert(Unit, Unit.Byte); set => si = value / convert(Unit, Unit.Byte); }
		public float2 KB { get => si * convert(Unit, Unit.KB); set => si = value / convert(Unit, Unit.KB); }
		public float2 MB { get => si * convert(Unit, Unit.MB); set => si = value / convert(Unit, Unit.MB); }
		public float2 GB { get => si * convert(Unit, Unit.GB); set => si = value / convert(Unit, Unit.GB); }
		public float2 TB { get => si * convert(Unit, Unit.TB); set => si = value / convert(Unit, Unit.TB); }
		public float2 PB { get => si * convert(Unit, Unit.PB); set => si = value / convert(Unit, Unit.PB); }

		public float2 bps { get => si * convert(Unit, Unit.bps); set => si = value / convert(Unit, Unit.bps); }
		public float2 Kbps { get => si * convert(Unit, Unit.Kbps); set => si = value / convert(Unit, Unit.Kbps); }
		public float2 Mbps { get => si * convert(Unit, Unit.Mbps); set => si = value / convert(Unit, Unit.Mbps); }
		public float2 Gbps { get => si * convert(Unit, Unit.Gbps); set => si = value / convert(Unit, Unit.Gbps); }
		public float2 Tbps { get => si * convert(Unit, Unit.Tbps); set => si = value / convert(Unit, Unit.Tbps); }
		public float2 Pbps { get => si * convert(Unit, Unit.Pbps); set => si = value / convert(Unit, Unit.Pbps); }
		public float2 Bps { get => si * convert(Unit, Unit.Bps); set => si = value / convert(Unit, Unit.Bps); }
		public float2 KBps { get => si * convert(Unit, Unit.KBps); set => si = value / convert(Unit, Unit.KBps); }
		public float2 MBps { get => si * convert(Unit, Unit.MBps); set => si = value / convert(Unit, Unit.MBps); }
		public float2 GBps { get => si * convert(Unit, Unit.GBps); set => si = value / convert(Unit, Unit.GBps); }
		public float2 TBps { get => si * convert(Unit, Unit.TBps); set => si = value / convert(Unit, Unit.TBps); }
		public float2 PBps { get => si * convert(Unit, Unit.PBps); set => si = value / convert(Unit, Unit.PBps); }

		public float2 FLOPS { get => si * convert(Unit, Unit.FLOPS); set => si = value / convert(Unit, Unit.FLOPS); }
		public float2 kFLOPS { get => si * convert(Unit, Unit.kFLOPS); set => si = value / convert(Unit, Unit.kFLOPS); }
		public float2 MFLOPS { get => si * convert(Unit, Unit.MFLOPS); set => si = value / convert(Unit, Unit.MFLOPS); }
		public float2 GFLOPS { get => si * convert(Unit, Unit.GFLOPS); set => si = value / convert(Unit, Unit.GFLOPS); }
		public float2 TFLOPS { get => si * convert(Unit, Unit.TFLOPS); set => si = value / convert(Unit, Unit.TFLOPS); }
		public float2 PFLOPS { get => si * convert(Unit, Unit.PFLOPS); set => si = value / convert(Unit, Unit.PFLOPS); }
		public float2 EFLOPS { get => si * convert(Unit, Unit.EFLOPS); set => si = value / convert(Unit, Unit.EFLOPS); }
		public float2 ZFLOPS { get => si * convert(Unit, Unit.ZFLOPS); set => si = value / convert(Unit, Unit.ZFLOPS); }
		public float2 YFLOPS { get => si * convert(Unit, Unit.YFLOPS); set => si = value / convert(Unit, Unit.YFLOPS); }

		public float2 Hz { get => si * convert(Unit, Unit.Hz); set => si = value / convert(Unit, Unit.Hz); }
		public float2 kHz { get => si * convert(Unit, Unit.kHz); set => si = value / convert(Unit, Unit.kHz); }
		public float2 MHz { get => si * convert(Unit, Unit.MHz); set => si = value / convert(Unit, Unit.MHz); }
		public float2 GHz { get => si * convert(Unit, Unit.GHz); set => si = value / convert(Unit, Unit.GHz); }
		public float2 THz { get => si * convert(Unit, Unit.THz); set => si = value / convert(Unit, Unit.THz); }

		public float2 ohm { get => si * convert(Unit, Unit.ohm); set => si = value / convert(Unit, Unit.ohm); }
		public float2 mho { get => si * convert(Unit, Unit.mho); set => si = value / convert(Unit, Unit.mho); }

		public float2 mi { get => si * convert(siUnit, usUnit.mi); set => si = value / convert(siUnit, usUnit.mi); }//length nmi=nautical mile, ua=astronomic unit, ly=light-year, pc=parsec(parallax second), point=printer's point
		public float2 yd { get => si * convert(siUnit, usUnit.yd); set => si = value / convert(siUnit, usUnit.yd); }
		public float2 ft { get => si * convert(siUnit, usUnit.ft); set => si = value / convert(siUnit, usUnit.ft); }
		public float2 inch { get => si * convert(siUnit, usUnit.inch); set => si = value / convert(siUnit, usUnit.inch); }
		public float2 mil { get => si * convert(siUnit, usUnit.mil); set => si = value / convert(siUnit, usUnit.mil); }
		public float2 microinch { get => si * convert(siUnit, usUnit.microinch); set => si = value / convert(siUnit, usUnit.microinch); }
		public float2 angstrom { get => si * convert(siUnit, usUnit.angstrom); set => si = value / convert(siUnit, usUnit.angstrom); }
		public float2 point { get => si * convert(siUnit, usUnit.point); set => si = value / convert(siUnit, usUnit.point); }
		public float2 nmi { get => si * convert(siUnit, usUnit.nmi); set => si = value / convert(siUnit, usUnit.nmi); }
		public float2 fathom { get => si * convert(siUnit, usUnit.fathom); set => si = value / convert(siUnit, usUnit.fathom); }
		public float2 ua { get => si * convert(siUnit, usUnit.ua); set => si = value / convert(siUnit, usUnit.ua); }
		public float2 ly { get => si * convert(siUnit, usUnit.ly); set => si = value / convert(siUnit, usUnit.ly); }
		public float2 pc { get => si * convert(siUnit, usUnit.pc); set => si = value / convert(siUnit, usUnit.pc); }

		public float2 mi2 { get => si * convert(siUnit, usUnit.mi2); set => si = value / convert(siUnit, usUnit.mi2); }//area ha=hectare
		public float2 acre { get => si * convert(siUnit, usUnit.acre); set => si = value / convert(siUnit, usUnit.acre); }
		public float2 yd2 { get => si * convert(siUnit, usUnit.yd2); set => si = value / convert(siUnit, usUnit.yd2); }
		public float2 ft2 { get => si * convert(siUnit, usUnit.ft2); set => si = value / convert(siUnit, usUnit.ft2); }
		public float2 in2 { get => si * convert(siUnit, usUnit.in2); set => si = value / convert(siUnit, usUnit.in2); }

		public float2 in3 { get => si * convert(siUnit, usUnit.in3); set => si = value / convert(siUnit, usUnit.in3); } //Volume (bbl=oil-barrel, bu=bushel
		public float2 ft3 { get => si * convert(siUnit, usUnit.ft3); set => si = value / convert(siUnit, usUnit.ft3); }
		public float2 yd3 { get => si * convert(siUnit, usUnit.yd3); set => si = value / convert(siUnit, usUnit.yd3); }
		public float2 acre_ft { get => si * convert(siUnit, usUnit.acre_ft); set => si = value / convert(siUnit, usUnit.acre_ft); }
		public float2 bbl { get => si * convert(siUnit, usUnit.bbl); set => si = value / convert(siUnit, usUnit.bbl); }
		public float2 bu { get => si * convert(siUnit, usUnit.bu); set => si = value / convert(siUnit, usUnit.bu); }
		public float2 gal { get => si * convert(siUnit, usUnit.gal); set => si = value / convert(siUnit, usUnit.gal); }
		public float2 qt { get => si * convert(siUnit, usUnit.qt); set => si = value / convert(siUnit, usUnit.qt); }
		public float2 pt { get => si * convert(siUnit, usUnit.pt); set => si = value / convert(siUnit, usUnit.pt); }
		public float2 cup { get => si * convert(siUnit, usUnit.cup); set => si = value / convert(siUnit, usUnit.cup); }
		public float2 Tbs { get => si * convert(siUnit, usUnit.tbsp); set => si = value / convert(siUnit, usUnit.tbsp); }
		public float2 tsp { get => si * convert(siUnit, usUnit.tsp); set => si = value / convert(siUnit, usUnit.tsp); }
		public float2 fl_oz { get => si * convert(siUnit, usUnit.fl_oz); set => si = value / convert(siUnit, usUnit.fl_oz); }

		public float2 mph { get => si * convert(siUnit, usUnit.mph); set => si = value / convert(siUnit, usUnit.mph); } //speed
		public float2 ftps { get => si * convert(siUnit, usUnit.ftps); set => si = value / convert(siUnit, usUnit.ftps); }
		public float2 kn { get => si * convert(siUnit, usUnit.kn); set => si = value / convert(siUnit, usUnit.kn); }

		public float2 ftps2 { get => si * convert(siUnit, usUnit.ftps2); set => si = value / convert(siUnit, usUnit.ftps2); } //acceleration
		public float2 inps2 { get => si * convert(siUnit, usUnit.inps2); set => si = value / convert(siUnit, usUnit.inps2); }

		public float2 ft3ps { get => si * convert(siUnit, usUnit.ft3ps); set => si = value / convert(siUnit, usUnit.ft3ps); } //flow rate
		public float2 ft3pmin { get => si * convert(siUnit, usUnit.ft3pmin); set => si = value / convert(siUnit, usUnit.ft3pmin); }
		public float2 yd3pmin { get => si * convert(siUnit, usUnit.yd3pmin); set => si = value / convert(siUnit, usUnit.yd3pmin); }
		public float2 galpmin { get => si * convert(siUnit, usUnit.galpmin); set => si = value / convert(siUnit, usUnit.galpmin); }
		public float2 galpday { get => si * convert(siUnit, usUnit.galpday); set => si = value / convert(siUnit, usUnit.galpday); }

		public float2 F { get => si * convert(siUnit, usUnit.F); set => si = value / convert(siUnit, usUnit.F); } //temperature

		public float2 mpg { get => si * convert(siUnit, usUnit.mpg); set => si = value / convert(siUnit, usUnit.mpg); } //fuel efficiency

		public float2 ton_us { get => si * convert(siUnit, usUnit.ton); set => si = value / convert(siUnit, usUnit.ton); } //weight
		public float2 lb { get => si * convert(siUnit, usUnit.lb); set => si = value / convert(siUnit, usUnit.lb); }
		public float2 oz { get => si * convert(siUnit, usUnit.oz); set => si = value / convert(siUnit, usUnit.oz); }
		public float2 grain { get => si * convert(siUnit, usUnit.grain); set => si = value / convert(siUnit, usUnit.grain); }

		public float2 lb_ft { get => si * convert(siUnit, usUnit.lb_ft); set => si = value / convert(siUnit, usUnit.lb_ft); }//moment of mass
		public float2 tonpyd3 { get => si * convert(siUnit, usUnit.tonpyd3); set => si = value / convert(siUnit, usUnit.tonpyd3); } //density
		public float2 lbpft3 { get => si * convert(siUnit, usUnit.lbpft3); set => si = value / convert(siUnit, usUnit.lbpft3); }
		public float2 lbfpft3 { get => si * convert(siUnit, usUnit.lbfpft3); set => si = value / convert(siUnit, usUnit.lbfpft3); }
		public float2 lbpgal { get => si * convert(siUnit, usUnit.lbpgal); set => si = value / convert(siUnit, usUnit.lbpgal); } //concentration
		public float2 ozpgal { get => si * convert(siUnit, usUnit.ozpgal); set => si = value / convert(siUnit, usUnit.ozpgal); }
		public float2 lb_ftps { get => si * convert(siUnit, usUnit.lb_ftps); set => si = value / convert(siUnit, usUnit.lb_ftps); }//momentum
		public float2 lb_ft2 { get => si * convert(siUnit, usUnit.lb_ft2); set => si = value / convert(siUnit, usUnit.lb_ft2); }//moment of inertia
		public float2 lbf { get => si * convert(siUnit, usUnit.lbf); set => si = value / convert(siUnit, usUnit.lbf); } //force
		public float2 poundal { get => si * convert(siUnit, usUnit.poundal); set => si = value / convert(siUnit, usUnit.poundal); }
		public float2 lbf_ft { get => si * convert(siUnit, usUnit.lbf_ft); set => si = value / convert(siUnit, usUnit.lbf_ft); }  //torque
		public float2 lbf_in { get => si * convert(siUnit, usUnit.lbf_in); set => si = value / convert(siUnit, usUnit.lbf_in); }
		public float2 psf { get => si * convert(siUnit, usUnit.psf); set => si = value / convert(siUnit, usUnit.psf); }   //pressure
		public float2 torr { get => si * convert(siUnit, usUnit.torr); set => si = value / convert(siUnit, usUnit.torr); }
		public float2 psi { get => si * convert(siUnit, usUnit.psi); set => si = value / convert(siUnit, usUnit.psi); }
		public float2 atm { get => si * convert(siUnit, usUnit.atm); set => si = value / convert(siUnit, usUnit.atm); }
		public float2 bar { get => si * convert(siUnit, usUnit.bar); set => si = value / convert(siUnit, usUnit.bar); }
		public float2 mbar { get => si * convert(siUnit, usUnit.mbar); set => si = value / convert(siUnit, usUnit.mbar); }
		public float2 ksi { get => si * convert(siUnit, usUnit.ksi); set => si = value / convert(siUnit, usUnit.ksi); }

		public float2 centipoise { get => si * convert(siUnit, usUnit.centipoise); set => si = value / convert(siUnit, usUnit.centipoise); }//viscosity (dynamic)
		public float2 centistokes { get => si * convert(siUnit, usUnit.centistokes); set => si = value / convert(siUnit, usUnit.centistokes); }//Viscosity (kinematic)
		public float2 kWh { get => si * convert(siUnit, usUnit.kWh); set => si = value / convert(siUnit, usUnit.kWh); } //Energy 
		public float2 cal { get => si * convert(siUnit, usUnit.cal); set => si = value / convert(siUnit, usUnit.cal); }
		public float2 Cal { get => si * convert(siUnit, usUnit.Cal); set => si = value / convert(siUnit, usUnit.Cal); }
		public float2 BTU { get => si * convert(siUnit, usUnit.BTU); set => si = value / convert(siUnit, usUnit.BTU); }
		public float2 therm { get => si * convert(siUnit, usUnit.therm); set => si = value / convert(siUnit, usUnit.therm); }
		public float2 hph { get => si * convert(siUnit, usUnit.hph); set => si = value / convert(siUnit, usUnit.hph); }
		public float2 ft_lbf { get => si * convert(siUnit, usUnit.ft_lbf); set => si = value / convert(siUnit, usUnit.ft_lbf); }
		public float2 BTUps { get => si * convert(siUnit, usUnit.BTUps); set => si = value / convert(siUnit, usUnit.BTUps); } //Power
		public float2 BTUphr { get => si * convert(siUnit, usUnit.BTUphr); set => si = value / convert(siUnit, usUnit.BTUphr); }
		public float2 hp { get => si * convert(siUnit, usUnit.hp); set => si = value / convert(siUnit, usUnit.hp); }
		public float2 ft_lbfps { get => si * convert(siUnit, usUnit.ft_lbfps); set => si = value / convert(siUnit, usUnit.ft_lbfps); }
		public float2 BTU_inphr_ft2F { get => si * convert(siUnit, usUnit.BTU_inphr_ft2F); set => si = value / convert(siUnit, usUnit.BTU_inphr_ft2F); }//Thermal conductivity
		public float2 BTUphrft2F { get => si * convert(siUnit, usUnit.BTUphrft2F); set => si = value / convert(siUnit, usUnit.BTUphrft2F); }//Coefficient of heat transfer 
		public float2 BTUpF { get => si * convert(siUnit, usUnit.BTUpF); set => si = value / convert(siUnit, usUnit.BTUpF); }//Heat capacity
		public float2 BTUplbF { get => si * convert(siUnit, usUnit.BTUplbF); set => si = value / convert(siUnit, usUnit.BTUplbF); } //Specific heat capacity 
		public float2 oersted { get => si * convert(siUnit, usUnit.oersted); set => si = value / convert(siUnit, usUnit.oersted); }//Magnetic field strength
		public float2 maxwell { get => si * convert(siUnit, usUnit.maxwell); set => si = value / convert(siUnit, usUnit.maxwell); }//Magnetic flux
		public float2 gauss { get => si * convert(siUnit, usUnit.gauss); set => si = value / convert(siUnit, usUnit.gauss); }//Magnetic flux density
		public float2 Ah { get => si * convert(siUnit, usUnit.Ah); set => si = value / convert(siUnit, usUnit.Ah); }//Electric charge
		public float2 lambert { get => si * convert(siUnit, usUnit.lambert); set => si = value / convert(siUnit, usUnit.lambert); } //Luminance
		public float2 cdpin2 { get => si * convert(siUnit, usUnit.cdpin2); set => si = value / convert(siUnit, usUnit.cdpin2); }
		public float2 lmpft2 { get => si * convert(siUnit, usUnit.lmpft2); set => si = value / convert(siUnit, usUnit.lmpft2); } // Luminous exitance
		public float2 phot { get => si * convert(siUnit, usUnit.phot); set => si = value / convert(siUnit, usUnit.phot); }
		public float2 fc { get => si * convert(siUnit, usUnit.fc); set => si = value / convert(siUnit, usUnit.fc); }//Illuminance
		public float2 Curie { get => si * convert(siUnit, usUnit.Curie); set => si = value / convert(siUnit, usUnit.Curie); }  //Activity (of a radionuclide)

		public float2 km { get => si * convert(siUnit, siUnit.km); set => si = value / convert(siUnit, siUnit.km); }  //length
		public float2 m { get => si * convert(siUnit, siUnit.m); set => si = value / convert(siUnit, siUnit.m); }
		public float2 cm { get => si * convert(siUnit, siUnit.cm); set => si = value / convert(siUnit, siUnit.cm); }
		public float2 mm { get => si * convert(siUnit, siUnit.mm); set => si = value / convert(siUnit, siUnit.mm); }
		public float2 um { get => si * convert(siUnit, siUnit.um); set => si = value / convert(siUnit, siUnit.um); }
		public float2 nm { get => si * convert(siUnit, siUnit.nm); set => si = value / convert(siUnit, siUnit.nm); }
		public float2 pm { get => si * convert(siUnit, siUnit.pm); set => si = value / convert(siUnit, siUnit.pm); }

		public float2 km2 { get => si * convert(siUnit, siUnit.km2); set => si = value / convert(siUnit, siUnit.km2); }  //area ha=hectare
		public float2 ha { get => si * convert(siUnit, siUnit.ha); set => si = value / convert(siUnit, siUnit.ha); }
		public float2 m2 { get => si * convert(siUnit, siUnit.m2); set => si = value / convert(siUnit, siUnit.m2); }
		public float2 cm2 { get => si * convert(siUnit, siUnit.cm2); set => si = value / convert(siUnit, siUnit.cm2); }
		public float2 mm2 { get => si * convert(siUnit, siUnit.mm2); set => si = value / convert(siUnit, siUnit.mm2); }

		public float2 m3 { get => si * convert(siUnit, siUnit.m3); set => si = value / convert(siUnit, siUnit.m3); } //Volume
		public float2 cm3 { get => si * convert(siUnit, siUnit.cm3); set => si = value / convert(siUnit, siUnit.cm3); }
		public float2 mm3 { get => si * convert(siUnit, siUnit.mm3); set => si = value / convert(siUnit, siUnit.mm3); }
		public float2 l { get => si * convert(siUnit, siUnit.l); set => si = value / convert(siUnit, siUnit.l); }
		public float2 ml { get => si * convert(siUnit, siUnit.ml); set => si = value / convert(siUnit, siUnit.ml); }

		public float2 kph { get => si * convert(siUnit, siUnit.kph); set => si = value / convert(siUnit, siUnit.kph); } //speed: kn = knot
		public float2 mps { get => si * convert(siUnit, siUnit.mps); set => si = value / convert(siUnit, siUnit.mps); }
		//public float2 kn { get => si * convert(siUnit, siUnit.kn); }

		public float2 mps2 { get => si * convert(siUnit, siUnit.mps2); set => si = value / convert(siUnit, siUnit.mps2); } //acceleration
		public float2 m3ps { get => si * convert(siUnit, siUnit.m3ps); set => si = value / convert(siUnit, siUnit.m3ps); } //flow rate
		public float2 lps { get => si * convert(siUnit, siUnit.lps); set => si = value / convert(siUnit, siUnit.lps); }
		public float2 lpd { get => si * convert(siUnit, siUnit.lpd); set => si = value / convert(siUnit, siUnit.lpd); }
		public float2 C { get => si * convert(siUnit, siUnit.C); set => si = value / convert(siUnit, siUnit.C); }//temperature
		public float2 K { get => si * convert(siUnit, siUnit.K); set => si = value / convert(siUnit, siUnit.K); }

		public float2 kpl { get => si * convert(siUnit, siUnit.kmpl); set => si = value / convert(siUnit, siUnit.kmpl); }//fuel efficiency
		public float2 tonsi { get => si * convert(siUnit, siUnit.ton); set => si = value / convert(siUnit, siUnit.ton); } //weight
		public float2 kg { get => si * convert(siUnit, siUnit.kg); set => si = value / convert(siUnit, siUnit.kg); }
		public float2 g { get => si * convert(siUnit, siUnit.g); set => si = value / convert(siUnit, siUnit.g); }
		public float2 mg { get => si * convert(siUnit, siUnit.mg); set => si = value / convert(siUnit, siUnit.mg); }
		public float2 ug { get => si * convert(siUnit, siUnit.ug); set => si = value / convert(siUnit, siUnit.ug); }
		public float2 kg_m { get => si * convert(siUnit, siUnit.kg_m); set => si = value / convert(siUnit, siUnit.kg_m); }//moment of mass
		public float2 kgpm3 { get => si * convert(siUnit, siUnit.kgpm3); set => si = value / convert(siUnit, siUnit.kgpm3); } //density
		public float2 Npm3 { get => si * convert(siUnit, siUnit.Npm3); set => si = value / convert(siUnit, siUnit.Npm3); } //density
		public float2 tpm3 { get => si * convert(siUnit, siUnit.Tpm3); set => si = value / convert(siUnit, siUnit.Tpm3); }
		public float2 gpl { get => si * convert(siUnit, siUnit.gpL); set => si = value / convert(siUnit, siUnit.gpL); }//concentration
		public float2 kg_mps { get => si * convert(siUnit, siUnit.kg_mps); set => si = value / convert(siUnit, siUnit.kg_mps); }//momentum
		public float2 kg_m2 { get => si * convert(siUnit, siUnit.kg_m2); set => si = value / convert(siUnit, siUnit.kg_m2); }//moment of inertia
		public float2 N { get => si * convert(siUnit, siUnit.N); set => si = value / convert(siUnit, siUnit.N); }//force
		public float2 Nm { get => si * convert(siUnit, siUnit.Nm); set => si = value / convert(siUnit, siUnit.Nm); } //torque
		public float2 Pa { get => si * convert(siUnit, siUnit.Pa); set => si = value / convert(siUnit, siUnit.Pa); }  //pressure
		public float2 kPa { get => si * convert(siUnit, siUnit.kPa); set => si = value / convert(siUnit, siUnit.kPa); }
		public float2 MPa { get => si * convert(siUnit, siUnit.MPa); set => si = value / convert(siUnit, siUnit.MPa); }
		public float2 GPa { get => si * convert(siUnit, siUnit.GPa); set => si = value / convert(siUnit, siUnit.GPa); }
		public float2 Npm2 { get => si * convert(siUnit, siUnit.Npm2); set => si = value / convert(siUnit, siUnit.Npm2); }

		public float2 mPa_s { get => si * convert(siUnit, siUnit.mPa_s); set => si = value / convert(siUnit, siUnit.mPa_s); }//viscosity (dynamic)
		public float2 mm2ps { get => si * convert(siUnit, siUnit.mm2ps); set => si = value / convert(siUnit, siUnit.mm2ps); }//Viscosity (kinematic)
		public float2 J { get => si * convert(siUnit, siUnit.J); set => si = value / convert(siUnit, siUnit.J); }  //Energy 
		public float2 kJ { get => si * convert(siUnit, siUnit.kJ); set => si = value / convert(siUnit, siUnit.kJ); }
		public float2 MJ { get => si * convert(siUnit, siUnit.MJ); set => si = value / convert(siUnit, siUnit.MJ); }
		public float2 W { get => si * convert(siUnit, siUnit.W); set => si = value / convert(siUnit, siUnit.W); } //Power
		public float2 kW { get => si * convert(siUnit, siUnit.kW); set => si = value / convert(siUnit, siUnit.kW); }
		public float2 Wpm_K { get => si * convert(siUnit, siUnit.Wpm_K); set => si = value / convert(siUnit, siUnit.Wpm_K); }//Thermal conductivity
		public float2 Wpm2_K { get => si * convert(siUnit, siUnit.Wpm2_K); set => si = value / convert(siUnit, siUnit.Wpm2_K); }//Coefficient of heat transfer 
		public float2 kJpK { get => si * convert(siUnit, siUnit.kJpK); set => si = value / convert(siUnit, siUnit.kJpK); }//Heat capacity
		public float2 kJpkg_K { get => si * convert(siUnit, siUnit.kJpkg_K); set => si = value / convert(siUnit, siUnit.kJpkg_K); } //Specific heat capacity 
		public float2 Apm { get => si * convert(siUnit, siUnit.Apm); set => si = value / convert(siUnit, siUnit.Apm); }//Magnetic field strength
		public float2 nWb { get => si * convert(siUnit, siUnit.nWb); set => si = value / convert(siUnit, siUnit.nWb); }//Magnetic flux
		public float2 mT { get => si * convert(siUnit, siUnit.mT); set => si = value / convert(siUnit, siUnit.mT); }//Magnetic flux density
		public float2 Coulomb { get => si * convert(siUnit, siUnit.Coulomb); set => si = value / convert(siUnit, siUnit.Coulomb); }//Electric charge
		public float2 cdpm2 { get => si * convert(siUnit, siUnit.cdpm2); set => si = value / convert(siUnit, siUnit.cdpm2); }//Luminance
		public float2 lx { get => si * convert(siUnit, siUnit.lx); set => si = value / convert(siUnit, siUnit.lx); }// Luminous exitance { get => si * convert(siUnit, siUnit.mho); set => si = value / convert(siUnit, siUnit.xxx); }mph; Illuminance
		public float2 MBq { get => si * convert(siUnit, siUnit.MBq); set => si = value / convert(siUnit, siUnit.MBq); }  //Activity (of a radionuclide)
	}
}