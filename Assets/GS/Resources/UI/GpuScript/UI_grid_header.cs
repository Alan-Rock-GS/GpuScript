using System;
using System.Reflection;
using UnityEngine.UIElements;

namespace GpuScript
{
  [UxmlElement]
  public partial class UI_grid_header : UI_VisualElement
  {
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