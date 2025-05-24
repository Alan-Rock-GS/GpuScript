//// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC
//using UnityEngine;
//using System;
//using System.Collections;
//using UnityEditor;
//using GpuScript;

//[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
//public class HidePropertyAttribute : PropertyAttribute
//{
//  //The name of the bool field that will be in control
//  public string ConditionalSourceField = "";
//  public bool hideInInspector = false;

//  public HidePropertyAttribute(string conditionalSourceField)
//  {
//    ConditionalSourceField = conditionalSourceField;
//    hideInInspector = false;
//  }

//  public HidePropertyAttribute(string conditionalSourceField, bool hideInInspector)
//  {
//    ConditionalSourceField = conditionalSourceField;
//    this.hideInInspector = hideInInspector;
//  }
//}

////#if UNITY_EDITOR
////[CustomPropertyDrawer(typeof(GS))]
//////[CustomPropertyDrawer(typeof(gsAVGrid_Test))]
////public class GSDrawer : PropertyDrawer
////{
////  //public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
////  //{
////  //  GS f = fieldInfo.Parent<GS>(property);
////  //  if (f.att == null)
////  //    f.Init(f.gs, fieldInfo);
////  //  var att = f.att;
////  //  if (att == null) return;
////  //  EditorGUI.BeginProperty(position, label, property);
////  //  {
////  //    EditorGUI.LabelField(position, new GUIContent(att.Name, att.Description));
////  //    var indent = EditorGUI.indentLevel;
////  //    EditorGUI.indentLevel = 0;
////  //    var toggleRect = new Rect(125, position.y, GS.max(125, position.width - 125), position.height);
////  //    f.v = EditorGUI.Toggle(toggleRect, f.v);
////  //    //GS.W(att.Name, " ", f.v);
////  //    EditorGUI.indentLevel = indent;
////  //  }
////  //  EditorGUI.EndProperty();
////  //}

////  public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
////  {
////    //GS f = fieldInfo.Parent<GS>(property);
////    //GS.print($"f is {GS.check(f)}");
////    //get the attribute data
////    HidePropertyAttribute condHAtt = (HidePropertyAttribute)attribute;
////    //check if the propery we want to draw should be enabled
////    bool enabled = GetConditionalHideAttributeResult(condHAtt, property);

////    //Enable/disable the property
////    bool wasEnabled = GUI.enabled;
////    GUI.enabled = enabled;

////    //Check if we should draw the property
////    if (condHAtt != null && (!condHAtt.hideInInspector || enabled))
////      EditorGUI.PropertyField(position, property, label, true);

////    //Ensure that the next property that is being drawn uses the correct settings
////    GUI.enabled = wasEnabled;
////  }
////  private bool GetConditionalHideAttributeResult(HidePropertyAttribute condHAtt, SerializedProperty property)
////  {
////    bool enabled = true;
////    //Look for the sourcefield within the object that the property belongs to
////    string propertyPath = property.propertyPath; //returns the property path of the property we want to apply the attribute to
////    if (propertyPath != null && condHAtt != null)
////    {
////      string conditionPath = propertyPath.Replace(property.name, condHAtt.ConditionalSourceField); //changes the path to the conditionalsource property path
////      SerializedProperty sourcePropertyValue = property.serializedObject.FindProperty(conditionPath);

////      if (sourcePropertyValue != null)
////        enabled = sourcePropertyValue.boolValue;
////      else
////        Debug.LogWarning("Attempting to use a HidePropertyAttribute but no matching SourcePropertyValue found in object: " + condHAtt.ConditionalSourceField);
////    }
////    return enabled;
////  }

////  public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
////  {
////    HidePropertyAttribute condHAtt = (HidePropertyAttribute)attribute;
////    bool enabled = GetConditionalHideAttributeResult(condHAtt, property);

////    if (condHAtt != null && (!condHAtt.hideInInspector || enabled))
////    {
////      return EditorGUI.GetPropertyHeight(property, label);
////    }
////    else
////    {
////      //The property is not being drawn
////      //We want to undo the spacing added before and after the property
////      return -EditorGUIUtility.standardVerticalSpacing;
////    }
////  }

////}
////#endif //UNITY_EDITOR