using System;
using System.Reflection;
using UnityEngine.UIElements;
using static GpuScript.GS;

namespace GpuScript
{
	[UxmlElement]
	public partial class UI_double : UI_Slider_base
	{
		public override bool Init(GS gs, params GS[] gss)
		{
			if (!base.Init(gs, gss) && !isGrid) return false;
			Build(label, description, siUnit, usUnit, Unit, siFormat, usFormat, isReadOnly, isGrid, isPow2Slider, isPow10, isPow2, Nearest, NearestDigit, treeGroup_parent?.name);
			SliderV = v = val.To_double(); rangeMin = RangeMin.To_double(); rangeMax = RangeMax.To_double();
			if (siUnit != siUnit.Null) { rangeMin *= convert(siUnit); rangeMax *= convert(siUnit); }
			return true;
		}
		public UI_double() : base() { }
		public UI_double(int rowI, AttGS att) : base(rowI, att) { var a = att; range = double2(a.Min, a.Max); }
		public static void UXML_UI_grid_member(UI_Element e, MemberInfo m, AttGS att, uint rowI, double width)
		{
			if (att == null) return;
			UXML(e, att, $"{className}_{m.Name}_{rowI + 1}", "", "");
			e.uxml.Add($" is-grid=\"true\" style=\"width: {width}px; overflow: hidden;\" />");
		}
		public double Slider_Pow_Val(double v) => dclamp(dround(isPow2Slider ? (dpow10(dabs(v)) - 1) / 0.999 : v, GetNearest(v)), rangeMin, rangeMax);
		public double Slider_Log_Val(double v) => isPow2Slider ? dlog10(dabs(dclamp(dround(v, GetNearest(v)), rangeMin, rangeMax)) * 0.999f + 1) : v;
		public double SliderV { get => sliders[0] == null ? default : Slider_Pow_Val(sliders[0].value); set { if (sliders[0] != null) sliders[0].value = (float)Slider_Log_Val(value); } }
		public override Slider[] GetSliders() => new Slider[] { this.Q<Slider>("slider_x") };
		public override void OnMouseCaptureEvent(MouseCaptureEvent evt) { base.OnMouseCaptureEvent(evt); if (evt.currentTarget is TextField) { var o = evt.currentTarget as TextField; previousValue = o.value.To_double(); } }
		public override void OnMouseCaptureOutEvent(MouseCaptureOutEvent evt) { base.OnMouseCaptureOutEvent(evt); if (evt == null || evt.currentTarget is TextField) { var o = textField; if (changed && any(previousValue != o.value.To_double())) { OnTextFieldChanged(o); previousValue = o.value.To_double(); changed = false; } } }
		public static Type Get_Base_Type() => typeof(double);
		public static bool IsType(Type type) => type == typeof(double);
		public new static void _cs_Write(GS gs, StrBldr tData, StrBldr lateUpdate, StrBldr lateUpdate_ValuesChanged, StrBldr showIfs, StrBldr onValueChanged, AttGS attGS, string typeStr, string name)
		{
			StackFields(tData, typeStr, name);
			lateUpdate.Add($"\n    if (UI_{name}.Changed || {name} != UI_{name}.v) {name} = UI_{name}.v;");
			UI_VisualElement._cs_Write(gs, tData, lateUpdate, lateUpdate_ValuesChanged, showIfs, onValueChanged, attGS, typeStr, name);
		}
		public static string className => MethodBase.GetCurrentMethod().DeclaringType.ToString().After("GpuScript.");
		public static void UXML_UI_Element(UI_Element e) { UXML(e, e.attGS, e.memberInfo.Name, e.attGS.Name, className); e.uxml.Add($" />"); }
		public static new void UXML(UI_Element e, AttGS att, string name, string label, string typeName) { UI_Slider_base.UXML(e, att, name, label, className); }
		public double _v0 = default;
		public double _si; public double si { get => _si; set => v = value; }
		public void Text(double val)
		{
			if (siUnit != siUnit.Null && usUnit != usUnit.Null)
				textField.value = (siUnits ? dconvert(val / siConvert) : val * dconvert(GetUnitConversion(siUnit), usUnit)).ToString(format);
			else textField.value = val.ToString(format);
		}
		public double v
		{
			get => _si;
			set
			{
				if (any(disnan(value)) || any(disinf(value))) return;
				var val = isPow10 ? dround(dpow10(dround(dlog10(value)))) : isPow2 ? dround(dpow2(dround(dlog2(value)))) : value;
				if (Nearest > 0) val = dround(val, Nearest);
				Text(val);
				_si = val;
				if (hasRange) SliderV = val;
			}
		}
		public override string textString => v.ToString(format);
		public override object v_obj { get => v; set => v = value.To_double(); }
		public override void OnUnitsChanged()
		{
			base.OnUnitsChanged();
			if (siUnit != siUnit.Null && usUnit != usUnit.Null && textField != null)
				textField.value = (siUnits ? diconvert(si / siConvert) : dconvert(si / siConvert)).ToString(format);
		}
		public override bool hasRange { get => rangeMin < rangeMax; }
		double _rangeMin; public double rangeMin { get => _rangeMin; set { _rangeMin = value; if (sliders != null && sliders[0] != null) for (int i = 0; i < sliders.Length; i++) sliders[i].lowValue = (float)Slider_Log_Val(_rangeMin); } }
		double _rangeMax; public double rangeMax { get => _rangeMax; set { _rangeMax = value; if (sliders != null && sliders[0] != null) for (int i = 0; i < sliders.Length; i++) sliders[i].highValue = (float)Slider_Log_Val(_rangeMax); } }

		public double previousValue;
		public override void OnValueChanged(ChangeEvent<double> evt)
		{
			if (isBuilding) { isBuilding = false; return; }
			if (evt.currentTarget is Slider && textField != null && hasRange)
			{
				double val = SliderV;
				Text(val);
				_si = val;
				if (!GS.isGridVScroll && !GS.isGridBuilding) SetPropertyValue(val);
			}
		}
		public override void OnTextFieldChanged(TextField o)
		{
			double val = o.value.To_double();
			if (siUnit != siUnit.Null && usUnit != usUnit.Null) val = siUnits ? val * convert(siUnit) : val / convert(GetUnitConversion(siUnit), usUnit);
			else if (Unit != Unit.Null) val = val / convert(GetUnitConversion(Unit), Unit);
			_si = val;
			//SliderV = val;

			if (!hasRange)
			{
				if (!GS.isGridVScroll && !GS.isGridBuilding) SetPropertyValue(val);
			}
			else SliderV = val;
		}

		public double2 range { get => double2(rangeMin, rangeMax); set { rangeMin = value.x; rangeMax = value.y; } }
		public override bool Changed { get => any(v != _v0); set => _v0 = value ? v - 1 : v; }

		public static implicit operator double(UI_double f) => f.si;

		public double ns { get => si * convert(Unit, Unit.ns); set => si = value / convert(Unit, Unit.ns); }
		public double us { get => si * convert(Unit, Unit.us); set => si = value / convert(Unit, Unit.us); }
		public double ms { get => si * convert(Unit, Unit.ms); set => si = value / convert(Unit, Unit.ms); }
		public double s { get => si * convert(Unit, Unit.s); set => si = value / convert(Unit, Unit.s); }
		public double min { get => si * convert(Unit, Unit.min); set => si = value / convert(Unit, Unit.min); }
		public double hr { get => si * convert(Unit, Unit.hr); set => si = value / convert(Unit, Unit.hr); }
		public double day { get => si * convert(Unit, Unit.day); set => si = value / convert(Unit, Unit.day); }
		public double week { get => si * convert(Unit, Unit.week); set => si = value / convert(Unit, Unit.week); }
		public double month { get => si * convert(Unit, Unit.month); set => si = value / convert(Unit, Unit.month); }
		public double year { get => si * convert(Unit, Unit.year); set => si = value / convert(Unit, Unit.year); }

		public double deg { get => si * convert(Unit, Unit.deg); set => si = value / convert(Unit, Unit.deg); }
		public double rad { get => si * convert(Unit, Unit.rad); set => si = value / convert(Unit, Unit.rad); }
		public double deg_per_sec { get => si * convert(Unit, Unit.deg_per_sec); set => si = value / convert(Unit, Unit.deg_per_sec); }
		public double rad_per_sec { get => si * convert(Unit, Unit.rad_per_sec); set => si = value / convert(Unit, Unit.rad_per_sec); }
		public double rpm { get => si * convert(Unit, Unit.rpm); set => si = value / convert(Unit, Unit.rpm); }
		public double rps { get => si * convert(Unit, Unit.rps); set => si = value / convert(Unit, Unit.rps); }

		public double bit { get => si * convert(Unit, Unit.bit); set => si = value / convert(Unit, Unit.bit); }
		public double Byte { get => si * convert(Unit, Unit.Byte); set => si = value / convert(Unit, Unit.Byte); }
		public double KB { get => si * convert(Unit, Unit.KB); set => si = value / convert(Unit, Unit.KB); }
		public double MB { get => si * convert(Unit, Unit.MB); set => si = value / convert(Unit, Unit.MB); }
		public double GB { get => si * convert(Unit, Unit.GB); set => si = value / convert(Unit, Unit.GB); }
		public double TB { get => si * convert(Unit, Unit.TB); set => si = value / convert(Unit, Unit.TB); }
		public double PB { get => si * convert(Unit, Unit.PB); set => si = value / convert(Unit, Unit.PB); }

		public double bps { get => si * convert(Unit, Unit.bps); set => si = value / convert(Unit, Unit.bps); }
		public double Kbps { get => si * convert(Unit, Unit.Kbps); set => si = value / convert(Unit, Unit.Kbps); }
		public double Mbps { get => si * convert(Unit, Unit.Mbps); set => si = value / convert(Unit, Unit.Mbps); }
		public double Gbps { get => si * convert(Unit, Unit.Gbps); set => si = value / convert(Unit, Unit.Gbps); }
		public double Tbps { get => si * convert(Unit, Unit.Tbps); set => si = value / convert(Unit, Unit.Tbps); }
		public double Pbps { get => si * convert(Unit, Unit.Pbps); set => si = value / convert(Unit, Unit.Pbps); }
		public double Bps { get => si * convert(Unit, Unit.Bps); set => si = value / convert(Unit, Unit.Bps); }
		public double KBps { get => si * convert(Unit, Unit.KBps); set => si = value / convert(Unit, Unit.KBps); }
		public double MBps { get => si * convert(Unit, Unit.MBps); set => si = value / convert(Unit, Unit.MBps); }
		public double GBps { get => si * convert(Unit, Unit.GBps); set => si = value / convert(Unit, Unit.GBps); }
		public double TBps { get => si * convert(Unit, Unit.TBps); set => si = value / convert(Unit, Unit.TBps); }
		public double PBps { get => si * convert(Unit, Unit.PBps); set => si = value / convert(Unit, Unit.PBps); }

		public double FLOPS { get => si * convert(Unit, Unit.FLOPS); set => si = value / convert(Unit, Unit.FLOPS); }
		public double kFLOPS { get => si * convert(Unit, Unit.kFLOPS); set => si = value / convert(Unit, Unit.kFLOPS); }
		public double MFLOPS { get => si * convert(Unit, Unit.MFLOPS); set => si = value / convert(Unit, Unit.MFLOPS); }
		public double GFLOPS { get => si * convert(Unit, Unit.GFLOPS); set => si = value / convert(Unit, Unit.GFLOPS); }
		public double TFLOPS { get => si * convert(Unit, Unit.TFLOPS); set => si = value / convert(Unit, Unit.TFLOPS); }
		public double PFLOPS { get => si * convert(Unit, Unit.PFLOPS); set => si = value / convert(Unit, Unit.PFLOPS); }
		public double EFLOPS { get => si * convert(Unit, Unit.EFLOPS); set => si = value / convert(Unit, Unit.EFLOPS); }
		public double ZFLOPS { get => si * convert(Unit, Unit.ZFLOPS); set => si = value / convert(Unit, Unit.ZFLOPS); }
		public double YFLOPS { get => si * convert(Unit, Unit.YFLOPS); set => si = value / convert(Unit, Unit.YFLOPS); }

		public double Hz { get => si * convert(Unit, Unit.Hz); set => si = value / convert(Unit, Unit.Hz); }
		public double kHz { get => si * convert(Unit, Unit.kHz); set => si = value / convert(Unit, Unit.kHz); }
		public double MHz { get => si * convert(Unit, Unit.MHz); set => si = value / convert(Unit, Unit.MHz); }
		public double GHz { get => si * convert(Unit, Unit.GHz); set => si = value / convert(Unit, Unit.GHz); }
		public double THz { get => si * convert(Unit, Unit.THz); set => si = value / convert(Unit, Unit.THz); }

		public double ohm { get => si * convert(Unit, Unit.ohm); set => si = value / convert(Unit, Unit.ohm); }
		public double mho { get => si * convert(Unit, Unit.mho); set => si = value / convert(Unit, Unit.mho); }

		public double mi { get => si * convert(siUnit, usUnit.mi); set => si = value / convert(siUnit, usUnit.mi); }//length nmi=nautical mile, ua=astronomic unit, ly=light-year, pc=parsec(parallax second), point=printer's point
		public double yd { get => si * convert(siUnit, usUnit.yd); set => si = value / convert(siUnit, usUnit.yd); }
		public double ft { get => si * convert(siUnit, usUnit.ft); set => si = value / convert(siUnit, usUnit.ft); }
		public double inch { get => si * convert(siUnit, usUnit.inch); set => si = value / convert(siUnit, usUnit.inch); }
		public double mil { get => si * convert(siUnit, usUnit.mil); set => si = value / convert(siUnit, usUnit.mil); }
		public double microinch { get => si * convert(siUnit, usUnit.microinch); set => si = value / convert(siUnit, usUnit.microinch); }
		public double angstrom { get => si * convert(siUnit, usUnit.angstrom); set => si = value / convert(siUnit, usUnit.angstrom); }
		public double point { get => si * convert(siUnit, usUnit.point); set => si = value / convert(siUnit, usUnit.point); }
		public double nmi { get => si * convert(siUnit, usUnit.nmi); set => si = value / convert(siUnit, usUnit.nmi); }
		public double fathom { get => si * convert(siUnit, usUnit.fathom); set => si = value / convert(siUnit, usUnit.fathom); }
		public double ua { get => si * convert(siUnit, usUnit.ua); set => si = value / convert(siUnit, usUnit.ua); }
		public double ly { get => si * convert(siUnit, usUnit.ly); set => si = value / convert(siUnit, usUnit.ly); }
		public double pc { get => si * convert(siUnit, usUnit.pc); set => si = value / convert(siUnit, usUnit.pc); }

		public double mi2 { get => si * convert(siUnit, usUnit.mi2); set => si = value / convert(siUnit, usUnit.mi2); }//area ha=hectare
		public double acre { get => si * convert(siUnit, usUnit.acre); set => si = value / convert(siUnit, usUnit.acre); }
		public double yd2 { get => si * convert(siUnit, usUnit.yd2); set => si = value / convert(siUnit, usUnit.yd2); }
		public double ft2 { get => si * convert(siUnit, usUnit.ft2); set => si = value / convert(siUnit, usUnit.ft2); }
		public double in2 { get => si * convert(siUnit, usUnit.in2); set => si = value / convert(siUnit, usUnit.in2); }

		public double in3 { get => si * convert(siUnit, usUnit.in3); set => si = value / convert(siUnit, usUnit.in3); } //Volume (bbl=oil-barrel, bu=bushel
		public double ft3 { get => si * convert(siUnit, usUnit.ft3); set => si = value / convert(siUnit, usUnit.ft3); }
		public double yd3 { get => si * convert(siUnit, usUnit.yd3); set => si = value / convert(siUnit, usUnit.yd3); }
		public double acre_ft { get => si * convert(siUnit, usUnit.acre_ft); set => si = value / convert(siUnit, usUnit.acre_ft); }
		public double bbl { get => si * convert(siUnit, usUnit.bbl); set => si = value / convert(siUnit, usUnit.bbl); }
		public double bu { get => si * convert(siUnit, usUnit.bu); set => si = value / convert(siUnit, usUnit.bu); }
		public double gal { get => si * convert(siUnit, usUnit.gal); set => si = value / convert(siUnit, usUnit.gal); }
		public double qt { get => si * convert(siUnit, usUnit.qt); set => si = value / convert(siUnit, usUnit.qt); }
		public double pt { get => si * convert(siUnit, usUnit.pt); set => si = value / convert(siUnit, usUnit.pt); }
		public double cup { get => si * convert(siUnit, usUnit.cup); set => si = value / convert(siUnit, usUnit.cup); }
		public double Tbs { get => si * convert(siUnit, usUnit.tbsp); set => si = value / convert(siUnit, usUnit.tbsp); }
		public double tsp { get => si * convert(siUnit, usUnit.tsp); set => si = value / convert(siUnit, usUnit.tsp); }
		public double fl_oz { get => si * convert(siUnit, usUnit.fl_oz); set => si = value / convert(siUnit, usUnit.fl_oz); }

		public double mph { get => si * convert(siUnit, usUnit.mph); set => si = value / convert(siUnit, usUnit.mph); } //speed
		public double ftps { get => si * convert(siUnit, usUnit.ftps); set => si = value / convert(siUnit, usUnit.ftps); }
		public double kn { get => si * convert(siUnit, usUnit.kn); set => si = value / convert(siUnit, usUnit.kn); }

		public double ftps2 { get => si * convert(siUnit, usUnit.ftps2); set => si = value / convert(siUnit, usUnit.ftps2); } //acceleration
		public double inps2 { get => si * convert(siUnit, usUnit.inps2); set => si = value / convert(siUnit, usUnit.inps2); }

		public double ft3ps { get => si * convert(siUnit, usUnit.ft3ps); set => si = value / convert(siUnit, usUnit.ft3ps); } //flow rate
		public double ft3pmin { get => si * convert(siUnit, usUnit.ft3pmin); set => si = value / convert(siUnit, usUnit.ft3pmin); }
		public double yd3pmin { get => si * convert(siUnit, usUnit.yd3pmin); set => si = value / convert(siUnit, usUnit.yd3pmin); }
		public double galpmin { get => si * convert(siUnit, usUnit.galpmin); set => si = value / convert(siUnit, usUnit.galpmin); }
		public double galpday { get => si * convert(siUnit, usUnit.galpday); set => si = value / convert(siUnit, usUnit.galpday); }

		public double F { get => si * convert(siUnit, usUnit.F); set => si = value / convert(siUnit, usUnit.F); } //temperature

		public double mpg { get => si * convert(siUnit, usUnit.mpg); set => si = value / convert(siUnit, usUnit.mpg); } //fuel efficiency

		public double ton_us { get => si * convert(siUnit, usUnit.ton); set => si = value / convert(siUnit, usUnit.ton); } //weight
		public double lb { get => si * convert(siUnit, usUnit.lb); set => si = value / convert(siUnit, usUnit.lb); }
		public double oz { get => si * convert(siUnit, usUnit.oz); set => si = value / convert(siUnit, usUnit.oz); }
		public double grain { get => si * convert(siUnit, usUnit.grain); set => si = value / convert(siUnit, usUnit.grain); }

		public double lb_ft { get => si * convert(siUnit, usUnit.lb_ft); set => si = value / convert(siUnit, usUnit.lb_ft); }//moment of mass
		public double tonpyd3 { get => si * convert(siUnit, usUnit.tonpyd3); set => si = value / convert(siUnit, usUnit.tonpyd3); } //density
		public double lbpft3 { get => si * convert(siUnit, usUnit.lbpft3); set => si = value / convert(siUnit, usUnit.lbpft3); }
		public double lbfpft3 { get => si * convert(siUnit, usUnit.lbfpft3); set => si = value / convert(siUnit, usUnit.lbfpft3); }
		public double lbpgal { get => si * convert(siUnit, usUnit.lbpgal); set => si = value / convert(siUnit, usUnit.lbpgal); } //concentration
		public double ozpgal { get => si * convert(siUnit, usUnit.ozpgal); set => si = value / convert(siUnit, usUnit.ozpgal); }
		public double lb_ftps { get => si * convert(siUnit, usUnit.lb_ftps); set => si = value / convert(siUnit, usUnit.lb_ftps); }//momentum
		public double lb_ft2 { get => si * convert(siUnit, usUnit.lb_ft2); set => si = value / convert(siUnit, usUnit.lb_ft2); }//moment of inertia
		public double lbf { get => si * convert(siUnit, usUnit.lbf); set => si = value / convert(siUnit, usUnit.lbf); } //force
		public double poundal { get => si * convert(siUnit, usUnit.poundal); set => si = value / convert(siUnit, usUnit.poundal); }
		public double lbf_ft { get => si * convert(siUnit, usUnit.lbf_ft); set => si = value / convert(siUnit, usUnit.lbf_ft); }  //torque
		public double lbf_in { get => si * convert(siUnit, usUnit.lbf_in); set => si = value / convert(siUnit, usUnit.lbf_in); }
		public double psf { get => si * convert(siUnit, usUnit.psf); set => si = value / convert(siUnit, usUnit.psf); }   //pressure
		public double torr { get => si * convert(siUnit, usUnit.torr); set => si = value / convert(siUnit, usUnit.torr); }
		public double psi { get => si * convert(siUnit, usUnit.psi); set => si = value / convert(siUnit, usUnit.psi); }
		public double atm { get => si * convert(siUnit, usUnit.atm); set => si = value / convert(siUnit, usUnit.atm); }
		public double bar { get => si * convert(siUnit, usUnit.bar); set => si = value / convert(siUnit, usUnit.bar); }
		public double mbar { get => si * convert(siUnit, usUnit.mbar); set => si = value / convert(siUnit, usUnit.mbar); }
		public double ksi { get => si * convert(siUnit, usUnit.ksi); set => si = value / convert(siUnit, usUnit.ksi); }

		public double centipoise { get => si * convert(siUnit, usUnit.centipoise); set => si = value / convert(siUnit, usUnit.centipoise); }//viscosity (dynamic)
		public double centistokes { get => si * convert(siUnit, usUnit.centistokes); set => si = value / convert(siUnit, usUnit.centistokes); }//Viscosity (kinematic)
		public double kWh { get => si * convert(siUnit, usUnit.kWh); set => si = value / convert(siUnit, usUnit.kWh); } //Energy 
		public double cal { get => si * convert(siUnit, usUnit.cal); set => si = value / convert(siUnit, usUnit.cal); }
		public double Cal { get => si * convert(siUnit, usUnit.Cal); set => si = value / convert(siUnit, usUnit.Cal); }
		public double BTU { get => si * convert(siUnit, usUnit.BTU); set => si = value / convert(siUnit, usUnit.BTU); }
		public double therm { get => si * convert(siUnit, usUnit.therm); set => si = value / convert(siUnit, usUnit.therm); }
		public double hph { get => si * convert(siUnit, usUnit.hph); set => si = value / convert(siUnit, usUnit.hph); }
		public double ft_lbf { get => si * convert(siUnit, usUnit.ft_lbf); set => si = value / convert(siUnit, usUnit.ft_lbf); }
		public double BTUps { get => si * convert(siUnit, usUnit.BTUps); set => si = value / convert(siUnit, usUnit.BTUps); } //Power
		public double BTUphr { get => si * convert(siUnit, usUnit.BTUphr); set => si = value / convert(siUnit, usUnit.BTUphr); }
		public double hp { get => si * convert(siUnit, usUnit.hp); set => si = value / convert(siUnit, usUnit.hp); }
		public double ft_lbfps { get => si * convert(siUnit, usUnit.ft_lbfps); set => si = value / convert(siUnit, usUnit.ft_lbfps); }
		public double BTU_inphr_ft2F { get => si * convert(siUnit, usUnit.BTU_inphr_ft2F); set => si = value / convert(siUnit, usUnit.BTU_inphr_ft2F); }//Thermal conductivity
		public double BTUphrft2F { get => si * convert(siUnit, usUnit.BTUphrft2F); set => si = value / convert(siUnit, usUnit.BTUphrft2F); }//Coefficient of heat transfer 
		public double BTUpF { get => si * convert(siUnit, usUnit.BTUpF); set => si = value / convert(siUnit, usUnit.BTUpF); }//Heat capacity
		public double BTUplbF { get => si * convert(siUnit, usUnit.BTUplbF); set => si = value / convert(siUnit, usUnit.BTUplbF); } //Specific heat capacity 
		public double oersted { get => si * convert(siUnit, usUnit.oersted); set => si = value / convert(siUnit, usUnit.oersted); }//Magnetic field strength
		public double maxwell { get => si * convert(siUnit, usUnit.maxwell); set => si = value / convert(siUnit, usUnit.maxwell); }//Magnetic flux
		public double gauss { get => si * convert(siUnit, usUnit.gauss); set => si = value / convert(siUnit, usUnit.gauss); }//Magnetic flux density
		public double Ah { get => si * convert(siUnit, usUnit.Ah); set => si = value / convert(siUnit, usUnit.Ah); }//Electric charge
		public double lambert { get => si * convert(siUnit, usUnit.lambert); set => si = value / convert(siUnit, usUnit.lambert); } //Luminance
		public double cdpin2 { get => si * convert(siUnit, usUnit.cdpin2); set => si = value / convert(siUnit, usUnit.cdpin2); }
		public double lmpft2 { get => si * convert(siUnit, usUnit.lmpft2); set => si = value / convert(siUnit, usUnit.lmpft2); } // Luminous exitance
		public double phot { get => si * convert(siUnit, usUnit.phot); set => si = value / convert(siUnit, usUnit.phot); }
		public double fc { get => si * convert(siUnit, usUnit.fc); set => si = value / convert(siUnit, usUnit.fc); }//Illuminance
		public double Curie { get => si * convert(siUnit, usUnit.Curie); set => si = value / convert(siUnit, usUnit.Curie); }  //Activity (of a radionuclide)

		public double km { get => si * convert(siUnit, siUnit.km); set => si = value / convert(siUnit, siUnit.km); }  //length
		public double m { get => si * convert(siUnit, siUnit.m); set => si = value / convert(siUnit, siUnit.m); }
		public double cm { get => si * convert(siUnit, siUnit.cm); set => si = value / convert(siUnit, siUnit.cm); }
		public double mm { get => si * convert(siUnit, siUnit.mm); set => si = value / convert(siUnit, siUnit.mm); }
		public double um { get => si * convert(siUnit, siUnit.um); set => si = value / convert(siUnit, siUnit.um); }
		public double nm { get => si * convert(siUnit, siUnit.nm); set => si = value / convert(siUnit, siUnit.nm); }
		public double pm { get => si * convert(siUnit, siUnit.pm); set => si = value / convert(siUnit, siUnit.pm); }

		public double km2 { get => si * convert(siUnit, siUnit.km2); set => si = value / convert(siUnit, siUnit.km2); }  //area ha=hectare
		public double ha { get => si * convert(siUnit, siUnit.ha); set => si = value / convert(siUnit, siUnit.ha); }
		public double m2 { get => si * convert(siUnit, siUnit.m2); set => si = value / convert(siUnit, siUnit.m2); }
		public double cm2 { get => si * convert(siUnit, siUnit.cm2); set => si = value / convert(siUnit, siUnit.cm2); }
		public double mm2 { get => si * convert(siUnit, siUnit.mm2); set => si = value / convert(siUnit, siUnit.mm2); }

		public double m3 { get => si * convert(siUnit, siUnit.m3); set => si = value / convert(siUnit, siUnit.m3); } //Volume
		public double cm3 { get => si * convert(siUnit, siUnit.cm3); set => si = value / convert(siUnit, siUnit.cm3); }
		public double mm3 { get => si * convert(siUnit, siUnit.mm3); set => si = value / convert(siUnit, siUnit.mm3); }
		public double l { get => si * convert(siUnit, siUnit.l); set => si = value / convert(siUnit, siUnit.l); }
		public double ml { get => si * convert(siUnit, siUnit.ml); set => si = value / convert(siUnit, siUnit.ml); }

		public double kph { get => si * convert(siUnit, siUnit.kph); set => si = value / convert(siUnit, siUnit.kph); } //speed: kn = knot
		public double mps { get => si * convert(siUnit, siUnit.mps); set => si = value / convert(siUnit, siUnit.mps); }
		//public double kn { get => si * convert(siUnit, siUnit.kn); }

		public double mps2 { get => si * convert(siUnit, siUnit.mps2); set => si = value / convert(siUnit, siUnit.mps2); } //acceleration
		public double m3ps { get => si * convert(siUnit, siUnit.m3ps); set => si = value / convert(siUnit, siUnit.m3ps); } //flow rate
		public double lps { get => si * convert(siUnit, siUnit.lps); set => si = value / convert(siUnit, siUnit.lps); }
		public double lpd { get => si * convert(siUnit, siUnit.lpd); set => si = value / convert(siUnit, siUnit.lpd); }
		public double C { get => si * convert(siUnit, siUnit.C); set => si = value / convert(siUnit, siUnit.C); }//temperature
		public double K { get => si * convert(siUnit, siUnit.K); set => si = value / convert(siUnit, siUnit.K); }

		public double kpl { get => si * convert(siUnit, siUnit.kmpl); set => si = value / convert(siUnit, siUnit.kmpl); }//fuel efficiency
		public double tonsi { get => si * convert(siUnit, siUnit.ton); set => si = value / convert(siUnit, siUnit.ton); } //weight
		public double kg { get => si * convert(siUnit, siUnit.kg); set => si = value / convert(siUnit, siUnit.kg); }
		public double g { get => si * convert(siUnit, siUnit.g); set => si = value / convert(siUnit, siUnit.g); }
		public double mg { get => si * convert(siUnit, siUnit.mg); set => si = value / convert(siUnit, siUnit.mg); }
		public double ug { get => si * convert(siUnit, siUnit.ug); set => si = value / convert(siUnit, siUnit.ug); }
		public double kg_m { get => si * convert(siUnit, siUnit.kg_m); set => si = value / convert(siUnit, siUnit.kg_m); }//moment of mass
		public double kgpm3 { get => si * convert(siUnit, siUnit.kgpm3); set => si = value / convert(siUnit, siUnit.kgpm3); } //density
		public double Npm3 { get => si * convert(siUnit, siUnit.Npm3); set => si = value / convert(siUnit, siUnit.Npm3); } //density
		public double tpm3 { get => si * convert(siUnit, siUnit.Tpm3); set => si = value / convert(siUnit, siUnit.Tpm3); }
		public double gpl { get => si * convert(siUnit, siUnit.gpL); set => si = value / convert(siUnit, siUnit.gpL); }//concentration
		public double kg_mps { get => si * convert(siUnit, siUnit.kg_mps); set => si = value / convert(siUnit, siUnit.kg_mps); }//momentum
		public double kg_m2 { get => si * convert(siUnit, siUnit.kg_m2); set => si = value / convert(siUnit, siUnit.kg_m2); }//moment of inertia
		public double N { get => si * convert(siUnit, siUnit.N); set => si = value / convert(siUnit, siUnit.N); }//force
		public double Nm { get => si * convert(siUnit, siUnit.Nm); set => si = value / convert(siUnit, siUnit.Nm); } //torque
		public double Pa { get => si * convert(siUnit, siUnit.Pa); set => si = value / convert(siUnit, siUnit.Pa); }  //pressure
		public double kPa { get => si * convert(siUnit, siUnit.kPa); set => si = value / convert(siUnit, siUnit.kPa); }
		public double MPa { get => si * convert(siUnit, siUnit.MPa); set => si = value / convert(siUnit, siUnit.MPa); }
		public double GPa { get => si * convert(siUnit, siUnit.GPa); set => si = value / convert(siUnit, siUnit.GPa); }
		public double Npm2 { get => si * convert(siUnit, siUnit.Npm2); set => si = value / convert(siUnit, siUnit.Npm2); }

		public double mPa_s { get => si * convert(siUnit, siUnit.mPa_s); set => si = value / convert(siUnit, siUnit.mPa_s); }//viscosity (dynamic)
		public double mm2ps { get => si * convert(siUnit, siUnit.mm2ps); set => si = value / convert(siUnit, siUnit.mm2ps); }//Viscosity (kinematic)
		public double J { get => si * convert(siUnit, siUnit.J); set => si = value / convert(siUnit, siUnit.J); }  //Energy 
		public double kJ { get => si * convert(siUnit, siUnit.kJ); set => si = value / convert(siUnit, siUnit.kJ); }
		public double MJ { get => si * convert(siUnit, siUnit.MJ); set => si = value / convert(siUnit, siUnit.MJ); }
		public double W { get => si * convert(siUnit, siUnit.W); set => si = value / convert(siUnit, siUnit.W); } //Power
		public double kW { get => si * convert(siUnit, siUnit.kW); set => si = value / convert(siUnit, siUnit.kW); }
		public double Wpm_K { get => si * convert(siUnit, siUnit.Wpm_K); set => si = value / convert(siUnit, siUnit.Wpm_K); }//Thermal conductivity
		public double Wpm2_K { get => si * convert(siUnit, siUnit.Wpm2_K); set => si = value / convert(siUnit, siUnit.Wpm2_K); }//Coefficient of heat transfer 
		public double kJpK { get => si * convert(siUnit, siUnit.kJpK); set => si = value / convert(siUnit, siUnit.kJpK); }//Heat capacity
		public double kJpkg_K { get => si * convert(siUnit, siUnit.kJpkg_K); set => si = value / convert(siUnit, siUnit.kJpkg_K); } //Specific heat capacity 
		public double Apm { get => si * convert(siUnit, siUnit.Apm); set => si = value / convert(siUnit, siUnit.Apm); }//Magnetic field strength
		public double nWb { get => si * convert(siUnit, siUnit.nWb); set => si = value / convert(siUnit, siUnit.nWb); }//Magnetic flux
		public double mT { get => si * convert(siUnit, siUnit.mT); set => si = value / convert(siUnit, siUnit.mT); }//Magnetic flux density
		public double Coulomb { get => si * convert(siUnit, siUnit.Coulomb); set => si = value / convert(siUnit, siUnit.Coulomb); }//Electric charge
		public double cdpm2 { get => si * convert(siUnit, siUnit.cdpm2); set => si = value / convert(siUnit, siUnit.cdpm2); }//Luminance
		public double lx { get => si * convert(siUnit, siUnit.lx); set => si = value / convert(siUnit, siUnit.lx); }// Luminous exitance { get => si * convert(siUnit, siUnit.mho); set => si = value / convert(siUnit, siUnit.xxx); }mph; Illuminance
		public double MBq { get => si * convert(siUnit, siUnit.MBq); set => si = value / convert(siUnit, siUnit.MBq); }  //Activity (of a radionuclide)
	}
}