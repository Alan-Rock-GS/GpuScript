using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

namespace GpuScript
{
	[UxmlElement]
	public partial class UI_TreeGroup : UI_VisualElement
	{
		int _level;
		[UxmlAttribute]
		public int level
		{
			get => _level;
			set
			{
				_level = value;
				label_Title.text = label;
				int n3 = level % 3;
				Color color = n3 == 0 ? GS.YELLOW : n3 == 1 ? GS.LIGHT_CYAN : GS.LIGHT_GREEN;
				label_Indent.text = level == 0 ? "" : "".PadLeft(level, '-') + ">";// "-----";
				float label_w = GS.UI_TextWidth(label_Indent.text), title_w = GS.UI_TextWidth(Label) * 1.3f;
				Assign_style(label_Indent, width: label_w, maxwidth: label_w, background_color: color, display: level > 0);
				Assign_style(label_Title, width: title_w, maxwidth: title_w, background_color: color);
				float total_w = 20 + label_w + title_w;
				style.width = total_w;
				var ui_treeGroup = this.Q<VisualElement>("UI_TreeGroup"); if (ui_treeGroup != null) ui_treeGroup.style.width = total_w;
			}
		}
		public static void UXML_UI_Element(UI_Element e)
		{
			var ui_treeGroup = e.root?.Query<UI_TreeGroup>(e.memberInfo.Name).First();
			if (e.isNull = ui_treeGroup == null) ui_treeGroup = new UI_TreeGroup() { name = e.memberInfo.Name, label = e.attGS.Name };
			UI_VisualElement.UXML(e, e.attGS, e.memberInfo.Name, e.attGS.Name, className);
			e.uxml.Add($" is-checked=\"true\" level=\"{e.level}\"");
			e.level++;
			e.uxml_level = e.level + 2;
			e.uxml.Add($" />");
		}
		public Label label_Indent, label_Title;
		public VisualElement toggle_container, tab;
		public Toggle toggle;
		public List<UI_VisualElement> ui_children = new List<UI_VisualElement>();

		public UI_TreeGroup() : base()
		{
			label_Title = this.Q<Label>("Label_Title");
			label_Indent = this.Q<Label>("Label_Indent");
			tab = this.Q("Tab");
			toggle = this.Q<Toggle>();
			toggle_container = this.Q<VisualElement>("toggle_container");

			label_Title?.RegisterCallback<MouseEnterEvent>(OnMouseEnter);
			label_Title?.RegisterCallback<MouseLeaveEvent>(OnMouseLeave);
			label_Title?.RegisterCallback<MouseDownEvent>(OnMouseDown);
			label_Indent?.RegisterCallback<MouseEnterEvent>(OnMouseEnter);
			label_Indent?.RegisterCallback<MouseLeaveEvent>(OnMouseLeave);
			label_Indent?.RegisterCallback<MouseDownEvent>(OnMouseDown);
			RegisterCallback<MouseEnterEvent>(OnMouseEnter);
			RegisterCallback<MouseLeaveEvent>(OnMouseLeave);
			toggle?.RegisterCallback<MouseEnterEvent>(OnMouseEnter);
			toggle?.RegisterCallback<MouseLeaveEvent>(OnMouseLeave);
			toggle?.RegisterValueChangedCallback(OnValueChanged);
		}

		void OnMouseDown(MouseDownEvent evt) { previousValue = v; v = !v; property?.SetValue(gs, v); }
		public static Type Get_Base_Type() => typeof(TreeGroup);
		public static bool IsType(Type type) => type == typeof(TreeGroup);
		public new static void _cs_Write(GS gs, StrBldr tData, StrBldr lateUpdate, StrBldr lateUpdate_ValuesChanged,
			StrBldr showIfs, StrBldr onValueChanged, AttGS attGS, string typeStr, string name)
		{
			StackFields(tData, "bool", name);
			UI_VisualElement._cs_Write(gs, tData, lateUpdate, lateUpdate_ValuesChanged, showIfs, onValueChanged, attGS, typeStr, name);
		}
		public static string className => MethodBase.GetCurrentMethod().DeclaringType.ToString().After("GpuScript.");
		public bool previousValue;
		public static implicit operator bool(UI_TreeGroup f) => f.v;
		public static explicit operator int(UI_TreeGroup f) => f.v ? 1 : 0;
		public bool v
		{
			get => isShowing && (toggle?.value ?? false);
			set
			{
				if (toggle != null)
				{
					toggle.value = value;
					if (treeGroup_parent == null)
					{
						label_Title.style.display = DisplayIf(value);
						label_Indent.style.display = DisplayIf(false);
						toggle_container.style.opacity = value ? 1 : 0.1f;
					}
				}
			}
		}
		public override string textString => label_Title.text;
		public override object v_obj { get => v; set => v = value.To_bool(); }
		void OnValueChanged(ChangeEvent<bool> evt) { previousValue = evt.previousValue; v = evt.newValue; property?.SetValue(gs, v); ShowHide_Tree(this, v); gs?.OnValueChanged(); }
		bool _isShowing = true;
		public bool isShowing { get => _isShowing; set { if (_isShowing != value) { _isShowing = value; Display_Tree(); } } }
		public void ShowHide_Tree(UI_TreeGroup leaf, bool v)
		{
			bool show = leaf.v && v && isExpanded;
			foreach (var c in leaf.ui_children) { c.display = show; if (c is UI_TreeGroup) ShowHide_Tree((UI_TreeGroup)c, show); }
		}
		public void Display_Tree() { ShowHide_Tree(this, v); }
		public bool isDisplaying => display;
		public void Display_Tree_If(bool v) { display = v; ShowHide_Tree(this, v); }
		public override bool Changed { get => v != previousValue; set => previousValue = value ? !v : v; }
		public bool isExpanded => treeGroup_parent == this ? true : isShowing && (toggle?.value ?? false) && (treeGroup_parent?.isExpanded ?? true);
	}
}