using GpuScript;

public class gsButton_Tutorial_GS : _GS
{
  [GS_UI, AttGS("UI|User Interface")] TreeGroup group_UI;
  [GS_UI, AttGS("Hello|Print Hello", UI.OnClicked, "print(\"Hello\");")] void Hello() { }
  [GS_UI, AttGS("Normal Button|Button action specified in code")] void NormalButton() { }
  [GS_UI, AttGS("Coroutine Button|Button with coroutine", UI.Sync)] void CoroutineButton() { }
  [GS_UI, AttGS("UI|User Interface")] TreeGroupEnd group_UI_End;
}