using GpuScript;

public class gsButton_Tutorial_GS : _GS
{
  [GS_UI, AttGS("UI|User Interface")] TreeGroup group_UI; 
  [GS_UI, AttGS("Hello|Print Hello", UI.OnClicked, "print(\"Hello\");")] void Hello() { }

  [GS_UI, AttGS("Run Method|Button that runs a method")] void RunMethod() { }

  [GS_UI, AttGS("Run Coroutine|Button that runs a coroutine", UI.Sync)] void RunCoroutine() { }

  [GS_UI, AttGS("UI|User Interface")] TreeGroupEnd groupEnd_UI;
}