using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using UnityEditor;
using UnityEngine;

public class EditorPrefBrowser : EditorWindow
{
  private static class Styles
  {
    public static readonly GUIStyle ToolbarSearchField = "ToolbarSeachTextField";
    public static readonly GUIStyle ToolbarSearchFieldCancel = "ToolbarSeachCancelButton";
    public static readonly GUIStyle ToolbarSearchFieldCancelEmpty = "ToolbarSeachCancelButtonEmpty";

    public static readonly GUIStyle HeaderBackground = new GUIStyle(GUI.skin.box);

    static Styles()
    {
      // Zero out margin to go to edges of window
      HeaderBackground.margin = new RectOffset();

      // Push one point border on left and right out of the bounds of the window
      HeaderBackground.overflow = new RectOffset(1, 1, 0, 0);
    }
  }

  [NonSerialized]
  private readonly SortedDictionary<string, object> m_EditorPrefsLookup = new SortedDictionary<string, object>();

  [NonSerialized]
  private IPreferenceProvider m_PrefProvider;

  [SerializeField]
  private Vector2 m_ScrollPosition = new Vector2(0f, 0f);

  [SerializeField]
  private string m_Filter = "";

  [NonSerialized]
  private const int kLinuxEditorPlatform = 16; // RuntimePlatform.LinuxEditor on new enough codebase

  private bool IsFiltering { get => !string.IsNullOrEmpty(m_Filter); }

  [MenuItem("Window/Editor Pref Browser")]
  public static void ShowWindow()
  {
    GetWindow<EditorPrefBrowser>().titleContent = new GUIContent("Editor Pref");
  }

  public void OnEnable()
  {
    m_PrefProvider = GetProvider();

    m_PrefProvider.FetchKeyValues(m_EditorPrefsLookup);
  }

  private IPreferenceProvider GetProvider()
  {
    switch (Application.platform)
    {
      case RuntimePlatform.WindowsEditor:
        return new WindowsPreferenceProvider();
      case (RuntimePlatform)kLinuxEditorPlatform:
        return new LinuxPreferenceProvider();
    }

    throw new NotImplementedException(string.Format("No IPreferenceProvider implemented for {0}.{1}", Application.platform.GetType(), Application.platform));
  }

  public void OnGUI()
  {
    DoToolbar();

    DoHeader();

    DoList();
  }

  private void DoHeader()
  {
    using (new EditorGUILayout.HorizontalScope(Styles.HeaderBackground, GUILayout.ExpandHeight(false)))
    {
      GUILayout.Label("Name", GUILayout.Width(EditorGUIUtility.labelWidth));
      GUILayout.Label("Value");
    }
  }

  private void DoList()
  {
    using (var scrollView = new EditorGUILayout.ScrollViewScope(m_ScrollPosition))
    {
      m_ScrollPosition = scrollView.scrollPosition;

      EditorGUI.BeginChangeCheck();
      string valueName = null;
      object value = null;

      foreach (var kvp in m_EditorPrefsLookup)
      {
        valueName = kvp.Key;
        value = kvp.Value;

        if (IsFiltering && !valueName.ToLower().Contains(m_Filter.ToLower()))
          continue;

        EditorGUI.BeginChangeCheck();
        MonoBehaviour.print($"{new { valueName }}<{new { value }}>");
        value = m_PrefProvider.ValueField(valueName, value);
        if (EditorGUI.EndChangeCheck())
          break;
      }

      if (EditorGUI.EndChangeCheck())
      {
        m_PrefProvider.SetKeyValue(valueName, value);
        m_EditorPrefsLookup[valueName] = value;
      }
    }
  }

  private void DoToolbar()
  {
    using (new EditorGUILayout.HorizontalScope(EditorStyles.toolbar))
    {
      // Refresh Button
      if (GUILayout.Button("Refresh", EditorStyles.toolbarButton))
        m_PrefProvider.FetchKeyValues(m_EditorPrefsLookup);

      GUILayout.FlexibleSpace();

      // Filter Field
      m_Filter = EditorGUILayout.TextField(m_Filter, Styles.ToolbarSearchField, GUILayout.Width(250f));
      if (GUILayout.Button(GUIContent.none, IsFiltering ? Styles.ToolbarSearchFieldCancel : Styles.ToolbarSearchFieldCancelEmpty))
      {
        m_Filter = "";
        GUIUtility.keyboardControl = 0;
      }
    }
  }
}