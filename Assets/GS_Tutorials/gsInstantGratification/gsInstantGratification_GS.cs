using GpuScript;

public class gsInstantGratification_GS : _GS
{
	[GS_UI, AttGS("Hello|Print Hello", UI.OnClicked, "print(\"Hello\");")] void Hello() { }
}