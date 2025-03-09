using System;
using System.Reflection;
using UnityEngine.UIElements;
using static GpuScript.GS;

namespace GpuScript
{
  public class UI_float4 : UI_Slider_base
  {
    public float4 Slider_Pow_Val(float4 v) => clamp(round(is_Pow2_Slider ? (pow10(abs(v)) - 1) / 0.999f : v, Nearest), range_Min, range_Max);
    public float4 Slider_Log_Val(float4 v) => is_Pow2_Slider ? log10(abs(clamp(round(v, Nearest), range_Min, range_Max)) * 0.999f + 1) : v;
    public float4 SliderV { get => Slider_Pow_Val(new float4(sliders[0].value, sliders[1].value, sliders[2].value, sliders[3].value)); set { var v = Slider_Log_Val(value); sliders[0].value = v.x; sliders[1].value = v.y; sliders[2].value = v.z; sliders[3].value = v.w; } }
    public override Slider[] GetSliders() => new Slider[] { this.Q<Slider>("slider_x"), this.Q<Slider>("slider_y"), this.Q<Slider>("slider_z"), this.Q<Slider>("slider_w") }; 
    public UI_float4() : base() { }
    public override void OnMouseCaptureEvent(MouseCaptureEvent evt) { base.OnMouseCaptureEvent(evt); if (evt.currentTarget is TextField) { var o = evt.currentTarget as TextField; previousValue = o.value.To_float4(); } }
    public override void OnMouseCaptureOutEvent(MouseCaptureOutEvent evt) { base.OnMouseCaptureOutEvent(evt); if (evt == null || evt.currentTarget is TextField) { var o = textField; if (changed && any(previousValue != o.value.To_float4())) { OnTextFieldChanged(o); previousValue = o.value.To_float4(); changed = false; } } }
    public static Type Get_Base_Type() => typeof(float4); 
    public static bool IsType(Type type) => type == typeof(float4); 
    public override bool Init(GS gs, params GS[] gss) { if (!base.Init(gs, gss)) return false; v = textField.value.To_float4(); return true; }
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
    float4 _v0 = default;
    public float4 _si; public float4 si { get => _si; set => v = value; }
		//public void Text(float4 val)
		//{
		//	if (siUnit != siUnit.Null && usUnit != usUnit.Null) textField.value = (siUnits ? val : convert(val)).ToString(format);
		//	else textField.value = val.ToString(format);
		//}
		public void Text(float4 val)
		{
			if (siUnit != siUnit.Null && usUnit != usUnit.Null)
				textField.value = (siUnits ? convert(val / siConvert) : val * convert(GetUnitConversion(siUnit), usUnit)).ToString(format);
			else textField.value = val.ToString(format);
		}
		public float4 v
		{
			get => _si;
			set
			{
				if (any(isnan(value)) || any(isinf(value))) return;
				var val = is_Pow10 ? round(pow10(round(log10(value)))) : is_Pow2 ? round(pow2(round(log2(value)))) : value;
				if (Nearest > 0) val = round(val, Nearest);
				Text(val);
				_si = val;
				if (hasRange) SliderV = val;
			}
		}
		public override string textString => v.ToString(format);
    public override object v_obj { get => v; set => v = value.To_float4(); }
		//public override void OnUnitsChanged() { base.OnUnitsChanged(); if (siUnit != siUnit.Null && usUnit != usUnit.Null && textField != null) textField.value = (siUnits ? iconvert(si) : convert(si)).ToString(format); }
		public override void OnUnitsChanged() { base.OnUnitsChanged(); if (siUnit != siUnit.Null && usUnit != usUnit.Null && textField != null) textField.value = (siUnits ? iconvert(si / siConvert) : convert(si / siConvert)).ToString(format); }

		public override bool hasRange { get => any(range_Min < range_Max); }
    float4 _range_Min; public float4 range_Min { get => _range_Min; set { _range_Min = value; if (sliders[0] != null) { var v = Slider_Log_Val(value); for (int i = 0; i < sliders.Length; i++) sliders[i].lowValue = v[i]; } } }
    float4 _range_Max; public float4 range_Max { get => _range_Max; set { _range_Max = value; if (sliders[0] != null) { var v = Slider_Log_Val(value); for (int i = 0; i < sliders.Length; i++) sliders[i].highValue = v[i]; } } }

    public float4 previousValue;
		public override void OnValueChanged(ChangeEvent<float> evt) { if (evt.currentTarget is Slider && textField != null) { float4 val = SliderV; Text(val); SetPropertyValue(val); } }
		//public override void OnTextFieldChanged(TextField o) { float4 val = o.value.To_float4(); if (siUnit != siUnit.Null && usUnit != usUnit.Null && !siUnits) val = iconvert(val); SetPropertyValue(SliderV = val); }
		public override void OnTextFieldChanged(TextField o)
		{
			float4 val = o.value.To_float4();
			if (siUnit != siUnit.Null && usUnit != usUnit.Null) val = siUnits ? val * convert(siUnit) : val / convert(GetUnitConversion(siUnit), usUnit);
			SetPropertyValue(SliderV = val);
			if (siUnit != siUnit.Null) { range_Min *= convert(siUnit); range_Max *= convert(siUnit); }
		}
		public void Build(string title, string description, string val, string rangeMin, string rangeMax, string _siUnit, string _usUnit, string _Unit,
      string siFormat, string usFormat, bool isReadOnly, bool isGrid, bool isPow2Slider, bool isPow10, bool isPow2, float nearest, string treeGroup_parent)
    {
      base.Build(title, description, _siUnit, _usUnit, _Unit, siFormat, usFormat, isReadOnly, isGrid, isPow2Slider, isPow10, isPow2, nearest, treeGroup_parent);
      range_Min = rangeMin.To_float4(); range_Max = rangeMax.To_float4(); SliderV = val.To_float4();
    }

    public new class UxmlFactory : UxmlFactory<UI_float4, UxmlTraits> { }
    public new class UxmlTraits : UI_VisualElement.UxmlTraits
    {
      UxmlStringAttributeDescription m_float4_value = new UxmlStringAttributeDescription { name = "UI_float4_value" };
      UxmlStringAttributeDescription m_float4_min = new UxmlStringAttributeDescription { name = "UI_float4_min" };
      UxmlStringAttributeDescription m_float4_max = new UxmlStringAttributeDescription { name = "UI_float4_max" };
      UxmlStringAttributeDescription m_float4_siFormat = new UxmlStringAttributeDescription { name = "UI_float4_siFormat", defaultValue = "0.0000" };
      UxmlStringAttributeDescription m_float4_usFormat = new UxmlStringAttributeDescription { name = "UI_float4_usFormat", defaultValue = "0.0000" };
      UxmlStringAttributeDescription m_float4_siUnit = new UxmlStringAttributeDescription { name = "UI_float4_siUnit", defaultValue = "" };
      UxmlStringAttributeDescription m_float4_usUnit = new UxmlStringAttributeDescription { name = "UI_float4_usUnit", defaultValue = "" };
      UxmlStringAttributeDescription m_float4_Unit = new UxmlStringAttributeDescription { name = "UI_float4_Unit", defaultValue = "" };

      public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
      {
        base.Init(ve, bag, cc);
        ((UI_float4)ve).Build(m_Label.GetValueFromBag(bag, cc), m_Description.GetValueFromBag(bag, cc),
          m_float4_value.GetValueFromBag(bag, cc), m_float4_min.GetValueFromBag(bag, cc), m_float4_max.GetValueFromBag(bag, cc),
          m_float4_siUnit.GetValueFromBag(bag, cc), m_float4_usUnit.GetValueFromBag(bag, cc), m_float4_Unit.GetValueFromBag(bag, cc),
          m_float4_siFormat.GetValueFromBag(bag, cc), m_float4_usFormat.GetValueFromBag(bag, cc),
          m_isReadOnly.GetValueFromBag(bag, cc), m_isGrid.GetValueFromBag(bag, cc), m_isPow2Slider.GetValueFromBag(bag, cc),
          m_isPow10.GetValueFromBag(bag, cc), m_isPow2.GetValueFromBag(bag, cc), m_Nearest.GetValueFromBag(bag, cc),
          m_TreeGroup_Parent.GetValueFromBag(bag, cc));
      }
    }

    //public float4 siRange, usRange;
    public override bool Changed { get => any(v != _v0); set => _v0 = value ? v - 1 : v; }

    public static implicit operator float4(UI_float4 f) => f.si; 

    public float4 ns { get => si * convert(Unit, Unit.ns); set => si = value / convert(Unit, Unit.ns); }
    public float4 us { get => si * convert(Unit, Unit.us); set => si = value / convert(Unit, Unit.us); }
    public float4 ms { get => si * convert(Unit, Unit.ms); set => si = value / convert(Unit, Unit.ms); }
    public float4 s { get => si * convert(Unit, Unit.s); set => si = value / convert(Unit, Unit.s); }
    public float4 min { get => si * convert(Unit, Unit.min); set => si = value / convert(Unit, Unit.min); }
    public float4 hr { get => si * convert(Unit, Unit.hr); set => si = value / convert(Unit, Unit.hr); }
    public float4 day { get => si * convert(Unit, Unit.day); set => si = value / convert(Unit, Unit.day); }
    public float4 week { get => si * convert(Unit, Unit.week); set => si = value / convert(Unit, Unit.week); }
    public float4 month { get => si * convert(Unit, Unit.month); set => si = value / convert(Unit, Unit.month); }
    public float4 year { get => si * convert(Unit, Unit.year); set => si = value / convert(Unit, Unit.year); }

    public float4 deg { get => si * convert(Unit, Unit.deg); set => si = value / convert(Unit, Unit.deg); }
    public float4 rad { get => si * convert(Unit, Unit.rad); set => si = value / convert(Unit, Unit.rad); }

    public float4 bit { get => si * convert(Unit, Unit.bit); set => si = value / convert(Unit, Unit.bit); }
    public float4 Byte { get => si * convert(Unit, Unit.Byte); set => si = value / convert(Unit, Unit.Byte); }
    public float4 KB { get => si * convert(Unit, Unit.KB); set => si = value / convert(Unit, Unit.KB); }
    public float4 MB { get => si * convert(Unit, Unit.MB); set => si = value / convert(Unit, Unit.MB); }
    public float4 GB { get => si * convert(Unit, Unit.GB); set => si = value / convert(Unit, Unit.GB); }
    public float4 TB { get => si * convert(Unit, Unit.TB); set => si = value / convert(Unit, Unit.TB); }
    public float4 PB { get => si * convert(Unit, Unit.PB); set => si = value / convert(Unit, Unit.PB); }

    public float4 bps { get => si * convert(Unit, Unit.bps); set => si = value / convert(Unit, Unit.bps); }
    public float4 Kbps { get => si * convert(Unit, Unit.Kbps); set => si = value / convert(Unit, Unit.Kbps); }
    public float4 Mbps { get => si * convert(Unit, Unit.Mbps); set => si = value / convert(Unit, Unit.Mbps); }
    public float4 Gbps { get => si * convert(Unit, Unit.Gbps); set => si = value / convert(Unit, Unit.Gbps); }
    public float4 Tbps { get => si * convert(Unit, Unit.Tbps); set => si = value / convert(Unit, Unit.Tbps); }
    public float4 Pbps { get => si * convert(Unit, Unit.Pbps); set => si = value / convert(Unit, Unit.Pbps); }
    public float4 Bps { get => si * convert(Unit, Unit.Bps); set => si = value / convert(Unit, Unit.Bps); }
    public float4 KBps { get => si * convert(Unit, Unit.KBps); set => si = value / convert(Unit, Unit.KBps); }
    public float4 MBps { get => si * convert(Unit, Unit.MBps); set => si = value / convert(Unit, Unit.MBps); }
    public float4 GBps { get => si * convert(Unit, Unit.GBps); set => si = value / convert(Unit, Unit.GBps); }
    public float4 TBps { get => si * convert(Unit, Unit.TBps); set => si = value / convert(Unit, Unit.TBps); }
    public float4 PBps { get => si * convert(Unit, Unit.PBps); set => si = value / convert(Unit, Unit.PBps); }

    public float4 FLOPS { get => si * convert(Unit, Unit.FLOPS); set => si = value / convert(Unit, Unit.FLOPS); }
    public float4 kFLOPS { get => si * convert(Unit, Unit.kFLOPS); set => si = value / convert(Unit, Unit.kFLOPS); }
    public float4 MFLOPS { get => si * convert(Unit, Unit.MFLOPS); set => si = value / convert(Unit, Unit.MFLOPS); }
    public float4 GFLOPS { get => si * convert(Unit, Unit.GFLOPS); set => si = value / convert(Unit, Unit.GFLOPS); }
    public float4 TFLOPS { get => si * convert(Unit, Unit.TFLOPS); set => si = value / convert(Unit, Unit.TFLOPS); }
    public float4 PFLOPS { get => si * convert(Unit, Unit.PFLOPS); set => si = value / convert(Unit, Unit.PFLOPS); }
    public float4 EFLOPS { get => si * convert(Unit, Unit.EFLOPS); set => si = value / convert(Unit, Unit.EFLOPS); }
    public float4 ZFLOPS { get => si * convert(Unit, Unit.ZFLOPS); set => si = value / convert(Unit, Unit.ZFLOPS); }
    public float4 YFLOPS { get => si * convert(Unit, Unit.YFLOPS); set => si = value / convert(Unit, Unit.YFLOPS); }

    public float4 Hz { get => si * convert(Unit, Unit.Hz); set => si = value / convert(Unit, Unit.Hz); }
    public float4 kHz { get => si * convert(Unit, Unit.kHz); set => si = value / convert(Unit, Unit.kHz); }
    public float4 MHz { get => si * convert(Unit, Unit.MHz); set => si = value / convert(Unit, Unit.MHz); }
    public float4 GHz { get => si * convert(Unit, Unit.GHz); set => si = value / convert(Unit, Unit.GHz); }
    public float4 THz { get => si * convert(Unit, Unit.THz); set => si = value / convert(Unit, Unit.THz); }

    public float4 ohm { get => si * convert(Unit, Unit.ohm); set => si = value / convert(Unit, Unit.ohm); }
    public float4 mho { get => si * convert(Unit, Unit.mho); set => si = value / convert(Unit, Unit.mho); }

    public float4 mi { get => si * convert(siUnit, usUnit.mi); set => si = value / convert(siUnit, usUnit.mi); }//length nmi=nautical mile, ua=astronomic unit, ly=light-year, pc=parsec(parallax second), point=printer's point
    public float4 yd { get => si * convert(siUnit, usUnit.yd); set => si = value / convert(siUnit, usUnit.yd); }
    public float4 ft { get => si * convert(siUnit, usUnit.ft); set => si = value / convert(siUnit, usUnit.ft); }
    public float4 inch { get => si * convert(siUnit, usUnit.inch); set => si = value / convert(siUnit, usUnit.inch); }
    public float4 mil { get => si * convert(siUnit, usUnit.mil); set => si = value / convert(siUnit, usUnit.mil); }
    public float4 microinch { get => si * convert(siUnit, usUnit.microinch); set => si = value / convert(siUnit, usUnit.microinch); }
    public float4 angstrom { get => si * convert(siUnit, usUnit.angstrom); set => si = value / convert(siUnit, usUnit.angstrom); }
    public float4 point { get => si * convert(siUnit, usUnit.point); set => si = value / convert(siUnit, usUnit.point); }
    public float4 nmi { get => si * convert(siUnit, usUnit.nmi); set => si = value / convert(siUnit, usUnit.nmi); }
    public float4 fathom { get => si * convert(siUnit, usUnit.fathom); set => si = value / convert(siUnit, usUnit.fathom); }
    public float4 ua { get => si * convert(siUnit, usUnit.ua); set => si = value / convert(siUnit, usUnit.ua); }
    public float4 ly { get => si * convert(siUnit, usUnit.ly); set => si = value / convert(siUnit, usUnit.ly); }
    public float4 pc { get => si * convert(siUnit, usUnit.pc); set => si = value / convert(siUnit, usUnit.pc); }

    public float4 mi2 { get => si * convert(siUnit, usUnit.mi2); set => si = value / convert(siUnit, usUnit.mi2); }//area ha=hectare
    public float4 acre { get => si * convert(siUnit, usUnit.acre); set => si = value / convert(siUnit, usUnit.acre); }
    public float4 yd2 { get => si * convert(siUnit, usUnit.yd2); set => si = value / convert(siUnit, usUnit.yd2); }
    public float4 ft2 { get => si * convert(siUnit, usUnit.ft2); set => si = value / convert(siUnit, usUnit.ft2); }
    public float4 in2 { get => si * convert(siUnit, usUnit.in2); set => si = value / convert(siUnit, usUnit.in2); }

    public float4 in3 { get => si * convert(siUnit, usUnit.in3); set => si = value / convert(siUnit, usUnit.in3); } //Volume (bbl=oil-barrel, bu=bushel
    public float4 ft3 { get => si * convert(siUnit, usUnit.ft3); set => si = value / convert(siUnit, usUnit.ft3); }
    public float4 yd3 { get => si * convert(siUnit, usUnit.yd3); set => si = value / convert(siUnit, usUnit.yd3); }
    public float4 acre_ft { get => si * convert(siUnit, usUnit.acre_ft); set => si = value / convert(siUnit, usUnit.acre_ft); }
    public float4 bbl { get => si * convert(siUnit, usUnit.bbl); set => si = value / convert(siUnit, usUnit.bbl); }
    public float4 bu { get => si * convert(siUnit, usUnit.bu); set => si = value / convert(siUnit, usUnit.bu); }
    public float4 gal { get => si * convert(siUnit, usUnit.gal); set => si = value / convert(siUnit, usUnit.gal); }
    public float4 qt { get => si * convert(siUnit, usUnit.qt); set => si = value / convert(siUnit, usUnit.qt); }
    public float4 pt { get => si * convert(siUnit, usUnit.pt); set => si = value / convert(siUnit, usUnit.pt); }
    public float4 cup { get => si * convert(siUnit, usUnit.cup); set => si = value / convert(siUnit, usUnit.cup); }
    public float4 Tbs { get => si * convert(siUnit, usUnit.tbsp); set => si = value / convert(siUnit, usUnit.tbsp); }
    public float4 tsp { get => si * convert(siUnit, usUnit.tsp); set => si = value / convert(siUnit, usUnit.tsp); }
    public float4 fl_oz { get => si * convert(siUnit, usUnit.fl_oz); set => si = value / convert(siUnit, usUnit.fl_oz); }

    public float4 mph { get => si * convert(siUnit, usUnit.mph); set => si = value / convert(siUnit, usUnit.mph); } //speed
    public float4 ftps { get => si * convert(siUnit, usUnit.ftps); set => si = value / convert(siUnit, usUnit.ftps); }
    public float4 kn { get => si * convert(siUnit, usUnit.kn); set => si = value / convert(siUnit, usUnit.kn); }

    public float4 ftps2 { get => si * convert(siUnit, usUnit.ftps2); set => si = value / convert(siUnit, usUnit.ftps2); } //acceleration
    public float4 inps2 { get => si * convert(siUnit, usUnit.inps2); set => si = value / convert(siUnit, usUnit.inps2); }

    public float4 ft3ps { get => si * convert(siUnit, usUnit.ft3ps); set => si = value / convert(siUnit, usUnit.ft3ps); } //flow rate
    public float4 ft3pmin { get => si * convert(siUnit, usUnit.ft3pmin); set => si = value / convert(siUnit, usUnit.ft3pmin); }
    public float4 yd3pmin { get => si * convert(siUnit, usUnit.yd3pmin); set => si = value / convert(siUnit, usUnit.yd3pmin); }
    public float4 galpmin { get => si * convert(siUnit, usUnit.galpmin); set => si = value / convert(siUnit, usUnit.galpmin); }
    public float4 galpday { get => si * convert(siUnit, usUnit.galpday); set => si = value / convert(siUnit, usUnit.galpday); }

    public float4 F { get => si * convert(siUnit, usUnit.F); set => si = value / convert(siUnit, usUnit.F); } //temperature

    public float4 mpg { get => si * convert(siUnit, usUnit.mpg); set => si = value / convert(siUnit, usUnit.mpg); } //fuel efficiency

    public float4 ton_us { get => si * convert(siUnit, usUnit.ton); set => si = value / convert(siUnit, usUnit.ton); } //weight
    public float4 lb { get => si * convert(siUnit, usUnit.lb); set => si = value / convert(siUnit, usUnit.lb); }
    public float4 oz { get => si * convert(siUnit, usUnit.oz); set => si = value / convert(siUnit, usUnit.oz); }
    public float4 grain { get => si * convert(siUnit, usUnit.grain); set => si = value / convert(siUnit, usUnit.grain); }

    public float4 lb_ft { get => si * convert(siUnit, usUnit.lb_ft); set => si = value / convert(siUnit, usUnit.lb_ft); }//moment of mass
    public float4 tonpyd3 { get => si * convert(siUnit, usUnit.tonpyd3); set => si = value / convert(siUnit, usUnit.tonpyd3); } //density
    public float4 lbpft3 { get => si * convert(siUnit, usUnit.lbpft3); set => si = value / convert(siUnit, usUnit.lbpft3); }
    public float4 lbfpft3 { get => si * convert(siUnit, usUnit.lbfpft3); set => si = value / convert(siUnit, usUnit.lbfpft3); }
    public float4 lbpgal { get => si * convert(siUnit, usUnit.lbpgal); set => si = value / convert(siUnit, usUnit.lbpgal); } //concentration
    public float4 ozpgal { get => si * convert(siUnit, usUnit.ozpgal); set => si = value / convert(siUnit, usUnit.ozpgal); }
    public float4 lb_ftps { get => si * convert(siUnit, usUnit.lb_ftps); set => si = value / convert(siUnit, usUnit.lb_ftps); }//momentum
    public float4 lb_ft2 { get => si * convert(siUnit, usUnit.lb_ft2); set => si = value / convert(siUnit, usUnit.lb_ft2); }//moment of inertia
    public float4 lbf { get => si * convert(siUnit, usUnit.lbf); set => si = value / convert(siUnit, usUnit.lbf); } //force
    public float4 poundal { get => si * convert(siUnit, usUnit.poundal); set => si = value / convert(siUnit, usUnit.poundal); }
    public float4 lbf_ft { get => si * convert(siUnit, usUnit.lbf_ft); set => si = value / convert(siUnit, usUnit.lbf_ft); }  //torque
    public float4 lbf_in { get => si * convert(siUnit, usUnit.lbf_in); set => si = value / convert(siUnit, usUnit.lbf_in); }
    public float4 psf { get => si * convert(siUnit, usUnit.psf); set => si = value / convert(siUnit, usUnit.psf); }   //pressure
    public float4 torr { get => si * convert(siUnit, usUnit.torr); set => si = value / convert(siUnit, usUnit.torr); }
    public float4 psi { get => si * convert(siUnit, usUnit.psi); set => si = value / convert(siUnit, usUnit.psi); }
    public float4 atm { get => si * convert(siUnit, usUnit.atm); set => si = value / convert(siUnit, usUnit.atm); }
    public float4 bar { get => si * convert(siUnit, usUnit.bar); set => si = value / convert(siUnit, usUnit.bar); }
    public float4 mbar { get => si * convert(siUnit, usUnit.mbar); set => si = value / convert(siUnit, usUnit.mbar); }
    public float4 ksi { get => si * convert(siUnit, usUnit.ksi); set => si = value / convert(siUnit, usUnit.ksi); }

    public float4 centipoise { get => si * convert(siUnit, usUnit.centipoise); set => si = value / convert(siUnit, usUnit.centipoise); }//viscosity (dynamic)
    public float4 centistokes { get => si * convert(siUnit, usUnit.centistokes); set => si = value / convert(siUnit, usUnit.centistokes); }//Viscosity (kinematic)
    public float4 kWh { get => si * convert(siUnit, usUnit.kWh); set => si = value / convert(siUnit, usUnit.kWh); } //Energy 
    public float4 cal { get => si * convert(siUnit, usUnit.cal); set => si = value / convert(siUnit, usUnit.cal); }
    public float4 Cal { get => si * convert(siUnit, usUnit.Cal); set => si = value / convert(siUnit, usUnit.Cal); }
    public float4 BTU { get => si * convert(siUnit, usUnit.BTU); set => si = value / convert(siUnit, usUnit.BTU); }
    public float4 therm { get => si * convert(siUnit, usUnit.therm); set => si = value / convert(siUnit, usUnit.therm); }
    public float4 hph { get => si * convert(siUnit, usUnit.hph); set => si = value / convert(siUnit, usUnit.hph); }
    public float4 ft_lbf { get => si * convert(siUnit, usUnit.ft_lbf); set => si = value / convert(siUnit, usUnit.ft_lbf); }
    public float4 BTUps { get => si * convert(siUnit, usUnit.BTUps); set => si = value / convert(siUnit, usUnit.BTUps); } //Power
    public float4 BTUphr { get => si * convert(siUnit, usUnit.BTUphr); set => si = value / convert(siUnit, usUnit.BTUphr); }
    public float4 hp { get => si * convert(siUnit, usUnit.hp); set => si = value / convert(siUnit, usUnit.hp); }
    public float4 ft_lbfps { get => si * convert(siUnit, usUnit.ft_lbfps); set => si = value / convert(siUnit, usUnit.ft_lbfps); }
    public float4 BTU_inphr_ft2F { get => si * convert(siUnit, usUnit.BTU_inphr_ft2F); set => si = value / convert(siUnit, usUnit.BTU_inphr_ft2F); }//Thermal conductivity
    public float4 BTUphrft2F { get => si * convert(siUnit, usUnit.BTUphrft2F); set => si = value / convert(siUnit, usUnit.BTUphrft2F); }//Coefficient of heat transfer 
    public float4 BTUpF { get => si * convert(siUnit, usUnit.BTUpF); set => si = value / convert(siUnit, usUnit.BTUpF); }//Heat capacity
    public float4 BTUplbF { get => si * convert(siUnit, usUnit.BTUplbF); set => si = value / convert(siUnit, usUnit.BTUplbF); } //Specific heat capacity 
    public float4 oersted { get => si * convert(siUnit, usUnit.oersted); set => si = value / convert(siUnit, usUnit.oersted); }//Magnetic field strength
    public float4 maxwell { get => si * convert(siUnit, usUnit.maxwell); set => si = value / convert(siUnit, usUnit.maxwell); }//Magnetic flux
    public float4 gauss { get => si * convert(siUnit, usUnit.gauss); set => si = value / convert(siUnit, usUnit.gauss); }//Magnetic flux density
    public float4 Ah { get => si * convert(siUnit, usUnit.Ah); set => si = value / convert(siUnit, usUnit.Ah); }//Electric charge
    public float4 lambert { get => si * convert(siUnit, usUnit.lambert); set => si = value / convert(siUnit, usUnit.lambert); } //Luminance
    public float4 cdpin2 { get => si * convert(siUnit, usUnit.cdpin2); set => si = value / convert(siUnit, usUnit.cdpin2); }
    public float4 lmpft2 { get => si * convert(siUnit, usUnit.lmpft2); set => si = value / convert(siUnit, usUnit.lmpft2); } // Luminous exitance
    public float4 phot { get => si * convert(siUnit, usUnit.phot); set => si = value / convert(siUnit, usUnit.phot); }
    public float4 fc { get => si * convert(siUnit, usUnit.fc); set => si = value / convert(siUnit, usUnit.fc); }//Illuminance
    public float4 Curie { get => si * convert(siUnit, usUnit.Curie); set => si = value / convert(siUnit, usUnit.Curie); }  //Activity (of a radionuclide)

    public float4 km { get => si * convert(siUnit, siUnit.km); set => si = value / convert(siUnit, siUnit.km); }  //length
    public float4 m { get => si * convert(siUnit, siUnit.m); set => si = value / convert(siUnit, siUnit.m); }
    public float4 cm { get => si * convert(siUnit, siUnit.cm); set => si = value / convert(siUnit, siUnit.cm); }
    public float4 mm { get => si * convert(siUnit, siUnit.mm); set => si = value / convert(siUnit, siUnit.mm); }
    public float4 um { get => si * convert(siUnit, siUnit.um); set => si = value / convert(siUnit, siUnit.um); }
    public float4 nm { get => si * convert(siUnit, siUnit.nm); set => si = value / convert(siUnit, siUnit.nm); }
    public float4 pm { get => si * convert(siUnit, siUnit.pm); set => si = value / convert(siUnit, siUnit.pm); }

    public float4 km2 { get => si * convert(siUnit, siUnit.km2); set => si = value / convert(siUnit, siUnit.km2); }  //area ha=hectare
    public float4 ha { get => si * convert(siUnit, siUnit.ha); set => si = value / convert(siUnit, siUnit.ha); }
    public float4 m2 { get => si * convert(siUnit, siUnit.m2); set => si = value / convert(siUnit, siUnit.m2); }
    public float4 cm2 { get => si * convert(siUnit, siUnit.cm2); set => si = value / convert(siUnit, siUnit.cm2); }
    public float4 mm2 { get => si * convert(siUnit, siUnit.mm2); set => si = value / convert(siUnit, siUnit.mm2); }

    public float4 m3 { get => si * convert(siUnit, siUnit.m3); set => si = value / convert(siUnit, siUnit.m3); } //Volume
    public float4 cm3 { get => si * convert(siUnit, siUnit.cm3); set => si = value / convert(siUnit, siUnit.cm3); }
    public float4 mm3 { get => si * convert(siUnit, siUnit.mm3); set => si = value / convert(siUnit, siUnit.mm3); }
    public float4 l { get => si * convert(siUnit, siUnit.l); set => si = value / convert(siUnit, siUnit.l); }
    public float4 ml { get => si * convert(siUnit, siUnit.ml); set => si = value / convert(siUnit, siUnit.ml); }

    public float4 kph { get => si * convert(siUnit, siUnit.kph); set => si = value / convert(siUnit, siUnit.kph); } //speed: kn = knot
    public float4 mps { get => si * convert(siUnit, siUnit.mps); set => si = value / convert(siUnit, siUnit.mps); }
    //public float4 kn { get => si * convert(siUnit, siUnit.kn); }

    public float4 mps2 { get => si * convert(siUnit, siUnit.mps2); set => si = value / convert(siUnit, siUnit.mps2); } //acceleration
    public float4 m3ps { get => si * convert(siUnit, siUnit.m3ps); set => si = value / convert(siUnit, siUnit.m3ps); } //flow rate
    public float4 lps { get => si * convert(siUnit, siUnit.lps); set => si = value / convert(siUnit, siUnit.lps); }
    public float4 lpd { get => si * convert(siUnit, siUnit.lpd); set => si = value / convert(siUnit, siUnit.lpd); }
    public float4 C { get => si * convert(siUnit, siUnit.C); set => si = value / convert(siUnit, siUnit.C); }//temperature
    public float4 K { get => si * convert(siUnit, siUnit.K); set => si = value / convert(siUnit, siUnit.K); }

    public float4 kpl { get => si * convert(siUnit, siUnit.kmpl); set => si = value / convert(siUnit, siUnit.kmpl); }//fuel efficiency
    public float4 tonsi { get => si * convert(siUnit, siUnit.ton); set => si = value / convert(siUnit, siUnit.ton); } //weight
    public float4 kg { get => si * convert(siUnit, siUnit.kg); set => si = value / convert(siUnit, siUnit.kg); }
    public float4 g { get => si * convert(siUnit, siUnit.g); set => si = value / convert(siUnit, siUnit.g); }
    public float4 mg { get => si * convert(siUnit, siUnit.mg); set => si = value / convert(siUnit, siUnit.mg); }
    public float4 ug { get => si * convert(siUnit, siUnit.ug); set => si = value / convert(siUnit, siUnit.ug); }
    public float4 kg_m { get => si * convert(siUnit, siUnit.kg_m); set => si = value / convert(siUnit, siUnit.kg_m); }//moment of mass
    public float4 kgpm3 { get => si * convert(siUnit, siUnit.kgpm3); set => si = value / convert(siUnit, siUnit.kgpm3); } //density
    public float4 Npm3 { get => si * convert(siUnit, siUnit.Npm3); set => si = value / convert(siUnit, siUnit.Npm3); } //density
    public float4 tpm3 { get => si * convert(siUnit, siUnit.Tpm3); set => si = value / convert(siUnit, siUnit.Tpm3); }
    public float4 gpl { get => si * convert(siUnit, siUnit.gpL); set => si = value / convert(siUnit, siUnit.gpL); }//concentration
    public float4 kg_mps { get => si * convert(siUnit, siUnit.kg_mps); set => si = value / convert(siUnit, siUnit.kg_mps); }//momentum
    public float4 kg_m2 { get => si * convert(siUnit, siUnit.kg_m2); set => si = value / convert(siUnit, siUnit.kg_m2); }//moment of inertia
    public float4 N { get => si * convert(siUnit, siUnit.N); set => si = value / convert(siUnit, siUnit.N); }//force
    public float4 Nm { get => si * convert(siUnit, siUnit.Nm); set => si = value / convert(siUnit, siUnit.Nm); } //torque
    public float4 Pa { get => si * convert(siUnit, siUnit.Pa); set => si = value / convert(siUnit, siUnit.Pa); }  //pressure
    public float4 kPa { get => si * convert(siUnit, siUnit.kPa); set => si = value / convert(siUnit, siUnit.kPa); }
    public float4 MPa { get => si * convert(siUnit, siUnit.MPa); set => si = value / convert(siUnit, siUnit.MPa); }
    public float4 GPa { get => si * convert(siUnit, siUnit.GPa); set => si = value / convert(siUnit, siUnit.GPa); }
    public float4 Npm2 { get => si * convert(siUnit, siUnit.Npm2); set => si = value / convert(siUnit, siUnit.Npm2); }

    public float4 mPa_s { get => si * convert(siUnit, siUnit.mPa_s); set => si = value / convert(siUnit, siUnit.mPa_s); }//viscosity (dynamic)
    public float4 mm2ps { get => si * convert(siUnit, siUnit.mm2ps); set => si = value / convert(siUnit, siUnit.mm2ps); }//Viscosity (kinematic)
    public float4 J { get => si * convert(siUnit, siUnit.J); set => si = value / convert(siUnit, siUnit.J); }  //Energy 
    public float4 kJ { get => si * convert(siUnit, siUnit.kJ); set => si = value / convert(siUnit, siUnit.kJ); }
    public float4 MJ { get => si * convert(siUnit, siUnit.MJ); set => si = value / convert(siUnit, siUnit.MJ); }
    public float4 W { get => si * convert(siUnit, siUnit.W); set => si = value / convert(siUnit, siUnit.W); } //Power
    public float4 kW { get => si * convert(siUnit, siUnit.kW); set => si = value / convert(siUnit, siUnit.kW); }
    public float4 Wpm_K { get => si * convert(siUnit, siUnit.Wpm_K); set => si = value / convert(siUnit, siUnit.Wpm_K); }//Thermal conductivity
    public float4 Wpm2_K { get => si * convert(siUnit, siUnit.Wpm2_K); set => si = value / convert(siUnit, siUnit.Wpm2_K); }//Coefficient of heat transfer 
    public float4 kJpK { get => si * convert(siUnit, siUnit.kJpK); set => si = value / convert(siUnit, siUnit.kJpK); }//Heat capacity
    public float4 kJpkg_K { get => si * convert(siUnit, siUnit.kJpkg_K); set => si = value / convert(siUnit, siUnit.kJpkg_K); } //Specific heat capacity 
    public float4 Apm { get => si * convert(siUnit, siUnit.Apm); set => si = value / convert(siUnit, siUnit.Apm); }//Magnetic field strength
    public float4 nWb { get => si * convert(siUnit, siUnit.nWb); set => si = value / convert(siUnit, siUnit.nWb); }//Magnetic flux
    public float4 mT { get => si * convert(siUnit, siUnit.mT); set => si = value / convert(siUnit, siUnit.mT); }//Magnetic flux density
    public float4 Coulomb { get => si * convert(siUnit, siUnit.Coulomb); set => si = value / convert(siUnit, siUnit.Coulomb); }//Electric charge
    public float4 cdpm2 { get => si * convert(siUnit, siUnit.cdpm2); set => si = value / convert(siUnit, siUnit.cdpm2); }//Luminance
    public float4 lx { get => si * convert(siUnit, siUnit.lx); set => si = value / convert(siUnit, siUnit.lx); }// Luminous exitance { get => si * convert(siUnit, siUnit.mho); set => si = value / convert(siUnit, siUnit.xxx); }mph; Illuminance
    public float4 MBq { get => si * convert(siUnit, siUnit.MBq); set => si = value / convert(siUnit, siUnit.MBq); }  //Activity (of a radionuclide)
  }
}