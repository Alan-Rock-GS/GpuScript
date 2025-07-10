using System;
using System.Reflection;
using UnityEngine.UIElements;

namespace GpuScript
{
#if NEW_UI
  [UxmlElement]
  public partial class UI_grid_header : UI_VisualElement
  {
#elif !NEW_UI
  public class UI_grid_header : UI_VisualElement
  {
    public void Build(string title, string description, string _siUnit, string _usUnit, string _Unit)
    {
      Build(title, description, false, null);
      siUnit = _siUnit.IsNotEmpty() ? _siUnit.ToEnum<siUnit>() : siUnit.Null;
      usUnit = _usUnit.IsNotEmpty() ? _usUnit.ToEnum<usUnit>() : usUnit.Null;
      Unit = _Unit.IsNotEmpty() ? _Unit.ToEnum<Unit>() : Unit.Null;
      if (usUnit == usUnit.Null && siUnit != siUnit.Null) usUnit = Match(siUnit);
      else if (usUnit != usUnit.Null && siUnit == siUnit.Null) siUnit = Match(usUnit);
      if (unitLabel != null) { unitLabel.text = unit; unitLabel.HideIf(unit.IsEmpty()); }
    }
    public new class UxmlFactory : UxmlFactory<UI_grid_header, UxmlTraits> { }
    public new class UxmlTraits : UI_VisualElement.UxmlTraits
    {
      UxmlStringAttributeDescription m_grid_header_siUnit = new UxmlStringAttributeDescription { name = "UI_grid_header_siUnit", defaultValue = "" };
      UxmlStringAttributeDescription m_grid_header_usUnit = new UxmlStringAttributeDescription { name = "UI_grid_header_usUnit", defaultValue = "" };
      UxmlStringAttributeDescription m_grid_header_Unit = new UxmlStringAttributeDescription { name = "UI_grid_header_Unit", defaultValue = "" };
      public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
      {
        base.Init(ve, bag, cc);
        ((UI_grid_header)ve).Build(m_Label.GetValueFromBag(bag, cc), m_Description.GetValueFromBag(bag, cc),
           m_grid_header_siUnit.GetValueFromBag(bag, cc), m_grid_header_usUnit.GetValueFromBag(bag, cc), m_grid_header_Unit.GetValueFromBag(bag, cc));
      }
    }
#endif //NEW_UI
    public Button headerButton;
    public Label unitLabel;
    public VisualElement container;
    public override float ui_width { get => label.ui_width() + 4 + unitLabel.ui_width(); set => style.width = headerButton.style.width = value; }

    public UI_grid_header() : base() { headerButton = this.Q<Button>(); headerButton.text = label; }
    public override bool Init(GS gs, params GS[] gss)
    {
      if (!base.Init(gs, gss)) return false;
      this.gs = gs;
      headerButton = this.Q<Button>();
      headerButton.text = label;
      headerButton.RegisterCallback<MouseEnterEvent>(OnMouseEnter);
      headerButton.RegisterCallback<MouseLeaveEvent>(OnMouseLeave);
      headerButton.RegisterCallback<ClickEvent>(On_headerButton_click);
      unitLabel = this.Q<Label>(nameof(unitLabel));
      unitLabel.text = unit;
      unitLabel.RegisterCallback<ClickEvent>(On_unitLabel_click);
      return true;
    }

    private void On_headerButton_click(ClickEvent evt)
    {
      print("sort by this column");
    }
    public override void OnUnitsChanged() { if (unitLabel != null) unitLabel.text = unit; }
    void On_unitLabel_click(ClickEvent evt) { GS.siUnits = !GS.siUnits; gs.OnUnitsChanged(); }
    public new static void _cs_Write(GS gs, StrBldr tData, StrBldr lateUpdate, StrBldr lateUpdate_ValuesChanged, StrBldr showIfs, StrBldr onValueChanged, AttGS attGS, string typeStr, string name)
    {
      StackFields(tData, typeStr, name);
      lateUpdate.Add($"\n    if (UI_{name}.Changed || {name} != UI_{name}.v) {name} = UI_{name}.v;");
      UI_VisualElement._cs_Write(gs, tData, lateUpdate, lateUpdate_ValuesChanged, showIfs, onValueChanged, attGS, typeStr, name);
    }
    public static string className => MethodBase.GetCurrentMethod().DeclaringType.ToString().After("GpuScript.");
    public static void UXML_UI_Element(UI_Element e)
    {
      UXML(e, e.attGS, e.memberInfo.Name, e.attGS.Name, className);
      e.uxml.Add($" />");
    }
    public static new void UXML(UI_Element e, AttGS att, string name, string label, string typeName)
    {
      UI_Slider_base.UXML(e, att, name, label, className);
    }
    public static void UXML_UI_grid_member(UI_Element e, MemberInfo m, AttGS att, uint rowI, float width)
    {
      if (att == null) return;
      UXML(e, att, $"{className}_{m.Name}", "", "");
      e.uxml.Add($" style=\"width: {width}px;\" />");
    }
     public override string label { get => base.label; set { base.label = value; if (headerButton != null) headerButton.text = value; } }
  }
}