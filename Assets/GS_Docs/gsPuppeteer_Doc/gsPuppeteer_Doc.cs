using GpuScript;
using System.Collections;

public class gsPuppeteer_Doc : gsPuppeteer_Doc_
{
  public override IEnumerator Translate_Sync()
  {
    string english = "Hello";
    yield return Puppeteer_Lib.Open_Google_Language_Translate_Browser("English", "Chinese");
    yield return Puppeteer_Lib.GoogleTranslate(english);
    yield return Puppeteer_Lib.GoogleTranslate(english);
    print_status($"{english} => {Puppeteer_Lib.translation}");
  }
  public override IEnumerator Locate_Sync()
  {
    yield return Puppeteer_Lib.Get_Browser_Page_Coroutine(false, "https://www.iplocation.Net/ip-lookup");
    yield return Puppeteer_Lib.MouseClick_Coroutine(45, 485); yield return Puppeteer_Lib.SelectAll_Copy_Coroutine();
    string s = Clipboard, public_IP = s.Between("IP ADDRESS:\r\n", "\r\n").Trim();
    string ip_city = s.Between("CITY:\r\n", "\r\n").Trim(), ip_state = s.Between("REGION:\r\n", "\r\n").Trim(), ip_country = s.Between("COUNTRY:\r\n", "\r\n").Trim();
    string ip_lat_long = $"{s.Between("LATITUDE:\r\n", "\r\n").Trim()}, {s.Between("LONGITUDE:\r\n", "\r\n").Trim()}";
    print_status($"{public_IP} => {ip_city}, {ip_state}, {ip_country}, {ip_lat_long}");
  }
}