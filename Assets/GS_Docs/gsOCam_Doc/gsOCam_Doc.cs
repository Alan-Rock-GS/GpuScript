using GpuScript;
//using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class gsOCam_Doc : gsOCam_Doc_
{
  public override void LateUpdate1_GS()
  {
    base.LateUpdate1_GS();
    if (Screen.orientation == ScreenOrientation.Portrait) orientation = Orientation.Portrait;
    else if (Screen.orientation == ScreenOrientation.PortraitUpsideDown) orientation = Orientation.UPortrait;
    else if (Screen.orientation == ScreenOrientation.LandscapeLeft) orientation = Orientation.LLandscape;
    else if (Screen.orientation == ScreenOrientation.LandscapeRight) orientation = Orientation.RLandscape;
  }
}