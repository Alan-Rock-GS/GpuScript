using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

namespace GpuScript
{
  public class UI_TreeGroup : UI_VisualElement
  {
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
    public static void UXML_UI_Element(UI_Element e)
    {
      //var ui_treeGroup = e.root?.Query<UI_TreeGroup>(e.matchInfo.name).First();
      //if (e.isNull = ui_treeGroup == null) ui_treeGroup = new UI_TreeGroup() { name = e.matchInfo.name, label = e.matchInfo.Name };
      //UI_VisualElement.UXML(e, e.attGS, e.matchInfo.name, e.matchInfo.Name, className);
      var ui_treeGroup = e.root?.Query<UI_TreeGroup>(e.memberInfo.Name).First();
      if (e.isNull = ui_treeGroup == null) ui_treeGroup = new UI_TreeGroup() { name = e.memberInfo.Name, label = e.attGS.Name };
      UI_VisualElement.UXML(e, e.attGS, e.memberInfo.Name, e.attGS.Name, className);
      var p = e.treeGroup;
      e.treeGroup = ui_treeGroup;
      e.treeGroup.treeGroup_parent = p;

      e.treeGroup.treeGroup_parent_name = p?.name ?? "";

      e.uxml.Add(" UI_TreeGroup_Checked=\"true\"");
      e.uxml.Add($" UI_TreeGroup_Level=\"{e.level}\"");
      e.level++;
      e.uxml_level = e.level + 2;
      e.uxml.Add($" />");
    }

    public void Build(string Label, string Description, string treeGroup_parent, bool isChecked, int level)
    {
      base.Build(Label, Description, false, treeGroup_parent);

      toggle.value = isChecked;
      this.level = level;
      label_Title.text = Label;
      int n3 = level % 3;
      Color color = n3 == 0 ? GS.YELLOW : n3 == 1 ? GS.LIGHT_CYAN : GS.LIGHT_GREEN;
      label_Indent.text = level == 0 ? "" : "".PadLeft(level, '-') + ">";// "-----";
      float label_w = GS.UI_TextWidth(label_Indent.text);
      float title_w = GS.UI_TextWidth(Label) * 1.3f;
      Assign_style(label_Indent, width: label_w, maxwidth: label_w, background_color: color, display: level > 0);
      Assign_style(label_Title, width: title_w, maxwidth: title_w, background_color: color);
      float total_w = 20 + label_w + title_w;
      style.width = total_w;
      var ui_treeGroup = this.Q<VisualElement>("UI_TreeGroup");
      if (ui_treeGroup != null)
        ui_treeGroup.style.width = total_w;
    }
    public int level;
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
          //display = value;
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
    void OnValueChanged(ChangeEvent<bool> evt)
    {
      previousValue = evt.previousValue;
      v = evt.newValue; property?.SetValue(gs, v);
      ShowHide_Tree(this, v);
      gs?.OnValueChanged();
    }

    bool _isShowing = true;
    public bool isShowing { get => _isShowing; set { if (_isShowing != value) { _isShowing = value; Display_Tree(); } } }
    public void ShowHide_Tree(UI_TreeGroup leaf, bool v)
    {
      bool show = leaf.v && v && isExpanded;
      //if (treeGroup_parent != null) leaf.display = show; //bug
      foreach (var c in leaf.ui_children) { c.display = show; if (c is UI_TreeGroup) ShowHide_Tree((UI_TreeGroup)c, show); }
    }
    public void Display_Tree() { ShowHide_Tree(this, v); }
    //public void Display_Tree_If(bool v) { DisplayIf(v); ShowHide_Tree(this, v); }
    public bool isDisplaying => display;
    public void Display_Tree_If(bool v) { display = v; ShowHide_Tree(this, v); }

    public override bool Changed { get => v != previousValue; set => previousValue = value ? !v : v; }
    public bool isExpanded => v && isShowing && (treeGroup_parent?.isExpanded ?? true);

    public new class UxmlFactory : UxmlFactory<UI_TreeGroup, UxmlTraits> { }
    public new class UxmlTraits : UI_VisualElement.UxmlTraits
    {
      UxmlBoolAttributeDescription m_TreeGroup_Checked = new UxmlBoolAttributeDescription { name = "UI_TreeGroup_Checked", defaultValue = false };
      UxmlIntAttributeDescription m_TreeGroup_Level = new UxmlIntAttributeDescription { name = "UI_TreeGroup_Level", defaultValue = 0 };
      public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
      {
        base.Init(ve, bag, cc);
        ((UI_TreeGroup)ve).Build(m_Label.GetValueFromBag(bag, cc), m_Description.GetValueFromBag(bag, cc),
          m_TreeGroup_Parent.GetValueFromBag(bag, cc), m_TreeGroup_Checked.GetValueFromBag(bag, cc), m_TreeGroup_Level.GetValueFromBag(bag, cc));
      }
    }
  }
}