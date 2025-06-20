using Functional;
using GpuScript;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceProcess;
using UnityEngine;
using UnityEngine.UIElements;
using static gsFunctional;

/// <summary>
/// https://www.youtube.com/watch?v=UM-3ZsHhogA&list=PLYV2P3sg-Rw5JGN0KVSmO9VESNr9R90xW&index=8
/// </summary>
public class gsFunctional : gsFunctional_
{
  //public override void onLoaded()
  //{
  //  base.onLoaded();
  //  //var root = GetComponent<UIDocument>().rootVisualElement;
  //  var dropdown = new DropdownField("Option", new List<string> { "Option 1", "Option 2", "Option 3" }, 0);
  //  dropdown.RegisterValueChangedCallback(evt => print($"Selected: {evt.newValue}"));
  //  //uiDocument.rootVisualElement.Add(dropdown);
  //  uiDocument.rootVisualElement.Insert(0, dropdown);
  //  //UI_group_UI.Insert(0, dropdown);
  //  //dropdown.refresh
  //}
  public override void RunScreenshot()
  {
    print("RunScreenshot");
#if UNITY_STANDALONE_WIN
    SaveWholeScreen($"{projectPath}Screen.jpg");
#endif //UNITY_STANDALONE_WIN
  }

  class MEx : Exception { public object v; public MEx(object o) => v = o; }
  public int MethodA(int arg) { try { return MethodB(arg); } catch (MEx e) { throw new MEx((int)e.v + 2); } }
  public int MethodB(int arg) { try { return MethodC(arg); } catch (MEx e) { throw new MEx(arg % (int)e.v); } }
  public int MethodC(int arg) => throw new MEx(arg > 10 ? 2 : 3);
  public override void RunExceptional() { try { MethodA(2); } catch (MEx e) { print(e.v); } }

  public override IEnumerator StopWindowsUpdate_Sync()
  {
    ServiceController sc = new("Windows Update");
    print($"Status = {sc.Status}, CanStop = {sc.CanStop}");
    if (sc.Status == ServiceControllerStatus.Running)
    {
      yield return Status("Stopping Windows Service");
      for (sc.Stop(); sc.Status == ServiceControllerStatus.StopPending;) yield return null;
    }
    yield return Status("Windows Service Stopped");
  }
  public override void MatMultiply()
  {
    AllocData_Mat_A(MatN * MatRowN * MatRowN); AllocData_Mat_x(MatN * MatRowN); AllocData_Mat_b(MatN * MatRowN);
    Gpu_Init_Mat_A(); Gpu_Init_Mat_x(); Gpu_Init_Mat_b();
    print($"GPU Multiply\t{MatN}\t{MatRowN}\t{TimeAction_Str(100, () => Gpu_Multiply_Mat_A_x(), Unit.us)}");
    print($"CPU Multiply\t{MatN}\t{MatRowN}\t{Secs(() => Cpu_Multiply_Mat_A_x())} sec");
  }
  public uint MatA_Index(uint matI, uint i, uint colI) => id_to_i(matI, i, colI, MatN, MatRowN);
  public float MatA(uint matI, uint i, uint j) => Mat_A[MatA_Index(matI, i, j)];
  public void MatA(uint matI, uint i, uint j, float v) => Mat_A[MatA_Index(matI, i, j)] = v;
  public uint Mat_Index(uint matI, uint i) => id_to_i(matI, i, MatN, MatRowN);
  public float Matx(uint matI, uint i) => Mat_x[Mat_Index(matI, i)];
  public void Matx(uint matI, uint i, float v) => Mat_x[Mat_Index(matI, i)] = v;
  public int Matb(uint matI, uint i) => Mat_b[Mat_Index(matI, i)];
  public void Matb(uint matI, uint i, int V) => Mat_b[Mat_Index(matI, i)] = V;
  public override void Init_Mat_A_GS(uint3 id) { uint matI = id.x, i = id.y, j = id.z; MatA(matI, i, j, 1); }
  public override void Init_Mat_x_GS(uint3 id) { uint matI = id.x, i = id.y; Matx(matI, i, 1); }
  public override void Init_Mat_b_GS(uint3 id) { uint matI = id.x, i = id.y; Matx(matI, i, 0); }
  public override void Multiply_Mat_A_x_GS(uint3 id) { uint matI = id.x, i = id.y, j = id.z; InterlockedAdd(Mat_b, Mat_Index(matI, j), roundi(MatA(matI, i, j) * Matx(matI, j))); }

  public override void BillionLoop()
  {
    AllocData_B_Ints(BillionN = 1_000_000);
    print($"Billion Loop Runtime = {TimeAction_Str(100, () => Gpu_RunBillion(), Unit.us)}");
  }
  public override void RunBillion_GS(uint3 id) { for (uint i = 0; i < 10000; i++) InterlockedAdd(B_Ints, id.x, i); }

  public override void Init_ints_GS(uint3 id)
  {
    uint listI = id.x, operationI = id.y;
    ints[listI * Ints_N + operationI] = operationI == Ints_Min ? int_max : operationI == Ints_Max ? int_min : 0;
  }
  public override void Init_Lists_GS(uint3 id) { uint listI = id.x, numberI = id.y; Numbers[listI * NumberN + numberI] = numberI / (float)NumberN; }
  //public override void Calc_Min_Max_Sum_GS(uint3 id)
  //{
  //  uint listI = id.x, numberI = id.y, operationI = id.z;
  //  float v = Numbers[listI * NumberN + numberI];
  //  int V = roundi(v * 1e6f);
  //  if (operationI == Ints_Min) InterlockedMin(ints, listI * Ints_N + operationI, V);
  //  else if (operationI == Ints_Max) InterlockedMax(ints, listI * Ints_N + operationI, V);
  //  else InterlockedAdd(ints, listI * Ints_N + operationI, V);
  //}
  public override void Calc_Min_Max_Sum_GS(uint3 id)
  {
    uint listI = id.x, numberI = id.y, opI = id.z, i = listI * Ints_N + opI;
    float v = Numbers[listI * NumberN + numberI];
    int V = roundi(v * 1e6f);
    if (opI == Ints_Min) InterlockedMin(ints, i, V);
    else if (opI == Ints_Max) InterlockedMax(ints, i, V);
    else InterlockedAdd(ints, i, V);
  }

  public override void MinMaxSum()
  {
    InitBuffers();
    Gpu_Init_ints();
    Gpu_Init_Lists();
    float sum = 0, mn = fPosInf, mx = fNegInf;
    print($"Min Max Sum: {TimeAction_Str(3, () => Gpu_Calc_Min_Max_Sum(), Unit.us)}");
    print($"Cpu: {Secs(() => { for (int i = 0; i < NumberN; i++) { float v = Numbers[i]; mn = min(mn, v); mx = max(mn, v); sum += v; } }) * ListN:#,###} sec");
  }

  public override void LinqMax()
  {
    AllocData_intNums(NumberN = 100_000);
    AllocData_MaxInt(1);
    Gpu_Init_MaxInt();
    Gpu_Init_intNums();
    intNums.GetData();
    print($"GS Max: {TimeAction_Str(100, () => Gpu_Calc_Max(), Unit.us)}");
    print($"Linq Max: {TimeAction_Str(100, () => intNums.Data.Max(), Unit.us)}");
  }
  public override void Init_MaxInt_GS(uint3 id) => MaxInt[id.x] = int_min;
  public override void Init_intNums_GS(uint3 id) => intNums[id.x] = (int)id.x;
  public override void Calc_Max_GS(uint3 id) => InterlockedMax(MaxInt, 0, intNums[id.x]);

  public void RunMethod2(Action<int> f) { f(3); }
  public void Method2Finished(int i) { print($"Method2Finished({i})"); }
  public void RunMethod(Func<int> f) { f(); }
  public int MethodFinished() { print("MethodFinished"); return 1; }
  public IEnumerator RunCouroutine(Func<int> f) { yield return new WaitForSeconds(2); f(); }
  public int CouroutineFinished() { print("CouroutineFinished"); return 1; }

  public IEnumerator RunCouroutine2() { yield return new WaitForSeconds(2); print("Couroutine2Finished"); }

  public override IEnumerator TestCoroutine_Sync()
  {
    yield return "RunCouroutine".InvokeCoroutine(this, new object[] { (Func<int>)CouroutineFinished });
    yield return "RunCouroutine2".InvokeCoroutine(this);
  }

  public IEnumerator Test3_Coroutine(Action<(int, float)> a) { a((1, 2.0f)); yield return null; }
  Func<string, string> toUpper = s => s.ToUpper();
  Func<(int a, int b), int> plus = c => c.a + c.b;
  public IEnumerable<float> Iter((float a, float b, float dx) r)
  {
    float a = r.a, b = r.b, dx = abs(r.dx);
    if (dx != 0) { if (a < b) for (; a <= b + dx / 2; a += dx) yield return a; else if (a > b) for (; a >= b - dx / 2; a -= dx) yield return a; else yield return a; }
  }
  public static IEnumerable<int> Iter((int a, int b, int dx) r)
  {
    int a = r.a, b = r.b, dx = abs(r.dx);
    if (dx != 0) { if (a < b) for (; a < b; a += dx) yield return a; else if (a > b) for (b -= dx; b >= a; b -= dx) yield return b; }
  }
  public override void RunTest()
  {
    print($"uint = {((uint)1000000).ToString("#,###")}");
  }
  public record Person(string Name, DateTime BirthDate, string Nickname = "")
  {
    public override string ToString()
    {
      var members = this.GetMembers();
      return $"{GetType().Name}[{members.Length}] {{ {Name}, {BirthDate.ToString("M/dd/yyyy")}{(Nickname.IsNotEmpty() ? $", Nickname = {Nickname}" : "")} }}";
    }
    public void Deconstruct(out string name, out int year, out int month, out int day) { name = Name; year = BirthDate.Year; month = BirthDate.Month; day = BirthDate.Day; }
  }
  IEnumerable<Person> people = new Person[]
{
  "Joe" .Person(1997, 4, 28, "Pinky"),
  "Jane".Person(1982, 5, 14),
  "Joe" .Person(1986, 7, 19),
  "Joe" .Person(1997, 4, 28),
  "Jane".Person(1982, 5, 14),
  new("Jane", new(1982, 5, 14)),
};


  public override void RunPerson()
  {
    Person joe = "Joe".Person(1997, 4, 28, nickname: "Pinky"), jim = joe with { Name = "Jim", Nickname = "" }, jane = "Jane".Person(1982, 5, 14);
    print($"{joe}, {jim}, {jane}");
    print(people.ToString());
    print(people.ToStr());

    Demo.Program.Main(null);
  }
}

namespace Functional
{
  public static class PersonX
  {
    public static Person Person(this string name, DateTime birthDate, string nickname = "") => new(name, birthDate, nickname);
    public static Person Person(this string name, int year, int month, int day, string nickname = "") => new(name, new(year, month, day), nickname);
    public static Person Person(this string name, (int, int, int) birthDate, string nickname = "") => new(name, new DateTime(birthDate.Item1, birthDate.Item2, birthDate.Item3), nickname);
    public static bool IsBeforeBirthday(this Person p, int month, int day) => month < p.BirthDate.Month || (month == p.BirthDate.Month && day < p.BirthDate.Day);
    public static int GetAge(this Person p, DateTime date) => date.Year - p.BirthDate.Year + (p.IsBeforeBirthday(date.Month, date.Day) ? 0 : 1);
    public static string ToString(this IEnumerable<Person> persons) => String.Join(", ", persons.Select(a => a.Name).ToArray());
    public static string ToStr(this IEnumerable<Person> persons) => String.Join(", ", persons.Select(a => a.Name).ToArray());
  }
}

namespace Demo
{
  interface IUser { }
  class AnonymousUser : IUser { }
  class RegisteredUser : IUser { public string UserName { get; } public RegisteredUser(string userName) => UserName = userName; }
  class ProUser : IUser
  {
    public string Token { get; }
    public string Name { get; }
    public int PromoPoints { get; }
    public ProUser(string token, string name, int promoPoints)
    {
      Token = token; Name = name;
      PromoPoints = promoPoints >= 0 ? promoPoints : throw new ArgumentException(nameof(promoPoints));
    }
    public ProUser(string token, string name) : this(token, name, 0) { }
    public ProUser Add(int points) => new ProUser(Token, Name, PromoPoints + points);
    public ProUser Spend(int points) => new ProUser(Token, Name, PromoPoints - points);
    public override string ToString()
    {
      return $"{Name} +{PromoPoints}";
    }
  }
  interface IMessage { string Content { get; } string PostedBy { get; } }
  class Posting
  {
    public IMessage Message { get; }
    public IUser Poster { get; }
    public Posting(IMessage message, IUser poster) { Message = message; Poster = poster; }
    public void Deconstruct(out IMessage message, out IUser poster) { message = Message; poster = Poster; }
    public static implicit operator Posting((IMessage message, IUser poster) tuple) => new Posting(tuple.message, tuple.poster);
  }
  static class Messaging
  {
    public static Posting Post(this IUser by, string content) => by switch
    {
      ProUser pro when pro.PromoPoints > 0 => (new ApprovedMessage(content, pro.Name), pro.Spend(1)),
      ProUser pro => (new PendingMessage(content, pro.Name, DateTime.Today.AddDays(14)), by),
      RegisteredUser registered => (new PendingMessage(content, registered.UserName, DateTime.Today.AddDays(7)), by),
      AnonymousUser _ => (new PendingMessage(content, "Anonymous", DateTime.Today.AddDays(2)), by),
      _ => throw new InvalidOperationException("What am I doing here?")
    };

    public static string DisplayName(this IUser by) =>
      by is ProUser pro ? pro.Name
      : by is RegisteredUser registered ? registered.UserName
      : "Anonymous";
  }
  class ApprovedMessage : IMessage
  {
    public string Content { get; }
    public string PostedBy { get; }
    public ApprovedMessage(string content, string by) { Content = content; PostedBy = by; }
  }
  class PendingMessage : IMessage
  {
    public string Content { get; }
    public string PostedBy { get; }
    public DateTime ExpireOn { get; }
    public PendingMessage(string content, string by, DateTime expireOn) { Content = content; PostedBy = by; ExpireOn = expireOn; }
  }
  class Person
  {
    public string Name { get; }
    public Person(string name) { Name = name; }
    public Person WithName(string name) => new Person(name);
  }
  class Program
  {
    static string NameOf(object o) => o.GetType().Name;
    static void print(object s) { GS.print(s); }
    public static void Main(string[] args)
    {
      object o = "Something";
      print(NameOf(o).Length);

      Person person = new Person("Mr. Baggins");
      print(person.Name.ToUpper());

      IUser bilbo = new ProUser("Sting", "Mr. Baggins").Add(9);
      print($"Hello {bilbo.DisplayName()}");

      var (msg, finalBilbo) = bilbo.Post("Hi everyone!");
      print($"({msg}, {finalBilbo})");
    }
  }
}
