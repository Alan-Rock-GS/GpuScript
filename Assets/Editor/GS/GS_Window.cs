using GpuScript;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using static GpuScript.GS;
using UnityEditor.Compilation;

//https://www.youtube.com/watch?v=Xy_jvBg1vS0

[InitializeOnLoad]
public class GS_Window : EditorWindow
{
  [SerializeField] VisualTreeAsset visualTreeAsset;

  [MenuItem("Window/GpuScript")] public static void ShowWindow() { var w = GetWindow<GS_Window>("GpuScript"); w.minSize = new Vector2(200, 50); }

  [SerializeField] string gsClass_name_val, package_name_val, backup_description_val, backup_omitFolders_val, exe_Version_val;
  [SerializeField] bool gsClass_Run_Val, exe_Parent_val, exe_Build_val, exe_Debug_val, exe_Run_val;

  TextField gsClass_name, package_name, backup_description, backup_omitFolders, info_Android_dirs, CodeCount, exe_Version;
  Button gsClass_Build, gsClass_Fix, gsClass_Lib, package_Create, backup_Backup, backup_Restore,
    android_projectPath, android_persistentPath, android_phonePath, exe_Exe, exe_Setup, exe_Apk, exe_Apk_CMake;
  Toggle gsClass_Run, exe_Parent, exe_Build, exe_Debug, exe_Run;
  DropdownField info_platform;

  public static GS_Window This;
  void CreateGUI()
  {
    This = this;
    visualTreeAsset?.CloneTree(rootVisualElement);
    Type[] types = new Type[] {typeof(Label), typeof(Button), typeof(Toggle), typeof(Scroller), typeof(TextField), typeof(Foldout), typeof(Slider),
      typeof(SliderInt), typeof(MinMaxSlider), typeof(ProgressBar), typeof(DropdownField), typeof(Enum), typeof(RadioButton), typeof(RadioButtonGroup)};

    foreach (var fld in GetType().GetFields(bindings))
      foreach (var type in types)
        if (fld.FieldType == type)
        {
          var element = rootVisualElement.Q(fld.Name);
          fld.SetValue(this, element);
          if (type == typeof(TextField))
          {
            var valFld = GetType().GetField(fld.Name + "_val", bindings);
            if (valFld != null)
            {
              var textField = (TextField)element;
              if (textField != null)
              {
                textField.isDelayed = true; //RegisterValueChangedCallback only called when user presses enter or gives away focus, with no Escape notification
                textField.RegisterValueChangedCallback((evt) => { valFld.SetValue(this, evt.newValue); TextFieldChanged(textField); });
                textField.value = valFld.GetValue(this)?.ToString();
              }
            }
          }
          if (type == typeof(Toggle))
          {
            var valFld = GetType().GetField(fld.Name + "_val", bindings);
            if (valFld != null)
            {
              var toggle = (Toggle)element;
              toggle.RegisterValueChangedCallback((evt) => { valFld.SetValue(this, evt.newValue); ToggleChanged(toggle); });
              toggle.value = valFld.GetValue(this)?.To_bool() ?? false;
            }
          }
          else if (type == typeof(Button))
          {
            var button = (Button)element;
            var method = GetType().GetMethod(fld.Name + "_clicked", bindings);
            button?.RegisterCallback<ClickEvent>((evt) => { method?.Invoke(this, new object[] { evt }); });
          }
        }
    info_platform.RegisterValueChangedCallback((evt) => { SwitchPlatform(); });
    CodeCount.RegisterCallback<ClickEvent>(CodeCount_clicked);
    CodeCount_clicked(null);
    info_platform.index = isAndroid ? 1 : 0;
    SwitchPlatform();
  }

  bool ignoreTextFieldChange = false;
  private void TextFieldChanged(TextField textField)
  {
    if (ignoreTextFieldChange) { ignoreTextFieldChange = false; return; }
    if (textField == gsClass_name && textField.value.IsNotEmpty() && textField.value.DoesNotStartWith("gs")) { ignoreTextFieldChange = true; textField.value = "gs" + textField.value; }
  }

  void SceneCopy(string newSceneName)
  {
    if (SceneName.IsEmpty()) return;

    string oldSceneName = SceneName;
    This = this;
    SaveScene();

    string scenePath = SceneManager.GetActiveScene().path;
    string assemblyName = scenePath.Between("Assets/", "/");
    string name0 = oldSceneName, name1 = newSceneName, gsName0 = $"gs{oldSceneName}", gsName1 = $"gs{newSceneName}";
    string path = $"{AssetsPath}{assemblyName}/", path0 = $"{path}{gsName0}/", path1 = $"{path}{gsName1}/";
    path1.CreatePath();

    string[] dirs = Directory.GetDirectories(path).Select(a => a.ToPath()).Where(a => a.Contains($"/{gsName0}/")).Select(a => a).ToArray(), files;
    foreach (var dir0 in dirs)
    {
      string dir1 = dir0.Replace(name0, name1);
      files = dir0.GetFiles($"*{name0}*.*");
      foreach (var file in files)
        if (file.EndsWith(".compute", ".shader"))
        {
          string file0 = file.ReplaceAll("\\", "/"), file1 = $"{dir1}{file0.After(dir0).Replace(name0, name1)}".Replace("\\", "/");
          file0.CopyFile(file1);
          file1.WriteAllTextAscii(file1.ReadAllTextAscii().RegexReplace(name0, name1));
        }
        else if (file.DoesNotEndWith(".meta", ".unity", ".uxml", ".mat"))
        {
          string file0 = file.ReplaceAll("\\", "/"), file1 = $"{dir1}{file0.After(dir0).Replace(name0, name1)}".Replace("\\", "/");
          file0.CopyFile(file1);
          file1.WriteAllText(file1.ReadAllText().RegexReplace(name0, name1));
        }
    }

    SaveSceneAs($"{path1}{name1}.unity");
    gsClass_name_val = gsClass_name.value = gsName1;
    GameObject.Find(gsName0).Destroy();

    path = dataPath; path0 = $"{path}{name0}/"; path1 = $"{path}{name1}/";
    path1.CreatePath();
    dirs = path0.GetThisAndAllDirectories();
    foreach (var dir0 in dirs)
    {
      print(dir0);
      string dir1 = dir0.Replace(name0, name1);
      files = dir0.GetFiles();
      foreach (var file in files)
      {
        if (file.DoesNotEndWith("Recompiled.txt"))
        {
          string file0 = file.ReplaceAll("\\", "/"), file1 = $"{dir1}{file0.After(dir0).Replace(name0, name1)}".Replace("\\", "/");
          file0.CopyFile(file1);
          file1.WriteAllText(file1.ReadAllText().RegexReplace(name0, name1));
        }
      }
    }
    rebuild = 20;
    SaveActiveScene();
  }

  string SceneName => EditorSceneManager.GetActiveScene().name;
  bool OpenScene(string name) { var f = $"Assets/gs{name}/{name}.unity"; return (dataPath + f).Exists() && EditorSceneManager.OpenScene(f).IsValid(); }

  private void ToggleChanged(Toggle toggle)
  {
    if (exe_Debug != null)
      exe_Debug.style.display = DisplayIf(exe_Build.value);
  }

  void OnEnable()
  {
    Selection.selectionChanged -= SelectionChanged;
    Selection.selectionChanged += SelectionChanged;
    EditorSceneManager.activeSceneChangedInEditMode -= OnActiveSceneChangedInEditMode;
    EditorSceneManager.activeSceneChangedInEditMode += OnActiveSceneChangedInEditMode;
  }

  void OnDisable() //probably unnecessary
  {
    if (Selection.selectionChanged != null) Selection.selectionChanged -= SelectionChanged;
  }

  public static string importedAssets_filename => $"{dataPath}importedAssets.txt";

  void SelectionChanged()
  {
    var o = Selection.activeGameObject;
    if (o != null && o.name.StartsWith("gs") && gsClass_name != null && o.name.After("gs") == sceneName)
      gsClass_name_val = gsClass_name.value = o.name;
  }

  void OnActiveSceneChangedInEditMode(Scene previousScene, Scene newScene)
  {
    if (gsClass_name != null)
    {
      gsClass_name_val = gsClass_name.value = "gs" + newScene.name;
      if (gsClass_name_val == "gsDocs")
      {
        //https://docs.unity3d.com/ScriptReference/EditorBuildSettings-scenes.html
        print($"Add all scenes to build, {appPath}");
        var fs = AssetsPath.GetAllFiles("*.unity");
        var s = StrBldr("Editor Scenes:");
        foreach (var f in fs) s.Add($" {f.After("/Assets/")}");
        s.Add(", Scenes:");
        foreach (var scene in EditorBuildSettings.scenes) s.Add($" {scene.path.After("/Assets/")}");
        print(s);
      }
      else
      {
        ////print("Add current scene to build, https://docs.unity3d.com/ScriptReference/EditorBuildSettings-scenes.html");
        //print($"Add current scene to build, {appPath}");
      }
    }
  }

  public void android_projectPath_clicked(ClickEvent evt = null) { print(projectPath); projectPath.Run(); }
  public void android_persistentPath_clicked(ClickEvent evt = null) { print(dataPath); }

  public static void ClearConsole() { System.Reflection.Assembly.GetAssembly(typeof(SceneView)).GetType("UnityEditor.LogEntries").GetMethod("Clear").Invoke(new object(), null); }

  public void android_phonePath_clicked(ClickEvent evt = null)
  {
    print($@"This PC\Galaxy S20 Ultra 5G\Internal storage\Android\data\{Application.identifier}\files");
  }

  public delegate void UpdateDelegate();

  #region Coroutines
  public GS_Coroutines coroutines = new GS_Coroutines();
  public GS_Coroutine StartCoroutine(IEnumerator routine) { var coroutine = new GS_Coroutine(routine); coroutines.Add(coroutine); return coroutine; }
  #endregion Coroutines

  [Serializable]
  public class ProjectData
  {
    public string Name, gsName, _GS_Name, AssetsPath, classPath, gsPathFile, _GS_filename, _GS_Text, _GS_Code, _cs_filename, _cs_Text, _cs_Code,
      cs_filename, cs_Text, cs_Code, compute_filename, shader_filename, material_filename;
    bool _GS_file_changed = false; string _GS_FileTextCode { get => (_GS_Text = _GS_filename.ReadAllText()).Clean(); set { if (_GS_file_changed = _GS_filename.WriteAllText_IfChanged(_GS_Text = value)) _GS_Code = _GS_Text.Clean(); } }
    bool _cs_file_changed = false; string _cs_FileTextCode { get => (_cs_Text = _cs_filename.ReadAllText()).Clean(); set { if (_cs_file_changed = _cs_filename.WriteAllText_IfChanged(_cs_Text = value)) _cs_Code = _cs_Text.Clean(); } }
    bool cs_file_changed = false; string cs_FileTextCode { get => (cs_Text = cs_filename.ReadAllText()).rRemoveExcludeRegions().Clean(); set { if (cs_file_changed = cs_filename.WriteAllText_IfChanged(cs_Text = value)) cs_Code = cs_Text.Clean(); } }

    string compute_FileTextCode { set => compute_filename.WriteAllTextAscii(value); }
    string shader_FileTextCode { set => shader_filename.WriteAllTextAscii(value); }
    GameObject gameObject;
    Component _GS_component, GS_script;
    GS gs;
    _GS _gs;
    Type _GS_Type;
    Type _cs_Type => $"{gsName}_".ToType() ?? Type.GetType(gsName + "_" + ", Assembly_" + gsName);

    Type[] _GS_nestedTypes;
    FieldInfo[] _GS_fields;
    PropertyInfo[] _GS_properties;
    MethodInfo[] _GS_methods;
    MemberInfo[] _GS_members, buff_members;
    StrBldr _cs_includes;
    bool build_UI = true;
    public struct ValueChange { public object previousValue; public bool changed; }
    public Dictionary<string, ValueChange> valueChanges = new Dictionary<string, ValueChange>();
    public List<FieldInfo> gpu_flds, gpu_flds2, lib_flds, _GS_texture_flds;
    StrBldr gStruct;

    public string AssemblyPath(string name) => GS_Window.AssemblyPath(name);

    public ProjectData(string name)
    {
      Name = name.StartsWith("gs") ? name.After("gs") : name;
      gsName = $"gs{Name}";
      (_GS_Name, AssetsPath) = ($"{gsName}_GS", $"{dataPath}Assets/");
      classPath = AssemblyPath(gsName);
      gsPathFile = $"{classPath}{gsName}";
      (_GS_filename, _cs_filename, cs_filename) = ($"{gsPathFile}_GS.cs", $"{gsPathFile}_.cs", $"{gsPathFile}.cs");
      (compute_filename, shader_filename, material_filename) = ($"{gsPathFile}.compute", $"{gsPathFile}.shader", $"{gsPathFile}.mat");
    }

    bool BuildLib(string Name0, FieldInfo libFld, ref string _GS_Code0, StrBldr _cs_includes, StrBldr _cs_lib_regions, List<string> libKernels)
    {
      bool changed = false;
      string declaration = $"{libFld.FieldType} {libFld.Name};";
      if (_GS_Code0.Contains(declaration))
      {
        string region = $"\n  #region <{libFld.Name}>", endregion = $"\n  #endregion <{libFld.Name}>", s = _GS_Code0.After(declaration);
        bool rebuild = s.DoesNotContain(region), erase = !rebuild && s.Before(region).CharN("\n") >= 1;
        string libTypeName = libFld.FieldType.ToString(), libName = libTypeName.After("gs");

        bool isExternalLib = libFld.isExternal_Lib();
        string libPath = isExternalLib ? "" : $"{AssemblyPath(libTypeName)}Lib/";
        string libPrefix = $"{libPath}{libTypeName}";

        bool libPath_Exists = libPath.IsNotEmpty() && libPath.Exists();
        string _GS_lib_Code = libPath_Exists ? $"{libPrefix}_GS_lib.txt".ReadAllText() : "", cs_lib_Code = libPath_Exists ? $"{libPrefix}_cs_lib.txt".ReadAllText() : "";

        string cs_lib_kernels_str = libPath_Exists ? $"{libPrefix}_cs_lib_kernels.txt".ReadAllText() : "";
        string[] cs_lib_includes = libPath_Exists ? $"{libPrefix}_cs_lib_includes.txt".ReadAllLines() : new string[] { };
        if (libName != libFld.Name)
        {
          string pattern1 = $@"\b{libName}_", replace1 = libFld.Name + "_", pattern2 = $@"_{libName}", replace2 = "_" + libFld.Name;
          _GS_lib_Code = _GS_lib_Code.RegexReplace(pattern1, replace1, pattern2, replace2);
          cs_lib_Code = cs_lib_Code.RegexReplace(pattern1, replace1, pattern2, replace2);
          cs_lib_kernels_str = cs_lib_kernels_str.RegexReplace(pattern1, replace1, pattern2, replace2);
        }
        string[] cs_lib_kernels = cs_lib_kernels_str.Split("\t");
        if (cs_lib_kernels != null) foreach (var k in cs_lib_kernels) if (!libKernels.Contains(k)) libKernels.Add(k);
        if (cs_lib_includes != null) foreach (var i in cs_lib_includes) if (_cs_includes.ToString().DoesNotContain(i)) _cs_includes.Add("\n" + i);
        if (rebuild || erase)
        {
          _GS_Code0 = StrBldr(_GS_Code0.BeforeIncluding(declaration), region, "\n", _GS_lib_Code, endregion, _GS_Code0.After(rebuild ? declaration : endregion));
          changed = true;
        }
        cs_lib_Code = cs_lib_Code.RegexReplace($@"\b{libFld.Name}_Gpu_", "Gpu_" + libFld.Name + "_",
          $@"\b{libFld.Name}_Cpu_", "Cpu_" + libFld.Name + "_", $@"\b{libFld.Name}_g{libName}\b", $"g{Name0}");

        if (libFld.isExternal_Lib())
          cs_lib_Code = $"  {libTypeName} _{libFld.Name}; public {libTypeName} {libFld.Name} => _{libFld.Name} = _{libFld.Name} ?? Add_Component_to_gameObject<{libTypeName}>();" + cs_lib_Code;
        _cs_lib_regions.Add($"\n  #region <{libFld.Name}>\n", cs_lib_Code, $"\n  #endregion <{libFld.Name}>");
      }
      return changed;
    }

    private IEnumerator attempt(Func<bool> f) { for (int i = 0; !f(); i++) if (i == 0) yield return null; else yield break; }
    public StrBldr StrBldr(params object[] items) => new StrBldr(items);

    bool created_GS_Code = false;

    bool Create_GS_Code()
    {
      string scenePath = SceneManager.GetActiveScene().path;
      string assemblyName = scenePath.Between("Assets/", "/");

      classPath = AssemblyPath(gsName);
      gsPathFile = $"{classPath}{gsName}";
      (_GS_filename, _cs_filename, cs_filename) = ($"{gsPathFile}_GS.cs", $"{gsPathFile}_.cs", $"{gsPathFile}.cs");
      (compute_filename, shader_filename, material_filename) = ($"{gsPathFile}.compute", $"{gsPathFile}.shader", $"{gsPathFile}.mat");


      Directory.CreateDirectory(classPath);
      if (_GS_filename.DoesNotExist() || _GS_filename.FileLength() < 10)
      {
        var s = StrBldr().AddLines(
          "using System;",
          "using UnityEngine;",
          "using GpuScript;",
          "",
         $"[AttGS(GS_Class.Name, \"{Name}\", GS_Class.Description, \"{Name}\")]",
          "public class gs{Name}_GS : _GS",
          "{{",
          "  [GS_UI, AttGS(\"UI|User Interface\")] TreeGroup group_UI;",
          "  [GS_UI, AttGS(\"UI|User Interface\")] TreeGroupEnd groupEnd_UI;",
        "}}");
        _GS_filename.WriteAllText(s);
        AssetDatabase.Refresh();

        return false;
      }

      gameObject = FindOrCreate_GameObject(gsName); //this should be inserted at the top
      gameObject.transform.SetAsFirstSibling();

      if (gameObject == null) return false;
      _GS_component = gameObject.GetComponent(_GS_Name);

      if (!_GS_component)
      {
        try
        {
          Type componentType = GetComponentTypeByName(_GS_Name);
          _GS_component = gameObject.AddComponent(componentType);
          if (!_GS_component) return false;
        }
        catch (Exception) { AutoRefresh = true; return false; }
      }
      _GS_component ??= gameObject.AddComponent(GetComponentTypeByName(gsName));
      if (_GS_component == null) return false;
      created_GS_Code = (GS_script = FindOrCreate_Script(gameObject, gsName)) != null;
      return created_GS_Code;
    }

    void StackFields(StrBldr sb, string type, string name, string indent = "    ")
    {
      string s = sb.ToString(), t = $"public {type} ";
      if (s.Contains(t)) sb.Set(s.BeforeIncluding(t), s.Between(t, ";"), $", {name};", s.After(t).After(";"));
      else sb.Add("\n", indent, t, name, ";");
    }

    void Get_gStruct()
    {
      gStruct = StrBldr($"\n  public struct G{Name}", "\n  {");
      (gpu_flds, lib_flds) = (new List<FieldInfo>(), new List<FieldInfo>());
      foreach (var fld in _GS_fields)
      {
        if (fld.FieldType.IsNotAny(typeof(TreeGroup), typeof(TreeGroupEnd), typeof(string), typeof(strings), typeof(Texture2D)) && !fld.FieldType.IsArray)
        {
          Type fldType = fld.FieldType;
          string typeName = fldType.GetTypeName().Replace("GpuScript.", "");
          if (fldType.IsEnum || fldType == typeof(bool)) typeName = "uint";
          if (typeName.StartsWith("gs"))
            lib_flds.Add(fld);
          else
          {
            StackFields(gStruct, typeName, fld.Name);
            gpu_flds.Add(fld);
          }
        }
      }
      gStruct.Add("\n  };");
    }

    StrBldr _cs_lib_regions;
    List<string> libKernels;
    Type GetType(Type type, string add = "") => Type.GetType(type.AssemblyQualifiedName.Replace(type.Name, type.Name + add));
    Dictionary<string, string> _cs_lib_kernels = new Dictionary<string, string>();
    List<ProjectData> libProjects;

    void ForceRecompile()
    {
      CompilationPipeline.RequestScriptCompilation();
      AssetDatabase.Refresh();
    }

    bool _GS_Build_Libs()
    {
      bool changed = false;
      libProjects = new List<ProjectData>();
      string _GS_Text0 = _GS_Text.rRemoveDuplicateEmptyLines();
      Get_gStruct();
      libKernels = new List<string>();
      _cs_lib_regions = StrBldr();
      if (lib_flds.Count > 0) //add library code to _GS, comment out lib_flds in _GS, compile libraries by writing gsLib.cs files, or just use regex
      {
        _cs_lib_kernels.Clear();
        foreach (var libFld in lib_flds)
        {
          ProjectData libProject = new ProjectData(libFld.FieldType.ToString());
          libProjects.Add(libProject);
          if (libProject.BuildLib(Name, libFld, ref _GS_Text0, _cs_includes, _cs_lib_regions, libKernels)) changed = true;
        }
        if (_GS_filename.WriteAllText_IfChanged(_GS_Text0))
        {
          print($"Libraries reloaded: {_GS_filename.AfterLast("/")}, Recompiling");
          ForceRecompile();
          changed = true;
        }
        else changed = false;
      }
      return changed;
    }


    public UIDocument uiDocument;
    public VisualElement root, UI_GS;
    public bool Get_uiDocument()
    {
      uiDocument = null;
      var docs = FindObjectsByType<UIDocument>(FindObjectsInactive.Exclude, FindObjectsSortMode.InstanceID);
      foreach (var doc in docs) if (doc.isActiveAndEnabled) { uiDocument = doc; break; }
      uiDocument ??= FindOrCreate_GameObject(gsName)?.AddComponent<UIDocument>();
      if (uiDocument == null) return false;
      uiDocument.panelSettings ??= $"Assets/GS/UI_Elements/UI Toolkit/PanelSettings.asset".LoadAssetAtPath<PanelSettings>();
      root ??= uiDocument?.rootVisualElement?.Q<VisualElement>("Root");
      if (uiDocument.visualTreeAsset == null && uxml_filename.Exists()) uiDocument.visualTreeAsset = uxml_filename.LoadAssetAtPath<VisualTreeAsset>();
      root ??= uiDocument?.rootVisualElement?.Q<VisualElement>("Root");
      UI_GS ??= uiDocument?.rootVisualElement?.Q("UI_GS");
      return true;
    }

    void Get_USS_Text(StrBldr text, int height, params string[] names)
    {
      int fontSize = 12;
      string alignText = "lower-left";
      foreach (var name in names)
        UI_VisualElement.Get_USS_Text(text, name, fontSize: fontSize, alignText: alignText, height: height, padding: 0, margin: 0, border: 0);
    }
    public virtual string uss_filename => $"{dataPath}Assets/GS/Resources/UI/GpuScript/gs_USS.uss";

    bool Build_USS_File()
    {
      StrBldr text = StrBldr();
      int fontSize = 12;
      string alignText = "middle-center";
      UI_VisualElement.Get_USS_Text(text, ".ui_font", unity_font: "Arial Font/arial Unicode", fontSize: fontSize, alignText: alignText, margin: 0, padding: 0, border: 0);
      Get_USS_Text(text, 20, ".unity-base-field", ".unity-base-text-field__input", ".unity-button", ".unity-float-field", ".unity-label",
        ".unity-toggle > .unity-label", ".unity-enum-field", ".unity-enum-field__input", ".unity-integer-field");
      return uss_filename.WriteAllText_IfChanged(text);
    }

    public VisualElement[] ui_items;
    public bool in_Get_ui_items;

    MemberInfo[] _gs_members;

    public virtual string uxml_filename => $"{AssemblyPath(gsName)}{gsName}_UXML.uxml";

    public UI_Element Get_ui_items()
    {
      in_Get_ui_items = true;
      var items = new List<VisualElement>();

      gameObject ??= FindOrCreate_GameObject(gsName);
      _gs ??= gameObject.GetComponent<_GS>();
      gs ??= gameObject.GetComponent<GS>();

      UI_Element e = new UI_Element() { _gs = _gs, gs = gs, root = root };
      _gs_members = (gsName + "_GS").GetOrderedMembers();

      foreach (var member in _gs_members)
      {
        e.memberInfo = member;
        e._GS_memberType = member.GetMemberType();
        var declaringType = member.DeclaringType;
        e._GS_fieldType = member.IsFld() ? e._GS_memberType : null;
        e._GS_propType = member.IsProp() ? e._GS_memberType : null;
        e.methodInfo = member.IsMethod() ? member.Method() : null;
        e.attGS = member.AttGS();
        e.isExternalLib = member.isExternal_Lib();
        //generate e.uxml if is element, otherwise generate e.uxml for library. Then everything will be in the correct order.
        bool isRenderMethod = member.IsMethod() && e.attGS != null && e.attGS.isGS_Render;
        string typeStr = e._GS_memberType.ToString();
        if (typeStr.StartsWith("GpuScript.")) typeStr = typeStr.After("GpuScript.");

        if (typeStr == "class")
        {
          _GS_nestedTypes = _GS_Type.GetNestedTypes(_GS_bindings);
          Type[] classTypes = _GS_nestedTypes.Where(t => !t.IsValueType && !t.IsEnum).Select(t => t).ToArray();
          e._GS_fieldType = classTypes.First(a => a.Name.After("+") == member.Name);
        }
        if (e.attGS != null && member.Name != "class" && !isRenderMethod && !e.attGS.GroupShared)
          UI_VisualElement.UXML_UI_Element(e, _gs_members);
        else if (e.isExternalLib) //if is a library, insert UI
        {
          string uxml_file = $"{AssemblyPath(e._GS_memberType.ToString())}{e._GS_memberType}_UXML.uxml", ui0 = "<GpuScript.UI_GS ", ui1 = "</GpuScript.UI_GS>";
          if (uxml_file.Exists())
          {
            string lib_t = uxml_file.ReadAllText(), lib_ui0 = "UI_TreeGroup_Level=\"", lib_ui1 = "\"";
            if (lib_t.ContainsAll(ui0))
            {
              lib_t = lib_t.After(ui0).AfterIncluding("\n").BeforeLast(ui1).BeforeLast("\n");
              int treeLevel_offset = 0;
              var treeLevels = new List<int>();
              for (int i = 0; i < _gs_members.Length; i++)
              {
                if (_gs_members[i].IsType(typeof(TreeGroup))) { treeLevel_offset++; treeLevels.Add(i); }
                else if (_gs_members[i].IsType(typeof(TreeGroupEnd))) { treeLevel_offset--; treeLevels.RemoveAt(treeLevels.Count - 1); }
                else if (_gs_members[i].IsType(e._GS_memberType)) break;
              }
              while (lib_t.Contains("UI_TreeGroup_Level=\""))
              {
                int treeLevel = lib_t.Between(lib_ui0, lib_ui1).To_int();
                e.uxml.Add(lib_t.BeforeIncluding(lib_ui0), treeLevel + treeLevel_offset, lib_ui1);
                if (treeLevel == 0) e.uxml.Add($" UI_TreeGroup_Parent=\"{(treeLevels.Count == 0 ? "" : _gs_members[treeLevels[^1]].Name)}\"");
                lib_t = lib_t.After(lib_ui0).After(lib_ui1);
              }
              e.uxml.Add(lib_t);
            }
          }
        }
      }
      ui_items = items.ToArray();
      in_Get_ui_items = false;
      return e;
    }

    public IEnumerator Build_UXML_Coroutine()
    {
      if (_GS_Code.DoesNotContain("[GS_UI, AttGS(\"")) yield break;
      if (!Get_uiDocument()) yield break;
      if (root == null || UI_GS == null)
      {
        var uxml = StrBldr().AddLines(
  "<ui:UXML xmlns:ui=\"UnityEngine.UIElements\" xmlns:uie=\"UnityEditor.UIElements\" editor-extension-mode=\"False\">",
 $"    <Style src=\"project://database/Assets/GS/Resources/UI/GpuScript/gs_USS.uss?fileID=7433441132597879392&amp;guid={new_uxml_guid()}&amp;type=3#{gsName}_USS\" />",
  "    <ui:VisualElement name=\"Root\" style=\"flex-grow: 1; -unity-font: resource(&apos;Arial Font/arial Unicode&apos;);\">",
  "        <GpuScript.UI_GS name=\"UI_GS\" style=\"width: auto; flex-wrap: wrap; flex-grow: 1;\">",
  "        </GpuScript.UI_GS>",
  "        <ui:ProgressBar picking-mode=\"Ignore\" name=\"Progress\" label=\"Progress\" value=\"0\" low-value=\"0\" high-value=\"100\" title=\"\" style=\"flex-grow: 0; min-height: 24px; width: 100%;\" />",
  "    </ui:VisualElement>",
  "</ui:UXML>");

        if (uxml_filename.WriteAllText_IfChanged(uxml)) { }

        root = uiDocument.rootVisualElement?.Q<VisualElement>("Root");
      }
      UI_GS = uiDocument.rootVisualElement?.Q("UI_GS") ?? new VisualElement();
      Build_USS_File();

      string t = uxml_filename.ReadAllText(), ui0 = "<GpuScript.UI_GS ", ui1 = "</GpuScript.UI_GS>";
      if (t.ContainsAll(ui0))
      {
        StrBldr t0 = StrBldr(t.BeforeIncluding(ui0), t.Between(ui0, "\n")), t1 = StrBldr(t.Before(ui1).AfterLastIncluding("\n"), t.AfterIncluding(ui1));
        UI_Element e = Get_ui_items();
        e.uxml = StrBldr(t0, e.uxml, t1);
        if (uxml_filename.WriteAllText_IfChanged(e.uxml)) { }
      }
    }


    public StrBldr Enums, Enums_cginc, AssignEnums, Shader_Enums;
    public string[] AllEnumItems = new string[0];
    Type[] enumTypes, libTypes;
    public void Calc_enums()
    {
      (Enums, Enums_cginc, AssignEnums, Shader_Enums) = StrBldr();
      enumTypes = _GS_nestedTypes.Where(t => t != null && t.IsEnum).Select(t => t).ToArray();
      var EnumList = new List<string>();
      StrBldr enumItems = StrBldr();
      foreach (var t in enumTypes)
      {
        string enumName = t.Name;
        string[] enumNames = t.GetEnumNames();
        Array enumValues = t.GetEnumValues();
        enumItems.Clear();
        int itemValue0 = -1;
        for (int i = 0; i < enumNames.Length; i++)
        {
          string itemName = enumNames[i];
          object value = enumValues.GetValue(i);
          int itemValue = Convert.ChangeType(value, Enum.GetUnderlyingType(value.GetType())).To_int();
          enumItems.Add(itemName, itemValue == itemValue0 + 1 ? "" : $" = _{itemValue}", i == enumNames.Length - 1 ? "" : ", ");
          itemValue0 = itemValue;
          Enums_cginc.Add("\n  #define ", enumName, "_", itemName, "	", itemValue);
          AssignEnums.Add(i == 0 ? "\n  public const uint " : ", ", enumName, "_", itemName, " = ", itemValue);
          EnumList.Add($"{enumName}.{itemName}");
        }
        if (AssignEnums.IsNotEmpty()) AssignEnums.Add(";");
        enumItems.Replace(" = _", " = ");
        Enums.Add("\n  [JsonConverter(typeof(StringEnumConverter))] public enum ", enumName, " : uint { ", enumItems, " }");
      }
      AllEnumItems = EnumList.OrderByDescending(a => a.Length).ToArray();
    }

    public StrBldr declare_structs, appdata_v2f_struct, v2f_struct, declare_classes;
    public void Calc_structs()
    {
      (declare_structs, appdata_v2f_struct, v2f_struct, declare_classes) = StrBldr();
      Type[] structTypes = _GS_nestedTypes.Where(t => t.IsValueType && !t.IsEnum).Select(t => t).ToArray();
      foreach (Type t in structTypes)
      {
        string structName = t.Name;
        var structFlds = t.GetFields(bindings);
        StrBldr structValues = StrBldr();
        Type itemType0 = null;
        foreach (var fld in structFlds)
        {
          Type itemType = fld.FieldType;
          string itemName = fld.Name, itemTypeStr = fld.GetTypeName();
          if (itemTypeStr == "strings") itemTypeStr = "string";
          structValues.Add(itemType0 != itemType ? $"{(structValues == "" ? "" : "; ")}public {itemTypeStr} " : ", ", itemName);
          itemType0 = itemType;
        }
        string declare_struct = $"\n  public struct {structName} {{ {structValues}; }};";
        if (structName == "v2f") v2f_struct.Add(declare_struct);
        else if (structName == "appdata_v2f") appdata_v2f_struct.Add(declare_struct);
        else declare_structs.Add(declare_struct);
      }
      Type[] classTypes = _GS_nestedTypes.Where(t => !t.IsValueType && !t.IsEnum).Select(t => t).ToArray();
      foreach (Type t in classTypes)
      {
        string className = t.Name;
        var classFlds = t.GetFields(bindings);
        var (classValues, UI_classValues) = StrBldr();
        Type itemType0 = null;
        foreach (var fld in classFlds)
        {
          Type itemType = fld.FieldType;
          string itemName = fld.Name, itemTypeStr = fld.GetTypeName(), separator = classValues == "" ? "" : "; ";
          if (itemTypeStr == "strings") itemTypeStr = "string";
          if (itemType.IsEnum) classValues.Add(itemType0 != itemType ? $"{separator}public uint " : ", ", itemName);
          else classValues.Add(itemType0 != itemType ? $"{separator}public {itemTypeStr} " : ", ", itemName);
          itemType0 = itemType;
        }
        string declare_struct = $"\n  public struct {className} {{ {classValues}; }};";
        declare_structs.Add(declare_struct);
      }
    }

    public struct SizeInfo { public string sizeN, uintN; public int3 dimension; public string[] threadN; }

    SizeInfo GetSizeInfo(string o)
    {
      if (o == null) print("Size must be specified for all kernels");
      string[] size = o.Split(","), threadN = new string[] { "1", "1", "1" };
      for (int i = 0; i < size.Length; i++) size[i] = size[i].Trim();
      string sizeN = "", uintN = "";
      int3 dimension = i000;
      int n = size.Length, total_dimension = 0;
      for (int i = 0; i < n; i++)
      {
        string str = size[i];
        if (i < threadN.Length)
          threadN[i] = str;
        if (char.IsDigit(str[0])) { dimension[i]++; sizeN = $"{sizeN}{(i == 0 ? "" : " * ")}{str}"; }
        else
        {
          FieldInfo fld = gpu_flds.GetField(str);
          string fldStr = "1";
          if (fld != null) fldStr = fld.FieldType.SimplifyType();
          else if (str.Contains(".") && str.After(".").DoesNotStartWithAny("x", "y", "z") && enumTypes != null) str = fldStr = str.Replace(".", "_");
          if (fldStr.EndsWith("int2")) { dimension[i] += 2; sizeN = $"{sizeN}{(i == 0 ? "" : " * ")}product({str})"; }
          else if (fldStr.EndsWith("int3")) { dimension[i] += 3; sizeN = $"{sizeN}{(i == 0 ? "" : " * ")}product({str})"; }
          else { dimension[i]++; sizeN = $"{sizeN}{(i == 0 ? "" : " * ")}{str}"; }
        }
        total_dimension += dimension[i];
      }
      if (total_dimension == 1) uintN = threadN[0];
      else if (total_dimension == 2) { if (dimension[0] == 2) uintN = threadN[0]; else uintN = $"uint2({threadN[0]}, {threadN[1]})"; }
      else { if (dimension[0] == 3) uintN = threadN[0]; else if (dimension[0] == 2 || dimension[1] == 2) uintN = $"uint3({threadN[0]}, {threadN[1]})"; else uintN = $"uint3({threadN[0]}, {threadN[1]}, {threadN[2]})"; }
      return new SizeInfo() { sizeN = sizeN, dimension = dimension, threadN = threadN, uintN = uintN };
    }

    public class method_data
    {
      public method_data(string Name, string methodName)
      {
        this.methodName = methodName;
        name = $"g{Name}_{methodName}_GS";
        args = "uint3 id";
        return_type = "void";
        code = "";
      }
      public method_data(string Name, Match match)
      {
        access = match.Group(1);
        inheritance = match.Group(2);
        return_type = match.Group(3);
        name = match.Group(4);
        methodName = name.After(Name + "_").BeforeLast("_GS");
        args = match.Group(5);

        code = match.Group(6);
        if (code.Contains(" => ")) code = $" {{ return {match.Group(6).Between(" => ", ";")}; }}";
        argN = args.ArgN();
      }
      public MethodInfo method;
      public string methodSizeN, methodUint;
      public bool isVirtual, thisWrite;
      public int argN;

      public string access, inheritance, return_type, name, methodName, args, code, gcode, size, numThreads, id_less;
      public bool sync;
      public SizeInfo sizeInfo;
      public int3 dimension;
      public int dimensionN;
      public string[] threadN;
      public List<method_data> method_calls = new List<method_data>();
      public List<buffer_read_write> buffers = new List<buffer_read_write>();

      public void SetSizeInfo(SizeInfo o)
      {
        sizeInfo = o; size = o.sizeN; dimension = o.dimension; dimensionN = dot(o.dimension, i111); threadN = o.threadN;
        numThreads = dimensionN <= 1 ? "numthreads1, 1, 1" : dimensionN == 2 ? "numthreads2, numthreads2, 1" : "numthreads3, numthreads3, numthreads3";
        string thread1 = threadN[0], thread2 = threadN[1], thread3 = threadN[2];
        StrBldr id_less = new StrBldr();
        if (!sync)
        {
          if (dimension.z == 1) id_less.Add($"id.z < {thread3} && ");
          if (dimension.y == 1) id_less.Add($"{(dimension.x == 1 ? "id.y" : "id.z")} < {thread2} && "); else if (dimension.y == 2) id_less.Add($"id.yz < {thread2} && ");
          if (dimension.x == 1) id_less.Add($"id.x < {thread1}"); else if (dimension.x == 2) id_less.Add($"all(id.xy < {thread1})"); else if (dimension.x == 3) id_less.Add($"all(id < {thread1})");
        }
        this.id_less = id_less;
      }

      public override string ToString() => $"  {return_type} {name}({args}) {code}";

      public bool isKernel => return_type.IsAny("void", "IEnumerator") && name.EndsWith("_GS") && name.DoesNotContainAny("vert_", "frag_", "Cpu_") && args.IsAny("uint3 id", "uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI");

      public buffer_read_write AddBuffer(buffer_data buff)
      {
        var b = buffers.Find(a => a.buffer == buff);
        if (b == null)
          buffers.Add(b = new buffer_read_write() { buffer = buff });
        return b;
      }
      public void AddReadBuffer(buffer_data buff) { if (buff.isNotGroupShared) AddBuffer(buff).read = true; }
      public void AddWriteBuffer(buffer_data buff) { AddBuffer(buff).write = true; }
      public void AddReadWriteBuffer(buffer_data buff) { var b = AddBuffer(buff); b.read = b.write = true; }

      public void SetReadWriteBuffers()
      {
        foreach (var m in method_calls)
        {
          m.SetReadWriteBuffers();
          foreach (var b in m.buffers)
            if (b.buffer.isNotGroupShared)
              if (b.read && b.write) AddReadWriteBuffer(b.buffer); else if (b.read) AddReadBuffer(b.buffer); else if (b.write) AddWriteBuffer(b.buffer);
        }
      }
    }

    public List<FieldInfo> texture_flds;
    public StrBldr drawSphere, RWStructuredBuffers, classArrays, compute_groupshared, base_groupshared, addComputeBuffers, initBuffers,
      renderObject, RenderSetValues, Texture2Ds;

    public class buffer_data
    {
      public string name; public SizeInfo sizeInfo; public MemberInfo member; public int MaxMem_GB;
      public string GetTypeName => member.GetTypeName();
      public string comment => member.AttGS()?.Description ?? "";

      public bool fullRace, halfRace;
      public bool isFullRace => member.AttGS()?.FullRace ?? false;
      public bool isNotFullRace => !isFullRace;
      public bool isHalfRace => member.AttGS()?.HalfRace ?? false;
      public bool isNotHalfRace => !isHalfRace;
      public bool isGroupShared => member.AttGS()?.GroupShared ?? false;
      public bool isNotGroupShared => !isGroupShared;
    }
    public buffer_data[] buffers;

    bool generateMaterialShader;
    public void Calc_Buffers()
    {
      texture_flds = _GS_fields.Where(a => a.FieldType == typeof(Texture2D)).Select(a => a).ToList();

      (drawSphere, Texture2Ds, RWStructuredBuffers, compute_groupshared, base_groupshared, addComputeBuffers, RenderSetValues, renderObject) = StrBldr();

      foreach (var fld in texture_flds) Texture2Ds.Add($"\n  public Texture2D {fld.Name};");
      var (s, gParams) = StrBldr();
      addComputeBuffers.Add(
        $"\n    AddComputeBuffer(ref g{Name}, nameof(g{Name}), 1);",
         "\n    InitKernels();");

      for (int i = 0; i < kernel_methods.Count; i++)
      {
        var k = kernel_methods[i];
        if (i % 16 == 0) addComputeBuffers.Add($"\n    SetKernelValues(g{Name}, nameof(g{Name})");
        addComputeBuffers.Add($", kernel_{k.methodName}");
        if (i % 16 == 15 || i == kernel_methods.Count - 1) addComputeBuffers.Add(");");
      }

      foreach (var buff in buffers)
      {
        string bufferType = buff.GetTypeName, bufferName = buff.name, bufferComment = buff.comment.IsEmpty() ? "" : $"//{buff.comment}";
        if (bufferType.Contains("[]")) bufferType = bufferType.Replace("[]", "");
        if (bufferType == "Double") bufferType = "double";
        if (buff.isGroupShared)
        {
          string groupsharedName = bufferName;
          string size = buff.sizeInfo.sizeN;
          if (size.IsEmpty()) size = buff.member.AttGS()?.GroupShared_Size.ToString() ?? "";
          base_groupshared.Add($"\n  [HideInInspector] public {bufferType}[] {bufferName} = new {bufferType}[{size}];");
          compute_groupshared.Add($"\n  groupshared {bufferType} {bufferName}[{size}];");
        }
        else
        {
          if (drawSphere == "") drawSphere.Set("\n    if (i < (n = 1)) o = vert_DrawSphere(f000, 1, palette(0.5f), i, j, o); i -= n;// *********** green sphere test ************");
          StackFields(RWStructuredBuffers, $"RWStructuredBuffer<{bufferType}>", bufferName, "  ");
          if (buff.member.IsProp())
          {
            string bufferN = buff.sizeInfo.sizeN;
            if (bufferN.IsNotEmpty())
            {
              if (bufferN.Contains(".") && bufferN.After(".").DoesNotContainAny("x", "y", "z")) bufferN = bufferN.Replace(".", "_");
              addComputeBuffers.Add($"\n    AddComputeBuffer(ref {bufferName}, nameof({bufferName}), {bufferN});{bufferComment}");
            }
          }
        }
      }
      classArrays = StrBldr();
      var cFlds = _GS_fields.Select(a => new { a, att = a.AttGS() }).Where(a => (a.a.FieldType.IsArray && a.a.FieldType.GetElementType().IsClass && ((a.att != null && !a.att.isGS_Buffer) || a.att == null))
       && a.a.FieldType != typeof(Texture2D)).Select(a => a.a).ToArray();
      foreach (var cFld in cFlds)
      {
        var att = cFld.AttGS();
        string description = att?.Description ?? "";
        string fName = cFld.Name;
        if (fName.Contains("+")) fName = fName.After("+");
        string fldType = cFld.FieldType.ToString();
        if (fldType.Contains("+")) fldType = fldType.After("+");

        if (cFld.FieldType.IsArray)
        {
          string fldTypeStr = cFld.FieldType.FullName.Before("[]");
          Type arrayType = fldTypeStr.ToType();
          if (arrayType?.IsClass ?? false) { }
          else StackFields(RWStructuredBuffers, $"RWStructuredBuffer<{fldType.Before("[]")}>", fName, "  ");
        }
        else StackFields(classArrays, fldType, fName, "  ");
      }

      var (s_InitBuffers0, s_InitBuffers1) = StrBldr();
      foreach (var lib_fld in lib_flds) if (lib_fld.isInternal_Lib()) { s_InitBuffers0.Add($"\n    {lib_fld.Name}_InitBuffers0_GS();"); s_InitBuffers1.Add($"\n    {lib_fld.Name}_InitBuffers1_GS();"); }
      virtual_method(s_InitBuffers0, "InitBuffers0_GS()", s_InitBuffers1, "InitBuffers1_GS()");

      initBuffers = StrBldr(
  "\n  public virtual void InitBuffers()",
  "\n  {",
  "\n    InitBuffers0_GS();", addComputeBuffers,
  "\n    InitBuffers1_GS();",
  "\n    isInitBuffers = true;",
  "\n  }", s_InitBuffers0, s_InitBuffers1,
  "");

      generateMaterialShader = render_methods.Length > 0;

      renderObject.Clear();
      if (generateMaterialShader)
      {
        foreach (var b in buffers) if (b.name.DoesNotStartWith("groupshared_")) gParams.Add($", new {{ {b.name} }}");
        foreach (var b in texture_flds) gParams.Add($", new {{ {b.Name} }}");
        RenderSetValues.Set($"material, new {{ g{Name} }}{gParams}, new {{ _PaletteTex }}");

        renderObject.Add(
          "\n  public virtual bool onRenderObject_GS_N(bool cpu)",
          "\n  {",
          "\n    RenderQuads(material, onRenderObject_LIN_Quads(0).z);",
          "\n    RenderPoints(material, onRenderObject_LIN_Points(0).z);",
          "\n    return true;",
          "\n  }",
          "\n  public override bool onRenderObject()",
          "\n  {",
         $"\n    if (g{Name} == null) return false;",
          "\n    bool render = true, cpu = false;",
          "\n    onRenderObject_GS(ref render, ref cpu);",
          "\n    g_SetData();",
          "\n    if (!render) return false;",
         $"\n    if (cpu) Cpu({RenderSetValues});",
         $"\n    else Gpu({RenderSetValues});",
          "\n    return onRenderObject_GS_N(cpu);",
          "\n  }",
          "");
      }
      var s_onRenderObject = StrBldr();
      foreach (var lib_fld in lib_flds)
        if (lib_fld.isInternal_Lib()) s_onRenderObject.Add($"\n    {lib_fld.Name}_onRenderObject_GS(ref render, ref cpu);");
      virtual_method(s_onRenderObject, "onRenderObject_GS(ref bool render, ref bool cpu)");
      renderObject.Add(s_onRenderObject);
    }

    public bool generateComputeShader;
    StrBldr compute_or_material_shader;
    class RenderMethod { public bool renderQuads; public MethodInfo method; public string methodN, methodName, methodUint, size, showIf; public int3 dimension; public int dimensionN; public string[] threadN; }
    RenderMethod[] render_methods;
    public void Get_compute_or_material_shader()
    {
      if (_GS_methods == null) return;
      var sizeMatches = _GS_Code.RegexMatch($@"\[GS_UI, AttGS\((.*?)\)\].*?\bvoid vert_(.*?)\(\).*?Size\((.*?)\);");
      render_methods = new RenderMethod[sizeMatches.Count];
      for (int methodI = 0; methodI < render_methods.Length; methodI++)
      {
        Match m = sizeMatches[methodI];
        var o = GetSizeInfo(m.Group(3));
        var method = _GS_methods.Where(a => a.Name == "vert_" + m.Group(2)).Select(a => a).FirstOrDefault();
        if (method != null)
        {
          var showIfObj = method.AttGS().ShowIf?.ToString().Replace("False", "false", "True", "true");
          string showIf = showIfObj != null ? showIfObj + ", " : "";
          render_methods[methodI] = new RenderMethod() { renderQuads = method.AttGS().RenderQuads, method = method, methodN = o.sizeN, methodName = method.Name, methodUint = o.uintN, size = o.sizeN, showIf = showIf, dimension = o.dimension, dimensionN = dot(o.dimension, i111), threadN = o.threadN, };
        }
      }

      var rMethods = new List<RenderMethod>();
      for (int i = render_methods.Length - 1; i >= 0; i--)
      {
        var m = render_methods[i];
        if (m != null)
        {
          if (!m.renderQuads)
            rMethods.Add(m);
          else
            rMethods.Insert(0, m);
        }
      }
      render_methods = rMethods.ToArray();

      Calc_Buffers();
      generateComputeShader = buffers.Length > 0 || kernel_methods.Count > 0;

      compute_or_material_shader = StrBldr(
        "\n  [Serializable]", gStruct,
       $"\n  public RWStructuredBuffer<G{Name}> g{Name};", declare_structs, RWStructuredBuffers,
        "\n  public Texture2D _PaletteTex;",
        "\n  public float4 palette(float v) => paletteColor(_PaletteTex, v);",
        "\n  public float4 palette(float v, float w) => float4(palette(v).xyz, w);",
        "\n", Texture2Ds,
        "\n  public virtual void onRenderObject_LIN(bool show, uint _itemN, ref uint i, ref uint index, ref uint3 LIN) { uint n = 0; if (show) { if (i < (n = _itemN)) LIN = uint3(index, i, 0); LIN.z += n; i -= n; } index++; }",
        "\n  public virtual void onRenderObject_LIN(uint _itemN, ref uint i, ref uint index, ref uint3 LIN) { onRenderObject_LIN(true, _itemN, ref i, ref index, ref LIN); }",
        "\n  public virtual uint3 onRenderObject_LIN(uint i) { uint3 LIN = u000; uint index = 0;");
      foreach (var m in render_methods)
        if (m != null)
          compute_or_material_shader.Add($" onRenderObject_LIN({m.showIf}{m.size}, ref i, ref index, ref LIN);");
      compute_or_material_shader.Add(" return LIN; }",
        "\n  public virtual uint3 onRenderObject_LIN_Quads(uint i) { uint3 LIN = u000; uint index = 0;");
      foreach (var m in render_methods) if (m != null && m.renderQuads) compute_or_material_shader.Add($" onRenderObject_LIN({m.showIf}{m.size}, ref i, ref index, ref LIN);");
      compute_or_material_shader.Add(" return LIN; }",
        "\n  public virtual uint3 onRenderObject_LIN_Points(uint i) { uint3 LIN = u000; uint index = 0;");
      foreach (var m in render_methods) if (m != null && !m.renderQuads) compute_or_material_shader.Add($" onRenderObject_LIN({m.showIf}{m.size}, ref i, ref index, ref LIN);");
      compute_or_material_shader.Add(" return LIN; }");
    }

    public StrBldr kernels, kernels_, kernelWrappers, kernelRunMethods;
    public void Calc_kernels()
    {
      (kernels, kernels_, kernelWrappers, kernelRunMethods) = StrBldr();

      if (generateComputeShader)
      {
        if (kernel_methods != null)
        {
          foreach (var buffer in buffers)
          {
            if (buffer.isNotGroupShared)
            {
              kernelWrappers.Add($"\n  public virtual void {buffer.name}_SetKernels(bool reallocate = false) {{ if ({buffer.name} != null && (reallocate || {buffer.name}.reallocated)) {{ SetKernelValues({buffer.name}, nameof({buffer.name})");
              foreach (var k in kernel_methods) foreach (var b in k.buffers) if (b.buffer == buffer) { kernelWrappers.Add($", kernel_{k.methodName}"); break; }
              kernelWrappers.Add($"); {buffer.name}.reallocated = false; }} }}");
            }
          }
          foreach (var k in kernel_methods)
          {
            string uintN = k.sizeInfo.uintN;
            int3 dimension = k.dimension; int dimensionN = k.dimensionN; string[] threadN = k.threadN;
            bool sync = k.sync;

            string numThreads = dimensionN <= 1 ? "numthreads1, 1, 1" : dimensionN == 2 ? "numthreads2, numthreads2, 1" : "numthreads3, numthreads3, numthreads3";
            string thread1 = threadN[0], thread2 = threadN[1], thread3 = threadN[2];
            var (id_less, id_to_i) = StrBldr();
            if (dimension.z == 1) id_less.Add($"id.z < {thread3} && ");
            if (dimension.y == 1) id_less.Add($"{(dimension.x == 1 ? "id.y" : "id.z")} < {thread2} && "); else if (dimension.y == 2) id_less.Add($"id.yz < {thread2} && ");
            if (dimension.x == 1) id_less.Add($"id.x < {thread1}"); else if (dimension.x == 2) id_less.Add($"all(id.xy < {thread1})"); else if (dimension.x == 3) id_less.Add($"all(id < {thread1})");
            if (all(dimension == i111)) id_to_i.Add($"{thread1} * ({thread2} * id.z + id.y) + id.x");
            else if (all(dimension == i10_ + 1)) id_to_i.Add($"{thread1}.x * ({thread1}.y * id.z + id.y) + id.x");
            else
            {
              if (dimension.z == 1) id_to_i.Add($"id.z * {thread1} * {thread2} + ");
              if (dimension.y == 1) id_to_i.Add($"id.y * {thread1} + "); else if (dimension.y == 2) id_to_i.Add($"id_to_i(id.yz, {thread2}) + ");
              if (dimension.x == 1) id_to_i.Add($"id.x"); else if (dimension.x >= 2) id_to_i.Add($"id_to_i(id, {thread1})");
            }

            string args = sync ? "uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI" : "uint3 id";
            string pass_args = sync ? "grp_tid, grp_id, id, grpI" : "id";

            string kernel_code = sync ? "yield return GroupMemoryBarrierWithGroupSync(); " : "";

            string kernel_return = sync ? "IEnumerator" : "void";
            string kernel_coroutine = sync ? "yield return StartCoroutine(" : "";
            id_less.Set($"if ({id_less}) ");
            kernels_.Add($"\n  [HideInInspector] public int kernel_{k.methodName}; [numthreads({numThreads})] protected {kernel_return} {k.methodName}({args}) {{ {id_less}{kernel_coroutine}{k.methodName}_GS({pass_args}){(sync ? ")" : "")}; }}");
            if (libKernels.DoesNotContain(k.methodName + "_GS"))
              kernels_.Add($"\n  public virtual {kernel_return} {k.methodName}_GS({args}) {{{(sync ? " yield return null;" : "")} }}");

            kernels.Add($"  public override {kernel_return} {k.methodName}_GS({args}) {{ {kernel_coroutine}base.{k.methodName}_GS({pass_args});}}\n");

            kernelWrappers.Add($"\n  public virtual void Gpu_{k.methodName}() {{ g_SetData();");
            foreach (var buffer in k.buffers)
            {
              string buff = buffer.buffer.name;
              if (buffer.buffer.isNotGroupShared)
              {
                if (buffer.read && buff != "_PaletteTex") { string b = buff == "this" ? $"g{Name}" : buff; kernelWrappers.Add($" {b}?.SetCpu();"); }
                if ((buffer.read || buffer.write) && buff != "this") kernelWrappers.Add($" {buff}_SetKernels();");
              }
            }
            if (k.thisWrite) kernelWrappers.Add($" g{Name}?.SetCpu();");
            kernelWrappers.Add($" Gpu(kernel_{k.methodName}, {k.methodName}, {uintN});");
            foreach (var buffer in k.buffers)
              if (buffer.write && buffer.buffer.isNotGroupShared)
              {
                string buff = buffer.buffer.name;
                if (buff == "this") kernelWrappers.Add($" g = G;");
                else if (buff != "_PaletteTex") kernelWrappers.Add($" {buff}?.ResetWrite();");
              }
            if (k.thisWrite) kernelWrappers.Add($" g = G;");
            kernelWrappers.Add(" }");

            var (cpuWrite, cpuRead) = StrBldr();
            foreach (var buffer in k.buffers)
              if (buffer.buffer.isNotGroupShared)
                if (buffer.read || buffer.write)
                {
                  string buff = buffer.buffer.name;
                  if (buff == "ints") buff += "";
                  if (buff != "this" && buff != "_PaletteTex") cpuWrite.Add($" {buff}?.GetGpu();");
                }

            foreach (var buffer in k.buffers)
              if (buffer.write && buffer.buffer.isNotGroupShared)
              {
                string buff = buffer.buffer.name;
                if (buff != "this" && buff != "_PaletteTex") cpuRead.Add($" {buff}.SetData();");
              }
            if (sync) kernelWrappers.Add($"\n  public virtual IEnumerator Cpu_{k.methodName}() {{{cpuWrite} yield return StartCoroutine(Cpu_Coroutine(kernel_{k.methodName}, {k.methodName}, {uintN})); }}");
            else kernelWrappers.Add($"\n  public virtual void Cpu_{k.methodName}() {{{cpuWrite} Cpu({k.methodName}, {uintN});{cpuRead} }}");
            kernelWrappers.Add($"\n  public virtual void Cpu_{k.methodName}({args}) {{{cpuWrite} {k.methodName}({pass_args});{cpuRead} }}");
          }
        }
      }
    }
    public string SimplifyType(AttGS att, string typeName) => (att != null ? "UI_" : "") + typeName.SimplifyType();
    public string SimplifyType(AttGS att, Type typ) => SimplifyType(att, typ.ToString());

    bool _cs_Write()
    {
      valueChanges.Clear();
      var (showIfs, update, lateUpdate, lateUpdate_keys, lateUpdate_ValuesChanged, onValueChanged, dataWrappers) = StrBldr();
      _GS_nestedTypes = _GS_Type.GetNestedTypes(_GS_bindings);
      Calc_enums(); Calc_consts(); Calc_structs(); Get_compute_or_material_shader(); Calc_kernels();

      var (vertDrawSegments, virtual_verts, vertCode, s_frag, fragCode) = StrBldr();
      string regions = _cs_lib_regions;
      int libI = 0;
      var libNames = lib_flds.Select(a => a.Name).ToList();
      for (int i = 0; i < libNames.Count; i++)//detect if lib is or has BDraw, and make that libI == 0
        if (_GS_Code.Contains($"{libNames[i]}_BDraw_")) { if (i > 0) { var item = libNames[i]; libNames.RemoveAt(i); libNames.Insert(0, item); } break; }

      for (int i = 0; i < render_methods.Length; i++)
      {
        var m = render_methods[i];

        if (m != null)
        {
          libI = libNames.Select((a, i) => new { a, i }).Where(a => m.methodName.After("vert_").StartsWith(a.a + "_")).Select(a => a.i).FirstOrDefault();
          var elseStr = i == 0 ? "" : "else ";
          if (i == 0) vertDrawSegments.Add("\n    uint3 LIN = onRenderObject_LIN(i); int index = -1, level = ((int)LIN.x); i = LIN.y;");
          vertDrawSegments.Add($"\n    {elseStr}if (level == ++index) {{ o = {m.methodName}(i, j, o); o.tj.x = {libI}; }}");
          string methodDeclaration = $"public virtual v2f {m.methodName}(uint i, uint j, v2f o)";
          //if (regions.DoesNotContain(methodDeclaration))
          //  virtual_verts.Add($"\n  {methodDeclaration} {{ return o; }}");
          if (regions.DoesNotContain(methodDeclaration))
            virtual_verts.Add($"\n  {methodDeclaration} => o;");
        }
      }
      vertCode.Add(
"\n  public virtual v2f vert_GS(uint i, uint j, v2f o)",
"\n  {", vertDrawSegments,
"\n    return o;",
"\n  }",
"\n  public v2f vert(uint i, uint j)",
"\n  {",
"\n    v2f o = default;",
"\n    o.pos = o.color = o.tj = o.tk = f0000; o.normal = o.p0 = o.p1 = o.wPos = f000; o.uv = f00; o.ti = float4(i, j, 0, 0);",
"\n    return vert_GS(i, j, o);",
"\n  }");

      libI = 0;
      foreach (var lib_fld in lib_flds)
        if (lib_fld.isInternal_Lib())
        {
          string path = $"{AssetsPath}GS_Libs/{lib_fld.FieldType}/Lib/";
          if (path.Exists()) s_frag.Add($"\n    if (libI == {libI}) return frag_{lib_fld.Name}_GS(i, color);");
          libI++;
        }

      fragCode.Add(
"\n  public virtual float4 frag_GS(v2f i, float4 color)",
"\n  {",
"\n    uint libI = roundu(i.tj.x);", s_frag,
"\n    return color;",
"\n  }",
"\n  public virtual float4 frag(v2f i)",
"\n  {",
"\n    float4 color = i.color;",
"\n    color = frag_GS(i, color);",
"\n    if (color.a == 0) Discard(0);",
"\n    return color;",
"\n  }");

      if (gpu_flds != null)
      {
        foreach (var f in gpu_flds)
        {
          var att = f.AttGS();
          string typ = SimplifyType(att, f.FieldType), n = f.Name;
          bool isEnum = f.FieldType.IsEnum;
          bool isUI = typ.StartsWith("UI_");
          if (isUI) typ = typ.After("UI_");
          string r = isEnum ? $"({typ})g.{n}" : typ == "bool" ? $"Is(g.{n})" : $"g.{n}";
          string assignFld = isEnum ? "(uint)value" : "value";
          string si = UI_VisualElement.GetUnitConversion(att);
          if (si == "Null") si = "si";
          if (typ.StartsWith("string")) assignFld = "";
          else if (isUI)
          {
            if (typ == "bool") assignFld = $"g.{n} = Is(UI_{n}.v = {assignFld}); ";
            else if (typ.StartsWith("float")) assignFld = $"g.{n} = UI_{n}.{si} = {assignFld}; ";
            else if (isEnum) assignFld = $"g.{n} = UI_{n}.v = {assignFld}; ";
            else assignFld = $"g.{n} = UI_{n}.v = {assignFld}; ";
          }
          else if (isEnum) assignFld = $"g.{n} = {assignFld}; ";
          else assignFld = typ == "bool" ? $"g.{n} = Is({assignFld}); " : $"g.{n} = {assignFld}; ";
          string compare = "";
          if (isEnum) compare = $"({typ})g.{n} != value";
          else if (typ.StartsWith("float")) compare = $"any(g.{n} != value)";
          else if (typ.EndsWithAny("2", "3", "4")) compare = $"any(g.{n} != value)";
          else if (typ == "bool") compare = $"g.{n} != Is(value)";
          else compare = $"g.{n} != value";
          if (isUI)
          {
            if (isEnum) compare += $" || ({typ})UI_{n}.v != value";
            else if (typ.StartsWith("float")) compare += $" || any(UI_{n}.{si} != value)";
            else if (typ.EndsWithAny("2", "3", "4")) compare += $" || any(UI_{n}.v != value)";
            else if (typ == "bool") compare += $" || UI_{n}.v != value";
            else compare += $" || UI_{n}.v != value";
          }

          if (typ.EndsWithAny("2x2", "3x3", "4x4"))
          {
            if (typ.StartsWith("UnityEngine.")) typ = typ.After("UnityEngine.");
            dataWrappers.Add($"\n  public {typ} {n} {{ get => {r}; set {{ {assignFld}}} }}");
          }
          else if (typ != nameof(TreeGroupEnd))
            dataWrappers.Add($"\n  public virtual {typ} {n} {{ get => {r}; set {{ if ({compare}) {{ {assignFld}ValuesChanged = gChanged = true; }} }} }}");
        }
      }

      _GS_fields?.Where(a => a.FieldType == typeof(TreeGroup)).ForEach(f => dataWrappers.Add($"\n  public bool {f.Name} {{ get => UI_{f.Name}?.v ?? false; set {{ if (UI_{f.Name} != null) UI_{f.Name}.v = value; }} }}"));

      StrBldr G_Str = StrBldr("\n  [HideInInspector] public G", Name, " g; public G", Name, " G { get { g", Name, ".GetData(); return g", Name, "[0]; } }");
      if (generateComputeShader || generateMaterialShader)
        G_Str.Add("\n  public void g_SetData() { if (gChanged && g", Name, " != null) { g", Name, "[0] = g; g", Name, ".SetData(); gChanged = false; } }");

      StrBldr tData = StrBldr("\n  [Serializable]", "\n  public class uiData", "\n  {");
      StackFields(tData, "bool", "siUnits");

      var (ui_to_data, Load_UI, data_to_ui, data_to_ui_Defaults, gridWrapper, OnGrid, onMethodClicked, clickedMethods) = StrBldr();

      ui_to_data.Add($"\n    data.siUnits = siUnits;");
      if (_gs_members != null)
        foreach (var gs_member in _gs_members)
        {
          AttGS attGS = gs_member.AttGS();
          if (attGS?.GroupShared ?? false)
            continue;
          Type gs_memberType = gs_member.GetMemberType();
          FieldInfo _GS_field = gs_member.IsFld() ? gs_member.Fld() : null;
          PropertyInfo _GS_prop = gs_member.IsProp() ? gs_member.Prop() : null;
          MethodInfo _GS_method = gs_member.IsMethod() ? gs_member.Method() : null;
          Type _GS_fieldType = _GS_field?.FieldType;
          Type _GS_propType = _GS_prop?.PropertyType;
          string m_name = gs_member.Name;
          string m_typeStr = gs_memberType.SimplifyType();

          if (gs_member.IsMethod())
          {
            if (attGS.Key.IsNotEmpty())
            {
              string modifiers = attGS.Key.Contains("(") ? attGS.Key.Before("(") + ", " : "";
              string key = attGS.Key.Contains("(") ? attGS.Key.Between("(", ")") : attGS.Key;
              if (attGS.isSync) lateUpdate_keys.Add($"\n    if (GetKeyDown({modifiers}'{key}')) StartCoroutine({gs_member.Name}());");
              else lateUpdate_keys.Add($"\n    if (GetKeyDown({modifiers}'{key}')) {gs_member.Name}();");
            }
            UI_method._cs_Write(_GS_fieldType, gs, tData, lateUpdate, lateUpdate_ValuesChanged, showIfs, onValueChanged, attGS, m_typeStr, gs_member.Name);
            if (attGS.ShowIf != null)
            {
              if (gs_member.Name.DoesNotStartWith("vert_"))
                onValueChanged.Add($"\n    UI_{gs_member.Name}.DisplayIf(Show_{gs_member.Name} && UI_{gs_member.Name}.treeGroup_parent.isExpanded);");
            }
          }
          if (gs_member.IsFld() || gs_member.IsClass())
          {
            UI_VisualElement._cs_Write(_GS_fieldType, gs, tData, lateUpdate, lateUpdate_ValuesChanged, showIfs, onValueChanged, attGS, m_typeStr, m_name);
            if (attGS?.ShowIf != null)
            {
              if (m_name.StartsWith("group_"))
                onValueChanged.Add($"\n    UI_{m_name}.DisplayIf((UI_{m_name}.isShowing = Show_{m_name}) && UI_{m_name}.treeGroup_parent.isExpanded);");
              else if (m_name.DoesNotStartWith("vert_"))
              {
                if (_GS_fieldType.IsArray)
                {
                  if (attGS != null && attGS.ShowIf != null)
                  {
                    StrBldr s = new StrBldr("\n  public virtual bool Show_", m_name, " { get => ", attGS.ShowIf.ToString().ReplaceAll("True", "true", "False", "false"), "; }");
                    if (showIfs.ToString().DoesNotContain(s)) showIfs.Add(s);
                  }
                  onValueChanged.Add($"\n    UI_grid_{m_name}.DisplayIf(Show_{m_name} && UI_grid_{m_name}.treeGroup_parent.isExpanded);");
                }
                else
                  onValueChanged.Add($"\n    UI_{m_name}.DisplayIf(Show_{m_name} && UI_{m_name}.treeGroup_parent.isExpanded);");
              }
            }
            if (_GS_fieldType != null && _GS_fieldType.IsEnum) m_typeStr = "enum";
            if (m_typeStr != nameof(TreeGroupEnd))
            {
              if (!_GS_fieldType.isNestedClass())
              {
                string defaultStr = "";
                if (attGS != null && attGS.Val != null)
                {
                  if (m_typeStr.IsAny("string", "strings"))
                    defaultStr = $"\"{attGS.Val}\"";
                  else if (m_typeStr.IsAny("float2", "float3", "float4", "int2", "int3", "int4", "uint2", "uint3", "uint4"))
                    defaultStr = $"{m_typeStr}(\"{attGS.Val}\")";
                  else if (m_typeStr == "float") defaultStr = $"{attGS.Val}f";
                  else if (m_typeStr == "enum") defaultStr = $"{_GS_fieldType.Name}.{attGS.Val}";
                  else defaultStr = $"{attGS.Val}".ReplaceAll("True", "true", "False", "false");
                }
                if (m_typeStr.DoesNotStartWith("gs"))
                {
                  string v = "";
                  ui_to_data.Add($"\n    data.{m_name}{v} = {m_name};");
                  if (defaultStr.IsEmpty()) data_to_ui.Add($"\n    {m_name} = data.{m_name}{v};");
                  else data_to_ui.Add($"\n    {m_name} = ui_txt_str.Contains(\"\\\"{m_name}\\\"\") ? data.{m_name}{v} : {defaultStr};");
                  if (m_typeStr == "TreeGroup" && Load_UI.IsEmpty()) Load_UI.Add($"\n    UI_{m_name}?.Display_Tree();");
                }

                if (_GS_fieldType.IsArray)
                {
                  dataWrappers.Add($"\n  public UI_grid UI_grid_{m_name};");
                  if (_GS_fieldType.IsClass)
                  {
                    string className = m_typeStr.Before("[]");
                    dataWrappers.Add($"\n  public {m_typeStr} {m_name};");
                    var typeStr = m_typeStr == "strings" ? "string" : m_typeStr;
                    tData.Add($"\n    public {typeStr} {m_name};",
                              $"\n    public bool[] {m_name}_DisplayCols;",
                              $"\n    public float {m_name}_VScroll;",
                              $"\n    public uint {m_name}_DisplayRowN;",
                              $"\n    public bool {m_name}_isExpanded;",
                              $"\n    public string {m_name}_selectedRows;",
                              $"\n    public int {m_name}_lastClickedRow;",
                              "");
                    ui_to_data.Add($"\n    data.{m_name}_DisplayCols = UI_grid_{m_name}?.displayColumns?.Select(a => a.v).ToArray() ?? default;",
                                   $"\n    data.{m_name}_VScroll = UI_grid_{m_name}?.VScroll.value ?? default;",
                                   $"\n    data.{m_name}_DisplayRowN = UI_grid_{m_name}?.dispRowN.v ?? default;",
                                   $"\n    data.{m_name}_isExpanded = UI_grid_{m_name}?.isExpanded ?? default;",
                                   $"\n    data.{m_name}_lastClickedRow = UI_grid_{m_name}?.lastClickedRow ?? default;",
                                   $"\n    data.{m_name}_selectedRows = UI_grid_{m_name}?.isRowSelected.bools_to_RangeStr() ?? default;",
                                    "");

                    var (ui_to_array, array_to_ui, grid_item_OnValueChanged, grid_OnValueChanged, grid_Cols, grid_ShowIf) = StrBldr();
                    string aTypeStr = _GS_fieldType.ToString().Before("[]");
                    Type arrayType = aTypeStr.ToType();
                    var arrayFlds = arrayType?.GetFields(bindings);
                    uint row = 0;
                    ui_to_array.Add(
                      $"\n      var ui = UI_{m_name}[i];",
                      $"\n      var row = {m_name}[i + startRow];");
                    grid_OnValueChanged.Add(
                      $"\n    var ui = UI_{m_name}[row];",
                      $"\n    if (row + startRow >= {m_name}.Length) return;",
                      $"\n    var data = {m_name}[row + startRow];",
                      "");
                    array_to_ui.Add(ui_to_array);

                    var classType = _GS_fieldType.GetElementType();
                    var classMembers = classType.GetOrderedMembers();
                    int gridCol = 0;

                    onMethodClicked.Clear();
                    string else_str = "";

                    foreach (var classMember in classMembers)
                    {
                      if (classMember.IsFld())
                      {
                        Type fldTyp = classMember.Fld().FieldType;
                        string fldName = classMember.Name;
                        string v = "", arrayFldType = fldTyp.SimplifyType();
                        array_to_ui.Add($"\n      ui.{fldName} = {(fldTyp.IsEnum ? $"({arrayFldType})" : "")}row.{fldName}{v};");

                        bool isEnum = fldTyp.IsEnum;
                        string typ = arrayFldType, n = fldName;
                        if (arrayFldType == "strings") arrayFldType = "string";

                        string compare = $"data.{n} != ui.{n}";
                        if (typ.StartsWith("float") || typ.EndsWithAny("2", "3", "4")) compare = $"any({compare})";
                        else if (isEnum) compare = $"({typ}){compare}";

                        string enumCast = isEnum ? "(uint)" : "";
                        string enumTypeCast = isEnum ? $"({typ})" : "";
                        grid_OnValueChanged.Add(
      $"\n    {(grid_OnValueChanged.ToString().Contains("(col == ") ? "else " : "")}",
      $"if (col == {m_name}_{fldName}_Col && {compare}) {{ var v = {enumTypeCast}data.{fldName}; data.{fldName} = {enumCast}ui.{fldName}; ",
      $"{m_name}[row + startRow] = data; {m_name}_{fldName}_OnValueChanged(row + startRow, v); }}");

                        if (grid_Cols.IsEmpty()) grid_Cols.Add($"\n  public const int {m_name}_{fldName}_Col = {gridCol}");
                        else grid_Cols.Add($", {m_name}_{fldName}_Col = {gridCol}");

                        var att = classMember.AttGS();
                        if (att.ShowIf != null)
                          grid_ShowIf.Add($"\n    if (col == {m_name}_{fldName}_Col) return {att.ShowIf.ToString().ReplaceAll("False", "false", "True", "true")};");
                        ui_to_array.Add($"\n      row.{fldName}{v} = {enumCast}ui.{fldName};");
                        grid_item_OnValueChanged.Add($"\n  public virtual void {m_name}_{fldName}_OnValueChanged(int row, {arrayFldType} previousValue) {{ }}");
                      }
                      else if (classMember.IsMethod())
                      {
                        onMethodClicked.Add($"\n    {else_str}if (name == \"{classMember.Name}\") {m_name}_{classMember.Name}(UI_grid.StartRow + row);");
                        else_str = "else ";
                        clickedMethods.Add($"\n  public virtual void {m_name}_{classMember.Name}(int row) {{ }}");
                      }
                      gridCol++;
                    }

                    ui_to_array.Add($"\n      {m_name}[i + startRow] = row;");

                    grid_Cols.Add(";");
                    if (grid_ShowIf.IsNotEmpty()) grid_ShowIf.Set(
                      $"\n    if (row >= 0) row += UI_grid_{m_name}.StartRow;",
                      $"\n    {className} gridRow = {m_name} == null || row < 0 || row >= {m_name}.Length ? new {className}() : {m_name}[row];{grid_ShowIf}");

                    OnGrid.Add(
    $"\n  public List<{className}> {m_name}_CopyPaste;",

    $"\n  public virtual void {m_name}_OnButtonClicked(int row, int col)",
    "\n  {",
    $"\n    var UI_grid = UI_grid_{m_name};",
    $"\n    var name = UI_grid.RowItems[row][col].name.After(\"UI_method_\").BeforeLast(\"_\");", onMethodClicked,
    "\n  }",

    $"\n  public virtual void UI_To_{m_name}()",
     "\n  {",
    $"\n    if ({m_name} == null) return;",
    $"\n    var UI_grid = UI_grid_{m_name};",
    $"\n    int startRow = UI_grid.StartRow, i1 = min(UI_grid.DisplayRowN, {m_name}.Length - startRow);",
    $"\n    i1 = min(i1, UI_grid.RowItems.Count);",
    $"\n    for (int i = 0; i < i1; i++)",
     "\n    {", ui_to_array,
     "\n    }",
     "\n  }",

    $"\n  protected bool in_{m_name}_To_UI = false;",
    $"\n  public virtual bool {m_name}_To_UI()",
     "\n  {",
    $"\n    if ({m_name} == null || in_{m_name}_To_UI) return false;",
    $"\n    in_{m_name}_To_UI = true;",
    $"\n    var UI_grid = UI_grid_{m_name};",
    $"\n    int startRow = UI_grid.StartRow, i1 = min(UI_grid.DisplayRowN, {m_name}.Length - startRow);",
    $"\n    i1 = min(i1, UI_grid.RowItems.Count);",
     "\n    for (int i = 0; i < i1; i++)",
     "\n    {", array_to_ui,
     "\n    }",
    $"\n    UI_grid.DrawGrid();",
    $"\n    in_{m_name}_To_UI = false;",
     "\n    return true;",
     "\n  }", grid_Cols,

    $"\n  public virtual bool {m_name}_ShowIf(int row, int col)",
    "\n  {", grid_ShowIf,
    "\n    return true;",
    "\n  }",

    $"\n  public virtual void {m_name}_OnAddRow() {{ var list = {m_name}.ToList(); list.Add(default); {m_name} = list.ToArray(); }}",
    grid_item_OnValueChanged,
    $"\n  public virtual void {m_name}_OnValueChanged(int row, int col)",
    "\n  {",
    $"\n    if (!ui_loaded) return;",
    $"\n    var UI_grid = UI_grid_{m_name};",
    $"\n    int startRow = UI_grid.StartRow;", grid_OnValueChanged,
    "\n  }",

    $"\n  public virtual int {m_name}_GetGridArrayLength() => {m_name}?.Length ?? 0;",

    $"\n  public virtual int {m_name}_SelectedRow",
    "\n  {",
    $"\n    get {{ for (int i = 0; i < {m_name}.Length; i++) if (UI_grid_{m_name}.isRowSelected[i]) return i; return -1; }}",
    "\n    set",
    "\n    {",
    $"\n      for (int i = 0; i < {m_name}.Length; i++) UI_grid_{m_name}.isRowSelected[i] = value == i;",
    $"\n      UI_grid_{m_name}.lastClickedRow = value;",
    $"\n      UI_grid_{m_name}.DrawGrid();",
    "\n    }",
    "\n  }",

    $"\n  public virtual int[] {m_name}_SelectedRows",
    "\n  {",
    $"\n    get => UI_grid_{m_name}.isRowSelected.Select((a, i) => new {{ a, i }}).Where(a => a.a).Select(a => a.i).ToArray();",
    "\n    set",
    "\n    {",
    $"\n      for (int i = 0; i < {m_name}.Length; i++) UI_grid_{m_name}.isRowSelected[i] = false;",
    $"\n      for (int i = 0; i < value.Length; i++) {{ int row = value[i]; UI_grid_{m_name}.isRowSelected[row] = true; UI_grid_{m_name}.lastClickedRow = row; }}",
    $"\n      UI_grid_{m_name}.DrawGrid();",
    "\n    }",
    "\n  }",

    $"\n  public virtual void {m_name}_OnCut()",
    "\n  {",
    $"\n    {m_name}_OnCopy();",
    $"\n    {m_name} = {m_name}.Except({m_name}_CopyPaste).ToArray();",
    $"\n    UI_grid_{m_name}.StartRow = min(UI_grid_{m_name}.StartRow, max(0, {m_name}_CopyPaste.Count - UI_grid_{m_name}.DisplayRowN));",
    $"\n    UI_grid_{m_name}.isRowSelected = new bool[{m_name}_CopyPaste.Count];",
    $"\n    UI_grid_{m_name}.DrawGrid();",
    "\n  }",
    $"\n  public virtual void {m_name}_OnCopy() {{ {m_name}_CopyPaste = {m_name}.Where((a, i) => UI_grid_{m_name}.isRowSelected[i]).Select(a => a).ToList(); }}",
    $"\n  public virtual void {m_name}_OnPaste()",
    "\n  {",
    $"\n    var list = {m_name}.ToList();",
    "\n    var newSelectedRows = new List<int>();",
    $"\n    for (int i = {m_name}.Length - 1; i >= 0; i--)",
    $"\n      if (UI_grid_{m_name}.isRowSelected[i])",
    "\n      {",
    $"\n        for (int j = 0; j < {m_name}_CopyPaste.Count; j++) {{ list.Insert(i + j, {m_name}_CopyPaste[j]); newSelectedRows.Add(i + j); }}",
    "\n        break;",
    "\n      }",
    $"\n    {m_name} = list.ToArray();",
    $"\n    UI_grid_{m_name}.isRowSelected = newSelectedRows.ToArray().ints_to_selected_bools(list.Count);",
    $"\n    {m_name}_To_UI();",
    "\n  }",
    $"\n  public virtual void {m_name}_OnInsert()",
    "\n  {",
    $"\n    var list = {m_name}.ToList();",
    "\n    var newSelectedRows = new List<int>();",
    $"\n    for (int i = 0; i < {m_name}.Length; i++) if (UI_grid_{m_name}.isRowSelected[i]) {{ list.Insert(i, default); newSelectedRows.Add(i); }}",
    $"\n    {m_name} = list.ToArray();",
    $"\n    UI_grid_{m_name}.isRowSelected = newSelectedRows.ToArray().ints_to_selected_bools(list.Count);",
    $"\n    {m_name}_To_UI();",
    "\n  }",
    $"\n  public virtual void {m_name}_OnDelete() {{ {m_name}_OnCut(); }}",
    $"\n  public virtual void {m_name}_OnUpArrow()",
    "\n  {",
    "\n    int row = 1;",
    $"\n    if ({m_name}.Length > 1 && !UI_grid_{m_name}.isRowSelected[0])",
    "\n    {",
    $"\n      var list = {m_name}.ToList();",
    "\n      var newSelectedRows = new List<int>();",
    $"\n      for (int i = 0; i < {m_name}.Length; i++)",
    $"\n        if (UI_grid_{m_name}.isRowSelected[i]) {{ var item = list[i]; list.RemoveAt(i); list.Insert(i - 1, item); newSelectedRows.Add(i - 1); }}",
    $"\n      {m_name} = list.ToArray();",
    $"\n      UI_grid_{m_name}.isRowSelected = newSelectedRows.ToArray().ints_to_selected_bools(list.Count);",
    $"\n      {m_name}_To_UI();",
    $"\n      row = clamp(newSelectedRows[0], 0, UI_grid_{m_name}.rowNumberButtons.Count - 1);",
    "\n    }",
    $"\n    var b = UI_grid_{m_name}.rowNumberButtons[row]; b.schedule.Execute(() => {{ b.Focus(); }});",
    "\n  }",
    $"\n  public virtual void {m_name}_OnDownArrow()",
    "\n  {",
    $"\n    var row = ^1;",
    $"\n    if ({m_name}.Length > 1 && !UI_grid_{m_name}.isRowSelected[^1])",
    "\n    {",
    $"\n      var list = {m_name}.ToList();",
    "\n      var newSelectedRows = new List<int>();",
    $"\n      for (int i = {m_name}.Length - 1; i >= 0; i--)",
    $"\n        if (UI_grid_{m_name}.isRowSelected[i]) {{ var item = list[i]; list.RemoveAt(i); list.Insert(i + 1, item); newSelectedRows.Add(i + 1); }}",
    $"\n      {m_name} = list.ToArray();",
    $"\n      UI_grid_{m_name}.isRowSelected = newSelectedRows.ToArray().ints_to_selected_bools(list.Count);",
    $"\n      {m_name}_To_UI();",
    $"\n      row = clamp(newSelectedRows[0], 0, UI_grid_{m_name}.rowNumberButtons.Count - 1);",
    "\n    }",
    $"\n    var b = UI_grid_{m_name}.rowNumberButtons[row]; b.schedule.Execute(() => {{ b.Focus(); }});",
    "\n  }",
    $"\n  public virtual void {m_name}_OnKeyDown(KeyDownEvent evt)",
    "\n  {",
    $"\n    //print($\"{m_name}_OnKeyDown, {{(evt.shiftKey ? \"Shift-\" : \"\")}}{{(evt.ctrlKey ? \"Ctrl-\" : \"\")}}{{(evt.altKey ? \"Alt-\" : \"\")}}{{evt.keyCode.ToString()}}\");",
    "\n  }",
    "");
                    data_to_ui.Add(
                      $"\n    UI_{m_name} = new _UI_{className}(this);",
                      $"\n    \"{m_name}_To_UI\".InvokeMethod(this);",
                      $"\n    for (int i = 0; i < data.{m_name}_DisplayCols?.Length && i < UI_grid_{m_name}.displayColumns.Count; i++)",
                       "\n    {",
                      $"\n      UI_grid_{m_name}.displayColumns[i].v = data.{m_name}_DisplayCols[i];",
                      $"\n      var item = UI_grid_{m_name}.RowItems[0][i];",
                      $"\n      if(UI_grid_{m_name}.headerButtons[i].unitLabel != null)",
                      $"\n        UI_grid_{m_name}.headerButtons[i].unitLabel.style.display = DisplayIf(item.siUnit != siUnit.Null || item.usUnit != usUnit.Null || item.Unit != Unit.Null);",
                       "\n    }",
                      $"\n    UI_grid_{m_name}.VScroll.value = data.{m_name}_VScroll;",
                      $"\n    UI_grid_{m_name}.dispRowN.v = data.{m_name}_DisplayRowN == 0 ? 5 : data.{m_name}_DisplayRowN;",
                      $"\n    UI_grid_{m_name}.isExpanded = data.{m_name}_isExpanded;",
                      $"\n    UI_grid_{m_name}.selectedRows = data.{m_name}_selectedRows;",
                      $"\n    UI_grid_{m_name}.lastClickedRow = data.{m_name}_lastClickedRow;",
                      $"\n    {m_name} ??= new {m_typeStr} {{ }};",
                      $"\n    UI_grid_{m_name}.isRowSelected = data.{m_name}_selectedRows.RangeStr_to_bools({m_name}.Length);",
                      $"\n    UI_grid_{m_name}.DrawGrid();",
                      "");
                    var arrayWrappers = StrBldr();
                    row = 0;
                    foreach (var arrayFld in arrayFlds)
                    {
                      typeStr = arrayFld.FieldType.SimplifyType();
                      if (arrayFld.FieldType.IsEnum)
                        arrayWrappers.Add($"\n      public UI_enum UI_{arrayFld.Name} => gs.UI_grid_{m_name}.RowItems[row][{row++}] as UI_enum;",
                                          $" public {typeStr} {arrayFld.Name} {{ get => ({typeStr})UI_{arrayFld.Name}.v; set => UI_{arrayFld.Name}.v = value.To_uint(); }}");
                      else if (typeStr == "strings")
                        arrayWrappers.Add($"\n      public UI_{typeStr} UI_{arrayFld.Name} => gs.UI_grid_{m_name}.RowItems[row][{row++}] as UI_{typeStr};",
                                          $" public string {arrayFld.Name} {{ get => UI_{arrayFld.Name}.v; set => UI_{arrayFld.Name}.v = value; }}");
                      else
                        arrayWrappers.Add($"\n      public UI_{typeStr} UI_{arrayFld.Name} => gs.UI_grid_{m_name}.RowItems[row][{row++}] as UI_{typeStr};",
                                          $" public {typeStr} {arrayFld.Name} {{ get => UI_{arrayFld.Name}.v; set => UI_{arrayFld.Name}.v = value; }}");
                    }

                    gridWrapper.Add(
    $"\n  public class _UI_{className}",
    "\n  {",
    $"\n    public class UI_{className}_Items",
    "\n    {",
    $"\n      public {gs.name}_ gs;",
    "\n      public int row;",
    $"\n      public UI_{className}_Items({gs.name}_ gs) {{ this.gs = gs; }}", arrayWrappers,
    "\n    }",
    $"\n    public {gs.name}_ gs;",
    $"\n    public UI_{className}_Items ui_{className}_Items;",
    $"\n    public _UI_{className}({gs.name}_ gs) {{ this.gs = gs; ui_{className}_Items = new UI_{className}_Items(gs); }}",
    $"\n    public UI_{className}_Items this[int i] {{ get {{ ui_{className}_Items.row = i; return ui_{className}_Items; }} }}",
    "\n  }",
    $"\n  public _UI_{className} UI_{m_name};");
                  }
                }
                else if (m_typeStr.StartsWith("gs")) { }
                else StackFields(dataWrappers, $"UI_{m_typeStr}", $"UI_{m_name}", "  ");
              }
              else dataWrappers.Add($"\n  public {m_name}[] UI_{m_name};");
            }
            if (_GS_fieldType == typeof(string))
              dataWrappers.Add($"\n  public string {m_name} {{ get => UI_{m_name}?.v ?? \"\"; set {{ if (UI_{m_name} != null && data != null) UI_{m_name}.v = data.{m_name} = value; }} }}");
            else if (_GS_fieldType == typeof(strings))
              dataWrappers.Add($"\n  public string {m_name} {{ get => UI_{m_name}?.v ?? \"\"; set {{ if (UI_{m_name} != null && data != null) UI_{m_name}.v = data.{m_name} = value; }} }}");
          }
          else if (gs_member.IsProp()) { }
          else if (gs_member.IsMethod())
          {
            if (attGS.isSync)
            {
              dataWrappers.Add(
          $"\n  public UI_method UI_{m_name};",
          $"\n  [HideInInspector] public bool in_{m_name} = false; public IEnumerator {m_name}() {{ if (in_{m_name}) {{ in_{m_name} = false; yield break; }} in_{m_name} = true; yield return StartCoroutine({m_name}_Sync()); in_{m_name} = false; }}");

              if (attGS.OnClicked.IsEmpty()) dataWrappers.Add($"\n  public virtual IEnumerator {m_name}_Sync() {{ yield return null; }}");
              else if (attGS.OnClicked.Contains("\n"))
                dataWrappers.Add(
                  $"\n  public virtual IEnumerator {m_name}_Sync()",
                   "\n  {",
                  $"\n    {attGS.OnClicked.Trim()}",
                   "\n    yield return null;",
                   "\n  }");
              else dataWrappers.Add($"\n  public virtual IEnumerator {m_name}_Sync() {{ {attGS.OnClicked.Trim()} yield return null; }}");
            }
            else
            {
              dataWrappers.Add(
         $"\n  public UI_method UI_{m_name};");
              if (attGS.OnClicked.IsEmpty()) dataWrappers.Add($"\n  public virtual void {m_name}() {{ }}");
              else if (attGS.OnClicked.Contains("\n"))
                dataWrappers.Add(
                  $"\n  public virtual void {m_name}()",
                   "\n  {",
                  $"\n    {attGS.OnClicked.Trim()}",
                   "\n  }");
              else dataWrappers.Add($"\n  public virtual void {m_name}() {{ {attGS.OnClicked.Trim()} }}");
            }
          }
        }
      if (lateUpdate_ValuesChanged.Length > 0)
        lateUpdate_ValuesChanged.Set("\n    if (ValuesChanged) { gChanged = true; OnValueChanged(); ValuesChanged", lateUpdate_ValuesChanged, " = false; }");

      foreach (var lib_fld in lib_flds)
        if (lib_fld.isInternal_Lib())
        {
          lateUpdate_ValuesChanged.Add($"\n    {lib_fld.Name}_LateUpdate1_GS();");
          update.Add($"\n    {lib_fld.Name}_Update1_GS();");
        }

      tData.Add("\n  }");
      data_to_ui.Add("\n    if (!data.siUnits) { for (int i = 0; i < 3; i++) siUnits = !siUnits; OnUnitsChanged(); }");
      OnGrid.Add(clickedMethods);

      var (s_start0_GS, s_start1_GS, s_onValueChanged, s_LateUpdate0, s_LateUpdate1, s_Update0, s_Update1, s_OnApplicationQuit) = StrBldr();

      foreach (var lib_fld in lib_flds)
        if (lib_fld.isInternal_Lib())
        {
          s_LateUpdate0.Add($"\n    {lib_fld.Name}_LateUpdate0_GS();");
          s_LateUpdate1.Add($"\n    {lib_fld.Name}_LateUpdate1_GS();");
          s_Update0.Add($"\n    {lib_fld.Name}_Update0_GS();");
          s_Update1.Add($"\n    {lib_fld.Name}_Update1_GS();");
          s_start0_GS.Add($"\n    {lib_fld.Name}_Start0_GS();");
          s_start1_GS.Add($"\n    {lib_fld.Name}_Start1_GS();");
          s_onValueChanged.Add($"\n    {lib_fld.Name}_OnValueChanged_GS();");
          s_OnApplicationQuit.Add($"\n    {lib_fld.Name}_OnApplicationQuit_GS();");
        }

      //if (uiDocument && gsName != "gsReport") s_onValueChanged.Add("\n    GetComponent<gsReport>()?.OnValueChanged_GS();");
      //if (uiDocument && gsName != "gsReport") s_onValueChanged.Add("\n        ((GS)GetComponent(\"gsReport\".ToType()))?.OnValueChanged_GS();");
      if (uiDocument && gsName != "gsReport") s_onValueChanged.Add("\n    var type = \"gsReport\".ToType();\n    if (type != null) ((GS)GetComponent(type))?.OnValueChanged_GS();\r\n");

      virtual_method(s_start0_GS, "Start0_GS()", s_start1_GS, "Start1_GS()", s_LateUpdate0, "LateUpdate0_GS()", s_LateUpdate1, "LateUpdate1_GS()",
        s_Update0, "Update0_GS()", s_Update1, "Update1_GS()", s_onValueChanged, "OnValueChanged_GS()", s_OnApplicationQuit, "OnApplicationQuit_GS()");

      StrBldr _cs = StrBldr().Add(_cs_includes,
     $"\npublic class gs{Name}_ : GS",
      "\n{",
      "\n  [HideInInspector] public uiData data;", G_Str, kernelWrappers, Enums, AssignEnums, AssignConsts,
      "\n  [HideInInspector] public bool ValuesChanged, gChanged;",
     $"\n  public static gs{Name} This;",
     $"\n  public virtual void Awake() {{ This = this as gs{Name}; Awake_GS(); }}",
      "\n  public virtual void Awake_GS() { }",
      "\n  public virtual void Start() { Build_UI(); Load_UI(); Start0_GS(); InitBuffers(); Start1_GS(); }", s_start0_GS, s_start1_GS,
      "\n  [HideInInspector] public bool already_quited = false;",
      "\n  public override void OnApplicationQuit() { Save_UI(); OnApplicationQuit_GS(); base.OnApplicationQuit(); already_quited = true; }", s_OnApplicationQuit, gridWrapper,
      "\n  public override void ui_to_data()",
      "\n  {",
      "\n    if (data == null) return;", ui_to_data,
      "\n  }",
      "\n  public override void data_to_ui()",
      "\n  {", data_to_ui_Defaults,
      "\n    if (data == null) return;", data_to_ui,
      "\n  }", OnGrid,
      "\n  public virtual void Save(string path, string projectName)",
      "\n  {",
      "\n    projectPaths = path;",
      "\n    $\"{projectPath}{projectName}.txt\".WriteAllText(JsonConvert.SerializeObject(data, Formatting.Indented));",
      "\n    foreach (var lib in GetComponents<GS>()) if (lib != this) lib.Save_UI();",
      "\n    $\"{projectPath}{name}_Data.txt\".WriteAllText(JsonConvert.SerializeObject(data, Formatting.Indented));",
      "\n  }",
      "\n  public override bool Save_UI_As(string path, string projectName)",
      "\n  {",
      "\n    if (already_quited) return false;",
      "\n    if (data != null) ui_to_data();",
      "\n    if (lib_parent_gs == this) Save(path, projectName);",
      "\n    else $\"{path}{projectName}.txt\".WriteAllText(JsonConvert.SerializeObject(data, Formatting.Indented));",
      "\n    return true;",
      "\n  }",
      "\n  public override bool Load_UI_As(string path, string projectName)",
      "\n  {",
      "\n    if (path == appPath && SelectedProjectFile.Exists())",
      "\n      path = $\"{appPath}{SelectedProjectFile.ReadAllText()}/\".Replace(\"//\", \"/\");",
      "\n    if(loadedProjectPath == path) return false;",
      "\n    projectPaths = loadedProjectPath = path;",
      "\n    string file = $\"{projectPath}{projectName}.txt\";",
      "\n    data = file.Exists() ? JsonConvert.DeserializeObject<uiData>(ui_txt_str = file.ReadAllText()) : new uiData();",
      "\n    if(data == null) return false;",
      "\n    foreach (var fld in data.GetType().GetFields(bindings)) if (fld != null && fld.FieldType == typeof(TreeGroup) && fld.GetValue(data) == null) fld.SetValue(data, new TreeGroup() { isChecked = true });",
      "\n    data_to_ui();", Load_UI,
      "\n    if (lib_parent_gs == this)",
      "\n    {",
      "\n      foreach (var lib in GetComponents<GS>())",
      "\n        if (lib != this && lib.GetType() != Type.GetType(\"gsProject\"))",
      "\n        {",
      "\n          lib.Build_UI();",
      "\n          lib.Load_UI();",
      "\n        }",
      "\n    }",
      "\n    ui_loaded = true;",
      "\n    return true;",
      "\n  }",
      "\n  public virtual void OnApplicationPause(bool pause) { if (ui_loaded) Save_UI(); }",
      "\n  [HideInInspector] public uint lateUpdateI = 0;",
      "\n  public virtual void LateUpdate()",
      "\n  {",
      "\n    if (!ui_loaded) return;",
      "\n    LateUpdate0_GS();", lateUpdate, lateUpdate_keys, lateUpdate_ValuesChanged,
      "\n    LateUpdate1_GS();",
      //"\n    lateUpdateI += Is(lateUpdateI < 100);",
      //"\n    lateUpdateI++;",
      "\n    if(lateUpdateI++ == uint_max) lateUpdateI = 100;",
      "\n  }", s_LateUpdate0, s_LateUpdate1,
      "\n  public override void Update()",
      "\n  {",
      "\n    base.Update();",
      "\n    if (!ui_loaded) return;",
      "\n    Update0_GS();", update,
      "\n    Update1_GS();",
      "\n  }", s_Update0, s_Update1,
      "\n  public override void OnValueChanged()",
      "\n  {",
      "\n    if (!ui_loaded) return;", onValueChanged,
      "\n    OnValueChanged_GS();",
      "\n  }", s_onValueChanged, Scenes_in_Build,
      "", showIfs, dataWrappers, tData, initBuffers, base_groupshared, declare_classes, classArrays,
      compute_or_material_shader, kernels_,
      vertCode, virtual_verts, renderObject, fragCode,
      "\n}");

      // if lib overrides an OnGrid method, add "base_" prefix to OnGrid method. Same for dataWrappers
      var matchStr = @"\s*((?:public|protected|private)?)\s*((?:virtual|override)?)\s*(\w+) (\w+)\((.*?)\)(?s)(.*?)(?=public|protected|private|#region|#endregion|\r\n}|\n})";
      MatchCollection _cs_matches = _cs.ToString().RegexMatch(matchStr), lib_matches = _cs_lib_regions.ToString().RegexMatch(matchStr);
      var _cs_methods = new List_method_data();
      foreach (Match _cs_match in _cs_matches) { var _cs_m = new method_data("", _cs_match); if (_cs_m.name.DoesNotStartWithAny("Cpu_", "Gpu_")) _cs_methods.Add(_cs_m); }
      var lib_methods = new List_method_data();
      foreach (Match _cs_match in lib_matches) { var lib_m = new method_data("", _cs_match); if (lib_m.name.DoesNotStartWithAny("Cpu_", "Gpu_")) lib_methods.Add(lib_m); }
      foreach (var lib_method in lib_methods)
      {
        var all_methods = _cs_methods.FindAll(a => a.name == lib_method.name);
        if (all_methods.Count == 1)
          _cs.RegexReplace($@"\b{lib_method.return_type} {lib_method.name}\(", $"{lib_method.return_type} base_{lib_method.name}(");
      }
      _cs.Set(_cs.ToString().BeforeLast("\n}"), _cs_lib_regions, "\n}");
      string _cs_str = _cs;
      _cs_str = _cs_str.ReplaceAll("public virtual bool Equals(object obj)", "public override bool Equals(object obj)",
        "public virtual int GetHashCode()", "public override int GetHashCode()",
        "public virtual void OnValueChanged_GS()", "public override void OnValueChanged_GS()");
      //if (_cs_str.Contains(" record "))
      //  _cs_str += "\nnamespace System.Runtime.CompilerServices { [ComponentModel.EditorBrowsable(ComponentModel.EditorBrowsableState.Never)] internal class IsExternalInit { } }";
      bool wrote_cs_File = _cs_filename.WriteAllText_IfChanged(_cs_str);

      Load_gs_prefabs();
      return wrote_cs_File;
    }

    public string Scenes_in_Build
    {
      get
      {
        var scenes = EditorBuildSettings.scenes;
        var s = StrBldr("\n  public string[] Scenes_in_Build => new string[] { ");
        bool firstTime = true;
        for (int i = 0; i < scenes.Length; i++)
        {
          var scene = scenes[i];
          if (scene.enabled)
          {
            s.Add($"{(firstTime ? "" : ", ")}\"{scene.path}\"");
            firstTime = false;
          }
        }
        s.Add(" };");
        return s;
      }
    }

    void virtual_method(params object[] args)
    {
      for (int i = 0; i < args.Length; i += 2)
      {
        StrBldr s = args[i] as StrBldr;
        string method = args[i + 1] as string;
        s.Set($"\n  public virtual void {method}", s.IsEmpty() ? " { }" : $"\n  {{{s}\n  }}");
      }
    }

    public void Load_gs_prefabs()
    {
      foreach (string typeStr in _GS_fields.Where(a => a.FieldType.IsType(typeof(GS))).Select(a => a.FieldType.GetTypeName()))
      {
        string prefabName = typeStr.After("gs"), prefab_File = $"{AssemblyPath(typeStr).AfterIncluding("Assets/")}{prefabName}.prefab";
        if ($"{dataPath}{prefab_File}".Exists())
        {
          GameObject prefabObj = GameObject.Find(prefabName);
          if (prefabObj?.IsNotActive() ?? true)
          {
            prefabObj = Instantiate(prefab_File.LoadAssetAtPath<GameObject>());
            prefabObj.name = prefabName;
          }
        }
      }
    }

    public class buffer_read_write { public buffer_data buffer; public bool read, write; }
    List_method_data compute_include_methods, kernel_methods, vert_frag_include_methods, vert_frag_methods;

    public class List_method_data : List<method_data>
    {
      public List_method_data() { }
      private List_method_data New() => new List_method_data();
      public void Deconstruct(out List_method_data s1) { s1 = New(); }
      public void Deconstruct(out List_method_data s1, out List_method_data s2) { s1 = New(); s2 = New(); }
      public void Deconstruct(out List_method_data s1, out List_method_data s2, out List_method_data s3) { s1 = New(); s2 = New(); s3 = New(); }
      public void Deconstruct(out List_method_data s1, out List_method_data s2, out List_method_data s3, out List_method_data s4) { s1 = New(); s2 = New(); s3 = New(); s4 = New(); }
      public void Deconstruct(out List_method_data s1, out List_method_data s2, out List_method_data s3, out List_method_data s4, out List_method_data s5) { Deconstruct(out s1, out s2, out s3, out s4); s5 = New(); }
      public void Deconstruct(out List_method_data s1, out List_method_data s2, out List_method_data s3, out List_method_data s4, out List_method_data s5, out List_method_data s6) { Deconstruct(out s1, out s2, out s3, out s4); s5 = New(); s6 = New(); }
    }

    public void GetReadWriteBuffers()
    {
      var sizeMatches = _GS_Code.RegexMatch($@"(\w+)\[\] (\w+)(.*)Size\(((.*?)\);)");
      buffers = new buffer_data[buff_members.Length];
      for (int i = 0; i < buffers.Length; i++)
      {
        var b = buffers[i] = new buffer_data() { name = buff_members[i].Name, member = buff_members[i] };
        foreach (Match m in sizeMatches) if (m.Group(2) == b.name) { b.sizeInfo = GetSizeInfo(m.Group(5)); break; }
      }
      cs_Code = cs_FileTextCode;
      _cs_Code = _cs_FileTextCode;
      var matchStr = @"\s*((?:public|protected|private)?)\s*((?:virtual|override)?)\s*(\w+) (\w+)\((.*?)\)(?s)(.*?)(?=public|protected|private|#region|#endregion|\r\n}|\n})";
      MatchCollection _cs_method_matches = _cs_Code.RegexMatch(matchStr), cs_method_matches = cs_Code.RegexMatch(matchStr);
      var methods = new List_method_data();
      (compute_include_methods, kernel_methods, vert_frag_include_methods, vert_frag_methods) = new List_method_data();
      foreach (Match match in _cs_method_matches)
      {
        var m = new method_data(Name, match);
        if (m.name.DoesNotStartWithAny("Cpu_", "Gpu_"))
          methods.Add(m);
      }
      var GS_kernel_matches = _GS_Code.RegexMatch(@"\bvoid (.*?)\(\) \{ Size\(");
      for (int i = methods.Count - 1; i >= 0; i--)//remove kernel methods in _cs that are not in _GS
      {
        var m = methods[i];
        if (m.isKernel && GS_kernel_matches.FirstOrDefault(a => a.Group(1) == m.methodName) == null)
          methods.RemoveAt(i);
      }
      foreach (Match match in GS_kernel_matches)//add kernel_methods from _GS that are not in _cs_method_matches
      {
        string methodName = match.Group(1);
        if (methods.FirstOrDefault(a => a.methodName == methodName) == null)
          methods.Add(new method_data(Name, methodName));
      }
      foreach (Match match in cs_method_matches)// replace virtual _cs methods with override code from cs, add methods in cs not in _cs
      {
        method_data override_code = new method_data(Name, match);
        var virtual_code = methods.FirstOrDefault(a => a.return_type == override_code.return_type && a.name == override_code.name && a.args == override_code.args);
        if (virtual_code != null)
        {
          if (override_code.code.Contains($"base.{override_code.name}("))
          {
            virtual_code.name = "base_" + virtual_code.name;
            override_code.code = override_code.code.RegexReplace(@$"\bbase.{override_code.name}\(", @$"base_{override_code.name}(");
            virtual_code.inheritance = "";
          }
          else
            methods.Remove(virtual_code);
        }
        override_code.inheritance = "";
        methods.Add(override_code);
      }
      for (int i = methods.Count - 1; i >= 0; i--)
      {
        var method = methods[i];
        if (method.isKernel)
        {
          if (_GS_methods.FirstOrDefault(a => a.Name == method.methodName) != null)
          {
            compute_include_methods.Add(method);
            kernel_methods.Add(method);
          }
          methods.RemoveAt(i);
        }
        else if (method.name.IsAny("vert_GS", "frag"))
        {
          vert_frag_include_methods.Add(method); vert_frag_methods.Add(method);
          methods.RemoveAt(i);
        }
      }
      Get_include_methods(compute_include_methods, methods);
      foreach (var include_method in compute_include_methods)
      {
        foreach (var b in buffers)
        {
          matchStr = @$"((?:Interlocked.*\(| \= ((?:\+\+|\-\-)))?)\b({b.name})\b(?:\[(?s)(.*?)\]|\b)((?:\+\+|\-\-| (?:\+|\-|\*|\/|\^|\%|\<\<|\>\>)?\= ))?";
          var matches = include_method.code.RegexMatch(matchStr);
          foreach (Match match in matches)
            if (match.Groups.Count >= 6)
            {
              if (match.Group(5) == " = ") include_method.AddWriteBuffer(b);
              else if (match.Group(5).Contains("=") || match.Group(4).ContainsAny("++", "--") || match.Group(1).Contains("Interlocked")) include_method.AddReadWriteBuffer(b);
              else include_method.AddReadBuffer(b);
            }
        }
        include_method.gcode = AddG(include_method.code);
        matchStr = @$"((?:\+\+|\-\-))?\bg\.(\w+)((?:\+\+|\-\-| (?:\+|\-|\*|\/|\^|\%|\<\<|\>\>)?\= ))?";
        var this_matches = include_method.gcode.RegexMatch(matchStr);
        foreach (Match m in this_matches)
          if (m.Group(1).IsNotEmpty() || m.Group(3).IsNotEmpty())
          {
            include_method.thisWrite = true;
            break;
          }
      }
      foreach (var m in kernel_methods)
      {
        string nm = m.name.Between("g" + Name + "_", "_GS"), items = _GS_Code.After($"void {nm}()").Between("{", "}"), size = items.Between("Size(", ");");
        m.SetSizeInfo(GetSizeInfo(size));
        m.sync = items.Contains("Sync();");
      }
      foreach (var method in kernel_methods)
        method.SetReadWriteBuffers();
      Topological_Sort(compute_include_methods);
      Get_include_methods(vert_frag_include_methods, methods);
      Topological_Sort(vert_frag_include_methods);
    }

    private void Topological_Sort(List_method_data include_methods)
    {
      var ms = new List_method_data();
      foreach (var m in include_methods) ms.Add(m);
      include_methods.Clear();
      while (ms.Count > 0)
        for (int i = 0; i < ms.Count; i++)
        {
          var m = ms[i];
          if (m.method_calls.Count == 0) { include_methods.Add(m); ms.Remove(m); foreach (var t in ms) t.method_calls.Remove(m); i = 0; }
        }
    }

    private void MatchCode(string code, method_data include_method, List_method_data include_methods, List_method_data methods)
    {
      var matches = code.RegexMatch(@$"\b(\w+)\b(\(.*(?=\;|\n|\r|.*))");
      foreach (Match match in matches)
      {
        string name = match.Group(1), match2 = match.Group(2);
        var methodsWithName = methods.Where(a => a.name == name);
        if (methodsWithName.Count() == 1)
        {
          var method = methodsWithName.First();
          if (!include_method.method_calls.Contains(method) && method != include_method) include_method.method_calls.Add(method);
          if (!include_methods.Contains(method)) include_methods.Add(method);
        }
        else if (methodsWithName.Count() > 1)
        {
          string bracketStr = match2?.BracketStr("()");
          int matchArgN = bracketStr?.ArgN() ?? 0;
          foreach (var method in methodsWithName.Where(a => a.argN == matchArgN && !include_method.method_calls.Contains(a)
            && a != include_method && !a.method_calls.Contains(include_method)))
          {
            include_method.method_calls.Add(method);
            if (!include_methods.Contains(method))
              include_methods.Add(method);
          }
        }
        if (match2.Contains("("))
        {
          string code2 = match2.After("(");
          if (code2.Contains("(")) MatchCode(code2, include_method, include_methods, methods);
        }
      }
    }

    private void Get_include_methods(List_method_data include_methods, List_method_data methods)//adds additional include_methods in order
    {
      for (int i = 0; i < include_methods.Count; i++)
      {
        var m = include_methods[i];
        MatchCode(m.code, m, include_methods, methods);
      }
      StrBldr s = StrBldr();
      foreach (var include_method in include_methods)
      {
        bool keep = false;
        StrBldr s2 = StrBldr("\n", include_method.name, "(", include_method.args, ")");
        foreach (var m in include_method.method_calls)
        {
          s2.Add("\n\t", m.name, "(", m.args, ")");
          if (include_method == m) { keep = true; s2.Add("\t**************"); }
          foreach (var m2 in m.method_calls)
          {
            s2.Add("\n\t\t", m2.name, "(", m2.args, ")");
            if (include_method == m2) { keep = true; s2.Add("\t**************"); }
            foreach (var m3 in m2.method_calls)
            {
              s2.Add("\n\t\t\t", m3.name, "(", m3.args, ")");
              if (include_method == m3) { keep = true; s2.Add("\t**************"); }
            }
          }
        }
        if (keep) s.Add(s2);
      }
      if (s.IsNotEmpty())
        print("Get_include_methods" + s);
    }

    public void Build()
    {
      This.StartCoroutine(Build_Coroutine());
    }
    public IEnumerator Build_Coroutine()
    {
      StrBldr compileTimeStr = StrBldr();
      ClockSec();
      compileTimeStr.Add("Build Times");
      var class_name = This.gsClass_name.value;
      string sceneName = class_name;
      if (sceneName.StartsWith("gs")) sceneName = sceneName.After("gs");
      if (This.SceneName != sceneName && !This.OpenScene(sceneName))
      {
        This.SceneCopy(sceneName);
        AssetDatabase.Refresh();
        yield return null;
      }
      Create_GS_Code();
      yield return This.StartCoroutine(attempt(() => gameObject = FindOrCreate_GameObject(gsName)));
      Component GS_script = FindOrCreate_Script(gameObject, gsName);
      if (GS_script == null)
      {
        AssetDatabase.Refresh();
        yield return null;
        GS_script = FindOrCreate_Script(gameObject, gsName);

        if (GS_script == null)
        {
          print($"Abort Build, GS_script == null, gsName = {gsName}");
          yield break;
        }
      }
      if ((gs = gameObject.GetComponent<GS>()) == null) //Write a simple .cs file
      {
        if (_cs_filename.DoesNotExist()) _cs_FileTextCode = StrBldr().AddLines("using GpuScript;", $"public class {gsName}_ : GS", "{", "}");
        if (cs_filename.DoesNotExist()) cs_FileTextCode = StrBldr().AddLines("using GpuScript;", $"public class {gsName} : {gsName}_", "{", "}");
        yield return null;
      }
      if ((gs = gameObject.GetComponent<GS>()) == null) { print("Abort Build, gs == null"); yield break; }
      _gs = gameObject.GetComponent<_GS>();
      if (_gs == null) { print("Abort Build, _gs == null"); yield break; }
      _cs_Code = _cs_FileTextCode;
      cs_Code = cs_FileTextCode;
      _GS_Code = _GS_FileTextCode;
      _GS_Type = GetType(_gs.GetType());
      _GS_fields = _GS_Type.GetFields(_GS_bindings);
      _GS_properties = _GS_Type.GetProperties(_GS_bindings);
      _GS_methods = _GS_Type.GetMethods(_GS_bindings);
      _GS_members = _GS_Type.GetMembers(_GS_bindings);
      _GS_texture_flds = _GS_fields.Where(a => a.FieldType == typeof(Texture2D)).Select(a => a).ToList();
      buff_members = _GS_Type.GetMembers(_GS_bindings).Where(a => a.IsFldOrProp() && a.IsArray() && !a.IsClass() && a.GetMemberType() != typeof(Texture2D)).Select(a => a).ToArray();
      _cs_includes = StrBldr().AddLines(
        "using System;",
        "using System.Collections;",
        "using System.Collections.Generic;",
        "using System.Linq;",
        "using System.Reflection;",
        "using UnityEngine;",
        "using UnityEngine.UIElements;",
        "using Newtonsoft.Json;",
        "using Newtonsoft.Json.Converters;",
        "using GpuScript;");
      build_UI = Name == SceneManager.GetActiveScene().name;
      if (_GS_Build_Libs()) { }

      bool added_scripts = false;
      foreach (var lib_fld in lib_flds)
      {
        if (lib_fld.isExternal_Lib())
        {
          string scriptName = "gs" + lib_fld.Name;
          GS gs_script = gameObject.GetComponent(scriptName) as GS;
          if (gs_script == null)
          {
            try { gs_script = gameObject.AddComponent(Type.GetType($"{scriptName}, GS_Libs_Assembly")) as GS; } catch (Exception e) { print($"Error {e}"); }
            if (gs_script != null)
            {
              string f = $"{AssemblyPath(scriptName)}{scriptName}";
              gs_script.material = $"{f}.mat".LoadAssetAtPath<Material>();
              gs_script.computeShader = $"{f}.compute".LoadAssetAtPath<ComputeShader>();
            }
            added_scripts = true;
          }
          else if (!gs_script.enabled) gs_script.enabled = true;
        }
      }
      if (added_scripts) { SaveActiveScene(); AutoRefresh = true; }

      compileTimeStr.Add($", _GS_Build_Libs: {ClockSec_Segment():0.##} sec");

      if (gs != null) //assign library fields to library prefab gameobjects
        foreach (var f in gs.GetType().GetFields(bindings).Where(a => a.FieldType.IsType(typeof(GS)) && a.GetValue(gs) == null))
          f.SetValue(gs, FindGameObject(f.Name)?.GetComponent<GS>() ?? null);

      yield return This.StartCoroutine(Build_UXML_Coroutine());
      GetReadWriteBuffers();
      compileTimeStr.Add($", GetReadWriteBuffers: {ClockSec_Segment():0.##} sec");
      if (!_cs_Write()) yield return null;
      compileTimeStr.Add($", _cs_Write: {ClockSec_Segment():0.##} sec");
      compute_Write();
      compileTimeStr.Add($", compute: {ClockSec_Segment():0.##} sec");
      shader_Write();
      compileTimeStr.Add($", shader: {ClockSec_Segment():0.##} sec");
      if (GS_Window.This.gsClass_Run.value) EditorApplication.EnterPlaymode(); else Beep(0.5f, 1000);
      compileTimeStr.Add($"\nBuilt {Name}: {ClockSec_SoFar():0.##} sec");
      print(compileTimeStr);
      AssetDatabase.Refresh();
    }
    public WaitForSeconds WaitForSeconds(float seconds) => new WaitForSeconds(seconds);

    string AddG(string s) { s = s.RegexReplace($@"\bg\.", ""); foreach (var f in gpu_flds) s = s.RegexReplace($@"\b{f.Name}\b", $"g.{f.Name}"); return s; }
    StrBldr compute_cginc, cginc;
    StrBldr compute_gStruct, compute_RWStructuredBuffers;

    public void compute_Write()
    {
      if (!generateComputeShader) { if (compute_filename.Exists()) compute_filename.DeleteFile(); return; }
      (compute_cginc, cginc, compute_RWStructuredBuffers) = StrBldr();

      cginc.Add(Enums_cginc, consts_cginc, $"\n  #define g g{Name}[0]");
      compute_cginc.Add(cginc, $"{(SM6 ? "\n  #pragma use_dxc" : "")}");
      StrBldr s = StrBldr();
      compute_gStruct = gStruct.Replace("\n  public ", "\n  ", "\n    public ", "\n    ");
      declare_structs = declare_structs.Replace("public ", "");
      declare_classes = declare_structs.Replace("[Serializable]", "", "public ", "");
      compute_RWStructuredBuffers.Add($"\n  RWStructuredBuffer<G{Name}> g{Name};", RWStructuredBuffers.Replace("\n  public ", "\n  "));
      s.Add(
        "\n  #include \"UnityCG.cginc\"",
        "\n  #include \"Lighting.cginc\"",
       "\n  #include \"../../GS/GS_Compute.cginc\"",
        compute_cginc, compute_gStruct, declare_structs, compute_groupshared, compute_RWStructuredBuffers);
      foreach (var m in compute_include_methods)
      {
        m.args = m.args.RegexReplace(@$"\bref ", "inout ");
        m.code = m.code.RegexReplace(@$"\bref ", "", @$"\bout ", "", "\r\n", "\n");
        if (m.code.EndsWith("\n  ")) m.code = m.code.BeforeLast("\n");
        if (m.code.Trim().IsEmpty()) m.code = " { }";
        if (m.isKernel)
        {
          if (m.sync) { }
          else if (m.code.DoesNotContain("\n")) m.code = $" {{ if ({m.id_less}){m.code} }}";
          else { m.code = m.code.ReplaceAll("\n  ", "\n    "); m.code = $"\n  {{\n    if ({m.id_less}){m.code}\n  }}"; }
          m.code = AddG(m.code);
          if (m.sync) s.Add("\n  [numthreads(", m.numThreads, ")] void ", m.methodName, "(uint3 grp_tid : SV_GroupThreadID, uint3 grp_id : SV_GroupID, uint3 id : SV_DispatchThreadID, uint grpI : SV_GroupIndex)", m.code.RegexReplace(@"\byield return ", ""));
          else s.Add("\n  [numthreads(", m.numThreads, ")] void ", m.methodName, "(", m.args, " : SV_DispatchThreadID)", m.code);
        }
        else { m.code = AddG(m.code); s.Add("\n  ", m.return_type, " ", m.name, "(", m.args, ")", m.code); }
      }

      foreach (var k in kernel_methods) s.Add($"\n  #pragma kernel {k.methodName}");
      foreach (var e in enumTypes) s.RegexReplace($@"\b{e.Name}.", $"{e.Name}_");
      s = s.Replace("[HideInInspector]", "", "[AttGS_Lib]", "");
      if (compute_filename.WriteAllTextAscii_IfChanged(s))
      {
        print($"{compute_filename} changed");
      }
      else
        print($"{compute_filename} did not change");
      if (gs.computeShader == null) gs.computeShader = compute_filename.LoadAssetAtPath<ComputeShader>();
    }

    public void shader_Write()
    {
      shaderCode = StrBldr();
      if (!generateMaterialShader) { if (shader_filename.Exists()) shader_filename.DeleteFile(); return; }

      compute_or_material_shader = StrBldr("\n", Texture2Ds);
      var (vertDrawSegments, virtual_verts) = StrBldr();
      for (int i = 0; i < render_methods.Length; i++)
      {
        var m = render_methods[i];
        if (m != null)
        {
          if (i == 0) vertDrawSegments.Add("\n    uint3 LIN = onRenderObject_LIN(i); int index = -1, level = ((int)LIN.x); i = LIN.y;",
              $"\n    if (level == ++index) o = {m.methodName}(i, j, o);");
          else vertDrawSegments.Add($"\n    else if (level == ++index) o = {m.methodName}(i, j, o);");
          //virtual_verts.Add($"\n  public virtual v2f {m.methodName}(uint i, uint j, v2f o) {{ return o; }}");
          virtual_verts.Add($"\n  public virtual v2f {m.methodName}(uint i, uint j, v2f o) => o;");
        }
      }

      var vert_frag_sb = StrBldr();
      foreach (var m in vert_frag_include_methods)
      {
        m.args = m.args.RegexReplace(@$"\bref ", "inout ");
        m.code = m.code.RegexReplace(@$"\bref ", "", @$"\bout ", "", "\r\n", "\n");
        if (m.code.EndsWith("\n  ")) m.code = m.code.BeforeLast("\n");
        m.code = AddG(m.code);
        vert_frag_sb.Add("\n  ", m.return_type, " ", m.name, "(", m.args, ")", m.name == "frag" ? " : SV_Target" : "", m.code);
      }
      StrBldr vert_frag_Code = StrBldr(
    "\n  struct v2f { float4 pos : POSITION, color : COLOR1, ti : TEXCOORD0, tj : TEXCOORD1, tk : TEXCOORD2; float3 normal : NORMAL, p0 : TEXCOORD3, p1 : TEXCOORD4, wPos : TEXCOORD5; float2 uv : TEXCOORD6; };",
    vert_frag_sb,
    "\n  v2f vert(uint i : SV_InstanceID, uint j : SV_VertexID)",
    "\n  {",
    "\n    v2f o;",
    "\n    o.pos = o.color = o.tj = o.tk = f0000; o.normal = o.p0 = o.p1 = o.wPos = f000; o.uv = f00; o.ti = float4(i, j, 0, 0);",
    "\n    return vert_GS(i, j, o);",
    "\n  }");
      shaderCode.Add(cginc, Enums_cginc, consts_cginc, Shader_Enums, compute_gStruct, declare_structs, compute_RWStructuredBuffers, compute_or_material_shader, "\n  Texture2D _PaletteTex;");
      var s = StrBldr().Add(
    $"Shader \"gs/{gsName}\"",
    "\n{",
    "\n  SubShader",
    "\n  {",
    "\n    Blend SrcAlpha OneMinusSrcAlpha",
    "\n    Cull Off",
    "\n    Pass",
    "\n    {",
    "\n      CGPROGRAM",
    "\n        #pragma target 5.0",
    "\n        #pragma vertex vert",
    "\n        #pragma fragment frag",
    "\n        #include \"UnityCG.cginc\"",
    "\n        #include \"Lighting.cginc\"",
    "\n        #include \"../../GS/GS_Shader.cginc\"",
    shaderCode, vert_frag_Code,
    "\n      ENDCG",
    "\n    }",
    "\n  }",
    "\n    Fallback Off",
    "\n}");

      foreach (var e in enumTypes) s.RegexReplace($@"\b{e.Name}.", $"{e.Name}_");

      s = s.Replace("[HideInInspector]", "", "[AttGS_Lib]", "");

      if (shader_filename.WriteAllTextAscii_IfChanged(s)) { }
      gs.material ??= material_filename.LoadAssetAtPath<Material>();
      if (!gs.material)
      {
        Shader shader = Shader.Find($"gs/gs{Name}");
        if (shader)
        {
          gs.material = new Material(shader);
          material_filename.CreateAsset(gs.material);
          if (gs.material)
          {
            Component script = GameObject.Find(gsName).GetComponent(gsName);
            script.GetType().GetField("material")?.SetValue(script, gs.material);
          }
        }
      }

      var paletteTex = _cs_Type.GetField("_PaletteTex", bindings);
      if (paletteTex != null)
      {
        Component script = GameObject.Find(gsName).GetComponent(gsName);
        try
        {
          var paletteVal = paletteTex.GetValue(script);
          if (paletteVal == null || paletteVal.ToString() == "null")
          {
            Texture2D palette = "Assets/GS/Resources/Palettes/Rainbow.png".LoadAssetAtPath<Texture2D>();
            paletteTex.SetValue(script, palette);
          }
        }
        catch (Exception e)
        {
          //Texture2D palette = "Assets/GS/Resources/Palettes/Rainbow.png".LoadAssetAtPath<Texture2D>();
          //paletteTex.SetValue(script, palette);
        }
      }
    }

    public BindingFlags const_bindings = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
    public StrBldr consts_cginc, AssignConsts;

    public void Calc_consts()
    {
      Type constType0 = null;
      (consts_cginc, AssignConsts) = StrBldr();

      FieldInfo[] constFlds = _GS_Type.GetConstants();
      foreach (var f in constFlds)
      {
        Type constType = f.FieldType;
        string constName = f.Name, constTypeName = f.GetTypeName(), constValue = f.GetValue(null).ToString().ToLower();
        if (constTypeName == "float" && (constValue.Contains(".") || constValue.Contains("e")) && constValue.DoesNotContain("f")) constValue = $"{constValue}f";
        else if (constValue.Contains(".") && constValue.DoesNotContain("f")) constValue = $"{constValue}f";
        consts_cginc.Add($"\n  #define {constName} {constValue}");
        if (constType0 != constType)
        {
          if (AssignConsts.IsNotEmpty()) AssignConsts.Add(";");
          AssignConsts.Add($"\n  public const {constTypeName} {constName} = {constValue}");
        }
        else
          AssignConsts.Add($", {constName} = {constValue}");
        constType0 = constType;
      }
      if (AssignConsts.IsNotEmpty() && AssignConsts.ToString().DoesNotEndWith(";")) AssignConsts.Add(";");
    }

#if UNITY_STANDALONE_WIN
    [HideInInspector] public bool SM6 = true;
#else
    [HideInInspector] public bool SM6 = false;
#endif //UNITY_STANDALONE_WIN

    StrBldr shaderCode;

    public struct MatchInfo { public string attStr, typeStr, name, expression, Name, Description, DeclaringType; }
    public static MatchInfo GetMatchInfo(Match match)
    {
      var m = new MatchInfo() { attStr = match.V(1), typeStr = match.V(2), name = match.V(3), expression = match.V(4).Trim(), Name = "", Description = "", DeclaringType = null };
      if (m.attStr.StartsWith("\"")) { m.Name = m.attStr.Between("\"", "\""); if (m.Name.Contains("|")) { m.Description = m.Name.After("|"); m.Name = m.Name.Before("|"); } }
      if (m.attStr.Contains("UI.DeclaringType, typeof(")) m.DeclaringType = m.attStr.Between("UI.DeclaringType, typeof(", ")");
      return m;
    }

    public void Build_Precompiled_Lib(string cs_Code, string _cs_Code, string _GS_Code, string path)
    {
      string matchStr;
      var classList = new List<string>();

      if (_gs_members == null) _gs_members ??= (gsName + "_GS").GetOrderedMembers();
      foreach (var mem in _gs_members)
        if (mem.AttGS() != null && mem.IsArray() && mem.GetMemberType().GetElementType().IsClass)
          if (classList.DoesNotContain(mem.Name))
            classList.Add(mem.Name);

      string[] cs_includes = cs_Text.Before("public").Split("\n").Select(a => a.Trim()).Where(a => a.IsNotEmpty()).Distinct().ToArray();
      matchStr = @"\s*((?:public|protected|private)?)\s*((?:virtual|override))\s*(\w+) (\w+)\((.*?)\)(?s)(.*?)(?=public|protected|private|\[Serializable\]|#region|#endregion|\r\n}|\n})";
      MatchCollection cs_method_matches = cs_Code.RegexMatch(matchStr), _cs_method_matches = _cs_Code.RegexMatch(matchStr);
      if (_GS_Code.Contains("\r\n")) _GS_Code = _GS_Code.BeforeLast("\r\n"); else _GS_Code = _GS_Code.BeforeLast("\n");

      MatchCollection lib_matches = _GS_Code.RegexMatch(@$"  gs(.*) (.*);(((?s).*?)\#region \<(\w+)\>(?s).*?\#endregion \<(\w+)\>)?");

      _GS_Code = _GS_Code.RegexReplace(@$"  #region(.*)\n", "", @$"  #endregion(.*)\n", "", @$"  gs(.*)\n", "");
      _GS_Type = $"{gsName}_GS".ToType();
      var _GS_Members = _GS_Type?.GetMembers(_GS_bindings);
      foreach (var p in _GS_Members)
      {
        _GS_Code = _GS_Code.RegexReplace(@$"\b{p.Name}\b", $"{Name}_{p.Name}");
        cs_Code = cs_Code.RegexReplace(@$"\b{p.Name}\b", $"{Name}_{p.Name}");
        _cs_Code = _cs_Code.RegexReplace(@$"\b{p.Name}\b", $"{Name}_{p.Name}");
      }
      var _GS_constFlds = _GS_Type.GetConstants();
      foreach (var c in _GS_constFlds)
      {
        _GS_Code = _GS_Code.RegexReplace(@$"\b{c.Name}\b", $"{Name}_{c.Name}");
        cs_Code = cs_Code.RegexReplace(@$"\b{c.Name}\b", $"{Name}_{c.Name}");
        _cs_Code = _cs_Code.RegexReplace(@$"\b{c.Name}\b", $"{Name}_{c.Name}");
      }
      _GS_Code = _GS_Code.RegexReplace($@"\b{Name}_vert_", $"vert_{Name}_");

      $"{path}{gsName}_GS_lib.txt".WriteAllText(_GS_Code);

      var (_methods_to_remove, methods, _methods) = new List_method_data();
      foreach (Match match in cs_method_matches) methods.Add(new method_data(Name, match));
      foreach (Match match in _cs_method_matches) _methods.Add(new method_data(Name, match));

      var vMeths = _methods.Where(_m => _m.name.DoesNotStartWithAny("Cpu_", "Gpu_", "onRenderObject_", "vert_", "frag_", "base_")
        && _m.inheritance.IsNotEmpty() && _m.return_type == "void" && _m.inheritance.IsNotEmpty()
        && _m.name.IsNotAny("Awake", "Start", "LateUpdate", "Update", "OnValueChanged", "InitBuffers", " OnApplicationQuit",
          "ui_to_data", "data_to_ui", "Load_UI", "Save_UI", "onRenderObject")
        && _m.code.Between("{", "}").Trim().IsNotEmpty() && methods.FirstOrDefault(m => m.name == _m.name && m.argN == _m.argN) == null);
      var s = StrBldr($"Built Precompiled Lib: Assets/GS_Libs/{gsName}/{path.BeforeLast("/").AfterLast("/")}/");
      foreach (var m in vMeths)
      {
        if (m.name == "InitBuffers1_GS" || m.name == "CamViews_To_UI")
        {
          var args = string.Join(", ", m.args.Split(",").Select(a => a.Trim().After(" ")));
          m.inheritance = "override";
          m.code = $"{{ base_{Name}_{m.name}({args}); }}"; //remember to add methods with a return type
          s.Add($"\n  {m.access} {m.inheritance} {m.return_type} {m.name}({m.args}) {m.code}");
          methods.Add(m);
        }
      }
      print(s);

      foreach (var m in methods)// fix _base
      {
        string baseStr = $"base.{m.name}();";
        if (m.inheritance == "override" && m.code.Contains($"base.{m.name}"))
        {
          if (Generate_pre_and_post_methods(m, baseStr, "InitBuffers")) { }
          else if (Generate_pre_and_post_methods(m, baseStr, "LateUpdate")) { }
          else if (Generate_pre_and_post_methods(m, baseStr, "Update")) { }
          else
          {
            cs_Code = cs_Code.RegexReplace(@$"\bbase.{m.name}\(", @$"base_{Name}_{m.name}(");
          }
        }
      }

      StrBldr _s = StrBldr();
      foreach (var m in _methods)// add base_VGrid_InitBuffers0_GS(); from _cs_Code
        if (m.name.IsAny("InitBuffers0_GS", "InitBuffers1_GS", "LateUpdate0_GS", "LateUpdate1_GS", "Update0_GS", "Update1_GS",
          "Start0_GS", "Start1_GS", "OnValueChanged_GS", "onRenderObject_GS", "OnApplicationQuit", "Load_UI", "Save_UI"))
          _s.Add($"\n  public virtual {m.return_type} base_{Name}_{m.name}({m.args}){m.code.TrimEnd()}");
        else if (m.name == "frag_GS")
          _s.Add($"\n  public virtual {m.return_type} base_frag_{Name}_GS({m.args}){m.code.TrimEnd()}");
      cs_Code += _s;

      foreach (Match lib_m in lib_matches)
      {
        string libName = lib_m.Groups[1].Value, libTypeName = "gs" + libName, libPath = $"{AssemblyPath(libTypeName)}Lib/{libTypeName}";
        string libVarName = lib_m.Groups[2].Value;
        string _GS_lib_Code = $"{libPath}_GS_lib.txt".ReadAllText();
        string cs_lib_Code = $"{libPath}_cs_lib.txt".ReadAllText();
        string cs_lib_kernels_str = $"{libPath}_cs_lib_kernels.txt".ReadAllText();
        string[] cs_lib_includes = $"{libPath}_cs_lib_includes.txt".ReadAllLines();
        cs_includes = cs_includes.Union(cs_lib_includes).ToArray();

        MatchCollection lib_method_matches = cs_lib_Code.RegexMatch(matchStr);
        foreach (Match match in lib_method_matches)
        {
          var meth = new method_data(Name, match);
          string methName = libVarName + meth.name.After(libName);
          var find_meth = methods.Find(a => a.name == methName && a.args == meth.args);
          if (find_meth == null) find_meth = methods.Find(a => a.name == meth.name && a.args == meth.args);
          if (find_meth != null) //later, see if override calls base and add in code
          {
            var findStr = $@"public virtual {meth.return_type} {meth.name}\({meth.args}\)(?s)(.*?)(?=public|protected|private|#region|#endregion|\r\n}}|\n}})";
            cs_lib_Code = cs_lib_Code.RegexReplace(findStr, "");
          }
        }

        cs_lib_Code = cs_lib_Code.RegexReplace($@"\b{libName}_", $"{Name}_{libVarName}_",
  $@"\b{Name}_{libName}_Gpu_", $"Gpu_{Name}_{libVarName}_",
  $@"\b{Name}_{libName}_Cpu_", $"Cpu_{Name}_{libVarName}_",
  $@"\b{Name}_{libName}_vert_", $"vert_{Name}_{libVarName}_",
  $@"\b{Name}_{libName}_frag_", $"frag_{Name}_{libVarName}_",
  $@"\bbase_{libName}_", $"base_{Name}_{libVarName}_",
  $@"\bbase_frag_{libName}_", $"base_frag_{Name}_{libVarName}_",
  $@"\bfrag_{Name}_{libName}_", $"frag_{Name}_{libVarName}_",
  $@"\bfrag_{libName}_", $"frag_{Name}_{libVarName}_",
  $@"\bGpu_{libName}_", $"Gpu_{Name}_{libVarName}_",
  $@"\bCpu_{libName}_", $"Cpu_{Name}_{libVarName}_",
  $@"\boverride\b", "virtual");

        cs_Code = cs_Code.RegexReplace($@"\b{libVarName}_", $"{Name}_{libVarName}_");


        cs_Code += "\n" + cs_lib_Code;
      }

      Insert_GS_methods(ref cs_Code, "void InitBuffers0_GS()", "void InitBuffers1_GS()", "void LateUpdate0_GS()", "void LateUpdate1_GS()",
        "void Update0_GS()", "void Update1_GS()", "void Start0_GS()", "void Start1_GS()", "void OnValueChanged_GS()",
        "void OnApplicationQuit_GS()", "void onRenderObject_GS(ref bool render, ref bool cpu)",
        "float4 frag_GS(v2f i, float4 color) { return color; }");

      foreach (var c in classList) cs_Code = cs_Code.RegexReplace($@"\b{c}_", $"{Name}_{c}_");

      cs_Code = cs_Code.RegexReplace(
        $@"\bUI_", $"UI_{Name}_",
        $@"\bUI_{Name}_grid_", $"UI_grid_{Name}_",
        $@"\bGpu_", $"Gpu_{Name}_",
        $@"\bGpu_{Name}_{Name}_", $"Gpu_{Name}_",
        $@"\bCpu_", $"Cpu_{Name}_",
        $@"\bCpu_{Name}_{Name}_", $"Cpu_{Name}_",
        $@"kernel_", $"kernel_{Name}_",
        $@"kernel_{Name}_{Name}_", $"kernel_{Name}_",
        $@"vert_", $"vert_{Name}_",
        $@"vert_{Name}_{Name}_", $"vert_{Name}_",
        $@"frag_", $"frag_{Name}_",
        $@"frag_{Name}_{Name}_", $"frag_{Name}_",

        $@"\bUI_{Name}_TreeGroup", $"UI_TreeGroup",
        $@"\bUI_{Name}_VisualElement", $"UI_VisualElement",
        $@"\bUI_{Name}_grid", $"UI_grid",
        $@"\bUI_{Name}_enum", $"UI_enum",

        $@"\boverride\b", "virtual");
      cs_includes = cs_includes.OrderBy(a => a).ToArray();
      string includes = String.Join("\n", cs_includes).Trim();
      $"{path}{gsName}_cs_lib_includes.txt".WriteAllText(includes);

      Type cs_Type = gsName.ToType();
      var cs_Members = cs_Type?.GetMembers(_GS_bindings);
      foreach (var p in cs_Members)
      {
        string pName = p.Name;
        if (pName.StartsWith("get_")) pName = pName.After("get_");
        else if (pName.StartsWith("set_")) pName = pName.After("set_");
        cs_Code = cs_Code.RegexReplace(@$"\b{pName}\b", $"{Name}_{pName}");
      }

      var _constFlds = _cs_Type.GetConstants();
      foreach (var c in _constFlds)
        cs_Code = cs_Code.RegexReplace(@$"\b{c.Name}\b", $"{Name}_{c.Name}");

      cs_Code = cs_Code.RegexReplace($@"\b{Name}_kernel_", $@"kernel_{Name}_", $@"\b{Name}_vert_", $@"vert_{Name}_",
        $@"\b{Name}_frag_", $@"frag_{Name}_", $@"\b{Name}_frag", $@"frag_{Name}", $@"{Name}_{Name}_", $"{Name}_");

      cs_Code = cs_Code.ReplaceAll("public virtual string ToString()", "public override string ToString()");

      $"{path}{gsName}_cs_lib.txt".WriteAllText(cs_Code);

      StrBldr lib_kernels = StrBldr();
      MatchCollection kernel_matches = cs_Code.RegexMatch(@"\bpublic virtual void (.*?)_GS\(uint3 id\)");
      foreach (Match m in kernel_matches) lib_kernels.Add(lib_kernels.IsEmpty() ? "" : "\t", m.Group(1), "_GS");
      kernel_matches = cs_Code.RegexMatch(@"\bpublic virtual IEnumerator (.*?)_GS\(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI\)");
      foreach (Match m in kernel_matches) lib_kernels.Add(lib_kernels.IsEmpty() ? "" : "\t", m.Group(1), "_GS");
      $"{path}{gsName}_cs_lib_kernels.txt".WriteAllText(lib_kernels);

    }
    public void Get_region_code(ref string code, string region)
    {
      string r = $"  #region {region}", r2 = $"  #endregion {region}";
      StrBldr sb = new StrBldr();
      while (code.Contains(r)) { sb.Add(code.After(r).After("\n").Before(r2)); code = code.After(r2); }
      code = sb;
    }
    public void Build_Precompiled_Lib()
    {
      cs_Code = cs_FileTextCode.After("{").After("\n").BeforeLast("}");
      _cs_Code = _cs_FileTextCode.After("{").After("\n").BeforeLast("}");
      _GS_Code = _GS_FileTextCode.rRemoveExcludeRegions().After("{").After("\n").BeforeLast("}");
      Build_Precompiled_Lib(cs_Code, _cs_Code, _GS_Code, $"{classPath}Lib/");
    }

    private void Insert_GS_methods(ref string cs_Code, params string[] methods)
    {
      var s = StrBldr();
      foreach (var method in methods)
      {
        string declaration = method.Contains(" {") ? method.Before(" {") : method;
        if (cs_Code.DoesNotContain(declaration))
        {
          StrBldr arg_sb = StrBldr();
          string returnType = declaration.Before(" "), m = declaration.After(" ");
          string[] args = m.Between("(", ")").Split(",").Select(a => a.Trim()).ToArray();
          for (int i = 0; i < args.Length; i++)
          {
            if (args[i].StartsWithAny("ref ", "out ")) args[i] = args[i].BeforeIncluding(" ") + args[i].After(" ").After(" ");
            else args[i] = args[i].After(" ");
            arg_sb.Add((arg_sb.IsEmpty() ? "" : ", "), args[i]);
          }
          var code = StrBldr();
          if (lib_flds != null)
            foreach (var lib_fld in lib_flds) code.Add($"\n    {Name}_{lib_fld.Name}_{m.Before("(")}({arg_sb});");
          if (method.Contains(" {"))
            code.Add($"\n    {method.Between("{", "}").Trim()}");
          //s.Add($"\n  public virtual {returnType} {Name}_{m}{(code.IsEmpty() ? " { }" : $"\n  {{{code}\n  }}")}");
          s.Add($"\n  public {(method.StartsWith("override ") ? "override" : "virtual")} {returnType} {Name}_{m}{(code.IsEmpty() ? " { }" : $"\n  {{{code}\n  }}")}");
        }
      }
      cs_Code += s;
    }

    private bool Generate_pre_and_post_methods(method_data m, string baseStr, string m_name)
    {
      if (m.name == m_name) //generate pre and post methods
      {
        string pre = m.code.Before(baseStr).Trim();
        if (pre.IsNotEmpty()) //add pre code to new method
        {
          if (cs_Code.DoesNotContain($"void {m_name}0_GS()"))
            cs_Code += $"\n  public virtual void {m_name}0_GS()\n  {{\n    {pre}\n  }}";
          else //add pre code to existing method
            cs_Code = cs_Code.RegexReplace(@"void {gs_name0}_GS()(?s)(.*?)\{", $"void {Name}_{m_name}0_GS()\n  {{\n    {pre}\n");
        }
        string post = m.code.After(baseStr).Trim();
        if (post.IsNotEmpty()) //add post code to new method
        {
          if (cs_Code.DoesNotContain($"void {m_name}1_GS()"))
            cs_Code += $"\n  public virtual void {m_name}1_GS()\n  {{\n    {post}\n  }}";
          else //add post code to existing method
            cs_Code = cs_Code.RegexReplace(@"void {gs_name1}_GS()(?s)(.*?)\{", $"void {Name}_{m_name}1_GS()\n  {{\n    {post}\n");
        }
        return true;
      }
      return false;
    }
  }

  public void gsClass_Build_clicked(ClickEvent evt = null)
  {
    This = this;
    var project = new ProjectData(Name);
    project.Build();
  }

  public void CommentOutFile(string f)
  {
    if (f.Exists())
    {
      string s = f.ReadAllText(), s0 = s.BeforeIncluding("{"), s2 = s.AfterLastIncluding("}"), s1 = s.After("{").BeforeLast("}").BeforeLast("\n");
      s1 = s1.ReplaceAll("\n", "\n//");
      s = s0 + s1 + s2;
      (f + "_test").WriteAllText(s);
    }
  }
  public void UnCommentFile(string f)
  {
    f += "_test";
    if (f.Exists())
    {
      string s = f.ReadAllText(), s0 = s.BeforeIncluding("{"), s2 = s.AfterLastIncluding("}"), s1 = s.After("{").BeforeLast("}");
      s1 = s1.ReplaceAll("\n//", "\n");
      s = s0 + s1 + s2;
      f.WriteAllText(s);
    }
  }

  public void gsClass_Lib_clicked(ClickEvent evt = null)
  {
    ClearConsole();
    var project = new ProjectData(Name);
    project.Build_Precompiled_Lib();
    AssetDatabase.Refresh();
  }

  public void gsClass_Create_clicked(ClickEvent evt = null) { gsClass_Build_clicked(); }

  #region Modify Code

  public bool isGeneratingCode = false;

  public StrBldr StrBldr(params object[] items) => new StrBldr(items);

  protected static void SaveSceneAs(Scene scene, string path) { if (!EditorApplication.isPlaying) { EditorSceneManager.MarkSceneDirty(scene); EditorSceneManager.SaveScene(scene, path); } }
  protected static void SaveScene(Scene scene) { if (!EditorApplication.isPlaying) { EditorSceneManager.MarkSceneDirty(scene); EditorSceneManager.SaveScene(scene); } }
  protected static void SaveActiveScene() { SaveScene(SceneManager.GetActiveScene()); }

  protected static void SaveScene() { SaveScene(SceneManager.GetActiveScene()); }
  protected static void SaveSceneAs(string path) { SaveSceneAs(SceneManager.GetActiveScene(), path); }
  protected string UnityVersion => Application.unityVersion;

  public int rebuild = 0;
  void Update_cginc_Version(params string[] cginc_Files)
  {
    foreach (var c in cginc_Files)
    {
      string f = $"{AssetsPath}GS/{c}.cginc";
      string s = f.ReadAllTextAscii();
      string versionStr = s.Between("// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC", "\n");
      int version = versionStr.IsEmpty() ? 1 : versionStr.After("Update: ").To_int() + 1;
      f.WriteAllTextAscii($"// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC, Update: {version}\n" + s.After("\n"));
    }
  }
  void Update_cginc_Files() { Update_cginc_Version("GS", "GS_Compute", "GS_Shader"); }

  public void OnScriptsCompiled(string[] compiled_gsFiles)
  {
    foreach (var f in compiled_gsFiles) if (f.EndsWithAny($"{gsName}_GS.cs", $"{gsName}.cs")) rebuild = 2;
  }

  public static string AssemblyPath(string name)
  {
    string AssetsPath = $"{dataPath}Assets/";
    foreach (var p in GS_Assemblies) { string f = $"{AssetsPath}{p}/{name}/"; if (f.Exists()) return f; }
    return $"{AssetsPath}{name}/";
  }

  void Update()
  {
    coroutines.Update();
    if (importedAssets_filename.Exists())
    {
      var importedAssets = importedAssets_filename.ReadAllLines();
      importedAssets_filename.DeleteFile();
      if (importedAssets != null && importedAssets.Length > 0)
      {
        var gsFiles = new List<string>();
        string str = AssemblyPath(gsName).AfterIncluding("Assets/");
        foreach (var f in importedAssets) if (f.StartsWith(str)) gsFiles.Add(f.After(str));
        var compiled_gsFiles = gsFiles.ToArray();
        if (compiled_gsFiles.Length > 0) OnScriptsCompiled(compiled_gsFiles);
        foreach (var f in importedAssets) if (f.EndsWith("/GS.cs")) { Update_cginc_Files(); AssetDatabase.Refresh(); break; }
      }
      else print("importedAssets = null");
    }
    if (rebuild > 0) { rebuild--; if (rebuild == 0) gsClass_Build_clicked(); }
  }

  protected bool isAndroid => EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android;

  protected bool AutoCompile { get => EditorPrefs.GetInt("kAutoRefresh") == 1; set => EditorPrefs.SetInt("kAutoRefresh", value ? 1 : 0); }
  protected static bool AutoRefresh;
  protected static GameObject FindObjectWithComponent(string component)
  {
    foreach (var obj in UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects()) { var o = FindObjectWithComponent(obj, component); if (o) return o; }
    return null;
  }
  protected static GameObject FindObjectWithComponent(GameObject obj, string component)
  {
    Component c = obj.GetComponent(component);
    if (c) return obj;
    for (int i = 0; i < obj.transform.childCount; i++) { var o = obj.transform.GetChild(i).gameObject; c = o.GetComponent(component); if (c) return o; }
    for (int i = 0; i < obj.transform.childCount; i++) { var o = obj.transform.GetChild(i).gameObject; o = FindObjectWithComponent(o, component); if (o) return o; }
    return null;
  }

  protected static Type GetComponentTypeByName(string name)
  {
    foreach (var a in GS_Assemblies)
    {
      Type t = Type.GetType($"{name}, {a}_Assembly, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");
      if (t != null) return t;
    }
    return null;
  }

  public static GameObject FindOrCreate_GameObject(string gsName)
  {
    GameObject o = GameObject.Find(gsName);
    if (!o) { o = FindObjectWithComponent(gsName); if (o) o.name = gsName; }
    try { if (!o) o = new GameObject(gsName); Selection.activeGameObject = o; }
    catch (Exception) { AutoRefresh = true; return null; }
    return o;
  }
  static Component FindOrCreate_Script(GameObject o, string _GS)
  {
    if (o == null)
      return null;
    Component GS_script = o.GetComponent(_GS);
    if (!GS_script)
    {
      try { GS_script = o.AddComponent(GetComponentTypeByName(_GS)); } catch (Exception) { }
      AutoRefresh = true;
      return null;
    }
    return GS_script;
  }

  public string Name { get { string s = gsClass_name_val.ToVarName(); if (s.StartsWith("gs")) s = s.After("gs"); return s; } }
  public string gsName => "gs" + Name;
  public static string AssetsPath => dataPath + "Assets/";
  public string classPath => $"{AssetsPath}{gsName}/";

  #endregion Modify Code

  #region Package

  public string packageDescription => package_name.text;
  public string backupScene => sceneName;
  public string backupDescription => backup_description.text;
  public string backupOmitDirs => backup_omitFolders.text;
  public string[] getOmitPaths => backupOmitDirs.Split("|");

  void backup_Backup_clicked(ClickEvent evt)
  {
    SaveScene();
    var omitPaths = getOmitPaths;
    string folder = $"{backupScene.Trim()} {backupDescription.Trim()}".Trim();
    string backupPath = $"{dataPath}Backup/{folder}/";
    string data_backupPath = $"{backupPath}{backupScene}/", gs_backupPath = $"{backupPath}gs{backupScene.Trim()}/";
    string data_srcPath = $"{dataPath}{backupScene}/", gs_srcPath = $"{dataPath}Assets/gs{backupScene.Trim()}/";
    if (gs_srcPath.DoesNotExist()) { EditorUtility.DisplayDialog("Error", $"gs{backupScene.Trim()} does not exist.", "Abort Backup"); return; }
    if (backupPath.Exists() && !EditorUtility.DisplayDialog("Overwrite?", $"Are you sure you want to overwrite {folder}?", "Overwrite", "Abort Backup")) return;
    gs_srcPath.CopyDirAll(gs_backupPath);
    data_srcPath.CopyDirAll(data_backupPath, omitPaths);
    print($"Backed up {folder}");
  }

  public void backup_Restore_clicked(ClickEvent evt)
  {
    SaveScene();
    var omitPaths = getOmitPaths;
    string folder = $"{backupScene.Trim()} {backupDescription.Trim()}".Trim();
    string backupPath = $"{dataPath}Backup/{folder}/";
    string data_backupPath = $"{backupPath}{backupScene.Trim()}/", gs_backupPath = $"{backupPath}gs{backupScene.Trim()}/";
    string data_srcPath = $"{dataPath}{backupScene.Trim()}/", gs_srcPath = $"{dataPath}Assets/gs{backupScene.Trim()}/";
    if (backupPath.DoesNotExist()) { EditorUtility.DisplayDialog("Error", $"{backupPath} does not exist", "Abort Restore"); return; }
    if (!EditorUtility.DisplayDialog("Overwrite?", $"Are you sure you want to overwrite {backupScene.Trim()}?", "Overwrite and Restore", "Abort Restore")) return;
    gs_srcPath.DeleteFiles("*.*");
    gs_backupPath.CopyDirAll(gs_srcPath);
    if (data_backupPath.Exists()) { data_srcPath.DeleteFiles("*.*"); data_backupPath.CopyDirAll(data_srcPath, omitPaths); }
    print($"Restored {folder}");
  }

  public void package_Create_clicked(ClickEvent evt)
  {
    SaveScene();
    string UnityVersion = this.UnityVersion;
    string GpuScript_ = isAndroid ? "GpuScript_Android_" : "GpuScript_";
    var versionFiles = dataPath.GetFiles($"{GpuScript_}*.unitypackage");
    string version = versionFiles.Length == 0 ? "10" : versionFiles[versionFiles.Length - 1].Between($"/{GpuScript_}", ".");
    string build = versionFiles.Length == 0 ? "0058" : $"{versionFiles[versionFiles.Length - 1].After($"/{GpuScript_}").Between(".", "_").To_int() + 1:0000}";
    string pkgFile = $"{dataPath}{GpuScript_}{version}.{build}_{UnityVersion}_{packageDescription}.unitypackage";

    string assetsPath = $"{dataPath}Assets/";
    pkgFile.DeleteFile();
    List<string> exportPaths = new List<string>();
    foreach (var file in assetsPath.GetDirectories())
    {
      string f = $"/{file.Replace("\\", "/").After(assetsPath)}/";
      if (f.DoesNotContainAny("/gsQuandl/"))
        exportPaths.Add($"Assets{f.BeforeLast("/")}");
    }
    AssetDatabase.ExportPackage(exportPaths.ToArray(), pkgFile, ExportPackageOptions.Recurse); // | ExportPackageOptions.IncludeDependencies);
    Beep(0.1f, 1000);
    dataPath.Run();
  }

  #endregion Package

  void SwitchPlatform() //if in Android mode, automatically "~remove" all unnecessary folders when generating APK.
  {
    var android_folders = new string[] { "Editor", "GS" }.ToList();
    var android_dirs = info_Android_dirs.value.Split(',');
    foreach (var dir in android_dirs) if (dir.StartsWith("gs")) android_folders.Add(dir); else android_folders.Add("gs" + dir);
    var dirs = AssetsPath.GetDirectories();
    if (info_platform.index == 0 && isAndroid)
    {
      var s = StrBldr("Windows");
      foreach (var dir in dirs)
      {
        if (dir.EndsWith("~"))
        {
          string d = $"{dir.Replace("\\", "/")}/", d2 = $"{d.BeforeLast("~/")}/";
          if (android_folders.FirstOrDefault(a => d.Contains($"/{a}~/")) == null && d.Exists())
            s.Add(" ", d2.After("Assets/"));
        }
      }
      print(s);
      EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64);
    }
    else if (info_platform.index == 1 && !isAndroid)
    {
      var s = StrBldr("Android");
      foreach (var dir in dirs)
      {
        string d = $"{dir.Replace("\\", "/")}/";
        if (d.DoesNotEndWith("~/"))
        {
          if (android_folders.FirstOrDefault(a => d.Contains($"/{a}/")) == null && d.Exists())
          {
            string d2 = $"{d.BeforeLast("/")}~/";
            if (d2.DoesNotExist())
              s.Add(" ", d2.After("Assets/"));
          }
        }
      }
      print(s);
      EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
    }
    bool isPhone = info_platform.index == 1;

    exe_Apk.style.display = DisplayIf(isPhone);
    exe_Apk_CMake.style.display = DisplayIf(isPhone && Show_exe_Apk_CMake);
    exe_Parent.style.display = HideIf(isPhone);
    exe_Build.style.display = HideIf(isPhone);
    exe_Debug.style.display = HideIf(isPhone && !exe_Build.value);
    exe_Run.style.display = HideIf(isPhone);
    exe_Exe.style.display = HideIf(isPhone);
    exe_Setup.style.display = HideIf(isPhone);
  }

  #region Count Lines
  uint2 fileN = u00;
  string CountLines(string path, params string[] search)
  {
    fileN = u00;
    ProcessPath(path, search);
    return $"{(fileN.x <= 1 ? "" : $"{fileN.x}F,")} {(fileN.y > 10000 ? $"{fileN.y / 1000:#,###}K" : $"{fileN.y:#,###}")}";
  }

  string sceneName => SceneManager.GetActiveScene().name;

  string LineCountStr()
  {
    string dir = Directory.GetCurrentDirectory(), all = CountLines($@"{dir}\Assets", "*.cs", "*.cginc", "*.compute", "*.shader"),
      core = CountLines($@"{dir}\Assets\GS", "*.cs", "*.cginc"), scn = "";
    foreach (var a in GS_Assemblies)
      if ($@"{dir}\Assets\{a}\gs{sceneName}".Exists())
      {
        scn = CountLines($@"{dir}\Assets\{a}\gs{sceneName}", "*.cs", "*.compute", "*.shader");
        break;
      }
    return $"GS({core}) All({all}) Scene({scn})";
  }

  void CodeCount_clicked(ClickEvent evt) { CodeCount.value = LineCountStr(); }

  void ProcessPath(string dir, params string[] search)
  {
    if (dir.DoesNotExist()) return;
    foreach (var s in search) foreach (var f in FastDirectory.GetFiles(dir, s)) ProcessFile(f);
    foreach (var d in Directory.GetDirectories(dir)) ProcessPath(d, search);
  }

  void ProcessFile(string filename)
  {
    StreamReader f = File.OpenText(filename);
    int lineN = 0;
    bool inComment = false;
    while (f.Peek() >= 0)
    {
      string line = f.ReadLine().Trim();
      if (line.Contains("/*")) { inComment = true; line = line.Before("/*"); }
      if (line.Contains("*/")) { inComment = false; line = line.After("*/"); }
      if (!inComment)
      {
        if (line.Contains("//")) line = line.Before("//").Trim();
        if (line.IsNotEmpty())
        {
          lineN++;
          foreach (char c in line) if (c == '{' || c == '}' || c == ';') lineN++;
          if (line.EndsWith(";")) lineN--;
          if (line == "{") lineN--;
          if (line == "}") lineN--;
          while (line.Contains("for (")) { lineN -= 2; line = line.After("for ("); }
        }
      }
    }
    f.Close();
    fileN += new uint2(1, lineN);
  }
  #endregion Count Lines

  #region Build and Run
  protected void Write_issFile()
  {
    string appName = appPath.BeforeLast("/").AfterLast("/");
    string outputDir = appPath.BeforeLast("/").BeforeLast("/");
    string issFile = $"{appPath.BeforeLast("/")}_{exe_Version.text}.iss";
    string includeSource = appPath.Exists() ? "" : ";";
    StrBldr sb = new StrBldr().AddLines(
      $"#define MyAppName \"{appName}\"",
       "#define MyAppVersion \"1.000\"",
       "#define MyAppPublisher \"SummitPeak\"",
       "#define MyAppURL \"http://www.SummitPeakTechnologies.com/\"",
      $"#define MyOutputDir \"{outputDir}\"",
       "",
       "[Setup]",
      $"AppId={{{{{new_guid().ToString()}}}",
       "AppName={#MyAppName}",
       "AppVersion={#MyAppVersion}",
       "AppPublisher={#MyAppPublisher}",
       "AppPublisherURL={#MyAppURL}",
       "AppSupportURL={#MyAppURL}",
       "AppUpdatesURL={#MyAppURL}",
       "DefaultDirName={commondocs}\\{#MyAppName}",
       "DefaultGroupName={#MyAppName}",
       "AllowNoIcons=yes",
       "OutputDir={#MyOutputDir}",
      $"OutputBaseFilename={{#MyAppName}}_setup_{exe_Version.text}",
       ";Password=GS#123",
       "Compression=lzma",
       "SolidCompression=yes",
       "",
       "[Languages]",
       "Name: \"english\"; MessagesFile: \"compiler:Default.isl\"",
       "",
       "[Tasks]",
       "Name: \"desktopicon\"; Description: \"{cm:CreateDesktopIcon}\"; GroupDescription: \"{cm:AdditionalIcons}\"; Flags: unchecked",
       "",
       "[Files]",
      $"Source: {{#MyOutputDir}}/{{#MyAppName}} {exe_Version.text}.exe; DestDir: \"{{app}} {exe_Version.text}\"; Flags: ignoreversion",
      $"Source: {{#MyOutputDir}}/{{#MyAppName}} {exe_Version.text}_Data\\*; DestDir: \"{{app}} {exe_Version.text}\\{{#MyAppName}} {exe_Version.text}_Data\"; Flags: ignoreversion recursesubdirs createallsubdirs",
      $"Source: {{#MyOutputDir}}/MonoBleedingEdge\\*; DestDir: \"{{app}} {exe_Version.text}\\MonoBleedingEdge\"; Flags: ignoreversion recursesubdirs createallsubdirs",
      $"Source: {{#MyOutputDir}}/D3D12\\*; DestDir: \"{{app}} {exe_Version.text}\\D3D12\"; Flags: ignoreversion recursesubdirs createallsubdirs",
      $"Source: \"{{#MyOutputDir}}/{{#MyAppName}}/*\"; DestDir: \"{{app}} {exe_Version.text}/{{#MyAppName}}\"; Flags: ignoreversion"
      );

    string[] omitPaths = GS_Window.This.getOmitPaths.Select(a => "/" + a + "/").ToArray();
    string[] dirs = $"{outputDir}/{appName}/".GetAllDirectories().Select(a => a.Replace("\\", "/") + "/").ToArray();
    foreach (var dir in dirs)
    {
      if (dir.DoesNotContainAny(omitPaths) && dir.GetFiles("*").Length > 0)
      {
        string path = dir.After($"{outputDir}").Replace("\\", "/").BeforeLast("/");
        sb.Add($"\nSource: \"{{#MyOutputDir}}{path}/*\"; DestDir: \"{{app}} {exe_Version.text}\\{path}\"; Flags: ignoreversion");
      }
    }
    sb.Add(
      $"\nSource: {{#MyOutputDir}}/UnityPlayer.dll; DestDir: \"{{app}} {exe_Version.text}\"; Flags: ignoreversion",
       "\n; NOTE: Don't use \"Flags: ignoreversion\" on any shared system files",
       "\n",
       "\n[Icons]",
      $"\nName: \"{{group}}\\{{#MyAppName}}\"; Filename: \"{{app}} {exe_Version.text}\\{{#MyAppName}} {exe_Version.text}.exe\"",
      $"\nName: \"{{group}}\\{{cm:UninstallProgram,{{#MyAppName}} {exe_Version.text}\\}}\"; Filename: \"{{uninstallexe}}\"",
      $"\nName: \"{{commondesktop}}\\{{#MyAppName}}\"; Filename: \"{{app}} {exe_Version.text}\\{{#MyAppName}} {exe_Version.text}.exe\"; Tasks: desktopicon",
       "\n",
       "\n[Run]",
      $"\nFilename: \"{{app}} {exe_Version.text}\\{{#MyAppName}} {exe_Version.text}.exe\"; Description: \"{{cm:LaunchProgram,{{#StringChange(MyAppName, '&', '&&')}}}}\"; Flags: nowait postinstall skipifsilent");
    issFile.WriteAllTextAscii(sb);
  }

  protected bool CheckForInno()
  {
    if (@"C:\Program Files (x86)\Inno Setup 6\ISCC.exe".Exists()) return true;
    print(@"C:\Program Files (x86)\Inno Setup 6\ISCC.exe  does not exist, install Inno Setup from http://www.jrsoftware.org/download.php/is.exe");
    Application.OpenURL("http://www.jrsoftware.org/download.php/is.exe");
    return false;
  }

  public bool ok;
  public void Build_Parent()
  {
    if (ok)
    {
      string appName = appPath.BeforeLast("/").AfterLast("/"), exeFile = $"{appPath.BeforeLast("/")}.exe";
      string appDataPath = $"{appPath.BeforeLast("/").BeforeLast("/")}/{appName}_Data/";
      string appPath2 = $"{appPath.BeforeLast("/").BeforeLast("/").BeforeLast("/")}/{appName}/{appName}/";
      string appDataPath2 = $"{appPath.BeforeLast("/").BeforeLast("/").BeforeLast("/")}/{appName}/{appName}_Data/";
      string exeFile2 = $"{appPath2.BeforeLast("/")}.exe";

      string monoPath = $"{appPath.BeforeLast("/").BeforeLast("/")}/MonoBleedingEdge/";
      string monoPath2 = $"{appPath.BeforeLast("/").BeforeLast("/").BeforeLast("/")}/{appName}/MonoBleedingEdge/";
      string unityDll = $"{appPath.BeforeLast("/").BeforeLast("/")}/UnityPlayer.dll";
      string unityDll2 = $"{appPath.BeforeLast("/").BeforeLast("/").BeforeLast("/")}/{appName}/UnityPlayer.dll";

      appPath.MergeFolder(appPath2);

      try { appDataPath.MergeFolder(appDataPath2); } catch (Exception) { print($"Error, the application is running. Close it and try again."); }

      monoPath.MergeFolder(monoPath2);
      unityDll.CopyFile(unityDll2);

      exeFile.CopyFile(exeFile2);
    }
  }

  public void Run_GS_Exe()
  {
    string exeFile = $"{appPath.BeforeLast("/")} {exe_Version.text}.exe";
    exeFile.Run();
  }

  public void Run_Parent()
  {
    string appName = appPath.BeforeLast("/").AfterLast("/");
    string appPath2 = $"{appPath.BeforeLast("/").BeforeLast("/").BeforeLast("/")}/{appName}/{appName}/";
    string exeFile2 = $"{appPath2.BeforeLast("/")}_{exe_Version.text}.exe";
    exeFile2.Run();
  }

  public void Generate_GS_Setup(bool build)
  {
    string appName = appPath.BeforeLast("/").AfterLast("/");
    string outputDir = appPath.BeforeLast("/").BeforeLast("/"), issFile = $"{appPath.BeforeLast("/")}_{exe_Version.text}.iss",
      setupFile = $"{appPath.BeforeLast("/")}_setup_{exe_Version.text}.exe";
    string exe = $"{outputDir}/{appName} {exe_Version.text}.exe";
    if (build || exe.DoesNotExist())
    {
      Build_GS_Exe();
      if (!ok) return;
    }
    else ok = true;
    Write_issFile();
    if (!CheckForInno()) return;
    setupFile.DeleteFile();
    string filename = @"""C:\Program Files (x86)\Inno Setup 6\ISCC.exe""", args = $"\"{appPath.BeforeLast("/")}_{exe_Version.text}.iss\"";
    var b = new StringBuilder();
    Process process = new Process() { StartInfo = new ProcessStartInfo(filename, args) { UseShellExecute = false, CreateNoWindow = false, RedirectStandardError = true, RedirectStandardOutput = true, RedirectStandardInput = true } };
    process.OutputDataReceived += (sender, e) => b.Append(e.Data);
    process.Start(); process.BeginOutputReadLine(); process.WaitForExit(); process.CancelOutputRead(); process.Dispose();
    if (setupFile.Exists())
    { print($"Setup file generated: {setupFile}"); Beep(0.1f, 1000); }
    else { print($"Error in .iss file: {b.ToString()}"); Beep(0.1f, 500); }
  }

  public void exe_Exe_clicked(ClickEvent evt = null)
  {
    if (exe_Build.value)
    {
      Build_GS_Exe();
      if (exe_Parent.value) { Build_Parent(); if (exe_Run.value && ok) Run_Parent(); } else if (exe_Run.value && ok) Run_GS_Exe();
    }
    else if (exe_Run.value)
    {
      if (exe_Parent.value) Run_Parent(); else Run_GS_Exe();
    }
  }
  public void exe_Setup_clicked(ClickEvent evt = null) { Generate_GS_Setup(exe_Build.value); }
  public BuildPlayerOptions BuildSettings()
  {
    PlayerSettings.companyName = "SummitPeak";
    string appName = appPath.BeforeLast("/").AfterLast("/");
    PlayerSettings.productName = $"{appName} {exe_Version.text}";
    string appIcon = $"{Application.dataPath}/{gsName}/Icon.png", icon = $"{Application.dataPath}/Icon.png", defaultIcon = $"{Application.dataPath}/Icon_Default.png";
    (appIcon.Exists() ? appIcon : defaultIcon).CopyFile(icon);
    icon.ImportAsset();
    PlayerSettings.bundleVersion = $"{DateTime.Now:yyyy.MM.dd.HH.mm}";
    BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
    buildPlayerOptions.scenes = new[] { SceneManager.GetActiveScene().path };
    return buildPlayerOptions;
  }
  public void Build_Report(BuildPlayerOptions buildPlayerOptions)
  {
    BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
    BuildSummary summary = report.summary;
    ok = summary.result == BuildResult.Succeeded;
    if (ok) print($"Build succeeded: {summary.totalSize} bytes"); else { print("Build failed"); Beep(0.1f, 500); }
  }

  public void Build_GS_Exe()
  {
    BuildPlayerOptions buildPlayerOptions = BuildSettings();
    PlayerSettings.fullScreenMode = UnityEngine.FullScreenMode.Windowed;
    PlayerSettings.resizableWindow = true;
    PlayerSettings.runInBackground = true;
    UnityEngine.Rendering.GraphicsDeviceType[] apis = PlayerSettings.GetGraphicsAPIs(BuildTarget.StandaloneWindows);
    if (apis.Length == 1)
    {
      var apis2 = new UnityEngine.Rendering.GraphicsDeviceType[2];
      apis2[1] = apis[0];
      apis2[0] = UnityEngine.Rendering.GraphicsDeviceType.Direct3D12;
      apis = apis2;
    }
    PlayerSettings.SetGraphicsAPIs(BuildTarget.StandaloneWindows, apis);
    PlayerSettings.SetApiCompatibilityLevel(NamedBuildTarget.Standalone, ApiCompatibilityLevel.NET_4_6);
    buildPlayerOptions.locationPathName = $"{appName} {exe_Version.text}.exe";
    buildPlayerOptions.target = BuildTarget.StandaloneWindows64;
    buildPlayerOptions.options = exe_Debug.value ? BuildOptions.Development | BuildOptions.AllowDebugging : BuildOptions.None;
    buildPlayerOptions.targetGroup = BuildTargetGroup.Standalone;
    Build_Report(buildPlayerOptions);
  }

  public void exe_Apk_clicked(ClickEvent evt = null) { StartCoroutine(Apk_Coroutine()); }
  IEnumerator Apk_Coroutine()
  {
    BuildPlayerOptions buildPlayerOptions = BuildSettings();
    PlayerSettings.SetIl2CppCompilerConfiguration(NamedBuildTarget.Android, Il2CppCompilerConfiguration.Release);
    PlayerSettings.SetIl2CppCodeGeneration(NamedBuildTarget.Android, Il2CppCodeGeneration.OptimizeSpeed);
    buildPlayerOptions.locationPathName = $"{appName}/{appName}.apk";
    buildPlayerOptions.target = BuildTarget.Android;
    buildPlayerOptions.options = BuildOptions.AutoRunPlayer;
    yield return null;
    Build_Report(buildPlayerOptions);
  }
  public bool Show_exe_Apk_CMake => $@"C:\Program Files\Unity\Hub\Editor\{UnityVersion}\Editor\Data\PlaybackEngines\AndroidPlayer\SDK\cmake".DoesNotExist();
  public void exe_Apk_CMake_clicked(ClickEvent evt = null)
  {
    if (EditorUtility.DisplayDialog("Fix", $"Copy CMake 3.22.1 from 2023.1.0a26 to {UnityVersion}?", "Fix", "Abort Fix"))
    {
      string cmake0 = @"C:\Program Files\Unity\Hub\Editor\2023.1.0a26\Editor\Data\PlaybackEngines\AndroidPlayer\SDK\cmake";
      string cmake1 = $@"C:\Program Files\Unity\Hub\Editor\{UnityVersion}\Editor\Data\PlaybackEngines\AndroidPlayer\SDK\cmake";
      cmake0.CopyDirAll(cmake1);
    }
  }
  #endregion Build and Run

  #region Unity Project Menu
  public static string Get_Report_Suffix(string path)
  {
    string report = @$"{path}gsReport.txt";
    if (report.Exists()) return report.ReadAllText().Between("suffixName\": \"", "\"");
    return "";
  }
  public static string Get_Selected_Path_from_guiID(string guiID)
  {
    string path0 = Path.Combine(Directory.GetCurrentDirectory(), AssetDatabase.GUIDToAssetPath(guiID));
    string path = File.GetAttributes(path0).HasFlag(FileAttributes.Directory) ? path0 : Path.GetDirectoryName(path0);
    path = path.Replace("\\", "/");
    if (path.DoesNotEndWith("/")) path += "/";
    return path;
  }
  public static string[] Get_Report_Files()
  {
    var files = new List<string>();
    foreach (string id in Selection.assetGUIDs)
    {
      string path = Get_Selected_Path_from_guiID(id), root_name = path.BeforeLast("/").AfterLast("/gs"), root_path = path.Before("/Assets/") + "/";
      string suffix = Get_Report_Suffix($"{root_path}{root_name}/"), reportFile = $"{root_path}{root_name}/{suffix}/{root_name}.txt";
      if (reportFile.Exists()) files.Add(reportFile);
    }
    return files.ToArray();
  }
  public static string[] Get_Report_HTML_Files()
  {
    var files = new List<string>();
    foreach (string id in Selection.assetGUIDs)
    {
      string path = Get_Selected_Path_from_guiID(id), root_name = path.BeforeLast("/").AfterLast("/gs"), root_path = path.Before("/Assets/") + "/";
      string suffix = Get_Report_Suffix($"{root_path}{root_name}/"), htmlFile = $"{root_path}{root_name}/{suffix}/HTML/{root_name}.html";
      if (htmlFile.Exists()) files.Add(htmlFile);
    }
    return files.ToArray();
  }

  [MenuItem("Assets/Edit GpuScript Report")] public static void Edit_gsReport() { foreach (var report in Get_Report_Files()) Open_File_in_Visual_Studio(report); }
  [MenuItem("Assets/Edit GpuScript Report", validate = true, priority = 111, secondaryPriority = 1)] public static bool Validate_Edit_gsReport() => Get_Report_Files().Length > 0;
  [MenuItem("Assets/Edit GpuScript HTML")] public static void Edit_gsReport_HTML() { foreach (var report in Get_Report_HTML_Files()) Open_File_in_Visual_Studio(report); }
  [MenuItem("Assets/Edit GpuScript HTML", validate = true, priority = 122, secondaryPriority = 2)] public static bool Validate_Edit_gsReport_HTML() => Get_Report_HTML_Files().Length > 0;
  #endregion Unity Project Menu
}
