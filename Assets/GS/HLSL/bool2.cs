// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC
using UnityEngine;
using static GpuScript.GS;
namespace GpuScript
{
  [System.Serializable]
  public struct bool2 // : I_bool2
  {
    public bool x, y;
    public int X { get => x ? 1 : 0; }
    public int Y { get => y ? 1 : 0; }

    public override string ToString() => $"{x}{separator}{y}";

    public bool2(bool _x, bool _y) { x = _x; y = _y; }
    public bool2(int _x, int _y) : this(_x != 0, _y != 0) { }
    public bool2(float x, float y) : this(roundi(x), roundi(y)) { }

    public static implicit operator bool[](bool2 p) => new bool[] { p.x, p.y };
    public bool this[int i] { get => i == 0 ? x : y; set { if (i == 0) x = value; else y = value; } }
    public static bool2 operator !(bool2 a) => bool2(!a.x, !a.y);


    public static bool2 operator +(bool2 a, bool2 b) => bool2(a.X + b.X, a.Y + b.Y);
    public static bool2 operator +(bool2 a, int b) => bool2(a.X + b, a.Y + b);
    public static bool2 operator -(bool2 a) => bool2(-a.X, -a.Y);
    public static bool2 operator -(bool2 a, bool2 b) => bool2(a.X - b.X, a.Y - b.Y);
    public static bool2 operator -(bool2 a, int b) => bool2(a.X - b, a.Y - b);
    public static bool2 operator -(int a, bool2 b) => bool2(a - b.X, a - b.Y);
    public static bool2 operator *(bool2 a, bool2 b) => bool2(a.X * b.X, a.Y * b.Y);
    public static bool2 operator *(bool2 a, int b) => bool2(a.X * b, a.Y * b);
    public static float2 operator *(bool2 a, float b) => float2(a.X * b, a.Y * b);
    public static float2 operator *(float b, bool2 a) => float2(a.X * b, a.Y * b);
    public static bool2 operator *(int a, bool2 b) => b * a;
    public static bool2 operator /(bool2 a, int b) => bool2(a.X / b, a.Y / b);
    public static float2 operator /(bool2 a, float b) => float2(a.X / b, a.Y / b);
    public static float2 operator /(bool2 a, bool2 b) => float2(a.X / (float)b.X, a.Y / (float)b.Y);
    public static float2 operator /(bool2 a, float2 b) => float2(a.X / b.x, a.Y / b.y);
    public static float2 operator /(Vector2 a, bool2 b) => float2(a.x / b.X, a.y / b.Y);
    public static bool2 operator /(int a, bool2 b) => bool2(a / b.X, a / b.Y);
    public static bool2 operator ==(bool2 a, bool2 b) => bool2(a.x == b.x ? 1 : 0, a.y == b.y ? 1 : 0);
    public static bool2 operator !=(bool2 a, bool2 b) => 1 - (a == b);
    public static bool2 operator ==(bool2 a, int b) => bool2(a.X == b ? 1 : 0, a.Y == b ? 1 : 0);
    public static bool2 operator !=(bool2 a, int b) => 1 - (a == b);
    public static bool2 operator <(bool2 a, bool2 b) => bool2(a.X < b.X ? 1 : 0, a.Y < b.Y ? 1 : 0);
    public static bool2 operator <(bool2 a, int b) => bool2(a.X < b ? 1 : 0, a.Y < b ? 1 : 0);
    public static bool2 operator >(bool2 a, bool2 b) => bool2(a.X > b.X ? 1 : 0, a.Y > b.Y ? 1 : 0);
    public static bool2 operator >(bool2 a, int b) => bool2(a.X > b ? 1 : 0, a.Y > b ? 1 : 0);
    public static bool2 operator <=(bool2 a, bool2 b) => 1 - (a > b);
    public static bool2 operator <=(bool2 a, int b) => 1 - (a > b);
    public static bool2 operator >=(bool2 a, bool2 b) => 1 - (a < b);
    public static bool2 operator >=(bool2 a, int b) => 1 - (a < b);

    public override bool Equals(object obj) => base.Equals(obj);
    public override int GetHashCode() => base.GetHashCode();
  }
}