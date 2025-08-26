using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;
using static GpuScript.GS;

namespace GpuScript
{
  [UxmlElement]
  public partial class UI_string : UI_VisualElement
  {
    [UxmlAttribute] public bool isPassword { get; set; }
    string _val;
    [UxmlAttribute]
    public string val
    {
      get => _val;
      set { _val = value; if (textField != null && isEditor) { textField.value = val; textField.isReadOnly = isReadOnly; textField.isPasswordField = isPassword; } }
    }
    public override bool Init(GS gs, params GS[] gss)
    {
			if (!base.Init(gs, gss) && !isGrid) return false;
			Build(label, description, isReadOnly, isGrid, treeGroup_parent?.name);
      textField.value = val;
      textField.isReadOnly = isReadOnly;
      textField.isPasswordField = isPassword;
      return true;
    }
    public static new void UXML(UI_Element e, AttGS att, string name, string label, string typeName)
    {
      UI_VisualElement.UXML(e, att, name, label, className);
      if (att.Val != null) e.uxml.Add($" val=\"{att.Val}\"");
      if (att.isPassword) e.uxml.Add($" is-password=\"true\"");
    }
    public static void UXML_UI_grid_member(UI_Element e, MemberInfo m, AttGS att, uint rowI, float width)
    {
      if (att == null) return;
      UXML(e, att, $"{className}_{m.Name}_{rowI + 1}", "", "");
      e.uxml.Add($" is-grid=\"true\" style=\"width: {width}px;\" />");
    }
    public override bool _isGrid { get => base._isGrid; set { base._isGrid = value; headerLabel?.HideIf(label.IsEmpty() || isGrid); } }
    public Label headerLabel;
    public TextField textField;
    public override void RegisterGridCallbacks(GS gs, UI_grid grid, int gridRow, int gridCol)
    {
      base.RegisterGridCallbacks(gs, grid, gridRow, gridCol);
      textField.RegisterValueChangedCallback(OnTextFieldChanged);
    }
    void OnTextFieldChanged(ChangeEvent<string> evt) => grid_OnValueChanged();
    public UI_string() : base()
    {
      headerLabel = this.Q<Label>();
      headerLabel.RegisterCallback<MouseEnterEvent>(OnMouseEnter); headerLabel.RegisterCallback<MouseLeaveEvent>(OnMouseLeave);
      textField = this.Q<TextField>();
#if UNITY_STANDALONE_WIN
      textField.isDelayed = true;
#endif //UNITY_STANDALONE_WIN
      textField.RegisterValueChangedCallback(OnValueChanged);
      textField.RegisterCallback<MouseEnterEvent>(OnMouseEnter); textField.RegisterCallback<MouseLeaveEvent>(OnMouseLeave);
      textField.RegisterCallback<KeyDownEvent>(OnKeyDown); textField.RegisterCallback<KeyUpEvent>(OnKeyUp);
      RegisterCallback<MouseEnterEvent>(OnMouseEnter); RegisterCallback<MouseLeaveEvent>(OnMouseLeave);
    }

    void OnKeyDown(KeyDownEvent evt) { if (evt.ctrlKey && evt.keyCode == KeyCode.V) textField.value = GS.Clipboard.TrimEnd(); }
    void OnKeyUp(KeyUpEvent evt) { }

    public static Type Get_Base_Type() => typeof(string);
    public static bool IsType(Type type) => type == typeof(string);
    public new static void _cs_Write(GS gs, StrBldr tData, StrBldr lateUpdate, StrBldr lateUpdate_ValuesChanged,
      StrBldr showIfs, StrBldr onValueChanged, AttGS attGS, string typeStr, string name)
    {
      StackFields(tData, typeStr, name);
      lateUpdate.Add($"\n    if (UI_{name}.Changed || {name} != UI_{name}.v) {{ data.{name} = UI_{name}.v; ValuesChanged = gChanged = true; }}");
      UI_VisualElement._cs_Write(gs, tData, lateUpdate, lateUpdate_ValuesChanged, showIfs, onValueChanged, attGS, typeStr, name);
    }

    public static string className => MethodBase.GetCurrentMethod().DeclaringType.ToString().After("GpuScript.");
    public static void UXML_UI_Element(UI_Element e) { UXML(e, e.attGS, e.memberInfo.Name, e.attGS.Name, className); e.uxml.Add($" />"); }

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
  }
}