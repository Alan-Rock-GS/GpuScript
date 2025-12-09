
////using GpuScript;
//using System.Text;

//namespace GpuScript
//{
//  //public struct PGA4
//  //{
//  //  public float4 v0, v1, v2, v3;
//  //  public PGA4(float f, uint i) { this = GS.setPGA(GS.pga4_zero, i, f); }
//  //  public PGA4(PGA4 a) { v0 = a.v0; v1 = a.v1; v2 = a.v2; v3 = a.v3; }
//  //  public PGA4(params float[] a) { v0 = new float4(a[0], a[1], a[2], a[3]); v1 = new float4(a[4], a[5], a[6], a[7]); v2 = new float4(a[8], a[9], a[10], a[11]); v3 = new float4(a[12], a[13], a[14], a[15]); }
//  //  public PGA4(float4 _v0, float4 _v1, float4 _v2, float4 _v3) { v0 = _v0; v1 = _v1; v2 = _v2; v3 = _v3; }
//  //  public static string[] _basis = new[] { "1", "e0", "e1", "e2", "e3", "e01", "e02", "e03", "e12", "e31", "e23", "e021", "e013", "e032", "e123", "e0123" };
//  //  public override string ToString() { var sb = new StringBuilder(); var n = 0; for (uint i = 0; i < 16; i++) if (GS.getPGA(this, i) != 0) { sb.Append($"({GS.getPGA(this, i):0.###} {(i == 0 ? string.Empty : _basis[i])}) + "); n++; } if (n == 0) sb.Append("0"); return sb.ToString().TrimEnd(' ', '+'); }
//  //};

//  public struct PGA3
//  {
//    public float s; public float4 v; public float3 e, E; public float4 t; public float p;
//    public PGA3(float f, uint i) { this = setPGA3(GS.pga3_zero, i, f); }
//    public PGA3(PGA3 a) { s = a.s; v = a.v; e = a.e; E = a.E; t = a.t; p = a.p; }
//    public PGA3(params float[] a) { s = a[0]; v = float4(a[1], a[2], a[3], a[4]); e = float3(a[5], a[6], a[7]); E = float3(a[8], a[9], a[10]); t = float4(a[11], a[12], a[13], a[14]); p = a[15]; }
//    public PGA3(float _s, float4 _v, float3 _e, float3 _E, float4 _t, float _p) { s = _s; v = _v; e = _e; E = _E; t = _t; p = _p; }
//    public PGA3(float4 v0, float4 v1, float4 v2, float4 v3) { s = v0.x; v = float4(v0.yzw, v1.x); e = v1.yzw; E = v2.xyz; t = float4(v2.w, v3.xyz); p = v3.w; }

//    public static readonly float3 f000 = new float3(0, 0, 0);
//    public static readonly float4 f0000 = new float4(0, 0, 0, 0);
//    public static float4 float4(float x, float y, float z, float w) { return new float4(x, y, z, w); }
//    public static float4 float4(float3 xyz, float w) { return new float4(xyz, w); }
//    public static float4 float4(float x, float3 yzw) { return new float4(x, yzw); }
//    public static float3 float3(float x, float y, float z) { return new float3(x, y, z); }
//    public static float getPGA3(PGA3 a, uint i) { return GS.getPGA3(a, i); }
//    public static PGA3 setPGA3(PGA3 a, uint i, float f) { return GS.setPGA3(a, i, f); }
//    public static string[] _basis = new[] { "1", "e0", "e1", "e2", "e3", "e01", "e02", "e03", "e12", "e31", "e23", "e021", "e013", "e032", "e123", "e0123" };
//    public override string ToString() { var sb = new StringBuilder(); var n = 0; for (uint i = 0; i < 16; i++) if (getPGA3(this, i) != 0) { sb.Append($"({getPGA3(this, i):0.###} {(i == 0 ? string.Empty : _basis[i])}) + "); n++; } if (n == 0) sb.Append("0"); return sb.ToString().TrimEnd(' ', '+'); }


//    //public static string operator ~(PGA3 a) { return $"Reverse(a)"; }
//    public static PGA3 operator ~(PGA3 a) { return GS.Reverse(a); }
//    public static PGA3 operator !(PGA3 a) { return GS.Dual(a); }
//    public static PGA3 operator *(PGA3 a, PGA3 b) { return GS.Mul(a, b); }
//    public static PGA3 operator *(float a, PGA3 b) { return GS.Mul(a, b); }
//    public static PGA3 operator *(PGA3 a, float b) { return GS.Mul(a, b); }
//    public static PGA3 operator ^(PGA3 a, PGA3 b) { return GS.Wedge(a, b); }
//    public static PGA3 operator &(PGA3 a, PGA3 b) { return GS.Vee(a, b); }
//    public static PGA3 operator |(PGA3 a, PGA3 b) { return GS.Dot(a, b); }
//    public static PGA3 operator +(PGA3 a, PGA3 b) { return GS.Add(a, b); }
//    public static PGA3 operator +(float a, PGA3 b) { return GS.Add(a, b); }
//    public static PGA3 operator +(PGA3 a, float b) { return GS.Add(a, b); }
//    public static PGA3 operator -(PGA3 a, PGA3 b) { return GS.Sub(a, b); }
//    public static PGA3 operator -(float a, PGA3 b) { return GS.Sub(a, b); }
//    public static PGA3 operator -(PGA3 a, float b) { return GS.Sub(a, b); }

//  };

//  public struct PGA2
//  {
//    public float s; public float3 e, E; public float p;
//    public PGA2(float f, uint i) { this = setPGA2(GS.pga2_zero, i, f); }
//    public PGA2(PGA2 a) { s = a.s; e = a.e; E = a.E; p = a.p; }
//    public PGA2(params float[] a) { s = a[0]; e = float3(a[1], a[2], a[3]); E = float3(a[4], a[5], a[6]); p = a[7]; }
//    public PGA2(float _s, float3 _e, float3 _E, float _p) { s = _s; e = _e; E = _E; p = _p; }
//    public PGA2(float4 v0, float4 v1) { s = v0.x; e = v0.yzw; E = v1.xyz; p = v1.w; }

//    public static float3 float3(float x, float y, float z) { return new float3(x, y, z); }
//    public static float getPGA2(PGA2 a, uint i) { return GS.getPGA2(a, i); }
//    public static PGA2 setPGA2(PGA2 a, uint i, float f) { return GS.setPGA2(a, i, f); }
//    public static string[] _basis = new[] { "1", "e0", "e1", "e2", "e01", "e20", "e12", "e012" };
//    public override string ToString() { var sb = new StringBuilder(); var n = 0; for (uint i = 0; i < 8; ++i) if (getPGA2(this, i) != 0.0f) { sb.Append($"({getPGA2(this, i):0.###} {(i == 0 ? string.Empty : _basis[i])}) + "); n++; } if (n == 0) sb.Append("0"); return sb.ToString().TrimEnd(' ', '+'); }

//    public static PGA2 operator ~(PGA2 a) { return GS.Reverse(a); }
//    public static PGA2 operator !(PGA2 a) { return GS.Dual(a); }
//    public static PGA2 operator *(PGA2 a, PGA2 b) { return GS.Mul(a, b); }
//    public static PGA2 operator *(float a, PGA2 b) { return GS.Mul(a, b); }
//    public static PGA2 operator *(PGA2 a, float b) { return GS.Mul(a, b); }
//    public static PGA2 operator ^(PGA2 a, PGA2 b) { return GS.Wedge(a, b); }
//    public static PGA2 operator &(PGA2 a, PGA2 b) { return GS.Vee(a, b); }
//    public static PGA2 operator |(PGA2 a, PGA2 b) { return GS.Dot(a, b); }
//    public static PGA2 operator +(PGA2 a, PGA2 b) { return GS.Add(a, b); }
//    public static PGA2 operator +(float a, PGA2 b) { return GS.Add(a, b); }
//    public static PGA2 operator +(PGA2 a, float b) { return GS.Add(a, b); }
//    public static PGA2 operator -(PGA2 a, PGA2 b) { return GS.Sub(a, b); }
//    public static PGA2 operator -(float a, PGA2 b) { return GS.Sub(a, b); }
//    public static PGA2 operator -(PGA2 a, float b) { return GS.Sub(a, b); }
//  }


//  //public struct PGA //: IBStream, IEquatable<float2>, IFormattable
//  //{

//  //  public static readonly float4 f0000 = new float4(0, 0, 0, 0);
//  //  public static readonly float4 f0001 = new float4(0, 0, 0, 1);
//  //  public static readonly float4 f0010 = new float4(0, 0, 1, 0);
//  //  public static readonly float4 f0011 = new float4(0, 0, 1, 1);
//  //  public static readonly float4 f0100 = new float4(0, 1, 0, 0);
//  //  public static readonly float4 f0101 = new float4(0, 1, 0, 1);
//  //  public static readonly float4 f0110 = new float4(0, 1, 1, 0);
//  //  public static readonly float4 f0111 = new float4(0, 1, 1, 1);
//  //  public static readonly float4 f1000 = new float4(1, 0, 0, 0);
//  //  public static readonly float4 f1001 = new float4(1, 0, 0, 1);
//  //  public static readonly float4 f1010 = new float4(1, 0, 1, 0);
//  //  public static readonly float4 f1011 = new float4(1, 0, 1, 1);
//  //  public static readonly float4 f1100 = new float4(1, 1, 0, 0);
//  //  public static readonly float4 f1101 = new float4(1, 1, 0, 1);
//  //  public static readonly float4 f1110 = new float4(1, 1, 1, 0);
//  //  public static readonly float4 f1111 = new float4(1, 1, 1, 1);

//  //  public static readonly float4 f____ = new float4(-1, -1, -1, -1);
//  //  public static readonly float4 f___1 = new float4(-1, -1, -1, 1);
//  //  public static readonly float4 f__1_ = new float4(-1, -1, 1, -1);
//  //  public static readonly float4 f__11 = new float4(-1, -1, 1, 1);
//  //  public static readonly float4 f_1__ = new float4(-1, 1, -1, -1);
//  //  public static readonly float4 f_1_1 = new float4(-1, 1, -1, 1);
//  //  public static readonly float4 f_11_ = new float4(-1, 1, 1, -1);
//  //  public static readonly float4 f_111 = new float4(-1, 1, 1, 1);
//  //  public static readonly float4 f1___ = new float4(1, -1, -1, -1);
//  //  public static readonly float4 f1__1 = new float4(1, -1, -1, 1);
//  //  public static readonly float4 f1_1_ = new float4(1, -1, 1, -1);
//  //  public static readonly float4 f1_11 = new float4(1, -1, 1, 1);
//  //  public static readonly float4 f11__ = new float4(1, 1, -1, -1);
//  //  public static readonly float4 f11_1 = new float4(1, 1, -1, 1);
//  //  public static readonly float4 f111_ = new float4(1, 1, 1, -1);

//  //  public static readonly float4 f000_ = new float4(0, 0, 0, -1);
//  //  public static readonly float4 f00_0 = new float4(0, 0, -1, 0);
//  //  public static readonly float4 f00__ = new float4(0, 0, -1, -1);
//  //  public static readonly float4 f0_00 = new float4(0, -1, 0, 0);
//  //  public static readonly float4 f0_0_ = new float4(0, -1, 0, -1);
//  //  public static readonly float4 f0__0 = new float4(0, -1, -1, 0);
//  //  public static readonly float4 f0___ = new float4(0, -1, -1, -1);
//  //  public static readonly float4 f_000 = new float4(-1, 0, 0, 0);
//  //  public static readonly float4 f_00_ = new float4(-1, 0, 0, -1);
//  //  public static readonly float4 f_0_0 = new float4(-1, 0, -1, 0);
//  //  public static readonly float4 f_0__ = new float4(-1, 0, -1, -1);
//  //  public static readonly float4 f__00 = new float4(-1, -1, 0, 0);
//  //  public static readonly float4 f__0_ = new float4(-1, -1, 0, -1);
//  //  public static readonly float4 f___0 = new float4(-1, -1, -1, 0);

//  //  public static float4 float4(float x, float y, float z, float w) { return new float4(x, y, z, w); }
//  //  public static float4 float4(double x, double y, double z, double w) { return new float4(x, y, z, w); }
//  //  public static float4 float4(string x, string y, string z, string w) { return new float4(x, y, z, w); }
//  //  public static float4 float4(float2 xy, float z, float w) { return new float4(xy, z, w); }
//  //  public static float4 float4(float2 xy, float2 zw) { return new float4(xy, zw); }
//  //  public static float4 float4(float3 xyz, float w) { return new float4(xyz, w); }
//  //  public static float4 float4(float x, float y, float2 zw) { return new float4(x, y, zw); }
//  //  public static float4 float4(float x, float3 yzw) { return new float4(x, yzw); }
//  //  public static float4 float4(float v) { return new float4(v); }
//  //  public static float4 float4(string v) { return new float4(v); }

//  //  public static int csum(int2 x) { return x.x + x.y; }
//  //  public static int csum(int3 x) { return x.x + x.y + x.z; }
//  //  public static int csum(int4 x) { return x.x + x.y + x.z + x.w; }
//  //  public static uint csum(uint2 x) { return x.x + x.y; }
//  //  public static uint csum(uint3 x) { return x.x + x.y + x.z; }
//  //  public static uint csum(uint4 x) { return x.x + x.y + x.z + x.w; }
//  //  public static float csum(float2 x) { return x.x + x.y; }
//  //  public static float csum(float3 x) { return x.x + x.y + x.z; }
//  //  public static float csum(float4 x) { return (x.x + x.y) + (x.z + x.w); }
//  //  public static double csum(double2 x) { return x.x + x.y; }
//  //  public static double csum(double3 x) { return x.x + x.y + x.z; }

//  //  public static string[] _basis = new[] { "1", "e0", "e1", "e2", "e3", "e01", "e02", "e03", "e12", "e31", "e23", "e021", "e013", "e032", "e123", "e0123" };

//  //  public float4 v0, v1, v2, v3;

//  //  float v(uint i) { return i < 4 ? v0[i] : i < 8 ? v1[i - 4] : i < 12 ? v2[i - 8] : v3[i - 12]; }
//  //  void v(uint i, float f) { if (i < 4) v0[i] = f; else if (i < 8) v1[i - 4] = f; else if (i < 12) v2[i - 8] = f; else v3[i - 12] = f; }

//  //  public PGA(float f = 0f, uint idx = 0) { v0 = f0000; v1 = f0000; v2 = f0000; v3 = f0000; v(idx, f); }
//  //  public PGA(PGA a) { v0 = a.v0; v1 = a.v1; v2 = a.v2; v3 = a.v3; }
//  //  public PGA(params float[] a) { v0 = float4(a[0], a[1], a[2], a[3]); v1 = float4(a[4], a[5], a[6], a[7]); v2 = float4(a[8], a[9], a[10], a[11]); v3 = float4(a[12], a[13], a[14], a[15]); }
//  //  public PGA(float4 _v0, float4 _v1, float4 _v2, float4 _v3) { v0 = _v0; v1 = _v1; v2 = _v2; v3 = _v3; }

//  //  public float this[uint i] { get { return v(i); } set { v(i, value); } }
//  //  public float this[int i] { get { return v((uint)i); } set { v((uint)i, value); } }

//  //  #region Overloaded Operators

//  //  public static PGA Reverse(PGA a) { return GS.PGA(a.v0, a.v1 * f1___, -a.v2, a.v3 * f___1); }
//  //  public static PGA operator ~(PGA a) { return Reverse(a); }
//  //  public static PGA Dual(PGA a) { return GS.PGA(a.v3.wzyx, a.v2.wzyx, a.v1.wzyx, a.v0.wzyx); }
//  //  public static PGA operator !(PGA a) { return Dual(a); }
//  //  public PGA Conjugate() { return GS.PGA(v0 * f1___, -v1, v2 * f___1, v3); }
//  //  public PGA Involute() { return GS.PGA(v0 * f1___, v1 * f_111, v2 * f111_, v3 * f___1); }
//  //  public static PGA Mul(PGA a, PGA b)
//  //  {
//  //    return GS.PGA(
//  //     csum(b.v0.xzw * a.v0.xzw) + b.v1.x * a.v1.x - csum(b.v2.xyz * a.v2.xyz) - b.v3.z * a.v3.z,
//  //     csum(b.v0.yx * a.v0.xy) - csum(b.v1.yz * a.v0.zw) + csum(b.v1.wx * a.v1.xw * GS.f_1) + csum(b.v0.zw * a.v1.yz) + csum(b.v2.wx * a.v2.xw) + csum(b.v3.xy * a.v2.yz) + csum(b.v2.yz * a.v3.xy) + csum(b.v3.wz * a.v3.zw * GS.f1_),
//  //     csum(b.v0.zx * a.v0.xz) - b.v2.x * a.v0.w + b.v2.y * a.v1.x + b.v0.w * a.v2.x - b.v1.x * a.v2.y - b.v3.z * a.v2.z - b.v2.z * a.v3.z,
//  //     b.v0.w * a.v0.x + b.v2.x * a.v0.z + b.v0.x * a.v0.w - b.v2.z * a.v1.x - b.v0.z * a.v2.x - b.v3.z * a.v2.y + b.v1.x * a.v2.z - b.v2.y * a.v3.z,
//  //     b.v1.x * a.v0.x - b.v2.y * a.v0.z + b.v2.z * a.v0.w + b.v0.x * a.v1.x - b.v3.z * a.v2.x + b.v0.z * a.v2.y - b.v0.w * a.v2.z - b.v2.x * a.v3.z,
//  //     b.v1.y * a.v0.x + b.v0.z * a.v0.y - b.v0.y * a.v0.z - b.v2.w * a.v0.w + b.v3.x * a.v1.x + b.v0.x * a.v1.y - b.v2.x * a.v1.z + b.v2.y * a.v1.w + b.v1.z * a.v2.x - b.v1.w * a.v2.y - b.v3.w * a.v2.z - b.v0.w * a.v2.w + b.v1.x * a.v3.x + b.v3.z * a.v3.y - b.v3.y * a.v3.z - b.v2.z * a.v3.w,
//  //     b.v1.z * a.v0.x + b.v0.w * a.v0.y + b.v2.w * a.v0.z - b.v0.y * a.v0.w - b.v3.y * a.v1.x + b.v2.x * a.v1.y + b.v0.x * a.v1.z - b.v2.z * a.v1.w - b.v1.y * a.v2.x - b.v3.w * a.v2.y + b.v1.w * a.v2.z + b.v0.z * a.v2.w + b.v3.z * a.v3.x - b.v1.x * a.v3.y - b.v3.x * a.v3.z - b.v2.y * a.v3.w,
//  //     b.v1.w * a.v0.x + b.v1.x * a.v0.y - b.v3.x * a.v0.z + b.v3.y * a.v0.w - b.v0.y * a.v1.x - b.v2.y * a.v1.y + b.v2.z * a.v1.z + b.v0.x * a.v1.w - b.v3.w * a.v2.x + b.v1.y * a.v2.y - b.v1.z * a.v2.z + b.v3.z * a.v2.w - b.v0.z * a.v3.x + b.v0.w * a.v3.y - b.v2.w * a.v3.z - b.v2.x * a.v3.w,
//  //     b.v2.x * a.v0.x + b.v0.w * a.v0.z - b.v0.z * a.v0.w + b.v3.z * a.v1.x + b.v0.x * a.v2.x + b.v2.z * a.v2.y - b.v2.y * a.v2.z + b.v1.x * a.v3.z,
//  //     b.v2.y * a.v0.x - b.v1.x * a.v0.z + b.v3.z * a.v0.w + b.v0.z * a.v1.x - b.v2.z * a.v2.x + b.v0.x * a.v2.y + b.v2.x * a.v2.z + b.v0.w * a.v3.z,
//  //     b.v2.z * a.v0.x + b.v3.z * a.v0.z + b.v1.x * a.v0.w - b.v0.w * a.v1.x + b.v2.y * a.v2.x - b.v2.x * a.v2.y + b.v0.x * a.v2.z + b.v0.z * a.v3.z,
//  //     b.v2.w * a.v0.x - b.v2.x * a.v0.y + b.v1.z * a.v0.z - b.v1.y * a.v0.w + b.v3.w * a.v1.x - b.v0.w * a.v1.y + b.v0.z * a.v1.z - b.v3.z * a.v1.w - b.v0.y * a.v2.x + b.v3.y * a.v2.y - b.v3.x * a.v2.z + b.v0.x * a.v2.w + b.v2.z * a.v3.x - b.v2.y * a.v3.y + b.v1.w * a.v3.z - b.v1.x * a.v3.w,
//  //     b.v3.x * a.v0.x - b.v2.y * a.v0.y - b.v1.w * a.v0.z + b.v3.w * a.v0.w + b.v1.y * a.v1.x + b.v1.x * a.v1.y - b.v3.z * a.v1.z - b.v0.z * a.v1.w - b.v3.y * a.v2.x - b.v0.y * a.v2.y + b.v2.w * a.v2.z - b.v2.z * a.v2.w + b.v0.x * a.v3.x + b.v2.x * a.v3.y + b.v1.z * a.v3.z - b.v0.w * a.v3.w,
//  //     b.v3.y * a.v0.x - b.v2.z * a.v0.y + b.v3.w * a.v0.z + b.v1.w * a.v0.w - b.v1.z * a.v1.x - b.v3.z * a.v1.y - b.v1.x * a.v1.z + b.v0.w * a.v1.w + b.v3.x * a.v2.x - b.v2.w * a.v2.y - b.v0.y * a.v2.z + b.v2.y * a.v2.w - b.v2.x * a.v3.x + b.v0.x * a.v3.y + b.v1.y * a.v3.z - b.v0.z * a.v3.w,
//  //     b.v3.z * a.v0.x + b.v2.z * a.v0.z + b.v2.y * a.v0.w + b.v2.x * a.v1.x + b.v1.x * a.v2.x + b.v0.w * a.v2.y + b.v0.z * a.v2.z + b.v0.x * a.v3.z,
//  //     b.v3.w * a.v0.x + b.v3.z * a.v0.y + b.v3.y * a.v0.z + b.v3.x * a.v0.w + b.v2.w * a.v1.x + b.v2.z * a.v1.y + b.v2.y * a.v1.z + b.v2.x * a.v1.w + b.v1.w * a.v2.x + b.v1.z * a.v2.y + b.v1.y * a.v2.z - b.v1.x * a.v2.w - b.v0.w * a.v3.x - b.v0.z * a.v3.y - b.v0.y * a.v3.z + b.v0.x * a.v3.w);
//  //  }
//  //  public static PGA operator *(PGA a, PGA b) { return Mul(a, b); }
//  //  public static PGA Wedge(PGA a, PGA b)
//  //  {
//  //    return GS.PGA(
//  //     b.v0.x * a.v0.x,
//  //     csum(b.v0.yx * a.v0.xy),
//  //     csum(b.v0.zx * a.v0.xz),
//  //     csum(b.v0.wx * a.v0.xw),
//  //     b.v1.x * a.v0.x + b.v0.x * a.v1.x,
//  //     b.v1.y * a.v0.x + b.v0.z * a.v0.y - b.v0.y * a.v0.z + b.v0.x * a.v1.y,
//  //     b.v1.z * a.v0.x + b.v0.w * a.v0.y - b.v0.y * a.v0.w + b.v0.x * a.v1.z,
//  //     b.v1.w * a.v0.x + b.v1.x * a.v0.y - b.v0.y * a.v1.x + b.v0.x * a.v1.w,
//  //     b.v2.x * a.v0.x + b.v0.w * a.v0.z - b.v0.z * a.v0.w + b.v0.x * a.v2.x,
//  //     b.v2.y * a.v0.x - b.v1.x * a.v0.z + b.v0.z * a.v1.x + b.v0.x * a.v2.y,
//  //     b.v2.z * a.v0.x + b.v1.x * a.v0.w - b.v0.w * a.v1.x + b.v0.x * a.v2.z,
//  //     b.v2.w * a.v0.x - b.v2.x * a.v0.y + b.v1.z * a.v0.z - b.v1.y * a.v0.w - b.v0.w * a.v1.y + b.v0.z * a.v1.z - b.v0.y * a.v2.x + b.v0.x * a.v2.w,
//  //     b.v3.x * a.v0.x - b.v2.y * a.v0.y - b.v1.w * a.v0.z + b.v1.y * a.v1.x + b.v1.x * a.v1.y - b.v0.z * a.v1.w - b.v0.y * a.v2.y + b.v0.x * a.v3.x,
//  //     b.v3.y * a.v0.x - b.v2.z * a.v0.y + b.v1.w * a.v0.w - b.v1.z * a.v1.x - b.v1.x * a.v1.z + b.v0.w * a.v1.w - b.v0.y * a.v2.z + b.v0.x * a.v3.y,
//  //     b.v3.z * a.v0.x + b.v2.z * a.v0.z + b.v2.y * a.v0.w + b.v2.x * a.v1.x + b.v1.x * a.v2.x + b.v0.w * a.v2.y + b.v0.z * a.v2.z + b.v0.x * a.v3.z,
//  //     b.v3.w * a.v0.x + b.v3.z * a.v0.y + b.v3.y * a.v0.z + b.v3.x * a.v0.w + b.v2.w * a.v1.x + b.v2.z * a.v1.y + b.v2.y * a.v1.z + b.v2.x * a.v1.w + b.v1.w * a.v2.x + b.v1.z * a.v2.y + b.v1.y * a.v2.z - b.v1.x * a.v2.w - b.v0.w * a.v3.x - b.v0.z * a.v3.y - b.v0.y * a.v3.z + b.v0.x * a.v3.w);
//  //  }
//  //  public static PGA operator ^(PGA a, PGA b) { return Wedge(a, b); }
//  //  public static PGA Vee(PGA a, PGA b) //The regressive product. (JOIN)
//  //  {
//  //    return GS.PGA(
//  //    csum(a.v0 * b.v3.wzyx * f_1__) + csum(a.v1 * b.v2.wzyx * f_111) + csum(a.v2 * b.v1.wzyx) + csum(a.v3 * b.v0.wzyx),
//  //    -a.v0.y * b.v3.w + a.v1.y * b.v3.y - a.v1.z * b.v3.x - a.v1.w * b.v2.w - a.v2.w * b.v1.w - a.v3.x * b.v1.z - a.v3.y * b.v1.y + a.v3.w * b.v0.y,
//  //    -a.v0.z * b.v3.w - a.v1.y * b.v3.z - a.v2.x * b.v3.x + a.v2.y * b.v2.w + a.v2.w * b.v2.y - a.v3.x * b.v2.x + a.v3.z * b.v1.y + a.v3.w * b.v0.z,
//  //    -a.v0.w * b.v3.w - a.v1.z * b.v3.z + a.v2.x * b.v3.y - a.v2.z * b.v2.w - a.v2.w * b.v2.z + a.v3.y * b.v2.x + a.v3.z * b.v1.z + a.v3.w * b.v0.w,
//  //    -a.v1.x * b.v3.w - a.v1.w * b.v3.z - a.v2.y * b.v3.y + a.v2.z * b.v3.x + a.v3.x * b.v2.z - a.v3.y * b.v2.y + a.v3.z * b.v1.w + a.v3.w * b.v1.x,
//  //    a.v1.y * b.v3.w + a.v2.w * b.v3.x - a.v3.x * b.v2.w + a.v3.w * b.v1.y,
//  //    a.v1.z * b.v3.w - a.v2.w * b.v3.y + a.v3.y * b.v2.w + a.v3.w * b.v1.z,
//  //    a.v1.w * b.v3.w + a.v3.x * b.v3.y - a.v3.y * b.v3.x + a.v3.w * b.v1.w,
//  //    a.v2.x * b.v3.w + a.v2.w * b.v3.z - a.v3.z * b.v2.w + a.v3.w * b.v2.x,
//  //    a.v2.y * b.v3.w + a.v3.x * b.v3.z - a.v3.z * b.v3.x + a.v3.w * b.v2.y,
//  //    a.v2.z * b.v3.w + a.v3.y * b.v3.z - a.v3.z * b.v3.y + a.v3.w * b.v2.z,
//  //    -a.v2.w * b.v3.w + a.v3.w * b.v2.w,
//  //    -a.v3.x * b.v3.w + a.v3.w * b.v3.x,
//  //    -a.v3.y * b.v3.w + a.v3.w * b.v3.y,
//  //    -a.v3.z * b.v3.w + a.v3.w * b.v3.z,
//  //    a.v3.w * b.v3.w);
//  //  }
//  //  public static PGA operator &(PGA a, PGA b) { return Vee(a, b); }
//  //  public static PGA Dot(PGA a, PGA b)// The inner product.
//  //  {
//  //    return GS.PGA(
//  //     csum(b.v0.xzw * a.v0.xzw) + b.v1.x * a.v1.x - csum(b.v2.xyz * a.v2.xyz) - b.v3.z * a.v3.z,
//  //     b.v0.y * a.v0.x + b.v0.x * a.v0.y - b.v1.y * a.v0.z - b.v1.z * a.v0.w - b.v1.w * a.v1.x + b.v0.z * a.v1.y + b.v0.w * a.v1.z + b.v1.x * a.v1.w + b.v2.w * a.v2.x + b.v3.x * a.v2.y + b.v3.y * a.v2.z + b.v2.x * a.v2.w + b.v2.y * a.v3.x + b.v2.z * a.v3.y + b.v3.w * a.v3.z - b.v3.z * a.v3.w,
//  //     b.v0.z * a.v0.x + b.v0.x * a.v0.z - b.v2.x * a.v0.w + b.v2.y * a.v1.x + b.v0.w * a.v2.x - b.v1.x * a.v2.y - b.v3.z * a.v2.z - b.v2.z * a.v3.z,
//  //     b.v0.w * a.v0.x + b.v2.x * a.v0.z + b.v0.x * a.v0.w - b.v2.z * a.v1.x - b.v0.z * a.v2.x - b.v3.z * a.v2.y + b.v1.x * a.v2.z - b.v2.y * a.v3.z,
//  //     b.v1.x * a.v0.x - b.v2.y * a.v0.z + b.v2.z * a.v0.w + b.v0.x * a.v1.x - b.v3.z * a.v2.x + b.v0.z * a.v2.y - b.v0.w * a.v2.z - b.v2.x * a.v3.z,
//  //     b.v1.y * a.v0.x - b.v2.w * a.v0.w + b.v3.x * a.v1.x + b.v0.x * a.v1.y - b.v3.w * a.v2.z - b.v0.w * a.v2.w + b.v1.x * a.v3.x - b.v2.z * a.v3.w,
//  //     b.v1.z * a.v0.x + b.v2.w * a.v0.z - b.v3.y * a.v1.x + b.v0.x * a.v1.z - b.v3.w * a.v2.y + b.v0.z * a.v2.w - b.v1.x * a.v3.y - b.v2.y * a.v3.w,
//  //     b.v1.w * a.v0.x - b.v3.x * a.v0.z + b.v3.y * a.v0.w + b.v0.x * a.v1.w - b.v3.w * a.v2.x - b.v0.z * a.v3.x + b.v0.w * a.v3.y - b.v2.x * a.v3.w,
//  //     b.v2.x * a.v0.x + b.v3.z * a.v1.x + b.v0.x * a.v2.x + b.v1.x * a.v3.z,
//  //     b.v2.y * a.v0.x + b.v3.z * a.v0.w + b.v0.x * a.v2.y + b.v0.w * a.v3.z,
//  //     b.v2.z * a.v0.x + b.v3.z * a.v0.z + b.v0.x * a.v2.z + b.v0.z * a.v3.z,
//  //     b.v2.w * a.v0.x + b.v3.w * a.v1.x + b.v0.x * a.v2.w - b.v1.x * a.v3.w,
//  //     b.v3.x * a.v0.x + b.v3.w * a.v0.w + b.v0.x * a.v3.x - b.v0.w * a.v3.w,
//  //     b.v3.y * a.v0.x + b.v3.w * a.v0.z + b.v0.x * a.v3.y - b.v0.z * a.v3.w,
//  //     b.v3.z * a.v0.x + b.v0.x * a.v3.z,
//  //     b.v3.w * a.v0.x + b.v0.x * a.v3.w);
//  //  }
//  //  public static PGA operator |(PGA a, PGA b) { return Dot(a, b); }
//  //  public static PGA Add(PGA a, PGA b) { return GS.PGA(a.v0 + b.v0, a.v1 + b.v1, a.v2 + b.v2, a.v3 + b.v3); }
//  //  public static PGA operator +(PGA a, PGA b) { return Add(a, b); }
//  //  public static PGA Sub(PGA a, PGA b) { return GS.PGA(a.v0 - b.v0, a.v1 - b.v1, a.v2 - b.v2, a.v3 - b.v3); }
//  //  public static PGA operator -(PGA a, PGA b) { return Sub(a, b); }
//  //  public static PGA Mul(float a, PGA b) { return GS.PGA(a * b.v0, a * b.v1, a * b.v2, a * b.v3); }
//  //  public static PGA operator *(float a, PGA b) { return Mul(a,b); }
//  //  public static PGA Mul(PGA b, float a) { return GS.PGA(a * b.v0, a * b.v1, a * b.v2, a * b.v3); }
//  //  public static PGA operator *(PGA b, float a) { return Mul(b, a); }

//  //  public static PGA Add(float a, PGA b) { return GS.PGA(float4(a + b.v0.x, b.v0.yzw), b.v1, b.v2, b.v3); }
//  //  public static PGA operator +(float a, PGA b) { return Add(a, b); }
//  //  public static PGA Add(PGA a, float b) { return GS.PGA(float4(a.v0.x + b, a.v0.yzw), a.v1, a.v2, a.v3); }
//  //  public static PGA operator +(PGA a, float b) { return Add(a, b); }
//  //  public static PGA Sub(float a, PGA b) { return GS.PGA(float4(a - b.v0.x, -b.v0.yzw), -b.v1, -b.v2, -b.v3); }
//  //  public static PGA operator -(float a, PGA b) { return Sub(a, b); }
//  //  public static PGA Sub(PGA a, float b) { return GS.PGA(float4(a.v0.x - b, a.v0.yzw), a.v1, a.v2, a.v3); }
//  //  public static PGA operator -(PGA a, float b) { return Sub(a, b); }

//  //  #endregion

//  //  public float norm() { return (float)GS.sqrt(GS.abs((this * Conjugate())[0])); }
//  //  public float inorm() { return this[1] != 0.0f ? this[1] : this[15] != 0.0f ? this[15] : (!this).norm(); }
//  //  public PGA normalized() { return this * (1 / norm()); }

//  //  public static PGA e0 = GS.PGA(1f, 1), e1 = GS.PGA(1f, 2), e2 = GS.PGA(1f, 3), e3 = GS.PGA(1f, 4);
//  //  public static PGA e01 = e0 ^ e1, e02 = e0 ^ e2, e03 = e0 ^ e3, e12 = e1 ^ e2, e31 = e3 ^ e1, e23 = e2 ^ e3;
//  //  public static PGA e123 = e1 ^ e2 ^ e3, e032 = e0 ^ e3 ^ e2, e013 = e0 ^ e1 ^ e3, e021 = e0 ^ e2 ^ e1;
//  //  public static PGA plane(float a, float b, float c, float d) { return a * e1 + b * e2 + c * e3 + d * e0; }
//  //  public static PGA plane(float4 a) { return plane(a.x, a.y, a.z, a.w); }
//  //  public static PGA point(float x, float y, float z) { return e123 + x * e032 + y * e013 + z * e021; }
//  //  public static PGA point(float3 a) { return point(a.x, a.y, a.z); }
//  //  public static PGA rotor(float angle, PGA line) { return ((float)GS.cos(angle / 2.0f)) + ((float)GS.sin(angle / 2.0f)) * line.normalized(); }
//  //  public static PGA translator(float dist, PGA line) { return 1.0f + (dist / 2.0f) * line; }

//  //  public static PGA circle(float t, float radius, PGA line) { return rotor(t * GS.TwoPI, line) * translator(radius, e1 * e0); }
//  //  public static PGA torus(float s, float t, float r1, PGA l1, float r2, PGA l2) { return circle(s, r2, l2) * circle(t, r1, l1); }
//  //  public static PGA point_on_torus(float s, float t) { var to = torus(s, t, 0.25f, e12, 0.6f, e31); return to * e123 * ~to; }

//  //  public override string ToString() { var sb = new StringBuilder(); var n = 0; for (int i = 0; i < 16; i++) if (this[i] != 0.0f) { sb.Append($"{this[i]}{(i == 0 ? string.Empty : _basis[i])} + "); n++; } if (n == 0) sb.Append("0"); return sb.ToString().TrimEnd(' ', '+'); }

//  //  public static void Test(string[] args)
//  //  {
//  //    var rot = rotor(GS.PI / 2, e1 * e2);// Elements of the even subalgebra (scalar + bivector + pss) of unit length are motors
//  //    var ax_z = e1 ^ e2;// The outer product ^ is the MEET. Here we intersect the yz (x=0) and xz (y=0) planes.
//  //    var orig = ax_z ^ e3;// line and plane meet in point. We intersect the line along the z-axis (x=0,y=0) with the xy (z=0) plane.
//  //    var px = point(GS.f100);// We can also easily create points and join them into a line using the regressive (vee, &) product.
//  //    var line = orig & px;
//  //    var p = plane(2, 0, 1, -3);// Lets also create the plane with equation 2x + z - 3 = 0
//  //    var rotated_plane = rot * p * ~rot; // rotations work on all elements
//  //    var rotated_line = rot * line * ~rot;
//  //    var rotated_point = rot * px * ~rot;
//  //    var point_on_plane = (p | px) * p;// See the 3D PGA Cheat sheet for a huge collection of useful formulas

//  //    GS.print("a point       : " + px);// Some output
//  //    GS.print("a line        : " + line);
//  //    GS.print("a plane       : " + p);
//  //    GS.print("a rotor       : " + rot);
//  //    GS.print("rotated line  : " + rotated_line);
//  //    GS.print("rotated point : " + rotated_point);
//  //    GS.print("rotated plane : " + rotated_plane);
//  //    GS.print("point on plane: " + point_on_plane.normalized());
//  //    GS.print("point on torus: " + point_on_torus(0.0f, 0.0f));

//  //  }
//  //}


//}


////// Written by a generator written by enki.
////using System;
////using System.Text;
////using static PGA.PGA3; // static variable acces

////namespace PGA
////{
////  public class PGA3
////  {
////    // just for debug and print output, the basis names
////    public static string[] _basis = new[] { "1", "e0", "e1", "e2", "e3", "e01", "e02", "e03", "e12", "e31", "e23", "e021", "e013", "e032", "e123", "e0123" };

////    private float[] _mVec = new float[16];

////    /// <summary>
////    /// Ctor
////    /// </summary>
////    /// <param name="f"></param>
////    /// <param name="idx"></param>
////    public PGA3(float f = 0f, int idx = 0)
////    {
////      _mVec[idx] = f;
////    }

////    #region Array Access
////    public float this[int idx]
////    {
////      get { return _mVec[idx]; }
////      set { _mVec[idx] = value; }
////    }
////    #endregion

////    #region Overloaded Operators

////    /// <summary>
////    /// PGA3.Reverse : res = ~a
////    /// Reverse the order of the basis blades.
////    /// </summary>
////    public static PGA3 operator ~(PGA3 a)
////    {
////      PGA3 res = new PGA3();
////      res[0] = a[0];
////      res[1] = a[1];
////      res[2] = a[2];
////      res[3] = a[3];
////      res[4] = a[4];
////      res[5] = -a[5];
////      res[6] = -a[6];
////      res[7] = -a[7];
////      res[8] = -a[8];
////      res[9] = -a[9];
////      res[10] = -a[10];
////      res[11] = -a[11];
////      res[12] = -a[12];
////      res[13] = -a[13];
////      res[14] = -a[14];
////      res[15] = a[15];
////      return res;
////    }

////    /// <summary>
////    /// PGA3.Dual : res = !a
////    /// Poincare duality operator.
////    /// </summary>
////    public static PGA3 operator !(PGA3 a)
////    {
////      PGA3 res = new PGA3();
////      res[0] = a[15];
////      res[1] = a[14];
////      res[2] = a[13];
////      res[3] = a[12];
////      res[4] = a[11];
////      res[5] = a[10];
////      res[6] = a[9];
////      res[7] = a[8];
////      res[8] = a[7];
////      res[9] = a[6];
////      res[10] = a[5];
////      res[11] = a[4];
////      res[12] = a[3];
////      res[13] = a[2];
////      res[14] = a[1];
////      res[15] = a[0];
////      return res;
////    }

////    /// <summary>
////    /// PGA3.Conjugate : res = a.Conjugate()
////    /// Clifford Conjugation
////    /// </summary>
////    public PGA3 Conjugate()
////    {
////      PGA3 res = new PGA3();
////      res[0] = this[0];
////      res[1] = -this[1];
////      res[2] = -this[2];
////      res[3] = -this[3];
////      res[4] = -this[4];
////      res[5] = -this[5];
////      res[6] = -this[6];
////      res[7] = -this[7];
////      res[8] = -this[8];
////      res[9] = -this[9];
////      res[10] = -this[10];
////      res[11] = this[11];
////      res[12] = this[12];
////      res[13] = this[13];
////      res[14] = this[14];
////      res[15] = this[15];
////      return res;
////    }

////    /// <summary>
////    /// PGA3.Involute : res = a.Involute()
////    /// Main involution
////    /// </summary>
////    public PGA3 Involute()
////    {
////      PGA3 res = new PGA3();
////      res[0] = this[0];
////      res[1] = -this[1];
////      res[2] = -this[2];
////      res[3] = -this[3];
////      res[4] = -this[4];
////      res[5] = this[5];
////      res[6] = this[6];
////      res[7] = this[7];
////      res[8] = this[8];
////      res[9] = this[9];
////      res[10] = this[10];
////      res[11] = -this[11];
////      res[12] = -this[12];
////      res[13] = -this[13];
////      res[14] = -this[14];
////      res[15] = this[15];
////      return res;
////    }

////    /// <summary>
////    /// PGA3.Mul : res = a * b
////    /// The geometric product.
////    /// </summary>
////    public static PGA3 operator *(PGA3 a, PGA3 b)
////    {
////      PGA3 res = new PGA3();
////      res[0] = b[0] * a[0] + b[2] * a[2] + b[3] * a[3] + b[4] * a[4] - b[8] * a[8] - b[9] * a[9] - b[10] * a[10] - b[14] * a[14];
////      res[1] = b[1] * a[0] + b[0] * a[1] - b[5] * a[2] - b[6] * a[3] - b[7] * a[4] + b[2] * a[5] + b[3] * a[6] + b[4] * a[7] + b[11] * a[8] + b[12] * a[9] + b[13] * a[10] + b[8] * a[11] + b[9] * a[12] + b[10] * a[13] + b[15] * a[14] - b[14] * a[15];
////      res[2] = b[2] * a[0] + b[0] * a[2] - b[8] * a[3] + b[9] * a[4] + b[3] * a[8] - b[4] * a[9] - b[14] * a[10] - b[10] * a[14];
////      res[3] = b[3] * a[0] + b[8] * a[2] + b[0] * a[3] - b[10] * a[4] - b[2] * a[8] - b[14] * a[9] + b[4] * a[10] - b[9] * a[14];
////      res[4] = b[4] * a[0] - b[9] * a[2] + b[10] * a[3] + b[0] * a[4] - b[14] * a[8] + b[2] * a[9] - b[3] * a[10] - b[8] * a[14];
////      res[5] = b[5] * a[0] + b[2] * a[1] - b[1] * a[2] - b[11] * a[3] + b[12] * a[4] + b[0] * a[5] - b[8] * a[6] + b[9] * a[7] + b[6] * a[8] - b[7] * a[9] - b[15] * a[10] - b[3] * a[11] + b[4] * a[12] + b[14] * a[13] - b[13] * a[14] - b[10] * a[15];
////      res[6] = b[6] * a[0] + b[3] * a[1] + b[11] * a[2] - b[1] * a[3] - b[13] * a[4] + b[8] * a[5] + b[0] * a[6] - b[10] * a[7] - b[5] * a[8] - b[15] * a[9] + b[7] * a[10] + b[2] * a[11] + b[14] * a[12] - b[4] * a[13] - b[12] * a[14] - b[9] * a[15];
////      res[7] = b[7] * a[0] + b[4] * a[1] - b[12] * a[2] + b[13] * a[3] - b[1] * a[4] - b[9] * a[5] + b[10] * a[6] + b[0] * a[7] - b[15] * a[8] + b[5] * a[9] - b[6] * a[10] + b[14] * a[11] - b[2] * a[12] + b[3] * a[13] - b[11] * a[14] - b[8] * a[15];
////      res[8] = b[8] * a[0] + b[3] * a[2] - b[2] * a[3] + b[14] * a[4] + b[0] * a[8] + b[10] * a[9] - b[9] * a[10] + b[4] * a[14];
////      res[9] = b[9] * a[0] - b[4] * a[2] + b[14] * a[3] + b[2] * a[4] - b[10] * a[8] + b[0] * a[9] + b[8] * a[10] + b[3] * a[14];
////      res[10] = b[10] * a[0] + b[14] * a[2] + b[4] * a[3] - b[3] * a[4] + b[9] * a[8] - b[8] * a[9] + b[0] * a[10] + b[2] * a[14];
////      res[11] = b[11] * a[0] - b[8] * a[1] + b[6] * a[2] - b[5] * a[3] + b[15] * a[4] - b[3] * a[5] + b[2] * a[6] - b[14] * a[7] - b[1] * a[8] + b[13] * a[9] - b[12] * a[10] + b[0] * a[11] + b[10] * a[12] - b[9] * a[13] + b[7] * a[14] - b[4] * a[15];
////      res[12] = b[12] * a[0] - b[9] * a[1] - b[7] * a[2] + b[15] * a[3] + b[5] * a[4] + b[4] * a[5] - b[14] * a[6] - b[2] * a[7] - b[13] * a[8] - b[1] * a[9] + b[11] * a[10] - b[10] * a[11] + b[0] * a[12] + b[8] * a[13] + b[6] * a[14] - b[3] * a[15];
////      res[13] = b[13] * a[0] - b[10] * a[1] + b[15] * a[2] + b[7] * a[3] - b[6] * a[4] - b[14] * a[5] - b[4] * a[6] + b[3] * a[7] + b[12] * a[8] - b[11] * a[9] - b[1] * a[10] + b[9] * a[11] - b[8] * a[12] + b[0] * a[13] + b[5] * a[14] - b[2] * a[15];
////      res[14] = b[14] * a[0] + b[10] * a[2] + b[9] * a[3] + b[8] * a[4] + b[4] * a[8] + b[3] * a[9] + b[2] * a[10] + b[0] * a[14];
////      res[15] = b[15] * a[0] + b[14] * a[1] + b[13] * a[2] + b[12] * a[3] + b[11] * a[4] + b[10] * a[5] + b[9] * a[6] + b[8] * a[7] + b[7] * a[8] + b[6] * a[9] + b[5] * a[10] - b[4] * a[11] - b[3] * a[12] - b[2] * a[13] - b[1] * a[14] + b[0] * a[15];
////      return res;
////    }

////    /// <summary>
////    /// PGA3.Wedge : res = a ^ b
////    /// The outer product. (MEET)
////    /// </summary>
////    public static PGA3 operator ^(PGA3 a, PGA3 b)
////    {
////      PGA3 res = new PGA3();
////      res[0] = b[0] * a[0];
////      res[1] = b[1] * a[0] + b[0] * a[1];
////      res[2] = b[2] * a[0] + b[0] * a[2];
////      res[3] = b[3] * a[0] + b[0] * a[3];
////      res[4] = b[4] * a[0] + b[0] * a[4];
////      res[5] = b[5] * a[0] + b[2] * a[1] - b[1] * a[2] + b[0] * a[5];
////      res[6] = b[6] * a[0] + b[3] * a[1] - b[1] * a[3] + b[0] * a[6];
////      res[7] = b[7] * a[0] + b[4] * a[1] - b[1] * a[4] + b[0] * a[7];
////      res[8] = b[8] * a[0] + b[3] * a[2] - b[2] * a[3] + b[0] * a[8];
////      res[9] = b[9] * a[0] - b[4] * a[2] + b[2] * a[4] + b[0] * a[9];
////      res[10] = b[10] * a[0] + b[4] * a[3] - b[3] * a[4] + b[0] * a[10];
////      res[11] = b[11] * a[0] - b[8] * a[1] + b[6] * a[2] - b[5] * a[3] - b[3] * a[5] + b[2] * a[6] - b[1] * a[8] + b[0] * a[11];
////      res[12] = b[12] * a[0] - b[9] * a[1] - b[7] * a[2] + b[5] * a[4] + b[4] * a[5] - b[2] * a[7] - b[1] * a[9] + b[0] * a[12];
////      res[13] = b[13] * a[0] - b[10] * a[1] + b[7] * a[3] - b[6] * a[4] - b[4] * a[6] + b[3] * a[7] - b[1] * a[10] + b[0] * a[13];
////      res[14] = b[14] * a[0] + b[10] * a[2] + b[9] * a[3] + b[8] * a[4] + b[4] * a[8] + b[3] * a[9] + b[2] * a[10] + b[0] * a[14];
////      res[15] = b[15] * a[0] + b[14] * a[1] + b[13] * a[2] + b[12] * a[3] + b[11] * a[4] + b[10] * a[5] + b[9] * a[6] + b[8] * a[7] + b[7] * a[8] + b[6] * a[9] + b[5] * a[10] - b[4] * a[11] - b[3] * a[12] - b[2] * a[13] - b[1] * a[14] + b[0] * a[15];
////      return res;
////    }

////    /// <summary>
////    /// PGA3.Vee : res = a & b
////    /// The regressive product. (JOIN)
////    /// </summary>
////    public static PGA3 operator &(PGA3 a, PGA3 b)
////    {
////      PGA3 res = new PGA3();
////      res[15] = 1 * (a[15] * b[15]);
////      res[14] = -1 * (a[14] * -1 * b[15] + a[15] * b[14] * -1);
////      res[13] = -1 * (a[13] * -1 * b[15] + a[15] * b[13] * -1);
////      res[12] = -1 * (a[12] * -1 * b[15] + a[15] * b[12] * -1);
////      res[11] = -1 * (a[11] * -1 * b[15] + a[15] * b[11] * -1);
////      res[10] = 1 * (a[10] * b[15] + a[13] * -1 * b[14] * -1 - a[14] * -1 * b[13] * -1 + a[15] * b[10]);
////      res[9] = 1 * (a[9] * b[15] + a[12] * -1 * b[14] * -1 - a[14] * -1 * b[12] * -1 + a[15] * b[9]);
////      res[8] = 1 * (a[8] * b[15] + a[11] * -1 * b[14] * -1 - a[14] * -1 * b[11] * -1 + a[15] * b[8]);
////      res[7] = 1 * (a[7] * b[15] + a[12] * -1 * b[13] * -1 - a[13] * -1 * b[12] * -1 + a[15] * b[7]);
////      res[6] = 1 * (a[6] * b[15] - a[11] * -1 * b[13] * -1 + a[13] * -1 * b[11] * -1 + a[15] * b[6]);
////      res[5] = 1 * (a[5] * b[15] + a[11] * -1 * b[12] * -1 - a[12] * -1 * b[11] * -1 + a[15] * b[5]);
////      res[4] = 1 * (a[4] * b[15] - a[7] * b[14] * -1 + a[9] * b[13] * -1 - a[10] * b[12] * -1 - a[12] * -1 * b[10] + a[13] * -1 * b[9] - a[14] * -1 * b[7] + a[15] * b[4]);
////      res[3] = 1 * (a[3] * b[15] - a[6] * b[14] * -1 - a[8] * b[13] * -1 + a[10] * b[11] * -1 + a[11] * -1 * b[10] - a[13] * -1 * b[8] - a[14] * -1 * b[6] + a[15] * b[3]);
////      res[2] = 1 * (a[2] * b[15] - a[5] * b[14] * -1 + a[8] * b[12] * -1 - a[9] * b[11] * -1 - a[11] * -1 * b[9] + a[12] * -1 * b[8] - a[14] * -1 * b[5] + a[15] * b[2]);
////      res[1] = 1 * (a[1] * b[15] + a[5] * b[13] * -1 + a[6] * b[12] * -1 + a[7] * b[11] * -1 + a[11] * -1 * b[7] + a[12] * -1 * b[6] + a[13] * -1 * b[5] + a[15] * b[1]);
////      res[0] = 1 * (a[0] * b[15] + a[1] * b[14] * -1 + a[2] * b[13] * -1 + a[3] * b[12] * -1 + a[4] * b[11] * -1 + a[5] * b[10] + a[6] * b[9] + a[7] * b[8] + a[8] * b[7] + a[9] * b[6] + a[10] * b[5] - a[11] * -1 * b[4] - a[12] * -1 * b[3] - a[13] * -1 * b[2] - a[14] * -1 * b[1] + a[15] * b[0]);
////      return res;
////    }

////    /// <summary>
////    /// PGA3.Dot : res = a | b
////    /// The inner product.
////    /// </summary>
////    public static PGA3 operator |(PGA3 a, PGA3 b)
////    {
////      PGA3 res = new PGA3();
////      res[0] = b[0] * a[0] + b[2] * a[2] + b[3] * a[3] + b[4] * a[4] - b[8] * a[8] - b[9] * a[9] - b[10] * a[10] - b[14] * a[14];
////      res[1] = b[1] * a[0] + b[0] * a[1] - b[5] * a[2] - b[6] * a[3] - b[7] * a[4] + b[2] * a[5] + b[3] * a[6] + b[4] * a[7] + b[11] * a[8] + b[12] * a[9] + b[13] * a[10] + b[8] * a[11] + b[9] * a[12] + b[10] * a[13] + b[15] * a[14] - b[14] * a[15];
////      res[2] = b[2] * a[0] + b[0] * a[2] - b[8] * a[3] + b[9] * a[4] + b[3] * a[8] - b[4] * a[9] - b[14] * a[10] - b[10] * a[14];
////      res[3] = b[3] * a[0] + b[8] * a[2] + b[0] * a[3] - b[10] * a[4] - b[2] * a[8] - b[14] * a[9] + b[4] * a[10] - b[9] * a[14];
////      res[4] = b[4] * a[0] - b[9] * a[2] + b[10] * a[3] + b[0] * a[4] - b[14] * a[8] + b[2] * a[9] - b[3] * a[10] - b[8] * a[14];
////      res[5] = b[5] * a[0] - b[11] * a[3] + b[12] * a[4] + b[0] * a[5] - b[15] * a[10] - b[3] * a[11] + b[4] * a[12] - b[10] * a[15];
////      res[6] = b[6] * a[0] + b[11] * a[2] - b[13] * a[4] + b[0] * a[6] - b[15] * a[9] + b[2] * a[11] - b[4] * a[13] - b[9] * a[15];
////      res[7] = b[7] * a[0] - b[12] * a[2] + b[13] * a[3] + b[0] * a[7] - b[15] * a[8] - b[2] * a[12] + b[3] * a[13] - b[8] * a[15];
////      res[8] = b[8] * a[0] + b[14] * a[4] + b[0] * a[8] + b[4] * a[14];
////      res[9] = b[9] * a[0] + b[14] * a[3] + b[0] * a[9] + b[3] * a[14];
////      res[10] = b[10] * a[0] + b[14] * a[2] + b[0] * a[10] + b[2] * a[14];
////      res[11] = b[11] * a[0] + b[15] * a[4] + b[0] * a[11] - b[4] * a[15];
////      res[12] = b[12] * a[0] + b[15] * a[3] + b[0] * a[12] - b[3] * a[15];
////      res[13] = b[13] * a[0] + b[15] * a[2] + b[0] * a[13] - b[2] * a[15];
////      res[14] = b[14] * a[0] + b[0] * a[14];
////      res[15] = b[15] * a[0] + b[0] * a[15];
////      return res;
////    }

////    /// <summary>
////    /// PGA3.Add : res = a + b
////    /// Multivector addition
////    /// </summary>
////    public static PGA3 operator +(PGA3 a, PGA3 b)
////    {
////      PGA3 res = new PGA3();
////      res[0] = a[0] + b[0];
////      res[1] = a[1] + b[1];
////      res[2] = a[2] + b[2];
////      res[3] = a[3] + b[3];
////      res[4] = a[4] + b[4];
////      res[5] = a[5] + b[5];
////      res[6] = a[6] + b[6];
////      res[7] = a[7] + b[7];
////      res[8] = a[8] + b[8];
////      res[9] = a[9] + b[9];
////      res[10] = a[10] + b[10];
////      res[11] = a[11] + b[11];
////      res[12] = a[12] + b[12];
////      res[13] = a[13] + b[13];
////      res[14] = a[14] + b[14];
////      res[15] = a[15] + b[15];
////      return res;
////    }

////    /// <summary>
////    /// PGA3.Sub : res = a - b
////    /// Multivector subtraction
////    /// </summary>
////    public static PGA3 operator -(PGA3 a, PGA3 b)
////    {
////      PGA3 res = new PGA3();
////      res[0] = a[0] - b[0];
////      res[1] = a[1] - b[1];
////      res[2] = a[2] - b[2];
////      res[3] = a[3] - b[3];
////      res[4] = a[4] - b[4];
////      res[5] = a[5] - b[5];
////      res[6] = a[6] - b[6];
////      res[7] = a[7] - b[7];
////      res[8] = a[8] - b[8];
////      res[9] = a[9] - b[9];
////      res[10] = a[10] - b[10];
////      res[11] = a[11] - b[11];
////      res[12] = a[12] - b[12];
////      res[13] = a[13] - b[13];
////      res[14] = a[14] - b[14];
////      res[15] = a[15] - b[15];
////      return res;
////    }

////    /// <summary>
////    /// PGA3.smul : res = a * b
////    /// scalar/multivector multiplication
////    /// </summary>
////    public static PGA3 operator *(float a, PGA3 b)
////    {
////      PGA3 res = new PGA3();
////      res[0] = a * b[0];
////      res[1] = a * b[1];
////      res[2] = a * b[2];
////      res[3] = a * b[3];
////      res[4] = a * b[4];
////      res[5] = a * b[5];
////      res[6] = a * b[6];
////      res[7] = a * b[7];
////      res[8] = a * b[8];
////      res[9] = a * b[9];
////      res[10] = a * b[10];
////      res[11] = a * b[11];
////      res[12] = a * b[12];
////      res[13] = a * b[13];
////      res[14] = a * b[14];
////      res[15] = a * b[15];
////      return res;
////    }

////    /// <summary>
////    /// PGA3.muls : res = a * b
////    /// multivector/scalar multiplication
////    /// </summary>
////    public static PGA3 operator *(PGA3 a, float b)
////    {
////      PGA3 res = new PGA3();
////      res[0] = a[0] * b;
////      res[1] = a[1] * b;
////      res[2] = a[2] * b;
////      res[3] = a[3] * b;
////      res[4] = a[4] * b;
////      res[5] = a[5] * b;
////      res[6] = a[6] * b;
////      res[7] = a[7] * b;
////      res[8] = a[8] * b;
////      res[9] = a[9] * b;
////      res[10] = a[10] * b;
////      res[11] = a[11] * b;
////      res[12] = a[12] * b;
////      res[13] = a[13] * b;
////      res[14] = a[14] * b;
////      res[15] = a[15] * b;
////      return res;
////    }

////    /// <summary>
////    /// PGA3.sadd : res = a + b
////    /// scalar/multivector addition
////    /// </summary>
////    public static PGA3 operator +(float a, PGA3 b)
////    {
////      PGA3 res = new PGA3();
////      res[0] = a + b[0];
////      res[1] = b[1];
////      res[2] = b[2];
////      res[3] = b[3];
////      res[4] = b[4];
////      res[5] = b[5];
////      res[6] = b[6];
////      res[7] = b[7];
////      res[8] = b[8];
////      res[9] = b[9];
////      res[10] = b[10];
////      res[11] = b[11];
////      res[12] = b[12];
////      res[13] = b[13];
////      res[14] = b[14];
////      res[15] = b[15];
////      return res;
////    }

////    /// <summary>
////    /// PGA3.adds : res = a + b
////    /// multivector/scalar addition
////    /// </summary>
////    public static PGA3 operator +(PGA3 a, float b)
////    {
////      PGA3 res = new PGA3();
////      res[0] = a[0] + b;
////      res[1] = a[1];
////      res[2] = a[2];
////      res[3] = a[3];
////      res[4] = a[4];
////      res[5] = a[5];
////      res[6] = a[6];
////      res[7] = a[7];
////      res[8] = a[8];
////      res[9] = a[9];
////      res[10] = a[10];
////      res[11] = a[11];
////      res[12] = a[12];
////      res[13] = a[13];
////      res[14] = a[14];
////      res[15] = a[15];
////      return res;
////    }

////    /// <summary>
////    /// PGA3.ssub : res = a - b
////    /// scalar/multivector subtraction
////    /// </summary>
////    public static PGA3 operator -(float a, PGA3 b)
////    {
////      PGA3 res = new PGA3();
////      res[0] = a - b[0];
////      res[1] = -b[1];
////      res[2] = -b[2];
////      res[3] = -b[3];
////      res[4] = -b[4];
////      res[5] = -b[5];
////      res[6] = -b[6];
////      res[7] = -b[7];
////      res[8] = -b[8];
////      res[9] = -b[9];
////      res[10] = -b[10];
////      res[11] = -b[11];
////      res[12] = -b[12];
////      res[13] = -b[13];
////      res[14] = -b[14];
////      res[15] = -b[15];
////      return res;
////    }

////    /// <summary>
////    /// PGA3.subs : res = a - b
////    /// multivector/scalar subtraction
////    /// </summary>
////    public static PGA3 operator -(PGA3 a, float b)
////    {
////      PGA3 res = new PGA3();
////      res[0] = a[0] - b;
////      res[1] = a[1];
////      res[2] = a[2];
////      res[3] = a[3];
////      res[4] = a[4];
////      res[5] = a[5];
////      res[6] = a[6];
////      res[7] = a[7];
////      res[8] = a[8];
////      res[9] = a[9];
////      res[10] = a[10];
////      res[11] = a[11];
////      res[12] = a[12];
////      res[13] = a[13];
////      res[14] = a[14];
////      res[15] = a[15];
////      return res;
////    }

////    #endregion

////    /// <summary>
////    /// PGA3.norm()
////    /// Calculate the Euclidean norm. (strict positive).
////    /// </summary>
////    public float norm() { return (float)Math.Sqrt(Math.Abs((this * this.Conjugate())[0])); }

////    /// <summary>
////    /// PGA3.inorm()
////    /// Calculate the Ideal norm. (signed)
////    /// </summary>
////    public float inorm() { return this[1] != 0.0f ? this[1] : this[15] != 0.0f ? this[15] : (!this).norm(); }

////    /// <summary>
////    /// PGA3.normalized()
////    /// Returns a normalized (Euclidean) element.
////    /// </summary>
////    public PGA3 normalized() { return this * (1 / norm()); }


////    // PGA is plane based. Vectors are planes. (think linear functionals)
////    public static PGA3 e0 = new PGA3(1f, 1);
////    public static PGA3 e1 = new PGA3(1f, 2);
////    public static PGA3 e2 = new PGA3(1f, 3);
////    public static PGA3 e3 = new PGA3(1f, 4);

////    // PGA lines are bivectors.
////    public static PGA3 e01 = e0 ^ e1;
////    public static PGA3 e02 = e0 ^ e2;
////    public static PGA3 e03 = e0 ^ e3;
////    public static PGA3 e12 = e1 ^ e2;
////    public static PGA3 e31 = e3 ^ e1;
////    public static PGA3 e23 = e2 ^ e3;

////    // PGA points are trivectors.
////    public static PGA3 e123 = e1 ^ e2 ^ e3; // the origin
////    public static PGA3 e032 = e0 ^ e3 ^ e2;
////    public static PGA3 e013 = e0 ^ e1 ^ e3;
////    public static PGA3 e021 = e0 ^ e2 ^ e1;

////    /// <summary>
////    /// PGA3.plane(a,b,c,d)
////    /// A plane is defined using its homogenous equation ax + by + cz + d = 0
////    /// </summary>
////    public static PGA3 plane(float a, float b, float c, float d) { return a * e1 + b * e2 + c * e3 + d * e0; }

////    /// <summary>
////    /// PGA3.point(x,y,z)
////    /// A point is just a homogeneous point, euclidean coordinates plus the origin
////    /// </summary>
////    public static PGA3 point(float x, float y, float z) { return e123 + x * e032 + y * e013 + z * e021; }

////    /// <summary>
////    /// Rotors (euclidean lines) and translators (ideal lines)
////    /// </summary>
////    public static PGA3 rotor(float angle, PGA3 line) { return ((float)Math.Cos(angle / 2.0f)) + ((float)Math.Sin(angle / 2.0f)) * line.normalized(); }
////    public static PGA3 translator(float dist, PGA3 line) { return 1.0f + (dist / 2.0f) * line; }

////    // for our toy problem (generate points on the surface of a torus)
////    // we start with a function that generates motors.
////    // circle(t) with t going from 0 to 1.
////    public static PGA3 circle(float t, float radius, PGA3 line)
////    {
////      return rotor(t * 2.0f * (float)Math.PI, line) * translator(radius, e1 * e0);
////    }

////    // a torus is now the product of two circles. 
////    public static PGA3 torus(float s, float t, float r1, PGA3 l1, float r2, PGA3 l2)
////    {
////      return circle(s, r2, l2) * circle(t, r1, l1);
////    }

////    // and to sample its points we simply sandwich the origin ..
////    public static PGA3 point_on_torus(float s, float t)
////    {
////      var to = torus(s, t, 0.25f, e12, 0.6f, e31);
////      return to * e123 * ~to;
////    }


////    /// string cast
////    public override string ToString()
////    {
////      var sb = new StringBuilder();
////      var n = 0;
////      for (int i = 0; i < 16; ++i)
////        if (_mVec[i] != 0.0f)
////        {
////          sb.Append($"{_mVec[i]}{(i == 0 ? string.Empty : _basis[i])} + ");
////          n++;
////        }
////      if (n == 0) sb.Append("0");
////      return sb.ToString().TrimEnd(' ', '+');
////    }
////  }

////  class Program
////  {


////    static void Main(string[] args)
////    {

////      // Elements of the even subalgebra (scalar + bivector + pss) of unit length are motors
////      var rot = rotor((float)Math.PI / 2.0f, e1 * e2);

////      // The outer product ^ is the MEET. Here we intersect the yz (x=0) and xz (y=0) planes.
////      var ax_z = e1 ^ e2;

////      // line and plane meet in point. We intersect the line along the z-axis (x=0,y=0) with the xy (z=0) plane.
////      var orig = ax_z ^ e3;

////      // We can also easily create points and join them into a line using the regressive (vee, &) product.
////      var px = point(1, 0, 0);
////      var line = orig & px;

////      // Lets also create the plane with equation 2x + z - 3 = 0
////      var p = plane(2, 0, 1, -3);

////      // rotations work on all elements
////      var rotated_plane = rot * p * ~rot;
////      var rotated_line = rot * line * ~rot;
////      var rotated_point = rot * px * ~rot;

////      // See the 3D PGA Cheat sheet for a huge collection of useful formulas
////      var point_on_plane = (p | px) * p;

////      // Some output
////      Console.WriteLine("a point       : " + px);
////      Console.WriteLine("a line        : " + line);
////      Console.WriteLine("a plane       : " + p);
////      Console.WriteLine("a rotor       : " + rot);
////      Console.WriteLine("rotated line  : " + rotated_line);
////      Console.WriteLine("rotated point : " + rotated_point);
////      Console.WriteLine("rotated plane : " + rotated_plane);
////      Console.WriteLine("point on plane: " + point_on_plane.normalized());
////      Console.WriteLine("point on torus: " + point_on_torus(0.0f, 0.0f));

////    }
////  }
////}

