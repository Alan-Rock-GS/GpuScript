using GpuScript;
using System;
using System.Runtime.InteropServices;
/// <summary>
///    MouseOperations.MouseEvent(MouseOperations.Mouse.LeftUp | MouseOperations.Mouse.LeftDown); //send mouse click event
/// </summary>
public class MouseOperations
{
  [Flags] public enum Mouse { Move = 0x1, LeftDown = 0x2, LeftUp = 0x4, RightDown = 0x8, RightUp = 0x10, MiddleDown = 0x20, MiddleUp = 0x40, Absolute = 0x8000 }
  [StructLayout(LayoutKind.Sequential)] public struct MousePoint { public int X, Y; public MousePoint(int x, int y) { X = x; Y = y; } }
  [DllImport("user32.dll")] private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
  [DllImport("user32.dll")][return: MarshalAs(UnmanagedType.Bool)] private static extern bool GetCursorPos(out MousePoint lpMousePoint);
  [DllImport("user32.dll", EntryPoint = "SetCursorPos")][return: MarshalAs(UnmanagedType.Bool)] private static extern bool SetCursorPos(int X, int Y);
  public static void MouseEvent(Mouse v) { int2 p = GetCursorPosition(); mouse_event((int)v, p.x, p.y, 0, 0); }
  public static int2 GetCursorPosition() { MousePoint p; if (!GetCursorPos(out p)) p = new MousePoint(0, 0); return new int2(p.X, p.Y); }
  public static void SetCursorPosition(int X, int Y) { SetCursorPos(X, Y); }
  public static void SetCursorPosition(int2 p) { SetCursorPos(p.x, p.y); }
}