//using UnityEngine;
//using UnityEngine.UIElements;

//namespace MyUILibrary
//{
//  public class SlideToggle : BaseField<bool>
//  {
//    public new class UxmlFactory : UxmlFactory<SlideToggle, UxmlTraits> { }

//    public new class UxmlTraits : BaseFieldTraits<bool, UxmlBoolAttributeDescription> { }

//    // In the spirit of the BEM standard, the SlideToggle has its own block class and two element classes. It also
//    // has a class that represents the enabled state of the toggle.
//    public static readonly new string ussClassName = "slide-toggle", inputUssClassName = "slide-toggle__input";
//    public static readonly string inputKnobUssClassName = "slide-toggle__input-knob", inputCheckedUssClassName = "slide-toggle__input--checked";

//    VisualElement m_Input, m_Knob;

//    public SlideToggle() : this(null) { } // Custom controls need a default constructor. This default constructor calls the other constructor in this class.


//    public SlideToggle(string label) : base(label, null)// This constructor allows users to set the contents of the label.
//    {
//      AddToClassList(ussClassName);// Style the control overall.
//      m_Input = this.Q(className: BaseField<bool>.inputUssClassName);// Get the BaseField's visual input element and use it as the background of the slide.
//      m_Input.AddToClassList(inputUssClassName);
//      Add(m_Input);
//      m_Knob = new();// Create a "knob" child element for the background to represent the actual slide of the toggle.
//      m_Knob.AddToClassList(inputKnobUssClassName);
//      m_Input.Add(m_Knob);

//      // There are three main ways to activate or deactivate the SlideToggle. All three event handlers use the
//      // static function pattern described in the Custom control best practices.
//      RegisterCallback<ClickEvent>(evt => OnClick(evt));// ClickEvent fires when a sequence of pointer down and pointer up actions occurs.
//      RegisterCallback<KeyDownEvent>(evt => OnKeydownEvent(evt));// KeydownEvent fires when the field has focus and a user presses a key.
//      RegisterCallback<NavigationSubmitEvent>(evt => OnSubmit(evt));// NavigationSubmitEvent detects input from keyboards, gamepads, or other devices at runtime.
//    }

//    static void OnClick(ClickEvent evt)
//    {
//      var slideToggle = evt.currentTarget as SlideToggle;
//      slideToggle.ToggleValue();
//      evt.StopPropagation();
//    }

//    static void OnSubmit(NavigationSubmitEvent evt)
//    {
//      var slideToggle = evt.currentTarget as SlideToggle;
//      slideToggle.ToggleValue();
//      evt.StopPropagation();
//    }

//    static void OnKeydownEvent(KeyDownEvent evt)
//    {
//      var slideToggle = evt.currentTarget as SlideToggle;

//      // NavigationSubmitEvent event already covers keydown events at runtime, so this method shouldn't handle them.
//      if (slideToggle.panel?.contextType == ContextType.Player) return;

//      // Toggle the value only when the user presses Enter, Return, or Space.
//      if (evt.keyCode == KeyCode.KeypadEnter || evt.keyCode == KeyCode.Return || evt.keyCode == KeyCode.Space)
//      {
//        slideToggle.ToggleValue();
//        evt.StopPropagation();
//      }
//    }
//    void ToggleValue() { value = !value; }// All three callbacks call this method.
//    // Because ToggleValue() sets the value property, the BaseField class dispatches a ChangeEvent. This results in a
//    // call to SetValueWithoutNotify(). This example uses it to style the toggle based on whether it's currently enabled.
//    public override void SetValueWithoutNotify(bool newValue)
//    {
//      base.SetValueWithoutNotify(newValue);
//      m_Input.EnableInClassList(inputCheckedUssClassName, newValue);//style the input element to look enabled or disabled.
//    }
//  }
//}