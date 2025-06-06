using GpuScript;
public class gsVGrid_Doc : gsVGrid_Doc_
{
  public override void Views_Lib_LoadCamView(ref Views_Lib_CamView view) => (sphere, cube, torus, box, roundBox, boxFrame, VGrid_Lib_twoSided, VGrid_Lib_meshVal, VGrid_Lib_meshRange, OCam_Lib.legendTitle) = (view.view_sphere, view.view_cube, view.view_torus, view.view_box, view.view_roundBox, view.view_boxFrame, Is(view.view_twoSided), view.view_meshVal, view.view_meshRange, view.viewName);
  public override void Views_Lib_SaveCamView(ref Views_Lib_CamView view) { view.view_sphere = sphere; view.view_cube = cube; view.view_torus = torus; view.view_box = box; view.view_roundBox = roundBox; view.view_boxFrame = boxFrame; view.view_twoSided = Is(VGrid_Lib_twoSided); view.view_meshVal = VGrid_Lib_meshVal; view.view_meshRange = VGrid_Lib_meshRange; }
  public float sdSphere(float3 p, float s) => (length(p) - s) * 0.866f;
  public float sdSphere(float3 p) => sdSphere(p, 0);
  public float sdCube(float3 p) => cmax(abs(p)) * 2;
  public float sdBox(float3 p, float3 b) { float3 q = abs(p) - b; return length(max(q, f000)) + min(cmax(q), 0); }
  public float sdRoundBox(float3 p, float3 b, float r) { float3 q = abs(p) - b + r; return length(max(q, f000)) + min(cmax(q), 0) - r; }
  public float sdBoxFrame(float3 p, float3 b, float e)
  {
    p = abs(p) - b;
    float3 q = abs(p + e) - e, pqq = float3(p.x, q.yz), qpq = float3(q.x, p.y, q.z), qqp = float3(q.xy, p.z);
    return cmin(float3(length(max(pqq, f000)) + min(cmax(pqq), 0), length(max(qpq, f000)) + min(cmax(qpq), 0), length(max(qqp, f000)) + min(cmax(qqp), 0)));
  }
  public float sdTorus(float3 p, float R, float r) => (length(float2(length(p.xz) - R, p.y)) - r) / 0.606f;
  public override void VGrid_Lib_Grid_Calc_Vals_GS(uint3 id)
  {
    float3 p = VGrid_Lib_NodeLocation3(id);
    float torus_v = sdTorus(p, 0.33f, 0), sphere_v = sdSphere(p), cube_v = sdCube(p), box_v = 0.3f + sdBox(p, f111 / 3),
      roundBox_v = 0.3f + sdRoundBox(p, f111 / 3, 0.05f), boxFrame_v = 0.3f + sdBoxFrame(p, f111 / 3, 0.05f), 
      d = max(0.001f, torus + sphere + cube + box + roundBox + boxFrame), 
      v = (torus * torus_v + sphere * sphere_v + cube * cube_v + box * box_v + roundBox * roundBox_v + boxFrame * boxFrame_v) / d;
    VGrid_Lib_Val3(id, v);
  }
}