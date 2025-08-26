using System;
using System.Collections;
using System.Reflection;
using UnityEngine.UIElements;

namespace GpuScript
{
	[UxmlElement]
	public partial class UI_method : UI_VisualElement
	{
    public override bool Init(GS gs, params GS[] gss)
    {
			if (!base.Init(gs, gss) && !isGrid) return false;
			Build(label, description, isReadOnly, isGrid, treeGroup_parent?.name);
      var m = gs.GetType().GetMethod(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
      if (m != null) method = m;
      return true;
    }
		public static void UXML_UI_grid_member(UI_Element e, MemberInfo m, AttGS att, uint rowI, float width)
		{
			if (att == null) return;
			UXML(e, att, $"{className}_{m.Name}_{rowI + 1}", "", "");
      e.uxml.Add($" is-grid=\"true\" label=\"{att.Name}\" style=\"width: {width}px;\" />");
    }
    public Button button;
		public override void RegisterGridCallbacks(GS gs, UI_grid grid, int gridRow, int gridCol)
		{
			base.RegisterGridCallbacks(gs, grid, gridRow, gridCol);
			button.RegisterCallback<ClickEvent>(On_grid_button_Changed);
		}
		void On_grid_button_Changed(ClickEvent evt) => grid_OnValueChanged();
		public UI_method() : base()
		{
			button = this.Q<Button>();
			button.RegisterCallback<ClickEvent>(OnButtonClicked);
			button.RegisterCallback<MouseEnterEvent>(OnMouseEnter); button.RegisterCallback<MouseLeaveEvent>(OnMouseLeave);
			RegisterCallback<MouseEnterEvent>(OnMouseEnter); RegisterCallback<MouseLeaveEvent>(OnMouseLeave);
		}
		private void OnButtonClicked(ClickEvent evt)
		{
			if (isGrid) $"{grid.name}_OnButtonClicked".InvokeMethod(gs, gridRow, gridCol);
			else if (gs != null && method != null)
			{
				if (method.ReturnType == typeof(IEnumerator)) gs.StartCoroutine(method.Name);
				else try { method.Invoke(gs, null); } catch (Exception e) { print($"OnButtonClicked {method?.Name}: {e}"); }
				gs.OnButtonClicked(method.Name);
			}
			else print($"Button {label} clicked");
		}
		public MethodInfo method;
		public static Type Get_Base_Type() => typeof(void);
		public static bool IsType(Type type) => type == typeof(void) || type == typeof(IEnumerator);
		public new static void _cs_Write(GS gs, StrBldr tData, StrBldr lateUpdate, StrBldr lateUpdate_ValuesChanged,
			StrBldr showIfs, StrBldr onValueChanged, AttGS attGS, string typeStr, string name)
		{
			Add_showIf(showIfs, name, attGS);
		}
		public static string className => MethodBase.GetCurrentMethod().DeclaringType.ToString().After("GpuScript.");
		public static void UXML_UI_Element(UI_Element e) { UXML(e, e.attGS, e.memberInfo.Name, e.attGS.Name, className); e.uxml.Add($" />"); }
		public static void UXML(UI_Element e)
		{
			var member = e.memberInfo;
			var att = member.AttGS();
			UI_VisualElement.UXML(e, att, member.Name, att.Name, className);
		}
		public static new void UXML(UI_Element e, AttGS att, string name, string label, string typeName) { UI_VisualElement.UXML(e, att, name, label, className); }
		public override string label { get => base.label; set { base.label = value; if (button != null) button.text = value; } }
		public override float ui_width { get => base.ui_width; set => style.width = button.style.width = value; }
		public override string textString => label;

	}
}