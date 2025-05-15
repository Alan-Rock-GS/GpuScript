Shader "gs/gsTCloud"
{
  SubShader
  {
    Blend SrcAlpha OneMinusSrcAlpha
    Cull Off
    Pass
    {
      CGPROGRAM
        #pragma target 5.0
        #pragma vertex vert
        #pragma fragment frag
        #include "UnityCG.cginc"
        #include "Lighting.cginc"
        #include "../../GS/GS_Shader.cginc"
  #define ConnectStatus_Undefined	0
  #define ConnectStatus_Ok	1
  #define ConnectStatus_Licenses_Full	2
  #define ConnectStatus_Invalid_Password	3
  #define ConnectStatus_Expired_Date	4
  #define ConnectStatus_Disconnected	5
  #define ConnectStatus_Server_Down	6
  #define ConnectStatus_Attempting	7
  #define HostIP_Name_SummitPeak	0
  #define HostIP_Name_Local	1
  #define HostIP_Name_Custom	2
  #define MapType_Plane	0
  #define MapType_Sphere	1
  #define MapImage_Countries	0
  #define MapImage_Day	1
  #define MapImage_Night	2
  #define Draw_Point	0
  #define Draw_Sphere	1
  #define Draw_Line	2
  #define Draw_Arrow	3
  #define Draw_Signal	4
  #define Draw_LineSegment	5
  #define Draw_Texture_2D	6
  #define Draw_SphereTexture	7
  #define Draw_Quad	8
  #define Draw_WebCam	9
  #define Draw_Mesh	10
  #define Draw_Number	11
  #define Draw_N	12
  #define g gTCloud[0]
  #define ConnectStatus_Undefined	0
  #define ConnectStatus_Ok	1
  #define ConnectStatus_Licenses_Full	2
  #define ConnectStatus_Invalid_Password	3
  #define ConnectStatus_Expired_Date	4
  #define ConnectStatus_Disconnected	5
  #define ConnectStatus_Server_Down	6
  #define ConnectStatus_Attempting	7
  #define HostIP_Name_SummitPeak	0
  #define HostIP_Name_Local	1
  #define HostIP_Name_Custom	2
  #define MapType_Plane	0
  #define MapType_Sphere	1
  #define MapImage_Countries	0
  #define MapImage_Day	1
  #define MapImage_Night	2
  #define Draw_Point	0
  #define Draw_Sphere	1
  #define Draw_Line	2
  #define Draw_Arrow	3
  #define Draw_Signal	4
  #define Draw_LineSegment	5
  #define Draw_Texture_2D	6
  #define Draw_SphereTexture	7
  #define Draw_Quad	8
  #define Draw_WebCam	9
  #define Draw_Mesh	10
  #define Draw_Number	11
  #define Draw_N	12
  struct GTCloud
  {
    uint connectStatus, hostIP_Name, clientId, Is_Host, open_client_accounts, open_online_clients, open_client_logs, notepad, excel, visual_studio, Is_MultiUser, is_OCam_MultiUser, are_TreeGroups_MultiUser, showMap, mapType, mapImage, showInactiveClients, useLatLon, activeClientN, inactiveClientN, lineSegmentN, EarthSphereSegments;
    int App_Port;
    float clientPointSize, connectionSize, Latitude, Longitude, min_Lat, max_Lat, dLon, mapScale;
    uint2 earthColorsSize;
  };
  struct client_info { uint client_clientId; string client_Username, client_Email, client_License, client_ExpireDate, client_App, client_App_Version, client_Project, client_startTime, client_duration, client_inactive_time; float client_latency; string client_device; float client_cpu_memory, client_gpu_memory; string client_lat_long; };
  struct account_info { string account_Username, account_Email, account_App, account_Key; uint account_LicenseN; string account_ExpireDate; };
  RWStructuredBuffer<GTCloud> gTCloud;
  RWStructuredBuffer<float3> clientPoints;
  RWStructuredBuffer<float4> lineSegments;
  RWStructuredBuffer<Color32> earthColors;

  public Texture2D earthTexture;
  Texture2D _PaletteTex;
  struct v2f { float4 pos : POSITION, color : COLOR1, ti : TEXCOORD0, tj : TEXCOORD1, tk : TEXCOORD2; float3 normal : NORMAL, p0 : TEXCOORD3, p1 : TEXCOORD4, wPos : TEXCOORD5; float2 uv : TEXCOORD6; };
  float4 frag_Draw_Sphere(v2f i) { float2 uv = i.uv; float r = dot(uv, uv); float4 color = i.color; if (r > 1.0f || color.a == 0) return f0000; float3 n = new float3(uv, r - 1), _LightDir = new float3(0.321f, 0.766f, -0.557f); float lightAmp = max(0.0f, dot(n, _LightDir)); float4 diffuse_Light = (lightAmp + UNITY_LIGHTMODEL_AMBIENT) * color; float spec = max(0, (lightAmp - 0.95f) / 0.05f); color = lerp(diffuse_Light, f1111, spec / 4); color.a = 1; return color; }
  float4 frag_Draw_Line(v2f i) { float3 p0 = i.p0, p1 = i.p1; float lineRadius = i.ti.w; float2 uv = i.uv; float r = dot(uv, uv), r2 = lineRadius * lineRadius; float4 color = i.color; float3 p10 = p1 - p0; float lp10 = length(p10); if (uv.x < 0) r /= r2; else if (uv.x > lp10) { uv.x -= lp10; r = dot(uv, uv) / r2; } else { uv.x = 0; r = dot(uv, uv) / r2; } if (r > 1.0f || color.a == 0) return f0000; float3 n = new float3(uv, r - 1), _LightDir = new float3(0.321f, 0.766f, -0.557f); float lightAmp = max(0.0f, dot(n, _LightDir)); float4 diffuse_Light = (lightAmp + UNITY_LIGHTMODEL_AMBIENT) * color; float spec = max(0, (lightAmp - 0.95f) / 0.05f); color = lerp(diffuse_Light, f1111, spec / 4); color.a = 1; return color; }
  float4 frag_Quad(RWStructuredBuffer<Color> v, v2f i, uint2 _image_size) { return (float4)(i.color * v[id_to_i((uint2)(i.uv * (float2)_image_size), _image_size)]); }
  float4 frag_Quad(RWStructuredBuffer<Color32> v, v2f i, uint2 _image_size) { return (float4)(i.color * c32_f4(v[id_to_i((uint2)(i.uv * (float2)_image_size), _image_size)])); }
  float4 frag_SphereTexture(RWStructuredBuffer<Color32> v, v2f i, uint2 _image_size)
  {
    return (float4)(i.color * c32_f4(v[id_to_i((uint2)(i.uv * (float2)_image_size), _image_size)]));
  }
  float4 frag_GS(v2f i, float4 color)
  {
    switch (roundu(i.ti.z))
    {
      case uint_max: Discard(0); break;
      case Draw_Sphere: color = frag_Draw_Sphere(i); break;
      case Draw_Line: color = frag_Draw_Line(i); break;
      case Draw_Quad: color = frag_Quad(earthColors, i, g.earthColorsSize); break;
      case Draw_SphereTexture: color = frag_SphereTexture(earthColors, i, g.earthColorsSize); break;
    }
    return color;
  }
  void onRenderObject_LIN(bool show, uint _itemN, inout uint i, inout uint index, inout uint3 LIN) { uint n = 0; if (show) { if (i < (n = _itemN)) LIN = uint3(index, i, 0); LIN.z += n; i -= n; } index++; }
  uint3 onRenderObject_LIN(uint i) { uint3 LIN = u000; uint index = 0; onRenderObject_LIN(g.showMap, g.activeClientN + g.inactiveClientN, i, index, LIN); onRenderObject_LIN(g.showMap, g.lineSegmentN, i, index, LIN); onRenderObject_LIN(g.showMap && g.mapType == MapType_Sphere, g.EarthSphereSegments * g.EarthSphereSegments, i, index, LIN); return LIN; }
  float2 JQuad(uint j) { j += 2; return sign(float2(20, 18) % float2(j, j)); }
  float3 uv_to_sphere_p(float2 uv, float r) { float2 a = uv * float2(TwoPI, -PI), ca = cos(a), sa = sin(a); return float3(ca.x * sa.y, -ca.y, sa.x * sa.y) * r; }
  v2f vert_Draw_Textured_Sphere(uint segN, float r, float minLat, float maxLat, uint i, uint j, v2f o)
  {
    float2 uv = (float2(i % segN, i / segN) + JQuad(j)) / segN;
    float3 p = uv_to_sphere_p(uv, r);
    o.wPos = p; o.normal = normalize(p);
    uv.y = lerp(g.min_Lat, g.max_Lat, uv.y);
    uv.x += g.dLon;
    o.uv = uv;
    o.pos = UnityObjectToClipPos(float4(p, 1)); o.ti = float4(i, 0, Draw_SphereTexture, 0); o.color = WHITE;
    return o;
  }
  v2f vert_Draw_Earth_Sphere(uint i, uint j, v2f o)
  {
    return vert_Draw_Textured_Sphere(g.EarthSphereSegments, 12756 / 2 / 1000.0f, -67, 80, i, j, o);
  }
  float D_wrapJ(uint j, uint n) { return ((j + n) % 6) / 3; }
  float4 D_Sphere_quadPoint(float r, uint j) { return r * float4(2 * D_wrapJ(j, 2) - 1, 1 - 2 * D_wrapJ(j, 1), 0, 0); }
  v2f vert_Draw_Sphere(float3 p, float r, float4 color, uint i, uint j, v2f o) { float4 p4 = float4(p, 1), quadPoint = D_Sphere_quadPoint(r, j); o.pos = mul(UNITY_MATRIX_P, mul(UNITY_MATRIX_V, p4) + quadPoint); o.wPos = p; o.uv = quadPoint.xy / r; o.normal = -f001; o.color = color; o.ti = float4(i, 0, Draw_Sphere, 0); return o; }
  v2f vert_Draw_Client_Points(uint i, uint j, v2f o)
  {
    float3 p;
    float4 c = paletteColor(_PaletteTex, i == 0 ? 1 : i < g.activeClientN ? 0.5f : 0);
    float r = g.clientPointSize * (i == 0 || i == g.clientId ? 4 : i < g.activeClientN ? 3 : 2) / 1000.0f;
    float2 q = clientPoints[i].xy;
    if ((uint)g.mapType == MapType_Plane) p = float3(q, -0.1f);
    else
    {
      if (g.useLatLon) q = float2(g.Latitude, g.Longitude);
      p = f100 * (12756.0f / 2 / 1000 + r);
      p = rotateZDeg(p, -q.x);
      p = rotateYDeg(p, q.y);
    }
    return vert_Draw_Sphere(p, r, c, i, j, o);
  }
  float2 Draw_Line_uv(float3 p0, float3 p1, float r, uint j) { return float2(length(p1 - p0) * (1 - D_wrapJ(j, 1)), (1 - 2 * D_wrapJ(j, 2)) * r); }
  float4 Draw_LineArrow_p4(float dpf, float3 p0, float3 p1, float3 p3, float r, uint j) { float3 dp = normalize(cross(p1 - p0, p3 - p0)) * r * dpf; return float4(D_wrapJ(j, 1) * (p0 - p1) + p1 + dp * (1 - 2 * D_wrapJ(j, 2)), 1); }
  v2f vert_Draw_Line(float3 p0, float3 p1, float r, float4 color, uint i, uint j, v2f o) { o.p0 = p0; o.p1 = p1; o.uv = Draw_Line_uv(p0, p1, r, j); o.pos = UnityObjectToClipPos(Draw_LineArrow_p4(1, p0, p1, _WorldSpaceCameraPos, r, j)); o.color = color; o.ti = float4(i, 0, Draw_Line, r); return o; }
  v2f vert_Draw_Client_Connections(uint i, uint j, v2f o)
  {
    if (g.mapType == MapType_Plane)
    {
      if (i > 0)
      {
        float3 p0 = float3(clientPoints[0].xy, -0.5f), p = float3(clientPoints[i].xy, -0.5f);
        float r = g.connectionSize * g.mapScale;
        float4 c = paletteColor(_PaletteTex, clientPoints[i].z / 1000);
        o = vert_Draw_Line(p0, p, r, c, i, j, o);
      }
    }
    else
    {
      float3 p0 = lineSegments[i * 2].xyz, p = lineSegments[i * 2 + 1].xyz;
      float r = g.connectionSize * g.mapScale;
      float4 c = paletteColor(_PaletteTex, lineSegments[i].w);
      o = vert_Draw_Line(p0, p, r, c, i, j, o);
    }
    return o;
  }
  v2f vert_GS(uint i, uint j, v2f o)
  {
    uint3 LIN = onRenderObject_LIN(i); int index = -1, level = ((int)LIN.x); i = LIN.y;
    if (level == ++index) { o = vert_Draw_Client_Points(i, j, o); o.tj.x = 0; }
    else if (level == ++index) { o = vert_Draw_Client_Connections(i, j, o); o.tj.x = 0; }
    else if (level == ++index) { o = vert_Draw_Earth_Sphere(i, j, o); o.tj.x = 0; }
    return o;
  }
  float4 frag(v2f i) : SV_Target
  {
    float4 color = i.color;
    color = frag_GS(i, color);
    if (color.a == 0) Discard(0);
    return color;
  }
  v2f vert(uint i : SV_InstanceID, uint j : SV_VertexID)
  {
    v2f o;
    o.pos = o.color = o.tj = o.tk = f0000; o.normal = o.p0 = o.p1 = o.wPos = f000; o.uv = f00; o.ti = float4(i, j, 0, 0);
    return vert_GS(i, j, o);
  }
      ENDCG
    }
  }
    Fallback Off
}