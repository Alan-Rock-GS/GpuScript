using System;
using System.Reflection;
using UnityEngine.UIElements;

namespace GpuScript
{
  public class UI_bool : UI_VisualElement
  {
    public Label headerLabel;
    public VisualElement toggle_container;
    public Toggle toggle;
    public override void RegisterGridCallbacks(GS gs, UI_grid grid, int gridRow, int gridCol)
    {
      base.RegisterGridCallbacks(gs, grid, gridRow, gridCol);
      toggle.UnregisterValueChangedCallback(On_UI_bool_Changed);
      toggle.RegisterValueChangedCallback(On_UI_bool_Changed);
    }
    void On_UI_bool_Changed(ChangeEvent<bool> evt) => grid_OnValueChanged();
    public UI_bool() : base()
    {
      headerLabel = this.Q<Label>();
      headerLabel.RegisterCallback<MouseEnterEvent>(OnMouseEnter);
      headerLabel.RegisterCallback<MouseLeaveEvent>(OnMouseLeave);
      toggle = this.Q<Toggle>();
      toggle.RegisterCallback<MouseEnterEvent>(OnMouseEnter);
      toggle.RegisterCallback<MouseLeaveEvent>(OnMouseLeave);
      toggle.RegisterValueChangedCallback(OnValueChanged);
      toggle_container = this.Q<VisualElement>(nameof(toggle_container));
      RegisterCallback<MouseEnterEvent>(OnMouseEnter);
      RegisterCallback<MouseLeaveEvent>(OnMouseLeave);
    }
    public static Type Get_Base_Type() { return typeof(bool); }
    public static bool IsType(Type type) { return type == typeof(bool); }
    public override bool Init(GS gs, params GS[] gss)
    {
      if (!base.Init(gs, gss)) return false;
      v = toggle.value;
      return true;
    }
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
    public static void UXML_UI_grid_member(UI_Element e, MemberInfo m, AttGS att, uint rowI, float width)
    {
      if (att == null) return;
      UXML(e, att, $"{className}_{m.Name}_{rowI + 1}", "", "");
      e.uxml.Add($" UI_isGrid=\"true\" style=\"width: {width}px;\" />");
    }
    public void Build(string title, string description, bool val, bool isReadOnly, bool isGrid, string treeGroup_parent)
    {
      base.Build(title, description, isReadOnly, isGrid, treeGroup_parent);
      toggle.value = val;
      headerLabel?.HideIf(label.IsEmpty() || isGrid);
    }
    public override string label { get => base.label; set { base.label = value; if (headerLabel != null) headerLabel.text = value; } }
    public bool previousValue;
    public static implicit operator bool(UI_bool f) { return f.v; }
    public static explicit operator int(UI_bool f) { return f.v ? 1 : 0; }
    //public bool v0 = false;
    public bool v
    {
      get => toggle?.value ?? false;
      set
      {
        if (toggle != null) toggle.value = value;
        //v0 = value;
      }
    }
    public override string textString => v.ToString();
    public override object v_obj { get => v; set => v = value.To_bool(); }
    //void OnValueChanged(ChangeEvent<bool> evt)
    //{
    //  previousValue = evt.previousValue;
    //  if (!isReadOnly) { v = evt.newValue; property?.SetValue(gs, v); } else if (toggle.value != v0) toggle.value = v0;
    //}
    void OnValueChanged(ChangeEvent<bool> evt) { previousValue = evt.previousValue; v = evt.newValue; property?.SetValue(gs, v); }

    public override bool Changed { get => v != previousValue; set => previousValue = value ? !v : v; }
    public new class UxmlFactory : UxmlFactory<UI_bool, UxmlTraits> { }
    public new class UxmlTraits : UI_VisualElement.UxmlTraits
    {
      UxmlBoolAttributeDescription m_bool_Val = new UxmlBoolAttributeDescription { name = "UI_bool_value", defaultValue = false };
      public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
      {
        base.Init(ve, bag, cc);
        ((UI_bool)ve).Build(m_Label.GetValueFromBag(bag, cc), m_Description.GetValueFromBag(bag, cc), m_bool_Val.GetValueFromBag(bag, cc),
          m_isReadOnly.GetValueFromBag(bag, cc), m_isGrid.GetValueFromBag(bag, cc), m_TreeGroup_Parent.GetValueFromBag(bag, cc));
      }
    }
  }
}