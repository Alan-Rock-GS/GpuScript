using GpuScript;

public class gsARand_02_GS : _GS
{
	[GS_UI, AttGS("UI|User Interface")] TreeGroup group_UI;
	[GS_UI, AttGS("Hello|Print Hello", UI.OnClicked, "print(\"Hello\");")] void Hello() { }
	[GS_UI, AttGS("UI|User Interface")] TreeGroupEnd groupEnd_UI;
}