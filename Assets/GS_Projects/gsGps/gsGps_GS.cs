using GpuScript;
using UnityEngine;

[GS_UI, AttGS(GS_Class.Name, "Gps", GS_Class.Description, "Gps")]
public class gsGps_GS : _GS
{
  [GS_UI, AttGS("UI|User Interface")] TreeGroup group_UI;
  [GS_UI, AttGS("Accuracy|(accuracy, max allowed error)", UI.ValRange, "1, 100", "1, 10", "50, 2700", siUnit.m, UI.Format, "0")] float2 accuracy;
  [GS_UI, AttGS("Activate|Collect GPS location data")] bool activate;
  [GS_UI, AttGS("Position|GPS latitude and longitude", UI.Format, "0.0000", UI.ReadOnly)] float2 gpsPosition;
  [GS_UI, AttGS("GPS Altitude|GPS position with altitude", siUnit.m, UI.Format, "0.0", UI.ReadOnly)] float gpsAltitude;
  [GS_UI, AttGS("Error|Horizontal and Vertical Accuracy", siUnit.m, UI.Format, "0.0", UI.ReadOnly)] float2 gpsError;
  [GS_UI, AttGS("Time|Time stamp")] string date_time;
  [GS_UI, AttGS("Path|Get GPS data path")] void Get_GPS_Data_Path() { }
  [GS_UI, AttGS("Input|Compute shader input", UI.ValRange, 1, 1, 10)] uint input;
  [GS_UI, AttGS("Run Shader|Run compute shader")] void RunComputeShader() { }
  [GS_UI, AttGS("Output|Compute shader output", UI.ReadOnly)] uint output;
  [GS_UI, AttGS("UI|User Interface")] TreeGroupEnd groupEnd_UI;
}
