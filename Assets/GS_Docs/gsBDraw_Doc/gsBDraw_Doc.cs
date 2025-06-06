using GpuScript;
using UnityEngine;

public class gsBDraw_Doc : gsBDraw_Doc_
{
  //public override void onLoaded()
  //{
  //  base.onLoaded();
  //  //Report.show_French = true;
  //  //Report.show_German = true;
  //  //Report.show_Italian = true;
  //  //Report.show_Japanese = true;
  //  //Report.show_Russian = true;
  //  Report.show_Spanish = true;

  //  //var s = StrBldr("BDraw: Selected Languages:"); foreach (var selectedLanguage in Report.SelectedLanguages) s.Add($" {selectedLanguage}"); print(s);
  //}
  public override void LateUpdate1_GS()
  {
    base.LateUpdate1_GS();
    if (!Report_Lib.isAnimating) rotation_time += speed * Time.deltaTime;
    frag(vert(0, 0));
    //vert_Draw_Shapes(id_to_i(shapeN / (u100 * 2 + 2), u111 * shapeN), 0, new v2f());
    //vert_Draw_Shapes(id_to_i(uint3(15, 12, 12), u111 * 25), 0, new v2f());
  }
  public override v2f vert_Draw_Shapes(uint i, uint j, v2f o)
  {
    uint3 id = i_to_id(i, u111 * shapeN);
    float3 p = lerp(f___, f111, id / (shapeN - 1.0f));
    float ground_y = -0.8f, vortexR = lerp(0.2f, 1, lerp1(ground_y, 1, p.y)), len = length(p.xz);
    float4 color = palette(len * 0.7f);
    bool underGround = p.y < ground_y, inVortex = len < vortexR;
    if (underGround) return vert_BDraw_Sphere(p, 0.8f / shapeN * 2, color, i, j, o);
    if (!inVortex) return o;
    float v = lerp(1, 0.1f, len / vortexR), r = v * 0.01f;
    p = rotateY(p, rotation_time * v);
    float3 dp = v * cross(normalize(p), f010) / 2;
    return vert_BDraw_Arrow(p - dp, p + dp, r, color, i, j, o);
  }
}