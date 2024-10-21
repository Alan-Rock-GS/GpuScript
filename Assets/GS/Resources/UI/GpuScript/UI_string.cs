using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

namespace GpuScript
{
  public class UI_string : UI_VisualElement
  {
    public Label headerLabel;
    public TextField textField;
    public override void RegisterGridCallbacks(GS gs, UI_grid grid, int gridRow, int gridCol)
    {
      base.RegisterGridCallbacks(gs, grid, gridRow, gridCol);
      textField.RegisterValueChangedCallback(OnTextFieldChanged);
    }
    //void OnTextFieldChanged(ChangeEvent<string> evt) { gs.OnValueChanged(grid, gridRow, gridCol); }
    void OnTextFieldChanged(ChangeEvent<string> evt) => grid_OnValueChanged();
    public UI_string() : base()
    {
      headerLabel = this.Q<Label>();
      headerLabel.RegisterCallback<MouseEnterEvent>(OnMouseEnter); headerLabel.RegisterCallback<MouseLeaveEvent>(OnMouseLeave);
      textField = this.Q<TextField>();
      textField.isDelayed = true; //RegisterValueChangedCallback only called when user presses enter or gives away focus, with no Escape notification
      textField.RegisterValueChangedCallback(OnValueChanged);
      textField.RegisterCallback<MouseEnterEvent>(OnMouseEnter); textField.RegisterCallback<MouseLeaveEvent>(OnMouseLeave);
      textField.RegisterCallback<KeyDownEvent>(OnKeyDown); textField.RegisterCallback<KeyUpEvent>(OnKeyUp);
      RegisterCallback<MouseEnterEvent>(OnMouseEnter); RegisterCallback<MouseLeaveEvent>(OnMouseLeave);
    }

    //void OnKeyDown(KeyDownEvent evt) { if (evt.ctrlKey && evt.keyCode == KeyCode.V) textField.value = GS.Clipboard.Replace("\t", ",").TrimEnd(); }
    //void OnKeyUp(KeyUpEvent evt) { if (evt.ctrlKey && evt.keyCode == KeyCode.C) GS.Clipboard = GS.Clipboard.Replace(",", "\t"); }
    void OnKeyDown(KeyDownEvent evt) { if (evt.ctrlKey && evt.keyCode == KeyCode.V) textField.value = GS.Clipboard.TrimEnd(); }
    void OnKeyUp(KeyUpEvent evt)
    {
      //if (evt.ctrlKey && evt.keyCode == KeyCode.C) GS.Clipboard = textField.value; //this happens automatically
    }

    public static Type Get_Base_Type() => typeof(string); 
    public static bool IsType(Type type) => type == typeof(string); 
    public new static void _cs_Write(GS gs, StrBldr tData, StrBldr lateUpdate, StrBldr lateUpdate_ValuesChanged,
      StrBldr showIfs, StrBldr onValueChanged, AttGS attGS, string typeStr, string name)
    {
      StackFields(tData, typeStr, name);
      lateUpdate.Add($"\n    if (UI_{name}.Changed || {name} != UI_{name}.v) {{ data.{name} = UI_{name}.v; ValuesChanged = gChanged = true; }}");
      UI_VisualElement._cs_Write(gs, tData, lateUpdate, lateUpdate_ValuesChanged, showIfs, onValueChanged, attGS, typeStr, name);
    }

    public override bool Init(GS gs, params GS[] gss) { if (!base.Init(gs, gss)) return false; v = textField.value; return true; }
    public static string className => MethodBase.GetCurrentMethod().DeclaringType.ToString().After("GpuScript.");
    public static void UXML_UI_Element(UI_Element e) { UXML(e, e.attGS, e.memberInfo.Name, e.attGS.Name, className); e.uxml.Add($" />"); }
    public static new void UXML(UI_Element e, AttGS att, string name, string label, string typeName)
    {
      UI_VisualElement.UXML(e, att, name, label, className);
      if (att.Val != null) e.uxml.Add($" {className}_value=\"{att.Val}\"");
      if (att.isPassword) e.uxml.Add($" UI_string_is_password=\"true\"");

    }
    public static void UXML_UI_grid_member(UI_Element e, MemberInfo m, AttGS att, uint rowI, float width)
    {
      if (att == null) return;
      UXML(e, att, $"{className}_{m.Name}_{rowI + 1}", "", "");
      e.uxml.Add($" UI_isGrid=\"true\" style=\"width: {width}px;\" />");
    }

    public override string label { get => base.label; set { base.label = value; if (headerLabel != null) headerLabel.text = value; } }
    public string previousValue;

    public static implicit operator string(UI_string f) => f.v; 

    string _v = ""; public string v { get => textField != null ? _v = textField.value : _v; set { _v = value; if (textField != null) textField.value = value; } }
    public override string textString => v;
    public override float ui_width { get => base.ui_width; set => style.width = textField.style.width = value; }
    public override object v_obj { get => v; set => v = value.ToString(); }
    void OnValueChanged(ChangeEvent<string> evt)
    {
      previousValue = evt.previousValue;
      v = evt.newValue;
      property?.SetValue(gs, textField.value);
    }
    public override bool Changed { get => textField != null ? textField.value != previousValue : false; set => previousValue = textField != null && !value ? textField.value : ""; }

    //public void Build(string title, string description, string val, bool isReadOnly, bool isGrid, string treeGroup_parent)
    //{
    //  base.Build(title, description, isReadOnly, isGrid, treeGroup_parent);
    //  textField.value = val;
    //  textField.isReadOnly = isReadOnly;
    //  headerLabel?.HideIf(label.IsEmpty() || isGrid);
    //}
    public void Build(string title, string description, string val, bool isReadOnly, bool isPassword, bool isGrid, string treeGroup_parent)
    {
      base.Build(title, description, isReadOnly, isGrid, treeGroup_parent);
      textField.value = val;
      textField.isReadOnly = isReadOnly;
      textField.isPasswordField = isPassword;
      headerLabel?.HideIf(label.IsEmpty() || isGrid);
    }

    public new class UxmlFactory : UxmlFactory<UI_string, UxmlTraits> { }
    public new class UxmlTraits : UI_VisualElement.UxmlTraits
    {
      //UxmlStringAttributeDescription m_string_Val = new UxmlStringAttributeDescription { name = "UI_string_value", defaultValue = "text" }; //or throws Ambiguous error
      UxmlStringAttributeDescription m_string_Val = new UxmlStringAttributeDescription { name = "UI_string_value", defaultValue = "" };
      UxmlBoolAttributeDescription m_string_isPassword = new UxmlBoolAttributeDescription { name = "UI_string_is_password", defaultValue = false };
      public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
      {
        base.Init(ve, bag, cc);
        //((UI_string)ve).Build(m_Label.GetValueFromBag(bag, cc), m_Description.GetValueFromBag(bag, cc), m_string_Val.GetValueFromBag(bag, cc),
        //  m_isReadOnly.GetValueFromBag(bag, cc), m_isGrid.GetValueFromBag(bag, cc), m_TreeGroup_Parent.GetValueFromBag(bag, cc));
        ((UI_string)ve).Build(m_Label.GetValueFromBag(bag, cc), m_Description.GetValueFromBag(bag, cc), m_string_Val.GetValueFromBag(bag, cc),
          m_isReadOnly.GetValueFromBag(bag, cc), m_string_isPassword.GetValueFromBag(bag,cc), 
          m_isGrid.GetValueFromBag(bag, cc), m_TreeGroup_Parent.GetValueFromBag(bag, cc));
      }
    }
  }
}