using System.Collections;

public class gsDrawSpheres_Tutorial : gsDrawSpheres_Tutorial_
{
  public override void onLoaded()
  {
    sphereN = 10;
    base.onLoaded();
  }

  public override v2f vert_Spheres(uint i, uint j, v2f o)
  {
    float r = 0.2f;
    return vert_BDraw_Sphere(i * r * 2 * f010, r, palette(i / (max(sphereN, 2) - 1.0f)), i, j, o);
  }
}