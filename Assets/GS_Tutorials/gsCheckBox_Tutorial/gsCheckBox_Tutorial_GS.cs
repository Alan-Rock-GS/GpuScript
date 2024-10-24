using GpuScript;
using UnityEngine;

public class gsCheckBox_Tutorial_GS : _GS
{
  [GS_UI, AttGS("UI|User Interface")] TreeGroup group_UI;
  [GS_UI, AttGS("Display Buttons|Show or hide the buttons")] bool displayButtons;
  [GS_UI, AttGS("Hello|Print Hello", UI.OnClicked, "print(\"Hello\");", UI.ShowIf, nameof(displayButtons))] void Hello() { }
  [GS_UI, AttGS("Normal Button|Button action specified in code", UI.ShowIf, nameof(displayButtons))] void NormalButton() { }
  [GS_UI, AttGS("Coroutine Button|Button with coroutine", UI.Sync, UI.ShowIf, nameof(displayButtons))] void CoroutineButton() { }
  [GS_UI, AttGS("UI|User Interface")] TreeGroupEnd group_UI_End;
}