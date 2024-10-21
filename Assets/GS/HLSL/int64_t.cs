// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC
using System;
using UnityEngine;

namespace GpuScript
{
  [System.Serializable]
  public struct int64_t 
  {
    public int x;

    public int64_t(int x) { this.x = x; }
    public static implicit operator int(int64_t p) => p.x; 
    public static implicit operator int64_t(int p) => new int64_t(p); 
    public int this[int i] { get => x; set => x = value; }
  }
}
