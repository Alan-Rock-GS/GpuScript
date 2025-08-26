using System;
using System.Linq;
using System.Reflection;
using UnityEngine.UIElements;
using static GpuScript.GS;

namespace GpuScript
{
	[UxmlElement]
	public partial class UI_enum : UI_VisualElement
	{
		[UxmlAttribute] public string EnumType;
		public void SetLabel(bool is_grid)
		{
			isGrid = is_grid;
			enumType = EnumType == null ? property.PropertyType : EnumType.ToType();
			if (enumType != null)
			{
				enumValue = (Enum)Enum.ToObject(enumType, v);
				dropdownField.choices = Enum.GetNames(enumType).ToList();
				dropdownField.value = enumValue.ToString();
				headerLabel?.HideIf(label.IsEmpty() || isGrid);
				if (dropdownField.value != null) v = Enum.Parse(enumType, dropdownField.value, true).To_uint();
				dropdownField.style.width = ui_width;
			}
		}
		public override bool Init(GS gs, params GS[] gss)
		{
			if (!base.Init(gs, gss) && !isGrid) return false;
			base.Build(label, description, isReadOnly, isGrid, treeGroup_parent?.name);
			headerLabel?.HideIf(label.IsEmpty() || isGrid);
			SetLabel(isGrid);
			return true;
		}
		public UI_enum() : base()
		{
			headerLabel = this.Q<Label>();
			headerLabel.RegisterCallback<MouseEnterEvent>(OnMouseEnter); headerLabel.RegisterCallback<MouseLeaveEvent>(OnMouseLeave);
			dropdownField = this.Q<DropdownField>();
			dropdownField.RegisterCallback<MouseEnterEvent>(OnMouseEnter); dropdownField.RegisterCallback<MouseLeaveEvent>(OnMouseLeave);
			dropdownField.RegisterValueChangedCallback(OnValueChanged);
			RegisterCallback<MouseEnterEvent>(OnMouseEnter); RegisterCallback<MouseLeaveEvent>(OnMouseLeave);
		}
		public static new void UXML(UI_Element e, AttGS att, string name, string label, string typeName)
		{
			UI_VisualElement.UXML(e, att, name, label, className);
			e.uxml.Add($" enum-type=\"{e._GS_fieldType.FullName.Replace("GS+", "+")}\"");
		}
		public static void UXML_UI_grid_member(UI_Element e, MemberInfo m, AttGS att, uint rowI, float width)
		{
			if (att == null) return;
			UXML(e, att, $"{className}_{m.Name}_{rowI + 1}", "", "");
			e.uxml.Add($" is-grid=\"true\" />");
		}
		public Type enumType;
		public Enum enumValue;
		public Label headerLabel;
		public DropdownField dropdownField;
		public override void RegisterGridCallbacks(GS gs, UI_grid grid, int gridRow, int gridCol)
		{
			base.RegisterGridCallbacks(gs, grid, gridRow, gridCol);
			dropdownField.RegisterValueChangedCallback(OnEnumFieldChanged);
		}
		void OnEnumFieldChanged(ChangeEvent<string> evt) => grid_OnValueChanged();
		public static Type Get_Base_Type() => typeof(Enum);
		public static bool IsType(Type type) => type != null && type.IsEnum;
		public new static void _cs_Write(GS gs, StrBldr tData, StrBldr lateUpdate, StrBldr lateUpdate_ValuesChanged,
			StrBldr showIfs, StrBldr onValueChanged, AttGS attGS, string typeStr, string name)
		{
			StackEnums(tData, typeStr, name);
			lateUpdate.Add($"\n    if (UI_{name}.Changed || (uint){name} != UI_{name}.v) {name} = ({typeStr})UI_{name}.v;");
			UI_VisualElement._cs_Write(gs, tData, lateUpdate, lateUpdate_ValuesChanged, showIfs, onValueChanged, attGS, typeStr, name);
		}
		protected override object GetValue(AttGS attGS, PropertyInfo property, GS gs, bool isNull, bool ui_txt_Exists, object v)
		{
			if (attGS != null && attGS.Val != null && isNull && !ui_txt_Exists) v = (uint)Enum.Parse(enumType, attGS.Val.ToString());
			else if (property != null) v = property.GetValue(gs);
			return v;
		}
		public static string className => MethodBase.GetCurrentMethod().DeclaringType.ToString().After("GpuScript.");
		public static void UXML_UI_Element(UI_Element e) { UXML(e, e.attGS, e.memberInfo.Name, e.attGS.Name, className); e.uxml.Add($" />"); }
		public override string label { get => base.label; set { base.label = value; if (headerLabel != null) headerLabel.text = value; } }
		public uint v
		{
			get => enumValue.To_uint();
			set { enumValue = (Enum)Enum.ToObject(enumType, value); if (dropdownField != null) dropdownField.value = Enum.ToObject(enumType, value).ToString(); }
		}
		public override string textString => enumValue.ToString();
		public override float ui_width { get => Enum.GetNames(enumType).Max(a => UI_Component_Width(a)) + 60; set => style.width = dropdownField.style.width = value; }
		public override object v_obj { get => v; set => v = value.To_uint(); }
		public string previousValue;
		void OnValueChanged(ChangeEvent<string> evt)
		{
			previousValue = evt.previousValue;
			dropdownField.value = evt.newValue;
			if (enumType != null) v = Enum.Parse(enumType, dropdownField.value).To_uint();
			gs?.OnValueChanged();
			gs?.lib_parent_gs?.OnValueChanged();
		}
		public override bool Changed { get => dropdownField != null ? dropdownField.value != previousValue : false; set => previousValue = dropdownField != null && !value ? dropdownField.value : null; }
	}
}