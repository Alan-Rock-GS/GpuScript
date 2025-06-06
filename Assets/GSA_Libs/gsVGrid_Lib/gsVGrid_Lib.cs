using GpuScript;
using UnityEngine;

public class gsVGrid_Lib : gsVGrid_Lib_
{
  public Light _directionalLightObj = null;
  public Light directionalLightObj { get { if (_directionalLightObj == null) _directionalLightObj = FindGameObject("Directional Light").GetComponent<Light>(); return _directionalLightObj; } set => _directionalLightObj = value; }
  public virtual bool ViewportChanged() => any(viewRect != roundu(mainCam.pixelRect));
  public Camera _legendCam; public Camera legendCam => _legendCam ??= mainCam.gameObject.transform.parent.Find("Legend Camera")?.GetComponent<Camera>();

  public UI_TreeGroup ui_VGrid_Lib_group_VGrid_Lib => UI_group_VGrid_Lib;
  public virtual void updateScreenSize() => viewSize = (viewRect = roundu(mainCam.pixelRect)).zw;
  public override void AllocData_depthColors(uint n) => AddComputeBuffer(ref depthColors, nameof(depthColors), n);
  public override void AllocData_Vals(uint n) => AddComputeBuffer(ref Vals, nameof(Vals), n);
  public IVGrid_Lib iVGrid => lib_parent_gs as IVGrid_Lib;
  public virtual void InitVariableNBuffers(float maxNodeEdgeN) => GS_VGrid_Lib.VGrid_Lib_InitVariableNBuffers(iVGrid, maxNodeEdgeN);

  public virtual void InitVariableNBuffers() => InitVariableNBuffers(806);
  public virtual void ResizeGrid() { if (resolution > 0) { UI_slices.range_Min = gridMin() - gridExtent(); UI_slices.range_Max = gridMax() + gridExtent(); InitVariableNBuffers(); Gpu_Grid_Calc_Vals(); } }
  public override void Start0_GS() { base.Start0_GS(); if (resolution == 0) resolution = 0.1f; }
  public override void LateUpdate0_GS()
  {
    showAxes = drawGrid && drawAxes; showMeshRange = drawGrid && twoSided; showMeshVal = drawGrid && !twoSided;
    showOutline = drawGrid && drawBox; showNormalizedAxes = drawGrid && customAxesRangeN > 0; showSurface = drawGrid && drawSurface;
    if (UI_paletteRange.Changed) UI_meshVal.range = paletteRange;
  }
  public override void LateUpdate1_GS()
  {
    base.LateUpdate1_GS();
    if (!isInitBuffers) return;
    if (buildText) RebuildText();
    if (mainCam.transform.hasChanged) { retrace = true; mainCam.transform.hasChanged = false; }
    if (reCalc) { ResizeGrid(); retrace = true; }
    if (retrace) TraceRays();
    reCalc = buildText = retrace = ValuesChanged = gChanged = false;
  }
  public virtual bool Vals_Ok => Vals != null;
  public virtual void TraceRays()
  {
    updateScreenSize();
    maxDist = mainCam.farClipPlane; camToWorld = mainCam.cameraToWorldMatrix; isOrtho = mainCam.orthographic;
    orthoSize = mainCam.orthographicSize; cameraInvProjection = mainCam.projectionMatrix.inverse;
    if (Vals_Ok) if (twoSided) Gpu_Grid_TraceRay(); else Gpu_Grid_Simple_TraceRay();
  }
  //public virtual bool AddAxes1() { BDraw_AddAxes(textSize.x, textSize.y, gridMin(), gridMax(), axesRangeMin, axesRangeMax, float4(axesColor, axesOpacity), titles, axesFormats); return true; }
  //public virtual bool AddAxes2() { BDraw_AddAxes(textSize.x, textSize.y, float4(axesColor, axesOpacity), gridMin(), gridMax(), axesRangeMin, axesRangeMax, axesRangeMin1, axesRangeMax1, titles, axesFormats); return true; }
  //public virtual bool AddAxes3() { BDraw_AddAxes(textSize.x, textSize.y, gridMin(), gridMax(), float4(axesColor, axesOpacity), titles, axesFormats, zeroOrigin); return true; }
  public virtual float3 _axesRangeMin() => axesRangeMin / (siUnits ? 1 : 0.3048f);
  public virtual float3 _axesRangeMax() => axesRangeMax / (siUnits ? 1 : 0.3048f);
  public virtual float3 _axesRangeMin1() => axesRangeMin1 / (siUnits ? 1 : 0.3048f);
  public virtual float3 _axesRangeMax1() => axesRangeMax1 / (siUnits ? 1 : 0.3048f);
  public virtual bool AddAxes1() { BDraw_AddAxes(textSize.x, textSize.y, gridMin(), gridMax(), _axesRangeMin(), _axesRangeMax(), float4(axesColor, axesOpacity), titles, axesFormats); return true; }
  public virtual bool AddAxes2() { BDraw_AddAxes(textSize.x, textSize.y, float4(axesColor, axesOpacity), gridMin(), gridMax(), _axesRangeMin(), _axesRangeMax(), _axesRangeMin1(), _axesRangeMax1(), titles, axesFormats); return true; }
  public virtual bool AddAxes3() { BDraw_AddAxes(textSize.x, textSize.y, gridMin(), gridMax(), float4(axesColor, axesOpacity), titles, axesFormats, zeroOrigin); return true; }
  public virtual void RebuildText()
  {
    BDraw_ClearTexts();
    bool r = showAxes && customAxesRangeN switch { 1 => AddAxes1(), 2 => AddAxes2(), _ => AddAxes3() };
    RebuildText_Extra();
    BDraw_BuildTexts();
  }
  public virtual void RebuildText_Extra() { }
  public override void InitBuffers0_GS() { base.InitBuffers0_GS(); nodeN = roundu((gridMax() - gridMin()) / resolution + f111); retrace = reCalc = buildText = true; }
  public override void InitBuffers1_GS()
  {
    base.InitBuffers1_GS();
    updateScreenSize();
    _WorldSpaceLightPos0 = float4(-directionalLightObj.transform.forward, directionalLightObj.intensity);
    RebuildText();
    retrace = reCalc = true;
  }
  public float4 paletteBufferColor(float v) => c32_f4(paletteBuffer[roundu(clamp(v * 255, 0, 255))]);
  public float4 paletteBufferColor(float v, float w) => float4(paletteBufferColor(v).xyz, w);

  public float3 gridMin() => float3(GridX.x, GridY.x, GridZ.x);
  public float3 gridMax() => float3(GridX.y, GridY.y, GridZ.y);
  public float3 gridExtent() => gridMax() - gridMin();
  public float3 gridSize() => gridMax() - gridMin();
  public float3 gridCenter() => (gridMax() + gridMin()) / 2;

  public float2 axesRangeX() => float2(axesRangeMin.x, axesRangeMax.x);
  public float2 axesRangeY() => float2(axesRangeMin.y, axesRangeMax.y);
  public float2 axesRangeZ() => float2(axesRangeMin.z, axesRangeMax.z);

  public uint3 NodeID(uint i) => i_to_id(i, nodeN);
  public uint NodeI(uint3 id) => id_to_i(id, nodeN);
  public float3 NodeLocation(uint i) => NodeID(i) * resolution + gridMin();
  public float3 NodeLocation3(uint3 id) => id * resolution + gridMin();

  public uint3 nodeN1() => max(nodeN, u111) - u111;
  public uint GridToIndex(int3 _I) => id_to_i(clamp(_I, u000, nodeN1()), nodeN);
  public float3 GetCorner_q(float3 p) => clamp((p - gridMin()) / resolution, f000, (int3)nodeN - f111);
  public uint3 GetCorner_I(float3 q) => (uint3)q;
  public uint4 GetFaceI(uint3 _I, int3 x, int3 y, int3 z, int3 w) => new uint4(GridToIndex((int3)_I + x), GridToIndex((int3)_I + y), GridToIndex((int3)_I + z), GridToIndex((int3)_I + w));
  public uint4 GetFaceI(uint3 _I, int3 d) => GetFaceI(_I, i000 + d, i001 + d, i010 + d, i011 + d);

  //public virtual float Val(uint i) => Vals[i] / 100000.0f;
  public virtual float Val(uint i) => Vals[i];
  public virtual float Val3(uint3 id) => Val(NodeI(id));
  public float Interpolate_Val(float3 fq, uint4 c0, uint4 c1) => Interpolate(Val(c0.x), Val(c0.y), Val(c0.z), Val(c0.w), Val(c1.x), Val(c1.y), Val(c1.z), Val(c1.w), fq);
  public virtual float Val(float3 p) { float3 q = GetCorner_q(p); uint3 _I = GetCorner_I(q); return Interpolate_Val(frac(q), GetFaceI(_I, i000), GetFaceI(_I, i100)); }
  public virtual float3 Val(float3 p, float d) => float3(Val(p + f100 * d), Val(p + f010 * d), Val(p + f001 * d));

  public float setDepth(float depth, TRay ray, ref float3 p, ref float val) { p = depth * ray.direction + ray.origin; val = Val(p); return depth; }

  public float3 Normal(float3 p)
  {
    float r = resolution * 0.5f, margin = r * 0.02f;
    float3 normal = p <= gridMin() + margin;
    if (all(normal == f000)) normal = p >= gridMax() - margin;
    if (all(normal == f000)) normal = normalize(Val(p, r) - Val(p, -r));
    return normal;
  }

  public uint2 pixDepthColor(uint i) => depthColors[i];
  public uint2 pixDepthColor(uint2 id) => pixDepthColor(id_to_i(id, viewSize));
  public float pixDepth(uint2 dc) => dc.x / (float)uint_max * (maxDist - 2); // - 2: get rid of annoying circle in distance
  public float4 pixColor(uint2 dc) => c32_f4(u_c32(dc.y));

  public virtual TRay CreateShaderCameraRay(float2 _uv)
  {
    TRay ray;
    ray.origin = mul(camToWorld, isOrtho ? float4(orthoSize * _uv / float2(aspect(viewSize), 1), 0, 1) : f0001).xyz;
    ray.direction = normalize(mul(camToWorld, float4(mul(cameraInvProjection, float4(_uv, 0, 1)).xyz, 0)).xyz);
    ray.color = f0000;
    ray.dist = 0;
    return ray;
  }
  public virtual TRay CreateRay(float3 origin, float3 direction) { TRay ray; ray.origin = origin; ray.direction = direction; ray.color = f0000; ray.dist = 0; return ray; }
  public virtual TRay CreateCameraRay(float2 _uv) { TRay ray = CreateShaderCameraRay(_uv); return CreateRay(ray.origin, ray.direction); }
  public void gridMin(float3 v) { GridX = float2(v.x, GridX.y); GridY = float2(v.y, GridY.y); GridZ = float2(v.z, GridZ.y); }
  public void gridMax(float3 v) { GridX = float2(GridX.x, v.x); GridY = float2(GridY.x, v.y); GridZ = float2(GridZ.x, v.x); }
  public void gridSize(float3 v) { float3 c = gridCenter(); v /= 2; gridMin(c - v); gridMax(c + v); }
  public void gridCenter(float3 v) { float3 r = gridSize() / 2; gridMin(v - r); gridMax(v + r); }

  public void pixDepthColor(uint i, float d, float4 c) => depthColors[i] = uint2((uint)(d / maxDist * uint_max), c32_u(f4_c32(c)));
  public void pixDepthColor(uint2 id, float d, float4 c) => pixDepthColor(id_to_i(id, viewSize), d, c);

  public TRay CreateRayHit() { TRay hit; hit.origin = f000; hit.dist = fPosInf; hit.direction = f000; hit.color = f0000; return hit; }
  public void Assign(ref TRay hit, float3 position, float3 normal, float4 color, float dist) { hit.origin = position; hit.direction = normal; hit.color = color; hit.dist = dist; }
  //public void Val(uint i, float v) => Vals[i] = roundi(v * 100000);
  //public void Val3(uint3 id, float v) => Vals[NodeI(id)] = roundi(v * 100000);
  public void Val(uint i, float v) => Vals[i] = v;
  public void Val3(uint3 id, float v) => Val(NodeI(id), v);

  public bool insideSurface(float val) => IsInside(val, meshRange);

  public virtual float4 GetPointColor(float3 p, float val) => paletteBufferColor(lerp1(paletteRange, val));
  public virtual float4 GetNormalColor(TRay ray, float3 normal, float val, float3 p)
  {
    float3 lightDirection = _WorldSpaceLightPos0.xyz;
    float3 h = (lightDirection - ray.direction) / 2;
    float v = 1, gloss = 0.5f, s = sqr(abs(dot(normal, h))) * gloss, NdotL = abs(dot(normal, lightDirection));
    if (GridLineThickness > 0)
    {
      float w = 1 / max(0.00001f, GridLineThickness);
      p /= resolution;
      float3 blend = normalize(max((abs(normal) - 0.2f) * 7, 0.0f));
      v = csum(blend * saturate(0.5f * float3(product((1 - abs(1 - 2 * frac(p.yz))) * w), product((1 - abs(1 - 2 * frac(p.xz))) * w), product((1 - abs(1 - 2 * frac(p.xy))) * w))));
    }
    return float4(saturate((GetPointColor(p, val).xyz * (NdotL + 0.5f) + s) * v), opacity);
  }
  public virtual float4 GetColor(TRay ray, ref float3 normal, float val, float3 p) => GetNormalColor(ray, normal = Normal(p), val, p);

  public virtual float4 DrawSliceColor(float4 color, float3 q) => color;
  public virtual void DrawSlice(float3 axis, TRay ray, ref TRay hit, ref float3 p, ref float val, ref float depth, ref bool found, ref float4 color, float3 normal)
  {
    float depth_slice;
    float3 axis2 = rotateZYXDeg(axis, sliceRotation), q = PlaneLineIntersectionPoint(axis2, slices, ray.origin, ray.direction);
    if (q.x != fNegInf && IsNotOutside(q, gridMin(), gridMax()))
    {
      depth_slice = setDepth(max(distance(q, ray.origin), 0.018f), ray, ref p, ref val);
      if (depth_slice < depth) { found = true; depth = depth_slice; color = DrawSliceColor(GetNormalColor(ray, axis2, val, p), q); Assign(ref hit, p, normal, color, depth); }
    }
  }

  public virtual void TraceRay(uint3 id, bool isSimple)
  {
    TRay ray = CreateCameraRay(2.0f * id.xy / viewSize - 1), hit = CreateRayHit();
    float3 mn = gridMin(), mx = max(mn + 0.001f, gridMax());
    float2 dst = HitGridBox(mn, mx, ray.origin, ray.direction);
    bool hitOutside = HitOutsideGrid(dst), hitInside = HitInsideGrid(dst);
    if (hitOutside || hitInside)
    {
      float3 p = f000, normal = f100;
      float val = 0, depth = setDepth(max(dst.x, 0.018f), ray, ref p, ref val), depth2 = dst.y, step = resolution, d0, d2;
      bool found = false;
      float4 color = f0000;

      if (GridDrawFront && hitOutside) { color = GetColor(ray, ref normal, val, p); Assign(ref hit, p, normal, color, depth); }
      else
      {
        float val0 = val;
        if (isSimple)
        {
          float v = meshVal;
          for (uint i = 0, n = csum(nodeN); i < n && !found; i++)
          {
            if (val0 > v && val <= v)//outer surface without face caps, ok
            {
              val0 = val;
              for (d0 = max(0, depth - step), d2 = depth; d2 - d0 > step / 100;) { depth = setDepth((d0 + d2) / 2, ray, ref p, ref val); if (val > v) d0 = depth; else d2 = depth; }
              color = GetColor(ray, ref normal, val, p);
              Assign(ref hit, p, normal, color, depth); found = true;
            }
            else if (val0 < v && val >= v)//inner surface viewed from inside, causes back cap to have wrong size
            {
              val0 = val;
              for (d0 = depth - step, d2 = depth; d2 - d0 > step / 100;) { depth = setDepth((d0 + d2) / 2, ray, ref p, ref val); if (val < v) d0 = depth; else d2 = depth; }
              if (depth + step < depth2)//prevents artifcats on back cap
              {
                color = GetColor(ray, ref normal, val, p);
                Assign(ref hit, p, normal, color, depth); found = true;
              }
            }
            else val0 = val;
            if (depth + step > depth2) { depth = setDepth(depth2, ray, ref p, ref val); break; }
            depth = setDepth(depth + step, ray, ref p, ref val);
          }
        }
        else
        {
          for (uint i = 0, n = csum(nodeN); i < n && (opacity < 0.999f || !found); i++)
          {
            if (i == 0 && hitOutside && val <= meshRange.y && val >= meshRange.x)//front face caps, ok
            {
              val0 = val;
              color = GetColor(ray, ref normal, val, p);
              Assign(ref hit, p, normal, color, depth);
              found = true;
            }
            else if (val0 > meshRange.y && val <= meshRange.y)//outer surface without face caps, ok
            {
              val0 = val;
              for (d0 = max(0, depth - step), d2 = depth; d2 - d0 > step / 100;) { depth = setDepth((d0 + d2) / 2, ray, ref p, ref val); if (val > meshRange.y) d0 = depth; else d2 = depth; }
              color = GetColor(ray, ref normal, val, p);
              if (!found) { Assign(ref hit, p, normal, color, depth); found = true; }
              else hit.color.xyz = opacity * hit.color.xyz + (1 - opacity) * color.xyz;
            }
            else if (val0 < meshRange.x && val >= meshRange.x)//inner surface viewed from inside, causes back cap to have wrong size
            {
              val0 = val;
              for (d0 = depth - step, d2 = depth; d2 - d0 > step / 100;) { depth = setDepth((d0 + d2) / 2, ray, ref p, ref val); if (val < meshRange.x) d0 = depth; else d2 = depth; }
              if (depth + step < depth2)//prevents artifcats on back cap
              {
                color = GetColor(ray, ref normal, val, p);
                if (!found) { Assign(ref hit, p, normal, color, depth); found = true; }
                else hit.color.xyz = opacity * hit.color.xyz + (1 - opacity) * color.xyz;
              }
            }
            else if (val0 > meshRange.x && val <= meshRange.x)//inner surface, ok
            {
              val0 = val;
              for (d0 = depth - step, d2 = depth; d2 - d0 > step / 100;) { depth = setDepth((d0 + d2) / 2, ray, ref p, ref val); if (val > meshRange.x) d0 = depth; else d2 = depth; }
              color = GetColor(ray, ref normal, val, p);
              if (!found) { Assign(ref hit, p, normal, color, depth); found = true; }
              else hit.color.xyz = opacity * hit.color.xyz + (1 - opacity) * color.xyz;
            }
            else if (val0 < meshRange.y && val >= meshRange.y)//outer surface viewed from inside, ok
            {
              val0 = val;
              for (d0 = depth - step, d2 = depth; d2 - d0 > step / 100;) { depth = setDepth((d0 + d2) / 2, ray, ref p, ref val); if (val < meshRange.y) d0 = depth; else d2 = depth; }
              color = GetColor(ray, ref normal, val, p);
              if (!found) { Assign(ref hit, p, normal, color, depth); found = true; }
              else hit.color.xyz = opacity * hit.color.xyz + (1 - opacity) * color.xyz;
            }
            else if (hitInside && depth > step / 2 && val0 > meshRange.x && val <= meshRange.x)//inner surface when in middle, ok
            {
              val0 = val;
              for (d0 = depth - step, d2 = depth; d2 - d0 > step / 100;) { depth = setDepth((d0 + d2) / 2, ray, ref p, ref val); if (val > meshRange.x) d0 = depth; else d2 = depth; }
              color = GetColor(ray, ref normal, val, p);
              if (!found) { Assign(ref hit, p, normal, color, depth); found = true; }
              else hit.color.xyz = opacity * hit.color.xyz + (1 - opacity) * color.xyz;
            }
            else val0 = val;
            if (depth + step > depth2) { depth = setDepth(depth2, ray, ref p, ref val); break; }
            depth = setDepth(depth + step, ray, ref p, ref val);
          }
        }
        //if (show_slices)
        //{
        //	float2 dst_slice; float depth_slice;
        //	if (IsNotOutside(slices.x, GridX))
        //	{
        //		dst_slice = HitGridBox(float3(slices.x, gridMin().yz), float3(slices.x + 0.001f, gridMax().yz), ray.origin, ray.direction);
        //		if (HitOutsideGrid(dst_slice))
        //		{
        //			depth_slice = setDepth(max(dst_slice.x, 0.018f), ray, ref p, ref val);
        //			if (depth_slice < depth) { found = true; depth = depth_slice; color = GetNormalColor(ray, f100, val, p); Assign(ref hit, p, normal, color, depth); }
        //		}
        //	}
        //	if (IsNotOutside(slices.y, GridY))
        //	{
        //		dst_slice = HitGridBox(float3(slices.y, gridMin().xz).yxz, float3(slices.y + 0.001f, gridMax().xz).yxz, ray.origin, ray.direction);
        //		if (HitOutsideGrid(dst_slice))
        //		{
        //			depth_slice = setDepth(max(dst_slice.x, 0.018f), ray, ref p, ref val);
        //			if (depth_slice < depth) { found = true; depth = depth_slice; color = GetNormalColor(ray, f010, val, p); Assign(ref hit, p, normal, color, depth); }
        //		}
        //	}
        //	if (IsNotOutside(slices.z, GridZ))
        //	{
        //		dst_slice = HitGridBox(float3(slices.z, gridMin().xy).yzx, float3(slices.z + 0.001f, gridMax().xy).yzx, ray.origin, ray.direction);
        //		if (HitOutsideGrid(dst_slice))
        //		{
        //			depth_slice = setDepth(max(dst_slice.x, 0.018f), ray, ref p, ref val);
        //			if (depth_slice < depth) { found = true; depth = depth_slice; color = GetNormalColor(ray, f001, val, p); Assign(ref hit, p, normal, color, depth); }
        //		}
        //	}
        //}
        if (show_slices) for (uint i = 0; i < 3; i++) DrawSlice(index(f000, i, 1.0f), ray, ref hit, ref p, ref val, ref depth, ref found, ref color, normal);
        if (GridDrawBack)
        {
          color = GetColor(ray, ref normal, val, p);
          if (!found) Assign(ref hit, p, normal, color, depth);
          else hit.color.xyz = opacity * hit.color.xyz + (1 - opacity) * color.xyz;
        }
      }
    }
    pixDepthColor(id.xy, hit.dist, hit.color);
  }
  public override void Grid_Calc_Vals_GS(uint3 id)
  {
    //Val3(id, length(NodeLocation3(id)));//sphere
    //Val3(id, max(abs(NodeLocation3(id))));//cube
    float3 p = NodeLocation3(id);//torus
    float R = 0.3333f, v = sqr(length(p.xz) - R) + sqr(p.y);
    Val3(id, v * 10);
  }
  public override void Grid_TraceRay_GS(uint3 id) { TraceRay(id, false); }
  public override void Grid_Simple_TraceRay_GS(uint3 id) { TraceRay(id, true); }

  public Camera vrCam;
  public Transform camTransform0;

  public override v2f vert_BDraw_Box(uint i, uint j, v2f o) => vert_BDraw_BoxFrame(gridMin(), gridMax(), boxLineThickness, DARK_BLUE, i, j, o);
  public override v2f vert_DrawScreen(uint i, uint j, v2f o)
  {
    uint2 id = i_to_id(i, viewSize), dc = pixDepthColor(i);
    float2 uv = (id + f11 * 0.5f) / viewSize * 2 - 1;
    TRay ray = CreateShaderCameraRay(uv);
    return vert_BDraw_Point(ray.origin + pixDepth(dc) * ray.direction, pixColor(dc), i, o);
  }
  public override float4 frag_GS(v2f i, float4 color)
  {
    //switch (roundu(i.ti.z))
    switch (BDraw_o_drawType(i))
    {
      case uint_max: Discard(0); break;
      case BDraw_Draw_Sphere: color = frag_BDraw_Sphere(i); break;
      case BDraw_Draw_Line: color = frag_BDraw_Line(i); break;
      case BDraw_Draw_Arrow: color = frag_BDraw_Arrow(i); break;
      case BDraw_Draw_Signal: color = frag_BDraw_Signal(i); break;
      case BDraw_Draw_LineSegment: color = frag_BDraw_LineSegment(i); break;
      case BDraw_Draw_Mesh: color = frag_BDraw_Mesh(i); break;
      case BDraw_Draw_Text3D:
        //BDraw_TextInfo t = BDraw_textInfo(roundu(i.ti.x));
        BDraw_TextInfo t = BDraw_textInfo(BDraw_o_i(i));
        color = frag_BDraw_Text(BDraw_fontTexture, BDraw_tab_delimeted_text, BDraw_fontInfos, BDraw_fontSize, t.quadType, t.backColor, BDraw_Get_text_indexes(t.textI), i);
        break;
    }
    return color;
  }
}