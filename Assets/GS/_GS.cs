// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC
using UnityEngine;

namespace GpuScript
{
  public class _GS : MonoBehaviour
  {
    public void Size(params object[] size) { }
    public void SyncSize(params object[] size) { }
    [System.Obsolete("Sync() is deprecated, please use SyncSize(...) instead.")] public void Sync() { }
  }
}