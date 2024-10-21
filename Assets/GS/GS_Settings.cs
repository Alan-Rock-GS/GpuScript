// GpuScript Copyright (C) 2024 Summit Peak Technologies, LLC
using UnityEngine;

namespace GpuScript
{
  public class GS_Settings : MonoBehaviour
  {
    public static object[] Size, Include, DrawSegments;
  }
  [System.Serializable]
  //public struct strings { public int index; public string v; }
  public struct strings { public string v; }
}