using System.Collections;

public class gsButton_Tutorial : gsButton_Tutorial_
{
  public override void RunMethod() => print("Method Button");

  public override IEnumerator RunCoroutine_Sync()
  {
    for (int i = 0, n = 1000; i < n; i++)
    {
      yield return Status(i, n, UI_RunCoroutine.label = $"Running {i + 1}");
      if (!in_RunCoroutine) break;
    }
    UI_RunCoroutine.label = "Coroutine";
    yield return Status();
  }
}
