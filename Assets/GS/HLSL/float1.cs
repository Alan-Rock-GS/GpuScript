// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC
using System;
using UnityEngine;

namespace GpuScript
{
  [System.Serializable]
  public class Float1
  {
    public float x;
    public Float1() { }
    public Float1(float x) => this.x = x;
    public static implicit operator float(Float1 p) => p.x;
    public static implicit operator Float1(float p) => new Float1(p);
    public float this[int i] { get => x; set => x = value; }
  }
}
