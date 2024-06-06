using DALayer.Entities;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using System.Net;
using System.Net.Mail;

namespace PLayer.Helper
{
    public static class MailSetting
    {
        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com" ,587) ;
            client.EnableSsl = true ;
            client.Credentials = new NetworkCredential("somayamagdyaboelola@gmail.com", "xsgoyytbihjrwxcj");
            client.Send("somayamagdyaboelola@gmail.com", email.To, email.Subject, email.Body); 
        }
    }
}
