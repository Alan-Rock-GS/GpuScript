using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;
using static GpuScript.GS_cginc;
using static GpuScript.GS;

namespace GpuScript
{
	[UxmlElement]
	public partial class UI_VisualElement : VisualElement
	{
		[UxmlAttribute] public string Label { get => label; set => label = value; }
		[UxmlAttribute] public string description { get; set; } = "";
		public string treeGroup_parent_name { get => treeGroup_parent?.name ?? ""; set { treeGroup_parent = gs?.UI_GS?.Query<UI_TreeGroup>(value) ?? null; } }
		[UxmlAttribute] public string tree { get; set; }
		public virtual bool _isGrid { get; set; }
		[UxmlAttribute] public bool isGrid { get => _isGrid; set => _isGrid = value; }
		public virtual bool Init(GS gs, params GS[] gss)
		{
			GS gs2 = gs;
			var fld = gs.GetType().GetField($"UI_grid_{name}", GS.bindings) ?? gs.GetType().GetField($"UI_{name}", GS.bindings);
			if (fld == null)
			{
				foreach (var obj_gs in gss)
					if (gss != null)
					{
						fld = obj_gs.GetType().GetField($"UI_grid_{name}", GS.bindings) ?? obj_gs.GetType().GetField($"UI_{name}", GS.bindings);
						if (fld != null) { gs2 = obj_gs; break; }
					}
			}
			if (fld != null)
			{
				this.gs = gs2;
				var prop = gs2.GetType().GetProperty(name, GS.bindings);
				property = prop;
				ui_field = fld;
				ui_field?.SetValue(gs2, this);
				Assign_style(gs.UI_GS, width: width_UI);
				if (tree != null) treeGroup_parent = gs.UI_GS.Query<UI_TreeGroup>(tree);
				if (treeGroup_parent != null && this is UI_TreeGroup) treeGroup_parent.ui_children.Add(this);
				return true;
			}
			else
			{
				var method = gs2.GetType().GetMethod(name, GS.bindings);
				if (method != null)
				{
					this.gs = gs2;
					treeGroup_parent = gs.UI_GS.Query<UI_TreeGroup>(treeGroup_parent_name);
					treeGroup_parent?.ui_children.Add(this);
					return true;
				}
			}
			return false;
		}
		public static void UXML(UI_Element e, AttGS att, string name, string label, string typeName)
		{
			if (att == null) return;
			UXML(e, 0, $"<GpuScript.{typeName} name=\"{name}\"");
			if (label.IsNotEmpty()) e.uxml.Add(" label=\"", label, "\"");
			if (att.Description.IsNotEmpty()) e.uxml.Add($" description=\"{att.Description}\"");
			//if (att.readOnly) e.uxml.Add($" isReadonly=\"true\"");
			if (att.readOnly) e.uxml.Add($" is-read-only=\"true\"");
			if (e.tree.IsNotEmpty()) e.uxml.Add($" tree=\"{e.tree}\"");
			e.uxml.Add(" tabindex=\"-1\"");
		}
		public void Build(string label, string description, bool isReadOnly, bool isGrid, string treeGroup_parent_name)
		{
			Build(label, description, isReadOnly, treeGroup_parent_name); this.isGrid = isGrid;
		}
		public void Build(string Label, string Description, bool isReadOnly, string treeGroup_parent_name)
		{
			label = Label; description = Description; this.isReadOnly = isReadOnly; this.treeGroup_parent_name = treeGroup_parent_name;
			treeGroup_parent = gs?.UI_GS.Query<UI_TreeGroup>(treeGroup_parent_name);
			if (treeGroup_parent != null && treeGroup_parent != this) treeGroup_parent.ui_children.Add(this);
		}
		public virtual string label { get; set; }
		public bool changedFromCloud;
		public bool isServerUpdate;
		public bool isNotServerUpdate => !isServerUpdate;
		public Stopwatch LastChangedTime;
		public virtual string textString => "";
		public virtual float ui_width { get => UI_TextWidth(textString); set { style.width = value; } }
		public float4 screen_coordinates() => worldBound;
		public virtual void OnUnitsChanged() { }
		protected void Load() { Resources.Load<VisualTreeAsset>("UI/GpuScript/" + GetType().Name)?.CloneTree(this); }
		public UI_VisualElement() { Load(); }
		public bool isBuilding = false;
		public UI_VisualElement(int rowI, AttGS att) : this()
		{
			isBuilding = true;
			var a = att;
			var fldName = a.Name.ReplaceAll(" ", "_", "-", "_");
			name = $"UI_float_{fldName}_{rowI + 1}";
			description = a.Description;
			isGrid = true;
			isReadOnly = a.readOnly;
		}

		public PropertyInfo property;
		public FieldInfo ui_field;
		public GS gs;
		public static DisplayStyle DisplayIf(bool display) => display ? DisplayStyle.Flex : DisplayStyle.None;
		public static DisplayStyle HideIf(bool hide) => DisplayIf(!hide);
		public bool isNotGrid => !isGrid;
		public virtual bool _isReadOnly { get; set; }
		[UxmlAttribute] public bool isReadOnly { get => _isReadOnly; set => _isReadOnly = value; }
		public bool isNotReadOnly => !isReadOnly;
		public virtual void OnMouseEnter(MouseEnterEvent evt) { if (gs != null) { gs.status = description; GS.mouseInUI = true; } }
		public virtual void OnMouseLeave(MouseLeaveEvent evt) { if (gs != null) { gs.status = ""; if (!GS.sliderHasFocus) GS.mouseInUI = false; } }

		public virtual bool display { get => style.display == DisplayStyle.Flex; set => style.display = value ? DisplayStyle.Flex : DisplayStyle.None; }
		protected virtual object GetValue(AttGS attGS, PropertyInfo property, GS gs, bool isNull, bool ui_txt_Exists, object v) => attGS?.Val ?? property?.GetValue(gs) ?? v;

		public static int width_UI = 200;

		public virtual object v_obj { get; set; }

		//protected void SetPropertyValue(object val) { if (property != null) property.SetValue(gs, val); else if (isGrid && gs != null) grid_OnValueChanged(); gs?.OnValueChanged(); }
		protected void SetPropertyValue(object val)
		{
			if (property != null)
				property.SetValue(gs, val);
			else if (isGrid && gs != null)
				grid_OnValueChanged();
			gs?.OnValueChanged();
		}

		protected Color _BackgroundColor = WHITE;
		public virtual Color BackgroundColor { get => _BackgroundColor; set => _BackgroundColor = value; }


		public static void Start_UXML_Text(StrBldr uxml, int uxml_level) { uxml.Add("\n"); for (int i = 0; i < uxml_level; i++) uxml.Add("    "); }
		public static void Start_UXML_Component_Text(StrBldr uxml, int uxml_level, string component) { Start_UXML_Text(uxml, uxml_level); uxml.Add("<", component); }
		public static void UXML(UI_Element e, int level) { e.uxml_level += level; e.uxml.Add("\n"); for (int i = 0; i < e.uxml_level; i++) e.uxml.Add("    "); }
		public static void UXML(UI_Element e, int level, string component) { UXML(e, level); e.uxml.Add(component); }

		public UI_grid grid;
		public int gridRow, gridCol;
		//public void grid_OnValueChanged() => $"{grid.name}_OnValueChanged".InvokeMethod(gs, gridRow, gridCol);
		public void grid_OnValueChanged()
		{
			if (grid != null && !GS.isGridVScroll && !GS.isGridBuilding)
				$"{grid.name}_OnValueChanged".InvokeMethod(gs, gridRow, gridCol);
		}
		//public virtual void RegisterGridCallbacks(GS gs, UI_grid grid, int gridRow, int gridCol)
		//{
		//  this.gs = gs;
		//  this.grid = grid;
		//  this.gridRow = gridRow;
		//  this.gridCol = gridCol;
		//}
		public virtual void RegisterGridCallbacks(GS gs, UI_grid grid, int gridRow, int gridCol) => (this.gs, this.grid, this.gridRow, this.gridCol) = (gs, grid, gridRow, gridCol);

		public void End_UI_Element(UI_Element e) { End_UXML_Component_Text(e.uxml); }


		public static void _cs_Write(GS gs, StrBldr tData, StrBldr lateUpdate, StrBldr lateUpdate_ValuesChanged,
			StrBldr showIfs, StrBldr onValueChanged, AttGS attGS, string typeStr, string name)
		{
			lateUpdate_ValuesChanged.Add($" = UI_{name}.Changed");
			Add_onValueChanged(onValueChanged, name, attGS);
			Add_showIf(showIfs, name, attGS);
		}

		public static void _cs_Write(Type _GS_fieldType, GS gs, StrBldr tData, StrBldr lateUpdate, StrBldr lateUpdate_ValuesChanged,
					StrBldr showIfs, StrBldr onValueChanged, AttGS attGS, string typeStr, string name)
		{
			if (_GS_fieldType == null)
				UI_method._cs_Write(gs, tData, lateUpdate, lateUpdate_ValuesChanged, showIfs, onValueChanged, attGS, typeStr, name);
			else if (_GS_fieldType.isNestedClass())
				UI_Array._cs_Write(gs, tData, lateUpdate, lateUpdate_ValuesChanged, showIfs, onValueChanged, attGS, typeStr, name);
			else if (!_GS_fieldType.IsArray)
				Run_static_Method(_GS_fieldType, "_cs_Write", gs, tData, lateUpdate, lateUpdate_ValuesChanged, showIfs, onValueChanged, attGS, typeStr, name);
		}

		public static MethodInfo Get_static_Method(Type _GS_fieldType, string methodName)
		{
			return Get_UI_VisualElement_Type(_GS_fieldType)?.GetMethod(methodName, BindingFlags.Static | BindingFlags.Public);
		}
		public static void Run_static_Method(Type type, string methodName, params object[] args)
		{
			(Get_static_Method(type, methodName) ?? type.GetMethod(methodName, BindingFlags.Static | BindingFlags.Public))?.Invoke(null, args);
		}

		protected static void StackFields(StrBldr sb, string type, string name, string indent = "    ")
		{
			string s = sb.ToString(), t = $"public {type} ";
			if (s.Contains(t)) sb.Set(s.BeforeIncluding(t), s.Between(t, ";"), $", {name};", s.After(t).After(";")); else sb.Add("\n", indent, t, name, ";");
		}
		protected static void StackEnums(StrBldr sb, string type, string name, string indent = "    ")
		{
			string s = sb.ToString(), t = $"[JsonConverter(typeof(StringEnumConverter))] public {type} ";
			if (s.Contains(t)) sb.Set(s.BeforeIncluding(t), s.Between(t, ";"), $", {name};", s.After(t).After(";")); else sb.Add("\n", indent, t, name, ";");
		}
		public static void Add_showIf(StrBldr showIfs, string name, AttGS attGS)
		{
			if (showIfs != null && attGS != null && attGS.ShowIf != null)
			{
				StrBldr s = new StrBldr("\n  public virtual bool Show_", name, " { get => ", attGS.ShowIf.ToString().ReplaceAll("True", "true", "False", "false"), "; }");
				if (showIfs.ToString().DoesNotContain(s)) showIfs.Add(s);
			}
		}

		protected static void Add_onValueChanged(StrBldr onValueChanged, string name, AttGS attGS)
		{
			if (attGS != null)
			{
				string code = attGS.OnValueChanged;
				if (code.IsNotEmpty()) { if (code.DoesNotEndWith(";")) code += ";"; onValueChanged.Add($"\n    if (UI_{name}.Changed) {{ {code} }}"); }
			}
		}

		public static Type Get_UI_VisualElement_Type(Type type)
		{
			foreach (var t in UI_Element_Types)
			{
				Type typ = (Type)t.GetMethod("Get_Base_Type", BindingFlags.Static | BindingFlags.Public)?.Invoke(null, null);
				if (t.Name.StartsWith("UI_"))
					if (type == typ || (type != null && type.IsEnum && typ == typeof(Enum)))
						return t;
			}
			return Type.GetType("Row_UI_" + type?.ToString()?.Replace("GpuScript.", ""));
		}

		public UI_TreeGroup treeGroup_parent;
		public static bool UXML_UI_Element(UI_Element e, MemberInfo[] _gs_members)
		{
			if (e._GS_fieldType == null && e._GS_propType == null && e.methodInfo != null) UI_method.UXML_UI_Element(e);
			else if (e._GS_fieldType?.IsArray ?? false) UI_grid.UXML_UI_Element(e, _gs_members);
			else if (e._GS_fieldType.IsEnum) UI_enum.UXML_UI_Element(e);
			else if (e._GS_fieldType.Name == nameof(TreeGroup)) TreeGroup.UXML_UI_Element(e);
			else if (e._GS_fieldType.Name == nameof(TreeGroupEnd)) TreeGroupEnd.UXML_UI_Element(e);
			else Run_static_Method(e._GS_fieldType, "UXML_UI_Element", e);
			return true;
		}
		public static void UXML_UI_grid_type_member(UI_Element e, Type type, AttGS attGS, MemberInfo m, uint rowI, float rowWidthPercent)
		{
			if (type == null && e._GS_propType == null && e.methodInfo != null) UI_method.UXML_UI_grid_member(e, m, attGS, rowI, rowWidthPercent);
			else if (type?.IsArray ?? false) UI_grid.UXML_UI_grid_member(e, m, attGS, rowI, rowWidthPercent);
			else if (type?.IsEnum ?? false) UI_enum.UXML_UI_grid_member(e, m, attGS, rowI, rowWidthPercent);
			else if (type != null) Run_static_Method(type, "UXML_UI_grid_member", e, m, attGS, rowI, rowWidthPercent);
		}

		public static void Start_USS_Segment_Text(GS gs, StrBldr sb, string component, string name = "", string text = "")
		{
			sb.Add("\n");
			for (int i = 0; i < gs.uxml_level; i++) sb.Add("    ");
			sb.Add("<", component);
			if (name.IsNotEmpty()) sb.Add(" name=\"", name, "\"");
			if (text.IsNotEmpty()) sb.Add(" text=\"", text, "\"");
			sb.Add(" >");
			gs.uxml_level++;
		}
		public static string UXML_Level(int level) => "\n".PadRight(4 * level, ' ');
		public static void End_UXML_Component_Text(StrBldr sb) { sb.Add(" />"); }
		public static void Middle_UXML_Component_Text(StrBldr uxml, string component, string name = "", string label = "", string text = "", string className = "",
			string type = "", string value = "", string low_value = "", string high_value = "", string picking_mode = "", string tabindex = "",
			string display_tooltip_when_elided = "", string height = "")
		{
			bool isUI = component.StartsWith("UI_");
			if (picking_mode.IsNotEmpty()) uxml.Add(" picking-mode=\"", picking_mode, "\"");
			if (name.IsNotEmpty()) uxml.Add(" name=\"", name, "\"");
			if (label.IsNotEmpty()) uxml.Add(" ", isUI ? component + "_" : "", "label=\"", label, "\"");
			if (value.IsNotEmpty()) uxml.Add(" ", isUI ? component + "_" : "", "value=\"", value, "\"");
			if (low_value.IsNotEmpty()) uxml.Add(" low-value=\"", low_value, "\"");
			if (high_value.IsNotEmpty()) uxml.Add(" high-value=\"", high_value, "\"");
			if (text.IsNotEmpty()) uxml.Add(" text=\"", text, "\"");
			if (type.IsNotEmpty()) uxml.Add(" ", isUI ? component + "_" : "", "type=\"", type, "\"");
			if (tabindex.IsNotEmpty()) uxml.Add(" tabindex=\"", tabindex, "\"");
			if (display_tooltip_when_elided.IsNotEmpty()) uxml.Add(" display-tooltip-when-elided=\"", display_tooltip_when_elided, "\"");
			if (className.IsNotEmpty()) uxml.Add(" class=\"", className, "\"");
			if (height.IsNotEmpty()) uxml.Add(" style=\"height: ", height, ";\"");
		}

		public static Type[] Get_Subclass_Types(Type subclass_Type)
		{
			return AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes()).Where(t => t.IsSubclassOf(subclass_Type)).Select(t => t).ToArray();
		}
		static Type[] _UI_Element_Types = null;
		public static Type[] UI_Element_Types => _UI_Element_Types ?? (_UI_Element_Types = Get_Subclass_Types(typeof(UI_VisualElement)));
		//public static void print(object message) => message.print();
		public static bool Get_USS_Text(StrBldr text, string name, int height = 0, float margin = GS.fNegInf, float padding = GS.fNegInf,
		float border = GS.fNegInf, int fontSize = 0, string alignText = "", string background_color = "", string unity_font = "", string justify_content = "")
		{
			bool rebuild = false;
			if (text.ToString().DoesNotContain($"{name} {{"))
			{
				rebuild = true;
				text.Add(name, " {");
				if (fontSize > 0) text.Add(" font-size: ", fontSize, "px;");
				if (unity_font.IsNotEmpty()) text.Add(" -unity-font: resource(\"", unity_font, "\");");
				if (alignText.IsNotEmpty()) text.Add(" -unity-text-align: ", alignText, ";");
				if (background_color.IsNotEmpty()) text.Add(" background-color: ", background_color, ";");
				if (height > 0) text.Add(" height: ", height, "px;");
				if (GS.IsNotNegInf(margin)) text.Add(" margin: ", margin, ";");
				if (GS.IsNotNegInf(padding)) text.Add(" padding: ", padding, ";");
				if (GS.IsNotNegInf(border)) text.Add(" border: ", border, ";");
				text.Add(" }\n");
			}
			return rebuild;
		}

		public static siUnit GetUnitConversion(siUnit si)
		{
			switch (si)
			{
				case siUnit.km: case siUnit.m: case siUnit.cm: case siUnit.mm: case siUnit.um: case siUnit.nm: case siUnit.pm: return siUnit.m;
				case siUnit.km2: case siUnit.ha: case siUnit.m2: case siUnit.cm2: case siUnit.mm2: return siUnit.m2;
				case siUnit.m3: case siUnit.cm3: case siUnit.mm3: case siUnit.l: case siUnit.ml: return siUnit.m3;
				case siUnit.kph: case siUnit.mps: case siUnit.kn: return siUnit.mps;
				case siUnit.m3ps: case siUnit.lps: case siUnit.lpd: return siUnit.m3ps;
				case siUnit.C: case siUnit.K: return siUnit.C;
				case siUnit.ton: case siUnit.kg: case siUnit.g: case siUnit.mg: case siUnit.ug: return siUnit.kg;
				case siUnit.Pa: case siUnit.kPa: case siUnit.MPa: case siUnit.GPa: case siUnit.Npm2: return siUnit.MPa;
				case siUnit.J: case siUnit.kJ: case siUnit.MJ: return siUnit.kJ;
				case siUnit.W: case siUnit.kW: return siUnit.kW;
			}
			return siUnit.Null;
		}
		public static Unit GetUnitConversion(Unit unit)
		{
			switch (unit)
			{
				case Unit.ns: case Unit.us: case Unit.ms: case Unit.s: case Unit.min: case Unit.hr: case Unit.day: case Unit.week: case Unit.month: case Unit.year: return Unit.s;
				case Unit.deg: return Unit.deg;
				case Unit.deg_per_sec: case Unit.rpm: case Unit.rps: return Unit.deg_per_sec;
				case Unit.rad: return Unit.rad;
				case Unit.bit: case Unit.Byte: case Unit.KB: case Unit.MB: case Unit.GB: case Unit.TB: case Unit.PB: return Unit.Byte;
				case Unit.bps: case Unit.Bps: case Unit.Kbps: case Unit.Mbps: case Unit.Gbps: case Unit.Tbps: case Unit.Pbps: case Unit.KBps: case Unit.MBps: case Unit.GBps: case Unit.TBps: case Unit.PBps: return Unit.Bps;
				case Unit.Hz: case Unit.kHz: case Unit.MHz: case Unit.GHz: case Unit.THz: return Unit.Hz;
				case Unit.FLOPS: case Unit.kFLOPS: case Unit.MFLOPS: case Unit.GFLOPS: case Unit.TFLOPS: case Unit.PFLOPS: case Unit.EFLOPS: case Unit.ZFLOPS: case Unit.YFLOPS: return Unit.FLOPS;
			}
			return Unit.Null;
		}
		public static string GetUnitConversion(AttGS att)
		{
			if (att == null) return "v";
			siUnit si = GetUnitConversion(att.siUnit);
			if (si != siUnit.Null) return si.ToString();
			Unit unit = GetUnitConversion(att.Unit);
			return unit.ToString();
		}



		public static VisualElement Assign_style(VisualElement o, StyleLength width = default, StyleLength maxwidth = default, StyleLength height = default,
			float margin = 0, float padding = 0, float border = 0,
			TextAnchor textAnchor = TextAnchor.LowerLeft, string className = "", int flexGrow = 1, FlexDirection flexDirection = FlexDirection.Column,
			Align alignSelf = Align.Auto, Color background_color = default, bool display = true)
		{
			var s = o.style;
			if (width != default)
				s.width = width;
			if (maxwidth != default)
				s.maxWidth = maxwidth;
			if (height != default) s.height = height;
			s.marginBottom = s.marginLeft = s.marginRight = s.marginTop = margin;
			s.paddingBottom = s.paddingLeft = s.paddingRight = s.paddingTop = padding;
			s.borderBottomWidth = s.borderTopWidth = border;
			s.unityTextAlign = textAnchor;
			s.flexGrow = flexGrow;
			s.flexDirection = flexDirection;
			if (alignSelf != Align.Auto) s.alignSelf = alignSelf;
			if (className.IsNotEmpty() && !o.ClassListContains(className)) o.AddToClassList(className);
			if (background_color != default) s.backgroundColor = new StyleColor(background_color);
			s.display = display ? DisplayStyle.Flex : DisplayStyle.None;
			return o;
		}

		[HideInInspector, UxmlAttribute] public usUnit usUnit;
		[HideInInspector, UxmlAttribute] public siUnit siUnit;
		//[HideInInspector, UxmlAttribute] public Unit Unit;
		Unit _Unit;
		[HideInInspector, UxmlAttribute] public Unit Unit { get => _Unit; set { _Unit = value; unit = _Unit.ToString(); } }
		//public string unit
		//{
		//  get
		//  {
		//    if (GS.siUnits) return siUnit != siUnit.Null ? siUnit.ToLabel() : Unit.ToLabel();
		//    else return usUnit != usUnit.Null ? usUnit.ToLabel() : Unit.ToLabel();
		//  }
		//}
		string _unit;
		public virtual string unit
		{
			get { if (GS.siUnits) return siUnit != siUnit.Null ? siUnit.ToLabel() : Unit.ToLabel(); else return usUnit != usUnit.Null ? usUnit.ToLabel() : Unit.ToLabel(); }
			set => _unit = value;
		}

		public virtual bool Changed { get; set; }
		public bool NotChanged => !Changed;
		public static siUnit Match(usUnit us)
		{
			switch (us)
			{
				case usUnit.mi: return siUnit.km;//length nmi=nautical mile, ua=astronomic unit, ly=light-year, pc=parsec(parallax second), point=printer's point
				case usUnit.yd: return siUnit.m;
				case usUnit.ft: return siUnit.m;
				case usUnit.inch: return siUnit.mm;
				case usUnit.mil: return siUnit.mm;
				case usUnit.microinch: return siUnit.um;
				case usUnit.angstrom: return siUnit.nm;
				case usUnit.point: return siUnit.mm;
				case usUnit.nmi: return siUnit.km;
				case usUnit.fathom: return siUnit.m;
				case usUnit.ua: return siUnit.km;
				case usUnit.ly: return siUnit.km;
				case usUnit.pc: return siUnit.km;

				case usUnit.mi2: return siUnit.km2; //area ha=hectare
				case usUnit.acre: return siUnit.ha;
				case usUnit.yd2: return siUnit.m2;
				case usUnit.ft2: return siUnit.m2;
				case usUnit.in2: return siUnit.mm2;

				case usUnit.in3: return siUnit.mm3; //Volume (bbl=oil-barrel, bu=bushel
				case usUnit.ft3: return siUnit.m3;
				case usUnit.yd3: return siUnit.m3;
				case usUnit.acre_ft: return siUnit.m3;
				case usUnit.bbl: return siUnit.l;
				case usUnit.bu: return siUnit.l;
				case usUnit.gal: return siUnit.l;
				case usUnit.qt: return siUnit.l;
				case usUnit.pt: return siUnit.l;
				case usUnit.cup: return siUnit.l;
				case usUnit.tbsp: return siUnit.ml;
				case usUnit.tsp: return siUnit.ml;
				case usUnit.fl_oz: return siUnit.ml;

				case usUnit.mph: return siUnit.kph; //speed
				case usUnit.ftps: return siUnit.mps;
				case usUnit.kn: return siUnit.kn;

				case usUnit.ftps2: return siUnit.mps2; //acceleration
				case usUnit.inps2: return siUnit.mps2;

				case usUnit.ft3ps: return siUnit.m3ps; //flow rate
				case usUnit.ft3pmin: return siUnit.m3ps;
				case usUnit.yd3pmin: return siUnit.lps;
				case usUnit.galpmin: return siUnit.lps;
				case usUnit.galpday: return siUnit.lpd;

				case usUnit.F: return siUnit.C; //temperature

				case usUnit.mpg: return siUnit.kmpl; //fuel efficiency

				case usUnit.ton: return siUnit.ton; //weight
				case usUnit.lb: return siUnit.kg;
				case usUnit.oz: return siUnit.g;
				case usUnit.grain: return siUnit.mg;

				case usUnit.lb_ft: return siUnit.kg_m;//moment of mass
				case usUnit.tonpyd3: return siUnit.Tpm3; //density
				case usUnit.lbpft3: return siUnit.kgpm3;
				case usUnit.lbfpft3: return siUnit.Npm3;
				case usUnit.lbpgal: return siUnit.gpL; //concentration
				case usUnit.ozpgal: return siUnit.gpL;
				case usUnit.lb_ftps: return siUnit.kg_mps;//momentum
				case usUnit.lb_ft2: return siUnit.kg_m2;//moment of inertia
				case usUnit.lbf: return siUnit.N; //force
				case usUnit.poundal: return siUnit.N;
				case usUnit.lbf_ft: return siUnit.Nm;  //torque
				case usUnit.lbf_in: return siUnit.Nm;
				case usUnit.psf: return siUnit.kPa;   //pressure
				case usUnit.torr: return siUnit.Pa;
				case usUnit.psi: return siUnit.kPa;
				case usUnit.atm: return siUnit.kPa;
				case usUnit.bar: return siUnit.kPa;
				case usUnit.mbar: return siUnit.kPa;
				case usUnit.ksi: return siUnit.MPa;

				case usUnit.centipoise: return siUnit.mPa_s;//viscosity (dynamic)
				case usUnit.centistokes: return siUnit.mm2ps;//Viscosity (kinematic)
				case usUnit.kWh: return siUnit.MJ; //Energy: return siUnit.mg; work: return siUnit.mg; heat 
				case usUnit.cal: return siUnit.J;
				case usUnit.Cal: return siUnit.kJ;
				case usUnit.BTU: return siUnit.kJ;
				case usUnit.therm: return siUnit.MJ;
				case usUnit.hph: return siUnit.MJ;
				case usUnit.ft_lbf: return siUnit.J;
				case usUnit.BTUps: return siUnit.kW; //Power
				case usUnit.BTUphr: return siUnit.W;
				case usUnit.hp: return siUnit.W;
				case usUnit.ft_lbfps: return siUnit.W;
				case usUnit.BTU_inphr_ft2F: return siUnit.Wpm_K;//Thermal conductivity
				case usUnit.BTUphrft2F: return siUnit.Wpm2_K;//Coefficient of heat transfer 
				case usUnit.BTUpF: return siUnit.kJpK;//Heat capacity
				case usUnit.BTUplbF: return siUnit.kJpkg_K; //Specific heat capacity 
				case usUnit.oersted: return siUnit.Apm;//Magnetic field strength
				case usUnit.maxwell: return siUnit.nWb;//Magnetic flux
				case usUnit.gauss: return siUnit.mT;//Magnetic flux density
				case usUnit.Ah: return siUnit.Coulomb;//Electric charge
				case usUnit.lambert: return siUnit.cdpm2; //Luminance
				case usUnit.cdpin2: return siUnit.cdpm2;
				case usUnit.lmpft2: return siUnit.lx; // Luminous exitance
				case usUnit.phot: return siUnit.lx;
				case usUnit.fc: return siUnit.lx;//Illuminance
				case usUnit.Curie: return siUnit.MBq;  //Activity (of a radionuclide)
			}
			return 0;
		}
		public static usUnit Match(siUnit si)
		{
			switch (si)
			{
				case siUnit.km: return usUnit.mi;  //length
				case siUnit.m: return usUnit.ft;
				case siUnit.cm: return usUnit.inch;
				case siUnit.mm: return usUnit.inch;
				case siUnit.um: return usUnit.microinch;
				case siUnit.nm: return usUnit.angstrom;
				case siUnit.pm: return usUnit.angstrom;

				case siUnit.km2: return usUnit.mi2;  //area ha=hectare
				case siUnit.ha: return usUnit.acre;
				case siUnit.m2: return usUnit.ft2;
				case siUnit.cm2: return usUnit.in2;
				case siUnit.mm2: return usUnit.in2;

				case siUnit.m3: return usUnit.ft3; //Volume
				case siUnit.cm3: return usUnit.in3;
				case siUnit.mm3: return usUnit.in3;
				case siUnit.l: return usUnit.gal;
				case siUnit.ml: return usUnit.in3;

				case siUnit.kph: return usUnit.mph; //speed: kn = knot
				case siUnit.mps: return usUnit.ftps;
				case siUnit.kn: return usUnit.kn;

				case siUnit.mps2: return usUnit.ftps2; //acceleration
				case siUnit.m3ps: return usUnit.ft3ps; //flow rate
				case siUnit.lps: return usUnit.galpmin;
				case siUnit.lpd: return usUnit.galpday;
				case siUnit.C: return usUnit.F;//temperature
				case siUnit.K: return usUnit.F;

				case siUnit.kmpl: return usUnit.mpg;//fuel efficiency
				case siUnit.ton: return usUnit.ton; //weight
				case siUnit.kg: return usUnit.lb;
				case siUnit.g: return usUnit.oz;
				case siUnit.mg: return usUnit.grain;
				case siUnit.ug: return usUnit.grain;
				case siUnit.kg_m: return usUnit.lb_ft;//moment of mass
				case siUnit.kgpm3: return usUnit.lbpft3; //density
				case siUnit.Npm3: return usUnit.lbfpft3; //density
				case siUnit.Tpm3: return usUnit.tonpyd3;
				case siUnit.gpL: return usUnit.lbpgal;//concentration
				case siUnit.kg_mps: return usUnit.lb_ftps;//momentum
				case siUnit.kg_m2: return usUnit.lb_ft2;//moment of inertia
				case siUnit.N: return usUnit.lbf;//force
				case siUnit.Nm: return usUnit.lbf_ft; //torque
				case siUnit.Pa: return usUnit.torr;  //pressure
				case siUnit.kPa: return usUnit.psi;
				case siUnit.MPa: return usUnit.ksi;
				case siUnit.GPa: return usUnit.ksi;

				case siUnit.mPa_s: return usUnit.centipoise;//viscosity (dynamic)
				case siUnit.mm2ps: return usUnit.centistokes;//Viscosity (kinematic)
				case siUnit.J: return usUnit.cal;  //Energy, work, heat 
				case siUnit.kJ: return usUnit.Cal;
				case siUnit.MJ: return usUnit.hph;
				case siUnit.W: return usUnit.hp; //Power
				case siUnit.kW: return usUnit.BTUps;
				case siUnit.Wpm_K: return usUnit.BTU_inphr_ft2F;//Thermal conductivity
				case siUnit.Wpm2_K: return usUnit.BTUphrft2F;//Coefficient of heat transfer 
				case siUnit.kJpK: return usUnit.BTUpF;//Heat capacity
				case siUnit.kJpkg_K: return usUnit.BTUplbF; //Specific heat capacity 
				case siUnit.Apm: return usUnit.oersted;//Magnetic field strength
				case siUnit.nWb: return usUnit.maxwell;//Magnetic flux
				case siUnit.mT: return usUnit.gauss;//Magnetic flux density
				case siUnit.Coulomb: return usUnit.Ah;//Electric charge
				case siUnit.cdpm2: return usUnit.lambert;//Luminance
				case siUnit.lx: return usUnit.fc;// Luminous exitance, Illuminance
				case siUnit.MBq: return usUnit.Curie;  //Activity (of a radionuclide)
			}
			return 0;
		}

		public float iconvert(float v) => usUnits && siUnit == siUnit.C ? C_to_F(v) : usUnits && siUnit == siUnit.K ? v * 1.8f - 459.67f : v / (siUnits || usUnit == 0 ? 1 : convert(siUnit, usUnit));
		public float2 iconvert(float2 v) => usUnits && siUnit == siUnit.C ? C_to_F(v) : usUnits && siUnit == siUnit.K ? v * 1.8f - 459.67f : v / (siUnits || usUnit == 0 ? 1 : convert(siUnit, usUnit));
		public float3 iconvert(float3 v) => usUnits && siUnit == siUnit.C ? C_to_F(v) : usUnits && siUnit == siUnit.K ? v * 1.8f - 459.67f : v / (siUnits || usUnit == 0 ? 1 : convert(siUnit, usUnit));
		public float4 iconvert(float4 v) => usUnits && siUnit == siUnit.C ? C_to_F(v) : usUnits && siUnit == siUnit.K ? v * 1.8f - 459.67f : v / (siUnits || usUnit == 0 ? 1 : convert(siUnit, usUnit));
		public double diconvert(double v) => usUnits && siUnit == siUnit.C ? dC_to_F(v) : usUnits && siUnit == siUnit.K ? v * 1.8 - 459.67 : v / (siUnits || usUnit == 0 ? 1 : dconvert(siUnit, usUnit));
		public double2 diconvert(double2 v) => usUnits && siUnit == siUnit.C ? dC_to_F(v) : usUnits && siUnit == siUnit.K ? v * 1.8 - 459.67 : v / (siUnits || usUnit == 0 ? 1 : dconvert(siUnit, usUnit));
		public double3 diconvert(double3 v) => usUnits && siUnit == siUnit.C ? dC_to_F(v) : usUnits && siUnit == siUnit.K ? v * 1.8 - 459.67 : v / (siUnits || usUnit == 0 ? 1 : dconvert(siUnit, usUnit));
		public double4 diconvert(double4 v) => usUnits && siUnit == siUnit.C ? dC_to_F(v) : usUnits && siUnit == siUnit.K ? v * 1.8 - 459.67 : v / (siUnits || usUnit == 0 ? 1 : dconvert(siUnit, usUnit));
		public float convert(float v) => usUnits && siUnit == siUnit.C ? F_to_C(v) : usUnits && siUnit == siUnit.K ? (v + 459.67f) / 1.8f : v * (siUnits || usUnit == 0 ? 1 : convert(siUnit, usUnit));
		public float2 convert(float2 v) => usUnits && siUnit == siUnit.C ? F_to_C(v) : usUnits && siUnit == siUnit.K ? (v + 459.67f) / 1.8f : v * (siUnits || usUnit == 0 ? 1 : convert(siUnit, usUnit));
		public float3 convert(float3 v) => usUnits && siUnit == siUnit.C ? F_to_C(v) : usUnits && siUnit == siUnit.K ? (v + 459.67f) / 1.8f : v * (siUnits || usUnit == 0 ? 1 : convert(siUnit, usUnit));
		public float4 convert(float4 v) => usUnits && siUnit == siUnit.C ? F_to_C(v) : usUnits && siUnit == siUnit.K ? (v + 459.67f) / 1.8f : v * (siUnits || usUnit == 0 ? 1 : convert(siUnit, usUnit));
		public double dconvert() => dconvert(siUnit, usUnit);
		public double dconvert(double v) => usUnits && siUnit == siUnit.C ? dF_to_C(v) : usUnits && siUnit == siUnit.K ? (v + 459.67) / 1.8 : v * (siUnits || usUnit == 0 ? 1 : dconvert(siUnit, usUnit));
		public double2 dconvert(double2 v) => usUnits && siUnit == siUnit.C ? dF_to_C(v) : usUnits && siUnit == siUnit.K ? (v + 459.67) / 1.8 : v * (siUnits || usUnit == 0 ? 1 : dconvert(siUnit, usUnit));
		public double3 dconvert(double3 v) => usUnits && siUnit == siUnit.C ? dF_to_C(v) : usUnits && siUnit == siUnit.K ? (v + 459.67) / 1.8 : v * (siUnits || usUnit == 0 ? 1 : dconvert(siUnit, usUnit));
		public double4 dconvert(double4 v) => usUnits && siUnit == siUnit.C ? dF_to_C(v) : usUnits && siUnit == siUnit.K ? (v + 459.67) / 1.8 : v * (siUnits || usUnit == 0 ? 1 : dconvert(siUnit, usUnit));
		public static float convert(siUnit siUnit) => convert(siUnit, GetUnitConversion(siUnit));
		public static float convert(siUnit siUnit, usUnit usUnit)
		{
			var u = usUnit;
			switch (siUnit)
			{
				case siUnit.m: return u == usUnit.mi ? 0.000621371f : u == usUnit.yd ? 1.09361f : u == usUnit.ft ? 3.28084f : u == usUnit.inch ? 39.3701f : u == usUnit.mil ? 39370.1f : u == usUnit.microinch ? 39370100f : u == usUnit.angstrom ? 1e10f : u == usUnit.point ? 2845.27559055116f : u == usUnit.nmi ? 0.000539957f : u == usUnit.fathom ? 0.546807f : u == usUnit.ua ? 6.68459e-12f : u == usUnit.ly ? 1.057e-16f : u == usUnit.pc ? 3.24078e-17f : float.NaN;//length
				case siUnit.km: case siUnit.cm: case siUnit.mm: case siUnit.um: case siUnit.nm: case siUnit.pm: return convert(siUnit.m, usUnit) / convert(siUnit.m, siUnit);
				case siUnit.m2: return u == usUnit.mi2 ? 3.86102e-7f : u == usUnit.acre ? 0.000247105f : u == usUnit.yd2 ? 1.19599f : u == usUnit.ft2 ? 10.7639f : u == usUnit.in2 ? 1550f : float.NaN;//area ha=hectare
				case siUnit.km2: case siUnit.ha: case siUnit.cm2: case siUnit.mm2: return convert(siUnit.m2, usUnit) / convert(siUnit.m2, siUnit);
				case siUnit.m3: return u == usUnit.in3 ? 61023.7f : u == usUnit.ft3 ? 35.3147f : u == usUnit.yd3 ? 1.30795f : u == usUnit.acre_ft ? 0.000810714f : u == usUnit.bbl ? 6.28981f : u == usUnit.bu ? 28.3776f : u == usUnit.gal ? 264.172f : u == usUnit.qt ? 1056.69f : u == usUnit.pt ? 2113.38f : u == usUnit.cup ? 4226.75f : u == usUnit.tbsp ? 67628f : u == usUnit.tsp ? 202884f : u == usUnit.fl_oz ? 33814f : float.NaN;//Volume  bbl=oil-barrel, bu=bushel
				case siUnit.cm3: case siUnit.mm3: case siUnit.l: case siUnit.ml: return convert(siUnit.m3, usUnit) / convert(siUnit.m3, siUnit);
				case siUnit.mps: return u == usUnit.mph ? 2.23694f : u == usUnit.kn ? 1.94384f : u == usUnit.ftps ? 3.28084f : float.NaN;//speed: kn = knot
				case siUnit.kph: case siUnit.kn: return convert(siUnit.mps, usUnit) / convert(siUnit.mps, siUnit);
				case siUnit.mps2: return u == usUnit.ftps2 ? 3.28084f : u == usUnit.inps2 ? 39.3701f : float.NaN;//acceleration
				case siUnit.m3ps: return u == usUnit.ft3ps ? 35.3147f : u == usUnit.ft3pmin ? 2118.88f : u == usUnit.yd3pmin ? 78.477f : u == usUnit.galpmin ? 15850.3f : u == usUnit.galpday ? 2.282e+7f : float.NaN;//flow rate
				case siUnit.lps: case siUnit.lpd: return convert(siUnit.m3ps, usUnit) / convert(siUnit.m3ps, siUnit);
				case siUnit.kmpl: return u == usUnit.mpg ? 2.35215f : float.NaN;//fuel efficiency
				case siUnit.kg: return u == usUnit.ton ? 0.00110231f : u == usUnit.lb ? 2.20462f : u == usUnit.oz ? 35.274f : u == usUnit.grain ? 15432.4f : float.NaN;//weight
				case siUnit.ton: case siUnit.g: case siUnit.mg: case siUnit.ug: return convert(siUnit.kg, usUnit) / convert(siUnit.kg, siUnit);
				case siUnit.kg_m: return u == usUnit.lb_ft ? 7.2330138512099f : float.NaN;//moment of mass
				case siUnit.kgpm3: return u == usUnit.tonpyd3 ? 0.000842777f : u == usUnit.lbpft3 ? 0.062428f : float.NaN; //density
				case siUnit.Npm3: case siUnit.Tpm3: return convert(siUnit.kgpm3, usUnit) / convert(siUnit.kgpm3, siUnit);
				case siUnit.gpL: return u == usUnit.lbpgal ? 0.0083454042651525f : u == usUnit.ozpgal ? 0.133526f : float.NaN;//concentration
				case siUnit.kg_mps: return u == usUnit.lb_ftps ? 7.23301385f : float.NaN;//momentum
				case siUnit.kg_m2: return u == usUnit.lb_ft2 ? 23.73036f : float.NaN;//moment of inertia
				case siUnit.N: return u == usUnit.lbf ? 0.224809f : u == usUnit.poundal ? 7.23301f : float.NaN;//force
				case siUnit.Nm: return u == usUnit.lbf_ft ? 0.737562f : u == usUnit.lbf_in ? 8.850745792f : float.NaN;//torque
				case siUnit.kPa: return u == usUnit.psf ? 20.885434273039f : u == usUnit.torr ? 7.50062f : u == usUnit.psi ? 0.145038f : u == usUnit.atm ? 0.00986923f : u == usUnit.bar ? 0.01f : u == usUnit.mbar ? 10f : u == usUnit.ksi ? 0.0001450377f : float.NaN;//pressure
				case siUnit.Pa: case siUnit.MPa: case siUnit.GPa: case siUnit.Npm2: return convert(siUnit.kPa, usUnit) / convert(siUnit.kPa, siUnit);
				case siUnit.mPa_s: return u == usUnit.centipoise ? 1f : float.NaN;//viscosity (dynamic)
				case siUnit.mm2ps: return u == usUnit.centistokes ? 1f : float.NaN;//Viscosity (kinematic)
				case siUnit.kJ: return u == usUnit.kWh ? 0.00027777777777778f : u == usUnit.cal ? 238.8458966275f : u == usUnit.Cal ? 0.2388458966275f : u == usUnit.BTU ? 0.94708628903179f : u == usUnit.therm ? 9.4708628903179E-6f : u == usUnit.hph ? 0.0003725f : u == usUnit.ft_lbf ? 737.562f : float.NaN; //Energy, work, heat
				case siUnit.J: case siUnit.MJ: return convert(siUnit.kJ, usUnit) / convert(siUnit.kJ, siUnit);
				case siUnit.W: return u == usUnit.BTUps ? 0.000947817f : u == usUnit.BTUphr ? 3.412142f : u == usUnit.hp ? 0.00134102f : u == usUnit.ft_lbfps ? 0.737562149f : float.NaN;//Power
				case siUnit.kW: return convert(siUnit.W, usUnit) / convert(siUnit.W, siUnit);
				case siUnit.Wpm_K: return u == usUnit.BTU_inphr_ft2F ? 6.933471263f : float.NaN;//Thermal conductivity
				case siUnit.Wpm2_K: return u == usUnit.BTUphrft2F ? 0.176110194f : float.NaN;//Coefficient of heat transfer
				case siUnit.kJpK: return u == usUnit.BTUpF ? 0.526564938f : float.NaN;//Heat capacity
				case siUnit.kJpkg_K: return u == usUnit.BTUplbF ? 0.238845897f : float.NaN;//Specific heat capacity 
				case siUnit.Apm: return u == usUnit.oersted ? 0.012566371f : float.NaN;//Magnetic field strength
				case siUnit.nWb: return u == usUnit.maxwell ? 0.1f : float.NaN;//Magnetic flux
				case siUnit.mT: return u == usUnit.gauss ? 10f : float.NaN;//Magnetic flux density
				case siUnit.Coulomb: return u == usUnit.Ah ? 0.000277778f : float.NaN;//Electric charge
				case siUnit.cdpm2: return u == usUnit.lambert ? 0.000314159f : u == usUnit.cdpin2 ? 0.00064516f : float.NaN;//Luminance
				case siUnit.lx: return u == usUnit.fc ? 0.092903044f : u == usUnit.phot ? 0.0001f : float.NaN;// Luminous exitance, Illuminance
				case siUnit.MBq: return u == usUnit.Curie ? 2.7027E-05f : float.NaN;//Activity (of a radionuclide)
			}
			return float.NaN;
		}
		public static double dconvert(siUnit siUnit, usUnit usUnit)
		{
			var u = usUnit;
			switch (siUnit)
			{
				case siUnit.m: return u == usUnit.mi ? 0.000621371192 : u == usUnit.yd ? 1.09361f : u == usUnit.ft ? 3.28084f : u == usUnit.inch ? 39.3701f : u == usUnit.mil ? 39370.1f : u == usUnit.microinch ? 39370100f : u == usUnit.angstrom ? 1e10f : u == usUnit.point ? 2845.27559055116f : u == usUnit.nmi ? 0.000539957f : u == usUnit.fathom ? 0.546807f : u == usUnit.ua ? 6.68459e-12f : u == usUnit.ly ? 1.057e-16f : u == usUnit.pc ? 3.24078e-17f : float.NaN;//length
				case siUnit.km: case siUnit.cm: case siUnit.mm: case siUnit.um: case siUnit.nm: case siUnit.pm: return convert(siUnit.m, usUnit) / convert(siUnit.m, siUnit);
				case siUnit.m2: return u == usUnit.mi2 ? 3.86102e-7f : u == usUnit.acre ? 0.000247105f : u == usUnit.yd2 ? 1.19599f : u == usUnit.ft2 ? 10.7639f : u == usUnit.in2 ? 1550f : float.NaN;//area ha=hectare
				case siUnit.km2: case siUnit.ha: case siUnit.cm2: case siUnit.mm2: return convert(siUnit.m2, usUnit) / convert(siUnit.m2, siUnit);
				case siUnit.m3: return u == usUnit.in3 ? 61023.7f : u == usUnit.ft3 ? 35.3147f : u == usUnit.yd3 ? 1.30795f : u == usUnit.acre_ft ? 0.000810714f : u == usUnit.bbl ? 6.28981f : u == usUnit.bu ? 28.3776f : u == usUnit.gal ? 264.172f : u == usUnit.qt ? 1056.69f : u == usUnit.pt ? 2113.38f : u == usUnit.cup ? 4226.75f : u == usUnit.tbsp ? 67628f : u == usUnit.tsp ? 202884f : u == usUnit.fl_oz ? 33814f : float.NaN;//Volume  bbl=oil-barrel, bu=bushel
				case siUnit.cm3: case siUnit.mm3: case siUnit.l: case siUnit.ml: return convert(siUnit.m3, usUnit) / convert(siUnit.m3, siUnit);
				case siUnit.mps: return u == usUnit.mph ? 2.23694f : u == usUnit.kn ? 1.94384f : u == usUnit.ftps ? 3.28084f : float.NaN;//speed: kn = knot
				case siUnit.kph: case siUnit.kn: return convert(siUnit.mps, usUnit) / convert(siUnit.mps, siUnit);
				case siUnit.mps2: return u == usUnit.ftps2 ? 3.28084f : u == usUnit.inps2 ? 39.3701f : float.NaN;//acceleration
				case siUnit.m3ps: return u == usUnit.ft3ps ? 35.3147f : u == usUnit.ft3pmin ? 2118.88f : u == usUnit.yd3pmin ? 78.477f : u == usUnit.galpmin ? 15850.3f : u == usUnit.galpday ? 2.282e+7f : float.NaN;//flow rate
				case siUnit.lps: case siUnit.lpd: return convert(siUnit.m3ps, usUnit) / convert(siUnit.m3ps, siUnit);
				case siUnit.kmpl: return u == usUnit.mpg ? 2.35215f : float.NaN;//fuel efficiency
				case siUnit.kg: return u == usUnit.ton ? 0.00110231f : u == usUnit.lb ? 2.20462f : u == usUnit.oz ? 35.274f : u == usUnit.grain ? 15432.4f : float.NaN;//weight
				case siUnit.ton: case siUnit.g: case siUnit.mg: case siUnit.ug: return convert(siUnit.kg, usUnit) / convert(siUnit.kg, siUnit);
				case siUnit.kg_m: return u == usUnit.lb_ft ? 7.2330138512099f : float.NaN;//moment of mass
				case siUnit.kgpm3: return u == usUnit.tonpyd3 ? 0.000842777f : u == usUnit.lbpft3 ? 0.062428f : float.NaN; //density
				case siUnit.Npm3: case siUnit.Tpm3: return convert(siUnit.kgpm3, usUnit) / convert(siUnit.kgpm3, siUnit);
				case siUnit.gpL: return u == usUnit.lbpgal ? 0.0083454042651525f : u == usUnit.ozpgal ? 0.133526f : float.NaN;//concentration
				case siUnit.kg_mps: return u == usUnit.lb_ftps ? 7.23301385f : float.NaN;//momentum
				case siUnit.kg_m2: return u == usUnit.lb_ft2 ? 23.73036f : float.NaN;//moment of inertia
				case siUnit.N: return u == usUnit.lbf ? 0.224809f : u == usUnit.poundal ? 7.23301f : float.NaN;//force
				case siUnit.Nm: return u == usUnit.lbf_ft ? 0.737562f : u == usUnit.lbf_in ? 8.850745792f : float.NaN;//torque
				case siUnit.kPa: return u == usUnit.psf ? 20.885434273039f : u == usUnit.torr ? 7.50062f : u == usUnit.psi ? 0.145038f : u == usUnit.atm ? 0.00986923f : u == usUnit.bar ? 0.01f : u == usUnit.mbar ? 10f : u == usUnit.ksi ? 0.0001450377f : float.NaN;//pressure
				case siUnit.Pa: case siUnit.MPa: case siUnit.GPa: case siUnit.Npm2: return convert(siUnit.kPa, usUnit) / convert(siUnit.kPa, siUnit);
				case siUnit.mPa_s: return u == usUnit.centipoise ? 1f : float.NaN;//viscosity (dynamic)
				case siUnit.mm2ps: return u == usUnit.centistokes ? 1f : float.NaN;//Viscosity (kinematic)
				case siUnit.kJ: return u == usUnit.kWh ? 0.00027777777777778f : u == usUnit.cal ? 238.8458966275f : u == usUnit.Cal ? 0.2388458966275f : u == usUnit.BTU ? 0.94708628903179f : u == usUnit.therm ? 9.4708628903179E-6f : u == usUnit.hph ? 0.0003725f : u == usUnit.ft_lbf ? 737.562f : float.NaN; //Energy, work, heat
				case siUnit.J: case siUnit.MJ: return convert(siUnit.kJ, usUnit) / convert(siUnit.kJ, siUnit);
				case siUnit.W: return u == usUnit.BTUps ? 0.000947817f : u == usUnit.BTUphr ? 3.412142f : u == usUnit.hp ? 0.00134102f : u == usUnit.ft_lbfps ? 0.737562149f : float.NaN;//Power
				case siUnit.kW: return convert(siUnit.W, usUnit) / convert(siUnit.W, siUnit);
				case siUnit.Wpm_K: return u == usUnit.BTU_inphr_ft2F ? 6.933471263f : float.NaN;//Thermal conductivity
				case siUnit.Wpm2_K: return u == usUnit.BTUphrft2F ? 0.176110194f : float.NaN;//Coefficient of heat transfer
				case siUnit.kJpK: return u == usUnit.BTUpF ? 0.526564938f : float.NaN;//Heat capacity
				case siUnit.kJpkg_K: return u == usUnit.BTUplbF ? 0.238845897f : float.NaN;//Specific heat capacity 
				case siUnit.Apm: return u == usUnit.oersted ? 0.012566371f : float.NaN;//Magnetic field strength
				case siUnit.nWb: return u == usUnit.maxwell ? 0.1f : float.NaN;//Magnetic flux
				case siUnit.mT: return u == usUnit.gauss ? 10f : float.NaN;//Magnetic flux density
				case siUnit.Coulomb: return u == usUnit.Ah ? 0.000277778f : float.NaN;//Electric charge
				case siUnit.cdpm2: return u == usUnit.lambert ? 0.000314159f : u == usUnit.cdpin2 ? 0.00064516f : float.NaN;//Luminance
				case siUnit.lx: return u == usUnit.fc ? 0.092903044f : u == usUnit.phot ? 0.0001f : float.NaN;// Luminous exitance, Illuminance
				case siUnit.MBq: return u == usUnit.Curie ? 2.7027E-05f : float.NaN;//Activity (of a radionuclide)
			}
			return float.NaN;
		}
		public static float convert(usUnit usUnit0, usUnit usUnit1)
		{
			int us0 = (int)usUnit0, us1 = (int)usUnit1;
			if (us0 == us1) return 1;
			var u = usUnit1;
			switch (usUnit0)
			{
				case usUnit.mi: return u == usUnit.yd ? 1760f : u == usUnit.ft ? 5280f : u == usUnit.inch ? 63360f : u == usUnit.mil ? 6.336e+7f : u == usUnit.microinch ? 6.336e+10f : u == usUnit.angstrom ? 1.609e+13f : u == usUnit.point ? 4579027.2f : u == usUnit.nmi ? 0.868976f : u == usUnit.fathom ? 880f : u == usUnit.ua ? 1.07578e-8f : u == usUnit.ly ? 1.70108e-13f : u == usUnit.pc ? 5.21553e-14f : float.NaN; //distance
				case usUnit.yd: case usUnit.ft: case usUnit.inch: case usUnit.mil: case usUnit.microinch: case usUnit.angstrom: case usUnit.point: case usUnit.nmi: case usUnit.fathom: case usUnit.ua: case usUnit.ly: case usUnit.pc: return convert(usUnit.mi, usUnit1) / convert(usUnit.mi, usUnit0);
				case usUnit.mi2: return u == usUnit.acre ? 640f : u == usUnit.yd2 ? 3.098e+6f : u == usUnit.ft2 ? 2.788e+7f : u == usUnit.in2 ? 4.014e+9f : float.NaN;//area ha=hectare
				case usUnit.acre: case usUnit.yd2: case usUnit.ft2: case usUnit.in2: return convert(usUnit.mi2, usUnit1) / convert(usUnit.mi2, usUnit0);
				case usUnit.ft3: return u == usUnit.in3 ? 1728f : u == usUnit.yd3 ? 0.037037f : u == usUnit.acre_ft ? 2.29569e-5f : u == usUnit.bbl ? 0.178108f : u == usUnit.bu ? 0.803564f : u == usUnit.gal ? 7.48052f : u == usUnit.qt ? 29.9221f : u == usUnit.pt ? 59.8442f : u == usUnit.cup ? 119.688f : u == usUnit.tbsp ? 1915.01f : u == usUnit.tsp ? 5745.04f : u == usUnit.fl_oz ? 957.506f : float.NaN;//Volume bbl=oil-barrel, bu=bushel
				case usUnit.in3: case usUnit.yd3: case usUnit.acre_ft: case usUnit.bbl: case usUnit.bu: case usUnit.gal: case usUnit.qt: case usUnit.pt: case usUnit.cup: case usUnit.tbsp: case usUnit.tsp: case usUnit.fl_oz: return convert(usUnit.ft3, usUnit1) / convert(usUnit.ft3, usUnit0);
				case usUnit.mph: return u == usUnit.ftps ? 1.46667f : u == usUnit.kn ? 0.868976f : float.NaN;//speed
				case usUnit.ftps: case usUnit.kn: return convert(usUnit.mph, usUnit1) / convert(usUnit.mph, usUnit0);
				case usUnit.ftps2: return u == usUnit.inps2 ? 12f : float.NaN; //acceleration
				case usUnit.inps2: return convert(usUnit.ftps2, usUnit1) / convert(usUnit.ftps2, usUnit0);
				case usUnit.ft3ps: return u == usUnit.ft3pmin ? 60f : u == usUnit.yd3pmin ? 2.22222f : u == usUnit.galpmin ? 448.831f : u == usUnit.galpday ? 646317f : float.NaN;//flow rate
				case usUnit.ft3pmin: case usUnit.yd3pmin: case usUnit.galpmin: case usUnit.galpday: return convert(usUnit.ft3ps, usUnit1) / convert(usUnit.ft3ps, usUnit0);
				case usUnit.lb: return u == usUnit.ton ? 0.0005f : u == usUnit.oz ? 16f : u == usUnit.grain ? 7000f : float.NaN;//weight
				case usUnit.ton: case usUnit.oz: case usUnit.grain: return convert(usUnit.lb, usUnit1) / convert(usUnit.lb, usUnit0);
				case usUnit.lbpft3: return u == usUnit.tonpyd3 ? 0.0135f : float.NaN;//density
				case usUnit.tonpyd3: return convert(usUnit.lbpft3, usUnit1) / convert(usUnit.lbpft3, usUnit0);
				case usUnit.lbpgal: return u == usUnit.ozpgal ? 16f : float.NaN;//concentration
				case usUnit.ozpgal: return convert(usUnit.lbpgal, usUnit1) / convert(usUnit.lbpgal, usUnit0);
				case usUnit.lbf: return u == usUnit.poundal ? 32.174f : float.NaN;//force
				case usUnit.poundal: return convert(usUnit.lbf, usUnit1) / convert(usUnit.lbf, usUnit0);
				case usUnit.lbf_ft: return u == usUnit.lbf_in ? 12f : float.NaN;//torque
				case usUnit.lbf_in: return convert(usUnit.lbf_ft, usUnit1) / convert(usUnit.lbf_ft, usUnit0);
				case usUnit.psf: return u == usUnit.torr ? 0.35913147550511f : u == usUnit.psi ? 0.006944443f : u == usUnit.atm ? 0.00047254f : u == usUnit.bar ? 0.0004788026f : u == usUnit.mbar ? 0.47880258888889f : u == usUnit.ksi ? 0.00000694444444446f : float.NaN;//pressure
				case usUnit.torr: case usUnit.psi: case usUnit.atm: case usUnit.bar: case usUnit.mbar: case usUnit.ksi: return convert(usUnit.psf, usUnit1) / convert(usUnit.psf, usUnit0);
				case usUnit.kWh: return u == usUnit.cal ? 860420.65f : u == usUnit.Cal ? 860.421f : u == usUnit.BTU ? 3412.14f : u == usUnit.therm ? 0.0341296f : u == usUnit.hph ? 1.34102208960f : u == usUnit.ft_lbf ? 2.655e+6f : float.NaN;//Energy, work, heat
				case usUnit.cal: case usUnit.Cal: case usUnit.BTU: case usUnit.therm: case usUnit.hph: case usUnit.ft_lbf: return convert(usUnit.kWh, usUnit1) / convert(usUnit.kWh, usUnit0);
				case usUnit.hp: return u == usUnit.BTUps ? 0.70707157216924f : u == usUnit.BTUphr ? 2544.433222210f : u == usUnit.ft_lbfps ? 550f : float.NaN;//Power
				case usUnit.BTUps: case usUnit.BTUphr: case usUnit.ft_lbfps: return convert(usUnit.hp, usUnit1) / convert(usUnit.hp, usUnit0);
				case usUnit.lambert: return u == usUnit.cdpin2 ? 2.05361f : float.NaN;//Luminance
				case usUnit.cdpin2: return convert(usUnit.lambert, usUnit1) / convert(usUnit.lambert, usUnit0);
				case usUnit.lmpft2: return u == usUnit.phot ? 0.001076391f : float.NaN;// Luminous exitance
				case usUnit.phot: return convert(usUnit.lmpft2, usUnit1) / convert(usUnit.lmpft2, usUnit0);
			}
			return float.NaN;
		}
		public static float convert(siUnit siUnit0, siUnit siUnit1)
		{
			int si0 = (int)siUnit0, si1 = (int)siUnit1;
			if (si0 == si1) return 1;
			var u = siUnit1;
			switch (siUnit0)
			{
				case siUnit.m: return u == siUnit.km ? 1e-3f : u == siUnit.cm ? 100 : u == siUnit.mm ? 1000f : u == siUnit.um ? 1e6f : u == siUnit.nm ? 1e9f : u == siUnit.pm ? 1e12f : float.NaN;//length
				case siUnit.km: case siUnit.cm: case siUnit.mm: case siUnit.um: case siUnit.nm: case siUnit.pm: return convert(siUnit.m, siUnit1) / convert(siUnit.m, siUnit0);
				case siUnit.m2: return u == siUnit.km2 ? 1e-6f : u == siUnit.ha ? 0.0001f : u == siUnit.cm2 ? 10000f : u == siUnit.mm2 ? 1000000f : float.NaN;//area ha=hectare
				case siUnit.km2: case siUnit.ha: case siUnit.cm2: case siUnit.mm2: return convert(siUnit.m2, siUnit1) / convert(siUnit.m2, siUnit0);
				case siUnit.m3: return u == siUnit.cm3 ? 1000000f : u == siUnit.mm3 ? 1e+9f : u == siUnit.l ? 1000f : u == siUnit.ml ? 1000000f : float.NaN;//Volume
				case siUnit.cm3: case siUnit.mm3: case siUnit.l: case siUnit.ml: return convert(siUnit.m3, siUnit1) / convert(siUnit.m3, siUnit0);
				case siUnit.mps: return u == siUnit.kph ? 3.6f : u == siUnit.kn ? 1.94384f : float.NaN;//speed: kn = knot
				case siUnit.kph: case siUnit.kn: return convert(siUnit.mps, siUnit1) / convert(siUnit.mps, siUnit0);
				case siUnit.m3ps: return u == siUnit.lps ? 1000f : u == siUnit.lpd ? 8.64e+7f : float.NaN;//flow rate
				case siUnit.lps: case siUnit.lpd: return convert(siUnit.m3ps, siUnit1) / convert(siUnit.m3ps, siUnit0);
				case siUnit.kg: return u == siUnit.ton ? 0.001f : u == siUnit.g ? 1000f : u == siUnit.mg ? 1e6f : u == siUnit.ug ? 1e9f : float.NaN;//weight
				case siUnit.ton: case siUnit.g: case siUnit.mg: case siUnit.ug: return convert(siUnit.kg, siUnit1) / convert(siUnit.kg, siUnit0);
				case siUnit.kgpm3: return u == siUnit.Tpm3 ? 0.001f : u == siUnit.Npm3 ? 9.80665f : float.NaN;//density
				case siUnit.Npm3: case siUnit.Tpm3: return convert(siUnit.kgpm3, siUnit1) / convert(siUnit.kgpm3, siUnit0);
				case siUnit.kPa: return u == siUnit.Pa || u == siUnit.Npm2 ? 1e6f : u == siUnit.MPa ? 1 : u == siUnit.GPa ? 1e-3f : float.NaN;//pressure
				case siUnit.Pa: case siUnit.MPa: case siUnit.GPa: case siUnit.Npm2: return convert(siUnit.kPa, siUnit1) / convert(siUnit.kPa, siUnit0);
				case siUnit.J: return u == siUnit.kJ ? 1e-3f : u == siUnit.MJ ? 1e-6f : float.NaN;//Energy, work, heat 
				case siUnit.kJ: case siUnit.MJ: return convert(siUnit.J, siUnit1) / convert(siUnit.J, siUnit0);
				case siUnit.W: return u == siUnit.kW ? 1e-3f : float.NaN;//Power
				case siUnit.kW: return convert(siUnit.W, siUnit1) / convert(siUnit.W, siUnit0);
			}
			return float.NaN;
		}
		public static float convert(Unit Unit0, Unit Unit1)
		{
			int si0 = (int)Unit0, si1 = (int)Unit1;
			if (si0 == si1) return 1;
			var u = Unit1;
			switch (Unit0)
			{
				case Unit.s: return u == Unit.ns ? 1e+9f : u == Unit.us ? 1e+6f : u == Unit.ms ? 1e+3f : u == Unit.min ? 0.0166667f : u == Unit.hr ? 0.000277778f : u == Unit.day ? 1.15741e-5f : u == Unit.week ? 1.65344e-6f : u == Unit.month ? 3.80517e-7f : u == Unit.year ? 3.17098e-8f : float.NaN;
				case Unit.ns: case Unit.us: case Unit.ms: case Unit.min: case Unit.hr: case Unit.day: case Unit.week: case Unit.month: case Unit.year: return convert(Unit.s, Unit1) / convert(Unit.s, Unit0);
				case Unit.deg: return u == Unit.rad ? 0.0174533f : float.NaN;
				//case Unit.deg_per_sec: return u == Unit.rad_per_sec ? 0.0174533f : u == Unit.rps ? 1/6f : u == Unit.rpm ? 1/360f : 1;
				case Unit.deg_per_sec: return u == Unit.rad_per_sec ? 0.0174533f : u == Unit.rps ? 1 / 360f : u == Unit.rpm ? 1 / 6f : 1;
				case Unit.rad_per_sec: case Unit.rpm: case Unit.rps: return convert(Unit.deg_per_sec, Unit1) / convert(Unit.deg_per_sec, Unit0);
				case Unit.rad: return convert(Unit.deg, Unit1) / convert(Unit.deg, Unit0);
				case Unit.Byte: return u == Unit.bit ? 8f : u == Unit.KB ? 1e-3f : u == Unit.MB ? 1e-6f : u == Unit.GB ? 1e-9f : u == Unit.TB ? 1e-12f : u == Unit.PB ? 1e-16f : float.NaN;
				case Unit.bit: case Unit.KB: case Unit.MB: case Unit.GB: case Unit.TB: case Unit.PB: return convert(Unit.Byte, Unit1) / convert(Unit.Byte, Unit0);
				case Unit.Bps: return u == Unit.bps ? 8f : u == Unit.Kbps ? 8e-3f : u == Unit.Mbps ? 8e-6f : u == Unit.Gbps ? 8e-9f : u == Unit.Tbps ? 8e-12f : u == Unit.Pbps ? 8e-15f : u == Unit.KBps ? 1e-3f : u == Unit.MBps ? 1e-6f : u == Unit.GBps ? 1e-9f : u == Unit.TBps ? 1e-12f : u == Unit.PBps ? 1e-15f : float.NaN;
				case Unit.bps: case Unit.Kbps: case Unit.Mbps: case Unit.Gbps: case Unit.Tbps: case Unit.Pbps: case Unit.KBps: case Unit.MBps: case Unit.GBps: case Unit.TBps: case Unit.PBps: return convert(Unit.Bps, Unit1) / convert(Unit.Bps, Unit0);
				case Unit.Hz: return u == Unit.kHz ? 1e-3f : u == Unit.MHz ? 1e-6f : u == Unit.GHz ? 1e-9f : u == Unit.THz ? 1e-12f : float.NaN;
				case Unit.kHz: case Unit.MHz: case Unit.GHz: case Unit.THz: return convert(Unit.Hz, Unit1) / convert(Unit.Hz, Unit0);
				case Unit.FLOPS: return u == Unit.kFLOPS ? 1e-3f : u == Unit.MFLOPS ? 1e-6f : u == Unit.GFLOPS ? 1e-9f : u == Unit.TFLOPS ? 1e-12f : u == Unit.PFLOPS ? 1e-15f : u == Unit.EFLOPS ? 1e-18f : u == Unit.ZFLOPS ? 1e-21f : u == Unit.YFLOPS ? 1e-24f : float.NaN;
				case Unit.kFLOPS: case Unit.MFLOPS: case Unit.GFLOPS: case Unit.TFLOPS: case Unit.PFLOPS: case Unit.EFLOPS: case Unit.ZFLOPS: case Unit.YFLOPS: return convert(Unit.FLOPS, Unit1) / convert(Unit.FLOPS, Unit0);
			}
			//return float.NaN;
			return 1;
		}

		public bool isInGroup(params UI_TreeGroup[] grps)
		{
			foreach (var grp in grps) if (treeGroup_parent == grp || (treeGroup_parent?.isInGroup(grp) ?? false)) return true;
			return false;
		}
		public bool isNotInGroup(params UI_TreeGroup[] grps) => !isInGroup(grps);
	}

	public class UI_Element
	{
		public int level = 0, uxml_level = 3;
		public GS gs;
		public _GS _gs;
		public Type _GS_fieldType, _GS_propType, _GS_memberType;
		public MethodInfo methodInfo;
		public MemberInfo memberInfo;
		public VisualElement root;
		public StrBldr uxml = StrBldr();
		public AttGS attGS;
		public bool isExternalLib;
		public bool isInternalLib => !isExternalLib;
		public bool isNull;
		public List<VisualElement> items = new List<VisualElement>();
		public string tree;
	}

	[Serializable]
	public class TreeGroup
	{
		[SerializeField] public bool isChecked;
		public static void UXML_UI_Element(UI_Element e) { UI_TreeGroup.UXML_UI_Element(e); }
	}
	public class TreeGroupEnd { public static void UXML_UI_Element(UI_Element e) { e.level--; e.uxml_level--; } }

	public static class UI_Extensions
	{
		public static float ui_width(this string label) => label != null ? GS.UI_TextWidth(label) : 0;
		public static float ui_width(this Label label) => label?.text?.ui_width() ?? 0;
		public static VisualElement Append(this VisualElement visualElement, params object[] items)
		{
			foreach (var item in items) if (item != null) { if (item is VisualElement) visualElement.Add((VisualElement)item); else visualElement.Append((VisualElement[])item); }
			return visualElement;
		}
		public static VisualElement Style(this VisualElement visualElement, Length height = default, Length width = default, StyleFloat flexGrow = default,
			FlexDirection flexDirection = default, Wrap flexWrap = default, StyleFloat flexShrink = default, TextAnchor unityTextAlign = default,
			DisplayStyle display = default)
		{
			if (height != default) visualElement.style.height = height;
			if (width != default) visualElement.style.width = width;
			if (flexGrow != default) visualElement.style.flexGrow = flexGrow;
			if (flexDirection != default) visualElement.style.flexDirection = flexDirection;
			if (flexWrap != default) visualElement.style.flexWrap = flexWrap;
			if (flexShrink != default) visualElement.style.flexShrink = flexShrink;
			if (unityTextAlign != default) visualElement.style.unityTextAlign = unityTextAlign;
			if (display != default) visualElement.style.display = display;
			return visualElement;
		}
		public static void display(this VisualElement visualElement, bool display) { visualElement.style.display = UI_VisualElement.DisplayIf(display); }
		public static void DisplayIf(this VisualElement visualElement, bool display) { if (visualElement != null && visualElement.style != null) visualElement.style.display = UI_VisualElement.DisplayIf(display); }
		public static void HideIf(this VisualElement visualElement, bool hide) { visualElement.style.display = UI_VisualElement.HideIf(hide); }

		public static bool isKey(this KeyDownEvent evt)
		{
			return evt.keyCode != KeyCode.LeftControl && evt.keyCode != KeyCode.RightControl
					&& evt.keyCode != KeyCode.LeftAlt && evt.keyCode != KeyCode.RightAlt
					&& evt.keyCode != KeyCode.LeftShift && evt.keyCode != KeyCode.RightShift;
		}
		public static bool isDelete(this KeyDownEvent evt) => evt.keyCode == KeyCode.Delete;
		public static bool isInsert(this KeyDownEvent evt) => evt.keyCode == KeyCode.Insert;
		public static bool isUpArrow(this KeyDownEvent evt) => evt.keyCode == KeyCode.UpArrow;
		public static bool isDownArrow(this KeyDownEvent evt) => evt.keyCode == KeyCode.DownArrow;
		public static bool isCut(this KeyDownEvent evt) => evt.ctrlKey && evt.keyCode == KeyCode.X;
		public static bool isCopy(this KeyDownEvent evt) => evt.ctrlKey && evt.keyCode == KeyCode.C;
		public static bool isPaste(this KeyDownEvent evt) => evt.ctrlKey && evt.keyCode == KeyCode.V;
	}
}