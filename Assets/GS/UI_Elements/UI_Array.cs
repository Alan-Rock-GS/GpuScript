namespace GpuScript
{
  public class UI_Array : UI_VisualElement
  {
    public new static void _cs_Write(GS gs, StrBldr tData, StrBldr lateUpdate, StrBldr lateUpdate_ValuesChanged,
      StrBldr showIfs, StrBldr onValueChanged, AttGS attGS, string typeStr, string name)
    {
      tData.Add($"\n    public {name}[] {name};");
    }
  }
}