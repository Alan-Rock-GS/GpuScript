using GpuScript;
using System.Linq;
using UnityEditor;
using UnityEngine;

/// <summary>
/// On Android, use 2 fingers to zoom, 4 fingers to rotate, 3 fingers to pan
/// Legend width has to be changed when rotating from portrait to landscape
/// Sphere titles should use ; as well as | for separator
/// UI_Float2 scrollbar hides when changing to another scrollbar
/// Webcam is rotated and shows back cam instead of forward cam
/// </summary>
public class gsOCam_Lib : gsOCam_Lib_, IOCam_Lib
{
  public override void InitBuffers0_GS() { base.InitBuffers0_GS(); buildText = true; }
  public override void LateUpdate1_GS() { base.LateUpdate1_GS(); if (buildText) RebuildText(); }
  public void RebuildText() { BDraw_ClearTexts(); RebuildText_Extra(); BDraw_BuildTexts(); buildText = false; }
  public v2f vert_Draw_Legend(uint i, uint j, v2f o)
  {
    float w = 0.4f, h = 8, y0 = legendSphereN * 0.4f - h / 2, y1 = h / 2;
    float3 c = f110 * 10000, p0 = c + float3(w, y0, 0), p1 = p0 + f100 * w, p2 = p1 + (y1 - y0) * f010, p3 = p0 + (y1 - y0) * f010;
    return BDraw_o_drawType(DrawType_Legend, vert_BDraw_Quad(p0, p1, p2, p3, WHITE, i, j, o));
  }
  public void RebuildText_Extra()
  {
    string[] sphereTitles = legendSphereTitles.IsEmpty() ? new string[0] : legendSphereTitles.Split('|', ';');
    legendSphereN = (uint)sphereTitles.Length;
    AllocData_legendSphereColors(legendSphereN);
    legendPaletteN = Is(displayLegendPalette);
    float3 c = f110 * 10000, p0 = c + float3(0.4f, legendSphereN * 0.4f - 4, 0), p1 = c + float3(0.4f, 4, 0);
    if (legendPaletteN == 1) BDraw_AddYAxis(legendAxisTitle, 0.25f, p0, p1, f010, f_00, BLACK, legendRange, legendFormat, 0.2f, f100, f010, 1.2f * f_00, BDraw_Text_QuadType.FrontOnly, p0, p1);
    BDraw_AddText(legendTitle, c + float3(0, 4.2f, 0), f100, f010, BLACK, EMPTY, 0.4f, BDraw_Text_QuadType.FrontOnly, BDraw_TextAlignment.BottomCenter);
    for (uint i = 0; i < legendSphereN; i++)
    {
      string text = sphereTitles[legendSphereN - i - 1];
      float4 color = palette(legendSphereN == 1 ? 0 : i / (legendSphereN - 1.0f));

      if (text.Contains("<"))
      {
        string colorText = text.Between("<", ">");
        color = colorText[0].IsDigit() ? palette(colorText.To_float()) : (float4)colorText.ToColor();
        text = text.Before("<");
      }
      BDraw_AddText(text, c + float3(-0.3f, 4 - 0.4f * (legendSphereN - i) - legendPaletteN * (8 - legendSphereN * 0.4f), 0), f100, f010, BLACK, EMPTY, 0.2f, BDraw_Text_QuadType.FrontOnly, BDraw_TextAlignment.CenterLeft);
      legendSphereColors[i] = color;
    }
    quad.transform.localPosition = float3(-0.78f, -0.713f, 5);
    quad.transform.localScale = float3(5.47f, 13.0f, 0);
    quad.Active(displayLegendBackground);
  }
  public override v2f vert_Legend(uint i, uint j, v2f o)
  {
    if (i < legendSphereN)
    {
      float3 c = f110 * 10000;
      float y = 4 - 0.4f * (legendSphereN - i) - legendPaletteN * (8 - legendSphereN * 0.4f);
      return vert_BDraw_Sphere(c + float3(-0.5f, y, 0), 0.15f, legendSphereColors[i], i, j, o);
    }
    else if (Is(legendPaletteN)) return vert_Draw_Legend(i, j, o);
    return o;
  }
  public override float4 frag_GS(v2f i, float4 color)
  {
    color = frag_BDraw_GS(i, color);
    if (BDraw_o_drawType(i) == BDraw_Draw_Texture_2D)
      switch (roundu(i.tj.z)) { case DrawType_Legend: uint drawType = roundu(i.tj.z); if (drawType == 1) color = palette(i.uv.y); break; }
    return color;
  }

  public GameObject MainCam { get; set; }
  public override void Start0_GS()
  {
    GameObject prefabObj = GameObject.Find("OCam_Lib");
    legendCamera = prefabObj.FindObject("Legend Camera");
    MainCam = prefabObj.FindObject("Main Cam");
    MousePlane = MainCam.FindObject("Mouse Plane");
    Webcam_Plane = MainCam.FindObject("Webcam Plane");
    side_Cam = prefabObj.FindObject("Side Camera").GetComponent<Camera>();
    top__Cam = prefabObj.FindObject("Top Camera").GetComponent<Camera>();
    frontCam = prefabObj.FindObject("Front Camera").GetComponent<Camera>();
    quad = legendCamera.FindObject("Legend Quad");
#if UNITY_EDITOR
    computeShader = AssetDatabase.LoadAssetAtPath<ComputeShader>($"Assets/GS_Libs/gsOCam_Lib/gsOCam_Lib.compute");
    material = AssetDatabase.LoadAssetAtPath<Material>($"Assets/GS_Libs/gsOCam_Lib/gsOCam_Lib.mat");
    if (_PaletteTex == null) _PaletteTex = AssetDatabase.LoadAssetAtPath<Texture2D>($"Assets/GS/Resources/Palettes/Rainbow.png");
#endif //UNITY_EDITOR
  }
  public void SetProjection(uint v) => projection = (ProjectionMode)v;

  public GameObject legendCamera, quad, MousePlane;
  public bool useMousePlane { get => MousePlane.IsActive(); set => MousePlane.Active(value); }
  public Camera side_Cam, top__Cam, frontCam;
  public GameObject Webcam_Plane;
  [Tooltip("Select central gameObject, if desired")] public Transform target; [HideInInspector] public Transform _target;

  public override void Update_Ortho_Size() { if (mainCam != null) mainCam.orthographicSize = dist * tanDeg(mainCam.fieldOfView / 2); }
  public void AdjustCam() { Update_Ortho_Size(); mainCam?.Render(); }

  public override void onLoaded() => GS_OCam_Lib.onLoaded(this);

  public WebCamTexture webCamTexture;
  public override void centerView() => center = Default_Center;
  public override void northView() => tiltSpin = f00;
  public override void southView() => tiltSpin = f01 * 180;
  public override void eastView() => tiltSpin = f01 * -90;
  public override void westView() => tiltSpin = f01 * 90;
  public override void downView() => tiltSpin = f10 * 90;
  public override void orthoView() => tiltSpin = orthoTiltSpin;
  protected Rect Cam_rect = float4(f0011);
  public void ConfigureCams()
  {
    if (mainCam == null) return;
    if (side_Cam != null) { side_Cam.gameObject.Active(multiCams); top__Cam.gameObject.Active(multiCams); frontCam.gameObject.Active(multiCams); }
    var cam = legendCamera.GetComponent<Camera>();

    legendViewWidth = legendWidthRatio * Screen.height / Screen.width;
    cam.rect = float4(1 - legendViewWidth, 0, legendViewWidth, 0.95f);
    cam.rect = float4(1 - legendViewWidth, 0, legendViewWidth, 0.95f);
    cam.clearFlags = CameraClearFlags.Depth;//make legend transparent

    if (UI_multiCams.Changed)
    {
      if (multiCams)
      {
        Cam_rect = mainCam.rect; mainCam.rect = displayLegend ? float4(0.45f, 0.5f, 0.45f, 0.5f) : float4(0.5f);
        if (frontCam && side_Cam && top__Cam)
        {
          frontCam.orthographicSize = dist / 3 / orthoSize; side_Cam.orthographicSize = dist / 3 / orthoSize; top__Cam.orthographicSize = dist / 3 / orthoSize;
          frontCam.transform.position = center + f001 * -100; side_Cam.transform.position = center + f100 * 100; top__Cam.transform.position = center + f010 * 100;
          if (displayLegend) { frontCam.rect = float4(0, 0, 0.45f, 0.5f); side_Cam.rect = float4(0.45f, 0, 0.45f, 0.5f); top__Cam.rect = float4(0, 0.5f, 0.45f, 0.5f); }
          else { frontCam.rect = f0011 / 2; side_Cam.rect = f1011 / 2; top__Cam.rect = f0111 / 2; }
        }
      }
      else { Cam_rect = mainCam.rect; mainCam.rect = displayLegend ? float4(0, 0, 1 - legendViewWidth, 1) : f0011; }
    }
    else Cam_rect = mainCam.rect;
  }
  public bool showWebCam => plotBackground.IsAny(PlotBackground.WebCam_Back, PlotBackground.WebCam_Front);
  public override void OnValueChanged_GS()
  {
    base.OnValueChanged_GS();
    mainCam = Camera.main;
    if (mainCam == null || legendCamera == null) return;
    if (UI_displayLegend == null) return;
    if (UI_displayLegend.Changed) legendCamera.Active(displayLegend);
    MainCam.transform.rotation = Quaternion.Euler(tiltSpin.x, tiltSpin.y, 0);
    MainCam.transform.position = MainCam.transform.rotation * f001 * -dist + center + (target ? (float3)target.position : f000);

    ProjectionMode p = projection;
    int2 tiltSpinI = roundi(tiltSpin);
    if (p == ProjectionMode.Automatic)
      p = tiltSpinI.x == 90 || tiltSpinI.x == -90 || (tiltSpinI.x == 0 && tiltSpinI.y % 90 == 0) ? ProjectionMode.Orthographic : ProjectionMode.Perspective;
    if (p == ProjectionMode.Orthographic)
    {
      if (!mainCam.orthographic || ScrollWheel != 0)
      {
        mainCam.orthographic = true;
        mainCam.orthographicSize = dist * tanDeg(mainCam.fieldOfView / 2);
        if (Webcam_Plane) Webcam_Plane.transform.localScale = float3(64, 1, 36) / 64;
      }
    }
    else if (mainCam.orthographic) { mainCam.orthographic = false; if (Webcam_Plane) Webcam_Plane.transform.localScale = float3(64, 1, 36); }
    if (UI_plotBackground.Changed)
    {
      if (plotBackground == PlotBackground.Default_Sky)
      {
        mainCam.clearFlags = UnityEngine.CameraClearFlags.Skybox; mainCam.backgroundColor = float4(49, 77, 121, 0) / 255;
        if (webCamTexture) { webCamTexture.Stop(); DestroyImmediate(webCamTexture); webCamTexture = null; }
      }
      else if (plotBackground == PlotBackground.White)
      {
        mainCam.clearFlags = UnityEngine.CameraClearFlags.SolidColor; mainCam.backgroundColor = float4(1, 1, 1, 0);
        if (webCamTexture) { webCamTexture.Stop(); DestroyImmediate(webCamTexture); webCamTexture = null; }
      }
      else if (showWebCam)
      {
        if (webCamTexture) { webCamTexture.Stop(); DestroyImmediate(webCamTexture); }
        webCamTexture = GetWebCamTexture(plotBackground == PlotBackground.WebCam_Front);
      }
      if (Webcam_Plane) Webcam_Plane.Active(showWebCam);
    }
    //#if UNITY_ANDROID && !UNITY_EDITOR
    //    //if (Webcam_Plane)
    //    //{
    //    //  Webcam_Plane.transform.localEulerAngles = plotBackground == PlotBackground.WebCam_Front ? float3(180, -90, 90) : float3(0, -90, 90);
    //    //  Webcam_Plane.transform.localScale = float3(64, 1, 36);
    //    //}
    //#else
    //    if (Webcam_Plane) Webcam_Plane.transform.localEulerAngles = float3(-270, -180, 0);




    //    //if (Webcam_Plane)
    //    //{
    //    //  //bool isPortrait = Screen.height > Screen.width;
    //    //  Webcam_Plane.transform.localEulerAngles = plotBackground == PlotBackground.WebCam_Front ? float3(180, -90, 90) : float3(0, -90, 90);
    //    //  Webcam_Plane.transform.localScale = float3(64, 1, 36);
    //    //}


    //#endif //UNITY_ANDROID && !UNITY_EDITOR


    //switch (Screen.orientation)
    //{
    //  case ScreenOrientation.Portrait:
    //    if (plotBackground == PlotBackground.WebCam_Front)
    //    {
    //      Webcam_Plane.transform.localEulerAngles = float3(180, -90, 90);
    //      Webcam_Plane.transform.localScale = float3(64, 1, 36);
    //    }
    //    else
    //    {
    //      Webcam_Plane.transform.localEulerAngles = float3(0, -90, 90);
    //      Webcam_Plane.transform.localScale = float3(64, 1, 36);
    //    }
    //    break;
    //  case ScreenOrientation.PortraitUpsideDown:
    //    if (plotBackground == PlotBackground.WebCam_Front)
    //    {
    //      Webcam_Plane.transform.localEulerAngles = float3(0, -90, 90);
    //      Webcam_Plane.transform.localScale = float3(64, 1, 36);
    //    }
    //    else
    //    {
    //      Webcam_Plane.transform.localEulerAngles = float3(180, -90, 90);
    //      Webcam_Plane.transform.localScale = float3(64, 1, 36);
    //    }
    //    break;
    //  case ScreenOrientation.LandscapeLeft:
    //    if (plotBackground == PlotBackground.WebCam_Front)
    //    {
    //      //Webcam_Plane.transform.localEulerAngles = float3(90, -90, 90);
    //      Webcam_Plane.transform.localEulerAngles = float3(45, -90, 90);
    //      Webcam_Plane.transform.localScale = float3(64, 1, 36) * 4;
    //    }
    //    else
    //    {
    //      //Webcam_Plane.transform.localEulerAngles = float3(-90, -90, 90);
    //      Webcam_Plane.transform.localEulerAngles = float3(-45, -90, 90);
    //      Webcam_Plane.transform.localScale = float3(64, 1, 36) * 4;
    //    }
    //    break;
    //  case ScreenOrientation.LandscapeRight: break;
    //}

    if (AnyChanged(UI_multiCams, UI_orthoSize, UI_center, UI_displayLegend, UI_legendWidthRatio)) ConfigureCams();
    if (UI_paletteType.Changed) { var o = gameObject.GetComponent<GS>(); "_PaletteTex".SetFieldValue(o.GetType(), o, _PaletteTex); }
    (GetComponent("gsReport_Lib") as GS)?.OnValueChanged_GS();

    switch (Screen.orientation)
    {
      case ScreenOrientation.Portrait:
        if (plotBackground == PlotBackground.WebCam_Front) { Webcam_Plane.transform.localEulerAngles = float3(180, -90, 90); Webcam_Plane.transform.localScale = float3(64, 1, 36); }
        else { Webcam_Plane.transform.localEulerAngles = float3(0, -90, 90); Webcam_Plane.transform.localScale = float3(64, 1, 36); }
        break;
      case ScreenOrientation.PortraitUpsideDown:
        if (plotBackground == PlotBackground.WebCam_Front) { Webcam_Plane.transform.localEulerAngles = float3(0, -90, 90); Webcam_Plane.transform.localScale = float3(64, 1, 36); }
        else { Webcam_Plane.transform.localEulerAngles = float3(180, -90, 90); Webcam_Plane.transform.localScale = float3(64, 1, 36); }
        break;
      case ScreenOrientation.LandscapeLeft:
        if (plotBackground == PlotBackground.WebCam_Front) { Webcam_Plane.transform.localEulerAngles = float3(45, -90, 90); Webcam_Plane.transform.localScale = float3(64, 1, 36) * 4; }
        else { Webcam_Plane.transform.localEulerAngles = float3(-45, -90, 90); Webcam_Plane.transform.localScale = float3(64, 1, 36) * 4; }
        break;
      case ScreenOrientation.LandscapeRight: break;
    }
  }
  public override void OnApplicationQuit_GS() { if (showWebCam) webCamTexture.Stop(); }
  public bool isKeyPressed() { for (char c = 'a'; c <= 'z'; c++) if (Key((KeyCode)c)) return true; for (char c = '0'; c <= '9'; c++) if (Key((KeyCode)c)) return true; return false; }
  public float2 mousePositionDelta => MousePositionDelta;
  public bool mouseLeftButton => MouseLeftButton;
  public bool mouseRightButton => MouseRightButton;
  public bool mouseMiddleButton => MouseMiddleButton;
  public bool isFunctionKey => For(1, 12).Any(i => Input.GetKey($"f{i}"));
  public override void Update1_GS()
  {
    if (mouseInUI || sliderHasFocus) return;
    if (!isKeyPressed() && !isFunctionKey)
    {
      float3 delta = new float3(MousePositionDelta, 0);
      float ShiftScale = Shift ? 1 : 0.1f;
      if (MouseLeftButton) { center += rotateYDeg(-0.4f * dist * Time.deltaTime * ShiftScale * delta, -tiltSpin.y); AdjustCam(); }
      else if (MouseRightButton) { center += rotateYDeg(-0.4f * dist * Time.deltaTime * ShiftScale * delta.xzy, -tiltSpin.y); AdjustCam(); }
      else if (MouseMiddleButton || ScrollWheel != 0)
      {
        if (MouseMiddleButton)
        {
          if (any(delta != f000))
          {
            float2 ts = tiltSpin;
            ts += Time.deltaTime * ShiftScale * rotationSpeed * f_1 * delta.yx;
            ts.x = clamp(ts.x, tiltRange);
            if (ts.y > 180) ts.y -= 360; else if (ts.y < -180) ts.y += 360;
            tiltSpin = ts;
            AdjustCam();
          }
        }
        else { dist = clamp(dist - ScrollWheel * dist * distSpeed * ShiftScale, distanceRange); AdjustCam(); }
      }
      if (checkCollisions) { RaycastHit hit; if (Physics.Linecast(MainCam.transform.position, center, out hit)) dist = max(dist, distance(center, hit.point) + distanceRange.x); }
    }
    if (showWebCam && Webcam_Plane) Update_webCamTexture(webCamTexture, Webcam_Plane);
  }
}