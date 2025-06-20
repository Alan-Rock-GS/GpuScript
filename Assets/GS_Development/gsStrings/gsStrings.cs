using GpuScript;
using UnityEngine;

public class gsStrings : gsStrings_
{
  public GameObject sphere;
  float x = 1.2f, dx = 0.005f, y = 0.4f, dy = 0.01f;
  float3 p { get => sphere.transform.position; set => sphere.transform.position = value; }
  float px { get => p.x; set { p = float3(value, p.y, p.z); } }
  float py { get => p.y; set { p = float3(p.x, value, p.z); } }
  public override void LateUpdate1_GS() { base.LateUpdate1_GS(); dx *= abs(px += dx) > x ? -1 : 1; dy *= abs(py += dy) > y ? -1 : 1; }
}
