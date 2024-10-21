using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace GpuScript
{
  //[UxmlElement]
  public class UI_GS : VisualElement
  {
    public new class UxmlFactory : UxmlFactory<UI_GS, UxmlTraits> { }
    public new class UxmlTraits : UI_VisualElement.UxmlTraits { }
  }
}