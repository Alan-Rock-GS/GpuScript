using GpuScript;

public class gsInstantGratification_GS : _GS
{
  [GS_UI, AttGS("UI|User Interface")] TreeGroup group_UI;
  [GS_UI, AttGS("Instant Gratification|Run Instant Gratification")] void RunInstantGratification() { }
  [GS_UI, AttGS("UI|User Interface")] TreeGroupEnd group_UI_End;
}