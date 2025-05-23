using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace GpuScript
{
	public class UI_strings : UI_VisualElement
	{
		public Label headerLabel;
		public DropdownField dropdownField;
		//public uint index => (uint)dropdownField.index;
		public uint index
		{
			get => (uint)dropdownField.index;
      set
      {
        dropdownField.index = (int)value;
        v = dropdownField.text;
        //VisualElement ve = dropdownField.Children().ElementAt(1);
        //var popup = dropdownField.Children().ElementAt(1).Children().ElementAt(0) as BasePopupField<System.String, System.String>;
        ////dropdownField.value = dropdownField.text;
        //dropdownField.label = v;
        //dropdownField.labelElement.text = v;
        //    print($"v = {v}, {ve.Children().ElementAt(0).GetType()}, {ve.Children().ElementAt(1).GetType()}, {popup.check()}");
      }
    }
    public override void RegisterGridCallbacks(GS gs, UI_grid grid, int gridRow, int gridCol)
		{
			base.RegisterGridCallbacks(gs, grid, gridRow, gridCol);
			dropdownField.RegisterValueChangedCallback(OnDropdownFieldChanged);
		}
		//private void OnDropdownFieldChanged(ChangeEvent<string> evt) { gs.OnValueChanged(grid, gridRow, gridCol); }
		void OnDropdownFieldChanged(ChangeEvent<string> evt) => grid_OnValueChanged();
		public UI_strings() : base()
		{
			headerLabel = this.Q<Label>();
			headerLabel.RegisterCallback<MouseEnterEvent>(OnMouseEnter); headerLabel.RegisterCallback<MouseLeaveEvent>(OnMouseLeave);

			dropdownField = this.Q<DropdownField>();
			//dropdownField = new DropdownField("Options", new List<string> { "Option 1", "Option 2", "Option 3" }, 0);
			//dropdownField.choices.Add("Option 4");
			//dropdownField.index = 1; // Option 2
			//dropdownField.value = "Option3";
			////Assert.IsTrue(myDropdown.index == 2);
			dropdownField.RegisterCallback<MouseEnterEvent>(OnMouseEnter); dropdownField.RegisterCallback<MouseLeaveEvent>(OnMouseLeave);
			dropdownField.RegisterValueChangedCallback(OnValueChanged);
			RegisterCallback<MouseEnterEvent>(OnMouseEnter); RegisterCallback<MouseLeaveEvent>(OnMouseLeave);
		}

		public static Type Get_Base_Type() => typeof(strings);
		public static bool IsType(Type type) => type == typeof(strings);

		public new static void _cs_Write(GS gs, StrBldr tData, StrBldr lateUpdate, StrBldr lateUpdate_ValuesChanged,
			StrBldr showIfs, StrBldr onValueChanged, AttGS attGS, string typeStr, string name)
		{
			//StackFields(tData, "strings", name);
			//lateUpdate.Add($"\n    if (UI_{name}.Changed || {name} != UI_{name}.v) {{ data.{name}.v = UI_{name}.v; ValuesChanged = gChanged = true; }}");
			StackFields(tData, "string", name);
			lateUpdate.Add($"\n    if (UI_{name}.Changed || {name} != UI_{name}.v) {{ data.{name} = UI_{name}.v; ValuesChanged = gChanged = true; }}");
			UI_VisualElement._cs_Write(gs, tData, lateUpdate, lateUpdate_ValuesChanged, showIfs, onValueChanged, attGS, typeStr, name);
		}

		public void RefreshChoices()
		{
			if (choiceMethod != null)
			{
				var choices = choiceMethod.Invoke(gs, null);
				if (choices.GetType().IsArray) dropdownField.choices = ((string[])choices).ToList<string>();
				else if (choices.IsList()) dropdownField.choices = (List<string>)choices;
				dropdownField.style.width = ui_width;
			}
		}
		public MethodInfo choiceMethod;
		public override bool Init(GS gs, params GS[] gss)
		{
			if (!base.Init(gs, gss)) return false;
			if (dropdownField.choices.Count == 1)
			{
				if (dropdownField.choices[0].Contains("()"))
				{
					choiceMethod = gs.GetType().GetMethod(dropdownField.choices[0].Before("()"), GS.bindings);
					RefreshChoices();
					//if (choiceMethod != null)
					//{
					//  var choices = choiceMethod.Invoke(gs, null);
					//  if (choices.GetType().IsArray) dropdownField.choices = ((string[])choices).ToList<string>();
					//  else if (choices.IsList()) dropdownField.choices = (List<string>)choices;
					//}
				}
			}
			v = dropdownField.value;

			dropdownField.style.width = ui_width;
			headerLabel.style.width = label.ui_width();
			return true;
		}
		public static string className => MethodBase.GetCurrentMethod().DeclaringType.ToString().After("GpuScript.");
		public static void UXML_UI_Element(UI_Element e) { UXML(e, e.attGS, e.memberInfo.Name, e.attGS.Name, className); e.uxml.Add($" />"); }
		public static new void UXML(UI_Element e, AttGS att, string name, string label, string typeName)
		{
			UI_VisualElement.UXML(e, att, name, label, className);
			if (att.Vals != null) e.uxml.Add(" UI_strings_choices=\"", att.Vals.ToString(), "\"");
			if (att.Val != null) e.uxml.Add(" UI_strings_Val=\"", att.Val.ToString(), "\"");
		}
		public static void UXML_UI_grid_member(UI_Element e, MemberInfo m, AttGS att, uint rowI, float width)
		{
			if (att == null) return;
			UXML(e, att, $"{className}_{m.Name}_{rowI + 1}", "", "");
			e.uxml.Add($" UI_isGrid=\"true\" />");
		}
		public override string label { get => base.label; set { base.label = value; if (headerLabel != null) headerLabel.text = value; } }
		public MethodInfo GetStrings_Method;
		public string[] strings;
		public string v
		{
			get => dropdownField.value;
			set
			{
				if (dropdownField != null)
				{
					dropdownField.value = dropdownField.label = value;
				}
			}
		}
		public override string textString => v;
		public override float ui_width { get => (dropdownField?.choices?.Max(a => a.ui_width()) ?? 100) + 60; set => style.width = dropdownField.style.width = value; }
		public override object v_obj { get => v; set => v = value.ToString(); }
		public string previousValue;
		//void OnValueChanged(ChangeEvent<string> evt) { previousValue = evt.previousValue; dropdownField.value = evt.newValue; if (property != null) property.SetValue(gs, dropdownField.value); }
		void OnValueChanged(ChangeEvent<string> evt) { previousValue = evt.previousValue; dropdownField.value = evt.newValue; SetPropertyValue(dropdownField.value); }
		public override bool Changed { get => dropdownField != null ? dropdownField.value != previousValue : false; set => previousValue = dropdownField != null && !value ? dropdownField.value : null; }

		public void Build(string title, string description, string choices, string val, bool isReadOnly, bool isGrid, string treeGroup_parent)
		{
			base.Build(title, description, isReadOnly, isGrid, treeGroup_parent);
			dropdownField.choices = choices.Split("|").ToList();
			dropdownField.value = val;
			headerLabel?.HideIf(label.IsEmpty() || isGrid);
		}
		public new class UxmlFactory : UxmlFactory<UI_strings, UxmlTraits> { }
		public new class UxmlTraits : UI_VisualElement.UxmlTraits
		{
			UxmlStringAttributeDescription m_strings_choices = new UxmlStringAttributeDescription { name = "UI_strings_choices", defaultValue = "A|B|C" };
			UxmlStringAttributeDescription m_strings_Val = new UxmlStringAttributeDescription { name = "UI_strings_Val", defaultValue = "values" };
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				((UI_strings)ve).Build(m_Label.GetValueFromBag(bag, cc), m_Description.GetValueFromBag(bag, cc),
					m_strings_choices.GetValueFromBag(bag, cc), m_strings_Val.GetValueFromBag(bag, cc),
					m_isReadOnly.GetValueFromBag(bag, cc), m_isGrid.GetValueFromBag(bag, cc), m_TreeGroup_Parent.GetValueFromBag(bag, cc));
			}
		}
	}
}