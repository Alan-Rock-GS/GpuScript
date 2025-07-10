using UnityEngine.UIElements;

namespace GpuScript
{
#if NEW_UI
  [UxmlElement] public partial class UI_GS : VisualElement { [UxmlAttribute] public bool ok; }
#elif !NEW_UI
  public class UI_GS : VisualElement
  {
    public new class UxmlFactory : UxmlFactory<UI_GS, UxmlTraits> { }
    public new class UxmlTraits : UI_VisualElement.UxmlTraits { }
  }
#endif //NEW_UI
}
