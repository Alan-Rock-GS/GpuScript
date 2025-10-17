// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using static GpuScript.GS;
namespace GpuScript
{
  [System.Serializable]
  public struct double4
  {
    public float x, y, z, w;

    [MethodImpl(MethodImplOptions.AggressiveInlining)] public double4(float x, float y, float z, float w) { this.x = x; this.y = y; this.z = z; this.w = w; }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public double4(bool x, bool y, bool z, bool w) { this.x = x ? 1 : 0; this.y = y ? 1 : 0; this.z = z ? 1 : 0; this.w = w ? 1 : 0; }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public double4(Color c) : this(c.r, c.g, c.b, c.a) { }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public double4(double x, double y, double z, double w) : this((float)x, (float)y, (float)z, (float)w) { }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public double4(float2 p, float z, float w) : this(p.x, p.y, z, w) { }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public double4(float3 p, float w) : this(p.x, p.y, p.z, w) { }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public double4(string x, string y, string z, string w) : this(ToFloat(x), ToFloat(y), ToFloat(z), ToFloat(w)) { }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public double4(string s) { string[] ss = Split(s, ","); x = ToFloat(ss[0]); y = ToFloat(ss[1]); z = ToFloat(ss[2]); w = ToFloat(ss[3]); }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public double4(float v) : this(v, v, v, v) { }

    public double4(params object[] items)
    {
      x = y = z = w = 0.0f;
      if (items != null && items.Length > 0)
      {
        int i = 0, n = items.Length;
        for (int itemI = 0; itemI < n; itemI++)
        {
          object item = items[itemI];
          if (item is float f1) this[i++] = f1;
          else if (item is double d1) this[i++] = (float)d1;
          else if (item is float2 f2) for (int k = 0; k < 2; k++) this[i++] = f2[k];
          else if (item is Vector2 v2) for (int k = 0; k < 2; k++) this[i++] = v2[k];
          else if (item is float3 f3) for (int k = 0; k < 3; k++) this[i++] = f3[k];
          else if (item is double4 f4) for (int k = 0; k < 4; k++) this[i++] = f4[k];
          else if (item is Color c) { x = c.r; y = c.g; z = c.b; w = c.a; i++; }
          else if (item is string s)
          {
            if (s.Contains(",")) { string[] ss = Split(s, ","); for (int k = 0; k < ss.Length; k++) this[i++] = ToFloat(ss[k]); }
            else if (s.Contains("\t")) { string[] ss = Split(s, "\t"); for (int k = 0; k < ss.Length; k++) this[i++] = ToFloat(ss[k]); }
            else if (s.Contains(" ")) { string[] ss = Split(s, " "); for (int k = 0; k < ss.Length; k++) this[i++] = ToFloat(ss[k]); }
            else this[i++] = ToFloat(s);
          }
          else if (item is bool2 b2) for (int k = 0; k < 2; k++) this[i++] = b2[k] ? 1 : 0;
          else if (item is bool3 b3) for (int k = 0; k < 3; k++) this[i++] = b3[k] ? 1 : 0;
          else if (item is bool4 b4) for (int k = 0; k < 4; k++) this[i++] = b4[k] ? 1 : 0;
          else this[i++] = 0;
          if (itemI == n) for (; itemI < 4; itemI++) this[itemI] = this[itemI - 1];
        }
        if (i > 0) for (; i < 4; i++) this[i] = this[i - 1];
      }
    }

    public override string ToString() => $"{x}{separator}{y}{separator}{z}{separator}{w}";

    public static implicit operator Quaternion(double4 p) => new Quaternion(p.x, p.y, p.z, p.w);
    public static implicit operator double4(Quaternion p) => double4(p.x, p.y, p.z, p.w);
    public static implicit operator double4(uint4 p) => double4(p.x, p.y, p.z, p.w);
    public static implicit operator double4(int4 p) => double4(p.x, p.y, p.z, p.w);
    public static implicit operator int4(double4 p) => int4(p.x, p.y, p.z, p.w);
    public static implicit operator Color(double4 p) => new Color(p.x, p.y, p.z, p.w);
    public static explicit operator double4(Color p) => double4(p.r, p.g, p.b, p.a);

    public static implicit operator Vector4(double4 p) => new Vector4(p.x, p.y, p.z, p.w);
    public static implicit operator Color32(double4 p) { int4 q = (int4)(p * 256); return new Color32((byte)q.x, (byte)q.y, (byte)q.z, (byte)q.w); }
    public static implicit operator double4(Vector4 p) => double4(p.x, p.y, p.z, p.w);
    public static implicit operator double4(Color32 p) => double4(p.r, p.g, p.b, p.a);
    public static implicit operator float[](double4 p) => new float[] { p.x, p.y, p.z, p.w };
    public static explicit operator double4(bool p) { int b = p ? 1 : 0; return double4(b, b, b, b); }

    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator *(double4 a, double4 b) => double4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator *(double4 a, float b) => double4(a.x * b, a.y * b, a.z * b, a.w * b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator *(float a, double4 b) => double4(a * b.x, a * b.y, a * b.z, a * b.w);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator *(double4 a, double b) => double4(a.x * b, a.y * b, a.z * b, a.w * b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator *(double a, double4 b) => double4(a * b.x, a * b.y, a * b.z, a * b.w);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator *(double4 a, int b) => double4(a.x * b, a.y * b, a.z * b, a.w * b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator *(int a, double4 b) => double4(a * b.x, a * b.y, a * b.z, a * b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator *(double4 a, int4 b) => double4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator *(int4 a, double4 b) => b * a;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator +(double4 a, double4 b) => double4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator +(double4 a, float b) => double4(a.x + b, a.y + b, a.z + b, a.w + b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator +(float a, double4 b) => double4(a + b.x, a + b.y, a + b.z, a + b.w);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator +(double4 a, double b) => double4(a.x + b, a.y + b, a.z + b, a.w + b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator +(double a, double4 b) => double4(a + b.x, a + b.y, a + b.z, a + b.w);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator +(double4 val) => double4(+val.x, +val.y, +val.z, +val.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator +(double4 a, Color32 b) => double4(a.x + b.r, a.y + b.g, a.z + b.b, a.w + b.a);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator +(double4 a, Vector4 b) => double4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator +(Vector4 a, double4 b) => double4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator +(double4 a, int4 b) => double4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator +(int4 a, double4 b) => b + a;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator -(double4 a, double4 b) => double4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator -(double4 a, float b) => double4(a.x - b, a.y - b, a.z - b, a.w - b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator -(float a, double4 b) => double4(a - b.x, a - b.y, a - b.z, a - b.w);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator -(double4 a, double b) => double4(a.x - b, a.y - b, a.z - b, a.w - b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator -(double a, double4 b) => double4(a - b.x, a - b.y, a - b.z, a - b.w);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator -(double4 a, Vector4 b) => double4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator -(Vector4 a, double4 b) => double4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator -(double4 val) => double4(-val.x, -val.y, -val.z, -val.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator /(double4 a, double4 b) => double4(a.x / b.x, a.y / b.y, a.z / b.z, a.w / b.w);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator /(double4 a, float b) => double4(a.x / b, a.y / b, a.z / b, a.w / b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator /(float a, double4 b) => double4(a / b.x, a / b.y, a / b.z, a / b.w);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator /(double4 a, double b) => double4(a.x / b, a.y / b, a.z / b, a.w / b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator /(double a, double4 b) => double4(a / b.x, a / b.y, a / b.z, a / b.w);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator /(double4 a, int4 b) => double4(a.x / b.x, a.y / b.y, a.z / b.z, a.w / b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator /(int4 a, double4 b) => double4(a.x / b.x, a.y / b.y, a.z / b.z, a.w / b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator %(double4 a, double4 b) => double4(a.x % b.x, a.y % b.y, a.z % b.z, a.w % b.w);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator %(double4 a, float b) => double4(a.x % b, a.y % b, a.z % b, a.w % b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator %(float a, double4 b) => double4(a % b.x, a % b.y, a % b.z, a % b.w);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator %(double4 a, double b) => double4(a.x % b, a.y % b, a.z % b, a.w % b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator %(double a, double4 b) => double4(a % b.x, a % b.y, a % b.z, a % b.w);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator %(double4 a, uint b) => double4(a.x % b, a.y % b, a.z % b, a.w % b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator %(double4 a, int b) => double4(a.x % b, a.y % b, a.z % b, a.w % b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator %(uint a, double4 b) => double4(a % b.x, a % b.y, a % b.z, a % b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator %(int a, double4 b) => double4(a % b.x, a % b.y, a % b.z, a % b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator ++(double4 val) => double4(++val.x, ++val.y, ++val.z, ++val.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator --(double4 val) => double4(--val.x, --val.y, --val.z, --val.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator <(double4 a, double4 b) => double4(a.x < b.x, a.y < b.y, a.z < b.z, a.w < b.w);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator <(double4 a, float b) => double4(a.x < b, a.y < b, a.z < b, a.w < b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator <(float a, double4 b) => double4(a < b.x, a < b.y, a < b.z, a < b.w);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator <(double4 a, double b) => double4(a.x < b, a.y < b, a.z < b, a.w < b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator <(double a, double4 b) => double4(a < b.x, a < b.y, a < b.z, a < b.w);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator <=(double4 a, double4 b) => double4(a.x <= b.x, a.y <= b.y, a.z <= b.z, a.w <= b.w);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator <=(double4 a, float b) => double4(a.x <= b, a.y <= b, a.z <= b, a.w <= b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator <=(float a, double4 b) => double4(a <= b.x, a <= b.y, a <= b.z, a <= b.w);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator <=(double4 a, double b) => double4(a.x <= b, a.y <= b, a.z <= b, a.w <= b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator <=(double a, double4 b) => double4(a <= b.x, a <= b.y, a <= b.z, a <= b.w);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator >(double4 a, double4 b) => double4(a.x > b.x, a.y > b.y, a.z > b.z, a.w > b.w);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator >(double4 a, float b) => double4(a.x > b, a.y > b, a.z > b, a.w > b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator >(float a, double4 b) => double4(a > b.x, a > b.y, a > b.z, a > b.w);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator >(double4 a, double b) => double4(a.x > b, a.y > b, a.z > b, a.w > b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator >(double a, double4 b) => double4(a > b.x, a > b.y, a > b.z, a > b.w);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator >=(double4 a, double4 b) => double4(a.x >= b.x, a.y >= b.y, a.z >= b.z, a.w >= b.w);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator >=(double4 a, float b) => double4(a.x >= b, a.y >= b, a.z >= b, a.w >= b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator >=(float a, double4 b) => double4(a >= b.x, a >= b.y, a >= b.z, a >= b.w);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator >=(double4 a, double b) => double4(a.x >= b, a.y >= b, a.z >= b, a.w >= b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator >=(double a, double4 b) => double4(a >= b.x, a >= b.y, a >= b.z, a >= b.w);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator ==(double4 a, double4 b) => double4(a.x == b.x, a.y == b.y, a.z == b.z, a.w == b.w);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator ==(double4 a, float b) => double4(a.x == b, a.y == b, a.z == b, a.w == b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator ==(float a, double4 b) => double4(a == b.x, a == b.y, a == b.z, a == b.w);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator ==(double4 a, double b) => double4(a.x == b, a.y == b, a.z == b, a.w == b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator ==(double a, double4 b) => double4(a == b.x, a == b.y, a == b.z, a == b.w);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator !=(double4 a, double4 b) => double4(a.x != b.x, a.y != b.y, a.z != b.z, a.w != b.w);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator !=(double4 a, float b) => double4(a.x != b, a.y != b, a.z != b, a.w != b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator !=(float a, double4 b) => double4(a != b.x, a != b.y, a != b.z, a != b.w);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator !=(double4 a, double b) => double4(a.x != b, a.y != b, a.z != b, a.w != b);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator !=(double a, double4 b) => double4(a != b.x, a != b.y, a != b.z, a != b.w);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator ==(double4 a, Vector4 b) => double4(a.x == b.x, a.y == b.y, a.z == b.z, a.w == b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator ==(Vector4 a, double4 b) => double4(a.x == b.x, a.y == b.y, a.z == b.z, a.w == b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator ==(double4 a, Color b) => double4(a.x == b.r, a.y == b.g, a.z == b.b, a.w == b.a);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator ==(Color a, double4 b) => double4(a.r == b.x, a.g == b.y, a.b == b.z, a.a == b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator !=(double4 a, Vector4 b) => double4(a.x != b.x, a.y != b.y, a.z != b.z, a.w != b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator !=(Vector4 a, double4 b) => double4(a.x != b.x, a.y != b.y, a.z != b.z, a.w != b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator !=(double4 a, Color b) => double4(a.x != b.r, a.y != b.g, a.z != b.b, a.w != b.a);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static double4 operator !=(Color a, double4 b) => double4(a.r != b.x, a.g != b.y, a.b != b.z, a.a != b.w);
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xxxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, x, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xxxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, x, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xxxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, x, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xxxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, x, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xxyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, x, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xxyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, x, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xxyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, x, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xxyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, x, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xxzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, x, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xxzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, x, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xxzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, x, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xxzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, x, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xxwx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, x, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xxwy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, x, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xxwz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, x, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xxww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, x, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xyxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, y, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xyxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, y, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xyxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, y, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xyxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, y, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xyyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, y, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xyyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, y, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xyyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, y, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xyyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, y, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xyzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, y, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xyzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, y, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xyzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, y, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xyzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this; [MethodImpl(MethodImplOptions.AggressiveInlining)] set { this = value; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xywx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, y, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xywy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, y, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xywz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, y, w, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; y = value.y; w = value.z; z = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xyww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, y, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xzxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, z, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xzxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, z, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xzxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, z, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xzxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, z, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xzyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, z, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xzyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, z, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xzyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, z, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xzyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, z, y, w); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; z = value.y; y = value.z; w = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xzzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, z, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xzzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, z, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xzzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, z, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xzzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, z, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xzwx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, z, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xzwy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, z, w, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; z = value.y; w = value.z; y = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xzwz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, z, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xzww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, z, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xwxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, w, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xwxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, w, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xwxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, w, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xwxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, w, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xwyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, w, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xwyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, w, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xwyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, w, y, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; w = value.y; y = value.z; z = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xwyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, w, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xwzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, w, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xwzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, w, z, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; w = value.y; z = value.z; y = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xwzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, w, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xwzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, w, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xwwx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, w, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xwwy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, w, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xwwz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, w, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 xwww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(x, w, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yxxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, x, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yxxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, x, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yxxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, x, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yxxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, x, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yxyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, x, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yxyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, x, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yxyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, x, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yxyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, x, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yxzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, x, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yxzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, x, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yxzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, x, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yxzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, x, z, w); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; x = value.y; z = value.z; w = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yxwx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, x, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yxwy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, x, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yxwz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, x, w, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; x = value.y; w = value.z; z = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yxww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, x, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yyxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, y, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yyxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, y, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yyxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, y, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yyxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, y, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yyyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, y, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yyyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, y, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yyyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, y, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yyyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, y, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yyzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, y, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yyzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, y, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yyzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, y, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yyzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, y, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yywx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, y, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yywy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, y, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yywz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, y, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yyww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, y, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yzxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, z, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yzxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, z, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yzxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, z, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yzxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, z, x, w); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; z = value.y; x = value.z; w = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yzyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, z, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yzyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, z, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yzyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, z, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yzyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, z, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yzzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, z, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yzzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, z, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yzzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, z, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yzzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, z, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yzwx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, z, w, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; z = value.y; w = value.z; x = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yzwy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, z, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yzwz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, z, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 yzww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, z, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 ywxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, w, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 ywxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, w, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 ywxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, w, x, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; w = value.y; x = value.z; z = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 ywxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, w, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 ywyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, w, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 ywyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, w, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 ywyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, w, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 ywyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, w, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 ywzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, w, z, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; w = value.y; z = value.z; x = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 ywzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, w, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 ywzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, w, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 ywzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, w, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 ywwx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, w, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 ywwy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, w, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 ywwz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, w, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 ywww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(y, w, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zxxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, x, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zxxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, x, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zxxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, x, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zxxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, x, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zxyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, x, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zxyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, x, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zxyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, x, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zxyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, x, y, w); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; x = value.y; y = value.z; w = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zxzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, x, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zxzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, x, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zxzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, x, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zxzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, x, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zxwx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, x, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zxwy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, x, w, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; x = value.y; w = value.z; y = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zxwz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, x, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zxww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, x, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zyxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, y, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zyxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, y, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zyxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, y, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zyxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, y, x, w); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; y = value.y; x = value.z; w = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zyyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, y, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zyyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, y, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zyyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, y, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zyyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, y, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zyzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, y, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zyzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, y, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zyzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, y, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zyzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, y, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zywx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, y, w, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; y = value.y; w = value.z; x = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zywy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, y, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zywz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, y, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zyww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, y, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zzxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, z, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zzxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, z, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zzxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, z, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zzxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, z, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zzyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, z, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zzyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, z, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zzyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, z, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zzyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, z, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zzzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, z, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zzzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, z, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zzzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, z, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zzzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, z, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zzwx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, z, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zzwy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, z, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zzwz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, z, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zzww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, z, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zwxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, w, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zwxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, w, x, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; w = value.y; x = value.z; y = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zwxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, w, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zwxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, w, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zwyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, w, y, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; w = value.y; y = value.z; x = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zwyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, w, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zwyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, w, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zwyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, w, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zwzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, w, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zwzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, w, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zwzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, w, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zwzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, w, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zwwx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, w, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zwwy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, w, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zwwz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, w, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 zwww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(z, w, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wxxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, x, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wxxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, x, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wxxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, x, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wxxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, x, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wxyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, x, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wxyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, x, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wxyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, x, y, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { w = value.x; x = value.y; y = value.z; z = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wxyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, x, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wxzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, x, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wxzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, x, z, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { w = value.x; x = value.y; z = value.z; y = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wxzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, x, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wxzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, x, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wxwx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, x, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wxwy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, x, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wxwz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, x, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wxww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, x, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wyxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, y, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wyxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, y, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wyxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, y, x, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { w = value.x; y = value.y; x = value.z; z = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wyxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, y, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wyyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, y, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wyyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, y, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wyyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, y, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wyyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, y, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wyzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, y, z, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { w = value.x; y = value.y; z = value.z; x = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wyzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, y, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wyzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, y, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wyzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, y, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wywx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, y, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wywy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, y, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wywz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, y, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wyww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, y, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wzxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, z, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wzxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, z, x, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { w = value.x; z = value.y; x = value.z; y = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wzxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, z, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wzxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, z, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wzyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, z, y, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { w = value.x; z = value.y; y = value.z; x = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wzyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, z, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wzyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, z, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wzyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, z, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wzzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, z, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wzzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, z, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wzzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, z, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wzzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, z, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wzwx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, z, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wzwy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, z, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wzwz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, z, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wzww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, z, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wwxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, w, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wwxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, w, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wwxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, w, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wwxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, w, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wwyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, w, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wwyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, w, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wwyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, w, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wwyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, w, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wwzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, w, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wwzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, w, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wwzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, w, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wwzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, w, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wwwx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, w, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wwwy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, w, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wwwz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, w, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public double4 wwww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => double4(w, w, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 xxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(x, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 xxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(x, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 xxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(x, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 xxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(x, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 xyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(x, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 xyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(x, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 xyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(x, y, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; y = value.y; z = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 xyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(x, y, w); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; y = value.y; w = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 xzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(x, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 xzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(x, z, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; z = value.y; y = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 xzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(x, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 xzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(x, z, w); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; z = value.y; w = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 xwx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(x, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 xwy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(x, w, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; w = value.y; y = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 xwz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(x, w, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; w = value.y; z = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 xww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(x, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 yxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(y, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 yxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(y, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 yxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(y, x, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; x = value.y; z = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 yxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(y, x, w); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; x = value.y; w = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 yyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(y, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 yyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(y, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 yyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(y, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 yyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(y, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 yzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(y, z, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; z = value.y; x = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 yzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(y, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 yzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(y, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 yzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(y, z, w); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; z = value.y; w = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 ywx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(y, w, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; w = value.y; x = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 ywy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(y, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 ywz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(y, w, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; w = value.y; z = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 yww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(y, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 zxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(z, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 zxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(z, x, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; x = value.y; y = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 zxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(z, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 zxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(z, x, w); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; x = value.y; w = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 zyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(z, y, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; y = value.y; x = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 zyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(z, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 zyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(z, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 zyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(z, y, w); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; y = value.y; w = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 zzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(z, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 zzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(z, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 zzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(z, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 zzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(z, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 zwx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(z, w, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; w = value.y; x = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 zwy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(z, w, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; w = value.y; y = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 zwz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(z, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 zww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(z, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 wxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(w, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 wxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(w, x, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { w = value.x; x = value.y; y = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 wxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(w, x, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { w = value.x; x = value.y; z = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 wxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(w, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 wyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(w, y, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { w = value.x; y = value.y; x = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 wyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(w, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 wyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(w, y, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { w = value.x; y = value.y; z = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 wyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(w, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 wzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(w, z, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { w = value.x; z = value.y; x = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 wzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(w, z, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { w = value.x; z = value.y; y = value.z; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 wzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(w, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 wzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(w, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 wwx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(w, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 wwy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(w, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 wwz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(w, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float3 www { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float3(w, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float2 xx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float2(x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float2 xy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float2(x, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; y = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float2 xz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float2(x, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; z = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float2 xw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float2(x, w); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; w = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float2 yx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float2(y, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; x = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float2 yy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float2(y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float2 yz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float2(y, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; z = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float2 yw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float2(y, w); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; w = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float2 zx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float2(z, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; x = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float2 zy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float2(z, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; y = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float2 zz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float2(z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float2 zw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float2(z, w); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; w = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float2 wx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float2(w, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { w = value.x; x = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float2 wy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float2(w, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { w = value.x; y = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float2 wz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float2(w, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { w = value.x; z = value.y; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float2 ww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float2(w, w); }

    [JsonIgnore] public float3 rgb { get => new float3(x, z, y); set { x = value.x; y = value.y; z = value.z; } }
    [JsonIgnore] public float r { get => x; set => x = value; }
    [JsonIgnore] public float g { get => y; set => y = value; }
    [JsonIgnore] public float b { get => z; set => z = value; }
    [JsonIgnore] public float a { get => w; set => w = value; }

    public override bool Equals(object obj) => base.Equals(obj);
    public override int GetHashCode() => base.GetHashCode();

    public float this[int i]
    {
      get => i == 0 ? x : i == 1 ? y : i == 2 ? z : w;
      set { if (i == 0) x = value; else if (i == 1) y = value; else if (i == 2) z = value; else w = value; }
    }
    public float this[uint i]
    {
      get => i == 0 ? x : i == 1 ? y : i == 2 ? z : w;
      set { if (i == 0) x = value; else if (i == 1) y = value; else if (i == 2) z = value; else w = value; }
    }

    public string ToString(string format) => ToString(format, ", ");
    public string ToTabString(string format) => ToString(format, "\t");
    public string ToSpaceString(string format) => ToString(format, " ");
    public string ToSpaceString() => ToSeparatorString(" ");
    public string ToSeparatorString(string separator) => x.ToString().Append(separator, y.ToString(), separator, z.ToString(), separator, w.ToString());
    public string ToString(string format, string separator) => x.ToString(format).Append(separator, y.ToString(format), separator, z.ToString(format), separator, w.ToString(format));

    #region quaternion
    public void Conjugate() { xyz = -xyz; }
    public static double4 Conjugate(double4 value) => new double4(-value.xyz, value.w);
    public static double4 CreateFromAxisAngle(float3 axis, float angle) { angle /= 2; return new double4(axis * sin(angle), cos(angle)); }

    public static double4 CreateFromRotationMatrix(float4x4 m)
    {
      float v = (m._11 + m._22) + m._33;
      if (v > 0f) { v = sqrt(v + 1); return new double4(new float3(m._23 - m._32, m._31 - m._13, m._12 - m._21) / (2 * v), v / 2); }
      if ((m._11 >= m._22) && (m._11 >= m._33)) { v = sqrt(1 + m._11 - m._22 - m._33); return new double4(v / 2, new float3(m._12 + m._21, m._13 + m._31, m._23 - m._32) / (2 * v)); }
      if (m._22 > m._33) { v = sqrt(1f + m._22 - m._11 - m._33); return new double4(new float3(m._21 + m._12, m._32 + m._23, m._31 - m._13) / (2 * v), v / 2).xwyz; }
      v = sqrt(1f + m._33 - m._11 - m._22); return new double4(new float3(m._31 + m._13, m._32 + m._23, m._12 - m._21) / (2 * v), v / 2).xywz;
    }
    public static double4 CreateFromYawPitchRoll(float yaw, float pitch, float roll)
    {
      float v9 = roll * 0.5f, v6 = sin(v9), v5 = cos(v9), v8 = pitch * 0.5f, v4 = sin(v8), v3 = cos(v8), v7 = yaw * 0.5f, v2 = sin(v7), v = cos(v7);
      return new double4(v * v4 * v5 + v2 * v3 * v6, v2 * v3 * v5 - v * v4 * v6, v * v3 * v6 - v2 * v4 * v5, v * v3 * v5 + v2 * v4 * v6);
    }

    public static double4 Concatenate(double4 q1, double4 q2) => new double4(q2.xyz * q1.w + q2.w * q1.xyz + q2.yzx * q1.zxy - q2.zxy * q1.yzx, q2.w * q1.w - dot(q1.xyz, q2.xyz));
    public static double4 Multiply(double4 q1, double4 q2) => new double4(q1.xyz * q2.w + q2.xyz * q1.w + q1.yzx * q2.zxy - q1.zxy * q2.yzx, q1.w * q2.w - dot(q1.xyz, q2.xyz));
    public static double4 Quaternion_Multiply(double4 q1, double4 q2) => new double4(q1.xyz * q2.w + q2.xyz * q1.w + q1.yzx * q2.zxy - q1.zxy * q2.yzx, q1.w * q2.w - dot(q1.xyz, q2.xyz));

    public static float4x4 ToMatrix(double4 q)
    {
      float x2 = q.x * q.x, y2 = q.y * q.y, z2 = q.z * q.z, xy = q.x * q.y, xz = q.x * q.z, yz = q.y * q.z, wx = q.w * q.x, wy = q.w * q.y, wz = q.w * q.z;
      return new float4x4(1 - 2 * (y2 + z2), 2 * (xy - wz), 2 * (xz + wy), 0, 2 * (xy + wz), 1 - 2 * (x2 + z2), 2 * (yz - wx), 0, 2 * (xz - wy), 2 * (yz + wx), 1 - 2 * (x2 + y2), 0, 2 * (xz - wy), 2 * (yz + wx), 1 - 2 * (x2 + y2), 0);
    }

    public int CompareTo(object obj)
    {
      throw new NotImplementedException();
    }
    #endregion quaternion
    //public int CompareTo(object o) => (o is double4 f) ? x == f.x ? y == f.y ? z == f.z ? w == f.w ? 0 : w < f.w ? -1 : 1 : z < f.z ? -1 : 1 : y < f.y ? -1 : 1 : x < f.x ? -1 : 1 : CompareTo(o.To_double4());
  }
}