using GpuScript;
using Microsoft.Extensions.Logging.Abstractions;
using System.Collections;
using UnityEngine;

public class gsArray_Tutorial : gsArray_Tutorial_
{
  public override void NormalButton() => print("Normal Button");
  public override IEnumerator CoroutineButton_Sync()
  {
    for (int i = 0; i < 3; i++)
    {
      UI_CoroutineButton.label = $"Running {i + 1}";
      UI_CoroutineButton.description = $"{i + 1}";
      print(i + 1);
      yield return new WaitForSeconds(1);
    }
    UI_CoroutineButton.label = "Coroutine Button";
    UI_CoroutineButton.description = "Button with coroutine";
  }
  public override void onLoaded() { GenerateSpheres(); base.onLoaded(); }
  public override void GenerateSpheres()
  {
    AddComputeBuffer(ref sphereGrid, nameof(sphereGrid), sphereN);
    Gpu_BuildSpheres();
    sphereGrid_To_UI();
    //frag(vert(1, 0));
  }
  public override void BuildSpheres_GS(uint3 id)
  {
    uint i = id.x;
    SphereElement sphere = sphereGrid[i];
    sphere.p = i * sphereRadius * 2 * f010; sphere.v = i / (max(sphereN, 2) - 1.0f); sphere.r = sphereRadius;
    sphereGrid[i] = sphere;
  }
  public override void sphereGrid_OnValueChanged(int row, int col)
  {
    base.sphereGrid_OnValueChanged(row, col);
    if (sphereGrid != null)
      sphereGrid[row] = new SphereElement() { p = sphereGrid[row].p, r = sphereGrid[row].r, v = sphereGrid[row].v };
  }
  public override v2f vert_Spheres(uint i, uint j, v2f o) { SphereElement s = sphereGrid[i]; return vert_BDraw_Sphere(s.p, s.r, palette(s.v), i, j, o); }
}