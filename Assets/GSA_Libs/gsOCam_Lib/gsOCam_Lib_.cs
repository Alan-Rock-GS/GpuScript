using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using GpuScript;
using System.Text;
public class gsOCam_Lib_ : GS
{
  [HideInInspector] public uiData data;
  [HideInInspector] public GOCam_Lib g; public GOCam_Lib G { get { gOCam_Lib.GetData(); return gOCam_Lib[0]; } }
  public void g_SetData() { if (gChanged && gOCam_Lib != null) { gOCam_Lib[0] = g; gOCam_Lib.SetData(); gChanged = false; } }
  public virtual void BDraw_tab_delimeted_text_SetKernels(bool reallocate = false) { if (BDraw_tab_delimeted_text != null && (reallocate || BDraw_tab_delimeted_text.reallocated)) { SetKernelValues(BDraw_tab_delimeted_text, nameof(BDraw_tab_delimeted_text), kernel_BDraw_AppendBuff_GetSums, kernel_BDraw_AppendBuff_Get_Bits, kernel_BDraw_getTextInfo); BDraw_tab_delimeted_text.reallocated = false; } }
  public virtual void BDraw_textInfos_SetKernels(bool reallocate = false) { if (BDraw_textInfos != null && (reallocate || BDraw_textInfos.reallocated)) { SetKernelValues(BDraw_textInfos, nameof(BDraw_textInfos), kernel_BDraw_setDefaultTextInfo, kernel_BDraw_getTextInfo); BDraw_textInfos.reallocated = false; } }
  public virtual void BDraw_fontInfos_SetKernels(bool reallocate = false) { if (BDraw_fontInfos != null && (reallocate || BDraw_fontInfos.reallocated)) { SetKernelValues(BDraw_fontInfos, nameof(BDraw_fontInfos), kernel_BDraw_getTextInfo); BDraw_fontInfos.reallocated = false; } }
  public virtual void paletteBuffer_SetKernels(bool reallocate = false) { if (paletteBuffer != null && (reallocate || paletteBuffer.reallocated)) { SetKernelValues(paletteBuffer, nameof(paletteBuffer)); paletteBuffer.reallocated = false; } }
  public virtual void BDraw_AppendBuff_Bits_SetKernels(bool reallocate = false) { if (BDraw_AppendBuff_Bits != null && (reallocate || BDraw_AppendBuff_Bits.reallocated)) { SetKernelValues(BDraw_AppendBuff_Bits, nameof(BDraw_AppendBuff_Bits), kernel_BDraw_AppendBuff_GetIndexes, kernel_BDraw_AppendBuff_Get_Bits_Sums, kernel_BDraw_AppendBuff_GetSums, kernel_BDraw_AppendBuff_Get_Bits); BDraw_AppendBuff_Bits.reallocated = false; } }
  public virtual void BDraw_AppendBuff_Sums_SetKernels(bool reallocate = false) { if (BDraw_AppendBuff_Sums != null && (reallocate || BDraw_AppendBuff_Sums.reallocated)) { SetKernelValues(BDraw_AppendBuff_Sums, nameof(BDraw_AppendBuff_Sums), kernel_BDraw_AppendBuff_GetIndexes, kernel_BDraw_AppendBuff_IncSums, kernel_BDraw_AppendBuff_GetFills1, kernel_BDraw_AppendBuff_Get_Bits_Sums, kernel_BDraw_AppendBuff_GetSums); BDraw_AppendBuff_Sums.reallocated = false; } }
  public virtual void BDraw_AppendBuff_Indexes_SetKernels(bool reallocate = false) { if (BDraw_AppendBuff_Indexes != null && (reallocate || BDraw_AppendBuff_Indexes.reallocated)) { SetKernelValues(BDraw_AppendBuff_Indexes, nameof(BDraw_AppendBuff_Indexes), kernel_BDraw_AppendBuff_GetIndexes, kernel_BDraw_getTextInfo); BDraw_AppendBuff_Indexes.reallocated = false; } }
  public virtual void BDraw_AppendBuff_Fills1_SetKernels(bool reallocate = false) { if (BDraw_AppendBuff_Fills1 != null && (reallocate || BDraw_AppendBuff_Fills1.reallocated)) { SetKernelValues(BDraw_AppendBuff_Fills1, nameof(BDraw_AppendBuff_Fills1), kernel_BDraw_AppendBuff_IncSums, kernel_BDraw_AppendBuff_IncFills1, kernel_BDraw_AppendBuff_GetFills2, kernel_BDraw_AppendBuff_GetFills1); BDraw_AppendBuff_Fills1.reallocated = false; } }
  public virtual void BDraw_AppendBuff_Fills2_SetKernels(bool reallocate = false) { if (BDraw_AppendBuff_Fills2 != null && (reallocate || BDraw_AppendBuff_Fills2.reallocated)) { SetKernelValues(BDraw_AppendBuff_Fills2, nameof(BDraw_AppendBuff_Fills2), kernel_BDraw_AppendBuff_IncFills1, kernel_BDraw_AppendBuff_GetFills2); BDraw_AppendBuff_Fills2.reallocated = false; } }
  public virtual void legendSphereColors_SetKernels(bool reallocate = false) { if (legendSphereColors != null && (reallocate || legendSphereColors.reallocated)) { SetKernelValues(legendSphereColors, nameof(legendSphereColors)); legendSphereColors.reallocated = false; } }
  public virtual void Gpu_BDraw_AppendBuff_GetIndexes() { g_SetData(); BDraw_AppendBuff_Bits?.SetCpu(); BDraw_AppendBuff_Bits_SetKernels(); BDraw_AppendBuff_Sums?.SetCpu(); BDraw_AppendBuff_Sums_SetKernels(); BDraw_AppendBuff_Indexes_SetKernels(); Gpu(kernel_BDraw_AppendBuff_GetIndexes, BDraw_AppendBuff_GetIndexes, BDraw_AppendBuff_BitN); BDraw_AppendBuff_Indexes?.ResetWrite(); }
  public virtual void Cpu_BDraw_AppendBuff_GetIndexes() { BDraw_AppendBuff_Bits?.GetGpu(); BDraw_AppendBuff_Sums?.GetGpu(); BDraw_AppendBuff_Indexes?.GetGpu(); Cpu(BDraw_AppendBuff_GetIndexes, BDraw_AppendBuff_BitN); BDraw_AppendBuff_Indexes.SetData(); }
  public virtual void Cpu_BDraw_AppendBuff_GetIndexes(uint3 id) { BDraw_AppendBuff_Bits?.GetGpu(); BDraw_AppendBuff_Sums?.GetGpu(); BDraw_AppendBuff_Indexes?.GetGpu(); BDraw_AppendBuff_GetIndexes(id); BDraw_AppendBuff_Indexes.SetData(); }
  public virtual void Gpu_BDraw_AppendBuff_IncSums() { g_SetData(); BDraw_AppendBuff_Sums?.SetCpu(); BDraw_AppendBuff_Sums_SetKernels(); BDraw_AppendBuff_Fills1?.SetCpu(); BDraw_AppendBuff_Fills1_SetKernels(); gOCam_Lib?.SetCpu(); Gpu(kernel_BDraw_AppendBuff_IncSums, BDraw_AppendBuff_IncSums, BDraw_AppendBuff_BitN); BDraw_AppendBuff_Sums?.ResetWrite(); g = G; }
  public virtual void Cpu_BDraw_AppendBuff_IncSums() { BDraw_AppendBuff_Sums?.GetGpu(); BDraw_AppendBuff_Fills1?.GetGpu(); Cpu(BDraw_AppendBuff_IncSums, BDraw_AppendBuff_BitN); BDraw_AppendBuff_Sums.SetData(); }
  public virtual void Cpu_BDraw_AppendBuff_IncSums(uint3 id) { BDraw_AppendBuff_Sums?.GetGpu(); BDraw_AppendBuff_Fills1?.GetGpu(); BDraw_AppendBuff_IncSums(id); BDraw_AppendBuff_Sums.SetData(); }
  public virtual void Gpu_BDraw_AppendBuff_IncFills1() { g_SetData(); BDraw_AppendBuff_Fills1?.SetCpu(); BDraw_AppendBuff_Fills1_SetKernels(); BDraw_AppendBuff_Fills2?.SetCpu(); BDraw_AppendBuff_Fills2_SetKernels(); Gpu(kernel_BDraw_AppendBuff_IncFills1, BDraw_AppendBuff_IncFills1, BDraw_AppendBuff_BitN1); BDraw_AppendBuff_Fills1?.ResetWrite(); }
  public virtual void Cpu_BDraw_AppendBuff_IncFills1() { BDraw_AppendBuff_Fills1?.GetGpu(); BDraw_AppendBuff_Fills2?.GetGpu(); Cpu(BDraw_AppendBuff_IncFills1, BDraw_AppendBuff_BitN1); BDraw_AppendBuff_Fills1.SetData(); }
  public virtual void Cpu_BDraw_AppendBuff_IncFills1(uint3 id) { BDraw_AppendBuff_Fills1?.GetGpu(); BDraw_AppendBuff_Fills2?.GetGpu(); BDraw_AppendBuff_IncFills1(id); BDraw_AppendBuff_Fills1.SetData(); }
  public virtual void Gpu_BDraw_AppendBuff_GetFills2() { g_SetData(); BDraw_AppendBuff_Fills1?.SetCpu(); BDraw_AppendBuff_Fills1_SetKernels(); BDraw_AppendBuff_Fills2_SetKernels(); Gpu(kernel_BDraw_AppendBuff_GetFills2, BDraw_AppendBuff_GetFills2, BDraw_AppendBuff_BitN2); BDraw_AppendBuff_Fills2?.ResetWrite(); }
  public virtual IEnumerator Cpu_BDraw_AppendBuff_GetFills2() { BDraw_AppendBuff_Fills1?.GetGpu(); BDraw_AppendBuff_Fills2?.GetGpu(); yield return StartCoroutine(Cpu_Coroutine(kernel_BDraw_AppendBuff_GetFills2, BDraw_AppendBuff_GetFills2, BDraw_AppendBuff_BitN2)); }
  public virtual void Cpu_BDraw_AppendBuff_GetFills2(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { BDraw_AppendBuff_Fills1?.GetGpu(); BDraw_AppendBuff_Fills2?.GetGpu(); BDraw_AppendBuff_GetFills2(grp_tid, grp_id, id, grpI); BDraw_AppendBuff_Fills2.SetData(); }
  public virtual void Gpu_BDraw_AppendBuff_GetFills1() { g_SetData(); BDraw_AppendBuff_Sums?.SetCpu(); BDraw_AppendBuff_Sums_SetKernels(); BDraw_AppendBuff_Fills1_SetKernels(); Gpu(kernel_BDraw_AppendBuff_GetFills1, BDraw_AppendBuff_GetFills1, BDraw_AppendBuff_BitN1); BDraw_AppendBuff_Fills1?.ResetWrite(); }
  public virtual IEnumerator Cpu_BDraw_AppendBuff_GetFills1() { BDraw_AppendBuff_Sums?.GetGpu(); BDraw_AppendBuff_Fills1?.GetGpu(); yield return StartCoroutine(Cpu_Coroutine(kernel_BDraw_AppendBuff_GetFills1, BDraw_AppendBuff_GetFills1, BDraw_AppendBuff_BitN1)); }
  public virtual void Cpu_BDraw_AppendBuff_GetFills1(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { BDraw_AppendBuff_Sums?.GetGpu(); BDraw_AppendBuff_Fills1?.GetGpu(); BDraw_AppendBuff_GetFills1(grp_tid, grp_id, id, grpI); BDraw_AppendBuff_Fills1.SetData(); }
  public virtual void Gpu_BDraw_AppendBuff_Get_Bits_Sums() { g_SetData(); BDraw_AppendBuff_Bits?.SetCpu(); BDraw_AppendBuff_Bits_SetKernels(); BDraw_AppendBuff_Sums_SetKernels(); Gpu(kernel_BDraw_AppendBuff_Get_Bits_Sums, BDraw_AppendBuff_Get_Bits_Sums, BDraw_AppendBuff_BitN); BDraw_AppendBuff_Sums?.ResetWrite(); }
  public virtual IEnumerator Cpu_BDraw_AppendBuff_Get_Bits_Sums() { BDraw_AppendBuff_Bits?.GetGpu(); BDraw_AppendBuff_Sums?.GetGpu(); yield return StartCoroutine(Cpu_Coroutine(kernel_BDraw_AppendBuff_Get_Bits_Sums, BDraw_AppendBuff_Get_Bits_Sums, BDraw_AppendBuff_BitN)); }
  public virtual void Cpu_BDraw_AppendBuff_Get_Bits_Sums(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { BDraw_AppendBuff_Bits?.GetGpu(); BDraw_AppendBuff_Sums?.GetGpu(); BDraw_AppendBuff_Get_Bits_Sums(grp_tid, grp_id, id, grpI); BDraw_AppendBuff_Sums.SetData(); }
  public virtual void Gpu_BDraw_AppendBuff_GetSums() { g_SetData(); BDraw_AppendBuff_Bits_SetKernels(); BDraw_AppendBuff_Sums_SetKernels(); BDraw_tab_delimeted_text?.SetCpu(); BDraw_tab_delimeted_text_SetKernels(); Gpu(kernel_BDraw_AppendBuff_GetSums, BDraw_AppendBuff_GetSums, BDraw_AppendBuff_BitN); BDraw_AppendBuff_Bits?.ResetWrite(); BDraw_AppendBuff_Sums?.ResetWrite(); }
  public virtual IEnumerator Cpu_BDraw_AppendBuff_GetSums() { BDraw_AppendBuff_Bits?.GetGpu(); BDraw_AppendBuff_Sums?.GetGpu(); BDraw_tab_delimeted_text?.GetGpu(); yield return StartCoroutine(Cpu_Coroutine(kernel_BDraw_AppendBuff_GetSums, BDraw_AppendBuff_GetSums, BDraw_AppendBuff_BitN)); }
  public virtual void Cpu_BDraw_AppendBuff_GetSums(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { BDraw_AppendBuff_Bits?.GetGpu(); BDraw_AppendBuff_Sums?.GetGpu(); BDraw_tab_delimeted_text?.GetGpu(); BDraw_AppendBuff_GetSums(grp_tid, grp_id, id, grpI); BDraw_AppendBuff_Bits.SetData(); BDraw_AppendBuff_Sums.SetData(); }
  public virtual void Gpu_BDraw_AppendBuff_Get_Bits() { g_SetData(); BDraw_AppendBuff_Bits_SetKernels(); BDraw_tab_delimeted_text?.SetCpu(); BDraw_tab_delimeted_text_SetKernels(); Gpu(kernel_BDraw_AppendBuff_Get_Bits, BDraw_AppendBuff_Get_Bits, BDraw_AppendBuff_BitN); BDraw_AppendBuff_Bits?.ResetWrite(); }
  public virtual void Cpu_BDraw_AppendBuff_Get_Bits() { BDraw_AppendBuff_Bits?.GetGpu(); BDraw_tab_delimeted_text?.GetGpu(); Cpu(BDraw_AppendBuff_Get_Bits, BDraw_AppendBuff_BitN); BDraw_AppendBuff_Bits.SetData(); }
  public virtual void Cpu_BDraw_AppendBuff_Get_Bits(uint3 id) { BDraw_AppendBuff_Bits?.GetGpu(); BDraw_tab_delimeted_text?.GetGpu(); BDraw_AppendBuff_Get_Bits(id); BDraw_AppendBuff_Bits.SetData(); }
  public virtual void Gpu_BDraw_setDefaultTextInfo() { g_SetData(); BDraw_textInfos?.SetCpu(); BDraw_textInfos_SetKernels(); Gpu(kernel_BDraw_setDefaultTextInfo, BDraw_setDefaultTextInfo, BDraw_textN); BDraw_textInfos?.ResetWrite(); }
  public virtual void Cpu_BDraw_setDefaultTextInfo() { BDraw_textInfos?.GetGpu(); Cpu(BDraw_setDefaultTextInfo, BDraw_textN); BDraw_textInfos.SetData(); }
  public virtual void Cpu_BDraw_setDefaultTextInfo(uint3 id) { BDraw_textInfos?.GetGpu(); BDraw_setDefaultTextInfo(id); BDraw_textInfos.SetData(); }
  public virtual void Gpu_BDraw_getTextInfo() { g_SetData(); BDraw_fontInfos?.SetCpu(); BDraw_fontInfos_SetKernels(); BDraw_textInfos?.SetCpu(); BDraw_textInfos_SetKernels(); BDraw_AppendBuff_Indexes?.SetCpu(); BDraw_AppendBuff_Indexes_SetKernels(); BDraw_tab_delimeted_text?.SetCpu(); BDraw_tab_delimeted_text_SetKernels(); Gpu(kernel_BDraw_getTextInfo, BDraw_getTextInfo, BDraw_textN); BDraw_textInfos?.ResetWrite(); }
  public virtual void Cpu_BDraw_getTextInfo() { BDraw_fontInfos?.GetGpu(); BDraw_textInfos?.GetGpu(); BDraw_AppendBuff_Indexes?.GetGpu(); BDraw_tab_delimeted_text?.GetGpu(); Cpu(BDraw_getTextInfo, BDraw_textN); BDraw_textInfos.SetData(); }
  public virtual void Cpu_BDraw_getTextInfo(uint3 id) { BDraw_fontInfos?.GetGpu(); BDraw_textInfos?.GetGpu(); BDraw_AppendBuff_Indexes?.GetGpu(); BDraw_tab_delimeted_text?.GetGpu(); BDraw_getTextInfo(id); BDraw_textInfos.SetData(); }
  [JsonConverter(typeof(StringEnumConverter))] public enum BDraw_Draw : uint { Point, Sphere, Line, Arrow, Signal, LineSegment, Texture_2D, Quad, WebCam, Mesh, Number, N }
  [JsonConverter(typeof(StringEnumConverter))] public enum BDraw_TextAlignment : uint { BottomLeft, CenterLeft, TopLeft, BottomCenter, CenterCenter, TopCenter, BottomRight, CenterRight, TopRight }
  [JsonConverter(typeof(StringEnumConverter))] public enum BDraw_Text_QuadType : uint { FrontOnly, FrontBack, Switch, Arrow, Billboard }
  [JsonConverter(typeof(StringEnumConverter))] public enum ProjectionMode : uint { Automatic, Perspective, Orthographic }
  [JsonConverter(typeof(StringEnumConverter))] public enum PlotBackground : uint { White, Default_Sky, WebCam_Front, WebCam_Back }
  [JsonConverter(typeof(StringEnumConverter))] public enum PaletteType : uint { Rainbow, GradientRainbow, GradientRainbow10, GradientRainbow20, Heat, GradientHeat, WhiteRainbow, invRainbow, Green, Gray, DarkGray, CT }
  public const uint BDraw_Draw_Point = 0, BDraw_Draw_Sphere = 1, BDraw_Draw_Line = 2, BDraw_Draw_Arrow = 3, BDraw_Draw_Signal = 4, BDraw_Draw_LineSegment = 5, BDraw_Draw_Texture_2D = 6, BDraw_Draw_Quad = 7, BDraw_Draw_WebCam = 8, BDraw_Draw_Mesh = 9, BDraw_Draw_Number = 10, BDraw_Draw_N = 11;
  public const uint BDraw_TextAlignment_BottomLeft = 0, BDraw_TextAlignment_CenterLeft = 1, BDraw_TextAlignment_TopLeft = 2, BDraw_TextAlignment_BottomCenter = 3, BDraw_TextAlignment_CenterCenter = 4, BDraw_TextAlignment_TopCenter = 5, BDraw_TextAlignment_BottomRight = 6, BDraw_TextAlignment_CenterRight = 7, BDraw_TextAlignment_TopRight = 8;
  public const uint BDraw_Text_QuadType_FrontOnly = 0, BDraw_Text_QuadType_FrontBack = 1, BDraw_Text_QuadType_Switch = 2, BDraw_Text_QuadType_Arrow = 3, BDraw_Text_QuadType_Billboard = 4;
  public const uint ProjectionMode_Automatic = 0, ProjectionMode_Perspective = 1, ProjectionMode_Orthographic = 2;
  public const uint PlotBackground_White = 0, PlotBackground_Default_Sky = 1, PlotBackground_WebCam_Front = 2, PlotBackground_WebCam_Back = 3;
  public const uint PaletteType_Rainbow = 0, PaletteType_GradientRainbow = 1, PaletteType_GradientRainbow10 = 2, PaletteType_GradientRainbow20 = 3, PaletteType_Heat = 4, PaletteType_GradientHeat = 5, PaletteType_WhiteRainbow = 6, PaletteType_invRainbow = 7, PaletteType_Green = 8, PaletteType_Gray = 9, PaletteType_DarkGray = 10, PaletteType_CT = 11;
  public const uint BDraw_Draw_Text3D = 12, BDraw_LF = 10, BDraw_TB = 9, BDraw_ZERO = 48, BDraw_NINE = 57, BDraw_PERIOD = 46, BDraw_COMMA = 44, BDraw_PLUS = 43, BDraw_MINUS = 45, BDraw_SPACE = 32, DrawType_Legend = 1;
  [HideInInspector] public bool ValuesChanged, gChanged;
  public static gsOCam_Lib This;
  public virtual void Awake() { This = this as gsOCam_Lib; Awake_GS(); }
  public virtual void Awake_GS()
  {
  }
  public virtual void Start() { Build_UI(); Load_UI(); Start0_GS(); InitBuffers(); Start1_GS(); }
  public virtual void Start0_GS()
  {
    BDraw_Start0_GS();
  }
  public virtual void Start1_GS()
  {
    BDraw_Start1_GS();
  }
  public override void onLoaded()
  {
    base.onLoaded();
  }
  [HideInInspector] public bool already_quited = false;
  public override void OnApplicationQuit() { Save_UI(); OnApplicationQuit_GS(); base.OnApplicationQuit(); already_quited = true; }
  public virtual void OnApplicationQuit_GS()
  {
    BDraw_OnApplicationQuit_GS();
  }
  public override void ui_to_data()
  {
    if (data == null) return;
    data.siUnits = siUnits;
    data.group_Cam = group_Cam;
    data.group_CamTransform = group_CamTransform;
    data.center = center;
    data.Default_Center = Default_Center;
    data.dist = dist;
    data.tiltSpin = tiltSpin;
    data.projection = projection;
    data.group_CamRanges = group_CamRanges;
    data.tiltRange = tiltRange;
    data.distSpeed = distSpeed;
    data.rotationSpeed = rotationSpeed;
    data.distanceRange = distanceRange;
    data.checkCollisions = checkCollisions;
    data.orthoTiltSpin = orthoTiltSpin;
    data.orthoSize = orthoSize;
    data.group_CamShow = group_CamShow;
    data.plotBackground = plotBackground;
    data.multiCams = multiCams;
    data.group_CamLegend = group_CamLegend;
    data.displayLegend = displayLegend;
    data.displayLegendPalette = displayLegendPalette;
    data.paletteType = paletteType;
    data.legendRange = legendRange;
    data.legendTitle = legendTitle;
    data.legendAxisTitle = legendAxisTitle;
    data.legendFormat = legendFormat;
    data.legendSphereTitles = legendSphereTitles;
    data.legendViewWidthPixels = legendViewWidthPixels;
    data.displayLegendBackground = displayLegendBackground;
    data.group_CamActions = group_CamActions;
  }
  public override void data_to_ui()
  {
    if (data == null) return;
    group_Cam = data.group_Cam;
    group_CamTransform = data.group_CamTransform;
    center = ui_txt_str.Contains("\"center\"") ? data.center : float3("0");
    Default_Center = ui_txt_str.Contains("\"Default_Center\"") ? data.Default_Center : float3("0");
    dist = ui_txt_str.Contains("\"dist\"") ? data.dist : 5f;
    tiltSpin = ui_txt_str.Contains("\"tiltSpin\"") ? data.tiltSpin : float2("0");
    projection = ui_txt_str.Contains("\"projection\"") ? data.projection : ProjectionMode.Automatic;
    group_CamRanges = data.group_CamRanges;
    tiltRange = ui_txt_str.Contains("\"tiltRange\"") ? data.tiltRange : float2("-45, 90");
    distSpeed = ui_txt_str.Contains("\"distSpeed\"") ? data.distSpeed : 10f;
    rotationSpeed = ui_txt_str.Contains("\"rotationSpeed\"") ? data.rotationSpeed : float2("129");
    distanceRange = ui_txt_str.Contains("\"distanceRange\"") ? data.distanceRange : float2("0.000001, 1000000");
    checkCollisions = data.checkCollisions;
    orthoTiltSpin = ui_txt_str.Contains("\"orthoTiltSpin\"") ? data.orthoTiltSpin : float2("25, -30");
    orthoSize = ui_txt_str.Contains("\"orthoSize\"") ? data.orthoSize : 1f;
    group_CamShow = data.group_CamShow;
    plotBackground = ui_txt_str.Contains("\"plotBackground\"") ? data.plotBackground : PlotBackground.Default_Sky;
    multiCams = data.multiCams;
    group_CamLegend = data.group_CamLegend;
    displayLegend = data.displayLegend;
    displayLegendPalette = data.displayLegendPalette;
    paletteType = data.paletteType;
    legendRange = data.legendRange;
    legendTitle = data.legendTitle;
    legendAxisTitle = data.legendAxisTitle;
    legendFormat = data.legendFormat;
    legendSphereTitles = data.legendSphereTitles;
    legendViewWidthPixels = ui_txt_str.Contains("\"legendViewWidthPixels\"") ? data.legendViewWidthPixels : 100;
    displayLegendBackground = data.displayLegendBackground;
    group_CamActions = data.group_CamActions;
  }
  public virtual void Save(string path, string projectName)
  {
    projectPaths = path;
    $"{projectPath}{projectName}.txt".WriteAllText(JsonConvert.SerializeObject(data, Formatting.Indented));
    foreach (var lib in GetComponents<GS>()) if (lib != this) lib.Save_UI();
    string usFile = $"{projectPath}usUnits.txt";
    if (siUnits) usFile.DeleteFile();
    else usFile.WriteAllText("usUnits");
  }
  public override bool Save_UI_As(string path, string projectName)
  {
    if (already_quited) return false;
    if (data != null) ui_to_data();
    if (lib_parent_gs == this) Save(path, projectName);
    else $"{path}{projectName}.txt".WriteAllText(JsonConvert.SerializeObject(data, Formatting.Indented));
    return true;
  }
  public override bool Load_UI_As(string path, string projectName)
  {
    if (path == appPath && SelectedProjectFile.Exists())
      path = $"{appPath}{SelectedProjectFile.ReadAllText()}/".Replace("//", "/");
    if(loadedProjectPath == path) return false;
    projectPaths = loadedProjectPath = path;
    string file = $"{projectPath}{projectName}.txt";
    data = file.Exists() ? JsonConvert.DeserializeObject<uiData>(ui_txt_str = file.ReadAllText()) : new uiData();
    if(data == null) return false;
    foreach (var fld in data.GetType().GetFields(bindings)) if (fld != null && fld.FieldType == typeof(TreeGroup) && fld.GetValue(data) == null) fld.SetValue(data, new TreeGroup() { isChecked = true });
    data_to_ui();
    UI_group_Cam?.Display_Tree();
    if (lib_parent_gs == this)
    {
      foreach (var lib in GetComponents<GS>())
        if (lib != this && lib.GetType() != "gsProject_Lib".ToType())
        {
          lib.Build_UI();
          lib.Load_UI();
        }
    }
    ui_loaded = true;
    return true;
  }
  public virtual void OnApplicationPause(bool pause) { if (ui_loaded) Save_UI(); }
  [HideInInspector] public uint lateUpdateI = 0;
  public virtual void LateUpdate()
  {
    if (!ui_loaded) return;
    string usFile = $"{projectPath}usUnits.txt";
    if (lateUpdateI == 5 && usFile.Exists()) { usFile.DeleteFile(); siUnits = false; OnUnitsChanged(); }
    LateUpdate0_GS();
    if (UI_center.Changed || any(center != UI_center.v)) center = UI_center.v;
    if (UI_Default_Center.Changed || any(Default_Center != UI_Default_Center.v)) Default_Center = UI_Default_Center.v;
    if (UI_dist.Changed || dist != UI_dist.v) dist = UI_dist.v;
    if (UI_tiltSpin.Changed || any(tiltSpin != UI_tiltSpin.v)) tiltSpin = UI_tiltSpin.v;
    if (UI_projection.Changed || (uint)projection != UI_projection.v) projection = (ProjectionMode)UI_projection.v;
    if (UI_tiltRange.Changed || any(tiltRange != UI_tiltRange.v)) tiltRange = UI_tiltRange.v;
    if (UI_distSpeed.Changed || distSpeed != UI_distSpeed.v) distSpeed = UI_distSpeed.v;
    if (UI_rotationSpeed.Changed || any(rotationSpeed != UI_rotationSpeed.v)) rotationSpeed = UI_rotationSpeed.v;
    if (UI_distanceRange.Changed || any(distanceRange != UI_distanceRange.v)) distanceRange = UI_distanceRange.v;
    if (UI_checkCollisions.Changed || checkCollisions != UI_checkCollisions.v) checkCollisions = UI_checkCollisions.v;
    if (UI_orthoTiltSpin.Changed || any(orthoTiltSpin != UI_orthoTiltSpin.v)) orthoTiltSpin = UI_orthoTiltSpin.v;
    if (UI_orthoSize.Changed || orthoSize != UI_orthoSize.v) orthoSize = UI_orthoSize.v;
    if (UI_plotBackground.Changed || (uint)plotBackground != UI_plotBackground.v) plotBackground = (PlotBackground)UI_plotBackground.v;
    if (UI_multiCams.Changed || multiCams != UI_multiCams.v) multiCams = UI_multiCams.v;
    if (UI_displayLegend.Changed || displayLegend != UI_displayLegend.v) displayLegend = UI_displayLegend.v;
    if (UI_displayLegendPalette.Changed || displayLegendPalette != UI_displayLegendPalette.v) displayLegendPalette = UI_displayLegendPalette.v;
    if (UI_paletteType.Changed || (uint)paletteType != UI_paletteType.v) paletteType = (PaletteType)UI_paletteType.v;
    if (UI_legendRange.Changed || any(legendRange != UI_legendRange.v)) legendRange = UI_legendRange.v;
    if (UI_legendTitle.Changed || legendTitle != UI_legendTitle.v) { data.legendTitle = UI_legendTitle.v; ValuesChanged = gChanged = true; }
    if (UI_legendAxisTitle.Changed || legendAxisTitle != UI_legendAxisTitle.v) { data.legendAxisTitle = UI_legendAxisTitle.v; ValuesChanged = gChanged = true; }
    if (UI_legendFormat.Changed || legendFormat != UI_legendFormat.v) { data.legendFormat = UI_legendFormat.v; ValuesChanged = gChanged = true; }
    if (UI_legendSphereTitles.Changed || legendSphereTitles != UI_legendSphereTitles.v) { data.legendSphereTitles = UI_legendSphereTitles.v; ValuesChanged = gChanged = true; }
    if (UI_legendViewWidthPixels.Changed || legendViewWidthPixels != UI_legendViewWidthPixels.v) legendViewWidthPixels = UI_legendViewWidthPixels.v;
    if (UI_displayLegendBackground.Changed || displayLegendBackground != UI_displayLegendBackground.v) displayLegendBackground = UI_displayLegendBackground.v;
    if (GetKeyDown(CtrlAlt, 'c')) centerView();
    if (GetKeyDown(CtrlAlt, 'n')) northView();
    if (GetKeyDown(CtrlAlt, 's')) southView();
    if (GetKeyDown(CtrlAlt, 'e')) eastView();
    if (GetKeyDown(CtrlAlt, 'w')) westView();
    if (GetKeyDown(CtrlAlt, 'd')) downView();
    if (GetKeyDown(CtrlAlt, 'o')) orthoView();
    if (ValuesChanged) { gChanged = true; OnValueChanged(); ValuesChanged = UI_group_Cam.Changed = UI_group_CamTransform.Changed = UI_center.Changed = UI_Default_Center.Changed = UI_dist.Changed = UI_tiltSpin.Changed = UI_projection.Changed = UI_group_CamRanges.Changed = UI_tiltRange.Changed = UI_distSpeed.Changed = UI_rotationSpeed.Changed = UI_distanceRange.Changed = UI_checkCollisions.Changed = UI_orthoTiltSpin.Changed = UI_orthoSize.Changed = UI_group_CamShow.Changed = UI_plotBackground.Changed = UI_multiCams.Changed = UI_group_CamLegend.Changed = UI_displayLegend.Changed = UI_displayLegendPalette.Changed = UI_paletteType.Changed = UI_legendRange.Changed = UI_legendTitle.Changed = UI_legendAxisTitle.Changed = UI_legendFormat.Changed = UI_legendSphereTitles.Changed = UI_legendViewWidthPixels.Changed = UI_displayLegendBackground.Changed = UI_group_CamActions.Changed = false; }
    BDraw_LateUpdate1_GS();
    LateUpdate1_GS();
    if(lateUpdateI++ == uint_max) lateUpdateI = 100;
  }
  public virtual void LateUpdate0_GS()
  {
    BDraw_LateUpdate0_GS();
  }
  public virtual void LateUpdate1_GS()
  {
    BDraw_LateUpdate1_GS();
  }
  public override void Update()
  {
    base.Update();
    if (!ui_loaded) return;
    Update0_GS();
    BDraw_Update1_GS();
    Update1_GS();
  }
  public virtual void Update0_GS()
  {
    BDraw_Update0_GS();
  }
  public virtual void Update1_GS()
  {
    BDraw_Update1_GS();
  }
  public override void OnValueChanged()
  {
    if (!ui_loaded) return;
    OnValueChanged_GS();
    UI_Update_Ortho_Size.DisplayIf(Show_Update_Ortho_Size && UI_Update_Ortho_Size.treeGroup_parent.isExpanded);
    if (UI_dist.Changed) { Update_Ortho_Size(); }
    if (UI_displayLegendPalette.Changed) { buildText = true; }
    if (UI_paletteType.Changed) { _PaletteTex = LoadPalette(UI_paletteType.textString, ref paletteBuffer); }
    if (UI_legendRange.Changed) { buildText = true; }
    if (UI_legendTitle.Changed) { buildText = true; }
    if (UI_legendAxisTitle.Changed) { buildText = true; }
    if (UI_legendFormat.Changed) { buildText = true; }
    if (UI_legendSphereTitles.Changed) { buildText = true; }
    UI_legendViewWidthPixels.DisplayIf(Show_legendViewWidthPixels && UI_legendViewWidthPixels.treeGroup_parent.isExpanded);
    if (UI_displayLegendBackground.Changed) { buildText = true; }
    UI_displayLegendBackground.DisplayIf(Show_displayLegendBackground && UI_displayLegendBackground.treeGroup_parent.isExpanded);
  }
  public override void OnValueChanged_GS()
  {
    BDraw_OnValueChanged_GS();
    var type = "gsReport_Lib".ToType();
    if (type != null) ((GS)GetComponent(type))?.OnValueChanged_GS();

  }
  public string[] Scenes_in_Build => new string[] {  };
  public virtual bool Show_Update_Ortho_Size { get => false; }
  public virtual bool Show_legendViewWidthPixels { get => displayLegend; }
  public virtual bool Show_displayLegendBackground { get => displayLegend; }
  public virtual uint BDraw_AppendBuff_IndexN { get => g.BDraw_AppendBuff_IndexN; set { if (g.BDraw_AppendBuff_IndexN != value) { g.BDraw_AppendBuff_IndexN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint BDraw_AppendBuff_BitN { get => g.BDraw_AppendBuff_BitN; set { if (g.BDraw_AppendBuff_BitN != value) { g.BDraw_AppendBuff_BitN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint BDraw_AppendBuff_N { get => g.BDraw_AppendBuff_N; set { if (g.BDraw_AppendBuff_N != value) { g.BDraw_AppendBuff_N = value; ValuesChanged = gChanged = true; } } }
  public virtual uint BDraw_AppendBuff_BitN1 { get => g.BDraw_AppendBuff_BitN1; set { if (g.BDraw_AppendBuff_BitN1 != value) { g.BDraw_AppendBuff_BitN1 = value; ValuesChanged = gChanged = true; } } }
  public virtual uint BDraw_AppendBuff_BitN2 { get => g.BDraw_AppendBuff_BitN2; set { if (g.BDraw_AppendBuff_BitN2 != value) { g.BDraw_AppendBuff_BitN2 = value; ValuesChanged = gChanged = true; } } }
  public virtual bool BDraw_omitText { get => Is(g.BDraw_omitText); set { if (g.BDraw_omitText != Is(value)) { g.BDraw_omitText = Is(value); ValuesChanged = gChanged = true; } } }
  public virtual bool BDraw_includeUnicode { get => Is(g.BDraw_includeUnicode); set { if (g.BDraw_includeUnicode != Is(value)) { g.BDraw_includeUnicode = Is(value); ValuesChanged = gChanged = true; } } }
  public virtual uint BDraw_fontInfoN { get => g.BDraw_fontInfoN; set { if (g.BDraw_fontInfoN != value) { g.BDraw_fontInfoN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint BDraw_textN { get => g.BDraw_textN; set { if (g.BDraw_textN != value) { g.BDraw_textN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint BDraw_textCharN { get => g.BDraw_textCharN; set { if (g.BDraw_textCharN != value) { g.BDraw_textCharN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint BDraw_boxEdgeN { get => g.BDraw_boxEdgeN; set { if (g.BDraw_boxEdgeN != value) { g.BDraw_boxEdgeN = value; ValuesChanged = gChanged = true; } } }
  public virtual float BDraw_fontSize { get => g.BDraw_fontSize; set { if (any(g.BDraw_fontSize != value)) { g.BDraw_fontSize = value; ValuesChanged = gChanged = true; } } }
  public virtual float BDraw_boxThickness { get => g.BDraw_boxThickness; set { if (any(g.BDraw_boxThickness != value)) { g.BDraw_boxThickness = value; ValuesChanged = gChanged = true; } } }
  public virtual float4 BDraw_boxColor { get => g.BDraw_boxColor; set { if (any(g.BDraw_boxColor != value)) { g.BDraw_boxColor = value; ValuesChanged = gChanged = true; } } }
  public virtual float3 center { get => g.center; set { if (any(g.center != value) || any(UI_center.v != value)) { g.center = UI_center.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float3 Default_Center { get => g.Default_Center; set { if (any(g.Default_Center != value) || any(UI_Default_Center.v != value)) { g.Default_Center = UI_Default_Center.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float dist { get => g.dist; set { if (any(g.dist != value) || any(UI_dist.v != value)) { g.dist = UI_dist.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float2 tiltSpin { get => g.tiltSpin; set { if (any(g.tiltSpin != value) || any(UI_tiltSpin.v != value)) { g.tiltSpin = UI_tiltSpin.v = value; ValuesChanged = gChanged = true; } } }
  public virtual ProjectionMode projection { get => (ProjectionMode)g.projection; set { if ((ProjectionMode)g.projection != value || (ProjectionMode)UI_projection.v != value) { g.projection = UI_projection.v = (uint)value; ValuesChanged = gChanged = true; } } }
  public virtual float2 tiltRange { get => g.tiltRange; set { if (any(g.tiltRange != value) || any(UI_tiltRange.v != value)) { g.tiltRange = UI_tiltRange.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float distSpeed { get => g.distSpeed; set { if (any(g.distSpeed != value) || any(UI_distSpeed.v != value)) { g.distSpeed = UI_distSpeed.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float2 rotationSpeed { get => g.rotationSpeed; set { if (any(g.rotationSpeed != value) || any(UI_rotationSpeed.v != value)) { g.rotationSpeed = UI_rotationSpeed.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float2 distanceRange { get => g.distanceRange; set { if (any(g.distanceRange != value) || any(UI_distanceRange.v != value)) { g.distanceRange = UI_distanceRange.v = value; ValuesChanged = gChanged = true; } } }
  public virtual bool checkCollisions { get => Is(g.checkCollisions); set { if (g.checkCollisions != Is(value) || UI_checkCollisions.v != value) { g.checkCollisions = Is(UI_checkCollisions.v = value); ValuesChanged = gChanged = true; } } }
  public virtual float2 orthoTiltSpin { get => g.orthoTiltSpin; set { if (any(g.orthoTiltSpin != value) || any(UI_orthoTiltSpin.v != value)) { g.orthoTiltSpin = UI_orthoTiltSpin.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float orthoSize { get => g.orthoSize; set { if (any(g.orthoSize != value) || any(UI_orthoSize.v != value)) { g.orthoSize = UI_orthoSize.v = value; ValuesChanged = gChanged = true; } } }
  public virtual PlotBackground plotBackground { get => (PlotBackground)g.plotBackground; set { if ((PlotBackground)g.plotBackground != value || (PlotBackground)UI_plotBackground.v != value) { g.plotBackground = UI_plotBackground.v = (uint)value; ValuesChanged = gChanged = true; } } }
  public virtual bool multiCams { get => Is(g.multiCams); set { if (g.multiCams != Is(value) || UI_multiCams.v != value) { g.multiCams = Is(UI_multiCams.v = value); ValuesChanged = gChanged = true; } } }
  public virtual bool displayLegend { get => Is(g.displayLegend); set { if (g.displayLegend != Is(value) || UI_displayLegend.v != value) { g.displayLegend = Is(UI_displayLegend.v = value); ValuesChanged = gChanged = true; } } }
  public virtual bool displayLegendPalette { get => Is(g.displayLegendPalette); set { if (g.displayLegendPalette != Is(value) || UI_displayLegendPalette.v != value) { g.displayLegendPalette = Is(UI_displayLegendPalette.v = value); ValuesChanged = gChanged = true; } } }
  public virtual PaletteType paletteType { get => (PaletteType)g.paletteType; set { if ((PaletteType)g.paletteType != value || (PaletteType)UI_paletteType.v != value) { g.paletteType = UI_paletteType.v = (uint)value; ValuesChanged = gChanged = true; } } }
  public virtual float2 legendRange { get => g.legendRange; set { if (any(g.legendRange != value) || any(UI_legendRange.v != value)) { g.legendRange = UI_legendRange.v = value; ValuesChanged = gChanged = true; } } }
  public virtual float legendViewWidth { get => g.legendViewWidth; set { if (any(g.legendViewWidth != value)) { g.legendViewWidth = value; ValuesChanged = gChanged = true; } } }
  public virtual uint legendViewWidthPixels { get => g.legendViewWidthPixels; set { if (g.legendViewWidthPixels != value || UI_legendViewWidthPixels.v != value) { g.legendViewWidthPixels = UI_legendViewWidthPixels.v = value; ValuesChanged = gChanged = true; } } }
  public virtual bool displayLegendBackground { get => Is(g.displayLegendBackground); set { if (g.displayLegendBackground != Is(value) || UI_displayLegendBackground.v != value) { g.displayLegendBackground = Is(UI_displayLegendBackground.v = value); ValuesChanged = gChanged = true; } } }
  public virtual uint legendSphereN { get => g.legendSphereN; set { if (g.legendSphereN != value) { g.legendSphereN = value; ValuesChanged = gChanged = true; } } }
  public virtual uint legendPaletteN { get => g.legendPaletteN; set { if (g.legendPaletteN != value) { g.legendPaletteN = value; ValuesChanged = gChanged = true; } } }
  public virtual bool buildText { get => Is(g.buildText); set { if (g.buildText != Is(value)) { g.buildText = Is(value); ValuesChanged = gChanged = true; } } }
  public bool group_Cam { get => UI_group_Cam?.v ?? false; set { if (UI_group_Cam != null) UI_group_Cam.v = value; } }
  public bool group_CamTransform { get => UI_group_CamTransform?.v ?? false; set { if (UI_group_CamTransform != null) UI_group_CamTransform.v = value; } }
  public bool group_CamRanges { get => UI_group_CamRanges?.v ?? false; set { if (UI_group_CamRanges != null) UI_group_CamRanges.v = value; } }
  public bool group_CamShow { get => UI_group_CamShow?.v ?? false; set { if (UI_group_CamShow != null) UI_group_CamShow.v = value; } }
  public bool group_CamLegend { get => UI_group_CamLegend?.v ?? false; set { if (UI_group_CamLegend != null) UI_group_CamLegend.v = value; } }
  public bool group_CamActions { get => UI_group_CamActions?.v ?? false; set { if (UI_group_CamActions != null) UI_group_CamActions.v = value; } }
  public UI_TreeGroup UI_group_Cam, UI_group_CamTransform, UI_group_CamRanges, UI_group_CamShow, UI_group_CamLegend, UI_group_CamActions;
  public UI_float3 UI_center, UI_Default_Center;
  public UI_method UI_Update_Ortho_Size;
  public virtual void Update_Ortho_Size() { }
  public UI_float UI_dist, UI_distSpeed, UI_orthoSize;
  public UI_float2 UI_tiltSpin, UI_tiltRange, UI_rotationSpeed, UI_distanceRange, UI_orthoTiltSpin, UI_legendRange;
  public UI_enum UI_projection, UI_plotBackground, UI_paletteType;
  public UI_bool UI_checkCollisions, UI_multiCams, UI_displayLegend, UI_displayLegendPalette, UI_displayLegendBackground;
  public UI_string UI_legendTitle, UI_legendAxisTitle, UI_legendFormat, UI_legendSphereTitles;
  public string legendTitle { get => UI_legendTitle?.v ?? ""; set { if (UI_legendTitle != null && data != null) UI_legendTitle.v = data.legendTitle = value; } }
  public string legendAxisTitle { get => UI_legendAxisTitle?.v ?? ""; set { if (UI_legendAxisTitle != null && data != null) UI_legendAxisTitle.v = data.legendAxisTitle = value; } }
  public string legendFormat { get => UI_legendFormat?.v ?? ""; set { if (UI_legendFormat != null && data != null) UI_legendFormat.v = data.legendFormat = value; } }
  public string legendSphereTitles { get => UI_legendSphereTitles?.v ?? ""; set { if (UI_legendSphereTitles != null && data != null) UI_legendSphereTitles.v = data.legendSphereTitles = value; } }
  public UI_uint UI_legendViewWidthPixels;
  public UI_method UI_centerView;
  public virtual void centerView() { }
  public UI_method UI_northView;
  public virtual void northView() { }
  public UI_method UI_southView;
  public virtual void southView() { }
  public UI_method UI_eastView;
  public virtual void eastView() { }
  public UI_method UI_westView;
  public virtual void westView() { }
  public UI_method UI_downView;
  public virtual void downView() { }
  public UI_method UI_orthoView;
  public virtual void orthoView() { }
  public UI_TreeGroup ui_group_Cam => UI_group_Cam;
  public UI_TreeGroup ui_group_CamTransform => UI_group_CamTransform;
  public UI_float3 ui_center => UI_center;
  public UI_float3 ui_Default_Center => UI_Default_Center;
  public UI_float ui_dist => UI_dist;
  public UI_float2 ui_tiltSpin => UI_tiltSpin;
  public UI_enum ui_projection => UI_projection;
  public UI_TreeGroup ui_group_CamRanges => UI_group_CamRanges;
  public UI_float2 ui_tiltRange => UI_tiltRange;
  public UI_float ui_distSpeed => UI_distSpeed;
  public UI_float2 ui_rotationSpeed => UI_rotationSpeed;
  public UI_float2 ui_distanceRange => UI_distanceRange;
  public UI_bool ui_checkCollisions => UI_checkCollisions;
  public UI_float2 ui_orthoTiltSpin => UI_orthoTiltSpin;
  public UI_float ui_orthoSize => UI_orthoSize;
  public UI_TreeGroup ui_group_CamShow => UI_group_CamShow;
  public UI_enum ui_plotBackground => UI_plotBackground;
  public UI_bool ui_multiCams => UI_multiCams;
  public UI_TreeGroup ui_group_CamLegend => UI_group_CamLegend;
  public UI_bool ui_displayLegend => UI_displayLegend;
  public UI_bool ui_displayLegendPalette => UI_displayLegendPalette;
  public UI_enum ui_paletteType => UI_paletteType;
  public UI_float2 ui_legendRange => UI_legendRange;
  public UI_string ui_legendTitle => UI_legendTitle;
  public UI_string ui_legendAxisTitle => UI_legendAxisTitle;
  public UI_string ui_legendFormat => UI_legendFormat;
  public UI_string ui_legendSphereTitles => UI_legendSphereTitles;
  public UI_uint ui_legendViewWidthPixels => UI_legendViewWidthPixels;
  public UI_bool ui_displayLegendBackground => UI_displayLegendBackground;
  public UI_TreeGroup ui_group_CamActions => UI_group_CamActions;
  [Serializable]
  public class uiData
  {
    public bool siUnits, group_Cam, group_CamTransform, group_CamRanges, checkCollisions, group_CamShow, multiCams, group_CamLegend, displayLegend, displayLegendPalette, displayLegendBackground, group_CamActions;
    public float3 center, Default_Center;
    public float dist, distSpeed, orthoSize;
    public float2 tiltSpin, tiltRange, rotationSpeed, distanceRange, orthoTiltSpin, legendRange;
    [JsonConverter(typeof(StringEnumConverter))] public ProjectionMode projection;
    [JsonConverter(typeof(StringEnumConverter))] public PlotBackground plotBackground;
    [JsonConverter(typeof(StringEnumConverter))] public PaletteType paletteType;
    public string legendTitle, legendAxisTitle, legendFormat, legendSphereTitles;
    public uint legendViewWidthPixels;
  }
  public virtual void InitBuffers()
  {
    InitBuffers0_GS();
    AddComputeBuffer(ref gOCam_Lib, nameof(gOCam_Lib), 1);
    InitKernels();
    SetKernelValues(gOCam_Lib, nameof(gOCam_Lib), kernel_BDraw_AppendBuff_GetIndexes, kernel_BDraw_AppendBuff_IncSums, kernel_BDraw_AppendBuff_IncFills1, kernel_BDraw_AppendBuff_GetFills2, kernel_BDraw_AppendBuff_GetFills1, kernel_BDraw_AppendBuff_Get_Bits_Sums, kernel_BDraw_AppendBuff_GetSums, kernel_BDraw_AppendBuff_Get_Bits, kernel_BDraw_setDefaultTextInfo, kernel_BDraw_getTextInfo);
    AddComputeBuffer(ref BDraw_tab_delimeted_text, nameof(BDraw_tab_delimeted_text), BDraw_textN);
    AddComputeBuffer(ref BDraw_textInfos, nameof(BDraw_textInfos), BDraw_textN);
    AddComputeBuffer(ref BDraw_fontInfos, nameof(BDraw_fontInfos), BDraw_fontInfoN);
    AddComputeBuffer(ref paletteBuffer, nameof(paletteBuffer), 256);
    InitBuffers1_GS();
    isInitBuffers = true;
  }
  public virtual void InitBuffers0_GS()
  {
    BDraw_InitBuffers0_GS();
  }
  public virtual void InitBuffers1_GS()
  {
    BDraw_InitBuffers1_GS();
  }
  [HideInInspector] public uint[] BDraw_AppendBuff_grp = new uint[1024];
  [HideInInspector] public uint[] BDraw_AppendBuff_grp0 = new uint[1024];
  [Serializable]
  public struct GOCam_Lib
  {
    public uint BDraw_AppendBuff_IndexN, BDraw_AppendBuff_BitN, BDraw_AppendBuff_N, BDraw_AppendBuff_BitN1, BDraw_AppendBuff_BitN2, BDraw_omitText, BDraw_includeUnicode, BDraw_fontInfoN, BDraw_textN, BDraw_textCharN, BDraw_boxEdgeN, projection, checkCollisions, plotBackground, multiCams, displayLegend, displayLegendPalette, paletteType, legendViewWidthPixels, displayLegendBackground, legendSphereN, legendPaletteN, buildText;
    public float BDraw_fontSize, BDraw_boxThickness, dist, distSpeed, orthoSize, legendViewWidth;
    public float4 BDraw_boxColor;
    public float3 center, Default_Center;
    public float2 tiltSpin, tiltRange, rotationSpeed, distanceRange, orthoTiltSpin, legendRange;
  };
  public RWStructuredBuffer<GOCam_Lib> gOCam_Lib;
  public struct BDraw_FontInfo { public float2 uvBottomLeft, uvBottomRight, uvTopLeft, uvTopRight; public int advance, bearing, minX, minY, maxX, maxY; };
  public struct BDraw_TextInfo { public float3 p, right, up, p0, p1; public float2 size, uvSize; public float4 color, backColor; public uint justification, textI, quadType, axis; public float height; };
  public RWStructuredBuffer<uint> BDraw_tab_delimeted_text, BDraw_AppendBuff_Bits, BDraw_AppendBuff_Sums, BDraw_AppendBuff_Indexes, BDraw_AppendBuff_Fills1, BDraw_AppendBuff_Fills2;
  public RWStructuredBuffer<BDraw_TextInfo> BDraw_textInfos;
  public RWStructuredBuffer<BDraw_FontInfo> BDraw_fontInfos;
  public RWStructuredBuffer<Color32> paletteBuffer;
  public RWStructuredBuffer<float4> legendSphereColors;
  public virtual void AllocData_BDraw_tab_delimeted_text(uint n) => AddComputeBuffer(ref BDraw_tab_delimeted_text, nameof(BDraw_tab_delimeted_text), n);
  public virtual void AssignData_BDraw_tab_delimeted_text(params uint[] data) => AddComputeBufferData(ref BDraw_tab_delimeted_text, nameof(BDraw_tab_delimeted_text), data);
  public virtual void AllocData_BDraw_textInfos(uint n) => AddComputeBuffer(ref BDraw_textInfos, nameof(BDraw_textInfos), n);
  public virtual void AssignData_BDraw_textInfos(params BDraw_TextInfo[] data) => AddComputeBufferData(ref BDraw_textInfos, nameof(BDraw_textInfos), data);
  public virtual void AllocData_BDraw_fontInfos(uint n) => AddComputeBuffer(ref BDraw_fontInfos, nameof(BDraw_fontInfos), n);
  public virtual void AssignData_BDraw_fontInfos(params BDraw_FontInfo[] data) => AddComputeBufferData(ref BDraw_fontInfos, nameof(BDraw_fontInfos), data);
  public virtual void AllocData_paletteBuffer(uint n) => AddComputeBuffer(ref paletteBuffer, nameof(paletteBuffer), n);
  public virtual void AssignData_paletteBuffer(params Color32[] data) => AddComputeBufferData(ref paletteBuffer, nameof(paletteBuffer), data);
  public virtual void AllocData_BDraw_AppendBuff_Bits(uint n) => AddComputeBuffer(ref BDraw_AppendBuff_Bits, nameof(BDraw_AppendBuff_Bits), n);
  public virtual void AssignData_BDraw_AppendBuff_Bits(params uint[] data) => AddComputeBufferData(ref BDraw_AppendBuff_Bits, nameof(BDraw_AppendBuff_Bits), data);
  public virtual void AllocData_BDraw_AppendBuff_Sums(uint n) => AddComputeBuffer(ref BDraw_AppendBuff_Sums, nameof(BDraw_AppendBuff_Sums), n);
  public virtual void AssignData_BDraw_AppendBuff_Sums(params uint[] data) => AddComputeBufferData(ref BDraw_AppendBuff_Sums, nameof(BDraw_AppendBuff_Sums), data);
  public virtual void AllocData_BDraw_AppendBuff_Indexes(uint n) => AddComputeBuffer(ref BDraw_AppendBuff_Indexes, nameof(BDraw_AppendBuff_Indexes), n);
  public virtual void AssignData_BDraw_AppendBuff_Indexes(params uint[] data) => AddComputeBufferData(ref BDraw_AppendBuff_Indexes, nameof(BDraw_AppendBuff_Indexes), data);
  public virtual void AllocData_BDraw_AppendBuff_Fills1(uint n) => AddComputeBuffer(ref BDraw_AppendBuff_Fills1, nameof(BDraw_AppendBuff_Fills1), n);
  public virtual void AssignData_BDraw_AppendBuff_Fills1(params uint[] data) => AddComputeBufferData(ref BDraw_AppendBuff_Fills1, nameof(BDraw_AppendBuff_Fills1), data);
  public virtual void AllocData_BDraw_AppendBuff_Fills2(uint n) => AddComputeBuffer(ref BDraw_AppendBuff_Fills2, nameof(BDraw_AppendBuff_Fills2), n);
  public virtual void AssignData_BDraw_AppendBuff_Fills2(params uint[] data) => AddComputeBufferData(ref BDraw_AppendBuff_Fills2, nameof(BDraw_AppendBuff_Fills2), data);
  public virtual void AllocData_legendSphereColors(uint n) => AddComputeBuffer(ref legendSphereColors, nameof(legendSphereColors), n);
  public virtual void AssignData_legendSphereColors(params float4[] data) => AddComputeBufferData(ref legendSphereColors, nameof(legendSphereColors), data);
  public Texture2D _PaletteTex;
  public float4 palette(float v) => paletteColor(_PaletteTex, v);
  public float4 palette(float v, float w) => float4(palette(v).xyz, w);

  public Texture2D BDraw_fontTexture;
  public virtual void onRenderObject_LIN(bool show, uint _itemN, ref uint i, ref uint index, ref uint3 LIN) { uint n = 0; if (show) { if (i < (n = _itemN)) LIN = uint3(index, i, 0); LIN.z += n; i -= n; } index++; }
  public virtual void onRenderObject_LIN(uint _itemN, ref uint i, ref uint index, ref uint3 LIN) { onRenderObject_LIN(true, _itemN, ref i, ref index, ref LIN); }
  public virtual uint3 onRenderObject_LIN(uint i) { uint3 LIN = u000; uint index = 0; onRenderObject_LIN(BDraw_textN, ref i, ref index, ref LIN); onRenderObject_LIN(BDraw_boxEdgeN, ref i, ref index, ref LIN); onRenderObject_LIN(legendPaletteN + legendSphereN, ref i, ref index, ref LIN); return LIN; }
  public virtual uint3 onRenderObject_LIN_Quads(uint i) { uint3 LIN = u000; uint index = 0; onRenderObject_LIN(BDraw_textN, ref i, ref index, ref LIN); onRenderObject_LIN(BDraw_boxEdgeN, ref i, ref index, ref LIN); onRenderObject_LIN(legendPaletteN + legendSphereN, ref i, ref index, ref LIN); return LIN; }
  public virtual uint3 onRenderObject_LIN_Points(uint i) { uint3 LIN = u000; return LIN; }
  [HideInInspector] public int kernel_BDraw_AppendBuff_GetIndexes; [numthreads(numthreads1, 1, 1)] protected void BDraw_AppendBuff_GetIndexes(uint3 id) { unchecked { if (id.x < BDraw_AppendBuff_BitN) BDraw_AppendBuff_GetIndexes_GS(id); } }
  [HideInInspector] public int kernel_BDraw_AppendBuff_IncSums; [numthreads(numthreads1, 1, 1)] protected void BDraw_AppendBuff_IncSums(uint3 id) { unchecked { if (id.x < BDraw_AppendBuff_BitN) BDraw_AppendBuff_IncSums_GS(id); } }
  [HideInInspector] public int kernel_BDraw_AppendBuff_IncFills1; [numthreads(numthreads1, 1, 1)] protected void BDraw_AppendBuff_IncFills1(uint3 id) { unchecked { if (id.x < BDraw_AppendBuff_BitN1) BDraw_AppendBuff_IncFills1_GS(id); } }
  [HideInInspector] public int kernel_BDraw_AppendBuff_GetFills2; [numthreads(numthreads1, 1, 1)] protected IEnumerator BDraw_AppendBuff_GetFills2(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < BDraw_AppendBuff_BitN2) yield return StartCoroutine(BDraw_AppendBuff_GetFills2_GS(grp_tid, grp_id, id, grpI)); } }
  [HideInInspector] public int kernel_BDraw_AppendBuff_GetFills1; [numthreads(numthreads1, 1, 1)] protected IEnumerator BDraw_AppendBuff_GetFills1(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < BDraw_AppendBuff_BitN1) yield return StartCoroutine(BDraw_AppendBuff_GetFills1_GS(grp_tid, grp_id, id, grpI)); } }
  [HideInInspector] public int kernel_BDraw_AppendBuff_Get_Bits_Sums; [numthreads(numthreads1, 1, 1)] protected IEnumerator BDraw_AppendBuff_Get_Bits_Sums(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < BDraw_AppendBuff_BitN) yield return StartCoroutine(BDraw_AppendBuff_Get_Bits_Sums_GS(grp_tid, grp_id, id, grpI)); } }
  [HideInInspector] public int kernel_BDraw_AppendBuff_GetSums; [numthreads(numthreads1, 1, 1)] protected IEnumerator BDraw_AppendBuff_GetSums(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI) { unchecked { if (id.x < BDraw_AppendBuff_BitN) yield return StartCoroutine(BDraw_AppendBuff_GetSums_GS(grp_tid, grp_id, id, grpI)); } }
  [HideInInspector] public int kernel_BDraw_AppendBuff_Get_Bits; [numthreads(numthreads1, 1, 1)] protected void BDraw_AppendBuff_Get_Bits(uint3 id) { unchecked { if (id.x < BDraw_AppendBuff_BitN) BDraw_AppendBuff_Get_Bits_GS(id); } }
  [HideInInspector] public int kernel_BDraw_setDefaultTextInfo; [numthreads(numthreads1, 1, 1)] protected void BDraw_setDefaultTextInfo(uint3 id) { unchecked { if (id.x < BDraw_textN) BDraw_setDefaultTextInfo_GS(id); } }
  [HideInInspector] public int kernel_BDraw_getTextInfo; [numthreads(numthreads1, 1, 1)] protected void BDraw_getTextInfo(uint3 id) { unchecked { if (id.x < BDraw_textN) BDraw_getTextInfo_GS(id); } }
  public virtual v2f vert_GS(uint i, uint j, v2f o)
  {
    uint3 LIN = onRenderObject_LIN(i); int index = -1, level = ((int)LIN.x); i = LIN.y;
    if (level == ++index) { o = vert_BDraw_Text(i, j, o); o.tj.x = 0; }
    else if (level == ++index) { o = vert_BDraw_Box(i, j, o); o.tj.x = 0; }
    else if (level == ++index) { o = vert_Legend(i, j, o); o.tj.x = 0; }
    return o;
  }
  public v2f vert(uint i, uint j)
  {
    v2f o = default;
    o.pos = o.color = o.tj = o.tk = f0000; o.normal = o.p0 = o.p1 = o.wPos = f000; o.uv = f00; o.ti = float4(i, j, 0, 0);
    return vert_GS(i, j, o);
  }
  public virtual v2f vert_Legend(uint i, uint j, v2f o) => o;
  public virtual bool onRenderObject_GS_N(bool cpu)
  {
    RenderQuads(material, onRenderObject_LIN_Quads(0).z);
    RenderPoints(material, onRenderObject_LIN_Points(0).z);
    return true;
  }
  public override bool onRenderObject()
  {
    if (gOCam_Lib == null) return false;
    bool render = true, cpu = false;
    onRenderObject_GS(ref render, ref cpu);
    g_SetData();
    if (!render) return false;
    if (cpu) Cpu(material, new { gOCam_Lib }, new { BDraw_tab_delimeted_text }, new { BDraw_textInfos }, new { BDraw_fontInfos }, new { paletteBuffer }, new { BDraw_AppendBuff_Bits }, new { BDraw_AppendBuff_Sums }, new { BDraw_AppendBuff_Indexes }, new { BDraw_AppendBuff_grp }, new { BDraw_AppendBuff_grp0 }, new { BDraw_AppendBuff_Fills1 }, new { BDraw_AppendBuff_Fills2 }, new { legendSphereColors }, new { BDraw_fontTexture }, new { _PaletteTex });
    else Gpu(material, new { gOCam_Lib }, new { BDraw_tab_delimeted_text }, new { BDraw_textInfos }, new { BDraw_fontInfos }, new { paletteBuffer }, new { BDraw_AppendBuff_Bits }, new { BDraw_AppendBuff_Sums }, new { BDraw_AppendBuff_Indexes }, new { BDraw_AppendBuff_grp }, new { BDraw_AppendBuff_grp0 }, new { BDraw_AppendBuff_Fills1 }, new { BDraw_AppendBuff_Fills2 }, new { legendSphereColors }, new { BDraw_fontTexture }, new { _PaletteTex });
    return onRenderObject_GS_N(cpu);
  }
  public virtual void onRenderObject_GS(ref bool render, ref bool cpu)
  {
    BDraw_onRenderObject_GS(ref render, ref cpu);
  }
  public virtual float4 frag_GS(v2f i, float4 color)
  {
    uint libI = roundu(i.tj.x);
    return color;
  }
  public virtual float4 frag(v2f i)
  {
    float4 color = i.color;
    color = frag_GS(i, color);
    if (color.a == 0) Discard(0);
    return color;
  }
  #region <BDraw>
  public float BDraw_wrapJ(uint j, uint n) => ((j + n) % 6) / 3;
  public uint2 BDraw_JQuadu(uint j) => uint2(j + 2, j + 1) / 3 % 2;
  public float2 BDraw_JQuadf(uint j) => (float2)BDraw_JQuadu(j);
  public float4 BDraw_Number_quadPoint(float rx, float ry, uint j) { float2 p = BDraw_JQuadf(j); return float4((2 * p.x - 1) * rx, (1 - 2 * p.y) * ry, 0, 0); }
  public float4 BDraw_Sphere_quadPoint(float r, uint j) => r * float4(2 * BDraw_JQuadf(j) - 1, 0, 0);
  public float2 BDraw_Line_uv(float3 p0, float3 p1, float r, uint j) { float2 p = BDraw_JQuadf(j); return float2(length(p1 - p0) * (1 - p.y), (1 - 2 * p.x) * r); }
  public float2 BDraw_LineArrow_uv(float dpf, float3 p0, float3 p1, float r, uint j) { float2 p = BDraw_JQuadf(j); return float2((length(p1 - p0) + 2 * r) * (1 - p.y) - r, (1 - 2 * p.x) * r * dpf); }
  public float4 BDraw_LineArrow_p4(float dpf, float3 p0, float3 p1, float3 p3, float r, uint j) { float2 p = BDraw_JQuadf(j); float3 dp = normalize(cross(p1 - p0, p3 - p0)) * r * dpf; return float4(p.y * (p0 - p1) + p1 + dp * (1 - 2 * p.x), 1); }
  public v2f vert_BDraw_Point(float3 p, float4 color, uint i, v2f o) { o.pos = UnityObjectToClipPos(float4(p, 1)); o.ti = float4(i, 0, BDraw_Draw_Point, 0); o.color = color; return o; }
  public v2f vert_BDraw_Sphere(float3 p, float r, float4 color, uint i, uint j, v2f o) { float4 p4 = float4(p, 1), quadPoint = BDraw_Sphere_quadPoint(r, j); o.pos = mul(UNITY_MATRIX_P, mul(UNITY_MATRIX_V, p4) + quadPoint); o.wPos = p; o.uv = quadPoint.xy / r; o.normal = -f001; o.color = color; o.ti = float4(i, 0, BDraw_Draw_Sphere, 0); return o; }
  public v2f vert_BDraw_LineArrow(float dpf, float3 p0, float3 p1, float r, float4 color, uint i, uint j, v2f o) { o.p0 = p0; o.p1 = p1; o.uv = BDraw_LineArrow_uv(dpf, p0, p1, r, j); o.pos = UnityObjectToClipPos(BDraw_LineArrow_p4(dpf, p0, p1, _WorldSpaceCameraPos, r, j)); o.color = color; o.ti = float4(i, 0, dpf == 1 ? BDraw_Draw_Line : BDraw_Draw_Arrow, r); return o; }
  public v2f vert_BDraw_Line(float3 p0, float3 p1, float r, float4 color, uint i, uint j, v2f o) { o.p0 = p0; o.p1 = p1; o.uv = BDraw_Line_uv(p0, p1, r, j); o.pos = UnityObjectToClipPos(BDraw_LineArrow_p4(1, p0, p1, _WorldSpaceCameraPos, r, j)); o.color = color; o.ti = float4(i, 0, BDraw_Draw_Line, r); return o; }
  public v2f vert_BDraw_LineSegment(float3 p0, float3 p1, float r, float4 color, uint i, uint j, v2f o) { o = vert_BDraw_LineArrow(1, p0, p1, r, color, i, j, o); o.ti.z = BDraw_Draw_LineSegment; return o; }
  public v2f vert_BDraw_Arrow(float3 p0, float3 p1, float r, float4 color, uint i, uint j, v2f o) { return vert_BDraw_LineArrow(3, p0, p1, r, color, i, j, o); }
  public v2f vert_BDraw_BoxFrame(float3 c0, float3 c1, float lineRadius, float4 color, uint i, uint j, v2f o) { float3 p0, p1; switch (i) { case 0: p0 = c0; p1 = c0 * f110 + c1 * f001; break; case 1: p0 = c0 * f110 + c1 * f001; p1 = c0 * f100 + c1 * f011; break; case 2: p0 = c0 * f100 + c1 * f011; p1 = c0 * f101 + c1 * f010; break; case 3: p0 = c0 * f101 + c1 * f010; p1 = c0; break; case 4: p0 = c0 * f011 + c1 * f100; p1 = c0 * f010 + c1 * f101; break; case 5: p0 = c0 * f010 + c1 * f101; p1 = c1; break; case 6: p0 = c1; p1 = c0 * f001 + c1 * f110; break; case 7: p0 = c0 * f001 + c1 * f110; p1 = c0 * f011 + c1 * f100; break; case 8: p0 = c0; p1 = c0 * f011 + c1 * f100; break; case 9: p0 = c0 * f101 + c1 * f010; p1 = c0 * f001 + c1 * f110; break; case 10: p0 = c0 * f100 + c1 * f011; p1 = c1; break; default: p0 = c0 * f110 + c1 * f001; p1 = c0 * f010 + c1 * f101; break; } return vert_BDraw_Line(p0, p1, lineRadius, color, i, j, o); }
  public v2f vert_BDraw_Quad(float3 p0, float3 p1, float3 p2, float3 p3, float4 color, uint i, uint j, v2f o) { float3 p = o.wPos = j % 5 == 0 ? p3 : j == 1 ? p2 : j == 4 ? p0 : p1, n = cross(p1 - p0, p0 - p3); o.color = color; o.pos = UnityObjectToClipPos(p); o.uv = float2(BDraw_wrapJ(j, 2), BDraw_wrapJ(j, 4)); o.normal = n; o.ti = float4(i, 0, BDraw_Draw_Texture_2D, 0); return o; }
  public v2f vert_BDraw_Legend(uint i, uint j, v2f o) { float h = 8; float3 c = f110 * 10000, p0 = c + float3(0.4f, -h / 2, 0), p1 = p0 + f100 * 0.4f, p2 = p1 + h * f010, p3 = p0 + h * f010; return vert_BDraw_Quad(p0, p1, p2, p3, WHITE, i, j, o); }
  public v2f vert_BDraw_WebCam(float3 p0, float3 p1, float3 p2, float3 p3, float4 color, uint i, uint j, v2f o) { float3 p = o.wPos = j % 5 == 0 ? p3 : j == 1 ? p2 : j == 4 ? p0 : p1, n = cross(p1 - p0, p0 - p3); o.color = color; o.pos = UnityObjectToClipPos(p); o.uv = float2(BDraw_wrapJ(j, 2), BDraw_wrapJ(j, 4)); o.normal = n; o.ti = float4(i, 0, BDraw_Draw_WebCam, 0); return o; }
  public v2f vert_BDraw_Cube(float3 p, float3 r, float4 color, uint i, uint j, v2f o) { float3 p0, p1, p2, p3; switch (i % 6) { case 0: p0 = f___; p1 = f1__; p2 = f11_; p3 = f_1_; break; case 1: p0 = f1_1; p1 = f__1; p2 = f_11; p3 = f111; break; case 2: p0 = f__1; p1 = f1_1; p2 = f1__; p3 = f___; break; case 3: p0 = f_1_; p1 = f11_; p2 = f111; p3 = f_11; break; case 4: p0 = f__1; p1 = f___; p2 = f_1_; p3 = f_11; break; default: p0 = f1__; p1 = f1_1; p2 = f111; p3 = f11_; break; } return vert_BDraw_Quad(p0 * r + p, p1 * r + p, p2 * r + p, p3 * r + p, color, i, j, o); }
  public v2f vert_BDraw_Cube(float3 p, float r, float4 color, uint i, uint j, v2f o) { return vert_BDraw_Cube(p, f111 * r, color, i, j, o); }
  public v2f vert_BDraw_Signal(float3 p0, float3 p1, float r, uint i, uint j, v2f o) { float2 p = BDraw_JQuadf(j); o.p0 = p0; o.p1 = p1; o.uv = f11 - p.yx; o.pos = UnityObjectToClipPos(BDraw_LineArrow_p4(1, p0, p1, _WorldSpaceCameraPos, r, j)); o.ti = float4(i, 0, BDraw_Draw_Signal, r); return o; }
  public v2f vert_BDraw_Signal(float3 p0, float3 p1, float r, float4 color, int drawType, float thickness, uint i, uint j, v2f o) { float2 p = BDraw_JQuadf(j); o.p0 = p0; o.p1 = p1; o.uv = f11 - p.yx; o.pos = UnityObjectToClipPos(BDraw_LineArrow_p4(1, p0, p1, _WorldSpaceCameraPos, r, j)); o.ti = float4(i, 0, BDraw_Draw_Signal, r); o.tj = float4(distance(p0, p1), r, drawType, thickness); o.color = color; return o; }
  public virtual uint BDraw_SignalSmpN(uint chI) => 1024;
  public virtual float4 BDraw_SignalColor(uint chI, uint smpI) => YELLOW;
  public virtual float4 BDraw_SignalBackColor(uint chI, uint smpI) => float4(1, 1, 1, 0.2f);
  public virtual float BDraw_SignalSmpV(uint chI, uint smpI) => 0;
  public virtual float BDraw_SignalThickness(uint chI, uint smpI) => 0.004f;
  public virtual float BDraw_SignalFillCrest(uint chI, uint smpI) => 1;
  public virtual bool BDraw_SignalMarkerColor(uint stationI, float station_smpI, float4 color, uint chI, float smpI, uint display_x, out float4 return_color)
  {
    float d = abs(smpI - station_smpI + display_x);
    return (return_color = chI == stationI && d < 1 ? float4(color.xyz * (1 - d), 1) : f0000).w > 0;
  }
  public virtual float4 BDraw_SignalMarker(uint chI, float smpI) => f0000;
  public virtual float4 frag_BDraw_Signal(v2f i)
  {
    uint chI = roundu(i.ti.x), SmpN = BDraw_SignalSmpN(chI);
    float2 uv = i.uv, wh = float2(distance(i.p1, i.p0), i.ti.w);
    float smpI = lerp(0, SmpN, uv.x), y = lerp(-1, 1, uv.y), h = wh.y / wh.x * SmpN, thick = BDraw_SignalThickness(chI, (uint)smpI) * SmpN, d = float_PositiveInfinity;
    uint SmpI = (uint)smpI, dSmpI = ceilu(thick) + 1, SmpI0 = (uint)max(0, (int)SmpI - (int)dSmpI), SmpI1 = min(SmpN - 1, SmpI + dSmpI);
    float2 p0 = float2(smpI, y * h), q0 = float2(SmpI0, (h - thick) * BDraw_SignalSmpV(chI, SmpI0)), q1;
    for (uint sI = SmpI0; sI < SmpI1; sI++) { q1 = float2(sI + 1, (h - thick) * BDraw_SignalSmpV(chI, sI + 1)); d = min(d, LineSegDist(q0, q1, p0)); q0 = q1; }
    float4 c = BDraw_SignalColor(chI, SmpI);
    float v = 0.9f * lerp(BDraw_SignalSmpV(chI, SmpI), BDraw_SignalSmpV(chI, SmpI + 1), frac(smpI)), crest = BDraw_SignalFillCrest(chI, SmpI);
    float4 marker = BDraw_SignalMarker(chI, smpI);
    if (marker.w > 0) return marker;
    if (crest >= 0 ? y > crest && y < v : y < crest && y > v) return c;
    if (d < thick) return float4(c.xyz * (1 - d / thick), 1);
    return BDraw_SignalBackColor(chI, SmpI);
  }
  public float4 frag_BDraw_Sphere(v2f i) { float2 uv = i.uv; float r = dot(uv, uv); float4 color = i.color; if (r > 1.0f || color.a == 0) return f0000; float3 n = new float3(uv, r - 1), _LightDir = new float3(0.321f, 0.766f, -0.557f); float lightAmp = max(0.0f, dot(n, _LightDir)); float4 diffuse_Light = (lightAmp + UNITY_LIGHTMODEL_AMBIENT) * color; float spec = max(0, (lightAmp - 0.95f) / 0.05f); color = lerp(diffuse_Light, f1111, spec / 4); color.a = 1; return color; }
  public float4 frag_BDraw_Line(v2f i) { float3 p0 = i.p0, p1 = i.p1; float lineRadius = i.ti.w; float2 uv = i.uv; float r = dot(uv, uv), r2 = lineRadius * lineRadius; float4 color = i.color; float3 p10 = p1 - p0; float lp10 = length(p10); if (uv.x < 0) r /= r2; else if (uv.x > lp10) { uv.x -= lp10; r = dot(uv, uv) / r2; } else { uv.x = 0; r = dot(uv, uv) / r2; } if (r > 1.0f || color.a == 0) return f0000; float3 n = new float3(uv, r - 1), _LightDir = new float3(0.321f, 0.766f, -0.557f); float lightAmp = max(0.0f, dot(n, _LightDir)); float4 diffuse_Light = (lightAmp + UNITY_LIGHTMODEL_AMBIENT) * color; float spec = max(0, (lightAmp - 0.95f) / 0.05f); color = lerp(diffuse_Light, f1111, spec / 4); color.a = 1; return color; }
  public float4 frag_BDraw_Arrow(v2f i) { float3 p0 = i.p0, p1 = i.p1; float lineRadius = i.ti.w; float2 uv = i.uv; float r = dot(uv, uv), r2 = lineRadius * lineRadius; float4 color = i.color; float3 p10 = p1 - p0; float lp10 = length(p10); if (uv.x < 0) r /= r2; else if (uv.x > lp10 - lineRadius * 3 && abs(uv.y) > lineRadius) { uv.x -= lp10; uv = rotate_sc(uv, -sign(uv.y) * 0.5f, 0.866025404f); uv.x = 0; r = dot(uv, uv) / r2; } else if (uv.x > lp10) { uv.x -= lp10; r = dot(uv, uv) / r2; } else { uv.x = 0; r = dot(uv, uv) / r2; } if (r > 1.0f || color.a == 0) return f0000; float3 n = new float3(uv, r - 1), _LightDir = new float3(0.321f, 0.766f, -0.557f); float lightAmp = max(0.0f, dot(n, _LightDir)); float4 diffuse_Light = (lightAmp + UNITY_LIGHTMODEL_AMBIENT) * color; float spec = max(0, (lightAmp - 0.95f) / 0.05f); color = lerp(diffuse_Light, f1111, spec / 4); color.a = 1; return color; }
  public float4 frag_BDraw_LineSegment(v2f i) { float3 p0 = i.p0, p1 = i.p1; float lineRadius = i.ti.w; float2 uv = i.uv; float r = dot(uv, uv), r2 = lineRadius * lineRadius; float4 color = i.color; float3 p10 = p1 - p0; float lp10 = length(p10); uv.x = 0; r = dot(uv, uv) / r2; if (r > 1.0f || color.a == 0) return f0000; float3 n = new float3(uv, r - 1), _LightDir = new float3(0.321f, 0.766f, -0.557f); float lightAmp = max(0.0f, dot(n, _LightDir)); float4 diffuse_Light = (lightAmp + UNITY_LIGHTMODEL_AMBIENT) * color; float spec = max(0, (lightAmp - 0.95f) / 0.05f); color = lerp(diffuse_Light, f1111, spec / 4); color.a = 1; return color; }
  public float4 frag_BDraw_Quad(Texture2D t, v2f i) => i.color * tex2Dlod(t, new float4(i.uv, f00));
  public float4 frag_BDraw_Quad(RWStructuredBuffer<Color> v, v2f i, uint2 _image_size) => (float4)(i.color * v[id_to_i((uint2)(i.uv * (float2)_image_size), _image_size)]);
  public float4 frag_BDraw_Quad(RWStructuredBuffer<Color32> v, v2f i, uint2 _image_size) => (float4)(i.color * c32_f4(v[id_to_i((uint2)(i.uv * (float2)_image_size), _image_size)]));
  public float4 frag_BDraw_Mesh(v2f i) { float3 p = i.wPos; float4 color = i.color; color.xyz += dot(i.normal, _WorldSpaceLightPos0.xyz) / 2; return saturate(color); }
  public virtual bool BDraw_AppendBuff_IsBitOn(uint i) { uint c = BDraw_Byte(i); return c == BDraw_TB || c == BDraw_LF; }
  public class BDraw_TText3D
  {
    public string text; public float3 p, right, up, p0, p1; public float h; public float4 color, backColor;
    public BDraw_Text_QuadType quadType; public BDraw_TextAlignment textAlignment; public uint axis;
  }
  public Font BDraw_font { get; set; }
  public virtual void BDraw_InitBuffers0_GS()
  {
    if (BDraw_omitText) BDraw_fontInfoN = 0;
    else { BDraw_font ??= Resources.Load<Font>("Arial Font/arial Unicode"); BDraw_fontTexture = (Texture2D)BDraw_font.material.mainTexture; BDraw_fontInfoN = BDraw_includeUnicode ? BDraw_font.characterInfo.uLength() : 128 - 32; }
  }
  public virtual void BDraw_InitBuffers1_GS()
  {
    for (int i = 0; i < BDraw_fontInfoN; i++)
    {
      var c = BDraw_font.characterInfo[i];
      if (i == 0) BDraw_fontSize = c.size;
      if (c.index < 128) BDraw_fontInfos[c.index - 32] = new BDraw_FontInfo() { uvBottomLeft = c.uvBottomLeft, uvBottomRight = c.uvBottomRight, uvTopLeft = c.uvTopLeft, uvTopRight = c.uvTopRight, advance = max(c.advance, roundi(c.glyphWidth * 1.05f)), bearing = c.bearing, minX = c.minX, minY = c.minY, maxX = c.maxX, maxY = c.maxY };
    }
    BDraw_fontInfos.SetData();
  }
  public float BDraw_GetTextHeight() { return 0.1f; }
  public uint BDraw_GetText_ch(float v, uint _I, uint neg, uint uN) { return _I < neg ? 13u : _I < uN + neg ? 16u + flooru(10 * frac(abs(v) / pow10(uN - _I + neg))) : _I == uN + neg ? 14u : 16u + flooru(10 * frac(abs(v) * pow10(_I - neg - uN - 1))); }
  public uint BDraw_Byte(uint i) { return TextByte(BDraw_tab_delimeted_text, i); }
  public uint2 BDraw_Get_text_indexes(uint textI) { return uint2(textI == 0 ? 0 : BDraw_AppendBuff_Indexes[textI - 1] + 1, textI < BDraw_AppendBuff_IndexN ? BDraw_AppendBuff_Indexes[textI] : BDraw_textCharN); }
  public float BDraw_GetTextWidth(float v, uint decimalN)
  {
    float textWidth = 0, p10 = pow10(decimalN);
    v = round(v * p10) / p10;
    uint u = flooru(abs(v)), uN = u == 0 ? 1 : flooru(log10(abs(v)) + 1), numDigits = uN + decimalN + (decimalN == 0 ? 0 : 1u), neg = v < 0 ? 1u : 0;
    for (uint _I = 0; _I < numDigits + neg; _I++)
    {
      uint ch = BDraw_GetText_ch(v, _I, neg, uN);
      BDraw_FontInfo f = BDraw_fontInfos[ch];
      float2 mn = new float2(f.minX, f.minY) / BDraw_fontSize, mx = new float2(f.maxX, f.maxY) / BDraw_fontSize, range = mx - mn;
      float dx = f.advance / BDraw_fontSize;
      textWidth += dx;
    }
    return textWidth;
  }
  public float3 BDraw_GetTextWidth(float3 v, uint3 decimalN) => new float3(BDraw_GetTextWidth(v.x, decimalN.x), BDraw_GetTextWidth(v.y, decimalN.y), BDraw_GetTextWidth(v.z, decimalN.z));
  public List<BDraw_TText3D> BDraw_texts = new List<BDraw_TText3D>();
  public void BDraw_ClearTexts() => BDraw_texts.Clear();
  public virtual void BDraw_AddText(string text, float3 p, float3 right, float3 up, float4 color, float4 backColor, float h, BDraw_Text_QuadType quadType, BDraw_TextAlignment textAlignment, float3 p0, float3 p1, uint axis = 0) => BDraw_texts.Add(new BDraw_TText3D() { text = text, p = p, right = right, up = up, color = color, backColor = backColor, h = h, quadType = quadType, textAlignment = textAlignment, p0 = p0, p1 = p1, axis = axis });
  public void BDraw_AddText(string text, float3 p, float3 right, float3 up, float4 color, float4 backColor, float h, BDraw_Text_QuadType quadType, BDraw_TextAlignment textAlignment) => BDraw_AddText(text, p, right, up, color, backColor, h, quadType, textAlignment, f000, f000, 0);
  public virtual BDraw_TextInfo BDraw_textInfo(uint i) { return BDraw_textInfos[i]; }
  public virtual void BDraw_textInfo(uint i, BDraw_TextInfo t) { BDraw_textInfos[i] = t; }
  public int BDraw_ExtraTextN = 0;
  public virtual void BDraw_RebuildExtraTexts() { BDraw_BuildTexts(); BDraw_BuildTexts(); }
  public virtual void BDraw_BuildExtraTexts() { }
  public virtual void BDraw_BuildTexts()
  {
    SetBytes(ref BDraw_tab_delimeted_text, (BDraw_texts.Select(a => a.text).Join("\n") + "\n").ToBytes(Encoding.UTF8));
    AddComputeBuffer(ref BDraw_textInfos, nameof(BDraw_textInfo), BDraw_textN = max(1, BDraw_AppendBuff_Run(BDraw_textCharN = BDraw_tab_delimeted_text.uLength * 4)));
    for (uint i = 0; i < BDraw_texts.Count; i++)
    {
      var t = BDraw_texts[(int)i];
      var ti = BDraw_textInfo(i);
      ti.color = t.color; ti.backColor = t.backColor; ti.p = t.p; ti.p0 = t.p0; ti.p1 = t.p1; ti.height = t.h;
      ti.quadType = (uint)t.quadType; ti.axis = t.axis; ti.right = t.right; ti.up = t.up; ti.justification = (uint)t.textAlignment;
      BDraw_textInfo(i, ti);
    }
    if (BDraw_AppendBuff_Indexes == null || BDraw_AppendBuff_Indexes.computeBuffer == null) { AddComputeBuffer(ref BDraw_AppendBuff_Indexes, nameof(BDraw_AppendBuff_Indexes), 1); BDraw_AppendBuff_Indexes.SetData(new uint[] { 0 }); }
    if (BDraw_fontInfos != null && BDraw_AppendBuff_Indexes != null) { computeShader.SetBuffer(kernel_BDraw_getTextInfo, nameof(BDraw_textInfos), BDraw_textInfos); Gpu_BDraw_getTextInfo(); }
    if (BDraw_ExtraTextN > 0 && BDraw_texts.Count >= BDraw_ExtraTextN) BDraw_texts.RemoveRange(BDraw_texts.Count - BDraw_ExtraTextN, BDraw_ExtraTextN);
    int n = BDraw_texts.Count;
    BDraw_BuildExtraTexts();
    BDraw_ExtraTextN = BDraw_texts.Count - n;
  }
  public virtual IEnumerator BDraw_BuildTexts_Coroutine()
  {
    SetBytes(ref BDraw_tab_delimeted_text, (BDraw_texts.Select(a => a.text).Join("\n") + "\n").ToBytes(Encoding.UTF8));
    AddComputeBuffer(ref BDraw_textInfos, nameof(BDraw_textInfo), BDraw_textN = max(1, BDraw_AppendBuff_Run(BDraw_textCharN = BDraw_tab_delimeted_text.uLength * 4)));
    for (uint i = 0; i < BDraw_texts.Count; i++)
    {
      var t = BDraw_texts[(int)i];
      var ti = BDraw_textInfo(i);
      ti.color = t.color; ti.backColor = t.backColor; ti.p = t.p; ti.p0 = t.p0; ti.p1 = t.p1; ti.height = t.h;
      ti.quadType = (uint)t.quadType; ti.axis = t.axis; ti.right = t.right; ti.up = t.up; ti.justification = (uint)t.textAlignment;
      BDraw_textInfo(i, ti);
      if (i % 1000 == 0) { progress(i, (uint)BDraw_texts.Count); yield return null; }
    }
    progress(0);
    if (BDraw_AppendBuff_Indexes == null || BDraw_AppendBuff_Indexes.computeBuffer == null) { AddComputeBuffer(ref BDraw_AppendBuff_Indexes, nameof(BDraw_AppendBuff_Indexes), 1); BDraw_AppendBuff_Indexes.SetData(new uint[] { 0 }); }
    if (BDraw_fontInfos != null && BDraw_AppendBuff_Indexes != null) { computeShader.SetBuffer(kernel_BDraw_getTextInfo, nameof(BDraw_textInfos), BDraw_textInfos); Gpu_BDraw_getTextInfo(); }
  }
  public virtual void BDraw_BuildTexts_Default()
  {
    SetBytes(ref BDraw_tab_delimeted_text, (BDraw_texts.Select(a => a.text).Join("\n") + "\n").ToBytes(Encoding.UTF8));
    AddComputeBuffer(ref BDraw_textInfos, nameof(BDraw_textInfo), BDraw_textN = max(1, BDraw_AppendBuff_Run(BDraw_textCharN = BDraw_tab_delimeted_text.uLength * 4)));
    if (BDraw_texts.Count > 0)
    {
      var t = BDraw_texts[0];
      var ti = BDraw_textInfo(0);
      ti.color = t.color; ti.backColor = t.backColor; ti.p = t.p; ti.p0 = t.p0; ti.p1 = t.p1; ti.height = t.h;
      ti.quadType = (uint)t.quadType; ti.axis = t.axis; ti.right = t.right; ti.up = t.up; ti.justification = (uint)t.textAlignment;
      BDraw_textInfo(0, ti);
      Gpu_BDraw_setDefaultTextInfo();
    }
    if (BDraw_AppendBuff_Indexes == null || BDraw_AppendBuff_Indexes.computeBuffer == null) { AddComputeBuffer(ref BDraw_AppendBuff_Indexes, nameof(BDraw_AppendBuff_Indexes), 1); BDraw_AppendBuff_Indexes.SetData(new uint[] { 0 }); }
    if (BDraw_fontInfos != null && BDraw_AppendBuff_Indexes != null) Gpu_BDraw_getTextInfo();
  }
  public void BDraw_AddXAxis(string title, float titleHeight, float3 p0, float3 p1, float3 tRight, float3 tUp, float4 color, float2 vRange, string format, float textHeight, float3 right, float3 up, float3 margin, BDraw_Text_QuadType quadType, float3 mn, float3 mx, uint axis = 0)
  {
    if (extent(vRange) <= 0) return;
    uint decimalN = BDraw_GetDecimalN(vRange) + (uint)format.Length, maxTickN = BDraw_GetXAxisN(textHeight, decimalN, p1.xy - p0.xy);
    float range = NiceNum(abs(extent(vRange)), false), dxi = NiceNum(range / (maxTickN - 1), true), xi0 = ceil(vRange.x / dxi) * dxi, xi1 = floor(vRange.y / dxi) * dxi, x0, x1;
    if (vRange.x < vRange.y) { x0 = xi0; x1 = xi1 + dxi / 2; } else { x0 = xi1; x1 = xi0 - dxi / 2; }
    for (float xi = x0; xi < x1; xi += dxi) BDraw_AddText(xi.ToString(format), float3(lerp(p0.x, p1.x, (xi - vRange.x) / extent(vRange)), p0.y, p0.z) + textHeight / 2 * margin, right, up, color, EMPTY, textHeight, quadType, margin.y > 0 ? BDraw_TextAlignment.BottomCenter : BDraw_TextAlignment.TopCenter, mn, mx, axis);
    BDraw_AddText(title, (p0 + p1) / 2 + titleHeight * 2 * margin, tRight, tUp, color, EMPTY, titleHeight, quadType, margin.y > 0 ? BDraw_TextAlignment.BottomCenter : BDraw_TextAlignment.TopCenter, mn, mx, axis);
  }
  public void BDraw_AddYAxis(string title, float titleHeight, float3 p0, float3 p1, float3 tRight, float3 tUp, float4 color, float2 vRange, string format, float textHeight, float3 right, float3 up, float3 margin, BDraw_Text_QuadType quadType, float3 mn, float3 mx, uint axis = 0)
  {
    if (extent(vRange) <= 0) return;
    uint decimalN = BDraw_GetDecimalN(vRange) + (uint)format.Length, maxTickN = BDraw_GetXAxisN(textHeight, decimalN, p1.xy - p0.xy);
    float range = NiceNum(abs(extent(vRange)), false), dxi = NiceNum(range / (maxTickN - 1), true), xi0 = ceil(vRange.x / dxi) * dxi, xi1 = floor(vRange.y / dxi) * dxi, x0, x1;
    if (vRange.x < vRange.y) { x0 = xi0; x1 = xi1 + dxi / 2; } else { x0 = xi1; x1 = xi0 - dxi / 2; }
    for (float xi = x0; xi < x1; xi += dxi) BDraw_AddText(xi.ToString(format), float3(p0.x, lerp(p0.y, p1.y, (xi - vRange.x) / extent(vRange)), p0.z) + textHeight / 2 * margin, right, up, color, EMPTY, textHeight, quadType, tUp.x < 0 ? BDraw_TextAlignment.CenterRight : BDraw_TextAlignment.CenterLeft, mn, mx, axis);
    BDraw_AddText(title, (p0 + p1) / 2 + textHeight * (2 + decimalN / 5.0f) * margin, tRight, tUp, color, EMPTY, titleHeight, quadType, BDraw_TextAlignment.BottomCenter, mn, mx, axis);
  }
  public void BDraw_AddZAxis(string title, float titleHeight, float3 p0, float3 p1, float3 tRight, float3 tUp, float4 color, float2 vRange, string format, float textHeight, float3 right, float3 up, float3 margin, BDraw_Text_QuadType quadType, float3 mn, float3 mx, uint axis = 0)
  {
    if (extent(vRange) <= 0) return;
    uint decimalN = BDraw_GetDecimalN(vRange) + (uint)format.Length, maxTickN = BDraw_GetXAxisN(textHeight, decimalN, p1.zy - p0.zy);
    float range = NiceNum(abs(extent(vRange)), false), dxi = NiceNum(range / (maxTickN - 1), true), xi0 = ceil(vRange.x / dxi) * dxi, xi1 = floor(vRange.y / dxi) * dxi, x0, x1;
    if (vRange.x < vRange.y) { x0 = xi0; x1 = xi1 + dxi / 2; } else { x0 = xi1; x1 = xi0 - dxi / 2; }
    for (float xi = x0; xi < x1; xi += dxi) BDraw_AddText(xi.ToString(format), float3(p0.x, p0.y, lerp(p0.z, p1.z, (xi - vRange.x) / extent(vRange))) + textHeight / 2 * margin, right, up, color, EMPTY, textHeight, quadType, margin.y > 0 ? BDraw_TextAlignment.BottomCenter : BDraw_TextAlignment.TopCenter, mn, mx, axis);
    BDraw_AddText(title, (p0 + p1) / 2 + titleHeight * 2 * margin, tRight, tUp, color, EMPTY, titleHeight, quadType, margin.y > 0 ? BDraw_TextAlignment.BottomCenter : BDraw_TextAlignment.TopCenter, mn, mx, axis);
  }
  public void BDraw_AddAxes(float numberHeight, float titleHeight, float3 p0, float3 p1, float3 range0, float3 range1, float4 color,
    string xTitle, string yTitle, string zTitle, string xFormat = "0.00", string yFormat = null, string zFormat = null)
  {
    if (yFormat == null) yFormat = xFormat; if (zFormat == null) zFormat = yFormat;
    BDraw_AddXAxis(xTitle, titleHeight, float3(p0.x, p0.y, p0.z), float3(p1.x, p0.y, p0.z), f100, f011, color, float2(range0.x, range1.x), xFormat, numberHeight, f100, f011, f0__, BDraw_Text_QuadType.Switch, p0, p1, 100);
    BDraw_AddXAxis(xTitle, titleHeight, float3(p0.x, p0.y, p1.z), float3(p1.x, p0.y, p1.z), f100, f01_, color, float2(range0.x, range1.x), xFormat, numberHeight, f100, f01_, f0_1, BDraw_Text_QuadType.Switch, p0, p1, 200);
    BDraw_AddXAxis(xTitle, titleHeight, float3(p0.x, p1.y, p1.z), float3(p1.x, p1.y, p1.z), f100, f011, color, float2(range0.x, range1.x), xFormat, numberHeight, f100, f011, f011, BDraw_Text_QuadType.Switch, p0, p1, 300);
    BDraw_AddXAxis(xTitle, titleHeight, float3(p0.x, p1.y, p0.z), float3(p1.x, p1.y, p0.z), f100, f01_, color, float2(range0.x, range1.x), xFormat, numberHeight, f100, f01_, f01_, BDraw_Text_QuadType.Switch, p0, p1, 400);
    BDraw_AddYAxis(yTitle, titleHeight, float3(p0.x, p0.y, p0.z), float3(p0.x, p1.y, p0.z), f010, f_0_, color, float2(range0.y, range1.y), yFormat, numberHeight, f101, f010, f_0_, BDraw_Text_QuadType.Switch, p0, p1, 10);
    BDraw_AddYAxis(yTitle, titleHeight, float3(p1.x, p0.y, p0.z), float3(p1.x, p1.y, p0.z), f0_0, f10_, color, float2(range0.y, range1.y), yFormat, numberHeight, f10_, f010, f10_, BDraw_Text_QuadType.Switch, p0, p1, 20);
    BDraw_AddYAxis(yTitle, titleHeight, float3(p1.x, p0.y, p1.z), float3(p1.x, p1.y, p1.z), f0_0, f101, color, float2(range0.y, range1.y), yFormat, numberHeight, f101, f010, f101, BDraw_Text_QuadType.Switch, p0, p1, 30);
    BDraw_AddYAxis(yTitle, titleHeight, float3(p0.x, p0.y, p1.z), float3(p0.x, p1.y, p1.z), f0_0, f_01, color, float2(range0.y, range1.y), yFormat, numberHeight, f10_, f010, f_01, BDraw_Text_QuadType.Switch, p0, p1, 40);
    BDraw_AddZAxis(zTitle, titleHeight, float3(p0.x, p0.y, p0.z), float3(p0.x, p0.y, p1.z), f001, f110, color, float2(range0.z, range1.z), zFormat, numberHeight, f001, f110, f__0, BDraw_Text_QuadType.Switch, p0, p1, 1);
    BDraw_AddZAxis(zTitle, titleHeight, float3(p0.x, p1.y, p0.z), float3(p0.x, p1.y, p1.z), f001, f_10, color, float2(range0.z, range1.z), zFormat, numberHeight, f001, f_10, f_10, BDraw_Text_QuadType.Switch, p0, p1, 2);
    BDraw_AddZAxis(zTitle, titleHeight, float3(p1.x, p1.y, p0.z), float3(p1.x, p1.y, p1.z), f001, f110, color, float2(range0.z, range1.z), zFormat, numberHeight, f001, f110, f110, BDraw_Text_QuadType.Switch, p0, p1, 3);
    BDraw_AddZAxis(zTitle, titleHeight, float3(p1.x, p0.y, p0.z), float3(p1.x, p0.y, p1.z), f001, f_10, color, float2(range0.z, range1.z), zFormat, numberHeight, f001, f_10, f1_0, BDraw_Text_QuadType.Switch, p0, p1, 4);
  }
  public string BDraw_str(string[] s, int i) => i < s.Length ? s[i] : i > 2 && i - 3 < s.Length ? s[i - 3] : "";
  public void BDraw_AddAxes(float numberHeight, float titleHeight, float4 color, float3 p0, float3 p1,
    float3 rangeA0, float3 rangeA1, float3 rangeB0, float3 rangeB1, string xyz_titles, string xyz_formats = "0.00")
  {
    var (ts, fs, ttl, fmt) = (xyz_titles.Split("|"), xyz_formats.Split("|"), new string[6], new string[6]);
    for (int i = 0; i < 6; i++) { ttl[i] = BDraw_str(ts, i); fmt[i] = BDraw_str(fs, i); }
    BDraw_AddAxes(numberHeight, titleHeight, color, p0, p1, rangeA0, rangeA1, rangeB0, rangeB1, ttl, fmt);
  }
  public void BDraw_AddAxes(float numberHeight, float titleHeight, float4 color, float3 p0, float3 p1,
    float3 rangeA0, float3 rangeA1, float3 rangeB0, float3 rangeB1, string[] ttl, string[] fmt)
  {
    BDraw_AddXAxis(ttl[0], titleHeight, float3(p0.x, p0.y, p0.z), float3(p1.x, p0.y, p0.z), f100, f011, color, float2(rangeA0.x, rangeA1.x), fmt[0], numberHeight, f100, f011, f0__, BDraw_Text_QuadType.Switch, p0, p1, 100);
    BDraw_AddXAxis(ttl[0], titleHeight, float3(p0.x, p0.y, p1.z), float3(p1.x, p0.y, p1.z), f100, f01_, color, float2(rangeA0.x, rangeA1.x), fmt[0], numberHeight, f100, f01_, f0_1, BDraw_Text_QuadType.Switch, p0, p1, 200);
    BDraw_AddXAxis(ttl[3], titleHeight, float3(p0.x, p1.y, p1.z), float3(p1.x, p1.y, p1.z), f100, f011, color, float2(rangeB0.x, rangeB1.x), fmt[3], numberHeight, f100, f011, f011, BDraw_Text_QuadType.Switch, p0, p1, 300);
    BDraw_AddXAxis(ttl[3], titleHeight, float3(p0.x, p1.y, p0.z), float3(p1.x, p1.y, p0.z), f100, f01_, color, float2(rangeB0.x, rangeB1.x), fmt[3], numberHeight, f100, f01_, f01_, BDraw_Text_QuadType.Switch, p0, p1, 400);
    BDraw_AddYAxis(ttl[1], titleHeight, float3(p0.x, p0.y, p0.z), float3(p0.x, p1.y, p0.z), f010, f_0_, color, float2(rangeA0.y, rangeA1.y), fmt[1], numberHeight, f101, f010, f_0_, BDraw_Text_QuadType.Switch, p0, p1, 10);
    BDraw_AddYAxis(ttl[4], titleHeight, float3(p1.x, p0.y, p0.z), float3(p1.x, p1.y, p0.z), f0_0, f10_, color, float2(rangeB0.y, rangeB1.y), fmt[4], numberHeight, f10_, f010, f10_, BDraw_Text_QuadType.Switch, p0, p1, 20);
    BDraw_AddYAxis(ttl[1], titleHeight, float3(p1.x, p0.y, p1.z), float3(p1.x, p1.y, p1.z), f0_0, f101, color, float2(rangeA0.y, rangeA1.y), fmt[1], numberHeight, f101, f010, f101, BDraw_Text_QuadType.Switch, p0, p1, 30);
    BDraw_AddYAxis(ttl[4], titleHeight, float3(p0.x, p0.y, p1.z), float3(p0.x, p1.y, p1.z), f0_0, f_01, color, float2(rangeB0.y, rangeB1.y), fmt[4], numberHeight, f10_, f010, f_01, BDraw_Text_QuadType.Switch, p0, p1, 40);
    BDraw_AddZAxis(ttl[2], titleHeight, float3(p0.x, p0.y, p0.z), float3(p0.x, p0.y, p1.z), f001, f110, color, float2(rangeA0.z, rangeA1.z), fmt[2], numberHeight, f001, f110, f__0, BDraw_Text_QuadType.Switch, p0, p1, 1);
    BDraw_AddZAxis(ttl[5], titleHeight, float3(p0.x, p1.y, p0.z), float3(p0.x, p1.y, p1.z), f001, f_10, color, float2(rangeB0.z, rangeB1.z), fmt[5], numberHeight, f001, f_10, f_10, BDraw_Text_QuadType.Switch, p0, p1, 2);
    BDraw_AddZAxis(ttl[5], titleHeight, float3(p1.x, p1.y, p0.z), float3(p1.x, p1.y, p1.z), f001, f110, color, float2(rangeB0.z, rangeB1.z), fmt[5], numberHeight, f001, f110, f110, BDraw_Text_QuadType.Switch, p0, p1, 3);
    BDraw_AddZAxis(ttl[2], titleHeight, float3(p1.x, p0.y, p0.z), float3(p1.x, p0.y, p1.z), f001, f_10, color, float2(rangeA0.z, rangeA1.z), fmt[2], numberHeight, f001, f_10, f1_0, BDraw_Text_QuadType.Switch, p0, p1, 4);
  }
  public void BDraw_AddAxes(float numberHeight, float titleHeight, float3 p0, float3 p1, float3 range0, float3 range1, float4 color, string xyz_titles, string xyz_formats = "0.00")
  {
    var _titles = splitStr3(xyz_titles, false);
    var _formats = splitStr3(xyz_formats, true);
    BDraw_AddAxes(numberHeight, titleHeight, p0, p1, range0, range1, color, _titles[0], _titles[1], _titles[2], _formats[0], _formats[1], _formats[2]);
  }
  public void BDraw_AddAxes(float numberHeight, float titleHeight, float3 p0, float3 p1, float4 color,
    string xTitle, string yTitle, string zTitle, string xFormat = "0.00", string yFormat = null, string zFormat = null, bool zeroOrigin = false)
  {
    float3 range0 = p0, range1 = p1; if (zeroOrigin) { range1 -= range0; range0 = f000; }
    BDraw_AddAxes(numberHeight, titleHeight, p0, p1, range0, range1, color, xTitle, yTitle, zTitle, xFormat, yFormat, zFormat);
  }
  public void BDraw_AddAxes(float numberHeight, float titleHeight, float3 p0, float3 p1, float4 color,
  string xyz_titles, string xyz_formats = "0.00", bool zeroOrigin = false)
  {
    var _titles = splitStr3(xyz_titles, false);
    var _formats = splitStr3(xyz_formats, true);
    float3 range0 = p0, range1 = p1; if (zeroOrigin) { range1 -= range0; range0 = f000; }
    BDraw_AddAxes(numberHeight, titleHeight, p0, p1, range0, range1, color, _titles[0], _titles[1], _titles[2], _formats[0], _formats[1], _formats[2]);
  }
  public virtual void BDraw_AddAxes(float numberHeight, float titleHeight, float4 color,
    string xTitleA, float3 x0A, float3 x1A, float2 xRangeA, string xFormatA,
    string xTitleB, float3 x0B, float3 x1B, float2 xRangeB, string xFormatB,
    string xTitleC, float3 x0C, float3 x1C, float2 xRangeC, string xFormatC,
    string xTitleD, float3 x0D, float3 x1D, float2 xRangeD, string xFormatD,
    string yTitleA, float3 y0A, float3 y1A, float2 yRangeA, string yFormatA,
    string yTitleB, float3 y0B, float3 y1B, float2 yRangeB, string yFormatB,
    string yTitleC, float3 y0C, float3 y1C, float2 yRangeC, string yFormatC,
    string yTitleD, float3 y0D, float3 y1D, float2 yRangeD, string yFormatD,
    string zTitleA, float3 z0A, float3 z1A, float2 zRangeA, string zFormatA,
    string zTitleB, float3 z0B, float3 z1B, float2 zRangeB, string zFormatB,
    string zTitleC, float3 z0C, float3 z1C, float2 zRangeC, string zFormatC,
    string zTitleD, float3 z0D, float3 z1D, float2 zRangeD, string zFormatD)
  {
    BDraw_AddXAxis(xTitleA, titleHeight, x0A, x1A, f100, f011, color, xRangeA, xFormatA, numberHeight, f100, f011, f0__, BDraw_Text_QuadType.Switch, f000, f000);
    BDraw_AddXAxis(xTitleB, titleHeight, x0B, x1B, f100, f011, color, xRangeB, xFormatB, numberHeight, f100, f011, f011, BDraw_Text_QuadType.Switch, f000, f000);
    BDraw_AddXAxis(xTitleC, titleHeight, x0C, x1C, f100, f011, color, xRangeC, xFormatC, numberHeight, f100, f011, f0__, BDraw_Text_QuadType.Switch, f000, f000);
    BDraw_AddXAxis(xTitleD, titleHeight, x0D, x1D, f100, f011, color, xRangeD, xFormatD, numberHeight, f100, f011, f011, BDraw_Text_QuadType.Switch, f000, f000);
    BDraw_AddYAxis(yTitleA, titleHeight, y0A, y1A, f010, f_01, color, yRangeA, yFormatA, numberHeight, f10_, f010, f_01, BDraw_Text_QuadType.Switch, f000, f000);
    BDraw_AddYAxis(yTitleB, titleHeight, y0B, y1B, f0_0, f10_, color, yRangeB, yFormatB, numberHeight, f10_, f010, f10_, BDraw_Text_QuadType.Switch, f000, f000);
    BDraw_AddYAxis(yTitleC, titleHeight, y0C, y1C, f010, f_01, color, yRangeC, yFormatC, numberHeight, f10_, f010, f_01, BDraw_Text_QuadType.Switch, f000, f000);
    BDraw_AddYAxis(yTitleD, titleHeight, y0D, y1D, f0_0, f10_, color, yRangeD, yFormatD, numberHeight, f10_, f010, f10_, BDraw_Text_QuadType.Switch, f000, f000);
    BDraw_AddZAxis(zTitleA, titleHeight, z0A, z1A, f010, f_01, color, zRangeA, zFormatA, numberHeight, f10_, f010, f_01, BDraw_Text_QuadType.Switch, f000, f000);
    BDraw_AddZAxis(zTitleB, titleHeight, z0B, z1B, f0_0, f10_, color, zRangeB, zFormatB, numberHeight, f10_, f010, f10_, BDraw_Text_QuadType.Switch, f000, f000);
    BDraw_AddZAxis(zTitleC, titleHeight, z0C, z1C, f010, f_01, color, zRangeC, zFormatC, numberHeight, f10_, f010, f_01, BDraw_Text_QuadType.Switch, f000, f000);
    BDraw_AddZAxis(zTitleD, titleHeight, z0D, z1D, f0_0, f10_, color, zRangeD, zFormatD, numberHeight, f10_, f010, f10_, BDraw_Text_QuadType.Switch, f000, f000);
  }
  public void BDraw_AddAxes(float numberHeight, float titleHeight, float4 color,
    string xTitleA, float3 x0A, float3 x1A, float2 xRangeA, string xFormatA,
    string xTitleB, float3 x0B, float3 x1B, float2 xRangeB, string xFormatB,
    string yTitleA, float3 y0A, float3 y1A, float2 yRangeA, string yFormatA,
    string yTitleB, float3 y0B, float3 y1B, float2 yRangeB, string yFormatB,
    string zTitleA, float3 z0A, float3 z1A, float2 zRangeA, string zFormatA,
    string zTitleB, float3 z0B, float3 z1B, float2 zRangeB, string zFormatB)
  {
    BDraw_AddXAxis(xTitleA, titleHeight, x0A, x1A, f100, f011, color, xRangeA, xFormatA, numberHeight, f100, f011, f0__, BDraw_Text_QuadType.Switch, f000, f000);
    BDraw_AddXAxis(xTitleB, titleHeight, x0B, x1B, f100, f011, color, xRangeB, xFormatB, numberHeight, f100, f011, f011, BDraw_Text_QuadType.Switch, f000, f000);
    BDraw_AddYAxis(yTitleA, titleHeight, y0A, y1A, f010, f_01, color, yRangeA, yFormatA, numberHeight, f10_, f010, f_01, BDraw_Text_QuadType.Switch, f000, f000);
    BDraw_AddYAxis(yTitleB, titleHeight, y0B, y1B, f0_0, f10_, color, yRangeB, yFormatB, numberHeight, f10_, f010, f10_, BDraw_Text_QuadType.Switch, f000, f000);
    BDraw_AddZAxis(zTitleA, titleHeight, z0A, z1A, f010, f_01, color, zRangeA, zFormatA, numberHeight, f10_, f010, f_01, BDraw_Text_QuadType.Switch, f000, f000);
    BDraw_AddZAxis(zTitleB, titleHeight, z0B, z1B, f0_0, f10_, color, zRangeB, zFormatB, numberHeight, f10_, f010, f10_, BDraw_Text_QuadType.Switch, f000, f000);
  }
  public void BDraw_AddAxes(float numberHeight, float titleHeight, float4 color,
    string xTitleA, float3 x0A, float3 x1A, float2 xRangeA, string xFormatA,
    string xTitleB, float3 x0B, float3 x1B, float2 xRangeB, string xFormatB,
    string yTitleA, float3 y0A, float3 y1A, float2 yRangeA, string yFormatA,
    string yTitleB, float3 y0B, float3 y1B, float2 yRangeB, string yFormatB)
  {
    BDraw_AddXAxis(xTitleA, titleHeight, x0A, x1A, f100, f011, color, xRangeA, xFormatA, numberHeight, f100, f011, f0__, BDraw_Text_QuadType.Switch, f000, f000);
    BDraw_AddXAxis(xTitleB, titleHeight, x0B, x1B, f100, f011, color, xRangeB, xFormatB, numberHeight, f100, f011, f011, BDraw_Text_QuadType.Switch, f000, f000);
    BDraw_AddYAxis(yTitleA, titleHeight, y0A, y1A, f010, f_01, color, yRangeA, yFormatA, numberHeight, f10_, f010, f_01, BDraw_Text_QuadType.Switch, f000, f000);
    BDraw_AddYAxis(yTitleB, titleHeight, y0B, y1B, f0_0, f10_, color, yRangeB, yFormatB, numberHeight, f10_, f010, f10_, BDraw_Text_QuadType.Switch, f000, f000);
  }
  public void BDraw_AddAxes(float numberHeight, float titleHeight, float4 color,
    string xTitleA, float3 x0A, float3 x1A, float2 xRangeA, string xFormatA,
    string yTitleA, float3 y0A, float3 y1A, float2 yRangeA, string yFormatA)
  {
    BDraw_AddAxes(numberHeight, titleHeight, color, xTitleA, x0A, x1A, xRangeA, xFormatA, xTitleA, x0A, x1A, xRangeA, xFormatA,
     yTitleA, y0A, y1A, yRangeA, yFormatA, yTitleA, y0A, y1A, yRangeA, yFormatA);
  }
  public uint BDraw_GetXAxisN(float textHeight, uint decimalN, float2 vRange) { float w = decimalN * textHeight; uint axisN = roundu(abs(extent(vRange)) / w); return clamp(axisN, 2, 25); }
  public uint BDraw_GetYAxisN(float textHeight, float2 vRange) => roundu(abs(extent(vRange)) / textHeight * 0.75f);
  public uint3 BDraw_GetXAxisN(float textHeight, uint3 decimalN, float3 cubeMin, float3 cubeMax)
  {
    float3 w = decimalN * textHeight;
    uint3 axisN = roundu(abs(cubeMax - cubeMin) / w);
    return clamp(axisN, u111 * 2, u111 * 25);
  }
  public uint3 BDraw_GetDecimalN(float3 cubeMin, float3 cubeMax)
  {
    int3 tickN = 25 * i111;
    float3 pRange = cubeMax - cubeMin, range = NiceNum(pRange, false);
    float3 di = NiceNum(range / (tickN - 1), true);
    uint3 decimalN = roundu(di >= 1) * flooru(1 + abs(log10(roundu(di == f000) + di)));
    return max(u111, decimalN);
  }
  public uint BDraw_GetDecimalN(float2 vRange)
  {
    int tickN = 25;
    float pRange = abs(extent(vRange)), range = NiceNum(pRange, false);
    float di = NiceNum(range / (tickN - 1), true);
    uint decimalN = roundu(Is(di >= 1)) * flooru(1 + abs(log10(roundu(Is(di == 0)) + di)));
    return max(1, decimalN);
  }
  public void BDraw_AddLegend(string title, float2 vRange, string format)
  {
    float h = 8;
    float3 c = 10000 * f110;
    BDraw_AddYAxis(title, 0.4f, c + float3(0.4f, -h / 2, 0), c + float3(0.4f, h / 2, 0), f010, f_00, BLACK, vRange, format, 0.2f, f100, f010, f_00, BDraw_Text_QuadType.FrontOnly, f000, f000);
  }
  public virtual v2f vert_BDraw_Text(BDraw_TextInfo t, uint i, uint j, v2f o)
  {
    float3 p = t.p, p0 = t.p0, p1 = t.p1, right = t.right, up = t.up;
    float h = t.height;
    float2 uvSize = t.uvSize;
    float4 color = t.color;
    uint quadType = t.quadType, justification = t.justification, axis = t.axis;
    float w = h * uvSize.x;
    float3 jp = f000;
    if (quadType == BDraw_Text_QuadType_Billboard)
    {
      o.wPos = p;
      switch (justification)
      {
        case BDraw_TextAlignment_BottomLeft: jp = w / 2 * right + h / 2 * up; break;
        case BDraw_TextAlignment_CenterLeft: jp = w / 2 * right; break;
        case BDraw_TextAlignment_TopLeft: jp = w / 2 * right - h / 2 * up; break;
        case BDraw_TextAlignment_BottomCenter: jp = h / 2 * up; break;
        case BDraw_TextAlignment_CenterCenter: break;
        case BDraw_TextAlignment_TopCenter: jp = -h / 2 * up; break;
        case BDraw_TextAlignment_BottomRight: jp = -w / 2 * right + h / 2 * up; break;
        case BDraw_TextAlignment_CenterRight: jp = -w / 2 * right; break;
        case BDraw_TextAlignment_TopRight: jp = -w / 2 * right - h / 2 * up; break;
      }
      float4 p4 = new float4(p, 1), billboardQuad = float4((BDraw_wrapJ(j, 2) - 0.5f) * w, (0.5f - BDraw_wrapJ(j, 1)) * h, 0, 0);
      o.pos = mul(UNITY_MATRIX_P, mul(UNITY_MATRIX_V, p4) + billboardQuad + float4(jp, 0));
      o.normal = f00_;
      o.uv = float2(BDraw_wrapJ(j, 2), BDraw_wrapJ(j, 4)) * uvSize;
      o.ti = float4(i, 0, BDraw_Draw_Text3D, i);
      o.color = color;
    }
    else if (quadType == BDraw_Text_QuadType_Arrow)
    {
      o.wPos = p;
      right = normalize(p1 - p0);
      up = f010;
      jp = w / 2 * right;
      float3 q0 = p - jp, q1 = p + jp;
      o = vert_BDraw_Arrow(q0, q1, h * 0.165f, color, i, j, o);
      float4 ti = o.ti; ti.z = BDraw_Draw_Text3D; o.ti = ti;
      if (dot(p - _WorldSpaceCameraPos, cross(right, up)) < 0)
        o.uv = float2(length(q1 - q0) / h * BDraw_wrapJ(j, 1), BDraw_wrapJ(j, 2) - 0.5f);
      else
        o.uv = float2(length(q1 - q0) / h * (1 - BDraw_wrapJ(j, 1)), 0.5f - BDraw_wrapJ(j, 2));
    }
    else if (quadType == BDraw_Text_QuadType_FrontBack || quadType == BDraw_Text_QuadType_Switch || dot(p - _WorldSpaceCameraPos, cross(right, up)) > 0)
    {
      bool draw = true;
      if (_WorldSpaceCameraPos.y > p1.y)
      {
        if ((axis == 400 && _WorldSpaceCameraPos.z < p0.z) || (axis == 300 && _WorldSpaceCameraPos.z > p1.z)) draw = false;
        if ((axis == 3 && _WorldSpaceCameraPos.x > p1.x) || (axis == 2 && _WorldSpaceCameraPos.x < p0.x)) draw = false;
        if (axis == 100 && _WorldSpaceCameraPos.z > p0.z) draw = false;
        if (axis == 200 && _WorldSpaceCameraPos.z < p1.z) draw = false;
        if (axis == 4 && _WorldSpaceCameraPos.x < p1.x) draw = false;
        if (axis == 1 && _WorldSpaceCameraPos.x > p0.x) draw = false;
      }
      else if (_WorldSpaceCameraPos.y < p0.y)
      {
        if ((axis == 100 && _WorldSpaceCameraPos.z < p0.z) || (axis == 200 && _WorldSpaceCameraPos.z > p1.z)) draw = false;
        if ((axis == 4 && _WorldSpaceCameraPos.x > p1.x) || (axis == 1 && _WorldSpaceCameraPos.x < p0.x)) draw = false;
        if (axis == 400 && _WorldSpaceCameraPos.z > p0.z) draw = false;
        if (axis == 300 && _WorldSpaceCameraPos.z < p1.z) draw = false;
        if (axis == 3 && _WorldSpaceCameraPos.x < p1.x) draw = false;
        if (axis == 2 && _WorldSpaceCameraPos.x > p0.x) draw = false;
      }
      else
      {
        if ((axis == 400 && _WorldSpaceCameraPos.z > p0.z) || (axis == 300 && _WorldSpaceCameraPos.z < p1.z)) draw = false;
        if ((axis == 3 && _WorldSpaceCameraPos.x < p1.x) || (axis == 2 && _WorldSpaceCameraPos.x > p0.x)) draw = false;
        if ((axis == 100 && _WorldSpaceCameraPos.z > p0.z) || (axis == 200 && _WorldSpaceCameraPos.z < p1.z)) draw = false;
        if ((axis == 4 && _WorldSpaceCameraPos.x < p1.x) || (axis == 1 && _WorldSpaceCameraPos.x > p0.x)) draw = false;
      }
      if (axis == 10 && _WorldSpaceCameraPos.x > p0.x && _WorldSpaceCameraPos.z > p0.z) draw = false;
      else if (axis == 10 && _WorldSpaceCameraPos.x < p0.x && _WorldSpaceCameraPos.z < p0.z) draw = false;
      else if (axis == 20 && _WorldSpaceCameraPos.x > p1.x && _WorldSpaceCameraPos.z < p0.z) draw = false;
      else if (axis == 20 && _WorldSpaceCameraPos.x < p1.x && _WorldSpaceCameraPos.z > p0.z) draw = false;
      else if (axis == 30 && _WorldSpaceCameraPos.x > p1.x && _WorldSpaceCameraPos.z > p1.z) draw = false;
      else if (axis == 30 && _WorldSpaceCameraPos.x < p1.x && _WorldSpaceCameraPos.z < p1.z) draw = false;
      else if (axis == 40 && _WorldSpaceCameraPos.x < p0.x && _WorldSpaceCameraPos.z > p1.z) draw = false;
      else if (axis == 40 && _WorldSpaceCameraPos.x > p0.x && _WorldSpaceCameraPos.z < p1.z) draw = false;
      if (draw)
      {
        o.wPos = p;
        switch (justification)
        {
          case BDraw_TextAlignment_BottomLeft: break;
          case BDraw_TextAlignment_CenterLeft: jp = -h / 2 * up; break;
          case BDraw_TextAlignment_TopLeft: jp = -h * up; break;
          case BDraw_TextAlignment_BottomCenter: jp = -w / 2 * right; break;
          case BDraw_TextAlignment_CenterCenter: jp = -w / 2 * right - h / 2 * up; break;
          case BDraw_TextAlignment_TopCenter: jp = -w / 2 * right - h * up; break;
          case BDraw_TextAlignment_BottomRight: jp = -w * right; break;
          case BDraw_TextAlignment_CenterRight: jp = -w * right - h / 2 * up; break;
          case BDraw_TextAlignment_TopRight: jp = -w * right - h * up; break;
        }
        float3 q0 = p + jp, q1 = q0 + right * w, p2 = q1 + up * h, p3 = q0 + up * h;
        o.pos = UnityObjectToClipPos(o.wPos = j % 5 == 0 ? p3 : j == 1 ? p2 : j == 4 ? q0 : q1);
        o.normal = cross(right, up);
        if (quadType == BDraw_Text_QuadType_Switch && dot(p - _WorldSpaceCameraPos, cross(right, up)) < 0)
          o.uv = float2(1 - BDraw_wrapJ(j, 2), BDraw_wrapJ(j, 4)) * uvSize;
        else
          o.uv = float2(BDraw_wrapJ(j, 2), BDraw_wrapJ(j, 4)) * uvSize;
        o.ti = float4(i, 0, BDraw_Draw_Text3D, i);
        o.color = color;
      }
    }
    return o;
  }
  public virtual v2f vert_BDraw_Text(uint i, uint j, v2f o) => vert_BDraw_Text(BDraw_textInfo(i), i, j, o);
  public virtual void BDraw_getTextInfo_GS(uint3 id)
  {
    uint i = id.x;
    BDraw_TextInfo ti = BDraw_textInfo(i);
    ti.textI = i;
    ti.uvSize = f01;
    uint2 textIs = BDraw_Get_text_indexes(i);
    float2 t = ti.uvSize;
    for (uint j = textIs.x; j < textIs.y; j++) { uint byteI = BDraw_Byte(j); if (byteI >= 32) { byteI -= 32; t.x += BDraw_fontInfos[byteI].advance; } }
    t.x /= g.BDraw_fontSize;
    ti.uvSize = t;
    BDraw_textInfo(i, ti);
  }
  public virtual void BDraw_setDefaultTextInfo_GS(uint3 id)
  {
    uint i = id.x;
    if (i > 0)
    {
      BDraw_TextInfo t = BDraw_textInfo(0), ti = BDraw_textInfo(i);
      ti.color = t.color; ti.backColor = t.backColor; ti.p = t.p; ti.p0 = t.p0; ti.p1 = t.p1; ti.height = t.height;
      ti.quadType = (uint)t.quadType; ti.axis = t.axis; ti.right = t.right; ti.up = t.up; ti.justification = t.justification;
      BDraw_textInfo(i, ti);
    }
  }
  public virtual float3 BDraw_gridMin() { return f000; }
  public virtual float3 BDraw_gridMax() { return f111; }
  public float3 BDraw_gridSize() { return BDraw_gridMax() - BDraw_gridMin(); }
  public float3 BDraw_gridCenter() { return (BDraw_gridMax() + BDraw_gridMin()) / 2; }
  public virtual v2f vert_BDraw_Box(uint i, uint j, v2f o) { return vert_BDraw_BoxFrame(BDraw_gridMin(), BDraw_gridMax(), BDraw_boxThickness, BDraw_boxColor, i, j, o); }
  public virtual float4 frag_BDraw_Text(Texture2D t, RWStructuredBuffer<uint> _text, RWStructuredBuffer<BDraw_FontInfo> BDraw_fontInfos, float BDraw_fontSize, uint quadType, float4 backColor, uint2 textIs, v2f i)
  {
    float uv_x = 0;
    float4 color = backColor;
    for (uint textI = textIs.x; textI < textIs.y; textI++)
    {
      uint fontInfoI = TextByte(_text, textI) - 32;
      BDraw_FontInfo f = BDraw_fontInfos[fontInfoI];
      float dx = f.advance / BDraw_fontSize;
      float2 mn = float2(f.minX, f.minY) / BDraw_fontSize, mx = float2(f.maxX, f.maxY) / BDraw_fontSize, range = mx - mn;
      if (quadType == BDraw_Text_QuadType_Arrow) { mn.y -= 0.5f; mx.y -= 0.5f; }
      float2 uv = (i.uv - uv_x * f10 + f1_ * mn - float2(f.bearing / BDraw_fontSize, 0.25f)) / range;
      if (IsNotOutside(uv, f00, f11))
      {
        if (any(f.uvTopLeft > f.uvBottomRight)) uv = uv.yx;
        uv = lerp(f.uvBottomLeft, f.uvTopRight, uv);
        color = tex2Dlod(t, float4(uv, f00));
        color.rgb = (1 - color.rgb) * i.color.rgb;
        if (color.w == 0) color = backColor;
        else color.w = i.color.w;
        break;
      }
      uv_x += dx;
    }
    return color;
  }
  public virtual float4 frag_BDraw_GS(v2f i, float4 color)
  {
    switch (roundu(i.ti.z))
    {
      case uint_max: Discard(0); break;
      case BDraw_Draw_Sphere: color = frag_BDraw_Sphere(i); break;
      case BDraw_Draw_Line: color = frag_BDraw_Line(i); break;
      case BDraw_Draw_Arrow: color = frag_BDraw_Arrow(i); break;
      case BDraw_Draw_Signal: color = frag_BDraw_Signal(i); break;
      case BDraw_Draw_LineSegment: color = frag_BDraw_LineSegment(i); break;
      case BDraw_Draw_Mesh: color = frag_BDraw_Mesh(i); break;
      case BDraw_Draw_Text3D:
        BDraw_TextInfo t = BDraw_textInfo(roundu(i.ti.x));
        color = frag_BDraw_Text(BDraw_fontTexture, BDraw_tab_delimeted_text, BDraw_fontInfos, BDraw_fontSize, t.quadType, t.backColor, BDraw_Get_text_indexes(t.textI), i);
        break;
    }
    return color;
  }

  public virtual void base_BDraw_Start0_GS()
  {
    BDraw_AppendBuff_Start0_GS();
  }
  public virtual void base_BDraw_Start1_GS()
  {
    BDraw_AppendBuff_Start1_GS();
  }
  public virtual void base_BDraw_OnApplicationQuit() { Save_UI(); OnApplicationQuit_GS(); base.OnApplicationQuit(); already_quited = true; }
  public virtual void base_BDraw_LateUpdate0_GS()
  {
    BDraw_AppendBuff_LateUpdate0_GS();
  }
  public virtual void base_BDraw_LateUpdate1_GS()
  {
    BDraw_AppendBuff_LateUpdate1_GS();
  }
  public virtual void base_BDraw_Update0_GS()
  {
    BDraw_AppendBuff_Update0_GS();
  }
  public virtual void base_BDraw_Update1_GS()
  {
    BDraw_AppendBuff_Update1_GS();
  }
  public virtual void base_BDraw_OnValueChanged_GS()
  {
    BDraw_AppendBuff_OnValueChanged_GS();
  }
  public virtual void base_BDraw_InitBuffers0_GS()
  {
    BDraw_AppendBuff_InitBuffers0_GS();
  }
  public virtual void base_BDraw_InitBuffers1_GS()
  {
    BDraw_AppendBuff_InitBuffers1_GS();
  }
  [HideInInspector]
  public virtual void base_BDraw_onRenderObject_GS(ref bool render, ref bool cpu)
  {
    BDraw_AppendBuff_onRenderObject_GS(ref render, ref cpu);
  }
  public virtual float4 base_frag_BDraw_GS(v2f i, float4 color)
  {
    uint libI = roundu(i.tj.x);
    return color;
  }
  public void BDraw_AppendBuff_print_Bits(string title) { StrBldr sb = StrBldr($"{title}, BDraw_AppendBuff_Bits"); for (uint i = 0; i < BDraw_AppendBuff_BitN; i++) sb.Add(" ", BDraw_AppendBuff_Bits[i]); print(sb); }
  public void BDraw_AppendBuff_print_Sums(string title) { StrBldr sb = StrBldr($"{title}, BDraw_AppendBuff_Sums"); for (uint i = 0; i < BDraw_AppendBuff_BitN; i++) sb.Add(" ", BDraw_AppendBuff_Sums[i]); print(sb); }
  public void BDraw_AppendBuff_print_Indexes(string title) { StrBldr sb = StrBldr($"{title}: BDraw_AppendBuff_Indexes"); for (uint i = 0; i < BDraw_AppendBuff_IndexN; i++) sb.Add(" ", BDraw_AppendBuff_Indexes[i]); print(sb); }
  public void BDraw_AppendBuff_SetN(uint n)
  {
    if (n > 2147450880) throw new Exception("gsAppendBuff: BDraw_AppendBuff_N > 2,147,450,880");
    BDraw_AppendBuff_N = n; BDraw_AppendBuff_BitN = ceilu(BDraw_AppendBuff_N, 32); BDraw_AppendBuff_BitN1 = ceilu(BDraw_AppendBuff_BitN, numthreads1); BDraw_AppendBuff_BitN2 = ceilu(BDraw_AppendBuff_BitN1, numthreads1);
    AllocData_BDraw_AppendBuff_Bits(BDraw_AppendBuff_BitN); AllocData_BDraw_AppendBuff_Fills1(BDraw_AppendBuff_BitN1); AllocData_BDraw_AppendBuff_Fills2(BDraw_AppendBuff_BitN2); AllocData_BDraw_AppendBuff_Sums(BDraw_AppendBuff_BitN);
  }
  public void BDraw_AppendBuff_FillPrefixes() { Gpu_BDraw_AppendBuff_GetFills1(); Gpu_BDraw_AppendBuff_GetFills2(); Gpu_BDraw_AppendBuff_IncFills1(); Gpu_BDraw_AppendBuff_IncSums(); }
  public void BDraw_AppendBuff_getIndexes() { AllocData_BDraw_AppendBuff_Indexes(BDraw_AppendBuff_IndexN); Gpu_BDraw_AppendBuff_GetIndexes(); }
  public void BDraw_AppendBuff_FillIndexes() { BDraw_AppendBuff_FillPrefixes(); BDraw_AppendBuff_getIndexes(); }
  public virtual uint BDraw_AppendBuff_Run(uint n) { BDraw_AppendBuff_SetN(n); Gpu_BDraw_AppendBuff_GetSums(); BDraw_AppendBuff_FillIndexes(); return BDraw_AppendBuff_IndexN; }
  public uint BDraw_AppendBuff_Run(int n) => BDraw_AppendBuff_Run((uint)n);
  public uint BDraw_AppendBuff_Run(uint2 n) => BDraw_AppendBuff_Run(cproduct(n)); public uint BDraw_AppendBuff_Run(uint3 n) => BDraw_AppendBuff_Run(cproduct(n));
  public uint BDraw_AppendBuff_Run(int2 n) => BDraw_AppendBuff_Run(cproduct(n)); public uint BDraw_AppendBuff_Run(int3 n) => BDraw_AppendBuff_Run(cproduct(n));
  public virtual void BDraw_AppendBuff_Init_Bits(uint n, GS parent, string _N, string _BitN, ref RWStructuredBuffer<uint> _Bits) { BDraw_AppendBuff_SetN(n); parent.SetValue(_N, BDraw_AppendBuff_N); parent.SetValue(_BitN, BDraw_AppendBuff_BitN); }
  public virtual void BDraw_AppendBuff_Prefix_Sums() { Gpu_BDraw_AppendBuff_Get_Bits_Sums(); BDraw_AppendBuff_FillPrefixes(); }
  public virtual void BDraw_AppendBuff_Bit_Indexes(RWStructuredBuffer<uint> bits, GS _this, string _IndexN, ref RWStructuredBuffer<uint> _Indexes) { BDraw_AppendBuff_Prefix_Sums(); BDraw_AppendBuff_getIndexes(); _this.SetValue(_IndexN, BDraw_AppendBuff_IndexN); }
  public uint BDraw_AppendBuff_Assign_Bits(uint i, uint j, uint bits) => bits | (Is(i < BDraw_AppendBuff_N && BDraw_AppendBuff_IsBitOn(i)) << (int)j);
  public virtual void BDraw_AppendBuff_Get_Bits_GS(uint3 id) { uint i = id.x, j, k, bits = 0; if (i < BDraw_AppendBuff_BitN) { for (j = 0, k = i * 32; j < 32; j++) bits = BDraw_AppendBuff_Assign_Bits(k + j, j, bits); BDraw_AppendBuff_Bits[i] = bits; } }
  public virtual IEnumerator BDraw_AppendBuff_GetSums_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI)
  {
    uint i = id.x, c, s, j, k, bits = 0;
    if (i < BDraw_AppendBuff_BitN) { for (j = 0, k = i * 32; j < 32; j++) bits = BDraw_AppendBuff_Assign_Bits(k + j, j, bits); BDraw_AppendBuff_Bits[i] = bits; c = countbits(bits); } else c = 0;
    BDraw_AppendBuff_grp0[grpI] = c; BDraw_AppendBuff_grp[grpI] = c; yield return GroupMemoryBarrierWithGroupSync();
    for (s = 1; s < numthreads1; s *= 2)
    {
      if (grpI >= s && i < BDraw_AppendBuff_BitN) BDraw_AppendBuff_grp[grpI] = BDraw_AppendBuff_grp0[grpI] + BDraw_AppendBuff_grp0[grpI - s]; yield return GroupMemoryBarrierWithGroupSync();
      BDraw_AppendBuff_grp0[grpI] = BDraw_AppendBuff_grp[grpI]; yield return GroupMemoryBarrierWithGroupSync();
    }
    if (i < BDraw_AppendBuff_BitN) BDraw_AppendBuff_Sums[i] = BDraw_AppendBuff_grp[grpI];
  }
  public virtual IEnumerator BDraw_AppendBuff_Get_Bits_Sums_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI)
  {
    uint i = id.x, c = i < BDraw_AppendBuff_BitN ? countbits(BDraw_AppendBuff_Bits[i]) : 0, s;
    BDraw_AppendBuff_grp0[grpI] = c; BDraw_AppendBuff_grp[grpI] = c; yield return GroupMemoryBarrierWithGroupSync();
    for (s = 1; s < numthreads1; s *= 2)
    {
      if (grpI >= s && i < BDraw_AppendBuff_BitN) BDraw_AppendBuff_grp[grpI] = BDraw_AppendBuff_grp0[grpI] + BDraw_AppendBuff_grp0[grpI - s]; yield return GroupMemoryBarrierWithGroupSync();
      BDraw_AppendBuff_grp0[grpI] = BDraw_AppendBuff_grp[grpI]; yield return GroupMemoryBarrierWithGroupSync();
    }
    if (i < BDraw_AppendBuff_BitN) BDraw_AppendBuff_Sums[i] = BDraw_AppendBuff_grp[grpI];
  }
  public virtual IEnumerator BDraw_AppendBuff_GetFills1_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI)
  {
    uint i = id.x, k = i + 1, j = k * numthreads1 - 1, c = i < BDraw_AppendBuff_BitN1 - 1 ? BDraw_AppendBuff_Sums[j] : i < BDraw_AppendBuff_BitN1 ? BDraw_AppendBuff_Sums[BDraw_AppendBuff_BitN - 1] : 0, s;
    BDraw_AppendBuff_grp0[grpI] = c; BDraw_AppendBuff_grp[grpI] = c; yield return GroupMemoryBarrierWithGroupSync();
    for (s = 1; s < numthreads1; s *= 2)
    {
      if (grpI >= s && i < BDraw_AppendBuff_BitN1) BDraw_AppendBuff_grp[grpI] = BDraw_AppendBuff_grp0[grpI] + BDraw_AppendBuff_grp0[grpI - s]; yield return GroupMemoryBarrierWithGroupSync();
      BDraw_AppendBuff_grp0[grpI] = BDraw_AppendBuff_grp[grpI]; yield return GroupMemoryBarrierWithGroupSync();
    }
    if (i < BDraw_AppendBuff_BitN1) BDraw_AppendBuff_Fills1[i] = BDraw_AppendBuff_grp[grpI];
  }
  public virtual IEnumerator BDraw_AppendBuff_GetFills2_GS(uint3 grp_tid, uint3 grp_id, uint3 id, uint grpI)
  {
    uint i = id.x, k = i + 1, j = k * numthreads1 - 1, c = i < BDraw_AppendBuff_BitN2 - 1 ? BDraw_AppendBuff_Fills1[j] : i < BDraw_AppendBuff_BitN2 ? BDraw_AppendBuff_Fills1[BDraw_AppendBuff_BitN1 - 1] : 0, s;
    BDraw_AppendBuff_grp0[grpI] = c; BDraw_AppendBuff_grp[grpI] = c; yield return GroupMemoryBarrierWithGroupSync();
    for (s = 1; s < numthreads1; s *= 2)
    {
      if (grpI >= s && i < BDraw_AppendBuff_BitN2) BDraw_AppendBuff_grp[grpI] = BDraw_AppendBuff_grp0[grpI] + BDraw_AppendBuff_grp0[grpI - s]; yield return GroupMemoryBarrierWithGroupSync();
      BDraw_AppendBuff_grp0[grpI] = BDraw_AppendBuff_grp[grpI]; yield return GroupMemoryBarrierWithGroupSync();
    }
    if (i < BDraw_AppendBuff_BitN2) BDraw_AppendBuff_Fills2[i] = BDraw_AppendBuff_grp[grpI];
  }
  public virtual void BDraw_AppendBuff_IncFills1_GS(uint3 id) { uint i = id.x; if (i >= numthreads1) BDraw_AppendBuff_Fills1[i] += BDraw_AppendBuff_Fills2[i / numthreads1 - 1]; }
  public virtual void BDraw_AppendBuff_IncSums_GS(uint3 id) { uint i = id.x; if (i >= numthreads1) BDraw_AppendBuff_Sums[i] += BDraw_AppendBuff_Fills1[i / numthreads1 - 1]; if (i == BDraw_AppendBuff_BitN - 1) BDraw_AppendBuff_IndexN = BDraw_AppendBuff_Sums[i]; }
  public virtual void BDraw_AppendBuff_GetIndexes_GS(uint3 id) { uint i = id.x, j, sum = i == 0 ? 0 : BDraw_AppendBuff_Sums[i - 1], b, i32 = i << 5, k; for (k = 0, b = BDraw_AppendBuff_Bits[i]; b > 0; k++) { j = (uint)findLSB(b); BDraw_AppendBuff_Indexes[sum + k] = i32 + j; b = SetBitu(b, j, 0); } }

  public virtual void base_BDraw_AppendBuff_Start0_GS() { }
  public virtual void base_BDraw_AppendBuff_Start1_GS() { }
  public virtual void base_BDraw_AppendBuff_OnApplicationQuit() { Save_UI(); OnApplicationQuit_GS(); base.OnApplicationQuit(); already_quited = true; }
  public virtual void base_BDraw_AppendBuff_LateUpdate0_GS() { }
  public virtual void base_BDraw_AppendBuff_LateUpdate1_GS() { }
  public virtual void base_BDraw_AppendBuff_Update0_GS() { }
  public virtual void base_BDraw_AppendBuff_Update1_GS() { }
  public virtual void base_BDraw_AppendBuff_OnValueChanged_GS() { }
  public virtual void base_BDraw_AppendBuff_InitBuffers0_GS() { }
  public virtual void base_BDraw_AppendBuff_InitBuffers1_GS() { }
  [HideInInspector]
  public virtual void base_BDraw_AppendBuff_onRenderObject_GS(ref bool render, ref bool cpu) { }
  public virtual float4 base_frag_BDraw_AppendBuff_GS(v2f i, float4 color)
  {
    uint libI = roundu(i.tj.x);
    return color;
  }
  public virtual void BDraw_AppendBuff_InitBuffers0_GS() { }
  public virtual void BDraw_AppendBuff_InitBuffers1_GS() { }
  public virtual void BDraw_AppendBuff_LateUpdate0_GS() { }
  public virtual void BDraw_AppendBuff_LateUpdate1_GS() { }
  public virtual void BDraw_AppendBuff_Update0_GS() { }
  public virtual void BDraw_AppendBuff_Update1_GS() { }
  public virtual void BDraw_AppendBuff_Start0_GS() { }
  public virtual void BDraw_AppendBuff_Start1_GS() { }
  public virtual void BDraw_AppendBuff_OnValueChanged_GS() { }
  public virtual void BDraw_AppendBuff_OnApplicationQuit_GS() { }
  public virtual void BDraw_AppendBuff_onRenderObject_GS(ref bool render, ref bool cpu) { }
  public virtual float4 frag_BDraw_AppendBuff_GS(v2f i, float4 color)
  {
    return color;
  }
  public virtual void BDraw_LateUpdate0_GS() { }
  public virtual void BDraw_LateUpdate1_GS() { }
  public virtual void BDraw_Update0_GS() { }
  public virtual void BDraw_Update1_GS() { }
  public virtual void BDraw_Start0_GS() { }
  public virtual void BDraw_Start1_GS() { }
  public virtual void BDraw_OnValueChanged_GS() { }
  public virtual void BDraw_OnApplicationQuit_GS() { }
  public virtual void BDraw_onRenderObject_GS(ref bool render, ref bool cpu) { }
  #endregion <BDraw>
}