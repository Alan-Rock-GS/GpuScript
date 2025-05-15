using System.Net.Mail;
using System.Net;
using UnityEngine;

public static class libX
{
  public static void SendGmail(string toEmail, string toName, string subject, string body)
  {
    string fromEmail = "alanrock.gs@gmail.com", fromName = "Summit Peak Technologies LLC", fromPassword = "grphpsqsehtfkvvh";
    MailAddress fromAddress = new MailAddress(fromEmail, fromName), toAddress = new MailAddress(toEmail, toName);
    var smtp = new SmtpClient { Host = "smtp.gmail.com", Port = 587, EnableSsl = true, DeliveryMethod = SmtpDeliveryMethod.Network, Credentials = new NetworkCredential(fromAddress.Address, fromPassword), Timeout = 20000 };
    using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body }) { smtp.Send(message); }
  }
  public static uint Hash(string s) { unchecked { uint h1 = 5381, h2 = h1; for (int i = 0; i < s.Length; i += 2) { h1 = ((h1 << 5) + h1) ^ s[i]; if (i < s.Length - 1) h2 = ((h2 << 5) + h2) ^ s[i + 1]; } return h1 + h2 * 1566083957; } }
  public static uint Key_6_digit(string s) => (Hash(s) % 900000) + 100000;
  public static uint Key_6_digit(string lib, string email, string expires) => Key_6_digit($"{expires}_{lib}_{email}");
  public static uint Key_6_digit(string app, string email, uint deviceID, string expires) => Key_6_digit($"{app}_{deviceID}_{expires}_{email}");


}
