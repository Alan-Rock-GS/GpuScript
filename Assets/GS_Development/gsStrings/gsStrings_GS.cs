using GpuScript;

public class gsStrings_GS : _GS
{
  [GS_UI, AttGS("UI|User Interface")] TreeGroup group_UI;
  //[GS_UI, AttGS("Strings|User Interface")] TreeGroup group_Strings;
  [GS_UI, AttGS("Names|Select a name", UI.Vals, "Alan|Weston|Marshal")] strings Names;
  //[GS_UI, AttGS("Strings|User Interface")] TreeGroupEnd groupEnd_Strings;
  [GS_UI, AttGS("UI|User Interface")] TreeGroupEnd groupEnd_UI;
}

