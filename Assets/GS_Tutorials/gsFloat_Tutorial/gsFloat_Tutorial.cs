using System.Collections;

public class gsFloat_Tutorial : gsFloat_Tutorial_
{
  public override v2f vert_Spheres(uint i, uint j, v2f o)
  {
    float r = sphereRadius;
    return vert_BDraw_Sphere(i * r * 2 * f010, r, palette(i / (max(sphereN, 2) - 1.0f)), i, j, o);
  }
}