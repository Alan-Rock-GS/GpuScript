using GpuScript;
using System.Collections;
using UnityEngine;

public class gsBDraw_Fix_Tutorial : gsBDraw_Fix_Tutorial_
{
  public override void NormalButton() => print("Normal Button");

  public override IEnumerator CoroutineButton_Sync()
  {
    for (int i = 0; i < 3; i++)
    {
      yield return new WaitForSeconds(1);
      print(i + 1);
    }
  }
}