// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC
using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using static GpuScript.GS;

namespace GpuScript
{
  [System.Serializable]
  public struct float4 // : I_float4
  {
    public float x, y, z, w;

    [MethodImpl(MethodImplOptions.AggressiveInlining)] public float4(float x, float y, float z, float w) { this.x = x; this.y = y; this.z = z; this.w = w; }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public float4(bool x, bool y, bool z, bool w) { this.x = x ? 1 : 0; this.y = y ? 1 : 0; this.z = z ? 1 : 0; this.w = w ? 1 : 0; }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public float4(Color c) : this(c.r, c.g, c.b, c.a) { }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public float4(double x, double y, double z, double w) : this((float)x, (float)y, (float)z, (float)w) { }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public float4(float2 p, float z, float w) : this(p.x, p.y, z, w) { }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public float4(float3 p, float w) : this(p.x, p.y, p.z, w) { }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public float4(string x, string y, string z, string w) : this(ToFloat(x), ToFloat(y), ToFloat(z), ToFloat(w)) { }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public float4(string s) { string[] ss = Split(s, ","); int n = ss.Length; x = ToFloat(ss[0]); y = n < 2 ? x : ToFloat(ss[1]); z = n < 3 ? y : ToFloat(ss[2]); w = n < 4 ? z : ToFloat(ss[3]); }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public float4(float v) : this(v, v, v, v) { }

    public float4(params object[] items)
    {
      x = y = z = w = 0.0f;
      if (items != null && items.Length > 0)
      {
        int i = 0, n = items.Length;
        for (int itemI = 0; itemI < n; itemI++)
        {
          object item = items[itemI];
          if (item is float f1) this[i++] = f1;
          else if (item is int i1) this[i++] = i1;
          else if (item is uint u1) this[i++] = u1;
          else if (item is double d1) this[i++] = (float)d1;
          else if (item is float2 f2) for (int k = 0; k < 2; k++) this[i++] = f2[k];
          else if (item is Vector2 v2) for (int k = 0; k < 2; k++) this[i++] = v2[k];
          else if (item is float3 f3) for (int k = 0; k < 3; k++) this[i++] = f3[k];
          else if (item is float4 f4) for (int k = 0; k < 4; k++) this[i++] = f4[k];
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

    public static implicit operator Quaternion(float4 p) => new Quaternion(p.x, p.y, p.z, p.w);
    public static implicit operator float4(Quaternion p) => float4(p.x, p.y, p.z, p.w);
    public static implicit operator float4(uint4 p) => float4(p.x, p.y, p.z, p.w);
    public static implicit operator float4(int4 p) => float4(p.x, p.y, p.z, p.w);
    public static implicit operator Color(float4 p) => new Color(p.x, p.y, p.z, p.w);
    public static explicit operator float4(Color p) => float4(p.r, p.g, p.b, p.a);

    public static implicit operator Rect(float4 p) => new Rect(p.x, p.y, p.z, p.w);
    public static implicit operator float4(Rect p) => float4(p.x, p.y, p.width, p.height);
    public static implicit operator Vector4(float4 p) => new Vector4(p.x, p.y, p.z, p.w);
    public static implicit operator float4(Vector4 p) => float4(p.x, p.y, p.z, p.w);
    public static implicit operator Color32(float4 p) { int4 q = (int4)(p * 256); return new Color32((byte)q.x, (byte)q.y, (byte)q.z, (byte)q.w); }
    public static implicit operator float4(Color32 p) => float4(p.r, p.g, p.b, p.a);
    public static implicit operator float[](float4 p) => new float[] { p.x, p.y, p.z, p.w };
    public static explicit operator float4(bool p) { int b = p ? 1 : 0; return float4(b, b, b, b); }

    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator *(float4 a, float4 b) => float4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator *(float4 a, float b) => float4(a.x * b, a.y * b, a.z * b, a.w * b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator *(float a, float4 b) => float4(a * b.x, a * b.y, a * b.z, a * b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator *(float4 a, int b) => float4(a.x * b, a.y * b, a.z * b, a.w * b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator *(int a, float4 b) => float4(a * b.x, a * b.y, a * b.z, a * b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator *(float4 a, int4 b) => float4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator *(int4 a, float4 b) => b * a;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator +(float4 a, float4 b) => float4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator +(float4 a, float b) => float4(a.x + b, a.y + b, a.z + b, a.w + b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator +(float a, float4 b) => float4(a + b.x, a + b.y, a + b.z, a + b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator +(float4 val) => float4(+val.x, +val.y, +val.z, +val.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator +(float4 a, Color32 b) => float4(a.x + b.r, a.y + b.g, a.z + b.b, a.w + b.a);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator +(float4 a, Vector4 b) => float4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator +(Vector4 a, float4 b) => float4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator +(float4 a, int4 b) => float4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator +(int4 a, float4 b) => b + a;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator -(float4 a, float4 b) => float4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator -(float4 a, float b) => float4(a.x - b, a.y - b, a.z - b, a.w - b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator -(float a, float4 b) => float4(a - b.x, a - b.y, a - b.z, a - b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator -(float4 a, Vector4 b) => float4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator -(Vector4 a, float4 b) => float4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator -(float4 val) => float4(-val.x, -val.y, -val.z, -val.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator /(float4 a, float4 b) => float4(a.x / b.x, a.y / b.y, a.z / b.z, a.w / b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator /(float4 a, float b) => float4(a.x / b, a.y / b, a.z / b, a.w / b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator /(float a, float4 b) => float4(a / b.x, a / b.y, a / b.z, a / b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator /(float4 a, int4 b) => float4(a.x / b.x, a.y / b.y, a.z / b.z, a.w / b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator /(int4 a, float4 b) => float4(a.x / b.x, a.y / b.y, a.z / b.z, a.w / b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator %(float4 a, float4 b) => float4(a.x % b.x, a.y % b.y, a.z % b.z, a.w % b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator %(float4 a, float b) => float4(a.x % b, a.y % b, a.z % b, a.w % b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator %(float a, float4 b) => float4(a % b.x, a % b.y, a % b.z, a % b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator %(float4 a, uint b) => float4(a.x % b, a.y % b, a.z % b, a.w % b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator %(float4 a, int b) => float4(a.x % b, a.y % b, a.z % b, a.w % b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator %(uint a, float4 b) => float4(a % b.x, a % b.y, a % b.z, a % b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator %(int a, float4 b) => float4(a % b.x, a % b.y, a % b.z, a % b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator ++(float4 val) => float4(++val.x, ++val.y, ++val.z, ++val.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator --(float4 val) => float4(--val.x, --val.y, --val.z, --val.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator <(float4 a, float4 b) => float4(a.x < b.x, a.y < b.y, a.z < b.z, a.w < b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator <(float4 a, float b) => float4(a.x < b, a.y < b, a.z < b, a.w < b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator <(float a, float4 b) => float4(a < b.x, a < b.y, a < b.z, a < b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator <=(float4 a, float4 b) => float4(a.x <= b.x, a.y <= b.y, a.z <= b.z, a.w <= b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator <=(float4 a, float b) => float4(a.x <= b, a.y <= b, a.z <= b, a.w <= b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator <=(float a, float4 b) => float4(a <= b.x, a <= b.y, a <= b.z, a <= b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator >(float4 a, float4 b) => float4(a.x > b.x, a.y > b.y, a.z > b.z, a.w > b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator >(float4 a, float b) => float4(a.x > b, a.y > b, a.z > b, a.w > b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator >(float a, float4 b) => float4(a > b.x, a > b.y, a > b.z, a > b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator >=(float4 a, float4 b) => float4(a.x >= b.x, a.y >= b.y, a.z >= b.z, a.w >= b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator >=(float4 a, float b) => float4(a.x >= b, a.y >= b, a.z >= b, a.w >= b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator >=(float a, float4 b) => float4(a >= b.x, a >= b.y, a >= b.z, a >= b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator ==(float4 a, float4 b) => float4(a.x == b.x, a.y == b.y, a.z == b.z, a.w == b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator ==(float4 a, float b) => float4(a.x == b, a.y == b, a.z == b, a.w == b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator ==(float a, float4 b) => float4(a == b.x, a == b.y, a == b.z, a == b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator !=(float4 a, float4 b) => float4(a.x != b.x, a.y != b.y, a.z != b.z, a.w != b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator !=(float4 a, float b) => float4(a.x != b, a.y != b, a.z != b, a.w != b);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator !=(float a, float4 b) => float4(a != b.x, a != b.y, a != b.z, a != b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator ==(float4 a, Vector4 b) => float4(a.x == b.x, a.y == b.y, a.z == b.z, a.w == b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator ==(Vector4 a, float4 b) => float4(a.x == b.x, a.y == b.y, a.z == b.z, a.w == b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator ==(float4 a, Color b) => float4(a.x == b.r, a.y == b.g, a.z == b.b, a.w == b.a);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator ==(Color a, float4 b) => float4(a.r == b.x, a.g == b.y, a.b == b.z, a.a == b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator !=(float4 a, Vector4 b) => float4(a.x != b.x, a.y != b.y, a.z != b.z, a.w != b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator !=(Vector4 a, float4 b) => float4(a.x != b.x, a.y != b.y, a.z != b.z, a.w != b.w);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator !=(float4 a, Color b) => float4(a.x != b.r, a.y != b.g, a.z != b.b, a.w != b.a);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float4 operator !=(Color a, float4 b) => float4(a.r != b.x, a.g != b.y, a.b != b.z, a.a != b.w);
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xxxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, x, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xxxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, x, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xxxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, x, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xxxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, x, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xxyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, x, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xxyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, x, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xxyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, x, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xxyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, x, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xxzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, x, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xxzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, x, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xxzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, x, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xxzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, x, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xxwx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, x, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xxwy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, x, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xxwz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, x, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xxww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, x, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xyxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, y, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xyxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, y, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xyxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, y, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xyxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, y, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xyyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, y, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xyyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, y, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xyyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, y, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xyyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, y, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xyzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, y, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xyzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, y, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xyzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, y, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xyzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this; [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this = value; }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xywx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, y, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xywy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, y, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xywz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, y, w, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; y = value.y; w = value.z; z = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xyww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, y, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xzxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, z, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xzxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, z, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xzxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, z, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xzxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, z, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xzyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, z, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xzyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, z, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xzyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, z, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xzyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, z, y, w); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; z = value.y; y = value.z; w = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xzzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, z, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xzzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, z, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xzzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, z, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xzzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, z, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xzwx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, z, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xzwy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, z, w, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; z = value.y; w = value.z; y = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xzwz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, z, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xzww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, z, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xwxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, w, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xwxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, w, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xwxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, w, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xwxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, w, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xwyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, w, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xwyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, w, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xwyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, w, y, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; w = value.y; y = value.z; z = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xwyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, w, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xwzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, w, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xwzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, w, z, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { x = value.x; w = value.y; z = value.z; y = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xwzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, w, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xwzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, w, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xwwx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, w, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xwwy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, w, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xwwz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, w, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 xwww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(x, w, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yxxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, x, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yxxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, x, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yxxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, x, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yxxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, x, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yxyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, x, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yxyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, x, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yxyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, x, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yxyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, x, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yxzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, x, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yxzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, x, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yxzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, x, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yxzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, x, z, w); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; x = value.y; z = value.z; w = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yxwx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, x, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yxwy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, x, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yxwz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, x, w, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; x = value.y; w = value.z; z = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yxww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, x, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yyxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, y, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yyxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, y, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yyxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, y, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yyxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, y, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yyyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, y, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yyyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, y, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yyyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, y, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yyyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, y, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yyzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, y, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yyzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, y, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yyzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, y, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yyzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, y, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yywx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, y, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yywy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, y, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yywz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, y, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yyww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, y, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yzxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, z, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yzxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, z, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yzxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, z, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yzxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, z, x, w); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; z = value.y; x = value.z; w = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yzyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, z, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yzyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, z, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yzyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, z, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yzyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, z, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yzzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, z, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yzzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, z, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yzzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, z, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yzzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, z, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yzwx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, z, w, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; z = value.y; w = value.z; x = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yzwy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, z, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yzwz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, z, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 yzww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, z, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 ywxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, w, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 ywxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, w, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 ywxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, w, x, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; w = value.y; x = value.z; z = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 ywxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, w, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 ywyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, w, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 ywyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, w, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 ywyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, w, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 ywyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, w, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 ywzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, w, z, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { y = value.x; w = value.y; z = value.z; x = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 ywzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, w, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 ywzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, w, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 ywzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, w, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 ywwx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, w, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 ywwy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, w, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 ywwz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, w, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 ywww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(y, w, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zxxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, x, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zxxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, x, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zxxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, x, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zxxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, x, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zxyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, x, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zxyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, x, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zxyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, x, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zxyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, x, y, w); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; x = value.y; y = value.z; w = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zxzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, x, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zxzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, x, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zxzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, x, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zxzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, x, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zxwx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, x, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zxwy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, x, w, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; x = value.y; w = value.z; y = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zxwz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, x, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zxww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, x, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zyxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, y, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zyxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, y, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zyxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, y, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zyxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, y, x, w); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; y = value.y; x = value.z; w = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zyyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, y, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zyyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, y, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zyyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, y, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zyyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, y, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zyzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, y, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zyzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, y, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zyzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, y, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zyzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, y, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zywx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, y, w, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; y = value.y; w = value.z; x = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zywy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, y, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zywz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, y, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zyww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, y, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zzxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, z, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zzxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, z, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zzxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, z, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zzxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, z, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zzyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, z, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zzyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, z, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zzyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, z, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zzyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, z, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zzzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, z, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zzzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, z, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zzzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, z, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zzzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, z, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zzwx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, z, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zzwy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, z, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zzwz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, z, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zzww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, z, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zwxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, w, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zwxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, w, x, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; w = value.y; x = value.z; y = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zwxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, w, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zwxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, w, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zwyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, w, y, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { z = value.x; w = value.y; y = value.z; x = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zwyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, w, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zwyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, w, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zwyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, w, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zwzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, w, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zwzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, w, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zwzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, w, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zwzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, w, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zwwx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, w, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zwwy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, w, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zwwz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, w, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 zwww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(z, w, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wxxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, x, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wxxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, x, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wxxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, x, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wxxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, x, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wxyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, x, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wxyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, x, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wxyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, x, y, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { w = value.x; x = value.y; y = value.z; z = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wxyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, x, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wxzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, x, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wxzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, x, z, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { w = value.x; x = value.y; z = value.z; y = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wxzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, x, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wxzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, x, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wxwx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, x, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wxwy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, x, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wxwz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, x, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wxww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, x, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wyxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, y, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wyxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, y, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wyxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, y, x, z); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { w = value.x; y = value.y; x = value.z; z = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wyxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, y, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wyyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, y, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wyyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, y, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wyyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, y, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wyyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, y, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wyzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, y, z, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { w = value.x; y = value.y; z = value.z; x = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wyzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, y, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wyzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, y, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wyzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, y, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wywx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, y, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wywy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, y, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wywz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, y, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wyww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, y, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wzxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, z, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wzxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, z, x, y); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { w = value.x; z = value.y; x = value.z; y = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wzxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, z, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wzxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, z, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wzyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, z, y, x); [MethodImpl(MethodImplOptions.AggressiveInlining)] set { w = value.x; z = value.y; y = value.z; x = value.w; } }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wzyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, z, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wzyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, z, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wzyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, z, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wzzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, z, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wzzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, z, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wzzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, z, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wzzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, z, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wzwx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, z, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wzwy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, z, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wzwz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, z, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wzww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, z, w, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wwxx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, w, x, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wwxy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, w, x, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wwxz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, w, x, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wwxw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, w, x, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wwyx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, w, y, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wwyy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, w, y, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wwyz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, w, y, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wwyw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, w, y, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wwzx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, w, z, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wwzy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, w, z, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wwzz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, w, z, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wwzw { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, w, z, w); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wwwx { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, w, w, x); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wwwy { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, w, w, y); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wwwz { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, w, w, z); }
    [JsonIgnore, EditorBrowsable(EditorBrowsableState.Never)] public float4 wwww { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(w, w, w, w); }
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

    [JsonIgnore] public float3 rgb { get => float3(x, z, y); set { x = value.x; y = value.y; z = value.z; } }
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

    public static float4 Identity { get => f0001; }

    public void Conjugate() { xyz = -xyz; }
    public static float4 Conjugate(float4 value) => float4(-value.xyz, value.w);
    public static float4 CreateFromAxisAngle(float3 axis, float angle) { angle /= 2; return float4(axis * sin(angle), cos(angle)); }

    public static float4 CreateFromRotationMatrix(float4x4 m)
    {
      float v = (m._11 + m._22) + m._33;
      if (v > 0f) { v = sqrt(v + 1); return float4(float3(m._23 - m._32, m._31 - m._13, m._12 - m._21) / (2 * v), v / 2); }
      if ((m._11 >= m._22) && (m._11 >= m._33)) { v = sqrt(1 + m._11 - m._22 - m._33); return float4(v / 2, float3(m._12 + m._21, m._13 + m._31, m._23 - m._32) / (2 * v)); }
      if (m._22 > m._33) { v = sqrt(1f + m._22 - m._11 - m._33); return float4(float3(m._21 + m._12, m._32 + m._23, m._31 - m._13) / (2 * v), v / 2).xwyz; }
      v = sqrt(1f + m._33 - m._11 - m._22); return float4(float3(m._31 + m._13, m._32 + m._23, m._12 - m._21) / (2 * v), v / 2).xywz;
    }
    public static float4 CreateFromYawPitchRoll(float yaw, float pitch, float roll)
    {
      float v9 = roll * 0.5f, v6 = sin(v9), v5 = cos(v9), v8 = pitch * 0.5f, v4 = sin(v8), v3 = cos(v8), v7 = yaw * 0.5f, v2 = sin(v7), v = cos(v7);
      return float4(v * v4 * v5 + v2 * v3 * v6, v2 * v3 * v5 - v * v4 * v6, v * v3 * v6 - v2 * v4 * v5, v * v3 * v5 + v2 * v4 * v6);
    }
    public static float4 Inverse(float4 q) => q * f___1 / dot(q, q);

    public static float4 Lerp(float4 q1, float4 q2, float v) { float v2 = 1f - v, v5 = dot(q1, q2); float4 q = v2 * q1 + (v5 >= 0 ? v : -v) * q2; return q / length(q); }

    public static float4 Slerp(float4 q1, float4 q2, float v)
    {
      float v2, v3, v4 = dot(q1, q2);
      bool flag = false;
      if (v4 < 0f) { flag = true; v4 = -v4; }
      if (v4 > 0.999999f) { v3 = 1f - v; v2 = flag ? -v : v; }
      else { float v5 = acos(v4), v6 = 1 / sin(v5); v3 = (sin(1 - v) * v5) * v6; v2 = flag ? ((-sin((v * v5))) * v6) : ((sin((v * v5))) * v6); }
      return v3 * q1 + v2 * q2;
    }

    public static float4 Concatenate(float4 q1, float4 q2) => float4(q2.xyz * q1.w + q2.w * q1.xyz + q2.yzx * q1.zxy - q2.zxy * q1.yzx, q2.w * q1.w - dot(q1.xyz, q2.xyz));
    public static float4 Multiply(float4 q1, float4 q2) => float4(q1.xyz * q2.w + q2.xyz * q1.w + q1.yzx * q2.zxy - q1.zxy * q2.yzx, q1.w * q2.w - dot(q1.xyz, q2.xyz));
    public static float4 Quaternion_Multiply(float4 q1, float4 q2) => float4(q1.xyz * q2.w + q2.xyz * q1.w + q1.yzx * q2.zxy - q1.zxy * q2.yzx, q1.w * q2.w - dot(q1.xyz, q2.xyz));

    public static float4 Divide(float4 q1, float4 q2)
    {
      float v14 = dot(q2, q2), v5 = 1f / v14, v4 = -q2.x * v5, v3 = -q2.y * v5, v2 = -q2.z * v5, v = q2.w * v5,
       v13 = q1.y * v2 - q1.z * v3, v12 = q1.z * v4 - q1.x * v2, v11 = q1.x * v3 - q1.y * v4, v10 = q1.x * v4 + q1.y * v3 + q1.z * v2;
      return float4(q1.x * v + v4 * q1.w + v13, q1.y * v + v3 * q1.w + v12, q1.z * v + v2 * q1.w + v11, q1.w * v - v10);
    }
    public static float4 Quaternion_Divide(float4 q1, float4 q2)
    {
      float v14 = dot(q2, q2), v5 = 1 / v14, v4 = -q2.x * v5, v3 = -q2.y * v5, v2 = -q2.z * v5, v = q2.w * v5;
      return float4(q1.x * v + v4 * q1.w + q1.y * v2 - q1.z * v3, q1.y * v + v3 * q1.w + q1.z * v4 - q1.x * v2, q1.z * v + v2 * q1.w + q1.x * v3 - q1.y * v4, q1.w * v - q1.x * v4 - q1.y * v3 - q1.z * v2);
    }

    public static float4x4 ToMatrix(float4 q)
    {
      float x2 = q.x * q.x, y2 = q.y * q.y, z2 = q.z * q.z, xy = q.x * q.y, xz = q.x * q.z, yz = q.y * q.z, wx = q.w * q.x, wy = q.w * q.y, wz = q.w * q.z;
      return float4x4(1 - 2 * (y2 + z2), 2 * (xy - wz), 2 * (xz + wy), 0, 2 * (xy + wz), 1 - 2 * (x2 + z2), 2 * (yz - wx), 0, 2 * (xz - wy), 2 * (yz + wx), 1 - 2 * (x2 + y2), 0, 2 * (xz - wy), 2 * (yz + wx), 1 - 2 * (x2 + y2), 0);
    }
    #endregion quaternion
  }

  [System.Serializable]
  public class Float4
  {
    public float4 a;
    public Float4() { }
    public Float4(float4 a) { this.a = a; }
    public Float4(float x, float y, float z, float w) : this(float4(x, y, z, w)) { }

    public float x { get => a.x; set => a.x = value; }
    public float y { get => a.y; set => a.y = value; }
    public float z { get => a.z; set => a.z = value; }
    public float w { get => a.w; set => a.w = value; }

    public static implicit operator float4(Float4 p) => float4(p.x, p.y, p.z, p.w);
    public static implicit operator Float4(float4 p) => new Float4(p.x, p.y, p.z, p.w);

    public float this[int i] { get => a[i]; set => a[i] = value; }

    public static Float4 operator +(Float4 a, Float4 b) => new Float4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
    public static Float4 operator +(Float4 a, Color32 b) => new Float4(a.x + b.r, a.y + b.g, a.z + b.b, a.w + b.a);
    public static Float4 operator +(Float4 a, Vector4 b) => new Float4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
    public static Float4 operator +(Vector4 a, Float4 b) => new Float4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
    public static Float4 operator +(Float4 a, int4 b) => new Float4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
    public static Float4 operator +(int4 a, Float4 b) => b + a;
    public static Float4 operator +(Float4 a, float b) => new Float4(a.x + b, a.y + b, a.z + b, a.w + b);
    public static Float4 operator +(float a, Float4 b) => b + a;

    public static Float4 operator -(Float4 a) => new Float4(-a.x, -a.y, -a.z, -a.w);
    public static Float4 operator -(Float4 a, Float4 b) => new Float4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
    public static Float4 operator -(Float4 a, Vector4 b) => new Float4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
    public static Float4 operator -(Vector4 a, Float4 b) => new Float4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
    public static Float4 operator -(Float4 a, float b) => new Float4(a.x - b, a.y - b, a.z - b, a.w - b);
    public static Float4 operator -(float a, Float4 b) => new Float4(a - b.x, a - b.y, a - b.z, a - b.w);

    public static Float4 operator *(Float4 a, Float4 b) => new Float4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
    public static Float4 operator *(Float4 a, int4 b) => new Float4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
    public static Float4 operator *(int4 a, Float4 b) => b * a;
    public static Float4 operator *(Float4 a, float b) => new Float4(a.x * b, a.y * b, a.z * b, a.w * b);
    public static Float4 operator *(float a, Float4 b) => b * a;

    public static Float4 operator /(Float4 a, Float4 b) => new Float4(a.x / b.x, a.y / b.y, a.z / b.z, a.w / b.w);
    public static Float4 operator /(Float4 a, int4 b) => new Float4(a.x / b.x, a.y / b.y, a.z / b.z, a.w / b.w);
    public static Float4 operator /(int4 a, Float4 b) => new Float4(a.x / b.x, a.y / b.y, a.z / b.z, a.w / b.w);
    public static Float4 operator /(float a, Float4 b) => new Float4(a / b.x, a / b.y, a / b.z, a / b.w);
    public static Float4 operator /(Float4 a, float b) => new Float4(a.x / b, a.y / b, a.z / b, a.w / b);
  }
}