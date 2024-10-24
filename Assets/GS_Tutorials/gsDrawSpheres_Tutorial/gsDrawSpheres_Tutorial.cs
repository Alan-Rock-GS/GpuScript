using GpuScript;
using System.Collections;
using UnityEngine;

public class gsDrawSpheres_Tutorial : gsDrawSpheres_Tutorial_
{
  public override void NormalButton() => print("Normal Button");

  public override IEnumerator CoroutineButton_Sync()
  {
    for (int i = 0; i < 3; i++) { yield return new WaitForSeconds(1); print(i + 1); }
  }

  public override void onLoaded()
  {
    sphereN = 10;
    base.onLoaded();
  }

  public override v2f vert_Spheres(uint i, uint j, v2f o)
  {
    float r = 0.2f;
    return vert_BDraw_Sphere(i * r * f010, r, palette(i / (sphereN - 1.0f)), i, j, o);
  }
}