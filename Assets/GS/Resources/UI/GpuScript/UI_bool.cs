using System;
using System.Reflection;
using UnityEngine.UIElements;

namespace GpuScript
{
	[UxmlElement]
	public partial class UI_bool : UI_VisualElement
	{
		public override bool Init(GS gs, params GS[] gss)
		{
			if (!base.Init(gs, gss) && !isGrid) return false;
			base.Build(label, description, isReadOnly, isGrid, treeGroup_parent?.name);
			v = toggle.value;
			return true;
		}
		public static void UXML_UI_grid_member(UI_Element e, MemberInfo m, AttGS att, uint rowI, float width)
		{
			if (att == null) return;
			UXML(e, att, $"{className}_{m.Name}_{rowI + 1}", "", "");
			e.uxml.Add($" is-grid=\"true\" style=\"width: {width}px;\" />");
		}
		public override bool _isGrid { get => base._isGrid; set { base._isGrid = value; headerLabel?.HideIf(label.IsEmpty() || isGrid); } }
		public Label headerLabel;
		public VisualElement toggle_container;
		public Toggle toggle;
		public override void RegisterGridCallbacks(GS gs, UI_grid grid, int gridRow, int gridCol)
		{
			base.RegisterGridCallbacks(gs, grid, gridRow, gridCol);
			toggle.UnregisterValueChangedCallback(On_UI_bool_Changed); toggle.RegisterValueChangedCallback(On_UI_bool_Changed);
		}
		void On_UI_bool_Changed(ChangeEvent<bool> evt) => grid_OnValueChanged();
		public UI_bool() : base()
		{
			headerLabel = this.Q<Label>();
			headerLabel.RegisterCallback<MouseEnterEvent>(OnMouseEnter); headerLabel.RegisterCallback<MouseLeaveEvent>(OnMouseLeave);
			toggle = this.Q<Toggle>();
			toggle.RegisterCallback<MouseEnterEvent>(OnMouseEnter); toggle.RegisterCallback<MouseLeaveEvent>(OnMouseLeave);
			toggle.RegisterValueChangedCallback(OnValueChanged);
			toggle_container = this.Q<VisualElement>(nameof(toggle_container));
			RegisterCallback<MouseEnterEvent>(OnMouseEnter); RegisterCallback<MouseLeaveEvent>(OnMouseLeave);
		}
		public static Type Get_Base_Type() { return typeof(bool); }
		public static bool IsType(Type type) { return type == typeof(bool); }
		public new static void _cs_Write(GS gs, StrBldr tData, StrBldr lateUpdate, StrBldr lateUpdate_ValuesChanged,
			StrBldr showIfs, StrBldr onValueChanged, AttGS attGS, string typeStr, string name)
		{
			StackFields(tData, typeStr, name);
			lateUpdate.Add($"\n    if (UI_{name}.Changed || {name} != UI_{name}.v) {name} = UI_{name}.v;");
			UI_VisualElement._cs_Write(gs, tData, lateUpdate, lateUpdate_ValuesChanged, showIfs, onValueChanged, attGS, typeStr, name);
		}
		public static string className => MethodBase.GetCurrentMethod().DeclaringType.ToString().After("GpuScript.");
		public static void UXML_UI_Element(UI_Element e) { UXML(e, e.attGS, e.memberInfo.Name, e.attGS.Name, className); e.uxml.Add($" />"); }
		public static new void UXML(UI_Element e, AttGS att, string name, string label, string typeName)
		{
			UI_VisualElement.UXML(e, att, name, label, className);
			if (att.Val != null) e.uxml.Add($" {className}_value=\"{att.Val}\"");
		}
		public override string label { get => base.label; set { base.label = value; if (headerLabel != null) headerLabel.text = value; } }
		public bool previousValue;
		public static implicit operator bool(UI_bool f) { return f.v; }
		public static explicit operator int(UI_bool f) { return f.v ? 1 : 0; }
		public bool v { get => toggle?.value ?? false; set { if (toggle != null) toggle.value = value; } }
		public override string textString => v.ToString();
		public override object v_obj { get => v; set => v = value.To_bool(); }
		void OnValueChanged(ChangeEvent<bool> evt) { previousValue = evt.previousValue; v = evt.newValue; property?.SetValue(gs, v); }
		public override bool Changed { get => v != previousValue; set => previousValue = value ? !v : v; }
	}
}