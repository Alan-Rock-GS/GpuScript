//using System;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UIElements;

namespace GpuScript
{
  public class UI_Array : UI_VisualElement
  {
    //public static Type Get_Base_Type() => typeof(Array); 
    //public static bool IsType(Type type) => type.IsArray; 
    public new static void _cs_Write(GS gs, StrBldr tData, StrBldr lateUpdate, StrBldr lateUpdate_ValuesChanged,
      StrBldr showIfs, StrBldr onValueChanged, AttGS attGS, string typeStr, string name)
    {
      tData.Add($"\n    public {name}[] {name};");
    }

    //  public VisualElement array_container;
    //  public Scroller scroller;
    //  public List<VisualElement> Rows;

    //  public static void Build(UI_Array ui_array)
    //  {
    //    string label = "Array";
    //    bool val = true;

    //    if (ui_array.toggle != null)
    //    {
    //      ui_array.toggle.value = val;
    //      ui_array.label = label;
    //      Assign_style(ui_array, flexGrow: 0, width: GS.max(20, GS.UI_Component_Width(ui_array.label)), height: Length.Auto());
    //      return;
    //    }

    //    ui_array.Clear();

    //    ui_array.header = new Button();
    //    ui_array.toggle_container = new VisualElement();
    //    ui_array.toggle = new Toggle("") { value = val };
    //    ui_array.label = label;
    //    Assign_style(ui_array, flexGrow: 0, width: GS.max(20, GS.UI_Component_Width(ui_array.label)), height: Length.Auto());
    //    Assign_style(ui_array.header, flexGrow: 0, textAnchor: TextAnchor.MiddleCenter, width: Length.Auto());
    //    Assign_style(ui_array.toggle_container, flexGrow: 0, width: Length.Auto());
    //    Assign_style(ui_array.toggle, alignSelf: Align.Center, width: 20);
    //    ui_array.toggle.RegisterValueChangedCallback(ui_array.OnValueChanged);

    //    ui_array.RegisterCallback<MouseEnterEvent>(ui_array.OnMouseEnter);
    //    ui_array.RegisterCallback<MouseLeaveEvent>(ui_array.OnMouseLeave);
    //    ui_array.header.RegisterCallback<MouseEnterEvent>(ui_array.OnMouseEnter);
    //    ui_array.header.RegisterCallback<MouseLeaveEvent>(ui_array.OnMouseLeave);
    //    ui_array.toggle_container.RegisterCallback<MouseEnterEvent>(ui_array.OnMouseEnter);
    //    ui_array.toggle_container.RegisterCallback<MouseLeaveEvent>(ui_array.OnMouseLeave);
    //    ui_array.toggle.RegisterCallback<MouseEnterEvent>(ui_array.OnMouseEnter);
    //    ui_array.toggle.RegisterCallback<MouseLeaveEvent>(ui_array.OnMouseLeave);

    //    ui_array.Add(ui_array.header);
    //    ui_array.Add(ui_array.toggle_container);
    //    ui_array.toggle_container.Add(ui_array.toggle);
    //  }

    //  public Button header;
    //  public VisualElement toggle_container;
    //  public Toggle toggle;
    //  public override string label { get => base.label; set { base.label = value; if (header != null) header.text = value; } }
    //  public bool previousValue;

    //  public static implicit operator bool(UI_Array f) => f.v; 
    //  public static explicit operator int(UI_Array f) => f.v ? 1 : 0; 

    //  public bool v { get => toggle != null ? (bool)toggle?.value : false; set { if (toggle != null) toggle.value = value; } }

    //  void OnValueChanged(ChangeEvent<bool> evt)
    //  {
    //    previousValue = evt.previousValue; v = evt.newValue; property?.SetValue(gs, toggle.value);
    //  }
    //  public override bool Changed { get => v != previousValue; set => previousValue = value ? !v : v; }

    //  public new class UxmlFactory : UxmlFactory<UI_Array, UxmlTraits> { }
    //  public new class UxmlTraits : UI_VisualElement.UxmlTraits
    //  {
    //    public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
    //    {
    //      base.Init(ve, bag, cc);
    //      Build(ve as UI_Array);
    //    }
    //  }
  }
}